using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JIDIANQI;

namespace CarProject.Method
{

    public class HomeMethod
    {
        public readonly SqlSugarClient db;
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

        private TcpClient client;
        private bool YLRunning;
        private bool TRunning = true;
       
        public HomeMethod(SqlSugarClient datadb)
        {
            this.db = datadb;
        }
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public string GetLocalIPAddress()  //寻找本机IP地址
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
            return "";
        }
        /// <summary>
        /// 获取电池温度
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
     public string GetDCWD(string IPAddress)
        {
            //mainWindow.DC();
            return "";
        }
    }
}
