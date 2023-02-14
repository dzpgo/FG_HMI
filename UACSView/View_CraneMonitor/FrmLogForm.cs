using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Baosight.ColdRolling.Utility;
//using Baosight.ColdRolling.LogicLayer;
//using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.Common;
using UACSDAL;
using Baosight.iSuperframe.Authorization.Interface;
using Baosight.iSuperframe.Authorization;
using System.Xml;

namespace UACSView
{
    /// <summary>
    /// 画面操作历史
    /// </summary>
    public partial class FrmLogForm : Baosight.iSuperframe.Forms.FormBase
    {
       // private static Baosight.iSuperframe.Common.IDBHelper dBHelper = null;
        bool isFalse = false;

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
            //dataGridView1.ColumnHeadersHeight = 35;
            //dataGridView1.RowTemplate.Height = 35;
            //dataGridView1.AllowUserToResizeRows = false;          //禁止用户改变DataGridView1所有行的行高
            //dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   //居中
            //dataGridView1.RowsDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Regular);
            //GetFormLogData();
            //UACS.ViewHelper.DataGridViewInit(cbxKey1);
            this.dateTimeStart.Value = DateTime.Now.Date;
            dateTimeEnd.Text = DateTime.Now.Date.AddDays(1).ToString();
            GetoLogsData(true,dateTimeStart.Value, dateTimeEnd.Value, "", "");
        }
        private void GetFormLogData()
        {
            //try
            //{
            //    string sql = "select * from LV_LOG_LOGINFO order by TOC desc";
            //    ViewHelper.GenDataGridViewData(dBHelper, dataGridView1, sql, isFalse, "KEY1", cbxKey1);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        /// <summary>
        /// 点击查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //GetFormLogData();
            GetoLogsData(false,dateTimeStart.Value, dateTimeEnd.Value, txtKey1.Text.Trim(), txtInfo.Text.Trim());                 
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="isLoad">是否是初始化 true=是初始化，false=不是初始化</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key1">单独记录的关键信息1</param>
        /// <param name="info">日志信息</param>
        private void GetoLogsData(bool isLoad, DateTime start, DateTime end, string key1, string info)
        {            
            string strStart = start.ToString("yyyyMMddHHmmss");
            string strEnd = end.ToString("yyyyMMddHHmmss");
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT ROW_NUMBER() OVER() as ROW_INDEX , SEQNO, KEY1, KEY2, \"LEVEL\", INFO, MODULE, USERID, TOC FROM UACS_HMI_LOG   WHERE 1 = 1  ";
                sql += " AND TOC  > '" + strStart + "' and TOC <'" + strEnd + "'";

                if (!isLoad)
                {
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
                }
                else
                {
                    //初次加载时默认查询倒序30条数据（仅初始化时用）
                    sql = "SELECT ROWNUM AS ROW_INDEX,SEQNO, KEY1, KEY2, \"LEVEL\", INFO, MODULE, USERID, TOC FROM (SELECT ROW_NUMBER() OVER(ORDER BY TOC DESC) AS ROWNUM, SEQNO, KEY1, KEY2, \"LEVEL\", INFO, MODULE, USERID, TOC FROM UACS_HMI_LOG WHERE TOC IS NOT NULL) a WHERE ROWNUM > 0 and ROWNUM <=30 ";
                }
                
                dt.Clear();
                dt = new DataTable();

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(rdr);
                }
                cbxKey1.DataSource = dt;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}
