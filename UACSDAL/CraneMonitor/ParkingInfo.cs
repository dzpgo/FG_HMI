using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;

namespace UACSDAL
{

    public class ParkingInfo
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
        private Dictionary<string, ParkingBase> dicCarMessage = new Dictionary<string, ParkingBase>();
        public Dictionary<string, ParkingBase> DicCarMessage
        {
            get { return dicCarMessage; }
            set { dicCarMessage = value; }
        }

        public void GetParkingMessage()
        {
            try
            {           
                string sql = string.Format("select A.*,B.* from UACS_PARKING_WORK_STATUS A left join UACS_YARDMAP_PARKINGSITE B on A.PARKING_NO = B.ID where A.BAY_NO ='" + bayNo + "'and B.YARD_NO is not null and B.PARKING_TYPE = '0'");
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        ParkingBase parkingInfo = new ParkingBase();
                        if (rdr["PARKING_NO"] != DBNull.Value)
                        {
                            parkingInfo.ParkingName = rdr["PARKING_NO"].ToString();
                        }
                        if (rdr["X_START"] != DBNull.Value)
                        {
                            parkingInfo.X_START = Convert.ToInt32(rdr["X_START"]);
                        }
                        if (rdr["X_END"] != DBNull.Value)
                        {
                            parkingInfo.X_END = Convert.ToInt32(rdr["X_END"]);
                        }
                        if (rdr["Y_START"] != DBNull.Value)
                        {
                            parkingInfo.Y_START = Convert.ToInt32(rdr["Y_START"]);
                        }
                        if (rdr["Y_END"] != DBNull.Value)
                        {
                            parkingInfo.Y_END = Convert.ToInt32(rdr["Y_END"]);
                        }
                        if (rdr["X_CENTER"] != DBNull.Value)
                        {
                            parkingInfo.X_CENTER = Convert.ToInt32(rdr["X_CENTER"]);
                        }
                        if (rdr["Y_CENTER"] != DBNull.Value)
                        {
                            parkingInfo.Y_CENTER = Convert.ToInt32(rdr["Y_CENTER"]);
                        }
                        if (rdr["X_LENGTH"] != DBNull.Value)
                        {
                            parkingInfo.X_LENGTH = Convert.ToInt32(rdr["X_LENGTH"]);
                        }
                        if (rdr["Y_LENGTH"] != DBNull.Value)
                        {
                            parkingInfo.Y_LENGTH = Convert.ToInt32(rdr["Y_LENGTH"]);
                        }
                        //if (rdr["ISLOADED"] != DBNull.Value)
                        //{
                        //    parkingInfo.IsLoaded = Convert.ToInt32(rdr["ISLOADED"]);
                        //}
                        if (rdr["HEAD_POSTION"] != DBNull.Value)
                        {
                            parkingInfo.HeadPostion = rdr["HEAD_POSTION"].ToString();
                        }
                        if (rdr["WORK_STATUS"] != DBNull.Value)
                        {
                            parkingInfo.PackingStatus = Convert.ToInt32(rdr["WORK_STATUS"]);
                        }
                        if (rdr["CAR_NO"] != DBNull.Value)
                        {
                            parkingInfo.Car_No = rdr["CAR_NO"].ToString();
                        }
                        if (rdr["TREATMENT_NO"] != DBNull.Value)
                        {
                            parkingInfo.TREATMENT_NO = rdr["TREATMENT_NO"].ToString();
                        }
                        //if (rdr["STOWAGE_ID"] != DBNull.Value)
                        //{
                        //    parkingInfo.STOWAGE_ID = Convert.ToInt32(rdr["STOWAGE_ID"]);
                        //}
                        parkingInfo.CarLength = parkingInfo.Y_END - parkingInfo.Y_START;
                        parkingInfo.CarWidth = parkingInfo.X_END - parkingInfo.X_START;
                        dicCarMessage[parkingInfo.ParkingName] = parkingInfo;
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        /// <summary>
        /// 获得框架配载图指令
        /// </summary>
        /// <param name="theParkNO">停车位号</param>
        /// <param name="dgv"></param>
        public static void dgvStowageOrder(string theParkNO, DataGridView dgv)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            try
            {
                string sql = " SELECT C.GROOVEID, C.MAT_NO,B.BAY_NO,B.FROM_STOCK_NO ,B.TO_STOCK_NO FROM UACS_TRUCK_STOWAGE_DETAIL C ";
                sql += " RIGHT JOIN UACS_CRANE_ORDER_Z32_Z33 B ON C.MAT_NO = B.MAT_NO ";
                sql += " WHERE C.STOWAGE_ID IN (SELECT STOWAGE_ID FROM UACS_PARKING_STATUS WHERE PARKING_NO ='{0}') ORDER BY  C.GROOVEID ";
                sql = string.Format(sql, theParkNO);
                using (IDataReader odrIn = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(odrIn);
                }
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {}
        }

        /// <summary>
        /// 获得框架配载图指令
        /// </summary>
        /// <param name="theCarNo">车辆号</param>
        /// <param name="theParkNO">停车位号</param>
        /// <param name="dgv"></param>
        public static void dgvStowageOrder(string theCarNo, string theParkNO, DataGridView dgv)
        {
            DataTable dtNull = new DataTable();
            DataTable dtOrder = new DataTable();
            dtOrder.Clear();
            try
            {
                if (!string.IsNullOrEmpty(theParkNO) && !string.IsNullOrEmpty(theCarNo))
                {
                    //对应指令车辆配载明细
                    string SQLOder = @"SELECT A.ORDER_NO AS ORDERNO,C.BOF_NO,A.ORDER_GROUP_NO,A.EXE_SEQ,B.MAT_CNAME,A.FROM_STOCK_NO,A.TO_STOCK_NO,A.BAY_NO FROM UACS_ORDER_QUEUE AS A ";
                    SQLOder += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                    SQLOder += " LEFT JOIN UACS_L3_MAT_OUT_INFO AS C on A.PLAN_NO = C.PLAN_NO ";
                    SQLOder += " WHERE A.CMD_STATUS = '0' AND A.CAR_NO = '{0}' AND A.TO_STOCK_NO = '{1}' ORDER BY A.ORDER_PRIORITY,A.ORDER_NO ";
                    SQLOder = string.Format(SQLOder, theCarNo, theParkNO);
                    using (IDataReader odrIn = DB2Connect.DBHelper.ExecuteReader(SQLOder))
                    {
                        dtOrder.Load(odrIn);
                    }
                    dgv.DataSource = dtOrder;
                }
                else
                {
                    dtNull.Clear();
                    dgv.DataSource = dtNull;
                }
            }
            catch (Exception ex)
            {}
        }

        /// <summary>
        ///  获得框架配载图信息
        /// </summary>
        /// <param name="theStowageId"></param>
        /// <param name="dgv"></param>
        public static void dgvStowageMessage(int theStowageId, DataGridView dgv)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            try
            {
               
                string sql = @"SELECT  A.GROOVEID , A.MAT_NO, A.POS_ON_FRAME, A.X_CENTER, A.Y_CENTER, A.Z_CENTER, C.STOCK_NO, A.SLEEVE_LENGTH, A.PACKAGE_STATUS , 
                                   CASE
                                        WHEN  A.STATUS = 0 THEN '初始化'
                                        WHEN  A.STATUS = 100 THEN '执行完'
                                        WHEN  A.STATUS = 30 THEN '已暂停'
                                        ELSE '其他'
                                    END as  STATUS , 
                                    CASE
                                        WHEN  A.ISUNLOAD = 0 THEN '卸'
                                        WHEN  A.ISUNLOAD = 1 THEN '不卸'
                                        ELSE '未知'
                                    END as  ISUNLOAD , 
                                    D.WEIGHT, D.OUTDIA ,D.WIDTH  FROM UACS_TRUCK_STOWAGE_DETAIL A  ";
                sql += " LEFT JOIN  UACS_YARDMAP_COIL D ON A.MAT_NO = D.COIL_NO ";
                sql += " LEFT JOIN  UACS_YARDMAP_STOCK_DEFINE C ON A.MAT_NO = C.MAT_NO ";
                sql += " LEFT JOIN UACS_TRUCK_STOWAGE B ON A.STOWAGE_ID = B.STOWAGE_ID WHERE 1=1 ";
                sql += " AND B.STOWAGE_ID = '" + theStowageId + "' ORDER BY A.POS_ON_FRAME ";

                using (IDataReader odrIn = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(odrIn);
                }
                dgv.DataSource = dt;
            }
            catch (Exception er)
            {}
        }

        /// <summary>
        /// 获取配载信息
        /// </summary>
        /// <param name="theStowageId">配载号</param>
        /// <param name="carNo">车辆号</param>
        /// <param name="packingNo">停车位号</param>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static void dgvStowageMessage(int theStowageId,string carNo, string packingNo, DataGridView dgv)
        {
            DataTable dtNull = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(carNo) && packingNo.Contains('A') && packingNo != "请选择")
                {
                    //对应指令车辆配载明细
                    string sqlText_ORDER = @"SELECT A.ORDER_NO,A.ORDER_GROUP_NO,A.EXE_SEQ,A.ORDER_PRIORITY,CMD_SEQ,
                                                    CASE 
                                                    WHEN A.CMD_STATUS = 0 THEN '初始化' 
                                                    WHEN A.CMD_STATUS = 1 THEN '获取指令' 
                                                    WHEN A.CMD_STATUS = 2 THEN '激光扫描' 
                                                    WHEN A.CMD_STATUS = 3 THEN '到取料点上方' 
                                                    WHEN A.CMD_STATUS = 4 THEN '空载下降到位' 
                                                    WHEN A.CMD_STATUS = 5 THEN '有载荷量' 
                                                    WHEN A.CMD_STATUS = 6 THEN '重载上升到位' 
                                                    WHEN A.CMD_STATUS = 7 THEN '到放料点上方' 
                                                    WHEN A.CMD_STATUS = 8 THEN '重载下降到位' 
                                                    WHEN A.CMD_STATUS = 9 THEN '无载荷量' 
                                                    WHEN A.CMD_STATUS = 10 THEN '空载上升到位' 
                                                    ELSE '其他' 
                                                    END AS CMD_STATUS
                                                    ,A.PLAN_NO,B.MAT_CNAME,B.MAT_CNAME AS MAT_CNAME2 ,A.FROM_STOCK_NO,A.TO_STOCK_NO,A.REQ_WEIGHT,A.ACT_WEIGHT,A.UPD_TIME,A.REC_TIME 
                                                    FROM UACS_ORDER_QUEUE AS A ";
                    sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                    sqlText_ORDER += " WHERE A.CMD_STATUS = '0' AND A.CAR_NO = '{0}' AND A.TO_STOCK_NO = '{1}' ";
                    sqlText_ORDER += " ORDER BY A.ORDER_PRIORITY,A.ORDER_NO ";
                    sqlText_ORDER = string.Format(sqlText_ORDER, carNo, packingNo);
                    DataTable dt = new DataTable();
                    using (IDataReader odrIn = DB2Connect.DBHelper.ExecuteReader(sqlText_ORDER))
                    {
                            dt.Load(odrIn);
                            dgv.DataSource = dt;
                            foreach (DataGridViewColumn item in dgv.Columns)
                            {
                                if (item.Name == "Index")
                                {
                                    for (int y = 0; y < dgv.Rows.Count - 1; y++)
                                    {
                                        dgv.Rows[y].Cells["Index"].Value = y;
                                    }
                                    break;
                                }
                            }
                        odrIn.Close();
                    }
                    dt.Dispose();
                }
                else
                {
                    dgv.DataSource = dtNull;
                    dtNull.Dispose();
                }                
            }
            catch (Exception ex)
            {}
        }

        /// <summary>
        /// 获取停车位车辆的类别
        /// </summary>
        /// <param name="theStowageId">配载号</param>
        /// <param name="carNo">车辆号</param>
        /// <param name="packingNo">停车位号</param>
        /// <returns></returns>
        public static string getStowageCarType(int theStowageId, string carNo, string packingNo)
        {
            string carType = "未知";
            try
            {
                string sql = @"SELECT  CASE
                                        WHEN CAR_TYPE = 1 THEN '社会车'
                                        WHEN CAR_TYPE = 2 THEN '装料车'
                                        ELSE '其他'
                                    END as CAR_TYPE  FROM UACS_PARKING_WORK_STATUS WHERE 1=1 ";
                if (theStowageId > 0)
                {
                    sql += " AND STOWAGE_ID = " + theStowageId + "";
                }
                if (!string.IsNullOrEmpty(carNo))
                {
                    sql += " AND CAR_NO = '" + carNo + "'";
                }
                if (!string.IsNullOrEmpty(packingNo))
                {
                    sql += " AND PARKING_NO = '" + packingNo + "'";
                }
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        carType = rdr["CAR_TYPE"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                return carType;
            }
            return carType;
        }
        /// <summary>
        /// 获取停车位车辆的类别
        /// </summary>
        /// <param name="theStowageId"></param>
        /// <returns></returns>
        public static string getStowageCarType(int theStowageId)
        {
            string carType = "未知";
            string isWoodenCar = "";
            try
            {
                string sql = @"SELECT  CASE
                                        WHEN CAR_TYPE = 100 THEN '框架'
                                        WHEN CAR_TYPE = 101 THEN '一般社会车辆'
                                        WHEN CAR_TYPE = 102 THEN '大头娃娃车'
                                        WHEN CAR_TYPE = 103 THEN '较低社会车辆'
                                        WHEN CAR_TYPE = 104 THEN '雨棚车'
                                        WHEN CAR_TYPE = 200 THEN '木架卷车'
                                        ELSE '其他'
                                    END as CAR_TYPE, ISWOODENCAR  FROM UACS_TRUCK_STOWAGE WHERE STOWAGE_ID = " + theStowageId + "";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        isWoodenCar = rdr["ISWOODENCAR"].ToString();
                        carType = rdr["CAR_TYPE"].ToString();
                        if (carType == "一般社会车辆" && isWoodenCar == "1")
                        {
                            carType = "木架卷车";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return carType;
            }
            return carType;
        }
    }
}
