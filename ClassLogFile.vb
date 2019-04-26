Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic

'Imports System
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports Microsoft.VisualBasic
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.ConstrainedExecution
Imports System.Security
Public Class ClassLogFile
    Dim strDateNow As String
    Dim PathFile As String
    Public Function LogFile(ByVal str As String)
        Try
            DeleteFile()
            strDateNow = "_" & Format(Now.Day, "00") & "_" & Format(Now.Month, "00") & "_" & Now.Year
            PathFile = System.Environment.CurrentDirectory & "\LogFile\Log" & strDateNow & ".txt"
            'เปิด File เก่า
            Dim Old_Text As String
            Old_Text = GetFileContents(PathFile)
            Dim TextFile As New StreamWriter(PathFile)
            'เขียน File ให้เหมือนเก่า
            TextFile.WriteLine(Old_Text)
            'เขียน ข้อมูลใหม่ ลงไป
            TextFile.WriteLine("===============" & Now() & "======================")
            TextFile.WriteLine(str)
            TextFile.WriteLine("===================================================")
            TextFile.WriteLine("    ")
            TextFile.Close()
        Catch ex As Exception

        End Try

    End Function
    Public Function GetFileContents(ByVal FullPath As String) As String
        Dim strContents As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            Dim TextFile As New StreamWriter(PathFile)
            TextFile.WriteLine("    ")
            TextFile.Close()
        End Try
    End Function
    Private Sub DeleteFile()
        Dim DDel As Date
        DDel = Now
        DDel = DDel.AddDays(-7)
        Dim strDelDate As String
        strDelDate = "_" & Format(DDel.Day, "00") & "_" & Format(DDel.Month, "00") & "_" & DDel.Year
        Dim PahtFileDelete As String
        PahtFileDelete = System.Environment.CurrentDirectory & "\LogFile\Log" & strDelDate & ".txt"
        If System.IO.File.Exists(PahtFileDelete) = True Then
            System.IO.File.Delete(PahtFileDelete)
        End If
    End Sub
    Public Sub CreateORM_File(ByVal strOrderMSG As String, ByVal FileName As String)
        Try
            'DeleteFile()
            'strDateNow = "_" & Format(Now.Day, "00") & "_" & Format(Now.Month, "00") & "_" & Now.Year
            'PathFile = System.Environment.CurrentDirectory & "\LogFile\Log" & strDateNow & ".txt"
            PathFile = "C:\" & FileName & ".orm"
            ''เปิด File เก่า
            'Dim Old_Text As String
            'Old_Text = GetFileContents(PathFile)
            'Dim TextFile As New StreamWriter(PathFile)



            'ServerLogon()
            ' create .ORM file encoding with no BOM 
            'Dim outputEnc As Encoding = New UTF8Encoding(False)

            ' open file with encoding write data here
            Dim fileORM As TextWriter = New StreamWriter(PathFile, False, System.Text.Encoding.GetEncoding("TIS-620"))
            fileORM.Write(strOrderMSG)
            ' save and close it
            fileORM.Close()

            ' create .OK file encoding with no BOM 
            PathFile = "C:\" & FileName & ".ok"
            ' open file with encoding write data here
            Dim fileOK As TextWriter = New StreamWriter(PathFile, False, System.Text.Encoding.GetEncoding("TIS-620"))
            fileOK.Write(strOrderMSG)
            ' save and close it
            fileOK.Close()

            ''เขียน File ให้เหมือนเก่า
            'TextFile.WriteLine(Old_Text)
            ''เขียน ข้อมูลใหม่ ลงไป
            'TextFile.WriteLine("===============" & Now() & "======================")
            'TextFile.WriteLine(Str)
            'TextFile.WriteLine("===================================================")
            'TextFile.WriteLine("    ")
            'TextFile.Close()
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try

    End Sub
    
    Public Function CreateOK_File(ByVal MSG_ControlID As String, ByVal FileName As String)
        'Dim response As String = "MSH|^~\&|38-1||LIS|38-1|20170323143747.39+0700||ACK^R01|202|P|2.3" & vbCrLf & "MSA|AA|" & MSG_ControlID
        Dim HL7 As New HL7_Send
        Dim MSH As String = HL7.MSH("MU", "", "TD", "", "", "", "", MSG_ControlID, "P", "2.3")
        'Dim MSA As String = HL7.MSA("AA", MSG_ControlID, "", "", "", "")
        Dim MSA As String = HL7.MSA("AA", MSG_ControlID)
        Dim ACK_MSG As String = MSH & vbCrLf & MSA
        PathFile = "C:\" & FileName & ".ok"
        ' open file with encoding write data here
        Dim fileOK As TextWriter = New StreamWriter(PathFile, False, System.Text.Encoding.GetEncoding("TIS-620"))
        fileOK.Write(ACK_MSG)
        ' save and close it
        fileOK.Close()
    End Function
    'Sub ServerLogin()
    'Private declare Auto Function LogonUser Lib "advapi32.dll" (ByVal un As String, ByVal domain As String, ByVal pw As String, ByVal LogonType As Integer, ByVal LogonProvider As Integer,  ByRef Token As IntPtr) As Boolean

    'Public Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean

    '    Dim tokenHandle As New IntPtr(0)
    '    Try
    '        If LogonUser("un", "DOMAINNAME", "pw", 2, 0, tokenHandle) Then
    '            Dim newId As New WindowsIdentity(tokenHandle)
    '            Using impersonatedUser As WindowsImpersonationContext = newId.Impersonate()
    '                'perform impersonated commands
    '                System.IO.File.WriteAllText("C:ttestimp.txt", "test")
    '            End Using
    '            CloseHandle(tokenHandle)
    '        Else
    '            'logon failed
    '        End If
    '    Catch ex As Exception
    '        'exception
    '    End Try
    'End Sub
End Class
