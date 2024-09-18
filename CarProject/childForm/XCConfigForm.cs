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
    public partial class XCConfigForm : Form
    {
        public long carid;
        public string account;
        public string realName;
        public readonly SqlSugarClient db;
        public XCConfigForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void FCConfigForm_Load(object sender, EventArgs e)
        {
            Connect_GridView.AutoGenerateColumns = false;
            Fault_GridView.AutoGenerateColumns = false;
            
            FormAssignment();
            LoadGLData();
        }
        /// <summary>
        /// 给窗口赋值
        /// </summary>
        public void FormAssignment()
        {
            var query = this.db.Queryable<Pro_CarInfo, Base_User, Base_User, Pro_sheltersInfo>((a, b, c, d) => new JoinQueryInfos(
    JoinType.Left, a.workorderclerkid == b.userid, //左连接 左链接 左联 
    JoinType.Left, a.operatorid == c.userid, //左连接 左链接 左联 
    JoinType.Left, a.sheltersid == d.sheltersid)).Where((a, b, c, d) => a.carid == carid).Select((a, b, c, d) => new
    {
        a.carid,
        a.carcode,
        a.serverstatus,
        a.harddiskstorage,
        a.monthusenumber,
        a.usenumber,
        a.companyname,
        a.otherdescriptions,
        a.sheltersid,
        a.workorderclerkid,
        a.operatorid,
        workname = b.account,
        operatorname = c.account,
        d.shelterscode
    }).First();
            if(query != null)
            {
                fwqljzt_text.Text = query.serverstatus == 1 ? "已连接" : "未连接";
                ypcc_text.Text = query.harddiskstorage;
                xcbh_text.Text = query.carcode;
                czygl_text.Text = query.operatorname;
                gdygl_text.Text = query.workname;
                yfwdycs_btn.Text = query.monthusenumber.ToString()+"次";
                ljfwdycs_text.Text = query.usenumber.ToString()+"次";
                gsgsmc_text.Text = query.companyname;
                qtms_text.Text = query.otherdescriptions;
            }

        }
        /// <summary>
        /// 操作员刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sxgl2_text_Click(object sender, EventArgs e)
        {
             
        }
        /// <summary>
        /// 工单员刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sxgl3_btn_Click(object sender, EventArgs e)
        {
           
        }
        public void LoadGLData()
        { 
            var list=this.db.Queryable<Pro_carConnect>().Where(a=>a.carid==carid).ToList();
            Connect_GridView.DataSource = list;
        }

        private void Connect_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void sxgl1_btn_Click(object sender, EventArgs e)
        {
            
        }

        private void fwqljzt_text_Click(object sender, EventArgs e)
        {

        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            var query = this.db.Queryable<Pro_CarInfo>().Where(a => a.carid == carid).First();
            if (!string.IsNullOrEmpty(fcgl_text.Text))
            {
                query.sheltersid = this.db.Queryable<Pro_sheltersInfo>().Where(a => a.shelterscode == fcgl_text.Text).First().sheltersid;
            }
            if (!string.IsNullOrEmpty(gdygl_text.Text))
            {
                query.workorderclerkid = this.db.Queryable<Base_User>().Where(a => a.account == gdygl_text.Text).First().userid;
            }
            if (!string.IsNullOrEmpty(czygl_text.Text))
            {
                query.operatorid = this.db.Queryable<Base_User>().Where(a => a.account == czygl_text.Text).First().userid;
            }
            this.db.Updateable<Pro_CarInfo>(query).UpdateColumns(it => new { it.operatorid, it.carid, it.workorderclerkid }).ExecuteCommand();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
