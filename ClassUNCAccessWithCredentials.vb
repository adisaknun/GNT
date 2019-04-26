Imports System.Runtime.InteropServices
Imports BOOL = System.Boolean
Imports DWORD = System.UInt32
Imports LPWSTR = System.String
Imports NET_API_STATUS = System.UInt32

'Namespace ConnectUNCWithCredentials
Public Class UNCAccessWithCredentials
    Implements IDisposable
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Friend Structure USE_INFO_2
        Friend ui2_local As LPWSTR
        Friend ui2_remote As LPWSTR
        Friend ui2_password As LPWSTR
        Friend ui2_status As DWORD
        Friend ui2_asg_type As DWORD
        Friend ui2_refcount As DWORD
        Friend ui2_usecount As DWORD
        Friend ui2_username As LPWSTR
        Friend ui2_domainname As LPWSTR
    End Structure

    <DllImport("NetApi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Friend Shared Function NetUseAdd(ByVal UncServerName As LPWSTR, ByVal Level As DWORD, ByRef Buf As USE_INFO_2, ByRef ParmError As DWORD) As NET_API_STATUS
    End Function

    <DllImport("NetApi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Friend Shared Function NetUseDel(ByVal UncServerName As LPWSTR, ByVal UseName As LPWSTR, ByVal ForceCond As DWORD) As NET_API_STATUS
    End Function

    Private disposed As Boolean = False

    Private sUNCPath As String
    Private sUser As String
    Private sPassword As String
    Private sDomain As String
    Private iLastError As Integer

    ''' <summary>
    ''' A disposeable class that allows access to a UNC resource with credentials.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' The last system error code returned from NetUseAdd or NetUseDel.  Success = 0
    ''' </summary>
    Public ReadOnly Property LastError() As Integer
        Get
            Return iLastError
        End Get
    End Property

    Public Sub Dispose() Implements IDisposable.Dispose
        If Not Me.disposed Then
            NetUseDelete()
        End If
        disposed = True
        GC.SuppressFinalize(Me)
    End Sub

    ''' <summary>
    ''' Connects to a UNC path using the credentials supplied.
    ''' </summary>
    ''' <param name="UNCPath">Fully qualified domain name UNC path</param>
    ''' <param name="User">A user with sufficient rights to access the path.</param>
    ''' <param name="Domain">Domain of User.</param>
    ''' <param name="Password">Password of User</param>
    ''' <returns>True if mapping succeeds.  Use LastError to get the system error code.</returns>
    Public Function NetUseWithCredentials(ByVal UNCPath As String, ByVal User As String, ByVal Domain As String, ByVal Password As String) As Boolean
        sUNCPath = UNCPath
        sUser = User
        sPassword = Password
        sDomain = Domain
        Return NetUseWithCredentials()
    End Function

    Private Function NetUseWithCredentials() As Boolean
        Dim returncode As UInteger
        Try
            Dim useinfo As New USE_INFO_2()

            useinfo.ui2_remote = sUNCPath
            useinfo.ui2_username = sUser
            useinfo.ui2_domainname = sDomain
            useinfo.ui2_password = sPassword
            useinfo.ui2_asg_type = 0
            useinfo.ui2_usecount = 1
            Dim paramErrorIndex As UInteger
            returncode = NetUseAdd(Nothing, 2, useinfo, paramErrorIndex)
            iLastError = CInt(returncode)
            Return returncode = 0
        Catch
            iLastError = Marshal.GetLastWin32Error()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Ends the connection to the remote resource 
    ''' </summary>
    ''' <returns>True if it succeeds.  Use LastError to get the system error code</returns>
    Public Function NetUseDelete() As Boolean
        Dim returncode As UInteger
        Try
            returncode = NetUseDel(Nothing, sUNCPath, 2)
            iLastError = CInt(returncode)
            Return (returncode = 0)
        Catch
            iLastError = Marshal.GetLastWin32Error()
            Return False
        End Try
    End Function

    Protected Overrides Sub Finalize()
        Try
            Dispose()
        Finally
            MyBase.Finalize()
        End Try
    End Sub

End Class
'End Namespace
