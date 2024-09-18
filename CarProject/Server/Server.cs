using Emgu.CV.Cuda;
using HZH_Controls;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
 

namespace CarProject.Server
{

    public class Server
    {
        Thread threadClient = null; // 创建用于接收服务端消息的 线程；  
        Socket sockClient = null;

        public async Task<string>  Serve(string serverIp, int port)
        {
            //string serverIp = "8.137.119.17";
            //int port = 8080;
           TcpClient client = new TcpClient();
            client.Connect(serverIp, port);
            Console.WriteLine("已连接");
            NetworkStream stream=client.GetStream();
            
            byte[] buffer = new byte[100024];
           
            int bytesread=stream.Read(buffer, 0, buffer.Length);
            string dataReceived=Encoding.ASCII.GetString(buffer,0,bytesread);
            Console.WriteLine("接收的数据：" + dataReceived);
            //关闭连接
            stream.Close();
            client.Close();
            return dataReceived;
        }
         
    }
   
}
