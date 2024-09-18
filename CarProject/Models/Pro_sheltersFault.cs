using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace CarProject.Models
{
    ///<summary>
    ///方舱故障
    ///</summary>
    public partial class Pro_sheltersFault
    {
           public Pro_sheltersFault(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public long sheltersFaultid {get;set;}

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
           public string faultdate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string faulttime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string description {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(IsIgnore=false)]
           public string reportperson {get;set;}

    }
}
