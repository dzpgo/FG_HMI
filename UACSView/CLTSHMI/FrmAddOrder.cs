using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSView.CLTSHMI
{
    public partial class FrmAddOrder : Form
    {
        public FrmAddOrder()
        {
            InitializeComponent();
        }

        private void comboWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboWorkType.Text == "倒垛")
            {
                txtToStock.Text = "Z01";
                txtToStock.ReadOnly = true;
                txtFromStock.Text = "";
            }
            else if(comboWorkType.Text == "修复卷入库")
            {
                txtFromStock.Text = "Z01A5";
                txtToStock.Text = "Z01";
                txtFromStock.ReadOnly = true;
                txtToStock.ReadOnly = true;
            }
            else
            {
                txtFromStock.Text = "";
                txtToStock.Text = "Z01A5";
                txtToStock.ReadOnly = true;
            }
        }

        private void txtCoilNo_TextChanged(object sender, EventArgs e)
        {
            if(comboWorkType.Text != "修复卷入库")
            {
                if(txtCoilNo.Text.Length > 11)
                {
                    string sqlText = @"SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO = '" + txtCoilNo.Text.Trim() + "'";
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            if (rdr["STOCK_NO"] != DBNull.Value)
                            {
                                txtFromStock.Text = rdr["STOCK_NO"].ToString().Trim();
                            }
                        }
                    }
                }
            }
        }

        private void FrmAddOrder_Load(object sender, EventArgs e)
        {
            comboWorkType.Text = "倒垛";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(txtCoilNo.Text != "" && txtFromStock.Text != "")
            {
                string sqlText = @"INSERT INTO CLTS_CRANE_ORDER (COIL_NO ,FROM_STOCK ,TO_STOCK ,BAY_NO ) VALUES ('{0}', '{1}', '{2}', 'Z01')";
                sqlText = string.Format(sqlText, txtCoilNo.Text.Trim(), txtFromStock.Text.Trim(), txtToStock.Text.Trim());
                DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                MessageBox.Show("添加成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入完整！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
