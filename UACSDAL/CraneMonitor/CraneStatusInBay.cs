using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using UACSDAL.CraneMonitor;

namespace UACSDAL
{
    /// <summary>
    /// 行车状态处理数据类
    /// </summary>
    public class CraneStatusInBay
    {

        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        private Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
        private const string numberIsNull = "999999";

        //step1
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


        public List<string> lstCraneNO = new List<string>();

        //step2
        public void AddCraneNO(string strCraneNO)
        {
            try
            {
                lstCraneNO.Add(strCraneNO);
            }
            catch (Exception ex)
            {
            }
        }


        private string[] arrTagAdress;

        //step3
        public void SetReady()
        {
            try
            {
                List<string> lstAdress = new List<string>();
                foreach (string theCranNO in lstCraneNO)
                {
                    string tag_Head = theCranNO + "_";
                    // 准备好
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_READY);
                    // 控制模式
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_CONTROL_MODE);
                    // 请求计划
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_ASK_PLAN);
                    // 当前指令
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_ORDER_ID);
                    // 大车位置
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_XACT);
                    // 小车位置
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_YACT);
                    // 夹钳高度
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_ZACT);
                    // 有卷标志
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_HAS_COIL);
                    // 行车状态
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_CRANE_STATUS);
                    // HEART_BEAT
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_CRANE_PLC_HEART_BEAT);
                    // 大车方向实际速度
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_XSPEED);
                    // 小车方向实际速度
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_YSPEED);
                    // 升降实际速度
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_ZSPEED);
                    // 称重信号
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_WEIGHT_LOADED);
                    // 夹钳旋转角度
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_ROTATE_ANGLE_ACT);
                    // 夹钳开度
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_CLAMP_WIDTH_ACT);
                    // 计划起卷X
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_PLAN_UP_X);
                    // 计划起卷Y
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_PLAN_UP_Y);
                    // 计划起卷Z
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_PLAN_UP_Z);
                    // 计划落卷X
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_PLAN_DOWN_X);
                    // 计划落卷Y
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_PLAN_DOWN_Y);
                    // 计划落卷Z
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_PLAN_DOWN_Z);
                    // 夹钳温度
                    lstAdress.Add(tag_Head + CraneStatusBase.ADRESS_CRANE_COIL_TEMPERATURE);
                    // 工位装料计划完成（1:完成 0：未完成）
                    lstAdress.Add(CraneStatusBase.ADRESS_PLAN_FINISH + "_A" + theCranNO);


                    #region 报警
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_0);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_1);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_2);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_3);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_4);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_5);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_6);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_7);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_8);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_9);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_10);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_11);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_12);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_13);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_14);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_15);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_16);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_17);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_18);
                    lstAdress.Add(tag_Head + CraneStatusBase.FAULT_CODE_19);
                    #endregion


                    #region 红绿灯
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_1);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_1);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_2);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_2);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_3);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_3);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_4);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_4);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_5);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_5);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_6);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_6);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_7);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_7);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_8);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_8);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_9);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_9);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_10);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_10);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_11);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_11);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_12);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_12);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_13);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_13);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_14);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_14);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_15);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_15);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_16);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_16);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_17);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_17);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_18);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_18);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_19);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_19);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_20);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_20);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_21);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_21);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_22);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_22);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_23);
                    lstAdress.Add(TrafficLightBase.AREA_SAFE_23);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_24);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_25);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_26);
                    lstAdress.Add(TrafficLightBase.AREA_RESERVE_27);
                    #endregion
                }
                arrTagAdress = lstAdress.ToArray<string>();
            }
            catch (Exception ex)
            {
            }
        }


        private Dictionary<string, CraneStatusBase> dicCranePLCStatusBase = new Dictionary<string, CraneStatusBase>();

        public Dictionary<string, CraneStatusBase> DicCranePLCStatusBase
        {
            get { return dicCranePLCStatusBase; }
        }



        //step4
        public void getAllPLCStatusInBay(List<string> listCraneNo)
        {
            try
            {
                readTags();
                foreach (string theCraneNO in lstCraneNO)
                {
                    if (listCraneNo.Contains(theCraneNO))
                    {
                        CraneStatusBase cranePLCStatusBase = getCranePLCStatusFromTags(theCraneNO);
                        dicCranePLCStatusBase[theCraneNO] = cranePLCStatusBase;
                    }
                  
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void getAllPLCStatusInCrane(string _CraneNo)
        {
            try
            {
                readTags();
                foreach (string theCraneNO in lstCraneNO)
                {
                    if (_CraneNo.Contains(theCraneNO))
                    {
                        CraneStatusBase cranePLCStatusBase = getCranePLCStatusFromTags(theCraneNO);
                        dicCranePLCStatusBase[theCraneNO] = cranePLCStatusBase;
                    }

                }

            }
            catch (Exception ex)
            {
            }
        }

        private Dictionary<string, TrafficLightBase> dicTrafficLightBase = new Dictionary<string, TrafficLightBase>();

        public Dictionary<string, TrafficLightBase> DicTrafficLightBase
        {
            get { return dicTrafficLightBase; }
        }

        /// <summary>
        /// 红绿灯Tag
        /// </summary>
        /// <param name="list"></param>
        public void getAllTrafficLight(List<TrafficLightBase> list)
        {
            try
            {
                readTags();
                foreach (TrafficLightBase Light in list)
                {
                    TrafficLightBase trafficLightBase = getTrafficLightBaseFromTags(Light);
                    dicTrafficLightBase[trafficLightBase.AreaNo] = trafficLightBase;
                }

            }
            catch (Exception ex)
            {
            }
        }



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
        

        private CraneStatusBase getCranePLCStatusFromTags(string theCraneNO)
        {
            CraneStatusBase craneBase = new CraneStatusBase();
            try
            {
                craneBase.CraneNO = theCraneNO;
                string tag_Head = craneBase.CraneNO + "_";
                // 准备好
                craneBase.Ready = get_value_x(tag_Head + CraneStatusBase.ADRESS_READY);
                // 控制模式
                craneBase.ControlMode = get_value_x(tag_Head + CraneStatusBase.ADRESS_CONTROL_MODE);
                // 请求计划
                craneBase.AskPlan = get_value_x(tag_Head + CraneStatusBase.ADRESS_ASK_PLAN);
                // 当前指令
                craneBase.OrderID = get_value_x(tag_Head + CraneStatusBase.ADRESS_ORDER_ID);
                // 大车位置  铁路库Double  成品库库int 
                craneBase.XAct = get_value_real(tag_Head + CraneStatusBase.ADRESS_XACT);  
                // 小车位置
                craneBase.YAct = get_value_real(tag_Head + CraneStatusBase.ADRESS_YACT);
                // 夹钳高度
                craneBase.ZAct = get_value_real(tag_Head + CraneStatusBase.ADRESS_ZACT);
                // 有卷标志
                craneBase.HasCoil = get_value_x(tag_Head + CraneStatusBase.ADRESS_HAS_COIL);              
                // 行车状态
                craneBase.CraneStatus = get_value_x(tag_Head + CraneStatusBase.ADRESS_CRANE_STATUS);
                // 心跳
                craneBase.ReceiveTime = get_value_string(tag_Head + CraneStatusBase.ADRESS_CRANE_PLC_HEART_BEAT).ToString();
                // 大车方向实际速度
                craneBase.XSpeed = get_value_x(tag_Head + CraneStatusBase.ADRESS_XSPEED);
                // 小车方向实际速度
                craneBase.YSpeed = get_value_x(tag_Head + CraneStatusBase.ADRESS_YSPEED);
                // 升降实际速度
                craneBase.ZSpeed = get_value_x(tag_Head + CraneStatusBase.ADRESS_ZSPEED);
                // 称重信号
                craneBase.WeightLoaded = get_value_x(tag_Head + CraneStatusBase.ADRESS_WEIGHT_LOADED);
                // 夹钳旋转角度
                craneBase.RotateAngleAct = get_value_real(tag_Head + CraneStatusBase.ADRESS_ROTATE_ANGLE_ACT);
                // 夹钳开度
                craneBase.ClampWidthAct = get_value_real(tag_Head + CraneStatusBase.ADRESS_CLAMP_WIDTH_ACT);
                // 计划起卷X
                craneBase.PlanUpX = get_value_real(tag_Head + CraneStatusBase.ADRESS_PLAN_UP_X);
                // 计划起卷Y
                craneBase.PlanUpY = get_value_real(tag_Head + CraneStatusBase.ADRESS_PLAN_UP_Y);
                // 计划起卷Z
                craneBase.PlanUpZ = get_value_real(tag_Head + CraneStatusBase.ADRESS_PLAN_UP_Z);
                // 计划落卷X
                craneBase.PlanDownX = get_value_real(tag_Head + CraneStatusBase.ADRESS_PLAN_DOWN_X);
                // 计划落卷Y
                craneBase.PlanDownY = get_value_real(tag_Head + CraneStatusBase.ADRESS_PLAN_DOWN_Y);
                // 计划落卷Z
                craneBase.PlanDownZ = get_value_real(tag_Head + CraneStatusBase.ADRESS_PLAN_DOWN_Z);
                // 夹钳温度
                craneBase.COIL_TEMPERATURE = get_value_x(tag_Head + CraneStatusBase.ADRESS_CRANE_COIL_TEMPERATURE);

                string tag_HeadA =  "_A" + craneBase.CraneNO;
                //工位装料计划完成（1:完成 0：未完成）
                craneBase.EV_PLAN_FINISH = get_value_x(CraneStatusBase.ADRESS_PLAN_FINISH + tag_HeadA);

                #region 报警信息
                craneBase.FaultCode_0 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_0);
                craneBase.FaultCode_1 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_1);
                craneBase.FaultCode_2 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_2);
                craneBase.FaultCode_3 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_3);
                craneBase.FaultCode_4 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_4);
                craneBase.FaultCode_5 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_5);
                craneBase.FaultCode_6 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_6);
                craneBase.FaultCode_7 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_7);
                craneBase.FaultCode_8 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_8);
                craneBase.FaultCode_9 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_9);
                craneBase.FaultCode_10 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_10);
                craneBase.FaultCode_11 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_11);
                craneBase.FaultCode_12 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_12);
                craneBase.FaultCode_13 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_13);
                craneBase.FaultCode_14 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_14);
                craneBase.FaultCode_15 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_15);
                craneBase.FaultCode_16 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_16);
                craneBase.FaultCode_17 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_17);
                craneBase.FaultCode_18 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_18);
                craneBase.FaultCode_19 = get_value_x(tag_Head + CraneStatusBase.FAULT_CODE_19);
                #endregion
            }
            catch (Exception ex)
            {
            }
            return craneBase;
        }

        private TrafficLightBase getTrafficLightBaseFromTags(TrafficLightBase theTraffic)
        {
            try
            {
                if (!string.IsNullOrEmpty(theTraffic.AreaNo))
                {
                    string tag_Head =  "1_";
                    // 准备好
                    var Ready = get_value_x(tag_Head + CraneStatusBase.ADRESS_READY);
                    var A2 = get_value_x("AreaReserve2");
                    var A3 = get_value_x("AreaReserve3");
                    var A4 = get_value_x("AreaReserve4");
                    var A5 = get_value_x("AreaReserve5");
                    var A6 = get_value_x("AreaReserve6");
                    var A7 = get_value_x("AreaReserve7");
                    var A8 = get_value_x("AreaReserve8");

                    if (theTraffic.AreaNo.Equals("A1"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_1);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_1);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_1);
                    }
                    else if (theTraffic.AreaNo.Equals("A2"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_2);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_2);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_2);
                    }
                    else if (theTraffic.AreaNo.Equals("A3"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_3);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_3);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_3);
                    }
                    else if (theTraffic.AreaNo.Equals("A4"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_4);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_4);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_4);
                    }
                    else if (theTraffic.AreaNo.Equals("A5"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_5);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_5);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_5);
                    }
                    else if (theTraffic.AreaNo.Equals("A6"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_6);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_6);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_6);
                    }
                    else if (theTraffic.AreaNo.Equals("A7"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_7);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_7);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_7);
                    }
                    else if (theTraffic.AreaNo.Equals("A8"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_8);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_8);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_8);
                    }
                    else if (theTraffic.AreaNo.Equals("A9"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_9);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_9);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_9);
                    }
                    else if (theTraffic.AreaNo.Equals("A10"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_10);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_10);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_10);
                    }
                    else if (theTraffic.AreaNo.Equals("A11"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_11);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_11);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_11);
                    }
                    else if (theTraffic.AreaNo.Equals("A12"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_12);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_12);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_12);
                    }
                    else if (theTraffic.AreaNo.Equals("A13"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_13);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_13);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_13);
                    }
                    else if (theTraffic.AreaNo.Equals("A14"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_14);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_14);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_14);
                    }
                    else if (theTraffic.AreaNo.Equals("A15"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_15);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_15);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_15);
                    }
                    else if (theTraffic.AreaNo.Equals("A16"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_16);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_16);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_16);
                    }
                    else if (theTraffic.AreaNo.Equals("A17"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_17);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_17);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_17);
                    }
                    else if (theTraffic.AreaNo.Equals("A18"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_18);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_18);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_18);
                    }
                    else if (theTraffic.AreaNo.Equals("A19"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_19);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_19);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_19);
                    }
                    else if (theTraffic.AreaNo.Equals("A20"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_20);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_20);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_20);
                    }
                    else if (theTraffic.AreaNo.Equals("A21"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_21);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_21);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_21);
                    }
                    else if (theTraffic.AreaNo.Equals("A22"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_22);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_22);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_22);
                    }
                    else if (theTraffic.AreaNo.Equals("A23"))
                    {
                        theTraffic.AreaReserve = get_value_x(TrafficLightBase.AREA_RESERVE_23);
                        theTraffic.AreaSafe = get_value_x(TrafficLightBase.AREA_SAFE_23);
                        theTraffic.AreaGratStatus = get_value_x(TrafficLightBase.AREA_GRATSTATUS_23);
                    }
                    //工位
                    if (theTraffic.AreaNo.Equals("T1"))
                    {
                        theTraffic.AreaReserveCubicle = get_value_x(TrafficLightBase.AREA_RESERVE_24);
                    }
                    else if (theTraffic.AreaNo.Equals("T2"))
                    {
                        theTraffic.AreaReserveCubicle = get_value_x(TrafficLightBase.AREA_RESERVE_25);
                    }
                    else if (theTraffic.AreaNo.Equals("T3"))
                    {
                        theTraffic.AreaReserveCubicle = get_value_x(TrafficLightBase.AREA_RESERVE_26);
                    }
                    else if (theTraffic.AreaNo.Equals("T4"))
                    {
                        theTraffic.AreaReserveCubicle = get_value_x(TrafficLightBase.AREA_RESERVE_27);
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return theTraffic;
        }

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
            return theValue; 
        }

        private long get_value_real(string tagName)
        {
            long theValue = 0;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToInt32(Convert.ToDouble(valueObject));
            }
            catch
            {
                valueObject = null;
            }
            return theValue; 
        }

        private string get_value_string(string tagName)
        {
            string theValue = string.Empty;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToString((valueObject));
            }
            catch
            {
                valueObject = null;
            }
            return theValue; 
        }

        private double get_value_Double(string tagName)
        {
            double theValue = 0;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToDouble(valueObject);
            }
            catch
            {
                valueObject = null;
            }
            return theValue; 
        }



        /// <summary>
        /// 获取 对应 行车 避让 数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> getCraneRequestInfo(string crane_NO)
        {

            Dictionary<string, string> data = new Dictionary<string, string>();
            try
            {

                string sql = string.Format("SELECT TARGET_CRANE_NO, SENDER, ORIGINAL_SENDER, EVADE_X_REQUEST, EVADE_X, EVADE_DIRECTION, EVADE_ACTION_TYPE, STATUS  FROM UACS_CRANE_EVADE_REQUEST WHERE TARGET_CRANE_NO='{0}'", crane_NO);

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        string tmp = string.Empty;
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            data.Add(rdr.GetName(i).ToString(), rdr[i].ToString());
                        }
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        /// <summary>
        /// 行车指令显示
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="txt_CraneOrder">指令号</param>
        /// <param name="txt_CoilNo">废钢代码</param>
        /// <param name="txt_FromStock">起吊位置</param>
        /// <param name="txt_ToStock">放下位置</param>
        /// <param name="tb_MAT_REQ_WGT">要求重量（装车时有效，卸料为0） 单位-KG</param>
        /// <param name="tb_MAT_ACT_WGT">累计作业重量(当放下时累加)</param>
        /// <param name="tb_MAT_CUR_WGT">当次作业重量(当吊起时记录,当放下时清空)</param>
        /// <param name="tb_ACT_WEIGHT">总累计作业重量 单位-KG</param>
        /// <param name="tb_CurrentStatus">行车当前状态</param>
        public void craneOrderInfo(string craneNo, TextBox txt_CraneOrder, TextBox txt_CoilNo, TextBox txt_FromStock, TextBox txt_ToStock, TextBox tb_MAT_REQ_WGT, TextBox tb_MAT_ACT_WGT, TextBox tb_MAT_CUR_WGT, TextBox tb_ACT_WEIGHT,TextBox tb_CurrentStatus)
        {
            bool isValue = false;
            try
            {
                var orderNo = "";
                string sql = string.Format("SELECT A.*,B.MAT_CNAME FROM UACS_CRANE_ORDER_CURRENT AS A LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE WHERE CRANE_NO = '{0}' ", craneNo);

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {

                        //指令号
                        if (rdr["ORDER_NO"] != System.DBNull.Value)
                        {

                            orderNo = rdr["ORDER_NO"].ToString();
                        }
                        else
                            orderNo = "";
                        bool types = true;
                        //指令号
                        if (rdr["ORDER_TYPE"] != System.DBNull.Value)
                        {
                            tb_CurrentStatus.BackColor = Color.LightGreen;
                            var status = rdr["ORDER_TYPE"].ToString();
                            if (status.Equals("11"))
                            {
                                tb_CurrentStatus.Text = "归堆中";
                            }
                            else if (status.Equals("21"))
                            {
                                tb_CurrentStatus.Text = "装车中";
                            }
                            else if (status.Equals("41"))
                            {
                                tb_CurrentStatus.Text = "工位清扫";
                            }
                            else if (status.Equals("42"))
                            {
                                tb_CurrentStatus.Text = "装冷却剂";
                            }
                            else if (status.Equals("43"))
                            {
                                tb_CurrentStatus.Text = "吸料补料";
                            }
                            else if (status.Equals("X1"))
                            {
                                tb_CurrentStatus.Text = "回登车位";
                            }
                            else if (status.Equals("51"))
                            {
                                tb_CurrentStatus.Text = "检修中";
                            }
                            else if (status.Equals("99"))
                            {
                                tb_CurrentStatus.Text = "空闲";
                            }
                            else
                            {
                                tb_CurrentStatus.Text = "无状态";
                            }
                            types = false;
                        }
                        else
                        {
                            tb_CurrentStatus.Text = "空闲";
                            //tb_CurrentStatus.BackColor = SystemColors.ControlLight;
                            tb_CurrentStatus.BackColor = Color.LightGreen;
                        }
                        if (types)
                        {
                            //指令状态 EMPTY-空闲 ORDER_INIT-执行 COIL_UP_PROCESS-吊起 COIL_DOWN_PROCESS-放下
                            if (rdr["CMD_STATUS"] != System.DBNull.Value)
                            {
                                var status = rdr["CMD_STATUS"].ToString();
                                if (status.Equals("EMPTY"))
                                {
                                    tb_CurrentStatus.Text = "空闲";
                                    tb_CurrentStatus.BackColor = Color.LightGreen;
                                }
                            }
                        }

                        //废钢代码
                        if (rdr["MAT_CODE"] != System.DBNull.Value)
                        {

                            txt_CoilNo.Text = rdr["MAT_CNAME"].ToString();
                        }
                        else
                            txt_CoilNo.Text = numberIsNull;

                        //要求重量
                        if (rdr["MAT_REQ_WGT"] != System.DBNull.Value)
                        {

                            tb_MAT_REQ_WGT.Text = rdr["MAT_REQ_WGT"].ToString();
                        }
                        else
                            tb_MAT_REQ_WGT.Text = numberIsNull;

                        //累计作业重量
                        if (rdr["MAT_ACT_WGT"] != System.DBNull.Value)
                        {

                            tb_MAT_ACT_WGT.Text = rdr["MAT_ACT_WGT"].ToString();
                        }
                        else
                            tb_MAT_ACT_WGT.Text = numberIsNull;

                        ////当次作业重量
                        //if (rdr["MAT_CUR_WGT"] != System.DBNull.Value)
                        //{

                        //    tb_MAT_CUR_WGT.Text = rdr["MAT_CUR_WGT"].ToString();
                        //}
                        //else
                        //    tb_MAT_CUR_WGT.Text = numberIsNull;

                        //前吊作业重量
                        if (rdr["MAT_LAST_WGT"] != System.DBNull.Value)
                        {

                            tb_MAT_CUR_WGT.Text = rdr["MAT_LAST_WGT"].ToString();
                        }
                        else
                            tb_MAT_CUR_WGT.Text = numberIsNull;

                        //起卷库位
                        if (rdr["FROM_STOCK_NO"] != System.DBNull.Value)
                        {
                            txt_FromStock.Text = rdr["FROM_STOCK_NO"].ToString();
                        }
                        else
                            txt_FromStock.Text = numberIsNull;

                        //落下库位
                        if (rdr["TO_STOCK_NO"] != System.DBNull.Value)
                        {
                            txt_ToStock.Text = rdr["TO_STOCK_NO"].ToString();
                        }
                        else
                            txt_ToStock.Text = numberIsNull;

                        isValue = true;
                    }
                }

                if (!string.IsNullOrEmpty(orderNo))
                {
                    string sqlPlanNo = string.Format("SELECT ORDER_NO,ORDER_GROUP_NO,PLAN_NO,REQ_WEIGHT,CMD_STATUS,ACT_WEIGHT FROM UACS_ORDER_QUEUE WHERE CMD_STATUS = '0' AND PLAN_NO IN (SELECT PLAN_NO FROM UACS_ORDER_QUEUE WHERE ORDER_NO = '{0}')  ", orderNo);
                    var ActWeight = 0;
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlPlanNo))
                    {
                        while (rdr.Read())
                        {
                            //累计作业重量 单位-KG
                            if (rdr["ACT_WEIGHT"] != System.DBNull.Value)
                            {
                                ActWeight += Convert.ToInt32(rdr["ACT_WEIGHT"]);
                            }
                            //计划号
                            if (rdr["PLAN_NO"] != System.DBNull.Value)
                            {

                                txt_CraneOrder.Text = rdr["PLAN_NO"].ToString();
                            }
                            else
                                txt_CraneOrder.Text = "";
                        }
                    }
                    //计划总累计重量
                    tb_ACT_WEIGHT.Text = ActWeight.ToString();
                }
            }
            catch (Exception er)
            {

                //throw;
            }
            finally
            {
                if (!isValue)
                {
                    txt_CraneOrder.Text = "";
                    //txt_CoilNo.Text = numberIsNull;
                    txt_FromStock.Text = numberIsNull;
                    txt_ToStock.Text = numberIsNull;
                }
                else
                {
                    //if (txt_CoilNo.Text.Trim() != numberIsNull)
                    //{
                    //    if (IsRepetitionCoil(txt_CoilNo.Text.Trim()))
                    //    {
                    //        txt_CoilNo.ForeColor = Color.Red;
                    //    }
                    //    else
                    //    {
                    //        txt_CoilNo.ForeColor = Color.Black;
                    //    }
                    //}
                    //else
                    //{
                    //    txt_CoilNo.ForeColor = Color.Black;
                    //}
                }
            }
        }

        /// <summary>
        /// 多库位报警
        /// </summary>
        /// <param name="coil"></param>
        /// <returns></returns>
        private bool IsRepetitionCoil(string coil)
        {
            int index = 0;
            try
            {
                string sql = string.Format("SELECT * FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO = '{0}' ", coil);

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        index++;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            if (index >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
