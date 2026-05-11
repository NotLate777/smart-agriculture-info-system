using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;

using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PreviewDemo
{
    /// <summary>
    /// Form1 的摘要说明。
    /// </summary>
    public class Preview : System.Windows.Forms.Form
    {
        private System.Threading.Timer threadTimer1;
        private System.Threading.Timer threadTimer2;
        private System.Threading.Timer threadTimer3;
        private System.Threading.Timer threadTimer4;
        private System.Threading.Timer threadTimer5;
        private System.Threading.Timer threadTimer6;
        private System.Threading.Timer threadTimer7;
        private System.Threading.Timer threadTimer8;
        private List<string> list1 = new List<string>();
        private List<string> list2 = new List<string>();
        private List<string> list3 = new List<string>();
        private List<string> list4 = new List<string>();
        private List<string> list5 = new List<string>();
        private List<string> list6 = new List<string>();
        private List<string> list7 = new List<string>();
        private List<string> list8 = new List<string>();
        public delegate void setCen(object value);





        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private bool m_bTalk = false;
        private Int32 m_lRealHandle = -1;
        private int lVoiceComHandle = -1;
        private string str;

        CHCNetSDK.REALDATACALLBACK RealData = null;
        CHCNetSDK.LOGINRESULTCALLBACK LoginCallBack = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg;
        public CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLogInfo;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V40 DeviceInfo;

        public delegate void UpdateTextStatusCallback(string strLogStatus, IntPtr lpDeviceInfo);
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.PictureBox RealPlayWnd;
        private TextBox textBoxIP;
        private TextBox textBoxPort;
        private TextBox textBoxUserName;
        private TextBox textBoxPassword;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label13;
        private TextBox textBoxChannel;
        private Button btn_Exit;
        /*private Button PtzGet;
        private Button PtzSet;*/
        private Label label19;
        /*private ComboBox comboBox1;
        private TextBox textBoxPanPos;
        private TextBox textBoxTiltPos;
        private TextBox textBoxZoomPos;*/
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label26;
        private Label label27;
        private Label labelLogin;
        private Label data1;
        private Label data2;
        private Label data5;
        private Label data6;
        private Label data7;
        private Label data8;
        private Label data4;
        private Label data44;
        private Label data55;
        public Label data88;
        public Label data77;
        private Label data66;
        private Label data33;
        private Label data22;
        private Label data11;
        private Button button3;
        private Button button4;
        private Label data3;
        private Label label25;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label12;
        private Label label11;

        private TextBox ID;
        private TextBox apikey;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        public System.Windows.Forms.Timer timer1;
        private Button button1;
        private Button button2;
        private IContainer components;

        //private GroupBox groupBox1;

        public Preview()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
            }
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
            }
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
            }
            if (m_bInitSDK == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.RealPlayWnd = new System.Windows.Forms.PictureBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.data1 = new System.Windows.Forms.Label();
            this.data2 = new System.Windows.Forms.Label();
            this.data5 = new System.Windows.Forms.Label();
            this.data6 = new System.Windows.Forms.Label();
            this.data7 = new System.Windows.Forms.Label();
            this.data8 = new System.Windows.Forms.Label();
            this.data4 = new System.Windows.Forms.Label();
            this.data44 = new System.Windows.Forms.Label();
            this.data55 = new System.Windows.Forms.Label();
            this.data88 = new System.Windows.Forms.Label();
            this.data77 = new System.Windows.Forms.Label();
            this.data66 = new System.Windows.Forms.Label();
            this.data33 = new System.Windows.Forms.Label();
            this.data22 = new System.Windows.Forms.Label();
            this.data11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.data3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.TextBox();
            this.apikey = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.Control;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(46, 568);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(76, 41);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.SystemColors.Control;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreview.Location = new System.Drawing.Point(332, 561);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(504, 57);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "预览";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // RealPlayWnd
            // 
            this.RealPlayWnd.BackColor = System.Drawing.SystemColors.WindowText;
            this.RealPlayWnd.Location = new System.Drawing.Point(316, 10);
            this.RealPlayWnd.Name = "RealPlayWnd";
            this.RealPlayWnd.Size = new System.Drawing.Size(553, 527);
            this.RealPlayWnd.TabIndex = 4;
            this.RealPlayWnd.TabStop = false;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(146, 296);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(152, 25);
            this.textBoxIP.TabIndex = 2;
            this.textBoxIP.Text = "192.168.1.65";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(146, 434);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(152, 25);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "8001";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(146, 365);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(152, 25);
            this.textBoxUserName.TabIndex = 4;
            this.textBoxUserName.Text = "admin";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPassword.Location = new System.Drawing.Point(146, 503);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(152, 25);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.Text = "chuangxin508";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label5.Location = new System.Drawing.Point(42, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 22);
            this.label5.TabIndex = 9;
            this.label5.Text = "设备IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label6.Location = new System.Drawing.Point(27, 434);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 22);
            this.label6.TabIndex = 10;
            this.label6.Text = "设备端口";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label7.Location = new System.Drawing.Point(42, 365);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 22);
            this.label7.TabIndex = 11;
            this.label7.Text = "用户名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label8.Location = new System.Drawing.Point(46, 503);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 22);
            this.label8.TabIndex = 12;
            this.label8.Text = "密码";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(577, 286);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "预览/抓图通道";
            this.label13.Visible = false;
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(697, 281);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(133, 25);
            this.textBoxChannel.TabIndex = 6;
            this.textBoxChannel.Text = "1";
            this.textBoxChannel.Visible = false;
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Exit.Location = new System.Drawing.Point(195, 568);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(76, 41);
            this.btn_Exit.TabIndex = 11;
            this.btn_Exit.Tag = "";
            this.btn_Exit.Text = "退出";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label26.Location = new System.Drawing.Point(8, 227);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(135, 22);
            this.label26.TabIndex = 35;
            this.label26.Text = "master-apikey";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label27.Location = new System.Drawing.Point(42, 89);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(67, 22);
            this.label27.TabIndex = 34;
            this.label27.Text = "设备ID";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(578, 206);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(142, 15);
            this.labelLogin.TabIndex = 33;
            this.labelLogin.Text = "登录状态（异步）：";
            this.labelLogin.Visible = false;
            // 
            // data1
            // 
            this.data1.AutoSize = true;
            this.data1.BackColor = System.Drawing.Color.Transparent;
            this.data1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data1.Location = new System.Drawing.Point(962, 19);
            this.data1.Name = "data1";
            this.data1.Size = new System.Drawing.Size(64, 22);
            this.data1.TabIndex = 65;
            this.data1.Text = "label1";
            this.data1.Visible = false;
            // 
            // data2
            // 
            this.data2.AutoSize = true;
            this.data2.BackColor = System.Drawing.Color.Transparent;
            this.data2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data2.Location = new System.Drawing.Point(963, 87);
            this.data2.Name = "data2";
            this.data2.Size = new System.Drawing.Size(64, 22);
            this.data2.TabIndex = 66;
            this.data2.Text = "label1";
            this.data2.Visible = false;
            // 
            // data5
            // 
            this.data5.AutoSize = true;
            this.data5.BackColor = System.Drawing.Color.Transparent;
            this.data5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data5.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data5.Location = new System.Drawing.Point(963, 295);
            this.data5.Name = "data5";
            this.data5.Size = new System.Drawing.Size(64, 22);
            this.data5.TabIndex = 67;
            this.data5.Text = "label1";
            this.data5.Visible = false;
            // 
            // data6
            // 
            this.data6.AutoSize = true;
            this.data6.BackColor = System.Drawing.Color.Transparent;
            this.data6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data6.Location = new System.Drawing.Point(963, 364);
            this.data6.Name = "data6";
            this.data6.Size = new System.Drawing.Size(64, 22);
            this.data6.TabIndex = 69;
            this.data6.Text = "label1";
            this.data6.Visible = false;
            // 
            // data7
            // 
            this.data7.AutoSize = true;
            this.data7.BackColor = System.Drawing.Color.Transparent;
            this.data7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data7.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data7.Location = new System.Drawing.Point(964, 433);
            this.data7.Name = "data7";
            this.data7.Size = new System.Drawing.Size(64, 22);
            this.data7.TabIndex = 70;
            this.data7.Text = "label1";
            this.data7.Visible = false;
            // 
            // data8
            // 
            this.data8.AutoSize = true;
            this.data8.BackColor = System.Drawing.Color.Transparent;
            this.data8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data8.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data8.Location = new System.Drawing.Point(964, 506);
            this.data8.Name = "data8";
            this.data8.Size = new System.Drawing.Size(64, 22);
            this.data8.TabIndex = 71;
            this.data8.Text = "label1";
            this.data8.Visible = false;
            // 
            // data4
            // 
            this.data4.AutoSize = true;
            this.data4.BackColor = System.Drawing.Color.Transparent;
            this.data4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data4.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data4.Location = new System.Drawing.Point(963, 226);
            this.data4.Name = "data4";
            this.data4.Size = new System.Drawing.Size(64, 22);
            this.data4.TabIndex = 72;
            this.data4.Text = "label1";
            this.data4.Visible = false;
            // 
            // data44
            // 
            this.data44.AutoSize = true;
            this.data44.BackColor = System.Drawing.Color.Transparent;
            this.data44.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data44.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data44.Location = new System.Drawing.Point(1033, 227);
            this.data44.Name = "data44";
            this.data44.Size = new System.Drawing.Size(64, 22);
            this.data44.TabIndex = 82;
            this.data44.Text = "label1";
            // 
            // data55
            // 
            this.data55.AutoSize = true;
            this.data55.BackColor = System.Drawing.Color.Transparent;
            this.data55.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data55.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data55.Location = new System.Drawing.Point(1033, 296);
            this.data55.Name = "data55";
            this.data55.Size = new System.Drawing.Size(64, 22);
            this.data55.TabIndex = 81;
            this.data55.Text = "label1";
            // 
            // data88
            // 
            this.data88.AutoSize = true;
            this.data88.BackColor = System.Drawing.Color.Transparent;
            this.data88.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data88.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data88.Location = new System.Drawing.Point(1033, 506);
            this.data88.Name = "data88";
            this.data88.Size = new System.Drawing.Size(64, 22);
            this.data88.TabIndex = 79;
            this.data88.Text = "label1";
            // 
            // data77
            // 
            this.data77.AutoSize = true;
            this.data77.BackColor = System.Drawing.Color.Transparent;
            this.data77.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data77.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data77.Location = new System.Drawing.Point(1033, 434);
            this.data77.Name = "data77";
            this.data77.Size = new System.Drawing.Size(64, 22);
            this.data77.TabIndex = 78;
            this.data77.Text = "label1";
            // 
            // data66
            // 
            this.data66.AutoSize = true;
            this.data66.BackColor = System.Drawing.Color.Transparent;
            this.data66.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data66.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data66.Location = new System.Drawing.Point(1033, 365);
            this.data66.Name = "data66";
            this.data66.Size = new System.Drawing.Size(64, 22);
            this.data66.TabIndex = 77;
            this.data66.Text = "label1";
            // 
            // data33
            // 
            this.data33.AutoSize = true;
            this.data33.BackColor = System.Drawing.Color.Transparent;
            this.data33.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data33.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data33.Location = new System.Drawing.Point(1033, 158);
            this.data33.Name = "data33";
            this.data33.Size = new System.Drawing.Size(64, 22);
            this.data33.TabIndex = 76;
            this.data33.Text = "label1";
            // 
            // data22
            // 
            this.data22.AutoSize = true;
            this.data22.BackColor = System.Drawing.Color.Transparent;
            this.data22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data22.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data22.Location = new System.Drawing.Point(1033, 88);
            this.data22.Name = "data22";
            this.data22.Size = new System.Drawing.Size(64, 22);
            this.data22.TabIndex = 75;
            this.data22.Text = "label1";
            // 
            // data11
            // 
            this.data11.AutoSize = true;
            this.data11.BackColor = System.Drawing.Color.Transparent;
            this.data11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data11.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data11.Location = new System.Drawing.Point(1033, 20);
            this.data11.Name = "data11";
            this.data11.Size = new System.Drawing.Size(64, 22);
            this.data11.TabIndex = 74;
            this.data11.Text = "label1";
            this.data11.Click += new System.EventHandler(this.data11_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(865, 568);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 41);
            this.button3.TabIndex = 83;
            this.button3.Text = "开灯";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(1037, 568);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 41);
            this.button4.TabIndex = 84;
            this.button4.Text = "灌溉开";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // data3
            // 
            this.data3.AutoSize = true;
            this.data3.BackColor = System.Drawing.Color.Transparent;
            this.data3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data3.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.data3.Location = new System.Drawing.Point(962, 157);
            this.data3.Name = "data3";
            this.data3.Size = new System.Drawing.Size(64, 22);
            this.data3.TabIndex = 73;
            this.data3.Text = "label1";
            this.data3.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label25.Location = new System.Drawing.Point(874, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(48, 22);
            this.label25.TabIndex = 42;
            this.label25.Text = "温度";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label18.Location = new System.Drawing.Point(874, 89);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 22);
            this.label18.TabIndex = 48;
            this.label18.Text = "湿度";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label17.Location = new System.Drawing.Point(874, 158);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 22);
            this.label17.TabIndex = 52;
            this.label17.Text = "土壤温度";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label16.Location = new System.Drawing.Point(874, 227);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 22);
            this.label16.TabIndex = 54;
            this.label16.Text = "土壤湿度";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label15.Location = new System.Drawing.Point(874, 296);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 22);
            this.label15.TabIndex = 56;
            this.label15.Text = "水位";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label14.Location = new System.Drawing.Point(874, 365);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 22);
            this.label14.TabIndex = 58;
            this.label14.Text = "光强";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label12.Location = new System.Drawing.Point(874, 434);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 22);
            this.label12.TabIndex = 60;
            this.label12.Text = "LED灯";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label11.Location = new System.Drawing.Point(875, 503);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 22);
            this.label11.TabIndex = 62;
            this.label11.Text = "灌溉";
            // 
            // ID
            // 
            this.ID.Location = new System.Drawing.Point(149, 89);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(152, 25);
            this.ID.TabIndex = 85;
            this.ID.Text = "699680697";
            // 
            // apikey
            // 
            this.apikey.Location = new System.Drawing.Point(149, 227);
            this.apikey.Name = "apikey";
            this.apikey.Size = new System.Drawing.Size(152, 25);
            this.apikey.TabIndex = 86;
            this.apikey.Text = "b=H46=2EmSi8apg=b7tTf6SDgQQ=";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(149, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(152, 25);
            this.textBox1.TabIndex = 88;
            this.textBox1.Text = "699680697";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(46, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 22);
            this.label1.TabIndex = 87;
            this.label1.Text = "产品ID";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(149, 158);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(152, 25);
            this.textBox2.TabIndex = 90;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label2.Location = new System.Drawing.Point(27, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 22);
            this.label2.TabIndex = 91;
            this.label2.Text = "SeverAddr";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 4000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(951, 568);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 41);
            this.button1.TabIndex = 92;
            this.button1.Text = "关灯";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Font = new System.Drawing.Font("宋体", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(1130, 568);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 41);
            this.button2.TabIndex = 93;
            this.button2.Text = "灌溉关";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Preview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1244, 629);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.apikey);
            this.Controls.Add(this.RealPlayWnd);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.data44);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.data55);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.data88);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.data77);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.data66);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.data33);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.data22);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.data11);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.data3);
            this.Controls.Add(this.data4);
            this.Controls.Add(this.data8);
            this.Controls.Add(this.data7);
            this.Controls.Add(this.data6);
            this.Controls.Add(this.data5);
            this.Controls.Add(this.data2);
            this.Controls.Add(this.data1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.textBoxChannel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnPreview);
            this.HelpButton = true;
            this.Name = "Preview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview";
            this.Load += new System.EventHandler(this.Preview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        public void UpdateClientList(string strLogStatus, IntPtr lpDeviceInfo)
        {
            //列表新增报警信息
            labelLogin.Text = "登录状态（异步）：" + strLogStatus;
        }

        public void cbLoginCallBack(int lUserID, int dwResult, IntPtr lpDeviceInfo, IntPtr pUser)
        {
            string strLoginCallBack = "登录设备，lUserID：" + lUserID + "，dwResult：" + dwResult;

            if (dwResult == 0)
            {
                uint iErrCode = CHCNetSDK.NET_DVR_GetLastError();
                strLoginCallBack = strLoginCallBack + "，错误号:" + iErrCode;
            }

            //下面代码注释掉也会崩溃
            if (InvokeRequired)
            {
                object[] paras = new object[2];
                paras[0] = strLoginCallBack;
                paras[1] = lpDeviceInfo;
                labelLogin.BeginInvoke(new UpdateTextStatusCallback(UpdateClientList), paras);
            }
            else
            {
                //创建该控件的主线程直接更新信息列表 
                UpdateClientList(strLoginCallBack, lpDeviceInfo);
            }

        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (textBoxIP.Text == "" || textBoxPort.Text == "" ||
                textBoxUserName.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Please input IP, Port, User name and Password!");
                return;
            }
            if (m_lUserID < 0)
            {

                struLogInfo = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();

                //设备IP地址或者域名
                byte[] byIP = System.Text.Encoding.Default.GetBytes(textBoxIP.Text);
                struLogInfo.sDeviceAddress = new byte[129];
                byIP.CopyTo(struLogInfo.sDeviceAddress, 0);

                //设备用户名
                byte[] byUserName = System.Text.Encoding.Default.GetBytes(textBoxUserName.Text);
                struLogInfo.sUserName = new byte[64];
                byUserName.CopyTo(struLogInfo.sUserName, 0);

                //设备密码
                byte[] byPassword = System.Text.Encoding.Default.GetBytes(textBoxPassword.Text);
                struLogInfo.sPassword = new byte[64];
                byPassword.CopyTo(struLogInfo.sPassword, 0);

                struLogInfo.wPort = ushort.Parse(textBoxPort.Text);//设备服务端口号

                if (LoginCallBack == null)
                {
                    LoginCallBack = new CHCNetSDK.LOGINRESULTCALLBACK(cbLoginCallBack);//注册回调函数                    
                }
                struLogInfo.cbLoginResult = LoginCallBack;
                struLogInfo.bUseAsynLogin = false; //是否异步登录：0- 否，1- 是 

                DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();

                //登录设备 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V40(ref struLogInfo, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Login_V40 failed, error code= " + iLastErr; //登录失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //登录成功
                    MessageBox.Show("Login Success!");
                    btnLogin.Text = "Logout";
                }

            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle >= 0)
                {
                    MessageBox.Show("Please stop live view firstly");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Logout failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                m_lUserID = -1;
                btnLogin.Text = "Login";
            }
            return;
        }

        private void btnPreview_Click(object sender, System.EventArgs e)
        {
            if (m_lUserID < 0)
            {
                MessageBox.Show("Please login the device firstly");
                return;
            }

            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;

                /*if (textBoxID.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }*/


                if (RealData == null)
                {
                    RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                }

                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //预览成功
                    btnPreview.Text = "Stop Live View";
                }
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopRealPlay failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                m_lRealHandle = -1;
                btnPreview.Text = "Live View";

            }
            return;
        }

        public void Data()
        {
            data11.Text = setData(data1.Text);
            data22.Text = setData(data2.Text);
            data33.Text = setData(data3.Text);
            data44.Text = setData(data4.Text);
            data55.Text = setData(data5.Text);
            data66.Text = setData(data6.Text);
            data77.Text = setData(data7.Text);
            data88.Text = setData(data8.Text);
            if (data77.Text == null)
            {
                setData1(data7.Text);
            }
            if (data88.Text == null)
            {
                setData1(data8.Text);
            }
            list1.Add(setData(data1.Text));
            list2.Add(setData(data2.Text));
            list3.Add(setData(data3.Text));
            list4.Add(setData(data4.Text));
            list5.Add(setData(data5.Text));
            list6.Add(setData(data6.Text));
            list7.Add(setData(data7.Text));
            list8.Add(setData(data8.Text));
            /*
            if (Convert.ToInt32(Value2(data66.Text)) < 50)
            {
                Post("led", "1");
                MessageBox.Show("光照强度过低，LED灯开启。");
            }
            */
        }

        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            if (dwBufSize > 0)
            {
                byte[] sData = new byte[dwBufSize];
                Marshal.Copy(pBuffer, sData, 0, (Int32)dwBufSize);

                string str = "实时流数据.ps";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sData, 0, iLen);
                fs.Close();
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            //停止预览 Stop live view 
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                m_lRealHandle = -1;
            }

            //注销登录 Logout the device
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                m_lUserID = -1;
            }

            CHCNetSDK.NET_DVR_Cleanup();

            string path1 = @"d:\a.txt";
            string[] arr1 = list1.ToArray();
            writer(path1, arr1);
            string path2 = @"d:\b.txt";
            string[] arr2 = list2.ToArray();
            writer(path2, arr2);
            string path3 = @"d:\c.txt";
            string[] arr3 = list3.ToArray();
            writer(path3, arr3);
            string path4 = @"d:\d.txt";
            string[] arr4 = list4.ToArray();
            writer(path4, arr4);
            string path5 = @"d:\e.txt";
            string[] arr5 = list5.ToArray();
            writer(path5, arr5);
            string path6 = @"d:\f.txt";
            string[] arr6 = list6.ToArray();
            writer(path6, arr6);
            string path7 = @"d:\g.txt";
            string[] arr7 = list7.ToArray();
            writer(path7, arr7);
            string path8 = @"d:\h.txt";
            string[] arr8 = list8.ToArray();
            writer(path8, arr8);

            Application.Exit();
        }

        private void btnPTZ_Click(object sender, EventArgs e)
        {

        }

        public void VoiceDataCallBack(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser)
        {
            byte[] sString = new byte[dwBufSize];
            Marshal.Copy(pRecvDataBuffer, sString, 0, (Int32)dwBufSize);

            if (byAudioFlag == 0)
            {
                //将缓冲区里的音频数据写入文件 save the data into a file
                string str = "PC采集音频文件.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }
            if (byAudioFlag == 1)
            {
                //将缓冲区里的音频数据写入文件 save the data into a file
                string str = "设备音频文件.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }

        }

        private void btnVioceTalk_Click(object sender, EventArgs e)
        {
            if (m_bTalk == false)
            {
                //开始语音对讲 Start two-way talk
                CHCNetSDK.VOICEDATACALLBACKV30 VoiceData = new CHCNetSDK.VOICEDATACALLBACKV30(VoiceDataCallBack);//预览实时流回调函数

                lVoiceComHandle = CHCNetSDK.NET_DVR_StartVoiceCom_V30(m_lUserID, 1, true, VoiceData, IntPtr.Zero);
                //bNeedCBNoEncData [in]需要回调的语音数据类型：0- 编码后的语音数据，1- 编码前的PCM原始数据

                if (lVoiceComHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StartVoiceCom_V30 failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //btnVioceTalk.Text = "Stop Talk";
                    m_bTalk = true;
                }
            }
            else
            {
                //停止语音对讲 Stop two-way talk
                if (!CHCNetSDK.NET_DVR_StopVoiceCom(lVoiceComHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopVoiceCom failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //btnVioceTalk.Text = "Start Talk";
                    m_bTalk = false;
                }
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void Preview_Load(object sender, EventArgs e)
        {

            ID.Text = "698227736";//设备ID
            apikey.Text = "qfJ6Ufa=eqEPWMgn9wC4F5mIp6U=";//Master-APIkey
            data1.Text = "temp";//数据流名称
            /*
            data2.Text = "humidity";//数据流名称
            data3.Text = "soil_temperature";//数据流名称
            data4.Text = "soil_moisture";//数据流名称
            data5.Text = "water_level";//数据流名称
            data6.Text = "light_intensity";//数据流名称
            data7.Text = "led";//数据流名称
            data8.Text = "fan";//数据流名称
            textBox2.Text = "183.230.40.39";
            */


            Data();
          
            
        }

        private String setData(string text)
        {
            string url = "http://api.heclouds.com/devices/" + ID.Text + "/datapoints?datastream_id=" + text;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
            BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(request.Headers, null) as NameValueCollection;
                collection["api-key"] = apikey.Text;
            }
            request.Host = "api.heclouds.com";
            request.ProtocolVersion = new Version(1, 1);
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return Value(retString) + "   " + DateTime.Now.ToString("MM-dd hh:mm:ss");
        }

        private String setData1(string text)
        {
            string url = "http://api.heclouds.com/devices/" + ID.Text + "/datapoints?datastream_id=" + text;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
            BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(request.Headers, null) as NameValueCollection;
                collection["api-key"] = apikey.Text;
            }
            request.Host = "api.heclouds.com";
            request.ProtocolVersion = new Version(1, 1);
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return Value1(retString) + "   " + DateTime.Now.ToString("MM-dd hh:mm");
        }


        private void label17_Click(object sender, EventArgs e)
        {

        }

        public string Value(string temp)
        {
            string s = null;
            //textBox4.Text = humi;
            for (int i = 1; i < 9; i++)
            {
                if (checkstring(temp.Substring(99, i)) == true)
                {
                    s = temp.Substring(99, i);
                }
                else
                {
                    break;
                }

            }
            return s;

        }

        public string Value1(string temp)
        {
            string s = null;
            //textBox4.Text = humi;
            for (int i = 1; i < 9; i++)
            {
                if (checkstring(temp.Substring(100, i)) == true)
                {
                    s = temp.Substring(100, i);
                }
                else
                {
                    break;
                }

            }
            return s;
        }

        public string Value2(string temp)
        {
            string s = null;
            //textBox4.Text = humi;
            for (int i = 1; i < 9; i++)
            {
                if (checkstring(temp.Substring(0, i)) == true)
                {
                    s = temp.Substring(0, i);
                }
                else
                {
                    break;
                }

            }
            return s;
        }

        private bool checkstring(string a)
        {
            bool isnum = Regex.IsMatch(a, @"^\d+$");
            return isnum;
        }

        public void writer(string path, string[] arr)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sw.WriteLine(arr[i]);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(@"d:\a.txt"))
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sw.WriteLine(arr[i]);
                    }
            }
        }

        public void Post(string id, string swit)
        {
            //pictureBox1.BackColor = Color.Gray;
            string url = "http://api.heclouds.com/devices/706171223/datapoints?";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            SetHeaderValue(request.Headers, "api-key", "5O2f=v0Ej261OwSe90Xsuuz2sBc=");//设备API地址和 首部参数
            request.Host = "api.heclouds.com";
            request.ProtocolVersion = new Version(1, 1);
            string Cod = "{\"datastreams\":[{\"id\":\"" + id + "\",\"datapoints\":[{\"value\":" + swit + "}]}]}";
            byte[] data = Encoding.UTF8.GetBytes(Cod);
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容 
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                //textBox6.Text = reader.ReadToEnd();
                //pictureBox1.BackColor = Color.Lime;
            }
            // return result;
        }//面向OneNet的发送

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)// HTTP协议报文头加入

        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {/*
            String
            EventBus.getDefault().post(new First_event("POST /devices/" + id + "/datapoints" +
                        "?type=3 HTTP/1.1\n" +
                        "api-key:" + apikey + "\n" +
                        "Host:api.heclouds.com\n" +
                        "Content-Length:" + i + "\n" +
                        "\n" +
                        "{" + s + "}\n"));
            */

            try
            { 
                    Post("led", "1");

            }
            catch
            {
                MessageBox.Show("更改失败,请检查设置");
            }
            Data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            try
            {

                    Post("fan", "1");

            }
            catch
            {
                MessageBox.Show("更改失败,请检查设置");
            }    
            Data();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Post("led", "0");
            Data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Post("fan", "0");
            Data();
        }

        private void data11_Click(object sender, EventArgs e)
        {

        }
    }
}
