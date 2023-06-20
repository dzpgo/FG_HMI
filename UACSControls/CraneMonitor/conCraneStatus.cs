using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UACSDAL;
using System.Threading;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using ParkClassLibrary;
using System.Media;
using UACSPopupForm;
using Baosight.iSuperframe.Authorization;
using static System.Net.Mime.MediaTypeNames;

namespace UACSControls
{
    /// <summary>
    /// 行车状态
    /// </summary>
    public partial class conCraneStatus : UserControl
    {
        #region 定义
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
        /// <summary>
        /// 故障代码
        /// </summary>
        private Dictionary<string, string> AlarmCodeDefine = new Dictionary<string, string>();
        /// <summary>
        /// 报警按钮闪烁
        /// </summary>
        private bool AlarmLamp= false;
        private bool AlarmTwinkle = false;
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
            LoadingCraneAlarmCodeDefine();
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
        long messagebox5 = 0;
        long messagebox6 = 0;
        long messagebox7 = 0;
        long messagebox8 = 0;
        /// <summary>
        /// 存解析出来的报警代码
        /// </summary>
        private List<int> listAlarm = new List<int>();
        private List<string> listCrane = new List<string>();
        private CraneStatusBase craneStatusBase = new CraneStatusBase();
        public delegate void RefreshControlInvoke(CraneStatusBase theCraneStatusBase);
        public string AX { get; set; }

        #endregion

        #region 刷新行车状态Tag值
        /// <summary>
        /// 刷新行车状态Tag值
        /// </summary>
        /// <param name="theCraneStatusBase">Tag值</param>
        public void RefreshControl(CraneStatusBase theCraneStatusBase)
        {
            try
            {
                auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
                getUserName();
                craneStatusBase = theCraneStatusBase;

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
                AX = txt_XACT.Text;
                //当前重量
                tb_Tag_WEIGHT_LOADED.Text = craneStatusBase.WeightLoaded.ToString();
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
                if (!string.IsNullOrEmpty(craneStatusBase.EV_PLAN_FINISH.ToString()) && craneStatusBase.EV_PLAN_FINISH == 1)
                {                   
                    if (craneStatusBase.CraneNO == "1")
                    {
                        messagebox5 += 1;
                        if (messagebox5 == 1)
                        {
                            this.Invoke(new MethodInvoker(() => this.OpenForm(craneStatusBase.CraneNO.ToString(), craneStatusBase.OrderID.ToString())));
                        }                      
                    }
                    else if (craneStatusBase.CraneNO == "2")
                    {
                        messagebox6 += 1;
                        if (messagebox6 == 1)
                        {
                            this.Invoke(new MethodInvoker(() => this.OpenForm(craneStatusBase.CraneNO.ToString(), craneStatusBase.OrderID.ToString())));
                        }
                    }
                    else if (craneStatusBase.CraneNO == "3")
                    {
                        messagebox7 += 1;
                        if (messagebox7 == 1)
                        {
                            this.Invoke(new MethodInvoker(() => this.OpenForm(craneStatusBase.CraneNO.ToString(), craneStatusBase.OrderID.ToString())));
                        }
                    }
                    else if ( craneStatusBase.CraneNO == "4")
                    {
                        messagebox8 += 1;
                        if (messagebox8 == 1)
                        {
                            this.Invoke(new MethodInvoker(() => this.OpenForm(craneStatusBase.CraneNO.ToString(), craneStatusBase.OrderID.ToString())));
                        }
                    }
                }
                else
                {
                    if (craneStatusBase.CraneNO == "1")
                    {
                        messagebox5 = 0;
                    }
                    else if (craneStatusBase.CraneNO == "2")
                    {
                        messagebox6 = 0;
                    }
                    else if (craneStatusBase.CraneNO == "3")
                    {
                        messagebox7 = 0;
                    }
                    else if (craneStatusBase.CraneNO == "4")
                    {
                        messagebox8 = 0;
                    }
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
                craneinfo.craneOrderInfo(craneStatusBase.CraneNO.ToString(), txt_CraneOrder, txt_CoilNo, txt_FromStock, txt_ToStock, tb_MAT_REQ_WGT, tb_MAT_ACT_WGT, tb_MAT_CUR_WGT, tb_ACT_WEIGHT, tb_CurrentStatus);
                #region 行车报警
                listAlarm.Clear();
                for (int i = 0; i <= 19; i++)
                {
                    var TagName_FaultCode = craneNO + "_FaultCode_" + "" + i + "";
                    SetReady(TagName_FaultCode);
                    readTags();
                    string data = get_value_string(TagName_FaultCode).Trim();
                    if (!string.IsNullOrEmpty(data) && !data.Equals("0"))
                        listAlarm.Add(Convert.ToInt32(data));
                }
                var craneAlarmCount = 0;
                if (listAlarm.Count > 0 && (txt_CONTROL_MODE.Text == "等待" || txt_CONTROL_MODE.Text == "自动" || txt_CONTROL_MODE.Text == "人工"))
                {
                    btnShow.Visible = true;
                    //if (firstTimeShow == false)
                    //{
                    //    NoDefineCraneAlarm(listAlarm, firstTimeShow);
                    //    firstTimeShow = true;
                    //}
                    //if (CraneAlarmGetValues(listAlarm))
                    if (GetCraneAlarmGetValues(listAlarm))
                    {
                        btnShow.BackColor = Color.Red;
                        craneAlarmCount++;
                        AlarmLamp = true;
                        timer4.Interval = 1000;
                        timer4.Enabled = true;
                    }
                    else
                    {
                        btnShow.BackColor = System.Drawing.SystemColors.Control;
                        AlarmLamp = true;
                        timer4.Enabled = false;
                    }
                }
                else
                {
                    timer4.Enabled = false;
                    btnShow.BackColor = System.Drawing.SystemColors.Control;
                    //firstTimeShow = false;
                    btnShow.Visible = true;
                }
                #endregion
                #region 行车故障音频
                if (craneAlarmCount >= 1 && (craneNO == "1" || craneNO == "2" || craneNO == "3" || craneNO == "4"))
                {
                    timer1.Interval = 7000;
                    timer1.Enabled = true;
                }
                else
                {
                    timer1.Enabled = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogManager.WriteProgramLog(ex.Message);
                LogManager.WriteProgramLog(ex.StackTrace);
            }
        }

        //将未定义的故障代码写入未定义表
        private void NoDefineCraneAlarm(List<int> list, bool firstTime)
        {
            string alarmTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                if (firstTime == false)
                {
                    foreach (int item in list)
                    {
                        string sql = @"SELECT * FROM UACS_CRANE_ALARM_CODE_DEFINE WHERE ALARM_CODE = " + item;
                        using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                        {
                            if (!rdr.Read())
                            {
                                string sqlInsert = string.Format("INSERT INTO UACS_CRANE_ALARM_CODE_DEFINE(ALARM_CODE) VALUES('{0}')", item);
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
                    string sql = @"SELECT ALARM_CODE,ALARM_INFO,ALARM_CLASS FROM UACS_CRANE_ALARM_CODE_DEFINE WHERE ALARM_CODE = " + item;
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
        /// 加载故障代码
        /// </summary>
        private void LoadingCraneAlarmCodeDefine()
        {
            try
            {
                AlarmCodeDefine.Clear();
                string sql = @"SELECT ALARM_CODE,ALARM_INFO,ALARM_CLASS FROM UACS_CRANE_ALARM_CODE_DEFINE ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        AlarmCodeDefine.Add(rdr["ALARM_CODE"].ToString(), rdr["ALARM_INFO"].ToString());
                    }
                }
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// 判断Tag值是否含有故障代码
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool GetCraneAlarmGetValues(List<int> list)
        {
            int index = 0;
            try
            {
                foreach (int item in list)
                {
                    if (AlarmCodeDefine.ContainsKey(item.ToString()))
                    {
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
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

        #endregion

        #region 错误弹窗 And ~

        /// <summary>
        /// 行车心跳中断 弹窗
        /// </summary>
        private void showMessageBox2()
        {
            box = new FormMessageBox();
            box.MesssageBoxFlag = 2;
            box.MesssageBoxInfo = "【" + CraneNO + "】行车心跳中断\r\n 人工或遥控起落卷时请做好库位记录！";
            box.ShowDialog();
        }

        /// <summary>
        /// 行车载荷异常 弹窗
        /// </summary>
        private void showMessageBox3()
        {
            box = new FormMessageBox();
            box.MesssageBoxFlag = 3;
            box.MesssageBoxInfo = "【" + CraneNO + "】行车载荷异常\r\n 人工或遥控起落卷时请盘库，确认库位信息！";
            box.ShowDialog();
        }
        /// <summary>
        /// 计划完成提示弹窗
        /// </summary>
        /// <param name="cranmeNo">行车号</param>
        /// <param name="orderNo">计划号</param>
        private void OpenForm(string cranmeNo, string orderNo)
        {
            // 检查窗体是否已经打开
            bool isOpen = false;
            foreach (System.Windows.Forms.Form form in System.Windows.Forms.Application.OpenForms)
            {
                if (form is FrmPlanCompletionBox myForm && myForm.CranmeNo == cranmeNo && myForm.OrderNo == orderNo)
                {
                    isOpen = true;
                    form.Activate();
                    break;
                }
            }

            // 如果窗体未打开，则打开它
            if (!isOpen)
            {
                FrmPlanCompletionBox myForm = new FrmPlanCompletionBox(cranmeNo, orderNo);
                myForm.Show();
            }
        }
        /// <summary>
        /// 计划已完成提醒
        /// </summary>
        private void showPlanCompletionBox(string cranmeNo)
        {
            // 检查窗体是否已经打开
            bool isOpen = false;
            foreach (System.Windows.Forms.Form form in System.Windows.Forms.Application.OpenForms)
            {
                if (form.GetType() == typeof(FrmPlanCompletionBox))
                {
                    isOpen = true;
                    form.Activate();
                    break;
                }
            }

            // 如果窗体未打开，则打开它
            if (!isOpen)
            {
                FrmPlanCompletionBox myForm = new FrmPlanCompletionBox();
                myForm.CranmeNo = cranmeNo;
                myForm.Show();
            }
        }

        /// <summary>
        /// 行车故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #endregion

        #region 点击事件

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
        /// <summary>
        /// 行车报警 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, EventArgs e)
        {
            PopAlarmCurrent pop = new PopAlarmCurrent();
            pop.Crane_No = craneNO;
            pop.ShowDialog();

        }
        #endregion

        #region 处理

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

        /// <summary>
        /// 报警按钮闪烁  定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (AlarmLamp)
            {
                if (AlarmTwinkle)
                {
                    btnShow.BackColor = Color.Red;
                    AlarmTwinkle =false;
                }
                else
                {
                    btnShow.BackColor = System.Drawing.SystemColors.Control;
                    AlarmTwinkle = true;
                }
            }
        }
        #endregion

    }

    #region 文件

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

    #endregion
}
