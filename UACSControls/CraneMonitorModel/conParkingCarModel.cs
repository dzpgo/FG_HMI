using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public class conParkingCarModel
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
        private ParkingInfo theCarInfoInBay = new ParkingInfo();
        private string tagServiceName = string.Empty;

        public void conInit(Panel theBayPanel, string theBayNO, string theTagServiceName, long _baySpaceX, long _baySpaceY, int _panelWidth, int _panelHeight, bool _xAxisRight, bool _yAxisDown)
        {
            try
            {
                bayPanel = theBayPanel;
                bayNO = theBayNO;
                tagServiceName = theTagServiceName;
                baySpaceX = _baySpaceX;
                baySpaceY = _baySpaceY;
                panelWidth = _panelWidth;
                panelHeight = _panelHeight;
                xAxisRight = _xAxisRight;
                yAxisDown = _yAxisDown;
                theCarInfoInBay.conInit(theBayNO);
                refreshControl();
            }
            catch (Exception ex)
            {
            }
        }



        private Dictionary<string, conParkingCar> dicSaddleVisual = new Dictionary<string, conParkingCar>();

        public void refreshControl()
        {
            try
            {

                theCarInfoInBay.GetParkingMessage();
                foreach (ParkingBase theSaddleInfo in theCarInfoInBay.DicCarMessage.Values)
                {
                    conParkingCar theSaddleVisual = new conParkingCar();
                    if (dicSaddleVisual.ContainsKey(theSaddleInfo.ParkingName))
                    {
                        theSaddleVisual = dicSaddleVisual[theSaddleInfo.ParkingName];

                    }
                    else
                    {
                        theSaddleVisual = new conParkingCar();
                        theSaddleVisual.conInit(bayPanel, theSaddleInfo.Car_No, tagServiceName);
                        bayPanel.Controls.Add(theSaddleVisual);
                    }
                    conParkingCar.carRefreshInvoke theInvoke = new conParkingCar.carRefreshInvoke(theSaddleVisual.refreshControl);
                    if (theSaddleVisual.IsHandleCreated)
                    {
                        theSaddleVisual.BeginInvoke(theInvoke, new Object[] { theSaddleInfo, baySpaceX, baySpaceY, panelWidth, panelHeight, xAxisRight, yAxisDown });
                    }
                    dicSaddleVisual[theSaddleInfo.ParkingName] = theSaddleVisual;
                    //Application.DoEvents();
                }

            }
            catch (Exception ex)
            {
            }
        }

    }
}
