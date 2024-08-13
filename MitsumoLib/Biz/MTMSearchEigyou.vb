Imports System
Imports System.Data.SqlClient
Namespace Biz
    ''' <summary>
    ''' 営業所コード検索ビジネスロジック
    ''' </summary>
    Public Class MTMSearchEigyou
        Inherits BaseBiz

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 営業所マスターテーブルデータを取得
        ''' </summary>
        ''' <returns></returns>
        Public Function GetEigyou() As DataTable
            Dim table As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT HANM036001" _
                        + ", HANM036002" _
                        + " FROM HAN10M036EIGYOU" _
                        + " ORDER BY HANM036001 ASC"

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
