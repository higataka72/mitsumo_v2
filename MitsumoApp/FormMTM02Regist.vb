Imports System.Configuration
Imports MitsumoLib

Public Class FormMTM02Regist

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
        DataGridView001.Font = New Font(“メイリオ”, 9)
        Me.SetHeader()
        Me.SetDataGridColumn()
        Me.SetDataGridColumnVisible(False)
        Me.SetData()
        Me.SetFooter()
        DataGridView001.Columns(10).Frozen = True
        Me.CheckBox0001.Checked = False
        DataGridView001.ScrollBars = ScrollBars.None
        DataGridView001.ScrollBars = ScrollBars.Both
        Me.WindowState = FormWindowState.Maximized
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
    Private Sub SetDataGridColumn()
        Me.DataGridView001.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Dim column001 As New DataGridViewCheckBoxColumn With {
            .HeaderText = "確定",
            .DataPropertyName = "MTMR002085",
            .Name = "MTMR002085"
        }
        column001.Width = 30
        column001.HeaderCell.Style.Font = New Font("メイリオ", 7, FontStyle.Underline)
        column001.HeaderCell.Style.ForeColor = Color.White
        column001.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column001.SortMode = DataGridViewColumnSortMode.Automatic
        Me.DataGridView001.Columns.Add(column001)
        Dim column002 As New DataGridViewCheckBoxColumn With {
            .HeaderText = "印刷なし",
            .DataPropertyName = "MTMR002086",
            .Name = "MTMR002086"
        }
        column002.Width = 30
        column002.HeaderCell.Style.Font = New Font("メイリオ", 7, FontStyle.Regular)
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        column002.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column002.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
            .HeaderText = "営業所",
            .DataPropertyName = "MTMR002009",
            .Name = "MTMR002009"
        }
        column003.Width = 80
        column003.ReadOnly = True
        column003.DefaultCellStyle.BackColor = Color.LightGray
        column003.SortMode = DataGridViewColumnSortMode.NotSortable
        column003.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column003.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column003)
        Dim column004 As New DataGridViewTextBoxColumn With {
            .HeaderText = "部課",
            .DataPropertyName = "MTMR002011",
            .Name = "MTMR002011"
        }
        column004.Width = 150
        column004.ReadOnly = True
        column004.DefaultCellStyle.BackColor = Color.LightGray
        column004.SortMode = DataGridViewColumnSortMode.NotSortable
        column004.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column004.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column004)
        Dim column005 As New DataGridViewTextBoxColumn With {
            .HeaderText = "担当者",
            .DataPropertyName = "MTMR002013",
            .Name = "MTMR002013"
        }
        column005.Width = 80
        column005.ReadOnly = True
        column005.DefaultCellStyle.BackColor = Color.LightGray
        column005.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        column005.HeaderCell.Style.ForeColor = Color.White
        column005.HeaderCell.Style.BackColor = Color.LightSeaGreen
        Me.DataGridView001.Columns.Add(column005)
        Dim column006_0 As New DataGridViewTextBoxColumn With {
            .HeaderText = "得意先コード",
            .DataPropertyName = "MTMR002001",
            .Name = "MTMR002001"
        }
        column006_0.Width = 90
        column006_0.ReadOnly = True
        column006_0.DefaultCellStyle.BackColor = Color.LightGray
        column006_0.SortMode = DataGridViewColumnSortMode.NotSortable
        column006_0.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column006_0.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column006_0)
        Dim column006 As New DataGridViewTextBoxColumn With {
            .HeaderText = "★得意先名",
            .DataPropertyName = "MTMR002005",
            .Name = "MTMR002005"
        }
        column006.Width = 230
        column006.ReadOnly = True
        column006.DefaultCellStyle.BackColor = Color.LightGray
        column006.SortMode = DataGridViewColumnSortMode.NotSortable
        column006.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column006.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column006)
        Dim column007 As New DataGridViewTextBoxColumn With {
            .HeaderText = "ﾗﾝｸ",
            .DataPropertyName = "MTMR002007",
            .Name = "MTMR002007"
        }
        column007.Width = 40
        column007.ReadOnly = True
        column007.DefaultCellStyle.BackColor = Color.LightGray
        column007.HeaderCell.Style.Font = New Font("メイリオ", 11, FontStyle.Underline)
        column007.HeaderCell.Style.ForeColor = Color.White
        column007.HeaderCell.Style.BackColor = Color.LightSeaGreen
        Me.DataGridView001.Columns.Add(column007)
        Dim column008 As New DataGridViewTextBoxColumn With {
            .HeaderText = "商品C",
            .DataPropertyName = "MTMR002002",
            .Name = "MTMR002002"
        }
        column008.Width = 90
        column008.ReadOnly = True
        column008.DefaultCellStyle.BackColor = Color.LightGray
        column008.SortMode = DataGridViewColumnSortMode.NotSortable
        column008.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column008.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column008)
        Dim column009 As New DataGridViewTextBoxColumn With {
            .HeaderText = "商品名",
            .DataPropertyName = "MTMR002016",
            .Name = "MTMR002016"
        }
        column009.Width = 230
        column009.ReadOnly = True
        column009.DefaultCellStyle.BackColor = Color.LightGray
        column009.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        column009.HeaderCell.Style.ForeColor = Color.White
        column009.HeaderCell.Style.BackColor = Color.LightSeaGreen
        Me.DataGridView001.Columns.Add(column009)
        Dim column010 As New DataGridViewTextBoxColumn With {
            .HeaderText = "規格",
            .DataPropertyName = "MTMR002003",
            .Name = "MTMR002003"
        }
        column010.Width = 65
        column010.ReadOnly = True
        column010.DefaultCellStyle.BackColor = Color.LightGray
        column010.SortMode = DataGridViewColumnSortMode.NotSortable
        column010.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column010.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column010)
        Dim column011 As New DataGridViewTextBoxColumn With {
            .HeaderText = "★ロット",
            .DataPropertyName = "MTMR002017",
            .Name = "MTMR002017",
            .MaxInputLength = 12
        }
        column011.Width = 80
        'column011.DefaultCellStyle.BackColor = Color.LightSteelBlue
        column011.SortMode = DataGridViewColumnSortMode.NotSortable
        column011.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column011.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column011)
        Dim column012 As New DataGridViewTextBoxColumn With {
            .HeaderText = "数量・上限",
            .DataPropertyName = "MTMR002004",
            .Name = "MTMR002004"
        }
        column012.Width = 80
        column012.ReadOnly = True
        column012.DefaultCellStyle.BackColor = Color.LightGray
        column012.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column012.DefaultCellStyle.Format = "#,##0"
        column012.SortMode = DataGridViewColumnSortMode.NotSortable
        'column012.SortMode = DataGridViewColumnSortMode.Automatic
        'column012.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        column012.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column012.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column012)
        Dim column013 As New DataGridViewTextBoxColumn With {
            .HeaderText = "★入数",
            .DataPropertyName = "MTMR002019",
            .Name = "MTMR002019"
        }
        column013.Width = 50
        column013.ReadOnly = True
        column013.DefaultCellStyle.BackColor = Color.LightGray
        column013.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column013.DefaultCellStyle.Format = "N4"
        column013.SortMode = DataGridViewColumnSortMode.NotSortable
        column013.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column013.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column013)
        Dim column014 As New DataGridViewTextBoxColumn With {
            .HeaderText = "★単位",
            .DataPropertyName = "MTMR002020",
            .Name = "MTMR002020"
        }
        column014.Width = 50
        column014.ReadOnly = True
        column014.DefaultCellStyle.BackColor = Color.LightGray
        column014.SortMode = DataGridViewColumnSortMode.NotSortable
        column014.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column014.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column014)
        Dim column015 As New DataGridViewTextBoxColumn With {
            .HeaderText = "改定前売単価",
            .DataPropertyName = "MTMR002022",
            .Name = "MTMR002022"
        }
        column015.Width = 100
        column015.ReadOnly = True
        column015.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Regular)
        column015.DefaultCellStyle.BackColor = Color.LightGray
        column015.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column015.DefaultCellStyle.Format = "N4"
        column015.SortMode = DataGridViewColumnSortMode.NotSortable
        column015.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column015.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column015)
        Dim column016 As New DataGridViewTextBoxColumn With {
            .HeaderText = "改定前仕単価",
            .DataPropertyName = "MTMR002023",
            .Name = "MTMR002023"
        }
        column016.Width = 100
        column016.ReadOnly = True
        column016.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Regular)
        column016.DefaultCellStyle.BackColor = Color.LightGray
        column016.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column016.DefaultCellStyle.Format = "N4"
        column016.SortMode = DataGridViewColumnSortMode.NotSortable
        column016.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column016.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column016)
        Dim column017 As New DataGridViewTextBoxColumn With {
            .HeaderText = "改定前粗利率",
            .DataPropertyName = "MTMR002024",
            .Name = "MTMR002024"
        }
        column017.Width = 100
        column017.ReadOnly = True
        column017.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Regular)
        column017.DefaultCellStyle.BackColor = Color.LightGray
        column017.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column017.DefaultCellStyle.Format = "N1"
        column017.SortMode = DataGridViewColumnSortMode.NotSortable
        column017.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column017.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column017)
        Dim column018 As New DataGridViewTextBoxColumn With {
            .HeaderText = "★現売単価",
            .DataPropertyName = "MTMR002025",
            .Name = "MTMR002025"
        }
        column018.Width = 80
        column018.ReadOnly = True
        column018.DefaultCellStyle.BackColor = Color.LightGray
        column018.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column018.DefaultCellStyle.Format = "N4"
        column018.SortMode = DataGridViewColumnSortMode.NotSortable
        column018.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column018.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column018)
        Dim column019 As New DataGridViewTextBoxColumn With {
            .HeaderText = "現売㎡単価",
            .DataPropertyName = "MTMR002026",
            .Name = "MTMR002026"
        }
        column019.Width = 80
        column019.ReadOnly = True
        column019.DefaultCellStyle.BackColor = Color.LightGray
        column019.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column019.DefaultCellStyle.Format = "N4"
        column019.SortMode = DataGridViewColumnSortMode.NotSortable
        column019.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column019.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column019)
        Dim column020 As New DataGridViewTextBoxColumn With {
            .HeaderText = "現仕単価",
            .DataPropertyName = "MTMR002027",
            .Name = "MTMR002027"
        }
        column020.Width = 80
        column020.ReadOnly = True
        column020.DefaultCellStyle.BackColor = Color.LightGray
        column020.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column020.DefaultCellStyle.Format = "N4"
        column020.SortMode = DataGridViewColumnSortMode.NotSortable
        column020.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column020.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column020)
        Dim column021 As New DataGridViewTextBoxColumn With {
            .HeaderText = "現仕㎡単価",
            .DataPropertyName = "MTMR002028",
            .Name = "MTMR002028"
        }
        column021.Width = 80
        column021.ReadOnly = True
        column021.DefaultCellStyle.BackColor = Color.LightGray
        column021.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column021.DefaultCellStyle.Format = "N4"
        column021.SortMode = DataGridViewColumnSortMode.NotSortable
        column021.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column021.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column021)
        Dim column022 As New DataGridViewTextBoxColumn With {
            .HeaderText = "現粗利率",
            .DataPropertyName = "MTMR002029",
            .Name = "MTMR002029"
        }
        column022.Width = 80
        column022.ReadOnly = True
        column022.DefaultCellStyle.BackColor = Color.LightGray
        column022.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column022.DefaultCellStyle.Format = "N1"
        column022.SortMode = DataGridViewColumnSortMode.NotSortable
        column022.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column022.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column022)
        Dim column023 As New DataGridViewTextBoxColumn With {
            .HeaderText = "★新売単価",
            .DataPropertyName = "MTMR002030",
            .Name = "MTMR002030",
            .MaxInputLength = 12
        }
        column023.Width = 80
        column023.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'column023.DefaultCellStyle.BackColor = Color.LightSteelBlue
        'column023.DefaultCellStyle.Format = "N4"
        column023.SortMode = DataGridViewColumnSortMode.NotSortable
        column023.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column023.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column023)
        Dim column024 As New DataGridViewTextBoxColumn With {
            .HeaderText = "新売㎡単価",
            .DataPropertyName = "MTMR002031",
            .Name = "MTMR002031",
            .MaxInputLength = 9
        }
        column024.Width = 80
        column024.ReadOnly = True
        column024.DefaultCellStyle.BackColor = Color.LightGray
        column024.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column024.DefaultCellStyle.Format = "N4"
        column024.SortMode = DataGridViewColumnSortMode.NotSortable
        column024.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column024.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column024)
        Dim column025 As New DataGridViewTextBoxColumn With {
            .HeaderText = "売実施日",
            .DataPropertyName = "MTMR002032",
            .Name = "MTMR002032",
            .MaxInputLength = 10
        }
        'Dim column025 As New DataGridViewTextBoxColumn With {
        '    .HeaderText = "売実施日",
        '    .DataPropertyName = "MTMR002032",
        '    .Name = "MTMR002032"
        '}
        column025.Width = 100
        column025.SortMode = DataGridViewColumnSortMode.NotSortable
        column025.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column025.HeaderCell.Style.ForeColor = Color.White
        column025.DefaultCellStyle.Format = "yyyy/MM/dd"
        Me.DataGridView001.Columns.Add(column025)
        Dim column026 As New DataGridViewTextBoxColumn With {
            .HeaderText = "新仕単価",
            .DataPropertyName = "MTMR002033",
            .Name = "MTMR002033",
            .MaxInputLength = 12
        }
        column026.Width = 80
        column026.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column026.DefaultCellStyle.Format = "N4"
        column026.SortMode = DataGridViewColumnSortMode.NotSortable
        column026.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column026.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column026)
        Dim column027 As New DataGridViewTextBoxColumn With {
            .HeaderText = "新仕㎡単価",
            .DataPropertyName = "MTMR002034",
            .Name = "MTMR002034",
            .MaxInputLength = 20
        }
        column027.Width = 80
        column027.ReadOnly = True
        column027.DefaultCellStyle.BackColor = Color.LightGray
        column027.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column027.DefaultCellStyle.Format = "N4"
        column027.SortMode = DataGridViewColumnSortMode.NotSortable
        column027.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column027.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column027)
        Dim column028 As New DataGridViewTextBoxColumn With {
            .HeaderText = "仕実施日",
            .DataPropertyName = "MTMR002035",
            .Name = "MTMR002035",
            .MaxInputLength = 10
        }
        column028.Width = 100
        column028.SortMode = DataGridViewColumnSortMode.NotSortable
        column028.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column028.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column028)
        Dim column029 As New DataGridViewTextBoxColumn With {
            .HeaderText = "新粗利率",
            .DataPropertyName = "MTMR002036_ARARI",
            .Name = "MTMR002036_ARARI"
        }
        column029.Width = 50
        column029.ReadOnly = True
        column029.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Underline)
        column029.DefaultCellStyle.BackColor = Color.LightGray
        column029.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column029.DefaultCellStyle.Format = "N1"
        column029.HeaderCell.Style.ForeColor = Color.White
        column029.HeaderCell.Style.BackColor = Color.LightSeaGreen
        'column029.SortMode = DataGridViewColumnSortMode.Automatic
        Me.DataGridView001.Columns.Add(column029)
        Dim column030 As New DataGridViewTextBoxColumn With {
            .HeaderText = "新粗利率UP",
            .DataPropertyName = "MTMR002036UP",
            .Name = "MTMR002036UP"
        }
        column030.Width = 50
        column030.ReadOnly = True
        column030.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Underline)
        column030.DefaultCellStyle.BackColor = Color.LightGray
        column030.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column030.DefaultCellStyle.Format = "N1"
        column030.HeaderCell.Style.ForeColor = Color.White
        column030.HeaderCell.Style.BackColor = Color.LightSeaGreen
        'column030.SortMode = DataGridViewColumnSortMode.Programmatic
        Me.DataGridView001.Columns.Add(column030)
        Dim column030_1 As New DataGridViewTextBoxColumn With {
            .HeaderText = "値上率",
            .DataPropertyName = "MTMR002037",
            .Name = "MTMR002037"
        }
        column030_1.Width = 45
        column030_1.ReadOnly = True
        column030_1.DefaultCellStyle.BackColor = Color.LightGray
        column030_1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column030_1.DefaultCellStyle.Format = "N1"
        column030_1.SortMode = DataGridViewColumnSortMode.NotSortable
        column030_1.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column030_1.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column030_1)
        Dim column031 As New DataGridViewTextBoxColumn With {
            .HeaderText = "仕入先コメント",
            .DataPropertyName = "MTMR002038",
            .Name = "MTMR002038"
        }
        column031.Width = 200
        column031.ReadOnly = True
        column031.DefaultCellStyle.BackColor = Color.LightGray
        column031.SortMode = DataGridViewColumnSortMode.NotSortable
        column031.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column031.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column031)
        Dim column032 As New DataGridViewTextBoxColumn With {
            .HeaderText = "社内摘要",
            .DataPropertyName = "MTMR002045",
            .Name = "MTMR002045"
        }
        column032.Width = 250
        column032.ReadOnly = True
        column032.DefaultCellStyle.BackColor = Color.LightGray
        column032.SortMode = DataGridViewColumnSortMode.NotSortable
        column032.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column032.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column032)
        Dim column033 As New DataGridViewTextBoxColumn With {
            .HeaderText = "発注摘要",
            .DataPropertyName = "MTMR002046",
            .Name = "MTMR002046"
        }
        column033.Width = 250
        column033.ReadOnly = True
        column033.DefaultCellStyle.BackColor = Color.LightGray
        column033.SortMode = DataGridViewColumnSortMode.NotSortable
        column033.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column033.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column033)
        Dim column034 As New DataGridViewTextBoxColumn With {
            .HeaderText = "運賃摘要",
            .DataPropertyName = "MTMR002047",
            .Name = "MTMR002047"
        }
        column034.Width = 250
        column034.ReadOnly = True
        column034.DefaultCellStyle.BackColor = Color.LightGray
        column034.SortMode = DataGridViewColumnSortMode.NotSortable
        column034.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column034.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column034)
        Dim column035 As New DataGridViewTextBoxColumn With {
            .HeaderText = "★最終売上日",
            .DataPropertyName = "MTMR002049",
            .Name = "MTMR002049"
        }
        column035.Width = 100
        column035.ReadOnly = True
        column035.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Regular)
        column035.DefaultCellStyle.BackColor = Color.LightGray
        column035.SortMode = DataGridViewColumnSortMode.NotSortable
        column035.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column035.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column035)
        Dim column036 As New DataGridViewTextBoxColumn With {
            .HeaderText = "最終売上単価",
            .DataPropertyName = "MTMR002050",
            .Name = "MTMR002050"
        }
        column036.Width = 80
        column036.ReadOnly = True
        column036.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Regular)
        column036.DefaultCellStyle.BackColor = Color.LightGray
        column036.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column036.DefaultCellStyle.Format = "N4"
        column036.SortMode = DataGridViewColumnSortMode.NotSortable
        column036.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column036.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column036)
        Dim column037 As New DataGridViewTextBoxColumn With {
           .HeaderText = "最終売上数量",
           .DataPropertyName = "MTMR002051",
           .Name = "MTMR002051"
        }
        column037.Width = 80
        column037.ReadOnly = True
        column037.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Regular)
        column037.DefaultCellStyle.BackColor = Color.LightGray
        column037.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column037.DefaultCellStyle.Format = "N4"
        column037.SortMode = DataGridViewColumnSortMode.NotSortable
        column037.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column037.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column037)
        Dim column038 As New DataGridViewTextBoxColumn With {
           .HeaderText = "最終納品先",
           .DataPropertyName = "MTMR002052",
           .Name = "MTMR002052"
        }
        column038.Width = 250
        column038.ReadOnly = True
        column038.DefaultCellStyle.BackColor = Color.LightGray
        column038.SortMode = DataGridViewColumnSortMode.NotSortable
        column038.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column038.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column038)
        Dim column039 As New DataGridViewCheckBoxColumn With {
           .HeaderText = "ｶｯﾄ",
           .DataPropertyName = "MTMR002084",
           .Name = "MTMR002084"
        }
        column039.Width = 60
        column039.ReadOnly = True
        column039.DefaultCellStyle.BackColor = Color.LightGray
        column039.HeaderCell.Style.Font = New Font("メイリオ", 11, FontStyle.Underline)
        column039.HeaderCell.Style.ForeColor = Color.White
        column039.SortMode = DataGridViewColumnSortMode.Automatic
        column039.HeaderCell.Style.BackColor = Color.LightSeaGreen
        Me.DataGridView001.Columns.Add(column039)
        Dim column040 As New DataGridViewTextBoxColumn With {
           .HeaderText = "★納品先履歴",
           .DataPropertyName = "MTMR002053",
           .Name = "MTMR002053",
           .MaxInputLength = 102
        }
        column040.Width = 250
        'column040.ReadOnly = True
        column040.SortMode = DataGridViewColumnSortMode.NotSortable
        column040.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column040.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column040)
        Dim column041 As New DataGridViewTextBoxColumn With {
           .HeaderText = "数量",
           .DataPropertyName = "MTMR002039",
           .Name = "MTMR002039"
        }
        column041.Width = 50
        column041.ReadOnly = True
        column041.DefaultCellStyle.BackColor = Color.LightGray
        column041.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column041.DefaultCellStyle.Format = "N4"
        column041.SortMode = DataGridViewColumnSortMode.NotSortable
        column041.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column041.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column041)
        Dim column042 As New DataGridViewTextBoxColumn With {
           .HeaderText = "売上回数",
           .DataPropertyName = "MTMR002040",
           .Name = "MTMR002040"
        }
        column042.Width = 60
        column042.ReadOnly = True
        column042.HeaderCell.Style.Font = New Font("メイリオ", 8, FontStyle.Regular)
        column042.DefaultCellStyle.BackColor = Color.LightGray
        column042.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column042.DefaultCellStyle.Format = "N4"
        column042.SortMode = DataGridViewColumnSortMode.NotSortable
        column042.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column042.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column042)
        Dim column043 As New DataGridViewTextBoxColumn With {
           .HeaderText = "売上金額",
           .DataPropertyName = "MTMR002041",
           .Name = "MTMR002041"
        }
        column043.Width = 80
        column043.ReadOnly = True
        column043.DefaultCellStyle.BackColor = Color.LightGray
        column043.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column043.DefaultCellStyle.Format = "N4"
        column043.SortMode = DataGridViewColumnSortMode.NotSortable
        column043.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column043.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column043)
        Dim column044 As New DataGridViewTextBoxColumn With {
           .HeaderText = "粗利金額",
           .DataPropertyName = "MTMR002042",
           .Name = "MTMR002042"
        }
        column044.Width = 80
        column044.ReadOnly = True
        column044.DefaultCellStyle.BackColor = Color.LightGray
        column044.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column044.DefaultCellStyle.Format = "N4"
        column044.SortMode = DataGridViewColumnSortMode.NotSortable
        column044.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column044.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column044)
        Dim column045 As New DataGridViewTextBoxColumn With {
           .HeaderText = "手配",
           .DataPropertyName = "MTMR002021",
           .Name = "MTMR002021"
        }
        column045.Width = 60
        column045.ReadOnly = True
        column045.DefaultCellStyle.BackColor = Color.LightGray
        column045.SortMode = DataGridViewColumnSortMode.NotSortable
        column045.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column045.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column045)
        Dim column046 As New DataGridViewTextBoxColumn With {
           .HeaderText = "★見積書商品名",
           .DataPropertyName = "MTMR002048",
           .Name = "MTMR002048"
        }
        column046.Width = 250
        column046.ReadOnly = True
        column046.DefaultCellStyle.BackColor = Color.LightGray
        column046.SortMode = DataGridViewColumnSortMode.NotSortable
        column046.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column046.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column046)
        Dim column047 As New DataGridViewTextBoxColumn With {
           .HeaderText = "★得意先FAX",
           .DataPropertyName = "MTMR002054",
           .Name = "MTMR002054",
           .MaxInputLength = 15
        }
        column047.Width = 120
        column047.Visible = False
        column047.SortMode = DataGridViewColumnSortMode.NotSortable
        column047.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column047.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column047)
        Dim column048 As New DataGridViewTextBoxColumn With {
           .HeaderText = "★備考",
           .DataPropertyName = "MTMR002072",
           .Name = "MTMR002072",
           .MaxInputLength = 50
        }
        column048.Width = 250
        column048.SortMode = DataGridViewColumnSortMode.NotSortable
        column048.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column048.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column048)
        Dim column049 As New DataGridViewCheckBoxColumn With {
           .HeaderText = "メール",
           .DataPropertyName = "MTMR002076MAIL",
           .Name = "MTMR002076MAIL"
        }
        column049.Width = 70
        column049.SortMode = DataGridViewColumnSortMode.NotSortable
        column049.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column049.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column049)
        Dim column050 As New DataGridViewCheckBoxColumn With {
           .HeaderText = "ＦＡＸ",
           .DataPropertyName = "MTMR002076FAX",
           .Name = "MTMR002076FAX"
        }
        column050.Width = 70
        column050.SortMode = DataGridViewColumnSortMode.NotSortable
        column050.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column050.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column050)
        Dim column051 As New DataGridViewTextBoxColumn With {
           .HeaderText = "★宛先名",
           .DataPropertyName = "MTMR002079",
           .Name = "MTMR002079",
           .MaxInputLength = 36
        }
        column051.Width = 250
        column051.SortMode = DataGridViewColumnSortMode.NotSortable
        column051.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column051.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column051)
        Dim column052 As New DataGridViewTextBoxColumn With {
           .HeaderText = "メール宛先",
           .DataPropertyName = "MTMR002078",
           .Name = "MTMR002078",
           .MaxInputLength = 200
        }
        column052.Width = 250
        column052.SortMode = DataGridViewColumnSortMode.NotSortable
        column052.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column052.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column052)
        Dim column053 As New DataGridViewTextBoxColumn With {
           .HeaderText = "★ＦＡＸ宛先",
           .DataPropertyName = "MTMR002077",
           .Name = "MTMR002077",
           .MaxInputLength = 20
        }
        column053.Width = 120
        column053.SortMode = DataGridViewColumnSortMode.NotSortable
        column053.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column053.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column053)
        Dim column054 As New DataGridViewTextBoxColumn With {
           .HeaderText = "最終送信日",
           .DataPropertyName = "MTMR002087",
           .Name = "MTMR002087"
        }
        column054.Width = 120
        column054.ReadOnly = True
        column054.DefaultCellStyle.BackColor = Color.LightGray
        column054.SortMode = DataGridViewColumnSortMode.NotSortable
        column054.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column054.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column054)
        'Dim column055 As New DataGridViewTextBoxColumn With {
        '   .HeaderText = "得意先コード",
        '   .DataPropertyName = "MTMR002001",
        '   .Name = "MTMR002001"
        '}
        'column055.Width = 120
        'column055.ReadOnly = True
        'column055.Visible = False
        'column055.SortMode = DataGridViewColumnSortMode.NotSortable
        'column055.HeaderCell.Style.BackColor = Color.LightSeaGreen
        'column055.HeaderCell.Style.ForeColor = Color.White
        'Me.DataGridView001.Columns.Add(column055)
        Dim column056 As New DataGridViewTextBoxColumn With {
           .HeaderText = "価格入力番号",
           .DataPropertyName = "MTMR002080",
           .Name = "MTMR002080"
        }
        column056.Width = 120
        column056.ReadOnly = True
        column056.Visible = False
        column056.SortMode = DataGridViewColumnSortMode.NotSortable
        column056.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column056.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column056)
        Dim column057 As New DataGridViewTextBoxColumn With {
           .HeaderText = "改定後売上金額",
           .DataPropertyName = "MTMR002043",
           .Name = "MTMR002043"
        }
        column057.Width = 120
        column057.ReadOnly = True
        column057.Visible = False
        column057.SortMode = DataGridViewColumnSortMode.NotSortable
        column057.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column057.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column057)
        Dim column058 As New DataGridViewTextBoxColumn With {
           .HeaderText = "改定後粗利金額",
           .DataPropertyName = "MTMR002044",
           .Name = "MTMR002044"
        }
        column058.Width = 120
        column058.ReadOnly = True
        column058.Visible = False
        column058.SortMode = DataGridViewColumnSortMode.NotSortable
        column058.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column058.HeaderCell.Style.ForeColor = Color.White
        Me.DataGridView001.Columns.Add(column058)
    End Sub
    ''' <summary>
    ''' データグリッド項目の表示・非表示設定
    ''' </summary>
    ''' <param name="visible"></param>
    Public Sub SetDataGridColumnVisible(ByVal visible As Boolean)
        '実行管理テーブルの項目表示設定を反映する
        'チェックONなら常に表示（対象）
        '改定前売単価(MTMR002022)、改定前仕入単価(MTMR002023)、改定前粗利率(MTMR002024)、現売㎡単価(MTMR002026)、現仕㎡単価(MTMR002028)、新売㎡単価(MTMR002031)、新仕㎡単価(MTMR002034)
        Me.DataGridView001.Columns("MTMR002001").Visible = visible
        Me.DataGridView001.Columns("MTMR002009").Visible = visible
        Me.DataGridView001.Columns("MTMR002011").Visible = visible
        Me.DataGridView001.Columns("MTMR002002").Visible = visible
        If displayMTMR003011 Then
            Me.DataGridView001.Columns("MTMR002022").Visible = True
        Else
            Me.DataGridView001.Columns("MTMR002022").Visible = visible
        End If
        If displayMTMR003014 Then
            Me.DataGridView001.Columns("MTMR002023").Visible = True
        Else
            Me.DataGridView001.Columns("MTMR002023").Visible = visible
        End If
        If displayMTMR003017 Then
            Me.DataGridView001.Columns("MTMR002024").Visible = True
        Else
            Me.DataGridView001.Columns("MTMR002024").Visible = visible
        End If
        If displayMTMR003012 Then
            Me.DataGridView001.Columns("MTMR002026").Visible = True
        Else
            Me.DataGridView001.Columns("MTMR002026").Visible = visible
        End If
        If displayMTMR003015 Then
            Me.DataGridView001.Columns("MTMR002028").Visible = True
        Else
            Me.DataGridView001.Columns("MTMR002028").Visible = visible
        End If
        If displayMTMR003013 Then
            Me.DataGridView001.Columns("MTMR002031").Visible = True
        Else
            Me.DataGridView001.Columns("MTMR002031").Visible = visible
        End If
        If displayMTMR003016 Then
            Me.DataGridView001.Columns("MTMR002034").Visible = True
        Else
            Me.DataGridView001.Columns("MTMR002034").Visible = visible
        End If
        Me.DataGridView001.Columns("MTMR002039").Visible = visible
        Me.DataGridView001.Columns("MTMR002040").Visible = visible
        Me.DataGridView001.Columns("MTMR002041").Visible = visible
        Me.DataGridView001.Columns("MTMR002042").Visible = visible
        Me.DataGridView001.Columns("MTMR002048").Visible = visible
    End Sub
    ''' <summary>
    ''' データの設定
    ''' </summary>
    Public Sub SetData()
        Dim table As DataTable = Me.Biz02.GetKakaku(Me.SearchCondition)
        Me.DataGridView001.DataSource = table
        Me.DataGridView001.CurrentCell = Nothing
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

        Me.DataGridView001.Columns("MTMR002001").Visible = Visible
    End Sub
    ''' <summary>
    ''' 実行管理テーブルの表示項目設定を取得
    ''' </summary>
    Public Sub SetJikouHyouji()
        Dim table As DataTable = Me.Biz02.GetKakaku(Me.SearchCondition)
        Me.DataGridView001.DataSource = table
        Me.DataGridView001.CurrentCell = Nothing
        If table.Rows.Count = 0 Then
            Me.ButtonRegist.Enabled = False
        End If
    End Sub
    ''' <summary>
    ''' フッターイベント
    ''' </summary>
    Public Sub SetFooter()
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
        Me.SetDataGridColumnVisible(Me.CheckBox001.Checked)
        DataGridView001.Font = New Font(“メイリオ”, 9)
        DataGridView001.Columns(10).Frozen = True
    End Sub
    ''' <summary>
    ''' 登録ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonRegist_Click(sender As Object, e As EventArgs) Handles ButtonRegist.Click
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
    ''' グリッドのバリデーションチェック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView001.CellValidating
        Dim dbl As Double
        Dim dt As Date

        'ロット桁数
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002017" Then
            If (Not e.FormattedValue.ToString() Is DBNull.Value) AndAlso (Not String.IsNullOrEmpty(e.FormattedValue.ToString())) Then
                Dim strMTMR002017 = e.FormattedValue.ToString()
                If (Not Me.Biz02.StringNumberByteDigitsCheck(strMTMR002017, 12)) Then
                    Me.DataGridView001.Rows(e.RowIndex).ErrorText = "ロットが12バイトを超えています"
                    e.Cancel = True
                End If
            End If
        End If

        '新売単価
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002030" AndAlso Not Double.TryParse(e.FormattedValue.ToString(), dbl) Then
            Me.DataGridView001.Rows(e.RowIndex).ErrorText = "新売単価が数値ではありません"
            MessageBox.Show("新売単価が数値ではありません" & vbCrLf & "入力例：123456789.12 ( 整数部:最大9桁、小数部:最大2桁 )")
            e.Cancel = True
        ElseIf Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002030" Then
            If Not (String.IsNullOrEmpty(e.FormattedValue.ToString())) Then
                ' 文字列内の小数点の位置を取得します。
                Dim ret As String = e.FormattedValue.ToString()
                Dim i As Integer = ret.IndexOf(".")
                Dim strInteger As String
                Dim strDecimal As String
                ' 文字列を編集します。
                If i > -1 Then
                    strInteger = ret.Substring(0, i)
                    strDecimal = ret.Substring(i + 1)
                    '整数部のチェック(9桁)
                    If (strInteger.Length > 9) Then
                        MessageBox.Show("整数部が9桁を超えています" & vbCrLf & "入力例：123456789.12 ( 整数部:最大9桁、小数部:最大2桁 )")
                        e.Cancel = True
                    End If
                Else
                    strInteger = ret
                    '整数部のチェック(9桁)
                    If (strInteger.Length > 9) Then
                        MessageBox.Show("整数部が9桁を超えています" & vbCrLf & "入力例：123456789.12 ( 整数部:最大9桁、小数部:最大2桁 )")
                        e.Cancel = True
                    End If
                End If
            End If
        End If

        '新売㎡単価
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002031" AndAlso Not Double.TryParse(e.FormattedValue.ToString(), dbl) Then
            Me.DataGridView001.Rows(e.RowIndex).ErrorText = "新売㎡単価が数値ではありません"
            e.Cancel = True
        End If

        '新仕入単価
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002033" AndAlso Not Double.TryParse(e.FormattedValue.ToString(), dbl) Then
            Me.DataGridView001.Rows(e.RowIndex).ErrorText = "新仕入単価が数値ではありません"
            MessageBox.Show("新仕入単価が数値ではありません" & vbCrLf & "入力例：123456789.12 ( 整数部:最大9桁、小数部:最大2桁 )")
            e.Cancel = True
        ElseIf Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002033" Then
            If Not (String.IsNullOrEmpty(e.FormattedValue.ToString())) Then
                ' 文字列内の小数点の位置を取得します。
                Dim ret As String = e.FormattedValue.ToString()
                Dim i As Integer = ret.IndexOf(".")
                Dim strInteger As String
                Dim strDecimal As String
                ' 文字列を編集します。
                If i > -1 Then
                    strInteger = ret.Substring(0, i)
                    strDecimal = ret.Substring(i + 1)
                    '整数部のチェック(9桁)
                    If (strInteger.Length > 9) Then
                        MessageBox.Show("整数部が9桁を超えています" & vbCrLf & "入力例：123456789.12 ( 整数部:最大9桁、小数部:最大2桁 )")
                        e.Cancel = True
                    End If
                Else
                    strInteger = ret
                    '整数部のチェック(9桁)
                    If (strInteger.Length > 9) Then
                        MessageBox.Show("整数部が9桁を超えています" & vbCrLf & "入力例：123456789.12 ( 整数部:最大9桁、小数部:最大2桁 )")
                        e.Cancel = True
                    End If
                End If
            End If
        End If

        '売実施日
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002032" Then
            Dim fomartDate As String
            Dim dtNow As DateTime = DateTime.Now
            fomartDate = e.FormattedValue.ToString()
            If (Not String.IsNullOrEmpty(e.FormattedValue.ToString())) Then
                If Not Date.TryParse(fomartDate, dt) Then
                    fomartDate = fomartDate.Replace("/", "")
                    If (fomartDate.Length = 3) Then
                        '3桁対応
                        fomartDate = dtNow.Year.ToString() & "/0" & fomartDate.Substring(0, 1) & "/" & fomartDate.Substring(1, 2)
                    ElseIf (fomartDate.Length = 4) Then
                        '4桁対応
                        fomartDate = dtNow.Year.ToString() & "/" & fomartDate.Substring(0, 2) & "/" & fomartDate.Substring(2, 2)
                    ElseIf (fomartDate.Length = 8) Then
                        '8桁対応
                        fomartDate = fomartDate.Substring(0, 4) & "/" & fomartDate.Substring(4, 2) & "/" & fomartDate.Substring(6, 2)
                    Else
                        Me.DataGridView001.Rows(e.RowIndex).ErrorText = "売実施日が日付ではありません(桁数不正 例)0401)"
                        e.Cancel = True
                    End If
                End If
            End If
            If Not String.IsNullOrEmpty(fomartDate) Then
                If Not Date.TryParse(fomartDate, dt) Then
                    Me.DataGridView001.Rows(e.RowIndex).ErrorText = "売実施日が日付ではありません"
                    e.Cancel = True
                End If
            End If
        End If

        '新仕㎡単価
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002034" AndAlso Not Double.TryParse(e.FormattedValue.ToString(), dbl) Then
            Me.DataGridView001.Rows(e.RowIndex).ErrorText = "新仕㎡単価が数値ではありません"
            e.Cancel = True
        End If

        '仕実施日
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002035" Then
            Dim fomartDate As String
            Dim dtNow As DateTime = DateTime.Now
            fomartDate = e.FormattedValue.ToString()
            If (Not String.IsNullOrEmpty(e.FormattedValue.ToString())) Then
                If Not Date.TryParse(fomartDate, dt) Then
                    fomartDate = fomartDate.Replace("/", "")
                    If (fomartDate.Length = 3) Then
                        '3桁対応
                        fomartDate = dtNow.Year.ToString() & "/0" & fomartDate.Substring(0, 1) & "/" & fomartDate.Substring(1, 2)
                    ElseIf (fomartDate.Length = 4) Then
                        '4桁対応
                        fomartDate = dtNow.Year.ToString() & "/" & fomartDate.Substring(0, 2) & "/" & fomartDate.Substring(2, 2)
                    ElseIf (fomartDate.Length = 8) Then
                        '8桁対応
                        fomartDate = fomartDate.Substring(0, 4) & "/" & fomartDate.Substring(4, 2) & "/" & fomartDate.Substring(6, 2)
                    Else
                        Me.DataGridView001.Rows(e.RowIndex).ErrorText = "売実施日が日付ではありません(桁数不正 例)0401)"
                        e.Cancel = True
                    End If
                End If
            End If
            If Not String.IsNullOrEmpty(fomartDate) Then
                If Not Date.TryParse(fomartDate, dt) Then
                    Me.DataGridView001.Rows(e.RowIndex).ErrorText = "仕実施日が日付ではありません"
                    e.Cancel = True
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' グリッドのバリデーションチェック後処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellValidated
        Me.DataGridView001.Rows(e.RowIndex).ErrorText = Nothing
    End Sub
    ''' <summary>
    ''' グリッドのセルステータス変更処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView001.CurrentCellDirtyStateChanged

        If (Me.DataGridView001.Columns(DataGridView001.CurrentCell.ColumnIndex).Name = "MTMR002076MAIL" Or Me.DataGridView001.Columns(DataGridView001.CurrentCell.ColumnIndex).Name = "MTMR002076FAX") AndAlso
            DataGridView001.IsCurrentCellDirty Then
            'コミットする
            DataGridView001.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    ''' <summary>
    ''' グリッドのセル値変更処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellValueChanged
        Dim dt As Date

        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002030" Then

            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002030").Value Is DBNull.Value) Then
                Dim formatVal As Decimal
                If (Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002030").Value, formatVal)) Then
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002030").Value = formatVal.ToString("0.00")
                    Dim decMTMR002029 As Decimal = 0.00
                    Dim decMTMR002030 As Decimal = 0.00
                    Dim decMTMR002033 As Decimal = 0.00
                    Dim decMTMR002036_ARARI As Decimal = 0.0
                    Dim decMTMR002036UP As Decimal = 0.0
                    '新粗利率計算
                    If Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002030").Value, decMTMR002030) Then
                        If Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002033").Value, decMTMR002033) Then
                            If (Not decMTMR002030 = 0.00) And (Not decMTMR002033 = 0.00) Then
                                decMTMR002036_ARARI = Math.Round((decMTMR002030 - decMTMR002033) / decMTMR002030 * 100, 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036_ARARI = 0.0) Then
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036_ARARI").Value = decMTMR002036_ARARI.ToString("0.0")
                    Else
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036_ARARI").Value = decMTMR002036_ARARI
                    End If
                    '新粗利率アップ計算
                    If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002029").Value Is DBNull.Value) Then
                        If (Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002029").Value, decMTMR002029)) Then
                            If (Not decMTMR002036_ARARI = 0.0) And (Not decMTMR002029 = 0.0) Then
                                decMTMR002036UP = Math.Round((decMTMR002036_ARARI - decMTMR002029), 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036UP = 0.0) Then
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036UP").Value = decMTMR002036UP.ToString("0.0")
                    Else
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036UP").Value = decMTMR002036UP
                    End If
                End If
            End If
        End If

        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002031" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002031").Value Is DBNull.Value) Then
                Dim formatVal As Decimal
                If (Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002031").Value, formatVal)) Then
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002031").Value = formatVal.ToString("0.00")
                End If
            End If
        End If

        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002032" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002032").Value Is DBNull.Value) Then
                Dim fomartDate As String
                Dim dtNow As DateTime = DateTime.Now
                fomartDate = Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002032").Value
                If (Not String.IsNullOrEmpty(fomartDate)) Then
                    If Not Date.TryParse(fomartDate, dt) Then
                        fomartDate = fomartDate.Replace("/", "")
                        If (fomartDate.Length = 3) Then
                            '3桁対応
                            fomartDate = dtNow.Year.ToString() & "/0" & fomartDate.Substring(0, 1) & "/" & fomartDate.Substring(1, 2)
                        ElseIf (fomartDate.Length = 4) Then
                            '4桁対応
                            fomartDate = dtNow.Year.ToString() & "/" & fomartDate.Substring(0, 2) & "/" & fomartDate.Substring(2, 2)
                        ElseIf (fomartDate.Length = 8) Then
                            '8桁対応
                            fomartDate = fomartDate.Substring(0, 4) & "/" & fomartDate.Substring(4, 2) & "/" & fomartDate.Substring(6, 2)
                        Else

                        End If
                    Else
                        fomartDate = dt.ToString("yyyy/MM/dd")
                    End If
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002032").Value = fomartDate
                End If
            End If
        End If

        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002033" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002033").Value Is DBNull.Value) Then
                Dim formatVal As Decimal
                If (Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002033").Value, formatVal)) Then
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002033").Value = formatVal.ToString("0.00")
                    Dim decMTMR002027 As Decimal = 0.00
                    Dim decMTMR002029 As Decimal = 0.00
                    Dim decMTMR002030 As Decimal = 0.00
                    Dim decMTMR002033 As Decimal = 0.00
                    Dim decMTMR002036_ARARI As Decimal = 0.0
                    Dim decMTMR002036UP As Decimal = 0.0
                    Dim decMTMR002037 As Decimal = 0.0
                    '新粗利率計算
                    If Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002030").Value, decMTMR002030) Then
                        If Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002033").Value, decMTMR002033) Then
                            If (Not decMTMR002030 = 0.00) And (Not decMTMR002033 = 0.00) Then
                                decMTMR002036_ARARI = Math.Round((decMTMR002030 - decMTMR002033) / decMTMR002030 * 100, 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036_ARARI = 0.0) Then
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036_ARARI").Value = decMTMR002036_ARARI.ToString("0.0")
                    Else
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036_ARARI").Value = decMTMR002036_ARARI
                    End If
                    '新粗利率アップ計算
                    If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002029").Value Is DBNull.Value) Then
                        If (Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002029").Value, decMTMR002029)) Then
                            If (Not decMTMR002036_ARARI = 0.0) And (Not decMTMR002029 = 0.0) Then
                                decMTMR002036UP = Math.Round((decMTMR002036_ARARI - decMTMR002029), 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002036UP = 0.0) Then
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036UP").Value = decMTMR002036UP.ToString("0.0")
                    Else
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002036UP").Value = decMTMR002036UP
                    End If
                    '値上率計算
                    If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002027").Value Is DBNull.Value) Then
                        If (Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002027").Value, decMTMR002027)) Then
                            If (Not decMTMR002027 = 0.0) And (Not decMTMR002033 = 0.0) Then
                                decMTMR002037 = Math.Round((decMTMR002033 / decMTMR002027) * 100, 1, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    If (decMTMR002037 = 0.0) Then
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002037").Value = decMTMR002037.ToString("0.0")
                    Else
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002037").Value = decMTMR002037
                    End If

                End If
            End If
        End If

        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002034" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002034").Value Is DBNull.Value) Then
                Dim formatVal As Decimal
                If (Decimal.TryParse(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002034").Value, formatVal)) Then
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002034").Value = formatVal.ToString("0.00")
                End If
            End If
        End If

        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002035" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002035").Value Is DBNull.Value) Then
                Dim fomartDate As String
                Dim dtNow As DateTime = DateTime.Now
                fomartDate = Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002035").Value
                If (Not String.IsNullOrEmpty(fomartDate)) Then
                    If Not Date.TryParse(fomartDate, dt) Then
                        If (fomartDate.Length = 3) Then
                            '3桁対応
                            fomartDate = dtNow.Year.ToString() & "/0" & fomartDate.Substring(0, 1) & "/" & fomartDate.Substring(1, 2)
                        ElseIf (fomartDate.Length = 4) Then
                            '4桁対応
                            fomartDate = dtNow.Year.ToString() & "/" & fomartDate.Substring(0, 2) & "/" & fomartDate.Substring(2, 2)
                        ElseIf (fomartDate.Length = 8) Then
                            '8桁対応
                            fomartDate = fomartDate.Substring(0, 4) & "/" & fomartDate.Substring(4, 2) & "/" & fomartDate.Substring(6, 2)
                        Else

                        End If
                    Else
                        fomartDate = dt.ToString("yyyy/MM/dd")
                    End If

                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002035").Value = fomartDate
                End If
            End If
        End If

        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002076MAIL" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value Is DBNull.Value) Then
                If Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 1 Then
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 1
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").ReadOnly = True
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 0
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").ReadOnly = False
                End If
            End If
        End If
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002076FAX" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value Is DBNull.Value) Then
                If Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 1 Then
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 1
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").ReadOnly = True
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 0
                    Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").ReadOnly = False
                End If

            End If
        End If

        'If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002076MAIL" Then
        '    If (Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value Is DBNull.Value) Then
        '        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 1
        '        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 0
        '    Else
        '        If Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 1 Then
        '            Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 1
        '            Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 0
        '        End If
        '    End If
        'End If

        'If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002076FAX" Then
        '    If (Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value Is DBNull.Value) Then
        '        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 0
        '        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 1
        '    Else
        '        If Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 1 Then
        '            Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 0
        '            Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 1
        '        End If
        '    End If
        'End If

        '納品先履歴（過去18カ月）
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002053" Then
            If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002053").Value Is DBNull.Value) Then
                Dim comMTMR002052 = Me.Biz02.DeliveryConvert(Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002053").Value)
                Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002053").Value = comMTMR002052
            End If
        End If

        Me.SetFooter()
    End Sub
    ''' <summary>
    ''' グリッド行選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellDoubleClick
        Dim formSelect As New FormMTMSelect

        If (Not IsNothing(DataGridView001.CurrentCell)) Then
            Dim intSelectRow As Integer
            intSelectRow = DataGridView001.CurrentCell.RowIndex

            If (intSelectRow >= 0) Then
                formSelect.selectMTMR002001 = DataGridView001.CurrentRow.Cells("MTMR002001").Value
                formSelect.selectMTMR002002 = DataGridView001.CurrentRow.Cells("MTMR002002").Value
                formSelect.selectMTMR002003 = DataGridView001.CurrentRow.Cells("MTMR002003").Value
            End If
        End If

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
    Private Sub DataGridView001_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView001.CellFormatting
        'セルの列を確認
        If (e.ColumnIndex > 0) Then
            If DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002036_ARARI" Then
                Dim val As Decimal = CDec(e.Value)
                'セルの値により、背景色を変更する
                If val > 0 Then
                    e.CellStyle.ForeColor = Color.Black
                ElseIf val < 0 Then
                    e.CellStyle.ForeColor = Color.Red
                End If
            End If
            If DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002036UP" Then
                Dim val As Decimal = CDec(e.Value)
                'セルの値により、背景色を変更する
                If val > 0 Then
                    e.CellStyle.ForeColor = Color.Black
                ElseIf val < 0 Then
                    e.CellStyle.ForeColor = Color.Red
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 確定オール選択/ オール解除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM02Regist_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Me.DataGridView001.Rows.Count > 0 Then
            If e.KeyCode = Keys.F2 Then
                Dim setValue As Integer = 0
                If (allCheck) Then
                    setValue = 1
                    allCheck = False
                Else
                    setValue = 0
                    allCheck = True
                End If
                '--------------------------
                'データソースを入替（確定フラグをロジックで変更して積みなおし）
                Dim sortTable As DataTable = Me.DataGridView001.DataSource
                Dim sortRows As DataRow() = sortTable.Select().Clone()
                Dim sortChangeTable As New DataTable()
                sortChangeTable = sortTable.Clone()
                For Each row As DataRow In sortRows
                    row("MTMR002085") = setValue
                    sortChangeTable.ImportRow(row)
                Next
                Me.DataGridView001.DataSource = sortChangeTable
                '--------------------------
                'Me.DataGridView001.CurrentCell = Me.DataGridView001("MTMR002001", 0)
                'If (Me.CheckBox0001.Checked = True) Then
                '    For i As Integer = 0 To Me.DataGridView001.RowCount - 1
                '        Me.DataGridView001("MTMR002085", i).Value = False
                '    Next
                '    Me.CheckBox0001.Checked = False
                '    Me.DataGridView001.CurrentCell = Me.DataGridView001("MTMR002086", 0)
                'Else
                '    For i As Integer = 0 To Me.DataGridView001.RowCount - 1
                '        Me.DataGridView001("MTMR002085", i).Value = True
                '    Next
                '    Me.CheckBox0001.Checked = True
                '    Me.DataGridView001.CurrentCell = Me.DataGridView001("MTMR002086", 0)
                'End If
            End If
            If e.KeyCode = Keys.F5 Then
                Me.DataGridView001.DataSource = ""
                DataGridView001.Font = New Font(“メイリオ”, 9)
                Me.SetHeader()
                Me.SetDataGridColumn()
                Me.SetDataGridColumnVisible(False)
                Me.SetData()
                Me.SetFooter()
                DataGridView001.Columns(10).Frozen = True
                Me.CheckBox0001.Checked = False
                DataGridView001.ScrollBars = ScrollBars.None
                DataGridView001.ScrollBars = ScrollBars.Both
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
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView001.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim dgv As DataGridView = sender
            If TypeOf dgv.Columns(e.ColumnIndex) Is DataGridViewCheckBoxColumn Then
                e.PaintBackground(e.CellBounds, True)
                ControlPaint.DrawCheckBox(e.Graphics, e.CellBounds.X + 1,
                    e.CellBounds.Y + 1, e.CellBounds.Width - 2, e.CellBounds.Height - 2,
                    If(CBool(e.FormattedValue), ButtonState.Checked, ButtonState.Normal))
                e.Handled = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView001.EditingControlShowing
        '表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
        If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
            Dim dgv As DataGridView = CType(sender, DataGridView)

            '編集のために表示されているコントロールを取得
            Dim tb1 As DataGridViewTextBoxEditingControl =
            CType(e.Control, DataGridViewTextBoxEditingControl)
            Dim tb2 As DataGridViewTextBoxEditingControl =
            CType(e.Control, DataGridViewTextBoxEditingControl)
            Dim tb3 As DataGridViewTextBoxEditingControl =
            CType(e.Control, DataGridViewTextBoxEditingControl)
            Dim tb4 As DataGridViewTextBoxEditingControl =
            CType(e.Control, DataGridViewTextBoxEditingControl)
            Dim tb5 As DataGridViewTextBoxEditingControl =
            CType(e.Control, DataGridViewTextBoxEditingControl)
            Dim tb6 As DataGridViewTextBoxEditingControl =
            CType(e.Control, DataGridViewTextBoxEditingControl)

            'イベントハンドラを削除
            RemoveHandler tb1.KeyPress, AddressOf dataGridViewTextBox_KeyPress
            RemoveHandler tb2.KeyPress, AddressOf dataGridViewTextBox_KeyPress3
            RemoveHandler tb3.KeyPress, AddressOf dataGridViewTextBox_KeyPress4
            RemoveHandler tb4.KeyPress, AddressOf dataGridViewTextBox_KeyPress2
            RemoveHandler tb5.KeyDown, AddressOf dataGridViewTextBox_KeyDown5
            RemoveHandler tb6.KeyDown, AddressOf dataGridViewTextBox_KeyDown6

            '該当する列か調べる
            If dgv.CurrentCell.OwningColumn.Name = "MTMR002030" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb1.KeyPress, AddressOf dataGridViewTextBox_KeyPress3
                AddHandler tb5.KeyDown, AddressOf dataGridViewTextBox_KeyDown5

            End If
            'If dgv.CurrentCell.OwningColumn.Name = "MTMR002031" Then
            '    'KeyPressイベントハンドラを追加
            '    If (Not keypressMTMR002031) Then
            '        AddHandler tb1.KeyPress, AddressOf dataGridViewTextBox_KeyPress
            '        keypressMTMR002031 = True
            '    End If
            'End If
            If dgv.CurrentCell.OwningColumn.Name = "MTMR002033" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb2.KeyPress, AddressOf dataGridViewTextBox_KeyPress4
                AddHandler tb6.KeyDown, AddressOf dataGridViewTextBox_KeyDown6
            End If
            'If dgv.CurrentCell.OwningColumn.Name = "MTMR002034" Then
            '    'KeyPressイベントハンドラを追加
            '    If (Not keypressMTMR002034) Then
            '        AddHandler tb.KeyPress, AddressOf dataGridViewTextBox_KeyPress
            '        keypressMTMR002034 = True
            '    End If
            'End If
            If dgv.CurrentCell.OwningColumn.Name = "MTMR002032" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb3.KeyPress, AddressOf dataGridViewTextBox_KeyPress2
            End If
            If dgv.CurrentCell.OwningColumn.Name = "MTMR002035" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb4.KeyPress, AddressOf dataGridViewTextBox_KeyPress2
            End If
        End If
    End Sub

    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dataGridViewTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        '数字しか入力できないようにする
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> "."c And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dataGridViewTextBox_KeyPress2(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        '数字しか入力できないようにする
        'If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
        '    e.Handled = True
        'End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dataGridViewTextBox_KeyPress3(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        '数字しか入力できないようにする
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> "."c And e.KeyChar <> vbBack Then
            e.Handled = True
            'Else
            '    If e.KeyChar = vbBack Then
            '        If copyMTMR002030.Length <> 0 Then
            '            copyMTMR002030 = copyMTMR002030.Remove(copyMTMR002030.Length - 1, 1)
            '            Dim count As Integer = copyMTMR002030.Length - copyMTMR002030.Replace(".", "").Length
            '            '.が１つ以上ならキャンセル
            '            If (count = 0) Then
            '                digitsMTMR002030 = copyMTMR002030.Length
            '            Else
            '                fewMTMR002030 -= 1
            '            End If
            '        End If
            '    Else
            '        copyMTMR002030 += e.KeyChar
            '        Dim count As Integer = copyMTMR002030.Length - copyMTMR002030.Replace(".", "").Length
            '        '.が１つ以上ならキャンセル
            '        If (count > 1) Then
            '            copyMTMR002030 = copyMTMR002030.Remove(copyMTMR002030.Length - 1, 1)
            '            e.Handled = True
            '        ElseIf (count = 1) Then
            '            '小数点以下の対応
            '            konmaMTMR002030 = True
            '            If (e.KeyChar <> "."c) Then
            '                fewMTMR002030 += 1
            '            End If
            '        Else
            '            '整数部の対応
            '            konmaMTMR002030 = False
            '            digitsMTMR002030 += 1
            '        End If
            '        '桁数チェック
            '        If (konmaMTMR002030) Then
            '            '小数点以下の桁数対応
            '            If (fewMTMR002030) > 2 Then
            '                fewMTMR002030 -= 1
            '                copyMTMR002030 = copyMTMR002030.Remove(copyMTMR002030.Length - 1, 1)
            '                e.Handled = True
            '            End If
            '        Else
            '            '整数部の桁数対応
            '            If (digitsMTMR002030) > 9 Then
            '                digitsMTMR002030 -= 1
            '                copyMTMR002030 = copyMTMR002030.Remove(copyMTMR002030.Length - 1, 1)
            '                e.Handled = True
            '            End If
            '        End If
            '    End If
        End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dataGridViewTextBox_KeyPress4(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        '数字しか入力できないようにする
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> "."c And e.KeyChar <> vbBack Then
            e.Handled = True
            'Else
            '    If e.KeyChar = vbBack Then
            '        If copyMTMR002033.Length <> 0 Then
            '            copyMTMR002033 = copyMTMR002033.Remove(copyMTMR002033.Length - 1, 1)
            '            Dim count As Integer = copyMTMR002033.Length - copyMTMR002033.Replace(".", "").Length
            '            '.が１つ以上ならキャンセル
            '            If (count = 0) Then
            '                digitsMTMR002033 = copyMTMR002033.Length
            '            Else
            '                fewMTMR002033 -= 1
            '            End If
            '        End If
            '    Else
            '        copyMTMR002033 += e.KeyChar
            '        Dim count As Integer = copyMTMR002033.Length - copyMTMR002033.Replace(".", "").Length
            '        '.が１つ以上ならキャンセル
            '        If (count > 1) Then
            '            copyMTMR002033 = copyMTMR002033.Remove(copyMTMR002033.Length - 1, 1)
            '            e.Handled = True
            '        ElseIf (count = 1) Then
            '            '小数点以下の対応
            '            konmaMTMR002033 = True
            '            If (e.KeyChar <> "."c) Then
            '                fewMTMR002033 += 1
            '            End If
            '        Else
            '            '整数部の対応
            '            konmaMTMR002033 = False
            '            digitsMTMR002033 += 1
            '        End If
            '        '桁数チェック
            '        If (konmaMTMR002033) Then
            '            '小数点以下の桁数対応
            '            If (fewMTMR002033) > 2 Then
            '                fewMTMR002033 -= 1
            '                copyMTMR002033 = copyMTMR002033.Remove(copyMTMR002033.Length - 1, 1)
            '                e.Handled = True
            '            End If
            '        Else
            '            '整数部の桁数対応
            '            If (digitsMTMR002033) > 9 Then
            '                digitsMTMR002033 -= 1
            '                copyMTMR002033 = copyMTMR002033.Remove(copyMTMR002033.Length - 1, 1)
            '                e.Handled = True
            '            End If
            '        End If
            '    End If
        End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dataGridViewTextBox_KeyDown5(ByVal sender As Object, ByVal e As KeyEventArgs)
        'If e.KeyCode = Keys.Delete Then
        '    If copyMTMR002030.Length <> 0 Then
        '        copyMTMR002030 = copyMTMR002030.Remove(copyMTMR002030.Length - 1, 1)
        '        Dim count As Integer = copyMTMR002030.Length - copyMTMR002030.Replace(".", "").Length
        '        '.が１つ以上ならキャンセル
        '        If (count = 0) Then
        '            digitsMTMR002030 = copyMTMR002030.Length
        '        Else
        '            fewMTMR002030 -= 1
        '        End If
        '    End If
        'End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dataGridViewTextBox_KeyDown6(ByVal sender As Object, ByVal e As KeyEventArgs)
        'If e.KeyCode = Keys.Delete Then
        '    If copyMTMR002033.Length <> 0 Then
        '        copyMTMR002033 = copyMTMR002033.Remove(copyMTMR002033.Length - 1, 1)
        '        Dim count As Integer = copyMTMR002033.Length - copyMTMR002033.Replace(".", "").Length
        '        '.が１つ以上ならキャンセル
        '        If (count = 0) Then
        '            digitsMTMR002033 = copyMTMR002033.Length
        '        Else
        '            fewMTMR002033 -= 1
        '        End If
        '    End If
        'End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView001.CellMouseClick
        If (e.ColumnIndex >= 0 And e.RowIndex >= 0) Then
            If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002076MAIL" Then
                If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value Is DBNull.Value) Then
                    If Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 1 Then
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 1
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").ReadOnly = True
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 0
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").ReadOnly = False
                    End If
                End If
            End If
            If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002076FAX" Then
                If (Not Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value Is DBNull.Value) Then
                    If Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 1 Then
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").Value = 1
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076FAX").ReadOnly = True
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").Value = 0
                        Me.DataGridView001.Rows(e.RowIndex).Cells("MTMR002076MAIL").ReadOnly = False
                    End If

                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView001.CellBeginEdit
        'If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002030" Then
        '    digitsMTMR002030 = 0
        '    fewMTMR002030 = 0
        '    copyMTMR002030 = ""
        '    konmaMTMR002030 = False
        'End If
        'If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002033" Then
        '    digitsMTMR002033 = 0
        '    fewMTMR002033 = 0
        '    copyMTMR002033 = ""
        '    konmaMTMR002033 = False
        'End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellEnter
        'ロット
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002017" Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
        '新売単価
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002030" Then
            DataGridView001.ImeMode = ImeMode.Disable
        End If
        '新仕単価
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002033" Then
            DataGridView001.ImeMode = ImeMode.Disable
        End If
        '納品先履歴
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002053" Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
        '備考
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002072" Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
        '宛先名
        If Me.DataGridView001.Columns(e.ColumnIndex).Name = "MTMR002079" Then
            DataGridView001.ImeMode = ImeMode.On
            DataGridView001.ImeMode = ImeMode.Hiragana
        End If
    End Sub
End Class

