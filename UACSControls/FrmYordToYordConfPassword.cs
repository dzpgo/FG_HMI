using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACS
{
    public partial class FrmYordToYordConfPassword : Form
    {

        #region 数据库连接
        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;
        //连接数据库
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
                    }
                    catch (System.Exception e)
                    {
                        //throw e;
                    }
                }
                return dbHelper;
            }
        }

        public string Remark = string.Empty;
        private string User = string.Empty;
        private string PassWord = string.Empty;
        #endregion
        public FrmYordToYordConfPassword()
        {
            InitializeComponent();
            string str = Application.StartupPath + @"\Eighteen.ssk";
            this.skinEngine1.SkinFile = str;
            this.skinEngine1.SkinAllForm = false;
        }

        private string passWords = string.Empty;
        //step2
        public string PassWords
        {
            get { return passWords; }
            set { passWords = value; }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            //if(Remark == "修正行车高度/角度")
            //{
            //    User = "修正行车高度";
            //}
            //string sql = @"SELECT * FROM HMI_PASSWORD WHERE REMARK = '" + User + "'";
            //using (IDataReader rdr = DBHelper.ExecuteReader(sql))
            //{
            //    rdr.Read();
            //    PassWord = rdr["PASSWORD"].ToString();
            //}
            //if (txtPassword.Text.Trim() == PassWord)
            //{
            //     this.DialogResult = DialogResult.OK;
            //}
            if (txtPassword.Text.Trim() == passWords)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        
    }
}
