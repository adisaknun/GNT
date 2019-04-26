Imports System.Data
Imports System.Data.SqlClient
Public Class ClassDeleteDB
    Public Sub DeleteServicePending(ByVal LabNo As String)
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlDeleteServiceMainPending, SqlDeleteServiceSubPending As String
        SqlDeleteServiceMainPending = "DELETE FROM ServiceMainPending WHERE LabNo='" & LabNo & "'"
        SqlDeleteServiceSubPending = "DELETE FROM ServiceSubPending WHERE LabNo='" & LabNo & "'"
        Dim DeleteServiceMainPending As SqlCommand = New SqlCommand()
        Dim DeleteServiceSubPending As SqlCommand = New SqlCommand()
        DeleteServiceMainPending.Connection = Con
        DeleteServiceSubPending.Connection = Con
        DeleteServiceMainPending.CommandText = SqlDeleteServiceMainPending
        DeleteServiceSubPending.CommandText = SqlDeleteServiceSubPending

        Con.Open()
        DeleteServiceMainPending.ExecuteNonQuery()
        DeleteServiceSubPending.ExecuteNonQuery()
        Con.Close()
    End Sub
    Public Sub DeletePendingOutDate()
        Dim BackDate As Date = DateAdd(DateInterval.Day, -7, Now)
        Dim _Year As Integer = BackDate.Year
        Dim _Month As String = Format(BackDate.Month, "00")
        Dim _Day As String = Format(BackDate.Day, "00")
        Dim Sql As String
        Sql = "SELECT        ServiceDate, BillNo, LabNo " & _
        "FROM ServiceMainPending " & _
        "WHERE   (ServiceDate <= CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102))"
        Dim C1 As New ClassConnect
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        If ds.Tables(0).Rows.Count <> 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow
                dr = ds.Tables(0).Rows(i)
                Dim _LabNo As String
                _LabNo = dr("LabNo")
                DeleteServicePending(_LabNo)
            Next
        End If
    End Sub
End Class
