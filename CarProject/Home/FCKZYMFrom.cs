

using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CarProject.Method;
using System.Net.Sockets;
using System.Net;
using Messages.dynamic_reconfigure;
using System.Runtime.InteropServices;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;


namespace CarProject.Home
{
    public partial class FCKZYMFrom : Form
    {
        //private VideoCapture capture;
        //private Mat frame=new Mat();

        int w;//定义当前窗体的宽度
        int h;//定义当前窗体的高度
        public readonly SqlSugarClient db;
        public HomeMethod homemethod;
        public FCKZYMFrom(SqlSugarClient datadb)
        {
            InitializeComponent();
            //拿到屏幕的长和宽
            w = System.Windows.Forms.SystemInformation.VirtualScreen.Width;
            h = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
            this.db = datadb;
            //CvInvoke.UseOpenCL = true;
            //try
            //{
            //     capture = new VideoCapture();

            //    capture.ImageGrabbed += ProcessFrame;


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            homemethod = new HomeMethod(db);
        }

        private void FCKZYMFrom_Load(object sender, EventArgs e)
        {
            //IO();
            //  InitCamera();
            string propath = Directory.GetCurrentDirectory();//项目地址
            string processName = "EasyPlayer";

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
            FCSNKXZSXT(propath);
            FCSNGDSXT(propath);
            FCSWGDSXT(propath);
        }

        private void lighting_img_Click(object sender, EventArgs e)
        {
            if (lighting_img.Tag.ToString() == "开")
            {
               // lighting_img.Image = imageList1.Images[3];
                lighting_img.Image = Properties.Resources.开关1__1_;
                lighting_img.Tag = "关";
            }
            else
            {
                lighting_img.Image = Properties.Resources.开关2;
                lighting_img.Tag = "开";
            }
        }

        private void byhdl_lab_Click(object sender, EventArgs e)
        {

        }
        #region IO
        public void IO()
        {

            CONNECTfunction("115.236.153.177", 13408);
        }
       
        //连接函数
        private void CONNECTfunction(string ip, int Desport)
        {
            string ipAddress = ip;
            int port = Desport;

            try
            {
                // 连接到硬件设备
                tcpClient = new TcpClient();
                tcpClient.Connect(ipAddress, port);
                networkStream = tcpClient.GetStream();
                //MessageBox.Show("Connected to the server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败!" + ex.Message);
            }
        }
        #endregion
        
        private async void JWFJ2(string ioip, int ioport)
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
                await homemethod.SendBytesAsync(networkStream, bytesBH);
                
                //延时一秒
                await Task.Delay(1000);
                await homemethod.SendBytesAsync(networkStream, bytesDK);
                
            }
        }
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        /// <summary>
        /// 门升
        /// </summary>
        /// <param name="ioip"></param>
        /// <param name="ioport"></param>
        private async void JWFJ1(string ioip, int ioport)
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

                await homemethod.SendBytesAsync(networkStream, bytesBH);
              
                //延时一秒
                await Task.Delay(1000);
                await homemethod.SendBytesAsync(networkStream, bytesDK);

            }
        }
        private void Water_img_Click(object sender, EventArgs e)
        {
            if (Water_img.Tag.ToString() == "开")
            {
                Water_img.Image = Properties.Resources.开关1__1_;
                Water_img.Tag = "关";
            }
            else
            {
                Water_img.Image = Properties.Resources.开关2;
                Water_img.Tag = "开";
            }
        }

        private void temperature_img_Click(object sender, EventArgs e)
        {
            if (temperature_img.Tag.ToString() == "开")
            {
                temperature_img.Image = Properties.Resources.开关1__1_;
                temperature_img.Tag = "关";
            }
            else
            {
                temperature_img.Image = Properties.Resources.开关2;
                temperature_img.Tag = "开";
            }
        }

        private void xbkz_img_Click(object sender, EventArgs e)
        {
            if (xbkz_img.Tag.ToString() == "开")
            {
                xbkz_img.Image = Properties.Resources.开关1__1_;
                xbkz_img.Tag = "关";
            }
            else
            {
                xbkz_img.Image = Properties.Resources.开关2;
                xbkz_img.Tag = "开";
            }
        }

        private void jxbjs_img_Click(object sender, EventArgs e)
        {
            if (jxbjs_img.Tag.ToString() == "开")
            {
                jxbjs_img.Image = Properties.Resources.开关1__1_;
                jxbjs_img.Tag = "关";
            }
            else
            {
                jxbjs_img.Image = Properties.Resources.开关2;
                jxbjs_img.Tag = "开";
            }
        }

        private void jxbcd_img_Click(object sender, EventArgs e)
        {
            if (jxbcd_img.Tag.ToString() == "开")
            {
                jxbcd_img.Image = Properties.Resources.开关1__1_;
                jxbcd_img.Tag = "关";
            }
            else
            {
                jxbcd_img.Image = Properties.Resources.开关2;
                jxbcd_img.Tag = "开";
            }
        }
         
     
        private void FCKZYMFrom_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / w;
            float newy = (this.Height) / h;
            setControls(newx, newy, this);


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
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fcdmbtn.Value == 30 || fcdmbtn.Value == 0)
            {
                fcdmvalue = fcdmbtn.Value;
                label42.Text = "停";
            }
            else
            {


                if (fcdmbtn.Value < fcdmvalue)//下降
                {
                    fcdmbtn.Value = fcdmbtn.Value - 1;
                }
                else//上升
                {
                    fcdmbtn.Value = fcdmbtn.Value + 1;
                }
            }
        }
        private void ProcessFrame(object sender, EventArgs e)
        {
            //if (capture != null && capture.Ptr != IntPtr.Zero)
            //{
            //    capture.Retrieve(frame, 0);
            //    FCSWGDSXT_pictureBox.Image = frame.ToBitmap();
            //}
        }

        private void FCSWGDSXT_pictureBox_Click(object sender, EventArgs e)
        {
            //capture.Start();
        }
        #region 方舱摄像头
        /// <summary>
        /// 方舱室内可旋转摄像头
        /// </summary>
       public void FCSNKXZSXT(string propath)
        {
            fcsnkxzsxt_app.AppFilename = propath + @"\\fcsxt1\\EasyPlayer.exe";
            fcsnkxzsxt_app.Start();
        }
        /// <summary>
        /// '方舱室内固定摄像头
        /// </summary>
        /// <param name="propath"></param>
       public void FCSNGDSXT(string propath)
        {

            fcsngdsxt_app.AppFilename = propath + @"\\fcsxt3\\EasyPlayer.exe";
            fcsngdsxt_app.Start();
        }
        /// <summary>
        /// 方舱外固定摄像头
        /// </summary>
        //加载OpenCVSharp4 nuget包进行数据转换
       public void FCSWGDSXT(string propath)
        {
            fcswgdsxt_app.AppFilename = propath + @"\\fcsxt2\\EasyPlayer.exe";
            fcswgdsxt_app.Start();
        } 
      
        #endregion
        private void FCKZYMFrom_FormClosed(object sender, FormClosedEventArgs e)
        {

            //capture.Stop();
           // mCameraControl.Stop();
            
        }

        private void fcdmbtn_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private async void YDKZK(string ioip, int ioport)
        {
            string ipaddress = ioip;
            int port = ioport;
            
            string[] hexValues0 = { "48", "3a", "01", "70", "03", "00", "00", "00", "45", "44" };//常开点断开
            string[] hexValues1 = { "48", "3a", "01", "70", "03", "01", "00", "00", "45", "44" };//常开点闭合            
            byte[] bytesDK = hexValues0.Select(s => Convert.ToByte(s, 16)).ToArray();
            byte[] bytesBH = hexValues1.Select(s => Convert.ToByte(s, 16)).ToArray();
            // 发送字节数据          
            // 确保TcpClient已连接
            if (tcpClient != null && tcpClient.Connected)
            {
                await homemethod.SendBytesAsync(networkStream, bytesBH);
                await homemethod.SendBytesAsync(networkStream, bytesDK);
                // 接收数据

                //D3.BackColor = Color.Red;
                //if (clickCount3 % 2 == 0)
                //{

                //    D3.BackColor = Color.Green;
                //}
            }
        }
        int fcdmvalue = 0;
        private void fcdmbtn_Click(object sender, EventArgs e)
        {
            if (fcdmbtn.Tag.ToString()=="停")
            {
                fcdmbtn.Tag = "开";
                if (fcdmbtn.Value < fcdmvalue)//下降
                {
                    label42.Text = "关";
                    JWFJ2("115.236.153.177", 13408);
                   
                }
                else//上升
                {
                    label42.Text = "开";
                    JWFJ1("115.236.153.177", 13408);
                }
                timer1.Start();
            }
            else
            {
                fcdmbtn.Tag = "停";
                label42.Text = "停";
                fcdmvalue = fcdmbtn.Value;
                YDKZK("115.236.153.177", 13408);
                timer1.Stop();
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ndxhllzl_lab_Click(object sender, EventArgs e)
        {

        }

        private void swgd_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fcswgdsxt_app_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void title_lab2_Click(object sender, EventArgs e)
        {

        }

        private void ndxcgzzl_lab_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void ndfcyszl_lab_Click(object sender, EventArgs e)
        {

        }

        private void ndfcydzl_lab_Click(object sender, EventArgs e)
        {

        }

        private void ypcc_lab_Click(object sender, EventArgs e)
        {

        }

        private void byfcrgkqcs_lab_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void fwqzt_lab_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void wbwd_lab_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void nbwd_lab_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void windspeed_lab_Click(object sender, EventArgs e)
        {

        }

        private void XCYPCC_LAB_Click(object sender, EventArgs e)
        {

        }

        private void snq_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fcsngdsxt_app_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void xcfwqzt_lab_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void snkxz_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void title_lab3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sb_ValueChanged(object sender, EventArgs e)
        {

        }

        private void eb_ValueChanged(object sender, EventArgs e)
        {

        }

        private void yb_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void jxbjzzyyd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void title_lab1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void fcsnkxzsxt_app_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
