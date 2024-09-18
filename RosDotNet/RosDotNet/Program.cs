// See https://aka.ms/new-console-template for more information

using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Threading.Tasks;
using WebSocket = System.Net.WebSockets.WebSocket;
using WebSocketState = System.Net.WebSockets.WebSocketState;
public class Program
{
   public static async Task Main(string[] args)
    {

        // 使用方法:
     await WebSocket1("ws://192.168.2.150:9090");
    }
    public async static Task Serve(string ip,int port)
    {
        try
        {
            TcpClient client = new TcpClient(ip, port); // 替换为服务器IP和端口
            NetworkStream stream = client.GetStream();

            try
            {

                byte[] buffer = new byte[1024];
                // 发送消息给服务器
                string messageToSend = "{ \"op\": \"subscribe\",\r\n  \"topic\":\"/o3d_points\",\r\n  \"type\": \"std_msgs/Float32MultiArray\"\r\n}";
                byte[] messageBytes = Encoding.UTF8.GetBytes(messageToSend);
                stream.Write(messageBytes, 0, messageBytes.Length);

                // 接收服务器
                // int bytesRead1 = stream.Read(buffer, 0, buffer.Length);
                //Console.WriteLine("嘿嘿" + bytesRead1);
                //string receivedData1 = Encoding.UTF8.GetString(buffer, 0, bytesRead1);
                //Console.WriteLine("嘿嘿" + receivedData1 );
                string msg = "";
                string base64String = "";
               List<string> list = new List<string>(); ;
                 
                while (true)
                {
                    // 接收服务器的 Base64 数据并解码
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                   string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("十进制表示：" + string.Join(", ", list));

                }

                //byte[] decodedData = Convert.FromBase64String(base64String);

                //// 将解码后的字节数组转换为十进制表示
                //

                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            // 在结束时关闭TcpClient
            client.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        } 
    }
    /// <summary>
    /// web1
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
 public async static Task  WebSocket1(string ip )
    {
      //  Uri webSocketUri = new("ws://192.168.2.150:8001");
        Uri webSocketUri = new(ip);
        using SocketsHttpHandler handler = new();
        using ClientWebSocket ws = new();
        try
        {
            //   // 连接
            await ws.ConnectAsync(webSocketUri, default);
            // 构造发送的 Json 内容 订阅主题
            var replyMess = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new
            {
                op = "subscribe",
                topic = "/o3d_points",
                type = "std_msgs/Float32MultiArray"
            }));
            // 发送
            await ws.SendAsync(new ArraySegment<byte>(replyMess), WebSocketMessageType.Text, true, default);
            // 接收一次消息
            //while (true)
            //{
              var bytes = new byte[5000000];
                var result = await ws.ReceiveAsync(bytes, default);
                // var result= Task.Run(async () => await ws.ReceiveAsync(bytes, default)).Result;
                string res = Encoding.UTF8.GetString(bytes, 0, result.Count);
                //  string res= BitConverter.ToString(bytes).Replace("-", ""); // 将字节数组转换为十六进制字符串，并去除其中的连字符;

                var ros = res.Split(':');
                var str1 = ros[7].Replace("[", "").Replace("]", "");
                string[] rosdata = str1.Split(", ");

                Console.WriteLine(res);

          //  }
            ;
            // 关闭退出
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closed", default);
            //   Console.ReadL    
            //  await Serve("8.137.119.17",8888);
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.ReadLine();
            // Console.ReadLine();
        }
    }
    private static CancellationTokenSource _cts = new CancellationTokenSource();
    /// <summary>
    /// 开始web服务
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public async static Task StartAsync(string uri)
    {
        var listener = new WebSocketListener();

        listener.Start(uri, async (socket, uri) =>
        {
            while (!_cts.IsCancellationRequested && socket.State == WebSocketState.Open)
            {
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result;
                var segment = new ArraySegment<byte>(buffer);

                try
                {
                    do
                    {
                        result = await socket.ReceiveAsync(segment, _cts.Token);
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, _cts.Token);
                        }
                        else
                        {
                            var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                            Console.WriteLine("Received: " + message);
                        }
                    }
                    while (!result.EndOfMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    break;
                }
            }
        });

        Console.WriteLine("Server started on " + uri);
        await Task.Delay(-1, _cts.Token);
    }
    /// <summary>
    /// 停止web服务
    /// </summary>
    public void Stop()
    {
        _cts.Cancel();
    }
    public class WebSocketListener
    {
        public void Start(string uri, Func<WebSocket, Uri, Task> processRequest)
        {
            // Implementation goes here
        }
    }
}
