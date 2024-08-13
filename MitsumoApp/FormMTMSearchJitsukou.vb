Imports System.Configuration
Imports MitsumoLib
Public Class FormMTMSearchJitsukou

    Public Property Selected As Models.MTM10R003JITSUKOU

    ''' <summary>
    ''' 価格入力番号検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchJitsukou As Biz.MTMSearchJitsukou

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchJitsukou = New Biz.MTMSearchJitsukou(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMJitsukouIdx_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        column001.DefaultCellStyle.Font = New Font("メイリオ", 9, FontStyle.Underline)
        column001.HeaderCell.Style.Font = New Font("メイリオ", 12, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column001)
        Dim column002 As New DataGridViewTextBoxColumn With {
                .HeaderText = "締切日",
                .DataPropertyName = "MTMR003007",
                .Name = "MTMR003007"
            }
        column002.Width = 200
        column002.ReadOnly = True
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "価格入力名",
                .DataPropertyName = "MTMR003002",
                .Name = "MTMR003002"
            }
        column003.Width = 450
        column003.ReadOnly = True
        column003.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column003)
        Dim column003_1 As New DataGridViewTextBoxColumn With {
                .HeaderText = "一斉送信日時",
                .DataPropertyName = "MTMR003005_KETUGOU",
                .Name = "MTMR003005_KETUGOU"
            }
        column003_1.Width = 200
        column003_1.ReadOnly = True
        column003_1.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column003_1)
        Dim column004 As New DataGridViewTextBoxColumn With {
                .HeaderText = "取込担当者コード",
                .DataPropertyName = "MTMR003003",
                .Name = "MTMR003003"
            }
        column004.Width = 150
        column004.ReadOnly = True
        column004.Visible = False
        column004.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column004)
        Dim column005 As New DataGridViewTextBoxColumn With {
                .HeaderText = "取込年月日",
                .DataPropertyName = "MTMR003004",
                .Name = "MTMR003004"
            }
        column005.Width = 100
        column005.ReadOnly = True
        column005.Visible = False
        column005.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column005)
        Dim column006 As New DataGridViewTextBoxColumn With {
                .HeaderText = "一斉送信日付",
                .DataPropertyName = "MTMR003005",
                .Name = "MTMR003005"
            }
        column006.Width = 200
        column006.ReadOnly = True
        column006.Visible = False
        column006.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column006)
        Dim column007 As New DataGridViewTextBoxColumn With {
                .HeaderText = "一斉送信時間",
                .DataPropertyName = "MTMR003005_2",
                .Name = "MTMR003005_2"
            }
        column007.Width = 200
        column007.ReadOnly = True
        column007.Visible = False
        column007.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column007)
        Dim column008 As New DataGridViewTextBoxColumn With {
                .HeaderText = "更新日付",
                .DataPropertyName = "MTMR003006",
                .Name = "MTMR003006"
            }
        column008.Width = 100
        column008.ReadOnly = True
        column008.Visible = False
        column008.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column008)
        Dim column009 As New DataGridViewTextBoxColumn With {
                .HeaderText = "更新時間",
                .DataPropertyName = "MTMR003006_2",
                .Name = "MTMR003006_2"
            }
        column009.Width = 100
        column009.ReadOnly = True
        column009.Visible = False
        column009.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column009)
        Dim column010 As New DataGridViewTextBoxColumn With {
                .HeaderText = "価格入力番号",
                .DataPropertyName = "MTMR003001",
                .Name = "MTMR003001"
            }
        column010.Width = 200
        column010.ReadOnly = True
        column010.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column010)
        Dim column011 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "運賃の指定",
                    .DataPropertyName = "MTMR003008",
                    .Name = "MTMR003008"
                }
        column011.Width = 100
        column011.ReadOnly = True
        column011.Visible = False
        column011.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column011)
        Dim column012 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "案内文",
                    .DataPropertyName = "MTMR003009",
                    .Name = "MTMR003009"
                }
        column012.Width = 250
        column012.ReadOnly = True
        column012.Visible = False
        column012.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column012)
        Dim column013 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "取込元ファイル名",
                    .DataPropertyName = "MTMR003010",
                    .Name = "MTMR003010"
                }
        column013.Width = 250
        column013.ReadOnly = True
        column013.Visible = False
        column013.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column013)
        Dim column014 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "値下前売単価",
                    .DataPropertyName = "MTMR003011",
                    .Name = "MTMR003011"
                }
        column014.Width = 120
        column014.ReadOnly = True
        column014.Visible = False
        column014.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column014)
        Dim column015 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "値下前仕入単価",
                    .DataPropertyName = "MTMR003012",
                    .Name = "MTMR003012"
                }
        column015.Width = 130
        column015.ReadOnly = True
        column015.Visible = False
        column015.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column015)
        Dim column016 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "値下前粗利率",
                    .DataPropertyName = "MTMR003013",
                    .Name = "MTMR003013"
                }
        column016.Width = 120
        column016.ReadOnly = True
        column016.Visible = False
        column016.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column016)
        Dim column017 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "現売㎡単価",
                    .DataPropertyName = "MTMR003014",
                    .Name = "MTMR003014"
                }
        column017.Width = 120
        column017.ReadOnly = True
        column017.Visible = False
        column017.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column017)
        Dim column018 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "現仕㎡単価",
                    .DataPropertyName = "MTMR003015",
                    .Name = "MTMR003015"
                }
        column018.Width = 120
        column018.ReadOnly = True
        column018.Visible = False
        column018.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column018)
        Dim column019 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "新売㎡単価",
                    .DataPropertyName = "MTMR003016",
                    .Name = "MTMR003016"
                }
        column019.Width = 120
        column019.ReadOnly = True
        column019.Visible = False
        column019.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column019)
        Dim column020 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "新仕㎡単価",
                    .DataPropertyName = "MTMR003017",
                    .Name = "MTMR003017"
                }
        column020.Width = 120
        column020.ReadOnly = True
        column020.Visible = False
        column020.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column020)
        Dim column021 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "取込件数",
                    .DataPropertyName = "MTMR003018",
                    .Name = "MTMR003018"
                }
        column021.Width = 100
        column021.ReadOnly = True
        column021.Visible = False
        column021.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column021)
        Dim column022 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "取込開始時間",
                    .DataPropertyName = "MTMR003019",
                    .Name = "MTMR003019"
                }
        column022.Width = 120
        column022.ReadOnly = True
        column022.Visible = False
        column022.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column022)
        Dim column023 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "取込終了時間",
                    .DataPropertyName = "MTMR003020",
                    .Name = "MTMR003020"
                }
        column023.Width = 120
        column023.ReadOnly = True
        column023.Visible = False
        column023.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column023)
        Dim column024 As New DataGridViewTextBoxColumn With {
                    .HeaderText = "改定実施日",
                    .DataPropertyName = "MTMR003021",
                    .Name = "MTMR003021"
                }
        column024.Width = 100
        column024.ReadOnly = True
        column024.Visible = False
        column024.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column024)

    End Sub
    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData()
        Me.DataGridView001.DataSource = Me.BizSearchJitsukou.GetJitsukou()
        Me.DataGridView001.CurrentCell = Nothing
    End Sub
    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData2()
        Me.DataGridView001.DataSource = Me.BizSearchJitsukou.GetJitsukouAll()
        Me.DataGridView001.CurrentCell = Nothing
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
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellContentClick
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                Dim selectRow As DataGridViewRow = Me.DataGridView001.Rows(e.RowIndex)
                Me.Selected = New Models.MTM10R003JITSUKOU With {
                    .MTMR003001 = selectRow.Cells("MTMR003001").Value,
                    .MTMR003002 = selectRow.Cells("MTMR003002").Value.ToString.Trim,
                    .MTMR003003 = selectRow.Cells("MTMR003003").Value.ToString,
                    .MTMR003004 = selectRow.Cells("MTMR003004").Value.ToString,
                    .MTMR003005 = selectRow.Cells("MTMR003005").Value.ToString,
                    .MTMR003005_2 = selectRow.Cells("MTMR003005_2").Value.ToString,
                    .MTMR003006 = selectRow.Cells("MTMR003006").Value.ToString,
                    .MTMR003006_2 = selectRow.Cells("MTMR003006_2").Value.ToString,
                    .MTMR003007 = selectRow.Cells("MTMR003007").Value.ToString,
                    .MTMR003008 = selectRow.Cells("MTMR003008").Value.ToString,
                    .MTMR003009 = selectRow.Cells("MTMR003009").Value.ToString,
                    .MTMR003010 = selectRow.Cells("MTMR003010").Value.ToString,
                    .MTMR003011 = selectRow.Cells("MTMR003011").Value.ToString,
                    .MTMR003012 = selectRow.Cells("MTMR003012").Value.ToString,
                    .MTMR003013 = selectRow.Cells("MTMR003013").Value.ToString,
                    .MTMR003014 = selectRow.Cells("MTMR003014").Value.ToString,
                    .MTMR003015 = selectRow.Cells("MTMR003015").Value.ToString,
                    .MTMR003016 = selectRow.Cells("MTMR003016").Value.ToString,
                    .MTMR003017 = selectRow.Cells("MTMR003017").Value.ToString,
                    .MTMR003018 = selectRow.Cells("MTMR003018").Value,
                    .MTMR003019 = selectRow.Cells("MTMR003019").Value.ToString,
                    .MTMR003020 = selectRow.Cells("MTMR003020").Value.ToString,
                    .MTMR003021 = selectRow.Cells("MTMR003021").Value.ToString
                }
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.SetData2()
        DataGridView001.Font = New Font(“メイリオ”, 10)
    End Sub
    ''' <summary>
    ''' フォームイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchJitsukou_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then
            Me.SetData2()
            DataGridView001.Font = New Font(“メイリオ”, 10)
        ElseIf e.KeyCode = Keys.F11 Then
            Me.SetData()
            DataGridView001.Font = New Font(“メイリオ”, 10)
        End If
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.SetData()
        DataGridView001.Font = New Font(“メイリオ”, 10)
    End Sub
End Class