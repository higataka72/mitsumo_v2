<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMTMMailPreview
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMMailPreview))
        Me.RichTextBox001 = New System.Windows.Forms.RichTextBox()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.Label001 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'RichTextBox001
        '
        Me.RichTextBox001.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.RichTextBox001.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox001.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RichTextBox001.Location = New System.Drawing.Point(12, 137)
        Me.RichTextBox001.Name = "RichTextBox001"
        Me.RichTextBox001.ReadOnly = True
        Me.RichTextBox001.Size = New System.Drawing.Size(736, 543)
        Me.RichTextBox001.TabIndex = 0
        Me.RichTextBox001.TabStop = False
        Me.RichTextBox001.Text = ""
        '
        'ButtonClose
        '
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(663, 691)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(85, 39)
        Me.ButtonClose.TabIndex = 1
        Me.ButtonClose.Text = "閉じる"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'Label001
        '
        Me.Label001.AutoSize = True
        Me.Label001.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label001.Location = New System.Drawing.Point(12, 18)
        Me.Label001.Name = "Label001"
        Me.Label001.Size = New System.Drawing.Size(32, 18)
        Me.Label001.TabIndex = 119
        Me.Label001.Text = "宛先"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(89, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 18)
        Me.Label1.TabIndex = 120
        Me.Label1.Text = "Sample_to@sample.com"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 18)
        Me.Label2.TabIndex = 121
        Me.Label2.Text = "CC"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(89, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 18)
        Me.Label3.TabIndex = 122
        Me.Label3.Text = "Sample_cc@sample.com"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 18)
        Me.Label4.TabIndex = 123
        Me.Label4.Text = "BCC"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(89, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(161, 18)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "Sample_bcc@sample.com"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("メイリオ", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 20)
        Me.Label6.TabIndex = 125
        Me.Label6.Text = "件名"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("メイリオ", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(89, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(229, 20)
        Me.Label7.TabIndex = 126
        Me.Label7.Text = "(｛価格入力名｝)改定お見積書の送付"
        '
        'FormMTMMailPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(760, 738)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label001)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.RichTextBox001)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormMTMMailPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo]メールプレビュー"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RichTextBox001 As RichTextBox
    Friend WithEvents ButtonClose As Button
    Friend WithEvents Label001 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
