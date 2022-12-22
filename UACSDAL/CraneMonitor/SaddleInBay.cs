using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace UACSDAL
{
    /// <summary>
    /// 鞍座处理数据类
    /// </summary>
    public class SaddleInBay
    {
        Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        private string bayNo = string.Empty;
        /// <summary>
        /// 跨别
        /// </summary>
        public string BayNo
        {
            get { return bayNo; }
        }


        private string myBayNo = null;

        public string MyBayNo
        {
            get { return myBayNo; }
            set { myBayNo = value; }
        }
        
        public void conInit(string theBayNO, string tagServiceName,string theMyBayNo = null)
        {
            try
            {
                bayNo = theBayNO;
                myBayNo = theMyBayNo;
                tagDataProvider.ServiceName = tagServiceName;
            }
            catch (Exception ex)
            {
            }
        }

        private Dictionary<string, SaddleBase> dicSaddles = new Dictionary<string, SaddleBase>();
        public Dictionary<string, SaddleBase> DicSaddles
        {
            get { return dicSaddles; }
            set { dicSaddles = value; }
        }

        private Dictionary<string, GRIDBase> dicGRID = new Dictionary<string, GRIDBase>();
        public Dictionary<string, GRIDBase> DicGRID
        {
            get { return dicGRID; }
            set { dicGRID = value; }
        }

        //public void getSaddleInfo()
        //{
        //    try
        //    {
        //        string sql = null;
        //        sql = @"SELECT b.STOCK_NO,b.STOCK_NAME,a.SADDLE_SEQ,a.ROW_NO,a.COL_NO,a.X_CENTER,a.Y_CENTER,a.Z_CENTER,a.SADDLE_WIDTH,a.SADDLE_LENGTH,a.LAYER_NUM,b.STOCK_STATUS,b.LOCK_FLAG,b.MAT_NO,d.PLASTIC_FLAG,e.PRODUCT_TIME,e.L3_COIL_STATUS  FROM  ";
        //        sql += " UACS_YARDMAP_SADDLE_DEFINE a inner join UACS_YARDMAP_SADDLE_STOCK c on a.SADDLE_NO = c.SADDLE_NO ";
        //        sql += " inner join UACS_YARDMAP_STOCK_DEFINE b on  c.STOCK_NO = b.STOCK_NO left join UACS_YARDMAP_COIL_PLASTIC d on  b.MAT_NO = d.COIL_NO left join UACS_YARDMAP_COIL e on b.MAT_NO = e.COIL_NO";
        //        sql += "  inner join UACS_YARDMAP_STOCK_DEFINE b on  c.STOCK_NO = b.STOCK_NO left join UACS_YARDMAP_COIL_PLASTIC d on  b.MAT_NO = d.COIL_NO left join UACS_YARDMAP_COIL e on b.MAT_NO = e.COIL_NO ";

        //        using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
        //        {
        //            while (rdr.Read())
        //            {
        //                SaddleBase theSaddle = new SaddleBase();
        //                if (rdr["STOCK_NO"] != System.DBNull.Value) 
        //                {
        //                    theSaddle.SaddleNo = Convert.ToString(rdr["STOCK_NO"]); 
        //                }
        //                if (rdr["LAYER_NUM"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Layer_Num = Convert.ToInt32(rdr["LAYER_NUM"]);
        //                }
        //                if (rdr["STOCK_NAME"] != System.DBNull.Value)
        //                {
        //                    theSaddle.SaddleName = Convert.ToString(rdr["STOCK_NAME"]);
        //                }
        //                if (rdr["SADDLE_SEQ"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Saddle_Seq = Convert.ToInt32(rdr["SADDLE_SEQ"]);
        //                }
        //                if (rdr["ROW_NO"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Row_No = Convert.ToInt32(rdr["ROW_NO"]);
        //                }
        //                if (rdr["COL_NO"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Col_No = Convert.ToInt32(rdr["COL_NO"]);
        //                }
        //                if (rdr["X_CENTER"] != System.DBNull.Value)
        //                {
        //                    theSaddle.X_Center = Convert.ToInt32(rdr["X_CENTER"]);
        //                }
        //                if (rdr["Y_CENTER"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Y_Center = Convert.ToInt32(rdr["Y_CENTER"]);
        //                }
        //                if (rdr["Z_CENTER"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Z_Center = Convert.ToInt32(rdr["Z_CENTER"]);
        //                }
        //                if (rdr["STOCK_STATUS"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Stock_Status = Convert.ToInt32(rdr["STOCK_STATUS"]);
        //                }

        //                if (rdr["LOCK_FLAG"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Lock_Flag = Convert.ToInt32(rdr["LOCK_FLAG"]);
        //                }
        //                if (rdr["SADDLE_WIDTH"] != System.DBNull.Value)
        //                {
        //                    theSaddle.SaddleWidth = Convert.ToInt32(rdr["SADDLE_WIDTH"]);
        //                }
        //                if (rdr["SADDLE_LENGTH"] != System.DBNull.Value)
        //                {
        //                    theSaddle.SaddleLength = Convert.ToInt32(rdr["SADDLE_LENGTH"]);
        //                }
        //                if (rdr["MAT_NO"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Mat_No = Convert.ToString(rdr["MAT_NO"]);

        //                    theSaddle.Next_Unit_No = GetNextUnitNOByCoil(Convert.ToString(rdr["MAT_NO"]));
        //                }
        //                if (rdr["PLASTIC_FLAG"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Plastic_Flag = Convert.ToInt32(rdr["PLASTIC_FLAG"]);
        //                }
        //                if (rdr["PRODUCT_TIME"] != System.DBNull.Value)
        //                {
        //                    theSaddle.Product_Time = Convert.ToString(rdr["PRODUCT_TIME"]);
        //                }
        //                if (rdr["L3_COIL_STATUS"] != System.DBNull.Value)
        //                {
        //                    theSaddle.L3_coil_Status1 = Convert.ToString(rdr["L3_COIL_STATUS"]);
        //                }
        //                dicSaddles[theSaddle.SaddleNo] = theSaddle;
        //            }
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show(er.Message);
        //    }

        //}

        /// <summary>
        /// 查询图库GRID定义表和图库AREA定义表、废钢品种物料名信息表
        /// </summary>
        /// <param name="AreaNo">区域号</param>
        /// <param name="isGRID_DIV">是否切换展示模式</param>
        public void getSaddleInfo(string AreaNo, bool isGRID_DIV)
        {
            try
            {                
                var GRID_NO = "";   //料格号
                var GRID_DIV = -1;  //料格区分 0:全格 1:南北划分 2:田字划分
                string sql = null;
                sql = @"SELECT B.AREA_NO,B.AREA_NAME, C.MAT_CNAME,A.GRID_NO,A.GRID_NAME,A.GRID_DIV,A.GRID_TYPE,A.X_START,A.X_END,A.X_CENTER,A.Y_START,A.Y_END,A.Y_CENTER,A.MAT_CODE,A.COMP_CODE,A.MAT_WGT,A.GRID_STATUS,A.GRID_ENABLE,A.YARD_NO,A.BAY_NO ";
                sql += "FROM UACS_YARDMAP_GRID_DEFINE A ";
                sql += "INNER JOIN UACS_YARDMAP_AREA_DEFINE B ON A.AREA_NO = B.AREA_NO ";
                sql += "LEFT JOIN UACS_L3_MAT_INFO C ON A.MAT_CODE = C.MAT_CODE ";
                if (!string.IsNullOrEmpty(AreaNo))
                    sql += "WHERE A.AREA_NO = '" + AreaNo + "'";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        SaddleBase theGRID = new SaddleBase();
                        if (rdr["GRID_NO"] != System.DBNull.Value)
                        {
                            theGRID.GRID_NO = Convert.ToString(rdr["GRID_NO"]);
                            if (theGRID.GRID_NO.Length >= 4)
                            {
                                string[] array = theGRID.GRID_NO.Split('-');  // 01-0 分割截取横杠后第4位为0的数                          
                                if (array.Length > 1 && array[1].Equals("0"))
                                {
                                    GRID_NO = Convert.ToString(rdr["GRID_NO"]);
                                    if (rdr["GRID_DIV"] != System.DBNull.Value)
                                    {
                                        GRID_DIV = Convert.ToInt32(rdr["GRID_DIV"]);
                                        if (!GRID_DIV.Equals(0))
                                        {
                                            continue; //结束本次循环，执行下次循环
                                        }
                                    }
                                }
                                else
                                {
                                    if (!GRID_DIV.Equals(Convert.ToInt32(rdr["GRID_DIV"])))
                                    {
                                        continue; //结束本次循环，执行下次循环
                                    }
                                }
                            }
                        }
                        if (rdr["GRID_NAME"] != System.DBNull.Value)
                        {
                            theGRID.GRID_NAME = Convert.ToString(rdr["GRID_NAME"]);
                        }
                        if (rdr["AREA_NO"] != System.DBNull.Value)
                        {
                            theGRID.AREA_NO = Convert.ToString(rdr["AREA_NO"]);
                        }
                        if (rdr["AREA_NAME"] != System.DBNull.Value)
                        {
                            theGRID.AREA_NAME = Convert.ToString(rdr["AREA_NAME"]);
                        }
                        if (rdr["MAT_CNAME"] != System.DBNull.Value)
                        {
                            theGRID.MAT_CNAME = Convert.ToString(rdr["MAT_CNAME"]);
                        }
                        if (rdr["GRID_DIV"] != System.DBNull.Value)
                        {
                            theGRID.GRID_DIV = Convert.ToString(rdr["GRID_DIV"]);
                        }
                        if (rdr["GRID_TYPE"] != System.DBNull.Value)
                        {
                            theGRID.GRID_TYPE = Convert.ToString(rdr["GRID_TYPE"]);
                        }
                        if (rdr["X_START"] != System.DBNull.Value)
                        {
                            theGRID.X_START = Convert.ToInt32(rdr["X_START"]);
                        }
                        if (rdr["X_END"] != System.DBNull.Value)
                        {
                            theGRID.X_END = Convert.ToInt32(rdr["X_END"]);
                        }
                        if (rdr["X_CENTER"] != System.DBNull.Value)
                        {
                            theGRID.X_CENTER = Convert.ToInt32(rdr["X_CENTER"]);
                        }
                        if (rdr["Y_START"] != System.DBNull.Value)
                        {
                            theGRID.Y_START = Convert.ToInt32(rdr["Y_START"]);
                        }
                        if (rdr["Y_END"] != System.DBNull.Value)
                        {
                            theGRID.Y_END = Convert.ToInt32(rdr["Y_END"]);
                        }
                        if (rdr["Y_CENTER"] != System.DBNull.Value)
                        {
                            theGRID.Y_CENTER = Convert.ToInt32(rdr["Y_CENTER"]);
                        }
                        if (rdr["MAT_CODE"] != System.DBNull.Value)
                        {
                            theGRID.MAT_CODE = Convert.ToString(rdr["MAT_CODE"]);
                        }
                        if (rdr["COMP_CODE"] != System.DBNull.Value)
                        {
                            theGRID.COMP_CODE = Convert.ToString(rdr["COMP_CODE"]);
                        }
                        if (rdr["MAT_WGT"] != System.DBNull.Value)
                        {
                            theGRID.MAT_WGT = Convert.ToInt32(rdr["MAT_WGT"]);
                        }
                        if (rdr["GRID_STATUS"] != System.DBNull.Value)
                        {
                            theGRID.GRID_STATUS = Convert.ToString(rdr["GRID_STATUS"]);
                        }
                        if (rdr["GRID_ENABLE"] != System.DBNull.Value)
                        {
                            theGRID.GRID_ENABLE = Convert.ToString(rdr["GRID_ENABLE"]);
                        }
                        if (rdr["YARD_NO"] != System.DBNull.Value)
                        {
                            theGRID.YARD_NO = Convert.ToString(rdr["YARD_NO"]);
                        }
                        if (rdr["BAY_NO"] != System.DBNull.Value)
                        {
                            theGRID.BAY_NO = Convert.ToString(rdr["BAY_NO"]);
                        }

                        dicSaddles[theGRID.GRID_NO] = theGRID;
                    }
                }

                //更新料格区分 0:全格 1:南北划分 2:田字划分
                if (isGRID_DIV && !string.IsNullOrEmpty(GRID_NO))
                {
                    if (GRID_DIV.Equals(0))
                    {
                        GRID_DIV++;
                    }
                    else if (GRID_DIV.Equals(1))
                    {
                        GRID_DIV++;
                    }
                    else if (GRID_DIV.Equals(2))
                    {
                        GRID_DIV = 0;
                    }
                    else
                    {
                        GRID_DIV = 0;
                    }
                    string upsql = null;
                    upsql = "UPDATE UACS_YARDMAP_GRID_DEFINE ";
                    upsql += "SET GRID_DIV = '" + GRID_DIV + "' ";
                    upsql += "WHERE AREA_NO = '" + AreaNo + "' ";
                    upsql += "AND GRID_NO = '" + GRID_NO + "' ";
                    DB2Connect.DBHelper.ExecuteNonQuery(upsql);
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }


        /// <summary>
        /// 查询机组鞍座数据信息(true 为出口，false 为入口)
        /// </summary>
        /// <param name="flag">true 为出口;false 为入口</param>
        public void getUnitSaddleData(bool flag)
        {
            string sql = null;
            try
            {
                if (flag == true)
                {
                    sql = @"SELECT  a.SADDLE_NO,a.SADDLE_NAME,a.X_CENTER,a.Y_CENTER,a.Z_CENTER,a.SADDLE_WIDTH,a.SADDLE_LENGTH,b.TAG_ISEMPTY,b.TAG_LOCK_REQUEST,b.TAG_ISLOCKED,c.COIL_NO FROM 
                        UACS_YARDMAP_SADDLE_DEFINE a 
                        inner join UACS_LINE_SADDLE_DEFINE b on a.SADDLE_NO = b.STOCK_NO  
                        inner join UACS_LINE_EXIT_L2INFO c ON b.UNIT_NO = c.UNIT_NO and  b.SADDLE_L2NAME = c.SADDLE_L2NAME
                        inner join UACS_YARDMAP_SADDLE_STOCK d ON a.SADDLE_NO = d.SADDLE_NO 
                        inner join UACS_YARDMAP_STOCK_DEFINE e ON e.STOCK_NO = d.STOCK_NO 
                        WHERE a.COL_ROW_NO LIKE '%" + bayNo + "%' and e.BAY_NO = '" + myBayNo + "'";
                    //数据存在问题  需要过滤
                    //sql += "  AND a.SADDLE_NO != 'D271VC1A02' AND a.SADDLE_NO != 'D271VC1A03' ";
                }
                else if (flag == false)
                {
                    sql = @"SELECT  a.SADDLE_NO,a.SADDLE_NAME,a.X_CENTER,a.Y_CENTER,a.Z_CENTER,a.SADDLE_WIDTH,a.SADDLE_LENGTH,b.TAG_ISEMPTY,b.TAG_LOCK_REQUEST,b.TAG_ISLOCKED,c.COIL_NO FROM UACS_YARDMAP_SADDLE_DEFINE a 
                        inner join UACS_LINE_SADDLE_DEFINE b on a.SADDLE_NO = b.STOCK_NO  
                        inner join UACS_LINE_ENTRY_L2INFO c ON b.UNIT_NO = c.UNIT_NO and  b.SADDLE_L2NAME = c.SADDLE_L2NAME
                        inner join UACS_YARDMAP_SADDLE_STOCK d ON a.SADDLE_NO = d.SADDLE_NO 
                        inner join UACS_YARDMAP_STOCK_DEFINE e ON e.STOCK_NO = d.STOCK_NO 
                        WHERE a.COL_ROW_NO LIKE '" + bayNo + "%' and e.BAY_NO = '" + myBayNo + "'";
                    //数据存在问题  需要过滤
                   // sql += "  AND a.SADDLE_NO != 'D271VR1A03'  ";
                }

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        SaddleBase theSaddle = new SaddleBase();
                        if (rdr["SADDLE_NO"] != System.DBNull.Value)
                        {
                            theSaddle.SaddleNo = Convert.ToString(rdr["SADDLE_NO"]);
                        }
                        else
                        {
                            theSaddle.SaddleNo = string.Empty;
                        }
                        if (rdr["SADDLE_NAME"] != System.DBNull.Value)
                        {
                            theSaddle.SaddleName = Convert.ToString(rdr["SADDLE_NAME"]);
                        }
                        else
                        {
                            theSaddle.SaddleName = string.Empty;
                        }
                        if (rdr["X_CENTER"] != System.DBNull.Value)
                        {
                            theSaddle.X_Center = Convert.ToInt32(rdr["X_CENTER"]);
                        }
                        else
                        {
                            theSaddle.X_Center = 0;
                        }
                        if (rdr["Y_CENTER"] != System.DBNull.Value)
                        {
                            theSaddle.Y_Center = Convert.ToInt32(rdr["Y_CENTER"]);
                        }
                        else
                        {
                            theSaddle.Y_Center = 0;
                        }
                        if (rdr["Z_CENTER"] != System.DBNull.Value)
                        {
                            theSaddle.Z_Center = Convert.ToInt32(rdr["Z_CENTER"]);
                        }
                        else
                        {
                            theSaddle.Z_Center = 0;
                        }
                        if (rdr["SADDLE_WIDTH"] != System.DBNull.Value)
                        {
                            theSaddle.SaddleWidth = Convert.ToInt32(rdr["SADDLE_WIDTH"]);
                        }
                        else
                        {
                            theSaddle.SaddleWidth = 0;
                        }
                        if (rdr["SADDLE_NO"].ToString().Contains("C") || rdr["SADDLE_NO"].ToString().Contains("R") )
                        {
                            theSaddle.SaddleLength = 800;
                            theSaddle.SaddleWidth = 1200;
                        }
                        else
                        {
                           
                            if (rdr["SADDLE_LENGTH"] != System.DBNull.Value)
                            {
                                theSaddle.SaddleLength = Convert.ToInt32(rdr["SADDLE_LENGTH"]);
                            }
                            else
                            {
                                theSaddle.SaddleLength = 0;
                            }
                        }
                       
                        if (rdr["TAG_ISEMPTY"] != System.DBNull.Value)
                        {
                            theSaddle.TagAdd_IsOccupied = Convert.ToString(rdr["TAG_ISEMPTY"]);
                        }
                        else
                        {
                            theSaddle.TagAdd_IsOccupied = string.Empty;
                        }

                        if (rdr["TAG_LOCK_REQUEST"] != System.DBNull.Value)
                        {
                            theSaddle.Tag_Lock_Request = Convert.ToString(rdr["TAG_LOCK_REQUEST"]);
                        }
                        else
                        {
                            theSaddle.Tag_Lock_Request = string.Empty;
                        }

                        if (rdr["TAG_ISLOCKED"] != System.DBNull.Value)
                        {
                            theSaddle.Tag_IsLocked = Convert.ToString(rdr["TAG_ISLOCKED"]);
                        }
                        else
                        {
                            theSaddle.Tag_IsLocked = string.Empty;
                        }

                        if (rdr["COIL_NO"] != System.DBNull.Value)
                        {
                            theSaddle.CoilNO = Convert.ToString(rdr["COIL_NO"]);
                            theSaddle.Coil_Open_Direction = GetCoilOpenDirection(Convert.ToString(rdr["COIL_NO"]));
                        }
                        else
                        {
                            theSaddle.CoilNO = string.Empty;
                        }

                        dicSaddles[theSaddle.SaddleNo] = theSaddle;
                    }
                }

            }
            catch (Exception er)
            {
                
                throw;
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
                foreach (SaddleBase theSaddle in dicSaddles.Values)
                {
                    TagNamelist.Add(theSaddle.TagAdd_IsOccupied);
                    TagNamelist.Add(theSaddle.Tag_Lock_Request);
                    TagNamelist.Add(theSaddle.Tag_IsLocked);
                }

                arrTagAdress = TagNamelist.ToArray<string>();
            }
            catch (Exception)
            {

                throw;
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
            try
            {
                //清空原来的map
                inDatas.Clear();
                //读所有鞍座tag点的值
                tagDataProvider.GetData(arrTagAdress, out inDatas);

                //为每个鞍座的tag点值属性赋值
                foreach (SaddleBase theSaddle in dicSaddles.Values)
                {
                    theSaddle.TagVal_IsOccupied = get_value_x(theSaddle.TagAdd_IsOccupied);
                    theSaddle.Tag_IsLocked_Value = get_value_x(theSaddle.Tag_IsLocked);
                    theSaddle.Tag_Lock_Request_Value = get_value_x(theSaddle.Tag_Lock_Request);
                }
            }
            catch (Exception ex)
            {
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
        /// 查询对应下道机组
        /// </summary>
        /// <param name="coilNo"></param>
        /// <returns></returns>
        private string GetNextUnitNOByCoil(string coilNo)
        {
            string nextUnitNO = string.Empty;
            try
            {
                string sql = @"SELECT NEXT_UNIT_NO FROM UACS_YARDMAP_COIL WHERE COIL_NO = '"+coilNo+"'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["NEXT_UNIT_NO"] != System.DBNull.Value)
                        {
                            nextUnitNO = rdr["NEXT_UNIT_NO"].ToString();
                        }
                    }
                }
            }
            catch (Exception er)
            {
                
                //throw;
            }
            return nextUnitNO;
        }

        /// <summary>
        /// 查询对应钢卷开口方向
        /// </summary>
        /// <param name="coilNo"></param>
        /// <returns></returns>
        private int GetCoilOpenDirection(string coilNo)
        {
            int coilOpenDirection = 0;
            try
            {
                string sql = @"SELECT COIL_OPEN_DIRECTION FROM UACS_YARDMAP_COIL WHERE COIL_NO = '" + coilNo + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["COIL_OPEN_DIRECTION"] != System.DBNull.Value)
                        {
                            coilOpenDirection = Convert.ToInt32(rdr["COIL_OPEN_DIRECTION"]);
                        }
                    }
                }
            }
            catch (Exception er)
            {

                //throw;
            }
            return coilOpenDirection;
        }

        /// <summary>
        /// 根据跨别查询空库位数量
        /// </summary>
        /// <param name="bayNo"></param>
        /// <returns></returns>
        public int GetBayNoCoilCount(string bayNo)
        {
            int noCoilCount = 999;
            try
            {
                string sql = @"SELECT count(*) as num FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO like '" + bayNo + "%' and STOCK_TYPE = 0 and STOCK_STATUS = 0  and  LOCK_FLAG = 0";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["num"] != System.DBNull.Value)
                        {
                            noCoilCount = Convert.ToInt32(rdr["num"]);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                
                //throw;
            }
            return noCoilCount;
        }

        /// <summary>
        /// 根据跨别查询有卷库位数量
        /// </summary>
        /// <param name="bayNo"></param>
        /// <returns></returns>
        public int GetBayHaveCoilCount(string bayNo)
        {
            int haveCoilCount = 999;
            try
            {
                string sql = @"SELECT count(*) as num FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO like '" + bayNo + "%' and STOCK_TYPE = 0 and STOCK_STATUS = 2  and  LOCK_FLAG = 0";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["num"] != System.DBNull.Value)
                        {
                            haveCoilCount = Convert.ToInt32(rdr["num"]);
                        }
                    }
                }
            }
            catch (Exception er)
            {

                //throw;
            }
            return haveCoilCount;
        }

        /// <summary>
        /// 根据跨别查询待判库位数量
        /// </summary>
        /// <param name="bayNo"></param>
        /// <returns></returns>
        public int GetBayStayCoilCount(string bayNo)
        {
            int stayCoilCount = 999;
            try
            {
                string sql = @"SELECT count(*) as num FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO like '" + bayNo + "%' and STOCK_TYPE = 0  and  LOCK_FLAG != 0";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["num"] != System.DBNull.Value)
                        {
                            stayCoilCount = Convert.ToInt32(rdr["num"]);
                        }
                    }
                }
            }
            catch (Exception er)
            {

                //throw;
            }
            return stayCoilCount;
        }
    }
}
