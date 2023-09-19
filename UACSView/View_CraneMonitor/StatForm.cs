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
            //初始化查询
            GetUACS_ORDER_OPER(true, 1);
           
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
            //var sqlText = "SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER () AS ROWNUM,tab.* FROM ( ";
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
            //if (!isLoad)
            //{
            //    if (craneNo != "全部")
            //    {
            //        sqlText = string.Format("{0} and CRANE_NO = '{1}' ", sqlText, craneNo);     //行车号
            //    }
            //    if (craneMode != "全部")
            //    {
            //        sqlText = string.Format("{0} and CRANE_MODE = '{1}' ", sqlText, craneMode); //操作模式
            //    }
            //    //按 NO>流水号>记录时间>更新时间 降序
            //    sqlText += " ORDER BY OPER_ID DESC,ORDER_NO DESC,REC_TIME DESC ";
            //}
          
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
            //dataGridView2.DataSource = dts2;
            //this.dataGridView2.DataSource = GetDataPage(dts2, currentPage, 2);
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
            chart1.BackColor = Color.Transparent;
            //图表区背景
            chart1.ChartAreas[0].BackColor = Color.Transparent;
            chart1.ChartAreas[0].BorderColor = Color.Transparent;
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

            chart1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;

            //背景渐变
            chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;

            //chart1.Legends.Clear();
            //图例样式
            Legend legend2 = new Legend("#VALX");
            legend2.Title = "图例";
            legend2.TitleBackColor = Color.Transparent;
            legend2.BackColor = Color.Transparent;
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
                        var res = ChinaRound((double)Math.Round(PassCount / allCount * 100, 1), 0).ToString() + "%";
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

        #region 废弃 注释
        //        /// <summary>
        //        /// 查询数据库
        //        /// </summary>
        //        /// <param name="sender"></param>
        //        /// <param name="e"></param>
        //        private void button1_Click(object sender, EventArgs e)
        //        {
        //            if (m_dbHelper == null)
        //                return;

        //           // HMILogger.WriteLog("k1","k2","message");


        //            //选择班组
        //            string strshift = cmbShift.Text.Trim();

        //            //选择哪一跨的
        //            string strWare = cmbWare.Text.Trim();

        //            DateTime dt = dateTimePicker1.Value; ;
        //            //string timespan1 = ViewHelper.GenTimeSpanSQL(strshift.Trim(), dt, "TIME");
        //            DateTime dt2 = dateTimePicker2.Value;
        //            string timespan = ViewHelper.GenTimeSpanSQL(strshift.Trim(), dt, dt2, "TIME");
        //            string timespan2 = ViewHelper.GenTimeSpanSQL(dt, dt2, "REC_TIME");

        //            //查询作业总数
        //            //string sql0 = @"select crane_no,cast(count(*) / 2 as float) as num  from  UACS_CRANE_ORDER_OPER ";
        //            string sql0 = @"select crane_no,count(*) as num from UACS_L3_IN_STOCK ";
        //            if (strshift == "全部")
        //            {
        //                sql0 += "  where " + timespan ;
        //            }
        //            else
        //            {
        //                sql0 += "  where " + timespan;
        //            }
        //            //查询出3列
        ////            string sql1 = @"select crane_no,CASE
        ////                            WHEN crane_mode = 1 THEN '遥控'
        ////                            WHEN crane_mode = 2 THEN '手动'
        ////                            WHEN crane_mode = 4 THEN '自动'
        ////                            else '手动'
        ////                            END as mode ,cast(count(*) / 2 as float) as num  from  UACS_CRANE_ORDER_OPER ";
        //            string sql1 = @"select crane_no,mode ,count(*) as num FROM (select crane_no,CASE
        //            WHEN FROM_MODE = 4 AND TO_MODE = 4 THEN '自动'
        //            WHEN FROM_MODE = 1 AND TO_MODE = 1 THEN '遥控'
        //            WHEN FROM_MODE = 1 AND TO_MODE = 4 THEN '遥控'
        //            WHEN FROM_MODE = 4 AND TO_MODE = 1 THEN '遥控'
        //            WHEN FROM_MODE = 2 OR TO_MODE = 2 THEN '手动'
        //            ELSE '其他'
        //            END as mode from UACS_L3_IN_STOCK";
        //            if (strshift == "全部")
        //            {
        //                sql1 += "  where " + timespan;
        //            }
        //            else
        //            {
        //                sql1 += "  where " + timespan;
        //            }

        //            //增加作业类型，共4列
        //            string sql2 = @"select ordertype,crane_no,mode,count(*) as num from (select CASE
        //            WHEN to_order_type = '01' THEN '机组入口→台车'
        //            WHEN to_order_type = '02' THEN '机组出口→机组入口'
        //            WHEN to_order_type = '03' THEN '机组出口→台车'       
        //            WHEN to_order_type = '04' THEN '机组出口→拆包/离线'
        //            WHEN to_order_type = '05' THEN '台车→机组入口'
        //            WHEN to_order_type = '06' THEN '台车→机组出口'
        //            WHEN to_order_type = '07' THEN '台车→拆包/离线'
        //            WHEN to_order_type = '08' THEN '机组出口→机组出口'
        //            WHEN to_order_type = '11' THEN '机组产出入库'
        //            WHEN to_order_type = '12' THEN '台车入库'
        //            WHEN to_order_type = '13' THEN '卡车入库'
        //            WHEN to_order_type = '14' THEN '机组退料入库'
        //            WHEN to_order_type = '15' THEN '修复区→库区'
        //            WHEN to_order_type = '16' THEN '运输链入库'
        //            WHEN to_order_type = '21' THEN '机组上料'
        //            WHEN to_order_type = '22' THEN '卡车出库'
        //            WHEN to_order_type = '24' THEN '台车出库'
        //            WHEN to_order_type = '26' THEN '库区→修复区'
        //            WHEN to_order_type = '28' THEN '短驳出库'
        //            WHEN to_order_type = '31' THEN '倒垛'
        //            WHEN to_order_type = '34' THEN '拒收入库'
        //            WHEN to_order_type = '41' THEN '运输链出库'
        //            WHEN to_order_type = '0B' THEN '机组入口→拆包/离线'
        //            ELSE '其他'
        //            END as ordertype, crane_no,FROM_MODE,TO_MODE,CASE
        //            WHEN FROM_MODE = 4 AND TO_MODE = 4 THEN '自动'
        //            WHEN FROM_MODE = 1 AND TO_MODE = 1 THEN '遥控'
        //            WHEN FROM_MODE = 1 AND TO_MODE = 4 THEN '遥控'
        //            WHEN FROM_MODE = 4 AND TO_MODE = 1 THEN '遥控'
        //            WHEN FROM_MODE = 2 OR TO_MODE = 2 THEN '手动'
        //            ELSE '其他'
        //            END as mode from  UACS_L3_IN_STOCK ";
        //            if (strshift == "全部")
        //            {
        //                sql2 += "  where " + timespan;
        //            }
        //            else
        //            {
        //                sql2 += "  where " + timespan;
        //            }
        ////            string sql2 = @"select(CASE ORDER_TYPE 
        ////                            WHEN '16' THEN '运输链入库'
        ////                            WHEN '13' THEN '卡车入库'
        ////                            WHEN '11' THEN '机组产出入库'
        ////                            WHEN '34' THEN '拒收入库'
        ////                            WHEN '15' THEN '包装入库'
        ////                            WHEN '14' THEN '机组退料入库'
        ////                            WHEN '41' THEN '运输链出库'
        ////                            WHEN '22' THEN '卡车出库'
        ////                            WHEN '28' THEN '短驳出库'
        ////                            WHEN '21' THEN '机组上料'
        ////                            WHEN '31' THEN '倒垛'
        ////                            WHEN '24' THEN '台车出库'
        ////                            WHEN '12' THEN '台车入库'
        ////                            WHEN '01' THEN '机组入口→台车'
        ////                            WHEN '02' THEN '机组出口→机组入口'
        ////                            WHEN '03' THEN '机组出口→台车'
        ////                            WHEN '04' THEN '机组出口→拆包/离线'
        ////                            WHEN '05' THEN '台车→机组入口'
        ////                            WHEN '06' THEN '台车→机组出口'
        ////                            WHEN '07' THEN '台车→拆包/离线'
        ////                            WHEN '08' THEN '机组出口→机组出口'
        ////                            WHEN '0B' THEN '机组入口→拆包/离线'
        ////                            WHEN '26' THEN '拆包/离线→台车 库区→拆包/离线'
        ////                            else '其它'
        ////                            end ) ORDER_TYPE, crane_no,(CASE
        ////                            WHEN crane_mode = 1 THEN '遥控'
        ////                            WHEN crane_mode = 2 THEN '手动'
        ////                            WHEN crane_mode = 4 THEN '自动'
        ////                            else '手动'
        ////                            END) as mode ,cast(count(*) / 2 as float) as num  from  UACS_CRANE_ORDER_OPER where " + timespan1;

        ////            string sql3 = @"SELECT 
        ////                            (CASE                          
        ////                            WHEN STOCK_NO like 'D112%' THEN '112出口'
        ////                            WHEN STOCK_NO like 'D108%' THEN '108出口'
        ////                            WHEN STOCK_NO like 'D208%' THEN '208出口'
        ////                            WHEN STOCK_NO like 'D171WR%' THEN '171入口'
        ////                            WHEN STOCK_NO like 'D171WC%' THEN '171出口'
        ////                            WHEN STOCK_NO like 'D172WR%' THEN '172入口'
        ////                            WHEN STOCK_NO like 'D172WC%' THEN '172出口'
        ////                            WHEN STOCK_NO like 'D173WR%' THEN '173入口'
        ////                            WHEN STOCK_NO like 'D173WC%' THEN '173出口'
        ////                            WHEN STOCK_NO like 'D174WR%' THEN '174入口'
        ////                            WHEN STOCK_NO like 'D174WC%' THEN '174出口'
        ////                            END ) as stock_no ,
        ////                            count(*) as num,
        ////                            (CASE ORDER_TYPE 
        ////                            WHEN '16' THEN '运输链入库'
        ////                            WHEN '13' THEN '卡车入库'
        ////                            WHEN '11' THEN '机组产出入库'
        ////                            WHEN '34' THEN '拒收入库'
        ////                            WHEN '15' THEN '包装入库'
        ////                            WHEN '14' THEN '机组退料入库'
        ////                            WHEN '41' THEN '运输链出库'
        ////                            WHEN '22' THEN '卡车出库'
        ////                            WHEN '28' THEN '短驳出库'
        ////                            WHEN '21' THEN '机组上料'
        ////                            WHEN '31' THEN '倒垛'
        ////                            WHEN '24' THEN '台车出库'
        ////                            WHEN '12' THEN '台车入库'
        ////                            WHEN '01' THEN '机组入口→台车'
        ////                            WHEN '02' THEN '机组出口→机组入口'
        ////                            WHEN '03' THEN '机组出口→台车'
        ////                            WHEN '04' THEN '机组出口→拆包/离线'
        ////                            WHEN '05' THEN '台车→机组入口'
        ////                            WHEN '06' THEN '台车→机组出口'
        ////                            WHEN '07' THEN '台车→拆包/离线'
        ////                            WHEN '08' THEN '机组出口→机组出口'
        ////                            WHEN '0B' THEN '机组入口→拆包/离线'
        ////                            WHEN '26' THEN '拆包/离线→台车 库区→拆包/离线'
        ////                            else '其它'
        ////                            end ) ORDER_TYPE,
        ////                            crane_no,
        ////                            (CASE
        ////                            WHEN crane_mode = 1 THEN '遥控'
        ////                            WHEN crane_mode = 2 THEN '手动'
        ////                            WHEN crane_mode = 4 THEN '自动'
        ////                            else '手动'
        ////                            END ) as mode
        ////                            FROM UACS_CRANE_ORDER_OPER
        ////                            WHERE " + timespan1;
        //            string sql3 = @"select crane_no,stock,count(*) as num,ordertype,mode FROM (select crane_no ,CASE
        //            WHEN FORM_STOCK LIKE '%YSL%' OR TO_STOCK LIKE '%YSL%' THEN '运输链'
        //            WHEN FORM_STOCK LIKE '%D102%' OR TO_STOCK LIKE '%D102%' THEN 'D102'          
        //            ELSE '过跨台车'
        //            END as stock,CASE
        //            WHEN to_order_type = '01' THEN '机组入口→台车'
        //            WHEN to_order_type = '02' THEN '机组出口→机组入口'
        //            WHEN to_order_type = '03' THEN '机组出口→台车'       
        //            WHEN to_order_type = '04' THEN '机组出口→拆包/离线'
        //            WHEN to_order_type = '05' THEN '台车→机组入口'
        //            WHEN to_order_type = '06' THEN '台车→机组出口'
        //            WHEN to_order_type = '07' THEN '台车→拆包/离线'
        //            WHEN to_order_type = '08' THEN '机组出口→机组出口'
        //            WHEN to_order_type = '11' THEN '机组产出入库'
        //            WHEN to_order_type = '12' THEN '台车入库'
        //            WHEN to_order_type = '13' THEN '卡车入库'
        //            WHEN to_order_type = '14' THEN '机组退料入库'
        //            WHEN to_order_type = '15' THEN '修复区→库区'
        //            WHEN to_order_type = '16' THEN '运输链入库'
        //            WHEN to_order_type = '21' THEN '机组上料'
        //            WHEN to_order_type = '22' THEN '卡车出库'
        //            WHEN to_order_type = '24' THEN '台车出库'
        //            WHEN to_order_type = '26' THEN '库区→修复区'
        //            WHEN to_order_type = '28' THEN '短驳出库'
        //            WHEN to_order_type = '31' THEN '倒垛'
        //            WHEN to_order_type = '34' THEN '拒收入库'
        //            WHEN to_order_type = '41' THEN '运输链出库'
        //            WHEN to_order_type = '0B' THEN '机组入口→拆包/离线'
        //            ELSE '其他'
        //            END as ordertype, FROM_MODE,TO_MODE,CASE
        //            WHEN FROM_MODE = 4 AND TO_MODE = 4 THEN '自动'
        //            WHEN FROM_MODE = 1 AND TO_MODE = 1 THEN '遥控'
        //            WHEN FROM_MODE = 1 AND TO_MODE = 4 THEN '遥控'
        //            WHEN FROM_MODE = 4 AND TO_MODE = 1 THEN '遥控'
        //            WHEN FROM_MODE = 2 OR TO_MODE = 2 THEN '手动'
        //            ELSE '其他'
        //            END as mode from  UACS_L3_IN_STOCK ";
        //            if (strshift == "全部")
        //            {
        //                sql3 += "  where " + timespan;
        //            }
        //            else
        //            {
        //                sql3 += "  where " + timespan;
        //            }
        //            sql3 += " and (FORM_STOCK LIKE '%D%' or TO_STOCK LIKE '%D%' or FORM_STOCK LIKE '%PA%' or TO_STOCK LIKE '%PA%' or FORM_STOCK LIKE '%MC%' or TO_STOCK LIKE '%MC%') ";
        //            sql3 += " and (to_order_type = '11' or to_order_type = '21') ";
        //            //sql3 += " and (STOCK_NO like 'D108%' or STOCK_NO like 'D112%'  or STOCK_NO like 'D208%' or STOCK_NO like 'D171WR%' or STOCK_NO like 'D171WC%' or STOCK_NO like 'D172WR%' or STOCK_NO like 'D172WC%' or STOCK_NO like 'D173WR%' or STOCK_NO like 'D173WC%' or STOCK_NO like 'D174WR%' or STOCK_NO like 'D174WC%') ";

        //            //sql3 += " and ( crane_no='4_1' or crane_no='4_2' or crane_no='4_3') ";
        //            //sql3 += " and ( crane_no='3_1' or crane_no='3_2' or crane_no='3_3' or crane_no='3_4' or crane_no='3_5') ";

        //            if (strWare == "原料库")
        //            {
        //                sql0 += " and  crane_no in ('1_1','1_2','1_3')";
        //                sql1 += " and  crane_no in ('1_1','1_2','1_3')) AS A";
        //                sql2 += " and  crane_no in ('1_1','1_2','1_3')) AS A";
        //                sql3 += " and  crane_no in ('1_1','1_2','1_3')) AS A";

        //            }
        //            if (strWare == "全部")
        //            {
        //                sql0 += " and crane_no in ('1_1','1_2','1_3')";
        //                sql1 += " and crane_no in ('1_1','1_2','1_3')) AS A";
        //                sql2 += " and crane_no in ('1_1','1_2','1_3')) AS A";
        //                sql3 += " and crane_no in ('1_1','1_2','1_3')) AS A";
        //            }

        //            sql0 += " group by crane_no";
        //            sql1 += " group by crane_no,mode order by crane_no";
        //            sql2 += " group by crane_no,mode,ordertype order by crane_no";
        //            sql3 += " group by crane_no,mode,ordertype,stock order by crane_no";

        //            //查询吊运总数
        //            DataTable data0 = null;
        //            string error = "";
        //            // DBHelper.ReadTable
        //            data0 = m_dbHelper.ReadData(sql0, out error);

        //            Dictionary<string, double> myDictionary = new Dictionary<string, double>();
        //            //计算百分比
        //            if (data0 != null)
        //            {
        //                foreach (DataRow dr in data0.Rows)
        //                {
        //                   double tmp = Convert.ToDouble(dr[1]);
        //                   myDictionary.Add((string)dr[0], tmp);
        //                }
        //            }
        //            DataTable data1 = null;
        //            data1 = m_dbHelper.ReadData(sql1, out error);



        //            //计算百分比
        //            if (data1 != null)
        //            {
        //                data1.Columns.Add("PERCENT", Type.GetType("System.Single"));
        //                dataGridView1.Rows.Clear();   

        //                foreach(DataRow dr in data1.Rows)
        //                {
        //                    string str = (string)dr[0];
        //                    double count = myDictionary[str];
        //                    if (count!=0)
        //                    {
        //                        double temp = (Convert.ToDouble(dr[2]) * 100 / count);
        //                        dr[3] = float.Parse(temp.ToString("#0.00"));
        //                    }
        //                     else
        //                        dr[3] = 0.0;
        //                    // myDictionary[str];
        //                }
        //            }

        //            ViewHelper.SetDataGridViewData(dataGridView1, data1, true);

        //            DataTable data2 = null;

        //            data2 = m_dbHelper.ReadData(sql2, out error);

        //            dataGridView2.Rows.Clear();
        //            ViewHelper.SetDataGridViewData(dataGridView2, data2, true);

        //            //GetDatagridview(timespan);
        //            //VisibleColumn(dataGridView3);

        //            DataTable data3 = null;
        //            data3 = m_dbHelper.ReadData(sql3, out error);
        //            dataGridView3.Rows.Clear();
        //            ViewHelper.SetDataGridViewData(dataGridView3, data3, true);

        //            dataGridViewColor(); 
        //        } 
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


            //for (int i = 0; i < dataGridView3.Rows.Count; i++)
            //{
            //    if (dataGridView3.Rows[i].Cells["CraneMode3"].Value != System.DBNull.Value)
            //    {
            //        if (dataGridView3.Rows[i].Cells["CraneMode3"].Value.ToString().Trim() == "自动")
            //        {
            //            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
            //        }
            //    }
            //}
        }



        //private void GetDatagridview(string time)
        //{
        //    dataGridView3.Rows.Clear();
        //    dataGridView3.Rows.Add(12);
        //    dataGridView3.Rows[0].Cells[0].Value = "D171";
        //    dataGridView3.Rows[0].Cells[1].Value = GetNUM(time, 21, "D171WR", 4);  
        //    dataGridView3.Rows[0].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[0].Cells[3].Value = "自动";

        //    dataGridView3.Rows[1].Cells[0].Value = "D171";
        //    dataGridView3.Rows[1].Cells[1].Value = GetNUM(time, 21, "D171WR", 2);  
        //    dataGridView3.Rows[1].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[1].Cells[3].Value = "手动";

        //    dataGridView3.Rows[2].Cells[0].Value = "D171";
        //    dataGridView3.Rows[2].Cells[1].Value = GetNUM(time, 14, "D171WR", 4);  
        //    dataGridView3.Rows[2].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[2].Cells[3].Value = "自动";

        //    dataGridView3.Rows[3].Cells[0].Value = "D171";
        //    dataGridView3.Rows[3].Cells[1].Value = GetNUM(time, 14, "D171WR", 2);  
        //    dataGridView3.Rows[3].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[3].Cells[3].Value = "手动";

        //    dataGridView3.Rows[4].Cells[0].Value = "D171";
        //    dataGridView3.Rows[4].Cells[1].Value = GetNUM(time, 21, "D171WR", 1);  
        //    dataGridView3.Rows[4].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[4].Cells[3].Value = "遥控";

        //    dataGridView3.Rows[5].Cells[0].Value = "D171";
        //    dataGridView3.Rows[5].Cells[1].Value = GetNUM(time, 14, "D171WR", 1);  
        //    dataGridView3.Rows[5].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[5].Cells[3].Value = "遥控";

        //    dataGridView3.Rows[6].Cells[0].Value = "D172";
        //    dataGridView3.Rows[6].Cells[1].Value = GetNUM(time, 24, "172WR", 4);  
        //    dataGridView3.Rows[6].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[6].Cells[3].Value = "自动";

        //    dataGridView3.Rows[7].Cells[0].Value = "D172";
        //    dataGridView3.Rows[7].Cells[1].Value = GetNUM(time, 24, "172WR", 2);  
        //    dataGridView3.Rows[7].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[7].Cells[3].Value = "手动";

        //    dataGridView3.Rows[8].Cells[0].Value = "D172";
        //    dataGridView3.Rows[8].Cells[1].Value = GetNUM(time, 11, "172WR", 4);  
        //    dataGridView3.Rows[8].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[8].Cells[3].Value = "自动";

        //    dataGridView3.Rows[9].Cells[0].Value = "D172";
        //    dataGridView3.Rows[9].Cells[1].Value = GetNUM(time, 11, "172WR", 2); 
        //    dataGridView3.Rows[9].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[9].Cells[3].Value = "手动";


        //    dataGridView3.Rows[10].Cells[0].Value = "D172";
        //    dataGridView3.Rows[10].Cells[1].Value = GetNUM(time, 24, "172WR", 1); 
        //    dataGridView3.Rows[10].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[10].Cells[3].Value = "遥控";

        //    dataGridView3.Rows[11].Cells[0].Value = "D172";
        //    dataGridView3.Rows[11].Cells[1].Value = GetNUM(time, 11, "172WR", 1);
        //    dataGridView3.Rows[11].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[11].Cells[3].Value = "遥控";

        //    dataGridView3.Rows[6].Cells[0].Value = "D173";
        //    dataGridView3.Rows[6].Cells[1].Value = GetNUM(time, 24, "D173WR", 4);
        //    dataGridView3.Rows[6].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[6].Cells[3].Value = "自动";

        //    dataGridView3.Rows[7].Cells[0].Value = "D173";
        //    dataGridView3.Rows[7].Cells[1].Value = GetNUM(time, 24, "D173WR", 2);
        //    dataGridView3.Rows[7].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[7].Cells[3].Value = "手动";

        //    dataGridView3.Rows[8].Cells[0].Value = "D173";
        //    dataGridView3.Rows[8].Cells[1].Value = GetNUM(time, 11, "D173WR", 4); 
        //    dataGridView3.Rows[8].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[8].Cells[3].Value = "自动";

        //    dataGridView3.Rows[9].Cells[0].Value = "D173";
        //    dataGridView3.Rows[9].Cells[1].Value = GetNUM(time, 11, "D173WR", 2); 
        //    dataGridView3.Rows[9].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[9].Cells[3].Value = "手动";


        //    dataGridView3.Rows[10].Cells[0].Value = "D173";
        //    dataGridView3.Rows[10].Cells[1].Value = GetNUM(time, 24, "D173WR", 1);
        //    dataGridView3.Rows[10].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[10].Cells[3].Value = "遥控";

        //    dataGridView3.Rows[11].Cells[0].Value = "D173";
        //    dataGridView3.Rows[11].Cells[1].Value = GetNUM(time, 11, "D173WR", 1);
        //    dataGridView3.Rows[11].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[11].Cells[3].Value = "遥控";

        //    dataGridView3.Rows[6].Cells[0].Value = "D174";
        //    dataGridView3.Rows[6].Cells[1].Value = GetNUM(time, 24, "D174WR", 4);
        //    dataGridView3.Rows[6].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[6].Cells[3].Value = "自动";

        //    dataGridView3.Rows[7].Cells[0].Value = "D174";
        //    dataGridView3.Rows[7].Cells[1].Value = GetNUM(time, 24, "D174WR", 2);
        //    dataGridView3.Rows[7].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[7].Cells[3].Value = "手动";

        //    dataGridView3.Rows[8].Cells[0].Value = "D174";
        //    dataGridView3.Rows[8].Cells[1].Value = GetNUM(time, 11, "D174WR", 4);
        //    dataGridView3.Rows[8].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[8].Cells[3].Value = "自动";

        //    dataGridView3.Rows[9].Cells[0].Value = "D174";
        //    dataGridView3.Rows[9].Cells[1].Value = GetNUM(time, 11, "D174WR", 2);
        //    dataGridView3.Rows[9].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[9].Cells[3].Value = "手动";


        //    dataGridView3.Rows[10].Cells[0].Value = "D174";
        //    dataGridView3.Rows[10].Cells[1].Value = GetNUM(time, 24, "D174WR", 1);
        //    dataGridView3.Rows[10].Cells[2].Value = "出库(入口上料)";
        //    dataGridView3.Rows[10].Cells[3].Value = "遥控";

        //    dataGridView3.Rows[11].Cells[0].Value = "D174";
        //    dataGridView3.Rows[11].Cells[1].Value = GetNUM(time, 11, "D174WR", 1);
        //    dataGridView3.Rows[11].Cells[2].Value = "入库(入口退料)";
        //    dataGridView3.Rows[11].Cells[3].Value = "遥控";

        //}

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

                    Export2Excel(dataGridView1, saveFileDialog1.FileName);

                }));
                ParkClassLibrary.HMILogger.WriteLog("行车作业结果分析", "行车统计导出", ParkClassLibrary.LogLevel.Info, this.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
    }
}
