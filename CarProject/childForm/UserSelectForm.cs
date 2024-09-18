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

namespace CarProject.childForm
{
   
    public partial class UserSelectForm : Form
    {
        public readonly SqlSugarClient db;
        public string RoleName="";
        public DataRow SelectDataRow=null;
        public List<DataGridViewRow> SelectRows = null;
        /// <summary>
        /// 是否多选
        /// </summary>
        public bool isMiuSelect = false;
        public UserSelectForm(SqlSugarClient datadb)
        {
            InitializeComponent();

            this.db = datadb;
        }

        private void UserSelectForm_Load(object sender, EventArgs e)
        {
            User_GridView.AutoGenerateColumns = false;
            if (isMiuSelect)
            {
                User_GridView.MultiSelect = true;
                User_GridView.Columns["Column1"].Visible = true;
            }
            else
            {
                User_GridView.MultiSelect = false;
                User_GridView.Columns["Column1"].Visible = false;
            }
            LoadData();
        }
        public void LoadData()
        {
            DataTable dt = new DataTable();
            
            try
            {
                if (!string.IsNullOrEmpty(RoleName))
                {
                    dt = this.db.Queryable<Base_User>().LeftJoin<Base_Role_User>((a, b) => a.userid == b.UserId)
                        .LeftJoin<Base_Role>((a, b, c) => b.RoleId == c.RoleId)
                        .Where((a, b, c) => a.enabled == 1).WhereIF(!string.IsNullOrEmpty(RoleName), (a, b, c) => c.RoleName == RoleName)
                        .ToDataTable();
                }
                else
                {
                    dt=this.db.Queryable<Base_User>().Where(a=>a.enabled==1).ToDataTable();
                }
                if (dt != null)
                {
                    User_GridView.DataSource = dt;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void srue_btn_Click(object sender, EventArgs e)
        {
            if (!isMiuSelect)
            {
                // 获取选中的行
                DataGridViewRow selectedRow = User_GridView.SelectedRows[0];
                // 获取行数据

                var query = selectedRow.DataBoundItem as DataRowView;
                SelectDataRow = query.Row;
                DialogResult = DialogResult.OK;
            }
            else
            {
                // 使用LINQ查询获取满足条件的行
                SelectRows = User_GridView.Rows.Cast<DataGridViewRow>()
                                    .Where(row => row.Cells["Column1"].Value.ToBool() == true)
                                    .ToList();
                DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void User_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isMiuSelect && e.RowIndex >= 0)
            {
                // 获取对应的单元格
                DataGridViewCell Cell = User_GridView.Rows[e.RowIndex].Cells["Column1"];
                // 获取选中的行
                //    DataGridViewRow selectedRow = YC_GridView.SelectedRows[0];
                // 获取行数据

                //  var query = selectedRow.DataBoundItem as DataRowView;
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
