Imports System.Configuration
Imports MitsumoLib
Public Class FormMTM06
    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' 実行管理テーブルメンテナンスビジネスロジック
    ''' </summary>
    Private ReadOnly Biz06 As Biz.MTM06

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
        Me.Biz06 = New Biz.MTM06(connectionString)
    End Sub
    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM05_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim tableMTM10R006MAIL As New DataTable
        Me.TextBox001.Text = ""
        Me.TextBox002.Text = ""
        Me.TextBox003.Text = ""
        Me.TextBox004.Text = ""
        Me.TextBox005.Text = ""
        Me.TextBox006.Text = ""

        Dim loginUserId = Me.FormMenu.ModelMtmUser.MTMM002001.Trim
        tableMTM10R006MAIL = Me.Biz06.GetMail(loginUserId)
        Dim rowsMTM10R006MAIL As DataRow()
        rowsMTM10R006MAIL = tableMTM10R006MAIL.Select()
        If (tableMTM10R006MAIL.Rows.Count > 0) Then
            For Each dataRow As DataRow In rowsMTM10R006MAIL
                Me.TextBox001.Text = dataRow("MTMR006002")
                Me.TextBox002.Text = dataRow("MTMR006003")
                Me.TextBox003.Text = dataRow("MTMR006004")
                Me.TextBox004.Text = dataRow("MTMR006005")
                Me.TextBox005.Text = dataRow("MTMR006006")
                Me.TextBox006.Text = dataRow("MTMR006007")
            Next
        End If

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
    Private Sub FormMTM05_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' 実行ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonExecute_Click(sender As Object, e As EventArgs) Handles ButtonExecute.Click
        Dim txt001 As String = Me.FormMenu.ModelMtmUser.MTMM002001.Trim()
        Dim txt002 As String = Me.TextBox001.Text.Trim
        Dim txt003 As String = Me.TextBox002.Text.Trim
        Dim txt004 As String = Me.TextBox003.Text.Trim
        Dim txt005 As String = Me.TextBox004.Text.Trim
        Dim txt006 As String = Me.TextBox005.Text.Trim
        Dim txt007 As String = Me.TextBox006.Text.Trim

        Dim sysError As String = ""

        Dim result As DialogResult = MessageBox.Show("登録・更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        If result = DialogResult.No Then Exit Sub

        Try
            Dim mtm10r006mail = New MitsumoLib.Models.MTM10R006MAIL

            mtm10r006mail.MTMR006001 = txt001
            mtm10r006mail.MTMR006002 = txt002
            mtm10r006mail.MTMR006003 = txt003
            mtm10r006mail.MTMR006004 = txt004
            mtm10r006mail.MTMR006005 = txt005
            mtm10r006mail.MTMR006006 = txt006
            mtm10r006mail.MTMR006007 = txt007

            'メール文のバリデーションチェック
            Dim validateErrorList = Me.Biz06.ValidateCheck(mtm10r006mail)
            If validateErrorList.Count > 0 Then
                For Each errorMessage As String In validateErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If

            Dim dataCheck = Me.Biz06.DataCheckMail(mtm10r006mail.MTMR006001)
            If (dataCheck = False) Then
                '登録処理
                Dim insertErrorList = Me.Biz06.DataInsertMail(mtm10r006mail, sysError)
                If insertErrorList.Count > 0 Then
                    For Each errorMessage As String In insertErrorList
                        MessageBox.Show(errorMessage)
                        Exit Sub
                    Next
                End If
            Else
                '更新処理
                Dim updateErrorList = Me.Biz06.DataUpdateMail(mtm10r006mail, sysError)
                If updateErrorList.Count > 0 Then
                    For Each errorMessage As String In updateErrorList
                        MessageBox.Show(errorMessage)
                        Exit Sub
                    Next
                End If
            End If

            MessageBox.Show("メール文テーブルへの更新が完了しました")

        Catch ex As Exception
            If (Not String.IsNullOrEmpty(sysError)) Then
                MessageBox.Show("システムエラーが発生しました" & vbCrLf & sysError)
            Else
                MessageBox.Show("システムエラーが発生しました")
            End If
            Exit Sub
        End Try
    End Sub
    ''' <summary>
    ''' 案内文プレビューボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonPreview_Click(sender As Object, e As EventArgs) Handles ButtonPreview.Click

        Try
            Dim tantouName As String = Me.FormMenu.ModelMtmUser.MTMM002002.Trim()

            '本文文字列作成
            Dim body As String
            body = "｛得意先名｝" + vbCrLf
            body += "｛得意先名事業所名｝　御中" + vbCrLf
            body += vbCrLf
            body += "お世話になっております。" + vbCrLf
            body += "もりや産業の｛営業担当者名｝です。" + vbCrLf
            body += vbCrLf
            body += "この度、下記の商品の価格改定がございますので" + vbCrLf
            body += "改定見積書を添付致します。" + vbCrLf
            body += "ご査証いただきますようよろしくお願い致します。" + vbCrLf

            '前文の内容を埋め込み
            If (Not String.IsNullOrEmpty(Me.TextBox001.Text)) OrElse
                    (Not String.IsNullOrEmpty(Me.TextBox002.Text)) OrElse
                    (Not String.IsNullOrEmpty(Me.TextBox003.Text)) Then
                body += vbCrLf
            End If
            If (Not String.IsNullOrEmpty(Me.TextBox001.Text)) Then
                body += "" + Me.TextBox001.Text + vbCrLf
            End If
            If (Not String.IsNullOrEmpty(Me.TextBox002.Text)) Then
                body += "" + Me.TextBox002.Text + vbCrLf
            End If
            If (Not String.IsNullOrEmpty(Me.TextBox003.Text)) Then
                body += "" + Me.TextBox003.Text + vbCrLf
            End If

            body += vbCrLf
            body += "該当商品：｛価格入力名｝" + vbCrLf
            body += "改定日：　｛改定日分｝より" + vbCrLf

            '後文の内容を埋め込み
            If (Not String.IsNullOrEmpty(Me.TextBox004.Text)) OrElse
                    (Not String.IsNullOrEmpty(Me.TextBox005.Text)) OrElse
                    (Not String.IsNullOrEmpty(Me.TextBox006.Text)) Then
                body += vbCrLf
            End If
            If (Not String.IsNullOrEmpty(Me.TextBox004.Text)) Then
                body += "" + Me.TextBox004.Text + vbCrLf
            End If
            If (Not String.IsNullOrEmpty(Me.TextBox005.Text)) Then
                body += "" + Me.TextBox005.Text + vbCrLf
            End If
            If (Not String.IsNullOrEmpty(Me.TextBox006.Text)) Then
                body += "" + Me.TextBox006.Text + vbCrLf
            End If

            body += vbCrLf
            body += "以上よろしくお願い致します。" + vbCrLf
            body += vbCrLf
            body += "--------------------------------" + vbCrLf
            body += "もりや産業株式会社" + vbCrLf
            body += "｛営業担当所属先名｝" + vbCrLf
            body += "｛営業担当名｝" + vbCrLf
            body += vbCrLf
            body += "住所　｛営業担当所属先住所｝" + vbCrLf
            body += "TEL.｛営業担当所属先TEL｝ FAX.｛営業担当所属先FAX｝" + vbCrLf
            body += "メール　｛営業担当者MAIL｝" + vbCrLf

            Dim formMailPreview As New FormMTMMailPreview
            formMailPreview.RichTextBox001.Text = body


            Dim result As DialogResult = formMailPreview.ShowDialog()
            formMailPreview.Dispose()

            'Process.Start("mailto:foo@boo.co.jp,moo@boo.co.jp?cc=boo@boo.co.jp&bcc=poo@boo.co.jp&subject=ここは件名&body=ここは本文")

        Catch ex As Exception
            If (Not String.IsNullOrEmpty(ex.Message)) Then
                MessageBox.Show("システムエラーが発生しました" & vbCrLf & ex.Message)
            Else
                MessageBox.Show("システムエラーが発生しました")
            End If
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox001_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox001.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox002_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox002.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox003_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox003.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox004_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox004.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox005_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox005.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox006_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox006.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            'Me.ProcessTabKey(forward);
            Me.SelectNextControl(Me.ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub
End Class