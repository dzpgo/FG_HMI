using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Baosight.iSuperframe.Forms;


namespace GDITest
{
    public partial class frmSocialCarOut : Form
    {
        public frmSocialCarOut()
        {
            InitializeComponent();
        }

        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;

        public static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("uacs");
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }

                }
                return dbHelper;
            }
        }

        private clsParkingSpaceUtility parkingSpaceUtility = new clsParkingSpaceUtility();


        private string parkingNO = string.Empty;

        public string ParkingNO
        {
            get { return parkingNO; }
            set { parkingNO = value; }
        }


        private void frmSocialCarOut_Load(object sender, EventArgs e)
        {
            try
            {
                //绑定各种事件
                this.comboBox_SCO_PlanNO.DropDown += new EventHandler(comboBox_SCO_PlanNO_DropDown);
                this.comboBox_SCO_PlanNO.SelectedValueChanged += new EventHandler(comboBox_SCO_PlanNO_SelectedValueChanged);

                //读取停车位对应的激光数据
                ReadLaserData(parkingNO);
            }
            catch (Exception ex)
            {
            }
        }



#region  "计划号的选择"

        //计划号下拉框的拉下
        void comboBox_SCO_PlanNO_DropDown(object sender, EventArgs e)
        {
            try
            {
                string strPlanType = "ALL";
                bool blAllPlanStatus = false;

                if (comboBox_SCO_PlanType.Text == "1")
                {
                    strPlanType = "1";
                }
                if (comboBox_SCO_PlanType.Text == "3")
                {
                    strPlanType = "3";
                }
                if (comboBox_SCO_PlanType.Text == "9")
                {
                    strPlanType = "9";
                }
                if (comboBox_SCO_PlanType.Text == "全部")
                {
                    strPlanType = "ALL";
                }
                if (checkBox_SocPlanAllStatus.Checked == true)
                {
                    blAllPlanStatus = true;
                }
                else
                {
                    blAllPlanStatus = false;
                }
                GetPlanNO_SOC(dateTimePicker_Start_SocialCar.Value, dateTimePicker_End_SocialCar.Value, strPlanType, blAllPlanStatus);

            }
            catch (Exception ex)
            {
            }
        }
        //按照查询条件获得计划号，填充下拉框
        public void GetPlanNO_SOC(DateTime theTimeStart, DateTime theTimeEnd, string strPlanType, bool blAllPlanStatus)
        {

            comboBox_SCO_PlanNO.Items.Clear();
            try
            {

                string sqlText = "";
                sqlText = "    SELECT distinct a.plan_no plan_no FROM UACS_L3PLAN_OUT a ,UACS_L3PLANOUT_DETAIL b where (a.plan_no=b.plan_no)  ";
                if (strPlanType == "ALL")
                {
                    
                }
                else
                {
                    sqlText += "and   (substr(a.plan_no,1,1)='"+strPlanType+"') ";
                }
                

                sqlText += "  and  a.Plan_Time>= " + parkingSpaceUtility.DateStart_ToString(theTimeStart);
                sqlText += "  and a.Plan_Time<= " + parkingSpaceUtility.DateEnd_ToString(theTimeEnd);
                if (blAllPlanStatus == false)
                {
                    sqlText += " and b.Plan_Status!=80 ";
                }
                sqlText += "  order by a.PLAN_NO ";



                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {

                    while (rdr.Read())
                    {

                        if (rdr["PLAN_NO"] != System.DBNull.Value)
                        {
                            comboBox_SCO_PlanNO.Items.Add(Convert.ToString(rdr["PLAN_NO"]));
                        }


                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        //计划号下拉框的计划号选择
        void comboBox_SCO_PlanNO_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string strSN = string.Empty;
                long intXStart = 0;

                if (txt_ParkingNO.Text.ToString() == "0305" || txt_ParkingNO.Text.ToString() == "0306" || txt_ParkingNO.Text.ToString() == "0307" || txt_ParkingNO.Text.ToString() == "0308")
                {
                    intXStart = 224000;
                    strSN = "S";
                }
                if (txt_ParkingNO.Text.ToString() == "0405" || txt_ParkingNO.Text.ToString() == "0406" || txt_ParkingNO.Text.ToString() == "0407" || txt_ParkingNO.Text.ToString() == "0408")
                {
                    intXStart = 1000;
                    strSN = "S";
                }
                if (txt_ParkingNO.Text.ToString() == "0301" || txt_ParkingNO.Text.ToString() == "0302" || txt_ParkingNO.Text.ToString() == "0303" || txt_ParkingNO.Text.ToString() == "0304")
                {
                    intXStart = 224000;
                    strSN = "N";
                }
                if (txt_ParkingNO.Text.ToString() == "0401" || txt_ParkingNO.Text.ToString() == "0402" || txt_ParkingNO.Text.ToString() == "0403" || txt_ParkingNO.Text.ToString() == "0404")
                {
                    intXStart = 1000;
                    strSN = "N";
                }

                Read_Data_SCO_Coils(comboBox_SCO_PlanNO.Text.Trim(), strSN, intXStart);
            }
            catch (Exception ex)
            {
            }
        }

        //按照选择的计划号，读取钢卷计划信息和库位信息，并显示  计划下钢卷显示的顺序是由远到近
        public int Read_Data_SCO_Coils(string strPlanNO, string strSN, long intX_Start)
        {

            int i = 0;
            try
            {

                string sqlText = "";
                sqlText = "select     d.s_n,a.bigarea_no bigarea_no,  a.columnno  columnno  ,a.rowno  rowno,a.Flag_ToDetermin Flag_ToDetermin,  c.mat_no materialno    ,c.out_mat_no Out_materialno ,";
                sqlText += "          b.plan_no plan_no,  b.Instore_ID Instore_ID,   b.Ship_lot_Num Ship_lot_Num,  b.Ship_Cname Ship_CName, b.Plan_Time plan_Time, b.piece piece, b.flag_topped flag_topped,  ";
                sqlText += "          c.require_time require_time ,c.plan_Status plan_Status,    ";
                sqlText += "          c.width steelwide, c.external_diameter external_diameter, c.net_weight net_weight, c.gross_weight gross_weight   ";

                sqlText += "  from UACS_L3PLANOUT_DETAIL c  left outer join UACS_COILUNIT_SET a  on (a.materialno= c.mat_no or a.materialno= c.out_mat_no ), UACS_L3PLAN_OUT b , UACS_BIGAREA_DEFINE d   ";


                sqlText += "  where    b.plan_no=c.plan_no  and d.bigarea_no= a.bigarea_no ";
                sqlText += "  and    b.plan_no= " + "'" + strPlanNO + "'";
                if (strSN != string.Empty)
                {
                    sqlText += "  and    d.s_n= " + "'" + strSN + "'";
                }
                if (intX_Start != 0)
                {
                    sqlText += " order by abs(a.X_THEORETICALCENTER-" + intX_Start.ToString() + ")    ";
                }
                else
                {
                    sqlText += "  order by c.mat_no       ";
                }



                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    GridCoilsSCO.Rows.Clear();
                    while (rdr.Read())
                    {
                        i++;
                        GridCoilsSCO.Rows.Add();
                        DataGridViewRow theRow = GridCoilsSCO.Rows[GridCoilsSCO.Rows.Count - 1];
                        if (rdr["bigarea_no"] != System.DBNull.Value && rdr["Flag_ToDetermin"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(rdr["Flag_ToDetermin"]) == 0)
                            {
                                theRow.Cells["GridCoilsSCO_Big_AreaNo"].Value = Convert.ToString(rdr["bigarea_no"]);
                            }
                        }
                        if (rdr["columnno"] != System.DBNull.Value && rdr["rowno"] != System.DBNull.Value && rdr["Flag_ToDetermin"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(rdr["Flag_ToDetermin"]) == 0)
                            {
                                theRow.Cells["GridCoilsSCO_UnitSet"].Value = Convert.ToString(rdr["columnno"]) + "列" + Convert.ToString(rdr["rowno"]) + "行";
                            }
                        }
                        string strMatNO = "";
                        if (rdr["materialno"] != System.DBNull.Value)
                        {
                            theRow.Cells["GridCoilsSCO_Materialno"].Value = Convert.ToString(rdr["materialno"]);
                            strMatNO = Convert.ToString(rdr["materialno"]);
                        }
                        string strOutMatNO = "";
                        if (rdr["Out_materialno"] != System.DBNull.Value)
                        {
                            theRow.Cells["GridCoilsSCO_Out_Materialno"].Value = Convert.ToString(rdr["Out_materialno"]);
                            strOutMatNO = Convert.ToString(rdr["Out_materialno"]);
                        }
                        if (rdr["plan_no"] != System.DBNull.Value) { theRow.Cells["GridCoilsSCO_Plan_no"].Value = Convert.ToString(rdr["plan_no"]); }

                        if (rdr["Instore_ID"] != System.DBNull.Value)
                        {
                            theRow.Cells["GridCoilsSCO_Destation"].Value = Convert.ToString(rdr["Instore_ID"]);
                        }


                        if (rdr["steelwide"] != System.DBNull.Value) { theRow.Cells["GridCoilsSCO_steelwide"].Value = Convert.ToString(rdr["steelwide"]); }


                        long intDia = 0;
                        if (rdr["external_diameter"] != System.DBNull.Value)
                        {
                            theRow.Cells["GridCoilsSCO_external_diameter"].Value = Convert.ToString(rdr["external_diameter"]);
                            intDia = Convert.ToInt32(rdr["external_diameter"]);
                        }
                        if (intDia == 0)
                        {
                            if (Find_Diameter_InCloiInfo(strMatNO, strOutMatNO, ref intDia) == true)
                            {
                                theRow.Cells["GridCoilsSCO_external_diameter"].Value = intDia.ToString();
                            }
                            else
                            {
                                try
                                {
                                    theRow.Cells["GridCoilsSCO_external_diameter"].Value = (Cal_Coil_ExDiameter(Convert.ToInt32(rdr["net_weight"]), Convert.ToInt32(rdr["steelwide"]))).ToString();
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }

                        if (rdr["net_weight"] != System.DBNull.Value) { theRow.Cells["GridCoilsSCO_net_weight"].Value = Convert.ToString(rdr["net_weight"]); }
                        if (rdr["gross_weight"] != System.DBNull.Value) { theRow.Cells["GridCoilsSCO_gross_weight"].Value = Convert.ToString(rdr["gross_weight"]); }
                        //if (rdr["plan_Time"] != System.DBNull.Value) { theRow.Cells["GridCoilsSCO_Plan_Time"].Value = Convert.ToString(rdr["plan_Time"]); }
                        //if (rdr["piece"] != System.DBNull.Value) { theRow.Cells["GridCoilsSCO_Piece"].Value = Convert.ToString(rdr["piece"]); }
                        //if (rdr["Flag_Topped"] != System.DBNull.Value) { theRow.Cells["GridCoilsSCO_Flag_Topped"].Value = Convert.ToString(rdr["Flag_Topped"]); }
                        if (theRow.Cells["GridCoilsSCO_Big_AreaNo"].Value == null)
                        {
                            theRow.Cells["GridCoilsSCO_UnitSet"].Style.BackColor = Color.SlateGray;
                        }
                        else
                        {
                            theRow.Cells["GridCoilsSCO_UnitSet"].Style.BackColor = Color.LightGreen;
                        }

                        theRow.Cells["GridCoilsSCO_Selected"].Value = 0;


                    }
                }



            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return i;
        }

        //从内部卷表中获得钢卷的直径
        private bool Find_Diameter_InCloiInfo(string strMatNO, string strOurMatNO, ref long intDia)
        {
            bool blFinded = false;
            try
            {
                intDia = 0;
                string sqlText = "SELECT * FROM UACS_INCOILS_INFO where materialno='" + strMatNO + "' or materialno='" + strOurMatNO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        if (rdr["external_diameter"] != System.DBNull.Value)
                        {
                            intDia = Convert.ToInt32(rdr["external_diameter"]);
                            if (intDia > 0)
                            {
                                blFinded = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return blFinded;
        }

        //补算计算钢卷直径
        public long Cal_Coil_ExDiameter(long intNET_WEIGHT, long intSteelWide)
        {
            long exDiameter = 0;
            try
            {

                exDiameter = Convert.ToInt32((Math.Sqrt(intNET_WEIGHT * 1000 / (3.1416 * 7782 * intSteelWide) + 0.58 / 2 * 0.58 / 2)) * 1000 * 2);


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return exDiameter;
        }

#endregion


#region "读取激光数据"

        bool blSOC_CanSelectCoils = false;


        //读取社会车辆激光主表的数据
        void ReadLaserData(string strParkingNO)
        {
            try
            {
                blSOC_CanSelectCoils = false;

                string strTreatmentNO = parkingSpaceUtility.Get_TreatmentNO_OnParkingSpace(strParkingNO);


                string strSN = parkingSpaceUtility.Get_SN_OnParkingSpace(strParkingNO);
                if (strSN == "S")
                {
                    textBox_SOC_SN.Text = "南跨";
                }
                else if (strSN == "N")
                {
                    textBox_SOC_SN.Text = "北跨";
                }

                string strHead_Postion = parkingSpaceUtility.Get_Head_Postion_OnParkingSpace(strParkingNO);


                string strCarNO = parkingSpaceUtility.Get_CarNO_OnParkingSpace(strParkingNO);

                textBox_SOC_CarNO.Text = strCarNO;

                string strIsLoaded = parkingSpaceUtility.Get_ISLoaded_OnParkingSpace(strParkingNO);



                long intMaxCount = parkingSpaceUtility.Get_MAXCount_OfCramera(strTreatmentNO);

                bool FlagConfirmed = parkingSpaceUtility.Get_FlagConfirmed_OfCramera(strTreatmentNO, intMaxCount);

                bool hasCranePlan = parkingSpaceUtility.Has_CranePlan(strTreatmentNO);

                if (FlagConfirmed == false && hasCranePlan == false)
                {
                    blSOC_CanSelectCoils = true;
                }
                else
                {
                    blSOC_CanSelectCoils = false;
                }


                if (blSOC_CanSelectCoils == true)
                {
                    textBox_SOC_CanCreatePlan.Text = "可以操作";
                }
                else
                {
                    textBox_SOC_CanCreatePlan.Text = "不能操作";
                }


                ReadLaserData_Detail(strTreatmentNO, intMaxCount, strSN);

            }
            catch (Exception ex)
            {
            }
        }



        //鞍座的位置从车头到车位排序显示
        void ReadLaserData_Detail(string strTreatmentNO, long intMaxCount, string strSN)
        {
            try
            {
                string sqlText = "SELECT * FROM UACS_CAMERASCAN_DETAIL where treatment_NO=" + "'" + strTreatmentNO + "'" + " and  SCAN_COUNT=" + intMaxCount.ToString();
                if (strSN == "N")
                {
                    sqlText = sqlText + " order by Groove_CenterY ";
                }

                if (strSN == "S")
                {
                    sqlText = sqlText + " order by Groove_CenterY desc";
                }


                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    int i = 0;
                    dataGridSOC.Rows.Clear();
                    while (rdr.Read())
                    {
                        i++;
                        dataGridSOC.Rows.Add();
                        DataGridViewRow theRow = dataGridSOC.Rows[dataGridSOC.Rows.Count - 1];
                        if (rdr["MaterialNO"] != System.DBNull.Value) { theRow.Cells["dataGridSOC_Materialno"].Value = Convert.ToString(rdr["MaterialNO"]); }
                        theRow.Cells["dataGridSOC_POS_Index"].Value = "北侧起第" + i.ToString() + "鞍座";
                        if (rdr["Groove_CenterX"] != System.DBNull.Value) { theRow.Cells["dataGridSOC_X"].Value = Convert.ToString(rdr["Groove_CenterX"]); }
                        if (rdr["Groove_CenterY"] != System.DBNull.Value) { theRow.Cells["dataGridSOC_Y"].Value = Convert.ToString(rdr["Groove_CenterY"]); }
                        if (rdr["ID"] != System.DBNull.Value) { theRow.Cells["dataGridSOC_ID"].Value = Convert.ToString(rdr["ID"]); }
                        if (rdr["MaterialNO"] != System.DBNull.Value)
                        {
                            string strPlanNO = "";
                            string strOutMatNO = "";
                            string strStrexternalDiameter = "";
                            string strWidth = "";
                            string strGrossWeight = "";
                            string strNetWeight = "";
                            Get_Mat_Out_Info(Convert.ToString(rdr["MaterialNO"]), ref strPlanNO, ref strOutMatNO, ref strStrexternalDiameter, ref strWidth, ref strGrossWeight, ref strNetWeight);
                            theRow.Cells["dataGridSOC_Plan_no"].Value = strPlanNO;
                            theRow.Cells["dataGridSOC_Out_Materialno"].Value = strOutMatNO;
                            theRow.Cells["dataGridSOC_external_diameter"].Value = strStrexternalDiameter;
                            theRow.Cells["dataGridSOC_steelwide"].Value = strWidth;
                            theRow.Cells["dataGridSOC_net_weight"].Value = strNetWeight;
                            theRow.Cells["dataGridSOC_gross_weight"].Value = strGrossWeight;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        public void Get_Mat_Out_Info(string strMatNO, ref string strPlanNO, ref string strOutMatNO, ref string strStrexternalDiameter, ref string strWidth, ref string strGrossWeight, ref string strNetWeight)
        {
            try
            {
                string sqlText = "select  a.plan_no plan_no,b.mat_no mat_no,b.out_mat_no out_mat_no,b.external_diameter external_diameter,b.width width,b.net_weight net_weight,b.gross_weight gross_weight  from UACS_L3PLAN_OUT a, UACS_L3PLANOUT_DETAIL b where a.Plan_No=b.Plan_NO and  (b.Mat_No=" + "'" + strMatNO + "'" + " or b.Out_Mat_NO=" + "'" + strMatNO + "')";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {

                    if (rdr.Read())
                    {
                        strStrexternalDiameter = "0";

                        if (rdr["plan_no"] != System.DBNull.Value) { strPlanNO = Convert.ToString(rdr["plan_no"]); }
                        if (rdr["out_mat_no"] != System.DBNull.Value) { strOutMatNO = Convert.ToString(rdr["out_mat_no"]); }
                        if (rdr["width"] != System.DBNull.Value) { strWidth = Convert.ToString(rdr["width"]); }
                        if (rdr["external_diameter"] != System.DBNull.Value) { strStrexternalDiameter = Convert.ToString(rdr["external_diameter"]); }
                        if (rdr["gross_weight"] != System.DBNull.Value) { strGrossWeight = Convert.ToString(rdr["gross_weight"]); }
                        if (rdr["net_weight"] != System.DBNull.Value) { strNetWeight = Convert.ToString(rdr["net_weight"]); }


                        try
                        {
                            if (strStrexternalDiameter == "0")
                            {
                                long intDia = 0;
                                if (Find_Diameter_InCloiInfo(strMatNO, strOutMatNO, ref intDia) == true)
                                {
                                    strStrexternalDiameter = intDia.ToString();
                                }
                                else
                                {
                                    (Cal_Coil_ExDiameter(Convert.ToInt32(rdr["net_weight"]), Convert.ToInt32(rdr["steelwide"]))).ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
#endregion


#region "配卷功能"
        private void matchCoilProcess()
        {
            try
            {
                string strTreatmentNO = parkingSpaceUtility.Get_TreatmentNO_OnParkingSpace(parkingNO);

                string strSN = parkingSpaceUtility.Get_SN_OnParkingSpace(parkingNO);

                long intMaxCount = parkingSpaceUtility.Get_MAXCount_OfCramera(strTreatmentNO);


                bool FlagConfirmed = parkingSpaceUtility.Get_FlagConfirmed_OfCramera(strTreatmentNO, intMaxCount);

                bool hasCranePlan = parkingSpaceUtility.Has_CranePlan(strTreatmentNO);

                if (FlagConfirmed == false && hasCranePlan == false)
                {
                    blSOC_CanSelectCoils = true;
                }
                else
                {
                    blSOC_CanSelectCoils = false;

                }


                if (blSOC_CanSelectCoils == true)
                {
                    textBox_SOC_CanCreatePlan.Text = "可以操作";
                }
                else
                {
                    textBox_SOC_CanCreatePlan.Text = "不能操作";

                    return;
                }


                Delete_From_CAMERASCAN_DETAIL_AllRecord_Created_By_System(strTreatmentNO);

                ReadLaserData(this.parkingNO);

                Match_Coils();
            }
            catch (Exception ex)
            {
            }
        }

        public void Delete_From_CAMERASCAN_DETAIL_AllRecord_Created_By_System(string strTreatment_No)
        {

            try
            {
                string strSql = " delete from UACS_CAMERASCAN_DETAIL where treatment_no=" + "'" + strTreatment_No + "'" + " and createtime is null";

                DBHelper.ExecuteNonQuery(strSql);

            }
            catch
            {

            }

        }

        const int intYDirectionShouldLeft = 1;
        //
        private void Match_Coils()
        {
            try
            {
                long intWeightSum = 0;

                foreach (DataGridViewRow theRow_GridSCO in dataGridSOC.Rows)
                {

                    bool blFounded = false;
                    foreach (DataGridViewRow theRow_PlanCoils in GridCoilsSCO.Rows)
                    {
                        if (blFounded == false)
                        {
                            string strMatNO = string.Empty;
                            if (theRow_PlanCoils.Cells["GridCoilsSCO_Materialno"].Value != null) { strMatNO = theRow_PlanCoils.Cells["GridCoilsSCO_Materialno"].Value.ToString(); }
                            if (strMatNO != null && strMatNO != string.Empty && blSOC_CanSelectCoils != false)
                            {
                                if (Is_Mat_In_Data_GridSOC(strMatNO) == false && IS_Mat_HasUnit(strMatNO) == true)
                                {
                                    string strPlanNO = string.Empty;
                                    string strOutMatNO = string.Empty;
                                    string strStrexternalDiameter = string.Empty;
                                    string strWidth = string.Empty;
                                    string strGrossWeight = string.Empty;
                                    string strNetWeight = string.Empty;

                                    Get_Mat_Out_Info(strMatNO, ref strPlanNO, ref strOutMatNO, ref strStrexternalDiameter, ref strWidth, ref strGrossWeight, ref strNetWeight);
                                    if (Is_TheCoils_HasCranePlan(strMatNO, strOutMatNO) == false)
                                    {
                                        try
                                        {
                                            int intRowIndex = theRow_GridSCO.Index;
                                            if (intRowIndex >= 1)
                                            {
                                                DataGridViewRow theRow_Before = dataGridSOC.Rows[intRowIndex - 1];
                                                DataGridViewRow theRow_Current = dataGridSOC.Rows[intRowIndex];
                                                int intYDis = Math.Abs(Convert.ToInt32(theRow_Before.Cells["dataGridSOC_Y"].Value) - Convert.ToInt32(theRow_Current.Cells["dataGridSOC_Y"].Value));
                                                int intYLeft = intYDis - Convert.ToInt32(theRow_Before.Cells["dataGridSOC_external_diameter"].Value) / 2 - Convert.ToInt32(strStrexternalDiameter) / 2;
                                                if (intYLeft >= intYDirectionShouldLeft && intWeightSum + Convert.ToInt64(strNetWeight) <= Convert.ToInt64(comboBox_WeightLimt.Text) * 1000)
                                                {
                                                    Set_Data_GridSOC(strMatNO, intRowIndex);
                                                    intWeightSum = intWeightSum + Convert.ToInt64(strNetWeight);
                                                    blFounded = true;
                                                }

                                            }
                                            else
                                            {
                                                Set_Data_GridSOC(strMatNO, intRowIndex);
                                                intWeightSum = intWeightSum + Convert.ToInt64(strNetWeight);
                                                blFounded = true;
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        bool Is_Mat_In_Data_GridSOC(string strMatNO)
        {
            bool isInGrid = false;
            try
            {
                foreach (DataGridViewRow theRow_GridSCO in dataGridSOC.Rows)
                {
                    if (theRow_GridSCO.Cells["dataGridSOC_Materialno"].Value != null)
                    {
                        if (theRow_GridSCO.Cells["dataGridSOC_Materialno"].Value.ToString() != string.Empty)
                        {
                            if (theRow_GridSCO.Cells["dataGridSOC_Materialno"].Value.ToString() == strMatNO)
                            {
                                isInGrid = true;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return isInGrid;
        }


        bool IS_Mat_HasUnit(string strMatNO)
        {
            bool blHasUnit = false;
            try
            {
                foreach (DataGridViewRow theRow in GridCoilsSCO.Rows)
                {
                    if (theRow.Cells["GridCoilsSCO_Materialno"].Value != null)
                    {
                        if (theRow.Cells["GridCoilsSCO_Materialno"].Value.ToString() != string.Empty)
                        {

                            if (theRow.Cells["GridCoilsSCO_Materialno"].Value.ToString() == strMatNO)
                            {
                                if (theRow.Cells["GridCoilsSCO_Big_AreaNo"].Value != null)
                                {
                                    blHasUnit = true;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return blHasUnit;
        }



        public bool Is_TheCoils_HasCranePlan(string strMat_NO, string strMat_Out)
        {
            bool ret = false;
            try
            {
                string sqlText = "SELECT * from UACS_Crane_Plan where  (Up_Column_NO is not null and Up_Row_NO is not null  and down_column_no is null and down_row_no is null) and (materialno=" + "'" + strMat_NO + "'" + " or materialno=" + "'" + strMat_Out + "'" + ")";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return ret;
        }


        void Set_Data_GridSOC(string strMatNO, int intRowIndex)
        {
            try
            {
                foreach (DataGridViewRow theRow_GridCoilsSCO in GridCoilsSCO.Rows)
                {
                    if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_Materialno"].Value != null)
                    {
                        if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_Materialno"].Value.ToString() != string.Empty)
                        {
                            if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_Materialno"].Value.ToString() == strMatNO)
                            {
                                dataGridSOC.Rows[intRowIndex].Cells["dataGridSOC_Plan_no"].Value = theRow_GridCoilsSCO.Cells["GridCoilsSCO_Plan_no"].Value.ToString();
                                dataGridSOC.Rows[intRowIndex].Cells["dataGridSOC_Materialno"].Value = theRow_GridCoilsSCO.Cells["GridCoilsSCO_Materialno"].Value.ToString();
                                if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_Out_Materialno"].Value != null)
                                {
                                    dataGridSOC.Rows[intRowIndex].Cells["dataGridSOC_Out_Materialno"].Value = theRow_GridCoilsSCO.Cells["GridCoilsSCO_Out_Materialno"].Value.ToString();
                                }
                                if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_steelwide"].Value != null)
                                {
                                    dataGridSOC.Rows[intRowIndex].Cells["dataGridSOC_steelwide"].Value = theRow_GridCoilsSCO.Cells["GridCoilsSCO_steelwide"].Value.ToString();
                                }
                                if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_external_diameter"].Value != null)
                                {
                                    dataGridSOC.Rows[intRowIndex].Cells["dataGridSOC_external_diameter"].Value = theRow_GridCoilsSCO.Cells["GridCoilsSCO_external_diameter"].Value.ToString();
                                }
                                if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_net_weight"].Value != null)
                                {
                                    dataGridSOC.Rows[intRowIndex].Cells["dataGridSOC_net_weight"].Value = theRow_GridCoilsSCO.Cells["GridCoilsSCO_net_weight"].Value.ToString();
                                }
                                if (theRow_GridCoilsSCO.Cells["GridCoilsSCO_gross_weight"].Value != null)
                                {
                                    dataGridSOC.Rows[intRowIndex].Cells["dataGridSOC_gross_weight"].Value = theRow_GridCoilsSCO.Cells["GridCoilsSCO_gross_weight"].Value.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
#endregion





    }
}
