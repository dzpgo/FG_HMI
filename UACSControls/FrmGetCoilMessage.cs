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
using Baosight.iSuperframe.TagService;

namespace UACSControls
{
    public partial class FrmGetCoilMessage : FormBase
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public FrmGetCoilMessage()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmGetCoilMessage_KeyDown);
            

        }

        public FrmGetCoilMessage(string coil)
        {
            InitializeComponent();
            txtCoilNo.Text = coil;
            CoilMessage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CoilMessage();
        }

        private void CoilMessage()
        {
            CoilMessage coilMessage = new CoilMessage();
            coilMessage.GetCoilMessageByCoil(dgvCoilMessage, txtCoilNo.Text.Trim());
        }

        private void FrmGetCoilMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.button1_Click(sender, e);//触发button事件
            }
        }

        private void FrmGetCoilMessage_Load(object sender, EventArgs e)
        {
            //CoilMessage();
        }

        private string strCoil = string.Empty;
        private void button2_Click(object sender, EventArgs e)
        {
            strCoil = txtCoilNo.Text.Trim().ToString();
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add("MatInfQuery", null);
            tagDP.Attach(TagValues);

            if(strCoil.Length == 11 || strCoil.Length == 12)
            {
                tagDP.SetData("MatInfQuery", strCoil);
                MessageBox.Show("已申请！");
            }
            else
            {
                MessageBox.Show("请输入正确的钢卷号！");
            }
            
        }

        private void dgvCoilMessage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if (dgvCoilMessage.Columns[e.ColumnIndex].Name == "COIL_OPEN_DIRECTION")
            {
                string coilNo = null;
                string CoilOpenDir = null;
                if (dgvCoilMessage.Rows[e.RowIndex].Cells["COIL_OPEN_DIRECTION"].Value != DBNull.Value)
                {
                    coilNo = dgvCoilMessage.Rows[e.RowIndex].Cells["COIL_NO"].Value.ToString().Trim();
                    CoilOpenDir = dgvCoilMessage.Rows[e.RowIndex].Cells["COIL_OPEN_DIRECTION"].Value.ToString().Trim();
                    if (CoilOpenDir == "正")
                    {
                        DialogResult dr = MessageBox.Show("确定要修改钢卷" + coilNo + "的开卷方向为反吗？", "修改确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sqlText = @"UPDATE UACS_YARDMAP_COIL SET COIL_OPEN_DIRECTION = 1 WHERE COIL_NO = '" + coilNo + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                            ParkClassLibrary.HMILogger.WriteLog("开卷方向", "修改钢卷" + coilNo + "开卷方向为反", ParkClassLibrary.LogLevel.Info, this.Text);
                        }
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("确定要修改钢卷" + coilNo + "的开卷方向为正吗？", "修改确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sqlText = @"UPDATE UACS_YARDMAP_COIL SET COIL_OPEN_DIRECTION = 0 WHERE COIL_NO = '" + coilNo + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                            ParkClassLibrary.HMILogger.WriteLog("开卷方向", "修改钢卷" + coilNo + "开卷方向为正", ParkClassLibrary.LogLevel.Info, this.Text);
                        }
                    }
                }
                CoilMessage();
            }
        }
    }
}
