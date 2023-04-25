using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using UACSDAL;
using UACSDAL.CraneMonitor;

namespace UACSControls.CraneMonitorModel
{
    /// <summary>
    /// 红绿灯实体类
    /// </summary>
    public class conTrafficLightModel
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
        private TrafficLightInfo theTrafficLightInBay = new TrafficLightInfo();

        public void conInit(Panel theBayPanel, string theBayNO, string theTagServiceName, long _baySpaceX, long _baySpaceY, int _panelWidth, int _panelHeight, bool _xAxisRight, bool _yAxisDown)
        {
            try
            {
                bayPanel = theBayPanel;
                bayNO = theBayNO;
                baySpaceX = _baySpaceX;
                baySpaceY = _baySpaceY;
                panelWidth = _panelWidth;
                panelHeight = _panelHeight;
                xAxisRight = _xAxisRight;
                yAxisDown = _yAxisDown;
                theTrafficLightInBay.conInit(theBayNO);
                refreshControl();
            }
            catch (Exception ex)
            {
            }
        }

        public void refreshControl()
        {
            try
            {
                //theTrafficLightInBay.GetParkingMessage();
                //theTrafficLightInBay.GetParkingMessage2();
                //foreach (TrafficLightBase thetrafficLightInfo in theTrafficLightInBay.DicCarMessage.Values)
                //{
                //    conTrafficLight theSaddleVisual = new conTrafficLight();
                //    if (dicTrafficLightVisual.ContainsKey(thetrafficLightInfo.AreaNo))
                //    {
                //        theSaddleVisual = dicTrafficLightVisual[thetrafficLightInfo.AreaNo];

                //    }
                //    else
                //    {
                //        //theSaddleVisual = new conTrafficLight();
                //        //theSaddleVisual.conInit(bayPanel, thetrafficLightInfo.Car_No, tagServiceName);
                //        bayPanel.Controls.Add(theSaddleVisual);
                //    }
                //    conTrafficLight.RefreshControlInvoke theInvoke = new conTrafficLight.RefreshControlInvoke(theSaddleVisual.RefreshControl);
                //    if (theSaddleVisual.IsHandleCreated)
                //    {
                //        theSaddleVisual.BeginInvoke(theInvoke, new Object[] { thetrafficLightInfo, baySpaceX, baySpaceY, panelWidth, panelHeight, xAxisRight, yAxisDown });
                //    }
                //    dicTrafficLightVisual[thetrafficLightInfo.AreaNo] = theSaddleVisual;
                //}
            }
            catch (Exception ex)
            {
            }
        }
        public List<TrafficLightBase> GetYardmapArea()
        {
            return theTrafficLightInBay.GetYardmapArea();
        }
    }
}
