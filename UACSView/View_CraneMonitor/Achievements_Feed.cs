using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACS;
using Baosight.iSuperframe.Forms;
using UACSDAL;
using System.Windows.Forms.VisualStyles;
using Baosight.iSuperframe.Common;
using System.Xml;
using Microsoft.Office.Interop.Outlook;
using System.Windows.Markup;
using System.Windows.Documents;
//using Microsoft.Office.Interop.Excel;

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 装料实际查询
    /// </summary>
    public partial class Achievements_Feed : FormBase
    {
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        /// <summary>
        /// Tag
        /// </summary>
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public Achievements_Feed()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            SetNull();
            dataGridView1.DataSource =  BindMatStockByL3Stowage(tb_Query_PlanNo.Text, cmb_Query_CarNo.Text);
        }

        #region 初始化
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Achievements_Feed_Load(object sender, EventArgs e)
        {
            //this.dtp_StartTime.Value = DateTime.Now.AddDays(-1);
            this.dtp_StartTime.Value = DateTime.Now;
            this.dtp_EndTime.Value = DateTime.Now;
            //绑定槽号
            BindCmbCar();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="datagridView"></param>
        /// <returns></returns>
        private DataTable InitDataTable(DataGridView datagridView)
        {
            DataTable dataTable = new DataTable();
            foreach (DataGridViewColumn dgvColumn in datagridView.Columns)
            {
                DataColumn dtColumn = new DataColumn();
                if (!dgvColumn.GetType().Equals(typeof(DataGridViewCheckBoxColumn)))
                {
                    dtColumn.ColumnName = dgvColumn.Name.ToUpper();
                    dtColumn.DataType = typeof(String);
                    dataTable.Columns.Add(dtColumn);
                }
                else
                {
                    dtColumn.ColumnName = dgvColumn.Name.ToUpper();
                    //dtColumn.DataType = typeof(bool);
                    dataTable.Columns.Add(dtColumn);
                }
            }

            return dataTable;
        }
        #endregion

        #region 点击事件
        /// <summary>
        /// 查询 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Query_Click(object sender, EventArgs e)
        {
            SetNull();
            dataGridView1.DataSource = BindMatStockByL3Stowage(tb_Query_PlanNo.Text, cmb_Query_CarNo.Text);
            
        }
        /// <summary>
        /// 重置 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Resetting_Click(object sender, EventArgs e)
        {
            this.tb_Query_PlanNo.Text = string.Empty;
            this.cmb_Query_CarNo.Text = string.Empty;
            this.dtp_StartTime.Value = DateTime.Now;
            this.dtp_EndTime.Value = DateTime.Now;
            SetNull();
            dataGridView1.DataSource = BindMatStockByL3Stowage(tb_Query_PlanNo.Text, cmb_Query_CarNo.Text);
        }
        /// <summary>
        /// 查询保存 实绩重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_QuerySave_Click(object sender, EventArgs e)
        {
            Update_UACS_ORDER_QUEUE();
        }
        /// <summary>
        /// 查询发送 实绩重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_QuerySend_Click(object sender, EventArgs e)
        {
            Send_Tag();
        }
        /// <summary>
        /// 保存 实绩重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Save_Click(object sender, EventArgs e)
        {
            Update_UACS_ORDER_QUEUE();
        }
        /// <summary>
        /// 发送 实绩重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Send_Click(object sender, EventArgs e)
        {
            Send_Tag();
        }

        /// <summary>
        /// 更新指令表的实绩重量
        /// </summary>
        private void Update_UACS_ORDER_QUEUE()
        {
            if (string.IsNullOrEmpty(tb_PlanNo.Text.ToString().Trim()))
            {
                MessageBox.Show("请选择计划！");
                return;
            }
            //确认提示
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否保存？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                if (dataGridView1.SelectedCells.Count != 0)
                {
                    ////得到选中行的索引
                    int intRow = dataGridView1.SelectedCells[0].RowIndex;
                    ////得到列的索引
                    //int intColumn = dataGridView1.SelectedCells[0].ColumnIndex;
                    //var sss = dataGridView1.Rows[intRow].Cells["cbChoice"].Value.ToString();
                    //dataGridView1.Rows[intRow].Cells["cbChoice"].Value = 1;
                    //var dddd = dataGridView1.Rows[intRow].Cells["cbChoice"].Value.ToString();
                    //得到选中行某列的值
                    //string str = dataGridView1.CurrentRow.Cells[2].Value.ToString();

                    string gPlanNO = dataGridView1.CurrentRow.Cells["GPlanNO"].Value.ToString();
                    string cbChoice = dataGridView1.CurrentRow.Cells["cbChoice"].Value.ToString();
                    if (string.IsNullOrEmpty(gPlanNO) || !tb_PlanNo.Text.ToString().Trim().Equals(gPlanNO))
                    {
                        MessageBox.Show("请选择计划！");
                        return;
                    }
                    if (!cbChoice.Equals("1"))
                    {
                        MessageBox.Show("请选择计划！");
                        return;
                    }

                    string gMatCode_1 = dataGridView1.CurrentRow.Cells["GMatCode_1"].Value.ToString();
                    string gMatCode_2 = dataGridView1.CurrentRow.Cells["GMatCode_2"].Value.ToString();
                    string gMatCode_3 = dataGridView1.CurrentRow.Cells["GMatCode_3"].Value.ToString();
                    string gMatCode_4 = dataGridView1.CurrentRow.Cells["GMatCode_4"].Value.ToString();
                    string gMatCode_5 = dataGridView1.CurrentRow.Cells["GMatCode_5"].Value.ToString();
                    string gMatCode_6 = dataGridView1.CurrentRow.Cells["GMatCode_6"].Value.ToString();
                    string gMatCode_7 = dataGridView1.CurrentRow.Cells["GMatCode_7"].Value.ToString();
                    string gMatCode_8 = dataGridView1.CurrentRow.Cells["GMatCode_8"].Value.ToString();
                    string gMatCode_9 = dataGridView1.CurrentRow.Cells["GMatCode_9"].Value.ToString();
                    string gMatCode_10 = dataGridView1.CurrentRow.Cells["GMatCode_10"].Value.ToString();
                    string sqlSave = "";
                    if (!string.IsNullOrEmpty(gMatCode_1))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_1.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_1 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_1"].Value = tb_Feed_Weight_1.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_2))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_2.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_2 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_2"].Value = tb_Feed_Weight_2.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_3))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_3.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_3 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_3"].Value = tb_Feed_Weight_3.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_4))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_4.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_4 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_4"].Value = tb_Feed_Weight_4.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_5))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_5.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_5 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_5"].Value = tb_Feed_Weight_5.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_6))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_6.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_6 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_6"].Value = tb_Feed_Weight_6.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_7))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_7.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_7 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_7"].Value = tb_Feed_Weight_7.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_8))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_8.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_8 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_8"].Value = tb_Feed_Weight_8.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_9))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_9.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_9 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_9"].Value = tb_Feed_Weight_9.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(gMatCode_10))
                    {
                        sqlSave += @"UPDATE UACS_ORDER_QUEUE SET ACT_WEIGHT = '" + tb_Feed_Weight_10.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "' AND MAT_CODE = '" + gMatCode_10 + "'; ";
                        dataGridView1.CurrentRow.Cells["Act_Weight_10"].Value = tb_Feed_Weight_10.Text.Trim(); //更新dataGridView1实绩重量
                    }
                    if (!string.IsNullOrEmpty(sqlSave))
                    {
                        var data = DB2Connect.DBHelper.ExecuteNonQuery(sqlSave);
                        DialogResult dr = MessageBox.Show("装料实绩保存成功！", "提示", MessageBoxButtons.OK);                
                    }
                    //更改料槽车号
                    string gCarNo = dataGridView1.CurrentRow.Cells["GCarNo"].Value.ToString();
                    if (!gCarNo.Equals(tb_CarNo.Text.Trim()))
                    {
                        var sql2 = @"UPDATE UACS_L3_MAT_OUT_INFO SET CAR_NO = '" + tb_CarNo.Text.Trim() + "' WHERE PLAN_NO = '" + gPlanNO + "'; ";
                        var data = DB2Connect.DBHelper.ExecuteNonQuery(sql2);
                        if (data > 0)
                            dataGridView1.CurrentRow.Cells["GCarNo"].Value = tb_CarNo.Text.Trim();
                    }
                    ParkClassLibrary.HMILogger.WriteLog("装料实绩查询", "装料实绩保存", ParkClassLibrary.LogLevel.Info, this.Text);
                    //刷新
                    RefreshGridView();
                }
            }
        }
        /// <summary>
        /// 刷新 dataGridView1
        /// </summary>
        private void RefreshGridView()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate ()
                {
                    RefreshGridView();
                });
            }
            else
                dataGridView1.Refresh();
        }
        /// <summary>
        /// 发送Tag 实绩重量
        /// </summary>
        private void Send_Tag()
        {
            if (string.IsNullOrEmpty(tb_PlanNo.Text.ToString().Trim()))
            {
                MessageBox.Show("请选择计划！");
                return;
            }
            //确认提示
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否发送？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                if (dataGridView1.SelectedCells.Count != 0)
                {
                    //Tag发送的数据
                    var Data = "";
                    //计划号
                    string gPlanNO = dataGridView1.CurrentRow.Cells["GPlanNO"].Value.ToString();
                    //料槽车号
                    string gCarNo = dataGridView1.CurrentRow.Cells["GCarNo"].Value.ToString();
                    //停车位号（落料位）
                    string gTO_STOCK_NO = dataGridView1.CurrentRow.Cells["TO_STOCK_NO"].Value.ToString();
                    //选择的行
                    string cbChoice = dataGridView1.CurrentRow.Cells["cbChoice"].Value.ToString();
                    if (string.IsNullOrEmpty(gPlanNO) || !tb_PlanNo.Text.ToString().Trim().Equals(gPlanNO))
                    {
                        MessageBox.Show("请选择计划！");
                        return;
                    }
                    if (!cbChoice.Equals("1"))
                    {
                        MessageBox.Show("请选择计划！");
                        return;
                    }
                    //计划号，料槽号，工位号，废钢种类数量，废钢代码1#重量，废钢代码2#重量，废钢代码3#重量，废钢代码4#重量，废钢代码5#重量......
                    Data += gPlanNO + "," + gCarNo + "," + gTO_STOCK_NO;
                    string gMatCode_1 = dataGridView1.CurrentRow.Cells["GMatCode_1"].Value.ToString();
                    string gMatCode_2 = dataGridView1.CurrentRow.Cells["GMatCode_2"].Value.ToString();
                    string gMatCode_3 = dataGridView1.CurrentRow.Cells["GMatCode_3"].Value.ToString();
                    string gMatCode_4 = dataGridView1.CurrentRow.Cells["GMatCode_4"].Value.ToString();
                    string gMatCode_5 = dataGridView1.CurrentRow.Cells["GMatCode_5"].Value.ToString();
                    string gMatCode_6 = dataGridView1.CurrentRow.Cells["GMatCode_6"].Value.ToString();
                    string gMatCode_7 = dataGridView1.CurrentRow.Cells["GMatCode_7"].Value.ToString();
                    string gMatCode_8 = dataGridView1.CurrentRow.Cells["GMatCode_8"].Value.ToString();
                    string gMatCode_9 = dataGridView1.CurrentRow.Cells["GMatCode_9"].Value.ToString();
                    string gMatCode_10 = dataGridView1.CurrentRow.Cells["GMatCode_10"].Value.ToString();
                    int coun = 0;
                    Data += ",{0}";
                    if (!string.IsNullOrEmpty(gMatCode_1))
                    {
                        Data += "," + gMatCode_1 + "#" + tb_Feed_Weight_1.Text;
                        coun = coun + 1;

                    }
                    if (!string.IsNullOrEmpty(gMatCode_2))
                    {
                        Data += "," + gMatCode_2 + "#" + tb_Feed_Weight_2.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_3))
                    {
                        Data += "," + gMatCode_3 + "#" + tb_Feed_Weight_3.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_4))
                    {
                        Data += "," + gMatCode_4 + "#" + tb_Feed_Weight_4.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_5))
                    {
                        Data += "," + gMatCode_5 + "#" + tb_Feed_Weight_5.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_6))
                    {
                        Data += "," + gMatCode_6 + "#" + tb_Feed_Weight_6.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_7))
                    {
                        Data += "," + gMatCode_7 + "#" + tb_Feed_Weight_7.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_8))
                    {
                        Data += "," + gMatCode_8 + "#" + tb_Feed_Weight_8.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_9))
                    {
                        Data += "," + gMatCode_9 + "#" + tb_Feed_Weight_9.Text;
                        coun = coun + 1;
                    }
                    if (!string.IsNullOrEmpty(gMatCode_10))
                    {
                        Data += "," + gMatCode_10 + "#" + tb_Feed_Weight_10.Text;
                        coun = coun + 1;
                    }
                    Data = string.Format(Data, coun);
                    tagDP.SetData("EV_L3MSG_SND_BHKI02", Data);
                    DialogResult dr = MessageBox.Show("装料实绩发送成功！", "提示", MessageBoxButtons.OK);
                    ParkClassLibrary.HMILogger.WriteLog("装料实绩发送", "装料实绩发送：" + Data, ParkClassLibrary.LogLevel.Info, this.Text);
                }
            }
        }

        #region 实绩重量值发生改变时重新计算
        /// <summary>
        /// 实绩重量1 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_1_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量2 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_2_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量3 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_3_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量4 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_4_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量5 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_5_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量6 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_6_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量7 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_7_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量8 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_8_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量9 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_9_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 实绩重量10 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Feed_Weight_10_TextChanged(object sender, EventArgs e)
        {
            Update_Feed_Weight();
        }
        /// <summary>
        /// 更新实绩总量
        /// </summary>
        private void Update_Feed_Weight()
        {
            long fw_1 = !string.IsNullOrEmpty(tb_Feed_Weight_1.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_1.Text.ToString().Trim()) : 0;
            long fw_2 = !string.IsNullOrEmpty(tb_Feed_Weight_2.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_2.Text.ToString().Trim()) : 0;
            long fw_3 = !string.IsNullOrEmpty(tb_Feed_Weight_3.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_3.Text.ToString().Trim()) : 0;
            long fw_4 = !string.IsNullOrEmpty(tb_Feed_Weight_4.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_4.Text.ToString().Trim()) : 0;
            long fw_5 = !string.IsNullOrEmpty(tb_Feed_Weight_5.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_5.Text.ToString().Trim()) : 0;
            long fw_6 = !string.IsNullOrEmpty(tb_Feed_Weight_6.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_6.Text.ToString().Trim()) : 0;
            long fw_7 = !string.IsNullOrEmpty(tb_Feed_Weight_7.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_7.Text.ToString().Trim()) : 0;
            long fw_8 = !string.IsNullOrEmpty(tb_Feed_Weight_8.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_8.Text.ToString().Trim()) : 0;
            long fw_9 = !string.IsNullOrEmpty(tb_Feed_Weight_9.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_9.Text.ToString().Trim()) : 0;
            long fw_10 = !string.IsNullOrEmpty(tb_Feed_Weight_10.Text.ToString().Trim()) ? Convert.ToInt64(tb_Feed_Weight_10.Text.ToString().Trim()) : 0;
            tb_Weight_Sum.Text = Convert.ToString(fw_1 + fw_2 + fw_3 + fw_4 + fw_5 + fw_6 + fw_7 + fw_8 + fw_9 + fw_10);
            tb_Weight_Sum_2.Text = Convert.ToString(fw_1 + fw_2 + fw_3 + fw_4 + fw_5 + fw_6 + fw_7 + fw_8 + fw_9 + fw_10);
        }
        #endregion

        #region 实绩重量只能输入整数
        private void tb_Feed_Weight_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_7_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_8_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tb_Feed_Weight_10_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        } 
        #endregion
        #endregion

        #region 数据处理
        /// <summary>
        /// 绑定计划数据
        /// </summary>
        /// <param name="PlanNo">计划号</param>
        /// <param name="CarNo">行车号</param>
        /// <returns></returns>
        private DataTable BindMatStockByL3Stowage(string PlanNo, string CarNo)
        {
            DataTable dtResult = InitDataTable(dataGridView1);            
            string startTime = this.dtp_StartTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string endTime = this.dtp_EndTime.Value.ToString("yyyyMMdd235959");  //结束时间
            DataTable tbL3_MAT_OUT_INFO = new DataTable("UACS_L3_MAT_OUT_INFO");
            string sqlText_All = @" SELECT 0 AS CHECK_COLUMN, WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, WEIGHT_1, MAT_CODE_2, WEIGHT_2, MAT_CODE_3, WEIGHT_3, 
            MAT_CODE_4, WEIGHT_4, MAT_CODE_5, WEIGHT_5, MAT_CODE_6, WEIGHT_6, MAT_CODE_7, WEIGHT_7, MAT_CODE_8, WEIGHT_8, MAT_CODE_9, WEIGHT_9, MAT_CODE_10, 
            WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME, 
            '0' AS Act_Weight_1, '0' AS Act_Weight_2, '0' AS Act_Weight_3, '0' AS Act_Weight_4, '0' AS Act_Weight_5, '0' AS Act_Weight_6, '0' AS Act_Weight_7, '0' AS Act_Weight_8, '0' AS Act_Weight_9, '0' AS Act_Weight_10, '' AS TO_STOCK_NO 
            FROM UACSAPP.UACS_L3_MAT_OUT_INFO 
            WHERE 1 = 1 ";
            sqlText_All += " AND REC_TIME >= '{0}' and REC_TIME <= '{1}' ";
            if (!string.IsNullOrEmpty(CarNo) && !CarNo.Equals("无"))
            {
                sqlText_All += "AND CAR_NO ='" + CarNo + "'";
            }
            if (!string.IsNullOrEmpty(PlanNo))
            {
                sqlText_All += "AND PLAN_NO ='" + PlanNo + "'";
            }
            //按 计划号>流水号>记录时间>更新时间 降序
            sqlText_All += " order by WORK_SEQ_NO DESC,PLAN_NO DESC,REC_TIME DESC,UPD_TIME DESC ";
            sqlText_All = string.Format(sqlText_All, startTime, endTime);
            // 执行
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_All))
            {
                DataColumn col;
                DataRow row;
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    col = new DataColumn();
                    col.ColumnName = rdr.GetName(i);
                    col.DataType = rdr.GetFieldType(i);
                    tbL3_MAT_OUT_INFO.Columns.Add(col);
                }

                while (rdr.Read())
                {

                    row = tbL3_MAT_OUT_INFO.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row[i] = rdr[i];
                    }
                    tbL3_MAT_OUT_INFO.Rows.Add(row);
                }
            }

            #region 指令表实际重量
            List<string> listAlarm = new List<string>();
            foreach (DataRow dataRow in tbL3_MAT_OUT_INFO.Rows)
            {
                if (!string.IsNullOrEmpty(dataRow["PLAN_NO"].ToString()))
                    listAlarm.Add(dataRow["PLAN_NO"].ToString());
            }
            if (listAlarm.Count > 0)
            {
                DataTable tbORDER_QUEUE = new DataTable("UACS_ORDER_QUEUE");
                string SQL_ORDER = @"SELECT ORDER_NO,PLAN_NO,MAT_CODE,CAL_WEIGHT,ACT_WEIGHT,TO_STOCK_NO FROM UACS_ORDER_QUEUE ";
                SQL_ORDER += " WHERE PLAN_NO IN (";
                foreach (string item in listAlarm)
                {
                    SQL_ORDER += "'" + item + "',";
                }
                SQL_ORDER = SQL_ORDER.Substring(0, SQL_ORDER.Length - 1);
                SQL_ORDER += ");";
                using (IDataReader rdr = DBHelper.ExecuteReader(SQL_ORDER))
                {
                    tbORDER_QUEUE.Load(rdr);
                }

                if (tbORDER_QUEUE.Rows.Count > 0)
                {
                    foreach (DataRow L3dataRow in tbL3_MAT_OUT_INFO.Rows)
                    {
                        foreach (DataRow OQdataRow in tbORDER_QUEUE.Rows)
                        {
                            if (L3dataRow["PLAN_NO"].ToString().Equals(OQdataRow["PLAN_NO"].ToString()))
                            {
                                if (L3dataRow["MAT_CODE_1"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_1"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_2"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_2"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_3"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_3"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_4"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_4"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_5"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_5"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_6"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_6"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_7"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_7"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_8"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_8"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_9"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_9"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                else if (L3dataRow["MAT_CODE_10"].ToString().Equals(OQdataRow["MAT_CODE"].ToString()))
                                {
                                    L3dataRow["Act_Weight_10"] = OQdataRow["ACT_WEIGHT"].ToString();
                                }
                                L3dataRow["TO_STOCK_NO"] = OQdataRow["TO_STOCK_NO"].ToString();
                            }
                        }
                    }
                }
            }
            #endregion


            foreach (DataRow dataRow in tbL3_MAT_OUT_INFO.Rows)
            {
                
                    dtResult.Rows.Add(
                    0, /*cbChoice*/
                    dataRow["PLAN_NO"].ToString(),
                    dataRow["CAR_NO"].ToString(),
                    dataRow["MAT_CODE_1"].ToString(),
                    dataRow["WEIGHT_1"].ToString(),
                    dataRow["Act_Weight_1"].ToString(),
                    dataRow["MAT_CODE_2"].ToString(),
                    dataRow["WEIGHT_2"].ToString(),
                    dataRow["Act_Weight_2"].ToString(),
                    dataRow["MAT_CODE_3"].ToString(),
                    dataRow["WEIGHT_3"].ToString(),
                    dataRow["Act_Weight_3"].ToString(),
                    dataRow["MAT_CODE_4"].ToString(),
                    dataRow["WEIGHT_4"].ToString(),
                    dataRow["Act_Weight_4"].ToString(),
                    dataRow["MAT_CODE_5"].ToString(),
                    dataRow["WEIGHT_5"].ToString(),
                    dataRow["Act_Weight_5"].ToString(),
                    dataRow["MAT_CODE_6"].ToString(),
                    dataRow["WEIGHT_6"].ToString(),
                    dataRow["Act_Weight_6"].ToString(),
                    dataRow["MAT_CODE_7"].ToString(),
                    dataRow["WEIGHT_7"].ToString(),
                    dataRow["Act_Weight_7"].ToString(),
                    dataRow["MAT_CODE_8"].ToString(),
                    dataRow["WEIGHT_8"].ToString(),
                    dataRow["Act_Weight_8"].ToString(),
                    dataRow["MAT_CODE_9"].ToString(),
                    dataRow["WEIGHT_9"].ToString(),
                    dataRow["Act_Weight_9"].ToString(),
                    dataRow["MAT_CODE_10"].ToString(),
                    dataRow["WEIGHT_10"].ToString(),
                    dataRow["Act_Weight_10"].ToString(),
                    dataRow["TO_STOCK_NO"].ToString()
                    );

            }

            return dtResult;
        }

        /// <summary>
        /// 单击选中dataGridView行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                //得到选中行的索引
                int intRow = dataGridView1.SelectedCells[0].RowIndex;
                // 选中行
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvRow.Cells["cbChoice"].Value.ToString()))
                    {
                        if (dgvRow.Index.Equals(intRow))
                        {
                            var isChoice = dgvRow.Cells["cbChoice"].Value.ToString();
                            if (isChoice.Equals("0"))
                            {
                                dgvRow.Cells["cbChoice"].Value = 1; //选中
                                UpdateDgvRow(true, dgvRow);
                            }
                            else if (isChoice.Equals("1"))
                            {
                                dgvRow.Cells["cbChoice"].Value = 0; //未选中
                                //UpdateDgvRow(false);
                            }
                            else
                            {
                                dgvRow.Cells["cbChoice"].Value = 0; //未选中
                            }
                        }
                        else
                        {
                            if (dgvRow.Cells["cbChoice"].Value.ToString().Equals("1"))
                            {
                                dgvRow.Cells["cbChoice"].Value = 0; //未选中
                            }

                        }
                    }
                }

                bool IsNoChoice = true;
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvRow.Cells["cbChoice"].Value.ToString()))
                    {
                        if (dgvRow.Cells["cbChoice"].Value.ToString().Equals("1"))
                        {
                            IsNoChoice = false;
                            break;
                        }
                    }
                }
                if (IsNoChoice)
                {
                    UpdateDgvRow(false);
                }

            }
        }

        #region DataGridViewRow 点击处理数据
        /// <summary>
        /// 更新物料名，重量
        /// </summary>
        /// <param name="isNulls"></param>
        /// <param name="dgvRow"></param>
        private void UpdateDgvRow(bool isNulls, DataGridViewRow dgvRow)
        {
            if (isNulls)
            {
                #region 查询物料名
                DataTable tbL3_MAT_INFO = new DataTable("UACS_L3_MAT_INFO");
                string sqlText = @" SELECT MAT_CODE, BASE_RESOURCE, MAT_CNAME, MAT_TYPE, REC_TIME FROM UACSAPP.UACS_L3_MAT_INFO ";
                // 执行
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    DataColumn col;
                    DataRow row;
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        col = new DataColumn();
                        col.ColumnName = rdr.GetName(i);
                        col.DataType = rdr.GetFieldType(i);
                        tbL3_MAT_INFO.Columns.Add(col);
                    }

                    while (rdr.Read())
                    {

                        row = tbL3_MAT_INFO.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            row[i] = rdr[i];
                        }
                        tbL3_MAT_INFO.Rows.Add(row);
                    }
                }

                #region 更新物料名
                foreach (DataRow L3Row in tbL3_MAT_INFO.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_1"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_1"].Value.ToString()))
                    {
                        tbMatCode_1.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_1.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_1.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_2"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_2"].Value.ToString()))
                    {
                        tbMatCode_2.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_2.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_2.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_3"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_3"].Value.ToString()))
                    {
                        tbMatCode_3.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_3.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_3.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_4"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_4"].Value.ToString()))
                    {
                        tbMatCode_4.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_4.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_4.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_5"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_5"].Value.ToString()))
                    {
                        tbMatCode_5.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_5.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_5.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_6"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_6"].Value.ToString()))
                    {
                        tbMatCode_6.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_6.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_6.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_7"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_7"].Value.ToString()))
                    {
                        tbMatCode_7.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_7.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_7.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_8"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_8"].Value.ToString()))
                    {
                        tbMatCode_8.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_8.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_8.Text = L3Row["MAT_CODE"].ToString();        //物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_9"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_9"].Value.ToString()))
                    {
                        tbMatCode_9.Text = L3Row["MAT_CNAME"].ToString();             //物料名称
                        tb_Feed_MatCodeName_9.Text = L3Row["MAT_CNAME"].ToString();   //实绩物料名称
                        tb_Feed_MatCode_9.Text = L3Row["MAT_CODE"].ToString();        //实绩物料代码
                    }
                    if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_10"].Value.ToString()) && L3Row["MAT_CODE"].ToString().Equals(dgvRow.Cells["GMatCode_10"].Value.ToString()))
                    {
                        tbMatCode_10.Text = L3Row["MAT_CNAME"].ToString();            //物料名称
                        tb_Feed_MatCodeName_10.Text = L3Row["MAT_CNAME"].ToString();  //实绩物料名称
                        tb_Feed_MatCode_10.Text = L3Row["MAT_CODE"].ToString();       //实绩物料代码
                    }
                }

                #endregion

                #endregion

                #region 更新重量
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_1"].Value.ToString()))
                {
                    tbWeight_1.Text = dgvRow.Cells["GWeight_1"].Value.ToString();
                    tb_Feed_Weight_1.Text = dgvRow.Cells["Act_Weight_1"].Value.ToString();
                }
                else
                {
                    tbWeight_1.Text = "";
                    tb_Feed_Weight_1.Text = "";
                    tbMatCode_1.Text = "";             //物料名称
                    tb_Feed_MatCodeName_1.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_1.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_2"].Value.ToString()))
                {
                    tbWeight_2.Text = dgvRow.Cells["GWeight_2"].Value.ToString();
                    tb_Feed_Weight_2.Text = dgvRow.Cells["Act_Weight_2"].Value.ToString();
                }
                else
                {
                    tbWeight_2.Text = "";
                    tb_Feed_Weight_2.Text = "";
                    tbMatCode_2.Text = "";             //物料名称
                    tb_Feed_MatCodeName_2.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_2.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_3"].Value.ToString()))
                {
                    tbWeight_3.Text = dgvRow.Cells["GWeight_3"].Value.ToString();
                    tb_Feed_Weight_3.Text = dgvRow.Cells["Act_Weight_3"].Value.ToString();
                }
                else
                {
                    tbWeight_3.Text = "";
                    tb_Feed_Weight_3.Text = "";
                    tbMatCode_3.Text = "";             //物料名称
                    tb_Feed_MatCodeName_3.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_3.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_4"].Value.ToString()))
                {
                    tbWeight_4.Text = dgvRow.Cells["GWeight_4"].Value.ToString();
                    tb_Feed_Weight_4.Text = dgvRow.Cells["Act_Weight_4"].Value.ToString();
                }
                else
                {
                    tbWeight_4.Text = "";
                    tb_Feed_Weight_4.Text = "";
                    tbMatCode_4.Text = "";             //物料名称
                    tb_Feed_MatCodeName_4.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_4.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_5"].Value.ToString()))
                {
                    tbWeight_5.Text = dgvRow.Cells["GWeight_5"].Value.ToString();
                    tb_Feed_Weight_5.Text = dgvRow.Cells["Act_Weight_5"].Value.ToString();
                }
                else
                {
                    tbWeight_5.Text = "";
                    tb_Feed_Weight_5.Text = "";
                    tbMatCode_5.Text = "";             //物料名称
                    tb_Feed_MatCodeName_5.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_5.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_6"].Value.ToString()))
                {
                    tbWeight_6.Text = dgvRow.Cells["GWeight_6"].Value.ToString();
                    tb_Feed_Weight_6.Text = dgvRow.Cells["Act_Weight_6"].Value.ToString();
                }
                else
                {
                    tbWeight_6.Text = "";
                    tb_Feed_Weight_6.Text = "";
                    tbMatCode_6.Text = "";             //物料名称
                    tb_Feed_MatCodeName_6.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_6.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_7"].Value.ToString()))
                {
                    tbWeight_7.Text = dgvRow.Cells["GWeight_7"].Value.ToString();
                    tb_Feed_Weight_7.Text = dgvRow.Cells["Act_Weight_7"].Value.ToString();
                }
                else
                {
                    tbWeight_7.Text = "";
                    tb_Feed_Weight_7.Text = "";
                    tbMatCode_7.Text = "";             //物料名称
                    tb_Feed_MatCodeName_7.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_7.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_8"].Value.ToString()))
                {
                    tbWeight_8.Text = dgvRow.Cells["GWeight_8"].Value.ToString();
                    tb_Feed_Weight_8.Text = dgvRow.Cells["Act_Weight_8"].Value.ToString();
                }
                else
                {
                    tbWeight_8.Text = "";
                    tb_Feed_Weight_8.Text = "";
                    tbMatCode_8.Text = "";             //物料名称
                    tb_Feed_MatCodeName_8.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_8.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_9"].Value.ToString()))
                {
                    tbWeight_9.Text = dgvRow.Cells["GWeight_9"].Value.ToString();
                    tb_Feed_Weight_9.Text = dgvRow.Cells["Act_Weight_9"].Value.ToString();
                }
                else
                {
                    tbWeight_9.Text = "";
                    tb_Feed_Weight_9.Text = "";
                    tbMatCode_9.Text = "";             //物料名称
                    tb_Feed_MatCodeName_9.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_9.Text = "";        //实绩物料代码
                }
                if (!string.IsNullOrEmpty(dgvRow.Cells["GMatCode_10"].Value.ToString()))
                {
                    tbWeight_10.Text = dgvRow.Cells["GWeight_10"].Value.ToString();
                    tb_Feed_Weight_10.Text = dgvRow.Cells["Act_Weight_10"].Value.ToString();
                }
                else
                {
                    tbWeight_10.Text = "";
                    tb_Feed_Weight_10.Text = "";
                    tbMatCode_10.Text = "";             //物料名称
                    tb_Feed_MatCodeName_10.Text = "";   //实绩物料名称
                    tb_Feed_MatCode_10.Text = "";        //实绩物料代码
                }
                #endregion

                #region 实际总重量
                tb_Start_Weight_Sum.Text = "";
                var gw_1 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_1"].Value.ToString()) ? dgvRow.Cells["GWeight_1"].Value.ToString() : "0");
                var gw_2 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_2"].Value.ToString()) ? dgvRow.Cells["GWeight_2"].Value.ToString() : "0");
                var gw_3 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_3"].Value.ToString()) ? dgvRow.Cells["GWeight_3"].Value.ToString() : "0");
                var gw_4 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_4"].Value.ToString()) ? dgvRow.Cells["GWeight_4"].Value.ToString() : "0");
                var gw_5 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_5"].Value.ToString()) ? dgvRow.Cells["GWeight_5"].Value.ToString() : "0");
                var gw_6 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_6"].Value.ToString()) ? dgvRow.Cells["GWeight_6"].Value.ToString() : "0");
                var gw_7 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_7"].Value.ToString()) ? dgvRow.Cells["GWeight_7"].Value.ToString() : "0");
                var gw_8 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_8"].Value.ToString()) ? dgvRow.Cells["GWeight_8"].Value.ToString() : "0");
                var gw_9 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_9"].Value.ToString()) ? dgvRow.Cells["GWeight_9"].Value.ToString() : "0");
                var gw_10 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["GWeight_10"].Value.ToString()) ? dgvRow.Cells["GWeight_10"].Value.ToString() : "0");
                var gsum = Convert.ToString(Convert.ToInt32(!string.IsNullOrEmpty(tb_Start_Weight_Sum.Text) ? tb_Start_Weight_Sum.Text : "0") + gw_1 + gw_2 + gw_3 + gw_4 + gw_5 + gw_6 + gw_7 + gw_8 + gw_9 + gw_10);
                tb_Start_Weight_Sum.Text = gsum;
                #endregion

                #region 实绩总重量
                tb_Weight_Sum.Text = "";
                tb_Weight_Sum_2.Text = "";
                var aw_1 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_1"].Value.ToString()) ? dgvRow.Cells["Act_Weight_1"].Value.ToString() : "0");
                var aw_2 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_2"].Value.ToString()) ? dgvRow.Cells["Act_Weight_2"].Value.ToString() : "0");
                var aw_3 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_3"].Value.ToString()) ? dgvRow.Cells["Act_Weight_3"].Value.ToString() : "0");
                var aw_4 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_4"].Value.ToString()) ? dgvRow.Cells["Act_Weight_4"].Value.ToString() : "0");
                var aw_5 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_5"].Value.ToString()) ? dgvRow.Cells["Act_Weight_5"].Value.ToString() : "0");
                var aw_6 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_6"].Value.ToString()) ? dgvRow.Cells["Act_Weight_6"].Value.ToString() : "0");
                var aw_7 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_7"].Value.ToString()) ? dgvRow.Cells["Act_Weight_7"].Value.ToString() : "0");
                var aw_8 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_8"].Value.ToString()) ? dgvRow.Cells["Act_Weight_8"].Value.ToString() : "0");
                var aw_9 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_9"].Value.ToString()) ? dgvRow.Cells["Act_Weight_9"].Value.ToString() : "0");
                var aw_10 = Convert.ToInt32(!string.IsNullOrEmpty(dgvRow.Cells["Act_Weight_10"].Value.ToString()) ? dgvRow.Cells["Act_Weight_10"].Value.ToString() : "0");
                var asum = Convert.ToString(Convert.ToInt32(!string.IsNullOrEmpty(tb_Weight_Sum.Text) ? tb_Weight_Sum.Text : "0") + aw_1 + aw_2 + aw_3 + aw_4 + aw_5 + aw_6 + aw_7 + aw_8 + aw_9 + aw_10);
                tb_Weight_Sum.Text = asum;
                tb_Weight_Sum_2.Text = asum;
                #endregion

                tb_PlanNo.Text = dgvRow.Cells["GPlanNO"].Value.ToString();  //计划号
                tb_CarNo.Text = dgvRow.Cells["GCarNo"].Value.ToString();    //料槽车号
            }
            else
            {
                SetNull();
            }
        }

        /// <summary>
        /// 更新物料控件和总量控件为空
        /// </summary>
        /// <param name="isNulls">false设置空</param>
        private void UpdateDgvRow(bool isNulls = false)
        {
            UpdateDgvRow(isNulls, null);
        }

        /// <summary>
        /// 控件清空
        /// </summary>
        private void SetNull()
        {
            tb_PlanNo.Text = "";  //计划号
            tb_CarNo.Text = "";   //料槽车号

            //#region 控件值赋空
            //物料代码
            tbMatCode_1.Text = "";
            tbMatCode_2.Text = "";
            tbMatCode_3.Text = "";
            tbMatCode_4.Text = "";
            tbMatCode_5.Text = "";
            tbMatCode_6.Text = "";
            tbMatCode_7.Text = "";
            tbMatCode_8.Text = "";
            tbMatCode_9.Text = "";
            tbMatCode_10.Text = "";
            //要求重量
            tbWeight_1.Text = "";
            tbWeight_2.Text = "";
            tbWeight_3.Text = "";
            tbWeight_4.Text = "";
            tbWeight_5.Text = "";
            tbWeight_6.Text = "";
            tbWeight_7.Text = "";
            tbWeight_8.Text = "";
            tbWeight_9.Text = "";
            tbWeight_10.Text = "";
            tb_Start_Weight_Sum.Text = "";
            //实绩重量
            tb_Feed_Weight_1.Text = "";
            tb_Feed_Weight_2.Text = "";
            tb_Feed_Weight_3.Text = "";
            tb_Feed_Weight_4.Text = "";
            tb_Feed_Weight_5.Text = "";
            tb_Feed_Weight_6.Text = "";
            tb_Feed_Weight_7.Text = "";
            tb_Feed_Weight_8.Text = "";
            tb_Feed_Weight_9.Text = "";
            tb_Feed_Weight_10.Text = "";
            //实绩物料代码
            tb_Feed_MatCode_1.Text = "";
            tb_Feed_MatCode_2.Text = "";
            tb_Feed_MatCode_3.Text = "";
            tb_Feed_MatCode_4.Text = "";
            tb_Feed_MatCode_5.Text = "";
            tb_Feed_MatCode_6.Text = "";
            tb_Feed_MatCode_7.Text = "";
            tb_Feed_MatCode_8.Text = "";
            tb_Feed_MatCode_9.Text = "";
            tb_Feed_MatCode_10.Text = "";
            //物料中文名
            tb_Feed_MatCodeName_1.Text = "";
            tb_Feed_MatCodeName_2.Text = "";
            tb_Feed_MatCodeName_3.Text = "";
            tb_Feed_MatCodeName_4.Text = "";
            tb_Feed_MatCodeName_5.Text = "";
            tb_Feed_MatCodeName_6.Text = "";
            tb_Feed_MatCodeName_7.Text = "";
            tb_Feed_MatCodeName_8.Text = "";
            tb_Feed_MatCodeName_9.Text = "";
            tb_Feed_MatCodeName_10.Text = "";
        }

        /// <summary>
        /// 绑定槽号
        /// </summary>
        /// <param name="ParkNO">停车位号</param>
        private void BindCmbCar()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                dr = dt.NewRow();
                dr["ID"] = "无";
                dr["NAME"] = "无";
                dt.Rows.Add(dr);
                //查车辆号
                string sqlText = @"SELECT FRAME_TYPE_NO FROM UACS_TRUCK_FRAME_DEFINE ";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        dr = dt.NewRow();
                        dr["ID"] = rdr["FRAME_TYPE_NO"].ToString();
                        dr["NAME"] = rdr["FRAME_TYPE_NO"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                //dr = dt.NewRow();
                //dr["ID"] = "SG99";
                //dr["NAME"] = "SG99";
                //dt.Rows.Add(dr);
                //绑定数据
                cmb_Query_CarNo.ValueMember = "ID";
                cmb_Query_CarNo.DisplayMember = "NAME";
                cmb_Query_CarNo.DataSource = dt;
                //根据text值选中项
                this.cmb_Query_CarNo.SelectedIndex = 0;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        #endregion

        #region 弃用（旧）
        ///// <summary>
        ///// 更新物料与总量
        ///// </summary>
        ///// <param name="isNulls">是否更新，true更新 or false设置空</param>
        ///// <param name="GMatCode_1">物料1</param>
        ///// <param name="GMatCode_2">物料2</param>
        ///// <param name="GMatCode_3">物料3</param>
        ///// <param name="GMatCode_4">物料4</param>
        ///// <param name="GMatCode_5">物料5</param>
        ///// <param name="GMatCode_6">物料6</param>
        ///// <param name="GMatCode_7">物料7</param>
        ///// <param name="GMatCode_8">物料8</param>
        ///// <param name="GMatCode_9">物料9</param>
        ///// <param name="GMatCode_10">物料10</param>
        ///// <param name="GWeight_1">重量1</param>
        ///// <param name="GWeight_2">重量2</param>
        ///// <param name="GWeight_3">重量3</param>
        ///// <param name="GWeight_4">重量4</param>
        ///// <param name="GWeight_5">重量5</param>
        ///// <param name="GWeight_6">重量6</param>
        ///// <param name="GWeight_7">重量7</param>
        ///// <param name="GWeight_8">重量8</param>
        ///// <param name="GWeight_9">重量9</param>
        ///// <param name="GWeight_10">重量10</param>
        //private void UpdateDgvRow(bool isNulls, string GMatCode_1, string GMatCode_2, string GMatCode_3, string GMatCode_4, string GMatCode_5, string GMatCode_6, string GMatCode_7, string GMatCode_8, string GMatCode_9, string GMatCode_10, string GWeight_1, string GWeight_2, string GWeight_3, string GWeight_4, string GWeight_5, string GWeight_6, string GWeight_7, string GWeight_8, string GWeight_9, string GWeight_10)
        //{
        //    if (isNulls)
        //    {
        //        #region 查询物料名
        //        DataTable tbL3_MAT_INFO = new DataTable("UACS_L3_MAT_INFO");
        //        string sqlText = @" SELECT MAT_CODE, BASE_RESOURCE, MAT_CNAME, MAT_TYPE, REC_TIME FROM UACSAPP.UACS_L3_MAT_INFO ";
        //        // 执行
        //        using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
        //        {
        //            DataColumn col;
        //            DataRow row;
        //            for (int i = 0; i < rdr.FieldCount; i++)
        //            {
        //                col = new DataColumn();
        //                col.ColumnName = rdr.GetName(i);
        //                col.DataType = rdr.GetFieldType(i);
        //                tbL3_MAT_INFO.Columns.Add(col);
        //            }

        //            while (rdr.Read())
        //            {

        //                row = tbL3_MAT_INFO.NewRow();
        //                for (int i = 0; i < rdr.FieldCount; i++)
        //                {
        //                    row[i] = rdr[i];
        //                }
        //                tbL3_MAT_INFO.Rows.Add(row);
        //            }
        //        }

        //        #region 更新物料名
        //        foreach (DataRow L3Row in tbL3_MAT_INFO.Rows)
        //        {
        //            if (!string.IsNullOrEmpty(GMatCode_1) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_1))
        //            {
        //                tbMatCode_1.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_2) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_2))
        //            {
        //                tbMatCode_2.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_3) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_3))
        //            {
        //                tbMatCode_3.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_4) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_4))
        //            {
        //                tbMatCode_4.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_5) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_5))
        //            {
        //                tbMatCode_5.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_6) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_6))
        //            {
        //                tbMatCode_6.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_7) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_7))
        //            {
        //                tbMatCode_7.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_8) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_8))
        //            {
        //                tbMatCode_8.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_9) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_9))
        //            {
        //                tbMatCode_9.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(GMatCode_10) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_10))
        //            {
        //                tbMatCode_10.Text = L3Row["MAT_CNAME"].ToString();
        //            }
        //        }

        //        #endregion

        //        #endregion

        //        #region 更新重量


        //        if (!string.IsNullOrEmpty(GWeight_1))
        //        {
        //            tbWeight_1.Text = GWeight_1;
        //        }
        //        else
        //        {
        //            tbWeight_1.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_2))
        //        {
        //            tbWeight_2.Text = GWeight_2;
        //        }
        //        else
        //        {
        //            tbWeight_2.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_3))
        //        {
        //            tbWeight_3.Text = GWeight_3;
        //        }
        //        else
        //        {
        //            tbWeight_3.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_4))
        //        {
        //            tbWeight_4.Text = GWeight_4;
        //        }
        //        else
        //        {
        //            tbWeight_4.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_5))
        //        {
        //            tbWeight_5.Text = GWeight_5;
        //        }
        //        else
        //        {
        //            tbWeight_5.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_6))
        //        {
        //            tbWeight_6.Text = GWeight_6;
        //        }
        //        else
        //        {
        //            tbWeight_6.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_7))
        //        {
        //            tbWeight_7.Text = GWeight_7;
        //        }
        //        else
        //        {
        //            tbWeight_7.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_8))
        //        {
        //            tbWeight_8.Text = GWeight_8;
        //        }
        //        else
        //        {
        //            tbWeight_8.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_9))
        //        {
        //            tbWeight_9.Text = GWeight_9;
        //        }
        //        else
        //        {
        //            tbWeight_9.Text = "";
        //        }
        //        if (!string.IsNullOrEmpty(GWeight_10))
        //        {
        //            tbWeight_10.Text = GWeight_10;
        //        }
        //        else
        //        {
        //            tbWeight_10.Text = "";
        //        }

        //        #endregion

        //        #region 计算总重量

        //        tb_Start_Weight_Sum.Text = "";

        //        var w_1 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_1) ? GWeight_1 : "0");
        //        var w_2 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_2) ? GWeight_2 : "0");
        //        var w_3 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_3) ? GWeight_3 : "0");
        //        var w_4 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_4) ? GWeight_4 : "0");
        //        var w_5 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_5) ? GWeight_5 : "0");
        //        var w_6 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_6) ? GWeight_6 : "0");
        //        var w_7 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_7) ? GWeight_7 : "0");
        //        var w_8 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_8) ? GWeight_8 : "0");
        //        var w_9 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_9) ? GWeight_9 : "0");
        //        var w_10 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_10) ? GWeight_10 : "0");

        //        var sum = Convert.ToString(Convert.ToInt32(!string.IsNullOrEmpty(tb_Start_Weight_Sum.Text) ? tb_Start_Weight_Sum.Text : "0") + w_1 + w_2 + w_3 + w_4 + w_5 + w_6 + w_7 + w_8 + w_9 + w_10);
        //        tb_Start_Weight_Sum.Text = sum;

        //        #endregion
        //    }
        //    else
        //    {
        //        tbMatCode_1.Text = "";
        //        tbMatCode_2.Text = "";
        //        tbMatCode_3.Text = "";
        //        tbMatCode_4.Text = "";
        //        tbMatCode_5.Text = "";
        //        tbMatCode_6.Text = "";
        //        tbMatCode_7.Text = "";
        //        tbMatCode_8.Text = "";
        //        tbMatCode_9.Text = "";
        //        tbMatCode_10.Text = "";
        //        tbWeight_1.Text = "";
        //        tbWeight_2.Text = "";
        //        tbWeight_3.Text = "";
        //        tbWeight_4.Text = "";
        //        tbWeight_5.Text = "";
        //        tbWeight_6.Text = "";
        //        tbWeight_7.Text = "";
        //        tbWeight_8.Text = "";
        //        tbWeight_9.Text = "";
        //        tbWeight_10.Text = "";
        //        tb_Start_Weight_Sum.Text = "";
        //    }
        //}

        ///// <summary>
        ///// 更新物料控件和总量控件为空
        ///// </summary>
        ///// <param name="isNulls">false设置空</param>
        //private void UpdateDgvRow(bool isNulls = false)
        //{
        //    UpdateDgvRow(isNulls, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
        //} 
        #endregion

        #endregion


    }
}
