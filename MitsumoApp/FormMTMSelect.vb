Public Class FormMTMSelect

    '---------------------
    '条件値
    '---------------------
    Public selectMTMR002001 As String         '得意先コード
    Public selectMTMR002002 As String         '商品コード
    Public selectMTMR002003 As String         '規格

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()
    End Sub
    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonUriSelect_Click(sender As Object, e As EventArgs) Handles ButtonUriSelect.Click
        '売上履歴
        Dim formMTMSearchUriage As New FormMTMSearchUriage
        formMTMSearchUriage.selectMTMR002001 = Me.selectMTMR002001
        formMTMSearchUriage.selectMTMR002002 = Me.selectMTMR002002
        formMTMSearchUriage.selectMTMR002003 = Me.selectMTMR002003
        Dim result As DialogResult = formMTMSearchUriage.ShowDialog()
        If result = DialogResult.OK Then
        End If
        formMTMSearchUriage.Dispose()
    End Sub

    Private Sub ButtonTankaSelect_Click(sender As Object, e As EventArgs) Handles ButtonTankaSelect.Click
        '単価台帳
        Dim formMTMSearchTanka As New FormMTMSearchTanka
        formMTMSearchTanka.selectMTMR002001 = Me.selectMTMR002001
        formMTMSearchTanka.selectMTMR002002 = Me.selectMTMR002002
        formMTMSearchTanka.selectMTMR002003 = Me.selectMTMR002003
        Dim result As DialogResult = formMTMSearchTanka.ShowDialog()
        If result = DialogResult.OK Then
        End If
        formMTMSearchTanka.Dispose()
    End Sub
    ''' <summary>
    ''' フォームイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSelect_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.No
            Me.Close()
        End If
    End Sub
End Class