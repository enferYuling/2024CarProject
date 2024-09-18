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
    public partial class FCConfigForm : Form
    {
        public long sheltersid;
        public string account;
        public string realName;
        public readonly SqlSugarClient db;
        public FCConfigForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void FCConfigForm_Load(object sender, EventArgs e)
        {
            Connect_GridView.AutoGenerateColumns = false;
            Fault_GridView.AutoGenerateColumns = false;
            FormAssignment();
        }
        /// <summary>
        /// 给窗口赋值
        /// </summary>
        public void FormAssignment()
        {
            var pro_SheltersInfo=this.db.Queryable<Pro_sheltersInfo>().Where(a=>a.sheltersid==sheltersid).First();
            if(pro_SheltersInfo == null)
            {
                MessageBox.Show("该方舱不存在");
                this.Close();
                return;
            }
            List<Pro_sheltersFault> list_SheltersFaults = this.db.Queryable<Pro_sheltersFault>().Where(a => a.sheltersid == sheltersid).ToList();//方舱故障
            List<Pro_sheltersConnect> _SheltersConnects = this.db.Queryable<Pro_sheltersConnect>().Where(a => a.sheltersid == sheltersid).ToList();//方舱连接
            Connect_GridView.DataSource = _SheltersConnects;
            Fault_GridView.DataSource = list_SheltersFaults;
            fcfs_text.Text = pro_SheltersInfo.outwindspeed.ToString() + "m/s";
            fcnbwd_text.Text = pro_SheltersInfo.intemperature;
            fcwbwd_text.Text = pro_SheltersInfo.outtemperature;
            fcbyhdl_text.Text = pro_SheltersInfo.powerconsumption;
            fcfwqzt_text.Text = pro_SheltersInfo.serverstatus==1?"启动":"停止";
            ypcc_text.Text = pro_SheltersInfo.harddiskstorage  ;
            byfcrgkqcs_text.Text = pro_SheltersInfo.artificial.ToString()+"次";
            ndfcyszl_text.Text = pro_SheltersInfo.yearwater.ToString()+"度";
            ndfcydzl_text.Text = pro_SheltersInfo.yearelectricity.ToString()+"方";
            ndfcxhllzl_text.Text = pro_SheltersInfo.yearflow.ToString()+"G";
            if (!string.IsNullOrEmpty(pro_SheltersInfo.operatorid))
            {
                var query = this.db.Queryable<Base_User>().Where(a => a.userid == pro_SheltersInfo.operatorid).First();
                if (query != null) 
                {
                    czyglzt_text.Text = "操作员已关联";
                    czygl_text.Text = query.account;
                }

            }
            else
            {
                czyglzt_text.Text = "操作员无关联";
            }
            if (!string.IsNullOrEmpty(pro_SheltersInfo.workorderclerkid))
            {
                var query = this.db.Queryable<Base_User>().Where(a => a.userid == pro_SheltersInfo.workorderclerkid).First();
                if (query != null)
                {
                    gdyglzt_btn.Text = "工单员已关联";
                    gdygl_text.Text = query.account;
                }

            }
            else
            {
                gdyglzt_btn.Text = "工单员无关联";
            }
            if (!string.IsNullOrEmpty(pro_SheltersInfo.carid))
            {
                
            }
            else
            {
                clglzt_text.Text = "车辆无关联";
            }
            gsgsmc_text.Text = pro_SheltersInfo.companyname;
            qtms_text.Text = pro_SheltersInfo.otherdescriptions;
            fcbh_text.Text = pro_SheltersInfo.shelterscode;
            switch (pro_SheltersInfo.status)
            {
                case 0:
                    fcljzt_text.Text = "方舱连接状态:运行中";
                    break;
                case 1:
                    fcljzt_text.Text = "方舱连接状态:离线";
                    break;
                case 2:
                    fcljzt_text.Text = "方舱连接状态:故障";
                    break;
            }
            qtms_text.Text = pro_SheltersInfo.otherdescriptions;
            qtms_text.Text = pro_SheltersInfo.otherdescriptions;
            yfwdycs_btn.Text = pro_SheltersInfo.monthusenumber.ToString()+"次";
            ljfwdycs_text.Text = pro_SheltersInfo.usenumber.ToString()+"次"; 
             
        }
        /// <summary>
        /// 操作员刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sxgl2_text_Click(object sender, EventArgs e)
        {
            var pro_SheltersConnect=this.db.Queryable<Pro_sheltersConnect>().Where(a=>a.sheltersid==sheltersid&&a.Account== czygl_text.Text&&a.rolename=="操作员").First();
            
            if (pro_SheltersConnect != null)
            {
                pro_SheltersConnect.startdate = DateTime.Now;
                this.db.Updateable<Pro_sheltersConnect>(pro_SheltersConnect).UpdateColumns(it => new {it.startdate}).ExecuteCommand();//只更新指定列
                czyglzt_text.Text = "关联成功";
            }
            else
            {
              
                var query = this.db.Queryable<Base_User>()
                    .LeftJoin<Base_Role_User>((a, b) => a.userid == b.UserId)
                    .LeftJoin<Base_Role>((a, b, c) => b.RoleId == c.RoleId)
                     .Where((a, b, c) => a.account == czygl_text.Text&&c.RoleName=="操作员").Select((a, b, c) => new
                     {
                         a.account,
                         a.userid,
                         a.enabled,
                         c.RoleName
                     }).First();
                if (query != null)
                {
                    pro_SheltersConnect = new Pro_sheltersConnect();
                    pro_SheltersConnect.Create();
                    pro_SheltersConnect.sheltersid = sheltersid;
                    pro_SheltersConnect.startdate = DateTime.Now;
                    pro_SheltersConnect.firstdate = DateTime.Now;
                    pro_SheltersConnect.Account = czygl_text.Text;
                    pro_SheltersConnect.userid = query.userid;
                    pro_SheltersConnect.rolename = query.RoleName;
                    pro_SheltersConnect.userenabled = query.enabled;
                    this.db.Insertable(pro_SheltersConnect).ExecuteCommand();
                    czyglzt_text.Text = "关联成功";
                }
                else
                {
                    czyglzt_text.Text = "操作员不存在";
                }

            }
            LoadGLData();
        }
        /// <summary>
        /// 工单员刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sxgl3_btn_Click(object sender, EventArgs e)
        {
            var pro_SheltersConnect = this.db.Queryable<Pro_sheltersConnect>().Where(a => a.sheltersid == sheltersid && a.Account == gdygl_text.Text&&a.rolename=="工单员").First();
            if (pro_SheltersConnect != null)
            {
                pro_SheltersConnect.startdate = DateTime.Now;
                this.db.Updateable<Pro_sheltersConnect>(pro_SheltersConnect).UpdateColumns(it => new { it.startdate }).ExecuteCommand();//只更新指定列
                gdyglzt_btn.Text = "关联成功";
            }
            else
            {

                var query = this.db.Queryable<Base_User>()
                    .LeftJoin<Base_Role_User>((a, b) => a.userid == b.UserId)
                    .LeftJoin<Base_Role>((a, b, c) => b.RoleId == c.RoleId)
                     .Where((a, b, c) => a.account == gdygl_text.Text && c.RoleName == "工单员").Select((a, b, c) => new
                     {
                         a.account,
                         a.userid,
                         a.enabled,
                         c.RoleName
                     }).First();
                if (query != null)
                {
                    pro_SheltersConnect = new Pro_sheltersConnect();
                    pro_SheltersConnect.Create();
                    pro_SheltersConnect.sheltersid = sheltersid;
                    pro_SheltersConnect.startdate = DateTime.Now;
                    pro_SheltersConnect.firstdate = DateTime.Now;
                    pro_SheltersConnect.Account = gdygl_text.Text;
                    pro_SheltersConnect.userid = query.userid;
                    pro_SheltersConnect.rolename = query.RoleName;
                    pro_SheltersConnect.userenabled = query.enabled;
                    this.db.Insertable(pro_SheltersConnect).ExecuteCommand();
                    gdyglzt_btn.Text = "关联成功";
                }
                else
                {
                    gdyglzt_btn.Text = "工单员不存在";
                }

            }
            LoadGLData();
        }
        public void LoadGLData()
        {
 
            List<Pro_sheltersConnect> _SheltersConnects = this.db.Queryable<Pro_sheltersConnect>().Where(a => a.sheltersid == sheltersid).ToList();//方舱连接
            Connect_GridView.DataSource = _SheltersConnects;
            
        }

        private void Connect_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.RowIndex >= 0)//删除按钮点击
            {
                // 获取对应的单元格
                DataGridViewCell buttonCell = Connect_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // 判断单元格类型是否为按钮类型
                if (buttonCell is DataGridViewButtonCell)
                {
                    var row = Connect_GridView.Rows[e.RowIndex];
                    var fcljxxid = row.Cells["fcljxxid"].Value.ToString();
                    if (!string.IsNullOrEmpty(fcljxxid))
                    {
                        this.db.Deleteable<Pro_sheltersConnect>().Where(a=>a.sheltersConnectid==fcljxxid).ExecuteCommand();
                        LoadGLData();
                    }
                }
            }
        }

        private void sxgl1_btn_Click(object sender, EventArgs e)
        {
            var carInfo=this.db.Queryable<Pro_CarInfo>().Where(a=>a.carcode==clgl_text.Text).First();
            if(carInfo!=null)
            {
                var query=this.db.Queryable<Pro_sheltersInfo>().Where(a=>a.sheltersid==sheltersid).First();
                query.carid = carInfo.carid.ToString();
                carInfo.sheltersid = sheltersid;
                this.db.Updateable(query).UpdateColumns(it => new { it.carid }).ExecuteCommand();
                this.db.Updateable(carInfo).UpdateColumns(it => new { it.sheltersid }).ExecuteCommand();
                clglzt_text.Text = "关联成功";
            }
            else
            {
                clglzt_text.Text = "小车不存在";
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            var query = this.db.Queryable<Pro_sheltersInfo>().Where(a => a.sheltersid == sheltersid).First();
            if (!string.IsNullOrEmpty(clgl_text.Text))
            {
                query.carid = this.db.Queryable<Pro_CarInfo>().Where(a => a.carcode == clgl_text.Text).First().carid.ToString();
            }
            if (!string.IsNullOrEmpty(gdygl_text.Text))
            {
                query.workorderclerkid = this.db.Queryable<Base_User>().Where(a => a.account == gdygl_text.Text).First().userid;
            }
            if (!string.IsNullOrEmpty(czygl_text.Text))
            {
               query.operatorid = this.db.Queryable<Base_User>().Where(a => a.account == czygl_text.Text).First().userid;
            }
            this.db.Updateable<Pro_sheltersInfo>(query).UpdateColumns(it => new {it.operatorid,it.carid,it.workorderclerkid}).ExecuteCommand();
           
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
