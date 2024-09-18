using CarProject.Method;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.childForm
{
    public partial class XCDT_DTCK : Form
    {
        enum AnnotationTool
        {
            Rectangle,
            Circle,
            Line
        }
        List<object> annotations = new List<object>();
        AnnotationTool currentTool = AnnotationTool.Rectangle;
        public XCDT_DTCK()
        {
            InitializeComponent();
        }

        private async void lddt_btn_Click(object sender, EventArgs e)
        {

            label1.Visible = false;
            label1.Text = "正在下载";
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
                        label1.Text = "下载完成";

                    }
                    catch (Exception ex)
                    {
                        label1.Text = $"下载失败：{ex.Message}";
                    }
                }
            }
            string str_url = Application.StartupPath + "\\jglddt.html"; //地图的路径+名称

            Uri url = new Uri(str_url);
            
            
            webView21.Source = url; 

        }

        private async void XCDT_DTCK_Load(object sender, EventArgs e)
        {
            
        }

        private void hpdt_btn_Click(object sender, EventArgs e)
        {
            string str_url = Application.StartupPath + "\\HPDT.html"; //地图的路径+名称

            Uri url = new Uri(str_url);



            webView21.Source = url;
        }

     
        private async void rhdt_btn_Click(object sender, EventArgs e)
        {
           
          //  string str_url = Application.StartupPath + "\\loadjgld.html"; //地图的路径+名称
            string str_url = "http://8.137.119.17:82/API/loadjgdt"; //地图的路径+名称
           
            Uri url = new Uri(str_url);
            
            
           
            webView21.Source= url; 

        }
    }
}
