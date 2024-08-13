<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMTMUser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMUser))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox001 = New System.Windows.Forms.TextBox()
        Me.TextBox002 = New System.Windows.Forms.TextBox()
        Me.TextBox003 = New System.Windows.Forms.TextBox()
        Me.ButtonRegist = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox003_2 = New System.Windows.Forms.TextBox()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ComboBox001 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button002 = New System.Windows.Forms.Button()
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ログインID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "ログイン名"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(19, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 24)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "パスワード"
        '
        'TextBox001
        '
        Me.TextBox001.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox001.Location = New System.Drawing.Point(175, 83)
        Me.TextBox001.MaxLength = 50
        Me.TextBox001.Name = "TextBox001"
        Me.TextBox001.Size = New System.Drawing.Size(344, 25)
        Me.TextBox001.TabIndex = 0
        '
        'TextBox002
        '
        Me.TextBox002.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox002.Location = New System.Drawing.Point(175, 114)
        Me.TextBox002.MaxLength = 24
        Me.TextBox002.Name = "TextBox002"
        Me.TextBox002.Size = New System.Drawing.Size(344, 25)
        Me.TextBox002.TabIndex = 2
        '
        'TextBox003
        '
        Me.TextBox003.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox003.Location = New System.Drawing.Point(175, 144)
        Me.TextBox003.MaxLength = 10
        Me.TextBox003.Name = "TextBox003"
        Me.TextBox003.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox003.Size = New System.Drawing.Size(109, 25)
        Me.TextBox003.TabIndex = 3
        '
        'ButtonRegist
        '
        Me.ButtonRegist.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonRegist.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonRegist.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonRegist.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonRegist.Location = New System.Drawing.Point(257, 220)
        Me.ButtonRegist.Name = "ButtonRegist"
        Me.ButtonRegist.Size = New System.Drawing.Size(96, 40)
        Me.ButtonRegist.TabIndex = 5
        Me.ButtonRegist.Text = "登録"
        Me.ButtonRegist.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(19, 174)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(154, 24)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "パスワード（確認）"
        '
        'TextBox003_2
        '
        Me.TextBox003_2.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox003_2.Location = New System.Drawing.Point(175, 173)
        Me.TextBox003_2.MaxLength = 10
        Me.TextBox003_2.Name = "TextBox003_2"
        Me.TextBox003_2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox003_2.Size = New System.Drawing.Size(109, 25)
        Me.TextBox003_2.TabIndex = 4
        '
        'ButtonClose
        '
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(461, 220)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(96, 40)
        Me.ButtonClose.TabIndex = 7
        Me.ButtonClose.Text = "閉じる"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'ComboBox001
        '
        Me.ComboBox001.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox001.FormattingEnabled = True
        Me.ComboBox001.Location = New System.Drawing.Point(175, 26)
        Me.ComboBox001.Name = "ComboBox001"
        Me.ComboBox001.Size = New System.Drawing.Size(344, 26)
        Me.ComboBox001.TabIndex = 7
        Me.ComboBox001.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(19, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 24)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "担当者選択"
        '
        'Button002
        '
        Me.Button002.Image = CType(resources.GetObject("Button002.Image"), System.Drawing.Image)
        Me.Button002.Location = New System.Drawing.Point(525, 83)
        Me.Button002.Name = "Button002"
        Me.Button002.Size = New System.Drawing.Size(29, 24)
        Me.Button002.TabIndex = 10
        Me.Button002.TabStop = False
        Me.Button002.UseVisualStyleBackColor = True
        '
        'ButtonDelete
        '
        Me.ButtonDelete.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonDelete.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonDelete.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonDelete.Location = New System.Drawing.Point(359, 220)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(96, 40)
        Me.ButtonDelete.TabIndex = 6
        Me.ButtonDelete.Text = "削除"
        Me.ButtonDelete.UseVisualStyleBackColor = False
        '
        'FormMTMUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(573, 277)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.Button002)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBox001)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.TextBox003_2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ButtonRegist)
        Me.Controls.Add(Me.TextBox003)
        Me.Controls.Add(Me.TextBox002)
        Me.Controls.Add(Me.TextBox001)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("ＭＳ 明朝", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMTMUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo] ユーザーマスタ登録"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox001 As TextBox
    Friend WithEvents TextBox002 As TextBox
    Friend WithEvents TextBox003 As TextBox
    Friend WithEvents ButtonRegist As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox003_2 As TextBox
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ComboBox001 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button002 As Button
    Friend WithEvents ButtonDelete As Button
End Class
