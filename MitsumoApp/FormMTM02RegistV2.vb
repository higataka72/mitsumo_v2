Imports System.Configuration
Imports System.Text.RegularExpressions
Imports C1.Win.C1FlexGrid
Imports MitsumoLib

Public Class FormMTM02RegistV2

    ''' <summary>
    ''' 価格入力検索フォーム
    ''' </summary>
    Private ReadOnly Form02 As FormMTM02
    Private ReadOnly SearchCondition As Biz.MTM02SearchCondition

    ''' <summary>
    ''' 価格入力ビジネスロジック
    ''' </summary>
    Private ReadOnly Biz02 As Biz.MTM02

    Private allCheck As Boolean = True

    Private digitsMTMR002030 As Integer = 0
    Private fewMTMR002030 As Integer = 0
    Private copyMTMR002030 As String
    Private konmaMTMR002030 As Boolean = False
    Private digitsMTMR002033 As Integer = 0
    Private fewMTMR002033 As Integer = 0
    Private copyMTMR002033 As String
    Private konmaMTMR002033 As Boolean = False

    'Keypressイベント記録
    Private keypressMTMR002030 As Boolean = False
    Private keypressMTMR002031 As Boolean = False
    Private keypressMTMR002033 As Boolean = False
    Private keypressMTMR002034 As Boolean = False
    Private keypressMTMR002032 As Boolean = False
    Private keypressMTMR002035 As Boolean = False

    '実行管理テーブルの表示項目
    Private displayMTMR003011 As Boolean = False
    Private displayMTMR003012 As Boolean = False
    Private displayMTMR003013 As Boolean = False
    Private displayMTMR003014 As Boolean = False
    Private displayMTMR003015 As Boolean = False
    Private displayMTMR003016 As Boolean = False
    Private displayMTMR003017 As Boolean = False

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="form02"></param>
    Public Sub New(ByVal form02 As FormMTM02)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.Form02 = form02
        Me.SearchCondition = form02.SearchCondition

        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.Biz02 = New Biz.MTM02(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM02Regist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Gridの設定
        DataGridView001.Font = New Font(“メイリオ”, 9)
        DataGridView001.AllowSorting = AllowSortingEnum.Auto
        ' フィルタの設定
        AddHandler DataGridView001.MouseClick, AddressOf DataGridView001_MouseClick
        Me.SetHeader()
        Me.SetDataV2()
        Me.SetDataGridColumnV2()
        Me.SetDataGridColumnVisibleV2(False)
        Me.SetFooterV2()
        ' 負の値を表示するために使用されるスタイルを作成します
        DataGridView001.Styles.Add("Red").ForeColor = Color.Red
        ' DrawModeプロパティを設定して、OwnerDrawを有効にします
        DataGridView001.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
        'DataGridView001.OwnerDrawCell = New C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(DataGridView001_OwnerDrawCell)
        DataGridView001.ScrollBars = ScrollBars.None
        DataGridView001.ScrollBars = ScrollBars.Both
        Me.WindowState = FormWindowState.Maximized

        'コンテキストメニュー
        'Dim cm As ContextMenuStrip = New ContextMenuStrip()
        '' コンテキストメニューにメニュー項目を追加します
        'cm.Items.Add("売上履歴")
        'cm.Items.Add("単価台帳参照")
        '' インスタンスをContextMenuStripプロパティに割り当てます 
        'DataGridView001.ContextMenuStrip = cm
    End Sub

    ''' <summary>
    ''' ヘッダー情報の設定
    ''' </summary>
    Private Sub SetHeader()
        Dim jitsukou As Models.MTM10R003JITSUKOU = Me.Biz02.GetJitsukou(Me.SearchCondition)
        Me.TextBox001.Text = jitsukou.MTMR003001
        Me.TextBox002.Text = jitsukou.MTMR003002
        Me.TextBox003.Text = jitsukou.MTMR003005
        Me.TextBox004.Text = jitsukou.MTMR003005_2
        Me.TextBox005.Text = jitsukou.MTMR003007
        If jitsukou.MTMR003011 = 1 Then displayMTMR003011 = True
        If jitsukou.MTMR003012 = 1 Then displayMTMR003012 = True
        If jitsukou.MTMR003013 = 1 Then displayMTMR003013 = True
        If jitsukou.MTMR003014 = 1 Then displayMTMR003014 = True
        If jitsukou.MTMR003015 = 1 Then displayMTMR003015 = True
        If jitsukou.MTMR003016 = 1 Then displayMTMR003016 = True
        If jitsukou.MTMR003017 = 1 Then displayMTMR003017 = True
    End Sub

    ''' <summary>
    ''' データグリッド項目の設定
    ''' </summary>
    Private Sub SetDataGridColumnV2()

        ' -------- FlexGridの共通 --------
        DataGridView001.Rows(0).Height = 80
        DataGridView001.Styles.Normal.WordWrap = True
        DataGridView001.Rows.Fixed = 2
        DataGridView001.Cols.Frozen = 12
        DataGridView001.Cols.Frozen = 12
        DataGridView001.Rows(0).Height = 80
        DataGridView001.Rows.DefaultSize = 20
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
        DataGridView001.Cols(1).Caption = "確定"
        DataGridView001.Cols(1).DataType = GetType(Boolean)
        DataGridView001.Cols(1).Name = "MTMR002085"
        DataGridView001.Cols(1).Width = 40
        DataGridView001.Cols(1).TextAlign = TextAlignEnum.CenterCenter
        DataGridView001.Cols(1).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 1, newStyle1_1)
        DataGridView001.SetCellStyle(1, 1, "checkBoxStyle1")

        DataGridView001.Cols(2).Caption = "印刷無"
        DataGridView001.Cols(2).DataType = GetType(Boolean)
        DataGridView001.Cols(2).Name = "MTMR002086"
        DataGridView001.Cols(2).AllowSorting = False
        DataGridView001.Cols(2).Width = 40
        DataGridView001.Cols(2).TextAlign = TextAlignEnum.CenterCenter
        DataGridView001.Cols(2).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 2, newStyle1)
        DataGridView001.SetCellStyle(1, 2, "checkBoxStyle1")

        DataGridView001.Cols(3).Caption = "営業所"
        DataGridView001.Cols(3).DataType = GetType(String)
        DataGridView001.Cols(3).Name = "MTMR002009"
        'DataGridView001.Cols(3).AllowFiltering = AllowFiltering.ByCondition
        DataGridView001.Cols(3).AllowSorting = False
        DataGridView001.Cols(3).Width = 80
        DataGridView001.Cols(3).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(3).AllowEditing = False
        DataGridView001.Cols(3).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 3, newStyle2)
        DataGridView001.SetCellStyle(1, 3, "checkBoxStyle2")

        DataGridView001.Cols(4).Caption = "部課"
        DataGridView001.Cols(4).DataType = GetType(String)
        DataGridView001.Cols(4).Name = "MTMR002011"
        DataGridView001.Cols(4).AllowSorting = False
        DataGridView001.Cols(4).Width = 160
        DataGridView001.Cols(4).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(4).AllowEditing = False
        DataGridView001.Cols(4).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 4, newStyle2)
        DataGridView001.SetCellStyle(1, 4, "checkBoxStyle2")

        DataGridView001.Cols(5).Caption = "担当者"
        DataGridView001.Cols(5).DataType = GetType(String)
        DataGridView001.Cols(5).Name = "MTMR002013"
        DataGridView001.Cols(5).Width = 80
        DataGridView001.Cols(5).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(5).AllowEditing = False
        DataGridView001.Cols(5).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 5, newStyle3)
        DataGridView001.SetCellStyle(1, 5, "checkBoxStyle2")

        DataGridView001.Cols(6).Caption = "得意先コード"
        DataGridView001.Cols(6).DataType = GetType(String)
        DataGridView001.Cols(6).Name = "MTMR002001"
        DataGridView001.Cols(6).AllowSorting = False
        DataGridView001.Cols(6).Width = 90
        DataGridView001.Cols(6).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(6).AllowEditing = False
        DataGridView001.Cols(6).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 6, newStyle2)
        DataGridView001.SetCellStyle(1, 6, "checkBoxStyle2")

        DataGridView001.Cols(7).Caption = "★得意先名"
        DataGridView001.Cols(7).DataType = GetType(String)
        DataGridView001.Cols(7).Name = "MTMR002005"
        DataGridView001.Cols(7).AllowSorting = False
        DataGridView001.Cols(7).Width = 240
        DataGridView001.Cols(7).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(7).AllowEditing = False
        DataGridView001.Cols(7).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 7, newStyle2)
        DataGridView001(1, 7) = "ﾀﾞﾌﾞﾙｸﾘｯｸで売上履歴・単価台帳を開く"
        DataGridView001.SetCellStyle(1, 7, "checkBoxStyle3")

        DataGridView001.Cols(8).Caption = "ﾗﾝｸ"
        DataGridView001.Cols(8).DataType = GetType(String)
        DataGridView001.Cols(8).Name = "MTMR002007"
        DataGridView001.Cols(8).Width = 45
        DataGridView001.Cols(8).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(8).AllowEditing = False
        DataGridView001.Cols(8).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 8, newStyle3)
        DataGridView001.SetCellStyle(1, 8, "checkBoxStyle2")

        DataGridView001.Cols(9).Caption = "商品C"
        DataGridView001.Cols(9).DataType = GetType(String)
        DataGridView001.Cols(9).Name = "MTMR002002"
        DataGridView001.Cols(9).AllowSorting = False
        DataGridView001.Cols(9).Width = 100
        DataGridView001.Cols(9).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(9).AllowEditing = False
        DataGridView001.Cols(9).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 9, newStyle2)
        DataGridView001.SetCellStyle(1, 9, "checkBoxStyle2")

        DataGridView001.Cols(10).Caption = "商品名"
        DataGridView001.Cols(10).DataType = GetType(String)
        DataGridView001.Cols(10).Name = "MTMR002016"
        DataGridView001.Cols(10).Width = 240
        DataGridView001.Cols(10).AllowFiltering = AllowFiltering.Default
        DataGridView001.Cols(10).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(10).AllowEditing = False
        DataGridView001.Cols(10).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 10, newStyle3)
        DataGridView001(1, 10) = "ﾀﾞﾌﾞﾙｸﾘｯｸで売上履歴・単価台帳を開く"
        DataGridView001.SetCellStyle(1, 10, "checkBoxStyle3")

        DataGridView001.Cols(11).Caption = "規格"
        DataGridView001.Cols(11).DataType = GetType(String)
        DataGridView001.Cols(11).Name = "MTMR002003"
        DataGridView001.Cols(11).AllowSorting = False
        DataGridView001.Cols(11).Width = 50
        DataGridView001.Cols(11).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(11).AllowEditing = False
        DataGridView001.Cols(11).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 11, newStyle2)
        DataGridView001.SetCellStyle(1, 11, "checkBoxStyle2")

        DataGridView001.Cols(12).Caption = "★ロット"
        DataGridView001.Cols(12).DataType = GetType(String)
        DataGridView001.Cols(12).Name = "MTMR002017"
        DataGridView001.Cols(12).AllowSorting = False
        DataGridView001.Cols(12).Width = 90
        DataGridView001.Cols(12).MaxLength = 12

        DataGridView001.Cols(12).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(12).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 12, newStyle2)
        DataGridView001.SetCellStyle(1, 12, "checkBoxStyle2")

        DataGridView001.Cols(13).Caption = "数量・上限"
        DataGridView001.Cols(13).DataType = GetType(Integer)
        DataGridView001.Cols(13).Name = "MTMR002004"
        DataGridView001.Cols(13).AllowSorting = False
        DataGridView001.Cols(13).Style.Format = "#,##0"
        DataGridView001.Cols(13).Width = 90
        DataGridView001.Cols(13).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(13).AllowEditing = False
        DataGridView001.Cols(13).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 13, newStyle2)
        DataGridView001.SetCellStyle(1, 13, "checkBoxStyle2")

        DataGridView001.Cols(14).Caption = "★入数"
        DataGridView001.Cols(14).DataType = GetType(Integer)
        DataGridView001.Cols(14).Name = "MTMR002019"
        DataGridView001.Cols(14).AllowSorting = False
        DataGridView001.Cols(14).Style.Format = "N4"
        DataGridView001.Cols(14).Width = 60
        DataGridView001.Cols(14).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(14).AllowEditing = False
        DataGridView001.Cols(14).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 14, newStyle2)
        DataGridView001.SetCellStyle(1, 14, "checkBoxStyle2")

        DataGridView001.Cols(15).Caption = "★単位"
        DataGridView001.Cols(15).DataType = GetType(String)
        DataGridView001.Cols(15).Name = "MTMR002020"
        DataGridView001.Cols(15).AllowSorting = False
        DataGridView001.Cols(15).Width = 60
        DataGridView001.Cols(15).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(15).AllowEditing = False
        DataGridView001.Cols(15).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 15, newStyle2)
        DataGridView001.SetCellStyle(1, 15, "checkBoxStyle2")

        DataGridView001.Cols(16).Caption = "改定前売単価"
        DataGridView001.Cols(16).DataType = GetType(Integer)
        DataGridView001.Cols(16).Name = "MTMR002022"
        DataGridView001.Cols(16).AllowSorting = False
        DataGridView001.Cols(16).Style.Format = "N4"
        DataGridView001.Cols(16).Width = 100
        DataGridView001.Cols(16).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(16).AllowEditing = False
        DataGridView001.Cols(16).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 16, newStyle1)
        DataGridView001.SetCellStyle(1, 16, "checkBoxStyle2")

        DataGridView001.Cols(17).Caption = "改定前仕単価"
        DataGridView001.Cols(17).DataType = GetType(Integer)
        DataGridView001.Cols(17).Name = "MTMR002023"
        DataGridView001.Cols(17).AllowSorting = False
        DataGridView001.Cols(17).Style.Format = "N4"
        DataGridView001.Cols(17).Width = 100
        DataGridView001.Cols(17).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(17).AllowEditing = False
        DataGridView001.Cols(17).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 17, newStyle1)
        DataGridView001.SetCellStyle(1, 17, "checkBoxStyle2")

        DataGridView001.Cols(18).Caption = "改定前粗利率"
        DataGridView001.Cols(18).DataType = GetType(Integer)
        DataGridView001.Cols(18).Name = "MTMR002024"
        DataGridView001.Cols(18).AllowSorting = False
        DataGridView001.Cols(18).Style.Format = "N1"
        DataGridView001.Cols(18).Width = 100
        DataGridView001.Cols(18).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(18).AllowEditing = False
        DataGridView001.Cols(18).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 18, newStyle1)
        DataGridView001.SetCellStyle(1, 18, "checkBoxStyle2")

        DataGridView001.Cols(19).Caption = "★現売単価"
        DataGridView001.Cols(19).DataType = GetType(Integer)
        DataGridView001.Cols(19).Name = "MTMR002025"
        DataGridView001.Cols(19).AllowSorting = False
        DataGridView001.Cols(19).Style.Format = "N4"
        DataGridView001.Cols(19).Width = 90
        DataGridView001.Cols(19).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(19).AllowEditing = False
        DataGridView001.Cols(19).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 19, newStyle1)
        DataGridView001.SetCellStyle(1, 19, "checkBoxStyle2")

        DataGridView001.Cols(20).Caption = "現売㎡単価"
        DataGridView001.Cols(20).DataType = GetType(Integer)
        DataGridView001.Cols(20).Name = "MTMR002026"
        DataGridView001.Cols(20).AllowSorting = False
        DataGridView001.Cols(20).Style.Format = "N4"
        DataGridView001.Cols(20).Width = 90
        DataGridView001.Cols(20).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(20).AllowEditing = False
        DataGridView001.Cols(20).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 20, newStyle1)
        DataGridView001.SetCellStyle(1, 20, "checkBoxStyle2")

        DataGridView001.Cols(21).Caption = "現仕単価"
        DataGridView001.Cols(21).DataType = GetType(Integer)
        DataGridView001.Cols(21).Name = "MTMR002027"
        DataGridView001.Cols(21).AllowSorting = False
        DataGridView001.Cols(21).Style.Format = "N4"
        DataGridView001.Cols(21).Width = 80
        DataGridView001.Cols(21).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(21).AllowEditing = False
        DataGridView001.Cols(21).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 21, newStyle1)
        DataGridView001.SetCellStyle(1, 21, "checkBoxStyle2")

        DataGridView001.Cols(22).Caption = "現仕㎡単価"
        DataGridView001.Cols(22).DataType = GetType(Integer)
        DataGridView001.Cols(22).Name = "MTMR002028"
        DataGridView001.Cols(22).AllowSorting = False
        DataGridView001.Cols(22).Style.Format = "N4"
        DataGridView001.Cols(22).Width = 90
        DataGridView001.Cols(22).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(22).AllowEditing = False
        DataGridView001.Cols(22).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 22, newStyle1)
        DataGridView001.SetCellStyle(1, 22, "checkBoxStyle2")

        DataGridView001.Cols(23).Caption = "現粗利率"
        DataGridView001.Cols(23).DataType = GetType(Integer)
        DataGridView001.Cols(23).Name = "MTMR002029"
        DataGridView001.Cols(23).AllowSorting = False
        DataGridView001.Cols(23).Style.Format = "N1"
        DataGridView001.Cols(23).Width = 80
        DataGridView001.Cols(23).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(23).AllowEditing = False
        DataGridView001.Cols(23).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 23, newStyle1)
        DataGridView001.SetCellStyle(1, 23, "checkBoxStyle2")

        DataGridView001.Cols(24).Caption = "★新売単価"
        DataGridView001.Cols(24).DataType = GetType(Integer)
        DataGridView001.Cols(24).Name = "MTMR002030"
        DataGridView001.Cols(24).AllowSorting = False
        DataGridView001.Cols(24).Style.Format = "N4"
        DataGridView001.Cols(24).Width = 90
        DataGridView001.Cols(24).MaxLength = 14
        DataGridView001.Cols(24).Format = "###,###,##0.00"
        DataGridView001.Cols(24).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.SetCellStyle(0, 24, newStyle1)
        DataGridView001.SetCellStyle(1, 24, "checkBoxStyle2")

        DataGridView001.Cols(25).Caption = "新売㎡単価"
        DataGridView001.Cols(25).DataType = GetType(Integer)
        DataGridView001.Cols(25).Name = "MTMR002031"
        DataGridView001.Cols(25).AllowSorting = False
        DataGridView001.Cols(25).Style.Format = "N4"
        DataGridView001.Cols(24).Format = "###,###,##0.00"
        DataGridView001.Cols(25).Width = 90
        DataGridView001.Cols(25).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(25).AllowEditing = False
        DataGridView001.Cols(25).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 25, newStyle1)
        DataGridView001.SetCellStyle(1, 25, "checkBoxStyle2")

        DataGridView001.Cols(26).Caption = "売実施日"
        DataGridView001.Cols(26).DataType = GetType(DateTime)
        DataGridView001.Cols(26).Name = "MTMR002032"
        DataGridView001.Cols(26).AllowSorting = False
        DataGridView001.Cols(26).Style.Format = "yyyy/MM/dd"
        DataGridView001.Cols(26).Width = 120
        DataGridView001.Cols(26).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.SetCellStyle(0, 26, newStyle1)
        DataGridView001.SetCellStyle(1, 26, "checkBoxStyle2")

        DataGridView001.Cols(27).Caption = "新仕単価"
        DataGridView001.Cols(27).DataType = GetType(Integer)
        DataGridView001.Cols(27).Name = "MTMR002033"
        DataGridView001.Cols(27).AllowSorting = False
        DataGridView001.Cols(27).Style.Format = "N4"
        DataGridView001.Cols(27).Width = 80
        DataGridView001.Cols(27).MaxLength = 14
        DataGridView001.Cols(27).Format = "###,###,##0.00"
        DataGridView001.Cols(27).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.SetCellStyle(0, 27, newStyle1)
        DataGridView001.SetCellStyle(1, 27, "checkBoxStyle2")

        DataGridView001.Cols(28).Caption = "新仕㎡単価"
        DataGridView001.Cols(28).DataType = GetType(Integer)
        DataGridView001.Cols(28).Name = "MTMR002034"
        DataGridView001.Cols(28).AllowSorting = False
        DataGridView001.Cols(28).Style.Format = "N4"
        DataGridView001.Cols(28).Width = 90
        DataGridView001.Cols(28).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(28).AllowEditing = False
        DataGridView001.Cols(28).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 28, newStyle1)
        DataGridView001.SetCellStyle(1, 28, "checkBoxStyle2")

        DataGridView001.Cols(29).Caption = "仕実施日"
        DataGridView001.Cols(29).DataType = GetType(DateTime)
        DataGridView001.Cols(29).Name = "MTMR002035"
        DataGridView001.Cols(29).AllowSorting = False
        DataGridView001.Cols(29).Style.Format = "yyyy/MM/dd"
        DataGridView001.Cols(29).Width = 120
        DataGridView001.Cols(29).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.SetCellStyle(0, 29, newStyle1)
        DataGridView001.SetCellStyle(1, 29, "checkBoxStyle2")

        DataGridView001.Cols(30).Caption = "新粗利率"
        DataGridView001.Cols(30).DataType = GetType(Integer)
        DataGridView001.Cols(30).Name = "MTMR002036_ARARI"
        DataGridView001.Cols(30).Style.Format = "N1"
        DataGridView001.Cols(30).Width = 55
        DataGridView001.Cols(30).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(30).AllowEditing = False
        DataGridView001.Cols(30).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 30, newStyle3)
        DataGridView001.SetCellStyle(1, 30, "checkBoxStyle2")

        DataGridView001.Cols(31).Caption = "新粗利率UP"
        DataGridView001.Cols(31).DataType = GetType(Integer)
        DataGridView001.Cols(31).Name = "MTMR002036UP"
        DataGridView001.Cols(31).Style.Format = "N1"
        DataGridView001.Cols(31).Width = 55
        DataGridView001.Cols(31).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(31).AllowEditing = False
        DataGridView001.Cols(31).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 31, newStyle3)
        DataGridView001.SetCellStyle(1, 31, "checkBoxStyle2")

        DataGridView001.Cols(32).Caption = "値上率"
        DataGridView001.Cols(32).DataType = GetType(Integer)
        DataGridView001.Cols(32).Name = "MTMR002037"
        DataGridView001.Cols(32).AllowSorting = False
        DataGridView001.Cols(32).Style.Format = "N1"
        DataGridView001.Cols(32).Width = 55
        DataGridView001.Cols(32).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(32).AllowEditing = False
        DataGridView001.Cols(32).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 32, newStyle3)
        DataGridView001.SetCellStyle(1, 32, "checkBoxStyle2")

        DataGridView001.Cols(33).Caption = "仕入先コメント"
        DataGridView001.Cols(33).DataType = GetType(String)
        DataGridView001.Cols(33).Name = "MTMR002038"
        DataGridView001.Cols(33).AllowSorting = False
        DataGridView001.Cols(33).Width = 210
        DataGridView001.Cols(33).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(33).AllowEditing = False
        DataGridView001.Cols(33).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 33, newStyle2)
        DataGridView001.SetCellStyle(1, 33, "checkBoxStyle2")

        DataGridView001.Cols(34).Caption = "社内摘要"
        DataGridView001.Cols(34).DataType = GetType(String)
        DataGridView001.Cols(34).Name = "MTMR002045"
        DataGridView001.Cols(34).AllowSorting = False
        DataGridView001.Cols(34).Width = 260
        DataGridView001.Cols(34).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(34).AllowEditing = False
        DataGridView001.Cols(34).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 34, newStyle2)
        DataGridView001.SetCellStyle(1, 34, "checkBoxStyle2")

        DataGridView001.Cols(35).Caption = "発注摘要"
        DataGridView001.Cols(35).DataType = GetType(String)
        DataGridView001.Cols(35).Name = "MTMR002046"
        DataGridView001.Cols(35).AllowSorting = False
        DataGridView001.Cols(35).Width = 260
        DataGridView001.Cols(35).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(35).AllowEditing = False
        DataGridView001.Cols(35).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 35, newStyle2)
        DataGridView001.SetCellStyle(1, 35, "checkBoxStyle2")

        DataGridView001.Cols(36).Caption = "運賃摘要"
        DataGridView001.Cols(36).DataType = GetType(String)
        DataGridView001.Cols(36).Name = "MTMR002047"
        DataGridView001.Cols(36).AllowSorting = False
        DataGridView001.Cols(36).Width = 260
        DataGridView001.Cols(36).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(36).AllowEditing = False
        DataGridView001.Cols(36).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 36, newStyle2)
        DataGridView001.SetCellStyle(1, 36, "checkBoxStyle2")

        DataGridView001.Cols(37).Caption = "★最終売上日"
        DataGridView001.Cols(37).DataType = GetType(DateTime)
        DataGridView001.Cols(37).Name = "MTMR002049"
        DataGridView001.Cols(37).AllowSorting = False
        DataGridView001.Cols(37).Style.Format = "yyyy/MM/dd"
        DataGridView001.Cols(37).Width = 120
        DataGridView001.Cols(37).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(37).AllowEditing = False
        DataGridView001.Cols(37).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 37, newStyle1)
        DataGridView001.SetCellStyle(1, 37, "checkBoxStyle2")

        DataGridView001.Cols(38).Caption = "最終売上単価"
        DataGridView001.Cols(38).DataType = GetType(Integer)
        DataGridView001.Cols(38).Name = "MTMR002050"
        DataGridView001.Cols(38).AllowSorting = False
        DataGridView001.Cols(38).Style.Format = "N4"
        DataGridView001.Cols(38).Width = 80
        DataGridView001.Cols(38).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(38).AllowEditing = False
        DataGridView001.Cols(38).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 38, newStyle1)
        DataGridView001.SetCellStyle(1, 38, "checkBoxStyle2")

        DataGridView001.Cols(39).Caption = "最終売上数量"
        DataGridView001.Cols(39).DataType = GetType(Integer)
        DataGridView001.Cols(39).Name = "MTMR002051"
        DataGridView001.Cols(39).AllowSorting = False
        DataGridView001.Cols(39).Style.Format = "N4"
        DataGridView001.Cols(39).Width = 80
        DataGridView001.Cols(39).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(39).AllowEditing = False
        DataGridView001.Cols(39).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 39, newStyle1)
        DataGridView001.SetCellStyle(1, 39, "checkBoxStyle2")

        DataGridView001.Cols(40).Caption = "最終納品先"
        DataGridView001.Cols(40).DataType = GetType(String)
        DataGridView001.Cols(40).Name = "MTMR002052"
        DataGridView001.Cols(40).AllowSorting = False
        DataGridView001.Cols(40).Width = 260
        DataGridView001.Cols(40).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(40).AllowEditing = False
        DataGridView001.Cols(40).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 40, newStyle2)
        DataGridView001.SetCellStyle(1, 40, "checkBoxStyle2")

        DataGridView001.Cols(41).Caption = "ｶｯﾄ"
        DataGridView001.Cols(41).DataType = GetType(Boolean)
        DataGridView001.Cols(41).Name = "MTMR002084"
        DataGridView001.Cols(41).Width = 40
        DataGridView001.Cols(41).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(41).AllowEditing = False
        DataGridView001.Cols(41).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 41, newStyle2)
        DataGridView001.SetCellStyle(1, 41, "checkBoxStyle2")

        DataGridView001.Cols(42).Caption = "★納品先履歴"
        DataGridView001.Cols(42).DataType = GetType(String)
        DataGridView001.Cols(42).Name = "MTMR002053"
        DataGridView001.Cols(42).AllowSorting = False
        DataGridView001.Cols(42).Width = 260
        DataGridView001.Cols(42).MaxLength = 102
        DataGridView001.Cols(42).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.SetCellStyle(0, 42, newStyle2)
        DataGridView001.SetCellStyle(1, 42, "checkBoxStyle2")

        DataGridView001.Cols(43).Caption = "数量"
        DataGridView001.Cols(43).DataType = GetType(Integer)
        DataGridView001.Cols(43).Name = "MTMR002039"
        DataGridView001.Cols(43).AllowSorting = False
        DataGridView001.Cols(43).Style.Format = "N4"
        DataGridView001.Cols(43).Width = 40
        DataGridView001.Cols(43).MaxLength = 14
        DataGridView001.Cols(43).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(43).AllowEditing = False
        DataGridView001.Cols(43).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 43, newStyle1)
        DataGridView001.SetCellStyle(1, 43, "checkBoxStyle2")

        DataGridView001.Cols(44).Caption = "売上回数"
        DataGridView001.Cols(44).DataType = GetType(Integer)
        DataGridView001.Cols(44).Name = "MTMR002040"
        DataGridView001.Cols(44).AllowSorting = False
        DataGridView001.Cols(44).Style.Format = "N4"
        DataGridView001.Cols(44).Width = 60
        DataGridView001.Cols(44).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(44).AllowEditing = False
        DataGridView001.Cols(44).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 44, newStyle1)
        DataGridView001.SetCellStyle(1, 44, "checkBoxStyle2")

        DataGridView001.Cols(45).Caption = "売上金額"
        DataGridView001.Cols(45).DataType = GetType(Integer)
        DataGridView001.Cols(45).Name = "MTMR002041"
        DataGridView001.Cols(45).AllowSorting = False
        DataGridView001.Cols(45).Style.Format = "N4"
        DataGridView001.Cols(45).Width = 60
        DataGridView001.Cols(45).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(45).AllowEditing = False
        DataGridView001.Cols(45).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 45, newStyle1)
        DataGridView001.SetCellStyle(1, 45, "checkBoxStyle2")

        DataGridView001.Cols(46).Caption = "粗利金額"
        DataGridView001.Cols(46).DataType = GetType(Integer)
        DataGridView001.Cols(46).Name = "MTMR002042"
        DataGridView001.Cols(46).AllowSorting = False
        DataGridView001.Cols(46).Style.Format = "N4"
        DataGridView001.Cols(46).Width = 60
        DataGridView001.Cols(46).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(46).AllowEditing = False
        DataGridView001.Cols(46).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 46, newStyle1)
        DataGridView001.SetCellStyle(1, 46, "checkBoxStyle2")

        DataGridView001.Cols(47).Caption = "手配"
        DataGridView001.Cols(47).DataType = GetType(String)
        DataGridView001.Cols(47).Name = "MTMR002021"
        DataGridView001.Cols(47).AllowSorting = False
        DataGridView001.Cols(47).Width = 50
        DataGridView001.Cols(47).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(47).AllowEditing = False
        DataGridView001.Cols(47).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 47, newStyle2)
        DataGridView001.SetCellStyle(1, 47, "checkBoxStyle2")

        DataGridView001.Cols(48).Caption = "★見積書商品名"
        DataGridView001.Cols(48).DataType = GetType(String)
        DataGridView001.Cols(48).Name = "MTMR002048"
        DataGridView001.Cols(48).AllowSorting = False
        DataGridView001.Cols(48).Width = 260
        DataGridView001.Cols(48).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(48).AllowEditing = False
        DataGridView001.Cols(48).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 48, newStyle2)
        DataGridView001.SetCellStyle(1, 48, "checkBoxStyle2")

        DataGridView001.Cols(49).Caption = "★得意先FAX"
        DataGridView001.Cols(49).DataType = GetType(String)
        DataGridView001.Cols(49).Name = "MTMR002054"
        DataGridView001.Cols(49).Visible = False
        DataGridView001.Cols(49).AllowSorting = False
        DataGridView001.Cols(49).Width = 70
        DataGridView001.Cols(49).MaxLength = 15
        DataGridView001.Cols(49).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(49).AllowEditing = False
        DataGridView001.Cols(49).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 49, newStyle2)
        DataGridView001.SetCellStyle(1, 49, "checkBoxStyle2")

        DataGridView001.Cols(50).Caption = "★備考"
        DataGridView001.Cols(50).DataType = GetType(String)
        DataGridView001.Cols(50).Name = "MTMR002072"
        DataGridView001.Cols(50).AllowSorting = False
        DataGridView001.Cols(50).Width = 260
        DataGridView001.Cols(50).MaxLength = 50
        DataGridView001.Cols(50).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.SetCellStyle(0, 50, newStyle2)
        DataGridView001.SetCellStyle(1, 50, "checkBoxStyle2")

        DataGridView001.Cols(51).Caption = "メール"
        DataGridView001.Cols(51).DataType = GetType(Boolean)
        DataGridView001.Cols(51).Name = "MTMR002076MAIL"
        DataGridView001.Cols(51).AllowSorting = False
        DataGridView001.Cols(51).Width = 60
        DataGridView001.Cols(51).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.SetCellStyle(0, 51, newStyle2)
        DataGridView001.SetCellStyle(1, 51, "checkBoxStyle1")

        DataGridView001.Cols(52).Caption = "ＦＡＸ"
        DataGridView001.Cols(52).DataType = GetType(Boolean)
        DataGridView001.Cols(52).Name = "MTMR002076FAX"
        DataGridView001.Cols(52).AllowSorting = False
        DataGridView001.Cols(52).Width = 60
        DataGridView001.Cols(52).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.SetCellStyle(0, 52, newStyle2)
        DataGridView001.SetCellStyle(1, 52, "checkBoxStyle1")

        DataGridView001.Cols(53).Caption = "★宛先名"
        DataGridView001.Cols(53).DataType = GetType(String)
        DataGridView001.Cols(53).Name = "MTMR002079"
        DataGridView001.Cols(53).AllowSorting = False
        DataGridView001.Cols(53).Width = 260
        DataGridView001.Cols(53).MaxLength = 36
        DataGridView001.Cols(53).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.SetCellStyle(0, 53, newStyle2)
        DataGridView001.SetCellStyle(1, 53, "checkBoxStyle2")

        DataGridView001.Cols(54).Caption = "メール宛先名"
        DataGridView001.Cols(54).DataType = GetType(String)
        DataGridView001.Cols(54).Name = "MTMR002078"
        DataGridView001.Cols(54).AllowSorting = False
        DataGridView001.Cols(54).Width = 260
        DataGridView001.Cols(54).MaxLength = 200
        DataGridView001.Cols(54).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.SetCellStyle(0, 54, newStyle2)
        DataGridView001.SetCellStyle(1, 54, "checkBoxStyle2")

        DataGridView001.Cols(55).Caption = "★ＦＡＸ宛先"
        DataGridView001.Cols(55).DataType = GetType(String)
        DataGridView001.Cols(55).Name = "MTMR002077"
        DataGridView001.Cols(55).AllowSorting = False
        DataGridView001.Cols(55).Width = 130
        DataGridView001.Cols(55).MaxLength = 20
        DataGridView001.Cols(55).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.SetCellStyle(0, 55, newStyle2)
        DataGridView001.SetCellStyle(1, 55, "checkBoxStyle2")

        DataGridView001.Cols(56).Caption = "最終送信日"
        DataGridView001.Cols(56).DataType = GetType(DateTime)
        DataGridView001.Cols(56).Name = "MTMR002087"
        DataGridView001.Cols(56).AllowSorting = False
        'DataGridView001.Cols(56).Style.Format = "yyyy/MM/dd"
        DataGridView001.Cols(56).Width = 120
        DataGridView001.Cols(56).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(56).AllowEditing = False
        DataGridView001.Cols(56).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 56, newStyle1)
        DataGridView001.SetCellStyle(1, 56, "checkBoxStyle2")

        DataGridView001.Cols(57).Caption = "価格入力番号"
        DataGridView001.Cols(57).DataType = GetType(String)
        DataGridView001.Cols(57).Name = "MTMR002080"
        DataGridView001.Cols(57).AllowSorting = False
        DataGridView001.Cols(57).Visible = False
        DataGridView001.Cols(57).Width = 130
        DataGridView001.Cols(57).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(57).AllowEditing = False
        DataGridView001.Cols(57).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 57, newStyle2)
        DataGridView001.SetCellStyle(1, 57, "checkBoxStyle2")

        DataGridView001.Cols(58).Caption = "改定後売上金額"
        DataGridView001.Cols(58).DataType = GetType(String)
        DataGridView001.Cols(58).Name = "MTMR002043"
        DataGridView001.Cols(58).AllowSorting = False
        DataGridView001.Cols(58).Visible = False
        DataGridView001.Cols(58).Width = 130
        DataGridView001.Cols(58).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(58).AllowEditing = False
        DataGridView001.Cols(58).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 58, newStyle2)
        DataGridView001.SetCellStyle(1, 58, "checkBoxStyle2")

        DataGridView001.Cols(59).Caption = "改定後粗利金額"
        DataGridView001.Cols(59).DataType = GetType(String)
        DataGridView001.Cols(59).Name = "MTMR002044"
        DataGridView001.Cols(59).AllowSorting = False
        DataGridView001.Cols(59).Visible = False
        DataGridView001.Cols(59).Width = 130
        DataGridView001.Cols(59).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(59).AllowEditing = False
        DataGridView001.Cols(59).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 59, newStyle2)
        DataGridView001.SetCellStyle(1, 59, "checkBoxStyle2")

        DataGridView001.Cols(60).Caption = "㎡計算"
        DataGridView001.Cols(60).DataType = GetType(String)
        DataGridView001.Cols(60).Name = "MTMR002069"
        DataGridView001.Cols(60).AllowSorting = False
        DataGridView001.Cols(60).Visible = False
        DataGridView001.Cols(60).Width = 80
        DataGridView001.Cols(60).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(60).AllowEditing = False
        DataGridView001.Cols(60).Style.BackColor = Color.LightGray
        DataGridView001.SetCellStyle(0, 60, newStyle2)
        DataGridView001.SetCellStyle(1, 60, "checkBoxStyle2")


    End Sub

    ''' <summary>
    ''' データグリッド項目の表示・非表示設定
    ''' </summary>
    ''' <param name="visible"></param>
    Public Sub SetDataGridColumnVisibleV2(ByVal visible As Boolean)

        'Me.DataGridView001.Columns("MTMR002001").Visible = visible
        DataGridView001.Cols(6).Visible = visible
        'Me.DataGridView001.Columns("MTMR002009").Visible = visible
        DataGridView001.Cols(3).Visible = visible
        'Me.DataGridView001.Columns("MTMR002011").Visible = visible
        DataGridView001.Cols(4).Visible = visible
        'Me.DataGridView001.Columns("MTMR002002").Visible = visible
        DataGridView001.Cols(9).Visible = visible

        '実行管理テーブルの項目表示設定を反映する
        'チェックONなら常に表示（対象）
        '改定前売単価(MTMR002022)、改定前仕入単価(MTMR002023)、改定前粗利率(MTMR002024)、現売㎡単価(MTMR002026)、現仕㎡単価(MTMR002028)、新売㎡単価(MTMR002031)、新仕㎡単価(MTMR002034)
        If displayMTMR003011 Then
            'Me.DataGridView001.Columns("MTMR002022").Visible = True
            DataGridView001.Cols(16).Visible = True
        Else
            'Me.DataGridView001.Columns("MTMR002022").Visible = visible
            DataGridView001.Cols(16).Visible = visible
        End If
        If displayMTMR003014 Then
            'Me.DataGridView001.Columns("MTMR002023").Visible = True
            DataGridView001.Cols(17).Visible = True
        Else
            'Me.DataGridView001.Columns("MTMR002023").Visible = visible
            DataGridView001.Cols(17).Visible = visible
        End If
        If displayMTMR003017 Then
            'Me.DataGridView001.Columns("MTMR002024").Visible = True
            DataGridView001.Cols(18).Visible = True
        Else
            'Me.DataGridView001.Columns("MTMR002024").Visible = visible
            DataGridView001.Cols(18).Visible = visible
        End If
        If displayMTMR003012 Then
            'Me.DataGridView001.Columns("MTMR002026").Visible = True
            DataGridView001.Cols(20).Visible = True
        Else
            'Me.DataGridView001.Columns("MTMR002026").Visible = visible
            DataGridView001.Cols(20).Visible = visible
        End If
        If displayMTMR003015 Then
            'Me.DataGridView001.Columns("MTMR002028").Visible = True
            DataGridView001.Cols(22).Visible = True
        Else
            'Me.DataGridView001.Columns("MTMR002028").Visible = visible
            DataGridView001.Cols(22).Visible = visible
        End If
        If displayMTMR003013 Then
            'Me.DataGridView001.Columns("MTMR002031").Visible = True
            DataGridView001.Cols(25).Visible = True
        Else
            'Me.DataGridView001.Columns("MTMR002031").Visible = visible
            DataGridView001.Cols(25).Visible = visible
        End If
        If displayMTMR003016 Then
            'Me.DataGridView001.Columns("MTMR002034").Visible = True
            DataGridView001.Cols(28).Visible = True
        Else
            'Me.DataGridView001.Columns("MTMR002034").Visible = visible
            DataGridView001.Cols(28).Visible = visible
        End If

        'Me.DataGridView001.Columns("MTMR002039").Visible = visible
        DataGridView001.Cols(43).Visible = visible
        'Me.DataGridView001.Columns("MTMR002040").Visible = visible
        DataGridView001.Cols(44).Visible = visible
        'Me.DataGridView001.Columns("MTMR002041").Visible = visible
        DataGridView001.Cols(45).Visible = visible
        'Me.DataGridView001.Columns("MTMR002042").Visible = visible
        DataGridView001.Cols(46).Visible = visible
        'Me.DataGridView001.Columns("MTMR002048").Visible = visible
        DataGridView001.Cols(48).Visible = visible
    End Sub

    ''' <summary>
    ''' データの設定
    ''' </summary>
    Public Sub SetDataV2()
        Dim table As DataTable = Me.Biz02.GetKakaku(Me.SearchCondition)
        Me.DataGridView001.DataSource = table
        If table.Rows.Count = 0 Then
            Me.ButtonRegist.Enabled = False
        End If
        '--------------------------
        '数値項目に並べ控えのため差し替える
        Dim sortTable As DataTable = Me.DataGridView001.DataSource
        Dim sortRows As DataRow() = table.Select().Clone()
        Dim sortChangeTable As New DataTable()
        sortChangeTable = table.Clone()
        sortChangeTable.Columns("MTMR002036_ARARI").DataType = Type.GetType("System.Decimal")
        sortChangeTable.Columns("MTMR002036UP").DataType = Type.GetType("System.Decimal")
        For Each row As DataRow In sortRows
            sortChangeTable.ImportRow(row)
        Next
        Me.DataGridView001.DataSource = sortChangeTable
        '--------------------------
    End Sub
    ''' <summary>
    ''' 実行管理テーブルの表示項目設定を取得
    ''' </summary>
    Public Sub SetJikouHyouji()
        Dim table As DataTable = Me.Biz02.GetKakaku(Me.SearchCondition)
        Me.DataGridView001.DataSource = table
        'Me.DataGridView001.CurrentCell = Nothing
        If table.Rows.Count = 0 Then
            Me.ButtonRegist.Enabled = False
        End If
    End Sub
    ''' <summary>
    ''' フッターイベント
    ''' </summary>
    Public Sub SetFooterV2()
        Dim totalMTMR002039 As Decimal = 0  '数量
        Dim totalMTMR002041 As Decimal = 0  '売上金額合計
        Dim totalMTMR002030 As Decimal = 0  '新売上単価合計
        Dim totalMTMR002030_1 As Decimal = 0  '新売上単価合計
        Dim totalMTMR002033 As Decimal = 0  '新仕入単価合計
        Dim totalMTMR002051 As Decimal = 0  '最終売上数量合計
        Dim kaiteigoUriage As Decimal = 0   '改定後売上金額合計
        Dim totalMTMR002025 As Decimal = 0  '現売上単価合計
        Dim totalMTMR002027 As Decimal = 0  '現仕入単価合計
        Dim kaiteimaeArari As Decimal = 0   '改定前粗利率
        Dim totalMTMR002044 As Decimal = 0  '改定後粗利金額合計
        Dim totalMTMR002043 As Decimal = 0  '改定後売上金額合計
        Dim kaiteigoArari As Decimal = 0    '改定後粗利率
        Dim arariUp As Decimal = 0          '粗利率UP
        Dim table As DataTable = Me.DataGridView001.DataSource

        Dim calMTMR002030 As Decimal = 0    '改定後売上金額計算(新売単価)
        Dim calMTMR002039 As Decimal = 0    '改定後売上金額計算(数量）
        Dim calMTMR002025 As Decimal = 0    '改定前粗利率計算(現売単価)
        Dim calMTMR002027 As Decimal = 0    '改定前粗利率計算(現仕単価）
        Dim calMTMR002030_1 As Decimal = 0    '改定後粗利率計算(現売単価)
        Dim calMTMR002033 As Decimal = 0    '改定後粗利率計算(現仕単価）

        For Each row As DataRow In table.Rows
            '売上金額合計計算
            totalMTMR002041 += Decimal.Parse(row.Item("MTMR002041").ToString)
            '改定後売上金額計算
            If (Decimal.TryParse(row.Item("MTMR002030").ToString, calMTMR002030)) And
                (Decimal.TryParse(row.Item("MTMR002039").ToString, calMTMR002039)) Then
                If (calMTMR002030 > 0) Then
                    kaiteigoUriage += calMTMR002030 * calMTMR002039
                End If
            End If
            '改定前粗利率計算
            If (Decimal.TryParse(row.Item("MTMR002025").ToString, calMTMR002025)) And
                (Decimal.TryParse(row.Item("MTMR002027").ToString, calMTMR002027)) Then
                If (calMTMR002025 > 0) And (calMTMR002027 > 0) Then
                    totalMTMR002025 += calMTMR002025
                    totalMTMR002027 += calMTMR002027
                End If
            End If
            '改定後粗利率計算
            If (Decimal.TryParse(row.Item("MTMR002030").ToString, calMTMR002030_1)) And
                (Decimal.TryParse(row.Item("MTMR002033").ToString, calMTMR002033)) Then
                If (calMTMR002030_1 > 0) And (calMTMR002033 > 0) Then
                    totalMTMR002030_1 += calMTMR002030_1
                    totalMTMR002033 += calMTMR002033
                End If
            End If
        Next

        '改定前粗利率計算
        If (totalMTMR002025 <> 0) And (totalMTMR002027 <> 0) Then
            kaiteimaeArari = Math.Round((totalMTMR002025 - totalMTMR002027) / totalMTMR002025 * 100, 1, MidpointRounding.AwayFromZero)
        End If

        '改定後粗利率計算
        If (totalMTMR002030_1 <> 0) And (totalMTMR002033 <> 0) Then
            kaiteigoArari = Math.Round((totalMTMR002030_1 - totalMTMR002033) / totalMTMR002030_1 * 100, 1, MidpointRounding.AwayFromZero)
            'kaiteimaeArari = Math.Round((totalMTMR002025 - totalMTMR002027) / totalMTMR002025 * 100)
        End If


        '売上金額合計
        Me.TextBox006.Text = Format(totalMTMR002041, "#,0")
        '改定後売上金額合計
        Me.TextBox007.Text = Format(kaiteigoUriage, "#,0")
        '改定前粗利率合計
        Me.TextBox008.Text = Format(kaiteimaeArari, "#,0.0")
        '改定後粗利率合計
        Me.TextBox009.Text = Format(kaiteigoArari, "#,0.0")
        '粗利率UP
        arariUp = kaiteigoArari - kaiteimaeArari
        Me.TextBox010.Text = Format(arariUp, "#,0.0")
        If arariUp > 0 Then
            Me.TextBox010.ForeColor = Color.Black
            Me.TextBox010.BackColor = SystemColors.Control
        ElseIf arariUp < 0 Then
            Me.TextBox010.ForeColor = Color.Red
            Me.TextBox010.BackColor = SystemColors.Control
        End If
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
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckBox001_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox001.CheckedChanged
        Me.SetDataGridColumnVisibleV2(Me.CheckBox001.Checked)
        'DataGridView001.Font = New Font(“メイリオ”, 9)
        'DataGridView001.Columns(10).Frozen = True
    End Sub
    ''' <summary>
    ''' 登録ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonRegist_Click(sender As Object, e As EventArgs) Handles ButtonRegist.Click

        '編集のみ抽出の検証
        'グリッドの編集を確定させる
        'Me.DataGridView001.FinishEditing()

        ''DataTableの変更行（追加・更新・削除）のみを取得
        'Dim dataTable As DataTable = Me.DataGridView001.DataSource
        'Dim changesTable As DataTable = dataTable.GetChanges()
        'End

        Dim updateErrorList = Me.Biz02.Update(Me.DataGridView001.DataSource)
        If updateErrorList.Count > 0 Then
            For Each errorMessage As String In updateErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        MessageBox.Show("登録しました")
    End Sub
    ''' <summary>
    ''' グリッド行選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        Dim formSelect As New FormMTMSelect

        'If (Not IsNothing(DataGridView001.CurrentCell)) Then
        '    Dim intSelectRow As Integer
        '    intSelectRow = DataGridView001.CurrentCell.RowIndex

        '    If (intSelectRow >= 0) Then
        '        formSelect.selectMTMR002001 = DataGridView001.CurrentRow.Cells("MTMR002001").Value
        '        formSelect.selectMTMR002002 = DataGridView001.CurrentRow.Cells("MTMR002002").Value
        '        formSelect.selectMTMR002003 = DataGridView001.CurrentRow.Cells("MTMR002003").Value
        '    End If
        'End If

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
        'セルの列を確認
        'If (e.ColumnIndex > 0) Then
        '    If DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002036_ARARI" Then
        '        Dim val As Decimal = CDec(e.Value)
        '        'セルの値により、背景色を変更する
        '        If val > 0 Then
        '            e.CellStyle.ForeColor = Color.Black
        '        ElseIf val < 0 Then
        '            e.CellStyle.ForeColor = Color.Red
        '        End If
        '    End If
        '    If DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002036UP" Then
        '        Dim val As Decimal = CDec(e.Value)
        '        'セルの値により、背景色を変更する
        '        If val > 0 Then
        '            e.CellStyle.ForeColor = Color.Black
        '        ElseIf val < 0 Then
        '            e.CellStyle.ForeColor = Color.Red
        '        End If
        '    End If
        'End If
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
            Me.Form02.Show()
        Else
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' データグリッド一括チェック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellChecked(sender As Object, e As RowColEventArgs) Handles DataGridView001.CellChecked
        If e.Row = 1 AndAlso e.Col = 1 Then
            DataGridView001.SortDefinition = String.Empty
            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                If DataGridView001.Rows(row).Visible Then
                    DataGridView001.SetCellCheck(row, 1, DataGridView001.GetCellCheck(e.Row, e.Col))
                End If
            Next
        End If
        If e.Row = 1 AndAlso e.Col = 2 Then
            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                If DataGridView001.Rows(row).Visible Then
                    DataGridView001.SetCellCheck(row, 2, DataGridView001.GetCellCheck(e.Row, e.Col))
                End If
            Next
        End If
        If e.Row = 1 AndAlso e.Col = 51 Then
            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                If DataGridView001.Rows(row).Visible Then
                    DataGridView001.SetCellCheck(row, 51, DataGridView001.GetCellCheck(e.Row, e.Col))
                End If
            Next
            'メールが全てTrueならFAXは全てFalseにする（逆も処理も行う）
            Dim checkCol51 = DataGridView001.GetCellCheck(e.Row, e.Col)
            If (checkCol51 = CheckEnum.Checked) Then
                DataGridView001.SetCellCheck(1, 52, CheckEnum.Unchecked)
                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                    If DataGridView001.Rows(row).Visible Then
                        DataGridView001.SetCellCheck(row, 52, CheckEnum.Unchecked)
                    End If
                Next
            Else
                DataGridView001.SetCellCheck(1, 52, CheckEnum.Checked)
                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                    If DataGridView001.Rows(row).Visible Then
                        DataGridView001.SetCellCheck(row, 52, CheckEnum.Checked)
                    End If
                Next
            End If
        End If

        If e.Row = 1 AndAlso e.Col = 52 Then
            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                If DataGridView001.Rows(row).Visible Then
                    DataGridView001.SetCellCheck(row, 52, DataGridView001.GetCellCheck(e.Row, e.Col))
                End If
            Next
            'FAXが全てTrueならメールは全てFalseにする（逆も処理も行う）
            Dim checkCol52 = DataGridView001.GetCellCheck(e.Row, e.Col)
            If (checkCol52 = CheckEnum.Checked) Then
                DataGridView001.SetCellCheck(1, 51, CheckEnum.Unchecked)
                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                    If DataGridView001.Rows(row).Visible Then
                        DataGridView001.SetCellCheck(row, 51, CheckEnum.Unchecked)
                    End If
                Next
            Else
                DataGridView001.SetCellCheck(1, 51, CheckEnum.Checked)
                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
                    If DataGridView001.Rows(row).Visible Then
                        DataGridView001.SetCellCheck(row, 51, CheckEnum.Checked)
                    End If
                Next
            End If
        End If
        If e.Row > 0 AndAlso e.Col = 51 Then
            Dim checkCol51Val = DataGridView001.GetCellCheck(e.Row, e.Col)
            If (checkCol51Val = CheckEnum.Checked) Then
                DataGridView001.SetCellCheck(e.Row, 52, CheckEnum.Unchecked)
            Else
                DataGridView001.SetCellCheck(e.Row, 52, CheckEnum.Checked)
            End If
        End If
        If e.Row > 0 AndAlso e.Col = 52 Then
            Dim checkCol52Val = DataGridView001.GetCellCheck(e.Row, e.Col)
            If (checkCol52Val = CheckEnum.Checked) Then
                DataGridView001.SetCellCheck(e.Row, 51, CheckEnum.Unchecked)
            Else
                DataGridView001.SetCellCheck(e.Row, 51, CheckEnum.Checked)
            End If
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
        'ロット桁数
        If e.Col = 12 Then
            If Not DataGridView001.Editor Is Nothing Then
                If Not DataGridView001.Editor.Text Is Nothing Then
                    Dim strMTMR002017 = DataGridView001.Editor.Text
                    If (Not strMTMR002017 Is DBNull.Value) AndAlso (Not String.IsNullOrEmpty(strMTMR002017)) Then
                        If (Not Me.Biz02.StringNumberByteDigitsCheck(strMTMR002017, 12)) Then
                            MessageBox.Show("ロットが12バイトを超えています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            e.Cancel = True
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 編集前イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_BeforeEdit(sender As Object, e As RowColEventArgs) Handles DataGridView001.BeforeEdit
        'ロット
        If e.Col = 12 Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
        '新売単価
        If e.Col = 24 Then
            DataGridView001.ImeMode = ImeMode.Disable
        End If
        '新仕単価
        If e.Col = 27 Then
            DataGridView001.ImeMode = ImeMode.Disable
        End If
        '納品先履歴
        If e.Col = 42 Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
        '備考
        If e.Col = 50 Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
        '宛先名
        If e.Col = 53 Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
        'メール
        If e.Col = 54 Then
            DataGridView001.ImeMode = ImeMode.Disable
        End If
        'ＦＡＸ宛先
        If e.Col = 55 Then
            DataGridView001.ImeMode = ImeMode.Disable
        End If
    End Sub
    ''' <summary>
    ''' グリッドのセル値変更処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellChanged(sender As Object, e As RowColEventArgs) Handles DataGridView001.CellChanged
        Dim dt As Date

        If e.Row < DataGridView001.Rows.Fixed Then
            Return
        End If

        If e.Col = 24 Then
            'Dim htText = DataGridView001.HitTest()
            'If Not htText.Column = -1 Then
            Dim colMTMR002030 = DataGridView001(e.Row, 24).ToString()
            If Not colMTMR002030 Is DBNull.Value Then
                Dim formatVal As Decimal
                If (Decimal.TryParse(colMTMR002030, formatVal)) Then
                    DataGridView001.SetData(e.Row, e.Col, formatVal.ToString("0.00"))
                    DataGridView001.FinishEditing()
                    'Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002030").Value = formatVal.ToString("0.00")
                    Dim decMTMR002029 As Decimal = 0.00
                    Dim decMTMR002030 As Decimal = 0.00
                    Dim decMTMR002031 As Decimal = 0.00
                    Dim decMTMR002033 As Decimal = 0.00
                    Dim decMTMR002034 As Decimal = 0.00
                    Dim decMTMR002036_ARARI As Decimal = 0.0
                    Dim decMTMR002036UP As Decimal = 0.0
                    Dim decMTMR002069 As Decimal = 0.00
                    '新売㎡単価
                    If Decimal.TryParse(colMTMR002030, decMTMR002030) Then
                        If Decimal.TryParse(DataGridView001(e.Row, 60).ToString(), decMTMR002069) Then
                            If (Not decMTMR002030 = 0.00) And (Not decMTMR002069 = 0.00) Then
                                decMTMR002031 = Math.Round(decMTMR002030 / decMTMR002069, 2, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002031 = 0.0) Then
                        DataGridView001.SetData(e.Row, 25, decMTMR002031.ToString("0.00"))
                        DataGridView001.FinishEditing()
                    Else
                        DataGridView001.SetData(e.Row, 25, decMTMR002031.ToString("0.00"))
                        DataGridView001.FinishEditing()
                    End If
                    '新粗利率計算
                    If Decimal.TryParse(colMTMR002030, decMTMR002030) Then
                        If Decimal.TryParse(DataGridView001(e.Row, 27).ToString(), decMTMR002033) Then
                            If (Not decMTMR002030 = 0.00) And (Not decMTMR002033 = 0.00) Then
                                decMTMR002036_ARARI = Math.Round((decMTMR002030 - decMTMR002033) / decMTMR002030 * 100, 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036_ARARI = 0.0) Then
                        DataGridView001.SetData(e.Row, 30, decMTMR002036_ARARI.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    Else
                        DataGridView001.SetData(e.Row, 30, decMTMR002036_ARARI.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    End If
                    '新粗利率アップ計算
                    If (Not DataGridView001(e.Row, 23).ToString() Is DBNull.Value) Then
                        If (Decimal.TryParse(DataGridView001(e.Row, 23).ToString(), decMTMR002029)) Then
                            If (Not decMTMR002036_ARARI = 0.0) And (Not decMTMR002029 = 0.0) Then
                                decMTMR002036UP = Math.Round((decMTMR002036_ARARI - decMTMR002029), 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036UP = 0.0) Then
                        DataGridView001.SetData(e.Row, 31, decMTMR002036UP.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    Else
                        DataGridView001.SetData(e.Row, 31, decMTMR002036UP.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    End If
                End If
            End If
            'End If
        End If

        If e.Col = 27 Then
            'Dim htText = DataGridView001.HitTest()
            'If Not htText.Column = -1 Then
            Dim calMTMR002033 = DataGridView001(e.Row, e.Col).ToString()
            If Not calMTMR002033 Is DBNull.Value Then
                Dim formatVal As Decimal
                If (Decimal.TryParse(calMTMR002033, formatVal)) Then
                    DataGridView001.SetData(e.Row, e.Col, formatVal.ToString("0.00"))
                    DataGridView001.FinishEditing()
                    Dim decMTMR002027 As Decimal = 0.00
                    Dim decMTMR002029 As Decimal = 0.00
                    Dim decMTMR002030 As Decimal = 0.00
                    Dim decMTMR002033 As Decimal = 0.00
                    Dim decMTMR002034 As Decimal = 0.00
                    Dim decMTMR002036_ARARI As Decimal = 0.0
                    Dim decMTMR002036UP As Decimal = 0.0
                    Dim decMTMR002037 As Decimal = 0.0
                    Dim decMTMR002069 As Decimal = 0.00
                    '新仕㎡単価
                    If Decimal.TryParse(calMTMR002033, decMTMR002033) Then
                        If Decimal.TryParse(DataGridView001(e.Row, 60).ToString(), decMTMR002069) Then
                            If (Not decMTMR002033 = 0.00) And (Not decMTMR002069 = 0.00) Then
                                decMTMR002034 = Math.Round(decMTMR002033 / decMTMR002069, 2, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002034 = 0.0) Then
                        DataGridView001.SetData(e.Row, 28, decMTMR002034.ToString("0.00"))
                        DataGridView001.FinishEditing()
                    Else
                        DataGridView001.SetData(e.Row, 28, decMTMR002034.ToString("0.00"))
                        DataGridView001.FinishEditing()
                    End If
                    '新粗利率計算
                    If Decimal.TryParse(DataGridView001(e.Row, 24).ToString(), decMTMR002030) Then
                        If Decimal.TryParse(calMTMR002033, decMTMR002033) Then
                            If (Not decMTMR002030 = 0.00) And (Not decMTMR002033 = 0.00) Then
                                decMTMR002036_ARARI = Math.Round((decMTMR002030 - decMTMR002033) / decMTMR002030 * 100, 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036_ARARI = 0.0) Then
                        DataGridView001.SetData(e.Row, 30, decMTMR002036_ARARI.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    Else
                        DataGridView001.SetData(e.Row, 30, decMTMR002036_ARARI.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    End If
                    '新粗利率アップ計算
                    If (Not DataGridView001(e.Row, 23).ToString() Is DBNull.Value) Then
                        If (Decimal.TryParse(DataGridView001(e.Row, 23).ToString(), decMTMR002029)) Then
                            If (Not decMTMR002036_ARARI = 0.0) And (Not decMTMR002029 = 0.0) Then
                                decMTMR002036UP = Math.Round((decMTMR002036_ARARI - decMTMR002029), 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036UP = 0.0) Then
                        DataGridView001.SetData(e.Row, 31, decMTMR002036UP.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    Else
                        DataGridView001.SetData(e.Row, 31, decMTMR002036UP.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    End If
                    '値上率計算
                    If (Not DataGridView001(e.Row, 21).ToString() Is DBNull.Value) Then
                        If (Decimal.TryParse(DataGridView001(e.Row, 21).ToString(), decMTMR002027)) Then
                            If (Not decMTMR002027 = 0.0) And (Not decMTMR002033 = 0.0) Then
                                decMTMR002037 = Math.Round((decMTMR002033 / decMTMR002027) * 100, 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002037 = 0.0) Then
                        DataGridView001.SetData(e.Row, 32, decMTMR002037.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    Else
                        DataGridView001.SetData(e.Row, 32, decMTMR002037.ToString("0.0"))
                        DataGridView001.FinishEditing()
                    End If
                End If
            End If
            'End If
        End If

        '納品先履歴（過去18カ月）
        If e.Col = 42 Then
            If (Not DataGridView001(e.Row, e.Col).ToString() Is DBNull.Value) Then
                Dim comMTMR002052 = Me.Biz02.DeliveryConvert(DataGridView001(e.Row, e.Col).ToString())
                DataGridView001.SetData(e.Row, 42, comMTMR002052)
                DataGridView001.FinishEditing()
            End If
        End If

        '新粗利率、新粗利率UPの文字色変更
        If e.Row > 1 Then
            If e.Col = 30 Then
                Dim colMTMR002036_ARARI = DataGridView001(e.Row, e.Col).ToString()
                Dim val As Decimal = CDec(colMTMR002036_ARARI)
                'セルの値により、背景色を変更する
                If val > 0 Then
                    DataGridView001.Styles.Add("nmStyle1Black")
                    DataGridView001.Styles("nmStyle1Black").ForeColor = Color.Black
                    DataGridView001.SetCellStyle(e.Row, e.Col, "nmStyle1Black")
                ElseIf val < 0 Then
                    DataGridView001.Styles.Add("nmStyle1Red")
                    DataGridView001.Styles("nmStyle1Red").ForeColor = Color.Red
                    DataGridView001.SetCellStyle(e.Row, e.Col, "nmStyle1Red")
                End If
            End If
            If e.Col = 31 Then
                Dim colMTMR002036UP = DataGridView001(e.Row, e.Col).ToString()
                Dim val As Decimal = CDec(colMTMR002036UP)
                'セルの値により、背景色を変更する
                If val > 0 Then
                    DataGridView001.Styles.Add("nmStyle1Black")
                    DataGridView001.Styles("nmStyle1Black").ForeColor = Color.Black
                    DataGridView001.SetCellStyle(e.Row, e.Col, "nmStyle1Black")
                ElseIf val < 0 Then
                    DataGridView001.Styles.Add("nmStyle1Red")
                    DataGridView001.Styles("nmStyle1Red").ForeColor = Color.Red
                    DataGridView001.SetCellStyle(e.Row, e.Col, "nmStyle1Red")
                End If
            End If
        End If

        Me.SetFooterV2()
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
        If e.KeyCode = Keys.C AndAlso e.Control Then
            'Me.WindowState = FormWindowState.Normal
            'Me.WindowState = FormWindowState.Maximized
            'DataGridView001.Refresh()
            If (CheckBox001.Checked) Then
                CheckBox001.Checked = False
                CheckBox001.Checked = True
            Else
                CheckBox001.Checked = True
                CheckBox001.Checked = False
            End If
        End If

        '一括チェック(列ないのF2)
        'If e.KeyCode = Keys.F2 Then
        '    If (Not IsNothing(DataGridView001.RowSel)) Then
        '        Dim intSelectRow As Integer
        '        Dim intSelectCol As Integer
        '        intSelectRow = DataGridView001.RowSel
        '        intSelectCol = DataGridView001.ColSel
        '        If intSelectRow > 0 AndAlso intSelectCol = 1 Then
        '            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                DataGridView001.SetCellCheck(row, 1, DataGridView001.GetCellCheck(intSelectRow, intSelectCol))
        '            Next
        '        End If
        '        If intSelectRow > 0 AndAlso intSelectCol = 2 Then
        '            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                DataGridView001.SetCellCheck(row, 2, DataGridView001.GetCellCheck(intSelectRow, intSelectCol))
        '            Next
        '        End If
        '        If intSelectRow > 0 AndAlso intSelectCol = 51 Then
        '            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                DataGridView001.SetCellCheck(row, 51, DataGridView001.GetCellCheck(intSelectRow, intSelectCol))
        '            Next
        '            'メールが全てTrueならFAXは全てFalseにする（逆も処理も行う）
        '            Dim checkCol51 = DataGridView001.GetCellCheck(intSelectRow, intSelectCol)
        '            If (checkCol51 = CheckEnum.Checked) Then
        '                DataGridView001.SetCellCheck(1, 52, CheckEnum.Unchecked)
        '                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                    DataGridView001.SetCellCheck(row, 52, CheckEnum.Unchecked)
        '                Next
        '            Else
        '                DataGridView001.SetCellCheck(1, 52, CheckEnum.Checked)
        '                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                    DataGridView001.SetCellCheck(row, 52, CheckEnum.Checked)
        '                Next
        '            End If
        '        End If

        '        If intSelectRow > 0 AndAlso intSelectCol = 52 Then
        '            For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                DataGridView001.SetCellCheck(row, 52, DataGridView001.GetCellCheck(intSelectRow, intSelectCol))
        '            Next
        '            'FAXが全てTrueならメールは全てFalseにする（逆も処理も行う）
        '            Dim checkCol52 = DataGridView001.GetCellCheck(intSelectRow, intSelectCol)
        '            If (checkCol52 = CheckEnum.Checked) Then
        '                DataGridView001.SetCellCheck(1, 51, CheckEnum.Unchecked)
        '                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                    DataGridView001.SetCellCheck(row, 51, CheckEnum.Unchecked)
        '                Next
        '            Else
        '                DataGridView001.SetCellCheck(1, 51, CheckEnum.Checked)
        '                For row As Integer = DataGridView001.Rows.Fixed To DataGridView001.Rows.Count - 1
        '                    DataGridView001.SetCellCheck(row, 51, CheckEnum.Checked)
        '                Next
        '            End If
        '        End If
        '        If intSelectRow > 0 AndAlso intSelectCol = 51 Then
        '            Dim checkCol51Val = DataGridView001.GetCellCheck(intSelectRow, intSelectCol)
        '            If (checkCol51Val = CheckEnum.Checked) Then
        '                DataGridView001.SetCellCheck(intSelectRow, 52, CheckEnum.Unchecked)
        '            Else
        '                DataGridView001.SetCellCheck(intSelectRow, 52, CheckEnum.Checked)
        '            End If
        '        End If
        '        If intSelectRow > 0 AndAlso intSelectCol = 52 Then
        '            Dim checkCol52Val = DataGridView001.GetCellCheck(intSelectRow, intSelectCol)
        '            If (checkCol52Val = CheckEnum.Checked) Then
        '                DataGridView001.SetCellCheck(intSelectRow, 51, CheckEnum.Unchecked)
        '            Else
        '                DataGridView001.SetCellCheck(intSelectRow, 51, CheckEnum.Checked)
        '            End If
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub ButtonCopy_Click(sender As Object, e As EventArgs) Handles ButtonCopy.Click
        ' 選択したセル範囲のCellRangeオブジェクトを取得します
        Dim cr As C1.Win.C1FlexGrid.CellRange
        cr = DataGridView001.GetCellRange(0, 1, DataGridView001.Rows.Count - 1, DataGridView001.Cols.Count - 1)
        Dim StrCopy = ""
        '文字列内の改行対応
        'Dim StrValue = ""

        For i = cr.r1 To cr.r2
            If i <> 1 Then
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
            End If
        Next
        ' クリップボードに設定します
        Clipboard.SetDataObject(StrCopy)

        '文字列内の改行対応
        'For i = cr.r1 To cr.r2
        '    If i <> 1 Then
        '        If DataGridView001.Rows(i).Visible = True Then
        '            For j = cr.c1 To cr.c2
        '                If DataGridView001.Cols(j).Visible = True Then
        '                    StrValue = DataGridView001(i, j).ToString()
        '                    If InStr(1, StrValue, vbCrLf) > 0 Or InStr(1, StrValue, vbLf) > 0 Then
        '                        StrValue = Replace(StrValue, vbCr, " ")
        '                        StrValue = Replace(StrValue, vbLf, " ")
        '                        StrCopy = StrCopy & StrValue
        '                    Else
        '                        StrCopy = StrCopy & DataGridView001(i, j).ToString()
        '                    End If

        '                    If j <> cr.c2 Then
        '                        StrCopy = StrCopy & Microsoft.VisualBasic.Constants.vbTab
        '                    End If
        '                End If
        '            Next

        '            StrCopy = StrCopy & Microsoft.VisualBasic.Constants.vbLf
        '        End If
        '    End If
        'Next
        '' クリップボードに設定します
        Clipboard.SetDataObject(StrCopy)

        'DataGridView001.ClipboardCopyMode = ClipboardCopyModeEnum.DataAndAllHeaders
        ''DataGridView001.Rows.Remove(1)
        'Dim cr As CellRange = DataGridView001.GetCellRange(0, 1, DataGridView001.Rows.Count - 1, DataGridView001.Cols.Count - 1)
        'Dim obj As Object = cr.Clip
        'Clipboard.SetDataObject(obj, True)
        'DataGridView001.ClipboardCopyMode = ClipboardCopyModeEnum.DataOnly
        ''DataGridView001.SaveExcel("C:\Temp\test.xlsx", FileFlags.AsDisplayed Or FileFlags.AsDisplayed)
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

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub
End Class

