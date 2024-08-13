Imports System.Configuration
Imports MitsumoLib
Public Class FormMTMSearchUser

    Public Property Selected As Models.MTM00M002USER

    ''' <summary>
    ''' 担当者検索ビジネスロジック
    ''' </summary>
    Private ReadOnly BizSearchUser As Biz.MTMSearchUser

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSearchUser = New Biz.MTMSearchUser(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                .HeaderText = "ログインID",
                .DataPropertyName = "MTMM002001",
                .Name = "MTMM002001"
            }
        column002.Width = 180
        column002.ReadOnly = True
        column002.SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView001.Columns.Add(column002)
        Dim column003 As New DataGridViewTextBoxColumn With {
                .HeaderText = "ログイン名",
                .DataPropertyName = "MTMM002002",
                .Name = "MTMM002002"
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
        Me.DataGridView001.DataSource = Me.BizSearchUser.GetUser()
        Me.DataGridView001.CurrentCell = Nothing
    End Sub

    ''' <summary>
    ''' 閉じるボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
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
                Me.Selected = New Models.MTM00M002USER With {
                    .MTMM002001 = selectRow.Cells("MTMM002001").Value.ToString.Trim,
                    .MTMM002002 = selectRow.Cells("MTMM002002").Value.ToString.Trim
                }

                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub
End Class