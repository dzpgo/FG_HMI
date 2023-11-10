using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACS;
using System.Runtime.InteropServices;
using UACSDAL;
using System.Windows.Forms.DataVisualization.Charting;

namespace UACSView
{
    /// <summary>
    /// 行车作业统计
    /// 行车作业结果分析
    /// </summary>
    public partial class StatForm : FormBase
    {
        private DataBaseHelper m_dbHelper;
        #region 连接数据库
        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;

        private static Baosight.iSuperframe.Common.IDBHelper DBHelper
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

                    }
                }
                return dbHelper;
            }
        }
        #endregion
        public StatForm()
        {
            InitializeComponent();
            this.Load += StatForm_Load;
        }

        private Dictionary<string, GRIDBase> dicGRID = new Dictionary<string, GRIDBase>();

        void StatForm_Load(object sender, EventArgs e)
        {
            m_dbHelper = new DataBaseHelper();
            m_dbHelper.OpenDB(DBHelper.ConnectionString);


            this.ucPage1.CurrentPage = 1;
            this.ucPage1.PageSize = Convert.ToInt32(this.ucPage1.CboPageSize.Text);
            this.ucPage1.TotalPages = 1;
            this.ucPage1.ClickPageButtonEvent += ucPage1_ClickPageButtonEvent;
            this.ucPage1.ChangedPageSizeEvent += ucPage1_ChangedPageSizeEvent;
            this.ucPage1.JumpPageEvent += ucPage1_JumpPageEvent;


            //绑定下拉框
            BindCombox();
            //this.ucPageDemo.CurrentPage = 1;
            //this.ucPageDemo.PageSize = Convert.ToInt32(this.ucPageDemo.CboPageSize.Text);
            //this.ucPageDemo.TotalPages = 1;
            //this.ucPageDemo.ClickPageButtonEvent += ucPageDemo_ClickPageButtonEvent;
            //this.ucPageDemo.ChangedPageSizeEvent += ucPageDemo_ChangedPageSizeEvent;
            //this.ucPageDemo.JumpPageEvent += ucPageDemo_JumpPageEvent;
            MatPie();
            //MatPieData();
            //自动化率
            GetUACS_ORDER_OPER(true, 1);
            //计划用时统计
            GetUACS_L3_MAT_OUT_INFO(1);
            //折线图
            MatSpline();

        }
        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        private void BindCombox()
        {
            //绑定行车号
            DataTable dtCRANE_NO = GetCRANE_NO(true);
            bindCombox(this.cbb_CRANE_NO, dtCRANE_NO, true);
            //绑定行车模式
            DataTable dtCRANE_MODE = GetCRANE_MODE(true);
            bindCombox(this.cbb_CRANE_MODE, dtCRANE_MODE, true);

        }

        private Dictionary<string, DataTable> datalist = new Dictionary<string, DataTable>();

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            GetUACS_ORDER_OPER(false, 1);
            GetUACS_L3_MAT_OUT_INFO(1);
            //折线图
            MatSpline();
        }
        /// <summary>
        /// 查询指令数据
        /// </summary>
        /// <param name="isLoad">是否是初始化 true=是初始化，false=不是初始化</param>
        private void GetUACS_ORDER_OPER(bool isLoad, int currentPage)
        {
            if (m_dbHelper == null)
                return;
            string craneNo = this.cbb_CRANE_NO.SelectedValue.ToString();  //跨号
            string craneMode = this.cbb_CRANE_MODE.SelectedValue.ToString();  //行车模式
            string recTime1 = this.dateTimePicker1.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2.Value.ToString("yyyyMMdd235959");  //结束时间
            var sqlText = @"SELECT 
                                ROW_NUMBER() OVER (ORDER BY AUTO_FLAG) AS RowNumber,
                                CASE
                                    WHEN AUTO_FLAG = 0 THEN '未开始'
                                    WHEN AUTO_FLAG = 1 THEN '全自动'
                                    WHEN AUTO_FLAG = 2 THEN '半自动'
                                    ELSE '其他'
                                END AS AutoFlag,
                                COUNT(*) AS OrderCount,
                                CAST(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM UACS_L3_MAT_OUT_INFO WHERE AUTO_FLAG IS NOT NULL AND REC_TIME >= '{0}' AND REC_TIME <= '{1}') AS DECIMAL(10, 2)) AS PERCENTAGE
                            FROM UACS_L3_MAT_OUT_INFO
                            WHERE AUTO_FLAG IS NOT NULL AND REC_TIME >= '{2}' AND REC_TIME <= '{3}' 
                            GROUP BY AUTO_FLAG
                            ORDER BY AUTO_FLAG;";
            //sqlText += "WHERE REC_TIME >= '{2}' and REC_TIME <= '{3}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2, recTime1, recTime2);
            DataTable dataTable = new DataTable();
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dataTable.Load(rdr);
            }
            string[] CodeNameList = new string[] { };
            double[] NumberList = new double[] { };
            List<string> codeNames = CodeNameList.ToList();
            List<double> numbers = NumberList.ToList();
            DataTable dtSource = InitDataTable(dataGridView1);
            var numberTotal = 0.0;
            foreach (DataRow item in dataTable.Rows)
            {
                dtSource.Rows.Add(item["RowNumber"].ToString(), item["AutoFlag"].ToString(), item["OrderCount"].ToString(), item["PERCENTAGE"].ToString() + "%");
                codeNames.Add(item["AutoFlag"].ToString());
                numbers.Add(Convert.ToDouble(item["OrderCount"]));
                numberTotal = numberTotal + Convert.ToDouble(item["OrderCount"].ToString());
            }

            dataGridView1.DataSource = dtSource;

            foreach (Series series in this.chart1.Series)
            {
                series.Points.Clear();
            }
            if (codeNames.Count > 0)
            {
                CodeNameList = codeNames.ToArray();
            }
            if (numbers.Count > 0)
            {
                NumberList = numbers.ToArray();
            }
            chart1.Titles[1].Text = "合计：" + numberTotal + " 槽";
            chart1.Series[0].Points.DataBindXY(CodeNameList, NumberList);
        }
        /// <summary>
        /// 获取计划数据
        /// </summary>
        /// <param name="currentPage"></param>
        private void GetUACS_L3_MAT_OUT_INFO(int currentPage)
        {
            string recTime1 = this.dateTimePicker1.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER (ORDER BY PLAN_NO DESC) AS ROWNUM,tab.* FROM ( ";
            sqlText += @"SELECT PLAN_NO, BOF_NO,
                                CASE
                                    WHEN AUTO_FLAG = 0 THEN '未开始'
                                    WHEN AUTO_FLAG = 1 THEN '全自动'
                                    WHEN AUTO_FLAG = 2 THEN '半自动'
                                    ELSE '其他'
                                END AS AutoFlag2, REC_TIME FROM UACS_L3_MAT_OUT_INFO 
                            WHERE AUTO_FLAG IS NOT NULL ";
            sqlText += "AND REC_TIME >= '{0}' AND REC_TIME <= '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            sqlText += "ORDER BY PLAN_NO DESC ";
            sqlText += " ) tab ) WHERE ROWNUM BETWEEN ((" + currentPage + " - 1) * " + this.ucPage1.PageSize + ") + 1 AND " + currentPage + " *  " + this.ucPage1.PageSize;
            DataTable dtUacsL3MatOutInfo = new DataTable();
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dtUacsL3MatOutInfo.Load(rdr);
            }
            string[] CodeNameList = new string[] { };
            double[] NumberList = new double[] { };
            List<string> codeNames = CodeNameList.ToList();
            List<double> numbers = NumberList.ToList();
            var planNo = string.Empty;
            DataTable dtSource = InitDataTable(dataGridView2);
            foreach (DataRow item in dtUacsL3MatOutInfo.Rows)
            {
                planNo = planNo + "'" + item["PLAN_NO"].ToString() + "',";
            }
            if (!string.IsNullOrEmpty(planNo) && planNo.EndsWith(","))
            {
                planNo = planNo.Remove(planNo.Length - 1);   //使用 Remove 方法删除最后一个字符

                var sqlText2 = @"SELECT CRANE_NO,PLAN_NO,CMD_SEQ,REQ_WEIGHT,START_TIME,UPD_TIME 
                                 FROM UACS_ORDER_QUEUE ";
                sqlText2 += "WHERE PLAN_NO IN ({0}) ;";
                sqlText2 = string.Format(sqlText2, planNo);
                DataTable dtUacsOrderQueue = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText2))
                {
                    dtUacsOrderQueue.Load(rdr);
                }
                foreach (DataRow drMatOut in dtUacsL3MatOutInfo.Rows)
                {
                    var RaneNo = string.Empty;
                    var CmdSeq = 0;
                    var Weight = 0.0;
                    var LoadingTime = 0.0;
                    var FiftyFiveTonTime = 0.0;
                    var SeventyTwoTonTime = 0.0;

                    DateTime? PlanoutStart = null;
                    DateTime? PlanoutEnd = null;
                    foreach (DataRow drOrder in dtUacsOrderQueue.Rows)
                    {
                        if (drMatOut["PLAN_NO"].ToString().Equals(drOrder["PLAN_NO"].ToString()))
                        {
                            RaneNo = drOrder["CRANE_NO"].ToString();
                            CmdSeq += Convert.ToInt32(drOrder["CMD_SEQ"]);
                            Weight += Convert.ToDouble(drOrder["REQ_WEIGHT"]);
                            if (drOrder["START_TIME"] != System.DBNull.Value && PlanoutStart == null)
                            {
                                PlanoutStart = Convert.ToDateTime(drOrder["START_TIME"]);
                            }
                            if (drOrder["START_TIME"] != System.DBNull.Value && PlanoutStart != null && Convert.ToDateTime(drOrder["START_TIME"]) < PlanoutStart)
                            {
                                PlanoutStart = Convert.ToDateTime(drOrder["START_TIME"]);
                            }
                            if (drOrder["UPD_TIME"] != System.DBNull.Value && PlanoutEnd == null)
                            {
                                PlanoutEnd = Convert.ToDateTime(drOrder["UPD_TIME"]);
                            }
                            if (drOrder["UPD_TIME"] != System.DBNull.Value && PlanoutEnd != null && Convert.ToDateTime(drOrder["UPD_TIME"]) > PlanoutEnd)
                            {
                                PlanoutEnd = Convert.ToDateTime(drOrder["UPD_TIME"]);
                            }
                        }
                    }
                    var T55 = 55000;
                    var T72 = 72000;
                    if (PlanoutStart.HasValue && PlanoutEnd.HasValue)
                    {
                        // 计算两个日期之间的时间差
                        TimeSpan timeDifference = PlanoutEnd.Value - PlanoutStart.Value;
                        LoadingTime = timeDifference.TotalMinutes;
                        // 要根据60吨和40分钟的数据来计算每55吨的用时，您可以使用以下公式：
                        // 用时 = (给定重量 / 60吨) *给定时间
                        FiftyFiveTonTime = (T55 / Weight) * LoadingTime;
                        SeventyTwoTonTime = (T72 / Weight) * LoadingTime;
                    }
                    dtSource.Rows.Add(drMatOut["ROWNUM"].ToString(), drMatOut["AutoFlag2"].ToString(), RaneNo, drMatOut["PLAN_NO"].ToString(), drMatOut["BOF_NO"].ToString(), CmdSeq, Weight, Math.Round(LoadingTime, 2) + " 分钟", Math.Round(FiftyFiveTonTime, 2) + " 分钟", Math.Round(SeventyTwoTonTime, 2) + " 分钟", drMatOut["REC_TIME"].ToString(), drMatOut["TotalRows"].ToString());
                }
            }
            //dataGridView2.DataSource = dtSource;

            dataGridView2.DataSource = GetDataPage(dtSource, currentPage);
        }

        #region 图形

        /// <summary>
        /// 饼状图 物料进料次数分析
        /// </summary>
        private void MatPie()
        {
            #region 饼状图
            string[] x = new string[] { "成都大队", "广东大队", "广西大队", "云南大队", "上海大队", "苏州大队", "深圳大队", "北京大队", "湖北大队", "湖南大队", "重庆大队", "辽宁大队" };
            double[] y = new double[] { 589, 598, 445, 654, 884, 457, 941, 574, 745, 854, 684, 257 };
            string[] z = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
            //标题
            //this.chart1.ChartAreas.Clear();
            chart1.Titles.Add("自动化率");
            chart1.Titles[0].ForeColor = Color.Blue;
            chart1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart1.Titles[0].Alignment = ContentAlignment.TopCenter;
            //chart1.Titles[0].Visible = false;
            chart1.Titles.Add("合计： 槽");
            chart1.Titles[1].ForeColor = Color.Blue;
            chart1.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            chart1.Titles[1].Alignment = ContentAlignment.TopRight;
            //chart1.Titles[1].Visible = false;

            //控件背景
            chart1.BackColor = SystemColors.Control;
            //图表区背景
            chart1.ChartAreas[0].BackColor = SystemColors.Control;
            chart1.ChartAreas[0].BorderColor = SystemColors.Control;
            //X轴标签间距
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;

            //X坐标轴颜色
            chart1.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a");
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Blue;
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //X坐标轴标题
            chart1.ChartAreas[0].AxisX.Title = "数量 (槽)";
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;
            chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            chart1.ChartAreas[0].AxisX.ToolTip = "数量 (槽)";
            //X轴网络线条
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Blue;
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart1.ChartAreas[0].AxisY.Title = "数量 (槽)";
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Blue;
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.ToolTip = "数量 (槽)";
            //Y轴网格线条
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart1.ChartAreas[0].AxisY2.LineColor = SystemColors.Control;

            //背景渐变
            chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;

            //chart1.Legends.Clear();
            //图例样式
            Legend legend2 = new Legend("#VALX");
            legend2.Title = "图例";
            legend2.TitleBackColor = SystemColors.Control;
            legend2.BackColor = SystemColors.Control;
            legend2.TitleForeColor = Color.Blue;
            legend2.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend2.ForeColor = Color.Blue;

            chart1.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            //chart1.Series[0].Label = "#VAL";                //设置显示X Y的值
            chart1.Series[0].Label = "#VALX #PERCENT{P2}";
            chart1.Series[0].LabelForeColor = Color.Blue;
            chart1.Series[0].ToolTip = "#VALX：#VAL (槽)";     //鼠标移动到对应点显示数值
            chart1.Series[0].ChartType = SeriesChartType.Pie;    //图类型(折线)

            chart1.Series[0].Color = Color.Lime;
            chart1.Series[0].LegendText = legend2.Name;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].LabelForeColor = Color.Blue;
            chart1.Series[0].CustomProperties = "DrawingStyle = Cylinder";
            chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            chart1.Legends.Add(legend2);
            chart1.Legends[0].Position.Auto = true;
            chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            chart1.Series[0].IsVisibleInLegend = true;
            chart1.Series[0].ShadowOffset = 0;

            //饼图折线
            chart1.Series[0]["PieLineColor"] = "Blue";
            //绑定数据
            chart1.Series[0].Points.DataBindXY(x, y);
            chart1.Series[0].Points[0].Color = Color.Blue;
            //绑定颜色
            chart1.Series[0].Palette = ChartColorPalette.BrightPastel;

            #endregion
        }

        #region 折线图
        /// <summary>
        /// 折线图
        /// </summary>
        private void MatSpline()
        {
            try
            {
                // 假设我们有一个字典，其中每个键值对代表一条折线的数据
                // 键是名称，值是一个包含日期和数据的字典
                Dictionary<string, Dictionary<DateTime, double>> DataDictionary = new Dictionary<string, Dictionary<DateTime, double>>()
                {
                    { "线1", new Dictionary<DateTime, double>() { { DateTime.Today, 0 }, { DateTime.Today.AddDays(1), 1 }, { DateTime.Today.AddDays(2), 2 }, { DateTime.Today.AddDays(3), 3 }, { DateTime.Today.AddDays(4), 4 }, { DateTime.Today.AddDays(5), 5 }, { DateTime.Today.AddDays(6), 6 }, { DateTime.Today.AddDays(7), 7 }, { DateTime.Today.AddDays(8), 8 }, { DateTime.Today.AddDays(9), 9 }, { DateTime.Today.AddDays(10), 10 }, { DateTime.Today.AddDays(11), 9 }, { DateTime.Today.AddDays(12), 8 }, { DateTime.Today.AddDays(13), 7 }, { DateTime.Today.AddDays(14), 6 }, { DateTime.Today.AddDays(15), 8 }, { DateTime.Today.AddDays(16), 9 }, { DateTime.Today.AddDays(17), 10 }, { DateTime.Today.AddDays(18), 9 }, { DateTime.Today.AddDays(19), 8 }, { DateTime.Today.AddDays(20), 7 }, { DateTime.Today.AddDays(21), 6 } } },
                    { "线2", new Dictionary<DateTime, double>() { { DateTime.Today, 2 }, { DateTime.Today.AddDays(1), 3 }, { DateTime.Today.AddDays(2), 4 }, { DateTime.Today.AddDays(3), 5 }, { DateTime.Today.AddDays(4), 6 }, { DateTime.Today.AddDays(5), 4 }, { DateTime.Today.AddDays(6), 7 }, { DateTime.Today.AddDays(7), 8 }, { DateTime.Today.AddDays(8), 9 }, { DateTime.Today.AddDays(9), 10 }, { DateTime.Today.AddDays(10), 6 }, { DateTime.Today.AddDays(11), 7 }, { DateTime.Today.AddDays(12), 9 }, { DateTime.Today.AddDays(13), 10 }, { DateTime.Today.AddDays(14), 2 }, { DateTime.Today.AddDays(15), 9 }, { DateTime.Today.AddDays(16), 10 }, { DateTime.Today.AddDays(17), 6 }, { DateTime.Today.AddDays(18), 7 }, { DateTime.Today.AddDays(19), 9 }, { DateTime.Today.AddDays(20), 10 }, { DateTime.Today.AddDays(21), 2 } } },
                    { "线3", new Dictionary<DateTime, double>() { { DateTime.Today, 4 }, { DateTime.Today.AddDays(1), 6 }, { DateTime.Today.AddDays(2), 8 }, { DateTime.Today.AddDays(3), 2 }, { DateTime.Today.AddDays(4), 3 }, { DateTime.Today.AddDays(5), 6 }, { DateTime.Today.AddDays(6), 9 }, { DateTime.Today.AddDays(7), 10 }, { DateTime.Today.AddDays(8), 5 }, { DateTime.Today.AddDays(9), 6 }, { DateTime.Today.AddDays(10), 7 }, { DateTime.Today.AddDays(11), 8 }, { DateTime.Today.AddDays(12), 7 }, { DateTime.Today.AddDays(13), 12 }, { DateTime.Today.AddDays(14), 9 }, { DateTime.Today.AddDays(15), 5 }, { DateTime.Today.AddDays(16), 6 }, { DateTime.Today.AddDays(17), 7 }, { DateTime.Today.AddDays(18), 8 }, { DateTime.Today.AddDays(19), 7 }, { DateTime.Today.AddDays(20), 12 }, { DateTime.Today.AddDays(21), 9 } } },
                    { "线4", new Dictionary<DateTime, double>() { { DateTime.Today, 5 }, { DateTime.Today.AddDays(1), 7 }, { DateTime.Today.AddDays(2), 9 }, { DateTime.Today.AddDays(3), 4 }, { DateTime.Today.AddDays(4), 5 }, { DateTime.Today.AddDays(5), 7 }, { DateTime.Today.AddDays(6), 8 }, { DateTime.Today.AddDays(7), 9 }, { DateTime.Today.AddDays(8), 10 }, { DateTime.Today.AddDays(9), 8 }, { DateTime.Today.AddDays(10), 8 }, { DateTime.Today.AddDays(11), 8 }, { DateTime.Today.AddDays(12), 11 }, { DateTime.Today.AddDays(13), 9 }, { DateTime.Today.AddDays(14), 8 }, { DateTime.Today.AddDays(15), 7 }, { DateTime.Today.AddDays(16), 8 }, { DateTime.Today.AddDays(17), 9 }, { DateTime.Today.AddDays(18), 10 }, { DateTime.Today.AddDays(19), 11 }, { DateTime.Today.AddDays(20), 12 }, { DateTime.Today.AddDays(21), 10 } } }
                };
                Dictionary<DateTime, double> DataTimeDictionary = new Dictionary<DateTime, double>();
                DataTimeDictionary.Add(DateTime.Today, 0);
                DataTimeDictionary.Add(DateTime.Today.AddDays(1), 1);
                DataDictionary.Clear();
                DataDictionary.Add("线1", DataTimeDictionary);

                Dictionary<string, string> ChartCodeNameDictionary = new Dictionary<string, string>();
                Dictionary<DateTime, DateTime> ChartTimeDictionary = new Dictionary<DateTime, DateTime>();
                //显示名称
                List<string> ChartCodeNameList = new List<string>();
                //显示时间
                List<DateTime> ChartTimeList = new List<DateTime>();
                //显示数据
                List<OrderDate> ChartDateList = new List<OrderDate>();
                //显示数据
                List<OrderDate> ChartDateLists = new List<OrderDate>();

                #region 访问数据库库            

                string recTime1 = this.dateTimePicker1.Value.Day.Equals(DateTime.Now.Day) ? this.dateTimePicker1.Value.AddDays(-7).ToString("yyyyMMdd000000") : this.dateTimePicker1.Value.ToString("yyyyMMdd000000");  //开始时间
                string recTime2 = this.dateTimePicker2.Value.ToString("yyyyMMdd235959");  //结束时间
                var sqlText = @"SELECT ROW_NUMBER() OVER (ORDER BY PLAN_NO DESC) AS RowNumber,
                            PLAN_NO, BOF_NO,
                                CASE
                                    WHEN AUTO_FLAG = 0 THEN '未开始'
                                    WHEN AUTO_FLAG = 1 THEN '全自动'
                                    WHEN AUTO_FLAG = 2 THEN '半自动'
                                    ELSE '其他'
                                END AS AutoFlag2, REC_TIME FROM UACS_L3_MAT_OUT_INFO 
                            WHERE AUTO_FLAG IS NOT NULL ";
                sqlText += "AND REC_TIME >= '{0}' AND REC_TIME <= '{1}' ";
                sqlText = string.Format(sqlText, recTime1, recTime2);
                sqlText += "ORDER BY PLAN_NO DESC ;";
                DataTable dtUacsL3MatOutInfo = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    dtUacsL3MatOutInfo.Load(rdr);
                }
                string[] CodeNameList = new string[] { };
                double[] NumberList = new double[] { };
                List<string> codeNames = CodeNameList.ToList();
                List<double> numbers = NumberList.ToList();
                var planNo = string.Empty;
                DataTable dtSource = InitDataTable(dataGridView2);
                foreach (DataRow item in dtUacsL3MatOutInfo.Rows)
                {
                    planNo = planNo + "'" + item["PLAN_NO"].ToString() + "',";
                }
                if (!string.IsNullOrEmpty(planNo) && planNo.EndsWith(","))
                {
                    planNo = planNo.Remove(planNo.Length - 1);   //使用 Remove 方法删除最后一个字符

                    var sqlText2 = @"SELECT CRANE_NO,PLAN_NO,CMD_SEQ,REQ_WEIGHT,START_TIME,UPD_TIME 
                                 FROM UACS_ORDER_QUEUE ";
                    sqlText2 += "WHERE PLAN_NO IN ({0}) ;";
                    sqlText2 = string.Format(sqlText2, planNo);
                    DataTable dtUacsOrderQueue = new DataTable();
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText2))
                    {
                        dtUacsOrderQueue.Load(rdr);
                    }
                    foreach (DataRow drMatOut in dtUacsL3MatOutInfo.Rows)
                    {
                        var RaneNo = string.Empty;
                        var CmdSeq = 0;
                        var Weight = 0.0;
                        var LoadingTime = 0.0;
                        var FiftyFiveTonTime = 0.0;
                        var SeventyTwoTonTime = 0.0;

                        DateTime? PlanoutStart = null;
                        DateTime? PlanoutEnd = null;
                        foreach (DataRow drOrder in dtUacsOrderQueue.Rows)
                        {
                            if (drMatOut["PLAN_NO"].ToString().Equals(drOrder["PLAN_NO"].ToString()))
                            {
                                RaneNo = drOrder["CRANE_NO"].ToString();
                                CmdSeq += Convert.ToInt32(drOrder["CMD_SEQ"]);
                                Weight += Convert.ToDouble(drOrder["REQ_WEIGHT"]);
                                if (drOrder["START_TIME"] != System.DBNull.Value && PlanoutStart == null)
                                {
                                    PlanoutStart = Convert.ToDateTime(drOrder["START_TIME"]);
                                }
                                if (drOrder["START_TIME"] != System.DBNull.Value && PlanoutStart != null && Convert.ToDateTime(drOrder["START_TIME"]) < PlanoutStart)
                                {
                                    PlanoutStart = Convert.ToDateTime(drOrder["START_TIME"]);
                                }
                                if (drOrder["UPD_TIME"] != System.DBNull.Value && PlanoutEnd == null)
                                {
                                    PlanoutEnd = Convert.ToDateTime(drOrder["UPD_TIME"]);
                                }
                                if (drOrder["UPD_TIME"] != System.DBNull.Value && PlanoutEnd != null && Convert.ToDateTime(drOrder["UPD_TIME"]) > PlanoutEnd)
                                {
                                    PlanoutEnd = Convert.ToDateTime(drOrder["UPD_TIME"]);
                                }
                            }
                        }
                        var T55 = 55000;
                        var T72 = 72000;
                        if (PlanoutStart.HasValue && PlanoutEnd.HasValue)
                        {
                            // 计算两个日期之间的时间差
                            TimeSpan timeDifference = PlanoutEnd.Value - PlanoutStart.Value;
                            LoadingTime = timeDifference.TotalMinutes;
                            // 要根据60吨和40分钟的数据来计算每55吨的用时，您可以使用以下公式：
                            // 用时 = (给定重量 / 60吨) *给定时间
                            FiftyFiveTonTime = (T55 / Weight) * LoadingTime;
                            SeventyTwoTonTime = (T72 / Weight) * LoadingTime;
                        }
                        dtSource.Rows.Add(drMatOut["RowNumber"].ToString(), drMatOut["AutoFlag2"].ToString(), RaneNo, drMatOut["PLAN_NO"].ToString(), drMatOut["BOF_NO"].ToString(), CmdSeq, Weight, Math.Round(LoadingTime, 2), Math.Round(FiftyFiveTonTime, 2), Math.Round(SeventyTwoTonTime, 2), drMatOut["REC_TIME"].ToString());
                    }
                }
                foreach (DataRow item in dtSource.Rows)
                {
                    OrderDate od = new OrderDate();
                    if (item["REC_TIME"] != System.DBNull.Value)
                    {
                        od.DaY = Convert.ToDateTime(item["REC_TIME"]);
                    }
                    if (item["CRANE_NO"] != System.DBNull.Value)
                    {
                        od.CRANE_NO = item["CRANE_NO"].ToString();
                    }
                    if (item["LoadingTime"] != System.DBNull.Value)
                    {
                        od.AverageTime = Convert.ToDouble(item["LoadingTime"]);
                    }

                    ChartDateList.Add(od);
                    if (item["CRANE_NO"] != System.DBNull.Value && !string.IsNullOrEmpty(item["CRANE_NO"].ToString()) && !ChartCodeNameDictionary.ContainsKey(item["CRANE_NO"].ToString()))
                    {
                        ChartCodeNameDictionary.Add(item["CRANE_NO"].ToString(), item["CRANE_NO"].ToString());
                        ChartCodeNameList.Add(item["CRANE_NO"].ToString());
                    }
                    if (item["REC_TIME"] != System.DBNull.Value && !ChartTimeDictionary.ContainsKey(Convert.ToDateTime(Convert.ToDateTime(item["REC_TIME"]).ToString("yyyy-MM-dd"))))
                    {
                        ChartTimeDictionary.Add(Convert.ToDateTime(Convert.ToDateTime(item["REC_TIME"]).ToString("yyyy-MM-dd")), Convert.ToDateTime(Convert.ToDateTime(item["REC_TIME"]).ToString("yyyy-MM-dd")));
                        ChartTimeList.Add(Convert.ToDateTime(Convert.ToDateTime(item["REC_TIME"]).ToString("yyyy-MM-dd")));
                    }
                }

                // 使用 Lambda 表达式进行正序排序
                ChartCodeNameList.Sort((s1, s2) => string.Compare(s1, s2));
                ChartTimeList.Sort((date1, date2) => date1.CompareTo(date2));

                DataTimeDictionary.Clear();
                DataDictionary.Clear();
                foreach (string name in ChartCodeNameList)
                {
                    DataTimeDictionary.Clear();
                    foreach (DateTime day in ChartTimeList)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("CRANE_NO", typeof(string));
                        dataTable.Columns.Add("LoadingTime", typeof(double));
                        dataTable.Columns.Add("REC_TIME", typeof(DateTime));
                        OrderDate od = new OrderDate();
                        od.DaY = day;
                        od.CRANE_NO = name.ToString();
                        foreach (DataRow item in dtSource.Rows)
                        {
                            if (name.ToString().Equals(item["CRANE_NO"].ToString()) && Convert.ToDateTime(day.ToString("yyyy-MM-dd")) == Convert.ToDateTime(Convert.ToDateTime(item["REC_TIME"]).ToString("yyyy-MM-dd")))
                            {
                                DataRow row = dataTable.NewRow();
                                row["CRANE_NO"] = item["CRANE_NO"].ToString();
                                row["LoadingTime"] = Convert.ToDouble(item["LoadingTime"]);
                                row["REC_TIME"] = Convert.ToDateTime(item["REC_TIME"]);
                                dataTable.Rows.Add(row);
                            }
                        }
                        double average = CalculateColumnAverage(dataTable, "LoadingTime");
                        od.AverageTime = Math.Round(average, 2);
                        ChartDateLists.Add(od);
                        var test = Convert.ToDateTime(day.ToString("yyyy-MM-dd"));
                        DataTimeDictionary.Add(Convert.ToDateTime(day.ToString("yyyy-MM-dd")), Math.Round(average, 2));
                    }
                    DataDictionary.Add(name + "#行车", new Dictionary<DateTime, double>(DataTimeDictionary));
                }
                #endregion

                GetChart(DataDictionary);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        /// <summary>
        /// 折线图
        /// </summary>
        /// <param name="data"></param>
        private void GetChart(Dictionary<string, Dictionary<DateTime, double>> data)
        {
            // 获取chart控件
            Chart chart3 = this.chart3;
            chart3.Dock = DockStyle.Fill;
            chart3.ChartAreas[0].AxisY.Minimum = 0; // 最小值
            chart3.ChartAreas[0].AxisY.Maximum = 100; // 最大值
            // 控件背景
            this.chart3.BackColor = SystemColors.Control;
            // 图表区背景
            chart3.ChartAreas[0].BackColor = SystemColors.Control;
            chart3.ChartAreas[0].BorderColor = SystemColors.Control;
            // 设置标题
            chart3.Titles.Clear();
            chart3.Titles.Add("S01");
            chart3.Titles[0].Text = "装料平均时间（天）";
            chart3.Titles[0].ForeColor = Color.Blue;
            chart3.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            //X坐标轴标题
            chart3.ChartAreas[0].AxisX.Title = "日期 (天)";
            chart3.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart3.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;
            chart3.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            chart3.ChartAreas[0].AxisX.ToolTip = "日期 (天)";
            //Y坐标轴标题
            chart3.ChartAreas[0].AxisY.Title = "用时 (分)";
            chart3.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart3.ChartAreas[0].AxisY.TitleForeColor = Color.Blue;
            chart3.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart3.ChartAreas[0].AxisY.ToolTip = "用时 (分)";

            // 设置X轴的数据类型为日期
            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
            chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
            chart3.ChartAreas[0].AxisX.Interval = 1;

            // 设置X轴和Y轴的网格线颜色为半透明
            chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(16, Color.Black);
            chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(16, Color.Black);

            chart3.Series.Clear();
            // 使用循环来添加多条折线
            foreach (var kvp in data)
            {
                // 创建一个新的Series（系列）用于存放数据
                Series series = new Series(kvp.Key);

                // 设置Series的类型为Line（折线图）
                series.ChartType = SeriesChartType.Line;

                // 设置Series的IsValueShownAsLabel属性为true，这将在每个数据点上显示其值
                series.IsValueShownAsLabel = true;

                // 设置数据点的标记样式
                series.MarkerStyle = MarkerStyle.Circle;

                // 设置数据点的标记大小
                series.MarkerSize = 4;

                // 设置数据点上的文本颜色为红色
                //series.LabelForeColor = Color.Red;

                // 添加数据到Series中
                foreach (var point in kvp.Value)
                {
                    // 注意这里我们使用了DateTime类型的数据点
                    series.Points.AddXY(point.Key, point.Value);
                }

                // 将Series添加到chart的Series集合中
                chart3.Series.Add(series);
            }
            this.chart3.DataBind();
            // 刷新图表
            this.chart3.Update();
        }

        #endregion

        /// <summary>
        /// 计算时间平均值
        /// </summary>
        /// <param name="timeValues"></param>
        /// <returns></returns>
        private double CalculateColumnAverage(DataTable table, string columnName)
        {
            double sum = 0;
            int count = 0;

            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull(columnName))
                {
                    sum += Convert.ToDouble(row[columnName]);
                    count++;
                }
            }

            if (count > 0)
            {
                return sum / count;
            }
            else
            {
                // 如果没有有效数据，则返回0或其他你认为合适的默认值
                return 0;
            }
        }
        #endregion


        #region 业务数据处理
        /// <summary>
        /// 业务数据处理
        /// </summary>
        /// <param name="T0"></param>
        /// <returns></returns>
        private static DataTable GetCrane(DataTable T0)
        {
            DataTable returnData = new DataTable();//最终数据
            DataTable T1 = new DataTable();
            T1 = T0.Copy();
            T1.Rows.Clear();
            returnData = T0.Copy();
            returnData.Rows.Clear();

            #region 统计行车
            Dictionary<string, string> dicList = new Dictionary<string, string>();
            foreach (DataRow item in T0.Rows)
            {
                if (item["CRANE_MODE"].Equals("1"))
                {
                    item["CRANE_MODENAME"] = "远程操控";
                }
                if (item["CRANE_MODE"].Equals("2"))
                {
                    item["CRANE_MODENAME"] = "人工";
                }
                if (item["CRANE_MODE"].Equals("4"))
                {
                    item["CRANE_MODENAME"] = "自动";
                }
                if (item["CRANE_MODE"].Equals("5"))
                {
                    item["CRANE_MODENAME"] = "等待";
                }
                if (!dicList.ContainsKey(item["CRANE_NO"].ToString()))
                {
                    T1.ImportRow(item);//这是加入的是第一行
                    dicList.Add(item["CRANE_NO"].ToString(), item["CRANE_MODENAME"].ToString());
                }
            }
            dicList.Clear();
            #endregion

            #region 统计行车操作模式
            foreach (DataRow t0 in T1.Rows)
            {
                foreach (DataRow t1 in T0.Rows)
                {
                    //行车号相等
                    if (t0["CRANE_NO"].Equals(t1["CRANE_NO"]))
                    {
                        DataTable test = new DataTable();
                        test = returnData.Copy();
                        int count = 0;
                        var bbb = t1["CRANE_MODE"];
                        for (int i = 0; i < test.Rows.Count; i++)
                        {
                            var aaa = test.Rows[i]["CRANE_MODE"];
                            var ccc = test.Rows[i]["CRANE_NO"];
                            if (test.Rows[i]["CRANE_NO"].Equals(t1["CRANE_NO"]) && test.Rows[i]["CRANE_MODE"].Equals(t1["CRANE_MODE"]))
                            {
                                count = Convert.ToInt32(returnData.Rows[i]["COUNT"]) + 1;
                                returnData.Rows[i]["COUNT"] = count;
                            }
                        }
                        if (returnData.Rows.Count == 0)
                        {
                            returnData.ImportRow(t1);
                        }
                        else
                        {
                            if (count == 0)
                            {
                                returnData.ImportRow(t1);
                            }
                        }
                    }
                }
            }
            #endregion

            #region 统计总数
            foreach (DataRow dr0 in T1.Rows)
            {
                foreach (DataRow dr1 in returnData.Rows)
                {
                    //行车号相等
                    if (dr0["CRANE_NO"].Equals(dr1["CRANE_NO"]))
                    {
                        var ddd = dr0["PERCENTAGE"];
                        if (string.IsNullOrEmpty(ddd.ToString()))
                        {
                            dr0["PERCENTAGE"] = "0";
                        }
                        //统计总数，如果等于空就赋值0，否则PERCENTAGE+COUNT                        
                        dr0["PERCENTAGE"] = Convert.ToString(Convert.ToInt32(dr0["PERCENTAGE"].ToString()) + Convert.ToInt32(dr1["COUNT"].ToString()));
                    }
                }
            }
            #endregion


            #region 计算百分比
            foreach (DataRow dr0 in T1.Rows)
            {
                double allCount = Convert.ToDouble(dr0["PERCENTAGE"]);
                foreach (DataRow dr1 in returnData.Rows)
                {
                    if (dr1["CRANE_NO"].Equals(dr0["CRANE_NO"]))
                    {
                        double PassCount = Convert.ToDouble(dr1["COUNT"]);
                        var res = ChinaRound((double)Math.Round(PassCount / allCount * 100, 1), 2).ToString() + "%";
                        dr1["PERCENTAGE"] = res;
                    }
                }
            }
            #endregion

            return returnData;
        }

        #region 方法

        /// <summary>
        /// 获取行车号
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetCRANE_NO(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            string sqlText = @"SELECT CRANE_NO AS TypeValue, CRANE_NO AS TypeName FROM UACSAPP.UACS_CRANE_DEFINE ";
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
        /// 行车模式 1-远程操控 2-人工 4-自动 5-等待
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetCRANE_MODE(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //dt.Rows.Add("0", "未开始");
            dt.Rows.Add("1", "自动");
            dt.Rows.Add("2", "半自动");
            //dt.Rows.Add("5", "等待");
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            return dt;
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="value">当前值</param>
        /// <param name="decimals">总数</param>
        /// <returns></returns>
        private static double ChinaRound(double value, int decimals)
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }
        /// <summary>
        /// 获取DataGridView数据结构
        /// </summary>
        /// <param name="datagridView"></param>
        /// <returns></returns>
        private DataTable InitDataTable(DataGridView datagridView)
        {
            DataTable dataTable = new DataTable();
            foreach (DataGridViewColumn dgvColumn in datagridView.Columns)
            {
                DataColumn dtColumn = new DataColumn();
                if (!dgvColumn.GetType().Equals(typeof(DataGridViewCheckBoxColumn)))
                {
                    dtColumn.ColumnName = dgvColumn.Name.ToUpper();
                    dtColumn.DataType = typeof(String);
                    dataTable.Columns.Add(dtColumn);
                }
                else
                {
                    dtColumn.ColumnName = dgvColumn.Name.ToUpper();
                    //dtColumn.DataType = typeof(bool);
                    dataTable.Columns.Add(dtColumn);
                }
            }

            return dataTable;
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="control">下拉框控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="showLastIndex">是否显示最后一条（通常用于查询条件中全部）</param>
        private void bindCombox(ComboBox control, DataTable dt, bool showLastIndex)
        {
            control.DataSource = dt;
            control.DisplayMember = "TypeName";
            control.ValueMember = "TypeValue";
            if (showLastIndex)
            {
                control.SelectedIndex = dt.Rows.Count - 1;
            }
        }
        #endregion

        #endregion


        private void StatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_dbHelper.CloseDB();
        }



        private void dataGridViewColor()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // CraneMode1
                if (dataGridView1.Rows[i].Cells["CraneMode1"].Value != System.DBNull.Value)
                {
                    if (dataGridView1.Rows[i].Cells["CraneMode1"].Value.ToString().Trim() == "自动")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells["CraneMode2"].Value != System.DBNull.Value)
                {
                    if (dataGridView2.Rows[i].Cells["CraneMode2"].Value.ToString().Trim() == "自动")
                    {
                        //dataGridView2.Rows[i].Cells["CraneMode2"].Style.BackColor = Color.Blue;
                        dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        }


        /// <summary>
        /// 获取具体数量
        /// orderType 分为
        /// 机组回退（14）
        /// 机组上料（21）
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="orderType">类别</param>
        /// <param name="stockNo">机组号</param>
        /// <param name="mode">行车模式</param>
        /// <returns></returns>
        private string GetNUM(string time, int orderType, string unitNo, int mode)
        {
            string num = "0";
            try
            {
                string sql = @" SELECT  count(*) as num  FROM UACS_CRANE_ORDER_OPER ";
                sql += " WHERE " + time;
                sql += " and (crane_no='6_1' or crane_no = '6_2' or crane_no = '6_3' or crane_no = '6_4' or crane_no = '6_5' or crane_no = '6_6' or crane_no = '6_7' or crane_no = '6_8' or crane_no = '6_9' or crane_no = '6_10')  and ORDER_TYPE = " + orderType + " ";
                sql += " and STOCK_NO like '" + unitNo + "%' ";
                sql += " and crane_mode = " + mode + " ";

                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["num"] != DBNull.Value)
                        {
                            num = rdr["num"].ToString();
                        }
                        else
                        {
                            num = "0";
                        }

                    }
                }

                return num;

            }
            catch (Exception er)
            {
                return "0";
            }
        }


        private void VisibleColumn(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["num"].Value != System.DBNull.Value)
                {
                    if (dgv.Rows[i].Cells["num"].Value.ToString().Trim() == "0")
                    {
                        dgv.Rows[i].Visible = false;
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.ShowDialog();
                this.Invoke(new MethodInvoker(delegate ()
                {

                    Export2Excel(GetExportData(), saveFileDialog1.FileName);

                }));
                GetUACS_L3_MAT_OUT_INFO(1);
                ParkClassLibrary.HMILogger.WriteLog("行车作业结果分析", "行车统计导出", ParkClassLibrary.LogLevel.Info, this.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 获取导出数据
        /// </summary>
        private DataGridView GetExportData()
        {
            string recTime1 = this.dateTimePicker1.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER (ORDER BY PLAN_NO DESC) AS ROWNUM,tab.* FROM ( ";
            sqlText += @"SELECT PLAN_NO, BOF_NO,
                                CASE
                                    WHEN AUTO_FLAG = 0 THEN '未开始'
                                    WHEN AUTO_FLAG = 1 THEN '全自动'
                                    WHEN AUTO_FLAG = 2 THEN '半自动'
                                    ELSE '其他'
                                END AS AutoFlag2, REC_TIME FROM UACS_L3_MAT_OUT_INFO 
                            WHERE AUTO_FLAG IS NOT NULL ";
            sqlText += "AND REC_TIME >= '{0}' AND REC_TIME <= '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            sqlText += "ORDER BY PLAN_NO DESC ";
            sqlText += " ) tab ) ";
            DataTable dtUacsL3MatOutInfo = new DataTable();
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dtUacsL3MatOutInfo.Load(rdr);
            }
            string[] CodeNameList = new string[] { };
            double[] NumberList = new double[] { };
            List<string> codeNames = CodeNameList.ToList();
            List<double> numbers = NumberList.ToList();
            var planNo = string.Empty;
            DataTable dtSource = InitDataTable(dataGridView2);
            foreach (DataRow item in dtUacsL3MatOutInfo.Rows)
            {
                planNo = planNo + "'" + item["PLAN_NO"].ToString() + "',";
            }
            if (!string.IsNullOrEmpty(planNo) && planNo.EndsWith(","))
            {
                planNo = planNo.Remove(planNo.Length - 1);   //使用 Remove 方法删除最后一个字符

                var sqlText2 = @"SELECT CRANE_NO,PLAN_NO,CMD_SEQ,REQ_WEIGHT,START_TIME,UPD_TIME 
                                 FROM UACS_ORDER_QUEUE ";
                sqlText2 += "WHERE PLAN_NO IN ({0}) ;";
                sqlText2 = string.Format(sqlText2, planNo);
                DataTable dtUacsOrderQueue = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText2))
                {
                    dtUacsOrderQueue.Load(rdr);
                }
                foreach (DataRow drMatOut in dtUacsL3MatOutInfo.Rows)
                {
                    var RaneNo = string.Empty;
                    var CmdSeq = 0;
                    var Weight = 0.0;
                    var LoadingTime = 0.0;
                    var FiftyFiveTonTime = 0.0;
                    var SeventyTwoTonTime = 0.0;

                    DateTime? PlanoutStart = null;
                    DateTime? PlanoutEnd = null;
                    foreach (DataRow drOrder in dtUacsOrderQueue.Rows)
                    {
                        if (drMatOut["PLAN_NO"].ToString().Equals(drOrder["PLAN_NO"].ToString()))
                        {
                            RaneNo = drOrder["CRANE_NO"].ToString();
                            CmdSeq += Convert.ToInt32(drOrder["CMD_SEQ"]);
                            Weight += Convert.ToDouble(drOrder["REQ_WEIGHT"]);
                            if (drOrder["START_TIME"] != System.DBNull.Value && PlanoutStart == null)
                            {
                                PlanoutStart = Convert.ToDateTime(drOrder["START_TIME"]);
                            }
                            if (drOrder["START_TIME"] != System.DBNull.Value && PlanoutStart != null && Convert.ToDateTime(drOrder["START_TIME"]) < PlanoutStart)
                            {
                                PlanoutStart = Convert.ToDateTime(drOrder["START_TIME"]);
                            }
                            if (drOrder["UPD_TIME"] != System.DBNull.Value && PlanoutEnd == null)
                            {
                                PlanoutEnd = Convert.ToDateTime(drOrder["UPD_TIME"]);
                            }
                            if (drOrder["UPD_TIME"] != System.DBNull.Value && PlanoutEnd != null && Convert.ToDateTime(drOrder["UPD_TIME"]) > PlanoutEnd)
                            {
                                PlanoutEnd = Convert.ToDateTime(drOrder["UPD_TIME"]);
                            }
                        }
                    }
                    var T55 = 55000;
                    var T72 = 72000;
                    if (PlanoutStart.HasValue && PlanoutEnd.HasValue)
                    {
                        // 计算两个日期之间的时间差
                        TimeSpan timeDifference = PlanoutEnd.Value - PlanoutStart.Value;
                        LoadingTime = timeDifference.TotalMinutes;
                        // 要根据60吨和40分钟的数据来计算每55吨的用时，您可以使用以下公式：
                        // 用时 = (给定重量 / 60吨) *给定时间
                        FiftyFiveTonTime = (T55 / Weight) * LoadingTime;
                        SeventyTwoTonTime = (T72 / Weight) * LoadingTime;
                    }
                    dtSource.Rows.Add(drMatOut["ROWNUM"].ToString(), drMatOut["AutoFlag2"].ToString(), RaneNo, drMatOut["PLAN_NO"].ToString(), drMatOut["BOF_NO"].ToString(), CmdSeq, Weight, Math.Round(LoadingTime, 2) + " 分钟", Math.Round(FiftyFiveTonTime, 2) + " 分钟", Math.Round(SeventyTwoTonTime, 2) + " 分钟", drMatOut["REC_TIME"].ToString(), drMatOut["TotalRows"].ToString());
                }
            }
            dataGridView2.DataSource = dtSource;
            return dataGridView2;
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


        #region 分页

        //int totalPages = 0;

        ///// <summary>
        ///// 数据分页
        ///// </summary>
        ///// <param name="dtResult"></param>
        ///// <returns></returns>
        //private DataTable GetDataPage(DataTable dtResult, int currentPage, int gvd)
        //{
        //    totalPages = 0;
        //    int totalRows = 0;
        //    if (gvd == 1)
        //    {
        //        if (null == dtResult || dtResult.Rows.Count == 0)
        //        {
        //            this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", "1", "1");
        //            this.ucPageDemo.TotalRows.Text = @"0";
        //            this.ucPageDemo.CurrentPage = 1;
        //            this.ucPageDemo.TotalPages = 1;
        //        }
        //        else
        //        {
        //            totalRows = Convert.ToInt32(dtResult.Rows[0]["TotalRows"].ToString());
        //            totalPages = totalRows % this.ucPageDemo.PageSize == 0 ? totalRows / this.ucPageDemo.PageSize : (totalRows / this.ucPageDemo.PageSize) + 1;
        //            this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", currentPage, totalPages);
        //            this.ucPageDemo.TotalRows.Text = totalRows.ToString();
        //            this.ucPageDemo.CurrentPage = currentPage;
        //            this.ucPageDemo.TotalPages = totalPages;
        //        }
        //    }
        //    else
        //    {
        //        if (null == dtResult || dtResult.Rows.Count == 0)
        //        {
        //            this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", "1", "1");
        //            this.ucPageDemo.TotalRows.Text = @"0";
        //            this.ucPageDemo.CurrentPage = 1;
        //            this.ucPageDemo.TotalPages = 1;
        //        }
        //        else
        //        {
        //            totalRows = Convert.ToInt32(dtResult.Rows[0]["TotalRows2"].ToString());
        //            totalPages = totalRows % this.ucPageDemo.PageSize == 0 ? totalRows / this.ucPageDemo.PageSize : (totalRows / this.ucPageDemo.PageSize) + 1;
        //            this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", currentPage, totalPages);
        //            this.ucPageDemo.TotalRows.Text = totalRows.ToString();
        //            this.ucPageDemo.CurrentPage = currentPage;
        //            this.ucPageDemo.TotalPages = totalPages;
        //        }
        //    }
        //    return dtResult;
        //}

        ///// <summary>
        ///// 页数跳转
        ///// </summary>
        ///// <param name="jumpPage">跳转页</param>
        //void ucPageDemo_JumpPageEvent(int jumpPage)
        //{
        //    if (jumpPage <= this.ucPageDemo.TotalPages)
        //    {
        //        if (jumpPage > 0)
        //        {
        //            this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
        //            this.ucPageDemo.JumpPageCtrl.Text = jumpPage.ToString();
        //            this.GetUACS_ORDER_OPER(false, jumpPage);
        //        }
        //        else
        //        {
        //            jumpPage = 1;
        //            this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
        //            this.ucPageDemo.JumpPageCtrl.Text = jumpPage.ToString();
        //            this.GetUACS_ORDER_OPER(false, jumpPage);
        //        }
        //    }
        //    else
        //    {
        //        this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
        //        MessageBox.Show(@"超出当前最大页数", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        ///// <summary>
        ///// 改变每页展示数据长度
        ///// </summary>
        //void ucPageDemo_ChangedPageSizeEvent()
        //{
        //    this.GetUACS_ORDER_OPER(false, 1);
        //}
        ///// <summary>
        ///// 页数改变按钮(最前页,最后页,上一页,下一页)
        ///// </summary>
        ///// <param name="current"></param>
        //void ucPageDemo_ClickPageButtonEvent(int current)
        //{
        //    if (totalPages != 0 && current > totalPages)
        //    {
        //        current = 1;
        //    }
        //    this.GetUACS_ORDER_OPER(false, current);
        //}
        #endregion

        #region 日期查询        
        /// <summary>
        /// 当天
        /// </summary>
        private void GetToDayTime()
        {
            this.dateTimePicker1.Value = DateTime.Now;
            this.dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToString());
            //查询
            GetUACS_ORDER_OPER(false, 1);
            GetUACS_L3_MAT_OUT_INFO(1);
            //折线图
            MatSpline();
        }
        /// <summary>
        /// 本周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_WeekTime_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Parse(DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))).ToString("yyyy-MM-dd 00:00:00"));  	//本周周一  
            this.dateTimePicker2.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            //查询
            GetUACS_ORDER_OPER(false, 1);
            GetUACS_L3_MAT_OUT_INFO(1);
            //折线图
            MatSpline();
        }
        /// <summary>
        /// 月度
        /// </summary>
        private void GetMonthlyTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-MM-01");
            //控件设置值
            this.dateTimePicker1.Value = Convert.ToDateTime(day1);
            //查询
            GetUACS_ORDER_OPER(false, 1);
            GetUACS_L3_MAT_OUT_INFO(1);
            //折线图
            MatSpline();
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
            this.dateTimePicker1.Value = Convert.ToDateTime(day1);
            //查询
            GetUACS_ORDER_OPER(false, 1);
            GetUACS_L3_MAT_OUT_INFO(1);
            //折线图
            MatSpline();
        }
        /// <summary>
        /// 年度
        /// </summary>
        private void GetAnnualTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-01-01");
            //控件设置值
            this.dateTimePicker1.Value = Convert.ToDateTime(day1);
            //查询
            GetUACS_ORDER_OPER(false, 1);
            GetUACS_L3_MAT_OUT_INFO(1);
            //折线图
            MatSpline();
        }
        private void bt_TodayTime_Click(object sender, EventArgs e)
        {
            GetToDayTime();
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
        #endregion

        #region 分页 - 送料计划

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
                this.ucPage1.PageInfo.Text = string.Format("第{0}/{1}页", "1", "1");
                this.ucPage1.TotalRows.Text = @"0";
                this.ucPage1.CurrentPage = 1;
                this.ucPage1.TotalPages = 1;
            }
            else
            {
                totalRows = Convert.ToInt32(dtResult.Rows[0]["TotalRows"].ToString());
                totalPages = totalRows % this.ucPage1.PageSize == 0 ? totalRows / this.ucPage1.PageSize : (totalRows / this.ucPage1.PageSize) + 1;
                this.ucPage1.PageInfo.Text = string.Format("第{0}/{1}页", currentPage, totalPages);
                this.ucPage1.TotalRows.Text = totalRows.ToString();
                this.ucPage1.CurrentPage = currentPage;
                this.ucPage1.TotalPages = totalPages;
            }
            return dtResult;
        }

        /// <summary>
        /// 页数跳转
        /// </summary>
        /// <param name="jumpPage">跳转页</param>
        void ucPage1_JumpPageEvent(int jumpPage)
        {
            if (jumpPage <= this.ucPage1.TotalPages)
            {
                if (jumpPage > 0)
                {
                    this.ucPage1.JumpPageCtrl.Text = string.Empty;
                    this.ucPage1.JumpPageCtrl.Text = jumpPage.ToString();
                    //L3送料计划
                    this.GetUACS_L3_MAT_OUT_INFO(jumpPage);
                }
                else
                {
                    jumpPage = 1;
                    this.ucPage1.JumpPageCtrl.Text = string.Empty;
                    this.ucPage1.JumpPageCtrl.Text = jumpPage.ToString();
                    //L3送料计划
                    this.GetUACS_L3_MAT_OUT_INFO(jumpPage);
                }
            }
            else
            {
                this.ucPage1.JumpPageCtrl.Text = string.Empty;
                MessageBox.Show(@"超出当前最大页数", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 改变每页展示数据长度
        /// </summary>
        void ucPage1_ChangedPageSizeEvent()
        {
            //L3送料计划
            this.GetUACS_L3_MAT_OUT_INFO(1);
        }
        /// <summary>
        /// 页数改变按钮(最前页,最后页,上一页,下一页)
        /// </summary>
        /// <param name="current"></param>
        void ucPage1_ClickPageButtonEvent(int current)
        {
            if (totalPages != 0 && current > totalPages)
            {
                current = 1;
            }
            //L3送料计划
            this.GetUACS_L3_MAT_OUT_INFO(current);
        }
        #endregion

    }

    public class OrderDate
    {
        /// <summary>
        /// 行车
        /// </summary>
        public string CRANE_NO { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DaY { get; set; }
        /// <summary>
        /// 平均装车时间
        /// </summary>
        public double AverageTime { get; set; }
    }
}
