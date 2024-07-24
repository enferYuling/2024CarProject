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
    public partial class RoleAddForm : Form
    {
        public readonly SqlSugarClient db;
        public int? RoleId;
        public RoleAddForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void RoleAddForm_Load(object sender, EventArgs e)
        {
            if (RoleId != null)
            {
                var base_role=this.db.Queryable<Base_Role>().Where(a=>a.RoleId==RoleId).First();
                if (base_role != null)
                {
                    jsbh_text.Text = base_role.RoleCode;
                    jsmc_text.Text = base_role.RoleName;
                }
            }
        }

        private void sure_btn_Click(object sender, EventArgs e)
        {
            
           
            if (RoleId != null)
            {
              var  base_Role=this.db.Queryable<Base_Role>().Where(a=>a.RoleId==RoleId).First();
                if (string.IsNullOrEmpty(jsbh_text.Text))
                {
                    var count = this.db.Queryable<Base_Role>().Count();
                    base_Role.RoleCode = "JS_" + DateTime.Now.ToString("yyyyMMdd") + (count + 1).ToString();
                }
                else
                {
                    base_Role.RoleCode=jsbh_text.Text;
                }
                base_Role.RoleName = jsmc_text.Text;
                this.db.Updateable(base_Role).ExecuteCommand();
            }
            else
            {
                Base_Role base_Role = new Base_Role();
                if (string.IsNullOrEmpty(jsbh_text.Text))
                {
                    var count = this.db.Queryable<Base_Role>().Count();
                    base_Role.RoleCode = "JS_" + DateTime.Now.ToString("yyyyMMdd") + (count + 1).ToString();
                }
                else
                {
                    base_Role.RoleCode = jsbh_text.Text;
                }
                base_Role.RoleName = jsmc_text.Text;
               var keyVule= this.db.Insertable<Base_Role>(base_Role).ExecuteReturnIdentity();
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
