using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarProject.Common;
using CarProject.Models;
using SqlSugar;
using SqlSugar.DistributedSystem.Snowflake;

namespace CarProject.Login
{
    public partial class RegisterForm : Form
    {
        private readonly SqlSugarClient db;
        public string title;
        public string account;
        public string realName;
        public string userid;
        
        public RegisterForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void sruepwd_text_TextChanged(object sender, EventArgs e)
        {
            var pwdtext1=password_text.Text;
            var pwdtext2=sruepwd_text.Text;
            if (pwdtext1 != pwdtext2)
            {
                label5.Visible = true;
                tips_lab.Text = "两次密码不正确，请重新输入";
            }
            else if(string.IsNullOrEmpty(pwdtext2))
            {
                label5.Visible = false;
                tips_lab.Text = "";
            }
            else
            {
                label5.Visible = false;
                tips_lab.Text = "";
            }
        }

        private void password_text_TextChanged(object sender, EventArgs e)
        {
            password_text.Text.Trim();
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            var pwdtext1 = password_text.Text;
            var pwdtext2 = sruepwd_text.Text;
             
            if (pwdtext1 != pwdtext2)
            {
                label5.Visible = true;
                tips_lab.Text = "两次密码不正确，请重新输入";
                return;
            }
            else
            {
                label5.Visible = false ;
                tips_lab.Text = "";
            }
            Base_User base_User=this.db.Queryable<Base_User>().Where(a=>a.account==account_text.Text).First();
            if (base_User != null) {
                tips_lab.Text="该账号已存在";
                return;
            }
              base_User=  new Base_User();
            base_User.account = account_text.Text.Trim();
           base_User.realname=realName_text.Text;
           base_User.mobile=Mobile_text.Text;
           base_User.email=Email_text.Text;
            base_User.address=address_text.Text.Trim();
            base_User.issystem= issystemCheckBox.Checked?"1":"0";
             CodeHelp codeHelp = new CodeHelp(db);
          
            if (string.IsNullOrEmpty(base_User.account))
            {
                tips_lab.Text = "用户账号不能为空";
                return;
            }
            if (string.IsNullOrEmpty(password_text.Text))
            {
                tips_lab.Text = "用户密码不能为空";
                return;
            }
            if (string.IsNullOrEmpty(realName_text.Text))
            {
                tips_lab.Text = "真实姓名不能为空";
                return;
            }
            bool containsDigitAndLetter = Regex.IsMatch(password_text.Text, @"^(?=.*[A-Za-z])(?=.*\d).+$");
            if (!containsDigitAndLetter)
            {
                tips_lab.Text = "密码需包含字母和数字";
                return;
            }
            try
            {
                base_User.Create();
                base_User.createusername = realName_text.Text;
                if (!string.IsNullOrEmpty(account))
                {
                    base_User.createuserid = userid;
                    base_User.createusername= realName;
                }
                var pwd = MD5Help.GetMD5Hash(password_text.Text + "CarProject");
                base_User.password = pwd;
                string code = codeHelp.encoded(base_User.account, DateTime.Now, "base_User");
                base_User.usercode=code;
                this.db.Insertable<Base_User>(base_User).ExecuteCommand();
                Base_Userpwd base_Userpwd = new Base_Userpwd();
                base_Userpwd.Create();
                base_Userpwd.userid = base_User.userid;
                base_Userpwd.password = pwd;
                base_Userpwd.oldpassword = password_text.Text;
                base_User.logintime = DateTime.Now;
                this.db.Insertable<Base_Userpwd>(base_Userpwd).ExecuteCommand();
                //角色用户
                var roleid = this.db.Queryable<Base_Role>().Where(a => a.RoleName == js_text.Text).First().RoleId;
                Base_Role_User base_Role_User = new Base_Role_User();
                base_Role_User.Create();
                base_Role_User.RoleId = roleid;
                base_Role_User.UserId=base_User.userid;
                this.db.Insertable<Base_Role_User>(base_Role_User).ExecuteCommand();
                if (string.IsNullOrEmpty(account))
                {
                    MessageBox.Show("注册成功");
                    this.Hide();
                    Home.Home home = new Home.Home(db);
                    home.account = base_User.account;
                    home.realName = base_User.realname;
                    home.UserId = base_User.userid;
                    home.Show();
                }
                else
                {
                    MessageBox.Show("保存成功");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex) { 
                tips_lab.Text= ex.Message;
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(title))
            {
                label1.Text= title;
            }
            var query = this.db.Queryable<Base_Role>().Select(a=>a.RoleName).ToList();
            js_text.DataSource= query;
        }

        private void js_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
