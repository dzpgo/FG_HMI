using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSControls;
using UACSDAL;
using UACS;

namespace UACSView
{
    public partial class FrmZ21BayOffLinePacking : FormBase
    {
        public FrmZ21BayOffLinePacking()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint, true);
            this.Load += FrmZ21BayOffLinePacking_Load;
        }
        #region  ----------------------------连接数据库-------------------------------
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

        #region  ------------------------------TAG配置--------------------------------
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

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
        private string get_value(string tagName)
        {
            string theValue = string.Empty;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToString(valueObject);
            }
            catch
            {
                valueObject = null;
            }
            return theValue; ;
        }
        #endregion

        #region  -----------------------------方法字段--------------------------------
        private const string BayNo = "Z21";
        private const string AreaNo_A = "Z02-P1";
        private const string AreaNo_B = "Z02-P2";
        private const string SubAreaNo_A = "21";
        private const string SubAreaNo_B = "22";
        private const string D173UnitNo = "D173";
        private const string D108UnitNo = "D108";
        bool pressed = true;
        bool press = true;
        
        //private const string D212UnitNo = "D212";
        /// <summary>
        /// 鞍座控件
        /// </summary>
        private Dictionary<string, CoilPicture> dicSaddleControls = new Dictionary<string, CoilPicture>();
        private Dictionary<CoilPicture, conOffLinePackingSaddleInfo>
            dicControl = new Dictionary<CoilPicture, conOffLinePackingSaddleInfo>();
        private OffinePackingSaddleInBay offineSaddle = new OffinePackingSaddleInBay();
        private List<string> listUnit = new List<string>();
        private OffinePackingUrgentStop craneStop;
        private CraneStatusInBay craneStatusInBay;
        private List<conCraneStatus> lstConCraneStatusPanel = new List<conCraneStatus>();

        //设置离线包装区安全连锁显示
        private void SetOffLinePackingAreaColor()
        {
            List<string> lstAdress = new List<string>();
            lstAdress.Clear();
            lstAdress.Add("AREA_SAFE_D173_PR1");
            lstAdress.Add("AREA_SAFE_D173_PR2");
            arrTagAdress = lstAdress.ToArray<string>();
            readTags();
            if (get_value("AREA_SAFE_D173_PR1") == "1")
            {
                pl_5111.BackColor = Color.LightPink;
            }
            else
            {
                pl_5111.BackColor = Color.LightSteelBlue;
            }

            if (get_value("AREA_SAFE_D173_PR2") == "1")
            {
                pl_5112.BackColor = Color.LightPink;
            }
            else
            {
                pl_5112.BackColor = Color.LightSteelBlue;
            }
        }

        #endregion

        #region -----------------------------初始加载--------------------------------

        void FrmZ21BayOffLinePacking_Load(object sender, EventArgs e)
        {
            tagDataProvider.ServiceName = "iplature";
            dicSaddleControls["D173PR0003"] = Z21BayParkingArea_3;
            Z21BayParkingArea_3.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_3).Key;
            dicSaddleControls["D173PR0002"] = Z21BayParkingArea_2;
            Z21BayParkingArea_2.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_2).Key;
            dicSaddleControls["D173PR0001"] = Z21BayParkingArea_1;
            Z21BayParkingArea_1.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_1).Key;
            dicSaddleControls["D173PR0005"] = Z21BayParkingArea_5;
            Z21BayParkingArea_5.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_5).Key;
            dicSaddleControls["D173PR0004"] = Z21BayParkingArea_4;
            Z21BayParkingArea_4.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_4).Key;
            dicSaddleControls["D173PR0008"] = Z21BayParkingArea_8;
            Z21BayParkingArea_8.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_8).Key;
            dicSaddleControls["D173PR0007"] = Z21BayParkingArea_7;
            Z21BayParkingArea_7.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_7).Key;
            dicSaddleControls["D173PR0006"] = Z21BayParkingArea_6;
            Z21BayParkingArea_6.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_6).Key;
            dicSaddleControls["D173PR0010"] = Z21BayParkingArea_10;
            Z21BayParkingArea_10.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_10).Key;
            dicSaddleControls["D173PR0009"] = Z21BayParkingArea_9;
            Z21BayParkingArea_9.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z21BayParkingArea_9).Key;
           

            dicControl[Z21BayParkingArea_3] = conOffLinePackingSaddleInfo3;
            dicControl[Z21BayParkingArea_2] = conOffLinePackingSaddleInfo2;
            dicControl[Z21BayParkingArea_1] = conOffLinePackingSaddleInfo1;
            dicControl[Z21BayParkingArea_5] = conOffLinePackingSaddleInfo5;
            dicControl[Z21BayParkingArea_4] = conOffLinePackingSaddleInfo4;
            dicControl[Z21BayParkingArea_8] = conOffLinePackingSaddleInfo8;
            dicControl[Z21BayParkingArea_10] = conOffLinePackingSaddleInfo10;
            dicControl[Z21BayParkingArea_7] = conOffLinePackingSaddleInfo7;
            dicControl[Z21BayParkingArea_6] = conOffLinePackingSaddleInfo6;
            dicControl[Z21BayParkingArea_9] = conOffLinePackingSaddleInfo9;

            //-----------------------行车状态---------------------------------
            conCraneStatus6_1.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus6_1.CraneNO = "6_1";
            lstConCraneStatusPanel.Add(conCraneStatus6_1);
            conCraneStatus6_2.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus6_2.CraneNO = "6_2";
            lstConCraneStatusPanel.Add(conCraneStatus6_2);
            conCraneStatus6_3.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus6_3.CraneNO = "6_3";
            lstConCraneStatusPanel.Add(conCraneStatus6_3);

            //---------------------行车tag配置--------------------------------
            craneStatusInBay = new CraneStatusInBay();
            craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
            craneStatusInBay.AddCraneNO("6_1");
            craneStatusInBay.AddCraneNO("6_2");
            craneStatusInBay.AddCraneNO("6_3");
            craneStatusInBay.SetReady();

            this.dgvOffLineSaddleInfo.AutoGenerateColumns = false;     
           
            timer1.Enabled = true;
            timer1.Interval = 2500;
        } 
        #endregion

        #region  -----------------------------画面刷新--------------------------------

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabActived == false)
            {
                return; 
            }
            try
            {
                //string tmp = "Z02-P";                
                //for (int i = 0; i < 2; i++)
                //{
                //string s = string.Format("{0}{1}",tmp,i == 0? "1":"2");    
                    SetOffLinePackingAreaColor();
                    offineSaddle.ReadDefintion("Z21-PR");
                    foreach (string theSaddleName in dicSaddleControls.Keys)
                    {
                        if (offineSaddle.DicSaddles.ContainsKey(theSaddleName))
                        {
                            CoilPicture conSaddle = dicSaddleControls[theSaddleName];
                            // 获取钢卷号
                            OffinePackingSaddle theSaddleInfo = offineSaddle.DicSaddles[theSaddleName];

                            conOffLinePackingSaddleInfo coil = dicControl[conSaddle];

                            conOffLinePackingSaddleInfo.DelegatePackingSaddleInfo info = new
                                conOffLinePackingSaddleInfo.DelegatePackingSaddleInfo(coil.UpPackingSaddleInfo);
                            info(dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key);
                            if (theSaddleInfo.CONFIRM_FLAG == 10)
                            {
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key + "(吊入)";
                                conSaddle.CoilBackColor = Color.Cyan;
                                //coil.BackColor = Color.Cyan;
                                conSaddle.CoilId = theSaddleInfo.Coil;
                            }
                            else if (theSaddleInfo.CONFIRM_FLAG == 20)
                            {
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key + "(包装)";
                                conSaddle.CoilBackColor = Color.Pink;
                               // coil.BackColor = Color.Pink;
                            }
                            else if (theSaddleInfo.CONFIRM_FLAG == 30)
                            {
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key + "(吊离)";
                                conSaddle.CoilBackColor = Color.Yellow;
                                //coil.BackColor = Color.Yellow;
                            }
                            else if (theSaddleInfo.CONFIRM_FLAG == 40)
                            {
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key + "(待包卷吊入)";
                                conSaddle.CoilBackColor = Color.PaleGreen;
                                //coil.BackColor = Color.PaleGreen;
                            }
                            else
                            {
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key;
                                conSaddle.CoilBackColor = Color.SkyBlue;
                               // coil.BackColor = Color.SkyBlue;
                            }

                            if (checkBox_Show.Checked)
                            {
                                if (theSaddleInfo.SaddleStatus == 2 && theSaddleInfo.LockFlag == 0)                        //有卷可用
                                {
                                    if (theSaddleInfo.CoilWeight > 15000)
                                        conSaddle.CoilStatus = -2;
                                    else
                                        conSaddle.CoilStatus = 2;
                                    conSaddle.CoilId = theSaddleInfo.Coil;
                                }
                                else if (theSaddleInfo.SaddleStatus == 0 && theSaddleInfo.LockFlag == 0)                   //无卷可用
                                {
                                    //conSaddle.CoilId = "";
                                    conSaddle.CoilStatus = -10;
                                    conSaddle.CoilId = theSaddleInfo.Coil;
                                }
                                else                                                                                       //库位状态不明
                                {
                                    conSaddle.CoilStatus = 5;
                                    conSaddle.CoilId = theSaddleInfo.Coil;
                                }
                            }
                            else
                            {
                                if (theSaddleInfo.SaddleStatus == 2 && theSaddleInfo.LockFlag == 0)                        //有卷可用
                                {
                                    if (theSaddleInfo.CoilWeight > 15000)
                                        conSaddle.CoilStatus = -2;
                                    else
                                        conSaddle.CoilStatus = 2;
                                    conSaddle.CoilId = theSaddleInfo.Coil;
                                }
                                else                                                                                       //库位状态不明
                                {
                                    conSaddle.CoilId = "";
                                    conSaddle.CoilStatus = -10;
                                }
                            }
                        }
                    }
                                                                         
                if (pressed)
                {                   
                    offineSaddle.GetOffLinePackingByUnitSaddleInfo(D173UnitNo, BayNo, dataGridView1);
                    ChangeDGVColorByPackCode(dataGridView1);

                    offineSaddle.GetOffLinePackingByUnitSaddleInfo(D108UnitNo, BayNo, dataGridView2);
                    ChangeDGVColorByPackCode(dataGridView2);
                }

                if(press)
                {
                    offineSaddle.GetOffLinePackingByZ34034Info(dgvOffLineSaddleInfo, "D173PR");
                    ChangeDGVColorByPackCode(dgvOffLineSaddleInfo);
                }

                craneStatusInBay.getAllPLCStatusInBay(craneStatusInBay.lstCraneNO);
                foreach (conCraneStatus conCraneStatusPanel in lstConCraneStatusPanel)
                {
                    conCraneStatus.RefreshControlInvoke ConCraneStatusPanel_Invoke = new conCraneStatus.RefreshControlInvoke(conCraneStatusPanel.RefreshControl);
                    conCraneStatusPanel.BeginInvoke(ConCraneStatusPanel_Invoke, new Object[] { craneStatusInBay.DicCranePLCStatusBase[conCraneStatusPanel.CraneNO].Clone() });
                }

                GC.Collect();
            }
            catch (Exception er)
            {

                throw;
            }
        }

        private void ChangeDGVColorByPackCode(params DataGridView[] _dgv)
        {
            for (int i = 0; i < _dgv.Length; i++)
            {
                for (int j = 0; j < _dgv[i].RowCount; j++)
                {
                    if (_dgv[i].Rows[j].Cells[8].Value != DBNull.Value)
                    {
                        string packCode = _dgv[i].Rows[j].Cells[8].Value.ToString();
                        if (!packCode.Contains("1"))
                        {
                            _dgv[i].Rows[j].DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                    }
                }
            }

        }
        #endregion

        #region -----------------------------画面切换--------------------------------
        private bool tabActived = true;
        void MyTabActivated(object sender, EventArgs e)
        {
            tabActived = true;
        }
        void MyTabDeactivated(object sender, EventArgs e)
        {
            tabActived = false;
        }
        #endregion
      
        private void btnCraneStop_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要紧停离线包装上方的自动行车吗？", "操作提示", btn);
            if (dr == DialogResult.OK)
            {
                craneStop.isUrgentStop();
            }
        }

        
        private void dgvD401UnitSaddleInfo_Scroll(object sender, ScrollEventArgs e)
        {
            pressed = false;
            
        }

        private void dgvD401UnitSaddleInfo_MouseLeave(object sender, EventArgs e)
        {
            pressed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!(MessageBox.Show("确定要吊离打包1区所有卷吗？","吊离提示",MessageBoxButtons.OKCancel)==DialogResult.OK))
            {
                return;
            }
            try
            {
                string sql1 = @"UPDATE UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30 WHERE STOCK_NO IN (SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO != 'NULL' AND STOCK_STATUS = 2 AND LOCK_FLAG = 0 AND STOCK_NO IN (SELECT STOCK_NO FROM UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE SUB_AREA_ID = 'D173-PR1'))";
                DBHelper.ExecuteNonQuery(sql1);
                MessageBox.Show("已执行打包1区所有卷吊离！");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(MessageBox.Show("确定要吊离打包2区所有卷吗？", "吊离提示", MessageBoxButtons.OKCancel) == DialogResult.OK))
            {
                return;
            }
            try
            {
                string sql2 = @"UPDATE UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30 WHERE STOCK_NO IN (SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO != 'NULL' AND STOCK_STATUS = 2 AND LOCK_FLAG = 0 AND STOCK_NO IN (SELECT STOCK_NO FROM UACS_OFFLINE_PACKING_AREA_STOCK_DEFINE WHERE SUB_AREA_ID = 'D173-PR2'))";
                DBHelper.ExecuteNonQuery(sql2);
                MessageBox.Show("已执行打包2区所有卷吊离！");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void dgvOffLineSaddleInfo_MouseLeave(object sender, EventArgs e)
        {
            press = true;
        }

        private void dgvOffLineSaddleInfo_Scroll(object sender, ScrollEventArgs e)
        {
            press = false;
        }
    }
}
