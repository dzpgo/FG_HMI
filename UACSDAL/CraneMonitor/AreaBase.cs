﻿using System;

namespace UACSDAL
{

    /// <summary>
    /// 小区基类
    /// </summary>
    public class AreaBase : ICloneable
    {
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public AreaBase Clone()
        {
            return (AreaBase)this.MemberwiseClone();
        }
        private string areaNo;
        /// <summary>
        /// 小区
        /// </summary>
        public string AreaNo
        {
            get { return areaNo; }
            set { areaNo = value; }
        }

        private int areaType;
        /// <summary>
        /// 小区类别
        /// </summary>
        public int AreaType
        {
            get { return areaType; }
            set { areaType = value; }
        }

        private string area_Name;
        /// <summary>
        /// 昵称
        /// </summary>
        public string Area_Name
        {
            get { return area_Name; }
            set { area_Name = value; }
        }
        

        private string bayNo;
        /// <summary>
        /// 跨别
        /// </summary>
        public string BayNo
        {
            get { return bayNo; }
            set { bayNo = value; }
        }


        private long x_Start;
        /// <summary>
        /// X起
        /// </summary>
        public long X_Start
        {
            get { return x_Start; }
            set { x_Start = value; }
        }

        private long x_End;
        /// <summary>
        /// X终
        /// </summary>
        public long X_End
        {
            get { return x_End; }
            set { x_End = value; }
        }

        private long y_Start;
        /// <summary>
        /// Y起
        /// </summary>
        public long Y_Start
        {
            get { return y_Start; }
            set { y_Start = value; }
        }

        private long y_End;
        /// <summary>
        /// Y终
        /// </summary>
        public long Y_End
        {
            get { return y_End; }
            set { y_End = value; }
        }


        private long areaWidth;
        /// <summary>
        /// 小区宽度
        /// </summary>
        public long AreaWidth
        {
            get { return areaWidth; }
            set 
            {
                areaWidth = x_End - x_Start;
            }
        }

        private long areaHeight;
        /// <summary>
        /// 小区宽度
        /// </summary>
        public long AreaHeight
        {
            get { return areaHeight; }
            set
            {
                areaHeight = y_End - y_Start;
            }
        }

        private long areaDoorSefeValue;

        public long AreaDoorSefeValue
        {
            get { return areaDoorSefeValue; }
            set { areaDoorSefeValue = value; }
        }

        private string areaDoorSefeName;

        public string AreaDoorSefeName
        {
            get { return areaDoorSefeName; }
            set { areaDoorSefeName = value; }
        }

        private long areaDoorReserveValue;

        public long AreaDoorReserveValue
        {
            get { return areaDoorReserveValue; }
            set { areaDoorReserveValue = value; }
        }

        private string areaDoorReserveName;

        public string AreaDoorReserveName
        {
            get { return areaDoorReserveName; }
            set { areaDoorReserveName = value; }
        }


        private int saddleNum;

        public int SaddleNum
        {
            get { return saddleNum; }
            set { saddleNum = value; }
        }

        private int saddleNoCoilNum;

        public int SaddleNoCoilNum
        {
            get { return saddleNoCoilNum; }
            set { saddleNoCoilNum = value; }
        }

        private int saddleCoilNum;

        public int SaddleCoilNum
        {
            get { return saddleCoilNum; }
            set { saddleCoilNum = value; }
        }


        private string tBayNO;

        public string TBayNO
        {
            get { return tBayNO; }
            set { tBayNO = value; }
        }


        private string tAreaNo;

        public string TAreaNo
        {
            get { return tAreaNo; }
            set { tAreaNo = value; }
        }
        /// <summary>
        /// 红绿灯 （1：红灯  0：绿灯）
        /// </summary>
        public long AreaReserve { get; set; }

        /// <summary>
        /// 车辆预定 （1：有车进入不安全  0：安全）
        /// </summary>
        public long AreaSafe { get; set; }

        /// <summary>
        /// 1:整个料格封红；2：整个料格解封；3：取消光电开关功能；4：光电开关投入使用；
        /// </summary>
        public long AreaGratStatus { get; set; }
    }
}
