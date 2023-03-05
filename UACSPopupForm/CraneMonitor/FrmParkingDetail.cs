using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.Authorization.Interface;
using Baosight.iSuperframe.Common;

namespace UACSPopupForm
{
    public partial class FrmParkingDetail : Form
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
        public FrmParkingDetail()
        {
            InitializeComponent();
        }

        private ParkingBase packingInfo;
        public ParkingBase PackingInfo
        {
            get { return packingInfo; }
            set { packingInfo = value; }
        }
        int carIsLoad;
        private void FrmParkingDetail_Load(object sender, EventArgs e)
        {
            lblCarNo.Text = packingInfo.Car_No;
            lblCarStatus.Text = packingInfo.PackingStatusDesc();
            lblPacking.Text = packingInfo.ParkingName;
            carIsLoad = packingInfo.IsLoaded;
            //lblCarType.Text = ParkingInfo.getStowageCarType(packingInfo.STOWAGE_ID);
            //ParkingInfo.dgvStowageMessage(packingInfo.STOWAGE_ID, dgvStowageMessage);
            //ParkingInfo.dgvStowageOrder(packingInfo.ParkingName, dgvCraneOder);
            lblCarType.Text = ParkingInfo.getStowageCarType(packingInfo.STOWAGE_ID, packingInfo.Car_No, packingInfo.ParkingName);
            ParkingInfo.dgvStowageMessage(packingInfo.STOWAGE_ID, packingInfo.Car_No, packingInfo.ParkingName, dgvStowageMessage);
            ParkingInfo.dgvStowageOrder(packingInfo.Car_No, packingInfo.ParkingName, dgvCraneOder);
            //ShiftStowageMessage();
            this.Deactivate += new EventHandler(frmSaddleDetail_Deactivate);
        }
        void frmSaddleDetail_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }
        private void ShiftStowageMessage()
        {
            for (int i = 0; i < dgvStowageMessage.Rows.Count; i++)
            {
                dgvStowageMessage.Rows[i].DefaultCellStyle.BackColor = Color.White;
                if (dgvStowageMessage.Rows[i].Cells["STATUS"].Value != DBNull.Value)
                {
                    if (dgvStowageMessage.Rows[i].Cells["STATUS"].Value.ToString() == "执行完")
                    {
                        dgvStowageMessage.Rows[i].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            if (lblPacking.Text.Contains("A"))
            {
                string bayno = null;
                switch (lblPacking.Text)
                {
                    case "Z01A1":
                    case "Z01A2":
                        bayno = "1550酸轧原料库";
                        break;
                    case "Z63A1":
                    case "Z63A2":
                        bayno = "轧后库63跨";
                        break;
                    default:
                        bayno = null;
                        break;
                }
                if (lblCarStatus.Text.Contains("出库") || carIsLoad == 0)
                {
                    if (auth.IsOpen("02 车辆出库"))
                    {
                        auth.CloseForm("02 车辆出库");
                        //auth.OpenForm("02 车辆出库", true, bayno, lblPacking.Text);
                        auth.OpenForm("02 车辆出库", lblPacking.Text);
                    }
                    else
                    {
                        //auth.OpenForm("02 车辆出库", true, bayno, lblPacking.Text);
                        auth.OpenForm("02 车辆出库", lblPacking.Text);
                    }
                    this.Close();
                }
                if (lblCarStatus.Text.Contains("入库") || carIsLoad == 1)
                {
                    if (auth.IsOpen("01 车辆入库"))
                    {
                        auth.CloseForm("01 车辆入库");
                        //auth.OpenForm("01 车辆入库", true, bayno, lblPacking.Text);
                        auth.OpenForm("01 车辆入库", lblPacking.Text);
                    }
                    else
                    {
                        //auth.OpenForm("01 车辆入库", true, bayno, lblPacking.Text);
                        auth.OpenForm("01 车辆入库", lblPacking.Text);
                    }
                    this.Close();
                }
            }
        }

    }
}
