Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Namespace Biz
    Public Class MTM05
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
        ''' バリデーション前処理
        ''' </summary>
        ''' <param name="mtm10r003jitsukou"></param>
        ''' <returns>List</returns>
        Public Function ValidateCheck(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU) As List(Of String)
            Dim errorList As New List(Of String)
            Dim dbl As Double

            '価格入力番号
            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003001) Then
                errorList.Add("価格入力番号が空です")
            End If

            '価格入力名称
            If (String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003002)) Then
            ElseIf (Len(mtm10r003jitsukou.MTMR003002) > 36) Then
                errorList.Add("価格入力名称の文字数が36桁を超えています")
            End If

            '一斉送信日時（時間チェック）
            If Not String.IsNullOrEmpty(mtm10r003jitsukou.MTMR003005_2) Then
                If Double.TryParse(mtm10r003jitsukou.MTMR003005_2, dbl) Then
                    If (dbl < 0) Then errorList.Add("一斉送信時間の数値が0-24ではありません")
                    If (dbl > 24) Then errorList.Add("一斉送信時間の数値が0-24ではありません")
                Else
                    errorList.Add("一斉送信時間が数値ではありません")
                End If
            End If

            '更新日時（時間チェック）
            If Not String.IsNullOrEmpty(mtm10r003jitsukou.MTMR003006_2) Then
                If Double.TryParse(mtm10r003jitsukou.MTMR003006_2, dbl) Then
                    If (dbl < 0) Then errorList.Add("更新時間の数値が0-24ではありません")
                    If (dbl > 24) Then errorList.Add("更新時間の数値が0-24ではありません")
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

            If String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003007) Then
                errorList.Add("締切日が空です")
            End If

            '運賃の指定
            If (String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003008)) Then
            ElseIf (Len(mtm10r003jitsukou.MTMR003008) > 50) Then
                errorList.Add("運賃の指定の文字数が50桁を超えています")
            End If

            '案内文の指定
            If (String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003009)) Then
            ElseIf (Len(mtm10r003jitsukou.MTMR003009) > 200) Then
                errorList.Add("案内文の指定の文字数が200桁を超えています")
            End If

            '入力元の指定
            If (String.IsNullOrWhiteSpace(mtm10r003jitsukou.MTMR003010)) Then
            ElseIf (Len(mtm10r003jitsukou.MTMR003010) > 200) Then
                errorList.Add("入力元の指定の文字数が200桁を超えています")
            End If

            Return errorList
        End Function
        ''' <summary>
        ''' 実行管理テーブル存在チェック
        ''' <param name="scMTMR003001"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataCheckSutanka(ByVal scMTMR003001 As Decimal) As Boolean
            Dim reBool As Boolean = False
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "MTMR003001 " _
                                        + "FROM MTM10R003JITSUKOU " _
                                        + "WHERE LTRIM(RTRIM(MTMR003001)) = @MTMR003001 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR003001", scMTMR003001))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reBool = True
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reBool

        End Function
        ''' <summary>
        ''' 実行管理テーブル更新
        ''' <param name="mtm10r003jitsukou"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataUpdateJitsuko(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU, ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        command.CommandText = "UPDATE MTM10R003JITSUKOU " _
                                        + "SET MTMR003002 = @MTMR003002 " _
                                        + ",MTMR003003 = @MTMR003003 " _
                                        + ",MTMR003005 = @MTMR003005 " _
                                        + ",MTMR003006 = @MTMR003006 " _
                                        + ",MTMR003021 = @MTMR003021 " _
                                        + ",MTMR003007 = @MTMR003007 " _
                                        + ",MTMR003008 = @MTMR003008 " _
                                        + ",MTMR003009 = @MTMR003009 " _
                                        + ",MTMR003010 = @MTMR003010 " _
                                        + ",MTMR003011 = @MTMR003011 " _
                                        + ",MTMR003012 = @MTMR003012 " _
                                        + ",MTMR003013 = @MTMR003013 " _
                                        + ",MTMR003014 = @MTMR003014 " _
                                        + ",MTMR003015 = @MTMR003015 " _
                                        + ",MTMR003016 = @MTMR003016 " _
                                        + ",MTMR003017 = @MTMR003017 " _
                                        + "WHERE LTRIM(RTRIM(MTMR003001)) = @MTMR003001 "
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR003001", mtm10r003jitsukou.MTMR003001))
                        command.Parameters.Add(New SqlParameter("@MTMR003002", mtm10r003jitsukou.MTMR003002))
                        command.Parameters.Add(New SqlParameter("@MTMR003003", mtm10r003jitsukou.MTMR003003))
                        command.Parameters.Add(New SqlParameter("@MTMR003005", mtm10r003jitsukou.MTMR003005 + mtm10r003jitsukou.MTMR003005_2))
                        command.Parameters.Add(New SqlParameter("@MTMR003006", mtm10r003jitsukou.MTMR003006 + mtm10r003jitsukou.MTMR003006_2))
                        command.Parameters.Add(New SqlParameter("@MTMR003021", mtm10r003jitsukou.MTMR003021))
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
                        command.ExecuteNonQuery()

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        sysError = ex.Message
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 連携管理テーブル更新
        ''' <param name="mtm10r003jitsukou"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataUpdateRenkei(ByVal mtm10r003jitsukou As Models.MTM10R003JITSUKOU, ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        command.CommandText = "UPDATE MTM10R004RENKEI " _
                                        + "SET MTMR004003 = @MTMR004003 " _
                                        + "WHERE LTRIM(RTRIM(MTMR004002)) = @MTMR004002 "
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR004002", mtm10r003jitsukou.MTMR003001))
                        command.Parameters.Add(New SqlParameter("@MTMR004003", mtm10r003jitsukou.MTMR003002))
                        command.ExecuteNonQuery()

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        sysError = ex.Message
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 実行管理テーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetJitsukou(ByVal scMTMR003001 As Integer) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMR003001" _
                        + ", MTMR003002" _
                        + ", MTMR003003" _
                        + ", CASE WHEN MTMR003004 IS NOT NULL AND REPLACE(MTMR003004, ' ', '') <> '' THEN SUBSTRING(MTMR003004, 1, 4) + '/' + SUBSTRING(MTMR003004, 5, 2) + '/' + SUBSTRING(MTMR003004, 7, 2) ELSE '' END AS MTMR003004" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 1, 4) + '/' + SUBSTRING(MTMR003005, 5, 2) + '/' + SUBSTRING(MTMR003005, 7, 2) ELSE '' END AS MTMR003005" _
                        + ", CASE WHEN MTMR003005 IS NOT NULL AND REPLACE(MTMR003005, ' ', '') <> '' THEN SUBSTRING(MTMR003005, 9, 2) ELSE '' END AS MTMR003005_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 1, 4) + '/' + SUBSTRING(MTMR003006, 5, 2) + '/' + SUBSTRING(MTMR003006, 7, 2) ELSE '' END AS MTMR003006" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003006, ' ', '') <> '' THEN SUBSTRING(MTMR003006, 9, 2) ELSE '' END AS MTMR003006_2" _
                        + ", CASE WHEN MTMR003006 IS NOT NULL AND REPLACE(MTMR003007, ' ', '') <> '' THEN SUBSTRING(MTMR003007, 1, 4) + '/' + SUBSTRING(MTMR003007, 5, 2) + '/' + SUBSTRING(MTMR003007, 7, 2) ELSE '' END AS MTMR003007" _
                        + ", MTMR003008" _
                        + ", MTMR003009" _
                        + ", MTMR003010" _
                        + ", ISNULL(MTMR003021,'') AS MTMR003021" _
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
                        + " WHERE MTMR003001 = @MTMR003001"
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR003001", scMTMR003001))
                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
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
