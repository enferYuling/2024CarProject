using CarProject.Method;
using HZH_Controls.Controls;
using HZH_Controls;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CHCNetSDK.CHCNet;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using CarProject.Models;
using MultiCardCS;
using System.Diagnostics;
using System.Net;
using System.IO.Ports;
using SharpGL;
using Sunny.UI.Win32;
using Sunny.UI;




namespace CarProject.Home
{
    public partial class XCCZYM : Form
    {
        int w;//定义当前窗体的宽度
        int h;//定义当前窗体的高度
        public readonly SqlSugarClient db;

        public LJTSMethod ljtsmethod;
        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private bool m_bTalk = false;
        private Int32 m_lRealHandle = -1;
        private int lVoiceComHandle = -1;
        private string str;

       public REALDATACALLBACK RealData = null;
       public LOGINRESULTCALLBACK LoginCallBack = null;
        public NET_DVR_PTZPOS m_struPtzCfg;
        public NET_DVR_USER_LOGIN_INFO struLogInfo;
        public NET_DVR_DEVICEINFO_V40 DeviceInfo;
        public double wlSpeed;
        public HomeMethod homemethod;
        //声明卡对象，如果有多个卡，可以声明多个对象
        MultiCardCS.MultiCardCS MultiCardCS_1 = new MultiCardCS.MultiCardCS();
        //本机ip
        public string IP;
        public XCCZYM(SqlSugarClient datadb)
        {
            InitializeComponent();
            homemethod = new HomeMethod(db);
            m_bInitSDK = NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                //保存SDK日志 To save the SDK log
                NET_DVR_SetLogToFile(3, "E:\\项目\\2024CarProject\\CarProject\\SDKLog", true);
            }
            //拿到屏幕的长和宽
            w = System.Windows.Forms.SystemInformation.VirtualScreen.Width;
            h = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
            this.db = datadb;
            this.ljtsmethod = new LJTSMethod(db);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cons"></param>
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="newx"></param>
        /// <param name="newy"></param>
        /// <param name="cons"></param>
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    //修改字体也随 窗体大小改变而改变
                    //Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    //con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        private void XCCZYM_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / w;
            float newy = (this.Height) / h;
            setControls(newx, newy, this);
        }

        private void XCCZYM_Load(object sender, EventArgs e)
        {
            IP = homemethod.GetLocalIPAddress();
            //wlSpeed = this.Tag.ToDouble();
            //var res = GetSPZSX();
            //if (!string.IsNullOrEmpty(res))
            //{
            //    MessageBox.Show(res);
            //    rcxzt_lab.Text = "无信号";
            //    return;
            //}
            //else
            //{
            //    wltimer.Start();
                
            //    if (wlSpeed > 20)
            //    {
            //        DownloadSpee(wlSpeed);
            //        rcxzt_lab.Text = "已连接";
            //    }
            //    else
            //    {
            //        DownloadSpee(wlSpeed);
            //        rcxzt_lab.Text = "网络信号差";
            //    }


            //}
            //IO();
            // QBZ();
            // HBZ();
            //  DKBK();
           // InitializeRos();
            uiRoundMeter1.BackgroundImage = zt_imageList.Images[1];
            uiRoundMeter1.AngleImage = zt_imageList.Images[0];
           
            sd_lab.Parent = zsxt_img;
            dy_lab.Parent = zsxt_img;
            dl_lab.Parent = zsxt_img;
            zb_lab.Parent = zsxt_img;
            dw_lab.Parent = zsxt_img;
            label50.Parent = openGLControl1;
            dytzt_lab.Parent = openGLControl1;
            label51.Parent = pictureBox3;
            rcxzt_lab.Parent = pictureBox3;
            label53.Parent = FX_IMG;
            label54.Parent = FX_IMG;
            label55.Parent = FX_IMG;
            label56.Parent = FX_IMG;
            label57.Parent = FX_IMG;
            label58.Parent = FX_IMG;
            label59.Parent = FX_IMG;
            label60.Parent = FX_IMG;
        }
        #region 前避障
        /// <summary>
        /// 前避障
        /// </summary>
        public async void QBZ()
        {
            CONNECTfunction("192.168.2.80", 10137);
            NetworkCommunication1 communicator = new NetworkCommunication1();
            using (NetworkStream stream = tcpClient.GetStream())
            {
                // 获取NetworkStream用于发送和接收
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    while (true)
                    {
                        // 接收数据
                        byte[] receivedData = await homemethod.ReceiveBytesAsync(networkStream, 1024);
                        if (receivedData != null && receivedData.Length > 0)
                        {
                            // 处理接收到的数据
                            AddBytes(receivedData); // 添加到处理器                                                                                
                            var qbzld = QBZBytes1();
                            foreach (var status in qbzld)
                            {
                                double first = status.QTONGDAO1;

                                //TextBoxChannel1.Text = "前通道1:" + first.ToString() + "m";//前通道1的值
                                double second = status.QTONGDAO2;

                                //TextBoxChannel2.Text = "前通道2:" + second.ToString() + "m";//前通道2的值
                                double third = status.QTONGDAO3;

                                //TextBoxChannel3.Text = "前通道3:" + third.ToString() + "m";//前通道3的值
                                double fourth = status.QTONGDAO4;

                                //TextBoxChannel4.Text = "前通道4:" + fourth.ToString() + "m";//前通道4的值
                                Juli1(first, second, third, fourth, 2, 1, 0.6, 0.35);//后通道4，最远值，次远值，次近值，最近值
                            }
                        }
                        else
                        {
                            MessageBox.Show("No data received or connection closed by the server.");
                        }
                    }
                }
            }
        }
        public List<QBZLDZT> QBZBytes1()
        {
            List<QBZLDZT> allStatuses = new List<QBZLDZT>();

            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    var status = QBZGroup1(group);
                    allStatuses.Add(status);
                }
            }

            return allStatuses;
        }
        private QBZLDZT QBZGroup1(byte[] group)
        {
            var qbzldzt = new QBZLDZT();
            qbzldzt.QTONGDAO1 = Qtd1(group);
            qbzldzt.QTONGDAO2 = Qtd2(group);
            qbzldzt.QTONGDAO3 = Qtd3(group);
            qbzldzt.QTONGDAO4 = Qtd4(group);
            return qbzldzt;
        }
        private double Qtd1(byte[] group) //通道1所测距离
        {
            byte highByte1 = group[3]; // 索引从0开始，所以第6个字节是索引5
            byte lowByte1 = group[4];  // 第7个字节是索引6
            ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
            int decimalValue1 = combined1;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
            double firstTD = 0.001 * decimalValue1;
            //TextBoxChannel1.Text = "通道1:" + TD1;           
            return firstTD;
        }
        private double Qtd2(byte[] group) //通道2所测距离
        {
            byte highByte2 = group[5];
            byte lowByte2 = group[6];
            ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
            int decimalValue2 = combined2;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
            double secondTD = 0.001 * decimalValue2;
            return secondTD;
        }
        private double Qtd3(byte[] group) //通道3所测距离
        {
            byte highByte3 = group[7];
            byte lowByte3 = group[8];
            ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
            int decimalValue3 = combined3;
            double thirdTD = 0.001 * decimalValue3;
            return thirdTD;
        }
        private double Qtd4(byte[] group) //通道4所测距离
        {
            byte highByte4 = group[9];
            byte lowByte4 = group[10];
            ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);
            int decimalValue4 = combined4;
            double fourthTD = 0.001 * decimalValue4;
            return fourthTD;
        }
        public byte[] ExtractGroup()
        {
            if (buffer.Count < groupSize) return null; // 如果不足一个分组，返回null

            var group = new byte[groupSize];
            for (int i = 0; i < groupSize; i++)
            {
                group[i] = buffer.Dequeue();
            }
            return group;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <param name="name3"></param>
        /// <param name="name4"></param>
        /// <param name="maxj"></param>
        /// <param name="nmaxj"></param>
        /// <param name="nmainj"></param>
        /// <param name="minj"></param>
        private void Juli1(double name1, double name2, double name3, double name4, double maxj, double nmaxj, double nmainj, double minj) //设定距离值
        {
            double MZ1 = name1; double MZ2 = name2; double MZ3 = name3; double MZ4 = name4;
            double MY = maxj;//最远
            double NY = nmaxj;//次远
            double NJ = nmainj;//次近
            double MJ = minj;//最近
            if (MZ1 >= MY || MZ2 >= MY || MZ3 >= MY || MZ4 >= MY)
            {

            }
            if ((MZ1 < MY && MZ1 >= NY) || (MZ2 < MY && MZ2 >= NY) || (MZ3 < MY && MZ3 >= NY) || (MZ4 < MY && MZ4 >= NY))
            {
                qbz = 3;
            }
            if ((MZ1 < NY && MZ1 >= NJ) || (MZ2 < NY && MZ2 >= NJ) || (MZ3 < NY && MZ3 >= NJ) || (MZ4 < NY && MZ4 >= NJ))
            {
                qbz = 2;
            }
            if ((MZ1 < NJ && MZ1 >= MJ) || (MZ2 < NJ && MZ2 >= MJ) || (MZ3 < NJ && MZ3 >= MJ) || (MZ4 < NJ && MZ4 >= MJ))
            {
                qbz = 3;
            }
            if ((MZ1 < MJ) || (MZ2 < MJ) || (MZ3 < MJ) || (MZ4 < MJ))
            {

            }
        }
        public int qbz = 0;
        public int hbz = 0;
        private void Juli2(double name1, double name2, double name3, double name4, double maxj, double nmaxj, double nmainj, double minj) //设定距离值
        {
            double MZ1 = name1; double MZ2 = name2; double MZ3 = name3; double MZ4 = name4;
            double MY = maxj;//最远
            double NY = nmaxj;//次远
            double NJ = nmainj;//次近
            double MJ = minj;//最近
            if (MZ1 >= MY || MZ2 >= MY || MZ3 >= MY || MZ4 >= MY)
            {

            }
            else
            if ((MZ1 < MY && MZ1 >= NY) || (MZ2 < MY && MZ2 >= NY) || (MZ3 < MY && MZ3 >= NY) || (MZ4 < MY && MZ4 >= NY))
            {
                hbz = 3;
            }
            else
            if ((MZ1 < NY && MZ1 >= NJ) || (MZ2 < NY && MZ2 >= NJ) || (MZ3 < NY && MZ3 >= NJ) || (MZ4 < NY && MZ4 >= NJ))
            {

                hbz = 2;
            }
            else
            if ((MZ1 < NJ && MZ1 >= MJ) || (MZ2 < NJ && MZ2 >= MJ) || (MZ3 < NJ && MZ3 >= MJ) || (MZ4 < NJ && MZ4 >= MJ))
            {

                hbz = 1;
            }
            else
            if ((MZ1 < MJ) || (MZ2 < MJ) || (MZ3 < MJ) || (MZ4 < MJ))
            {

            }
            string imgtext = qbz.ToString() + "-" + hbz.ToString();
            switch (imgtext)
            {
                case "1-1":
                    bz_img.Image = Properties.Resources.避障1_1;
                    break;
                case "1-2":
                    bz_img.Image = Properties.Resources.避障1_2;
                    break;
                case "1-3":
                    bz_img.Image = Properties.Resources.避障1_3;
                    break;
                case "2-1":
                    bz_img.Image = Properties.Resources.避障2_1;
                    break;
                case "2-2":
                    bz_img.Image = Properties.Resources.避障2_2;
                    break;
                case "2-3":
                    bz_img.Image = Properties.Resources.避障2_3;
                    break;
                case "3-1":
                    bz_img.Image = Properties.Resources.避障3_1;
                    break;
                case "3-2":
                    bz_img.Image = Properties.Resources.避障3_2;
                    break;
                case "3-3":
                    bz_img.Image = Properties.Resources.避障3_3;
                    break;
                default:
                    bz_img.Image = null;
                    break;
            }
        }
        private Queue<byte> buffer = new Queue<byte>(); // 用于存储接收到的字节      
        private int groupSize = 13; // 分组大小
        public void AddBytes(byte[] newBytes)
        {

            foreach (byte b in newBytes)
            {
                buffer.Enqueue(b);
            }
        }
        #endregion
        #region 后避障
        public async void HBZ()
        {
            CONNECTfunction("192.168.2.80", 10138);
            NetworkCommunication1 communicator = new NetworkCommunication1();
            using (NetworkStream stream = tcpClient.GetStream())
            {
                // 获取NetworkStream用于发送和接收
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    while (true)
                    {
                        // 接收数据
                        byte[] receivedData = await  homemethod.ReceiveBytesAsync(networkStream, 1024);
                        if (receivedData != null && receivedData.Length > 0)
                        {
                            // 处理接收到的数据
                            AddBytes(receivedData); // 添加到处理器                                
                                                    //YLBytes();
                            var hbzld = HBZBytes1();
                            foreach (var status in hbzld)
                            {
                                double first = status.HTONGDAO1;

                                //TextBoxChannel5.Text = "后通道1:" + first.ToString() + "m";//前通道1的值
                                double second = status.HTONGDAO2;

                              //  TextBoxChannel6.Text = "后通道2:" + second.ToString() + "m";//前通道2的值
                                double third = status.HTONGDAO3;

                              //  TextBoxChannel7.Text = "后通道3:" + third.ToString() + "m";//前通道3的值
                                double fourth = status.HTONGDAO4;

                              //  TextBoxChannel8.Text = "后通道4:" + fourth.ToString() + "m";//前通道4的值

                                Juli2(first, second, third, fourth, 2, 1, 0.6, 0.35);//后通道4，最远值，次远值，次近值，最近值
                            }
                        }
                        else
                        {
                            MessageBox.Show("No data received or connection closed by the server.");
                        }
                    }
                }
            }
        }
        public List<HBZLDZT> HBZBytes1()
        {
            List<HBZLDZT> allStatuses = new List<HBZLDZT>();

            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    var status = HBZGroup1(group);
                    allStatuses.Add(status);
                }
            }
            return allStatuses;
        }
        private HBZLDZT HBZGroup1(byte[] group)
        {
            var hbzldzt = new HBZLDZT();
            hbzldzt.HTONGDAO1 = Htd1(group);
            hbzldzt.HTONGDAO2 = Htd2(group);
            hbzldzt.HTONGDAO3 = Htd3(group);
            hbzldzt.HTONGDAO4 = Htd4(group);
            return hbzldzt;
        }
        private double Htd1(byte[] group) //后通道1所测距离
        {
            byte highByte1 = group[3]; // 索引从0开始，所以第6个字节是索引5
            byte lowByte1 = group[4];  // 第7个字节是索引6
            ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
            int decimalValue1 = combined1;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
            double firstTD = 0.001 * decimalValue1;
            //TextBoxChannel1.Text = "通道1:" + TD1;           
            return firstTD;
        }
        private double Htd2(byte[] group) //后通道2所测距离
        {
            byte highByte2 = group[5];
            byte lowByte2 = group[6];
            ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
            int decimalValue2 = combined2;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
            double secondTD = 0.001 * decimalValue2;
            //string TD2 = secondTD.ToString();//通道2所测距离
            //TextBoxChannel2.Text = "通道2:" + TD2;
            return secondTD;
        }
        private double Htd3(byte[] group) //后通道3所测距离
        {
            byte highByte3 = group[7];
            byte lowByte3 = group[8];
            ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
            int decimalValue3 = combined3;
            double thirdTD = 0.001 * decimalValue3;
            //string TD3 = thirdTD.ToString();//通道3所测距离
            //TextBoxChannel3.Text = "通道3:" + TD3;
            return thirdTD;
        }
        private double Htd4(byte[] group) //后通道4所测距离
        {
            byte highByte4 = group[9];
            byte lowByte4 = group[10];
            ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);
            int decimalValue4 = combined4;
            double fourthTD = 0.001 * decimalValue4;
            //string TD3 = thirdTD.ToString();//通道3所测距离
            //TextBoxChannel3.Text = "通道3:" + TD3;
            return fourthTD;
        }
        #endregion
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
                    NET_DVR_PREVIEWINFO lpPreviewInfo = new NET_DVR_PREVIEWINFO();
                    lpPreviewInfo.hPlayWnd = zsxt_img.Handle;//预览窗口
                    lpPreviewInfo.lChannel = Int16.Parse("1");//预te览的设备通道
                    lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                    lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                    lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                    lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                    lpPreviewInfo.byProtoType = 0;
                    lpPreviewInfo.byPreviewMode = 0;

                    NET_DVR_PREVIEWINFO lpPreviewInfo1 = new NET_DVR_PREVIEWINFO();
                    lpPreviewInfo1.hPlayWnd = pictureBox3.Handle;//预览窗口
                    lpPreviewInfo1.lChannel = Int16.Parse("2");//预te览的设备通道
                    lpPreviewInfo1.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                    lpPreviewInfo1.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                    lpPreviewInfo1.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                    lpPreviewInfo1.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                    lpPreviewInfo1.byProtoType = 0;
                    lpPreviewInfo1.byPreviewMode = 0;
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
        /// 预览实时流回调函数
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="pUser"></param>
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
        //获取网速
        private void DownloadSpee(double MB)
        {
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

            }
            else
            {

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
                MessageBox.Show(str);
            }
            else
            {


            }
            Marshal.FreeHGlobal(ptrspCfg);
            #endregion
        }
        #region IO
        public void IO()
        {
            CONNECTfunction("192.168.2.253", 1030);
        }
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        //连接函数
        private void CONNECTfunction(string ip, int Desport)
        {
            string ipAddress = ip;
            int port = Desport;
            try
            {
                // 关闭 NetworkStream
                if (networkStream != null)
                {
                    networkStream.Close();
                    networkStream.Dispose();
                }
                // 断开 TcpClient 连接并释放资源
                if (tcpClient != null && tcpClient.Connected)
                {
                    tcpClient.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理关闭连接时可能出现的异常
                MessageBox.Show("关闭连接时发生错误: " + ex.Message);
            }
            finally
            {
                // 确保 TcpClient 和 NetworkStream 被设置为 null 或重置状态
                tcpClient = null;
                networkStream = null;
            }
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                {
                    // 连接到硬件设备
                    tcpClient = new TcpClient();
                    tcpClient.Connect(ipAddress, port);
                    networkStream = tcpClient.GetStream();
                   
                }
                else
                {
                    // 如果连接已存在，可以跳过或提醒
                    MessageBox.Show("已存在活动的连接。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败!" + ex.Message);
            }
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            if (ds_btn.Tag.ToString() == "开")
            {
                ds_btn.ImageIndex = 1;
                ds_btn.Tag = "关";
            }
            else
            {
                ds_btn.ImageIndex = 2;
                ds_btn.Tag = "开";
            }
        }
        private void qtcl_btn_Click(object sender, EventArgs e)
        {
            if (qtcl_btn.Tag.ToString() == "开")
            {
                qtcl_btn.ImageIndex = 1;
                qtcl_btn.Tag = "关";
            }
            else
            {
                qtcl_btn.ImageIndex = 0;
                qtcl_btn.Tag = "开";
            }
        }

        private async void sqx_btn_Click(object sender, EventArgs e)
        {
            if (sqx_btn.Tag.ToString() == "开")
            {
                sqx_btn.ImageIndex = 1;
                sqx_btn.Tag = "关";
            }
            else
            {
                sqx_btn.ImageIndex = 0;
                sqx_btn.Tag = "开";
            }
             QXFJ("192.168.2.253", 1030, sqx_btn.Tag.ToString());
        }
        private async void QXFJ(string ioip, int ioport,string type)
        {
            string ipaddress = ioip;
            int port = ioport;
            
            NetworkCommunication1 communicator = new NetworkCommunication1();
            string[] hexValues0 = { "48", "3a", "01", "70", "04", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "04", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                if (type=="关")
                {
                    await homemethod.SendBytesAsync(networkStream, bytesDK);
                }
                else
                {
                    await homemethod.SendBytesAsync(networkStream, bytesBH);
                }
            }
        }
        private void gs_btn_Click(object sender, EventArgs e)
        {
            if (gs_btn.Tag.ToString() == "开")
            {
                gs_btn.ImageIndex = 1;
                gs_btn.Tag = "关";
            }
            else
            {
                gs_btn.ImageIndex = 0;
                gs_btn.Tag = "开";
            }
        }

        private void xcq_btn_Click(object sender, EventArgs e)
        {
            if (xcq_btn.Tag.ToString() == "开")
            {
                xcq_btn.ImageIndex = 1;
                xcq_btn.Tag = "关";
            }
            else
            {
                xcq_btn.ImageIndex = 0;
                xcq_btn.Tag = "开";
            }
            XCFJ("192.168.2.253", 1030, xcq_btn.Tag.ToString());

        }
        private async void XCFJ(string ioip, int ioport,string type)
        {
            string ipaddress = ioip;
            int port = ioport;
          
            NetworkCommunication1 communicator = new NetworkCommunication1();
            string[] hexValues0 = { "48", "3a", "01", "70", "03", "00", "00", "01", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "03", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                if (type =="关")
                {
                    //延时一秒
                    await Task.Delay(1000);
                    await homemethod.SendBytesAsync(networkStream, bytesDK);
                }
                else
                {
                    await homemethod.SendBytesAsync(networkStream, bytesBH);
                }
            }
        }
        /// <summary>
        /// 大灯远光
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qzm_btn_Click(object sender, EventArgs e)
        {
            if (qzm_btn.Tag.ToString() == "开")
            {
                qzm_btn.ImageIndex = 1;
                qzm_btn.Tag = "关";
            }
            else
            {
                qzm_btn.ImageIndex = 0;
                qzm_btn.Tag = "开";
            }
            IO_O(0, 2);
        }

        private void hzm_btn_Click(object sender, EventArgs e)
        {
            if (hzm_btn.Tag.ToString() == "开")
            {
                hzm_btn.ImageIndex = 1;
                hzm_btn.Tag = "关";
            }
            else
            {
                hzm_btn.ImageIndex = 0;
                hzm_btn.Tag = "开";
            }
            IO_O(0, 3);
        }

        private void gzd_btn_Click(object sender, EventArgs e)
        {
            if (gzd_btn.Tag.ToString() == "开")
            {
                gzd_btn.ImageIndex = 1;
                gzd_btn.Tag = "关";
            }
            else
            {
                gzd_btn.ImageIndex = 0;
                gzd_btn.Tag = "开";
            }
            IO_O(0, 4);
        }

        private void jxb_btn_Click(object sender, EventArgs e)
        {
            if (jxb_btn.Tag.ToString() == "开")
            {
                jxb_btn.ImageIndex = 1;
                jxb_btn.Tag = "关";
            }
            else
            {
                jxb_btn.ImageIndex = 0;
                jxb_btn.Tag = "开";
            }
        }

        private void ygd_btn_Click(object sender, EventArgs e)
        {
            if (ygd_btn.Tag.ToString() == "开")
            {
                ygd_btn.ImageIndex = 1;
                ygd_btn.Tag = "关";
            }
            else
            {
                ygd_btn.ImageIndex = 0;
                ygd_btn.Tag = "开";
            }
            IO_O(0, 5);
        }

        private void ychh_btn_Click(object sender, EventArgs e)
        {
            if (ychh_btn.Tag.ToString() == "开")
            {
                ychh_btn.ImageIndex = 1;
                ychh_btn.Tag = "关";
            }
            else
            {
                ychh_btn.ImageIndex = 0;
                ychh_btn.Tag = "开";
            }
        }

        private void dcjw_btn_Click(object sender, EventArgs e)
        {
            if (dcjw_btn.Tag.ToString() == "开")
            {
                dcjw_btn.ImageIndex = 1;
                dcjw_btn.Tag = "关";
            }
            else
            {
                dcjw_btn.ImageIndex = 0;
                dcjw_btn.Tag = "开";
            }
            JWFJ1("192.168.2.253", 1030, dcjw_btn.Tag.ToString());
            JWFJ2("192.168.2.253", 1030, dcjw_btn.Tag.ToString());
        }
        /// <summary>
        /// 降温风机1
        /// </summary>
        /// <param name="ioip"></param>
        /// <param name="ioport"></param>
        /// <param name="type"></param>
        private async void JWFJ1(string ioip, int ioport,string type)
        {
            string ipaddress = ioip;
            int port = ioport;
           
            NetworkCommunication1 communicator = new NetworkCommunication1();
            string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                if (type=="关")
                {
                    //延时一秒
                    await Task.Delay(1000);
                    await homemethod.SendBytesAsync(networkStream, bytesDK);
                }
                else
                {
                    await homemethod.SendBytesAsync(networkStream, bytesBH);
                }
            }
        }
        /// <summary>
        /// 降温风机2
        /// </summary>
        /// <param name="ioip"></param>
        /// <param name="ioport"></param>
        /// <param name="type"></param>
        private async void JWFJ2(string ioip, int ioport,string type)
        {
            string ipaddress = ioip;
            int port = ioport;
           
            NetworkCommunication1 communicator = new NetworkCommunication1();
            string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                if (type =="关")
                {
                    //延时一秒
                    await Task.Delay(1000);
                    await homemethod.SendBytesAsync(networkStream, bytesDK);
                }
                else
                {
                    await homemethod.SendBytesAsync(networkStream, bytesBH);
                }
            }
        }

        private void zdph_btn_Click(object sender, EventArgs e)
        {
            if (zdph_btn.Tag.ToString() == "开")
            {
                zdph_btn.ImageIndex = 1;
                zdph_btn.Tag = "关";
            }
            else
            {
                zdph_btn.ImageIndex = 0;
                zdph_btn.Tag = "开";
            }
            CCZD("192.168.2.253", 1030, zdph_btn.Tag.ToString());
        }
        private async void CCZD(string ioip, int ioport,string type)
        {
            string ipaddress = ioip;
            int port = ioport;
            
            NetworkCommunication1 communicator = new NetworkCommunication1();
            string[] hexValues0 = { "48", "3a", "01", "70", "05", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "05", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                if (type=="关")
                {
                    await homemethod.SendBytesAsync(networkStream, bytesDK);
                }
                else
                {
                    await homemethod.SendBytesAsync(networkStream, bytesBH);
                }
            }
        }
        private void fcnzm_btn_Click(object sender, EventArgs e)
        {
            if (fcnzm_btn.Tag.ToString() == "开")
            {
                fcnzm_btn.ImageIndex = 1;
                fcnzm_btn.Tag = "关";
            }
            else
            {
                fcnzm_btn.ImageIndex = 0;
                fcnzm_btn.Tag = "开";
            }
        }

        private void fcdm_btn_Click(object sender, EventArgs e)
        {
            if (fcdm_btn.Tag.ToString() == "开")
            {
                fcdm_btn.ImageIndex = 1;
                fcdm_btn.Tag = "关";
            }
            else
            {
                fcdm_btn.ImageIndex = 0;
                fcdm_btn.Tag = "开";
            }
        }

        private void yjcbsb_btn_Click(object sender, EventArgs e)
        {
            if (yjcbsb_btn.Tag.ToString() == "开")
            {
                yjcbsb_btn.ImageIndex = 1;
                yjcbsb_btn.Tag = "关";
            }
            else
            {
                yjcbsb_btn.ImageIndex = 0;
                yjcbsb_btn.Tag = "开";
            }
        }

        private void ztwd_btn_Click(object sender, EventArgs e)
        {
            if (ztwd_btn.Tag.ToString() == "开")
            {
                ztwd_btn.ImageIndex = 1;
                ztwd_btn.Tag = "关";
            }
            else
            {
                ztwd_btn.ImageIndex = 0;
                ztwd_btn.Tag = "开";
            }
        }

        private void zsyg_btn_Click(object sender, EventArgs e)
        {
            if (zsyg_btn.Tag.ToString() == "开")
            {
                zsyg_btn.ImageIndex = 1;
                zsyg_btn.Tag = "关";
                zsyg_timer.Stop();
            }
            else
            {
                zsyg_btn.ImageIndex = 0;
                zsyg_btn.Tag = "开";
                NET_DVR_PTZControl(m_lRealHandle, (uint)WIPER_PWRON, 0);
                zsyg_timer.Start();
            }
        }
        private Point mouseDownPoint = Point.Empty;//记录初始位置
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            // 记录鼠标按下的位置
            mouseDownPoint = e.Location;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            //判断鼠标是否按下并且正在向下滑动
            if (e.Button == MouseButtons.Right)
            {
                // 比较当前位置和按下时的位置，判断是否向下滑动
                int delta = e.Y - mouseDownPoint.Y;
                if (delta > 0)
                {
                    // 向下滑动的处理逻辑

                    //   NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.CHCNetSDK.TILT_DOWN, 1, 4);
                    NET_DVR_PTZControlWithSpeed_Other(m_lUserID, 1, TILT_DOWN, 0, 4);
                }
                else if (delta < 0)
                {
                    // 向上滑动的处理逻辑

                    NET_DVR_PTZControlWithSpeed_Other(m_lUserID, 1, TILT_UP, 0, 4);
                }
            }
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int delta = e.Y - mouseDownPoint.Y;
                if (delta > 0)
                {
                    //鼠标下滑
                    // NET_DVR_PTZControlWithSpeed(m_lRealHandle, TILT_DOWN, 1, 4);
                    NET_DVR_PTZControlWithSpeed_Other(m_lUserID, 1, TILT_DOWN, 1, 4);
                }
                else if (delta < 0)
                {
                    // NET_DVR_PTZControlWithSpeed(m_lRealHandle, TILT_UP, 1, 4);
                    NET_DVR_PTZControlWithSpeed_Other(m_lUserID, 1, TILT_UP, 1, 4);
                }
            }
        }

        private void sbkg_btn_Click(object sender, EventArgs e)
        {
            if (sbkg_btn.Tag.ToString() == "开")
            {
                sbkg_btn.ImageIndex = 1;
                sbkg_btn.Tag = "关";
            }
            else
            {
                sbkg_btn.ImageIndex = 0;
                sbkg_btn.Tag = "开";
            }
            SBKZ("192.168.1.253", 1030,sbkg_btn.Tag.ToString());
        }
        private async void SBKZ(string ioip, int ioport,string type)
        {
            string ipaddress = ioip;
            int port = ioport; 
            NetworkCommunication1 communicator = new NetworkCommunication1();
            string[] hexValues0 = { "48", "3a", "01", "70", "06", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "06", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                if (type=="关")
                {
                    await homemethod.SendBytesAsync(networkStream, bytesDK);
                }
                else
                {
                    await homemethod.SendBytesAsync(networkStream, bytesBH);
                }
            }
        }
        private void fyfj_btn_Click(object sender, EventArgs e)
        {
            if (fyfj_btn.Tag.ToString() == "开")
            {
                fyfj_btn.ImageIndex = 1;
                fyfj_btn.Tag = "关";
            }
            else
            {
                fyfj_btn.ImageIndex = 0;
                fyfj_btn.Tag = "开";
            }
            FYFJ("192.168.1.253", 1030,fyfj_btn.Tag.ToString());
        }
        private async void FYFJ(string ioip, int ioport,string type)
        {
            string ipaddress = ioip;
            int port = ioport;
            
            NetworkCommunication1 communicator = new NetworkCommunication1();
            string[] hexValues0 = { "48", "3a", "01", "70", "07", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "07", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                if (type=="关")
                {
                    await homemethod.SendBytesAsync(networkStream, bytesDK);
                }
                else
                {
                    await homemethod.SendBytesAsync(networkStream, bytesBH);
                }
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        #region IO2
        //通用输出控制函数（输出或关闭）
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AXis_NO">轴号</param>
        /// <param name="nBitIndex">0-15段</param>
        private void IO_O(short AXis_NO, short nBitIndex)
        {
            int iRes = 0;
            short nValue = 0;
            iRes = MultiCardCS_1.GA_GetExtDoBit(0, nBitIndex, ref nValue);

            if (0 == nValue)
            {
                iRes = MultiCardCS_1.GA_SetExtDoBit(0, nBitIndex, 1);
            }
            else
            {
                iRes = MultiCardCS_1.GA_SetExtDoBit(0, nBitIndex, 0);
            }
        }
        public void DKBK()
        {
            int iRes = 0;
            //注意板卡端端口号必须和PC端端口号保持一致
            iRes = MultiCardCS_1.GA_Open(1, IP, 60000, "192.168.2.2", 60000);

            if (iRes == 0)
            {
                MessageBox.Show("打开板卡成功");
            }
            else
            {
                MessageBox.Show("打开板卡失败" + iRes);
                MultiCardCS_1.GA_Close();
            }
            EventArgs e = new EventArgs();
            object Q = new object();

        }
        #endregion

        private void zsyg_timer_Tick(object sender, EventArgs e)
        {
            NET_DVR_PTZControl(m_lRealHandle, (uint)WIPER_PWRON, 0);
        }

        private void wltimer_Tick(object sender, EventArgs e)
        {
            dynamic query = this.Parent.Parent;
            double query1 = query.WLSpeed;
            wlSpeed = query1;
          
            DownloadSpee(wlSpeed);
        }
        #region 车辆姿态
        /// <summary>
        /// 打开串口
        /// </summary>
        public void OpenPort()
        {
            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();

                    serialPort.DataReceived += DataReceivedHandler;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"车辆姿态串口打开失败: {ex.Message}");
                    uiRoundMeter1.Angle = 0;
                    return;
                }
            }
            else
            {
                MessageBox.Show("车辆姿态串口已经打开，请勿重复操作");
            }
        }
        private SerialPort _serialPort;
        private SerialPort serialPort;
        private string _portName;
        private void InitializeSerialPort()
        {
            serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None,
                ReadTimeout = 500,
                WriteTimeout = 500
            };
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int numberOfBytes = sp.BytesToRead;
            byte[] buffer = new byte[numberOfBytes];
            sp.Read(buffer, 0, numberOfBytes);
            ProcessReceivedBytes(buffer);
        }
        /// <summary>
        /// 处理字节
        /// </summary>
        /// <param name="buffer"></param>
        private void ProcessReceivedBytes(byte[] buffer)
        {
            byte valueToFind = 0x55;
            int groupSize = 11; // 一组的大小
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == valueToFind)
                {
                    // 确保有足够的字节来形成一个组
                    if (i + groupSize <= buffer.Length)
                    {
                        // 提取一组字节
                        byte[] group = new byte[groupSize];
                        Array.Copy(buffer, i, group, 0, groupSize);
                        // 处理这一组字节
                        AddBytes(group);
                        ZTBytes();
                        // 移动索引以跳过已处理的字节
                        i += groupSize - 1;
                    }
                }
            }
        }
        /// <summary>
        /// 姿态字节处理
        /// </summary>
        public void ZTBytes()
        {
            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    ZTGroup(group);
                }
            }
        }
        private byte XYT = 85;//协议头
        private byte TIME = 80;//时间
        private byte JIASD = 81;//加速度
        private byte JIAOSD = 82;//角速度
        private byte JD = 83;//角度
        private byte CC = 84;//磁场
        private byte DKZT = 85;//端口状态
        private byte QYGD = 86;//气压高度
        private byte JWD = 87;//经纬度
        private byte DS = 88;//GPS数据输出
        private byte SYS = 89;//四元数
        private byte GPS = 90;//GPS定位精度
        private byte DQ = 95;//读取
        /// <summary>
        /// 分组处理
        /// </summary>
        /// <param name="group"></param>
        private void ZTGroup(byte[] group)
        {
            if (group[0] == XYT)
            {


                if (group[1] == JD) //角度
                {
                    byte RollL = group[2];//滚转角X低8位
                    byte RollH = group[3];//滚转角X高8位
                    byte PitchL = group[4];//俯仰角y低8位
                    byte PitchH = group[5];//俯仰角y高8位
                    byte YawL = group[6];//偏航角z低8位
                    byte YawH = group[7];//偏航角z高8位
                    ushort combined1 = (ushort)((RollH << 8) | RollL);// 角度X
                    ushort combined2 = (ushort)((PitchH << 8) | PitchL);// 角度Y
                    ushort combined3 = (ushort)((YawH << 8) | YawL);// 角度Z
                    if ((group[3] & 0x80) != 0)
                    {
                        double JDX = ~combined1 + 1;
                        double JDX1 = (-JDX) / 32768 * 180;
                        //cxlj_btn.Invoke(new Action(() =>
                        //{
                        //    textBoxJDX.Text = "角度X:" + JDX1.ToString() + "°";// 滚转角度X
                        //}));
                        uiRoundMeter1.Angle = JDX1;
                    }
                    else
                    {
                        double JDX = combined1;
                        double JDX1 = JDX / 32768 * 180;
                        //textBoxJDX.Invoke(new Action(() =>
                        //{
                        //    textBoxJDX.Text = "角度X:" + JDX1.ToString() + "°";// 滚转角度X
                        //}));
                        uiRoundMeter1.Angle = JDX1;
                    }

                }

            }
        }
        #endregion
        #region 三维建模
        private void openGLControl1_GDIDraw(object sender, SharpGL.RenderEventArgs args)
        {
            if (rosdata != null)
            {
                /// <summary>
                /// 默认绘画模式为线条
                /// </summary>
                uint _model = OpenGL.GL_POINTS;
                // 创建一个GL对象
                SharpGL.OpenGL gl = this.openGLControl1.OpenGL;

                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);  // 清空屏幕
                gl.LoadIdentity();                  // 重置
                gl.Translate(0.0f, 0.0f, -6.0f);    // 设置坐标，距离屏幕距离为6

                gl.Rotate(_x, 50.0f, 0.0f, 0.0f);   // 绕X轴旋转
                gl.Rotate(_y, 0.0f, 50.0f, 0.0f);   // 绕Y轴旋转
                gl.Rotate(_z, 0.0f, 0.0f, 50.0f);    // 绕Z轴旋转

                gl.Begin(_model);                   // 绘制立方体
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
                gl.End();
            }// 结束绘制
        }
        public string[] rosdata;
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
            Process process1 = Process.Start(startInfo);
            //this.Visible = true;
            //using (Process process = Process.Start(startInfo))
            //{
            //    // 可以读取控制台程序的输出

            var output = process1.StandardOutput.ReadLine();
            process1.Kill();
            var ros = output.Split(':');
            var str1 = ros[7].Replace("[", "").Replace("]", "");
            rosdata = str1.Split(", ");
            var q = rosdata.Count();
        }

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            // 记录鼠标按下的位置
            mouseDownPoint = e.Location;
        }
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
        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //判断鼠标是否按下并且正在向下滑动
            if (e.Button == MouseButtons.Right)
            {
                // 比较当前位置和按下时的位置，判断是否向下滑动
                int up = e.Y - mouseDownPoint.Y;
                if (up > 0)
                {
                    // 向下滑动的处理逻辑

                    _z = up;
                }
                else if (up < 0)
                {
                    // 向上滑动的处理逻辑

                    _z = up;
                }
                int left = e.X - mouseDownPoint.X;
                if (left > 0)//向右
                {
                    // NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, PAN_RIGHT, 0, (uint)4);
                    _x = left;
                }
                else//向左
                {
                    // NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, PAN_LEFT, 0, (uint)4);
                    _x = left;
                }
            }
        }
        #endregion
    }
}
