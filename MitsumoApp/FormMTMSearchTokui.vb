Imports System.Configuration
Imports MitsumoLib
Public Class FormMTMSearchTokui
    Public Property Selected As Models.HAN10M001TOKUI

    ''' <summary>
    ''' 得意先コード検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchTokui As Biz.MTMSearchTokui

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchTokui = New Biz.MTMSearchTokui(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchTokui_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetDataGridColumn()
        Me.SetData()
        DataGridView001.Font = New Font(“メイリオ”, 10)
    End Sub

    ''' <summary>
    ''' データグリッド項目の設定
    ''' </summary>
    Private Sub SetDataGridColumn()
        Me.DataGridView001.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Dim column001 As New DataGridViewButtonColumn With {
                .HeaderText = "選択",
                .Name = "SelectButtonSelect"
            }
        column001.Width = 80
        column001.DefaultCellStyle.ForeColor = Color.Black
        column001.DefaultCellStyle.BackColor = Color.White
        column001.UseColumnTextForButtonValue = True
        column001.Text = "選択"
        column001.DefaultCellStyle.Font = New Font("ＭＳ 明朝", 9, FontStyle.Underline)
        column001.HeaderCell.Style.Font = New Font("ＭＳ 明朝", 12, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column001)
        Dim column002 As New DataGridViewTextBoxColumn With {
                .HeaderText = "請求先コード",
                .DataPropertyName = "HANM001001",
                .Name = "HANM001001"
            }
        column002.Width = 120
        column002.ReadOnly = True
        column002.Visible = False
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "請求先区分",
                .DataPropertyName = "HANM001002",
                .Name = "HANM001002"
            }
        column003.ReadOnly = True
        column003.Visible = False
        column003.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column003)
        Dim column004 As New DataGridViewTextBoxColumn With {
                .HeaderText = "得意先コード",
                .DataPropertyName = "HANM001003",
                .Name = "HANM001003"
            }
        column004.Width = 180
        column004.ReadOnly = True
        column004.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column004)
        Dim column005 As New DataGridViewTextBoxColumn With {
                .HeaderText = "得意先名",
                .DataPropertyName = "HANM001004",
                .Name = "HANM001004"
            }
        column005.Width = 600
        column005.ReadOnly = True
        column005.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column005)
        Dim column006 As New DataGridViewTextBoxColumn With {
                .HeaderText = "得意先名２",
                .DataPropertyName = "HANM001005",
                .Name = "HANM001005"
            }
        column006.Width = 250
        column006.ReadOnly = True
        column006.Visible = False
        column006.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column006)
        Dim column007 As New DataGridViewTextBoxColumn With {
                .HeaderText = "得意先略称",
                .DataPropertyName = "HANM001006",
                .Name = "HANM001006"
            }
        column007.Width = 200
        column007.ReadOnly = True
        column007.Visible = False
        column007.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column007)
        Dim column008 As New DataGridViewTextBoxColumn With {
                .HeaderText = "得意先索引",
                .DataPropertyName = "HANM001007",
                .Name = "HANM001007"
            }
        column008.Width = 200
        column008.ReadOnly = True
        column008.Visible = True
        column008.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column008)
        Dim column009 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "郵便番号",
                    .DataPropertyName = "HANM001008",
                    .Name = "HANM001008"
                }
        column009.ReadOnly = True
        column009.Visible = False
        column009.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column009)
        Dim column010 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "住所１",
                    .DataPropertyName = "HANM001009",
                    .Name = "HANM001009"
                }
        column010.ReadOnly = True
        column010.Visible = False
        column010.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column010)
        Dim column011 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "住所２",
                    .DataPropertyName = "HANM001010",
                    .Name = "HANM001010"
                }
        column011.ReadOnly = True
        column011.Visible = False
        column011.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column011)
        Dim column012 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "住所３",
                    .DataPropertyName = "HANM001011",
                    .Name = "HANM001011"
                }
        column012.ReadOnly = True
        column012.Visible = False
        column012.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column012)
        Dim column013 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "電話番号",
                    .DataPropertyName = "HANM001012",
                    .Name = "HANM001012"
                }
        column013.ReadOnly = True
        column013.Visible = False
        column013.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column013)
        Dim column014 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "ＦＡＸ番号",
                    .DataPropertyName = "HANM001013",
                    .Name = "HANM001013"
                }
        column014.ReadOnly = True
        column014.Visible = False
        column014.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column014)
        Dim column015 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "カスタマバーコード",
                    .DataPropertyName = "HANM001014",
                    .Name = "HANM001014"
                }
        column015.ReadOnly = True
        column015.Visible = False
        column015.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column015)
    End Sub

    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData()
        Me.DataGridView001.DataSource = Me.BizSearchTokui.GetTokui()
        Me.DataGridView001.CurrentCell = Nothing
    End Sub
    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData2()
        If (Not String.IsNullOrEmpty(Me.TextBox001.Text.Trim)) Or (Not String.IsNullOrEmpty(Me.TextBox002.Text.Trim)) Then
            Dim tokuisakiName As String
            tokuisakiName = Me.TextBox001.Text.Trim()
            'tokuisakiName = Replace(Me.TextBox001.Text.Trim, Space(1), String.Empty)
            'tokuisakiName = Replace(tokuisakiName, Chr(-32448), String.Empty)
            Dim sakuin As String
            sakuin = Me.TextBox002.Text.Trim()
            'sakuin = Replace(Me.TextBox002.Text.Trim, Space(1), String.Empty)
            'sakuin = Replace(sakuin, Chr(-32448), String.Empty)
            Me.DataGridView001.DataSource = Me.BizSearchTokui.GetTokui(tokuisakiName, sakuin)
            Me.DataGridView001.CurrentCell = Nothing
        Else
            Me.DataGridView001.DataSource = Me.BizSearchTokui.GetTokui()
            Me.DataGridView001.CurrentCell = Nothing
        End If

    End Sub

    ''' <summary>
    ''' 閉じるボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub DataGridView001_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellContentClick
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                Dim selectRow As DataGridViewRow = Me.DataGridView001.Rows(e.RowIndex)
                Me.Selected = New Models.HAN10M001TOKUI With {
                    .HANM001001 = selectRow.Cells("HANM001001").Value.ToString.Trim,
                    .HANM001002 = selectRow.Cells("HANM001002").Value,
                    .HANM001003 = selectRow.Cells("HANM001003").Value.ToString.Trim,
                    .HANM001004 = selectRow.Cells("HANM001004").Value.ToString.Trim,
                    .HANM001005 = selectRow.Cells("HANM001005").Value.ToString.Trim,
                    .HANM001006 = selectRow.Cells("HANM001006").Value.ToString.Trim,
                    .HANM001007 = selectRow.Cells("HANM001007").Value.ToString.Trim,
                    .HANM001008 = selectRow.Cells("HANM001008").Value.ToString.Trim,
                    .HANM001009 = selectRow.Cells("HANM001009").Value.ToString.Trim,
                    .HANM001010 = selectRow.Cells("HANM001010").Value.ToString.Trim,
                    .HANM001011 = selectRow.Cells("HANM001011").Value.ToString.Trim,
                    .HANM001012 = selectRow.Cells("HANM001012").Value.ToString.Trim,
                    .HANM001013 = selectRow.Cells("HANM001013").Value.ToString.Trim,
                    .HANM001014 = selectRow.Cells("HANM001014").Value.ToString.Trim
                    }
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub
    ''' <summary>
    ''' 検索ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Me.SetData2()
        DataGridView001.Font = New Font(“メイリオ”, 10)
    End Sub
End Class