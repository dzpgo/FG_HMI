using System;
using System.Data;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;

namespace UACSControls
{
    public partial class FrmChangeFromStock : FormBase
    {
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        /// <summary>
        /// 行车号
        /// </summary>
        public string CraneNo { get; set; }


        public FrmChangeFromStock()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmChangeFromStock_Load(object sender, EventArgs e)
        {
            GetCraneOrderCurrent();
        }

        /// <summary>
        /// 获取当前指令信息
        /// </summary>
        private void GetCraneOrderCurrent()
        {
            try
            {
                string sqlText = @"SELECT ORDER_NO,CRANE_NO,BAY_NO,MAT_CODE,FROM_STOCK_NO,TO_STOCK_NO FROM UACS_CRANE_ORDER_CURRENT WHERE CRANE_NO = '" + CraneNo + "'; ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        txt_FromStock.Text = rdr["FROM_STOCK_NO"].ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CraneNo))
                {
                    MessageBox.Show("选择行车错误");
                }
                if (string.IsNullOrEmpty(txt_FromStock.Text.Trim()))
                {
                    MessageBox.Show("取料位置不能为空！");
                }
                string sqlText = @"UPDATE UACS_CRANE_ORDER_CURRENT SET FROM_STOCK_NO = '"+ txt_FromStock.Text.Trim() + "' WHERE CRANE_NO = '" + CraneNo + "';";

                DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                ParkClassLibrary.HMILogger.WriteLog("修改取料位置", CraneNo + "#行车，取料位置：" + txt_FromStock.Text, ParkClassLibrary.LogLevel.Info, this.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
