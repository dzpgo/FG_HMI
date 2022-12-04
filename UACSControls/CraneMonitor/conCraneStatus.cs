using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using System.Threading;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using ParkClassLibrary;
using System.Media;
using UACSPopupForm;

namespace UACSControls
{

    public partial class conCraneStatus : UserControl
    {
        public const long SHORT_CMD_NORMAL_STOP = 100;
        public const long SHORT_CMD_EMG_STOP = 200;
        public const long SHORT_CMD_RESET = 300;
        public const long SHORT_CMD_ASK_COMPUTER_AUTO = 400;
        public const long SHORT_CMD_CANCEL_COMPUTER_AUTO = 500;

        private FrmModeSwitchover frmModeSwitchover = null;
        private PlaySoundHandler playSound = null;
        private CraneStatusInBay craneinfo = null;
        private FormMessageBox box = null;
        private bool flag = false;
        private bool weightFlag = false;

        private bool firstTimeShow = false;
        private bool playesounderFlag = false;

        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = null;

        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth = null;

        //无动作报警
        public event Closedelegate Closedelegate;
        Thread t_init = null;
        public const string CRANE_ALARM = "CRANE_ALARM";
        int craneAlarm = 0;
        DateTime dt_start = DateTime.Now;

        int timeOutMin = 3;
        System.Object locker = new System.Object();
        public conCraneStatus()
        {
            InitializeComponent();
        }


        private string TagServiceName = string.Empty;
        //step1
        public void InitTagDataProvide(string tagServiceName)
        {
            try
            {
                tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
                this.TagServiceName = tagServiceName;
                tagDataProvider.ServiceName = TagServiceName;
                craneinfo = new CraneStatusInBay();
                playSound += new PlaySoundHandler(PlaySoundEvt);
            }
            catch (Exception ex)
            { }
        }


        private string craneNO = string.Empty;
        //step2
        public string CraneNO
        {
            get { return craneNO; }
            set { craneNO = value; }
        }

        long heatBeatCounter = 0;
        bool communicate_PLC_OK = true;
        int messagebox = 0;
        int messagebox3 = 0;

        /// <summary>
        /// 存解析出来的报警代码
        /// </summary>
        private List<int> listAlarm = new List<int>();
        private List<string> listCrane = new List<string>();
        private CraneStatusBase craneStatusBase = new CraneStatusBase();
        public delegate void RefreshControlInvoke(CraneStatusBase theCraneStatusBase);


        //step3
        public void RefreshControl(CraneStatusBase theCraneStatusBase)
        {
            try
            {
                auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
                getUserName();
                craneStatusBase = theCraneStatusBase;

                //if(craneStatusBase.CraneNO.ToString().Trim() == "1_4" || craneStatusBase.CraneNO.ToString().Trim() == "1_5" || craneStatusBase.CraneNO.ToString().Trim() == "1_6")
                //{
                //    btnStop.Visible = false;
                //    btnStop.Enabled = false;
                //    btnMode.Visible = false;
                //    btnMode.Enabled = false;
                //    btnShow.Visible = false;
                //    btnShow.Enabled = false;
                //}

                if (craneStatusBase.CraneNO.ToString().Trim() == "1_5" || craneStatusBase.CraneNO.ToString().Trim() == "1_6")
                {
                    btnStop.Visible = false;
                    btnStop.Enabled = false;
                    btnMode.Visible = false;
                    btnMode.Enabled = false;                    
                }
                btnShow.Visible = false;
                btnShow.Enabled = false;

                //行车号
                lbl_CraneNo.Text = "行车 " + craneStatusBase.CraneNO.ToString();
                //准备好信号灯
                refresh_Textbox_Light(light_READY, craneStatusBase.Ready);
                //自动信号灯
                if (craneStatusBase.ControlMode == 4)
                {
                    refresh_Textbox_Light(light_CONTROL_MODE, 1);
                }
                else
                {
                    refresh_Textbox_Light(light_CONTROL_MODE, 0);
                }
                //控制模式
                txt_CONTROL_MODE.Text = craneStatusBase.CraneModeDesc();
                //请求指令信号灯
                refresh_Textbox_Light(light_ASK_PLAN, craneStatusBase.AskPlan);
                //x
                txt_XACT.Text = craneStatusBase.XAct.ToString("0,000");
                //y
                txt_YACT.Text = craneStatusBase.YAct.ToString("0,000");
                //z
                txt_ZACT.Text = craneStatusBase.ZAct.ToString("0,000");
                //夹钳温度
                //txt_CraneOrder.Text = craneStatusBase.COIL_TEMPERATURE.ToString() + "°";

                //有卷信号灯
                refresh_Textbox_Light(light_HAS_COIL, craneStatusBase.HasCoil);
                //行车状态
                txt_CRANE_STATUS.Text = craneStatusBase.CraneStatusDesc();
                //与行车通讯状态
                if (lbl_HeartBeat.Text == craneStatusBase.ReceiveTime.ToString() && communicate_PLC_OK == true)
                {
                    heatBeatCounter++;
                }
                if (lbl_HeartBeat.Text != craneStatusBase.ReceiveTime.ToString() && communicate_PLC_OK == true)
                {
                    heatBeatCounter = 0;
                }
                else if (lbl_HeartBeat.Text != craneStatusBase.ReceiveTime.ToString() && communicate_PLC_OK == false)
                {
                    heatBeatCounter = 0;
                    communicate_PLC_OK = true;
                    messagebox = 0;
                }

                if (heatBeatCounter >= 20 && communicate_PLC_OK == true)
                {
                    communicate_PLC_OK = false;
                }

                if (communicate_PLC_OK)
                {
                    lbl_HeartBeat.BackColor = Color.LightGreen;
                }
                else
                {
                    lbl_HeartBeat.BackColor = Color.Red;

                    messagebox += 1;
                    if (messagebox == 1)
                    {
                        MethodInvoker mi = new MethodInvoker(showMessageBox2);
                        BeginInvoke(mi);
                    }

                }
                if (craneStatusBase.WeightLoaded > 10000)
                {
                    timer3.Enabled = true;
                }
                else
                {
                    timer3.Enabled = false;
                    weightFlag = false;
                }
                if (craneStatusBase.WeightLoaded > 10000 && weightFlag == true && craneStatusBase.HasCoil == 0 && (txt_CONTROL_MODE.Text == "人工" || txt_CONTROL_MODE.Text == "遥控"))
                {
                    messagebox3 += 1;
                    if (messagebox3 == 1)
                    {
                        MethodInvoker mi3 = new MethodInvoker(showMessageBox3);
                        BeginInvoke(mi3);
                    }
                }
                //时间心跳
                lbl_HeartBeat.Text = craneStatusBase.ReceiveTime.ToString();
                //行车指令
                craneinfo.craneOrderInfo(craneStatusBase.CraneNO.ToString(), txt_CraneOrder, txt_CoilNo, txt_FromStock, txt_ToStock);

                string TagName = craneNO + "_ALARM_CURRENT";
                SetReady(TagName);
                readTags();
                string value = get_value_string(TagName).Trim();
                string[] sArray = value.Split(',');

                string TagNameWMS = craneNO + "_WMS_ALARM_CURRENT";
                SetReady(TagNameWMS);
                readTags();
                string valueWMS = get_value_string(TagNameWMS).Trim();
                string[] sArrayWMS = valueWMS.Split(',');

                listAlarm.Clear();
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    foreach (string i in sArray)
                    {
                        int values = Convert.ToInt32(i.ToString());
                        listAlarm.Add(values);
                    }
                }
                if (!String.IsNullOrEmpty(valueWMS.Trim()))
                {
                    foreach (string i in sArrayWMS)
                    {
                        int valuesWMS = Convert.ToInt32(i.ToString());
                        listAlarm.Add(valuesWMS);
                    }
                }

                //if (txt_CONTROL_MODE.Text == "等待" && txt_CRANE_STATUS.Text == "999")
                //if ((!String.IsNullOrEmpty(value.Trim()) || !String.IsNullOrEmpty(valueWMS.Trim())) && (hasValues == true) && (txt_CONTROL_MODE.Text == "等待" || txt_CONTROL_MODE.Text == "自动"))
                if ((!String.IsNullOrEmpty(value.Trim()) || !String.IsNullOrEmpty(valueWMS.Trim())) && (txt_CONTROL_MODE.Text == "等待" || txt_CONTROL_MODE.Text == "自动"))
                {
                    //btnShow.Visible = true;
                    //timer1.Enabled = true; 
                    //if (!flag)
                    //{
                    //    btnShow.BackColor = Color.Red;
                    //    flag = true;
                    //}
                    //else
                    //{
                    //    btnShow.BackColor = System.Drawing.SystemColors.Control;
                    //    flag = false;
                    //}


                    btnShow.Visible = true;
                    if (firstTimeShow == false)
                    {
                        NoDefineCraneAlarm(listAlarm, firstTimeShow);
                        firstTimeShow = true;
                    }
                    if (CraneAlarmGetValues(listAlarm))
                    {
                        btnShow.BackColor = Color.Red;
                        //if ((!String.IsNullOrEmpty(value.Trim()) || !String.IsNullOrEmpty(valueWMS.Trim())) && txt_CONTROL_MODE.Text == "自动")
                        //{
                        //    timer1.Enabled = true;
                        //}
                        //else
                        //{
                        //    timer1.Enabled = false;
                        //}
                    }
                    else
                    {
                        btnShow.BackColor = System.Drawing.SystemColors.Control;
                    }

                }
                else
                {
                    btnShow.BackColor = System.Drawing.SystemColors.Control;
                    firstTimeShow = false;
                    btnShow.Visible = true;
                    //timer1.Enabled = false;
                    //playesounderFlag = false;
                    //btnShow.Visible = false;
                }

                #region 行车故障音频
                listCrane.Clear();
                if (craneNO == "1_1" || craneNO == "1_2" || craneNO == "1_3" || craneNO == "1_4")
                {
                    listCrane.Add("1_1");
                    listCrane.Add("1_2");
                    listCrane.Add("1_3");
                    listCrane.Add("1_4");
                }
                //else if(craneNO == "6_4" || craneNO == "6_5" || craneNO == "6_6")
                //{
                //    listCrane.Add("6_4");
                //    listCrane.Add("6_5");
                //    listCrane.Add("6_6");
                //}
                //else if (craneNO == "6_7" || craneNO == "6_8" || craneNO == "6_9" || craneNO == "6_10")
                //{
                //    listCrane.Add("6_7");
                //    listCrane.Add("6_8");
                //    listCrane.Add("6_9");
                //    listCrane.Add("6_10");
                //}
                int craneAlarmCount = 0;
                foreach (string item in listCrane)
                {
                    string VoiceTagName = item + "_ALARM_CURRENT";
                    SetReady(VoiceTagName);
                    readTags();
                    string VoiceValue = get_value_string(VoiceTagName).Trim();
                    string[] VoiceArray = VoiceValue.Split(',');

                    string VoiceTagNameWMS = item + "_WMS_ALARM_CURRENT";
                    SetReady(VoiceTagNameWMS);
                    readTags();
                    string VoiceValueWMS = get_value_string(VoiceTagNameWMS).Trim();
                    string[] VoiceArrayWMS = VoiceValueWMS.Split(',');

                    string VoiceTagNameMode = item + "_autoMode";
                    SetReady(VoiceTagNameMode);
                    readTags();
                    string VoiceValueMode = get_value_string(VoiceTagNameMode).Trim();

                    listAlarm.Clear();
                    if (!String.IsNullOrEmpty(VoiceValue.Trim()))
                    {
                        foreach (string i in VoiceArray)
                        {
                            int values = Convert.ToInt32(i.ToString());
                            listAlarm.Add(values);
                        }
                    }
                    if (!String.IsNullOrEmpty(VoiceValueWMS.Trim()))
                    {
                        foreach (string i in VoiceArrayWMS)
                        {
                            int valuesWMS = Convert.ToInt32(i.ToString());
                            listAlarm.Add(valuesWMS);
                        }
                    }
                    if ((!String.IsNullOrEmpty(VoiceValue.Trim()) || !String.IsNullOrEmpty(VoiceValueWMS.Trim())) && (VoiceValueMode == "5" || VoiceValueMode == "4"))
                    {
                        if (CraneAlarmGetValues(listAlarm))
                        {
                            if (VoiceValueMode == "4")
                            {
                                craneAlarmCount++;
                            }
                        }
                    }
                }
                if (craneAlarmCount >= 1 && (craneNO == "1_1" || craneNO == "1_2" || craneNO == "1_3" || craneNO == "1_4"))
                {
                    timer1.Enabled = true;
                }
                else
                {
                    timer1.Enabled = false;
                }
                #endregion

                //if (craneStatusBase.CraneNO.ToString().Trim() == "1_4" || craneStatusBase.CraneNO.ToString().Trim() == "1_5" || craneStatusBase.CraneNO.ToString().Trim() == "1_6")
                //{
                //    btnStop.Visible = false;
                //    btnStop.Enabled = false;
                //    btnMode.Visible = false;
                //    btnMode.Enabled = false;
                //    btnShow.Visible = false;
                //    btnShow.Enabled = false;
                //}
                if (craneStatusBase.CraneNO.ToString().Trim() == "1_5" || craneStatusBase.CraneNO.ToString().Trim() == "1_6")
                {
                    btnStop.Visible = false;
                    btnStop.Enabled = false;
                    btnMode.Visible = false;
                    btnMode.Enabled = false;                    
                }
                btnShow.Visible = false;
                btnShow.Enabled = false;
            }
            catch (Exception ex)
            {
                LogManager.WriteProgramLog(ex.Message);
                LogManager.WriteProgramLog(ex.StackTrace);
            }
        }

        private void showMessageBox2()
        {
            box = new FormMessageBox();
            box.MesssageBoxFlag = 2;
            box.MesssageBoxInfo = "【" + CraneNO + "】行车心跳中断\r\n 人工或遥控起落卷时请做好库位记录！";
            box.ShowDialog();
        }
        private void showMessageBox3()
        {
            box = new FormMessageBox();
            box.MesssageBoxFlag = 3;
            box.MesssageBoxInfo = "【" + CraneNO + "】行车载荷异常\r\n 人工或遥控起落卷时请盘库，确认库位信息！";
            box.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PlaySoundDelegate dge = new PlaySoundDelegate(PlaySoundFuntion);
            dge.BeginInvoke("行车故障.wav", null, null);
        }

        private void PlaySoundFuntion(string fileName)
        {
            if (btnShow.Visible == true)
            {
                SoundEvtAgs e = new SoundEvtAgs();
                e.FileName = fileName;
                playSound(this, e);
            }
        }
        private void PlaySoundEvt(object sender, SoundEvtAgs e)
        {
            playesounder(e.FileName);
        }
        private void playesounder(String strWaveName)
        {
            try
            {
                System.Media.SoundPlayer player = new SoundPlayer();
                player.SoundLocation = System.Windows.Forms.Application.StartupPath + "\\" + strWaveName;
                player.Load();
                player.PlaySync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //将未定义的故障代码写入未定义表
        private void NoDefineCraneAlarm(List<int> list, bool firstTime)
        {
            string alarmTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string alarmClass = "";
            try
            {
                foreach (int item in list)
                {

                    string sql = @"SELECT * FROM UACS_CRANE_ALARM_CODE_DEFINE WHERE ALARM_CODE = " + item;
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        if (!rdr.Read())
                        {
                            if (firstTime == false)
                            {
                                string sqlInsert = string.Format("INSERT INTO UACS_CRANE_ALARM_CODE_NO_DEFINE(CRANE_NO, ALARM_CODE, ALARM_TIME) VALUES('{0}', '{1}', '{2}')", craneNO, item, alarmTime);
                                DB2Connect.DBHelper.ExecuteNonQuery(sqlInsert);
                            }
                        }
                    }
                }

            }
            catch (Exception er)
            {
                throw (er);
            }
        }

        //判断Tag值是否含有故障代码
        private bool CraneAlarmGetValues(List<int> list)
        {
            int index = 0;
            try
            {
                foreach (int item in list)
                {

                    string sql = @"SELECT * FROM UACS_CRANE_ALARM_CODE_DEFINE WHERE ALARM_CODE = " + item;
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        while (rdr.Read())
                        {
                            index++;
                        }
                    }
                }
            }

            catch (Exception er)
            {
                throw (er);
            }
            if (index >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        /// <param name="theTextBox"></param>
        /// <param name="theValue"></param>
        private static void refresh_Textbox_Light(Panel panel, long theValue)
        {
            try
            {
                if (theValue == 1)
                {
                    panel.BackColor = Color.LightGreen;
                }
                else
                {
                    panel.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 紧停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            SendShortCmd(craneNO, CraneStatusBase.SHORT_CMD_EMG_STOP);
            HMILogger.WriteLog("紧停", "紧停：" + craneNO, LogLevel.Warn, this.Text);
        }


        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMode_Click(object sender, EventArgs e)
        {
            if (frmModeSwitchover == null || frmModeSwitchover.IsDisposed)
            {
                frmModeSwitchover = new FrmModeSwitchover();
                frmModeSwitchover.Crane_No = craneNO;
                frmModeSwitchover.TagServiceName = TagServiceName;
                frmModeSwitchover.Show();
            }
            else
            {
                frmModeSwitchover.WindowState = FormWindowState.Normal;
                frmModeSwitchover.Activate();
            }
        }


        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="theCraneNO">行车号</param>
        /// <param name="cmdFlag">对应模式切换数值</param>
        private void SendShortCmd(string theCraneNO, long cmdFlag)
        {
            try
            {
                string messageBuffer = string.Empty;
                messageBuffer = theCraneNO + "," + cmdFlag.ToString();
                Baosight.iSuperframe.TagService.DataCollection<object> wirteDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
                wirteDatas[theCraneNO + "_DownLoadShortCommand"] = messageBuffer;
                tagDataProvider.SetData(theCraneNO + "_DownLoadShortCommand", messageBuffer);
            }
            catch (Exception ex)
            { }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            PopAlarmCurrent pop = new PopAlarmCurrent();
            pop.Crane_No = craneNO;
            pop.ShowDialog();

        }

        private string get_value_string(string tagName)
        {
            string theValue = string.Empty;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToString((valueObject));
            }
            catch
            {
                valueObject = null;
            }
            return theValue; ;
        }

        private string[] arrTagAdress;

        public void SetReady(string tagName)
        {
            try
            {
                List<string> lstAdress = new List<string>();
                lstAdress.Add(tagName);
                arrTagAdress = lstAdress.ToArray<string>();
            }
            catch (Exception er)
            {

            }
        }

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


        //用户功能限制
        private void getUserName()
        {
            if (!auth.GetUserName().Contains("admin") && !auth.GetUserName().Contains("ZYBG"))
            {
                //btnStop.Visible = false;
                btnMode.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                //Tag
                craneAlarm = getTagValues(CraneNO + "_" + CRANE_ALARM);
                //报警
                if (craneAlarm == 1)
                {
                    if (t_init == null)
                    {
                        t_init = new Thread(new ParameterizedThreadStart(InitShow));
                        t_init.Name = "初始化";
                        t_init.IsBackground = true;
                        t_init.Start((object)CraneNO);
                    }
                    //playMusic();
                }
                else
                {
                    //isStart = false;

                    if (Closedelegate != null)
                        Closedelegate();
                }


            }
            catch (Exception ex)
            {
                if (Closedelegate != null)
                {
                    if (t_init != null)
                        t_init.Join(100);
                    Closedelegate();
                }
                else
                {
                    if (t_init != null)
                        t_init.Abort();
                }
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                if (craneAlarm == 0)
                {
                    if (Closedelegate != null)
                    {
                        if (t_init != null)
                            t_init.Join(100);
                        Closedelegate();
                    }
                    if (t_init != null)
                    {
                        t_init.Abort();
                        t_init = null;
                    }
                }
            }
        }
        private void InitShow(object o)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new queryTimer_TickDelegate(InitShow), new object[] { o });
                return;
            }
            InitForm frm = new InitForm((string)o);
            this.Closedelegate += new UACSControls.Closedelegate(frm.Form1_Closedelegate);
            frm.ShowDialog(frm.Owner);

            //frm.Dispose();
            //tagDataProvider.SetData(CraneNO + "_" + CRANE_ALARM, "0");
        }
        public void getTagNameList()
        {
            try
            {
                string TagNameAlarm = CraneNO + "_" + CRANE_ALARM;
                SetReady(TagNameAlarm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int getTagValues(string tagName)
        {
            object valueObject;
            int theValue = 0;
            try
            {
                getTagNameList();
                readTags();
                valueObject = inDatas[tagName];
                theValue = Convert.ToInt32(valueObject);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(t + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);

            }
            return theValue;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            weightFlag = true;
        }
    }

    delegate void queryTimer_TickDelegate(object state);
    public delegate void Closedelegate();

    public delegate void PlaySoundHandler(object sender, SoundEvtAgs e);
    public delegate void PlaySoundDelegate(string fileName);
    public class SoundEvtAgs : System.EventArgs
    {
        private string fileName;
        public string FileName
        {
            set { fileName = value; }
            get { return fileName; }
        }
    }
}
