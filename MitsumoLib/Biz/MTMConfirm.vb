Imports System
Imports System.Data.SqlClient

Namespace Biz
    ''' <summary>
    ''' 起動パスワード確認ビジネスロジック
    ''' </summary>
    Public Class MTMConfirm
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 起動パスワードの確認
        ''' </summary>
        ''' <param name="password">起動パスワード</param>
        ''' <returns>比較結果</returns>
        Public Function ConfirmPassword(ByVal password As String) As MTMConfirmResult
            Dim result As New MTMConfirmResult
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM001002 FROM MTM00M001SEIGYO WHERE MTMM001002 = @MTMM001002"
                    command.Parameters.Add(New SqlParameter("@MTMM001002", password))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        result.Result = True
                        result.Password = reader.Item("MTMM001002").ToString()
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return result
        End Function
    End Class

    Public Class MTMConfirmResult
        ''' <summary>
        ''' 結果
        ''' </summary>
        ''' <returns>True:ログイン可/False:ログイン不可</returns>
        Public Property Result As Boolean = False
        ''' <summary>
        ''' 表示用メッセージ
        ''' </summary>
        ''' <returns></returns>
        Public Property Password As String = ""
    End Class
End Namespace
