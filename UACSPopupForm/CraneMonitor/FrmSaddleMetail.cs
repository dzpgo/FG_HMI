using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using ParkClassLibrary;

namespace UACSPopupForm
{
    public partial class FrmSaddleMetail : Form
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;

        private SaddleBase saddleInfo = new SaddleBase();

        public SaddleBase SaddleInfo
        {
            get { return saddleInfo; }
            set { saddleInfo = value; }
        }

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

        private const string strPassWord = "123123";

        //称重卷变量
        private string WEIGH_MAT_NO = String.Empty;
        private string WEIGH_YARD_STOCK_NO = String.Empty;

        public FrmSaddleMetail()
        {
            InitializeComponent();
            this.Load += FrmSaddleMetail_Load;
        }

        void FrmSaddleMetail_Load(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            this.Deactivate += new EventHandler(frmSaddleDetail_Deactivate);
            txtSaddleNo.Text = saddleInfo.SaddleNo;
            txtSaddleName.Text = saddleInfo.SaddleName;
            txtCoilNo.Text = saddleInfo.Mat_No;
            txtXCenter.Text = saddleInfo.X_Center.ToString();
            txtYCenter.Text = saddleInfo.Y_Center.ToString();
            txtZCenter.Text = saddleInfo.Z_Center.ToString();
            label6.Text = saddleInfo.Row_No.ToString() + "-" + saddleInfo.Col_No.ToString();
            if(saddleInfo.Product_Time != null)
            {
                txtProductTime.Text = saddleInfo.Product_Time.ToString();
            }
            else
            {
                txtProductTime.Text = "无";
            }

            #region 转换状态（垃圾）
            switch (saddleInfo.Stock_Status)
            {
                case 0:
                    txtStatus.Text = "无卷";
                    break;
                case 1:
                    txtStatus.Text = "预定";
                    break;
                case 2:
                    txtStatus.Text = "占用";
                    break;
                default:
                    txtStatus.Text = "无";
                    break;
            }
            switch (saddleInfo.Lock_Flag)
            {
                case 0:
                    txtflag.Text = "可用";
                    break;
                case 1:
                    txtflag.Text = "待判";
                    break;
                case 2:
                    txtflag.Text = "封锁";
                    break;
                default:
                    txtflag.Text = "无";
                    break;
            }
            #endregion

            AuthorityManagement authority = new AuthorityManagement();
            if (authority.isUserJudgeEqual("D308", "D202", "scal", "D212"))
            {
                btnCoilMessage.Visible = false;
                txtMatNo.Visible = false;
                btnUpStockByCoil.Visible = false;
                btnByNoCoil.Visible = false;
                btnByReserve.Visible = false;
                btnByOccupy.Visible = false;
                btnNoCoilByUsable.Visible = false;
                btnByUsable.Visible = false;
                btnByStay.Visible = false;
                btnByBlock.Visible = false;
                label7.Visible = false;
                txtPassWord.Visible = false;
                txtPopupMessage.Visible = false;
            }
            getUserName();
            getUserName2();
            //btn_Weighing_Show();
            if (txtSaddleNo.Text.Contains("WJ"))
            {
                btnNoCoilByUsable.Visible = true;
            }
        }

        void frmSaddleDetail_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 更新钢卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpStockByCoil_Click(object sender, EventArgs e)
        {

            try
            {
                //if (txtPassWord.Text == strPassWord)
                //{
                    if(saddleInfo.SaddleName.ToString().Contains("Z01"))
                    {
                        if (txtMatNo.Text.Length == 12)
                        {
                            string initialCoilNo = txtCoilNo.Text.Trim();
                            string coilNo = txtMatNo.Text.Trim();
                            string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = '" + coilNo + "',STOCK_STATUS = 2,LOCK_FLAG = 0,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                            DBHelper.ExecuteNonQuery(sql);
                            txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "已添加钢卷" + coilNo;
                            ParkClassLibrary.HMILogger.WriteLog("设置钢卷号", "库位号：" + saddleInfo.SaddleNo + "，初始钢卷：" + initialCoilNo + "，修改后钢卷：" + coilNo, ParkClassLibrary.LogLevel.Info, this.Text);
                        }
                        else
                        {
                            txtPopupMessage.Text = "输入卷号长度不符！！！";
                        }
                    }
                    else
                    {
                        if (txtMatNo.Text.Length < 12)
                        {
                            string initialCoilNo = txtCoilNo.Text.Trim();
                            string coilNo = txtMatNo.Text.Trim();
                            string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = '" + coilNo + "',STOCK_STATUS = 2,LOCK_FLAG = 0,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                            DBHelper.ExecuteNonQuery(sql);
                            txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "已添加钢卷" + coilNo;
                            ParkClassLibrary.HMILogger.WriteLog("设置钢卷号", "库位号：" + saddleInfo.SaddleNo + "，初始钢卷：" + initialCoilNo + "，修改后钢卷：" + coilNo, ParkClassLibrary.LogLevel.Info, this.Text);
                        }
                        else
                        {
                            txtPopupMessage.Text = "输入卷号长度不符！！！";
                        }
                    }               
                //}
                //else
                //    txtPopupMessage.Text = "输入密码错误！！！";
            }
            catch (Exception er)
            {
                txtPopupMessage.Text = er.Message;
            }
        }
        /// <summary>
        /// 库位无卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnByNoCoil_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassWord.Text == strPassWord)
                {
                    string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = NULL,STOCK_STATUS = 0,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                    DBHelper.ExecuteNonQuery(sql);
                    txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "状态已无卷";
                    ParkClassLibrary.HMILogger.WriteLog("库位无卷", "库位无卷：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                    txtPopupMessage.Text = "输入密码错误！！！";
            }
            catch (Exception er)
            {
                txtPopupMessage.Text = er.Message;
            }
        }
        /// <summary>
        /// 库位预定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnByReserve_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassWord.Text == strPassWord)
                {
                    string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 1,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                    DBHelper.ExecuteNonQuery(sql);
                    txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "状态已预定";
                    ParkClassLibrary.HMILogger.WriteLog("库位预定", "库位预定：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                    txtPopupMessage.Text = "输入密码错误！！！";
            }
            catch (Exception er)
            {

                txtPopupMessage.Text = er.Message;
            }
        }

        /// <summary>
        /// 库位占用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnByOccupy_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtPassWord.Text == strPassWord)
                {
                    string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 2,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                    DBHelper.ExecuteNonQuery(sql);
                    txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "状态已占用";
                    ParkClassLibrary.HMILogger.WriteLog("库位占用", "库位占用：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                    txtPopupMessage.Text = "输入密码错误！！！";

            }
            catch (Exception er)
            {

                txtPopupMessage.Text = er.Message;
            }
        }

        /// <summary>
        /// 库位可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnByUsable_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassWord.Text == strPassWord)
                {
                    string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET LOCK_FLAG = 0,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                    DBHelper.ExecuteNonQuery(sql);
                    txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "标记已可用";
                    ParkClassLibrary.HMILogger.WriteLog("库位可用", "投用库位：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                    txtPopupMessage.Text = "输入密码错误！！！";
            }
            catch (Exception er)
            {

                txtPopupMessage.Text = er.Message;
            }
        }

        /// <summary>
        /// 库位待判
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnByStay_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET LOCK_FLAG = 1,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                DBHelper.ExecuteNonQuery(sql);
                txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "标记已待判";
                ParkClassLibrary.HMILogger.WriteLog("库位待判", "库位待判：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                //if (txtPassWord.Text == strPassWord)
                //{
                //    string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET LOCK_FLAG = 1,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                //    DBHelper.ExecuteNonQuery(sql);
                //    txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "标记已待判";
                //    ParkClassLibrary.HMILogger.WriteLog("库位待判", "库位待判：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                //}
                //else
                //    txtPopupMessage.Text = "输入密码错误！！！";

            }
            catch (Exception er)
            {

                txtPopupMessage.Text = er.Message;
            }
        }
        /// <summary>
        /// 库位封锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnByBlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassWord.Text == strPassWord)
                {
                    string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET LOCK_FLAG = 2,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                    DBHelper.ExecuteNonQuery(sql);
                    txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "标记已封锁";
                    ParkClassLibrary.HMILogger.WriteLog("库位封锁", "封锁库位：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                    txtPopupMessage.Text = "输入密码错误！！！";

            }
            catch (Exception er)
            {
                txtPopupMessage.Text = er.Message;
            }
        }

        private void btnNoCoilByUsable_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassWord.Text == strPassWord)
                {
                    string coilNo = txtMatNo.Text.Trim();
                    string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = NULL,STOCK_STATUS = 0,LOCK_FLAG = 0,EVENT_ID= 888888 WHERE STOCK_NO = '" + saddleInfo.SaddleNo + "' ";
                    DBHelper.ExecuteNonQuery(sql);
                    txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "已无卷可用";
                    ParkClassLibrary.HMILogger.WriteLog("库位无卷可用", "库位无卷可用：" + saddleInfo.SaddleNo, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                else
                    txtPopupMessage.Text = "输入密码错误！！！";


            }
            catch (Exception er)
            {
                txtPopupMessage.Text = er.Message;
            }

        }

        private string strCoil = string.Empty;
        private void btnCoilMessage_Click(object sender, EventArgs e)
        {


            strCoil = txtCoilNo.Text.Trim().ToString();

            if (auth.IsOpen("02 钢卷信息"))
            {
                auth.CloseForm("02 钢卷信息");

                if (strCoil.Count() > 0)
                {
                    auth.OpenForm("02 钢卷信息", strCoil);             
                }
                else
                    auth.OpenForm("02 钢卷信息");
            }
            else
            {
                if (strCoil.Count() > 0)
                {
                    auth.OpenForm("02 钢卷信息", strCoil);
                }
                else
                    auth.OpenForm("02 钢卷信息");
            }
            this.Close();
        }

        private void btnCoilPlastic_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                if (txtCoilNo.Text.Trim() != "")
                {

                    string coilNo = txtCoilNo.Text.Trim();
                    string sql1 = "select * from UACS_YARDMAP_COIL_PLASTIC where COIL_NO = '" + coilNo + "'";

                    using (IDataReader rdr = DBHelper.ExecuteReader(sql1))
                    {
                        while (rdr.Read())
                        {
                            flag = true;
                            string sql2 = @"UPDATE UACS_YARDMAP_COIL_PLASTIC SET PLASTIC_FLAG = 1,PLASTIC_TIME = current timestamp  WHERE COIL_NO = '" + coilNo + "' ";
                            DBHelper.ExecuteNonQuery(sql2);
                            txtPopupMessage.Text = "已将钢卷:" + coilNo + "改成套袋状态";
                        }
                    }
                    if (!flag)
                    {
                        string sql2 = @"insert into UACS_YARDMAP_COIL_PLASTIC(COIL_NO,PLASTIC_FLAG,PLASTIC_TIME)  values ( '" + coilNo + "',1,current timestamp )";
                        DBHelper.ExecuteNonQuery(sql2);
                        txtPopupMessage.Text = "已将钢卷:" + coilNo + "改成套袋状态";
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnNoCoilPlastic_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCoilNo.Text.Trim() != "")
                {

                    string coilNo = txtCoilNo.Text.Trim();
                    string sql1 = "select * from UACS_YARDMAP_COIL_PLASTIC where COIL_NO = '" + coilNo + "'";

                    using (IDataReader rdr = DBHelper.ExecuteReader(sql1))
                    {
                        while (rdr.Read())
                        {
                            string sql2 = @"UPDATE UACS_YARDMAP_COIL_PLASTIC SET PLASTIC_FLAG = 0,PLASTIC_TIME = current timestamp  WHERE COIL_NO = '" + coilNo + "' ";
                            DBHelper.ExecuteNonQuery(sql2);
                            txtPopupMessage.Text = "已将钢卷:" + coilNo + "改成无套袋状态";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //用户功能限制
        private void getUserName()
        {
            if (!auth.GetUserName().Contains("admin") && !auth.GetUserName().Contains("CLTS"))
            {
                btnUpStockByCoil.Visible = false;
            }
        }
        private void getUserName2()
        {
            if (!auth.GetUserName().Contains("admin")&& !auth.GetUserName().Contains("CLTS"))
            {
                btnByUsable.Visible = false;
            }
        }
        private void btn_Weighing_Show()
        {
            try
            {
                if (auth.GetUserName().Trim().Contains("admin") || auth.GetUserName().Trim().Contains("ZYBG"))
                {
                    string sql = @"SELECT WEIGH_MATNO,WEIGH_YARD_STOCKNO,WEIGH_UNIT_STOCKNO,EXE_CRANES FROM UNIT_WEIGHING_CONFIGURATION WHERE 1 = 1";
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        while (rdr.Read())
                        {
                            string COIL_NO = rdr["WEIGH_MATNO"].ToString().Trim();
                            string STOCK_NO = rdr["WEIGH_YARD_STOCKNO"].ToString().Trim();
                            if (txtSaddleNo.Text.Trim() == STOCK_NO)
                            {
                                WEIGH_MAT_NO = COIL_NO;
                                WEIGH_YARD_STOCK_NO = STOCK_NO;
                                btn_Weighing.Enabled = true;
                                btn_Weighing.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }          
        }

        private void btn_Weighing_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"UPDATE UACS_YARDMAP_STOCK_DEFINE SET MAT_NO = '" + WEIGH_MAT_NO + "',STOCK_STATUS = 2,LOCK_FLAG = 0,EVENT_ID= 888888 WHERE STOCK_NO = '" + WEIGH_YARD_STOCK_NO + "' ";
                DBHelper.ExecuteNonQuery(sql);
                txtPopupMessage.Text = "库位" + saddleInfo.SaddleNo + "钢卷称重";
                HMILogger.WriteLog("钢卷称重", "钢卷称重：" + saddleInfo.SaddleNo + "钢卷号："+ WEIGH_MAT_NO, LogLevel.Info, this.Text);
            }
            catch (Exception er)
            {
                txtPopupMessage.Text = er.Message;
            }
        }
    }
}
