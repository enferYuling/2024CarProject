using CarProject.Method;
using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using SqlSugar.Extensions;

namespace CarProject.Home
{
    public partial class YCCLGLDGLYFCForm : Form
    {
        int w;//定义当前窗体的宽度
        int h;//定义当前窗体的高度
        public readonly SqlSugarClient db;
        public YCCLGLDMethod method;
        public string account;
        public string realName;
       
 
        public YCCLGLDGLYFCForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            //拿到屏幕的长和宽
            w = System.Windows.Forms.SystemInformation.VirtualScreen.Width;
            h = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
            this.db = datadb;
            method=new YCCLGLDMethod(db);
            
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


        private void search_btn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle, Color.DeepSkyBlue, ButtonBorderStyle.Solid);
        }

        private void YCCLGLDGLYFCForm_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / w;
            float newy = (this.Height) / h;
            setControls(newx, newy, this);
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            childForm.GLYFCAddForm xCDT_DTCK = new childForm.GLYFCAddForm(db);
            xCDT_DTCK.StartPosition = FormStartPosition.CenterParent;
            xCDT_DTCK.realName = realName;
            // 显示模态对话框
            xCDT_DTCK.ShowDialog(this); // 将父窗体作为参数传递
            LoadData();
        }

        private void YCCLGLDGLYFCForm_Load(object sender, EventArgs e)
        {
            var str=this.Tag.ToString().Split(',');
            account=str[0];
            realName =str[1];
            FC_GridView.AutoGenerateColumns = false;
            
            
            
        }
        public void LoadData()
        {
            var dt = method.LoadFCData(search_fcbh.Text, search_zt.SelectedIndex);
            if (dt != null)
            {
                dt.Columns.Add("clgl",typeof(string));
                dt.Columns.Add("yhgl",typeof(string));
               
                foreach (DataRow dr in dt.Rows)
                {
                    dr["clgl"] = string.IsNullOrEmpty(dr["carid"].ToString()) ? "无" : "有";
                    dr["yhgl"] = string.IsNullOrEmpty(dr["operatorid"].ToString()) ? "无" : "有";
                    dr["yhgl"] = string.IsNullOrEmpty(dr["workorderclerkid"].ToString()) ? "无" : "有";
                    

                }
                FC_GridView.DataSource = dt;
                Bitmap statusImage = null;
                foreach (DataGridViewRow dr in FC_GridView.Rows)
                {
                    var status = dr.Cells[10].Value.ToInt();
                    switch (status)
                    {
                        case 0:
                            statusImage = new Bitmap(imageList1.Images[3]);
                            dr.Cells[11].Value = statusImage;
                            break;
                        case 1:
                            statusImage = new Bitmap(imageList1.Images[1]);
                            dr.Cells[11].Value = statusImage;
                            break;
                        case 2:
                            statusImage = new Bitmap(imageList1.Images[2]);
                            dr.Cells[11].Value = statusImage;
                            break;
                    }
                }
            }
        }
        
        private void FC_GridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void FC_GridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
             
        }

        private void FC_GridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
 
        }

        private void FC_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell Cell = FC_GridView.Rows[e.RowIndex].Cells["Column1"];

            if (Cell.Value.ToBool())
            {
                Cell.Value = false;

            }
            else
            {
                Cell.Value = true;

            }
            // 判断用户点击的是按钮列
            if (e.ColumnIndex == 13 && e.RowIndex >= 0)//配置按钮点击
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = FC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = FC_GridView.Rows[e.RowIndex];
                    var sheltersid = row.Cells["fcid"].Value.ToString();
                    if (string.IsNullOrEmpty(sheltersid))
                    {
                        MessageBox.Show("该行没有方舱id");
                        return;
                    }
                  
                   childForm.FCConfigForm fCConfigForm = new childForm.FCConfigForm(db);
                    fCConfigForm.StartPosition = FormStartPosition.CenterParent;
                    fCConfigForm.sheltersid = sheltersid.ToLong();
                    fCConfigForm.account = account;
                    fCConfigForm.realName = realName;
                  
                    fCConfigForm.ShowDialog();

                }
            }
            else if (e.ColumnIndex == 14 && e.RowIndex >= 0)//删除按钮点击
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = FC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = FC_GridView.Rows[e.RowIndex];
                    var sheltersid = row.Cells["fcid"].Value.ToString();
                    if (!string.IsNullOrEmpty(sheltersid))
                    {
                        Delete(sheltersid.ToLong());
                    }
                }
            }
        }
        /// <summary>
        /// 删除选中行
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long id)
        {
            if (id > 0)
            {
               var isok = this.db.Deleteable<Pro_sheltersInfo>().Where(a => a.sheltersid == id).ExecuteCommand();
                this.db.Deleteable<Pro_sheltersConnect>().Where(a => a.sheltersid == id).ExecuteCommand();
                this.db.Deleteable<Pro_sheltersFault>().Where(a => a.sheltersid == id).ExecuteCommand();
                LoadData();
            }
           
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            search_fcbh.Text = "";
            search_zt.SelectedIndex = -1;
            LoadData();
        }

        private void batch_btn_Click(object sender, EventArgs e)
        {
            var ids = FC_GridView.Rows.Cast<DataGridViewRow>()
                              .Where(row => row.Cells["Column1"].Value.ToBool() == true)
                              .Select(a => a.Cells["fcid"].Value.ToString()).AsEnumerable()
                              .ToArray();
            long?[] ids1 = Array.ConvertAll<string, long?>(ids, delegate (string s) { return s.ToLong(); });
            this.db.Deleteable<Pro_sheltersConnect>().Where(a=>ids1.Contains(a.sheltersid)).ExecuteCommand();
            this.db.Deleteable<Pro_sheltersFault>().Where(a=>ids1.Contains(a.sheltersid)).ExecuteCommand();
            this.db.Deleteable<Pro_sheltersInfo>().Where(a=>ids1.Contains(a.sheltersid)).ExecuteCommand();
            
            LoadData();
        }
    }
}
