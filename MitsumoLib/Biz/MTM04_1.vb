Imports System.Data.SqlClient

Namespace Biz
    Public Class MTM04_1
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 連番管理テーブル（基幹系）
        ''' </summary>
        ''' <param name="dummyNumber"></param>
        ''' <param name="sysError"></param>
        ''' <returns>List</returns>
        Public Function UpdateRenbanManage(ByVal dummyNumber As List(Of Decimal),
                                           ByRef renbanStart As String,
                                           ByRef renbanMax As String,
                                           ByRef sysError As String) As List(Of String)

            Dim errorList As New List(Of String)
            Dim dtToday = DateTime.Now.ToString("yyyyMMdd")

            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        '連番管理テーブルをロックして更新
                        command.CommandText = "SELECT HANC021005 " _
                                            + "FROM HAN10C021RENBAN "
                        command.Parameters.Clear()
                        Dim reader As SqlDataReader = command.ExecuteReader
                        If reader.Read = True Then
                            If Not Decimal.TryParse(reader.Item("HANC021005").ToString(), renbanStart) Then
                                errorList.Add("連番管理テーブルから採番できません")
                                Return errorList
                            End If
                        End If
                        If (dummyNumber.Count() > 0) Then
                            renbanMax = renbanStart + dummyNumber.Count()
                        End If
                        reader.Close()

                        '連番管理テーブルを更新
                        command.CommandText = "UPDATE HAN10C021RENBAN SET HANC021005 = @HANC021005 "
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@HANC021005", renbanMax))
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

    End Class
End Namespace