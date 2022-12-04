using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.ColdRolling.TcmControls;
using MODEL_OF_REPOSITORIES;

namespace CONTROLS_OF_REPOSITORIES
{
    public partial class CoilPicture : UserControl
    {
        /// <summary>
        /// 钢卷标记，0和-1灰色  1-5有颜色 其他无卷
        /// </summary>
       public int CoilStatus
        {
            get { return skidControl.CoilStatus; }
            set { skidControl.CoilStatus=value; }
        }
        /// <summary>
        /// 鞍座名
        /// </summary>
        public string SkidName
        {
            get { return skidControl.Name; }
            set { skidControl.Name = value; }
        } 
        /// <summary>
        /// 卷号，显示在框中
        /// </summary>
        public string CoilId
        {
            get { return skidControl.CoilIdText; }
            set { skidControl.CoilIdText = value; }
        }
        /// <summary>
        /// 显示的位置名称
        /// </summary>
        public string PosName
        {
            get { return skidControl.PosName; }
            set { skidControl.PosName = value; }
        }
        /// <summary>
        /// 位置号，编程使用
        /// </summary>
        public int PosNo
        {
            get { return skidControl.PosNo; }
            set { skidControl.PosNo = value; }
        }
        /// <summary>
        /// 字体
        /// </summary>
        public Font TextFont
        {
            get { return skidControl.TextFont; }
            set { skidControl.TextFont = value; }
        }
        /// <summary>
        /// 控件方向,默认水平布局 （ Baosight.ColdRolling.TcmControl.Direct）
        /// </summary>
        public Baosight.ColdRolling.TcmControl.Direct Director
        {
            get { return skidControl.Director; }
            set { skidControl.Director = value; }
        }

        /// <summary>
        /// 上按钮
        /// </summary>
        public bool UpEnable
        {
            get { return skidControl.UpEnable; }
            set { skidControl.UpEnable = value; }
        }
        public bool UpVisiable
        {
            get { return skidControl.UpVisiable; }
            set { skidControl.UpVisiable = value; }
        }
        /// <summary>
        /// 下按钮
        /// </summary>
        public bool DownEnable
        {
            get { return skidControl.DownEnable; }
            set { skidControl.DownEnable = value; }
        }
        public bool DownVisiable
        {
            get { return skidControl.DownVisiable; }
            set { skidControl.DownVisiable = value; }
        }

     /// <summary>
     /// 尺寸大小
     /// </summary>
        public Size SkidSize
        {
            get { return skidControl.Size; }
            set { skidControl.Size = value; }
        }

        /// <summary>
        /// 卷号框背景色
        /// </summary>
        public Color CoilBackColor
        {
            get { return skidControl.PosBackColor; }
            set { skidControl.PosBackColor = value; }
        }
        /// <summary>
        /// 卷号框前景色
        /// </summary>
        public Color CoilFontColor
        {
            get { return skidControl.CoilFontColor; }
            set { skidControl.CoilFontColor = value; }
        }
        /// <summary>
        /// 钢卷号长度
        /// </summary>
        public int CoilLength
        {
            get { return skidControl.CoilLength; }
            set { skidControl.CoilLength = value; }
        }
        /// <summary>
        /// 上按钮图片
        /// </summary>
        public Image ButtonDImage
        {
            get { return skidControl.ButtonDImage; }
            set { skidControl.ButtonDImage = value; }
        }
        /// <summary>
        /// 下按钮图片
        /// </summary>
        public Image ButtonUImage
        {
            get { return skidControl.ButtonUImage; }
            set { skidControl.ButtonUImage = value; }
        }

        public CoilPicture()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            CoilPictureInit();
            Load += Init;

            AuthorityManagement auth = new AuthorityManagement();
            if (auth.isUserJudgeEqual("MANUAL_PACKING"))
            {
                this.ContextMenuStrip = null;
            }
            


        }
         Baosight.ColdRolling.TcmControls.SkidControl skidControl = new Baosight.ColdRolling.TcmControls.SkidControl();
         protected override void WndProc(ref Message m)
         {

             if (m.Msg == 0x0014) // 禁掉清除背景消息

                 return;

             base.WndProc(ref m);

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
        private void Init(object sender, EventArgs e)
        {
            Controls.Add(skidControl);
        }
        void CoilPictureInit()
        {
            skidControl.Anchor = AnchorStyles.None;
            //skidControl.ButtonDImage=coilBtnImg.greenPoint;
            //skidControl.ButtonUImage = CoilPictureImg.unlock;
           
            skidControl.Margin = new Padding(14, 9, 14, 9);
            skidControl.Size = new Size(215, 115);
            skidControl.TextFont = new Font("微软雅黑", 15f);
            skidControl.BtnClearVisiable = false;
            //  skidControl.ButtonDImage = 
            //skidControl.ButtonDImage = Image(resource);
            //skidControl.ButtonUImage = Resource.right;
            skidControl.CaptionMarginSpace = 2;
            skidControl.CheckType = Baosight.ColdRolling.TcmControl.CheckType.Status;
            skidControl.CoilBackColor = Color.White; 
            skidControl.CoilFontColor = Color.Black;
            skidControl.CoilLength = 16;
            skidControl.ColSub = Color.White;     //颜色设定
            skidControl.ColMain = Color.White; //卷颜色设定  
        
            skidControl.ColSch = Color.Gray;    //颜色设定 -1
            skidControl.ColLoad = Color.Black;  //颜色设定 0
            skidControl.ColConfirm = Color.OrangeRed;
            skidControl.ColBook = Color.Blue;
            
            skidControl.ColNoWeight = Color.LightYellow;
            skidControl.ColReject = Color.Green;
            skidControl.ColWeight = Color.LightGreen;
            skidControl.ColSend = Color.Red;
           
            skidControl.Director = Baosight.ColdRolling.TcmControl.Direct.Horizontal;
            skidControl.DownEnable = true;
            skidControl.DownVisiable = true;
            skidControl.InnerId = 0;
            skidControl.IRate = 0.9D;
            skidControl.IsVisiblePicture = false;
            skidControl.Name = "skidControl";
            skidControl.PosBackColor = Color.SkyBlue;
            skidControl.PosFontColor = Color.Black;
            skidControl.PosNo = 1;
            skidControl.poxImage = null;
            skidControl.RadiusInner = 8;
            skidControl.SkType = 0;
            skidControl.UpEnable = true;
            skidControl.UpVisiable = true;
            skidControl.MoveUpEvent += new EventHandler<ClickEventAgrs>(this.skidControl_MoveUpEvent);
            skidControl.MoveDownEvent += new EventHandler<ClickEventAgrs>(this.skidControl_MoveDownEvent);


        }
        
        public delegate void SkidEventHandl();
        /// <summary>
        /// 下按钮事件
        /// </summary>
        public event SkidEventHandl SkidDownEvent;
        /// <summary>
        /// 上按钮事件
        /// </summary>
        public event SkidEventHandl SkidUpEvent;
        //抛事件
        void skidControl_MoveUpEvent(object sr, ClickEventAgrs e)
        {
            if (SkidUpEvent != null)
            {
                SkidUpEvent();
            }
        }

        void skidControl_MoveDownEvent(object sr, ClickEventAgrs e)
        {
            if (SkidDownEvent != null)
                SkidDownEvent();
        }

        private string unit_no;

        public string Unit_no
        {
            get { return unit_no; }
            set { unit_no = value; }
        }

        private string saddle_no;

        public string Saddle_no
        {
            get { return saddle_no; }
            set { saddle_no = value; }
        }


        private void 包装ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.PosName != "")
            {
                string saddleNo = this.PosName.Substring(0, 10);
                bool flag = OffinePackingSaddleInBay.UpPackingSaddleStatus(
               (int)OffinePackingSaddleInBay.AreaStatus.Working, saddleNo);
                if (flag)
                    MessageBox.Show("成功");
                else
                    MessageBox.Show("失败");

            }
        }

        private void 吊离ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.PosName != "")
            {
                string saddleNo = this.PosName.Substring(0, 10);
               bool flag =   OffinePackingSaddleInBay.UpPackingSaddleStatus(
               (int)OffinePackingSaddleInBay.AreaStatus.OutCoil, saddleNo);

               if (flag)
                   MessageBox.Show("成功");
                else
                   MessageBox.Show("失败");

            }
        }

        private void 包装纸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPLATE_WIDTH width = new FrmPLATE_WIDTH();
            width.SaddleNo = this.PosName.Substring(0, 10);
            width.ShowDialog();
        }

        private void 卷信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOffLinePackingCoilMessage coilMssage = new FrmOffLinePackingCoilMessage();
            coilMssage.MyCoil = this.CoilId.Trim();
            coilMssage.ShowDialog();
        }

        private void 地板高度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = OffinePackingSaddleInBay.GetPackingSaddleFloor(this.PosName.Substring(0, 10));

            if (str == null)
            {
                MessageBox.Show("鞍座地板查询不到");
            }
            else
            {
                string[] sArray = str.Split('|');
                if (sArray.Count() == 4)
                {
                    string floor = sArray[0];
                    string oper_flag = sArray[1];
                    string crane_mode = sArray[2];
                    string crane_no = sArray[3];

                    MessageBox.Show(
                        "鞍座号：" + this.PosName.Substring(0, 10) + "\r" + 
                        "地板高度：" + floor + "\r" + 
                        "最近动作：" + oper_flag + "\r" +
                        "行车模式：" + crane_mode + "\r" +
                        "行车号：" + crane_no + "\r" 
                        );
                }
            }
           
        }

        private void 待包卷吊入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAwaitCoilStock awaitCoil = new FormAwaitCoilStock();
            awaitCoil.FromStock = this.PosName.Substring(0, 10);
            awaitCoil.ShowDialog();
        }

        private void 卷标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetPackingCoilType coiltype = new FrmSetPackingCoilType(this.PosName.Substring(0, 10));
            coiltype.ShowDialog();


        }

        private void 吊入信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = OffinePackingSaddleInBay.GetPackingSaddleInfo(this.PosName.Substring(0, 10));

            if (str == null)
            {
                MessageBox.Show("鞍座信息查询不到");
            }
            else
            {
                string[] sArray = str.Split('|');
                if (sArray.Count() == 3)
                {
                    string HUGE_COIL = sArray[0];
                    string FORM_STOCK = sArray[1];
                    string COIL_TYPE = sArray[2];

                    MessageBox.Show(
                       "鞍座号：" + this.PosName.Substring(0, 10) + "\r" +
                       "大卷标记：" + HUGE_COIL + "\r" +
                       "钢卷类别：" + COIL_TYPE + "\r" +
                       "待吊入钢卷库位：" + FORM_STOCK + "\r" 
                       );
                }
            }
        }

        private void 正常吊入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.PosName != "")
            {
                string saddleNo = this.PosName.Substring(0, 10);

                if (OffinePackingSaddleInBay.isEmptyStock(saddleNo))
                {
                    bool flag = OffinePackingSaddleInBay.UpPackingSaddleStatus(
                     (int)OffinePackingSaddleInBay.AreaStatus.InCoil, saddleNo);
                    if (flag)
                        MessageBox.Show("吊入成功");
                    else
                        MessageBox.Show("吊入失败");
                }
                else
                {
                    MessageBox.Show(saddleNo + "该库位状态异常;若确认库位无卷，请强制吊入！！！");
                }
               

            }
        }

        private void 强制吊入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.PosName != "")
            {
                string saddleNo = this.PosName.Substring(0, 10);

                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要对鞍座" + saddleNo + "进行强制吊入吗", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    if (OffinePackingSaddleInBay.setEmptyStock(saddleNo))
                    {
                        bool flag = OffinePackingSaddleInBay.UpPackingSaddleStatus(
                        (int)OffinePackingSaddleInBay.AreaStatus.InCoil, saddleNo);
                        if (flag)
                            MessageBox.Show("强制吊入成功");
                        else
                            MessageBox.Show("强制吊入失败");
                    }
                   
                }
               

            }
        }
    }
}
