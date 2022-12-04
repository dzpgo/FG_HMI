using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;

namespace UACSControls
{
    public partial class FrmSeekCoil : FormBase
    {
        
        public conStockSaddleModel saddleInStock_Z11_Z12 { get; set; }
        public int Z11_Z12_Width { get; set; }
        public int Z11_Z12_Height { get; set; }
        public Panel Z11_Z12Panel { get; set; }
        public string BayNo { get; set; }



        private CoilMessage coilMessageClass = new CoilMessage();
        public FrmSeekCoil()
        {
             InitializeComponent();
            //string str = Application.StartupPath + @"\Eighteen.ssk";
            //this.skinEngine1.SkinFile = str;
            //this.skinEngine1.SkinAllForm = false;
            this.Load += FrmSeekCoil_Load;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmSeekCoil_KeyDown);
        }

        void FrmSeekCoil_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Text = BayNo + "找卷";
        }

        private void btnGetCoil_Click(object sender, EventArgs e)
        {
            string coil = this.txtCoilNo.Text.Trim();

            //if (BayNo == constData.bayNo)
            //{
            //    saddleInStock_Z11_Z12.conInit(Z11_Z12Panel, constData.bayNo, SaddleBase.tagServiceName, constData.Z11_Z12BaySpaceX,
            //        constData.Z11_Z12BaySpaceY, Z11_Z12_Width, Z11_Z12_Height, constData.xAxisRight, constData.yAxisDown, coil);
            //}
            //else
            //{
                
            //}
     
            coilMessageClass.GetLabeTxtByCoil(lblMessage,coil);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //if (BayNo == SaddleBase.bayNo_Z32)
            //{
            //    saddleInStock_Z32.conInit(Z32Panel, SaddleBase.bayNo_Z32, SaddleBase.tagServiceName, SaddleBase.Z32baySpaceX, SaddleBase.Z32baySpaceY, Z32_Width, Z32_Height, SaddleBase.xAxisRight, SaddleBase.yAxisDown);
            //}
            //else if (BayNo == SaddleBase.bayNo_Z33)
            //{
            //    saddleInStock_Z33.conInit(Z33Panel, SaddleBase.bayNo_Z33, SaddleBase.tagServiceName, SaddleBase.Z33baySpaceX, SaddleBase.Z33baySpaceY, Z33_Width, Z33_Height, SaddleBase.xAxisRight, SaddleBase.yAxisDown);
            //}
            
            this.Close();
        }

        private void FrmSeekCoil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnGetCoil_Click(sender, e);
            }
        }


    }
}
