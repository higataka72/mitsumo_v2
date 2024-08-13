Public Class FormMTMProgress
End Class
Public Class DialogMTMProgress
    Implements IDisposable

    Private disposedValue As Boolean
    Private formProgress As FormMTMProgress                     'ダイアログフォーム
    Private ownerForm As Form                                   'オーナーフォーム
    Private startEvent As System.Threading.ManualResetEvent     'フォームが表示されるまで待機するための待機ハンドル
    Private thread As System.Threading.Thread                   '別処理をするためのスレッド
    Private showed As Boolean = False                           'フォームが一度表示されたか
    Private _message As String = ""                             '表示するメッセージ
    Private closing As Boolean = False                          'フォームをコードで閉じているか
    Private _canceled As Boolean = False                        'キャンセルボタンがクリックされたか
    Private ReadOnly canceledSyncObject = New Object

    Public Property UserCanceled() As Boolean
        Get
            SyncLock (canceledSyncObject)
                Return Me._canceled
            End SyncLock
        End Get
        Set(ByVal Value As Boolean)
            SyncLock (canceledSyncObject)
                Me._canceled = Value
            End SyncLock
        End Set
    End Property

    ''' <summary>
    ''' ダイアログに表示するメッセージ
    ''' </summary>
    Public Property Message As String
        Get
            Return _message
        End Get
        Set(ByVal Value As String)
            _message = Value
            If Not (formProgress Is Nothing) Then
                formProgress.Invoke(New MethodInvoker(AddressOf SetMessage))
            End If
        End Set
    End Property

    ''' <summary>
    ''' キャンセルされたか
    ''' </summary>
    Public ReadOnly Property Canceled() As Boolean
        Get
            Return _canceled
        End Get
    End Property
    ''' <summary>
    ''' 終了イベント
    ''' </summary>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: マネージド状態を破棄します (マネージド オブジェクト)
            End If

            ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
            ' TODO: 大きなフィールドを null に設定します
            disposedValue = True
        End If
    End Sub
    ''' <summary>
    ''' 終了イベント
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        If Not (formProgress Is Nothing) Then
            formProgress.Invoke(New MethodInvoker(AddressOf formProgress.Dispose))
        End If
        ' このコードを変更しないでください。クリーンアップ コードを 'Dispose(disposing As Boolean)' メソッドに記述します
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
    ''' <summary>
    ''' ダイアログ表示イベント
    ''' </summary>
    <Obsolete>
    Public Overloads Sub Show(ByVal owner As Form)
        If showed Then
            Throw New Exception("ダイアログは一度表示されています。")
        End If
        showed = True

        startEvent = New System.Threading.ManualResetEvent(False)
        ownerForm = owner

        'スレッドを作成
        thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf Run))
        thread.IsBackground = True
        thread.ApartmentState = System.Threading.ApartmentState.STA
        thread.Start()

        'フォームが表示されるまで待機する
        startEvent.WaitOne()
    End Sub
    ''' <summary>
    ''' ダイアログイベント
    ''' </summary>
    <Obsolete>
    Public Overloads Sub Show()
        Show(Nothing)
    End Sub
    ''' <summary>
    ''' ダイアログ開始イベント
    ''' </summary>
    Private Sub Run()
        'フォームの設定
        formProgress = New FormMTMProgress
        AddHandler formProgress.Activated, AddressOf form_Activated
        'フォームの表示位置をオーナーの中央へ
        If Not (ownerForm Is Nothing) Then
            formProgress.StartPosition = FormStartPosition.Manual
            formProgress.Left =
                ownerForm.Left + (ownerForm.Width - formProgress.Width) \ 2
            formProgress.Top =
                ownerForm.Top + (ownerForm.Height - formProgress.Height) \ 2
        End If
        'フォームの表示
        formProgress.ShowDialog()

        formProgress.Dispose()
    End Sub
    ''' <summary>
    ''' メッセージイベント
    ''' </summary>
    Private Sub SetMessage()
        If Not (formProgress Is Nothing) And Not formProgress.IsDisposed Then
            formProgress.LabelMessage.Text = _message
        End If
    End Sub
    ''' <summary>
    ''' フォーム処理イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub form_Activated(ByVal sender As Object, ByVal e As EventArgs)
        RemoveHandler formProgress.Activated, AddressOf form_Activated
        startEvent.Set()
    End Sub
    ''' <summary>
    ''' フォーム処理イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub form_Closing(ByVal sender As Object,
        ByVal e As System.ComponentModel.CancelEventArgs)
        If Not closing Then
            e.Cancel = True
            _canceled = True
        End If
    End Sub
    ''' <summary>
    ''' ダイアログを閉じる
    ''' </summary>
    Public Sub Close()
        If Not (formProgress Is Nothing) Then
            formProgress.Invoke(New MethodInvoker(AddressOf formProgress.Close))
        End If
    End Sub
End Class