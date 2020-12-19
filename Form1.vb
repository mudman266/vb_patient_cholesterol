Public Class frmMain
    ' Class Variables
    Private READFILENAME = "c:\school\csc-139\chapter8\patient.txt"
    Private WRITEFILENAME = "c:\school\csc-139\chapter8\consult.txt"
    Public Shared PATIENTS(14, 1)
    Private CONSULTNAME() As String
    Private CONSULTSCORE() As Integer
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Show the splash screen for ~3 seconds
        Threading.Thread.Sleep(3000)

        'Prepare the reader
        Dim objReader As IO.StreamReader
        objReader = IO.File.OpenText(READFILENAME)

        'Populate the 2D array
        Dim strNextLine As String
        For i = 0 To PATIENTS.GetUpperBound(0)
            strNextLine = objReader.ReadLine
            If strNextLine <> "" Then
                PATIENTS(i, 0) = strNextLine
                PATIENTS(i, 1) = objReader.ReadLine
            End If
        Next

        'Store patients with > 200 cholesterol
        Dim j As Integer = 0
        For i = 0 To PATIENTS.GetUpperBound(0)
            If PATIENTS(i, 1) > 200 Then
                ReDim Preserve CONSULTNAME(j)
                CONSULTNAME(j) = PATIENTS(i, 0)
                ReDim Preserve CONSULTSCORE(j)
                CONSULTSCORE(j) = PATIENTS(i, 1)
                j += 1
            End If
        Next

        'Calculate the average cholesterol for the day
        Dim decAvgCholesterol As Decimal
        Dim intTotalCholesterol As Integer
        For i = 0 To PATIENTS.GetUpperBound(0)
            intTotalCholesterol += PATIENTS(i, 1)
        Next
        decAvgCholesterol = intTotalCholesterol / (PATIENTS.GetUpperBound(0) + 1)

        'Update the labels
        lblAvgCholesterol.Text = "Average Cholesterol: " & decAvgCholesterol.ToString("F0")
        lblNumPatients.Text = CONSULTNAME.Count().ToString()

        'Write the patients with > 200 cholesterol to a file
        Dim objWriter As IO.StreamWriter
        objWriter = My.Computer.FileSystem.OpenTextFileWriter(WRITEFILENAME, True)
        For i = 0 To CONSULTNAME.GetUpperBound(0)
            objWriter.WriteLine(CONSULTNAME(i) & " " & CONSULTSCORE(i))
        Next

        'Close the streamreader and steamwriter
        objWriter.Close()
        objReader.Close()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        lblAvgCholesterol.Text = "Average Cholesterol: "
        lblNumPatients.Text = "0"
    End Sub

    Private Sub DisplayPatientInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPatientInformationToolStripMenuItem.Click
        frmPatientInfo.Show()
    End Sub
End Class
