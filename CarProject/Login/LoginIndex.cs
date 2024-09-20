using CarProject.Common;
using CarProject.Models;
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
using CarProject.Method;
using System.Security.Principal;


namespace CarProject.Login
{
    public partial class LoginIndex : Form
    {
        public readonly SqlSugarClient db;
        public LoginMethod loginMethod;
        public LoginIndex(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            
            var isok = loginMethod.Login(account_text.Text,pwd_text.Text,checkBox1.Checked,out string realName,out string userid);
            if (isok)
            {
                Home.Home from = new Home.Home(db);
                from.account=account_text.Text;
                from.realName=realName;
                from.UserId=userid;
                this.Hide();
                from.ShowDialog();
                account_text.Text = string.Empty;
                pwd_text.Text = string.Empty;
                checkBox1.Checked = false;
                this.Show();
            }

        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login.RegisterForm registerForm = new Login.RegisterForm(db);
            registerForm.ShowDialog();
            this.Show();
        }

        private void editpwd_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login.EditPwdForm editPwdForm = new Login.EditPwdForm(db);
            
            editPwdForm.ShowDialog();
            this.Show();
        }
 

        private void LoginIndex_Load(object sender, EventArgs e)
        {
            loginMethod = new LoginMethod(db);
        }

        private void LoginIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            var isok = loginMethod.Login(account_text.Text, pwd_text.Text, checkBox1.Checked, out string realName,out string userid);
            if (isok)
            {
                Home.Home from = new Home.Home(db);
                from.account = account_text.Text;
                from.realName = realName;
                this.Hide();
                from.ShowDialog();
                account_text.Text = string.Empty;
                pwd_text.Text = string.Empty;
                checkBox1.Checked = false;
                this.Show();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void pwdfind_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
