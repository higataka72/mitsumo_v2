<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMTMSelect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMSelect))
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonUriSelect = New System.Windows.Forms.Button()
        Me.ButtonTankaSelect = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonClose
        '
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(128, 72)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(116, 34)
        Me.ButtonClose.TabIndex = 3
        Me.ButtonClose.Text = "閉じる(ESC)"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'ButtonUriSelect
        '
        Me.ButtonUriSelect.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonUriSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonUriSelect.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonUriSelect.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonUriSelect.Location = New System.Drawing.Point(7, 20)
        Me.ButtonUriSelect.Name = "ButtonUriSelect"
        Me.ButtonUriSelect.Size = New System.Drawing.Size(116, 34)
        Me.ButtonUriSelect.TabIndex = 1
        Me.ButtonUriSelect.Text = "売上履歴"
        Me.ButtonUriSelect.UseVisualStyleBackColor = False
        '
        'ButtonTankaSelect
        '
        Me.ButtonTankaSelect.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonTankaSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonTankaSelect.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonTankaSelect.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonTankaSelect.Location = New System.Drawing.Point(128, 20)
        Me.ButtonTankaSelect.Name = "ButtonTankaSelect"
        Me.ButtonTankaSelect.Size = New System.Drawing.Size(116, 34)
        Me.ButtonTankaSelect.TabIndex = 2
        Me.ButtonTankaSelect.Text = "単価台帳参照"
        Me.ButtonTankaSelect.UseVisualStyleBackColor = False
        '
        'FormMTMSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(255, 125)
        Me.Controls.Add(Me.ButtonTankaSelect)
        Me.Controls.Add(Me.ButtonUriSelect)
        Me.Controls.Add(Me.ButtonClose)
        Me.Font = New System.Drawing.Font("ＭＳ 明朝", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMTMSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo]参照選択"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ButtonUriSelect As Button
    Friend WithEvents ButtonTankaSelect As Button
End Class
