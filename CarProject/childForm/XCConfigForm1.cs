using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HelixToolkit.Wpf.SharpDX.Model.Metadata;

namespace CarProject.childForm
{
    public partial class XCConfigForm1 : Form
    {
        public long carid;
        public readonly SqlSugarClient db;
        public XCConfigForm1(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void xzjgdt_btn_Click(object sender, EventArgs e)
        {
            ShowDT(false);
            label1.Visible = false;
            label2.Text = "正在下载";
            string serverUrl = "http://8.137.119.17:82/uploads/map.jpeg";
            string localFilePath = Directory.GetCurrentDirectory() + "\\uploads\\map.jpeg";
            string createpath = Directory.GetCurrentDirectory() + "\\uploads";
            if (!Directory.Exists(createpath))
            {
                Directory.CreateDirectory(createpath);
            }
            using (WebClient client = new WebClient())
            {
                try
                {

                    //client.DownloadFile(serverUrl, localFilePath);
                    //label2.Text = "下载完成";
                    //Image image = Image.FromFile(localFilePath);
                    //Size newSize = new Size(dt_box.Width, dt_box.Height);
                    //Bitmap resizedImage = new Bitmap(image, newSize);
                    //dt_box.Image = resizedImage;


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"下载失败：{ex.Message}");
                }
            }

        }

        private void schpt_btn_Click(object sender, EventArgs e)
        {
          
            
            string str_url = Application.StartupPath + "\\HPDT.html"; //地图的路径+名称
            Uri url = new Uri(str_url);
            string queryString = "?isdraw=false";
            webBrowser1.Navigate(new Uri(str_url + queryString));
            //webBrowser1.Url = url;
            //webBrowser1.ObjectForScripting = this;
        }
        /// <summary>
        /// 是否显示航拍图
        /// </summary>
        /// <param name="isshow"></param>

        public void ShowDT(bool isshow)
        {
            if (isshow)
            {
                tableLayoutPanel1.SetRow(panel1, 5);
                tableLayoutPanel1.SetRowSpan(panel1, 1);
                tableLayoutPanel1.SetColumn(panel1, 6);
                tableLayoutPanel1.SetColumnSpan(panel1, 1);
                panel1.Visible = false;

                tableLayoutPanel1.SetRow(webBrowser1, 1);
                tableLayoutPanel1.SetRowSpan(webBrowser1, 5);
                tableLayoutPanel1.SetColumn(webBrowser1, 0);
                tableLayoutPanel1.SetColumnSpan(webBrowser1, 5);
                webBrowser1.Visible = true;
            }
            else
            {
                tableLayoutPanel1.SetRow(webBrowser1, 0);
                tableLayoutPanel1.SetRowSpan(webBrowser1, 1);
                tableLayoutPanel1.SetColumn(webBrowser1, 0);
                tableLayoutPanel1.SetColumnSpan(webBrowser1, 1);
                webBrowser1.Visible = false;

                tableLayoutPanel1.SetRow(panel1, 1);
                tableLayoutPanel1.SetRowSpan(panel1, 5);
                tableLayoutPanel1.SetColumn(panel1, 0);
                tableLayoutPanel1.SetColumnSpan(panel1, 5);
                panel1.Visible = true;
            }
        }

        private void XCConfigForm1_Load(object sender, EventArgs e)
        {
            
        }
        //鼠标滚轮缩放图片的增量值
        private int ZoomStep = 20;

       
         
        

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }
    }
}
