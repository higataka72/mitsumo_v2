<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMTMSearchTankaV2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMSearchTankaV2))
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.DataGridView001 = New C1.Win.C1FlexGrid.C1FlexGrid()
        CType(Me.DataGridView001, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(768, 463)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(117, 34)
        Me.ButtonClose.TabIndex = 5
        Me.ButtonClose.Text = "閉じる(ESC)"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'DataGridView001
        '
        Me.DataGridView001.AllowFiltering = True
        Me.DataGridView001.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView001.AutoClipboard = True
        Me.DataGridView001.ClipboardCopyMode = C1.Win.C1FlexGrid.ClipboardCopyModeEnum.DataAndColumnHeaders
        Me.DataGridView001.ColumnInfo = "10,1,0,0,0,-1,Columns:"
        Me.DataGridView001.Location = New System.Drawing.Point(12, 12)
        Me.DataGridView001.Name = "DataGridView001"
        Me.DataGridView001.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.DataGridView001.Size = New System.Drawing.Size(873, 434)
        Me.DataGridView001.TabIndex = 6
        '
        'FormMTMSearchTankaV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(901, 507)
        Me.Controls.Add(Me.DataGridView001)
        Me.Controls.Add(Me.ButtonClose)
        Me.Font = New System.Drawing.Font("ＭＳ 明朝", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimizeBox = False
        Me.Name = "FormMTMSearchTankaV2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo]単価台帳参照"
        CType(Me.DataGridView001, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonClose As Button
    Friend WithEvents DataGridView001 As C1.Win.C1FlexGrid.C1FlexGrid
End Class
