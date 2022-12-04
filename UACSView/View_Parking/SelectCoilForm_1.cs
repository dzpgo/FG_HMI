using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;

using Baosight.iSuperframe.TagService;
using ParkingControlLibrary;
using ParkClassLibrary;

namespace UACSParking
{
    public delegate void TransferValue(string weight, bool isStowage);
    public partial class SelectCoilForm : Form
    {
        public event TransferValue TransferValue;
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        DataTable dt = new DataTable();
        DataTable dt_selected = new DataTable();
        bool hasSetColumn;
        string bayNO;
        string parkNO;
        string carNO;
        //添加材料个数
        //int count = 0;
        int coilsWeight = 0;   //添加材料重量
        int coilWidth = 0;
        //int coilsDistance = 0;  //半径距离
        //int x_coil1 = 0;
        //int x_coil2 = 0;

        string carType;

        public string CarType
        {
            get { return carType; }
            set
            {
                carType = value;
                this.Text = string.Format("{0}材料选择", carType);
            }
        }

        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public string CarNO
        {
            get { return carNO; }
            set { carNO = value; }
        }

        public string ParkNO
        {
            get { return parkNO; }
            set { parkNO = value; }
        }

        private string grooveTotal;
        public string GrooveTotal
        {
            get { return grooveTotal; }
            set { grooveTotal = value; }
        }

        private string grooveNum;
        public string GrooveNum
        {
            get { return grooveNum; }
            set { grooveNum = value; }
        }
        public SelectCoilForm()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("2030YLK");

            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            //TagValues.Add("EV_NEW_PARKING_CARLEAVE", null);
            //社会车出库
            TagValues.Add("EV_NEW_PARKING_MDL_OUT_CAL_JUDGE", null);
            //框架车出库
            TagValues.Add("EV_PARKING_MDL_OUT_CAL_START", null);
            tagDP.Attach(TagValues);

            //初始化dataGridview属性
            DataGridViewInit(dataGridView1);
            dataGridView1.AutoGenerateColumns = false;
            DataGridViewInit(dataGridView2);
            //dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.RowTemplate.Height = 40;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            this.Text = string.Format("{0}材料选择", carType);
        }

        private void SelectCoilForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(242, 246, 252);         
            //判断跨别
            judgeBayNo(parkNO);
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dt_selected.Columns.Add(dataGridView2.Columns[i].Name);
            }
            //加载扫描数据
            RefreshHMILaserOutData();
            //加载材料信息
            BindMatStock(parkNO);
            dataGridViewColor(dataGridView1);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string truckNo = carNO;
            string parkingNo = parkNO;
            try
            {
                //框架车号不能为空
                if (truckNo == "")
                {
                    MessageBox.Show("车号不能为空");
                    return;
                }
                //车位号不能为空
                if (parkingNo == "" || parkingNo == "请选择")
                {
                    MessageBox.Show("该车找不到对应的停车位号");
                    return;
                }

                bool flag = false;
                foreach (DataGridViewRow dgvRows in dataGridView2.Rows)
                {

                    if (dgvRows.Cells["COIL_NO2"].Value.ToString() != "")
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    MessageBox.Show("请选择钢卷！");
                    return;
                }

                #region    社会车卷径干涉判断  材料号输入0不判断
                if ((carType == "101" || carType == "103" || carType == "200") && txtGetMat.Text.Trim() != "0")                    //开关
                {
                    //检查社会车辆两个可见光落点间距是否大于彼此材料半径加上安全距离（防止碰撞）
                    ////先获取车头方向配置表里的车长方向坐标轴
                    //string AXES_CAR_LENGTH = "";
                    //string TREND_TO_TAIL = "";
                    //string sqlText_head = @"SELECT AXES_CAR_LENGTH, TREND_TO_TAIL FROM UACS_HEAD_POSITION_CONFIG WHERE HEAD_POSTION IN ";
                    //sqlText_head += "(SELECT HEAD_POSTION FROM UACS_PARKING_STATUS WHERE PARKING_NO = '{0}') AND PARKING_NO = '{0}'";
                    //sqlText_head = string.Format(sqlText_head, parkingNo);
                    //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_head))
                    //{
                    //    if (rdr.Read())
                    //    {
                    //        AXES_CAR_LENGTH = rdr["AXES_CAR_LENGTH"].ToString();
                    //        TREND_TO_TAIL = rdr["TREND_TO_TAIL"].ToString();
                    //    }
                    //}

                    int GROOVE_ACT_X1 = 0;
                    int GROOVE_ACT_Y1 = 0;
                    int GROOVE_ACT_X2 = 0;
                    int GROOVE_ACT_Y2 = 0;
                    string MAT_NO1 = "";
                    string MAT_NO2 = "";
                    int OUTDIA1 = 0;
                    int OUTDIA2 = 0;
                    //string sqlText_outdia = "";
                    const int safeDistance = 100;  //安全距离100mm
                    //卷径干涉判断  社会车

                    for (int j = 0; j < this.dataGridView2.Rows.Count - 1; j++)
                    {
                        GROOVE_ACT_X1 = Convert.ToInt32(this.dataGridView2.Rows[j].Cells["GROOVE_ACT_X"].Value.ToString());  //槽中心点X1
                        GROOVE_ACT_Y1 = Convert.ToInt32(this.dataGridView2.Rows[j].Cells["GROOVE_ACT_Y"].Value.ToString());  //槽中心点Y1
                        MAT_NO1 = this.dataGridView2.Rows[j].Cells["COIL_NO2"].Value.ToString();                             //材料号1
                        if (MAT_NO1.Length > 8)
                        {
                            OUTDIA1 = Convert.ToInt32(this.dataGridView2.Rows[j].Cells["OUTDIA2"].Value.ToString()) / 2;  //钢卷半径
                        }
                        else
                        {
                            OUTDIA1 = 0;
                        }
                        GROOVE_ACT_X2 = Convert.ToInt32(this.dataGridView2.Rows[j + 1].Cells["GROOVE_ACT_X"].Value.ToString());  //槽中心点X2
                        GROOVE_ACT_Y2 = Convert.ToInt32(this.dataGridView2.Rows[j + 1].Cells["GROOVE_ACT_Y"].Value.ToString());  //槽中心点Y2
                        MAT_NO2 = this.dataGridView2.Rows[j + 1].Cells["COIL_NO2"].Value.ToString();                             //材料号2
                        if (MAT_NO2.Length > 8)
                        {
                            OUTDIA2 = Convert.ToInt32(this.dataGridView2.Rows[j + 1].Cells["OUTDIA2"].Value.ToString()) / 2;  //钢卷半径
                        }
                        else
                        {
                            OUTDIA2 = 0;
                        }
                        ////获取材料号1的外径
                        //sqlText_outdia = @"SELECT OUTDIA FROM UACS_YARDMAP_COIL WHERE COIL_NO = '{0}'";
                        //sqlText_outdia = string.Format(sqlText_outdia, MAT_NO1);
                        //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_outdia))
                        //{
                        //    if (rdr.Read())
                        //    {
                        //        OUTDIA1 = (int)rdr["OUTDIA"];
                        //        OUTDIA1 = OUTDIA1 / 2;
                        //    }
                        //}

                        ////获取材料号2的外径
                        //sqlText_outdia = @"SELECT OUTDIA FROM UACS_YARDMAP_COIL WHERE COIL_NO = '{0}'";
                        //sqlText_outdia = string.Format(sqlText_outdia, MAT_NO2);
                        //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_outdia))
                        //{
                        //    if (rdr.Read())
                        //    {
                        //        OUTDIA2 = (int)rdr["OUTDIA"];
                        //        OUTDIA2 = OUTDIA2 / 2;
                        //    }
                        //}

                        //if (AXES_CAR_LENGTH == "X")
                        //{
                        //   // int dist = GROOVE_ACT_X2 - GROOVE_ACT_X1 - OUTDIA1 - OUTDIA2;
                        //    int dist = System.Math.Abs(GROOVE_ACT_X1 - GROOVE_ACT_X2) - (OUTDIA1 + OUTDIA2);
                        //    if (dist <= safeDistance)
                        //    {
                        //        DialogResult dr = MessageBox.Show(string.Format("{0}槽与{1}钢卷之间距离: {2}mm小于安全距离:{3}mm，继续可能存在危险！",j,j+1, dist, safeDistance), "警告", MessageBoxButtons.YesNo);
                        //        if (dr == DialogResult.Yes)
                        //        {

                        //        }
                        //        else if (dr == DialogResult.No)
                        //        {
                        //            return;
                        //        }
                        //    }
                        //}
                        //else if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "DES")
                        //{
                        //    //目前暂定安全距离10，后续待调整改为可配置
                        //    int dist = GROOVE_ACT_X1 - GROOVE_ACT_X2 - OUTDIA1 - OUTDIA2;
                        //    if (dist <= safeDistance)
                        //    {
                        //        //MessageBox.Show("落点钢卷之间距离小于安全距离，请重新选择配载材料！");
                        //        DialogResult dr = MessageBox.Show(string.Format("{0}槽与{1}钢卷之间距离: {2}mm小于安全距离:{3}mm，继续可能存在危险！", j, j + 1, dist, safeDistance), "警告", MessageBoxButtons.YesNo);
                        //        if (dr == DialogResult.Yes)
                        //        {

                        //        }
                        //        else if (dr == DialogResult.No)
                        //        {
                        //            return;
                        //        }
                        //    }
                        //}
                        //else if (AXES_CAR_LENGTH == "Y")
                        if (parkingNo.Contains("Z"))  //成品库库位类型
                        {
                            //int dist = GROOVE_ACT_Y2 - GROOVE_ACT_Y1 - OUTDIA1 - OUTDIA2;
                            int dist = System.Math.Abs(GROOVE_ACT_Y1 - GROOVE_ACT_Y2) - (OUTDIA1 + OUTDIA2);
                            if (dist <= safeDistance)
                            {
                                MessageBox.Show(string.Format("{0}槽与{1}槽的钢卷距离: {2}mm小于安全距离:{3}mm，可能存在危险，请重新选卷提交！", j, j + 1, dist, safeDistance), "警告");
                                return;
                                //DialogResult dr = MessageBox.Show(string.Format("{0}槽与{1}槽的钢卷距离: {2}mm小于安全距离:{3}mm，是否继续，继续可能存在危险！", j, j + 1, dist, safeDistance), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                //if (dr != DialogResult.Yes)
                                //{
                                //    return;
                                //}

                            }
                        }
                        //else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "DES")
                        //{
                        //    int  dist = GROOVE_ACT_Y1 - GROOVE_ACT_Y2 - OUTDIA1 - OUTDIA2;
                        //    if (dist <= safeDistance)
                        //    {
                        //        //MessageBox.Show("落点钢卷之间距离小于安全距离，请重新选择配载材料！");
                        //        DialogResult dr = MessageBox.Show(string.Format("{0}槽与{1}钢卷之间距离: {2}mm小于安全距离:{3}mm，继续可能存在危险！", j, j + 1, dist, safeDistance), "警告", MessageBoxButtons.YesNo);
                        //        if (dr == DialogResult.Yes)
                        //        {

                        //        }
                        //        else if (dr == DialogResult.No)
                        //        {
                        //            return;
                        //        }
                        //    }
                        //}
                    }
                }
                #endregion

                #region 钢卷多库位判断
                string temp;
                if (checkMatNOCount(out temp))
                {
                    //DialogResult dr = MessageBox.Show(string.Format("所选钢卷存在多库位或者卷信息有误：\r\n{0}！继续请先确认钢卷信息。", temp), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    MessageBox.Show("所选钢卷存在多库位或者卷信息有误：\r\n{0}！继续请先确认钢卷信息。");
                    //if (dr != System.Windows.Forms.DialogResult.Yes)
                    //{
                    return;
                    //}
                }


                #endregion

                string myValue = "";
                //停车位号|CaoNO|处理号|模型计算次数|配载图ID-卷|卷
                string treatmentNo = "";
                string stowageNo = "";
                int currengMdlCalId = 0;
                long LASER_ACTION_COUNT = 0;
                string sqlText = @"SELECT TREATMENT_NO, STOWAGE_ID, MDL_CAL_ID, LASER_ACTION_COUNT FROM UACS_PARKING_STATUS where PARKING_NO = '{0}'";
                sqlText = string.Format(sqlText, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        treatmentNo = rdr["TREATMENT_NO"].ToString();
                        LASER_ACTION_COUNT = Convert.ToInt64(rdr["LASER_ACTION_COUNT"].ToString());

                        stowageNo = rdr["STOWAGE_ID"].ToString();
                        if (rdr["MDL_CAL_ID"] != DBNull.Value)
                        {
                            currengMdlCalId = (int)rdr["MDL_CAL_ID"];
                        }
                    }
                }
                if (carType == "101" || carType == "103" || carType == "200")
                {
                    // 检查画面选定数据与后台当前数据是否一致
                    if (!dataGridView2.Rows[0].Cells["GROOVEID"].Value.ToString().Equals(""))
                    {
                        if (!CheckWithLaserOutData(treatmentNo, LASER_ACTION_COUNT))
                        {
                            RefreshHMILaserOutData();
                            MessageBox.Show("数据已发生修改，画面刷新！请重新选择材料");
                            txtCoilsWeight.Text = "";
                            txtCoilsWeight.BackColor = Color.White;
                            return;
                        }
                    }

                }
                else if (carType == "100")
                {

                }

                //模型计算次数
                int mdlCalId = currengMdlCalId + 1;
                myValue = string.Format("{0}|{1}|{2}|{3}|{4}-", parkingNo, truckNo, treatmentNo, mdlCalId, stowageNo);
                //MessageBox.Show(dt_selected.Rows.Count.ToString());
                for (int i = 0; i < dt_selected.Rows.Count; i++)
                {
                    if (i < 30)
                    {
                        myValue += dt_selected.Rows[i]["COIL_NO2"].ToString();
                        myValue += "|";
                    }
                }
                //debug
                //richTextBoxDebug.Text += string.Format("发送的Tag的myValue 的值：\n{0}\n", myValue);
                //DialogResult dr = MessageBox.Show(string.Format("发送的Tag的myValue 的值：\n{0}\n", myValue), "提示", MessageBoxButtons.YesNo);
                //if (dr == DialogResult.Yes)
                //{
                //    //this.Close();
                //    //return;
                //}
                //else if (dr == DialogResult.No)
                //{
                //    return;
                //}
                //debug


                //更新社会车辆中间选卷数据到配载图的选卷完成中间数据里
                string shehuicheValue = "";
                for (int i = 0; i < dt_selected.Rows.Count; i++)
                {
                    if (i < 30)
                    {
                        string coilNO = dt_selected.Rows[i]["COIL_NO2"].ToString().Trim();
                        if (coilNO.Length != 0)
                        {
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_X"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_Y"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["COIL_NO2"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_Z"].ToString();
                            shehuicheValue += "|";
                            if (dt_selected.Rows[i]["GROOVEID"].ToString() == "")
                            {
                                shehuicheValue += i + 1;
                            }
                            else
                            {
                                shehuicheValue += dt_selected.Rows[i]["GROOVEID"].ToString();
                            }
                            shehuicheValue += "-";
                        }
                    }
                }

                sqlText = @"UPDATE UACS_TRUCK_STOWAGE SET MD_COIL_READY = '{0}' WHERE STOWAGE_ID = {1} ";
                sqlText = string.Format(sqlText, shehuicheValue, stowageNo);
                DBHelper.ExecuteNonQuery(sqlText);

                //木架卷更新UACS_YARDMAP_COIL_PLASTIC
                if (carType == "200")
                {
                    for (int i = 0; i < dt_selected.Rows.Count; i++)
                    {
                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string coilNO = dt_selected.Rows[i]["COIL_NO2"].ToString().Trim();
                        string sqlSelect = @"SELECT COIL_NO FROM UACS_YARDMAP_COIL_SPECIAL WHERE COIL_NO = '" + coilNO + "'";
                        using (IDataReader rdr = DBHelper.ExecuteReader(sqlSelect))
                        {
                            if (rdr.Read())
                            {
                                string sqlUpdate = @"UPDATE UACS_YARDMAP_COIL_SPECIAL SET WOODEN_FLAG = 1,UP_TIME = '{0}' WHERE COIL_NO = '{1}'";
                                sqlUpdate = string.Format(sqlUpdate, time, coilNO);
                                DBHelper.ExecuteNonQuery(sqlUpdate);
                            }
                            else
                            {
                                string sqlInsert = @"INSERT INTO UACS_YARDMAP_COIL_SPECIAL (COIL_NO, WOODEN_FLAG, UP_TIME) VALUES ('{0}', 1, '{1}')";
                                sqlInsert = string.Format(sqlInsert, coilNO, time);
                                DBHelper.ExecuteNonQuery(sqlInsert);
                            }
                        }
                    }
                }

                //发送tag
                myValue = myValue.Substring(0, myValue.Length - 1);

                if (carType == "101" || carType == "103" || carType == "200")
                {
                    tagDP.SetData("EV_NEW_PARKING_MDL_OUT_CAL_JUDGE", myValue);
                }
                else if (carType == "100" || carType == "102" || carType == "106")
                {
                    tagDP.SetData("EV_PARKING_MDL_OUT_CAL_START", myValue);
                }
                //tagDP.SetData("EV_NEW_PARKING_MDL_OUT_CAL_JUDGE", myValue);

                //更新模型计算次数
                sqlText = @"UPDATE UACS_PARKING_STATUS SET MDL_CAL_ID = {0} where PARKING_NO = '{1}'";
                sqlText = string.Format(sqlText, mdlCalId, parkingNo);
                DBHelper.ExecuteNonQuery(sqlText);
                if (carType == "101" || carType == "103")
                {
                    TransferValue(coilsWeight.ToString(), true);
                }

                MessageBox.Show("材料选择成功，自动行车准备作业，请注意安全！");
                this.Close();
            }
            catch (Exception er)
            {
                TransferValue(coilsWeight.ToString(), false);
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
                MessageBox.Show("车辆选择材料失败！");
            }


        }

        //木架卷激光
        private bool ResetWoodenCoilLaserOutInfo(string parkingNo, long LASER_ACTION_COUNT)
        {
            bool ret = true;
            try
            {
                //先获取车头方向配置表里的车长方向坐标轴和趋势
                string TREATMENT_NO = "";
                string AXES_CAR_LENGTH = "";
                string TREND_TO_TAIL = "";
                string sqlText_head = @"SELECT AXES_CAR_LENGTH, TREND_TO_TAIL FROM UACS_HEAD_POSITION_CONFIG WHERE HEAD_POSTION IN ";
                sqlText_head += "(SELECT HEAD_POSTION FROM UACS_PARKING_STATUS WHERE PARKING_NO = '{0}') AND PARKING_NO = '{0}'";
                sqlText_head = string.Format(sqlText_head, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_head))
                {
                    if (rdr.Read())
                    {
                        AXES_CAR_LENGTH = rdr["AXES_CAR_LENGTH"].ToString();
                        TREND_TO_TAIL = rdr["TREND_TO_TAIL"].ToString();
                    }
                }

                string sqlorder = "";
                if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "INC")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_X ";
                }
                else if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "DES")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_X DESC";
                }
                else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "INC")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_Y ";
                }
                else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "DES")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_Y DESC";
                }

                //从出库激光表里取出激光扫描数据
                string sqlText = @"SELECT GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z, GROOVEID FROM UACS_LASER_OUT ";
                sqlText += "WHERE TREATMENT_NO = '{0}' AND LASER_ACTION_COUNT = '{1}' ";
                sqlText += sqlorder;
                sqlText = string.Format(sqlText, TREATMENT_NO, LASER_ACTION_COUNT);

                string GROOVE_ACT_X_H = "";
                string GROOVE_ACT_Y_H = "";
                string GROOVE_ACT_Z_H = "";
                string GROOVEID_H = "";
                string GROOVE_ACT_X_T = "";
                string GROOVE_ACT_Y_T = "";
                string GROOVE_ACT_Z_T = "";
                string GROOVEID_T = "";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    int count = 0;
                    while (rdr.Read())
                    {
                        if (count == 0)
                        {
                            GROOVE_ACT_X_H = rdr["GROOVE_ACT_X"].ToString();
                            GROOVE_ACT_Y_H = rdr["GROOVE_ACT_Y"].ToString();
                            GROOVE_ACT_Z_H = rdr["GROOVE_ACT_Z"].ToString();
                            GROOVEID_H = rdr["GROOVEID"].ToString();
                        }
                        else
                        {
                            GROOVE_ACT_X_T = rdr["GROOVE_ACT_X"].ToString();
                            GROOVE_ACT_Y_T = rdr["GROOVE_ACT_Y"].ToString();
                            GROOVE_ACT_Z_T = rdr["GROOVE_ACT_Z"].ToString();
                            GROOVEID_T = rdr["GROOVEID"].ToString();
                        }
                        count++;
                    }
                }

                //1、先把第一个卷 和头部激光数据 匹配写入
                //2、拿第一个和第二个钢卷外径加（100） 由趋势 推算第二个槽位置
                //3、重复
                //4、最后一个的特殊处理

                string shehuicheValue = "";
                for (int i = 0; i < dt_selected.Rows.Count; i++)
                {
                    if (i < 30)
                    {
                        string coilNO = dt_selected.Rows[i]["COIL_NO2"].ToString().Trim();
                        if (coilNO.Length != 0)
                        {
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_X"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_Y"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["COIL_NO2"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_Z"].ToString();
                            shehuicheValue += "|";
                            if (dt_selected.Rows[i]["GROOVEID"].ToString() == "")
                            {
                                shehuicheValue += i + 1;
                            }
                            else
                            {
                                shehuicheValue += dt_selected.Rows[i]["GROOVEID"].ToString();
                            }
                            shehuicheValue += "-";
                        }
                    }
                }

                sqlText = @"UPDATE UACS_TRUCK_STOWAGE SET MD_COIL_READY = '{0}' WHERE STOWAGE_ID = {1} ";
                //sqlText = string.Format(sqlText, shehuicheValue, stowageNo);
                DBHelper.ExecuteNonQuery(sqlText);

            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
                return ret;
            }
            return ret;
        }

        private string GetParkType(string stowageID)
        {
            string ret = "";

            try
            {
                string sql = "select CAR_TYPE from UACS_TRUCK_STOWAGE where STOWAGE_ID= '" + stowageID + "' ";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        ret = rdr["STOWAGE_ID"].ToString();

                    }
                }
                return ret;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
                return ret;
            }
        }

        /// <summary>
        /// 绑定材料位置信息
        /// </summary>
        private void BindMatStock(string packing, string planNo = "")
        {
            if (!packing.Contains('Z') || packing.Trim() == "")
            {
                return;
            }
            dt.Clear();

            string sqlText_All = @"  SELECT 0 AS CHECK_COLUMN, C.MAT_NO AS COIL_NO, A.LOT_NO as LOT_NO,  C.BAY_NO, C.STOCK_NO, B.WEIGHT, B.WIDTH, B.INDIA, B.OUTDIA, B.PACK_CODE,";
            sqlText_All += "    D.X_CENTER, D.Y_CENTER, C.Z_CENTER ,";
            sqlText_All += " B.ACT_WEIGHT, B.ACT_WIDTH FROM UACS_YARDMAP_STOCK_DEFINE C ";
            sqlText_All += " LEFT JOIN UACS_YARDMAP_COIL B ON C.MAT_NO = B.COIL_NO ";
            sqlText_All += " LEFT JOIN  UACS_PLAN_L3PICK A ON C.MAT_NO = A.COIL_NO ";
            sqlText_All += " LEFT JOIN  UACS_YARDMAP_SADDLE_STOCK E ON C.STOCK_NO = E.STOCK_NO ";
            sqlText_All += " LEFT JOIN  UACS_YARDMAP_SADDLE_DEFINE D  ON E.SADDLE_NO = D.SADDLE_NO ";
            if (packing.Contains("Z"))
            {
                sqlText_All += " WHERE C.BAY_NO like '" + packing.Substring(0, 3) + "%' ";
            }
            else
            {
                //sqlText_All += "WHERE C.BAY_NO = '" + (packing.Substring(0, 3) == "Z07" ? "Z22" : "Z21") + "'";
            }

            if (carType == "200")
            {
                if (packing.Substring(0, 3) == "Z08" || packing.Substring(0, 3) == "Z21")
                {
                    sqlText_All += " AND (D.COL_ROW_NO = 'Z21-C-29' OR D.COL_ROW_NO = 'Z21-C-30' OR D.COL_ROW_NO = 'Z21-C-31' OR D.COL_ROW_NO = 'Z21-C-32') ";
                }
                else if (packing.Substring(0, 3) == "Z07" || packing.Substring(0, 3) == "Z22")
                {
                    sqlText_All += " AND (D.COL_ROW_NO = 'Z22-B-9' OR D.COL_ROW_NO = 'Z22-B-10' OR D.COL_ROW_NO = 'Z22-B-11') ";
                    sqlText_All += " AND (D.COL_NO >= 1 AND D.COL_NO <= 6) ";
                }
                else if (packing.Substring(0, 3) == "Z23")
                {
                    //sqlText_All += " AND (D.COL_ROW_NO = 'Z23-G-72' OR D.COL_ROW_NO = 'Z23-G-73' OR D.COL_ROW_NO = 'Z23-G-74') ";
                    sqlText_All += " AND (D.COL_ROW_NO = '%Z23-G%') ";
                }
                sqlText_All += " AND (B.PACK_CODE = 'P2' OR B.PACK_CODE = '2P' OR B.PACK_CODE = 'p2' OR B.PACK_CODE = '2p' OR B.PACK_CODE = 'W4' OR B.PACK_CODE = '4W' OR B.PACK_CODE = 'w4' OR B.PACK_CODE = '4w' OR B.PACK_CODE = 'X2' OR B.PACK_CODE = '2X' OR B.PACK_CODE = 'x2' OR B.PACK_CODE = '2x') ";
            }

            sqlText_All += " AND C.STOCK_STATUS = 2 AND C.LOCK_FLAG = 0 AND C.MAT_NO IS NOT NULL  ";

            if (planNo == "")
            {

            }
            else if (planNo.Trim().Length > 3)
            {
                sqlText_All += " AND A.LOT_NO  like '" + "%" + planNo + "%' ";
            }
            sqlText_All += " order by C.STOCK_NO DESC ";
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_All))
            {
                while (rdr.Read())
                {
                    if (!hasSetColumn)
                    {
                        setDataColumn(dt, rdr);
                    }
                    hasSetColumn = true;
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        dr[i] = rdr[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            this.dataGridView1.DataSource = dt;
            //dataGridView1.Columns["ACT_WEIGHT"].Visible = false;
            //dataGridView1.Columns["ACT_WIDTH"].Visible = false;
        }
        /// <summary>
        /// 设置table的列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rdr"></param>
        private void setDataColumn(DataTable dt, IDataReader rdr)
        {
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = rdr.GetName(i);
                dt.Columns.Add(dc);
            }
            //

        }

        /// <summary>
        /// 用于DataGridView初始化一般属性
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static string DataGridViewInit(DataGridView dataGridView)
        {
            // dataGridView.ReadOnly = true;
            foreach (DataGridViewColumn c in dataGridView.Columns)
                if (c.Index != 0) c.ReadOnly = true;
            //列标题属性
            dataGridView.AutoGenerateColumns = false;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;//标题背景颜色
            //设置列高
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView.ColumnHeadersHeight = 35;
            //设置标题内容居中显示;  
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //设置行属性
            //dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;  //隐藏行标题
            //禁止用户改变DataGridView1所有行的行高  
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowTemplate.Height = 30;

            dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            return "";
        }

        /// <summary>
        /// 激光扫描数据
        /// </summary>
        /// <returns></returns>
        private bool RefreshHMILaserOutData()
        {
            bool bResut = false;
            try
            {
                string parkingNo = "";
                string TREATMENT_NO = "";
                long LASER_ACTION_COUNT = 0;

                // 读取车牌数据
                string truckNo = carNO;      //车号
                if (truckNo == "")
                {
                    return bResut;
                }

                // 车号对应的停车位数据
                //string sqlText = @"SELECT PARKING_NO FROM UACS_PARKING_STATUS WHERE CAR_NO = '{0}'";
                //sqlText = string.Format(sqlText, truckNo);
                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                //{
                //    if (rdr.Read())
                //    {
                //        parkingNo = rdr["PARKING_NO"].ToString();
                //    }
                //}
                // this.lbParkingNo.Text = parkingNo;
                parkingNo = parkNO;
                //先获取车头方向配置表里的车长方向坐标轴和趋势
                string AXES_CAR_LENGTH = "";
                string TREND_TO_TAIL = "";
                string sqlText_head = @"SELECT AXES_CAR_LENGTH, TREND_TO_TAIL FROM UACS_HEAD_POSITION_CONFIG WHERE HEAD_POSTION IN ";
                sqlText_head += "(SELECT HEAD_POSTION FROM UACS_PARKING_STATUS WHERE PARKING_NO = '{0}') AND PARKING_NO = '{0}'";
                sqlText_head = string.Format(sqlText_head, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_head))
                {
                    if (rdr.Read())
                    {
                        AXES_CAR_LENGTH = rdr["AXES_CAR_LENGTH"].ToString();
                        TREND_TO_TAIL = rdr["TREND_TO_TAIL"].ToString();
                    }
                }

                string sqlorder = "";
                if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "INC")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_X ";
                }
                else if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "DES")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_X DESC";
                }
                else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "INC")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_Y ";
                }
                else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "DES")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_Y DESC";
                }

                //从停车位表里取出处理号和激光扫描次数
                string sqlText = @"SELECT TREATMENT_NO, LASER_ACTION_COUNT FROM UACS_PARKING_STATUS WHERE PARKING_NO='{0}' ";
                sqlText = string.Format(sqlText, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        TREATMENT_NO = rdr["TREATMENT_NO"].ToString();
                        LASER_ACTION_COUNT = Convert.ToInt64(rdr["LASER_ACTION_COUNT"].ToString());
                    }
                }

                string GROOVE_ACT_X = "";
                string GROOVE_ACT_Y = "";
                string GROOVE_ACT_Z = "";
                string GROOVEID = "";
                dt_selected.Clear();

                //从出库激光表里取出激光扫描数据
                sqlText = @"SELECT GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z, GROOVEID FROM UACS_LASER_OUT ";
                sqlText += "WHERE TREATMENT_NO = '{0}' AND LASER_ACTION_COUNT = '{1}' ";
                sqlText += sqlorder;
                sqlText = string.Format(sqlText, TREATMENT_NO, LASER_ACTION_COUNT);

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        GROOVE_ACT_X = rdr["GROOVE_ACT_X"].ToString();
                        GROOVE_ACT_Y = rdr["GROOVE_ACT_Y"].ToString();
                        GROOVE_ACT_Z = rdr["GROOVE_ACT_Z"].ToString();
                        GROOVEID = rdr["GROOVEID"].ToString();
                        dt_selected.Rows.Add("0", GROOVEID, "", "", "", "", GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z);
                    }
                    //if (carType == "100" && grooveTotal != "" && Convert.ToInt32(grooveTotal) >= 1)
                    //{
                    //    for (int i = 1; i <= Convert.ToInt32(grooveNum) - Convert.ToInt32(grooveTotal); i++)
                    //    {
                    //        dt_selected.Rows.Add("0", "", "", "", "", "", 999999, 999999, 999999);
                    //    }
                    //}
                    //else if (carType == "102" && grooveTotal != "" && Convert.ToInt32(grooveTotal) >= 1)
                    //{
                    //    for (int i = 1; i <= 2 - Convert.ToInt32(grooveTotal); i++)
                    //    {
                    //        dt_selected.Rows.Add("0", "", "", "", "", "", 999999, 999999, 999999);
                    //    }
                    //}
                    //else if (carType == "200")
                    //{
                    //    for (int i = 1; i <= 6 ; i++)
                    //    {
                    //        dt_selected.Rows.Add("0", "", "", "", "", "", 999999, 999999, 999999);
                    //    }
                    //}
                }

                this.dataGridView2.DataSource = dt_selected;
                bResut = true;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }

            return bResut;
        }
        /// <summary>
        /// 与配载信息匹配
        /// </summary>
        /// <param name="treatmentNo"></param>
        /// <param name="LASER_ACTION_COUNT"></param>
        /// <returns></returns>
        private bool CheckWithLaserOutData(string treatmentNo, long LASER_ACTION_COUNT)
        {
            bool bResult = false;
            string sqlText;

            try
            {
                // 获取最新激光扫描数据（从出库激光表里取出激光扫描数据）
                Dictionary<string, LASER_OUT_DATA> dictGrooveIDLaserOut = new Dictionary<string, LASER_OUT_DATA>();
                sqlText = @"SELECT GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z, GROOVEID FROM UACS_LASER_OUT ";
                sqlText += "WHERE TREATMENT_NO = '{0}' AND LASER_ACTION_COUNT = '{1}' ";
                sqlText = string.Format(sqlText, treatmentNo, LASER_ACTION_COUNT);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        LASER_OUT_DATA laseroutData;
                        laseroutData.GROOVE_ACT_X = rdr["GROOVE_ACT_X"].ToString();
                        laseroutData.GROOVE_ACT_Y = rdr["GROOVE_ACT_Y"].ToString();
                        laseroutData.GROOVE_ACT_Z = rdr["GROOVE_ACT_Z"].ToString();
                        laseroutData.GROOVEID = rdr["GROOVEID"].ToString();

                        dictGrooveIDLaserOut[laseroutData.GROOVEID] = laseroutData;
                    }
                }

                // 与画面选定的配载信息比对
                int nCountCoil = 0;
                int nCountChecked = 0;
                for (int i = 0; i < dt_selected.Rows.Count; i++)
                {
                    if (i < 30)
                    {
                        string coilNO = dt_selected.Rows[i]["COIL_NO2"].ToString().Trim();
                        if (coilNO.Length != 0)
                        {
                            nCountCoil++;

                            // 配过卷的
                            string GROOVEID = dt_selected.Rows[i]["GROOVEID"].ToString();
                            string GROOVE_X = dt_selected.Rows[i]["GROOVE_ACT_X"].ToString();
                            string GROOVE_Y = dt_selected.Rows[i]["GROOVE_ACT_Y"].ToString();
                            string GROOVE_Z = dt_selected.Rows[i]["GROOVE_ACT_Z"].ToString();

                            if (dictGrooveIDLaserOut.ContainsKey(GROOVEID))
                            {
                                LASER_OUT_DATA laserout = dictGrooveIDLaserOut[GROOVEID];

                                // 画面数据与选择数据匹配
                                if (laserout.GROOVE_ACT_X == GROOVE_X &&
                                    laserout.GROOVE_ACT_Y == GROOVE_Y &&
                                    laserout.GROOVE_ACT_Z == GROOVE_Z)
                                {
                                    nCountChecked++;
                                }
                            }
                        }
                    }
                }
                // 数据与后台均匹配
                if (nCountChecked == nCountCoil && nCountCoil != 0)
                    bResult = true;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }

            return bResult;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string strStockNO;
            //提单
            if (txtGetPlanNo.Text.Trim().Length > 3)
            {
                //改 
                //string strPacking = cbbPacking.Text.Trim().Substring(0, 3);
                //StringBuilder sbb = new StringBuilder(strPacking);
                //sbb.Append("-1");
                string toUpPlanNO = txtGetPlanNo.Text.ToUpper().Trim();
                BindMatStock(parkNO, toUpPlanNO);
                //
                return;
            }
            else
            {
                txtGetPlanNo.Text = "";
            }
            //库位
            //if (!txtBoxStockNO.Text.Contains('-') && txtGetMat.Text.Trim() == "")
            //{
            //    MessageBox.Show(string.Format("输入库位：{0}格式不合法，请重新输入，格式为：排-列。", txtBoxStockNO.Text));
            //    txtBoxStockNO.Text = "";
            //    return;
            //}
            if (txtBoxStockNO.Text.Trim().Length != 10 && !txtBoxStockNO.Text.Contains('-') && txtGetMat.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("输入库位：{0}位数不对，请重新输入", txtBoxStockNO.Text));
                txtBoxStockNO.Text = "";
                return;
            }
            if (txtBoxStockNO.Text.Trim().Length == 10 && txtGetMat.Text.Trim() == "")
            {
                strStockNO = txtBoxStockNO.Text;

                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (dgvRow.Cells["STOCK_NO"].Value != null)
                    {
                        if (dgvRow.Cells["STOCK_NO"].Value.ToString() == strStockNO)
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = dgvRow.Index;
                            dgvRow.Cells["STOCK_NO"].Selected = true;
                            dgvRow.Cells["CHECK_COLUMN"].Value = true;
                            dataGridView1.CurrentCell = dgvRow.Cells["STOCK_NO"];
                            if (((DataTable)dataGridView2.DataSource).Rows.Count > 0)
                            {
                                foreach (DataGridViewRow item2 in dataGridView2.Rows)
                                {
                                    if (item2.Cells["COIL_NO2"].Value.ToString() == "")
                                    {
                                        item2.Cells[0].Value = true;
                                        //dataGridView1.CurrentCell = item2.Cells[0];
                                        return;
                                    }
                                    else
                                    {
                                        item2.Cells[0].Value = false;
                                    }
                                }
                            }
                            return;
                        }
                    }
                }
                MessageBox.Show(string.Format("没有找到指定的库位号：{0}", strStockNO));
            }
            if (txtBoxStockNO.Text.Contains('-') && txtGetMat.Text.Trim() == "")
            {
                strStockNO = txtBoxStockNO.Text;
                int index = txtBoxStockNO.Text.IndexOf("-");
                strStockNO = String.Format("{0}{1}1", strStockNO.Substring(0, index).PadLeft(2, '0'), strStockNO.Substring(index + 1).PadLeft(2, '0'));

                //string str0;
                //string str1;
                //string str2;
                //str0 = parkNO.Substring(0, 3);
                //int index1 = txtBoxStockNO.Text.Trim().IndexOf('-');
                //str1 = txtBoxStockNO.Text.Trim().Substring(0, index1);
                //if (str1.Length == 1)
                //{
                //    str1 = string.Format("0{0}", str1);
                //}
                //else if (str1.Length == 2)
                //{
                //    str1 = string.Format("{0}", str1);
                //}
                //else
                //{

                //}
                //str2 = txtBoxStockNO.Text.Trim().Substring(index1 + 1);
                //for (int i = str2.Length; i < 2; i++)
                //{
                //    str2 = string.Format("0{0}", str2);

                //}
                //strStockNO = string.Format("{0}{1}{2}1", str0, str1, str2);
                // SelectDataGridViewRow(dataGridView1, txtBoxStockNO.Text.Trim(), "STOCK_NO");

                //foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                //{                
                //    //if (dgvRow.Cells["STOCK_NO"].Value != null)
                //    if(m.Success)
                //    {
                //        if (dgvRow.Cells["STOCK_NO"].Value.ToString() == strStockNO)
                //        {
                //            dataGridView1.FirstDisplayedScrollingRowIndex = dgvRow.Index;
                //            dgvRow.Cells["STOCK_NO"].Selected = true;
                //            dgvRow.Cells["CHECK_COLUMN"].Value = true;
                //            dataGridView1.CurrentCell = dgvRow.Cells["STOCK_NO"];
                //            if (((DataTable)dataGridView2.DataSource).Rows.Count > 0)
                //            {
                //                foreach (DataGridViewRow item2 in dataGridView2.Rows)
                //                {
                //                    if (item2.Cells["COIL_NO2"].Value.ToString() == "")
                //                    {
                //                        item2.Cells[0].Value = true;
                //                        //dataGridView1.CurrentCell = item2.Cells[0];
                //                        return;
                //                    }
                //                    else
                //                    {
                //                        item2.Cells[0].Value = false;
                //                    }
                //                }
                //            }
                //            return;
                //        }
                //    }
                //}
                string sqlText_All = @"  SELECT 0 AS CHECK_COLUMN,C.MAT_NO AS COIL_NO, A.LOT_NO as LOT_NO,  C.BAY_NO, C.STOCK_NO, B.WEIGHT, B.WIDTH, B.INDIA, B.OUTDIA,  B.PACK_CODE,";
                sqlText_All += "    D.X_CENTER, D.Y_CENTER, C.Z_CENTER ,";
                sqlText_All += " B.ACT_WEIGHT, B.ACT_WIDTH FROM UACS_YARDMAP_STOCK_DEFINE C ";
                sqlText_All += " LEFT JOIN UACS_YARDMAP_COIL B ON C.MAT_NO = B.COIL_NO ";
                sqlText_All += " LEFT JOIN  UACS_PLAN_L3PICK A ON C.MAT_NO = A.COIL_NO ";
                sqlText_All += " LEFT JOIN  UACS_YARDMAP_SADDLE_STOCK E ON C.STOCK_NO = E.STOCK_NO ";
                sqlText_All += " LEFT JOIN  UACS_YARDMAP_SADDLE_DEFINE D  ON E.SADDLE_NO = D.SADDLE_NO ";
                if (parkNO.Contains("Z6"))
                {
                    sqlText_All += " WHERE C.BAY_NO like '" + parkNO.Substring(0, 3) + "%' ";
                }
                else
                {
                    sqlText_All += "WHERE C.BAY_NO = '" + (parkNO.Substring(0, 3) == "Z07" ? "Z22" : "Z21") + "'";
                }
                if (carType == "200")
                {
                    if (parkNO.Substring(0, 3) == "Z08" || parkNO.Substring(0, 3) == "Z21")
                    {
                        sqlText_All += " AND (D.COL_ROW_NO = 'Z21-C-29' OR D.COL_ROW_NO = 'Z21-C-30' OR D.COL_ROW_NO = 'Z21-C-31' OR D.COL_ROW_NO = 'Z21-C-32') ";
                    }
                    else if (parkNO.Substring(0, 3) == "Z07" || parkNO.Substring(0, 3) == "Z22")
                    {
                        sqlText_All += " AND (D.COL_ROW_NO = 'Z22-B-9' OR D.COL_ROW_NO = 'Z22-B-10' OR D.COL_ROW_NO = 'Z22-B-11') ";
                        sqlText_All += " AND (D.COL_NO >= 1 AND D.COL_NO <= 6) ";
                    }
                    else if (parkNO.Substring(0, 3) == "Z23")
                    {
                        sqlText_All += " AND (D.COL_ROW_NO = 'Z23-G-72' OR D.COL_ROW_NO = 'Z23-G-73' OR D.COL_ROW_NO = 'Z23-G-74') ";
                    }
                    sqlText_All += " AND (B.PACK_CODE = 'P2' OR B.PACK_CODE = '2P' OR B.PACK_CODE = 'p2' OR B.PACK_CODE = '2p' OR B.PACK_CODE = 'W4' OR B.PACK_CODE = '4W' OR B.PACK_CODE = 'w4' OR B.PACK_CODE = '4w' OR B.PACK_CODE = 'X2' OR B.PACK_CODE = '2X' OR B.PACK_CODE = 'x2' OR B.PACK_CODE = '2x') ";
                }
                sqlText_All += "AND C.STOCK_NO LIKE '%" + strStockNO + "'";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_All))
                {
                    dt.Clear();
                    while (rdr.Read())
                    {
                        if (!hasSetColumn)
                        {
                            setDataColumn(dt, rdr);
                        }
                        hasSetColumn = true;
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            dr[i] = rdr[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                this.dataGridView1.DataSource = dt;
                dataGridViewColor(dataGridView1);

                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (dgvRow.Cells["STOCK_NO"].Value != null)
                    {
                        return;
                    }
                }
                MessageBox.Show(string.Format("没有找到指定的库位号：{0}", strStockNO));

            }

            //材料
            if (txtGetMat.Text.Trim() == "")
            {
                //cbBoxPickNo_SelectedIndexChanged(null, null);
                return;
            }
            if (txtGetMat.Text.Trim().Length >= 11)
                SelectDataGridViewRow(dataGridView1, txtGetMat.Text.Trim(), "COIL_NO");
            else
            {
                MessageBox.Show(string.Format("没有找到该材料号：{0}", txtGetMat.Text.Trim()));
            }
        }

        /// <summary>
        /// 定位到指定的行
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="searchString"></param>
        /// <param name="columnName"></param>
        private void SelectDataGridViewRow(DataGridView dgv, string searchString, string columnName)
        {
            try
            {
                foreach (DataGridViewRow dgvRow in dgv.Rows)
                {
                    if (dgvRow.Cells[columnName].Value != null)
                    {
                        if (dgvRow.Cells[columnName].Value.ToString() == searchString)
                        {
                            dgv.FirstDisplayedScrollingRowIndex = dgvRow.Index;
                            dgvRow.Cells[columnName].Selected = true;
                            dgv.CurrentCell = dgvRow.Cells[columnName];
                            return;
                        }
                    }
                }
                MessageBox.Show(string.Format("没有找到指定的钢卷：{0}", searchString));
            }

            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }
        }



        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow item in dataGridView2.Rows)
                {
                    if (item.Index == e.RowIndex)
                    {
                        item.Cells[0].Value = true;
                    }
                    else
                    {
                        item.Cells[0].Value = false;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string matNo = "";  //材料号
                string pickNo = ""; //提单号
                string coilweight = "";
                string coilOutdia = "";
                string coilwidth = "";
                string bayNo = "";


                //检测所选材料是否为单选
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    //string  hasChecked = this.dataGridView1.Rows[i].Cells["CHECK_COLUMN"].Value.ToString();  //打钩标记
                    bool hasChecked = (bool)dataGridView1.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
                    if (hasChecked)
                    {
                        matNo = dataGridView1.Rows[i].Cells["COIL_NO"].Value.ToString();            //材料号
                        pickNo = dataGridView1.Rows[i].Cells["PLAN_NO"].Value.ToString();           //计划号
                        coilweight = dataGridView1.Rows[i].Cells["ACT_WEIGHT"].Value.ToString();        //重量
                        coilOutdia = dataGridView1.Rows[i].Cells["OUTDIA"].Value.ToString();        //外径
                        coilwidth = dataGridView1.Rows[i].Cells["ACT_WIDTH"].Value.ToString();          //宽度
                        bayNo = dataGridView1.Rows[i].Cells["BAY_NO"].Value.ToString();             //跨别
                        //count++;
                        //消除打钩
                        this.dataGridView1.Rows[i].Cells["CHECK_COLUMN"].Value = 0;
                        break;
                    }
                }

                ////判断材料是否与停车位同一跨别
                //if (bayNo != bayNO)
                //{
                //    MessageBox.Show(string.Format("该材料:{0}不在当前跨，请重新选择材料号！", matNo));
                //    return;
                //}

                //判断材料号是否相同
                foreach (DataGridViewRow item in dataGridView2.Rows)
                {
                    if (item.Cells["COIL_NO2"].Value.ToString() != "")
                    {
                        if (item.Cells["COIL_NO2"].Value.ToString() == matNo)
                        {
                            MessageBox.Show(string.Format("该材料:{0}已经选择，请重新选择材料号！", matNo));
                            return;
                        }
                    }
                }
                if (((DataTable)dataGridView2.DataSource).Rows.Count == 0)
                {
                    MessageBox.Show("扫描槽号结果为空。");
                    return;
                }

                //判断选卷重量
                for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                {
                    bool hasChecked2 = (bool)dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].EditedFormattedValue;
                    if (hasChecked2)
                    {
                        if (coilwidth != "")
                            coilWidth = Convert.ToInt32(coilwidth);
                        if (carType == "102")
                        {
                            if (coilWidth < 900)//卷宽小于900
                            {
                                MessageBox.Show("大头车选卷钢卷宽度不允许小于900mm，请重新选卷");
                                return;
                            }
                        }
                        //显示钢卷中重量
                        //coilsWeight += GetCoilWeight(matNo);
                        if (coilweight != "")
                            coilsWeight += Convert.ToInt32(coilweight);
                        if (carType == "102")
                        {
                            if (coilsWeight >= 60000)//大于60吨报警
                            {
                                //txtCoilsWeight.BackColor = Color.Red;
                                MessageBox.Show("大头车选卷不允许超过60吨，请重新选卷");
                                coilsWeight -= Convert.ToInt32(coilweight);
                                return;
                            }
                            else if (0 < coilsWeight && coilsWeight < 60000)
                            {
                                txtCoilsWeight.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            if (coilsWeight >= 130000)//大于130吨报警
                            {
                                txtCoilsWeight.BackColor = Color.Red;
                                //return;                               
                            }
                            else if (0 < coilsWeight && coilsWeight < 50000)
                            {
                                txtCoilsWeight.BackColor = Color.White;
                            }
                        }
                        this.dataGridView2.Rows[i].Cells["COIL_NO2"].Value = matNo;
                        this.dataGridView2.Rows[i].Cells["PICK_NO"].Value = pickNo;
                        this.dataGridView2.Rows[i].Cells["WEIGHT2"].Value = coilweight;
                        this.dataGridView2.Rows[i].Cells["OUTDIA2"].Value = coilOutdia;
                        //消除打钩
                        this.dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value = 0;
                        break;
                    }
                }
                txtCoilsWeight.Text = string.Format("{0} /公斤", coilsWeight);
                txtBoxStockNO.Focus();
                txtBoxStockNO.SelectAll();
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Index == e.RowIndex)
                    {
                        item.Cells[0].Value = true;
                    }
                    else
                    {
                        item.Cells[0].Value = false;
                    }
                }
            }
            if (((DataTable)dataGridView2.DataSource).Rows.Count > 0)
            {
                foreach (DataGridViewRow item2 in dataGridView2.Rows)
                {
                    if (item2.Cells["COIL_NO2"].Value.ToString() == "")
                    {
                        item2.Cells[0].Value = true;
                        //dataGridView1.CurrentCell = item2.Cells[0];
                        return;
                    }
                    else
                    {
                        item2.Cells[0].Value = false;
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = this.dataGridView2.Rows.Count - 1; i >= 0; i--)
                {
                    //string  hasChecked = this.dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value.ToString();
                    //string coilNo = this.dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                    bool hasChecked = (bool)this.dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].EditedFormattedValue;
                    if (hasChecked)
                    {
                        coilsWeight -= Convert.ToInt32(dataGridView2.Rows[i].Cells["WEIGHT2"].Value);
                        this.dataGridView2.Rows[i].Cells["COIL_NO2"].Value = "";
                        this.dataGridView2.Rows[i].Cells["PICK_NO"].Value = "";
                        dataGridView2.Rows[i].Cells["WEIGHT2"].Value = "";         //重量
                        dataGridView2.Rows[i].Cells["OUTDIA2"].Value = "";        //外径

                        //消除打钩

                        this.dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value = 0;
                        break;
                    }
                }
                // this.dataGridView2.DataSource = dt_selected;
                txtCoilsWeight.Text = string.Format("{0} /公斤", coilsWeight);
                txtCoilsWeight.BackColor = Color.White;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }
        }



        private void txtBoxStockNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TransferValue("", false);
            this.Close();
        }

        private void txtGetPlanNo_TextChanged(object sender, EventArgs e)
        {
            string UpTem = txtGetPlanNo.Text;
            txtGetPlanNo.Text = UpTem.ToUpper().Trim();
            txtGetPlanNo.SelectionStart = txtGetPlanNo.Text.Length;
            txtGetPlanNo.SelectionLength = 0;
            if (txtGetPlanNo.Text.Trim() == "")
            {
                //加载材料信息
                BindMatStock(parkNO);
            }
        }

        private void txtBoxStockNO_TextChanged(object sender, EventArgs e)
        {
            txtGetPlanNo.Text = "";
            txtGetMat.Text = "";
        }

        private void txtGetMat_TextChanged(object sender, EventArgs e)
        {
            string UpTem = txtGetMat.Text;
            txtGetMat.Text = UpTem.ToUpper().Trim();
            txtGetMat.SelectionStart = txtGetMat.Text.Length;
            txtGetMat.SelectionLength = 0;
            txtGetPlanNo.Text = "";
            txtBoxStockNO.Text = "";
            if (txtGetMat.Text == "1")
            {
                MessageBox.Show("卷半径干涉关闭。", "警告");
            }
        }

        private void txtGetPlanNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }

        }

        private void txtGetMat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void txtBoxStockNO_Click(object sender, EventArgs e)
        {
            txtBoxStockNO.SelectAll();
        }

        private void txtGetPlanNo_Click(object sender, EventArgs e)
        {
            txtGetPlanNo.SelectAll();
        }

        private void txtGetMat_Click(object sender, EventArgs e)
        {
            txtGetMat.SelectAll();
        }
        #region 多库位、卷信息有误判断

        private bool checkMatNOCount(out string repeatedMats)
        {
            bool ret = false;
            repeatedMats = "";
            foreach (DataGridViewRow item in dataGridView2.Rows)
            {
                if (item.Cells["COIL_NO2"].Value != null && item.Cells["COIL_NO2"].Value.ToString() != "")
                {
                    string matNO = item.Cells["COIL_NO2"].Value.ToString();
                    int matNOcount = getMatCount(matNO);
                    if (matNOcount > 1)
                    {
                        repeatedMats += " 多库位钢卷：";
                        repeatedMats += matNO + "  ";
                        ret = true;
                        //return ret;
                    }
                    else if (matNOcount == 0)
                    {
                        repeatedMats += " 无库位钢卷: " + matNO + " ";
                        ret = true;
                    }
                    if (judgetCoilInfo(matNO, ref repeatedMats))
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }
        private int getMatCount(string matNO)
        {
            int count = 0;
            try
            {
                string sql = " SELECT COUNT (MAT_NO) AS MAT_NO_COUNT FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO ='" + matNO + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        count = ManagerHelper.JudgeIntNull(rdr["MAT_NO_COUNT"]);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            return count;
        }
        private bool judgetCoilInfo(string coilNO, ref string retInfo)
        {
            bool ret = false;
            try
            {
                string sql = " SELECT  ACT_WEIGHT, WIDTH, INDIA, OUTDIA FROM UACS_YARDMAP_COIL WHERE COIL_NO ='" + coilNO + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        int weight = ManagerHelper.JudgeIntNull(rdr["ACT_WEIGHT"]);
                        int width = ManagerHelper.JudgeIntNull(rdr["WIDTH"]);
                        int inDIA = ManagerHelper.JudgeIntNull(rdr["INDIA"]);
                        int outDIA = ManagerHelper.JudgeIntNull(rdr["OUTDIA"]);
                        if (weight < 10)
                        {
                            retInfo += "\r\n";
                            retInfo += " 钢卷信息有误：" + coilNO + " , ";
                            retInfo += " 重量小于10 ; ";
                            ret = true;
                        }
                        if (width < 100)
                        {
                            retInfo += "\r\n";
                            retInfo += " 钢卷信息有误：" + coilNO + " , ";
                            retInfo += " 宽度小于100 ; ";
                            ret = true;
                        }
                        if (inDIA < 10)
                        {
                            retInfo += "\r\n";
                            retInfo += " 钢卷信息有误：" + coilNO + " , ";
                            retInfo += " 内径小于10 ; ";
                            ret = true;
                        }
                        if (outDIA < 10)
                        {
                            retInfo += "\r\n";
                            retInfo += " 钢卷信息有误：" + coilNO + " , ";
                            retInfo += " 外径小于10 ; ";
                            ret = true;
                        }
                    }
                    else
                    {
                        retInfo += "\r\n";
                        retInfo += "没有钢卷信息 ：" + coilNO + " ;";
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            return ret;
        }
        #endregion

        //判断跨别
        private void judgeBayNo(string parkingNo)
        {
            if (parkingNo.Contains("Z21") || parkingNo.Contains("Z08"))
            {
                bayNO = "Z21";
            }
            else if (parkingNo.Contains("Z22") || parkingNo.Contains("Z07"))
            {
                bayNO = "Z22";
            }
            else if (parkingNo.Contains("Z23"))
            {
                bayNO = "Z23";
            }
            else if (parkingNo.Contains("Z62"))
            {
                bayNO = "Z62";
            }
            else if (parkingNo.Contains("Z63"))
            {
                bayNO = "Z63";
            }
            else if (parkingNo.Contains("Z01"))
            {
                bayNO = "Z01";
            }
            else
            {
                bayNO = "";
            }
        }

        //卷号设置颜色
        private void dataGridViewColor(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //if (dataGridView.Rows[i].Cells["PACK_CODE"].Value != System.DBNull.Value)
                //{
                //    string packCode = dataGridView.Rows[i].Cells["PACK_CODE"].Value.ToString().Trim().ToUpper();
                //    if (packCode == "X2" || packCode == "2X" || packCode == "P2" || packCode == "2P" || packCode == "W4" || packCode == "4W")
                //    {
                //        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                //    }
                //}
                if (dataGridView.Rows[i].Cells["BAY_NO"].Value != System.DBNull.Value)
                {
                    if (dataGridView.Rows[i].Cells["BAY_NO"].Value.ToString().Trim() != bayNO)
                    {
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
               
       
    }
}
