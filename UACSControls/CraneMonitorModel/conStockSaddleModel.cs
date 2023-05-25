using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
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
        public bool isGRID_DIV = false;
        public string grid_DIV = string.Empty;

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
                refreshControl(areaBase.AreaNo, false);
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        /// <param name="theBayPanel"></param>
        /// <param name="areaBase"></param>
        /// <param name="theTagServiceName"></param>
        /// <param name="_panelWidth"></param>
        /// <param name="_panelHeight"></param>
        /// <param name="_xAxisRight"></param>
        /// <param name="_yAxisDown"></param>
        /// <param name="_index"></param>
        /// <param name="isGRID_DIV"></param>
        public void conInit(Panel theBayPanel, AreaBase areaBase, string theTagServiceName, int _panelWidth, int _panelHeight, bool _xAxisRight, bool _yAxisDown, int _index, bool isGRID_DIV)
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
                refreshControl(areaBase.AreaNo, isGRID_DIV);
            }
            catch (Exception ex)
            { }
        }

        private Dictionary<string, conStockSaddle> dicSaddleVisual = new Dictionary<string, conStockSaddle>();

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="AreaNo"></param>
        /// <param name="isGRID_DIV"></param>
        public void refreshControl(string AreaNo, bool isGRID_div)
        {
            isGRID_div = isGRID_DIV;
            //取这块小区的大小
            double X_Width = theAreaBase.X_End - theAreaBase.X_Start;
            double Y_Height = theAreaBase.Y_End - theAreaBase.Y_Start;
            bayPanel.Controls.Clear();
            theSaddlsInfoInBay.getSaddleInfo(AreaNo, grid_DIV, isGRID_div);
            isGRID_DIV = false;
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
