Imports System
Imports System.Data.SqlClient
Namespace Biz
    Public Class MTMSearchUriage
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 売上履歴データを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetUriage(ByVal scHANMA02001 As String, ByVal scHANMA02002 As String, ByVal scHANMA02003 As String) As DataTable
            Dim table As New DataTable
            Try
                scHANMA02001 = scHANMA02001.Trim()
                scHANMA02002 = scHANMA02002.Trim()
                scHANMA02003 = scHANMA02003.Trim()
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "CASE WHEN CONVERT(VARCHAR, HANR002001) <> '0' THEN SUBSTRING(CONVERT(VARCHAR, HANR002001), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, HANR002001), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, HANR002001), 7, 2) ELSE '' END AS HANR002001" _
                                        + ", RTRIM(HANR002019) AS HANR002019" _
                                        + ", ISNULL(RTRIM(HANR002020),'') AS HANR002020" _
                                        + ", ISNULL(RTRIM(HANR002118),'') AS HANR002118" _
                                        + ", FORMAT(NULLIF(HANR002022,0),'N0') AS HANR002022" _
                                        + ", FORMAT(NULLIF(HANR002025,0),'N0') AS HANR002025" _
                                        + ", RTRIM(HANR002021) AS HANR002021" _
                                        + ", FORMAT(NULLIF(HANR002026,0),'N2') AS HANR002026" _
                                        + ", FORMAT(NULLIF(HANR002028,0),'N2') AS HANR002028" _
                                        + ", ISNULL(FORMAT(ROUND(((NULLIF(HANR002034,0)) / NULLIF(HANR002027,0) * 100), 2),'N1'),0) AS ARARI" _
                                        + ", FORMAT(NULLIF(HANR002027,0),'N0') AS HANR002027" _
                                        + ", FORMAT((HANR002025 * HANR002A004),'N0') AS SHIIRE_TANKA" _
                                        + ", ISNULL(RTRIM(HANM031003),'') AS HANM031003" _
                                        + ", ISNULL(RTRIM(HANM007003),'') AS HANM007003" _
                                        + ", ISNULL(RTRIM(HANM007005),'') AS HANM007005" _
                                        + ", ISNULL(RTRIM(HANM002006),'') AS HANM002006" _
                                        + ", ISNULL(RTRIM(HANR002A002),'') AS HANR002A002" _
                                        + " FROM " _
                                        + "  HAN10R002TORIHIKIM" _
                                        + "  LEFT OUTER JOIN " _
                                        + "  (SELECT HANM031002, HANM031003 FROM HAN10M031SANSHO WHERE HANM031001 = '1005') AS SANSHO " _
                                        + "  ON  HANR002A005 = HANM031002 " _
                                        + "  LEFT OUTER JOIN " _
                                        + "  HAN10R001TORIHIKIH " _
                                        + "  ON HANR002003 = HANR001005 " _
                                        + "  LEFT OUTER JOIN " _
                                        + "  HAN10M007NOHIN " _
                                        + "  ON HANR001002 = HANM007001 AND HANR001012 = HANM007002 " _
                                        + "  LEFT OUTER JOIN " _
                                        + "  HAN98MA01TANKA " _
                                        + "  ON HANR002008 = HANMA01001　AND HANR002019 = HANMA01002 AND HANR002A002 = HANMA01003 " _
                                        + "  LEFT OUTER JOIN " _
                                        + "  HAN10M002SHIIRE " _
                                        + "  ON HANMA01019 = HANM002003 " _
                                        + " WHERE " _
                                        + "　RTRIM(HANR002008) = RTRIM(@HANR002008)" _
                                        + "  AND RTRIM(HANR002019) = RTRIM(@HANR002019)" _
                                        + "  AND RTRIM(HANR002A002) = RTRIM(@HANR002A002)" _
                                        + "  AND HANR002A001 = 1 AND HANR002002 = 3 AND HANR002005 = 0 " _
                                        + " ORDER BY HANR002001 DESC, HANR002004, HANR002006"
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANR002008", scHANMA02001))
                    command.Parameters.Add(New SqlParameter("@HANR002019", scHANMA02002))
                    command.Parameters.Add(New SqlParameter("@HANR002A002", scHANMA02003))
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
