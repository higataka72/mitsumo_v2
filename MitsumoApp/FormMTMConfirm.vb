Imports System.Configuration
Imports MitsumoLib
Public Class FormMTMConfirm
    ''' <summary>
    ''' 単価入力シート取込ビジネスロジック
    ''' </summary>
    Private ReadOnly BizConfirm As Biz.MTMConfirm

    Public Property Password As String = ""

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizConfirm = New Biz.MTMConfirm(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMConfirm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.TextBox001.Text = ""
    End Sub

    ''' <summary>
    ''' OKボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Dim txt001 As String = Me.TextBox001.Text.Trim

        Dim result = Me.BizConfirm.ConfirmPassword(txt001)
        If result.Result = True Then
            Me.Password = result.Password
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = DialogResult.No
        End If

        Me.Close()
    End Sub
    ''' <summary>
    ''' OKボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
        'Me.formMenu.Show()
    End Sub
End Class