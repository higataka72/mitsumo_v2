Imports System.Configuration
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports MitsumoLib
Public Class FormMTM01
    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' 単価入力シート取込ビジネスロジック
    ''' </summary>
    Private ReadOnly Biz01 As Biz.MTM01
    Private ReadOnly BizCom As Biz.ComTaskScheduler

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
        Me.Biz01 = New Biz.MTM01(connectionString)
        Me.BizCom = New Biz.ComTaskScheduler
    End Sub

    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM01_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.FormMenu.Show()
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM01_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.AutoScroll = True
        'AutoScrollMarginを指定するとマージンのサイズを設定できる。
        Me.AutoScrollMargin = New Size(10, 10)
        'AutoScrollMinSizeを指定すると
        'スクロールバーを表示する最小サイズを設定できる。
        Me.AutoScrollMinSize = New Size(100, 100)
        'AutoScrollPositionを指定すると位置を設定できる。
        Me.AutoScrollPosition = New Point(-50, 50)
        Dim formConfirm As New FormMTMConfirm

        Dim result As DialogResult = formConfirm.ShowDialog()

        If result = DialogResult.OK Then
            Me.Biz01.Password = formConfirm.Password
            formConfirm.Dispose()
        Else
            MessageBox.Show("起動権限がありません")
            formConfirm.Dispose()
            Me.FormMenu.Show()
            Me.Close()
        End If

        Me.RadioButton001.Checked = True
        Me.Button001.Visible = False

        Me.TextBox001.Text = Me.Biz01.GetCostInputNumber()
        'Me.TextBox002.Text = ""
        Me.TextBox003.Text = Me.FormMenu.ModelMtmUser.MTMM002001.Trim
        Me.TextBox004.Text = Me.FormMenu.ModelMtmUser.MTMM002002
        Me.TextBox005.Text = ""
        Me.TextBox006.Text = ""
        Me.TextBox010.Text = "受注分より"
        Me.TextBox007.Text = "従来通り"
        Me.TextBox008.Text = ""
        '取込ファイル名を取得してセット
        Me.TextBox009.Text = Me.Biz01.ImportFileNameSearch()
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
    ''' ファイルオープンボタンクリック処理（案内文の指定）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonFile001_Click(sender As Object, e As EventArgs) Handles ButtonFile001.Click
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
    Private Sub ButtonFile002_Click(sender As Object, e As EventArgs) Handles ButtonFile002.Click
        Dim ofDialog As New OpenFileDialog
        ofDialog.Filter = "テキストファイル(*.txt)|*.txt"
        ofDialog.Title = "入力元のファイルを選択してください"
        ofDialog.RestoreDirectory = True

        If ofDialog.ShowDialog = DialogResult.OK Then
            Me.TextBox009.Text = ofDialog.FileName
        End If
    End Sub
    ''' <summary>
    ''' チェック変更処理（一括取込 or 一括取込）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton002.CheckedChanged, RadioButton001.CheckedChanged
        If (Me.RadioButton001.Checked) Then
            Me.Button001.Visible = False
            If (Me.Biz01 Is Nothing) Then Exit Sub
            Me.TextBox001.Text = Me.Biz01.GetCostInputNumber()
        ElseIf (Me.RadioButton002.Checked) Then
            Me.Button001.Visible = True
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
        Else
            Me.DatePicker001.Format = DateTimePickerFormat.Long
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
        Else
            Me.DatePicker002.Format = DateTimePickerFormat.Long
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
        Else
            Me.DatePicker003.Format = DateTimePickerFormat.Long
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
    ''' 数値制限イベントイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox005_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox005.KeyPress
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso
       e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub
    ''' <summary>
    ''' 数値制限イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox006_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox006.KeyPress
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso
       e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' 実行ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    <Obsolete>
    Private Sub ButtonExecute_Click(sender As Object, e As EventArgs) Handles ButtonExecute.Click
        Dim msgLineImport As String = ""
        Dim msgLineBacupFIle As String = ""
        Dim msgLineTaskMitsumori As String = ""
        Dim msgLineTaskSystem As String = ""
        Dim txt001 As String = Me.TextBox001.Text.Trim
        Dim txt002 As String = Me.TextBox002.Text.Trim
        Dim txt003 As String = Me.TextBox003.Text.Trim
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

        Dim mtm10r003jitsukou = New MitsumoLib.Models.MTM10R003JITSUKOU
        Dim serversetting = New MitsumoLib.Models.SERVERSETTING
        mtm10r003jitsukou.MTMR003001 = Integer.Parse(txt001)

        If Me.RadioButton001.Checked Then
            ''''''''''''''''''
            ' 一括取込時
            ''''''''''''''''''
            'ログインIDを取込時はロック
            Me.TextBox003.Enabled = False
            mtm10r003jitsukou.MTMR003002 = txt002
            mtm10r003jitsukou.MTMR003003 = txt003
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

            Dim validateErrorList = Me.Biz01.ValidateBeforeImport(mtm10r003jitsukou)
            If validateErrorList.Count > 0 Then
                For Each errorMessage As String In validateErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If

            Try
                '取込ファイルの存在チェック
                If (Me.Biz01.FileCheck(mtm10r003jitsukou.MTMR003010) = False) Then
                    MessageBox.Show("入力元ファイルが存在しません")
                    Exit Sub
                End If

                Dim dialogProgress As New DialogMTMProgress
                dialogProgress.Show(Me)

                Dim errorList As New List(Of String)

                Dim table As DataTable = Models.MTM10R001TANKA.GetDataTable()
                Dim lineCount = 0
                Dim readCount = 0
                Dim progressNum As Decimal = 100

                Dim dtStart = DateTime.Now

                Using parser As New TextFieldParser(mtm10r003jitsukou.MTMR003010, Encoding.GetEncoding("Shift_JIS"))
                    parser.TextFieldType = FieldType.Delimited
                    parser.SetDelimiters(vbTab)
                    parser.TrimWhiteSpace = False

                    While Not parser.EndOfData
                        Dim arrayRow As String() = parser.ReadFields()
                        If lineCount > 1 Then
                            If arrayRow.Length >= 77 Then
                                Dim mtm10r001tanka As New Models.MTM10R001TANKA

                                '得意先コードが空白なら処理しない
                                If (String.IsNullOrEmpty(arrayRow(0))) Then
                                    Continue While
                                End If

                                '検索値 桁数チェック(11)
                                mtm10r001tanka.MTMR001001 = arrayRow(0)
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001001, 11)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 検索値 桁数エラー")
                                    Exit While
                                End If
                                '連番 桁数チェック(2)
                                mtm10r001tanka.MTMR001002 = arrayRow(1)
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001002, 2)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 検索値 桁数エラー")
                                    Exit While
                                End If
                                '得意先コード 桁数チェック(11)
                                mtm10r001tanka.MTMR001003 = arrayRow(2).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001003, 11)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 得意先コード 桁数エラー")
                                    Exit While
                                End If
                                '得意先名 桁数チェック(100)
                                mtm10r001tanka.MTMR001004 = arrayRow(3).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001004, 100)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 得意先名 桁数エラー")
                                    Exit While
                                End If
                                'ﾗﾝｸC 桁数チェック(8)
                                mtm10r001tanka.MTMR001005 = arrayRow(4).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001005, 8)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") ランクコード 桁数エラー")
                                    Exit While
                                End If
                                'ﾗﾝｸ 桁数チェック(32)
                                mtm10r001tanka.MTMR001006 = arrayRow(5).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001006, 32)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") ランク 桁数エラー")
                                    Exit While
                                End If
                                '営C 桁数チェック(8)
                                mtm10r001tanka.MTMR001007 = arrayRow(6).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001007, 8)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 営業所コード 桁数エラー")
                                    Exit While
                                End If
                                '営業所 桁数チェック(32)
                                mtm10r001tanka.MTMR001008 = arrayRow(7).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001008, 32)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 営業所 桁数エラー")
                                    Exit While
                                End If
                                '部課C 桁数チェック(8)
                                mtm10r001tanka.MTMR001009 = arrayRow(8).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001009, 8)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 部課コード 桁数エラー")
                                    Exit While
                                End If
                                '部課名 桁数チェック(32)
                                mtm10r001tanka.MTMR001010 = arrayRow(9).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001010, 32)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 部課名 桁数エラー")
                                    Exit While
                                End If
                                '担C 桁数チェック(8)
                                mtm10r001tanka.MTMR001011 = arrayRow(10).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001011, 8)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 担当者コード 桁数エラー")
                                    Exit While
                                End If
                                '担当者名 桁数チェック(32)
                                mtm10r001tanka.MTMR001012 = arrayRow(11).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001012, 32)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 担当者名 桁数エラー")
                                    Exit While
                                End If
                                '商品C 桁数チェック(100)
                                mtm10r001tanka.MTMR001013 = arrayRow(12).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001013, 25)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 商品コード 桁数エラー")
                                    Exit While
                                End If
                                '商品名 桁数チェック(80)
                                mtm10r001tanka.MTMR001014 = arrayRow(13).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001014, 80)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 商品名 桁数エラー")
                                    Exit While
                                End If
                                '規格 桁数チェック(36)
                                mtm10r001tanka.MTMR001015 = arrayRow(14).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001015, 36)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 規格 桁数エラー")
                                    Exit While
                                End If
                                '数量・上限 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001016 = arrayRow(15).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001016.Length = 1) Then
                                    mtm10r001tanka.MTMR001016 = mtm10r001tanka.MTMR001016.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001016)) Then mtm10r001tanka.MTMR001016 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001016, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 数量・上限 数値・桁数エラー")
                                    Exit While
                                End If
                                'ロット 桁数チェック(12)
                                mtm10r001tanka.MTMR001017 = arrayRow(16).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001017, 12)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") ロット 桁数エラー")
                                    Exit While
                                End If
                                '行番号 数値チェック＆整数・少数桁数チェック(3.0) 
                                mtm10r001tanka.MTMR001018 = arrayRow(17).Replace(",", "").Replace("-", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001018)) Then mtm10r001tanka.MTMR001018 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001018, 3, 0)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 行番号 数値・桁数エラー")
                                    Exit While
                                End If
                                '入数 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001019 = arrayRow(18).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001019.Length = 1) Then
                                    mtm10r001tanka.MTMR001019 = mtm10r001tanka.MTMR001019.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001019)) Then mtm10r001tanka.MTMR001019 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001019, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 入数 数値・桁数エラー")
                                    Exit While
                                End If
                                '単位 桁数チェック(4)
                                mtm10r001tanka.MTMR001020 = arrayRow(19).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001020, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 単位 桁数エラー")
                                    Exit While
                                End If
                                '手配 桁数チェック(4)
                                mtm10r001tanka.MTMR001021 = arrayRow(20).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001021, 20)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 手配 桁数エラー")
                                    Exit While
                                End If
                                '改定前売単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001022 = arrayRow(21).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001022.Length = 1) Then
                                    mtm10r001tanka.MTMR001022 = mtm10r001tanka.MTMR001022.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001022)) Then mtm10r001tanka.MTMR001022 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001022, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 改定前売単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '改定前仕入単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001023 = arrayRow(22).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001023.Length = 1) Then
                                    mtm10r001tanka.MTMR001023 = mtm10r001tanka.MTMR001023.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001023)) Then mtm10r001tanka.MTMR001023 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001023, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 改定前仕入単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '改定前粗利率 数値チェック＆整数・少数桁数チェック(5.1) 
                                mtm10r001tanka.MTMR001024 = arrayRow(23).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001024.Length = 1) Then
                                    mtm10r001tanka.MTMR001024 = mtm10r001tanka.MTMR001024.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001024)) Then mtm10r001tanka.MTMR001024 = "0"
                                Dim decMTMR001024 As Decimal = 0.0
                                If Decimal.TryParse(mtm10r001tanka.MTMR001024, decMTMR001024) Then mtm10r001tanka.MTMR001024 = decMTMR001024.ToString("0.0")
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001024, 5, 1)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 改定前粗利率 数値・桁数エラー")
                                    Exit While
                                End If
                                '現売単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001025 = arrayRow(24).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001025.Length = 1) Then
                                    mtm10r001tanka.MTMR001025 = mtm10r001tanka.MTMR001025.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001025)) Then mtm10r001tanka.MTMR001025 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001025, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 現売単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '現売㎡単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001026 = arrayRow(25).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001026.Length = 1) Then
                                    mtm10r001tanka.MTMR001026 = mtm10r001tanka.MTMR001026.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001026)) Then mtm10r001tanka.MTMR001026 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001026, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 現売㎡単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '現仕入単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001027 = arrayRow(26).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001027.Length = 1) Then
                                    mtm10r001tanka.MTMR001027 = mtm10r001tanka.MTMR001027.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001027)) Then mtm10r001tanka.MTMR001027 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001027, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 現仕入単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '現仕㎡単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001028 = arrayRow(27).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001028.Length = 1) Then
                                    mtm10r001tanka.MTMR001028 = mtm10r001tanka.MTMR001028.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001028)) Then mtm10r001tanka.MTMR001028 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001028, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 現仕㎡単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '現粗利率 数値チェック＆整数・少数桁数チェック(5.1) 
                                mtm10r001tanka.MTMR001029 = arrayRow(28).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001029.Length = 1) Then
                                    mtm10r001tanka.MTMR001029 = mtm10r001tanka.MTMR001029.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001029)) Then mtm10r001tanka.MTMR001029 = "0"
                                Dim decMTMR001029 As Decimal = 0.0
                                If Decimal.TryParse(mtm10r001tanka.MTMR001029, decMTMR001029) Then mtm10r001tanka.MTMR001029 = decMTMR001029.ToString("0.0")
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001029, 5, 1)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 現粗利率 数値・桁数エラー")
                                    Exit While
                                End If
                                '新売単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001030 = arrayRow(29).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001030.Length = 1) Then
                                    mtm10r001tanka.MTMR001030 = mtm10r001tanka.MTMR001030.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001030)) Then mtm10r001tanka.MTMR001030 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001030, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 新売単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '新売㎡単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001031 = arrayRow(30).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001031.Length = 1) Then
                                    mtm10r001tanka.MTMR001031 = mtm10r001tanka.MTMR001031.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001031)) Then mtm10r001tanka.MTMR001031 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001031, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 新売㎡単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '売実施日 日付妥当性チェック
                                Dim dtColumn31 As DateTime
                                If DateTime.TryParse(arrayRow(31), dtColumn31) Then
                                    mtm10r001tanka.MTMR001032 = dtColumn31.ToString("yyyyMMdd")
                                Else
                                    If (String.IsNullOrEmpty(arrayRow(31))) Then
                                        mtm10r001tanka.MTMR001032 = "0"
                                    Else
                                        errorList.Add("行数(" + lineCount.ToString() + ") 売実施日 日付妥当性エラー")
                                        Exit While
                                    End If

                                End If
                                '新仕入単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001033 = arrayRow(32).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001033.Length = 1) Then
                                    mtm10r001tanka.MTMR001033 = mtm10r001tanka.MTMR001033.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001033)) Then mtm10r001tanka.MTMR001033 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001033, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 新仕入単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '新仕㎡単価 数値チェック＆整数・少数桁数チェック(19.4) 
                                mtm10r001tanka.MTMR001034 = arrayRow(33).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001034.Length = 1) Then
                                    mtm10r001tanka.MTMR001034 = mtm10r001tanka.MTMR001034.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001034)) Then mtm10r001tanka.MTMR001034 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001034, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 新仕㎡単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '仕実施日　日付妥当性チェック
                                Dim dtColumn34 As DateTime
                                If DateTime.TryParse(arrayRow(34), dtColumn34) Then
                                    mtm10r001tanka.MTMR001035 = dtColumn34.ToString("yyyyMMdd")
                                Else
                                    If (String.IsNullOrEmpty(arrayRow(34))) Then
                                        mtm10r001tanka.MTMR001035 = "0"
                                    Else
                                        errorList.Add("行数(" + lineCount.ToString() + ") 仕実施日 日付妥当性エラー")
                                        Exit While
                                    End If
                                End If
                                '新粗利率 数値チェック＆整数・少数桁数チェック(5.1)
                                mtm10r001tanka.MTMR001036 = arrayRow(35).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001036.Length = 1) Then
                                    mtm10r001tanka.MTMR001036 = mtm10r001tanka.MTMR001036.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001036)) Then mtm10r001tanka.MTMR001036 = "0"
                                Dim decMTMR001036 As Decimal = 0.0
                                If Decimal.TryParse(mtm10r001tanka.MTMR001036, decMTMR001036) Then mtm10r001tanka.MTMR001036 = decMTMR001036.ToString("0.0")
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001036, 5, 1)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 新粗利率 数値・桁数エラー")
                                    Exit While
                                End If
                                '値上率 数値チェック＆整数・少数桁数チェック(5.1)
                                mtm10r001tanka.MTMR001037 = arrayRow(36).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001037.Length = 1) Then
                                    mtm10r001tanka.MTMR001037 = mtm10r001tanka.MTMR001037.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001037)) Then mtm10r001tanka.MTMR001037 = "0"
                                Dim decMTMR001037 As Decimal = 0.0
                                If Decimal.TryParse(mtm10r001tanka.MTMR001037, decMTMR001037) Then mtm10r001tanka.MTMR001037 = decMTMR001037.ToString("0.0")
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001037, 5, 1)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 値上率 数値・桁数エラー")
                                    Exit While
                                End If
                                '仕入先コメント 桁数チェック(50)
                                mtm10r001tanka.MTMR001038 = arrayRow(37).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001038, 50)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 仕入先コメント 桁数エラー")
                                    Exit While
                                End If
                                '数量 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001039 = arrayRow(38).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001039.Length = 1) Then
                                    mtm10r001tanka.MTMR001039 = mtm10r001tanka.MTMR001039.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001039)) Then mtm10r001tanka.MTMR001039 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001039, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 数量 数値・桁数エラー")
                                    Exit While
                                End If
                                '売上回数 数値チェック＆整数・少数桁数チェック(3.0)
                                mtm10r001tanka.MTMR001040 = arrayRow(39).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001040.Length = 1) Then
                                    mtm10r001tanka.MTMR001040 = mtm10r001tanka.MTMR001040.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001040)) Then mtm10r001tanka.MTMR001040 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001040, 5, 0)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 売上回数 数値・桁数エラー")
                                    Exit While
                                End If
                                '売上金額 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001041 = arrayRow(40).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001041.Length = 1) Then
                                    mtm10r001tanka.MTMR001041 = mtm10r001tanka.MTMR001041.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001041)) Then mtm10r001tanka.MTMR001041 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001041, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 売上金額 数値・桁数エラー")
                                    Exit While
                                End If
                                '粗利金額 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001042 = arrayRow(41).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001042.Length = 1) Then
                                    mtm10r001tanka.MTMR001042 = mtm10r001tanka.MTMR001042.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001042)) Then mtm10r001tanka.MTMR001042 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001042, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 粗利金額 数値・桁数エラー")
                                    Exit While
                                End If
                                '改定後売上金額 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001043 = arrayRow(42).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001043.Length = 1) Then
                                    mtm10r001tanka.MTMR001043 = mtm10r001tanka.MTMR001043.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001043)) Then mtm10r001tanka.MTMR001043 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001043, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 改定後売上金額 数値・桁数エラー")
                                    Exit While
                                End If
                                '改定後粗利金額 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001044 = arrayRow(43).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001044.Length = 1) Then
                                    mtm10r001tanka.MTMR001044 = mtm10r001tanka.MTMR001044.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001044)) Then mtm10r001tanka.MTMR001044 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001044, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 改定後粗利金額 数値・桁数エラー")
                                    Exit While
                                End If
                                '社内適用 桁数チェック(50)
                                mtm10r001tanka.MTMR001045 = arrayRow(44).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001045, 50)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 社内適用 桁数エラー")
                                    Exit While
                                End If
                                '発注適用 桁数チェック(50)
                                mtm10r001tanka.MTMR001046 = arrayRow(45).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001046, 50)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 発注適用 桁数エラー")
                                    Exit While
                                End If
                                '運賃適用 桁数チェック(70)
                                mtm10r001tanka.MTMR001047 = arrayRow(46).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001047, 70)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 運賃適用 桁数エラー")
                                    Exit While
                                End If
                                '見積書商品名 桁数チェック(100)
                                mtm10r001tanka.MTMR001048 = arrayRow(47).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001048, 100)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 見積書商品名 桁数エラー")
                                    Exit While
                                End If
                                '最終売上日 日付妥当性チェック
                                Dim dtColumn48 As DateTime
                                If DateTime.TryParse(arrayRow(48), dtColumn48) Then
                                    mtm10r001tanka.MTMR001049 = dtColumn48.ToString("yyyyMMdd")
                                Else
                                    If (String.IsNullOrEmpty(arrayRow(48))) Then
                                        mtm10r001tanka.MTMR001049 = "0"
                                    Else
                                        errorList.Add("行数(" + lineCount.ToString() + ") 最終売上日 日付妥当性エラー")
                                        Exit While
                                    End If
                                End If
                                '最終売上単価 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001050 = arrayRow(49).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001050.Length = 1) Then
                                    mtm10r001tanka.MTMR001050 = mtm10r001tanka.MTMR001050.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001050)) Then mtm10r001tanka.MTMR001050 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001050, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 最終売上単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '最終売上数量　数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001051 = arrayRow(50).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001051.Length = 1) Then
                                    mtm10r001tanka.MTMR001051 = mtm10r001tanka.MTMR001051.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001051)) Then mtm10r001tanka.MTMR001051 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001051, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 最終売上数量 数値・桁数エラー")
                                    Exit While
                                End If
                                '最終納品先　桁数チェック(70)
                                mtm10r001tanka.MTMR001052 = arrayRow(51).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001052, 70)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 最終納品先 桁数エラー")
                                    Exit While
                                End If
                                '納品先履歴 半角変換、101バイトチェック（100バイトにカットし、100バイト + SPACE + "他")
                                mtm10r001tanka.MTMR001053 = Me.Biz01.DeliveryConvert(arrayRow(52).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", ""))
                                'mtm10r001tanka.MTMR001053 = arrayRow(52).Replace("#DIV/0!", "").Replace("#REF!", "")

                                '得意先FAX 桁数チェック(15)
                                mtm10r001tanka.MTMR001054 = arrayRow(53).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001054, 15)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 得意先FAX 桁数エラー")
                                    Exit While
                                End If
                                '仕入先C 桁数チェック(8)
                                mtm10r001tanka.MTMR001055 = arrayRow(54).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001055, 8)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 仕入先コード 桁数エラー")
                                    Exit While
                                End If
                                '仕入先名 桁数チェック(32)
                                mtm10r001tanka.MTMR001056 = arrayRow(55).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001056, 100)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 仕入先名 桁数エラー")
                                    Exit While
                                End If
                                '売価変更 桁数チェック(5)
                                mtm10r001tanka.MTMR001057 = arrayRow(56).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001057, 5)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 売価変更 桁数エラー")
                                    Exit While
                                End If
                                '仕価変更 桁数チェック(5)
                                mtm10r001tanka.MTMR001058 = arrayRow(57).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001058, 5)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 仕価変更 桁数エラー")
                                    Exit While
                                End If
                                '商品名(比較用) 桁数チェック(80)
                                mtm10r001tanka.MTMR001059 = arrayRow(58).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001059, 80)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 商品名(比較用) 桁数エラー")
                                    Exit While
                                End If
                                '商品名 桁数チェック(80)
                                mtm10r001tanka.MTMR001060 = arrayRow(59).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001060, 80)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 商品名 桁数エラー")
                                    Exit While
                                End If
                                'ｽﾘｯﾄ 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001061 = arrayRow(60).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001061.Length = 1) Then
                                    mtm10r001tanka.MTMR001061 = mtm10r001tanka.MTMR001061.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001061)) Then mtm10r001tanka.MTMR001061 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001061, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") ｽﾘｯﾄ 数値・桁数エラー")
                                    Exit While
                                End If
                                '# 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001062 = arrayRow(61).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001062.Length = 1) Then
                                    mtm10r001tanka.MTMR001062 = mtm10r001tanka.MTMR001062.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001062)) Then mtm10r001tanka.MTMR001062 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001062, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") # 数値・数値・桁数エラー")
                                    Exit While
                                End If
                                '厚み 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001063 = arrayRow(62).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001063.Length = 1) Then
                                    mtm10r001tanka.MTMR001063 = mtm10r001tanka.MTMR001063.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001063)) Then mtm10r001tanka.MTMR001063 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001063, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 厚み 数値・桁数エラー")
                                    Exit While
                                End If
                                '（空白１）
                                mtm10r001tanka.MTMR001064 = arrayRow(63)
                                '巾1 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001065 = arrayRow(64).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001065.Length = 1) Then
                                    mtm10r001tanka.MTMR001065 = mtm10r001tanka.MTMR001065.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001065)) Then mtm10r001tanka.MTMR001065 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001065, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 巾1 数値・桁数エラー")
                                    Exit While
                                End If
                                '（空白２）
                                mtm10r001tanka.MTMR001066 = arrayRow(65)
                                '巾2 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001067 = arrayRow(66).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001067.Length = 1) Then
                                    mtm10r001tanka.MTMR001067 = mtm10r001tanka.MTMR001067.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001067)) Then mtm10r001tanka.MTMR001067 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001067, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 巾2 数値・桁数エラー")
                                    Exit While
                                End If
                                '（空白３）
                                mtm10r001tanka.MTMR001068 = arrayRow(67)
                                '長さ 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001069 = arrayRow(68).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001069.Length = 1) Then
                                    mtm10r001tanka.MTMR001069 = mtm10r001tanka.MTMR001069.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001069)) Then mtm10r001tanka.MTMR001069 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001069, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 長さ 数値・桁数エラー")
                                    Exit While
                                End If
                                'M 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001070 = arrayRow(69).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001070.Length = 1) Then
                                    mtm10r001tanka.MTMR001070 = mtm10r001tanka.MTMR001070.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001070)) Then mtm10r001tanka.MTMR001070 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001070, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") M 数値・桁数エラー")
                                    Exit While
                                End If
                                '+表示 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001071 = arrayRow(70).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001071.Length = 1) Then
                                    mtm10r001tanka.MTMR001071 = mtm10r001tanka.MTMR001071.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001071)) Then mtm10r001tanka.MTMR001071 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001071, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") +表示 数値・桁数エラー")
                                    Exit While
                                End If
                                'ｽﾘｯﾄ 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001072 = arrayRow(71).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001072.Length = 1) Then
                                    mtm10r001tanka.MTMR001072 = mtm10r001tanka.MTMR001072.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001072)) Then mtm10r001tanka.MTMR001072 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001072, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") ｽﾘｯﾄ 数値・桁数エラー")
                                    Exit While
                                End If
                                '備考 桁数チェック(50)
                                mtm10r001tanka.MTMR001073 = arrayRow(72).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001073, 50)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 備考 桁数エラー")
                                    Exit While
                                End If
                                '㎡計算 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001074 = arrayRow(73).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001074.Length = 1) Then
                                    mtm10r001tanka.MTMR001074 = mtm10r001tanka.MTMR001074.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001074)) Then mtm10r001tanka.MTMR001074 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001074, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") ㎡計算 桁数エラー")
                                    Exit While
                                End If
                                '改定単価 数値チェック＆整数・少数桁数チェック(19.4)
                                mtm10r001tanka.MTMR001075 = arrayRow(74).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001075.Length = 1) Then
                                    mtm10r001tanka.MTMR001075 = mtm10r001tanka.MTMR001075.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001075)) Then mtm10r001tanka.MTMR001075 = "0"
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001075, 19, 4)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 改定単価 数値・桁数エラー")
                                    Exit While
                                End If
                                '値上がり率 数値チェック＆整数・少数桁数チェック(5.1)
                                mtm10r001tanka.MTMR001076 = arrayRow(75).Replace(",", "").Replace("%", "").Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#VALUE!", "").Replace(" ", "").Replace("　", "").Replace("#NAME?", "").Replace("#N/A", "")
                                If (mtm10r001tanka.MTMR001076.Length = 1) Then
                                    mtm10r001tanka.MTMR001076 = mtm10r001tanka.MTMR001076.Replace("-", "")
                                End If
                                If (String.IsNullOrEmpty(mtm10r001tanka.MTMR001076)) Then mtm10r001tanka.MTMR001076 = "0"
                                Dim decMTMR001076 As Decimal = 0.0
                                If Decimal.TryParse(mtm10r001tanka.MTMR001076, decMTMR001076) Then mtm10r001tanka.MTMR001076 = decMTMR001076.ToString("0.0")
                                If (Not Me.Biz01.NumericNumberDigitsCheck(mtm10r001tanka.MTMR001076, 5, 1)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 値上がり率 数値・桁数エラー")
                                    Exit While
                                End If
                                '備考 桁数チェック(50)
                                mtm10r001tanka.MTMR001077 = arrayRow(76).Replace("#DIV/0!", "").Replace("#REF!", "").Replace("#NAME?", "")
                                If (Not Me.Biz01.StringNumberDigitsCheck(mtm10r001tanka.MTMR001077, 50)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 備考 桁数エラー")
                                    Exit While
                                End If

                                '重複チェック
                                If (Me.Biz01.KakakuDataCheck(mtm10r001tanka, mtm10r003jitsukou.MTMR003001)) Then
                                    errorList.Add("行数(" + lineCount.ToString() + ") 既に存在するデータがあります")
                                    Exit While
                                End If

                                'Dim validateError As List(Of String) = mtm10r001tanka.Validate
                                'If validateError.Count > 0 Then
                                '    errorList.AddRange(validateError)
                                'End If

                                mtm10r001tanka.SetDataRow(table)

                                'メッセージを変更する
                                If (progressNum = readCount) Then
                                    dialogProgress.Message = "取込中　" + readCount.ToString + "件"
                                    progressNum = readCount + 100
                                End If

                                '1秒間待機する（本来なら何らかの処理を行う）
                                'System.Threading.Thread.Sleep(100)

                                'キャンセルされた時はループを抜ける
                                If dialogProgress.UserCanceled Then
                                    Exit While
                                End If

                                readCount += 1
                            Else
                                dialogProgress.Close()
                                Me.Activate()
                                MessageBox.Show("単価入力シートが77列以上ではありません" & vbCrLf & "行(" & lineCount & ")" & " 列：" & arrayRow.Length & vbCrLf)
                                Exit Sub
                            End If
                        End If

                        lineCount += 1
                    End While
                End Using

                dialogProgress.Close()
                Me.Activate()

                If errorList.Count > 0 Then
                    For Each errorMessage As String In errorList
                        MessageBox.Show(errorMessage)
                        Exit Sub
                    Next
                End If

                'ファイル取込処理(+制御マスタ更新）
                Dim sysError As String = ""
                Dim importErrorList = Me.Biz01.Import(mtm10r003jitsukou, table, sysError)
                If importErrorList.Count > 0 Then
                    For Each errorMessage As String In importErrorList
                        MessageBox.Show(errorMessage)
                        Exit Sub
                    Next
                Else
                    msgLineImport = "【成功】一括取込が完了"
                End If

                '取込ファイルをバックアップへ移動
                If (Me.Biz01.BackUpfileMove(mtm10r003jitsukou.MTMR003010) = False) Then
                    msgLineBacupFIle = "【失敗】取込ファイルのバックアップ失敗(手動バックアップして下さい)"
                Else
                    msgLineBacupFIle = "【成功】取込ファイルのバックアップ完了"
                End If

                'タスクスケジュール登録（見積一斉送信、システム間連携）
                If (bl001) Then

                    serversetting.ServerStartup = ConfigurationManager.AppSettings("TASK_SERVER_STARTUP")
                    serversetting.ServerIp = ConfigurationManager.AppSettings("TASK_SERVER_IP")
                    serversetting.ConnectionUser = ConfigurationManager.AppSettings("TASK_USER")
                    serversetting.ConnectionPassword = ConfigurationManager.AppSettings("TASK_PASSD")
                    serversetting.StartupUser = ConfigurationManager.AppSettings("TASK_STARTUP_USER")
                    serversetting.StartupPassword = ConfigurationManager.AppSettings("TASK_STARTUP_PASS")

                    If (String.IsNullOrEmpty(serversetting.ServerIp)) And
                       (String.IsNullOrEmpty(serversetting.ConnectionUser)) And
                       (String.IsNullOrEmpty(serversetting.ConnectionPassword)) And
                       (String.IsNullOrEmpty(serversetting.StartupUser)) And
                       (String.IsNullOrEmpty(serversetting.StartupPassword)) Then
                        msgLineTaskMitsumori = "【失敗】一斉送信日時のタスクスケジュール登録失敗(手動実行して下さい）"
                    Else
                        Dim taskMitsumoriErrorList = Me.BizCom.RegistTask(mtm10r003jitsukou, serversetting, 1)
                        If taskMitsumoriErrorList.Count > 0 Then
                            For Each errorMessage As String In taskMitsumoriErrorList
                                msgLineTaskMitsumori = "【失敗】一斉送信日時のタスクスケジュール登録失敗(手動実行して下さい）"
                            Next
                        Else
                            msgLineTaskMitsumori = "【成功】一斉送信日時のタスクスケジュール登録完了"
                        End If
                    End If
                    If (bl002) Then
                        Dim taskSysteErrorList = Me.BizCom.RegistTask(mtm10r003jitsukou, serversetting, 2)
                        If taskSysteErrorList.Count > 0 Then
                            For Each errorMessage As String In taskSysteErrorList
                                msgLineTaskSystem = "【失敗】更新データ日時のタスクスケジュール登録失敗(手動実行して下さい）"
                            Next
                        Else
                            msgLineTaskSystem = "【成功】更新データ日時のタスクスケジュール登録完了"
                        End If
                    End If
                End If

                '価格入力番号を採番した番号へ表示切替
                Me.TextBox001.Text = Me.Biz01.GetCostInputNumber()

            Catch ex As Exception
                MessageBox.Show("システムエラーが発生しました" & vbCrLf & ex.Message)
                Exit Sub
            Finally
                'ログインIDのロック解除
                Me.TextBox003.Enabled = True
            End Try

            Dim msgEndLine As String = "一括取込の処理が終了しました"
            If (Not String.IsNullOrEmpty(msgLineImport)) Then
                msgEndLine = msgEndLine & vbCrLf & msgLineImport
            End If
            If (Not String.IsNullOrEmpty(msgLineBacupFIle)) Then
                msgEndLine = msgEndLine & vbCrLf & msgLineBacupFIle
            End If
            If (Not String.IsNullOrEmpty(msgLineTaskMitsumori)) Then
                msgEndLine = msgEndLine & vbCrLf & msgLineTaskMitsumori
            End If
            If (Not String.IsNullOrEmpty(msgLineTaskSystem)) Then
                msgEndLine = msgEndLine & vbCrLf & msgLineTaskSystem
            End If

            MessageBox.Show(msgEndLine)

        Else
            ''''''''''''''''''
            ' 一括取消時時
            ''''''''''''''''''
            Dim result As DialogResult = MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            If result = DialogResult.No Then Exit Sub

            mtm10r003jitsukou.MTMR003002 = txt002
            Dim validateErrorList = Me.Biz01.ValidateBeforeDelete(mtm10r003jitsukou)
            If validateErrorList.Count > 0 Then
                For Each errorMessage As String In validateErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If
            Dim sysError As String = ""
            Try
                Dim importErrorList = Me.Biz01.Delete(mtm10r003jitsukou, sysError)
                If importErrorList.Count > 0 Then
                    For Each errorMessage As String In importErrorList
                        MessageBox.Show(errorMessage)
                        Exit Sub
                    Next
                End If
            Catch ex As Exception
                If (Not String.IsNullOrEmpty(sysError)) Then
                    MessageBox.Show("システムエラーが発生しました" & vbCrLf & sysError)
                Else
                    MessageBox.Show("システムエラーが発生しました" & vbCrLf & ex.Message)
                End If
                Exit Sub
            End Try

            MessageBox.Show("一括取消を実行しました")

        End If
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
    ''' ログインID検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonUser_Click(sender As Object, e As EventArgs) Handles ButtonUser.Click
        Dim formSearch As New FormMTMSearchUser
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox003.Text = formSearch.Selected.MTMM002001
            Me.TextBox004.Text = formSearch.Selected.MTMM002002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' テキスト変更処理（価格入力番号）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox001_Validated(sender As Object, e As EventArgs) Handles TextBox001.Validated
        If Me.RadioButton002.Checked Then
            Try
                Dim mtm10r003jitsukou = New MitsumoLib.Models.MTM10R003JITSUKOU
                Dim blData As Boolean = False
                If (Not String.IsNullOrEmpty(Me.TextBox001.Text.Trim)) Then
                    mtm10r003jitsukou.MTMR003001 = Integer.Parse(Me.TextBox001.Text.Trim)
                End If
                If (Not String.IsNullOrEmpty(Me.TextBox001.Text.Trim)) Then
                    mtm10r003jitsukou = Me.Biz01.KakakuDataCheck(Me.TextBox001.Text.Trim, blData)
                    If (blData = False) Then
                        MessageBox.Show("実行管理テーブルに価格入力番号が存在しません。(価格入力番号:" + Me.TextBox001.Text.Trim + ")")
                        Exit Sub
                    End If

                    '各項目をセット
                    Me.TextBox001.Text = mtm10r003jitsukou.MTMR003001                                  '価格入力番号
                    Me.TextBox002.Text = mtm10r003jitsukou.MTMR003002                                  '価格入力名称
                    Me.TextBox003.Text = mtm10r003jitsukou.MTMR003003                                  '取込担当者コード
                    If (String.IsNullOrEmpty(mtm10r003jitsukou.MTMR003003)) Then
                        Me.TextBox004.Text = Me.Biz01.UserNameSearch(mtm10r003jitsukou.MTMR003003)     '担当者名
                    End If
                    Dim txtDatePicker001 As Date                                                       '一斉送信時間(日付）
                    Dim txtTextBox005 As String
                    If (Me.Biz01.GetDate(mtm10r003jitsukou.MTMR003005, txtDatePicker001)) Then
                        Me.DatePicker001.Value = txtDatePicker001.Year & "-" & txtDatePicker001.Month & "-" & txtDatePicker001.Day
                        If (mtm10r003jitsukou.MTMR003005.Trim.Length >= 9) Then
                            txtTextBox005 = mtm10r003jitsukou.MTMR003005.Trim.Substring(8)
                            Me.TextBox005.Text = txtTextBox005
                        Else
                            Me.TextBox005.Text = ""
                        End If
                    Else
                        Me.DatePicker001.CustomFormat = " "
                        Me.TextBox005.Text = ""
                    End If
                    Dim txtDatePicker002 As Date                                                       '更新日付
                    Dim txtTextBox006 As String
                    If (Me.Biz01.GetDate(mtm10r003jitsukou.MTMR003006, txtDatePicker002)) Then
                        Me.DatePicker002.Value = txtDatePicker002.Year & "-" & txtDatePicker002.Month & "-" & txtDatePicker002.Day
                        If (mtm10r003jitsukou.MTMR003006.Trim.Length >= 9) Then
                            txtTextBox006 = mtm10r003jitsukou.MTMR003006.Trim.Substring(8)
                            Me.TextBox006.Text = txtTextBox006
                        Else
                            Me.TextBox006.Text = ""
                        End If
                    Else
                        Me.DatePicker002.CustomFormat = " "
                        Me.TextBox006.Text = ""
                    End If

                    Dim txtDatePicker003 As Date                                                       '締切日
                    If (Me.Biz01.GetDate(mtm10r003jitsukou.MTMR003007, txtDatePicker003)) Then
                        Me.DatePicker003.Value = txtDatePicker003.Year & "-" & txtDatePicker003.Month & "-" & txtDatePicker003.Day
                    Else
                        Me.DatePicker003.CustomFormat = " "
                    End If
                    Me.TextBox010.Text = mtm10r003jitsukou.MTMR003021                                  '改定実施日
                    Me.TextBox007.Text = mtm10r003jitsukou.MTMR003008                                  '運賃の指定
                    Me.TextBox008.Text = mtm10r003jitsukou.MTMR003009                                  '案内文
                    Me.TextBox009.Text = mtm10r003jitsukou.MTMR003010                                  '取込元ファイル名
                    If (mtm10r003jitsukou.MTMR003011 = "1") Then                                       '値下前売単価
                        Me.CheckBox001.Checked = True
                    Else
                        Me.CheckBox001.Checked = False
                    End If
                    If (mtm10r003jitsukou.MTMR003012 = "1") Then                                       '値下前仕入単価
                        Me.CheckBox002.Checked = True
                    Else
                        Me.CheckBox002.Checked = False
                    End If
                    If (mtm10r003jitsukou.MTMR003013 = "1") Then                                       '値下前粗利率
                        Me.CheckBox003.Checked = True
                    Else
                        Me.CheckBox003.Checked = False
                    End If
                    If (mtm10r003jitsukou.MTMR003014 = "1") Then                                       '現売㎡単価
                        Me.CheckBox004.Checked = True
                    Else
                        Me.CheckBox004.Checked = False
                    End If
                    If (mtm10r003jitsukou.MTMR003015 = "1") Then                                       '現仕㎡単価
                        Me.CheckBox005.Checked = True
                    Else
                        Me.CheckBox005.Checked = False
                    End If
                    If (mtm10r003jitsukou.MTMR003016 = "1") Then                                       '新売㎡単価
                        Me.CheckBox006.Checked = True
                    Else
                        Me.CheckBox006.Checked = False
                    End If
                    If (mtm10r003jitsukou.MTMR003017 = "1") Then                                       '新仕㎡単価
                        Me.CheckBox007.Checked = True
                    Else
                        Me.CheckBox007.Checked = False
                    End If
                Else
                    '価格入力番号が入っていないため初期化
                    MessageBox.Show("価格入力番号が入力されていません")
                End If
            Catch ex As Exception
                MessageBox.Show("システムエラーが発生しました")
            End Try

        End If
    End Sub
    ''' <summary>
    ''' 価格入力番号の虫眼鏡（一括取込時）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button001_Click(sender As Object, e As EventArgs) Handles Button001.Click
        Dim formSearch As New FormMTMSearchJitsukou
        Dim result As DialogResult = formSearch.ShowDialog()
        If result = DialogResult.OK Then
            Me.TextBox001.Text = formSearch.Selected.MTMR003001
            Me.TextBox002.Text = formSearch.Selected.MTMR003002
            Me.TextBox003.Text = Me.FormMenu.ModelMtmUser.MTMM002001
            Me.TextBox004.Text = Me.FormMenu.ModelMtmUser.MTMM002002
            Me.TextBox005.Text = ""
            Me.TextBox006.Text = ""
            Me.TextBox010.Text = "受注分より"
            Me.TextBox007.Text = "従来通り"
            Me.TextBox008.Text = ""
            '取込ファイル名を取得してセット
            Me.TextBox009.Text = Me.Biz01.ImportFileNameSearch()
            Me.DatePicker001.Format = DateTimePickerFormat.Custom
            Me.DatePicker001.CustomFormat = " "
            Me.DatePicker002.Format = DateTimePickerFormat.Custom
            Me.DatePicker002.CustomFormat = " "
            Me.DatePicker003.Format = DateTimePickerFormat.Custom
            Me.DatePicker003.CustomFormat = " "
            Me.CheckBox001.Checked = False
            Me.CheckBox002.Checked = False
            Me.CheckBox003.Checked = False
            Me.CheckBox004.Checked = False
            Me.CheckBox005.Checked = False
            Me.CheckBox006.Checked = False
            Me.CheckBox007.Checked = False
        End If
        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' ログインID空白処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox003_TextChanged(sender As Object, e As EventArgs) Handles TextBox003.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox003.Text))) Then
            Me.TextBox004.Text = ""
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

        errorList = Me.Biz01.OutputPreviewAnnaiPdf(Me.TextBox008.Text, executingPath + "\" + pdfDir)
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