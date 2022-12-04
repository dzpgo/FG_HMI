using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSPopupForm
{
    public partial class FrmHotCoilYard2Yard : Form
    {
        private string craneNO = string.Empty;
        /// <summary>
        /// 行车号
        /// </summary>
        public string CraneNO
        {
            get { return craneNO; }
            set { craneNO = value; }
        }

        public FrmHotCoilYard2Yard()
        {
            InitializeComponent();
        }

        private void FrmHotCoilYard2Yard_Load(object sender, EventArgs e)
        {
            GetCoilStrategy(dgvCoilStrategy);
            ShiftDgvColorByFlag();
        }

        int p = 0; 
        
        public static void GetCoilStrategy(DataGridView _dgv)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT NEME,CRANE_NO,
                               CASE 
                                    WHEN FLAG = 1 THEN '开'
                                    WHEN FLAG = 0 THEN '关'
                               END AS FLAG
                               FROM  YARD_TO_YARD_COL_ROW_NO_ON_OFF WHERE 1=1";             
                dt.Clear();
                dt = new DataTable();

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
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
            }
            catch (Exception)
            {

                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("NEME", typeof(String));               
                dt.Columns.Add("FLAG", typeof(String));              
            }
            _dgv.DataSource = dt;       
        }

        private void dgvCoilStrategy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if (dgvCoilStrategy.Columns[e.ColumnIndex].Name == "FLAG")
            {
                string ROW = null;              
                string FLAG = null;
                if (dgvCoilStrategy.Rows[e.RowIndex].Cells["FLAG"].Value != DBNull.Value)
                {
                    ROW = dgvCoilStrategy.Rows[e.RowIndex].Cells["NEME"].Value.ToString().Trim();                    
                    FLAG = dgvCoilStrategy.Rows[e.RowIndex].Cells["FLAG"].Value.ToString().Trim();
                    if (FLAG == "开")
                    {
                        DialogResult dr = MessageBox.Show("确定要关闭" + ROW + "的倒垛吗？", "修改确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sqlText = @"UPDATE YARD_TO_YARD_COL_ROW_NO_ON_OFF SET FLAG = 0,CRANE_NO = null WHERE NEME = '" + ROW + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                            ParkClassLibrary.HMILogger.WriteLog("关闭热卷区倒垛", "关闭" + ROW + "倒垛", ParkClassLibrary.LogLevel.Info, this.Text);
                        }
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("确定要打开" + ROW + "的倒垛吗？", "修改确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sqlText = @"UPDATE YARD_TO_YARD_COL_ROW_NO_ON_OFF SET FLAG = 1,CRANE_NO = '" + craneNO + "'  WHERE NEME = '" + ROW + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                            ParkClassLibrary.HMILogger.WriteLog("打开热卷区倒垛", "打开" + ROW + "倒垛", ParkClassLibrary.LogLevel.Info, this.Text);
                        }
                    }
                }

                GetCoilStrategy(dgvCoilStrategy);
                ShiftDgvColorByFlag();
                if (dgvCoilStrategy.RowCount > p)
                {
                    dgvCoilStrategy.Rows[0].Selected = false;
                    dgvCoilStrategy.FirstDisplayedScrollingRowIndex = p;
                    dgvCoilStrategy.CurrentCell = null;
                }
            }
        }

        private void dgvCoilStrategy_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }

        private void ShiftDgvColorByFlag()
        {
            for (int i = 0; i < dgvCoilStrategy.Rows.Count; i++)
            {
                if (dgvCoilStrategy.Rows[i].Cells["FLAG"].Value != DBNull.Value)
                {
                    string flag = dgvCoilStrategy.Rows[i].Cells["FLAG"].Value.ToString();
                    if (flag == "开")
                    {
                        //dgvCoilStrategy.Rows[i].Cells["FLAG"].Style.BackColor = Color.Green;
                        dgvCoilStrategy.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        //dgvCoilStrategy.Rows[i].Cells["FLAG"].Style.BackColor = Color.Red;
                        dgvCoilStrategy.Rows[i].DefaultCellStyle.BackColor = Color.MistyRose;
                    }
                }
            }
        }
    }
}
