using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CarProject.Models
{
    ///<summary>
    ///用户角色表
    ///</summary>
    [SugarTable("Base_Role_User")]
    public partial class Base_Role_User
    {
        #region 实体
        /// <summary>
        /// 用户角色id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//数据库是自增才配自增 
        public string UserRoleId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string UserId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public int? RoleId { get; set; }
      

        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.UserRoleId = Guid.NewGuid().ToString().ToLower().Replace("-", "");

        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.UserRoleId = keyValue;

        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(string keyValue)
        {
            this.UserRoleId = keyValue;
        }
        #endregion
       

    }
}
