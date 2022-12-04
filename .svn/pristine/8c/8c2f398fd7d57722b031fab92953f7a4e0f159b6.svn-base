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
    public partial class FrmSetTimesCount : Form
    {
        public FrmSetTimesCount()
        {
            InitializeComponent();
            string str = Application.StartupPath + @"\Eighteen.ssk";
           // this.skinEngine1.SkinFile = str;
           // this.skinEngine1.SkinAllForm = false;
        }
        CraneOrderConfig config = new CraneOrderConfig();
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        private string craneNO;

        public string CraneNO
        {
            get { return craneNO; }
            set { craneNO = value; }
        }


        private void FrmSetTimesCount_Load(object sender, EventArgs e)
        {
            this.Text = "ID【" + id + "】设置执行次数";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            string error = null;
            try
            {
                 count = Convert.ToInt32( txtCount.Text.Trim());
            }
            catch (Exception er)
            {
                MessageBox.Show("请输入数字");
                return;
            }


            if (count > 10)
            {
                MessageBox.Show("输入参数过大");
                return;
            }

            config.SetExcuteTimesCount(id, count, out error);

            if (error != null)
            {
                LogManager.WriteProgramLog(id.ToString());
                LogManager.WriteProgramLog(error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

       
        
        
    }
}
