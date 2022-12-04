using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ParkClassLibrary;
using UACSDAL;

namespace UACSPopupForm
{
    public partial class FrmUnitRotateRequest : Form
    {
        public string CraneNo { get; set; }
        public string UintNo { get; set; }
        public string CoilNo { get; set; }
        public string FromStock { get; set; }
        public string ToStock{ get; set; }

        private string craneNo = string.Empty;
        private string unitNo = string.Empty;
        private string coilNo = string.Empty;
        private string strFrom = string.Empty;
        private string strTo = string.Empty;
        private string FromStock1 = string.Empty;
        private string FromStock2 = string.Empty;

        private int manuValue;

        public FrmUnitRotateRequest()
        {
            InitializeComponent();
            this.Load += FrmYardToYardRequest_Load;
        }

        void FrmYardToYardRequest_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            txtCoilno.Text = CoilNo;
            cbbFromStock.Text = FromStock;
            cbbToStock.Text = ToStock;
            unitNo = UintNo;
            bindcbb();
            GetManuOrder();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if(cbbCraneNo.Text == "" || cbbFromStock.Text == "" ||txtCoilno.Text == "")
            {
                MessageBox.Show("请选择行车号和起卷位！");
                return;
            }
            bool flag = false;
            try
            {
                craneNo = cbbCraneNo.Text.Trim();
                coilNo = txtCoilno.Text.Trim();
                manuValue = 0;

                string sql1 = string.Format("UPDATE UACS_YARDMAP_COIL SET MANU_VALUE = '{0}' WHERE COIL_NO = '{1}'", manuValue, coilNo);
                DB2Connect.DBHelper.ExecuteNonQuery(sql1);

                string sql = @"update UACS_CRANE_MANU_ORDER set COIL_NO = null,FROM_STOCK = null,TO_STOCK = null,STATUS = 'EMPTY' ";
                sql += " WHERE CRANE_NO = '" + craneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                MessageBox.Show("已取消指令！");
                HMILogger.WriteLog("机组倒垛", unitNo + "机组入口," + craneNo + "行车取消机组倒垛指令。", LogLevel.Info, this.Text);
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
            string status;
            string sqlText = @"SELECT STATUS FROM UACS_CRANE_MANU_ORDER WHERE CRANE_NO = '{0}'";
            sqlText = String.Format(sqlText, cbbCraneNo.Text.Trim());
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                rdr.Read();
                status = rdr["STATUS"].ToString().Trim();
                if (status != "EMPTY" && status != "NULL")
                {
                    MessageBox.Show("行车已有指令！");
                    return;
                }

            }

            if (txtCoilno.Text.Trim() == "" || cbbFromStock.Text.Trim() == "" || cbbToStock.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整");
                return;
            }


            bool flag = false;
            try
            {
                craneNo = cbbCraneNo.Text.Trim();
                coilNo = txtCoilno.Text.Trim();
                strFrom = cbbFromStock.Text.Trim();
                strTo = cbbToStock.Text.Trim();
                if (cbbRotate.Text.Trim() == "旋转")
                {
                    manuValue = 1;
                }
                else
                {
                    manuValue = 0;
                }

                string sql1 = string.Format("UPDATE UACS_YARDMAP_COIL SET MANU_VALUE = '{0}' WHERE COIL_NO = '{1}'", manuValue, coilNo);
                DB2Connect.DBHelper.ExecuteNonQuery(sql1);

                string sql = @"update UACS_CRANE_MANU_ORDER  set COIL_NO = '" + coilNo + "',FROM_STOCK = '" + strFrom + "',TO_STOCK = '" + strTo + "',STATUS = 'INIT' ";
                sql += " WHERE CRANE_NO = '" + craneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                MessageBox.Show("已下达指令！");
                if(manuValue == 1)
                {
                    HMILogger.WriteLog("机组倒垛", unitNo + "机组入口,执行行车：" + craneNo + ",起卷位：" + strFrom + ",钢卷" + txtCoilno.Text + "设置为旋转180°", LogLevel.Info, this.Text);
                }
                else if (manuValue == 0)
                {
                    HMILogger.WriteLog("机组倒垛", unitNo + "机组入口,执行行车：" + craneNo + ",起卷位：" + strFrom + ",钢卷" + txtCoilno.Text + "设置为不旋转°", LogLevel.Info, this.Text);
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


        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindcbb()
        {
            cbbCraneNo.Items.Clear();
            cbbFromStock.Items.Clear();
            cbbToStock.Items.Clear();
            cbbRotate.Text = "旋转";
            if (unitNo=="D171")
            {
                cbbCraneNo.Items.Add("6_4");
                cbbCraneNo.Items.Add("6_5");
                cbbCraneNo.Items.Add("6_6");

                cbbFromStock.Items.Add("D171WR0001");
                cbbFromStock.Items.Add("D171WR0002");
                FromStock1 = "D171WR0001";
                FromStock2 = "D171WR0002";

                cbbToStock.Items.Add("D171WR0001");
                cbbToStock.Text = "D171WR0001";
            }
            else if (unitNo == "D102")
            {
                cbbCraneNo.Items.Add("1_1");
                cbbCraneNo.Items.Add("1_2");
                cbbCraneNo.Items.Add("1_3");               

                cbbFromStock.Items.Add("D102WR0001");
                cbbFromStock.Items.Add("D102WR0002");
                cbbFromStock.Items.Add("D102WR0003");
                cbbFromStock.Items.Add("D102WR0004");
                cbbFromStock.Items.Add("D102WR0005");
                cbbFromStock.Items.Add("D102WR0006");
                cbbFromStock.Items.Add("D102WR0007");
                cbbFromStock.Items.Add("D102WR0008");
                cbbFromStock.Items.Add("D102WR0009");
                cbbFromStock.Items.Add("D102WR0010");
                //FromStock1 = "D172WR0001";
                //FromStock2 = "D172WR0002";
                //cbbToStock.Items.Add("D172WR0001");
                //cbbToStock.Text = "D172WR0001";
            }
            else if (unitNo == "D172")
            {
                cbbCraneNo.Items.Add("6_4");
                cbbCraneNo.Items.Add("6_5");
                cbbCraneNo.Items.Add("6_6");

                cbbFromStock.Items.Add("D172WR0001");
                cbbFromStock.Items.Add("D172WR0002");
                FromStock1 = "D172WR0001";
                FromStock2 = "D172WR0002";
                cbbToStock.Items.Add("D172WR0001");
                cbbToStock.Text = "D172WR0001";
            }
            else if (unitNo == "D173")
            {
                cbbCraneNo.Items.Add("6_1");
                cbbCraneNo.Items.Add("6_2");
                cbbCraneNo.Items.Add("6_3");

                cbbFromStock.Items.Add("D173WR0001");
                cbbFromStock.Items.Add("D173WR0002");
                FromStock1 = "D173WR0001";
                FromStock2 = "D173WR0002";
                cbbToStock.Items.Add("D173WR0001");
                cbbToStock.Text = "D173WR0001";
            }
            else if (unitNo == "D174")
            {
                cbbCraneNo.Items.Add("6_7");
                cbbCraneNo.Items.Add("6_8");
                cbbCraneNo.Items.Add("6_9");
                cbbCraneNo.Items.Add("6_10");

                cbbFromStock.Items.Add("D174WR0001");
                cbbFromStock.Items.Add("D174WR0002");
                FromStock1 = "D174WR0001";
                FromStock2 = "D174WR0002";
                cbbToStock.Items.Add("D174WR0001");
                cbbToStock.Items.Add("D174WR0002");
                cbbToStock.Text = "D174WR0001";
            }
            else if (unitNo == "D122")
            {
                cbbCraneNo.Items.Add("4_1");
                cbbCraneNo.Items.Add("4_2");

                cbbFromStock.Items.Add("D122WR0001");
                cbbFromStock.Items.Add("D122WR0002");
                cbbFromStock.Items.Add("D122WR0003");
                cbbToStock.Items.Add("D122WR0001");
                cbbToStock.Items.Add("D122WR0002");
                cbbToStock.Items.Add("D122WR0003");
            }
            else if (unitNo == "D312")
            {
                cbbCraneNo.Items.Add("4_4");
                cbbCraneNo.Items.Add("4_5");

                cbbFromStock.Items.Add("D312WR0001");
                cbbFromStock.Items.Add("D312WR0002");
                cbbFromStock.Items.Add("D312WR0003");
                cbbFromStock.Items.Add("D312WR0004");
                cbbFromStock.Items.Add("D312WR0005");
                cbbToStock.Items.Add("D312WR0001");
                cbbToStock.Items.Add("D312WR0002");
                cbbToStock.Items.Add("D312WR0003");
                cbbToStock.Items.Add("D312WR0004");
                cbbToStock.Items.Add("D312WR0005");
            }
            else if (unitNo == "D408")
            {
                cbbCraneNo.Items.Add("4_3");
                cbbCraneNo.Items.Add("4_4");

                cbbFromStock.Items.Add("D408WR0001");
                cbbFromStock.Items.Add("D408WR0002");
                cbbFromStock.Items.Add("D408WR0003");
                cbbToStock.Items.Add("D408WR0001");
                cbbToStock.Items.Add("D408WR0002");
                cbbToStock.Items.Add("D408WR0003");
            }
            else if (unitNo == "D508")
            {
                cbbCraneNo.Items.Add("4_1");
                cbbCraneNo.Items.Add("4_2");

                cbbFromStock.Items.Add("D508WR0001");
                cbbFromStock.Items.Add("D508WR0002");
                cbbFromStock.Items.Add("D508WR0003");
                cbbToStock.Items.Add("D508WR0001");
                cbbToStock.Items.Add("D508WR0002");
                cbbToStock.Items.Add("D508WR0003");
            }
            else
            {
                cbbCraneNo.Items.Add("1_1");
                cbbCraneNo.Items.Add("1_2");
                cbbCraneNo.Items.Add("1_3");
            }
        }

        private void cbbFromStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string sql = @"SELECT MAT_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO = '{0}'";
            sql = String.Format(sql, cbbFromStock.Text.Trim());
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                rdr.Read();
                txtCoilno.Text = rdr["MAT_NO"].ToString().Trim();

            }

            cbbToStock.Text = cbbFromStock.Text;
        }

        private void GetManuOrder()
        {
            try
            {
                string sql = @"SELECT A.CRANE_NO CRANE_NO,A.FROM_STOCK FROM_STOCK, A.COIL_NO COIL_NO, A.TO_STOCK TO_STOCK,
                                (CASE B.MANU_VALUE  
                                WHEN 1 THEN '旋转' 
                                else '不旋转'
                                end ) MANU_VALUE 
                                FROM UACS_CRANE_MANU_ORDER A LEFT JOIN UACS_YARDMAP_COIL B ON B.COIL_NO = A.COIL_NO 
                                WHERE A.FROM_STOCK like '%" + unitNo + "%' OR A.FROM_STOCK like '%" + unitNo + "%'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["CRANE_NO"] != DBNull.Value)
                            cbbCraneNo.Text = rdr["CRANE_NO"].ToString();
                        else
                            cbbCraneNo.Text = "";

                        if (rdr["FROM_STOCK"] != DBNull.Value)
                            cbbFromStock.Text = rdr["FROM_STOCK"].ToString();
                        else
                            cbbFromStock.Text = "";

                        if (rdr["COIL_NO"] != DBNull.Value)
                            txtCoilno.Text = rdr["COIL_NO"].ToString();
                        else
                            txtCoilno.Text = "";

                        if (rdr["TO_STOCK"] != DBNull.Value)
                        {
                            cbbToStock.Text = rdr["TO_STOCK"].ToString();
                        }
                        else
                            cbbToStock.Text = "";

                        if (rdr["MANU_VALUE"] != DBNull.Value)
                        {
                            cbbRotate.Text = rdr["MANU_VALUE"].ToString();
                        }
                        else
                            cbbRotate.Text = "";
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
