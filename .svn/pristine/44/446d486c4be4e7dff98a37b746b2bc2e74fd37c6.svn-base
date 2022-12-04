using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.ColdRolling.TcmControls;
using Baosight.iSuperframe.Authorization.Interface;
using Baosight.iSuperframe.Common;

namespace UACSControls
{
    public partial class CoilUnitSaddle : UserControl
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
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

        public CoilUnitSaddle()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            CoilPictureInit();
            Load += Init;
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
            skidControl.Size = new Size(160, 115);
            skidControl.TextFont = new Font("微软雅黑", 12f);
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

        private string strCoil = string.Empty;
        private void 申请钢卷信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            strCoil = this.CoilId.Trim();
            if (auth.IsOpen("03 钢卷信息"))
            {
                auth.CloseForm("03 钢卷信息");

                if (strCoil.Count() > 0)
                {
                    auth.OpenForm("03 钢卷信息", strCoil);
                }
                else
                    auth.OpenForm("03 钢卷信息");
            }
            else
            {
                if (strCoil.Count() > 0)
                {
                    auth.OpenForm("03 钢卷信息", strCoil);
                }
                else
                    auth.OpenForm("03 钢卷信息");
            }
        }


        


    }
}
