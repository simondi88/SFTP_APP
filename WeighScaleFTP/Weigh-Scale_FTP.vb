'Imports FTP
Imports WeighScaleFTP
Imports System
Imports System.IO
Imports WinSCP

Public Class frm_WS_FTP
    Inherits System.Windows.Forms.Form
    Dim Env_1 As String
    Dim Env_long As String
    Dim Datenow As String
    Dim WorkDir As String
    Dim FTP_ok As Boolean
    Const csvFileType As String = "ACCPAC Comma Separated Value Files (*.csv)|*.csv"
    Const Server As String = "bcsc01.gov.bc.ca"  '"142.34.36.41"
    Const BillMarsh As String = "SC68496"
    Const GrahamHayes As String = "PC76967"
    Const TabithaGarcia As String = "SC46300"
    'added by Joe Jaffey
    Const JoeJaffey As String = "SC75905"
    Dim ddtt As String = System.DateTime.Now.ToString("yyyy-MM-dd_HHmmss")
    Dim dupe_temp_file As String = "weigh-scale-temp" + ddtt + ".txt"
    Const Hdr_ID As String = "RECTYPE"
    Const ChargeFileName As String = ".d01conv.charge.csv"     ' Simon Di removed ' in '"
    Const ChargeFileName427 As String = ".d01c427.charge.csv"   ' Simon Di removed ' in '"
    Const PayFileName As String = ".d01conv.payment.csv"        ' Simon Di removed ' in '"
    Const PayFileName427 As String = ".d01c427.payment.csv"     ' Simon Di removed ' in '"
    Const InterestFileName As String = ".m01ftp.interest.csv"   ' Simon Di removed ' in '"
    Const HLQ As String = "CTMS"
    Const DailyInvPrefix As String = "Daily_Invoices_"
    Const DailyInvPrefix427 As String = "Daily_Invcs427_"
    Const DailyRecPrefix As String = "Daily_Receipts_"
    Const DailyRecPrefix427 As String = "Daily_Recps427_"
    Const MonthlyPrefix As String = "Monthly_Invoices_"
    Const csvSuffix As String = ".csv"
    Const BaseDirTest As String = "c:\ACCPAC_FTP\"
    Const BaseDirProd As String = "G:\WEIGH SCALES\"
    Const WorkDirSuffix As String = " Data\"


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnCharge As System.Windows.Forms.Button
    Friend WithEvents btnPayment As System.Windows.Forms.Button
    Friend WithEvents rdo_Prod As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_Test As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_Devl As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_Monthly As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_Daily As System.Windows.Forms.RadioButton
    Friend WithEvents btn_Transfer As System.Windows.Forms.Button
    Friend WithEvents lbl_warn As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents PnlDaily As System.Windows.Forms.Panel
    Friend WithEvents lbl_dly_chg_mf As System.Windows.Forms.Label
    Friend WithEvents btn_dly_chg_open As System.Windows.Forms.Button
    Friend WithEvents btn_dly_pay_open As System.Windows.Forms.Button
    Friend WithEvents lbl_dly_chg_pc As System.Windows.Forms.Label
    Friend WithEvents lbl_dly_pay_mf As System.Windows.Forms.Label
    Friend WithEvents lbl_dly_pay_pc As System.Windows.Forms.Label
    Friend WithEvents PnlMonthly As System.Windows.Forms.Panel
    Friend WithEvents lbl_mth_chg_mf As System.Windows.Forms.Label
    Friend WithEvents btn_mth_chg_save As System.Windows.Forms.Button
    Friend WithEvents lbl_mth_chg_pc As System.Windows.Forms.Label
    Friend WithEvents txtUserid As System.Windows.Forms.TextBox
    Friend WithEvents txtPW As System.Windows.Forms.TextBox
    Friend WithEvents gbxXferType As System.Windows.Forms.GroupBox
    Friend WithEvents lblMonthlyCharges As System.Windows.Forms.Label
    Friend WithEvents lblMonthlyChgTo As System.Windows.Forms.Label
    Friend WithEvents lblDailyCharges As System.Windows.Forms.Label
    Friend WithEvents lblDailyChgFrom As System.Windows.Forms.Label
    Friend WithEvents lblDailyPayFrom As System.Windows.Forms.Label
    Friend WithEvents lblDailyPay As System.Windows.Forms.Label
    Friend WithEvents gbxEnv As System.Windows.Forms.GroupBox
    Friend WithEvents gbxRACF As System.Windows.Forms.GroupBox
    Friend WithEvents lblUserid As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblDailyPayTo As System.Windows.Forms.Label
    Friend WithEvents lblMonthlyChgFrom As System.Windows.Forms.Label
    Friend WithEvents lblDailyChgTo As System.Windows.Forms.Label
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lbl_dly_chg_mf_427 As System.Windows.Forms.Label
    Friend WithEvents lbl_dly_chg_pc_427 As System.Windows.Forms.Label
    Friend WithEvents lbl_dly_pay_mf_427 As System.Windows.Forms.Label
    Friend WithEvents lbl_dly_pay_pc_427 As System.Windows.Forms.Label
    Friend WithEvents btn_dly_chg_427_open As System.Windows.Forms.Button
    Friend WithEvents btn_dly_pay_427_open As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.gbxXferType = New System.Windows.Forms.GroupBox()
        Me.PnlMonthly = New System.Windows.Forms.Panel()
        Me.lbl_mth_chg_mf = New System.Windows.Forms.Label()
        Me.btn_mth_chg_save = New System.Windows.Forms.Button()
        Me.lblMonthlyCharges = New System.Windows.Forms.Label()
        Me.lblMonthlyChgTo = New System.Windows.Forms.Label()
        Me.lbl_mth_chg_pc = New System.Windows.Forms.Label()
        Me.lblMonthlyChgFrom = New System.Windows.Forms.Label()
        Me.PnlDaily = New System.Windows.Forms.Panel()
        Me.lbl_dly_chg_mf = New System.Windows.Forms.Label()
        Me.btn_dly_chg_open = New System.Windows.Forms.Button()
        Me.lblDailyCharges = New System.Windows.Forms.Label()
        Me.lblDailyChgFrom = New System.Windows.Forms.Label()
        Me.lblDailyPayFrom = New System.Windows.Forms.Label()
        Me.lblDailyPay = New System.Windows.Forms.Label()
        Me.btn_dly_pay_open = New System.Windows.Forms.Button()
        Me.lbl_dly_chg_pc = New System.Windows.Forms.Label()
        Me.lbl_dly_pay_mf = New System.Windows.Forms.Label()
        Me.lbl_dly_pay_pc = New System.Windows.Forms.Label()
        Me.lblDailyPayTo = New System.Windows.Forms.Label()
        Me.lblDailyChgTo = New System.Windows.Forms.Label()
        Me.rdo_Monthly = New System.Windows.Forms.RadioButton()
        Me.rdo_Daily = New System.Windows.Forms.RadioButton()
        Me.btn_dly_pay_427_open = New System.Windows.Forms.Button()
        Me.btn_dly_chg_427_open = New System.Windows.Forms.Button()
        Me.lbl_dly_pay_pc_427 = New System.Windows.Forms.Label()
        Me.lbl_dly_pay_mf_427 = New System.Windows.Forms.Label()
        Me.lbl_dly_chg_pc_427 = New System.Windows.Forms.Label()
        Me.lbl_dly_chg_mf_427 = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.gbxEnv = New System.Windows.Forms.GroupBox()
        Me.rdo_Prod = New System.Windows.Forms.RadioButton()
        Me.rdo_Test = New System.Windows.Forms.RadioButton()
        Me.rdo_Devl = New System.Windows.Forms.RadioButton()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.btn_Transfer = New System.Windows.Forms.Button()
        Me.lbl_warn = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btn_cancel = New System.Windows.Forms.Button()
        Me.gbxRACF = New System.Windows.Forms.GroupBox()
        Me.lblUserid = New System.Windows.Forms.Label()
        Me.txtUserid = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPW = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.gbxXferType.SuspendLayout()
        Me.PnlMonthly.SuspendLayout()
        Me.PnlDaily.SuspendLayout()
        Me.gbxEnv.SuspendLayout()
        Me.gbxRACF.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbxXferType
        '
        Me.gbxXferType.Controls.Add(Me.PnlMonthly)
        Me.gbxXferType.Controls.Add(Me.PnlDaily)
        Me.gbxXferType.Controls.Add(Me.rdo_Monthly)
        Me.gbxXferType.Controls.Add(Me.rdo_Daily)
        Me.gbxXferType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxXferType.Location = New System.Drawing.Point(16, 40)
        Me.gbxXferType.Name = "gbxXferType"
        Me.gbxXferType.Size = New System.Drawing.Size(688, 560)
        Me.gbxXferType.TabIndex = 1
        Me.gbxXferType.TabStop = False
        Me.gbxXferType.Text = "Select Transfer Type"
        Me.ToolTip1.SetToolTip(Me.gbxXferType, "Select either Daily or Monthly transfer")
        '
        'PnlMonthly
        '
        Me.PnlMonthly.AutoScroll = True
        Me.PnlMonthly.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlMonthly.Controls.Add(Me.lbl_mth_chg_mf)
        Me.PnlMonthly.Controls.Add(Me.btn_mth_chg_save)
        Me.PnlMonthly.Controls.Add(Me.lblMonthlyCharges)
        Me.PnlMonthly.Controls.Add(Me.lblMonthlyChgTo)
        Me.PnlMonthly.Controls.Add(Me.lbl_mth_chg_pc)
        Me.PnlMonthly.Controls.Add(Me.lblMonthlyChgFrom)
        Me.PnlMonthly.Location = New System.Drawing.Point(16, 398)
        Me.PnlMonthly.Name = "PnlMonthly"
        Me.PnlMonthly.Size = New System.Drawing.Size(656, 146)
        Me.PnlMonthly.TabIndex = 1
        '
        'lbl_mth_chg_mf
        '
        Me.lbl_mth_chg_mf.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_mth_chg_mf.Location = New System.Drawing.Point(24, 96)
        Me.lbl_mth_chg_mf.Name = "lbl_mth_chg_mf"
        Me.lbl_mth_chg_mf.Size = New System.Drawing.Size(544, 16)
        Me.lbl_mth_chg_mf.TabIndex = 4
        '
        'btn_mth_chg_save
        '
        Me.btn_mth_chg_save.Location = New System.Drawing.Point(496, 24)
        Me.btn_mth_chg_save.Name = "btn_mth_chg_save"
        Me.btn_mth_chg_save.Size = New System.Drawing.Size(64, 24)
        Me.btn_mth_chg_save.TabIndex = 5
        Me.btn_mth_chg_save.Text = "Browse..."
        Me.ToolTip1.SetToolTip(Me.btn_mth_chg_save, "Press to change filename")
        '
        'lblMonthlyCharges
        '
        Me.lblMonthlyCharges.Location = New System.Drawing.Point(8, 8)
        Me.lblMonthlyCharges.Name = "lblMonthlyCharges"
        Me.lblMonthlyCharges.Size = New System.Drawing.Size(120, 16)
        Me.lblMonthlyCharges.TabIndex = 0
        Me.lblMonthlyCharges.Text = "Charges:"
        '
        'lblMonthlyChgTo
        '
        Me.lblMonthlyChgTo.Location = New System.Drawing.Point(16, 80)
        Me.lblMonthlyChgTo.Name = "lblMonthlyChgTo"
        Me.lblMonthlyChgTo.Size = New System.Drawing.Size(216, 16)
        Me.lblMonthlyChgTo.TabIndex = 3
        Me.lblMonthlyChgTo.Text = "To Mainframe File:"
        '
        'lbl_mth_chg_pc
        '
        Me.lbl_mth_chg_pc.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_mth_chg_pc.Location = New System.Drawing.Point(32, 48)
        Me.lbl_mth_chg_pc.Name = "lbl_mth_chg_pc"
        Me.lbl_mth_chg_pc.Size = New System.Drawing.Size(544, 16)
        Me.lbl_mth_chg_pc.TabIndex = 2
        '
        'lblMonthlyChgFrom
        '
        Me.lblMonthlyChgFrom.Location = New System.Drawing.Point(16, 32)
        Me.lblMonthlyChgFrom.Name = "lblMonthlyChgFrom"
        Me.lblMonthlyChgFrom.Size = New System.Drawing.Size(216, 16)
        Me.lblMonthlyChgFrom.TabIndex = 3
        Me.lblMonthlyChgFrom.Text = "From LAN Interest CSV File:"
        '
        'PnlDaily
        '
        Me.PnlDaily.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlDaily.Controls.Add(Me.lbl_dly_chg_mf)
        Me.PnlDaily.Controls.Add(Me.btn_dly_chg_open)
        Me.PnlDaily.Controls.Add(Me.lblDailyCharges)
        Me.PnlDaily.Controls.Add(Me.lblDailyChgFrom)
        Me.PnlDaily.Controls.Add(Me.lblDailyPayFrom)
        Me.PnlDaily.Controls.Add(Me.lblDailyPay)
        Me.PnlDaily.Controls.Add(Me.btn_dly_pay_open)
        Me.PnlDaily.Controls.Add(Me.lbl_dly_chg_pc)
        Me.PnlDaily.Controls.Add(Me.lbl_dly_pay_mf)
        Me.PnlDaily.Controls.Add(Me.lbl_dly_pay_pc)
        Me.PnlDaily.Controls.Add(Me.lblDailyPayTo)
        Me.PnlDaily.Controls.Add(Me.lblDailyChgTo)
        Me.PnlDaily.Location = New System.Drawing.Point(16, 48)
        Me.PnlDaily.Name = "PnlDaily"
        Me.PnlDaily.Size = New System.Drawing.Size(656, 296)
        Me.PnlDaily.TabIndex = 5
        '
        'lbl_dly_chg_mf
        '
        Me.lbl_dly_chg_mf.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dly_chg_mf.Location = New System.Drawing.Point(45, 48)
        Me.lbl_dly_chg_mf.Name = "lbl_dly_chg_mf"
        Me.lbl_dly_chg_mf.Size = New System.Drawing.Size(531, 26)
        Me.lbl_dly_chg_mf.TabIndex = 2
        '
        'btn_dly_chg_open
        '
        Me.btn_dly_chg_open.Location = New System.Drawing.Point(585, 121)
        Me.btn_dly_chg_open.Name = "btn_dly_chg_open"
        Me.btn_dly_chg_open.Size = New System.Drawing.Size(64, 22)
        Me.btn_dly_chg_open.TabIndex = 10
        Me.btn_dly_chg_open.Text = "Browse..."
        Me.ToolTip1.SetToolTip(Me.btn_dly_chg_open, "Press to change filename")
        '
        'lblDailyCharges
        '
        Me.lblDailyCharges.Location = New System.Drawing.Point(8, 8)
        Me.lblDailyCharges.Name = "lblDailyCharges"
        Me.lblDailyCharges.Size = New System.Drawing.Size(120, 16)
        Me.lblDailyCharges.TabIndex = 0
        Me.lblDailyCharges.Text = "Charges:"
        '
        'lblDailyChgFrom
        '
        Me.lblDailyChgFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyChgFrom.Location = New System.Drawing.Point(32, 32)
        Me.lblDailyChgFrom.Name = "lblDailyChgFrom"
        Me.lblDailyChgFrom.Size = New System.Drawing.Size(216, 16)
        Me.lblDailyChgFrom.TabIndex = 1
        Me.lblDailyChgFrom.Text = "From Mainframe File:"
        '
        'lblDailyPayFrom
        '
        Me.lblDailyPayFrom.Location = New System.Drawing.Point(32, 184)
        Me.lblDailyPayFrom.Name = "lblDailyPayFrom"
        Me.lblDailyPayFrom.Size = New System.Drawing.Size(240, 16)
        Me.lblDailyPayFrom.TabIndex = 6
        Me.lblDailyPayFrom.Text = "From Mainframe File:"
        '
        'lblDailyPay
        '
        Me.lblDailyPay.Location = New System.Drawing.Point(8, 160)
        Me.lblDailyPay.Name = "lblDailyPay"
        Me.lblDailyPay.Size = New System.Drawing.Size(120, 16)
        Me.lblDailyPay.TabIndex = 5
        Me.lblDailyPay.Text = "Payments:"
        '
        'btn_dly_pay_open
        '
        Me.btn_dly_pay_open.Location = New System.Drawing.Point(584, 260)
        Me.btn_dly_pay_open.Name = "btn_dly_pay_open"
        Me.btn_dly_pay_open.Size = New System.Drawing.Size(64, 22)
        Me.btn_dly_pay_open.TabIndex = 12
        Me.btn_dly_pay_open.Text = "Browse..."
        Me.ToolTip1.SetToolTip(Me.btn_dly_pay_open, "Press to change filename")
        '
        'lbl_dly_chg_pc
        '
        Me.lbl_dly_chg_pc.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dly_chg_pc.Location = New System.Drawing.Point(42, 125)
        Me.lbl_dly_chg_pc.Name = "lbl_dly_chg_pc"
        Me.lbl_dly_chg_pc.Size = New System.Drawing.Size(536, 16)
        Me.lbl_dly_chg_pc.TabIndex = 4
        '
        'lbl_dly_pay_mf
        '
        Me.lbl_dly_pay_mf.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dly_pay_mf.Location = New System.Drawing.Point(45, 200)
        Me.lbl_dly_pay_mf.Name = "lbl_dly_pay_mf"
        Me.lbl_dly_pay_mf.Size = New System.Drawing.Size(523, 19)
        Me.lbl_dly_pay_mf.TabIndex = 7
        '
        'lbl_dly_pay_pc
        '
        Me.lbl_dly_pay_pc.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dly_pay_pc.Location = New System.Drawing.Point(48, 264)
        Me.lbl_dly_pay_pc.Name = "lbl_dly_pay_pc"
        Me.lbl_dly_pay_pc.Size = New System.Drawing.Size(528, 16)
        Me.lbl_dly_pay_pc.TabIndex = 9
        '
        'lblDailyPayTo
        '
        Me.lblDailyPayTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyPayTo.Location = New System.Drawing.Point(32, 248)
        Me.lblDailyPayTo.Name = "lblDailyPayTo"
        Me.lblDailyPayTo.Size = New System.Drawing.Size(216, 16)
        Me.lblDailyPayTo.TabIndex = 1
        Me.lblDailyPayTo.Text = "To LAN File:"
        '
        'lblDailyChgTo
        '
        Me.lblDailyChgTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyChgTo.Location = New System.Drawing.Point(32, 90)
        Me.lblDailyChgTo.Name = "lblDailyChgTo"
        Me.lblDailyChgTo.Size = New System.Drawing.Size(83, 22)
        Me.lblDailyChgTo.TabIndex = 1
        Me.lblDailyChgTo.Text = "To LAN File:"
        '
        'rdo_Monthly
        '
        Me.rdo_Monthly.Location = New System.Drawing.Point(8, 360)
        Me.rdo_Monthly.Name = "rdo_Monthly"
        Me.rdo_Monthly.Size = New System.Drawing.Size(312, 32)
        Me.rdo_Monthly.TabIndex = 0
        Me.rdo_Monthly.TabStop = True
        Me.rdo_Monthly.Text = "Copy &Monthly Interest Charges from LAN "
        Me.ToolTip1.SetToolTip(Me.rdo_Monthly, "Select this to perform Monthly transfer")
        '
        'rdo_Daily
        '
        Me.rdo_Daily.Location = New System.Drawing.Point(8, 24)
        Me.rdo_Daily.Name = "rdo_Daily"
        Me.rdo_Daily.Size = New System.Drawing.Size(264, 16)
        Me.rdo_Daily.TabIndex = 0
        Me.rdo_Daily.TabStop = True
        Me.rdo_Daily.Text = "Move &Daily RMS Interface Extract to LAN"
        Me.ToolTip1.SetToolTip(Me.rdo_Daily, "Select this to perform Daily transfer")
        '
        'btn_dly_pay_427_open
        '
        Me.btn_dly_pay_427_open.Location = New System.Drawing.Point(0, 0)
        Me.btn_dly_pay_427_open.Name = "btn_dly_pay_427_open"
        Me.btn_dly_pay_427_open.Size = New System.Drawing.Size(75, 23)
        Me.btn_dly_pay_427_open.TabIndex = 0
        '
        'btn_dly_chg_427_open
        '
        Me.btn_dly_chg_427_open.Location = New System.Drawing.Point(0, 0)
        Me.btn_dly_chg_427_open.Name = "btn_dly_chg_427_open"
        Me.btn_dly_chg_427_open.Size = New System.Drawing.Size(75, 23)
        Me.btn_dly_chg_427_open.TabIndex = 0
        '
        'lbl_dly_pay_pc_427
        '
        Me.lbl_dly_pay_pc_427.Location = New System.Drawing.Point(0, 0)
        Me.lbl_dly_pay_pc_427.Name = "lbl_dly_pay_pc_427"
        Me.lbl_dly_pay_pc_427.Size = New System.Drawing.Size(100, 23)
        Me.lbl_dly_pay_pc_427.TabIndex = 0
        '
        'lbl_dly_pay_mf_427
        Me.lbl_dly_pay_mf_427.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dly_pay_mf_427.Location = New System.Drawing.Point(75, 200)
        Me.lbl_dly_pay_mf_427.Name = "lbl_dly_pay_mf_427"
        Me.lbl_dly_pay_mf_427.Size = New System.Drawing.Size(100, 23)
        Me.lbl_dly_pay_mf_427.TabIndex = 0
        '
        'lbl_dly_chg_pc_427
        '
        Me.lbl_dly_chg_pc_427.Location = New System.Drawing.Point(0, 0)
        Me.lbl_dly_chg_pc_427.Name = "lbl_dly_chg_pc_427"
        Me.lbl_dly_chg_pc_427.Size = New System.Drawing.Size(100, 23)
        Me.lbl_dly_chg_pc_427.TabIndex = 0
        '
        'lbl_dly_chg_mf_427
        '
        Me.lbl_dly_chg_mf_427.Location = New System.Drawing.Point(0, 0)
        Me.lbl_dly_chg_mf_427.Name = "lbl_dly_chg_mf_427"
        Me.lbl_dly_chg_mf_427.Size = New System.Drawing.Size(100, 23)
        Me.lbl_dly_chg_mf_427.TabIndex = 0
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem3})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2})
        Me.MenuItem1.Text = "&File"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "E&xit!"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4})
        Me.MenuItem3.Text = "&Help"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.Text = "&About Weigh-Scale Transfer"
        '
        'gbxEnv
        '
        Me.gbxEnv.Controls.Add(Me.rdo_Prod)
        Me.gbxEnv.Controls.Add(Me.rdo_Test)
        Me.gbxEnv.Controls.Add(Me.rdo_Devl)
        Me.gbxEnv.Location = New System.Drawing.Point(720, 40)
        Me.gbxEnv.Name = "gbxEnv"
        Me.gbxEnv.Size = New System.Drawing.Size(120, 104)
        Me.gbxEnv.TabIndex = 3
        Me.gbxEnv.TabStop = False
        Me.gbxEnv.Text = "Select Environment"
        Me.ToolTip1.SetToolTip(Me.gbxEnv, "Choose the environment to work in")
        '
        'rdo_Prod
        '
        Me.rdo_Prod.Location = New System.Drawing.Point(8, 30)
        Me.rdo_Prod.Name = "rdo_Prod"
        Me.rdo_Prod.Size = New System.Drawing.Size(90, 24)
        Me.rdo_Prod.TabIndex = 0
        Me.rdo_Prod.TabStop = True
        Me.rdo_Prod.Text = "&Production"
        Me.ToolTip1.SetToolTip(Me.rdo_Prod, "Select this to use Production files")
        '
        'rdo_Test
        '
        Me.rdo_Test.Location = New System.Drawing.Point(8, 48)
        Me.rdo_Test.Name = "rdo_Test"
        Me.rdo_Test.Size = New System.Drawing.Size(88, 24)
        Me.rdo_Test.TabIndex = 1
        Me.rdo_Test.TabStop = True
        Me.rdo_Test.Text = "&Test"
        Me.ToolTip1.SetToolTip(Me.rdo_Test, "Select this to use Testing files")
        '
        'rdo_Devl
        '
        Me.rdo_Devl.Location = New System.Drawing.Point(8, 72)
        Me.rdo_Devl.Name = "rdo_Devl"
        Me.rdo_Devl.Size = New System.Drawing.Size(96, 24)
        Me.rdo_Devl.TabIndex = 2
        Me.rdo_Devl.TabStop = True
        Me.rdo_Devl.Text = "D&evelopment"
        Me.ToolTip1.SetToolTip(Me.rdo_Devl, "Select this to use Development files")
        '
        'btn_Transfer
        '
        Me.btn_Transfer.Enabled = False
        Me.btn_Transfer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn_Transfer.Location = New System.Drawing.Point(728, 392)
        Me.btn_Transfer.Name = "btn_Transfer"
        Me.btn_Transfer.Size = New System.Drawing.Size(88, 40)
        Me.btn_Transfer.TabIndex = 5
        Me.btn_Transfer.Text = "Transfer"
        Me.ToolTip1.SetToolTip(Me.btn_Transfer, "Press this to perform the file transfer")
        '
        'lbl_warn
        '
        Me.lbl_warn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_warn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbl_warn.Location = New System.Drawing.Point(16, 8)
        Me.lbl_warn.Name = "lbl_warn"
        Me.lbl_warn.Size = New System.Drawing.Size(656, 24)
        Me.lbl_warn.TabIndex = 0
        Me.lbl_warn.Text = "Choose one Transfer Type, one Environment and enter your CITS Userid and Password" &
    ""
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.InitialDirectory = "c:\accpac_ftp"
        '
        'btn_cancel
        '
        Me.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_cancel.Location = New System.Drawing.Point(728, 448)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(88, 40)
        Me.btn_cancel.TabIndex = 6
        Me.btn_cancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.btn_cancel, "Press to quit")
        '
        'gbxRACF
        '
        Me.gbxRACF.Controls.Add(Me.lblUserid)
        Me.gbxRACF.Controls.Add(Me.txtUserid)
        Me.gbxRACF.Controls.Add(Me.lblPassword)
        Me.gbxRACF.Controls.Add(Me.txtPW)
        Me.gbxRACF.Location = New System.Drawing.Point(720, 152)
        Me.gbxRACF.Name = "gbxRACF"
        Me.gbxRACF.Size = New System.Drawing.Size(120, 120)
        Me.gbxRACF.TabIndex = 4
        Me.gbxRACF.TabStop = False
        Me.gbxRACF.Text = "Enter RACF Info"
        Me.ToolTip1.SetToolTip(Me.gbxRACF, "Enter ID and password here")
        '
        'lblUserid
        '
        Me.lblUserid.Location = New System.Drawing.Point(8, 16)
        Me.lblUserid.Name = "lblUserid"
        Me.lblUserid.Size = New System.Drawing.Size(48, 16)
        Me.lblUserid.TabIndex = 0
        Me.lblUserid.Text = "&Userid:"
        '
        'txtUserid
        '
        Me.txtUserid.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserid.Location = New System.Drawing.Point(6, 28)
        Me.txtUserid.MaxLength = 8
        Me.txtUserid.Name = "txtUserid"
        Me.txtUserid.Size = New System.Drawing.Size(96, 22)
        Me.txtUserid.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtUserid, "Enter your CITS userid")
        '
        'lblPassword
        '
        Me.lblPassword.Location = New System.Drawing.Point(8, 64)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(70, 16)
        Me.lblPassword.TabIndex = 2
        Me.lblPassword.Text = "Pass&word:"
        '
        'txtPW
        '
        Me.txtPW.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPW.Location = New System.Drawing.Point(8, 80)
        Me.txtPW.MaxLength = 8
        Me.txtPW.Name = "txtPW"
        Me.txtPW.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPW.Size = New System.Drawing.Size(96, 22)
        Me.txtPW.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtPW, "Enter your CITS RACF password")
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(728, 360)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(88, 24)
        Me.ProgressBar1.TabIndex = 7
        Me.ProgressBar1.Visible = False
        '
        'frm_WS_FTP
        '
        Me.AcceptButton = Me.btn_Transfer
        Me.AllowDrop = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.CancelButton = Me.btn_cancel
        Me.ClientSize = New System.Drawing.Size(869, 632)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.gbxRACF)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.lbl_warn)
        Me.Controls.Add(Me.gbxEnv)
        Me.Controls.Add(Me.gbxXferType)
        Me.Controls.Add(Me.btn_Transfer)
        Me.HelpButton = True
        Me.Menu = Me.MainMenu1
        Me.Name = "frm_WS_FTP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Weigh-Scale Transfer Program 2.0"
        Me.gbxXferType.ResumeLayout(False)
        Me.PnlMonthly.ResumeLayout(False)
        Me.PnlDaily.ResumeLayout(False)
        Me.gbxEnv.ResumeLayout(False)
        Me.gbxRACF.ResumeLayout(False)
        Me.gbxRACF.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        End
    End Sub

    Private Sub rdo_Prod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdo_Prod.CheckedChanged
        Env_1 = "P"
        Env_long = "Production"
        apply_changes()
    End Sub

    Private Sub rdo_Test_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdo_Test.CheckedChanged
        Env_1 = "T"
        Env_long = "Testing"
        apply_changes()
    End Sub

    Private Sub rdo_Devl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdo_Devl.CheckedChanged
        Env_1 = "D"
        Env_long = "Development"
        apply_changes()
    End Sub

    Private Sub rdo_Daily_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdo_Daily.CheckedChanged
        apply_changes()
    End Sub

    Private Sub rdo_Monthly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdo_Monthly.CheckedChanged
        apply_changes()
    End Sub
    Private Sub apply_changes()
        If (rdo_Daily.Checked Or rdo_Monthly.Checked) And (rdo_Devl.Checked Or rdo_Test.Checked Or rdo_Prod.Checked) Then
            WorkDir = IIf(Env_1.ToUpper = "T", BaseDirTest, BaseDirProd) & Env_long & WorkDirSuffix
            Dim HLQ_Head = IIf(Env_1.ToUpper = "T", "/SYSTEM/tmp/" + HLQ, "/SYSTEM/tmp/" + HLQ)
            lbl_warn.Visible = False
            btn_Transfer.Enabled = True
            If rdo_Daily.Checked Then
                PnlDaily.Visible = True
                PnlMonthly.Visible = False

                lbl_dly_chg_mf.Text = HLQ_Head & (Env_1 & ChargeFileName).ToUpper
                'lbl_dly_chg_mf.Text = lbl_dly_chg_mf.Text.ToUpper
                lbl_dly_chg_mf_427.Text = HLQ_Head & (Env_1 & ChargeFileName427).ToUpper
                'lbl_dly_chg_mf_427.Text = lbl_dly_chg_mf_427.Text.ToUpper

                lbl_dly_pay_mf.Text = HLQ_Head & (Env_1 & PayFileName).ToUpper
                ' lbl_dly_pay_mf.Text = lbl_dly_pay_mf.Text.ToUpper
                lbl_dly_pay_mf_427.Text = HLQ_Head & (Env_1 & PayFileName427).ToUpper
                'lbl_dly_pay_mf_427.Text = lbl_dly_pay_mf_427.Text.ToUpper

                lbl_dly_chg_pc.Text = WorkDir & DailyInvPrefix & Datenow & csvSuffix
                lbl_dly_chg_pc.Text = lbl_dly_chg_pc.Text.ToUpper
                lbl_dly_chg_pc_427.Text = WorkDir & DailyInvPrefix427 & Datenow & csvSuffix
                lbl_dly_chg_pc_427.Text = lbl_dly_chg_pc_427.Text.ToUpper

                lbl_dly_pay_pc.Text = WorkDir & DailyRecPrefix & Datenow & csvSuffix
                lbl_dly_pay_pc.Text = lbl_dly_pay_pc.Text.ToUpper
                lbl_dly_pay_pc_427.Text = WorkDir & DailyRecPrefix427 & Datenow & csvSuffix
                lbl_dly_pay_pc_427.Text = lbl_dly_pay_pc_427.Text.ToUpper


            End If
            If rdo_Monthly.Checked Then
                PnlDaily.Visible = False
                PnlMonthly.Visible = True
                lbl_mth_chg_pc.Text = WorkDir & MonthlyPrefix & Datenow & csvSuffix
                lbl_mth_chg_pc.Text = lbl_mth_chg_pc.Text.ToUpper
                lbl_mth_chg_mf.Text = HLQ_Head & (Env_1 & InterestFileName).ToUpper
                'lbl_mth_chg_mf.Text = lbl_mth_chg_mf.Text.ToUpper    'The server directory and file are case sensitive. Directory name can be in lower case.
            End If
        Else
            lbl_warn.Visible = True
        End If
    End Sub
    Private Sub frm_WS_FTP_ctor(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Datenow = Microsoft.VisualBasic.DateAndTime.Year(Now.Date).ToString & Microsoft.VisualBasic.Right("0" & Microsoft.VisualBasic.DateAndTime.Month(Now.Date).ToString, 2) & Microsoft.VisualBasic.Right("0" & Microsoft.VisualBasic.DateAndTime.Day(Now.Date).ToString, 2)
        PnlDaily.Visible = False
        PnlMonthly.Visible = False
        rdo_Daily.Checked = True
        rdo_Monthly.Checked = False
        rdo_Prod.Checked = False
        rdo_Test.Checked = False
        rdo_Devl.Checked = False
        rdo_Prod.Enabled = True

        rdo_Test.Enabled = False

        rdo_Devl.Enabled = False
        FTP_ok = False
    End Sub

    Private Sub btn_Transfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Transfer.Click
        'reset task bar  - by Simon Di
        ProgressBar1.Value = 0
        If Not ((rdo_Prod.Checked Or rdo_Test.Checked Or rdo_Devl.Checked) And (rdo_Daily.Checked Or rdo_Monthly.Checked)) Then
            MsgBox("Select one Transfer-Type and one Environment", MsgBoxStyle.OkOnly, "Weigh-Scale Transfer")
        Else
            If txtUserid.Text = "" Or txtPW.Text = "" Then
                MsgBox("Please enter both your CITS RACF userid and password", MsgBoxStyle.Exclamation, "Weigh Scale Transfer")
                If txtUserid.Text = "" Then
                    txtUserid.Focus()
                Else
                    txtPW.Focus()
                End If
            Else            ' good id and password
                MsgBox("The file transfer is about to start, please note any error messages that may follow", MsgBoxStyle.ApplicationModal, "Weigh_Scale Transfer")
                RunFTP()
                txtPW.Text = ""
                If rdo_Daily.Checked Then
                    If FTP_ok Then
                        MsgBox("The RMS Interface files have been transferred and can now be loaded into ACCPAC", MsgBoxStyle.OkOnly, "Weigh_Scale Transfer")
                    Else    ' FTP from mainframe failed
                        MsgBox("The RMS Interface files FAILED to be transferred. Please note any error messages and contact support", MsgBoxStyle.Exclamation, "Weigh_Scale Transfer")
                    End If
                Else        ' monthly transfer
                    If FTP_ok Then
                        MsgBox("The ACCPAC interest charges file has been transferred successfully to the mainframe", MsgBoxStyle.OkOnly, "Weigh_Scale Transfer")

                    Else    ' ftp to mainframe failed
                        MsgBox("The ACCPAC interest charges file FAILED to be transferred to the mainframe. Please note any error messages and contact support", MsgBoxStyle.Exclamation, "Weigh_Scale Transfer")
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        End
    End Sub
    Private Sub btn_dly_pay_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_dly_pay_open.Click
        SaveFileDialog1.Filter = csvFileType
        SaveFileDialog1.InitialDirectory = WorkDir
        SaveFileDialog1.Title = "Daily Extract Receipts LAN Filename"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            lbl_dly_pay_pc.Text = SaveFileDialog1.FileName.ToUpper
        End If
    End Sub
    Private Sub btn_dly_pay_427_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_dly_pay_427_open.Click
        SaveFileDialog1.Filter = csvFileType
        SaveFileDialog1.InitialDirectory = WorkDir
        SaveFileDialog1.Title = "Daily Extract 427 Receipts LAN Filename"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            lbl_dly_pay_pc_427.Text = SaveFileDialog1.FileName.ToUpper
        End If
    End Sub
    Private Sub btn_dly_chg_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_dly_chg_open.Click
        SaveFileDialog1.Filter = csvFileType
        SaveFileDialog1.InitialDirectory = WorkDir
        SaveFileDialog1.Title = "Daily Extract Invoices LAN Filename"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            lbl_dly_chg_pc.Text = SaveFileDialog1.FileName.ToUpper
        End If
    End Sub
    Private Sub btn_dly_chg_427_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_dly_chg_427_open.Click
        SaveFileDialog1.Filter = csvFileType
        SaveFileDialog1.InitialDirectory = WorkDir
        SaveFileDialog1.Title = "Daily Extract Invoices 427 LAN Filename"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            lbl_dly_chg_pc_427.Text = SaveFileDialog1.FileName.ToUpper
        End If
    End Sub
    Private Sub btn_mth_chg_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_mth_chg_save.Click
        OpenFileDialog1.Filter = csvFileType
        OpenFileDialog1.InitialDirectory = WorkDir
        OpenFileDialog1.Title = "Monthly Interest Charges LAN Filename"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            lbl_mth_chg_pc.Text = OpenFileDialog1.FileName.ToUpper
        End If
    End Sub
    Protected Sub RunFTP()
        'Dim sess As clsFTP
        'sess = New clsFTP

        'Dim winSCPSession As New Session()
        Dim winSCPSessionOptions As New SessionOptions()
        Dim winSCPTransferOptions As New TransferOptions()

        Dim userName = txtUserid.Text
        Dim passWD = txtPW.Text
        With winSCPSessionOptions
            .Protocol = Protocol.Sftp
            .HostName = Server
            .UserName = userName
            .Password = passWD
            .GiveUpSecurityAndAcceptAnySshHostKey = True
        End With

        'added by J.Jaffey to fix original developers defect
        'hostkey is ssh-rsa 2048 11:d7:89:88:5d:3a:2a:f8:72:b4:fc:20:c9:37:69:44
        'winSCPSessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 11:d7:89:88:5d:3a:2a:f8:72:b4:fc:20:c9:37:69:44"

        winSCPTransferOptions.TransferMode = TransferMode.Ascii  'Binary won't work

        'sess.RemoteHost = Server
        'sess.RemoteUser = txtUserid.Text
        'sess.RemotePassword = txtPW.Text
        'FTP_ok = True
        Using winSCPSession As Session = New Session
            Try
                winSCPSession.Open(winSCPSessionOptions)

                If rdo_Daily.Checked Then
                    'For Daily transaction
                    'Copy Files
                    Try
                        Dim copyCommand As String = "cp ""//' CTMS" + Env_1.ToUpper + ".D01C427.CHARGE.CSV'"" /tmp/CTMS" + Env_1.ToUpper + ".D01C427.CHARGE.CSV"
                        winSCPSession.ExecuteCommand(copyCommand).Check()

                        copyCommand = "cp ""//' CTMS" + Env_1.ToUpper + ".D01CONV.CHARGE.CSV'"" /tmp/CTMS" + Env_1.ToUpper + ".D01CONV.CHARGE.CSV"
                        winSCPSession.ExecuteCommand(copyCommand).Check()

                        copyCommand = "cp ""//' CTMS" + Env_1.ToUpper + ".D01C427.PAYMENT.CSV'"" /tmp/CTMS" + Env_1.ToUpper + ".D01C427.PAYMENT.CSV"
                        winSCPSession.ExecuteCommand(copyCommand).Check()

                        copyCommand = "cp ""//' CTMS" + Env_1.ToUpper + ".D01CONV.PAYMENT.CSV'"" /tmp/CTMS" + Env_1.ToUpper + ".D01CONV.PAYMENT.CSV"
                        winSCPSession.ExecuteCommand(copyCommand).Check()
                    Catch ex As System.Exception
                        MessageBox.Show(ex.Message)
                        FTP_ok = False
                        Return
                    End Try
                    'If sess.Login() Then
                    '    sess.SetBinaryMode(False)
                    ProgressBar1.Visible = True
                    ProgressBar1.Maximum = 12

                    ProgressBar1.Value = 0
                    'winSCPSession.GetFiles("/tmp/CTMST.D01CONV.CHARGE.CSV", lbl_dly_chg_pc.Text, False, winSCPTransferOptions)
                    'winSCPSession.GetFiles("/tmp/CTMST.D01CONV.PAYMENT.csv", lbl_dly_pay_pc.Text, False, winSCPTransferOptions)

                    Try
                        Dim svrFile_chg As String = lbl_dly_chg_mf.Text '"/tmp/CTMST.D01CONV.CHARGE.CSV"
                        winSCPSession.GetFiles(svrFile_chg, lbl_dly_chg_pc.Text, False, winSCPTransferOptions)
                        ProgressBar1.Value += 1
                        winSCPSession.RemoveFiles(lbl_dly_chg_mf.Text)
                        'sess.DeleteFile(lbl_dly_chg_mf_427.Text)
                    Catch ex As System.Exception
                        MessageBox.Show(ex.Message)

                    End Try
                    ProgressBar1.Value += 1
                    Try
                        'sess.DownloadFile(lbl_dly_chg_mf.Text, lbl_dly_chg_pc.Text)
                        Dim svrFile_chg427 As String = lbl_dly_chg_mf_427.Text '"/tmp/CTMST.D01C427.CHARGE.CSV" 'lbl_dly_chg_mf_427.Text
                        winSCPSession.GetFiles(svrFile_chg427, lbl_dly_chg_pc_427.Text, False, winSCPTransferOptions)
                        ProgressBar1.Value += 1
                        winSCPSession.RemoveFiles(lbl_dly_chg_mf_427.Text)
                        'sess.DeleteFile(lbl_dly_chg_mf.Text)
                    Catch ex As System.Exception
                        MessageBox.Show(ex.Message)
                    End Try
                    ProgressBar1.Value += 1
                    Try
                        'sess.DownloadFile(lbl_dly_chg_mf_427.Text, lbl_dly_chg_pc_427.Text)
                        Dim svrFile_pay As String = lbl_dly_pay_mf.Text '"/tmp/CTMST.D01CONV.PAYMENT.CSV" 'lbl_dly_pay_mf.Text
                        winSCPSession.GetFiles(svrFile_pay, lbl_dly_pay_pc.Text, False, winSCPTransferOptions)
                        ProgressBar1.Value += 1
                        winSCPSession.RemoveFiles(lbl_dly_pay_mf.Text)
                        'sess.DeleteFile(lbl_dly_pay_mf.Text)
                    Catch ex As System.Exception
                        MessageBox.Show(ex.Message)

                    End Try
                    ProgressBar1.Value += 1
                    Try
                        'sess.DownloadFile(lbl_dly_pay_mf.Text, lbl_dly_pay_pc.Text)
                        Dim svrFile_pay427 As String = lbl_dly_pay_mf_427.Text '"/tmp/CTMST.D01CONV.PAYMENT.CSV" 'lbl_dly_pay_mf_427.Text
                        winSCPSession.GetFiles(svrFile_pay427, lbl_dly_pay_pc_427.Text, False, winSCPTransferOptions)
                        'sess.DownloadFile(lbl_dly_pay_mf_427.Text, lbl_dly_pay_pc_427.Text)
                        ProgressBar1.Value += 1
                        winSCPSession.RemoveFiles(lbl_dly_pay_mf_427.Text)
                        'sess.DeleteFile(lbl_dly_pay_mf_427.Text)
                    Catch ex As System.Exception
                        MessageBox.Show(ex.Message)

                    End Try

                    ProgressBar1.Value += 1

                    ' because the file may have been concatenated with other days files
                    ' it may have file headers numerous times in the file, 
                    ' we only want one set of headers. 
                    ' Though you likely dont want to because failure will mess with 
                    ' subsequent downloads.  Should be rewritten for more redundancy
                    ' -bp,2017

                    ' Charge file has 3 lines of hdrs, payments has 4 lines.
                    remove_dupe_accpac_hdrs(3, lbl_dly_chg_pc.Text)
                    ProgressBar1.Value += 1
                    remove_dupe_accpac_hdrs(3, lbl_dly_chg_pc_427.Text)
                    ProgressBar1.Value += 1

                    remove_dupe_accpac_hdrs(4, lbl_dly_pay_pc.Text)
                    ProgressBar1.Value += 1
                    remove_dupe_accpac_hdrs(4, lbl_dly_pay_pc_427.Text)
                    ProgressBar1.Value += 1
                Else
                    'For monthly transaction: Transfer monthly files from local to the server
                    ProgressBar1.Visible = True
                    ProgressBar1.Maximum = 1
                    ProgressBar1.Value = 0
                    winSCPSession.PutFiles(lbl_mth_chg_pc.Text, lbl_mth_chg_mf.Text, False, winSCPTransferOptions)
                    'sess.UploadFile(lbl_mth_chg_mf.Text, lbl_mth_chg_pc.Text)
                    ProgressBar1.Value += 1
                End If
                FTP_ok = True
            Catch ex As System.Exception
                MessageBox.Show(ex.Message)

                'If (Not winSCPSession Is Nothing) Then
                '    MessageBox.Show("Message from FTP Server was: " & )
                'End If
                FTP_ok = False
            Finally
                If Not winSCPSession Is Nothing Then
                    If winSCPSession.Opened Then
                        winSCPSession.Close()
                    End If

                    winSCPSession.Dispose()
                End If
            End Try
        End Using
    End Sub
    '  Comment from Simon Di
    'This Sub reads() content line by line from the file s_infilename
    ' And write to a temporary file called "weigh-scale-temp2017-03-17_104624.txt"
    ' Then copy the temporary file to the fole of s_infilename
    ' Then delete the temporary file of "weigh-scale-temp2017-03-17_104624.txt"
    ' Not sure what purpose it serves.  As I don't understand the purpose of the business application, I kept the it as is.
    Protected Sub remove_dupe_accpac_hdrs(ByVal int_maxhdrs As Integer, ByVal s_infilename As String)
        'Dim fileStream As FileStream
        Dim stream_reader As StreamReader
        Dim stream_writer As StreamWriter
        If (Not File.Exists(s_infilename)) Then    'Added the if block by Simon Di
            Return
        End If
        Try
            'fileStream = New FileStream(s_infilename, FileMode.OpenOrCreate)
            Dim fs As New FileStream(dupe_temp_file, FileMode.OpenOrCreate, FileAccess.Write)
            stream_reader = New StreamReader(s_infilename)
            Dim line As String
            Dim hdr_count As Integer = 0

            stream_writer = New StreamWriter(fs)
            ' read file into temp-file, and delete duplicate headers as we go
            line = stream_reader.ReadLine()
            Do While Not (line Is Nothing)
                line = line.Trim()
                If line.Length > 0 Then
                    If line.Substring(0, 7).Equals(Hdr_ID) Then
                        If hdr_count < int_maxhdrs Then
                            'still processing 1st hdrs, write line away
                            stream_writer.WriteLine(line)
                            hdr_count = hdr_count + 1
                        End If
                    Else
                        ' not a header, so write line
                        stream_writer.WriteLine(line)
                    End If
                End If
                line = stream_reader.ReadLine()
            Loop
        Catch exc As Exception
            MsgBox(exc.Message, MsgBoxStyle.Exclamation, " Remove Dupe Error")
        Finally
            stream_reader.Close()
            stream_writer.Close()
            ' copy temp file to original file, then delete temp file
            File.Copy(dupe_temp_file, s_infilename, True)
            File.Delete(dupe_temp_file)
        End Try
    End Sub
    Private Sub txtUserid_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserid.LostFocus
        If txtUserid.Text.ToUpper <> BillMarsh And txtUserid.Text.ToUpper <> TabithaGarcia And txtUserid.Text.ToUpper <> JoeJaffey And txtUserid.Text.ToUpper <> "SC82939" Then
            rdo_Prod.Checked = True
            rdo_Test.Enabled = False
            rdo_Devl.Enabled = False
        Else
            rdo_Prod.Checked = True
            rdo_Test.Enabled = True
            rdo_Devl.Enabled = True
        End If
    End Sub

End Class