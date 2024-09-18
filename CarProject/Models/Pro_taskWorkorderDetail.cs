using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///任务工单明细
    ///</summary>
    public partial class Pro_taskWorkorderDetail
    {
        #region 实体
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey=true)]
           public long WorkorderDetailid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public long? taskworkorderid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int? state {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? carfinishdate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string cargis {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string abnormalimg {get;set;}
        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.WorkorderDetailid = SnowFlakeSingle.Instance.NextId();//雪花id;
           
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(long keyValue)
        {
            this.WorkorderDetailid = keyValue;

        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(long keyValue)
        {
            this.WorkorderDetailid = keyValue;

        }
        #endregion
    }
}
