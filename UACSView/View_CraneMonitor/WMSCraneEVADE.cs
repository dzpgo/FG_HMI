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
using UACSControls;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using System.Threading;

namespace UACSView.View_CraneMonitor
{
    public partial class WMSCraneEVADE : FormBase
    {
        public WMSCraneEVADE()
        {
            InitializeComponent();
        }

        #region  -------------------声明字段------------------------------
        private Dictionary<string, int> dicCheck = new Dictionary<string, int>();
        DataTable dt = new DataTable();
        bool hasSetColumn = false;
        #endregion

        #region  ---------------------方法--------------------------------

        //刷新
        private void Refresh()
        {
            BindDGV(dgvWMSCraneOrder);
            dgvCheck(dgvWMSCraneOrder);
        }
        //绑定数据表
        private void BindDGV(DataGridView dgv)
        {
            try
            {
                string strSq = " SELECT 0 AS CHECK_COLUMN, A.ORDER_NUMBER ORDER_NUMBER, A.BAY_NO BAY_NO, A.ALTERN_CRANE_NO ALTERN_CRANE_NO, A.CRANE_NO CRANE_NO, A.TASK_PRIORITY TASK_PRIORITY, A.START_POINT START_POINT, A.END_POINT END_POINT, A.ORDER_SOURCE ORDER_SOURCE, A.SOUR_CRANE SOUR_CRANE FROM WMS_CRANE_EVADE A";
                strSq += " where A.BAY_NO='" + cbbBayNo.SelectedValue.ToString().Trim() + "' order by A.ORDER_NUMBER";
                dt.Clear(); 
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSq))
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
                dgv.DataSource = dt;
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        //绑定DGV选项
        private void dgvCheck(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                foreach (string key in dicCheck.Keys)
                {
                    if (dgv.Rows[i].Cells["ORDER_NUMBER"].Value.ToString() == key)
                    {
                        dgv.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[key];
                    }
                }
            }
        }

        //绑定跨号
        private void Bind_cbbBayNo(ComboBox comBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //cmbArea.Items.Clear();
            try
            {
                string sqlText = @"SELECT DISTINCT BAY_NO as TypeValue,BAY_NAME as TypeName FROM UACS_YARDMAP_BAY_DEFINE";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();

                        if (rdr["TypeName"].ToString().Trim() != "")
                        {
                            dr["TypeValue"] = rdr["TypeValue"];
                            dr["TypeName"] = rdr["TypeName"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                //绑定列表下拉框数据
                comBox.DataSource = dt;
                comBox.DisplayMember = "TypeName";
                comBox.ValueMember = "TypeValue";
                comBox.SelectedItem = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        #endregion

        #region  ---------------------事件--------------------------------
        #endregion
        private void WMSCraneOrderConfig_Load(object sender, EventArgs e)
        {
            Bind_cbbBayNo(cbbBayNo);
            Refresh();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void WMSCraneOrderConfig_TabActivated(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void WMSCraneOrderConfig_TabDeactivated(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void dgvWMSCraneOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 1 && e.RowIndex != -1)
                //{
                //    foreach (DataGridViewRow item in dgvWMSCraneOrder.Rows)
                //    {
                //        if (item.Index == e.RowIndex)
                //        {
                //            item.Cells[0].Value = true;
                //        }
                //        else
                //        {
                //            item.Cells[0].Value = false;
                //        }
                //    }
                //}
                dicCheck.Clear();
                for (int i = 0; i < dgvWMSCraneOrder.Rows.Count; i++)
                {
                    string order_NO = dgvWMSCraneOrder.Rows[i].Cells["ORDER_NUMBER"].Value.ToString();
                    bool hasChecked = (bool)dgvWMSCraneOrder.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
                    if (hasChecked)
                    {
                        dicCheck[order_NO] = 1;
                    }
                    else
                    {
                        dicCheck[order_NO] = 0;

                    }
                    dgvWMSCraneOrder.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[order_NO];
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void cbbBayNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除勾选的指令？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvWMSCraneOrder.Rows)
                    {
                        if (r.Cells["CHECK_COLUMN"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN"].Value) == 1)
                        {
                            string orderCrane = r.Cells["CRANE_NO"].Value.ToString().Trim();
                            if(orderCrane != "")
                            {
                                MessageBox.Show("指令处于正在执行状态，无法删除!");
                                return;
                            }
                            string orderNO = r.Cells["ORDER_NUMBER"].Value.ToString().Trim();
                            string sqlOrder = string.Format("DELETE FROM WMS_CRANE_EVADE WHERE ORDER_NUMBER = '{0}'", orderNO);
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlOrder);

                            MessageBox.Show("成功删除指令！");
                        }
                    }                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnModifyPriority_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定提升指令优先级？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvWMSCraneOrder.Rows)
                    {
                        if (r.Cells["CHECK_COLUMN"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN"].Value) == 1)
                        {
                            string orderCrane = r.Cells["CRANE_NO"].Value.ToString().Trim();
                            if (orderCrane != "")
                            {
                                MessageBox.Show("指令处于正在执行状态，无法提升!");
                                return;
                            }

                            string evadePriority = r.Cells["TASK_PRIORITY"].Value.ToString();
                            string orderNO = r.Cells["ORDER_NUMBER"].Value.ToString();

                            FrmCraneEvadePriority FrmEvadePriority = new FrmCraneEvadePriority();
                            FrmEvadePriority.OrderNO = orderNO;
                            FrmEvadePriority.CraneEvadePriority = evadePriority;
                            FrmEvadePriority.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
