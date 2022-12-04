using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public class conUnitSaddleModel
    {
        private Panel bayPanel = new Panel();
        private long baySpaceX = 0;
        private long baySpaceY = 0;
        private int panelWidth = 0;
        private int panelHeight = 0;
        private bool xAxisRight = false;
        private bool yAxisDown = false;
        private SaddleInBay theSaddlsInfoInBay = new SaddleInBay();
        private string unitNoFlag = string.Empty;
        private string tagServiceName = string.Empty;

        public void conInit(Panel _theBayPanel, string _theUnitNo, string _theTagServiceName, long _baySpaceX, long _baySpaceY, int _panelWidth, int _panelHeight, bool _xAxisRight, bool _yAxisDown, string _BayNo)
        {
            try
            {
                bayPanel = _theBayPanel;
                tagServiceName = _theTagServiceName;
                baySpaceX = _baySpaceX;
                baySpaceY = _baySpaceY;
                panelWidth = _panelWidth;
                panelHeight = _panelHeight;
                xAxisRight = _xAxisRight;
                yAxisDown = _yAxisDown;
                unitNoFlag = _theUnitNo;
                theSaddlsInfoInBay.conInit(_theUnitNo, _theTagServiceName, _BayNo);
                refreshControl();
            }
            catch (Exception ex)
            {
            }

        }

        private Dictionary<string, conUnitSaddle> dicSaddleVisual = new Dictionary<string, conUnitSaddle>();

        public void refreshControl()
        {
            try
            {
                //出口机组
                if ((unitNoFlag.IndexOf("C") > -1) || unitNoFlag.IndexOf("EL") > -1 || unitNoFlag.IndexOf("PA") > -1)
                {
                    theSaddlsInfoInBay.getUnitSaddleData(true);
                }
                //入口机组
                if (unitNoFlag.IndexOf("R") > -1)
                {
                    theSaddlsInfoInBay.getUnitSaddleData(false);
                }

                theSaddlsInfoInBay.getTagNameList();
                theSaddlsInfoInBay.getTagValues();


                foreach (SaddleBase theSaddleInfo in theSaddlsInfoInBay.DicSaddles.Values.ToArray())
                {
                    conUnitSaddle theSaddleVisual = new conUnitSaddle();

                    if (dicSaddleVisual.ContainsKey(theSaddleInfo.SaddleNo))
                    {
                        theSaddleVisual = dicSaddleVisual[theSaddleInfo.SaddleNo];
                    }
                    else
                    {
                        theSaddleVisual = new conUnitSaddle();
                        bayPanel.Invoke(new Action(() => { bayPanel.Controls.Add(theSaddleVisual); }));
                    }
                    conUnitSaddle.saddlesRefreshInvoke theInvoke = new conUnitSaddle.saddlesRefreshInvoke(theSaddleVisual.refreshControl);
                    if (theSaddleVisual.IsHandleCreated)
                    {
                        theSaddleVisual.BeginInvoke(theInvoke, new Object[] { theSaddleInfo, baySpaceX, baySpaceY, panelWidth, panelHeight, xAxisRight, yAxisDown });
                    }
                    theSaddleVisual.Saddle_Selected -= new conUnitSaddle.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
                    theSaddleVisual.Saddle_Selected += new conUnitSaddle.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
                    dicSaddleVisual[theSaddleInfo.SaddleNo] = theSaddleVisual;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteProgramLog(ex.Message);
                LogManager.WriteProgramLog(ex.StackTrace);
            }
        }

        void theSaddleVisual_Saddle_Selected(SaddleBase theSaddleInfo)
        {
            try
            {
                if (Saddle_Selected != null)
                {
                    Saddle_Selected(theSaddleInfo.Clone());
                }
            }
            catch (Exception ex)
            {
            }
        }

        public delegate void EventHandler_Saddle_Selected(SaddleBase theSaddleInfo);
        public event EventHandler_Saddle_Selected Saddle_Selected;
    }
}
