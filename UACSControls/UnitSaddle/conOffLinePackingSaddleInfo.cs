using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using System.Threading;

namespace UACSControls
{
    public partial class conOffLinePackingSaddleInfo : UserControl
    {
        public conOffLinePackingSaddleInfo()
        {
            InitializeComponent();
        }
        

        private string mySaddleNO;
        /// <summary>
        /// 鞍座号
        /// </summary>
        public string MySaddleNO
        {
            get { return mySaddleNO; }
            set { mySaddleNO = value; }
        }



        public delegate void DelegatePackingSaddleInfo(string _saddleName);

        public void UpPackingSaddleInfo(string _saddleName)
        {
            
            //string crane = OffinePackingSaddleInBay.GetOffLineSaddleInCrane(_saddleName);
            //if (crane == null)
            //    lbl_CraneNo.Text = "";
            //else
            //    lbl_CraneNo.Text = crane; 
            string width = OffinePackingSaddleInBay.GetSaddlePlateWidth(_saddleName);
            if (width == null)
                lbl_PlateWidth.Text = "";
            else
                lbl_PlateWidth.Text = width;
            string packingCode = OffinePackingSaddleInBay.GetPackingCode(_saddleName);
            if (packingCode == null)
                lblPackingCode.Text = "";
            else
                lblPackingCode.Text = packingCode;

            string coilType= OffinePackingSaddleInBay.GetCoilType(_saddleName);
            if (coilType == null)
            {
                lalCoilType.Text = "";
            }
            else
            {
                if(coilType.Contains("2014"))
                {
                    lalCoilType.Text = "普冷";
                }
                else if (coilType.Contains("2015"))
                {
                    lalCoilType.Text = "镀锌";
                }
                else
                {
                    lalCoilType.Text = "";
                }
            }
            //string coiltype = OffinePackingSaddleInBay.GetCoilType(_saddleName);
            //lbl_CoilType.Text = coiltype;


        }
    }
}
