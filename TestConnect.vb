Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Namespace ConnectUNCWithCredentials
    Partial Public Class TestConnect
        Inherits Form
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim UNCPath As String = "\\10.51.101.6\ORU-F-SII"
            Dim dirs As String()
            ' Process the list of .txt files found in the directory. '
            Dim fileName As String


            Using unc As New UNCAccessWithCredentials()
                If unc.NetUseWithCredentials(UNCPath, "HIS", "TDNL.local", "MedTech@1") Then
                    dirs = Directory.GetDirectories(UNCPath)
                    'For Each d As String In dirs
                    '    tbDirList.Text += d & vbCr & vbLf
                    'Next
                    Dim ASTfiles As String() = Directory.GetFiles("\\10.51.101.6\ORU-F-SII\Output\SII", "*.ast")
                    For Each fileName In ASTfiles
                        If (System.IO.File.Exists(fileName)) Then
                            tbDirList.Text += fileName & vbCr & vbLf
                            'Read File and Print Result if its true
                        End If
                    Next

                Else
                    Me.Cursor = Cursors.[Default]
                    MessageBox.Show("Failed to connect to " + tbUNCPath.Text & vbCr & vbLf & "LastError = " & unc.LastError.ToString(), "Failed to connect", MessageBoxButtons.OK, MessageBoxIcon.[Error])

                End If
            End Using
            Me.Cursor = Cursors.[Default]
        End Sub

        
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        End Sub
    End Class
End Namespace
