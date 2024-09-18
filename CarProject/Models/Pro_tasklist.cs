using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///任务单
    ///</summary>
    public partial class Pro_tasklist
    {
        #region 实体
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey=true)]
           public long taskid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int totalnumber {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int finishnumber {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int notfinishnumber {get;set;}

           /// <summary>
           /// Desc:删除标记,1-删除
           /// Default:0
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int DeleteMark {get;set;}

           /// <summary>
           /// Desc:是否有效(0无效，1有效)
           /// Default:1
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int Enabled {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public long workordercarid {get;set;}

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
           /// Desc:修改日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? ModifyDate {get;set;}

           /// <summary>
           /// Desc:修改用户主键
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string ModifyUserId {get;set;}

           /// <summary>
           /// Desc:修改用户
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string ModifyUserName {get;set;}
        /// <summary>
        /// Desc:操作员id
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore = false)]
        public string operatorid { get; set; }
        /// <summary>
        /// Desc:任务单编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore = false)]
        public string taskcode { get; set; }
        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.taskid = SnowFlakeSingle.Instance.NextId();//雪花id;
            this.CreateDate = DateTime.Now;
           
            this.Enabled = 1;
            this.DeleteMark = 0;
            this.totalnumber =0;
            this.finishnumber = 0;
            this.notfinishnumber =0;
           
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(long keyValue)
        {
            this.taskid = keyValue;
            this.ModifyDate = DateTime.Now;
             
        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(long keyValue)
        {
            this.taskid = keyValue;
            this.DeleteMark = 1;
            this.Enabled = 0;
        }
        #endregion
    }
}
