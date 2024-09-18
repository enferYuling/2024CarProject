using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Method
{
    public class NetworkCommunication1
    {
        // 发送数据到指定服务器和端口
        public async Task SendBytesAsync(NetworkStream stream, byte[] dataToSend)
        {
            if (stream.CanWrite)
            {
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            }
        }

        // 从NetworkStream中异步接收数据
        public async Task<byte[]> ReceiveBytesAsync(NetworkStream stream, int bufferSize)
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
               
            }

            return receivedBytes;
        }
    }
}
