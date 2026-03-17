Imports System.Configuration
Imports MitsumoLib
Public Class FormMTM07

    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' 価格入力ビジネスロジック
    ''' </summary>
    Private ReadOnly Biz07 As Biz.MTM07

    Public ReadOnly SearchCondition As Biz.MTM07SearchCondition

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="formMenu"></param>
    Public Sub New(ByVal formMenu As FormMTMMenu)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.FormMenu = formMenu
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.Biz07 = New Biz.MTM07(connectionString)
        Me.SearchCondition = New Biz.MTM07SearchCondition
    End Sub
    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM07_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM07_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '条件指定
        Me.TextBox005.Text = ""
        Me.TextBox006.Text = ""
        Me.TextBox007.Text = ""
        Me.TextBox008.Text = ""
        Me.TextBox009.Text = ""
        Me.TextBox010.Text = ""
        Me.TextBox011.Text = ""
        Me.TextBox012.Text = ""
        Me.TextBox013.Text = ""
        Me.TextBox014.Text = ""
        Me.RadioButton001.Checked = True

        '日付
        Me.DatePicker004.Format = DateTimePickerFormat.Custom
        Me.DatePicker004.CustomFormat = " "
        Me.DatePicker004.Checked = False
        Me.DatePicker005.Format = DateTimePickerFormat.Custom
        Me.DatePicker005.CustomFormat = " "
        Me.DatePicker005.Checked = False
        Me.DatePicker006.Format = DateTimePickerFormat.Custom
        Me.DatePicker006.CustomFormat = DateTime.Today
        Me.DatePicker006.Checked = True
        SearchCondition.SimekiribiTo = DateTime.Today.ToString("yyyyMMdd")
        Me.DatePicker007.Format = DateTimePickerFormat.Custom
        Me.DatePicker007.CustomFormat = " "
        Me.DatePicker007.Checked = False

        'TextBox007のLostFocusイベントハンドラを追加する
        AddHandler DatePicker004.LostFocus, AddressOf DatePicker004_LostFocus
        'TextBox007のLostFocusイベントハンドラを追加する
        AddHandler TextBox005.LostFocus, AddressOf TextBox005_LostFocus
        'TextBox007のLostFocusイベントハンドラを追加する
        AddHandler TextBox007.LostFocus, AddressOf TextBox007_LostFocus
        'TextBox007のLostFocusイベントハンドラを追加する
        AddHandler TextBox011.LostFocus, AddressOf TextBox011_LostFocus

    End Sub
    'LostFocusイベントハンドラ
    Private Sub DatePicker004_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If (Not String.IsNullOrEmpty(Trim(Me.DatePicker004.Text))) Then
            If (String.IsNullOrEmpty(Trim(Me.DatePicker005.Text))) Then
                Me.DatePicker005.Format = DateTimePickerFormat.Long
                Me.DatePicker005.Value = Me.DatePicker004.Value
                Me.DatePicker005.Checked = True
            End If
        Else
            Me.DatePicker005.Format = DateTimePickerFormat.Custom
            Me.DatePicker005.CustomFormat = " "
            Me.DatePicker005.Checked = False
        End If
    End Sub
    'LostFocusイベントハンドラ
    Private Sub TextBox005_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If (Not String.IsNullOrEmpty(Trim(Me.TextBox005.Text))) Then
            If (String.IsNullOrEmpty(Trim(Me.TextBox015.Text))) Then
                Me.TextBox015.Text = Me.TextBox005.Text
            End If
        End If
    End Sub
    'LostFocusイベントハンドラ
    Private Sub TextBox007_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If (Not String.IsNullOrEmpty(Trim(Me.TextBox007.Text))) Then
            If (String.IsNullOrEmpty(Trim(Me.TextBox009.Text))) Then
                Me.TextBox009.Text = Me.TextBox007.Text
            End If
        End If
    End Sub
    'LostFocusイベントハンドラ
    Private Sub TextBox011_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If (Not String.IsNullOrEmpty(Trim(Me.TextBox011.Text))) Then
            If (String.IsNullOrEmpty(Trim(Me.TextBox013.Text))) Then
                Me.TextBox013.Text = Me.TextBox011.Text
            End If
        End If
    End Sub

    ''' <summary>
    ''' 検索イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch001.Click
        Dim rdo001 As Boolean = Me.RadioButton001.Checked     '未確定
        Dim rdo002 As Boolean = Me.RadioButton002.Checked     '未送信
        Dim txt005 As String = Me.TextBox005.Text.Trim        '営業所コードFrom
        Dim txt015 As String = Me.TextBox015.Text.Trim        '営業所コードTo
        Dim txt007 As String = Me.TextBox007.Text.Trim        '部課コードFrom
        Dim txt009 As String = Me.TextBox009.Text.Trim        '部課コードTo
        Dim txt011 As String = Me.TextBox011.Text.Trim        '担当者コードFrom
        Dim txt013 As String = Me.TextBox013.Text.Trim        '担当者コードTo


        Me.SearchCondition.Clear()
        Dim dml As Decimal
        Me.SearchCondition.Mikakutei = rdo001
        Me.SearchCondition.Misoushin = rdo002
        Me.SearchCondition.EigyosyoCodeFrom = txt005
        Me.SearchCondition.EigyosyoCodeTo = txt015
        Me.SearchCondition.BukaCodeFrom = txt007
        Me.SearchCondition.BukaCodeTo = txt009
        Me.SearchCondition.TantousyaCodeFrom = txt011
        Me.SearchCondition.TantousyaCodeTo = txt013

        '締切日(From)
        Dim dt004 As Date = Me.DatePicker004.Value
        Dim bl004 As Boolean = Me.DatePicker004.Checked
        If (bl004) Then
            If Not IsNothing(dt004) Then
                SearchCondition.SimekiribiFrom = dt004.ToString("yyyyMMdd")
            End If
        Else
            SearchCondition.SimekiribiFrom = ""
        End If
        '締切日(To)
        Dim dt005 As Date = Me.DatePicker005.Value
        Dim bl005 As Boolean = Me.DatePicker005.Checked
        If (bl005) Then
            If Not IsNothing(dt005) Then
                SearchCondition.SimekiribiTo = dt005.ToString("yyyyMMdd")
            End If
        Else
            SearchCondition.SimekiribiTo = ""
        End If
        '仕入先実施日(From)
        Dim dt006 As Date = Me.DatePicker006.Value
        Dim bl006 As Boolean = Me.DatePicker006.Checked
        If (bl006) Then
            If Not IsNothing(dt006) Then
                SearchCondition.JitsushibiFrom = dt006.ToString("yyyyMMdd")
            End If
        Else
            SearchCondition.JitsushibiFrom = ""
        End If
        '仕入先実施日(To)
        Dim dt007 As Date = Me.DatePicker007.Value
        Dim bl007 As Boolean = Me.DatePicker007.Checked
        If (bl007) Then
            If Not IsNothing(dt007) Then
                SearchCondition.JitsushibiTo = dt007.ToString("yyyyMMdd")
            End If
        Else
            SearchCondition.JitsushibiTo = ""
        End If


        Me.Hide()
        Dim formRegist As New FormMTM07ViewV2(Me)
        If (rdo001) Then
            formRegist.TypeText = "未確定確認"
        Else
            formRegist.TypeText = "未送信確認"
        End If
        formRegist.Show()
    End Sub

    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice001_Click(sender As Object, e As EventArgs) Handles ButtonOffice001.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox005.Text = formSearch.Selected.HANM036001
            Me.TextBox006.Text = formSearch.Selected.HANM036002
            Me.TextBox005.Focus()
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice002_Click(sender As Object, e As EventArgs) Handles ButtonOffice002.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox015.Text = formSearch.Selected.HANM036001
            Me.TextBox016.Text = formSearch.Selected.HANM036002
            Me.TextBox015.Focus()
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection001_Click(sender As Object, e As EventArgs) Handles ButtonSection001.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox007.Text = formSearch.Selected.HANM015001
            Me.TextBox008.Text = formSearch.Selected.HANM015002
            Me.TextBox007.Focus()
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection002_Click(sender As Object, e As EventArgs) Handles ButtonSection002.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox009.Text = formSearch.Selected.HANM015001
            Me.TextBox010.Text = formSearch.Selected.HANM015002
            Me.TextBox009.Focus()
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff001_Click(sender As Object, e As EventArgs) Handles ButtonStaff001.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox011.Text = formSearch.Selected.HANM004001
            Me.TextBox012.Text = formSearch.Selected.HANM004002
            Me.TextBox011.Focus()
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff002_Click(sender As Object, e As EventArgs) Handles ButtonStaff002.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox013.Text = formSearch.Selected.HANM004001
            Me.TextBox014.Text = formSearch.Selected.HANM004002
            Me.TextBox013.Focus()
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        Me.FormMenu.Show()
    End Sub

    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox005_TextChanged(sender As Object, e As EventArgs) Handles TextBox005.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox005.Text))) Then
            Me.TextBox006.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox007_TextChanged(sender As Object, e As EventArgs) Handles TextBox007.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox007.Text))) Then
            Me.TextBox008.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox009_TextChanged(sender As Object, e As EventArgs) Handles TextBox009.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox009.Text))) Then
            Me.TextBox010.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox011_TextChanged(sender As Object, e As EventArgs) Handles TextBox011.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox011.Text))) Then
            Me.TextBox012.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox013_TextChanged(sender As Object, e As EventArgs) Handles TextBox013.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox013.Text))) Then
            Me.TextBox014.Text = ""
        End If
    End Sub

    Private Sub TextBox005_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox005.KeyDown
        If e.KeyCode = Keys.F3 Then
            ButtonOffice001.PerformClick()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox007_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox007.KeyDown
        If e.KeyCode = Keys.F3 Then
            ButtonSection001.PerformClick()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox009_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox009.KeyDown
        If e.KeyCode = Keys.F3 Then
            ButtonSection002.PerformClick()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox011_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox011.KeyDown
        If e.KeyCode = Keys.F3 Then
            ButtonStaff001.PerformClick()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox013_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox013.KeyDown
        If e.KeyCode = Keys.F3 Then
            ButtonStaff002.PerformClick()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox023_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox024_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub RadioButton001_KeyDown(sender As Object, e As KeyEventArgs) Handles RadioButton001.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub RadioButton002_KeyDown(sender As Object, e As KeyEventArgs) Handles RadioButton002.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub RadioButton003_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub
    ''' <summary>
    ''' 締切日変更処理（From）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker004_ValueChanged(sender As Object, e As EventArgs) Handles DatePicker004.ValueChanged
        If IsNothing(Me.DatePicker004.Value) Then
            Me.DatePicker004.Format = DateTimePickerFormat.Custom
            Me.DatePicker004.CustomFormat = " "
        Else
            Me.DatePicker004.Format = DateTimePickerFormat.Long
        End If
    End Sub
    ''' <summary>
    ''' 締切日KeyDown（From）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker004_KeyDown(sender As Object, e As KeyEventArgs) Handles DatePicker004.KeyDown
        If e.KeyValue = Keys.Delete Then
            Me.DatePicker004.Format = DateTimePickerFormat.Custom
            Me.DatePicker004.CustomFormat = " "
            Me.DatePicker004.Checked = False
        End If
    End Sub
    ''' <summary>
    ''' 締切日変更処理（To）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker005_ValueChanged(sender As Object, e As EventArgs) Handles DatePicker005.ValueChanged
        If IsNothing(Me.DatePicker005.Value) Then
            Me.DatePicker005.Format = DateTimePickerFormat.Custom
            Me.DatePicker005.CustomFormat = " "
        Else
            Me.DatePicker005.Format = DateTimePickerFormat.Long
        End If
    End Sub
    ''' <summary>
    ''' 締切日KeyDown（From）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker005_KeyDown(sender As Object, e As KeyEventArgs) Handles DatePicker005.KeyDown
        If e.KeyValue = Keys.Delete Then
            Me.DatePicker005.Format = DateTimePickerFormat.Custom
            Me.DatePicker005.CustomFormat = " "
            Me.DatePicker005.Checked = False
        End If
    End Sub
    ''' <summary>
    ''' 仕入先実施日変更処理（From）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker006_ValueChanged(sender As Object, e As EventArgs) Handles DatePicker006.ValueChanged
        If IsNothing(Me.DatePicker006.Value) Then
            Me.DatePicker006.Format = DateTimePickerFormat.Custom
            Me.DatePicker006.CustomFormat = " "
        Else
            Me.DatePicker006.Format = DateTimePickerFormat.Long
        End If
    End Sub
    ''' <summary>
    ''' 仕入先実施日KeyDown（From）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker006_KeyDown(sender As Object, e As KeyEventArgs) Handles DatePicker006.KeyDown
        If e.KeyValue = Keys.Delete Then
            Me.DatePicker006.Format = DateTimePickerFormat.Custom
            Me.DatePicker006.CustomFormat = " "
            Me.DatePicker006.Checked = False
        End If
    End Sub
    ''' <summary>
    ''' 仕入先実施日変更処理（To）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker007_ValueChanged(sender As Object, e As EventArgs) Handles DatePicker007.ValueChanged
        If IsNothing(Me.DatePicker007.Value) Then
            Me.DatePicker007.Format = DateTimePickerFormat.Custom
            Me.DatePicker007.CustomFormat = " "
        Else
            Me.DatePicker007.Format = DateTimePickerFormat.Long
        End If
    End Sub
    ''' <summary>
    ''' 仕入先実施日KeyDown（To）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker007_KeyDown(sender As Object, e As KeyEventArgs) Handles DatePicker007.KeyDown
        If e.KeyValue = Keys.Delete Then
            Me.DatePicker007.Format = DateTimePickerFormat.Custom
            Me.DatePicker007.CustomFormat = " "
            Me.DatePicker007.Checked = False
        End If
    End Sub
End Class