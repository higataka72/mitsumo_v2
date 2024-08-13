Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' ログインID検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchUser
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' ユーザーマスタデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetUser() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM002001" _
                        + ", MTMM002002" _
                        + " FROM MTM00M002USER" _
                        + " ORDER BY MTMM002001 ASC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
    End Class
End Namespace
