using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
namespace CarProject.Method
{

    public class HomeMethod
    {
        public readonly SqlSugarClient db;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
      
       
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
           // JIDIANQI.MainWindow.DC();
            return "";
        }
        // 发送数据到指定服务器和端口
        public  async Task SendBytesAsync(NetworkStream stream, byte[] dataToSend)
        {
            if (stream.CanWrite)
            {
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            }
        }

        // 从NetworkStream中异步接收数据
        public  async Task<byte[]> ReceiveBytesAsync(NetworkStream stream, int bufferSize)
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
