using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.Home
{
    public partial class YCCLGLDGLYYCForm : Form
    {
        private readonly SqlSugarClient db;
        public YCCLGLDGLYYCForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }
        public void LoadData()
        {

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            childForm.YCAddForm yCAddForm = new childForm.YCAddForm(db);
            yCAddForm.StartPosition = FormStartPosition.CenterParent;
           if(yCAddForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            search_account.Text = string.Empty;
            CreateDate1.CustomFormat = " ";
            CreateDate1.Format = DateTimePickerFormat.Custom;
            CreateDate2.CustomFormat = " ";
            CreateDate2.Format = DateTimePickerFormat.Custom;
        }

        private void CreateDate1_ValueChanged(object sender, EventArgs e)
        {
            if (CreateDate1.CustomFormat == " ")
            {
                CreateDate1.CustomFormat = "yyyy-MM-dd";
                CreateDate1.Format = DateTimePickerFormat.Custom;
            }
        }

        private void CreateDate2_ValueChanged(object sender, EventArgs e)
        {
            if (CreateDate2.CustomFormat == " ")
            {
                CreateDate2.CustomFormat = "yyyy-MM-dd";
                CreateDate2.Format = DateTimePickerFormat.Custom;
            }
        }

        private void YC_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 获取对应的单元格
            DataGridViewCell Cell = YC_GridView.Rows[e.RowIndex].Cells["Column1"];
          
            if (Cell.Value.ToBool())
            {
                Cell.Value = false;

            }
            else
            {
                Cell.Value = true;

            }
            #region 判断用户点击的是按钮列
            if (e.ColumnIndex == 10 && e.RowIndex >= 0)//查看工单
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = YC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = YC_GridView.Rows[e.RowIndex];
                    var ycdid = row.Cells["ycdid"].Value.ToString();
                    if (string.IsNullOrEmpty(ycdid))
                    {
                        MessageBox.Show("该行没有id");
                        return;
                    }
                    

                }
            }
             
            #endregion
        }
    }
}
