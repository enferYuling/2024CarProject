using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///方舱信息
    ///</summary>
    public partial class Pro_sheltersInfo
    {
        #region 实体
        /// <summary>
        /// Desc: 
        [SugarColumn(IsPrimaryKey=true)]
           public long sheltersid {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string shelterscode {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int? status {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string outtemperature {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string powerconsumption {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int? serverstatus {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string harddiskstorage {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int? monthusenumber {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public long? usenumber {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int? usemonth {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string carid {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string operatorid {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string workorderclerkid {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string companyname {get;set;}

           /// <summary>
           /// Desc: 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string otherdescriptions {get;set;}

           /// <summary>
           /// Desc:创建日期 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? CreateDate {get;set;}

           /// <summary>
           /// Desc:创建用户主键 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string CreateUserId {get;set;}

           /// <summary>
           /// Desc:创建人 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string CreateUserName {get;set;}

           /// <summary>
           /// Desc:修改日期 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? ModifyDate {get;set;}

           /// <summary>
           /// Desc:修改用户主键 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string ModifyUserId {get;set;}

           /// <summary>
           /// Desc:删除标记,1-删除 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int DeleteMark {get;set;}

           /// <summary>
           /// Desc:是否有效(0无效，1有效) 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public int Enabled {get;set;}

           /// <summary>
           /// Desc:修改用户 
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string ModifyUserName {get;set;}

        /// <summary>
        /// Desc:本月人工方舱开启次数 
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public int? artificial {get;set;}

        /// <summary>
        /// Desc:年度方舱用电总量 
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public long? yearelectricity {get;set;}

        /// <summary>
        /// Desc:年度方舱用水总量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public long? yearwater {get;set;}

        /// <summary>
        /// Desc:年度方舱信号流量总量 
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public long? yearflow {get;set;}

        /// <summary>
        /// Desc:故障内容 
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public string faultcontent {get;set;}

        /// <summary>
        /// Desc:下次检修时间 
        /// </summary>           
        [SugarColumn(IsIgnore=false)]
           public string nextservicedate {get;set;}
        /// <summary>
        /// Desc:外部风速 
        /// </summary>           
        [SugarColumn(IsIgnore = false)]
        public int? outwindspeed { get; set; }
        /// <summary>
        /// Desc:内部温度 
        /// </summary>           
        [SugarColumn(IsIgnore = false)]
        public string intemperature { get; set; }

        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.sheltersid = SnowFlakeSingle.Instance.NextId();//雪花id
            this.CreateDate = DateTime.Now;

            this.Enabled = 1;
            this.DeleteMark = 0;
            this.monthusenumber = 0;
            this.usemonth = 0;
            this.usenumber = 0;
            this.artificial = 0;
            this.yearelectricity = 0;
            this.yearflow = 0;
            this.yearwater = 0;


        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(long keyValue)
        {
            this.sheltersid = keyValue;
            this.ModifyDate = DateTime.Now;

        }
        /// <summary>
        /// 删除调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Delete(long keyValue)
        {
            this.sheltersid = keyValue;
        }
        #endregion
    }
}
