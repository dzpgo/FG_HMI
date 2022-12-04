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
using ParkClassLibrary;

namespace UACSView
{
    public partial class FrmWaterTank : FormBase
    {
        public FrmWaterTank()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint, true);
            this.Load += FrmWaterTank_Load;
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

        void FrmWaterTank_Load(object sender, EventArgs e)
        {

            dicSaddleControls["Z0115581"] = coilPicture1;
            coilPicture1.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture1).Key;
            dicSaddleControls["Z0115591"] = coilPicture2;
            coilPicture2.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture2).Key;
            dicSaddleControls["Z0115601"] = coilPicture3;
            coilPicture3.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture3).Key;
            dicSaddleControls["Z0115611"] = coilPicture4;
            coilPicture4.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture4).Key;
            dicSaddleControls["Z0115621"] = coilPicture5;
            coilPicture5.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture5).Key;
            dicSaddleControls["Z0115631"] = coilPicture6;
            coilPicture6.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture6).Key;
            dicSaddleControls["Z0115641"] = coilPicture7;
            coilPicture7.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture7).Key;
            dicSaddleControls["Z0115651"] = coilPicture8;
            coilPicture8.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture8).Key;
            dicSaddleControls["Z0115661"] = coilPicture9;
            coilPicture9.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture9).Key;
            dicSaddleControls["Z0115671"] = coilPicture10;
            coilPicture10.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture10).Key;
            dicSaddleControls["Z0115681"] = coilPicture11;
            coilPicture11.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture11).Key;
            dicSaddleControls["Z0115691"] = coilPicture12;
            coilPicture12.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture12).Key;
            dicSaddleControls["Z0115701"] = coilPicture13;
            coilPicture13.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture13).Key;
            dicSaddleControls["Z0115711"] = coilPicture14;
            coilPicture14.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture14).Key;
            dicSaddleControls["Z0115721"] = coilPicture15;
            coilPicture15.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == coilPicture15).Key;

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
                    offineSaddle.ReadDefintion("Z01-WT");
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
                                conSaddle.PosName = dicSaddleControls.FirstOrDefault(p => p.Value == conSaddle).Key + "(冷却中)";
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
                    offineSaddle.GetOffLinePackingByZ34034Info(dgvOffLineSaddleInfo, "Z01-6-15");
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

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawString("Z01-1区", new Font("微软雅黑", 15, FontStyle.Bold), Brushes.Black, new Point(0,panel14.Height / 2 - 30), new StringFormat(StringFormatFlags.DirectionVertical));
        }

        private void btn_1WaterTank_Out_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("请确认水槽是否有水，是否能正常吊离？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (!(ret == DialogResult.OK))
            {
                return;
            }
            if (!(MessageBox.Show("确定要吊离1#水槽所有卷吗？", "吊离提示", MessageBoxButtons.OKCancel) == DialogResult.OK))
            {
                return;
            }
            try
            {
                string sql1 = @"UPDATE UACS_WATER_TANK_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30 WHERE STOCK_NO IN (SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO != 'NULL' AND STOCK_STATUS = 2 AND LOCK_FLAG = 0 AND (STOCK_NO = 'Z0115581' OR STOCK_NO = 'Z0115591' OR STOCK_NO = 'Z0115601' OR STOCK_NO = 'Z0115611' OR STOCK_NO = 'Z0115621'))";
                DBHelper.ExecuteNonQuery(sql1);
                MessageBox.Show("已执行1#水槽所有卷吊离！");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void btn_2WaterTank_Out_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("请确认水槽是否有水，是否能正常吊离？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (!(ret == DialogResult.OK))
            {
                return;
            }
            if (!(MessageBox.Show("确定要吊离2#水槽所有卷吗？", "吊离提示", MessageBoxButtons.OKCancel) == DialogResult.OK))
            {
                return;
            }
            try
            {
                string sql1 = @"UPDATE UACS_WATER_TANK_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30 WHERE STOCK_NO IN (SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO != 'NULL' AND STOCK_STATUS = 2 AND LOCK_FLAG = 0 AND (STOCK_NO = 'Z0115631' OR STOCK_NO = 'Z0115641' OR STOCK_NO = 'Z0115651' OR STOCK_NO = 'Z0115661' OR STOCK_NO = 'Z0115671'))";
                DBHelper.ExecuteNonQuery(sql1);
                MessageBox.Show("已执行2#水槽所有卷吊离！");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void btn_3WaterTank_Out_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("请确认水槽是否有水，是否能正常吊离？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (!(ret == DialogResult.OK))
            {
                return;
            }
            if (!(MessageBox.Show("确定要吊离3#水槽所有卷吗？", "吊离提示", MessageBoxButtons.OKCancel) == DialogResult.OK))
            {
                return;
            }
            try
            {
                string sql1 = @"UPDATE UACS_WATER_TANK_AREA_STOCK_DEFINE SET CONFIRM_FLAG = 30 WHERE STOCK_NO IN (SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO != 'NULL' AND STOCK_STATUS = 2 AND LOCK_FLAG = 0 AND (STOCK_NO = 'Z0115681' OR STOCK_NO = 'Z0115691' OR STOCK_NO = 'Z0115701' OR STOCK_NO = 'Z0115711' OR STOCK_NO = 'Z0115721'))";
                DBHelper.ExecuteNonQuery(sql1);
                MessageBox.Show("已执行3#水槽所有卷吊离！");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private List<string> listCoil = new List<string>();
        private List<string> listWaterTankSaddle = new List<string>();
        private List<string> listSaddleNo = new List<string>();

        private void  OneKeyHoistIn(string order,string planNo_Array, List<string> listSaddle)
        {
            listCoil.Clear();
            listWaterTankSaddle.Clear();
            int i = 0;
            string[] planNoArray = planNo_Array.Trim().Split('+');
            try
            {
                //根据计划号遍历计划库位钢卷排序，获取计划钢卷集合
                string sqlPlan = @"SELECT A.PLAN_NO,C.MAT_NO,E.X_CENTER FROM UACS_PLAN_L3PRODPLAN A ";
                sqlPlan += "LEFT JOIN UACS_YARDMAP_COIL B ON A.COIL_NO = B.COIL_NO ";
                sqlPlan += "LEFT JOIN UACS_YARDMAP_STOCK_DEFINE C ON B.COIL_NO = C.MAT_NO ";
                sqlPlan += "LEFT JOIN UACS_YARDMAP_SADDLE_STOCK D ON C.STOCK_NO = D.STOCK_NO ";
                sqlPlan += "LEFT JOIN UACS_YARDMAP_SADDLE_DEFINE E ON D.SADDLE_NO = E.SADDLE_NO ";
                sqlPlan += "WHERE C.MAT_NO != 'NULL' ";
                sqlPlan += "AND C.STOCK_NO != 'Z0115581' AND  C.STOCK_NO != 'Z0115591' AND  C.STOCK_NO != 'Z0115601' AND  C.STOCK_NO != 'Z0115611' AND  C.STOCK_NO != 'Z0115621' ";
                sqlPlan += "AND C.STOCK_NO != 'Z0115631' AND  C.STOCK_NO != 'Z0115641' AND  C.STOCK_NO != 'Z0115651' AND  C.STOCK_NO != 'Z0115661' AND  C.STOCK_NO != 'Z0115671' ";
                sqlPlan += "AND C.STOCK_NO != 'Z0115681' AND  C.STOCK_NO != 'Z0115691' AND  C.STOCK_NO != 'Z0115701' AND  C.STOCK_NO != 'Z0115711' AND  C.STOCK_NO != 'Z0115721' ";
                sqlPlan += "AND C.MAT_NO NOT IN(SELECT COIL_NO FROM UACS_WATER_TANK_AREA_STOCK_DEFINE WHERE AREA_ID = 'Z01-WT' AND COIL_NO != 'NULL') ";
                sqlPlan += "AND (A.PLAN_NO = 'NULL' ";
                if (planNoArray.Length > 0)
                {
                    foreach (string planNo in planNoArray)
                    {
                        if(planNo.Trim() != "")
                        {
                            sqlPlan += " OR A.PLAN_NO = '" + planNo.Trim() + "'";
                        }
                    }
                }
                if (order == "DESC")
                {
                    sqlPlan += " ) ORDER BY E.X_CENTER DESC";
                }
                else
                {
                    sqlPlan += " ) ORDER BY E.X_CENTER ASC";
                }

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlPlan))
                {
                    while (rdr.Read())
                    {
                        if(rdr["MAT_NO"] != DBNull.Value)
                        {
                            listCoil.Add(rdr["MAT_NO"].ToString().Trim());
                        }
                    }
                }

                //获取水槽空鞍座集合
                string sqlSaddle = @"SELECT A.STOCK_NO,A.STOCK_STATUS,A.LOCK_FLAG,B.CONFIRM_FLAG,B.AREA_ID FROM UACS_WATER_TANK_AREA_STOCK_DEFINE B LEFT JOIN UACS_YARDMAP_STOCK_DEFINE A  ON B.STOCK_NO = A.STOCK_NO ";
                sqlSaddle += "WHERE A.STOCK_NO = 'NULL' ";
                foreach (string saddleNo in listSaddle)
                {
                    if(saddleNo.Trim()!="")
                    {
                        sqlSaddle += " OR A.STOCK_NO = '" + saddleNo.Trim() + "'";
                    }
                }

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlSaddle))
                {
                    while (rdr.Read())
                    {
                        if (Convert.ToInt32(rdr["STOCK_STATUS"].ToString()) == 0 && Convert.ToInt32(rdr["LOCK_FLAG"].ToString()) == 0 && Convert.ToInt32(rdr["CONFIRM_FLAG"].ToString()) == 0)
                        {
                            listWaterTankSaddle.Add(rdr["STOCK_NO"].ToString());
                        }
                    }
                }

                //根据空鞍座吊入计划顺序钢卷
                if(listWaterTankSaddle.Count > listCoil.Count)
                {
                    foreach (string coilNo in listCoil)
                    {
                        string waterTankSaddle = listWaterTankSaddle[i];
                        string sql = @"update UACS_WATER_TANK_AREA_STOCK_DEFINE  set COIL_NO = '" + coilNo + "',CONFIRM_FLAG = '10'";
                        sql += " WHERE STOCK_NO = '" + waterTankSaddle + "'";
                        DBHelper.ExecuteNonQuery(sql);
                        i++;
                    }
                }
                else
                {
                    foreach (string waterTankSaddle in listWaterTankSaddle)
                    {
                        string coilNo = listCoil[i];
                        string sql = @"update UACS_WATER_TANK_AREA_STOCK_DEFINE  set COIL_NO = '" + coilNo + "',CONFIRM_FLAG = '10'";
                        sql += " WHERE STOCK_NO = '" + waterTankSaddle + "'";
                        DBHelper.ExecuteNonQuery(sql);
                        i++;
                    }
                }

                //判断成功吊入计划号钢卷数量
                if(i > 0)
                {
                    MessageBox.Show("成功吊入" + i.ToString() + "个计划钢卷!");
                }
                else
                {
                    MessageBox.Show("查找不到计划钢卷或查找不到无作业状态水槽鞍座!");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }
        private void btn_1WaterTank_Into_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("请确认水槽是否有水，是否能正常吊入？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (!(ret == DialogResult.OK))
            {
                return;
            }

            listSaddleNo.Clear();
            listSaddleNo.Add("Z0115581");
            listSaddleNo.Add("Z0115591");
            listSaddleNo.Add("Z0115601");
            listSaddleNo.Add("Z0115611");
            listSaddleNo.Add("Z0115621");
            OneKeyHoistIn("ASC", lab_PlanNoList.Text.Trim(), listSaddleNo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("请确认水槽是否有水，是否能正常吊入？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (!(ret == DialogResult.OK))
            {
                return;
            }
            listSaddleNo.Clear();
            listSaddleNo.Add("Z0115631");
            listSaddleNo.Add("Z0115641");
            listSaddleNo.Add("Z0115651");
            listSaddleNo.Add("Z0115661");
            listSaddleNo.Add("Z0115671");
            OneKeyHoistIn("ASC", lab_PlanNoList.Text.Trim(), listSaddleNo);
        }

        private void btn_3WaterTank_Into_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("请确认水槽是否有水，是否能正常吊入？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (!(ret == DialogResult.OK))
            {
                return;
            }
            listSaddleNo.Clear();
            listSaddleNo.Add("Z0115681");
            listSaddleNo.Add("Z0115691");
            listSaddleNo.Add("Z0115701");
            listSaddleNo.Add("Z0115711");
            listSaddleNo.Add("Z0115721");
            OneKeyHoistIn("DESC", lab_PlanNoList.Text.Trim(), listSaddleNo);
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if(txt_PlanNoList.Text.Trim() !="" )
            {
                lab_PlanNoList.Text = txt_PlanNoList.Text.Trim();
            }
            else
            {
                MessageBox.Show("输入计划号不能位空！");
            }
        }
    }
}
