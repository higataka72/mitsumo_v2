Namespace Biz
    ''' <summary>
    ''' Windowsタスクスケジューラー登録
    ''' </summary>
    Public Class ComTaskScheduler

        Public Function RegistTask(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU, ByVal serversetting As Models.SERVERSETTING, ByVal AppNum As Integer) As List(Of String)
            Dim process As New System.Diagnostics.Process()
            Dim successExitCode As Integer = 0
            Dim dbl As Double
            Dim errorList As New List(Of String)

            Try
                'タスクスケジューラー登録変数
                Dim taskMitsumoriTitleName As String = "mitsumoApp_mitsumori_"
                Dim taskSystemTitleName As String = "mitsumoApp_System_"
                Dim taskAppName As String = "/MitsumoTaskApp.exe "
                Dim taskAppExe As String = ""
                Dim taskName As String = ""
                Dim taskAppOption As String = ""
                Dim taskDate As String = ""
                Dim tsskdateTime As String = "00:00"
                Dim workStrDate As String
                Dim workDatDate As Date
                Dim workChkDate As Date
                Dim executionAsm As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
                Dim executingPath As String = IO.Path.GetDirectoryName(New Uri(executionAsm.CodeBase).LocalPath)


                'タスクスケジューラーの切替と生成
                If (AppNum = 1) Then
                    '見積送信(一斉送信日時）
                    'タスク名
                    taskName = taskMitsumoriTitleName & mtm10r003jitsukou.MTMR003001
                    'アプリケーション
                    taskAppOption = "1 " & mtm10r003jitsukou.MTMR003001
                    taskAppName = executingPath & taskAppName
                    taskAppExe = taskAppName
                    '日付を生成
                    workStrDate = mtm10r003jitsukou.MTMR003005
                    workDatDate = DateTime.ParseExact(workStrDate, "yyyyMMdd", Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)
                    taskDate = workDatDate.ToString("yyyy/MM/dd")
                    '時間を生成
                    If Not String.IsNullOrEmpty(mtm10r003jitsukou.MTMR003005_2) Then
                        If Double.TryParse(mtm10r003jitsukou.MTMR003005_2, dbl) Then
                            If Not ((dbl < 0) Or (dbl > 23)) Then
                                tsskdateTime = mtm10r003jitsukou.MTMR003005_2.PadLeft(2, "0"c) & ":00"
                            End If
                        End If
                    End If

                    '起動チェック
                    '(日付不正)
                    If (Not DateTime.TryParse(taskDate, workChkDate)) Then
                        errorList.Add("一斉送信日付が不正なためスケジュールへの登録不可(手動登録して下さい)")
                        Return errorList
                    End If
                ElseIf (AppNum = 2) Then
                    'システム間連携（更新日時）
                    'タスク名
                    taskName = taskSystemTitleName & mtm10r003jitsukou.MTMR003001
                    'アプリケーション
                    taskAppOption = "2 " & mtm10r003jitsukou.MTMR003001
                    taskAppName = executingPath & taskAppName
                    taskAppExe = taskAppName
                    '日付を生成
                    workStrDate = mtm10r003jitsukou.MTMR003006
                    workDatDate = DateTime.ParseExact(workStrDate, "yyyyMMdd", Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)
                    taskDate = workDatDate.ToString("yyyy/MM/dd")
                    '時間を生成
                    If Not String.IsNullOrEmpty(mtm10r003jitsukou.MTMR003006_2) Then
                        If Double.TryParse(mtm10r003jitsukou.MTMR003006_2, dbl) Then
                            If Not ((dbl < 0) Or (dbl > 23)) Then
                                tsskdateTime = mtm10r003jitsukou.MTMR003006_2.PadLeft(2, "0"c) & ":00"
                            End If
                        End If
                    End If

                    '起動チェック
                    '(日付不正)
                    If (Not DateTime.TryParse(taskDate, workChkDate)) Then
                        errorList.Add("更新日付が不正なためスケジュールへの登録不可(手動登録して下さい)")
                        Return errorList
                    End If
                Else
                    '未パラメータ

                End If

                'コマンド生成
                'ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
                process.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec")
                '出力を読み取れるようにする
                process.StartInfo.UseShellExecute = False
                process.StartInfo.RedirectStandardOutput = True
                process.StartInfo.RedirectStandardInput = False
                'ウィンドウを表示しないようにする
                process.StartInfo.CreateNoWindow = True
                'コマンドラインを指定（）
                If (serversetting.ServerStartup = "TRUE") Then
                    process.StartInfo.Arguments = "/c schtasks /create " &
                                             " /S " & serversetting.ServerIp &
                                             " /U " & serversetting.ConnectionUser &
                                             " /P " & serversetting.ConnectionPassword &
                                             " /tn " & taskName &
                                             " /tr " & Chr(34) & taskAppExe & " " & taskAppOption & Chr(34) &
                                             " /sc once /sd " & taskDate & " /st " & tsskdateTime &
                                             " /F " &
                                             " /ru " & serversetting.StartupUser &
                                             " /rp " & serversetting.StartupPassword
                Else
                    process.StartInfo.Arguments = "/c schtasks /create /tn " & taskName & " /tr " & Chr(34) & taskAppExe & " " & taskAppOption & Chr(34) & " /sc once /sd " & taskDate & " /st " & tsskdateTime & " /F"
                End If

                '起動
                process.Start()

                'プロセス終了まで待機時間を設定
                'WaitForExitはReadToEndの後である必要がある
                '(親プロセス、子プロセスでブロック防止のため)
                process.WaitForExit(3000)
                successExitCode = process.ExitCode()
                If (successExitCode <> 0) Then
                    errorList.Add("タスクスケジュールへの登録失敗しました(手動登録して下さい)")
                End If
                process.Close()


            Catch ex As Exception
                errorList.Add(ex.Message)
            Finally
                process?.Dispose()
            End Try

            Return errorList

        End Function

    End Class
End Namespace
