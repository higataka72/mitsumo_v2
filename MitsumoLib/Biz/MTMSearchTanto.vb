Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' ログインID検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchTanto
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 担当者を取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetTanto() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT HANM004001" _
                        + ", HANM004002" _
                        + " FROM HAN10M004TANTO" _
                        + " ORDER BY RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(HANM004001)), 8) ASC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

        ''' <summary>
        ''' 担当者(条件検索)を取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetTanto(ByVal tantousyaName As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT HANM004001" _
                        + ", HANM004002" _
                        + " FROM HAN10M004TANTO" _
                        + " WHERE RTRIM(REPLACE(HANM004002,' ','')) LIKE @HANM004002" _
                        + " ORDER BY RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(HANM004001)), 8) ASC"
                    command.Parameters.Add(New SqlParameter("@HANM004002", "%" & tantousyaName & "%"))

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