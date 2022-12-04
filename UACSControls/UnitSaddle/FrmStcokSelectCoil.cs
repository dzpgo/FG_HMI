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
    public partial class FrmStcokSelectCoil : Form
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
        public FrmStcokSelectCoil()
        {
            InitializeComponent();
            this.Load += FrmStcokSelectCoil_Load;
        }

        void FrmStcokSelectCoil_Load(object sender, EventArgs e)
        {
            //窗体固定大小
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

            txtBayNo.Text = bayNo;

        }
        /// <summary>
        /// 根据库位查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetStockNo_Click(object sender, EventArgs e)
        {

            bool flag = false;
            try
            {
                string sql = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO = '"+txtStockNo.Text+"' and BAY_NO = '"+bayNo+"' ";

                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while(rdr.Read())
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

        /// <summary>
        /// 根据钢卷查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCoilNo_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                string sql = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO = '" + txtStockNo.Text + "' and BAY_NO = '" + bayNo + "' ";

                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["STOCK_NO"] != DBNull.Value)
                        {
                            txtStockNo.Text = rdr["STOCK_NO"].ToString();
                        }
                        else
                            txtStockNo.Text = strNull;

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
                    txtStockNo.Text = strNull;
                    txtStockStatus.Text = strNull;
                    txtStockFlag.Text = strNull;
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (txtStockNo.Text != strNull && txtCoilNo.Text != strNull)
            {
                if (txtStockStatus.Text == "2" && txtStockFlag.Text =="0")
                {
                    //触发事件
                    TransfEvent(txtStockNo.Text, txtCoilNo.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("库位状态不正常，无法吊运");
                }
                
            }
            else
            {
                MessageBox.Show("库位和钢卷号不能为空");
                return;
            }
        }

        public delegate void TransfDelegate(String stockNo,string coilNo);
        public event TransfDelegate TransfEvent; 
    }
}
