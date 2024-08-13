Imports System
Imports System.Data.SqlClient

Namespace Biz
    ''' <summary>
    ''' 基底ビジネスロジック
    ''' </summary>
    Public Class BaseBiz
        ''' <summary>
        ''' データベース接続
        ''' </summary>
        ''' <returns></returns>
        Protected ReadOnly Property connection As SqlConnection

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            Me.connection = New SqlConnection(connectionString)
        End Sub
    End Class
End Namespace
