
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Bioinspired;
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


namespace CarProject.Home
{
    public partial class FCKZYMFrom : Form
    {
        //private VideoCapture capture;
        //private Mat frame=new Mat();

        int w;//定义当前窗体的宽度
        int h;//定义当前窗体的高度
        public readonly SqlSugarClient db;
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

        }

        private void FCKZYMFrom_Load(object sender, EventArgs e)
        {
            
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

        private void gate_img_Click(object sender, EventArgs e)
        {
            if (gate_img.Tag.ToString() == "开")
            {
                gate_img.Image = Properties.Resources.开关1__1_;
                gate_img.Tag = "关";
            }
            else
            {
                gate_img.Image = Properties.Resources.开关2;
                gate_img.Tag = "开";
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

        private void FCKZYMFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            //capture.Stop();
             
        }
    }
}
