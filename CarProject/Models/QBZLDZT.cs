using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Models
{
    /// <summary>
    /// 前后避障雷达报文解析
    /// </summary>
    public class QBZLDZT
    {
        public double QTONGDAO1 { get; set; }//前通道1
        public double QTONGDAO2 { get; set; }// 前通道2
        public double QTONGDAO3 { get; set; }// 前通道3
        public double QTONGDAO4 { get; set; }// 前通道4        
    }
}
