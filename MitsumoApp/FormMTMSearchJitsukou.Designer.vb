<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMTMSearchJitsukou
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMSearchJitsukou))
        Me.DataGridView001 = New System.Windows.Forms.DataGridView()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox001 = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.DataGridView001, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView001
        '
        Me.DataGridView001.AllowUserToAddRows = False
        Me.DataGridView001.AllowUserToDeleteRows = False
        Me.DataGridView001.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView001.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView001.Location = New System.Drawing.Point(17, 84)
        Me.DataGridView001.MultiSelect = False
        Me.DataGridView001.Name = "DataGridView001"
        Me.DataGridView001.ReadOnly = True
        Me.DataGridView001.RowHeadersVisible = False
        Me.DataGridView001.RowHeadersWidth = 62
        Me.DataGridView001.RowTemplate.Height = 27
        Me.DataGridView001.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView001.Size = New System.Drawing.Size(1367, 401)
        Me.DataGridView001.TabIndex = 2
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(1299, 505)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(85, 39)
        Me.ButtonClose.TabIndex = 3
        Me.ButtonClose.Text = "閉じる"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.MediumTurquoise
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Location = New System.Drawing.Point(283, 505)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(260, 39)
        Me.Button1.TabIndex = 4
        Me.Button1.TabStop = False
        Me.Button1.Text = "全表示(F12)"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.MediumTurquoise
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.Control
        Me.Button2.Location = New System.Drawing.Point(17, 505)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(260, 39)
        Me.Button2.TabIndex = 5
        Me.Button2.TabStop = False
        Me.Button2.Text = "表示対象(F11)※締切内＆未確定"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'TextBox001
        '
        Me.TextBox001.Location = New System.Drawing.Point(113, 21)
        Me.TextBox001.Name = "TextBox001"
        Me.TextBox001.Size = New System.Drawing.Size(390, 23)
        Me.TextBox001.TabIndex = 0
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.label1.Location = New System.Drawing.Point(16, 21)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(90, 24)
        Me.label1.TabIndex = 12
        Me.label1.Text = "価格入力名"
        '
        'ButtonSearch
        '
        Me.ButtonSearch.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonSearch.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonSearch.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonSearch.Location = New System.Drawing.Point(708, 21)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(65, 23)
        Me.ButtonSearch.TabIndex = 1
        Me.ButtonSearch.Text = "検索"
        Me.ButtonSearch.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("メイリオ", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(502, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(181, 20)
        Me.Label18.TabIndex = 55
        Me.Label18.Text = "（全角スペースでAND検索）"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("メイリオ", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(121, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(620, 20)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "※再検索する場合は、「表示対象」または「全表示」ボタンを再度押しなおしてから検索してください。"
        '
        'FormMTMSearchJitsukou
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1404, 556)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.TextBox001)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.DataGridView001)
        Me.Font = New System.Drawing.Font("ＭＳ 明朝", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "FormMTMSearchJitsukou"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo] 価格入力番号選択"
        CType(Me.DataGridView001, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView001 As DataGridView
    Friend WithEvents ButtonClose As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox001 As TextBox
    Friend WithEvents label1 As Label
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents Label18 As Label
    Friend WithEvents Label2 As Label
End Class
