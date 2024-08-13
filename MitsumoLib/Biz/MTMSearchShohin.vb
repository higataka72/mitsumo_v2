Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' 商品コード検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchShohin
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 商品マスターテーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetShohin() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT RTRIM(HANM003001) AS HANM003001" _
                        + ", RTRIM(HANM003103) AS HANM003103" _
                        + ", RTRIM(HANM003104) AS HANM003104" _
                        + " FROM HAN10M003SHOHIN" _
                        + " ORDER BY HANM003001 ASC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

        ''' <summary>
        ''' 商品マスター(条件検索)テーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetShohin(ByVal shouhinName As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT RTRIM(HANM003001) AS HANM003001" _
                        + ", RTRIM(HANM003103) AS HANM003103" _
                        + ", RTRIM(HANM003104) AS HANM003104" _
                        + " FROM HAN10M003SHOHIN" _
                        + " WHERE " _
                        + "  RTRIM(HANM003103) LIKE @HANM003103" _
                        + "  OR RTRIM(HANM003104) LIKE @HANM003104" _
                        + " ORDER BY HANM003001 ASC"
                    command.Parameters.Add(New SqlParameter("@HANM003103", "%" & shouhinName & "%"))
                    command.Parameters.Add(New SqlParameter("@HANM003104", "%" & shouhinName & "%"))

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
