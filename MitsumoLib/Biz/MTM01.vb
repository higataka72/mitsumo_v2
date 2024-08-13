Imports System
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Namespace Biz
    ''' <summary>
    ''' 単価入力シート取込ビジネスロジック
    ''' </summary>
    Public Class MTM01
        Inherits BaseBiz

        Public Property Password As String = ""

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 価格入力番号取得
        ''' </summary>
        ''' <returns>String</returns>
        Public Function GetCostInputNumber() As String
            Dim number As String = ""

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM001003 FROM MTM00M001SEIGYO WHERE MTMM001002 = @MTMM001002"
                    command.Parameters.Add(New SqlParameter("@MTMM001002", Me.Password))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        number = reader.Item("MTMM001003").ToString()
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return number
        End Function
        ''' <summary>
        ''' バリデーション前処理(Inport用)
        ''' </summary>
        ''' <param name="mtm10r003jitsukou"></param>
        ''' <returns>List</returns>
        Public Function ValidateBeforeImport(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU) As List(Of String)
            Dim errorList As New List(Of String)
            Dim dbl As Double

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003001) Then
                errorList.Add("価格入力番号が空です")
            End If

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003002) Then
                errorList.Add("価格入力名称が空です")
            End If

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003003) Then
                errorList.Add("ログインIDが空です")
            End If
            If Not String.IsNullOrEmpty(mtm10r003jitsukou.MTMR003005_2) Then
                If Double.TryParse(mtm10r003jitsukou.MTMR003005_2, dbl) Then
                    If (dbl < 0) Then errorList.Add("一斉送信時間の数値が0-24ではありません")
                    If (dbl > 24) Then errorList.Add("一斉送信時間の数値が0-24ではありません")
                Else
                    errorList.Add("一斉送信時間が数値ではありません")
                End If
            End If

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003006) Then
                errorList.Add("更新日が空です")
            End If

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003006_2) Then
                errorList.Add("更新時間が空です")
            End If
            If Not String.IsNullOrEmpty(mtm10r003jitsukou.MTMR003006_2) Then
                If Double.TryParse(mtm10r003jitsukou.MTMR003006_2, dbl) Then
                    If (dbl < 0) Then errorList.Add("更新時間の数値が0-24ではありません")
                    If (dbl > 24) Then errorList.Add("更新時間の数値が0-24ではありません")
                Else
                    errorList.Add("一斉送信時間が数値ではありません")
                End If
            End If

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003007) Then
                errorList.Add("締切日が空です")
            End If

            'If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003009) Then
            '    errorList.Add("案内文が空です")
            'End If

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003010) Then
                errorList.Add("入力元が空です")
            End If

            Return errorList
        End Function
        ''' <summary>
        ''' バリデーション前処理(Delete用)
        ''' </summary>
        ''' <param name="mtm10r003jitsukou"></param>
        ''' <returns>List</returns>
        Public Function ValidateBeforeDelete(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU) As List(Of String)
            Dim errorList As New List(Of String)

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003001) Then
                errorList.Add("価格入力番号が空です")
            End If

            Return errorList
        End Function
        ''' <summary>
        ''' 一括取込処理
        ''' </summary>
        ''' <param name="mtm10r003jitsukou"></param>
        ''' <param name="table"></param>
        ''' <returns>List</returns>
        Public Function Import(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU, ByVal table As DataTable, ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)
            Dim dtStart = DateTime.Now

            Try
                Me.connection.Open()

                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        command.CommandText = "DELETE FROM MTM10R001TANKA"
                        command.ExecuteNonQuery()

                        Dim bulkcopy As New SqlBulkCopy(Me.connection, SqlBulkCopyOptions.KeepIdentity, transaction)
                        bulkcopy.DestinationTableName = "MTM10R001TANKA"
                        bulkcopy.WriteToServer(table)

                        command.CommandText = "INSERT INTO MTM10R002KAKAKU (" _
                                            + "  MTMR002001" _ '得意先コード
                                            + ", MTMR002002" _ '商品コード
                                            + ", MTMR002003" _ '規格
                                            + ", MTMR002004" _ '数量・上限
                                            + ", MTMR002005" _ '得意先名
                                            + ", MTMR002006" _ 'ランクコード
                                            + ", MTMR002007" _ 'ランク
                                            + ", MTMR002008" _ '営業所コード
                                            + ", MTMR002009" _ '営業所
                                            + ", MTMR002010" _ '部課コード
                                            + ", MTMR002011" _ '部課名
                                            + ", MTMR002012" _ '担当者コード
                                            + ", MTMR002013" _ '担当者名
                                            + ", MTMR002014" _ '仕入先コード
                                            + ", MTMR002015" _ '仕入先名
                                            + ", MTMR002016" _ '商品名
                                            + ", MTMR002017" _ 'ロット
                                            + ", MTMR002018" _ '行番号
                                            + ", MTMR002019" _ '入数
                                            + ", MTMR002020" _ '単位
                                            + ", MTMR002021" _ '手配
                                            + ", MTMR002022" _ '改定前売単価
                                            + ", MTMR002023" _ '改定前仕入単価
                                            + ", MTMR002024" _ '改定前粗利率
                                            + ", MTMR002025" _ '現売上単価
                                            + ", MTMR002026" _ '現売㎡単価
                                            + ", MTMR002027" _ '現仕入単価
                                            + ", MTMR002028" _ '現仕㎡単価
                                            + ", MTMR002029" _ '現粗利率
                                            + ", MTMR002030" _ '新売上単価
                                            + ", MTMR002031" _ '新売㎡単価
                                            + ", MTMR002032" _ '売実施日
                                            + ", MTMR002033" _ '新仕入単価
                                            + ", MTMR002034" _ '新仕㎡単価
                                            + ", MTMR002035" _ '仕実施日
                                            + ", MTMR002036" _ '新粗利率
                                            + ", MTMR002037" _ '値上率
                                            + ", MTMR002038" _ '仕入先コメント
                                            + ", MTMR002039" _ '数量
                                            + ", MTMR002040" _ '売上回数
                                            + ", MTMR002041" _ '売上金額
                                            + ", MTMR002042" _ '粗利金額
                                            + ", MTMR002043" _ '改定後売上金額
                                            + ", MTMR002044" _ '改定後粗利金額
                                            + ", MTMR002045" _ '社内摘要
                                            + ", MTMR002046" _ '発注摘要
                                            + ", MTMR002047" _ '運賃摘要
                                            + ", MTMR002048" _ '見積書商品名
                                            + ", MTMR002049" _ '最終売上日
                                            + ", MTMR002050" _ '最終売上単価
                                            + ", MTMR002051" _ '最終売上数量
                                            + ", MTMR002052" _ '最終納品先
                                            + ", MTMR002053" _ '納品先履歴（過去18カ月）
                                            + ", MTMR002054" _ '得意先FAX
                                            + ", MTMR002055" _ '売価変更
                                            + ", MTMR002056" _ '仕価変更
                                            + ", MTMR002057" _ '商品名(比較用)
                                            + ", MTMR002058" _ '商品名
                                            + ", MTMR002059" _ 'ｽﾘｯﾄ
                                            + ", MTMR002060" _ '#
                                            + ", MTMR002061" _ '厚み
                                            + ", MTMR002062" _ '巾1
                                            + ", MTMR002063" _ '巾2
                                            + ", MTMR002064" _ '長さ
                                            + ", MTMR002065" _ 'M
                                            + ", MTMR002066" _ '+表示
                                            + ", MTMR002067" _ 'ｽﾘｯﾄ
                                            + ", MTMR002068" _ '備考
                                            + ", MTMR002069" _ '㎡計算
                                            + ", MTMR002070" _ '改定単価
                                            + ", MTMR002071" _ '値上がり率
                                            + ", MTMR002072" _ '備考（見積書）
                                            + ", MTMR002073" _ '宛先名
                                            + ", MTMR002074" _ 'ログインID
                                            + ", MTMR002075" _ 'もりや担当者メールアドレス
                                            + ", MTMR002076" _ '宛先送信区分
                                            + ", MTMR002077" _ 'FAX番号
                                            + ", MTMR002078" _ 'メールアドレス
                                            + ", MTMR002079" _ '宛先名
                                            + ", MTMR002080" _ '価格入力番号
                                            + ", MTMR002081" _ '価格入力名
                                            + ", MTMR002082" _ '取込年月日
                                            + ", MTMR002083" _ '取込担当者コード
                                            + ", MTMR002084"   '納品先履歴カットフラグ
                        command.CommandText += ") SELECT " _
                                            + "  TNK.MTMR001003" _                                                                              '得C (MTMR002001)
                                            + ", TNK.MTMR001013" _                                                                              '商品C (MTMR002002)
                                            + ", TNK.MTMR001015" _                                                                              '規格 (MTMR002003)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001016 <> '' THEN TNK.MTMR001016 ELSE '0' END)" _    '数量・上限 (MTMR002004)
                                            + ", TNK.MTMR001004" _                                                                              '得意先名 (MTMR002005)
                                            + ", TNK.MTMR001005" _                                                                              'ﾗﾝｸC (MTMR002006)
                                            + ", TNK.MTMR001006" _                                                                              'ﾗﾝｸ (MTMR002007)
                                            + ", TNK.MTMR001007" _                                                                              '営C (MTMR002008)
                                            + ", TNK.MTMR001008" _                                                                              '営業所 (MTMR002009)
                                            + ", TNK.MTMR001009" _                                                                              '部課C (MTMR002010)
                                            + ", TNK.MTMR001010" _                                                                              '部課名 (MTMR002011)
                                            + ", TNK.MTMR001011" _                                                                              '担C (MTMR002012)
                                            + ", TNK.MTMR001012" _                                                                              '担当者名 (MTMR002013)
                                            + ", TNK.MTMR001055" _                                                                              '仕入先C (MTMR002014)
                                            + ", TNK.MTMR001056" _                                                                              '仕入先名 (MTMR002015)
                                            + ", TNK.MTMR001014" _                                                                              '商品名 (MTMR002016)
                                            + ", TNK.MTMR001017" _                                                                              'ロット (MTMR002017)
                                            + ", CONVERT(decimal, CASE WHEN TNK.MTMR001018 <> '' THEN TNK.MTMR001018 ELSE '0' END)" _           '行番号 (MTMR002018)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001019 <> '' THEN TNK.MTMR001019 ELSE '0' END)" _    '入数 (MTMR002019)
                                            + ", TNK.MTMR001020" _                                                                              '単位 (MTMR002020)
                                            + ", TNK.MTMR001021" _                                                                              '手配 (MTMR002021)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001022 <> '' THEN TNK.MTMR001022 ELSE '0' END)" _    '改定前売単価 (MTMR002022)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001023 <> '' THEN TNK.MTMR001023 ELSE '0' END)" _    '改定前仕入単価 (MTMR002023)
                                            + ", CONVERT(decimal(5, 1), CASE WHEN TNK.MTMR001024 <> '' THEN TNK.MTMR001024 ELSE '0' END)" _     '改定前粗利率 (MTMR002024)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001025 <> '' THEN TNK.MTMR001025 ELSE '0' END)" _    '現売単価 (MTMR002025)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001026 <> '' THEN TNK.MTMR001026 ELSE '0' END)" _    '現売㎡単価 (MTMR002026)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001027 <> '' THEN TNK.MTMR001027 ELSE '0' END)" _    '現仕入単価 (MTMR002027)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001028 <> '' THEN TNK.MTMR001028 ELSE '0' END)" _    '現仕㎡単価 (MTMR002028)             
                                            + ", CONVERT(decimal(5, 1), CASE WHEN TNK.MTMR001029 <> '' THEN TNK.MTMR001029 ELSE '0' END)" _     '現粗利率 (MTMR002029)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001030 <> '' THEN TNK.MTMR001030 ELSE '0' END)" _    '新売単価 (MTMR002030)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001031 <> '' THEN TNK.MTMR001031 ELSE '0' END)" _    '新売㎡単価 (MTMR002031)
                                            + ", CONVERT(decimal, CASE WHEN TNK.MTMR001032 <> '' THEN TNK.MTMR001032 ELSE '0' END)" _           '売実施日 (MTMR002032)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001033 <> '' THEN TNK.MTMR001033 ELSE '0' END)" _    '新仕入単価 (MTMR002033)  
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001034 <> '' THEN TNK.MTMR001034 ELSE '0' END)" _    '新仕㎡単価 (MTMR002034)
                                            + ", CONVERT(decimal, CASE WHEN TNK.MTMR001035 <> '' THEN TNK.MTMR001035 ELSE '0' END)" _           '仕実施日 (MTMR002035)
                                            + ", CONVERT(decimal(5, 1), CASE WHEN TNK.MTMR001036 <> '' THEN TNK.MTMR001036 ELSE '0' END)" _     '新粗利率 (MTMR002036)
                                            + ", CONVERT(decimal(5, 1), CASE WHEN TNK.MTMR001037 <> '' THEN TNK.MTMR001037 ELSE '0' END)" _     '値上率 (MTMR002037)
                                            + ", TNK.MTMR001038" _                                                                              '仕入先コメント (MTMR002038)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001039 <> '' THEN TNK.MTMR001039 ELSE '0' END)" _    '数量 (MTMR002039)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001040 <> '' THEN TNK.MTMR001040 ELSE '0' END)" _    '売上回数 (MTMR002040)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001041 <> '' THEN TNK.MTMR001041 ELSE '0' END)" _    '売上金額 (MTMR002041)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001042 <> '' THEN TNK.MTMR001042 ELSE '0' END)" _    '粗利金額 (MTMR002042)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001043 <> '' THEN TNK.MTMR001043 ELSE '0' END)" _    '改定後売上金額 (MTMR002043)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001044 <> '' THEN TNK.MTMR001044 ELSE '0' END)" _    '改定後粗利金額 (MTMR002044)
                                            + ", TNK.MTMR001045" _                                                                              '社内適用 (MTMR002045)
                                            + ", TNK.MTMR001046" _                                                                              '発注適用 (MTMR002046)
                                            + ", TNK.MTMR001047" _                                                                              '運賃適用 (MTMR002047)
                                            + ", TNK.MTMR001048" _                                                                              '見積書商品名 (MTMR002048)
                                            + ", CONVERT(decimal, TNK.MTMR001049)" _                                                            '最終売上日 (MTMR002049)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001050 <> '' THEN TNK.MTMR001050 ELSE '0' END)" _    '最終売上単価 (MTMR002050)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001051 <> '' THEN TNK.MTMR001051 ELSE '0' END)" _    '最終売上数量 (MTMR002051)
                                            + ", TNK.MTMR001052" _                                                                              '最終納品先 (MTMR002052)
                                            + ", TNK.MTMR001053" _                                                                              '納品先履歴 (MTMR002053)
                                            + ", TNK.MTMR001054" _                                                                              '得意先FAX (MTMR002054)
                                            + ", TNK.MTMR001057" _                                                                              '売価変更 (MTMR002055)
                                            + ", TNK.MTMR001058" _                                                                              '仕価変更 (MTMR002056)
                                            + ", TNK.MTMR001059" _                                                                              '商品名(比較用) (MTMR002057)
                                            + ", TNK.MTMR001060" _                                                                              '商品名 (MTMR002058)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001061 <> '' THEN TNK.MTMR001061 ELSE '0' END)" _    'ｽﾘｯﾄ (MTMR002059)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001062 <> '' THEN TNK.MTMR001062 ELSE '0' END)" _    '# (MTMR002060)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001063 <> '' THEN TNK.MTMR001063 ELSE '0' END)" _    '厚み (MTMR002061)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001065 <> '' THEN TNK.MTMR001065 ELSE '0' END)" _    '巾1 (MTMR002062)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001067 <> '' THEN TNK.MTMR001067 ELSE '0' END)" _    '巾2 (MTMR002063)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001069 <> '' THEN TNK.MTMR001069 ELSE '0' END)" _    '長さ (MTMR002064)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001070 <> '' THEN TNK.MTMR001070 ELSE '0' END)" _    'M (MTMR002065)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001071 <> '' THEN TNK.MTMR001071 ELSE '0' END)" _    '+表示 (MTMR002066)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001072 <> '' THEN TNK.MTMR001072 ELSE '0' END)" _    'ｽﾘｯﾄ (MTMR002067)
                                            + ", TNK.MTMR001073" _                                                                              '備考 (MTMR002068)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001074 <> '' THEN TNK.MTMR001074 ELSE '0' END)" _    '㎡計算 (MTMR002069)
                                            + ", CONVERT(decimal(19, 4), CASE WHEN TNK.MTMR001075 <> '' THEN TNK.MTMR001075 ELSE '0' END)" _    '改定単価 (MTMR002070)
                                            + ", CONVERT(decimal(5, 1), CASE WHEN TNK.MTMR001076 <> '' THEN TNK.MTMR001076 ELSE '0' END)" _     '値上がり率 (MTMR002071)
                                            + ", TNK.MTMR001077" _                                                                              '備考（見積書） (MTMR002072)
                                            + ", '－'" _                                                                                        '宛先名  (MTMR002073)
                                            + ", ISNULL(TNT.HANM004K012, '')" _                                                                 '担当者M.担当者コード  (MTMR002074)
                                            + ", ISNULL(TNT.HANM004K011, '')" _                                                                 '担当者M.拡張項目１(メールアドレス) (MTMR002075)
                                            + ", CONVERT(decimal, ISNULL(ATS.CUSMB04002, 2))" _                                                 '宛先マスタ.宛先送信初期値 (MTMR002076)
                                            + ", IIF(ISNULL(ATS.CUSMB04005, '') = '', TNK.MTMR001054, ATS.CUSMB04005)" _                                                                  '宛先マスタ.FAX番号 (MTMR002077)
                                            + ", ISNULL(ATS.CUSMB04004, ISNULL(TNT.HANM004K001, ''))" _                                         '宛先マスタ.メールアドレス/担当者メールアドレス (MTMR002078)
                                            + ", ATS.CUSMB04003" _                                                                              '宛先マスタ.宛先名 (MTMR002079)
                                            + ", CONVERT(decimal, @MTMR003001)" _                                                               '画面.価格入力番号 (MTMR002080)
                                            + ", @MTMR003002" _                                                                                 '画面.価格入力名 (MTMR002081)
                                            + ", CONVERT(decimal, @SYSTEMDATE)" _                                                               'システム日付 (MTMR002082)
                                            + ", @MTMR003003" _                                                                                 '画面.取込担当者コード (MTMR002083)
                                            + ",IIF(DATALENGTH(RTRIM(TNK.MTMR001053))>100, '1', '0') "                                          '判定.納品先履歴カットフラグ (MTMR002084)
                        command.CommandText += " FROM MTM10R001TANKA TNK" _
                                            + " LEFT JOIN HAN10M004TANTO TNT ON RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(TNK.MTMR001011)), 8) = RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(TNT.HANM004001)), 8)" _
                                            + " LEFT JOIN CUS88MB04MITSUMOATESAKI ATS ON RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(TNK.MTMR001003)), 8) = RIGHT('00000000' + CONVERT(NVARCHAR, RTRIM(ATS.CUSMB04001)), 8)"

                        command.Parameters.Add(New SqlParameter("@MTMR003001", mtm10r003jitsukou.MTMR003001))
                        command.Parameters.Add(New SqlParameter("@MTMR003002", mtm10r003jitsukou.MTMR003002))
                        command.Parameters.Add(New SqlParameter("@MTMR003003", mtm10r003jitsukou.MTMR003003))
                        command.Parameters.Add(New SqlParameter("@SYSTEMDATE", DateTime.Now.ToString("yyyyMMdd")))
                        command.ExecuteNonQuery()

                        command.CommandText = "INSERT INTO MTM10R003JITSUKOU (" _
                            + "MTMR003001" _   '価格入力番号
                            + ", MTMR003002" _ '価格入力名
                            + ", MTMR003003" _ '取込担当者コード
                            + ", MTMR003004" _ '取込年月日
                            + ", MTMR003005" _ '一斉送信日時
                            + ", MTMR003006" _ '更新日時
                            + ", MTMR003007" _ '締切日
                            + ", MTMR003008" _ '運賃の指定
                            + ", MTMR003009" _ '案内文
                            + ", MTMR003010" _ '取込元ファイル名
                            + ", MTMR003011" _ '値下前売単価
                            + ", MTMR003012" _ '値下前仕入単価
                            + ", MTMR003013" _ '値下前粗利率
                            + ", MTMR003014" _ '現売㎡単価
                            + ", MTMR003015" _ '現仕㎡単価
                            + ", MTMR003016" _ '新売㎡単価
                            + ", MTMR003017" _ '新仕㎡単価
                            + ", MTMR003018" _ '取込件数
                            + ", MTMR003019" _ '取込開始時間
                            + ", MTMR003020" _ '取込終了時間
                            + ", MTMR003021" _ '改定実施日
                            + ") VALUES (" _
                            + "CONVERT(decimal, @MTMR003001)" _
                            + ", @MTMR003002" _
                            + ", @MTMR003003" _
                            + ", @MTMR003004" _
                            + ", @MTMR003005" _
                            + ", @MTMR003006" _
                            + ", @MTMR003007" _
                            + ", @MTMR003008" _
                            + ", @MTMR003009" _
                            + ", @MTMR003010" _
                            + ", @MTMR003011" _
                            + ", @MTMR003012" _
                            + ", @MTMR003013" _
                            + ", @MTMR003014" _
                            + ", @MTMR003015" _
                            + ", @MTMR003016" _
                            + ", @MTMR003017" _
                            + ", @MTMR003018" _
                            + ", @MTMR003019" _
                            + ", @MTMR003020" _
                            + ", @MTMR003021" _
                            + ")"
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR003001", mtm10r003jitsukou.MTMR003001))
                        command.Parameters.Add(New SqlParameter("@MTMR003002", mtm10r003jitsukou.MTMR003002))
                        command.Parameters.Add(New SqlParameter("@MTMR003003", mtm10r003jitsukou.MTMR003003))
                        '取込年月日はシステム日付を設定
                        mtm10r003jitsukou.MTMR003004 = DateTime.Now.ToString("yyyyMMdd")
                        command.Parameters.Add(New SqlParameter("@MTMR003004", mtm10r003jitsukou.MTMR003004))
                        command.Parameters.Add(New SqlParameter("@MTMR003005", mtm10r003jitsukou.MTMR003005 + mtm10r003jitsukou.MTMR003005_2))
                        command.Parameters.Add(New SqlParameter("@MTMR003006", mtm10r003jitsukou.MTMR003006 + mtm10r003jitsukou.MTMR003006_2))
                        command.Parameters.Add(New SqlParameter("@MTMR003007", mtm10r003jitsukou.MTMR003007))
                        command.Parameters.Add(New SqlParameter("@MTMR003008", mtm10r003jitsukou.MTMR003008))
                        command.Parameters.Add(New SqlParameter("@MTMR003009", mtm10r003jitsukou.MTMR003009))
                        command.Parameters.Add(New SqlParameter("@MTMR003010", mtm10r003jitsukou.MTMR003010))
                        command.Parameters.Add(New SqlParameter("@MTMR003011", mtm10r003jitsukou.MTMR003011))
                        command.Parameters.Add(New SqlParameter("@MTMR003012", mtm10r003jitsukou.MTMR003012))
                        command.Parameters.Add(New SqlParameter("@MTMR003013", mtm10r003jitsukou.MTMR003013))
                        command.Parameters.Add(New SqlParameter("@MTMR003014", mtm10r003jitsukou.MTMR003014))
                        command.Parameters.Add(New SqlParameter("@MTMR003015", mtm10r003jitsukou.MTMR003015))
                        command.Parameters.Add(New SqlParameter("@MTMR003016", mtm10r003jitsukou.MTMR003016))
                        command.Parameters.Add(New SqlParameter("@MTMR003017", mtm10r003jitsukou.MTMR003017))
                        command.Parameters.Add(New SqlParameter("@MTMR003018", table.Rows.Count))
                        command.Parameters.Add(New SqlParameter("@MTMR003019", dtStart.ToString("HHmmss")))
                        command.Parameters.Add(New SqlParameter("@MTMR003020", DateTime.Now.ToString("HHmmss")))
                        command.Parameters.Add(New SqlParameter("@MTMR003021", mtm10r003jitsukou.MTMR003021))
                        command.ExecuteNonQuery()

                        '制御マスタ更新（価格入力番号の採番、取込ファイル）
                        Dim numNext As Decimal = 0
                        numNext = Decimal.Parse(mtm10r003jitsukou.MTMR003001) + 1
                        command.CommandText = "UPDATE MTM00M001SEIGYO SET MTMM001003 = @MTMM001003, MTMM001011 = @MTMM001011 "
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMM001003", numNext))
                        command.Parameters.Add(New SqlParameter("@MTMM001011", mtm10r003jitsukou.MTMR003010))
                        command.ExecuteNonQuery()

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        sysError = ex.Message
                        errorList.Add(ex.Message)
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function
        ''' <summary>
        ''' 一括取消処理
        ''' </summary>
        ''' <param name="mtm10r003jitsukou"></param>
        ''' <param name="sysError"></param>
        ''' <returns>List</returns>
        Public Function Delete(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU, ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)

            Try
                Me.connection.Open()

                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction

                        '実行管理テーブル削除
                        command.CommandText = "DELETE FROM MTM10R003JITSUKOU WHERE MTMR003001 = @MTMR003001"
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR003001", mtm10r003jitsukou.MTMR003001))
                        command.ExecuteNonQuery()

                        '価格入力テーブル削除
                        command.CommandText = "DELETE FROM MTM10R002KAKAKU WHERE MTMR002080 = @MTMR002080"
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR002080", mtm10r003jitsukou.MTMR003001))
                        command.ExecuteNonQuery()


                    Catch ex As Exception
                        transaction.Rollback()
                        sysError = ex.Message
                        Throw ex
                    Finally
                        transaction.Commit()
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 価格入力データ重複チェック（プライマリーチェック）
        ''' </summary>
        ''' <returns>True:存在, Flase:存在しない</returns>
        Public Function KakakuDataCheck(mtm10r001tanka As Models.MTM10R001TANKA, ByVal kakakuNum As Integer) As Boolean
            Dim count As String = ""
            Dim check As Boolean = False

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT COUNT(MTMR002001) as MTMR002001_COUNT FROM MTM10R002KAKAKU " _
                                        + "WHERE " _
                                        + "MTMR002001 = @MTMR002001 " _
                                        + "AND MTMR002002 = @MTMR002002 " _
                                        + "AND MTMR002003 = @MTMR002003 " _
                                        + "AND MTMR002004 = @MTMR002004 " _
                                        + "AND MTMR002080 = @MTMR002080 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002001", mtm10r001tanka.MTMR001003))
                    command.Parameters.Add(New SqlParameter("@MTMR002002", mtm10r001tanka.MTMR001013))
                    command.Parameters.Add(New SqlParameter("@MTMR002003", mtm10r001tanka.MTMR001015))
                    command.Parameters.Add(New SqlParameter("@MTMR002004", mtm10r001tanka.MTMR001016))
                    command.Parameters.Add(New SqlParameter("@MTMR002080", kakakuNum))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        count = reader.Item("MTMR002001_COUNT").ToString()
                        If (count <> "0") Then
                            check = True
                        End If
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return check
        End Function
        ''' <summary>
        ''' 実行管理テーブルより情報取得
        ''' </summary>
        ''' <param name="searchMTMR003001"></param>
        ''' <returns>Models.MTM10R003JITSUKOU</returns>
        Public Function KakakuDataCheck(searchMTMR003001 As String, ByRef reDataFlg As Boolean) As Models.MTM10R003JITSUKOU
            Dim reMTM10R003JITSUKOU As New MitsumoLib.Models.MTM10R003JITSUKOU
            Dim check As Boolean = False
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "JTK.MTMR003001, JTK.MTMR003002, JTK.MTMR003003, JTK.MTMR003004, JTK.MTMR003005, JTK.MTMR003006, JTK.MTMR003007, " _
                                        + "JTK.MTMR003008, JTK.MTMR003009, JTK.MTMR003010, JTK.MTMR003011, JTK.MTMR003012, JTK.MTMR003013, JTK.MTMR003014, " _
                                        + "JTK.MTMR003015, JTK.MTMR003016, JTK.MTMR003017, JTK.MTMR003021 " _
                                        + "FROM MTM10R003JITSUKOU JTK " _
                                        + "WHERE " _
                                        + "MTMR003001 = @MTMR003001 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR003001", searchMTMR003001))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reDataFlg = True
                        reMTM10R003JITSUKOU.MTMR003001 = reader.Item("MTMR003001").ToString()      '価格入力番号
                        reMTM10R003JITSUKOU.MTMR003002 = reader.Item("MTMR003002").ToString()      '価格入力名
                        reMTM10R003JITSUKOU.MTMR003003 = reader.Item("MTMR003003").ToString()      '取込担当者コード
                        reMTM10R003JITSUKOU.MTMR003005 = reader.Item("MTMR003005").ToString()      '一斉送信日時
                        reMTM10R003JITSUKOU.MTMR003006 = reader.Item("MTMR003006").ToString()      '更新日時
                        reMTM10R003JITSUKOU.MTMR003007 = reader.Item("MTMR003007").ToString()      '締切日
                        reMTM10R003JITSUKOU.MTMR003021 = reader.Item("MTMR003021").ToString()      '改定実施日
                        reMTM10R003JITSUKOU.MTMR003008 = reader.Item("MTMR003008").ToString()      '運賃の指定
                        reMTM10R003JITSUKOU.MTMR003009 = reader.Item("MTMR003009").ToString()      '案内文
                        reMTM10R003JITSUKOU.MTMR003010 = reader.Item("MTMR003010").ToString()      '取込元ファイル名
                        reMTM10R003JITSUKOU.MTMR003011 = reader.Item("MTMR003011").ToString()      '値下前売単価
                        reMTM10R003JITSUKOU.MTMR003012 = reader.Item("MTMR003012").ToString()      '値下前仕入単価
                        reMTM10R003JITSUKOU.MTMR003013 = reader.Item("MTMR003013").ToString()      '値下前粗利率
                        reMTM10R003JITSUKOU.MTMR003014 = reader.Item("MTMR003014").ToString()      '現売㎡単価
                        reMTM10R003JITSUKOU.MTMR003015 = reader.Item("MTMR003015").ToString()      '現仕㎡単価
                        reMTM10R003JITSUKOU.MTMR003016 = reader.Item("MTMR003016").ToString()      '新売㎡単価
                        reMTM10R003JITSUKOU.MTMR003017 = reader.Item("MTMR003017").ToString()      '新仕㎡単価
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return reMTM10R003JITSUKOU
        End Function
        ''' <summary>
        ''' 担当者名称の取得
        ''' </summary>
        ''' <param name="searchHANM004001"></param>
        ''' <returns>String</returns>
        Public Function UserNameSearch(searchHANM004001 As String) As String
            Dim reHANM004002 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANM004002 " _
                                        + "FROM HAN10M004TANTO " _
                                        + "WHERE " _
                                        + "HANM004001 = @HANM004001 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANM004001", searchHANM004001))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANM004002 = reader.Item("HANM004002").ToString()      '担当者名称

                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return reHANM004002
        End Function
        ''' <summary>
        ''' 日付分解
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function GetDate(setDate As String, ByRef reDate As DateTime) As Boolean
            Dim strdate As String
            Dim check As Boolean = False
            Try
                strdate = setDate.Trim
                If (Not String.IsNullOrEmpty(strdate)) Then
                    If (strdate.Length >= 8) Then
                        strdate = strdate.Substring(0, 4) + "/" + strdate.Substring(4, 2) + "/" + strdate.Substring(6, 2)
                        If (Date.TryParse(strdate, reDate)) Then
                            check = True
                        Else
                            reDate = ""
                        End If
                    End If
                End If
            Finally
                Me.connection.Close()
            End Try

            Return check
        End Function
        ''' <summary>
        ''' 取込ファイル名を取得（制御マスタ）
        ''' </summary>
        ''' <returns>String</returns>
        Public Function ImportFileNameSearch() As String
            Dim reMTMM001011 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM001011 FROM MTM00M001SEIGYO "
                    command.Parameters.Clear()
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMM001011 = reader.Item("MTMM001011").ToString()      '取込ファイル名
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return reMTMM001011
        End Function
        ''' <summary>
        ''' ファイルバックアップ
        ''' </summary>
        ''' <returns>String</returns>
        Public Function BackUpfileMove(targetFile As String) As Boolean
            Dim reMTMM001012 As String = ""
            Dim check As Boolean = False
            Dim dtToday As DateTime = DateTime.Now
            Dim strfileName As String

            Try
                '取込済データ退避フォルダ取得
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM001012 FROM MTM00M001SEIGYO "
                    command.Parameters.Clear()
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMM001012 = reader.Item("MTMM001012").ToString()      '取込済データ退避フォルダ
                    End If
                End Using

                '退避フォルダ存在チェック（作成）
                If (Not String.IsNullOrEmpty(reMTMM001012)) Then
                    If ((Directory.Exists(reMTMM001012) = False)) Then
                        'ディレクトリを作成
                        System.IO.Directory.CreateDirectory(reMTMM001012)
                    End If
                    If (File.Exists(targetFile) = True) Then
                        '退避ファイル名の生成
                        strfileName = Path.GetFileNameWithoutExtension(targetFile)
                        strfileName = strfileName + dtToday.ToString("yyyyMMddHHmmss") + ".txt"
                        '退避フォルダ移動
                        File.Move(targetFile, reMTMM001012 + "\\" + strfileName)
                        check = True
                    End If
                End If

            Catch ex As Exception
                check = False
                Return check
            Finally
                Me.connection.Close()
            End Try

            Return check
        End Function
        ''' <summary>
        ''' ファイル存在チェック
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function FileCheck(targetFile As String) As Boolean
            Dim check As Boolean = False

            Try
                If (File.Exists(targetFile) = True) Then
                    check = True
                End If
            Catch ex As Exception
                check = False
            End Try

            Return check
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
        ''' 数値チェック桁数(整数部、小数部）チェック
        ''' 小数部がない場合はcheckDigit2=0を指定
        ''' </summary>
        ''' <param name="checkTarget"></param>
        ''' <param name="checkDigit1"></param>
        ''' <param name="checkDigit2"></param>
        ''' <returns>Boolean</returns>
        Public Function NumericNumberDigitsCheck(checkTarget As String, checkDigit1 As Integer, checkDigit2 As Integer) As Boolean
            Dim check As Boolean = False
            Dim getDot As Integer = 0
            Dim getIntegerLength As Integer = 0
            Dim getDecimalLength As Integer = 0

            '数値チェック
            If IsNumeric(checkTarget) Then
                getIntegerLength = checkTarget.Length
                getDot = checkTarget.IndexOf(".")
                If (getDot = -1) Then
                    '整数部の桁数チェックのみ
                    If (getIntegerLength <= checkDigit1) Then
                        check = True
                    End If
                Else
                    '整数部の桁数取得
                    getIntegerLength = checkTarget.Substring(0, getDot).Length
                    '小数部の桁数
                    getDecimalLength = checkTarget.Substring(getDot + 1).Length

                    '整数部、小数部の桁数チェック
                    If (getIntegerLength <= checkDigit1) And ((getDecimalLength > 0) And (getDecimalLength <= checkDigit2)) Then
                        check = True
                    End If
                End If
            End If

            Return check

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
                reString = CutStrByteLen2(strConvert, 100)
                'reString = CutStrByteLen(strConvert, 100)
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
            Dim strTemp As String = sjis.GetString(bytTemp, 0, intLen) & Space(1) & "他"
            If strTemp.EndsWith(ControlChars.NullChar) OrElse strTemp.EndsWith("・") Then
                strTemp = sjis.GetString(bytTemp, 0, intLen - 1) + " 他"
            End If
            Return strTemp
        End Function
        ''' <summary>
        ''' 文字列バイト数変換
        ''' </summary>
        ''' <param name="strInput"></param>
        ''' <param name="intLen"></param>
        ''' <returns>String</returns>
        Public Function CutStrByteLen2(ByVal strInput As String, ByVal intLen As Integer) As String
            Dim InputString As String
            Dim Pos As Integer
            Dim Len As Integer
            Dim lenSum As Integer
            Dim sb As New Text.StringBuilder

            InputString = strInput

            For Pos = 0 To InputString.Length - 1
                Len = Encoding.GetEncoding("Shift_JIS").GetByteCount(InputString.Substring(Pos, 1))
                lenSum += Len
                If lenSum <= intLen Then
                    sb.Append(InputString.Substring(Pos, 1))
                End If
                If (lenSum = 101) Then
                    sb.Append(" 他")
                End If
            Next

            Return sb.ToString
        End Function

        ''' <summary>
        ''' 価格入力番号採番
        ''' </summary>
        ''' <param name="mtm10r003jitsukou"></param>
        ''' <returns>List</returns>
        Public Function UpdateKakakuNum(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU, ByRef numNext As Decimal) As List(Of String)
            Dim errorList As New List(Of String)
            Dim numOld As Decimal = 0

            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection

                        '価格入力番号の現在値を取得
                        command.CommandText = "SELECT MTMM001003 FROM MTM00M001SEIGYO"
                        Dim reader As SqlDataReader = command.ExecuteReader
                        If reader.Read = True Then
                            numOld = Decimal.Parse(reader.Item("MTMM001003").ToString())
                        End If

                        '採番
                        numNext = numOld + 1

                    Catch ex As Exception
                        transaction.Rollback()
                        errorList.Add(ex.Message)
                        numNext = 0
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function
        ''' <summary>
        ''' プレビュー用PDF出力(案内文のみ)
        ''' </summary>
        ''' <returns>String</returns>
        Public Function OutputPreviewAnnaiPdf(ByVal annaiFilePath As String, ByVal outputPath As String) As List(Of String)
            Dim errorList As New List(Of String)

            Try
                If Not System.IO.Directory.Exists(outputPath) Then
                    System.IO.Directory.CreateDirectory(outputPath)
                End If

                'ドキュメントを作成
                Dim doc As New Document(PageSize.A4.Rotate)
                Dim stream As New FileStream(outputPath + "/見積書.pdf", FileMode.Create)
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

                'ドキュメントを閉じる
                doc.Close()
            Catch ex As Exception
                Console.Write(ex.Message)
                errorList.Add(ex.Message)
            End Try

            Return errorList
        End Function
    End Class
End Namespace