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
using UACS;
using UACSPopupForm.CraneMonitor;
using UACSDAL.CraneMonitor;
using System.Drawing.Drawing2D;

namespace UACSControls
{
    public partial class conTrafficLight2 : UserControl
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth = null;
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        /// <summary>
        /// 中心颜色
        /// </summary>
        public Color CenterColor = Color.White;
        /// <summary>
        /// 外围颜色
        /// </summary>
        public Color SurroundColor = Color.Red;
        /// <summary>
        /// 灭灯颜色
        /// </summary>
        public Color DarkColor = Color.Green;  // Color.Gray;
        Color CurrentColor = Color.Gray; //灰色
        public bool ChangeColor = false;
        public conTrafficLight2()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲   
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Paint += Ovalshape_Paint;
            //this.BackColor = Color.Transparent;
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ovalshape_Paint(object sender, PaintEventArgs e)
        {
            //重绘时 画出中心放射颜色的圆形
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Size.Width, this.Size.Height);
            PathGradientBrush pthGrBrush = new PathGradientBrush(path);
            pthGrBrush.CenterColor = CenterColor;
            Color[] colors = { CurrentColor };
            pthGrBrush.SurroundColors = colors;
            e.Graphics.FillEllipse(pthGrBrush, 0, 0, this.Size.Width, this.Size.Height);
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
        private Label lblCraneNo = new Label();
        private bool isCraneLbl = false;
        private CraneStatusBase cranePLCStatusBase = new CraneStatusBase();
        public TrafficLightBase craneTrafficLightBase = new TrafficLightBase();

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

        private string areaNO = string.Empty;
        public string AreaNO { get => areaNO; set => areaNO = value; }
        private string craneNO = string.Empty;
        //step2
        public string CraneNO
        {
            get { return craneNO; }
            set
            {
                craneNO = value;
                //this.ContextMenuStrip = contextMenuStrip1;
            }
        }



        public delegate void RefreshControlInvoke(TrafficLightBase theParkingBase, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel);
        /// <summary>
        /// 刷新控件
        /// </summary>
        /// <param name="theParkingBase"></param>
        /// <param name="baySpaceX"></param>
        /// <param name="baySpaceY"></param>
        /// <param name="panelWidth"></param>
        /// <param name="panelHeight"></param>
        /// <param name="xAxisRight"></param>
        /// <param name="yAxisDown"></param>
        public void RefreshControl(TrafficLightBase theTrafficLightBase, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel)
        {
            this.Width = 25;
            this.Height = 25;
            //计算X方向上的比例关系
            double xScale = Convert.ToDouble(panelWidth) / Convert.ToDouble(baySpaceX);
            double location_X = 0;
            if (xAxisRight == true)
            {
                //location_X = Convert.ToDouble(theTrafficLightBase.X_Start) * xScale;
                var x1 = Convert.ToDouble(theTrafficLightBase.X_Start) * xScale;
                var x2 = Convert.ToDouble(theTrafficLightBase.X_End) * xScale;
                var x3 = x1 + this.Width;
                var x4 = x2 - x3;
                var x5 = x4 / 2;
                location_X = x1 + x5;
            }
            else
            {
                location_X = Convert.ToDouble(baySpaceX - (theTrafficLightBase.X_End)) * xScale;
            }
            //计算y方向上的比例关系
            double yScale = Convert.ToDouble(panelHeight) / Convert.ToDouble(baySpaceY);

            double location_Y = 0;
            if (yAxisDown == true)
            {
                //location_Y = Convert.ToDouble(theTrafficLightBase.Y_Start) * yScale;
                //location_Y = (Convert.ToDouble(theTrafficLightBase.Y_End) - 2000) * yScale;
                location_Y = (Convert.ToDouble(theTrafficLightBase.Y_End) - 700) * yScale;
            }
            else
            {
                location_Y = Convert.ToDouble(baySpaceY - (theTrafficLightBase.Y_End)) * yScale;
            }
            if (location_Y < 0)
            {
                location_Y = 0;
            }
            this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y));
            ChangeColor = false;
            if (theTrafficLightBase.AreaNo.Contains("A"))  //跨区
                ChangeColor = theTrafficLightBase.AreaReserve == 1 ? true : false;
            if(theTrafficLightBase.AreaNo.Contains("T"))   //工位
                ChangeColor = theTrafficLightBase.AreaReserveCubicle == 1 ? true : false;
            CurrentColor = ChangeColor == true ? SurroundColor : DarkColor;
            this.Invalidate();

        }
    }
}
