Imports System.IO
Imports AdvanceSoftware.VBReport

Namespace Biz
    ''' <summary>
    ''' PrintMTM03
    ''' 見積書印刷処理クラス
    ''' </summary>
    ''' 
    Public Class MTM03Print

        Private WithEvents CellReport As New AdvanceSoftware.VBReport.CellReport

        '1ページに印刷する最大行
        Private Const MAXROWS As Integer = 14
        Private Const MAXROWS2 As Integer = 24
        'シート名（基本は固定でTemplateとする）
        Private Const SHEETNAME As String = "Template"
        Private Const SHEETNAME2 As String = "Template2"

        Public Function Print2(ByVal result As MTM03SearchResult, ByVal outputPath As String, ByVal templatePath As String, ByRef createPdf As String) As Boolean
            Dim PrintItem As New H_Print

            'プリント設定
            PrintItem.ErrorCode = ""  'エラーコードを初期化
            PrintItem.TemplateDirectory = templatePath 'テンプレートファイルパス
            PrintItem.TemplateFileName = "見積書１.xlsx"  'テンプレートファイル名

            PrintItem.OutputDirectory = outputPath
            PrintItem.PrintType = "見積書" '出力時のファイル名

            'テンプレートファイルのチェック

            PrintItem.TemplateFilePath = PrintCommon.CombineFolderAndFile(1, PrintItem)
            If Not PrintCommon.TemplateFileCheck(PrintItem) Then
                Return False
            End If

            Try
                If Not result.ElementList.Count > 0 Then
                    '印刷対象データが無い
                    PrintItem.ErrorCode = PrintCommon.ErrorCode.COM_WARN_PRT_NODATA.ToString
                    Return False
                Else
                    '印刷処理を実行
                    'テンプレートファイルを読み込む
                    CellReport.FileName = PrintItem.TemplateFilePath
                    CellReport.Report.Start(ReportMode.Speed)
                    CellReport.Report.File()
                    'CellReport.Page.Start(SHEETNAME, "1")
                    CellReport.Section.Start(SHEETNAME, False)
                    CellReport.Section.ReportToFill = False
                    CellReport.Section.MaxDetailCount = 0

                    Dim recCount As Integer = 0
                    Dim v_offset As Integer = 0
                    Dim totalCount As Double = 0

                    For Each rec As MTM03SearchResultElement In result.ElementList
                        totalCount += 1

                        If totalCount <> 1 Then
                            Continue For
                        End If

                        '見積書ヘッダー作成
                        With CellReport
                            'ヘッダーの出力
                            .Cell("**PrintedAt").Value = Date.Now.ToString("yyyy/MM/dd")
                            Dim tokuisakiName As String
                            Dim spelling As String = ""
                            If Not String.IsNullOrEmpty(Trim(rec.AtesakiName)) Then
                                If rec.AtesakiName.LastIndexOf("様") = (rec.AtesakiName.Length - 1) Then
                                    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2 + "　" + rec.AtesakiName
                                    spelling = ""
                                Else
                                    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2 + "　" + rec.AtesakiName
                                    spelling = "御中"
                                End If
                            Else
                                tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                                spelling = "御中"
                            End If
                            .Cell("**CompanyName").Value = tokuisakiName
                            .Cell("**Spelling").Value = spelling
                            .Cell("**FaxNo").Value = rec.AtesakiFaxNo
                            .Cell("**Subject").Value = rec.JitsukouKakakuNyuuryokuName
                            .Cell("**RevisedImplementationDate").Value = rec.NeageDate + "　" + rec.Kaiteijitsusi
                            .Cell("**Fare").Value = rec.Unchin
                            .Cell("**OwnCompany").Value = rec.KaisyaName
                            .Cell("**Office").Value = rec.EigyosyoName
                            .Cell("**Manager").Value = rec.TantoName
                            .Cell("**PostCode").Value = rec.PostCode
                            .Cell("**Address").Value = rec.Address
                            .Cell("**TelFaxNo").Value = rec.PhoneFax
                        End With

                        ' 見積明細行の組立
                        Dim oldSyohinCode As String = ""
                        Dim oldKikaku As String = ""
                        Dim topNohinHistory As String = ""
                        Dim detailScrutinyList As New List(Of MTM03SearchResultElementDetail)
                        Dim rowCounter As Integer = 0
                        Dim rowNumber As Integer = 0
                        For Each detail As MTM03SearchResultElementDetail In rec.ElementDetailList
                            Dim dataRow As New MTM03SearchResultElementDetail
                            If ((Not oldSyohinCode = detail.SyohinCode.Trim) Or (Not oldKikaku = detail.Kikaku.Trim)) Then
                                rowCounter += 1
                                rowNumber = 1
                                dataRow.RowId = rowCounter
                                dataRow.RowIdVal = rowCounter
                                dataRow.RowDetailNo = rowNumber
                                dataRow.SyohinName = detail.SyohinName
                                dataRow.Irisu = detail.Irisu
                                dataRow.Tanni = detail.Tanni
                                dataRow.Lot = detail.Lot
                                dataRow.NewTanka = detail.NewTanka
                                dataRow.OldTanka = detail.OldTanka
                                dataRow.SaisyuDate = detail.SaisyuDate
                                dataRow.Bikou = detail.Bikou
                                dataRow.NohinHistory = ""
                                If Not String.IsNullOrEmpty(Trim(detail.NohinHistory)) Then
                                    dataRow.NohinHistory = detail.NohinHistory
                                    topNohinHistory = detail.NohinHistory
                                End If
                            Else
                                rowNumber += 1
                                dataRow.RowId = 0
                                dataRow.RowIdVal = rowCounter
                                dataRow.RowDetailNo = rowNumber
                                dataRow.SyohinName = ""
                                dataRow.Irisu = detail.Irisu
                                dataRow.Tanni = detail.Tanni
                                dataRow.Lot = detail.Lot
                                dataRow.NewTanka = detail.NewTanka
                                dataRow.OldTanka = detail.OldTanka
                                dataRow.SaisyuDate = detail.SaisyuDate
                                dataRow.Bikou = detail.Bikou
                                If String.IsNullOrEmpty(Trim(topNohinHistory)) Then
                                    dataRow.NohinHistory = detail.NohinHistory
                                    topNohinHistory = detail.NohinHistory
                                Else
                                    dataRow.NohinHistory = topNohinHistory
                                End If
                            End If

                            'リストへ詰めなおし
                            detailScrutinyList.Add(dataRow)
                            '比較文字列を設定
                            oldSyohinCode = detail.SyohinCode.Trim
                            oldKikaku = detail.Kikaku.Trim
                        Next

                        'ページ作成
                        Dim detailList As New List(Of MTM03SearchResultElementDetail) 'ページ付きリスト
                        Dim pageCounter As Integer = 1
                        Dim maxPageLimit As Integer = MAXROWS
                        rowCounter = 0
                        For Each pageDetail As MTM03SearchResultElementDetail In detailScrutinyList

                            If Not pageDetail.RowId = 0 Then
                                'RowIdが0以上なら納品先履歴もカウントする
                                rowCounter += 2
                                pageDetail.RowId = pageDetail.RowId
                                pageDetail.SyohinName = pageDetail.SyohinName
                                pageDetail.Irisu = pageDetail.Irisu
                                pageDetail.Tanni = pageDetail.Tanni
                                pageDetail.Lot = pageDetail.Lot
                                pageDetail.NewTanka = pageDetail.NewTanka
                                pageDetail.OldTanka = pageDetail.OldTanka
                                pageDetail.SaisyuDate = pageDetail.SaisyuDate
                                pageDetail.Bikou = pageDetail.Bikou
                                pageDetail.NohinHistory = pageDetail.NohinHistory
                            Else
                                'RowIdが0なら納品先履歴もカウントしない
                                rowCounter += 1
                                pageDetail.RowId = pageDetail.RowId
                                pageDetail.SyohinName = pageDetail.SyohinName
                                pageDetail.Irisu = pageDetail.Irisu
                                pageDetail.Tanni = pageDetail.Tanni
                                pageDetail.Lot = pageDetail.Lot
                                pageDetail.NewTanka = pageDetail.NewTanka
                                pageDetail.OldTanka = pageDetail.OldTanka
                                pageDetail.SaisyuDate = pageDetail.SaisyuDate
                                pageDetail.Bikou = pageDetail.Bikou
                                pageDetail.NohinHistory = pageDetail.NohinHistory
                            End If

                            If rowCounter > maxPageLimit Then
                                'ページをカウントする
                                pageCounter += 1
                                maxPageLimit = maxPageLimit + MAXROWS

                            End If

                            pageDetail.PageNo = pageCounter

                            'リストへ詰めなおし
                            detailList.Add(pageDetail)

                        Next

                        '見積書明細
                        Dim rowNo = 16
                        Dim rowCount As Integer = 0
                        Dim pageMax1 As Integer = 26
                        Dim pageMax2Over As Integer = 26
                        Dim maxCount As Integer = detailList.Count
                        Dim targetPage As Integer = 1


                        For Each detail As MTM03SearchResultElementDetail In detailList

                            rowCount += 1
                            'If detail.RowIdVal > 3 Then
                            '    Continue For
                            'End If

                            '改ページ判断
                            If detail.PageNo <> targetPage Then  '１ページ以外
                                pageMax1 = pageMax1 + 12
                                If detail.PageNo <> 1 Then
                                    'ページ替え処理
                                    rowNo = 16
                                    CellReport.Section.End()
                                    CellReport.Section.Start(SHEETNAME, False)
                                    'CellReport.Page.End()
                                    'CellReport.Page.Start(SHEETNAME, "1")
                                    targetPage = detail.PageNo
                                End If
                            End If

                            'セクションを設定
                            Dim SectionDetail As SectionDetail = CellReport.Section.Detail("A16:BN16")

                            '対象RowIdのマックス行番号を取得
                            Dim rowMaxNum As Integer = detailList.Where(Function(n) n.RowIdVal = detail.RowIdVal) _
                                                .Select(Function(n) n.RowDetailNo).Max()

                            With SectionDetail

                                .Start()

                                'Noの列
                                .Cell("C" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("C" & rowNo).Attr.Box(BoxType.Over, BorderStyle.Thin, xlColor.Black)
                                .Cell("C" & rowNo).Value = detail.RowId
                                '商品名の列
                                .Cell("E" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("E" & rowNo).Value = detail.SyohinName.Trim
                                '入数の列
                                .Cell("Y" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("Y" & rowNo).Value = detail.Irisu
                                '単位の列
                                .Cell("AB" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("AB" & rowNo).Value = detail.Tanni
                                'ロットの列
                                .Cell("AE" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("AE" & rowNo).Value = detail.Lot.Trim
                                '新単価の列
                                .Cell("AH" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("AH" & rowNo).Value = detail.NewTanka
                                '旧単価の列
                                .Cell("AN" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("AN" & rowNo).Value = detail.OldTanka
                                '最終実績の列
                                .Cell("AT" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("AT" & rowNo).Value = detail.SaisyuDate
                                '備考の列
                                .Cell("AY" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                .Cell("BN" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Thin, xlColor.Black)
                                .Cell("AY" & rowNo).Value = detail.Bikou

                                .Next()

                                ''納品先履歴を表示するか、行削除するかを判断
                                'If rowMaxNum <> detail.RowDetailNo Then
                                '    '行数カウンターを+1(納品先履歴がないため）
                                '    rowNo += 1
                                'Else
                                '    '納品先履歴の列
                                '    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                '    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                '    .Cell("E" & rowNo + 1).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                '    .Cell("BN" & rowNo + 1).Attr.Box(BoxType.Right, BorderStyle.Thin, xlColor.Black)
                                '    .Cell("E" & rowNo + 1).Attr.Box(BoxType.Over, BorderStyle.Thin, xlColor.Black)
                                '    .Cell("E" & rowNo + 1).Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                '    .Cell("E" & rowNo + 1).Value = detail.NohinHistory
                                '    rowNo += 2
                                'End If

                            End With
                        Next



                        'Exit For
                        v_offset = 0
                    Next
                End If

                '終了処理
                CellReport.Section.End()
                'CellReport.Page.End()
                CellReport.Report.End()

                'ファイル出力する
                createPdf = PrintCommon.GetTemporaryFileName(PrintItem.PrintType) + ".pdf"
                PrintItem.TemporaryFileName = createPdf
                CellReport.Report.SavePdf(PrintCommon.CombineFolderAndFile(2, PrintItem))

                Return True

            Catch ex As Exception
                PrintItem.ErrorCode = PrintCommon.ErrorCode.COM_WARN_PRT_PRINT_ERROR.ToString
                PrintItem.ErrorMessage = ex.Message.ToString
                Return False
            End Try

        End Function

        Public Function Print(ByVal result As MTM03SearchResult, ByVal outputPath As String, ByVal templatePath As String, ByVal createPdf As String) As Boolean
            Dim PrintItem As New H_Print

            'プリント設定
            PrintItem.ErrorCode = ""  'エラーコードを初期化
            PrintItem.TemplateDirectory = templatePath 'テンプレートファイルパス
            PrintItem.TemplateFileName = "MTM03.xlsx"  'テンプレートファイル名

            PrintItem.OutputDirectory = outputPath
            'PrintItem.PrintType = "見積書" '出力時のファイル名
            PrintItem.PrintType = createPdf '出力時のファイル名

            'テンプレートファイルのチェック

            PrintItem.TemplateFilePath = PrintCommon.CombineFolderAndFile(1, PrintItem)
            If Not PrintCommon.TemplateFileCheck(PrintItem) Then
                Return False
            End If

            Try
                If Not result.ElementList.Count > 0 Then
                    '印刷対象データが無い
                    PrintItem.ErrorCode = PrintCommon.ErrorCode.COM_WARN_PRT_NODATA.ToString
                    Return False
                Else
                    '印刷処理を実行
                    'テンプレートファイルを読み込む
                    CellReport.FileName = PrintItem.TemplateFilePath
                    CellReport.Report.Start(ReportMode.Speed)
                    CellReport.Report.File()

                    Dim totalCount As Double = 0
                    For Each rec As MTM03SearchResultElement In result.ElementList

                        totalCount += 1
                        'If totalCount <> 3 Then
                        '    Continue For
                        'End If

                        'ページ開始宣言
                        CellReport.Page.Start(SHEETNAME, "1")

                        '見積書ヘッダー作成
                        With CellReport
                            'ヘッダーの出力
                            .Cell("**Hed01").Value = "M" + rec.KakakuNyuuryokuNo + "-" + rec.TokuisakiCode.Trim
                            .Cell("**PrintedAt").Value = Date.Now.ToString("yyyy/MM/dd")

                            '修正後のCompanyNameの設定
                            Dim tokuisakiName As String
                            Dim atesakiName As String
                            Dim spelling1 As String = ""
                            Dim spelling2 As String = ""
                            Dim pattern As String = "2"
                            If Not String.IsNullOrEmpty(Trim(rec.AtesakiName)) Then
                                If rec.AtesakiName.LastIndexOf("様") = (rec.AtesakiName.Length - 1) Then
                                    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                                    atesakiName = rec.AtesakiName
                                    spelling1 = ""
                                    spelling2 = ""
                                    pattern = "1"
                                Else
                                    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                                    atesakiName = rec.AtesakiName
                                    spelling1 = ""
                                    spelling2 = "御中"
                                    pattern = "2"
                                End If
                            Else
                                tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                                atesakiName = ""
                                spelling1 = "御中"
                                spelling2 = ""
                                pattern = "3"
                            End If
                            '.Cell("BA6:BF8").Drawing.AddImage(Path.Combine(templatePath, "社印.EMF"))
                            .Cell("**CompanyName1").Value = tokuisakiName
                            .Cell("**CompanyName2").Value = atesakiName
                            If pattern = "1" Then
                                .Cell("**Spelling1").Value = spelling1
                                .Cell("**Spelling2").Value = spelling2
                                '.Cell("C5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                '.Cell("AE5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                .Cell("C6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                            ElseIf pattern = "2" Then
                                .Cell("**Spelling1").Value = spelling1
                                .Cell("**Spelling2").Value = spelling2
                                .Cell("C6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                .Cell("AE6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                '.Cell("C6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                            ElseIf pattern = "3" Then
                                .Cell("**Spelling1").Value = spelling1
                                .Cell("**Spelling2").Value = spelling2
                                .Cell("C5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                .Cell("AE5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                            End If

                            '修正前のCompanyNameの設定
                            'Dim tokuisakiName As String
                            'Dim spelling As String = ""
                            'If Not String.IsNullOrEmpty(Trim(rec.AtesakiName)) Then
                            '    If rec.AtesakiName.LastIndexOf("様") = (rec.AtesakiName.Length - 1) Then
                            '        tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2 + "　" + rec.AtesakiName
                            '        spelling = ""
                            '    Else
                            '        tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2 + "　" + rec.AtesakiName
                            '        spelling = "御中"
                            '    End If
                            'Else
                            '    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                            '    spelling = "御中"
                            'End If
                            ''.Cell("BA6:BF8").Drawing.AddImage(Path.Combine(templatePath, "社印.EMF"))
                            '.Cell("**CompanyName").Value = tokuisakiName
                            '.Cell("**Spelling").Value = spelling

                            .Cell("**FaxNo").Value = rec.AtesakiFaxNo
                            .Cell("**Subject").Value = rec.JitsukouKakakuNyuuryokuName
                            .Cell("**RevisedImplementationDate").Value = rec.NeageDate + "　" + rec.Kaiteijitsusi
                            .Cell("**Fare").Value = rec.Unchin
                            .Cell("**OwnCompany").Value = rec.KaisyaName
                            .Cell("**Office").Value = rec.EigyosyoName
                            .Cell("**Manager").Value = rec.TantoName
                            .Cell("**PostCode").Value = rec.PostCode
                            .Cell("**Address").Value = rec.Address
                            .Cell("**TelFaxNo").Value = rec.PhoneFax
                        End With

                        ' 見積明細行の組立
                        Dim oldSyohinCode As String = ""
                        Dim oldKikaku As String = ""
                        Dim topNohinHistory As String = ""
                        Dim detailScrutinyList As New List(Of MTM03SearchResultElementDetail)
                        Dim rowCounter As Integer = 0
                        Dim rowNumber As Integer = 0
                        For Each detail As MTM03SearchResultElementDetail In rec.ElementDetailList
                            Dim dataRow As New MTM03SearchResultElementDetail
                            If ((Not oldSyohinCode = detail.SyohinCode.Trim) Or (Not oldKikaku = detail.Kikaku.Trim)) Then
                                rowCounter += 1
                                rowNumber = 1
                                dataRow.RowId = rowCounter
                                dataRow.RowIdVal = rowCounter
                                dataRow.RowDetailNo = rowNumber
                                dataRow.SyohinName = detail.SyohinName
                                dataRow.Irisu = detail.Irisu
                                dataRow.Tanni = detail.Tanni
                                dataRow.Lot = detail.Lot
                                dataRow.NewTanka = detail.NewTanka
                                dataRow.OldTanka = detail.OldTanka
                                dataRow.SaisyuDate = detail.SaisyuDate
                                dataRow.Bikou = detail.Bikou
                                dataRow.NohinHistory = ""
                                If Not String.IsNullOrEmpty(Trim(detail.NohinHistory)) Then
                                    dataRow.NohinHistory = detail.NohinHistory
                                    topNohinHistory = detail.NohinHistory
                                End If
                            Else
                                rowNumber += 1
                                dataRow.RowId = 0
                                dataRow.RowIdVal = rowCounter
                                dataRow.RowDetailNo = rowNumber
                                dataRow.SyohinName = ""
                                dataRow.Irisu = detail.Irisu
                                dataRow.Tanni = detail.Tanni
                                dataRow.Lot = detail.Lot
                                dataRow.NewTanka = detail.NewTanka
                                dataRow.OldTanka = detail.OldTanka
                                dataRow.SaisyuDate = detail.SaisyuDate
                                dataRow.Bikou = detail.Bikou
                                If String.IsNullOrEmpty(Trim(topNohinHistory)) Then
                                    dataRow.NohinHistory = detail.NohinHistory
                                    topNohinHistory = detail.NohinHistory
                                Else
                                    dataRow.NohinHistory = topNohinHistory
                                End If
                            End If

                            'リストへ詰めなおし
                            detailScrutinyList.Add(dataRow)
                            '比較文字列を設定
                            oldSyohinCode = detail.SyohinCode.Trim
                            oldKikaku = detail.Kikaku.Trim
                        Next

                        'ページ作成
                        Dim detailList As New List(Of MTM03SearchResultElementDetail) 'ページ付きリスト
                        Dim pageCounter As Integer = 1
                        Dim pageDetailCounter As Integer = 1
                        Dim maxPageLimit As Integer = MAXROWS
                        rowCounter = 0
                        For Each pageDetail As MTM03SearchResultElementDetail In detailScrutinyList

                            If Not pageDetail.RowId = 0 Then
                                'RowIdが0以上なら納品先履歴もカウントする
                                rowCounter += 2
                                pageDetail.RowId = pageDetail.RowId
                                pageDetail.SyohinName = pageDetail.SyohinName
                                pageDetail.Irisu = pageDetail.Irisu
                                pageDetail.Tanni = pageDetail.Tanni
                                pageDetail.Lot = pageDetail.Lot
                                pageDetail.NewTanka = pageDetail.NewTanka
                                pageDetail.OldTanka = pageDetail.OldTanka
                                pageDetail.SaisyuDate = pageDetail.SaisyuDate
                                pageDetail.Bikou = pageDetail.Bikou
                                pageDetail.NohinHistory = pageDetail.NohinHistory
                            Else
                                'RowIdが0なら納品先履歴もカウントしない
                                rowCounter += 1
                                pageDetail.RowId = pageDetail.RowId
                                pageDetail.SyohinName = pageDetail.SyohinName
                                pageDetail.Irisu = pageDetail.Irisu
                                pageDetail.Tanni = pageDetail.Tanni
                                pageDetail.Lot = pageDetail.Lot
                                pageDetail.NewTanka = pageDetail.NewTanka
                                pageDetail.OldTanka = pageDetail.OldTanka
                                pageDetail.SaisyuDate = pageDetail.SaisyuDate
                                pageDetail.Bikou = pageDetail.Bikou
                                pageDetail.NohinHistory = pageDetail.NohinHistory
                            End If

                            If rowCounter > maxPageLimit Then
                                'ページをカウントする
                                pageCounter += 1
                                pageDetailCounter = 1
                                maxPageLimit = maxPageLimit + MAXROWS2

                            End If

                            pageDetail.PageNo = pageCounter
                            pageDetail.PageVal = pageDetailCounter

                            pageDetailCounter += 1

                            'リストへ詰めなおし
                            detailList.Add(pageDetail)

                        Next

                        '見積書明細
                        Dim rowNo = 16
                        Dim targetPage As Integer = 1
                        For Each detail As MTM03SearchResultElementDetail In detailList

                            '改ページ判断
                            If detail.PageNo <> targetPage Then  '１ページ以外
                                If detail.PageNo <> 1 Then
                                    'ページ替え処理
                                    rowNo = 5
                                    CellReport.Page.End()
                                    CellReport.Page.Start(SHEETNAME2, "1")
                                    targetPage = detail.PageNo
                                End If
                            End If

                            '対象RowIdのマックス行番号を取得
                            Dim rowMaxNum As Integer = detailList.Where(Function(n) n.RowIdVal = detail.RowIdVal) _
                                                .Select(Function(n) n.RowDetailNo).Max()

                            'ページの最終行を取得
                            Dim pageMaxNum As Integer = detailList.Where(Function(n) n.PageNo = targetPage) _
                                                .Select(Function(n) n.PageVal).Max()

                            With CellReport

                                'Noの列
                                If detail.RowId = 0 Then
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                Else
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Hair, xlColor.Black)
                                    .Cell("C" & rowNo).Value = detail.RowId
                                End If
                                '商品名の列
                                .Cell("E" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("E" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Hair, xlColor.Black)
                                .Cell("E" & rowNo).Value = detail.SyohinName.Trim
                                '入数の列
                                .Cell("Y" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("Y" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Hair, xlColor.Black)
                                .Cell("Y" & rowNo).Value = detail.Irisu
                                '単位の列
                                .Cell("AB" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AB" & rowNo).Value = detail.Tanni
                                'ロットの列
                                .Cell("AE" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AE" & rowNo).Value = detail.Lot.Trim
                                '新単価の列
                                .Cell("AH" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AH" & rowNo).Value = detail.NewTanka
                                '旧単価の列
                                .Cell("AN" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AN" & rowNo).Value = detail.OldTanka
                                '最終実績の列
                                .Cell("AT" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AT" & rowNo).Value = detail.SaisyuDate
                                '備考の列
                                .Cell("AY" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("BN" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Thin, xlColor.Black)
                                .Cell("AY" & rowNo).Value = detail.Bikou
                                '納品先履歴を表示するか、行削除するかを判断
                                If rowMaxNum <> detail.RowDetailNo Then
                                    '行数カウンターを+1(納品先履歴がないため）
                                    .Cell("C" & rowNo + 1).RowHeight = 0
                                Else
                                    '納品先履歴の列
                                    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Over, BorderStyle.Hair, xlColor.Black)
                                    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                    .Cell("BN" & rowNo + 1).Attr.Box(BoxType.Right, BorderStyle.Thin, xlColor.Black)
                                    .Cell("E" & rowNo + 1).Attr.Box(BoxType.Over, BorderStyle.Hair, xlColor.Black)
                                    .Cell("E" & rowNo + 1).Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                    .Cell("E" & rowNo + 1).Value = detail.NohinHistory
                                End If
                                'ページの最終行なら罫線
                                If pageMaxNum = detail.PageVal Then
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("E" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("Y" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AB" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AE" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AH" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AN" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AT" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AY" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                End If
                                rowNo += 2
                            End With
                        Next
                        CellReport.Page.End()
                    Next
                End If

                '終了処理
                CellReport.Report.End()

                'ファイル出力する
                'createPdf = PrintCommon.GetTemporaryFileName(PrintItem.PrintType) + ".pdf"
                PrintItem.TemporaryFileName = createPdf
                CellReport.Report.SavePdf(PrintCommon.CombineFolderAndFile(2, PrintItem))

                Return True

            Catch ex As Exception
                PrintItem.ErrorCode = PrintCommon.ErrorCode.COM_WARN_PRT_PRINT_ERROR.ToString
                PrintItem.ErrorMessage = ex.Message.ToString
                Return False
            End Try

        End Function

        Public Function PrintPreview(ByVal result As MTM03SearchResult, ByVal outputPath As String, ByVal templatePath As String, ByRef createPdf As String) As Boolean
            Dim PrintItem As New H_Print

            'プリント設定
            PrintItem.ErrorCode = ""  'エラーコードを初期化
            PrintItem.TemplateDirectory = templatePath 'テンプレートファイルパス
            PrintItem.TemplateFileName = "MTM03.xlsx"  'テンプレートファイル名

            PrintItem.OutputDirectory = outputPath
            PrintItem.PrintType = "見積書" '出力時のファイル名

            'テンプレートファイルのチェック

            PrintItem.TemplateFilePath = PrintCommon.CombineFolderAndFile(1, PrintItem)
            If Not PrintCommon.TemplateFileCheck(PrintItem) Then
                Return False
            End If

            Try
                If Not result.ElementList.Count > 0 Then
                    '印刷対象データが無い
                    PrintItem.ErrorCode = PrintCommon.ErrorCode.COM_WARN_PRT_NODATA.ToString
                    Return False
                Else
                    '印刷処理を実行
                    'テンプレートファイルを読み込む
                    CellReport.FileName = PrintItem.TemplateFilePath
                    CellReport.Report.Start(ReportMode.Speed)
                    CellReport.Report.File()

                    Dim totalCount As Double = 0
                    For Each rec As MTM03SearchResultElement In result.ElementList

                        totalCount += 1
                        'If totalCount <> 3 Then
                        '    Continue For
                        'End If

                        'ページ開始宣言
                        CellReport.Page.Start(SHEETNAME, "1")

                        '見積書ヘッダー作成
                        With CellReport
                            'ヘッダーの出力
                            .Cell("**Hed01").Value = "M" + rec.KakakuNyuuryokuNo + "-" + rec.TokuisakiCode.Trim
                            .Cell("**PrintedAt").Value = Date.Now.ToString("yyyy/MM/dd")

                            '修正後のCompanyNameの設定
                            Dim tokuisakiName As String
                            Dim atesakiName As String
                            Dim spelling1 As String = ""
                            Dim spelling2 As String = ""
                            Dim pattern As String = "2"
                            If Not String.IsNullOrEmpty(Trim(rec.AtesakiName)) Then
                                If rec.AtesakiName.LastIndexOf("様") = (rec.AtesakiName.Length - 1) Then
                                    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                                    atesakiName = rec.AtesakiName
                                    spelling1 = ""
                                    spelling2 = ""
                                    pattern = "1"
                                Else
                                    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                                    atesakiName = rec.AtesakiName
                                    spelling1 = ""
                                    spelling2 = "御中"
                                    pattern = "2"
                                End If
                            Else
                                tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                                atesakiName = ""
                                spelling1 = "御中"
                                spelling2 = ""
                                pattern = "3"
                            End If
                            '.Cell("BA6:BF8").Drawing.AddImage(Path.Combine(templatePath, "社印.EMF"))
                            .Cell("**CompanyName1").Value = tokuisakiName
                            .Cell("**CompanyName2").Value = atesakiName
                            If pattern = "1" Then
                                .Cell("**Spelling1").Value = spelling1
                                .Cell("**Spelling2").Value = spelling2
                                '.Cell("C5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                '.Cell("AE5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                .Cell("C6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                            ElseIf pattern = "2" Then
                                .Cell("**Spelling1").Value = spelling1
                                .Cell("**Spelling2").Value = spelling2
                                .Cell("C6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                .Cell("AE6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                '.Cell("C6").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                            ElseIf pattern = "3" Then
                                .Cell("**Spelling1").Value = spelling1
                                .Cell("**Spelling2").Value = spelling2
                                .Cell("C5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                .Cell("AE5").Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                            End If

                            '修正前のCompanyNameの設定
                            'Dim tokuisakiName As String
                            'Dim spelling As String = ""
                            'If Not String.IsNullOrEmpty(Trim(rec.AtesakiName)) Then
                            '    If rec.AtesakiName.LastIndexOf("様") = (rec.AtesakiName.Length - 1) Then
                            '        tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2 + "　" + rec.AtesakiName
                            '        spelling = ""
                            '    Else
                            '        tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2 + "　" + rec.AtesakiName
                            '        spelling = "御中"
                            '    End If
                            'Else
                            '    tokuisakiName = rec.TokuisakiName1 + "　" + rec.TokuisakiName2
                            '    spelling = "御中"
                            'End If
                            ''.Cell("BA6:BF8").Drawing.AddImage(Path.Combine(templatePath, "社印.EMF"))
                            '.Cell("**CompanyName").Value = tokuisakiName
                            '.Cell("**Spelling").Value = spelling

                            .Cell("**FaxNo").Value = rec.AtesakiFaxNo
                            .Cell("**Subject").Value = rec.JitsukouKakakuNyuuryokuName
                            .Cell("**RevisedImplementationDate").Value = rec.NeageDate + "　" + rec.Kaiteijitsusi
                            .Cell("**Fare").Value = rec.Unchin
                            .Cell("**OwnCompany").Value = rec.KaisyaName
                            .Cell("**Office").Value = rec.EigyosyoName
                            .Cell("**Manager").Value = rec.TantoName
                            .Cell("**PostCode").Value = rec.PostCode
                            .Cell("**Address").Value = rec.Address
                            .Cell("**TelFaxNo").Value = rec.PhoneFax
                        End With

                        ' 見積明細行の組立
                        Dim oldSyohinCode As String = ""
                        Dim oldKikaku As String = ""
                        Dim topNohinHistory As String = ""
                        Dim detailScrutinyList As New List(Of MTM03SearchResultElementDetail)
                        Dim rowCounter As Integer = 0
                        Dim rowNumber As Integer = 0
                        For Each detail As MTM03SearchResultElementDetail In rec.ElementDetailList
                            Dim dataRow As New MTM03SearchResultElementDetail
                            If ((Not oldSyohinCode = detail.SyohinCode.Trim) Or (Not oldKikaku = detail.Kikaku.Trim)) Then
                                rowCounter += 1
                                rowNumber = 1
                                dataRow.RowId = rowCounter
                                dataRow.RowIdVal = rowCounter
                                dataRow.RowDetailNo = rowNumber
                                dataRow.SyohinName = detail.SyohinName
                                dataRow.Irisu = detail.Irisu
                                dataRow.Tanni = detail.Tanni
                                dataRow.Lot = detail.Lot
                                dataRow.NewTanka = detail.NewTanka
                                dataRow.OldTanka = detail.OldTanka
                                dataRow.SaisyuDate = detail.SaisyuDate
                                dataRow.Bikou = detail.Bikou
                                dataRow.NohinHistory = ""
                                If Not String.IsNullOrEmpty(Trim(detail.NohinHistory)) Then
                                    dataRow.NohinHistory = detail.NohinHistory
                                    topNohinHistory = detail.NohinHistory
                                End If
                            Else
                                rowNumber += 1
                                dataRow.RowId = 0
                                dataRow.RowIdVal = rowCounter
                                dataRow.RowDetailNo = rowNumber
                                dataRow.SyohinName = ""
                                dataRow.Irisu = detail.Irisu
                                dataRow.Tanni = detail.Tanni
                                dataRow.Lot = detail.Lot
                                dataRow.NewTanka = detail.NewTanka
                                dataRow.OldTanka = detail.OldTanka
                                dataRow.SaisyuDate = detail.SaisyuDate
                                dataRow.Bikou = detail.Bikou
                                If String.IsNullOrEmpty(Trim(topNohinHistory)) Then
                                    dataRow.NohinHistory = detail.NohinHistory
                                    topNohinHistory = detail.NohinHistory
                                Else
                                    dataRow.NohinHistory = topNohinHistory
                                End If
                            End If

                            'リストへ詰めなおし
                            detailScrutinyList.Add(dataRow)
                            '比較文字列を設定
                            oldSyohinCode = detail.SyohinCode.Trim
                            oldKikaku = detail.Kikaku.Trim
                        Next

                        'ページ作成
                        Dim detailList As New List(Of MTM03SearchResultElementDetail) 'ページ付きリスト
                        Dim pageCounter As Integer = 1
                        Dim pageDetailCounter As Integer = 1
                        Dim maxPageLimit As Integer = MAXROWS
                        rowCounter = 0
                        For Each pageDetail As MTM03SearchResultElementDetail In detailScrutinyList

                            If Not pageDetail.RowId = 0 Then
                                'RowIdが0以上なら納品先履歴もカウントする
                                rowCounter += 2
                                pageDetail.RowId = pageDetail.RowId
                                pageDetail.SyohinName = pageDetail.SyohinName
                                pageDetail.Irisu = pageDetail.Irisu
                                pageDetail.Tanni = pageDetail.Tanni
                                pageDetail.Lot = pageDetail.Lot
                                pageDetail.NewTanka = pageDetail.NewTanka
                                pageDetail.OldTanka = pageDetail.OldTanka
                                pageDetail.SaisyuDate = pageDetail.SaisyuDate
                                pageDetail.Bikou = pageDetail.Bikou
                                pageDetail.NohinHistory = pageDetail.NohinHistory
                            Else
                                'RowIdが0なら納品先履歴もカウントしない
                                rowCounter += 1
                                pageDetail.RowId = pageDetail.RowId
                                pageDetail.SyohinName = pageDetail.SyohinName
                                pageDetail.Irisu = pageDetail.Irisu
                                pageDetail.Tanni = pageDetail.Tanni
                                pageDetail.Lot = pageDetail.Lot
                                pageDetail.NewTanka = pageDetail.NewTanka
                                pageDetail.OldTanka = pageDetail.OldTanka
                                pageDetail.SaisyuDate = pageDetail.SaisyuDate
                                pageDetail.Bikou = pageDetail.Bikou
                                pageDetail.NohinHistory = pageDetail.NohinHistory
                            End If

                            If rowCounter > maxPageLimit Then
                                'ページをカウントする
                                pageCounter += 1
                                pageDetailCounter = 1
                                maxPageLimit = maxPageLimit + MAXROWS2

                            End If

                            pageDetail.PageNo = pageCounter
                            pageDetail.PageVal = pageDetailCounter

                            pageDetailCounter += 1

                            'リストへ詰めなおし
                            detailList.Add(pageDetail)

                        Next

                        '見積書明細
                        Dim rowNo = 16
                        Dim targetPage As Integer = 1
                        For Each detail As MTM03SearchResultElementDetail In detailList

                            '改ページ判断
                            If detail.PageNo <> targetPage Then  '１ページ以外
                                If detail.PageNo <> 1 Then
                                    'ページ替え処理
                                    rowNo = 5
                                    CellReport.Page.End()
                                    CellReport.Page.Start(SHEETNAME2, "1")
                                    targetPage = detail.PageNo
                                End If
                            End If

                            '対象RowIdのマックス行番号を取得
                            Dim rowMaxNum As Integer = detailList.Where(Function(n) n.RowIdVal = detail.RowIdVal) _
                                                .Select(Function(n) n.RowDetailNo).Max()

                            'ページの最終行を取得
                            Dim pageMaxNum As Integer = detailList.Where(Function(n) n.PageNo = targetPage) _
                                                .Select(Function(n) n.PageVal).Max()

                            With CellReport

                                'Noの列
                                If detail.RowId = 0 Then
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                Else
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Hair, xlColor.Black)
                                    .Cell("C" & rowNo).Value = detail.RowId
                                End If
                                '商品名の列
                                .Cell("E" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("E" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Hair, xlColor.Black)
                                .Cell("E" & rowNo).Value = detail.SyohinName.Trim
                                '入数の列
                                .Cell("Y" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("Y" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Hair, xlColor.Black)
                                .Cell("Y" & rowNo).Value = detail.Irisu
                                '単位の列
                                .Cell("AB" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AB" & rowNo).Value = detail.Tanni
                                'ロットの列
                                .Cell("AE" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AE" & rowNo).Value = detail.Lot.Trim
                                '新単価の列
                                .Cell("AH" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AH" & rowNo).Value = detail.NewTanka
                                '旧単価の列
                                .Cell("AN" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AN" & rowNo).Value = detail.OldTanka
                                '最終実績の列
                                .Cell("AT" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("AT" & rowNo).Value = detail.SaisyuDate
                                '備考の列
                                .Cell("AY" & rowNo).Attr.Box(BoxType.Left, BorderStyle.Hair, xlColor.Black)
                                .Cell("BN" & rowNo).Attr.Box(BoxType.Right, BorderStyle.Thin, xlColor.Black)
                                .Cell("AY" & rowNo).Value = detail.Bikou
                                '納品先履歴を表示するか、行削除するかを判断
                                If rowMaxNum <> detail.RowDetailNo Then
                                    '行数カウンターを+1(納品先履歴がないため）
                                    .Cell("C" & rowNo + 1).RowHeight = 0
                                Else
                                    '納品先履歴の列
                                    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Left, BorderStyle.Thin, xlColor.Black)
                                    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Over, BorderStyle.Hair, xlColor.Black)
                                    .Cell("C" & rowNo + 1).Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                    .Cell("BN" & rowNo + 1).Attr.Box(BoxType.Right, BorderStyle.Thin, xlColor.Black)
                                    .Cell("E" & rowNo + 1).Attr.Box(BoxType.Over, BorderStyle.Hair, xlColor.Black)
                                    .Cell("E" & rowNo + 1).Attr.Box(BoxType.Under, BorderStyle.Thin, xlColor.Black)
                                    .Cell("E" & rowNo + 1).Value = detail.NohinHistory
                                End If
                                'ページの最終行なら罫線
                                If pageMaxNum = detail.PageVal Then
                                    .Cell("C" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("E" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("Y" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AB" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AE" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AH" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AN" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AT" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                    .Cell("AY" & rowNo).Attr.Box(BoxType.Under, BorderStyle.Hair, xlColor.Black)
                                End If
                                rowNo += 2
                            End With
                        Next
                        CellReport.Page.End()
                    Next
                End If

                '終了処理
                CellReport.Report.End()

                'ファイル出力する
                createPdf = PrintCommon.GetTemporaryFileName(PrintItem.PrintType) + ".pdf"
                PrintItem.TemporaryFileName = createPdf
                CellReport.Report.SavePdf(PrintCommon.CombineFolderAndFile(2, PrintItem))

                Return True

            Catch ex As Exception
                PrintItem.ErrorCode = PrintCommon.ErrorCode.COM_WARN_PRT_PRINT_ERROR.ToString
                PrintItem.ErrorMessage = ex.Message.ToString
                Return False
            End Try

        End Function

    End Class

End Namespace