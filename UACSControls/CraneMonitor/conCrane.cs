using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using UACSPopupForm;
using ParkClassLibrary;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using Baosight.iSuperframe.TagService;
using Microsoft.VisualBasic;
using ParkClassLibrary;
using UACS;

namespace UACSControls
{
    /// <summary>
    /// 行车操作
    /// </summary>
    public partial class conCrane : UserControl
    {
       // private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = null;
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth = null;
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public conCrane()
        {

            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲   
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN 
                return parms;
            }
        }    
        private  Label lblCraneNo = new Label();
        private bool isCraneLbl = false;
        private CraneStatusBase cranePLCStatusBase = new CraneStatusBase();

        //step1
        public void InitTagDataProvide(string tagServiceName)
        {
            try
            {
                tagDataProvider.ServiceName = tagServiceName;
            }
            catch (Exception ex)
            {
            }
        }


        private string craneNO = string.Empty;
        //step2
        public string CraneNO
        {
            get { return craneNO; }
            set
            {
                craneNO = value;
                this.ContextMenuStrip = contextMenuStrip1;
            }
        }

        //step3
        public delegate void RefreshControlInvoke(CraneStatusBase _cranePLCStatusBase, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, long craneWith, Panel panel);

        public void RefreshControl(CraneStatusBase _cranePLCStatusBase, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown,long craneWith,Panel panel)
        {
            try
            {
                auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
                getUserName();
                cranePLCStatusBase = _cranePLCStatusBase;

                ////行车控制模式
                ////自动模式
                //if (_cranePLCStatusBase.ControlMode == 4)
                //{
                //    this.panelCrane.BackgroundImage = global::UACSControls.Resource1.行车_Avoid;
                //}
                ////遥控模式
                //else if (_cranePLCStatusBase.ControlMode == 1)
                //{
                //    this.panelCrane.BackgroundImage = global::UACSControls.Resource1.行车_Run;
                //}
                ////人工模式
                //else if (_cranePLCStatusBase.ControlMode == 2)
                //{
                //    this.panelCrane.BackgroundImage = global::UACSControls.Resource1.行车_Avoid1;
                //}

                //计算X方向上的比例关系
                double xScale = Convert.ToDouble(panelWidth) / Convert.ToDouble(baySpaceX);

                //计算控件行车中心X，区分为X坐标轴向左或者向右
                double X = 0;
                double location_Crane_X = 0;
                double location_Crab_X = 0;
                if (xAxisRight == true)
                {
                    X = Convert.ToDouble(_cranePLCStatusBase.XAct) * xScale;
                    location_Crane_X = Convert.ToDouble(_cranePLCStatusBase.XAct - craneWith / 2) * xScale;
                    location_Crab_X = 0;//在行车panel内，所以永远为0
                }
                else
                {
                    //X = (Convert.ToDouble(baySpaceX) - Convert.ToDouble(_cranePLCStatusBase.XAct)) * xScale;
                    //location_Crane_X = Convert.ToDouble(_cranePLCStatusBase.XAct + craneWith / 2) * xScale;
                    //location_Crab_X = 0;//在行车panel内，所以永远为0
                    X = (Convert.ToDouble(baySpaceX) - Convert.ToDouble(_cranePLCStatusBase.XAct)) * xScale;
                    location_Crane_X = (Convert.ToDouble(baySpaceX) - Convert.ToDouble(_cranePLCStatusBase.XAct + craneWith / 2)) * xScale;
                    location_Crab_X = 0;//在行车panel内，所以永远为0
                }

                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(panelHeight) / Convert.ToDouble(baySpaceY);

                //计算行车中心Y 区分Y坐标轴向上或者向下
                double Y = 0;
                double location_Crane_Y = 0;
                double location_Crab_Y = 0;
                if (yAxisDown == true)
                {
                    Y = Convert.ToDouble(_cranePLCStatusBase.YAct) * yScale;
                    location_Crane_Y = 0;
                    location_Crab_Y = Y - panelCrab.Height / 2;
                }
                else
                {
                    Y = (Convert.ToDouble(baySpaceY) - Convert.ToDouble(_cranePLCStatusBase.YAct)) * yScale;
                    location_Crane_Y = 0;
                    location_Crab_Y = Y - panelCrab.Height / 2;
                }
               

                //修改行车大车控件的宽度和高度
                this.Width = Convert.ToInt32(craneWith * xScale);
                this.Height = panelHeight;//大车的高度直接等于panel的高度

                //定位大车的坐标
                this.Location = new Point(Convert.ToInt32(location_Crane_X), Convert.ToInt32(location_Crane_Y));


                //修改小车的宽度
                panelCrab.Width = this.Width;

                //定位小车的坐标
                panelCrab.Location = new Point(Convert.ToInt32(location_Crab_X), Convert.ToInt32(location_Crab_Y));
                panelCrab.BringToFront();

                //无卷显示无卷标记
                if (_cranePLCStatusBase.HasCoil == 0)
                {
                    this.panelCrab.BackgroundImage = global::UACSControls.Resource1.imgCarNoCoil;
                }
                //有卷显示有卷标记
                else if (_cranePLCStatusBase.HasCoil == 1)
                {
                    //this.panelCrab.BackgroundImage = global::UACSControls.Resource1.imgCarCoil;
                    this.panelCrab.BackgroundImage = null; //背景图片
                    //this.panelCrab.BackColor = Color.SaddleBrown; //棕色
                    this.panelCrab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(175)))), ((int)(((byte)(66)))));  //虎皮黄
                }
                this.BringToFront();

                //if (CraneNO.ToString().Trim() == "1_4" || CraneNO.ToString().Trim() == "1_5" || CraneNO.ToString().Trim() == "1_6")
                //{
                //    contextMenuStrip1.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                LogManager.WriteProgramLog(ex.Message);
                LogManager.WriteProgramLog(ex.StackTrace);
            }
        }

        private void panelCrane_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawString(cranePLCStatusBase.CraneNO.ToString(), 
                new Font("微软雅黑", 9, FontStyle.Bold), 
                Brushes.White, new Point(2, this.Height - 20));

        }

        

        private void panelCrane_DoubleClick(object sender, EventArgs e)
        {
            if ( craneNO != string.Empty)
            {
                auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
                if (craneNO.Contains("1"))
                {
                    string bayno = null;
                    switch (craneNO)
                    {                       
                        case "1_1":
                        case "1_2":
                        case "1_3":
                            bayno = "2030原料库";
                            break;
                        case "1_4":
                        case "1_5":
                        case "1_6":
                            bayno = "原料库61跨";
                            break;
                        default:
                            bayno = null; ;
                            break;
                    }
                    if (bayno != null)
                    {
                        if (auth.IsOpen("01 行车指令配置"))
                        {
                            auth.CloseForm("01 行车指令配置");

                            auth.OpenForm("01 行车指令配置", true, bayno, craneNO);
                        }
                        else
                        {
                            auth.OpenForm("01 行车指令配置", true, bayno, craneNO);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 人工指令 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStrip_YardToTard_Click(object sender, EventArgs e)
        {
           FrmYardToYardRequest yard = new FrmYardToYardRequest();
            yard.CraneNo = craneNO;
            yard.Show();
        }
        /// <summary>
        /// 清空指令 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStrip_DelCraneOrder_Click(object sender, EventArgs e)
        {
            if (cranePLCStatusBase.HasCoil == 1)
            {
                MessageBox.Show("行车有料状态禁止清除指令");
                return;
            }
            //if(cranePLCStatusBase.CraneStatus == 40 && cranePLCStatusBase.ControlMode == 4)
            //{
            //    MessageBox.Show("自动模式空钩下降状态禁止清除指令");
            //    return;
            //}
            if (cranePLCStatusBase.ControlMode == 4)
            {
                MessageBox.Show("自动模式禁止清除指令");
                return;
            }

            DialogResult ret = MessageBox.Show("确定要清空" + craneNO + "行车的指令吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (ret == DialogResult.Cancel)
                return;
            if (cranePLCStatusBase.ControlMode == 4)
            {
                if (CreateManuOrder.isDelCraneOrder(craneNO))
                {
                    MessageBox.Show(craneNO + "指令已清空");
                    ParkClassLibrary.HMILogger.WriteLog("清空指令", craneNO + "行车清空指令", ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                {
                    MessageBox.Show(craneNO + "指令清空失败");
                }
            }
            else
            {
                if (CreateManuOrder.isDelNotAutoCraneOrder(craneNO))
                {
                    MessageBox.Show(craneNO + "指令已清空");
                    ParkClassLibrary.HMILogger.WriteLog("清空指令", craneNO + "行车清空指令", ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                {
                    MessageBox.Show(craneNO + "指令清空失败");
                }
            }


        }

        private void 登车ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult ret = MessageBox.Show("确定要" + craneNO + "行车回登车位吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (ret == DialogResult.Cancel)
            {
                return;
            }

            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add("BOARDING_" + craneNO, null);
            tagDP.Attach(TagValues);
            tagDP.SetData("BOARDING_" + craneNO, "1");
            HMILogger.WriteLog("登车", "行车登机：" + craneNO, LogLevel.Info, this.Text);
        }

        private void 矫正高度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string s = Interaction.InputBox("输入高度值（单位mm", craneNO + "行车高度矫正", "", -1, -1);
            //if (string.IsNullOrEmpty(s))
            //    return;
            //string tagValue = craneNO + "," + s + ",1,0,0,0,0,0";
            //Baosight.iSuperframe.TagService.DataCollection<object> wirteDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
            //wirteDatas["RESET_CRANE_Z"] = tagValue;
            //tagDataProvider.SetData("RESET_CRANE_Z", tagValue);
            //ParkClassLibrary.HMILogger.WriteLog("标高", craneNO + "行车矫正高度为：" + s + "mm", ParkClassLibrary.LogLevel.Info, "主监控");
            //确认提示
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否矫正高度？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                var tagValue = craneNO + ",20";
                var tag_CraneMode = craneNO + "_DownLoadShortCommand";
                tagDataProvider.SetData(tag_CraneMode, tagValue);
                ParkClassLibrary.HMILogger.WriteLog("标高", craneNO + "行车矫正高度为：" + tagValue, ParkClassLibrary.LogLevel.Info, "主监控");
            }
        }

        private void 避让ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    tagDP.ServiceName = "iplature";
            //    tagDP.AutoRegist = true;
            //    TagValues.Clear();
            //    TagValues.Add("Z01_EVADE_REQUEST", null);                
            //    tagDP.Attach(TagValues);

            //    string startPoint = "";
            //    string endPoint = "";
            //    string sqlPos = @"SELECT POS_WAIT FROM CRANE_PIPI WHERE CRANE_NO = '{0}'";
            //    sqlPos = string.Format(sqlPos, craneNO);
            //    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlPos))
            //    {
            //        while (rdr.Read())
            //        {
            //            if (rdr["POS_WAIT"] != DBNull.Value)
            //            {
            //                string value = rdr["POS_WAIT"].ToString().Trim();
            //                string[] sArray = value.Split(',');
            //                double xValues = (Convert.ToDouble(sArray[0].ToString()) / 1000);
            //                double yValues = (Convert.ToDouble(sArray[1].ToString()) / 1000);
            //                startPoint = xValues.ToString() + "," + yValues.ToString() +",5";
            //                endPoint = xValues.ToString() + "," + yValues.ToString() + ",5";
            //                //startPoint = rdr["POS_WAIT"].ToString().Trim() + ",5";
            //                //endPoint = rdr["POS_WAIT"].ToString().Trim() + ",5";
            //            }
            //            else
            //            {
            //                MessageBox.Show("该行车未设置避让位置！");
            //                return;
            //            }
            //        }
            //    }

            //    if (craneNO == "2_1")
            //    {
            //        string sql = @"INSERT INTO WMS_CRANE_EVADE
            //                (ORDER_NUMBER, BAY_NO, ALTERN_CRANE_NO, TASK_PRIORITY, TIME_TYPE_FRM_WMS, START_POINT, END_POINT, ORDER_SOURCE, CURRENT_2_START_TIME, START_2_END_TIME, CRANE_NO, SOUR_CRANE) 
            //                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')";
            //        sql = string.Format(sql, 3, "Z01", craneNO, 999999, 0, startPoint, endPoint, 6, "999999", "999999", "", "0");
            //        try
            //        {
            //            if (JudgeCraneEvade(craneNO))
            //            {
            //                MessageBox.Show("行车已设置避让！");
            //                return;
            //            }
            //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
            //            MessageBox.Show(craneNO + "避让已经创建成功");
            //            ParkClassLibrary.HMILogger.WriteLog("设置避让", craneNO + "行车设置避让", ParkClassLibrary.LogLevel.Info, "主监控");
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString());
            //        }

            //        //tagDP.SetData("Z21_EVADE_REQUEST", "6_3,6_2,999999,25000,X_DES");
            //        //MessageBox.Show(craneNO + "避让已经创建成功");
            //        //ParkClassLibrary.HMILogger.WriteLog("设置避让", craneNO + "行车设置避让", ParkClassLibrary.LogLevel.Info, "主监控");
            //    }
            //    else if (craneNO == "2_2")
            //    {

            //        string sql = @"INSERT INTO WMS_CRANE_EVADE
            //                (ORDER_NUMBER, BAY_NO, ALTERN_CRANE_NO, TASK_PRIORITY, TIME_TYPE_FRM_WMS, START_POINT, END_POINT, ORDER_SOURCE, CURRENT_2_START_TIME, START_2_END_TIME, CRANE_NO, SOUR_CRANE) 
            //                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')";
            //        sql = string.Format(sql, 3, "Z01", craneNO, 999999, 0, startPoint, endPoint, 6, "999999", "999999", "", "0");
            //        try
            //        {
            //            if (JudgeCraneEvade(craneNO))
            //            {
            //                MessageBox.Show("行车已设置避让！");
            //                return;
            //            }
            //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
            //            MessageBox.Show(craneNO + "避让已经创建成功");
            //            ParkClassLibrary.HMILogger.WriteLog("设置避让", craneNO + "行车设置避让", ParkClassLibrary.LogLevel.Info, "主监控");
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString());
            //        }

            //        //tagDP.SetData("Z22_EVADE_REQUEST", "6_6,6_5,999999,25000,X_DES");
            //        //MessageBox.Show(craneNO + "避让已经创建成功");
            //        //ParkClassLibrary.HMILogger.WriteLog("设置避让", craneNO + "行车设置避让", ParkClassLibrary.LogLevel.Info, "主监控");
            //    }
            //    else if (craneNO == "2_3")
            //    {
            //        string sql = @"INSERT INTO WMS_CRANE_EVADE
            //                (ORDER_NUMBER, BAY_NO, ALTERN_CRANE_NO, TASK_PRIORITY, TIME_TYPE_FRM_WMS, START_POINT, END_POINT, ORDER_SOURCE, CURRENT_2_START_TIME, START_2_END_TIME, CRANE_NO, SOUR_CRANE) 
            //                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')";
            //        sql = string.Format(sql, 3, "Z01", craneNO, 999999, 0, startPoint, endPoint, 6, "999999", "999999", "", "0");
            //        try
            //        {
            //            if (JudgeCraneEvade(craneNO))
            //            {
            //                MessageBox.Show("行车已设置避让！");
            //                return;
            //            }
            //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
            //            MessageBox.Show(craneNO + "避让已经创建成功");
            //            ParkClassLibrary.HMILogger.WriteLog("设置避让", craneNO + "行车设置避让", ParkClassLibrary.LogLevel.Info, "主监控");
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString());
            //        }

            //        //tagDP.SetData("Z23_EVADE_REQUEST", "6_9,6_8,999999,25000,X_DES");
            //        //MessageBox.Show(craneNO + "避让已经创建成功");
            //        //ParkClassLibrary.HMILogger.WriteLog("设置避让", craneNO + "行车设置避让", ParkClassLibrary.LogLevel.Info, "主监控");
            //    }                
            //    else
            //    {
            //        //MessageBox.Show("暂无该功能！");
            //    }
            //}
            //catch(Exception err)
            //{
            //    throw;
            //}
        }

        private void 取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add("Z01_EVADE_CANCEL", null);
            tagDP.Attach(TagValues);

            if (craneNO == "1_1")
            {
                string sql = @"update UACS_CRANE_EVADE_REQUEST set SENDER = NULL ,ORIGINAL_SENDER = NULL , EVADE_X_REQUEST = NULL ,EVADE_X = NULL ,EVADE_DIRECTION = NULL ,EVADE_ACTION_TYPE  = NULL ,STATUS = 'EMPTY' WHERE TARGET_CRANE_NO = '{0}'";
                sql = string.Format(sql,craneNO);
                //string sql = @"DELETE FROM WMS_CRANE_EVADE WHERE ORDER_NUMBER = '{0}' AND ALTERN_CRANE_NO = '{1}' AND ORDER_SOURCE = '{2}'";
                //sql = string.Format(sql, 3, craneNO, 6);
                try
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sql);
                    MessageBox.Show(craneNO + "避让指令清除成功");
                    ParkClassLibrary.HMILogger.WriteLog("取消避让", craneNO + "行车取消避让", ParkClassLibrary.LogLevel.Info, "主监控");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //tagDP.SetData("Z62_EVADE_CANCEL", "4_1,X_DES");
                //MessageBox.Show(craneNO + "避让指令清除成功");
                //ParkClassLibrary.HMILogger.WriteLog("取消避让", craneNO + "行车取消避让", ParkClassLibrary.LogLevel.Info, "主监控");
            }
            else if (craneNO == "1_2")
            {
                string sql = @"update UACS_CRANE_EVADE_REQUEST set SENDER = NULL ,ORIGINAL_SENDER = NULL , EVADE_X_REQUEST = NULL ,EVADE_X = NULL ,EVADE_DIRECTION = NULL ,EVADE_ACTION_TYPE  = NULL ,STATUS = 'EMPTY' WHERE TARGET_CRANE_NO = '{0}'";
                sql = string.Format(sql, craneNO);
                //string sql = @"DELETE FROM WMS_CRANE_EVADE WHERE ORDER_NUMBER = '{0}' AND ALTERN_CRANE_NO = '{1}' AND ORDER_SOURCE = '{2}'";
                //sql = string.Format(sql, 3, craneNO, 6);
                try
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sql);
                    MessageBox.Show(craneNO + "避让指令清除成功");
                    ParkClassLibrary.HMILogger.WriteLog("取消避让", craneNO + "行车取消避让", ParkClassLibrary.LogLevel.Info, "主监控");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //tagDP.SetData("Z62_EVADE_CANCEL", "4_2,X_DES");
                //MessageBox.Show(craneNO + "避让指令清除成功");
                //ParkClassLibrary.HMILogger.WriteLog("取消避让", craneNO + "行车取消避让", ParkClassLibrary.LogLevel.Info, "主监控");
            }
            else if (craneNO == "1_3")
            {
                string sql = @"update UACS_CRANE_EVADE_REQUEST set SENDER = NULL ,ORIGINAL_SENDER = NULL , EVADE_X_REQUEST = NULL ,EVADE_X = NULL ,EVADE_DIRECTION = NULL ,EVADE_ACTION_TYPE  = NULL ,STATUS = 'EMPTY' WHERE TARGET_CRANE_NO = '{0}'";
                sql = string.Format(sql, craneNO);
                //string sql = @"DELETE FROM WMS_CRANE_EVADE WHERE ORDER_NUMBER = '{0}' AND ALTERN_CRANE_NO = '{1}' AND ORDER_SOURCE = '{2}'";
                //sql = string.Format(sql, 3, craneNO,  6);
                try
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sql);
                    MessageBox.Show(craneNO + "避让指令清除成功");
                    ParkClassLibrary.HMILogger.WriteLog("取消避让", craneNO + "行车取消避让", ParkClassLibrary.LogLevel.Info, "主监控");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //tagDP.SetData("Z63_EVADE_CANCEL", "4_3,X_DES");
                //MessageBox.Show(craneNO + "避让指令清除成功");
                //ParkClassLibrary.HMILogger.WriteLog("取消避让", craneNO + "行车取消避让", ParkClassLibrary.LogLevel.Info, "主监控");
            }
            else
            {
                //MessageBox.Show("暂无该功能！");
            }
        }

        private void 高度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
            password.PassWords = craneNO.Trim();
            DialogResult retValue = password.ShowDialog();
            if(retValue != DialogResult.OK)
            {
                return;
            }

            string s = Interaction.InputBox("输入高度值（单位：mm)", craneNO + "行车高度矫正", "", -1, -1);
            if (string.IsNullOrEmpty(s))
                return;
            string tagValue = craneNO + "," + s + ",1,0,0,0,0,0";
            Baosight.iSuperframe.TagService.DataCollection<object> wirteDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
            wirteDatas["RESET_CRANE_Z"] = tagValue;
            tagDataProvider.SetData("RESET_CRANE_Z", tagValue);
            ParkClassLibrary.HMILogger.WriteLog("矫正高度", craneNO + "行车矫正高度为：" + s + "mm", ParkClassLibrary.LogLevel.Info, "主监控");
        }

        //private void 角度ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
        //    password.PassWords = craneNO.Trim();
        //    DialogResult retValue = password.ShowDialog();
        //    if (retValue != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    string s = Interaction.InputBox("输入角度值（单位：°)", craneNO + "行车角度矫正", "", -1, -1);
        //    if (string.IsNullOrEmpty(s))
        //        return;
        //    string tagValue = craneNO + ",0,0," + s + ",0,1,0,0";
        //    Baosight.iSuperframe.TagService.DataCollection<object> wirteDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
        //    wirteDatas["RESET_CRANE_Z"] = tagValue;
        //    tagDataProvider.SetData("RESET_CRANE_Z", tagValue);
        //    ParkClassLibrary.HMILogger.WriteLog("矫正角度", craneNO + "行车矫正角度为：" + s + "°", ParkClassLibrary.LogLevel.Info, "主监控");
        //}

        //用户功能限制
        private void getUserName()
        {
            if (!auth.GetUserName().Contains("admin") && !auth.GetUserName().Contains("ZYBG"))
            {
                contextMenuStrip1.Enabled = false;
            }
        }

        private bool craneEvadeValue;
        //判断避让
        private bool JudgeCraneEvade(string craneNo)
        {
            try
            {
                string sql = @"SELECT * FROM WMS_CRANE_EVADE WHERE ORDER_NUMBER = '{0}' AND ALTERN_CRANE_NO = '{1}' AND ORDER_SOURCE = '{2}'";
                sql = string.Format(sql, 0, craneNo, 6);
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    if(rdr.Read())
                    {
                        craneEvadeValue = true;
                    }
                    else
                    {
                        craneEvadeValue = false;
                    }
                }
            }          
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
            if(craneEvadeValue == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            try
            {
                //string sql = @"SELECT STATUS,POS_DENGJI_1,POS_DENGJI_2,TYPE FROM CRANE_PIPI ";
                //sql += " WHERE CRANE_NO = '" + craneNO + "'";
                //using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                //{
                //    while (rdr.Read())
                //    {
                //        if (rdr["POS_DENGJI_1"].ToString().Trim() == "" && rdr["POS_DENGJI_2"].ToString().Trim() == "")
                //        {
                //            this.登机请求ToolStripMenuItem.Visible = false;
                //        }
                //        else if (rdr["POS_DENGJI_1"].ToString().Trim() == "")
                //        {
                //            this.登机1ToolStripMenuItem.Visible = false;
                //        }
                //        else if(rdr["POS_DENGJI_2"].ToString().Trim() == "")
                //        {
                //            this.登机2ToolStripMenuItem.Visible = false;
                //        }
                //        if (rdr["TYPE"].ToString() == "1")
                //        {
                //            //this.行车排水ToolStripMenuItem.Text = "行车排水：关闭";
                //            if (rdr["STATUS"].ToString() == "ENDED" || rdr["STATUS"].ToString() == "TO_BE_END")
                //            {
                //                this.登机请求ToolStripMenuItem.Text = "登机请求：关闭";
                //                this.开启ToolStripMenuItem.Enabled = true;
                //                this.关闭ToolStripMenuItem.Enabled = true;
                //            }
                //            else
                //            {
                //                this.登机请求ToolStripMenuItem.Text = "登机请求：登机1";
                //                this.开启ToolStripMenuItem.Enabled = false;
                //                this.关闭ToolStripMenuItem.Enabled = false;
                //            }
                //        }
                //        else if(rdr["TYPE"].ToString() == "2")
                //        {
                //            //this.行车排水ToolStripMenuItem.Text = "行车排水：关闭";
                //            if (rdr["STATUS"].ToString() == "ENDED" || rdr["STATUS"].ToString() == "TO_BE_END")
                //            {
                //                this.登机请求ToolStripMenuItem.Text = "登机请求：关闭";
                //                this.开启ToolStripMenuItem.Enabled = true;
                //                this.关闭ToolStripMenuItem.Enabled = true;
                //            }
                //            else
                //            {
                //                this.登机请求ToolStripMenuItem.Text = "登机请求：登机2";
                //                this.开启ToolStripMenuItem.Enabled = false;
                //                this.关闭ToolStripMenuItem.Enabled = false;
                //            }
                //        }
                //        else
                //        {
                //            this.登机请求ToolStripMenuItem.Text = "登机请求：关闭";
                //            if (rdr["STATUS"].ToString() == "ENDED" || rdr["STATUS"].ToString() == "TO_BE_END")
                //            {
                //                //this.行车排水ToolStripMenuItem.Text = "行车排水：关闭";
                //                this.登机1ToolStripMenuItem.Enabled = true;
                //                this.登机2ToolStripMenuItem.Enabled = true;
                //                this.关闭ToolStripMenuItem1.Enabled = true;
                //            }
                //            else
                //            {
                //                //this.行车排水ToolStripMenuItem.Text = "行车排水：开启";
                //                this.登机1ToolStripMenuItem.Enabled = false;
                //                this.登机2ToolStripMenuItem.Enabled = false;
                //                this.关闭ToolStripMenuItem1.Enabled = false;
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void 登机1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要登机1请求开启吗？", "操作提示", btn);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                //string sql = @"update CRANE_PIPI set STATUS = 'TO_BE_START',TYPE = '1' ";
                //sql += " WHERE CRANE_NO = '" + craneNO + "'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql);
                //MessageBox.Show(craneNO + "登机1请求开启");
                HMILogger.WriteLog("登机请求", "登机1：" + craneNO, LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void 登机2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要登机2请求开启吗？", "操作提示", btn);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                //string sql = @"update CRANE_PIPI set STATUS = 'TO_BE_START',TYPE = '2' ";
                //sql += " WHERE CRANE_NO = '" + craneNO + "'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql);
                //MessageBox.Show(craneNO + "登机2请求开启");
                HMILogger.WriteLog("登机请求", "登机2：" + craneNO, LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void 关闭ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要登机请求关闭吗？", "操作提示", btn);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                //string sql = @"update CRANE_PIPI set STATUS = 'TO_BE_END' ";
                //sql += " WHERE CRANE_NO = '" + craneNO + "'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql);
                //MessageBox.Show(craneNO + "登机请求关闭");
                HMILogger.WriteLog("登机请求", "登机关闭：" + craneNO, LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void 开启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要行车排水开启吗？", "操作提示", btn);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                //string sql = @"update CRANE_PIPI set STATUS = 'TO_BE_START',TYPE = '0' ";
                //sql += " WHERE CRANE_NO = '" + craneNO + "'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql);
                //MessageBox.Show(craneNO + "行车排水开启");
                HMILogger.WriteLog("行车排水", "排水开启：" + craneNO, LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要行车排水关闭吗？", "操作提示", btn);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                //string sql = @"update CRANE_PIPI set STATUS = 'TO_BE_END' ";
                //sql += " WHERE CRANE_NO = '" + craneNO + "'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql);
                //MessageBox.Show(craneNO + "行车排水关闭");
                HMILogger.WriteLog("行车排水", "排水关闭：" + craneNO, LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (CraneNO.ToString().Trim() == "1_4" || CraneNO.ToString().Trim() == "1_5" || CraneNO.ToString().Trim() == "1_6")
            {
                this.ToolStrip_DelCraneOrder.Enabled = false;
                this.设置避让ToolStripMenuItem.Enabled = false;
                this.登车ToolStripMenuItem.Enabled = false;
                this.登机请求ToolStripMenuItem.Enabled = false;
                //this.行车排水ToolStripMenuItem.Enabled = false;
                //this.角度ToolStripMenuItem.Enabled = false;
            }
            if (CraneNO.ToString().Trim() == "1_1" || CraneNO.ToString().Trim() == "1_2" || CraneNO.ToString().Trim() == "1_3")
            {
                this.ToolStrip_DelCraneOrder.Enabled = true;
                this.设置避让ToolStripMenuItem.Enabled = true;
                this.登车ToolStripMenuItem.Enabled = true;
                this.登机请求ToolStripMenuItem.Enabled = true;
                //this.行车排水ToolStripMenuItem.Enabled = true;
                //this.角度ToolStripMenuItem.Enabled = true;
            }
            if (CraneNO.ToString().Trim() == "1" || CraneNO.ToString().Trim() == "2" || CraneNO.ToString().Trim() == "3" || CraneNO.ToString().Trim() == "4")
            {
                this.ToolStrip_DelCraneOrder.Enabled = true;
                this.设置避让ToolStripMenuItem.Enabled = true;
                this.登车ToolStripMenuItem.Enabled = true;
                this.登机请求ToolStripMenuItem.Enabled = true;
                //this.行车排水ToolStripMenuItem.Enabled = true;
                //this.角度ToolStripMenuItem.Enabled = true;
            }
        }

        private void 修改卸下位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {                    
            FrmChangeToStock frm = new FrmChangeToStock();
            frm.CraneNo = craneNO;
            frm.ShowDialog();
        }
    }
}
