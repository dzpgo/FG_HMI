using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using ParkClassLibrary;

namespace UACSPopupForm
{
    public partial class FrmSetCoilWidth : FormBase
    {
        public FrmSetCoilWidth()
        {
            InitializeComponent();
        }
        public string saddleNO = string.Empty;
        public string SaddleNO
        {
            get { return saddleNO; }
            set { SaddleNO = value; }

        }
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
        private void BtnOk_Click(object sender, EventArgs e)
        {
            string sql = String.Empty;
            if (txtPackingCode.Text == "" || txtCoilWidth.Text == "")
            {
                MessageBox.Show("请输入信息！");
            }
            else
            {
                if (cmbCoilType.Text == "普冷")
                {
                    sql = @"UPDATE UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE SET PLATE_WIDTH = " + Convert.ToInt32(txtCoilWidth.Text.Trim()) + ", PACKING_CODE = '" + txtPackingCode.Text.Trim() + "', COIL_TYPE = '2014', CONFIRM_FLAG = 10 WHERE STOCK_NO = '" + SaddleNO + "'";
                }
                else if (cmbCoilType.Text == "镀锌")
                {
                    sql = @"UPDATE UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE SET PLATE_WIDTH = " + Convert.ToInt32(txtCoilWidth.Text.Trim()) + ", PACKING_CODE = '" + txtPackingCode.Text.Trim() + "', COIL_TYPE = '2015',CONFIRM_FLAG = 10 WHERE STOCK_NO = '" + SaddleNO + "'";
                }
                else
                {
                    sql = @"UPDATE UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE SET PLATE_WIDTH = " + Convert.ToInt32(txtCoilWidth.Text.Trim()) + ", PACKING_CODE = '" + txtPackingCode.Text.Trim() + "', CONFIRM_FLAG = 10 WHERE STOCK_NO = '" + SaddleNO + "'";
                }
                DBHelper.ExecuteNonQuery(sql);
                HMILogger.WriteLog("按规格吊入", "鞍座号：" + SaddleNO + "卷类型：" + cmbCoilType.Text + "卷宽：" + txtCoilWidth.Text, LogLevel.Info, this.Text);
                this.Close(); 
            }
        }

        private void FrmSetCoilWidth_Activated(object sender, EventArgs e)
        {
            txtPackingCode.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCoilWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnOk.Focus();
        }
    }
}
