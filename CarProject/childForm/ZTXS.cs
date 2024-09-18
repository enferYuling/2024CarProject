using CarProject.Method;
using HZH_Controls;
using QtCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.childForm
{
    public partial class ZTXS : Form
    {
        public ZTXS()
        {
            InitializeComponent();
            InitializeSerialPort();
            
        }
      
        static Mutex mutex = new Mutex();
        private void ZTXS_Load(object sender, EventArgs e)
        {
            OpenPort();
            imgori = Hori_Line();
             Hori_Disp(DbPitchAngle, DbRowAngle);
             
           timer1.Start();
        }
       
        #region 姿态
        private float roll = 0; // 横滚角
        private float pitch = 0; // 俯仰角
                                 // 声明 PrintWindow 函数

        private ArtificialHorizon artificialHorizon;
        public Image imgtmp;
        public Image imgori;
        public Bitmap bitmp;
        public Bitmap bm;
        public bool IsSure = false;

      public  double DbPitchAngle = 0; //俯仰角度[-90,90]
        public double DbRowAngle =0;   //滚转角度[-180,180]
       
                                             //重绘旋转后的仪表盘图片 
        public void Overlap(Bitmap btm, int x, int y, int w, int h)
        {
            Bitmap image = new Bitmap(btm);
            Bitmap hi = new Bitmap(bm);
            Graphics g = Graphics.FromImage(hi);
            g.DrawImage(image, new Rectangle(x, y, w, h));
            bm = hi;
        }



        //-------------------地平仪刻度划线函数-------------------//
        //入口参数：无       
        public Image Hori_Line()
        {
            bitmp = new Bitmap(HoriBox.Width, HoriBox.Height);
            System.Drawing.Graphics gscale = System.Drawing.Graphics.FromImage(bitmp);
            System.Drawing.Pen p1 = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
            System.Drawing.Pen p2 = new System.Drawing.Pen(System.Drawing.Color.Green, 2);

            //准心绘线

            var q2 = (350 - HoriBox.Width) / 2;
            var q1 = 350 - HoriBox.Width;
            gscale.DrawEllipse(Pens.Red, 165 - q2, 165 - q2, 20, 20);

            gscale.DrawLine(p2, 170 - q2, 175 - q2, 185 - q2, 175 - q2);
            gscale.DrawLine(p1, 170 - q2, 185 - q2, 177 - q2, 175 - q2);
            gscale.DrawLine(p1, 185 - q2, 185 - q2, 177 - q2, 175 - q2);
            //滚转刻度线
            gscale.DrawEllipse(Pens.White, 35, 35, 280 - q1, 280 - q1);
            int i, i1, j, j1, k;
            for (k = 0; k < 73; k++)
            {
                i = Convert.ToInt32((130 - q2) * Math.Cos(k * Math.PI / 36) + (175 - q2));
                j = Convert.ToInt32((130 - q2) * Math.Sin(k * Math.PI / 36) + (175 - q2));
                if (k % 2 == 0)
                {
                    i1 = Convert.ToInt32((155 - q2) * Math.Cos(k * Math.PI / 36) + (175 - q2));
                    j1 = Convert.ToInt32((155 - q2) * Math.Sin(k * Math.PI / 36) + (175 - q2));
                }
                else
                {
                    i1 = Convert.ToInt32((150 - q2) * Math.Cos(k * Math.PI / 36) + (175 - q2));
                    j1 = Convert.ToInt32((150 - q2) * Math.Sin(k * Math.PI / 36) + (175 - q2));
                }
                gscale.DrawLine(Pens.White, i, j, i1, j1);
            }
            gscale.Dispose();
            return bitmp;
        }

        /// <summary>
        //-------------------地平仪显示函数-------------------//
        //入口参数：
        //俯仰角 pitch_angle 范围-90~90 度 
        //滚动角 row_angle   范围-90~90 度
        /// </summary>
        /// <param name="pitch_angle"></param>
        /// <param name="row_angle"></param>
        public void Hori_Disp(double pitch_angle, double row_angle)
        {
            //1地平仪图像载入带平移
            int pic_position;
            double q1 = (HoriBox.Width.ToDouble() / 90);
            pic_position = Convert.ToInt32(pitch_angle * q1);

            try
            {
                imgtmp = new Bitmap(Properties.Resources.刻度1);
                row_angle = row_angle % 360;
                //弧度转换  
                double ar = 2;
                double radian = (row_angle - 90) * Math.PI / 180.0;
                double radiana = (row_angle - 90 - ar) * Math.PI / 180.0;
                double radianc = (row_angle - 90 + ar) * Math.PI / 180.0;
                double cos = Math.Cos(radian);
                double cosa = Math.Cos(radiana);
                double cosc = Math.Cos(radianc);
                double sin = Math.Sin(radian);
                double sina = Math.Sin(radiana);
                double sinc = Math.Sin(radianc);
                //目标位图
                Bitmap dsImage = new Bitmap(HoriBox.Width, HoriBox.Height);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //计算偏移量
                int q2 = (350 - HoriBox.Width) / 2;
                int q3 = (350 - HoriBox.Width) * 2;
                Rectangle rect = new Rectangle(-175 + q2, -175 + q2 + pic_position, 700 - q3, 700 - q3);
                g.TranslateTransform(175 - q2, 175 - q2);
                g.RotateTransform((float)row_angle);
                //恢复图像在水平和垂直方向的平移 
                g.TranslateTransform(-175 + q2, -175 + q2);
                g.DrawImage(imgtmp, rect);
                //重至绘图的所有变换  
                g.ResetTransform();
                g.Dispose();
                //保存旋转后的图片
                bm = dsImage;
                //调用imgori 已经是画好的刻度盘
                Bitmap bitmp = new Bitmap(imgori);
                Overlap(bitmp, 0, 0, HoriBox.Width, HoriBox.Width);
                Bitmap pointImage = new Bitmap(HoriBox.Width, HoriBox.Height);
                // 指针设计 ;
                System.Drawing.Graphics gPoint = System.Drawing.Graphics.FromImage(pointImage);
                SolidBrush h = new SolidBrush(System.Drawing.Color.Red);
 

                Point a = new Point(Convert.ToInt32(175 - q2 + (131 - q2) * cosa), Convert.ToInt32(180 - q2 + (131 - q2) * sina));
                Point b = new Point(Convert.ToInt32((141 - q2) * cos + (175 - q2)), Convert.ToInt32((141 - q2) * sin + (175 - q2)));
                Point c = new Point(Convert.ToInt32(175 - q2 + (131 - q2) * cosc), Convert.ToInt32(180 - q2 + (131 - q2) * sinc));

                Point[] pointer = { a, b, c };
                gPoint.FillPolygon(h, pointer);



                Overlap(pointImage, 0, 0, HoriBox.Width, HoriBox.Height);
                HoriBox.Image = bm;
                g.Dispose();
                imgtmp.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.FillMode = 0;
                gp.AddEllipse(new Rectangle(0, 0, HoriBox.Width, HoriBox.Height));
                HoriBox.Region = new Region(gp);
                gp.Dispose();
           
           
        }






        #endregion
        #region 车辆姿态

        private Queue<byte> buffer = new Queue<byte>(); // 用于存储接收到的字节      
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
        public void AddBytes(byte[] newBytes)
        {

            foreach (byte b in newBytes)
            {
                buffer.Enqueue(b);
            }
        }
        private int groupSize = 13; // 分组大小
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

                        DbRowAngle = JDX1;
                        
                    }
                    else
                    {
                        double JDX = combined1;
                        double JDX1 = JDX / 32768 * 180;


                        DbRowAngle = JDX1;
                        
                    }
                    if ((group[5] & 0x80) != 0)
                    {
                        double JDY = ~combined2;
                        double JDY1 = (-JDY) / 32768 * 180;
                       
                            DbPitchAngle = JDY1;
                          
                    }
                    else
                    {
                        double JDY = combined2;
                        double JDY1 = JDY / 32768 * 180;
                         DbPitchAngle = JDY1;
                           
                    }
                    // 使用 Invoke 方法在控件所属线程中更新控件
                   
                   
                //   Hori_Disp(DbPitchAngle, DbRowAngle);
                // Hori_Line();
                // 创建并启动一个新线程

                }

            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Hori_Disp(DbPitchAngle, DbRowAngle);
            Hori_Line();
            label2.Text=DbPitchAngle.ToString();
            label1.Text= DbRowAngle.ToString();
            
        }
    }
}
