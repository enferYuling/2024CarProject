using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Common
{
    public class SqlSugarConfig
    {
         private static readonly string connectionString = "Data Source=8.137.119.17;Database=CarProject;User Id=sa;Password=Hbjkj@#123;";
       // private static readonly string connectionString = "Data Source=.;Database=CarProject;User Id=sa;Password=123456;";

        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            return db;
        }
    }
}
