Public Class AboutForm
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim url As String = "https://github.com/rc-earnest/BetterEtch-O-Sketch.git"
        Try
            Dim psi As New System.Diagnostics.ProcessStartInfo(url) With {.UseShellExecute = True}
            System.Diagnostics.Process.Start(psi)
            CType(sender, LinkLabel).LinkVisited = True
        Catch ex As Exception
            MessageBox.Show("Unable to open link: " & ex.Message, "Open Link", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class