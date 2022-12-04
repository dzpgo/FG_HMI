using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.TagService.Interface;

namespace UACS
{
    public partial class CoilCranOrder : UserControl
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
       
        public delegate void CoilNoDelegate(string coilno);

        private const string Entry = "Entry";
        private const string Exit = "Exit";
        private const string OrderName = "指令号：";
        private const string StockName = "库位：";
        public CoilCranOrder()
        {
            InitializeComponent();
            //双缓存
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        #region 数据库连接
        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;
        //连接数据库
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
                    }
                    catch (System.Exception e)
                    {
                        //throw e;
                    }
                }
                return dbHelper;
            }
        }
        #endregion

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0014) // 禁掉清除背景消息

                return;

            base.WndProc(ref m);

        }
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN 
                return parms;
            }
        }    
        /// <summary>
        /// 出入口区分
        /// </summary>
        public string Name = null;
        /// <summary>
        /// 钢卷号
        /// </summary>
        public string CoilNo = null;

        private string saddleNo = null;
        /// <summary>
        /// 鞍座号
        /// </summary>
        public string SaddleNo
        {
            get
            {
                return saddleNo;
            }
            set 
            {
                saddleNo = value;
                CreateOrder();
            }
        }
        /// <summary>
        /// 指令号
        /// </summary>
        private string OrderNo;
        private enum StatusColor
        {
            #region 出口
            /// <summary>
            ///  有钢卷无指令(red)
            /// </summary>
            ExitCoilNoCranorder,
            /// <summary>
            ///  有钢卷有指令(White)
            /// </summary>
            ExitCoilInCranorder,

            #endregion
            /// <summary>
            /// 没有钢卷
            /// </summary>
            NoCoil,

            #region 入口
            /// <summary>
            /// 入口退料（yellow）
            /// </summary>
            EntryReturnMaterial,
            /// <summary>
            /// 入口上料（Green）
            /// </summary>
            EntryUpMaterial,
            /// <summary>
            /// 入口称重（blue）
            /// </summary>
            EntryWeight
            #endregion
        }
         
        /// <summary>
        /// 声明公共使用变量
        /// </summary>
        private StatusColor Status;
        /// <summary>
        ///  根据钢卷号给控件赋值（入口）
        /// </summary>
        /// <param name="coil_no"></param>
        public void EntryStatusOrCoil(string coil_no)
        {
            try
            {
                if (string.IsNullOrEmpty(coil_no))
                {
                    coil_no = null;
                }
                //用于接收吊运指令类型
                int orderType = 0;
                string sqlEntry = @"SELECT * FROM UACS_CRANE_ORDER_CURRENT ";
                sqlEntry += " WHERE MAT_NO = '" + coil_no + "' ORDER BY ORDER_NO  FETCH FIRST 1 ROWS ONLY";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlEntry))
                {
                    while (rdr.Read())
                    {
                        if (rdr["ORDER_NO"] != DBNull.Value)
                        {
                            lblOrder_No.Text = OrderName + rdr["ORDER_NO"].ToString();
                        }
                        else
                            lblOrder_No.Text = OrderName;


                        if (rdr["TO_STOCK_NO"] != DBNull.Value)
                        {
                            lblTo_Stock_No.Text = StockName + rdr["TO_STOCK_NO"].ToString();
                        }
                        else
                            lblTo_Stock_No.Text = StockName;

                        if (rdr["ORDER_TYPE"] != DBNull.Value)
                        {
                            orderType = Convert.ToInt32(rdr["ORDER_TYPE"].ToString());
                        }
                        else
                            orderType = 999;
                       
                        if (orderType == 14)   // 退料
                        {
                            Status = StatusColor.EntryReturnMaterial;
                        }
                        else if (orderType == 21)  // 上料
                        {
                            Status = StatusColor.EntryUpMaterial;
                        }
                        else if (orderType == 19)  // 称重
                        {
                            Status = StatusColor.EntryWeight;
                        }
                        else
                        {
                            Status = StatusColor.ExitCoilInCranorder;       
                        }
                    }
                }
                // 有钢卷号没有指令
                if (coil_no != null && orderType == 0)
                {                
                    Status = StatusColor.ExitCoilNoCranorder;
                    lblOrder_No.Text = OrderName;
                    lblTo_Stock_No.Text = StockName;
                    lblOrder_No.ForeColor = Color.White;
                    lblOrder_No.BackColor = Color.LightGray;
                }
                else if (coil_no == null && orderType == 0)  // 没有钢卷
                {                 
                    Status = StatusColor.NoCoil;
                    lblOrder_No.Text = OrderName;
                    lblTo_Stock_No.Text = StockName;
                    lblOrder_No.ForeColor = Color.Black;
                    lblOrder_No.BackColor = Color.White;        
                }

                tableLayoutPanel1.CellPaint += tableLayoutPanel1_CellPaint;

            }
            catch (Exception er)
            {

            }
        }

        void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 || e.Row == 2)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                switch (Status)
                {
                    case StatusColor.ExitCoilNoCranorder:
                        g.FillRectangle(Brushes.LightGray, r);                     
                        break;
                    case StatusColor.ExitCoilInCranorder:
                        lblOrder_No.ForeColor = Color.Black;
                        lblOrder_No.BackColor = Color.Cyan;
                        g.FillRectangle(Brushes.Cyan, r);              
                        break;
                    case StatusColor.EntryReturnMaterial:
                        lblOrder_No.ForeColor = Color.Black;  
                        lblOrder_No.BackColor = Color.Yellow;  
                        g.FillRectangle(Brushes.Yellow, r);
                        break;
                    case StatusColor.EntryUpMaterial:
                        lblOrder_No.BackColor = Color.Green;  
                        lblOrder_No.ForeColor = Color.White; 
                        g.FillRectangle(Brushes.Green, r);
                        break;
                    case StatusColor.EntryWeight:
                        lblOrder_No.ForeColor = Color.White;  
                        lblOrder_No.BackColor = Color.Blue;  
                        g.FillRectangle(Brushes.Blue, r);     
                        break;
                    case StatusColor.NoCoil:
                        g.FillRectangle(Brushes.White, r);                     
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 创建指令
        /// </summary>
        private void CreateOrder()
        {
            if (saddleNo != null)
            {
                tagDP.ServiceName = "iplature";
                tagDP.AutoRegist = true;
                TagValues.Clear();
                TagValues.Add("EV_ORDER_CREATE", null);
                tagDP.Attach(TagValues);
                if (Name == Entry)  //入口
                {
                    if (saddleNo != null && CoilNo != null)
                    {
                        MessageBox.Show("钢卷号：" + CoilNo);
                        string tagValue = CoilNo + "|" + "26" + "|" + saddleNo + "|";
                        tagDP.SetData("EV_ORDER_CREATE", tagValue);
                        MessageBox.Show("tagName:EV_ORDER_CREATE");
                        MessageBox.Show("tagValue:" + tagValue);
                    }
                }
                else if (Name ==  Exit)   //出口
                {
                    string tagValue = CoilNo + "|" + "15" + "|" + saddleNo + "|";
                    tagDP.SetData("EV_ORDER_CREATE", tagValue);
                    MessageBox.Show("tagName:EV_ORDER_CREATE");
                    MessageBox.Show("tagValue:" + tagValue);
                }
            }
           
        }

        /// <summary>
        /// 删除指令
        /// </summary>
        public void DelOrder(string coilno)
        {
            OrderNo = null;
            string sqlEntry = @"SELECT * FROM UACS_CRANE_ORDER_Z32_Z33 ";
            sqlEntry += " WHERE MAT_NO = '" + coilno + "' ORDER BY ORDER_NO  FETCH FIRST 1 ROWS ONLY";
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlEntry))
            {
                while (rdr.Read())
                {
                    OrderNo = rdr["ORDER_NO"].ToString();
                }
            }

            if (OrderNo != null)
            {
                OrderNo = OrderNo.Replace(" ", "");
                int count = OrderNo.Count();
                if (count != 0)
                {
                    tagDP.ServiceName = "iplature";
                    tagDP.AutoRegist = true;
                    TagValues.Clear();
                    TagValues.Add("EV_ORDER_DEL", null);
                    tagDP.Attach(TagValues);
                    tagDP.SetData("EV_ORDER_DEL", OrderNo);
                    MessageBox.Show("删除指令成功");
                }
            }
            else
                MessageBox.Show("没有指令号");
        }
    }
}