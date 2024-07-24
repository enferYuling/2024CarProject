 using CarProject.Common;
using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.Method
{
    public class LoginMethod
    {
        public readonly SqlSugarClient db;
        public LoginMethod(SqlSugarClient datadb)
        {
            this.db = datadb;
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="ischek">是否勾选</param>
        /// <returns></returns>
        public bool Login(string account, string password, bool ischek,out string realNmae,out string userid)
        {
            if (string.IsNullOrEmpty(account))
            {
                MessageBox.Show("请输入账号");
                realNmae = null;
                userid = null;
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码");
                realNmae = null;
                userid = null;
                return false;
            }
            if (!ischek)
            {
                MessageBox.Show("请勾选同意《用户远程操作协议》");
                realNmae = null;
                userid = null;
                return false;
            }
            var base_user = this.db.Queryable<Base_User>().Where(a => a.account == account).First();
            if (base_user == null)
            {
                MessageBox.Show("账号不存在，请先注册");
                realNmae = null;
                userid = null;
                return false;
            }


            var pwd = MD5Help.GetMD5Hash(password + "CarProject");
            if (pwd != base_user.password)
            {
                MessageBox.Show("密码错误，请重新输入");
                realNmae = null;
                userid = null;
                return false;
            }
            realNmae = base_user.realname;
            userid = base_user.userid;
            base_user.Loginnumber += 1;
            if (DateTime.Now.ToString("MM").ToInt() == base_user.loginmonth)
            {
                base_user.monthnumber += 1;
            }
            else
            {
                base_user.loginmonth = DateTime.Now.ToString("MM").ToInt();
                base_user.monthnumber =1;
            }
            base_user.logintime = DateTime.Now;
            this.db.Updateable(base_user).UpdateColumns(it => new {it.logintime,it.loginmonth,it.Loginnumber,it.monthnumber}).ExecuteCommand();
            return true;
        }
    }
}
