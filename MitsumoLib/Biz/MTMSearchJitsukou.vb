Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' 価格入力番号検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchJitsukou
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 実行管理テーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukouAll() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003021" _
                        + ", MTMR003001" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + " FROM MTM10R003JITSUKOU" _
                        + " ORDER BY MTMR003007 DESC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
        ''' <summary>
        ''' 実行管理テーブルデータを取得(本日から未来日付)
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukou() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003001" _
                        + ", MTMR003021" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + " FROM MTM10R003JITSUKOU" _
                        + " WHERE " _
                        + " RTRIM(MTMR003007) <> '' AND RTRIM(MTMR003007) >= CONVERT(NVARCHAR, GETDATE(), 112) " _
                        + " ORDER BY MTMR003007"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
        ''' <summary>
        ''' 実行管理テーブルデータを取得(本日から未来日付)-価格入力用
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukou2(ByVal strLoginID As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003001" _
                        + ", MTMR003021" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + ", CONVERT(int, ISNULL(MTMR002080_CNT, 0)) AS MTMR002080_CNT" _
                        + " FROM MTM10R003JITSUKOU"
                    If (Not String.IsNullOrWhiteSpace(strLoginID)) Then
                        command.CommandText += " RIGHT JOIN "
                        command.CommandText += " (SELECT MTMR002080,COUNT(MTMR002080) AS  MTMR002080_CNT FROM MTM10R002KAKAKU WHERE MTMR002074 ='" + strLoginID + "' AND ISNULL(MTMR002085, 0) = 0 GROUP BY MTMR002080) TANKA "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    Else
                        command.CommandText += " RIGHT JOIN "
                        command.CommandText += " (SELECT MTMR002080,COUNT(MTMR002080) AS  MTMR002080_CNT FROM MTM10R002KAKAKU WHERE ISNULL(MTMR002085, 0) = 0 GROUP BY MTMR002080) TANKA "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    End If
                    command.CommandText += " WHERE "
                    command.CommandText += " RTRIM(MTMR003007) <> '' AND RTRIM(MTMR003007) >= CONVERT(NVARCHAR, GETDATE(), 112) "
                    command.CommandText += " ORDER BY MTMR003007"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
        ''' <summary>
        ''' 実行管理テーブルデータを取得-価格入力用
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukouAll2(ByVal strLoginID As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003021" _
                        + ", MTMR003001" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + ", MTMR002080_CNT" _
                        + " FROM MTM10R003JITSUKOU"
                    If (Not String.IsNullOrWhiteSpace(strLoginID)) Then
                        command.CommandText += " RIGHT JOIN ("
                        command.CommandText += " SELECT MTMR002080, ISNULL(CAST(MAX(MTMR002080_CNT) AS varchar(10)), '') AS MTMR002080_CNT "
                        command.CommandText += " FROM ( "
                        command.CommandText += "  SELECT "
                        command.CommandText += "   MTMR002080,"
                        command.CommandText += "   COUNT(MTMR002085)"
                        command.CommandText += "   - SUM(CASE WHEN ISNULL(MTMR002085,0) <> 0 THEN 1 ELSE 0 END) AS MTMR002080_CNT"
                        command.CommandText += "  FROM MTM10R002KAKAKU"
                        command.CommandText += "  WHERE MTMR002074 = '" + strLoginID + "' "
                        command.CommandText += "  GROUP BY MTMR002080 "
                        command.CommandText += "  UNION ALL"
                        command.CommandText += "  SELECT"
                        command.CommandText += "   MTMR002080,"
                        command.CommandText += "   NULL AS MTMR002080_CNT"
                        command.CommandText += "  FROM MTM10R002KAKAKU"
                        command.CommandText += "  WHERE MTMR002074 <> '" + strLoginID + "' "
                        command.CommandText += "  GROUP BY MTMR002080"
                        command.CommandText += " ) TANKA"
                        command.CommandText += " GROUP BY MTMR002080"
                        command.CommandText += " ) TANKA_ALL"
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA_ALL.MTMR002080 "

                        'command.CommandText += " RIGHT JOIN "
                        'command.CommandText += " (SELECT "
                        'command.CommandText += " MTMR002080 "
                        'command.CommandText += " ,(COUNT(MTMR002085) - SUM(CASE WHEN ISNULL(MTMR002085,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT "
                        'command.CommandText += " FROM MTM10R002KAKAKU "
                        'command.CommandText += " WHERE MTMR002074 = '" + strLoginID + "' GROUP BY MTMR002080) TANKA "
                        'command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    Else
                        command.CommandText += " RIGHT JOIN "
                        command.CommandText += " (SELECT "
                        command.CommandText += " MTMR002080 "
                        command.CommandText += " ,(COUNT(MTMR002085) - SUM(CASE WHEN ISNULL(MTMR002085,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " GROUP BY MTMR002080) TANKA "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    End If
                    command.CommandText += " ORDER BY MTMR003007 DESC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

        ''' <summary>
        ''' 実行管理テーブルデータを取得(本日から未来日付)-見積送信用
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukou3(ByVal strLoginID As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003001" _
                        + ", MTMR003021" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + ", CONVERT(int, ISNULL(MTMR002080_CNT, 0)) AS MTMR002080_CNT2" _
                        + " FROM MTM10R003JITSUKOU"
                    If (Not String.IsNullOrWhiteSpace(strLoginID)) Then
                        command.CommandText += " RIGHT JOIN "
                        command.CommandText += " (SELECT MTMR002080,COUNT(MTMR002080) AS  MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE MTMR002074 ='" + strLoginID + "' AND LTRIM(RTRIM(ISNULL(MTMR002087, ''))) = '' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0 "
                        command.CommandText += " GROUP BY MTMR002080) TANKA "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    Else
                        command.CommandText += " RIGHT JOIN "
                        command.CommandText += " (SELECT MTMR002080,COUNT(MTMR002080) AS  MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE LTRIM(RTRIM(ISNULL(MTMR002087, ''))) = '' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0 "
                        command.CommandText += " GROUP BY MTMR002080) TANKA "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    End If
                    command.CommandText += " WHERE "
                    command.CommandText += " RTRIM(MTMR003007) <> '' AND RTRIM(MTMR003007) >= CONVERT(NVARCHAR, GETDATE(), 112) "
                    command.CommandText += " ORDER BY MTMR003007"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
        ''' <summary>
        ''' 実行管理テーブルデータを取得-見積送信用
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukouAll3(ByVal strLoginID As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003021" _
                        + ", MTMR003001" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + ", MTMR002080_CNT AS MTMR002080_CNT2" _
                        + " FROM MTM10R003JITSUKOU"
                    If (Not String.IsNullOrWhiteSpace(strLoginID)) Then
                        command.CommandText += " RIGHT JOIN ("
                        command.CommandText += " SELECT MTMR002080, ISNULL(CAST(MAX(MTMR002080_CNT) AS varchar(10)), '') AS MTMR002080_CNT "
                        command.CommandText += " FROM ( "
                        command.CommandText += " SELECT MTMR002080,(COUNT(MTMR002087) - SUM(CASE WHEN ISNULL(MTMR002087,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT"
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE MTMR002074 = '" + strLoginID + "' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0"
                        'command.CommandText += " WHERE MTMR002074 = '" + strLoginID + "' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0 "
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " UNION ALL "
                        command.CommandText += " SELECT MTMR002080,NULL AS MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE MTMR002074 <> '" + strLoginID + "'"
                        'command.CommandText += " WHERE MTMR002074 <> '" + strLoginID + "' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0 "
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " ) KAKAKU"
                        command.CommandText += " GROUP BY MTMR002080"
                        command.CommandText += " ) KAKAKU_ALL "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = KAKAKU_ALL.MTMR002080 "

                        'command.CommandText += " RIGHT JOIN "
                        'command.CommandText += " (SELECT "
                        'command.CommandText += " MTMR002080 "
                        'command.CommandText += " ,(COUNT(MTMR002087) - SUM(CASE WHEN ISNULL(MTMR002087,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT "
                        'command.CommandText += " FROM MTM10R002KAKAKU "
                        'command.CommandText += " WHERE MTMR002074 ='" + strLoginID + "' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0 "
                        'command.CommandText += " GROUP BY MTMR002080) TANKA "
                        'command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    Else
                        command.CommandText += " RIGHT JOIN ("
                        command.CommandText += " SELECT MTMR002080, ISNULL(CAST(MAX(MTMR002080_CNT) AS varchar(10)), '') AS MTMR002080_CNT "
                        command.CommandText += " FROM ( "
                        command.CommandText += " SELECT MTMR002080,(COUNT(MTMR002087) - SUM(CASE WHEN ISNULL(MTMR002087,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT"
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0"
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " UNION ALL "
                        command.CommandText += " SELECT MTMR002080,NULL AS MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " ) KAKAKU"
                        command.CommandText += " GROUP BY MTMR002080"
                        command.CommandText += " ) KAKAKU_ALL "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = KAKAKU_ALL.MTMR002080 "

                        'command.CommandText += " RIGHT JOIN "
                        'command.CommandText += " (SELECT "
                        'command.CommandText += " MTMR002080 "
                        'command.CommandText += " ,(COUNT(MTMR002087) - SUM(CASE WHEN ISNULL(MTMR002087,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT "
                        'command.CommandText += " FROM MTM10R002KAKAKU "
                        'command.CommandText += " WHERE ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0 "
                        'command.CommandText += " GROUP BY MTMR002080) TANKA "
                        'command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                    End If
                    command.CommandText += " ORDER BY MTMR003007 DESC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

        ''' <summary>
        ''' 実行管理テーブルデータを取得-検索用
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukouAll4(ByVal strKakakumei As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003021" _
                        + ", MTMR003001" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + " FROM MTM10R003JITSUKOU" _
                        + " WHERE 1=1 "
                    If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                        Dim Search_Str As String() = strKakakumei.Split("　")
                        For i As Integer = 0 To Search_Str.Length - 1
                            command.CommandText += " AND　IsNull(RTRIM(MTMR003002), '') LIKE @MTMR003002" & i & " COLLATE Japanese_CI_AS"  '価格入力名
                        Next
                    End If
                    command.CommandText += " ORDER BY MTMR003007 DESC"

                    If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                        Dim Search_Str As String() = strKakakumei.Split("　")
                        For i As Integer = 0 To Search_Str.Length - 1
                            command.Parameters.Add(New SqlParameter("@MTMR003002" & i, "%" & Search_Str(i) & "%"))
                        Next
                    End If

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

        ''' <summary>
        ''' 実行管理テーブルデータを取得-価格入力用-検索用
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukouAll5(ByVal strLoginID As String, ByVal strKakakumei As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003021" _
                        + ", MTMR003001" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + ", MTMR002080_CNT" _
                        + " FROM MTM10R003JITSUKOU"
                    If (Not String.IsNullOrWhiteSpace(strLoginID)) Then
                        command.CommandText += " RIGHT JOIN ("
                        command.CommandText += " SELECT MTMR002080, ISNULL(CAST(MAX(MTMR002080_CNT) AS varchar(10)), '') AS MTMR002080_CNT "
                        command.CommandText += " FROM ( "
                        command.CommandText += "  SELECT "
                        command.CommandText += "   MTMR002080,"
                        command.CommandText += "   COUNT(MTMR002085)"
                        command.CommandText += "   - SUM(CASE WHEN ISNULL(MTMR002085,0) <> 0 THEN 1 ELSE 0 END) AS MTMR002080_CNT"
                        command.CommandText += "  FROM MTM10R002KAKAKU"
                        command.CommandText += "  WHERE MTMR002074 = '" + strLoginID + "' "
                        command.CommandText += "  GROUP BY MTMR002080 "
                        command.CommandText += "  UNION ALL"
                        command.CommandText += "  SELECT"
                        command.CommandText += "   MTMR002080,"
                        command.CommandText += "   NULL AS MTMR002080_CNT"
                        command.CommandText += "  FROM MTM10R002KAKAKU"
                        command.CommandText += "  WHERE MTMR002074 <> '" + strLoginID + "' "
                        command.CommandText += "  GROUP BY MTMR002080"
                        command.CommandText += " ) TANKA"
                        command.CommandText += " GROUP BY MTMR002080"
                        command.CommandText += " ) TANKA_ALL"
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA_ALL.MTMR002080 "
                        command.CommandText += " WHERE 1=1 AND IsNull(RTRIM(MTMR003002), '') IS NOT NULL"

                        If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                            Dim Search_Str As String() = strKakakumei.Split("　")
                            For i As Integer = 0 To Search_Str.Length - 1
                                command.CommandText += " AND　IsNull(RTRIM(MTMR003002), '') LIKE @MTMR003002" & i & " COLLATE Japanese_CI_AS"  '価格入力名
                            Next
                        End If

                    Else
                        command.CommandText += " RIGHT JOIN "
                        command.CommandText += " (SELECT "
                        command.CommandText += " MTMR002080 "
                        command.CommandText += " ,(COUNT(MTMR002085) - SUM(CASE WHEN ISNULL(MTMR002085,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " GROUP BY MTMR002080) TANKA "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = TANKA.MTMR002080 "
                        command.CommandText += " WHERE 1=1 AND IsNull(RTRIM(MTMR003002), '') IS NOT NULL"

                        If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                            Dim Search_Str As String() = strKakakumei.Split("　")
                            For i As Integer = 0 To Search_Str.Length - 1
                                command.CommandText += " AND　IsNull(RTRIM(MTMR003002), '') LIKE @MTMR003002" & i & " COLLATE Japanese_CI_AS"  '価格入力名
                            Next
                        End If
                    End If
                    command.CommandText += " ORDER BY MTMR003007 DESC"

                    If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                        Dim Search_Str As String() = strKakakumei.Split("　")
                        For i As Integer = 0 To Search_Str.Length - 1
                            command.Parameters.Add(New SqlParameter("@MTMR003002" & i, "%" & Search_Str(i) & "%"))
                        Next
                    End If

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

        ''' <summary>
        ''' 実行管理テーブルデータを取得-見積送信用-検索用
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukouAll6(ByVal strLoginID As String, ByVal strKakakumei As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + " MTMR003002" _
                        + ", MTMR003022" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 4) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", CASE WHEN MTMR003023 IS NOT NULL AND REPLACE(MTMR003023, ' ', '') <> '' THEN SUBSTRING(MTMR003023, 1, 4) + '/' + SUBSTRING(MTMR003023, 5, 2) + '/' + SUBSTRING(MTMR003023, 7, 2) ELSE '' END AS MTMR003023" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", MTMR003021" _
                        + ", MTMR003001" _
                        + ", CASE WHEN MTMR003011 = 1 THEN '表示する' ELSE '' END AS MTMR003011" _
                        + ", CASE WHEN MTMR003012 = 1 THEN '表示する' ELSE '' END AS MTMR003012" _
                        + ", CASE WHEN MTMR003013 = 1 THEN '表示する' ELSE '' END AS MTMR003013" _
                        + ", CASE WHEN MTMR003014 = 1 THEN '表示する' ELSE '' END AS MTMR003014" _
                        + ", CASE WHEN MTMR003015 = 1 THEN '表示する' ELSE '' END AS MTMR003015" _
                        + ", CASE WHEN MTMR003016 = 1 THEN '表示する' ELSE '' END AS MTMR003016" _
                        + ", CASE WHEN MTMR003017 = 1 THEN '表示する' ELSE '' END AS MTMR003017" _
                        + ", CONVERT(int, ISNULL(MTMR003018, 0)) AS MTMR003018" _
                        + ", CASE WHEN MTMR003019 IS NOT NULL AND REPLACE(MTMR003019, ' ', '') <> '' THEN SUBSTRING(MTMR003019, 1, 2) + ':' + SUBSTRING(MTMR003019, 3, 2) + ':' + SUBSTRING(MTMR003019, 5, 2) ELSE '' END AS MTMR003019" _
                        + ", CASE WHEN MTMR003020 IS NOT NULL AND REPLACE(MTMR003020, ' ', '') <> '' THEN SUBSTRING(MTMR003020, 1, 2) + ':' + SUBSTRING(MTMR003020, 3, 2) + ':' + SUBSTRING(MTMR003020, 5, 2) ELSE '' END AS MTMR003020" _
                        + ", MTMR002080_CNT AS MTMR002080_CNT2" _
                        + " FROM MTM10R003JITSUKOU"
                    If (Not String.IsNullOrWhiteSpace(strLoginID)) Then
                        command.CommandText += " RIGHT JOIN ("
                        command.CommandText += " SELECT MTMR002080, ISNULL(CAST(MAX(MTMR002080_CNT) AS varchar(10)), '') AS MTMR002080_CNT "
                        command.CommandText += " FROM ( "
                        command.CommandText += " SELECT MTMR002080,(COUNT(MTMR002087) - SUM(CASE WHEN ISNULL(MTMR002087,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT"
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE MTMR002074 = '" + strLoginID + "' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0"
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " UNION ALL "
                        command.CommandText += " SELECT MTMR002080,NULL AS MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE MTMR002074 <> '" + strLoginID + "'"
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " ) KAKAKU"
                        command.CommandText += " GROUP BY MTMR002080"
                        command.CommandText += " ) KAKAKU_ALL "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = KAKAKU_ALL.MTMR002080 "
                        command.CommandText += " WHERE 1=1 AND IsNull(RTRIM(MTMR003002), '') IS NOT NULL"

                        If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                            Dim Search_Str As String() = strKakakumei.Split("　")
                            For i As Integer = 0 To Search_Str.Length - 1
                                command.CommandText += " AND　IsNull(RTRIM(MTMR003002), '') LIKE @MTMR003002" & i & " COLLATE Japanese_CI_AS"  '価格入力名
                            Next
                        End If

                    Else
                        command.CommandText += " RIGHT JOIN ("
                        command.CommandText += " SELECT MTMR002080, ISNULL(CAST(MAX(MTMR002080_CNT) AS varchar(10)), '') AS MTMR002080_CNT "
                        command.CommandText += " FROM ( "
                        command.CommandText += " SELECT MTMR002080,(COUNT(MTMR002087) - SUM(CASE WHEN ISNULL(MTMR002087,0) <> 0 THEN 1 ELSE 0 END)) AS MTMR002080_CNT"
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " WHERE ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0"
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " UNION ALL "
                        command.CommandText += " SELECT MTMR002080,NULL AS MTMR002080_CNT "
                        command.CommandText += " FROM MTM10R002KAKAKU "
                        command.CommandText += " GROUP BY MTMR002080 "
                        command.CommandText += " ) KAKAKU"
                        command.CommandText += " GROUP BY MTMR002080"
                        command.CommandText += " ) KAKAKU_ALL "
                        command.CommandText += " ON MTM10R003JITSUKOU.MTMR003001 = KAKAKU_ALL.MTMR002080 "
                        command.CommandText += " WHERE 1=1 AND IsNull(RTRIM(MTMR003002), '') IS NOT NULL"

                        If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                            Dim Search_Str As String() = strKakakumei.Split("　")
                            For i As Integer = 0 To Search_Str.Length - 1
                                command.CommandText += " AND　IsNull(RTRIM(MTMR003002), '') LIKE @MTMR003002" & i & " COLLATE Japanese_CI_AS"  '価格入力名
                            Next
                        End If
                    End If
                    command.CommandText += " ORDER BY MTMR003007 DESC"

                    If Not String.IsNullOrWhiteSpace(strKakakumei) Then
                        Dim Search_Str As String() = strKakakumei.Split("　")
                        For i As Integer = 0 To Search_Str.Length - 1
                            command.Parameters.Add(New SqlParameter("@MTMR003002" & i, "%" & Search_Str(i) & "%"))
                        Next
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