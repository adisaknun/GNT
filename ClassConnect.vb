Imports System.IO
Imports System.Configuration
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ClassConnect
    Public ConnectStr As String
    Private objConn As SqlConnection
    Private objCmd As SqlCommand
    Public Sub New()
        'Dim settings As New ConnectionStringSettings
        'settings = ConfigurationManager.ConnectionStrings("BookCMS.My.MySettings.CMSLISConnectionString")
        'ConnectStr = "Data Source=TOY-PC\TOY;Initial Catalog=LIS;Persist Security Info=True;User ID=lis;Password=opd"
        ConnectStr = "Data Source=10.4.11.100;Initial Catalog=LIS;Persist Security Info=True;User ID=lis;Password=opd"
        'ConnectStr = "Data Source=ADISAK-PC;Initial Catalog=LIS;Persist Security Info=True;User ID=sa;Password=genius"
    End Sub
    Public Function GetDataSet(ByVal strsql As String, Optional ByVal DatasetName As String = "Dataset1", Optional ByVal TableName As String = "Table1") As DataSet
        Dim Da As New SqlDataAdapter(strsql, ConnectStr)
        'Dim Da As New MySqlDataAdapter(strsql, ConnectStr)
        Dim DS As New DataSet(DatasetName)
        Try
            Da.Fill(DS, TableName)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
            'MsgBox("ไม่สามารถติดต่อ Server ได้")
        End Try
        Return DS
    End Function
    Public Function GetDatatable(ByVal strsql As String, Optional ByVal TableName As String = "Table1") As DataTable
        Dim Da As New SqlDataAdapter(strsql, ConnectStr)
        Dim DT As New DataTable(TableName)
        Try
            Da.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
            'MsgBox("ไม่สามารถติดต่อ Server ได้")
        End Try
        Return DT
    End Function
    Public Function GetDataReader(ByVal strSQL As String) As SqlDataReader
        Dim dtReader As SqlDataReader
        objConn = New SqlConnection
        With objConn
            .ConnectionString = ConnectStr
            .Open()
        End With
        objCmd = New SqlCommand(strSQL, objConn)
        dtReader = objCmd.ExecuteReader()
        Return dtReader '*** Return DataReader ***'
    End Function
    Public Function GetBloodTime(ByVal strsql As String, Optional ByVal DatasetName As String = "Dataset1", Optional ByVal TableName As String = "Table1") As DataSet
        'Dim objConn As MySqlConnection
        'Dim objCmd As MySqlCommand
        Dim strConnString As String
        strConnString = "Server=10.4.11.150;User Id=root; Password=masterkey; Database=gnt_int; Pooling=false"
        'objConn = New MySqlConnection(strConnString)
        objConn.Open()
        'BindData()

        Dim Da As New SqlDataAdapter(strsql, ConnectStr)
        'Dim Da As New MySqlDataAdapter(strsql, ConnectStr)
        Dim DS As New DataSet(DatasetName)
        Try
            Da.Fill(DS, TableName)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
            'MsgBox("ไม่สามารถติดต่อ Server ได้")
        End Try
        Return DS
    End Function
End Class
