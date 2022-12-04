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
    public partial class FrmCraneEvadePriority : Form
    {
        public FrmCraneEvadePriority()
        {
            InitializeComponent();
        }

        private string orderNO = string.Empty;
        //指令号
        public string OrderNO
        {
            get { return orderNO; }
            set { orderNO = value; }
        }

        private string craneEvadePriority = string.Empty;
        //指令优先级
        public string CraneEvadePriority
        {
            get { return craneEvadePriority; }
            set { craneEvadePriority = value; }
        }

        private string priority = string.Empty;
        //修改优先级
        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //判断提升指令优先级
            priority = txtPriority.Text;
            if (priority.Trim()=="")
            {
                MessageBox.Show("指令提升优先级不能为空，请重新输入！");
                return;
            }

            //判断指令状态是否为初始化状态
            string craneNO = String.Empty;
            string sql = string.Format("select CRANE_NO from WMS_CRANE_EVADE  where ORDER_NUMBER = '{0}'", orderNO);
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    craneNO = rdr["CRANE_NO"].ToString().Trim();
                }
            }
            if(craneNO != "")
            {
                MessageBox.Show("该指令不处于初始状态，指令已执行，不允许修改优先级！");
                return;
            }

            string sqlPriority1 = string.Format("Update WMS_CRANE_EVADE set TASK_PRIORITY = '{0}' where ORDER_NUMBER = '{1}'", priority, orderNO);
            DB2Connect.DBHelper.ExecuteNonQuery(sqlPriority1);

            MessageBox.Show("成功提升指令优先级！");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
       }

        private void FrmOrderPriority_Load(object sender, EventArgs e)
        {
            labOrderNO.Text = orderNO;
            labOrderPriority.Text = craneEvadePriority;
        }
    }
}
