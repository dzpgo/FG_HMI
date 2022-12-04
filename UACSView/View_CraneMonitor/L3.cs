using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.Common;
using System.Reflection;
using System.Runtime.InteropServices;
using UACSDAL;

namespace HMI_OF_REPOSITORIES
{
    public partial class L3 : FormBase
    {
        public L3()
        {
            InitializeComponent();

            //Type dgvL3Type = this.dgvL3.GetType();
            //PropertyInfo pi = dgvL3Type.GetProperty("DoubleBuffered",
            //    BindingFlags.Instance | BindingFlags.NonPublic);
            //pi.SetValue(this.dgvL3, true, null);

            //this.dgvL3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;

        }

        DataTable dt;

        /// <summary>
        /// 按照日期和跨别查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {               
                dt = new DataTable();
                bool all = this.radioButton1.Checked;
                bool A = this.radioButton2.Checked;
                bool B = this.radioButton3.Checked;
                bool C = this.radioButton4.Checked;

                string recTime1;
                string recTime2;
                if (cmbShift.Text.Trim() == "全部")
                {
                    recTime1 = this.dtp1.Value.ToString("yyyyMMdd080000");
                    recTime2 = this.dtp2.Value.AddDays(1).ToString("yyyyMMdd080000");
                }
                else if (cmbShift.Text.Trim() == "早班")
                {
                    recTime1 = this.dtp1.Value.ToString("yyyyMMdd080000");
                    recTime2 = this.dtp1.Value.ToString("yyyyMMdd200000");
                }
                else if (cmbShift.Text.Trim() == "晚班")
                {
                    recTime1 = this.dtp1.Value.ToString("yyyyMMdd200000");
                    recTime2 = this.dtp1.Value.AddDays(1).ToString("yyyyMMdd080000");
                }
                else
                {
                    MessageBox.Show("班次错误");
                    return;
                }
                if (textBox1.Text.Trim() == "")
                {
                    //string sqlTime = @"select ROW_NUMBER() OVER() AS ROW_NUMBER ,B.TO_STOCK as STOCK_NO,P.MAT_NO as COIL_NO, C.WIDTH as WIDTH,C.WEIGHT as WEIGHT,C.PACK_CODE as PACK_CODE ,P.CRANE_NO as CRANE_NO,(CASE P.ACK_FLAG WHEN '0' THEN '入库成功' else '入库失败' END)as INFO,P.REC_TIME as TIME 
                    //                 from UACS_L3_IN_STOCK B  left join UACS_YARDMAP_COIL C on  C.COIL_NO = B.COIL_NO 
                    //                 left join UACS_PLAN_CRANPLAN_OPERACK P on  P.MAT_NO = B.COIL_NO AND P.CRANE_NO = B.CRANE_NO AND P.REC_TIME >= (TIMESTAMP(B.TIME)-15 seconds) AND P.REC_TIME <= (TIMESTAMP(B.TIME)+15 seconds) 
                    //                 where  P.CMD_STATUS = 'E' AND (P.MESSAGE like '%调用入库操作函数失败%' OR P.MESSAGE like '%调用倒垛操作函数失败%' OR P.ACK_FLAG = 0)  AND B.TO_STOCK NOT LIKE '%-%' AND B.TO_STOCK NOT LIKE '%D%' AND B.TO_STOCK NOT LIKE '%M%' AND B.TO_STOCK LIKE '%Z%' AND B.TIME >='{0}'and B.TIME <='{1}' ";

                    //string sqlTime = @"select ROW_NUMBER() OVER() AS ROW_NUMBER ,B.TO_STOCK as STOCK_NO,P.MAT_NO as COIL_NO, C.WIDTH as WIDTH,C.WEIGHT as WEIGHT,C.PACK_CODE as PACK_CODE ,P.CRANE_NO as CRANE_NO,(CASE P.ACK_FLAG WHEN '0' THEN '入库成功' else '入库失败' END)as STATUS,P.REC_TIME as TIME,(CASE 
                    //                    WHEN P.MESSAGE LIKE '%已有热卷%' THEN '该库位已有热卷'  
                    //                    WHEN P.MESSAGE LIKE '%已经入库%' THEN '热卷已经入库' 
                    //                    WHEN P.MESSAGE LIKE '%没有热卷信息%' THEN '库区没有热卷信息' 
                    //                    WHEN P.MESSAGE LIKE '%无法查询到热卷信息%' THEN '无法查询到热卷信息' 
                    //                    WHEN P.MESSAGE LIKE '%库位号信息不正确%' THEN '输入的库位号信息不正确' 
                    //                    WHEN P.MESSAGE = '成功' THEN '成功' 
                    //                    else '其他' END)as INFO
                    //                    from UACS_L3_IN_STOCK B  left join UACS_YARDMAP_COIL C on  C.COIL_NO = B.COIL_NO 
                    //                    left join UACS_PLAN_CRANPLAN_OPERACK P on  P.MAT_NO = B.COIL_NO AND P.CRANE_NO = B.CRANE_NO AND P.REC_TIME >= (TIMESTAMP(B.TIME)-15 seconds) AND P.REC_TIME <= (TIMESTAMP(B.TIME)+15 seconds) 
                    //                    where  P.CMD_STATUS = 'E' AND B.TO_STOCK NOT LIKE '%-%' AND B.TO_STOCK NOT LIKE '%D%' AND B.TO_STOCK NOT LIKE '%M%' AND B.TO_STOCK LIKE '%Z%' AND B.TIME >='{0}'and B.TIME <='{1}' ";

                    string sqlTime = @"select ROW_NUMBER() OVER() AS ROW_NUMBER ,B.TO_STOCK as STOCK_NO,P.MAT_NO as COIL_NO, C.WIDTH as WIDTH,C.WEIGHT as WEIGHT,C.PACK_CODE as PACK_CODE ,P.CRANE_NO as CRANE_NO,(CASE P.ACK_FLAG WHEN '0' THEN '入库成功' else '入库失败' END)as STATUS,P.REC_TIME as TIME,P.MESSAGE as INFO 
                                        from UACS_L3_IN_STOCK B  left join UACS_YARDMAP_COIL C on  C.COIL_NO = B.COIL_NO 
                                        left join UACS_PLAN_CRANPLAN_OPERACK P on  P.MAT_NO = B.COIL_NO AND P.CRANE_NO = B.CRANE_NO AND P.REC_TIME >= (TIMESTAMP(B.TIME)-15 seconds) AND P.REC_TIME <= (TIMESTAMP(B.TIME)+15 seconds) 
                                        where  P.CMD_STATUS = 'E' AND B.TO_STOCK NOT LIKE '%-%' AND B.TO_STOCK NOT LIKE '%D%' AND B.TO_STOCK NOT LIKE '%M%' AND B.TO_STOCK LIKE '%Z%' AND B.TIME >='{0}'and B.TIME <='{1}' ";
                    sqlTime = string.Format(sqlTime, recTime1, recTime2, recTime1, recTime2);
                    if (textBox1.Text.Trim() == "")
                    {
                        //if (all == false && A == false && B == false && C == false)
                        //{
                        //    sqlTime = string.Format(sqlTime);
                        //    //dtData(dt);
                        //    //   return;
                        //}
                        //else if (all == true)
                        //{
                        //    sqlTime = string.Format(sqlTime);
                        //}
                        //else if (A == true)
                        //{
                        //    sqlTime = string.Format("{0} and P.CRANE_NO IN('{1}','{2}','{3}') ", sqlTime, "1_1", "1_2", "1_3");
                        //}
                        //else if (B == true)
                        //{
                        //    sqlTime = string.Format("{0} and P.CRANE_NO IN('{1}','{2}','{3}') ", sqlTime, "1_1", "1_2", "1_3");
                        //}
                        //else if (C == true)
                        //{
                        //    sqlTime = string.Format("{0} and P.CRANE_NO IN('{1}','{2}','{3}','{4}') ", sqlTime, "6_7", "6_8", "6_9", "6_10");
                        //}
                    }
                    else
                    {
                        sqlTime = string.Format("{0} and P.MAT_NO LIKE '%{1}%'", sqlTime, textBox1.Text.Trim());
                    }
                    sqlTime += " order by P.REC_TIME ASC";//order by 字段名 desc倒叙

                    DataTable dt_T = new DataTable();
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlTime))
                    {
                        dt_T.Load(rdr);
                    }
                    dgvL3.DataSource = dt_T;
                }
                else
                {
                    string rowNumber = string.Empty;
                    string time = string.Empty;
                    string craneNo = string.Empty;
                    string coilNo = string.Empty;
                    string stockNo = string.Empty;
                    string width = string.Empty;
                    string weight = string.Empty;
                    string packCode = string.Empty;
                    string info = string.Empty;
                    string strInfo = string.Empty;
                    string sqlTime = @"select ROW_NUMBER() OVER() AS 序号,CLOCK AS 操作时间 ,DATA as 入库信息,
                                        (CASE MSGID
                                        WHEN 'KDDRMB' THEN '入库成功'
                                        WHEN 'KDDRMC' THEN '入库失败'
                                        ELSE '其它'
                                        END ) AS 入库状态
                                        FROM T_BM_MSG_IN_LOG
                                        WHERE CLOCK >='{0}'AND CLOCK <='{1}' AND (MSGID ='KDDRMB' OR MSGID ='KDDRMC') AND DATA LIKE '%E%' ";
                    sqlTime = string.Format(sqlTime, recTime1, recTime2);
                    sqlTime += " ORDER BY CLOCK ASC";//order by 字段名 desc倒叙

                    DataTable dtAuto = new DataTable();
                    dtAuto.Columns.Add("ROW_NUMBER", typeof(String));
                    dtAuto.Columns.Add("COIL_NO", typeof(String));
                    dtAuto.Columns.Add("CRANE_NO", typeof(String));
                    dtAuto.Columns.Add("STOCK_NO", typeof(String));
                    dtAuto.Columns.Add("WIDTH", typeof(String));
                    dtAuto.Columns.Add("WEIGHT", typeof(String));
                    dtAuto.Columns.Add("PACK_CODE", typeof(String));
                    dtAuto.Columns.Add("STATUS", typeof(String));
                    dtAuto.Columns.Add("INFO", typeof(String));
                    dtAuto.Columns.Add("TIME", typeof(String));
                    using (IDataReader rdr = DB2Connect.DB0Helper.ExecuteReader(sqlTime))
                    {
                        dtAuto.Clear();
                        while (rdr.Read())
                        {
                            DataRow dr = dtAuto.NewRow();
                            if (rdr["操作时间"] != DBNull.Value)
                            {
                                time = rdr["操作时间"].ToString();
                                dr["TIME"] = time;
                            }
                            else
                            {
                                dr["TIME"] = "";
                            }

                            if (rdr["入库状态"] != DBNull.Value)
                            {
                                info = rdr["入库状态"].ToString();
                                dr["STATUS"] = info.Trim();
                            }
                            else
                            {
                                dr["STATUS"] = "";
                            }

                            if (rdr["序号"] != DBNull.Value)
                            {
                                rowNumber = rdr["序号"].ToString();
                                dr["ROW_NUMBER"] = rowNumber;
                            }
                            else
                            {
                                dr["ROW_NUMBER"] = "";
                            }

                            if (rdr["入库信息"] != DBNull.Value)
                            {
                                strInfo = rdr["入库信息"].ToString().Trim();
                                string[] s = strInfo.Split(',');
                                craneNo = s[2].Trim();
                                coilNo = s[3].Trim();
                                if(s.Length>6)
                                {
                                    info = s[6];
                                }
                                else
                                {
                                    info = "成功";
                                }
                                dr["CRANE_NO"] = craneNo;
                                dr["COIL_NO"] = coilNo;
                                dr["INFO"] = info;
                                //if (info.Contains("已有热卷"))
                                //{
                                //    dr["INFO"] = "该库位已有热卷";
                                //}
                                //else if (info.Contains("热卷已经入库"))
                                //{
                                //    dr["INFO"] = "该库位已有热卷";
                                //}
                                //else if (info.Contains("没有热卷信息"))
                                //{
                                //    dr["INFO"] = "库区没有热卷信息";
                                //}
                                //else if (info.Contains("无法查询到热卷信息"))
                                //{
                                //    dr["INFO"] = "无法查询到热卷信息";
                                //}
                                //else if (info.Contains("库位号信息不正确"))
                                //{
                                //    dr["INFO"] = "输入的库位号信息不正确";
                                //}
                            }
                            else
                            {
                                dr["CRANE_NO"] = "";
                                dr["COIL_NO"] = "";
                                dr["INFO"] = "";
                            }

                            if (textBox1.Text.Trim() == "")
                            {
                                if (all == false && A == false && B == false && C == false)
                                {
                                    dtAuto.Rows.Add(dr);
                                }
                                else if (all == true)
                                {
                                    dtAuto.Rows.Add(dr);
                                }
                                else if (A == true)
                                {
                                    if (dr["CRANE_NO"].ToString().Trim() == "1_1" || dr["CRANE_NO"].ToString().Trim() == "1_2" || dr["CRANE_NO"].ToString().Trim() == "1_3")
                                    {
                                        dtAuto.Rows.Add(dr);
                                    }
                                }
                            }
                            else
                            {
                                if (dr["COIL_NO"].ToString().Trim().Contains(textBox1.Text.Trim()))
                                {
                                    dtAuto.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    dgvL3.DataSource = dtAuto;
                    foreach (DataGridViewRow dgvRow in dgvL3.Rows)
                    {
                        string CoilNo = dgvRow.Cells["COIL_NO"].Value.ToString().Trim();
                        string CraneNo = dgvRow.Cells["CRANE_NO"].Value.ToString().Trim();
                        string Time = dgvRow.Cells["TIME"].Value.ToString();

                        string sql = @"select B.TO_STOCK as STOCK_NO,C.WIDTH as WIDTH,C.WEIGHT as WEIGHT,C.PACK_CODE as PACK_CODE 
                                         from UACS_L3_IN_STOCK B
                                         left join UACS_YARDMAP_COIL C on  C.COIL_NO = B.COIL_NO
                                         where B.COIL_NO = '{0}' AND B.CRANE_NO = '{1}' AND B.TIME >= (TIMESTAMP('{2}')-30 seconds) AND B.TIME <= (TIMESTAMP('{2}')+30 seconds) ";

                        sql = string.Format(sql, CoilNo, CraneNo, Convert.ToDateTime(Time).ToString("yyyyMMddHHmmss"));

                        using (IDataReader idr = DB2Connect.DBHelper.ExecuteReader(sql))
                        {
                            while (idr.Read())
                            {
                                if (idr["STOCK_NO"] != DBNull.Value)
                                {
                                    stockNo = idr["STOCK_NO"].ToString();
                                    dgvRow.Cells["STOCK_NO"].Value = stockNo.Trim();
                                }
                                else
                                {
                                    dgvRow.Cells["STOCK_NO"].Value = "";
                                }

                                if (idr["WIDTH"] != DBNull.Value)
                                {
                                    width = idr["WIDTH"].ToString();
                                    dgvRow.Cells["WIDTH"].Value = width.Trim();
                                }
                                else
                                {
                                    dgvRow.Cells["WIDTH"].Value = "";
                                }

                                if (idr["WEIGHT"] != DBNull.Value)
                                {
                                    weight = idr["WEIGHT"].ToString();
                                    dgvRow.Cells["WEIGHT"].Value = weight.Trim();
                                }
                                else
                                {
                                    dgvRow.Cells["WEIGHT"].Value = "";
                                }

                                if (idr["PACK_CODE"] != DBNull.Value)
                                {
                                    packCode = idr["PACK_CODE"].ToString();
                                    dgvRow.Cells["PACK_CODE"].Value = packCode.Trim();
                                }
                                else
                                {
                                    dgvRow.Cells["PACK_CODE"].Value = "";
                                }
                            }
                        }
                    }
                }
                dataGridViewScreen();
                ShiftInStockStatus();
                dataGridViewColor();
            } 
            catch (Exception er)
            {
                MessageBox.Show(er.Message + "\r\n" + er.StackTrace);
            }
        }

        private void ShiftInStockStatus()
        {
            try
            {
                //自动入库件数
                double SuccessNum = 0;
                double FailNum = 0;
                double Allnum = 0;    // 总数
                for (int i = 0; i < dgvL3.RowCount; i++)
                {
                    if (dgvL3.Rows[i].Cells["STATUS"].Value != DBNull.Value)
                    {
                        //if (dgvL3.Rows[i].Cells["入库状态"].Value.ToString() == "0")
                        //{
                        //    dgvL3.Rows[i].Cells["异常报错信息"].Value = "入库成功";
                        //}
                       
                        Allnum++;
                        //入库成功
                        if (dgvL3.Rows[i].Cells["STATUS"].Value.ToString() == "入库成功")
                        {
                            SuccessNum++;
                        }
                        //入库失败
                        if (dgvL3.Rows[i].Cells["STATUS"].Value.ToString() == "入库失败")
                        {
                            FailNum++;
                        }
                    }                
                }

                lblInStockSuccessNum.Text = SuccessNum.ToString();
                lblInStockFailNum.Text = FailNum.ToString();
                lblAllNum.Text = Allnum.ToString();
                //lblAllNum.Text = (num - autoNum - manuNum).ToString();
                string aaa = ((SuccessNum / Allnum) * 100).ToString();
                if (aaa.Length >= 4)
                {
                    lblSuccess.Text = aaa.Substring(0, 4) + "%";
                }
                else if (aaa.Length == 3 || aaa.Length == 2  || aaa.Length == 1)
                {
                    lblSuccess.Text = aaa + "%";
                }
                else
                {
                    lblSuccess.Text = "999";
                }

                


            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }        
        }


        ///// <summary>
        ///// 当单选按钮选中All后者都不点时 查全部
        ///// </summary>
        ///// <param name="dt"></param>
        //private void dtData(DataTable dt)
        //{
        //    dt.Columns.Add("CRANE_NO", typeof(String));
        //    dt.Columns.Add("STOCK_NO", typeof(String));
        //    dt.Columns.Add("RES_FLAG", typeof(String));
        //    dt.Columns.Add("MESSAGE", typeof(String));
        //}

        /// <summary>
        /// 按照钢卷号模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try {
//                string sqlCOIL_NO = @"select  P.MAT_NO as 钢卷号, P.STOCK_NO as 鞍座号, P.RES_FLAG as 判定状态 ,P.EQU_NO as 操作者, P.MESSAGE as 信息  from  UACS_PLAN_MATINSTOCK_ACK P 
//                                               where  P.MAT_NO = '" +textBox1.Text.Trim()+"'";
                string sqlCOIL_NO = @"select ROW_NUMBER() OVER() AS 序号 ,MAT_NO as 钢卷号,  ACK_FLAG as 判定状态 , MESSAGE as 信息  from  UACS_PLAN_CRANPLAN_OPERACK  where  MAT_NO = '" + textBox1.Text.Trim() + "'";
            DataTable dt_NO = new DataTable();

            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlCOIL_NO))
            {
                dt_NO.Load(rdr);
            }
            dgvL3.DataSource = dt_NO;
            }
            catch 
            {
                MessageBox.Show("请输入正确的钢卷号!");
            }

            lblSuccess.Text = "999";
            lblInStockSuccessNum.Text = "999";
            lblAllNum.Text = "999";
            lblInStockFailNum.Text = "999";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.ShowDialog();

                this.Invoke(new MethodInvoker(delegate()
                {

                    Export2Excel(dgvL3, saveFileDialog1.FileName);

                }));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        #region 导出到Excel
        /// <summary>
        /// 初始化一个工作薄
        /// </summary>
        /// <param name="path">工作薄的路径</param>
        /// 
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        public void Export2Excel(DataGridView gridView, string fileName)
        {
            System.Reflection.Missing miss = System.Reflection.Missing.Value; //创建EXCEL对象appExcel,Workbook对象,Worksheet对象,Range对象
            Microsoft.Office.Interop.Excel.Application appExcel;
            appExcel = new Microsoft.Office.Interop.Excel.Application();          
            Microsoft.Office.Interop.Excel.Workbook workbookData;
            Microsoft.Office.Interop.Excel.Worksheet worksheetData;
            Microsoft.Office.Interop.Excel.Range rangedata; //设置对象不可见
            appExcel.Visible = false;
            /* 在调用Excel应用程序，或创建Excel工作簿之前，记着加上下面的两行代码 * 这是因为Excel有一个Bug，如果你的操作系统的环境不是英文的，而Excel就会在执行下面的代码时，报异常。 */
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture; System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            workbookData = appExcel.Workbooks.Add(miss);
            worksheetData = (Microsoft.Office.Interop.Excel.Worksheet)workbookData.Worksheets.Add(miss, miss, miss, miss); //给工作表赋名称
            worksheetData.Name = "UACS"; //清零计数并开始计数

            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                worksheetData.Cells[1, i + 1] = gridView.Columns[i].HeaderText.ToString();
            } //先给Range对象一个范围为A2开始，Range对象可以给一个CELL的范围，也可以给例如A1到H10这样的范围 //因为第一行已经写了表头，所以所有数据都应该从A2开始
            rangedata = worksheetData.get_Range("A2", miss);
            Microsoft.Office.Interop.Excel.Range xlRang = null; //iRowCount为实际行数，最大行
            int iRowCount = gridView.RowCount;
            int iParstedRow = 0,
            iCurrSize = 0; //iEachSize为每次写行的数值，可以自己设置，每次写1000行和每次写2000行大家可以自己测试下效率
            int iEachSize = 1000; //iColumnAccount为实际列数，最大列数
            int iColumnAccount = gridView.ColumnCount; //在内存中声明一个iEachSize×iColumnAccount的数组，iEachSize是每次最大存　　储的行数，iColumnAccount就是存储的实际列数
            object[,] objVal = new object[iEachSize, iColumnAccount];
            try
            {

                iCurrSize = iEachSize;
                while (iParstedRow < iRowCount)
                {
                    if ((iRowCount - iParstedRow) < iEachSize)
                        iCurrSize = iRowCount - iParstedRow; //用FOR循环给数组赋值
                    for (int i = 0; i < iCurrSize; i++)
                    {
                        for (int j = 0; j < iColumnAccount; j++)
                            objVal[i, j] = gridView[j, i + iParstedRow].Value.ToString();

                        System.Windows.Forms.Application.DoEvents();
                    }
                    xlRang = worksheetData.get_Range("A" + ((int)(iParstedRow + 2)).ToString(), ((char)('A' + iColumnAccount - 1)).ToString() + ((int)(iParstedRow + iCurrSize + 1)).ToString()); // 调用Range的Value2属性，把内存中的值赋给Excel
                    xlRang.Value2 = objVal;
                    iParstedRow = iParstedRow + iCurrSize;
                } //保存工作表
                worksheetData.SaveAs(fileName, miss, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss); System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRang);
                xlRang = null;
                //关闭EXCEL进程，大家可以试下不用的话如果程序不关闭在进程里一直会有EXCEL.EXE这个进程并锁定你的EXCEL表格

                if (appExcel != null)
                {

                    int lpdwProcessId;
                    GetWindowThreadProcessId(new IntPtr(appExcel.Hwnd), out lpdwProcessId);
                    System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();

                }

                MessageBox.Show("数据已经成功导出到：" + fileName, "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        private void L3_Load(object sender, EventArgs e)
        {
            lblSuccess.Text = "";
            lblInStockSuccessNum.Text = "";
            lblAllNum.Text = "";
            lblInStockFailNum.Text = "";
        }

        private void dataGridViewColor()
        {
            for (int i = 0; i < dgvL3.Rows.Count; i++)
            {
                if (dgvL3.Rows[i].Cells["STATUS"].Value != System.DBNull.Value)
                {
                    if (dgvL3.Rows[i].Cells["STATUS"].Value.ToString().Trim() == "入库失败")
                    {
                        dgvL3.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void dgvL3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridViewColor();
        }

        private void dataGridViewScreen()
        {
            try
            {
                bool A = this.radioButton1.Checked;
                bool B = this.radioButton2.Checked;
                for (int i = 0; i < dgvL3.Rows.Count; i++)
                {
                    string craneNo = dgvL3.Rows[i].Cells["CRANE_NO"].Value.ToString();
                    if (A == true)
                    {
                        if (craneNo != "1_1" && craneNo != "1_2" && craneNo != "1_3")
                        {
                            dgvL3.Rows.RemoveAt(dgvL3.Rows[i].Index);
                            i--;
                        }
                    }
                    else if (B == true)
                    {
                        if (craneNo != "1_1" && craneNo != "1_2" && craneNo != "1_3")
                        {
                            dgvL3.Rows.RemoveAt(dgvL3.Rows[i].Index);
                            i--;
                        }
                    }
                }
                //foreach (DataGridViewRow dgr in dgvL3.Rows)
                //{
                //    string craneNo = dgr.Cells["CRANE_NO"].Value.ToString();
                //    if (A == true)
                //    {
                //        if(craneNo != "6_1" && craneNo != "6_2" && craneNo != "6_3")
                //        {
                //            dgvL3.Rows.Remove(dgr);
                //        }
                //    }
                //    else if (B == true)
                //    {
                //        if (craneNo != "6_4" && craneNo != "6_5" && craneNo != "6_6")
                //        {
                //            dgvL3.Rows.Remove(dgr);
                //        }
                //    }
                //    else if (C == true)
                //    {
                //        if (craneNo != "6_7" && craneNo != "6_8" && craneNo != "6_9" && craneNo != "6_10")
                //        {
                //            dgvL3.Rows.Remove(dgr);
                //        }
                //    }
                //}

                //删除数据后重新排序
                foreach (DataGridViewRow dgvR in dgvL3.Rows)
                {
                    //判断材料号是否相同
                    if (dgvR.Cells["COIL_NO"].ToString() != "")
                    {
                        dgvR.Cells["ROW_NUMBER"].Value = (dgvR.Index + 1).ToString();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
