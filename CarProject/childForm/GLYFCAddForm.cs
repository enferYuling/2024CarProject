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
    public partial class GLYFCAddForm : Form
    {
        public readonly SqlSugarClient db;
        public string  realName;//账号
        public GLYFCAddForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void sure_btn_Click(object sender, EventArgs e)
        {
            if (CheckNull())
            {
                Pro_sheltersInfo pro_SheltersInfo = new Pro_sheltersInfo();
                pro_SheltersInfo.Create();
                pro_SheltersInfo.CreateUserName= realName;
                if (string.IsNullOrEmpty(fcbh_text.Text))
                {
                    var count = this.db.Queryable<Pro_sheltersInfo>().Where(a => a.CreateDate == DateTime.Now).Count();
                    pro_SheltersInfo.shelterscode = "FC_" + DateTime.Now.ToString("yyyyMMdd") + (count + 1).ToString();
                }
                else
                {
                    pro_SheltersInfo.shelterscode = fcbh_text.Text;
                }
                try
                {
                    pro_SheltersInfo.status = fcljzt_comboBox.SelectedIndex;
                    pro_SheltersInfo.outtemperature = fcwbwd_text.Text + "度";
                    pro_SheltersInfo.serverstatus = fcfwqzt_checkBox.Checked ? 1 : 0;
                    pro_SheltersInfo.harddiskstorage = ypcc_text.Text;
                    pro_SheltersInfo.carid = ljxc_text.Tag == null ? "" : ljxc_text.Tag.ToString();
                    pro_SheltersInfo.workorderclerkid = ljgdy_text.Tag == null ? "" : ljgdy_text.Tag.ToString();
                    pro_SheltersInfo.operatorid = ljczy_text.Tag == null ? "" : ljczy_text.Tag.ToString();
                    pro_SheltersInfo.companyname = gsgsmc_text.Text;
                    pro_SheltersInfo.faultcontent = gznr_text.Text;
                    pro_SheltersInfo.nextservicedate = xcjxsj_date.Text;
                    pro_SheltersInfo.otherdescriptions = qtms_text.Text;
                    this.db.Insertable<Pro_sheltersInfo>(pro_SheltersInfo).ExecuteCommand();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
           
        }

        private void fcwbwd_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                if (e.KeyChar != '.' || this.fcwbwd_text.Text.Length == 0)//小数点  
                {
                    e.Handled = true;
                }
                if (fcwbwd_text.Text.LastIndexOf('.') != -1)
                {
                    e.Handled = true;
                }
            }
        }
        /// <summary>
        /// 检查为空
        /// </summary>
        /// <returns></returns>
        public bool CheckNull()
        {
            if (string.IsNullOrEmpty(fcwbwd_text.Text))
            {
                MessageBox.Show("方舱室外温度不能为空");
                return false;
            }
            if(fcljzt_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("请选择方舱连接状态");
                return false;
            }
            return true;
        }

        private void canle_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ljczy_text_TextChanged(object sender, EventArgs e)
        {

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

        private void ljxc_text_Click(object sender, EventArgs e)
        {
            CarSelectForm carSelectForm = new CarSelectForm(db);

            carSelectForm.StartPosition = FormStartPosition.CenterParent;
            if (carSelectForm.ShowDialog() == DialogResult.OK)
            {
                if (carSelectForm.SelectDataRow != null)
                {
                    ljxc_text.Text = carSelectForm.SelectDataRow["carid"].ToString();
                    ljxc_text.Tag = carSelectForm.SelectDataRow["carcode"].ToString();
                }
            }
        }

        private void GLYFCAddForm_Load(object sender, EventArgs e)
        {

        }

        private void fcwbwd_text_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
