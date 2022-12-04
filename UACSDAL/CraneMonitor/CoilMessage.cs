using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace UACSDAL
{
    /// <summary>
    /// 钢卷信息类
    /// </summary>
    public  class CoilMessage
    {
        /// <summary>
        /// 钢卷信息显示
        /// </summary>
        /// <param name="dgv">要显示的DataGridView</param>
        /// <param name="coilNo">钢卷号</param>
        public void GetCoilMessageByCoil(DataGridView dgv,string coilNo)
        {
            DataTable coilDt = new DataTable();
            bool hasSetColumn = false;
            try
            {
                string sql = @"SELECT  COIL_NO,ACT_WEIGHT,WEIGHT,ACT_WIDTH,WIDTH,INDIA,OUTDIA,PACK_FLAG,SLEEVE_WIDTH,PRODUCT_TIME,
                               CASE
                                   WHEN  COIL_OPEN_DIRECTION = 0 THEN '正'
                                   WHEN  COIL_OPEN_DIRECTION = 1 THEN '反' 
                                   END as  COIL_OPEN_DIRECTION , 
                                   NEXT_UNIT_NO,PACK_CODE,FORBIDEN_FLAG,L3_COIL_STATUS FROM UACS_YARDMAP_COIL  ";
                sql += " WHERE COIL_NO LIKE '%" + coilNo + "%' ";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = coilDt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                coilDt.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn = true;
                        coilDt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                //throw;
            }
            finally
            {
                if (!hasSetColumn)
                {
                     coilDt.Columns.Add("COIL_NO", typeof(String));
                     coilDt.Columns.Add("ACT_WEIGHT", typeof(String));
                     coilDt.Columns.Add("WEIGHT", typeof(String));
                     coilDt.Columns.Add("ACT_WIDTH", typeof(String));
                     coilDt.Columns.Add("WIDTH", typeof(String));
                     coilDt.Columns.Add("INDIA", typeof(String));
                     coilDt.Columns.Add("OUTDIA", typeof(String));
                     coilDt.Columns.Add("PACK_FLAG", typeof(String));
                     coilDt.Columns.Add("SLEEVE_WIDTH", typeof(String));
                     coilDt.Columns.Add("COIL_OPEN_DIRECTION", typeof(String));
                     coilDt.Columns.Add("NEXT_UNIT_NO", typeof(String));
                     //coilDt.Columns.Add("STEEL_GRANDID", typeof(String));
                     //coilDt.Columns.Add("ACT_WEIGHT", typeof(String));
                     //coilDt.Columns.Add("ACT_WIDTH", typeof(String));
                }


                dgv.DataSource = coilDt;
            }
        }


        /// <summary>
        /// 根据钢卷号显示到Label
        /// </summary>
        /// <param name="label">显示的Label</param>
        /// <param name="coilNo">钢卷号</param>
        public void GetLabeTxtByCoil(Label label,string coilNo)
        {
            string stockNo = "";
            if (coilNo.Trim().Count() == 11|| coilNo.Trim().Count() == 12)
            {
                try
                {
                    string sql = string.Format("SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE  MAT_NO = '{0}'",coilNo);
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        while (rdr.Read())
                        {
                            if (stockNo.Trim().Count() >= 10)
                            {
                                stockNo += "," + rdr["STOCK_NO"].ToString();
                            }
                            else
                            {
                                stockNo = rdr["STOCK_NO"].ToString();
                            }
                            
                        }
                    }


                    if (stockNo == "")
                    {
                        label.Text = coilNo + "没有库位";
                    }
                    else if (stockNo.Count() > 10)
                    {
                        label.Text = "警告！" + coilNo + "出现多库位" + stockNo;
                    }
                    else
                    {
                        label.Text = "所在库位："+stockNo;
                    }
                }
                catch (Exception er)
                {
                    label.Text = er.Message;
                }
            }
            else
                label.Text = "钢卷号不符合钢卷规则";
        }
    }
}
