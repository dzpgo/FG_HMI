using System;
using System.Data;
using UACSDAL;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;

namespace UACSView
{
    /// <summary>
    /// L3送料与配料计划
    /// </summary>
    public partial class L3_MAT_OutAndWeight_Info : FormBase
    {
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        bool hasSetColumn = false;
        public L3_MAT_OutAndWeight_Info()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void L3_MAT_OutAndWeight_Info_Load(object sender, EventArgs e)
        {
            this.ucPage1.CurrentPage = 1;
            this.ucPage1.PageSize = Convert.ToInt32(this.ucPage1.CboPageSize.Text);
            this.ucPage1.TotalPages = 1;
            this.ucPage1.ClickPageButtonEvent += ucPage1_ClickPageButtonEvent;
            this.ucPage1.ChangedPageSizeEvent += ucPage1_ChangedPageSizeEvent;
            this.ucPage1.JumpPageEvent += ucPage1_JumpPageEvent;

            //L3送料计划
            getL3MatWeightInfo(1);
            MatPie();
            MatPieData();
            MatColumn();
            MatColumnData();
            MatSpline();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //L3送料计划
            getL3MatWeightInfo(1);
            //MatPie();
            MatPieData();
            //MatColumn();
            MatColumnData();
            MatSpline();
        }

        #region L3送料计划
        /// <summary>
        /// L3送料计划查询数据
        /// </summary>
        /// <param name="currentPage">当前页面</param>
        private void getL3MatWeightInfo(int currentPage)
        {
            string work_seqNo = this.textWORK_SEQ_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER () AS ROWNUM,tab.* FROM ( ";
            sqlText += @"SELECT A.WORK_SEQ_NO,A.TRUCK_NO AS TRUCK_NO2,A.MAT_PROD_CODE AS MAT_PROD_CODE2, C.MAT_CNAME AS MAT_CNAME2,A.MAT_WT AS MAT_WT2,A.REC_TIME AS REC_TIME2,A.UPD_TIME AS UPD_TIME2 FROM UACSAPP.UACS_L3_MAT_WEIGHT_INFO A ";
            sqlText += "LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_PROD_CODE ";
            sqlText += "WHERE A.REC_TIME > '{0}' and A.REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            if (!string.IsNullOrEmpty(work_seqNo))
            {
                sqlText = string.Format("{0} and A.WORK_SEQ_NO LIKE '%{1}%' ", sqlText, work_seqNo);
            }
            //按 NO>>记录时间>更新时间 降序
            sqlText += " ORDER BY A.WORK_SEQ_NO DESC,A.REC_TIME DESC,A.UPD_TIME DESC ";
            sqlText += " ) tab ) WHERE ROWNUM BETWEEN ((" + currentPage + " - 1) * " + this.ucPage1.PageSize + ") + 1 AND " + currentPage + " *  " + this.ucPage1.PageSize;
            DataTable dataTable = new DataTable();
            hasSetColumn = false;
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dataTable.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (!hasSetColumn)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = rdr.GetName(i);
                            dataTable.Columns.Add(dc);
                        }
                        dr[i] = rdr[i];
                    }
                    hasSetColumn = true;
                    dataTable.Rows.Add(dr);
                }
            }
            dataGridView1.DataSource = GetDataPage(dataTable, currentPage);
        }
        #endregion

        #region 统计图形

        // <summary>
        /// 初始化图表
        /// </summary>
        private void InitChart()
        {

        }
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
            chart1.Titles.Add("进料次数分析");
            chart1.Titles[0].ForeColor = Color.Blue;
            chart1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart1.Titles[0].Alignment = ContentAlignment.TopCenter;
            //chart1.Titles[0].Visible = false;
            chart1.Titles.Add("合计： 次");
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
            chart1.ChartAreas[0].AxisX.Title = "数量 (次)";
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;
            chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            chart1.ChartAreas[0].AxisX.ToolTip = "数量 (次)";
            //X轴网络线条
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Blue;
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart1.ChartAreas[0].AxisY.Title = "数量 (次)";
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Blue;
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.ToolTip = "数量 (次)";
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
            chart1.Series[0].ToolTip = "#VALX：#VAL (次)";     //鼠标移动到对应点显示数值
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
            //chart1.Series[0].Points.DataBindXY(CodeNameList, NumberList);
            chart1.Series[0].Points[0].Color = Color.Blue;
            //绑定颜色
            chart1.Series[0].Palette = ChartColorPalette.BrightPastel;

            #endregion
        }
        /// <summary>
        /// 饼状图数据
        /// </summary>
        private void MatPieData()
        {
            #region 访问数据库库
            string[] CodeNameList = new string[] { };
            double[] NumberList = new double[] { };
            List<string> codeNames = CodeNameList.ToList();
            List<double> numbers = NumberList.ToList();
            string work_seqNo = this.textWORK_SEQ_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            var sqlText = @"SELECT C.MAT_CNAME AS CodeName,COUNT(0) AS Number ";
            sqlText += "FROM UACSAPP.UACS_L3_MAT_WEIGHT_INFO A ";
            sqlText += "LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_PROD_CODE ";
            sqlText += "WHERE A.REC_TIME > '{0}' and A.REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            if (!string.IsNullOrEmpty(work_seqNo))
            {
                sqlText = string.Format("{0} and A.WORK_SEQ_NO LIKE '%{1}%' ", sqlText, work_seqNo);
            }
            sqlText += "AND A.MAT_PROD_CODE IN ( SELECT MAT_CODE FROM UACS_L3_MAT_INFO) ";
            //按 NO>>记录时间>更新时间 降序
            sqlText += "GROUP BY C.MAT_CNAME HAVING COUNT(C.MAT_CNAME) > 1 ";
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    if (rdr["CodeName"] != System.DBNull.Value)
                    {
                        codeNames.Add(rdr["CodeName"].ToString());
                    }
                    if (rdr["Number"] != System.DBNull.Value)
                    {
                        numbers.Add(Convert.ToDouble(rdr["Number"]));
                    }
                }
            }
            if (codeNames.Count > 0)
            {
                CodeNameList = codeNames.ToArray();
            }
            double numberTotal = 0;
            if (numbers.Count > 0)
            {
                NumberList = numbers.ToArray();
                foreach (var number in NumberList)
                {
                    numberTotal += number;
                }
            }
            #endregion
            foreach (Series series in this.chart1.Series)
            {
                series.Points.Clear();
            }
            chart1.Titles[1].Text = "合计：" + numberTotal + " 次";
            chart1.Series[0].Points.DataBindXY(CodeNameList, NumberList);
        }

        /// <summary>
        /// 柱状图 物料进料重量分析
        /// </summary>
        private void MatColumn()
        {
            #region 柱状图
            string[] a = new string[] { "南山大队", "福田大队", "罗湖大队", "宝安大队", "指挥处", };
            double[] b = new double[] { 541, 574, 345, 854, 257 };
            //this.chart2.ChartAreas.Clear();
            //标题
            chart2.Titles.Add("进料重量分析");
            chart2.Titles[0].ForeColor = Color.Blue;
            chart2.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart2.Titles[0].Alignment = ContentAlignment.TopCenter;
            chart2.Titles.Add("合计：吨");
            chart2.Titles[1].ForeColor = Color.Blue;
            chart2.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            chart2.Titles[1].Alignment = ContentAlignment.TopRight;

            //控件背景
            chart2.BackColor = SystemColors.Control;
            //图表区背景
            chart2.ChartAreas[0].BackColor = SystemColors.Control;
            chart2.ChartAreas[0].BorderColor = SystemColors.Control;
            //X轴标签间距
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart2.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            chart2.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;

            //X坐标轴颜色
            chart2.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
            chart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Blue;
            chart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //X坐标轴标题
            //chart2.ChartAreas[0].AxisX.Title = "重量(宗)";
            //chart2.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            //chart2.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;
            //chart2.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            //chart2.ChartAreas[0].AxisX.ToolTip = "重量(宗)";
            //X轴网络线条
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart2.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Blue;
            chart2.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart2.ChartAreas[0].AxisY.Title = "重量 (吨)";
            chart2.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart2.ChartAreas[0].AxisY.TitleForeColor = Color.Blue;
            chart2.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart2.ChartAreas[0].AxisY.ToolTip = "重量 (吨)";
            //Y轴网格线条
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart2.ChartAreas[0].AxisY2.LineColor = SystemColors.Control;
            chart2.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            Legend legend = new Legend("legend");
            legend.Title = "legendTitle";

            chart2.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart2.Series[0].Label = "#VAL";                //设置显示X Y的值    
            chart2.Series[0].LabelForeColor = Color.Blue;
            chart2.Series[0].ToolTip = "#VALX：#VAL (吨)";     //鼠标移动到对应点显示数值
            chart2.Series[0].ChartType = SeriesChartType.Column;    //图类型(折线)


            chart2.Series[0].Color = Color.Lime;
            chart2.Series[0].LegendText = legend.Name;
            chart2.Series[0].IsValueShownAsLabel = true;
            chart2.Series[0].LabelForeColor = Color.Blue;
            chart2.Series[0].CustomProperties = "DrawingStyle = Cylinder";
            //chart2.Series[0].CustomProperties = "PieLabelStyle = Outside";
            chart2.Legends.Add(legend);
            chart2.Legends[0].Position.Auto = false;

            //绑定数据
            //chart2.Series[0].Points.DataBindXY(CodeNameList, MatWTList);
            //chart2.Series[0].Points[0].Color = Color.Blue;
            chart2.Series[0].Palette = ChartColorPalette.BrightPastel;

            #endregion
        }

        /// <summary>
        /// 柱状图数据
        /// </summary>
        private void MatColumnData()
        {
            #region 访问数据库库
            string[] CodeNameList = new string[] { };
            double[] MatWTList = new double[] { };
            List<string> codeNames = CodeNameList.ToList();
            List<double> MatWT = MatWTList.ToList();
            string work_seqNo = this.textWORK_SEQ_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            var sqlText = @"SELECT C.MAT_CNAME AS CodeName,SUM(A.MAT_WT) AS MatWT ";
            sqlText += "FROM UACSAPP.UACS_L3_MAT_WEIGHT_INFO A ";
            sqlText += "LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_PROD_CODE ";
            sqlText += "WHERE A.REC_TIME > '{0}' and A.REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            if (!string.IsNullOrEmpty(work_seqNo))
            {
                sqlText = string.Format("{0} and A.WORK_SEQ_NO LIKE '%{1}%' ", sqlText, work_seqNo);
            }
            sqlText += "AND A.MAT_PROD_CODE IN ( SELECT MAT_CODE FROM UACS_L3_MAT_INFO) ";
            //按 NO>>记录时间>更新时间 降序
            sqlText += "GROUP BY C.MAT_CNAME HAVING COUNT(C.MAT_CNAME) > 1 ";
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    if (rdr["CodeName"] != System.DBNull.Value)
                    {
                        codeNames.Add(rdr["CodeName"].ToString());
                    }
                    if (rdr["MatWT"] != System.DBNull.Value)
                    {
                        MatWT.Add(Convert.ToDouble(rdr["MatWT"]) / 1000);
                    }
                }
            }
            if (codeNames.Count > 0)
            {
                CodeNameList = codeNames.ToArray();
            }
            double MatWTTotal = 0;
            if (MatWT.Count > 0)
            {
                MatWTList = MatWT.ToArray();
                foreach (var number in MatWTList)
                {
                    MatWTTotal += number;
                }
            }
            #endregion
            foreach (Series series in this.chart2.Series)
            {
                series.Points.Clear();
            }
            chart2.Titles[1].Text = "合计：" + MatWTTotal + " 吨";
            //chart2.Titles.Add("合计：" + MatWTTotal + " 公斤");
            chart2.Series[0].Points.DataBindXY(CodeNameList, MatWTList);
        }

        /// <summary>
        /// 折线图 物料进料重量分析
        /// </summary>
        private void MatSpline()
        {
            try
            {
                Dictionary<string, Dictionary<DateTime, double>> DataDictionary = new Dictionary<string, Dictionary<DateTime, double>>();
                List<ChartDate> ChartDateList = new List<ChartDate>();
                HashSet<string> ChartCodeNameSet = new HashSet<string>();
                HashSet<DateTime> ChartTimeSet = new HashSet<DateTime>();
                // 访问数据库部分
                string work_seqNo = this.textWORK_SEQ_NO.Text.Trim();
                string recTime1 = dateTimePicker1_recTime.Value.Day.Equals(DateTime.Now.Day) ? this.dateTimePicker1_recTime.Value.AddDays(-7).ToString("yyyyMMdd000000") : this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");
                string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");
                var sqlText = @"SELECT DATE(A.REC_TIME) AS DaY, C.MAT_CNAME AS CodeName,SUM(A.MAT_WT) AS MatWT ";
                sqlText += "FROM UACSAPP.UACS_L3_MAT_WEIGHT_INFO A ";
                sqlText += "LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_PROD_CODE ";
                sqlText += "WHERE A.REC_TIME BETWEEN '{0}' AND '{1}' ";
                sqlText = string.Format(sqlText, recTime1, recTime2);
                if (!string.IsNullOrEmpty(work_seqNo))
                {
                    sqlText = string.Format("{0} AND A.WORK_SEQ_NO LIKE '%{1}%' ", sqlText, work_seqNo);
                }
                sqlText += "AND A.MAT_PROD_CODE IN (SELECT MAT_CODE FROM UACS_L3_MAT_INFO) ";
                sqlText += "GROUP BY DATE(A.REC_TIME), C.MAT_CNAME ";
                sqlText += "ORDER BY DaY, C.MAT_CNAME ";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        ChartDate cd = new ChartDate();
                        if (rdr["DaY"] != DBNull.Value)
                        {
                            cd.DaY = Convert.ToDateTime(rdr["DaY"]);
                        }
                        if (rdr["CodeName"] != DBNull.Value)
                        {
                            cd.CodeName = rdr["CodeName"].ToString();
                            ChartCodeNameSet.Add(cd.CodeName);
                        }
                        if (rdr["MatWT"] != DBNull.Value)
                        {
                            cd.MatWT = Convert.ToDouble(rdr["MatWT"]) / 1000;
                        }

                        ChartDateList.Add(cd);
                        ChartTimeSet.Add(cd.DaY.Date);
                    }
                }

                // 使用 Lambda 表达式进行正序排序
                List<string> ChartCodeNameList = ChartCodeNameSet.ToList();
                ChartCodeNameList.Sort((s1, s2) => string.Compare(s1, s2));

                List<DateTime> ChartTimeList = ChartTimeSet.ToList();
                ChartTimeList.Sort();

                DataDictionary.Clear();
                foreach (string name in ChartCodeNameList)
                {
                    var filteredData = ChartDateList
                        .Where(item => name.Equals(item.CodeName))
                        .ToDictionary(item => item.DaY.Date, item => Math.Round(item.MatWT, 2));

                    DataDictionary.Add(name, filteredData);
                }

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
            chart3.ChartAreas[0].AxisY.Minimum = -200; // 最小值
            //chart3.ChartAreas[0].AxisY.Maximum = 100; // 最大值
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
                    this.getL3MatWeightInfo(jumpPage);
                }
                else
                {
                    jumpPage = 1;
                    this.ucPage1.JumpPageCtrl.Text = string.Empty;
                    this.ucPage1.JumpPageCtrl.Text = jumpPage.ToString();
                    //L3送料计划
                    this.getL3MatWeightInfo(jumpPage);
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
            this.getL3MatWeightInfo(1);
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
            this.getL3MatWeightInfo(current);
        }
        #endregion

        #region 日期查询        
        /// <summary>
        /// 当天
        /// </summary>
        private void GetToDayTime()
        {
            this.dateTimePicker1_recTime.Value = DateTime.Now;
            this.dateTimePicker2_recTime.Value = DateTime.Now;
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
        }
        /// <summary>
        /// 本周
        /// </summary>
        private void GetWeekTime()
        {
            this.dateTimePicker1_recTime.Value = DateTime.Parse(DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))).ToString("yyyy-MM-dd 00:00:00"));  	//本周周一  
            this.dateTimePicker2_recTime.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
        }
        /// <summary>
        /// 月度
        /// </summary>
        private void GetMonthlyTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-MM-01");
            //控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
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
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
        }
        /// <summary>
        /// 年度
        /// </summary>
        private void GetAnnualTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-01-01");
            // 控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            // 查询
            // L3送料计划
            getL3MatWeightInfo(1);
        }
        #endregion

        private void bt_TodayTime_Click(object sender, EventArgs e)
        {
            GetToDayTime();
            // 饼状图()
            MatPieData();
            // 柱状图
            MatColumnData();
            // 折线图
            MatSpline();
        }

        private void bt_MonthlyTime_Click(object sender, EventArgs e)
        {
            GetMonthlyTime();
            // 饼状图()
            MatPieData();
            // 柱状图
            MatColumnData();
            // 折线图
            MatSpline();
        }

        private void bt_QuarterlyTime_Click(object sender, EventArgs e)
        {
            GetQuarterlyTime();
            // 饼状图()
            MatPieData();
            // 柱状图
            MatColumnData();
            // 折线图
            MatSpline();
        }

        private void bt_AnnualTime_Click(object sender, EventArgs e)
        {
            GetAnnualTime();
            // 饼状图()
            MatPieData();
            // 柱状图
            MatColumnData();
            // 折线图
            MatSpline();
        }
        private void bt_WeekTime_Click(object sender, EventArgs e)
        {
            GetWeekTime();
            // 饼状图()
            MatPieData();
            // 柱状图
            MatColumnData();
            // 折线图
            MatSpline();
        }
    }

    public class ChartDate
    {
        /// <summary>
        /// 物料
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DaY { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double MatWT { get; set; }
    }
}
