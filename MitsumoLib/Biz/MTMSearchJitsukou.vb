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
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 2) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
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
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) + ' ' + SUBSTRING(MTMR003005, 9, 2) + '時' ELSE '' END AS MTMR003005_KETUGOU" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 2) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
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
    End Class
End Namespace