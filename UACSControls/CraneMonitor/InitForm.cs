using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UACSControls
{
    public partial class InitForm : Form
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);
        public InitForm()
        {
            InitializeComponent();
        }
        public InitForm(string str)
        {
            try
            {
                InitializeComponent();
                label1.Text = string.Format("{0}{1}", str, label1.Text);
                //this.TopMost = true;
                this.Load += InitForm_Load;
            }
            catch (System.Threading.ThreadInterruptedException ex)
            {
               
            }

        }

        void InitForm_Load(object sender, EventArgs e)
        {
            this.Shown += InitForm_Shown;
            label1.Click += label1_Click;
        }

        void InitForm_Shown(object sender, EventArgs e)
        {
            this.Opacity = 0.7; 
        }

        void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        delegate void queryTimer_TickDelegate();

        public void Form1_Closedelegate()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new queryTimer_TickDelegate(Form1_Closedelegate), new object[] { });
                return;
            }
            this.Close();
        }
        
    }
}
