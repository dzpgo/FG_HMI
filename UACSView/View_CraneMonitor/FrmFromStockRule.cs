using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UACSDAL;

namespace UACSView.View_CraneMonitor
{
    public partial class FrmFromStockRule : Form
    {
        #region 全局变量
        private Dictionary<string, string> MatCode = new Dictionary<string, string>();
        DataTable dtSource = new DataTable();
        private ComboBox comboBox1 = new ComboBox();
        private ComboBox comboBox2 = new ComboBox();
        private ComboBox comboBox3 = new ComboBox();
        private ComboBox comboBox4 = new ComboBox();
        private ComboBox comboBox5 = new ComboBox();
        private ComboBox comboBox6 = new ComboBox();
        //防止弹出信息关闭画面
        bool isPopupMessage = false;
        #endregion
        #region 初始化
        public FrmFromStockRule()
        {
            InitializeComponent();
        }

        private void FrmFromStockRule_Load(object sender, EventArgs e)
        {
            GetFromStockRule();
            GetDataInitial();
        }
        private void GetDataInitial()
        {
            comboBox1.Visible = false;
            comboBox1.Items.Add("抓斗");
            comboBox1.Items.Add("吸盘");
            dataGridView1.Controls.Add(comboBox1);
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);

            comboBox2.Visible = false;
            comboBox2.Items.Add("跨后取料");
            comboBox2.Items.Add("跨口取料");
            //comboBox2.Items.Add("大车方向优先");
            //comboBox2.Items.Add("未知");
            comboBox2.Items.Add("默认");
            dataGridView1.Controls.Add(comboBox2);
            this.comboBox2.SelectedValueChanged += new System.EventHandler(this.comboBox2_SelectedValueChanged);

            comboBox3.Visible = false;
            comboBox3.Items.Add("小地址往大地址");
            comboBox3.Items.Add("大地址往小地址");
            comboBox3.Items.Add("默认");
            dataGridView1.Controls.Add(comboBox3);
            this.comboBox3.SelectedValueChanged += new System.EventHandler(this.comboBox3_SelectedValueChanged);

            comboBox4.Visible = false;
            comboBox4.Items.Add("小地址往大地址");
            comboBox4.Items.Add("大地址往小地址");
            comboBox4.Items.Add("默认");
            dataGridView1.Controls.Add(comboBox4);
            this.comboBox4.SelectedValueChanged += new System.EventHandler(this.comboBox4_SelectedValueChanged);

            comboBox5.Visible = false;
            comboBox5.Items.Add("小地址往大地址");
            comboBox5.Items.Add("大地址往小地址");
            comboBox5.Items.Add("默认");
            dataGridView1.Controls.Add(comboBox5);
            this.comboBox5.SelectedValueChanged += new System.EventHandler(this.comboBox5_SelectedValueChanged);

            comboBox6.Visible = false;
            comboBox6.Items.Add("小地址往大地址");
            comboBox6.Items.Add("大地址往小地址");
            comboBox6.Items.Add("默认");
            dataGridView1.Controls.Add(comboBox6);
            this.comboBox6.SelectedValueChanged += new System.EventHandler(this.comboBox6_SelectedValueChanged);
        }
        private void GetFromStockRule()
        {
            dtSource.Clear();
            var sqlText = @"SELECT CRANE_NO, BAY_NO, 
                            CASE CLAMP_TYPE
                            WHEN 0 THEN '抓斗' 
                            WHEN 1 THEN '吸盘' 
                            ELSE '' 
                            END AS CLAMP_TYPE 
                            , SEQ_NO, WORK_POS_X_START, WORK_POS_X_END, CLAMP_LENGTH, CLAMP_WIDTH, 
                            CASE UP_MAT_DIR_1
                            WHEN 0 THEN '小地址往大地址' 
                            WHEN 1 THEN '大地址往小地址' 
                            ELSE '默认' 
                            END AS UP_MAT_DIR_1 , 
                            CASE DOWN_MAT_DIR_1 
                            WHEN 0 THEN '小地址往大地址' 
                            WHEN 1 THEN '大地址往小地址' 
                            ELSE '默认' 
                            END AS DOWN_MAT_DIR_1 , 
                            CASE UP_MAT_DIR_2 
                            WHEN 0 THEN '小地址往大地址' 
                            WHEN 1 THEN '大地址往小地址' 
                            ELSE '默认' 
                            END AS UP_MAT_DIR_2 , 
                            CASE DOWN_MAT_DIR_2 
                            WHEN 0 THEN '小地址往大地址' 
                            WHEN 1 THEN '大地址往小地址' 
                            ELSE '默认' 
                            END AS DOWN_MAT_DIR_2 , 
                            UP_MAT_LIMIT_1, DOWN_MAT_LIMIT_1, UP_MAT_LIMIT_2, 
                            DOWN_MAT_LIMIT_2, UP_MAT_LIMIT_3, DOWN_MAT_LIMIT_3, UP_MAT_LIMIT_4, DOWN_MAT_LIMIT_4, 
                            CASE STARTEGY_TYPE
                            WHEN 0 THEN '跨后取料' 
                            WHEN 1 THEN '跨口取料' 
                            WHEN 2 THEN '大车方向优先' 
                            WHEN 3 THEN '默认' 
                            ELSE '默认' 
                            END AS STARTEGY_TYPE 
                            FROM UACSAPP.UACS_CRANE_DEFINE ORDER BY CRANE_NO ASC ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dtSource.Load(rdr);
            }
            dataGridView1.DataSource = dtSource;
            
        }
        #endregion

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox1.Left = rect.Left;
                    comboBox1.Top = rect.Top;
                    comboBox1.Width = rect.Width;
                    comboBox1.Height = rect.Height;
                    comboBox1.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox1.Visible = true;
                }
                else 
                    comboBox1.Visible = false;
                if (this.dataGridView1.CurrentCell.ColumnIndex == 20)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox2.Left = rect.Left;
                    comboBox2.Top = rect.Top;
                    comboBox2.Width = rect.Width;
                    comboBox2.Height = rect.Height;
                    comboBox2.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox2.Visible = true;
                }
                else
                    comboBox2.Visible = false;
                if (this.dataGridView1.CurrentCell.ColumnIndex == 8)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox3.Left = rect.Left;
                    comboBox3.Top = rect.Top;
                    comboBox3.Width = rect.Width;
                    comboBox3.Height = rect.Height;
                    comboBox3.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox3.Visible = true;
                }
                else
                    comboBox3.Visible = false;
                if (this.dataGridView1.CurrentCell.ColumnIndex == 9)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox4.Left = rect.Left;
                    comboBox4.Top = rect.Top;
                    comboBox4.Width = rect.Width;
                    comboBox4.Height = rect.Height;
                    comboBox4.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox4.Visible = true;
                }
                else
                    comboBox4.Visible = false;
                if (this.dataGridView1.CurrentCell.ColumnIndex == 10)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox5.Left = rect.Left;
                    comboBox5.Top = rect.Top;
                    comboBox5.Width = rect.Width;
                    comboBox5.Height = rect.Height;
                    comboBox5.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox5.Visible = true;
                }
                else
                    comboBox5.Visible = false;
                if (this.dataGridView1.CurrentCell.ColumnIndex == 11)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox6.Left = rect.Left;
                    comboBox6.Top = rect.Top;
                    comboBox6.Width = rect.Width;
                    comboBox6.Height = rect.Height;
                    comboBox6.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox6.Visible = true;
                }
                else
                    comboBox6.Visible = false;
            }
            catch
            {
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox1.SelectedItem.ToString();
        }
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox2.SelectedItem.ToString();
        }
        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox3.SelectedItem.ToString();
        }
        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox4.SelectedItem.ToString();
        }
        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox5.SelectedItem.ToString();
        }
        private void comboBox6_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox6.SelectedItem.ToString();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Refurbish_Click(object sender, EventArgs e)
        {
            GetFromStockRule();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Save_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (dataGridView1.DataSource != null && dtSource != null && dataGridView1.Rows.Count > 0 && dtSource.Rows.Count > 0)
            {
                try
                {
                    //确认提示
                    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                    DialogResult drmsg = MessageBox.Show("确认是否修改数据？", "提示", btn, MessageBoxIcon.Asterisk);
                    if (drmsg == DialogResult.OK)
                    {
                        var craneNo = "";
                        foreach (DataGridViewRow dgr in dataGridView1.Rows)
                        {
                            var clampType = dgr.Cells["CLAMP_TYPE"].Value.ToString().Trim().Equals("抓斗") ? 0 : 1;
                            var startegy = dgr.Cells["STARTEGY_TYPE"].Value.ToString().Trim();
                            var startegyType = startegy.Equals("跨后取料") ? 0 : startegy.Equals("跨口取料") ? 1 : startegy.Equals("大车方向优先") ? 2 : 3;
                            var UpMatDir1 = dgr.Cells["UP_MAT_DIR_1"].Value.ToString().Trim().Equals("小地址往大地址") ? 0 : 1;
                            var DownMatDir1 = dgr.Cells["DOWN_MAT_DIR_1"].Value.ToString().Trim().Equals("小地址往大地址") ? 0 : 1;
                            var UpMatDir2 = dgr.Cells["UP_MAT_DIR_2"].Value.ToString().Trim().Equals("小地址往大地址") ? 0 : 1;
                            var DownMatDir2 = dgr.Cells["DOWN_MAT_DIR_2"].Value.ToString().Trim().Equals("小地址往大地址") ? 0 : 1;

                            string ExeSql = @" UPDATE UACS_CRANE_DEFINE SET ";
                            //ExeSql += "BAY_NO = '" + dgr.Cells["BAY_NO"].Value.ToString() + "' ";
                            ExeSql += "CLAMP_TYPE = '" + clampType + "' ";
                            ExeSql += ",SEQ_NO = '" + dgr.Cells["SEQ_NO"].Value.ToString().Trim() + "' ";
                            ExeSql += ",WORK_POS_X_START = " + (string.IsNullOrEmpty(dgr.Cells["WORK_POS_X_START"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["WORK_POS_X_START"].Value.ToString().Trim())) + " ";
                            ExeSql += ",WORK_POS_X_END = " + (string.IsNullOrEmpty(dgr.Cells["WORK_POS_X_END"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["WORK_POS_X_END"].Value.ToString().Trim())) + " ";
                            ExeSql += ",CLAMP_LENGTH = " + (string.IsNullOrEmpty(dgr.Cells["CLAMP_LENGTH"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["CLAMP_LENGTH"].Value.ToString().Trim())) + " ";
                            ExeSql += ",CLAMP_WIDTH = " + (string.IsNullOrEmpty(dgr.Cells["CLAMP_WIDTH"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["CLAMP_WIDTH"].Value.ToString().Trim())) + " ";
                            ExeSql += ",UP_MAT_DIR_1 = '" + UpMatDir1 + "' ";
                            ExeSql += ",DOWN_MAT_DIR_1 = '" + DownMatDir1 + "' ";
                            ExeSql += ",UP_MAT_DIR_2 = '" + UpMatDir2 + "' ";
                            ExeSql += ",DOWN_MAT_DIR_2 = '" + DownMatDir2 + "' ";
                            ExeSql += ",UP_MAT_LIMIT_1 = " + (string.IsNullOrEmpty(dgr.Cells["UP_MAT_LIMIT_1"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["UP_MAT_LIMIT_1"].Value.ToString().Trim())) + " ";
                            ExeSql += ",DOWN_MAT_LIMIT_1 = " + (string.IsNullOrEmpty(dgr.Cells["DOWN_MAT_LIMIT_1"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["DOWN_MAT_LIMIT_1"].Value.ToString().Trim())) + " ";
                            ExeSql += ",UP_MAT_LIMIT_2 = " + (string.IsNullOrEmpty(dgr.Cells["UP_MAT_LIMIT_2"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["UP_MAT_LIMIT_2"].Value.ToString().Trim())) + " ";
                            ExeSql += ",DOWN_MAT_LIMIT_2 = " + (string.IsNullOrEmpty(dgr.Cells["DOWN_MAT_LIMIT_2"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["DOWN_MAT_LIMIT_2"].Value.ToString().Trim())) + " ";
                            ExeSql += ",UP_MAT_LIMIT_3 = " + (string.IsNullOrEmpty(dgr.Cells["UP_MAT_LIMIT_3"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["UP_MAT_LIMIT_3"].Value.ToString().Trim())) + " ";
                            ExeSql += ",DOWN_MAT_LIMIT_3 = " + (string.IsNullOrEmpty(dgr.Cells["DOWN_MAT_LIMIT_3"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["DOWN_MAT_LIMIT_3"].Value.ToString().Trim())) + " ";
                            ExeSql += ",UP_MAT_LIMIT_4 = " + (string.IsNullOrEmpty(dgr.Cells["UP_MAT_LIMIT_4"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["UP_MAT_LIMIT_4"].Value.ToString().Trim())) + " ";
                            ExeSql += ",DOWN_MAT_LIMIT_4 = " + (string.IsNullOrEmpty(dgr.Cells["DOWN_MAT_LIMIT_4"].Value.ToString().Trim()) ? 0 : Convert.ToInt32(dgr.Cells["DOWN_MAT_LIMIT_4"].Value.ToString().Trim())) + " ";
                            ExeSql += ",STARTEGY_TYPE = '" + startegyType + "' ";

                            ExeSql += " WHERE 1=1 ";
                            ExeSql += " AND CRANE_NO = '" + dgr.Cells["CRANE_NO"].Value.ToString().Trim() + "'";
                            ExeSql += " AND BAY_NO = '" + dgr.Cells["BAY_NO"].Value.ToString().Trim() + "' ";
                            

                            DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
                            craneNo += dgr.Cells["CRANE_NO"].Value.ToString().Trim() + "  ";

                        }
                        ParkClassLibrary.HMILogger.WriteLog("取料规则", "修改取料规则，行车号：" + craneNo, ParkClassLibrary.LogLevel.Info, this.Text);
                        GetFromStockRule(); //刷新页面
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxButtons btn = MessageBoxButtons.OK;
                    DialogResult drmsg = MessageBox.Show("保存失败，无数据更改！", "提示", btn, MessageBoxIcon.Warning);
                }
            }
            isPopupMessage = false;
        }
    }
}
