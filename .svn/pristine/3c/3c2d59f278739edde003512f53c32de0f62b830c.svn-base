using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSControls
{
    public partial class FrmReCoilUnit : Form
    {
        public FrmReCoilUnit()
        {
            InitializeComponent();
        }

        private Dictionary<string, bool> dicUnit = new Dictionary<string, bool>();

        public Dictionary<string, bool> DicUnit
        {
            get { return dicUnit; }
            set { dicUnit = value; }
        }

        private void FrmReCoilUnit_Load(object sender, EventArgs e)
        {
            lblD171WR.Text = "";
            lblD172WR.Text = "";
            lblD173WR.Text = "";
            lblD174WR.Text = "";

            lblD171WC.Text = "";
            lblD172WC.Text = "";
            lblD173WC.Text = "";
            lblD174WC.Text = "";

            foreach (string unit in dicUnit.Keys)
            {
                if(unit =="D171WR")
                {
                    if(dicUnit[unit]==true)
                    {
                        lblD171WR.Text = "机组需上料";
                    }
                    else
                    {
                        lblD171WR.Text = "";
                    }
                }
                else if (unit == "D172WR")
                {
                    if (dicUnit[unit] == true)
                    {
                        lblD172WR.Text = "机组需上料";
                    }
                    else
                    {
                        lblD172WR.Text = "";
                    }
                }
                else if (unit == "D173WR")
                {
                    if (dicUnit[unit] == true)
                    {
                        lblD173WR.Text = "机组需上料";
                    }
                    else
                    {
                        lblD173WR.Text = "";
                    }
                }
                else if (unit == "D174WR")
                {
                    if (dicUnit[unit] == true)
                    {
                        lblD174WR.Text = "机组需上料";
                    }
                    else
                    {
                        lblD174WR.Text = "";
                    }
                }
                else if (unit == "D171WC")
                {
                    if (dicUnit[unit] == true)
                    {
                        lblD171WC.Text = "机组需收料";
                    }
                    else
                    {
                        lblD171WC.Text = "";
                    }
                }
                else if (unit == "D172WC")
                {
                    if (dicUnit[unit] == true)
                    {
                        lblD172WC.Text = "机组需收料";
                    }
                    else
                    {
                        lblD172WC.Text = "";
                    }
                }
                else if (unit == "D173WC")
                {
                    if (dicUnit[unit] == true)
                    {
                        lblD173WC.Text = "机组需收料";
                    }
                    else
                    {
                        lblD173WC.Text = "";
                    }
                }
                else if (unit == "D174WC")
                {
                    if (dicUnit[unit] == true)
                    {
                        lblD174WC.Text = "机组需收料";
                    }
                    else
                    {
                        lblD174WC.Text = "";
                    }
                }
            }
        }
    }
}
