using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baosight.iSuperframe.Forms;
using System.Data;

namespace GDITest
{
    public class clsParkingSpaceUtility
    {


        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;

        public static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("2030YLK");
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }

                }
                return dbHelper;
            }
        }

        //读停车位上的处理号
        public string Get_TreatmentNO_OnParkingSpace(string strParking_NO)
        {
            string strTreatmentNO = string.Empty;
            try
            {

                string sqlText = "SELECT * FROM UACS_PARKING_STATUS where PARKING_NO=" + "'" + strParking_NO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["TREATMENT_NO"] != System.DBNull.Value) { strTreatmentNO = Convert.ToString(rdr["TREATMENT_NO"]); }

                    }
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return strTreatmentNO;
        }




        public string Get_SN_OnParkingSpace(string strParking_NO)
        {
            string strSN = string.Empty;
            try
            {

                string sqlText = "SELECT * FROM UACS_PARKING_STATUS where parking_no=" + "'" + strParking_NO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["S_N"] != System.DBNull.Value) { strSN = Convert.ToString(rdr["S_N"]); }

                    }
                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return strSN;
        }



        public string Get_Head_Postion_OnParkingSpace(string strParking_NO)
        {
            string strHead_Postion = string.Empty;
            try
            {

                string sqlText = "SELECT * FROM UACS_PARKING_STATUS where PARKING_NO=" + "'" + strParking_NO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["HEAD_POSTION"] != System.DBNull.Value) { strHead_Postion = Convert.ToString(rdr["HEAD_POSTION"]); }

                    }
                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return strHead_Postion;
        }


        //读停车位上的框架车号
        public string Get_CarNO_OnParkingSpace(string strParking_NO)
        {
            string strCarNO = string.Empty;
            try
            {

                string sqlText = "SELECT * FROM UACS_PARKING_STATUS where PARKING_NO=" + "'" + strParking_NO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["CAR_NO"] != System.DBNull.Value) { strCarNO = Convert.ToString(rdr["CAR_NO"]); }

                    }
                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return strCarNO;
        }


        //读停车位上的框架车号




        public string Get_ISLoaded_OnParkingSpace(string strParking_NO)
        {
            string strISLoaded = string.Empty;
            try
            {

                string sqlText = "SELECT * FROM UACS_PARKING_STATUS where PARKING_NO=" + "'" + strParking_NO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["ISLOADED"] != System.DBNull.Value) { strISLoaded = Convert.ToString(rdr["ISLOADED"]); }

                    }
                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return strISLoaded;
        }



        public long Get_MAXCount_OfCramera(string strTreatmentNO)
        {
            long intMaxCount = 0;
            try
            {

                string sqlText = "SELECT Max(LASER_ACTION_COUNT) LASER_ACTION_COUNT FROM UACS_PARKING_STATUS where TREATMENT_NO=" + "'" + strTreatmentNO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["LASER_ACTION_COUNT"] != System.DBNull.Value) { intMaxCount = Convert.ToInt32(rdr["LASER_ACTION_COUNT"]); }

                    }
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return intMaxCount;
        }



        public bool Get_FlagConfirmed_OfCramera(string strTreatmentNO, long intMaxCount)
        {
            bool blFlag_Confirmed = false;
            try
            {

                string sqlText = "SELECT *  FROM UACS_CAMERA_SCAN where treatment_NO=" + "'" + strTreatmentNO + "'" + " and  SCAN_COUNT=" + intMaxCount.ToString();

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["Flag_Confirmed"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(rdr["Flag_Confirmed"]) == 1)
                            {
                                blFlag_Confirmed = true;
                            }
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return blFlag_Confirmed;
        }



        public bool Has_CranePlan(string strTreatmentNO)
        {
            bool hasCranePlan = false;
            try
            {
                string sqlText = "SELECT *  FROM UACS_CRANE_PLAN where treatment_NO=" + "'" + strTreatmentNO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        hasCranePlan = true;

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return hasCranePlan;
        }

        public void Delete_UACS_CAMERASCAN_DETAIL(string strTreatment_No,long scanCount)
        {

            try
            {
                string strSql = " delete from UACS_LASER_OUT where TREATMENT_NO=" + "'" + strTreatment_No + "'" + " and LASER_ACTION_COUNT=" + scanCount.ToString();

                DBHelper.ExecuteNonQuery(strSql);

            }
            catch
            {

            }

        }
        //,PARKING_NO,CAR_NO
        //,long mparking_no,long mcar_no
        // strSql = strSql + mparking_no.ToString() + ","; strSql = strSql + mcar_no.ToString() + ",";

        public void InsertInto_UACS_CAMERASCAN_DETAIL(string mtreatment_no,
                                      long mscan_count,
                                      long mgroove_centerx,
                                      long mgroove_centery,
                                      long mgroove_centerz)
        {
            try
            {
                string strSql = " Insert into  UACS_LASER_OUT ";
                strSql = strSql + " (TREATMENT_NO,LASER_ACTION_COUNT,GROOVE_ACT_X,GROOVE_ACT_Y,GROOVE_ACT_Z,TIME_CREATED)";
                strSql = strSql + "  Values ";
                strSql = strSql + "  ( ";
                strSql = strSql + "'"+mtreatment_no + "'"+",";
                strSql = strSql + mscan_count.ToString()+ ",";
                strSql = strSql + mgroove_centerx.ToString() + ",";
                strSql = strSql + mgroove_centery.ToString() + ",";
                strSql = strSql + mgroove_centerz.ToString() + ","; 
                strSql = strSql + "sysdate ";
                strSql = strSql + "  ) ";
                DBHelper.ExecuteNonQuery(strSql);
            }
            catch (Exception ex)
            {
            }
        }


        public string DateStart_ToString(DateTime theDateTime)
        {
            string strDateTime = "";
            try
            {
                strDateTime = theDateTime.Year.ToString("0000") + theDateTime.Month.ToString("00") + theDateTime.Day.ToString("00") + "000000";
                strDateTime = "to_date('" + strDateTime + "','YYYYMMDDHH24MISS')";
            }
            catch (Exception ex)
            {
            }
            return strDateTime;
        }


        public string DateEnd_ToString(DateTime theDateTime)
        {
            string strDateTime = "";
            try
            {
                strDateTime = theDateTime.Year.ToString("0000") + theDateTime.Month.ToString("00") + theDateTime.Day.ToString("00") + "235959";
                strDateTime = "to_date('" + strDateTime + "','YYYYMMDDHH24MISS')";
            }
            catch (Exception ex)
            {
            }
            return strDateTime;
        }

    }
}
