<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Log_IN
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Log_IN))
        Me.lblLogIN = New System.Windows.Forms.Label
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblUser = New System.Windows.Forms.Label
        Me.btnLogIN = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lblLogIN
        '
        Me.lblLogIN.AutoSize = True
        Me.lblLogIN.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblLogIN.Location = New System.Drawing.Point(196, 21)
        Me.lblLogIN.Name = "lblLogIN"
        Me.lblLogIN.Size = New System.Drawing.Size(48, 17)
        Me.lblLogIN.TabIndex = 0
        Me.lblLogIN.Text = "Log IN"
        '
        'txtUser
        '
        Me.txtUser.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtUser.Location = New System.Drawing.Point(150, 61)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(159, 24)
        Me.txtUser.TabIndex = 1
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(150, 103)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(120)
        Me.txtPassword.Size = New System.Drawing.Size(159, 24)
        Me.txtPassword.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(78, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Password"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUser.Location = New System.Drawing.Point(81, 68)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(35, 17)
        Me.lblUser.TabIndex = 4
        Me.lblUser.Text = "User"
        '
        'btnLogIN
        '
        Me.btnLogIN.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnLogIN.Location = New System.Drawing.Point(182, 151)
        Me.btnLogIN.Name = "btnLogIN"
        Me.btnLogIN.Size = New System.Drawing.Size(75, 29)
        Me.btnLogIN.TabIndex = 5
        Me.btnLogIN.Text = "Log in"
        Me.btnLogIN.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(360, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 13)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "V 3.0"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Log_IN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 221)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnLogIN)
        Me.Controls.Add(Me.lblUser)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.lblLogIN)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Log_IN"
        Me.Text = "LogIN"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLogIN As System.Windows.Forms.Label
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents btnLogIN As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
