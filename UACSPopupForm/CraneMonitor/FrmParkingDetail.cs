using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.Authorization.Interface;
using Baosight.iSuperframe.Common;
using ParkClassLibrary;

namespace UACSPopupForm
{
    /// <summary>
    /// 停车位详细
    /// </summary>
    public partial class FrmParkingDetail : Form
    {
        /// <summary>
        /// Tag
        /// </summary>
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
        //防止弹出信息关闭画面
        bool isPopupMessage = false;
        private Dictionary<string, string> MatCode = new Dictionary<string, string>();
        string[] dgvColumnsName = { "BTN_UP", "ORDER_NO", "PLAN_NO", "ORDER_PRIORITY", "CMD_SEQ" , "CMD_STATUS", "MAT_CODE", "MAT_CNAME", "FROM_STOCK_NO", "TO_STOCK_NO", "REQ_WEIGHT", "ACT_WEIGHT", "START_TIME", "UPD_TIME", "REC_TIME" };
        string[] dgvHeaderText = { "按钮", "指令号", "计划号", "优先级",  "吊运次数","吊运状态", "物料代码", "物料名称", "取料位置", "落料位", "要求重量", "实绩重量","开始时间", "更新时间", "创建时间" };
        string[] dgvOderColumnsName = { "ORDER_NO", "ORDER_GROUP_NO", "EXE_SEQ", "MAT_CNAME", "FROM_STOCK_NO", "TO_STOCK_NO", "BAY_NO" };
        string[] dgvOderHeaderText = { "指令号", "指令组号", "指令顺序", "物料名称", "取料位", "落料位", "跨别" };
        /// <summary>
        /// L3废钢品名信息
        /// </summary>
        private List<string> L3MatInfoLists = new List<string> { "厂内中混废", "外购中混废", "矽钢片", "汽车切片打包块", "破碎料", "厂内切头", "外购重废", "渣钢", "渣铁", "炼钢生铁", "边角余料一号", "热压铁块", "厂内矽钢片", "高纯渣钢", "冷却剂" };
        public FrmParkingDetail()
        {
            InitializeComponent();
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
        }
        //private DataTable initial_dgv;
        public DataTable Initial_dgv = new DataTable();
        private ParkingBase packingInfo;
        public ParkingBase PackingInfo
        {
            get { return packingInfo; }
            set { packingInfo = value; }
        }
        int carIsLoad;
        private void FrmParkingDetail_Load(object sender, EventArgs e)
        {
            L3MatInfoListsInit();
            ManagerHelper.DataGridViewInit(dgvStowageMessage);
            dgvStowageMessage.SelectionMode = DataGridViewSelectionMode.CellSelect;
            CreatDgvHeader(dgvStowageMessage, dgvColumnsName, dgvHeaderText);
            //dgvStowageMessage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ManagerHelper.DataGridViewInit(dgvCraneOder);
            dgvCraneOder.SelectionMode = DataGridViewSelectionMode.CellSelect;
            CreatDgvHeader(dgvCraneOder, dgvOderColumnsName, dgvOderHeaderText);
            //dgvCraneOder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            GetMAT_Code();
            //CreateComboBoxColumn();
            Refurbish();
            //ShiftStowageMessage();
            this.Deactivate += new EventHandler(frmSaddleDetail_Deactivate);
        }
        /// <summary>
        /// 初始化 L3废钢品名信息
        /// </summary>
        private void L3MatInfoListsInit()
        {
            L3MatInfoLists.Clear();
            string sql = @" SELECT MAT_CODE,MAT_CNAME FROM UACS_L3_MAT_INFO ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    L3MatInfoLists.Add(rdr["MAT_CNAME"].ToString());
                }
            }
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmSaddleDetail_Deactivate(object sender, EventArgs e)
        {
            try
            {
                if (!isPopupMessage)
                {
                    //this.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ShiftStowageMessage()
        {
            for (int i = 0; i < dgvStowageMessage.Rows.Count; i++)
            {
                dgvStowageMessage.Rows[i].DefaultCellStyle.BackColor = Color.White;
                if (dgvStowageMessage.Rows[i].Cells["STATUS"].Value != DBNull.Value)
                {
                    if (dgvStowageMessage.Rows[i].Cells["STATUS"].Value.ToString() == "执行完")
                    {
                        dgvStowageMessage.Rows[i].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            if (lblPacking.Text.Contains("A"))
            {
                string bayno = null;
                switch (lblPacking.Text)
                {
                    case "Z01A1":
                    case "Z01A2":
                        bayno = "1550酸轧原料库";
                        break;
                    case "Z63A1":
                    case "Z63A2":
                        bayno = "轧后库63跨";
                        break;
                    default:
                        bayno = null;
                        break;
                }
                if (lblCarStatus.Text.Contains("出库") || carIsLoad == 0)
                {
                    if (auth.IsOpen("02 车辆出库"))
                    {
                        auth.CloseForm("02 车辆出库");
                        //auth.OpenForm("02 车辆出库", true, bayno, lblPacking.Text);
                        auth.OpenForm("02 车辆出库", lblPacking.Text);
                    }
                    else
                    {
                        //auth.OpenForm("02 车辆出库", true, bayno, lblPacking.Text);
                        auth.OpenForm("02 车辆出库", lblPacking.Text);
                    }
                    this.Close();
                }
                if (lblCarStatus.Text.Contains("入库") || carIsLoad == 1)
                {
                    if (auth.IsOpen("01 车辆入库"))
                    {
                        auth.CloseForm("01 车辆入库");
                        //auth.OpenForm("01 车辆入库", true, bayno, lblPacking.Text);
                        auth.OpenForm("01 车辆入库", lblPacking.Text);
                    }
                    else
                    {
                        //auth.OpenForm("01 车辆入库", true, bayno, lblPacking.Text);
                        auth.OpenForm("01 车辆入库", lblPacking.Text);
                    }
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refurbish()
        {
            isPopupMessage = true;
            lblCarNo.Text = packingInfo.Car_No;
            lblCarStatus.Text = packingInfo.PackingStatusDesc();
            lblPacking.Text = packingInfo.ParkingName;
            carIsLoad = packingInfo.IsLoaded;
            
            DataTable dtNull = new DataTable();
            //if (dgvStowageMessage.DataSource != null)
            //    dgvStowageMessage.DataSource = dtNull;
            //if (dgvCraneOder.DataSource != null)
            //    dgvCraneOder.DataSource = dtNull;
            lb_REQ_WEIGHT.Text = "";
            lb_ACT_WEIGHT.Text = "";
            lblCarType.Text = ParkingInfo.getStowageCarType(packingInfo.STOWAGE_ID, packingInfo.Car_No, packingInfo.ParkingName);
            //ParkingInfo.dgvStowageMessage(packingInfo.STOWAGE_ID, packingInfo.Car_No, packingInfo.ParkingName, dgvStowageMessage);
            Get_L3_MAT_INFO(packingInfo.Car_No, packingInfo.ParkingName);
            ParkingInfo.dgvStowageOrder(packingInfo.Car_No, packingInfo.ParkingName, dgvCraneOder);
            if (dgvStowageMessage.DataSource != null && dgvStowageMessage.Rows.Count > 0)
            {
                //DataTable dt = (dgvStowageMessage.DataSource as DataTable);
                //Initial_dgv = dt;
                Initial_dgv = GetDgvToTable(dgvStowageMessage);
            }

            var REQWeight = 0;
            var ACTWeight = 0;
            if (dgvStowageMessage.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dgvStowageMessage.Rows)
                {
                    if (!string.IsNullOrEmpty(dgr.Cells["REQ_WEIGHT"].Value.ToString()))
                    {
                        //要求重量
                        REQWeight = REQWeight + Convert.ToInt32(dgr.Cells["REQ_WEIGHT"].Value.ToString());
                    }
                    if (!string.IsNullOrEmpty(dgr.Cells["ACT_WEIGHT"].Value.ToString()))
                    {
                        //累计重量
                        ACTWeight = ACTWeight + Convert.ToInt32(dgr.Cells["ACT_WEIGHT"].Value.ToString());
                    }
                }
                if (REQWeight > 0)
                {
                    lb_REQ_WEIGHT.Text = Convert.ToString(REQWeight);
                }
                if (ACTWeight > 0)
                {
                    lb_ACT_WEIGHT.Text = Convert.ToString(ACTWeight);
                }

                //MessageBox.Show("没有记录可以查看");
                //return;
            }
            //this.dgvStowageMessage
            isPopupMessage = false;
        }
        private bool CreatDgvHeader(DataGridView dataGridView, string[] columnsName, string[] headerText)
        {
            bool isFirst = false;
            if (!isFirst)
            {
                dataGridView.ReadOnly = false;
                for (int i = 0; i < headerText.Count(); i++)
                {
                    int index = 0;
                    if (columnsName[i].Equals("MAT_CNAME"))
                    {
                        DataGridViewComboBoxColumn column1 = new DataGridViewComboBoxColumn();
                        column1.Name = columnsName[i];
                        column1.DataPropertyName = columnsName[i];//对应数据源的字段
                        column1.HeaderText = headerText[i];
                        column1.ReadOnly = false;
                        if (i > 0)
                        {
                            column1.Width = 130;
                        }                       
                        column1.DataSource = L3MatInfoLists;
                        index = dataGridView.Columns.Add(column1);
                    }
                    else if (columnsName[i].Equals("REQ_WEIGHT") || columnsName[i].Equals("ACT_WEIGHT"))
                    {
                        DataGridViewTextBoxColumn targetColumn = new DataGridViewTextBoxColumn();                        
                        targetColumn.DataPropertyName = columnsName[i];
                        targetColumn.Name = columnsName[i];
                        targetColumn.HeaderText = headerText[i];
                        targetColumn.MaxInputLength = 5;
                        if (columnsName[i].Equals("REQ_WEIGHT"))
                        {
                            targetColumn.ReadOnly = false;
                        }
                        else if (columnsName[i].Equals("ACT_WEIGHT"))
                        {
                            targetColumn.ReadOnly = false;
                        }
                        else
                        {
                            targetColumn.ReadOnly = true;
                        }
                        index = dataGridView.Columns.Add(targetColumn);
                    }
                    else if (columnsName[i].Equals("BTN_UP"))
                    {
                        //在datagridview中添加第一个button按钮
                        DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
                        btn1.Name = "btn_UP";
                        btn1.HeaderText = "排序";
                        btn1.DefaultCellStyle.NullValue = "";
                        btn1.Width = 46;
                        index = dataGridView.Columns.Add(btn1);

                        //在datagridview中添加第二个button按钮
                        //DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
                        //btn2.Name = "Button2";
                        //btn2.HeaderText = "按钮2";
                        //btn2.DefaultCellStyle.NullValue = "按钮2";
                        //index = dataGridView.Columns.Add(btn2);
                    }
                    else
                    {
                        DataGridViewColumn column = new DataGridViewTextBoxColumn();
                        column.DataPropertyName = columnsName[i];
                        column.Name = columnsName[i];
                        column.HeaderText = headerText[i];
                        if (columnsName[i].Equals("FROM_STOCK_NO"))
                        {
                            column.ReadOnly = false;
                        }
                        else if (columnsName[i].Equals("TO_STOCK_NO")) 
                        {
                            column.ReadOnly = false;
                        }
                        //else if (columnsName[i].Equals("REQ_WEIGHT"))
                        //{
                        //    column.ReadOnly = false;
                        //}
                        //else if (columnsName[i].Equals("ACT_WEIGHT"))
                        //{
                        //    column.ReadOnly = false;
                        //}
                        //else
                        //{
                        //    column.ReadOnly = true;
                        //}                        
                        if (i > 0)
                        {
                            column.Width = 80;
                        }
                        if (columnsName[i].Equals("START_TIME"))
                        {
                            column.Width = 125;
                        }
                        if (columnsName[i].Equals("UPD_TIME"))
                        {
                            column.Width = 125;
                        }
                        if (columnsName[i].Equals("REC_TIME"))
                        {
                            column.Width = 125;
                        }
                        if (columnsName[i].Equals("MAT_CODE"))
                        {
                            column.Visible = false;
                        }
                        if (columnsName[i].Equals("ORDER_NO"))
                        {
                            column.Visible = false;
                        }
                        index = dataGridView.Columns.Add(column);
                        
                    }
                    dataGridView.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                isFirst = true;
                return isFirst;
            }
            else
                return isFirst;
        }
        private void CreateComboBoxColumn()
        {
            DataGridViewComboBoxColumn column1 = new DataGridViewComboBoxColumn();
            column1.Name = "MAT_CNAME2";
            column1.DataPropertyName = "MAT_CNAME2";//对应数据源的字段
            column1.HeaderText = "物料2";
            column1.Width = 130;
            this.dgvStowageMessage.Columns.Add(column1);
            column1.DataSource = L3MatInfoLists; //这里需要设置一下combox的itemsource,以便combox根据数据库中对应的值自动显示信息

        }
        /// <summary>
        /// 刷新 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Refurbish_Click(object sender, EventArgs e)
        {
            Refurbish();
            //ParkClassLibrary.HMILogger.WriteLog("停车位详细", "刷新", ParkClassLibrary.LogLevel.Info, this.Text);
        }

        /// <summary>
        /// 保存 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Save_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (dgvStowageMessage.DataSource != null && Initial_dgv != null && dgvStowageMessage.Rows.Count > 0 && Initial_dgv.Rows.Count > 0)
            {
                try
                {
                    //确认提示
                    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                    DialogResult drmsg = MessageBox.Show("确认是否修改数据？", "提示", btn, MessageBoxIcon.Asterisk);
                    if (drmsg == DialogResult.OK)
                    {
                        DataTable TotalDT = (DataTable)dgvStowageMessage.DataSource;
                        DataTable dt = TotalDT.Clone();
                        var count = 0;
                        var isTrue = false;
                        var planNo = "";
                        var orderNo = "";
                        var ExecuteNonQueryCount = 0;
                        foreach (DataGridViewRow dgr in dgvStowageMessage.Rows)
                        {
                            isTrue = false;
                            foreach (DataRow Initdgr in Initial_dgv.Rows)
                            {
                                var ddd = Initdgr["MAT_CNAME"].ToString();
                                var MAT_CNAME2 = dgr.Cells["MAT_CNAME"].Value.ToString();
                                var qq = Initdgr["FROM_STOCK_NO"].ToString();
                                var qqq = dgr.Cells["FROM_STOCK_NO"].Value.ToString();
                                //判断计划号和指令号是否空，判断dgvStowageMessage计划号和Initial_dgv计划号是否一致，判断dgvStowageMessage指令号和Initial_dgv指令号是否一致
                                if (!string.IsNullOrEmpty(dgr.Cells["PLAN_NO"].Value.ToString()) && !string.IsNullOrEmpty(Initdgr["ORDER_NO"].ToString()) && dgr.Cells["PLAN_NO"].Value.ToString().Equals(Initdgr["PLAN_NO"].ToString()) && dgr.Cells["ORDER_NO"].Value.ToString().Equals(Initdgr["ORDER_NO"].ToString()))
                                {
                                    //判断物料是否更改
                                    if (!dgr.Cells["MAT_CNAME"].Value.ToString().Equals(Initdgr["MAT_CNAME"].ToString()))
                                    {
                                        //物料
                                        var dd = dgr.Cells["MAT_CNAME"].Value.ToString();
                                        count++;
                                        isTrue = true;
                                    }
                                    if (!dgr.Cells["REQ_WEIGHT"].Value.ToString().Equals(Initdgr["REQ_WEIGHT"].ToString()))
                                    {
                                        //要求重量
                                        count++;
                                        isTrue = true;
                                    }
                                    if (!dgr.Cells["ACT_WEIGHT"].Value.ToString().Equals(Initdgr["ACT_WEIGHT"].ToString()))
                                    {
                                        //实绩重量
                                        count++;
                                        isTrue = true;
                                    }
                                    if (!dgr.Cells["FROM_STOCK_NO"].Value.ToString().Equals(Initdgr["FROM_STOCK_NO"].ToString()))
                                    {
                                        //取料位
                                        count++;
                                        isTrue = true;
                                    }
                                    if (!dgr.Cells["TO_STOCK_NO"].Value.ToString().Equals(Initdgr["TO_STOCK_NO"].ToString()))
                                    {
                                        //落料位
                                        count++;
                                        isTrue = true;
                                    }
                                }
                            }
                            if (isTrue)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ORDER_NO"] = dgr.Cells["ORDER_NO"].Value.ToString();
                                //dr["ORDER_GROUP_NO"] = dgr.Cells["ORDER_GROUP_NO"].Value.ToString();
                                //dr["MAT_CNAME2"] = dgr.Cells["MAT_CNAME2"].Value.ToString();
                                dr["FROM_STOCK_NO"] = dgr.Cells["FROM_STOCK_NO"].Value.ToString();
                                dr["TO_STOCK_NO"] = dgr.Cells["TO_STOCK_NO"].Value.ToString();
                                dr["REQ_WEIGHT"] = dgr.Cells["REQ_WEIGHT"].Value.ToString();
                                dr["ACT_WEIGHT"] = dgr.Cells["ACT_WEIGHT"].Value.ToString();

                                dr["MAT_CODE"] = dgr.Cells["MAT_CODE"].Value.ToString();
                                dr["MAT_CNAME"] = dgr.Cells["MAT_CNAME"].Value.ToString();
                                //dr["EXE_SEQ"] = dgr.Cells["EXE_SEQ"].Value.ToString();
                                dr["ORDER_PRIORITY"] = dgr.Cells["ORDER_PRIORITY"].Value.ToString();
                                dr["CMD_SEQ"] = dgr.Cells["CMD_SEQ"].Value.ToString();
                                dr["CMD_STATUS"] = dgr.Cells["CMD_STATUS"].Value.ToString().Trim();
                                dr["PLAN_NO"] = dgr.Cells["PLAN_NO"].Value.ToString();
                                //dr["UPD_TIME"] = dgr.Cells["UPD_TIME"].Value.ToString();
                                //dr["REC_TIME"] = dgr.Cells["REC_TIME"].Value.ToString();
                                dt.Rows.Add(dr);
                                //dt.Rows.Add(dgr);
                                //dt.ImportRow(dgr);
                            }
                        }
                        if (count <= 0)
                        {
                            MessageBoxButtons btn2 = MessageBoxButtons.OK;
                            DialogResult drmsg2 = MessageBox.Show("保存失败，无数据更改！", "提示", btn, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            string ExeSql = @" UPDATE UACS_ORDER_QUEUE SET  ";
                            foreach (KeyValuePair<string, string> keyvalue in MatCode)
                            {
                                if (keyvalue.Value.Equals(dr["MAT_CNAME"].ToString()))
                                {
                                    ExeSql += " MAT_CODE = '" + keyvalue.Key + "' ,";
                                    ExeSql += " COMP_CODE = '" + keyvalue.Key + "' ,";
                                    break;
                                }
                            }
                            ExeSql += " FROM_STOCK_NO = '" + dr["FROM_STOCK_NO"].ToString() + "' ";
                            ExeSql += " ,TO_STOCK_NO = '" + dr["TO_STOCK_NO"].ToString() + "' ";
                            ExeSql += " ,REQ_WEIGHT = '" + dr["REQ_WEIGHT"].ToString() + "' ";
                            ExeSql += " ,CAL_WEIGHT = '" + dr["REQ_WEIGHT"].ToString() + "' ";
                            ExeSql += " ,ACT_WEIGHT = '" + dr["ACT_WEIGHT"] + "' ";
                            //ExeSql += " ,EXE_SEQ = '" + dr["EXE_SEQ"].ToString() + "' ";
                            ExeSql += " ,ORDER_PRIORITY = '" + dr["ORDER_PRIORITY"].ToString() + "' ";
                            ExeSql += " ,CMD_SEQ = '" + dr["CMD_SEQ"].ToString() + "' ";
                            //ExeSql += " ,CMD_STATUS = '" + dr["CMD_STATUS"] + "' ";

                            ExeSql += " WHERE 1=1 ";
                            ExeSql += " AND PLAN_NO = '" + dr["PLAN_NO"] + "'";
                            ExeSql += " AND ORDER_NO = " + Convert.ToInt32(dr["ORDER_NO"]) + " ";
                            //ExeSql += " AND ORDER_GROUP_NO = " + Convert.ToInt32(dr["ORDER_GROUP_NO"]) + " ; ";
                            //planNo = planNo + dr["PLAN_NO"] + ",";
                            orderNo = orderNo + dr["ORDER_NO"] + ",";
                            DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
                        }

                        //DB2Connect.DBHelper.ExecuteNonQuery(sql);
                        ParkClassLibrary.HMILogger.WriteLog("停车位详细", "指令号：" + orderNo, ParkClassLibrary.LogLevel.Info, this.Text);
                        Refurbish(); //刷新页面
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxButtons btn = MessageBoxButtons.OK;
                    DialogResult drmsg = MessageBox.Show("保存失败，无数据更改！", "提示", btn, MessageBoxIcon.Warning);
                }
            }
            isPopupMessage = false;
        }

        private void GetMAT_Code()
        {
            isPopupMessage = true;
            MatCode.Clear();
            string sql = @" SELECT MAT_CODE,MAT_CNAME FROM UACS_L3_MAT_INFO ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    MatCode.Add(rdr["MAT_CODE"].ToString(), rdr["MAT_CNAME"].ToString());
                }
            }
            isPopupMessage = false;
        }

        /// <summary>
        /// 获取配载信息
        /// </summary>
        /// <param name="theStowageId">配载号</param>
        /// <param name="carNo">车辆号</param>
        /// <param name="packingNo">停车位号</param>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public void Get_L3_MAT_INFO(string carNo, string packingNo)
        {
            DataTable dtNull = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(carNo) && packingNo.Contains('A') && packingNo != "请选择")
                {
                    //对应指令车辆配载明细
                    string sqlText_ORDER = @"SELECT A.ORDER_NO,A.ORDER_GROUP_NO,A.ORDER_PRIORITY,CMD_SEQ,
                                                    CASE 
                                                    WHEN A.CMD_STATUS = 0 THEN '初始化' 
                                                    WHEN A.CMD_STATUS = 1 THEN '获取指令' 
                                                    WHEN A.CMD_STATUS = 2 THEN '激光扫描' 
                                                    WHEN A.CMD_STATUS = 3 THEN '到取料点上方' 
                                                    WHEN A.CMD_STATUS = 4 THEN '空载下降到位' 
                                                    WHEN A.CMD_STATUS = 5 THEN '有载荷量' 
                                                    WHEN A.CMD_STATUS = 6 THEN '重载上升到位' 
                                                    WHEN A.CMD_STATUS = 7 THEN '到放料点上方' 
                                                    WHEN A.CMD_STATUS = 8 THEN '重载下降到位' 
                                                    WHEN A.CMD_STATUS = 9 THEN '无载荷量' 
                                                    WHEN A.CMD_STATUS = 10 THEN '空载上升到位' 
                                                    ELSE '其他' 
                                                    END AS CMD_STATUS
                                                    ,A.PLAN_NO,A.MAT_CODE,B.MAT_CNAME ,A.FROM_STOCK_NO,A.TO_STOCK_NO,A.REQ_WEIGHT,A.ACT_WEIGHT,A.START_TIME,A.UPD_TIME,A.REC_TIME 
                                                    FROM UACS_ORDER_QUEUE AS A ";
                    sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                    sqlText_ORDER += " WHERE A.CMD_STATUS = '0' AND A.CAR_NO = '{0}' AND A.TO_STOCK_NO = '{1}' ";
                    sqlText_ORDER += " ORDER BY A.ORDER_PRIORITY,A.ORDER_NO ";
                    sqlText_ORDER = string.Format(sqlText_ORDER, carNo, packingNo);
                    DataTable dt = new DataTable();
                    using (IDataReader odrIn = DB2Connect.DBHelper.ExecuteReader(sqlText_ORDER))
                    {
                        dt.Load(odrIn);
                        dgvStowageMessage.DataSource = dt;
                        //dgvStowageMessage.DataSource = Initial_dgv;
                        foreach (DataGridViewColumn item in dgvStowageMessage.Columns)
                        {
                            if (item.Name == "Index")
                            {
                                for (int y = 0; y < dgvStowageMessage.Rows.Count - 1; y++)
                                {
                                    dgvStowageMessage.Rows[y].Cells["Index"].Value = y;
                                }
                                break;
                            }
                        }
                        odrIn.Close();
                    }
                    //dt.Dispose();
                }
                else
                {
                    dgvStowageMessage.DataSource = dtNull;
                    dtNull.Dispose();
                }
            }
            catch (Exception ex)
            { }
        }
               

        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 单元格内容发生更改时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStowageMessage_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //得到选中行的索引
            int intRow = dgvStowageMessage.SelectedCells[0].RowIndex;
            //得到列的索引
            int intColumn = dgvStowageMessage.SelectedCells[0].ColumnIndex;
            //var sss = dgvStowageMessage.Rows[intRow].Cells["cbChoice"].Value.ToString();
            //dgvStowageMessage.Rows[intRow].Cells["cbChoice"].Value = 1;
            //var dddd = dgvStowageMessage.Rows[intRow].Cells["cbChoice"].Value.ToString();
            //得到选中行某列的值
            string str = dgvStowageMessage.CurrentRow.Cells[2].Value.ToString();
            var data1 = dgvStowageMessage.Rows[intRow].Cells[intColumn].Value.ToString();
            var data2 = Initial_dgv.Rows[intRow].ItemArray[intColumn];
            if (data2.Equals(data1))
            {
                //dgvStowageMessage.Rows[intRow].Cells[intColumn].Style.ForeColor = Color.Red;
                dgvStowageMessage.Rows[intRow].Cells[intColumn].Style.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                //DataGridViewCell aa = dgvStowageMessage.Rows[intRow].Cells[intColumn];
                //aa.Style.ForeColor = Color.Red;
                //aa.Style.BackColor = Color.LightGreen;
                //dgvStowageMessage.Rows[intRow].Cells[intColumn].Style.ForeColor = Color.Red;
                dgvStowageMessage.Rows[intRow].Cells[intColumn].Style.BackColor = Color.LightGreen;
                

            }

        }

        /// <summary>
        /// 在编辑模式下绘制单元格。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStowageMessage_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvStowageMessage.CurrentCell.ColumnIndex == 10 || this.dgvStowageMessage.CurrentCell.ColumnIndex == 11)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dgvStowageMessage_KeyPress);
            }
        }

        /// <summary>
        /// 不处于编辑模式时绘制单元格。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStowageMessage_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            isPopupMessage = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dgvStowageMessage.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewComboBoxCell)
                {
                    var cell = this.dgvStowageMessage.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                    var foreColor = cell.Style.ForeColor.Name == "0" ? Color.Black : cell.Style.ForeColor;

                    e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
                    e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentBackground);

                    using (Brush forebrush = new SolidBrush(foreColor))
                    using (Brush backbrush = new SolidBrush(cell.Style.BackColor))
                    using (StringFormat format = new StringFormat())
                    {
                        Rectangle rect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 19, e.CellBounds.Height - 3);
                        format.LineAlignment = StringAlignment.Center;

                        e.Graphics.FillRectangle(backbrush, rect);
                        e.Graphics.DrawString(cell.FormattedValue.ToString(), e.CellStyle.Font, forebrush, rect, format);
                    }
                    e.Paint(e.ClipBounds, DataGridViewPaintParts.ErrorIcon);
                    e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);
                    e.Handled = true;
                }

                if (dgvStowageMessage.Columns[e.ColumnIndex].Name == "btn_UP" && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    e.Graphics.DrawImage(global::UACSPopupForm.Properties.Resources.up_btn, e.CellBounds);
                    e.Handled = true;
                }
            }
            isPopupMessage = false;
        }

        // 手动绘制组合框.
        private void cb_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            // Non-sourced vs sourced examples.
            string value = cb.Items[e.Index].ToString();
            // string value = (cb.DataSource as DataTable).Rows[e.Index].ItemArray[SourceColumnIndex];

            if (e.Index >= 0)
            {
                using (Brush forebrush = new SolidBrush(cb.ForeColor))
                using (Brush backbrush = new SolidBrush(cb.BackColor))
                {
                    e.Graphics.FillRectangle(backbrush, e.Bounds);
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    e.Graphics.DrawString(value, e.Font, forebrush, e.Bounds);
                }
            }
        }

        /// <summary>
        /// 如果输入的不是退格和数字，则屏蔽输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStowageMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void dgvStowageMessage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStowageMessage.Columns[e.ColumnIndex].Name == "btn_UP" && e.RowIndex >= 0)
            {
                try
                {
                    // 处理第一个按钮的点击事件
                    //得到选中行的索引
                    int intRow = dgvStowageMessage.SelectedCells[0].RowIndex;
                    if (intRow > 0)
                    {
                        //得到列的索引
                        int intColumn = dgvStowageMessage.SelectedCells[0].ColumnIndex;
                        var mini = Convert.ToInt32(dgvStowageMessage.Rows[intRow - 1].Cells["ORDER_PRIORITY"].Value.ToString());
                        var max = Convert.ToInt32(dgvStowageMessage.Rows[intRow].Cells["ORDER_PRIORITY"].Value.ToString());
                        var orderno1 = dgvStowageMessage.Rows[intRow - 1].Cells["ORDER_NO"].Value.ToString();
                        var orderno2 = dgvStowageMessage.Rows[intRow].Cells["ORDER_NO"].Value.ToString();
                        var planno = dgvStowageMessage.Rows[intRow].Cells["PLAN_NO"].Value.ToString();
                        var matcname = dgvStowageMessage.Rows[intRow].Cells["MAT_CNAME"].Value.ToString();
                        if (max > mini)
                        {
                            var ExeSql = @"UPDATE UACS_ORDER_QUEUE SET ORDER_PRIORITY = '" + max + "' WHERE ORDER_NO = '" + orderno1 + "' AND PLAN_NO = '" + planno + "'; ";
                            ExeSql += "UPDATE UACS_ORDER_QUEUE SET ORDER_PRIORITY = '" + mini + "' WHERE ORDER_NO = '" + orderno2 + "' AND PLAN_NO = '" + planno + "'; ";
                            DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
                            //刷新
                            Refurbish();
                            ParkClassLibrary.HMILogger.WriteLog("停车位详细", "更改优先级，计划号：" + planno + "，指令号：" + orderno2 + "，物料：" + matcname + "，优先级：" + mini + "，旧优先级：" + max, ParkClassLibrary.LogLevel.Info, this.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ParkClassLibrary.HMILogger.WriteLog("停车位详细", "更改优先级错误："+ ex.Message.ToString().Trim(), ParkClassLibrary.LogLevel.Info, this.Text);
                }
            }
        }

        /// <summary>
        /// 提交实绩
        /// </summary>
        private void bt_Send_Click(object sender, EventArgs e)
        {
            Send_Tag();
        }

        /// <summary>
        /// 发送实绩
        /// </summary>
        private void Send_Tag()
        {            
            //确认提示
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否发送实绩？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                if (dgvStowageMessage.Rows.Count > 0)
                {
                    //计划号，料槽号，工位号，废钢种类数量，废钢代码1#重量，废钢代码2#重量，废钢代码3#重量，废钢代码4#重量，废钢代码5#重量......
                    //Tag发送的数据
                    var Data = "";
                    //计划号
                    string sPlanNO = dgvStowageMessage.CurrentRow.Cells["PLAN_NO"].Value.ToString();
                    //料槽车号
                    string sCarNo = !string.IsNullOrEmpty(lblCarNo.Text.ToString().Trim()) ? lblCarNo.Text.ToString().Trim() : "";
                    //停车位号（落料位）
                    string sToStockNo = dgvStowageMessage.CurrentRow.Cells["TO_STOCK_NO"].Value.ToString();
                    //废钢种类数量
                    var count = dgvStowageMessage.Rows.Count;
                    Data += sPlanNO + "," + sCarNo + "," + sToStockNo + "," + count;


                    foreach (DataGridViewRow dgr in dgvStowageMessage.Rows)
                    {
                        var sMatCode = "";
                        var sActWeight = "";
                        if (!string.IsNullOrEmpty(dgr.Cells["MAT_CODE"].Value.ToString()))
                        {
                            //要求重量
                            sMatCode =  dgr.Cells["MAT_CODE"].Value.ToString();
                        }
                        if (!string.IsNullOrEmpty(dgr.Cells["ACT_WEIGHT"].Value.ToString()))
                        {
                            //累计重量
                            sActWeight = dgr.Cells["ACT_WEIGHT"].Value.ToString();
                        }
                        Data += "," + sMatCode + "#" + sActWeight;
                    }

                    tagDP.SetData("EV_L3MSG_SND_BHKI02", Data);
                    DialogResult dr = MessageBox.Show("停车位详细实绩发送成功！", "提示", MessageBoxButtons.OK);
                    ParkClassLibrary.HMILogger.WriteLog("停车位详细实绩发送", "实绩发送：" + Data, ParkClassLibrary.LogLevel.Info, this.Text);
                }                
            }
        }
    }
}
