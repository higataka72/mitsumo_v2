Imports System
Imports System.Configuration
Imports System.IO
Imports MitsumoLib

''' <summary>
''' ログインフォーム
''' </summary>
Public Class FormMTMLogin
    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' ログインビジネスロジック
    ''' </summary>
    Private ReadOnly BizLogin As Biz.MTMLogin
    Private ReadOnly BizLogin2 As Biz.MTMLogin

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
        Me.BizLogin = New Biz.MTMLogin(connectionString)
        Dim connectionString2 = ConfigurationManager.ConnectionStrings("KIKAN_DB").ConnectionString
        Me.BizLogin2 = New Biz.MTMLogin(connectionString2)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim errorInfo As String
        'データベース接続チェック
        If (Not Me.BizLogin.DatabaseConect(errorInfo)) Then
            MessageBox.Show("[mitsumoDB]データベースへ接続できません" & vbCrLf & "設定ファイルを確認して下さい。" & vbCrLf & errorInfo)
            ButtonLogin.Enabled = False
        End If
        'データベース接続チェック
        If (Not Me.BizLogin2.DatabaseConect2(errorInfo)) Then
            MessageBox.Show("[kikanDB]データベースへ接続できません" & vbCrLf & "設定ファイルを確認して下さい。" & vbCrLf & errorInfo)
            ButtonLogin.Enabled = False
        End If
        ClearAll()
    End Sub

    ''' <summary>
    ''' クリア
    ''' </summary>
    Private Sub ClearAll()
        Me.TextBox001.Text = ""
        Me.TextBox002.Text = ""
    End Sub

    ''' <summary>
    ''' ログインボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonLogin_Click(sender As Object, e As EventArgs) Handles ButtonLogin.Click
        Dim txt001 As String = Me.TextBox001.Text.Trim
        Dim txt002 As String = Me.TextBox002.Text.Trim

        Dim mtm00m002user = New Models.MTM00M002USER
        mtm00m002user.MTMM002001 = txt001
        mtm00m002user.MTMM002003 = txt002

        Dim validateErrorList = Me.BizLogin.ValidateLogin(mtm00m002user)
        If validateErrorList.Count > 0 Then
            For Each errorMessage As String In validateErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        Dim result = Me.BizLogin.Login(mtm00m002user)
        If result.Result = False Then
            MessageBox.Show(result.Message)
        Else
            'PDFファイルの過去ファイル削除
            FileDelete()
            Me.FormMenu.ModelMtmUser = result.Mtm00m002User
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' フォームクロース処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMLogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Me.FormMenu.ModelMtmUser Is Nothing Then
            Me.FormMenu.Close()
        Else
            Me.FormMenu.Show()
        End If
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    Private Sub FileDelete()
        Dim executionAsm As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
        Dim executingPath As String = IO.Path.GetDirectoryName(New Uri(executionAsm.CodeBase).LocalPath)
        Dim pdfDir = ConfigurationManager.AppSettings("PDF_DIR")

        Dim targetFolder As String = executingPath + "\" + pdfDir
        Dim currentDate As DateTime = DateTime.Now
        Dim sevenDaysAgo As DateTime = currentDate.AddDays(-1)

        Dim directoryInfo As New DirectoryInfo(targetFolder)

        For Each file In directoryInfo.GetFiles()
            Dim fileDate As DateTime = file.LastWriteTime

            If fileDate < sevenDaysAgo Then
                Try
                    Dim comparison As String = file.FullName
                    Dim result As Boolean = comparison.Contains("プレビュー")

                    If (result) Then
                        System.IO.File.Delete(file.FullName)
                        Console.WriteLine("File deleted: " & file.FullName)
                    End If

                Catch ex As Exception
                    Console.WriteLine("Error deleting file: " & ex.Message)
                End Try
            End If
        Next
    End Sub
End Class
