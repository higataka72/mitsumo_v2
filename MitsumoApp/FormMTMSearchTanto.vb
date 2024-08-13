Imports System.Configuration
Imports MitsumoLib

Public Class FormMTMSearchTanto

    Public Property Selected As Models.HAN10M004TANTO

    ''' <summary>
    ''' ログインID検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchTanto As Biz.MTMSearchTanto

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchTanto = New Biz.MTMSearchTanto(connectionString)
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
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchTanto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                .HeaderText = "担当者コード",
                .DataPropertyName = "HANM004001",
                .Name = "HANM004001"
            }
        column002.Width = 180
        column002.ReadOnly = True
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "担当者名",
                .DataPropertyName = "HANM004002",
                .Name = "HANM004002"
            }
        column003.Width = 450
        column003.ReadOnly = True
        column003.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column003)
    End Sub
    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData()
        Me.DataGridView001.DataSource = Me.BizSearchTanto.GetTanto()
        Me.DataGridView001.CurrentCell = Nothing
    End Sub
    ''' <summary>
    ''' データ設定
    ''' </summary>
    Public Sub SetData2()
        If (Not String.IsNullOrEmpty(Me.TextBox001.Text.Trim)) Then
            Dim tantousyaName As String
            tantousyaName = Me.TextBox001.Text.Trim()
            'tantousyaName = Replace(Me.TextBox001.Text.Trim, Space(1), String.Empty)
            'tantousyaName = Replace(tantousyaName, Chr(-32448), String.Empty)
            Me.DataGridView001.DataSource = Me.BizSearchTanto.GetTanto(tantousyaName)
            Me.DataGridView001.CurrentCell = Nothing
        Else
            Me.DataGridView001.DataSource = Me.BizSearchTanto.GetTanto()
            Me.DataGridView001.CurrentCell = Nothing
        End If
    End Sub
    ''' <summary>
    ''' 選択ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellContentClick
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                Dim selectRow As DataGridViewRow = Me.DataGridView001.Rows(e.RowIndex)
                Me.Selected = New Models.HAN10M004TANTO With {
                    .HANM004001 = selectRow.Cells("HANM004001").Value.ToString.Trim,
                    .HANM004002 = selectRow.Cells("HANM004002").Value.ToString.Trim
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
    Private Sub ButtonSearch_Click_1(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Me.SetData2()
        DataGridView001.Font = New Font(“メイリオ”, 10)
    End Sub
End Class