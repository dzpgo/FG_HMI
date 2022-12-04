using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using UACSControls;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
namespace UACSView
{
    public partial class VIEW_YSLExitLineSaddle : FormBase
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
        public VIEW_YSLExitLineSaddle()
        {
            InitializeComponent();
            //反射获取双缓存
            Type dgvType = this.dgvExitSaddleInfo.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvExitSaddleInfo, true, null);

            this.Load += VIEW_D108ExitLineSaddle_Load;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }

        private UnitSaddleTagRead lineSaddleTag = new UnitSaddleTagRead();
        private UnitSaddleMethod saddleMethod = null;
        private UnitExitSaddleInfo exitSaddleInfo = new UnitExitSaddleInfo();

        private Dictionary<string, CoilUnitSaddle> dicSaddleControls = new Dictionary<string, CoilUnitSaddle>();
        private bool tabActived = true;

        private const string AREA_SAFE_EXIT_D508 = "AREA_SAFE_D508_EXIT";

        public long theValue;
        public long thevalue
        {
            get { return theValue; }
            set { thevalue = value; }
        }

        void VIEW_D108ExitLineSaddle_Load(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            tagDataProvider.ServiceName = "iplature";
            //绑定鞍座控件
            dicSaddleControls["YSL1WC0010"] = coilUnitSaddle10;
            dicSaddleControls["YSL1WC0009"] = coilUnitSaddle9;
            dicSaddleControls["YSL1WC0008"] = coilUnitSaddle8;
            dicSaddleControls["YSL1WC0007"] = coilUnitSaddle7;
            dicSaddleControls["YSL1WC0006"] = coilUnitSaddle6;
            dicSaddleControls["YSL1WC0005"] = coilUnitSaddle5;
            dicSaddleControls["YSL1WC0004"] = coilUnitSaddle4;
            dicSaddleControls["YSL1WC0003"] = coilUnitSaddle3;
            dicSaddleControls["YSL1WC0002"] = coilUnitSaddle2;
            dicSaddleControls["YSL1WC0001"] = coilUnitSaddle1;

            //coilUnitStatus1.InitTagDataProvide(constData.tagServiceName);
            //coilUnitStatus1.MyStatusTagName = AREA_SAFE_EXIT_D102;

            //实例化机组鞍座处理类
            saddleMethod = new UnitSaddleMethod(constData.UnitNo_YSL, constData.ExitSaddleDefine, constData.tagServiceName);
            saddleMethod.ReadDefintion();

            lineSaddleTag.InitTagDataProvider(constData.tagServiceName);

            coilUnitSaddleButton1.MySaddleNo = "YSL1WC0001";
            coilUnitSaddleButton2.MySaddleNo = "YSL1WC0002";
            coilUnitSaddleButton3.MySaddleNo = "YSL1WC0003";
            coilUnitSaddleButton4.MySaddleNo = "YSL1WC0004";
            coilUnitSaddleButton5.MySaddleNo = "YSL1WC0005";
            coilUnitSaddleButton6.MySaddleNo = "YSL1WC0006";
            coilUnitSaddleButton7.MySaddleNo = "YSL1WC0007";
            coilUnitSaddleButton8.MySaddleNo = "YSL1WC0008";
            coilUnitSaddleButton9.MySaddleNo = "YSL1WC0009";
            coilUnitSaddleButton10.MySaddleNo = "YSL1WC0010";
            //coilUnitSaddleButton.MySaddleTagName = "D108_EXIT_0001_LOCK_REQUEST_SET";

            //把表中的tag名称赋值到控件中
            foreach (Control control in panel1.Controls)
            {
                //添加解锁鞍座控件
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (saddleMethod.DicSaddles.ContainsKey(t.MySaddleNo))
                    {
                        UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[t.MySaddleNo];
                        if (!string.IsNullOrEmpty(theSaddleInfo.TagAdd_LockRequest) && theSaddleInfo.TagAdd_LockRequest != "")
                        {
                            t.MySaddleTagName = theSaddleInfo.TagAdd_LockRequest;
                            lineSaddleTag.AddTagName(theSaddleInfo.TagAdd_LockRequest);

                        }
                    }
                }
                //添加机组状态控件
                if (control is CoilUnitStatus)
                {
                    CoilUnitStatus t = (CoilUnitStatus)control;
                    if (!string.IsNullOrEmpty(t.MyStatusTagName) && t.MyStatusTagName != "")
                    {
                        lineSaddleTag.AddTagName(t.MyStatusTagName);
                    }
                }
            }

            lineSaddleTag.SetReady();
            //把实例化后的机组tag处理类装备每个控件
            foreach (Control control in panel1.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    t.InitUnitSaddle(lineSaddleTag);
                }
            }

            exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo, constData.UnitNo_YSL);           
            //是否开启定时器
            timer_LineSaddleControl.Enabled = true;
            //设定刷新时间
            timer_LineSaddleControl.Interval = 3000;
            //GetFirstArea();
        }

        private void timer_LineSaddleControl_Tick(object sender, EventArgs e)
        {
            //不在当前页面停止刷新
            if (tabActived == false)
            {
                return;
            }

            //coilUnitStatus1.RefreshControl();
            //if (thevalue == 1)
            //{
            //    this.panel1.BackColor = Color.Transparent;
            //}
            //else
            //{
            //    this.panel1.BackColor = Color.Red;
            //}
            lineSaddleTag.readTags();
            foreach (Control control in panel1.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (!string.IsNullOrEmpty(t.MySaddleTagName) && t.MySaddleTagName != "")
                    {
                        CoilUnitSaddleButton.delRefresh_Button_Light del = t.refresh_Button_Light;
                        del(lineSaddleTag.getTagValue(t.MySaddleTagName));
                    }
                }
            }

            foreach (Control control in panel1.Controls)
            {
                if (control is CoilUnitStatus)
                {
                    CoilUnitStatus t = (CoilUnitStatus)control;
                    if (!string.IsNullOrEmpty(t.MyStatusTagName) && t.MyStatusTagName != "")
                    {
                        CoilUnitStatus.delSetColor del = t.SetColor;
                        del(lineSaddleTag.getTagValue(t.MyStatusTagName));
                    }
                }
            }

            exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo, constData.UnitNo_YSL);          
            getSaddleMessage();
            //Refresh_D108_TO_D208();
            //GetFirstArea();
            //GetNextUnit();
        }


        private void getSaddleMessage()
        {
            saddleMethod.ReadDefintion();
            saddleMethod.getTagNameList();
            saddleMethod.getTagValues();

            foreach (string theL2SaddleName in dicSaddleControls.Keys)
            {
                if (saddleMethod.DicSaddles.ContainsKey(theL2SaddleName))
                {
                    CoilUnitSaddle conSaddle = dicSaddleControls[theL2SaddleName];
                    //CoilUnitSaddleButton SaddleButton = dicUnitSaddleButton[theL2SaddleName];
                    UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[theL2SaddleName];
                    //鞍座反馈
                    if (theSaddleInfo.TagVal_IsLocked == 1)
                        conSaddle.UpVisiable = true;
                    else
                        conSaddle.UpVisiable = false;

                    //鞍座占位
                    if (theSaddleInfo.TagVal_IsOccupied == 1)
                        conSaddle.CoilBackColor = Color.Green;
                    else
                        conSaddle.CoilBackColor = Color.FromArgb(165, 222, 241);


                    //钢卷号
                    if (theSaddleInfo.CoilNO != string.Empty)
                    {
                        conSaddle.CoilId = theSaddleInfo.CoilNO;
                        conSaddle.CoilStatus = 2;
                    }
                    else
                    {
                        conSaddle.CoilId = "";
                        conSaddle.CoilStatus = -10;
                    }
                }

            }
        }

        //需要引用formbase
        void MyTabActivated(object sender, EventArgs e)
        {
            tabActived = true;
        }
        void MyTabDeactivated(object sender, EventArgs e)
        {
            tabActived = false;
        }


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

        //private string SelectNextUnio;
        //private void selectUnio()
        //{
        //    SelectNextUnio = string.Empty;
        //    if (checkBoxAll.Checked == true)
        //    {
        //        SelectNextUnio += "ALL";
        //        SelectNextUnio += ",";
        //    }
        //    if (checkBoxD122.Checked==true)
        //    {
        //        SelectNextUnio += "D122";
        //        SelectNextUnio += ",";
        //    }
        //    if(checkBoxD212.Checked == true)
        //    {
        //        SelectNextUnio += "D212";
        //        SelectNextUnio += ",";
        //    }
        //    if(checkBoxD112.Checked == true)
        //    {
        //        SelectNextUnio += "D112";
        //        SelectNextUnio += ",";
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    selectUnio();
        //    inDatas["D102EXIT_TO_B_NEXT_UNIT_NO"] = SelectNextUnio;
        //    tagDataProvider.Write2Level1(inDatas, 1);
        //    MessageBox.Show("已选择下道机组！");
        //    checkBoxD122.Checked = false;
        //    checkBoxD212.Checked = false;
        //    checkBoxD112.Checked = false;
        //    checkBoxAll.Checked = false;
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    SelectNextUnio = "";
        //    inDatas["D102EXIT_TO_B_NEXT_UNIT_NO"] = SelectNextUnio;
        //    tagDataProvider.Write2Level1(inDatas, 1);
        //    MessageBox.Show("已清空下道机组！");
        //    checkBoxD122.Checked = false;
        //    checkBoxD212.Checked = false;
        //    checkBoxD112.Checked = false;
        //    checkBoxAll.Checked = false;
        //}

        ////获取当前选择的下道机组
        //private void GetNextUnit()
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("D102EXIT_TO_B_NEXT_UNIT_NO");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    labelNextUnit.Text = get_value("D102EXIT_TO_B_NEXT_UNIT_NO");
        //    if (labelNextUnit.Text.Contains("ALL"))
        //    {
        //        labelNextUnit.Text = "全部机组";
        //    }
        //}

        //private void GetFirstArea()
        //{
        //    try
        //    {
        //        List<string> lstAdress = new List<string>();
        //        lstAdress.Clear();
        //        lstAdress.Add("D102EXIT_B_TO_Z04_OR_Z05");
        //        lstAdress.Add("D102EXIT_B_TO_Z02_OR_Z03");
        //        arrTagAdress = lstAdress.ToArray<string>();
        //        readTags();
        //        string firstArea = get_value("D102EXIT_B_TO_Z04_OR_Z05");
        //        string firstArea1 = get_value("D102EXIT_B_TO_Z02_OR_Z03");
        //        if (firstArea == "Z04")
        //        {
        //            btnZ04.Enabled = false;
        //            btnZ05.Enabled = true;
        //            btnZ04.BackColor = Color.Green;
        //            btnZ05.BackColor = Color.Gray;
        //        }
        //        else if (firstArea == "Z05")
        //        {
        //            btnZ05.Enabled = false;
        //            btnZ04.Enabled = true;
        //            btnZ05.BackColor = Color.Green;
        //            btnZ04.BackColor = Color.Gray;
        //        }
        //        else
        //        {
        //            btnZ05.Enabled = true;
        //            btnZ04.Enabled = true;
        //            btnZ05.BackColor = Color.Gray;
        //            btnZ04.BackColor = Color.Gray;
        //        }

        //        if (firstArea1 == "Z02")
        //        {
        //            btnZ02.Enabled = false;
        //            btnZ03.Enabled = true;
        //            btnZ02.BackColor = Color.Green;
        //            btnZ03.BackColor = Color.Gray;
        //        }
        //        else if (firstArea1 == "Z03")
        //        {
        //            btnZ03.Enabled = false;
        //            btnZ02.Enabled = true;
        //            btnZ03.BackColor = Color.Green;
        //            btnZ02.BackColor = Color.Gray;
        //        }
        //        else
        //        {
        //            btnZ02.Enabled = true;
        //            btnZ03.Enabled = true;
        //            btnZ02.BackColor = Color.Gray;
        //            btnZ03.BackColor = Color.Gray;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        private void btnZ02_Click(object sender, EventArgs e)
        {
            string firstArea = "Z02";
            inDatas["D102EXIT_B_TO_Z02_OR_Z03"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z02！");
        }

        private void btnZ03_Click(object sender, EventArgs e)
        {
            string firstArea = "Z03";
            inDatas["D102EXIT_B_TO_Z02_OR_Z03"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z03！");
        }

        private void btnZ04_Click(object sender, EventArgs e)
        {
            string firstArea = "Z04";
            inDatas["D102EXIT_B_TO_Z04_OR_Z05"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z04！");
        }

        private void btnZ05_Click(object sender, EventArgs e)
        {
            string firstArea = "Z05";
            inDatas["D102EXIT_B_TO_Z04_OR_Z05"] = firstArea;
            tagDataProvider.Write2Level1(inDatas, 1);
            MessageBox.Show("已选择优先库区Z05！");
        }

        //private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    checkBoxD122.Checked = false;
        //    checkBoxD212.Checked = false;
        //    checkBoxD112.Checked = false;
        //}

        //private void checkBoxD112_CheckedChanged(object sender, EventArgs e)
        //{
        //    checkBoxAll.Checked = false;
        //}

        //private void checkBoxD122_CheckedChanged(object sender, EventArgs e)
        //{
        //    checkBoxAll.Checked = false;
        //}

        //private void checkBoxD212_CheckedChanged(object sender, EventArgs e)
        //{
        //    checkBoxAll.Checked = false;
        //}


        private void button4_Click(object sender, EventArgs e)
        {
            auth.OpenForm("01 Z01跨监控");
        }


        //#region 封闭卷进机组或进库区
        //private string sql = string.Empty;
        //private string plasticFlag = string.Empty;
        //private string coilFlow = string.Empty;
        //private string time = string.Empty;
        //private int index;
        //private int[] ret = new int[6];
        //private bool[] same = new bool[6];
        //private string[] coilNo = new string[6];

        ////判断是否为封闭卷，判断钢卷号是否相同
        //private void Judge()
        //{
        //    same[0] = false;
        //    same[1] = false;
        //    same[2] = false;
        //    same[3] = false;
        //    same[4] = false;
        //    same[5] = false;
        //    try
        //    {
        //        string sql = @"SELECT COIL_NO, PLASTIC_FLAG, PLASTIC_TIME FROM UACS_YARDMAP_COIL_PLASTIC";
        //        for (int i = 0; i < dgvExitSaddleInfo.RowCount; i++)
        //        {
        //            if (dgvExitSaddleInfo.Rows[i].Cells["FORBIDEN_FLAG"].Value.ToString() == "1")
        //            {
        //                ret[i] = 1;
        //                coilNo[i] = dgvExitSaddleInfo.Rows[i].Cells["COIL_NO"].Value.ToString().Trim();

        //                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
        //                {
        //                    while (rdr.Read())
        //                    {
        //                        if (coilNo[i] == rdr["COIL_NO"].ToString().Trim())
        //                        {
        //                            same[i] = true;
        //                        }
        //                        //else
        //                        //{
        //                        //    same[i] = false;
        //                        //}
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ret[i] = 0;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        //MessageBox.Show(ex.ToString());
        //    }
        //}

        ////控件显示
        //private void btnDispaly()
        //{
        //    if (ret[0] == 1)
        //    {
        //        btnToUnit1.Visible = true;
        //        btnToStock1.Visible = true;
        //    }
        //    else
        //    {
        //        btnToUnit1.Visible = false;
        //        btnToStock1.Visible = false;
        //    }

        //    if (ret[1] == 1)
        //    {
        //        btnToUnit2.Visible = true;
        //        btnToStock2.Visible = true;
        //    }
        //    else
        //    {
        //        btnToUnit2.Visible = false;
        //        btnToStock2.Visible = false;
        //    }

        //    if (ret[2] == 1)
        //    {
        //        btnToUnit3.Visible = true;
        //        btnToStock3.Visible = true;
        //    }
        //    else
        //    {
        //        btnToUnit3.Visible = false;
        //        btnToStock3.Visible = false;
        //    }

        //    if (ret[3] == 1)
        //    {
        //        btnToUnit4.Visible = true;
        //        btnToStock4.Visible = true;
        //    }
        //    else
        //    {
        //        btnToUnit4.Visible = false;
        //        btnToStock4.Visible = false;
        //    }
        //    if (ret[4] == 1)
        //    {
        //        btnToUnit5.Visible = true;
        //        btnToStock5.Visible = true;
        //    }
        //    else
        //    {
        //        btnToUnit5.Visible = false;
        //        btnToStock5.Visible = false;
        //    }
        //    if (ret[5] == 1)
        //    {
        //        btnToUnit6.Visible = true;
        //        btnToStock6.Visible = true;
        //    }
        //    else
        //    {
        //        btnToUnit6.Visible = false;
        //        btnToStock6.Visible = false;
        //    }
        //}

        //private void Refresh_D108_TO_D208()
        //{
        //    Judge();
        //    btnDispaly();
        //}

        ////SQL执行
        //private void executeNonQuery(int i)
        //{
        //    time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    if (same[i] == true)
        //    {
        //        sql = @"UPDATE UACS_YARDMAP_COIL_PLASTIC  SET PLASTIC_FLAG = '{0}',PLASTIC_TIME = '{1}' WHERE COIL_NO = '{2}'";
        //        sql = string.Format(sql, plasticFlag, time, coilNo[i]);
        //    }
        //    else
        //    {
        //        sql = @"INSERT INTO UACS_YARDMAP_COIL_PLASTIC(COIL_NO, PLASTIC_FLAG, PLASTIC_TIME) VALUES ('{0}', '{1}', '{2}')";
        //        sql = string.Format(sql, coilNo[i], plasticFlag, time);
        //    }
        //    try
        //    {
        //        DB2Connect.DBHelper.ExecuteNonQuery(sql);
        //        MessageBox.Show("已更新封闭卷状态");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}


        //private void btnToUnit1_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到机组操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "1";
        //        index = 0;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToStock1_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到库区操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "0";
        //        index = 0;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToUnit2_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到机组操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "1";
        //        index = 1;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToStock2_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到库区操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "0";
        //        index = 1;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToUnit3_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到机组操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "1";
        //        index = 2;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToStock3_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到库区操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "0";
        //        index = 2;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToUnit4_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到机组操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "1";
        //        index = 3;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToStock4_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到库区操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "0";
        //        index = 3;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToUnit5_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到机组操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "1";
        //        index = 4;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToStock5_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到库区操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "0";
        //        index = 4;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToUnit6_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到机组操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "1";
        //        index = 5;
        //        executeNonQuery(index);
        //    }
        //}

        //private void btnToStock6_Click(object sender, EventArgs e)
        //{
        //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
        //    DialogResult dr = MessageBox.Show("确定要对封闭卷进行到库区操作吗？", "操作提示", btn);
        //    if (dr == DialogResult.OK)
        //    {
        //        plasticFlag = "0";
        //        index = 5;
        //        executeNonQuery(index);
        //    }
        //}
        //#endregion

        private void dgvExitSaddleInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if (dgvExitSaddleInfo.Columns[e.ColumnIndex].Name == "COIL_OPEN_DIRECTION")
            {
                string coilNo = null;
                string CoilOpenDir = null;
                if (dgvExitSaddleInfo.Rows[e.RowIndex].Cells["COIL_OPEN_DIRECTION"].Value != DBNull.Value)
                {
                    coilNo = dgvExitSaddleInfo.Rows[e.RowIndex].Cells["COIL_NO"].Value.ToString().Trim();
                    CoilOpenDir = dgvExitSaddleInfo.Rows[e.RowIndex].Cells["COIL_OPEN_DIRECTION"].Value.ToString().Trim();
                    if (CoilOpenDir == "正")
                    {
                        DialogResult dr = MessageBox.Show("确定要修改钢卷" + coilNo + "的开卷方向为反吗？","修改确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if(dr == DialogResult.OK)
                        {
                            string sqlText = @"UPDATE UACS_YARDMAP_COIL SET COIL_OPEN_DIRECTION = 1 WHERE COIL_NO = '" + coilNo + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                            ParkClassLibrary.HMILogger.WriteLog("开卷方向","修改钢卷" + coilNo +"开卷方向为反",ParkClassLibrary.LogLevel.Info,this.Text);
                        }                       
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("确定要修改钢卷" + coilNo + "的开卷方向为正吗？", "修改确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if(dr == DialogResult.OK)
                        {
                            string sqlText = @"UPDATE UACS_YARDMAP_COIL SET COIL_OPEN_DIRECTION = 0 WHERE COIL_NO = '" + coilNo + "'";
                            DB2Connect.DBHelper.ExecuteNonQuery(sqlText);
                            ParkClassLibrary.HMILogger.WriteLog("开卷方向", "修改钢卷" + coilNo + "开卷方向为正", ParkClassLibrary.LogLevel.Info, this.Text);
                        }                       
                    }
                }
                exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo, constData.UnitNo_YSL);                

            }
            //操作按钮
            //if (dgvExitSaddleInfo.Columns[e.ColumnIndex].Name == "FORBIDEN_COIL_FLOW")
            //{
            //    string coilNo = null;
            //    string forbidenCoilFlow = null;
            //    int forbidenFlag = 0;
            //    int L3CoilStatus = 0;
            //    if (((DataGridViewButtonCell)dgvExitSaddleInfo.Rows[e.RowIndex].Cells["FORBIDEN_COIL_FLOW"]).UseColumnTextForButtonValue == true)
            //    {
            //        if (dgvExitSaddleInfo.Rows[e.RowIndex].Cells["FORBIDEN_COIL_FLOW"].Value != DBNull.Value)
            //        {
            //            coilNo = dgvExitSaddleInfo.Rows[e.RowIndex].Cells["COIL_NO"].Value.ToString().Trim();
            //            forbidenFlag = Convert.ToInt32(dgvExitSaddleInfo.Rows[e.RowIndex].Cells["FORBIDEN_FLAG"].Value);
            //            L3CoilStatus = Convert.ToInt32(dgvExitSaddleInfo.Rows[e.RowIndex].Cells["L3_COIL_STATUS"].Value);
            //        }
            //        FrmForbidenCoilFlow frmForbidenCoilFlow = new FrmForbidenCoilFlow();
            //        frmForbidenCoilFlow.CoilNo = coilNo;
            //        frmForbidenCoilFlow.UnitNo = constData.UnitNo_YSL;
            //        frmForbidenCoilFlow.CoilStatus = L3CoilStatus;
            //        frmForbidenCoilFlow.ForbidenFlag = forbidenFlag;
            //        frmForbidenCoilFlow.StartPosition = FormStartPosition.CenterScreen;
            //        frmForbidenCoilFlow.ShowDialog();
            //        exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo, constData.UnitNo_YSL);
            //        dgvRefresh();
            //    }
            //}
        }

        //private void dgvRefresh()
        //{
        //    int forbidenFlag = 0;
        //    int L3CoilStatus = 0;
        //    for (int i = 0; i < dgvExitSaddleInfo.RowCount; i++)
        //    {
        //        if ((dgvExitSaddleInfo.Rows[i].Cells["FORBIDEN_FLAG"].Value != DBNull.Value && dgvExitSaddleInfo.Rows[i].Cells["L3_COIL_STATUS"].Value != DBNull.Value))
        //        {
        //            forbidenFlag = Convert.ToInt32(dgvExitSaddleInfo.Rows[i].Cells["FORBIDEN_FLAG"].Value);
        //            L3CoilStatus = Convert.ToInt32(dgvExitSaddleInfo.Rows[i].Cells["L3_COIL_STATUS"].Value);
        //            if ((forbidenFlag != 1 && L3CoilStatus < 20) || (forbidenFlag == 1))
        //            {
        //                ((DataGridViewButtonCell)dgvExitSaddleInfo.Rows[i].Cells["FORBIDEN_COIL_FLOW"]).UseColumnTextForButtonValue = true;
        //            }
        //        }
        //    }
        //}
    }
}
