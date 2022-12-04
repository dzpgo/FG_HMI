using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UACSPopupForm
{
    public partial class FrmYardToYardRequest : Form
    {
        public string CraneNo { get; set; }

        private string BayNo = string.Empty;
        

        
        public FrmYardToYardRequest()
        {
            InitializeComponent();
            //string str = Application.StartupPath + @"\Eighteen.ssk";
            //this.skinEngine1.SkinFile = str;
            //this.skinEngine1.SkinAllForm = false;
            this.Load += FrmYardToYardRequest_Load;
        }

        #region 数据库连接
        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;
        //连接数据库
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
                    }
                    catch (System.Exception e)
                    {
                        //throw e;
                    }
                }
                return dbHelper;
            }
        }
        #endregion

        void FrmYardToYardRequest_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            lblCraneYardToYard.Text = CraneNo + "人工指令";                 
            if (CraneNo == "4_1" || CraneNo == "4_2" || CraneNo == "6_3")
            {
                BayNo = "Z62";
            }
            else if (CraneNo == "4_3" || CraneNo == "4_4" || CraneNo == "4_5")
            {
                BayNo = "Z63";
            }
            else if (CraneNo == "1_1" || CraneNo == "1_2" || CraneNo == "1_3")
            {
                BayNo = "Z01";
            }
            else
            {
                BayNo = "999";
            }
            GetManuOrder(CraneNo);
            txtFromYard.SelectAll();
            txtFromYard.Focus();

         //   if (txtFromYard.Text.Trim() != "")
	        //{
         //       GetStockInfo(txtFromYard.Text.Trim(), txtCoilno.Text.Trim());
		       // txtFromYard.Enabled = false;
	        //}
         //   if (txtToYard.Text.Trim() != "")
         //   {
         //       txtToYard.Enabled = false;
         //   }

        }

       


        private void BtnClose_Click(object sender, EventArgs e)
        {

            bool flag = false;
            try
            {
                string sql = @"update UACS_CRANE_MANU_ORDER set COIL_NO = null,FROM_STOCK = null,TO_STOCK = null,STATUS = 'EMPTY' ";
                sql += " WHERE CRANE_NO = '" + CraneNo + "'";
                DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                flag = true;
                MessageBox.Show(er.Message);
            }

            if (!flag)
            {
                Thread.Sleep(500);
                this.Close();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {

            if (txtCoilno.Text.Trim() == "" || txtFromYard.Text.Trim() == "" || txtToYard.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整");
                return;
            }
            bool From_status = GetStockInfo(txtFromYard.Text);
            bool To_status = CheckStockInfo(txtToYard.Text);

            bool flag = false;
            try
            {
                if(From_status && To_status)
                {
                    string coilNo = txtCoilno.Text.Trim();
                    string strFrom = txtFromYard.Text.Trim();
                    string strTo = txtToYard.Text.Trim();

                    string sql = @"update UACS_CRANE_MANU_ORDER  set COIL_NO = '" + coilNo + "',FROM_STOCK = '" + strFrom + "',TO_STOCK = '" + strTo + "',STATUS = 'INIT' ";
                    sql += " WHERE CRANE_NO = '" + CraneNo + "'";
                    DBHelper.ExecuteNonQuery(sql);
                }
                else
                {
                    MessageBox.Show("请检查库位！");
                    return;
                }
            }
            catch (Exception er)
            {
                flag = true;
                MessageBox.Show(er.Message);
            }

            if (!flag)
            {
                Thread.Sleep(500);
                this.Close();
            }
           
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //txtFromYard.Enabled = false;

            string strFromYard = txtFromYard.Text.Trim();

            //if (CheckStockNo(strFromYard))
            //{
            //    if (GetCoilNoByStockNO(strFromYard) != null)
            //        txtCoilno.Text = GetCoilNoByStockNO(strFromYard);
            //    else
            //        txtCoilno.Text = "";
            //}
            //else if (strFromYard.Contains("-"))
            //{
            //    txtFromYard.Text = JointStockNo(strFromYard);

            //    if (GetCoilNoByStockNO(JointStockNo(strFromYard)) != null)
            //        txtCoilno.Text = GetCoilNoByStockNO(JointStockNo(strFromYard));
            //    else
            //        txtCoilno.Text = "";
            //}
            //else
            //{
            //    MessageBox.Show("请输入指定格式！！！");
            //    return;
            //}

            txtCoilno.Text = GetCoilNoByStockNO(strFromYard);

            //if (txtCoilno.Text.Trim() != "")
            //{
            //    //GetStockInfo(txtFromYard.Text.Trim(), txtCoilno.Text.Trim());

            //    //if (lblStockStatus.Text.Trim() == "库位正常" && lblCoilStatus.Text.Trim() == "否")
            //    //{
            //    //    BtnOk.Enabled = true;
            //    //    BtnCloseToYard.Enabled = true;
            //    //}
            //    //else
            //    //{
            //    //    BtnOk.Enabled = false;
            //    //    BtnCloseToYard.Enabled = false;
            //    //}
            //}
            //else
            //{
            //    BtnOk.Enabled = false;
            //    BtnCloseToYard.Enabled = false;
            //}

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetOrder_Click(object sender, EventArgs e)
        {
            txtFromYard.Enabled = true;
            txtToYard.Enabled = true;
        }


        /// <summary>
        /// 检查库位
        /// </summary>
        /// <param name="_stockNo"></param>
        /// <returns></returns>
        private bool CheckStockNo(string _stockNo)
        {
            bool flag = false;
            try
            {
                string sql = @"SELECT STOCK_NO from UACS_YARDMAP_STOCK_DEFINE where BAY_NO = '" + BayNo + "' AND STOCK_NO = '"+_stockNo+"'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        flag = true;
                    }
                    
                }
            }
            catch (Exception)
            {

                return flag;
            }
            return flag;
        }

        /// <summary>
        /// 拼接库位
        /// </summary>
        /// <param name="_Stock"></param>
        /// <returns></returns>
        private string JointStockNo(string _Stock)
        {
            StringBuilder strStockNo = null;
            if (CraneNo == "7_3")
            {
                strStockNo = new StringBuilder("Z34");
            }
            else
            {
                strStockNo = new StringBuilder(BayNo.Substring(0, 3));
            }
            try
            {       
                string[] listYrad = _Stock.Split('-');
                if (listYrad.Length != 2)
                {
                    MessageBox.Show("请输入指定格式！！！");
                    return null;
                }

                string strRow = listYrad[0];
                string strCol = listYrad[1];   
       
               
                if (strRow.Length == 1)
                    strStockNo.Append("00" + strRow);
                if (strRow.Length == 2)
                    strStockNo.Append("0" + strRow);
                if (strRow.Length == 3)
                    strStockNo.Append(strRow);

                if (strCol.Length == 1)
                    strStockNo.Append("00" + strCol);
                if (strCol.Length == 2)
                    strStockNo.Append("0" + strCol);
                if (strCol.Length == 3)
                    strStockNo.Append(strCol);
                strStockNo.Append("1");
            }
            catch (Exception)
            {
                return null;
            }
            return strStockNo.ToString();
        }

        /// <summary>
        /// 获取钢卷号
        /// </summary>
        /// <param name="_StockNO"></param>
        /// <returns></returns>
        private string GetCoilNoByStockNO(string _stockNo)
        {
            string coilNo = null;
            try
            {
                string sql = @"SELECT MAT_NO from UACS_YARDMAP_STOCK_DEFINE where  STOCK_NO = '" + _stockNo + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        coilNo = rdr["MAT_NO"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                return coilNo;
            }
            return coilNo;
           
        }


        /// <summary>
        /// 获取库位鞍座信息
        /// </summary>
        /// <param name="_stockNo"></param>
        /// <param name="_coilNo"></param>
        private bool GetStockInfo(string _stockNo)
        {
            // step 1  检查库位是否正常
            bool flag = false;
            int stock_Status = 999;
            int lock_Flag = 999;
            try
            {
                string sql1 = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE  where BAY_NO = '" + BayNo + "' AND STOCK_NO = '" + _stockNo + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql1))
                {
                     while (rdr.Read())
                    {
                        stock_Status = Convert.ToInt32( rdr["STOCK_STATUS"]);
                        lock_Flag = Convert.ToInt32(rdr["LOCK_FLAG"]);
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            if (stock_Status == 2 && lock_Flag == 0)
            {
                lblStockStatus.Text = "库位正常";
                flag = true;
            }
            else
            {
                lblStockStatus.Text = "库位异常-需要盘库";
                flag = false;
            }
            return flag;
            // step 2 检查钢卷是否存在多库位
            //int index = 0;    //计算有几条数据
            //try
            //{
            //    string sql2 = @"SELECT * FROM  UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO = '" + _coilNo + "'";
            //    using (IDataReader rdr = DBHelper.ExecuteReader(sql2))
            //    {
            //        while (rdr.Read())
            //        {
            //            index++;
            //        }
            //    }   
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}

            //if (index > 1)
            //{
            //    lblCoilStatus.Text = "是";   
            //}
            //else
            //{
            //    lblCoilStatus.Text = "否";  
            //}

           

            // step 3  查询钢卷信息

            //lblWeight.Text = "";
            //lblWidth.Text = "";
            //lblInDia.Text = "";
            //lblOutdia.Text = "";
            //lblPackCode.Text = "";
            //try
            //{
            //    string sql3 = @"SELECT * FROM UACS_YARDMAP_COIL  where COIL_NO = '" + _coilNo + "'";
            //    using (IDataReader rdr = DBHelper.ExecuteReader(sql3))
            //    {
            //         while (rdr.Read())
            //        {
            //            lblWeight.Text = rdr["WEIGHT"].ToString();
            //            lblWidth.Text = rdr["WIDTH"].ToString();
            //            lblInDia.Text = rdr["INDIA"].ToString();
            //            lblOutdia.Text = rdr["OUTDIA"].ToString();
            //            lblPackCode.Text = rdr["PACK_CODE"].ToString();
            //        }
            //    }
            //}
            //catch (Exception er)
            //{
            //    MessageBox.Show(er.Message);
            //}
        }


        private void GetManuOrder(string _craneNo)
        {
            try 
	        {	        
		       string sql  = @"SELECT * FROM UACS_CRANE_MANU_ORDER WHERE CRANE_NO = '"+_craneNo+"'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["FROM_STOCK"] != DBNull.Value)
	                        txtFromYard.Text = rdr["FROM_STOCK"].ToString();
                        else
                            txtFromYard.Text = "";

                        if (rdr["COIL_NO"] != DBNull.Value)
	                        txtCoilno.Text = rdr["COIL_NO"].ToString();
                        else
                             txtCoilno.Text = "";

                        if (rdr["TO_STOCK"] != DBNull.Value)
                        {
                            txtToYard.Text = rdr["TO_STOCK"].ToString();
                        }
                        else
                            txtToYard.Text = "";
                       
                    }
                }
	        }
	        catch (Exception er)
	        {	
		       MessageBox.Show(er.Message);
	        }
        }



        private bool  CheckStockInfo(string stockNo)
        {
            bool flag = false;
            int stock_Status = 999;
            int lock_Flag = 999;
            try
            {
                string sql1 = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE  where BAY_NO = '" + BayNo + "' AND STOCK_NO = '" + stockNo + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql1))
                {
                    while (rdr.Read())
                    {
                        stock_Status = Convert.ToInt32(rdr["STOCK_STATUS"]);
                        lock_Flag = Convert.ToInt32(rdr["LOCK_FLAG"]);

                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                //return false;
            }

            if (stock_Status == 0 && lock_Flag == 0)
            {
                //return true;
                lblStockStatus.Text = "库位正常！";
                flag = true;
            }
            else
            {
                //return false;
                lblStockStatus.Text = "库位异常-需要盘库！";
                flag = false;
            }
            return flag;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox1.Checked)
            //{
            //    if (CraneNo == "7_3")
            //    {
            //        txtToYard.Text = "D413入口上料";
            //    }
            //    else
            //    {
            //        txtToYard.Text = "拆包-返修-取样";
            //    }             
            //    txtToYard.ReadOnly = true;
            //}
            //else
            //{
            //    txtToYard.Text = "";
            //    txtToYard.ReadOnly = false;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //txtFromYard.Text = "D272VC1A03";
            //btnSelect_Click(null,null);
            //txtToYard.Text = "D272VR1A01";

        }

        private void txtFromYard_TextChanged(object sender, EventArgs e)
        {
            string stockFrom;
            stockFrom = txtFromYard.Text.ToUpper().ToString().Trim();
            txtFromYard.Text = stockFrom;
            txtFromYard.SelectionStart = txtFromYard.Text.Length;
            txtFromYard.SelectionLength = 0;
            if (txtFromYard.Text.Trim().Length == 8 || txtFromYard.Text.Trim().Length == 10)
            {
                GetStockInfo(txtFromYard.Text);
                if (lblStockStatus.Text == "库位正常")
                {
                    string strFromYard = txtFromYard.Text.Trim();
                    txtCoilno.Text = GetCoilNoByStockNO(strFromYard);
                }
            }
        }

        private void txtToYard_TextChanged(object sender, EventArgs e)
        {
            string stockTo;
            stockTo = txtToYard.Text.ToUpper().ToString().Trim();
            txtToYard.Text = stockTo;
            txtToYard.SelectionStart = txtToYard.Text.Length;
            txtToYard.SelectionLength = 0;
            if (txtToYard.Text.Trim().Length == 10)
            {
                CheckStockInfo(txtToYard.Text);
            }
        }

    }
}
