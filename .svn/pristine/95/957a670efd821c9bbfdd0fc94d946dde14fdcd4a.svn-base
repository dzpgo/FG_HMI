using Baosight.iSuperframe.TagService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace UACS
{
    public partial class FrmInsertCoilMessage : Form
    {

        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private const string tagServiceName = "iplature";
        SaddleMethod method = new SaddleMethod();
        private string unitNo = null;
        /// <summary>
        /// 机组号
        /// </summary>
        public string UnitNo
        {
            get { return unitNo; }
            set { unitNo = value; }
        }


        private string bayNo = null;
        /// <summary>
        /// 跨别号
        /// </summary>
        public string BayNo
        {
            get { return bayNo; }
            set { bayNo = value; }
        }
        public FrmInsertCoilMessage()
        {
            InitializeComponent();
        }

        private void FrmInsertCoilMessage_Load(object sender, EventArgs e)
        {
            //窗体固定大小
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

            tagDP.ServiceName = tagServiceName;
            tagDP.AutoRegist = true;
            TagValues.Clear();
            tagDP.Attach(TagValues);

            if (unitNo == "MC1" || unitNo == "MC2")
            {
                this.Text = "吊钢卷上过跨台车";
                label1.Text = "上车计划卷";
                btnStockSelect.Visible = true;
                btnOk.Text = "钢卷上车";
            }
            else
            {
                method.GetCoilLabel(unitNo, lblNextCoil, lblStockNo);
                btnStockSelect.Visible = false;
            }

            method.GetSaddleMessageInDgv(unitNo, bayNo, dataGridView1, true);

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (unitNo == "MC1" || unitNo == "MC2")
            {
                if (lblNextCoil.Text == "999999")
                {
                    MessageBox.Show("没有需要上到过跨台车的钢卷,无法上车");
                    return;
                }
                if (lblStockNo.Text == "999999")
                {
                    MessageBox.Show("没有需要上到过跨台车的库位,无法上车");
                    return;
                }

            }
            else
            {
                if (lblNextCoil.Text == "没有钢卷")
                {
                    MessageBox.Show("没有即将插料的钢卷,无法插料");
                    return;
                }
                if (lblStockNo.Text == "无库位")
                {
                    MessageBox.Show("没有即将插料的钢卷的库位,无法插料");
                    return;
                }

            }
           
            int index = 0;
            int saddleIndex = 999;
            string coilNo = string.Empty;
            string stockNo = string.Empty;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells["Column1"].EditedFormattedValue == true)
                {
                    index++;
                    saddleIndex = i;
                }
            }
            if (saddleIndex == 999)
            {
                MessageBox.Show("请选择一个鞍座进行插料");
                return;
            }

            if (index > 1)
            {
                MessageBox.Show("不能选择多个鞍座进行插料");
                return;
            }

            if (dataGridView1.Rows[saddleIndex].Cells["COIL_NO"].Value != DBNull.Value)
            {
                coilNo = dataGridView1.Rows[saddleIndex].Cells["COIL_NO"].Value.ToString().Trim();
            }
            if (dataGridView1.Rows[saddleIndex].Cells["STOCK_NO"].Value != DBNull.Value)
            {
                stockNo = dataGridView1.Rows[saddleIndex].Cells["STOCK_NO"].Value.ToString().Trim();
            }

            if (unitNo == "D308")
            {
                if (string.IsNullOrEmpty(coilNo))
                {
                    MessageBox.Show("热镀锌入口自动插料，D308WR1A01必须有卷，才能自动插料！");
                    return;
                }
                //else
                //{
                //    if (stockNo != "D308WR1A02")
                //    {
                //        MessageBox.Show("热镀锌入口自动插料，只能放在D308WR1A02才能自动插料！");
                //        return;
                //    }
                //}
            }

            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要进行钢卷插料操作吗？", "操作提示", btn);
            if (dr == DialogResult.OK)
            {            
                if (string.IsNullOrEmpty(coilNo))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("YARD_TO_LINE");
                    sb.Append(",");
                    sb.Append(lblNextCoil.Text);
                    sb.Append(",");
                    sb.Append(lblStockNo.Text);
                    sb.Append(",");
                    sb.Append(stockNo);
                    tagDP.SetData(unitNo + "_SPEC_ACTION", sb.ToString());
                    MessageBox.Show("行车已准备对钢卷" + lblNextCoil.Text + "----插料----到鞍座" + lblStockNo.Text + " ");
                    ParkClassLibrary.HMILogger.WriteLog("机组插料", unitNo + "机组将钢卷" + lblNextCoil.Text + "插料到鞍座" + lblStockNo.Text, ParkClassLibrary.LogLevel.Info, unitNo + "机组");
                    this.Close();
                }
                else
                    MessageBox.Show(stockNo + "有钢卷，无法插料");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStockSelect_Click(object sender, EventArgs e)
        {
            FrmStcokSelectCoil frm = new FrmStcokSelectCoil();
            frm.BayNo = bayNo;
            frm.TransfEvent += frm_TransfEvent;
            frm.ShowDialog();
        }

        void frm_TransfEvent(String stockNo, string coilNo)
        {
            lblNextCoil.Text = coilNo;
            lblStockNo.Text = stockNo;
        }
    }
}
