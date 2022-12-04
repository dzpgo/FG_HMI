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
using ParkClassLibrary;

namespace UACSView
{
    public partial class CraneOrderConfig : FormBase
    {
        public CraneOrderConfig()
        {
            InitializeComponent();
            Bind_cbbBayNo(cbbBayNo);
            Bind_cbbCraneNo(cbbCraneNo);
        }

        public CraneOrderConfig(bool _isOpenPasswordForm,string _bayNo,string _craneNo)
        {
            InitializeComponent();

            isOpenPasswordForm = _isOpenPasswordForm;
            Bind_cbbBayNo(cbbBayNo);
            Bind_cbbCraneNo(cbbCraneNo);
            this.cbbBayNo.Text = _bayNo;
            this.cbbCraneNo.Text = _craneNo;
        }


        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;


        #region  -------------------声明字段------------------------------
        private DataTable dgvGroupDt = new DataTable();
        private DataTable dgvTaskPREDt = new DataTable();
        private DataTable dgvTaskPostDt = new DataTable();
        private UACSDAL.CraneOrderConfig craneOrderConfig = new UACSDAL.CraneOrderConfig();
        private CraneStatusInBay craneStatusInBay = new CraneStatusInBay();
        private bool isOpenPasswordForm = false;
        private List<string> listCraneNo = new List<string>();
        #endregion


        #region  -------------------加载事件------------------------------
        private void CraneOrderConfig_Load(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;

            
            //
            craneStatusInBay.InitTagDataProvide(SaddleBase.tagServiceName);

            //Bind_cbbBayNo(cbbBayNo);
            //Bind_cbbCraneNo(cbbCraneNo);
            GetCraneNo(listCraneNo);

            //craneStatusInBay.AddCraneNO("6_1");
            //craneStatusInBay.AddCraneNO("6_2");
            //craneStatusInBay.AddCraneNO("6_3");
            //craneStatusInBay.AddCraneNO("6_4");
            //craneStatusInBay.AddCraneNO("6_5");
            //craneStatusInBay.AddCraneNO("6_6");
            //craneStatusInBay.AddCraneNO("6_7");
            //craneStatusInBay.AddCraneNO("6_8");
            //craneStatusInBay.AddCraneNO("6_9");
            //craneStatusInBay.AddCraneNO("6_10");
            //craneStatusInBay.SetReady();


            //if (!isOpenPasswordForm)
            //{
            //    ShiftCraneByBay();
            //    //必须输对密码才能进行查看信息
            //    FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
            //    DialogResult ret = password.ShowDialog();

            //    if (ret == DialogResult.OK)
            //    {
            //        string craneNo = cbbCraneNo.Text.Trim();
            //        RefreshCraneOrderGroupMessage(craneNo);
            //        RefreshDgvPER(craneNo);
            //        RefreshDgvPOST(craneNo);
            //    }
            //    else
            //    {
            //        timer1.Enabled = true;
            //    }
            //}
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            auth.CloseForm("行车指令配置");
            timer1.Enabled = false;
        }
        #endregion



        #region -------------------combobox值改变事件--------------------
        private void cbbBayNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_cbbCraneNo(cbbCraneNo);
        }
        private void cbbCraneNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string craneNo = cbbCraneNo.Text.Trim();
            RefreshCraneOrderGroupMessage(craneNo);
            RefreshDgvPER(craneNo);
            RefreshDgvPOST(craneNo);
        }
        #endregion


        #region -------------------方法----------------------------------
        /// <summary>
        /// 刷新行车指令分组表
        /// </summary>
        /// <param name="craneNo"></param>
        private void RefreshCraneOrderGroupMessage(string craneNo)
        {
            string error = null;
            dgvGroupDt.Clear();
            if (radioButton1.Checked)
                dgvGroupDt = craneOrderConfig.GetCraneOrderExcuteGroupOfData(craneNo, UACSDAL.CraneOrderConfig.SORT_TYPE.INDEX_SORT, out error);
            if (radioButton2.Checked)
                dgvGroupDt = craneOrderConfig.GetCraneOrderExcuteGroupOfData(craneNo, UACSDAL.CraneOrderConfig.SORT_TYPE.ENABLED_SORT, out error);

            if (error != null)
            {
                LogManager.WriteProgramLog(error);
                MessageBox.Show("查询CRANE_ORDER_EXCUTE_GROUP表出错");
            }
            else
            {
                dgvGroup.DataSource = dgvGroupDt;
                AlterDGVButtonColor(dgvGroup, "FLAG_ENABLED");
                AlterDGVRowColorByFlagCuttent();
               
            }
            Transform_Chinese(dgvGroup);
        }
        /// <summary>
        /// 刷新行车前置后置表
        /// </summary>
        /// <param name="craneNo"></param>
        /// <param name="type"></param>
        private void RefreshCraneOrderSequenceMessage(string craneNo, UACSDAL.CraneOrderConfig.SEQ_TYPE type, DataTable dt, DataGridView dgv)
        {
            string error = null;
            dt.Clear();
            dt = craneOrderConfig.GetCraneOrderExcuteSequenceOfData(craneNo, type, out error);

            if (error != null)
            {
                LogManager.WriteProgramLog(error);
                MessageBox.Show("查询CRANE_ORDER_EXCUTE_SEQUENCE表出错");
            }
            else
            {
                dgv.DataSource = dt;
            }
        }


        /// <summary>
        /// 刷新前置表格
        /// </summary>
        /// <param name="craneNo"></param>
        private void RefreshDgvPER(string craneNo)
        {
            RefreshCraneOrderSequenceMessage(craneNo, UACSDAL.CraneOrderConfig.SEQ_TYPE.PRE, dgvTaskPREDt, dgvTaskPRE);
            AlterDGVButtonColor(dgvTaskPRE, "PRE_FLAG_ENABLED");
            Transform_Chinese(dgvTaskPRE);
            dgvRefresh(dgvTaskPRE);
        }

        /// <summary>
        /// 刷新后置表格
        /// </summary>
        /// <param name="craneNo"></param>
        private void RefreshDgvPOST(string craneNo)
        {
            RefreshCraneOrderSequenceMessage(craneNo, UACSDAL.CraneOrderConfig.SEQ_TYPE.POST, dgvTaskPostDt, dgvTaskPOST);
            AlterDGVButtonColor(dgvTaskPOST, "POST_FLAG_ENABLED");
            Transform_Chinese(dgvTaskPOST);
        }


        private void AlterDGVButtonColor(DataGridView dgv, string dgvCellsName)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv.Rows[i].Cells[dgvCellsName].Value != DBNull.Value)
                {
                    if (dgv.Rows[i].Cells[dgvCellsName].Value.ToString() == "开")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    //if (dgv.Rows[i].Cells[dgvCellsName].Value.ToString() == "关")
                    //{
                    //    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    //}
                }
            }
        }


        private void AlterDGVRowColorByFlagCuttent()
        {
            try
            {
                for (int i = 0; i < dgvGroup.RowCount; i++)
                {
                    if (dgvGroup.Rows[i].Cells["FLAG_CURRENT_INDEX"].Value != DBNull.Value)
                    {
                        int flag = Convert.ToInt32(dgvGroup.Rows[i].Cells["FLAG_CURRENT_INDEX"].Value.ToString());
                        if (flag == 1)
                        {
                            dgvGroup.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        //获取全部行车号
        private void GetCraneNo(List<string> list)
        {
            list.Clear();
            string craneNo;
            try
            {
                string strSql = @"SELECT ID FROM UACS_YARDMAP_CRANE ORDER BY ID ASC ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["ID"].ToString().Trim() != "")
                        {
                            craneNo = rdr["ID"].ToString().Trim();
                            list.Add(craneNo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        //根据跨号绑定行车号
        private void Bind_cbbCraneNo(ComboBox comBox)
        {
            string bayNo = cbbBayNo.SelectedValue.ToString().Trim();
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            try
            {
                string strSql = @"SELECT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_CRANE WHERE BAY_NO = '" + bayNo + "'ORDER BY ID ASC ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSql))
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
        ///// <summary>
        ///// 根据跨别对应行车号
        ///// </summary>
        //private void ShiftCraneByBay()
        //{
        //    if (cbbBayNo.Text == "Z21跨")
        //    {
        //        cbbCraneNo.Text = "6_1";
        //        cbbCraneNo.Items.Clear();
        //        cbbCraneNo.Items.Add("6_1");
        //        cbbCraneNo.Items.Add("6_2");
        //        cbbCraneNo.Items.Add("6_3");
        //    }
        //    else if (cbbBayNo.Text == "Z22跨")
        //    {
        //        cbbCraneNo.Text = "6_4";
        //        cbbCraneNo.Items.Clear();
        //        cbbCraneNo.Items.Add("6_4");
        //        cbbCraneNo.Items.Add("6_5");
        //        cbbCraneNo.Items.Add("6_6");
        //    }
        //    else if (cbbBayNo.Text == "Z23跨")
        //    {
        //        cbbCraneNo.Text = "6_7";
        //        cbbCraneNo.Items.Clear();
        //        cbbCraneNo.Items.Add("6_7");
        //        cbbCraneNo.Items.Add("6_8");
        //        cbbCraneNo.Items.Add("6_9");
        //        cbbCraneNo.Items.Add("6_10");
        //    }
        //    else
        //    {
        //        MessageBox.Show("不符合要求");
        //    }
        //}

        /// <summary>
        /// 启用标记
        /// 1 启用   0 不启用
        /// </summary>
        /// <param name="flag"></param>
        private void StartUingEnadled(int flagEnadled)
        {
            string error = null;
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    string craneNo = null;
                    string taskName = null;
                    int id = 0;
                    if (dgvGroup.Rows[i].Cells["CRANE_NO"].Value != DBNull.Value)
                        craneNo = dgvGroup.Rows[i].Cells["CRANE_NO"].Value.ToString().Trim();
                    if (dgvGroup.Rows[i].Cells["TASK_NAME"].Value != DBNull.Value)
                        taskName = dgvGroup.Rows[i].Cells["TASK_NAME"].Value.ToString().Trim();
                    if (dgvGroup.Rows[i].Cells["ID"].Value != DBNull.Value)
                        id = Convert.ToInt32(dgvGroup.Rows[i].Cells["ID"].Value.ToString().Trim());

                    if (craneNo != null && taskName != null && id != 0)
                    {
                        craneOrderConfig.UpCraneOrderEnadled(craneNo, id, flagEnadled, out error);
                        if (flagEnadled == 1)
                        {
                            HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",开启(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                        }
                        else if (flagEnadled == 0)
                        {
                            HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",关闭(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                        }
                        if (error != null)
                        {
                            LogManager.WriteProgramLog("CraneOrderConfig_StartUingEnadled");
                            LogManager.WriteProgramLog("行车号：" + craneNo);
                            LogManager.WriteProgramLog("任务名称：" + taskName);
                            LogManager.WriteProgramLog(error);
                        }
                    }
                }
            }
            RefreshCraneOrderGroupMessage(cbbCraneNo.Text.Trim());
        }

        /// <summary>
        /// 给已经选中的行标记
        /// </summary>
        /// <param name="_id"></param>
        private void SelectedCheckCell(int _id)
        {
            int id = 0;
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                if (dgvGroup.Rows[i].Cells["ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvGroup.Rows[i].Cells["ID"].Value.ToString().Trim());
                if (id == _id)
                {
                    dgvGroup.Rows[i].Cells["Select"].Value = true;
                }
            }
        }


        /// <summary>
        /// 改变颜色
        /// </summary>
        private void CheckCellShift()
        {
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    dgvGroup.Rows[i].DefaultCellStyle.BackColor = Color.LavenderBlush;
                }
            }
        }

        #endregion


        #region -------------------表格值改变事件------------------------
        // 选中改变颜色
        private void dgvGroup_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //for (int i = 0; i < dgvGroup.RowCount; i++)
            //{
            //    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
            //    Boolean flag = Convert.ToBoolean(checkCell.Value);
            //    if (flag)
            //    {
            //        dgvGroup.Rows[i].DefaultCellStyle.BackColor = Color.LavenderBlush;
            //    }
            //}
        }

        private void dgvGroup_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < dgvGroup.RowCount; i++)
            //{
            //    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
            //    Boolean flag = Convert.ToBoolean(checkCell.Value);
            //    if (flag)
            //    {
            //        dgvGroup.Rows[i].DefaultCellStyle.BackColor = Color.LavenderBlush;
            //    }
            //}
        }
        #endregion


        #region -------------------按钮事件------------------------------
        // 全选按钮
        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                dgvGroup.Rows[i].Cells["Select"].Value = true;
            }
            //CheckCellShift();
        }

        // 反选按钮
        private void btnInvertSelection_Click(object sender, EventArgs e)
        {
            List<int> listIndex = new List<int>();
            //
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    listIndex.Add(i);
                }
            }
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                dgvGroup.Rows[i].Cells["Select"].Value = true;
            }

            foreach (int index in listIndex)
            {
                dgvGroup.Rows[index].Cells["Select"].Value = false;
            }

            //CheckCellShift();


        }

        // 启用按钮
        private void btnStartUsing_Click(object sender, EventArgs e)
        {
            StartUingEnadled(1);
        }

        //不启用
        private void btnDisable_Click(object sender, EventArgs e)
        {
            StartUingEnadled(0);
        }

        //刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string craneNo = cbbCraneNo.Text.Trim();
            RefreshCraneOrderGroupMessage(craneNo);
            RefreshDgvPER(craneNo);
            RefreshDgvPOST(craneNo);
        }

        //提升优先级
        private void btnLiftRank_Click(object sender, EventArgs e)
        {
            //提升优先级一次自能选一个
            int index = 0;
            int num = 999;
            string error = null;
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    index++;
                    num = i;
                }
            }

            if (index == 1)
            {
                if (num == 999)
                    return;
                string craneNo = null;
                string index_in_group = null;
                int id = 0;
                int groupId = 0;
                if (dgvGroup.Rows[num].Cells["CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvGroup.Rows[num].Cells["CRANE_NO"].Value.ToString().Trim();
                if (dgvGroup.Rows[num].Cells["INDEX_IN_GROUP"].Value != DBNull.Value)
                    index_in_group = dgvGroup.Rows[num].Cells["INDEX_IN_GROUP"].Value.ToString().Trim();
                if (dgvGroup.Rows[num].Cells["ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvGroup.Rows[num].Cells["ID"].Value.ToString().Trim());
                if (dgvGroup.Rows[num].Cells["GROUP_ID"].Value != DBNull.Value)
                    groupId = Convert.ToInt32(dgvGroup.Rows[num].Cells["GROUP_ID"].Value.ToString().Trim());

                if (craneNo != null && index_in_group != null && id != 0 && groupId != 0)
                {
                    craneOrderConfig.UpPromotionTask(craneNo, index_in_group, id, groupId, out error);
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("CraneOrderConfig_btnLiftRank");
                        LogManager.WriteProgramLog("行车号：" + craneNo);
                        LogManager.WriteProgramLog("ID：" + id);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshCraneOrderGroupMessage(cbbCraneNo.Text.Trim());
                        SelectedCheckCell(id);
                    }
                }


            }
            else if (index == 0)
            {
                MessageBox.Show("请选择一项任务进行提升优先级");
            }
            else
            {
                MessageBox.Show("只能选择一项任务进行提升");
            }
        }

        //设置指针
        private void btnUpPointer_Click(object sender, EventArgs e)
        {
            //提升优先级一次自能选一个
            int index = 0;
            int num = 999;
            string error = null;
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    index++;
                    num = i;
                }
            }
            if (index == 1)
            {
                if (num == 999)
                    return;
                string craneNo = null;
                int id = 0;
                int groupId = 0;
                if (dgvGroup.Rows[num].Cells["CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvGroup.Rows[num].Cells["CRANE_NO"].Value.ToString().Trim();
                if (dgvGroup.Rows[num].Cells["ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvGroup.Rows[num].Cells["ID"].Value.ToString().Trim());
                if (dgvGroup.Rows[num].Cells["GROUP_ID"].Value != DBNull.Value)
                    groupId = Convert.ToInt32(dgvGroup.Rows[num].Cells["GROUP_ID"].Value.ToString().Trim());

                if (craneNo != null && id != 0 && groupId != 0)
                {



                    craneStatusInBay.getAllPLCStatusInCrane(craneNo);

                    CraneStatusBase crane = craneStatusInBay.DicCranePLCStatusBase[craneNo];

                    if (crane.CraneStatusDesc() != CraneStatusBase.STATUS_NEED_TO_READY_Desc &&
                        crane.CraneStatusDesc() != CraneStatusBase.STATUS_WAITING_ORDER_WITH_OUT_COIL_Desc &&
                        crane.CraneStatusDesc() != CraneStatusBase.STATUS_ARRIVED_POS_WITH_OUT_COIL_Desc)
                    {
                        craneOrderConfig.SetCurrentPointer(craneNo, id, groupId, out error);
                        if (error != null)
                        {
                            LogManager.WriteProgramLog("CraneOrderConfig_btnUpPointer");
                            LogManager.WriteProgramLog("行车号：" + craneNo);
                            LogManager.WriteProgramLog("ID：" + id);
                            LogManager.WriteProgramLog(error);
                            MessageBox.Show(error);
                        }
                        else
                        {
                            RefreshCraneOrderGroupMessage(cbbCraneNo.Text.Trim());
                        }
                    }
                    else
                    {
                        MessageBox.Show("请在自动行车自动行走中设置指针");
                    }





                }
            }
            else if (index == 0)
            {
                MessageBox.Show("请选择一项任务设置为当前指针");
            }
            else
            {
                MessageBox.Show("只能选择一项任务设置指针");
            }
        }

        //任务置顶
        private void btnTaskTop_Click(object sender, EventArgs e)
        {
            //提升优先级一次自能选一个
            int index = 0;
            int num = 999;
            string error = null;
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    index++;
                    num = i;
                }

            }

            if (index == 1)
            {
                if (num == 999)
                    return;
                string craneNo = null;
                string index_in_group = null;
                int id = 0;
                int groupId = 0;
                if (dgvGroup.Rows[num].Cells["CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvGroup.Rows[num].Cells["CRANE_NO"].Value.ToString().Trim();
                if (dgvGroup.Rows[num].Cells["INDEX_IN_GROUP"].Value != DBNull.Value)
                    index_in_group = dgvGroup.Rows[num].Cells["INDEX_IN_GROUP"].Value.ToString().Trim();
                if (dgvGroup.Rows[num].Cells["ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvGroup.Rows[num].Cells["ID"].Value.ToString().Trim());
                if (dgvGroup.Rows[num].Cells["GROUP_ID"].Value != DBNull.Value)
                    groupId = Convert.ToInt32(dgvGroup.Rows[num].Cells["GROUP_ID"].Value.ToString().Trim());

                if (craneNo != null && index_in_group != null && id != 0 && groupId != 0)
                {
                    craneOrderConfig.UpTaskTop(craneNo, index_in_group, id, groupId, out error);
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("CraneOrderConfig_TaskTop");
                        LogManager.WriteProgramLog("行车号：" + craneNo);
                        LogManager.WriteProgramLog("ID：" + id);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshCraneOrderGroupMessage(cbbCraneNo.Text.Trim());
                    }
                }
            }
            else if (index == 0)
            {
                MessageBox.Show("请选择一项任务进行任务置顶");
            }
            else
            {
                MessageBox.Show("只能选择一项任务进行任务置顶");
            }
        }

        //取消选择
        private void btnCancelCheck_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                dgvGroup.Rows[i].Cells["Select"].Value = false;
            }
        }

        //设置执行次数
        private void btnSetTimesCount_Click(object sender, EventArgs e)
        {
            int index = 0;
            int num = 999;
            string error = null;
            for (int i = 0; i < dgvGroup.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGroup.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    index++;
                    num = i;
                }
            }

            if (index == 1)
            {
                string craneNo = null;
                int id = 0;
                if (dgvGroup.Rows[num].Cells["CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvGroup.Rows[num].Cells["CRANE_NO"].Value.ToString().Trim();
                if (dgvGroup.Rows[num].Cells["ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvGroup.Rows[num].Cells["ID"].Value.ToString().Trim());
                if (id != 0 && craneNo != null)
                {
                    FrmSetTimesCount set = new FrmSetTimesCount();
                    set.Id = id;
                    set.CraneNO = craneNo;
                    DialogResult ret = set.ShowDialog();
                    if (ret == DialogResult.OK)
                    {
                        RefreshCraneOrderGroupMessage(cbbCraneNo.Text.Trim());
                        SelectedCheckCell(id);
                    }
                }

            }
            else if (index == 0)
            {
                MessageBox.Show("请选择一项任务进行设置执行次数");
            }
            else
            {
                MessageBox.Show("只能选择一项任务进行设置执行次数");
            }


        }
        #endregion


        #region ------------------表格点击事件---------------------------
        private void dgvGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex < 0)
            {
                return;
            }

            //启用标记按钮
            if (dgvGroup.Columns[e.ColumnIndex].Name == "FLAG_ENABLED")
            {
                string craneNo = null;
                string taskName = null;
                string error = null;
                string flag_enadled = null;
                int id = 0;
                if (dgvGroup.Rows[e.RowIndex].Cells["CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvGroup.Rows[e.RowIndex].Cells["CRANE_NO"].Value.ToString().Trim();

                if (dgvGroup.Rows[e.RowIndex].Cells["TASK_NAME"].Value != DBNull.Value)
                    taskName = dgvGroup.Rows[e.RowIndex].Cells["TASK_NAME"].Value.ToString().Trim();

                if (dgvGroup.Rows[e.RowIndex].Cells["ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvGroup.Rows[e.RowIndex].Cells["ID"].Value.ToString().Trim());

                if (dgvGroup.Rows[e.RowIndex].Cells["FLAG_ENABLED"].Value != DBNull.Value)
                    flag_enadled = dgvGroup.Rows[e.RowIndex].Cells["FLAG_ENABLED"].Value.ToString().Trim();

                if (craneNo != null && taskName != null && id != 0 && flag_enadled != null)
                {
                    if (flag_enadled == "开")
                    {
                        craneOrderConfig.UpCraneOrderEnadled(craneNo,  id, 0, out error);
                        HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",关闭(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                    }
                    else if (flag_enadled == "关")
                    {
                        craneOrderConfig.UpCraneOrderEnadled(craneNo,  id, 1, out error);
                        HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",开启(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                    }
                    else
                    {
                        return;
                    }

                    if (error != null)
                    {
                        LogManager.WriteProgramLog("CraneOrderConfig_StartUingEnadled");
                        LogManager.WriteProgramLog("行车号：" + craneNo);
                        LogManager.WriteProgramLog("任务名称：" + taskName);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshCraneOrderGroupMessage(cbbCraneNo.Text.Trim());
                    }
                }
            }
        }

        private void dgvTaskPRE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex < 0)
            {
                return;
            }

            string error = null;
            //启用按钮
            if (dgvTaskPRE.Columns[e.ColumnIndex].Name == "PRE_FLAG_ENABLED")
            {
                int id = 0;
                string flag_enadled = null;
                string craneNo = null;
                string taskName = null;
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_ID"].Value.ToString().Trim());
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_FLAG_ENABLED"].Value != DBNull.Value)
                    flag_enadled = dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_FLAG_ENABLED"].Value.ToString().Trim();
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_CRANE_NO"].Value.ToString().Trim();
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_DESCRIBE"].Value != DBNull.Value)
                    taskName = dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_DESCRIBE"].Value.ToString().Trim();

                if (id != 0)
                {
                    if (flag_enadled == "开")
                    {
                        craneOrderConfig.UpCraneOrderSeqTypeEnadled(id, 0, out error);
                        HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",关闭(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                    }
                    else if (flag_enadled == "关")
                    {
                        craneOrderConfig.UpCraneOrderSeqTypeEnadled(id, 1, out error);
                        HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",开启(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                    }
                    else
                    {
                        return;
                    }

                    if (error != null)
                    {
                        LogManager.WriteProgramLog("dgvTaskPRE_CellContentClick");
                        LogManager.WriteProgramLog("ID：" + id);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshDgvPER(cbbCraneNo.Text.Trim());
                    }
                }
            }
            //操作按钮

            if (dgvTaskPRE.Columns[e.ColumnIndex].Name == "PRE_HANDLE")
            {
                string craneNo = null;
                int index = 0;
                int id = 0;
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_CRANE_NO"].Value.ToString().Trim();
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_EXCUTE_INDEX"].Value != DBNull.Value)
                    index = Convert.ToInt32(dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_EXCUTE_INDEX"].Value.ToString().Trim());
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_ID"].Value.ToString().Trim());

                if (((DataGridViewButtonCell)dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_HANDLE"]).UseColumnTextForButtonValue != true)
                {
                    return;
                }

                if (id != 0 && index != 0 && craneNo != null)
                {
                    craneOrderConfig.UpSequencePromotionTask(craneNo, index, id, out error);
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("dgvTaskPRE_CellContentClick");
                        LogManager.WriteProgramLog("ID：" + id);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshDgvPER(cbbCraneNo.Text.Trim());
                    }
                }
            }

            //置顶操作按钮
            if (dgvTaskPRE.Columns[e.ColumnIndex].Name == "SET_TOP")
            {
                string craneNo = null;
                int index = 0;
                int id = 0;
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_CRANE_NO"].Value.ToString().Trim();
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_EXCUTE_INDEX"].Value != DBNull.Value)
                    index = Convert.ToInt32(dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_EXCUTE_INDEX"].Value.ToString().Trim());
                if (dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvTaskPRE.Rows[e.RowIndex].Cells["PRE_ID"].Value.ToString().Trim());

                if (((DataGridViewButtonCell)dgvTaskPRE.Rows[e.RowIndex].Cells["SET_TOP"]).UseColumnTextForButtonValue != true)
                {
                    return;
                }

                if (id != 0 && index != 0 && craneNo != null)
                {
                    for (int i = index; i > 1; i--)
                    {
                        craneOrderConfig.UpSequencePromotionTask(craneNo, i, id, out error);
                    }
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("dgvTaskPRE_CellContentClick");
                        LogManager.WriteProgramLog("ID：" + id);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshDgvPER(cbbCraneNo.Text.Trim());
                    }
                }
            }
        }

        private void dgvTaskPOST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }

            string error = null;
            if (dgvTaskPOST.Columns[e.ColumnIndex].Name == "POST_FLAG_ENABLED")
            {
                int id = 0;
                string flag_enadled = null;
                string craneNo = null;
                string taskName = null;
                if (dgvTaskPOST.Rows[e.RowIndex].Cells["POST_ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvTaskPOST.Rows[e.RowIndex].Cells["POST_ID"].Value.ToString().Trim());
                if (dgvTaskPOST.Rows[e.RowIndex].Cells["POST_FLAG_ENABLED"].Value != DBNull.Value)
                    flag_enadled = dgvTaskPOST.Rows[e.RowIndex].Cells["POST_FLAG_ENABLED"].Value.ToString().Trim();
                if (dgvTaskPOST.Rows[e.RowIndex].Cells["POST_CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvTaskPOST.Rows[e.RowIndex].Cells["POST_CRANE_NO"].Value.ToString().Trim();
                if (dgvTaskPOST.Rows[e.RowIndex].Cells["POST_DESCRIBE"].Value != DBNull.Value)
                    taskName = dgvTaskPOST.Rows[e.RowIndex].Cells["POST_DESCRIBE"].Value.ToString().Trim();
                
                if (id != 0)
                {
                    if (flag_enadled == "开")
                    {
                        craneOrderConfig.UpCraneOrderSeqTypeEnadled(id, 0, out error);
                        if (taskName == "按生产计划倒垛")
                        {
                            UpCraneFlagEnabledByStop1or6or7(craneNo);
                        }
                        else if(taskName == "冷卷倒垛")
                        {
                            UpCraneFlagEnabledByStopNo1and6and7(craneNo);
                        }
                        HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",关闭(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                    }
                    else if (flag_enadled == "关")
                    {
                        craneOrderConfig.UpCraneOrderSeqTypeEnadled(id, 1, out error);
                        if (taskName == "按生产计划倒垛")
                        {
                            UpCraneFlagEnabledByOpen1or6or7(craneNo);
                        }
                        else if (taskName == "冷卷倒垛")
                        {
                            UpCraneFlagEnabledByOpenNo1and6and7(craneNo);
                        }
                        HMILogger.WriteLog("行车指令", "行车：" + craneNo + ",开启(" + taskName + ")行车任务指令", LogLevel.Info, this.Text);
                    }
                    else
                    {
                        return;
                    }
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("dgvTaskPOST_CellContentClick");
                        LogManager.WriteProgramLog("ID：" + id);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {

                        RefreshDgvPOST(cbbCraneNo.Text.Trim());
                    }
                }
            }

            //操作按钮
            if (dgvTaskPOST.Columns[e.ColumnIndex].Name == "POST_HANDLE")
            {
                string craneNo = null;
                int index = 0;
                int id = 0;
                if (dgvTaskPOST.Rows[e.RowIndex].Cells["POST_CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvTaskPOST.Rows[e.RowIndex].Cells["POST_CRANE_NO"].Value.ToString().Trim();
                if (dgvTaskPOST.Rows[e.RowIndex].Cells["POST_EXCUTE_INDEX"].Value != DBNull.Value)
                    index = Convert.ToInt32(dgvTaskPOST.Rows[e.RowIndex].Cells["POST_EXCUTE_INDEX"].Value.ToString().Trim());
                if (dgvTaskPOST.Rows[e.RowIndex].Cells["POST_ID"].Value != DBNull.Value)
                    id = Convert.ToInt32(dgvTaskPOST.Rows[e.RowIndex].Cells["POST_ID"].Value.ToString().Trim());

                if (id != 0 && index != 0 && craneNo != null)
                {
                    craneOrderConfig.UpSequencePromotionTask(craneNo, index, id, out error);
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("dgvTaskPOST_CellContentClick");
                        LogManager.WriteProgramLog("ID：" + id);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshDgvPOST(cbbCraneNo.Text.Trim());
                    }
                }
            }
        }
        #endregion



        #region  ------------------右击点击事件---------------------------

        private void ToolStripMenuItem_Delect_PRE_Click(object sender, EventArgs e)
        {
             
            if (dgvTaskPRE.Rows.Count == 0)
            {
                MessageBox.Show("前置任务没有数据可以删除");
                return;
            }

            string error = null;
            int index = dgvTaskPRE.CurrentRow.Index;
            if (index >= 0)
            {
                string craneNo = null;
                string taskName = null;
                string describe = null;

                if (dgvTaskPRE.Rows[index].Cells["PRE_CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvTaskPRE.Rows[index].Cells["PRE_CRANE_NO"].Value.ToString().Trim();

                if (dgvTaskPRE.Rows[index].Cells["PRE_TASK_NAME"].Value != DBNull.Value)
                    taskName = dgvTaskPRE.Rows[index].Cells["PRE_TASK_NAME"].Value.ToString().Trim();

                if (dgvTaskPRE.Rows[index].Cells["PRE_DESCRIBE"].Value != DBNull.Value)
                    describe = dgvTaskPRE.Rows[index].Cells["PRE_DESCRIBE"].Value.ToString().Trim();

                DialogResult dr = MessageBox.Show("确定要删除行车【" + craneNo + "】的【" + describe + "】的任务吗", "删除确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    craneOrderConfig.DelSequenceTask(craneNo, UACSDAL.CraneOrderConfig.SEQ_TYPE.PRE, taskName, out error);
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("ToolStripMenuItem_Delect_PRE_Click");
                        LogManager.WriteProgramLog("craneNo：" + craneNo);
                        LogManager.WriteProgramLog("taskName：" + taskName);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshDgvPER(cbbCraneNo.Text.Trim());

                        LogManager.GenSQL(craneNo, "PRE", taskName,5,"行车指令配置删除",out error);
                        if (error != null)
                        {
                             LogManager.WriteProgramLog("行车指令配置删除-PRE");
                             LogManager.WriteProgramLog("craneNo：" + craneNo);
                             LogManager.WriteProgramLog("taskName：" + taskName);
                             LogManager.WriteProgramLog(error);
                        }
                    }
                  
                }
            }


        }

        private void ToolStripMenuItem_AddTask__PRE_Click(object sender, EventArgs e)
        {
            FrmAddTask add = new FrmAddTask();
            add.MySqeType = UACSDAL.CraneOrderConfig.SEQ_TYPE.PRE;
            add.MyCraneNo = cbbCraneNo.Text.Trim();
            DialogResult ret = add.ShowDialog();
            if (ret == DialogResult.OK)
            {
                RefreshDgvPER(cbbCraneNo.Text.Trim());
            }

        }

        private void toolStripMenuItem_Delect_POST_Click(object sender, EventArgs e)
        {

            if (dgvTaskPOST.Rows.Count == 0)
            {
                MessageBox.Show("后置任务没有数据可以删除");
                return;
            }


            string error = null;
            int index = dgvTaskPOST.CurrentRow.Index;
            if (index >= 0)
            {
                string craneNo = null;
                string taskName = null;
                string describe = null;

                if (dgvTaskPOST.Rows[index].Cells["POST_CRANE_NO"].Value != DBNull.Value)
                    craneNo = dgvTaskPOST.Rows[index].Cells["POST_CRANE_NO"].Value.ToString().Trim();

                if (dgvTaskPOST.Rows[index].Cells["POST_TASK_NAME"].Value != DBNull.Value)
                    taskName = dgvTaskPOST.Rows[index].Cells["POST_TASK_NAME"].Value.ToString().Trim();

                if (dgvTaskPOST.Rows[index].Cells["POST_DESCRIBE"].Value != DBNull.Value)
                    describe = dgvTaskPOST.Rows[index].Cells["POST_DESCRIBE"].Value.ToString().Trim();

                DialogResult dr = MessageBox.Show("确定要删除行车【" + craneNo + "】的【" + describe + "】的任务吗", "删除确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    craneOrderConfig.DelSequenceTask(craneNo, UACSDAL.CraneOrderConfig.SEQ_TYPE.POST, taskName, out error);
                    if (error != null)
                    {
                        LogManager.WriteProgramLog("toolStripMenuItem_Delect_POST_Click");
                        LogManager.WriteProgramLog("craneNo：" + craneNo);
                        LogManager.WriteProgramLog("taskName：" + taskName);
                        LogManager.WriteProgramLog(error);
                        MessageBox.Show(error);
                    }
                    else
                    {
                        RefreshDgvPOST(cbbCraneNo.Text.Trim());
                        LogManager.GenSQL(craneNo, "POST", taskName, 5, "行车指令配置删除", out error);
                        if (error != null)
                        {
                            LogManager.WriteProgramLog("行车指令配置删除-POST");
                            LogManager.WriteProgramLog("craneNo：" + craneNo);
                            LogManager.WriteProgramLog("taskName：" + taskName);
                            LogManager.WriteProgramLog(error);
                        }
                    }
                }
            }
        }

        private void toolStripMenuItem_AddTask__POST_Click(object sender, EventArgs e)
        {

            FrmAddTask add = new FrmAddTask();
            add.MySqeType = UACSDAL.CraneOrderConfig.SEQ_TYPE.POST;
            add.MyCraneNo = cbbCraneNo.Text.Trim();
            DialogResult ret = add.ShowDialog();
            if (ret == DialogResult.OK)
            {
                RefreshDgvPOST(cbbCraneNo.Text.Trim());
            }
        }

        #endregion


        #region -----------------------------画面切换--------------------------------
        //private bool tabActived = true;
        void MyTabActivated(object sender, EventArgs e)
        {
            string craneNo = cbbCraneNo.Text.Trim();
            RefreshCraneOrderGroupMessage(craneNo);
            RefreshDgvPER(craneNo);
            RefreshDgvPOST(craneNo);
        }
        void MyTabDeactivated(object sender, EventArgs e)
        {
        }
        #endregion

        private void btnReturnMonitor_Click(object sender, EventArgs e)
        {
            string bay = this.cbbBayNo.Text.Trim();
            bay = this.cbbBayNo.SelectedValue.ToString().Trim();
            switch (bay)
            {
                case "Z62":
                    auth.OpenForm("01 Z62跨监控");
                    break;
                case "Z63":
                    auth.OpenForm("02 Z63跨监控");
                    break;
                case "Z01":
                    auth.OpenForm("01 Z01跨监控");
                    break;
                default:
                    break;
            }
            //if (bay.Contains("62"))
            //{
            //    auth.OpenForm("01 Z62跨监控");
            //}
            //else
            //{
            //    auth.OpenForm("02 Z63跨监控");
            //}
        }

        private void Transform_Chinese(DataGridView dgv)
        {
            //string dgvName = dgv.Name.ToString().Substring(7);
            string dgvCraneNO;
            string dgvTaskName;
            if (dgv.Name.ToString().Contains("PRE"))
            {
                dgvCraneNO = "PRE_CRANE_NO";
                dgvTaskName = "PRE_TASK_NAME";
            }
            else if(dgv.Name.ToString().Contains("POST"))
            {
                dgvCraneNO = "POST_CRANE_NO";
                dgvTaskName = "POST_TASK_NAME";
            }
            else
            {
                dgvCraneNO = "CRANE_NO";
                dgvTaskName = "TASK_NAME";
            }

            try
            {
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    //string CRANE_NO = r.Cells[dgvName+"_CRANE_NO"].Value.ToString().Trim();
                    //string TASK_NAME = r.Cells[dgvName + "_TASK_NAME"].Value.ToString().Trim();
                    string CRANE_NO = r.Cells[dgvCraneNO].Value.ToString().Trim();
                    string TASK_NAME = r.Cells[dgvTaskName].Value.ToString().Trim();
                    string sql = string.Format("SELECT DESCRIBE FROM CRANE_ORDER_TASK_DEFINE WHERE CRANE_NO = '{0}' AND TASK_NAME = '{1}'", CRANE_NO, TASK_NAME);
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        while (rdr.Read())
                        {
                            if (rdr["DESCRIBE"] != System.DBNull.Value)
                            {
                                r.Cells[dgvTaskName].Value = rdr["DESCRIBE"];
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbCraneNo_TextChanged(object sender, EventArgs e)
        {
            string craneNo = cbbCraneNo.Text.Trim();
            RefreshCraneOrderGroupMessage(craneNo);
            RefreshDgvPER(craneNo);
            RefreshDgvPOST(craneNo);
        }

        private void dgvRefresh(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                int flag = 0;
                if (dgv.Rows[i].Cells["FIXED"].Value != DBNull.Value)
                {
                    flag = Convert.ToInt32(dgv.Rows[i].Cells["FIXED"].Value);
                }
                if (flag != 1)
                {
                    ((DataGridViewButtonCell)dgv.Rows[i].Cells["PRE_HANDLE"]).UseColumnTextForButtonValue = true;
                    ((DataGridViewButtonCell)dgv.Rows[i].Cells["SET_TOP"]).UseColumnTextForButtonValue = true;
                }
            }
        }

        public bool UpCraneFlagEnabledByOpen1or6or7(string _craneNo)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 1 where CRANE_NO = '" + _craneNo + "' and ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN =1 or FLAG_FIND_BY_PLAN =6 or FLAG_FIND_BY_PLAN =7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        public bool UpCraneFlagEnabledByStop1or6or7(string _craneNo)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 0 where CRANE_NO = '" + _craneNo + "' and ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN =1 or FLAG_FIND_BY_PLAN =6 or FLAG_FIND_BY_PLAN =7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        public bool UpCraneFlagEnabledByOpenNo1and6and7(string _craneNo)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 1 where CRANE_NO = '" + _craneNo + "' and ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN !=1 and FLAG_FIND_BY_PLAN !=6 and FLAG_FIND_BY_PLAN !=7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        public bool UpCraneFlagEnabledByStopNo1and6and7(string _craneNo)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 0 where CRANE_NO = '" + _craneNo + "' and ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN !=1 and FLAG_FIND_BY_PLAN !=6 and FLAG_FIND_BY_PLAN !=7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }
    }
}
