using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///任务工单
    ///</summary>
    public partial class Pro_taskWorkorder
    {
        #region 实体
        /// <summary>
        /// Desc:任务工单id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey=true)]
           public long taskworkorderid {get;set;}

        /// <summary>
        /// Desc:任务单id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public long taskid {get;set;}

        /// <summary>
        /// Desc:发布者id
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public string releaseid {get;set;}

        /// <summary>
        /// Desc:发布者账号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public string releaseaccount {get;set;}

        /// <summary>
        /// Desc:工单车辆id
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public long? carid {get;set;}

        /// <summary>
        /// Desc:工单完成期限（月）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public int? termmouth {get;set;}

        /// <summary>
        /// Desc:操作员完成日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public DateTime? enddate {get;set;}

        /// <summary>
        /// Desc:操作员id
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public string operatorid {get;set;}

        /// <summary>
        /// Desc:计划完成日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public DateTime? plandate {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? CreateDate {get;set;}

           /// <summary>
           /// Desc:创建用户主键
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string CreateUserId {get;set;}

           /// <summary>
           /// Desc:创建人
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string CreateUserName {get;set;}
           /// <summary>
           /// Desc:工单状态0未完成1已完成
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int? workstate { get;set;}
        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.taskworkorderid = SnowFlakeSingle.Instance.NextId();//雪花id;
            this.CreateDate = DateTime.Now; 
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(long keyValue)
        {
            this.taskworkorderid = keyValue;
            
        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(long keyValue)
        {
            this.taskworkorderid = keyValue;
            
        }
        #endregion
    }
}
