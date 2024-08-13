Imports System
Imports System.Configuration
Imports MitsumoLib.Biz.MTMUser
Imports MitsumoLib.Models.MTM00M002USER

''' <summary>
''' ユーザーマスタ登録フォーム
''' </summary>
Public Class FormMTMUser
    ''' <summary>
    ''' メニューフォーム
    ''' </summary>
    Private ReadOnly FormMenu As FormMTMMenu
    ''' <summary>
    ''' ユーザーマスタ登録ビジネスロジック
    ''' </summary>
    Private ReadOnly BizUser As MitsumoLib.Biz.MTMUser

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="formMenu"></param>
    Public Sub New(ByVal formMenu As FormMTMMenu)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.FormMenu = formMenu
        Dim connectionString = ConfigurationManager.ConnectionStrings("MITSUMO_DB").ConnectionString
        Me.BizUser = New MitsumoLib.Biz.MTMUser(connectionString)
    End Sub

    ''' <summary>
    ''' フォームロード処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim formConfirm As New FormMTMConfirm
        Dim result As DialogResult = formConfirm.ShowDialog()
        If result = DialogResult.OK Then
            Me.BizUser.Password = formConfirm.Password
            formConfirm.Dispose()
        Else
            MessageBox.Show("起動権限がありません")
            formConfirm.Dispose()
            Me.FormMenu.Show()
            Me.Close()
        End If

        ClearAll()
        '担当者プルダウンを設定
        ComboBox001.DataSource = Me.BizUser.TantouList()
        ComboBox001.DisplayMember = "name"
        ComboBox001.ValueMember = "name"
    End Sub

    ''' <summary>
    ''' クリア
    ''' </summary>
    Private Sub ClearAll()
        Me.TextBox001.Text = ""
        Me.TextBox002.Text = ""
        Me.TextBox003.Text = ""
        Me.TextBox003_2.Text = ""
    End Sub

    ''' <summary>
    ''' 登録ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonRegist_Click(sender As Object, e As EventArgs) Handles ButtonRegist.Click
        Dim txt001 As String = Me.TextBox001.Text.Trim
        Dim txt002 As String = Me.TextBox002.Text.Trim
        Dim txt003 As String = Me.TextBox003.Text.Trim
        Dim txt003_2 As String = Me.TextBox003_2.Text.Trim

        Dim mtm00m002user = New MitsumoLib.Models.MTM00M002USER
        mtm00m002user.MTMM002001 = txt001
        mtm00m002user.MTMM002002 = txt002
        mtm00m002user.MTMM002003 = txt003
        mtm00m002user.MTMM002003_2 = txt003_2

        Dim validateErrorList = Me.BizUser.ValidateRegist(mtm00m002user)
        If validateErrorList.Count > 0 Then
            For Each errorMessage As String In validateErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        If (Me.BizUser.ExistLoginId(mtm00m002user.MTMM002001)) Then
            Dim deletetErrorList = Me.BizUser.LoginIdDelete(txt001)
            If deletetErrorList.Count > 0 Then
                For Each errorMessage As String In deletetErrorList
                    MessageBox.Show(errorMessage)
                    Exit Sub
                Next
            End If
        End If

        Dim registErrorList = Me.BizUser.Regist(mtm00m002user)
        If registErrorList.Count > 0 Then
            For Each errorMessage As String In registErrorList
                MessageBox.Show(errorMessage)
                Exit Sub
            Next
        End If

        MessageBox.Show("登録しました")
        Me.ClearAll()
    End Sub

    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMTMUser_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.FormMenu.Show()
    End Sub
    ''' <summary>
    ''' フォームクローズ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        Me.formMenu.Show()
    End Sub
    ''' <summary>
    ''' 担当者選択値を取得して展開
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ComboBox001_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox001.SelectedValueChanged
        If ComboBox001.SelectedIndex <> -1 Then
            'ラベルに表示
            SetIdName(ComboBox001.SelectedValue.ToString())
        End If

    End Sub
    ''' <summary>
    ''' 担当者選択よりコードと名称を分解して設定
    ''' </summary>
    Private Sub SetIdName(ByVal name As String)
        Dim foundIndex As Integer = 0
        If (Not String.IsNullOrEmpty(name)) Then
            If 0 <= name.IndexOf(": ") Then
                foundIndex = name.IndexOf(": ")
                TextBox001.Text = name.Substring(0, foundIndex)
                TextBox002.Text = name.Substring(foundIndex + 2)
            End If

        End If
    End Sub
    ''' <summary>
    ''' ログインID検索ボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button002_Click(sender As Object, e As EventArgs) Handles Button002.Click
        Dim formSearch As New FormMTMSearchUser
        Dim result As DialogResult = formSearch.ShowDialog()

        If result = DialogResult.OK Then
            Me.TextBox001.Text = formSearch.Selected.MTMM002001
            Me.TextBox002.Text = formSearch.Selected.MTMM002002
        End If

        formSearch.Dispose()
    End Sub
    ''' <summary>
    ''' ログインID削除処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Dim result As DialogResult = MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        If result = DialogResult.No Then Exit Sub
        Dim txt001 As String = Me.TextBox001.Text.Trim
        If (Not String.IsNullOrEmpty(txt001)) Then
            If (Me.BizUser.ExistLoginId(txt001)) Then
                Dim deletetErrorList = Me.BizUser.LoginIdDelete(txt001)
                If deletetErrorList.Count > 0 Then
                    For Each errorMessage As String In deletetErrorList
                        MessageBox.Show(errorMessage)
                        Exit Sub
                    Next
                Else
                    MessageBox.Show("削除しました")
                    Me.ClearAll()
                End If
            Else
                MessageBox.Show("ログインIDは存在しません")
            End If
        End If
    End Sub
End Class