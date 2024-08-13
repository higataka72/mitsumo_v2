Imports System.Configuration
Imports MitsumoLib
Public Class FormMTM02

    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' 価格入力ビジネスロジック
    ''' </summary>
    Private ReadOnly Biz02 As Biz.MTM02

    Public ReadOnly SearchCondition As Biz.MTM02SearchCondition

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="formMenu"></param>
    Public Sub New(ByVal formMenu As FormMTMMenu)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.FormMenu = formMenu
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.Biz02 = New Biz.MTM02(connectionString)
        Me.SearchCondition = New Biz.MTM02SearchCondition
    End Sub
    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM02_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM02_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '条件指定
        Me.TextBox001.Text = ""
        Me.TextBox002.Text = ""
        Me.TextBox003.Text = Me.FormMenu.ModelMtmUser.MTMM002001.Trim
        Me.TextBox004.Text = Me.FormMenu.ModelMtmUser.MTMM002002
        'Me.TextBox003.Text = ""
        'Me.TextBox004.Text = ""
        Me.TextBox005.Text = ""
        Me.TextBox006.Text = ""
        Me.TextBox007.Text = ""
        Me.TextBox008.Text = ""
        Me.TextBox009.Text = ""
        Me.TextBox010.Text = ""
        Me.TextBox011.Text = ""
        Me.TextBox012.Text = ""
        Me.TextBox013.Text = ""
        Me.TextBox014.Text = ""
        Me.TextBox015.Text = ""
        Me.TextBox016.Text = ""
        Me.TextBox017.Text = ""
        Me.TextBox018.Text = ""
        Me.TextBox019.Text = ""
        Me.TextBox020.Text = ""
        Me.TextBox021.Text = ""
        Me.TextBox022.Text = ""
        Me.TextBox023.Text = ""
        Me.TextBox024.Text = ""
        Me.RadioButton001.Checked = True

        '個別指定
        Me.TextBox025.Text = ""
        Me.TextBox026.Text = ""
        Me.TextBox027.Text = ""
        Me.TextBox028.Text = ""
        Me.TextBox029.Text = ""
        Me.TextBox030.Text = ""
        Me.TextBox031.Text = ""
        Me.TextBox032.Text = ""
        Me.TextBox033.Text = ""
        Me.TextBox034.Text = ""
        Me.TextBox035.Text = ""
        Me.TextBox036.Text = ""
        Me.TextBox037.Text = ""
        Me.TextBox038.Text = ""
        Me.TextBox039.Text = ""
        Me.TextBox040.Text = ""
        Me.TextBox041.Text = ""
        Me.TextBox042.Text = ""
        Me.TextBox043.Text = ""
        Me.TextBox044.Text = ""
        Me.TextBox045.Text = ""
        Me.TextBox046.Text = ""
        Me.TextBox047.Text = ""
        Me.TextBox048.Text = ""
        Me.TextBox049.Text = ""
        Me.TextBox050.Text = ""
        Me.TextBox051.Text = ""
        Me.TextBox052.Text = ""
        Me.TextBox053.Text = ""
        Me.TextBox054.Text = ""
        Me.TextBox055.Text = ""
        Me.TextBox056.Text = ""
        Me.TextBox057.Text = ""
        Me.TextBox058.Text = ""
        Me.TextBox059.Text = ""
        Me.TextBox060.Text = ""
        Me.TextBox061.Text = ""
        Me.TextBox062.Text = ""
        Me.TextBox063.Text = ""
        Me.TextBox064.Text = ""
        Me.TextBox065.Text = ""
        Me.TextBox066.Text = ""
        Me.TextBox067.Text = ""
        Me.TextBox068.Text = ""
        Me.TextBox069.Text = ""
        Me.TextBox070.Text = ""
        Me.TextBox071.Text = ""
        Me.TextBox072.Text = ""
    End Sub
    ''' <summary>
    ''' 検索イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch001.Click, ButtonSearch002.Click
        Dim txt001 As String = Me.TextBox001.Text.Trim
        Dim txt003 As String = Me.TextBox003.Text.Trim
        Dim txt005 As String = Me.TextBox005.Text.Trim
        Dim txt007 As String = Me.TextBox007.Text.Trim
        Dim txt009 As String = Me.TextBox009.Text.Trim
        Dim txt011 As String = Me.TextBox011.Text.Trim
        Dim txt013 As String = Me.TextBox013.Text.Trim
        Dim txt015 As String = Me.TextBox015.Text.Trim
        Dim txt017 As String = Me.TextBox017.Text.Trim
        Dim txt019 As String = Me.TextBox019.Text.Trim
        Dim txt021 As String = Me.TextBox021.Text.Trim
        Dim txt023 As String = Me.TextBox023.Text.Trim
        Dim txt024 As String = Me.TextBox024.Text.Trim
        Dim txt025 As String = Me.TextBox025.Text.Trim
        Dim txt027 As String = Me.TextBox027.Text.Trim
        Dim txt029 As String = Me.TextBox029.Text.Trim
        Dim txt031 As String = Me.TextBox031.Text.Trim
        Dim txt033 As String = Me.TextBox033.Text.Trim
        Dim txt035 As String = Me.TextBox035.Text.Trim
        Dim txt037 As String = Me.TextBox037.Text.Trim
        Dim txt039 As String = Me.TextBox039.Text.Trim
        Dim txt041 As String = Me.TextBox041.Text.Trim
        Dim txt043 As String = Me.TextBox043.Text.Trim
        Dim txt045 As String = Me.TextBox045.Text.Trim
        Dim txt047 As String = Me.TextBox047.Text.Trim
        Dim txt049 As String = Me.TextBox049.Text.Trim
        Dim txt051 As String = Me.TextBox051.Text.Trim
        Dim txt053 As String = Me.TextBox053.Text.Trim
        Dim txt055 As String = Me.TextBox055.Text.Trim
        Dim txt057 As String = Me.TextBox057.Text.Trim
        Dim txt059 As String = Me.TextBox059.Text.Trim
        Dim txt061 As String = Me.TextBox061.Text.Trim
        Dim txt063 As String = Me.TextBox063.Text.Trim
        Dim txt065 As String = Me.TextBox065.Text.Trim
        Dim txt067 As String = Me.TextBox067.Text.Trim
        Dim txt069 As String = Me.TextBox069.Text.Trim
        Dim txt071 As String = Me.TextBox071.Text.Trim
        Dim rdo001 As Boolean = Me.RadioButton001.Checked
        Dim rdo002 As Boolean = Me.RadioButton002.Checked
        Dim rdo003 As Boolean = Me.RadioButton003.Checked

        Me.SearchCondition.Clear()
        Dim dml As Decimal
        Me.SearchCondition.KakakuNyuuryokuNo = txt001
        Me.SearchCondition.LoginId = txt003
        Me.SearchCondition.EigyosyoCode = txt005
        Me.SearchCondition.BukaCodeFrom = txt007
        Me.SearchCondition.BukaCodeTo = txt009
        Me.SearchCondition.TantousyaCodeFrom = txt011
        Me.SearchCondition.TantousyaCodeTo = txt013
        Me.SearchCondition.TokuisakiCodeFrom = txt015
        Me.SearchCondition.TokuisakiCodeTo = txt017
        Me.SearchCondition.ShohinCodeFrom = txt019
        Me.SearchCondition.ShohinCodeTo = txt021
        If (String.IsNullOrEmpty(txt023)) Then
            Me.SearchCondition.ShinArariFrom_bool = False
        Else
            Me.SearchCondition.ShinArariFrom_bool = True
            Me.SearchCondition.ShinArariFrom = If(Decimal.TryParse(txt023, dml), dml, 0)
        End If
        If (String.IsNullOrEmpty(txt024)) Then
            Me.SearchCondition.ShinArariTo_bool = False
        Else
            Me.SearchCondition.ShinArariTo_bool = True
            Me.SearchCondition.ShinArariTo = If(Decimal.TryParse(txt024, dml), dml, 0)
        End If
        Me.SearchCondition.Mikakutei = rdo001
        Me.SearchCondition.Kakutei = rdo002
        Me.SearchCondition.Zenken = rdo003
        If Not String.IsNullOrWhiteSpace(txt025) Then
            Me.SearchCondition.EigyosyoCodeList.Add(txt025)
        End If
        If Not String.IsNullOrWhiteSpace(txt027) Then
            Me.SearchCondition.EigyosyoCodeList.Add(txt027)
        End If
        If Not String.IsNullOrWhiteSpace(txt029) Then
            Me.SearchCondition.EigyosyoCodeList.Add(txt029)
        End If
        If Not String.IsNullOrWhiteSpace(txt031) Then
            Me.SearchCondition.EigyosyoCodeList.Add(txt031)
        End If
        If Not String.IsNullOrWhiteSpace(txt033) Then
            Me.SearchCondition.EigyosyoCodeList.Add(txt033)
        End If
        If Not String.IsNullOrWhiteSpace(txt035) Then
            Me.SearchCondition.EigyosyoCodeList.Add(txt035)
        End If
        If Not String.IsNullOrWhiteSpace(txt037) Then
            Me.SearchCondition.BukaCodeList.Add(txt037)
        End If
        If Not String.IsNullOrWhiteSpace(txt039) Then
            Me.SearchCondition.BukaCodeList.Add(txt039)
        End If
        If Not String.IsNullOrWhiteSpace(txt041) Then
            Me.SearchCondition.BukaCodeList.Add(txt041)
        End If
        If Not String.IsNullOrWhiteSpace(txt043) Then
            Me.SearchCondition.BukaCodeList.Add(txt043)
        End If
        If Not String.IsNullOrWhiteSpace(txt045) Then
            Me.SearchCondition.BukaCodeList.Add(txt045)
        End If
        If Not String.IsNullOrWhiteSpace(txt047) Then
            Me.SearchCondition.BukaCodeList.Add(txt047)
        End If
        If Not String.IsNullOrWhiteSpace(txt049) Then
            Me.SearchCondition.TantousyaCodeList.Add(txt049)
        End If
        If Not String.IsNullOrWhiteSpace(txt051) Then
            Me.SearchCondition.TantousyaCodeList.Add(txt051)
        End If
        If Not String.IsNullOrWhiteSpace(txt053) Then
            Me.SearchCondition.TantousyaCodeList.Add(txt053)
        End If
        If Not String.IsNullOrWhiteSpace(txt055) Then
            Me.SearchCondition.TantousyaCodeList.Add(txt055)
        End If
        If Not String.IsNullOrWhiteSpace(txt057) Then
            Me.SearchCondition.TantousyaCodeList.Add(txt057)
        End If
        If Not String.IsNullOrWhiteSpace(txt059) Then
            Me.SearchCondition.TantousyaCodeList.Add(txt059)
        End If
        If Not String.IsNullOrWhiteSpace(txt061) Then
            Me.SearchCondition.TokuisakiCodeList.Add(txt061)
        End If
        If Not String.IsNullOrWhiteSpace(txt063) Then
            Me.SearchCondition.TokuisakiCodeList.Add(txt063)
        End If
        If Not String.IsNullOrWhiteSpace(txt065) Then
            Me.SearchCondition.TokuisakiCodeList.Add(txt065)
        End If
        If Not String.IsNullOrWhiteSpace(txt067) Then
            Me.SearchCondition.TokuisakiCodeList.Add(txt067)
        End If
        If Not String.IsNullOrWhiteSpace(txt069) Then
            Me.SearchCondition.TokuisakiCodeList.Add(txt069)
        End If
        If Not String.IsNullOrWhiteSpace(txt071) Then
            Me.SearchCondition.TokuisakiCodeList.Add(txt071)
        End If

        Dim validateErrorList = Me.Biz02.ValidateBeforeSearch(Me.SearchCondition)
        If validateErrorList.Count > 0 Then
            For Each errorMessage As String In validateErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        Me.Hide()
        Dim formRegist As New FormMTM02Regist(Me)
        formRegist.Show()
    End Sub
    ''' <summary>
    ''' 価格入力検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCostInputNumber_Click(sender As Object, e As EventArgs) Handles ButtonCostInputNumber.Click
        Dim formSearch As New FormMTMSearchJitsukou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox001.Text = formSearch.Selected.MTMR003001
            Me.TextBox002.Text = formSearch.Selected.MTMR003002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' ログインID検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonUser_Click(sender As Object, e As EventArgs) Handles ButtonUser.Click
        Dim formSearch As New FormMTMSearchUser
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox003.Text = formSearch.Selected.MTMM002001
            Me.TextBox004.Text = formSearch.Selected.MTMM002002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice001_Click(sender As Object, e As EventArgs) Handles ButtonOffice001.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox005.Text = formSearch.Selected.HANM036001
            Me.TextBox006.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice002_Click(sender As Object, e As EventArgs) Handles ButtonOffice002.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox025.Text = formSearch.Selected.HANM036001
            Me.TextBox026.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice003_Click(sender As Object, e As EventArgs) Handles ButtonOffice003.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox027.Text = formSearch.Selected.HANM036001
            Me.TextBox028.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice004_Click(sender As Object, e As EventArgs) Handles ButtonOffice004.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox029.Text = formSearch.Selected.HANM036001
            Me.TextBox030.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice005_Click(sender As Object, e As EventArgs) Handles ButtonOffice005.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox031.Text = formSearch.Selected.HANM036001
            Me.TextBox032.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice006_Click(sender As Object, e As EventArgs) Handles ButtonOffice006.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox033.Text = formSearch.Selected.HANM036001
            Me.TextBox034.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOffice007_Click(sender As Object, e As EventArgs) Handles ButtonOffice007.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox035.Text = formSearch.Selected.HANM036001
            Me.TextBox036.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection001_Click(sender As Object, e As EventArgs) Handles ButtonSection001.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox007.Text = formSearch.Selected.HANM015001
            Me.TextBox008.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection002_Click(sender As Object, e As EventArgs) Handles ButtonSection002.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox009.Text = formSearch.Selected.HANM015001
            Me.TextBox010.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection003_Click(sender As Object, e As EventArgs) Handles ButtonSection003.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox037.Text = formSearch.Selected.HANM015001
            Me.TextBox038.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection004_Click(sender As Object, e As EventArgs) Handles ButtonSection004.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox039.Text = formSearch.Selected.HANM015001
            Me.TextBox040.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection005_Click(sender As Object, e As EventArgs) Handles ButtonSection005.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox041.Text = formSearch.Selected.HANM015001
            Me.TextBox042.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection006_Click(sender As Object, e As EventArgs) Handles ButtonSection006.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox043.Text = formSearch.Selected.HANM015001
            Me.TextBox044.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection007_Click(sender As Object, e As EventArgs) Handles ButtonSection007.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox045.Text = formSearch.Selected.HANM015001
            Me.TextBox046.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSection008_Click(sender As Object, e As EventArgs) Handles ButtonSection008.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox047.Text = formSearch.Selected.HANM015001
            Me.TextBox048.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff001_Click(sender As Object, e As EventArgs) Handles ButtonStaff001.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox011.Text = formSearch.Selected.HANM004001
            Me.TextBox012.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff002_Click(sender As Object, e As EventArgs) Handles ButtonStaff002.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox013.Text = formSearch.Selected.HANM004001
            Me.TextBox014.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff003_Click(sender As Object, e As EventArgs) Handles ButtonStaff003.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox049.Text = formSearch.Selected.HANM004001
            Me.TextBox050.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff004_Click(sender As Object, e As EventArgs) Handles ButtonStaff004.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox051.Text = formSearch.Selected.HANM004001
            Me.TextBox052.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff005_Click(sender As Object, e As EventArgs) Handles ButtonStaff005.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox053.Text = formSearch.Selected.HANM004001
            Me.TextBox054.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff006_Click(sender As Object, e As EventArgs) Handles ButtonStaff006.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox055.Text = formSearch.Selected.HANM004001
            Me.TextBox056.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff007_Click(sender As Object, e As EventArgs) Handles ButtonStaff007.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox057.Text = formSearch.Selected.HANM004001
            Me.TextBox058.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonStaff008_Click(sender As Object, e As EventArgs) Handles ButtonStaff008.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox059.Text = formSearch.Selected.HANM004001
            Me.TextBox060.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer001_Click(sender As Object, e As EventArgs) Handles ButtonCustomer001.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox015.Text = formSearch.Selected.HANM001003
            Me.TextBox016.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer002_Click(sender As Object, e As EventArgs) Handles ButtonCustomer002.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox017.Text = formSearch.Selected.HANM001003
            Me.TextBox018.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer003_Click(sender As Object, e As EventArgs) Handles ButtonCustomer003.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox061.Text = formSearch.Selected.HANM001003
            Me.TextBox062.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer004_Click(sender As Object, e As EventArgs) Handles ButtonCustomer004.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox063.Text = formSearch.Selected.HANM001003
            Me.TextBox064.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer005_Click(sender As Object, e As EventArgs) Handles ButtonCustomer005.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox065.Text = formSearch.Selected.HANM001003
            Me.TextBox066.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer006_Click(sender As Object, e As EventArgs) Handles ButtonCustomer006.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox067.Text = formSearch.Selected.HANM001003
            Me.TextBox068.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer007_Click(sender As Object, e As EventArgs) Handles ButtonCustomer007.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox069.Text = formSearch.Selected.HANM001003
            Me.TextBox070.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCustomer008_Click(sender As Object, e As EventArgs) Handles ButtonCustomer008.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox071.Text = formSearch.Selected.HANM001003
            Me.TextBox072.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 商品コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonItem001_Click(sender As Object, e As EventArgs) Handles ButtonItem001.Click
        Dim formSearch As New FormMTMSearchShohin
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox019.Text = formSearch.Selected.HANM003001
            Me.TextBox020.Text = formSearch.Selected.HANM003002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 商品コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonItem002_Click(sender As Object, e As EventArgs) Handles ButtonItem002.Click
        Dim formSearch As New FormMTMSearchShohin
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox021.Text = formSearch.Selected.HANM003001
            Me.TextBox022.Text = formSearch.Selected.HANM003002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 数値イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox023_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox023.KeyPress
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso
       e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub
    ''' <summary>
    ''' 数値イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox024_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox024.KeyPress
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso
       e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose2_Click(sender As Object, e As EventArgs) Handles ButtonClose2.Click
        Me.Close()
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox001_TextChanged(sender As Object, e As EventArgs) Handles TextBox001.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox001.Text))) Then
            Me.TextBox002.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox003_TextChanged(sender As Object, e As EventArgs) Handles TextBox003.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox003.Text))) Then
            Me.TextBox004.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox005_TextChanged(sender As Object, e As EventArgs) Handles TextBox005.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox005.Text))) Then
            Me.TextBox006.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox007_TextChanged(sender As Object, e As EventArgs) Handles TextBox007.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox007.Text))) Then
            Me.TextBox008.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox009_TextChanged(sender As Object, e As EventArgs) Handles TextBox009.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox009.Text))) Then
            Me.TextBox010.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox011_TextChanged(sender As Object, e As EventArgs) Handles TextBox011.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox011.Text))) Then
            Me.TextBox012.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox013_TextChanged(sender As Object, e As EventArgs) Handles TextBox013.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox013.Text))) Then
            Me.TextBox014.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox015_TextChanged(sender As Object, e As EventArgs) Handles TextBox015.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox015.Text))) Then
            Me.TextBox016.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox017_TextChanged(sender As Object, e As EventArgs) Handles TextBox017.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox017.Text))) Then
            Me.TextBox018.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox019_TextChanged(sender As Object, e As EventArgs) Handles TextBox019.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox019.Text))) Then
            Me.TextBox020.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox021_TextChanged(sender As Object, e As EventArgs) Handles TextBox021.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox021.Text))) Then
            Me.TextBox022.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox025_TextChanged(sender As Object, e As EventArgs) Handles TextBox025.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox025.Text))) Then
            Me.TextBox026.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox027_TextChanged(sender As Object, e As EventArgs) Handles TextBox027.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox027.Text))) Then
            Me.TextBox028.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox029_TextChanged(sender As Object, e As EventArgs) Handles TextBox029.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox029.Text))) Then
            Me.TextBox030.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox031_TextChanged(sender As Object, e As EventArgs) Handles TextBox031.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox031.Text))) Then
            Me.TextBox032.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox033_TextChanged(sender As Object, e As EventArgs) Handles TextBox033.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox033.Text))) Then
            Me.TextBox034.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox035_TextChanged(sender As Object, e As EventArgs) Handles TextBox035.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox035.Text))) Then
            Me.TextBox036.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox037_TextChanged(sender As Object, e As EventArgs) Handles TextBox037.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox037.Text))) Then
            Me.TextBox038.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox039_TextChanged(sender As Object, e As EventArgs) Handles TextBox039.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox039.Text))) Then
            Me.TextBox040.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox041_TextChanged(sender As Object, e As EventArgs) Handles TextBox041.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox041.Text))) Then
            Me.TextBox042.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox043_TextChanged(sender As Object, e As EventArgs) Handles TextBox043.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox043.Text))) Then
            Me.TextBox044.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox045_TextChanged(sender As Object, e As EventArgs) Handles TextBox045.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox045.Text))) Then
            Me.TextBox046.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox047_TextChanged(sender As Object, e As EventArgs) Handles TextBox047.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox047.Text))) Then
            Me.TextBox048.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox049_TextChanged(sender As Object, e As EventArgs) Handles TextBox049.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox049.Text))) Then
            Me.TextBox050.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox051_TextChanged(sender As Object, e As EventArgs) Handles TextBox051.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox051.Text))) Then
            Me.TextBox052.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox053_TextChanged(sender As Object, e As EventArgs) Handles TextBox053.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox053.Text))) Then
            Me.TextBox054.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox055_TextChanged(sender As Object, e As EventArgs) Handles TextBox055.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox055.Text))) Then
            Me.TextBox056.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox057_TextChanged(sender As Object, e As EventArgs) Handles TextBox057.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox057.Text))) Then
            Me.TextBox058.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox059_TextChanged(sender As Object, e As EventArgs) Handles TextBox059.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox059.Text))) Then
            Me.TextBox060.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox061_TextChanged(sender As Object, e As EventArgs) Handles TextBox061.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox061.Text))) Then
            Me.TextBox062.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox063_TextChanged(sender As Object, e As EventArgs) Handles TextBox063.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox063.Text))) Then
            Me.TextBox064.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox065_TextChanged(sender As Object, e As EventArgs) Handles TextBox065.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox065.Text))) Then
            Me.TextBox066.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox067_TextChanged(sender As Object, e As EventArgs) Handles TextBox067.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox067.Text))) Then
            Me.TextBox068.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox069_TextChanged(sender As Object, e As EventArgs) Handles TextBox069.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox069.Text))) Then
            Me.TextBox070.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' テキスト変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox071_TextChanged(sender As Object, e As EventArgs) Handles TextBox071.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox071.Text))) Then
            Me.TextBox072.Text = ""
        End If
    End Sub
End Class