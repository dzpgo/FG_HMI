using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UACSControls;
using UACSDAL;

using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.TagService.Controls;
using Baosight.iSuperframe.TagService;

namespace UACSView
{
    public partial class PREMIERE_TESTING_61_TO_63 : FormBase
    {
        public PREMIERE_TESTING_61_TO_63()
        {
            InitializeComponent();
        }


        private string tagServiceName = "iplature";

        private long baySpaceX = 400000;
        private long baySpaceY = 50000;
        private int panelWidth;
        private int panelHeight;
        private bool xAxisRight = true;
        private bool yAxisDown = false;

        private string craneNO_61 = "61";
        private string craneNO_62 = "62";
        private string craneNO_63 = "63";
        //private string craneNO_4 = "7_3";

        private CranePLCStatusInBay cranePLCStatusInBay = new CranePLCStatusInBay();

        private List<ConCraneStatusPanel> lstConCraneStatusPanel = new List<ConCraneStatusPanel>();
        private List<ConCraneVisual>lstConCraneVisual=new List<ConCraneVisual> ();

        private void PREMIERE_TESTING_4_1_TO_4_3_Load(object sender, EventArgs e)
        {
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                conCraneVisual_61.InitTagDataProvide(constData.tagServiceName);
                conCraneVisual_61.CraneNO = craneNO_61;
                lstConCraneVisual.Add(conCraneVisual_61);

                conCraneVisual_62.InitTagDataProvide(constData.tagServiceName);
                conCraneVisual_62.CraneNO = craneNO_62;
                lstConCraneVisual.Add(conCraneVisual_62);

                //conCraneVisual_63.InitTagDataProvide(constData.tagServiceName);
                //conCraneVisual_63.CraneNO = craneNO_63;
                //lstConCraneVisual.Add(conCraneVisual_63);

                //conCraneVisual_4.InitTagDataProvide(tagServiceName);
                //conCraneVisual_4.CraneNO = craneNO_4;
                //lstConCraneVisual.Add(conCraneVisual_4);

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                conCraneStatusPanel_1.InitTagDataProvide(constData.tagServiceName);
                conCraneStatusPanel_1.CraneNO = craneNO_61;
                lstConCraneStatusPanel.Add(conCraneStatusPanel_1);

                conCraneStatusPanel_2.InitTagDataProvide(constData.tagServiceName);
                conCraneStatusPanel_2.CraneNO = craneNO_62;
                lstConCraneStatusPanel.Add(conCraneStatusPanel_2);

                //conCraneStatusPanel_3.InitTagDataProvide(constData.tagServiceName);
                //conCraneStatusPanel_3.CraneNO = craneNO_63;
                //lstConCraneStatusPanel.Add(conCraneStatusPanel_3);

                //conCraneStatusPanel_4.InitTagDataProvide(tagServiceName);
                //conCraneStatusPanel_4.CraneNO = craneNO_4;
                //lstConCraneStatusPanel.Add(conCraneStatusPanel_4);


                ////////////////////////////////////////////////////////////////////////////////////////////////////
                cranePLCStatusInBay.InitTagDataProvide(constData.tagServiceName);

                cranePLCStatusInBay.AddCraneNO(craneNO_61);
                cranePLCStatusInBay.AddCraneNO(craneNO_62);
                //cranePLCStatusInBay.AddCraneNO(craneNO_63);
                //cranePLCStatusInBay.AddCraneNO(craneNO_4);

                cranePLCStatusInBay.SetReady();

                timer.Interval = 500;
                timer.Enabled = true;


            }
            catch (Exception ex)
            {
            }
        }

        bool tabActived = true;
        void MyTabActivated(object sender, EventArgs e)
        {
            tabActived = true;
        }
        void MyTabDeactivated(object sender, EventArgs e)
        {
            tabActived = false;
        }

        //时钟函数
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tabActived == false)
                {
                    return;
                }

                cranePLCStatusInBay.getAllPLCStatusInBay();

                foreach(ConCraneVisual  conCraneVisual in lstConCraneVisual)
                {
                    ConCraneVisual.RefreshControlInvoke ConCraneVisual_Invoke = new ConCraneVisual.RefreshControlInvoke(conCraneVisual.RefreshControl);
                    conCraneVisual.BeginInvoke(ConCraneVisual_Invoke, new Object[] { cranePLCStatusInBay.DicCranePLCStatusBase[conCraneVisual.CraneNO].Clone(), baySpaceX, baySpaceY, panelBay.Width, panelBay.Height, xAxisRight, yAxisDown });

                }
                
                foreach(ConCraneStatusPanel conCraneStatusPanel in lstConCraneStatusPanel)
                {
                        ConCraneStatusPanel.RefreshControlInvoke ConCraneStatusPanel_Invoke = new ConCraneStatusPanel.RefreshControlInvoke(conCraneStatusPanel.RefreshControl);
                        conCraneStatusPanel.BeginInvoke(ConCraneStatusPanel_Invoke, new Object[] { cranePLCStatusInBay.DicCranePLCStatusBase[conCraneStatusPanel.CraneNO].Clone() });
                }

            }
            catch (Exception ex)
            {
            }
        }


    }
}
