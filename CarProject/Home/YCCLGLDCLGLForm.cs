using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.Home
{
    public partial class YCCLGLDCLGLForm : Form
    {
        public readonly SqlSugarClient db;
        public string account;
        public string realName;
        public string userid;
        public YCCLGLDCLGLForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void YCCLGLDCLGLForm_Load(object sender, EventArgs e)
        {
            XC_GridView.AutoGenerateColumns = false;
            var str = this.Tag.ToString().Split(',');
            account = str[0];
            realName = str[1];
            userid = str[2];
        }
        public void LoadData()
        {
            var exp = Expressionable.Create<Pro_CarInfo>();
            exp.AndIF(!string.IsNullOrEmpty(search_clbh.Text), it => it.carcode == search_clbh.Text);// 
            exp.AndIF(search_zt.SelectedIndex != -1, it => it.status == search_zt.SelectedIndex);//. 
            var dt=this.db.Queryable<Pro_CarInfo>().Where(exp.ToExpression()).ToDataTable();
            if (dt != null)
            {
                dt.Columns.Add("fcgl", typeof(string));
                dt.Columns.Add("yhgl", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["fcgl"] = dr["sheltersid"].ToString().ToLong()==0 ? "无" : "有";
                    dr["yhgl"] = string.IsNullOrEmpty(dr["operatorid"].ToString()) ? "无" : "有";
                    dr["yhgl"] = string.IsNullOrEmpty(dr["workorderclerkid"].ToString()) ? "无" : "有";
                }
                XC_GridView.DataSource = dt;
                Bitmap statusImage = null;
                foreach (DataGridViewRow dr in XC_GridView.Rows)
                {
                    var status = dr.Cells[10].Value.ToInt();//状态
                    switch (status)
                    {
                        case 0:
                            statusImage = new Bitmap(imageList1.Images[0]);

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
                        case 3:
                            statusImage = new Bitmap(imageList1.Images[3]);
                            dr.Cells[10].Value = statusImage;
                            break;

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


        private void XC_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 获取对应的单元格
            DataGridViewCell Cell = XC_GridView.Rows[e.RowIndex].Cells["Column1"];

            if (Cell.Value.ToBool())
            {
                Cell.Value = false;

            }
            else
            {
                Cell.Value = true;

            }
            // 判断用户点击的是按钮列
            if (e.ColumnIndex == 13 && e.RowIndex >= 0)//配置1
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = XC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = XC_GridView.Rows[e.RowIndex];
                    var carid = row.Cells["carid"].Value.ToString();
                    if (string.IsNullOrEmpty(carid))
                    {
                        MessageBox.Show("该行没有小车id");
                        return;
                    }
                    childForm.XCConfigForm xCConfigForm = new childForm.XCConfigForm(db);
                    xCConfigForm.carid = carid.ToLong();
                    xCConfigForm.StartPosition = FormStartPosition.CenterParent;
                    xCConfigForm.ShowDialog();
                }
            }
            else if (e.ColumnIndex == 14 && e.RowIndex >= 0)//删除按钮点击
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = XC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = XC_GridView.Rows[e.RowIndex];
                    var yhid = row.Cells["carid"].Value.ToString();
                    if (!string.IsNullOrEmpty(yhid))
                    {
                        
                        LoadData();
                    }
                }
            }else if (e.ColumnIndex == 15 && e.RowIndex >= 0)//配置2
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = XC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = XC_GridView.Rows[e.RowIndex];
                    var yhid = row.Cells["carid"].Value.ToString();
                    if (!string.IsNullOrEmpty(yhid))
                    {
                        childForm.XCConfigForm1 xCConfigForm = new childForm.XCConfigForm1(db);
                        xCConfigForm.carid = yhid.ToLong();
                        xCConfigForm.StartPosition = FormStartPosition.CenterParent;
                        xCConfigForm.ShowDialog();
                    }
                }
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            childForm.CLGLAddForm cLGLAddForm = new childForm.CLGLAddForm(db);
            cLGLAddForm.userid = userid;
            cLGLAddForm.account = account;
            cLGLAddForm.realName = realName;
            cLGLAddForm.StartPosition = FormStartPosition.CenterParent;
            if (cLGLAddForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            search_clbh.Text=string.Empty;
            search_zt.SelectedIndex = -1;
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            zxgzbj_lab.Text = "0台";
        }

        private void batch_btn_Click(object sender, EventArgs e)
        {
            var ids = XC_GridView.Rows.Cast<DataGridViewRow>()
                              .Where(row => row.Cells["Column1"].Value.ToBool() == true)
                              .Select(a => a.Cells["carid"].Value.ToString()).AsEnumerable()
                              .ToArray();
            long?[] ids1 = Array.ConvertAll<string, long?>(ids, delegate (string s) { return s.ToLong(); });
            this.db.Deleteable<Pro_carConnect>().Where(a => ids1.Contains(a.carid)).ExecuteCommand();
            this.db.Deleteable<Pro_carFault>().Where(a => ids1.Contains(a.carid)).ExecuteCommand();
            this.db.Deleteable<Pro_CarInfo>().Where(a => ids1.Contains(a.carid)).ExecuteCommand();

            LoadData();
        }
    }
}
