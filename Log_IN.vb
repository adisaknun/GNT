Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Globalization
Public Class Log_IN
    Dim C1 As New ClassConnect
    Dim connection As SqlConnection
    Dim command As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As DataSet
    Dim connetionString As String = "Data Source=10.4.11.100;Initial Catalog=LIS;Persist Security Info=True;User ID=lis;Password=opd"
    Dim MessageControlID As String

    Private Sub btnLogIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogIN.Click
        Log_IN()

    End Sub
    Private Sub Log_IN()
        'Check Log IN
        Dim ds As DataSet
        ds = C1.GetDataSet("SELECT * FROM Staff WHERE Account='" & txtUser.Text & "' AND Password='" & txtPassword.Text & "'")
        If ds.Tables(0).Rows.Count <> 0 Then
            MainPrintBarcode.Show()
            'Listening()
        End If
    End Sub

    Sub Listening()
        Dim port As Int32 = 12000
        Dim ipHostInfo As IPHostEntry = Dns.Resolve(Dns.GetHostName())
        Dim IP As String = ipHostInfo.AddressList(0).ToString
        Dim localAddr As IPAddress = IPAddress.Parse(IP)
        Dim serverSocket = New TcpListener(localAddr, port)
        Dim clientSocket As TcpClient
        'serverSocket.Start()
        While (True)
            Try
                serverSocket.Start()
                clientSocket = serverSocket.AcceptTcpClient()
                Dim networkStream As NetworkStream = clientSocket.GetStream()
                Dim bytesFrom(10024) As Byte
                networkStream.Read(bytesFrom, 0, CInt(clientSocket.ReceiveBufferSize))
                Dim dataFromClient As String = System.Text.Encoding.ASCII.GetString(bytesFrom)
                'Dim dataFromClient As String = "MSH|^~\&|LIS|38-1^LIS^LABO|38-1||20170928132016||ORU^R01^ORU_R01|TD0000126479|P|2.3|||AL|NE|||||"

                SaveResult(dataFromClient)
                'TextBox1.Text = TextBox1.Text & vbCrLf & dataFromClient
                Dim serverResponse As String = msgResponse(dataFromClient)
                Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(serverResponse)
                'Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes("00")
                networkStream.Write(sendBytes, 0, sendBytes.Length)
                networkStream.Flush()
            Catch ex As Exception
                MsgBox(ex.ToString)
                'Finally
                '    clientSocket.Close()
                '    serverSocket.Stop()
            End Try
            clientSocket.Close()
            serverSocket.Stop()
        End While
        'clientSocket.Close()
        'serverSocket.Stop()

    End Sub

    Sub SaveResult(ByVal msgResult As String)
        Dim file As New ClassLogFile
        'Dim strResult As String = file.GetFileContents(filename)
        Dim FullAccessNumber, UserValidate, strApprovedTime, strValidateTime As String
        'Dim objReader As StreamReader
        Dim arrayText() As String
        'Dim txt_path As String

        Dim stringSeparators() As String = {Chr(13)}
        Dim arrayResult() As String
        arrayResult = msgResult.Split(stringSeparators, _
                              StringSplitOptions.RemoveEmptyEntries)

        'Clear MessageControlID value
        MessageControlID = Nothing

        'objReader = New StreamReader(filename)
        'While objReader.EndOfStream = False
        For Each value As String In arrayResult
            Dim UserApproved As String
            arrayText = value.Split("|")
            Dim asciis As Byte() = System.Text.Encoding.ASCII.GetBytes(arrayText(0))
            Select Case arrayText(0)
                Case Chr(11) & "MSH"
                    Dim MessageType As String = arrayText(8)
                    MessageControlID = arrayText(9)
                    strValidateTime = arrayText(6)
                    'Dim yyyy, mm, dd, hh, min, ss As String
                    'yyyy = Strings.Left(strValidateTime, 4)
                    'mm = Mid(strValidateTime, 5, 2)
                    'dd = Mid(strValidateTime, 7, 2)
                    'hh = Mid(strValidateTime, 9, 2)
                    'min = Mid(strValidateTime, 11, 2)
                    'ss = Mid(strValidateTime, 13, 2)
                    'strValidateTime = yyyy + "-" + mm + "-" + dd + " " + hh + ":" + min + ":" + ss
                    strValidateTime = ConvertStringToDateFormat(strValidateTime)
                Case "PID"
                Case "NTE"
                Case "PV1"
                Case "ORC"
                    'Dim PlacerGroupNumber As String = arrayText(3)
                    ''FullAccessNumber = PlacerGroupNumber
                    'FullAccessNumber = arrayText(3)
                    'FullAccessNumber = Strings.Left(FullAccessNumber, 10)
                Case "OBR"
                    FullAccessNumber = arrayText(3)
                    FullAccessNumber = Strings.Left(FullAccessNumber, 10)
                    UserValidate = arrayText(32)
                    UserValidate = UserValidate.Substring(0, InStr(UserValidate, "&") - 1)
                    'Update user validate
                    Dim UpdateDB As New ClassUpdateDB
                    UpdateDB.UpdateServiceMain(FullAccessNumber, UserValidate, strValidateTime)
                    strApprovedTime = arrayText(14)
                    strApprovedTime = ConvertStringToDateFormat(strApprovedTime)
                    'Dim yyyy, mm, dd, hh, min, ss As String
                    'yyyy = Strings.Left(strApprovedTime, 4)
                    'mm = Mid(strApprovedTime, 5, 2)
                    'dd = Mid(strApprovedTime, 7, 2)
                    'hh = Mid(strApprovedTime, 9, 2)
                    'min = Mid(strApprovedTime, 11, 2)
                    'ss = Mid(strApprovedTime, 13, 2)
                    'strApprovedTime = yyyy + "-" + mm + "-" + dd + " " + hh + ":" + min + ":" + ss
                    'Dim UserApproved As String
                    UserApproved = arrayText(34)
                Case "OBX"
                    If arrayText(11) = "F" Or arrayText(11) = "C" Then 'Status of result is finish("F")
                        Dim TCode As String = arrayText(3)
                        TCode = TCode.Substring(0, InStr(TCode, "^") - 1)
                        If TestNotExist(TCode) Then
                            Dim TestName As String = TCode.Substring(InStr(TCode, "^") + 1, TCode.Length - InStr(TCode, "^"))
                            AddNewTest(TCode, TestName)
                        End If
                        Dim Result As String = Mid(arrayText(5), InStr(arrayText(5), "^") + 1, Len(arrayText(5)) - InStr(arrayText(5), "&") + 1)
                        Dim Flag As String = arrayText(8)
                        If Flag = "N" Then Flag = Nothing 'Normal result
                        UpdateResult(FullAccessNumber, TCode, Result, Flag, UserApproved, strApprovedTime)
                    End If
            End Select
        Next
        ''Delete file
        'If System.IO.File.Exists(filename) = True Then
        '    objReader.Close()
        '    System.IO.File.Delete(filename)
        'End If
    End Sub

    Function ConvertStringToDateFormat(ByVal strDateTime As String) As String
        Dim yyyy, mm, dd, hh, min, ss As String
        yyyy = Strings.Left(strDateTime, 4)
        mm = Mid(strDateTime, 5, 2)
        dd = Mid(strDateTime, 7, 2)
        hh = Mid(strDateTime, 9, 2)
        min = Mid(strDateTime, 11, 2)
        ss = Mid(strDateTime, 13, 2)
        strDateTime = yyyy + "-" + mm + "-" + dd + " " + hh + ":" + min + ":" + ss
        Return strDateTime
    End Function

    Private Sub UpdateResult(ByVal LabNo As String, ByVal TCode As String, ByVal Result As String, ByVal HL_flag As String, ByVal UserApproved As String, ByVal ApprovedTime As String)
        Dim UpdateDB As New ClassUpdateDB
        UpdateDB.UpdateResult(LabNo, TCode, Result, HL_flag, UserApproved, ApprovedTime)
    End Sub

    Function TestNotExist(ByVal TCode As String) As Boolean
        Dim ds As DataSet
        ds = C1.GetDataSet("SELECT TCode, Test FROM Test WHERE TCode='" & TCode & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            Return False 'Test is exist.
        Else
            Return True
        End If
    End Function

    Sub AddNewTest(ByVal TCode As String, ByVal Test As String)
        'Sub AddNewTest(ByVal TCode As String, ByVal Test As String, ByVal PrintIndex As String, ByVal IsProfile As Boolean, ByVal Department As String, ByVal SubDepartment As String, ByVal ShowInReceive As Boolean, ByVal TestNamePrintResult As String, ByVal IsSendToLIS As Boolean)
        Dim C_AppendNewTest As New ClassInsertDB
        C_AppendNewTest.InsertNewTest(TCode, Test)
    End Sub

    Function msgResponse(ByVal txt As String) As String
        Dim strACK As String
        If InStr(txt, "ORU") > 0 Then
            'Dim MessageControlID As String
            'Dim intControlID_Start, intControlID_End As Integer
            'intControlID_Start = InStr(txt, "ORU_R01|") + 8
            'intControlID_End = InStr(txt, "|P")
            'MessageControlID = Mid(txt, intControlID_Start, intControlID_End - intControlID_Start)
            strACK = "MSH|^~\&|38-1||LIS|38-1|||ACK|A" & MessageControlID & "|P|2.3|" & vbCrLf & "MSA|AA|" & MessageControlID & "|"
            strACK = Chr(11) & strACK & Chr(13) & Chr(28) & Chr(13)
        End If
        If InStr(txt, "ORM") > 0 Then
            Dim MessageControlID As String
            Dim intControlID_Start, intControlID_End As Integer
            intControlID_Start = InStr(txt, "ORM_O01|") + 8
            intControlID_End = InStr(txt, "|P")
            MessageControlID = Mid(txt, intControlID_Start, intControlID_End - intControlID_Start + 1)
            'Dim LIS As New InterfaceLIS
            '"MSH|^~\&|38-1||LIS|38-1|||ACK|TD0001032035|P|2.3|" & vbCrLf & "MSA|AA|TD0001032035|"
            'strACK = "MSH|^~\&|TDR||MU||||ACK|A" & MessageControlID & "|P|2.3|" & vbCrLf & "MSA|AA|TD0001000217|"
            strACK = "MSH|^~\&|38-1||LIS|38-1|20170323143747.39+0700||ACK^R01|202|P|2.3" & vbCrLf & "MSA|AA|TD0000000219"
            strACK = Chr(11) & strACK & Chr(13) & Chr(28) & Chr(13)
        End If
        Return strACK
    End Function
    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyValue = 13 Then
            Log_IN()
        End If

    End Sub


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

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub
End Class