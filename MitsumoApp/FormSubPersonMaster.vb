Imports System.Configuration
Imports MitsumoLib

Public Class FormSubPersonMaster

    ''' <summary>
    ''' 呼出し元フォーム
    ''' </summary>
    Private ReadOnly Form01 As FormMTM01
    Private ReadOnly Form02 As FormMTM02
    Private TargetForm As String
    ''' <summary>
    ''' リスト選択
    ''' </summary>
    Private ReadOnly BizSubPersonMaster As Biz.SubPersonMaster
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="form01"></param>
    Public Sub New(ByVal form01 As FormMTM01)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.Form01 = form01
        TargetForm = "form01"
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSubPersonMaster = New Biz.SubPersonMaster(connectionString)
    End Sub
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="form02"></param>
    Public Sub New(ByVal form02 As FormMTM02)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.Form02 = form02
        TargetForm = "form02"
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizSubPersonMaster = New Biz.SubPersonMaster(connectionString)
    End Sub
    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMUserList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetDataGridColumn()
        Me.SetGridDataSource()
    End Sub
    ''' <summary>
    ''' データグリッド項目セット
    ''' </summary>
    Private Sub SetDataGridColumn()

        '担当者マスターより選択リストを表示
        Me.DataGridView001.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Dim column001 As New DataGridViewButtonColumn With {
            .HeaderText = "選択",
            .Name = "SelectButton1"
        }
        column001.Width = 60
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
        column002.Width = 150
        column002.ReadOnly = True
        column002.DefaultCellStyle.BackColor = Color.LightGray
        column002.HeaderCell.Style.Font = New Font("ＭＳ 明朝", 12, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column002)

        Dim column003 As New DataGridViewTextBoxColumn With {
            .HeaderText = "担当者名",
            .DataPropertyName = "HANM004002",
            .Name = "HANM004002"
        }
        column003.Width = 600
        column003.ReadOnly = True
        column003.DefaultCellStyle.BackColor = Color.LightGray
        column002.HeaderCell.Style.Font = New Font("ＭＳ 明朝", 12, FontStyle.Underline)
        Me.DataGridView001.Columns.Add(column003)

    End Sub
    ''' <summary>
    ''' データグリッドデータソースセット
    ''' </summary>
    Private Sub SetGridDataSource()

        Dim dtDataGridView001 As New DataTable
        dtDataGridView001 = Me.BizSubPersonMaster.PersonMasterList()
        If (dtDataGridView001.Rows.Count > 0) Then
            Me.DataGridView001.DataSource = dtDataGridView001
        Else
            MessageBox.Show("担当者マスタにリストがありません")
        End If


    End Sub
    ''' <summary>
    ''' 閉じる処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 閉じるボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMUserList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If (TargetForm = "form01") Then
            Me.Form01.Show()
        ElseIf (TargetForm = "form02") Then
            Me.Form02.Show()
        End If
    End Sub
    ''' <summary>
    ''' データグリッドボタン選択時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView001_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView001.CellContentClick
        Dim ColIndex As Integer = e.ColumnIndex
        Dim RowIndex As Integer = e.RowIndex
        Dim selHANM004001 As String = ""
        Dim selHANM004002 As String = ""

        Try
            If ColIndex = 0 Then
                selHANM004001 = DataGridView001.Rows(RowIndex).Cells(1).Value
                selHANM004002 = DataGridView001.Rows(RowIndex).Cells(2).Value
            End If

            '画面へ戻す
            If (TargetForm = "form01") Then
                'FormMTM01画面用戻し処理
                Me.Form01.TextBox003.Text = selHANM004001
                Me.Form01.TextBox004.Text = selHANM004002
                Me.Form01.Show()
                Me.Close()
            ElseIf (TargetForm = "form02") Then
                'FormMTM02画面用戻し処理
                Me.Form02.Show()
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("選択時にエラーが発生しました。")
        End Try

    End Sub
End Class