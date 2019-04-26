'Imports System.Windows.Forms
'Imports System.Threading
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Net.Sockets
Imports System
Imports System.Net
Imports System.Text
Imports System.IO
Imports Microsoft.VisualBasic
'Imports System.Text
Public Class InterfaceLIS
    Dim clientSocket As Object
    Public Shared serverStream As NetworkStream
    Dim readData As String
    Dim infiniteCounter As Integer
    Dim ctThread As Threading.Thread
    Dim PID, PV1, ServiceDate As String
    Public Shared allDone As New ManualResetEvent(False)
    Private WithEvents myChat As New ClassListener
    Dim C1 As New ClassConnect
    Dim ds As DataSet
    Dim command As SqlCommand
    Dim da As New SqlDataAdapter
    Dim Universal_Service_ID() As String


    Private Sub Connecting()
        clientSocket = New System.Net.Sockets.TcpClient()
        Dim ReadINI As New ClassReadINI
        ReadINI.ReadINI()
        Try
            clientSocket.Connect(ReadINI.ServerIP, ReadINI.Port)
            serverStream = clientSocket.GetStream()


            ctThread = New Threading.Thread(AddressOf getMessage)
            ctThread.Start()
            Form2.Label1.Text = "Connected to LIS Server"
            Form2.Label1.ForeColor = Drawing.Color.Green

            'Timer_Reconnect.Stop()

            Form2.Timer1.Interval = 1000
            Form2.Timer1.Start()

        Catch ex As Exception
            Form2.Label1.Text = "Connected Fail"
            Form2.Label1.ForeColor = Drawing.Color.Red


        End Try
    End Sub

    Public Sub Connect(ByVal server As [String], ByVal port As Int32, ByVal message As [String])
        'On Error GoTo err

        'Shared Sub Connect(ByVal server As [String], ByVal port As Int32, ByVal message As [String])
        Try
            ' Create a TcpClient.
            ' Note, for this client to work you need to have a TcpServer 
            ' connected to the same address as specified by the server, port
            ' combination.
            'Dim port As Int32 = 15000
            Dim client As New TcpClient(server, port)

            ' Translate the passed message into ASCII and store it as a Byte array.
            'Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(message)
            Dim data As Byte() = System.Text.Encoding.GetEncoding("TIS-620").GetBytes(message)
            ' Get a client stream for reading and writing.
            '  Stream stream = client.GetStream();
            Dim stream As NetworkStream = client.GetStream()

            ' Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length)

            'Console.WriteLine("Sent: {0}", message)

            ' Receive the TcpServer.response.
            ' Buffer to store the response bytes.
            data = New [Byte](256) {}

            ' String to store the response ASCII representation.
            Dim responseData As [String] = [String].Empty

            ' Read the first batch of the TcpServer response bytes.
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            'Console.WriteLine("Received: {0}", responseData)
            'MainPrintBarcode.TextBox1.Text = responseData
            'message = Chr(6)
            'data = System.Text.Encoding.GetEncoding("TIS-620").GetBytes(message)
            'stream.Write(data, 0, data.Length)

            ' Close everything.
            stream.Close()
            client.Close()
        Catch e As ArgumentNullException
            'Console.WriteLine("ArgumentNullException: {0}", e)
            MsgBox("ArgumentNullException: " & e.Message)
        Catch e As SocketException
            MsgBox(e.ErrorCode & ":" & e.Message)
            'Console.WriteLine("SocketException: {0}", e)
        Catch e As Exception

        End Try
        'err:
        'Exit Sub
        'Console.WriteLine(ControlChars.Cr + " Press Enter to continue...")
        'Console.Read()
    End Sub
    Private Sub getMessage()
        For infiniteCounter = 1 To 2
            Try
                infiniteCounter = 1
                serverStream = clientSocket.GetStream()
                Dim buffSize As Integer
                Dim inStream(10024) As Byte
                buffSize = clientSocket.ReceiveBufferSize
                serverStream.Read(inStream, 0, buffSize)
                Dim returndata As String = System.Text.Encoding.GetEncoding(874).GetString(inStream)
                readData = "" + returndata
                msg()
            Catch ex As Exception
                infiniteCounter = 2
                SyncLock ctThread
                    ctThread.Abort()
                End SyncLock
                Exit For
            End Try
        Next
    End Sub
    Private Sub msg()
        If Form2.InvokeRequired Then
            Form2.Invoke(New MethodInvoker(AddressOf msg))
        Else
            Form2.txtReturnData.Text = readData
        End If
    End Sub

    Public Sub SendOrderToLIS(ByVal BillNo As String, ByVal LabNo As String)
        'Connecting()
        'Send_ORM(BillNo, LabNo)
        If BillNo = "" And LabNo = "" Then
            Dim strACK As String
            'strACK = "MSH|^~\&|TDR||MU||||ACK|ATD0000008651" & "|P|2.3|" & vbCrLf & "MSA|AA|TD0001000217|"
            strACK = Chr(11) & "MSH|^~\&|38-1||LIS|38-1|||ACK|ATD0000008660|P|2.3|" & vbCrLf & "MSA|AA|TD0000008660|" & Chr(13) & Chr(28) & Chr(13)
            'Connect("10.51.101.6", "15000", strACK)
            Connect("10.4.101.102", "15000", strACK)
        Else
            ''ตรวจสอบว่า LabNo นี้ถูกรับ specimen หรือยัง
            'Dim sqlMultipleLabNumber As String = "SELECT DISTINCT BillNo FROM ServiceSub WHERE LabNo='" & LabNo & "'"
            'Dim dtLabNoServiceSub = C1.GetDatatable(sqlMultipleLabNumber)
            'If dtLabNoServiceSub.Rows.Count > 1 Then 'ตรวจสอบว่ามี labno เดียวกัน แต่ BillNo ต่างกัน แสดงว่ามีการคีย์ order เพิ่ม
            '    For i As Integer = 0 To dtLabNoServiceSub.Rows.Count - 1
            '        Dim strBillNo As String = dtLabNoServiceSub.Rows(i).Item("BillNo")
            '        Dim sqlBillNoServiceSub As String = "SELECT DISTINCT BillNo FROM ServiceSub WHERE BillNo='" & strBillNo & "'"
            '        Dim dtBillNoServiceSub As DataTable = C1.GetDatatable(sqlBillNoServiceSub)
            '        'ตรวจสอบว่าได้รับ Specimen ไปหรือยัง
            '        Dim sqlServiceSubAcceptedSpecimen As String = "SELECT BillNo, IsAcceptedSpecimen FROM ServiceSub WHERE BillNo='" & strBillNo & "' AND IsAcceptedSpecimen = 'True'"
            '        Dim dtServiceSubAcceptedSpecimen As DataTable = C1.GetDatatable(sqlServiceSubAcceptedSpecimen)
            '        If dtServiceSubAcceptedSpecimen.Rows.Count = 0 Then 'ยังไม่ได้รับ specimen
            '            'ส่ง Order ที่เป็น HL7 message เข้าระบบ TD-LIS
            SendOrderHL7(BillNo, LabNo)
            '        Else 'รับ specimen แล้ว ให้สร้างไฟล์ ast แล้ววางใน path ที่กำหนด เพื่อเพิ่ม order
            ''Generate ASTM file and copy file to TD-LIS server
            'Create_ASTM_File(BillNo, LabNo)
            'End If
            '        Next
            '    End If
        End If
    End Sub
    Public Sub SendOrderHL7(ByVal BillNo As String, ByVal LabNo As String)
        Dim NumberOfTestOrder As Integer = GetTestOrder(LabNo).NumberOfTest
        If NumberOfTestOrder > 0 Then
            Dim Log As New ClassLogFile
            Dim strADT_Message As String = Send_ADT(LabNo)
            'Connect("10.51.101.6", 15003, strADT_Message)
            Connect("10.4.101.102", 15003, strADT_Message)
            Log.LogFile(strADT_Message)
            Dim strOrderMessage As String = Send_ORM(LabNo)
            'Connect("10.51.101.6", 15000, strOrderMessage)
            Connect("10.4.101.102", 15000, strOrderMessage)
            Log.LogFile(strOrderMessage)

            Dim update As New ClassUpdateDB
            update.UpdateServiceMain_IsSendToLIS(BillNo, LabNo)
        End If
    End Sub
    Public Sub Create_ASTM_File(ByVal BillNo As String, ByVal LabNo As String)
        Dim iLineCount As Integer
        Dim strDateTime As String = Date.Now.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
        Dim strH As String = "H|^~\&|||38-1^LIS||ORM|||38-1||P|A.2.|" & strDateTime & "|"
        iLineCount = iLineCount + 1
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim sqlService, PatientName, BirthDate, Gender As String
        Dim dtBirthDate, dtServiceDate, dtServiceTime As Date
        sqlService = "SELECT BillNo, CID, ServiceMain.HN, OTitle, OFName, OLName, BirthDate, Gender, Telephone, ServiceDate, ServiceTime, IsExpress " & _
                     "FROM ServiceMain INNER JOIN Patients ON ServiceMain.HN = Patients.HN " & _
                     "WHERE BillNo='" & BillNo & "'"
        'sqlService = "SELECT LabNo, CID, ServiceMain.HN, OTitle, OFName, OLName, BirthDate, Gender, Telephone, ServiceDate, ServiceTime, IsExpress, Ward.Location, Ward.Ward " & _
        '            "FROM ServiceMain INNER JOIN Patients ON ServiceMain.HN = Patients.HN INNER JOIN Ward ON ServiceMain.Ward = Ward.WardCode " & _
        '            "WHERE LabNo='" & LabNo & "'"
        Dim drService As SqlDataReader
        drService = clsConnect.GetDataReader(sqlService)
        Dim SiteCode, ServiceDate, ServiceTime, RequestDate, Ward, Location, strExpress, HN As String
        Dim IsExpress As Boolean
        If drService.HasRows Then
            Do While drService.Read
                'Ward = drService("Ward")
                'If IsDBNull(drService("Location")) = False Then
                '    Location = drService("Location")
                'Else
                '    Location = "SII"
                'End If
                HN = drService("HN")
                'HN = HN & "^^^^PATNUMBER~^^^^ALTNUMBER"
                'CID = ""
                PatientName = drService("OTitle") & drService("OFName") & "^" & drService("OLName") & "^^^"
                If IsDBNull(drService("BirthDate")) = False Then
                    dtBirthDate = drService("BirthDate")
                    BirthDate = dtBirthDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
                End If
                If IsDBNull(drService("Gender")) = False Then
                    Gender = drService("Gender")
                    If Gender = "1" Then
                        Gender = "M"
                    ElseIf Gender = "2" Then
                        Gender = "F"
                    Else
                        Gender = "U"
                    End If
                End If
                dtServiceDate = drService("ServiceDate")
                ServiceDate = dtServiceDate.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
                ServiceDate = Left(ServiceDate, 8)
                dtServiceTime = drService("ServiceTime")
                Dim cultures() As CultureInfo = {CultureInfo.InvariantCulture, _
                                       New CultureInfo("en-us")}
                For Each culture As CultureInfo In cultures
                    ServiceTime = dtServiceTime.ToString(culture)
                Next
                ServiceTime = Right(ServiceTime, 11)
                ServiceTime = ConvertTime(ServiceTime)
                RequestDate = ServiceDate & ServiceTime
                IsExpress = drService("IsExpress")
            Loop
            If IsExpress Then
                strExpress = "S^^^11^D"
            Else
                strExpress = "R^^^11^D"
            End If
        End If

        Dim strP As String = "P|1|" & HN & "|||" & PatientName & "||" & BirthDate & "|" & Gender & "|||||||||||||||||SII^^^|"
        iLineCount = iLineCount + 1
        'หาจำนวน Test order 
        Dim NumberOfTestOrder As Integer = GetTestOrderASTM(BillNo, LabNo).NumberOfTest
        Dim strOBR As String
        'สร้างสตริง  OBR ตามจำนวน Test order
        Dim OBR(NumberOfTestOrder) As String
        For i As Integer = 0 To NumberOfTestOrder - 1
            Dim strSequence As String
            Select Case i + 1
                Case Is < 10
                    strSequence = "000" & i + 1
                Case Is < 100
                    strSequence = "00" & i + 1
            End Select
            OBR(i) = "OBR|" & strSequence & "|^" & LabNo & "||" & Universal_Service_ID(i) & "|" & strExpress & "|" & RequestDate & _
                     "|" & strDateTime & "||||A|||" & strDateTime & "|" & "DEFDOC^Default Doctor||SII|||||||"
            If i = 0 Then
                strOBR = OBR(i)
            Else
                strOBR = strOBR & vbCrLf & OBR(i)
            End If
            iLineCount = iLineCount + 1
        Next
        Dim strL As String = "L|||1|" & iLineCount & "|"
        Dim strOrderContent As String = strH & vbCrLf & strP & vbCrLf & strOBR & vbCrLf & strL
        'Create AST file
        Dim Filename As String = "ast" & LabNo & ".spi"
        Dim PathFile As String = "C:\" & Filename
        Dim NetworkDrive As String = "X"
        ' open file with encoding write data here
        Dim fileAST As TextWriter = New StreamWriter(PathFile, False, System.Text.Encoding.GetEncoding("TIS-620"))
        fileAST.Write(strOrderContent)
        ' save and close it
        fileAST.Close()
        ClassNetworkDrive.Map("\\10.51.101.8\ftp", NetworkDrive, False, "bjc", "bjc@dm1n")
        'Copy AST file to LIS-TD (TDNLPROD)
        CopyFileToServer(Filename, PathFile, NetworkDrive)

        'Create OK file
        Dim Filename_OK As String = "ast" & LabNo & ".ok"
        Dim PathFile_OK As String = "C:\" & Filename_OK
        ' open file with encoding write data here
        Dim fileOK As TextWriter = New StreamWriter(PathFile_OK, False, System.Text.Encoding.GetEncoding("TIS-620"))
        fileOK.Write(strOrderContent)
        ' save and close it
        fileOK.Close()

        'Copy OK file to LIS-TD (TDNLPROD)
        CopyFileToServer(Filename_OK, PathFile_OK, NetworkDrive)
        ClassNetworkDrive.Unmap(NetworkDrive)
    End Sub
    Sub CopyFileToServer(ByVal filename As String, ByVal PathFile As String, ByVal Drive As String)
        Dim strLocation As String = Drive & ":\" & filename
        File.Copy(PathFile, strLocation)
    End Sub
    Public Class StateObject
        ' Client  socket.
        Public workSocket As Socket = Nothing
        ' Size of receive buffer.
        Public Const BufferSize As Integer = 1024
        ' Receive buffer.
        Public buffer(BufferSize) As Byte
        ' Received data string.
        Public sb As New StringBuilder
    End Class 'StateObject
    Public Sub StartListening()
        ' Data buffer for incoming data.
        Dim bytes() As Byte = New [Byte](1023) {}

        ' Establish the local endpoint for the socket.
        Dim ipHostInfo As IPHostEntry = Dns.Resolve(Dns.GetHostName())
        Dim localEP = New IPEndPoint(ipHostInfo.AddressList(0), 11000)
        Dim IP As String = ipHostInfo.AddressList(0).ToString
        MsgBox(IP)
        'Console.WriteLine("Local address and port : {0}", localEP.ToString())
        'MainPrintBarcode.TextBox1.Text = localEP.ToString()
        Dim listener As New Socket(localEP.Address.AddressFamily, _
           SocketType.Stream, ProtocolType.Tcp)

        Try
            listener.Bind(localEP)
            listener.Listen(10)

            While True
                allDone.Reset()

                'Console.WriteLine("Waiting for a connection...")
                listener.BeginAccept(New AsyncCallback(AddressOf AcceptCallback), listener)

                allDone.WaitOne()
            End While
        Catch e As Exception
            'Console.WriteLine(e.ToString())
            MsgBox(e.ToString)
        End Try
        'Console.WriteLine("Closing the listener...")
    End Sub 'StartListening
    Public Shared Sub AcceptCallback(ByVal ar As IAsyncResult)
        ' Get the socket that handles the client request.
        Dim listener As Socket = CType(ar.AsyncState, Socket)
        ' End the operation.
        Dim handler As Socket = listener.EndAccept(ar)

        ' Create the state object for the async receive.
        Dim state As New StateObject
        state.workSocket = handler
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)
    End Sub 'AcceptCallback
    Public Shared Sub ReadCallback(ByVal ar As IAsyncResult)
        Dim content As String = String.Empty

        ' Retrieve the state object and the handler socket
        ' from the asynchronous state object.
        Dim state As StateObject = CType(ar.AsyncState, StateObject)
        Dim handler As Socket = state.workSocket

        ' Read data from the client socket. 
        Dim bytesRead As Integer = handler.EndReceive(ar)

        If bytesRead > 0 Then
            ' There  might be more data, so store the data received so far.
            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead))

            ' Check for end-of-file tag. If it is not there, read 
            ' more data.
            content = state.sb.ToString()
            'If content.IndexOf("<EOF>") > -1 Then
            If content.Length > 0 Then
                ' All the data has been read from the 
                ' client. Display it on the console.
                'Console.WriteLine("Read {0} bytes from socket. " + vbLf + " Data : {1}", content.Length, content)
                'MainPrintBarcode.TextBox1.Text = content
                MsgBox(content)
                ' Echo the data back to the client.
                'Send(handler, content)
            Else
                ' Not all data received. Get more.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)
            End If
        End If
    End Sub 'ReadCallback
    Private Shared Sub Send(ByVal handler As Socket, ByVal data As String)
        ' Convert the string data to byte data using ASCII encoding.
        Dim byteData As Byte() = Encoding.ASCII.GetBytes(data)

        ' Begin sending the data to the remote device.
        handler.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), handler)
    End Sub 'Send
    Private Shared Sub SendCallback(ByVal ar As IAsyncResult)
        ' Retrieve the socket from the state object.
        Dim handler As Socket = CType(ar.AsyncState, Socket)

        ' Complete sending the data to the remote device.
        Dim bytesSent As Integer = handler.EndSend(ar)
        Console.WriteLine("Sent {0} bytes to client.", bytesSent)

        handler.Shutdown(SocketShutdown.Both)
        handler.Close()
        ' Signal the main thread to continue.
        allDone.Set()
    End Sub 'SendCallback
    Private Function Send_ADT(ByVal LabNo As String) As String
        Dim HL7 As New HL7_Send
        Dim MSH, MSG_ControlID, strDateTime, strADT As String
        Dim FullAccessNumber As String
        If Len(LabNo) = 13 Then
            FullAccessNumber = Right(LabNo, 10)
        Else
            FullAccessNumber = LabNo
        End If
        MSG_ControlID = FullAccessNumber
        strDateTime = Date.Now.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
        MSH = HL7.MSH("MU", "", "TD", "", strDateTime, "", "ADT^A08", MSG_ControlID, "P", "2.3")
        Dim HN, CID, PatientName, BirthDate, Gender, Telephone As String
        Dim dtBirthDate, dtServiceDate, dtServiceTime As Date
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim sqlService As String
        sqlService = "SELECT LabNo, CID, ServiceMain.HN, OTitle, OFName, OLName, BirthDate, Gender, Telephone, Ward, ServiceDate, ServiceTime " & _
                     "FROM ServiceMain INNER JOIN Patients ON ServiceMain.HN = Patients.HN " & _
                     "WHERE LabNo='" & LabNo & "'"
        Dim drService As SqlDataReader
        drService = clsConnect.GetDataReader(sqlService)
        Dim Ward, SiteCode, ServiceDate, ServiceTime As String
        If drService.HasRows Then
            Do While drService.Read
                HN = drService("HN")
                HN = HN & "^^^^PATNUMBER~^^^^ALTNUMBER"
                'CID = drService("CID")
                CID = ""
                PatientName = drService("OTitle") & drService("OFName") & "^" & drService("OLName") & "^H^^^^L~^^^^^^M"
                If IsDBNull(drService("BirthDate")) = False Then
                    dtBirthDate = drService("BirthDate")
                    BirthDate = dtBirthDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
                End If
                If IsDBNull(drService("Gender")) Then
                    Gender = "U"
                Else
                    Gender = drService("Gender")
                    If Gender = "1" Then
                        Gender = "M"
                    ElseIf Gender = "2" Then
                        Gender = "F"
                    Else
                        Gender = "U"
                    End If
                End If
                If IsDBNull(drService("Telephone")) = False Then
                    Telephone = drService("Telephone")
                End If
                PID = HL7.PID("", HN, CID, PatientName, "", BirthDate, Gender, "", "", "", Telephone, "", "", "", "", "", "", "", "", "")
                Ward = drService("Ward")
                SiteCode = "1" 'ดูจาก SiteCode ที่กำหนดไว้
                dtServiceDate = drService("ServiceDate")
                ServiceDate = dtServiceDate.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
                ServiceDate = Left(ServiceDate, 8)
                dtServiceTime = drService("ServiceTime")
                ServiceTime = dtServiceTime.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
                ServiceTime = Right(ServiceTime, 6)
            Loop
            Dim Visit_Number As String = LabNo
            'PV1 = HL7.PV1("O", Nothing, Nothing, Visit_Number, Nothing, Nothing, Nothing, ServiceDate + ServiceTime)
            PV1 = HL7.PV1("O", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            strADT = MSH & vbCrLf & PID & vbCrLf & PV1
            'strADT = strADT.Replace(" ", "^")
            strADT = Chr(11) & strADT & Chr(13) & Chr(28) & Chr(13)
            Return strADT
        Else
            MsgBox("ไม่พบข้อมูลคนไข้ในฐานข้อมูล", MsgBoxStyle.Critical, "Null record")
        End If
    End Function

    Private Function Send_ORM(ByVal LabNo As String) As String
        'ลง LogFile
        'Dim Log As New ClassLogFile
        'Log.LogFile(txtSendMsg.Text)
        Dim HL7 As New HL7_Send
        Dim MSG_ControlID, MSH, ORC, strDateTime, strSendMSG As String
        Dim FullAccessNumber, PON As String
        If Len(LabNo) = 13 Then
            FullAccessNumber = Right(LabNo, 10)
        Else
            FullAccessNumber = LabNo
        End If
        MSG_ControlID = FullAccessNumber
        strDateTime = Date.Now.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
        MSH = HL7.MSH("MU", "", "TD", "", strDateTime, "", "ORM^O01", MSG_ControlID, "P", "2.3")
        Dim HN, CID, PatientName, PatientNameComment, BirthDate, Gender, PatientAdddr, Telephone, Comment As String
        Dim dtBirthDate, dtServiceDate, dtServiceTime As Date
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim sqlService As String
        'sqlService = "SELECT BillNo, CID, ServiceMain.HN, OTitle, OFName, OLName, BirthDate, Gender, Telephone, ServiceDate, ServiceTime " & _
        '             "FROM ServiceMain INNER JOIN Patients ON ServiceMain.HN = Patients.HN " & _
        '             "WHERE BillNo='" & BillNo & "'"
        sqlService = "SELECT LabNo, CID, ServiceMain.HN, OTitle, OFName, OMName, OLName, BirthDate, Gender, Telephone, ServiceDate, ServiceTime, IsExpress, Ward.Location, Ward.Ward, Comment " & _
                    "FROM ServiceMain INNER JOIN Patients ON ServiceMain.HN = Patients.HN INNER JOIN Ward ON ServiceMain.Ward = Ward.WardCode " & _
                    "WHERE LabNo='" & LabNo & "'"
        Dim drService As SqlDataReader
        drService = clsConnect.GetDataReader(sqlService)
        Dim SiteCode, ServiceDate, ServiceTime, RequestDate, Ward, Location As String
        Dim IsExpress As Boolean
        If drService.HasRows Then
            Do While drService.Read
                Ward = drService("Ward")
                If IsDBNull(drService("Location")) = False Then
                    Location = drService("Location")
                Else
                    Location = "SII"
                End If
                HN = drService("HN")
                HN = HN & "^^^^PATNUMBER~^^^^ALTNUMBER"
                ' CID = drService("CID")
                CID = ""
                If IsDBNull(drService("OMName")) = False Then
                    PatientName = drService("OTitle") & drService("OFName") & "^" & drService("OMName") & "^" & drService("OLName") & "^H^^^^L~^^^^^^M"
                    PatientNameComment = drService("OTitle") & drService("OFName") & " " & drService("OMName") & " " & drService("OLName")
                Else
                    PatientName = drService("OTitle") & drService("OFName") & "^^" & drService("OLName") & "^H^^^^L~^^^^^^M"
                    PatientNameComment = drService("OTitle") & drService("OFName") & " " & drService("OLName")
                End If
                If IsDBNull(drService("Comment")) = False Then
                    Comment = drService("Comment")
                End If
                If IsDBNull(drService("BirthDate")) = False Then
                    dtBirthDate = drService("BirthDate")
                    BirthDate = dtBirthDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
                End If
                If IsDBNull(drService("Gender")) = False Then
                    Gender = drService("Gender")
                    If Gender = "1" Then
                        Gender = "M"
                    ElseIf Gender = "2" Then
                        Gender = "F"
                    Else
                        Gender = Nothing
                    End If
                End If
                If IsDBNull(drService("Telephone")) = False Then
                    Telephone = drService("Telephone")
                End If
                PID = HL7.PID("", HN, CID, PatientName, "", BirthDate, Gender, "", "", "", Telephone, "", "", "", "", "", "", "", "", "")
                SiteCode = "1" 'ดูจาก SiteCode ที่กำหนดไว้

                dtServiceDate = drService("ServiceDate")
                ServiceDate = dtServiceDate.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
                ServiceDate = Left(ServiceDate, 8)
                dtServiceTime = drService("ServiceTime")
                Dim cultures() As CultureInfo = {CultureInfo.InvariantCulture, _
                                       New CultureInfo("en-us")}
                For Each culture As CultureInfo In cultures
                    ServiceTime = dtServiceTime.ToString(culture)
                Next

                'ServiceTime = dtServiceTime.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
                ServiceTime = Right(ServiceTime, 11)
                ServiceTime = ConvertTime(ServiceTime)
                'dtServiceDate = drService("ServiceDate")
                'ServiceDate = dtServiceDate.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture)
                'dtServiceTime = drService("ServiceTime")
                'ServiceTime = dtServiceDate.ToString("hhmm", CultureInfo.InvariantCulture)
                RequestDate = ServiceDate & ServiceTime
                IsExpress = drService("IsExpress")
            Loop
        End If
        Dim strExpress As String
        If IsExpress Then
            strExpress = "^^^^^S"
        Else
            strExpress = Nothing
        End If
        'Dim Visit_Number As String = Right(LabNo, 7)
        Dim Visit_Number As String = LabNo
        'PV1 = HL7.PV1("O", Nothing, Nothing, Visit_Number, Nothing, Nothing, Nothing, RequestDate)
        PV1 = HL7.PV1("O", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        ''PON = GetTestOrder(BillNo, LabNo).PON
        ''PON = LabNo
        ''PON = "3000" + LabNo 'SiteCode+LabNo
        'หาจำนวน Test order 
        Dim NumberOfTestOrder As Integer = GetTestOrder(LabNo).NumberOfTest
        'สร้างสตริง ORC และ OBR ตามจำนวน Test order
        Dim OBR(NumberOfTestOrder) As String
        Dim NTE, ORC_OBR, ORC_NTE As String
        ORC = HL7.ORC("NW", GetTestOrder(LabNo).PON(0), "^^^", FullAccessNumber, "", "", "", "", "", "", "", "DEFDOC", "SII", "", RequestDate, "", "", "", "")
        NTE = "NTE|||" & Comment & ";" & Ward & ";" & Location & ";" & PatientNameComment
        ORC_NTE = ORC & vbCrLf & NTE
        For i As Integer = 0 To NumberOfTestOrder - 1
            ORC = HL7.ORC("NW", GetTestOrder(LabNo).PON(i), "^^^", FullAccessNumber, "", "", "", "", "", "", "", "DEFDOC", "SII", "", RequestDate, "", "", "", "")
            OBR(i) = HL7.OBR(i + 1, GetTestOrder(LabNo).PON(i), "", GetTestOrder(LabNo).Universal_Service_ID(i), "", "", "", "", "", "", "", "", "", "", "", "DEFDOC", "", "", "", "", "", "", "", "", "", "", strExpress, "")
            If i < NumberOfTestOrder - 1 Then
                ORC_OBR = ORC_OBR & vbCrLf & OBR(i) & vbCrLf & ORC
            Else
                ORC_OBR = ORC_OBR & vbCrLf & OBR(i)
            End If
        Next
        ORC_OBR = ORC_NTE + ORC_OBR
        'ส่งเข้า LIS
        Dim strMSG As String
        strMSG = MSH & vbCrLf & PID & vbCrLf & PV1 & vbCrLf & ORC_OBR
        'strSendMSG = strMSG.Replace(" ", "^")
        strSendMSG = Chr(11) & strMSG & Chr(13) & Chr(28) & Chr(13)

        ''ลง LogFile
        'Dim Log As New ClassLogFile
        'Log.LogFile(strSendMSG)

        Return strSendMSG
        ''Dim outStream As Byte() = System.Text.Encoding.GetEncoding(874).GetBytes(strSendMSG)
        'Dim outStream As Byte() = System.Text.Encoding.GetEncoding("TIS-620").GetBytes(strSendMSG)
        'serverStream.Write(outStream, 0, outStream.Length)
        'serverStream.Flush()

        ' '' Translate the passed message into ASCII and store it as a Byte array.
        ''Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(strSendMSG)
        'Dim data As [Byte]()
        '' Receive the TcpServer.response.
        '' Buffer to store the response bytes.
        'data = New [Byte](256) {}

        '' String to store the response ASCII representation.
        'Dim responseData As [String] = [String].Empty

        '' Read the first batch of the TcpServer response bytes.
        'Dim bytes As Int32 = serverStream.Read(data, 0, data.Length)
        'responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
        ''Console.WriteLine("Received: {0}", responseData)
        'MainPrintBarcode.TextBox1.Text = responseData

        ' ''Create order text file
        ''Dim fileOrder As New ClassLogFile
        ''fileOrder.CreateORM_File(strSendMSG, FullAccessNumber)


    End Function
    Function ConvertTime(ByVal ServiceTime As String) As String
        Dim HH, MM, SS, Period As String
        HH = LTrim(Left(ServiceTime, 2))
        If HH.Length < 2 Then HH = "0" + HH
        MM = Mid(ServiceTime, 4, 2)
        SS = Mid(ServiceTime, 7, 2)
        Period = Right(ServiceTime, 2)
        If Period = "PM" Then
            Select Case HH
                Case "01"
                    HH = "13"
                Case "02"
                    HH = "14"
                Case "03"
                    HH = "15"
                Case "04"
                    HH = "16"
                Case "05"
                    HH = "17"
                Case "06"
                    HH = "18"
                Case "07"
                    HH = "19"
                Case "08"
                    HH = "20"
                Case "09"
                    HH = "21"
                Case "10"
                    HH = "22"
                Case "11"
                    HH = "23"
            End Select
        End If
        ServiceTime = HH + MM + SS
        Return ServiceTime
    End Function

    Public Structure ServiceSub 'return ค่่่่า 2 ค่าจากฟังก์ชั่น GetTestOrder โดยใช้ Structure
        Public PON() As String
        Public NumberOfTest As Integer
        Public Universal_Service_ID() As String
    End Structure
    Public Function GetTestOrderOLD(ByVal BillNo As String, ByVal LabNo As String) As ServiceSub
        Dim TestOrder As ServiceSub
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim sqlServiceSub As String
        sqlServiceSub = "SELECT BillNo, Test.TCodeNumber, Test.IsSendToLIS, ServiceSub.TCode, Test " & _
                        "FROM ServiceSub INNER JOIN Test ON ServiceSub.TCode = Test.TCode " & _
                        "WHERE BillNo='" & BillNo & "' AND Test.IsSendToLIS = 1"
        Dim dsServiceSub As DataSet
        dsServiceSub = clsConnect.GetDataSet(sqlServiceSub)
        TestOrder.NumberOfTest = dsServiceSub.Tables(0).Rows.Count
        Dim N As Integer = dsServiceSub.Tables(0).Rows.Count
        If TestOrder.NumberOfTest > 0 Then
            ' ReDim TestOrder.PON(N)
            ReDim TestOrder.Universal_Service_ID(N)
            ReDim TestOrder.PON(N)
            For i As Integer = 0 To N - 1
                Dim drTestOrder As DataRow
                drTestOrder = dsServiceSub.Tables(0).Rows(i)
                Dim TCodeNumber As String = drTestOrder("TCodeNumber")
                If CInt(TCodeNumber) < 10 Then
                    TCodeNumber = "00" & TCodeNumber
                ElseIf CInt(TCodeNumber) < 100 Then
                    TCodeNumber = "0" & TCodeNumber
                End If
                TestOrder.PON(i) = TCodeNumber & Right(LabNo, 9)
                TestOrder.Universal_Service_ID(i) = drTestOrder("TCode") & "^" & drTestOrder("Test")
                Dim updateServiceSub As New ClassUpdateDB
                updateServiceSub.UpdateServiceSub_IsSendToLIS(LabNo, drTestOrder("TCode"))
            Next
        End If
        Return TestOrder
    End Function
    Public Function GetTestOrder(ByVal LabNo As String) As ServiceSub
        Dim TestOrder As ServiceSub
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim sqlProfileTest, sqlServiceSub As String
        'sqlProfileTest = "SELECT * FROM ProfileTest WHERE Profile='Liver'"
        sqlProfileTest = "SELECT LabNo, Test.TCodeNumber, Test.IsSendToLIS, ServiceSub.TCode, Test, IsProfile " & _
                         "FROM ServiceSub INNER JOIN Test ON ServiceSub.TCode = Test.TCode " & _
                         "WHERE LabNo='" & LabNo & "' AND IsProfile = 1 AND Test.IsSendToLIS = 1"
        sqlServiceSub = "SELECT LabNo, Test.TCodeNumber, Test.IsSendToLIS, ServiceSub.TCode, Test " & _
                        "FROM ServiceSub INNER JOIN Test ON ServiceSub.TCode = Test.TCode " & _
                        "WHERE LabNo='" & LabNo & "' AND Test.IsSendToLIS = 1 AND ServiceSub.IsAcceptedSpecimen = 0"
        ds = New DataSet
        Try
            conn.Open()
            'Create data table ProfileTest in dataset
            command = New SqlCommand(sqlProfileTest, conn)
            da.SelectCommand = command
            da.Fill(ds, "ProfileTest")
            'Create data table ServiceSub in dataset
            da.SelectCommand.CommandText = sqlServiceSub
            da.Fill(ds, "ServiceSub")
            CreateDataTable("ProfileExpand")
            CreateDataTable("TestOrder")
            'แตก Test จาก ProfileTest ลงใน  ProfileExpand data table
            For i = 0 To ds.Tables("ProfileTest").Rows.Count - 1
                ExpandProfile(ds.Tables("ProfileTest").Rows(i).Item("TCode"))
            Next
            For i As Integer = 0 To ds.Tables("ProfileExpand").Rows.Count - 1
                Dim tcode As String = ds.Tables("ProfileExpand").Rows(i).Item("TCode")
                For j As Integer = 0 To ds.Tables("ServiceSub").Rows.Count - 1
                    If ds.Tables("ServiceSub").Rows(j).Item("TCode") = tcode Then
                        ds.Tables("ServiceSub").Rows.RemoveAt(j)
                        Exit For
                    End If
                Next
            Next
            Dim test As String
            For j As Integer = 0 To ds.Tables("ServiceSub").Rows.Count - 1
                test = test + " " + ds.Tables("ServiceSub").Rows(j).Item("TCode")
            Next

            'dsServiceSub = clsConnect.GetDataSet(sqlServiceSub)
            TestOrder.NumberOfTest = ds.Tables("ServiceSub").Rows.Count
            Dim N As Integer = ds.Tables("ServiceSub").Rows.Count
            If TestOrder.NumberOfTest > 0 Then
                ' ReDim TestOrder.PON(N)
                ReDim TestOrder.Universal_Service_ID(N)
                ReDim TestOrder.PON(N)
                For i As Integer = 0 To N - 1
                    Dim drTestOrder As DataRow
                    drTestOrder = ds.Tables("ServiceSub").Rows(i)
                    Dim TCodeNumber As String = drTestOrder("TCodeNumber")
                    If CInt(TCodeNumber) < 10 Then
                        TCodeNumber = "00" & TCodeNumber
                    ElseIf CInt(TCodeNumber) < 100 Then
                        TCodeNumber = "0" & TCodeNumber
                    End If
                    TestOrder.PON(i) = TCodeNumber & Right(LabNo, 9)
                    TestOrder.Universal_Service_ID(i) = drTestOrder("TCode") & "^" & drTestOrder("Test")
                    Dim updateServiceSub As New ClassUpdateDB
                    updateServiceSub.UpdateServiceSub_IsSendToLIS(LabNo, drTestOrder("TCode"))
                Next
            End If
            da.Dispose()
            command.Dispose()
            conn.Close()
            Return TestOrder

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try


    End Function
    Public Function GetTestOrderASTM(ByVal BillNo As String, ByVal LabNo As String) As ServiceSub
        Dim TestOrder As ServiceSub
        Dim clsConnect As New ClassConnect
        Dim conn As New SqlConnection(clsConnect.ConnectStr)
        Dim sqlProfileTest, sqlServiceSub As String
        'sqlProfileTest = "SELECT * FROM ProfileTest WHERE Profile='Liver'"
        sqlProfileTest = "SELECT LabNo, Test.TCodeNumber, Test.IsSendToLIS, ServiceSub.TCode, Test, IsProfile " & _
                         "FROM ServiceSub INNER JOIN Test ON ServiceSub.TCode = Test.TCode " & _
                         "WHERE BillNo='" & BillNo & "' AND IsProfile = 1 AND Test.IsSendToLIS = 1"
        sqlServiceSub = "SELECT LabNo, Test.TCodeNumber, Test.IsSendToLIS, ServiceSub.TCode, Test " & _
                        "FROM ServiceSub INNER JOIN Test ON ServiceSub.TCode = Test.TCode " & _
                        "WHERE BillNo='" & BillNo & "' AND Test.IsSendToLIS = 1 AND ServiceSub.IsAcceptedSpecimen = 0"
        ds = New DataSet
        Try
            conn.Open()
            'Create data table ProfileTest in dataset
            command = New SqlCommand(sqlProfileTest, conn)
            da.SelectCommand = command
            da.Fill(ds, "ProfileTest")
            'Create data table ServiceSub in dataset
            da.SelectCommand.CommandText = sqlServiceSub
            da.Fill(ds, "ServiceSub")
            CreateDataTable("ProfileExpand")
            CreateDataTable("TestOrder")
            'แตก Test จาก ProfileTest ลงใน  ProfileExpand data table
            For i = 0 To ds.Tables("ProfileTest").Rows.Count - 1
                ExpandProfile(ds.Tables("ProfileTest").Rows(i).Item("TCode"))
            Next
            For i As Integer = 0 To ds.Tables("ProfileExpand").Rows.Count - 1
                Dim tcode As String = ds.Tables("ProfileExpand").Rows(i).Item("TCode")
                For j As Integer = 0 To ds.Tables("ServiceSub").Rows.Count - 1
                    If ds.Tables("ServiceSub").Rows(j).Item("TCode") = tcode Then
                        ds.Tables("ServiceSub").Rows.RemoveAt(j)
                        Exit For
                    End If
                Next
            Next
            Dim test As String
            For j As Integer = 0 To ds.Tables("ServiceSub").Rows.Count - 1
                test = test + " " + ds.Tables("ServiceSub").Rows(j).Item("TCode")
            Next

            'dsServiceSub = clsConnect.GetDataSet(sqlServiceSub)
            TestOrder.NumberOfTest = ds.Tables("ServiceSub").Rows.Count
            Dim N As Integer = ds.Tables("ServiceSub").Rows.Count
            If TestOrder.NumberOfTest > 0 Then
                ' ReDim TestOrder.PON(N)
                ReDim Universal_Service_ID(N)
                'ReDim TestOrder.PON(N)
                For i As Integer = 0 To N - 1
                    Dim drTestOrder As DataRow
                    drTestOrder = ds.Tables("ServiceSub").Rows(i)
                    'Dim TCodeNumber As String = drTestOrder("TCodeNumber")
                    'If CInt(TCodeNumber) < 10 Then
                    '    TCodeNumber = "00" & TCodeNumber
                    'ElseIf CInt(TCodeNumber) < 100 Then
                    '    TCodeNumber = "0" & TCodeNumber
                    'End If
                    'TestOrder.PON(i) = TCodeNumber & Right(LabNo, 9)
                    Universal_Service_ID(i) = drTestOrder("TCode") & "^^L"
                    Dim updateServiceSub As New ClassUpdateDB
                    updateServiceSub.UpdateServiceSub_IsSendToLIS(LabNo, drTestOrder("TCode"))
                    updateServiceSub.UpdateServiceSub_IsAcceptedSpecimen(LabNo, drTestOrder("TCode"))
                Next
            End If
            da.Dispose()
            command.Dispose()
            conn.Close()
            Return TestOrder

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try


    End Function
    Function IsProfile(ByVal TCode As String) As Boolean
        Dim dt As DataTable
        dt = C1.GetDatatable("SELECT TCode, IsProfile FROM Test WHERE TCode='" & TCode & "'")
        If dt.Rows(0).Item("IsProfile") = False Then
            Return False
        Else
            Return True
        End If
    End Function
    Sub CreateDataTable(ByVal TableName As String)
        Try
            Dim Table As DataTable = New DataTable(TableName)
            Table.Columns.Add("TCode")
            ds.Tables.Add(Table)
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub
    Sub ExpandProfile(ByVal TCode As String)
        Dim dt, dtTest As DataTable
        dtTest = ds.Tables("ProfileExpand")
        dt = C1.GetDatatable("SELECT TCode, Profile FROM ProfileTest WHERE Profile='" & TCode & "'")
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dtTest.Rows.Add(dt.Rows(i).Item("TCode"))
                If IsProfile(dt.Rows(i).Item("TCode")) Then
                    ExpandProfile(dt.Rows(i).Item("TCode"))
                End If
            Next
        Else
            dtTest.Rows.Add(TCode)
        End If
        'For i As Integer = 0 To dtTest.Rows.Count - 1
        '    MsgBox(dtTest.Rows(i).Item("Tcode"))
        'Next
    End Sub
End Class
