Imports log4net
Module MitsumoTask
    Sub Main(ByVal cmdArgs() As String)
        Dim log As ILog = LogManager.GetLogger(Reflection.MethodBase.GetCurrentMethod().Name)

        If cmdArgs.Length < 1 Then
            log.Error("エラー発生：引数がありません")
            Console.WriteLine("エラー発生：引数がありません")
            Exit Sub
        End If

        Dim appStartNo As String = cmdArgs(0)

        Dim cmdOptions As String()
        ReDim cmdOptions(cmdArgs.GetUpperBound(0) - 1)

        Array.Copy(cmdArgs, 1, cmdOptions, 0, cmdArgs.GetUpperBound(0))

        If appStartNo = "1" Then
            log.Info("アプリ起動番号：1.見積送信")
            Console.WriteLine("アプリ起動番号：1.見積送信")
            Dim task As New MTM03Task
            task.Execute(cmdOptions)
        ElseIf appStartNo = "2" Then
            log.Info("アプリ起動番号：2.システム間連携")
            Console.WriteLine("アプリ起動番号：2.システム間連携")
            Dim task As New MTM04Task
            task.Execute(cmdOptions)
        End If
    End Sub
End Module
