Imports System.Configuration
Imports System.Text.RegularExpressions
Imports C1.Win.C1FlexGrid
Imports MitsumoLib

Public Class FormMTM07ViewV2

    Public Property TypeText As String

    ''' <summary>
    ''' フォーム
    ''' </summary>
    Private ReadOnly Form07 As FormMTM07
    Private ReadOnly SearchCondition As Biz.MTM07SearchCondition

    ''' <summary>
    ''' 価格入力ビジネスロジック
    ''' </summary>
    Private ReadOnly Biz07 As Biz.MTM07

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="form07"></param>
    Public Sub New(ByVal form07 As FormMTM07)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.Form07 = form07
        Me.SearchCondition = form07.SearchCondition

        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.Biz07 = New Biz.MTM07(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM02Regist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '出力対象の判定
        Me.Text = "[mitsumo]" + TypeText
        'Gridの設定
        DataGridView001.Font = New Font(“メイリオ”, 9)
        DataGridView001.AllowSorting = AllowSortingEnum.Auto
        ' フィルタの設定
        'AddHandler DataGridView001.MouseClick, AddressOf DataGridView001_MouseClick
        If (TypeText = "未送信確認") Then
            Me.SetDataV3()
        Else
            Me.SetDataV2()
        End If
        Me.SetDataGridColumnV2()
        ' DrawModeプロパティを設定して、OwnerDrawを有効にします
        DataGridView001.ClipboardCopyMode = ClipboardCopyModeEnum.DataAndAllHeaders
        DataGridView001.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
        DataGridView001.ScrollBars = ScrollBars.None
        DataGridView001.ScrollBars = ScrollBars.Both
        Me.WindowState = FormWindowState.Maximized

    End Sub

    ''' <summary>
    ''' データグリッド項目の設定
    ''' </summary>
    Private Sub SetDataGridColumnV2()

        ' -------- FlexGridの共通 --------
        DataGridView001.Rows(0).Height = 50
        DataGridView001.Styles.Normal.WordWrap = True
        DataGridView001.Rows.Fixed = 1
        'DataGridView001.Cols.Frozen = 12
        'DataGridView001.Cols.Frozen = 12
        DataGridView001.Rows.DefaultSize = 25
        'DataGridView001.EditOptions = EditFlags.All

        ' -------- FlexGridのスタイル --------
        ' checkBoxStyle1　列ヘッダーにチェックボックスを設定
        Dim checkBoxStyle1 As CellStyle = DataGridView001.Styles.Add("CheckBoxStyle1")
        checkBoxStyle1.BackColor = Color.MediumTurquoise
        checkBoxStyle1.ForeColor = Color.White
        checkBoxStyle1.TextAlign = TextAlignEnum.CenterCenter
        checkBoxStyle1.Font = New Font("メイリオ", 8, FontStyle.Underline)
        checkBoxStyle1.DataType = GetType(Boolean)
        checkBoxStyle1.ImageAlign = ImageAlignEnum.CenterCenter

        ' checkBoxStyle2　列ヘッダーにチェックボックスを設定なし（ノーマル）
        Dim checkBoxStyle2 As CellStyle = DataGridView001.Styles.Add("CheckBoxStyle2")
        checkBoxStyle2.BackColor = Color.MediumTurquoise
        checkBoxStyle2.ForeColor = Color.White
        checkBoxStyle2.TextAlign = TextAlignEnum.CenterCenter
        checkBoxStyle2.Font = New Font("メイリオ", 8, FontStyle.Underline)

        ' checkBoxStyle3　列ヘッダーにチェックボックスを設定なし（ノーマル）
        Dim checkBoxStyle3 As CellStyle = DataGridView001.Styles.Add("checkBoxStyle3")
        checkBoxStyle3.BackColor = Color.MediumTurquoise
        checkBoxStyle3.ForeColor = Color.White
        checkBoxStyle3.TextAlign = TextAlignEnum.CenterCenter
        checkBoxStyle3.Font = New Font("メイリオ", 8, FontStyle.Regular)

        ' NewStyle1
        Dim newStyle1 As CellStyle = DataGridView001.Styles.Add("NewStyle1")
        newStyle1.BackColor = Color.LightSeaGreen
        newStyle1.ForeColor = Color.White
        newStyle1.TextAlign = TextAlignEnum.CenterCenter
        newStyle1.Font = New Font("メイリオ", 8, FontStyle.Regular)

        Dim newStyle1_1 As CellStyle = DataGridView001.Styles.Add("NewStyle1_1")
        newStyle1_1.BackColor = Color.LightSeaGreen
        newStyle1_1.ForeColor = Color.White
        newStyle1_1.TextAlign = TextAlignEnum.CenterCenter
        newStyle1_1.Font = New Font("メイリオ", 9, FontStyle.Underline)

        Dim newStyle2 As CellStyle = DataGridView001.Styles.Add("NewStyle2")
        newStyle2.BackColor = Color.LightSeaGreen
        newStyle2.ForeColor = Color.White
        newStyle2.TextAlign = TextAlignEnum.CenterCenter
        newStyle2.Font = New Font("メイリオ", 9, FontStyle.Regular)

        Dim newStyle3 As CellStyle = DataGridView001.Styles.Add("NewStyle3")
        newStyle3.BackColor = Color.LightSeaGreen
        newStyle3.ForeColor = Color.White
        newStyle3.TextAlign = TextAlignEnum.CenterCenter
        newStyle3.Font = New Font("メイリオ", 9, FontStyle.Underline)

        ' checkBoxStyle1　列ヘッダーにチェックボックスを設定合体
        Dim checkBoxStyleNew As CellStyle = DataGridView001.Styles.Add("checkBoxStyleNew")
        checkBoxStyleNew.BackColor = Color.LightSeaGreen
        checkBoxStyleNew.ForeColor = Color.White
        checkBoxStyleNew.TextAlign = TextAlignEnum.CenterCenter
        checkBoxStyleNew.Font = New Font("メイリオ", 8, FontStyle.Underline)
        checkBoxStyleNew.DataType = GetType(Boolean)
        'checkBoxStyle1.ImageAlign = ImageAlignEnum.CenterCenter

        ' -------- FlexGridの各項目設定 --------
        ' ヘッダーの設定
        DataGridView001.Cols(0).Caption = "社内締切日"
        DataGridView001.Cols(0).DataType = GetType(String)
        DataGridView001.Cols(0).Name = "MTMR003007"
        DataGridView001.Cols(0).AllowSorting = False
        DataGridView001.Cols(0).Width = 120
        DataGridView001.Cols(0).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(0).AllowEditing = False
        DataGridView001.Cols(0).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 0, newStyle2)

        DataGridView001.Cols(1).Caption = "仕入先実施日"
        DataGridView001.Cols(1).DataType = GetType(String)
        DataGridView001.Cols(1).Name = "MTMR003023"
        DataGridView001.Cols(1).AllowSorting = False
        DataGridView001.Cols(1).Width = 120
        DataGridView001.Cols(1).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(1).AllowEditing = False
        DataGridView001.Cols(1).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 1, newStyle2)

        DataGridView001.Cols(2).Caption = "価格入力名"
        DataGridView001.Cols(2).DataType = GetType(String)
        DataGridView001.Cols(2).Name = "MTMR003002"
        DataGridView001.Cols(2).AllowSorting = False
        DataGridView001.Cols(2).Width = 400
        DataGridView001.Cols(2).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(2).AllowEditing = False
        DataGridView001.Cols(2).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 2, newStyle2)

        DataGridView001.Cols(3).Caption = "営業所"
        DataGridView001.Cols(3).DataType = GetType(String)
        DataGridView001.Cols(3).Name = "MTMR002009"
        DataGridView001.Cols(3).AllowSorting = False
        DataGridView001.Cols(3).Width = 400
        DataGridView001.Cols(3).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(3).AllowEditing = False
        DataGridView001.Cols(3).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 3, newStyle2)

        DataGridView001.Cols(4).Caption = "部課"
        DataGridView001.Cols(4).DataType = GetType(String)
        DataGridView001.Cols(4).Name = "MTMR002011"
        DataGridView001.Cols(4).AllowSorting = False
        DataGridView001.Cols(4).Width = 400
        DataGridView001.Cols(4).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(4).AllowEditing = False
        DataGridView001.Cols(4).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 4, newStyle2)

        DataGridView001.Cols(5).Caption = "担当者"
        DataGridView001.Cols(5).DataType = GetType(String)
        DataGridView001.Cols(5).Name = "MTMR002013"
        DataGridView001.Cols(5).Width = 300
        DataGridView001.Cols(5).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(5).AllowEditing = False
        DataGridView001.Cols(5).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 5, newStyle2)

        DataGridView001.Cols(6).Caption = "残件数"
        DataGridView001.Cols(6).DataType = GetType(String)
        DataGridView001.Cols(6).Name = "MTMR002085_CNT"
        DataGridView001.Cols(6).AllowSorting = False
        DataGridView001.Cols(6).Style.Format = "#,##0"
        DataGridView001.Cols(6).Width = 100
        DataGridView001.Cols(6).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(6).AllowEditing = False
        DataGridView001.Cols(6).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 6, newStyle2)

        DataGridView001.Cols(7).Caption = "営業所コード"
        DataGridView001.Cols(7).DataType = GetType(String)
        DataGridView001.Cols(7).Name = "MTMR002008"
        DataGridView001.Cols(7).AllowSorting = False
        DataGridView001.Cols(7).Visible = False
        DataGridView001.Cols(7).Width = 80
        DataGridView001.Cols(7).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(7).AllowEditing = False
        DataGridView001.Cols(7).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 7, newStyle2)

        DataGridView001.Cols(8).Caption = "部課コード"
        DataGridView001.Cols(8).DataType = GetType(String)
        DataGridView001.Cols(8).Name = "MTMR002008"
        DataGridView001.Cols(8).AllowSorting = False
        DataGridView001.Cols(8).Visible = False
        DataGridView001.Cols(8).Width = 80
        DataGridView001.Cols(8).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(8).AllowEditing = False
        DataGridView001.Cols(8).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 8, newStyle2)

        DataGridView001.Cols(9).Caption = "担当者コード"
        DataGridView001.Cols(9).DataType = GetType(String)
        DataGridView001.Cols(9).Name = "MTMR002012"
        DataGridView001.Cols(9).AllowSorting = False
        DataGridView001.Cols(9).Visible = False
        DataGridView001.Cols(9).Width = 80
        DataGridView001.Cols(9).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(9).AllowEditing = False
        DataGridView001.Cols(9).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 9, newStyle2)

        DataGridView001.Cols(10).Caption = "価格入力番号"
        DataGridView001.Cols(10).DataType = GetType(String)
        DataGridView001.Cols(10).Name = "MTMR003001"
        DataGridView001.Cols(10).AllowSorting = False
        DataGridView001.Cols(10).Visible = False
        DataGridView001.Cols(10).Width = 80
        DataGridView001.Cols(10).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(10).AllowEditing = False
        DataGridView001.Cols(10).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 10, newStyle2)

        'DataGridView001.Cols(1).Caption = "社内締切日"
        'DataGridView001.Cols(1).DataType = GetType(String)
        'DataGridView001.Cols(1).Name = "MTMR003007"
        'DataGridView001.Cols(1).AllowSorting = False
        'DataGridView001.Cols(1).Width = 120
        'DataGridView001.Cols(1).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(1).AllowEditing = False
        'DataGridView001.Cols(1).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 1, newStyle2)

        'DataGridView001.Cols(2).Caption = "仕入先実施日"
        'DataGridView001.Cols(2).DataType = GetType(String)
        'DataGridView001.Cols(2).Name = "MTMR003023"
        'DataGridView001.Cols(2).AllowSorting = False
        'DataGridView001.Cols(2).Width = 120
        'DataGridView001.Cols(2).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(2).AllowEditing = False
        'DataGridView001.Cols(2).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 2, newStyle2)

        'DataGridView001.Cols(3).Caption = "価格入力名"
        'DataGridView001.Cols(3).DataType = GetType(String)
        'DataGridView001.Cols(3).Name = "MTMR003002"
        'DataGridView001.Cols(3).AllowSorting = False
        'DataGridView001.Cols(3).Width = 400
        'DataGridView001.Cols(3).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(3).AllowEditing = False
        'DataGridView001.Cols(3).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 3, newStyle2)

        'DataGridView001.Cols(4).Caption = "営業所"
        'DataGridView001.Cols(4).DataType = GetType(String)
        'DataGridView001.Cols(4).Name = "MTMR002009"
        'DataGridView001.Cols(4).AllowSorting = False
        'DataGridView001.Cols(4).Width = 400
        'DataGridView001.Cols(4).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(4).AllowEditing = False
        'DataGridView001.Cols(4).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 4, newStyle2)

        'DataGridView001.Cols(5).Caption = "部課"
        'DataGridView001.Cols(5).DataType = GetType(String)
        'DataGridView001.Cols(5).Name = "MTMR002011"
        'DataGridView001.Cols(5).AllowSorting = False
        'DataGridView001.Cols(5).Width = 400
        'DataGridView001.Cols(5).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(5).AllowEditing = False
        'DataGridView001.Cols(5).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 5, newStyle2)

        'DataGridView001.Cols(6).Caption = "担当者"
        'DataGridView001.Cols(6).DataType = GetType(String)
        'DataGridView001.Cols(6).Name = "MTMR002013"
        'DataGridView001.Cols(6).Width = 300
        'DataGridView001.Cols(6).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(6).AllowEditing = False
        'DataGridView001.Cols(6).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 6, newStyle2)

        'DataGridView001.Cols(7).Caption = "残件数"
        'DataGridView001.Cols(7).DataType = GetType(String)
        'DataGridView001.Cols(7).Name = "MTMR002085_CNT"
        'DataGridView001.Cols(7).AllowSorting = False
        'DataGridView001.Cols(7).Style.Format = "#,##0"
        'DataGridView001.Cols(7).Width = 100
        'DataGridView001.Cols(7).TextAlign = TextAlignEnum.RightCenter
        'DataGridView001.Cols(7).AllowEditing = False
        'DataGridView001.Cols(7).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 7, newStyle2)

        'DataGridView001.Cols(8).Caption = "営業所コード"
        'DataGridView001.Cols(8).DataType = GetType(String)
        'DataGridView001.Cols(8).Name = "MTMR002008"
        'DataGridView001.Cols(8).AllowSorting = False
        'DataGridView001.Cols(8).Visible = False
        'DataGridView001.Cols(8).Width = 80
        'DataGridView001.Cols(8).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(8).AllowEditing = False
        'DataGridView001.Cols(8).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 8, newStyle2)

        'DataGridView001.Cols(9).Caption = "部課コード"
        'DataGridView001.Cols(9).DataType = GetType(String)
        'DataGridView001.Cols(9).Name = "MTMR002008"
        'DataGridView001.Cols(9).AllowSorting = False
        'DataGridView001.Cols(9).Visible = False
        'DataGridView001.Cols(9).Width = 80
        'DataGridView001.Cols(9).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(9).AllowEditing = False
        'DataGridView001.Cols(9).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 9, newStyle2)

        'DataGridView001.Cols(10).Caption = "担当者コード"
        'DataGridView001.Cols(10).DataType = GetType(String)
        'DataGridView001.Cols(10).Name = "MTMR002012"
        'DataGridView001.Cols(10).AllowSorting = False
        'DataGridView001.Cols(10).Visible = False
        'DataGridView001.Cols(10).Width = 80
        'DataGridView001.Cols(10).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(10).AllowEditing = False
        'DataGridView001.Cols(10).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 10, newStyle2)

        'DataGridView001.Cols(11).Caption = "価格入力番号"
        'DataGridView001.Cols(11).DataType = GetType(String)
        'DataGridView001.Cols(11).Name = "MTMR003001"
        'DataGridView001.Cols(11).AllowSorting = False
        'DataGridView001.Cols(11).Visible = False
        'DataGridView001.Cols(11).Width = 80
        'DataGridView001.Cols(11).TextAlign = TextAlignEnum.LeftCenter
        'DataGridView001.Cols(11).AllowEditing = False
        'DataGridView001.Cols(11).Style.BackColor = Color.LightGray
        'DataGridView001.SetCellStyle(0, 11, newStyle2)

    End Sub

    ''' <summary>
    ''' データの設定(未確定)
    ''' </summary>
    Public Sub SetDataV2()
        Dim table As DataTable = Me.Biz07.GetKakaku(Me.SearchCondition)
        Me.DataGridView001.DataSource = table
    End Sub
    ''' <summary>
    ''' データの設定(未送信)
    ''' </summary>
    Public Sub SetDataV3()
        Dim table As DataTable = Me.Biz07.GetKakaku2(Me.SearchCondition)
        Me.DataGridView001.DataSource = table
    End Sub
    ''' <summary>
    ''' 実行管理テーブルの表示項目設定を取得
    ''' </summary>
    Public Sub SetJikouHyouji()
        Dim table As DataTable = Me.Biz07.GetKakaku(Me.SearchCondition)
        Me.DataGridView001.DataSource = table
    End Sub
    ''' <summary>
    ''' 閉じるボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' グリッド行選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        Dim formSelect As New FormMTMSelect

        Dim result As DialogResult = formSelect.ShowDialog()
        If result = DialogResult.OK Then
        End If

        formSelect.Dispose()

    End Sub
    ''' <summary>
    ''' グリッドフォーマッティング
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)

    End Sub
    ''' <summary>
    ''' 確定オール選択/ オール解除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM02Regist_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Me.DataGridView001.Rows.Count > 0 Then
            If e.KeyCode = Keys.F3 Then
                DataGridView001.FilterDefinition = String.Empty
            End If
            If e.KeyCode = Keys.F5 Then
                DataGridView001.SortDefinition = String.Empty
            End If
        End If
    End Sub
    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM02Regist_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim result As DialogResult = MessageBox.Show("終了します。よろしいですか？",
                                                     "確認",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Information,
                                                     MessageBoxDefaultButton.Button2)
        If result = DialogResult.Yes Then
            Me.Form07.Show()
        Else
            e.Cancel = True
        End If
    End Sub
    ''' <summary>
    ''' グリッド編集モード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_Click(sender As Object, e As EventArgs) Handles DataGridView001.Click
        'DataGridView001.StartEditing(DataGridView001.Row, DataGridView001.Col)
    End Sub

    ''' <summary>
    ''' グリッドのバリデーションチェック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_ValidateEdit(sender As Object, e As ValidateEditEventArgs) Handles DataGridView001.ValidateEdit

    End Sub
    ''' <summary>
    ''' グリッドイベントSetupEditor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_SetupEditor(sender As Object, e As RowColEventArgs) Handles DataGridView001.SetupEditor
        If e.Col = 26 OrElse e.Col = 29 Then
            Dim dt As DateTimePicker = CType(DataGridView001.Editor, DateTimePicker)
            dt.ShowCheckBox = False
        End If
    End Sub
    ''' <summary>
    ''' グリッドイベントOwnerDrawCell
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_OwnerDrawCell(sender As Object, e As OwnerDrawCellEventArgs) Handles DataGridView001.OwnerDrawCell
        If Not e.Measuring Then
            ' 行と列に整数データが含まれていることを確認します
            If e.Row > 1 Then
                If e.Col = 30 OrElse e.Col = 31 Then
                    Dim cellVal = DataGridView001(e.Row, e.Col).ToString()
                    If (cellVal < 0) Then
                        e.Style = DataGridView001.Styles("Red")
                    End If
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' グリッドイベントDoubleClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView001.DoubleClick
        Dim formSelect As New FormMTMSelect

        If (Not IsNothing(DataGridView001.RowSel)) Then
            Dim intSelectRow As Integer
            Dim intSelectCol As Integer
            intSelectRow = DataGridView001.RowSel
            intSelectCol = DataGridView001.ColSel
            If (intSelectRow >= 2 And (intSelectCol = 7 OrElse intSelectCol = 10)) Then
                formSelect.selectMTMR002001 = DataGridView001(intSelectRow, 6).ToString()
                formSelect.selectMTMR002002 = DataGridView001(intSelectRow, 9).ToString()
                formSelect.selectMTMR002003 = DataGridView001(intSelectRow, 11).ToString()


                Dim result As DialogResult = formSelect.ShowDialog()
                If result = DialogResult.OK Then
                End If

                formSelect.Dispose()
            End If
        End If
    End Sub
    ''' <summary>
    ''' グリッドイベントKeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView001.KeyDown


    End Sub

    Private Sub ButtonCopy_Click(sender As Object, e As EventArgs) Handles ButtonCopy.Click
        ' 選択したセル範囲のCellRangeオブジェクトを取得します
        Dim cr As C1.Win.C1FlexGrid.CellRange
        cr = DataGridView001.GetCellRange(0, 0, DataGridView001.Rows.Count - 1, DataGridView001.Cols.Count - 1)
        Dim StrCopy = ""

        For i = cr.r1 To cr.r2
            If DataGridView001.Rows(i).Visible = True Then
                For j = cr.c1 To cr.c2
                    If DataGridView001.Cols(j).Visible = True Then
                        StrCopy = StrCopy & DataGridView001(i, j).ToString()

                        If j <> cr.c2 Then
                            StrCopy = StrCopy & Microsoft.VisualBasic.Constants.vbTab
                        End If
                    End If
                Next

                StrCopy = StrCopy & Microsoft.VisualBasic.Constants.vbLf
            End If
        Next
        ' クリップボードに設定します
        Clipboard.SetDataObject(StrCopy)
    End Sub

    Private Sub DataGridView001_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView001.MouseClick
        If DataGridView001.HitTest(e.Location).Type = HitTestTypeEnum.FilterIcon Then
            For Each frm As Form In Application.OpenForms
                If frm.Name = "FilterEditorForm" AndAlso frm.GetType().ToString() = "C1.Win.C1FlexGrid.FilterEditorForm" Then
                    Dim wFrm As Integer = 600
                    frm.MaximumSize = New Size(wFrm, 600)
                    frm.Width = wFrm
                    frm.Controls(1).Width = frm.Width
                End If
            Next
        End If
    End Sub
End Class

