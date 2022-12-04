using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public class conOffinePackingSaddleModel
    {
        private string bayNO = string.Empty;
        public string BayNO
        {
            get { return bayNO; }
            set { bayNO = value; }
        }

        private Panel bayPanel = new Panel();
        private long baySpaceX = 0;
        private long baySpaceY = 0;
        private int panelWidth = 0;
        private int panelHeight = 0;
        private bool xAxisRight = false;
        private bool yAxisDown = false;
        private string tagServiceName = string.Empty;
        private SaddleInBay theSaddlsInfoInBay = new SaddleInBay();
        public void conInit(Panel _theBayPanel, string _theBayNO, string _theTagServiceName, long _baySpaceX, long _baySpaceY, bool _xAxisRight, bool _yAxisDown)
        {
            try
            {
                bayPanel = _theBayPanel;
                bayNO = _theBayNO;
                tagServiceName = _theTagServiceName;
                baySpaceX = _baySpaceX;
                baySpaceY = _baySpaceY;
                panelWidth = _theBayPanel.Width;
                panelHeight = _theBayPanel.Height;
                xAxisRight = _xAxisRight;
                yAxisDown = _yAxisDown;
                theSaddlsInfoInBay.conInit(_theBayNO, _theTagServiceName);
                refreshControl();
            }
            catch (Exception ex)
            { }
        }

        //private Dictionary<string, conOffinePackingSaddle> dicSaddleVisual = new Dictionary<string, conOffinePackingSaddle>();
        //public void refreshControl()
        //{
        //    try
        //    {

        //        theSaddlsInfoInBay.getSaddleInfo();
        //        foreach (SaddleBase theSaddleInfo in theSaddlsInfoInBay.DicSaddles.Values)
        //        {
        //            conOffinePackingSaddle theSaddleVisual = new conOffinePackingSaddle();

        //            if (dicSaddleVisual.ContainsKey(theSaddleInfo.SaddleNo))
        //            {
        //                theSaddleVisual = dicSaddleVisual[theSaddleInfo.SaddleNo];
        //            }
        //            else
        //            {
        //                theSaddleVisual = new conOffinePackingSaddle();
        //                theSaddleVisual.conInit();
        //                bayPanel.Controls.Add(theSaddleVisual);
        //            }
        //            conOffinePackingSaddle.saddlesRefreshInvoke theInvoke = new conOffinePackingSaddle.saddlesRefreshInvoke(theSaddleVisual.refreshControl);
        //            if (theSaddleVisual.IsHandleCreated)
        //            {
        //                theSaddleVisual.BeginInvoke(theInvoke, new Object[] { theSaddleInfo, baySpaceX, baySpaceY, panelWidth, panelHeight, xAxisRight, yAxisDown });
        //            }
                    
        //            dicSaddleVisual[theSaddleInfo.SaddleNo] = theSaddleVisual;

        //        }
              

        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteProgramLog(bayNO);
        //        LogManager.WriteProgramLog(ex.Message);
        //        LogManager.WriteProgramLog(ex.StackTrace);
        //    }
        //}

        private Dictionary<string, conOffinePackingSaddle> dicSaddleVisual = new Dictionary<string, conOffinePackingSaddle>();
        public void refreshControl()
        {
            try
            {

                theSaddlsInfoInBay.getSaddleInfo();
                foreach (SaddleBase theSaddleInfo in theSaddlsInfoInBay.DicSaddles.Values)
                {
                    conOffinePackingSaddle theSaddleVisual = new conOffinePackingSaddle();

                    if (dicSaddleVisual.ContainsKey(theSaddleInfo.SaddleNo))
                    {
                        theSaddleVisual = dicSaddleVisual[theSaddleInfo.SaddleNo];
                    }
                    else
                    {
                        theSaddleVisual = new conOffinePackingSaddle();
                        theSaddleVisual.conInit();
                        bayPanel.Controls.Add(theSaddleVisual);
                    }
                    conOffinePackingSaddle.saddlesRefreshInvoke theInvoke = new conOffinePackingSaddle.saddlesRefreshInvoke(theSaddleVisual.refreshControl);
                    if (theSaddleVisual.IsHandleCreated)
                    {
                        theSaddleVisual.BeginInvoke(theInvoke, new Object[] { theSaddleInfo, baySpaceX, baySpaceY, panelWidth, panelHeight, xAxisRight, yAxisDown });
                    }

                    dicSaddleVisual[theSaddleInfo.SaddleNo] = theSaddleVisual;

                }


            }
            catch (Exception ex)
            {
                LogManager.WriteProgramLog(bayNO);
                LogManager.WriteProgramLog(ex.Message);
                LogManager.WriteProgramLog(ex.StackTrace);
            }
        }

    }
}
