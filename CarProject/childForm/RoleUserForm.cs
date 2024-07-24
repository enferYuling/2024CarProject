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

namespace CarProject.childForm
{
    public partial class RoleUserForm : Form
    {
        public readonly SqlSugarClient db;
        public int?  Roleid;
        public string  rolename;
        public RoleUserForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void RoleUserForm_Load(object sender, EventArgs e)
        {
            RoleName_lab.Text = rolename;
            LoadUser();
        }
        /// <summary>
        /// 加载用户
        /// </summary>
        public void LoadUser()
        {
            var list=this.db.Queryable<Base_Role_User,Base_User>((a,b)=>new JoinQueryInfos(JoinType.Inner, a.UserId == b.userid)).Where(a => a.RoleId == Roleid).Select((a, b) =>new
            {
                a.RoleId,
                a.UserRoleId,
                a.UserId,
                b.account,
                b.realname
            }).ToList();
            if (list.Count > 0)
            {
                 
                
                listBox.DataSource=list;
                listBox.DisplayMember = "account";
                listBox.ValueMember = "UserRoleId";
                 
            }
            else
            {
                listBox.DataSource=null;
            }
        }

        private void add_user_Click(object sender, EventArgs e)
        {
            UserSelectForm userSelectForm = new UserSelectForm(db);
            userSelectForm.isMiuSelect = true;
            if (userSelectForm.ShowDialog() == DialogResult.OK)
            {
                var list =new List<Base_Role_User>();
                var oldlist =this.db.Queryable<Base_Role_User>().Where(a=>a.RoleId==Roleid).ToList();
                foreach (var row in userSelectForm.SelectRows)
                {
                    var query = oldlist.Where(a => a.UserId == row.Cells["UserId"].Value.ToString()).ToList();
                    if(query.Count== 0)
                    {
                        Base_Role_User user = new Base_Role_User();
                        user.Create();
                        user.RoleId = Roleid;
                        user.UserId = row.Cells["UserId"].Value.ToString();
                        list.Add(user);
                    }
                   
                }
                if (list.Count > 0)
                {
                    var x = this.db.Storageable<Base_Role_User>(list).ToStorage();
                    x.AsInsertable.ExecuteCommand();//不存在插入
                    x.AsUpdateable.ExecuteCommand();//存在更新
                }
                LoadUser();
            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
    
            for (int i = 0; i < listBox.SelectedItems.Count; i++)
            {
                //因获取listbox 选中项目selecteditem是一个object 类型 ，需要强制转换，把selecteditem
                //转换成需要显示的类型，可获取valueMember的值
                   var user= (dynamic)listBox.SelectedItems[i];
                string UserRoleId = user.UserRoleId;
                this.db.Deleteable<Base_Role_User>().Where(a=>a.UserRoleId== UserRoleId).ExecuteCommand();
            }
            LoadUser();
        }
    }
}
