Imports System.Data.SqlClient

Namespace Biz
    Public Class MTM06
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' メール文テーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetMail(ByVal scMTMR006001 As String) As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                        + "  MTMR006001" _
                        + ", MTMR006002" _
                        + ", MTMR006003" _
                        + ", MTMR006004" _
                        + ", MTMR006005" _
                        + ", MTMR006006" _
                        + ", MTMR006007" _
                        + " FROM MTM10R006MAIL" _
                        + " WHERE LTRIM(RTRIM(MTMR006001)) = @MTMR006001"
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR006001", scMTMR006001))
                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
        ''' <summary>
        ''' メール文テーブル存在チェック
        ''' <param name="scMTMR006001"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataCheckMail(ByVal scMTMR006001 As String) As Boolean
            Dim reBool As Boolean = False
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "MTMR006001 " _
                                        + "FROM MTM10R006MAIL " _
                                        + "WHERE LTRIM(RTRIM(MTMR006001)) = @MTMR006001 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR006001", scMTMR006001))
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
        ''' メール文テーブル更新
        ''' <param name="mtm10r006mail"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataInsertMail(ByVal mtm10r006mail As Models.MTM10R006MAIL, ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        command.CommandText = "INSERT INTO MTM10R006MAIL (" _
                                        + "  MTMR006001 " _
                                        + " ,MTMR006002 " _
                                        + " ,MTMR006003 " _
                                        + " ,MTMR006004 " _
                                        + " ,MTMR006005 " _
                                        + " ,MTMR006006 " _
                                        + " ,MTMR006007 " _
                                        + ") VALUES (" _
                                        + "  @MTMR006001 " _
                                        + " ,@MTMR006002 " _
                                        + " ,@MTMR006003 " _
                                        + " ,@MTMR006004 " _
                                        + " ,@MTMR006005 " _
                                        + " ,@MTMR006006 " _
                                        + " ,@MTMR006007 " _
                                        + ")"
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR006001", mtm10r006mail.MTMR006001))
                        command.Parameters.Add(New SqlParameter("@MTMR006002", mtm10r006mail.MTMR006002))
                        command.Parameters.Add(New SqlParameter("@MTMR006003", mtm10r006mail.MTMR006003))
                        command.Parameters.Add(New SqlParameter("@MTMR006004", mtm10r006mail.MTMR006004))
                        command.Parameters.Add(New SqlParameter("@MTMR006005", mtm10r006mail.MTMR006005))
                        command.Parameters.Add(New SqlParameter("@MTMR006006", mtm10r006mail.MTMR006006))
                        command.Parameters.Add(New SqlParameter("@MTMR006007", mtm10r006mail.MTMR006007))
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
        ''' メール文テーブル更新
        ''' <param name="mtm10r006mail"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataUpdateMail(ByVal mtm10r006mail As Models.MTM10R006MAIL, ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        command.CommandText = "UPDATE MTM10R006MAIL " _
                                        + "SET MTMR006002 = @MTMR006002 " _
                                        + ",MTMR006003 = @MTMR006003 " _
                                        + ",MTMR006004 = @MTMR006004 " _
                                        + ",MTMR006005 = @MTMR006005 " _
                                        + ",MTMR006006 = @MTMR006006 " _
                                        + ",MTMR006007 = @MTMR006007 " _
                                        + "WHERE LTRIM(RTRIM(MTMR006001)) = @MTMR006001 "
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@MTMR006001", mtm10r006mail.MTMR006001))
                        command.Parameters.Add(New SqlParameter("@MTMR006002", mtm10r006mail.MTMR006002))
                        command.Parameters.Add(New SqlParameter("@MTMR006003", mtm10r006mail.MTMR006003))
                        command.Parameters.Add(New SqlParameter("@MTMR006004", mtm10r006mail.MTMR006004))
                        command.Parameters.Add(New SqlParameter("@MTMR006005", mtm10r006mail.MTMR006005))
                        command.Parameters.Add(New SqlParameter("@MTMR006006", mtm10r006mail.MTMR006006))
                        command.Parameters.Add(New SqlParameter("@MTMR006007", mtm10r006mail.MTMR006007))
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
        ''' バリデーション前処理
        ''' </summary>
        ''' <param name="mtm10r006mail"></param>
        ''' <returns>List</returns>
        Public Function ValidateCheck(ByVal mtm10r006mail As Models.MTM10R006MAIL) As List(Of String)
            Dim errorList As New List(Of String)

            '前_テキスト１
            If (String.IsNullOrWhiteSpace(mtm10r006mail.MTMR006002)) Then
            ElseIf (Len(mtm10r006mail.MTMR006002) > 100) Then
                errorList.Add("前の１行目の文字数が100桁を超えています")
            End If

            '前_テキスト２
            If (String.IsNullOrWhiteSpace(mtm10r006mail.MTMR006003)) Then
            ElseIf (Len(mtm10r006mail.MTMR006003) > 100) Then
                errorList.Add("前の２行目の文字数が100桁を超えています")
            End If

            '前_テキスト３
            If (String.IsNullOrWhiteSpace(mtm10r006mail.MTMR006004)) Then
            ElseIf (Len(mtm10r006mail.MTMR006004) > 100) Then
                errorList.Add("前の３行目の文字数が100桁を超えています")
            End If

            '後_テキスト１
            If (String.IsNullOrWhiteSpace(mtm10r006mail.MTMR006005)) Then
            ElseIf (Len(mtm10r006mail.MTMR006005) > 100) Then
                errorList.Add("後の１行目の文字数が100桁を超えています")
            End If

            '後_テキスト２
            If (String.IsNullOrWhiteSpace(mtm10r006mail.MTMR006006)) Then
            ElseIf (Len(mtm10r006mail.MTMR006006) > 100) Then
                errorList.Add("後の２行目の文字数が100桁を超えています")
            End If

            '後_テキスト３
            If (String.IsNullOrWhiteSpace(mtm10r006mail.MTMR006007)) Then
            ElseIf (Len(mtm10r006mail.MTMR006007) > 100) Then
                errorList.Add("後の３行目の文字数が100桁を超えています")
            End If

            Return errorList
        End Function

    End Class
End Namespace

