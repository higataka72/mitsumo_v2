Imports System.IO

Namespace Biz

    ''' <summary>
    ''' PrintCommon
    ''' 印刷共通処理クラス
    ''' </summary>
    Public Class PrintCommon

        Public Enum ErrorCode
            COM_WARN_PRT_REQUEST_NOTEXIST       '印刷依頼データが存在しません
            COM_WARN_PRT_NODATA                 '印刷対象データが存在しません
            COM_WARN_PRT_DATA_ERROR             '印刷対象データ取得時にエラーが起きました
            COM_WARN_PRT_NO_PARAMS              '印刷パラメータが設定されていません
            COM_WARN_PRT_PRINT_ERROR            '印刷時にエラーが起きました
            COM_WARN_PRT_FILE_NOTEXIST          'デザインファイルが存在しません
            COM_WARN_PRT_INVALID_EXTENSTION     'デザインファイルの拡張子が不正です

        End Enum

        Public Enum OutputFileType As Integer
            PDF = 1
            XLSX = 2
        End Enum

        Public Enum PrintStatus As Integer
            YET = 0
            SUCCESS = 1
            FAIL = 2
        End Enum

        ''' <summary>
        ''' テンプレートファイルのチェック
        ''' </summary>
        ''' <param name="PrintItem"></param>
        ''' <returns></returns>
        Public Shared Function TemplateFileCheck(ByRef PrintItem As H_Print) As Boolean

            'ファイル存在チェック
            If Not File.Exists(PrintItem.TemplateFilePath) Then
                PrintItem.ErrorCode = ErrorCode.COM_WARN_PRT_FILE_NOTEXIST.ToString
                Return False
            End If

            '拡張子チェック
            Dim fileExtension As String = Path.GetExtension(PrintItem.TemplateFilePath)
            If Not fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) Then
                PrintItem.ErrorCode = ErrorCode.COM_WARN_PRT_INVALID_EXTENSTION.ToString
                Return False
            End If

            Return True
        End Function

        ''' <summary>
        ''' 出力時の一時ファイル名の生成
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        Public Shared Function GetTemporaryFileName(fileName As String) As String
            Dim formattedDate As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim random As New Random()
            Dim randomString As String = New String(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 5).Select(Function(s) s(random.Next(s.Length))).ToArray())
            Return $"{fileName}_{formattedDate}_{randomString}"
        End Function

        ''' <summary>
        ''' 入出力ファイルパスの生成
        ''' </summary>
        ''' <param name="mode"></param>
        ''' <param name="PrintItem"></param>
        ''' <returns></returns>
        Public Shared Function CombineFolderAndFile(mode As Integer, ByRef PrintItem As H_Print) As String
            Dim directorypath As String
            Dim newGuid As Guid = Guid.NewGuid()
            Dim guidString As String = newGuid.ToString()
            If mode = 1 Then
                'テンプレートファイルの取得（テンプレートルートディレクトリ+言語別フォルダ内のファイル）
                directorypath = Path.Combine(PrintItem.TemplateDirectory, PrintItem.TemplateFileName)
            Else
                '出力先フォルダの設定（出力先ディレクトリ+帳票ID+GUID）
                directorypath = PrintItem.OutputDirectory
                If Not Directory.Exists(directorypath) Then
                    Directory.CreateDirectory(directorypath)
                End If
                PrintItem.OutputDirectory = directorypath
                '一時ファイル名を付与してリターン
                Return Path.Combine(directorypath, PrintItem.TemporaryFileName)
            End If
            Return directorypath
        End Function

        ''' <summary>
        ''' 文字列を区切って配列化
        ''' </summary>
        ''' <param name="InputString"></param>
        ''' <returns></returns>
        Public Shared Function SplitStringIntoArray(InputString As String) As String()
            Dim stringArray As String() = InputString.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
            Return stringArray
        End Function

        ''' <summary>
        ''' 日付を年月日形式に変換
        ''' </summary>
        ''' <param name="inputDate"></param>
        ''' <returns></returns>
        Public Shared Function FormatDate(inputDate As String) As String
            Dim parsedDate As DateTime
            If DateTime.TryParse(inputDate, parsedDate) Then
                Return parsedDate.ToString("yyyy/MM/dd")
            Else
                Return String.Empty
            End If
        End Function

        ''' <summary>
        ''' 整数値に変換
        ''' </summary>
        ''' <param name="inputString"></param>
        ''' <returns></returns>
        Public Shared Function FormatStringAsInteger(inputString As String) As String
            Dim decimalValue As Decimal
            If Decimal.TryParse(inputString, decimalValue) Then
                ' 整数部分を取得し、3桁ごとにカンマを挿入する
                Return Decimal.ToInt32(decimalValue).ToString("N0")
            Else
                Return String.Empty
            End If
        End Function

        ''' <summary>
        ''' 郵便番号を〒xxx-xxxxの形で生成
        ''' </summary>
        ''' <param name="PostalCode"></param>
        ''' <returns></returns>
        Public Shared Function SetPostalCode(PostalCode As String) As String
            If PostalCode <> "" And Len(PostalCode) = 7 Then
                Return "〒" + PostalCode.Substring(0, 3) + "-" + PostalCode.Substring(3, 4)
            Else
                Return String.Empty
            End If
        End Function

    End Class

End Namespace
