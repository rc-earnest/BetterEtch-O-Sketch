'Rudy Earnest
'RCET 3371
'Fall 2025
'Better Etch-O-Sketch
Option Strict On
Option Explicit On
Option Compare Text

Imports System.Media
Imports System.Threading.Thread
Public Class Form1
    'Class-level variables and Properties--------------------------------------------------------------------------------
    Private _foreColor As Color = Color.Black
    Private _penWidth As Integer = 1
    Private _isDrawing As Boolean = False

    Public Property ForegroundColor As Color
        Get
            Return _foreColor
        End Get
        Set(value As Color)
            _foreColor = value
        End Set
    End Property

    Public Property PenWidth As Integer
        Get
            Return _penWidth
        End Get
        Set(value As Integer)
            If value > 100 Then
                _penWidth = 100
            ElseIf value > 0 Then
                _penWidth = value
            End If
        End Set
    End Property

    'Subs and Functions=================================================================================================================

    Private Sub ChangeForegroundColor()
        Using colorDialog As New ColorDialog()
            Dim result As DialogResult = colorDialog.ShowDialog()
            If result = DialogResult.OK Then
                ForegroundColor = colorDialog.Color
                ' Change the background color of the button:
                SelectColorButton.BackColor = ForegroundColor
                ' Change the text color of the button
                If colorDialog.Color.GetBrightness > 0.4 Then
                    SelectColorButton.ForeColor = Color.Black
                Else
                    SelectColorButton.ForeColor = Color.White
                End If
            End If
        End Using
    End Sub

    Private Sub ClearDrawing()
        Dim shakiness As Integer = 100
        Try
            My.Computer.Audio.Play(My.Resources.shaker, AudioPlayMode.Background)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Playing Sound", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        For i = 1 To 25
            Me.Top += shakiness
            Me.Left += shakiness
            System.Threading.Thread.Sleep(95)
            shakiness *= -1
        Next

        DrawingPictureBox.Refresh()
        _isDrawing = False
    End Sub

    ' Sub to draw a sine wave on the picture box
    Sub DrawSinWave()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Red)
        Dim ymax As Integer
        Dim oldX, oldY, newY As Integer
        Dim yOffset As Integer = DrawingPictureBox.Height \ 2
        Dim degreesPerPoint As Double = 360 / DrawingPictureBox.Width

        ymax = yOffset - 1
        oldY = yOffset - 1
        ymax *= -1
        For x = 0 To DrawingPictureBox.Width - 1
            newY = CInt(ymax * Math.Sin((Math.PI / 180) * (x * degreesPerPoint))) + yOffset
            g.DrawLine(pen, oldX, oldY, x, newY)
            oldX = x
            oldY = newY
        Next
        g.Dispose()
        pen.Dispose()
    End Sub

    ' Sub to draw a cosine wave on the picture box
    Sub DrawCosWave()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Blue)
        Dim ymax As Integer
        Dim oldX, oldY, newY As Integer
        Dim yOffset As Integer = DrawingPictureBox.Height \ 2
        Dim degreesPerPoint As Double = 360 / DrawingPictureBox.Width

        ymax = yOffset - 1
        oldY = yOffset - 1
        ymax *= -1
        For x = 0 To DrawingPictureBox.Width - 1
            newY = CInt(ymax * Math.Cos((Math.PI / 180) * (x * degreesPerPoint))) + yOffset
            g.DrawLine(pen, oldX, oldY, x, newY)
            oldX = x
            oldY = newY
        Next
        g.Dispose()
        pen.Dispose()
    End Sub

    ' Sub to draw a tangent wave on the picture box
    Sub DrawTanWave()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Green)
        Dim ymax As Integer
        Dim oldX, oldY, newY As Integer
        Dim yOffset As Integer = DrawingPictureBox.Height \ 2
        Dim degreesPerPoint As Double = 360 / DrawingPictureBox.Width

        ymax = yOffset - 1
        oldY = yOffset - 1
        ymax *= -1
        For x = 0 To DrawingPictureBox.Width - 1
            Dim angleDeg = x * degreesPerPoint
            Dim angleRad = (Math.PI / 180.0) * angleDeg
            Dim t = Math.Tan(angleRad)

            If Double.IsInfinity(t) OrElse Double.IsNaN(t) OrElse Math.Abs(t) > 1000000.0 Then
                ' skip drawing this segment or clamp to top/bottom pixel
                newY = If(t > 0, 0, DrawingPictureBox.Height - 1)
            Else
                Dim val As Double = ymax * t
                ' clamp to integer range and visible bounds before CInt
                val = Math.Max(Math.Min(val, Integer.MaxValue - 1), Integer.MinValue + 1)
                newY = CInt(val) + yOffset
            End If
            g.DrawLine(pen, oldX, oldY, x, newY)
            oldX = x
            oldY = newY
        Next

        g.Dispose()
        pen.Dispose()
    End Sub

    Sub DrawGraticules()
        Dim pen As New Pen(Color.Black)
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim yMax As Integer = DrawingPictureBox.Height
        Dim xMax As Integer = DrawingPictureBox.Width
        For i As Integer = 0 To 10
            g.DrawLine(pen, CSng(xMax * (i / 10)), 0, CSng(xMax * (i / 10)), yMax)
            g.DrawLine(pen, 0, CSng(yMax * (i / 10)), xMax, CSng(yMax * (i / 10)))
        Next
        g.Dispose()
        pen.Dispose()
    End Sub

    'Event Handlers=====================================================================================================================
    Private Sub DrawingPictureBox_MouseDown(sender As Object, e As MouseEventArgs) Handles DrawingPictureBox.MouseDown
        If e.Button = MouseButtons.Left Then
            _isDrawing = True
        ElseIf e.Button = MouseButtons.Middle Then
            ChangeForegroundColor()

        End If
    End Sub

    Private Sub DrawingPictureBox_MouseMove(sender As Object, e As MouseEventArgs) Handles DrawingPictureBox.MouseMove
        Static oldX As Integer
        Static oldy As Integer
        If _isDrawing = False Then
            oldX = e.X
            oldy = e.Y
        Else
            Dim g As Graphics = DrawingPictureBox.CreateGraphics()
            Dim pen As New Pen(ForegroundColor, PenWidth)
            g.DrawLine(pen, oldX, oldy, e.X, e.Y)
            oldX = e.X
            oldy = e.Y
            g.Dispose()
            pen.Dispose()
        End If
    End Sub

    Private Sub DrawingPictureBox_MouseUp(sender As Object, e As MouseEventArgs) Handles DrawingPictureBox.MouseUp
        If e.Button = MouseButtons.Left Then
            _isDrawing = False
        End If
    End Sub

    Private Sub ExitMenuStripItem_Click(sender As Object, e As EventArgs) Handles ExitMenuStripItem.Click
        Me.Close()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub ExitContextMenuItem_Click(sender As Object, e As EventArgs) Handles ExitContextMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ClearContextMenuItem_Click(sender As Object, e As EventArgs) Handles ClearContextMenuItem.Click
        ClearDrawing()
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        ClearDrawing()
    End Sub

    Private Sub ClearMenuStripItem_Click(sender As Object, e As EventArgs) Handles ClearMenuStripItem.Click
        ClearDrawing()
    End Sub

    Private Sub DrawWaveformsMenuStripItem_Click(sender As Object, e As EventArgs) Handles DrawWaveformsMenuStripItem.Click
        ClearDrawing()
        DrawGraticules()
        DrawSinWave()
        DrawCosWave()
        DrawTanWave()
    End Sub

    Private Sub DrawWaveformsButton_Click(sender As Object, e As EventArgs) Handles DrawWaveformsButton.Click
        ClearDrawing()
        DrawGraticules()
        DrawSinWave()
        DrawCosWave()
        DrawTanWave()
    End Sub

    Private Sub DrawContextMenuItem_Click(sender As Object, e As EventArgs) Handles DrawContextMenuItem.Click
        ClearDrawing()
        DrawGraticules()
        DrawSinWave()
        DrawCosWave()
        DrawTanWave()
    End Sub

    Private Sub SelectContextMenuItem_Click(sender As Object, e As EventArgs) Handles SelectContextMenuItem.Click
        ChangeForegroundColor()
    End Sub

    Private Sub SelectColorButton_Click(sender As Object, e As EventArgs) Handles SelectColorButton.Click
        ChangeForegroundColor()
    End Sub

    Private Sub SelectColorMenuStripItem_Click(sender As Object, e As EventArgs) Handles SelectColorMenuStripItem.Click
        ChangeForegroundColor()
    End Sub

    Private Sub AboutMenuStripItem_Click(sender As Object, e As EventArgs) Handles AboutMenuStripItem.Click
        AboutForm.ShowDialog()
    End Sub

    Private Sub AboutContextMenuItem_Click(sender As Object, e As EventArgs) Handles AboutContextMenuItem.Click
        AboutForm.ShowDialog()
    End Sub
End Class
