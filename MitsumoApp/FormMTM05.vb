Imports System.Configuration
Imports MitsumoLib
Public Class FormMTM05
    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' 実行管理テーブルメンテナンスビジネスロジック
    ''' </summary>
    Private ReadOnly Biz05 As Biz.MTM05

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
        Me.Biz05 = New Biz.MTM05(connectionString)
    End Sub
    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM05_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim formConfirm As New FormMTMConfirm
        Dim result As DialogResult = formConfirm.ShowDialog()
        If result = DialogResult.OK Then
            Me.Biz05.Password = formConfirm.Password
            formConfirm.Dispose()
        Else
            MessageBox.Show("起動権限がありません")
            formConfirm.Dispose()
            Me.FormMenu.Show()
            Me.Close()
        End If

        Me.TextBox001.Text = ""
        Me.TextBox002.Text = ""
        Me.TextBox005.Text = ""
        Me.TextBox006.Text = ""
        Me.TextBox010.Text = ""
        Me.TextBox007.Text = ""
        Me.TextBox008.Text = ""
        Me.TextBox009.Text = ""
        Me.CheckBox001.Checked = False
        Me.CheckBox002.Checked = False
        Me.CheckBox003.Checked = False
        Me.CheckBox004.Checked = False
        Me.CheckBox005.Checked = False
        Me.CheckBox006.Checked = False
        Me.CheckBox007.Checked = False
        Me.DatePicker001.Format = DateTimePickerFormat.Custom
        Me.DatePicker001.CustomFormat = " "
        Me.DatePicker001.Checked = False
        Me.DatePicker002.Format = DateTimePickerFormat.Custom
        Me.DatePicker002.CustomFormat = " "
        Me.DatePicker002.Checked = False
        Me.DatePicker003.Format = DateTimePickerFormat.Custom
        Me.DatePicker003.CustomFormat = " "
        Me.DatePicker003.Checked = False
    End Sub
    ''' <summary>
    ''' 閉じるボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM05_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' 価格入力検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button001_Click(sender As Object, e As EventArgs) Handles Button001.Click
        Dim formSearch As New FormMTMSearchJitsukou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Dim dtPicker001 As Date
            Dim dtPicker002 As Date
            Dim dtPicker003 As Date
            Me.TextBox001.Text = formSearch.Selected.MTMR003001
            Me.TextBox002.Text = formSearch.Selected.MTMR003002
            Me.TextBox003.Text = formSearch.Selected.MTMR003003
            If (DateTime.TryParse(formSearch.Selected.MTMR003005, dtPicker001)) Then
                Me.DatePicker001.Format = DateTimePickerFormat.Long
                Me.DatePicker001.Value = dtPicker001.Year & "-" & dtPicker001.Month & "-" & dtPicker001.Day
            Else
                Me.DatePicker001.Format = DateTimePickerFormat.Custom
                Me.DatePicker001.CustomFormat = " "
                formSearch.Selected.MTMR003005 = ""
            End If
            Me.TextBox005.Text = formSearch.Selected.MTMR003005_2
            If (DateTime.TryParse(formSearch.Selected.MTMR003006, dtPicker002)) Then
                Me.DatePicker002.Format = DateTimePickerFormat.Long
                Me.DatePicker002.Value = dtPicker002.Year & "-" & dtPicker002.Month & "-" & dtPicker002.Day
            Else
                Me.DatePicker002.Format = DateTimePickerFormat.Custom
                Me.DatePicker002.CustomFormat = " "
                formSearch.Selected.MTMR003006 = ""
            End If
            Me.TextBox006.Text = formSearch.Selected.MTMR003006_2
            If (DateTime.TryParse(formSearch.Selected.MTMR003007, dtPicker003)) Then
                Me.DatePicker003.Format = DateTimePickerFormat.Long
                Me.DatePicker003.Value = dtPicker003.Year & "-" & dtPicker003.Month & "-" & dtPicker003.Day
            Else
                Me.DatePicker003.Format = DateTimePickerFormat.Custom
                Me.DatePicker003.CustomFormat = " "
                formSearch.Selected.MTMR003007 = ""
            End If
            Me.TextBox010.Text = formSearch.Selected.MTMR003021
            Me.TextBox007.Text = formSearch.Selected.MTMR003008
            Me.TextBox008.Text = formSearch.Selected.MTMR003009
            Me.TextBox009.Text = formSearch.Selected.MTMR003010
            Me.CheckBox001.Checked = False
            Me.CheckBox002.Checked = False
            Me.CheckBox003.Checked = False
            Me.CheckBox004.Checked = False
            Me.CheckBox005.Checked = False
            Me.CheckBox006.Checked = False
            Me.CheckBox007.Checked = False
            If (Not String.IsNullOrEmpty(formSearch.Selected.MTMR003011)) Then
                Me.CheckBox001.Checked = True
            End If
            If (Not String.IsNullOrEmpty(formSearch.Selected.MTMR003012)) Then
                Me.CheckBox002.Checked = True
            End If
            If (Not String.IsNullOrEmpty(formSearch.Selected.MTMR003013)) Then
                Me.CheckBox003.Checked = True
            End If
            If (Not String.IsNullOrEmpty(formSearch.Selected.MTMR003014)) Then
                Me.CheckBox004.Checked = True
            End If
            If (Not String.IsNullOrEmpty(formSearch.Selected.MTMR003015)) Then
                Me.CheckBox005.Checked = True
            End If
            If (Not String.IsNullOrEmpty(formSearch.Selected.MTMR003016)) Then
                Me.CheckBox006.Checked = True
            End If
            If (Not String.IsNullOrEmpty(formSearch.Selected.MTMR003017)) Then
                Me.CheckBox007.Checked = True
            End If
        End If
        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' ファイルオープンボタンクリック処理（案内文の指定）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button002_Click(sender As Object, e As EventArgs) Handles Button002.Click
        Dim ofDialog As New OpenFileDialog
        ofDialog.Title = "案内文のファイルを選択してください"
        ofDialog.RestoreDirectory = True

        If ofDialog.ShowDialog = DialogResult.OK Then
            Me.TextBox008.Text = ofDialog.FileName
        End If
    End Sub
    ''' <summary>
    ''' ファイルオープンボタンクリック処理（入力元の指定）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button003_Click(sender As Object, e As EventArgs) Handles Button003.Click
        Dim ofDialog As New OpenFileDialog
        ofDialog.Filter = "テキストファイル(*.txt)|*.txt"
        ofDialog.Title = "入力元のファイルを選択してください"
        ofDialog.RestoreDirectory = True

        If ofDialog.ShowDialog = DialogResult.OK Then
            Me.TextBox009.Text = ofDialog.FileName
        End If
    End Sub
    ''' <summary>
    ''' データピッカーイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker001_KeyDown(sender As Object, e As KeyEventArgs) Handles DatePicker001.KeyDown
        If e.KeyValue = Keys.Delete Then
            Me.DatePicker001.Format = DateTimePickerFormat.Custom
            Me.DatePicker001.CustomFormat = " "
            Me.DatePicker001.Checked = False
        End If
    End Sub
    ''' <summary>
    ''' 日付変更処理（一斉送信日時）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker001_ValueChanged(sender As Object, e As EventArgs) Handles DatePicker001.ValueChanged
        If IsNothing(Me.DatePicker001.Value) Then
            Me.DatePicker001.Format = DateTimePickerFormat.Custom
            Me.DatePicker001.CustomFormat = " "
            Me.DatePicker001.Checked = False
        Else
            Me.DatePicker001.Format = DateTimePickerFormat.Long
            Me.DatePicker001.Checked = True
        End If
    End Sub
    ''' <summary>
    ''' データピッカーイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker002_KeyDown(sender As Object, e As KeyEventArgs) Handles DatePicker002.KeyDown
        If e.KeyValue = Keys.Delete Then
            Me.DatePicker002.Format = DateTimePickerFormat.Custom
            Me.DatePicker002.CustomFormat = " "
            Me.DatePicker002.Checked = False
        End If
    End Sub
    ''' <summary>
    ''' 日付変更処理（更新日時）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker002_ValueChanged(sender As Object, e As EventArgs) Handles DatePicker002.ValueChanged
        If IsNothing(Me.DatePicker002.Value) Then
            Me.DatePicker002.Format = DateTimePickerFormat.Custom
            Me.DatePicker002.CustomFormat = " "
            Me.DatePicker002.Checked = False
        Else
            Me.DatePicker002.Format = DateTimePickerFormat.Long
            Me.DatePicker002.Checked = True
        End If
    End Sub
    ''' <summary>
    ''' データピッカーイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker003_KeyDown(sender As Object, e As KeyEventArgs) Handles DatePicker003.KeyDown
        If e.KeyValue = Keys.Delete Then
            Me.DatePicker003.Format = DateTimePickerFormat.Custom
            Me.DatePicker003.CustomFormat = " "
            Me.DatePicker003.Checked = False
        End If
    End Sub
    ''' <summary>
    ''' 日付変更処理（締切日）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DatePicker003_ValueChanged(sender As Object, e As EventArgs) Handles DatePicker003.ValueChanged
        If IsNothing(Me.DatePicker003.Value) Then
            Me.DatePicker003.Format = DateTimePickerFormat.Custom
            Me.DatePicker003.CustomFormat = " "
            Me.DatePicker003.Checked = False
        Else
            Me.DatePicker003.Format = DateTimePickerFormat.Long
            Me.DatePicker003.Checked = True
        End If
    End Sub
    ''' <summary>
    ''' 実行ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonExecute_Click(sender As Object, e As EventArgs) Handles ButtonExecute.Click

        Dim txt001 As String = Me.TextBox001.Text.Trim
        Dim txt002 As String = Me.TextBox002.Text.Trim
        Dim txt005 As String = Me.TextBox005.Text.Trim
        Dim txt006 As String = Me.TextBox006.Text.Trim
        Dim txt010 As String = Me.TextBox010.Text.Trim
        Dim txt007 As String = Me.TextBox007.Text.Trim
        Dim txt008 As String = Me.TextBox008.Text.Trim
        Dim txt009 As String = Me.TextBox009.Text.Trim

        Dim dt001 As Date = Me.DatePicker001.Value
        Dim bl001 As Boolean = Me.DatePicker001.Checked
        Dim dt002 As Date = Me.DatePicker002.Value
        Dim bl002 As Boolean = Me.DatePicker002.Checked
        Dim dt003 As Date = Me.DatePicker003.Value
        Dim bl003 As Boolean = Me.DatePicker003.Checked

        Dim chk001 As Boolean = Me.CheckBox001.Checked
        Dim chk002 As Boolean = Me.CheckBox002.Checked
        Dim chk003 As Boolean = Me.CheckBox003.Checked
        Dim chk004 As Boolean = Me.CheckBox004.Checked
        Dim chk005 As Boolean = Me.CheckBox005.Checked
        Dim chk006 As Boolean = Me.CheckBox006.Checked
        Dim chk007 As Boolean = Me.CheckBox007.Checked

        Dim sysError As String = ""

        Dim result As DialogResult = MessageBox.Show("実行しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        If result = DialogResult.No Then Exit Sub

        Try
            Dim mtm10r003jitsukou = New MitsumoLib.Models.MTM10R003JITSUKOU

            If (Not String.IsNullOrEmpty(txt001)) Then
                If (Not Integer.TryParse(txt001, mtm10r003jitsukou.MTMR003001)) Then
                    MessageBox.Show("価格入力番号が入力されていません")
                    Exit Sub
                End If
            Else
                MessageBox.Show("価格入力番号が入力されていません")
                Exit Sub
            End If

            mtm10r003jitsukou.MTMR003002 = txt002
            mtm10r003jitsukou.MTMR003003 = Me.FormMenu.ModelMtmUser.MTMM002001

            If (bl001) Then
                If Not IsNothing(dt001) Then
                    mtm10r003jitsukou.MTMR003005 = dt001.ToString("yyyyMMdd")
                End If
            Else
                mtm10r003jitsukou.MTMR003005 = ""
            End If
            mtm10r003jitsukou.MTMR003005_2 = txt005

            If (bl002) Then
                If Not IsNothing(dt002) Then
                    mtm10r003jitsukou.MTMR003006 = dt002.ToString("yyyyMMdd")
                End If
            Else
                mtm10r003jitsukou.MTMR003006 = ""
            End If
            mtm10r003jitsukou.MTMR003006_2 = txt006

            If (bl003) Then
                If Not IsNothing(dt003) Then
                    mtm10r003jitsukou.MTMR003007 = dt003.ToString("yyyyMMdd")
                End If
            Else
                mtm10r003jitsukou.MTMR003007 = ""
            End If
            mtm10r003jitsukou.MTMR003021 = txt010
            mtm10r003jitsukou.MTMR003008 = txt007
            mtm10r003jitsukou.MTMR003009 = txt008
            mtm10r003jitsukou.MTMR003010 = txt009
            mtm10r003jitsukou.MTMR003011 = "0"
            If chk001 Then
                mtm10r003jitsukou.MTMR003011 = "1"
            End If
            mtm10r003jitsukou.MTMR003012 = "0"
            If chk002 Then
                mtm10r003jitsukou.MTMR003012 = "1"
            End If
            mtm10r003jitsukou.MTMR003013 = "0"
            If chk003 Then
                mtm10r003jitsukou.MTMR003013 = "1"
            End If
            mtm10r003jitsukou.MTMR003014 = "0"
            If chk004 Then
                mtm10r003jitsukou.MTMR003014 = "1"
            End If
            mtm10r003jitsukou.MTMR003015 = "0"
            If chk005 Then
                mtm10r003jitsukou.MTMR003015 = "1"
            End If
            mtm10r003jitsukou.MTMR003016 = "0"
            If chk006 Then
                mtm10r003jitsukou.MTMR003016 = "1"
            End If
            mtm10r003jitsukou.MTMR003017 = "0"
            If chk007 Then
                mtm10r003jitsukou.MTMR003017 = "1"
            End If

            '価格入力番号のチェック
            Dim validateErrorList = Me.Biz05.ValidateCheck(mtm10r003jitsukou)
            If validateErrorList.Count > 0 Then
                For Each errorMessage As String In validateErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If

            '価格入力番号の存在チェック
            Dim checkErrorList = Me.Biz05.DataCheckSutanka(mtm10r003jitsukou.MTMR003001)
            If (checkErrorList = False) Then
                MessageBox.Show("価格入力番号が実行管理テーブルに存在しません")
                Exit Sub
            End If

            '実行管理テーブルの更新
            Dim updateErrorList = Me.Biz05.DataUpdateJitsuko(mtm10r003jitsukou, sysError)
            If updateErrorList.Count > 0 Then
                For Each errorMessage As String In updateErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If

            '連携管理テーブルの更新
            Dim update2ErrorList = Me.Biz05.DataUpdateRenkei(mtm10r003jitsukou, sysError)
            If update2ErrorList.Count > 0 Then
                For Each errorMessage As String In update2ErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If

            MessageBox.Show("実行管理テーブルへの更新が完了しました")

        Catch ex As Exception
            If (Not String.IsNullOrEmpty(sysError)) Then
                MessageBox.Show("システムエラーが発生しました" & vbCrLf & sysError)
            Else
                MessageBox.Show("システムエラーが発生しました")
            End If
            Exit Sub
        End Try
    End Sub
    ''' <summary>
    ''' 価格入力番号の変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox001_Validated(sender As Object, e As EventArgs) Handles TextBox001.Validated
        Dim mtm10r003jitsukou = New MitsumoLib.Models.MTM10R003JITSUKOU
        Dim txt001 As String = Me.TextBox001.Text.Trim
        Dim tableMTM10R003JITSUKOU As New DataTable
        Dim dbl As Double

        '価格入力番号の入力チェック
        If (String.IsNullOrEmpty(txt001)) Then
            Exit Sub
        End If
        If Not Double.TryParse(txt001, dbl) Then
            MessageBox.Show("価格入力番号が数値ではありません")
            Exit Sub
        End If

        Dim dtPicker001 As Date
        Dim dtPicker002 As Date
        Dim dtPicker003 As Date
        tableMTM10R003JITSUKOU = Me.Biz05.GetJitsukou(txt001)
        Dim rowsMTM10R003JITSUKOU As DataRow()
        rowsMTM10R003JITSUKOU = tableMTM10R003JITSUKOU.Select()
        If (tableMTM10R003JITSUKOU.Rows.Count > 0) Then
            For Each dataRow As DataRow In rowsMTM10R003JITSUKOU
                Me.TextBox002.Text = dataRow("MTMR003002")
                Me.TextBox003.Text = dataRow("MTMR003003")
                If (DateTime.TryParse(dataRow("MTMR003005"), dtPicker001)) Then
                    Me.DatePicker001.Format = DateTimePickerFormat.Long
                    Me.DatePicker001.Value = dtPicker001.Year & "-" & dtPicker001.Month & "-" & dtPicker001.Day
                Else
                    Me.DatePicker001.Format = DateTimePickerFormat.Custom
                    Me.DatePicker001.CustomFormat = " "
                End If
                Me.TextBox005.Text = dataRow("MTMR003005_2")
                If (DateTime.TryParse(dataRow("MTMR003006"), dtPicker002)) Then
                    Me.DatePicker002.Format = DateTimePickerFormat.Long
                    Me.DatePicker002.Value = dtPicker002.Year & "-" & dtPicker002.Month & "-" & dtPicker002.Day
                Else
                    Me.DatePicker002.Format = DateTimePickerFormat.Custom
                    Me.DatePicker002.CustomFormat = " "
                End If
                Me.TextBox006.Text = dataRow("MTMR003006_2")
                If (DateTime.TryParse(dataRow("MTMR003007"), dtPicker003)) Then
                    Me.DatePicker003.Format = DateTimePickerFormat.Long
                    Me.DatePicker003.Value = dtPicker003.Year & "-" & dtPicker003.Month & "-" & dtPicker003.Day
                Else
                    Me.DatePicker003.Format = DateTimePickerFormat.Custom
                    Me.DatePicker003.CustomFormat = " "
                End If
                Me.TextBox010.Text = dataRow("MTMR003021")
                Me.TextBox007.Text = dataRow("MTMR003008")
                Me.TextBox008.Text = dataRow("MTMR003009")
                Me.TextBox009.Text = dataRow("MTMR003010")
                If (Not String.IsNullOrEmpty(dataRow("MTMR003011"))) Then
                    Me.CheckBox001.Checked = True
                End If
                If (Not String.IsNullOrEmpty(dataRow("MTMR003012"))) Then
                    Me.CheckBox002.Checked = True
                End If
                If (Not String.IsNullOrEmpty(dataRow("MTMR003013"))) Then
                    Me.CheckBox003.Checked = True
                End If
                If (Not String.IsNullOrEmpty(dataRow("MTMR003014"))) Then
                    Me.CheckBox004.Checked = True
                End If
                If (Not String.IsNullOrEmpty(dataRow("MTMR003015"))) Then
                    Me.CheckBox005.Checked = True
                End If
                If (Not String.IsNullOrEmpty(dataRow("MTMR003016"))) Then
                    Me.CheckBox006.Checked = True
                End If
                If (Not String.IsNullOrEmpty(dataRow("MTMR003017"))) Then
                    Me.CheckBox007.Checked = True
                End If
            Next
        Else
            Me.TextBox002.Text = ""
            Me.DatePicker001.Format = DateTimePickerFormat.Custom
            Me.DatePicker001.CustomFormat = " "
            Me.DatePicker002.Format = DateTimePickerFormat.Custom
            Me.DatePicker002.CustomFormat = " "
            Me.DatePicker003.Format = DateTimePickerFormat.Custom
            Me.DatePicker003.CustomFormat = " "
            Me.TextBox005.Text = ""
            Me.TextBox006.Text = ""
            Me.TextBox010.Text = ""
            Me.TextBox007.Text = ""
            Me.TextBox008.Text = ""
            Me.TextBox009.Text = ""
            Me.CheckBox001.Checked = False
            Me.CheckBox002.Checked = False
            Me.CheckBox003.Checked = False
            Me.CheckBox004.Checked = False
            Me.CheckBox005.Checked = False
            Me.CheckBox006.Checked = False
            Me.CheckBox007.Checked = False
        End If

    End Sub
    ''' <summary>
    ''' 案内文プレビューボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonPreview_Click(sender As Object, e As EventArgs) Handles ButtonPreview.Click
        Dim errorList As New List(Of String)
        Dim outputFilePath As String

        If (String.IsNullOrEmpty(Me.TextBox008.Text)) Then
            MessageBox.Show("案内文が設定されていません")
            Exit Sub
        End If

        outputFilePath = ""

        Dim executionAsm As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
        Dim executingPath As String = IO.Path.GetDirectoryName(New Uri(executionAsm.CodeBase).LocalPath)
        Dim pdfDir = ConfigurationManager.AppSettings("PDF_DIR")

        errorList = Me.Biz05.OutputPreviewAnnaiPdf(Me.TextBox008.Text, executingPath + "\" + pdfDir)
        If errorList.Count > 0 Then
            For Each errorMessage As String In errorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        Else
            outputFilePath = executingPath + "\" + pdfDir + "/見積書.pdf"
        End If

        If (String.IsNullOrEmpty(outputFilePath)) Then
            MessageBox.Show("案内文を表示できません")
            Exit Sub
        End If

        Dim formPreview As New FormMTMPreview
        formPreview.WebView2001.Source = New Uri(outputFilePath)
        Dim result As DialogResult = formPreview.ShowDialog()

        formPreview.Dispose()
    End Sub
End Class