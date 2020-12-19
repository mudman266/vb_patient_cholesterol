Public Class frmPatientInfo
    Private Sub frmPatientInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 0 To frmMain.PATIENTS.GetUpperBound(0)
            lstPatients.Items.Add(frmMain.PATIENTS(i, 0) & " - " & frmMain.PATIENTS(i, 1))
        Next
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Close()
    End Sub
End Class