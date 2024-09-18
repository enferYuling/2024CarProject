using CarProject.Models;
using HZH_Controls;
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
using static QtCore.QMetaObject;

namespace CarProject.childForm
{
    public partial class Cofig : Form
    {
        public readonly SqlSugarClient db;
        public Cofig(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void Cofig_Load(object sender, EventArgs e)
        {
            ListGridView.AutoGenerateColumns = false;
            LoadData();
        }
        public void LoadData()
        {
           
            var dt=this.db.Queryable<Base_cofig>().ToDataTable();
            dt.Columns.Add("cofigTypename", typeof(string));
            dt.Columns.Add("rowindex", typeof(string));
            int i = 1;
            foreach (DataRow item in dt.Rows)
            {
                switch (item["cofigType"].ToInt())
                {
                    case 0:
                        item["cofigTypename"] = "进程";
                        break;
                    case 1:
                        item["cofigTypename"] = "文件";
                        break;
                    case 2:
                        item["cofigTypename"] = "图片";
                        break;
                        default:
                        break;
                }
                item["rowindex"]=i.ToString();
                i++;
            }
            ListGridView.DataSource = dt;
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            DataTable dt= ListGridView.DataSource as DataTable;
            List<Base_cofig> list = new List<Base_cofig>();
            foreach(DataRow row in dt.Rows)
            {
                Base_cofig cofig = new Base_cofig();
                cofig.cofigName = row["cofigName"].ToString();
                cofig.cofigid = row["cofigid"].ToString();
                switch (row["cofigTypename"].ToString())
                {
                    case "进程":
                        cofig.cofigType = 0;
                        break;
                    case "文件":
                        cofig.cofigType = 1;
                        break;
                    case "图片":
                        cofig.cofigType = 2;
                        break;
                }
              
                cofig.configAddress = row["configAddress"].ToString();


                if (string.IsNullOrEmpty(cofig.cofigid))
                {
                    cofig.Create();
                }
                list.Add(cofig);
            }
            if (list.Count > 0)
            {

            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            DataTable dt = ListGridView.DataSource as DataTable;
            if (dt == null)
            {
                dt=new DataTable();
                dt.Columns.Add("cofigid", typeof(string));
                dt.Columns.Add("cofigName", typeof(string));
                dt.Columns.Add("cofigType", typeof(int));
                dt.Columns.Add("configAddress", typeof(string));
                dt.Columns.Add("cofigTypename", typeof(string));
                dt.Columns.Add("rowindex", typeof(string));
            }
             DataRow row = dt.NewRow();
            row["cofigType"] = 0;
            row["cofigTypename"] = "进程";
            row["rowindex"] = (dt.Rows.Count+1).ToString();
            dt.Rows.Add(row);
            
            ListGridView.DataSource = dt;
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
           DataTable dt= ListGridView.DataSource as DataTable;
           List<string> list = new List<string>();
            foreach (DataGridViewRow row in ListGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["xz"].Value))
                {
                    list.Add(row.Cells["configid"].Value.ToString());
                }
            }
            this.db.Deleteable<Base_cofig>().Where(a => list.Contains(a.cofigid)).ExecuteCommand();
            LoadData();
        }

        private void ListGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dt = ListGridView.DataSource as DataTable;
            if (dt != null)
            {
                // 获取对应的单元格
                DataGridViewCell Cell = ListGridView.Rows[e.RowIndex].Cells["xz"];
                // 获取选中的行
                DataGridViewRow selectedRow = ListGridView.SelectedRows[0];
                // 获取行数据

                var query = selectedRow.DataBoundItem as DataRowView;
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
}
