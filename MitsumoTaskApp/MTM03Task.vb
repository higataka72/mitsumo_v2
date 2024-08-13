Imports System.Configuration
Imports MitsumoLib
Imports log4net
Public Class MTM03Task

    Sub Execute(ByVal cmdArgs() As String)

        Dim Log As ILog = LogManager.GetLogger(Reflection.MethodBase.GetCurrentMethod().Name)
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Dim biz03 As New Biz.MTM03(connectionString)

        Log.Info("見積送信　開始")

        If cmdArgs.Length < 1 Then
            Log.Error("エラー発生：引数が足りません")
            Console.WriteLine("エラー発生：引数が足りません")
            Exit Sub
        End If

        Dim searchCondition As New Biz.MTM03SearchCondition With {
            .KakakuNyuuryokuNo = cmdArgs(0),
            .OutputNum = "0",
            .SendNum = "1"
            }

        'If cmdArgs.Length >= 4 Then
        '    searchCondition.TokuisakiCodeList.Add(cmdArgs(3))
        'End If

        Dim validateErrorList = biz03.ValidateBeforeSearch(searchCondition)
        If validateErrorList.Count > 0 Then
            For Each errorMessage As String In validateErrorList
                Log.Error("エラー発生：" + errorMessage)
                Console.WriteLine("エラー発生：" + errorMessage)
                Exit Sub
            Next
        End If

        Try
            Dim loginId As String = ConfigurationManager.AppSettings("ADMIN_USER").Trim

            Dim mailInfo As New Biz.MTM03MailInfo With {
                .MailHost = ConfigurationManager.AppSettings("MAIL_HOST"),
                .MailPort = ConfigurationManager.AppSettings("MAIL_PORT"),
                .MailUser = ConfigurationManager.AppSettings("MAIL_USER"),
                .MailPass = ConfigurationManager.AppSettings("MAIL_PASS")
                }

            Dim executionAsm As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
            Dim executingPath As String = IO.Path.GetDirectoryName(New Uri(executionAsm.CodeBase).LocalPath)
            Dim pdfDir = ConfigurationManager.AppSettings("PDF_DIR")

            Dim sendMailData As New Biz.MTM03SendMailData
            sendMailData.SearchResult = biz03.GetKakaku(searchCondition)

            If sendMailData.SearchResult.ElementList.Count > 0 Then
                For Each resultElement In sendMailData.SearchResult.ElementList
                    If (searchCondition.OutputNum = "0" Or searchCondition.OutputNum = "1") And resultElement.SoushinKubun = "1" Then
                        Dim sendMailErrorList = biz03.SendMail(searchCondition.KakakuNyuuryokuNo, mailInfo, resultElement, executingPath + "/" + pdfDir)
                        If sendMailErrorList.Count > 0 Then
                            sendMailData.ErrorList.AddRange(sendMailErrorList)
                            Exit For
                        End If
                    ElseIf (searchCondition.OutputNum = "0" Or searchCondition.OutputNum = "2") And resultElement.SoushinKubun = "2" Then
                        Dim sendEDocumentErrorList = biz03.SendEDocumentHeader(searchCondition.KakakuNyuuryokuNo, mailInfo, resultElement, executingPath + "/" + pdfDir)
                        If sendEDocumentErrorList.Count > 0 Then
                            sendMailData.ErrorList.AddRange(sendEDocumentErrorList)
                            Exit For
                        End If
                    End If

                    Dim updateErrorList = biz03.Update(resultElement, loginId)
                    If updateErrorList.Count > 0 Then
                        sendMailData.ErrorList.AddRange(updateErrorList)
                        Exit For
                    End If
                Next
            Else
                Log.Info("見積データがありません")
                'sendMailData.ErrorList.Add("見積データがありません")
            End If

            If sendMailData.ErrorList.Count > 0 Then
                For Each errorMessage As String In sendMailData.ErrorList
                    Log.Error("エラー発生：" + errorMessage)
                    Console.WriteLine("エラー発生：" + errorMessage)
                    Exit Sub
                Next
            End If

        Catch ex As Exception
            Log.Error("システムエラーが発生しました" & vbCrLf & ex.Message)
            Console.WriteLine("システムエラーが発生しました" & vbCrLf & ex.Message)
        End Try

        Log.Info("見積送信　完了")
        Console.WriteLine("見積送信 完了")
    End Sub
End Class
