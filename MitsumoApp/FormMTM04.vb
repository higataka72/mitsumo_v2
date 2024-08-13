﻿Imports System.Configuration
Imports MitsumoLib
Public Class FormMTM04
    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' システム間連携ビジネスロジック
    ''' </summary>
    Private ReadOnly Biz04 As Biz.MTM04
    Private ReadOnly Biz04_1 As Biz.MTM04_1
    Public ReadOnly SearchCondition As Biz.MTM02SearchCondition
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="formMenu"></param>
    Public Sub New(ByVal formMenu As FormMTMMenu)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.FormMenu = formMenu
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Dim connectionString2 = ConfigurationManager.ConnectionStrings("KIKAN_DB").ConnectionString
        Me.Biz04 = New Biz.MTM04(connectionString)
        Me.Biz04_1 = New Biz.MTM04_1(connectionString2)
        Me.SearchCondition = New Biz.MTM02SearchCondition
    End Sub
    ''' <summary>
    ''' 閉じるボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM04_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' 実行ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    <Obsolete>
    Private Sub ButtonExecute_Click(sender As Object, e As EventArgs) Handles ButtonExecute.Click

        '画面初期設定
        Dim txt001 As String = Me.TextBox001.Text.Trim
        Dim txt002 As String = Me.TextBox002.Text.Trim
        Dim txt003 As String = Me.TextBox003.Text.Trim
        Dim txt004 As String = Me.TextBox004.Text.Trim
        Dim txt005 As String = Me.TextBox005.Text.Trim
        Dim txt006 As String = Me.TextBox006.Text.Trim
        Dim txt007 As String = Me.TextBox007.Text.Trim
        Dim txt008 As String = Me.TextBox008.Text.Trim
        Dim txt009 As String = Me.TextBox009.Text.Trim
        Dim txt010 As String = Me.TextBox010.Text.Trim
        Dim txt011 As String = Me.TextBox011.Text.Trim
        Dim txt012 As String = Me.TextBox012.Text.Trim
        Dim txt013 As String = Me.TextBox013.Text.Trim
        Dim chk001 As Boolean = Me.CheckBox001.Checked
        Dim chk002 As Boolean = Me.CheckBox002.Checked

        Me.SearchCondition.Clear()
        Me.SearchCondition.KakakuNyuuryokuNo = txt001  '価格入力番号
        Me.SearchCondition.EigyosyoCode = txt007       '営業所コード
        Me.SearchCondition.TantousyaCodeFrom = txt003  '担当者コードFROM
        Me.SearchCondition.TantousyaCodeTo = txt005    '担当者コードTO
        Me.SearchCondition.TokuisakiCodeFrom = txt009  '得意先コードFROM
        Me.SearchCondition.TokuisakiCodeTo = txt011    '得意先コードTO

        Dim mtm10r004renkei = New MitsumoLib.Models.MTM10R004RENKEI
        '連携管理モデルへ設定
        mtm10r004renkei.MTMR004001 = 0                                  '連携番号(この時点は0，仮採番を設定予定)
        If String.IsNullOrWhiteSpace(txt001) Then
            MessageBox.Show("価格入力番号が空です")
            Exit Sub
        End If
        mtm10r004renkei.MTMR004002 = Integer.Parse(txt001)              '価格入力番号
        mtm10r004renkei.MTMR004003 = txt002                             '価格入力名
        mtm10r004renkei.MTMR004004 = ""                                 '出力対象
        If (chk001) And (chk002) Then
            mtm10r004renkei.MTMR004004 = "3"
        ElseIf chk001 = True Then
            mtm10r004renkei.MTMR004004 = "1"
        ElseIf chk002 = True Then
            mtm10r004renkei.MTMR004004 = "2"
        End If
        mtm10r004renkei.MTMR004005 = txt003                            '担当者コード開始
        mtm10r004renkei.MTMR004006 = txt005                            '担当者コード終了
        mtm10r004renkei.MTMR004007 = txt007                            '営業所コード
        mtm10r004renkei.MTMR004008 = txt009                            '得意先コード開始
        mtm10r004renkei.MTMR004009 = txt011                            '得意先コード終了
        mtm10r004renkei.MTMR004010 = DateTime.Now.ToString("yyyyMMdd") '出力年月日
        mtm10r004renkei.MTMR004011 = txt013                            '出力フォルダ名
        mtm10r004renkei.MTMR004012 = "0"                               '単価台帳
        mtm10r004renkei.MTMR004013 = "0"                               '見積書
        mtm10r004renkei.MTMR004014 = ""                                '取込開始時間
        mtm10r004renkei.MTMR004015 = ""                                '取込終了時間

        '変数
        Dim temporaryNum As Decimal = 0
        Dim sysError As String = ""
        Dim dtStartTime As Date
        Dim startTime As String
        dtStartTime = DateTime.Now
        startTime = dtStartTime.ToString("HHmmss")
        '取込終了時間
        mtm10r004renkei.MTMR004014 = startTime
        Dim dtEndTime As Date
        Dim endTime As String

        'バリデーション前処理
        Dim validateErrorList = Me.Biz04.ValidateBeforeImport(mtm10r004renkei)
        If validateErrorList.Count > 0 Then
            For Each errorMessage As String In validateErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        '仮採番（一旦、見積ヘッダーテーブルのMAXから採番する）
        temporaryNum = Me.Biz04.TemporaryNumbering()
        If temporaryNum < 1 Then
            MessageBox.Show("仮採番に失敗しました")
        End If

        Dim dialogProgress As New DialogMTMProgress
        Try
            Dim errorList As New List(Of String)
            Dim errorString As String = ""
            Dim tableMTM10R001TANKA As DataTable = Models.MTM10R001TANKA.GetDataTable()
            Dim tableMTM10R001TANKA2 As DataTable = Models.MTM10R001TANKA.GetDataTable()
            Dim tableHAN10R007MITSUMORIM As DataTable = Models.HAN10R007MITSUMORIM.GetDataTable()
            Dim tableHAN10R006MITSUMORIH As DataTable = Models.HAN10R006MITSUMORIH.GetDataTable()
            '明細拡張テーブル（ヘッダ、明細）04/17
            Dim tableHAN10R030KAKUCHO_H As DataTable = Models.HAN10R030KAKUCHO.GetDataTable()
            Dim tableHAN10R030KAKUCHO_M As DataTable = Models.HAN10R030KAKUCHO.GetDataTable()
            Dim tableHAN98MA01TANKA As DataTable = Models.HAN98MA01TANKA.GetDataTable()
            Dim tableHAN98MA02SUTANKA As DataTable = Models.HAN98MA02SUTANKA.GetDataTable()
            Dim lineCount = 0
            Dim readCount = 1
            Dim progressNum As Decimal = 100
            Dim conditionsFlg As Boolean = False
            Dim conditionsMTMR002001 As String = ""      '得意先コード比較用
            Dim conditionsMTMR002002 As String = ""      '商品コード比較用
            Dim conditionsMTMR002003 As String = ""      '規格比較用
            Dim conditionsMTMR002004 As String = ""      '売実施日
            Dim conditionsMTMR002035 As String = ""      '仕実施日
            Dim kariNum As Decimal = 0　                 '仮採番用
            Dim lineNo As Decimal = 0                    '行番号
            Dim conditionsDetailDate As String = ""      '見積日用
            Dim conditionsDetailDate2 As String = ""     '制約者日時
            Dim conditionsDetailDateNext As String = ""　'見積日用
            Dim conditionsDetailDateNext2 As String = "" '制約者日時
            Dim dtWork As DateTime = DateTime.Now        '日付用
            Dim dtToday = DateTime.Now                   '今日日付
            Dim sum01 As Decimal = 0                     '計算用
            Dim sum02 As Decimal = 0　                   '計算用
            Dim sumNum As Decimal = 0                    '計算用
            Dim TaxNum As Decimal = 0                    '消費税集計用
            Dim kariNumList As New List(Of Decimal)
            dialogProgress.Show(Me)

            '--------------------------------
            '①価格入力データを読み込み START
            '--------------------------------
            tableMTM10R001TANKA = Me.Biz04.GetDataPrice(Me.SearchCondition, errorString)
            If (Not String.IsNullOrEmpty(errorString)) Then
                MessageBox.Show("価格入力データの取得に失敗しました")
            End If

            For Each rowTanka As DataRow In tableMTM10R001TANKA.Rows
                Dim han10r007mitsumorim As New Models.HAN10R007MITSUMORIM
                Dim han10r006mitsumorih As New Models.HAN10R006MITSUMORIH
                '明細拡張テーブル（ヘッダ、明細）04/17
                Dim han10r030kakucho_m As New Models.HAN10R030KAKUCHO
                Dim han10r030kakucho_h As New Models.HAN10R030KAKUCHO
                conditionsFlg = False

                '前レコードとキー比較
                'If (conditionsMTMR002001 = rowTanka("MTMR002001").ToString().Trim() _
                '    And conditionsMTMR002002 = rowTanka("MTMR002002").ToString().Trim() _
                '    And conditionsMTMR002003 = rowTanka("MTMR002003").ToString().Trim() _
                '    And conditionsMTMR002004 = rowTanka("MTMR002032").ToString().Trim()) Then
                If (conditionsMTMR002001 = rowTanka("MTMR002001").ToString().Trim() _
                    And conditionsMTMR002004 = rowTanka("MTMR002032").ToString().Trim() _
                    And conditionsMTMR002035 = rowTanka("MTMR002035").ToString().Trim()) Then
                    '比較キーが一致
                    conditionsFlg = True
                    '行番号+1
                    lineNo = lineNo + 1
                Else
                    '仮番号を採番
                    kariNum = kariNum + 1
                    kariNumList.Add(kariNum)
                    '行番号を初期化
                    lineNo = 1
                End If

                Dim strErrorInfo As String
                strErrorInfo = "得意先C: " & rowTanka("MTMR002001").ToString().Trim() & " 商品C: " & rowTanka("MTMR002002").ToString().Trim()

                '-------------------------------
                '②見積明細テーブルを生成 START
                '-------------------------------
                '見積日付（年月日）条件あり
                '明細単位で異なる場合は1明細目をセット
                'If (String.IsNullOrEmpty(rowTanka("MTMR002087").ToString().Trim())) Then
                '    '未送信データのみの場合は、出力日をセット
                '    conditionsDetailDate = dtToday.ToString("yyyyMMdd")
                '    conditionsDetailDate2 = dtToday.ToString("yyyyMMddHHmmss.fff")
                'Else
                '    conditionsDetailDate = rowTanka("MTMR002087").ToString().Trim()
                '    conditionsDetailDate = conditionsDetailDate.Insert(6, "/").Insert(4, "/")
                '    If (DateTime.TryParse(conditionsDetailDate, dtWork)) Then
                '        conditionsDetailDate = dtWork.ToString("yyyyMMdd")
                '        conditionsDetailDate2 = dtWork.ToString("yyyyMMdd") & "000000"
                '    End If
                'End If
                ''前の明細と比較用
                'If (Not String.IsNullOrEmpty(conditionsDetailDateNext)) _
                '                And (conditionsDetailDateNext <> rowTanka("MTMR002087").ToString().Trim()) Then
                '    conditionsDetailDate = conditionsDetailDateNext
                '    conditionsDetailDate2 = conditionsDetailDateNext2
                'End If
                conditionsDetailDate = dtToday.ToString("yyyyMMdd")
                conditionsDetailDate2 = dtToday.ToString("yyyyMMddHHmmss.fff")

                han10r007mitsumorim.HANR007001 = conditionsDetailDate  '見積日付（年月日）
                han10r007mitsumorim.HANR007002 = kariNum  '処理連番
                han10r007mitsumorim.HANR007003 = "0"  '明細区分
                han10r007mitsumorim.HANR007004 = lineNo  '行番号
                han10r007mitsumorim.HANR007005 = "01"  '取引区分
                han10r007mitsumorim.HANR007006 = rowTanka("MTMR002002").ToString().Trim()  '商品コード
                '商品名１は単価台帳マスタより引き当てる
                han10r007mitsumorim.HANR007007 = Me.Biz04.ProductNameSearch(rowTanka("MTMR002001").ToString().Trim() _
                                                                          , rowTanka("MTMR002002").ToString().Trim() _
                                                                          , rowTanka("MTMR002003").ToString().Trim())
                han10r007mitsumorim.HANR007008 = rowTanka("MTMR002020").ToString().Trim()  '数量単位
                han10r007mitsumorim.HANR007009 = rowTanka("MTMR002019").ToString().Trim()  '入数
                han10r007mitsumorim.HANR007010 = "0"  '個数
                han10r007mitsumorim.HANR007011 = ""   '個数単位
                han10r007mitsumorim.HANR007012 = rowTanka("MTMR002004").ToString().Trim()   '見積数量　4/18に再度戻し
                'han10r007mitsumorim.HANR007012 = Me.Biz04.LotProcessAnswer(tableMTM10R001TANKA _
                '                                                           , rowTanka("MTMR002001").ToString().Trim() _
                '                                                           , rowTanka("MTMR002002").ToString().Trim() _
                '                                                           , rowTanka("MTMR002003").ToString().Trim() _
                '                                                           , rowTanka("MTMR002004").ToString().Trim())   '見積数量（ロット判定の内容にて値を設定 04/15）
                han10r007mitsumorim.HANR007013 = rowTanka("MTMR002030").ToString().Trim()   '見積単価
                '計算式（見積数量 * 見積単価)
                sumNum = sum01 = sum02 = 0
                'sum01 = han10r007mitsumorim.HANR007012
                sum01 = han10r007mitsumorim.HANR007009   '2023/04/06 No174対応
                sum02 = han10r007mitsumorim.HANR007013
                sumNum = sum01 * sum02
                ''---------
                'Dim strHANR007014 As String
                'Dim decHANR007014 As Decimal = 0.0000
                'If Not String.IsNullOrEmpty(sumNum.ToString) Then
                '    strHANR007014 = Me.Biz04.FormatDecimalNumber(sumNum, 19, 4)
                '    If (Not Decimal.TryParse(strHANR007014, decHANR007014)) Then
                '        decHANR007014 = 0.0000
                '    End If
                'End If
                'If (Not Me.Biz04.NumericNumberDigitsCheck(decHANR007014, 15, 4)) Then
                '    dialogProgress.Close()
                '    Me.Activate()
                '    MessageBox.Show(strErrorInfo & vbCrLf & "行数(" + readCount.ToString() + ") 明細 見積金額(計算式) 数値・桁数エラー")
                '    Exit Sub
                'End If
                ''---------
                han10r007mitsumorim.HANR007014 = sumNum   '見積金額
                han10r007mitsumorim.HANR007015 = rowTanka("MTMR002033").ToString().Trim()   '原単価
                han10r007mitsumorim.HANR007016 = "0"  '単価計算方法
                han10r007mitsumorim.HANR007017 = "0"  '単価掛率
                han10r007mitsumorim.HANR007018 = "0"  '標準売上単価
                han10r007mitsumorim.HANR007019 = "0"  '標準仕入単価
                han10r007mitsumorim.HANR007020 = "0"  '上代単価
                han10r007mitsumorim.HANR007022 = "0"  '値引対象額
                han10r007mitsumorim.HANR007023 = "0"  '値引率
                han10r007mitsumorim.HANR007024 = "0"  '課税区分
                '消費税率の引き当て
                '-新税率実施日＞新単価更新日（売）の時、旧税率
                '-新税率実施日≦新単価更新日（売）の時、新税率
                han10r007mitsumorim.HANR007025 = Me.Biz04.TaxNewOldSearch(rowTanka("MTMR002032").ToString().Trim())  '消費税率
                'han10r007mitsumorim.HANR007025 = "0"  '消費税率
                han10r007mitsumorim.HANR007026 = "0"  '内消費税額
                han10r007mitsumorim.HANR007027 = "0"  '外税額（明細消費税用）
                han10r007mitsumorim.HANR007028 = "0"  '消費税計算区分
                han10r007mitsumorim.HANR007029 = "0"  '行適用コード
                han10r007mitsumorim.HANR007030 = " "  '行適用１
                han10r007mitsumorim.HANR007031 = " "  '行適用２
                han10r007mitsumorim.HANR007032 = "JPY" '通貨コード
                han10r007mitsumorim.HANR007033 = " "  '管理コード
                han10r007mitsumorim.HANR007034 = "00"  'データ発生区分
                han10r007mitsumorim.HANR007035 = "0"  '消費税総額 上代課税区分
                '計算式（数量 * 原単価)
                sumNum = sum01 = sum02 = 0
                'sum01 = han10r007mitsumorim.HANR007012
                sum01 = han10r007mitsumorim.HANR007009   '2023/04/06 No174対応
                sum02 = han10r007mitsumorim.HANR007015
                sumNum = sum01 * sum02
                han10r007mitsumorim.HANR007036 = sumNum '原価金額
                '粗利額(見積金額－原価金額)
                sumNum = han10r007mitsumorim.HANR007014 - han10r007mitsumorim.HANR007036
                ''---------
                'Dim strHANR007021 As String
                'Dim decHANR007021 As Decimal = 0.0000
                'If Not String.IsNullOrEmpty(sumNum.ToString) Then
                '    strHANR007021 = Me.Biz04.FormatDecimalNumber(sumNum, 19, 4)
                '    If (Not Decimal.TryParse(strHANR007021, decHANR007021)) Then
                '        decHANR007021 = 0.0000
                '    End If
                'End If
                'If (Not Me.Biz04.NumericNumberDigitsCheck(decHANR007021, 15, 4)) Then
                '    dialogProgress.Close()
                '    Me.Activate()
                '    MessageBox.Show(strErrorInfo & vbCrLf & "行数(" + readCount.ToString() + ") 明細 粗利額(計算式) 数値・桁数エラー")
                '    Exit Sub
                'End If
                ''---------
                han10r007mitsumorim.HANR007021 = sumNum  '粗利額
                '0パディング対応
                Dim intHANR007037 As Integer = 0
                Dim strHANR007037 As String = rowTanka("MTMR002008").ToString()
                If Integer.TryParse(strHANR007037, intHANR007037) Then
                    strHANR007037 = intHANR007037.ToString("D6")
                End If
                han10r007mitsumorim.HANR007037 = strHANR007037   '営業所コード
                han10r007mitsumorim.HANR007038 = "0000"  '拡張項目入力パターン№
                han10r007mitsumorim.HANR007039 = rowTanka("MTMR002001").ToString().Trim()   '得意先コード
                '0パディング対応
                Dim intHANR007040 As Integer = 0
                Dim strHANR007040 As String = rowTanka("MTMR002012").ToString()
                If Integer.TryParse(strHANR007040, intHANR007040) Then
                    strHANR007040 = intHANR007040.ToString("D6")
                End If
                han10r007mitsumorim.HANR007040 = strHANR007040   '担当者コード
                '0パディング対応
                Dim intHANR007041 As Integer = 0
                Dim strHANR007041 As String = rowTanka("MTMR002010").ToString()
                If Integer.TryParse(strHANR007041, intHANR007041) Then
                    strHANR007041 = intHANR007041.ToString("D6")
                End If
                han10r007mitsumorim.HANR007041 = strHANR007041   '部課コード
                han10r007mitsumorim.HANR007042 = "01" '取引区分属性分
                han10r007mitsumorim.HANR007999 = "0"  '更新番号
                han10r007mitsumorim.HANR007043 = "0"   'F
                han10r007mitsumorim.HANR007044 = " "  '原価集計コード
                han10r007mitsumorim.HANR007045 = " "  '原価集計サブコード
                han10r007mitsumorim.HANR007046 = " "  '要素コード
                han10r007mitsumorim.HANR007047 = " "  '内訳コード１
                han10r007mitsumorim.HANR007048 = " "  '内訳コード２
                han10r007mitsumorim.HANR007049 = " "  '内訳コード３
                han10r007mitsumorim.HANR007050 = "0"  '換算数量
                han10r007mitsumorim.HANR007051 = "0"  '金額算出モード
                han10r007mitsumorim.HANR007052 = "0"  '原価金額算出モード
                '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                han10r007mitsumorim.HANR007INS = dtToday.ToString("yyyyMMddHHmmss.fff")  '登録日時
                '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                han10r007mitsumorim.HANR007UPD = dtToday.ToString("yyyyMMddHHmmss.fff")  '更新日時
                han10r007mitsumorim.HANR007053 = "0"  '仮伝票発生区分
                han10r007mitsumorim.HANR007054 = "0"  '検収区分
                han10r007mitsumorim.HANR007055 = "0"  '外貨　見積単価
                han10r007mitsumorim.HANR007056 = "0"  '外貨　見積金額
                han10r007mitsumorim.HANR007057 = "0"  '外貨　原単価
                han10r007mitsumorim.HANR007058 = "0"  '外貨　原価金額
                han10r007mitsumorim.HANR007059 = "0"  '外貨　粗利額
                han10r007mitsumorim.HANR007060 = "0"  '外貨　上代単価
                han10r007mitsumorim.HANR007061 = "0"  '外貨　値引対象額
                han10r007mitsumorim.HANR007062 = "0"  '消費税分類
                '商品名２は単価台帳マスタより引き当てる
                han10r007mitsumorim.HANR007063 = Me.Biz04.ProductName2Search(rowTanka("MTMR002001").ToString().Trim() _
                                                                           , rowTanka("MTMR002002").ToString().Trim() _
                                                                           , rowTanka("MTMR002003").ToString().Trim())  '商品名２
                han10r007mitsumorim.HANR007A001 = "1"  '単価台帳参照区分
                '手配区分は単価台帳マスタより引き当てる
                han10r007mitsumorim.HANR007A002 = Me.Biz04.ArrangeDivisionSearch(rowTanka("MTMR002001").ToString().Trim() _
                                                                               , rowTanka("MTMR002002").ToString().Trim() _
                                                                               , rowTanka("MTMR002003").ToString().Trim())  '手配区分
                han10r007mitsumorim.HANR007A003 = rowTanka("MTMR002003").ToString().Trim()   '規格
                han10r007mitsumorim.HANR007A004 = rowTanka("MTMR002048").ToString().Trim()   '見積書商品名
                han10r007mitsumorim.HANR007A005 = rowTanka("MTMR002014").ToString().Trim()   '仕入先コード
                han10r007mitsumorim.HANR007A006 = "0"  　　　　　　　　　　　　　　　　　　　'約区分
                han10r007mitsumorim.HANR007A007 = rowTanka("MTMR002025").ToString().Trim()   '旧売上単価
                han10r007mitsumorim.HANR007A008 = rowTanka("MTMR002025").ToString().Trim()   '売上単価（現在）
                han10r007mitsumorim.HANR007A009 = rowTanka("MTMR002027").ToString().Trim()   '仕入単価（現在）
                han10r007mitsumorim.HANR007A010 = "1"  　　　　　　　　　　　　　　　　　　　'成約区分
                '成約日時(価格入力データの見積書送付フラグ（日付）をセット)
                han10r007mitsumorim.HANR007A011 = conditionsDetailDate  　　　　　　　　　　　'成約日時
                '成約者ID(起動時のログインIDをセット)
                han10r007mitsumorim.HANR007A012 = "BULK UPDATE"  　　　'成約者ID
                'han10r007mitsumorim.HANR007A013 = rowTanka("MTMR002019").ToString().Trim()   '見積数量（ロット）
                han10r007mitsumorim.HANR007A013 = Me.Biz04.LotProcessAnswer(tableMTM10R001TANKA _
                                                                           , rowTanka("MTMR002001").ToString().Trim() _
                                                                           , rowTanka("MTMR002002").ToString().Trim() _
                                                                           , rowTanka("MTMR002003").ToString().Trim() _
                                                                           , rowTanka("MTMR002004").ToString().Trim())   '見積数量（ロット判定の内容にて値を設定 04/18）
                han10r007mitsumorim.HANR007A014 = "1"  　　　　　　　　　　　　　　　　　　　'台帳反映区分
                '旧単価印字区分(見積書送付フラグをセット)
                han10r007mitsumorim.HANR007A015 = rowTanka("MTMR002086").ToString().Trim()   '旧単価印字区分
                han10r007mitsumorim.HANR007A016 = " "  　　　　　　　　　　　　　　　　　　　'注意事項
                han10r007mitsumorim.HANR007A017 = "0"  　　　　　　　　　　　　　　　　　　　'特値区分（売上）
                han10r007mitsumorim.HANR007A018 = "0"  　　　　　　　　　　　　　　　　　　　'特値区分（仕入）

                'キー項目を設定
                conditionsMTMR002001 = rowTanka("MTMR002001").ToString().Trim()
                'conditionsMTMR002002 = rowTanka("MTMR002002").ToString().Trim()
                'conditionsMTMR002003 = rowTanka("MTMR002003").ToString().Trim()
                conditionsMTMR002004 = rowTanka("MTMR002032").ToString().Trim()
                conditionsMTMR002035 = rowTanka("MTMR002035").ToString().Trim()
                '見積日付（年月日）比較用に入れる
                conditionsDetailDateNext = conditionsDetailDate
                conditionsDetailDateNext2 = conditionsDetailDate2

                '見積明細データ作成
                han10r007mitsumorim.SetDataRow(tableHAN10R007MITSUMORIM)
                '-------------------------------
                '②見積明細テーブルを生成 END
                '-------------------------------
                '-------------------------------
                '②-1明細拡張テーブル（明細）を生成 START 4/17
                '-------------------------------
                han10r030kakucho_m.HANR030001 = 4
                han10r030kakucho_m.HANR030002 = 1
                han10r030kakucho_m.HANR030003 = 0
                han10r030kakucho_m.HANR030004 = kariNum
                han10r030kakucho_m.HANR030005 = lineNo
                han10r030kakucho_m.HANR030006 = 0
                han10r030kakucho_m.HANR030007 = ""
                han10r030kakucho_m.HANR030008 = kariNum
                'IF 価格入力テーブル・運賃適用を設定（無ければNULL）
                If (Not String.IsNullOrEmpty(rowTanka("MTMR002047").ToString().Trim())) Then
                    han10r030kakucho_m.HANR030009 = rowTanka("MTMR002047").ToString().Trim()
                Else
                    han10r030kakucho_m.HANR030009 = ""
                End If
                han10r030kakucho_m.HANR030010 = ""
                han10r030kakucho_m.HANR030011 = ""
                han10r030kakucho_m.HANR030012 = ""
                han10r030kakucho_m.HANR030013 = ""
                han10r030kakucho_m.HANR030014 = ""
                han10r030kakucho_m.HANR030015 = ""
                han10r030kakucho_m.HANR030016 = ""
                han10r030kakucho_m.HANR030017 = ""
                han10r030kakucho_m.HANR030018 = ""
                han10r030kakucho_m.HANR030019 = ""
                '単価台帳マスタ・手配区分コード１（HANMA01008）
                han10r030kakucho_m.HANR030020 = han10r007mitsumorim.HANR007A002
                han10r030kakucho_m.HANR030021 = ""
                'IF 価格入力テーブル・発注摘要（無ければNULL）
                If (Not String.IsNullOrEmpty(rowTanka("MTMR002046").ToString().Trim())) Then
                    han10r030kakucho_m.HANR030022 = rowTanka("MTMR002046").ToString().Trim()
                Else
                    han10r030kakucho_m.HANR030022 = ""
                End If
                han10r030kakucho_m.HANR030023 = ""
                'IF 価格入力テーブル・社内摘要（無ければNULL）
                If (Not String.IsNullOrEmpty(rowTanka("MTMR002045").ToString().Trim())) Then
                    han10r030kakucho_m.HANR030024 = rowTanka("MTMR002045").ToString().Trim()
                Else
                    han10r030kakucho_m.HANR030024 = ""
                End If
                'han10r030kakucho_m.HANR030024 = ""
                han10r030kakucho_m.HANR030025 = ""
                han10r030kakucho_m.HANR030026 = ""
                han10r030kakucho_m.HANR030999 = 0
                han10r030kakucho_m.HANR030INS = dtToday.ToString("yyyyMMddHHmmss.fff")
                han10r030kakucho_m.HANR030UPD = dtToday.ToString("yyyyMMddHHmmss.fff")
                '見積明細データ作成
                han10r030kakucho_m.SetDataRow(tableHAN10R030KAKUCHO_M)
                '-------------------------------
                '②-1明細拡張テーブル（明細）を生成 END 4/17
                '-------------------------------
                If (Me.CheckBox001.Checked = True) Then
                    '------------------------------------
                    '⑥数量別単価台帳マスターを更新 START
                    '------------------------------------
                    '更新スクリプト用データ
                    Dim han98ma02sutanka As New Models.HAN98MA02SUTANKA
                    han98ma02sutanka.HANMA02001 = rowTanka("MTMR002001").ToString().Trim()  '得意先コード
                    han98ma02sutanka.HANMA02002 = rowTanka("MTMR002002").ToString().Trim()  '商品コード
                    han98ma02sutanka.HANMA02003 = rowTanka("MTMR002003").ToString().Trim()  '規格
                    han98ma02sutanka.HANMA02004 = "0"                                       '台帳データ区分
                    han98ma02sutanka.HANMA02005 = rowTanka("MTMR002018").ToString().Trim()  '行番号
                    han98ma02sutanka.HANMA02006 = rowTanka("MTMR002004").ToString().Trim()  '数量・上限
                    han98ma02sutanka.HANMA02009 = rowTanka("MTMR002032").ToString().Trim()  '単価変更日付（売上）
                    han98ma02sutanka.HANMA02010 = rowTanka("MTMR002035").ToString().Trim()  '単価変更日付（仕入）
                    han98ma02sutanka.HANMA02011 = rowTanka("MTMR002030").ToString().Trim()  '売上単価（新）
                    han98ma02sutanka.HANMA02012 = rowTanka("MTMR002033").ToString().Trim()  '仕入単価（新）
                    han98ma02sutanka.SetDataRow(tableHAN98MA02SUTANKA)
                    '------------------------------------
                    '⑥数量別単価台帳マスターを更新 END
                    '------------------------------------
                    '------------------------------
                    '⑦単価台帳マスターを更新 START
                    '------------------------------
                    '更新スクリプト用データ
                    Dim han98ma01tanka As New Models.HAN98MA01TANKA
                    han98ma01tanka.HANMA01001 = rowTanka("MTMR002001").ToString().Trim()  '得意先コード
                    han98ma01tanka.HANMA01002 = rowTanka("MTMR002002").ToString().Trim()  '商品コード
                    han98ma01tanka.HANMA01003 = rowTanka("MTMR002003").ToString().Trim()  '規格
                    han98ma01tanka.HANMA01020 = rowTanka("MTMR002032").ToString().Trim()  '単価変更日付（売上）
                    han98ma01tanka.HANMA01021 = rowTanka("MTMR002035").ToString().Trim()  '単価変更日付（仕入）
                    han98ma01tanka.HANMA01029 = kariNum                                   '参照見積処理連番（一旦仮）
                    han98ma01tanka.HANMA01030 = lineNo                                    '参照見積行番号（一旦仮）
                    han98ma01tanka.HANMA01034 = Me.FormMenu.ModelMtmUser.MTMM002001       '更新者ID
                    han98ma01tanka.HANMA01035 = dtToday.ToString("yyyyMMdd")              '更新日時
                    han98ma01tanka.HANMA01036 = "MOBILE9999X"                             '更新端末
                    han98ma01tanka.HANMA01038 = kariNum                                   '参照見積処理連番
                    han98ma01tanka.HANMA01039 = lineNo                                    '参照見積行番号
                    han98ma01tanka.HANMA01UPD = dtToday.ToString("yyyyMMddHHmmss.fff")    '更新日時
                    han98ma01tanka.SetDataRow(tableHAN98MA01TANKA)
                    '------------------------------
                    '⑦単価台帳マスターを更新 END
                    '------------------------------
                End If

                If (conditionsFlg = False) Then
                    '-----------------------------------------
                    '②見積明細テーブル消費税データを生成 START
                    '-----------------------------------------
                    han10r007mitsumorim.HANR007001 = conditionsDetailDate  '見積日付（年月日）
                    han10r007mitsumorim.HANR007002 = kariNum  '処理連番
                    han10r007mitsumorim.HANR007003 = "1"  '明細区分
                    han10r007mitsumorim.HANR007004 = "1"  '行番号
                    han10r007mitsumorim.HANR007005 = "01" '取引区分
                    han10r007mitsumorim.HANR007006 = " "  '商品コード
                    TaxNum = Me.Biz04.GetDataEstimatedAmountTax(mtm10r004renkei.MTMR004002 _
                                                              , rowTanka("MTMR002001").ToString().Trim() _
                                                              , rowTanka("MTMR002032").ToString().Trim() _
                                                              , errorString)
                    '---------
                    han10r007mitsumorim.HANR007007 = "消費税等（課税対象額 " & TaxNum & ")"  '商品名１
                    '---------
                    han10r007mitsumorim.HANR007007 = Me.Biz04.DeliveryConvert(han10r007mitsumorim.HANR007007)
                    han10r007mitsumorim.HANR007008 = " "  '数量単位
                    han10r007mitsumorim.HANR007009 = "0"  '入数
                    han10r007mitsumorim.HANR007010 = "0"  '個数
                    han10r007mitsumorim.HANR007011 = " "  '個数単位
                    han10r007mitsumorim.HANR007012 = "0"  '見積数量
                    han10r007mitsumorim.HANR007013 = "0"  '見積単価
                    '計算式（見積金額の合計 * 消費税率)
                    '関数()にて計算値を取得する
                    sumNum = sum01 = sum02 = 0
                    'sum01 = han10r007mitsumorim.HANR007012
                    sum01 = han10r007mitsumorim.HANR007009   '2023/04/06 No174対応
                    sum02 = han10r007mitsumorim.HANR007013
                    sumNum = sum01 * sum02
                    '---------
                    'Dim strHANR007014_2 As String
                    'Dim decHANR007014_2 As Decimal = 0.0000
                    'If Not String.IsNullOrEmpty(sumNum.ToString) Then
                    '    strHANR007014_2 = Me.Biz04.FormatDecimalNumber(sumNum, 19, 4)
                    '    If (Not Decimal.TryParse(strHANR007014_2, decHANR007014_2)) Then
                    '        decHANR007014_2 = 0.0000
                    '    End If
                    'End If
                    'If (Not Me.Biz04.NumericNumberDigitsCheck(decHANR007014_2, 15, 4)) Then
                    '    dialogProgress.Close()
                    '    Me.Activate()
                    '    MessageBox.Show(strErrorInfo & vbCrLf & "行数(" + readCount.ToString() + ") 明細(消費税) 見積金額(計算式) 数値・桁数エラー")
                    '    Exit Sub
                    'End If
                    '---------
                    han10r007mitsumorim.HANR007014 = sumNum   '見積金額
                    han10r007mitsumorim.HANR007015 = "0"  '原単価
                    han10r007mitsumorim.HANR007016 = "0"  '単価計算方法
                    han10r007mitsumorim.HANR007017 = "0"  '単価掛率
                    han10r007mitsumorim.HANR007018 = "0"  '標準売上単価
                    han10r007mitsumorim.HANR007019 = "0"  '標準仕入単価
                    han10r007mitsumorim.HANR007020 = "0"  '上代単価
                    han10r007mitsumorim.HANR007021 = "0"  '粗利額
                    han10r007mitsumorim.HANR007022 = "0"  '値引対象額
                    han10r007mitsumorim.HANR007023 = "0"  '値引率
                    han10r007mitsumorim.HANR007024 = "9"  '課税区分
                    '消費税率の引き当て
                    '-新税率実施日＞新単価更新日（売）の時、旧税率
                    '-新税率実施日≦新単価更新日（売）の時、新税率
                    'han10r007mitsumorim.HANR007025 = "0"  '消費税率
                    '☆内消費税額(見積金額の合計×消費税率をセット ※端数処理は小数点以下1位四捨五入)
                    han10r007mitsumorim.HANR007026 = "0"  '内消費税額
                    han10r007mitsumorim.HANR007027 = "0"  '外税額（明細消費税用）
                    han10r007mitsumorim.HANR007028 = "0"  '消費税計算区分
                    han10r007mitsumorim.HANR007029 = "0"  '行適用コード
                    han10r007mitsumorim.HANR007030 = " "  '行適用１
                    han10r007mitsumorim.HANR007031 = " "  '行適用２
                    han10r007mitsumorim.HANR007032 = "JPY" '通貨コード
                    han10r007mitsumorim.HANR007033 = " "  '管理コード
                    han10r007mitsumorim.HANR007034 = "00" 'データ発生区分
                    han10r007mitsumorim.HANR007035 = "0"  '消費税総額 上代課税区分
                    han10r007mitsumorim.HANR007036 = "0"  '原価金額
                    '0パディング対応
                    intHANR007037 = 0
                    strHANR007037 = rowTanka("MTMR002008").ToString()
                    If Integer.TryParse(strHANR007037, intHANR007037) Then
                        strHANR007037 = intHANR007037.ToString("D6")
                    End If
                    han10r007mitsumorim.HANR007037 = strHANR007037   '営業所コード
                    han10r007mitsumorim.HANR007038 = "0000"  '拡張項目入力パターン№
                    han10r007mitsumorim.HANR007039 = rowTanka("MTMR002001").ToString().Trim()   '得意先コード
                    '0パディング対応
                    intHANR007040 = 0
                    strHANR007040 = rowTanka("MTMR002012").ToString()
                    If Integer.TryParse(strHANR007040, intHANR007040) Then
                        strHANR007040 = intHANR007040.ToString("D6")
                    End If
                    han10r007mitsumorim.HANR007040 = strHANR007040   '担当者コード
                    '0パディング対応
                    intHANR007041 = 0
                    strHANR007041 = rowTanka("MTMR002010").ToString()
                    If Integer.TryParse(strHANR007041, intHANR007041) Then
                        strHANR007041 = intHANR007041.ToString("D6")
                    End If
                    han10r007mitsumorim.HANR007041 = strHANR007041   '部課コード
                    han10r007mitsumorim.HANR007042 = "01" '取引区分属性分
                    han10r007mitsumorim.HANR007999 = "0"   '更新番号
                    han10r007mitsumorim.HANR007043 = "0"   'F
                    han10r007mitsumorim.HANR007044 = " "  '原価集計コード
                    han10r007mitsumorim.HANR007045 = " "  '原価集計サブコード
                    han10r007mitsumorim.HANR007046 = " "  '要素コード
                    han10r007mitsumorim.HANR007047 = " "  '内訳コード１
                    han10r007mitsumorim.HANR007048 = " "  '内訳コード２
                    han10r007mitsumorim.HANR007049 = " "  '内訳コード３
                    han10r007mitsumorim.HANR007050 = "0"  '換算数量
                    han10r007mitsumorim.HANR007051 = "0"  '金額算出モード
                    han10r007mitsumorim.HANR007052 = "0"  '原価金額算出モード
                    '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                    'han10r007mitsumorim.HANR007INS = "0"  '登録日時
                    '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                    'han10r007mitsumorim.HANR007UPD = "0"  '更新日時
                    han10r007mitsumorim.HANR007053 = "0"  '仮伝票発生区分
                    han10r007mitsumorim.HANR007054 = "0"  '検収区分
                    han10r007mitsumorim.HANR007055 = "0"  '外貨　見積単価
                    han10r007mitsumorim.HANR007056 = "0"  '外貨　見積金額
                    han10r007mitsumorim.HANR007057 = "0"  '外貨　原単価
                    han10r007mitsumorim.HANR007058 = "0"  '外貨　原価金額
                    han10r007mitsumorim.HANR007059 = "0"  '外貨　粗利額
                    han10r007mitsumorim.HANR007060 = "0"  '外貨　上代単価
                    han10r007mitsumorim.HANR007061 = "0"  '外貨　値引対象額
                    han10r007mitsumorim.HANR007062 = "0"  '消費税分類
                    han10r007mitsumorim.HANR007063 = " "  '商品名２
                    han10r007mitsumorim.HANR007A001 = "0" '単価台帳参照区分
                    han10r007mitsumorim.HANR007A002 = " " '手配区分
                    han10r007mitsumorim.HANR007A003 = " " '規格
                    han10r007mitsumorim.HANR007A004 = " " '見積書商品名
                    han10r007mitsumorim.HANR007A005 = " " '仕入先コード
                    han10r007mitsumorim.HANR007A006 = "0" '約区分
                    han10r007mitsumorim.HANR007A007 = "0"   '旧売上単価
                    han10r007mitsumorim.HANR007A008 = "0"   '売上単価（現在）
                    han10r007mitsumorim.HANR007A009 = "0"   '仕入単価（現在）
                    han10r007mitsumorim.HANR007A010 = "0"  '成約区分
                    han10r007mitsumorim.HANR007A011 = "0"  '成約日時
                    han10r007mitsumorim.HANR007A012 = " "  '成約者ID
                    han10r007mitsumorim.HANR007A013 = "0"  '見積数量（ロット）
                    han10r007mitsumorim.HANR007A014 = "0"  '台帳反映区分
                    han10r007mitsumorim.HANR007A015 = "0"  '旧単価印字区分
                    han10r007mitsumorim.HANR007A016 = " "  '注意事項
                    han10r007mitsumorim.HANR007A017 = " "  '特値区分（売上）
                    han10r007mitsumorim.HANR007A018 = " "  '特値区分（仕入）

                    '消費税データを作成
                    han10r007mitsumorim.SetDataRow(tableHAN10R007MITSUMORIM)
                    '-----------------------------------------
                    '②見積明細テーブル消費税データを生成 END
                    '-----------------------------------------
                End If

                '見積ヘッダーを作成
                If (conditionsFlg = False) Then
                    '----------------------------------
                    '③見積ヘッダーテーブルを生成 START
                    '----------------------------------
                    han10r006mitsumorih.HANR006001 = rowTanka("MTMR002001").ToString() '得意先コード
                    han10r006mitsumorih.HANR006002 = conditionsDetailDate '見積日付(年月日)
                    han10r006mitsumorih.HANR006003 = kariNum '見積書番号
                    han10r006mitsumorih.HANR006004 = kariNum '処理連番
                    han10r006mitsumorih.HANR006005 = dtToday.ToString("yyyyMMdd") '操作日付(年月日)
                    'ログインＩＤ(起動時のログインIDをセット)
                    'han10r006mitsumorih.HANR006006 = Me.FormMenu.ModelMtmUser.MTMM002001 'ログインＩＤ
                    han10r006mitsumorih.HANR006006 = "BULK UPDATE" 'ログインＩＤ
                    '0パディング対応
                    Dim intHANR006007 As Integer = 0
                    Dim strHANR006007 As String = rowTanka("MTMR002012").ToString()
                    If Integer.TryParse(strHANR006007, intHANR006007) Then
                        strHANR006007 = intHANR006007.ToString("D6")
                    End If
                    han10r006mitsumorih.HANR006007 = strHANR006007 '担当者コード
                    '0パディング対応
                    Dim intHANR006008 As Integer = 0
                    Dim strHANR006008 As String = rowTanka("MTMR002010").ToString()
                    If Integer.TryParse(strHANR006008, intHANR006008) Then
                        strHANR006008 = intHANR006008.ToString("D6")
                    End If
                    han10r006mitsumorih.HANR006008 = strHANR006008 '部課コード
                    han10r006mitsumorih.HANR006009 = "000000" '納品先コード(04/15)
                    han10r006mitsumorih.HANR006010 = "0" '納期日(年月日)
                    han10r006mitsumorih.HANR006011 = " " '納期(手入力)
                    '支払条件(７－２．支払条件のセットについて) ☆課題10
                    han10r006mitsumorih.HANR006012 = Me.Biz04.PaymentTermsSearch(rowTanka("MTMR002001").ToString().Trim())
                    han10r006mitsumorih.HANR006013 = "0" '有効期限(年月日)
                    han10r006mitsumorih.HANR006014 = "見積日より１カ月" '有効期限(手入力)
                    han10r006mitsumorih.HANR006015 = mtm10r004renkei.MTMR004003 '要件
                    han10r006mitsumorih.HANR006016 = "0" '消費税計算区分
                    '消費税率の引き当て
                    '-新税率実施日＞新単価更新日（売）の時、旧税率
                    '-新税率実施日≦新単価更新日（売）の時、新税率
                    han10r006mitsumorih.HANR006017 = han10r007mitsumorim.HANR007025  '消費税率
                    '見積書行数
                    han10r006mitsumorih.HANR006018 = Me.Biz04.GetDataPriceSpecify(mtm10r004renkei.MTMR004002 _
                                                                                , rowTanka("MTMR002001").ToString().Trim() _
                                                                                , rowTanka("MTMR002032").ToString().Trim() _
                                                                                , errorString)
                    han10r006mitsumorih.HANR006019 = "御見積書"  '見積書タイトル(04/18)
                    han10r006mitsumorih.HANR006020 = "下記の通りお見積申し上げます"  '見出し１
                    han10r006mitsumorih.HANR006021 = "御査収の程宜しくお願いいたします"  '見出し２
                    han10r006mitsumorih.HANR006022 = " "  '見出し３
                    han10r006mitsumorih.HANR006023 = "上記明細は消費税を含んでおりません。"  '備考１
                    han10r006mitsumorih.HANR006024 = "ご発注の際は見積Noをご明記願います。"  '備考２
                    han10r006mitsumorih.HANR006025 = " "  '備考３
                    han10r006mitsumorih.HANR006026 = "0"  '税込売上　売上額
                    han10r006mitsumorih.HANR006027 = "0"  '税込売上　値引額
                    han10r006mitsumorih.HANR006028 = "0"  '内消費税　売上額
                    han10r006mitsumorih.HANR006029 = "0"  '内消費税　値引額
                    '☆税抜売上　売上額
                    han10r006mitsumorih.HANR006030 = Me.Biz04.GetDataEstimatedAmount(mtm10r004renkei.MTMR004002 _
                                                                                    , rowTanka("MTMR002001").ToString().Trim() _
                                                                                    , rowTanka("MTMR002032").ToString().Trim() _
                                                                                    , errorString)
                    '---------
                    'If (Not Me.Biz04.NumericNumberDigitsCheck(han10r006mitsumorih.HANR006030, 15, 4)) Then
                    '    dialogProgress.Close()
                    '    Me.Activate()
                    '    MessageBox.Show(strErrorInfo & vbCrLf & "行数(" + readCount.ToString() + ") ヘッダー 税抜売上　売上額 数値・桁数エラー")
                    '    Exit Sub
                    'End If
                    '---------
                    han10r006mitsumorih.HANR006031 = "0"  '税抜売上　値引額
                    '外  税　売上額
                    han10r006mitsumorih.HANR006032 = han10r007mitsumorim.HANR007014  '外  税　売上額
                    '---------
                    'If (Not Me.Biz04.NumericNumberDigitsCheck(han10r006mitsumorih.HANR006032, 15, 4)) Then
                    '    dialogProgress.Close()
                    '    Me.Activate()
                    '    MessageBox.Show(strErrorInfo & vbCrLf & "行数(" + readCount.ToString() + ") ヘッダー 外  税　売上額 数値・桁数エラー")
                    '    Exit Sub
                    'End If
                    '---------
                    han10r006mitsumorih.HANR006033 = "0"  '外  税　値引額
                    han10r006mitsumorih.HANR006034 = "0"  '非課税　売上額
                    han10r006mitsumorih.HANR006035 = "0"  '非課税　値引額
                    '見積仮原価
                    han10r006mitsumorih.HANR006036 = Me.Biz04.GetDataTotalCost(mtm10r004renkei.MTMR004002 _
                                                                             , rowTanka("MTMR002001").ToString().Trim() _
                                                                             , rowTanka("MTMR002032").ToString().Trim() _
                                                                             , errorString)
                    '---------
                    'If (Not Me.Biz04.NumericNumberDigitsCheck(han10r006mitsumorih.HANR006036, 15, 4)) Then
                    '    dialogProgress.Close()
                    '    Me.Activate()
                    '    MessageBox.Show(strErrorInfo & vbCrLf & "行数(" + readCount.ToString() + ") ヘッダー 見積仮原価 数値・桁数エラー")
                    '    Exit Sub
                    'End If
                    '---------
                    '仮粗利額
                    han10r006mitsumorih.HANR006037 = Me.Biz04.GetDataGrossProfit(mtm10r004renkei.MTMR004002 _
                                                                               , rowTanka("MTMR002001").ToString().Trim() _
                                                                               , rowTanka("MTMR002032").ToString().Trim() _
                                                                               , errorString)
                    '---------
                    'If (Not Me.Biz04.NumericNumberDigitsCheck(han10r006mitsumorih.HANR006037, 15, 4)) Then
                    '    dialogProgress.Close()
                    '    Me.Activate()
                    '    MessageBox.Show(strErrorInfo & vbCrLf & "行数(" + readCount.ToString() + ") ヘッダー 仮粗利額 数値・桁数エラー")
                    '    Exit Sub
                    'End If
                    '---------
                    han10r006mitsumorih.HANR006038 = "0"  '関連売上伝票枚数
                    han10r006mitsumorih.HANR006039 = "0"  '関連受注伝票枚数
                    '手入力得意先名１(得意先マスタより)
                    han10r006mitsumorih.HANR006040 = Me.Biz04.Customer1Search(rowTanka("MTMR002001").ToString().Trim())
                    '手入力得意先名２(得意先マスタより)
                    han10r006mitsumorih.HANR006041 = Me.Biz04.Customer2Search(rowTanka("MTMR002001").ToString().Trim())
                    han10r006mitsumorih.HANR006042 = " "  '手入力納品先名
                    han10r006mitsumorih.HANR006043 = "JPY"  '通貨コード
                    han10r006mitsumorih.HANR006044 = "00"  'データ発生区分
                    '0パディング対応
                    Dim intHANR006045 As Integer = 0
                    Dim strHANR006045 As String = rowTanka("MTMR002008").ToString()
                    If Integer.TryParse(strHANR006045, intHANR006045) Then
                        strHANR006045 = intHANR006045.ToString("D6")
                    End If
                    han10r006mitsumorih.HANR006045 = strHANR006045 '営業所コード
                    han10r006mitsumorih.HANR006046 = "0"  '計上済区分
                    han10r006mitsumorih.HANR006047 = "0"  '計上伝票処理連番
                    han10r006mitsumorih.HANR006999 = "0"  '更新番号
                    han10r006mitsumorih.HANR006048 = "0"  '検収計上済区分
                    han10r006mitsumorih.HANR006049 = "0"  '検収計上伝票処理連番
                    han10r006mitsumorih.HANR006050 = "0"  '検収区分
                    han10r006mitsumorih.HANR006051 = "0"  'F
                    han10r006mitsumorih.HANR006052 = " "  '原価集計コード
                    han10r006mitsumorih.HANR006053 = " "  '原価集計サブコード
                    han10r006mitsumorih.HANR006911 = "0"  '履歴№
                    han10r006mitsumorih.HANR006914 = dtToday.ToString("yyyyMMdd")  '最終更新操作日付
                    han10r006mitsumorih.HANR006915 = dtToday.ToString("HHmmss")  '最終更新操作時刻
                    '最終更新ログインID(起動時のログインIDをセット)
                    'han10r006mitsumorih.HANR006916 = Me.FormMenu.ModelMtmUser.MTMM002001 '最終更新ログインID
                    han10r006mitsumorih.HANR006916 = "BULK UPDATE" '最終更新ログインID
                    han10r006mitsumorih.HANR006917 = "OSK.X1.HAN.A00080" '操作プログラムID
                    han10r006mitsumorih.HANR006918 = "0" '操作プログラムID枝番
                    han10r006mitsumorih.HANR006919 = "MOBILE9999X" '操作コンピュータ名
                    '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                    han10r006mitsumorih.HANR006INS = dtToday.ToString("yyyyMMddHHmmss.fff") '登録日時
                    '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                    han10r006mitsumorih.HANR006UPD = dtToday.ToString("yyyyMMddHHmmss.fff") '更新日時
                    han10r006mitsumorih.HANR006054 = "0" 'チェックマーク区分
                    han10r006mitsumorih.HANR006055 = "0" '仮伝票発生区分
                    han10r006mitsumorih.HANR006056 = "0" '承認日付
                    han10r006mitsumorih.HANR006057 = "0" '承認対象区分
                    han10r006mitsumorih.HANR006058 = "0" '発行済管理更新日時
                    han10r006mitsumorih.HANR006059 = "0" 'レート移送区分
                    han10r006mitsumorih.HANR006060 = "0" 'レート種類
                    han10r006mitsumorih.HANR006061 = "0" 'レート
                    han10r006mitsumorih.HANR006062 = "0" '外貨　売上額
                    han10r006mitsumorih.HANR006063 = "0" '外貨　値引額
                    han10r006mitsumorih.HANR006064 = "0" '外貨　見積仮原価
                    han10r006mitsumorih.HANR006065 = "0" '外貨　仮粗利額
                    han10r006mitsumorih.HANR006066 = "0" '消費税分類
                    han10r006mitsumorih.HANR006067 = "0" '副税率優先採用区分
                    '作成者ID(起動時のログインIDをセット)
                    han10r006mitsumorih.HANR006A001 = "BULK UPDATE" '作成者ID
                    han10r006mitsumorih.HANR006A002 = "2" '見積種別
                    han10r006mitsumorih.HANR006A003 = rowTanka("MTMR002032").ToString() '単価変更日付（売上）
                    han10r006mitsumorih.HANR006A004 = rowTanka("MTMR002035").ToString() '単価変更日付（仕入）
                    '運賃(実行管理テーブルを引き当て)
                    han10r006mitsumorih.HANR006A005 = Me.Biz04.FareSearch(mtm10r004renkei.MTMR004002)
                    han10r006mitsumorih.HANR006A006 = "10002" '備考コード(04/15)
                    han10r006mitsumorih.HANR006A007 = "以上、よろしくお願い申し上げます。" '備考
                    han10r006mitsumorih.HANR006A008 = "0" '社内メモコード
                    han10r006mitsumorih.HANR006A009 = "mitsumo" '社内メモ
                    han10r006mitsumorih.HANR006A010 = " " '添付フォルダパス
                    han10r006mitsumorih.HANR006A011 = "0" '成約行数
                    '単価台帳参照行数
                    han10r006mitsumorih.HANR006A012 = han10r006mitsumorih.HANR006018
                    han10r006mitsumorih.HANR006A013 = "1" '成約区分
                    han10r006mitsumorih.HANR006A014 = conditionsDetailDate2 '成約日時
                    '成約者ID(起動時のログインIDをセット)
                    han10r006mitsumorih.HANR006A015 = "BULK UPDATE" '成約者ID
                    han10r006mitsumorih.HANR006A016 = "0" '見積書印刷日時
                    han10r006mitsumorih.HANR006A017 = "0" '見積書送信区分
                    han10r006mitsumorih.HANR006A018 = "0" '見積書個別編集区分
                    han10r006mitsumorih.HANR006A019 = "0" '見積書送信日時
                    han10r006mitsumorih.HANR006A020 = " " '見積書送信者ID
                    '登録者ID(起動時のログインIDをセット)
                    'han10r006mitsumorih.HANR006A021 = Me.FormMenu.ModelMtmUser.MTMM002001 '登録者ID
                    han10r006mitsumorih.HANR006A021 = "BULK UPDATE" '登録者ID
                    '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                    han10r006mitsumorih.HANR006A022 = dtToday.ToString("yyyyMMddHHmmss.fff") '登録日時
                    han10r006mitsumorih.HANR006A023 = "MOBILE9999X" '登録端末
                    '更新者ID(起動時のログインIDをセット)
                    'han10r006mitsumorih.HANR006A024 = Me.FormMenu.ModelMtmUser.MTMM002001 '更新者ID
                    han10r006mitsumorih.HANR006A024 = "BULK UPDATE" '更新者ID
                    '日付設定(整数:西暦年月日時分秒 小数:ミリ秒)
                    han10r006mitsumorih.HANR006A025 = dtToday.ToString("yyyyMMddHHmmss.fff") '更新日時
                    han10r006mitsumorih.HANR006A026 = "MOBILE9999X" '更新端末
                    han10r006mitsumorih.HANR006A027 = " " '代理作成者ID
                    han10r006mitsumorih.HANR006A028 = "2" '成約分類
                    han10r006mitsumorih.HANR006A029 = "0" '台帳反映区分
                    han10r006mitsumorih.HANR006A030 = "0" '旧単価印字区分
                    han10r006mitsumorih.HANR006A031 = "2" '見積分類
                    han10r006mitsumorih.HANR006A032 = "0" '図面区分
                    han10r006mitsumorih.HANR006A033 = "0" '見積依頼区分
                    han10r006mitsumorih.HANR006A034 = "0" '仕入先見積区分
                    han10r006mitsumorih.HANR006A035 = "0" 'その他区分
                    han10r006mitsumorih.HANR006A036 = "0" '合計金額表示区分
                    han10r006mitsumorih.HANR006A037 = "" '拡張備考０１(04/15)
                    han10r006mitsumorih.HANR006A038 = "" '拡張備考０２(04/15)
                    han10r006mitsumorih.HANR006A039 = "" '拡張備考０３(04/15)
                    han10r006mitsumorih.HANR006A040 = "" '拡張備考０４(04/15)
                    han10r006mitsumorih.HANR006A041 = "" '拡張備考０５(04/15)
                    han10r006mitsumorih.HANR006A042 = "" '拡張備考０６(04/15)
                    han10r006mitsumorih.HANR006A043 = "" '拡張備考０７(04/15)
                    han10r006mitsumorih.HANR006A044 = "" '拡張備考０８(04/15)
                    han10r006mitsumorih.HANR006A045 = "" '拡張備考０９(04/15)
                    han10r006mitsumorih.HANR006A046 = "" '拡張備考１０(04/15)
                    han10r006mitsumorih.HANR006A047 = "" '拡張備考１１(04/15)
                    han10r006mitsumorih.HANR006A048 = "" '拡張備考１２(04/15)
                    han10r006mitsumorih.HANR006A049 = "" '拡張備考１３(04/15)
                    han10r006mitsumorih.HANR006A050 = "" '拡張備考１４(04/15)
                    han10r006mitsumorih.HANR006A051 = "" '拡張備考１５(04/15)
                    han10r006mitsumorih.HANR006A052 = "" '拡張備考１６(04/15)
                    han10r006mitsumorih.HANR006A053 = "" '拡張備考１７(04/15)
                    han10r006mitsumorih.HANR006A054 = "" '拡張備考１８(04/15)
                    han10r006mitsumorih.HANR006A055 = "" '拡張備考１９(04/15)
                    han10r006mitsumorih.HANR006A056 = "" '拡張備考２０(04/15)
                    han10r006mitsumorih.HANR006A057 = "" '拡張備考２１(04/15)
                    han10r006mitsumorih.HANR006A058 = "" '拡張備考２２(04/15)
                    han10r006mitsumorih.HANR006A059 = "" '拡張備考２３(04/15)
                    han10r006mitsumorih.HANR006A060 = "" '拡張備考２４(04/15)
                    han10r006mitsumorih.HANR006A061 = "" '拡張備考２５(04/15)
                    han10r006mitsumorih.HANR006A062 = "" '拡張備考２６(04/15)
                    han10r006mitsumorih.HANR006A063 = "" '拡張備考２７(04/15)
                    han10r006mitsumorih.HANR006A064 = "" '拡張備考２８(04/15)
                    han10r006mitsumorih.HANR006A065 = "" '拡張備考２９(04/15)
                    han10r006mitsumorih.HANR006A066 = "" '拡張備考３０(04/15)
                    han10r006mitsumorih.HANR006A067 = "0" '消費税金額表示区分

                    han10r006mitsumorih.SetDataRow(tableHAN10R006MITSUMORIH)
                    '----------------------------------
                    '③見積ヘッダーテーブルを生成 END
                    '----------------------------------
                    '-------------------------------
                    '③-1明細拡張テーブル（ヘッダー）を生成 START 4/17
                    '-------------------------------
                    han10r030kakucho_h.HANR030001 = 4
                    han10r030kakucho_h.HANR030002 = 1
                    han10r030kakucho_h.HANR030003 = 0
                    han10r030kakucho_h.HANR030004 = kariNum
                    han10r030kakucho_h.HANR030005 = 0
                    han10r030kakucho_h.HANR030006 = 0
                    han10r030kakucho_h.HANR030007 = ""
                    han10r030kakucho_h.HANR030008 = kariNum
                    han10r030kakucho_h.HANR030009 = ""
                    han10r030kakucho_h.HANR030010 = ""
                    han10r030kakucho_h.HANR030011 = ""
                    han10r030kakucho_h.HANR030012 = ""
                    han10r030kakucho_h.HANR030013 = ""
                    han10r030kakucho_h.HANR030014 = ""
                    han10r030kakucho_h.HANR030015 = ""
                    han10r030kakucho_h.HANR030016 = ""
                    han10r030kakucho_h.HANR030017 = ""
                    han10r030kakucho_h.HANR030018 = ""
                    han10r030kakucho_h.HANR030019 = ""
                    han10r030kakucho_h.HANR030020 = ""
                    han10r030kakucho_h.HANR030021 = ""
                    han10r030kakucho_h.HANR030022 = ""
                    han10r030kakucho_h.HANR030023 = ""
                    han10r030kakucho_h.HANR030024 = ""
                    han10r030kakucho_h.HANR030025 = ""
                    'IF 価格入力テーブル・宛先名（同一得意先の１行目）
                    han10r030kakucho_h.HANR030026 = Me.Biz04.AddressNameSearch(Me.SearchCondition.KakakuNyuuryokuNo, rowTanka("MTMR002001").ToString().Trim())
                    han10r030kakucho_h.HANR030999 = 0
                    han10r030kakucho_h.HANR030INS = dtToday.ToString("yyyyMMddHHmmss.fff")
                    han10r030kakucho_h.HANR030UPD = dtToday.ToString("yyyyMMddHHmmss.fff")
                    '見積ヘッダーデータ作成
                    han10r030kakucho_h.SetDataRow(tableHAN10R030KAKUCHO_H)
                    '-------------------------------
                    '③-1明細拡張テーブル（ヘッダー）を生成 END 4/17
                    '-------------------------------
                End If

                If (progressNum = readCount) Then
                    dialogProgress.Message = "取込中　" + readCount.ToString + "件"
                    progressNum = readCount + 100
                End If

                'キャンセルされた時はループを抜ける
                If dialogProgress.UserCanceled Then
                    Exit For
                End If

                readCount += 1

            Next

            If (tableMTM10R001TANKA.Rows.Count = 0) Then
                dialogProgress.Close()
                Me.Activate()
                MessageBox.Show("対象データがありません")
                Exit Sub
            End If

            '------------------------
            '②見積明細テーブルを生成
            '　消費税データを作成
            '③見積ヘッダーテーブルを生成
            '④見積処理連番を採番
            '⑥数量別単価台帳マスターの生成
            '⑦単価台帳マスターの生成
            '------------------------
            'メッセージを変更する
            Dim renbanStart As Decimal
            Dim renbanMax As Decimal
            dialogProgress.Message = "更新処理中"
            '画面の処理のみ対応（No173見積データがTRUEではない場合は採番はしない）
            If (chk002) Then
                Dim RenbanErrorList = Me.Biz04_1.UpdateRenbanManage(kariNumList, renbanStart, renbanMax, sysError)
                If RenbanErrorList.Count > 0 Then
                    For Each errorMessage As String In RenbanErrorList
                        MessageBox.Show(errorMessage)
                        dialogProgress.Close()
                        Me.Activate()
                        Exit Sub
                    Next
                End If
            Else
                renbanStart = 0
                renbanMax = 0
            End If
            Dim EstimateErrorList = Me.Biz04.EstimateProductions(kariNumList,
                                                                tableMTM10R001TANKA,
                                                                tableHAN10R006MITSUMORIH,
                                                                tableHAN10R007MITSUMORIM,
                                                                tableHAN10R030KAKUCHO_H,
                                                                tableHAN10R030KAKUCHO_M,
                                                                tableHAN98MA01TANKA,
                                                                renbanStart,
                                                                renbanMax,
                                                                sysError)
            If EstimateErrorList.Count > 0 Then
                For Each errorMessage As String In EstimateErrorList
                    MessageBox.Show(errorMessage)
                    dialogProgress.Close()
                    Me.Activate()
                    Exit Sub
                Next
            End If
            'Dim EstimateErrorList = Me.Biz04.EstimateProduction(kariNumList, tableMTM10R001TANKA, tableHAN10R006MITSUMORIH, tableHAN10R007MITSUMORIM, tableHAN98MA01TANKA, sysError)
            'If EstimateErrorList.Count > 0 Then
            '    For Each errorMessage As String In EstimateErrorList
            '        MessageBox.Show(errorMessage)
            '        dialogProgress.Close()
            '        Me.Activate()
            '        Exit Sub
            '    Next
            'End If

            '------------------------
            '⑧見積明細ファイル、見積ヘッダーファイルの更新スクリプト生成
            '------------------------
            dialogProgress.Message = "スクリプト出力中"
            Dim fileDirectory As String = Me.TextBox013.Text
            Dim OutputErrorList = Me.Biz04.SqlScriptOutput(fileDirectory,
                                                           Me.SearchCondition.KakakuNyuuryokuNo,
                                                           tableHAN10R006MITSUMORIH,
                                                           tableHAN10R007MITSUMORIM,
                                                           tableHAN10R030KAKUCHO_H,
                                                           tableHAN10R030KAKUCHO_M,
                                                           tableHAN98MA01TANKA,
                                                           tableHAN98MA02SUTANKA,
                                                           sysError, chk001, chk002)
            If OutputErrorList.Count > 0 Then
                For Each errorMessage As String In OutputErrorList
                    MessageBox.Show(errorMessage)
                    dialogProgress.Close()
                    Me.Activate()
                    Exit Sub
                Next
            End If

            '------------------------
            '出力時に指定したフォルダ名は制御マスタに記録
            '------------------------
            dialogProgress.Message = "制御マスタ更新中"
            Dim RecordErrorList = Me.Biz04.DataUpdateSeigyo(Me.TextBox013.Text)
            If RecordErrorList.Count > 0 Then
                For Each errorMessage As String In RecordErrorList
                    MessageBox.Show(errorMessage)
                    dialogProgress.Close()
                    Me.Activate()
                    Exit Sub
                Next
            End If

            '------------------------
            '連携管理テーブルを登録
            '------------------------
            If (chk002) Then
                dialogProgress.Message = "連携管理テーブル追加中"
                '取込終了時間
                dtEndTime = DateTime.Now
                endTime = dtEndTime.ToString("HHmmss")
                mtm10r004renkei.MTMR004015 = endTime
                '単価台帳の件数を取得
                mtm10r004renkei.MTMR004012 = tableHAN98MA01TANKA.Rows.Count()
                '見積書の件数を取得
                mtm10r004renkei.MTMR004013 = tableHAN10R006MITSUMORIH.Rows.Count()
                Dim InsertErrorList = Me.Biz04.DataInsertRenkei(mtm10r004renkei, tableHAN10R006MITSUMORIH, sysError)
                If InsertErrorList.Count > 0 Then
                    For Each errorMessage As String In InsertErrorList
                        MessageBox.Show(errorMessage)
                        dialogProgress.Close()
                        Me.Activate()
                        Exit Sub
                    Next
                End If
            End If

            dialogProgress.Message = "完了"
            dialogProgress.Close()
            Me.Activate()
            MessageBox.Show("システム間連携の処理が完了しました。")

        Catch ex As Exception
            dialogProgress.Close()
            Me.Activate()
            If (Not String.IsNullOrEmpty(sysError)) Then
                MessageBox.Show("システムエラーが発生しました" & vbCrLf & sysError)
            Else
                MessageBox.Show("システムエラーが発生しました")
            End If
            Exit Sub
        End Try

    End Sub
    ''' <summary>
    ''' シミュレーション番号検索ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button001_Click(sender As Object, e As EventArgs) Handles Button001.Click
        Dim formSearch As New FormMTMSearchJitsukou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox001.Text = formSearch.Selected.MTMR003001
            Me.TextBox002.Text = formSearch.Selected.MTMR003002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者検索1ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button002_Click(sender As Object, e As EventArgs) Handles Button002.Click
        Dim formSearch As New FormMTMSearchUser
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox003.Text = formSearch.Selected.MTMM002001
            Me.TextBox004.Text = formSearch.Selected.MTMM002002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 担当者検索2ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button003_Click(sender As Object, e As EventArgs) Handles Button003.Click
        Dim formSearch As New FormMTMSearchUser
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox005.Text = formSearch.Selected.MTMM002001
            Me.TextBox006.Text = formSearch.Selected.MTMM002002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 営業所検索ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button004_Click(sender As Object, e As EventArgs) Handles Button004.Click
        Dim formSearch As New FormMTMSearchEigyou
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox007.Text = formSearch.Selected.HANM036001
            Me.TextBox008.Text = formSearch.Selected.HANM036002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' フォームオープン時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTM04_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim formConfirm As New FormMTMConfirm

        Dim result As DialogResult = formConfirm.ShowDialog()

        'ログインIDを記録
        Me.Biz04.LoginId = Me.FormMenu.ModelMtmUser.MTMM002001

        If result = DialogResult.OK Then
            Me.Biz04.Password = formConfirm.Password
            formConfirm.Dispose()
        Else
            MessageBox.Show("起動権限がありません")
            formConfirm.Dispose()
            Me.FormMenu.Show()
            Me.Close()
        End If

        Me.CheckBox001.Checked = True
        Me.CheckBox002.Checked = True

        Me.TextBox001.Text = ""
        Me.TextBox002.Text = ""
        If (Not String.IsNullOrEmpty(Me.TextBox001.Text)) Then
            Me.TextBox002.Text = Me.Biz04.GetKakakuName(Me.TextBox001.Text)
        End If
        Me.TextBox003.Text = ""
        Me.TextBox004.Text = ""
        Me.TextBox005.Text = ""
        Me.TextBox006.Text = ""
        Me.TextBox007.Text = ""
        Me.TextBox008.Text = ""
        Me.TextBox009.Text = ""
        Me.TextBox010.Text = ""
        Me.TextBox011.Text = ""
        Me.TextBox011.Text = ""
        '取込ファイル名を取得してセット
        Me.TextBox013.Text = Me.Biz04.OutputFileNameSearch()

    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理(FROM)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button005_Click(sender As Object, e As EventArgs) Handles Button005.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox009.Text = formSearch.Selected.HANM001003
            Me.TextBox010.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' 得意先コード検索ボタンクリック処理(TO)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button006_Click(sender As Object, e As EventArgs) Handles Button006.Click
        Dim formSearch As New FormMTMSearchTokui
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox011.Text = formSearch.Selected.HANM001003
            Me.TextBox012.Text = formSearch.Selected.HANM001004
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' ファイルオープンボタンクリック処理（案内文の指定）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button007_Click(sender As Object, e As EventArgs) Handles Button007.Click
        Dim ofDialog As New FolderBrowserDialog
        ofDialog.Description = "出力先のファイルを選択してください"
        ofDialog.RootFolder = System.Environment.SpecialFolder.MyComputer
        If (Not String.IsNullOrEmpty(Me.TextBox013.Text)) Then ofDialog.SelectedPath = Me.TextBox013.Text + "\"
        ofDialog.ShowNewFolderButton = True

        If ofDialog.ShowDialog = DialogResult.OK Then
            Me.TextBox013.Text = ofDialog.SelectedPath
        End If
        ofDialog.Dispose()
    End Sub
    ''' <summary>
    ''' 変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox001_TextChanged(sender As Object, e As EventArgs) Handles TextBox001.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox001.Text))) Then
            Me.TextBox002.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' 変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox003_TextChanged(sender As Object, e As EventArgs) Handles TextBox003.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox003.Text))) Then
            Me.TextBox004.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' 変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox005_TextChanged(sender As Object, e As EventArgs) Handles TextBox005.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox005.Text))) Then
            Me.TextBox006.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' 変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox007_TextChanged(sender As Object, e As EventArgs) Handles TextBox007.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox007.Text))) Then
            Me.TextBox008.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' 変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox009_TextChanged(sender As Object, e As EventArgs) Handles TextBox009.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox009.Text))) Then
            Me.TextBox010.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' 変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox011_TextChanged(sender As Object, e As EventArgs) Handles TextBox011.TextChanged
        If (String.IsNullOrEmpty(Trim(Me.TextBox011.Text))) Then
            Me.TextBox012.Text = ""
        End If
    End Sub
End Class