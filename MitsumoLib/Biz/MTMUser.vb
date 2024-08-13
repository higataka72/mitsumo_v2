Imports System
Imports System.Data.SqlClient

Namespace Biz
    ''' <summary>
    ''' ユーザーマスタ登録ビジネスロジック
    ''' </summary>
    Public Class MTMUser
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
        ''' 登録前バリデーション
        ''' </summary>
        ''' <param name="mtm00m002user">ユーザーマスタ</param>
        ''' <returns>エラーメッセージリスト</returns>
        Public Function ValidateRegist(mtm00m002user As Models.MTM00M002USER) As List(Of String)
            Dim errorList As New List(Of String)

            If String.IsNullOrWhiteSpace(mtm00m002user.MTMM002001) Then
                errorList.Add("ログインIDが空です")
            End If

            If String.IsNullOrWhiteSpace(mtm00m002user.MTMM002002) Then
                errorList.Add("ログイン名が空です")
            End If

            If String.IsNullOrWhiteSpace(mtm00m002user.MTMM002003) Then
                errorList.Add("パスワードが空です")
            End If

            If mtm00m002user.MTMM002003.Length > 0 Then
                If mtm00m002user.MTMM002003 <> mtm00m002user.MTMM002003_2 Then
                    errorList.Add("パスワードが異なります")
                End If
            End If

            'If ExistLoginId(mtm00m002user.MTMM002001) Then
            '    errorList.Add("そのログインIDは既に存在します")
            'End If

            Return errorList
        End Function

        ''' <summary>
        ''' ログインIDの存在チェック
        ''' </summary>
        ''' <param name="mtmm002001">ログインID</param>
        ''' <returns>True:存在, Flase:存在しない</returns>
        Public Function ExistLoginId(mtmm002001 As String) As Boolean
            Dim result As Boolean = False

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT COUNT(MTMM002001) AS USER_COUNT FROM MTM00M002USER WHERE MTMM002001 = @MTMM002001"
                    command.Parameters.Add(New SqlParameter("@MTMM002001", mtmm002001))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        Dim count As Integer = Integer.Parse(reader.Item("USER_COUNT"))
                        If count > 0 Then
                            result = True
                        End If
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return result
        End Function

        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <param name="mtm00m002user">ユーザーマスタ</param>
        ''' <returns></returns>
        Public Function Regist(ByVal mtm00m002user As Models.MTM00M002USER) As List(Of String)
            Dim errorList As New List(Of String)

            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Using command As New SqlCommand
                        Try
                            command.Connection = Me.connection
                            command.Transaction = transaction
                            command.CommandText = "INSERT INTO MTM00M002USER (MTMM002001, MTMM002002, MTMM002003) VALUES (@MTMM002001, @MTMM002002, @MTMM002003)"
                            command.Parameters.Add(New SqlParameter("@MTMM002001", mtm00m002user.MTMM002001))
                            command.Parameters.Add(New SqlParameter("@MTMM002002", mtm00m002user.MTMM002002))
                            command.Parameters.Add(New SqlParameter("@MTMM002003", mtm00m002user.MTMM002003))
                            command.ExecuteNonQuery()
                            transaction.Commit()
                        Catch ex As Exception
                            transaction.Rollback()
                            Throw ex
                        End Try
                    End Using
                End Using
            Catch ex As Exception
                errorList.Add(ex.Message)
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function
        ''' <summary>
        ''' 削除処理
        ''' </summary>
        ''' <param name="loginId"></param>
        ''' <returns></returns>
        Public Function LoginIdDelete(ByVal loginId As String) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Using command As New SqlCommand
                        Try
                            command.Connection = Me.connection
                            command.Transaction = transaction
                            command.CommandText = "DELETE FROM MTM00M002USER WHERE MTMM002001 = @MTMM002001"
                            command.Parameters.Add(New SqlParameter("@MTMM002001", loginId))
                            command.ExecuteNonQuery()
                            transaction.Commit()
                        Catch ex As Exception
                            transaction.Rollback()
                            Throw ex
                        End Try
                    End Using
                End Using
            Catch ex As Exception
                errorList.Add(ex.Message)
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 担当者リスト
        ''' </summary>
        ''' <returns>DataTable</returns>
        Public Function TantouList() As DataTable
            Dim table As New DataTable

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT '' AS  NAME " _
                                        + "UNION " _
                                        + "SELECT RTRIM(HANM004001) + ': ' + RTRIM(HANM004002) AS NAME " _
                                        + "FROM HAN10M004TANTO " _
                                        + "ORDER BY NAME "
                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function

    End Class
End Namespace
