using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace UACSDAL
{
    /// <summary>
    /// 小区处理数据类
    /// </summary>
    public class AreaInfo
    {
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public enum AreaType
        {
            /// <summary>
            /// 全部类别
            /// </summary>
            AllType,
            /// <summary>
            /// 库内库区
            /// </summary>
            StockArea,
            /// <summary>
            /// 机组库区
            /// </summary>
            UnitArea,
            /// <summary>
            /// 卡车
            /// </summary>
            CarArea,
            /// <summary>
            /// 除了库区不显示其他都显示
            /// </summary>
            NoStockAndRestsType
        }

        /// <summary>
        /// 行车水位
        /// </summary>
        public const string tag_3_1_waterLevel_Flag = "3_1_h_water";
        public const string tag_3_2_waterLevel_Flag = "3_2_h_water";
        public const string tag_3_3_waterLevel_Flag = "3_3_h_water";
        public const string tag_3_4_waterLevel_Flag = "3_4_h_water";
        public const string tag_3_5_waterLevel_Flag = "3_5_h_water";
        public const string tag_1_waterLevel_Flag = "3_1_l_water";
        public const string tag_2_waterLevel_Flag = "3_2_l_water";
        public const string tag_3_waterLevel_Flag = "3_3_l_water";
        public const string tag_4_waterLevel_Flag = "3_4_l_water";
        public const string tag_5_waterLevel_Flag = "3_5_l_water";
        public const string tag_1_DownLoadWater = "3_1_DownLoadWater";
        public const string tag_2_DownLoadWater = "3_2_DownLoadWater";
        public const string tag_3_DownLoadWater = "3_3_DownLoadWater";
        public const string tag_4_DownLoadWater = "3_4_DownLoadWater";
        public const string tag_5_DownLoadWater = "3_5_DownLoadWater";


        private string bayNo = string.Empty;
        /// <summary>
        /// 跨别
        /// </summary>
        public string BayNo
        {
            get { return bayNo; }
        }

        private AreaType areaTypeS;
        /// <summary>
        /// 显示库区类别
        /// </summary>
        public AreaType AreaTypeS
        {
            get { return areaTypeS; }
        }

        public void conInit(string theBayNo, AreaType _areaType)
        {
            try
            {
                bayNo = theBayNo;
                areaTypeS = _areaType;

                tagDataProvider.ServiceName = "iplature";

            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 存储每个鞍座对应的数据（字典）
        /// </summary>
        private Dictionary<string, AreaBase> dicSaddles = new Dictionary<string, AreaBase>();
        public Dictionary<string, AreaBase> DicSaddles
        {
            get { return dicSaddles; }
            set { dicSaddles = value; }
        }

        /// <summary>
        /// 查找部分对应数据(不包括小区)
        /// </summary>
        public void getPortionAreaData(string _bayno)
        {

            string sql = null;

            try
            {
                // 标记用于区分小区类别
                sql = "SELECT AREA_NO,X_START,X_END,Y_START,Y_END,AREA_TYPE,AREA_NAME FROM UACS_YARDMAP_AREA_DEFINE";

                if (areaTypeS == AreaType.CarArea)
                {
                    sql += " WHERE BAY_NO LIKE '" + _bayno + "%'  and AREA_TYPE = 1";
                }
                else if (areaTypeS == AreaType.StockArea)
                {
                    sql += " WHERE BAY_NO LIKE '" + _bayno + "%'  and AREA_TYPE = 0";
                }
                else if (areaTypeS == AreaType.UnitArea)
                {
                    sql += " WHERE BAY_NO LIKE '" + _bayno + "%'  and AREA_TYPE in (4,5)";  //机组出入口
                }
                else if (areaTypeS == AreaType.NoStockAndRestsType)
                {
                    sql += " WHERE BAY_NO LIKE '" + _bayno + "%'  and AREA_TYPE != 0";
                }
                else
                {
                    sql += " WHERE BAY_NO LIKE '" + _bayno + "%' ";
                }

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["AREA_NO"] != System.DBNull.Value)
                        {
                            AreaBase theArea = new AreaBase();
                            if (rdr["AREA_NO"] != System.DBNull.Value)
                            {
                                theArea.AreaNo = Convert.ToString(rdr["AREA_NO"]);
                            }
                            if (rdr["X_START"] != System.DBNull.Value)
                            {
                                //theArea.X_Start = Convert.ToInt32(rdr["X_START"]) - 1000 ;
                                theArea.X_Start = Convert.ToInt32(rdr["X_START"]);
                            }
                            if (rdr["X_END"] != System.DBNull.Value)
                            {
                                theArea.X_End = Convert.ToInt32(rdr["X_END"]);
                            }
                            if (rdr["Y_START"] != System.DBNull.Value)
                            {
                                theArea.Y_Start = Convert.ToInt32(rdr["Y_START"]);
                            }
                            if (rdr["Y_END"] != System.DBNull.Value)
                            {
                                theArea.Y_End = Convert.ToInt32(rdr["Y_END"]);
                            }
                            if (rdr["AREA_TYPE"] != System.DBNull.Value)
                            {
                                theArea.AreaType = Convert.ToInt32(rdr["AREA_TYPE"]);
                            }
                            if (rdr["AREA_NAME"] != System.DBNull.Value)
                            {
                                theArea.Area_Name = Convert.ToString(rdr["AREA_NAME"]);
                            }

                            if (theArea.AreaType == 0)
                            {
                                theArea.SaddleNum = getAreaSaddleNum(theArea.AreaNo);
                                theArea.SaddleNoCoilNum = getAreaSaddleNoCoilNum(theArea.AreaNo);
                                theArea.SaddleCoilNum = getAreaSaddleCoilNum(theArea.AreaNo);
                                if (theArea.AreaNo.Contains("WJ"))
                                {
                                    theArea.TBayNO = theArea.AreaNo.Substring(0, 4);
                                    //theArea.TAreaNo = theArea.AreaNo.Substring(5, 2);
                                }
                                else if (theArea.AreaNo.Contains("PR"))
                                {
                                    theArea.TBayNO = theArea.AreaNo.Substring(0, 3);
                                    //theArea.TAreaNo = theArea.AreaNo.Substring(4);
                                }
                                else
                                {
                                    theArea.TBayNO = theArea.AreaNo.Substring(1);
                                    //theArea.TAreaNo = theArea.AreaNo.Substring(4);
                                }

                                StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_");
                                sbTagName_Safe.Append(theArea.TBayNO);
                                //sbTagName_Safe.Append("_");
                                //sbTagName_Safe.Append(theArea.TAreaNo);
                                theArea.AreaDoorSefeName = sbTagName_Safe.ToString();

                                StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_");
                                sbTagName_Reserve.Append(theArea.TBayNO);
                                //sbTagName_Reserve.Append("_");
                                //sbTagName_Reserve.Append(theArea.TAreaNo);
                                theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                            }
                            if (theArea.AreaType == 3)
                            {
                                //theArea.TBayNO = theArea.AreaNo.Substring(0, 4);
                                //theArea.TAreaNo = theArea.AreaNo.Substring(45);

                                //StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_");
                                //sbTagName_Safe.Append(theArea.TBayNO);
                                //sbTagName_Safe.Append("_");
                                //sbTagName_Safe.Append(theArea.TAreaNo);
                                //theArea.AreaDoorSefeName = sbTagName_Safe.ToString();

                                //StringBuilder sbTagName_Reserve = new StringBuilder("AREA_SAFE_");
                                //sbTagName_Reserve.Append(theArea.TBayNO);
                                //sbTagName_Reserve.Append("_");
                                //sbTagName_Reserve.Append(theArea.TAreaNo);
                                //sbTagName_Reserve.Append("_LOCKED");
                                //theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                if (theArea.AreaNo.Contains("XFQ"))
                                {
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_XFQ");
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();
                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_XFQ");
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                                if (theArea.AreaNo.Contains("WT"))
                                {
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_WT");
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();
                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_WT");
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                            }
                            if (theArea.AreaType == 4)
                            {
                                theArea.TBayNO = theArea.AreaNo.Substring(0, 4);
                                if (theArea.TBayNO.ToString().Trim() == "D302")
                                {
                                    theArea.TAreaNo = theArea.AreaNo.Substring(5, 2);
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_");
                                    sbTagName_Safe.Append(theArea.TBayNO);
                                    sbTagName_Safe.Append("_");
                                    //sbTagName_Safe.Append("WC");
                                    sbTagName_Safe.Append(theArea.TAreaNo);
                                    if (theArea.AreaNo == "D302-WC2")
                                    {
                                        sbTagName_Safe.Append("_Z63");
                                    }
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();

                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_SAFE_");
                                    sbTagName_Reserve.Append(theArea.TBayNO);
                                    sbTagName_Reserve.Append("_");
                                    sbTagName_Reserve.Append(theArea.TAreaNo);
                                    sbTagName_Reserve.Append("_LOCKED");
                                    if (theArea.AreaNo == "D302-WC2")
                                    {
                                        sbTagName_Reserve.Append("_Z63");
                                    }
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }

                                theArea.TAreaNo = theArea.AreaNo.Substring(0, 3);
                                if (theArea.AreaNo.ToString().Trim().Contains("YSL"))
                                {
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_YSL");
                                    //sbTagName_Safe.Append(theArea.TAreaNo);
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();

                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_YSL");
                                    //sbTagName_Reserve.Append(theArea.TBayNO);
                                    //sbTagName_Reserve.Append("_");
                                    //sbTagName_Reserve.Append(theArea.TAreaNo);
                                    //sbTagName_Reserve.Append("_LOCKED");
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }

                            }
                            if (theArea.AreaType == 1)
                            {
                                if (theArea.AreaNo.Contains("XFQ"))
                                {                                                                       
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_XFQ");                                 
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();
                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_XFQ");                                   
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                                if (theArea.AreaNo.Contains("WT"))
                                {
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_WT");
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();
                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_WT");
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                            }
                            if (theArea.AreaType == 6)
                            {
                                if (theArea.AreaNo.Contains("JQ"))
                                {
                                    StringBuilder sbTagName_Safe = new StringBuilder("by12");
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();
                                    //StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_JQ");
                                    //theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                            }
                            if (theArea.AreaType == 5)
                            {

                                theArea.TBayNO = theArea.AreaNo.Substring(0, 4);
                                if (theArea.TBayNO.ToString().Trim() == "D408" || theArea.TBayNO.ToString().Trim() == "D508" || theArea.TBayNO.ToString().Trim() == "D312" || theArea.TBayNO.ToString().Trim() == "D122")
                                {
                                    theArea.TAreaNo = theArea.AreaNo.Substring(5, 2);
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_");
                                    sbTagName_Safe.Append(theArea.TBayNO);
                                    sbTagName_Safe.Append("_");
                                    sbTagName_Safe.Append(theArea.TAreaNo);
                                    //sbTagName_Safe.Append("WR");
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();

                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_SAFE_");
                                    sbTagName_Reserve.Append(theArea.TBayNO);
                                    //sbTagName_Reserve.Append("_");
                                    //sbTagName_Reserve.Append(theArea.TAreaNo);
                                    sbTagName_Reserve.Append("_");
                                    //sbTagName_Safe.Append("WR");
                                    sbTagName_Reserve.Append(theArea.TAreaNo);
                                    sbTagName_Reserve.Append("_LOCKED");
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                                if(theArea.AreaNo.ToString().Trim() == "D102-WR")
                                {
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_D102_1-5");                                   
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();
                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_D102_1-5");                                    
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                                if (theArea.AreaNo.ToString().Trim() == "D102-WR-2")
                                {
                                    StringBuilder sbTagName_Safe = new StringBuilder("AREA_SAFE_D102_5-10");
                                    theArea.AreaDoorSefeName = sbTagName_Safe.ToString();
                                    StringBuilder sbTagName_Reserve = new StringBuilder("AREA_RESERVE_D102_5-10");
                                    theArea.AreaDoorReserveName = sbTagName_Reserve.ToString();
                                }
                            }
                            dicSaddles[theArea.AreaNo] = theArea;
                        }
                    }
                }
            }
            catch (Exception er)
            {

                throw;
            }

        }
        /// <summary>
        /// 获取库存重量 库图GRID定义表
        /// </summary>
        /// <returns></returns>
        public DataTable getGridData()
        {
            DataTable dt = new DataTable();
            Dictionary<string,string> list = new Dictionary<string, string>();
            try
            {
                var sql = "SELECT GRID_NO, GRID_NAME, GRID_DIV, GRID_STATUS, MAT_WGT, MAT_WGTTOTAL FROM UACS_YARDMAP_GRID_DEFINE";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(rdr);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }
        private string areaNo = string.Empty;
        public string AreaNo
        {
            get { return areaNo; }
        }
        private bool Flag = false;
        public void conInit(string theAreaNo, string tagServiceName, bool flag)
        {
            try
            {
                areaNo = theAreaNo;
                Flag = flag;
                //初始化Tag
                tagDataProvider.ServiceName = tagServiceName;
                //tagDP.AutoRegist = true;
                getTagNameList();
            }
            catch (Exception ex)
            {
            }
        }

        private string[] arrTagAdress;
        /// <summary>
        /// 存储所有tag点的地址
        /// </summary>
        public void getTagNameList()
        {
            try
            {
                List<string> TagNamelist = new List<string>();
                foreach (AreaBase item in dicSaddles.Values)
                {
                    if (item.AreaType == 0)
                    {
                        TagNamelist.Add(item.AreaDoorReserveName);
                        TagNamelist.Add(item.AreaDoorSefeName);
                    }
                    if (item.AreaType == 1)
                    {
                        TagNamelist.Add(item.AreaDoorReserveName);
                        TagNamelist.Add(item.AreaDoorSefeName);
                    }
                    if (item.AreaType == 3)
                    {
                        TagNamelist.Add(item.AreaDoorReserveName);
                        TagNamelist.Add(item.AreaDoorSefeName);
                    }
                    if (item.AreaType == 4)
                    {
                        TagNamelist.Add(item.AreaDoorReserveName);
                        TagNamelist.Add(item.AreaDoorSefeName);
                    }
                    if (item.AreaType == 5)
                    {
                        TagNamelist.Add(item.AreaDoorReserveName);
                        TagNamelist.Add(item.AreaDoorSefeName);
                    }
                    if (item.AreaType == 6)
                    {
                        //TagNamelist.Add(item.AreaDoorReserveName);
                        TagNamelist.Add(item.AreaDoorSefeName);
                    }

                    //TagNamelist.Add(tag_1_waterLevel_Flag);
                    //TagNamelist.Add(tag_2_waterLevel_Flag);
                    //TagNamelist.Add(tag_3_waterLevel_Flag);
                    //TagNamelist.Add(tag_4_waterLevel_Flag);
                    //TagNamelist.Add(tag_1_DownLoadWater);
                    //TagNamelist.Add(tag_2_DownLoadWater);
                    //TagNamelist.Add(tag_3_DownLoadWater);
                    //TagNamelist.Add(tag_4_DownLoadWater);
                    TagNamelist.Add(tag_3_1_waterLevel_Flag);
                    TagNamelist.Add(tag_3_2_waterLevel_Flag);
                    TagNamelist.Add(tag_3_3_waterLevel_Flag);
                    TagNamelist.Add(tag_3_4_waterLevel_Flag);
                    TagNamelist.Add(tag_3_5_waterLevel_Flag);

                    if (item.AreaNo.Contains("A"))
                    {
                        string[] words = item.AreaNo.Split('A');
                        var areaNo = words[1];
                        TagNamelist.Add("AreaReserve" + areaNo);
                        TagNamelist.Add("AreaSafe" + areaNo);
                    }
                    else if (item.AreaNo.Equals("T1"))
                    {
                        TagNamelist.Add("AreaReserve24");
                    }
                    else if (item.AreaNo.Equals("T2"))
                    {
                        TagNamelist.Add("AreaReserve25");
                    }
                    else if (item.AreaNo.Equals("T3"))
                    {
                        TagNamelist.Add("AreaReserve26");
                    }
                    else if (item.AreaNo.Equals("T4"))
                    {
                        TagNamelist.Add("AreaReserve27");
                    }
                }

                arrTagAdress = TagNamelist.ToArray<string>();
                readTags();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                throw;
            }
        }

        private void readTags()
        {
            try
            {
                inDatas.Clear();
                tagDataProvider.GetData(arrTagAdress, out inDatas);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// tag点值的map
        /// </summary>
        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

        /// <summary>
        /// 遍历tag点获取值
        /// </summary>
        public void getTagValues()
        {
            //string t = "";
            try
            {
                //清空原来的map
                inDatas.Clear();
                //读所有鞍座tag点的值
                tagDataProvider.GetData(arrTagAdress, out inDatas);


                //为每个鞍座的tag点值属性赋值
                foreach (AreaBase item in dicSaddles.Values)
                {
                    if (item.AreaType == 0)
                    {
                        item.AreaDoorSefeValue = get_value_x(item.AreaDoorSefeName);
                        //t = item.AreaDoorSefeName;
                        item.AreaDoorReserveValue = get_value_x(item.AreaDoorReserveName);
                        // t += ", " + item.AreaDoorReserveName;
                    }
                    if (item.AreaType == 1)
                    {
                        item.AreaDoorSefeValue = get_value_x(item.AreaDoorSefeName);                        
                        item.AreaDoorReserveValue = get_value_x(item.AreaDoorReserveName);                      
                    }
                    if (item.AreaType == 3)
                    {
                        item.AreaDoorSefeValue = get_value_x(item.AreaDoorSefeName);
                        //t = item.AreaDoorSefeName;
                        item.AreaDoorReserveValue = get_value_x(item.AreaDoorReserveName);
                        // t += ", " + item.AreaDoorReserveName;
                    }
                    if (item.AreaType == 4)
                    {
                        item.AreaDoorSefeValue = get_value_x(item.AreaDoorSefeName);
                        //t = item.AreaDoorSefeName;
                        item.AreaDoorReserveValue = get_value_x(item.AreaDoorReserveName);
                        // t += ", " + item.AreaDoorReserveName;
                    }
                    if (item.AreaType == 5)
                    {
                        item.AreaDoorSefeValue = get_value_x(item.AreaDoorSefeName);
                        //t = item.AreaDoorSefeName;
                        item.AreaDoorReserveValue = get_value_x(item.AreaDoorReserveName);
                        // t += ", " + item.AreaDoorReserveName;
                    }
                    if (item.AreaType == 6)
                    {
                        item.AreaDoorSefeValue = get_value_x(item.AreaDoorSefeName);
                        //item.AreaDoorReserveValue = get_value_x(item.AreaDoorReserveName);
                    }
                    if (item.AreaNo.Equals("A1"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_1);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_1);
                    }
                    else if (item.AreaNo.Equals("A2"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_2);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_2);
                    }
                    else if (item.AreaNo.Equals("A3"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_3);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_3);
                    }
                    else if (item.AreaNo.Equals("A4"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_4);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_4);
                    }
                    else if (item.AreaNo.Equals("A5"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_5);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_5);
                    }
                    else if (item.AreaNo.Equals("A6"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_6);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_6);
                    }
                    else if (item.AreaNo.Equals("A7"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_7);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_7);
                    }
                    else if (item.AreaNo.Equals("A8"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_8);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_8);
                    }
                    else if (item.AreaNo.Equals("A9"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_9);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_9);
                    }
                    else if (item.AreaNo.Equals("A10"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_10);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_10);
                    }
                    else if (item.AreaNo.Equals("A11"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_11);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_11);
                    }
                    else if (item.AreaNo.Equals("A12"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_12);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_12);
                    }
                    else if (item.AreaNo.Equals("A13"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_13);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_13);
                    }
                    else if (item.AreaNo.Equals("A14"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_14);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_14);
                    }
                    else if (item.AreaNo.Equals("A15"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_15);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_15);
                    }
                    else if (item.AreaNo.Equals("A16"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_16);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_16);
                    }
                    else if (item.AreaNo.Equals("A17"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_17);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_17);
                    }
                    else if (item.AreaNo.Equals("A18"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_18);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_18);
                    }
                    else if (item.AreaNo.Equals("A19"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_19);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_19);
                    }
                    else if (item.AreaNo.Equals("A20"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_20);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_20);
                    }
                    else if (item.AreaNo.Equals("A21"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_21);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_21);
                    }
                    else if (item.AreaNo.Equals("A22"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_22);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_22);
                    }
                    else if (item.AreaNo.Equals("A23"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_23);
                        item.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_23);
                    }
                    else if (item.AreaNo.Equals("T1"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_24);
                    }
                    else if (item.AreaNo.Equals("T2"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_25);
                    }
                    else if (item.AreaNo.Equals("T3"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_26);
                    }
                    else if (item.AreaNo.Equals("T4"))
                    {
                        item.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_27);
                    }


                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(t + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);

            }
        }
        /// <summary>
        /// 根据tag返回一个值
        /// </summary>
        /// <param name="tagName">tag名称</param>
        /// <returns></returns>
        private long get_value_x(string tagName)
        {
            long theValue = 0;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToInt32(valueObject);
            }
            catch
            {
                valueObject = null;
            }
            return theValue;
        }




        /// <summary>
        /// 读小区全部鞍座数量
        /// </summary>
        /// <param name="_AreaNo"></param>
        /// <returns></returns>
        public int getAreaSaddleNum(string _AreaNo)
        {
            int saddlenum = 0;
            try
            {
                string sql = string.Empty;
                //查询所有有鞍座坐标的鞍座数量
                sql = @"SELECT COUNT(*) as num FROM UACS_YARDMAP_STOCK_DEFINE A left join UACS_YARDMAP_SADDLE_STOCK B on A.STOCK_NO = B.STOCK_NO  
                            left join UACS_YARDMAP_SADDLE_DEFINE C on B.SADDLE_NO = C.SADDLE_NO 
                            LEFT JOIN UACS_YARDMAP_ROWCOL_DEFINE D ON D.COL_ROW_NO = C.COL_ROW_NO
                            WHERE D.AREA_NO = '" + _AreaNo + "' and C.X_CENTER > 0 ";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["num"] != DBNull.Value)
                            saddlenum = Convert.ToInt32(rdr["num"]);
                    }
                }
            }
            catch (Exception er)
            {
                return 0;
            }
            return saddlenum;
        }

        /// <summary>
        /// 读小区白库位数量
        /// </summary>
        /// <param name="_AreaNo"></param>
        /// <returns></returns>
        public int getAreaSaddleNoCoilNum(string _AreaNo)
        {
            int saddleNoCoil = 0;
            try
            {
                string sql = string.Empty;
                sql = @"SELECT  COUNT(*) as num FROM UACS_YARDMAP_STOCK_DEFINE A left join UACS_YARDMAP_SADDLE_STOCK B on A.STOCK_NO = B.STOCK_NO  
                            left join UACS_YARDMAP_SADDLE_DEFINE C on B.SADDLE_NO = C.SADDLE_NO 
                             LEFT JOIN UACS_YARDMAP_ROWCOL_DEFINE D ON D.COL_ROW_NO = C.COL_ROW_NO
                            WHERE D.AREA_NO = '" + _AreaNo + "' and C.X_CENTER > 0 AND A.STOCK_STATUS = 0 AND A.LOCK_FLAG = 0";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["num"] != DBNull.Value)
                            saddleNoCoil = Convert.ToInt32(rdr["num"]);
                    }
                }
            }
            catch (Exception er)
            {
                return 0;
            }
            return saddleNoCoil;
        }

        /// <summary>
        /// 读小区黑库位数量
        /// </summary>
        /// <param name="_AreaNo"></param>
        /// <returns></returns>
        public int getAreaSaddleCoilNum(string _AreaNo)
        {
            int saddleCoil = 0;
            try
            {
                string sql = string.Empty;
                sql = @"SELECT  COUNT(*) as num FROM UACS_YARDMAP_STOCK_DEFINE A left join UACS_YARDMAP_SADDLE_STOCK B on A.STOCK_NO = B.STOCK_NO  
                            left join UACS_YARDMAP_SADDLE_DEFINE C on B.SADDLE_NO = C.SADDLE_NO 
                             LEFT JOIN UACS_YARDMAP_ROWCOL_DEFINE D ON D.COL_ROW_NO = C.COL_ROW_NO
                            WHERE D.AREA_NO = '" + _AreaNo + "' and C.X_CENTER > 0 AND  A.STOCK_STATUS = 2 AND A.LOCK_FLAG = 0";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["num"] != DBNull.Value)
                            saddleCoil = Convert.ToInt32(rdr["num"]);
                    }
                }
            }
            catch (Exception er)
            {
                return 0;
            }
            return saddleCoil;
        }

        /// <summary>
        /// 行车水位状态
        /// </summary>
        /// <param name="crane"></param>
        /// <returns></returns>
        public bool getCraneWaterLevelStatus(string crane)
        {
            bool ret = false;
            switch (crane)
            {
                //case "1":
                //    ret = GetDaoZhaState(tag_1_waterLevel_Flag);
                //    break;
                //case "2":
                //    ret = GetDaoZhaState(tag_2_waterLevel_Flag);
                //    break;
                //case "3":
                //    ret = GetDaoZhaState(tag_3_waterLevel_Flag);
                //    break;
                //case "4":
                //    ret = GetDaoZhaState(tag_4_waterLevel_Flag);
                //    break;
                //case "5":
                //    ret = GetDaoZhaState(tag_5_waterLevel_Flag);
                //    break;
                case "3_1":
                    ret = GetDaoZhaState(tag_3_1_waterLevel_Flag);
                    break;
                case "3_2":
                    ret = GetDaoZhaState(tag_3_2_waterLevel_Flag);
                    break;
                case "3_3":
                    ret = GetDaoZhaState(tag_3_3_waterLevel_Flag);
                    break;
                case "3_4":
                    ret = GetDaoZhaState(tag_3_4_waterLevel_Flag);
                    break;
                case "3_5":
                    ret = GetDaoZhaState(tag_3_5_waterLevel_Flag);
                    break;
                //case "_1":
                //    ret = GetDaoZhaState(tag_1_DownLoadWater);
                //    break;
                //case "_2":
                //    ret = GetDaoZhaState(tag_2_DownLoadWater);
                //    break;
                //case "_3":
                //    ret = GetDaoZhaState(tag_3_DownLoadWater);
                //    break;
                //case "_4":
                //    ret = GetDaoZhaState(tag_4_DownLoadWater);
                //    break;
                //case "_5":
                //    ret = GetDaoZhaState(tag_5_DownLoadWater);
                //    break;  
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// tag点value为1是为true
        /// </summary>
        /// <param name="gate"></param>
        /// <returns></returns>
        public bool GetDaoZhaState(string gate)
        {
            bool ret = false;
            string tagName = gate;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                if (valueObject != null && valueObject.ToString() == "1")
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            return ret;
        }
    }    
}
