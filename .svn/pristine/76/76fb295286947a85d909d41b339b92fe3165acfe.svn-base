using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UACSDAL
{
    public class ParkingBase
    {
        /// <summary>
        /// 停车位名称
        /// </summary>
        public string ParkingName { get; set; }

        /// <summary>
        /// X起
        /// </summary>
        public int X_START { get; set; }

        /// <summary>
        /// X终
        /// </summary>
        public int X_END { get; set; }

        /// <summary>
        /// Y起
        /// </summary>
        public int Y_START { get; set; }

        /// <summary>
        /// Y终
        /// </summary>
        public int Y_END { get; set; }

        /// <summary>
        /// X中心
        /// </summary>
        public int X_CENTER { get; set; }

        /// <summary>
        /// Y中心
        /// </summary>
        public int Y_CENTER { get; set; }

        /// <summary>
        /// X长
        /// </summary>
        public int X_LENGTH { get; set; }

        /// <summary>
        /// Y长
        /// </summary>
        public int Y_LENGTH { get; set; }

        /// <summary>
        /// 车宽
        /// </summary>
        public int CarWidth { get; set; }

        /// <summary>
        /// 车长
        /// </summary>
        public int CarLength { get; set; }

        /// <summary>
        /// 计算的X车长
        /// </summary>
        public int X_CenterCount { get; set; }

        /// <summary>
        /// 计算的Y车宽
        /// </summary>
        public int Y_CenterCount { get; set; }


        /// <summary>
        /// 车头位置(东：E 西：W 南：S 北：N)
        /// </summary>
        public string HeadPostion { get; set; }

        /// <summary>
        /// 空重状态(空车：0 重车：1）
        /// </summary>
        public int IsLoaded { get; set; }

        /// <summary>
        /// 停车位状态
        ///5： 无车
        ///10：有车到达
        ///110：激光扫描开始
        ///120：入库激光扫描完成
        ///130：入库手持扫描完成
        ///210：出库激光扫描开始
        ///220：出库激光扫描完成
        /// </summary>
        public int PackingStatus { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string Car_No { get; set; }

        /// <summary>
        /// 配载图
        /// </summary>
        public int STOWAGE_ID { get; set; }

        /// <summary>
        /// 处理号
        /// </summary>
        public string TREATMENT_NO { get; set; }


        /// <summary>
        /// 停车位状态描述
        /// </summary>
        /// <returns></returns>
        public string PackingStatusDesc()
        {
            if (PackingStatus == 5)
            {
                return "无车";
            }
            else if (PackingStatus == 10)
            {
                return "有车到达";
            }
            else if (PackingStatus == 270)
            {
                return "已暂停";
            }
            else if (PackingStatus == 110)
            {
                return "激光扫描开始";
            }
            else if (PackingStatus == 120)
            {
                return "入库激光扫描完成";
            }
            else if (PackingStatus == 130)
            {
                return "入库手持扫描完成";
            }
            else if (PackingStatus == 210)
            {
                return "出库激光扫描开始";
            }
            else if (PackingStatus == 220)
            {
                return "出库激光扫描完成";
            }
            else if (PackingStatus == 240)
            {
                return "出库装卷";
            }
            else
            {
                return "未知";
            }
        }


    }
}
