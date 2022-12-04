using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSDAL
{
    public class AreaRowInfo
    {
        /// <summary>
        /// 通过小区，查找和计算对应数据，返回一个实体类
        /// </summary>
        /// <param name="areaNO">小区</param>
        /// <returns></returns>
        public AreaRowBese getAreaRowDesc(string areaNO)
        {
            AreaRowBese areaRowsBase = new AreaRowBese();

            try
            {
                string sql = @"select max(X_CENTER) as X_MAX,min(X_CENTER) as X_MIN,max(Y_CENTER) as Y_MAX from 
                               UACS_YARDMAP_SADDLE_DEFINE where SADDLE_NAME like '" + areaNO + "%' and X_CENTER != 999999";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["X_MAX"] != System.DBNull.Value)
                        {
                            areaRowsBase.X_Max = Convert.ToInt32(rdr["X_MAX"]);
                        }
                        if (rdr["X_MIN"] != System.DBNull.Value)
                        {
                            areaRowsBase.X_Min = Convert.ToInt32(rdr["X_MIN"]);
                        }
                        if (rdr["Y_MAX"] != System.DBNull.Value)
                        {
                            areaRowsBase.Y_Max = Convert.ToInt32(rdr["Y_MAX"]);
                        }

                        areaRowsBase.X_Width = areaRowsBase.X_Max - areaRowsBase.X_Min;

                        areaRowsBase.X_Center = areaRowsBase.X_Width / 2 + areaRowsBase.X_Min;

                        areaRowsBase.Y_Center = areaRowsBase.Y_Max + 1700;

                        areaRowsBase.Y_Height = 1400;

                    }
                }
            }
            catch (Exception er)
            { }
            return areaRowsBase;
        }

        /// <summary>
        /// 通过小区号，获取行列编号，返回一个list---轧后库
        /// </summary>
        /// <param name="areaNO">小区</param>
        /// <returns></returns>
        public List<int> getAreaNoRow(string areaNO)
        {
            List<int> rowList = new List<int>{};
            string strRow;
            int intRow;
            try
            {
                string sql = string.Format("select COL_ROW_NO from UACS_YARDMAP_ROWCOL_DEFINE where AREA_NO = '{0}'", areaNO);
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["COL_ROW_NO"] != System.DBNull.Value)
                        {
                            strRow = rdr["COL_ROW_NO"].ToString();

                            intRow = Convert.ToInt32(strRow.Substring(strRow.LastIndexOf('-')+1));
                            rowList.Add(intRow);
                        }         
                    }
                }
            }
            catch (Exception er)
            {            
                //throw;
            }
            return rowList;
        }

        public List<int> getAreaNoCol(string areaNO)
        {
            List<int> rowList = new List<int> { };
            string strRow;
            int intRow;
            List<int> colList = new List<int> { };
            List<int> colMaxList = new List<int> { };
            string strCol;
            int intCol;
            try
            {
                string sql = string.Format("select COL_ROW_NO from UACS_YARDMAP_ROWCOL_DEFINE where AREA_NO = '{0}'", areaNO);
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["COL_ROW_NO"] != System.DBNull.Value)
                        {
                            strRow = rdr["COL_ROW_NO"].ToString();
                            colList.Clear();
                            string sqlCol = string.Format("select COL_NO from UACS_YARDMAP_SADDLE_DEFINE where LAYER_NUM = '1' AND COL_ROW_NO = '{0}'", strRow);
                            using (IDataReader dr = DB2Connect.DBHelper.ExecuteReader(sqlCol))
                            {
                                while (dr.Read())
                                {
                                    if (dr["COL_NO"] != System.DBNull.Value)
                                    {
                                        strCol = dr["COL_NO"].ToString();
                                        intCol = Convert.ToInt32(strCol);
                                        colList.Add(intCol);
                                        if (colList.Count > colMaxList.Count)
                                        {
                                            colMaxList = colList;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {
                //throw;
            }
            return colMaxList;
        }

        /// <summary>
        /// 通过小区号，获取行列编号，返回一个list---成品库
        /// </summary>
        /// <param name="areaNO">小区</param>
        /// <returns></returns>
        public List<int> getRowColListByAreaNO(string areaNo)
        {
            List<int> rowList = new List<int> { };
            string strRow = null;
            int intRow;
            try
            {
                string sql = string.Format("select COL_ROW_NO from UACS_YARDMAP_ROWCOL_DEFINE where AREA_NO like '{0}%'", areaNo);
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["COL_ROW_NO"] != System.DBNull.Value)
                        {
                            strRow = rdr["COL_ROW_NO"].ToString();

                            intRow = Convert.ToInt32(strRow.Substring(strRow.Count() - 2, 2));

                            rowList.Add(intRow);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                throw;
            }
            return rowList;
        }


        /// <summary>
        /// 通过库位行号和编号，返回一个显示string ---成品库
        /// </summary>
        /// <param name="list">int类型List</param>
        /// <param name="AreaNo">小区</param>
        /// <returns></returns>
        public string getAreaRowsInfo(List<int> list)
        {
            //StringBuilder stringBuilder = new StringBuilder("排: ");
            StringBuilder stringBuilder = new StringBuilder();
            string strLabel = string.Empty;
            if (list.Count > 0)
            {
                ArrayList lists = new ArrayList(list);
                lists.Sort();
                int min = Convert.ToInt32(lists[0]);
                int max = Convert.ToInt32(lists[lists.Count - 1]);
                stringBuilder.Append(min);
                stringBuilder.Append("-");
                stringBuilder.Append(max);
            }
            strLabel = stringBuilder.ToString();

            return strLabel;
        }

        public string getAreaColsInfo(List<int> list)
        {
            StringBuilder stringBuilder = new StringBuilder("列: ");
            string strLabel = string.Empty;
            if (list.Count > 0)
            {
                ArrayList lists = new ArrayList(list);
                lists.Sort();
                int min = Convert.ToInt32(lists[0]);
                int max = Convert.ToInt32(lists[lists.Count - 1]);
                stringBuilder.Append(min);
                stringBuilder.Append("-");
                stringBuilder.Append(max);
            }
            strLabel = stringBuilder.ToString();

            return strLabel;
        }

        /// <summary>
        /// 通过跨号获取库区名称,返回list
        /// </summary>
        /// <param name="bayNO"></param>
        /// <returns></returns>
        public List<string> getAreaListByBayNo(string bayNO)
        {
            List<string> areaNoList = new List<string>();

            try
            {
                string sql = string.Format("select AREA_NO from UACS_YARDMAP_AREA_DEFINE where BAY_NO = '{0}' and AREA_TYPE = 0", bayNO);
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["AREA_NO"] != System.DBNull.Value)
                        {
                            areaNoList.Add(rdr["AREA_NO"].ToString());
                        }
                    }
                }
            }
            catch (Exception er)
            {    
                throw ;
            }
            return areaNoList;
            
        }
      
    }
}
