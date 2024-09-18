using CarProject.Common;
using CarProject.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.Login
{
    public partial class EditPwdForm : Form
    {
        public readonly SqlSugarClient db;
        public string account;
        public EditPwdForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void EditPwdForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(account))
            {
                label1.Visible = true;
                account_text.Visible = true;
                search_btn.Visible = true;

                label2.Visible = false;
                label3.Visible = false;
                pwd_text1.Visible = false;
                pwd_text2.Visible = false;
                srue_btn.Visible = false;
            }
            else
            {
                account_text.Text = account;
                account_text.Visible = false;
                label1.Visible = false;
                search_btn.Visible = false;

                label2.Visible = true;
                label3.Visible = true;
                pwd_text1.Visible = true;
                pwd_text2.Visible = true;
                srue_btn.Visible = true;
            }
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(account_text.Text))
            {
               ts_lab.Text="请输入账号";
                return;
            }
            var base_user=this.db.Queryable<Base_User>().Where(a=>a.account==account_text.Text).First();
            if (base_user == null)
            {
                ts_lab.Text="该账号不存在，请先注册";
                return;
            }
            account_text.Visible = false;
            label1.Visible = false;
            search_btn.Visible = false;

            label2.Visible = true;
            label3.Visible = true;
            pwd_text1.Visible = true;
            pwd_text2.Visible = true;
            srue_btn.Visible = true;
        }

        private void pwd_text1_TextChanged(object sender, EventArgs e)
        {
            pwd_text1.Text.Trim();
        }

        private void pwd_text2_TextChanged(object sender, EventArgs e)
        {
            var pwd1 = pwd_text1.Text;
            var pwd2 = pwd_text2.Text;
            if (pwd1 != pwd2)
            {
              
                ts_lab.Text = "两次密码不正确，请重新输入";
            }
            else if (string.IsNullOrEmpty(pwd2))
            {
              
                ts_lab.Text = "";
            }
            else
            {
                 
                ts_lab.Text = "";
            }
        }

        private void srue_btn_Click(object sender, EventArgs e)
        {
            var pwdtext1 = pwd_text1.Text;
            var pwdtext2 = pwd_text2.Text;

            if (pwdtext1 != pwdtext2)
            {
               
                ts_lab.Text = "两次密码不正确，请重新输入";
                return;
            }
            else
            {
                
                ts_lab.Text = "";
            }
            bool containsDigitAndLetter = Regex.IsMatch(pwd_text1.Text, @"^(?=.*[A-Za-z])(?=.*\d).+$");
            if (!containsDigitAndLetter)
            {
                ts_lab.Text = "密码需包含字母和数字";
                return;
            }
            var base_user = this.db.Queryable<Base_User>().Where(a => a.account == account_text.Text).First();
            var pwd = MD5Help.GetMD5Hash(pwd_text1.Text + "CarProject");
            base_user.password = pwd;
            var query=this.db.Queryable<Base_Userpwd>().Where(a=>a.userid==base_user.userid).First();
            query.password = pwd;
            query.oldpassword = pwd_text1.Text;
            try
            {
                this.db.Updateable<Base_User>(base_user).ExecuteCommand();
                this.db.Updateable<Base_Userpwd>(query).ExecuteCommand();
                MessageBox.Show("修改成功");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
    }
}
