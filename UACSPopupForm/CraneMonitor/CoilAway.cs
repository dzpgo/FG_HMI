using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Baosight.iSuperframe.Forms;
using ParkClassLibrary;
using Baosight.iSuperframe.TagService;

namespace UACSPopupForm
{
    public partial class CoilAway : Form
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        public string saddleNO = string.Empty;
        public string saddleno
        {
            get { return saddleNO; }
            set { saddleno = value; }
        }

        public string coilNO = string.Empty;
        public string coilno
        {
            get { return coilNO; }
            set { coilno = value; }
        }

        public string areaNO = string.Empty;
        public string areano
        {
            get { return areaNO; }
            set { areano = value; }
        }
        public CoilAway()
        {
            InitializeComponent();
        }

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

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if(txtCoilNO.Text == "")
            {
                MessageBox.Show("请输入卷号！");
            }
            else
            {
                if(areano.Contains("PR"))
                {
                    string sqlText = @"select * from UACS_YARDMAP_STOCK_DEFINE where STOCK_NO = '" + txtSaddle.Text + "'";
                    using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            if (Convert.ToInt32(rdr["STOCK_STATUS"].ToString()) != 2 || Convert.ToInt32(rdr["LOCK_FLAG"].ToString()) != 0 || (rdr["MAT_NO"].ToString() == ""))
                            {
                                string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 2, LOCK_FLAG = 0 , MAT_NO = '" + txtCoilNO.Text + "'WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                                DBHelper.ExecuteNonQuery(sql);
                            }

                            string sql1 = @"UPDATE UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30 ,PLATE_WIDTH = NULL, PACKING_CODE = NULL, COIL_NO = NULL WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                            DBHelper.ExecuteNonQuery(sql1);
                            string sql2 = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = '" + txtCoilNO.Text + "'WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                            DBHelper.ExecuteNonQuery(sql2);
                            MessageBox.Show("确认吊离成功！");
                            HMILogger.WriteLog("吊离", "鞍座号：" + txtSaddle.Text + "钢卷号：" + txtCoilNO.Text, LogLevel.Info, this.Text);
                            this.Close();
                        }
                    }                                   
                }
                else if(areano.Contains("PC"))
                {
                    int coilDirection = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                    int  packFlag = Convert.ToInt32(comboBox2.SelectedValue.ToString());

                    string sqlText = @"select * from UACS_YARDMAP_STOCK_DEFINE where STOCK_NO = '" + txtSaddle.Text + "'";
                    using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            if (Convert.ToInt32(rdr["STOCK_STATUS"].ToString()) != 2 || Convert.ToInt32(rdr["LOCK_FLAG"].ToString()) != 0 || (rdr["MAT_NO"].ToString() == ""))
                            {
                                string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 2, LOCK_FLAG = 0 , MAT_NO = '" + txtCoilNO.Text +  "' WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                                DBHelper.ExecuteNonQuery(sql);
                            }

                            string sql1 = @"UPDATE UACS_OPEN_PACKING_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30,COIL_NO = '" + txtCoilNO.Text + "' WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                            DBHelper.ExecuteNonQuery(sql1);
                            if(coilDirection == 0 || coilDirection == 1)
                            {
                                string sql2 = @"UPDATE UACS_YARDMAP_COIL SET COIL_OPEN_DIRECTION = " + coilDirection + " WHERE COIL_NO = '" + txtCoilNO.Text.Trim() + "'";
                                DBHelper.ExecuteNonQuery(sql2);
                            }                          
                            string sql3 = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = '" + txtCoilNO.Text + "'WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                            DBHelper.ExecuteNonQuery(sql3);

                            //申请钢卷信息
                            tagDP.ServiceName = "iplature";
                            tagDP.AutoRegist = true;
                            TagValues.Clear();
                            TagValues.Add("MatInfQuery", null);
                            tagDP.Attach(TagValues);

                            if (txtCoilNO.Text.Trim().Length == 11 || txtCoilNO.Text.Trim().Length == 12)
                            {
                                tagDP.SetData("MatInfQuery", txtCoilNO.Text.Trim());
                                MessageBox.Show("已申请！");
                            }
                            else
                            {
                                MessageBox.Show("请输入正确的钢卷号！");
                            }

                            MessageBox.Show("确认吊离成功！");
                            HMILogger.WriteLog("吊离", "鞍座号：" + txtSaddle.Text + "钢卷号：" + txtCoilNO.Text + "开卷方向：" + comboBox1.Text, LogLevel.Info, this.Text);
                            this.Close();
                        }
                    }                           
                }
                else if (areano.Contains("WT"))
                {
                    DialogResult ret = MessageBox.Show("请人工确认水箱水位，是否允许吊运？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (ret == DialogResult.Cancel)
                    {
                        return;
                    } 
                    int coilDirection = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                    int packFlag = Convert.ToInt32(comboBox2.SelectedValue.ToString());

                    string sqlText = @"select * from UACS_YARDMAP_STOCK_DEFINE where STOCK_NO = '" + txtSaddle.Text + "'";
                    using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            if (Convert.ToInt32(rdr["STOCK_STATUS"].ToString()) != 2 || Convert.ToInt32(rdr["LOCK_FLAG"].ToString()) != 0 || (rdr["MAT_NO"].ToString() == ""))
                            {
                                string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 2, LOCK_FLAG = 0 , MAT_NO = '" + txtCoilNO.Text + "' WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                                DBHelper.ExecuteNonQuery(sql);
                            }

                            string sql1 = @"UPDATE UACS_WATER_TANK_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30,COIL_NO = '" + txtCoilNO.Text + "' WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                            DBHelper.ExecuteNonQuery(sql1);
                            if (coilDirection == 0 || coilDirection == 1)
                            {
                                string sql2 = @"UPDATE UACS_YARDMAP_COIL SET COIL_OPEN_DIRECTION = " + coilDirection + " WHERE COIL_NO = '" + txtCoilNO.Text.Trim() + "'";
                                DBHelper.ExecuteNonQuery(sql2);
                            }
                            string sql3 = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = '" + txtCoilNO.Text + "'WHERE STOCK_NO = '" + txtSaddle.Text + "'";
                            DBHelper.ExecuteNonQuery(sql3);
                            MessageBox.Show("确认吊离成功！");
                            HMILogger.WriteLog("吊离", "鞍座号：" + txtSaddle.Text + "钢卷号：" + txtCoilNO.Text + "开卷方向：" + comboBox1.Text, LogLevel.Info, this.Text);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CoilAway_Load(object sender, EventArgs e)
        {            
            txtSaddle.Text = saddleno;
            txtCoilNO.Text = coilno;
            BindCoilDrection();
            BindPackFlag();
            if(areano.Contains("PC") || areano.Contains("WT"))
            {
                label3.Visible = true;
                comboBox1.Visible = true;
                label4.Visible = false;
                comboBox2.Visible = false;
            }
        }

        private void BindCoilDrection()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");

            DataRow dr = dt.NewRow();

            dr = dt.NewRow();
            dr["TypeValue"] = "2";
            dr["TypeName"] = "";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["TypeValue"] = "0";
            dr["TypeName"] = "上开卷";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["TypeValue"] = "1";
            dr["TypeName"] = "下开卷";
            dt.Rows.Add(dr);;
   
            //绑定列表下拉框数据
            this.comboBox1.DataSource = dt;
            this.comboBox1.DisplayMember = "TypeName";
            this.comboBox1.ValueMember = "TypeValue";
        }

        private void BindPackFlag()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");

            DataRow dr = dt.NewRow();
           
            dr = dt.NewRow();
            dr["TypeValue"] = "0";
            dr["TypeName"] = "未包装";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["TypeValue"] = "1";
            dr["TypeName"] = "包装";
            dt.Rows.Add(dr);

            
       
            //绑定列表下拉框数据
            this.comboBox2.DataSource = dt;
            this.comboBox2.DisplayMember = "TypeName";
            this.comboBox2.ValueMember = "TypeValue";
        }
    }
}
