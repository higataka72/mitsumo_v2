Imports System.Configuration
Imports C1.Win.C1FlexGrid
Imports MitsumoLib
Public Class FormMTMSearchTankaV2

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
        DataGridView001.AllowSorting = AllowSortingEnum.Auto
        Me.SetData()
        Me.SetDataGridColumn()
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
        Dim rowNo As Integer = 1

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
            DataGridView001.SetData(rowNo, 7, setValue)
            DataGridView001.FinishEditing()
            'Me.DataGridView001(rowNo, 6).Value = setValue
            rowNo += 1
        Next
    End Sub
    ''' <summary>
    ''' データグリッド項目の設定
    ''' </summary>
    Private Sub SetDataGridColumn()
        ' -------- FlexGridの共通 --------
        DataGridView001.Rows(0).Height = 30
        DataGridView001.Styles.Normal.WordWrap = True
        DataGridView001.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        DataGridView001.Styles.Focus.BackColor = DataGridView001.Styles.Highlight.BackColor

        ' -------- FlexGridのスタイル --------
        ' checkBoxStyle1　列ヘッダーにチェックボックスを設定
        Dim checkBoxStyle1 As CellStyle = DataGridView001.Styles.Add("CheckBoxStyle1")
        checkBoxStyle1.BackColor = Color.LightSeaGreen
        checkBoxStyle1.ForeColor = Color.White
        checkBoxStyle1.TextAlign = TextAlignEnum.CenterCenter
        checkBoxStyle1.Font = New Font("メイリオ", 8, FontStyle.Underline)
        checkBoxStyle1.DataType = GetType(Boolean)
        checkBoxStyle1.ImageAlign = ImageAlignEnum.CenterCenter

        ' checkBoxStyle1　列ヘッダーにチェックボックスを設定なし（ノーマル）
        Dim checkBoxStyle2 As CellStyle = DataGridView001.Styles.Add("CheckBoxStyle2")
        checkBoxStyle2.BackColor = Color.LightSeaGreen
        checkBoxStyle2.ForeColor = Color.White
        checkBoxStyle2.TextAlign = TextAlignEnum.CenterCenter
        checkBoxStyle2.Font = New Font("メイリオ", 8, FontStyle.Underline)

        ' NewStyle1
        Dim newStyle1 As CellStyle = DataGridView001.Styles.Add("NewStyle1")
        newStyle1.BackColor = Color.LightSeaGreen
        newStyle1.ForeColor = Color.White
        newStyle1.TextAlign = TextAlignEnum.CenterCenter
        newStyle1.Font = New Font("メイリオ", 8, FontStyle.Regular)

        Dim newStyle1_1 As CellStyle = DataGridView001.Styles.Add("NewStyle1_1")
        newStyle1_1.BackColor = Color.LightSeaGreen
        newStyle1_1.ForeColor = Color.White
        newStyle1_1.TextAlign = TextAlignEnum.CenterCenter
        newStyle1_1.Font = New Font("メイリオ", 8, FontStyle.Underline)

        Dim newStyle2 As CellStyle = DataGridView001.Styles.Add("NewStyle2")
        newStyle2.BackColor = Color.LightSeaGreen
        newStyle2.ForeColor = Color.White
        newStyle2.TextAlign = TextAlignEnum.CenterCenter
        newStyle2.Font = New Font("メイリオ", 9, FontStyle.Regular)

        Dim newStyle3 As CellStyle = DataGridView001.Styles.Add("NewStyle3")
        newStyle3.BackColor = Color.LightSeaGreen
        newStyle3.ForeColor = Color.White
        newStyle3.TextAlign = TextAlignEnum.CenterCenter
        newStyle3.Font = New Font("メイリオ", 9, FontStyle.Underline)

        ' -------- FlexGridの各項目設定 --------
        DataGridView001.Cols(0).Visible = False
        ' ヘッダーの設定
        DataGridView001.Cols(1).Caption = "商品C"
        DataGridView001.Cols(1).DataType = GetType(String)
        DataGridView001.Cols(1).Name = "HANMA02002"
        DataGridView001.Cols(1).AllowSorting = False
        DataGridView001.Cols(1).Width = 90
        DataGridView001.Cols(1).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(1).AllowEditing = False
        DataGridView001.Cols(1).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 1, newStyle2)

        DataGridView001.Cols(2).Caption = "商品名１"
        DataGridView001.Cols(2).DataType = GetType(String)
        DataGridView001.Cols(2).Name = "HANM003103"
        DataGridView001.Cols(2).AllowSorting = False
        DataGridView001.Cols(2).Width = 250
        DataGridView001.Cols(2).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(2).AllowEditing = False
        DataGridView001.Cols(2).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 2, newStyle2)

        DataGridView001.Cols(3).Caption = "商品名２"
        DataGridView001.Cols(3).DataType = GetType(String)
        DataGridView001.Cols(3).Name = "HANM003104"
        DataGridView001.Cols(3).AllowSorting = False
        DataGridView001.Cols(3).Width = 150
        DataGridView001.Cols(3).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(3).AllowEditing = False
        DataGridView001.Cols(3).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 3, newStyle2)

        DataGridView001.Cols(4).Caption = "規格"
        DataGridView001.Cols(4).DataType = GetType(String)
        DataGridView001.Cols(4).Name = "HANMA02003"
        DataGridView001.Cols(4).AllowSorting = False
        DataGridView001.Cols(4).Width = 80
        DataGridView001.Cols(4).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(4).AllowEditing = False
        DataGridView001.Cols(4).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 4, newStyle2)

        DataGridView001.Cols(5).Caption = "入数"
        DataGridView001.Cols(5).DataType = GetType(String)
        DataGridView001.Cols(5).Name = "HANMA01006"
        DataGridView001.Cols(5).AllowSorting = False
        DataGridView001.Cols(5).Width = 60
        DataGridView001.Cols(5).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(5).AllowEditing = False
        DataGridView001.Cols(5).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 5, newStyle2)

        DataGridView001.Cols(6).Caption = "単位"
        DataGridView001.Cols(6).DataType = GetType(String)
        DataGridView001.Cols(6).Name = "HANMA01007"
        DataGridView001.Cols(6).AllowSorting = False
        DataGridView001.Cols(6).Width = 60
        DataGridView001.Cols(6).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(6).AllowEditing = False
        DataGridView001.Cols(6).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 6, newStyle2)

        DataGridView001.Cols(7).Caption = "数量"
        DataGridView001.Cols(7).DataType = GetType(String)
        DataGridView001.Cols(7).Name = "HANMA02006"
        DataGridView001.Cols(7).AllowSorting = False
        DataGridView001.Cols(7).Width = 120
        DataGridView001.Cols(7).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(7).AllowEditing = False
        DataGridView001.Cols(7).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 7, newStyle2)

        DataGridView001.Cols(8).Caption = "売上単価"
        DataGridView001.Cols(8).DataType = GetType(String)
        DataGridView001.Cols(8).Name = "HANMA02007"
        DataGridView001.Cols(8).AllowSorting = False
        DataGridView001.Cols(8).Width = 80
        DataGridView001.Cols(8).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(8).AllowEditing = False
        DataGridView001.Cols(8).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 8, newStyle2)

        DataGridView001.Cols(9).Caption = "仕入単価"
        DataGridView001.Cols(9).DataType = GetType(String)
        DataGridView001.Cols(9).Name = "HANMA02008"
        DataGridView001.Cols(9).AllowSorting = False
        DataGridView001.Cols(9).Width = 80
        DataGridView001.Cols(9).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(9).AllowEditing = False
        DataGridView001.Cols(9).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 9, newStyle2)

        DataGridView001.Cols(10).Caption = "粗利率"
        DataGridView001.Cols(10).DataType = GetType(String)
        DataGridView001.Cols(10).Name = "ARARI"
        DataGridView001.Cols(10).AllowSorting = False
        DataGridView001.Cols(10).Width = 80
        DataGridView001.Cols(10).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(10).AllowEditing = False
        DataGridView001.Cols(10).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 10, newStyle2)

        DataGridView001.Cols(11).Caption = "最終売上日"
        DataGridView001.Cols(11).DataType = GetType(String)
        DataGridView001.Cols(11).Name = "HANMA01022"
        DataGridView001.Cols(11).AllowSorting = False
        DataGridView001.Cols(11).Width = 80
        DataGridView001.Cols(11).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(11).AllowEditing = False
        DataGridView001.Cols(11).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 11, newStyle2)

        DataGridView001.Cols(12).Caption = "売単価（新）"
        DataGridView001.Cols(12).DataType = GetType(String)
        DataGridView001.Cols(12).Name = "HANMA02011"
        DataGridView001.Cols(12).AllowSorting = False
        DataGridView001.Cols(12).Width = 100
        DataGridView001.Cols(12).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(12).AllowEditing = False
        DataGridView001.Cols(12).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 12, newStyle2)

        DataGridView001.Cols(13).Caption = "新売単価更新日"
        DataGridView001.Cols(13).DataType = GetType(String)
        DataGridView001.Cols(13).Name = "HANMA02009"
        DataGridView001.Cols(13).AllowSorting = False
        DataGridView001.Cols(13).Width = 110
        DataGridView001.Cols(13).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(13).AllowEditing = False
        DataGridView001.Cols(13).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 13, newStyle2)

        DataGridView001.Cols(14).Caption = "仕単価（新）"
        DataGridView001.Cols(14).DataType = GetType(String)
        DataGridView001.Cols(14).Name = "HANMA02012"
        DataGridView001.Cols(14).AllowSorting = False
        DataGridView001.Cols(14).Width = 100
        DataGridView001.Cols(14).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(14).AllowEditing = False
        DataGridView001.Cols(14).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 14, newStyle2)

        DataGridView001.Cols(15).Caption = "新仕単価更新日"
        DataGridView001.Cols(15).DataType = GetType(String)
        DataGridView001.Cols(15).Name = "HANMA02010"
        DataGridView001.Cols(15).AllowSorting = False
        DataGridView001.Cols(15).Width = 110
        DataGridView001.Cols(15).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(15).AllowEditing = False
        DataGridView001.Cols(15).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 15, newStyle2)

        DataGridView001.Cols(16).Caption = "新粗利率"
        DataGridView001.Cols(16).DataType = GetType(String)
        DataGridView001.Cols(16).Name = "SHIN_ARARI"
        DataGridView001.Cols(16).AllowSorting = False
        DataGridView001.Cols(16).Width = 100
        DataGridView001.Cols(16).TextAlign = TextAlignEnum.RightCenter
        DataGridView001.Cols(16).AllowEditing = False
        DataGridView001.Cols(16).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 16, newStyle2)

        DataGridView001.Cols(17).Caption = "手配"
        DataGridView001.Cols(17).DataType = GetType(String)
        DataGridView001.Cols(17).Name = "HANM031003"
        DataGridView001.Cols(17).AllowSorting = False
        DataGridView001.Cols(17).Width = 60
        DataGridView001.Cols(17).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(17).AllowEditing = False
        DataGridView001.Cols(17).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 17, newStyle2)

        DataGridView001.Cols(18).Caption = "発注摘要"
        DataGridView001.Cols(18).DataType = GetType(String)
        DataGridView001.Cols(18).Name = "HANMA01013"
        DataGridView001.Cols(18).AllowSorting = False
        DataGridView001.Cols(18).Width = 150
        DataGridView001.Cols(18).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(18).AllowEditing = False
        DataGridView001.Cols(18).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 18, newStyle2)

        DataGridView001.Cols(19).Caption = "社内摘要"
        DataGridView001.Cols(19).DataType = GetType(String)
        DataGridView001.Cols(19).Name = "HANMA01014"
        DataGridView001.Cols(19).AllowSorting = False
        DataGridView001.Cols(19).Width = 200
        DataGridView001.Cols(19).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(19).AllowEditing = False
        DataGridView001.Cols(19).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 19, newStyle2)

        DataGridView001.Cols(20).Caption = "運賃適用"
        DataGridView001.Cols(20).DataType = GetType(String)
        DataGridView001.Cols(20).Name = "HANMA01042"
        DataGridView001.Cols(20).AllowSorting = False
        DataGridView001.Cols(20).Width = 200
        DataGridView001.Cols(20).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(20).AllowEditing = False
        DataGridView001.Cols(20).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 20, newStyle2)

        DataGridView001.Cols(21).Caption = "仕入先C"
        DataGridView001.Cols(21).DataType = GetType(String)
        DataGridView001.Cols(21).Name = "HANMA01019"
        DataGridView001.Cols(21).AllowSorting = False
        DataGridView001.Cols(21).Width = 80
        DataGridView001.Cols(21).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(21).AllowEditing = False
        DataGridView001.Cols(21).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 21, newStyle2)

        DataGridView001.Cols(22).Caption = "仕入先名"
        DataGridView001.Cols(22).DataType = GetType(String)
        DataGridView001.Cols(22).Name = "HANM002006"
        DataGridView001.Cols(22).AllowSorting = False
        DataGridView001.Cols(22).Width = 250
        DataGridView001.Cols(22).TextAlign = TextAlignEnum.LeftCenter
        DataGridView001.Cols(22).AllowEditing = False
        DataGridView001.Cols(22).Style.BackColor = Color.White
        DataGridView001.SetCellStyle(0, 22, newStyle2)
    End Sub
    ''' <summary>
    ''' フォームイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMSearchTanka_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Me.DataGridView001.Rows.Count > 0 Then
            If e.KeyCode = Keys.F5 Then
                DataGridView001.SortDefinition = String.Empty
            End If
        End If
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
    Private Sub DataGridView001_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)

    End Sub
End Class