using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UACSDAL;
using UACSPopupForm;
using UACSView.View_CraneMonitor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace UACSControls
{
    public partial class FrmPlanCompletionBox : Form
    {
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
        public FrmPlanCompletionBox()
        {
            InitializeComponent();
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
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        private void GetOrderQueue()
        {
            var sqlText = @"SELECT ORDER_NO,CRANE_NO,CAR_NO,TO_STOCK_NO FROM UACS_ORDER_QUEUE WHERE ORDER_NO = '"+ OrderNo + "' AND CRANE_NO = '"+ CranmeNo + "';";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    CarNo = rdr["CAR_NO"].ToString();
                    ToStockNo = rdr["TO_STOCK_NO"].ToString();
                }
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ParkingBase parkingInfo = new ParkingBase();
            parkingInfo.Car_No = CarNo;
            parkingInfo.ParkingName = ToStockNo;
            parkingInfo.STOWAGE_ID = 0;
            parkingInfo.IsLoaded = 1;
            parkingInfo.PackingStatus = 10;
            FrmParkingDetail frm = new FrmParkingDetail();
            frm.PackingInfo = parkingInfo;
            frm.Show();
            this.Close();
        }
    }
}
