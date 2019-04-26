Imports System.Net
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Net.Sockets
Imports System.Threading
Imports System.Globalization


Public Class MainPrintBarcode
    Private WithEvents listener As New ClassListener

#Region "form"

    Dim C1 As New ClassConnect
    Dim _time10 As Integer
    Dim GV_PendingG_Count As Integer
    Dim GV_PendingG_Cindex As Integer
    Dim GV_PendingG_Rindex As Integer
    Dim GV_PendingS_Cindex As Integer
    Dim GV_PendingS_Rindex As Integer
    Dim IsOnlyUrine As Boolean

    Dim clientSocket As Object
    Public Shared serverStream As NetworkStream
    Dim readData As String
    Dim infiniteCounter As Integer
    Dim ctThread As Threading.Thread
    Dim mychat As New ClassListener
    Public watchfolder As FileSystemWatcher

    Private Sub MainPrintBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim CDelOut As New ClassDeleteDB
        CDelOut.DeletePendingOutDate()
        Log_IN.Hide()
        GV_PendingG_Bind()
        GV_PendingS_Bind()
        Timer1.Interval = 1000
        Timer1.Start()

        '=======================
        'Dim ipHostInfo As IPHostEntry = Dns.Resolve(Dns.GetHostName())
        'Dim IP As String = ipHostInfo.AddressList(0).ToString


        'listener.connect(IP, 11000) 'for result (ORU message)
        'LIS_Login()
        'MonitorFileAST()
        '========================


        'Listening1()

        'listener.disconnect()
        'listener.connect(IP, 15000) 'for order (ORM message)
    End Sub
    'Sub LIS_Login()
    '    'Dim UNCPath As String = "\\10.51.101.6\ORU-F-SII"
    '    Dim UNCPath As String = "\\10.51.101.6\ORU-F-SII\Output\SII"

    '    Dim dirs As String()
    '    ' Process the list of .txt files found in the directory. '
    '    Dim fileName As String

    '    Try
    '        Using unc As New UNCAccessWithCredentials()
    '            If unc.NetUseWithCredentials(UNCPath, "HIS", "TDNL.local", "MedTech@1") Then
    '                dirs = Directory.GetDirectories(UNCPath)
    '                'For Each d As String In dirs
    '                '    tbDirList.Text += d & vbCr & vbLf
    '                'Next
    '                'Dim ASTfiles As String() = Directory.GetFiles("\\10.51.101.6\ORU-F-SII\Output\SII", "*.ORU")
    '                Dim ASTfiles As String() = Directory.GetFiles("\\10.51.101.6\ORU-F-SII\Output\SII", "*.ast")
    '                For Each fileName In ASTfiles
    '                    If (System.IO.File.Exists(fileName)) Then
    '                        TextBox1.Text += fileName & vbCr & vbLf
    '                        'Read File and Print Result if its true
    '                        Read_AST_file(fileName)
    '                    End If
    '                Next

    '            Else
    '                'Me.Cursor = Cursors.[Default]
    '                MessageBox.Show("Failed to connect to " + UNCPath & vbCr & vbLf & "LastError = " & unc.LastError.ToString(), "Failed to connect", MessageBoxButtons.OK, MessageBoxIcon.[Error])

    '            End If
    '        End Using
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub
    Sub MonitorFileAST()
        watchfolder = New System.IO.FileSystemWatcher()

        'this is the path we want to monitor
        'watchfolder.Path = "\\10.51.101.6\ORU-F-SII\Output\SII"
        watchfolder.Path = "\\10.4.101.102\ORU-F-SII\Output\SII"
        'Add a list of Filter we want to specify
        'make sure you use OR for each Filter as we need to
        'all of those 

        watchfolder.NotifyFilter = IO.NotifyFilters.DirectoryName
        watchfolder.NotifyFilter = watchfolder.NotifyFilter Or _
                                   IO.NotifyFilters.FileName
        watchfolder.NotifyFilter = watchfolder.NotifyFilter Or _
                                   IO.NotifyFilters.Attributes

        ' add the handler to each event
        'AddHandler watchfolder.Changed, AddressOf logchange
        AddHandler watchfolder.Created, AddressOf logchange
        'AddHandler watchfolder.Deleted, AddressOf logchange

        ' add the rename handler as the signature is different
        'AddHandler watchfolder.Renamed, AddressOf logrename

        'Set this property to true to start watching
        watchfolder.EnableRaisingEvents = True
    End Sub

    Sub Read_AST_file(ByVal filename As String)
        Dim file As New ClassLogFile
        'Dim strResult As String = file.GetFileContents(filename)
        Dim FullAccessNumber, UserApproved As String

        Dim objReader As StreamReader
        Dim LineOfText As String
        Dim arrayTextFile() As String
        'Dim txt_path As String

        objReader = New StreamReader(filename)
        While objReader.EndOfStream = False
            LineOfText = objReader.ReadLine
            arrayTextFile = LineOfText.Split("|")
            Select Case arrayTextFile(0)
                Case "MSH"
                    Dim MessageType As String = arrayTextFile(8)
                    Dim MessageControlID As String = arrayTextFile(9)
                Case "PID"
                Case "NTE"
                Case "PV1"
                Case "ORC"
                    Dim PlacerGroupNumber As String = arrayTextFile(3)
                    'FullAccessNumber = PlacerGroupNumber
                    FullAccessNumber = arrayTextFile(4)
                Case "OBR"
                    UserApproved = Mid(arrayTextFile(34), InStr(arrayTextFile(34), "&") + 1, Len(arrayTextFile(34)) - InStr(arrayTextFile(34), "&") + 1)
                Case "OBX"
                    Dim TCode As String = arrayTextFile(3)
                    TCode = TCode.Substring(0, InStr(TCode, "^") - 1)
                    Dim Result As String = arrayTextFile(5)
                    Dim Flag As String = arrayTextFile(8)
                    Dim strApprovedTime As String = arrayTextFile(19)
                    Dim provider As CultureInfo = CultureInfo.InvariantCulture
                    Dim ApprovedTime As Date = Date.Now
                    '= Date.ParseExact(strApprovedTime, "yyyyMMddhhmmss", provider)
                    If arrayTextFile(11) = "F" Then 'Status of result is finish("F").
                        SaveResult(FullAccessNumber, TCode, Result, Flag, UserApproved, ApprovedTime)
                    End If
            End Select
        End While
        'Delete file
        If System.IO.File.Exists(filename) = True Then
            objReader.Close()
            System.IO.File.Delete(filename)
        End If
    End Sub
    Private Sub SaveResult(ByVal LabNo As String, ByVal TCode As String, ByVal Result As String, ByVal HL_flag As String, ByVal UserApproved As String, ByVal ApprovedTime As Date)
        Dim UpdateDB As New ClassUpdateDB
        UpdateDB.UpdateResult(LabNo, TCode, Result, HL_flag, UserApproved, ApprovedTime)
    End Sub
    Private Sub logchange(ByVal source As Object, ByVal e As  _
                        System.IO.FileSystemEventArgs)
        'If e.ChangeType = IO.WatcherChangeTypes.Changed Then
        '    txt_folderactivity.Text &= "File " & e.FullPath & _
        '                            " has been modified" & vbCrLf
        'End If
        If e.ChangeType = IO.WatcherChangeTypes.Created Then
            MsgBox("File " & e.FullPath & " has been created")
        End If
        If e.ChangeType = IO.WatcherChangeTypes.Deleted Then
            MsgBox("File " & e.FullPath & " has been deleted")
        End If
    End Sub
   

    'Sub Listening1()
    '    Dim server As TcpListener
    '    server = Nothing
    '    Try
    '        ' Set the TcpListener on port 13000.
    '        Dim port As Int32 = 11000
    '        Dim ipHostInfo As IPHostEntry = Dns.Resolve(Dns.GetHostName())
    '        Dim IP As String = ipHostInfo.AddressList(0).ToString
    '        Dim localAddr As IPAddress = IPAddress.Parse(IP)

    '        server = New TcpListener(localAddr, port)

    '        ' Start listening for client requests.
    '        server.Start()

    '        ' Buffer for reading data
    '        Dim bytes(1024) As Byte
    '        Dim data As String = Nothing

    '        ' Enter the listening loop.
    '        While True
    '            Console.Write("Waiting for a connection... ")

    '            ' Perform a blocking call to accept requests.
    '            ' You could also user server.AcceptSocket() here.
    '            Dim client As TcpClient = server.AcceptTcpClient()
    '            'Console.WriteLine("Connected!")
    '            'MsgBox("server Connected!")
    '            data = Nothing

    '            ' Get a stream object for reading and writing
    '            Dim stream As NetworkStream = client.GetStream()

    '            Dim i As Int32

    '            ' Loop to receive all the data sent by the client.
    '            i = stream.Read(bytes, 0, bytes.Length)
    '            While (i <> 0)
    '                ' Translate data bytes to a ASCII string.
    '                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i)
    '                'Console.WriteLine("Received: {0}", data)

    '                ' Process the data sent by the client.
    '                data = data.ToUpper()
    '                Dim msg As Byte() = System.Text.Encoding.ASCII.GetBytes(data)

    '                ' Send back a response.
    '                stream.Write(msg, 0, msg.Length)
    '                'Console.WriteLine("Sent: {0}", data)
    '                TextBox1.Text = "Sent: " & data
    '                i = stream.Read(bytes, 0, bytes.Length)

    '            End While

    '            ' Shutdown and end connection
    '            client.Close()
    '        End While
    '    Catch e As SocketException
    '        Console.WriteLine("SocketException: {0}", e)
    '    Finally
    '        server.Stop()
    '    End Try

    '    Console.WriteLine(ControlChars.Cr + "Hit enter to continue....")
    '    Console.Read()
    'End Sub

    Private Sub GV_PendingG_Bind()
        Dim _Year As Integer = Now.Year
        Dim _Month As String = Format(Now.Month, "00")
        Dim _Day As String = Format(Now.Day, "00")
        Dim dt As DataTable
        Dim Sql As String
        Sql = "SELECT   Distinct   RIGHT(ServiceMainPending.LabNo, 4) AS NoLab, ServiceMainPending.LabNo, ServiceMainPending.OTitle, ServiceMainPending.OFname, ServiceMainPending.Canceled, ServiceMainPending.OLname, Ward.Ward, Ward.WardCode, " & _
                         "ServiceMainPending.LabNo, ServiceMainPending.BillNo, Patients.HN, Patients.BirthDate, Patients.Age, Patients.Gender, Patients.Telephone " & _
"FROM            ServiceMainPending LEFT OUTER JOIN " & _
                         "Patients ON ServiceMainPending.HN = Patients.HN LEFT OUTER JOIN " & _
                         "Ward ON ServiceMainPending.Ward = Ward.WardCode " & _
        "WHERE Ward.IsHaveSpecimen = 0 AND ServiceMainPending.IsSendLab = 0 AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102)) " & _
"ORDER BY ServiceMainPending.LabNo "

        dt = C1.GetDatatable(Sql)
        GV_PendingG.DataSource = dt
        GV_PendingG_Count = dt.Rows.Count
        If GV_PendingG_Count <> CInt(lblG.Text) Then
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
        End If
        lblG.Text = dt.Rows.Count
        'สี
        'GV_PendingG.Rows(0).DefaultCellStyle.BackColor = Color.Blue
        IsOnlyUrine = False
        If dt.Rows.Count <> 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim NoLab As String = ""
                NoLab = dt.Rows(i).Item("NoLab")
                Dim LabNo As String = ""
                LabNo = dt.Rows(i).Item("LabNo")
                Dim Ward As String
                Ward = dt.Rows(i).Item("WardCode")
                'มีสิ่งส่งตรวจ สีแดง
                Dim Chk_S As String
                Chk_S = Ward.Substring(Ward.Length - 1)
                If Chk_S = "S" Then
                    GV_PendingG.Rows(i).DefaultCellStyle.BackColor = Color.Red
                End If
                'มี Urine สีเหลือง
                Dim _haveUrine As Boolean
                Dim CUrine As New ClassTestCheck
                _haveUrine = CUrine.UrineCheck(LabNo)
                If _haveUrine = True Then
                    GV_PendingG.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
                End If
                'ส่งเข้า Main Canceled = 1 Main
                Dim Canceled As Boolean
                Canceled = dt.Rows(i).Item("Canceled")
                If Canceled = True Then
                    Dim CInrt As New ClassInsertDB
                    CInrt.INSERTServiceNoBookNo(LabNo, True)
                End If
            Next
        End If

        Try
            GV_PendingG.Item(GV_PendingG_Cindex, GV_PendingG_Rindex).Selected = True
        Catch ex As Exception
        End Try
        'Check LabNo ซ้ำ และเพิ่มลง ServiceMain ServiceSub Auto
        Dim CAdd As New ClassInsertDB
        CAdd.AppenServiceLab()
        CAdd.AppenTestXX()
        '******************
    End Sub
    Private Sub GV_PendingS_Bind()
        Dim _Year As Integer = Now.Year
        Dim _Month As String = Format(Now.Month, "00")
        Dim _Day As String = Format(Now.Day, "00")
        Dim dt As DataTable
        Dim Sql As String
        Sql = "SELECT   Distinct   RIGHT(ServiceMainPending.LabNo, 4) AS NoLab, ServiceMainPending.LabNo, ServiceMainPending.OTitle, ServiceMainPending.OFname, ServiceMainPending.OLname, Ward.Ward, Ward.WardCode, " & _
                         "ServiceMainPending.LabNo, ServiceMainPending.BillNo, Patients.HN, Patients.BirthDate, Patients.Age, Patients.Gender, Patients.Telephone " & _
"FROM            ServiceMainPending LEFT OUTER JOIN " & _
                         "Patients ON ServiceMainPending.HN = Patients.HN LEFT OUTER JOIN " & _
                         "Ward ON ServiceMainPending.Ward = Ward.WardCode " & _
        "WHERE Ward.IsHaveSpecimen = 1 AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102)) AND ServiceMainPending.Canceled=0" & _
"ORDER BY ServiceMainPending.LabNo "
        dt = C1.GetDatatable(Sql)
        GV_PendingS.DataSource = dt
        lblS.Text = dt.Rows.Count
        Try
            GV_PendingS.Item(GV_PendingS_Cindex, GV_PendingS_Rindex).Selected = True
        Catch ex As Exception
        End Try
        Dim LabNo As String
        If dt.Rows.Count > 0 Then
            Dim SqlIn As String
            '            SqlIn = "SELECT   Distinct   RIGHT(ServiceMainPending.LabNo, 4) AS NoLab, ServiceMainPending.LabNo, ServiceMainPending.OTitle, ServiceMainPending.OFname, ServiceMainPending.OLname, Ward.Ward, Ward.WardCode, ServiceMainPending.Payment, ServiceMainPending.BookNo, ServiceMainPending.No, " & _
            '                        "ServiceMainPending.LabNo, Patients.HN, Patients.BirthDate, Patients.Age, Patients.Gender, Patients.Telephone " & _
            '"FROM            ServiceMainPending LEFT OUTER JOIN " & _
            '                        "Patients ON ServiceMainPending.HN = Patients.HN LEFT OUTER JOIN " & _
            '                        "Ward ON ServiceMainPending.Ward = Ward.WardCode " & _
            '       "WHERE Ward.IsHaveSpecimen = 1 AND ServiceMainPending.IsSendLab = 0 AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102)) AND ServiceMainPending.Canceled=0" & _
            '"ORDER BY ServiceMainPending.LabNo "
            SqlIn = "SELECT   Distinct   RIGHT(ServiceMainPending.LabNo, 4) AS NoLab, ServiceMainPending.LabNo, ServiceMainPending.OTitle, ServiceMainPending.OFname, ServiceMainPending.OLname, Ward.Ward, Ward.WardCode, ServiceMainPending.Payment, ServiceMainPending.BookNo, ServiceMainPending.No, " & _
                        "ServiceMainPending.LabNo, Patients.HN, Patients.BirthDate, Patients.Age, Patients.Gender, Patients.Telephone " & _
"FROM            ServiceMainPending LEFT OUTER JOIN " & _
                        "Patients ON ServiceMainPending.HN = Patients.HN LEFT OUTER JOIN " & _
                        "Ward ON ServiceMainPending.Ward = Ward.WardCode " & _
       "WHERE Ward.IsAutoPrintBarcode = 1 AND ServiceMainPending.IsSendLab = 0 AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102)) AND ServiceMainPending.Canceled=0" & _
"ORDER BY ServiceMainPending.LabNo "
            Dim dtIN As DataTable
            dtIN = C1.GetDatatable(SqlIn)
            If dtIN.Rows.Count > 0 Then
                Dim _BookNo As String = dtIN.Rows(0).Item("BookNo")
                Dim _No As String = dtIN.Rows(0).Item("No")
                Dim _Payment As String = dtIN.Rows(0).Item("Payment")

                If _Payment = 1 Or _Payment = 2 Or _Payment = 3 Then
                    If _BookNo > 0 And _No > 0 Then
                        LabNo = dtIN.Rows(0).Item("LabNo")
                        Dim INSert1 As New ClassInsertDB
                        INSert1.INSERTService(LabNo, False)
                    End If
                Else
                    LabNo = dtIN.Rows(0).Item("LabNo")
                    Dim INSert1 As New ClassInsertDB
                    INSert1.INSERTService(LabNo, False)
                End If

            End If
        End If
    End Sub

    Private Sub GV_PendingG_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles GV_PendingG.CellMouseDown
        'Try
        lblMobile.Text = "--"
        GV_PendingG_Cindex = e.ColumnIndex
        GV_PendingG_Rindex = e.RowIndex
        'lblHaveSp.Visible = False
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        lblBillNo.Text = GV_PendingG.Item("BillNo", e.RowIndex).Value
        lblNo.Text = GV_PendingG.Item("NoLab", e.RowIndex).Value
        Dim labNo As String = GV_PendingG.Item("LabNo", e.RowIndex).Value
        lblLabNo.Text = labNo

        Dim _Title, _Fname, _Lname As String
        If IsDBNull(GV_PendingG.Item("OTitle", e.RowIndex).Value) = False Then
            _Title = GV_PendingG.Item("OTitle", e.RowIndex).Value
        End If
        If IsDBNull(GV_PendingG.Item("OFname", e.RowIndex).Value) = False Then
            _Fname = GV_PendingG.Item("OFname", e.RowIndex).Value
        End If
        If IsDBNull(GV_PendingG.Item("OLname", e.RowIndex).Value) = False Then
            _Lname = GV_PendingG.Item("OLname", e.RowIndex).Value
        End If
        If IsDBNull(GV_PendingG.Item("Telephone", e.RowIndex).Value) = False Then
            lblMobile.Text = GV_PendingG.Item("Telephone", e.RowIndex).Value
        End If
        lblFullname.Text = _Title & " " & _Fname & " " & _Lname
        lblWard.Text = GV_PendingG.Item("Ward", e.RowIndex).Value
        lblWardCode.Text = GV_PendingG.Item("WardCode", e.RowIndex).Value
        Dim _WardCode As String
        _WardCode = GV_PendingG.Item("Wardcode", e.RowIndex).Value
        Dim Chk_S As String
        Chk_S = _WardCode.Substring(_WardCode.Length - 1)
        If Chk_S = "S" Then
            chkHaveSp.Checked = True
        Else
            chkHaveSp.Checked = False
        End If
        Dim CUrine As New ClassTestCheck
        If CUrine.UrineCheck(labNo) = True Then
            chkHaveSp.Checked = True
        End If
        'แสดงจำนวน TubeBarcode
        CountTubeBarcode(labNo)

        lblHN.Text = GV_PendingG.Item("HN", e.RowIndex).Value
        If IsDBNull(GV_PendingG.Item("Age", e.RowIndex).Value) = False Then
            lblAge.Text = GV_PendingG.Item("Age", e.RowIndex).Value
        Else
            lblAge.Text = "-"
        End If

        Dim _Birthdate As Date
        If IsDate(GV_PendingG.Item("BirthDate", e.RowIndex).Value) Then
            _Birthdate = GV_PendingG.Item("BirthDate", e.RowIndex).Value
        End If
        lblBirthDate.Text = Format(_Birthdate, "D")
        Dim _Gender As String
        If IsDBNull(GV_PendingG.Item("Gender", e.RowIndex).Value) = False Then
            _Gender = GV_PendingG.Item("Gender", e.RowIndex).Value
            If _Gender = "1" Then
                _Gender = "ชาย"
            ElseIf _Gender = "2" Then
                _Gender = "หญิง"
            Else
                _Gender = "-"
            End If
        Else
            _Gender = "-"
        End If
        lblGender.Text = _Gender

        'แสดง Test ใน List
        Dim SqlTest As String
        SqlTest = "SELECT        ServiceSubPending.LabNo, Test.TCode, Test.ShowInReceive, Test.PrintIndex, Test.Test " & _
"FROM            ServiceSubPending INNER JOIN " & _
                         "Test ON ServiceSubPending.TCode = Test.TCode " & _
"WHERE        (ServiceSubPending.LabNo = N'" & labNo & "') AND (Test.ShowInReceive = 1) " & _
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
                If dr("Tcode") = "VMA" Then
                    chkHaveSp.Checked = True
                End If
            Next
        End If
        _time10 = 0
        ' Catch ex As Exception

        'End Try

    End Sub


    Private Sub GV_PendingS_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles GV_PendingS.CellMouseDown
        lblMobile.Text = "--"
        Try
            lblTubeBule.Visible = False
            lblTubeGreen.Visible = False
            lblTubePurple.Visible = False
            lblTubeRed.Visible = False
            lblExtraBarcode.Visible = False

            GV_PendingS_Cindex = e.ColumnIndex
            GV_PendingS_Rindex = e.RowIndex
            'lblHaveSp.Visible = True
            lblBillNo.Text = GV_PendingS.Item("BillNo", e.RowIndex).Value
            lblNo.Text = GV_PendingS.Item("NoLab_s", e.RowIndex).Value
            Dim labNo As String = GV_PendingS.Item("LabNo_s", e.RowIndex).Value
            lblLabNo.Text = labNo

            Dim _Title, _Fname, _Lname As String
            If IsDBNull(GV_PendingS.Item("OTitle_s", e.RowIndex).Value) = False Then
                _Title = GV_PendingS.Item("OTitle_s", e.RowIndex).Value
            End If
            If IsDBNull(GV_PendingS.Item("OFname_s", e.RowIndex).Value) = False Then
                _Fname = GV_PendingS.Item("OFname_s", e.RowIndex).Value
            End If
            If IsDBNull(GV_PendingS.Item("OLname_s", e.RowIndex).Value) = False Then
                _Lname = GV_PendingS.Item("OLname_s", e.RowIndex).Value
            End If

            lblFullname.Text = _Title & " " & _Fname & " " & _Lname
            lblWard.Text = GV_PendingS.Item("Ward_s", e.RowIndex).Value
            lblWardCode.Text = GV_PendingS.Item("WardCode_s", e.RowIndex).Value
            If ChkWard(lblWardCode.Text) = True Then
                chkHaveSp.Checked = True
            End If
            lblHN.Text = GV_PendingS.Item("HN_s", e.RowIndex).Value
            If IsDBNull(GV_PendingS.Item("Age_s", e.RowIndex).Value) = False Then
                lblAge.Text = GV_PendingS.Item("Age_s", e.RowIndex).Value
            Else
                lblAge.Text = "-"
            End If
            Dim _Birthdate As Date
            If IsDate(GV_PendingS.Item("BirthDate_s", e.RowIndex).Value) Then
                _Birthdate = GV_PendingS.Item("BirthDate_s", e.RowIndex).Value
            End If
            lblBirthDate.Text = Format(_Birthdate, "D")
            Dim _Gender As String
            If IsDBNull(GV_PendingS.Item("Gender_s", e.RowIndex).Value) = False Then
                _Gender = GV_PendingS.Item("Gender_s", e.RowIndex).Value
                If _Gender = "1" Then
                    _Gender = "ชาย"
                ElseIf _Gender = "2" Then
                    _Gender = "หญิง"
                Else
                    _Gender = "-"
                End If
            Else
                _Gender = "-"
            End If
            lblGender.Text = _Gender
            'แสดง Test ใน List
            Dim SqlTest As String
            SqlTest = "SELECT        ServiceSubPending.LabNo, Test.TCode, Test.ShowInReceive, Test.PrintIndex, Test.Test " & _
    "FROM            ServiceSubPending INNER JOIN " & _
                             "Test ON ServiceSubPending.TCode = Test.TCode " & _
    "WHERE        (ServiceSubPending.LabNo = N'" & labNo & "') AND (Test.ShowInReceive = 1) " & _
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
            _time10 = 0
        Catch ex As Exception

        End Try

    End Sub
    Private Sub CountTubeBarcode(ByVal LabNo As String)
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
    Private Sub GV_PendingS_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV_PendingS.RowEnter

    End Sub
    Private Function ChkWard(ByVal WardCode As String) As Boolean
        'ตรวจ Check Ward ว่าเป็นแบบมี Specimen มาแล้วหรือไม่
        Dim SqlChkWard As String
        SqlChkWard = "SELECT * FROM Ward WHERE WardCode='" & WardCode & "'"
        Dim ds As DataSet
        ds = C1.GetDataSet(SqlChkWard)
        If ds.Tables(0).Rows.Count <> 0 Then
            Dim dr As DataRow
            dr = ds.Tables(0).Rows(0)
            ChkWard = dr("IsHaveSpecimen")
        End If
    End Function
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim chkPayment As New ClassInsertDB
        Dim IsPayment As Boolean
        IsPayment = chkPayment.ChkPayment_LabNo(lblLabNo.Text)
        If IsPayment = False Then
            MsgBox("รอการเงิน Key BookNo ก่อน")
            ShowClear()
            Exit Sub
        End If
        If lblLabNo.Text <> "--" Then
            If ChkWard(lblWardCode.Text) = False Then
                'คนไข้ปกติ
                Dim _MsgGNT As String
                Dim C_GenMsg As New MessageGNT
                If chkV.Checked = True Then
                    _MsgGNT = C_GenMsg.GenMessage(lblLabNo.Text, "V", False)
                Else
                    If chkHaveSp.Checked = True Then
                        '_MsgGNT = C_GenMsg.GenMessage(lblLabNo.Text, "T", True)
                        'Print ที่เครื่อง Barcode เดิม
                        ServicePendingToReal_PrintBarcodeOLD()
                        _MsgGNT = ""
                    Else
                        _MsgGNT = C_GenMsg.GenMessage(lblLabNo.Text, "G", False)
                    End If
                End If
                'ส่ง Message เข้าเครือง GNT
                If _MsgGNT <> "" Then
                    Dim gnt As New GNTCOMMWEB.GntActiveFormX
                    Me.Text = gnt.WAITDATA(0, "localhost")
                    lblAcceptNo.Text = gnt.TRANSDATA(0, "localhost", _MsgGNT)
                    If IsNumeric(lblAcceptNo.Text) = True Then
                        ServicePendingToReal()
                    Else
                        MsgBox("พบปัญหาบางประการไม่สามารถ Print ได้")
                    End If
                End If
            ElseIf ChkWard(lblWardCode.Text) = True Then
                'ไม่ต้องส่งเข้า GNT 
                ServicePendingToReal_PrintBarcodeOLD()
            End If
        End If

        'ส่ง Message เข้า LIS
        Dim LIS As New InterfaceLIS
        LIS.SendOrderToLIS(lblBillNo.Text, lblLabNo.Text)
        GV_PendingG_Bind()
        GV_PendingS_Bind()
        'adisak
        ShowClear()
    End Sub

    Private Sub ServicePendingToReal()
        'ServiceMain&SubPending to ServiceMain

        '(ปิด ชั่วคราว) For Real
        Dim C_INSServ As New ClassInsertDB
        lblLabNo.Text = "9021310026"
        C_INSServ.INSERTService(lblLabNo.Text, True)
        C_INSServ.InsertSendToLIS(lblBillNo.Text, lblLabNo.Text, "MTCL" & lblBillNo.Text)

        Dim C_Update As New ClassInsertDB
        C_Update.UpdateIsSendLab(lblLabNo.Text)


        GV_PendingG_Bind()
        GV_PendingS_Bind()

        'ShowClear()
    End Sub
    Private Sub ServicePendingToReal_PrintBarcodeOLD()
        'ServiceMain&SubPending to ServiceMain

        '(ปิด ชั่วคราว) For Real
        Dim C_INSServ As New ClassInsertDB
        C_INSServ.INSERTService(lblLabNo.Text, False)
        C_INSServ.InsertSendToLIS(lblBillNo.Text, lblLabNo.Text, "MTCL" & lblBillNo.Text)


        Dim C_Update As New ClassInsertDB
        C_Update.UpdateIsSendLab(lblLabNo.Text)


        GV_PendingG_Bind()
        GV_PendingS_Bind()

        'ShowClear()
    End Sub
    Private Sub btnRePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRePrint.Click
        RePrintBarcode.Show()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        _time10 += 1
        'Label8.Text = _time10
        If _time10 = 10 Then
            GV_PendingG_Bind()
            GV_PendingS_Bind()
            _time10 = 0
        ElseIf _time10 = 4 And GV_PendingG_Count = 0 Then
            GV_PendingG_Bind()
            GV_PendingS_Bind()
            _time10 = 0
        End If

        'ส่ง Message เข้า LIS สำหรับ Lab ที่มี Specimen
        Dim sql As String
        Dim dtLabSpecimen As DataTable
        Dim _Year As Integer = Now.Year
        Dim _Month As String = Format(Now.Month, "00")
        Dim _Day As String = Format(Now.Day, "00")

        sql = "SELECT BillNo, LabNo FROM ServiceMain INNER JOIN Ward ON ServiceMain.Ward = Ward.WardCode " & _
              "WHERE Ward.IsAutoPrintBarcode = 'True' AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102)) " & _
              "AND ServiceMain.Canceled='False' AND IsSendToLIS='False'"
        dtLabSpecimen = C1.GetDatatable(sql)
        If dtLabSpecimen.Rows.Count > 0 Then
            For i As Integer = 0 To dtLabSpecimen.Rows.Count - 1
                Dim BillNo, LabNo As String
                BillNo = dtLabSpecimen.Rows(i).Item("BillNo")
                LabNo = dtLabSpecimen.Rows(i).Item("LabNo")
                Dim LIS As New InterfaceLIS
                LIS.SendOrderToLIS(BillNo, LabNo)
            Next
        End If
        'CreateASTM_Order()
    End Sub
    Sub CreateASTM_Order()
        'ตรวจสอบว่า LabNo นี้ถูกรับ specimen หรือยัง
        Dim _Year As Integer = Now.Year
        Dim _Month As String = Format(Now.Month, "00")
        Dim _Day As String = Format(Now.Day, "00")
        'Dim sqlNotAcceptedSpecimen As String = "SELECT DISTINCT ServiceSub.BillNo, ServiceDate FROM ServiceMain INNER JOIN ServiceSub ON ServiceMain.BillNo = ServiceSub.Billno INNER JOIN Test ON ServiceSub.TCode = Test.TCode " & _
        '                                          "WHERE IsAcceptedSpecimen = 'False' AND IsAppend = 'True' AND Test.IsProfile ='False' AND Test.IsSendToLIS = 'TRUE' AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102))"
        'Dim dtNotAcceptedSpecimen = C1.GetDatatable(sqlNotAcceptedSpecimen)
        'If dtNotAcceptedSpecimen.Rows.Count > 1 Then 'มี labno ที่ยังไม่ได้รับ specimen
        '    For i As Integer = 0 To dtNotAcceptedSpecimen.Rows.Count - 1
        '        Dim strBillNoAcceptedSpecimen As String = dtNotAcceptedSpecimen.Rows(i).Item("BillNo") 'BillNo ที่ยังไม่ได้รับ specimen
        '        Dim sqlBillNoAcceptedSpecimen As String = "SELECT DISTINCT BillNo, LabNo FROM ServiceSub WHERE BillNo='" & strBillNoAcceptedSpecimen & "' AND IsAcceptedSpecimen = 'True'"
        '        Dim dtBillNoAcceptedSpecimen As DataTable = C1.GetDatatable(sqlBillNoAcceptedSpecimen)
        '        'ตรวจสอบว่า BillNo นี้ได้รับ specimen แล้ว แสดงว่ามี order เพิ่มภายหลัง
        '        If dtBillNoAcceptedSpecimen.Rows.Count > 0 Then
        '            Dim LIS As New InterfaceLIS
        '            Dim BillNo As String = dtBillNoAcceptedSpecimen.Rows(0).Item("BillNo")
        '            Dim LabNo As String = dtBillNoAcceptedSpecimen.Rows(0).Item("LabNo")
        '            LIS.Create_ASTM_File(BillNo, LabNo)
        '            'LIS.Create_ASTM_File("201802/1708", "8022610048")
        '        End If
        '    Next
        'End If
        Dim sqlAppend As String = "SELECT DISTINCT BillNo, LabNo, ServiceDate, IsAppend FROM ServiceMain " & _
                                          "WHERE IsAppend ='True' AND IsSendToLIS ='False' AND Canceled<>'True AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102))"
        Dim dtAppend = C1.GetDatatable(sqlAppend)
        If dtAppend.Rows.Count > 1 Then 'มีการเพิ่ม Order และยังไม่ได้รับ specimen
            For i As Integer = 0 To dtAppend.Rows.Count - 1
                Dim strBillNoAppend As String = dtAppend.Rows(i).Item("BillNo") 'BillNo ที่มีการเพิ่ม Order
                Dim strLabNoAppend As String = dtAppend.Rows(i).Item("BillNo") 'LabNo ที่มีการเพิ่ม Order
                Dim LIS As New InterfaceLIS
                LIS.Create_ASTM_File(strBillNoAppend, strLabNoAppend)
                'LIS.Create_ASTM_File("201802/1708", "8022610048")
                'Update IsAcceptedSpecimen is True and IsSendToLIS is True
                Dim Update As New ClassUpdateDB
                Update.UpdateServiceSub_IsAcceptedSpecimen(strBillNoAppend, strLabNoAppend)
                Update.UpdateServiceMain_IsSendToLIS(strBillNoAppend, strLabNoAppend)
            Next
        End If
    End Sub
    Private Sub ShowClear()
        lblMobile.Text = "--"
        lblNo.Text = "--"
        lblHN.Text = "--"
        lblFullname.Text = "--"
        lblAge.Text = "--"
        lblGender.Text = "--"
        lblBirthDate.Text = "--/--/----"
        lblWardCode.Text = "--"
        lblWard.Text = "--"
        lblAcceptNo.Text = "--"
        lblLabNo.Text = "--"
        ListView1.Items.Clear()
        ListView2.Items.Clear()
        ListView3.Items.Clear()
        'lblHaveSp.Visible = False
        lblTubeBule.Visible = False
        lblTubeGreen.Visible = False
        lblTubePurple.Visible = False
        lblTubeRed.Visible = False
        lblExtraBarcode.Visible = False
    End Sub

    Private Sub chkHaveSp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHaveSp.CheckedChanged
        Dim LabNo As String
        LabNo = lblLabNo.Text
        CountTubeBarcode(LabNo)
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    'Dim LIS As New InterfaceLIS
    '    ''LIS.SendOrderToLIS("201611/1344", "2016111600006")
    '    'LIS.SendOrderToLIS("", "") 'testing send ACK
    '    'ส่ง Message เข้า LIS
    '    Dim LIS As New InterfaceLIS
    '    LIS.SendOrderToLIS(lblBillNo.Text, lblLabNo.Text)

    'End Sub

    'Private Sub StartListening_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartListening.Click
    '    Dim LIS As New InterfaceLIS
    '    LIS.StartListening()
    'End Sub

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    '    Dim LIS As New InterfaceLIS
    '    Dim strACK As String
    '    strACK = "MSH|^~\&|TDR||MU||||ACK|A" & "|P|2.3|" & vbCrLf & "MSA|AA|TD0001000217|"
    '    strACK = Chr(11) & strACK & Chr(13) & Chr(28) & Chr(13)
    '    LIS.Connect("10.51.101.6", "11000", strACK)
    '    'LIS.SendOrderToLIS("1", "1")

    'End Sub
    ' output to listbox
    '
    'Public Sub txtOut(ByVal txt As String) Handles listener.Datareceived
    '    TextBox1.Text = (txt)
    '    Dim LIS As New InterfaceLIS
    '    'Dim client As New TcpClient("10.51.101.6", 11000)
    '    'client.Close()
    '    ' '' ''listener.disconnect()
    '    If InStr(txt, "ORU") > 0 Then
    '        Dim MessageControlID As String
    '        Dim intControlID_Start, intControlID_End As Integer
    '        intControlID_Start = InStr(txt, "ORU_R01|") + 8
    '        intControlID_End = InStr(txt, "|P")
    '        MessageControlID = Mid(txt, intControlID_Start, intControlID_End - intControlID_Start)
    '        Dim strACK As String
    '        strACK = "MSH|^~\&|38-1||LIS|38-1|||ACK|A" & MessageControlID & "|P|2.3|" & vbCrLf & "MSA|AA|" & MessageControlID & "|"
    '        strACK = Chr(11) & strACK & Chr(13) & Chr(28) & Chr(13)
    '        LIS.Connect("10.51.101.6", "11000", strACK)
    '    End If
    '    If InStr(txt, "ORM") > 0 Then
    '        Dim MessageControlID As String
    '        Dim intControlID_Start, intControlID_End As Integer
    '        intControlID_Start = InStr(txt, "ORM_O01|") + 8
    '        intControlID_End = InStr(txt, "|P")
    '        MessageControlID = Mid(txt, intControlID_Start, intControlID_End - intControlID_Start + 1)
    '        'Dim LIS As New InterfaceLIS
    '        Dim strACK As String
    '        '"MSH|^~\&|38-1||LIS|38-1|||ACK|TD0001032035|P|2.3|" & vbCrLf & "MSA|AA|TD0001032035|"
    '        'strACK = "MSH|^~\&|TDR||MU||||ACK|A" & MessageControlID & "|P|2.3|" & vbCrLf & "MSA|AA|TD0001000217|"
    '        strACK = "MSH|^~\&|38-1||LIS|38-1|20170323143747.39+0700||ACK^R01|202|P|2.3" & vbCrLf & "MSA|AA|TD0000000219"
    '        strACK = Chr(11) & strACK & Chr(13) & Chr(28) & Chr(13)
    '        LIS.Connect("10.51.101.6", "11000", strACK)
    '    End If
    'End Sub
#End Region

    'Private Sub FileSystemWatcher1_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs)
    '    'Open AST file to get MSG_ControlID and LabNo
    '    Dim PathFile, MSG_ControlID, LabNo As String
    '    Dim File As New ClassLogFile
    '    Try
    '        PathFile = e.FullPath.ToString
    '        'เปิด File เก่า
    '        Dim FileContent As String = File.GetFileContents(PathFile)
    '        MSG_ControlID = Mid(FileContent, 1, 2)
    '        LabNo = Mid(FileContent, 1, 2)
    '        File.CreateOK_File(MSG_ControlID, LabNo)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnResendOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResendOrder.Click
        ''Create ASTM file
        'Dim dtORDER_ASTM As DataTable
        'Dim sqlASTM As String = "SELECT BillNo, LabNo, IsSendToLIS FROM ServiceMain WHERE LabNo='" & txtLab.Text & "' AND IsSendToLIS=0"
        'dtORDER_ASTM = C1.GetDatatable(sqlASTM)
        'If dtORDER_ASTM.Rows.Count > 0 Then
        '    Dim LIS As New InterfaceLIS
        '    Dim BillNo As String = dtORDER_ASTM.Rows(0).Item("BillNo")
        '    Dim LabNo As String = dtORDER_ASTM.Rows(0).Item("LabNo")
        '    LIS.Create_ASTM_File(BillNo, LabNo)
        'Else
        'ส่ง Message เข้า LIS สำหรับ Lab ที่มี Specimen
        Dim dtLabSpecimen As DataTable
        Dim sql As String
        Dim _Year As Integer = Now.Year
        Dim _Month As String = Format(Now.Month, "00")
        Dim _Day As String = Format(Now.Day, "00")
        Dim txtLabno As String = txtLab.Text
        'sql = "SELECT BillNo, LabNo FROM ServiceMain INNER JOIN Ward ON ServiceMain.Ward = Ward.WardCode " & _
        '"WHERE Ward.IsHaveSpecimen = 1 AND (ServiceDate = CONVERT(DATETIME, '" & _Year & "-" & _Month & "-" & _Day & " 00:00:00', 102)) AND ServiceMain.Canceled=0 AND LabNo<>'2017050500033'"
        sql = "SELECT BillNo, LabNo FROM ServiceMainPending " & _
                "WHERE LabNo='" & txtLab.Text & "' " & _
                "ORDER BY BillNo DESC"
        dtLabSpecimen = C1.GetDatatable(sql)
        If dtLabSpecimen.Rows.Count > 0 Then
            For i As Integer = 0 To dtLabSpecimen.Rows.Count - 1
                Dim BillNo, LabNo As String
                BillNo = dtLabSpecimen.Rows(i).Item("BillNo")
                LabNo = dtLabSpecimen.Rows(i).Item("LabNo")
                Dim LIS As New InterfaceLIS
                'LIS.SendOrderToLIS(BillNo, LabNo)
                'ส่ง Order ที่เป็น HL7 message เข้าระบบ TD-LIS
                LIS.SendOrderHL7(BillNo, LabNo)
                Dim update As New ClassUpdateDB
                update.UpdateServiceMain_IsSendToLIS(BillNo, LabNo)
            Next
        End If
        'End If
        MsgBox("ส่ง Order เรียบร้อยแล้ว")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim INSert1 As New ClassInsertDB
        ''Dim LabNo As String = "8070210076"
        ''INSert1.INSERTService(LabNo, False)

        'Dim LIS As New InterfaceLIS
        ''LIS.SendOrderToLIS(BillNo, LabNo)
        ''ส่ง Order ที่เป็น HL7 message เข้าระบบ TD-LIS
        'LIS.SendOrderHL7("2018071/0058", "8070210076")

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim _MsgGNT As String
        Dim C_GenMsg As New MessageGNT
        _MsgGNT = C_GenMsg.GenMessage("8080110027", "V", True)
        MsgBox(_MsgGNT)
    End Sub

    
End Class


