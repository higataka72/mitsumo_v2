Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' 部門コード検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchBumon
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 部門マスターテーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetBumon() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT HANM015001" _
                        + ", HANM015002" _
                        + " FROM HAN10M015BUMON" _
                        + " ORDER BY HANM015001 ASC"

                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
    End Class
End Namespace

