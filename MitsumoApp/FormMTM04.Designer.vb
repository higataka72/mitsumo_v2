<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMTM04
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTM04))
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.Label001 = New System.Windows.Forms.Label()
        Me.CheckBox001 = New System.Windows.Forms.CheckBox()
        Me.CheckBox002 = New System.Windows.Forms.CheckBox()
        Me.Label002 = New System.Windows.Forms.Label()
        Me.TextBox002 = New System.Windows.Forms.TextBox()
        Me.Button001 = New System.Windows.Forms.Button()
        Me.TextBox001 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox004 = New System.Windows.Forms.TextBox()
        Me.TextBox003 = New System.Windows.Forms.TextBox()
        Me.Label003 = New System.Windows.Forms.Label()
        Me.Button002 = New System.Windows.Forms.Button()
        Me.Button003 = New System.Windows.Forms.Button()
        Me.TextBox006 = New System.Windows.Forms.TextBox()
        Me.TextBox005 = New System.Windows.Forms.TextBox()
        Me.Button004 = New System.Windows.Forms.Button()
        Me.Label004 = New System.Windows.Forms.Label()
        Me.TextBox008 = New System.Windows.Forms.TextBox()
        Me.TextBox007 = New System.Windows.Forms.TextBox()
        Me.Button006 = New System.Windows.Forms.Button()
        Me.TextBox012 = New System.Windows.Forms.TextBox()
        Me.TextBox011 = New System.Windows.Forms.TextBox()
        Me.Button005 = New System.Windows.Forms.Button()
        Me.Label005 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox010 = New System.Windows.Forms.TextBox()
        Me.TextBox009 = New System.Windows.Forms.TextBox()
        Me.Button007 = New System.Windows.Forms.Button()
        Me.TextBox013 = New System.Windows.Forms.TextBox()
        Me.Label006 = New System.Windows.Forms.Label()
        Me.ButtonExecute = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonClose
        '
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(735, 322)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(85, 39)
        Me.ButtonClose.TabIndex = 24
        Me.ButtonClose.Text = "閉じる"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'Label001
        '
        Me.Label001.AutoSize = True
        Me.Label001.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label001.Location = New System.Drawing.Point(32, 39)
        Me.Label001.Name = "Label001"
        Me.Label001.Size = New System.Drawing.Size(122, 24)
        Me.Label001.TabIndex = 21
        Me.Label001.Text = "出力対象の指定"
        '
        'CheckBox001
        '
        Me.CheckBox001.AutoSize = True
        Me.CheckBox001.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CheckBox001.Location = New System.Drawing.Point(240, 38)
        Me.CheckBox001.Name = "CheckBox001"
        Me.CheckBox001.Size = New System.Drawing.Size(93, 28)
        Me.CheckBox001.TabIndex = 1
        Me.CheckBox001.Text = "単価台帳"
        Me.CheckBox001.UseVisualStyleBackColor = True
        '
        'CheckBox002
        '
        Me.CheckBox002.AutoSize = True
        Me.CheckBox002.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CheckBox002.Location = New System.Drawing.Point(355, 38)
        Me.CheckBox002.Name = "CheckBox002"
        Me.CheckBox002.Size = New System.Drawing.Size(109, 28)
        Me.CheckBox002.TabIndex = 2
        Me.CheckBox002.Text = "見積データ"
        Me.CheckBox002.UseVisualStyleBackColor = True
        '
        'Label002
        '
        Me.Label002.AutoSize = True
        Me.Label002.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label002.Location = New System.Drawing.Point(32, 74)
        Me.Label002.Name = "Label002"
        Me.Label002.Size = New System.Drawing.Size(106, 24)
        Me.Label002.TabIndex = 24
        Me.Label002.Text = "価格入力番号"
        '
        'TextBox002
        '
        Me.TextBox002.Enabled = False
        Me.TextBox002.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox002.Location = New System.Drawing.Point(380, 74)
        Me.TextBox002.Name = "TextBox002"
        Me.TextBox002.Size = New System.Drawing.Size(437, 25)
        Me.TextBox002.TabIndex = 5
        '
        'Button001
        '
        Me.Button001.Image = CType(resources.GetObject("Button001.Image"), System.Drawing.Image)
        Me.Button001.Location = New System.Drawing.Point(345, 73)
        Me.Button001.Name = "Button001"
        Me.Button001.Size = New System.Drawing.Size(29, 24)
        Me.Button001.TabIndex = 4
        Me.Button001.UseVisualStyleBackColor = True
        '
        'TextBox001
        '
        Me.TextBox001.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox001.Location = New System.Drawing.Point(240, 74)
        Me.TextBox001.Name = "TextBox001"
        Me.TextBox001.Size = New System.Drawing.Size(99, 25)
        Me.TextBox001.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(342, 144)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 16)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "～"
        '
        'TextBox004
        '
        Me.TextBox004.Enabled = False
        Me.TextBox004.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox004.Location = New System.Drawing.Point(380, 109)
        Me.TextBox004.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox004.Name = "TextBox004"
        Me.TextBox004.Size = New System.Drawing.Size(297, 25)
        Me.TextBox004.TabIndex = 8
        '
        'TextBox003
        '
        Me.TextBox003.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox003.Location = New System.Drawing.Point(240, 110)
        Me.TextBox003.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox003.Name = "TextBox003"
        Me.TextBox003.Size = New System.Drawing.Size(99, 25)
        Me.TextBox003.TabIndex = 6
        '
        'Label003
        '
        Me.Label003.AutoSize = True
        Me.Label003.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label003.Location = New System.Drawing.Point(32, 111)
        Me.Label003.Name = "Label003"
        Me.Label003.Size = New System.Drawing.Size(106, 24)
        Me.Label003.TabIndex = 43
        Me.Label003.Text = "担当者コード"
        '
        'Button002
        '
        Me.Button002.Image = CType(resources.GetObject("Button002.Image"), System.Drawing.Image)
        Me.Button002.Location = New System.Drawing.Point(344, 109)
        Me.Button002.Name = "Button002"
        Me.Button002.Size = New System.Drawing.Size(29, 24)
        Me.Button002.TabIndex = 7
        Me.Button002.UseVisualStyleBackColor = True
        '
        'Button003
        '
        Me.Button003.Image = CType(resources.GetObject("Button003.Image"), System.Drawing.Image)
        Me.Button003.Location = New System.Drawing.Point(484, 140)
        Me.Button003.Name = "Button003"
        Me.Button003.Size = New System.Drawing.Size(29, 24)
        Me.Button003.TabIndex = 10
        Me.Button003.UseVisualStyleBackColor = True
        '
        'TextBox006
        '
        Me.TextBox006.Enabled = False
        Me.TextBox006.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox006.Location = New System.Drawing.Point(520, 140)
        Me.TextBox006.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox006.Name = "TextBox006"
        Me.TextBox006.Size = New System.Drawing.Size(297, 25)
        Me.TextBox006.TabIndex = 11
        '
        'TextBox005
        '
        Me.TextBox005.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox005.Location = New System.Drawing.Point(380, 141)
        Me.TextBox005.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox005.Name = "TextBox005"
        Me.TextBox005.Size = New System.Drawing.Size(99, 25)
        Me.TextBox005.TabIndex = 9
        '
        'Button004
        '
        Me.Button004.Image = CType(resources.GetObject("Button004.Image"), System.Drawing.Image)
        Me.Button004.Location = New System.Drawing.Point(344, 178)
        Me.Button004.Name = "Button004"
        Me.Button004.Size = New System.Drawing.Size(29, 24)
        Me.Button004.TabIndex = 13
        Me.Button004.UseVisualStyleBackColor = True
        '
        'Label004
        '
        Me.Label004.AutoSize = True
        Me.Label004.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label004.Location = New System.Drawing.Point(32, 179)
        Me.Label004.Name = "Label004"
        Me.Label004.Size = New System.Drawing.Size(154, 24)
        Me.Label004.TabIndex = 50
        Me.Label004.Text = "営業所コードの指定"
        '
        'TextBox008
        '
        Me.TextBox008.Enabled = False
        Me.TextBox008.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox008.Location = New System.Drawing.Point(380, 178)
        Me.TextBox008.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox008.Name = "TextBox008"
        Me.TextBox008.Size = New System.Drawing.Size(297, 25)
        Me.TextBox008.TabIndex = 14
        '
        'TextBox007
        '
        Me.TextBox007.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox007.Location = New System.Drawing.Point(240, 179)
        Me.TextBox007.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox007.Name = "TextBox007"
        Me.TextBox007.Size = New System.Drawing.Size(99, 25)
        Me.TextBox007.TabIndex = 12
        '
        'Button006
        '
        Me.Button006.Image = CType(resources.GetObject("Button006.Image"), System.Drawing.Image)
        Me.Button006.Location = New System.Drawing.Point(484, 245)
        Me.Button006.Name = "Button006"
        Me.Button006.Size = New System.Drawing.Size(29, 24)
        Me.Button006.TabIndex = 19
        Me.Button006.UseVisualStyleBackColor = True
        '
        'TextBox012
        '
        Me.TextBox012.Enabled = False
        Me.TextBox012.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox012.Location = New System.Drawing.Point(520, 245)
        Me.TextBox012.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox012.Name = "TextBox012"
        Me.TextBox012.Size = New System.Drawing.Size(297, 25)
        Me.TextBox012.TabIndex = 20
        '
        'TextBox011
        '
        Me.TextBox011.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox011.Location = New System.Drawing.Point(380, 246)
        Me.TextBox011.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox011.Name = "TextBox011"
        Me.TextBox011.Size = New System.Drawing.Size(99, 25)
        Me.TextBox011.TabIndex = 18
        '
        'Button005
        '
        Me.Button005.Image = CType(resources.GetObject("Button005.Image"), System.Drawing.Image)
        Me.Button005.Location = New System.Drawing.Point(344, 214)
        Me.Button005.Name = "Button005"
        Me.Button005.Size = New System.Drawing.Size(29, 24)
        Me.Button005.TabIndex = 16
        Me.Button005.UseVisualStyleBackColor = True
        '
        'Label005
        '
        Me.Label005.AutoSize = True
        Me.Label005.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label005.Location = New System.Drawing.Point(32, 216)
        Me.Label005.Name = "Label005"
        Me.Label005.Size = New System.Drawing.Size(106, 24)
        Me.Label005.TabIndex = 55
        Me.Label005.Text = "得意先コード"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(342, 249)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 16)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "～"
        '
        'TextBox010
        '
        Me.TextBox010.Enabled = False
        Me.TextBox010.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox010.Location = New System.Drawing.Point(380, 214)
        Me.TextBox010.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox010.Name = "TextBox010"
        Me.TextBox010.Size = New System.Drawing.Size(297, 25)
        Me.TextBox010.TabIndex = 17
        '
        'TextBox009
        '
        Me.TextBox009.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox009.Location = New System.Drawing.Point(240, 215)
        Me.TextBox009.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox009.Name = "TextBox009"
        Me.TextBox009.Size = New System.Drawing.Size(99, 25)
        Me.TextBox009.TabIndex = 15
        '
        'Button007
        '
        Me.Button007.Image = CType(resources.GetObject("Button007.Image"), System.Drawing.Image)
        Me.Button007.Location = New System.Drawing.Point(791, 280)
        Me.Button007.Name = "Button007"
        Me.Button007.Size = New System.Drawing.Size(29, 24)
        Me.Button007.TabIndex = 22
        Me.Button007.UseVisualStyleBackColor = True
        '
        'TextBox013
        '
        Me.TextBox013.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox013.Location = New System.Drawing.Point(240, 280)
        Me.TextBox013.Name = "TextBox013"
        Me.TextBox013.Size = New System.Drawing.Size(545, 25)
        Me.TextBox013.TabIndex = 21
        '
        'Label006
        '
        Me.Label006.AutoSize = True
        Me.Label006.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label006.Location = New System.Drawing.Point(32, 279)
        Me.Label006.Name = "Label006"
        Me.Label006.Size = New System.Drawing.Size(106, 24)
        Me.Label006.TabIndex = 62
        Me.Label006.Text = "出力先の指定"
        '
        'ButtonExecute
        '
        Me.ButtonExecute.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonExecute.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonExecute.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonExecute.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonExecute.Location = New System.Drawing.Point(644, 322)
        Me.ButtonExecute.Name = "ButtonExecute"
        Me.ButtonExecute.Size = New System.Drawing.Size(85, 39)
        Me.ButtonExecute.TabIndex = 23
        Me.ButtonExecute.Text = "実行"
        Me.ButtonExecute.UseVisualStyleBackColor = False
        '
        'FormMTM04
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(853, 384)
        Me.Controls.Add(Me.ButtonExecute)
        Me.Controls.Add(Me.Button007)
        Me.Controls.Add(Me.TextBox013)
        Me.Controls.Add(Me.Label006)
        Me.Controls.Add(Me.Button006)
        Me.Controls.Add(Me.TextBox012)
        Me.Controls.Add(Me.TextBox011)
        Me.Controls.Add(Me.Button005)
        Me.Controls.Add(Me.Label005)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox010)
        Me.Controls.Add(Me.TextBox009)
        Me.Controls.Add(Me.Button004)
        Me.Controls.Add(Me.Label004)
        Me.Controls.Add(Me.TextBox008)
        Me.Controls.Add(Me.TextBox007)
        Me.Controls.Add(Me.Button003)
        Me.Controls.Add(Me.TextBox006)
        Me.Controls.Add(Me.TextBox005)
        Me.Controls.Add(Me.Button002)
        Me.Controls.Add(Me.Label003)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBox004)
        Me.Controls.Add(Me.TextBox003)
        Me.Controls.Add(Me.TextBox002)
        Me.Controls.Add(Me.Button001)
        Me.Controls.Add(Me.TextBox001)
        Me.Controls.Add(Me.Label002)
        Me.Controls.Add(Me.CheckBox002)
        Me.Controls.Add(Me.CheckBox001)
        Me.Controls.Add(Me.Label001)
        Me.Controls.Add(Me.ButtonClose)
        Me.Font = New System.Drawing.Font("ＭＳ 明朝", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "FormMTM04"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo]システム間連携"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonClose As Button
    Friend WithEvents Label001 As Label
    Friend WithEvents CheckBox001 As CheckBox
    Friend WithEvents CheckBox002 As CheckBox
    Friend WithEvents Label002 As Label
    Friend WithEvents TextBox002 As TextBox
    Friend WithEvents Button001 As Button
    Friend WithEvents TextBox001 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox004 As TextBox
    Friend WithEvents TextBox003 As TextBox
    Friend WithEvents Label003 As Label
    Friend WithEvents Button002 As Button
    Friend WithEvents Button003 As Button
    Friend WithEvents TextBox006 As TextBox
    Friend WithEvents TextBox005 As TextBox
    Friend WithEvents Button004 As Button
    Friend WithEvents Label004 As Label
    Friend WithEvents TextBox008 As TextBox
    Friend WithEvents TextBox007 As TextBox
    Friend WithEvents Button006 As Button
    Friend WithEvents TextBox012 As TextBox
    Friend WithEvents TextBox011 As TextBox
    Friend WithEvents Button005 As Button
    Friend WithEvents Label005 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox010 As TextBox
    Friend WithEvents TextBox009 As TextBox
    Friend WithEvents Button007 As Button
    Friend WithEvents TextBox013 As TextBox
    Friend WithEvents Label006 As Label
    Friend WithEvents ButtonExecute As Button
End Class
