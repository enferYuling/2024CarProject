using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///小车信息
    ///</summary>
    public partial class Pro_CarInfo
    {
        #region 实体
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey=true)]
           public long carid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string carcode {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string operatorid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string workorderclerkid {get;set;}

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
           public int? serverstatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string harddiskstorage {get;set;}

        /// <summary>
        /// Desc:月服务调用次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public int? monthusenumber {get;set;}

        /// <summary>
        /// Desc:累计服务调用次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public long? usenumber {get;set;}

        /// <summary>
        /// Desc:当前调用月份
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public int? usemonth {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string companyname {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string otherdescriptions {get;set;}

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
        /// Desc: 小车连接状态（0.运行中,1.离线2.故障，3充电中）
        /// Default:1
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsIgnore = false)]
        public int status { get; set; }
        /// <summary>
        /// Desc:下次检修时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore = false)]
        public string nextservicedate { get; set; }
        /// <summary>
        /// Desc:故障内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore = false)]
        public string faultcontent { get; set; }
        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.carid = SnowFlakeSingle.Instance.NextId();//雪花id
            this.CreateDate = DateTime.Now;

            this.Enabled = 1;
            this.DeleteMark = 0;
            this.monthusenumber = 0;
            this.usemonth = 0;
            this.usenumber = 0;
            
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(long keyValue)
        {
            this.carid = keyValue;
            this.ModifyDate = DateTime.Now;

        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(long keyValue)
        {
            this.carid = keyValue;
            this.Enabled = 0;
            this.DeleteMark = 1;
        }
        #endregion

    }
}
