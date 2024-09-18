using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CarProject.Models
{
    ///<summary>
    ///角色表
    ///</summary>
    [SugarTable("Base_Role")]
    public partial class Base_Role
    {
        #region 实体
        /// <summary>
        /// 角色id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, IsNullable = false)]//数据库是自增才配自增 
        public int RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        [SugarColumn(IsIgnore = false)]
        public string RoleCode { get; set; }
       

        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            

        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(int keyValue)
        {
            this.RoleId = keyValue;

        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(int keyValue)
        {
            this.RoleId = keyValue;
        }
        #endregion
        
          

    }
}
