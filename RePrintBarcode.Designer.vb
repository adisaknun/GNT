<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RePrintBarcode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RePrintBarcode))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtLabNo = New System.Windows.Forms.TextBox
        Me.lblFullName = New System.Windows.Forms.Label
        Me.lblGender = New System.Windows.Forms.Label
        Me.lblAge = New System.Windows.Forms.Label
        Me.lblBarcode = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnPrint = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.chkHaveSp = New System.Windows.Forms.CheckBox
        Me.lblAcceptNo = New System.Windows.Forms.Label
        Me.ListView3 = New System.Windows.Forms.ListView
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ListView2 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.Tcode = New System.Windows.Forms.ColumnHeader
        Me.TestName = New System.Windows.Forms.ColumnHeader
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblExtraBarcode = New System.Windows.Forms.Label
        Me.lblTubeBule = New System.Windows.Forms.Label
        Me.lblTubeGreen = New System.Windows.Forms.Label
        Me.lblTubePurple = New System.Windows.Forms.Label
        Me.lblTubeRed = New System.Windows.Forms.Label
        Me.picTube_Bule = New System.Windows.Forms.PictureBox
        Me.picTube_Green = New System.Windows.Forms.PictureBox
        Me.picTube_Purple = New System.Windows.Forms.PictureBox
        Me.picTube_Red = New System.Windows.Forms.PictureBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblTelephone = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTube_Bule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTube_Green, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTube_Purple, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTube_Red, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "LabNo"
        '
        'txtLabNo
        '
        Me.txtLabNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtLabNo.Location = New System.Drawing.Point(129, 23)
        Me.txtLabNo.Name = "txtLabNo"
        Me.txtLabNo.Size = New System.Drawing.Size(213, 26)
        Me.txtLabNo.TabIndex = 1
        '
        'lblFullName
        '
        Me.lblFullName.AutoSize = True
        Me.lblFullName.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblFullName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblFullName.Location = New System.Drawing.Point(124, 110)
        Me.lblFullName.Name = "lblFullName"
        Me.lblFullName.Size = New System.Drawing.Size(26, 26)
        Me.lblFullName.TabIndex = 2
        Me.lblFullName.Text = "--"
        '
        'lblGender
        '
        Me.lblGender.AutoSize = True
        Me.lblGender.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblGender.Location = New System.Drawing.Point(232, 154)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(26, 26)
        Me.lblGender.TabIndex = 3
        Me.lblGender.Text = "--"
        '
        'lblAge
        '
        Me.lblAge.AutoSize = True
        Me.lblAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAge.Location = New System.Drawing.Point(63, 154)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(26, 26)
        Me.lblAge.TabIndex = 4
        Me.lblAge.Text = "--"
        '
        'lblBarcode
        '
        Me.lblBarcode.AutoSize = True
        Me.lblBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblBarcode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblBarcode.Location = New System.Drawing.Point(124, 65)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(26, 26)
        Me.lblBarcode.TabIndex = 5
        Me.lblBarcode.Text = "--"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Barcode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(25, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "อายุ"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.Location = New System.Drawing.Point(192, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 20)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "เพศ"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(54, 276)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(185, 39)
        Me.btnPrint.TabIndex = 9
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 20)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ชื่อ-นามสกุล"
        '
        'chkHaveSp
        '
        Me.chkHaveSp.AutoSize = True
        Me.chkHaveSp.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.chkHaveSp.ForeColor = System.Drawing.Color.Red
        Me.chkHaveSp.Location = New System.Drawing.Point(86, 231)
        Me.chkHaveSp.Name = "chkHaveSp"
        Me.chkHaveSp.Size = New System.Drawing.Size(108, 23)
        Me.chkHaveSp.TabIndex = 27
        Me.chkHaveSp.Text = "มีสิ่งส่งตรวจ"
        Me.chkHaveSp.UseVisualStyleBackColor = True
        '
        'lblAcceptNo
        '
        Me.lblAcceptNo.AutoSize = True
        Me.lblAcceptNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblAcceptNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAcceptNo.Location = New System.Drawing.Point(358, 289)
        Me.lblAcceptNo.Name = "lblAcceptNo"
        Me.lblAcceptNo.Size = New System.Drawing.Size(26, 26)
        Me.lblAcceptNo.TabIndex = 28
        Me.lblAcceptNo.Text = "--"
        '
        'ListView3
        '
        Me.ListView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.ListView3.Location = New System.Drawing.Point(785, 23)
        Me.ListView3.Name = "ListView3"
        Me.ListView3.Size = New System.Drawing.Size(205, 185)
        Me.ListView3.TabIndex = 37
        Me.ListView3.UseCompatibleStateImageBehavior = False
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Tcode"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "TestName"
        Me.ColumnHeader4.Width = 255
        '
        'ListView2
        '
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView2.Location = New System.Drawing.Point(574, 23)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(205, 185)
        Me.ListView2.TabIndex = 36
        Me.ListView2.UseCompatibleStateImageBehavior = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Tcode"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "TestName"
        Me.ColumnHeader2.Width = 255
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Tcode, Me.TestName})
        Me.ListView1.Location = New System.Drawing.Point(363, 23)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(205, 185)
        Me.ListView1.TabIndex = 35
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Tcode
        '
        Me.Tcode.Text = "Tcode"
        '
        'TestName
        '
        Me.TestName.Text = "TestName"
        Me.TestName.Width = 255
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(724, 259)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(84, 56)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 60
        Me.PictureBox1.TabStop = False
        '
        'lblExtraBarcode
        '
        Me.lblExtraBarcode.AutoSize = True
        Me.lblExtraBarcode.BackColor = System.Drawing.Color.Transparent
        Me.lblExtraBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblExtraBarcode.ForeColor = System.Drawing.Color.Maroon
        Me.lblExtraBarcode.Location = New System.Drawing.Point(754, 318)
        Me.lblExtraBarcode.Name = "lblExtraBarcode"
        Me.lblExtraBarcode.Size = New System.Drawing.Size(24, 26)
        Me.lblExtraBarcode.TabIndex = 59
        Me.lblExtraBarcode.Text = "0"
        '
        'lblTubeBule
        '
        Me.lblTubeBule.AutoSize = True
        Me.lblTubeBule.BackColor = System.Drawing.Color.Transparent
        Me.lblTubeBule.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTubeBule.ForeColor = System.Drawing.Color.Maroon
        Me.lblTubeBule.Location = New System.Drawing.Point(678, 318)
        Me.lblTubeBule.Name = "lblTubeBule"
        Me.lblTubeBule.Size = New System.Drawing.Size(24, 26)
        Me.lblTubeBule.TabIndex = 58
        Me.lblTubeBule.Text = "0"
        '
        'lblTubeGreen
        '
        Me.lblTubeGreen.AutoSize = True
        Me.lblTubeGreen.BackColor = System.Drawing.Color.Transparent
        Me.lblTubeGreen.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTubeGreen.ForeColor = System.Drawing.Color.Maroon
        Me.lblTubeGreen.Location = New System.Drawing.Point(634, 318)
        Me.lblTubeGreen.Name = "lblTubeGreen"
        Me.lblTubeGreen.Size = New System.Drawing.Size(24, 26)
        Me.lblTubeGreen.TabIndex = 57
        Me.lblTubeGreen.Text = "0"
        '
        'lblTubePurple
        '
        Me.lblTubePurple.AutoSize = True
        Me.lblTubePurple.BackColor = System.Drawing.Color.Transparent
        Me.lblTubePurple.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTubePurple.ForeColor = System.Drawing.Color.Maroon
        Me.lblTubePurple.Location = New System.Drawing.Point(591, 318)
        Me.lblTubePurple.Name = "lblTubePurple"
        Me.lblTubePurple.Size = New System.Drawing.Size(24, 26)
        Me.lblTubePurple.TabIndex = 56
        Me.lblTubePurple.Text = "0"
        '
        'lblTubeRed
        '
        Me.lblTubeRed.AutoSize = True
        Me.lblTubeRed.BackColor = System.Drawing.Color.Transparent
        Me.lblTubeRed.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTubeRed.ForeColor = System.Drawing.Color.Maroon
        Me.lblTubeRed.Location = New System.Drawing.Point(547, 318)
        Me.lblTubeRed.Name = "lblTubeRed"
        Me.lblTubeRed.Size = New System.Drawing.Size(24, 26)
        Me.lblTubeRed.TabIndex = 55
        Me.lblTubeRed.Text = "0"
        '
        'picTube_Bule
        '
        Me.picTube_Bule.Image = CType(resources.GetObject("picTube_Bule.Image"), System.Drawing.Image)
        Me.picTube_Bule.Location = New System.Drawing.Point(680, 231)
        Me.picTube_Bule.Name = "picTube_Bule"
        Me.picTube_Bule.Size = New System.Drawing.Size(23, 84)
        Me.picTube_Bule.TabIndex = 54
        Me.picTube_Bule.TabStop = False
        '
        'picTube_Green
        '
        Me.picTube_Green.Image = CType(resources.GetObject("picTube_Green.Image"), System.Drawing.Image)
        Me.picTube_Green.Location = New System.Drawing.Point(635, 231)
        Me.picTube_Green.Name = "picTube_Green"
        Me.picTube_Green.Size = New System.Drawing.Size(24, 84)
        Me.picTube_Green.TabIndex = 53
        Me.picTube_Green.TabStop = False
        '
        'picTube_Purple
        '
        Me.picTube_Purple.Image = CType(resources.GetObject("picTube_Purple.Image"), System.Drawing.Image)
        Me.picTube_Purple.Location = New System.Drawing.Point(593, 231)
        Me.picTube_Purple.Name = "picTube_Purple"
        Me.picTube_Purple.Size = New System.Drawing.Size(20, 84)
        Me.picTube_Purple.TabIndex = 52
        Me.picTube_Purple.TabStop = False
        '
        'picTube_Red
        '
        Me.picTube_Red.Image = CType(resources.GetObject("picTube_Red.Image"), System.Drawing.Image)
        Me.picTube_Red.Location = New System.Drawing.Point(549, 231)
        Me.picTube_Red.Name = "picTube_Red"
        Me.picTube_Red.Size = New System.Drawing.Size(26, 84)
        Me.picTube_Red.TabIndex = 51
        Me.picTube_Red.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label6.Location = New System.Drawing.Point(25, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 20)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "โทร"
        '
        'lblTelephone
        '
        Me.lblTelephone.AutoSize = True
        Me.lblTelephone.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTelephone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTelephone.Location = New System.Drawing.Point(64, 192)
        Me.lblTelephone.Name = "lblTelephone"
        Me.lblTelephone.Size = New System.Drawing.Size(26, 26)
        Me.lblTelephone.TabIndex = 62
        Me.lblTelephone.Text = "--"
        '
        'RePrintBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1021, 386)
        Me.Controls.Add(Me.lblTelephone)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblExtraBarcode)
        Me.Controls.Add(Me.lblTubeBule)
        Me.Controls.Add(Me.lblTubeGreen)
        Me.Controls.Add(Me.lblTubePurple)
        Me.Controls.Add(Me.lblTubeRed)
        Me.Controls.Add(Me.picTube_Bule)
        Me.Controls.Add(Me.picTube_Green)
        Me.Controls.Add(Me.picTube_Purple)
        Me.Controls.Add(Me.picTube_Red)
        Me.Controls.Add(Me.ListView3)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.lblAcceptNo)
        Me.Controls.Add(Me.chkHaveSp)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblBarcode)
        Me.Controls.Add(Me.lblAge)
        Me.Controls.Add(Me.lblGender)
        Me.Controls.Add(Me.lblFullName)
        Me.Controls.Add(Me.txtLabNo)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "RePrintBarcode"
        Me.Text = "RePrintBarcode"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTube_Bule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTube_Green, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTube_Purple, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTube_Red, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLabNo As System.Windows.Forms.TextBox
    Friend WithEvents lblFullName As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents lblBarcode As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkHaveSp As System.Windows.Forms.CheckBox
    Friend WithEvents lblAcceptNo As System.Windows.Forms.Label
    Friend WithEvents ListView3 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Tcode As System.Windows.Forms.ColumnHeader
    Friend WithEvents TestName As System.Windows.Forms.ColumnHeader
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblExtraBarcode As System.Windows.Forms.Label
    Friend WithEvents lblTubeBule As System.Windows.Forms.Label
    Friend WithEvents lblTubeGreen As System.Windows.Forms.Label
    Friend WithEvents lblTubePurple As System.Windows.Forms.Label
    Friend WithEvents lblTubeRed As System.Windows.Forms.Label
    Friend WithEvents picTube_Bule As System.Windows.Forms.PictureBox
    Friend WithEvents picTube_Green As System.Windows.Forms.PictureBox
    Friend WithEvents picTube_Purple As System.Windows.Forms.PictureBox
    Friend WithEvents picTube_Red As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblTelephone As System.Windows.Forms.Label
End Class
