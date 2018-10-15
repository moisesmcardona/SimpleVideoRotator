Imports System.Net
Imports Microsoft.Win32

Public Class Form1
    Dim InputFileList As New ListBox
    Dim OutputFileList As New ListBox
    Dim RotationParameters As New ListBox
    Dim CurrInputFile As String
    Dim CurrOutputFile As String
    Dim CurrRotation As Integer
    Public Sub ProcessStart(ByRef Executable, ByRef CommandLine)
        Dim P As New Process
        With P
            .StartInfo.FileName = Executable
            .StartInfo.Arguments = CommandLine
            .StartInfo.RedirectStandardOutput = False
            .StartInfo.RedirectStandardError = False
            .StartInfo.UseShellExecute = True
            .StartInfo.CreateNoWindow = False
            .Start()
            .WaitForExit()
        End With
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("An Input video is needed in order to process the rotation")
        ElseIf TextBox2.Text = "" Then
            MsgBox("An Output location is needed in order to process the rotation")
        ElseIf TextBox3.Text = "" Then
            MsgBox("Rotation value is not valid. It must be a number between 0 and 360")
        ElseIf TextBox3.Text < 0 Or TextBox3.Text > 360 Then
            MsgBox("Rotation value is not valid. It must be a number between 0 and 360")
        Else
            CurrInputFile = TextBox1.Text
            CurrOutputFile = TextBox2.Text
            CurrRotation = TextBox3.Text
            If CheckBox1.Checked = False Then
                ProcessStart("ffmpeg.exe", "-i """ & TextBox1.Text & """ -c copy -metadata:s:v:0 rotate=" & TextBox3.Text & " """ & TextBox2.Text & """")
            Else
                ProcessStart("ffmpeg.exe", "-i """ & TextBox1.Text & """ -c copy -metadata:s:v:0 rotate=" & TextBox3.Text & " """ & TextBox2.Text & "\" & My.Computer.FileSystem.GetName(TextBox1.Text) & """")
            End If
            MsgBox("Video processed!")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" = False Then OpenFileDialog1.FileName = My.Computer.FileSystem.GetName(TextBox1.Text) Else OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "MP4 Video (*.mp4)|*.mp4|Apple iOS device video (*.mov)|*.mov|All file types|*.*"
        OpenFileDialog1.Title = "Select a source video to process"
        Dim DialogOk As DialogResult = OpenFileDialog1.ShowDialog
        If DialogOk = Windows.Forms.DialogResult.OK Then If OpenFileDialog1.FileName = "" = False Then TextBox1.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox1.Checked = False Then
            If TextBox2.Text = "" = False Then SaveFileDialog2.FileName = My.Computer.FileSystem.GetName(TextBox2.Text) Else SaveFileDialog2.FileName = ""
            SaveFileDialog2.Filter = "MP4 Video (*.mp4)|*.mp4|Apple iOS device video (*.mov)|*.mov|All file types|*.*"
            SaveFileDialog2.Title = "Select a destination place and type a filename"
            Dim DialogOk As DialogResult = SaveFileDialog2.ShowDialog
            If DialogOk = Windows.Forms.DialogResult.OK Then If SaveFileDialog2.FileName = "" = False Then TextBox2.Text = SaveFileDialog2.FileName
        Else
            FolderBrowserDialog1.ShowNewFolderButton = True
            Dim DialogOk As DialogResult = FolderBrowserDialog1.ShowDialog
            If DialogOk = Windows.Forms.DialogResult.OK Then If FolderBrowserDialog1.SelectedPath = "" = False Then TextBox2.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox1.Text = "" Then
            MsgBox("An Input video is needed in order to process the rotation")
        ElseIf TextBox2.Text = "" Then
            MsgBox("An Output location is needed in order to process the rotation")
        ElseIf TextBox3.Text = "" Then
            MsgBox("Rotation value is not valid. It must be a number between 0 and 360")
        ElseIf TextBox3.Text < 0 Or TextBox3.Text > 360 Then
            MsgBox("Rotation value is not valid. It must be a number between 0 and 360")
        Else
            ListBox1.Items.Add(My.Computer.FileSystem.GetName(TextBox1.Text))
            InputFileList.Items.Add(TextBox1.Text)
            If CheckBox1.Checked = False Then OutputFileList.Items.Add(TextBox2.Text) Else OutputFileList.Items.Add(TextBox2.Text & "\" & My.Computer.FileSystem.GetName(TextBox1.Text))
            RotationParameters.Items.Add(TextBox3.Text)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Index As Integer = ListBox1.SelectedIndex
        ListBox1.Items.RemoveAt(Index)
        InputFileList.Items.RemoveAt(Index)
        OutputFileList.Items.RemoveAt(Index)
        RotationParameters.Items.RemoveAt(Index)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem = "" = False Then Button4.Enabled = True Else Button4.Enabled = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim processeditems As Integer = 0
        For i As Integer = 0 To ListBox1.Items.Count - 1
            CurrInputFile = InputFileList.Items.Item(i)
            CurrOutputFile = OutputFileList.Items.Item(i)
            CurrRotation = RotationParameters.Items.Item(i)
            ProcessStart("ffmpeg.exe", "-i """ & CurrInputFile & """ -c copy -metadata:s:v:0 rotate=" & CurrRotation & " """ & CurrOutputFile & """")
            processeditems = processeditems + 1
        Next
        MsgBox(processeditems & " Videos processed!")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ListBox1.Items.Clear()
        InputFileList.Items.Clear()
        OutputFileList.Items.Clear()
        RotationParameters.Items.Clear()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If IsNumeric(TextBox3.Text) Then
            If TextBox3.Text < 90 = False Then
                TextBox3.Text = TextBox3.Text - 90
            Else
                TextBox3.Text = 0
            End If
        Else : TextBox3.Text = 0
        End If

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If IsNumeric(TextBox3.Text) Then
            If TextBox3.Text > 270 = False Then
                TextBox3.Text = TextBox3.Text + 90
            Else
                TextBox3.Text = 360
            End If
        Else : TextBox3.Text = 90
        End If
    End Sub

End Class
