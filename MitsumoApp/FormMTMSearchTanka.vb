Imports System.Configuration
Imports MitsumoLib
Public Class FormMTMSearchTanka

    '---------------------
    '条件値
    '---------------------
    Public selectMTMR002001 As String         '得意先コード
    Public selectMTMR002002 As String         '商品コード
    Public selectMTMR002003 As String         '規格

    ''' <summary>
    ''' 単価台帳検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchTanka As Biz.MTMSearchTanka

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchTanka = New Biz.MTMSearchTanka(connectionString)
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
    Private Sub FormMTMTanka_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Me.DataGridView001.DataSource = Me.BizSearchTanka.GetTanka(selectMTMR002001, selectMTMR002002, selectMTMR002003)
        End If

        '数量の整形
        Dim table As DataTable = Me.DataGridView001.DataSource
        Dim nowHANMA02006 As Decimal = 0
        Dim recordBefore As Decimal = 0
        Dim nextHANMA02006 As Decimal = 0
        Dim convertHANMA02006 As New List(Of String)
        Dim first As Boolean = True
        Dim rowNo As Integer = 0

        If table.Rows.Count > 0 Then
            For Each row As DataRow In table.Rows
                nowHANMA02006 = Decimal.Parse(row.Item("HANMA02006").ToString)
                If (first) Then
                    convertHANMA02006.Add("1～")
                    first = False
                Else
                    nextHANMA02006 = recordBefore + 1
                    convertHANMA02006.Add(nextHANMA02006 & "～" & nowHANMA02006)
                End If

                recordBefore = Decimal.Parse(row.Item("HANMA02006").ToString)
            Next
        End If

        For Each setValue As String In convertHANMA02006
            Me.DataGridView001(6, rowNo).Value = setValue
            rowNo += 1
        Next


        Me.DataGridView001.CurrentCell = Nothing

    End Sub
    ''' <summary>
    ''' データグリッド項目の設定
    ''' </summary>
    Private Sub SetDataGridColumn()
        Me.DataGridView001.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Dim column001 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品C",
                .DataPropertyName = "HANMA02002",
                .Name = "HANMA02002"
            }
        column001.Width = 90
        column001.ReadOnly = True
        column001.HeaderCell.Style.ForeColor = Color.White
        column001.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column001.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column001)
        Dim column002 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品名１",
                .DataPropertyName = "HANM003103",
                .Name = "HANM003103"
            }
        column002.Width = 250
        column002.ReadOnly = True
        column002.HeaderCell.Style.ForeColor = Color.White
        column002.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品名２",
                .DataPropertyName = "HANM003104",
                .Name = "HANM003104"
            }
        column003.Width = 150
        column003.ReadOnly = True
        column003.HeaderCell.Style.ForeColor = Color.White
        column003.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column003.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column003)
        Dim column004 As New DataGridViewTextBoxColumn With {
                .HeaderText = "規格",
                .DataPropertyName = "HANMA02003",
                .Name = "HANMA02003"
            }
        column004.Width = 80
        column004.ReadOnly = True
        column004.HeaderCell.Style.ForeColor = Color.White
        column004.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column004.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column004)
        Dim column005 As New DataGridViewTextBoxColumn With {
                .HeaderText = "入数",
                .DataPropertyName = "HANMA01006",
                .Name = "HANMA01006"
            }
        column005.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column005.Width = 60
        column005.ReadOnly = True
        column005.HeaderCell.Style.ForeColor = Color.White
        column005.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column005.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column005)
        Dim column006 As New DataGridViewTextBoxColumn With {
                .HeaderText = "単位",
                .DataPropertyName = "HANMA01007",
                .Name = "HANMA01007"
            }
        column006.Width = 60
        column006.ReadOnly = True
        column006.HeaderCell.Style.ForeColor = Color.White
        column006.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column006.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column006)
        Dim column007 As New DataGridViewTextBoxColumn With {
                .HeaderText = "数量",
                .DataPropertyName = "HANMA02006",
                .Name = "HANMA02006"
            }
        column007.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column007.Width = 120
        column007.ReadOnly = True
        column007.HeaderCell.Style.ForeColor = Color.White
        column007.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column007.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column007)
        Dim column008 As New DataGridViewTextBoxColumn With {
                .HeaderText = "売上単価",
                .DataPropertyName = "HANMA02007",
                .Name = "HANMA02007"
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
                    .DataPropertyName = "HANMA02008",
                    .Name = "HANMA02008"
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
                    .HeaderText = "最終売上日",
                    .DataPropertyName = "HANMA01022",
                    .Name = "HANMA01022"
                }
        column011.Width = 80
        column011.ReadOnly = True
        column011.HeaderCell.Style.ForeColor = Color.White
        column011.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column011.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column011)
        Dim column012 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "売単価（新）",
                    .DataPropertyName = "HANMA02011",
                    .Name = "HANMA02011"
                }
        column012.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column012.Width = 100
        column012.ReadOnly = True
        column012.HeaderCell.Style.ForeColor = Color.White
        column012.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column012.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column012)
        Dim column013 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "新売単価更新日",
                    .DataPropertyName = "HANMA02009",
                    .Name = "HANMA02009"
                }
        column013.Width = 110
        column013.ReadOnly = True
        column013.HeaderCell.Style.ForeColor = Color.White
        column013.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column013.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column013)
        Dim column014 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "仕単価（新）",
                    .DataPropertyName = "HANMA02012",
                    .Name = "HANMA02012"
                }
        column014.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column014.Width = 100
        column014.ReadOnly = True
        column014.HeaderCell.Style.ForeColor = Color.White
        column014.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column014.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column014)
        Dim column015 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "新仕単価更新日",
                    .DataPropertyName = "HANMA02010",
                    .Name = "HANMA02010"
                }
        column015.Width = 110
        column015.ReadOnly = True
        column015.HeaderCell.Style.ForeColor = Color.White
        column015.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column015.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column015)
        Dim column016 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "新粗利率",
                    .DataPropertyName = "SHIN_ARARI",
                    .Name = "SHIN_ARARI"
                }
        column016.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column016.Width = 100
        column016.ReadOnly = True
        column016.HeaderCell.Style.ForeColor = Color.White
        column016.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column016.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column016)
        Dim column017 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "手配",
                    .DataPropertyName = "HANM031003",
                    .Name = "HANM031003"
                }
        column017.Width = 60
        column017.ReadOnly = True
        column017.HeaderCell.Style.ForeColor = Color.White
        column017.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column017.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column017)
        Dim column018 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "発注摘要",
                    .DataPropertyName = "HANMA01013",
                    .Name = "HANMA01013"
                }
        column018.Width = 150
        column018.ReadOnly = True
        column018.HeaderCell.Style.ForeColor = Color.White
        column018.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column018.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column018)
        Dim column019 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "社内摘要",
                    .DataPropertyName = "HANMA01014",
                    .Name = "HANMA01014"
                }
        column019.Width = 200
        column019.ReadOnly = True
        column019.HeaderCell.Style.ForeColor = Color.White
        column019.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column019.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column019)
        Dim column020 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "運賃適用",
                    .DataPropertyName = "HANMA01042",
                    .Name = "HANMA01042"
                }
        column020.Width = 200
        column020.ReadOnly = True
        column020.HeaderCell.Style.ForeColor = Color.White
        column020.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column020.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column020)
        Dim column021 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "仕入先C",
                    .DataPropertyName = "HANMA01019",
                    .Name = "HANMA01019"
                }
        column021.Width = 80
        column021.ReadOnly = True
        column021.HeaderCell.Style.ForeColor = Color.White
        column021.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column021.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column021)
        Dim column022 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "仕入先名",
                    .DataPropertyName = "HANM002006",
                    .Name = "HANM002006"
                }
        column022.Width = 250
        column022.ReadOnly = True
        column022.HeaderCell.Style.ForeColor = Color.White
        column022.HeaderCell.Style.BackColor = Color.LightSeaGreen
        column022.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column022)
    End Sub
    ''' <summary>
    ''' フォームイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchTanka_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.No
            Me.Close()
        End If
    End Sub
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView001.CellFormatting

    End Sub
End Class