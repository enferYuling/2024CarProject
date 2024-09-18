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
    public partial class YCCLGLDGLYCZForm : Form
    {
        public readonly SqlSugarClient db;
        public string account;
        public string realNmae;
        public string userid;
        public YCCLGLDGLYCZForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            search_account.Text= string.Empty;
            search_zt.SelectedIndex = -1;
            LoadData();
        }
        public void LoadData()
        {
            var exp = Expressionable.Create<Base_User>();
            exp.AndIF(!string.IsNullOrEmpty(search_account.Text), it => it.account == search_account.Text);//.OrIf 是条件成立才会拼接OR
            exp.AndIF( search_zt.SelectedIndex != -1, it => it.enabled == search_zt.SelectedIndex);//.OrIf 是条件成立才会拼接OR
            DataTable dt = this.db.Queryable<Base_User>().Where(exp.ToExpression()).ToDataTable();
            if (dt != null)
            {
                dt.Columns.Add("clglts", typeof(int));
                dt.Columns.Add("fcglts", typeof(int));
                foreach (DataRow dr in dt.Rows)
                {
                     var fccount = this.db.Queryable<Pro_sheltersConnect>().Where(a => a.userid == dr["UserId"].ToString()).GroupBy(a => a.sheltersid).Count();
                    dr["fcglts"] = fccount;
                }
                User_GridView.DataSource = dt;
                Bitmap statusImage = null;
                foreach (DataGridViewRow dr in User_GridView.Rows)
                {
                    var status = dr.Cells[8].Value.ToInt();
                    switch (status)
                    {
                        case 0:
                            statusImage = new Bitmap(imageList1.Images[0]);
                           
                            dr.Cells[9].Value = statusImage;
                            break;
                        case 1:
                            statusImage = new Bitmap(imageList1.Images[1]);
                            dr.Cells[9].Value = statusImage;
                            break;
                        
                    }
                }
            }
        }

        private void YCCLGLDGLYCZForm_Load(object sender, EventArgs e)
        {
            User_GridView.AutoGenerateColumns = false;
           var str=this.Tag.ToString().Split(',');
            account = str[0];
            realNmae = str[1];
            userid = str[2];
            
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            Login.RegisterForm registerForm = new Login.RegisterForm(db);
            registerForm.StartPosition = FormStartPosition.CenterParent;
            registerForm.title = "新建用户";
            registerForm.account = account ;
            registerForm.realName  = realNmae;
            registerForm.userid  = userid;
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
            
          
        }

        private void User_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell Cell = User_GridView.Rows[e.RowIndex].Cells["Column1"];

            if (Cell.Value.ToBool())
            {
                Cell.Value = false; 
            }
            else
            {
                Cell.Value = true;
            }
            // 判断用户点击的是按钮列
            if (e.ColumnIndex == 11 && e.RowIndex >= 0)//修改密码按钮点击
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = User_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = User_GridView.Rows[e.RowIndex];
                    var yhid = row.Cells["yhid"].Value.ToString();
                    if (string.IsNullOrEmpty(yhid))
                    {
                        MessageBox.Show("该行没有用户ID");
                        return;
                    }
                    Login.EditPwdForm editPwdForm = new Login.EditPwdForm(db);
                    editPwdForm.account = row.Cells["thzh"].Value.ToString();
                    editPwdForm.StartPosition = FormStartPosition.CenterParent; 
                    editPwdForm.ShowDialog();
                  

                }
            }
            else if (e.ColumnIndex == 12 && e.RowIndex >= 0)//删除按钮点击
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = User_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = User_GridView.Rows[e.RowIndex];
                    var yhid = row.Cells["yhid"].Value.ToString();
                    if (!string.IsNullOrEmpty(yhid))
                    {
                        if (yhid == userid)
                        {
                            MessageBox.Show("不能删除当前登录用户");
                            return;
                        }
                        this.db.Deleteable<Base_User>().Where(a=>a.userid==yhid).ExecuteCommand();
                        this.db.Deleteable<Base_Role_User>().Where(a=>a.UserId==yhid).ExecuteCommand();
                        this.db.Deleteable<Base_Userpwd>().Where(a=>a.userid==yhid).ExecuteCommand();
                        LoadData();
                    }
                }
            }
        }

        private void batch_btn_Click(object sender, EventArgs e)
        {
            var ids = User_GridView.Rows.Cast<DataGridViewRow>()
                              .Where(row => row.Cells["Column1"].Value.ToBool() == true)
                              .Select(a => a.Cells["yhid"].Value.ToString()).AsEnumerable()
                              .ToArray();
            
            this.db.Deleteable<Base_User>().Where(a => ids.Contains(a.userid)).ExecuteCommand();
            this.db.Deleteable<Base_Role_User>().Where(a => ids.Contains(a.UserId)).ExecuteCommand();
            this.db.Deleteable<Base_Userpwd>().Where(a => ids.Contains(a.userid)).ExecuteCommand();
            LoadData ();
        }
    }
}
