using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///方舱连接信息
    ///</summary>
    public partial class Pro_sheltersConnect
    {
        #region 实体
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey=true)]
           public string sheltersConnectid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public long? sheltersid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string userid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string Account {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int? userenabled {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string rolename {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? startdate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? firstdate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? enddate {get;set;}
        #endregion
        #region 扩展操作
        public void Create()
        {
            this.sheltersConnectid=Guid.NewGuid().ToString().Replace("-","");
        }
        #endregion
    }
}
