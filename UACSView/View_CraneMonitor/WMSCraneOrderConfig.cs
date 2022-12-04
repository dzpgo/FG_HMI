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
    public partial class WMSCraneOrderConfig : FormBase
    {
        public WMSCraneOrderConfig()
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
                string strSq = @"SELECT 0 AS CHECK_COLUMN, A.ORDER_NO ORDER_NO, A.BAY_NO BAY_NO, A.TASK_NAME TASK_NAME, B.DESCRIBE DESCRIBE, A.DEFAULT_CRANE_NO DEFAULT_CRANE_NO, A.CRANE_NO CRANE_NO, A.MAT_NO MAT_NO, A.ORDER_PRIORITY ORDER_PRIORITY, A.FROM_STOCK_NO FROM_STOCK_NO, A.TO_STOCK_NO TO_STOCK_NO, A.CMD_STATUS CMD_STATUS 
                               FROM WMS_CRANE_ORDER A LEFT JOIN CRANE_ORDER_TASK_DEFINE B ON B.TASK_NAME = A.TASK_NAME";
                strSq += " where A.BAY_NO='" + cbbBayNo.SelectedValue.ToString().Trim() + "' order by A.ORDER_NO";
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
                    if (dgv.Rows[i].Cells["ORDER_NO"].Value.ToString() == key)
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
                    string order_NO = dgvWMSCraneOrder.Rows[i].Cells["ORDER_NO"].Value.ToString();
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
                            string craneNO = r.Cells["CRANE_NO"].Value.ToString().Trim();
                            if(craneNO != "")
                            {
                                MessageBox.Show("指令处于正在执行状态，无法删除!");
                                return;
                            }

                            string coilNO = r.Cells["MAT_NO"].Value.ToString();
                            string sqlCoil = string.Format("Update UACS_YARDMAP_STOCK_DEFINE set BOOKING_MAT_NO = NULL, BOOKING_CRANE_NO = NULL, BOOKING_FLAG = 0 where BOOKING_MAT_NO = '{0}'", coilNO);
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlCoil);
                            string orderNO = r.Cells["ORDER_NO"].Value.ToString();
                            string sqlOrder = string.Format("DELETE FROM WMS_CRANE_ORDER WHERE ORDER_NO = '{0}'", orderNO);
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
                            string craneNO = r.Cells["CRANE_NO"].Value.ToString().Trim();
                            if (craneNO != "")
                            {
                                MessageBox.Show("指令处于正在执行状态，无法提升!");
                                return;
                            }

                            string orderPriority = r.Cells["ORDER_PRIORITY"].Value.ToString();
                            string orderNO = r.Cells["ORDER_NO"].Value.ToString();

                            FrmOrderPriority FrmPriority = new FrmOrderPriority();
                            FrmPriority.OrderNO = orderNO;
                            FrmPriority.OrderPriority = orderPriority;
                            FrmPriority.ShowDialog();

                            //string Priority = String.Empty;
                            //string sqlPriority = string.Format("Update WMS_CRANE_ORDER set ORDER_PRIORITY = '{0}' where ORDER_NO = '{1}'", orderNO, Priority);
                            //DB2Connect.DBHelper.ExecuteNonQuery(sqlPriority);

                            //MessageBox.Show("成功提升指令优先级！");
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
