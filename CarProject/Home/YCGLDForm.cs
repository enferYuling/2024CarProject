using HZH_Controls.Controls;
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

namespace CarProject.Home
{
    public partial class YCGLDForm : Form
    {
        public readonly SqlSugarClient db;
        public string account;
        public string realName;
        public string UserId;
        int type;
        bool iszksysb=true;
        bool iszkrwd=true;
        Form childForm=new Form();


        public YCGLDForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void YCGLDForm_Load(object sender, EventArgs e)
        {
            var str =this.Tag.ToString().Split(',');
            account = str[0];
            realName = str[1];
            UserId = str[2];
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = false;
        }
 

        private void button1_Click(object sender, EventArgs e)
        {
            type = 0;
           
            YCtimer1.Start();
        }

        private void YCtimer1_Tick(object sender, EventArgs e)
        {


            if (iszksysb && type == 0)
            {
                sysb_panle.Height += 84;
                if (sysb_panle.Height == sysb_panle.MaximumSize.Height)
                {
                    iszksysb = false;
                    YCtimer1.Stop();
                }
            }
            else if (type == 0 && !iszksysb)
            {
                sysb_panle.Height -= 84;
                if (sysb_panle.Height == sysb_panle.MinimumSize.Height)
                {
                    iszksysb = true;
                    YCtimer1.Stop();
                }
            }
             if (iszkrwd && type == 2)
            {
                rwdgl_panle.Height += 84;
                if (rwdgl_panle.Height == rwdgl_panle.MaximumSize.Height)
                {
                    iszkrwd = false;
                    YCtimer1.Stop();
                }
            }
            else if (type == 2 && !iszkrwd)
            {
                rwdgl_panle.Height -= 84;
                if (rwdgl_panle.Height == rwdgl_panle.MinimumSize.Height)
                {
                    iszkrwd = true;
                    YCtimer1.Stop();
                }
            }
        }

        private void fcgl_btn_Click(object sender, EventArgs e)
        {
            childForm.Close();
            
            childForm = new YCCLGLDGLYFCForm(db);
            childForm.TopLevel = false;
            this.znr_panle.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.znr_panle;
            childForm.Dock = DockStyle.Fill;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Show();
        }

        private void clgl_btn_Click(object sender, EventArgs e)
        {
            childForm.Close();

            childForm = new YCCLGLDCLGLForm(db);
            childForm.TopLevel = false;
            this.znr_panle.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.znr_panle;
            childForm.Dock = DockStyle.Fill;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Show();
        }

      
        private void yhgl_btn_Click(object sender, EventArgs e)
        {
            childForm.Close();

            childForm = new YCCLGLDGLYCZForm(db);
            childForm.TopLevel = false;
            this.znr_panle.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.znr_panle;
            childForm.Dock = DockStyle.Fill;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Show();
        }

        private void twdgl_btn_Click(object sender, EventArgs e)
        {
            type = 2;
          
            YCtimer1.Start();
        }

        private void qbrwd_btn_Click(object sender, EventArgs e)
        {
            childForm.Close();

            childForm = new YCCLGLDGLYRWForm(db);
            childForm.TopLevel = false;
            this.znr_panle.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.znr_panle;
            childForm.Dock = DockStyle.Fill;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Show();
        }

        private void ycsbd_btn_Click(object sender, EventArgs e)
        {
            childForm.Close();

            childForm = new YCCLGLDGLYYCForm(db);
            childForm.TopLevel = false;
            this.znr_panle.Controls.Add(childForm); // 将Form添加到Panel控件中
            childForm.Parent = this.znr_panle;
            childForm.Dock = DockStyle.Fill;
            childForm.Tag = account + "," + realName + "," + UserId;
            childForm.Show();
        }
    }
}
