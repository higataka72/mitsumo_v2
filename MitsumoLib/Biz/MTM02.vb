Imports System.Data.SqlClient

Namespace Biz
    Public Class MTM02
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' バリデーション前処理(検索用)
        ''' </summary>
        ''' <param name="searchCondition"></param>
        ''' <returns>List</returns>
        Public Function ValidateBeforeSearch(ByVal searchCondition As MTM02SearchCondition) As List(Of String)
            Dim errorList As New List(Of String)

            If String.IsNullOrWhiteSpace(searchCondition.KakakuNyuuryokuNo) Then
                errorList.Add("価格入力番号が空です")
            End If

            Return errorList
        End Function

        Public Function GetJitsukou(ByVal searchCondition As MTM02SearchCondition) As Models.MTM10R003JITSUKOU
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
                    command.Parameters.Add(New SqlParameter("@MTMR003001", searchCondition.KakakuNyuuryokuNo))

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
        ''' 価格入力データの取得
        ''' </summary>
        ''' <param name="searchCondition"></param>
        ''' <returns></returns>
        Public Function GetKakaku(ByVal searchCondition As MTM02SearchCondition) As DataTable
            Dim table As New DataTable

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMR002085" _ '確定
                        + ", MTMR002086" _ '印刷しない
                        + ", RTRIM(MTMR002009) AS MTMR002009" _ '営業所
                        + ", RTRIM(MTMR002011) AS MTMR002011" _ '部課
                        + ", RTRIM(MTMR002013) AS MTMR002013" _ '担当者
                        + ", IsNull(RTRIM(HANM001004), '') + '　' + IsNull(RTRIM(HANM001005), '') AS MTMR002005" _ '得意先名
                        + ", RTRIM(MTMR002007) AS MTMR002007" _ 'ランク
                        + ", RTRIM(MTMR002002) AS MTMR002002" _ '商品C
                        + ", RTRIM(MTMR002016) AS MTMR002016" _ '商品名
                        + ", RTRIM(MTMR002003) AS MTMR002003" _ '規格
                        + ", RTRIM(MTMR002017) AS MTMR002017" _ 'ロット
                        + ", ISNULL(MTMR002004,0) AS MTMR002004" _ '数量・上限
                        + ", FORMAT(ISNULL(MTMR002019,0), 'N0') AS MTMR002019" _ '入数
                        + ", MTMR002020" _ '単位
                        + ", FORMAT(ISNULL(MTMR002022,0), 'N2') AS MTMR002022" _ '改定前売単価
                        + ", FORMAT(ISNULL(MTMR002023,0), 'N2') AS MTMR002023" _ '改定前仕単価
                        + ", FORMAT(ISNULL(MTMR002024,0), 'N1') AS MTMR002024" _ '改定前粗利率
                        + ", FORMAT(ISNULL(MTMR002025,0), 'N2') AS MTMR002025" _ '現売単価
                        + ", FORMAT(ISNULL(MTMR002026,0), 'N2') AS MTMR002026" _ '現売㎡単価
                        + ", FORMAT(ISNULL(MTMR002027,0), 'N2') AS MTMR002027" _ '現仕単価
                        + ", FORMAT(ISNULL(MTMR002028,0), 'N2') AS MTMR002028" _ '現仕㎡単価
                        + ", FORMAT(ISNULL(MTMR002029,0), 'N1') AS MTMR002029" _ '現粗利率
                        + ", FORMAT(ISNULL(MTMR002030,0), 'N2') AS MTMR002030" _ '新売単価
                        + ", FORMAT(ISNULL(MTMR002031,0), 'N2') AS MTMR002031" _ '新仕㎡単価
                        + ", CASE WHEN CONVERT(VARCHAR, MTMR002032) <> '0' THEN SUBSTRING(CONVERT(VARCHAR, MTMR002032), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002032), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002032), 7, 2) ELSE '' END AS MTMR002032" _ '売実施日
                        + ", FORMAT(ISNULL(MTMR002033,0), 'N2') AS MTMR002033" _ '新仕単価
                        + ", FORMAT(ISNULL(MTMR002034,0), 'N2') AS MTMR002034" _ '新仕㎡単価
                        + ", CASE WHEN CONVERT(VARCHAR, MTMR002035) <> '0' THEN SUBSTRING(CONVERT(VARCHAR, MTMR002035), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002035), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002035), 7, 2) ELSE '' END AS MTMR002035" _ '仕実施日
                        + ", FORMAT(ISNULL(ROUND(((NULLIF(MTMR002030, 0) - NULLIF(MTMR002033, 0)) / NULLIF(MTMR002030, 0) * 100), 2), 0.0), 'N1') AS MTMR002036_ARARI" _ '新粗利率
                        + ", CASE WHEN MTMR002029=0.0" _
                        + "   THEN '0.0'" _
                        + "   ELSE" _
                        + "    CASE WHEN CAST(FORMAT((ISNULL(ROUND(((NULLIF(MTMR002030, 0) - NULLIF(MTMR002033, 0)) / NULLIF(MTMR002030, 0) * 100), 2), 0.0)) - MTMR002029, 'N1') AS nvarchar)  <> '0.0'" _
                        + "     THEN CAST(FORMAT((ISNULL(ROUND(((NULLIF(MTMR002030, 0) - NULLIF(MTMR002033, 0)) / NULLIF(MTMR002030, 0) * 100), 2), 0.0)) - MTMR002029, 'N1') AS nvarchar)" _
                        + "     ELSE '0.0'" _
                        + "    END" _
                        + "  END AS MTMR002036UP" _
                    '+ ", (ISNULL(FORMAT(ROUND(((NULLIF(MTMR002030, 0) - NULLIF(MTMR002033, 0)) / NULLIF(MTMR002030, 0) * 100), 2), 'N0'), 0) - MTMR002029) AS MTMR002036UP" _ '新粗利率UP
                    command.CommandText += ", FORMAT(ISNULL(MTMR002037,0), 'N1') AS MTMR002037" _ '値上率
                        + ", RTRIM(MTMR002038) AS MTMR002038" _ '仕入先コメント
                        + ", RTRIM(MTMR002045) AS MTMR002045" _ '社内摘要
                        + ", RTRIM(MTMR002046) AS MTMR002046" _ '発注摘要
                        + ", RTRIM(MTMR002047) AS MTMR002047" _ '運賃摘要
                        + ", CASE WHEN CONVERT(VARCHAR, MTMR002049) <> '' THEN SUBSTRING(CONVERT(VARCHAR, MTMR002049), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002049), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002049), 7, 2) ELSE '' END AS MTMR002049" _ '最終売上日
                        + ", FORMAT(ISNULL(MTMR002050,0), 'N2') AS MTMR002050" _ '最終売上単価
                        + ", FORMAT(ISNULL(MTMR002051,0), 'N0') AS MTMR002051" _ '最終売上数量
                        + ", RTRIM(MTMR002052) AS MTMR002052" _ '最終納品先
                        + ", CASE WHEN MTMR002084 = 1 THEN 1 ELSE 0 END AS MTMR002084" _ '納品先履歴カットフラグ
                        + ", RTRIM(MTMR002053) AS MTMR002053" _ '納品先履歴
                        + ", FORMAT(ISNULL(MTMR002039,0), 'N0') AS MTMR002039" _ '数量
                        + ", FORMAT(ISNULL(MTMR002040,0), 'N0') AS MTMR002040" _ '売上回数
                        + ", FORMAT(ISNULL(MTMR002041,0), 'N0') AS MTMR002041" _ '売上金額
                        + ", FORMAT(ISNULL(MTMR002042,0), 'N0') AS MTMR002042" _ '粗利金額
                        + ", FORMAT(ISNULL(MTMR002043,0), 'N0') AS MTMR002043" _ '改定後売上金額
                        + ", FORMAT(ISNULL(MTMR002044,0), 'N0') AS MTMR002044" _ '改定後粗利金額
                        + ", RTRIM(MTMR002021) AS MTMR002021" _ '手配
                        + ", RTRIM(MTMR002048) AS MTMR002048" _ '見積書商品名
                        + ", MTMR002054" _ '得意先FAX
                        + ", RTRIM(MTMR002072) AS MTMR002072" _ '備考
                        + ", CASE WHEN MTMR002076 = 1 THEN 1 ELSE 0 END AS MTMR002076MAIL" _ 'メール
                        + ", CASE WHEN MTMR002076 = 2 THEN 1 ELSE 0 END AS MTMR002076FAX" _ 'ＦＡＸ
                        + ", RTRIM(MTMR002079) AS MTMR002079" _ '宛先名
                        + ", RTRIM(MTMR002078) AS MTMR002078" _ 'メール宛先
                        + ", RTRIM(MTMR002077) AS MTMR002077" _ 'ＦＡＸ宛先
                        + ", CASE WHEN CONVERT(VARCHAR, MTMR002087) <> '' THEN SUBSTRING(CONVERT(VARCHAR, MTMR002087), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002087), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, MTMR002087), 7, 2) ELSE '' END AS MTMR002087" _ '最終送信日
                        + ", MTMR002001" _ '得意先コード(非表示)
                        + ", MTMR002080" _ '価格入力番号(非表示)
                        + " FROM MTM10R002KAKAKU" _
                        + " LEFT JOIN HAN10M001TOKUI AS TOKUI" _
                        + " ON RIGHT('00000000000' + CONVERT(NVARCHAR, RTRIM(MTMR002001)), 11) = RIGHT('00000000000' + CONVERT(NVARCHAR, RTRIM(TOKUI.HANM001003)), 11)" _
                        + " WHERE MTMR002080 = @MTMR002080"
                    If Not String.IsNullOrWhiteSpace(searchCondition.LoginId) Then
                        command.CommandText += " AND MTMR002074 = @MTMR002074"          'ログインID
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCode) Then
                        command.CommandText += " AND MTMR002008 = @MTMR002008"          '営業所コード
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeFrom) Then
                        command.CommandText += " AND MTMR002010 >= @MTMR002010_From"    '部課コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeTo) Then
                        command.CommandText += " AND MTMR002010 <= @MTMR002010_To"      '部課コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.CommandText += " AND MTMR002012 >= @MTMR002012_From"    '担当者コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.CommandText += " AND MTMR002012 <= @MTMR002012_To"      '担当者コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeFrom) Then
                        command.CommandText += " AND MTMR002001 >= @MTMR002001_From"    '得意先コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeTo) Then
                        command.CommandText += " AND MTMR002001 <= @MTMR002001_To"      '得意先コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.ShohinCodeFrom) Then
                        command.CommandText += " AND MTMR002002 >= @MTMR002002_From"    '商品コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.ShohinCodeTo) Then
                        command.CommandText += " AND MTMR002002 <= @MTMR002002_To"      '商品コード(To)
                    End If
                    If IsNumeric(searchCondition.ShinArariFrom) And (searchCondition.ShinArariFrom_bool = True) Then
                        command.CommandText += " AND MTMR002036 >= @MTMR002036_From"    '新粗利率(From)
                    End If
                    If IsNumeric(searchCondition.ShinArariTo) And (searchCondition.ShinArariTo_bool = True) Then
                        command.CommandText += " AND MTMR002036 <= @MTMR002036_To"      '新粗利率(To)
                    End If
                    If searchCondition.Mikakutei Then
                        command.CommandText += " AND ISNULL(MTMR002085, 0) = 0"
                    End If
                    If searchCondition.Kakutei Then
                        command.CommandText += " AND ISNULL(MTMR002085, 0) <> 0"
                    End If
                    If searchCondition.EigyosyoCodeList.Count > 0 Then
                        command.CommandText += " AND MTMR002008 IN ("
                        For Each eigyoCode In searchCondition.EigyosyoCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(eigyoCode.index = 0, "@MTMR002008_", ", @MTMR002008_") + (eigyoCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    If searchCondition.BukaCodeList.Count > 0 Then
                        command.CommandText += " AND MTMR002010 IN ("
                        For Each bukaCode In searchCondition.BukaCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(bukaCode.index = 0, "@MTMR002010_", ", @MTMR002010_") + (bukaCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    If searchCondition.TantousyaCodeList.Count > 0 Then
                        command.CommandText += " AND MTMR002012 IN ("
                        For Each tantousyaCode In searchCondition.TantousyaCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(tantousyaCode.index = 0, "@MTMR002012_", ", @MTMR002012_") + (tantousyaCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    If searchCondition.TokuisakiCodeList.Count > 0 Then
                        command.CommandText += " AND MTMR002001 IN ("
                        For Each tokuisakiCode In searchCondition.TokuisakiCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(tokuisakiCode.index = 0, "@MTMR002001_", ", @MTMR002001_") + (tokuisakiCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    command.CommandText += " ORDER BY MTMR002008, MTMR002010, MTMR002012, MTMR002001, MTMR002002, MTMR002003, MTMR002004"

                    command.Parameters.Add(New SqlParameter("@MTMR002080", searchCondition.KakakuNyuuryokuNo))
                    If Not String.IsNullOrWhiteSpace(searchCondition.LoginId) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002074", searchCondition.LoginId))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCode) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002008", searchCondition.EigyosyoCode))
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
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002001_From", searchCondition.TokuisakiCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002001_To", searchCondition.TokuisakiCodeTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.ShohinCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002002_From", searchCondition.ShohinCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.ShohinCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002002_To", searchCondition.ShohinCodeTo))
                    End If
                    If IsNumeric(searchCondition.ShinArariFrom) And (searchCondition.ShinArariFrom_bool = True) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002036_From", searchCondition.ShinArariFrom))
                    End If
                    If IsNumeric(searchCondition.ShinArariTo) And (searchCondition.ShinArariTo_bool = True) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002036_To", searchCondition.ShinArariTo))
                    End If
                    If searchCondition.EigyosyoCodeList.Count > 0 Then
                        For Each eigyoCode In searchCondition.EigyosyoCodeList.Select(Function(value, index) New With {value, index})
                            Dim parameterName As String = "@MTMR002008_" + (eigyoCode.index + 1).ToString()
                            command.Parameters.Add(New SqlParameter(parameterName, eigyoCode.value))
                        Next
                    End If
                    If searchCondition.BukaCodeList.Count > 0 Then
                        For Each bukaCode In searchCondition.BukaCodeList.Select(Function(value, index) New With {value, index})
                            Dim parameterName As String = "@MTMR002010_" + (bukaCode.index + 1).ToString()
                            command.Parameters.Add(New SqlParameter(parameterName, bukaCode.value))
                        Next
                    End If
                    If searchCondition.TantousyaCodeList.Count > 0 Then
                        For Each tantousyaCode In searchCondition.TantousyaCodeList.Select(Function(value, index) New With {value, index})
                            Dim parameterName As String = "@MTMR002012_" + (tantousyaCode.index + 1).ToString()
                            command.Parameters.Add(New SqlParameter(parameterName, tantousyaCode.value))
                        Next
                    End If
                    If searchCondition.TokuisakiCodeList.Count > 0 Then
                        For Each tokuisakiCode In searchCondition.TokuisakiCodeList.Select(Function(value, index) New With {value, index})
                            Dim parameterName As String = "@MTMR002001_" + (tokuisakiCode.index + 1).ToString()
                            command.Parameters.Add(New SqlParameter(parameterName, tokuisakiCode.value))
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
                            command.Parameters.Add(New SqlParameter("@MTMR002085", If(row.Item("MTMR002085").ToString() = "1", Integer.Parse(Date.Now.ToString("yyyyMMdd")), If(row.Item("MTMR002085").ToString <> "", row.Item("MTMR002085"), 0))))
                            command.Parameters.Add(New SqlParameter("@MTMR002086", If(row.Item("MTMR002086").ToString() = "1", 1, 0)))
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
                            command.Parameters.Add(New SqlParameter("@MTMR002076", If(row.Item("MTMR002076MAIL").ToString() = "1", 1, If(row.Item("MTMR002076FAX").ToString() = "1", 2, SqlTypes.SqlDecimal.Null))))
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

    Public Class MTM02SearchCondition
        ''' <summary>
        ''' 価格入力番号
        ''' </summary>
        ''' <returns></returns>
        Public Property KakakuNyuuryokuNo As String = ""
        ''' <summary>
        ''' ログインID
        ''' </summary>
        ''' <returns></returns>
        Public Property LoginId As String = ""
        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property EigyosyoCode As String = ""
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
        ''' <summary>
        ''' 得意先コード(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property TokuisakiCodeFrom As String = ""
        ''' <summary>
        ''' 得意先コード(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property TokuisakiCodeTo As String = ""
        ''' <summary>
        ''' 商品コード(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property ShohinCodeFrom As String = ""
        ''' <summary>
        ''' 商品コード(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property ShohinCodeTo As String = ""
        ''' <summary>
        ''' 新粗利率(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property ShinArariFrom As Decimal
        ''' <summary>
        ''' 新粗利率(From_input_boolean)
        ''' </summary>
        ''' <returns></returns>
        Public Property ShinArariFrom_bool As Boolean = False
        ''' <summary>
        ''' 新粗利率(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property ShinArariTo As Decimal
        ''' <summary>
        ''' 新粗利率(To_input_boolean)
        ''' </summary>
        ''' <returns></returns>
        Public Property ShinArariTo_bool As Boolean = False
        ''' <summary>
        ''' 確定の指定(未確定のみ出力)
        ''' </summary>
        ''' <returns></returns>
        Public Property Mikakutei As Boolean = False
        ''' <summary>
        ''' 確定の指定(確定のみ出力)
        ''' </summary>
        ''' <returns></returns>
        Public Property Kakutei As Boolean = False
        ''' <summary>
        ''' 確定の指定(全件出力)
        ''' </summary>
        ''' <returns></returns>
        Public Property Zenken As Boolean = False
        ''' <summary>
        ''' 営業所コードリスト
        ''' </summary>
        ''' <returns></returns>
        Public Property EigyosyoCodeList As New List(Of String)
        ''' <summary>
        ''' 部課コードリスト
        ''' </summary>
        ''' <returns></returns>
        Public Property BukaCodeList As New List(Of String)
        ''' <summary>
        ''' 担当者コードリスト
        ''' </summary>
        ''' <returns></returns>
        Public Property TantousyaCodeList As New List(Of String)
        ''' <summary>
        ''' 得意先コードリスト
        ''' </summary>
        ''' <returns></returns>
        Public Property TokuisakiCodeList As New List(Of String)

        Public Sub Clear()
            Me.KakakuNyuuryokuNo = ""
            Me.LoginId = ""
            Me.EigyosyoCode = ""
            Me.BukaCodeFrom = ""
            Me.BukaCodeTo = ""
            Me.TantousyaCodeFrom = ""
            Me.TantousyaCodeTo = ""
            Me.TokuisakiCodeFrom = ""
            Me.TokuisakiCodeTo = ""
            Me.ShohinCodeFrom = ""
            Me.ShohinCodeTo = ""
            Me.ShinArariFrom = Nothing
            Me.ShinArariFrom_bool = False
            Me.ShinArariTo = Nothing
            Me.ShinArariTo_bool = False
            Me.Mikakutei = False
            Me.Kakutei = False
            Me.Zenken = False
            Me.EigyosyoCodeList.Clear()
            Me.BukaCodeList.Clear()
            Me.TantousyaCodeList.Clear()
            Me.TokuisakiCodeList.Clear()
        End Sub
    End Class
End Namespace