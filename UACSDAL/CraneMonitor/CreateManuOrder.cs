using Baosight.iSuperframe.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace UACSDAL
{
    public class CreateManuOrder
    {


        /// <summary>
        /// 机组鞍座详细信息
        /// </summary>
        /// <param name="_unitNo">机组号</param>
        /// <param name="_bayNo">跨号</param>
        /// <param name="_dgv">dgv</param>
        /// <param name="_flagUnit">出入口区分(1 出口  0 入口)</param>
        public void GetSaddleMessageInDgv(string _unitNo, string _bayNo, System.Windows.Forms.DataGridView _dgv, int _flagUnit)
        {
            // 标记
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Empty;

                if (_flagUnit == 1)
                {
                    sql = @"SELECT b.STOCK_NO,c.COIL_NO ,c.WEIGHT,c.WIDTH,c.IN_DIA,c.OUT_DIA  FROM
                             UACS_LINE_SADDLE_DEFINE b
                             right JOIN UACS_LINE_EXIT_L2INFO c 
                            ON b.UNIT_NO= c.UNIT_NO AND b.SADDLE_L2NAME= c.SADDLE_L2NAME ";
                }
                else if (_flagUnit == 0)
                {
                    sql = @"SELECT a.STOCK_NO,c.COIL_NO  FROM
                               UACS_YARDMAP_STOCK_DEFINE a LEFT JOIN 
                               UACS_LINE_SADDLE_DEFINE b
                               ON a.STOCK_NO = b.STOCK_NO LEFT JOIN UACS_LINE_ENTRY_L2INFO c 
                               ON b.UNIT_NO= c.UNIT_NO AND b.SADDLE_L2NAME= c.SADDLE_L2NAME";
                }
                else
                {
                    MessageBox.Show("没有区分出入口");
                    return;
                }
                sql += " WHERE b.STOCK_NO like '%" + _unitNo + "%' and FLAG_UNIT_EXIT = " + _flagUnit + "";
                //sql += " WHERE a.STOCK_NO like '%" + _unitNo + "%' and a.BAY_NO = '" + _bayNo + "'";

                if (_unitNo == "D118" && _flagUnit == 1)
                {
                    sql += " and b.STOCK_NO in('D118VC1A05','D118VC1A06','D118VC1A07') ";
                }

                if (_unitNo == "D218" && _flagUnit == 1)
                {
                    sql += " and b.STOCK_NO in('D218VC1A07','D218VC1A08','D218VC1A09') ";
                }


                sql += " order by b.STOCK_NO";



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
            catch (Exception er)
            {

                //throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("STOCK_NO", typeof(String));
                dt.Columns.Add("COIL_NO", typeof(String));
                dt.Columns.Add("WEIGHT", typeof(String));
                dt.Columns.Add("WIDTH", typeof(String));
                dt.Columns.Add("IN_DIA", typeof(String));
                dt.Columns.Add("OUT_DIA", typeof(String));
            }

            _dgv.DataSource = dt;
        }


        /// <summary>
        /// 清除人工指令
        /// </summary>
        /// <param name="_bayNo"></param>
        /// <param name="_craneNo"></param>
        /// <returns></returns>
        public bool isDelManuOrder(string _bayNo, string _craneNo)
        {
            try
            {
                string sql = @" update UACS_CRANE_MANU_ORDER set ";
                sql += " FROM_STOCK = NULL ,";
                sql += " COIL_NO =  NULL,";
                sql += " TO_STOCK = NULL ,";
                sql += " STATUS = 'EMPTY' where BAY_NO = '" + _bayNo + "' and CRANE_NO = '" + _craneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                MessageBox.Show("指令清除成功");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新人工指令
        /// </summary>
        /// <param name="_fromStock"></param>
        /// <param name="_coilNo"></param>
        /// <param name="_toStock"></param>
        /// <param name="_bayNo"></param>
        /// <param name="_craneNo"></param>
        /// <returns></returns>
        public bool isUpdateManuOrder(string _fromStock, string _coilNo, string _toStock, string _bayNo, string _craneNo)
        {
            try
            {
                string sql = @" update UACS_CRANE_MANU_ORDER set ";
                sql += " FROM_STOCK = '" + _fromStock + "' ,";
                sql += " COIL_NO = '" + _coilNo + "',";
                sql += " TO_STOCK = '" + _toStock + "',";
                sql += " STATUS = 'INIT' where BAY_NO = '" + _bayNo + "' and CRANE_NO = '" + _craneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                MessageBox.Show("人工指令创建成功");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获取指令信息
        /// </summary>
        /// <param name="_txtUpStock"></param>
        /// <param name="_txtDownStock"></param>
        /// <param name="_txtCoilNo"></param>
        /// <param name="_txtStatus"></param>
        /// <param name="_craneNo"></param>
        public void GetCraneOrder(System.Windows.Forms.TextBox _txtUpStock, System.Windows.Forms.TextBox _txtDownStock, System.Windows.Forms.TextBox _txtCoilNo, System.Windows.Forms.TextBox _txtStatus, string _craneNo)
        {
            try
            {
                string sql = @"SELECT FROM_STOCK,COIL_NO,TO_STOCK,STATUS FROM UACS_CRANE_MANU_ORDER ";
                sql += " WHERE CRANE_NO = '" + _craneNo + "'";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["FROM_STOCK"] != DBNull.Value)
                            _txtUpStock.Text = rdr["FROM_STOCK"].ToString();
                        else
                            _txtUpStock.Text = "999999";
                        if (rdr["COIL_NO"] != DBNull.Value)
                            _txtCoilNo.Text = rdr["COIL_NO"].ToString();
                        else
                            _txtCoilNo.Text = "999999";
                        if (rdr["TO_STOCK"] != DBNull.Value)
                            _txtDownStock.Text = rdr["TO_STOCK"].ToString();
                        else
                            _txtDownStock.Text = "999999";
                        if (rdr["STATUS"] != DBNull.Value)
                            _txtStatus.Text = rdr["STATUS"].ToString();
                        else
                            _txtStatus.Text = "999999";
                    }
                }
            }
            catch (Exception er)
            {

                throw;
            }
        }


        /// <summary>
        /// 清空行车指令
        /// </summary>
        /// <param name="craneNo"></param>
        /// <returns></returns>
        public static bool isDelCraneOrder(string craneNo)
        {
            try
            {
                //if (craneNo == "6_4" || craneNo == "6_5" || craneNo == "6_6")
                //{
                //    string strSq4 = " DELETE FROM WMS_CRANE_ORDER ";

                //    strSq4 += " where ORDER_NO IN (SELECT ORDER_NO FROM UACS_CRANE_ORDER_CURRENT where CRANE_NO='" + craneNo + "')";

                //    DB2Connect.DBHelper.ExecuteNonQuery(strSq4);

                //    string strSq5 = " Update UACS_YARDMAP_STOCK_DEFINE set ";

                //    strSq5 += " BOOKING_MAT_NO=NULL" + ",";

                //    strSq5 += " BOOKING_CRANE_NO=NULL" + ",";

                //    strSq5 += " BOOKING_FLAG=0";

                //    strSq5 += " where BOOKING_MAT_NO IN (SELECT MAT_NO FROM UACS_CRANE_ORDER_CURRENT where CRANE_NO='" + craneNo + "')";

                //    DB2Connect.DBHelper.ExecuteNonQuery(strSq5);
                //}

                //string strSq5 = " Update UACS_YARDMAP_STOCK_DEFINE set ";

                //strSq5 += " BOOKING_MAT_NO=NULL" + ",";

                //strSq5 += " BOOKING_CRANE_NO=NULL" + ",";

                //strSq5 += " BOOKING_FLAG=0";

                //strSq5 += " where BOOKING_MAT_NO IN (SELECT MAT_NO FROM UACS_CRANE_ORDER_CURRENT where CRANE_NO='" + craneNo + "')";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq5);

                string strSql = " Update UACS_CRANE_ORDER_CURRENT set ";

                strSql += " ORDER_NO=NULL" + ",";

                //strSql += " BAY_NO=NULL" + ",";
                strSql += " BAY_NO='A'" + ",";

                strSql += " MAT_NO=NULL" + ",";

                strSql += " FROM_STOCK_NO=NULL" + ",";

                strSql += " TO_STOCK_NO=NULL" + ",";

                strSql += " PLAN_UP_X=NULL" + ",";

                strSql += " PLAN_UP_Y=NULL" + ",";

                strSql += " PLAN_UP_Z=NULL" + ",";

                strSql += " UP_ROTATE_ANGLE_SET=NULL" + ",";

                strSql += " CLAMP_WIDTH_SET=NULL" + ",";

                strSql += " PLAN_DOWN_X=NULL" + ",";

                strSql += " PLAN_DOWN_Y=NULL" + ",";

                strSql += " PLAN_DOWN_Z=NULL" + ",";

                strSql += " DOWN_ROTATE_ANGLESET=NULL" + ",";

                strSql += " COIL_WIDTH=NULL" + ",";

                strSql += " COIL_WEIGHT=NULL" + ",";

                strSql += " COIL_OUT_DIA=NULL" + ",";

                strSql += " COIL_IN_DIA=NULL" + ",";

                strSql += " FLOOR_UP_Z=NULL" + ",";

                strSql += " FLAG_SMALL_COIL=NULL" + ",";

                strSql += " FLOOR_DOWN_Z=NULL" + ",";

                strSql += " TARGET_X=NULL" + ",";

                strSql += " TARGET_Y=NULL" + ",";

                strSql += " TARGET_Z=NULL" + ",";

                strSql += " CMD_STATUS=" + "'" + "EMPTY" + "'";

                strSql += " where CRANE_NO=" + "'" + craneNo + "'";

                DB2Connect.DBHelper.ExecuteNonQuery(strSql);

                //string strSq2 = " Update UACS_YARDMAP_CRANE set ";

                //strSq2 += " STATE_SIGN=0";

                //strSq2 += " where ID=" + "'" + craneNo + "'";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq2);




                //string strSq2 = " Update UACS_YARDMAP_CRANE set ";

                //strSq2 += " ORDER_NUMBER=NULL" + ",";

                //strSq2 += " IS_DISPATCH=0";

                //strSq2 += " where ID=" + "'" + craneNo + "'";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq2);

                //string strSq3 = " DELETE FROM WMS_CRANE_ORDER ";

                //strSq3 += " where CRANE_NO=" + "'" + craneNo + "'";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq3);


            }
            catch (Exception er)

            {
                return false;
            }
            return true;
        }

        public static bool isDelNotAutoCraneOrder(string craneNo)
        {
            try
            {
                //string strSq4 = " DELETE FROM WMS_CRANE_ORDER ";

                //strSq4 += " where ORDER_NO IN (SELECT ORDER_NO FROM UACS_CRANE_ORDER_CURRENT where CRANE_NO='" + craneNo + "')";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq4);

                //string strSq5 = " Update UACS_YARDMAP_STOCK_DEFINE set ";

                //strSq5 += " BOOKING_MAT_NO=NULL" + ",";

                //strSq5 += " BOOKING_CRANE_NO=NULL" + ",";

                //strSq5 += " BOOKING_FLAG=0";

                //strSq5 += " where BOOKING_MAT_NO IN (SELECT MAT_NO FROM UACS_CRANE_ORDER_CURRENT where CRANE_NO='" + craneNo + "')";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq5);


                string strSql = " Update UACS_CRANE_ORDER_CURRENT set ";
                strSql += " ORDER_NO=NULL" + ",";
                //strSql += " BAY_NO=NULL" + ",";
                strSql += " BAY_NO='A'" + ",";
                strSql += " MAT_CODE=NULL" + ",";
                strSql += " COMP_CODE=NULL" + ",";

                strSql += " MAT_TYPE=NULL" + ",";

                strSql += " MAT_REQ_WGT=NULL" + ",";

                strSql += " MAT_ACT_WGT=NULL" + ",";

                strSql += " MAT_CUR_WGT=NULL" + ",";

                strSql += " ORDER_TYPE=NULL" + ",";

                strSql += " ORDER_PRIORITY=NULL" + ",";

                strSql += " FROM_STOCK_NO=NULL" + ",";

                strSql += " TO_STOCK_NO=NULL" + ",";

                //strSql += " CMD_STATUS=NULL" + ",";

                strSql += " CMD_SEQ=NULL" + ",";

                //strSql += " CRANE_NO=NULL" + ",";

                strSql += " CAR_SCAN_NO=NULL" + ",";

                strSql += " MAT_UP_SCAN_NO=NULL" + ",";

                strSql += " PLAN_UP_X=NULL" + ",";

                strSql += " PLAN_UP_Y=NULL" + ",";

                strSql += " PLAN_UP_Z=NULL" + ",";

                strSql += " UP_ROTATE_ANGLE=NULL" + ",";

                strSql += " MAT_DOWN_SCAN_NO=NULL" + ",";

                strSql += " PLAN_DOWN_X=NULL" + ",";

                strSql += " PLAN_DOWN_Y=NULL" + ",";

                strSql += " PLAN_DOWN_Z=NULL" + ",";
                strSql += " DOWN_ROTATE_ANGLE=NULL" + ",";

                strSql += " CMD_STATUS=" + "'" + "EMPTY" + "'";

                strSql += " where CRANE_NO=" + "'" + craneNo + "'";

                DB2Connect.DBHelper.ExecuteNonQuery(strSql);

                //string strSq2 = " Update UACS_YARDMAP_CRANE set ";

                //strSq2 += " ORDER_NUMBER=NULL" + ",";

                //strSq2 += " IS_DISPATCH=0";

                //strSq2 += " where ID=" + "'" + craneNo + "'";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq2);

                //string strSq3 = " DELETE FROM WMS_CRANE_ORDER ";

                //strSq3 += " where CRANE_NO=" + "'" + craneNo + "'";

                //DB2Connect.DBHelper.ExecuteNonQuery(strSq3);


            }
            catch (Exception er)

            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置行车避让
        /// </summary>
        /// <param name="_craneNo">行车号</param>
        /// <param name="_senderCraneNo">发送者</param>
        /// <param name="_reqEvadeX">避让X</param>
        /// <param name="_Direction">方向</param>
        /// <param name="_type">类别</param>
        /// <returns></returns>
        public static bool SetCraneEvadeRequest(string _craneNo, string _senderCraneNo, string _reqEvadeX, string _Direction, string _type, out string error)
        {
            error = null;
            try
            {
                string sql = string.Empty;
                sql = string.Format("UPDATE UACS_CRANE_EVADE_REQUEST SET SENDER ='{0}',ORIGINAL_SENDER ='{1}', EVADE_X_REQUEST ={2}, EVADE_X ={3}, EVADE_DIRECTION ='{4}', EVADE_ACTION_TYPE ='{5}',STATUS='{6}' WHERE TARGET_CRANE_NO ='{7}'  ",
                                                               _senderCraneNo, _senderCraneNo, _reqEvadeX, _reqEvadeX, _Direction, _type, "TO_DO", _craneNo);
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                error = er.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 清空行车避让指令
        /// </summary>
        /// <param name="_craneNo">行车号</param>
        /// <param name="error">错误</param>
        /// <returns></returns>
        public static bool ClearCraneEvadeRequest(string _craneNo, out string error)
        {
            error = null;
            try
            {
                string sql = string.Empty;
                sql = string.Format("UPDATE UACS_CRANE_EVADE_REQUEST SET SENDER =null,ORIGINAL_SENDER =null, EVADE_X_REQUEST =null, EVADE_X =null, EVADE_DIRECTION =null, EVADE_ACTION_TYPE =null,STATUS='EMPTY' WHERE TARGET_CRANE_NO ='{0}'  ", _craneNo);

                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                error = er.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是否清空归堆作业指令
        /// </summary>
        /// <param name="craneNo"></param>
        /// <returns></returns>
        public static bool isDelCraneManuOrder(string craneNo)
        {
            string sql = " SELECT BAY_NO,GRID_NO,CRANE_NO,MAT_NO,FROM_STOCK,TO_STOCK,STATUS FROM UACS_CRANE_MANU_ORDER ";
            sql += " WHERE STATUS = 'INIT' ";
            sql += " AND CRANE_NO = '" + craneNo + "' ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 清空归堆作业指令
        /// </summary>
        /// <param name="carneNo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool DelCraneManuOrder(string carneNo, out string error)
        {
            error = null;
            try
            {
                string sql = @"UPDATE UACS_CRANE_MANU_ORDER SET GRID_NO = null,MAT_NO = null,FROM_STOCK = null,TO_STOCK = null,STATUS = 'EMPTY' ";
                sql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + carneNo + "'";
                var enq = DB2Connect.DBHelper.ExecuteNonQuery(sql);
                if (enq > 0)
                    return true;
            }
            catch (Exception er)
            {
                error = er.Message;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否清空清扫工位指令
        /// </summary>
        /// <param name="craneNo"></param>
        /// <returns></returns>
        public static bool isDelCraneManuOrderClean(string craneNo)
        {
            string sql = " SELECT BAY_NO,CRANE_NO,MAT_NO,FROM_STOCK,TO_STOCK,CAR_NO,PLAN_NO,STATUS,PLAN_UP_Z,PLAN_DOWN_Z FROM UACS_CRANE_MANU_ORDER_CLEAN  ";
            sql += " WHERE STATUS = 'INIT' ";
            sql += " AND CRANE_NO = '" + craneNo + "' ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 清空清扫工位指令
        /// </summary>
        /// <param name="carneNo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool DelCraneManuOrderClean(string carneNo, out string error)
        {
            error = null;
            try
            {
                string sql = @"UPDATE UACS_CRANE_MANU_ORDER_CLEAN SET MAT_NO = null, FROM_STOCK = null, TO_STOCK = null, CAR_NO = null, PLAN_NO = null, STATUS = 'EMPTY' ";
                sql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + carneNo + "'";
                var enq = DB2Connect.DBHelper.ExecuteNonQuery(sql);
                if (enq > 0)
                    return true;
            }
            catch (Exception er)
            {
                error = er.Message;
                return false;
            }
            return false;
        }


        /// <summary>
        /// 是否清空装冷却剂指令
        /// </summary>
        /// <param name="craneNo"></param>
        /// <returns></returns>
        public static bool isDelCraneManuOrderCoolant(string craneNo)
        {
            string sql = "SELECT BAY_NO, CRANE_NO, MAT_NO, MAT_WEIGHT, FROM_STOCK, TO_STOCK, CAR_NO, STATUS, X_MIN, X_MAX, Y_MIN, Y_MAX, X_CENTER, Y_CENTER FROM UACSAPP.UACS_CRANE_MANU_ORDER_LBC19 ";
            sql += " WHERE STATUS = 'INIT' ";
            sql += " AND CRANE_NO = '" + craneNo + "' ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 清空装冷却剂指令
        /// </summary>
        /// <param name="carneNo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool DelCraneManuOrderCoolant(string carneNo, out string error)
        {
            error = null;
            try
            {
                string sql = @"UPDATE UACS_CRANE_MANU_ORDER_LBC19 SET MAT_WEIGHT = null, FROM_STOCK = '01-0', STATUS = 'EMPTY' ";
                sql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + carneNo + "'";
                var enq = DB2Connect.DBHelper.ExecuteNonQuery(sql);
                if (enq > 0)
                    return true;
            }
            catch (Exception er)
            {
                error = er.Message;
                return false;
            }
            return false;
        }
    }
}
