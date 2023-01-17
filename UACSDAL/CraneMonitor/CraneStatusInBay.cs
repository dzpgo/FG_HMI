using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

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
            }
            catch (Exception ex)
            {
            }
            return craneBase;
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
        /// <param name="craneNo"></param>
        public void craneOrderInfo(string craneNo, TextBox txt_CraneOrder, TextBox txt_CoilNo, TextBox txt_FromStock, TextBox txt_ToStock)
        {
            bool isValue = false;
            try
            {

                string sql = string.Format("SELECT A.*,B.MAT_CNAME FROM UACS_CRANE_ORDER_CURRENT AS A LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE WHERE CRANE_NO = '{0}' ", craneNo);

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {

                        //指令号
                        if (rdr["ORDER_NO"] != System.DBNull.Value)
                        {

                            txt_CraneOrder.Text = rdr["ORDER_NO"].ToString();
                        }
                        else
                            txt_CraneOrder.Text = "";


                        //废钢代码
                        if (rdr["MAT_CODE"] != System.DBNull.Value)
                        {

                            txt_CoilNo.Text = rdr["MAT_CNAME"].ToString();
                        }
                        else
                            txt_CoilNo.Text = numberIsNull;

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
