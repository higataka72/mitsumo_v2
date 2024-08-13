Imports System
Imports System.Configuration
Imports System.Data.SqlClient
Imports MitsumoLib


Public Class FormMTMMenu
    Public Property ModelMtmUser As Models.MTM00M002USER

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.ModelMtmUser = Nothing
    End Sub

    ''' <summary>
    ''' 表示処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMMenu_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Me.ModelMtmUser Is Nothing Then
            Me.Hide()
            Dim formLogin As New FormMTMLogin(Me)
            formLogin.Show()
        End If
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonUser_Click(sender As Object, e As EventArgs) Handles ButtonUser.Click
        'ユーザー
        Me.ButtonMTM02.Visible = True
        Me.ButtonMTM03.Visible = True
        '管理者
        Me.ButtonMTM01.Visible = False
        Me.ButtonMTM04.Visible = False
        Me.ButtonMTM05.Visible = False
        Me.ButtonMTMUser.Visible = False
        'ボタン強調
        Me.ButtonUser.BackColor = Color.LightSeaGreen
        Me.ButtonAdmin.BackColor = Color.MediumTurquoise
    End Sub
    ''' <summary>
    ''' ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonAdmin_Click(sender As Object, e As EventArgs) Handles ButtonAdmin.Click
        'ユーザー
        Me.ButtonMTM02.Visible = False
        Me.ButtonMTM03.Visible = False
        '管理者
        Me.ButtonMTM01.Visible = True
        Me.ButtonMTM04.Visible = True
        Me.ButtonMTM05.Visible = True
        Me.ButtonMTMUser.Visible = True
        'ボタン強調
        Me.ButtonUser.BackColor = Color.MediumTurquoise
        Me.ButtonAdmin.BackColor = Color.LightSeaGreen
    End Sub
    ''' <summary>
    ''' フォームオープン時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ユーザー
        Me.ButtonMTM02.Visible = True
        Me.ButtonMTM03.Visible = True
        '管理者
        Me.ButtonMTM01.Visible = False
        Me.ButtonMTM04.Visible = False
        Me.ButtonMTM05.Visible = False
        Me.ButtonMTMUser.Visible = False
        'ボタン強調
        Me.ButtonUser.BackColor = Color.LightSeaGreen
        Me.ButtonAdmin.BackColor = Color.MediumTurquoise
    End Sub
    ''' <summary>
    ''' 単価入力シート取込ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonMTM01_Click_1(sender As Object, e As EventArgs) Handles ButtonMTM01.Click
        Me.Hide()
        Dim form01 As New FormMTM01(Me)
        form01.Show()
    End Sub
    ''' <summary>
    ''' 価格入力ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonMTM02_Click_1(sender As Object, e As EventArgs) Handles ButtonMTM02.Click
        Me.Hide()
        Dim form02 As New FormMTM02(Me)
        form02.Show()
    End Sub
    ''' <summary>
    ''' 見積送信ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonMTM03_Click_1(sender As Object, e As EventArgs) Handles ButtonMTM03.Click
        Me.Hide()
        Dim form03 As New FormMTM03(Me)
        form03.Show()
    End Sub
    ''' <summary>
    ''' システム間連携ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonMTM04_Click_1(sender As Object, e As EventArgs) Handles ButtonMTM04.Click
        Me.Hide()
        Dim form04 As New FormMTM04(Me)
        form04.Show()
    End Sub
    ''' <summary>
    ''' 実行管理テーブルボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonMTM05_Click_1(sender As Object, e As EventArgs) Handles ButtonMTM05.Click
        Me.Hide()
        Dim form05 As New FormMTM05(Me)
        form05.Show()
    End Sub
    ''' <summary>
    ''' ユーザーマスタ登録ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonMTMUser_Click(sender As Object, e As EventArgs) Handles ButtonMTMUser.Click
        Me.Hide()
        Dim formUser As New FormMTMUser(Me)
        formUser.Show()
    End Sub
    ''' <summary>
    ''' 検証SQLボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonKensho01_Click(sender As Object, e As EventArgs) Handles ButtonKensho01.Click
        Dim connection As SqlConnection
        Dim kariNum As String
        'Dim ans As String
        'ans = InputBox("テーブル名を入力して下さい" + vbCrLf + "(例：HAN10R006MITSUMORIH)")
        Try
            Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
            connection = New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand
                command.Connection = connection
                command.CommandText = "SELECT ISNULL(MAX(HANR006004),0) + 1 AS KARI_NUM FROM HAN10R006MITSUMORIH"
                command.Parameters.Clear()
                Dim reader_num As SqlDataReader = command.ExecuteReader
                If reader_num.Read = True Then
                    kariNum = reader_num.Item("KARI_NUM").ToString()
                End If
                MessageBox.Show("SQL正常")
                'MessageBox.Show("[" + ans + "] 参照できるテーブル")
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    ''' <summary>
    ''' 検証SQLボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connection As SqlConnection
        Dim kariNum As Decimal = 0
        'Dim ans As String
        'ans = InputBox("テーブル名を入力して下さい" + vbCrLf + "(例：HAN10R006MITSUMORIH)")
        Try
            Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
            connection = New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand
                command.Connection = connection
                command.CommandText = "SELECT MAX(HANR006004) AS KARI_NUM FROM HAN10R006MITSUMORIH"
                command.Parameters.Clear()
                Dim reader_num As SqlDataReader = command.ExecuteReader
                If reader_num.Read = True Then
                    If (reader_num.Item("KARI_NUM").ToString() IsNot Nothing) _
                        AndAlso (reader_num.Item("KARI_NUM").ToString().Length <> 0) Then
                        kariNum = reader_num.Item("KARI_NUM").ToString()
                        kariNum = kariNum + 1
                    Else
                        kariNum = 1
                    End If
                End If
                MessageBox.Show("SQL正常")
                'MessageBox.Show("[" + ans + "] 参照できるテーブル")
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
End Class