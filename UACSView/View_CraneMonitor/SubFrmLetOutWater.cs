using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ParkClassLibrary;
using UACSControls;

namespace UACSView
{
    public partial class SubFrmLetOutWater : Form
    {
        #region iPlature配置
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = null;
        public Baosight.iSuperframe.TagService.Controls.TagDataProvider TagDP
        {
            get
            {
                if (tagDP == null)
                {
                    try
                    {
                        tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
                        tagDP.ServiceName = "iplature";
                        tagDP.AutoRegist = true;
                    }
                    catch (System.Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                }
                return tagDP;
            }
            //set { tagDP = value; }
        }
        #endregion

        #region  -----------------------------连接数据库--------------------------------
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
        #endregion
        string tagNameStart;
        //string tagNamePause;
        bool craneDrainWater = false;

        public bool CraneDrainWater
        {
            get { return craneDrainWater; }
            set { craneDrainWater = value; }
        }

       
        public SubFrmLetOutWater()
        {
            InitializeComponent();
        }
        public SubFrmLetOutWater(string name)
        {
            InitializeComponent();
            CraneName = name;
            InitTagname(name);
        }
        private void InitTagname(string name)
        {
            switch (name)
            {
                case "3_1":
                    tagNameStart = "3_1_DownLoadWater";
                    break;
                case "3_2":
                    tagNameStart = "3_2_DownLoadWater";
                    break;
                case "3_3":
                    tagNameStart = "3_3_DownLoadWater";
                    break;
                case "3_4":
                    tagNameStart = "3_4_DownLoadWater";
                    break;
                case "3_5":
                    tagNameStart = "3_5_DownLoadWater";
                    break;          
                default:
                    break;
            }
        
        }
        string craneName;
        public string CraneName
        {
            get { return craneName; }
            set { craneName = value; }
        }
        //开始
        private void button1_Click(object sender, EventArgs e)
        {
           
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要对" + craneName + "#行车进行排水？", "提示", btn,MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                //TagDP.SetData(tagNameStart, "1");
                string sql = @"update CRANE_PIPI set STATUS = 'TO_BE_START' where CRANE_NO = '" + craneName + "'";
                DBHelper.ExecuteNonQuery(sql);
                ParkClassLibrary.HMILogger.WriteLog(button1.Text, craneName + "行车进行排水：", ParkClassLibrary.LogLevel.Info, this.Text);

            }
            else
            {

            }
            this.Close();
        }
        //暂停
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要对" + craneName + "#行车暂停排水？", "提示", btn, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                //TagDP.SetData(tagNameStart, "0");
                string sql = @"update CRANE_PIPI set STATUS = 'TO_BE_END' where CRANE_NO = '" + craneName + "'";
                DBHelper.ExecuteNonQuery(sql);
                ParkClassLibrary.HMILogger.WriteLog(button1.Text, craneName + "行车进行排水：", ParkClassLibrary.LogLevel.Info, this.Text);
            }
            else
            {

            }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
