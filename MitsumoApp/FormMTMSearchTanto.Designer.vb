﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMTMSearchTanto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMTMSearchTanto))
        Me.DataGridView001 = New System.Windows.Forms.DataGridView()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox001 = New System.Windows.Forms.TextBox()
        Me.ButtonSearch = New System.Windows.Forms.Button()
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
        Me.DataGridView001.Location = New System.Drawing.Point(12, 58)
        Me.DataGridView001.MultiSelect = False
        Me.DataGridView001.Name = "DataGridView001"
        Me.DataGridView001.ReadOnly = True
        Me.DataGridView001.RowHeadersVisible = False
        Me.DataGridView001.RowHeadersWidth = 62
        Me.DataGridView001.RowTemplate.Height = 27
        Me.DataGridView001.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView001.Size = New System.Drawing.Size(597, 385)
        Me.DataGridView001.TabIndex = 1
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClose.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonClose.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonClose.Location = New System.Drawing.Point(525, 460)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(85, 39)
        Me.ButtonClose.TabIndex = 4
        Me.ButtonClose.Text = "閉じる"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 24)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "担当者名"
        '
        'TextBox001
        '
        Me.TextBox001.Location = New System.Drawing.Point(92, 16)
        Me.TextBox001.Name = "TextBox001"
        Me.TextBox001.Size = New System.Drawing.Size(171, 23)
        Me.TextBox001.TabIndex = 2
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSearch.BackColor = System.Drawing.Color.MediumTurquoise
        Me.ButtonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonSearch.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonSearch.ForeColor = System.Drawing.SystemColors.Control
        Me.ButtonSearch.Location = New System.Drawing.Point(269, 16)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(65, 23)
        Me.ButtonSearch.TabIndex = 3
        Me.ButtonSearch.Text = "検索"
        Me.ButtonSearch.UseVisualStyleBackColor = False
        '
        'FormMTMSearchTanto
        '
        Me.AcceptButton = Me.ButtonSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.ButtonClose
        Me.ClientSize = New System.Drawing.Size(621, 511)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.TextBox001)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.DataGridView001)
        Me.Font = New System.Drawing.Font("ＭＳ 明朝", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimizeBox = False
        Me.Name = "FormMTMSearchTanto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[mitsumo]担当者選択"
        CType(Me.DataGridView001, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView001 As DataGridView
    Friend WithEvents ButtonClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox001 As TextBox
    Friend WithEvents ButtonSearch As Button
End Class
