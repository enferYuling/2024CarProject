using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///异常明细内容
    ///</summary>
    public partial class Pro_Abnormalcontent
    {
           public Pro_Abnormalcontent(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string Abnormalcontentid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string address {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string position {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public DateTime? time {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string imgs {get;set;}

    }
}
