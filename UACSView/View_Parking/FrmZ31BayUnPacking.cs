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
    public partial class FrmZ01BayUnPacking : FormBase
    {
        public FrmZ01BayUnPacking()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint, true);
            this.Load += FrmZ01BayUnPacking_Load;
        }
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

        #region ------------------------------TAG配置----------------------------------
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

        #region  -----------------------------方法字段--------------------------------
        private const string BayNo = "Z21";
        private const string AreaNo_A = "Z02-P1";
        private const string AreaNo_B = "Z02-P2";
        private const string SubAreaNo_A = "21";
        private const string SubAreaNo_B = "22";
        private const string D171UnitNo = "D171";
        private const string D172UnitNo = "D172";
        bool pressed = true;
        bool press = true;
        private UnitEntrySaddleInfo entrySaddleInfo = new UnitEntrySaddleInfo();
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

        #endregion

        #region -----------------------------初始加载--------------------------------

        void FrmZ01BayUnPacking_Load(object sender, EventArgs e)
        {

            dicSaddleControls["Z0115401"] = Z01BayUnParkingArea_1;
            Z01BayUnParkingArea_1.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_1).Key;
            dicSaddleControls["Z0115411"] = Z01BayUnParkingArea_2;
            Z01BayUnParkingArea_2.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_2).Key;
            dicSaddleControls["Z0115421"] = Z01BayUnParkingArea_3;
            Z01BayUnParkingArea_3.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_3).Key;
            dicSaddleControls["Z0115431"] = Z01BayUnParkingArea_4;
            Z01BayUnParkingArea_4.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_4).Key;
            dicSaddleControls["Z0115441"] = Z01BayUnParkingArea_5;
            Z01BayUnParkingArea_5.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_5).Key;
            dicSaddleControls["Z0115451"] = Z01BayUnParkingArea_6;
            Z01BayUnParkingArea_6.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_6).Key;
            dicSaddleControls["Z0115461"] = Z01BayUnParkingArea_7;
            Z01BayUnParkingArea_7.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_7).Key;
            dicSaddleControls["Z0115471"] = Z01BayUnParkingArea_8;
            Z01BayUnParkingArea_8.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_8).Key;
            dicSaddleControls["Z0115481"] = Z01BayUnParkingArea_9;
            Z01BayUnParkingArea_9.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_9).Key;
            dicSaddleControls["Z0115491"] = Z01BayUnParkingArea_10;
            Z01BayUnParkingArea_10.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == Z01BayUnParkingArea_10).Key;




            tagDataProvider.ServiceName = "iplature";
            //SetReady();
            //this.dgvOffLineSaddleInfo.AutoGenerateColumns = false;     

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

                    readTags();
                    if (get_value_x("AREA_SAFE_XFU") == 1)
                    {
                        tableLayoutPanel2.BackColor = Color.LightPink;
                    }
                    else
                    {
                        tableLayoutPanel2.BackColor = Color.LightSteelBlue;
                    }
                    offineSaddle.ReadDefintion("Z01-PC");
                    foreach (string theSaddleName in dicSaddleControls.Keys)
                    {
                        if (offineSaddle.DicSaddles.ContainsKey(theSaddleName))
                        {
                            CoilPicture conSaddle = dicSaddleControls[theSaddleName];
                            // 获取钢卷号
                            OffinePackingSaddle theSaddleInfo = offineSaddle.DicSaddles[theSaddleName];

                            //conOffLinePackingSaddleInfo coil = dicControl[conSaddle];

                            //conOffLinePackingSaddleInfo.DelegatePackingSaddleInfo info = new
                            //    conOffLinePackingSaddleInfo.DelegatePackingSaddleInfo(coil.UpPackingSaddleInfo);
                            //info(dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key);
                            if (theSaddleInfo.CONFIRM_FLAG == 10)
                            {
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key + "(吊入)";
                                conSaddle.CoilBackColor = Color.Cyan;
                                //coil.BackColor = Color.Cyan;
                                //conSaddle.CoilId = theSaddleInfo.Coil;
                            }
                            else if (theSaddleInfo.CONFIRM_FLAG == 20)
                            {
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key + "(修复中)";
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

                            //if (checkBox_Show.Checked)
                            //{
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
                        
                        //else
                        //{
                        //    if (theSaddleInfo.SaddleStatus == 2 && theSaddleInfo.LockFlag == 0)                        //有卷可用
                        //    {
                        //        if (theSaddleInfo.CoilWeight > 15000)
                        //            conSaddle.CoilStatus = -2;
                        //        else
                        //            conSaddle.CoilStatus = 2;
                        //        conSaddle.CoilId = theSaddleInfo.Coil;
                        //    }
                        //    else                                                                                       //库位状态不明
                        //    {
                        //        conSaddle.CoilId = "";
                        //        conSaddle.CoilStatus = -10;
                        //    }
                            //}
                        }
                    }
                                                                                        
                if(press)
                {
                    offineSaddle.GetOffLinePackingByZ34034Info(dgvOffLineSaddleInfo, "Z01154");
                    //ChangeDGVColorByPackCode(dgvOffLineSaddleInfo);
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
               
                
        private void dgvOffLineSaddleInfo_Scroll(object sender, ScrollEventArgs e)
        {
            press = false;
        }

        private void dgvOffLineSaddleInfo_MouseLeave(object sender, EventArgs e)
        {
            press = true;
        }
        

        private void timer2_Tick(object sender, EventArgs e)
        {
            inDatas["D173PC_LIGHT_CURTAIN_RESET"] = 0;
            tagDataProvider.Write2Level1(inDatas, 1);
            timer1.Enabled = false;
        }

        public void SetReady()
        {
            try
            {
                List<string> lstAdress = new List<string>();
                lstAdress.Add("D173PC_LIGHT_CURTAIN_RESET");              
                arrTagAdress = lstAdress.ToArray<string>();
            }
            catch (Exception er)
            {

            }
        }
      

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawString("Z01-8区", new Font("微软雅黑", 15, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 0));
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawString("2-1门", new Font("微软雅黑", 15, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 0));
        }
    }
}
