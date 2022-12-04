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
    public partial class FrmForbidenCoilFlow : Form
    {
        public FrmForbidenCoilFlow()
        {
            InitializeComponent();

        }
        private string forbidenCoilWhereTo;

        private string coilNo;
        public string CoilNo
        {
            get { return coilNo; }
            set { coilNo = value; }
        }

        private string unitNo;
        public string UnitNo
        {
            get { return unitNo; }
            set { unitNo = value; }
        }

        private int forbidenFlag;
        public int ForbidenFlag
        {
            get { return forbidenFlag; }
            set { forbidenFlag = value; }
        }

        private int coilStatus;
        public int CoilStatus
        {
            get { return coilStatus; }
            set { coilStatus = value; }
        }

        string sql = String.Empty;
        //SQL执行
        private void executeNonQuery(string i)
        {
            sql = @"UPDATE UACS_YARDMAP_COIL  SET FORBIDEN_COIL_WHERE_TO = '{0}' WHERE COIL_NO = '{1}'";
            sql = string.Format(sql, forbidenCoilWhereTo, coilNo);
            try
            {
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_ToStock_Click(object sender, EventArgs e)
        {
            forbidenCoilWhereTo = "0";
            executeNonQuery(forbidenCoilWhereTo);
            MessageBox.Show("已更新封闭卷流向为到库区状态");
            this.Close();
        }

        private void btn_ToUnit_Click(object sender, EventArgs e)
        {
            forbidenCoilWhereTo = "1";
            executeNonQuery(forbidenCoilWhereTo);
            MessageBox.Show("已更新封闭卷流向为到机组状态");
            this.Close();
        }

        private void btn_ToOfflinePacking_Click(object sender, EventArgs e)
        {
            forbidenCoilWhereTo = "2";
            executeNonQuery(forbidenCoilWhereTo);
            MessageBox.Show("已更新封闭卷流向为到离线状态");
            this.Close();
        }

        private void FrmForbidenCoilFlow_Load(object sender, EventArgs e)
        {
            
            label2.Text = coilNo;
            if(forbidenFlag != 1)
            {
                this.Text = "中间料流向选择";
                btnMidCoilToYard.Visible = true;
                btn_ToStock.Visible = false;
                btn_ToUnit.Visible = false;
                btn_ToOfflinePacking.Visible = false;
            }
            else
            {
                this.Text = "封闭卷流向选择";
                btnMidCoilToYard.Visible = false;
                if (unitNo.Contains("D108"))
                {
                    btn_ToStock.Visible = false;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            forbidenCoilWhereTo = "";
            executeNonQuery(forbidenCoilWhereTo);
            MessageBox.Show("已更新封闭卷流向为初始状态");
            this.Close();
        }

        private void btnMidCoilToYard_Click(object sender, EventArgs e)
        {
            forbidenCoilWhereTo = "6";
            executeNonQuery(forbidenCoilWhereTo);
            MessageBox.Show("已更新卷流向为中间料入库");
            this.Close();
        }
    }
}
