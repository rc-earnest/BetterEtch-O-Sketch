<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.CommComboBox = New System.Windows.Forms.ComboBox()
        Me.CommPortLabel = New System.Windows.Forms.Label()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CommComboBox
        '
        Me.CommComboBox.FormattingEnabled = True
        Me.CommComboBox.Location = New System.Drawing.Point(157, 129)
        Me.CommComboBox.Name = "CommComboBox"
        Me.CommComboBox.Size = New System.Drawing.Size(121, 21)
        Me.CommComboBox.TabIndex = 0
        '
        'CommPortLabel
        '
        Me.CommPortLabel.AutoSize = True
        Me.CommPortLabel.Location = New System.Drawing.Point(154, 100)
        Me.CommPortLabel.Name = "CommPortLabel"
        Me.CommPortLabel.Size = New System.Drawing.Size(126, 13)
        Me.CommPortLabel.TabIndex = 1
        Me.CommPortLabel.Text = "Please Select Comm Port"
        '
        'ExitButton
        '
        Me.ExitButton.Location = New System.Drawing.Point(182, 206)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(75, 23)
        Me.ExitButton.TabIndex = 2
        Me.ExitButton.Text = "Exit"
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 241)
        Me.Controls.Add(Me.ExitButton)
        Me.Controls.Add(Me.CommPortLabel)
        Me.Controls.Add(Me.CommComboBox)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CommComboBox As ComboBox
    Friend WithEvents CommPortLabel As Label
    Friend WithEvents ExitButton As Button
End Class
