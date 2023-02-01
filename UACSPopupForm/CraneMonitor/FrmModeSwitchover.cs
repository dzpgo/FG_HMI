using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using ParkClassLibrary;

namespace UACSPopupForm
{
    public partial class FrmModeSwitchover : Form
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        /// <summary>
        /// 行车模式切换
        /// </summary>
        public const string tag_CraneMode = "DownLoadShortCommand"; 
        /// <summary>
        /// 行车号
        /// </summary>
        public string Crane_No { get; set; }

        /// <summary>
        /// tag
        /// </summary>
        public string TagServiceName { get; set; }
        public FrmModeSwitchover()
        {
            InitializeComponent();
            this.button1.MouseEnter += btnInCoil_MouseEnter;
            this.button2.MouseEnter += btnInCoil_MouseEnter;
            this.button3.MouseEnter += btnInCoil_MouseEnter;
            this.button4.MouseEnter += btnInCoil_MouseEnter;
            this.button5.MouseEnter += btnInCoil_MouseEnter;
            this.button1.MouseLeave += btnInCoil_MouseLeave;
            this.button2.MouseLeave += btnInCoil_MouseLeave;
            this.button3.MouseLeave += btnInCoil_MouseLeave;
            this.button4.MouseLeave += btnInCoil_MouseLeave;
            this.button5.MouseLeave += btnInCoil_MouseLeave;
            this.Load += FrmModeSwitchover_Load;      
        }
        void btnInCoil_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.SkyBlue;
        }

        void btnInCoil_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.CornflowerBlue;
        }

        void FrmModeSwitchover_Load(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            tagDataProvider.ServiceName = TagServiceName;
            label1.Text = Crane_No + "模式切换";
        }



        /// <summary>
        /// 手动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
           SendShortCmd(Crane_No, CraneStatusBase.SHORT_CMD_CANCEL_COMPUTER_AUTO);
           ParkClassLibrary.HMILogger.WriteLog("手动", "手动：" + Crane_No, LogLevel.Info, this.Text);
        }

        //防止频繁操作点击复位按钮
        bool isClickReset = false;
        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (isClickReset)
            {
                MessageBox.Show("已点击过复位，请两秒钟后再点");
                return;
            }
            SendShortCmd(Crane_No, CraneStatusBase.SHORT_CMD_RESET);
            HMILogger.WriteLog("复位", "复位：" + Crane_No, LogLevel.Info, this.Text);
            isClickReset = true;
            timer1.Enabled = true;
            isPopupMessage = false;

            
        }
        //防止频繁操作点击自动按钮
        bool isClickAuto = false;
        //防止弹出信息关闭画面
        bool isPopupMessage = false;
        /// <summary>
        /// 自动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (isClickAuto)
            {
                MessageBox.Show("已点击过自动，请两秒钟后再点");
                return;
            }      
            if (DialogResult.OK == MessageBox.Show("注意,行车是否要切自动？", "Warning", MessageBoxButtons.OKCancel))
            {
                SendShortCmd(Crane_No, CraneStatusBase.SHORT_CMD_ASK_COMPUTER_AUTO);
                HMILogger.WriteLog("自动", "自动：" + Crane_No, LogLevel.Info, this.Text);
                isClickAuto = true;
                timer2.Enabled = true;
            }
            isPopupMessage = false;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isClickReset)
            {
                isClickReset = false;
            }
            timer1.Enabled = false;
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isClickAuto)
            {
                isClickAuto = false;
            }
            timer2.Enabled = false;
        }


        /// <summary>
        /// 请求停车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            SendShortCmd(Crane_No, CraneStatusBase.SHORT_CMD_NORMAL_STOP);
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
                //DownLoadShortCommand
                Baosight.iSuperframe.TagService.DataCollection<object> wirteDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
                wirteDatas[theCraneNO + "_" + tag_CraneMode] = messageBuffer;
                tagDataProvider.SetData(theCraneNO + "_" + tag_CraneMode, messageBuffer);
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 取消窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmModeSwitchover_Deactivate(object sender, EventArgs e)
        {
            if (!isPopupMessage)
            {
                this.Close();
            }        
        }

        private void FrmModeSwitchover_Load_1(object sender, EventArgs e)
        {

        }
    }
}
