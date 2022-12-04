using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using System.Threading;

namespace UACS_LINE_Saddle
{
    public partial class StockDefine : FormBase
    {
        public StockDefine()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.Load += StockDefine_Load;
        }
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0014) // 禁掉清除背景消息

                return;

            base.WndProc(ref m);

        }

        #region -----------------------数据连接-------------------------
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


        #region -----------------------窗体事件-------------------------

        void StockDefine_Load(object sender, EventArgs e)
        {
            try
            {
                AddCbb();
            }
            catch (Exception er)
            {
            }
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string bayNo = this.cbbBayNo.Text.Trim();      //跨号
            string stockName = this.cbbStock.Text.Trim();      //库区

            bool flag1 = this.radioButton1.Checked;
            bool flag2 = this.radioButton2.Checked;
            bool flag3 = this.radioButton3.Checked;
            bool flag4 = this.radioButton4.Checked;
            bool flag5 = this.radioButton5.Checked;

            if (bayNo != "")
            {
                if (stockName != "" && stockName != "全部")
                {
                    if (flag1 == true)
                    {
                        GetNoCoil(bayNo, null, null, null, stockName);
                        GetReserve(bayNo, null, null, null, stockName);
                        GetOccupy(bayNo, null, null, null, stockName);
                    }
                    else if (flag2 == true)
                    {
                        GetNoCoil(bayNo, "0", null, null, stockName);
                        GetReserve(bayNo, "0", null, null, stockName);
                        GetOccupy(bayNo, "0", null, null, stockName);
                    }
                    else if (flag3 == true)
                    {
                        GetNoCoil(bayNo, "1", null, null, stockName);
                        GetReserve(bayNo, "1", null, null, stockName);
                        GetOccupy(bayNo, "1", null, null, stockName);
                    }
                    else if (flag4 == true)
                    {
                        GetNoCoil(bayNo, "2", null, null, stockName);
                        GetReserve(bayNo, "2", null, null, stockName);
                        GetOccupy(bayNo, "2", null, null, stockName);
                    }
                    else if (flag5 == true)
                    {
                        GetNoCoil(bayNo, "3", null, null, stockName);
                        GetReserve(bayNo, "3", null, null, stockName);
                        GetOccupy(bayNo, "3", null, null, stockName);
                    }
                    
                }
                else
                {
                    if (flag1 == true)
                    {
                        GetNoCoil(bayNo);
                        GetReserve(bayNo);
                        GetOccupy(bayNo);
                    }
                    else if (flag2 == true)
                    {
                        GetNoCoil(bayNo, "0");
                        GetReserve(bayNo, "0");
                        GetOccupy(bayNo, "0");
                    }
                    else if (flag3 == true)
                    {
                        GetNoCoil(bayNo, "1");
                        GetReserve(bayNo, "1");
                        GetOccupy(bayNo, "1");
                    }
                    else if (flag4 == true)
                    {
                        GetNoCoil(bayNo, "2");
                        GetReserve(bayNo, "2");
                        GetOccupy(bayNo, "2");
                    }
                    else if (flag5 == true)
                    {
                        GetNoCoil(bayNo, "3");
                        GetReserve(bayNo, "3");
                        GetOccupy(bayNo, "3");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择跨号！！！");
            }
            //if (checkBox1.Checked == true && checkBox2.Checked == false)
            //{
            //    string BayNo = cbbBayNo.Text.Trim();
            //    GetNoCoil(BayNo);
            //    GetReserve(BayNo);
            //    GetOccupy(BayNo);

            //}
            //else if (checkBox1.Checked == false && checkBox2.Checked == true)
            //{
            //    string type = cbbStock.Text.Trim();
            //    GetNoCoil(null, ByCbbType(type));
            //    GetReserve(null, ByCbbType(type));
            //    GetOccupy(null, ByCbbType(type));
            //}
            //else if (checkBox1.Checked == true && checkBox2.Checked == true)
            //{
            //    string BayNo = cbbBayNo.Text.Trim();
            //    string type = cbbStock.Text.Trim();

            //    if (BayNo != "" && type != "")
            //    {
            //        GetNoCoil(BayNo, ByCbbType(type));
            //        GetReserve(BayNo, ByCbbType(type));
            //        GetOccupy(BayNo, ByCbbType(type));
            //    }
            //    else if (BayNo != "" && type == "")
            //    {
            //        GetNoCoil(BayNo);
            //        GetReserve(BayNo);
            //        GetOccupy(BayNo);
            //    }
            //    else
            //    {
            //        GetNoCoil("");
            //        GetReserve("");
            //        GetOccupy("");
            //    }

            //}
            //else
            //{
            //    GetNoCoil("");
            //    GetReserve("");
            //    GetOccupy("");
            //}

            delDgv d1 = new delDgv(Dgv_FlagByColor);
            d1(dgvNoCoil);

            delDgv d2 = new delDgv(Dgv_FlagByColor);
            d2(dgvReserve);

            delDgv d3 = new delDgv(Dgv_FlagByColor);
            d3(dgvOccupy);
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string stock_no = this.txtStockNo.Text.Trim();
            string coil_no = this.txtCoilNo.Text.Trim();
            if (stock_no != "" && coil_no == "")
            {
                flag = true;
                GetNoCoil(null, null, stock_no);
                GetReserve(null, null, stock_no);
                GetOccupy(null, null, stock_no);
            }
            else if (stock_no == "" && coil_no != "")
            {
                flag = true;
                GetNoCoil(null, null, null, coil_no);
                GetReserve(null, null, null, coil_no);
                GetOccupy(null, null, null, coil_no);
            }
            else if (stock_no != "" && coil_no != "")
            {
                flag = true;
                GetNoCoil(null, null, stock_no, coil_no);
                GetReserve(null, null, stock_no, coil_no);
                GetOccupy(null, null, stock_no, coil_no);
            }

            if (flag == true)
            {
                delDgv d1 = new delDgv(Dgv_FlagByColor);
                d1(dgvNoCoil);

                delDgv d2 = new delDgv(Dgv_FlagByColor);
                d2(dgvReserve);

                delDgv d3 = new delDgv(Dgv_FlagByColor);
                d3(dgvOccupy);
            }
        }
        #endregion


        #region -----------------------操作方法-------------------------
       /// <summary>
       /// 无卷
       /// </summary>
       /// <param name="bayno">跨号</param>
       /// <param name="stock_type">库区类别</param>
       /// <param name="stock_no">库位号</param>
       /// <param name="coil_no">钢卷号</param>
       /// <param name="stock_name">库位名称</param>
        private void GetNoCoil(string bayno = null, string stock_type = null, string stock_no = null, string coil_no = null,string stock_name = null)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            string sql = null;
            try
            {
                sql = " SELECT STOCK_NO ,STOCK_NAME ,STOCK_TYPE ,STOCK_STATUS ,LOCK_FLAG ,MAT_NO ,EVENT_ID ,BAY_NO  FROM UACS_YARDMAP_STOCK_DEFINE ";
                if (bayno != null && stock_type == null && stock_no == null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 0 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type != null && stock_no == null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 0 AND BAY_NO = '" + bayno + "'AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type != null && stock_no == null && coil_no == null && stock_name != null)
                {
                    sql += " WHERE STOCK_STATUS = 0 AND BAY_NO = '" + bayno + "'AND STOCK_TYPE = '" + stock_type + "' AND STOCK_NAME LIKE '%" + stock_name + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type == null && stock_no == null && coil_no == null && stock_name != null)
                {
                    sql += " WHERE STOCK_STATUS = 0 AND BAY_NO = '" + bayno + "'AND STOCK_NAME LIKE '%" + stock_name + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no != null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 0 AND STOCK_NO like '%" + stock_no + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no == null && coil_no != null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 0 AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no != null && coil_no != null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 0 AND STOCK_NO like '%" + stock_no + "%' AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                }
                //if (bayno != null && stock_type == null )
                //{ 
                //    sql += " WHERE STOCK_STATUS = 0 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type != null && stock_type != "")
                //{
                //    sql += " WHERE STOCK_STATUS = 0 AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno != null && stock_type != null && stock_type != "")
                //{
                //    sql += " WHERE STOCK_STATUS = 0 AND BAY_NO = '" + bayno + "' AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no != null && coil_no == null)
                //{
                //    sql += " WHERE STOCK_STATUS = 0 AND STOCK_NO like '%" + stock_no + "%' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no == null && coil_no != null)
                //{
                //    sql += " WHERE STOCK_STATUS = 0 AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no != null && coil_no != null)
                //{
                //    sql += " WHERE STOCK_STATUS = 0 AND STOCK_NO like '%" + stock_no + "%' AND MAT_  NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                //}

                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
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
                if (hasSetColumn == false)
                {
                    dt.Columns.Add("STOCK_NO", typeof(String));
                    dt.Columns.Add("STOCK_NAME", typeof(String));
                    dt.Columns.Add("STOCK_TYPE", typeof(String));
                    dt.Columns.Add("STOCK_STATUS", typeof(String));
                    dt.Columns.Add("LOCK_FLAG", typeof(String));
                    dt.Columns.Add("MAT_NO", typeof(String));
                    dt.Columns.Add("EVENT_ID", typeof(String));
                    dt.Columns.Add("BAY_NO", typeof(String));

                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            dgvNoCoil.DataSource = dt;
            // Dgv_FlagByColor(dgvNoCoil);
        }

        /// <summary>
        /// 预定
        /// </summary>
        private void GetReserve(string bayno = null, string stock_type = null, string stock_no = null, string coil_no = null, string stock_name = null)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            string sql = null;
            try
            {
                sql = " SELECT STOCK_NO ,STOCK_NAME ,STOCK_TYPE ,STOCK_STATUS ,LOCK_FLAG ,MAT_NO ,EVENT_ID ,BAY_NO  FROM UACS_YARDMAP_STOCK_DEFINE ";
                if (bayno != null && stock_type == null && stock_no == null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 1 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type != null && stock_no == null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 1 AND BAY_NO = '" + bayno + "'AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type != null && stock_no == null && coil_no == null && stock_name != null)
                {
                    sql += " WHERE STOCK_STATUS = 1 AND BAY_NO = '" + bayno + "'AND STOCK_TYPE = '" + stock_type + "' AND STOCK_NAME LIKE '%" + stock_name + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type == null && stock_no == null && coil_no == null && stock_name != null)
                {
                    sql += " WHERE STOCK_STATUS = 1 AND BAY_NO = '" + bayno + "'AND STOCK_NAME LIKE '%" + stock_name + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no != null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 1 AND STOCK_NO like '%" + stock_no + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no == null && coil_no != null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 1 AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no != null && coil_no != null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 1 AND STOCK_NO like '%" + stock_no + "%' AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                }
                //sql = " SELECT STOCK_NO ,STOCK_NAME ,STOCK_TYPE ,STOCK_STATUS ,LOCK_FLAG ,MAT_NO ,EVENT_ID ,BAY_NO  FROM UACS_YARDMAP_STOCK_DEFINE  ";
                ////sql += " WHERE STOCK_STATUS = 1 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                //if (bayno != null && stock_type == null)
                //{
                //    sql += " WHERE STOCK_STATUS = 1 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type != null && stock_type != "")
                //{
                //    sql += " WHERE STOCK_STATUS = 1 AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno != null && stock_type != null && stock_type != "")
                //{
                //    sql += " WHERE STOCK_STATUS = 1 AND BAY_NO = '" + bayno + "' AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no != null && coil_no == null)
                //{
                //    sql += " WHERE STOCK_STATUS = 1 AND STOCK_NO like '%" + stock_no + "%' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no == null && coil_no != null)
                //{
                //    sql += " WHERE STOCK_STATUS = 1 AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no != null && coil_no != null)
                //{
                //    sql += " WHERE STOCK_STATUS = 1 AND STOCK_NO like '%" + stock_no + "%' AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                //}
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
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
                if (hasSetColumn == false)
                {
                    dt.Columns.Add("STOCK_NO", typeof(String));
                    dt.Columns.Add("STOCK_NAME", typeof(String));
                    dt.Columns.Add("STOCK_TYPE", typeof(String));
                    dt.Columns.Add("STOCK_STATUS", typeof(String));
                    dt.Columns.Add("LOCK_FLAG", typeof(String));
                    dt.Columns.Add("MAT_NO", typeof(String));
                    dt.Columns.Add("EVENT_ID", typeof(String));
                    dt.Columns.Add("BAY_NO", typeof(String));
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            dgvReserve.DataSource = dt;
            // Dgv_FlagByColor(dgvReserve);
        }

        /// <summary>
        /// 占位
        /// </summary>
        private void GetOccupy(string bayno = null, string stock_type = null, string stock_no = null, string coil_no = null, string stock_name = null)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            string sql = null;
            try
            {
                sql = " SELECT STOCK_NO ,STOCK_NAME ,STOCK_TYPE ,STOCK_STATUS ,LOCK_FLAG ,MAT_NO ,EVENT_ID ,BAY_NO  FROM UACS_YARDMAP_STOCK_DEFINE ";
                if (bayno != null && stock_type == null && stock_no == null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 2 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type != null && stock_no == null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 2 AND BAY_NO = '" + bayno + "'AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type != null && stock_no == null && coil_no == null && stock_name != null)
                {
                    sql += " WHERE STOCK_STATUS = 2 AND BAY_NO = '" + bayno + "'AND STOCK_TYPE = '" + stock_type + "' AND STOCK_NAME LIKE '%" + stock_name + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno != null && stock_type == null && stock_no == null && coil_no == null && stock_name != null)
                {
                    sql += " WHERE STOCK_STATUS = 2 AND BAY_NO = '" + bayno + "'AND STOCK_NAME LIKE '%" + stock_name + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no != null && coil_no == null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 2 AND STOCK_NO like '%" + stock_no + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no == null && coil_no != null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 2 AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                }
                else if (bayno == null && stock_type == null && stock_no != null && coil_no != null && stock_name == null)
                {
                    sql += " WHERE STOCK_STATUS = 2 AND STOCK_NO like '%" + stock_no + "%' AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                }
                //sql = " SELECT STOCK_NO ,STOCK_NAME ,STOCK_TYPE ,STOCK_STATUS ,LOCK_FLAG ,MAT_NO ,EVENT_ID ,BAY_NO  FROM UACS_YARDMAP_STOCK_DEFINE  ";
                //// sql += " WHERE STOCK_STATUS = 2 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                //if (bayno != null && stock_type == null)
                //{
                //    sql += " WHERE STOCK_STATUS = 2 AND BAY_NO = '" + bayno + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type != null && stock_type != "")
                //{
                //    sql += " WHERE STOCK_STATUS = 2 AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno != null && stock_type != null && stock_type != "")
                //{
                //    sql += " WHERE STOCK_STATUS = 2 AND BAY_NO = '" + bayno + "' AND STOCK_TYPE = '" + stock_type + "' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no != null && coil_no == null)
                //{
                //    sql += " WHERE STOCK_STATUS = 2 AND STOCK_NO like '%" + stock_no + "%' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no == null && coil_no != null)
                //{
                //    sql += " WHERE STOCK_STATUS = 2 AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                //}
                //else if (bayno == null && stock_type == null && stock_no != null && coil_no != null)
                //{
                //    sql += " WHERE STOCK_STATUS = 2 AND STOCK_NO like '%" + stock_no + "%' AND MAT_NO like '%" + coil_no + "%' ORDER BY EVENT_ID ";
                //}
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
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
                if (hasSetColumn == false)
                {
                    dt.Columns.Add("STOCK_NO", typeof(String));
                    dt.Columns.Add("STOCK_NAME", typeof(String));
                    dt.Columns.Add("STOCK_TYPE", typeof(String));
                    dt.Columns.Add("STOCK_STATUS", typeof(String));
                    dt.Columns.Add("LOCK_FLAG", typeof(String));
                    dt.Columns.Add("MAT_NO", typeof(String));
                    dt.Columns.Add("EVENT_ID", typeof(String));
                    dt.Columns.Add("BAY_NO", typeof(String));
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            dgvOccupy.DataSource = dt;
            // Dgv_FlagByColor(dgvOccupy);
        }

        private delegate void delDgv(DataGridView dgv);
        /// <summary>
        /// 给datagridview转换值
        /// </summary>
        /// <param name="dgv"></param>
        private void Dgv_FlagByColor(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                //封锁标记
                if (dgv.Rows[i].Cells[4].Value != "")
                {
                    string lock_flag = dgv.Rows[i].Cells[4].Value.ToString();

                    switch (lock_flag)
                    {
                        case "0":
                            dgv.Rows[i].Cells[4].Value = "0[可用]";
                            break;
                        case "1":
                            dgv.Rows[i].Cells[4].Value = "1[待判]";
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.Aquamarine;
                            break;
                        case "2":
                            dgv.Rows[i].Cells[4].Value = "2[封锁]";
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            break;
                        default:
                            dgv.Rows[i].Cells[4].Value = "无";
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
                            break;
                    }
                }
                else
                {
                    dgv.Rows[i].Cells[4].Value = "无";
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
                }
                // 库位类型
                if (dgv.Rows[i].Cells[2].Value != "")
                {
                    string type = dgv.Rows[i].Cells[2].Value.ToString();

                    switch (type)
                    {
                        case "0":
                            dgv.Rows[i].Cells[2].Value = "0[库内库位]";
                            break;
                        case "1":
                            dgv.Rows[i].Cells[2].Value = "1[机组库位]";
                            break;
                        case "2":
                            dgv.Rows[i].Cells[2].Value = "2[行车库位]";
                            break;
                        case "3":
                            dgv.Rows[i].Cells[2].Value = "3[卡车库位]";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    dgv.Rows[i].Cells[2].Value = "无";
                }
                // 库位状态
                if (dgv.Rows[i].Cells[3].Value != "")
                {
                    string status = dgv.Rows[i].Cells[3].Value.ToString();
                    switch (status)
                    {
                        case "0":
                            dgv.Rows[i].Cells[3].Value = "0[无卷]";
                            break;
                        case "1":
                            dgv.Rows[i].Cells[3].Value = "1[预定]";
                            break;
                        case "2":
                            dgv.Rows[i].Cells[3].Value = "2[占用]";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    dgv.Rows[i].Cells[3].Value = "无";
                }
            }
        }

        /// <summary>
        /// 给combobox赋值
        /// </summary>
        private void AddCbb()
        {
            try
            {
                string sql = "SELECT BAY_NO FROM UACS_YARDMAP_BAY_DEFINE ";

                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            cbbBayNo.Items.Add(rdr[i].ToString());
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        /// <summary>
        /// 根据事件号查询实时数据
        /// </summary>
        /// <param name="eventid">事件号</param>
        private void GetEventId(string eventid)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {

                string sql = "SELECT * FROM UACS_YARDMAP_STOCK_DEFINE WHERE EVENT_ID = '" + eventid + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
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
                MessageBox.Show(er.Message);
            }
            dgv_RealTime_Message.DataSource = dt;

        }

        /// <summary>
        /// 根据库位号查询历史数据
        /// </summary>
        /// <param name="StockNo">库位号</param>
        private void GetHistory(string StockNo)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT * FROM UACS_YARDMAP_STOCK_CHANGELOG WHERE EVENT_ID IN (SELECT EVENT_ID FROM UACS_YARDMAP_STOCK_CHANGELOG WHERE STOCK_NO = '" + StockNo + "') ORDER BY REC_TIME";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
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
                if (hasSetColumn == false)
                {
                    dt.Columns.Add("SEQ", typeof(String));
                    dt.Columns.Add("STOCK_NO", typeof(String));
                    dt.Columns.Add("STOCK_NAME", typeof(String));
                    dt.Columns.Add("STOCK_STATUS", typeof(String));
                    dt.Columns.Add("LAYER", typeof(String));
                    dt.Columns.Add("X_CENTER", typeof(String));
                    dt.Columns.Add("Y_CENTER", typeof(String));
                    dt.Columns.Add("Z_CENTER", typeof(String));
                    dt.Columns.Add("MAT_NO", typeof(String));
                    dt.Columns.Add("STOCK_TYPE", typeof(String));
                    dt.Columns.Add("REC_TIME", typeof(String));
                    dt.Columns.Add("EVENT_ID", typeof(String));
                }


            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            dgv_History_Message.DataSource = dt;
        }

        /// <summary>
        /// combobox转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ByCbbType(string type)
        {
            string stock_type = null;
            switch (type)
            {
                case "库内库位":
                    stock_type = "0";
                    break;
                case "机组库位":
                    stock_type = "1";
                    break;
                case "行车库位":
                    stock_type = "2";
                    break;
                case "卡车库位":
                    stock_type = "3";
                    break;
                default:
                    stock_type = "";
                    break;
            }

            return stock_type;
        }

        /// <summary>
        /// 给库位信息清空
        /// </summary>
        private void DtStock()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STOCK_NO", typeof(String));
            dt.Columns.Add("STOCK_NAME", typeof(String));
            dt.Columns.Add("STOCK_TYPE", typeof(String));
            dt.Columns.Add("STOCK_STATUS", typeof(String));
            dt.Columns.Add("LOCK_FLAG", typeof(String));
            dt.Columns.Add("LAYER", typeof(String));
            dt.Columns.Add("X_CENTER", typeof(String));
            dt.Columns.Add("Y_CENTER", typeof(String));
            dt.Columns.Add("Z_CENTER", typeof(String));
            dt.Columns.Add("MAT_NO", typeof(String));
            dt.Columns.Add("EVENT_ID", typeof(String));
            dt.Columns.Add("LAST_CHANGE_TIME", typeof(String));
            dt.Columns.Add("BAY_NO", typeof(String));
            dgv_RealTime_Message.DataSource = dt;
        }
        /// <summary>
        /// 转换实时信息
        /// </summary>
        private void dgvRealTimeByShift()
        {
            int RealTimeNum = dgv_RealTime_Message.Rows.Count;
            if (RealTimeNum >= 1)
            {
                for (int i = 0; i < RealTimeNum; i++)
                {
                    if (dgv_RealTime_Message.Rows[i].Cells[2].Value != "")
                    {
                        string type = dgv_RealTime_Message.Rows[i].Cells[2].Value.ToString().Trim();
                        switch (type)
                        {
                            case "0":
                                dgv_RealTime_Message.Rows[i].Cells[2].Value = "0[库内库位]";
                                break;
                            case "1":
                                dgv_RealTime_Message.Rows[i].Cells[2].Value = "1[机组库位]";
                                break;
                            case "2":
                                dgv_RealTime_Message.Rows[i].Cells[2].Value = "2[行车库位]";
                                break;
                            case "3":
                                dgv_RealTime_Message.Rows[i].Cells[2].Value = "3[卡车库位]";
                                break;
                            default:
                                break;
                        }
                    }
                    if (dgv_RealTime_Message.Rows[i].Cells[3].Value != "")
                    {
                        string status = dgv_RealTime_Message.Rows[i].Cells[3].Value.ToString().Trim();
                        switch (status)
                        {
                            case "0":
                                dgv_RealTime_Message.Rows[i].Cells[3].Value = "0[无卷]";
                                break;
                            case "1":
                                dgv_RealTime_Message.Rows[i].Cells[3].Value = "1[预定]";
                                break;
                            case "2":
                                dgv_RealTime_Message.Rows[i].Cells[3].Value = "2[占用]";
                                break;
                            default:
                                break;
                        }
                    }
                    if (dgv_RealTime_Message.Rows[i].Cells[4].Value != "")
                    {
                        string flag = dgv_RealTime_Message.Rows[i].Cells[4].Value.ToString().Trim();
                        switch (flag)
                        {
                            case "0":
                                dgv_RealTime_Message.Rows[i].Cells[4].Value = "0[可用]";
                                break;
                            case "1":
                                dgv_RealTime_Message.Rows[i].Cells[4].Value = "1[待判]";
                                break;
                            case "2":
                                dgv_RealTime_Message.Rows[i].Cells[4].Value = "2[封锁]";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 转换历史信息
        /// </summary>
        private void dgvHistoryByShift()
        {
            int HistoryNum = dgv_History_Message.Rows.Count;
            if (HistoryNum >= 1)
            {
                for (int i = 0; i < HistoryNum; i++)
                {
                    if (dgv_History_Message.Rows[i].Cells[9].Value != "")
                    {
                        string type = dgv_History_Message.Rows[i].Cells[9].Value.ToString().Trim();
                        switch (type)
                        {
                            case "0":
                                dgv_History_Message.Rows[i].Cells[9].Value = "0[库内库位]";
                                break;
                            case "1":
                                dgv_History_Message.Rows[i].Cells[9].Value = "1[机组库位]";
                                break;
                            case "2":
                                dgv_History_Message.Rows[i].Cells[9].Value = "2[行车库位]";
                                break;
                            case "3":
                                dgv_History_Message.Rows[i].Cells[9].Value = "3[卡车库位]";
                                break;
                            default:
                                break;
                        }
                    }
                    if (dgv_History_Message.Rows[i].Cells[3].Value != "")
                    {
                        string status = dgv_History_Message.Rows[i].Cells[3].Value.ToString().Trim();
                        switch (status)
                        {
                            case "0":
                                dgv_History_Message.Rows[i].Cells[3].Value = "0[无卷]";
                                break;
                            case "1":
                                dgv_History_Message.Rows[i].Cells[3].Value = "1[预定]";
                                break;
                            case "2":
                                dgv_History_Message.Rows[i].Cells[3].Value = "2[占用]";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 显示坐标
        /// </summary>
        /// <param name="stockno"></param>
        private void GetStockNo(string stockno)
        {
            try
            {
                string sql = @"SELECT X_CENTER,Y_CENTER,Z_CENTER FROM UACS_YARDMAP_SADDLE_DEFINE WHERE SADDLE_NO = '"+stockno+"'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        txtXCenter.Text = rdr["X_CENTER"].ToString();
                        txtYCenter.Text = rdr["Y_CENTER"].ToString();
                        //txtZCenter.Text = rdr["Z_CENTER"].ToString();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        #endregion


        #region -----------------------双击事件-------------------------
        private void dgvNoCoil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string eventid = null;
                string stockno = null;
                if (e.RowIndex >= 0)
                {
                    if (dgvNoCoil.Rows[e.RowIndex].Cells["EVENT_ID1"].Value != null)
                    {
                        eventid = dgvNoCoil.Rows[e.RowIndex].Cells["EVENT_ID1"].Value.ToString().Trim();
                        if (eventid != "")
                        {
                            GetEventId(eventid);
                        }
                        else
                        {
                            DtStock();
                        }
                    }

                    if (dgvNoCoil.Rows[e.RowIndex].Cells["STOCK_NO1"].Value != null)
                    {
                        stockno = dgvNoCoil.Rows[e.RowIndex].Cells["STOCK_NO1"].Value.ToString().Trim();
                        GetHistory(stockno);
                        GetStockNo(stockno);
                    }
                    dgvRealTimeByShift();
                    dgvHistoryByShift();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void dgvReserve_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string eventid = null;
            string stockno = null;
            if (e.RowIndex >= 0)
            {
                if (dgvReserve.Rows[e.RowIndex].Cells["EVENT_ID2"].Value != null)
                {
                    eventid = dgvReserve.Rows[e.RowIndex].Cells["EVENT_ID2"].Value.ToString().Trim();
                    if (eventid != "")
                    {
                        GetEventId(eventid);
                    }
                    else
                    {
                        DtStock();
                    }
                }
                if (dgvReserve.Rows[e.RowIndex].Cells["STOCK_Reserve"].Value != null)
                {
                    stockno = dgvReserve.Rows[e.RowIndex].Cells["STOCK_Reserve"].Value.ToString().Trim();
                    GetHistory(stockno);
                    GetStockNo(stockno);
                }
            }
            dgvRealTimeByShift();
            dgvHistoryByShift();
        }

        private void dgvOccupy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string eventid = null;
            string stockno = null;
            if (e.RowIndex >= 0)
            {
                if (dgvOccupy.Rows[e.RowIndex].Cells["EVENT_ID3"].Value != null)
                {
                    eventid = dgvOccupy.Rows[e.RowIndex].Cells["EVENT_ID3"].Value.ToString().Trim();
                    if (eventid != "")
                    {
                        GetEventId(eventid);
                    }
                    else
                    {
                        DtStock();
                    }
                }
                if (dgvOccupy.Rows[e.RowIndex].Cells["STOCK_Occupy"].Value != null)
                {
                    stockno = dgvOccupy.Rows[e.RowIndex].Cells["STOCK_Occupy"].Value.ToString().Trim();
                    GetHistory(stockno);
                    GetStockNo(stockno);
                }
            }
            dgvRealTimeByShift();
            dgvHistoryByShift();
        }
        #endregion

        private void cbbBayNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bayno = this.cbbBayNo.Text.Trim();
            cbbStock.Items.Clear();
            if (bayno == "Z21")
            {
                
                cbbStock.Items.Add("全部");
                cbbStock.Items.Add("Z21");
                cbbStock.Items.Add("Z08");                

            }
            else if (bayno == "Z22")
            {
                cbbStock.Items.Add("全部");
                cbbStock.Items.Add("Z22");
                cbbStock.Items.Add("Z07");
                
            }
            else if (bayno == "Z23")
            {
                cbbStock.Items.Add("全部");
                cbbStock.Items.Add("Z23");
                cbbStock.Items.Add("Z06");

            }
            else if (bayno == "Z01")
            {                
                cbbStock.Items.Add("Z01");             
            }
        }



    }
}
