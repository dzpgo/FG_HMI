using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACS;
using Baosight.iSuperframe.Forms;
using UACSDAL;

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// L3送料计划
    /// </summary>
    public partial class L3_MAT_Weight_Info2 : FormBase
    {
        DataTable dt = new DataTable();
        bool hasSetColumn = false;
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        public L3_MAT_Weight_Info2()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
        }

        #region 事件
        /// <summary>
        /// 数据加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void L3_MAT_Weight_Info2_Load(object sender, EventArgs e)
        {
            try
            {
                //设置背景色
                this.panel1.BackColor = UACSDAL.ColorSln.FormBgColor;
                this.panel2.BackColor = UACSDAL.ColorSln.FormBgColor;
                //绑定下拉框
                BindCombox();
                //
                this.dateTimePicker1_recTime.Value = DateTime.Now.AddDays(-1);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = getCraneOrderData();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

    #endregion

        #region 方法
        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        private void BindCombox()
        {
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        private DataTable getCraneOrderData()
        {
            string work_seqNo = this.textWORK_SEQ_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT WORK_SEQ_NO,TRUCK_NO,MAT_PROD_CODE,MAT_WT,REC_TIME FROM UACSAPP.UACS_L3_MAT_WEIGHT_INFO ";
            sqlText += "WHERE REC_TIME > '{0}' and REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            if (!string.IsNullOrEmpty(work_seqNo))
            {
                sqlText = string.Format("{0} and WORK_SEQ_NO LIKE '%{1}%' ", sqlText, work_seqNo);
            }
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

        private void btnQuery_Click_1(object sender, EventArgs e)
        {

        }
    }
}
