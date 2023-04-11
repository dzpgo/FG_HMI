using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UACSDAL;

namespace UACSPopupForm
{
    /// <summary>
    /// 归堆作业指令编辑
    /// </summary>
    public partial class FrmYardToYardRequest : Form
    {
        public string CraneNo { get; set; }
        private string BayNo = string.Empty;
        private string MatCode = string.Empty; //物料代码
        //防止弹出信息关闭画面
        bool isPopupMessage = false;


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
            lblCraneYardToYard.Text = CraneNo + "归堆作业指令";
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
                //BayNo = "Z01";
                BayNo = "A";
            }
            else if (CraneNo == "1" || CraneNo == "2" || CraneNo == "3" || CraneNo == "4")
            {
                BayNo = "A";
            }
            else
            {
                BayNo = "999";
            }
            //GetManuOrder(CraneNo);
            //txtBayNo.SelectAll();
            //txtBayNo.Focus();
            BindAeraDefine();
            BindLMR();

            //   if (txtFromYard.Text.Trim() != "")
            //{
            //       GetStockInfo(txtFromYard.Text.Trim(), txtCoilno.Text.Trim());
            // txtFromYard.Enabled = false;
            //}
            //   if (txtToYard.Text.Trim() != "")
            //   {
            //       txtToYard.Enabled = false;
            //   }

            this.Deactivate += new EventHandler(frmClose);
        }

        /// <summary>
        /// 绑定归堆跨
        /// </summary>
        private void BindAeraDefine()
        {
            try
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                dr = dt.NewRow();
                dr["ID"] = "无";
                dr["NAME"] = "无";
                dt.Rows.Add(dr);
                //查区域号 
                string sqlText = @"SELECT AREA_NO,AREA_NAME FROM UACS_YARDMAP_AREA_DEFINE WHERE AREA_TYPE = '0' AND YARD_NO = 'A' ";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        myDictionary.Add(rdr["AREA_NO"].ToString(), rdr["AREA_NO"].ToString().Remove(0, 1).PadLeft(2, '0'));
                    }
                }
                var dicSort = from objDic in myDictionary orderby objDic.Value ascending select objDic;  //排序
                foreach (KeyValuePair<string, string> keyvalue in dicSort)
                {
                    dr = dt.NewRow();
                    dr["ID"] = keyvalue.Key;
                    dr["NAME"] = keyvalue.Value.ToString();
                    dt.Rows.Add(dr);
                }
                //绑定数据
                cmb_AeraNo.ValueMember = "ID";
                cmb_AeraNo.DisplayMember = "NAME";
                cmb_AeraNo.DataSource = dt;
                //根据text值选中项
                this.cmb_AeraNo.SelectedIndex = 0;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 绑定归堆跨
        /// </summary>
        private void BindGridDefine(string areaNo)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                dr = dt.NewRow();
                dr["ID"] = "无";
                dr["NAME"] = "无";
                dt.Rows.Add(dr);
                if (!areaNo.Equals("无") && !string.IsNullOrEmpty(areaNo))
                {
                    //查区域号 
                    string sqlText = @"SELECT GRID_NO,GRID_NAME,AREA_NO FROM UACS_YARDMAP_GRID_DEFINE WHERE AREA_NO = '" + areaNo + "' ";
                    using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            dr = dt.NewRow();
                            dr["ID"] = rdr["GRID_NO"].ToString();
                            dr["NAME"] = rdr["GRID_NAME"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                }
                cmb_GridNo.ValueMember = "ID";
                cmb_GridNo.DisplayMember = "NAME";
                cmb_GridNo.DataSource = dt;
                //根据text值选中项
                this.cmb_GridNo.SelectedIndex = 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取废钢物料名
        /// </summary>
        private void GetMatInfo(string gridNo)
        {
            try
            {
                if (!gridNo.Equals("无") && !string.IsNullOrEmpty(gridNo))
                {
                    //查区域号 
                    string sqlText = @"SELECT A.MAT_CODE, B.MAT_CNAME FROM UACS_YARDMAP_GRID_DEFINE AS A LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE WHERE GRID_NO = '" + gridNo + "' ";
                    using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            MatCode = rdr["MAT_CODE"].ToString();
                            tb_MatCname.Text = rdr["MAT_CNAME"].ToString();
                        }
                    }
                }
                else
                {
                    tb_MatCname.Text = "";
                    MatCode = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 左中右 默认=中
        /// </summary>
        private void BindLMR()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                dr = dt.NewRow();
                dr["ID"] = "L";
                dr["NAME"] = "左";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["ID"] = "M";
                dr["NAME"] = "中";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["ID"] = "R";
                dr["NAME"] = "右";
                dt.Rows.Add(dr);
                //绑定数据
                cmb_LMR.ValueMember = "ID";
                cmb_LMR.DisplayMember = "NAME";
                cmb_LMR.DataSource = dt;
                //根据text值选中项
                this.cmb_LMR.SelectedIndex = 1;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 更改料区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_AeraNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_MatCname.Text = "";
            MatCode = "";
            BindGridDefine(cmb_AeraNo.SelectedValue.ToString().Trim());            
        }
        /// <summary>
        /// 更改物料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_GridNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMatInfo(cmb_GridNo.SelectedValue.ToString());
        }

        /// <summary>
        /// 取消归堆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            //确认提示
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否取消归堆？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                bool flag = false;
                try
                {
                    string sql = @"UPDATE UACS_CRANE_MANU_ORDER SET GRID_NO = null,MAT_NO = null,FROM_STOCK = null,TO_STOCK = null,STATUS = 'EMPTY' ";
                    sql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + CraneNo + "'";
                    DBHelper.ExecuteNonQuery(sql);
                    ParkClassLibrary.HMILogger.WriteLog("人工指令", "归堆作业指令编辑 - 取消归堆：行车" + CraneNo, ParkClassLibrary.LogLevel.Info, this.Text);
                    DialogResult dr = MessageBox.Show("已取消归堆，请清空当前行车指令！", "提示", MessageBoxButtons.OK);
                    if (dr == DialogResult.OK)
                    {
                        this.Close();
                        //return;
                    }
                    else
                    {
                        //this.Close();
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
            isPopupMessage = false;
        }
        /// <summary>
        /// 执行归堆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            //确认提示
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否执行归堆？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(cmb_AeraNo.Text.Trim()) || cmb_AeraNo.Text.Trim().Equals("无"))
                {
                    MessageBox.Show("请输入完整");
                    return;
                }
                if (string.IsNullOrEmpty(cmb_GridNo.Text.Trim()) || cmb_GridNo.Text.Trim().Equals("无"))
                {
                    MessageBox.Show("请输入完整");
                    return;
                }
                if (string.IsNullOrEmpty(cmb_LMR.Text.Trim()) || cmb_LMR.Text.Trim().Equals("无"))
                {
                    MessageBox.Show("请输入完整");
                    return;
                }
                if (string.IsNullOrEmpty(tb_MatCname.Text.Trim()))
                {
                    MessageBox.Show("请输入完整");
                    return;
                }
                try
                {
                    //var gridNo = cmb_GridNo.SelectedValue.ToString().Remove(0, 3).PadLeft(2, '0');
                    string sql = @"UPDATE UACS_CRANE_MANU_ORDER SET GRID_NO = '" + cmb_AeraNo.SelectedValue.ToString().Trim() + "',MAT_NO = '" + MatCode + "',FROM_STOCK = '" + cmb_AeraNo.Text.Trim() + "-" + cmb_LMR.SelectedValue.ToString() + "',TO_STOCK = '" + cmb_GridNo.SelectedValue.ToString() + "',STATUS = 'INIT' ";
                    sql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + CraneNo + "'";
                    DBHelper.ExecuteNonQuery(sql);                    
                    DialogResult dr = MessageBox.Show("确认执行归堆，请切换自动模式！", "提示", MessageBoxButtons.OK);
                    if (dr == DialogResult.OK)
                    {
                        this.Close();
                        //return;
                    }
                    else
                    {
                        //this.Close();
                    }
                    ParkClassLibrary.HMILogger.WriteLog("人工指令", "归堆作业指令编辑 - 执行归堆：行车" + CraneNo, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            isPopupMessage = false;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //txtFromYard.Enabled = false;

            //string strFromYard = txtBayNo.Text.Trim();

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

            //txtCoilno.Text = GetCoilNoByStockNO(strFromYard);

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
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetOrder_Click(object sender, EventArgs e)
        {
            //txtBayNo.Enabled = true;
            //txtFromStock.Enabled = true;
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
                string sql = @"SELECT STOCK_NO from UACS_YARDMAP_STOCK_DEFINE where BAY_NO = '" + BayNo + "' AND STOCK_NO = '" + _stockNo + "'";
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
                        stock_Status = Convert.ToInt32(rdr["STOCK_STATUS"]);
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
                string sql = @"SELECT * FROM UACS_CRANE_MANU_ORDER WHERE CRANE_NO = '" + _craneNo + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        //if (rdr["FROM_STOCK"] != DBNull.Value)
                        // txtBayNo.Text = rdr["FROM_STOCK"].ToString();
                        //else
                        //    txtBayNo.Text = "";

                        ////if (rdr["COIL_NO"] != DBNull.Value)
                        //// txtCoilno.Text = rdr["COIL_NO"].ToString();
                        ////else
                        ////     txtCoilno.Text = "";
                        //if (rdr["MAT_NO"] != DBNull.Value)
                        //    txtCoilno.Text = rdr["MAT_NO"].ToString();
                        //else
                        //    txtCoilno.Text = "";

                        //if (rdr["TO_STOCK"] != DBNull.Value)
                        //{
                        //    txtFromStock.Text = rdr["TO_STOCK"].ToString();
                        //}
                        //else
                        //    txtFromStock.Text = "";

                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }



        private bool CheckStockInfo(string stockNo)
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
            //string stockFrom;
            //stockFrom = txtBayNo.Text.ToUpper().ToString().Trim();
            //txtBayNo.Text = stockFrom;
            //txtBayNo.SelectionStart = txtBayNo.Text.Length;
            //txtBayNo.SelectionLength = 0;
            //if (txtBayNo.Text.Trim().Length == 8 || txtBayNo.Text.Trim().Length == 10)
            //{
            //    GetStockInfo(txtBayNo.Text);
            //    if (lblStockStatus.Text == "库位正常")
            //    {
            //        string strFromYard = txtBayNo.Text.Trim();
            //        txtCoilno.Text = GetCoilNoByStockNO(strFromYard);
            //    }
            //}
        }

        private void txtToYard_TextChanged(object sender, EventArgs e)
        {
            //string stockTo;
            //stockTo = txtFromStock.Text.ToUpper().ToString().Trim();
            //txtFromStock.Text = stockTo;
            //txtFromStock.SelectionStart = txtFromStock.Text.Length;
            //txtFromStock.SelectionLength = 0;
            //if (txtFromStock.Text.Trim().Length == 10)
            //{
            //    CheckStockInfo(txtFromStock.Text);
            //}
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmClose(object sender, EventArgs e)
        {
            try
            {
                if (!isPopupMessage)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
