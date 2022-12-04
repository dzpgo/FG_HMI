using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using ParkClassLibrary;
using Baosight.iSuperframe.TagService;

namespace UACSParking
{
    public partial class TagPTInEnd : Form
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        public string TAG_PARKING_NO = "";
        public bool CANCEL_FLAG = false;
        string flagRotate1 = "0";
        string flagRotate2 = "0";
        string flagRotate3 = "0";
        string flagRotate4 = "0";
        string flagRotate5 = "0";
        string flagRotate6 = "0";
        string flagRotate7 = "0";
        string flagRotate8 = "0";
        string isunload1 = "0";
        string isunload2 = "0";
        string isunload3 = "0";
        string isunload4 = "0";
        string isunload5 = "0";
        string isunload6 = "0";
        string isunload7 = "0";
        string isunload8 = "0";

        int CoilNum = 0;
        public TagPTInEnd()
        {
            InitializeComponent();
        }

        public TagPTInEnd(string ParkingNo)
        {
            InitializeComponent();
            txt_ParkingNO.Text = ParkingNo;
            txt_ParkingNO.Enabled = false;
            TagPTInEnd_Load();
        }

        #region 页面加载
        private void TagPTInEnd_Load()
        {
            try
            {
                //设置背景色
                this.panel1.BackColor = Color.FromArgb(242, 246, 252);
                try
                {
                    string sql = @"SELECT TREATMENT_NO, STOWAGE_ID, CAR_NO, PT_ACTION_COUNT FROM UACS_PARKING_STATUS WHERE PARKING_NO = '" + txt_ParkingNO.Text.Trim() + "'";
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        while (rdr.Read())
                        {
                            if (rdr["TREATMENT_NO"] != System.DBNull.Value)
                            {
                                txt_PROCESS_NO.Text = Convert.ToString(rdr["TREATMENT_NO"]);
                            }
                            if (rdr["STOWAGE_ID"] != System.DBNull.Value)
                            {
                                txt_STOWAGE_ID.Text = Convert.ToString(rdr["STOWAGE_ID"]);
                            }

                            int ptCount = ManagerHelper.JudgeIntNull(rdr["PT_ACTION_COUNT"]);
                            txt_PT_ACTION_COUNT.Text = Convert.ToString(ptCount + 1);

                            string sqlUpdate = @"UPDATE UACS_PARKING_STATUS SET PT_ACTION_COUNT=" + txt_PT_ACTION_COUNT.Text.Trim() + " WHERE PARKING_NO = '" + txt_ParkingNO.Text.Trim() + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlUpdate);

                            if (rdr["CAR_NO"] != System.DBNull.Value)
                            {
                                txt_COIL_POS_7_1.Text = Convert.ToString(rdr["CAR_NO"]) + "011";
                                txt_COIL_POS_7_2.Text = Convert.ToString(rdr["CAR_NO"]) + "021";
                                txt_COIL_POS_7_3.Text = Convert.ToString(rdr["CAR_NO"]) + "031";
                                txt_COIL_POS_7_4.Text = Convert.ToString(rdr["CAR_NO"]) + "041";
                                txt_COIL_POS_7_5.Text = Convert.ToString(rdr["CAR_NO"]) + "051";
                                txt_COIL_POS_7_6.Text = Convert.ToString(rdr["CAR_NO"]) + "061";
                                txt_COIL_POS_7_7.Text = Convert.ToString(rdr["CAR_NO"]) + "071";
                                txt_COIL_POS_7_8.Text = Convert.ToString(rdr["CAR_NO"]) + "081";
                            }
                            else
                            {
                                MessageBox.Show("车号不明！");
                                return;
                            }

                        }
                    }
                    txt_PROCESS_NO.Enabled = false;
                    txt_STOWAGE_ID.Enabled = false;
                    txt_PT_ACTION_COUNT.Enabled = false;
                    txt_COIL_POS_7_1.Enabled = false;
                    txt_COIL_POS_7_2.Enabled = false;
                    txt_COIL_POS_7_3.Enabled = false;
                    txt_COIL_POS_7_4.Enabled = false;
                    txt_COIL_POS_7_5.Enabled = false;
                    txt_COIL_POS_7_6.Enabled = false;
                    txt_COIL_POS_7_7.Enabled = false;
                    txt_COIL_POS_7_8.Enabled = false;
                    GetComboxOnCoilNo(txt_COIL_NO_1,1);
                    GetComboxOnCoilNo(txt_COIL_NO_2,2);
                    GetComboxOnCoilNo(txt_COIL_NO_3,3);
                    GetComboxOnCoilNo(txt_COIL_NO_4,4);
                    GetComboxOnCoilNo(txt_COIL_NO_5,5);
                    GetComboxOnCoilNo(txt_COIL_NO_6,6);
                    GetComboxOnCoilNo(txt_COIL_NO_7,7);
                    GetComboxOnCoilNo(txt_COIL_NO_8,8);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message + "\r\n" + er.StackTrace);
                }



            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        #endregion

        #region 确定
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                tagDP.ServiceName = "iplature";
                tagDP.AutoRegist = true;
                TagValues.Clear();
                TagValues.Add("MatInfQuery", null);
                tagDP.Attach(TagValues);

                TAG_PARKING_NO = txt_ParkingNO.Text.ToString().Trim();
                int indexY = 1;

                List<string> list = new List<string>();
                foreach (Control cm in panel1.Controls)
                {
                    if (cm.GetType() == typeof(ComboBox))
                    {
                        string t = (cm as ComboBox).Text;
                        if(t != "")
                        {
                            list.Add(t);
                        }
                        
                    }
                }                                  
                if (list.Distinct().Count<string>() != list.Count)
                {
                    MessageBox.Show("请勿重复选择钢卷！");
                    return;
                }

                if (checkBox1.Checked)
                    flagRotate1 = "1";
                if (checkBox2.Checked)
                    flagRotate2 = "1";
                if (checkBox3.Checked)
                    flagRotate3 = "1";
                if (checkBox4.Checked)
                    flagRotate4 = "1";
                if (checkBox5.Checked)
                    flagRotate5 = "1";
                if (checkBox6.Checked)
                    flagRotate6 = "1";
                if (checkBox7.Checked)
                    flagRotate7 = "1";
                if (checkBox8.Checked)
                    flagRotate8 = "1";

                if (checkBox_Unload1.Checked)
                    isunload1 = "1";
                if (checkBox_Unload2.Checked)
                    isunload2 = "1";
                if (checkBox_Unload3.Checked)
                    isunload3 = "1";
                if (checkBox_Unload4.Checked)
                    isunload4 = "1";
                if (checkBox_Unload5.Checked)
                    isunload5 = "1";
                if (checkBox_Unload6.Checked)
                    isunload6 = "1";
                if (checkBox_Unload7.Checked)
                    isunload7 = "1";
                if (checkBox_Unload8.Checked)
                    isunload8 = "1";

                if (txt_COIL_NO_1.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_1.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate1 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_1.Text.Trim() + "',1,'M'," + indexY.ToString() + "," + isunload1 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_1.Text.Trim());
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;
                }
                if (txt_COIL_NO_2.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_2.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate2 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_2.Text.Trim() + "',2,'M'," + indexY.ToString() + "," + isunload2 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_2.Text.Trim());
                 
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;

                }
                if (txt_COIL_NO_3.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_3.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate3 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_3.Text.Trim() + "',3,'M'," + indexY.ToString() + "," + isunload3 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_3.Text.Trim());                   
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;

                }
                if (txt_COIL_NO_4.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_4.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate4 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_4.Text.Trim() + "',4,'M'," + indexY.ToString() + "," + isunload4 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_4.Text.Trim());
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;

                }
                if (txt_COIL_NO_5.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_5.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate5 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_5.Text.Trim() + "',5,'M'," + indexY.ToString() + "," + isunload5 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_5.Text.Trim());
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;

                }
                if (txt_COIL_NO_6.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_6.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate6 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_6.Text.Trim() + "',6,'M'," + indexY.ToString() + "," + isunload6 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_6.Text.Trim());
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;

                }
                if (txt_COIL_NO_7.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_7.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate7 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_7.Text.Trim() + "',7,'M'," + indexY.ToString() + "," + isunload7 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_7.Text.Trim());
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;

                }
                if (txt_COIL_NO_8.Text.Trim() != "")
                {
                    string sql = @"INSERT INTO UACS_PDA_SCAN(MAT_NO,STOWAGE_ID,EQU_NO,USERNAME,REC_TIME,MAT_DIRECTION,PROCESS_NO,PT_ACTION_COUNT,COIL_POSITION_7,GROOVEID,POS_IN_GROOVE,INDEX_Y,ISUNLOAD) VALUES('" + txt_COIL_NO_8.Text.Trim() + "'," + txt_STOWAGE_ID.Text.Trim() + ",'HMI','HMI',CURRENT TIMESTAMP,'" + flagRotate8 + "','" + txt_PROCESS_NO.Text.Trim() + "'," + txt_PT_ACTION_COUNT.Text.Trim() + ",'" + txt_COIL_POS_7_8.Text.Trim() + "',8,'M'," + indexY.ToString() + "," + isunload8 + ")";
                    tagDP.SetData("MatInfQuery", txt_COIL_NO_8.Text.Trim());
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (rdr.RecordsAffected <= 0)
                        {
                            MessageBox.Show("手持机扫描数据录入失败！");
                            CANCEL_FLAG = true;
                            return;
                        }
                    }
                    indexY++;

                }
                CANCEL_FLAG = false;
                this.Close();
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
        }
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CANCEL_FLAG = true;
                this.Close();
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
        }
        #endregion

        private void GetComboxOnCoilNo(ComboBox comBox,int index)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            try
            {
                string sql = @"SELECT COUNT(*) as num FROM UACS_TRUCK_STOWAGE_DETAIL WHERE STOWAGE_ID = '" + txt_STOWAGE_ID.Text + "'";
                using (IDataReader rd = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rd.Read())
                    {
                        DataRow dr = dt.NewRow();

                        if (rd["num"] != DBNull.Value)
                            CoilNum = Convert.ToInt32(rd["num"]);
                    }
                }

                string sqlText = @"SELECT DISTINCT MAT_NO as TypeValue,MAT_NO as TypeName,POS_ON_FRAME FROM UACS_TRUCK_STOWAGE_DETAIL WHERE STOWAGE_ID = '" + txt_STOWAGE_ID.Text + "' ORDER BY POS_ON_FRAME ASC";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        if (rdr["TypeName"].ToString().Trim() != "")
                        {
                            dr["TypeValue"] = rdr["TypeValue"];
                            dr["TypeName"] = rdr["TypeName"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                //绑定列表下拉框数据
                DataRow dr0 = dt.NewRow();
                dr0["TypeValue"] = "";
                dr0["TypeName"] = "";
                dt.Rows.InsertAt(dr0, 0);
                comBox.DataSource = dt;
                comBox.DisplayMember = "TypeName";
                comBox.ValueMember = "TypeValue";
                comBox.SelectedItem = 0;
                //if (index <= CoilNum)
                //{
                //    comBox.SelectedIndex = index;
                //}
                //else
                //{
                //    comBox.SelectedIndex = -1;
                //}

                //comBox.Text = "请选择";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }
    }
}
