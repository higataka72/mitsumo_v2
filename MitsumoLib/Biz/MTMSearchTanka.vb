Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' 単価台帳検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchTanka
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 単価台帳マスタデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetTanka(ByVal scHANMA02001 As String, ByVal scHANMA02002 As String, ByVal scHANMA02003 As String) As DataTable
            Dim table As New DataTable
            Try
                scHANMA02001 = scHANMA02001.Trim()
                scHANMA02002 = scHANMA02002.Trim()
                scHANMA02003 = scHANMA02003.Trim()
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "  RTRIM(HANMA02002) AS HANMA02002" _
                                        + ", ISNULL(RTRIM(HANMA01074),'') AS HANM003103" _
                                        + ", ISNULL(RTRIM(HANMA01075),'') AS HANM003104" _
                                        + ", RTRIM(HANMA02003) AS HANMA02003" _
                                        + ", FORMAT(NULLIF(HANMA01006,0),'N0') AS HANMA01006" _
                                        + ", HANMA01007" _
                                        + ", FORMAT(NULLIF(HANMA02006,0),'N0') AS HANMA02006" _
                                        + ", FORMAT(NULLIF(HANMA02007,0),'N2') AS HANMA02007" _
                                        + ", FORMAT(NULLIF(HANMA02008,0),'N2') AS HANMA02008" _
                                        + ", ISNULL(FORMAT(ROUND(((NULLIF(HANMA02007,0) - NULLIF(HANMA02008,0)) / NULLIF(HANMA02007,0) * 100), 2),'N1'),0) AS ARARI" _
                                        + ", CASE WHEN CONVERT(VARCHAR, HANMA01022) <> '0' THEN SUBSTRING(CONVERT(VARCHAR, HANMA01022), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, HANMA01022), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, HANMA01022), 7, 2) ELSE '' END AS HANMA01022" _
                                        + ", FORMAT(NULLIF(HANMA02011,0),'N2') AS HANMA02011" _
                                        + ", CASE WHEN CONVERT(VARCHAR, HANMA02009) <> '0' THEN SUBSTRING(CONVERT(VARCHAR, HANMA02009), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, HANMA02009), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, HANMA01022), 7, 2) ELSE '' END AS HANMA02009" _
                                        + ", FORMAT(NULLIF(HANMA02012,0),'N2') AS HANMA02012" _
                                        + ", CASE WHEN CONVERT(VARCHAR, HANMA02010) <> '0' THEN SUBSTRING(CONVERT(VARCHAR, HANMA02010), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, HANMA02010), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, HANMA01022), 7, 2) ELSE '' END AS HANMA02010" _
                                        + ", ISNULL(FORMAT(ROUND(((NULLIF(HANMA02011,0) - NULLIF(HANMA02012,0)) / NULLIF(HANMA02011,0) * 100), 2),'N1'),0) AS SHIN_ARARI" _
                                        + ", ISNULL(HANM031003,'') AS HANM031003" _
                                        + ", RTRIM(HANMA01013) AS HANMA01013" _
                                        + ", RTRIM(HANMA01014) AS HANMA01014" _
                                        + ", RTRIM(HANMA01042) AS HANMA01042" _
                                        + ", RTRIM(HANMA01019) AS HANMA01019" _
                                        + ", ISNULL(RTRIM(HANM002006),'') AS HANM002006" _
                                        + " FROM " _
                                        + "  HAN98MA01TANKA" _
                                        + "  LEFT OUTER JOIN " _
                                        + "  HAN98MA02SUTANKA " _
                                        + "  ON  HANMA01001 = HANMA02001 " _
                                        + "  AND HANMA01002 = HANMA02002 " _
                                        + "  AND HANMA02003 = HANMA01003 " _
                                        + "  LEFT OUTER JOIN " _
                                        + "  HAN10M003SHOHIN " _
                                        + "  ON  HANMA02002 = HANM003001 " _
                                        + "  LEFT OUTER JOIN " _
                                        + "  HAN10M002SHIIRE " _
                                        + "  ON  HANMA01019 = HANM002001 " _
                                        + "  LEFT OUTER JOIN " _
                                        + "  (SELECT HANM031002, HANM031003 FROM HAN10M031SANSHO WHERE HANM031001 = '1005') AS SANSHO " _
                                        + "  ON  HANMA01008 = HANM031002 " _
                                        + " WHERE " _
                                        + "  RTRIM(HANMA01001) = RTRIM(@HANMA02001)" _
                                        + "  AND RTRIM(HANMA01002) = RTRIM(@HANMA02002)" _
                                        + "  AND RTRIM(HANMA01003) = RTRIM(@HANMA02003)" _
                                        + "  AND HANMA02004 = 0 " _
                                        + " ORDER BY HANMA01001, HANMA01002, HANMA01003, HANMA02005"
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANMA02001", scHANMA02001))
                    command.Parameters.Add(New SqlParameter("@HANMA02002", scHANMA02002))
                    command.Parameters.Add(New SqlParameter("@HANMA02003", scHANMA02003))
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

