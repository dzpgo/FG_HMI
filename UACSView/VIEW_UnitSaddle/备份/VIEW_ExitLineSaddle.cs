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
namespace UACSView
{
    public partial class VIEW_ExitLineSaddle : FormBase
    {
        public VIEW_ExitLineSaddle()
        {
            InitializeComponent();
            //反射获取双缓存
            Type dgvType = this.dgvExitSaddleInfo.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvExitSaddleInfo, true, null);

            this.Load += VIEW_ExitLineSaddle_Load;
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

        void VIEW_ExitLineSaddle_Load(object sender, EventArgs e)
        {
            //绑定鞍座控件
            dicSaddleControls["D401WC1A00"] = coilUnitSaddle0;
            dicSaddleControls["D401WC1A01"] = coilUnitSaddle1;
            dicSaddleControls["D401WC1A02"] = coilUnitSaddle2;
            dicSaddleControls["D401WC1A03"] = coilUnitSaddle3;
            dicSaddleControls["D401WC1A04"] = coilUnitSaddle4;
            dicSaddleControls["D401WC1A05"] = coilUnitSaddle5;
            dicSaddleControls["D401WC1A06"] = coilUnitSaddle6;
            dicSaddleControls["D401WC1A07"] = coilUnitSaddle7;
            dicSaddleControls["D401WC1A08"] = coilUnitSaddle8;
            dicSaddleControls["D401WC1A09"] = coilUnitSaddle9;
            dicSaddleControls["D401WC1A10"] = coilUnitSaddle10;
            dicSaddleControls["D401WC1A11"] = coilUnitSaddle11;
            dicSaddleControls["D401WC1A12"] = coilUnitSaddle12;
            dicSaddleControls["D401WC1A13"] = coilUnitSaddle13;
            dicSaddleControls["D401WC1A14"] = coilUnitSaddle14;
            dicSaddleControls["D401WC1A15"] = coilUnitSaddle15;
            dicSaddleControls["D401WC1A16"] = coilUnitSaddle16;
            dicSaddleControls["D401WC1A17"] = coilUnitSaddle17;
            dicSaddleControls["D401WC1A18"] = coilUnitSaddle18;
            dicSaddleControls["D401WC1A19"] = coilUnitSaddle19;
       

            //实例化机组鞍座处理类
            saddleMethod = new UnitSaddleMethod(constData.UnitNo,constData.ExitSaddleDefine,constData.tagServiceName);
            saddleMethod.ReadDefintion();

            lineSaddleTag.InitTagDataProvider(constData.tagServiceName);

            coilUnitSaddleButton0.MySaddleNo = "D401WC1A00";
            coilUnitSaddleButton1.MySaddleNo = "D401WC1A01";
            coilUnitSaddleButton2.MySaddleNo = "D401WC1A02";
            coilUnitSaddleButton3.MySaddleNo = "D401WC1A03";
            coilUnitSaddleButton4.MySaddleNo = "D401WC1A16";
            coilUnitSaddleButton5.MySaddleNo = "D401WC1A17";
            coilUnitSaddleButton6.MySaddleNo = "D401WC1A18";
            coilUnitSaddleButton7.MySaddleNo = "D401WC1A19";
            

            //把表中的tag名称赋值到控件中
            foreach (Control control in panelAutoScroll.Controls)
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
            foreach (Control control in panelAutoScroll.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    t.InitUnitSaddle(lineSaddleTag);
                }
            }

            exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo,constData.UnitNo);
            //是否开启定时器
            timer_LineSaddleControl.Enabled = true;
            //设定刷新时间
            timer_LineSaddleControl.Interval = 5000;
        }

        private void timer_LineSaddleControl_Tick(object sender, EventArgs e)
        {
            //不在当前页面停止刷新
            if (tabActived == false)
            {
                return;
            }

            lineSaddleTag.readTags();

            foreach (Control control in panelAutoScroll.Controls)
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

            exitSaddleInfo.getExitSaddleDt(dgvExitSaddleInfo, constData.UnitNo);

            getSaddleMessage();
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

                    UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[theL2SaddleName];
                    //鞍座反馈
                    if (theSaddleInfo.TagVal_IsLocked == 1)
                        conSaddle.UpVisiable = true;
                    else
                        conSaddle.UpVisiable = false;

                    //鞍座占位
                    if (theSaddleInfo.TagVal_IsOccupied == 0)
                        conSaddle.CoilBackColor = Color.Green;
                    else
                        conSaddle.CoilBackColor = Color.LightGray;

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

        

       

        
    }
}
