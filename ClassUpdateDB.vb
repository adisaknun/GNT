Imports System.Data
Imports System.Data.SqlClient

Public Class ClassUpdateDB
    Public Sub UpdateResult(ByVal LabNo As String, ByVal TCode As String, ByVal Result As String, ByVal HL_flag As String, ByVal UserApproved As String, ByVal ApprovedTime As String)
        Try
            Dim connect As New ClassConnect
            Dim Con As New SqlConnection(connect.ConnectStr)
            Dim SqlUpdate As String
            SqlUpdate = "Update ServiceSub SET Result ='" & Result & "', HL_flag='" & HL_flag & "', ApprovedBy='" & UserApproved & "', ApprovedTime=convert(datetime, '" & ApprovedTime & "', 120), IsApproved = 0 WHERE LabNo='" & LabNo & "' AND TCode='" & TCode & "'"
            Dim UpdateServiceSub As New SqlCommand
            UpdateServiceSub.Connection = Con
            UpdateServiceSub.CommandText = SqlUpdate
            Con.Open()
            UpdateServiceSub.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub UpdateServiceMain(ByVal LabNo As String, ByVal UserValidate As String, ByVal ValidateTime As String)
        Try
            Dim connect As New ClassConnect
            Dim Con As New SqlConnection(connect.ConnectStr)
            Dim SqlUpdate As String
            SqlUpdate = "Update ServiceMain SET ReportBy ='" & UserValidate & "', ValidateTime=convert(datetime, '" & ValidateTime & "', 120), IsValidate = 'True' WHERE LabNo='" & LabNo & "'"
            Dim UpdateServiceSub As New SqlCommand
            UpdateServiceSub.Connection = Con
            UpdateServiceSub.CommandText = SqlUpdate
            Con.Open()
            UpdateServiceSub.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub UpdateServiceMain_IsSendToLIS(ByVal BillNo As String, ByVal LabNo As String)
        Try
            Dim C1 As New ClassConnect
            Dim Con As New SqlConnection(C1.ConnectStr)
            Dim SqlUpdate As String
            SqlUpdate = "Update ServiceMain SET IsSendToLIS = 1 WHERE BillNo='" & BillNo & "' AND LabNo='" & LabNo & "'"
            Dim UpdateServiceMain As New SqlCommand
            UpdateServiceMain.Connection = Con
            UpdateServiceMain.CommandText = SqlUpdate
            Con.Open()
            UpdateServiceMain.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateServiceSub_IsSendToLIS(ByVal LabNo As String, ByVal TCode As String)
        Try
            Dim C1 As New ClassConnect
            Dim Con As New SqlConnection(C1.ConnectStr)
            Dim SqlUpdate As String
            SqlUpdate = "Update ServiceSub SET IsSendToLIS = 1 WHERE LabNo='" & LabNo & "' AND TCode='" & TCode & "'"
            Dim UpdateServiceSub As New SqlCommand
            UpdateServiceSub.Connection = Con
            UpdateServiceSub.CommandText = SqlUpdate
            Con.Open()
            UpdateServiceSub.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateServiceSub_IsAcceptedSpecimen(ByVal LabNo As String, ByVal TCode As String)
        Try
            Dim C1 As New ClassConnect
            Dim Con As New SqlConnection(C1.ConnectStr)
            Dim SqlUpdate As String
            SqlUpdate = "Update ServiceSub SET IsAcceptedSpecimen = 1 WHERE LabNo='" & LabNo & "' AND TCode='" & TCode & "'"
            Dim UpdateServiceSub As New SqlCommand
            UpdateServiceSub.Connection = Con
            UpdateServiceSub.CommandText = SqlUpdate
            Con.Open()
            UpdateServiceSub.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateIsPrintFalse(ByVal LabNo As String)
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlUpdate As String
        SqlUpdate = "Update PrintBarcodeFull SET IsPrint = 0 WHERE LabNo='" & LabNo & "'"
        Dim UpdateServiceMainPending As New SqlCommand
        UpdateServiceMainPending.Connection = Con
        UpdateServiceMainPending.CommandText = SqlUpdate
        Con.Open()
        UpdateServiceMainPending.ExecuteNonQuery()
        Con.Close()
    End Sub

    Public Sub UpdateTestDate(ByVal LabNo As String, ByVal dDate As Date)
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlUpdate As String
        '#12/21/2015 8:19:01#
        'SqlUpdate = "Update ServiceMain SET ServiceDate = " & dDate & " WHERE LabNo='" & LabNo & "'"
        Dim d As String = "2016-10-21 22:44:11"
        SqlUpdate = "Update ServiceMain SET ServiceDate = convert(datetime, '" + d + "', 120) WHERE LabNo='" & LabNo & "'"

        Dim UpdateServiceMain As New SqlCommand
        UpdateServiceMain.Connection = Con
        UpdateServiceMain.CommandText = SqlUpdate
        Con.Open()
        UpdateServiceMain.ExecuteNonQuery()
        Con.Close()
    End Sub
End Class
