Imports System.Configuration
Imports MitsumoLib

Public Class FormMTMSearchUriage
    '---------------------
    '条件値
    '---------------------
    Public selectMTMR002001 As String         '得意先コード
    Public selectMTMR002002 As String         '商品コード
    Public selectMTMR002003 As String         '規格

    ''' <summary>
    ''' 売上履歴検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchUriage As Biz.MTMSearchUriage

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchUriage = New Biz.MTMSearchUriage(connectionString)
    End Sub

    ''' <summary>
    ''' クローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub
    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchUriage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView001.Font = New Font(“メイリオ”, 9)
        Me.SetDataGridColumn()
        Me.SetData()
        DataGridView001.ScrollBars = ScrollBars.None
        DataGridView001.ScrollBars = ScrollBars.Both
        'Me.WindowState = FormWindowState.Maximized
        Me.Width = Screen.GetBounds(Me).Width \ 1
        Me.Height = Screen.GetBounds(Me).Height \ 3
        Me.Top = 0
        Me.Left = 0
    End Sub
    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData()
        If (Not String.IsNullOrEmpty(selectMTMR002001) And Not String.IsNullOrEmpty(selectMTMR002002)) Then
            Me.DataGridView001.DataSource = Me.BizSearchUriage.GetUriage(selectMTMR002001, selectMTMR002002, selectMTMR002003)
        End If
        Me.DataGridView001.CurrentCell = Nothing
    End Sub
    ''' <summary>
    ''' データグリッド項目の設定
    ''' </summary>
    Private Sub SetDataGridColumn()
        Me.DataGridView001.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Dim column001 As New DataGridViewTextBoxColumn With {
                .HeaderText = "売上日",
                .DataPropertyName = "HANR002001",
                .Name = "HANR002001"
            }
        column001.Width = 100
        column001.ReadOnly = True
        column001.HeaderCell.Style.ForeColor = Color.White
        column001.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column001.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column001)
        Dim column002 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品C",
                .DataPropertyName = "HANR002019",
                .Name = "HANR002019"
            }
        column002.Width = 90
        column002.ReadOnly = True
        column002.HeaderCell.Style.ForeColor = Color.White
        column002.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column002.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column002)
        Dim column002_1 As New DataGridViewTextBoxColumn With {
                .HeaderText = "規格",
                .DataPropertyName = "HANR002A002",
                .Name = "HANR002A002"
            }
        column002_1.Width = 80
        column002_1.ReadOnly = True
        column002_1.HeaderCell.Style.ForeColor = Color.White
        column002_1.HeaderCell.Style.BackColor = Color.LightSeaGreen
        Me.DataGridView001.Columns.Add(column002_1)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品名１",
                .DataPropertyName = "HANR002020",
                .Name = "HANR002020"
            }
        column003.Width = 250
        column003.ReadOnly = True
        column003.HeaderCell.Style.ForeColor = Color.White
        column003.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column003.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column003)
        Dim column004 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品名２",
                .DataPropertyName = "HANR002118",
                .Name = "HANR002118"
            }
        column004.Width = 150
        column004.ReadOnly = True
        column004.HeaderCell.Style.ForeColor = Color.White
        column004.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column004.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column004)
        Dim column005 As New DataGridViewTextBoxColumn With {
                .HeaderText = "入数",
                .DataPropertyName = "HANR002022",
                .Name = "HANR002022"
            }
        column005.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column005.Width = 60
        column005.ReadOnly = True
        column005.HeaderCell.Style.ForeColor = Color.White
        column005.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column005.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column005)
        Dim column006 As New DataGridViewTextBoxColumn With {
                .HeaderText = "数量",
                .DataPropertyName = "HANR002025",
                .Name = "HANR002025"
            }
        column006.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column006.Width = 80
        column006.ReadOnly = True
        column006.HeaderCell.Style.ForeColor = Color.White
        column006.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column006.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column006)
        Dim column007 As New DataGridViewTextBoxColumn With {
                .HeaderText = "単位",
                .DataPropertyName = "HANR002021",
                .Name = "HANR002021"
            }
        column007.Width = 60
        column007.ReadOnly = True
        column007.HeaderCell.Style.ForeColor = Color.White
        column007.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column007.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column007)
        Dim column008 As New DataGridViewTextBoxColumn With {
                .HeaderText = "売上単価",
                .DataPropertyName = "HANR002026",
                .Name = "HANR002026"
            }
        column008.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column008.Width = 80
        column008.ReadOnly = True
        column008.HeaderCell.Style.ForeColor = Color.White
        column008.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column008.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column008)
        Dim column009 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "仕入単価",
                    .DataPropertyName = "HANR002028",
                    .Name = "HANR002028"
                }
        column009.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column009.Width = 80
        column009.ReadOnly = True
        column009.HeaderCell.Style.ForeColor = Color.White
        column009.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column009.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column009)
        Dim column010 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "粗利率",
                    .DataPropertyName = "ARARI",
                    .Name = "ARARI"
                }
        column010.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column010.Width = 80
        column010.ReadOnly = True
        column010.HeaderCell.Style.ForeColor = Color.White
        column010.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column010.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column010)
        Dim column011 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "売上金額",
                    .DataPropertyName = "HANR002027",
                    .Name = "HANR002027"
                }
        column011.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column011.Width = 90
        column011.ReadOnly = True
        column011.HeaderCell.Style.ForeColor = Color.White
        column011.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column011.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column011)
        Dim column012 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "仕入金額",
                    .DataPropertyName = "SHIIRE_TANKA",
                    .Name = "SHIIRE_TANKA"
                }
        column012.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column012.Width = 90
        column012.ReadOnly = True
        column012.HeaderCell.Style.ForeColor = Color.White
        column012.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column012.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column012)
        Dim column013 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "手配",
                    .DataPropertyName = "HANM031003",
                    .Name = "HANM031003"
                }
        column013.Width = 60
        column013.ReadOnly = True
        column013.HeaderCell.Style.ForeColor = Color.White
        column013.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column013.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column013)
        Dim column014 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "納品先名１",
                    .DataPropertyName = "HANM007003",
                    .Name = "HANM007003"
                }
        column014.Width = 220
        column014.ReadOnly = True
        column014.HeaderCell.Style.ForeColor = Color.White
        column014.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column014.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column014)
        Dim column015 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "納品先名２",
                    .DataPropertyName = "HANM007005",
                    .Name = "HANM007005"
                }
        column015.Width = 150
        column015.ReadOnly = True
        column015.HeaderCell.Style.ForeColor = Color.White
        column015.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column015.HeaderCell.Style.Font = New Font("メイリオ", 9, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column015)
        Dim column016 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "仕入先略称",
                    .DataPropertyName = "HANM002006",
                    .Name = "HANM002006"
                }
        column016.Width = 180
        column016.ReadOnly = True
        column016.HeaderCell.Style.ForeColor = Color.White
        column016.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column016.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column016)
    End Sub
    ''' <summary>
    ''' フォームイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchUriage_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.No
            Me.Close()
        End If
    End Sub
End Class