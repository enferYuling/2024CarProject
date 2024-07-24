using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using SqlSugar.Extensions;
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
    public partial class RoleForm : Form
    {
        public readonly SqlSugarClient db;
        public RoleForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            RoleAddForm roleAddForm = new RoleAddForm(db);
            roleAddForm.StartPosition = FormStartPosition.CenterParent;
            if (roleAddForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("保存成功");
                LoadData();
            }
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
          var dt=  this.db.Queryable<Base_Role>().WhereIF(!string.IsNullOrEmpty(search_rolename.Text),a=>a.RoleName.Contains(search_rolename.Text)).ToDataTable();
            if (dt != null)
            {
                 
                YC_GridView.DataSource = dt;
            }
            else
            {
                YC_GridView.DataSource = null;
            }
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            search_rolename.Text=string.Empty;
            LoadData();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            YC_GridView.AutoGenerateColumns = false;
            LoadData();
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
            if (e.ColumnIndex ==3 && e.RowIndex >= 0)//编辑
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = YC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = YC_GridView.Rows[e.RowIndex];
                    var jsid = row.Cells["RoleId"].Value.ToString();
                    if (string.IsNullOrEmpty(jsid))
                    {
                        MessageBox.Show("该行没有角色id");
                        return;
                    }
                    childForm.RoleAddForm roleAddForm = new childForm.RoleAddForm(db);
                    roleAddForm.RoleId = jsid.ToInt();
                    roleAddForm.StartPosition = FormStartPosition.CenterParent;

                    if (roleAddForm.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("保存成功");
                        LoadData();
                    }
                   
                }
            }
            else if (e.ColumnIndex == 4 && e.RowIndex >= 0)//用户关联
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = YC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = YC_GridView.Rows[e.RowIndex];
                    var jsid = row.Cells["RoleId"].Value.ToString();
                    var RoleName = row.Cells["RoleName"].Value.ToString();
                    if (string.IsNullOrEmpty(jsid))
                    {
                        MessageBox.Show("该行没有角色id");
                        return;
                    }
                    childForm.RoleUserForm roleuserform = new childForm.RoleUserForm(db);
                    roleuserform.Roleid = jsid.ToInt();
                    roleuserform.rolename = RoleName;
                    roleuserform.StartPosition = FormStartPosition.CenterParent;

                    if (roleuserform.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("关联成功");
                        LoadData();
                    }
                }
            }
            else if (e.ColumnIndex == 5 && e.RowIndex >= 0)//删除
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = YC_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = YC_GridView.Rows[e.RowIndex];
                    var jsid = row.Cells["RoleId"].Value.ToString();
                    if (!string.IsNullOrEmpty(jsid))
                    {
                        this.db.Deleteable<Base_Role>().Where(A=>A.RoleId== jsid.ToInt()).ExecuteCommand();
                        this.db.Deleteable<Base_Role_User>().Where(A=>A.RoleId== jsid.ToInt()).ExecuteCommand();
                        LoadData();
                    }
                }
            }
            #endregion
        }

        private void batch_btn_Click(object sender, EventArgs e)
        {
             
            var RoleIds= YC_GridView.Rows.Cast<DataGridViewRow>()
                                .Where(row => row.Cells["Column1"].Value.ToBool() == true)
                                .Select(a => a.Cells["RoleId"].Value.ToInt()).AsEnumerable()
                                .ToArray();
            this.db.Deleteable<Base_Role>().Where(A => RoleIds.Contains(A.RoleId)).ExecuteCommand();
            int?[] ids1 = Array.ConvertAll<int, int?>(RoleIds, delegate (int s) { return s.ObjToInt(); });
            this.db.Deleteable<Base_Role_User>().Where(A => ids1.Contains(A.RoleId)).ExecuteCommand();
            LoadData ();
        }
    }
}
