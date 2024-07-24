using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Linq;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Markup;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using System;

namespace JIDIANQI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private int clickCount1 = 0; // 跟踪按钮1点击次数
        private int clickCount2 = 0; // 跟踪按钮2点击次数
        private int clickCount3 = 0; // 跟踪按钮3点击次数
        private int clickCount4 = 0; // 跟踪按钮4点击次数
        private int clickCount5 = 0; // 跟踪按钮5点击次数
        private int clickCount6 = 0; // 跟踪按钮6点击次数
        private int clickCount7 = 0; // 跟踪按钮7点击次数
        private int clickCount8 = 0; // 跟踪按钮8点击次数
        private int clickCount9 = 1; // 跟踪称重传感器停止按钮点击次数

        private string IOip = "192.168.1.253";//IO板的IP
        private int IOport = 1030;
        private string DCip = "192.168.2.101";//电池的IP
        private int DCport = 8886;
        private string QBZip = "192.168.2.80";//前避障雷达的IP
        private int QBZport = 10137;
        private string HBZip = "192.168.2.80";//后避障雷达的IP
        private int HBZport = 10138;
        private string Tip = "192.168.2.80";//温度传感器的IP
        private int Tport = 10136;
        private string YLip = "192.168.2.80";//称重传感器的IP
        private int YLport = 10136;

        private TcpClient client;
        private bool YLRunning ;
        private bool TRunning = true;

        public MainWindow()
        {
            InitializeComponent();
        }
        private string GetLocalIPAddress()  //寻找本机IP地址
        {
            // 遍历所有网络接口
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // 忽略不活动的网络接口
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    // 遍历网络接口的IP地址信息
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        // 返回IPv4地址
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return "未找到IP地址";
        }

        private void BUTTONGETIP_Click(object sender, EventArgs e)
        {
            BJIP();
        }
        private void BJIP()
        {
            TEXTBOXSHOWIP.Text = GetLocalIPAddress();
            // 将IP地址显示在TextBox中        
        }

        //连接IO板
        private void BUTTONCONNECT_Click(object sender, RoutedEventArgs e)
        {
            CONNECTIO("192.168.1.253",1030);
        }
        private void CONNECTIO(string ioip,int ioport)
        {
            string ipAddress = ioip;
            int port = ioport;
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
                    MessageBox.Show("Connected to the server.");
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
        //字符串转字节的函数
        private byte[] ConvertHexToBytes(string[] hexStrings)
        {
            byte[] byteArray = new byte[10];
            for (int i = 0; i < hexStrings.Length; i++)
            {
                // 将16进制字符串转换为字节
                byteArray[i] = Convert.ToByte(hexStrings[i], 16);
            }
            return byteArray;
        }
        //通过网络发送字节
        private void SendBytesOverNetwork(byte[] data, string server, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        // 将字节数据写入流中
                        stream.Write(data, 0, data.Length);
                    }
                }
                //MessageBox.Show("Bytes sent successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending bytes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //通过网络接收字节
        public class NetworkCommunication
        {
            public async Task<byte[]> ReceiveBytesAsync(string serverIpAddress, int port, int bufferSize)
            {
                byte[] buffer = new byte[bufferSize];
                byte[] receivedBytes = null;

                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync(serverIpAddress, port).ConfigureAwait(false);
                        NetworkStream stream = client.GetStream();

                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                        if (bytesRead > 0)
                        {
                            receivedBytes = new byte[bytesRead];
                            Array.Copy(buffer, receivedBytes, bytesRead);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常，例如打印错误信息
                    MessageBox.Show(ex.Message);
                }
                return receivedBytes;
            }
        }

        //IO板
        //   降温风机1
        private void BUTTONON1_Click(object sender, RoutedEventArgs e)
        {
            JWFJ1();
        }
        private void JWFJ1() 
        {
            clickCount1++;
            string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount1 % 2 == 0)
            {
                //延时一秒
                Task.Delay(1000).ContinueWith((t) => {
                    SendBytesOverNetwork(byteArray0, server, port);
                }, TaskScheduler.Default);

            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //降温风机2
        private void BUTTONON2_Click(object sender, RoutedEventArgs e)
        {
            JWFJ2();
        }
        private void JWFJ2()
        {
            clickCount2++;
            string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount2 % 2 == 0)
            {
                //延时一秒
                Task.Delay(1000).ContinueWith((t) => {
                    SendBytesOverNetwork(byteArray0, server, port);
                }, TaskScheduler.Default);
            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //吸尘风机
        private void BUTTONON3_Click(object sender, RoutedEventArgs e)
        {
            XCFJ();
        }
        private void XCFJ()
        {
            clickCount3++;
            string[] hexValues0 = { "48", "3a", "01", "70", "03", "00", "00", "01", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "03", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount3 % 2 == 0)
            {
                //延时一秒
                Task.Delay(1000).ContinueWith((t) => {
                    SendBytesOverNetwork(byteArray0, server, port);
                }, TaskScheduler.Default);

            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //清洗水泵
        private void BUTTONON4_Click(object sender, RoutedEventArgs e)
        {
            QXSB();
        }
        private void QXSB()
        {
            clickCount4++;
            string[] hexValues0 = { "48", "3a", "01", "70", "04", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "04", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount4 % 2 == 0)
            {
                SendBytesOverNetwork(byteArray0, server, port);
            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //除尘震动电机
        private void BUTTONON5_Click(object sender, RoutedEventArgs e)
        {
            CCZDDJ();
        }
        private void CCZDDJ()
        {
            clickCount5++;
            string[] hexValues0 = { "48", "3a", "01", "70", "05", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "05", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount5 % 2 == 0)
            {
                SendBytesOverNetwork(byteArray0, server, port);
            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //运动控制卡
        private void BUTTONON6_Click(object sender, RoutedEventArgs e)
        {
            YDKZK();
        }
        private void YDKZK()
        {
            clickCount6++;
            string[] hexValues0 = { "48", "3a", "01", "70", "06", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "06", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount6 % 2 == 0)
            {
                SendBytesOverNetwork(byteArray0, server, port);
            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //输出7 水泵控制
        private void BUTTONON7_Click(object sender, RoutedEventArgs e)
        {
            SBKZ();
        }
        private void SBKZ()
        {
            clickCount7++;
            string[] hexValues0 = { "48", "3a", "01", "70", "07", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "07", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount7 % 2 == 0)
            {
                SendBytesOverNetwork(byteArray0, server, port);
            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //输出8 负压风机
        private void BUTTONON8_Click(object sender, RoutedEventArgs e)
        {
            FYFJ();
        }
        private void FYFJ()
        {
            clickCount8++;
            string[] hexValues0 = { "48", "3a", "01", "70", "08", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "08", "01", "00", "00", "45", "44" };//常开点闭合
            byte[] byteArray0 = ConvertHexToBytes(hexValues0);
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            // 发送字节数据
            if (clickCount8 % 2 == 0)
            {
                SendBytesOverNetwork(byteArray0, server, port);
            }
            else
            {
                SendBytesOverNetwork(byteArray1, server, port);
            }
        }
        //输出全部关闭
        private void BUTTON_ALL_OFF_Click(object sender, RoutedEventArgs e)
        {
            QBGB();
        }
        private void QBGB()
        {
            string[] hexValues1 = { "48", "3a", "01", "70", "01", "00", "00", "00", "45", "44" };//继电器1常开点断开
            string[] hexValues2 = { "48", "3a", "01", "70", "02", "00", "00", "00", "45", "44" };//继电器2常开点断开
            string[] hexValues3 = { "48", "3a", "01", "70", "03", "00", "00", "00", "45", "44" };//继电器3常开点断开
            string[] hexValues4 = { "48", "3a", "01", "70", "04", "00", "00", "00", "45", "44" };//继电器4常开点断开
            string[] hexValues5 = { "48", "3a", "01", "70", "05", "00", "00", "00", "45", "44" };//继电器5常开点断开
            string[] hexValues6 = { "48", "3a", "01", "70", "06", "00", "00", "00", "45", "44" };//继电器6常开点断开
            string[] hexValues7 = { "48", "3a", "01", "70", "07", "00", "00", "00", "45", "44" };//继电器7常开点断开
            string[] hexValues8 = { "48", "3a", "01", "70", "08", "00", "00", "00", "45", "44" };//继电器8常开点断开
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            byte[] byteArray2 = ConvertHexToBytes(hexValues2);
            byte[] byteArray3 = ConvertHexToBytes(hexValues3);
            byte[] byteArray4 = ConvertHexToBytes(hexValues4);
            byte[] byteArray5 = ConvertHexToBytes(hexValues5);
            byte[] byteArray6 = ConvertHexToBytes(hexValues6);
            byte[] byteArray7 = ConvertHexToBytes(hexValues7);
            byte[] byteArray8 = ConvertHexToBytes(hexValues8);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;
            SendBytesOverNetwork(byteArray1, server, port);
            SendBytesOverNetwork(byteArray2, server, port);
            SendBytesOverNetwork(byteArray3, server, port);
            SendBytesOverNetwork(byteArray4, server, port);
            SendBytesOverNetwork(byteArray5, server, port);
            SendBytesOverNetwork(byteArray6, server, port);
            SendBytesOverNetwork(byteArray7, server, port);
            SendBytesOverNetwork(byteArray8, server, port);
        }
        //输出全部开启
        private void BUTTON_ALL_ON_Click(object sender, RoutedEventArgs e)
        {
            QBKQ();
        }
        private void QBKQ()
        {
            string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//继电器1常开点闭合
            string[] hexValues2 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//继电器2常开点闭合
            string[] hexValues3 = { "48", "3a", "01", "70", "03", "01", "00", "00", "45", "44" };//继电器3常开点闭合
            string[] hexValues4 = { "48", "3a", "01", "70", "04", "01", "00", "00", "45", "44" };//继电器4常开点闭合
            string[] hexValues5 = { "48", "3a", "01", "70", "05", "01", "00", "00", "45", "44" };//继电器5常开点闭合
            string[] hexValues6 = { "48", "3a", "01", "70", "06", "01", "00", "00", "45", "44" };//继电器6常开点闭合
            string[] hexValues7 = { "48", "3a", "01", "70", "07", "01", "00", "00", "45", "44" };//继电器7常开点闭合
            string[] hexValues8 = { "48", "3a", "01", "70", "08", "01", "00", "00", "45", "44" };//继电器8常开点闭合
            byte[] byteArray1 = ConvertHexToBytes(hexValues1);
            byte[] byteArray2 = ConvertHexToBytes(hexValues2);
            byte[] byteArray3 = ConvertHexToBytes(hexValues3);
            byte[] byteArray4 = ConvertHexToBytes(hexValues4);
            byte[] byteArray5 = ConvertHexToBytes(hexValues5);
            byte[] byteArray6 = ConvertHexToBytes(hexValues6);
            byte[] byteArray7 = ConvertHexToBytes(hexValues7);
            byte[] byteArray8 = ConvertHexToBytes(hexValues8);
            // 服务器地址和端口
            string server = IOip;
            int port = IOport;

            SendBytesOverNetwork(byteArray1, server, port);
            SendBytesOverNetwork(byteArray2, server, port);
            SendBytesOverNetwork(byteArray3, server, port);
            SendBytesOverNetwork(byteArray4, server, port);
            SendBytesOverNetwork(byteArray5, server, port);
            SendBytesOverNetwork(byteArray6, server, port);
            SendBytesOverNetwork(byteArray7, server, port);
            SendBytesOverNetwork(byteArray8, server, port);
        }

        //电池       
        //处理接收到电池报文
        private Queue<byte> buffer = new Queue<byte>(); // 用于存储接收到的字节
        private Dictionary<int, List<byte[]>> classifiedGroups = new Dictionary<int, List<byte[]>>();
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
        public void AddBytes(byte[] newBytes)
        {
            int maxGroups = 70;
            int currentGroups = buffer.Count / groupSize;
            foreach (byte b in newBytes)
            {
                buffer.Enqueue(b);
            }
            // 如果当前数据组数超过最大限制，清除旧数据
            while (currentGroups > maxGroups)
            {
                // 从 buffer 中移除旧数据组
                for (int i = 0; i < groupSize && buffer.Count > 0; i++)
                {
                    buffer.TryDequeue(out byte _);
                }
                --currentGroups; // 更新当前数据组数
            }
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
                    int SOCmax = int.Parse(TextBoxSOCmax.Text);
                    int SOCmain = int.Parse(TextBoxSOCmain.Text);
                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电池组充放电总电流
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//电池组总电压

                    //string decimalValue = ByteToBinaryString(group[5]);//电池各状态
                    if ((group[5] & 0x01) != 0) //充电线是否连接，0未连接，1连接
                    {

                    }
                    else { }
                    if ((group[5] & 0x02) != 0) //电池组充电状态，0未充电，1充电
                    {
                        TextBoxchar_dischar.Text = "充电中";
                    }
                    else
                    {
                        TextBoxchar_dischar.Text = "未充电";
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
                        TextBoxFD.Text = "放电中";
                    }
                    else
                    {
                        TextBoxFD.Text = "未放电";
                    }
                    if ((group[5] & 0x20) != 0) //充电接触器状态，0断开，1闭合
                    {

                    }
                    else { }
                    int SOC = group[6];//SOC的值
                    string SOC1 = SOC.ToString();
                    TextBoxSOC.Text = SOC1 + "%";//SOC
                    if (SOC >= SOCmax)
                    {
                        TextBoxSOCtx.Text = "SOC超过最大值!";
                    }
                    else if (SOC <= SOCmain)
                    {
                        TextBoxSOCtx.Text = "SOC超过最小值!";
                    }
                    else
                    {
                        TextBoxSOCtx.Text = "SOC值正常!";
                    }
                    int decimalValue1 = combined1;//电池组充放电总电流
                    double TC = 0.1 * decimalValue1;
                    string TotalCurrent = TC.ToString();
                    TextBoxTcurrent.Text = "充放电总电流" + TotalCurrent + "A";//显示电池充放电总电流

                    int decimalValue2 = combined2;//电池组总电压
                    double TV = 0.1 * decimalValue2;
                    string TotalVoltage = TV.ToString();
                    TextBoxTvoltage.Text = "总电压" + TotalVoltage + "V";//显示电池组总电压

                    int decimalValue3 = group[11];//故障等级
                    int decimalValue4 = group[12];//故障码
                    if (decimalValue3 == 1) //一级故障，严重故障， 立即停车
                    {
                        if (decimalValue4 >= F1 && decimalValue4 <= F2)
                        {
                            string Fault = decimalValue4.ToString();
                            TextBoxFault.Text = "一级故障" + Fault;
                        }
                    }
                    if (decimalValue3 == 2)//二级故障，普通故障，限速50%运行
                    {
                        if (decimalValue4 >= F3 && decimalValue4 <= F4)
                        {
                            string Fault = decimalValue4.ToString();
                            TextBoxFault.Text = "二级故障" + Fault;
                        }
                    }
                    if (decimalValue3 == 3)//三级故障，报警故障，报警
                    {
                        if (decimalValue4 >= F5 && decimalValue4 <= F6)
                        {
                            string Fault = decimalValue4.ToString();
                            TextBoxFault.Text = "三级故障" + Fault;
                        }
                    }
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

                    int decimalValue1 = combined1;//最高单体电压
                    double SMAXV = 0.001 * decimalValue1;
                    string SMAXV1 = SMAXV.ToString();
                    TextBoxSMAXV.Text = "最高单体电压" + SMAXV1 + "V";//最高单体电压   

                    int decimalValue2 = combined2;//最低单体电压
                    double SMINV = 0.001 * decimalValue2;
                    string SMINV1 = SMAXV.ToString();
                    TextBoxSMINV.Text = "最低单体电压" + SMINV1 + "V";//最低单体电压

                    int decimalValue3 = combined3;//最大允许放电电流。
                    double MAXAC = 0.1 * decimalValue3;
                    string MAXAC1 = MAXAC.ToString();
                    TextBoxMAXAC.Text = "最大允许放电电流" + MAXAC1 + "A";//最大允许放电电流。

                    int decimalValue4 = group[9];//单体最高温度
                    double SMAXT = 1 * decimalValue4;
                    string SMAXT1 = SMAXT.ToString();
                    TextBoxSMAXT.Text = "单体最高温度" + SMAXT1 + "℃";//单体最高温度

                    int decimalValue5 = group[10];//单体最低温度
                    double SMINT = 0.001 * decimalValue5;
                    string SMINT1 = SMINT.ToString();
                    TextBoxSMINT.Text = "单体最低温度" + SMINT1 + "℃";//单体最低温度
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
                    int decimalValue2 = combined2;//最高允许充电电流

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
                    int decimalValue2 = combined2;//输出电流

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
                        if ((group[9] & 0x02) != 0) //
                        {

                        }
                        else { }
                    }
                    else
                    {
                        if ((group[9] & 0x02) != 0) //
                        {

                        }
                        else { }
                    }
                    if ((group[9] & 0x04) != 0) //刹车状态，0无刹车，1有刹车
                    {

                    }
                    else { }
                    if ((group[9] & 0x08) != 0) //运行模式，00默认模式，01经济模式，10高速模式
                    {
                        if ((group[9] & 0x10) != 0)
                        {

                        }
                        else { }
                    }
                    else
                    {
                        if ((group[9] & 0x10) != 0) //启动状态，0充电机关闭状态，1处于充电状态
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

                    int decimalValue1 = group[6];//故障代码
                    byte highByte1 = group[8];//电机转速高字节
                    byte lowByte1 = group[7];//电机转速低字节
                    byte highByte2 = group[10];//小计里程高字节
                    byte lowByte2 = group[9];//小计里程低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电机转速
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//小计里程

                    int decimalValue2 = combined1;
                    int decimalValue3 = combined2;

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
                    int decimalValue2 = combined2;//电机电流
                    int decimalValue3 = combined3;//电机温度
                    int decimalValue4 = combined4;//控制器温度
                }
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
        public Dictionary<int, List<byte[]>> GetClassifiedGroups()
        {
            return classifiedGroups;
        }
        public static string ByteToBinaryString(byte value)
        {
            // 使用内置的Convert函数将字节转换为二进制字符串，不包含前导0b
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        //显示电池各状态
        private  void BUTTONbattery_Click(object sender, RoutedEventArgs e)
        {
            DC();
        }
        public async  void DC()
        {
            string ipAddress = DCip;
            int port = DCport;
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
                    MessageBox.Show("Connected to the server.");
                }
                else
                {
                    // 如果连接已存在，可以跳过或提醒
                    MessageBox.Show("已存在活动的连接。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NetworkCommunication communicator = new NetworkCommunication();
            //ByteGroupProcessor processor = new ByteGroupProcessor();
            int bufferSize = 1024; // 缓冲区大小
            string server = ipAddress;
            //int port = int.Parse(TextBoxPort.Text);
            try
            {
                while (true) // 可以设置一个退出条件来代替 while(true)
                {
                    byte[] receivedBytes = await communicator.ReceiveBytesAsync(server, port, bufferSize);
                    if (receivedBytes != null && receivedBytes.Length > 0)
                    {
                        AddBytes(receivedBytes); // 添加到处理器
                        ProcessBufferedBytes();
                    }
                    else
                    {
                        // 没有接收到数据或发生错误，可以选择重试或退出
                        await Task.Delay(1000); // 可以选择暂停一段时间再重试
                        //break; // 或者退出循环
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show(ex.Message));
            }
        }

        //前后避障雷达报文解析
        private void QBZGroup(byte[] group)//前后避障雷达报文解析
        {

            byte highByte1 = group[5]; // 索引从0开始，所以第6个字节是索引5
            byte lowByte1 = group[6];  // 第7个字节是索引6
            byte highByte2 = group[7];
            byte lowByte2 = group[8];
            byte highByte3 = group[9];
            byte lowByte3 = group[10];
            byte highByte4 = group[11];
            byte lowByte4 = group[12];
            double MY = double.Parse(TextBoxSetMY.Text);//最远距离
            double NY = double.Parse(TextBoxSetNY.Text);//次远距离
            double NJ = double.Parse(TextBoxSetNJ.Text);//次近距离
            double MJ = double.Parse(TextBoxSetMJ.Text);//最近距离

            // 将两个字节合并为UInt16
            ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
            ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
            ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
            ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);
            // 转换为10进制并输出或处理
            int decimalValue1 = combined1;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
            double firstTD = 0.001 * decimalValue1;
            string TD1 = firstTD.ToString();//通道1所测距离
            TextBoxChannel1.Text = "通道1:" + TD1;
            /*if (firstTD >= MY) 
            { 
            
            }
            if (firstTD < MY && firstTD >= NY) 
            { 
            
            }
            if (firstTD < NY && firstTD >= NJ)
            {

            }
            if (firstTD < NJ && firstTD >= MJ)
            {

            }*/
            int decimalValue2 = combined2;
            double secondTD = 0.001 * decimalValue2;
            string TD2 = secondTD.ToString();//通道2所测距离
            TextBoxChannel2.Text = "通道2:" + TD2;
            /*if (secondTD >= MY) 
           { 

           }
           if (secondTD < MY && secondTD >= NY) 
           { 

           }
           if (secondTD < NY && secondTD >= NJ)
           {

           }
           if (secondTD < NJ &&secondTD >= MJ)
           {

           }*/

            int decimalValue3 = combined3;
            double thirdTD = 0.001 * decimalValue1;
            string TD3 = thirdTD.ToString();//通道3所测距离
            TextBoxChannel3.Text = "通道3:" + TD3;
            /*if (thirdTD >= MY) 
           { 

           }
           if (thirdTD < MY && thirdTD >= NY) 
           { 

           }
           if (thirdTD < NY && thirdTD >= NJ)
           {

           }
           if (thirdTD < NJ && thirdTD>= MJ)
           {

           }*/

            int decimalValue4 = combined4;
            double fourthTD = 0.001 * decimalValue1;
            string TD4 = fourthTD.ToString();//通道4所测距离
            TextBoxChannel4.Text = "通道4:" + TD4;
            /*if (fourthTD >= MY) 
           { 

           }
           if (fourthTD < MY && fourthTD >= NY) 
           { 

           }
           if (fourthTD < NY && fourthTD >= NJ)
           {

           }
           if (fourthTD < NJ && fourthTD >= MJ)
           {

           }*/
        }
        public void QBZBufferedBytes()
        {

            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    QBZGroup(group);
                }
            }
        }
        public void QBZBytes()
        {

            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    QBZGroup(group);
                }
            }
        }
        //后避障雷达报文解析
        private void HBZGroup(byte[] group)//前后避障雷达报文解析
        {

            byte highByte1 = group[5]; // 索引从0开始，所以第6个字节是索引5
            byte lowByte1 = group[6];  // 第7个字节是索引6
            byte highByte2 = group[7];
            byte lowByte2 = group[8];
            byte highByte3 = group[9];
            byte lowByte3 = group[10];
            byte highByte4 = group[11];
            byte lowByte4 = group[12];
            double MY = double.Parse(TextBoxSetMY.Text);//最远距离
            double NY = double.Parse(TextBoxSetNY.Text);//次远距离
            double NJ = double.Parse(TextBoxSetNJ.Text);//次近距离
            double MJ = double.Parse(TextBoxSetMJ.Text);//最近距离
            // 将两个字节合并为UInt16
            ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
            ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
            ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
            ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);
            // 转换为10进制并输出或处理
            int decimalValue1 = combined1;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
            double firstTD = 0.001 * decimalValue1;
            string TD1 = firstTD.ToString();//通道1所测距离
            TextBoxChannel5.Text = "通道1:" + TD1;
            /*if (firstTD >= MY) 
            { 
            
            }
            if (firstTD < MY && firstTD >= NY) 
            { 
            
            }
            if (firstTD < NY && firstTD >= NJ)
            {

            }
            if (firstTD < NJ && firstTD >= MJ)
            {

            }*/

            int decimalValue2 = combined2;
            double secondTD = 0.001 * decimalValue2;
            string TD2 = secondTD.ToString();//通道2所测距离
            TextBoxChannel6.Text = "通道2:" + TD2;
            /*if (secondTD >= MY) 
           { 

           }
           if (secondTD < MY && secondTD >= NY) 
           { 

           }
           if (secondTD < NY && secondTD >= NJ)
           {

           }
           if (secondTD < NJ &&secondTD >= MJ)
           {

           }*/

            int decimalValue3 = combined3;
            double thirdTD = 0.001 * decimalValue1;
            string TD3 = thirdTD.ToString();//通道3所测距离
            TextBoxChannel7.Text = "通道3:" + TD3;
            /*if (thirdTD >= MY) 
           { 

           }
           if (thirdTD < MY && thirdTD >= NY) 
           { 

           }
           if (thirdTD < NY && thirdTD >= NJ)
           {

           }
           if (thirdTD < NJ && thirdTD>= MJ)
           {

           }*/

            int decimalValue4 = combined4;
            double fourthTD = 0.001 * decimalValue1;
            string TD4 = fourthTD.ToString();//通道4所测距离
            TextBoxChannel8.Text = "通道4:" + TD4;
            /*if (fourthTD >= MY) 
          { 

          }
          if (fourthTD < MY && fourthTD >= NY) 
          { 

          }
          if (fourthTD < NY && fourthTD >= NJ)
          {

          }
          if (fourthTD < NJ && fourthTD >= MJ)
          {

          }*/
        }
        public void HBZBufferedBytes()
        {

            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    QBZGroup(group);
                }
            }
        }
        public void HBZBytes()
        {

            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    QBZGroup(group);
                }
            }
        }
        //连接前避障雷达，并获得所测距离
        private  void BUTTONfront_Click(object sender, RoutedEventArgs e)
        {
            QBZLD();
        }
        private async void QBZLD()
        {
            string ipAddress = QBZip;
            int port = QBZport;
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
                    MessageBox.Show("前避障雷达连接成功！");
                }
                else
                {
                    // 如果连接已存在，可以跳过或提醒
                    MessageBox.Show("已存在活动的连接。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NetworkCommunication communicator = new NetworkCommunication();
            int bufferSize = 1024; // 缓冲区大小
            string server = ipAddress;
            try
            {
                while (true) // 可以设置一个退出条件来代替 while(true)
                {
                    byte[] receivedBytes = await communicator.ReceiveBytesAsync(server, port, bufferSize);
                    if (receivedBytes != null && receivedBytes.Length > 0)
                    {
                        AddBytes(receivedBytes); // 添加到处理器
                        QBZBytes();
                    }
                    else
                    {
                        // 没有接收到数据或发生错误，可以选择重试或退出
                        break; // 或者退出循环
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show(ex.Message));
            }
        }
        //连接后避障雷达，并获得所测距离
        private void BUTTONrear_Click(object sender, RoutedEventArgs e)
        {
              HBZLD();
        }
        private async void HBZLD()
        {
            string ipAddress = HBZip;
            int port = HBZport;
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
                    MessageBox.Show("后避障雷达连接成功！");
                }
                else
                {
                    // 如果连接已存在，可以跳过或提醒
                    MessageBox.Show("已存在活动的连接。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NetworkCommunication communicator = new NetworkCommunication();
            //ByteGroupProcessor processor = new ByteGroupProcessor();
            int bufferSize = 1024; // 缓冲区大小
            string server = ipAddress;
            //int port = int.Parse(TextBoxPort.Text);
            try
            {
                while (true) // 可以设置一个退出条件来代替 while(true)
                {
                    byte[] receivedBytes = await communicator.ReceiveBytesAsync(server, port, bufferSize);
                    if (receivedBytes != null && receivedBytes.Length > 0)
                    {
                        AddBytes(receivedBytes); // 添加到处理器
                        HBZBytes();
                    }
                    else
                    {
                        // 没有接收到数据或发生错误，可以选择重试或退出
                        // await Task.Delay(1000); // 可以选择暂停一段时间再重试
                        break; // 或者退出循环
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show(ex.Message));
            }
        }
        //温度传感器报文解析      
        private void TemperatureGroup(byte[] group)
        {
            byte highByte1 = group[3]; // 通道1温度高字节
            byte lowByte1 = group[4];  // 通道1温度低字节
            byte highByte2 = group[5];// 通道2温度高字节
            byte lowByte2 = group[6]; // 通道2温度低字节
            byte highByte3 = group[7];// 通道3温度高字节
            byte lowByte3 = group[8]; // 通道3温度低字节
            byte highByte4 = group[9];// 通道4温度高字节
            byte lowByte4 = group[10];// 通道4温度低字节

            // 将两个字节合并为UInt16
            ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
            ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
            ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
            ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);

            //1路温度值
            if ((group[3] & 0x80) != 0)
            {
                // 直接将byte转换为int，已经是十进制数
                int t1 = ~combined1 + 1;
                double t11 = -t1 * 0.1;
                string tfirst = t11.ToString();
                TextBoxShowT1.Text = "1路温度值:" + tfirst + "℃";
                /*float tNMint = float.Parse(TextBoxSetNMint.Text);
                float tMint = float.Parse(TextBoxSetMint.Text);
                float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t11 >= tMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t11 >= tNMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t11<= tNMint) //预警
                {
                
                }
                if (t11 <= tMint)//停车
                {

                }*/
            }
            else
            {
                int t1 = combined1;
                double t11 = t1 * 0.1;
                string tfirst = t11.ToString();
                TextBoxShowT1.Text = "1路温度值:" + tfirst + "℃";
                /*float tNMint = float.Parse(TextBoxSetNMint.Text);
                float tMint = float.Parse(TextBoxSetMint.Text);
                float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t1 >= tMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t1 >= tNMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t1 <= tNMint) //预警
                {

                }
                if (t1 <= tMint)//停车
                {

                }*/
            }
            //2路温度值
            if ((group[5] & 0x80) != 0)
            {
                // 直接将byte转换为int，已经是十进制数
                int t2 = ~combined2 + 1;
                double t22 = -t2 * 0.1;
                string tsecond = t22.ToString();
                TextBoxShowT2.Text = "2路温度值:" + tsecond + "℃";
                /*float tNMint = float.Parse(TextBoxSetNMint.Text);
                float tMint = float.Parse(TextBoxSetMint.Text);
                float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t22 >= tMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t22 >= tNMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t22 <= tNMint) //预警
                {

                }
                if (t22 <= tMint)//停车
                {

                }*/
            }
            else
            {
                int t2 = combined2;
                double t22 = t2 * 0.1;
                string tsecond = t22.ToString();
                TextBoxShowT2.Text = "2路温度值:" + tsecond + "℃";
                /*float tNMint = float.Parse(TextBoxSetNMint.Text);
                float tMint = float.Parse(TextBoxSetMint.Text);
                float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t2 >= tMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t2 >= tNMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t2 <= tNMint) //预警
                {

                }
                if (t2 <= tMint)//停车
                {

                }*/
            }
            //3路温度值
            if ((group[7] & 0x80) != 0)
            {
                // 直接将byte转换为int，已经是十进制数
                int t3 = ~combined3 + 1;
                double t33 = -t3 * 0.1;
                string tthird = t33.ToString();
                TextBoxShowT3.Text = "3路温度值:" + tthird + "℃";
                /*float tNMint = float.Parse(TextBoxSetNMint.Text);
                float tMint = float.Parse(TextBoxSetMint.Text);
                float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t33 >= tMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t33 >= tNMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t33 <= tNMint) //预警
                {

                }
                if (t33 <= tMint)//停车
                {

                }*/
            }
            else
            {
                int t3 = combined3;
                double t33 = t3 * 0.1;
                string tthird = t33.ToString();
                TextBoxShowT3.Text = "3路温度值:" + tthird + "℃";
                /*  float tNMint = float.Parse(TextBoxSetNMint.Text);
                 float tMint = float.Parse(TextBoxSetMint.Text);
                 float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                 float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t3 >= tMaxt)
                 {
                     string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                     byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                     // 服务器地址和端口
                     string server = "192.168.1.253";
                     int port = 1030;
                     // 发送字节数据  
                     SendBytesOverNetwork(byteArray1, server, port);
                 }
                 else
                 {
                     string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                     byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                     // 服务器地址和端口
                     string server = "192.168.1.253";
                     int port = 1030;
                     //延时一秒
                     Task.Delay(1000).ContinueWith((t) =>
                     {
                         SendBytesOverNetwork(byteArray0, server, port);
                     }, TaskScheduler.Default);
                 }
                 if (t3 >= tNMaxt)
                 {
                     string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                     byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                     // 服务器地址和端口
                     string server = "192.168.1.253";
                     int port = 1030;
                     // 发送字节数据  
                     SendBytesOverNetwork(byteArray1, server, port);
                 }
                 else
                 {
                     string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                     byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                     // 服务器地址和端口
                     string server = "192.168.1.253";
                     int port = 1030;
                     //延时一秒
                     Task.Delay(1000).ContinueWith((t) =>
                     {
                         SendBytesOverNetwork(byteArray0, server, port);
                     }, TaskScheduler.Default);
                 }
                 if (t3 <= tNMint) //预警
                 {

                 }
                 if (t3 <= tMint)//停车
                 {

                 }*/
            }
            //4路温度值
            if ((group[9] & 0x80) != 0)
            {
                // 直接将byte转换为int，已经是十进制数
                int t4 = ~combined4 + 1;
                double t44 = -t4 * 0.1;
                string tfourth = t44.ToString();
                TextBoxShowT4.Text = "4路温度值:" + tfourth + "℃";
                /*float tNMint = float.Parse(TextBoxSetNMint.Text);
                float tMint = float.Parse(TextBoxSetMint.Text);
                float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t44 >= tMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t44 >= tNMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t44 <= tNMint) //预警
                {

                }
                if (t44 <= tMint)//停车
                {

                }*/
            }
            else
            {
                int t4 = combined4;
                double t44 = t4 * 0.1;
                string tfourth = t44.ToString();
                TextBoxShowT4.Text = "4路温度值:" + tfourth + "℃";
                /*float tNMint = float.Parse(TextBoxSetNMint.Text);
                float tMint = float.Parse(TextBoxSetMint.Text);
                float tMaxt = float.Parse(TextBoxSetMaxt.Text);
                float tNMaxt = float.Parse(TextBoxSetNMaxt.Text);
                if (t4 >= tMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "01", "01", "00", "00", "45", "44" };//降温风机1打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "01", "00", "00", "05", "45", "44" };//常开点断开，降温风机1关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t4 >= tNMaxt)
                {
                    string[] hexValues1 = { "48", "3a", "01", "70", "02", "01", "00", "00", "45", "44" };//常开点闭合，降温风机2打开
                    byte[] byteArray1 = ConvertHexToBytes(hexValues1);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    // 发送字节数据  
                    SendBytesOverNetwork(byteArray1, server, port);
                }
                else
                {
                    string[] hexValues0 = { "48", "3a", "01", "70", "02", "00", "00", "01", "45", "44" };//常开点断开，降温风机2关闭
                    byte[] byteArray0 = ConvertHexToBytes(hexValues0);
                    // 服务器地址和端口
                    string server = "192.168.1.253";
                    int port = 1030;
                    //延时一秒
                    Task.Delay(1000).ContinueWith((t) =>
                    {
                        SendBytesOverNetwork(byteArray0, server, port);
                    }, TaskScheduler.Default);
                }
                if (t4 <= tNMint) //预警
                {

                }
                if (t4 <= tMint)//停车
                {

                }
                */
            }
        }
        public void TemperatureBytes()
        {

            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    TemperatureGroup(group);
                }
            }
        }
        

        //按键连接温度传感器，并获取温度值
        private void ButtonGetT_Click(object sender, RoutedEventArgs e)
        {
            WD();
        }
        private async void WD()
        {
            TRunning = true;
            string serverIpAddress = Tip;
            int port = Tport;
            int bufferSize = 1024;
            string[] hexStrings = { "64", "03", "00", "01", "00", "04", "1C", "3C" };
            byte[] bytesToSend = hexStrings.Select(s => Convert.ToByte(s, 16)).ToArray();

            NetworkCommunication1 communicator = new NetworkCommunication1();
            TcpClient client = new TcpClient();

            try
            {
                // 连接到服务器
                // 连接到服务器
                client = new TcpClient();
                await client.ConnectAsync(serverIpAddress, port);
                MessageBox.Show("Connected to the server.");

                // 获取NetworkStream用于接收
                using (NetworkStream stream = client.GetStream())
                {
                    // 获取NetworkStream用于发送和接收
                    using (NetworkStream networkStream = client.GetStream())
                    {
                        while (TRunning)
                            // 发送数据
                            await NetworkCommunication1.SendBytesAsync(networkStream, bytesToSend);
                        MessageBox.Show("Data sent to the server.");

                        // 接收数据
                        byte[] receivedData = await NetworkCommunication1.ReceiveBytesAsync(networkStream, bufferSize);
                        if (receivedData != null && receivedData.Length > 0)
                        {
                            // 处理接收到的数据
                            AddBytes(receivedData); // 添加到处理器
                            TemperatureBytes();
                        }
                        else
                        {
                            MessageBox.Show("No data received or connection closed by the server.");
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            finally
            {
                // 确保关闭TcpClient
                if (client != null && client.Connected)
                {
                    client.Close();
                }
            }
        }
        private void ButtonDK_Click(object sender, RoutedEventArgs e)
        {
            DK();
        }
        private void DK()
        {
            if (TRunning)
            {
                TRunning = false;
                // 确保关闭TcpClient
                try
                {
                    if (client != null && client.Connected)
                    {
                        client.Close();
                        MessageBox.Show("断开连接 ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when stopping: " + ex.Message);
                }
            }
        }
              

        //压力传感器报文解析
        private void YLGroup(byte[] group)
        {
            byte highByte1 = group[3];// 通道1压力高字节
            byte lowByte1 = group[4]; // 通道1压力低字节
            byte highByte2 = group[5];// 通道1压力高字节
            byte lowByte2 = group[6]; // 通道1压力低字节
            byte highByte3 = group[7];// 通道2压力高字节
            byte lowByte3 = group[8]; // 通道2压力低字节
            byte highByte4 = group[9];// 通道2压力高字节
            byte lowByte4 = group[10];// 通道2压力低字节
            /*//设置阈值1
            int smaxl1 = int.Parse(TextBoxSMAXL1.Text);
            int smainl1 = int.Parse(TextBoxSMAINL1.Text);
            int smaxy1 = int.Parse(TextBoxSMAXY1.Text);
            int smainy1 = int.Parse(TextBoxSMAINY1.Text);
            //设置阈值2
            int smaxl2 = int.Parse(TextBoxSMAXL2.Text);
            int smainl2 = int.Parse(TextBoxSMAINL2.Text);
            int smaxy2 = int.Parse(TextBoxSMAXY2.Text);
            int smainy2 = int.Parse(TextBoxSMAINY2.Text);*/

            // 将两个字节合并为UInt16
            ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
            ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
            UInt32 finally1 = (UInt32)(combined1 << 16) | combined2;

            ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
            ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);
            UInt32 finally2 = (UInt32)(combined3 << 16) | combined4;

            //1路压力值
            if ((group[3] & 0x80) != 0)
            {
                // 直接将byte转换为int，已经是十进制数
                int YL1 = (int)(~finally1 + 1);
                int YL12 = -YL1;
                string YLfirst = YL12.ToString();
                TextBoxYL1.Text = "1路压力值:" + YLfirst + "g";
                /*if (YL12>=(0- smainy1)) //压力值为负
                {
                
                }
                if (YL12 <(0 - smainy1)&& YL12 >= (0 - smaxy1)) //压力值为负
                {

                }
                if (YL12 < (0 - smaxy1)) //压力值为负
                {

                }*/
            }
            else
            {
                int YL1 = (int)finally1;
                string YLfirst = YL1.ToString();
                TextBoxYL1.Text = "1路拉力值:" + YLfirst + "g";
                /*if (YL1<= smainl1) //拉力值为正
                {
                
                }
                if (YL1> smainl1&& YL1 <= smaxl1) //拉力值为正
                {

                }
                if (YL1> smaxl1) //拉力值为正
                {

                }*/
            }

            //2路压力值
            if ((group[7] & 0x80) != 0)
            {
                // 直接将byte转换为int，已经是十进制数
                int YL2 = (int)(~finally2 + 1);
                int YL22 = -YL2;
                string YLsecond = YL22.ToString();
                TextBoxYL2.Text = "2路压力值:" + YLsecond + "g";
                /*if (YL22>=(0- smainy2)) //压力值为负
                {
                
                }
                if (YL22 <(0 - smainy2)&& YL22 >= (0 - smaxy2)) //压力值为负
                {

                }
                if (YL22 < (0 - smaxy2)) //压力值为负
                {

                }*/
            }
            else
            {
                int YL2 = (int)finally2;
                string YLsecond = YL2.ToString();
                TextBoxYL2.Text = "2路拉力值:" + YLsecond + "g";
                /*if (YL2<= smainl2) //拉力值为正
                {
                
                }
                if (YL2> smainl1&& YL2 <= smaxl2) //拉力值为正
                {

                }
                if (YL2> smaxl2) //拉力值为正
                {

                }*/
            }
        }
        public void YLBytes()
        {

            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    YLGroup(group);
                }
            }
        }
        //按键连接压力传感器
        private  void ButtonGetW_Click(object sender, RoutedEventArgs e)
        {
            YL();
        }
        private async void YL()
        {
            YLRunning = true;
            string serverIpAddress = YLip;
            int port = YLport;
            int bufferSize = 1024;
            string[] hexStrings = { "01", "03", "01", "C2", "00", "04", "E4", "09" };
            byte[] bytesToSend = hexStrings.Select(s => Convert.ToByte(s, 16)).ToArray();

            NetworkCommunication1 communicator = new NetworkCommunication1();
            //TcpClient client = new TcpClient();

            try
            {
                // 连接到服务器
                client = new TcpClient();
                await client.ConnectAsync(serverIpAddress, port);
                MessageBox.Show("Connected to the server.");

                // 获取NetworkStream用于接收
                using (NetworkStream stream = client.GetStream())
                {
                    // 获取NetworkStream用于发送和接收
                    using (NetworkStream networkStream = client.GetStream())
                    {
                        while (YLRunning)
                        {
                            // 发送数据
                            await NetworkCommunication1.SendBytesAsync(networkStream, bytesToSend);
                            // MessageBox.Show("Data sent to the server.");

                            // 接收数据
                            byte[] receivedData = await NetworkCommunication1.ReceiveBytesAsync(networkStream, bufferSize);
                            if (receivedData != null && receivedData.Length > 0)
                            {

                                // 处理接收到的数据
                                AddBytes(receivedData); // 添加到处理器                                
                                YLBytes();
                            }
                            else
                            {
                                MessageBox.Show("No data received or connection closed by the server.");
                            }
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            finally
            {
                // 确保关闭TcpClient
                if (client != null && client.Connected)
                {
                    client.Close();
                }
            }
        }

        //停止接收
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            TZ();
        }
        private void TZ()
        {
            if (YLRunning)
            {
                YLRunning = false;
                // 确保关闭TcpClient
                try
                {
                    if (client != null && client.Connected)
                    {
                        client.Close();
                        MessageBox.Show("断开连接 ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when stopping: " + ex.Message);
                }
            }
        }

     public class NetworkCommunication1
     {
          // 发送数据到指定服务器和端口
           public static async Task SendBytesAsync(NetworkStream stream, byte[] dataToSend)
           {
            if (stream.CanWrite)
            {
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            }
           }

        // 从NetworkStream中异步接收数据
        public static async Task<byte[]> ReceiveBytesAsync(NetworkStream stream, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            byte[] receivedBytes = null;

            try
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {                   
                    receivedBytes = new byte[bytesRead];
                    Array.Copy(buffer, receivedBytes, bytesRead);
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                MessageBox.Show(ex.Message);
            }

            return receivedBytes;
        }
     }

       
    }
}
 