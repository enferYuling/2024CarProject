using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using Sunny.UI;
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
    public partial class YCCLGLDGLYRWForm : Form
    {
        public readonly SqlSugarClient db;
        public string account;
        public string realNmae;
        public string userid;
        public YCCLGLDGLYRWForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void YCCLGLDGLYRWForm_Load(object sender, EventArgs e)
        {
            var str = this.Tag.ToString().Split(',');
            account = str[0];
            realNmae = str[1];
            userid = str[2];
        }
        public void LoadData()
        {
            
            DataTable dt = this.db.Queryable<Pro_tasklist>()
                .InnerJoin<Base_User>((a,b)=>a.operatorid==b.userid)
                .InnerJoin<Pro_CarInfo>((a,b,c)=>a.workordercarid==c.carid)
                .WhereIF(!string.IsNullOrEmpty(search_account.Text),(a,b,c)=>b.account==search_account.Text)
                .WhereIF(!string.IsNullOrEmpty(CreateDate1.Text.Replace(" ","")),(a,b,c)=>a.CreateDate<= CreateDate1.Value)
                .WhereIF(!string.IsNullOrEmpty(CreateDate2.Text.Replace(" ","")),(a,b,c)=>a.CreateDate>= CreateDate2.Value)
                .Select((a, b,c) => new
                {
                    a.taskid,
                    a.CreateDate,
                    a.CreateUserId,
                    a.CreateUserName,
                    a.ModifyDate,
                    a.ModifyUserId,
                    a.ModifyUserName,
                    a.operatorid,
                    a.Enabled,
                    a.DeleteMark,
                    a.totalnumber,
                    a.finishnumber,
                    a.notfinishnumber,
                    b.account,
                    c.carcode
                })
                .ToDataTable();
            if (dt != null)
            {
                dt.Columns.Add("zxrwdsj", typeof(string));
               
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                    {
                        dr["zxrwdsj"] = dr["CreateDate"].ToDate().ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        dr["zxrwdsj"] = dr["ModifyDate"].ToDate().ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                Task_GridView.DataSource = dt;
                Bitmap statusImage = null;
                foreach (DataGridViewRow dr in Task_GridView.Rows)
                {
                    var status = dr.Cells[6].Value.ToInt();
                    switch (status)
                    {
                        case 0:
                            statusImage = new Bitmap(imageList1.Images[1]);

                            dr.Cells[7].Value = statusImage;
                            break;
                        case 1:
                            statusImage = new Bitmap(imageList1.Images[0]);
                            dr.Cells[7].Value = statusImage;
                            break;

                    }
                }
            }
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            search_account.Text=string.Empty;
            CreateDate1.CustomFormat = " ";
            CreateDate1.Format =DateTimePickerFormat.Custom;
            CreateDate2.CustomFormat = " ";
            CreateDate2.Format = DateTimePickerFormat.Custom;
            LoadData();
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

        private void add_btn_Click(object sender, EventArgs e)
        {
            childForm.TaskAddForm taskAddForm = new childForm.TaskAddForm(db);
            taskAddForm.account = account;
            taskAddForm.userid = userid;
            taskAddForm.StartPosition = FormStartPosition.CenterParent;
            if(taskAddForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void Task_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell Cell = Task_GridView.Rows[e.RowIndex].Cells["Column1"];

            if (Cell.Value.ToBool())
            {
                Cell.Value = false;

            }
            else
            {
                Cell.Value = true;

            }
        }
    }
}
