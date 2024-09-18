using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.childForm
{
    public partial class TaskAddForm : Form
    {
        public readonly SqlSugarClient db;
        public long? taskid;
        public string account;
        public string userid;
        
        public TaskAddForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void rwclbh_text_Click(object sender, EventArgs e)
        {
            CarSelectForm carSelectForm=new CarSelectForm(db);
            carSelectForm.StartPosition = FormStartPosition.CenterParent;
            if(carSelectForm.ShowDialog() == DialogResult.OK)
            {
                if (carSelectForm.SelectDataRow != null)
                {
                    rwclbh_text.Text = carSelectForm.SelectDataRow["carcode"].ToString();
                    rwclbh_text.Tag = carSelectForm.SelectDataRow["carid"].ToString();
                    address_lab.Text = carSelectForm.SelectDataRow["companyname"].ToString();
                }
            }
        }

        private void czyzh_text_Click(object sender, EventArgs e)
        {
            UserSelectForm userSelectForm=new UserSelectForm(db);
            userSelectForm.RoleName = "操作员";
            userSelectForm.StartPosition=FormStartPosition.CenterParent;
            if(userSelectForm.ShowDialog() == DialogResult.OK)
            {
                if (userSelectForm.SelectDataRow != null)
                {
                    czyzh_text.Text = userSelectForm.SelectDataRow["Account"].ToString();
                    czyzh_text.Tag = userSelectForm.SelectDataRow["UserId"].ToString();
                }
            }
        }

        private void workadd_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rwclbh_text.Text))
            {
                MessageBox.Show("请先选择车辆");
                return;
            }
            if (string.IsNullOrEmpty(czyzh_text.Text))
            {
                MessageBox.Show("请先选择操作员");
                return;
            }
            var dt= WorkGridView.DataSource as DataTable ;
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("CreateDate", typeof(string));
                dt.Columns.Add("releaseaccount", typeof(string));
                dt.Columns.Add("releaseid", typeof(string));
                dt.Columns.Add("carid", typeof(string));
                dt.Columns.Add("carcode", typeof(string));
                dt.Columns.Add("enddate", typeof(string));
                dt.Columns.Add("operatorid", typeof(string));
                dt.Columns.Add("termmouth", typeof(int));
                dt.Columns.Add("taskworkorderid", typeof(string));
                dt.Columns.Add("taskid", typeof(string));
                dt.Columns.Add("workstate", typeof(string));
                dt.Columns.Add("CreateUserId", typeof(string));
                dt.Columns.Add("CreateUserName", typeof(string));
                dt.Columns.Add("qx", typeof(bool));
          
            }
            try
            {
                var dr = dt.NewRow();
                dr["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["releaseaccount"] = account;
                dr["releaseid"] = userid;
                dr["carid"] = rwclbh_text.Tag.ToString();
                dr["carcode"] = rwclbh_text.Text;
                dr["enddate"] = "";
                dr["operatorid"] = czyzh_text.Tag.ToString();
                dr["termmouth"] = 0;
                dr["taskworkorderid"] = "";
                dr["taskid"] = taskid;
                dr["workstate"] = 0;
                dr["CreateUserId"] =userid;
                dr["CreateUserName"] = account;
                dr["qx"] = false;
                dt.Rows.Add(dr);
                WorkGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deletework_btn_Click(object sender, EventArgs e)
        {
            
          
        }
        /// <summary>
        /// 加载工作单数据
        /// </summary>
        public void LoadWorkData()
        {
            var dt=this.db.Queryable<Pro_taskWorkorder>().Where(a=>a.taskid==taskid).ToList();
            if (dt.Count > 0) 
            {
            WorkGridView.DataSource = dt;
            }
        }
        /// <summary>
        /// 加载工作明细数据
        /// </summary>
        public void LoadWorkDetailData(string workids)
        {
            var ids=workids.Split(',');
           // var dt=this.db.Queryable<Pro_taskWorkorderDetail>().Where(a=>ids.Contains(a.taskworkorderid)).ToList();
        }

        private void TaskAddForm_Load(object sender, EventArgs e)
        {
            WorkGridView.AutoGenerateColumns = false;
            
        }

        private void workdetailadd_Click(object sender, EventArgs e)
        {
            TextForm textForm = new TextForm();
            textForm.StartPosition = FormStartPosition.CenterParent;
            textForm.ShowDialog();
            var hx=textForm.hx.ToInt();
            var zx=textForm.zx.ToInt();
            // string[] hx_text = { "A", "B", "C", "D", "E", "F", "G", "H", "L", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "", "", "", "", "", "", "" };
            var dt=new DataTable();
            for (int i = 0; i < hx+1; i++)
            {
                if (i == 0)
                {
                    dt.Columns.Add("序号", typeof(int));
                }
                else
                {
                    var ColumnsNmae = NumberToLetters(i);
                    dt.Columns.Add(ColumnsNmae, typeof(string));
                }
               
            }
            for (int i = 1;i < zx+1; i++)
            {
                var dr=dt.NewRow();
                dr["序号"] = i;
                dt.Rows.Add(dr);
            }
            detailGridView.DataSource = dt;
        }
        public static string NumberToLetters(int number)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException("Number must be greater than zero.");

            char startChar = 'A'; // 起始字符
            StringBuilder letters = new StringBuilder();

            while (number > 0)
            {
                letters.Insert(0, (char)(startChar + (number - 1) % 26));
                number = (number - 1) / 26;
            }

            return letters.ToString();
        }

        private void detailGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void WorkGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 获取对应的单元格
            DataGridViewCell Cell = WorkGridView.Rows[e.RowIndex].Cells["qx"];
            // 获取选中的行
            DataGridViewRow selectedRow = WorkGridView.SelectedRows[0];
            // 获取行数据

            var query = selectedRow.DataBoundItem as DataRowView;
            if (Cell.Value.ToBool())
            {
                Cell.Value = false;
                query.Row["qx"] = false;
            }
            else
            {
                Cell.Value = true;
                query.Row["qx"] = true;
            }
        }

        private void red_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
