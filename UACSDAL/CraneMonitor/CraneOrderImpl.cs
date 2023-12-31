﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Baosight.iSuperframe.Common;

namespace UACSDAL
{
    public class CraneOrderImpl
    {     
        /// <summary>
        /// 根据代码ID得到代码值
        /// </summary>
        /// <param name="codeId">代码ID</param>
        /// <param name="showAll">代码值中加“全部”</param>
        /// <returns>代码值</returns>
        public DataTable GetCodeValueByCodeId(string codeId,bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            string sqlText = string.Format("SELECT CODE_VALUE_ID as TypeValue,CODE_VALUE_NAME as TypeName FROM UACS_CODE_VALUE WHERE CODE_ID = '{0}' ", codeId);
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"];
                    dt.Rows.Add(dr);
                }
            }
            if (showAll)
            {
                dt.Rows.Add("全部","全部");
            }
            return dt;
        }

        /// <summary>
        /// 获取跨号
        /// </summary>
        /// <param name="showAll">代码值中加“全部”</param>
        /// <returns>代码值</returns>
        public DataTable GetBayNo(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            string sqlText = @"SELECT BAY_NO as TypeValue,BAY_NO as TypeName FROM UACS_YARDMAP_BAY_DEFINE ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"];
                    dt.Rows.Add(dr);
                }
            }
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            return dt;
        }

        /// <summary>
        /// 获取行车号
        /// </summary>
        /// <param name="showAll">代码值中加“全部”</param>
        /// <returns>代码值</returns>
        /// 
        public DataTable GetCraneNo(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据          
            string sqlText = @"SELECT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_CRANE ORDER BY LPAD(ID,'0',10) ";
         

            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                   
                    if (rdr["TypeValue"].ToString().Count() == 1 )
                    {
                        DataRow dr = dt.NewRow();
                        dr["TypeValue"] = rdr["TypeValue"];
                        dr["TypeName"] = rdr["TypeName"];
                        dt.Rows.Add(dr);
                    }
                   
                }
            }
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            return dt;
        }
        public DataTable GetCraneNo(bool showAll ,int rule)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据

            // string sqlText = @"SELECT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_CRANE  ";
            //string sqlText = string.Empty;

            //switch (rule)
            //{

            //    case 1:
            //        {
            //            sqlText = @" SELECT ID AS TypeValue, NAME AS TypeName FROM UACS_YARDMAP_CRANE 
            //                      WHERE YARD_NO = 'Z33' or YARD_NO = 'Z32' ";
            //            break;
            //        }
            //    case 2:
            //        {
            //            sqlText = @" SELECT ID AS TypeValue , NAME AS TypeName FROM UACS_YARDMAP_CRANE 
            //                         WHERE  YARD_NO='Z52' or  YARD_NO='Z53' or YARD_NO='Z54' ";
            //            break;
            //        }

            //    case 3:
            //        {
            //            sqlText = @"SELECT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_CRANE  ";
            //            break;
            //        }
            //    default: break;
            //}

            //string sqlText1 = @" SELECT ID AS TypeValue, NAME AS TypeName FROM UACS_YARDMAP_CRANE 
            //                   WHERE YARD_NO = 'Z33' or YARD_NO = 'Z32' ";
            //string sqlText2 = @" SELECT ID AS TypeValue , NAME AS TypeName FROM UACS_YARDMAP_CRANE 
            //                        WHERE  YARD_NO='Z52' or  YARD_NO='Z53' or YARD_NO='Z54' ";
            string sqlText = @"SELECT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_CRANE ORDER BY ID ASC ";

            //if ((string)RulesAssign.GetInstance().GetRuleItem(sqlText1, sqlText2, sqlText3) != "ERROR")
            //    sqlText = (string)RulesAssign.GetInstance().GetRuleItem(sqlText1, sqlText2, sqlText3);

            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"];
                    dt.Rows.Add(dr);
                }
            }
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            return dt;
        }

        /// <summary>
        /// 根据代码ID得到代码值
        /// </summary>
        /// <param name="codeId">代码ID</param>
        /// <param name="showAll">代码值中加“全部”</param>
        /// <returns>代码值</returns>
        public Dictionary<string,string> GetCodeValueDicByCodeId(string codeId, bool showAll)
        {
            Dictionary<string, string> CodeValue = new Dictionary<string, string>();
            //准备数据
            string sqlText = string.Format("SELECT CODE_VALUE_ID as TypeValue,CODE_VALUE_NAME as TypeName FROM UACS_CODE_VALUE WHERE CODE_ID = '{0}' ", codeId);
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    string value = rdr["TypeValue"].ToString();
                    string name = rdr["TypeName"].ToString();
                    CodeValue[value] = name;
                }
            }
            if (showAll)
            {
                CodeValue["全部"] = "全部";
            }
            return CodeValue;
        }


        /// <summary>
        /// 获取行车号
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetCRANE_DEFINE(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            if (showAll)
            {
                DataRow dr2 = dt.NewRow();
                dr2 = dt.NewRow();
                dr2["TypeValue"] = "全部";
                dr2["TypeName"] = "全部";
                dt.Rows.Add(dr2);
            }
            //准备数据
            string sqlText = @"SELECT CRANE_NO AS TypeValue,CRANE_NO AS TypeName, BAY_NO, CLAMP_TYPE, SEQ_NO, WORK_POS_X_START, WORK_POS_X_END FROM UACSAPP.UACS_CRANE_DEFINE ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"] + "号行车";
                    dt.Rows.Add(dr);
                }
            }
            //if (showAll)
            //{
            //    dt.Rows.Add("全部", "全部");
            //}
            return dt;
        }

        /// <summary>
        /// 获取图库AREA 区域
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetAREA_DEFINE(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            string sqlText = @"SELECT AREA_NO AS TypeValue,AREA_NO AS TypeName, AREA_NAME, AREA_TYPE, BAY_NO, YARD_NO, X_START, X_END, Y_START, Y_END FROM UACSAPP.UACS_YARDMAP_AREA_DEFINE  WHERE AREA_TYPE = 0 ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"];
                    dt.Rows.Add(dr);
                }
            }
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            return dt;
        }

        /// <summary>
        /// 获取图库GRID 料格
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetGRID_DEFINE(String areaNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            string sqlText = @"SELECT GRID_NO AS TypeValue,GRID_NAME AS TypeName FROM UACSAPP.UACS_YARDMAP_GRID_DEFINE  WHERE AREA_NO = '" + areaNo + "'";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取指令类型
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetORDER_TYPE(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            string sqlText = @"SELECT ORDER_TYPE AS TypeValue, ORDER_TYPE AS TypeName, ORDER_PRIORITY, ORDER_TYPE_NAME, FROM_STOCK_TYPE, TO_STOCK_TYPE FROM UACSAPP.UACS_ORDER_TYPE ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"];
                    dt.Rows.Add(dr);
                }
            }
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            return dt;
        }


    }
}
