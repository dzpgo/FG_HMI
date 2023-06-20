using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSPopupForm
{
    public partial class PopAlarmCurrent : Form
    {
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

        private string[] arrTagAdress;

        private string crane_No = null;

        private string TagName = null;
        private string TagNameWMS = null;

        private string TagName_FaultCode = null;

        /// <summary>
        /// 存解析出来的报警代码
        /// </summary>
        private List<int> listAlarm = new List<int>();
        /// <summary>
        /// 行车号
        /// </summary>
        public string Crane_No
        {
            get { return crane_No; }
            set
            {
                crane_No = value;
            }
        }
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

        public PopAlarmCurrent()
        {
            InitializeComponent();
            this.Load += PopAlarmCurrent_Load;
        }

        void PopAlarmCurrent_Load(object sender, EventArgs e)
        {
            if (Crane_No != null)
            {
                tagDataProvider.ServiceName = "iplature";
                lblCraneNo.Text = Crane_No + " 报警";
                listAlarm.Clear();
                for (int i = 0; i <= 19; i++)
                {
                    TagName_FaultCode = Crane_No + "_FaultCode_" + "" + i + "";
                    SetReady(TagName_FaultCode);
                    readTags();
                    string data = get_value_string(TagName_FaultCode).Trim();
                    if (!string.IsNullOrEmpty(data) && !data.Equals("0"))
                        listAlarm.Add(Convert.ToInt32(data));
                }
                GetDgvMessage(listAlarm);

                #region 弃用（旧）
                //tagDataProvider.ServiceName = "iplature";
                //TagName = Crane_No + "_ALARM_CURRENT";
                //TagNameWMS = Crane_No + "_WMS_ALARM_CURRENT";
                //lblCraneNo.Text = Crane_No + " 报警";
                //SetReady(TagName);
                //readTags();
                //string value = get_value_string(TagName).Trim();

                //SetReady(TagNameWMS);
                //readTags();
                //string valueWMS = get_value_string(TagNameWMS).Trim();

                //if (String.IsNullOrEmpty(value.Trim()) && String.IsNullOrEmpty(valueWMS.Trim()))
                //{
                //    //MessageBox.Show("无行车报警");
                //    return;
                //}

                //string[] sArray = value.Split(',');
                //string[] sArrayWMS = valueWMS.Split(',');
                //listAlarm.Clear();
                ////foreach (string i in sArray)
                ////{
                ////    int values = Convert.ToInt32(i.ToString());
                ////    listAlarm.Add(values);
                ////}

                //if (!String.IsNullOrEmpty(value.Trim()))
                //{
                //    foreach (string i in sArray)
                //    {
                //        int values = Convert.ToInt32(i.ToString());
                //        listAlarm.Add(values);
                //    }
                //}
                //if (!String.IsNullOrEmpty(valueWMS.Trim()))
                //{
                //    foreach (string i in sArrayWMS)
                //    {
                //        int valuesWMS = Convert.ToInt32(i.ToString());
                //        listAlarm.Add(valuesWMS);
                //    }
                //}
                //GetDgvMessage(listAlarm); 
                #endregion

            }

        }


        private void GetDgvMessage(List<int> list)
        {
            DataTable dt = new DataTable();
            bool hasSetColumn = false;

            try
            {
                string sql = @"SELECT ALARM_CODE,ALARM_INFO,ALARM_CLASS FROM UACS_CRANE_ALARM_CODE_DEFINE ";
                sql += " WHERE ALARM_CODE IN (";
                foreach (int item in list)
                {
                    sql += "" + item + ",";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ");";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt.Columns.Add(dc);
                            }

                            dr[i] = rdr[i];
                        }

                        hasSetColumn = true;
                        dt.Rows.Add(dr);
                    }
                    while (!rdr.Read())
                    {
                        DataRow da = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt.Columns.Add(dc);
                            }

                            da[i] = rdr[i];
                        }

                        hasSetColumn = true;
                        dt.Rows.Add(da);
                    }
                }
                if (hasSetColumn == false)
                {
                    dt.Columns.Add("ALARM_CODE", typeof(String));
                    dt.Columns.Add("ALARM_INFO", typeof(String));
                    dt.Columns.Add("ALARM_CLASS", typeof(String));
                }

            }
            catch (Exception er)
            {

            }

            dataGridView1.DataSource = dt;
        }


        #region read tag

        public void SetReady(string tagName)
        {
            try
            {
                List<string> lstAdress = new List<string>();
                lstAdress.Add(tagName);
                arrTagAdress = lstAdress.ToArray<string>();
            }
            catch (Exception er)
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
            return theValue; ;
        }
        #endregion

        private void PopAlarmCurrent_FormClosed(object sender, FormClosedEventArgs e)
        {
            inDatas[Crane_No + "_WMS_ALARM_CURRENT"] = "";
            tagDataProvider.Write2Level1(inDatas, 1);
        }
    }
}
