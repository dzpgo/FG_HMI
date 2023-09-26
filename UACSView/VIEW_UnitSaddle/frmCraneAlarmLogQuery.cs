using System;
using System.Data;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using System.Runtime.InteropServices;
using UACSDAL;
using System.Globalization;

namespace UACSView
{
    /// <summary>
    /// 报警信息管理
    /// </summary>
    public partial class frmCraneAlarmLogQuery : FormBase
    {
        public frmCraneAlarmLogQuery()
        {
            InitializeComponent();
            this.Load += frmCraneAlarmLogQuery_Load;
        }

        void frmCraneAlarmLogQuery_Load(object sender, EventArgs e)
        {
            //初始时间为前一小时和后一小时
            DateTime dt = DateTime.Now;
            TimeSpan tp = TimeSpan.Parse("00.1:00:00");
            dateTimePicker_Start.Value = dt.Subtract(tp);
            dateTimePicker_End.Value = dt.Add(tp);
            BindComboBox(comboBox_ShipLotNo);


            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;

            this.ucPageDemo.CurrentPage = 1;
            this.ucPageDemo.PageSize = Convert.ToInt32(this.ucPageDemo.CboPageSize.Text);
            this.ucPageDemo.TotalPages = 1;
            this.ucPageDemo.ClickPageButtonEvent += ucPageDemo_ClickPageButtonEvent;
            this.ucPageDemo.ChangedPageSizeEvent += ucPageDemo_ChangedPageSizeEvent;
            this.ucPageDemo.JumpPageEvent += ucPageDemo_JumpPageEvent;
            readAlarmLog(1, craneNO, timeStart, timeEnd);
        }



        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;

        public static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }

                }
                return dbHelper;
            }
        }

        string alarmTime_Current = string.Empty;
        string alarmTime_Old = string.Empty;
        int araneCode = 0;
        bool flagColor = false;
        /// <summary>
        /// 查询报警信息
        /// </summary>
        /// <param name="isLoad">是否是初始化 true=是初始化，false=不是初始化</param>
        /// <param name="craneNO">行车</param>
        /// <param name="timeStart">开始时间</param>
        /// <param name="timeEnd">结束时间</param>
        private void readAlarmLog(int currentPage, string craneNO, DateTime timeStart, DateTime timeEnd)
        {
            try
            {
                string strSql = "SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER () AS ROWNUM,tab.* FROM ( ";
                string tableName = "UACS_CRANE_ALARM_" + craneNO;
                strSql += "select ";
                strSql += " a.CRANE_NO   CRANE_NO, ";
                strSql += " a.ALARM_CODE   ALARM_CODE, ";
                strSql += " a.ALARM_TIME   ALARM_TIME, ";
                strSql += " a.X_ACT   X_ACT, ";
                strSql += " a.Y_ACT   Y_ACT, ";
                strSql += " a.Z_ACT   Z_ACT, ";
                strSql += " a.HAS_COIL   HAS_COIL, ";
                strSql += " a.CLAMP_WIDTH_ACT   CLAMP_WIDTH_ACT, ";
                strSql += " a.CONTROL_MODE   CONTROL_MODE, ";
                strSql += " a.CRANE_STATUS   CRANE_STATUS, ";
                strSql += " a.ORDER_ID   ORDER_ID, ";
                strSql += " b.ALARM_INFO   ALARM_INFO, ";
                strSql += " b.ALARM_CLASS   ALARM_CLASS ";
                strSql += " from ";
                strSql += tableName + " a " + ",";
                strSql += "  UACS_CRANE_ALARM_CODE_DEFINE b  ";
                strSql += " where ";
                strSql += "       a.ALARM_CODE=b.ALARM_CODE ";
                //if (!comboBox_ShipLotNo.SelectedText.Equals("全部"))
                //{
                    strSql += " and a.CRANE_NO=" + "'" + craneNO + "'";
                //}
                if (!string.IsNullOrEmpty(txtAlarmCode.Text.Trim()))
                {
                    strSql += " and a.ALARM_CODE LIKE '%" + txtAlarmCode.Text.Trim() + "%' ";
                }
                strSql += " and a.ALARM_TIME>= " + DateNormal_ToString(timeStart);
                strSql += " and a.ALARM_TIME<= " + DateNormal_ToString(timeEnd);
                strSql += " Order By a.ALARM_TIME DESC";
                strSql += " ) tab ) WHERE ROWNUM BETWEEN ((" + currentPage + " - 1) * " + this.ucPageDemo.PageSize + ") + 1 AND " + currentPage + " *  " + this.ucPageDemo.PageSize;

                DataTable dtResult = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSql))
                {
                    dtResult.Load(rdr);
                    //GridAlarmLog.Rows.Clear();
                    //while (rdr.Read())
                    //{
                    //    GridAlarmLog.Rows.Add();
                    //    DataGridViewRow theRow = GridAlarmLog.Rows[GridAlarmLog.Rows.Count - 1];
                    //    if (rdr["CRANE_NO"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["CRANE_NO"].Value = Convert.ToString(rdr["CRANE_NO"]);
                    //    }
                    //    if (rdr["ALARM_CODE"] != System.DBNull.Value)
                    //    {
                    //        araneCode = Convert.ToInt32(rdr["ALARM_CODE"]);
                    //        theRow.Cells["ALARM_CODE"].Value = Convert.ToString(rdr["ALARM_CODE"]);
                    //    }
                    //    if (rdr["ALARM_TIME"] != System.DBNull.Value)
                    //    {
                    //        alarmTime_Current = Convert.ToString(rdr["ALARM_TIME"]);
                    //        theRow.Cells["ALARM_TIME"].Value = Convert.ToString(rdr["ALARM_TIME"]);
                    //    }
                    //    if (rdr["X_ACT"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["X_ACT"].Value = Convert.ToString(rdr["X_ACT"]);
                    //    }
                    //    if (rdr["Y_ACT"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["Y_ACT"].Value = Convert.ToString(rdr["Y_ACT"]);
                    //    }
                    //    if (rdr["Z_ACT"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["Z_ACT"].Value = Convert.ToString(rdr["Z_ACT"]);
                    //    }
                    //    if (rdr["HAS_COIL"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["HAS_COIL"].Value = Convert.ToString(rdr["HAS_COIL"]);
                    //    }
                    //    //if (rdr["CLAMP_WIDTH_ACT"] != System.DBNull.Value)
                    //    //{
                    //    //    theRow.Cells["HAS_COIL"].Value = Convert.ToString(rdr["CLAMP_WIDTH_ACT"]);
                    //    //}
                    //    if (rdr["CONTROL_MODE"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["CONTROL_MODE"].Value = Convert.ToString(rdr["CONTROL_MODE"]);
                    //    }
                    //    if (rdr["CRANE_STATUS"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["CRANE_STATUS"].Value = Convert.ToString(rdr["CRANE_STATUS"]);
                    //    }
                    //    if (rdr["ORDER_ID"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["ORDER_ID"].Value = Convert.ToString(rdr["ORDER_ID"]);
                    //    }
                    //    if (rdr["ALARM_CODE"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["ALARM_CODE"].Value = Convert.ToString(rdr["ALARM_CODE"]);
                    //    }
                    //    if (rdr["ALARM_INFO"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["ALARM_INFO"].Value = Convert.ToString(rdr["ALARM_INFO"]);
                    //    }
                    //    if (rdr["ALARM_CLASS"] != System.DBNull.Value)
                    //    {
                    //        theRow.Cells["ALARM_CLASS"].Value = Convert.ToString(rdr["ALARM_CLASS"]);
                    //    }

                    //    if (alarmTime_Old != alarmTime_Current)
                    //    {
                    //        flagColor = !flagColor;
                    //    }


                    //    if (flagColor)
                    //    {
                    //        theRow.DefaultCellStyle.BackColor = Color.Silver;
                    //    }
                    //    else
                    //    {
                    //        theRow.DefaultCellStyle.BackColor = Color.DarkGray;
                    //    }
                    //    alarmTime_Old = alarmTime_Current;

                    //    //手自动切换
                    //    if (araneCode >= 1021 && araneCode <= 1033)
                    //    {
                    //        theRow.DefaultCellStyle.BackColor = Color.GhostWhite;
                    //    }
                    //}
                }
                this.GridAlarmLog.DataSource = GetDataPage(dtResult, currentPage);
            }
            catch (Exception ex)
            {
            }
        }

        public string DateNormal_ToString(DateTime theDateTime)
        {
            string strDateTime = "";
            try
            {
                strDateTime = theDateTime.Year.ToString("0000") + theDateTime.Month.ToString("00") + theDateTime.Day.ToString("00")
                            + theDateTime.Hour.ToString("00") + theDateTime.Minute.ToString("00") + theDateTime.Second.ToString("00");
                strDateTime = "to_date('" + strDateTime + "','YYYYMMDDHH24MISS')";
            }
            catch (Exception)
            {
            }
            return strDateTime;
        }

        private void cmdQuery_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime timeStart = dateTimePicker_Start.Value;
                DateTime timeEnd = dateTimePicker_End.Value;
                string craneNO = comboBox_ShipLotNo.Text;
                readAlarmLog(1, craneNO, timeStart, timeEnd);

                //光标显示到最后一行
                GridAlarmLog.FirstDisplayedScrollingRowIndex = GridAlarmLog.RowCount - 1;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 向上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            //获取当前行的索引
            int index = GridAlarmLog.CurrentRow.Index;
            //索引不能为负数
            if (index < 0)
                return;
            // MessageBox.Show(index.ToString());
            //找到的报警号
            string currentCode = string.Empty;
            //查询的报警号
            string selectCode = this.txtAlarmCode.Text.Trim();
            //查询的报警号不能为""
            if (selectCode == "")
                return;
            for (int i = index - 1; i < index; i--)
            {
                if (i >= 0)
                {
                    if (GridAlarmLog.Rows[i].Cells["ALARM_CODE"].Value != DBNull.Value)
                    {
                        currentCode = GridAlarmLog.Rows[i].Cells["ALARM_CODE"].Value.ToString();
                    }

                    //找到相同钢卷号
                    if (selectCode == currentCode)
                    {
                        GridAlarmLog.FirstDisplayedScrollingRowIndex = i;
                        GridAlarmLog.Rows[i].Cells["ALARM_CODE"].Selected = true;
                        GridAlarmLog.Rows[index].Cells["ALARM_CODE"].Selected = false;
                        GridAlarmLog.CurrentCell = GridAlarmLog.Rows[i].Cells["ALARM_CODE"];
                        return;
                    }
                }
                else
                    return;

            }

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //获取当前行的索引
            int index = GridAlarmLog.CurrentRow.Index;
            //索引不能为负数
            if (index < 0)
                return;
            //MessageBox.Show(index.ToString());
            //找到的报警号
            string currentCode = string.Empty;
            string selectCode = this.txtAlarmCode.Text.Trim();
            if (selectCode == "")
                return;

            for (int i = index + 1; i < GridAlarmLog.RowCount; i++)
            {
                if (GridAlarmLog.Rows[i].Cells["ALARM_CODE"].Value != DBNull.Value)
                {
                    currentCode = GridAlarmLog.Rows[i].Cells["ALARM_CODE"].Value.ToString();
                }

                //找到相同钢卷号
                if (selectCode == currentCode)
                {
                    GridAlarmLog.FirstDisplayedScrollingRowIndex = i;
                    GridAlarmLog.Rows[i].Cells["ALARM_CODE"].Selected = true;
                    GridAlarmLog.Rows[index].Cells["ALARM_CODE"].Selected = false;
                    GridAlarmLog.CurrentCell = GridAlarmLog.Rows[i].Cells["ALARM_CODE"];
                    return;
                }
            }
        }


        private void BindComboBox(ComboBox comBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            try
            {
                //DataRow dr2 = dt.NewRow();
                //dr2["TypeValue"] = "全部";
                //dr2["TypeName"] = "全部";
                //dt.Rows.Add(dr2);

                string strSql = @"SELECT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_CRANE ORDER BY ID ASC ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        if (rdr["TypeName"].ToString().Trim() != "")
                        {
                            dr["TypeValue"] = rdr["TypeValue"];
                            dr["TypeName"] = rdr["TypeName"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                //绑定列表下拉框数据
                comBox.DataSource = dt;
                comBox.DisplayMember = "TypeName";
                comBox.ValueMember = "TypeValue";
                comBox.SelectedItem = 0;
            }
            catch (Exception)
            {
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
                            objVal[i, j] = gridView[j, i + iParstedRow].Value;

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

        #region 日期查询        
        /// <summary>
        /// 当天
        /// </summary>
        private void GetToDayTime()
        {
            this.dateTimePicker_Start.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            this.dateTimePicker_End.Value = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToString());
            //查询
            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;
            readAlarmLog(1, craneNO, timeStart, timeEnd);
        }
        /// <summary>
        /// 本周
        /// </summary>
        private void GetWeekTime()
        {
            this.dateTimePicker_Start.Value = DateTime.Parse(DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))).ToString("yyyy-MM-dd 00:00:00"));  	//本周周一  
            this.dateTimePicker_End.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            //查询
            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;
            readAlarmLog(1, craneNO, timeStart, timeEnd);
        }
        /// <summary>
        /// 月度
        /// </summary>
        private void GetMonthlyTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-MM-01 00:00:00");
            //控件设置值
            this.dateTimePicker_Start.Value = Convert.ToDateTime(day1);
            //查询
            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;
            readAlarmLog(1, craneNO, timeStart, timeEnd);
        }
        /// <summary>
        /// 当前季度
        /// </summary>
        private void GetQuarterlyTime()
        {
            //季度第一天
            var day1 = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3).ToString("yyyy-MM-01");
            //季度最后一天
            var lastday = DateTime.Parse(DateTime.Now.AddMonths(3 - (DateTime.Now.Month - 1) % 3).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
            //控件设置值
            this.dateTimePicker_Start.Value = Convert.ToDateTime(day1);
            //查询
            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;
            readAlarmLog(1, craneNO, timeStart, timeEnd);
        }
        /// <summary>
        /// 年度
        /// </summary>
        private void GetAnnualTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-01-01");
            //控件设置值
            this.dateTimePicker_Start.Value = Convert.ToDateTime(day1);
            //查询
            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;
            readAlarmLog(1, craneNO, timeStart, timeEnd);
        }
        #endregion



        #region 分页

        int totalPages = 0;

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        private DataTable GetDataPage(DataTable dtResult, int currentPage)
        {
            totalPages = 0;
            int totalRows = 0;
            if (null == dtResult || dtResult.Rows.Count == 0)
            {
                this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", "1", "1");
                this.ucPageDemo.TotalRows.Text = @"0";
                this.ucPageDemo.CurrentPage = 1;
                this.ucPageDemo.TotalPages = 1;
            }
            else
            {
                totalRows = Convert.ToInt32(dtResult.Rows[0]["TotalRows"].ToString());
                totalPages = totalRows % this.ucPageDemo.PageSize == 0 ? totalRows / this.ucPageDemo.PageSize : (totalRows / this.ucPageDemo.PageSize) + 1;
                this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", currentPage, totalPages);
                this.ucPageDemo.TotalRows.Text = totalRows.ToString();
                this.ucPageDemo.CurrentPage = currentPage;
                this.ucPageDemo.TotalPages = totalPages;
            }
            return dtResult;
        }

        /// <summary>
        /// 页数跳转
        /// </summary>
        /// <param name="jumpPage">跳转页</param>
        void ucPageDemo_JumpPageEvent(int jumpPage)
        {
            if (jumpPage <= this.ucPageDemo.TotalPages)
            {
                if (jumpPage > 0)
                {
                    this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
                    this.ucPageDemo.JumpPageCtrl.Text = jumpPage.ToString();

                    DateTime timeStart = dateTimePicker_Start.Value;
                    DateTime timeEnd = dateTimePicker_End.Value;
                    string craneNO = comboBox_ShipLotNo.Text;
                    this.readAlarmLog(jumpPage, craneNO, timeStart, timeEnd);
                }
                else
                {
                    jumpPage = 1;
                    this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
                    this.ucPageDemo.JumpPageCtrl.Text = jumpPage.ToString();
                    DateTime timeStart = dateTimePicker_Start.Value;
                    DateTime timeEnd = dateTimePicker_End.Value;
                    string craneNO = comboBox_ShipLotNo.Text;
                    this.readAlarmLog(jumpPage, craneNO, timeStart, timeEnd);
                }
            }
            else
            {
                this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
                MessageBox.Show(@"超出当前最大页数", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 改变每页展示数据长度
        /// </summary>
        void ucPageDemo_ChangedPageSizeEvent()
        {
            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;
            this.readAlarmLog(1, craneNO, timeStart, timeEnd);
        }
        /// <summary>
        /// 页数改变按钮(最前页,最后页,上一页,下一页)
        /// </summary>
        /// <param name="current"></param>
        void ucPageDemo_ClickPageButtonEvent(int current)
        {
            if (totalPages != 0 && current > totalPages)
            {
                current = 1;
            }
            DateTime timeStart = dateTimePicker_Start.Value;
            DateTime timeEnd = dateTimePicker_End.Value;
            string craneNO = comboBox_ShipLotNo.Text;
            this.readAlarmLog(current, craneNO, timeStart, timeEnd);
        }
        #endregion

        private void bt_TodayTime_Click(object sender, EventArgs e)
        {
            GetToDayTime();
        }
        private void bt_WeekTime_Click(object sender, EventArgs e)
        {
            GetWeekTime();
        }
        private void bt_MonthlyTime_Click(object sender, EventArgs e)
        {
            GetMonthlyTime();
        }
        private void bt_QuarterlyTime_Click(object sender, EventArgs e)
        {
            GetQuarterlyTime();
        }
        private void bt_AnnualTime_Click(object sender, EventArgs e)
        {
            GetAnnualTime();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = saveFileDialog1.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                this.Invoke(new MethodInvoker(delegate ()
                {

                    Export2Excel(GridAlarmLog, saveFileDialog1.FileName);

                }));
                ParkClassLibrary.HMILogger.WriteLog("报警信息管理", "导出", ParkClassLibrary.LogLevel.Info, this.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
