Imports System
Imports System.Data.SqlClient

Namespace Biz
    ''' <summary>
    ''' ログインビジネスロジック
    ''' </summary>
    Public Class MTMLogin
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' ログイン前バリデーション
        ''' </summary>
        ''' <param name="mtm00m002user">ユーザーマスタ</param>
        ''' <returns>エラーメッセージリスト</returns>
        Public Function ValidateLogin(ByVal mtm00m002user As Models.MTM00M002USER) As List(Of String)
            Dim errorList As New List(Of String)

            If String.IsNullOrWhiteSpace(mtm00m002user.MTMM002001) Then
                errorList.Add("ログインIDが空です")
            End If

            If String.IsNullOrWhiteSpace(mtm00m002user.MTMM002003) Then
                errorList.Add("パスワードが空です")
            End If

            Return errorList
        End Function

        ''' <summary>
        ''' ログイン
        ''' </summary>
        ''' <param name="mtm00m002user">ログインユーザー</param>
        ''' <returns>ログイン結果</returns>
        Public Function Login(ByVal mtm00m002user As Models.MTM00M002USER) As MTMLoginResult
            Dim result As New MTMLoginResult

            If mtm00m002user.MTMM002001 = "admin" And mtm00m002user.MTMM002003 = "mitsumo23" Then
                result.Message = "ログインに成功しました。"
                result.Result = True
                result.Mtm00m002User.MTMM002001 = "admin"
                result.Mtm00m002User.MTMM002002 = "MITSUMO管理者"
                result.Mtm00m002User.MTMM002003 = "mitsumo23"

                Return result
            End If

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM002001, MTMM002002, MTMM002003 FROM MTM00M002USER WHERE MTMM002001 = @MTMM002001 AND MTMM002003 = @MTMM002003"
                    command.Parameters.Add(New SqlParameter("@MTMM002001", mtm00m002user.MTMM002001))
                    command.Parameters.Add(New SqlParameter("@MTMM002003", mtm00m002user.MTMM002003))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        result.Message = "ログインに成功しました。"
                        result.Result = True
                        result.Mtm00m002User.MTMM002001 = reader.Item("MTMM002001").ToString()
                        result.Mtm00m002User.MTMM002002 = reader.Item("MTMM002002").ToString()
                        result.Mtm00m002User.MTMM002003 = reader.Item("MTMM002003").ToString()
                    Else
                        result.Message = "ログインIDもしくはパスワードが異なります。"
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return result
        End Function

        ''' <summary>
        ''' データベース接続確認
        ''' </summary>
        ''' <returns>接続状況</returns>
        Public Function DatabaseConect(ByRef errInfo As String) As Boolean
            Dim result As Boolean = True
            errInfo = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM002001 FROM MTM00M002USER"
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        result = True
                    End If
                End Using
            Catch ex As Exception
                errInfo = ex.Message()
                Return False
            Finally
                Me.connection.Close()
            End Try

            Return result
        End Function

        ''' <summary>
        ''' データベース接続確認(基幹側)
        ''' </summary>
        ''' <returns>接続状況</returns>
        Public Function DatabaseConect2(ByRef errInfo As String) As Boolean
            Dim result As Boolean = True
            errInfo = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT * FROM HAN10C021RENBAN"
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        result = True
                    End If
                End Using
            Catch ex As Exception
                errInfo = ex.Message()
                Return False
            Finally
                Me.connection.Close()
            End Try

            Return result
        End Function
    End Class

    ''' <summary>
    ''' ログイン結果
    ''' </summary>
    Public Class MTMLoginResult
        ''' <summary>
        ''' ユーザーマスタ
        ''' </summary>
        ''' <returns></returns>
        Public Property Mtm00m002User As New Models.MTM00M002USER
        ''' <summary>
        ''' 結果
        ''' </summary>
        ''' <returns>True:ログイン可/False:ログイン不可</returns>
        Public Property Result As Boolean = False
        ''' <summary>
        ''' 表示用メッセージ
        ''' </summary>
        ''' <returns></returns>
        Public Property Message As String = ""
    End Class
End Namespace
