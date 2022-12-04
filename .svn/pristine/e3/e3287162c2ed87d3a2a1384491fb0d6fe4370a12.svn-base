using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Baosight.iSuperframe.Forms;

namespace UACSView
{
    public partial class FrmUACSMainMonitor : FormBase // Form
    {
        public FrmUACSMainMonitor()
        {
            InitializeComponent();
            this.Load += FrmUACSMainMonitor_Load;
            this.Shown += FrmUACSMainMonitor_Shown;

            this.Activated += FrmUACSMainMonitor_Activated;
            this.Deactivate += FrmUACSMainMonitor_Deactivate;
            this.FormClosing += FrmUACSMainMonitor_FormClosing;
        }

        void FrmUACSMainMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            uacsYardMonitor1.MonitorDispose();        
        }
    
        void FrmUACSMainMonitor_Activated(object sender, EventArgs e)
        {
            uacsYardMonitor1.StopReflesh = false;
        }

        void FrmUACSMainMonitor_Deactivate(object sender, EventArgs e)
        {
            uacsYardMonitor1.StopReflesh = true;
        }

        void FrmUACSMainMonitor_Shown(object sender, EventArgs e)
        {
            uacsYardMonitor1.StopReflesh = false;
        }

        void FrmUACSMainMonitor_Load(object sender, EventArgs e)
        {
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
        const int WM_SYSCOMMAND = 0x112;
        const int SC_CLOSE = 0xF060;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC_MINIMIZE)
                {
                    uacsYardMonitor1.StopReflesh = true;
                }
                else
                {
                    uacsYardMonitor1.StopReflesh = false;
                }
            }
            base.WndProc(ref m);
        }
    }
}
