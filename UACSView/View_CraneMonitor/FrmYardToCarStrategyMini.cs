using ParkClassLibrary;
using System;
using System.Data;
using System.Windows.Forms;
using UACSDAL;

namespace UACSView.View_CraneMonitor
{
    public partial class FrmYardToCarStrategyMini : Form
    {
        #region 全局变量
        DataTable dtSource = new DataTable();
        //防止弹出信息关闭画面
        bool isPopupMessage = false;
        #endregion
        #region 初始化
        public FrmYardToCarStrategyMini()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmYardToCarStrategyMini_Load(object sender, EventArgs e)
        {
            GetToCarStrategy();
        }
        /// <summary>
        /// 获取多车协同作业配置信息
        /// </summary>
        private void GetToCarStrategy()
        {
            try
            {
                dtSource.Clear();
                var sqlText = @"SELECT ID, CRANE_NO, PARKING_NO, GRID_START, GRID_END, FLAG_MY_DYUTY, FLAG_ENABLED, SEQ, X_DIR, Y_DIR, FLAG_DIFFENT FROM UACSAPP.UACS_ORDER_YARD_TO_CAR_STRATEGY ORDER BY ID ASC ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    dtSource.Load(rdr);
                }
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        var craneNo = dr["CRANE_NO"].ToString();
                        var id = dr["id"].ToString();
                        var flagEnabled = dr["FLAG_ENABLED"].ToString();
                        var flagDiffent = dr["FLAG_DIFFENT"].ToString();
                        var parkingNo = dr["PARKING_NO"].ToString();
                        if (craneNo.Equals("1"))
                        {
                            if (id.Equals("1"))
                            {
                                sb_A1_1.Checked = flagEnabled.Equals("1") ? true : false;                                
                            }
                            if (id.Equals("2"))
                            {
                                sb_A2_1.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("3"))
                            {
                                sb_A3_1.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("4"))
                            {
                                sb_A4_1.Checked = flagEnabled.Equals("1") ? true : false;
                            }                            
                        }
                        if (craneNo.Equals("2"))
                        {
                            if (id.Equals("5"))
                            {
                                sb_A2_2.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("6"))
                            {
                                sb_A1_2.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("7"))
                            {
                                sb_A3_2.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("8"))
                            {
                                sb_A4_2.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                        }
                        if (craneNo.Equals("3"))
                        {
                            if (id.Equals("9"))
                            {
                                sb_A3_3.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("10"))
                            {
                                sb_A2_3.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("11"))
                            {
                                sb_A4_3.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("12"))
                            {
                                sb_A1_3.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                        }
                        if (craneNo.Equals("4"))
                        {
                            if (id.Equals("13"))
                            {
                                sb_A4_4.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("14"))
                            {
                                sb_A3_4.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("15"))
                            {
                                sb_A2_4.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                            if (id.Equals("16"))
                            {
                                sb_A1_4.Checked = flagEnabled.Equals("1") ? true : false;
                            }
                        }

                        if (parkingNo.Equals("A1"))
                        {
                            sb_FlagDiffent_A1.Checked = flagDiffent.Equals("1") ? true : false;
                        }
                        else if (parkingNo.Equals("A2"))
                        {
                            sb_FlagDiffent_A2.Checked = flagDiffent.Equals("1") ? true : false;
                        }
                        else if (parkingNo.Equals("A3"))
                        {
                            sb_FlagDiffent_A3.Checked = flagDiffent.Equals("1") ? true : false;
                        }
                        else if (parkingNo.Equals("A4"))
                        {
                            sb_FlagDiffent_A4.Checked = flagDiffent.Equals("1") ? true : false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region 按钮
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Refresh_Click(object sender, EventArgs e)
        {
            GetToCarStrategy();
            HMILogger.WriteLog("多车协同", "多车协同，刷新", LogLevel.Info, this.Text);
        }
        /// <summary>
        /// 恢复默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_RestoreDefault_Click(object sender, EventArgs e)
        {
            try
            {
                isPopupMessage = true;
                DialogResult dr = MessageBox.Show("请确认是否恢复默认设置？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.OK)
                {
                    var sqlText = @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 1 WHERE ID = 1;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 2;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 3;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 4;

                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 1 WHERE ID = 5;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 6;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 7;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 8;

                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 1 WHERE ID = 9;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 10;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 11;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 12;

                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 1 WHERE ID = 13;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 14;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 15;
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = 0 WHERE ID = 16;

                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_DIFFENT = 0 WHERE PARKING_NO = 'A1';
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_DIFFENT = 0 WHERE PARKING_NO = 'A2';
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_DIFFENT = 0 WHERE PARKING_NO = 'A3';
                            UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_DIFFENT = 0 WHERE PARKING_NO = 'A4';";
                    if (!string.IsNullOrEmpty(sqlText))
                    {
                        DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                        HMILogger.WriteLog("多车协同", "多车协同，恢复默认设置", LogLevel.Info, this.Text);
                        MessageBox.Show("保存成功!");
                    }
                    else
                    {
                        MessageBox.Show("没有数据更新!");
                    }
                    //刷新
                    GetToCarStrategy();
                }
                isPopupMessage = false;
            }
            catch (Exception ex)
            { }
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
        #endregion

        #region 更新
        private void UpdataList(string craneNo, bool A1, bool A2, bool A3, bool A4, int id1, int id2, int id3, int id4)
        {
            var sqlText = @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = " + (A1 ? 1 : 0) + " WHERE ID = " + id1 + " AND CRANE_NO = '" + craneNo + "';";
            sqlText = sqlText + "UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = " + (A2 ? 1 : 0) + " WHERE ID = " + id2 + " AND CRANE_NO = '" + craneNo + "';";
            sqlText = sqlText + "UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = " + (A3 ? 1 : 0) + " WHERE ID = " + id3 + " AND CRANE_NO = '" + craneNo + "';";
            sqlText = sqlText + "UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_ENABLED = " + (A4 ? 1 : 0) + " WHERE ID = " + id4 + " AND CRANE_NO = '" + craneNo + "';";
            if (!string.IsNullOrEmpty(sqlText))
            {
                DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                HMILogger.WriteLog("多车协同", "多车协同, 1号行车, A1:" + (A1 ? "开启" : "关闭") + " A2:" + (A2 ? "开启" : "关闭") + " A3:" + (A3 ? "开启" : "关闭") + " A4:" + (A4 ? "开启" : "关闭") + " ", LogLevel.Info, this.Text);
                //刷新
                GetToCarStrategy();
            }
            else
            {
                MessageBox.Show("没有数据更新!");
            }
        }
        #endregion

        #region 1#号车
        private void sb_A1_1_Click(object sender, EventArgs e)
        {
            UpdataList("1", sb_A1_1.Checked, sb_A2_1.Checked, sb_A3_1.Checked, sb_A4_1.Checked, 1, 2, 3, 4);
        }

        private void sb_A2_1_Click(object sender, EventArgs e)
        {
            UpdataList("1", sb_A1_1.Checked, sb_A2_1.Checked, sb_A3_1.Checked, sb_A4_1.Checked, 1, 2, 3, 4);
        }

        private void sb_A3_1_Click(object sender, EventArgs e)
        {
            sb_A3_1.Checked = false;
            MessageBox.Show("开启失败，1#车无法帮助3#车作业!");
            //UpdataList("1", sb_A1_1.Checked, sb_A2_1.Checked, sb_A3_1.Checked, sb_A4_1.Checked, 1, 2, 3, 4);
        }

        private void sb_A4_1_Click(object sender, EventArgs e)
        {
            sb_A4_1.Checked = false;
            MessageBox.Show("开启失败，1#车无法帮助4#车作业!");
            //UpdataList("1", sb_A1_1.Checked, sb_A2_1.Checked, sb_A3_1.Checked, sb_A4_1.Checked, 1, 2, 3, 4);
        } 
        #endregion

        #region 2#号车
        private void sb_A1_2_Click(object sender, EventArgs e)
        {
            UpdataList("2", sb_A1_2.Checked, sb_A2_2.Checked, sb_A3_2.Checked, sb_A4_2.Checked, 6, 5, 7, 8);
        }

        private void sb_A2_2_Click(object sender, EventArgs e)
        {
            UpdataList("2", sb_A1_2.Checked, sb_A2_2.Checked, sb_A3_2.Checked, sb_A4_2.Checked, 6, 5, 7, 8);
        }

        private void sb_A3_2_Click(object sender, EventArgs e)
        {
            UpdataList("2", sb_A1_2.Checked, sb_A2_2.Checked, sb_A3_2.Checked, sb_A4_2.Checked, 6, 5, 7, 8);
        }

        private void sb_A4_2_Click(object sender, EventArgs e)
        {
            sb_A4_2.Checked = false;
            MessageBox.Show("开启失败，2#车无法帮助4#车作业!");
            //UpdataList("2", sb_A1_2.Checked, sb_A2_2.Checked, sb_A3_2.Checked, sb_A4_2.Checked, 6, 5, 7, 8);
        } 
        #endregion

        #region 3#号车
        private void sb_A1_3_Click(object sender, EventArgs e)
        {
            sb_A1_3.Checked = false;
            MessageBox.Show("开启失败，3#车无法帮助1#车作业!");
            //UpdataList("3", sb_A1_3.Checked, sb_A2_3.Checked, sb_A3_3.Checked, sb_A4_3.Checked, 12, 10, 9, 11);
        }

        private void sb_A2_3_Click(object sender, EventArgs e)
        {
            UpdataList("3", sb_A1_3.Checked, sb_A2_3.Checked, sb_A3_3.Checked, sb_A4_3.Checked, 12, 10, 9, 11);
        }

        private void sb_A3_3_Click(object sender, EventArgs e)
        {
            UpdataList("3", sb_A1_3.Checked, sb_A2_3.Checked, sb_A3_3.Checked, sb_A4_3.Checked, 12, 10, 9, 11);
        }

        private void sb_A4_3_Click(object sender, EventArgs e)
        {
            UpdataList("3", sb_A1_3.Checked, sb_A2_3.Checked, sb_A3_3.Checked, sb_A4_3.Checked, 12, 10, 9, 11);
        } 
        #endregion

        #region 4#号车
        private void sb_A1_4_Click(object sender, EventArgs e)
        {
            sb_A1_4.Checked = false;
            MessageBox.Show("开启失败，4#车无法帮助1#车作业!");
            //UpdataList("4", sb_A1_4.Checked, sb_A2_4.Checked, sb_A3_4.Checked, sb_A4_4.Checked, 16, 15, 14, 13);
        }

        private void sb_A2_4_Click(object sender, EventArgs e)
        {
            sb_A2_4.Checked = false;
            MessageBox.Show("开启失败，4#车无法帮助2#车作业!");
            //UpdataList("4", sb_A1_4.Checked, sb_A2_4.Checked, sb_A3_4.Checked, sb_A4_4.Checked, 16, 15, 14, 13);
        }

        private void sb_A3_4_Click(object sender, EventArgs e)
        {
            UpdataList("4", sb_A1_4.Checked, sb_A2_4.Checked, sb_A3_4.Checked, sb_A4_4.Checked, 16, 15, 14, 13);
        }

        private void sb_A4_4_Click(object sender, EventArgs e)
        {
            UpdataList("4", sb_A1_4.Checked, sb_A2_4.Checked, sb_A3_4.Checked, sb_A4_4.Checked, 16, 15, 14, 13);
        }
        #endregion

        #region 装不同料
        private void sb_FlagDiffent_A1_Click(object sender, EventArgs e)
        {
            UpdataFlagDiffent("A1", sb_FlagDiffent_A1.Checked);
        }

        private void sb_FlagDiffent_A2_Click(object sender, EventArgs e)
        {
            UpdataFlagDiffent("A2", sb_FlagDiffent_A2.Checked);
        }

        private void sb_FlagDiffent_A3_Click(object sender, EventArgs e)
        {
            UpdataFlagDiffent("A3", sb_FlagDiffent_A3.Checked);
        }

        private void sb_FlagDiffent_A4_Click(object sender, EventArgs e)
        {
            UpdataFlagDiffent("A4", sb_FlagDiffent_A4.Checked);
        }

        /// <summary>
        /// 装不同料
        /// </summary>
        /// <param name="parkingNo">工位</param>
        /// <param name="flagDiffent">装不同料标志 1：启用 0：关闭</param>
        private void UpdataFlagDiffent(string parkingNo, bool flagDiffent)
        {
            try
            {
                var sqlText = @"UPDATE UACS_ORDER_YARD_TO_CAR_STRATEGY SET FLAG_DIFFENT = " + (flagDiffent ? 1 : 0) + " WHERE PARKING_NO = '" + parkingNo + "';";
                if (!string.IsNullOrEmpty(sqlText))
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                    HMILogger.WriteLog("多车协同", "装不同料, 工位：" + parkingNo + " ，标志：" + (flagDiffent ? "启用" : "关闭"), LogLevel.Info, this.Text);
                    //刷新
                    GetToCarStrategy();
                }
                else
                {
                    MessageBox.Show("没有数据更新!");
                }
            }
            catch (Exception ex)
            {
                HMILogger.WriteLog("多车协同", "装不同料错误信息：" + ex.Message.ToString().Trim() + " ,工位：" + parkingNo + " ,标志：" + (flagDiffent ? "启用" : "关闭"), LogLevel.Info, this.Text);
            }
        } 
        #endregion
    }
}
