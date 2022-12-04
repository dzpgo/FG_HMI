using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public partial class conOffinePackingStatusSwitchover : UserControl
    {
        public conOffinePackingStatusSwitchover()
        {
            InitializeComponent();
            this.btnCraneAuto.MouseEnter += btnInCoil_MouseEnter;
            //this.btnOutCoil.MouseEnter += btnInCoil_MouseEnter;
            this.btnWorking.MouseEnter += btnInCoil_MouseEnter;
            //this.btnInCoil.MouseLeave += btnInCoil_MouseLeave;
            this.btnCraneAuto.MouseLeave += btnInCoil_MouseLeave;
            this.btnWorking.MouseLeave += btnInCoil_MouseLeave;
        }

        void btnInCoil_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.SkyBlue;
        }

        void btnInCoil_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.CornflowerBlue;
        }


        public string AreaStatus
        {
            get { return lblStatus.Text; }
            //set { lblStatus.Text = value; }
        }
        


        private string areaNo;

        public string AreaNo
        {
            get { return areaNo; }
            set { areaNo = value; }
        }

        private string subAreaNO;

        public string SubAreaNO
        {
            get { return subAreaNO; }
            set { subAreaNO = value; }
        }

        [Description("离线包装小区名称")]
        public string MyAreaName
        {
            get { return lblSubAreaName.Text; }
            set { lblSubAreaName.Text = value; }
        }

        private void btnInCoil_Click(object sender, EventArgs e)
        {
            OffinePackingSaddleInBay.UpPackingStatus(
                (int)OffinePackingSaddleInBay.AreaStatus.InCoil, areaNo, subAreaNO);
        }

        private void btnOutCoil_Click(object sender, EventArgs e)
        {
            OffinePackingSaddleInBay.UpPackingStatus(
                (int)OffinePackingSaddleInBay.AreaStatus.OutCoil, areaNo, subAreaNO);
        }

        private void btnWorking_Click(object sender, EventArgs e)
        {
            OffinePackingSaddleInBay.UpPackingStatus(
                (int)OffinePackingSaddleInBay.AreaStatus.Working, areaNo, subAreaNO);
        }       

        public void RefreshOffinePackingStatus()
        {
            string status = OffinePackingSaddleInBay.GetPackingStatus(areaNo, subAreaNO);
            if (status != null)
                lblStatus.Text = status;
            else
                lblStatus.Text = "未知";
        }

        private void btnCraneAuto_Click(object sender, EventArgs e)
        {
            OffinePackingSaddleInBay.UpPackingStatus(
                (int)OffinePackingSaddleInBay.AreaStatus.Auto, areaNo, subAreaNO);
        }

        
    }
}
