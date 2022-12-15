using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParkingControlLibrary
{
    public delegate void sendMember(string parkName);
    public partial class ParkingState : UserControl
    {
        public ParkingState(string parkNo,string carState)
        {
            InitializeComponent();
            txtparkNo.Text = parkNo;
            txtCarState.Text = carState;
            this.Click += ParkingState_DoubleClick;
        }
        public ParkingState()
        {
            InitializeComponent();
            this.DoubleClick += ParkingState_DoubleClick;
            foreach (var item in this.tableLayoutPanel1.Controls)
            {
                if (item is Label)
                {
                    ((Label)item).Click += ParkingState_DoubleClick;
                    //((Label)item).MouseLeave += ParkingState_MouseLeave;
                    //((Label)item).MouseMove += ParkingState_MouseMove;

                }
            }
        }
        private string parkName;

        public string ParkName
        {
            get { parkName = txtparkNo.Text; return parkName; }
           // set { parkName = value; }
        }
        void ParkingState_DoubleClick(object sender, EventArgs e)
        {
            if (sendMember!=null)
            {
                sendMember(txtparkNo.Text.Trim());
            }
        }

        public event sendMember sendMember;

        /// <summary>
        /// 车位状态与扫描状态
        /// </summary>
        /// <param name="parkNo">停车位编号</param>
        /// <param name="carState">卡车状态</param>
        /// <param name="parkState">停车位状态</param>
        /// <param name="carNo">卡车编号</param>
        /// <param name="carType">车类型</param>
        /// <param name="isWoodenCar">木架卷车</param>
        public void SetPark(string parkNo,string  carState,string  parkState,string carNo ,string carType, string isWoodenCar)
        {
            try
            {
                 txtparkNo.Text = parkNo;
                //
                 txtCarNo.Text = carNo;
                //
                 if (carState == "0")
                 {
                     txtCarState.Text = "出库";
                 }
                 else if (carState == "1")
                 {
                     txtCarState.Text = "入库";
                 }
                 else
                 {
                     txtCarState.Text = "";
                 }
                 //

                 if (parkState == "5")
                 {
                     txtParkState.Text = "无车";
                 }
                 else if (parkState == "10")
                 {
                     txtParkState.Text = "车位有车";
                 }
                 else if (parkState == "110")
                 {
                     txtParkState.Text = "扫描开始";
                 }
                 else if (parkState == "120")
                 {
                     txtParkState.Text = "扫描完成";
                 }
                 else if (parkState == "130")
                 {
                     txtParkState.Text = "手持机扫描完";
                 }
                 else if (parkState == "140")
                 {
                     txtParkState.Text = "计划生成";
                 }
                 else if (parkState == "160")
                 {
                     txtParkState.Text = "作业开始";
                 }
                 else if (parkState == "170")
                 {
                     txtParkState.Text = "入库暂停";
                 }
                 else if (parkState == "180")
                 {
                     txtParkState.Text = "作业结束";
                 }
                 else if (parkState == "210")
                 {
                     txtParkState.Text = "扫描开始";
                 }
                 else if (parkState == "220")
                 {
                     txtParkState.Text = "扫描完成";
                 }
                 else if (parkState == "240")
                 {
                     txtParkState.Text = "计划生成";
                 }
                 else if (parkState == "260")
                 {
                     txtParkState.Text = "作业开始";
                 }
                 else if (parkState == "270")
                 {
                     txtParkState.Text = "出库暂停";
                 }
                 else if (parkState == "280")
                 {
                     txtParkState.Text = "作业结束";
                 }
                 else if (parkState == "290")
                 {
                     txtParkState.Text = "手持机确认";
                 }

                 else
                 {
                     txtParkState.Text = "";
                 }
                string temp = addCarType(carType, isWoodenCar);
                 if (temp!="")
                 {
                     txtParkState.Text = string.Format("{0}{1}", temp, txtParkState.Text);
                 }

                 if (txtParkState.Text.Contains("无车"))
                 {
                    //txtParkState.ForeColor = Color .White;
                    txtParkState.ForeColor = Color.Black;
                }
                 else if (txtParkState.Text.Contains("暂停"))
                 {
                     txtParkState.ForeColor = Color.Orange;
                 }
                 else
                 {
                     txtParkState.ForeColor = Color.Black;
                 }
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }
        }

        private void ParkingState_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private string addCarType(string cartype, string isWoodenCar)
        {
            string strCarType = "";
            if (cartype == "100" || cartype == "106") //车辆类型（100：框架车/ 101：社会车/ 102：大头娃娃车/ 103：较低社会车/ 104：雨棚车/ 200：木架卷车）
            {
                strCarType = "框架车";
            }
            else if (cartype =="101")
            {
                if(isWoodenCar=="1")
                {
                    strCarType = "木架卷车";
                }
                else
                {
                    strCarType = "社会车";
                }
            }
            else if (cartype == "102")
            {
                strCarType = "娃娃车";
            }
            else if (cartype == "103")
            {
                strCarType = "较低车";
            }
            else if (cartype == "104")
            {
                strCarType = "雨棚车";
            }
            else if (cartype == "200")
            {
                strCarType = "木架卷车";
            }
            return strCarType;
        }

        private void ParkingState_MouseLeave(object sender, EventArgs e)
        {
           // this.BackColor = SystemColors.InactiveCaption;
        }

        private void ParkingState_MouseMove(object sender, MouseEventArgs e)
        {
           // this.BackColor = Color.LightBlue;
        }

        public void setControilBackColor(Color myColor)
        {
            this.BackColor = myColor;
        }




    }
}
