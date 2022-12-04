using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;
using UACSControls;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.TagService.Interface;
using Baosight.iSuperframe.Authorization.Interface;
using System.Threading;
using System.Xml;

namespace UACSView.CLTSHMI
{
    public partial class FrmCLTS : FormBase
    {
        public FrmCLTS()
        {
            InitializeComponent();
        }

        #region -------------------TAG配置------------------------------
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth = null;

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
        #endregion

        #region -------------------TAG配置2------------------------------
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = null;
        public Baosight.iSuperframe.TagService.Controls.TagDataProvider TagDP
        {
            get
            {
                if (tagDP == null)
                {
                    try
                    {
                        tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
                        tagDP.ServiceName = "iplature";
                        tagDP.AutoRegist = true;
                    }
                    catch (System.Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                }
                return tagDP;
            }
            //set { tagDP = value; }
        }

        private void TrigTag(string tagName, string tagValue)
        {
            try
            {
                if (tagValue.Length != 0 && tagName.Length != 0)
                {
                    tagDP.SetData(tagName, tagValue);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        #endregion

        #region  -------------------声明字段------------------------------
        private string bayNo = null;
        private string strOneselfCrane = null;
        private string strUpstreamCrane = null;
        private string strDownstreamCrane = null;
        private string 
            WMSCraneOrderDgv = null;
        private string sqlCarToYardDgv = null;

        private CraneStatusInBay craneStatusInBay;
        private CraneStatusBase craneStatusBase;
        private Dictionary<string, int> dicCheck = new Dictionary<string, int>();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataGridView dgvall = new DataGridView();
        string CMD_STATUS = "CMD_STATUS_1";
        bool hasSetColumn = false;
        bool hasSetColumn2 = false;
        int p = 0;      
        #endregion

        #region  ---------------------方法--------------------------------

        //刷新
        private void Refresh()
        {
            craneStatusInBay.getAllPLCStatusInBay(craneStatusInBay.lstCraneNO);
            craneStatusBase = craneStatusInBay.DicCranePLCStatusBase[strOneselfCrane].Clone();
            //获取行车当前坐标、夹钳高度
            txt_XACT.Text = craneStatusBase.XAct.ToString();
            txt_YACT.Text = craneStatusBase.YAct.ToString();
            txt_ZACT.Text = craneStatusBase.ZAct.ToString();
            //获取行车有卷状态
            if (craneStatusBase.HasCoil == 1)
            {
                string sql = @"SELECT MAT_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE STOCK_NO = '" + strOneselfCrane + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["MAT_NO"] != DBNull.Value)
                        {
                            txt_HASCOIL.Text = rdr["MAT_NO"].ToString().Trim();
                        }
                        else
                        {
                            txt_HASCOIL.Text = "有卷";
                        }
                    }
                }
                panel_HASCOIL.BackColor = Color.PaleGreen;
            }
            else
            {
                txt_HASCOIL.Text = "无卷";
                panel_HASCOIL.BackColor = System.Drawing.SystemColors.InactiveBorder;
            }
            //获取当前鞍座号
            GetCurrentSaddleNo(craneStatusBase.XAct, craneStatusBase.YAct, txt_SADDLENO);
            if (txt_SADDLENO.Text.Trim() != "999999" && txt_SADDLENO.Text.Trim() != "")
            {
                panel_SADDLENO.BackColor = Color.PaleGreen;
            }
            else
            {
                panel_SADDLENO.BackColor = System.Drawing.SystemColors.InactiveBorder;
            }
            GetCurrentOrder();
            GetNextCoil();
            //BindDGVWMSOrder(dgvWMSCraneOrder);
            //ChangeWMSOrderDGVColor(dgvWMSCraneOrder);

            //BindDGVCarOrder(dgvCraneCarOrder);
            //BindDGVWMSOrder1(dataGridView1);
            //BindDGVCarOrder(dataGridView2, "YSL_EXIT_2_YARD");
            //BindDGVCarOrder(dataGridView3, "YARD_2_CAR");
            //BindDGVCarOrder(dataGridView4, "CAR_2_YARD");
            //BindDGVCarOrder(dataGridView5, "CLTS_ORDER");
            switch (dgvall.Name)
            {
                case "dgvCraneCarOrder":
                    BindDGVCarOrder(dgvCraneCarOrder);
                    break;
                case "dataGridView1":
                    BindDGVWMSOrder1(dataGridView1);
                    break;
                case "dataGridView2":
                    BindDGVCarOrder(dataGridView2, "YSL_EXIT_2_YARD");
                    break;
                case "dataGridView3":
                    BindDGVCarOrder(dataGridView3, "YARD_2_CAR");
                    break;
                case "dataGridView4":
                    BindDGVCarOrder(dataGridView4, "CAR_2_YARD");
                    break;
                case "dataGridView5":
                    BindDGVCarOrder(dataGridView5, "CLTS_ORDER");
                    break;
                default:
                    break;
            }
            //ChangeCarOrderDGVColor(dgvCraneCarOrder);
            if (dgvall.RowCount > p)
            {
                //dgvCraneCarOrder.CurrentCell = dgvCraneCarOrder[1, 0];
                dgvall.Rows[0].Selected = false;
                dgvall.FirstDisplayedScrollingRowIndex = p;
                dgvall.CurrentCell = null;
            }
            //dgvCheck(dgvCraneCarOrder);

            ChangeCarOrderDGVColor(dgvall);
            dgvCheck(dgvall);

        }
        //绑定数据表
        private void BindDGVWMSOrder1(DataGridView dgv)
        {
            try
            {
                string strSq = @"SELECT 0 AS CHECK_COLUMN, A.ORDER_NO ORDER_NO, A.BAY_NO BAY_NO, B.DESCRIBE DESCRIBE, A.CRANE_NO CRANE_NO, A.MAT_NO MAT_NO, A.FROM_STOCK_NO FROM_STOCK_NO, A.TO_STOCK_NO TO_STOCK_NO, 
                                (CASE A.CMD_STATUS 
                                WHEN 'A' THEN '选中' 
                                WHEN 'S' THEN '吊起' 
                                WHEN 'E' THEN '卸下' 
                                ELSE '待执行'
                                END ) CMD_STATUS 
                               FROM CLTS_ORDER A LEFT JOIN CRANE_ORDER_TASK_DEFINE B ON B.TASK_NAME = A.TASK_NAME";
                strSq += " where A.TASK_NAME = 'YARD_2_D202_ENTRY' OR A.TASK_NAME = 'COLLAPSE' AND A.BAY_NO='" + bayNo.Trim() + "' order by A.ORDER_NO";
                dt1.Clear();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSq))
                {
                    dt1.Clear();
                    while (rdr.Read())
                    {
                        DataRow dr = dt1.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt1.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn = true;
                        dt1.Rows.Add(dr);
                    }
                }
                dgv.DataSource = dt1;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        //绑定数据表
        private void BindDGVCarOrder(DataGridView dgv)
        {
            try
            {
                string strSq = @"SELECT 0 AS CHECK_COLUMN, A.ORDER_NO ORDER_NO, A.BAY_NO BAY_NO, B.DESCRIBE DESCRIBE, A.CRANE_NO CRANE_NO, A.MAT_NO MAT_NO, A.FROM_STOCK_NO FROM_STOCK_NO, A.TO_STOCK_NO TO_STOCK_NO, 
                                (CASE A.CMD_STATUS 
                                WHEN 'A' THEN '选中' 
                                WHEN 'S' THEN '吊起' 
                                WHEN 'E' THEN '卸下' 
                                ELSE '待执行'
                                END ) CMD_STATUS 
                               FROM CLTS_ORDER A LEFT JOIN CRANE_ORDER_TASK_DEFINE B ON B.TASK_NAME = A.TASK_NAME";
                strSq += " where A.BAY_NO='" + bayNo.Trim() + "' order by A.ORDER_NO";
                //trSq += " where A.TASK_NAME LIKE '%CAR_2_YARD%' AND A.BAY_NO='" + bayNo.Trim() + "' order by A.ORDER_NO";
                dt2.Clear();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSq))
                {
                    dt2.Clear();
                    while (rdr.Read())
                    {
                        DataRow dr = dt2.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn2)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt2.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn2 = true;
                        dt2.Rows.Add(dr);
                    }
                }
                dgv.DataSource = dt2;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        //绑定指令表
        private void BindDGVCarOrder(DataGridView dgv, string taskname)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string strSq = @"SELECT 0 AS CHECK_COLUMN, A.ORDER_NO ORDER_NO, A.BAY_NO BAY_NO, B.DESCRIBE DESCRIBE, A.CRANE_NO CRANE_NO, A.MAT_NO MAT_NO, A.FROM_STOCK_NO FROM_STOCK_NO, A.TO_STOCK_NO TO_STOCK_NO, 
                                (CASE A.CMD_STATUS 
                                WHEN 'A' THEN '选中' 
                                WHEN 'S' THEN '吊起' 
                                WHEN 'E' THEN '卸下' 
                                ELSE '待执行'
                                END ) CMD_STATUS 
                               FROM CLTS_ORDER A LEFT JOIN CRANE_ORDER_TASK_DEFINE B ON B.TASK_NAME = A.TASK_NAME";
                strSq += " where A.BAY_NO='" + bayNo.Trim() + "' AND A.TASK_NAME = '" + taskname + "'order by A.ORDER_NO";
                dt.Clear();
                dt = new DataTable();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSq))
                {
                    //dt3.Clear();
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
                }
                dgv.DataSource = dt;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }
        //获取行车当前位置
        private void GetCurrentSaddleNo(long ActX, long ActY, Label saddleNo)
        {
            try
            {
                string sqlText = @"SELECT SADDLE_NO FROM UACS_YARDMAP_SADDLE_DEFINE WHERE  (X_CENTER - 300) <= " + ActX + " AND (X_CENTER + 300) >= " + ActX + " AND (Y_CENTER - 300) <= " + ActY + " AND (Y_CENTER + 300) >= " + ActY;
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        //DataRow dr = dt.NewRow();

                        if (rdr["SADDLE_NO"] != DBNull.Value)
                        {
                            saddleNo.Text = rdr["SADDLE_NO"].ToString().Trim();
                        }
                        else
                        {
                            saddleNo.Text = "999999";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        //获取行车当前位置
        private void GetCurrentOrder()
        {
            try
            {
                string sqlText = @"SELECT FROM_STOCK_NO,PLAN_UP_X,PLAN_UP_Y,TO_STOCK_NO,PLAN_DOWN_X,PLAN_DOWN_Y FROM UACS_CRANE_ORDER_CURRENT WHERE CRANE_NO = '" + strOneselfCrane + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        //DataRow dr = dt.NewRow();

                        if (rdr["FROM_STOCK_NO"] != DBNull.Value)
                        {
                            txt_FROM_STOCK_NO.Text = rdr["FROM_STOCK_NO"].ToString().Trim();
                        }
                        else
                        {
                            txt_FROM_STOCK_NO.Text = "999999";
                        }
                        if (rdr["PLAN_UP_X"] != DBNull.Value)
                        {
                            txt_PLAN_UP_X.Text = rdr["PLAN_UP_X"].ToString().Trim();
                        }
                        else
                        {
                            txt_PLAN_UP_X.Text = "999999";
                        }
                        if (rdr["PLAN_UP_Y"] != DBNull.Value)
                        {
                            txt_PLAN_UP_Y.Text = rdr["PLAN_UP_Y"].ToString().Trim();
                        }
                        else
                        {
                            txt_PLAN_UP_Y.Text = "999999";
                        }
                        if (rdr["TO_STOCK_NO"] != DBNull.Value)
                        {
                            txt_TO_STOCK_NO.Text = rdr["TO_STOCK_NO"].ToString().Trim();
                        }
                        else
                        {
                            txt_TO_STOCK_NO.Text = "999999";
                        }
                        if (rdr["PLAN_DOWN_X"] != DBNull.Value)
                        {
                            txt_PLAN_DOWN_X.Text = rdr["PLAN_DOWN_X"].ToString().Trim();
                        }
                        else
                        {
                            txt_PLAN_DOWN_X.Text = "999999";
                        }
                        if (rdr["PLAN_DOWN_Y"] != DBNull.Value)
                        {
                            txt_PLAN_DOWN_Y.Text = rdr["PLAN_DOWN_Y"].ToString().Trim();
                        }
                        else
                        {
                            txt_PLAN_DOWN_Y.Text = "999999";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        //获取上料计划卷
        private void GetNextCoil()
        {
            try
            {
                string NEXT_COILNO = null;
                string stockNo = null;
                string x_Center = null;
                string y_Center = null;
                string sql = @"SELECT NEXT_COILNO FROM UACS_LINE_NEXTCOIL WHERE UNIT_NO = '" + txt_UNIT_NO.Text.Trim() + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["NEXT_COILNO"] != DBNull.Value)
                        {
                            NEXT_COILNO = rdr["NEXT_COILNO"].ToString();
                        }
                        else
                            NEXT_COILNO = null;

                    }
                }

                if (NEXT_COILNO != null)
                {
                    txt_NEXT_COIL.Text = NEXT_COILNO;
                    string sqlStock = @"SELECT A.STOCK_NO,C.X_CENTER,C.Y_CENTER FROM UACS_YARDMAP_STOCK_DEFINE A 
                                        LEFT JOIN UACS_YARDMAP_SADDLE_STOCK B ON B.STOCK_NO = A.STOCK_NO 
                                        LEFT JOIN UACS_YARDMAP_SADDLE_DEFINE C ON C.SADDLE_NO = B.SADDLE_NO 
                                        WHERE A.MAT_NO = '" + NEXT_COILNO + "'";
                    using (IDataReader rdrStock = DB2Connect.DBHelper.ExecuteReader(sqlStock))
                    {
                        while (rdrStock.Read())
                        {
                            if (rdrStock["STOCK_NO"] != DBNull.Value)
                            {
                                stockNo = rdrStock["STOCK_NO"].ToString();
                                x_Center = rdrStock["X_CENTER"].ToString();
                                y_Center = rdrStock["Y_CENTER"].ToString();
                            }
                            else
                                stockNo = null;
                        }
                    }
                    if (stockNo != null)
                    {
                        txt_STOCK_NO.Text = stockNo;
                        txt_X_CENTER.Text = x_Center;
                        txt_Y_CENTER.Text = y_Center;
                    }
                    else
                    {
                        txt_STOCK_NO.Text = "无库位";
                        txt_X_CENTER.Text = "999999";
                        txt_Y_CENTER.Text = "999999";
                    }
                }
                else
                {
                    txt_NEXT_COIL.Text = "没有钢卷";
                    txt_STOCK_NO.Text = "无库位";
                    txt_X_CENTER.Text = "999999";
                    txt_Y_CENTER.Text = "999999";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        //绑定DGV选项
        private void dgvCheck(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                foreach (string key in dicCheck.Keys)
                {
                    if (dgv.Rows[i].Cells[1].Value.ToString() == key)
                    {
                        dgv.Rows[i].Cells[0].Value = dicCheck[key];
                    }
                }
            }
        }



        private void ChangeWMSOrderDGVColor(DataGridView dgv)
        {
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                string status = dgr.Cells["CMD_STATUS"].Value.ToString().Trim();
                if (status == "选中")
                {
                    dgr.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (status == "吊起" || status == "卸下")
                {
                    dgr.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                else
                {
                    dgr.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void ChangeCarOrderDGVColor(DataGridView dgv)
        {
            string status = "";
            
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                switch (dgv.Name)
                {
                    case "dgvCraneCarOrder":
                        status = dgr.Cells["CMD_STATUS_1"].Value.ToString().Trim();
                        break;
                    case "dataGridView1":
                        status = dgr.Cells["CMD_STATUS_2"].Value.ToString().Trim();
                        break;
                    case "dataGridView2":
                        status = dgr.Cells["CMD_STATUS_3"].Value.ToString().Trim();
                        break;
                    case "dataGridView3":
                        status = dgr.Cells["CMD_STATUS_4"].Value.ToString().Trim();
                        break;
                    case "dataGridView4":
                        status = dgr.Cells["CMD_STATUS_5"].Value.ToString().Trim();
                        break;
                    case "dataGridView5":
                        status = dgr.Cells["CMD_STATUS_6"].Value.ToString().Trim();
                        break;
                    default:
                        break;
                }
                                 
                if (status == "选中")
                {
                    dgr.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (status == "吊起" || status == "卸下")
                {
                    dgr.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                else
                {
                    dgr.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        //绑定跨号
        private void Bind_cbbBayNo(ComboBox comBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //cmbArea.Items.Clear();
            try
            {
                string sqlText = @"SELECT DISTINCT BAY_NO as TypeValue,BAY_NAME as TypeName FROM UACS_YARDMAP_BAY_DEFINE";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();

                        if (rdr["TypeName"].ToString().Trim() != "")
                        {
                            dr["TypeValue"] = rdr["TypeValue"];
                            dr["TypeName"] = rdr["TypeName"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                //绑定列表下拉框数据
                comBox.DataSource = dt;
                comBox.DisplayMember = "TypeName";
                comBox.ValueMember = "TypeValue";
                comBox.SelectedItem = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }
        #endregion

        #region  ---------------------事件--------------------------------
        #endregion
        private void FrmCLTS_Load(object sender, EventArgs e)
        {
            try
            {
                TagValues.Clear();
                TagValues.Add("EV_PITCH_ON_ORDER_CLTS", null);
                TagValues.Add("EV_COIL_UP_CLTS", null);
                TagValues.Add("EV_COIL_DOWN_CLTS", null);
                TagValues.Add("EV_CANCEL_CLTS", null);
                TagValues.Add("EV_DELETE_CLTS", null);
                TagDP.Attach(TagValues);

                craneStatusInBay = new CraneStatusInBay();
                craneStatusBase = new CraneStatusBase();
                #region  ---------------------读取配置文件--------------------------------
                //getData();
                XmlDocument doc = new XmlDocument();
                //doc.Load(@"..\1550CLTSHMICraneConf.xml");
                doc.Load(Application.StartupPath.ToString() + "/" + "1550CLTSHMICraneConf.xml");
                //本地行车
                XmlNode node1 = doc.SelectSingleNode("//Cranestore/Crane[@name='oneselfCrane']//CraneNo");//设置节点位置  
                if (node1 != null)
                {
                    strOneselfCrane = node1.Attributes["value"].Value;
                }
                //上游行车
                XmlNode node2 = doc.SelectSingleNode("//Cranestore/Crane[@name='upstreamCrane']//CraneNo");
                if (node2 != null)
                {
                    strUpstreamCrane = node2.Attributes["value"].Value;
                }
                //下游行车
                XmlNode node3 = doc.SelectSingleNode("//Cranestore/Crane[@name='downstreamCrane']//CraneNo");
                if (node3 != null)
                {
                    strDownstreamCrane = node3.Attributes["value"].Value;
                }
                //跨别
                XmlNode node4 = doc.SelectSingleNode("//Cranestore/Crane[@name='bayNo']//bayNo");
                if (node4 != null)
                {
                    bayNo = node4.Attributes["value"].Value;
                }
                #endregion

                craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
                craneStatusInBay.AddCraneNO(strOneselfCrane);
                craneStatusInBay.SetReady();

                txt_UNIT_NO.Text = constData.UnitNo_D102;
                dataGridView1.CellContentClick += dgvCraneCarOrder_CellContentClick_1;
                dataGridView2.CellContentClick += dgvCraneCarOrder_CellContentClick_1;
                dataGridView3.CellContentClick += dgvCraneCarOrder_CellContentClick_1;
                dataGridView4.CellContentClick += dgvCraneCarOrder_CellContentClick_1;
                dataGridView5.CellContentClick += dgvCraneCarOrder_CellContentClick_1;
                dgvall = dgvCraneCarOrder;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            lblCraneNo.Text = strOneselfCrane + "行车";


            //Bind_cbbBayNo(bayNo);          
            Refresh();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            craneStatusInBay.getAllPLCStatusInBay(craneStatusInBay.lstCraneNO);
            craneStatusBase = craneStatusInBay.DicCranePLCStatusBase[strOneselfCrane].Clone();
            Refresh();
        }

        private void WMSCraneOrderConfig_TabActivated(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void WMSCraneOrderConfig_TabDeactivated(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void dgvCraneCarOrder_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dicCheck.Clear();
                for (int i = 0; i < dgvall.Rows.Count; i++)
                {
                    //if (i != e.RowIndex && dgvCraneCarOrder.CurrentCell.ColumnIndex == 0)
                    if (i != e.RowIndex)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvall.Rows[i].Cells[0];
                        cell.Value = false;
                    }
                    else
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvall.Rows[i].Cells[0];
                        cell.Value = true;
                    }
                    //string order_NO = dgvall.Rows[i].Cells["ORDER_NO_2"].Value.ToString();
                    string order_NO = dgvall.Rows[i].Cells[1].Value.ToString();
                    //bool hasChecked = (bool)dgvall.Rows[i].Cells["CHECK_COLUMN_2"].EditedFormattedValue;
                    bool hasChecked = (bool)dgvall.Rows[i].Cells[0].EditedFormattedValue;
                    if (hasChecked)
                    {
                        dicCheck[order_NO] = 1;
                    }
                    else
                    {
                        dicCheck[order_NO] = 0;

                    }
                    //dgvall.Rows[i].Cells["CHECK_COLUMN_2"].Value = dicCheck[order_NO];
                    dgvall.Rows[i].Cells[0].Value = dicCheck[order_NO];
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Dictionary<string, int> dicCheck = new Dictionary<string, int>();
            if (MessageBox.Show("确定选中改指令？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvall.Rows)
                    {
                        //if (r.Cells["CRANE_NO_2"].Value != null)
                        if (r.Cells[4].Value != null)
                        {
                            //string CraneNo = r.Cells["CRANE_NO_2"].Value.ToString().Trim();
                            string CraneNo = r.Cells[4].Value.ToString().Trim();
                            if (CraneNo != "" && CraneNo == strOneselfCrane)
                            {
                                MessageBox.Show("该行车已有选中指令，无法选中其他指令!");
                                return;
                            }
                        }
                    }
                    foreach (DataGridViewRow r in dgvall.Rows)
                    {
                        //if (r.Cells["CHECK_COLUMN_2"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN_2"].Value) == 1)
                        if (r.Cells[0].Value != null && Convert.ToInt32(r.Cells[0].Value) == 1)
                        {
                            //string orderNO = r.Cells["ORDER_NO_2"].Value.ToString();
                            //string orderCrane = r.Cells["CRANE_NO_2"].Value.ToString().Trim();
                            //string orderStatus = r.Cells["CMD_STATUS_2"].Value.ToString().Trim();
                            string orderNO = r.Cells[1].Value.ToString();
                            string orderCrane = r.Cells[4].Value.ToString().Trim();
                            string orderStatus = r.Cells[CMD_STATUS].Value.ToString().Trim();
                            if (orderCrane != "" || orderStatus != "待执行")
                            {
                                MessageBox.Show("指令处于正在执行状态，无法选中!");
                                return;
                            }

                            string TagValue = strOneselfCrane + "|" + orderNO;
                            TrigTag("EV_PITCH_ON_ORDER_CLTS", TagValue);
                            MessageBox.Show("行车已选中该指令：" + orderNO + "!");
                        }
                    }
                    dicCheck.Clear();
                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定选中该指令？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvall.Rows)
                    {
                        //if (r.Cells["CHECK_COLUMN_2"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN_2"].Value) == 1)
                        if (r.Cells[0].Value != null && Convert.ToInt32(r.Cells[0].Value) == 1)
                        {
                            //string orderNO = r.Cells["ORDER_NO_2"].Value.ToString();
                            //string orderCrane = r.Cells["CRANE_NO_2"].Value.ToString().Trim();
                            //string orderStatus = r.Cells["CMD_STATUS_2"].Value.ToString().Trim();
                            string orderNO = r.Cells[1].Value.ToString();
                            string orderCrane = r.Cells[4].Value.ToString().Trim();
                            string orderStatus = r.Cells[CMD_STATUS].Value.ToString().Trim();
                            if (orderCrane != "" && orderCrane != strOneselfCrane)
                            {
                                MessageBox.Show("该指令非本行车选中，无法取消选中指令!");
                                return;
                            }
                            else if (orderCrane == strOneselfCrane && orderStatus != "选中")
                            {
                                MessageBox.Show("该指令处于执行中状态，无法取消选中指令!");
                                return;
                            }
                            else if (orderCrane == "" && orderStatus != "选中")
                            {
                                MessageBox.Show("该指令处于待执行状态，指令并未被行车选中，无需操作取消选中指令!");
                                return;
                            }
                            string TagValue = strOneselfCrane + "|" + orderNO;
                            TrigTag("EV_CANCEL_CLTS", TagValue);
                            MessageBox.Show("行车已取消选中该指令：" + orderNO + "!");
                        }
                    }
                    dicCheck.Clear();
                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCoilUp_Click(object sender, EventArgs e)
        {
            //Dictionary<string, int> dicCheck = new Dictionary<string, int>();
            if (MessageBox.Show("确定修改该指令状态为吊起状态？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvall.Rows)
                    {
                        //if (r.Cells["CHECK_COLUMN_2"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN_2"].Value) == 1)
                        if (r.Cells[0].Value != null && Convert.ToInt32(r.Cells[0].Value) == 1)
                        {
                            //string orderNO = r.Cells["ORDER_NO_2"].Value.ToString();
                            //string orderCrane = r.Cells["CRANE_NO_2"].Value.ToString().Trim();
                            //string orderStatus = r.Cells["CMD_STATUS_2"].Value.ToString().Trim();
                            string orderNO = r.Cells[1].Value.ToString();
                            string orderCrane = r.Cells[4].Value.ToString().Trim();
                            string orderStatus = r.Cells[CMD_STATUS].Value.ToString().Trim();
                            if (orderCrane == "" && orderStatus == "待执行")
                            {
                                MessageBox.Show("指令未被行车选中，处于待执行状态!");
                                return;
                            }
                            if (orderCrane != "" && orderCrane != strOneselfCrane)
                            {
                                MessageBox.Show("无法修改其他行车指令状态!");
                                return;
                            }
                            string TagValue = strOneselfCrane + "|" + orderNO;
                            TrigTag("EV_COIL_UP_CLTS", TagValue);
                            MessageBox.Show("修改该指令：" + orderNO + "状态为吊起状态!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //卸下操作
        private void btnCoilDown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定修改该指令状态为卸下状态？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvall.Rows)
                    {
                        //if (r.Cells["CHECK_COLUMN_2"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN_2"].Value) == 1)
                        if (r.Cells[0].Value != null && Convert.ToInt32(r.Cells[0].Value) == 1)
                        {
                            //string orderNO = r.Cells["ORDER_NO_2"].Value.ToString();
                            //string orderCrane = r.Cells["CRANE_NO_2"].Value.ToString().Trim();
                            //string orderStatus = r.Cells["CMD_STATUS_2"].Value.ToString().Trim();
                            string orderNO = r.Cells[1].Value.ToString();
                            string orderCrane = r.Cells[4].Value.ToString().Trim();
                            string orderStatus = r.Cells[CMD_STATUS].Value.ToString().Trim();
                            if (orderCrane == "" || orderStatus == "待执行")
                            {
                                MessageBox.Show("指令未被行车选中，处于待执行状态!");
                                return;
                            }
                            if (orderCrane != "" && orderCrane != strOneselfCrane)
                            {
                                MessageBox.Show("无法修改其他行车指令状态!");
                                return;
                            }
                            string TagValue = strOneselfCrane + "|" + orderNO;
                            TrigTag("EV_COIL_DOWN_CLTS", TagValue);
                            MessageBox.Show("修改该指令：" + orderNO + "状态为卸下状态!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //删除指令
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除该指令吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvall.Rows)
                    {
                        //if (r.Cells["CHECK_COLUMN_2"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN_2"].Value) == 1)
                        if (r.Cells[0].Value != null && Convert.ToInt32(r.Cells[0].Value) == 1)
                        {
                            //string orderNO = r.Cells["ORDER_NO_2"].Value.ToString();
                            //string orderCrane = r.Cells["CRANE_NO_2"].Value.ToString().Trim();
                            //string orderStatus = r.Cells["CMD_STATUS_2"].Value.ToString().Trim();
                            string orderNO = r.Cells[1].Value.ToString();
                            string orderCrane = r.Cells[4].Value.ToString().Trim();
                            string orderStatus = r.Cells[CMD_STATUS].Value.ToString().Trim();
                            if (orderCrane != "" && (orderStatus == "吊起" || orderStatus == "卸下"))
                            {
                                MessageBox.Show("行车正在执行该指令，无法删除该指令!");
                                return;
                            }
                            if (orderCrane != "" && orderCrane != strOneselfCrane)
                            {
                                MessageBox.Show("无法修改其他行车指令状态!");
                                return;
                            }
                            string TagValue = strOneselfCrane + "|" + orderNO;
                            TrigTag("EV_DELETE_CLTS", TagValue);
                            MessageBox.Show("已删除该指令：" + orderNO + "!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //刷新任务指令       
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dgvCraneCarOrder_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (this.tabControl1.SelectedTab == tabPage1)
            {
                dgvall = dgvCraneCarOrder;
                CMD_STATUS = "CMD_STATUS_1";
            }
            if (this.tabControl1.SelectedTab == tabPage2)
            {
                dgvall = dataGridView1;
                CMD_STATUS = "CMD_STATUS_2";
            }
            if (this.tabControl1.SelectedTab == tabPage3)
            {
                dgvall = dataGridView2;
                CMD_STATUS = "CMD_STATUS_3";
            }
            if (this.tabControl1.SelectedTab == tabPage4)
            {
                dgvall = dataGridView3;
                CMD_STATUS = "CMD_STATUS_4";
            }
            if (this.tabControl1.SelectedTab == tabPage5)
            {
                dgvall = dataGridView4;
                CMD_STATUS = "CMD_STATUS_5";
            }
            if (this.tabControl1.SelectedTab == tabPage6)
            {
                dgvall = dataGridView5;
                CMD_STATUS = "CMD_STATUS_6";
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }

        private void dataGridView3_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }

        private void dataGridView4_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }

        private void dataGridView5_Scroll(object sender, ScrollEventArgs e)
        {
            p = e.NewValue;
        }


    }
}
