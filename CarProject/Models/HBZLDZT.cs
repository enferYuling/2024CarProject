using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Models
{
    /// <summary>
    /// 后避障雷达报文解析
    /// </summary>
    public class HBZLDZT
    {
        public double HTONGDAO1 { get; set; }//后通道1
        public double HTONGDAO2 { get; set; }// 后通道2
        public double HTONGDAO3 { get; set; }// 后通道3
        public double HTONGDAO4 { get; set; }// 后通道4          
    }
}
