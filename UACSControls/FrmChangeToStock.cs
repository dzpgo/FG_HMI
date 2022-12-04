using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using UACS;
using Baosight.iSuperframe.TagService;

namespace UACSControls
{
    public partial class FrmChangeToStock : Form
    {
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        public FrmChangeToStock()
        {
            InitializeComponent();
        }
       

        private string craneNo = string.Empty;

        public string CraneNo
        {
            get { return craneNo; }
            set { craneNo = value; }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add("REVISE_TO_STOCK", null);
            tagDP.Attach(TagValues);

            string sql = @"SELECT * FROM UACS_YARDMAP_SADDLE_DEFINE  WHERE SADDLE_NO = '" + txtStockNo.Text.Trim() + "' AND SADDLE_TYPE = 0";
            using(IDataReader dr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while(!dr.Read())
                {
                    lblMessage.Text = "请检查库位号是否正确！";
                    return;
                }
            }
            int stock_Status = 999;
            int lock_Flag = 999;
            try
            {
                string sql1 = @"SELECT * FROM UACS_YARDMAP_STOCK_DEFINE  WHERE STOCK_NO = '" + txtStockNo.Text.Trim() + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql1))
                {
                    while (rdr.Read())
                    {
                        stock_Status = Convert.ToInt32(rdr["STOCK_STATUS"]);
                        lock_Flag = Convert.ToInt32(rdr["LOCK_FLAG"]);

                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            if (stock_Status == 0 && lock_Flag == 0)
            {              
                lblMessage.Text = "库位正常！";
                string messageBuffer = craneNo + "|" + txtStockNo.Text.Trim();
                Baosight.iSuperframe.TagService.DataCollection<object> wirteDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
                wirteDatas["REVISE_TO_STOCK"] = messageBuffer;
                tagDP.SetData("REVISE_TO_STOCK", messageBuffer);
                MessageBox.Show("修改成功！");
                ParkClassLibrary.HMILogger.WriteLog("修改卸下位置", craneNo + "行车修改卸下位置为：" + txtStockNo.Text, ParkClassLibrary.LogLevel.Info, "主监控");
                this.Close();
            }
            else
            {               
                lblMessage.Text = "库位异常-需要盘库！";
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
