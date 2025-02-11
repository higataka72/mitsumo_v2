Public Class FormMTMMailPreview
    ''' <summary>
    ''' 閉じるボタンクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub
End Class