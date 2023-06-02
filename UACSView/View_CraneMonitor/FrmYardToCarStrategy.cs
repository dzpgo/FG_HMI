using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;
using ParkClassLibrary;

namespace UACSView.View_CraneMonitor
{
    public partial class FrmYardToCarStrategy : FormBase
    {
        #region 全局变量
        DataTable dtSource = new DataTable(); 
        #endregion
        #region 初始化
        public FrmYardToCarStrategy()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmYardToCarStrategy_Load(object sender, EventArgs e)
        {
            GetDataLoad();
        } 
        #endregion
        #region 数据加载
        private void GetDataLoad()
        {
            dtSource.Clear();
            var sqlText = @"SELECT ID,CRANE_NO,PARKING_NO,GRID_START,GRID_END,FLAG_MY_DYUTY,FLAG_ENABLED,SEQ,X_DIR,Y_DIR FROM UACS_ORDER_YARD_TO_CAR_STRATEGY ORDER BY ID ASC ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dtSource.Load(rdr);
            }
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSource.Rows)
                {
                    if (dr["CRANE_NO"].ToString().Equals("1"))
                    {
                        if (dr["ID"].ToString().Equals("1"))
                        {
                            txt_CraneNo_1_1.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_1_1.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_1_1.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_1_1.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_1_1.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_1_1, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("2"))
                        {
                            txt_CraneNo_1_2.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_1_2.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_1_2.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_1_2.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_1_2.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_1_2, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("3"))
                        {
                            txt_CraneNo_1_3.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_1_3.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_1_3.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_1_3.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_1_3.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_1_3, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("4"))
                        {
                            txt_CraneNo_1_4.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_1_4.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_1_4.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_1_4.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_1_4.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_1_4, dr["FLAG_ENABLED"].ToString());
                        }
                    }
                    if (dr["CRANE_NO"].ToString().Equals("2"))
                    {
                        if (dr["ID"].ToString().Equals("5"))
                        {
                            txt_CraneNo_2_1.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_2_1.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_2_1.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_2_1.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_2_1.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_2_1, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("6"))
                        {
                            txt_CraneNo_2_2.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_2_2.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_2_2.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_2_2.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_2_2.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_2_2, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("7"))
                        {
                            txt_CraneNo_2_3.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_2_3.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_2_3.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_2_3.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_2_3.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_2_3, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("8"))
                        {
                            txt_CraneNo_2_4.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_2_4.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_2_4.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_2_4.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_2_4.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_2_4, dr["FLAG_ENABLED"].ToString());
                        }
                    }
                    if (dr["CRANE_NO"].ToString().Equals("3"))
                    {
                        if (dr["ID"].ToString().Equals("9"))
                        {
                            txt_CraneNo_3_1.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_3_1.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_3_1.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_3_1.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_3_1.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_3_1, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("10"))
                        {
                            txt_CraneNo_3_2.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_3_2.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_3_2.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_3_2.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_3_2.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_3_2, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("11"))
                        {
                            txt_CraneNo_3_3.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_3_3.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_3_3.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_3_3.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_3_3.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_3_3, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("12"))
                        {
                            txt_CraneNo_3_4.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_3_4.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_3_4.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_3_4.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_3_4.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_3_4, dr["FLAG_ENABLED"].ToString());
                        }
                    }
                    if (dr["CRANE_NO"].ToString().Equals("4"))
                    {
                        if (dr["ID"].ToString().Equals("13"))
                        {
                            txt_CraneNo_4_1.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_4_1.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_4_1.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_4_1.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_4_1.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_4_1, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("14"))
                        {
                            txt_CraneNo_4_2.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_4_2.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_4_2.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_4_2.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_4_2.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_4_2, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("15"))
                        {
                            txt_CraneNo_4_3.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_4_3.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_4_3.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_4_3.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_4_3.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_4_3, dr["FLAG_ENABLED"].ToString());
                        }
                        if (dr["ID"].ToString().Equals("16"))
                        {
                            txt_CraneNo_4_4.Text = dr["CRANE_NO"].ToString();
                            txt_ParkingNo_4_4.Text = dr["PARKING_NO"].ToString();
                            txt_GridStart_4_4.Text = dr["GRID_START"].ToString();
                            txt_GridEnt_4_4.Text = dr["GRID_END"].ToString();
                            txt_FlagMyDyuty_4_4.Text = dr["FLAG_MY_DYUTY"].ToString().Equals("1") ? "本职工作" : dr["FLAG_MY_DYUTY"].ToString().Equals("0") ? "帮助作业" : "无";
                            BindStatus(cmb_FlagEnabled_4_4, dr["FLAG_ENABLED"].ToString());
                        }
                    }
                }
            }
        } 
        #endregion
        #region 绑定状态
        private void BindStatus(ComboBox cmbBox, string status)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            dr = dt.NewRow();
            dr["TypeValue"] = "0";
            dr["TypeName"] = "关闭";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "1";
            dr["TypeName"] = "启用";
            dt.Rows.Add(dr);
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.SelectedIndex = status.Equals("0") ? 0 : 1;
            if (status.Equals("1"))
            {
                cmbBox.BackColor = Color.LightGreen;
            }

        } 
        #endregion
        #region 刷新
        private void bt_Refresh_1_Click(object sender, EventArgs e)
        {
            GetDataLoad();
        }

        private void bt_Refresh_2_Click(object sender, EventArgs e)
        {
            GetDataLoad();
        }

        private void bt_Refresh_3_Click(object sender, EventArgs e)
        {
            GetDataLoad();
        }

        private void bt_Refresh_4_Click(object sender, EventArgs e)
        {
            GetDataLoad();
        }
        #endregion
        #region 保存
        private void btn_Confirm_1_Click(object sender, EventArgs e)
        {
            try
            {
                var sqlText = "";
                var Info = "";
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        if (dr["CRANE_NO"].ToString().Equals("1"))
                        {
                            if (dr["ID"].ToString().Equals("1"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_1_1.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_1_1.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_1_1.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("2"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_1_2.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_1_2.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_1_2.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("3"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_1_3.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_1_3.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_1_3.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("4"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_1_4.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_1_4.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_1_4.SelectedValue.ToString() + "  ";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sqlText))
                    {
                        DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                        HMILogger.WriteLog("多车协同", Info, LogLevel.Info, this.Text);
                        GetDataLoad();
                        MessageBox.Show("保存成功!");
                    }
                    else
                    {
                        MessageBox.Show("没有数据更新!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败!");
            }
        }

        private void btn_Confirm_2_Click(object sender, EventArgs e)
        {
            try
            {
                var sqlText = "";
                var Info = "";
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        if (dr["CRANE_NO"].ToString().Equals("2"))
                        {
                            if (dr["ID"].ToString().Equals("5"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_2_1.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_2_1.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_2_1.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("6"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_2_2.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_2_2.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_2_2.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("7"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_2_3.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_2_3.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_2_3.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("8"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_2_4.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_2_4.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_2_4.SelectedValue.ToString() + "  ";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sqlText))
                    {
                        DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                        HMILogger.WriteLog("多车协同", Info, LogLevel.Info, this.Text);
                        GetDataLoad();
                        MessageBox.Show("保存成功!");
                    }
                    else
                    {
                        MessageBox.Show("没有数据更新!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败!");
            }
        }

        private void btn_Confirm_3_Click(object sender, EventArgs e)
        {
            try
            {
                var sqlText = "";
                var Info = "";
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        if (dr["CRANE_NO"].ToString().Equals("3"))
                        {
                            if (dr["ID"].ToString().Equals("9"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_3_1.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_3_1.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_3_1.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("10"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_3_2.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_3_2.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_3_2.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("11"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_3_3.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_3_3.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_3_3.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("12"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_3_4.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_3_4.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_3_4.SelectedValue.ToString() + "  ";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sqlText))
                    {
                        DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                        HMILogger.WriteLog("多车协同", Info, LogLevel.Info, this.Text);
                        GetDataLoad();
                        MessageBox.Show("保存成功!");
                    }
                    else
                    {
                        MessageBox.Show("没有数据更新!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败!");
            }
        }

        private void btn_Confirm_4_Click(object sender, EventArgs e)
        {
            try
            {
                var sqlText = "";
                var Info = "";
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        if (dr["CRANE_NO"].ToString().Equals("4"))
                        {
                            if (dr["ID"].ToString().Equals("13"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_4_1.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_4_1.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_4_1.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("14"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_4_2.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_4_2.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_4_2.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("15"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_4_3.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_4_3.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_4_3.SelectedValue.ToString() + "  ";
                                }
                            }
                            if (dr["ID"].ToString().Equals("16"))
                            {
                                if (!dr["FLAG_ENABLED"].ToString().Equals(cmb_FlagEnabled_4_4.SelectedValue.ToString()))
                                {
                                    sqlText = sqlText + @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = '" + cmb_FlagEnabled_4_4.SelectedValue.ToString() + "' WHERE ID = '" + dr["ID"].ToString() + "' AND CRANE_NO = '" + dr["CRANE_NO"].ToString() + "';";
                                    Info = Info + dr["CRANE_NO"].ToString() + "#行车,料格状态：" + cmb_FlagEnabled_4_4.SelectedValue.ToString() + "  ";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sqlText))
                    {
                        DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                        HMILogger.WriteLog("多车协同", Info, LogLevel.Info, this.Text);
                        GetDataLoad();
                        MessageBox.Show("保存成功!");
                    }
                    else
                    {
                        MessageBox.Show("没有数据更新!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败!");
            }
        }
        #endregion
        #region 值发生改变时触发
        private void cmb_FlagEnabled_1_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_1_1.Items.Count > 0 && cmb_FlagEnabled_1_1.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_1_1.BackColor = Color.LightGreen;
                cmb_FlagEnabled_1_2.SelectedValue = "0";
                cmb_FlagEnabled_1_3.SelectedValue = "0";
                cmb_FlagEnabled_1_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_1_1.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_1_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_1_2.Items.Count > 0 && cmb_FlagEnabled_1_2.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_1_2.BackColor = Color.LightGreen;
                cmb_FlagEnabled_1_1.SelectedValue = "0";
                cmb_FlagEnabled_1_3.SelectedValue = "0";
                cmb_FlagEnabled_1_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_1_2.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_1_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_1_3.Items.Count > 0 && cmb_FlagEnabled_1_3.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_1_3.BackColor = Color.LightGreen;
                cmb_FlagEnabled_1_1.SelectedValue = "0";
                cmb_FlagEnabled_1_2.SelectedValue = "0";
                cmb_FlagEnabled_1_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_1_3.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_1_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_1_4.Items.Count > 0 && cmb_FlagEnabled_1_4.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_1_4.BackColor = Color.LightGreen;
                cmb_FlagEnabled_1_1.SelectedValue = "0";
                cmb_FlagEnabled_1_2.SelectedValue = "0";
                cmb_FlagEnabled_1_3.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_1_4.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_2_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_2_1.Items.Count > 0 && cmb_FlagEnabled_2_1.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_2_1.BackColor = Color.LightGreen;
                cmb_FlagEnabled_2_2.SelectedValue = "0";
                cmb_FlagEnabled_2_3.SelectedValue = "0";
                cmb_FlagEnabled_2_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_2_1.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_2_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_2_2.Items.Count > 0 && cmb_FlagEnabled_2_2.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_2_2.BackColor = Color.LightGreen;
                cmb_FlagEnabled_2_1.SelectedValue = "0";
                cmb_FlagEnabled_2_3.SelectedValue = "0";
                cmb_FlagEnabled_2_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_2_2.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_2_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_2_3.Items.Count > 0 && cmb_FlagEnabled_2_3.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_2_3.BackColor = Color.LightGreen;
                cmb_FlagEnabled_2_1.SelectedValue = "0";
                cmb_FlagEnabled_2_2.SelectedValue = "0";
                cmb_FlagEnabled_2_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_2_3.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_2_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_2_4.Items.Count > 0 && cmb_FlagEnabled_2_4.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_2_4.BackColor = Color.LightGreen;
                cmb_FlagEnabled_2_1.SelectedValue = "0";
                cmb_FlagEnabled_2_2.SelectedValue = "0";
                cmb_FlagEnabled_2_3.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_2_4.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_3_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_3_1.Items.Count > 0 && cmb_FlagEnabled_3_1.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_3_1.BackColor = Color.LightGreen;
                cmb_FlagEnabled_3_2.SelectedValue = "0";
                cmb_FlagEnabled_3_3.SelectedValue = "0";
                cmb_FlagEnabled_3_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_3_1.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_3_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_3_2.Items.Count > 0 && cmb_FlagEnabled_3_2.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_3_2.BackColor = Color.LightGreen;
                cmb_FlagEnabled_3_1.SelectedValue = "0";
                cmb_FlagEnabled_3_3.SelectedValue = "0";
                cmb_FlagEnabled_3_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_3_2.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_3_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_3_3.Items.Count > 0 && cmb_FlagEnabled_3_3.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_3_3.BackColor = Color.LightGreen;
                cmb_FlagEnabled_3_1.SelectedValue = "0";
                cmb_FlagEnabled_3_2.SelectedValue = "0";
                cmb_FlagEnabled_3_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_3_3.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_3_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_3_4.Items.Count > 0 && cmb_FlagEnabled_3_4.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_3_4.BackColor = Color.LightGreen;
                cmb_FlagEnabled_3_1.SelectedValue = "0";
                cmb_FlagEnabled_3_2.SelectedValue = "0";
                cmb_FlagEnabled_3_3.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_3_4.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_4_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_4_1.Items.Count > 0 && cmb_FlagEnabled_4_1.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_4_1.BackColor = Color.LightGreen;
                cmb_FlagEnabled_4_2.SelectedValue = "0";
                cmb_FlagEnabled_4_3.SelectedValue = "0";
                cmb_FlagEnabled_4_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_4_1.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_4_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_4_2.Items.Count > 0 && cmb_FlagEnabled_4_2.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_4_2.BackColor = Color.LightGreen;
                cmb_FlagEnabled_4_1.SelectedValue = "0";
                cmb_FlagEnabled_4_3.SelectedValue = "0";
                cmb_FlagEnabled_4_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_4_2.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_4_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_4_3.Items.Count > 0 && cmb_FlagEnabled_4_3.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_4_3.BackColor = Color.LightGreen;
                cmb_FlagEnabled_4_1.SelectedValue = "0";
                cmb_FlagEnabled_4_2.SelectedValue = "0";
                cmb_FlagEnabled_4_4.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_4_3.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void cmb_FlagEnabled_4_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_FlagEnabled_4_4.Items.Count > 0 && cmb_FlagEnabled_4_4.SelectedValue.ToString().Equals("1"))
            {
                cmb_FlagEnabled_4_4.BackColor = Color.LightGreen;
                cmb_FlagEnabled_4_1.SelectedValue = "0";
                cmb_FlagEnabled_4_2.SelectedValue = "0";
                cmb_FlagEnabled_4_3.SelectedValue = "0";
            }
            else
            {
                cmb_FlagEnabled_4_4.BackColor = System.Drawing.SystemColors.Window;
            }
        } 
        #endregion
    }
}
