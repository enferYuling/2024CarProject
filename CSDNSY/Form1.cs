using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;



namespace CSDNSY
{
    public partial class Form1 : Form
    {
        int w;//定义当前窗体的宽度
        int h;//定义当前窗体的高度
        public Form1()
        {
            InitializeComponent();
            //拿到屏幕的长和宽
            w = System.Windows.Forms.SystemInformation.VirtualScreen.Width;
            h = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitCamera();
            
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
        Queue<(int width, int height, byte[] bytes)> frameQueue = new Queue<(int width, int height, byte[] bytes)>();     
        // 转换并显示的线程
        Thread displayThread;


        HZCameraControl mCameraControl = new HZCameraControl();
        
        //初始化SDK
        void InitCamera()
        {
            //监控初始化
            if (!mCameraControl.Init())
            {
                MessageBox.Show("监控初始化失败");
                return;
            }
            //用户登录  网络相机IP地址 端口号(默认9000) 登录名  密码
           // if (!mCameraControl.Login("115.236.153.177", 53672, "admin", "lwny1234"))
         if (!mCameraControl.Login("192.168.1.4", 9000, "admin", "lwny1234"))
           // if (!mCameraControl.Login("192.168.2.19", 9000, "admin", "lwny1234"))
            {
                MessageBox.Show("用户登录失败");
                return;
            }
            
           bool q= mCameraControl.SetConfig("192.168.1.4", 9000, "admin", "lwny1234");
            ////设置解码回调
            //mCameraControl.mDecCBFunc = DecodeCallBack;


            //// 启动转换并显示的线程
            //displayThread = new Thread(DisplayFrames);
            //displayThread.Start();

            ////获取监控数据
            //if (!mCameraControl.OpenPlay())
            //{
            //    return;
            //}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            if (displayThread != null && displayThread.IsAlive)
                displayThread.Abort();

            mCameraControl.Stop();        
        }
        public delegate void RefreshFrameHandle();
        RefreshFrameHandle refreshFramCallback;
        void DecodeCallBack(int nPort, IntPtr pBuf, int nSize, HZ_PLAY.FRAME_INFO pFrameInfo, IntPtr pUserData, int nReserved2)
        {
            //限制画面刷新
            mCameraControl.isUpdate = false;
            if (pFrameInfo.nType == 3)
            {
                //监控视频YUV流转RGB图片
                int width = pFrameInfo.nWidth;
                int height = pFrameInfo.nHeight;
                if (width <= 0 || height <= 0)
                    return;
                byte[] yuvbytes = new byte[nSize];
                Marshal.Copy(pBuf, yuvbytes, 0, yuvbytes.Length);              
                // 将新帧加入队列，并保持队列中只有最后一帧
                lock (frameQueue)
                {
                    frameQueue.Clear();
                    frameQueue.Enqueue((width, height, yuvbytes));
                }
               // try
                //{
                    /*refreshFramCallback = new RefreshFrameHandle(() =>
                    {
                        //在主线程中处理数据
                        //RefreshFrame(width, height, bytes);
                        RefreshFrame();
                    });
                    this?.Invoke(refreshFramCallback);*/
                   /* lock (frameQueue)
                    {
                        if (frameQueue.Count > 0)
                        {
                            var frameData = frameQueue.Dequeue();
                            using (Mat mat = new Mat(frameData.height + frameData.height / 2, frameData.width, MatType.CV_8UC1, frameData.bytes))
                            {
                                Mat dst = new Mat();
                                // YUV 转 RGB
                                Cv2.CvtColor(mat, dst, ColorConversionCodes.YUV2RGB_YV12);
                                // 显示监控画面
                                this.pictureBox1.Image = dst.ToBitmap();
                                dst.Dispose();// 释放资源
                                mat.Dispose();// using 代码块种 mat 会自动释放
                            }
                        }
                    }*/
               // }
                //catch { }
              
                              
            }
        }
       
        void DisplayFrames()
        {
            try
            {
                while (true)
                {
                    lock (frameQueue)
                    {
                        if (frameQueue.Count > 0)
                        {
                            var frameData = frameQueue.Dequeue();
                             using (Mat mat = new Mat(frameData.height + frameData.height / 2, frameData.width, MatType.CV_8UC1, frameData.bytes))
                             {
                                 Mat dst = new Mat();
                                 // YUV 转 RGB
                                 Cv2.CvtColor(mat, dst, ColorConversionCodes.YUV2RGB_YV12);
                                 // 显示监控画面
                                 this.pictureBox1.Image = dst.ToBitmap();
                                 dst.Dispose();// 释放资源
                                 mat.Dispose();// using 代码块种 mat 会自动释放
                             }                          
                        }
                    }
                    Thread.Sleep(10);
                }
            }
            catch (ThreadAbortException)
            {
                // 线程被终止时的处理
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / w;
            float newy = (this.Height) / h;
            setControls(newx, newy, this);
        }
        /*void RefreshFrame()
{
   try
   {
       lock (frameQueue)
       {
           if (frameQueue.Count > 0)
           {
               var frameData = frameQueue.Dequeue();
               using (Mat mat = new Mat(frameData.height + frameData.height / 2, frameData.width, MatType.CV_8UC1, frameData.bytes))
               {
                   Mat dst = new Mat();
                   // YUV 转 RGB
                   Cv2.CvtColor(mat, dst, ColorConversionCodes.YUV2RGB_YV12);
                   // 显示监控画面
                   this.pictureBox1.Image = dst.ToBitmap();
                   dst.Dispose();// 释放资源
                   mat.Dispose();// using 代码块种 mat 会自动释放
               }
           }
       }
   }
   catch (ObjectDisposedException e)
   {
       MessageBox.Show("fresh:" + e.TargetSite.Name);
   }
}*/


        //加载OpenCVSharp4 nuget包进行数据转换
        /*void RefreshFrame(int width, int height, byte[] bytes)
        {
             try
             {
                #region OpenCV YUV转Mat                                     
                using (Mat mat = new Mat(height + height / 2, width, MatType.CV_8UC1, bytes))                       
                {
                    Mat dst = new Mat();
                    //YUV转RGB
                    Cv2.CvtColor(mat, dst, ColorConversionCodes.YUV2RGB_YV12);
                    //显示监控画面
                    this.pictureBox1.Image = dst.ToBitmap();
                    dst.Dispose();//释放资源
                    mat.Dispose();// using代码块种mat会自动释放 
                }               
                 #endregion
             }
             catch (ObjectDisposedException e)
             {
                 MessageBox.Show("fresh:" + e.TargetSite.Name);
             }
        }*/










    }
}
