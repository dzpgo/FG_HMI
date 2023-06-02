using Baosight.ColdRolling.TcmControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
