using System;
using System.Data;
using System.Windows.Forms;
using UACSDAL;

namespace UACSView
{
    /// <summary>
    /// 画面操作历史
    /// </summary>
    public partial class FrmLogForm : Baosight.iSuperframe.Forms.FormBase
    {
        // private static Baosight.iSuperframe.Common.IDBHelper dBHelper = null;
        public FrmLogForm()
        {
            InitializeComponent();
            //dBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");
        }

        /// <summary>
        /// 数据加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MLogForm_Load(object sender, EventArgs e)
        {
            this.dateTimeStart.Value = DateTime.Now.Date;
            dateTimeEnd.Text = DateTime.Now.Date.AddDays(1).ToString();
            this.ucPage1.CurrentPage = 1;
            this.ucPage1.PageSize = Convert.ToInt32(this.ucPage1.CboPageSize.Text);
            this.ucPage1.TotalPages = 1;
            this.ucPage1.ClickPageButtonEvent += ucPage1_ClickPageButtonEvent;
            this.ucPage1.ChangedPageSizeEvent += ucPage1_ChangedPageSizeEvent;
            this.ucPage1.JumpPageEvent += ucPage1_JumpPageEvent;
            GetoLogsData(1, dateTimeStart.Value, dateTimeEnd.Value, "", "");
        }

        /// <summary>
        /// 点击查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //GetFormLogData();
            GetoLogsData(1, dateTimeStart.Value, dateTimeEnd.Value, txtKey1.Text.Trim(), txtInfo.Text.Trim());
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="currentPage">当前页面</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key1">单独记录的关键信息1</param>
        /// <param name="info">日志信息</param>
        private void GetoLogsData(int currentPage, DateTime start, DateTime end, string key1, string info)
        {
            string strStart = start.ToString("yyyyMMddHHmmss");
            string strEnd = end.ToString("yyyyMMddHHmmss");
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER () AS ROWNUM,tab.* FROM ( ";
                sql += "SELECT ROW_NUMBER() OVER() as ROW_INDEX , SEQNO, KEY1, KEY2, \"LEVEL\", INFO, MODULE, USERID, TOC FROM UACS_HMI_LOG   WHERE 1 = 1  ";
                sql += " AND TOC  > '" + strStart + "' and TOC <'" + strEnd + "'";
                if (key1 != "" && key1 != "全部")
                {
                    sql += " AND KEY1 LIKE '%" + key1 + "%' ";
                }
                if (info != "" && info != "全部")
                {
                    sql += " AND INFO LIKE  '%" + info + "%' ";
                }
                if (cmbLevelId.Text.Contains("出错信息"))
                {
                    sql += " AND LEVEL = '" + 3 + "' ";
                }
                else if (cmbLevelId.Text.Contains("普通警告"))
                {
                    sql += " AND LEVEL = '" + 2 + "' ";
                }
                else if (cmbLevelId.Text.Contains("普通信息"))
                {
                    sql += " AND LEVEL = '" + 1 + "' ";
                }
                sql += " ORDER BY TOC DESC ";
                sql += " ) tab ) WHERE ROWNUM BETWEEN ((" + currentPage + " - 1) * " + this.ucPage1.PageSize + ") + 1 AND " + currentPage + " *  " + this.ucPage1.PageSize;
                dt.Clear();
                dt = new DataTable();

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(rdr);
                }
                //cbxKey1.DataSource = dt;
                cbxKey1.DataSource = GetDataPage(dt, currentPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

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
                    this.GetoLogsData(jumpPage, dateTimeStart.Value, dateTimeEnd.Value, txtKey1.Text.Trim(), txtInfo.Text.Trim());
                }
                else
                {
                    jumpPage = 1;
                    this.ucPage1.JumpPageCtrl.Text = string.Empty;
                    this.ucPage1.JumpPageCtrl.Text = jumpPage.ToString();
                    this.GetoLogsData(jumpPage, dateTimeStart.Value, dateTimeEnd.Value, txtKey1.Text.Trim(), txtInfo.Text.Trim());
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
            this.GetoLogsData(1, dateTimeStart.Value, dateTimeEnd.Value, txtKey1.Text.Trim(), txtInfo.Text.Trim());
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
            this.GetoLogsData(current, dateTimeStart.Value, dateTimeEnd.Value, txtKey1.Text.Trim(), txtInfo.Text.Trim());
        }
        #endregion
    }
}
