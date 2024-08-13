Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' 得意先コード検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchTokui
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 得意先マスターテーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetTokui() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT HANM001001" _
                        + ", HANM001002" _
                        + ", HANM001003" _
                        + ", RTRIM(HANM001004) + ' ' + RTRIM(HANM001005) AS HANM001004" _
                        + ", HANM001005" _
                        + ", HANM001006" _
                        + ", HANM001007" _
                        + ", HANM001008" _
                        + ", HANM001009" _
                        + ", HANM001010" _
                        + ", HANM001011" _
                        + ", HANM001012" _
                        + ", HANM001013" _
                        + ", HANM001014" _
                        + " FROM HAN10M001TOKUI" _
                        + " ORDER BY HANM001001 ASC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

        ''' <summary>
        ''' 得意先マスタ(条件検索)ーテーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetTokui(ByVal tokuisakiName As String, ByVal sakuin As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT HANM001001" _
                        + ", HANM001002" _
                        + ", HANM001003" _
                        + ", RTRIM(HANM001004) + ' ' + RTRIM(HANM001005) AS HANM001004" _
                        + ", HANM001005" _
                        + ", HANM001006" _
                        + ", HANM001007" _
                        + ", HANM001008" _
                        + ", HANM001009" _
                        + ", HANM001010" _
                        + ", HANM001011" _
                        + ", HANM001012" _
                        + ", HANM001013" _
                        + ", HANM001014" _
                        + " FROM HAN10M001TOKUI" _
                        + " WHERE 1=1 "
                    If (Not String.IsNullOrEmpty(tokuisakiName)) Then
                        command.CommandText += " AND (RTRIM(HANM001004) LIKE @HANM001004"
                        command.CommandText += " OR RTRIM(HANM001005) LIKE @HANM001005)"
                    End If
                    If (Not String.IsNullOrEmpty(sakuin)) Then
                        command.CommandText += " AND RTRIM(REPLACE(HANM001007,' ','')) LIKE @HANM001007"
                    End If
                    command.CommandText += " ORDER BY HANM001001 ASC"

                    If (Not String.IsNullOrEmpty(tokuisakiName)) Then
                        command.Parameters.Add(New SqlParameter("@HANM001004", "%" & tokuisakiName & "%"))
                        command.Parameters.Add(New SqlParameter("@HANM001005", "%" & tokuisakiName & "%"))
                    End If
                    If (Not String.IsNullOrEmpty(sakuin)) Then
                        command.Parameters.Add(New SqlParameter("@HANM001007", "%" & sakuin & "%"))
                    End If

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
