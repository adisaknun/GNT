Imports System.Runtime.InteropServices

Public Class ClassNetworkDrive
    <DllImportAttribute("mpr.dll", EntryPoint:="WNetAddConnection2W")> _
  Public Shared Function WNetAddConnection2(ByRef lpNetResource _
     As NETRESOURCE, <InAttribute(), _
     MarshalAsAttribute(UnmanagedType.LPWStr)> ByVal _
     lpPassword As String, <InAttribute(), _
     MarshalAsAttribute(UnmanagedType.LPWStr)> ByVal _
     lpUserName As String, ByVal dwFlags As UInteger) As UInteger
    End Function

    <DllImportAttribute("mpr.dll", _
       EntryPoint:="WNetCancelConnectionW")> _
     Public Shared Function WNetCancelConnection(<InAttribute(), _
        MarshalAsAttribute(UnmanagedType.LPWStr)> ByVal _
        lpName As String, ByVal dwFlags As UInteger, _
        <MarshalAsAttribute(UnmanagedType.Bool)> ByVal _
        fForce As Boolean) As UInteger
    End Function
    Public Const NO_ERROR As UInteger = 0
    Public Const RESOURCETYPE_DISK As UInteger = 1
    Public Const CONNECT_UPDATE_PROFILE As UInteger = 1
    <StructLayoutAttribute(LayoutKind.Sequential)> _
    Public Structure NETRESOURCE

        Public dwScope As UInteger
        Public dwType As UInteger
        Public dwDisplayType As UInteger
        Public dwUsage As UInteger

        <MarshalAsAttribute(UnmanagedType.LPWStr)> _
        Public lpLocalName As String

        <MarshalAsAttribute(UnmanagedType.LPWStr)> _
        Public lpRemoteName As String

        <MarshalAsAttribute(UnmanagedType.LPWStr)> _
        Public lpComment As String

        <MarshalAsAttribute(UnmanagedType.LPWStr)> _
        Public lpProvider As String

    End Structure
    Public Shared Sub Map(ByVal strPath As String, _
         ByVal strDrive As Char, ByVal blnPersist As Boolean, _
         Optional ByVal strUser As String = Nothing, _
         Optional ByVal strPassword As String = Nothing)

        Dim nrDrive As New NETRESOURCE

        With nrDrive

            .dwType = RESOURCETYPE_DISK
            .lpLocalName = strDrive & ":"
            .lpRemoteName = strPath

        End With

        Dim uiSet As UInteger = 0

        If blnPersist Then

            uiSet = &H1

        End If

        Dim uiRes As UInteger = WNetAddConnection2(nrDrive, _
           strPassword, strUser, uiSet)

        If Not uiRes = NO_ERROR Then

            Throw New System.ComponentModel.Win32Exception _
               (CInt(uiRes))

        End If

    End Sub
    Public Shared Sub Unmap(ByVal cDrive As Char)

        Dim uiRes As UInteger = WNetCancelConnection(cDrive & ":", _
           CONNECT_UPDATE_PROFILE, True)

        If Not uiRes = NO_ERROR Then

            Throw New System.ComponentModel.Win32Exception _
               (CInt(uiRes))

        End If

    End Sub
End Class
