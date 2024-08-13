Imports System.Configuration
Imports MitsumoLib
Public Class FormMTMSearchShohin
    Public Property Selected As Models.HAN10M003SHOHIN

    ''' <summary>
    ''' 商品コード検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchShohin As Biz.MTMSearchShohin

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchShohin = New Biz.MTMSearchShohin(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchShohin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                .HeaderText = "商品コード",
                .DataPropertyName = "HANM003001",
                .Name = "HANM003001"
            }
        column002.Width = 180
        column002.ReadOnly = True
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品名１",
                .DataPropertyName = "HANM003103",
                .Name = "HANM003103"
            }
        column003.Width = 400
        column003.ReadOnly = True
        column003.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column003)
        Dim column004 As New DataGridViewTextBoxColumn With {
                .HeaderText = "商品名２",
                .DataPropertyName = "HANM003104",
                .Name = "HANM003104"
            }
        column004.Width = 350
        column004.ReadOnly = True
        column004.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column004)
    End Sub

    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData()
        Me.DataGridView001.DataSource = Me.BizSearchShohin.GetShohin()
        Me.DataGridView001.CurrentCell = Nothing
    End Sub
    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData2()
        If (Not String.IsNullOrEmpty(Me.TextBox001.Text.Trim)) Then
            Dim shouhinName As String
            'shouhinName = Replace(Me.TextBox001.Text.Trim, Space(1), String.Empty)
            'shouhinName = Replace(shouhinName, Chr(-32448), String.Empty)
            shouhinName = Me.TextBox001.Text.Trim
            Me.DataGridView001.DataSource = Me.BizSearchShohin.GetShohin(shouhinName)
            Me.DataGridView001.CurrentCell = Nothing
        Else
            Me.DataGridView001.DataSource = Me.BizSearchShohin.GetShohin()
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
    ''' <summary>
    ''' データグリッドイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellContentClick
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                Dim selectRow As DataGridViewRow = Me.DataGridView001.Rows(e.RowIndex)
                Me.Selected = New Models.HAN10M003SHOHIN With {
                    .HANM003001 = selectRow.Cells("HANM003001").Value.ToString.Trim,
                    .HANM003002 = selectRow.Cells("HANM003103").Value.ToString.Trim,
                    .HANM003003 = selectRow.Cells("HANM003104").Value.ToString.Trim
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