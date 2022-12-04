using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public class conStockSaddleModel
    {
        private string bayNO = string.Empty;
        public string BayNO
        {
            get { return bayNO; }
            set { bayNO = value; }
        }
        private long baySpaceX = 0;
        private long baySpaceY = 0;
        private string CoilNo = null;

        private Panel bayPanel = new Panel();
        private bool xAxisRight = false;
        private bool yAxisDown = false;
        private int panelWidth = 0;
        private int panelHeight = 0;
        private SaddleInBay theSaddlsInfoInBay = new SaddleInBay();
        private AreaBase theAreaBase = new AreaBase();
        private string tagServiceName = string.Empty;
        private List<int> list = new List<int>();

        public void conInit(Panel theBayPanel, AreaBase areaBase, string theTagServiceName, int _panelWidth, int _panelHeight, bool _xAxisRight, bool _yAxisDown, int _index)
        {
            try
            {
                theAreaBase = areaBase;
                bayPanel = theBayPanel;
                tagServiceName = theTagServiceName;
                xAxisRight = _xAxisRight;
                yAxisDown = _yAxisDown;
                panelWidth = _panelWidth;
                panelHeight = _panelHeight;

                if (_index == 888)
                {
                    list = null;
                }
                else
                {
                    list.Add(_index);
                }

                theSaddlsInfoInBay.conInit(areaBase.AreaNo, theTagServiceName);
                refreshControl();
            }
            catch (Exception ex)
            { }
        }

        public bool conInit(Panel _theBayPanel, string _theBayNO, string _theTagServiceName, long _baySpaceX, long _baySpaceY, int _panelWidth, int _panelHeight, bool _xAxisRight, bool _yAxisDown, string _coilNo = null)
        {
            try
            {
                bool isLoadUserControl = false;
                bayPanel = _theBayPanel;
                bayNO = _theBayNO;
                tagServiceName = _theTagServiceName;

                baySpaceX = _baySpaceX;
                baySpaceY = _baySpaceY;
                panelWidth = _panelWidth;
                panelHeight = _panelHeight;
                xAxisRight = _xAxisRight;
                yAxisDown = _yAxisDown;

                CoilNo = _coilNo;

                theSaddlsInfoInBay.conInit(_theBayNO, _theTagServiceName);

                refreshControl();

                return isLoadUserControl;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private Dictionary<string, conStockSaddle> dicSaddleVisual = new Dictionary<string, conStockSaddle>();

        //public void refreshControl()
        //{

        //    //取这块小区的大小
        //    double X_Width = theAreaBase.X_End - theAreaBase.X_Start;
        //    double Y_Height = theAreaBase.Y_End - theAreaBase.Y_Start;

        //    theSaddlsInfoInBay.getSaddleInfo();
        //    foreach (SaddleBase theSaddleInfo in theSaddlsInfoInBay.DicSaddles.Values)
        //    {
        //        conStockSaddle theSaddleVisual = new conStockSaddle();               
        //        if (dicSaddleVisual.ContainsKey(theSaddleInfo.SaddleNo))
        //        {
        //            theSaddleVisual = dicSaddleVisual[theSaddleInfo.SaddleNo];
        //        }
        //        else
        //        {                
        //            theSaddleVisual = new conStockSaddle();
        //            theSaddleVisual.conInit();
        //            bayPanel.Controls.Add(theSaddleVisual);
        //        }
        //        conStockSaddle.saddlesRefreshInvoke theInvoke = new conStockSaddle.saddlesRefreshInvoke(theSaddleVisual.refreshControl);
        //        theSaddleVisual.BeginInvoke(theInvoke, new Object[] { theSaddleInfo, X_Width, Y_Height, theAreaBase, panelWidth, panelHeight, xAxisRight, yAxisDown, bayPanel, list });           
        //        theSaddleVisual.Saddle_Selected -= new conStockSaddle.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
        //        theSaddleVisual.Saddle_Selected += new conStockSaddle.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
        //        dicSaddleVisual[theSaddleInfo.SaddleNo] = theSaddleVisual;
        //    }
        //}

        public void refreshControl()
        {

            //取这块小区的大小
            double X_Width = theAreaBase.X_End - theAreaBase.X_Start;
            double Y_Height = theAreaBase.Y_End - theAreaBase.Y_Start;

            theSaddlsInfoInBay.getSaddleInfo();
            foreach (SaddleBase theSaddleInfo in theSaddlsInfoInBay.DicSaddles.Values)
            {
                conStockSaddle theSaddleVisual = new conStockSaddle();
                if (dicSaddleVisual.ContainsKey(theSaddleInfo.GRID_NO))
                {
                    theSaddleVisual = dicSaddleVisual[theSaddleInfo.GRID_NO];
                }
                else
                {
                    theSaddleVisual = new conStockSaddle();
                    theSaddleVisual.conInit();
                    bayPanel.Controls.Add(theSaddleVisual);
                }
                conStockSaddle.saddlesRefreshInvoke theInvoke = new conStockSaddle.saddlesRefreshInvoke(theSaddleVisual.refreshControl);
                theSaddleVisual.BeginInvoke(theInvoke, new Object[] { theSaddleInfo, X_Width, Y_Height, theAreaBase, panelWidth, panelHeight, xAxisRight, yAxisDown, bayPanel, list });
                theSaddleVisual.Saddle_Selected -= new conStockSaddle.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
                theSaddleVisual.Saddle_Selected += new conStockSaddle.EventHandler_Saddle_Selected(theSaddleVisual_Saddle_Selected);
                dicSaddleVisual[theSaddleInfo.GRID_NO] = theSaddleVisual;
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
            {}
        }

        public delegate void EventHandler_Saddle_Selected(SaddleBase theSaddleInfo);
        public event EventHandler_Saddle_Selected Saddle_Selected;
    }
}
