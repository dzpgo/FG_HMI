using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using System.Threading;

namespace UACSControls
{
    public partial class conCraneDisplay : UserControl
    {
        public conCraneDisplay()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲           
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
        //private long craneWith = 0;
        private  Label lblCraneNo = new Label();

        private bool isCraneLbl = false;

        private CraneStatusBase cranePLCStatusBase = new CraneStatusBase();


        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth = null;

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


        private string craneNO = null;
        //step2
        public string CraneNO
        {
            get { return craneNO; }
            set
            {
                craneNO = value;
                this.ContextMenuStrip = contextMenuStrip1;
                if (craneNO.Contains("1"))
                {
                    设置避让ToolStripMenuItem.Visible = true;
                    ToolStrip_YardToTard.Visible = true;
                    确定ToolStripMenuItem.Visible = true;
                    if (craneNO == "7_3" || craneNO == "7_9")
                    {
                        //设置避让ToolStripMenuItem.Visible = false;
                        确定ToolStripMenuItem.Visible = false;
                    }
                }
                else
                {
                    //设置避让ToolStripMenuItem.Visible = false;
                    确定ToolStripMenuItem.Visible = false;
                    ToolStrip_YardToTard.Visible = false;
                }
               
            }
        }

        //step3
        public delegate void RefreshControlInvoke(CraneStatusBase _cranePLCStatusBase, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, long craneWith, Panel panel);

        public void RefreshControl(CraneStatusBase _cranePLCStatusBase, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown,long craneWith,Panel panel)
        {
            try
            {
                cranePLCStatusBase = _cranePLCStatusBase;
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
                    X = (Convert.ToDouble(baySpaceX) - Convert.ToDouble(_cranePLCStatusBase.XAct)) * xScale;
                    location_Crane_X = Convert.ToDouble(_cranePLCStatusBase.XAct + craneWith / 2) * xScale;
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
                    this.panelCrab.BackgroundImage = global::UACSControls.Resource1.imgCarCoil;
                }

                //if (!isCraneLbl)
                //{
                //     lblCraneNo.Name = cranePLCStatusBase.CraneNO;
                //     lblCraneNo.Text = cranePLCStatusBase.CraneNO;
                //     lblCraneNo.BackColor = Color.Transparent;
                //     lblCraneNo.Width = 40;
                //     panel.Controls.Add(lblCraneNo);
                //     isCraneLbl = true;
                //}

                //lblCraneNo.Location = new Point(Convert.ToInt32(location_Crane_X + this.Width), Convert.ToInt32(location_Crane_Y + this.Height - 20));
                //lblCraneNo.BringToFront();
                //if (isShowCrane)
                //{
                    
                //}

                this.BringToFront();

                //LogManager.WriteProgramLog("行车号：" + cranePLCStatusBase.CraneNO);
                //LogManager.WriteProgramLog("width：" + this.Width);
                //LogManager.WriteProgramLog("heiht：" + this.Height);

            }
            catch (Exception ex)
            {
                LogManager.WriteProgramLog(ex.Message);
                LogManager.WriteProgramLog(ex.StackTrace);
            }
        }




        /// <summary>
        /// 通过控件名获取控件
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        private Control GetPbControl(string strName)
        {
            string pbName = strName;
            return GetControl(this, pbName);
        }

        /// <summary>
        /// 通过控件名获取控件
        /// </summary>
        /// <param name="ct">控件所在的容器或者窗体</param>
        /// <param name="name">需要查找的控件名</param>
        /// <returns></returns>
        public static Control GetControl(Control ct, string name)
        {
            Control[] ctls = ct.Controls.Find(name, false);
            if (ctls.Length > 0)
            {
                return ctls[0];
            }
            else
            {
                return null;
            }
        }

        private void ToolStrip_YardToTard_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(threadPro);//创建新线程
            thread2.Start();

           
        }

        public void threadPro()
        {
             //MethodInvoker MethInvo = new MethodInvoker(ShowForm);
             //BeginInvoke(MethInvo);
        }
        //public void ShowForm()
        //{
        //    FrmYardToYardRequest yardtoyard = new FrmYardToYardRequest();
        //    yardtoyard.CraneNo = craneNO;
        //    yardtoyard.ShowDialog();
        //}

        private void panelCrane_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawString(cranePLCStatusBase.CraneNO.ToString(), 
                new Font("微软雅黑", 10, FontStyle.Bold), 
                Brushes.White, new Point(5, this.Height - 20));

        }

        private void ToolStrip_DelCraneOrder_Click(object sender, EventArgs e)
        {
            if (cranePLCStatusBase.HasCoil == 1)
            {
                MessageBox.Show("行车有卷状态禁止清除指令");
                return;
            }

            DialogResult ret = MessageBox.Show("确定要清空" + craneNO + "行车的指令吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (ret == DialogResult.Cancel)
                return;

            if (CreateManuOrder.isDelCraneOrder(craneNO))
            {
                MessageBox.Show(craneNO + "指令已清空");
            }
            else
            {
                MessageBox.Show(craneNO + "指令清空失败");
            }


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
                        case "1_4":
                            bayno = "Z11-Z12";
                            break;
                        
                    }

                    if (bayno != null)
                    {
                        if (auth.IsOpen("行车指令配置"))
                        {
                            auth.CloseForm("行车指令配置");

                            auth.OpenForm("行车指令配置", true, bayno, craneNO);
                        }
                        else
                        {
                            auth.OpenForm("行车指令配置", true, bayno, craneNO);
                        }
                    }
                }
            }
        }

        //设置避让
        private void 确定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //状态  立即
            string immediately = "RIGHT_NOW";
            //状态  关后
            string afterJob = "AFTER_JOB";
            //方向  正向
            string x_inc = "X_INC";
            //方向  负向
            string x_des = "X_DES";

            string error = null;
            if (craneNO == "7_2")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_3", "271059", x_inc, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");                  
            }
            if (craneNO == "7_1")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_2", "320084", x_inc, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");
            }
            if (craneNO == "7_0")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_1", "390792", x_inc, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");
            }
            if (craneNO == "7_5")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_6", "310386", x_inc, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");
            }
            if (craneNO == "7_4")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_5", "449278", x_inc, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");
            }
            if (craneNO == "7_7")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_8", "449884", x_inc, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");
            }
            if (craneNO == "7_8")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_9", "312575", x_inc, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");
            }
            if (craneNO == "7_6")
            {
                if (CreateManuOrder.SetCraneEvadeRequest(craneNO, "7_6", "80908", x_des, immediately, out error))
                    MessageBox.Show(craneNO + "避让已经创建成功");
            }

            

            if (error != null)
            {
                MessageBox.Show(error);
            }



        }
        //取消避让
        private void 取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string error = null;
            if (CreateManuOrder.ClearCraneEvadeRequest(craneNO,out error))
            {
                MessageBox.Show(craneNO + "避让指令清除成功");
            }
            else
            {
                MessageBox.Show(error);
            }
        }
    }
}
