using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSControls
{
    public partial class FrmMoreStock : Form
    {
        public FrmMoreStock()
        {
            InitializeComponent();
        }
        #region 数据库连接
        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;
        //连接数据库
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
                    }
                    catch (System.Exception e)
                    {
                        //throw e;
                    }
                }
                return dbHelper;
            }
        }
        #endregion

        private string  myLibrary;

        public string  MyLibrary
        {
            get { return myLibrary; }
            set { myLibrary = value; }
        }
        

        private void FrmMoreStock_Shown(object sender, EventArgs e)
        {
            GetMoreStockData(dgvMoreStockInfo);
            shiftDgvByColor();
        }

        private void GetMoreStockData(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            bool hasSetColumn = false;
            try
            {
                

                string sql = @" select STOCK_NO,MAT_NO,BAY_NO from UACS_YARDMAP_STOCK_DEFINE where MAT_NO in (select MAT_NO from UACS_YARDMAP_STOCK_DEFINE group by MAT_NO
                               having count(*)>1) order by MAT_NO ";


                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    dt.Clear();
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
                dgv.DataSource = dt;

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message,er.StackTrace);
            }
        }


        private void shiftDgvByColor()
        {
            if (dgvMoreStockInfo.RowCount > 0)
            {
                int i = 0;
                bool flag = true;
                do
                {
                    int j = 0;
                    if (flag)
                    {
                        dgvMoreStockInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;


                        while (i + j < dgvMoreStockInfo.RowCount)
                        {
                            if (dgvMoreStockInfo.Rows[i].Cells["MAT_NO"].Value.ToString() == dgvMoreStockInfo.Rows[i + j].Cells["MAT_NO"].Value.ToString())
                            {
                                dgvMoreStockInfo.Rows[i + j].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                                j++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        flag = false;
                    }
                    else
                    {
                        dgvMoreStockInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;


                        while (i + j < dgvMoreStockInfo.RowCount)
                        {
                            if (dgvMoreStockInfo.Rows[i].Cells["MAT_NO"].Value.ToString() == dgvMoreStockInfo.Rows[i + j].Cells["MAT_NO"].Value.ToString())
                            {
                                dgvMoreStockInfo.Rows[i + j].DefaultCellStyle.BackColor = Color.LightGreen;
                                j++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        flag = true;
                    }
                    i= i + j;
                } while (i < dgvMoreStockInfo.RowCount);
            }

            
        }



    }
}
