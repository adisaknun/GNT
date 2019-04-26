Imports System.IO
Public Class ClassReadINI
    Public Shared ServerIP As String
    Public Shared Port As String
    Public Shared DayDelete As String
    Public Sub ReadINI()
        Dim App As String = Environment.CurrentDirectory()
        Dim fileName As String = App + "\" + "Setting.ini"
        Dim objReader As StreamReader
        Dim LineOfText As String
        Dim arrayTextFile() As String
        'Dim txt_path As String

        objReader = New StreamReader(fileName)
        While objReader.EndOfStream = False
            LineOfText = objReader.ReadLine
            arrayTextFile = LineOfText.Split
            Select Case arrayTextFile(0)
                Case "Server_IP"
                    ServerIP = arrayTextFile(2)
                Case "Port"
                    Port = arrayTextFile(2)
                Case "Day_Delete"
                    DayDelete = arrayTextFile(2)
            End Select

        End While
    End Sub
End Class
