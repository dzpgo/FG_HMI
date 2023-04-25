using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UACSDAL
{
    /// <summary>
    /// 红绿灯基类
    /// </summary>
    public class TrafficLightBase : ICloneable
    {
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public TrafficLightBase Clone()
        {
            return (TrafficLightBase)this.MemberwiseClone();
        }
        private string areaNo;
        /// <summary>
        /// 跨号
        /// </summary>
        public string AreaNo
        {
            get { return areaNo; }
            set { areaNo = value; }
        }

        private int areaType;
        /// <summary>
        /// 跨类别
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

        /// <summary>
        /// 车宽
        /// </summary>
        public long CarWidth { get; set; }

        /// <summary>
        /// 车长
        /// </summary>
        public long CarLength { get; set; }

        /// <summary>
        /// 红绿灯 （1：红灯  0：绿灯）
        /// </summary>
        public long AreaReserve { get; set; }
        /// <summary>
        /// 停车位红绿灯 （1：红灯  0：绿灯）
        /// </summary>
        public long AreaReserveCubicle { get; set; }
        /// <summary>
        /// 卸料车辆红绿灯 （1：有车进入不安全  0：安全）
        /// </summary>
        public long AreaSafe { get; set; }




        //--------------------------------------------------行车预定红绿灯状态----------------------------------------------------------------
        /// <summary>
        /// 1#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_1 = "AreaReserve1";
        /// <summary>
        /// 2#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_2 = "AreaReserve2";
        /// <summary>
        /// 3#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_3 = "AreaReserve3";
        /// <summary>
        /// 4#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_4 = "AreaReserve4";
        /// <summary>
        /// 5#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_5 = "AreaReserve5";
        /// <summary>
        /// 6#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_6 = "AreaReserve6";
        /// <summary>
        /// 7#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_7 = "AreaReserve7";
        /// <summary>
        /// 8#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_8 = "AreaReserve8";
        /// <summary>
        /// 9#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_9 = "AreaReserve9";
        /// <summary>
        /// 10#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_10 = "AreaReserve10";
        /// <summary>
        /// 11#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_11 = "AreaReserve11";
        /// <summary>
        /// 12#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_12 = "AreaReserve12";
        /// <summary>
        /// 13#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_13 = "AreaReserve13";
        /// <summary>
        /// 14#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_14 = "AreaReserve14";
        /// <summary>
        /// 15#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_15 = "AreaReserve15";
        /// <summary>
        /// 16#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_16 = "AreaReserve16";
        /// <summary>
        /// 17#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_17 = "AreaReserve17";
        /// <summary>
        /// 18#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_18 = "AreaReserve18";
        /// <summary>
        /// 19#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_19 = "AreaReserve19";
        /// <summary>
        /// 20#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_20 = "AreaReserve20";
        /// <summary>
        /// 21#预定状态（1：预定  0：取消）
        /// </#预定状态（1：预定  0：取消）>
        public const string AREA_RESERVE_21 = "AreaReserve21";
        /// <summary>
        /// 22#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_22 = "AreaReserve22";
        /// <summary>
        /// 23#预定状态（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_23 = "AreaReserve23";
        /// <summary>
        /// 1#工位预定（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_24 = "AreaReserve24";
        /// <summary>
        /// 2#工位预定（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_25 = "AreaReserve25";
        /// <summary>
        /// 3#工位预定（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_26 = "AreaReserve26";
        /// <summary>
        /// 4#工位预定（1：预定  0：取消）
        /// </summary>
        public const string AREA_RESERVE_27 = "AreaReserve27";

        //--------------------------------------------------卸料车预定红绿灯状态----------------------------------------------------------------
        /// <summary>
        /// 1#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_1 = "AreaSafe1";
        /// <summary>
        /// 2#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_2 = "AreaSafe2";
        /// <summary>
        /// 3#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_3 = "AreaSafe3";
        /// <summary>
        /// 4#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_4 = "AreaSafe4";
        /// <summary>
        /// 5#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_5 = "AreaSafe5";
        /// <summary>
        /// 6#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_6 = "AreaSafe6";
        /// <summary>
        /// 7#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_7 = "AreaSafe7";
        /// <summary>
        /// 8#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_8 = "AreaSafe8";
        /// <summary>
        /// 9#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_9 = "AreaSafe9";
        /// <summary>
        /// 10#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_10 = "AreaSafe10";
        /// <summary>
        /// 11#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_11 = "AreaSafe11";
        /// <summary>
        /// 12#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_12 = "AreaSafe12";
        /// <summary>
        /// 13#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_13 = "AreaSafe13";
        /// <summary>
        /// 14#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_14 = "AreaSafe14";
        /// <summary>
        /// 15#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_15 = "AreaSafe15";
        /// <summary>
        /// 16#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_16 = "AreaSafe16";
        /// <summary>
        /// 17#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_17 = "AreaSafe17";
        /// <summary>
        /// 18#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_18 = "AreaSafe18";
        /// <summary>
        /// 19#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_19 = "AreaSafe19";
        /// <summary>
        /// 20#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_20 = "AreaSafe20";
        /// <summary>
        /// 21#预定状态（1：有车进入不安全  0：安全）
        /// </#预定状态（1：有车进入不安全  0：安全）>
        public const string AREA_SAFE_21 = "AreaSafe21";
        /// <summary>
        /// 22#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_22 = "AreaSafe22";
        /// <summary>
        /// 23#预定状态（1：有车进入不安全  0：安全）
        /// </summary>
        public const string AREA_SAFE_23 = "AreaSafe23";


    }
}
