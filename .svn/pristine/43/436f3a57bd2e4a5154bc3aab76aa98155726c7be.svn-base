using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public partial class FrmAddTask : Form
    {
        public FrmAddTask()
        {
            InitializeComponent();
        }
        private DataTable dgvTaskDefineDt = new DataTable();
        private CraneOrderConfig config;


        private CraneOrderConfig.SEQ_TYPE mySqeType;

        public CraneOrderConfig.SEQ_TYPE MySqeType
        {
            get { return mySqeType; }
            set { mySqeType = value; }
        }


        private string myCraneNo = string.Empty;

        public string MyCraneNo
        {
            get { return myCraneNo; }
            set { myCraneNo = value; }
        }

        private void FrmAddTask_Shown(object sender, EventArgs e)
        {
            if (mySqeType == CraneOrderConfig.SEQ_TYPE.PRE)
            {
                this.Text = myCraneNo + "行车前置任务添加";
            }
            if (mySqeType == CraneOrderConfig.SEQ_TYPE.POST)
            {
                 this.Text = myCraneNo + "行车后置任务添加";
            }
            config = new CraneOrderConfig();

            RefreshTaskDefine();

        }
       
        private void RefreshTaskDefine()
        {
            string error = null;
            dgvTaskDefineDt.Clear();

            dgvTaskDefineDt = config.GetTaskDefineData(myCraneNo,out error);
            if (error != null)
            {
                LogManager.WriteProgramLog(error);
                MessageBox.Show("查询CRANE_ORDER_TASK_DEFINE表出错");
            }
            else
            {
                dgvTaskDefine.DataSource = dgvTaskDefineDt;
            }

        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            int index = 0;
            int num = 999;
            string error = null;
            for (int i = 0; i < dgvTaskDefine.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvTaskDefine.Rows[i].Cells["Select"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    index++;
                    num = i;
                }
            }
            if (index == 1)
            {
                string describe = null;
                string taskName = null;

                if (dgvTaskDefine.Rows[num].Cells["TASK_NAME"].Value != DBNull.Value)
                    taskName = dgvTaskDefine.Rows[num].Cells["TASK_NAME"].Value.ToString().Trim();

                if (dgvTaskDefine.Rows[num].Cells["DESCRIBE"].Value != DBNull.Value)
                    describe = dgvTaskDefine.Rows[num].Cells["DESCRIBE"].Value.ToString().Trim();

                if (taskName == null && describe == null)
	            {
                    MessageBox.Show("任务值为空");
                    return;
	            }

                if (config.CheckSequenceTaskName(myCraneNo,mySqeType,taskName,out error))
                {
                    MessageBox.Show("任务已存在,请重新选择任务");
                    CancelChecked();

                }
                else
                {
                    if (error != null)
                    {
                         LogManager.WriteProgramLog(error);
                         MessageBox.Show(error);
                    }
                    else
                    {
                        if (config.AddSequenceTask(myCraneNo, mySqeType, taskName, describe, out error))
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            LogManager.WriteProgramLog(error);
                            MessageBox.Show(error);
                            CancelChecked();
                        }
                    }
                }

            }
            else if (index == 0)
            {
                MessageBox.Show("请选择一项任务添加");
            }
            else
            {
                MessageBox.Show("只能选择一项任务添加");
                CancelChecked();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        

        private void CancelChecked()
        {
            for (int i = 0; i < dgvTaskDefine.Rows.Count; i++)
            {
                dgvTaskDefine.Rows[i].Cells["Select"].Value = false;
            }
        }


    }
}
