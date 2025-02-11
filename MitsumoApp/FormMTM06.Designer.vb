<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMTM06
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTM06))
        Me.ButtonExecute = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonPreview = New System.Windows.Forms.Button()
        Me.TextBox001 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox002 = New System.Windows.Forms.TextBox()
        Me.TextBox003 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox006 = New System.Windows.Forms.TextBox()
        Me.TextBox005 = New System.Windows.Forms.TextBox()
        Me.TextBox004 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ButtonExecute
        '
        Me.ButtonExecute.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonExecute.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonExecute.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonExecute.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonExecute.Location = New System.Drawing.Point(527, 348)
        Me.ButtonExecute.Name = "ButtonExecute"
        Me.ButtonExecute.Size = New System.Drawing.Size(85, 39)
        Me.ButtonExecute.TabIndex = 7
        Me.ButtonExecute.Text = "登録"
        Me.ButtonExecute.UseVisualStyleBackColor = False
        '
        'ButtonClose
        '
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(618, 348)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(85, 39)
        Me.ButtonClose.TabIndex = 8
        Me.ButtonClose.Text = "閉じる"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'ButtonPreview
        '
        Me.ButtonPreview.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonPreview.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonPreview.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonPreview.Location = New System.Drawing.Point(408, 348)
        Me.ButtonPreview.Name = "ButtonPreview"
        Me.ButtonPreview.Size = New System.Drawing.Size(113, 39)
        Me.ButtonPreview.TabIndex = 6
        Me.ButtonPreview.TabStop = False
        Me.ButtonPreview.Text = "プレビュー"
        Me.ButtonPreview.UseVisualStyleBackColor = False
        '
        'TextBox001
        '
        Me.TextBox001.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox001.Location = New System.Drawing.Point(54, 55)
        Me.TextBox001.Name = "TextBox001"
        Me.TextBox001.Size = New System.Drawing.Size(649, 25)
        Me.TextBox001.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 24)
        Me.Label2.TabIndex = 147
        Me.Label2.Text = "前"
        '
        'TextBox002
        '
        Me.TextBox002.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox002.Location = New System.Drawing.Point(54, 86)
        Me.TextBox002.Name = "TextBox002"
        Me.TextBox002.Size = New System.Drawing.Size(649, 25)
        Me.TextBox002.TabIndex = 1
        '
        'TextBox003
        '
        Me.TextBox003.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox003.Location = New System.Drawing.Point(54, 117)
        Me.TextBox003.Name = "TextBox003"
        Me.TextBox003.Size = New System.Drawing.Size(649, 25)
        Me.TextBox003.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 24)
        Me.Label1.TabIndex = 150
        Me.Label1.Text = "後"
        '
        'TextBox006
        '
        Me.TextBox006.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox006.Location = New System.Drawing.Point(54, 264)
        Me.TextBox006.Name = "TextBox006"
        Me.TextBox006.Size = New System.Drawing.Size(649, 25)
        Me.TextBox006.TabIndex = 5
        '
        'TextBox005
        '
        Me.TextBox005.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox005.Location = New System.Drawing.Point(54, 233)
        Me.TextBox005.Name = "TextBox005"
        Me.TextBox005.Size = New System.Drawing.Size(649, 25)
        Me.TextBox005.TabIndex = 4
        '
        'TextBox004
        '
        Me.TextBox004.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox004.Location = New System.Drawing.Point(54, 202)
        Me.TextBox004.Name = "TextBox004"
        Me.TextBox004.Size = New System.Drawing.Size(649, 25)
        Me.TextBox004.TabIndex = 3
        '
        'FormMTM06
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(715, 406)
        Me.Controls.Add(Me.TextBox006)
        Me.Controls.Add(Me.TextBox005)
        Me.Controls.Add(Me.TextBox004)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox003)
        Me.Controls.Add(Me.TextBox002)
        Me.Controls.Add(Me.TextBox001)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonPreview)
        Me.Controls.Add(Me.ButtonExecute)
        Me.Controls.Add(Me.ButtonClose)
        Me.Font = New System.Drawing.Font("ＭＳ 明朝", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "FormMTM06"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo]メール文"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonExecute As Button
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ButtonPreview As Button
    Friend WithEvents TextBox001 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox002 As TextBox
    Friend WithEvents TextBox003 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox006 As TextBox
    Friend WithEvents TextBox005 As TextBox
    Friend WithEvents TextBox004 As TextBox
End Class
