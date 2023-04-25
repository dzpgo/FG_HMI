using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using static UACSDAL.AreaInfo;

namespace UACSDAL.CraneMonitor
{
    /// <summary>
    /// 红绿灯数据访问类
    /// </summary>
    public class TrafficLightInfo
    {
        private string bayNo = string.Empty;
        /// <summary>
        /// 跨别
        /// </summary>
        public string BayNo
        {
            get { return bayNo; }
        }
        public void conInit(string theBayNo)
        {
            try
            {
                bayNo = theBayNo;

            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 存储每个鞍座对应的数据（字典）
        /// </summary>
        private Dictionary<string, TrafficLightBase> dicCarMessage = new Dictionary<string, TrafficLightBase>();
        public Dictionary<string, TrafficLightBase> DicCarMessage
        {
            get { return dicCarMessage; }
            set { dicCarMessage = value; }
        }



        public void GetParkingMessage()
        {
            bayNo = "A";
            try
            {
                string sql = null;
                sql = "SELECT AREA_NO,X_START,X_END,Y_START,Y_END,AREA_TYPE,AREA_NAME FROM UACS_YARDMAP_TRAFFICLIGHT_DEFINE";

                //if (areaTypeS == AreaType.CarArea)
                //{
                //    sql += " WHERE BAY_NO LIKE '" + bayNo + "%'  and AREA_TYPE = 1";
                //}
                //else if (areaTypeS == AreaType.StockArea)
                //{
                //    sql += " WHERE BAY_NO LIKE '" + bayNo + "%'  and AREA_TYPE = 0";
                //}
                //else if (areaTypeS == AreaType.UnitArea)
                //{
                //    sql += " WHERE BAY_NO LIKE '" + bayNo + "%'  and AREA_TYPE in (4,5)";  //机组出入口
                //}
                //else if (areaTypeS == AreaType.NoStockAndRestsType)
                //{
                //    sql += " WHERE BAY_NO LIKE '" + bayNo + "%'  and AREA_TYPE != 0";
                //}
                //else
                //{
                //    sql += " WHERE BAY_NO LIKE '" + bayNo + "%' ";
                //}

                sql += " WHERE BAY_NO LIKE '" + bayNo + "%' AND AREA_TYPE = '0' ";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        TrafficLightBase trafficLightInfo = new TrafficLightBase();
                        if (rdr["AREA_NO"] != System.DBNull.Value)
                        {
                            trafficLightInfo.AreaNo = Convert.ToString(rdr["AREA_NO"]);
                        }
                        if (rdr["X_START"] != System.DBNull.Value)
                        {
                            //theArea.X_Start = Convert.ToInt32(rdr["X_START"]) - 1000 ;
                            trafficLightInfo.X_Start = Convert.ToInt32(rdr["X_START"]);
                        }
                        if (rdr["X_END"] != System.DBNull.Value)
                        {
                            trafficLightInfo.X_End = Convert.ToInt32(rdr["X_END"]);
                        }
                        if (rdr["Y_START"] != System.DBNull.Value)
                        {
                            trafficLightInfo.Y_Start = Convert.ToInt32(rdr["Y_START"]);
                        }
                        if (rdr["Y_END"] != System.DBNull.Value)
                        {
                            trafficLightInfo.Y_End = Convert.ToInt32(rdr["Y_END"]);
                        }
                        if (rdr["AREA_TYPE"] != System.DBNull.Value)
                        {
                            trafficLightInfo.AreaType = Convert.ToInt32(rdr["AREA_TYPE"]);
                        }
                        if (rdr["AREA_NAME"] != System.DBNull.Value)
                        {
                            trafficLightInfo.Area_Name = Convert.ToString(rdr["AREA_NAME"]);
                        }
                        //if (rdr["PARKING_NO"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.Name = rdr["PARKING_NO"].ToString();
                        //}
                        //if (rdr["X_START"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.X_START = Convert.ToInt32(rdr["X_START"]);
                        //}
                        //if (rdr["X_END"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.X_END = Convert.ToInt32(rdr["X_END"]);
                        //}
                        //if (rdr["Y_START"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.Y_START = Convert.ToInt32(rdr["Y_START"]);
                        //}
                        //if (rdr["Y_END"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.Y_END = Convert.ToInt32(rdr["Y_END"]);
                        //}
                        //if (rdr["X_CENTER"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.X_CENTER = Convert.ToInt32(rdr["X_CENTER"]);
                        //}
                        //if (rdr["Y_CENTER"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.Y_CENTER = Convert.ToInt32(rdr["Y_CENTER"]);
                        //}
                        //if (rdr["X_LENGTH"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.X_LENGTH = Convert.ToInt32(rdr["X_LENGTH"]);
                        //}
                        //if (rdr["Y_LENGTH"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.Y_LENGTH = Convert.ToInt32(rdr["Y_LENGTH"]);
                        //}
                        //if (rdr["HEAD_POSTION"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.HeadPostion = rdr["HEAD_POSTION"].ToString();
                        //}
                        //if (rdr["WORK_STATUS"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.PackingStatus = Convert.ToInt32(rdr["WORK_STATUS"]);
                        //}
                        //if (rdr["CAR_NO"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.Car_No = rdr["CAR_NO"].ToString();
                        //}
                        //if (rdr["TREATMENT_NO"] != DBNull.Value)
                        //{
                        //    trafficLightInfo.TREATMENT_NO = rdr["TREATMENT_NO"].ToString();
                        //}
                        trafficLightInfo.CarLength = trafficLightInfo.Y_End - trafficLightInfo.Y_Start;
                        trafficLightInfo.CarWidth = trafficLightInfo.X_End - trafficLightInfo.X_Start;
                        dicCarMessage[trafficLightInfo.AreaNo] = trafficLightInfo;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetParkingMessage2()
        {
            TrafficLightBase trafficLightInfo = new TrafficLightBase();
            trafficLightInfo.AreaNo = "A1";
            trafficLightInfo.X_Start = 5084;
            trafficLightInfo.X_End = 20035;
            trafficLightInfo.Y_Start = 1000;
            trafficLightInfo.Y_End = 32000;
            trafficLightInfo.AreaType = 0;
            trafficLightInfo.Area_Name = "A1";
            trafficLightInfo.CarLength = trafficLightInfo.Y_End - trafficLightInfo.Y_Start;
            trafficLightInfo.CarWidth = trafficLightInfo.X_End - trafficLightInfo.X_Start;
            dicCarMessage[trafficLightInfo.AreaNo] = trafficLightInfo;


            TrafficLightBase trafficLightInfo2 = new TrafficLightBase();
            trafficLightInfo2.AreaNo = "A2";
            trafficLightInfo2.X_Start = 20465;
            trafficLightInfo2.X_End = 33389;
            trafficLightInfo2.Y_Start = 1000;
            trafficLightInfo2.Y_End = 32000;
            trafficLightInfo2.AreaType = 0;
            trafficLightInfo2.Area_Name = "A2";
            trafficLightInfo2.CarLength = trafficLightInfo2.Y_End - trafficLightInfo2.Y_Start;
            trafficLightInfo2.CarWidth = trafficLightInfo2.X_End - trafficLightInfo2.X_Start;
            dicCarMessage[trafficLightInfo2.AreaNo] = trafficLightInfo2;
        }

        public List<TrafficLightBase> GetYardmapArea()
        {
            bayNo = "A";
            try
            {
                List<TrafficLightBase> list = new List<TrafficLightBase>();
                string sql = null;
                sql = "SELECT AREA_NO,X_START,X_END,Y_START,Y_END,AREA_TYPE,AREA_NAME FROM UACS_YARDMAP_AREA_DEFINE";
                sql += " WHERE BAY_NO LIKE '" + bayNo + "%' ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        TrafficLightBase trafficLightInfo = new TrafficLightBase();
                        if (rdr["AREA_NO"] != System.DBNull.Value)
                        {
                            trafficLightInfo.AreaNo = Convert.ToString(rdr["AREA_NO"]);
                        }
                        if (rdr["X_START"] != System.DBNull.Value)
                        {
                            //theArea.X_Start = Convert.ToInt32(rdr["X_START"]) - 1000 ;
                            trafficLightInfo.X_Start = Convert.ToInt32(rdr["X_START"]);
                        }
                        if (rdr["X_END"] != System.DBNull.Value)
                        {
                            trafficLightInfo.X_End = Convert.ToInt32(rdr["X_END"]);
                        }
                        if (rdr["Y_START"] != System.DBNull.Value)
                        {
                            trafficLightInfo.Y_Start = Convert.ToInt32(rdr["Y_START"]);
                        }
                        if (rdr["Y_END"] != System.DBNull.Value)
                        {
                            trafficLightInfo.Y_End = Convert.ToInt32(rdr["Y_END"]);
                        }
                        if (rdr["AREA_TYPE"] != System.DBNull.Value)
                        {
                            trafficLightInfo.AreaType = Convert.ToInt32(rdr["AREA_TYPE"]);
                        }
                        if (rdr["AREA_NAME"] != System.DBNull.Value)
                        {
                            trafficLightInfo.Area_Name = Convert.ToString(rdr["AREA_NAME"]);
                        }
                        trafficLightInfo.CarLength = trafficLightInfo.Y_End - trafficLightInfo.Y_Start;
                        trafficLightInfo.CarWidth = trafficLightInfo.X_End - trafficLightInfo.X_Start;
                        list.Add(trafficLightInfo);
                        //dicCarMessage[trafficLightInfo.AreaNo] = trafficLightInfo;
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
