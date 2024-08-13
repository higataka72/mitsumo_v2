Imports System.Configuration
Imports MitsumoLib

Public Class FormMTM03
    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' 単価入力シート取込ビジネスロジック
    ''' </summary>
    Private ReadOnly Biz03 As Biz.MTM03

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
        Me.Biz03 = New Biz.MTM03(connectionString)
    End Sub

    Private Sub FormMTM03_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RadioButton001.Checked = True
        Me.RadioButton004.Checked = True
        Me.TextBox003.Text = Me.FormMenu.ModelMtmUser.MTMM002001.Trim
        Me.TextBox004.Text = Me.FormMenu.ModelMtmUser.MTMM002002
    End Sub

    ''' <summary>
    ''' 閉じるボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        Me.FormMenu.Show()
    End Sub

    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM03_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.FormMenu.Show()
    End Sub

    ''' <summary>
    ''' プレビューボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonPreview_Click(sender As Object, e As EventArgs) Handles ButtonPreview.Click
        Dim searchCondition = Me.CreateSearchCondition

        Dim validateErrorList = Me.Biz03.ValidateBeforeSearch(searchCondition)
        If validateErrorList.Count > 0 Then
            For Each errorMessage As String In validateErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        Dim executionAsm As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
        Dim executingPath As String = IO.Path.GetDirectoryName(New Uri(executionAsm.CodeBase).LocalPath)
        Dim pdfDir = ConfigurationManager.AppSettings("PDF_DIR")

        Dim previewData As Biz.MTM03PreviewData = Me.Biz03.PreviewPdf(searchCondition, executingPath + "\" + pdfDir)

        If previewData.ErrorList.Count > 0 Then
            For Each errorMessage As String In previewData.ErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        'PDF起動
        Dim p As System.Diagnostics.Process =
        System.Diagnostics.Process.Start(previewData.FilePath)
        'p.WaitForExit()

        ''後処理としてプレビューPDFを削除する
        'System.IO.File.Delete(previewData.FilePath)

        'Dim formPreview As New FormMTMPreview
        'formPreview.WebView2001.Source = New Uri(previewData.FilePath)
        'Dim result As DialogResult = formPreview.ShowDialog()

        ''後処理としてプレビューPDFを削除する
        'System.IO.File.Delete(previewData.FilePath)

        'formPreview.Dispose()
    End Sub

    ''' <summary>
    ''' 検索条件の作成
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateSearchCondition() As Biz.MTM03SearchCondition
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
        Dim txt058 As String = Me.TextBox058.Text.Trim
        Dim txt060 As String = Me.TextBox060.Text.Trim
        Dim txt062 As String = Me.TextBox062.Text.Trim
        Dim txt064 As String = Me.TextBox064.Text.Trim
        Dim txt066 As String = Me.TextBox066.Text.Trim
        Dim txt068 As String = Me.TextBox068.Text.Trim
        Dim rdo001 As Boolean = Me.RadioButton001.Checked
        Dim rdo002 As Boolean = Me.RadioButton002.Checked
        Dim rdo003 As Boolean = Me.RadioButton003.Checked
        Dim rdo004 As Boolean = Me.RadioButton004.Checked
        Dim rdo005 As Boolean = Me.RadioButton005.Checked



        Dim searchCondition As New Biz.MTM03SearchCondition With {
            .KakakuNyuuryokuNo = txt001,
            .LoginId = txt003,
            .EigyosyoCodeFrom = txt005,
            .EigyosyoCodeTo = txt007,
            .BukaCodeFrom = txt009,
            .BukaCodeTo = txt011,
            .TantousyaCodeFrom = txt013,
            .TantousyaCodeTo = txt015,
            .TokuisakiCodeFrom = txt017,
            .TokuisakiCodeTo = txt019
            }
        '出力対象の指定
        If (rdo003) Then
            searchCondition.OutputNum = "2"
        ElseIf (rdo002) Then
            searchCondition.OutputNum = "1"
        ElseIf (rdo001) Then
            searchCondition.OutputNum = "0"
        Else
            searchCondition.OutputNum = "0"
        End If
        '送信区分
        If (rdo004) Then
            searchCondition.SendNum = "1"
        ElseIf (rdo005) Then
            searchCondition.SendNum = "2"
        Else
            searchCondition.SendNum = "1"
        End If

        If Not String.IsNullOrWhiteSpace(txt021) Then
            searchCondition.EigyosyoCodeList.Add(txt021)
        End If
        If Not String.IsNullOrWhiteSpace(txt023) Then
            searchCondition.EigyosyoCodeList.Add(txt023)
        End If
        If Not String.IsNullOrWhiteSpace(txt025) Then
            searchCondition.EigyosyoCodeList.Add(txt025)
        End If
        If Not String.IsNullOrWhiteSpace(txt027) Then
            searchCondition.EigyosyoCodeList.Add(txt027)
        End If
        If Not String.IsNullOrWhiteSpace(txt029) Then
            searchCondition.EigyosyoCodeList.Add(txt029)
        End If
        If Not String.IsNullOrWhiteSpace(txt031) Then
            searchCondition.EigyosyoCodeList.Add(txt031)
        End If
        If Not String.IsNullOrWhiteSpace(txt033) Then
            searchCondition.BukaCodeList.Add(txt033)
        End If
        If Not String.IsNullOrWhiteSpace(txt035) Then
            searchCondition.BukaCodeList.Add(txt035)
        End If
        If Not String.IsNullOrWhiteSpace(txt037) Then
            searchCondition.BukaCodeList.Add(txt037)
        End If
        If Not String.IsNullOrWhiteSpace(txt039) Then
            searchCondition.BukaCodeList.Add(txt039)
        End If
        If Not String.IsNullOrWhiteSpace(txt041) Then
            searchCondition.BukaCodeList.Add(txt041)
        End If
        If Not String.IsNullOrWhiteSpace(txt043) Then
            searchCondition.BukaCodeList.Add(txt043)
        End If
        If Not String.IsNullOrWhiteSpace(txt045) Then
            searchCondition.TantousyaCodeList.Add(txt045)
        End If
        If Not String.IsNullOrWhiteSpace(txt047) Then
            searchCondition.TantousyaCodeList.Add(txt047)
        End If
        If Not String.IsNullOrWhiteSpace(txt049) Then
            searchCondition.TantousyaCodeList.Add(txt049)
        End If
        If Not String.IsNullOrWhiteSpace(txt051) Then
            searchCondition.TantousyaCodeList.Add(txt051)
        End If
        If Not String.IsNullOrWhiteSpace(txt053) Then
            searchCondition.TantousyaCodeList.Add(txt053)
        End If
        If Not String.IsNullOrWhiteSpace(txt055) Then
            searchCondition.TantousyaCodeList.Add(txt055)
        End If
        If Not String.IsNullOrWhiteSpace(txt058) Then
            searchCondition.TokuisakiCodeList.Add(txt058)
        End If
        If Not String.IsNullOrWhiteSpace(txt060) Then
            searchCondition.TokuisakiCodeList.Add(txt060)
        End If
        If Not String.IsNullOrWhiteSpace(txt062) Then
            searchCondition.TokuisakiCodeList.Add(txt062)
        End If
        If Not String.IsNullOrWhiteSpace(txt064) Then
            searchCondition.TokuisakiCodeList.Add(txt064)
        End If
        If Not String.IsNullOrWhiteSpace(txt066) Then
            searchCondition.TokuisakiCodeList.Add(txt066)
        End If
        If Not String.IsNullOrWhiteSpace(txt068) Then
            searchCondition.TokuisakiCodeList.Add(txt068)
        End If

        Return searchCondition
    End Function
    <Obsolete>
    Private Sub ButtonSend_Click(sender As Object, e As EventArgs) Handles ButtonSend.Click
        Dim searchCondition = Me.CreateSearchCondition

        Dim dialogProgress As New DialogMTMProgress
        dialogProgress.Show(Me)

        Try
            Dim validateErrorList = Me.Biz03.ValidateBeforeSearch(searchCondition)
            If validateErrorList.Count > 0 Then
                For Each errorMessage As String In validateErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If

            Dim mailInfo As New Biz.MTM03MailInfo With {
                .MailHost = ConfigurationManager.AppSettings("MAIL_HOST"),
                .MailPort = ConfigurationManager.AppSettings("MAIL_PORT"),
                .MailUser = ConfigurationManager.AppSettings("MAIL_USER"),
                .MailPass = ConfigurationManager.AppSettings("MAIL_PASS")
                }

            Dim executionAsm As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
            Dim executingPath As String = IO.Path.GetDirectoryName(New Uri(executionAsm.CodeBase).LocalPath)
            Dim pdfDir = ConfigurationManager.AppSettings("PDF_DIR")

            Me.ButtonSend.Enabled = False
            dialogProgress.Message = "送信開始.."

            'Dim sendMailData As Biz.MTM03SendMailData = Me.Biz03.SendMailMitsumo(searchCondition, mailInfo, executingPath + "\" + pdfDir, Me.FormMenu.ModelMtmUser.MTMM002001)
            Dim sendMailData As New Biz.MTM03SendMailData
            sendMailData.SearchResult = Me.Biz03.GetKakaku(searchCondition)

            If sendMailData.SearchResult.ElementList.Count > 0 Then
                For Each resultElement In sendMailData.SearchResult.ElementList
                    dialogProgress.Message = "送信中/得意先.." & resultElement.TokuisakiCode.Trim()
                    If (searchCondition.OutputNum = "0" Or searchCondition.OutputNum = "1") And resultElement.SoushinKubun = "1" Then
                        Dim sendMailErrorList = Me.Biz03.SendMail(searchCondition.KakakuNyuuryokuNo, mailInfo, resultElement, executingPath + "\" + pdfDir)
                        If sendMailErrorList.Count > 0 Then
                            sendMailData.ErrorList.AddRange(sendMailErrorList)
                            Exit For
                        End If
                    ElseIf (searchCondition.OutputNum = "0" Or searchCondition.OutputNum = "2") And resultElement.SoushinKubun = "2" Then
                        Dim sendEDocumentErrorList = Me.Biz03.SendEDocumentHeader(searchCondition.KakakuNyuuryokuNo, mailInfo, resultElement, executingPath + "\" + pdfDir)
                        If sendEDocumentErrorList.Count > 0 Then
                            sendMailData.ErrorList.AddRange(sendEDocumentErrorList)
                            Exit For
                        End If
                    End If

                    Dim updateErrorList = Me.Biz03.Update(resultElement, Me.FormMenu.ModelMtmUser.MTMM002001)
                    If updateErrorList.Count > 0 Then
                        sendMailData.ErrorList.AddRange(updateErrorList)
                        Exit For
                    End If
                Next
            Else
                sendMailData.ErrorList.Add("見積データがありません")
            End If

            If sendMailData.ErrorList.Count > 0 Then
                For Each errorMessage As String In sendMailData.ErrorList
                    dialogProgress.Close()
                    Me.Activate()
                    MessageBox.Show(errorMessage)
                    Me.ButtonSend.Enabled = True
                    Exit Sub
                Next
            End If

            dialogProgress.Close()
            Me.Activate()
            MessageBox.Show("送信しました")
            Me.ButtonSend.Enabled = True
        Catch ex As Exception
            Me.ButtonSend.Enabled = True
            dialogProgress.Close()
            Me.Activate()
            MessageBox.Show("システムエラーが発生しました")
            Exit Sub
        End Try

    End Sub

    ''' <summary>
    ''' 価格入力検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button001_Click(sender As Object, e As EventArgs) Handles Button001.Click
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
    Private Sub Button002_Click(sender As Object, e As EventArgs) Handles Button002.Click
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
    Private Sub Button003_Click(sender As Object, e As EventArgs) Handles Button003.Click
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
    Private Sub Button004_Click(sender As Object, e As EventArgs) Handles Button004.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox007.Text = formSearch.Selected.HANM036001
            Me.TextBox008.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button005_Click(sender As Object, e As EventArgs) Handles Button005.Click
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
    Private Sub Button006_Click(sender As Object, e As EventArgs) Handles Button006.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox011.Text = formSearch.Selected.HANM015001
            Me.TextBox012.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button007_Click(sender As Object, e As EventArgs) Handles Button007.Click
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
    Private Sub Button008_Click(sender As Object, e As EventArgs) Handles Button008.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox015.Text = formSearch.Selected.HANM004001
            Me.TextBox016.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button009_Click(sender As Object, e As EventArgs) Handles Button009.Click
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
    Private Sub Button010_Click(sender As Object, e As EventArgs) Handles Button010.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox019.Text = formSearch.Selected.HANM001003
            Me.TextBox020.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button011_Click(sender As Object, e As EventArgs) Handles Button011.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox021.Text = formSearch.Selected.HANM036001
            Me.TextBox022.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button012_Click(sender As Object, e As EventArgs) Handles Button012.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox023.Text = formSearch.Selected.HANM036001
            Me.TextBox024.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 営業所コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button013_Click(sender As Object, e As EventArgs) Handles Button013.Click
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
    Private Sub Button014_Click(sender As Object, e As EventArgs) Handles Button014.Click
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
    Private Sub Button015_Click(sender As Object, e As EventArgs) Handles Button015.Click
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
    Private Sub Button016_Click(sender As Object, e As EventArgs) Handles Button016.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox031.Text = formSearch.Selected.HANM036001
            Me.TextBox032.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button017_Click(sender As Object, e As EventArgs) Handles Button017.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox033.Text = formSearch.Selected.HANM015001
            Me.TextBox034.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button018_Click(sender As Object, e As EventArgs) Handles Button018.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox035.Text = formSearch.Selected.HANM015001
            Me.TextBox036.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 部門コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button019_Click(sender As Object, e As EventArgs) Handles Button019.Click
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
    Private Sub Button020_Click(sender As Object, e As EventArgs) Handles Button020.Click
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
    Private Sub Button021_Click(sender As Object, e As EventArgs) Handles Button021.Click
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
    Private Sub Button022_Click(sender As Object, e As EventArgs) Handles Button022.Click
        Dim formSearch As New FormMTMSearchBumon
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox043.Text = formSearch.Selected.HANM015001
            Me.TextBox044.Text = formSearch.Selected.HANM015002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button023_Click(sender As Object, e As EventArgs) Handles Button023.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox045.Text = formSearch.Selected.HANM004001
            Me.TextBox046.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button024_Click(sender As Object, e As EventArgs) Handles Button024.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox047.Text = formSearch.Selected.HANM004001
            Me.TextBox048.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 担当者コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button025_Click(sender As Object, e As EventArgs) Handles Button025.Click
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
    Private Sub Button026_Click(sender As Object, e As EventArgs) Handles Button026.Click
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
    Private Sub Button027_Click(sender As Object, e As EventArgs) Handles Button027.Click
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
    Private Sub Button028_Click(sender As Object, e As EventArgs) Handles Button028.Click
        Dim formSearch As New FormMTMSearchTanto
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox055.Text = formSearch.Selected.HANM004001
            Me.TextBox056.Text = formSearch.Selected.HANM004002
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button029_Click(sender As Object, e As EventArgs) Handles Button029.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox058.Text = formSearch.Selected.HANM001003
            Me.TextBox059.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button030_Click(sender As Object, e As EventArgs) Handles Button030.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox060.Text = formSearch.Selected.HANM001003
            Me.TextBox061.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button031_Click(sender As Object, e As EventArgs) Handles Button031.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox062.Text = formSearch.Selected.HANM001003
            Me.TextBox063.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button032_Click(sender As Object, e As EventArgs) Handles Button032.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox064.Text = formSearch.Selected.HANM001003
            Me.TextBox065.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button033_Click(sender As Object, e As EventArgs) Handles Button033.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox066.Text = formSearch.Selected.HANM001003
            Me.TextBox067.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub

    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button034_Click(sender As Object, e As EventArgs) Handles Button034.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox068.Text = formSearch.Selected.HANM001003
            Me.TextBox069.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub

    Private Sub TextBox001_TextChanged(sender As Object, e As EventArgs) Handles TextBox001.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox001.Text))) Then
            Me.TextBox002.Text = ""
        End If
    End Sub

    Private Sub TextBox003_TextChanged(sender As Object, e As EventArgs) Handles TextBox003.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox003.Text))) Then
            Me.TextBox004.Text = ""
        End If
    End Sub

    Private Sub TextBox005_TextChanged(sender As Object, e As EventArgs) Handles TextBox005.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox005.Text))) Then
            Me.TextBox006.Text = ""
        End If
    End Sub

    Private Sub TextBox007_TextChanged(sender As Object, e As EventArgs) Handles TextBox007.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox007.Text))) Then
            Me.TextBox008.Text = ""
        End If
    End Sub

    Private Sub TextBox009_TextChanged(sender As Object, e As EventArgs) Handles TextBox009.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox009.Text))) Then
            Me.TextBox010.Text = ""
        End If
    End Sub

    Private Sub TextBox011_TextChanged(sender As Object, e As EventArgs) Handles TextBox011.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox011.Text))) Then
            Me.TextBox012.Text = ""
        End If
    End Sub

    Private Sub TextBox013_TextChanged(sender As Object, e As EventArgs) Handles TextBox013.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox013.Text))) Then
            Me.TextBox014.Text = ""
        End If
    End Sub

    Private Sub TextBox015_TextChanged(sender As Object, e As EventArgs) Handles TextBox015.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox015.Text))) Then
            Me.TextBox016.Text = ""
        End If
    End Sub

    Private Sub TextBox017_TextChanged(sender As Object, e As EventArgs) Handles TextBox017.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox017.Text))) Then
            Me.TextBox018.Text = ""
        End If
    End Sub

    Private Sub TextBox019_TextChanged(sender As Object, e As EventArgs) Handles TextBox019.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox019.Text))) Then
            Me.TextBox020.Text = ""
        End If
    End Sub

    Private Sub TextBox021_TextChanged(sender As Object, e As EventArgs) Handles TextBox021.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox021.Text))) Then
            Me.TextBox022.Text = ""
        End If
    End Sub

    Private Sub TextBox023_TextChanged(sender As Object, e As EventArgs) Handles TextBox023.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox023.Text))) Then
            Me.TextBox024.Text = ""
        End If
    End Sub

    Private Sub TextBox025_TextChanged(sender As Object, e As EventArgs) Handles TextBox025.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox025.Text))) Then
            Me.TextBox026.Text = ""
        End If
    End Sub

    Private Sub TextBox027_TextChanged(sender As Object, e As EventArgs) Handles TextBox027.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox027.Text))) Then
            Me.TextBox028.Text = ""
        End If
    End Sub

    Private Sub TextBox029_TextChanged(sender As Object, e As EventArgs) Handles TextBox029.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox029.Text))) Then
            Me.TextBox030.Text = ""
        End If
    End Sub

    Private Sub TextBox033_TextChanged(sender As Object, e As EventArgs) Handles TextBox033.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox033.Text))) Then
            Me.TextBox034.Text = ""
        End If
    End Sub

    Private Sub TextBox035_TextChanged(sender As Object, e As EventArgs) Handles TextBox035.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox035.Text))) Then
            Me.TextBox036.Text = ""
        End If
    End Sub

    Private Sub TextBox037_TextChanged(sender As Object, e As EventArgs) Handles TextBox037.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox037.Text))) Then
            Me.TextBox038.Text = ""
        End If
    End Sub

    Private Sub TextBox039_TextChanged(sender As Object, e As EventArgs) Handles TextBox039.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox039.Text))) Then
            Me.TextBox040.Text = ""
        End If
    End Sub

    Private Sub TextBox041_TextChanged(sender As Object, e As EventArgs) Handles TextBox041.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox041.Text))) Then
            Me.TextBox042.Text = ""
        End If
    End Sub

    Private Sub TextBox043_TextChanged(sender As Object, e As EventArgs) Handles TextBox043.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox043.Text))) Then
            Me.TextBox044.Text = ""
        End If
    End Sub

    Private Sub TextBox045_TextChanged(sender As Object, e As EventArgs) Handles TextBox045.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox045.Text))) Then
            Me.TextBox046.Text = ""
        End If
    End Sub

    Private Sub TextBox047_TextChanged(sender As Object, e As EventArgs) Handles TextBox047.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox047.Text))) Then
            Me.TextBox048.Text = ""
        End If
    End Sub

    Private Sub TextBox049_TextChanged(sender As Object, e As EventArgs) Handles TextBox049.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox049.Text))) Then
            Me.TextBox050.Text = ""
        End If
    End Sub

    Private Sub TextBox051_TextChanged(sender As Object, e As EventArgs) Handles TextBox051.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox051.Text))) Then
            Me.TextBox052.Text = ""
        End If
    End Sub

    Private Sub TextBox053_TextChanged(sender As Object, e As EventArgs) Handles TextBox053.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox053.Text))) Then
            Me.TextBox054.Text = ""
        End If
    End Sub

    Private Sub TextBox055_TextChanged(sender As Object, e As EventArgs) Handles TextBox055.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox055.Text))) Then
            Me.TextBox056.Text = ""
        End If
    End Sub

    Private Sub TextBox058_TextChanged(sender As Object, e As EventArgs) Handles TextBox058.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox058.Text))) Then
            Me.TextBox059.Text = ""
        End If
    End Sub

    Private Sub TextBox060_TextChanged(sender As Object, e As EventArgs) Handles TextBox060.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox060.Text))) Then
            Me.TextBox061.Text = ""
        End If
    End Sub

    Private Sub TextBox062_TextChanged(sender As Object, e As EventArgs) Handles TextBox062.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox062.Text))) Then
            Me.TextBox063.Text = ""
        End If
    End Sub

    Private Sub TextBox064_TextChanged(sender As Object, e As EventArgs) Handles TextBox064.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox064.Text))) Then
            Me.TextBox065.Text = ""
        End If
    End Sub

    Private Sub TextBox066_TextChanged(sender As Object, e As EventArgs) Handles TextBox066.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox066.Text))) Then
            Me.TextBox067.Text = ""
        End If
    End Sub

    Private Sub TextBox068_TextChanged(sender As Object, e As EventArgs) Handles TextBox068.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox068.Text))) Then
            Me.TextBox069.Text = ""
        End If
    End Sub

    Private Sub TextBox031_TextChanged(sender As Object, e As EventArgs) Handles TextBox031.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox031.Text))) Then
            Me.TextBox032.Text = ""
        End If
    End Sub
End Class