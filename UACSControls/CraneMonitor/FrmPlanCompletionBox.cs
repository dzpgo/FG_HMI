using System;
using System.Data;
using System.Windows.Forms;
using UACSDAL;
using UACSPopupForm;

namespace UACSControls
{
    /// <summary>
    /// 计划完成提醒
    /// </summary>
    public partial class FrmPlanCompletionBox : Form
    {
        //防止弹出信息关闭画面
        bool isPopupMessage = false;
        private string cranmeNo;
        private string orderNo;
        /// <summary>
        /// 行车号
        /// </summary>
        public string CranmeNo { get => cranmeNo; set => cranmeNo = value; }
        /// <summary>
        /// 指令号
        /// </summary>
        public string OrderNo { get => orderNo; set => orderNo = value; }
        public string CarNo;
        public string ToStockNo;
        public conCraneStatus ALM;
        private Timer timer;
        public FrmPlanCompletionBox()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 300000; // 设置定时器的间隔（单位：毫秒）
            timer.Tick += Timer_Tick; // 注册定时器的Tick事件处理方法
            timer.Start(); // 启动定时器
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 在定时器Tick事件中执行关闭窗体的操作
            Close();
        }
        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="form"></param>
        public FrmPlanCompletionBox(conCraneStatus form) : this()
        {
            ALM = form;
        }
        public FrmPlanCompletionBox(string cranmeNo1,string orderNo1) : this()
        {
            CranmeNo = cranmeNo1;
            OrderNo = orderNo1;
        }
        private void FrmPlanCompletionBox_Load(object sender, EventArgs e)
        {
            label1.Text = CranmeNo+ "#行车提醒";
            label2.Text = "A"+ CranmeNo + " 工位计划完成了";
            //加载数据
            GetOrderQueue();
            this.Deactivate += new EventHandler(FrmPlanCompletionBox_Deactivate);
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        private void GetOrderQueue()
        {            
            try
            {
                isPopupMessage = true;
                var sqlText = @"SELECT ORDER_NO,CRANE_NO,CAR_NO,TO_STOCK_NO FROM UACS_ORDER_QUEUE WHERE ORDER_NO = '" + OrderNo + "' AND CRANE_NO = '" + CranmeNo + "';";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        CarNo = rdr["CAR_NO"].ToString();
                        ToStockNo = rdr["TO_STOCK_NO"].ToString();
                    }
                }
                isPopupMessage = false;
            }
            catch (Exception)
            {
                isPopupMessage = false;
            }            
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmPlanCompletionBox_Deactivate(object sender, EventArgs e)
        {
            try
            {
                if (!isPopupMessage)
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            ParkingBase parkingInfo = new ParkingBase();
            parkingInfo.Car_No = CarNo;
            parkingInfo.ParkingName = ToStockNo;
            parkingInfo.STOWAGE_ID = 0;
            parkingInfo.IsLoaded = 1;
            parkingInfo.PackingStatus = 10;
            FrmParkingDetail frm = new FrmParkingDetail();
            frm.PackingInfo = parkingInfo;
            frm.Show();
            isPopupMessage = false;
            //this.Close();            
        }
    }
}
