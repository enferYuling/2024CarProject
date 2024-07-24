using CarProject.Models;
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

namespace CarProject.childForm
{
    public partial class CLGLAddForm : Form
    {
        public readonly SqlSugarClient db;
        public string  realName;//账号
        public string  userid;//账号
        public string  account;//账号
        public CLGLAddForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void sure_btn_Click(object sender, EventArgs e)
        {
            try
            {


                if (CheckNull())
                {
                    Pro_CarInfo pro_CarInfo = new Pro_CarInfo();
                    if (!string.IsNullOrEmpty(xcbh_text.Text))
                    {
                        var query = this.db.Queryable<Pro_CarInfo>().Where(a => a.carcode == xcbh_text.Text).First();
                        if (query != null)
                        {
                            MessageBox.Show("该编号小车已存在");
                            return;
                        }
                        pro_CarInfo.carcode = xcbh_text.Text;
                    }
                    else
                    {
                        var count = this.db.Queryable<Pro_CarInfo>().Where(a => a.CreateDate == DateTime.Now).Count();
                        pro_CarInfo.carcode = "XC_" + DateTime.Now.ToString("yyyyMMdd") + (count + 1).ToString();
                    }
                    pro_CarInfo.Create();

                    pro_CarInfo.CreateUserId = userid;
                    pro_CarInfo.CreateUserName = realName;
                    pro_CarInfo.status = xcljzt_comboBox.SelectedIndex;
                    pro_CarInfo.serverstatus = xcfwqzt_checkBox.Checked ? 1 : 0;
                    pro_CarInfo.harddiskstorage = ypcc_text.Text;
                    pro_CarInfo.sheltersid = ljfc_text.Tag == null ? 0 : ljfc_text.Tag.ToString().ToLong();
                    pro_CarInfo.workorderclerkid = ljgdy_text.Tag == null ? "" : ljgdy_text.Tag.ToString();
                    pro_CarInfo.operatorid = ljczy_text.Tag == null ? "" : ljczy_text.Tag.ToString();
                    pro_CarInfo.companyname = gsgsmc_text.Text;
                    pro_CarInfo.nextservicedate = xcjxsj_date.Value.ToString("yyyy-MM-dd");
                    pro_CarInfo.faultcontent = gznr_text.Text;
                    pro_CarInfo.otherdescriptions = qtms_text.Text;
                    this.db.Insertable(pro_CarInfo).ExecuteCommand();
                    MessageBox.Show("保存成功");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
 
        /// <summary>
        /// 检查为空
        /// </summary>
        /// <returns></returns>
        public bool CheckNull()
        {
            if(xcljzt_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("连接状态不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(gsgsmc_text.Text))
            {
                MessageBox.Show("归属公司不能为空");
                return false;
            }
            return true;
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ljczy_text_Click(object sender, EventArgs e)
        {
            UserSelectForm userSelectForm = new UserSelectForm(db);
            userSelectForm.RoleName = "操作员";
             userSelectForm.StartPosition = FormStartPosition.CenterParent;
            if (userSelectForm.ShowDialog() == DialogResult.OK)
            {
                if (userSelectForm.SelectDataRow!= null)
                {
                    ljczy_text.Text = userSelectForm.SelectDataRow["RealName"].ToString();
                    ljczy_text.Tag = userSelectForm.SelectDataRow["UserId"].ToString();
                }
            }
        }

        private void ljgdy_text_Click(object sender, EventArgs e)
        {
            UserSelectForm userSelectForm = new UserSelectForm(db);
            userSelectForm.RoleName = "工单员";
            userSelectForm.StartPosition = FormStartPosition.CenterParent;
            if (userSelectForm.ShowDialog() == DialogResult.OK)
            {
                if (userSelectForm.SelectDataRow != null)
                {
                    ljgdy_text.Text = userSelectForm.SelectDataRow["RealName"].ToString();
                    ljgdy_text.Tag = userSelectForm.SelectDataRow["UserId"].ToString();
                }
            }
        }

       

        private void GLYFCAddForm_Load(object sender, EventArgs e)
        {
           
        }

        private void qtms_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void ljfc_text_Click(object sender, EventArgs e)
        {
            FCSelectForm fCSelectForm = new FCSelectForm(db);

            fCSelectForm.StartPosition = FormStartPosition.CenterParent;
            if (fCSelectForm.ShowDialog() == DialogResult.OK)
            {
                if (fCSelectForm.SelectDataRow != null) 
                {
                    ljgdy_text.Text = fCSelectForm.SelectDataRow["shelterscode"].ToString();
                    ljgdy_text.Tag = fCSelectForm.SelectDataRow["sheltersid"].ToString();
                }
            }
        }
    }
}
