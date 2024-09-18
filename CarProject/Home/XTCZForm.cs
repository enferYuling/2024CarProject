using HZH_Controls.Controls;
using SqlSugar;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.Home
{

    public partial class XTCZForm : Form
    {
        public readonly SqlSugarClient db;
        int w;//定义当前窗体的宽度
        int h;//定义当前窗体的高度
        public XTCZForm(SqlSugarClient datadb)
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
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        private void XTCZForm_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / w;
            float newy = (this.Height) / h;
            setControls(newx, newy, this);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            childForm.DataForm dataForm = new childForm.DataForm(db);
            titlePanel.Text = sjkcz_btn.Text;

            dataForm.TopLevel = false;
            this.FormPanel.Controls.Add(dataForm); // 将Form添加到Panel控件中
            dataForm.Parent = this.FormPanel;
            dataForm.Dock = DockStyle.Fill;
            dataForm.Show();
        }

        private void jsgl_btn_Click(object sender, EventArgs e)
        {
            childForm.RoleForm dataForm = new childForm.RoleForm(db);
            titlePanel.Text = jsgl_btn.Text;
            
            dataForm.TopLevel = false;
            this.FormPanel.Controls.Add(dataForm); // 将Form添加到Panel控件中
            dataForm.Parent = this.FormPanel;
            dataForm.Dock = DockStyle.Fill;
            dataForm.Show();
        }

        private void uiButton1_Click_1(object sender, EventArgs e)
        {
            childForm.RoleForm dataForm = new childForm.RoleForm(db);
            titlePanel.Text = jsgl_btn.Text;

            dataForm.TopLevel = false;
            this.FormPanel.Controls.Add(dataForm); // 将Form添加到Panel控件中
            dataForm.Parent = this.FormPanel;
            dataForm.Dock = DockStyle.Fill;
            dataForm.Show();
        }

        private void configbtn_Click(object sender, EventArgs e)
        {
            childForm.Cofig dataForm = new childForm.Cofig(db);
            titlePanel.Text = configbtn.Text;

            dataForm.TopLevel = false;
            this.FormPanel.Controls.Add(dataForm); // 将Form添加到Panel控件中
            dataForm.Parent = this.FormPanel;
            dataForm.Dock = DockStyle.Fill;
            dataForm.Show();
        }
    }
}
