Namespace Models
    ''' <summary>
    ''' スケジュール登録時の設定情報
    ''' </summary>
    Public Class SERVERSETTING
        ''' <summary>
        ''' サーバー側に登録する場合（TREU / FALSE[ローカル環境])
        ''' </summary>
        ''' <returns></returns>
        Public Property ServerStartup As String
        ''' <summary>
        ''' SeverIP (192.168.0.1)
        ''' </summary>
        ''' <returns></returns>
        Public Property ServerIp As String
        ''' <summary>
        ''' 接続ユーザーID
        ''' </summary>
        ''' <returns></returns>
        Public Property ConnectionUser As String
        ''' <summary>
        ''' 接続パスワード
        ''' </summary>
        ''' <returns></returns>
        Public Property ConnectionPassword As String
        ''' <summary>
        ''' 起動ユーザー
        ''' </summary>
        ''' <returns></returns>
        Public Property StartupUser As String
        ''' <summary>
        ''' 起動パスワード
        ''' </summary>
        ''' <returns></returns>
        Public Property StartupPassword As String
    End Class
End Namespace
