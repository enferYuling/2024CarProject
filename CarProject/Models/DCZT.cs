using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Models
{
    /// <summary>
    /// 电池报文
    /// </summary>
    public class DCZT
    {
        public string DYBWStatus { get; set; }//电压详细报文
        public string WDBWStatus { get; set; }//温度详细
        public string ChargingStatus { get; set; }//是否充电中
        public string DCZKDStatus { get; set; }//电池组亏电
        public string DCZJXStatus { get; set; }//电池组就绪状态
        public string FDJCQStatus { get; set; }//放电接触器状态
        public string CDJCQStatus { get; set; }//充电器接触状态
        public string Allvoltage { get; set; }// 电池组总电压
        public string AllTem { get; set; }// 总温度
        public string Allcurrent { get; set; }// 电池组总充放电流
        public int SOCStatus { get; set; }// SOC的值
        public string Faulttatus { get; set; }//故障等级
        public string SMAXVStatus { get; set; }//单体最高电压
        public string SMAINVStatus { get; set; }//单体最低电压
        public string MAXCStatus { get; set; }//最大允许放电电流
        public string SMAXTStatus { get; set; }//单体最高温度
        public string SMAINTStatus { get; set; }//单体最低温度
        public string CDMAXVStatus { get; set; }//最高允许充电电电压
        public string CDMAXAStatus { get; set; }//最高允许充电电电流
        public string IscdjcdStatus { get; set; }//充电机是否开始充电           
        public string OUTPUTVStatus { get; set; }//输出电压
        public string OUTPUTAStatus { get; set; }//输出电流
        public string YJFAULTStatus { get; set; }//硬件故障
        public string CDJWDFAULTStatus { get; set; }//充电机温度故障
        public string LOWVmodelStatus { get; set; }//低压功率模式
        public string INTPUTVStatus { get; set; }//输入电压状态
        public string OPAmodelStatus { get; set; }//输出电流是否过流
        public string CDJSTARTStatus { get; set; }//充电机启动状态
        public string TONGXINGStatus { get; set; }//通信状态
        public string DCLJStatus { get; set; }//电池连接状态
        public string DWmodelStatus { get; set; }//挡位状态
        public string SCmodelStatus { get; set; }//刹车状态
        public string SPORTmodelStatus { get; set; }//运行模式
        public string KZQZTStatus { get; set; }//控制器状态
        public string XGHZTStatus { get; set; }//限功耗状态
        public string DJZSStatus { get; set; }//电机转速
        public string XJLCStatus { get; set; }//小计里程
        public string CARSPEEDStatus { get; set; }//车速
        public string DCVStatus { get; set; }//电池电压
        public string DJAStatus { get; set; }//电机电流
        public string DJTStatus { get; set; }//电机温度
        public string KZQTStatus { get; set; }//控制器温度           

    }
}
