using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSDAL
{
    public class OffinePackingSaddleInBay
    {

        public enum AreaStatus
        {
            /// <summary>
            /// 入卷
            /// </summary>
            InCoil = 10,
            /// <summary>
            /// 包装作业中
            /// </summary>
            Working = 20,
            /// <summary>
            /// 出卷
            /// </summary>
            OutCoil = 30,
            /// <summary>
            /// 自动吊运
            /// </summary>
            Auto = 40
        }



        private Dictionary<string, OffinePackingSaddle> dicSaddles = new Dictionary<string, OffinePackingSaddle>();

        public Dictionary<string, OffinePackingSaddle> DicSaddles
        {
            get { return dicSaddles; }
            set { dicSaddles = value; }
        }


        public void ReadDefintion(string _areaNo)
        {
            try
            {
                string sql;
                dicSaddles.Clear();
                if(_areaNo.Contains("PR"))
                {
                     sql = @"SELECT a.AREA_ID,a.SUB_AREA_ID,a.STOCK_NO,a.CONFIRM_FLAG,a.COIL_NO,b.MAT_NO,b.STOCK_STATUS, b.LOCK_FLAG ,c.WEIGHT  
                  FROM UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE a ";
                }
                else if(_areaNo.Contains("WT"))
                {
                    sql = @"SELECT a.AREA_ID,a.SUB_AREA_ID,a.STOCK_NO,a.CONFIRM_FLAG,a.COIL_NO,b.MAT_NO,b.STOCK_STATUS, b.LOCK_FLAG ,c.WEIGHT  
                  FROM UACS_WATER_TANK_AREA_STOCK_DEFINE a ";
                }
                else
                {
                     sql = @"SELECT a.AREA_ID,a.SUB_AREA_ID,a.STOCK_NO,a.CONFIRM_FLAG,a.COIL_NO,b.MAT_NO,b.STOCK_STATUS, b.LOCK_FLAG ,c.WEIGHT  
                  FROM UACS_OPEN_PACKING_AREA_STOCK_DEFINE a ";
                }
                
                sql += " left join UACS_YARDMAP_STOCK_DEFINE b on a.STOCK_NO = b.STOCK_NO ";
                sql += " left join UACS_YARDMAP_COIL c on b.MAT_NO = c.COIL_NO ";
                sql += " WHERE a.AREA_ID = '" + _areaNo + "' ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        OffinePackingSaddle theSaddle = new OffinePackingSaddle();
                        if (rdr["STOCK_NO"] != DBNull.Value)
                            theSaddle.OffinePackingSaddleNo = rdr["STOCK_NO"].ToString();
                        else
                            theSaddle.OffinePackingSaddleNo = string.Empty;

                        //if (Convert.ToInt32(rdr["STOCK_STATUS"]) == 0)
                        //{
                        //    if (rdr["COIL_NO"] != DBNull.Value)
                        //        theSaddle.Coil = rdr["COIL_NO"].ToString();
                        //    else
                        //        theSaddle.Coil = string.Empty;
                        //}
                        if (Convert.ToInt32(rdr["CONFIRM_FLAG"]) == 10)
                        {
                            if (rdr["COIL_NO"] != DBNull.Value)
                                theSaddle.Coil = rdr["COIL_NO"].ToString();
                            else
                                theSaddle.Coil = string.Empty;
                        }
                        else
                        {
                            if (rdr["MAT_NO"] != DBNull.Value)
                                theSaddle.Coil = rdr["MAT_NO"].ToString();
                            else
                                theSaddle.Coil = string.Empty;
                        }
                        

                        if (rdr["STOCK_STATUS"] != DBNull.Value)
                            theSaddle.SaddleStatus = Convert.ToInt32(rdr["STOCK_STATUS"].ToString());
                        else
                            theSaddle.SaddleStatus = 0;

                        if (rdr["LOCK_FLAG"] != DBNull.Value)
                            theSaddle.LockFlag = Convert.ToInt32(rdr["LOCK_FLAG"].ToString());
                        else
                            theSaddle.LockFlag = 0;

                        if (rdr["CONFIRM_FLAG"] != DBNull.Value)
                            theSaddle.CONFIRM_FLAG = Convert.ToInt32(rdr["CONFIRM_FLAG"].ToString());
                        else
                            theSaddle.CONFIRM_FLAG = 0;

                        if (rdr["WEIGHT"] != DBNull.Value)
                            theSaddle.CoilWeight = Convert.ToInt32(rdr["WEIGHT"].ToString());
                        else
                            theSaddle.CoilWeight = 0;

                        dicSaddles[theSaddle.OffinePackingSaddleNo] = theSaddle;
                    }
                }
            }
            catch (Exception er)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 修改离线包装小区状态
        /// </summary>
        /// <param name="_status">状态值</param>
        /// <param name="_areaId">离线包装区号</param>
        /// <param name="_subAreaNo">小区号</param>
        /// <returns></returns>
        public static bool UpPackingStatus(int _status, string _areaId,string _subAreaNo)
        {
            try
            {
                string sql = @"update UACS_OFFLINE_PACKING_AREA_STATUS set STATUS = " + _status + " where AREA_ID = '" + _areaId + "' and SUB_AREA_ID = '" + _subAreaNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 修改离线包装鞍座状态
        /// </summary>
        /// <param name="_status"></param>
        /// <param name="_areaId"></param>
        /// <param name="_subAreaNo"></param>
        /// <returns></returns>
        public static bool UpPackingSaddleStatus(int _status, string _saddleNo)
        {
            try
            {
                string sql = @"update UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE set CONFIRM_FLAG = " + _status + " where STOCK_NO = '" + _saddleNo + "' ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查询离线鞍座地板
        /// 地板高度|最近动作|行车模式|行车号
        /// </summary>
        /// <param name="saddleNo"></param>
        /// <returns></returns>
        public static string GetPackingSaddleFloor(string saddleNo)
        {
            string str = null;
            try
            {
                string sql = @"SELECT FLOOR,OPER_FLAG,CONTROL_MODE,CRANE_NO FROM UACS_CRANE_STOCK_OPER_LOG WHERE STOCK_NO = '" + saddleNo + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        str = rdr["FLOOR"].ToString();
                        str += "|";
                        str += rdr["OPER_FLAG"].ToString();
                        str += "|";
                        str += rdr["CONTROL_MODE"].ToString();
                        str += "|";
                        str += rdr["CRANE_NO"].ToString();
                    }
                }

            }
            catch (Exception)
            {
                return str;
            }
            return str;
        }

        /// <summary>
        /// 查询吊入的状态信息
        /// 包装大卷标记|待吊入钢卷位置|钢卷标记
        /// </summary>
        /// <param name="saddleNo"></param>
        /// <returns></returns>
        public static string GetPackingSaddleInfo(string saddleNo)
        {
            string str = null;
            try
            {
                string sql = @"SELECT PACKING_HUGE_COIL,FORM_STOCK,COIL_TYPE FROM UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE STOCK_NO = '" + saddleNo + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["PACKING_HUGE_COIL"] != DBNull.Value)
                            str = rdr["PACKING_HUGE_COIL"].ToString();
                        else
                            str = " ";   
          
                        str += "|";

                        if (rdr["FORM_STOCK"] != DBNull.Value)
                            str += rdr["FORM_STOCK"].ToString();
                        else
                            str += " ";

                        str += "|";

                        if (rdr["COIL_TYPE"] != DBNull.Value)
                            str += rdr["COIL_TYPE"].ToString();
                        else
                            str += " ";
                    }
                }
            }
            catch (Exception)
            {
                
                 return str;
            }
            return str;
        }



        /// <summary>
        /// 修改离线包装鞍座纸的宽度
        /// </summary>
        /// <param name="_status"></param>
        /// <param name="_areaId"></param>
        /// <param name="_subAreaNo"></param>
        /// <returns></returns>
        public static bool UpPackingSaddlePlateWidth(string _width, string _saddleNo)
        {
            try
            {
                string sql = @"update UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE set PLATE_WIDTH = " + _width + " where STOCK_NO = '" + _saddleNo + "' ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置离线包装鞍座的卷属性
        /// </summary>
        /// <param name="packing_huce_coil">大卷标记</param>
        /// <param name="coil_type">卷属性</param>
        /// <returns></returns>
        public static bool UpPackingSaddleCoilType(int packing_huce_coil,string coil_type,string _saddleNo,string _packCode)
        {
            try
            {
                string sql = @"update UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE set PACKING_HUGE_COIL = " + packing_huce_coil + ", COIL_TYPE = '" + coil_type + "',PACKING_CODE = '" + _packCode + "' where STOCK_NO = '" + _saddleNo + "' ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {             
                 return false;
            }
            return true;
        }




        /// <summary>
        /// 查询离线包装小区状态
        /// </summary>
        /// <param name="_areaId"></param>
        /// <param name="_subAreaNo"></param>
        public static string GetPackingStatus( string _areaId,string _subAreaNo)
        {
            string status = null;
            try
            {
                string sql = @"SELECT  CASE
                                  WHEN STATUS = 10 THEN '入卷'
                                  WHEN STATUS = 20 THEN '包装作业中'
                                  WHEN STATUS = 30 THEN '出卷'
                                  WHEN STATUS = 40 THEN '自动吊运'
                               END as STATUS FROM UACS_OFFLINE_PACKING_AREA_STATUS WHERE AREA_ID = '" + _areaId+"' and SUB_AREA_ID = '"+_subAreaNo+"'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["STATUS"] != DBNull.Value)
                            status = rdr["STATUS"].ToString();                     
                    }
                }
            }
            catch (Exception)
            {               
               return null;
            }
            return status;
        }


        public void GetOffLinePackingStatus(string _bayNo, DataGridView _dgv)
        {
            // 标记
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT a.AREA_ID,a.SUB_AREA_ID,a.BAY_NO,
                               CASE
                                  WHEN a.STATUS = 10 THEN '入卷'
                                  WHEN a.STATUS = 20 THEN '包装作业中'
                                  WHEN a.STATUS = 30 THEN '出卷'
                               END as STATUS, 
                            b.X_MIN,b.X_MAX,b.Y_MIN,b.Y_MAX FROM UACS_OFFLINE_PACKING_AREA_STATUS a ,STRATEGY_AREA_SPECIAL b ";
                sql += " WHERE a.BAY_NO = b.BAY_NO and a.SUB_AREA_ID = b.AREA_ID and a.BAY_NO= '" + _bayNo + "' ";
                dt.Clear();
                dt = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn = true;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("AREA_ID", typeof(String));
                dt.Columns.Add("SUB_AREA_ID", typeof(String));
                dt.Columns.Add("BAY_NO", typeof(String));
                dt.Columns.Add("STATUS", typeof(String));
                dt.Columns.Add("X_MIN", typeof(String));
                dt.Columns.Add("X_MAX", typeof(String));
                dt.Columns.Add("Y_MIN", typeof(String));
                dt.Columns.Add("Y_MAX", typeof(String));
            }
            _dgv.DataSource = dt;
        }


        public void GetOffLinePackingByUnitSaddleInfo(string _UnitNo, string _bayNo, DataGridView _dgv)
        {
             // 标记
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT A.SADDLE_L2NAME,A.STOCK_NO,B.COIL_NO,C.WEIGHT,C.WIDTH,C.ACT_WEIGHT,C.ACT_WIDTH,B.IN_DIA,B.OUT_DIA, C.PACK_CODE,C.NEXT_UNIT_NO,
                               CASE
                                  WHEN C.FORBIDEN_FLAG = 0 THEN '正常'
                                  WHEN C.FORBIDEN_FLAG = 1 THEN '封闭'
                               END as FORBIDEN_FLAG,C.PACK_FLAG FROM UACS_LINE_SADDLE_DEFINE A LEFT JOIN UACS_LINE_EXIT_L2INFO B ON A.UNIT_NO = B.UNIT_NO AND A.SADDLE_L2NAME = B.SADDLE_L2NAME 
                               LEFT JOIN UACS_YARDMAP_COIL C ON C.COIL_NO = B.COIL_NO  left join UACS_YARDMAP_STOCK_DEFINE D ON A.STOCK_NO = D.STOCK_NO left join UACS_YARDMAP_SADDLE_STOCK E on D.STOCK_NO = E.STOCK_NO left join UACS_YARDMAP_SADDLE_DEFINE F on E.SADDLE_NO = F.SADDLE_NO
                               WHERE A.UNIT_NO = '" + _UnitNo + "' AND A.FLAG_UNIT_EXIT = 1 AND D.BAY_NO = '" + _bayNo + "' ORDER BY A.STOCK_NO ";
                dt.Clear();
                dt = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn = true;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("SADDLE_L2NAME", typeof(String));
                dt.Columns.Add("STOCK_NO", typeof(String));
                dt.Columns.Add("COIL_NO", typeof(String));
                dt.Columns.Add("WEIGHT", typeof(String));
                dt.Columns.Add("WIDTH", typeof(String));
                dt.Columns.Add("ACT_WEIGHT", typeof(String));
                dt.Columns.Add("ACT_WIDTH", typeof(String));
                dt.Columns.Add("IN_DIA", typeof(String));
                dt.Columns.Add("OUT_DIA", typeof(String));
                dt.Columns.Add("PACK_CODE", typeof(String));
            }
            _dgv.DataSource = dt;
        }

        public void GetUnPackingByUnitSaddleInfo(string _UnitNo, string _bayNo, DataGridView _dgv)
        {
            // 标记
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT A.SADDLE_L2NAME,A.STOCK_NO,B.COIL_NO,C.WEIGHT,C.WIDTH,C.INDIA,C.OUTDIA, C.PACK_CODE,C.NEXT_UNIT_NO,
                               CASE
                                  WHEN C.FORBIDEN_FLAG = 0 THEN '正常'
                                  WHEN C.FORBIDEN_FLAG = 1 THEN '封闭'
                               END as FORBIDEN_FLAG,C.PACK_FLAG FROM UACS_LINE_SADDLE_DEFINE A LEFT JOIN UACS_LINE_ENTRY_L2INFO B ON A.UNIT_NO = B.UNIT_NO AND A.SADDLE_L2NAME = B.SADDLE_L2NAME 
                               LEFT JOIN UACS_YARDMAP_COIL C ON C.COIL_NO = B.COIL_NO  left join UACS_YARDMAP_STOCK_DEFINE D ON A.STOCK_NO = D.STOCK_NO left join UACS_YARDMAP_SADDLE_STOCK E on D.STOCK_NO = E.STOCK_NO left join UACS_YARDMAP_SADDLE_DEFINE F on F.SADDLE_NO = E.SADDLE_NO
                               WHERE A.UNIT_NO = '" + _UnitNo + "' AND A.FLAG_UNIT_EXIT = 0 AND D.BAY_NO = '" + _bayNo + "' and F.X_CENTER > 0  ORDER BY A.STOCK_NO ";
                dt.Clear();
                dt = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn = true;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("SADDLE_L2NAME", typeof(String));
                dt.Columns.Add("STOCK_NO", typeof(String));
                dt.Columns.Add("COIL_NO", typeof(String));
                dt.Columns.Add("WEIGHT", typeof(String));
                dt.Columns.Add("WIDTH", typeof(String));
                dt.Columns.Add("IN_DIA", typeof(String));
                dt.Columns.Add("OUT_DIA", typeof(String));
                dt.Columns.Add("PACK_CODE", typeof(String));
            }
            _dgv.DataSource = dt;
        }

        public void GetOffLinePackingByZ34034Info(DataGridView _dgv,string _stockNo)
        {
            // 标记
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select a.STOCK_NO,a.MAT_NO,b.WEIGHT,b.WIDTH,b.ACT_WEIGHT,b.ACT_WIDTH,b.INDIA,b.OUTDIA,b.PACK_CODE,b.NEXT_UNIT_NO,                              
                               CASE
                                  WHEN b.FORBIDEN_FLAG = 0 THEN ''
                                  WHEN b.FORBIDEN_FLAG = 1 THEN '封闭'
                               END as FORBIDEN_FLAG,
                               CASE
                                  WHEN b.DUMMY_COIL_FLAG = 1 THEN '返回'
                                  ELSE ''
                               END as DUMMY_COIL_FLAG,";
                               
                if (_stockNo.Contains("PR"))
                {
                    if (_stockNo.Contains("D112"))
                    {
                        sql += "CASE WHEN c.CONFIRM_FLAG = 10 THEN '吊入' WHEN c.CONFIRM_FLAG = 20 THEN '包装' WHEN c.CONFIRM_FLAG = 30 THEN '吊离' WHEN c.CONFIRM_FLAG = 40 THEN '待包卷吊入' ELSE '' END as CONFIRM_FLAG from UACS_YARDMAP_STOCK_DEFINE a left join UACS_YARDMAP_COIL b on a.MAT_NO = b.COIL_NO left join UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE c on a.STOCK_NO = c.STOCK_NO where (a.STOCK_NO = 'D112PR0049' OR a.STOCK_NO = 'D112PR0050' OR a.STOCK_NO = 'D112PR0051') order by a.STOCK_NO";
                    }
                    else
                    {
                        sql += "CASE WHEN c.CONFIRM_FLAG = 10 THEN '吊入' WHEN c.CONFIRM_FLAG = 20 THEN '包装' WHEN c.CONFIRM_FLAG = 30 THEN '吊离' WHEN c.CONFIRM_FLAG = 40 THEN '待包卷吊入' ELSE '' END as CONFIRM_FLAG from UACS_YARDMAP_STOCK_DEFINE a left join UACS_YARDMAP_COIL b on a.MAT_NO = b.COIL_NO left join UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE c on a.STOCK_NO = c.STOCK_NO where a.STOCK_NO like '" + _stockNo + "%' order by a.STOCK_NO";
                    }                                                                                    
                }
                if (_stockNo.Contains("PC"))
                {
                    sql += "CASE WHEN c.CONFIRM_FLAG = 10 THEN '吊入' WHEN c.CONFIRM_FLAG = 20 THEN '修复中' WHEN c.CONFIRM_FLAG = 30 THEN '吊离' WHEN c.CONFIRM_FLAG = 40 THEN '拆包卷吊入' ELSE '' END as CONFIRM_FLAG from UACS_YARDMAP_STOCK_DEFINE a left join UACS_YARDMAP_COIL b on a.MAT_NO = b.COIL_NO left join UACS_OPEN_PACKING_AREA_STOCK_DEFINE c on a.STOCK_NO = c.STOCK_NO  where a.STOCK_NO like '" + _stockNo + "%' order by a.STOCK_NO ";
                }
                if (_stockNo.Contains("Z01154"))
                {
                    sql += "CASE WHEN c.CONFIRM_FLAG = 10 THEN '吊入' WHEN c.CONFIRM_FLAG = 20 THEN '修复中' WHEN c.CONFIRM_FLAG = 30 THEN '吊离' WHEN c.CONFIRM_FLAG = 40 THEN '拆包卷吊入' ELSE '' END as CONFIRM_FLAG from UACS_YARDMAP_STOCK_DEFINE a left join UACS_YARDMAP_COIL b on a.MAT_NO = b.COIL_NO left join UACS_OPEN_PACKING_AREA_STOCK_DEFINE c on a.STOCK_NO = c.STOCK_NO  where a.STOCK_NO like '" + _stockNo + "%' order by a.STOCK_NO ";
                }
                if (_stockNo.Contains("Z01-6-15"))
                {
                    sql += "CASE WHEN c.CONFIRM_FLAG = 10 THEN '吊入' WHEN c.CONFIRM_FLAG = 20 THEN '冷却中' WHEN c.CONFIRM_FLAG = 30 THEN '吊离' WHEN c.CONFIRM_FLAG = 40 THEN '拆包卷吊入' ELSE '' END as CONFIRM_FLAG from UACS_YARDMAP_STOCK_DEFINE a left join UACS_YARDMAP_SADDLE_DEFINE e on a.STOCK_NO = e.SADDLE_NO left join UACS_YARDMAP_ROWCOL_DEFINE f on e.COL_ROW_NO = f.COL_ROW_NO left join UACS_YARDMAP_COIL b on a.MAT_NO = b.COIL_NO left join UACS_OPEN_PACKING_AREA_STOCK_DEFINE c on a.STOCK_NO = c.STOCK_NO  where f.COL_ROW_NO like '" + _stockNo + "%' order by a.STOCK_NO ";
                }
                else if (_stockNo == "Z51-1-A11")
                {
                    sql += "  where  a.STOCK_NAME like 'Z51-1-A11%' and d.X_CENTER > 0 and a.STOCK_STATUS = 2 and a.LOCK_FLAG = 0 order by a.STOCK_NO ";
                }

                dt.Clear();
                dt = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn = true;
                        dt.Rows.Add(dr);
                    }
                }
            
            }
            catch (Exception)
            {
                
                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("STOCK_NO", typeof(String));
                dt.Columns.Add("MAT_NO", typeof(String));
                dt.Columns.Add("CONFIRM_FLAG", typeof(String));
                //dt.Columns.Add("STOCK_NAME", typeof(String));
                dt.Columns.Add("WEIGHT", typeof(String));
                dt.Columns.Add("WIDTH", typeof(String));
                dt.Columns.Add("ACT_WEIGHT", typeof(String));
                dt.Columns.Add("ACT_WIDTH", typeof(String));
                dt.Columns.Add("INDIA", typeof(String));
                dt.Columns.Add("OUTDIA", typeof(String));
                dt.Columns.Add("PACK_CODE", typeof(String));
                dt.Columns.Add("FORBIDEN_FLAG", typeof(String));
                dt.Columns.Add("DUMMY_COIL_FLAG", typeof(String));
            }
            _dgv.DataSource = dt;
        }





        //SELECT a.STOCK_NO,a.X_CENTER,a.Y_CENTER,b.OPER_FLAG,b.OPEN_TIME,b.FLOOR,b.CONTROL_MODE,b.CRANE_NO ,d.X_CENTER,d.Y_CENTER
        //FROM UACS_YARDMAP_STOCK_DEFINE a ,UACS_CRANE_STOCK_OPER_LOG b,UACS_YARDMAP_SADDLE_STOCK c,UACS_YARDMAP_SADDLE_DEFINE d  WHERE
        //a.STOCK_NO = b.STOCK_NO and a.STOCK_NO = c.STOCK_NO and c.SADDLE_NO = d.SADDLE_NO and a.STOCK_NO = 'Z532000111'

        public void GetStockOperlogByDGV(DataGridView _dgv, string _AreaNo,string _subAreaNo)
        {
            // 标记
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
//                string sql = @"SELECT a.STOCK_NO,a.X_CENTER as STOCK_X,a.Y_CENTER as STOCK_Y,b.OPER_FLAG,b.OPEN_TIME,b.FLOOR,b.CONTROL_MODE,b.CRANE_NO ,d.X_CENTER as SADDLE_X,d.Y_CENTER as SADDLE_Y
//                               FROM UACS_YARDMAP_STOCK_DEFINE a ,UACS_CRANE_STOCK_OPER_LOG b,UACS_YARDMAP_SADDLE_STOCK c,UACS_YARDMAP_SADDLE_DEFINE d  WHERE
//                               a.STOCK_NO = b.STOCK_NO and a.STOCK_NO = c.STOCK_NO and c.SADDLE_NO = d.SADDLE_NO and a.STOCK_NO in 
//                               (SELECT STOCK_NO FROM  UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE AREA_ID  = '" + _AreaNo + "' and SUB_AREA_ID  = '" + _subAreaNo + "')";
                string sql = @"SELECT a.STOCK_NO,a.X_CENTER as STOCK_X,a.Y_CENTER as STOCK_Y,b.OPER_FLAG,b.OPEN_TIME,b.FLOOR,b.CONTROL_MODE,b.CRANE_NO ,d.X_CENTER as SADDLE_X,d.Y_CENTER as SADDLE_Y,e.PLATE_WIDTH
                               FROM UACS_YARDMAP_STOCK_DEFINE a ,UACS_CRANE_STOCK_OPER_LOG b,UACS_YARDMAP_SADDLE_STOCK c,UACS_YARDMAP_SADDLE_DEFINE d,UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE e  WHERE
                               a.STOCK_NO = b.STOCK_NO and a.STOCK_NO = c.STOCK_NO and c.SADDLE_NO = d.SADDLE_NO and a.STOCK_NO  = e.STOCK_NO  and a.STOCK_NO in 
                               (SELECT STOCK_NO FROM  UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE AREA_ID = '" + _AreaNo + "' and SUB_AREA_ID  = '" + _subAreaNo + "')";

                dt.Clear();
                dt = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn = true;
                        dt.Rows.Add(dr);
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("STOCK_NO", typeof(String));
                dt.Columns.Add("STOCK_X", typeof(String));
                dt.Columns.Add("STOCK_Y", typeof(String));
                dt.Columns.Add("OPER_FLAG", typeof(String));
                dt.Columns.Add("OPEN_TIME", typeof(String));
                dt.Columns.Add("FLOOR", typeof(String));
                dt.Columns.Add("CONTROL_MODE", typeof(String));
                dt.Columns.Add("CRANE_NO", typeof(String));
                dt.Columns.Add("SADDLE_X", typeof(String));
                dt.Columns.Add("SADDLE_Y", typeof(String));
                dt.Columns.Add("PLATE_WIDTH", typeof(String));
            }
            _dgv.DataSource = dt;
        }


        /// <summary>
        /// 查询离线包装鞍座包装纸
        /// </summary>
        /// <param name="_SaddleName"></param>
        /// <returns></returns>
        public static string GetSaddlePlateWidth(string _SaddleName)
        {
            string width = null;
            string sql = string.Format("SELECT PLATE_WIDTH FROM UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE STOCK_NO ='{0}'", _SaddleName);

            try
            {
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        width = rdr["PLATE_WIDTH"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                return width;
            }

            return width;
        }

        /// <summary>
        /// 查询到离线鞍座的行车
        /// </summary>
        /// <param name="_SaddleName"></param>
        /// <returns></returns>
         public static string GetOffLineSaddleInCrane(string _SaddleName)
        {
            string craneNo = null;
            try
            {
                string sql = @"SELECT CRANE_NO FROM UACS_CRANE_ORDER_CURRENT WHERE FROM_STOCK_NO = '" + _SaddleName + "' OR TO_STOCK_NO = '" + _SaddleName + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        craneNo = rdr["CRANE_NO"].ToString();
                    }
                }
            }
            catch (Exception er)
            {
                return craneNo;
            }

            return craneNo;
        }

        /// <summary>
        /// 查询离线包装的包装代码
        /// </summary>
        /// <param name="_SaddleName"></param>
        /// <returns></returns>
         public static string GetPackingCode(string _SaddleName)
        {
            string packingCode = null;
            string hugeCoilFlag = null;
            try
            {
                string sql = @"SELECT PACKING_CODE,PACKING_HUGE_COIL FROM UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE STOCK_NO ='" + _SaddleName + "' ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        packingCode = rdr["PACKING_CODE"].ToString();

                        if (rdr["PACKING_HUGE_COIL"] != DBNull.Value)
                            hugeCoilFlag = rdr["PACKING_HUGE_COIL"].ToString();
                        else
                            hugeCoilFlag = null;
                       
                    }
                }
            }
            catch (Exception er)
            {
                return packingCode;
            }
            return packingCode;
            //if (hugeCoilFlag == "1")
            //{
            //    return packingCode + "-大";
            //}
            //else
            //{
            //    return packingCode + "-小";
            //}   
        }

         public static string GetCoilType(string _SaddleName)
         {
             string coilType = null;
             try
             {
                 string sql = @"SELECT COIL_TYPE FROM UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE STOCK_NO ='" + _SaddleName + "' ";
                 using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                 {
                     while (rdr.Read())
                     {

                         if (rdr["COIL_TYPE"] != DBNull.Value)
                             coilType = rdr["COIL_TYPE"].ToString();
                         else
                             coilType = null;
                     }
                 }
             }
             catch (Exception er)
             {
                 return "默认";
             }

             if (coilType == "4")
             {
                 return "2824";
             }
             else if (coilType == "5")
             {
                 return "2825";
             }
             else if (coilType == "6")
             {
                 return "2825";
             }
            else if (coilType == "2014")
            {
                return "2014";
            }
            else if (coilType == "2015")
            {
                return "2015";
            }
            else
             {
                 return "默认";
             }
         }



        /// <summary>
        /// 更新离线包装库位待包卷状态和库位
        /// </summary>
        /// <param name="packing_stock">离线包装库位</param>
        /// <param name="from_stock">库内库位</param>
        /// <returns></returns>
        public bool SetPackingAwaitCoilFromStock(string packing_stock,string from_stock,out string error)
         {
             error = null;
             try
             {
                 string sql = @" update UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE set CONFIRM_FLAG = 40,FORM_STOCK = '" + from_stock + "' where STOCK_NO = '" + packing_stock + "' ";
                 DB2Connect.DBHelper.ExecuteReader(sql);
             }
             catch (Exception er)
             {
                 error = er.Message;
                 return false;
             }
             return true;

         }
    }
}
