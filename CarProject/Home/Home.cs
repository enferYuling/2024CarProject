using CarProject.Method;
using HZH_Controls;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Sockets;
using Sunny.UI;
using MultiCardCS;
using Newtonsoft.Json.Linq;



namespace CarProject.Home
{
    public partial class Home: Form
    {
        public string account="system";
        public string realName="系统管理员";
        public string UserId = "214398e452c8432e899456d64bb9ee8a";
        public Form childForm;
        public HomeMethod homemethod;
        public readonly SqlSugarClient db;
        private Stopwatch stopwatch;
       
        private TimeSpan elapsedTime;
          private PerformanceCounter bytesSentCounter;
    private PerformanceCounter bytesReceivedCounter;
        public string IPAddress;
        public double WLSpeed;
        private Queue<byte> buffer = new Queue<byte>(); // 用于存储接收到的字节      
        private int groupSize = 13; // 分组大小
                                    //定义PS和SA的值
        private byte BMS = 244;
        private byte DPLY = 40;
        private byte MCU = 239;
        private byte MMC = 56;
        private byte CCS = 229;
        private byte BCA = 80;

        // 定义PF的范围
        private int VStart = 200; // 电压详细信息的报文的开始
        private int VEnd = 249;   // 电压详细信息的报文的结束

        private int TemperatureStart = 180;//温度详细信息报文的开始（各探头的采样值）
        private int TemperatureEnd = 199;//温度详细信息报文的结束（各探头的采样值）

        private byte MAX = 255;//PF的最大值
        private byte NextM = 254;
        private byte CJ2 = 250;
        private byte CJ1 = 245;

        //故障码
        private int F1 = 1;
        private int F2 = 20;
        private int F3 = 21;
        private int F4 = 60;
        private int F5 = 61;
        private int F6 = 99;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        //声明卡对象，如果有多个卡，可以声明多个对象
        MultiCardCS.MultiCardCS MultiCardCS_1 = new MultiCardCS.MultiCardCS();
        
        public Home(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb; 
        }

        private void Home_Load(object sender, EventArgs e)
        {
           // Serve();
           // InitializeCounters();
            homemethod = new HomeMethod(db);
           IPAddress = homemethod.GetLocalIPAddress();//获取IP地址


            if (string.IsNullOrEmpty(IPAddress))
            {
                MessageBox.Show("获取IP地址失败");
                this.Close();
                return;
            }

            realname_lab.Text = "欢迎你，" + realName;
            // 将图片旋转90度
            sxsl_pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            sxsl_pictureBox.Refresh();
            //   this.ucPanelTitle1.Controls.Clear();
            childForm = new FCKZYMFrom(db);
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();

            initialize();
            timer1.Start();

            stopwatch = new Stopwatch();
            operate_lab.Text = $"操作时长: {elapsedTime:hh\\时mm\\分}";
            // UpdateLabel();
            stopwatch.Start();
            caozuo_timer2.Start();
            //  dc1();
            //  IO();
            //IP = homemethod.GetLocalIPAddress();
            //  DKBK();
        }
        #region 电池1
        public async void dc1()
        {
            CONNECTfunction("192.168.2.101", 8886);
            NetworkCommunication1 communicator = new NetworkCommunication1();
            try
            {
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
                                ProcessBufferedBytes();
                            }
                            else
                            {
                                MessageBox.Show("No data received or connection closed by the server.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                dlcd_lab.Text = "电池异常";
                dl_lab.Visible=false;
                DL_pictureBox.Visible=false;
            }
        }
        public void ProcessBufferedBytes()
        {
            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    ClassifyGroup(group);
                }
            }
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
 
        private void ClassifyGroup(byte[] group)
        {

            //按照PS,SA和PF开始分类
            if (group[4] == BMS && group[3] == DPLY)
            {
                // 电压详细信息的报文
                if (group[2] >= VStart && group[2] <= VEnd)
                {
                    byte highByte1 = group[5]; // 索引从0开始，所以第6个字节是索引5
                    byte lowByte1 = group[6];  // 第7个字节是索引6
                    byte highByte2 = group[7];
                    byte lowByte2 = group[8];
                    byte highByte3 = group[9];
                    byte lowByte3 = group[10];
                    byte highByte4 = group[11];
                    byte lowByte4 = group[12];

                    // 将两个字节合并为UInt16
                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
                    ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
                    ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);
                    // 转换为10进制并输出或处理
                    int decimalValue1 = combined1;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
                    int decimalValue2 = combined2;
                    int decimalValue3 = combined3;
                    int decimalValue4 = combined4;
                }
                //温度详细报文（探头采样值）
                if (group[2] >= TemperatureStart && group[2] <= TemperatureEnd)
                {
                    // 直接将byte转换为int，已经是十进制数
                    int decimalValue1 = group[5]; //string decimalValueString = receivedByte.ToString(); // 将十进制数转换为字符串表示
                    int decimalValue2 = group[6];
                    int decimalValue3 = group[7];
                    int decimalValue4 = group[8];
                    int decimalValue5 = group[9];
                    int decimalValue6 = group[10];
                    int decimalValue7 = group[11];
                    int decimalValue8 = group[12];
                }
                //BMS基本信息报文1
                if (group[2] == MAX)
                {
                    byte highByte1 = group[8];//电池组充放电总电流高字节
                    byte lowByte1 = group[7];//电池组充放电总电流低字节
                    byte highByte2 = group[10];//电池组总电压高字节
                    byte lowByte2 = group[9];//电池组总电压低字节                   
                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电池组充放电总电流
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//电池组总电压

                    if ((group[5] & 0x01) != 0) //充电线是否连接，0未连接，1连接
                    {

                    }
                    else { }
                    if ((group[5] & 0x02) != 0) //电池组充电状态，0未充电，1充电
                    {
                        dlcd_lab.Text = "正在充电";
                        dl_lab.Visible = false;
                        dlcd_lab.Visible = true;
                    }
                    else
                    {
                        dlcd_lab.Text = "";
                        dl_lab.Visible = true;
                        dlcd_lab.Visible = false;
                    }
                    if ((group[5] & 0x04) != 0) //电池组亏电状态，0未亏电，1亏电
                    {

                    }
                    else { }
                    if ((group[5] & 0x08) != 0) //电池组就绪状态，0未充电，1就绪
                    {

                    }
                    else { }
                    if ((group[5] & 0x10) != 0) //放电接触器状态，0断开，1闭合
                    {
                       // TextBoxFD.Text = "放电接触器闭合";
                    }
                    else
                    {
                       // TextBoxFD.Text = "放电接触器断开";
                    }
                    if ((group[5] & 0x20) != 0) //充电接触器状态，0断开，1闭合
                    {

                    }
                    else { }
                    int SOC = group[6];//SOC的值
                                       // CompareSOC(SOC, 20, 80);
                                       //  string SOC1 = SOC.ToString();
                    if (!string.IsNullOrEmpty(dlcd_lab.Text))
                    {
                        if (SOC == 100)
                        {
                            dlcd_lab.Text = "已充满";
                        }
                    }
                    else
                    {
                        dl_lab.Text = SOC.ToString() + "%";//SOC               
                    }
                   

                    //int decimalValue1 = combined1;//电池组充放电总电流
                    //double TC = 0.1 * decimalValue1;
                    //string TotalCurrent = TC.ToString();
                    //TextBoxTcurrent.Text = "充放电总电流" + TotalCurrent + "A";//显示电池充放电总电流

                    //int decimalValue2 = combined2;//电池组总电压
                    //double TV = 0.1 * decimalValue2;
                    //string TotalVoltage = TV.ToString();
                    //TextBoxTvoltage.Text = "总电压" + TotalVoltage + "V";//显示电池组总电压

                    //int decimalValue3 = group[11];//故障等级
                    //int decimalValue4 = group[12];//故障码
                    //if (decimalValue3 == 1) //一级故障，严重故障， 立即停车
                    //{
                    //    if (decimalValue4 >= F1 && decimalValue4 <= F2)
                    //    {
                    //        string Fault = decimalValue4.ToString();
                    //        TextBoxFault.Text = "一级故障" + Fault;
                    //    }
                    //}
                    //if (decimalValue3 == 2)//二级故障，普通故障，限速50%运行
                    //{
                    //    if (decimalValue4 >= F3 && decimalValue4 <= F4)
                    //    {
                    //        string Fault = decimalValue4.ToString();
                    //        TextBoxFault.Text = "二级故障" + Fault;
                    //    }
                    //}
                    //if (decimalValue3 == 3)//三级故障，报警故障，报警
                    //{
                    //    if (decimalValue4 >= F5 && decimalValue4 <= F6)
                    //    {
                    //        string Fault = decimalValue4.ToString();
                    //        TextBoxFault.Text = "三级故障" + Fault;
                    //    }
                    //}
                }
                //BMS基本信息报文2
                if (group[2] == NextM)
                {
                    byte highByte1 = group[6];//最高单体电压高字节
                    byte lowByte1 = group[5];//最高单体电压低字节
                    byte highByte2 = group[8];//最低单体电压高字节
                    byte lowByte2 = group[7];//最低单体电压低字节
                    byte highByte3 = group[12];//最大允许放电电流高字节
                    byte lowByte3 = group[11];//最大允许放电电流低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//最高单体电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//最低单体电压
                    ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);//最大允许放电电流

                  
                     

                    int decimalValue4 = group[9];//单体最高温度
                    double SMAXT = 1 * decimalValue4;
                    string SMAXT1 = SMAXT.ToString();
                    battery_lab.Text = "电池1温度：" + SMAXT1 + "℃";//单体最高温度

                    //int decimalValue5 = group[10];//单体最低温度
                    //double SMINT = 0.001 * decimalValue5;
                    //string SMINT1 = SMINT.ToString();
                    //TextBoxSMINT.Text = "单体最低温度" + SMINT1 + "℃";//单体最低温度
                }
            }
            //充电需求报文
            if (group[4] == BMS && group[3] == CCS)
            {
                if (group[2] == MAX)
                {
                    byte highByte1 = group[5];//最高允许充电端电压高字节
                    byte lowByte1 = group[6];//最高允许充电端电压低字节
                    byte highByte2 = group[7];//最高允许充电电流高字节
                    byte lowByte2 = group[8];//最高允许充电电流低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//最高允许充电端电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//最高允许充电电流

                    int decimalValue1 = combined1;//最高允许充电端电压
                    double YXmaxv = 0.1 * decimalValue1;
                    int decimalValue2 = combined2;//最高允许充电电流
                    double YXmaxA = 0.1 * decimalValue2;
                    if ((group[9] & 0x01) != 0) //充电机是否开始充电。0开是充电，1电池保护，关闭充电
                    {

                    }
                    else { }
                }
            }
            //充电机反馈信息报文
            if (group[4] == CCS && group[3] == BCA)
            {
                if (group[2] == MAX)
                {
                    byte highByte1 = group[5];//输出电压高字节
                    byte lowByte1 = group[6];//输出电压低字节
                    byte highByte2 = group[7];//输出电流高字节
                    byte lowByte2 = group[8];//输出电流低字节
                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//输出电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//输出电流
                    int decimalValue1 = combined1;//输出电压
                    double OUTPUTV = 0.1 * decimalValue1;
                    int decimalValue2 = combined2;//输出电流
                    double OUTPUTA = 0.1 * decimalValue2;
                    if ((group[9] & 0x01) != 0) //硬件故障。0正常，1硬件故障
                    {

                    }
                    else { }
                    if ((group[9] & 0x02) != 0) //充电机温度故障，0正常，1充电机温度过高保护
                    {

                    }
                    else { }
                    if ((group[9] & 0x04) != 0) //低压限功率模式，0输入电压正常，1输入电压偏低，进入低功率模式
                    {

                    }
                    else { }
                    if ((group[9] & 0x08) != 0) //输入电压状态，0输入电压正常，1输入过压或欠压故障
                    {

                    }
                    else { }
                    if ((group[9] & 0x10) != 0) //输出过流，0输出电流正常，1输出过流
                    {

                    }
                    else { }
                    if ((group[9] & 0x20) != 0) //启动状态，0充电机关闭状态，1处于充电状态
                    {

                    }
                    else { }
                    if ((group[9] & 0x40) != 0) //通信状态，0通信正常，1通信接收超时
                    {

                    }
                    else { }
                    if ((group[9] & 0x80) != 0) //电池连接状态，0电池连接正常，1电池反接或未接
                    {

                    }
                    else { }
                }
            }
            //电机控制器与仪表通讯报文
            if (group[4] == MCU && group[3] == MMC)
            {
                //电机控制器与仪表通讯报文1
                if (group[2] == CJ1)
                {
                    if ((group[9] & 0x01) != 0) //挡位状态。00无效，01前进，10后退
                    {
                        if ((group[9] & 0x02) == 0) //
                        {

                        }
                    }
                    else
                    {
                        if ((group[9] & 0x02) != 0) //
                        {

                        }
                    }
                    if ((group[9] & 0x04) != 0) //刹车状态，0无刹车，1有刹车
                    {

                    }
                    else { }
                    if ((group[9] & 0x08) != 0) //运行模式，00默认模式，01经济模式，10高速模式
                    {
                        if ((group[9] & 0x10) == 0)
                        {

                        }
                    }
                    else
                    {
                        if ((group[9] & 0x10) != 0)
                        {

                        }
                        else { }
                    }
                    if ((group[9] & 0x20) != 0) //控制器状态，0 not ready，1 ready
                    {

                    }
                    else { }
                    if ((group[9] & 0x40) != 0) //限功耗状态，0正常运行，1降功率运行
                    {

                    }
                    else { }

                    int decimalValue1 = group[6];//控制器故障代码
                    byte highByte1 = group[8];//电机转速高字节
                    byte lowByte1 = group[7];//电机转速低字节
                    byte highByte2 = group[10];//小计里程高字节
                    byte lowByte2 = group[9];//小计里程低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电机转速
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//小计里程

                    int decimalValue2 = combined1;
                    int decimalValue3 = combined2;
                    double XJLC = 0.1 * decimalValue3;
                    int decimalValue4 = group[11];//车速
                }
                //电机控制器与仪表通讯报文2
                if (group[2] == CJ2)
                {
                    byte highByte1 = group[6];//电池电压高字节
                    byte lowByte1 = group[5];//电池电压低字节
                    byte highByte2 = group[8];//电机电流高字节
                    byte lowByte2 = group[7];//电机电流低字节
                    byte highByte3 = group[10];//电机温度高字节
                    byte lowByte3 = group[9];//电机温度低字节
                    byte highByte4 = group[12];//控制器温度高字节
                    byte lowByte4 = group[11];//控制器温度低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电池电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//电机电流
                    ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);//电机温度
                    ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);//控制器温度

                    int decimalValue1 = combined1;//电池电压
                    double DCV = 0.1 * decimalValue1;
                    int decimalValue2 = combined2;//电机电流
                    double DCA = 0.1 * decimalValue2;
                    int decimalValue3 = combined3;//电机温度
                    double DJT = 0.1 * decimalValue3;
                    int decimalValue4 = combined4;//控制器温度
                    double KZQT = 0.1 * decimalValue4;
                }
            }
        }
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
        public void AddBytes(byte[] newBytes)
        {

            foreach (byte b in newBytes)
            {
                buffer.Enqueue(b);
            }
        }
#endregion
        private void Cabincontrol_btn_Click(object sender, EventArgs e)
        {
            ucPanelTitle1.Title = Cabincontrol_btn.Text;
            childForm.Close();
            childForm = new FCKZYMFrom(db);
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
            
        }
        public async void Serve()
        {
            Server.Server server=new Server.Server();
           // await server.ConnectToServer("8.137.119.17",80);
          //  await server.Serve();
        }
        private void Home_Resize(object sender, EventArgs e)
        {
       
 

        }
        public void initialize()
        {
            dateTime_lab.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH点mm分ss秒");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            dateTime_lab.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH点mm分ss秒");
            try
            {
               // double bytesSent = bytesSentCounter.RawValue / 1048576; // Convert to MB/s
               // double bytesReceived = bytesReceivedCounter.RawValue / 1048576; // Convert to MB/s
               // double totalSpeed = bytesSent + bytesReceived;
               // labelSpeed.Text = $"{totalSpeed:F2} MB/s";
               // WLSpeed = totalSpeed;
               //IO();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void caozuo_timer2_Tick(object sender, EventArgs e)
        {
            elapsedTime = stopwatch.Elapsed;
            operate_lab.Text = $"操作时长: {elapsedTime:hh\\时mm\\分}";
          //  UpdateLabel();

           
        }
        private void UpdateLabel()
        {
           

        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopwatch.Reset();
            caozuo_timer2.Stop();
            operate_lab.Text = $"操作时长: {elapsedTime:hh\\时mm\\分}";
            //  UpdateLabel();
        }

        /// <summary>
        /// 初始化实时网络
        /// </summary>
        private void InitializeCounters()
        {
            var InterfaceName = "";
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    //Console.WriteLine("Interface Description : " + networkInterface.Description);实例名
                    //Console.WriteLine("Interface ID : " + networkInterface.Id);
                    //Console.WriteLine("Interface Name : " + networkInterface.Name);
                    //Console.WriteLine("Interface Speed : " + networkInterface.Speed);
                    //Console.WriteLine("Interface Type : " + networkInterface.NetworkInterfaceType);
                    //Console.WriteLine("Interface Status : " + networkInterface.OperationalStatus);
                    //Console.WriteLine("-------------------------------------");
                    InterfaceName = networkInterface.Description;
                    break;
                }
            }

            bytesSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", InterfaceName); // 替换 "InterfaceName" 为你的网络接口名
            bytesReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", InterfaceName); // 替换 "InterfaceName" 为你的网络接口名

        }

        private void ucPanelTitle1_Load(object sender, EventArgs e)
        {

        }
        

       

        private void label2_Click(object sender, EventArgs e)
        {
             
        }

        private void dl_lab_TextChanged(object sender, EventArgs e)
        {
            LoadDLImage();
        }

        private void label3_TextChanged(object sender, EventArgs e)
        {
            LoadSXImage();

        }
        /// <summary>
        /// 电池电量变化
        /// </summary>
        private void LoadDLImage()
        {
            try
            {
                if (dl_lab.Text.Replace("%", "").ToInt() <= 100 && dl_lab.Text.Replace("%", "").ToInt() > 80)
                {
                    DL_pictureBox.Image = Properties.Resources.电量_5格电量_copy_copy;
                }
                else if (dl_lab.Text.Replace("%", "").ToInt() <= 80 && dl_lab.Text.Replace("%", "").ToInt() > 60)
                {
                    DL_pictureBox.Image = Properties.Resources.电量_4格电量_copy;
                }
                else if (dl_lab.Text.Replace("%", "").ToInt() <= 60 && dl_lab.Text.Replace("%", "").ToInt() > 40)
                {
                    DL_pictureBox.Image = Properties.Resources.电量_3格电量;
                }
                else if (dl_lab.Text.Replace("%", "").ToInt() <= 40 && dl_lab.Text.Replace("%", "").ToInt() > 20)
                {
                    DL_pictureBox.Image = Properties.Resources.电量_2格电量;
                }
                else if (dl_lab.Text.Replace("%", "").ToInt() <= 20 && dl_lab.Text.Replace("%", "").ToInt() > 0)
                {
                    DL_pictureBox.Image = Properties.Resources.电量_1格电量;
                }
                else
                {
                    DL_pictureBox.Image = Properties.Resources.电量_0格电量;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }
        /// <summary>
        /// 水箱水量变化
        /// </summary>
        private void LoadSXImage()
        {
            try
            {
                if (sxsl_lab.Text.Replace("%", "").ToInt() <= 100 && sxsl_lab.Text.Replace("%", "").ToInt() > 70)
                {
                    sxsl_pictureBox.Image = Properties.Resources.水箱水位_高;
                }
                else if (sxsl_lab.Text.Replace("%", "").ToInt() <= 70 && sxsl_lab.Text.Replace("%", "").ToInt() > 40)
                {
                    sxsl_pictureBox.Image = Properties.Resources.水箱水位_中;
                }
                else if (sxsl_lab.Text.Replace("%", "").ToInt() <= 40 && sxsl_lab.Text.Replace("%", "").ToInt() > 10)
                {
                    sxsl_pictureBox.Image = Properties.Resources.水箱水位_低;
                }
                else
                {
                    sxsl_pictureBox.Image = Properties.Resources.水箱水位_断开;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadSXImage();
        }

   

        private void LJTS_btn_Click(object sender, EventArgs e)
        {
            childForm.Close();
            ucPanelTitle1.Title = LJTS_btn.Text;
           
            childForm = new LJTSForm(db);
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Dock = DockStyle.Fill;
            
           childForm.Tag = WLSpeed;
             
            childForm.Show();
        }

 
        private void XCCZYM_btn_Click(object sender, EventArgs e)
        { 
            childForm.Close();
            ucPanelTitle1.Title = XCCZYM_btn.Text;
            
            childForm = new XCCZYM(db);
           childForm.Tag = WLSpeed;
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
           
          
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        private void XCDTCKJCZJM_btn_Click(object sender, EventArgs e)
        {
            
            ucPanelTitle1.Title = XCDTCKJCZJM_btn.Text;
            childForm.Close();
            childForm = new XCDTCKJCZJMForm(db);
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        

        private void xtcz_btn_Click(object sender, EventArgs e)
        {
      
            ucPanelTitle1.Title = xtcz_btn.Text;
            childForm.Close();
            childForm = new XTCZForm(db);
            childForm.TopLevel = false;
            childForm.Tag = account + "," + realName + "," + UserId;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }
           
         
       

        private void YCCLGLD_btn_Click(object sender, EventArgs e)
        {
            childForm.Close();
            ucPanelTitle1.Title = YCCLGLD_btn.Text;

            childForm = new YCGLDForm(db);
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Dock = DockStyle.Fill;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Show();
        }

        private void xcjtym_btn_Click(object sender, EventArgs e)
        {
            ucPanelTitle1.Title = xcjtym_btn.Text;
            childForm.Close();
            childForm = new childForm.XCJTYMForm(db);
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        private void Cabin_btn_Click(object sender, EventArgs e)
        {
            ucPanelTitle1.Title = Cabin_btn.Text;
            childForm.Close();
            childForm = new FCKZYMFrom(db);
            childForm.TopLevel = false;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }
        #region io1
        public  void IO()
        {
            int lValue = 0;
             MultiCardCS_1.GA_GetExtDiValue(0, ref lValue, 1);
            //水位判断
            if (0 == (lValue & 0x0002))
            {
                //请上水
                //textBox4.Text = "请上水";
                sxsl_pictureBox.Image = Properties.Resources.水箱水位_断开;
                if (1 == (lValue & 0X0001))
                {
                    MessageBox.Show("传感器出错");
                    sxsl_pictureBox.Image = null;
                    return;
                }
            }
            else
            {
                if (0 == (lValue & 0x0001))
                {
                    //水位正常
                    sxsl_pictureBox.Image = Properties.Resources.水箱水位_中;
                }
                else
                {
                    //水已满
                    sxsl_pictureBox.Image = Properties.Resources.水箱水位_高;
                    
                }
            }
        }
        public async void DKBK()
        {
            int iRes = 0;
            //注意板卡端端口号必须和PC端端口号保持一致
            iRes = MultiCardCS_1.GA_Open(1, IPAddress, 60000, "192.168.2.2", 60000);

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

        private void ucPanelTitle1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
