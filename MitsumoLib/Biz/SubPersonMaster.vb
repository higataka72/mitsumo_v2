Imports System
Imports System.Data.SqlClient

Namespace Biz
    ''' <summary>
    ''' 担当者マスタリスト選択ビジネスロジック
    ''' </summary>
    Public Class SubPersonMaster
        Inherits BaseBiz
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' 担当者マスタよりリスト取得
        ''' </summary>
        ''' <returns>True:存在, Flase:存在しない</returns>
        Public Function PersonMasterList() As DataTable
            Dim dt As New DataTable
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT HANM004001, HANM004002 FROM HAN10M004TANTO ORDER BY HANM004001"
                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(dt)
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return dt
        End Function

    End Class

End Namespace
