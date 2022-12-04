using UACSDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSControls
{
    public partial class FrmCraneConfig : Form
    {
        public string CraneNo { get; set; }
        public int ID { get; set; }
        public int Xmin { get; set; }
        public int Xmax { get; set; }
        public int Ymin { get; set; }
        public int Ymax { get; set; }
        public string Xdir { get; set; }

        public FrmCraneConfig()
        {
            InitializeComponent();
            //string str = Application.StartupPath + @"\Eighteen.ssk";
            //this.skinEngine1.SkinFile = str;
            //this.skinEngine1.SkinAllForm = false;
            txtXmax.KeyPress += txtXmax_KeyPress;
            txtXmin.KeyPress += txtXmax_KeyPress;
            txtYmin.KeyPress += txtXmax_KeyPress;
            txtYmax.KeyPress += txtXmax_KeyPress;
            this.Load += FrmCraneConfig_Load;
        }

        void FrmCraneConfig_Load(object sender, EventArgs e)
        {
            txtID.Text = ID.ToString();
            txtCraneNo.Text = CraneNo;
            txtXmin.Text = Xmin.ToString();
            txtXmax.Text = Xmax.ToString();
            txtYmin.Text = Ymin.ToString();
            txtYmax.Text = Ymax.ToString();
            txtXdir.Text = Xdir;

        }



        void txtXmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        public enum CraneConfig
        {
            CarToYard,
            YardToCar,
            ToArea
        }


        private CraneConfig myCraneConfig;

        public CraneConfig MyCraneConfig
        {
            get { return myCraneConfig; }
            set { myCraneConfig = value; }
        }
        

        
        private bool UpFindCoil(int _Xmin,int _Xmax,int _Ymin,int _Ymax,string _XDir)
        {
            try
            {
                string sql = @"UPDATE YARD_TO_YARD_FIND_COIL_STRATEGY SET ";
                sql += " X_MIN = " + _Xmin + ",X_MAX = " + _Xmax + ",Y_MIN = " + _Ymin + ",Y_MAX = " + _Ymax + ",X_DIR = '" + _XDir + "'";
                sql += " WHERE ID = "+txtID.Text.Trim()+"";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                MessageBox.Show("修改失败！！！");
                return false;
            }
            return true;
        }

        private bool UpFindSaddle(int _Xmin, int _Xmax, int _Ymin, int _Ymax, string _XDir)
        {
            try
            {
                string sql = @"UPDATE YARD_TO_YARD_FIND_SADDLE_STRATEGY SET ";
                sql += " X_MIN = " + _Xmin + ",X_MAX = " + _Xmax + ",Y_MIN = " + _Ymin + ",Y_MAX = " + _Ymax + ",X_DIR = '" + _XDir + "'";
                sql += " WHERE ID = " + txtID.Text.Trim() + "";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                MessageBox.Show("修改失败！！！");
                return false;
            }
            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtXmin.Text.Trim() != "" && txtXmax.Text.Trim() != "" && txtYmin.Text.Trim() != "" && txtYmax.Text.Trim() != "" && txtXdir.Text.Trim() != "")
            {
                //入库
                if (myCraneConfig == CraneConfig.CarToYard)
                {
                  bool flag =  UpFindSaddle(Convert.ToInt32(txtXmin.Text.Trim()), Convert.ToInt32(txtXmax.Text.Trim()),
                       Convert.ToInt32(txtYmin.Text.Trim()), Convert.ToInt32(txtYmax.Text.Trim()), txtXdir.Text.Trim());
                  if (flag)
                  {
                       this.DialogResult = DialogResult.OK;
                  }
                }
                //出库
                if (myCraneConfig == CraneConfig.YardToCar)
                {
                    bool flag = UpFindCoil(Convert.ToInt32(txtXmin.Text.Trim()), Convert.ToInt32(txtXmax.Text.Trim()), 
                        Convert.ToInt32(txtYmin.Text.Trim()), Convert.ToInt32(txtYmax.Text.Trim()), txtXdir.Text.Trim());
                    if (flag)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            else
                MessageBox.Show("所有数据不能为空！！！");
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


    }
}
