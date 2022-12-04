
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using UACSDAL;
using UACSControls;

namespace UACSView
{
    public partial class Frm_D172WJ3 : FormBase
    {
        public Frm_D172WJ3()
        {
            InitializeComponent();
            this.Load += FrmSaddleShow_Load;
        }
        private const string tagServiceName = "iplature";
        private string theBayNO = "Z22";
        private bool flag = false;
        private Dictionary<string, conArea> dicAreaVisual = new Dictionary<string, conArea>();
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
        #endregion

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

        #region TAG配置
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public void InitTagDataProvide(string tagServiceName)
        {
            try
            {
                tagDataProvider.ServiceName = tagServiceName;
            }
            catch (Exception ex)
            {
            }
        }
        private Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
        private string[] arrTagAdress;
        private void readTags()
        {
            try
            {
                inDatas.Clear();
                tagDataProvider.GetData(arrTagAdress, out inDatas);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 取tag值
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private long get_value_x(string tagName)
        {
            long theValue = 0;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToInt32(valueObject);
            }
            catch
            {
                valueObject = null;
            }
            return theValue; ;
        }
        #endregion

        private AreaInfo theAreaInfoInBay = new AreaInfo();

        private AreaBase areaBase = new AreaBase();
        public AreaBase AreaBase
        {
            get { return areaBase; }
            set { areaBase = value; }
        }
        conStockSaddleModel stockSaddleModel = null;



        void FrmSaddleShow_Load(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            theAreaInfoInBay.conInit(theBayNO, tagServiceName, flag);
            refreshControl();
            //窗体固定大小
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

            lblArea.Text = areaBase.AreaNo + "库位详细信息";

            if (areaBase.AreaNo.Contains("WJ"))
            {
                btnKillStock.Visible = true;
                btnResetStock.Visible = true;
                btnResetLight.Visible = true;
                tagDataProvider.ServiceName = tagServiceName;
                SetReady();
            }

            this.Shown += FrmSaddleShow_Shown;
            //GDIPant();

        }

        void FrmSaddleShow_Shown(object sender, EventArgs e)
        {
            stockSaddleModel = new conStockSaddleModel();
            if (areaBase.AreaNo.Contains("Z21") || areaBase.AreaNo.Contains("Z22") || areaBase.AreaNo.Contains("Z08") || areaBase.AreaNo.Contains("Z07"))
            {
                stockSaddleModel.conInit(panel2, areaBase, constData.tagServiceName, panel2.Width, panel2.Height, constData.xAxisRight, constData.yBxisDown, 999);
            }
            else
            {
                stockSaddleModel.conInit(panel2, areaBase, constData.tagServiceName, panel2.Width, panel2.Height, constData.xAxisRight, constData.yAxisDown, 999);
            }
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdata_Click(object sender, EventArgs e)
        {
            if (areaBase.AreaNo.Contains("Z21") || areaBase.AreaNo.Contains("Z22") || areaBase.AreaNo.Contains("Z08") || areaBase.AreaNo.Contains("Z07"))
            {
                stockSaddleModel.conInit(panel2, areaBase, constData.tagServiceName, panel2.Width, panel2.Height, constData.xAxisRight, constData.yBxisDown, 999);
            }
            else
            {
                stockSaddleModel.conInit(panel2, areaBase, constData.tagServiceName, panel2.Width, panel2.Height, constData.xAxisRight, constData.yAxisDown, 999);
            }
        }

        private void btnKillStock_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要全部杀红小尾卷鞍座吗？", "操作提示", btn);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET LOCK_FLAG = 1, EVENT_ID = 888888 WHERE LOCK_FLAG != 2 AND STOCK_NO IN(SELECT SADDLE_NO FROM UACS_YARDMAP_SADDLE_DEFINE WHERE COL_ROW_NO = '" + areaBase.AreaNo + "')";
                DBHelper.ExecuteNonQuery(sql);
                ParkClassLibrary.HMILogger.WriteLog("全部杀红", "全部杀红：" + areaBase.AreaNo, ParkClassLibrary.LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void btnResetStock_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要全部放白小尾卷鞍座吗？", "操作提示", btn);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,LOCK_FLAG = 0,MAT_NO = NULL,EVENT_ID= 888888 WHERE LOCK_FLAG != 2 AND STOCK_NO IN(SELECT SADDLE_NO FROM UACS_YARDMAP_SADDLE_DEFINE WHERE COL_ROW_NO = '" + areaBase.AreaNo + "')";
                DBHelper.ExecuteNonQuery(sql);
                ParkClassLibrary.HMILogger.WriteLog("全部放白", "全部放白：" + areaBase.AreaNo, ParkClassLibrary.LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void btnResetLight_Click(object sender, EventArgs e)
        {
            readTags();
            if (areaBase.AreaNo.Contains("D172"))
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要光幕复位吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    inDatas["D172WJ_LIGHT_CURTAIN_RESET"] = 1;
                    tagDataProvider.Write2Level1(inDatas, 1);
                    ParkClassLibrary.HMILogger.WriteLog(btnResetLight.Text, "D172WJ" + btnResetLight.Text, ParkClassLibrary.LogLevel.Info, "库区详细画面");
                }
            }
            else if (areaBase.AreaNo.Contains("D173"))
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要光幕复位吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    inDatas["D173WJ_LIGHT_CURTAIN_RESET"] = 1;
                    tagDataProvider.Write2Level1(inDatas, 1);
                    ParkClassLibrary.HMILogger.WriteLog(btnResetLight.Text, "D173WJ" + btnResetLight.Text, ParkClassLibrary.LogLevel.Info, "库区详细画面");
                }
            }
            else if (areaBase.AreaNo.Contains("D174"))
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要光幕复位吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    inDatas["D174WJ_LIGHT_CURTAIN_RESET"] = 1;
                    tagDataProvider.Write2Level1(inDatas, 1);
                    ParkClassLibrary.HMILogger.WriteLog(btnResetLight.Text, "D174WJ" + btnResetLight.Text, ParkClassLibrary.LogLevel.Info, "库区详细画面");
                }
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (areaBase.AreaNo.Contains("D172"))
            {
                inDatas["D172WJ_LIGHT_CURTAIN_RESET"] = 0;
                tagDataProvider.Write2Level1(inDatas, 1);
            }
            else if (areaBase.AreaNo.Contains("D173"))
            {
                inDatas["D173WJ_LIGHT_CURTAIN_RESET"] = 0;
                tagDataProvider.Write2Level1(inDatas, 1);
            }
            else if (areaBase.AreaNo.Contains("D174"))
            {
                inDatas["D174WJ_LIGHT_CURTAIN_RESET"] = 0;
                tagDataProvider.Write2Level1(inDatas, 1);
            }
            timer1.Enabled = false;
        }
        public void SetReady()
        {
            try
            {
                List<string> lstAdress = new List<string>();
                if (areaBase.AreaNo.Contains("D172"))
                {
                    lstAdress.Add("D172WJ_LIGHT_CURTAIN_RESET");
                }
                else if (areaBase.AreaNo.Contains("D173"))
                {
                    lstAdress.Add("D173WJ_LIGHT_CURTAIN_RESET");
                }
                else if (areaBase.AreaNo.Contains("D174"))
                {
                    lstAdress.Add("D174WJ_LIGHT_CURTAIN_RESET");
                }
                arrTagAdress = lstAdress.ToArray<string>();
            }
            catch (Exception er)
            {

            }
        }
        private void refreshControl()
        {
            try
            {
                theAreaInfoInBay.getPortionAreaData(theBayNO);
                theAreaInfoInBay.getTagNameList();
                theAreaInfoInBay.getTagValues();
                foreach (AreaBase theAreaInfo in theAreaInfoInBay.DicSaddles.Values.ToArray())
                {
                    if (theAreaInfo.AreaNo.Contains("D172-WJ3"))
                    {
                        areaBase = theAreaInfo;
                    }
                }                           
            }
            catch (Exception ex)
            {
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            refreshControl();
            if (areaBase.AreaNo.Contains("Z21") || areaBase.AreaNo.Contains("Z22") || areaBase.AreaNo.Contains("Z08") || areaBase.AreaNo.Contains("Z07"))
            {
                stockSaddleModel.conInit(panel2, areaBase, constData.tagServiceName, panel2.Width, panel2.Height, constData.xAxisRight, constData.yBxisDown, 999);
            }
            else
            {
                stockSaddleModel.conInit(panel2, areaBase, constData.tagServiceName, panel2.Width, panel2.Height, constData.xAxisRight, constData.yAxisDown, 999);
            }
        }

        private void Frm_D172WJ3_TabActivated(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void Frm_D172WJ3_TabDeactivated(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }
        //画图对象
        //Graphics g;
        //private void GDIPant()
        //{
        //    g = panelSaddle.CreateGraphics();
        //    Pen p = new Pen(Color.Blue, 2);
        //    //画竖坐标线

        //    g.DrawLine(p, 40, 0, 40, panelSaddle.Height);

        //    g.DrawLine(p, panelSaddle.Width - 40, 0, panelSaddle.Width - 40, panelSaddle.Height);
        //    //画横坐标线
        //    g.DrawLine(p, 0, 30, panelSaddle.Width, 30);

        //    g.DrawLine(p, 0, panelSaddle.Height - 30, panelSaddle.Width, panelSaddle.Height - 30);

        //    Pen pen = new Pen(Color.FromArgb(255, 0, 0), 5);
        //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;//虚线的样式
        //    pen.DashPattern = new float[] { 2, 2 };//设置虚线中实点和空白区域之间的间隔
        //    g.DrawLine(pen, 0, 0, 0, 0);
        //}


    }
}
