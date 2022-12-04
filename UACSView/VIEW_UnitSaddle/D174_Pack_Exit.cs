﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using UACSControls;
using ParkClassLibrary;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
namespace UACSView
{
    public partial class DuXinSteel_Pack_Exit : FormBase
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
        public DuXinSteel_Pack_Exit()
        {
            InitializeComponent();
            //反射获取双缓存
            Type dgvType = this.dgvExitSaddleInfo.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvExitSaddleInfo, true, null);

            this.Load += DuXinSteel_Pack_Exit_Load;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }

        private UnitSaddleTagRead lineSaddleTag = new UnitSaddleTagRead();
        private UnitSaddleMethod saddleMethod = null;
        private UnitSaddleMethod saddleMethod1 = null;
        private UnitExitSaddleInfo exitSaddleInfo = new UnitExitSaddleInfo();

        private Dictionary<string, CoilUnitSaddle> dicSaddleControls = new Dictionary<string, CoilUnitSaddle>();
        private bool tabActived = true;

        private const string AREA_SAFE_EXIT_D102 = "AREA_SAFE_D102_EXIT";

        public long theValue;
        public long thevalue
        {
            get { return theValue; }
            set { thevalue = value; }
        }

        //称重卷变量
        private string WEIGH_MAT_NO = String.Empty;
        private string WEIGH_YARD_STOCK_NO = String.Empty;
        private string WEIGH_UNIT_STOCK_NO = String.Empty;
        private string EXE_CRANES = String.Empty;

        void DuXinSteel_Pack_Exit_Load(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            tagDataProvider.ServiceName = "iplature";
            //绑定鞍座控件                     
            dicSaddleControls["PA-6WC0001"] = coilUnitSaddle1;
            dicSaddleControls["PA-6WC0002"] = coilUnitSaddle2;
            dicSaddleControls["PA-6WC0003"] = coilUnitSaddle3;
            dicSaddleControls["PA-6WC0004"] = coilUnitSaddle4;
            dicSaddleControls["PA-6WC0005"] = coilUnitSaddle5;
            dicSaddleControls["PA-6WC0006"] = coilUnitSaddle6;
            dicSaddleControls["PA-6WC0007"] = coilUnitSaddle7;
            dicSaddleControls["PA-6WC0008"] = coilUnitSaddle8;          
           
            dicSaddleControls["D174WC0004"] = coilUnitSaddle17;
            dicSaddleControls["D174WC0005"] = coilUnitSaddle18;
            dicSaddleControls["D174WC0006"] = coilUnitSaddle19;

            coilUnitSaddleButton4.MySaddleNo = "D174WC0004";
            coilUnitSaddleButton5.MySaddleNo = "D174WC0005";
            coilUnitSaddleButton6.MySaddleNo = "D174WC0006";    


            //coilUnitStatus1.InitTagDataProvide(constData.tagServiceName);
            //coilUnitStatus1.MyStatusTagName = AREA_SAFE_EXIT_D102;

            //实例化机组鞍座处理类
            saddleMethod = new UnitSaddleMethod(constData.UnitNo_11, constData.ExitSaddleDefine, constData.tagServiceName);
            saddleMethod.ReadDefintion();

            saddleMethod1 = new UnitSaddleMethod(constData.UnitNo_7, constData.ExitSaddleDefine, constData.tagServiceName);
            saddleMethod1.ReadDefintion();

            lineSaddleTag.InitTagDataProvider(constData.tagServiceName);

            coilUnitSaddleButton1.MySaddleNo = "PA-6WC0001";
            //coilUnitSaddleButton2.MySaddleNo = "D108WC0001";          

            //coilUnitSaddleButton0.MySaddleTagName = "D401_EXIT_00_LOCK_REQUEST";

            //把表中的tag名称赋值到控件中
            foreach (Control control in panel2.Controls)
            {
                //添加解锁鞍座控件
                if (control is CoilUnitSaddleButton)
                {
                    
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (saddleMethod.DicSaddles.ContainsKey(t.MySaddleNo))
                    {
                        UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[t.MySaddleNo];
                        if (!string.IsNullOrEmpty(theSaddleInfo.TagAdd_LockRequest) && theSaddleInfo.TagAdd_LockRequest != "")
                        {
                            t.MySaddleTagName = theSaddleInfo.TagAdd_LockRequest;
                            lineSaddleTag.AddTagName(theSaddleInfo.TagAdd_LockRequest);

                        }
                    }
                    if (saddleMethod1.DicSaddles.ContainsKey(t.MySaddleNo))
                    {
                        UnitSaddleBase theSaddleInfo = saddleMethod1.DicSaddles[t.MySaddleNo];
                        if (!string.IsNullOrEmpty(theSaddleInfo.TagAdd_LockRequest) && theSaddleInfo.TagAdd_LockRequest != "")
                        {
                            t.MySaddleTagName = theSaddleInfo.TagAdd_LockRequest;
                            lineSaddleTag.AddTagName(theSaddleInfo.TagAdd_LockRequest);

                        }
                    }
                }
                //添加机组状态控件
                if (control is CoilUnitStatus)
                {
                    CoilUnitStatus t = (CoilUnitStatus)control;
                    if (!string.IsNullOrEmpty(t.MyStatusTagName) && t.MyStatusTagName != "")
                    {
                        lineSaddleTag.AddTagName(t.MyStatusTagName);
                    }
                }
            }
            foreach (Control control in panel4.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {

                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (saddleMethod.DicSaddles.ContainsKey(t.MySaddleNo))
                    {
                        UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[t.MySaddleNo];
                        if (!string.IsNullOrEmpty(theSaddleInfo.TagAdd_LockRequest) && theSaddleInfo.TagAdd_LockRequest != "")
                        {
                            t.MySaddleTagName = theSaddleInfo.TagAdd_LockRequest;
                            lineSaddleTag.AddTagName(theSaddleInfo.TagAdd_LockRequest);

                        }
                    }
                }
            }

            lineSaddleTag.SetReady();
            //把实例化后的机组tag处理类装备每个控件
            foreach (Control control in panel2.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    t.InitUnitSaddle(lineSaddleTag);
                }
            }

            foreach (Control control in panel4.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    t.InitUnitSaddle(lineSaddleTag);
                }
            }
            GetLightCoilFlag();
            Get_PA_Status();
            exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo, constData.UnitNo_7);
            exitSaddleInfo.getExitSaddleDt(dataGridViewSaddleMessage_D413EXIT, constData.UnitNo_11);

            GetWeightingInfo();
            bindCBB();

            //是否开启定时器
            timer_LineSaddleControl.Enabled = true;
            //设定刷新时间
            timer_LineSaddleControl.Interval = 3000;
            //GetFirstArea();
        }

        private void timer_LineSaddleControl_Tick(object sender, EventArgs e)
        {
            //不在当前页面停止刷新
            if (tabActived == false)
            {
                return;
            }
         
            lineSaddleTag.readTags();
            foreach (Control control in panel4.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (!string.IsNullOrEmpty(t.MySaddleTagName) && t.MySaddleTagName != "")
                    {
                        CoilUnitSaddleButton.delRefresh_Button_Light del = t.refresh_Button_Light;
                        del(lineSaddleTag.getTagValue(t.MySaddleTagName));
                    }
                }
            }
            foreach (Control control in panel2.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (!string.IsNullOrEmpty(t.MySaddleTagName) && t.MySaddleTagName != "")
                    {
                        CoilUnitSaddleButton.delRefresh_Button_Light del = t.refresh_Button_Light;
                        del(lineSaddleTag.getTagValue(t.MySaddleTagName));
                    }
                }
            }
            foreach (Control control in panel1.Controls)
            {
                if (control is CoilUnitStatus)
                {
                    CoilUnitStatus t = (CoilUnitStatus)control;
                    if (!string.IsNullOrEmpty(t.MyStatusTagName) && t.MyStatusTagName != "")
                    {
                        CoilUnitStatus.delSetColor del = t.SetColor;
                        del(lineSaddleTag.getTagValue(t.MyStatusTagName));
                    }
                }
            }

            exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo, constData.UnitNo_7);
            exitSaddleInfo.getExitSaddleDt(dataGridViewSaddleMessage_D413EXIT, constData.UnitNo_11);
            getSaddleMessage();
            GetLightCoilFlag();
            Get_PA_Status();
            GetWeightingOrder();
        }


        private void getSaddleMessage()
        {
            saddleMethod.ReadDefintion();
            saddleMethod.getTagNameList();
            saddleMethod.getTagValues();

            saddleMethod1.ReadDefintion();
            saddleMethod1.getTagNameList();
            saddleMethod1.getTagValues();

            foreach (string theL2SaddleName in dicSaddleControls.Keys)
            {
                if (saddleMethod.DicSaddles.ContainsKey(theL2SaddleName))
                {
                    CoilUnitSaddle conSaddle = dicSaddleControls[theL2SaddleName];
                    //CoilUnitSaddleButton SaddleButton = dicUnitSaddleButton[theL2SaddleName];
                    UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[theL2SaddleName];
                    //鞍座反馈
                    if (theSaddleInfo.TagVal_IsLocked == 1)
                        conSaddle.UpVisiable = true;
                    else
                        conSaddle.UpVisiable = false;

                    //鞍座占位
                    if (theSaddleInfo.TagVal_IsOccupied == 1)
                        conSaddle.CoilBackColor = Color.Green;
                    else
                        conSaddle.CoilBackColor = Color.FromArgb(165, 222, 241);


                    //钢卷号
                    if (theSaddleInfo.CoilNO != string.Empty)
                    {
                        conSaddle.CoilId = theSaddleInfo.CoilNO;
                        conSaddle.CoilStatus = 2;
                    }
                    else
                    {
                        conSaddle.CoilId = "";
                        conSaddle.CoilStatus = -10;
                    }
                }
                if (saddleMethod1.DicSaddles.ContainsKey(theL2SaddleName))
                {
                    CoilUnitSaddle conSaddle = dicSaddleControls[theL2SaddleName];
                    UnitSaddleBase theSaddleInfo = saddleMethod1.DicSaddles[theL2SaddleName];
                    //鞍座反馈
                    if (theSaddleInfo.TagVal_IsLocked == 1)
                        conSaddle.UpVisiable = true;
                    else
                        conSaddle.UpVisiable = false;

                    //鞍座占位
                    if (theSaddleInfo.TagVal_IsOccupied == 1)
                        conSaddle.CoilBackColor = Color.Green;
                    else
                        conSaddle.CoilBackColor = Color.FromArgb(165, 222, 241);


                    //钢卷号
                    if (theSaddleInfo.CoilNO != string.Empty)
                    {
                        conSaddle.CoilId = theSaddleInfo.CoilNO;
                        conSaddle.CoilStatus = 2;
                    }
                    else
                    {
                        conSaddle.CoilId = "";
                        conSaddle.CoilStatus = -10;
                    }
                }

            }
        }

        //需要引用formbase
        void MyTabActivated(object sender, EventArgs e)
        {
            tabActived = true;
        }
        void MyTabDeactivated(object sender, EventArgs e)
        {
            tabActived = false;
        }


        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

        private string[] arrTagAdress;
        private void readTags()
        {
            try
            {
                inDatas.Clear();
                tagDataProvider.GetData(arrTagAdress, out inDatas);
            }
            catch (Exception ex)
            {
            }
        }
        private string get_value(string tagName)
        {
            string theValue = string.Empty;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToString(valueObject);
            }
            catch
            {
                valueObject = null;
            }
            return theValue; ;
        }
       

        private void btnZ02_Click(object sender, EventArgs e)
        {
            string firstArea = "Z02";
            inDatas["D102EXIT_B_TO_Z02_OR_Z03"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z02！");
        }

        private void btnZ03_Click(object sender, EventArgs e)
        {
            string firstArea = "Z03";
            inDatas["D102EXIT_B_TO_Z02_OR_Z03"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z03！");
        }

        private void btnZ04_Click(object sender, EventArgs e)
        {
            string firstArea = "Z04";
            inDatas["D102EXIT_B_TO_Z04_OR_Z05"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z04！");
        }

        private void b7tnZ05_Click(object sender, EventArgs e)
        {
            string firstArea = "Z05";
            inDatas["D102EXIT_B_TO_Z04_OR_Z05"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z05！");
        }

        


        private void button4_Click(object sender, EventArgs e)
        {
            auth.OpenForm("01 Z21跨监控");
        }

        private void btn_PA_Status_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_PA_Status.Text.Contains("正常"))
                {
                    string value = "1";
                    inDatas["PA-6_IS_BUSY"] = value;
                    tagDataProvider.Write2Level1(inDatas, 1);
                    MessageBox.Show("已打开包装线繁忙状态！");
                    btn_PA_Status.Text = "包装状态：繁忙";
                    btn_PA_Status.BackColor = Color.Yellow;

                }
                else if (btn_PA_Status.Text.Contains("繁忙"))
                {
                    string value = "0";
                    inDatas["PA-6_IS_BUSY"] = value;
                    tagDataProvider.Write2Level1(inDatas, 1);
                    MessageBox.Show("已恢复包装线正常状态！");
                    btn_PA_Status.Text = "包装状态：正常";
                    btn_PA_Status.BackColor = Color.LightGreen;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void Get_PA_Status()
        {

            List<string> lstAdress = new List<string>();
            lstAdress.Clear();
            lstAdress.Add("PA-6_IS_BUSY");
            lstAdress.Add("PA-6_READY");
            arrTagAdress = lstAdress.ToArray<string>();
            readTags();
            if (get_value("PA-6_IS_BUSY") == "0")
            {
                btn_PA_Status.Text = "包装状态：正常";
                btn_PA_Status.BackColor = Color.LightGreen;
            }
            else if (get_value("PA-6_IS_BUSY") == "1")
            {
                btn_PA_Status.Text = "包装状态：繁忙";
                btn_PA_Status.BackColor = Color.Yellow;
            }

            if (get_value("PA-6_READY") == "0")
            {
                btnLightCoil.Text = "光卷入库：关闭";
                btnLightCoil.BackColor = Color.Yellow;
            }
            else if (get_value("PA-6_READY") == "1")
            {
                btnLightCoil.Text = "光卷入库：打开";
                btnLightCoil.BackColor = Color.LightGreen;
            }
        }

        private void btnLightCoil_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnLightCoil.Text.Contains("关闭"))
                {
                    string value = "1";
                    inDatas["PA-6_READY"] = value;
                    tagDataProvider.Write2Level1(inDatas, 1);
                    MessageBox.Show("已打开光卷入库吊运！");
                    btnLightCoil.Text = "光卷入库：打开";
                    btnLightCoil.BackColor = Color.LightGreen;
                }
                else if (btnLightCoil.Text.Contains("打开"))
                {
                    string value = "0";
                    inDatas["PA-6_READY"] = value;
                    tagDataProvider.Write2Level1(inDatas, 1);
                    MessageBox.Show("已关闭光卷入库吊运！");
                    btnLightCoil.Text = "光卷入库：关闭";
                    btnLightCoil.BackColor = Color.Yellow;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void GetLightCoilFlag()
        {
            List<string> lstAdress = new List<string>();
            lstAdress.Clear();
            lstAdress.Add("PA-6_READY");
            arrTagAdress = lstAdress.ToArray<string>();
            readTags();
            if (get_value("PA-6_READY") == "0")
            {
                btnLightCoil.Text = "光卷入库：关闭";
                btnLightCoil.BackColor = Color.Yellow;
            }
            else if (get_value("PA-6_READY") == "1")
            {
                btnLightCoil.Text = "光卷入库：打开";
                btnLightCoil.BackColor = Color.LightGreen;
            }
        }

        private void btn_Coil_Down_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要机组称重落卷吗？", "操作提示", btn);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            if (string.IsNullOrEmpty(cbb_CraneNo.Text.Trim()) || !EXE_CRANES.Contains(cbb_CraneNo.Text.Trim()))
            {
                MessageBox.Show("请选择吊运行车！", "操作提示");
                return;
            }

            int Stock_Status = 0;
            int Lock_Flag = 0;
            string Crane_NO = cbb_CraneNo.Text.Trim();
            string Unit_NO = constData.UnitNo_7;
            string Mat_NO = String.Empty;
            string Coil_NO = WEIGH_MAT_NO;
            string FromStock_NO = WEIGH_YARD_STOCK_NO;
            string ToStock_NO = WEIGH_UNIT_STOCK_NO;
            string Flag = "1";
            try
            {
                //string sqlCheck = @"SELECT MAT_NO,STOCK_STATUS,LOCK_FLAG FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO = '" + FromStock_NO + "'";
                //using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlCheck))
                //{
                //    while (rdr.Read())
                //    {
                //        Stock_Status = Convert.ToInt32(rdr["STOCK_STATUS"]);
                //        Lock_Flag = Convert.ToInt32(rdr["LOCK_FLAG"]);
                //        Mat_NO = rdr["MAT_NO"].ToString().Trim();
                //    }
                //}

                //if (Stock_Status != 2 || Lock_Flag != 0 || Mat_NO != Coil_NO)
                //{
                //    MessageBox.Show("请点击" + FromStock_NO + "库位的钢卷称重！", "操作提示");
                //    return;
                //}

                string sqlSet = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = '" + WEIGH_MAT_NO + "',STOCK_STATUS = 2,LOCK_FLAG = 0,EVENT_ID= 888888 WHERE STOCK_NO = '" + WEIGH_YARD_STOCK_NO + "' ";
                DB2Connect.DBHelper.ExecuteNonQuery(sqlSet);

                string message = Crane_NO + "," + Unit_NO + "," + Coil_NO + "," + FromStock_NO + "," + ToStock_NO + "," + Flag;

                string sql = @"update UNIT_WEIGHING_CONFIGURATION set TAG_VALUE = '" + message + "'";
                sql += " WHERE UNIT_NO = '" + Unit_NO + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                GetWeightingOrder();

                HMILogger.WriteLog("称重卷吊入", "机组鞍座：" + ToStock_NO, LogLevel.Warn, this.Text);
                MessageBox.Show("已通知行车执行机组称重吊入操作！", "操作提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_Coil_Up_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要机组称重起卷吗？", "操作提示", btn);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            if (string.IsNullOrEmpty(cbb_CraneNo.Text.Trim()) || !EXE_CRANES.Contains(cbb_CraneNo.Text.Trim()))
            {
                MessageBox.Show("请选择吊运行车！", "操作提示");
                return;
            }

            string Crane_NO = cbb_CraneNo.Text.Trim();
            string Unit_NO = constData.UnitNo_7;
            string Coil_NO = WEIGH_MAT_NO;
            string FromStock_NO = WEIGH_UNIT_STOCK_NO;
            string ToStock_NO = WEIGH_YARD_STOCK_NO;
            string Flag = "0";
            string message = Crane_NO + "," + Unit_NO + "," + Coil_NO + "," + FromStock_NO + "," + ToStock_NO + "," + Flag;

            try
            {
                string sql = @"update UNIT_WEIGHING_CONFIGURATION set TAG_VALUE = '" + message + "'";
                sql += " WHERE UNIT_NO = '" + Unit_NO + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                GetWeightingOrder();

                HMILogger.WriteLog("称重卷吊离", "机组鞍座：" + FromStock_NO, LogLevel.Warn, this.Text);
                MessageBox.Show("已通知行车执行机组称重吊离操作！", "操作提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            string Unit_NO = constData.UnitNo_7;
            string message = "";

            string Mat_No = "";
            string Stock_Status = "";

            try
            {
                string sqlSelect = @"SELECT MAT_NO,STOCK_STATUS from UACS_YARDMAP_STOCK_DEFINE  WHERE STOCK_NO = '" + WEIGH_YARD_STOCK_NO + "' ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlSelect))
                {
                    while (rdr.Read())
                    {
                        if (rdr["MAT_NO"] != DBNull.Value)
                        {
                            Mat_No = rdr["MAT_NO"].ToString();
                            Stock_Status = rdr["STOCK_STATUS"].ToString();
                        }
                    }
                }
                if (Mat_No != "" || Stock_Status == "2")
                {
                    string sqlSet = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = NULL,STOCK_STATUS = 2,LOCK_FLAG = 1,EVENT_ID= 888888 WHERE STOCK_NO = '" + WEIGH_YARD_STOCK_NO + "' ";
                    DB2Connect.DBHelper.ExecuteNonQuery(sqlSet);
                }               

                string sql = @"update UNIT_WEIGHING_CONFIGURATION set TAG_VALUE = '" + message + "'";
                sql += " WHERE UNIT_NO = '" + Unit_NO + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);

                GetWeightingOrder();

                HMILogger.WriteLog("取消称重", "机组：" + Unit_NO, LogLevel.Warn, this.Text);
                MessageBox.Show("已取消机组称重（吊入/吊离）操作！", "操作提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetWeightingOrder()
        {
            try
            {
                string Value = String.Empty;
                string Unit_NO = constData.UnitNo_7;
                string sql = @"SELECT TAG_VALUE FROM UNIT_WEIGHING_CONFIGURATION WHERE UNIT_NO = '" + Unit_NO + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        Value = rdr["TAG_VALUE"].ToString().Trim();
                    }
                }
                if (String.IsNullOrEmpty(Value))
                {
                    panel_Coil_Down.BackColor = Color.White;
                    panel_Coil_Up.BackColor = Color.White;
                    label_Status.Text = "无请求";
                }
                else
                {
                    string[] Flag = Value.Split(',');
                    if (Flag[5] == "1")
                    {
                        panel_Coil_Down.BackColor = Color.Green;
                        panel_Coil_Up.BackColor = Color.Red;
                        label_Status.Text = "请求吊入";
                    }
                    else if ((Flag[5] == "0"))
                    {
                        panel_Coil_Up.BackColor = Color.Green;
                        panel_Coil_Down.BackColor = Color.Red;
                        label_Status.Text = "请求吊离";
                    }
                    else
                    {
                        panel_Coil_Down.BackColor = Color.White;
                        panel_Coil_Up.BackColor = Color.White;
                        label_Status.Text = "无请求";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetWeightingInfo()
        {
            try
            {
                string Unit_NO = constData.UnitNo_7;
                string sql = @"SELECT WEIGH_MATNO,WEIGH_YARD_STOCKNO,WEIGH_UNIT_STOCKNO,EXE_CRANES FROM UNIT_WEIGHING_CONFIGURATION WHERE UNIT_NO = '" + Unit_NO + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        WEIGH_MAT_NO = rdr["WEIGH_MATNO"].ToString().Trim();
                        WEIGH_YARD_STOCK_NO = rdr["WEIGH_YARD_STOCKNO"].ToString().Trim();
                        WEIGH_UNIT_STOCK_NO = rdr["WEIGH_UNIT_STOCKNO"].ToString().Trim();
                        EXE_CRANES = rdr["EXE_CRANES"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bindCBB()
        {
            try
            {
                cbb_CraneNo.Items.Clear();
                string[] Cranes = EXE_CRANES.Split(',');
                foreach (string crane in Cranes)
                {
                    cbb_CraneNo.Items.Add(crane.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
