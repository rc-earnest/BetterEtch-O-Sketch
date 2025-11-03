<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SelectColorButton = New System.Windows.Forms.Button()
        Me.DrawWaveformsButton = New System.Windows.Forms.Button()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectColorMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DrawWaveformsMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutMenuStripItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FileContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DrawContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DrawingPictureBox = New System.Windows.Forms.PictureBox()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.PICSerialPort = New System.IO.Ports.SerialPort(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStrip.SuspendLayout()
        CType(Me.DrawingPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SelectColorButton
        '
        Me.SelectColorButton.Location = New System.Drawing.Point(283, 415)
        Me.SelectColorButton.Name = "SelectColorButton"
        Me.SelectColorButton.Size = New System.Drawing.Size(75, 23)
        Me.SelectColorButton.TabIndex = 0
        Me.SelectColorButton.Text = "Select Color"
        Me.ToolTip.SetToolTip(Me.SelectColorButton, "Select Color to Draw with")
        Me.SelectColorButton.UseVisualStyleBackColor = True
        '
        'DrawWaveformsButton
        '
        Me.DrawWaveformsButton.Location = New System.Drawing.Point(364, 415)
        Me.DrawWaveformsButton.Name = "DrawWaveformsButton"
        Me.DrawWaveformsButton.Size = New System.Drawing.Size(107, 23)
        Me.DrawWaveformsButton.TabIndex = 1
        Me.DrawWaveformsButton.Text = "Draw Waveforms"
        Me.ToolTip.SetToolTip(Me.DrawWaveformsButton, "Draw Cosine Sine and Tan with Graticules")
        Me.DrawWaveformsButton.UseVisualStyleBackColor = True
        '
        'ClearButton
        '
        Me.ClearButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ClearButton.Location = New System.Drawing.Point(477, 415)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(75, 23)
        Me.ClearButton.TabIndex = 2
        Me.ClearButton.Text = "Clear"
        Me.ToolTip.SetToolTip(Me.ClearButton, "Clears display")
        Me.ClearButton.UseVisualStyleBackColor = True
        '
        'ExitButton
        '
        Me.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ExitButton.Location = New System.Drawing.Point(713, 415)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(75, 23)
        Me.ExitButton.TabIndex = 3
        Me.ExitButton.Text = "Exit"
        Me.ToolTip.SetToolTip(Me.ExitButton, "Exits the program")
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenuStripItem, Me.EditMenuStripItem, Me.HelpMenuStripItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileMenuStripItem
        '
        Me.FileMenuStripItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitMenuStripItem})
        Me.FileMenuStripItem.Name = "FileMenuStripItem"
        Me.FileMenuStripItem.Size = New System.Drawing.Size(37, 20)
        Me.FileMenuStripItem.Text = "File"
        '
        'ExitMenuStripItem
        '
        Me.ExitMenuStripItem.Name = "ExitMenuStripItem"
        Me.ExitMenuStripItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitMenuStripItem.Text = "Exit"
        '
        'EditMenuStripItem
        '
        Me.EditMenuStripItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectColorMenuStripItem, Me.DrawWaveformsMenuStripItem, Me.ClearMenuStripItem})
        Me.EditMenuStripItem.Name = "EditMenuStripItem"
        Me.EditMenuStripItem.Size = New System.Drawing.Size(39, 20)
        Me.EditMenuStripItem.Text = "Edit"
        '
        'SelectColorMenuStripItem
        '
        Me.SelectColorMenuStripItem.Name = "SelectColorMenuStripItem"
        Me.SelectColorMenuStripItem.Size = New System.Drawing.Size(180, 22)
        Me.SelectColorMenuStripItem.Text = "Select Color"
        '
        'DrawWaveformsMenuStripItem
        '
        Me.DrawWaveformsMenuStripItem.Name = "DrawWaveformsMenuStripItem"
        Me.DrawWaveformsMenuStripItem.Size = New System.Drawing.Size(180, 22)
        Me.DrawWaveformsMenuStripItem.Text = "Draw Waveforms"
        '
        'ClearMenuStripItem
        '
        Me.ClearMenuStripItem.Name = "ClearMenuStripItem"
        Me.ClearMenuStripItem.Size = New System.Drawing.Size(180, 22)
        Me.ClearMenuStripItem.Text = "Clear"
        '
        'HelpMenuStripItem
        '
        Me.HelpMenuStripItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutMenuStripItem})
        Me.HelpMenuStripItem.Name = "HelpMenuStripItem"
        Me.HelpMenuStripItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpMenuStripItem.Text = "Help"
        '
        'AboutMenuStripItem
        '
        Me.AboutMenuStripItem.Name = "AboutMenuStripItem"
        Me.AboutMenuStripItem.Size = New System.Drawing.Size(180, 22)
        Me.AboutMenuStripItem.Text = "About"
        '
        'ContextMenuStrip
        '
        Me.ContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileContextMenuItem, Me.EditContextMenuItem, Me.HelpContextMenuItem})
        Me.ContextMenuStrip.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip.Size = New System.Drawing.Size(100, 70)
        '
        'FileContextMenuItem
        '
        Me.FileContextMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitContextMenuItem})
        Me.FileContextMenuItem.Name = "FileContextMenuItem"
        Me.FileContextMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.FileContextMenuItem.Text = "File"
        '
        'ExitContextMenuItem
        '
        Me.ExitContextMenuItem.Name = "ExitContextMenuItem"
        Me.ExitContextMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitContextMenuItem.Text = "Exit"
        '
        'EditContextMenuItem
        '
        Me.EditContextMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectContextMenuItem, Me.DrawContextMenuItem, Me.ClearContextMenuItem})
        Me.EditContextMenuItem.Name = "EditContextMenuItem"
        Me.EditContextMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.EditContextMenuItem.Text = "Edit"
        '
        'SelectContextMenuItem
        '
        Me.SelectContextMenuItem.Name = "SelectContextMenuItem"
        Me.SelectContextMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SelectContextMenuItem.Text = "Select Color"
        '
        'DrawContextMenuItem
        '
        Me.DrawContextMenuItem.Name = "DrawContextMenuItem"
        Me.DrawContextMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DrawContextMenuItem.Text = "Draw Waveforms"
        '
        'ClearContextMenuItem
        '
        Me.ClearContextMenuItem.Name = "ClearContextMenuItem"
        Me.ClearContextMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ClearContextMenuItem.Text = "Clear"
        '
        'HelpContextMenuItem
        '
        Me.HelpContextMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutContextMenuItem})
        Me.HelpContextMenuItem.Name = "HelpContextMenuItem"
        Me.HelpContextMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.HelpContextMenuItem.Text = "Help"
        '
        'AboutContextMenuItem
        '
        Me.AboutContextMenuItem.Name = "AboutContextMenuItem"
        Me.AboutContextMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AboutContextMenuItem.Text = "About"
        '
        'DrawingPictureBox
        '
        Me.DrawingPictureBox.BackColor = System.Drawing.Color.Beige
        Me.DrawingPictureBox.Cursor = System.Windows.Forms.Cursors.Cross
        Me.DrawingPictureBox.Location = New System.Drawing.Point(12, 27)
        Me.DrawingPictureBox.Name = "DrawingPictureBox"
        Me.DrawingPictureBox.Size = New System.Drawing.Size(776, 382)
        Me.DrawingPictureBox.TabIndex = 5
        Me.DrawingPictureBox.TabStop = False
        Me.ToolTip.SetToolTip(Me.DrawingPictureBox, "Draw within this space")
        '
        'Form1
        '
        Me.AcceptButton = Me.DrawWaveformsButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ClearButton
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.DrawingPictureBox)
        Me.Controls.Add(Me.ExitButton)
        Me.Controls.Add(Me.ClearButton)
        Me.Controls.Add(Me.DrawWaveformsButton)
        Me.Controls.Add(Me.SelectColorButton)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Etch-O-Sketch"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStrip.ResumeLayout(False)
        CType(Me.DrawingPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SelectColorButton As Button
    Friend WithEvents DrawWaveformsButton As Button
    Friend WithEvents ClearButton As Button
    Friend WithEvents ExitButton As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileMenuStripItem As ToolStripMenuItem
    Friend WithEvents ExitMenuStripItem As ToolStripMenuItem
    Friend WithEvents EditMenuStripItem As ToolStripMenuItem
    Friend WithEvents SelectColorMenuStripItem As ToolStripMenuItem
    Friend WithEvents DrawWaveformsMenuStripItem As ToolStripMenuItem
    Friend WithEvents ClearMenuStripItem As ToolStripMenuItem
    Friend WithEvents HelpMenuStripItem As ToolStripMenuItem
    Friend WithEvents AboutMenuStripItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip As ContextMenuStrip
    Friend WithEvents FileContextMenuItem As ToolStripMenuItem
    Friend WithEvents EditContextMenuItem As ToolStripMenuItem
    Friend WithEvents SelectContextMenuItem As ToolStripMenuItem
    Friend WithEvents DrawContextMenuItem As ToolStripMenuItem
    Friend WithEvents ClearContextMenuItem As ToolStripMenuItem
    Friend WithEvents HelpContextMenuItem As ToolStripMenuItem
    Friend WithEvents AboutContextMenuItem As ToolStripMenuItem
    Friend WithEvents ExitContextMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents DrawingPictureBox As PictureBox
    Friend WithEvents PICSerialPort As IO.Ports.SerialPort
End Class
