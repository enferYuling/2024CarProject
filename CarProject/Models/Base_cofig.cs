using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class Base_cofig
    {
        #region 实体
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey=true)]
           public string cofigid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string cofigName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int cofigType {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string configAddress {get;set;}
        #endregion
        #region 扩展方法
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
this.cofigid=Guid.NewGuid().ToString().Replace("-","");

        }
        #endregion
    }
}
