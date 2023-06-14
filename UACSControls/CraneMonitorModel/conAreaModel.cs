using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public class conAreaModel
    {

        private delegate void AddAreaDelegate(conArea area);        // 创建委托和委托对象
        private AddAreaDelegate addAreaDelegate;

        private string bayNO = string.Empty;
        public string BayNO
        {
            get { return bayNO; }
            set { bayNO = value; }
        }
        private Panel bayPanel = new Panel();
        private Panel bayPanel2 = new Panel();
        private long baySpaceX = 0;
        private long baySpaceY = 0;
        private int panelWidth = 0;
        private int panelHeight = 0;
        private bool xAxisRight = false;
        private bool yAxisDown = false;
        private bool StockPercentage = false;
        private AreaInfo theAreaInfoInBay = new AreaInfo();
        private string tagServiceName = string.Empty;
        private bool flag = false;
        private Dictionary<string, conArea> dicAreaVisual = new Dictionary<string, conArea>();

        /// <summary>
        /// 显示小区需要的详细信息
        /// </summary>
        public void conInit(Panel theBayPanel, string theBayNO, string theTagServiceName, long _baySpaceX, long _baySpaceY, int _panelWidth, int _panelHeight, bool _xAxisRight, bool _yAxisDown , bool _stockPercentage, AreaInfo.AreaType _AreaType)
        {
            try
            {
                bayPanel = theBayPanel;
                bayPanel2 = theBayPanel;
                bayNO = theBayNO;
                tagServiceName = theTagServiceName;
                baySpaceX = _baySpaceX;
                baySpaceY = _baySpaceY;
                panelWidth = _panelWidth;
                panelHeight = _panelHeight;
                xAxisRight = _xAxisRight;
                yAxisDown = _yAxisDown;
                StockPercentage = _stockPercentage;
                //theAreaInfoInBay.conInit(theBayNO, _AreaType);
                theAreaInfoInBay.conInit(theBayNO, theTagServiceName, flag);
                refreshControl();
                //refreshControl2();
            }
            catch (Exception ex)
            {
            }
        }

        public void refreshControl()
        {
            try
            {
                theAreaInfoInBay.getPortionAreaData(bayNO);
                theAreaInfoInBay.getTagNameList();
                theAreaInfoInBay.getTagValues();
                DataTable Grids = theAreaInfoInBay.getGridData();
                foreach (AreaBase theAreaInfo in theAreaInfoInBay.DicSaddles.Values.ToArray())
                {
                        conArea theSaddleVisual = new conArea();
                        if (dicAreaVisual.ContainsKey(theAreaInfo.AreaNo))
                        {
                            theSaddleVisual = dicAreaVisual[theAreaInfo.AreaNo];
                        }
                        else
                        {
                            theSaddleVisual = new conArea();
                            bayPanel.Invoke(new Action(() => { bayPanel.Controls.Add(theSaddleVisual); }));
                        }
                        conArea.areaRefreshInvoke theInvoke = new conArea.areaRefreshInvoke(theSaddleVisual.refreshControl);
                        if (theSaddleVisual.IsHandleCreated)
                        {
                            theSaddleVisual.BeginInvoke(theInvoke, new Object[] { theAreaInfo, baySpaceX, baySpaceY, panelWidth, panelHeight, xAxisRight, yAxisDown, bayPanel, theSaddleVisual, StockPercentage, Grids });
                        }
                        theSaddleVisual.Saddle_Selected -= new conArea.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
                        theSaddleVisual.Saddle_Selected += new conArea.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
                        dicAreaVisual[theAreaInfo.AreaNo] = theSaddleVisual;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteProgramLog(bayNO);
                LogManager.WriteProgramLog(ex.Message);
                LogManager.WriteProgramLog(ex.StackTrace);
            }
        }

        void theSaddleVisual_Saddle_Selected(AreaBase theSaddleInfo)
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

        public delegate void EventHandler_Saddle_Selected(AreaBase theSaddleInfo);
        public event EventHandler_Saddle_Selected Saddle_Selected;

        public bool updataCraneWaterLevel(string craneNO)
        {
            bool ret = false;
            ret = theAreaInfoInBay.getCraneWaterLevelStatus(craneNO);
            return ret;

        }
    }
}
