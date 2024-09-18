using System;
using System.Linq;
using System.Text;
using HZH_Controls;
using SqlSugar;

namespace CarProject.Models
{
    ///<summary>
    ///用户信息表
    ///</summary>
    [SugarTable("Base_User")]
    public partial class Base_User
    {
        #region 实体
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//数据库是自增才配自增 
        public string userid { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string usercode { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string realname { get; set; }
        /// <summary>
        /// 呢称
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string nickname { get; set; }
        /// <summary>
        /// 性别（0.男1.女）
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public int gender { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string mobile { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string email { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string companyid { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string departmentid { get; set; }
        /// <summary>
        /// 是否系统管理员
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string issystem { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>

        [SugarColumn(IsIgnore = false)]
        public DateTime? createdate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string createuserid { get; set; }
        /// <summary>
        /// 创建用户名
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string createusername { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public DateTime? modifydate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string modifyuserid { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string modifyusername { get; set; }
        /// <summary>
        /// 用户头像地址
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string avatar { get; set; }
        /// <summary>
        /// 删除标记,1-删除
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public int deletemark { get; set; }
        /// <summary>
        /// 是否有效(1-有效,0-无效)
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public int enabled { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string address { get; set; }
        /// <summary>
        /// 月登录次数
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public int monthnumber { get; set; }
        /// <summary>
        /// 当前登录月
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public int loginmonth { get; set; }
        /// <summary>
        /// 累计登录次数
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public long Loginnumber { get; set; }
        /// <summary>
        /// 当前登录时间
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public DateTime logintime { get; set; }
        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.userid = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            this.createdate = DateTime.Now;
            this.createuserid = this.userid;
            this.createusername = this.realname;
            this.enabled = 1;
            this.deletemark = 0;
            this.monthnumber = 1;
            this.Loginnumber = 1;
            this.loginmonth = DateTime.Now.ToString("MM").ToInt();
            this.logintime = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.userid = keyValue;
            this.modifydate = DateTime.Now;
            
        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(string keyValue)
        {
            this.userid = keyValue;
            this.deletemark = 1;
            this.enabled=0;
        }
        #endregion

    }
}
