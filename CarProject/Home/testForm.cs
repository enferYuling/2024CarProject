
using CarProject.Method;
using HZH_Controls.Controls;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CHCNetSDK.CHCNet;
using Emgu.CV;
using System.Text.RegularExpressions;
using Emgu.CV.CvEnum;
using Sunny.UI;
using CarProject.Models;
using System.Windows.Controls;
using GroupBox = System.Windows.Forms.GroupBox;
using HZH_Controls;
using System.Diagnostics;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SharpGL;
using System.Reflection.Emit;
using RosSharp;
using System.Net.WebSockets;
using System.Text.Json;
using System.Net.Http;
using CookComputing.XmlRpc;
using System.Threading;
using Messages.roscpp_tutorials;
using QtCore;
using QtGui;
using QtWidgets;
using System.Collections.Specialized;
using Microsoft.SqlServer.Server;
using Image = System.Drawing.Image;
using System.Windows.Media;
using LibVLCSharp.Shared;
using Vlc.DotNet.Forms;
using FFmpeg.AutoGen;
using System.Drawing.Imaging;
using LibVLCSharp.WinForms;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Net.NetworkInformation;
using System.Net.Sockets;





namespace CarProject.Home
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class testForm : Form
    {
        public readonly SqlSugarClient db;
        public LJTSMethod ljtsmethod;
        public HomeMethod homemethod;
        public string[] rosdata;
        public testForm(SqlSugarClient datadb)
        {
            InitializeComponent();
          
            //m_bInitSDK = NET_DVR_Init();
            //if (m_bInitSDK == false)
            //{
            //    MessageBox.Show("NET_DVR_Init error!");
            //    return;
            //}
            //else
            //{
            //    //保存SDK日志 To save the SDK log
            //    NET_DVR_SetLogToFile(3, "E:\\项目\\2024CarProject\\CarProject\\SDKLog", true);
            //}
            this.db = datadb;
            this.ljtsmethod = new LJTSMethod(db);
            homemethod = new HomeMethod(db);
           
        }
      
        #region 摄像头
        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private bool m_bTalk = false;
        private Int32 m_lRealHandle = -1;
        private int lVoiceComHandle = -1;
        private string str;

        REALDATACALLBACK RealData = null;
        LOGINRESULTCALLBACK LoginCallBack = null;
        public NET_DVR_PTZPOS m_struPtzCfg;
        public NET_DVR_USER_LOGIN_INFO struLogInfo;
        public NET_DVR_DEVICEINFO_V40 DeviceInfo;
        #endregion
        private void testForm_Load(object sender, EventArgs e)
        {
            
            //var res = GetSPZSX();
            //if (!string.IsNullOrEmpty(res))
            //{
            //    MessageBox.Show(res);


            //    return;
            //}
            //else
            //{


            //}
            // ShowTextOverVideo("Hello, Hikvision!", Color.White);

            // LoadData();
            // SWCJ();
            //  InitializeRos();
            // ZT();
            //  imgori = Hori_Line();
            //DoubleBuffered = true;
            //videoView1.Enabled = false;
        }
        public void LoadData()
        {
            var list = this.db.Queryable<Pro_sheltersInfo>().ToList();

            // 使用LINQ查询对DataGridView进行分组
            var groupedData = from Pro_sheltersInfo row in list
                              group row by row.CreateDate into grp
                              select new
                              {
                                  GroupName = grp.Key,
                                  GroupData = grp.ToList()
                              };

            // 清空DataGridView中的数据
            //  YC_GridView.DataSource=null;

            // 将分组后的数据重新添加到DataGridView中
            // 假设我们要创建5个GroupBox
            const int numberOfGroupBoxes = 5;
            const int groupBoxSpacing = 10; // 组间距
            int groupBoxPosition = 10; // 组的起始位置

            foreach (var group in groupedData)
            {
                GroupBox groupBox = new GroupBox();
                // ' 设置GroupBox属性
                groupBox.Height = 150;
                groupBox.Location = new Point(groupBoxSpacing, groupBoxPosition);

                groupBox.Text = group.GroupName.ToDate().ToString("yyyy年MM月dd日");
                /// ' 设置DataGridView属性
                DataGridView dataGridView = new DataGridView();
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.Parent = groupBox;
                dataGridView.DataSource = group.GroupData;
                this.Controls.Add(groupBox);
                // 更新组的位置
                groupBoxPosition += groupBox.Height + groupBoxSpacing;
            }
        }
        #region 视频
        public NET_DVR_PREVIEWINFO lpPreviewInfo;
        public NET_DVR_PREVIEWINFO lpPreviewInfo1;
        public string GetSPZSX()
        {
            NET_DVR_USER_LOGIN_INFO SDK = ljtsmethod.GetSPZSX("192.168.2.64", "8000", "admin", "Admin123");

            if (SDK.sDeviceAddress.Count() <= 0)
            {
                return "初始化摄像头失败";
            }
            if (LoginCallBack == null)
            {
                LoginCallBack = new LOGINRESULTCALLBACK(cbLoginCallBack);//注册回调函数                    
            }
            SDK.cbLoginResult = LoginCallBack;
            SDK.bUseAsynLogin = false; //是否异步登录：0- 否，1- 是 


            DeviceInfo = new NET_DVR_DEVICEINFO_V40();
            //登录设备 Login the device
            m_lUserID = NET_DVR_Login_V40(ref SDK, ref DeviceInfo);
            if (m_lUserID < 0)
            {
                iLastErr = NET_DVR_GetLastError();
                return "NET_DVR_Login_V40 failed, error code= " + iLastErr;//登录失败，输出错误号
            }
            else
            {
                if (m_lRealHandle < 0)
                {

                    lpPreviewInfo = new NET_DVR_PREVIEWINFO();
                   // lpPreviewInfo.hPlayWnd = pictureBox1 .Handle;//预览窗口
                    lpPreviewInfo.lChannel = Int16.Parse("1");//预te览的设备通道
                    lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                    lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                    lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                    lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                    lpPreviewInfo.byProtoType = 0;
                    lpPreviewInfo.byPreviewMode = 0;

                    // lpPreviewInfo1 = new NET_DVR_PREVIEWINFO();
                    //lpPreviewInfo1.hPlayWnd = rcx_img.Handle;//预览窗口
                    //lpPreviewInfo1.lChannel = Int16.Parse("2");//预te览的设备通道
                    //lpPreviewInfo1.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                    //lpPreviewInfo1.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                    //lpPreviewInfo1.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                    //lpPreviewInfo1.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                    //lpPreviewInfo1.byProtoType = 0;
                    //lpPreviewInfo1.byPreviewMode = 0;
                    if (RealData == null)
                    {
                        RealData = new REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                    }

                    IntPtr pUser = new IntPtr();//用户数据

                    //打开预览 Start live view 
                    m_lRealHandle = NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                    m_lRealHandle = NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo1, null/*RealData*/, pUser);
                    if (m_lRealHandle < 0)
                    {
                        iLastErr = NET_DVR_GetLastError();

                        return "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号;
                    }
                }
            }
            
            return "";
        }
        
            /// <summary>
            /// 登录设备
            /// </summary>
            /// <param name="lUserID"></param>
            /// <param name="dwResult"></param>
            /// <param name="lpDeviceInfo"></param>
            /// <param name="pUser"></param>
            public void cbLoginCallBack(int lUserID, int dwResult, IntPtr lpDeviceInfo, IntPtr pUser)
        {
            bool m_bInitSDK = NET_DVR_Init();
            string strLoginCallBack = "登录设备，lUserID：" + lUserID + "，dwResult：" + dwResult;

            if (dwResult == 0)
            {
                uint iErrCode = NET_DVR_GetLastError();
                strLoginCallBack = strLoginCallBack + "，错误号:" + iErrCode;
            }

            //下面代码注释掉也会崩溃
            if (InvokeRequired)
            {
                object[] paras = new object[2];
                paras[0] = strLoginCallBack;
                paras[1] = lpDeviceInfo;
            }
            else
            {
                //创建该控件的主线程直接更新信息列表 
                //UpdateClientList(strLoginCallBack, lpDeviceInfo);
            }

        }
        /// <summary>
        /// 预览实时流回调函数
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="pUser"></param>
        IntPtr intPtr1 = IntPtr.Zero;
        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            if (dwBufSize > 0)
            {
                intPtr1 = pBuffer;
                byte[] sData = new byte[dwBufSize];
                Marshal.Copy(pBuffer, sData, 0, (Int32)dwBufSize);

                string str = "实时流数据.ps";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sData, 0, iLen);
                fs.Close();
            }
        }
        //获取网速
        private void DownloadSpee(double MB)
        {
          //  var query = Directory.GetCurrentDirectory().Split("\\");

            const string downloadUrl = "file:///E://项目//2024CarProject//CarProject//img//206搜索.png";
            WebClient client = new WebClient();

            Stopwatch stopwatch = new Stopwatch();

            // 每次循环重新开始计时
            stopwatch.Restart();

            try
            {
                client.DownloadFile(downloadUrl, "tempFile");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            stopwatch.Stop();

            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            double fileSizeInMB = MB;  // 假设下载的文件大小为 nMB
            double downloadSpeedMbps = fileSizeInMB / elapsedSeconds;//单位为MB
            MessageBox.Show($"Download Speed: {downloadSpeedMbps:F2} MB");
            if (downloadSpeedMbps > 20)
            {
                SPFB(0, 0, 27);
            }
            else
            {
                SPFB(12, 3, 19);
            }
        }
        public NET_DVR_COMPRESSIONCFG m_struspCfg;
        public void SPFB(byte zl, byte txzl, byte fbl)
        {
            #region 获取参数
            uint Return = 0;
            Int32 Size = Marshal.SizeOf(m_struspCfg);
            IntPtr ptrspCfg = Marshal.AllocHGlobal(Size);
            Marshal.StructureToPtr(m_struspCfg, ptrspCfg, false);
            bool Ret;
            Ret = NET_DVR_GetDVRConfig(m_lUserID, NET_DVR_GET_COMPRESSCFG, 1, ptrspCfg, (UInt32)Size, ref Return);
            if (!Ret)
            {
                iLastErr = NET_DVR_GetLastError();
                str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr;
                MessageBox.Show("失败" + str);
            }
            else
            {
                MessageBox.Show("成功");
                m_struspCfg = (NET_DVR_COMPRESSIONCFG)Marshal.PtrToStructure(ptrspCfg, typeof(NET_DVR_COMPRESSIONCFG));

                //分辨率 19-1280*720p    20-1280*960   27-1920*1080p
                string str2 = m_struspCfg.struRecordPara.byResolution.ToString();
                // textBox7.Text = str2;



            }
            #endregion
            #region 设置参数
            //帧率
            m_struspCfg.struRecordPara.dwVideoFrameRate = zl;
            //图像质量
            m_struspCfg.struRecordPara.byPicQuality = txzl;
            //分辨率
            m_struspCfg.struRecordPara.byResolution = fbl;

            Marshal.StructureToPtr(m_struspCfg, ptrspCfg, false);

            Ret = NET_DVR_SetDVRConfig(m_lUserID, NET_DVR_SET_COMPRESSCFG, 1, ptrspCfg, (UInt32)Size);
            if (!Ret)
            {
                iLastErr = NET_DVR_GetLastError();
                str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr;
                MessageBox.Show("设置失败" + str);
            }
            else
            {
                MessageBox.Show("成功设置");

            }
            Marshal.FreeHGlobal(ptrspCfg);
            #endregion
        }
        #endregion
        #region 三维建模
        /// <summary>
        /// 默认绘画模式为线条
        /// </summary>
        private uint _model = OpenGL.GL_POINTS;
        //GL_LINE_LOOP
        /// <summary>
        /// X轴坐标
        /// </summary>
        private float _x = 0;

        /// <summary>
        /// Y轴坐标
        /// </summary>
        private float _y = 0;

        /// <summary>
        /// Z轴坐标
        /// </summary>
        private float _z = 0;

        #endregion
    
        private void PtzGet_Click(object sender, EventArgs e)
        {
            UInt32 dwReturn = 0;
            Int32 nSize = Marshal.SizeOf(m_struPtzCfg);
            IntPtr ptrPtzCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struPtzCfg, ptrPtzCfg, false);
            //获取参数失败
            if (!NET_DVR_GetDVRConfig(m_lUserID, NET_DVR_GET_PTZPOS, -1, ptrPtzCfg, (UInt32)nSize, ref dwReturn))
            {
                iLastErr = NET_DVR_GetLastError();
                str = "NET_DVR_GetDVRConfig failed, error code= " + iLastErr;
                MessageBox.Show(str);
                return;
            }
        }
       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _model = OpenGL.GL_LINE_LOOP;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _model = OpenGL.GL_QUADS;
        }

        private void openGLControl1_GDIDraw(object sender, RenderEventArgs args)
        {
            // 创建一个GL对象
            SharpGL.OpenGL gl = new OpenGL();

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);	// 清空屏幕
            gl.LoadIdentity();					// 重置
            gl.Translate(0.0f, 0.0f, -6.0f);    // 设置坐标，距离屏幕距离为6

            gl.Rotate(_x, 50.0f, 0.0f, 0.0f);	// 绕X轴旋转
            gl.Rotate(_y, 0.0f, 50.0f, 0.0f);	// 绕Y轴旋转
            gl.Rotate(_z, 0.0f, 0.0f, 50.0f);    // 绕Z轴旋转

            gl.Begin(_model);				    // 绘制立方体
            gl.Color(0.0f, 1.0f, 0.0f);         // 设置颜色
            var cou = rosdata.Count() / 3;
            var grouped = rosdata.Select((item, index) => new { Item = item, Index = index })
                              .GroupBy(x => x.Index / 3)
                              .Select(g => g.Select(x => x.Item).ToArray());
            foreach (var group in grouped)
            {
               // gl.Color(group[0].ToFloat(), group[1].ToFloat(), group[2].ToFloat());
                gl.Vertex(group[0].ToFloat(), group[1].ToFloat(), group[2].ToFloat());
            }
            //gl.Vertex(1.0f, 1.0f, -1.0f);
            //gl.Vertex(-1.0f, 1.0f, -1.0f);
            //gl.Vertex(-1.0f, 1.0f, 1.0f);
            ////  gl.Vertex(1.0f, 1.0f, 1.0f);

            ////如下类同
            //gl.Color(1.0f, 0.5f, 0.0f);
            //gl.Vertex(1.0f, -1.0f, 1.0f);
            //gl.Vertex(-1.0f, -1.0f, 1.0f);
            //gl.Vertex(-1.0f, -1.0f, -1.0f);
            ////   gl.Vertex(1.0f, -1.0f, -1.0f);

            //gl.Color(1.0f, 0.0f, 0.0f);
            //gl.Vertex(1.0f, 1.0f, 1.0f);
            //gl.Vertex(-1.0f, 1.0f, 1.0f);
            //gl.Vertex(-1.0f, -1.0f, 1.0f);
            ////   gl.Vertex(1.0f, -1.0f, 1.0f);

            //gl.Color(1.0f, 1.0f, 0.0f);
            //gl.Vertex(1.0f, -1.0f, -1.0f);
            //gl.Vertex(-1.0f, -1.0f, -1.0f);
            //gl.Vertex(-1.0f, 1.0f, -1.0f);
            ////   gl.Vertex(1.0f, 1.0f, -1.0f);

            ////gl.Color(0.0f, 0.0f, 1.0f);
            ////gl.Vertex(-1.0f, 1.0f, 1.0f);
            ////gl.Vertex(-1.0f, 1.0f, -1.0f);
            ////gl.Vertex(-1.0f, -1.0f, -1.0f);
            ////gl.Vertex(-1.0f, -1.0f, 1.0f);

            ////gl.Color(1.0f, 0.0f, 1.0f);
            ////gl.Vertex(1.0f, 1.0f, -1.0f);
            ////gl.Vertex(1.0f, 1.0f, 1.0f);
            ////gl.Vertex(1.0f, -1.0f, 1.0f);
            ////gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.End();                       // 结束绘制

        }

        
        private async void InitializeRos()
        {

            try
            {
                string processName = "RosDotNet";

                // 找到所有匹配的进程
                Process[] processes = Process.GetProcessesByName(processName);

                // 遍历并关闭每个找到的进程
                foreach (Process process in processes)
                {
                    try
                    {
                        process.Kill(); // 关闭进程
                       process.WaitForExit(); // 等待进程退出
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"无法关闭进程: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            // 设置启动参数
            string consoleAppPath = @"E:\项目\2024CarProject\RosDotNet\RosDotNet\bin\Debug\net8.0\RosDotNet.exe"; // 控制台应用程序的完整路径

            ProcessStartInfo startInfo = new ProcessStartInfo(consoleAppPath)
            {
                UseShellExecute = false, // 不使用操作系统外壳程序启动
                RedirectStandardOutput = true, // 重定向标准输出
                RedirectStandardError = true, // 重定向标准错误
                CreateNoWindow = true // 不创建新窗口
            };
            Process process1= Process.Start(startInfo);
            //this.Visible = true;
            //using (Process process = Process.Start(startInfo))
            //{
            //    // 可以读取控制台程序的输出
         
            var output = process1.StandardOutput.ReadLine();
               process1.Kill();
           var ros=output.Split(':');
            if (ros.Length ==7)
            {
                var str1 = ros[7].Replace("[", "").Replace("]", "");
                rosdata = str1.Split(", ");
                var q = rosdata.Count();
            }
        }
        private static void subCallback(Messages.sensor_msgs.Image argument)
        {
          MessageBox.Show( argument.data.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            
        }
        public void Loadjgdt()
        {

        }
        public void LoadDT()
        {
            string str_url = Application.StartupPath + "\\test.html"; //地图的路径+名称
            Uri url = new Uri(str_url);
           // webBrowser1.Navigate(url);
            
            //webBrowser1.Url = url;
            //webBrowser1.ObjectForScripting = this;
        }
        /// <summary>
        /// 
        /// </summary>
        public void DYT()
        {

        }
        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _model = OpenGL.GL_POINTS;
        }

       
        private void ShowTextOverVideo(string text, System.Drawing.Color textColor)
        {
            //using (Graphics graphics = pictureBox1.CreateGraphics())
            //{
            //    using (Font arialFont = new Font("Arial", 20, FontStyle.Bold))
            //    {
            //        using (Brush whiteBrush = new SolidBrush(textColor))
            //        {
            //            PointF point = new PointF(10f, 10f);
            //            graphics.DrawString(text, arialFont, whiteBrush, point);
            //        }
            //    }
            //}
        }
        #region 姿态
        private float roll = 0; // 横滚角
        private float pitch = 0; // 俯仰角
                                 // 声明 PrintWindow 函数

        private ArtificialHorizon artificialHorizon;
        public Image imgtmp;
        public Image imgori;
        public Bitmap bitmp;
        public Bitmap bm;
        public bool IsSure = false;

        double DbPitchAngle = new double(); //俯仰角度[-90,90]
        double DbRowAngle = new double();   //滚转角度[-180,180]
        double DbYawAngle = new double();    //航向角度[-45,45]
                                             //重绘旋转后的仪表盘图片 
        public void Overlap(Bitmap btm, int x, int y, int w, int h)
        {
            Bitmap image = new Bitmap(btm);
            Bitmap hi = new Bitmap(bm);
            Graphics g = Graphics.FromImage(hi);
            g.DrawImage(image, new Rectangle(x, y, w, h));
            bm = hi;
        }

       

        //-------------------地平仪刻度划线函数-------------------//
        //入口参数：无       
        //public Image Hori_Line()
        //{
        //    bitmp = new Bitmap(HoriBox.Width, HoriBox.Height);
        //    System.Drawing.Graphics gscale = System.Drawing.Graphics.FromImage(bitmp);
        //    System.Drawing.Pen p1 = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
        //    System.Drawing.Pen p2 = new System.Drawing.Pen(System.Drawing.Color.Green, 2);

        //    //准心绘线
            
        //    var q2 = (350 - HoriBox.Width)/2;
        //    var q1 = 350 - HoriBox.Width;
        //   gscale.DrawEllipse(Pens.Red ,165- q2, 165- q2, 20, 20);
            
        //    gscale.DrawLine(p2, 170- q2, 175- q2, 185- q2, 175 - q2);
        //    gscale.DrawLine(p1, 170 - q2, 185 - q2, 177 - q2, 175 - q2);
        //    gscale.DrawLine(p1, 185 - q2, 185 - q2, 177 - q2, 175 - q2);
        //    //滚转刻度线
        //    gscale.DrawEllipse(Pens.White, 35, 35, 280- q1, 280-q1);
        //    int i, i1, j, j1, k;
        //    for (k = 0; k < 73; k++)
        //    {
        //        i = Convert.ToInt32((130- q2) * Math.Cos(k * Math.PI / 36) +( 175- q2));
        //        j = Convert.ToInt32((130 - q2) * Math.Sin(k * Math.PI / 36) + (175 - q2));
        //        if (k % 2 == 0)
        //        {
        //            i1 = Convert.ToInt32((155-q2) * Math.Cos(k * Math.PI / 36) + (175-q2));
        //            j1 = Convert.ToInt32((155 - q2) * Math.Sin(k * Math.PI / 36) + (175 - q2));
        //        }
        //        else
        //        {
        //            i1 = Convert.ToInt32((150-q2) * Math.Cos(k * Math.PI / 36) + (175-q2));
        //            j1 = Convert.ToInt32((150 - q2) * Math.Sin(k * Math.PI / 36) + (175 - q2));
        //        }
        //        gscale.DrawLine(Pens.White, i, j, i1, j1);
        //    }
        //    gscale.Dispose();
        //    return bitmp;
        //}

        /// <summary>
        //-------------------地平仪显示函数-------------------//
        //入口参数：
        //俯仰角 pitch_angle 范围-90~90 度 
        //滚动角 row_angle   范围-90~90 度
        /// </summary>
        /// <param name="pitch_angle"></param>
        /// <param name="row_angle"></param>
        //private void Hori_Disp(double pitch_angle, double row_angle)
        //{
        //    //1地平仪图像载入带平移
        //    int pic_position;
        //    double q1 = (HoriBox.Width.ToDouble() / 90);
        //    pic_position = Convert.ToInt32(pitch_angle * q1);
            
        //    try
        //    {
        //        imgtmp = new Bitmap(Properties.Resources.刻度1);
        //        row_angle = row_angle % 360;
        //        //弧度转换  
        //        double ar = 2;
        //        double radian = (row_angle - 90) * Math.PI / 180.0;
        //        double radiana = (row_angle - 90 - ar) * Math.PI / 180.0;
        //        double radianc = (row_angle - 90 + ar) * Math.PI / 180.0;
        //        double cos = Math.Cos(radian);
        //        double cosa = Math.Cos(radiana);
        //        double cosc = Math.Cos(radianc);
        //        double sin = Math.Sin(radian);
        //        double sina = Math.Sin(radiana);
        //        double sinc = Math.Sin(radianc);
        //        //目标位图
        //        Bitmap dsImage = new Bitmap(HoriBox.Width, HoriBox.Height);
        //        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        //计算偏移量
        //        int q2 = (350 - HoriBox.Width) / 2;
        //        int q3 = (350 - HoriBox.Width) * 2;
        //        Rectangle rect = new Rectangle(-175+ q2, -175+ q2 + pic_position, 700- q3, 700- q3);
        //        g.TranslateTransform(175- q2, 175- q2);
        //        g.RotateTransform((float)row_angle);
        //        //恢复图像在水平和垂直方向的平移 
        //        g.TranslateTransform(-175+ q2, -175+ q2);
        //        g.DrawImage(imgtmp, rect);
        //        //重至绘图的所有变换  
        //        g.ResetTransform();
        //        g.Dispose();
        //        //保存旋转后的图片
        //        bm = dsImage;
        //        //调用imgori 已经是画好的刻度盘
        //        Bitmap bitmp = new Bitmap(imgori);
        //        Overlap(bitmp, 0, 0, HoriBox.Width, HoriBox.Width);
        //        Bitmap pointImage = new Bitmap(HoriBox.Width, HoriBox.Height);
        //        // 指针设计 ;
        //        System.Drawing.Graphics gPoint = System.Drawing.Graphics.FromImage(pointImage);
        //        SolidBrush h = new SolidBrush(System.Drawing.Color.Red);

        //        //Point a = new Point(Convert.ToInt32(175 + 131 * cosa)-25, Convert.ToInt32(180 + 131 * sina)-25);
        //        //Point b = new Point(Convert.ToInt32(141 * cos + 175)-25, Convert.ToInt32(141 * sin + 175) - 25);
        //        //Point c = new Point(Convert.ToInt32(175 + 131 * cosc)-25, Convert.ToInt32(180 + 131 * sinc) - 25);

        //        Point a = new Point(Convert.ToInt32(175- q2 + (131- q2) * cosa), Convert.ToInt32(180- q2 + (131- q2) * sina));
        //        Point b = new Point(Convert.ToInt32((141- q2) * cos + (175- q2)), Convert.ToInt32((141- q2) * sin + (175- q2)));
        //        Point c = new Point(Convert.ToInt32(175- q2 + (131- q2) * cosc), Convert.ToInt32(180- q2 + (131- q2) * sinc));

        //        Point[] pointer = { a, b, c };
        //        gPoint.FillPolygon(h, pointer);
                


        //        Overlap(pointImage, 0, 0, HoriBox.Width, HoriBox.Height);
        //        HoriBox.Image = bm;
        //        g.Dispose();
        //        imgtmp.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
        //    gp.FillMode = 0;
        //    gp.AddEllipse(new Rectangle(0, 0, HoriBox.Width, HoriBox.Height));
        //    HoriBox.Region = new Region(gp);
        //    gp.Dispose();
        //}
      
         
      
        public void ZT()
        {
            try
            {

                //string processName = "property";

                //// 找到所有匹配的进程
                //Process[] processes = Process.GetProcessesByName(processName);

                //// 遍历并关闭每个找到的进程
                //foreach (Process process in processes)
                //{
                //    try
                //    {
                //        process.Kill(); // 关闭进程
                //        process.WaitForExit(); // 等待进程退出
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine($"无法关闭进程: {ex.Message}");
                //    }
                //}
                //// 设置启动参数
                //string consoleAppPath = @"D:\BaiduNetdiskDownload\Qt自定义控件\bin_quc\bin_quc\property.exe"; // 控制台应用程序的完整路径

                //ProcessStartInfo startInfo = new ProcessStartInfo(consoleAppPath)
                //{
                //    UseShellExecute = false, // 不使用操作系统外壳程序启动
                //    RedirectStandardOutput = true, // 重定向标准输出
                //    RedirectStandardError = true, // 重定向标准错误
                //    CreateNoWindow = true // 不创建新窗口
                //};
                //Process process1 = Process.Start(startInfo);
                //process1.Kill();

               


            }
            catch (Exception ex)
            {

            }
        }



        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //double DbPitchAngle = 0; //俯仰角度[-90,90]
            //double DbRowAngle = 0;   //滚转角度[-180,180]
            //double DbYawAngle = 0;   //航向角度[-45,45]
            //Random pR = new Random();
            //Random rR = new Random();
            //Random yR = new Random();
            //DbPitchAngle = pR.Next(0, 90);
            //DbRowAngle = rR.Next(-180, 180);
            //DbYawAngle = yR.Next(-45, 45);
            //Hori_Disp(DbPitchAngle, DbRowAngle);

            //Hori_Line();
            DownloadSpee();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          //  double DbPitchAngle = textBox1.Text.ToDouble(); //俯仰角度[-90,90]
          //  double DbRowAngle = textBox2.Text.ToDouble(); //俯仰角度[-90,90]
          //  Hori_Disp(DbPitchAngle, DbRowAngle);
          // // Hori_Disp1(DbPitchAngle, DbRowAngle);
          //  Hori_Line();
          ////  Hori_Line1();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
          //  double DbPitchAngle = textBox1.Text.ToDouble();  
          //  double DbRowAngle = textBox2.Text.ToDouble();  
          //  Hori_Disp(DbPitchAngle, DbRowAngle);
          //// Hori_Disp1(DbPitchAngle, DbRowAngle);
          //  Hori_Line();
          ////  Hori_Line1();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
          //  HtmlDocument doc = webBrowser1.Document; 
           // doc.InvokeScript("resizeToFit", new object[] { webBrowser1.ClientSize.Width, webBrowser1.ClientSize.Height });
        }
        #region 视频推流
        
        public void SPLL()
        {

        } 
        /// <summary>
        /// VLC视频播放
        /// </summary>
        private  void InitializeVLC()
        {
            Core.Initialize("D:\\VideoLAN\\VLC");

            var libVLC = new LibVLC();
            var mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(libVLC)
            {
                // 设置初始缓冲大小（单位：毫秒）
                // 增加缓冲大小可以减少延迟，但可能会增加延迟的累积
                NetworkCaching =200 // 例如，设置缓冲区为5秒
            };


            videoView1.MediaPlayer = mediaPlayer;

            videoView1.Dock = DockStyle.Fill;
            //var media = new Media(libVLC, new Uri("rtsp://admin:lwny1234@115.236.153.177:53672/h264/ch1/main/av_stream"));
            var media = new Media(libVLC, new Uri("rtsp://admin:lwny1234@5u4zkzk8e.shenzhuo.vip:10393/h264/ch1/main/av_stream"));
            mediaPlayer.Play(media);
            // 设置 RTSP 流地址
            //   mediaPlayer.Play(new Media(libVLC, "rtsp://8.137.119.17:554/live/test1"));
        }
        private AVFormatContext? formatContext;
        private AVCodecContext? codecContext;
        private AVStream? videoStream;
        private SwsContext? swsContext;
        private AVFrame? frame;
        private AVFrame? rgbFrame;
        private Bitmap bitmap;
        private System.Timers.Timer timer;
        /// <summary>
        /// FFMPEG视频播放
        /// </summary>
        private void InitializeFFMPEG()
        {
            string rtspUrl = "your_rtsp_url";

            // 初始化 FFmpeg 库
            ffmpeg.av_register_all();
           
        }
        private void EXE()
        {
            appContainer1.AppFilename = @"E:\项目\2024CarProject\CarProject\bin\Debug\fcsxt1\EasyPlayer.exe"; 
            appContainer2.AppFilename = @"E:\项目\2024CarProject\CarProject\bin\Debug\fcsxt2\EasyPlayer.exe"; 
            appContainer3.AppFilename = @"E:\项目\2024CarProject\CarProject\bin\Debug\fcsxt3\EasyPlayer.exe"; 
            appContainer1.Start();
            appContainer2.Start();
            appContainer3.Start();
        }

        private unsafe void PlayRtspStream(string rtspUrl)
        {
            
        }
        private unsafe Bitmap ConvertFrameToBitmap(AVFrame* frame)
        {
            int width = frame->width;
            int height = frame->height;

            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int linesize = frame->linesize[0];
            byte* data = (byte*)frame->data[0];

            for (int y = 0; y < height; y++)
            {
                System.Buffer.MemoryCopy(data + y * linesize, (void*)(bitmapData.Scan0 + y * bitmapData.Stride), bitmapData.Stride, linesize);
            }

            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }
        #endregion
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        private void button1_Click(object sender, EventArgs e)
        {
            //InitializeVLC();
            //appContainer1.AppFilename = @"E:\\刘玉玲\\WeChat Files\\wxid_wx7gylqo74fa22\\FileStorage\\File\\2024-09\\XPlayer\\XPlayer\\ZQTool.exe";

            //appContainer1.Start();

             EXE();
           
            DownloadSpee();
            timer1.Start();
        }

        private void appContainer1_Resize(object sender, EventArgs e)
        {

        }
       
        //获取网速
        private void DownloadSpee()
        {
            var network = NetworkInfo.TryGetRealNetworkInfo("");
            var oldRate = network.GetIpv4Speed();

            Thread.Sleep(1000);
            var newRate = network.GetIpv4Speed();
            var speed = NetworkInfo.GetSpeed(oldRate, newRate);
            oldRate = newRate;

            label1.Text = $"上传：{speed.Sent.Size} {speed.Sent.SizeType}/S    下载：{speed.Received.Size} {speed.Received.SizeType}/S";

        }


    }
}
