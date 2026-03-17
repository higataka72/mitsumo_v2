Imports System.Data.SqlClient

Namespace Biz
    Public Class MTM07
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Public Function GetJitsukou(ByVal searchCondition As MTM07SearchCondition) As Models.MTM10R003JITSUKOU
            Dim jitsukou As New Models.MTM10R003JITSUKOU
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMR003001" _                                                                                                                                                                                                         '価格入力番号
                        + ", MTMR003002" _                                                                                                                                                                                                                              '価格入力名
                        + ", CASE WHEN CONVERT(VARCHAR, MTMR003005) <> '' THEN SUBSTRING(CONVERT(VARCHAR, MTMR003005), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR003005), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR003005), 7, 2) ELSE '' END AS MTMR003005" _   '一斉送信日時
                        + ", CASE WHEN CONVERT(VARCHAR, MTMR003005) <> '' THEN SUBSTRING(CONVERT(VARCHAR, MTMR003005), 9, 2) ELSE '' END AS MTMR003005_2" _                                                                                                             '一斉送信日時(時間)
                        + ", CASE WHEN CONVERT(VARCHAR, MTMR003007) <> '' THEN SUBSTRING(CONVERT(VARCHAR, MTMR003007), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR003007), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR003007), 7, 2) ELSE '' END AS MTMR003007" _   '締切日
                        + ", ISNULL(MTMR003011,'0') AS MTMR003011" _   '値下前売単価
                        + ", ISNULL(MTMR003012,'0') AS MTMR003012" _   '値下前仕入単価
                        + ", ISNULL(MTMR003013,'0') AS MTMR003013" _   '値下前粗利率
                        + ", ISNULL(MTMR003014,'0') AS MTMR003014" _   '現売㎡単価
                        + ", ISNULL(MTMR003015,'0') AS MTMR003015" _   '現仕㎡単価
                        + ", ISNULL(MTMR003016,'0') AS MTMR003016" _   '新売㎡単価
                        + ", ISNULL(MTMR003017,'0') AS MTMR003017" _   '新仕㎡単価
                        + " FROM MTM10R003JITSUKOU" _
                        + " WHERE MTMR003001 = @MTMR003001"
                    command.Parameters.Add(New SqlParameter("@MTMR003001", searchCondition.EigyosyoCodeFrom))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        jitsukou.MTMR003001 = reader.Item("MTMR003001")                 '価格入力番号
                        jitsukou.MTMR003002 = reader.Item("MTMR003002").ToString        '価格入力名
                        jitsukou.MTMR003005 = reader.Item("MTMR003005").ToString        '一斉送信日時
                        jitsukou.MTMR003005_2 = reader.Item("MTMR003005_2").ToString    '一斉送信日時(時間)
                        jitsukou.MTMR003007 = reader.Item("MTMR003007").ToString        '締切日
                        jitsukou.MTMR003011 = reader.Item("MTMR003011").ToString        '値下前売単価
                        jitsukou.MTMR003012 = reader.Item("MTMR003012").ToString        '現売㎡単価
                        jitsukou.MTMR003013 = reader.Item("MTMR003013").ToString        '新売㎡単価
                        jitsukou.MTMR003014 = reader.Item("MTMR003014").ToString        '値下前仕入単価
                        jitsukou.MTMR003015 = reader.Item("MTMR003015").ToString        '現仕㎡単価
                        jitsukou.MTMR003016 = reader.Item("MTMR003016").ToString        '新仕㎡単価
                        jitsukou.MTMR003017 = reader.Item("MTMR003017").ToString        '値下前粗利率
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return jitsukou
        End Function

        ''' <summary>
        ''' 価格入力データの取得(未確定)
        ''' </summary>
        ''' <param name="searchCondition"></param>
        ''' <returns></returns>
        Public Function GetKakaku(ByVal searchCondition As MTM07SearchCondition) As DataTable
            Dim table As New DataTable

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + "　CASE WHEN MTMR003007 = '' OR MTMR003007 IS NULL THEN '' ELSE CONVERT(VARCHAR(10), CAST(MTMR003007 AS DATE), 111) END AS MTMR003007" _ '社内締切日
                        + ", CASE WHEN MTMR003023 = '' OR MTMR003023 IS NULL THEN '' ELSE CONVERT(VARCHAR(10), CAST(MTMR003023 AS DATE), 111) END AS MTMR003023" _ '仕入先実施日
                        + ", MTMR003001" _ '価格入力番号
                        + ", MTMR003002" _ '価格入力名
                        + ", RTRIM(MTMR002008) AS MTMR002008" _     '営業所コード
                        + ", RTRIM(MTMR002009) AS MTMR002009" _     '営業所
                        + ", RTRIM(MTMR002010) AS MTMR002010" _     '部課コード
                        + ", RTRIM(MTMR002011) AS MTMR002011" _     '部課
                        + ", RTRIM(MTMR002012) AS MTMR002012" _     '担当者コード
                        + ", RTRIM(MTMR002013) AS MTMR002013" _     '担当者
                        + ", COUNT(MTMR002085) AS MTMR002085_CNT" _ '未確定数
                        + " FROM MTM10R002KAKAKU" _
                        + " LEFT JOIN MTM10R003JITSUKOU" _
                        + " ON MTM10R002KAKAKU.MTMR002080 = MTM10R003JITSUKOU.MTMR003001" _
                        + " WHERE ISNULL(MTMR002085, 0) = 0"
                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiFrom) Then
                        command.CommandText += " AND RTRIM(MTMR003007) >= @MTMR003007_From"         '社内締切日(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiTo) Then
                        command.CommandText += " AND RTRIM(MTMR003007) <= @MTMR003007_To"           '社内締切日(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiFrom) Then
                        command.CommandText += " AND RTRIM(MTMR003023) >= @MTMR003023_From"         '仕入先実施日(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiTo) Then
                        command.CommandText += " AND RTRIM(MTMR003023) <= @MTMR003023_To"           '仕入先実施日(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeFrom) Then
                        command.CommandText += " AND MTMR002008 >= @MTMR002008_From"         '営業所コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeTo) Then
                        command.CommandText += " AND MTMR002008 <= @MTMR002008_To"           '営業所コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeFrom) Then
                        command.CommandText += " AND MTMR002010 >= @MTMR002010_From"         '部課コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeTo) Then
                        command.CommandText += " AND MTMR002010 <= @MTMR002010_To"           '部課コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.CommandText += " AND MTMR002012 >= @MTMR002012_From"         '担当者コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.CommandText += " AND MTMR002012 <= @MTMR002012_To"           '担当者コード(To)
                    End If
                    command.CommandText += " GROUP BY MTMR003001, MTMR003002, MTMR003007, MTMR003023, RTRIM(MTMR002008), RTRIM(MTMR002009), RTRIM(MTMR002010), RTRIM(MTMR002011), RTRIM(MTMR002012), RTRIM(MTMR002013)"
                    command.CommandText += " ORDER BY MTMR003007,MTMR003023,MTMR003001,RTRIM(MTMR002008),RTRIM(MTMR002010),RTRIM(MTMR002012)"

                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003007_From", searchCondition.SimekiribiFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003007_To", searchCondition.SimekiribiTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003023_From", searchCondition.JitsushibiFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003023_To", searchCondition.JitsushibiTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002008_From", searchCondition.EigyosyoCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002008_To", searchCondition.EigyosyoCodeTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002010_From", searchCondition.BukaCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002010_To", searchCondition.BukaCodeTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002012_From", searchCondition.TantousyaCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002012_To", searchCondition.TantousyaCodeTo))
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
        ''' 価格入力データの取得(未送信)
        ''' </summary>
        ''' <param name="searchCondition"></param>
        ''' <returns></returns>
        Public Function GetKakaku2(ByVal searchCondition As MTM07SearchCondition) As DataTable
            Dim table As New DataTable

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + "　CASE WHEN MTMR003007 = '' OR MTMR003007 IS NULL THEN '' ELSE CONVERT(VARCHAR(10), CAST(MTMR003007 AS DATE), 111) END AS MTMR003007" _ '社内締切日
                        + ", CASE WHEN MTMR003023 = '' OR MTMR003023 IS NULL THEN '' ELSE CONVERT(VARCHAR(10), CAST(MTMR003023 AS DATE), 111) END AS MTMR003023" _ '仕入先実施日
                        + ", MTMR003001" _ '価格入力番号
                        + ", MTMR003002" _ '価格入力名
                        + ", RTRIM(MTMR002008) AS MTMR002008" _     '営業所コード
                        + ", RTRIM(MTMR002009) AS MTMR002009" _     '営業所
                        + ", RTRIM(MTMR002010) AS MTMR002010" _     '部課コード
                        + ", RTRIM(MTMR002011) AS MTMR002011" _     '部課
                        + ", RTRIM(MTMR002012) AS MTMR002012" _     '担当者コード
                        + ", RTRIM(MTMR002013) AS MTMR002013" _     '担当者
                        + ", COUNT(MTMR002085) AS MTMR002085_CNT" _ '未確定数
                        + " FROM MTM10R002KAKAKU" _
                        + " LEFT JOIN MTM10R003JITSUKOU" _
                        + " ON MTM10R002KAKAKU.MTMR002080 = MTM10R003JITSUKOU.MTMR003001" _
                        + " WHERE LTRIM(RTRIM(ISNULL(MTMR002087, ''))) = '' AND ISNULL(MTMR002085, 0) <> 0 AND ISNULL(MTMR002032, 0) <> 0 AND ISNULL(MTMR002086, 0) = 0"
                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiFrom) Then
                        command.CommandText += " AND RTRIM(MTMR003007) >= @MTMR003007_From"         '社内締切日(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiTo) Then
                        command.CommandText += " AND RTRIM(MTMR003007) <= @MTMR003007_To"           '社内締切日(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiFrom) Then
                        command.CommandText += " AND RTRIM(MTMR003023) >= @MTMR003023_From"         '仕入先実施日(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiTo) Then
                        command.CommandText += " AND RTRIM(MTMR003023) <= @MTMR003023_To"           '仕入先実施日(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeFrom) Then
                        command.CommandText += " AND MTMR002008 >= @MTMR002008_From"         '営業所コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeTo) Then
                        command.CommandText += " AND MTMR002008 <= @MTMR002008_To"           '営業所コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeFrom) Then
                        command.CommandText += " AND MTMR002010 >= @MTMR002010_From"         '部課コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeTo) Then
                        command.CommandText += " AND MTMR002010 <= @MTMR002010_To"           '部課コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.CommandText += " AND MTMR002012 >= @MTMR002012_From"         '担当者コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.CommandText += " AND MTMR002012 <= @MTMR002012_To"           '担当者コード(To)
                    End If
                    command.CommandText += " GROUP BY MTMR003001, MTMR003002, MTMR003007, MTMR003023, RTRIM(MTMR002008), RTRIM(MTMR002009), RTRIM(MTMR002010), RTRIM(MTMR002011), RTRIM(MTMR002012), RTRIM(MTMR002013)"
                    command.CommandText += " ORDER BY MTMR003007,MTMR003023,MTMR003001,RTRIM(MTMR002008),RTRIM(MTMR002010),RTRIM(MTMR002012)"

                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003007_From", searchCondition.SimekiribiFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.SimekiribiTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003007_To", searchCondition.SimekiribiTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003023_From", searchCondition.JitsushibiFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.JitsushibiTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR003023_To", searchCondition.JitsushibiTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002008_From", searchCondition.EigyosyoCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002008_To", searchCondition.EigyosyoCodeTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002010_From", searchCondition.BukaCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002010_To", searchCondition.BukaCodeTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002012_From", searchCondition.TantousyaCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002012_To", searchCondition.TantousyaCodeTo))
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
        ''' 更新処理
        ''' </summary>
        ''' <param name="table"></param>
        ''' <returns></returns>
        Public Function Update(ByVal table As DataTable) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction

                        For Each row As DataRow In table.Rows
                            command.CommandText = "UPDATE MTM10R002KAKAKU SET "
                            command.CommandText += "MTMR002085 = @MTMR002085" _             '確定フラグ
                                + ", MTMR002086 = @MTMR002086" _                            '見積書送付フラグ
                                + ", MTMR002017 = @MTMR002017" _                            'ロット
                                + ", MTMR002030 = @MTMR002030" _                            '新売上単価
                                + ", MTMR002031 = @MTMR002031" _                            '新売㎡単価
                                + ", MTMR002032 = @MTMR002032" _                            '売実施日
                                + ", MTMR002033 = @MTMR002033" _                            '新仕入単価
                                + ", MTMR002034 = @MTMR002034" _                            '新仕㎡単価
                                + ", MTMR002035 = @MTMR002035" _                            '仕実施日
                                + ", MTMR002053 = @MTMR002053" _                            '納品先履歴（過去18カ月）
                                + ", MTMR002054 = @MTMR002054" _                            '得意先FAX
                                + ", MTMR002072 = @MTMR002072" _                            '備考（見積書）
                                + ", MTMR002076 = @MTMR002076" _                            '宛先送信区分
                                + ", MTMR002079 = @MTMR002079" _                            '宛先名
                                + ", MTMR002078 = @MTMR002078" _                            'メールアドレス
                                + ", MTMR002077 = @MTMR002077" _                            'ＦＡＸ宛先
                                + " WHERE MTMR002001 = @MTMR002001" _                       '得意先コード
                                + " AND MTMR002002 = @MTMR002002" _                         '商品コード
                                + " AND MTMR002003 = @MTMR002003" _                         '規格
                                + " AND MTMR002004 = @MTMR002004" _                         '数量・上限
                                + " AND MTMR002080 = @MTMR002080"                           '価格入力番号
                            Dim dt As Date
                            command.Parameters.Clear()
                            Dim setMTMR002085 As Integer = 0
                            If row.Item("MTMR002085").ToString() = "True" Then
                                setMTMR002085 = Integer.Parse(Date.Now.ToString("yyyyMMdd"))
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR002085", setMTMR002085))

                            Dim setMTMR002086 As Integer = 0
                            If row.Item("MTMR002086").ToString() = "True" Then
                                setMTMR002086 = 1
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR002086", setMTMR002086))
                            'command.Parameters.Add(New SqlParameter("@MTMR002086", row.Item("MTMR002086")))

                            command.Parameters.Add(New SqlParameter("@MTMR002017", row.Item("MTMR002017").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002030", row.Item("MTMR002030").ToString.Replace(",", "")))
                            command.Parameters.Add(New SqlParameter("@MTMR002031", row.Item("MTMR002031").ToString.Replace(",", "")))
                            If (String.IsNullOrEmpty(row.Item("MTMR002032").ToString)) Then
                                command.Parameters.Add(New SqlParameter("@MTMR002032", 0))
                            Else
                                command.Parameters.Add(New SqlParameter("@MTMR002032", If(Date.TryParse(row.Item("MTMR002032").ToString, dt), Integer.Parse(dt.ToString("yyyyMMdd")), SqlTypes.SqlDecimal.Null)))

                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR002033", row.Item("MTMR002033").ToString.Replace(",", "")))
                            command.Parameters.Add(New SqlParameter("@MTMR002034", row.Item("MTMR002034").ToString.Replace(",", "")))
                            If (String.IsNullOrEmpty(row.Item("MTMR002035").ToString)) Then
                                command.Parameters.Add(New SqlParameter("@MTMR002035", 0))
                            Else
                                command.Parameters.Add(New SqlParameter("@MTMR002035", If(Date.TryParse(row.Item("MTMR002035").ToString, dt), Integer.Parse(dt.ToString("yyyyMMdd")), SqlTypes.SqlDecimal.Null)))
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR002053", row.Item("MTMR002053").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002054", row.Item("MTMR002054").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002072", row.Item("MTMR002072").ToString()))

                            Dim setMTMR002076 As Integer = 1
                            If row.Item("MTMR002076MAIL").ToString() = "True" Then
                                setMTMR002076 = 1
                            ElseIf row.Item("MTMR002076FAX").ToString() = "True" Then
                                setMTMR002076 = 2
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR002076", setMTMR002076))

                            command.Parameters.Add(New SqlParameter("@MTMR002079", row.Item("MTMR002079").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002078", row.Item("MTMR002078").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002077", row.Item("MTMR002077").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002001", row.Item("MTMR002001").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002002", row.Item("MTMR002002").ToString()))
                            command.Parameters.Add(New SqlParameter("@MTMR002003", row.Item("MTMR002003").ToString()))
                            Dim decMTMR002004 As Decimal = 0.0000
                            If (Decimal.TryParse(row.Item("MTMR002004").ToString(), decMTMR002004)) Then
                                decMTMR002004 = Format(decMTMR002004, "#0.0000")
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR002004", decMTMR002004))
                            command.Parameters.Add(New SqlParameter("@MTMR002080", row.Item("MTMR002080")))
                            command.ExecuteNonQuery()
                        Next

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                errorList.Add(ex.Message)
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 納品先履歴の変換
        ''' </summary>
        ''' <param name="checkTarget"></param>
        ''' <returns>String</returns>
        Public Function DeliveryConvert(checkTarget As String) As String
            Dim reString As String = ""
            Dim strConvert As String
            Dim LenB As Integer = 0

            '半角かなコンバート
            If (Not String.IsNullOrEmpty(checkTarget)) Then
                strConvert = StrConv(checkTarget, VbStrConv.Narrow)
                '100バイト変換
                reString = CutStrByteLen(strConvert, 100)
            End If

            Return reString

        End Function
        ''' <summary>
        ''' 文字列バイト数変換
        ''' </summary>
        ''' <param name="strInput"></param>
        ''' <param name="intLen"></param>
        ''' <returns>String</returns>
        Public Function CutStrByteLen(ByVal strInput As String, ByVal intLen As Integer) As String
            Dim sjis As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim tempLen As Integer = sjis.GetByteCount(strInput)
            ' 引数チェック
            If intLen < 0 OrElse strInput.Length <= 0 Then
                Return ""
            End If
            ' 文字列が指定のバイト数未満の場合は、入力をそのまま返す
            If tempLen <= intLen Then
                Return strInput
            End If
            Dim bytTemp As Byte() = sjis.GetBytes(strInput)
            Dim strTemp As String = sjis.GetString(bytTemp, 0, intLen) + " 他"
            If strTemp.EndsWith(ControlChars.NullChar) OrElse strTemp.EndsWith("・") Then
                strTemp = sjis.GetString(bytTemp, 0, intLen - 1) + " 他"
            End If
            Return strTemp
        End Function

        ''' <summary>
        ''' 文字列桁数チェック
        ''' </summary>
        ''' <param name="checkTarget"></param>
        ''' <param name="checkDigit"></param>
        ''' <returns>Boolean</returns>
        Public Function StringNumberDigitsCheck(checkTarget As String, checkDigit As Integer) As Boolean
            Dim check As Boolean = False
            If (String.IsNullOrWhiteSpace(checkTarget)) Then
                check = True
            ElseIf (Len(checkTarget) <= checkDigit) Then
                check = True
            End If

            Return check

        End Function
        ''' <summary>
        ''' バイト桁数チェック
        ''' </summary>
        ''' <param name="checkTarget"></param>
        ''' <returns>Boolean</returns>
        Public Function StringNumberByteDigitsCheck(checkTarget As String, intLen As Integer) As Boolean
            Dim reBoole As Boolean = False
            Dim LenB As Integer = 0

            '半角かなコンバート
            If (Not String.IsNullOrEmpty(checkTarget)) Then
                Dim sjis As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                Dim tempLen As Integer = sjis.GetByteCount(checkTarget)
                ' 文字列が指定のバイト数未満の場合は、入力をそのまま返す
                If tempLen <= intLen Then
                    Return True
                End If
            End If

            Return reBoole

        End Function
    End Class

    Public Class MTM07SearchCondition
        ''' <summary>
        ''' 出力対象の指定(未確定のみ出力)
        ''' </summary>
        ''' <returns></returns>
        Public Property Mikakutei As Boolean = False
        ''' <summary>
        ''' 出力対象の指定(未送信のみ出力)
        ''' </summary>
        ''' <returns></returns>
        Public Property Misoushin As Boolean = False
        ''' <summary>
        ''' 締切日(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property SimekiribiFrom As String = ""
        ''' <summary>
        ''' 締切日(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property SimekiribiTo As String = ""
        ''' <summary>
        ''' 仕入先実施日(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property JitsushibiFrom As String = ""
        ''' <summary>
        ''' 仕入先実施日(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property JitsushibiTo As String = ""
        ''' <summary>
        ''' 営業所コード(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property EigyosyoCodeFrom As String = ""
        ''' <summary>
        ''' 営業所コード(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property EigyosyoCodeTo As String = ""
        ''' <summary>
        ''' 部課コード(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property BukaCodeFrom As String = ""
        ''' <summary>
        ''' 部課コード(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property BukaCodeTo As String = ""
        ''' <summary>
        ''' 担当者コード(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property TantousyaCodeFrom As String = ""
        ''' <summary>
        ''' 担当者コード(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property TantousyaCodeTo As String = ""

        Public Sub Clear()
            Me.Mikakutei = False
            Me.Misoushin = False
            Me.SimekiribiFrom = ""
            Me.SimekiribiTo = ""
            Me.JitsushibiFrom = ""
            Me.JitsushibiTo = ""
            Me.EigyosyoCodeFrom = ""
            Me.EigyosyoCodeTo = ""
            Me.BukaCodeFrom = ""
            Me.BukaCodeTo = ""
            Me.TantousyaCodeFrom = ""
            Me.TantousyaCodeTo = ""
            Me.Mikakutei = False
        End Sub
    End Class
End Namespace