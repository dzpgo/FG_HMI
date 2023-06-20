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
