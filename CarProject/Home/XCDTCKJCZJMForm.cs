using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;

namespace CarProject.Home
{
    public partial class XCDTCKJCZJMForm : Form
    {
        public readonly SqlSugarClient db;
        int w;//定义当前窗体的宽度
        int h;//定义当前窗体的高度
        public XCDTCKJCZJMForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            //拿到屏幕的长和宽
            w = System.Windows.Forms.SystemInformation.VirtualScreen.Width;
            h = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
            this.db = datadb;
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

        private void XCDTCKJCZJMForm_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / w;
            float newy = (this.Height) / h;
            setControls(newx, newy, this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ds_btn.Tag.ToString() == "开")
            {
                ds_btn.ImageIndex = 1;
                ds_btn.Tag = "关";
            }
            else
            {
                ds_btn.ImageIndex = 2;
                ds_btn.Tag = "开";
            }
        }
        private void qtcl_btn_Click(object sender, EventArgs e)
        {
            if (qtcl_btn.Tag.ToString() == "开")
            {
                qtcl_btn.ImageIndex = 1;
                qtcl_btn.Tag = "关";
            }
            else
            {
                qtcl_btn.ImageIndex = 0;
                qtcl_btn.Tag = "开";
            }
        }

        private void sqx_btn_Click(object sender, EventArgs e)
        {
            if (sqx_btn.Tag.ToString() == "开")
            {
                sqx_btn.ImageIndex = 1;
                sqx_btn.Tag = "关";
            }
            else
            {
                sqx_btn.ImageIndex = 0;
                sqx_btn.Tag = "开";
            }
        }

        private void gs_btn_Click(object sender, EventArgs e)
        {
            if (gs_btn.Tag.ToString() == "开")
            {
                gs_btn.ImageIndex = 1;
                gs_btn.Tag = "关";
            }
            else
            {
                gs_btn.ImageIndex = 0;
                gs_btn.Tag = "开";
            }
        }

        private void xcq_btn_Click(object sender, EventArgs e)
        {
            if (xcq_btn.Tag.ToString() == "开")
            {
                xcq_btn.ImageIndex = 1;
                xcq_btn.Tag = "关";
            }
            else
            {
                xcq_btn.ImageIndex = 0;
                xcq_btn.Tag = "开";
            }
        }

        private void qzm_btn_Click(object sender, EventArgs e)
        {
            if (qzm_btn.Tag.ToString() == "开")
            {
                qzm_btn.ImageIndex = 1;
                qzm_btn.Tag = "关";
            }
            else
            {
                qzm_btn.ImageIndex = 0;
                qzm_btn.Tag = "开";
            }
        }

        private void hzm_btn_Click(object sender, EventArgs e)
        {
            if (hzm_btn.Tag.ToString() == "开")
            {
                hzm_btn.ImageIndex = 1;
                hzm_btn.Tag = "关";
            }
            else
            {
                hzm_btn.ImageIndex = 0;
                hzm_btn.Tag = "开";
            }
        }

        private void gzd_btn_Click(object sender, EventArgs e)
        {
            if (gzd_btn.Tag.ToString() == "开")
            {
                gzd_btn.ImageIndex = 1;
                gzd_btn.Tag = "关";
            }
            else
            {
                gzd_btn.ImageIndex = 0;
                gzd_btn.Tag = "开";
            }
        }

        private void jxb_btn_Click(object sender, EventArgs e)
        {
            if (jxb_btn.Tag.ToString() == "开")
            {
                jxb_btn.ImageIndex = 1;
                jxb_btn.Tag = "关";
            }
            else
            {
                jxb_btn.ImageIndex = 0;
                jxb_btn.Tag = "开";
            }
        }

        private void ygd_btn_Click(object sender, EventArgs e)
        {
            if (ygd_btn.Tag.ToString() == "开")
            {
                ygd_btn.ImageIndex = 1;
                ygd_btn.Tag = "关";
            }
            else
            {
                ygd_btn.ImageIndex = 0;
                ygd_btn.Tag = "开";
            }
        }

        private void ychh_btn_Click(object sender, EventArgs e)
        {
            if (ychh_btn.Tag.ToString() == "开")
            {
                ychh_btn.ImageIndex = 1;
                ychh_btn.Tag = "关";
            }
            else
            {
                ychh_btn.ImageIndex = 0;
                ychh_btn.Tag = "开";
            }
        }

        private void dcjw_btn_Click(object sender, EventArgs e)
        {
            if (dcjw_btn.Tag.ToString() == "开")
            {
                dcjw_btn.ImageIndex = 1;
                dcjw_btn.Tag = "关";
            }
            else
            {
                dcjw_btn.ImageIndex = 0;
                dcjw_btn.Tag = "开";
            }
        }

        private void zdph_btn_Click(object sender, EventArgs e)
        {
            if (zdph_btn.Tag.ToString() == "开")
            {
                zdph_btn.ImageIndex = 1;
                zdph_btn.Tag = "关";
            }
            else
            {
                zdph_btn.ImageIndex = 0;
                zdph_btn.Tag = "开";
            }
        }

        private void fcnzm_btn_Click(object sender, EventArgs e)
        {
            if (fcnzm_btn.Tag.ToString() == "开")
            {
                fcnzm_btn.ImageIndex = 1;
                fcnzm_btn.Tag = "关";
            }
            else
            {
                fcnzm_btn.ImageIndex = 0;
                fcnzm_btn.Tag = "开";
            }
        }

        private void fcdm_btn_Click(object sender, EventArgs e)
        {
            if (fcdm_btn.Tag.ToString() == "开")
            {
                fcdm_btn.ImageIndex = 1;
                fcdm_btn.Tag = "关";
            }
            else
            {
                fcdm_btn.ImageIndex = 0;
                fcdm_btn.Tag = "开";
            }
        }

        private void yjcbsb_btn_Click(object sender, EventArgs e)
        {
            if (yjcbsb_btn.Tag.ToString() == "开")
            {
                yjcbsb_btn.ImageIndex = 1;
                yjcbsb_btn.Tag = "关";
            }
            else
            {
                yjcbsb_btn.ImageIndex = 0;
                yjcbsb_btn.Tag = "开";
            }
        }

        private void ztwd_btn_Click(object sender, EventArgs e)
        {
            if (ztwd_btn.Tag.ToString() == "开")
            {
                ztwd_btn.ImageIndex = 1;
                ztwd_btn.Tag = "关";
            }
            else
            {
                ztwd_btn.ImageIndex = 0;
                ztwd_btn.Tag = "开";
            }
        }

        private void zsyg_btn_Click(object sender, EventArgs e)
        {
            if (zsyg_btn.Tag.ToString() == "开")
            {
                zsyg_btn.ImageIndex = 1;
                zsyg_btn.Tag = "关";
            }
            else
            {
                zsyg_btn.ImageIndex = 0;
                zsyg_btn.Tag = "开";
            }
        }

        private void dtck_btn_Click(object sender, EventArgs e)
        {
            childForm.XCDT_DTCK xCDT_DTCK=new childForm.XCDT_DTCK();
            xCDT_DTCK.StartPosition = FormStartPosition.CenterParent;
            // 显示模态对话框
            xCDT_DTCK.ShowDialog(this); // 将父窗体作为参数传递
        }
    }
}
