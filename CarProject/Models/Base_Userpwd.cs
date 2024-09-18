using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CarProject.Models
{
    ///<summary>
    ///用户密码对照表
    ///</summary>
    [SugarTable("Base_Userpwd")]
    public partial class Base_Userpwd
    {
        #region 实体
        /// <summary>
        /// 用户密码主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//数据库是自增才配自增 
        public string userpwdid { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string userid { get; set; }
        /// <summary>
        /// 用户密码（加密）
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string password { get; set; }
        /// <summary>
        /// 用户密码（未加密）
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string oldpassword { get; set; }

        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.userpwdid = Guid.NewGuid().ToString().ToLower().Replace("-", "");

        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.userpwdid = keyValue;

        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(string keyValue)
        {
            this.userpwdid = keyValue;
        }
        #endregion

    }
}
