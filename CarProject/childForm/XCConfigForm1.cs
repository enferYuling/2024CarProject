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

            
            label2.Text = "正在下载";
            string serverUrl = "http://8.137.119.17:82/uploads/map.jpeg";
            string localFilePath = Directory.GetCurrentDirectory() + "\\uploads\\map.jpeg";
            string createpath = Directory.GetCurrentDirectory() + "\\uploads";
            if (!Directory.Exists(createpath))
            {
                Directory.CreateDirectory(createpath);
            }
            if (!File.Exists(localFilePath))
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {

                        client.DownloadFile(serverUrl, localFilePath);
                        label2.Text = "下载完成";

                    }
                    catch (Exception ex)
                    {
                        label2.Text = $"下载失败：{ex.Message}";
                    }
                }
            }
            string str_url = Application.StartupPath + "\\jglddt.html"; //地图的路径+名称

            Uri url = new Uri(str_url);


            webView21.Source = url;


        }

        private void schpt_btn_Click(object sender, EventArgs e)
        {
            string str_url = Application.StartupPath + "\\HPDT.html"; //地图的路径+名称 
            Uri url = new Uri(str_url); 
            webView21.Source = url;
        }
        /// <summary>
        /// 是否显示航拍图
        /// </summary>
        /// <param name="isshow"></param>

        public void ShowDT(bool isshow)
        {
             
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
