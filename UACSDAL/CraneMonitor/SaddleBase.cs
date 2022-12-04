using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UACSDAL
{
    /// <summary>
    /// 鞍座基类
    /// </summary>
    public class SaddleBase : ICloneable
    {

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public SaddleBase Clone()
        {
            return (SaddleBase)this.MemberwiseClone();
        }

        private string saddleNo;
        /// <summary>
        /// 鞍座号
        /// </summary>
        public string SaddleNo
        {
            get { return saddleNo; }
            set { saddleNo = value; }
        }

        private string saddleName;
        /// <summary>
        /// 鞍座名称
        /// </summary>
        public string SaddleName
        {
            get { return saddleName; }
            set { saddleName = value; }
        }

        private int saddle_Seq;
        /// <summary>
        /// 鞍座序号
        /// </summary>
        public int Saddle_Seq
        {
            get { return saddle_Seq; }
            set { saddle_Seq = value; }
        }

        private int row_No;
        /// <summary>
        /// 行号
        /// </summary>
        public int Row_No
        {
            get { return row_No; }
            set { row_No = value; }
        }

        private int col_No;
        /// <summary>
        /// 列号
        /// </summary>
        public int Col_No
        {
            get { return col_No; }
            set { col_No = value; }
        }

        private long saddleWidth;
        /// <summary>
        /// 鞍座宽度
        /// </summary>
        public long SaddleWidth
        {
            get { return saddleWidth; }
            set { saddleWidth = value; }
        }

        private long saddleLength;
        /// <summary>
        /// 鞍座长度
        /// </summary>
        public long SaddleLength
        {
            get { return saddleLength; }
            set { saddleLength = value; }
        }

        private int layer_Num;
        /// <summary>
        /// 层号
        /// </summary>
        public int Layer_Num
        {
            get { return layer_Num; }
            set { layer_Num = value; }
        }

        private long x_Center;
        /// <summary>
        /// X中心点
        /// </summary>
        public long X_Center
        {
            get { return x_Center; }
            set { x_Center = value; }
        }

        private long y_Center;
        /// <summary>
        /// Y中心点
        /// </summary>
        public long Y_Center
        {
            get { return y_Center; }
            set { y_Center = value; }
        }

        private long z_Center;
        /// <summary>
        /// Z中心点
        /// </summary>
        public long Z_Center
        {
            get { return z_Center; }
            set { z_Center = value; }
        }

        private int stock_Status;
        /// <summary>
        /// 库位状态
        /// </summary>
        public int Stock_Status
        {
            get { return stock_Status; }
            set { stock_Status = value; }
        }

        private int lock_Flag;
        /// <summary>
        /// 封锁标记
        /// </summary>
        public int Lock_Flag
        {
            get { return lock_Flag; }
            set { lock_Flag = value; }
        }


        private string mat_No;
        /// <summary>
        /// 材料号
        /// </summary>
        public string Mat_No
        {
            get { return mat_No; }
            set
            {
                mat_No = value;
            }
        }

        private int coil_Open_Direction;
        /// <summary>
        /// 开卷方向
        /// </summary>
        public int Coil_Open_Direction
        {
            get { return coil_Open_Direction; }
            set { coil_Open_Direction = value; }
        }

        private string next_Unit_No;
        /// <summary>
        /// 下道机组
        /// </summary>
        public string Next_Unit_No
        {
            get { return next_Unit_No; }
            set
            {
                next_Unit_No = value;
            }
        }

        private string tagAdd_IsOccupied;
        /// <summary>
        /// 鞍座占位信号点地址
        /// </summary>
        public string TagAdd_IsOccupied
        {
            get
            {
                return tagAdd_IsOccupied;
            }
            set
            {
                tagAdd_IsOccupied = value;
            }
        }

        private long tagVal_IsOccupied;
        /// <summary>
        /// 鞍座占位信号点值
        /// </summary>
        public long TagVal_IsOccupied
        {
            get
            {
                return tagVal_IsOccupied;
            }
            set
            {
                tagVal_IsOccupied = value;
            }
        }

        private string tag_Lock_Request;
        /// <summary>
        /// 鞍座锁定请求地址
        /// </summary>
        public string Tag_Lock_Request
        {
            get { return tag_Lock_Request; }
            set { tag_Lock_Request = value; }
        }

        private long tag_Lock_Request_Value;
        /// <summary>
        /// 鞍座锁定请求值
        /// </summary>
        public long Tag_Lock_Request_Value
        {
            get { return tag_Lock_Request_Value; }
            set { tag_Lock_Request_Value = value; }
        }

        private string tag_IsLocked;
        /// <summary>
        /// 鞍座锁定反馈地址
        /// </summary>
        public string Tag_IsLocked
        {
            get { return tag_IsLocked; }
            set { tag_IsLocked = value; }
        }



        private long tag_IsLocked_Value;
        /// <summary>
        /// 鞍座锁定反馈值
        /// </summary>
        public long Tag_IsLocked_Value
        {
            get { return tag_IsLocked_Value; }
            set { tag_IsLocked_Value = value; }
        }


        private string coilNO;
        /// <summary>
        /// 钢卷号
        /// </summary>
        public string CoilNO
        {
            get
            {
                return coilNO;
            }
            set
            {
                coilNO = value;
            }
        }


        private int plastic_Flag;
        /// <summary>
        /// 钢卷套袋
        /// </summary>
        public int Plastic_Flag
        {
            get
            {
                return plastic_Flag;
            }
            set
            {
                plastic_Flag = value;
            }
        }

        private string product_Time;
        /// <summary>
        /// 钢卷生产时间
        /// </summary>
        public string Product_Time
        {
            get
            {
                return product_Time;
            }
            set
            {
                product_Time = value;
            }
        }

        private string L3_coil_Status;

        public string L3_coil_Status1
        {
            get { return L3_coil_Status; }
            set { L3_coil_Status = value; }
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
        public long X_START { get; set; }
        /// <summary>
        /// 结束坐标X 单位-mm
        /// </summary>
        public long X_END { get; set; }
        /// <summary>
        /// 中心点坐标X 单位-mm
        /// </summary>
        public long X_CENTER { get; set; }
        /// <summary>
        /// 起始坐标Y 单位-mm
        /// </summary>
        public long Y_START { get; set; }
        /// <summary>
        /// 结束坐标Y 单位-mm
        /// </summary>
        public long Y_END { get; set; }
        /// <summary>
        /// 中心点坐标Y 单位-mm
        /// </summary>
        public long Y_CENTER { get; set; }
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
