using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSPopupForm
{
    public partial class PDAUSserADD : Form
    {

        public PDAUSserADD()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUSERID.Text != "" && txtUSERNAME.Text != "" && txtPRIORITY.Text != "" && txtCREW.Text != "" && txtSHIFT.Text != "" && txtPASSWORD.Text != "" && txtAGAINPASSWORD.Text != "")
                {
                    if (txtPASSWORD.Text == txtAGAINPASSWORD.Text)
                    {
                        string sql = @"SELECT USERID, USERNAME, PRIORITY, CREW, SHIFT, PASSWORD FROM UACS_USER_INF";
                        using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                        {
                            while (rdr.Read())
                            {
                                if (txtPASSWORD.Text.ToString().Trim() == rdr["USERID"].ToString().Trim())
                                {
                                    return;
                                }
                            }
                        }
                        sql = @"INSERT INTO UACS_USER_INF(USERID, USERNAME, PRIORITY, CREW, SHIFT PASSWORD) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')";
                        sql = string.Format(sql, txtUSERID.Text.Trim(), txtUSERNAME.Text.Trim(), txtPRIORITY.Text.Trim(), txtCREW.Text.Trim(), txtSHIFT.Text.Trim(), txtPASSWORD.Text.Trim());
                        DB2Connect.DBHelper.ExecuteNonQuery(sql);
                        MessageBox.Show("人员添加完成!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
