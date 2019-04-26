Imports System.Data
Public Class ClassTestCheck
    Public Function UrineCheck(ByVal LabNo As String) As Boolean
        Dim C1 As New ClassConnect
        Dim Sql As String
        Sql = "SELECT     Distinct Test.Department, ServiceSubPending.LabNo " & _
              "FROM            ServiceSubPending INNER JOIN " & _
              "Test ON ServiceSubPending.TCode = Test.TCode " & _
              "WHERE     ServiceSubPending.LabNo = '" & LabNo & "'"
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        If ds.Tables(0).Rows.Count = 1 Then
            Dim dr As DataRow
            dr = ds.Tables(0).Rows(0)
            If dr("Department") = "U" Or dr("Department") = "CH5" Then
                UrineCheck = True
            Else
                UrineCheck = False
            End If
        End If
    End Function

End Class
