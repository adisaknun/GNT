Imports System.Data
Imports System.Data.SqlClient
Public Class ClassInsertDB
    Public Sub INSERTService(ByVal LabNo As String, ByVal IsPrint As Boolean)

        Dim _ChkPayment As Boolean
        _ChkPayment = ChkPayment_LabNo(LabNo)
        If _ChkPayment = False Then
            Exit Sub
        End If

        If IsPrint = True Then
            UpdateIsPrintLabNo(LabNo)
        End If
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlInsertServiceMainPending, SqlInsertServiceSubPending As String
        SqlInsertServiceMainPending = "INSERT INTO ServiceMain " & _
"(BillNo, LabNo, BookNo, NO, ServiceDate, ServiceTime, HN, SID, Payment, SubTotalPrice, SubTotalPriceNotInclude, TOTPrice, Bank, BankBranch, ChqNo, ChqDate, IsDiscount, IsPrinted, Canceled, " & _
"CanceledNote, CanceledBy, CreditAccCode, CreditCardNo, IsCheckHealth, IsValidate, Ward, ReportBy, IsPrintedReport, InformReport, FinishTime, LastApprovedTime, " & _
"ValidateTime, ReportTime, IsSendToHost, Comment, AgeSYear, AgeSMonth, AgeSDay, OTitle, OFname, OLname, OMname, PatientTypeCode, IsHIS, IsSendToHIS, IsExpress, ComputerName, IsAppend) " & _
"SELECT        BillNo, LabNo, BookNo, NO, ServiceDate, ServiceTime, HN, SID, Payment, SubTotalPrice, SubTotalPriceNotInclude, TOTPrice, Bank, BankBranch, ChqNo, ChqDate, IsDiscount, IsPrinted, Canceled, " & _
        "CanceledNote, CanceledBy, CreditAccCode, CreditCardNo, IsCheckHealth, IsValidate, Ward, ReportBy, IsPrintedReport, InformReport, FinishTime, LastApprovedTime, " & _
        "ValidateTime, ReportTime, IsSendToHost, Comment, AgeSYear, AgeSMonth, AgeSDay, OTitle, OFname, OLname, OMname, PatientTypeCode, IsHIS, IsSendToHIS, IsExpress, ComputerName, IsAppend " & _
        "FROM ServiceMainPending " & _
"WHERE        (LabNo = N'" & LabNo & "') "

        SqlInsertServiceSubPending = "INSERT INTO ServiceSub " & _
"(BillNo, LabNo, BookNo, NO, TCode, Price, PriceNotInclude, Result, ResultBy, Profile, Invalid, ShowInReceive, IsApproved, ApprovedBy, IsSendToHost, ApprovedTime, TimeUsed, " & _
"WorkingTime, HL_flag, Critical, RefValue, IsRepeat, IsSendToHIS) " & _
"SELECT        BillNo, LabNo, BookNo, NO, TCode, Price, PriceNotInclude, Result, ResultBy, Profile, Invalid, ShowInReceive, IsApproved, ApprovedBy, IsSendToHost, ApprovedTime, TimeUsed, " & _
        "WorkingTime, HL_flag, Critical, RefValue, IsRepeat, IsSendToHIS " & _
        "FROM ServiceSubPending " & _
"WHERE        (LabNo = N'" & LabNo & "')"
        Dim INSServiceMainPending As SqlCommand = New SqlCommand()
        Dim INSServiceSubPending As SqlCommand = New SqlCommand()
        INSServiceMainPending.Connection = Con
        INSServiceSubPending.Connection = Con
        INSServiceMainPending.CommandText = SqlInsertServiceMainPending
        INSServiceSubPending.CommandText = SqlInsertServiceSubPending

        Con.Open()
        INSServiceMainPending.ExecuteNonQuery()
        INSServiceSubPending.ExecuteNonQuery()
        Con.Close()
        UpdateIsSendLab(LabNo)
    End Sub

    Public Sub UpdateIsSendLab(ByVal LabNo As String)
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlUpdate As String
        SqlUpdate = "Update ServiceMainPending SET IsSendLab = 1 WHERE LabNo='" & LabNo & "'"
        Dim UpdateServiceMainPending As New SqlCommand
        UpdateServiceMainPending.Connection = Con
        UpdateServiceMainPending.CommandText = SqlUpdate
        Con.Open()
        UpdateServiceMainPending.ExecuteNonQuery()
        Con.Close()
    End Sub
    Public Sub INSERTServiceNoBookNo(ByVal LabNo As String, ByVal IsPrint As Boolean)

        If IsPrint = True Then
            UpdateIsPrintLabNo(LabNo)
        End If
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlInsertServiceMainPending, SqlInsertServiceSubPending As String
        SqlInsertServiceMainPending = "INSERT INTO ServiceMain " & _
"(BillNo, LabNo, BookNo, NO, ServiceDate, ServiceTime, HN, SID, Payment, TOTPrice, Bank, BankBranch, ChqNo, ChqDate, IsDiscount, IsPrinted, Canceled, " & _
"CanceledNote, CanceledBy, CreditAccCode, CreditCardNo, IsCheckHealth, IsValidate, Ward, ReportBy, IsPrintedReport, InformReport, FinishTime, LastApprovedTime, " & _
"ValidateTime, ReportTime, IsSendToHost, Comment, AgeSYear, AgeSMonth, AgeSDay, OTitle, OFname, OLname, OMname, PatientTypeCode, IsHIS, IsSendToHIS) " & _
"SELECT        BillNo, LabNo, BookNo, NO, ServiceDate, ServiceTime, HN, SID, Payment, TOTPrice, Bank, BankBranch, ChqNo, ChqDate, IsDiscount, IsPrinted, Canceled, " & _
        "CanceledNote, CanceledBy, CreditAccCode, CreditCardNo, IsCheckHealth, IsValidate, Ward, ReportBy, IsPrintedReport, InformReport, FinishTime, LastApprovedTime, " & _
        "ValidateTime, ReportTime, IsSendToHost, Comment, AgeSYear, AgeSMonth, AgeSDay, OTitle, OFname, OLname, OMname, PatientTypeCode, IsHIS, IsSendToHIS " & _
        "FROM ServiceMainPending " & _
"WHERE        (LabNo = N'" & LabNo & "') "

        SqlInsertServiceSubPending = "INSERT INTO ServiceSub " & _
"(BillNo, LabNo, BookNo, NO, TCode, Price, Result, ResultBy, Profile, Invalid, ShowInReceive, IsApproved, ApprovedBy, IsSendToHost, ApprovedTime, TimeUsed, " & _
"WorkingTime, HL_flag, Critical, RefValue, IsRepeat, IsSendToHIS) " & _
"SELECT        BillNo, LabNo, BookNo, NO, TCode, Price, Result, ResultBy, Profile, Invalid, ShowInReceive, IsApproved, ApprovedBy, IsSendToHost, ApprovedTime, TimeUsed, " & _
        "WorkingTime, HL_flag, Critical, RefValue, IsRepeat, IsSendToHIS " & _
        "FROM ServiceSubPending " & _
"WHERE        (LabNo = N'" & LabNo & "')"
        Dim INSServiceMainPending As SqlCommand = New SqlCommand()
        Dim INSServiceSubPending As SqlCommand = New SqlCommand()
        INSServiceMainPending.Connection = Con
        INSServiceSubPending.Connection = Con
        INSServiceMainPending.CommandText = SqlInsertServiceMainPending
        INSServiceSubPending.CommandText = SqlInsertServiceSubPending

        Con.Open()
        Try
            INSServiceMainPending.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        Try
            INSServiceSubPending.ExecuteNonQuery()
        Catch ex As Exception
        End Try

        Con.Close()

        UpdateIsSendLab(LabNo)
    End Sub
    Public Sub INSERTServiceBillNo(ByVal BillNo As String)

        Dim _ChkPayment As Boolean
        _ChkPayment = ChkPayment(BillNo)
        If _ChkPayment = False Then
            Exit Sub
        End If

        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlInsertServiceMainPending, SqlInsertServiceSubPending As String
        SqlInsertServiceMainPending = "INSERT INTO ServiceMain " & _
"(BillNo, LabNo, BookNo, NO, ServiceDate, ServiceTime, HN, SID, Payment, TOTPrice, Bank, BankBranch, ChqNo, ChqDate, IsDiscount, IsPrinted, Canceled, " & _
"CanceledNote, CanceledBy, CreditAccCode, CreditCardNo, IsCheckHealth, IsValidate, Ward, ReportBy, IsPrintedReport, InformReport, FinishTime, LastApprovedTime, " & _
"ValidateTime, ReportTime, IsSendToHost, Comment, AgeSYear, AgeSMonth, AgeSDay, OTitle, OFname, OLname, PatientTypeCode, IsHIS, IsSendToHIS) " & _
"SELECT        BillNo, LabNo, BookNo, NO, ServiceDate, ServiceTime, HN, SID, Payment, TOTPrice, Bank, BankBranch, ChqNo, ChqDate, IsDiscount, IsPrinted, Canceled, " & _
        "CanceledNote, CanceledBy, CreditAccCode, CreditCardNo, IsCheckHealth, IsValidate, Ward, ReportBy, IsPrintedReport, InformReport, FinishTime, LastApprovedTime, " & _
        "ValidateTime, ReportTime, IsSendToHost, Comment, AgeSYear, AgeSMonth, AgeSDay, OTitle, OFname, OLname, PatientTypeCode, IsHIS, IsSendToHIS " & _
        "FROM ServiceMainPending " & _
"WHERE        (BillNo = N'" & BillNo & "') "

        SqlInsertServiceSubPending = "INSERT INTO ServiceSub " & _
"(BillNo, LabNo, BookNo, NO, TCode, Price, Result, ResultBy, Profile, Invalid, ShowInReceive, IsApproved, ApprovedBy, IsSendToHost, ApprovedTime, TimeUsed, " & _
"WorkingTime, HL_flag, Critical, RefValue, IsRepeat, IsSendToHIS) " & _
"SELECT        BillNo, LabNo, BookNo, NO, TCode, Price, Result, ResultBy, Profile, Invalid, ShowInReceive, IsApproved, ApprovedBy, IsSendToHost, ApprovedTime, TimeUsed, " & _
        "WorkingTime, HL_flag, Critical, RefValue, IsRepeat, IsSendToHIS " & _
        "FROM ServiceSubPending " & _
"WHERE        (BillNo = N'" & BillNo & "')"
        Dim INSServiceMainPending As SqlCommand = New SqlCommand()
        Dim INSServiceSubPending As SqlCommand = New SqlCommand()
        INSServiceMainPending.Connection = Con
        INSServiceSubPending.Connection = Con
        INSServiceMainPending.CommandText = SqlInsertServiceMainPending
        INSServiceSubPending.CommandText = SqlInsertServiceSubPending

        Con.Open()
        INSServiceMainPending.ExecuteNonQuery()
        INSServiceSubPending.ExecuteNonQuery()
        Con.Close()

        UpdateIsSendLabBillNo(BillNo)

    End Sub
    Public Sub UpdateIsPrintLabNo(ByVal LabNo As String)
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlUpdate As String
        SqlUpdate = "Update PrintBarcodeFull SET IsPrint = 1 WHERE LabNo='" & LabNo & "'"
        Dim UpdateServiceMainPending As New SqlCommand
        UpdateServiceMainPending.Connection = Con
        UpdateServiceMainPending.CommandText = SqlUpdate
        Con.Open()
        UpdateServiceMainPending.ExecuteNonQuery()
        Con.Close()
    End Sub
    Public Sub UpdateIsSendLabBillNo(ByVal BillNo As String)
        Dim C1 As New ClassConnect
        Dim Con As New SqlConnection(C1.ConnectStr)
        Dim SqlUpdate As String
        SqlUpdate = "Update ServiceMainPending SET IsSendLab = 1 WHERE BillNo='" & BillNo & "'"
        Dim UpdateServiceMainPending As New SqlCommand
        UpdateServiceMainPending.Connection = Con
        UpdateServiceMainPending.CommandText = SqlUpdate
        Con.Open()
        UpdateServiceMainPending.ExecuteNonQuery()
        Con.Close()
    End Sub
    Public Sub InsertSendToLIS(ByVal BillNo As String, ByVal LabNo As String, ByVal Message_Control_ID As String)
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim SqlSendToLIS As String = "INSERT INTO SendToLIS " & _
                                     "(BillNo, LabNo, Message_Control_ID) " & _
                                     "VALUES (@BillNo,@LabNo, @Message_Control_ID)"
        Dim commSendToLIS As SqlCommand = New SqlCommand()
        With commSendToLIS
            .Connection = conn
            .CommandText = SqlSendToLIS
            .Parameters.AddWithValue("@BillNo", BillNo)
            .Parameters.AddWithValue("@LabNo", LabNo)
            .Parameters.AddWithValue("@Message_Control_ID", Message_Control_ID)
        End With
        Try
            conn.Open()
            commSendToLIS.ExecuteNonQuery()
        Catch ex As SqlException
            MessageBox.Show(ex.Message.ToString(), "Error Message")
        Finally
            conn.Close()
        End Try
    End Sub
    Public Sub InsertNewTest(ByVal TCode As String, ByVal Test As String)
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim SqlSendToLIS As String = "INSERT INTO Test " & _
                                     "(TCode, Test) " & _
                                     "VALUES (@TCode,@Test)"
        Dim commAddNewTest As SqlCommand = New SqlCommand()
        With commAddNewTest
            .Connection = conn
            .CommandText = SqlSendToLIS
            .Parameters.AddWithValue("@TCode", TCode)
            .Parameters.AddWithValue("@Test", Test)
        End With
        Try
            conn.Open()
            commAddNewTest.ExecuteNonQuery()
        Catch ex As SqlException
            MessageBox.Show(ex.Message.ToString(), "Error Message")
        Finally
            conn.Close()
        End Try
    End Sub
   
    Public Sub AppenServiceLab()
        Dim C1 As New ClassConnect
        Dim SqlNew As String
        Dim _Year As Integer = Now.Year
        Dim _Month As String = Format(Now.Month, "00")
        Dim _Day As String = Format(Now.Day, "00")
        SqlNew = "SELECT        TOP (1) BillNo, BookNo, No, Payment, LabNo, ServiceDate, ServiceTime, IsSendLab " & _
        "FROM ServiceMainPending " & _
        "WHERE IsSendLab = 0 AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102))" & _
        "ORDER BY ServiceTime DESC"
        Dim ds As DataSet
        ds = C1.GetDataSet(SqlNew)
        If ds.Tables(0).Rows.Count > 0 Then
            'Check ว่ามี LabNo ซ้ำหรือป่าว
            Dim drNew As DataRow
            drNew = ds.Tables(0).Rows(0)
            Dim _LabNo As String = drNew("LabNo")
            Dim _BookNo As String = drNew("BookNo")
            Dim _No As String = drNew("No")
            Dim _Payment As String = drNew("Payment")
            Dim SqlDup As String
            SqlDup = "SELECT * FROM ServiceMainPending WHERE LabNo='" & _LabNo & "' AND IsSendLab= 1 "
            Dim dsDup As DataSet
            dsDup = C1.GetDataSet(SqlDup)
            If dsDup.Tables(0).Rows.Count > 0 Then
                If _Payment = 1 Or _Payment = 2 Or _Payment = 3 Then
                    If _BookNo > 0 And _No > 0 Then
                        Dim _BillNo As String = drNew("BillNo")
                        Dim CAddService As New ClassInsertDB
                        CAddService.INSERTServiceBillNo(_BillNo)
                    End If
                Else
                    Dim _BillNo As String = drNew("BillNo")
                    Dim CAddService As New ClassInsertDB
                    CAddService.INSERTServiceBillNo(_BillNo)
                End If
            End If
        End If
    End Sub
    Public Sub AppenTestXX()
        Dim _Year As Integer = Now.Year
        Dim _Month As String = Format(Now.Month, "00")
        Dim _Day As String = Format(Now.Day, "00")
        Dim C1 As New ClassConnect
        Dim Sql As String
        Sql = "SELECT       TOP (1) ServiceMainPending.ServiceDate, ServiceSubPending.TCode, ServiceMainPending.IsSendLab, ServiceMainPending.BillNo, ServiceMainPending.Payment, ServiceMainPending.BookNo, ServiceMainPending.No " & _
            "FROM            ServiceMainPending INNER JOIN " & _
            "ServiceSubPending ON ServiceMainPending.BillNo = ServiceSubPending.BillNo " & _
            "WHERE (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102)) AND (ServiceSubPending.TCode LIKE N'XX%') AND ServiceMainPending.IsSendLab = 0"
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            Dim _BillNo As String = dr("BillNo")
            Dim _Payment As String = dr("Payment")
            'Check เงิน BookNo No ต้องไม่เท่ากับ 0
            If _Payment = 1 Or _Payment = 2 Or _Payment = 3 Then
                Dim _BookNo, _No As Integer
                If IsDBNull(dr("BookNo")) = False Then
                    _BookNo = dr("BookNo")
                End If
                If IsDBNull(dr("No")) = False Then
                    _No = dr("No")
                End If
                If _BookNo > 0 And _No > 0 Then
                    Dim CAddService As New ClassInsertDB
                    CAddService.INSERTServiceBillNo(_BillNo)
                End If
            Else
                Dim CAddService As New ClassInsertDB
                CAddService.INSERTServiceBillNo(_BillNo)
            End If
        End If
    End Sub
    Private Function ChkPayment(ByVal BillNo As String) As Boolean
        Dim C1 As New ClassConnect
        Dim Sql As String
        Sql = "SELECT * FROM ServiceMainPending WHERE BillNo='" & BillNo & "'"
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow
            dr = ds.Tables(0).Rows(0)
            Dim _Payment As Integer
            Dim _BookNo As Integer
            Dim _No As Integer
            If IsDBNull(dr("Payment")) = False Then
                _Payment = dr("Payment")
                _BookNo = dr("BookNo")
                _No = dr("No")
            Else
                _Payment = 0
                _BookNo = 0
                _No = 0
            End If
            If _Payment = 1 Or _Payment = 2 Or _Payment = 3 Then
                If _BookNo > 0 And _No > 0 Then
                    ChkPayment = True
                Else
                    ChkPayment = False
                End If
            Else
                ChkPayment = True
            End If
        End If
    End Function
    Public Function ChkPayment_LabNo(ByVal LabNo As String) As Boolean
        Dim C1 As New ClassConnect
        Dim Sql As String
        Sql = "SELECT * FROM ServiceMainPending WHERE LabNo='" & LabNo & "'"
        Dim ds As DataSet
        ds = C1.GetDataSet(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow
            dr = ds.Tables(0).Rows(0)
            Dim _Payment As Integer
            Dim _BookNo As Integer
            Dim _No As Integer
            If IsDBNull(dr("Payment")) = False Then
                _Payment = dr("Payment")
                _BookNo = dr("BookNo")
                _No = dr("No")
            Else
                _Payment = 0
                _BookNo = 0
                _No = 0
            End If
            If _Payment = 1 Or _Payment = 2 Or _Payment = 3 Then
                If _BookNo > 0 And _No > 0 Then
                    ChkPayment_LabNo = True
                Else
                    ChkPayment_LabNo = False
                End If
            Else
                ChkPayment_LabNo = True
            End If
        End If
    End Function
End Class
