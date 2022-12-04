using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UACSDAL
{
    /// <summary>
    /// 鞍座基类
    /// </summary>
    public class GRIDBase : ICloneable
    {

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public GRIDBase Clone()
        {
            return (GRIDBase)this.MemberwiseClone();
        }

        /// <summary>
        /// 料格号
        /// </summary>
        public string GRID_NO { get; set; }
        /// <summary>
        /// 料格名
        /// </summary>
        public string GRID_NAME { get; set; }
        /// <summary>
        /// 区域号
        /// </summary>
        public string AREA_NO { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        public string AREA_NAME { get; set; }
        /// <summary>
        /// 料格区分
        /// </summary>
        public string GRID_DIV { get; set; }
        /// <summary>
        /// 料格类型
        /// </summary>
        public string GRID_TYPE { get; set; }
        /// <summary>
        /// 起始坐标X 单位-mm
        /// </summary>
        public int X_START { get; set; }
        /// <summary>
        /// 结束坐标X 单位-mm
        /// </summary>
        public int X_END { get; set; }
        /// <summary>
        /// 中心点坐标X 单位-mm
        /// </summary>
        public int X_CENTER { get; set; }
        /// <summary>
        /// 起始坐标Y 单位-mm
        /// </summary>
        public int Y_START { get; set; }
        /// <summary>
        /// 结束坐标Y 单位-mm
        /// </summary>
        public int Y_END { get; set; }
        /// <summary>
        /// 中心点坐标Y 单位-mm
        /// </summary>
        public int Y_CENTER { get; set; }
        /// <summary>
        /// 物料代码
        /// </summary>
        public string MAT_CODE { get; set; }
        /// <summary>
        /// 钢制代码
        /// </summary>
        public string COMP_CODE { get; set; }
        /// <summary>
        /// 库存重量 单位-KG
        /// </summary>
        public int MAT_WGT { get; set; }
        /// <summary>
        /// 料格状态
        /// </summary>
        public string GRID_STATUS { get; set; }
        /// <summary>
        /// 料格可用标记
        /// </summary>
        public string GRID_ENABLE { get; set; }
        /// <summary>
        /// 所在YARD号
        /// </summary>
        public string YARD_NO { get; set; }
        /// <summary>
        /// 所在BAY号
        /// </summary>
        public string BAY_NO { get; set; }

        public const string tagServiceName = "iplature";
    }
}
