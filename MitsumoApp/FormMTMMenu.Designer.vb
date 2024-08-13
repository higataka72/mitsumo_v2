<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMTMMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMMenu))
        Me.LabelVer = New System.Windows.Forms.Label()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonMTM02 = New System.Windows.Forms.Button()
        Me.ButtonMTM03 = New System.Windows.Forms.Button()
        Me.ButtonMTM01 = New System.Windows.Forms.Button()
        Me.ButtonMTM04 = New System.Windows.Forms.Button()
        Me.ButtonMTM05 = New System.Windows.Forms.Button()
        Me.ButtonMTMUser = New System.Windows.Forms.Button()
        Me.ButtonUser = New System.Windows.Forms.Button()
        Me.ButtonAdmin = New System.Windows.Forms.Button()
        Me.ButtonKensho01 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelVer
        '
        Me.LabelVer.AutoSize = True
        Me.LabelVer.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LabelVer.Location = New System.Drawing.Point(629, 23)
        Me.LabelVer.Name = "LabelVer"
        Me.LabelVer.Size = New System.Drawing.Size(65, 18)
        Me.LabelVer.TabIndex = 2
        Me.LabelVer.Text = "ver : 1.50"
        '
        'ButtonClose
        '
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonClose.ForeColor = System.Drawing.Color.White
        Me.ButtonClose.Image = CType(resources.GetObject("ButtonClose.Image"), System.Drawing.Image)
        Me.ButtonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonClose.Location = New System.Drawing.Point(537, 379)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(157, 66)
        Me.ButtonClose.TabIndex = 3
        Me.ButtonClose.TabStop = False
        Me.ButtonClose.Text = "  終了"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'ButtonMTM02
        '
        Me.ButtonMTM02.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonMTM02.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.ButtonMTM02.Image = CType(resources.GetObject("ButtonMTM02.Image"), System.Drawing.Image)
        Me.ButtonMTM02.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMTM02.Location = New System.Drawing.Point(46, 139)
        Me.ButtonMTM02.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonMTM02.Name = "ButtonMTM02"
        Me.ButtonMTM02.Size = New System.Drawing.Size(257, 87)
        Me.ButtonMTM02.TabIndex = 2
        Me.ButtonMTM02.Text = "価格入力"
        Me.ButtonMTM02.UseVisualStyleBackColor = True
        '
        'ButtonMTM03
        '
        Me.ButtonMTM03.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonMTM03.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.ButtonMTM03.Image = CType(resources.GetObject("ButtonMTM03.Image"), System.Drawing.Image)
        Me.ButtonMTM03.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMTM03.Location = New System.Drawing.Point(404, 139)
        Me.ButtonMTM03.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonMTM03.Name = "ButtonMTM03"
        Me.ButtonMTM03.Size = New System.Drawing.Size(257, 87)
        Me.ButtonMTM03.TabIndex = 3
        Me.ButtonMTM03.Text = "見積送信"
        Me.ButtonMTM03.UseVisualStyleBackColor = True
        '
        'ButtonMTM01
        '
        Me.ButtonMTM01.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonMTM01.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonMTM01.Image = CType(resources.GetObject("ButtonMTM01.Image"), System.Drawing.Image)
        Me.ButtonMTM01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMTM01.Location = New System.Drawing.Point(46, 139)
        Me.ButtonMTM01.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonMTM01.Name = "ButtonMTM01"
        Me.ButtonMTM01.Size = New System.Drawing.Size(257, 87)
        Me.ButtonMTM01.TabIndex = 2
        Me.ButtonMTM01.Text = "    単価入力シート取込"
        Me.ButtonMTM01.UseVisualStyleBackColor = True
        '
        'ButtonMTM04
        '
        Me.ButtonMTM04.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonMTM04.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.ButtonMTM04.Image = CType(resources.GetObject("ButtonMTM04.Image"), System.Drawing.Image)
        Me.ButtonMTM04.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMTM04.Location = New System.Drawing.Point(404, 139)
        Me.ButtonMTM04.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonMTM04.Name = "ButtonMTM04"
        Me.ButtonMTM04.Size = New System.Drawing.Size(257, 87)
        Me.ButtonMTM04.TabIndex = 4
        Me.ButtonMTM04.Text = "   システム間連携"
        Me.ButtonMTM04.UseVisualStyleBackColor = True
        '
        'ButtonMTM05
        '
        Me.ButtonMTM05.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonMTM05.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.ButtonMTM05.Image = CType(resources.GetObject("ButtonMTM05.Image"), System.Drawing.Image)
        Me.ButtonMTM05.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMTM05.Location = New System.Drawing.Point(46, 248)
        Me.ButtonMTM05.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonMTM05.Name = "ButtonMTM05"
        Me.ButtonMTM05.Size = New System.Drawing.Size(257, 87)
        Me.ButtonMTM05.TabIndex = 5
        Me.ButtonMTM05.Text = "    実行管理テーブル"
        Me.ButtonMTM05.UseVisualStyleBackColor = True
        '
        'ButtonMTMUser
        '
        Me.ButtonMTMUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonMTMUser.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.ButtonMTMUser.Image = CType(resources.GetObject("ButtonMTMUser.Image"), System.Drawing.Image)
        Me.ButtonMTMUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMTMUser.Location = New System.Drawing.Point(404, 248)
        Me.ButtonMTMUser.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonMTMUser.Name = "ButtonMTMUser"
        Me.ButtonMTMUser.Size = New System.Drawing.Size(257, 87)
        Me.ButtonMTMUser.TabIndex = 6
        Me.ButtonMTMUser.Text = "  ユーザー登録"
        Me.ButtonMTMUser.UseVisualStyleBackColor = True
        '
        'ButtonUser
        '
        Me.ButtonUser.BackColor = System.Drawing.Color.LightSeaGreen
        Me.ButtonUser.FlatAppearance.BorderColor = System.Drawing.Color.MintCream
        Me.ButtonUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonUser.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonUser.ForeColor = System.Drawing.Color.White
        Me.ButtonUser.Location = New System.Drawing.Point(1, 64)
        Me.ButtonUser.Name = "ButtonUser"
        Me.ButtonUser.Size = New System.Drawing.Size(360, 45)
        Me.ButtonUser.TabIndex = 7
        Me.ButtonUser.Text = "ユーザー画面"
        Me.ButtonUser.UseVisualStyleBackColor = False
        '
        'ButtonAdmin
        '
        Me.ButtonAdmin.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonAdmin.FlatAppearance.BorderColor = System.Drawing.Color.MintCream
        Me.ButtonAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonAdmin.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonAdmin.ForeColor = System.Drawing.Color.White
        Me.ButtonAdmin.Location = New System.Drawing.Point(359, 64)
        Me.ButtonAdmin.Name = "ButtonAdmin"
        Me.ButtonAdmin.Size = New System.Drawing.Size(360, 45)
        Me.ButtonAdmin.TabIndex = 8
        Me.ButtonAdmin.Text = "管理者画面"
        Me.ButtonAdmin.UseVisualStyleBackColor = False
        '
        'ButtonKensho01
        '
        Me.ButtonKensho01.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonKensho01.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.ButtonKensho01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonKensho01.Location = New System.Drawing.Point(46, 379)
        Me.ButtonKensho01.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonKensho01.Name = "ButtonKensho01"
        Me.ButtonKensho01.Size = New System.Drawing.Size(197, 43)
        Me.ButtonKensho01.TabIndex = 9
        Me.ButtonKensho01.Text = "検証SQL(元々)"
        Me.ButtonKensho01.UseVisualStyleBackColor = True
        Me.ButtonKensho01.Visible = False
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(46, 428)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(197, 43)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "検証SQL(関数変更)"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'FormMTMMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(720, 488)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ButtonKensho01)
        Me.Controls.Add(Me.ButtonAdmin)
        Me.Controls.Add(Me.ButtonUser)
        Me.Controls.Add(Me.ButtonMTM01)
        Me.Controls.Add(Me.ButtonMTM04)
        Me.Controls.Add(Me.ButtonMTMUser)
        Me.Controls.Add(Me.ButtonMTM03)
        Me.Controls.Add(Me.ButtonMTM05)
        Me.Controls.Add(Me.ButtonMTM02)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelVer)
        Me.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.MaximizeBox = False
        Me.Name = "FormMTMMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo] メインメニュー"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelVer As Label
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ButtonMTM03 As Button
    Friend WithEvents ButtonMTM02 As Button
    Friend WithEvents ButtonMTMUser As Button
    Friend WithEvents ButtonMTM05 As Button
    Friend WithEvents ButtonMTM04 As Button
    Friend WithEvents ButtonMTM01 As Button
    Friend WithEvents ButtonUser As Button
    Friend WithEvents ButtonAdmin As Button
    Friend WithEvents ButtonKensho01 As Button
    Friend WithEvents Button1 As Button
End Class
