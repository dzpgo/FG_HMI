using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.TagService.Interface;
using Baosight.iSuperframe.Forms;

namespace UACSView.CLTSHMI
{
    public partial class FrmCLTSCraneOrder : FormBase
    {
        DataTable dt = new DataTable();
        bool hasSetColumn = false;
        int p = 0;
        private Dictionary<string, int> dicCheck = new Dictionary<string, int>();

        public FrmCLTSCraneOrder()
        {
            InitializeComponent();
        }

        private void FrmCLTSCraneOrder_Load(object sender, EventArgs e)
        {              
            BinddataGridView1Data();
            timer1.Enabled = true;
        }

        private void BinddataGridView1Data()
        {
            string sqlText = @"SELECT 0 AS CHECK_COLUMN, A.ORDER_NO, A.BAY_NO, A.CRANE_NO, A.MAT_NO, A.FROM_STOCK_NO, A.TO_STOCK_NO, B.DESCRIBE DESCRIBE,
                                (CASE A.CMD_STATUS 
                                WHEN 'A' THEN '选中' 
                                WHEN 'S' THEN '吊起' 
                                WHEN 'E' THEN '卸下' 
                                ELSE '待执行'
                                END ) CMD_STATUS 
                               FROM CLTS_ORDER A LEFT JOIN CRANE_ORDER_TASK_DEFINE B ON B.TASK_NAME = A.TASK_NAME order by A.ORDER_NO";                
            dt.Clear();
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dt.Clear();
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (!hasSetColumn)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = rdr.GetName(i);
                            dt.Columns.Add(dc);
                        }
                        dr[i] = rdr[i];
                    }
                    hasSetColumn = true;
                    dt.Rows.Add(dr);
                }
            }
            this.dgvCraneCarOrder.DataSource = dt;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BinddataGridView1Data();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddOrder frm = new FrmAddOrder();
            frm.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除该指令吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvCraneCarOrder.Rows)
                    {
                        if (r.Cells["CHECK_COLUMN"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN"].Value) == 1)
                        {
                            string orderNO = r.Cells["ORDER_NO"].Value.ToString();
                            string orderStatus = r.Cells["CMD_STATUS"].Value.ToString().Trim();
                            if (orderStatus == "吊起" || orderStatus == "卸下")
                            {
                                MessageBox.Show("行车正在执行该指令，无法删除该指令!");
                                return;
                            }
                            string sqlText = @"DELETE FROM CLTS_ORDER WHERE ORDER_NO = '" + orderNO + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                            MessageBox.Show("已删除该指令：" + orderNO + "!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BinddataGridView1Data();
            if (dgvCraneCarOrder.RowCount > p)
            {
                dgvCraneCarOrder.Rows[0].Selected = false;
                dgvCraneCarOrder.FirstDisplayedScrollingRowIndex = p;
                dgvCraneCarOrder.CurrentCell = null;
            }
            dgvCheck(dgvCraneCarOrder);
        }

        private void FrmCLTSCraneOrder_TabActivated(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmCLTSCraneOrder_TabDeactivated(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void FrmCLTSCraneOrder_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }

        private void dgvCraneCarOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dicCheck.Clear();
                for (int i = 0; i < dgvCraneCarOrder.Rows.Count; i++)
                {                   
                    //if (i != e.RowIndex)
                    //{
                    //    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvCraneCarOrder.Rows[i].Cells[0];
                    //    cell.Value = false;
                    //}
                    //else
                    //{
                    //    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvCraneCarOrder.Rows[i].Cells[0];
                    //    cell.Value = true;
                    //}
                    string order_NO = dgvCraneCarOrder.Rows[i].Cells["ORDER_NO"].Value.ToString();
                    bool hasChecked = (bool)dgvCraneCarOrder.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
                    if (hasChecked)
                    {
                        dicCheck[order_NO] = 1;
                    }
                    else
                    {
                        dicCheck[order_NO] = 0;

                    }
                    dgvCraneCarOrder.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[order_NO];
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void dgvCheck(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                foreach (string key in dicCheck.Keys)
                {
                    if (dgv.Rows[i].Cells["ORDER_NO"].Value.ToString() == key)
                    {
                        dgv.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[key];
                    }
                }
            }
        }

       
        
    }
}
