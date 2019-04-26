Namespace ConnectUNCWithCredentials
    Partial Class TestConnect
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.btnConnect = New System.Windows.Forms.Button
            Me.tbPassword = New System.Windows.Forms.TextBox
            Me.label4 = New System.Windows.Forms.Label
            Me.tbDomain = New System.Windows.Forms.TextBox
            Me.label3 = New System.Windows.Forms.Label
            Me.tbUserName = New System.Windows.Forms.TextBox
            Me.label2 = New System.Windows.Forms.Label
            Me.label1 = New System.Windows.Forms.Label
            Me.tbUNCPath = New System.Windows.Forms.TextBox
            Me.tbDirList = New System.Windows.Forms.TextBox
            Me.Button1 = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'btnConnect
            '
            Me.btnConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnConnect.Location = New System.Drawing.Point(405, 88)
            Me.btnConnect.Name = "btnConnect"
            Me.btnConnect.Size = New System.Drawing.Size(75, 23)
            Me.btnConnect.TabIndex = 14
            Me.btnConnect.Text = "&Connect"
            Me.btnConnect.UseVisualStyleBackColor = True
            '
            'tbPassword
            '
            Me.tbPassword.Location = New System.Drawing.Point(74, 38)
            Me.tbPassword.Name = "tbPassword"
            Me.tbPassword.Size = New System.Drawing.Size(157, 20)
            Me.tbPassword.TabIndex = 9
            Me.tbPassword.UseSystemPasswordChar = True
            '
            'label4
            '
            Me.label4.AutoSize = True
            Me.label4.Location = New System.Drawing.Point(7, 41)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(56, 13)
            Me.label4.TabIndex = 17
            Me.label4.Text = "Password:"
            '
            'tbDomain
            '
            Me.tbDomain.Location = New System.Drawing.Point(74, 64)
            Me.tbDomain.Name = "tbDomain"
            Me.tbDomain.Size = New System.Drawing.Size(157, 20)
            Me.tbDomain.TabIndex = 11
            '
            'label3
            '
            Me.label3.AutoSize = True
            Me.label3.Location = New System.Drawing.Point(7, 67)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(46, 13)
            Me.label3.TabIndex = 16
            Me.label3.Text = "Domain:"
            '
            'tbUserName
            '
            Me.tbUserName.Location = New System.Drawing.Point(74, 12)
            Me.tbUserName.Name = "tbUserName"
            Me.tbUserName.Size = New System.Drawing.Size(157, 20)
            Me.tbUserName.TabIndex = 8
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(7, 15)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(63, 13)
            Me.label2.TabIndex = 12
            Me.label2.Text = "User Name:"
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(10, 93)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(58, 13)
            Me.label1.TabIndex = 10
            Me.label1.Text = "UNC Path:"
            '
            'tbUNCPath
            '
            Me.tbUNCPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbUNCPath.Location = New System.Drawing.Point(74, 90)
            Me.tbUNCPath.Name = "tbUNCPath"
            Me.tbUNCPath.Size = New System.Drawing.Size(325, 20)
            Me.tbUNCPath.TabIndex = 13
            '
            'tbDirList
            '
            Me.tbDirList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbDirList.Location = New System.Drawing.Point(10, 117)
            Me.tbDirList.Multiline = True
            Me.tbDirList.Name = "tbDirList"
            Me.tbDirList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.tbDirList.Size = New System.Drawing.Size(470, 187)
            Me.tbDirList.TabIndex = 15
            Me.tbDirList.WordWrap = False
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(302, 8)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 18
            Me.Button1.Text = "Button1"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'TestConnect
            '
            Me.AcceptButton = Me.btnConnect
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(492, 316)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.btnConnect)
            Me.Controls.Add(Me.tbPassword)
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.tbDomain)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.tbUserName)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.tbUNCPath)
            Me.Controls.Add(Me.tbDirList)
            Me.Name = "TestConnect"
            Me.Text = "Connect to UNC Path with Credentials"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private WithEvents btnConnect As System.Windows.Forms.Button
        Private tbPassword As System.Windows.Forms.TextBox
        Private label4 As System.Windows.Forms.Label
        Private tbDomain As System.Windows.Forms.TextBox
        Private label3 As System.Windows.Forms.Label
        Private tbUserName As System.Windows.Forms.TextBox
        Private label2 As System.Windows.Forms.Label
        Private label1 As System.Windows.Forms.Label
        Private tbUNCPath As System.Windows.Forms.TextBox
        Private tbDirList As System.Windows.Forms.TextBox
        Friend WithEvents Button1 As System.Windows.Forms.Button
    End Class
End Namespace
