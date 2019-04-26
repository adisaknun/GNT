Imports System.Data
Public Class RePrintBarcode
    Dim C1 As New ClassConnect
    Private Sub txtLabNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLabNo.KeyDown
        If e.KeyValue = 13 Then
            If Len(txtLabNo.Text) < 13 Then
                If Len(txtLabNo.Text) <> 10 Then
                    Dim _Year As Integer = Now.Year
                    Dim _Month As String = Format(Now.Month, "00")
                    Dim _Day As String = Format(Now.Day, "00")
                    Dim _LabNo As String = Format(CInt(txtLabNo.Text), "00000")
                    txtLabNo.Text = _Year & _Month & _Day & _LabNo

                    'ถ้ามี Urine อย่างเดียว
                    Dim _haveUrine As Boolean
                    Dim CUrine As New ClassTestCheck
                    _haveUrine = CUrine.UrineCheck(txtLabNo.Text)
                    If _haveUrine = True Then
                        chkHaveSp.Checked = True
                    End If
                End If
            End If
            Dim Sql As String
            Sql = "SELECT        ServiceMain.OTitle,ServiceMain.OFname, ServiceMain.OLname, Patients.Gender, Patients.Age, ServiceMain.LabNo, TubeGNT.TubeGNTCode, PrintBarcodeFull.Ward, " & _
       "PrintBarcodeFull.IsPrint, PrintBarcodeFull.BarcodeNameCombine, PrintBarcodeFull.ServiceDate, PrintBarcodeFull.PrnBarcode, Patients.HN, Patients.Telephone " & _
       "FROM            Patients INNER JOIN " & _
                        "ServiceMain ON Patients.HN = ServiceMain.HN INNER JOIN " & _
                        "PrintBarcodeFull ON ServiceMain.LabNo = PrintBarcodeFull.LabNo INNER JOIN " & _
                        "TubeGNT ON PrintBarcodeFull.SpecimenName = TubeGNT.SpecimenName " & _
    "WHERE        (ServiceMain.LabNo = N'" & txtLabNo.Text & "') "
            Dim ds As DataSet
            ds = C1.GetDataSet(Sql)
            If ds.Tables(0).Rows.Count <> 0 Then
                Dim dr As DataRow
                dr = ds.Tables(0).Rows(0)
                lblBarcode.Text = dr("PrnBarcode")
                Dim _Title, _Fname, _Lname As String
                If IsDBNull(dr("OTitle")) = False Then
                    _Title = dr("OTitle")
                End If
                If IsDBNull(dr("OFname")) = False Then
                    _Fname = dr("OFname")
                End If
                If IsDBNull(dr("OLname")) = False Then
                    _Lname = dr("OLname")
                End If
                lblFullName.Text = _Title & " " & _Fname & " " & _Lname
                Dim _Sex As String
                If IsDBNull(dr("Gender")) = False Then
                    If dr("Gender") = 1 Then
                        _Sex = "ชาย"
                    End If
                    If dr("Gender") = 2 Then
                        _Sex = "หญิง"
                    End If
                End If
                lblGender.Text = _Sex
                If IsDBNull(dr("Age")) = False Then
                    lblAge.Text = dr("Age")
                End If
                If IsDBNull(dr("Telephone")) = False Then
                    lblTelephone.Text = dr("Telephone")
                End If
            End If


            TestDataBind()
            CountTubeBarcode(txtLabNo.Text)
        End If
    End Sub
    Private Sub CountTubeBarcode(ByVal LabNo As String)
        If LabNo = "" Then
            Exit Sub
        End If
        Dim _Red As Integer = 0
        Dim _Purple As Integer = 0
        Dim _Green As Integer = 0
        Dim _Bule As Integer = 0
        Dim _ExBarcode As Integer = 0
        Dim Sql As String
        Sql = "SELECT        ServiceMainPending.OFname, ServiceMainPending.OLname, Patients.Gender, Patients.Age, ServiceMainPending.LabNo, TubeGNT.TubeGNTCode, PrintBarcodeFull.Ward, " & _
        "PrintBarcodeFull.IsPrint, PrintBarcodeFull.BarcodeNameCombine, PrintBarcodeFull.ServiceDate, PrintBarcodeFull.PrnBarcode, Patients.HN " & _
        "FROM            Patients INNER JOIN " & _
                         "ServiceMainPending ON Patients.HN = ServiceMainPending.HN INNER JOIN " & _
                         "PrintBarcodeFull ON ServiceMainPending.LabNo = PrintBarcodeFull.LabNo INNER JOIN " & _
                         "TubeGNT ON PrintBarcodeFull.SpecimenName = TubeGNT.SpecimenName " & _
"WHERE        (ServiceMainPending.LabNo = N'" & LabNo & "') ORDER BY PrintBarcodeFull.PrnBarcode"
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        If ds.Tables(0).Rows.Count <> 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow
                dr = ds.Tables(0).Rows(i)
                If dr("TubeGNTCode") = "2" Then
                    _Red = _Red + 1
                End If
                If dr("TubeGNTCode") = "1" Then
                    _Green = _Green + 1
                End If
                If dr("TubeGNTCode") = "0" Then
                    _Purple = _Purple + 1
                End If
                If dr("TubeGNTCode") = "3" Then
                    _Bule = _Bule + 1
                End If
                If dr("TubeGNTCode") = "T" Then
                    _ExBarcode = _ExBarcode + 1
                End If
            Next

            If _Red = 0 Then
                lblTubeRed.Visible = False
            Else
                lblTubeRed.Visible = True
                lblTubeRed.Text = _Red
            End If

            If _Bule = 0 Then
                lblTubeBule.Visible = False
            Else
                lblTubeBule.Visible = True
                lblTubeBule.Text = _Bule
            End If

            If _Green = 0 Then
                lblTubeGreen.Visible = False
            Else
                lblTubeGreen.Visible = True
                lblTubeGreen.Text = _Green
            End If

            If _Purple = 0 Then
                lblTubePurple.Visible = False
            Else
                lblTubePurple.Visible = True
                lblTubePurple.Text = _Purple
            End If

            If _ExBarcode = 0 Then
                lblExtraBarcode.Visible = False
            Else
                lblExtraBarcode.Visible = True
                lblExtraBarcode.Text = _ExBarcode
            End If

            If chkHaveSp.Checked = True Then
                lblTubeRed.Visible = False
                lblTubeBule.Visible = False
                lblTubeGreen.Visible = False
                lblTubePurple.Visible = False
                lblExtraBarcode.Visible = True
                lblExtraBarcode.Text = _Red + _Bule + _Green + _Purple + _ExBarcode
            End If

        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim _MsgGNT As String
        Dim C_GenMsg As New MessageGNT

        If chkHaveSp.Checked = True Then
            '_MsgGNT = C_GenMsg.ReGenMessage(txtLabNo.Text, "T", True)
            'ออก Printer เดิม
            Dim CUpdateisPrint As New ClassUpdateDB
            CUpdateisPrint.UpdateIsPrintFalse(txtLabNo.Text)
            _MsgGNT = ""
        Else
            _MsgGNT = C_GenMsg.ReGenMessage(txtLabNo.Text, "G", False)
        End If
        'Try
        'ส่ง Message เข้าเครือง GNT
        If _MsgGNT <> "" Then
            Dim gnt As New GNTCOMMWEB.GntActiveFormX
            Me.Text = gnt.WAITDATA(0, "localhost")
            lblAcceptNo.Text = gnt.TRANSDATA(0, "localhost", _MsgGNT)
            'Catch ex As Exception
            'End Try
            If IsNumeric(lblAcceptNo.Text) = True Then
            Else
                MsgBox("พบปัญหาบางประการไม่สามารถ Print ได้")
            End If
        End If

    End Sub
    Private Sub TestDataBind()
        'แสดง Test ใน List
        Dim SqlTest As String
        SqlTest = "SELECT        ServiceSubPending.LabNo, Test.TCode, Test.ShowInReceive, Test.PrintIndex, Test.Test " & _
"FROM            ServiceSubPending INNER JOIN " & _
                         "Test ON ServiceSubPending.TCode = Test.TCode " & _
"WHERE        (ServiceSubPending.LabNo = N'" & txtLabNo.Text & "') AND (Test.ShowInReceive = 1) " & _
"ORDER BY Test.PrintIndex"
        Dim dsTest As DataSet
        dsTest = C1.GetDataSet(SqlTest)
        Dim L1 As New List(Of String)
        ListView1.Items.Clear()
        ListView2.Items.Clear()
        ListView3.Items.Clear()
        If dsTest.Tables(0).Rows.Count <> 0 Then
            For _row As Integer = 0 To dsTest.Tables(0).Rows.Count - 1
                Dim dr As DataRow
                dr = dsTest.Tables(0).Rows(_row)
                If _row <= 7 Then
                    ListView1.View = View.Details
                    ListView1.GridLines = True
                    Dim Lst As ListViewItem
                    Dim ar(2) As String
                    ar(0) = dr("Tcode")
                    ar(1) = dr("Test")
                    Lst = New ListViewItem(ar)
                    ListView1.Items.Add(Lst)
                ElseIf _row >= 8 And _row <= 15 Then
                    ListView2.View = View.Details
                    ListView2.GridLines = True
                    Dim Lst As ListViewItem
                    Dim ar(2) As String
                    ar(0) = dr("Tcode")
                    ar(1) = dr("Test")
                    Lst = New ListViewItem(ar)
                    ListView2.Items.Add(Lst)
                Else
                    ListView3.View = View.Details
                    ListView3.GridLines = True
                    Dim Lst As ListViewItem
                    Dim ar(2) As String
                    ar(0) = dr("Tcode")
                    ar(1) = dr("Test")
                    Lst = New ListViewItem(ar)
                    ListView3.Items.Add(Lst)
                End If
            Next
        End If
    End Sub

    Private Sub RePrintBarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub chkHaveSp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHaveSp.CheckedChanged
        Dim LabNo As String
        LabNo = txtLabNo.Text
        CountTubeBarcode(LabNo)
    End Sub

    Private Sub txtLabNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLabNo.TextChanged

    End Sub
End Class