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

namespace UACSView.CLTSHMI
{
    public partial class Frm_CLTS_CAR_TO_YARD : FormBase
    {
        public Frm_CLTS_CAR_TO_YARD()
        {
            InitializeComponent();
        }

        #region  -------------------声明字段------------------------------
        private Dictionary<string, int> dicCheck = new Dictionary<string, int>();
        DataTable dt = new DataTable();
        bool hasSetColumn = false;
        private string parkingNo = "";
        private string carNo = "";
        #endregion

        #region  ---------------------方法--------------------------------

        //刷新
        private void Refresh()
        {
            BindDGV(dgvStowageDetail);
            //dgvCheck(dgvStowageDetail);
        }
        //绑定数据表
        private void BindDGV(DataGridView dgv)
        {
            try
            {
                string stowageID = "";
                string strStowage = @"SELECT STOWAGE_ID FROM UACS_TRUCK_STOWAGE ";
                strStowage += " WHERE FRAME_NO = '" + txtCarNo.Text.ToString().Trim() + "' ORDER BY REC_TIME DESC FETCH FIRST 1 ROWS ONLY";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strStowage))
                {
                    while (rdr.Read())
                    {
                        stowageID = rdr["STOWAGE_ID"].ToString().Trim();
                    }
                }
                if(stowageID != "")
                {
                    string strSq = @"SELECT 0 AS CHECK_COLUMN,A.STOWAGE_ID STOWAGE_ID,A.MAT_NO MAT_NO, B.WEIGHT WEIGHT, B.WIDTH WIDTH, B.INDIA INDIA, B.OUTDIA OUTDIA 
                                    FROM UACS_TRUCK_STOWAGE_DETAIL A
                                    LEFT JOIN UACS_YARDMAP_COIL B ON B.COIL_NO = A.MAT_NO ";                
                    strSq += " WHERE A.STOWAGE_ID  = '" + stowageID + "'";
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
        private void Bind_cbbParkingNo(ComboBox comBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //cmbArea.Items.Clear();
            try
            {
                string sqlText = @"SELECT DISTINCT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_PARKINGSITE WHERE YARD_NO = 'Z61'";
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
        private void Frm_CLTS_CAR_TO_YARD_Load(object sender, EventArgs e)
        {
            Bind_cbbParkingNo(cbbParkingNo);
            Refresh();
        }

        private void WMSCraneOrderConfig_TabActivated(object sender, EventArgs e)
        {
            //timer1.Enabled = true;
        }

        private void WMSCraneOrderConfig_TabDeactivated(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
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
                //dicCheck.Clear();
                //for (int i = 0; i < dgvStowageDetail.Rows.Count; i++)
                //{
                //    string order_NO = dgvStowageDetail.Rows[i].Cells["ORDER_NO"].Value.ToString();
                //    bool hasChecked = (bool)dgvStowageDetail.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
                //    if (hasChecked)
                //    {
                //        dicCheck[order_NO] = 1;
                //    }
                //    else
                //    {
                //        dicCheck[order_NO] = 0;

                //    }
                //    dgvStowageDetail.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[order_NO];
                //}
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(cbbParkingNo.Text.Trim() == "")
            {
                MessageBox.Show("请选择停车位！");
                return;
            }
            if(txtCarNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入车号！");
                return;
            }
            parkingNo = cbbParkingNo.Text.Trim();
            carNo = txtCarNo.Text.Trim();
            BindDGV(dgvStowageDetail);

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定选中钢卷生成指令吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    int count = 0;
                    foreach (DataGridViewRow r in dgvStowageDetail.Rows)
                    {
                        if (r.Cells["CHECK_COLUMN"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN"].Value) == 1)
                        {
                            count++;
                            string coilNo = r.Cells["MAT_NO"].Value.ToString().Trim();
                            string sqlInsert = string.Format("INSERT INTO CLTS_CAR_TO_YARD(SQE_NO,MAT_NO,DEST_YARD,PARKING_NO,CAR_NO,STATUS) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", count, coilNo,"Z61", parkingNo, carNo, "0");
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlInsert);
                        }
                    }
                    MessageBox.Show("成功选中" + count.ToString() + "个卷！");
                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定取消选中的钢卷吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    int count = 0;
                    foreach (DataGridViewRow r in dgvStowageDetail.Rows)
                    {
                        if (r.Cells["CHECK_COLUMN"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN"].Value) == 1)
                        {
                            count++;
                            string coilNo = r.Cells["MAT_NO"].Value.ToString().Trim();
                            string sqlDelete = string.Format("DELETE FROM CLTS_CAR_TO_YARD WHERE MAT_NO = '{0}' AND PARKING_NO = '{1}' AND CAR_NO = '{2}'", coilNo, parkingNo, carNo);
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlDelete);
                        }
                    }
                    MessageBox.Show("成功取消" + count.ToString() + "个卷！");
                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
