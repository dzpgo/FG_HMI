using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACS
{
    public partial class FrmStcokNoCoil : Form
    {
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

        private string bayNo = null;
        /// <summary>
        /// 跨别号
        /// </summary>
        public string BayNo
        {
            get { return bayNo; }
            set { bayNo = value; }
        }

        private string strNull = "999999";
        public FrmStcokNoCoil()
        {
            InitializeComponent();
            this.Load += FrmStcokNoCoil_Load;
        }

        void FrmStcokNoCoil_Load(object sender, EventArgs e)
        {
            //窗体固定大小
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

            txtBayNo.Text = bayNo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetStockNo_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                string sql = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO = '" + txtStockNo.Text + "' and BAY_NO = '" + bayNo + "' ";

                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["MAT_NO"] != DBNull.Value)
                        {
                            txtCoilNo.Text = rdr["MAT_NO"].ToString();
                        }
                        else
                            txtCoilNo.Text = strNull;

                        if (rdr["STOCK_STATUS"] != DBNull.Value)
                        {
                            txtStockStatus.Text = rdr["STOCK_STATUS"].ToString();
                        }
                        else
                            txtStockStatus.Text = strNull;

                        if (rdr["LOCK_FLAG"] != DBNull.Value)
                        {
                            txtStockFlag.Text = rdr["LOCK_FLAG"].ToString();
                        }
                        else
                            txtStockFlag.Text = strNull;

                        flag = true;

                    }
                }
            }
            catch (Exception er)
            {

                throw;
            }
            finally
            {
                if (!flag)
                {
                    txtCoilNo.Text = strNull;
                    txtStockStatus.Text = strNull;
                    txtStockFlag.Text = strNull;
                }

            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (txtStockNo.Text != strNull)
            {
                if (txtStockStatus.Text == "0" && txtStockFlag.Text == "0" || txtStockNo.Text.Trim() == "D271VR1A01")
                {
                    //触发事件
                    TransfEvent(txtStockNo.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("库位状态不正常，无法卸下");
                }

            }
            else
            {
                MessageBox.Show("卸下库位不能为空");
                return;
            }
        }

        public delegate void TransfDelegate(String stockNo);
        public event TransfDelegate TransfEvent; 
        

    }
}
