'Rudy Earnest
'RCET 3371
'Fall 2025
'Better Etch-O-Sketch
'https://github.com/rc-earnest/BetterEtch-O-Sketch.git
' This file implements the main form for a simple Etch-O-Sketch style drawing app.
' It supports manual drawing, rendering sine/cosine/tangent waveforms, and a PIC-mode
' that receives coordinates over a serial port and draws them.

Option Strict On
Option Explicit On
Option Compare Text

Imports System.IO.Ports         ' For SerialPort usage in PIC mode
Imports System.Media            ' For playing the shaker sound resource
Imports System.Threading.Thread ' For Sleep used during the shaking animation

Public Class Form1
    ' Class-level variables and Properties
    ' _foreColor: color used when drawing
    ' _penWidth: thickness of drawing pen
    ' _isDrawing: whether the user is currently drawing with the mouse
    ' _isPICMode: true when the app is receiving points from the PIC instead of mouse
    ' _isQYat: mode flag for different PIC protocols/baud behaviors
    Private _foreColor As Color = Color.Black
    Private _penWidth As Integer = 1
    Private _isDrawing As Boolean = False
    Private _isPICMode As Boolean = True
    Private _isQYat As Boolean = False

    ' Public property wrapper for ForegroundColor so other code can change/read it.
    Public Property ForegroundColor As Color
        Get
            Return _foreColor
        End Get
        Set(value As Color)
            _foreColor = value
        End Set
    End Property

    ' Property wrapper for PenWidth with simple validation to keep it in a sane range.
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

    ' Subs and Functions ================================================================================================================

    ' Prompts the user with a ColorDialog and updates the stored ForegroundColor and the
    ' SelectColorButton visuals to reflect the chosen color.
    Private Sub ChangeForegroundColor()
        Using colorDialog As New ColorDialog()
            Dim result As DialogResult = colorDialog.ShowDialog()
            If result = DialogResult.OK Then
                ForegroundColor = colorDialog.Color
                ' Update the button background to show the selected color
                SelectColorButton.BackColor = ForegroundColor
                ' Ensure button text remains readable by switching text color based on brightness
                If colorDialog.Color.GetBrightness > 0.4 Then
                    SelectColorButton.ForeColor = Color.Black
                Else
                    SelectColorButton.ForeColor = Color.White
                End If
            End If
        End Using
    End Sub

    ' Clears the drawing area and plays a short "shake" animation (simulates Etch-O-Sketch).
    ' Also sets _isDrawing to false to reset drawing state.
    Private Sub ClearDrawing()
        Dim shakiness As Integer = 100
        ' Try to play a shaker sound resource; on error show a message but continue.
        Try
            My.Computer.Audio.Play(My.Resources.shaker, AudioPlayMode.Background)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Playing Sound", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Simple screen shake: move the form back and forth to simulate erasing.
        For i = 1 To 25
            Me.Top += shakiness
            Me.Left += shakiness
            System.Threading.Thread.Sleep(95) ' blocking the UI; acceptable for toy app but not recommended for production
            shakiness *= -1
        Next

        ' Refresh the PictureBox to erase its contents (this uses the PictureBox default paint)
        DrawingPictureBox.Refresh()
        _isDrawing = False
    End Sub

    ' Draw a sine waveform across the width of the PictureBox.
    ' - Maps pixel x to degrees (0..360) then computes sine and scales to available vertical range.
    ' - Uses CreateGraphics for immediate drawing (not double-buffered).
    Sub DrawSinWave()
        Dim g As Graphics = DrawingPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Red)
        Dim ymax As Integer
        Dim oldX, oldY, newY As Integer
        Dim yOffset As Integer = DrawingPictureBox.Height \ 2
        Dim degreesPerPoint As Double = 360 / DrawingPictureBox.Width

        ' ymax holds the maximum vertical amplitude (negative for easier multiplication)
        ymax = yOffset - 1
        oldY = yOffset - 1
        ymax *= -1
        For x = 0 To DrawingPictureBox.Width - 1
            ' Compute the sample, scale to pixel coordinates and draw a line from previous point
            newY = CInt(ymax * Math.Sin((Math.PI / 180) * (x * degreesPerPoint))) + yOffset
            g.DrawLine(pen, oldX, oldY, x, newY)
            oldX = x
            oldY = newY
        Next
        g.Dispose()
        pen.Dispose()
    End Sub

    ' Draw a cosine waveform (same approach as the sine wave).
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

    ' Draw a tangent waveform.
    ' Notes:
    ' - Tangent has vertical asymptotes (undefined values) at 90° + k*180°, which will produce huge values.
    ' - This code checks for Infinity/NaN and clamps large values to avoid overflow and to map them
    '   to the top/bottom pixel row.
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
            ' Map pixel x to angle and compute tangent
            Dim angleDeg = x * degreesPerPoint
            Dim angleRad = (Math.PI / 180.0) * angleDeg
            Dim t = Math.Tan(angleRad)

            ' If t is infinite, NaN, or extremely large, map to top or bottom and avoid casting overflow.
            If Double.IsInfinity(t) OrElse Double.IsNaN(t) OrElse Math.Abs(t) > 1000000.0 Then
                ' Choose top (0) for positive overflow, bottom (height-1) for negative overflow.
                newY = If(t > 0, 0, DrawingPictureBox.Height - 1)
            Else
                Dim val As Double = ymax * t
                ' Clamp the computed value to integer boundaries before converting.
                val = Math.Max(Math.Min(val, Integer.MaxValue - 1), Integer.MinValue + 1)
                newY = CInt(val) + yOffset
            End If

            ' Draw a line from the previous sample to the current sample.
            g.DrawLine(pen, oldX, oldY, x, newY)
            oldX = x
            oldY = newY
        Next

        g.Dispose()
        pen.Dispose()
    End Sub

    ' Draw the graticule/gridlines on the PictureBox.
    ' Draws 11 vertical and horizontal lines spaced evenly (0 to 10 -> 11 lines).
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

    ' Event Handlers ===================================================================================================================

    ' MouseDown starts drawing only when not in PIC mode; Middle button opens color selector.
    Private Sub DrawingPictureBox_MouseDown(sender As Object, e As MouseEventArgs) Handles DrawingPictureBox.MouseDown
        If e.Button = MouseButtons.Left And _isPICMode = False Then
            _isDrawing = True
        ElseIf e.Button = MouseButtons.Middle Then
            ChangeForegroundColor()
        End If
    End Sub

    ' MouseMove draws while _isDrawing is true. It uses CreateGraphics to draw a line from
    ' the previous mouse position to the current position and updates the static previous coords.
    Private Sub DrawingPictureBox_MouseMove(sender As Object, e As MouseEventArgs) Handles DrawingPictureBox.MouseMove
        Static oldX As Integer
        Static oldy As Integer
        If _isDrawing = False Then
            ' Update the "previous" point when not drawing so the first segment starts at current cursor
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

    ' MouseUp stops drawing.
    Private Sub DrawingPictureBox_MouseUp(sender As Object, e As MouseEventArgs) Handles DrawingPictureBox.MouseUp
        If e.Button = MouseButtons.Left Then
            _isDrawing = False
        End If
    End Sub

    ' Handlers to close the application.
    Private Sub ExitMenuStripItem_Click(sender As Object, e As EventArgs) Handles ExitMenuStripItem.Click
        Me.Close()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub ExitContextMenuItem_Click(sender As Object, e As EventArgs) Handles ExitContextMenuItem.Click
        Me.Close()
    End Sub

    ' Clear the drawing (centralized ClearDrawing call) from different UI elements.
    Private Sub ClearContextMenuItem_Click(sender As Object, e As EventArgs) Handles ClearContextMenuItem.Click
        ClearDrawing()
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        ClearDrawing()
    End Sub

    Private Sub ClearMenuStripItem_Click(sender As Object, e As EventArgs) Handles ClearMenuStripItem.Click
        ClearDrawing()
    End Sub

    ' Trigger drawing of graticules and waveforms from multiple UI entry points.
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

    ' Color selection UI handlers forward to the color change helper.
    Private Sub SelectContextMenuItem_Click(sender As Object, e As EventArgs) Handles SelectContextMenuItem.Click
        ChangeForegroundColor()
    End Sub

    Private Sub SelectColorButton_Click(sender As Object, e As EventArgs) Handles SelectColorButton.Click
        ChangeForegroundColor()
    End Sub

    Private Sub SelectColorMenuStripItem_Click(sender As Object, e As EventArgs) Handles SelectColorMenuStripItem.Click
        ChangeForegroundColor()
    End Sub

    ' Show About dialog (modal).
    Private Sub AboutMenuStripItem_Click(sender As Object, e As EventArgs) Handles AboutMenuStripItem.Click
        AboutForm.ShowDialog()
    End Sub

    Private Sub AboutContextMenuItem_Click(sender As Object, e As EventArgs) Handles AboutContextMenuItem.Click
        AboutForm.ShowDialog()
    End Sub

    ' Form load initialization: set button colors and default modes.
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        SelectColorButton.ForeColor = Color.White
        SelectColorButton.BackColor = Color.Black
        DrawingRadioButton.Checked = True
        PICRadioButton.Checked = False
        _isPICMode = False
    End Sub

    ' When PIC mode radio button toggles on, prompt the user for a baud selection
    ' and attempt to open the serial port using the selected settings.
    Private Sub PICRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles PICRadioButton.CheckedChanged
        _isPICMode = True
        Dim temp As String()
        Dim result As DialogResult = MessageBox.Show("PIC Mode Activated. Press Yes for 9600 Baud no for 19200 Baud.", "PIC Mode", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        If result = DialogResult.Yes Then
            Try
                ' Get available COM port names and pick the first or show Form2 to let user choose.
                temp = SerialPort.GetPortNames()
                If temp.Length >= 1 Then
                    PICSerialPort.PortName = temp(0)
                Else
                    For Each portName In temp
                        Form2.CommComboBox.Items.Add(portName)
                        Form2.CommComboBox.SelectedIndex = 0
                    Next
                    Form2.ShowDialog()
                End If
                ' Configure serial port for the chosen PIC protocol and open it
                PICSerialPort.BaudRate = 9600
                PICSerialPort.DataBits = 8
                PICSerialPort.Parity = Parity.None
                PICSerialPort.StopBits = StopBits.One
                _isQYat = False

                PICSerialPort.Open()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Opening Serial Port", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DrawingRadioButton.Checked = True
            End Try
        ElseIf result = DialogResult.No Then
            Try
                ' Same port-selection logic, different baud for alternate PIC firmware
                temp = SerialPort.GetPortNames()
                If temp.Length >= 1 Then
                    PICSerialPort.PortName = temp(0)
                Else
                    For Each portName In temp
                        Form2.CommComboBox.Items.Add(portName)
                        Form2.CommComboBox.SelectedIndex = 0
                    Next
                    Form2.ShowDialog()
                End If

                PICSerialPort.BaudRate = 19200
                PICSerialPort.DataBits = 8
                PICSerialPort.Parity = Parity.None
                PICSerialPort.StopBits = StopBits.One
                _isQYat = True

                PICSerialPort.Open()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Opening Serial Port", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DrawingRadioButton.Checked = True
            End Try
        Else
            ' Cancelled selection - revert to drawing mode
            DrawingRadioButton.Checked = True
        End If
        ' Start the timer that polls/receives drawing coordinates from the PIC
        SendTimer.Enabled = True
    End Sub

    ' Turning drawing radio button on disables PIC-mode polling.
    Private Sub DrawingRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DrawingRadioButton.CheckedChanged
        SendTimer.Enabled = False
        _isPICMode = False
    End Sub

    ' Timer tick handler that polls or requests coordinates from the PIC and draws lines
    ' connecting sequential samples.
    Private Sub SendTimer_Tick(sender As Object, e As EventArgs) Handles SendTimer.Tick
        Static oldX As Integer
        Static oldy As Integer

        If _isQYat = False Then
            ' Old protocol: write a command and read bytes in sequence; uses Sleep for timing
            Dim dollar(0) As Byte
            Dim read As Integer
            Dim xPos As Integer
            Dim yPos As Integer
            dollar(0) = &H24
            PICSerialPort.Write(dollar, 0, 1)
            Sleep(20)
            read = PICSerialPort.ReadByte()
            If read = &H24 Then
                dollar(0) = &H3C
                PICSerialPort.Write(dollar, 0, 1)
                Sleep(20)
                xPos = PICSerialPort.ReadByte()
                PICSerialPort.DiscardInBuffer()
                dollar(0) = &H3E
                PICSerialPort.Write(dollar, 0, 1)
                Sleep(20)
                yPos = PICSerialPort.ReadByte()
                ' Draw the received point as a line from the previous point
                xPos = CInt((xPos * DrawingPictureBox.Width) / 255)
                yPos = CInt((yPos * DrawingPictureBox.Height) / 255)
                Dim g As Graphics = DrawingPictureBox.CreateGraphics()
                Dim pen As New Pen(ForegroundColor, PenWidth)
                g.DrawLine(pen, oldX, oldy, xPos, yPos)
                oldX = xPos
                oldy = yPos
                g.Dispose()
                pen.Dispose()
                PICSerialPort.DiscardInBuffer()
            End If
        ElseIf _isQYat = True Then
            ' Alternate protocol: send single-letter commands and receive raw values 0..255
            Dim send(0) As Byte
            Dim xPos As Integer
            Dim yPos As Integer
            PICSerialPort.DiscardInBuffer()
            send(0) = &H51  ' 'Q' command
            PICSerialPort.Write(send, 0, 1)
            xPos = PICSerialPort.ReadByte()
            PICSerialPort.DiscardInBuffer()
            send(0) = &H52  ' 'R' command
            PICSerialPort.Write(send, 0, 1)
            yPos = PICSerialPort.ReadByte()
            ' Map 0..255 returned from PIC to the PictureBox pixel coordinates
            xPos = CInt((xPos * DrawingPictureBox.Width) / 255)
            yPos = CInt((yPos * DrawingPictureBox.Height) / 255)
            Dim g As Graphics = DrawingPictureBox.CreateGraphics()
            Dim pen As New Pen(ForegroundColor, PenWidth)
            g.DrawLine(pen, oldX, oldy, xPos, yPos)
            oldX = xPos
            oldy = yPos
            g.Dispose()
            pen.Dispose()
            PICSerialPort.DiscardInBuffer()
        End If
    End Sub
End Class
