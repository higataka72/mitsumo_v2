<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMTMPreview
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMPreview))
        Me.WebView2001 = New Microsoft.Web.WebView2.WinForms.WebView2()
        CType(Me.WebView2001, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WebView2001
        '
        Me.WebView2001.AllowExternalDrop = True
        Me.WebView2001.CreationProperties = Nothing
        Me.WebView2001.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView2001.Location = New System.Drawing.Point(12, 12)
        Me.WebView2001.Name = "WebView2001"
        Me.WebView2001.Size = New System.Drawing.Size(1125, 744)
        Me.WebView2001.TabIndex = 0
        Me.WebView2001.ZoomFactor = 1.0R
        '
        'FormMTMPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1149, 780)
        Me.Controls.Add(Me.WebView2001)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormMTMPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo]PDFプレビュー"
        CType(Me.WebView2001, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents WebView2001 As Microsoft.Web.WebView2.WinForms.WebView2
End Class
