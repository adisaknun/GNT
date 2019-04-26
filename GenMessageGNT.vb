Imports System.Data
Public Class MessageGNT
    Dim C1 As New ClassConnect
    Public Function GenMessage(ByVal LabNo As String, ByVal TypePatient As String, ByVal IsHaveSp As Boolean) As String
        Dim Sql As String
        Sql = "SELECT        ServiceMainPending.OFname, ServiceMainPending.OLname, Patients.Gender, Patients.Age, ServiceMainPending.LabNo, TubeGNT.TubeGNTCode, PrintBarcodeFull.Ward, " & _
        "PrintBarcodeFull.IsPrint, PrintBarcodeFull.BarcodeNameCombine, PrintBarcodeFull.ServiceDate, PrintBarcodeFull.PrnBarcode,PrintBarcodeFull.SpecimenName, Patients.HN " & _
        "FROM            Patients INNER JOIN " & _
                         "ServiceMainPending ON Patients.HN = ServiceMainPending.HN INNER JOIN " & _
                         "PrintBarcodeFull ON ServiceMainPending.LabNo = PrintBarcodeFull.LabNo INNER JOIN " & _
                         "TubeGNT ON PrintBarcodeFull.SpecimenName = TubeGNT.SpecimenName " & _
"WHERE        (ServiceMainPending.LabNo = N'" & LabNo & "') ORDER BY PrintBarcodeFull.PrnBarcode"

        Dim Msg As String
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        Dim _P, _S As String
        If ds.Tables(0).Rows.Count <> 0 Then
            Dim dr As DataRow
            dr = ds.Tables(0).Rows(0)
            Dim FullName, PS As String

            If IsDBNull("OFname") = False Then
                FullName = dr("OFname")
            End If
            If IsDBNull("OLname") = False Then
                FullName = FullName & " " & dr("OLname")
            End If

            If IsDBNull(dr("Gender")) = False Then
                If dr("Gender") = 1 Then
                    PS = "^PS" & "M"
                ElseIf dr("Gender") = 2 Then
                    PS = "^PS" & "F"
                Else
                    PS = ""
                End If
            End If

            _P = "^^^P" & "^PI" & dr("HN") & "^PN" & FullName & PS & "^PA" & dr("Age") & "^PBA" & "^SW" & TypePatient & "^^^_P"
            _S = "^^^S"
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                dr = ds.Tables(0).Rows(i)
                _S = _S & "^SS"
                _S = _S & "^S1" & dr("PrnBarcode")
                If IsHaveSp = False Then
                    _S = _S & "^S2" & dr("TubeGNTCode")
                ElseIf IsHaveSp = True Then
                    _S = _S & "^S2T"
                End If
                _S = _S & "^S31"
                Dim BCombind As String
                BCombind = dr("BarcodeNameCombine")
                If Trim(BCombind) = "HI" Then
                    If dr("SpecimenName") = "Urine" Then
                        _S = _S & "^S42"
                    End If
                    '_S = _S & "^S4" & PrintCount(LabNo)
                End If
                _S = _S & "^SB" & dr("ServiceDate")
                _S = _S & "^SC" & dr("BarcodeNameCombine")
                _S = _S & "^SD" & dr("Ward")
                _S = _S & "^_SS"
            Next
            _S = _S & "^^^_S"

            GenMessage = _P & _S
        End If
    End Function
    Public Function ReGenMessage(ByVal LabNo As String, ByVal TypePatient As String, ByVal IsHaveSp As Boolean) As String
        Dim Sql As String
        Sql = "SELECT        ServiceMain.OFname, ServiceMain.OLname, Patients.Gender, Patients.Age, ServiceMain.LabNo, TubeGNT.TubeGNTCode, PrintBarcodeFull.Ward, " & _
        "PrintBarcodeFull.IsPrint, PrintBarcodeFull.BarcodeNameCombine, PrintBarcodeFull.ServiceDate, PrintBarcodeFull.PrnBarcode,PrintBarcodeFull.SpecimenName, Patients.HN " & _
        "FROM            Patients INNER JOIN " & _
                         "ServiceMain ON Patients.HN = ServiceMain.HN INNER JOIN " & _
                         "PrintBarcodeFull ON ServiceMain.LabNo = PrintBarcodeFull.LabNo INNER JOIN " & _
                         "TubeGNT ON PrintBarcodeFull.SpecimenName = TubeGNT.SpecimenName " & _
"WHERE        (ServiceMain.LabNo = N'" & LabNo & "') "
        Dim Msg As String
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        Dim _P, _S As String
        If ds.Tables(0).Rows.Count <> 0 Then
            Dim dr As DataRow
            dr = ds.Tables(0).Rows(0)
            Dim FullName, PS As String

            If IsDBNull("OFname") = False Then
                FullName = dr("OFname")
            End If
            If IsDBNull("OLname") = False Then
                FullName = FullName & " " & dr("OLname")
            End If

            If IsDBNull(dr("Gender")) = False Then
                If dr("Gender") = 1 Then
                    PS = "^PS" & "M"
                ElseIf dr("Gender") = 2 Then
                    PS = "^PS" & "F"
                Else
                    PS = ""
                End If
            End If

            _P = "^^^P" & "^PI" & dr("HN") & "^PN" & FullName & PS & "^PA" & dr("Age") & "^PBA" & "^SW" & TypePatient & "^^^_P"
            _S = "^^^S"
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                dr = ds.Tables(0).Rows(i)
                _S = _S & "^SS"
                _S = _S & "^S1" & dr("PrnBarcode")
                If IsHaveSp = False Then
                    _S = _S & "^S2" & dr("TubeGNTCode")
                ElseIf IsHaveSp = True Then
                    _S = _S & "^S2T"
                End If
                _S = _S & "^S31"
                Dim BCombind As String
                BCombind = dr("BarcodeNameCombine")
                If Trim(BCombind) = "HI" Then
                    If dr("SpecimenName") = "Urine" Then
                        _S = _S & "^S42"
                    End If
                    '_S = _S & "^S4" & PrintCount(LabNo)
                End If
                _S = _S & "^SB" & dr("ServiceDate")
                _S = _S & "^SC" & dr("BarcodeNameCombine")
                _S = _S & "^SD" & dr("Ward")
                _S = _S & "^_SS"
            Next
            _S = _S & "^^^_S"

            ReGenMessage = _P & _S
        End If
    End Function
    Private Function PrintCount(ByVal LabNo As String) As Integer
        Dim Sql As String
        ' Sql = "SELECT Tcode FROM ServiceSubPending WHERE LabNo='" & LabNo & "' AND  (Tcode='MAU' OR Tcode='UCRE')"
        Sql = "SELECT        Test.TCode, Test.SubDepartment, Test.Department " & _
              "FROM Test INNER JOIN " & _
              "ServiceSubPending ON Test.TCode = ServiceSubPending.TCode " & _
              "WHERE  Test.SubDepartment = N'UCHEM' AND ServiceSubPending.LabNo='" & LabNo & "'  "
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            PrintCount = 2
        Else
            PrintCount = 1
        End If
    End Function
End Class
