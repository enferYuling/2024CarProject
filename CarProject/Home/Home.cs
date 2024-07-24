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


        public Home(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb; 
        }

        private void Home_Load(object sender, EventArgs e)
        {
            InitializeCounters();
            homemethod = new HomeMethod(db);
           IPAddress= homemethod.GetLocalIPAddress();//获取IP地址
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
          
            UpdateLabel();
            stopwatch.Start();
            caozuo_timer2.Start();

          
        }

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
                double bytesSent = bytesSentCounter.RawValue / 1048576; // Convert to MB/s
                double bytesReceived = bytesReceivedCounter.RawValue / 1048576; // Convert to MB/s
                double totalSpeed = bytesSent + bytesReceived;
                labelSpeed.Text = $"{totalSpeed:F2} MB/s";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void caozuo_timer2_Tick(object sender, EventArgs e)
        {
            elapsedTime = stopwatch.Elapsed;
            UpdateLabel();

           
        }
        private void UpdateLabel()
        {
            operate_lab.Text = $"操作时长: {elapsedTime:hh\\时mm\\分}";
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopwatch.Reset();
            caozuo_timer2.Stop();
            UpdateLabel();
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
            childForm.Show();
        }

 
        private void XCCZYM_btn_Click(object sender, EventArgs e)
        { 
            childForm.Close();
            ucPanelTitle1.Title = XCCZYM_btn.Text;
            
            childForm = new XCCZYM(db);
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
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        

        private void xtcz_btn_Click(object sender, EventArgs e)
        {
         //   ucPanelTitle1.Controls.Clear();
            ucPanelTitle1.Title = xtcz_btn.Text;
            childForm.Close();
            childForm = new XTCZForm(db);
            childForm.TopLevel = false;
            childForm.Tag = account + "," + realName + "," + UserId;
            this.ucPanelTitle1.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.ucPanelTitle1;
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
    }
}
