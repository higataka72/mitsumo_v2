Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Net.Mail
Imports System.Text
Imports System.Net.Mime

Namespace Biz
    Public Class MTM03
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
        Public Function ValidateBeforeSearch(ByVal searchCondition As MTM03SearchCondition) As List(Of String)
            Dim errorList As New List(Of String)

            If String.IsNullOrWhiteSpace(searchCondition.KakakuNyuuryokuNo) Then
                errorList.Add("価格入力番号が空です")
            End If

            Return errorList
        End Function

        Public Function PreviewPdf(ByVal searchCondition As MTM03SearchCondition, ByVal outputPath As String) As MTM03PreviewData
            Dim previewData As New MTM03PreviewData

            previewData.SearchResult = Me.GetKakaku(searchCondition)

            If previewData.SearchResult.ElementList.Count > 0 Then
                Dim createPdf As String = ""
                Dim errorList = Me.OutputPreviewPdf(searchCondition.KakakuNyuuryokuNo, previewData.SearchResult, outputPath, createPdf)
                If errorList.Count > 0 Then
                    previewData.ErrorList.AddRange(errorList)
                Else
                    'previewData.FilePath = outputPath + "/見積書.pdf"
                    previewData.FilePath = createPdf
                End If
            Else
                previewData.ErrorList.Add("見積データがありません")
            End If

            Return previewData
        End Function

        ''' <summary>
        ''' 価格入力データの取得
        ''' </summary>
        ''' <param name="searchCondition"></param>
        ''' <returns></returns>
        Public Function GetKakaku(ByVal searchCondition As MTM03SearchCondition) As MTM03SearchResult
            Dim result As New MTM03SearchResult

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection

                    Dim kaisyaName As String = ""
                    Dim toAddress As String = ""
                    Dim ccAddress As String = ""
                    Dim bccAddress As String = ""
                    Dim fromAddress As String = ""
                    Dim serviceId As String = ""
                    Dim password As String = ""
                    Dim noticeClass As String = ""
                    Dim faxMode As String = ""

                    command.CommandText = "SELECT MTMM001005" _ '会社名
                        + ", MTMM001019" _                      'TOアドレス
                        + ", MTMM001020" _                      'CCアドレス
                        + ", MTMM001021" _                      'BCCアドレス
                        + ", MTMM001022" _                      'FROMアドレス
                        + ", MTMM001015" _                      'サービスID
                        + ", MTMM001016" _                      'パスワード
                        + ", MTMM001017" _                      '通知種別
                        + ", MTMM001018" _                      '画素密度
                        + " FROM MTM00M001SEIGYO"

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read Then
                        kaisyaName = reader.Item("MTMM001005").ToString
                        toAddress = reader.Item("MTMM001019").ToString
                        ccAddress = reader.Item("MTMM001020").ToString
                        bccAddress = reader.Item("MTMM001021").ToString
                        fromAddress = reader.Item("MTMM001022").ToString
                        serviceId = reader.Item("MTMM001015").ToString
                        password = reader.Item("MTMM001016").ToString
                        noticeClass = reader.Item("MTMM001017").ToString
                        faxMode = reader.Item("MTMM001018").ToString
                    End If
                    reader.Close()

                    command.CommandText = "SELECT KAKAKU.MTMR002001" _ '価格入力データ.得意先コード
                        + ", KAKAKU.MTMR002079" _   '価格入力データ.宛先名
                        + ", TOKUI.HANM001004" _    '得意先マスタ.得意先名１
                        + ", TOKUI.HANM001005" _    '得意先マスタ.得意先名２
                        + ", TOKUI.HANM001013" _    '得意先マスタ.FAX番号
                        + ", KAKAKU.MTMR002080" _   '価格入力データ.価格入力番号
                        + ", KAKAKU.MTMR002081" _   '価格入力データ.価格入力名
                        + ", CASE WHEN CONVERT(VARCHAR, KAKAKU.MTMR002032) <> '' THEN SUBSTRING(CONVERT(VARCHAR, KAKAKU.MTMR002032), 1, 4) + '年' + SUBSTRING(CONVERT(VARCHAR, KAKAKU.MTMR002032), 5, 2) + '月' + SUBSTRING(CONVERT(VARCHAR, KAKAKU.MTMR002032), 7, 2) + '日' ELSE '' END AS MTMR002032" _ '価格入力データ.売実施日
                        + ", JITSUKOU.MTMR003002" _ '実行管理テーブル.価格入力名
                        + ", JITSUKOU.MTMR003008" _ '実行管理テーブル.運賃の指定
                        + ", JITSUKOU.MTMR003021" _ '実行管理テーブル.改定実施日
                        + ", TANTO.HANM004K013" _    '担当者マスタ.担当者名
                        + ", KAKAKU.MTMR002008" _   '価格入力データ.営業所コード
                        + ", BUMON.HANM015K012" _   '部門マスタ.営業所名
                        + ", KAKAKU.MTMR002010" _   '価格入力データ.部課コード
                        + ", BUMON.HANM015K013" _   '部門マスタ.郵便番号
                        + ", LTRIM(RTRIM(BUMON.HANM015K014)) AS HANM015K014" _   '部門マスタ.住所
                        + ", BUMON.HANM015K015" _   '部門マスタ.電話番号/FAX番号
                        + ", KAKAKU.MTMR002048" _   '価格入力データ.見積商品名
                        + ", KAKAKU.MTMR002019" _   '価格入力データ.入数
                        + ", KAKAKU.MTMR002020" _   '価格入力データ.単位
                        + ", KAKAKU.MTMR002017" _   '価格入力データ.ロット
                        + ", KAKAKU.MTMR002030" _   '価格入力データ.新売上単価
                        + ", KAKAKU.MTMR002025" _   '価格入力データ.現売上単価
                        + ", KAKAKU.MTMR002025" _   '価格入力データ.現売上単価
                        + ", CASE WHEN CONVERT(VARCHAR, KAKAKU.MTMR002049) <> '' THEN SUBSTRING(CONVERT(VARCHAR, KAKAKU.MTMR002049), 1, 4) + '/' + SUBSTRING(CONVERT(VARCHAR, KAKAKU.MTMR002049), 5, 2) + '/' + SUBSTRING(CONVERT(VARCHAR, KAKAKU.MTMR002049), 7, 2) ELSE '' END AS MTMR002049" _ '価格入力データ.最終売上日
                        + ", KAKAKU.MTMR002072 " _ '価格入力データ.備考（見積書）
                        + ", KAKAKU.MTMR002053 " _ '価格入力データ.納品先履歴
                        + ", KAKAKU.MTMR002076" _   '価格入力データ.宛先送信区分
                        + ", KAKAKU.MTMR002078" _   '価格入力データ.メールアドレス
                        + ", TANTO.HANM004K001" _   '担当者マスタ.メールアドレス
                        + ", KAKAKU.MTMR002077" _   '価格入力データ.FAX番号
                        + ", KAKAKU.MTMR002074" _   '価格入力データ.ログインID
                        + ", KAKAKU.MTMR002075" _   '価格入力データ.もりや担当者メールアドレス
                        + ", KAKAKU.MTMR002002" _   '価格入力データ.商品コード
                        + ", KAKAKU.MTMR002003" _   '価格入力データ.規格
                        + " FROM MTM10R002KAKAKU AS KAKAKU" _
                        + " LEFT JOIN HAN10M001TOKUI AS TOKUI" _
                        + " ON RIGHT('00000000000' + CONVERT(NVARCHAR, RTRIM(KAKAKU.MTMR002001)), 11) = RIGHT('00000000000' + CONVERT(NVARCHAR, RTRIM(TOKUI.HANM001003)), 11)" _
                        + " LEFT JOIN MTM10R003JITSUKOU AS JITSUKOU ON LTRIM(RTRIM(KAKAKU.MTMR002080)) = LTRIM(RTRIM(JITSUKOU.MTMR003001))" _
                        + " LEFT JOIN HAN10M004TANTO AS TANTO" _
                        + " ON RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(KAKAKU.MTMR002012)), 8) = RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(TANTO.HANM004001)), 8)" _
                        + " LEFT JOIN HAN10M036EIGYOU AS EIGYOU" _
                        + " ON KAKAKU.MTMR002008 = EIGYOU.HANM036001" _
                        + " LEFT JOIN HAN10M015BUMON AS BUMON" _
                        + " ON KAKAKU.MTMR002010 = BUMON.HANM015001" _
                        + " WHERE KAKAKU.MTMR002080 = @MTMR002080" _                           '価格入力番号
                        + " AND ISNULL(KAKAKU.MTMR002085, 0) <> 0" _                           '確定済
                        + " AND ISNULL(KAKAKU.MTMR002032, 0) <> 0" _                           '売更新日あり
                        + " AND ISNULL(KAKAKU.MTMR002086, 0) = 0"                              '印刷する
                    If (searchCondition.SendNum = 1) Then
                        command.CommandText += " AND LTRIM(RTRIM(ISNULL(KAKAKU.MTMR002087, ''))) = ''"   '送信区分の指定
                    ElseIf (searchCondition.SendNum = 2) Then
                        command.CommandText += " AND LTRIM(RTRIM(ISNULL(KAKAKU.MTMR002087, ''))) <> ''"  '送信区分の指定
                    End If
                    If (searchCondition.OutputNum = 1) Then
                        command.CommandText += " AND MTMR002076 = 1"          '出力対象の指定
                    ElseIf (searchCondition.OutputNum = 2) Then
                        command.CommandText += " AND MTMR002076 = 2"          '出力対象の指定
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.LoginId) Then
                        command.CommandText += " AND MTMR002074 = @MTMR002074"          'ログインID
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeFrom) Then
                        command.CommandText += " AND KAKAKU.MTMR002008 >= @MTMR002008_From"    '営業所コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCodeTo) Then
                        command.CommandText += " AND KAKAKU.MTMR002008 <= @MTMR002008_To"      '営業所コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeFrom) Then
                        command.CommandText += " AND KAKAKU.MTMR002010 >= @MTMR002010_From"    '部課コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.BukaCodeTo) Then
                        command.CommandText += " AND KAKAKU.MTMR002010 <= @MTMR002010_To"      '部課コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.CommandText += " AND KAKAKU.MTMR002012 >= @MTMR002012_From"    '担当者コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.CommandText += " AND KAKAKU.MTMR002012 <= @MTMR002012_To"      '担当者コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeFrom) Then
                        command.CommandText += " AND KAKAKU.MTMR002001 >= @MTMR002001_From"    '得意先コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeTo) Then
                        command.CommandText += " AND KAKAKU.MTMR002001 <= @MTMR002001_To"      '得意先コード(To)
                    End If
                    If searchCondition.EigyosyoCodeList.Count > 0 Then
                        command.CommandText += " AND KAKAKU.MTMR002008 IN ("
                        For Each eigyoCode In searchCondition.EigyosyoCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(eigyoCode.index = 0, "@MTMR002008_", ", @MTMR002008_") + (eigyoCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    If searchCondition.BukaCodeList.Count > 0 Then
                        command.CommandText += " AND KAKAKU.MTMR002010 IN ("
                        For Each bukaCode In searchCondition.BukaCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(bukaCode.index = 0, "@MTMR002010_", ", @MTMR002010_") + (bukaCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    If searchCondition.TantousyaCodeList.Count > 0 Then
                        command.CommandText += " AND KAKAKU.MTMR002012 IN ("
                        For Each tantousyaCode In searchCondition.TantousyaCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(tantousyaCode.index = 0, "@MTMR002012_", ", @MTMR002012_") + (tantousyaCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    If searchCondition.TokuisakiCodeList.Count > 0 Then
                        command.CommandText += " AND KAKAKU.MTMR002001 IN ("
                        For Each tokuisakiCode In searchCondition.TokuisakiCodeList.Select(Function(value, index) New With {value, index})
                            command.CommandText += If(tokuisakiCode.index = 0, "@MTMR002001_", ", @MTMR002001_") + (tokuisakiCode.index + 1).ToString
                        Next
                        command.CommandText += ")"
                    End If
                    command.CommandText += " ORDER BY KAKAKU.MTMR002008, KAKAKU.MTMR002010, KAKAKU.MTMR002012, KAKAKU.MTMR002001, KAKAKU.MTMR002002, KAKAKU.MTMR002003, KAKAKU.MTMR002004"     '得意先コード、商品コード, 規格, 数量・上限の順

                    command.Parameters.Add(New SqlParameter("@MTMR002080", searchCondition.KakakuNyuuryokuNo))
                    If Not String.IsNullOrWhiteSpace(searchCondition.LoginId) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002074", searchCondition.LoginId))
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
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002001_From", searchCondition.TokuisakiCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002001_To", searchCondition.TokuisakiCodeTo))
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

                    reader = command.ExecuteReader
                    Dim oldTokuisakiCode As String = ""
                    Dim boolDetail As Boolean = False
                    Dim decNewdetail As Decimal = 0
                    Dim decNewTanka As Decimal = 0.00
                    Dim decOldTanka As Decimal = 0.00
                    Dim resultElement As MTM03SearchResultElement = Nothing

                    While (reader.Read)
                        Dim tokuisakiCode As String = reader.Item("MTMR002001").ToString

                        If tokuisakiCode <> oldTokuisakiCode Then
                            resultElement = New MTM03SearchResultElement
                            result.ElementList.Add(resultElement)
                            resultElement.AtesakiName = reader.Item("MTMR002079").ToString
                            resultElement.AtesakiFaxNo = reader.Item("MTMR002077").ToString
                            resultElement.TokuisakiCode = reader.Item("MTMR002001").ToString
                            resultElement.TokuisakiFaxNo = reader.Item("HANM001013").ToString
                            resultElement.TokuisakiName1 = reader.Item("HANM001004").ToString
                            resultElement.TokuisakiName2 = reader.Item("HANM001005").ToString
                            resultElement.KaisyaName = kaisyaName
                            resultElement.EigyosyoCode = reader.Item("MTMR002008").ToString
                            resultElement.EigyosyoName = reader.Item("HANM015K012").ToString
                            resultElement.BukaCode = reader.Item("MTMR002010").ToString
                            resultElement.KakakuNyuuryokuNo = reader.Item("MTMR002080").ToString
                            resultElement.KakakuNyuuryokuName = reader.Item("MTMR002081").ToString
                            resultElement.TantoName = reader.Item("HANM004K013").ToString
                            resultElement.PostCode = reader.Item("HANM015K013").ToString
                            resultElement.NeageDate = reader.Item("MTMR002032").ToString
                            resultElement.Address = reader.Item("HANM015K014").ToString
                            resultElement.JitsukouKakakuNyuuryokuName = reader.Item("MTMR003002").ToString
                            resultElement.Unchin = reader.Item("MTMR003008").ToString
                            resultElement.Kaiteijitsusi = reader.Item("MTMR003021").ToString
                            resultElement.PhoneFax = reader.Item("HANM015K015").ToString
                            resultElement.SoushinKubun = reader.Item("MTMR002076").ToString
                            resultElement.MailTo = reader.Item("MTMR002078").ToString
                            resultElement.MailFrom = reader.Item("MTMR002075").ToString
                            resultElement.EDocTo = toAddress
                            resultElement.EDocCc = ccAddress
                            resultElement.EDocBCc = bccAddress
                            resultElement.EDocFrom = fromAddress
                            resultElement.LoginId = reader.Item("MTMR002074").ToString
                            resultElement.EDocServiceId = serviceId
                            resultElement.EDocPassword = password
                            resultElement.EDocNoticeClass = noticeClass
                            resultElement.EDocFaxMode = faxMode
                            '20240628 e帳票の通知先指定を設定（もりや担当者メールアドレス）
                            resultElement.NoticeAddress = reader.Item("MTMR002075").ToString

                            Dim detail As New MTM03SearchResultElementDetail
                            detail.SyohinName = reader.Item("MTMR002048").ToString
                            If (reader.Item("MTMR002019").ToString = "0.0000") Then
                                detail.Irisu = ""
                            Else
                                If (Decimal.TryParse(reader.Item("MTMR002019").ToString, decNewdetail)) Then
                                    detail.Irisu = Format(decNewdetail, "#,0")
                                Else
                                    detail.Irisu = reader.Item("MTMR002019").ToString
                                End If
                            End If
                            detail.Tanni = reader.Item("MTMR002020").ToString
                            detail.Lot = reader.Item("MTMR002017").ToString
                            If (Decimal.TryParse(reader.Item("MTMR002030").ToString, decNewTanka)) Then
                                detail.NewTanka = Format(decNewTanka, "#,0.00")
                            Else
                                detail.NewTanka = reader.Item("MTMR002030").ToString
                            End If
                            If (Decimal.TryParse(reader.Item("MTMR002025").ToString, decOldTanka)) Then
                                detail.OldTanka = Format(decOldTanka, "#,0.00")
                            Else
                                detail.OldTanka = reader.Item("MTMR002025").ToString
                            End If
                            detail.SaisyuDate = reader.Item("MTMR002049").ToString
                            detail.Bikou = reader.Item("MTMR002072").ToString
                            detail.NohinHistory = reader.Item("MTMR002053").ToString
                            detail.SyohinCode = reader.Item("MTMR002002").ToString
                            detail.Kikaku = reader.Item("MTMR002003").ToString
                            resultElement.ElementDetailList.Add(detail)
                        Else
                            Dim detail As New MTM03SearchResultElementDetail
                            detail.SyohinName = reader.Item("MTMR002048").ToString
                            If (reader.Item("MTMR002019").ToString = "0.0000") Then
                                detail.Irisu = ""
                            Else
                                If (Decimal.TryParse(reader.Item("MTMR002019").ToString, decNewdetail)) Then
                                    detail.Irisu = Format(decNewdetail, "#,0")
                                Else
                                    detail.Irisu = reader.Item("MTMR002019").ToString
                                End If
                            End If
                            detail.Tanni = reader.Item("MTMR002020").ToString
                            detail.Lot = reader.Item("MTMR002017").ToString
                            If (Decimal.TryParse(reader.Item("MTMR002030").ToString, decNewTanka)) Then
                                detail.NewTanka = Format(decNewTanka, "#,0.00")
                            Else
                                detail.NewTanka = reader.Item("MTMR002030").ToString
                            End If
                            If (Decimal.TryParse(reader.Item("MTMR002025").ToString, decOldTanka)) Then
                                detail.OldTanka = Format(decOldTanka, "#,0.00")
                            Else
                                detail.OldTanka = reader.Item("MTMR002025").ToString
                            End If
                            detail.SaisyuDate = reader.Item("MTMR002049").ToString
                            detail.Bikou = reader.Item("MTMR002072").ToString
                            detail.NohinHistory = reader.Item("MTMR002053").ToString
                            detail.SyohinCode = reader.Item("MTMR002002").ToString
                            detail.Kikaku = reader.Item("MTMR002003").ToString
                            resultElement.ElementDetailList.Add(detail)
                        End If

                        'If Not IsNothing(resultElement) Then
                        '    resultElement.AtesakiName = reader.Item("MTMR002073").ToString
                        '    resultElement.AtesakiFaxNo = reader.Item("MTMR002077").ToString
                        '    resultElement.TokuisakiCode = reader.Item("MTMR002001").ToString
                        '    resultElement.TokuisakiFaxNo = reader.Item("HANM001013").ToString
                        '    resultElement.TokuisakiName1 = reader.Item("HANM001004").ToString
                        '    resultElement.TokuisakiName2 = reader.Item("HANM001005").ToString
                        '    resultElement.KaisyaName = kaisyaName
                        '    resultElement.EigyosyoCode = reader.Item("MTMR002008").ToString
                        '    resultElement.EigyosyoName = reader.Item("HANM036002").ToString
                        '    resultElement.BukaCode = reader.Item("MTMR002010").ToString
                        '    resultElement.KakakuNyuuryokuNo = reader.Item("MTMR002080").ToString
                        '    resultElement.KakakuNyuuryokuName = reader.Item("MTMR002081").ToString
                        '    resultElement.TantoName = reader.Item("HANM004002").ToString
                        '    resultElement.PostCode = reader.Item("HANM015K013").ToString
                        '    resultElement.NeageDate = reader.Item("MTMR002032").ToString
                        '    resultElement.Address = reader.Item("HANM015K014").ToString
                        '    resultElement.Unchin = reader.Item("MTMR003008").ToString
                        '    resultElement.PhoneFax = reader.Item("HANM015K015").ToString
                        '    resultElement.SoushinKubun = reader.Item("MTMR002076").ToString
                        '    resultElement.MailTo = reader.Item("MTMR002078").ToString
                        '    resultElement.MailFrom = reader.Item("HANM004K001").ToString
                        '    resultElement.EDocTo = toAddress
                        '    resultElement.EDocCc = ccAddress
                        '    resultElement.EDocBCc = bccAddress
                        '    resultElement.EDocFrom = fromAddress
                        '    resultElement.LoginId = reader.Item("MTMR002074").ToString
                        '    resultElement.EDocServiceId = serviceId
                        '    resultElement.EDocPassword = password
                        '    resultElement.EDocNoticeClass = noticeClass
                        '    resultElement.EDocFaxMode = faxMode
                        'End If
                        oldTokuisakiCode = tokuisakiCode
                    End While
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return result
        End Function


        ''' <summary>
        ''' プレビュー用PDF出力
        ''' </summary>
        ''' <returns>String</returns>
        Private Function OutputPreviewPdf(ByVal kakakuNyuuryokuNo As String, ByVal result As MTM03SearchResult, ByVal outputPath As String, ByRef createPdf As String) As List(Of String)
            Dim errorList As New List(Of String)

            Try
                If Not System.IO.Directory.Exists(outputPath) Then
                    System.IO.Directory.CreateDirectory(outputPath)
                End If

                Dim annaiFilePath As String = Me.GetAnnaiFilePath(kakakuNyuuryokuNo)

                'ドキュメントを作成
                Dim doc As New Document(PageSize.A4.Rotate)
                createPdf = outputPath + "/プレビュー見積書" + Guid.NewGuid().ToString() + ".pdf"
                Dim stream As New FileStream(createPdf, FileMode.Create)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, stream)

                'ドキュメントを開く
                doc.Open()

                Dim contentByte As PdfContentByte = writer.DirectContent

                If System.IO.File.Exists(annaiFilePath) Then
                    Dim reader As New PdfReader(annaiFilePath)
                    For idx As Integer = 1 To reader.NumberOfPages
                        Dim importedPage As PdfImportedPage = writer.GetImportedPage(reader, idx)
                        doc.SetPageSize(reader.GetPageSize(idx))
                        doc.NewPage()
                        contentByte.AddTemplate(importedPage, 0, 0)
                    Next
                    doc.SetPageSize(PageSize.A4.Rotate)
                    doc.NewPage()
                End If

                For Each resultElement In result.ElementList.Select(Function(Value, Index) New With {Value, Index})
                    If resultElement.Index > 0 Then
                        doc.NewPage()
                    End If

                    SetDocument2(doc, contentByte, resultElement.Value)
                Next

                'ドキュメントを閉じる
                doc.Close()
            Catch ex As Exception
                Console.Write(ex.Message)
                errorList.Add(ex.Message)
            End Try

            Return errorList
        End Function

        Private Sub SetDocument(ByRef doc As Document, ByRef contentByte As PdfContentByte, ByVal resultElement As MTM03SearchResultElement)
            Dim firstLineLimit As Integer = 6   '最初のページの行数
            Dim lineLimit As Integer = 15       '2ページ以降の行数

            SetDocumentHeader(doc, resultElement)

            Dim totalPageCount As Integer = 1                                     '総ページ数
            Dim totalLineCount As Integer = resultElement.ElementDetailList.Count '総行数

            If totalLineCount > firstLineLimit Then
                Dim restTotalLineCount As Integer = totalLineCount - firstLineLimit
                totalPageCount += Math.Ceiling((restTotalLineCount / lineLimit))
            End If

            Dim lineCounter As Integer = 0
            Dim rangeIndex As Integer = 0       '範囲指定インデックス
            Dim rangeCount As Integer = 0       '範囲指定個数
            For iPage As Integer = 1 To totalPageCount
                If iPage = 1 Then
                    If totalLineCount < firstLineLimit Then
                        rangeCount = totalLineCount
                    Else
                        rangeCount = firstLineLimit
                    End If
                Else
                    rangeIndex = firstLineLimit + (iPage - 2) * lineLimit
                    Dim modLineCount As Integer = (totalLineCount - firstLineLimit) Mod lineLimit

                    'ページが残っている場合
                    If totalPageCount > firstLineLimit + (iPage - 2) * lineLimit Then
                        rangeCount = lineLimit
                    Else
                        If modLineCount <> 0 Then
                            rangeCount = modLineCount
                        End If
                    End If

                    doc.NewPage()
                End If

                Dim detailList As List(Of MTM03SearchResultElementDetail) = resultElement.ElementDetailList.GetRange(rangeIndex, rangeCount)

                SetDocumentDetail2(doc, detailList, lineCounter, False, False, "")

                SetDocumentFooter(doc, contentByte, iPage, totalPageCount)
            Next
        End Sub
        Private Sub SetDocument2(ByRef doc As Document, ByRef contentByte As PdfContentByte, ByVal resultElement As MTM03SearchResultElement)
            Dim firstLineLimit As Integer = 8   '最初のページの行数
            Dim lineLimit As Integer = 16        '2ページ以降の行数

            SetDocumentHeader(doc, resultElement)

            Dim countlList As List(Of MTM03SearchResultElementDetail) = resultElement.ElementDetailList
            Dim oldSyohinCode As String = ""
            Dim oldKikaku As String = ""
            Dim LineCount As Integer = 0
            Dim plusCount As Integer = 0
            Dim pdfResult As New PDFCreateCountResult
            Dim pdfResultElement As PDFCreateCountResultElement = Nothing
            Dim pageNextLimit As Integer = firstLineLimit
            Dim pageNo As Integer = 1
            Dim topNohinHistory As String = ""

            ' 商品コード＋規格で納品先履歴の行が何行あるかカウント
            For Each detail In countlList
                pdfResultElement = New PDFCreateCountResultElement
                pdfResult.ElementList.Add(pdfResultElement)
                If oldSyohinCode <> "" And ((oldSyohinCode = detail.SyohinCode.Trim) And (oldKikaku = detail.Kikaku.Trim)) Then
                    pdfResultElement.SyohinCode = detail.SyohinCode.Trim
                    pdfResultElement.Kikaku = detail.Kikaku.Trim
                    pdfResultElement.TopNouhinHistory = topNohinHistory
                    LineCount += 1
                    pdfResultElement.LineCount = LineCount
                Else
                    topNohinHistory = ""
                    pdfResultElement.SyohinCode = detail.SyohinCode.Trim
                    pdfResultElement.Kikaku = detail.Kikaku.Trim
                    pdfResultElement.TopNouhinHistory = detail.NohinHistory.Trim
                    topNohinHistory = pdfResultElement.TopNouhinHistory
                    LineCount += 1
                    plusCount += 1
                    pdfResultElement.LineCount = LineCount
                End If

                oldSyohinCode = detail.SyohinCode.Trim
                oldKikaku = detail.Kikaku.Trim

                'ページ切替
                If (pageNextLimit < (LineCount + plusCount)) Then
                    pageNextLimit = pageNextLimit + lineLimit
                    pageNo += 1
                    pdfResultElement.PageMin = 1
                End If
                'ページ番号を付与
                pdfResultElement.PageNo = pageNo
            Next

            Dim totalPageCount As Integer = 1                                     '総ページ数
            Dim totalLineCount As Integer = resultElement.ElementDetailList.Count + plusCount '総行数

            If totalLineCount > firstLineLimit Then
                Dim restTotalLineCount As Integer = totalLineCount - firstLineLimit
                totalPageCount += Math.Ceiling((restTotalLineCount / lineLimit))
            End If

            Dim lineCounter As Integer = 0
            Dim rangeIndex As Integer = 0       '範囲指定インデックス
            Dim rangeCount As Integer = 0       '範囲指定個数
            Dim conectShohin As Boolean = False '商品跨ぎあり(True) / なし(False)
            Dim conectNohinRireki As Boolean = False '納品先履歴あり(True) / なし(False)
            Dim conectTopNohinHistory As String = "" '先頭の納品先履歴の文字列を入れる
            Dim pageElement = New PDFCreateCountResultElement
            For iPage As Integer = 1 To totalPageCount
                conectShohin = False
                conectNohinRireki = False
                If iPage = 1 Then
                    If totalLineCount <= firstLineLimit Then
                        Dim pageCount = pdfResult.ElementList.FindAll(Function(p) p.PageNo = 1).Count
                        rangeCount = pageCount
                    Else
                        '納品先履歴を含めたカウントを計算
                        Dim pageCount = pdfResult.ElementList.FindAll(Function(p) p.PageNo = 1).Count
                        rangeCount = pageCount

                        '次ページ跨ぎがあるなら印を入れる
                        Dim nextLine = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage + 1).Min(Function(p) p.LineCount)
                        Dim nextSyohinCode = pdfResult.ElementList.FindAll(Function(p) p.LineCount = nextLine).Max(Function(p) p.SyohinCode)
                        Dim nextKikaku = pdfResult.ElementList.FindAll(Function(p) p.LineCount = nextLine).Max(Function(p) p.Kikaku)
                        Dim currentLine = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage).Max(Function(p) p.LineCount)
                        Dim currentSyohinCode = pdfResult.ElementList.FindAll(Function(p) p.LineCount = currentLine).Max(Function(p) p.SyohinCode)
                        Dim currentKikaku = pdfResult.ElementList.FindAll(Function(p) p.LineCount = currentLine).Max(Function(p) p.Kikaku)
                        If (nextSyohinCode.Trim = currentSyohinCode.Trim) And (nextKikaku.Trim = currentKikaku.Trim) Then
                            conectNohinRireki = True
                        End If
                    End If
                Else
                    Dim pageLineMin = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage).Min(Function(p) p.LineCount)
                    Dim pageLineMax = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage).Count
                    rangeIndex = pageLineMin - 1
                    'rangeIndex = firstLineLimit + (iPage - 2) * lineLimit
                    rangeCount = pageLineMax

                    '前ページ跨ぎがあるなら印を入れる
                    Dim beforeLine = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage - 1).Max(Function(p) p.LineCount)
                    Dim beforeSyohinCode = pdfResult.ElementList.FindAll(Function(p) p.LineCount = beforeLine).Max(Function(p) p.SyohinCode)
                    Dim beforeKikaku = pdfResult.ElementList.FindAll(Function(p) p.LineCount = beforeLine).Max(Function(p) p.Kikaku)
                    Dim currentLine = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage).Min(Function(p) p.LineCount)
                    Dim currentSyohinCode = pdfResult.ElementList.FindAll(Function(p) p.LineCount = currentLine).Min(Function(p) p.SyohinCode)
                    Dim currentKikaku = pdfResult.ElementList.FindAll(Function(p) p.LineCount = currentLine).Min(Function(p) p.Kikaku)
                    Dim currentNohinHistory = pdfResult.ElementList.FindAll(Function(p) p.LineCount = currentLine).Max(Function(p) p.TopNouhinHistory)
                    If (iPage < totalPageCount) Then
                        '次ページ跨ぎがあるなら印を入れる
                        Dim nextLine = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage + 1).Min(Function(p) p.LineCount)
                        Dim nextSyohinCode = pdfResult.ElementList.FindAll(Function(p) p.LineCount = nextLine).Max(Function(p) p.SyohinCode)
                        Dim nextKikaku = pdfResult.ElementList.FindAll(Function(p) p.LineCount = nextLine).Max(Function(p) p.Kikaku)
                        Dim currentLine2 = pdfResult.ElementList.FindAll(Function(p) p.PageNo = iPage).Max(Function(p) p.LineCount)
                        Dim currentSyohinCode2 = pdfResult.ElementList.FindAll(Function(p) p.LineCount = currentLine2).Min(Function(p) p.SyohinCode)
                        Dim currentKikaku2 = pdfResult.ElementList.FindAll(Function(p) p.LineCount = currentLine2).Min(Function(p) p.Kikaku)
                        If (nextSyohinCode.Trim = currentSyohinCode2.Trim) And (nextKikaku.Trim = currentKikaku2.Trim) Then
                            conectNohinRireki = True
                        End If
                    End If
                    If (beforeSyohinCode.Trim = currentSyohinCode.Trim) And (beforeKikaku.Trim = currentKikaku.Trim) Then
                        conectShohin = True
                        conectTopNohinHistory = currentNohinHistory
                        lineCounter -= 1
                    End If

                    doc.NewPage()
                End If

                Dim detailList As List(Of MTM03SearchResultElementDetail) = resultElement.ElementDetailList.GetRange(rangeIndex, rangeCount)

                SetDocumentDetail2(doc, detailList, lineCounter, conectShohin, conectNohinRireki, conectTopNohinHistory)

                SetDocumentFooter(doc, contentByte, iPage, totalPageCount)


            Next
        End Sub

        ''' <summary>
        ''' PDFヘッダー部の設定
        ''' </summary>
        ''' <param name="doc"></param>
        ''' <param name="resultElement"></param>
        Private Sub SetDocumentHeader(ByRef doc As Document, ByVal resultElement As MTM03SearchResultElement)
            Dim msgothicPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "msgothic.ttc,0")
            Dim bf = BaseFont.CreateFont(msgothicPath, BaseFont.IDENTITY_H, True)

            'タイトル
            Dim titleTable As New PdfPTable(3) With {
                .WidthPercentage = 100,
                .SpacingAfter = 3.0F
                }
            titleTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim titleCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER
                }
            Dim titleChunk As New Chunk("御見積書", New Font(bf, 18))
            titleChunk.SetUnderline(0.1F, -2.0F)
            Dim titlePara As New Paragraph(titleChunk) With {
                .Alignment = Element.ALIGN_CENTER
                }
            titleCell.AddElement(titlePara)
            titleTable.AddCell(titleCell)
            Dim dateCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_BOTTOM
                }
            dateCell.AddElement(New Paragraph(Date.Now.ToString("yyyy/MM/dd"), New Font(bf, 12)) With {.Alignment = Element.ALIGN_RIGHT})
            titleTable.AddCell(dateCell)
            doc.Add(titleTable)

            'ヘッダー部1行目
            Dim headerTable As New PdfPTable(4) With {
                .WidthPercentage = 100
                }
            Dim tokuisakiCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .Colspan = 2
                }

            Dim tokuisakiName As String

            If Not String.IsNullOrEmpty(Trim(resultElement.AtesakiName)) Then
                If resultElement.AtesakiName.LastIndexOf("様") = (resultElement.AtesakiName.Length - 1) Then
                    tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　" + resultElement.AtesakiName
                Else
                    tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　" + resultElement.AtesakiName + "　御中"
                End If
            Else
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　御中"
            End If

            Dim customFontSize As Integer = 18
            Dim tokuisakiName_lenb = StringNumberByteDigitsLenB(tokuisakiName)
            If tokuisakiName_lenb >= 95 Then
                customFontSize = 12
            ElseIf tokuisakiName_lenb >= 73 Then
                customFontSize = 13
            ElseIf tokuisakiName_lenb >= 60 Then
                customFontSize = 14
            ElseIf tokuisakiName_lenb >= 50 Then
                customFontSize = 15
            ElseIf tokuisakiName_lenb >= 35 Then
                customFontSize = 16
            ElseIf tokuisakiName_lenb >= 30 Then
                customFontSize = 17
            ElseIf tokuisakiName_lenb >= 20 Then
                customFontSize = 18
            End If
            'If tokuisakiName_lenb >= 95 Then
            '    customFontSize = 9.5
            'ElseIf tokuisakiName_lenb >= 73 Then
            '    customFontSize = 10
            'ElseIf tokuisakiName_lenb >= 60 Then
            '    customFontSize = 11
            'ElseIf tokuisakiName_lenb >= 50 Then
            '    customFontSize = 13
            'ElseIf tokuisakiName_lenb >= 35 Then
            '    customFontSize = 16
            'ElseIf tokuisakiName_lenb >= 30 Then
            '    customFontSize = 17
            'ElseIf tokuisakiName_lenb >= 20 Then
            '    customFontSize = 18
            'End If

            Dim tokuisakiChunk As New Chunk(tokuisakiName, New Font(bf, customFontSize))
            tokuisakiChunk.SetUnderline(0.1F, -2.0F)
            Dim tokuisakiPara As New Paragraph(tokuisakiChunk) With {
                .Alignment = Element.ALIGN_LEFT
                }
            tokuisakiCell.AddElement(tokuisakiPara)
            headerTable.AddCell(tokuisakiCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER, .Colspan = 2})
            'ヘッダー部2行目
            Dim faxCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            faxCell.AddElement(New Paragraph("FAX：" + resultElement.AtesakiFaxNo, New Font(bf, 11)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(faxCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim kaisyaCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            kaisyaCell.AddElement(New Paragraph(resultElement.KaisyaName, New Font(bf, 11)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(kaisyaCell)
            'ヘッダー部3行目
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER, .Colspan = 3})
            Dim eigyosyoCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            eigyosyoCell.AddElement(New Paragraph(resultElement.EigyosyoName, New Font(bf, 10)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(eigyosyoCell)
            'ヘッダー部4行目
            Dim kenmeiCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            Dim customFontSize1 As Integer = 13
            Dim tokuisakiName_lenb2 = StringNumberByteDigitsLenB(resultElement.JitsukouKakakuNyuuryokuName)

            If tokuisakiName_lenb2 >= 45 Then
                customFontSize1 = 11
            ElseIf tokuisakiName_lenb2 >= 35 Then
                customFontSize1 = 12
            End If
            kenmeiCell.AddElement(New Paragraph("件名：" + resultElement.JitsukouKakakuNyuuryokuName, New Font(bf, customFontSize1)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(kenmeiCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim tantouCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            tantouCell.AddElement(New Paragraph("担当：" + resultElement.TantoName, New Font(bf, 10)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(tantouCell)
            'ヘッダー部5行目
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER, .Colspan = 3})
            Dim postCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            postCell.AddElement(New Paragraph(resultElement.PostCode, New Font(bf, 10)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(postCell)
            'ヘッダー部6行目
            Dim jitsushibiCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            'jitsushibiCell.AddElement(New Paragraph("改定実施日：" + resultElement.NeageDate + "　出荷分より", New Font(bf, 13, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_LEFT})
            jitsushibiCell.AddElement(New Paragraph("改定実施日：" + resultElement.NeageDate + "　" + resultElement.Kaiteijitsusi, New Font(bf, 13, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(jitsushibiCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim addressCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            Dim customFontSize2 As Integer = 10
            If resultElement.Address.Trim.Length >= 60 Then
                customFontSize2 = 8
            ElseIf resultElement.Address.Trim.Length >= 30 Then
                customFontSize2 = 10
            End If
            addressCell.AddElement(New Paragraph(resultElement.Address, New Font(bf, customFontSize2)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(addressCell)
            'ヘッダー部7行目
            Dim unchinCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            unchinCell.AddElement(New Paragraph("運賃：" + resultElement.Unchin, New Font(bf, 13, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(unchinCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim telFaxCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            Dim customFontSize3 As Integer = 10
            If resultElement.Address.Trim.Length >= 60 Then
                customFontSize3 = 8
            ElseIf resultElement.Address.Trim.Length >= 30 Then
                customFontSize3 = 10
            End If
            telFaxCell.AddElement(New Paragraph(resultElement.PhoneFax, New Font(bf, customFontSize3)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(telFaxCell)
            doc.Add(headerTable)
        End Sub
        ''' <summary>
        ''' PDFヘッダー部の設定
        ''' </summary>
        ''' <param name="doc"></param>
        ''' <param name="resultElement"></param>
        Private Sub SetDocumentHeader2(ByRef doc As Document, ByVal resultElement As MTM03SearchResultElement)
            Dim msgothicPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "msgothic.ttc,0")
            Dim bf = BaseFont.CreateFont(msgothicPath, BaseFont.IDENTITY_H, True)

            'タイトル
            Dim titleTable As New PdfPTable(3) With {
                .WidthPercentage = 100,
                .SpacingAfter = 3.0F
                }
            titleTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim titleCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER
                }
            Dim titleChunk As New Chunk("御見積書", New Font(bf, 18))
            titleChunk.SetUnderline(0.1F, -2.0F)
            Dim titlePara As New Paragraph(titleChunk) With {
                .Alignment = Element.ALIGN_CENTER
                }
            titleCell.AddElement(titlePara)
            titleTable.AddCell(titleCell)
            Dim dateCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_BOTTOM
                }
            dateCell.AddElement(New Paragraph(Date.Now.ToString("yyyy/MM/dd"), New Font(bf, 12)) With {.Alignment = Element.ALIGN_RIGHT})
            titleTable.AddCell(dateCell)
            doc.Add(titleTable)

            'ヘッダー部1行目
            Dim headerTable As New PdfPTable(4) With {
                .WidthPercentage = 100
                }
            headerTable.SetWidths({25, 25%, 25%, 25%})
            Dim tokuisakiCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .Colspan = 2
                }

            Dim tokuisakiName As String

            If Not String.IsNullOrEmpty(Trim(resultElement.AtesakiName)) Then
                If resultElement.AtesakiName.LastIndexOf("様") = (resultElement.AtesakiName.Length - 1) Then
                    tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　" + resultElement.AtesakiName
                Else
                    tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　" + resultElement.AtesakiName + "　御中"
                End If
            Else
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　御中"
            End If

            Dim customFontSize As Integer = 18

            'If tokuisakiName.Trim.Length >= 105 Then
            '    customFontSize = 6
            'ElseIf tokuisakiName.Trim.Length >= 84 Then
            '    customFontSize = 8
            'ElseIf tokuisakiName.Trim.Length >= 45 Then
            '    customFontSize = 9
            'ElseIf tokuisakiName.Trim.Length >= 35 Then
            '    customFontSize = 10
            'End If

            '-----------------------------------------
            'テストコード
            '-----------------------------------------
            Dim strtestcode01 As String = "株式会社　１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０　１２３４５６７８９０１２３４５６７８９０　１２３４５６７８あ 御中"
            tokuisakiName = strtestcode01
            If tokuisakiName.Trim.Length >= 105 Then
                customFontSize = 13
            ElseIf tokuisakiName.Trim.Length >= 84 Then
                customFontSize = 14
            ElseIf tokuisakiName.Trim.Length >= 45 Then
                customFontSize = 15
            ElseIf tokuisakiName.Trim.Length >= 35 Then
                customFontSize = 16
            End If

            Dim tokuisakiChunk As New Chunk(tokuisakiName, New Font(bf, customFontSize))
            tokuisakiChunk.SetUnderline(0.1F, -2.0F)
            Dim tokuisakiPara As New Paragraph(tokuisakiChunk) With {
                .Alignment = Element.ALIGN_LEFT
                }
            tokuisakiCell.AddElement(tokuisakiPara)
            headerTable.AddCell(tokuisakiCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER, .Colspan = 2})
            'ヘッダー部2行目
            Dim faxCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            faxCell.AddElement(New Paragraph("FAX：" + resultElement.AtesakiFaxNo, New Font(bf, 11)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(faxCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim kaisyaCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            kaisyaCell.AddElement(New Paragraph(resultElement.KaisyaName, New Font(bf, 12)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(kaisyaCell)
            'ヘッダー部3行目
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER, .Colspan = 2})
            Dim eigyosyoCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            eigyosyoCell.AddElement(New Paragraph(resultElement.EigyosyoName, New Font(bf, 12)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(eigyosyoCell)
            'ヘッダー部4行目
            Dim kenmeiCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            Dim customFontSize1 As Integer = 13
            If resultElement.JitsukouKakakuNyuuryokuName.Trim.Length >= 105 Then
                customFontSize1 = 6
            ElseIf resultElement.JitsukouKakakuNyuuryokuName.Trim.Length >= 84 Then
                customFontSize1 = 8
            ElseIf resultElement.JitsukouKakakuNyuuryokuName.Trim.Length >= 50 Then
                customFontSize1 = 10
            ElseIf resultElement.JitsukouKakakuNyuuryokuName.Trim.Length >= 35 Then
                customFontSize1 = 11
            End If

            kenmeiCell.AddElement(New Paragraph("件名：" + resultElement.JitsukouKakakuNyuuryokuName, New Font(bf, customFontSize1)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(kenmeiCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim tantouCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            tantouCell.AddElement(New Paragraph("担当：" + resultElement.TantoName, New Font(bf, 11)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(tantouCell)
            'ヘッダー部5行目
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER, .Colspan = 3})
            Dim postCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            postCell.AddElement(New Paragraph(resultElement.PostCode, New Font(bf, 11)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(postCell)
            'ヘッダー部6行目
            Dim jitsushibiCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            'jitsushibiCell.AddElement(New Paragraph("改定実施日：" + resultElement.NeageDate + "　出荷分より", New Font(bf, 13, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_LEFT})
            jitsushibiCell.AddElement(New Paragraph("改定実施日：" + resultElement.NeageDate + "　" + resultElement.Kaiteijitsusi, New Font(bf, 13, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(jitsushibiCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim addressCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            Dim customFontSize2 As Integer = 11
            If resultElement.Address.Trim.Length >= 60 Then
                customFontSize2 = 7
            ElseIf resultElement.Address.Trim.Length >= 30 Then
                customFontSize2 = 10
            End If
            addressCell.AddElement(New Paragraph(resultElement.Address, New Font(bf, customFontSize2)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(addressCell)
            'ヘッダー部7行目
            Dim unchinCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP,
                .Colspan = 2
                }
            unchinCell.AddElement(New Paragraph("運賃：" + resultElement.Unchin, New Font(bf, 13, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(unchinCell)
            headerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim telFaxCell As New PdfPCell With {
                .Border = Rectangle.NO_BORDER,
                .VerticalAlignment = Element.ALIGN_TOP
                }
            Dim customFontSize3 As Integer = 11
            If resultElement.Address.Trim.Length >= 60 Then
                customFontSize3 = 7
            ElseIf resultElement.Address.Trim.Length >= 30 Then
                customFontSize3 = 10
            End If
            telFaxCell.AddElement(New Paragraph(resultElement.PhoneFax, New Font(bf, customFontSize3)) With {.Alignment = Element.ALIGN_LEFT})
            headerTable.AddCell(telFaxCell)
            doc.Add(headerTable)
        End Sub
        ''' <summary>
        ''' PDF詳細部分の設定
        ''' </summary>
        ''' <param name="doc"></param>
        ''' <param name="detailList"></param>
        ''' <param name="lineCounter"></param>
        Private Sub SetDocumentDetail(ByRef doc As Document, ByVal detailList As List(Of MTM03SearchResultElementDetail), ByRef lineCounter As Integer)
            Dim msgothicPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "msgothic.ttc,0")
            Dim bf = BaseFont.CreateFont(msgothicPath, BaseFont.IDENTITY_H, True)

            '詳細
            Dim detailTable As New PdfPTable(9) With {
                .WidthPercentage = 100
                }
            detailTable.SetWidths({4, 32, 6, 6, 6, 6, 6, 6, 32})
            Dim lineHCell As New PdfPCell With {
                .BorderWidthTop = 2,
                .BorderWidthLeft = 2
                }
            lineHCell.AddElement(New Paragraph("行", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(lineHCell)
            Dim shohinHCell As New PdfPCell With {
                .BorderWidthTop = 2
                }
            shohinHCell.AddElement(New Paragraph("商品名", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(shohinHCell)
            Dim irisuHCell As New PdfPCell With {
                .BorderWidthTop = 2
                }
            irisuHCell.AddElement(New Paragraph("入数", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(irisuHCell)
            Dim taniHCell As New PdfPCell With {
                .BorderWidthTop = 2
                }
            taniHCell.AddElement(New Paragraph("単位", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(taniHCell)
            Dim lotHCell As New PdfPCell With {
                .BorderWidthTop = 2
                }
            lotHCell.AddElement(New Paragraph("ロット", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(lotHCell)
            Dim newTankaHCell As New PdfPCell With {
                .BorderWidthTop = 2
                }
            newTankaHCell.AddElement(New Paragraph("新単価", New Font(bf, 7, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(newTankaHCell)
            Dim oldTankaHCell As New PdfPCell With {
                .BorderWidthTop = 2
                }
            oldTankaHCell.AddElement(New Paragraph("旧単価", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(oldTankaHCell)
            Dim jissekiHCell As New PdfPCell With {
                .BorderWidthTop = 2
                }
            jissekiHCell.AddElement(New Paragraph("最終実績", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(jissekiHCell)
            Dim bikouHCell As New PdfPCell With {
                .BorderWidthTop = 2,
                .BorderWidthRight = 2,
                .VerticalAlignment = Element.ALIGN_MIDDLE
                }
            bikouHCell.AddElement(New Paragraph("備考", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(bikouHCell)
            Dim spaceCell1 As New PdfPCell With {
                .BorderWidthLeft = 2,
                .BorderWidthBottom = 2
                }
            detailTable.AddCell(spaceCell1)
            Dim spaceCell2 As New PdfPCell With {
                .Colspan = 7,
                .BorderWidthRight = Rectangle.NO_BORDER,
                .BorderWidthBottom = 2
                }
            detailTable.AddCell(spaceCell2)
            Dim nohinHCell As New PdfPCell With {
                .VerticalAlignment = Element.ALIGN_MIDDLE,
                .BorderWidthLeft = Rectangle.NO_BORDER,
                .BorderWidthRight = 2,
                .BorderWidthBottom = 2
                }
            nohinHCell.AddElement(New Paragraph("納品先履歴", New Font(bf, 8)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(nohinHCell)

            Dim oldSyohinCode As String = ""
            Dim oldKikaku As String = ""
            Dim topNohinHistory As String = ""
            For Each detail In detailList
                If oldSyohinCode <> "" And ((Not oldSyohinCode = detail.SyohinCode.Trim) Or (Not oldKikaku = detail.Kikaku.Trim)) Then
                    Dim restNohinCell As New PdfPCell With {
                        .Colspan = 9,
                        .BorderWidthLeft = 2,
                        .BorderWidthRight = 2
                        }
                    Dim nohinHistory As String = "　"
                    If (Not String.IsNullOrEmpty(Trim(topNohinHistory))) Then
                        nohinHistory = Trim(topNohinHistory)
                    End If
                    restNohinCell.AddElement(New Paragraph(nohinHistory, New Font(bf, 7)) With {.Alignment = Element.ALIGN_RIGHT})
                    detailTable.AddCell(restNohinCell)
                End If

                Dim shohinCell As New PdfPCell
                Dim showlLineCounter As Boolean = True
                If oldSyohinCode = detail.SyohinCode.Trim And oldKikaku = detail.Kikaku.Trim Then
                    shohinCell.AddElement(New Paragraph("同上", New Font(bf, 8)))
                    showlLineCounter = False
                Else
                    Dim shohinFontSize As Integer = 8

                    If detail.SyohinName.Length >= 87 Then
                        shohinFontSize = 4
                    ElseIf detail.SyohinName.Length >= 58 Then
                        shohinFontSize = 6
                    End If
                    shohinCell.AddElement(New Paragraph(detail.SyohinName, New Font(bf, shohinFontSize)))
                    topNohinHistory = detail.NohinHistory
                    lineCounter += 1
                    showlLineCounter = True
                End If

                Dim lineCell As New PdfPCell With {
                    .BorderWidthLeft = 2
                    }
                If (showlLineCounter) Then
                    lineCell.AddElement(New Paragraph(lineCounter, New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
                Else
                    lineCell.AddElement(New Paragraph("", New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
                End If
                detailTable.AddCell(lineCell)

                detailTable.AddCell(shohinCell)
                Dim irisuCell As New PdfPCell
                irisuCell.AddElement(New Paragraph(detail.Irisu, New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(irisuCell)
                Dim taniCell As New PdfPCell
                taniCell.AddElement(New Paragraph(detail.Tanni, New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(taniCell)
                Dim lotCell As New PdfPCell
                lotCell.AddElement(New Paragraph(detail.Lot, New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(lotCell)
                Dim newTankaCell As New PdfPCell
                newTankaCell.AddElement(New Paragraph(detail.NewTanka, New Font(bf, 7)) With {.Alignment = Element.ALIGN_RIGHT})
                detailTable.AddCell(newTankaCell)
                Dim oldTankaCell As New PdfPCell
                oldTankaCell.AddElement(New Paragraph(detail.OldTanka, New Font(bf, 7)) With {.Alignment = Element.ALIGN_RIGHT})
                detailTable.AddCell(oldTankaCell)
                Dim jissekiCell As New PdfPCell
                jissekiCell.AddElement(New Paragraph(detail.SaisyuDate, New Font(bf, 7)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(jissekiCell)

                Dim bikouFontSize As Integer = 7

                If detail.Bikou.Length >= 38 Then
                    bikouFontSize = 4
                ElseIf detail.Bikou.Length >= 33 Then
                    bikouFontSize = 6
                End If
                Dim bikouCell As New PdfPCell With {
                    .BorderWidthRight = 2
                    }
                bikouCell.AddElement(New Paragraph(detail.Bikou, New Font(bf, bikouFontSize)))
                detailTable.AddCell(bikouCell)

                oldSyohinCode = detail.SyohinCode.Trim
                oldKikaku = detail.Kikaku.Trim
            Next

            Dim nohinCell As New PdfPCell With {
                .Colspan = 9,
                .BorderWidthLeft = 2,
                .BorderWidthBottom = 2,
                .BorderWidthRight = 2
                }
            If String.IsNullOrEmpty(Trim(topNohinHistory)) Then
                topNohinHistory = "　"
            End If
            nohinCell.AddElement(New Paragraph(topNohinHistory, New Font(bf, 7)) With {.Alignment = Element.ALIGN_RIGHT})
            detailTable.AddCell(nohinCell)

            doc.Add(detailTable)
        End Sub

        ''' <summary>
        ''' PDF詳細部分の設定
        ''' </summary>
        ''' <param name="doc"></param>
        ''' <param name="detailList"></param>
        ''' <param name="lineCounter"></param>
        Private Sub SetDocumentDetail2(ByRef doc As Document, ByVal detailList As List(Of MTM03SearchResultElementDetail),
                                       ByRef lineCounter As Integer, ByVal conectShohin As Boolean, ByVal conectNohinRireki As Boolean, ByVal conectTopNohinHistory As String)
            Dim msgothicPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "msgothic.ttc,0")
            Dim bf = BaseFont.CreateFont(msgothicPath, BaseFont.IDENTITY_H, True)

            '詳細
            Dim detailTable As New PdfPTable(9) With {
                .WidthPercentage = 100
                }
            detailTable.SetWidths({4, 32, 5, 5, 5, 8, 8, 6, 30})
            Dim lineHCell As New PdfPCell With {
                .BorderWidthLeft = 1.5,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0,
                .FixedHeight = 21
                }
            lineHCell.AddElement(New Paragraph("行", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(lineHCell)
            Dim shohinHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            shohinHCell.AddElement(New Paragraph("商品名", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(shohinHCell)
            Dim irisuHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            irisuHCell.AddElement(New Paragraph("入数", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(irisuHCell)
            Dim taniHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            taniHCell.AddElement(New Paragraph("単位", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(taniHCell)
            Dim lotHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            lotHCell.AddElement(New Paragraph("ロット", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(lotHCell)
            Dim newTankaHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            newTankaHCell.AddElement(New Paragraph("新単価", New Font(bf, 10, System.Drawing.FontStyle.Regular, BaseColor.RED)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(newTankaHCell)
            Dim oldTankaHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            oldTankaHCell.AddElement(New Paragraph("旧単価", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(oldTankaHCell)
            Dim jissekiHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            jissekiHCell.AddElement(New Paragraph("最終実績", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(jissekiHCell)
            Dim bikouHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 1.5,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
            bikouHCell.AddElement(New Paragraph("備考", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(bikouHCell)
            'Dim spaceCell1 As New PdfPCell With {
            '    .BorderWidthLeft = 2,
            '    .BorderWidthBottom = 2
            '    }
            'detailTable.AddCell(spaceCell1)
            Dim spaceCell2 As New PdfPCell With {
                .Colspan = 8,
                .BorderWidthLeft = 1.5,
                .BorderWidthRight = 0,
                .BorderWidthTop = 0.5,
                .BorderWidthBottom = 1.5,
                .FixedHeight = 21
                }
            detailTable.AddCell(spaceCell2)
            Dim nohinHCell As New PdfPCell With {
                .BorderWidthLeft = 0,
                .BorderWidthRight = 1.5,
                .BorderWidthTop = 0.5,
                .BorderWidthBottom = 1.5
                }
            nohinHCell.AddElement(New Paragraph("納品先履歴", New Font(bf, 10)) With {.Alignment = Element.ALIGN_CENTER})
            detailTable.AddCell(nohinHCell)

            Dim oldSyohinCode As String = ""
            Dim oldKikaku As String = ""
            Dim topNohinHistory As String = ""
            For Each detail In detailList
                If oldSyohinCode <> "" And ((Not oldSyohinCode = detail.SyohinCode.Trim) Or (Not oldKikaku = detail.Kikaku.Trim)) Then
                    Dim restNohinCell As New PdfPCell With {
                        .Colspan = 9,
                        .BorderWidthLeft = 1.5,
                        .BorderWidthRight = 1.5,
                        .BorderWidthTop = 0.5,
                        .BorderWidthBottom = 1.5,
                        .FixedHeight = 22,
                        .VerticalAlignment = Element.ALIGN_MIDDLE
                        }
                    Dim nohinHistory As String = "　"
                    If (Not String.IsNullOrEmpty(Trim(topNohinHistory))) Then
                        nohinHistory = Trim(topNohinHistory)
                    End If
                    restNohinCell.AddElement(New Paragraph(nohinHistory, New Font(bf, 11)) With {.Alignment = Element.ALIGN_RIGHT})
                    detailTable.AddCell(restNohinCell)
                End If

                Dim shohinCell As New PdfPCell With {
                        .FixedHeight = 28,
                        .BorderWidthLeft = 0,
                        .BorderWidthRight = 0.5,
                        .BorderWidthTop = 0,
                        .BorderWidthBottom = 0,
                        .VerticalAlignment = Element.ALIGN_MIDDLE
                        }
                Dim showlLineCounter As Boolean = True
                If oldSyohinCode = detail.SyohinCode.Trim And oldKikaku = detail.Kikaku.Trim Then
                    shohinCell.AddElement(New Paragraph("　", New Font(bf, 11)))
                    showlLineCounter = False
                Else
                    If (conectShohin) Then
                        shohinCell.AddElement(New Paragraph("　", New Font(bf, 11)))
                        Dim nohinHistory As String = "　"
                        If (Not String.IsNullOrEmpty(Trim(conectTopNohinHistory))) Then
                            nohinHistory = Trim(conectTopNohinHistory)
                        End If
                        topNohinHistory = nohinHistory
                        lineCounter += 1
                        showlLineCounter = True
                        conectShohin = False
                    Else
                        Dim shohinFontSize As Integer = 11
                        Dim tokuisakiName_lenb2 = StringNumberByteDigitsLenB(detail.SyohinName)

                        If tokuisakiName_lenb2 >= 52 Then
                            shohinFontSize = 8
                        ElseIf tokuisakiName_lenb2 >= 45 Then
                            shohinFontSize = 9
                        ElseIf tokuisakiName_lenb2 >= 42 Then
                            shohinFontSize = 10
                        ElseIf tokuisakiName_lenb2 >= 25 Then
                            shohinFontSize = 11
                        End If
                        shohinCell.AddElement(New Paragraph(detail.SyohinName.Trim, New Font(bf, shohinFontSize)))
                        topNohinHistory = detail.NohinHistory
                        lineCounter += 1
                        showlLineCounter = True
                        conectShohin = False
                    End If
                End If

                Dim lineCell As New PdfPCell With {
                    .BorderWidthLeft = 1.5,
                    .BorderWidthRight = 0.5,
                    .BorderWidthTop = 0,
                    .BorderWidthBottom = 0,
                    .VerticalAlignment = Element.ALIGN_MIDDLE
                    }
                If (showlLineCounter) Then
                    lineCell.AddElement(New Paragraph(lineCounter, New Font(bf, 8)) With {.Alignment = Element.ALIGN_CENTER})
                Else
                    lineCell.AddElement(New Paragraph("　", New Font(bf, 8)) With {.Alignment = Element.ALIGN_CENTER})
                End If
                detailTable.AddCell(lineCell)

                detailTable.AddCell(shohinCell)
                Dim irisuCell As New PdfPCell With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .BorderWidthLeft = 0,
                        .BorderWidthRight = 0.5,
                        .BorderWidthTop = 0,
                        .BorderWidthBottom = 0
                        }
                Dim newIrisuFontSize As Integer = 8
                If detail.Irisu.Trim.Length >= 12 Then
                    newIrisuFontSize = 6
                ElseIf detail.Irisu.Trim.Length >= 9 Then
                    newIrisuFontSize = 7
                End If
                irisuCell.AddElement(New Paragraph(detail.Irisu.Trim, New Font(bf, newIrisuFontSize)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(irisuCell)
                Dim taniCell As New PdfPCell With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .BorderWidthLeft = 0,
                        .BorderWidthRight = 0.5,
                        .BorderWidthTop = 0,
                        .BorderWidthBottom = 0
                        }
                Dim strtaniCell As String = "　"
                If (Not String.IsNullOrEmpty(detail.Tanni.Trim)) Then
                    strtaniCell = detail.Tanni.Trim
                End If
                taniCell.AddElement(New Paragraph(strtaniCell, New Font(bf, 8)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(taniCell)
                Dim lotCell As New PdfPCell With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .BorderWidthLeft = 0,
                        .BorderWidthRight = 0.5,
                        .BorderWidthTop = 0,
                        .BorderWidthBottom = 0
                        }
                lotCell.AddElement(New Paragraph(detail.Lot, New Font(bf, 8)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(lotCell)
                Dim newTankaCell As New PdfPCell With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .BorderWidthLeft = 0,
                        .BorderWidthRight = 0.5,
                        .BorderWidthTop = 0,
                        .BorderWidthBottom = 0
                        }
                Dim newTankaFontSize As Integer = 11
                If detail.NewTanka.Trim.Length >= 12 Then
                    newTankaFontSize = 7
                ElseIf detail.NewTanka.Trim.Length >= 9 Then
                    newTankaFontSize = 9
                End If
                newTankaCell.AddElement(New Paragraph(detail.NewTanka, New Font(bf, newTankaFontSize)) With {.Alignment = Element.ALIGN_RIGHT})
                detailTable.AddCell(newTankaCell)
                Dim oldTankaCell As New PdfPCell With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .BorderWidthLeft = 0,
                        .BorderWidthRight = 0.5,
                        .BorderWidthTop = 0,
                        .BorderWidthBottom = 0
                        }
                Dim oldTankaFontSize As Integer = 11
                If detail.OldTanka.Trim.Length >= 12 Then
                    oldTankaFontSize = 7
                ElseIf detail.OldTanka.Trim.Length >= 9 Then
                    oldTankaFontSize = 9
                End If
                oldTankaCell.AddElement(New Paragraph(detail.OldTanka, New Font(bf, oldTankaFontSize)) With {.Alignment = Element.ALIGN_RIGHT})
                detailTable.AddCell(oldTankaCell)
                Dim jissekiCell As New PdfPCell With {
                        .VerticalAlignment = Element.ALIGN_MIDDLE,
                        .BorderWidthLeft = 0,
                        .BorderWidthRight = 0.5,
                        .BorderWidthTop = 0,
                        .BorderWidthBottom = 0
                        }
                jissekiCell.AddElement(New Paragraph(detail.SaisyuDate, New Font(bf, 8)) With {.Alignment = Element.ALIGN_CENTER})
                detailTable.AddCell(jissekiCell)

                Dim bikouFontSize As Integer = 11
                Dim bikou_lenb3 = StringNumberByteDigitsLenB(detail.Bikou)
                If bikou_lenb3 >= 49 Then
                    bikouFontSize = 8
                ElseIf bikou_lenb3 >= 44 Then
                    bikouFontSize = 9
                ElseIf bikou_lenb3 >= 39 Then
                    bikouFontSize = 10
                End If

                Dim bikouCell As New PdfPCell With {
                    .VerticalAlignment = Element.ALIGN_MIDDLE,
                    .BorderWidthLeft = 0,
                    .BorderWidthRight = 1.5,
                    .BorderWidthTop = 0,
                    .BorderWidthBottom = 0
                    }
                bikouCell.AddElement(New Paragraph(detail.Bikou.Trim, New Font(bf, bikouFontSize)))
                detailTable.AddCell(bikouCell)

                oldSyohinCode = detail.SyohinCode.Trim
                oldKikaku = detail.Kikaku.Trim
            Next

            If (Not conectNohinRireki) Then
                Dim nohinCell As New PdfPCell With {
                .Colspan = 9,
                .BorderWidthLeft = 1.5,
                .BorderWidthRight = 1.5,
                .BorderWidthTop = 0.5,
                .BorderWidthBottom = 1.5,
                .FixedHeight = 22,
                .VerticalAlignment = Element.ALIGN_MIDDLE
                }
                If String.IsNullOrEmpty(Trim(topNohinHistory)) Then
                    topNohinHistory = "　"
                End If
                nohinCell.AddElement(New Paragraph(topNohinHistory, New Font(bf, 11)) With {.Alignment = Element.ALIGN_RIGHT})
                detailTable.AddCell(nohinCell)
            Else
                Dim nohinCell As New PdfPCell With {
                .Colspan = 9,
                .BorderWidthLeft = 0,
                .BorderWidthRight = 0,
                .BorderWidthTop = 1.5,
                .BorderWidthBottom = 0
                }
                nohinCell.AddElement(New Paragraph("　", New Font(bf, 11)) With {.Alignment = Element.ALIGN_RIGHT})
                detailTable.AddCell(nohinCell)
            End If


            doc.Add(detailTable)
        End Sub

        ''' <summary>
        ''' PDFフッター部分の設定
        ''' </summary>
        ''' <param name="doc"></param>
        ''' <param name="contentByte"></param>
        ''' <param name="page"></param>
        ''' <param name="totalPage"></param>
        Private Sub SetDocumentFooter(ByRef doc As Document, ByRef contentByte As PdfContentByte, ByVal page As Integer, ByVal totalPage As Integer)
            Dim msgothicPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "msgothic.ttc,0")
            Dim bf = BaseFont.CreateFont(msgothicPath, BaseFont.IDENTITY_H, True)

            'ページ
            Dim footerTable As New PdfPTable(1)
            footerTable.AddCell(New PdfPCell With {.Border = Rectangle.NO_BORDER})
            Dim pageCell As New PdfPCell With {
            .Border = Rectangle.NO_BORDER
            }
            pageCell.AddElement(New Paragraph(page.ToString + " / " + totalPage.ToString, New Font(bf, 8)) With {.Alignment = Element.ALIGN_CENTER})
            footerTable.AddCell(pageCell)

            footerTable.SetTotalWidth(New Single() {840})
            footerTable.WriteSelectedRows(0, -1, 0, 50, contentByte)
        End Sub

        Public Function SendMailMitsumo(ByVal searchCondition As MTM03SearchCondition, ByVal mailInfo As MTM03MailInfo, ByVal outputPath As String, ByVal loginId As String) As MTM03SendMailData
            Dim sendMailData As New MTM03SendMailData

            sendMailData.SearchResult = Me.GetKakaku(searchCondition)

            If sendMailData.SearchResult.ElementList.Count > 0 Then
                For Each resultElement In sendMailData.SearchResult.ElementList
                    If (searchCondition.OutputNum = "0" Or searchCondition.OutputNum = "1") And resultElement.SoushinKubun = "1" Then
                        Dim sendMailErrorList = Me.SendMail(searchCondition.KakakuNyuuryokuNo, mailInfo, resultElement, outputPath)
                        If sendMailErrorList.Count > 0 Then
                            sendMailData.ErrorList.AddRange(sendMailErrorList)
                            Exit For
                        End If
                    ElseIf (searchCondition.OutputNum = "0" Or searchCondition.OutputNum = "2") And resultElement.SoushinKubun = "2" Then
                        Dim sendEDocumentErrorList = Me.SendEDocumentHeader(searchCondition.KakakuNyuuryokuNo, mailInfo, resultElement, outputPath)
                        If sendEDocumentErrorList.Count > 0 Then
                            sendMailData.ErrorList.AddRange(sendEDocumentErrorList)
                            Exit For
                        End If
                    End If

                    Dim updateErrorList = Me.Update(resultElement, loginId)
                    If updateErrorList.Count > 0 Then
                        sendMailData.ErrorList.AddRange(updateErrorList)
                        Exit For
                    End If
                Next
            Else
                sendMailData.ErrorList.Add("見積データがありません")
            End If

            Return sendMailData
        End Function

        ''' <summary>
        ''' メール本文送信処理
        ''' </summary>
        ''' <param name="mailInfo"></param>
        ''' <param name="resultElement"></param>
        ''' <param name="outputPath"></param>
        ''' <returns></returns>
        Public Function SendMail(ByVal kakakuNyuuryokuNo As String, ByVal mailInfo As MTM03MailInfo, ByVal resultElement As MTM03SearchResultElement, ByVal outputPath As String) As List(Of String)
            Dim errorList As New List(Of String)
            If Not System.IO.Directory.Exists(outputPath) Then
                System.IO.Directory.CreateDirectory(outputPath)
            End If

            Dim tokuisakiName As String
            Dim tantouName As String
            Dim tantouLastPosition As Integer = 1
            Dim tantouLastStr As String = 1

            If Not String.IsNullOrEmpty(Trim(resultElement.AtesakiName)) Then
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2
            Else
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　御中"
            End If

            If Not String.IsNullOrEmpty(Trim(resultElement.AtesakiName)) Then
                tantouLastStr = resultElement.AtesakiName.Substring(resultElement.AtesakiName.Length - tantouLastPosition)
                If (tantouLastStr.Contains("様")) Then
                    tantouName = resultElement.AtesakiName
                Else
                    tantouName = resultElement.AtesakiName + "　御中"
                End If
            Else
                tantouName = resultElement.AtesakiName
            End If

            Dim fileName = resultElement.KakakuNyuuryokuNo.Trim + "_御見積書_" + resultElement.TokuisakiName1.Trim + "様_" + Date.Now.ToString("yyyyMMddHHmm") + ".pdf"
            Dim filePath = outputPath + "\" + fileName

            Dim pdfErrorList = Me.OutputAttachmentPdf(kakakuNyuuryokuNo, resultElement, filePath)
            If pdfErrorList.Count > 0 Then
                Return pdfErrorList
                Exit Function
            End If

            Dim mailMessage As New System.Net.Mail.MailMessage
            Dim smtpClient As New System.Net.Mail.SmtpClient()
            Dim myEnc As Encoding = Encoding.GetEncoding("iso-2022-jp")

            Try
                mailMessage.From = New System.Net.Mail.MailAddress(resultElement.MailFrom)

                Dim toList = resultElement.MailTo.Split(",")
                For Each mailTo As String In toList
                    mailMessage.To.Add(New System.Net.Mail.MailAddress(mailTo.Trim))
                Next

                mailMessage.Subject = "（" + resultElement.JitsukouKakakuNyuuryokuName + "）改定お見積書の送付"

                Dim body As String = tokuisakiName + vbCrLf _
                    + tantouName + vbCrLf _
                    + vbCrLf _
                    + "　　お世話になっております。" + vbCrLf _
                    + "　　もりや産業の" + resultElement.TantoName + "です。" + vbCrLf _
                    + vbCrLf _
                    + "　　この度、下記の商品の価格改定がございますので" + vbCrLf _
                    + "　　改定見積書を添付致します。" + vbCrLf _
                    + "　　ご査証いただきますようよろしくお願い致します。" + vbCrLf _
                    + vbCrLf _
                    + "　　　　該当商品：　" + resultElement.JitsukouKakakuNyuuryokuName + vbCrLf _
                    + "　　　　改定日：　" + resultElement.NeageDate + " " + resultElement.Kaiteijitsusi + vbCrLf _
                    + vbCrLf _
                    + "　　以上よろしくお願い致します。" + vbCrLf _
                    + vbCrLf _
                    + "　　--------------------------------" + vbCrLf _
                    + "　　もりや産業株式会社" + vbCrLf _
                    + "　　　　" + resultElement.EigyosyoName + vbCrLf _
                    + "　　　　" + resultElement.TantoName + vbCrLf _
                    + vbCrLf _
                    + "　　　　住所　" + resultElement.Address + vbCrLf _
                    + "　　　　" + resultElement.PhoneFax + vbCrLf _
                    + "　　　　メール　" + resultElement.MailFrom + vbCrLf

                'mailMessage.Body = body
                'mailMessage.IsBodyHtml = False

                Dim altView As AlternateView = AlternateView.CreateAlternateViewFromString(body, myEnc, System.Net.Mime.MediaTypeNames.Text.Plain)
                altView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit
                mailMessage.AlternateViews.Add(altView)
                mailMessage.Headers.Add("Content-Transfer-Encoding", "7bit")

                Dim attach As New System.Net.Mail.Attachment(filePath, MediaTypeNames.Application.Pdf)
                Dim disposition As ContentDisposition = attach.ContentDisposition
                disposition.FileName = EncordB(fileName)
                mailMessage.Attachments.Add(attach)

                'gmailのSMTPサーバの設定
                smtpClient.Host = mailInfo.MailHost
                smtpClient.Port = Integer.Parse(mailInfo.MailPort)
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                'ユーザー名,パスワード
                smtpClient.Credentials = New System.Net.NetworkCredential(mailInfo.MailUser, mailInfo.MailPass)
                'SSL
                smtpClient.EnableSsl = False
                smtpClient.Timeout = 10000
                smtpClient.Send(mailMessage)
            Catch ex As Exception
                errorList.Add(ex.Message)
            Finally
                mailMessage.Dispose()
                smtpClient.Dispose()
            End Try

            Return errorList
        End Function


        ''' <summary>
        ''' 添付用PDF出力
        ''' </summary>
        ''' <returns>String</returns>
        Private Function OutputAttachmentPdf(ByVal kakakuNyuuryokuNo As String, ByVal resultElement As MTM03SearchResultElement, ByVal filePath As String) As List(Of String)
            Dim errorList As New List(Of String)

            Try
                Dim annaiFilePath As String = Me.GetAnnaiFilePath(kakakuNyuuryokuNo)

                'ドキュメントを作成
                Dim doc As New Document(PageSize.A4.Rotate)
                Dim stream As New FileStream(filePath, FileMode.Create)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, stream)

                'ドキュメントを開く
                doc.Open()

                Dim contentByte As PdfContentByte = writer.DirectContent

                If System.IO.File.Exists(annaiFilePath) Then
                    Dim reader As New PdfReader(annaiFilePath)
                    For idx As Integer = 1 To reader.NumberOfPages
                        Dim importedPage As PdfImportedPage = writer.GetImportedPage(reader, idx)
                        doc.SetPageSize(reader.GetPageSize(idx))
                        doc.NewPage()
                        contentByte.AddTemplate(importedPage, 0, 0)
                    Next

                    doc.SetPageSize(PageSize.A4.Rotate)
                    doc.NewPage()
                End If

                SetDocument2(doc, contentByte, resultElement)

                'ドキュメントを閉じる
                doc.Close()
            Catch ex As Exception
                errorList.Add(ex.Message)
            End Try

            Return errorList
        End Function

        Private Function GetAnnaiFilePath(ByVal kakakuNyuuryokuNo As String) As String
            Dim filePath As String = ""

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection

                    command.CommandText = "SELECT MTMR003009" _ '案内文
                        + " FROM MTM10R003JITSUKOU" _
                        + " WHERE MTMR003001 = @MTMR003001"

                    command.Parameters.Add(New SqlParameter("@MTMR003001", kakakuNyuuryokuNo))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read Then
                        filePath = reader.Item("MTMR003009").ToString
                    End If
                    reader.Close()
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return filePath
        End Function

        ''' <summary>
        ''' E帳票ヘッダー送信
        ''' </summary>
        ''' <param name="mailInfo"></param>
        ''' <param name="resultElement"></param>
        ''' <param name="outputPath"></param>
        ''' <returns></returns>
        Public Function SendEDocumentHeader(ByVal kakakuNyuuryokuNo As String, ByVal mailInfo As MTM03MailInfo, ByVal resultElement As MTM03SearchResultElement, ByVal outputPath As String) As List(Of String)
            Dim errorList As New List(Of String)
            If Not System.IO.Directory.Exists(outputPath) Then
                System.IO.Directory.CreateDirectory(outputPath)
            End If

            Dim tokuisakiName As String

            '必須チェック
            If (String.IsNullOrEmpty(resultElement.EDocFrom)) Then
                errorList.Add("送信できません。制御マスタのTOアドレスが空白です")
                Return errorList
                Exit Function
            End If
            If (String.IsNullOrEmpty(resultElement.EDocTo)) Then
                errorList.Add("送信できません。制御マスタのFROMアドレスが空白です")
                Return errorList
                Exit Function
            End If

            If Not String.IsNullOrEmpty(Trim(resultElement.AtesakiName)) Then
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2
            Else
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　御中"
            End If

            Dim fileName = resultElement.KakakuNyuuryokuNo.Trim + "_御見積書_" + resultElement.TokuisakiName1.Trim + "様_" + Date.Now.ToString("yyyyMMddHHmm") + ".pdf"
            Dim filePath = outputPath + "\" + fileName

            Dim pdfErrorList = Me.OutputAttachmentPdf(kakakuNyuuryokuNo, resultElement, filePath)
            If pdfErrorList.Count > 0 Then
                Return pdfErrorList
                Exit Function
            End If

            Dim mailMessage As New System.Net.Mail.MailMessage
            Dim smtpClient As New System.Net.Mail.SmtpClient()
            Dim myEnc As Encoding = Encoding.GetEncoding("iso-2022-jp")

            Try
                mailMessage.From = New System.Net.Mail.MailAddress(resultElement.EDocFrom)
                mailMessage.Sender = New System.Net.Mail.MailAddress(resultElement.EDocFrom)

                Dim toList = resultElement.EDocTo.Split(",")
                For Each mailTo As String In toList
                    mailMessage.To.Add(New System.Net.Mail.MailAddress(mailTo.Trim))
                Next

                mailMessage.Subject = "mitsumo_" + resultElement.KakakuNyuuryokuNo.Trim + "_" _
                    + resultElement.TokuisakiCode.Trim + "_" _
                    + Date.Now.ToString("yyyyMMdd") + " " _
                    + Date.Now.ToString("HHmmss")

                If (Not String.IsNullOrEmpty(resultElement.EDocBCc)) Then
                    Dim bccList = resultElement.EDocBCc.Split(",")
                    For Each bcc As String In bccList
                        mailMessage.Bcc.Add(New System.Net.Mail.MailAddress(bcc.Trim))
                    Next
                End If

                If (Not String.IsNullOrEmpty(resultElement.EDocCc)) Then
                    Dim ccList = resultElement.EDocCc.Split(",")
                    For Each cc As String In ccList
                        mailMessage.CC.Add(New System.Net.Mail.MailAddress(cc.Trim))
                    Next
                End If

                mailMessage.Headers.Add("Content-Transfer-Encoding", "7bit")

                Dim body As String = "#sid=" + resultElement.EDocServiceId + vbCrLf _
                    + "#passwd=" + resultElement.EDocPassword.Trim + vbCrLf _
                    + "#faxno=" + resultElement.AtesakiFaxNo.Trim + vbCrLf _
                    + "#transcd=T" + resultElement.TokuisakiCode.Trim + vbCrLf _
                    + "#groupcd=" + resultElement.LoginId + vbCrLf _
                    + "#dividecd=" + resultElement.EigyosyoCode.Trim + resultElement.BukaCode.Trim + vbCrLf _
                    + "#noticeclass=" + resultElement.EDocNoticeClass + vbCrLf _
                    + "#noticeaddress=" + resultElement.NoticeAddress + vbCrLf _
                    + "#faxmode=" + resultElement.EDocFaxMode + vbCrLf _
                    + "#noticesubject=" + "mitsumo_" + resultElement.KakakuNyuuryokuNo.Trim + "_" + resultElement.TokuisakiCode.Trim + vbCrLf _
                    + "#freeitem=" + "mitsumo_" + resultElement.KakakuNyuuryokuNo.Trim + "_" + tokuisakiName.Trim + vbCrLf

                mailMessage.Body = body
                mailMessage.IsBodyHtml = False
                mailMessage.BodyEncoding = myEnc
                mailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.SevenBit

                'Dim altView As AlternateView = AlternateView.CreateAlternateViewFromString(body, myEnc, System.Net.Mime.MediaTypeNames.Text.Plain)
                'altView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit
                'mailMessage.AlternateViews.Add(altView)

                Dim attach As New System.Net.Mail.Attachment(filePath, MediaTypeNames.Application.Pdf)
                Dim disposition As ContentDisposition = attach.ContentDisposition
                disposition.FileName = EncordB(fileName)
                mailMessage.Attachments.Add(attach)

                'gmailのSMTPサーバの設定
                smtpClient.Host = mailInfo.MailHost
                smtpClient.Port = Integer.Parse(mailInfo.MailPort)
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                'ユーザー名,パスワード
                smtpClient.Credentials = New System.Net.NetworkCredential(mailInfo.MailUser, mailInfo.MailPass)
                'SSL
                smtpClient.EnableSsl = False
                smtpClient.Timeout = 10000
                smtpClient.Send(mailMessage)
            Catch ex As Exception
                errorList.Add(ex.Message)
            Finally
                mailMessage.Dispose()
                smtpClient.Dispose()
            End Try

            Return errorList
        End Function
        ''' <summary>
        ''' E帳票ヘッダー送信
        ''' </summary>
        ''' <param name="mailInfo"></param>
        ''' <param name="resultElement"></param>
        ''' <param name="outputPath"></param>
        ''' <returns></returns>
        Public Function SendEDocumentHeader2(ByVal kakakuNyuuryokuNo As String, ByVal mailInfo As MTM03MailInfo, ByVal resultElement As MTM03SearchResultElement, ByVal outputPath As String) As List(Of String)
            Dim errorList As New List(Of String)
            If Not System.IO.Directory.Exists(outputPath) Then
                System.IO.Directory.CreateDirectory(outputPath)
            End If

            Dim tokuisakiName As String

            '必須チェック
            If (String.IsNullOrEmpty(resultElement.EDocFrom)) Then
                errorList.Add("送信できません。制御マスタのTOアドレスが空白です")
                Return errorList
                Exit Function
            End If
            If (String.IsNullOrEmpty(resultElement.EDocTo)) Then
                errorList.Add("送信できません。制御マスタのFROMアドレスが空白です")
                Return errorList
                Exit Function
            End If

            If Not String.IsNullOrEmpty(Trim(resultElement.AtesakiName)) Then
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2
            Else
                tokuisakiName = resultElement.TokuisakiName1 + "　" + resultElement.TokuisakiName2 + "　御中"
            End If

            Dim fileName = resultElement.KakakuNyuuryokuNo.Trim + "_御見積書_" + resultElement.TokuisakiName1.Trim + "様_" + Date.Now.ToString("yyyyMMddHHmm") + ".pdf"
            Dim filePath = outputPath + "\" + fileName

            Dim pdfErrorList = Me.OutputAttachmentPdf(kakakuNyuuryokuNo, resultElement, filePath)
            If pdfErrorList.Count > 0 Then
                Return pdfErrorList
                Exit Function
            End If

            Dim mailMessage As New System.Net.Mail.MailMessage
            Dim smtpClient As New System.Net.Mail.SmtpClient()
            Dim myEnc As Encoding = Encoding.GetEncoding("iso-2022-jp")

            Try
                mailMessage.From = New System.Net.Mail.MailAddress(resultElement.EDocFrom)
                mailMessage.Sender = New System.Net.Mail.MailAddress(resultElement.EDocFrom)

                Dim toList = resultElement.EDocTo.Split(",")
                For Each mailTo As String In toList
                    mailMessage.To.Add(New System.Net.Mail.MailAddress(mailTo.Trim))
                Next

                mailMessage.Subject = "mitsumo_" + resultElement.KakakuNyuuryokuNo.Trim + "_" _
                    + resultElement.TokuisakiCode.Trim + "_" _
                    + Date.Now.ToString("yyyyMMdd") + " " _
                    + Date.Now.ToString("HHmmss")

                If (Not String.IsNullOrEmpty(resultElement.EDocBCc)) Then
                    Dim bccList = resultElement.EDocBCc.Split(",")
                    For Each bcc As String In bccList
                        mailMessage.Bcc.Add(New System.Net.Mail.MailAddress(bcc.Trim))
                    Next
                End If

                If (Not String.IsNullOrEmpty(resultElement.EDocCc)) Then
                    Dim ccList = resultElement.EDocCc.Split(",")
                    For Each cc As String In ccList
                        mailMessage.CC.Add(New System.Net.Mail.MailAddress(cc.Trim))
                    Next
                End If

                mailMessage.Headers.Add("Content-Transfer-Encoding", "7bit")

                Dim body As String = "#sid=" + resultElement.EDocServiceId + vbCrLf _
                    + "#passwd=" + resultElement.EDocPassword.Trim + vbCrLf _
                    + "#faxno=" + resultElement.AtesakiFaxNo.Trim + vbCrLf _
                    + "#transcd=T" + resultElement.TokuisakiCode.Trim + vbCrLf _
                    + "#groupcd=" + resultElement.LoginId + vbCrLf _
                    + "#dividecd=" + resultElement.EigyosyoCode.Trim + resultElement.BukaCode.Trim + vbCrLf _
                    + "#noticeclass=" + resultElement.EDocNoticeClass + vbCrLf _
                    + "#noticeaddress=" + resultElement.NoticeAddress + vbCrLf _
                    + "#faxmode=" + resultElement.EDocFaxMode + vbCrLf _
                    + "#noticesubject=" + "mitsumo_" + resultElement.KakakuNyuuryokuNo.Trim + "_" + resultElement.TokuisakiCode.Trim + vbCrLf _
                    + "#freeitem=" + "mitsumo_" + resultElement.KakakuNyuuryokuNo.Trim + "_" + tokuisakiName.Trim + vbCrLf

                'mailMessage.Body = body
                'mailMessage.IsBodyHtml = False

                Dim altView As AlternateView = AlternateView.CreateAlternateViewFromString(body, myEnc, System.Net.Mime.MediaTypeNames.Text.Plain)
                altView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit
                mailMessage.AlternateViews.Add(altView)

                Dim attach As New System.Net.Mail.Attachment(filePath, MediaTypeNames.Application.Pdf)
                Dim disposition As ContentDisposition = attach.ContentDisposition
                disposition.FileName = EncordB(fileName)
                mailMessage.Attachments.Add(attach)

                'gmailのSMTPサーバの設定
                smtpClient.Host = mailInfo.MailHost
                smtpClient.Port = Integer.Parse(mailInfo.MailPort)
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                'ユーザー名,パスワード
                smtpClient.Credentials = New System.Net.NetworkCredential(mailInfo.MailUser, mailInfo.MailPass)
                'SSL
                smtpClient.EnableSsl = False
                smtpClient.Timeout = 10000
                smtpClient.Send(mailMessage)
            Catch ex As Exception
                errorList.Add(ex.Message)
            Finally
                mailMessage.Dispose()
                smtpClient.Dispose()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 更新処理
        ''' </summary>
        ''' <param name="resultElement"></param>
        ''' <returns></returns>
        Public Function Update(ByVal resultElement As MTM03SearchResultElement, ByVal loginId As String) As List(Of String)
            Dim errorList As New List(Of String)

            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction

                        command.CommandText = "UPDATE MTM10R002KAKAKU SET "
                        command.CommandText += "MTMR002087 = @MTMR002087" _              '見積書送信日
                                + ", MTMR002088 = @MTMR002088" _                         '見積書送付ログインID
                                + " WHERE MTMR002001 = @MTMR002001" _                    '得意先コード
                                + " AND MTMR002080 = @MTMR002080" _                      '価格入力番号
                                + " AND ISNULL(MTMR002085, 0) <> 0" _                    '確定済
                                + " AND ISNULL(MTMR002086, 0) = 0" _                     '印刷する
                                + " AND LTRIM(RTRIM(ISNULL(MTMR002087, ''))) = ''" _     '未送信
                                + " AND ISNULL(MTMR002032, 0) <> 0"                      '売更新日あり

                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR002087", Date.Now.ToString("yyyyMMdd")))
                        command.Parameters.Add(New SqlParameter("@MTMR002088", loginId))
                        command.Parameters.Add(New SqlParameter("@MTMR002001", resultElement.TokuisakiCode))
                        command.Parameters.Add(New SqlParameter("@MTMR002080", resultElement.KakakuNyuuryokuNo))
                        command.ExecuteNonQuery()

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
        ''' コード変換
        ''' </summary>
        ''' <param name="strTarget"></param>
        ''' <returns></returns>
        Private Function EncordB(ByVal strTarget As String) As String
            Dim arrbJisSubject() As Byte    ' iso-2022-jp に変換した件名
            Dim strEncSubject As String     ' B エンコードした件名
            ' 件名を iso-2022-jp に変換します。
            arrbJisSubject = System.Text.ASCIIEncoding.GetEncoding("iso-2022-jp").GetBytes(strTarget)
            ' iso-2022-jp に変換した文字列を Base64 エンコードし、エンコードを表す文字列を追加します。
            strEncSubject = "=?ISO-2022-JP?B?" & Convert.ToBase64String(arrbJisSubject) & "?="
            Return strEncSubject
        End Function
        ''' <summary>
        ''' バイト桁数
        ''' </summary>
        ''' <param name="checkTarget"></param>
        ''' <returns>Boolean</returns>
        Public Function StringNumberByteDigitsLenB(checkTarget As String) As Integer
            Dim reBoole As Boolean = False
            Dim LenB As Integer = 0

            '半角かなコンバート
            If (Not String.IsNullOrEmpty(checkTarget)) Then
                Dim sjis As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                Dim tempLen As Integer = sjis.GetByteCount(checkTarget)
                LenB = tempLen
            End If

            Return LenB

        End Function
    End Class
    Public Class MTM03SearchCondition
        ''' <summary>
        ''' 出力対象の指定
        ''' </summary>
        ''' <returns></returns>
        Public Property OutputNum As String = ""
        ''' <summary>
        ''' 送信区分
        ''' </summary>
        ''' <returns></returns>
        Public Property SendNum As String = ""
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
            Me.OutputNum = ""
            Me.SendNum = ""
            Me.KakakuNyuuryokuNo = ""
            Me.LoginId = ""
            Me.EigyosyoCodeFrom = ""
            Me.EigyosyoCodeTo = ""
            Me.BukaCodeFrom = ""
            Me.BukaCodeTo = ""
            Me.TantousyaCodeFrom = ""
            Me.TantousyaCodeTo = ""
            Me.TokuisakiCodeFrom = ""
            Me.TokuisakiCodeTo = ""
            Me.EigyosyoCodeList.Clear()
            Me.BukaCodeList.Clear()
            Me.TantousyaCodeList.Clear()
            Me.TokuisakiCodeList.Clear()
        End Sub
    End Class
    Public Class MTM03SearchResult
        Public Property ElementList As New List(Of MTM03SearchResultElement)
    End Class

    Public Class MTM03SearchResultElement

        Public Property TokuisakiCode As String = ""

        Public Property TokuisakiName1 As String = ""

        Public Property TokuisakiName2 As String = ""

        Public Property TokuisakiFaxNo As String = ""

        Public Property KakakuNyuuryokuName As String = ""

        Public Property JitsukouKakakuNyuuryokuName As String = ""

        Public Property KakakuNyuuryokuNo As String = ""

        Public Property NeageDate As String = ""

        Public Property Unchin As String = ""

        Public Property Kaiteijitsusi As String = ""

        Public Property KaisyaName As String = ""

        Public Property EigyosyoCode As String = ""

        Public Property EigyosyoName As String = ""

        Public Property BukaCode As String = ""

        Public Property TantoName As String = ""

        Public Property AtesakiName As String = ""

        Public Property AtesakiFaxNo As String = ""

        Public Property PostCode As String = ""

        Public Property Address As String = ""

        Public Property PhoneFax As String = ""

        Public Property SoushinKubun As String = ""

        Public Property LoginId As String = ""

        Public Property MailTo As String = ""

        Public Property MailFrom As String = ""

        Public Property EDocFrom As String = ""

        Public Property EDocTo As String = ""

        Public Property EDocCc As String = ""

        Public Property EDocBCc As String = ""

        Public Property EDocServiceId As String = ""

        Public Property EDocPassword As String = ""

        Public Property EDocNoticeClass As String = ""

        Public Property EDocFaxMode As String = ""

        Public Property NoticeAddress As String = ""

        Public Property ElementDetailList As New List(Of MTM03SearchResultElementDetail)
    End Class

    Public Class MTM03SearchResultElementDetail
        Public Property SyohinName As String = ""

        Public Property Irisu As String = ""

        Public Property Tanni As String = ""

        Public Property Lot As String = ""

        Public Property NewTanka As String = ""

        Public Property OldTanka As String = ""

        Public Property SaisyuDate As String = ""

        Public Property Bikou As String = ""

        Public Property NohinHistory As String = ""

        Public Property SyohinCode As String = ""

        Public Property Kikaku As String = ""
    End Class

    Public Class MTM03MailInfo

        Public Property MailHost As String = ""

        Public Property MailPort As String = ""

        Public Property MailUser As String = ""

        Public Property MailPass As String = ""
    End Class

    Public Class MTM03PreviewData
        Public Property ErrorList As New List(Of String)

        Public Property FilePath As String = ""

        Public Property SearchResult As MTM03SearchResult
    End Class
    Public Class MTM03SendMailData
        Public Property ErrorList As New List(Of String)

        Public Property SearchResult As MTM03SearchResult
    End Class

    Public Class PDFCreateCountResult
        Public Property ElementList As New List(Of PDFCreateCountResultElement)
    End Class

    Public Class PDFCreateCountResultElement

        Public Property SyohinCode As String = ""

        Public Property Kikaku As String = ""
        Public Property TopNouhinHistory As String = ""

        Public Property LineCount As Integer = 0
        Public Property PageMin As Integer = 0
        Public Property PageMax As Integer = 0
        Public Property PageNo As Integer = 0
    End Class
End Namespace
