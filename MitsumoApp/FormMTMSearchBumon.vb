Imports System.Configuration
Imports MitsumoLib
Public Class FormMTMSearchBumon
    Public Property Selected As Models.HAN10M015BUMON

    ''' <summary>
    ''' 部門コード検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchBumon As Biz.MTMSearchBumon

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchBumon = New Biz.MTMSearchBumon(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchBumon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                .HeaderText = "部門コード",
                .DataPropertyName = "HANM015001",
                .Name = "HANM015001"
            }
        column002.Width = 180
        column002.ReadOnly = True
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "部門名",
                .DataPropertyName = "HANM015002",
                .Name = "HANM015002"
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
        Me.DataGridView001.DataSource = Me.BizSearchBumon.GetBumon()
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
                Me.Selected = New Models.HAN10M015BUMON With {
                    .HANM015001 = selectRow.Cells("HANM015001").Value.ToString.Trim,
                    .HANM015002 = selectRow.Cells("HANM015002").Value.ToString.Trim
                    }
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub
End Class