using Baosight.ColdRolling.TcmControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls.CraneMonitor
{
    public enum CheckStyle
    {
        style1 = 0,
        style2 = 1,
        style3 = 2,
        style4 = 3,
        style5 = 4,
        style6 = 5
    };
    public partial class conSwitchButton : UserControl
    {
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        bool isCheck = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked
        {
            set { isCheck = value; this.Invalidate(); }
            get { return isCheck; }

        }
        CheckStyle checkStyle = CheckStyle.style1;
        public conSwitchButton()
        {
            InitializeComponent();
            //设置Style支持透明背景色并且双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(87, 27);
        }

        /// <summary>
        /// 样式
        /// </summary>
        public CheckStyle CheckStyleX
        {
            set { checkStyle = value; this.Invalidate(); }
            get { return checkStyle; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bitMapOn = null;
            Bitmap bitMapOff = null;

            if (checkStyle == CheckStyle.style1)
            {
                bitMapOn = Properties.Resources.btncheckon;
                bitMapOff = Properties.Resources.btncheckoff;
            }
            else if (checkStyle == CheckStyle.style2)
            {
                bitMapOn = Properties.Resources.btncheckon;
                bitMapOff = Properties.Resources.btncheckoff;
            }
            else if (checkStyle == CheckStyle.style3)
            {
                bitMapOn = Properties.Resources.btncheckon;
                bitMapOff = Properties.Resources.btncheckoff;
            }
            else if (checkStyle == CheckStyle.style4)
            {
                bitMapOn = Properties.Resources.btncheckon;
                bitMapOff = Properties.Resources.btncheckoff;
            }
            else if (checkStyle == CheckStyle.style5)
            {
                bitMapOn = Properties.Resources.btncheckon;
                bitMapOff = Properties.Resources.btncheckoff;
            }
            else if (checkStyle == CheckStyle.style6)
            {
                bitMapOn = Properties.Resources.btncheckon;
                bitMapOff = Properties.Resources.btncheckoff;
            }

            Graphics g = e.Graphics;
            Rectangle rec = new Rectangle(0, 0, this.Size.Width, this.Size.Height);

            if (isCheck)
            {
                g.DrawImage(bitMapOn, rec);
            }
            else
            {
                g.DrawImage(bitMapOff, rec);
            }
        }

        private void conSwitchButton_Click(object sender, EventArgs e)
        {
            isCheck = !isCheck;
            this.Invalidate();
        }

        #region 主监控按钮

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
        //private long areaReserve = 0;
        //public long AreaReserve { get => areaReserve; set => areaReserve = value; }
        public long? AreaReserve = null;
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

        public delegate void RefreshControlInvoke(TrafficLightBase theParkingBase, Dictionary<string, string> safePlcStatus, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel);
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
        public void RefreshControl(TrafficLightBase theTrafficLightBase, Dictionary<string, string> theSafePlcStatusList, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel)
        {
            AreaReserve = theTrafficLightBase.AreaReserve;
            this.Width = 55;
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
                //location_Y = (Convert.ToDouble(theTrafficLightBase.Y_End) - 700) * yScale;
                location_Y = (Convert.ToDouble(theTrafficLightBase.Y_Start) - 2600) * yScale;
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

            var areaNo = theTrafficLightBase.AreaNo.Split('A');
            var areaSafe = "AreaSafe" + areaNo[1];
            //var gratStatus = theTrafficLightBase.AreaGratStatus;
            var gratStatus = 0;
            if (theSafePlcStatusList.ContainsKey(areaSafe))
            {
                gratStatus = Convert.ToInt32(theSafePlcStatusList[areaSafe].ToString());
            }
            this.Checked = gratStatus == 1 ? true : false;

            this.Invalidate();

        }

        #endregion




    }
}
