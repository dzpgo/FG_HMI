using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;


namespace UACSPopupForm
{
    public partial class TrolleyFrmYardToYardRequest : Form
    {
        private string CraneNo = string.Empty;
        public string BayNo = string.Empty;
        public string bayNO
        {
            get { return BayNo; }
            set { bayNO = value; }
        }

        DataTable dt = new DataTable();
        bool hasSetColumn;

        public TrolleyFrmYardToYardRequest()
        {
            InitializeComponent();
            //string str = Application.StartupPath + @"\Eighteen.ssk";
            //this.skinEngine1.SkinFile = str;
            //this.skinEngine1.SkinAllForm = false;
            this.Load += TrolleyFrmYardToYardRequest_Load;
        }

        void TrolleyFrmYardToYardRequest_Load(object sender, EventArgs e)
        {
            //Z33_Z32_Trolley trolley = new Z33_Z32_Trolley();
            //string bayNO = trolley.BayNO.ToString().Trim();
            BindcbbCrane(bayNO);
            BinddataGridViewData();
            if (this.BayNo.Trim() == "Z62")
            {
                txtToYard.Text = "Z62MC10001";
            }
            else
            {
                txtToYard.Text = "Z63MC10001";
            }
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            //if (CraneNo == "7_3")
            //{
            //    checkBox1.Text = "D413入口上料";
            //    button1.Visible = true;
            //}
            //else
            //{
            //    button1.Visible = false;
            //}
           

            //if (CraneNo == "7_0" || CraneNo == "7_1" || CraneNo == "7_2" || CraneNo == "7_3")
            //{
            //    BayNo = "Z53-1";
            //}
            //else if (CraneNo == "7_4" || CraneNo == "7_5" || CraneNo == "7_6")
            //{
            //    BayNo = "Z52-1";
            //}
            //else if (CraneNo == "7_7" || CraneNo == "7_8" || CraneNo == "7_9")
            //{
            //    BayNo = "Z51-1";
            //}
            //else
            //{
            //    BayNo = "999";
           // }

            //GetManuOrder(CraneNo);

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
            CraneNo = cbbCrane.Text.ToString().Trim();
            bool flag = false;
            try
            {
                string sql = @"update UACS_CRANE_MANU_ORDER set COIL_NO = null,FROM_STOCK = null,TO_STOCK = null,STATUS = 'EMPTY' ";
                sql += " WHERE CRANE_NO = '" + CraneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
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

            if (cbbCrane.Text.Trim() == "" || txtCoilNO.Text.Trim() == "" || txtFromYard.Text.Trim() == "" || txtToYard.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整");
                return;
            }


            bool flag = false;
            try
            {

                string coilNo = txtCoilNO.Text.Trim();
                string strFrom = txtFromYard.Text.Trim();
                string strTo = txtToYard.Text.Trim();
                CraneNo = cbbCrane.Text.ToString().Trim();

                string sql = @"update UACS_CRANE_MANU_ORDER  set COIL_NO = '" + coilNo + "',FROM_STOCK = '" + strFrom + "',TO_STOCK = '" + strTo + "',STATUS = 'INIT' ";
                sql += " WHERE CRANE_NO = '" + CraneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    this.dataGridView1.Rows[i].Cells["CHECK_COLUMN"].Value = 0;
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

            txtCoilNO.Text = GetCoilNoByStockNO(strFromYard);

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
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
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
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
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
        private void GetStockInfo(string _stockNo)
        {
            // step 1  检查库位是否正常
            int stock_Status = 999;
            int lock_Flag = 999;
            try
            {
                string sql1 = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE  where BAY_NO = '" + BayNo + "' AND STOCK_NO = '" + _stockNo + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql1))
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
            }
            else
            {
                lblStockStatus.Text = "库位异常-需要盘库";
            }

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

           

            //// step 3  查询钢卷信息

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
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["FROM_STOCK"] != DBNull.Value)
	                        txtFromYard.Text = rdr["FROM_STOCK"].ToString();
                        else
                            txtFromYard.Text = "";

                        if (rdr["COIL_NO"] != DBNull.Value)
	                        txtCoilNO.Text = rdr["COIL_NO"].ToString();
                        else
                             txtCoilNO.Text = "";

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



        private void  CheckStockInfo(string stockNo)
        {
            int stock_Status = 999;
            int lock_Flag = 999;
            try
            {
                string sql1 = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE  where BAY_NO = '" + BayNo + "' AND STOCK_NO = '" + stockNo + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql1))
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
            
            //if (stockNo == "D272VR1A01")
            //{
            //    return true;
            //}

            if (stock_Status == 0 && lock_Flag == 0)
            {
                lblStockStatus.Text = "库位正常！";
            }
            else
            {
                lblStockStatus.Text = "库位异常-需要盘库！";
            }
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
        private void BinddataGridViewData()
        {
            //string NextUnitNO = txtNextUnitNO.Text.ToString().Trim();
            //string PackFlag = cbbPackFlag.Text.ToString().Trim();
            string SaddleNO = txtSaddleNO.Text.ToString().Trim();
            //string bayNO = cbbBayNO.Text.ToString().Trim();
            string craneNo = string.Empty;

            //string sqlText = @"SELECT 0 AS CHECK_COLUMN,A.NEXT_UNIT_NO,A.COIL_NO,B.STOCK_NO,A.WEIGHT,A.OUTDIA,A.WIDTH,A.INDIA,A.FORBIDEN_FLAG,A.DUMMY_COIL_FLAG,A.PACK_CODE,
            //                        CASE
            //                            WHEN  A.PACK_FLAG = 1 THEN '已包装'
            //                            ELSE '未包装'
            //                        END as  PACK_FLAG ";
            //sqlText += "FROM UACS_YARDMAP_COIL A LEFT JOIN UACS_YARDMAP_STOCK_DEFINE B ";
            //sqlText += "ON A.COIL_NO = B.MAT_NO ";
            //sqlText += "  LEFT JOIN UACS_YARDMAP_COIL C ON C.COIL_NO = B.COIL_NO ";

            string sqlText = @"SELECT 0 AS CHECK_COLUMN,A.NEXT_UNIT_NO,A.COIL_NO,B.STOCK_NO,A.WEIGHT,A.OUTDIA,A.WIDTH,A.INDIA,A.FORBIDEN_FLAG,A.DUMMY_COIL_FLAG,A.PACK_CODE,CASE
                                        WHEN A.PACK_FLAG = 1 THEN '已包装'
                                        ELSE '未包装'
                                    END as  PACK_FLAG,B.BAY_NO";
            sqlText += " FROM UACS_YARDMAP_COIL A";
            sqlText += " LEFT JOIN UACS_YARDMAP_STOCK_DEFINE B ON A.COIL_NO = B.MAT_NO ";
            //sqlText += " LEFT JOIN UACS_YARDMAP_SADDLE_STOCK C ON B.STOCK_NO = C.STOCK_NO ";
            //sqlText += " LEFT JOIN UACS_YARDMAP_SADDLE_DEFINE D ON C.SADDLE_NO = D.SADDLE_NO ";
            sqlText += " WHERE B.MAT_NO != 'NULL' AND B.BAY_NO = '" + bayNO + "'";

            if (CoilNO.Text != "")
            {
                sqlText += " AND A.COIL_NO LIKE '%" + CoilNO.Text.Trim() + "%'";
            }

            if (txtSaddleNO.Text != "")
            {
                
                sqlText += " AND B.STOCK_NO LIKE '%" + txtSaddleNO.Text.Trim() + "%'";
            }


            //if (bayNO != "")
            //{
            //    sqlText += " AND B.BAY_NO LIKE = '" + bayNO + "'";
            //}

            //else if (NextUnitNO != "" && PackFlag != "")
            //{
            //    if (PackFlag == "已包装")
            //    {
            //        PackFlag = "1";
            //    }
            //    else if (PackFlag == "未包装")
            //    {
            //        PackFlag = "0";
            //    }
            //    sqlText += " WHERE A.NEXT_UNIT_NO = '" + NextUnitNO + "' AND A.PACK_FLAG = '" + PackFlag + "'";
            //}

            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dt.Clear();
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (!hasSetColumn)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = rdr.GetName(i);
                            dt.Columns.Add(dc);
                        }
                        dr[i] = rdr[i];
                    }
                    hasSetColumn = true;
                    dt.Rows.Add(dr);
                }
            }
            this.dataGridView1.DataSource = dt;
        }

        private void BindcbbCrane(string bayNO)
        {
            if (bayNO == "Z62")
            {
                cbbCrane.Items.Clear();
                cbbCrane.Items.Add("4_1");
                cbbCrane.Items.Add("4_2");
            }
            else if (bayNO == "Z63")
            {
                cbbCrane.Items.Clear();
                cbbCrane.Items.Add("4_3");
                cbbCrane.Items.Add("4_4");
                cbbCrane.Items.Add("4_5");
            }
            else
            {
                cbbCrane.Items.Clear();
                
            }
        }

        private void btnCoilSelect_Click_1(object sender, EventArgs e)
        {
            BinddataGridViewData();
            //string strStockNO = txtSaddleNO.Text.ToString().Trim();
            //string coilNO = CoilNO.Text.ToString().Trim();
            //foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
            //{
            //    if (dgvRow.Cells["STOCK_NO"].Value != null)
            //    {
            //        if (dgvRow.Cells["STOCK_NO"].Value.ToString() == strStockNO)
            //        {
            //            dataGridView1.FirstDisplayedScrollingRowIndex = dgvRow.Index;
            //            dgvRow.Cells["STOCK_NO"].Selected = true;
            //            //dgvRow.Cells["CHECK_COLUMN"].Value = true;
            //            dataGridView1.CurrentCell = dgvRow.Cells["STOCK_NO"];
            //        }
            //        else if (dgvRow.Cells["COIL_NO"].Value.ToString() == coilNO)
            //        {
            //            dataGridView1.FirstDisplayedScrollingRowIndex = dgvRow.Index;
            //            dgvRow.Cells["COIL_NO"].Selected = true;
            //            //dgvRow.Cells["CHECK_COLUMN"].Value = true;
            //            dataGridView1.CurrentCell = dgvRow.Cells["COIL_NO"];
            //        }
            //        else 
            //        {
            //            MessageBox.Show("查找不到该卷！");
            //            return;
            //        }

            //    }
            //}
        }

        //只能勾选一个选项
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if(i!=e.RowIndex && dataGridView1.CurrentCell.ColumnIndex==0)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                    cell.Value = false;
                }

                bool hasChecked = (bool)dataGridView1.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
                if (hasChecked)
                {
                    txtFromYard.Text = dataGridView1.Rows[i].Cells["STOCK_NO"].Value.ToString();            //起卷位
                    txtCoilNO.Text = dataGridView1.Rows[i].Cells["COIL_NO"].Value.ToString();
                    //count++;
                    //消除打钩
                    //this.dataGridView1.Rows[i].Cells["CHECK_COLUMN"].Value = 0;
                    //break;
                }
            }
        }

        private void txtFromYard_TextChanged(object sender, EventArgs e)
        {
            string stockFrom;
            stockFrom = txtFromYard.Text.ToUpper().ToString().Trim();
            txtFromYard.Text = stockFrom;
            txtFromYard.SelectionStart = txtFromYard.Text.Length;
            txtFromYard.SelectionLength = 0;
            if (txtFromYard.Text.Trim().Length == 8)
            {
                GetStockInfo(txtFromYard.Text);
                if (lblStockStatus.Text == "库位正常")
                {
                    string strFromYard = txtFromYard.Text.Trim();
                    txtCoilNO.Text = GetCoilNoByStockNO(strFromYard);
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
            if (txtToYard.Text.Trim().Length == 8)
            {
                CheckStockInfo(txtToYard.Text);
            }
        }

    }
}
