using Baosight.iSuperframe.TagService;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSParking
{
    /*
     * 文件名：FrmCarEntry.cs
     * 作者：邓赵平
     * 描述：车到位管理，该画面用于车辆到位操作，车辆到达现场停放好后后，可进行车到位信息确认，并通知行车对料槽车进行扫描
     * 修改人：邓赵平
     * 修改时间：2022-12-15
     * 修改内容：新增[装料模式],[扫描行车]
     */
    public partial class FrmCarEntry : Form
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public FrmCarEntry()
        {
            InitializeComponent();
            this.Load += FrmCarEntry_Load;
        }
        /// <summary>
        /// 停车位
        /// </summary>
        public string PackingNo { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string CarNo { get; set; }        
        private string carType = "";
        string carFlag = "1";
        string carDirection = "";

        public string CarType
        {
            get { return carType; }
            set { carType = value; }
        }
        Int16 carTypeValue1550 = 0;

        public Int16 CarTypeValue1550
        {
            get { return carTypeValue1550; }
            set { carTypeValue1550 = value; }
        }

        void FrmCarEntry_Load(object sender, EventArgs e)
        {

            //cmbCarType.SelectedText = "热卷框架";
            cmbCarType.SelectedText = "装料车";
            txtPacking.Text = PackingNo;
            txtCarNo.Text = CarNo;
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add("EV_PARKING_CARARRIVE", null);
            tagDP.Attach(TagValues);

            this.Text = string.Format("{0}到位", carType);
            if (carType == "100")
            {
                txtDirection.Enabled = false;
                txtDirection.Text = "南";
                txtDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

                cmbCarType.Enabled = false;
                //cmbCarType.SelectedText = "普通框架";
                cmbCarType.Text = "普通框架";
                cmbCarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            }
            else if (carType == "101")
            {
                txtDirection.Text = "";
                //cmbCarType.SelectedText = "一般社会车辆";
                cmbCarType.Text = "一般社会车辆";
                cmbCarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            }
            else if (carType == "ALL")
            {

            }
            else
            {
                cmbCarType.Text = "车辆出库车到位";
                cmbCarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            }
            BindCarDrection();//绑定方向
            BindCarType(cmbCarType);
            //绑定装料模式
            BindCmbWordMode();
            //绑定扫描行车
            BindCmbScanCar();
            //txtDirection.Text = "";
            //if (PackingNo.Contains("Z3"))
            //{
            //    labTips.Text = "朝4-1门为南，朝4-4门为北";
            //}
            //else if (PackingNo.Contains("Z5"))
            //{
            //    labTips.Text = "朝7-1门为东，朝7-9门为西"; 
            //}
        }
        private void BindCarDrection()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");

            DataRow dr = dt.NewRow();
            //if (PackingNo.Contains("Z0"))
            //{
            dr = dt.NewRow();
            dr["TypeValue"] = "E";
            dr["TypeName"] = "东";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["TypeValue"] = "W";
            dr["TypeName"] = "西";
            dt.Rows.Add(dr);
            //}
            //else
            //{
            //dr = dt.NewRow();
            //dr["TypeValue"] = "S";
            //dr["TypeName"] = "南";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["TypeValue"] = "N";
            //dr["TypeName"] = "北";
            //dt.Rows.Add(dr);
            //}

            //绑定列表下拉框数据
            this.txtDirection.DisplayMember = "TypeName";
            this.txtDirection.ValueMember = "TypeValue";
            this.txtDirection.DataSource = dt;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 确定保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!txtPacking.Text.ToString().Contains('A') || txtPacking.Text.ToString().Trim() == "请选择")
            {
                MessageBox.Show("请先选择停车位！！", "提示");
                this.Close();
                return;
            }
            //框架车
            if (carType == "框架车" || carType == "装料车")
            {
                //txtDirection.Text = "南";
                // txtDirection.Enabled = false ;
                txtDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                //txtDirection
                if (txtDirection.Text.Trim() == "南")
                {
                    carDirection = "S";
                }
                else if (txtDirection.Text.Trim() == "北")
                {
                    carDirection = "N";
                }
                else if (txtDirection.Text.Trim() == "东")
                {
                    carDirection = "E";
                }
                else if (txtDirection.Text.Trim() == "西")
                {
                    carDirection = "W";
                }
                else
                {
                    MessageBox.Show("请输入车头方向！", "提示");
                    return;
                }
                if (txtCarNo.Text.Length < 4)
                {
                    MessageBox.Show("请输入四位以上的车牌号！", "提示");
                    return;
                }
                if (txtCarNo.Text.Trim() != "" || txtDirection.Text.Trim() != "" || txtFlag.Text.Trim() != "" || txtPacking.Text.Trim() != "")
                {
                    ////操作人|日期|班次|班组|停车位|车号|空满标记|车头方向|载重能力|设备号
                    ////车头位置(东：E 西：W 南：S 北：N)
                    //StringBuilder sb = new StringBuilder("HMI");
                    //sb.Append("|");
                    //sb.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    //sb.Append("|");
                    //sb.Append("1");
                    //sb.Append("|");
                    //sb.Append("1");
                    //sb.Append("|");
                    //sb.Append(txtPacking.Text.Trim());
                    //sb.Append("|");
                    //sb.Append(txtCarNo.Text.ToUpper().Trim());
                    //sb.Append("|");
                    //sb.Append(carFlag.Trim());
                    //sb.Append("|");
                    //// sb.Append(carDirection.Trim());
                    //sb.Append(txtDirection.SelectedValue.ToString().Trim());
                    //sb.Append("|");
                    //sb.Append("90");
                    //sb.Append("|");
                    //if (txtFlag.Text.Trim().Equals("空"))
                    //{
                    //    sb.Append("0");
                    //}
                    //else
                    //{
                    //    sb.Append("1");
                    //}                    
                    //sb.Append("|");
                    //string carTypeValue = cmbCarType.SelectedValue.ToString().Trim();
                    //sb.Append(carTypeValue); //100
                    //sb.Append("|");
                    //sb.Append("0");

                    //操作人 | 日期 | 班次 | 班组 | 停车位 | 车号 | 空重标记 | 车头朝向 | 装料模式 | 扫描行车 | 车辆类型 | 停车位类型
                    //操作人|日期|班次|班组|停车位|车号|空满标记|车头方向|载重能力|设备号
                    //车头位置(东：E 西：W 南：S 北：N)
                    StringBuilder sb = new StringBuilder("HMI");  //操作人
                    sb.Append("|");
                    sb.Append(DateTime.Now.ToString("yyyyMMddHHmmss")); //日期
                    sb.Append("|");
                    sb.Append("1");  //班次
                    sb.Append("|");
                    sb.Append("1");  //班组
                    sb.Append("|");
                    sb.Append(txtPacking.Text.Trim());   //停车位
                    sb.Append("|");
                    sb.Append(txtCarNo.Text.ToUpper().Trim());   //车号
                    sb.Append("|");
                    if (txtFlag.Text.Trim().Equals("空"))
                    {
                        sb.Append("0");  //空重标记 0：空
                    }
                    else
                    {
                        sb.Append("1");  //空重标记 1：满
                    }
                    //sb.Append(carFlag.Trim());
                    sb.Append("|");
                    // sb.Append(carDirection.Trim());
                    sb.Append(txtDirection.SelectedValue.ToString().Trim());   //车头朝向
                    sb.Append("|");
                    //sb.Append("90");
                    sb.Append(cmbWordMode.SelectedValue.ToString().Trim());   //装料模式
                    sb.Append("|");
                    sb.Append("0"); //扫描行车
                    sb.Append("|");
                    string carTypeValue = cmbCarType.SelectedValue.ToString().Trim();
                    sb.Append(carTypeValue); //车辆类型
                    sb.Append("|");
                    sb.Append("0");   //停车位类型


                    //DialogResult dResult = MessageBox.Show(sb.ToString(), "调试", MessageBoxButtons.YesNo);
                    //if (dResult == DialogResult.No)
                    //{
                    //    return;
                    //}
                    tagDP.SetData("EV_PARKING_CARARRIVE", sb.ToString());
                    DialogResult dr = MessageBox.Show("框架车车到位成功，激光扫描开始，请保证车位上方没有行车经过。", "提示", MessageBoxButtons.OK);
                    carTypeValue1550 = Int16.Parse(carTypeValue);
                    ParkClassLibrary.HMILogger.WriteLog(button1.Text, "车到位：" + sb.ToString(), ParkClassLibrary.LogLevel.Info, this.Text);
                    if (dr == DialogResult.OK)
                    {
                        this.Close();
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("请填写完整");
            }
            else if (carType == "社会车" || carType == "较低社会车辆" || carType == "ALL")
            {
                if (txtDirection.Text.Trim() == "东")
                {
                    carDirection = "E";
                }
                else if (txtDirection.Text.Trim() == "西")
                {
                    carDirection = "W";
                }
                else if (txtDirection.Text.Trim() == "南")
                {
                    carDirection = "S";
                }
                else if (txtDirection.Text.Trim() == "北")
                {
                    carDirection = "N";
                }
                else
                {
                    MessageBox.Show("请输入车头方向！", "提示");
                    return;
                }
                if (txtCarNo.Text.Length < 4)
                {
                    MessageBox.Show("请输入四位以上的车牌号！", "提示");
                    return;
                }
                if (txtCarNo.Text.Trim() != "" || txtDirection.Text.Trim() != "" || txtFlag.Text.Trim() != "" || txtPacking.Text.Trim() != "")
                {
                    //操作人|日期|班次|班组|停车位|车号|空满标记|车头方向|载重能力|设备号|车辆类型
                    //车头位置(东：E 西：W 南：S 北：N)
                    StringBuilder sb = new StringBuilder("HMI");
                    sb.Append("|");
                    sb.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    sb.Append("|");
                    sb.Append("1");
                    sb.Append("|");
                    sb.Append("1");
                    sb.Append("|");
                    sb.Append(txtPacking.Text.Trim());
                    sb.Append("|");
                    sb.Append(txtCarNo.Text.ToUpper().Trim());
                    sb.Append("|");
                    sb.Append(carFlag.Trim());
                    sb.Append("|");
                    //sb.Append(carDirection.Trim());
                    sb.Append(txtDirection.SelectedValue.ToString().Trim());
                    sb.Append("|");
                    sb.Append("90");
                    sb.Append("|");
                    if (txtFlag.Text.Trim().Equals("空"))
                    {
                        sb.Append("0");
                    }
                    else
                    {
                        sb.Append("1");
                    }
                    sb.Append("|");
                    string carTypeValue = cmbCarType.SelectedValue.ToString().Trim();
                    sb.Append(carTypeValue);
                    //sb.Append("101");
                    sb.Append("|");
                    if (carTypeValue == "2")
                    {
                        sb.Append("2");
                    }
                    else
                    {
                        sb.Append("1");
                    }
                    //DialogResult dResult = MessageBox.Show(sb.ToString(), "调试", MessageBoxButtons.YesNo);
                    //if (dResult == DialogResult.No)
                    //{
                    //    return;
                    //}
                    tagDP.SetData("EV_PARKING_CARARRIVE", sb.ToString());
                    carTypeValue1550 = Int16.Parse(carTypeValue);
                    DialogResult dr = MessageBox.Show("装料车车到位成功，激光扫描开始，请保证车位上方没有行车经过。", "提示", MessageBoxButtons.OK);
                    ParkClassLibrary.HMILogger.WriteLog("车到位", "车到位：" + sb.ToString(), ParkClassLibrary.LogLevel.Info, this.Text);
                    if (dr == DialogResult.OK)
                    {
                        this.Close();
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("请填写完整");
            }

            else
                MessageBox.Show("不存在该出库类型！");
        }

        private void FrmCarEntry_Activated(object sender, EventArgs e)
        {
            txtCarNo.Focus();
        }

        private void txtCarNo_TextChanged(object sender, EventArgs e)
        {
            string UpTem = txtCarNo.Text;
            txtCarNo.Text = UpTem.ToUpper().Trim();
            txtCarNo.SelectionStart = txtCarNo.Text.Length;
            txtCarNo.SelectionLength = 0;
        }
        private void BindCarType(ComboBox cmbBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");

            DataRow dr;
            if (carType == "社会车")
            {
                dr = dt.NewRow();
                dr["TypeValue"] = "101";
                dr["TypeName"] = "一般社会车辆";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "103";
                dr["TypeName"] = "较低社会车辆";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "104";
                dr["TypeName"] = "雨棚车";
                dt.Rows.Add(dr);
            }
            else if (carType == "框架车")
            {
                dr = dt.NewRow();
                dr["TypeValue"] = "100";
                dr["TypeName"] = "普通框架";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "106";
                dr["TypeName"] = "热卷框架";
                dt.Rows.Add(dr);
            }
            else if (carType == "ALL")
            {
                //dr = dt.NewRow();
                //dr["TypeValue"] = "106";
                //dr["TypeName"] = "热卷框架";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "101";
                //dr["TypeName"] = "一般社会车";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "103";
                //dr["TypeName"] = "较低社会车";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "102";
                //dr["TypeName"] = "大头娃娃车";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "100";
                //dr["TypeName"] = "普通框架";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "104";
                //dr["TypeName"] = "雨棚车";
                //dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "200";
                //dr["TypeName"] = "木架卷车";
                //dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "2";
                dr["TypeName"] = "装料车";
                dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "1";
                //dr["TypeName"] = "卡车";
                //dt.Rows.Add(dr);
            }
            else if (carType == "装料车")
            {
                dr = dt.NewRow();
                dr["TypeValue"] = "2";
                dr["TypeName"] = "装料车";
                dt.Rows.Add(dr);

                //dr = dt.NewRow();
                //dr["TypeValue"] = "1";
                //dr["TypeName"] = "卡车";
                //dt.Rows.Add(dr);
            }

            //绑定列表下拉框数据

            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.DataSource = dt;
            cmbBox.SelectedIndex = 0;
        }

        private void cmbCarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCarType.SelectedValue != null && (int.Parse(cmbCarType.SelectedValue.ToString()) == 100 || int.Parse(cmbCarType.SelectedValue.ToString()) == 106))
            {
                txtDirection.Enabled = true;
                //txtDirection.Text = "南";
                carType = "框架车";
                button1.Enabled = true;
            }
            else if (cmbCarType.SelectedValue != null && int.Parse(cmbCarType.SelectedValue.ToString()) == 102)
            {
                txtDirection.Enabled = true;
                //txtDirection.Text = "南";
                carType = "ALL";
                button1.Enabled = true;
            }
            else if (cmbCarType.SelectedValue != null && int.Parse(cmbCarType.SelectedValue.ToString()) == 2)
            {
                txtDirection.Enabled = true;
                //txtDirection.Text = "南";
                carType = "装料车";
                button1.Enabled = true;
            }
            else
            {
                txtDirection.Enabled = true;
                txtDirection.Text = "";
                carType = "社会车";
                button1.Enabled = true;
            }
        }

        /// <summary>
        /// 绑定装料模式
        /// </summary>
        private void BindCmbWordMode()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("ID");
            DataColumn dc2 = new DataColumn("NAME");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            DataRow dr1 = dt.NewRow();
            dr1["ID"] = "1";
            dr1["NAME"] = "模式一";
            DataRow dr2 = dt.NewRow();
            dr2["ID"] = "2";
            dr2["NAME"] = "模式二";
            DataRow dr3 = dt.NewRow();
            dr3["ID"] = "3";
            dr3["NAME"] = "模式三";
            DataRow dr4 = dt.NewRow();
            dr4["ID"] = "4";
            dr4["NAME"] = "模式四";
            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            dt.Rows.Add(dr4);
            //绑定数据
            cmbWordMode.DataSource = dt;
            cmbWordMode.ValueMember = "ID";
            cmbWordMode.DisplayMember = "NAME";
            //设置默认值
            this.cmbWordMode.SelectedIndex = 0;
        }

        /**
          *〈一句话功能简述〉
          *〈绑定扫描行车〉
          * @return[void]
          * @exception/throws[违例类型][违例说明]
          * @see[类、类#方法、类#成员]
          * @deprecated
        */
        /// <summary>
        /// 绑定扫描行车
        /// </summary>
        private void BindCmbScanCar()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("ID");
            DataColumn dc2 = new DataColumn("NAME");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            DataRow dr1 = dt.NewRow();
            dr1["ID"] = "1";
            dr1["NAME"] = "1号行车";
            DataRow dr2 = dt.NewRow();
            dr2["ID"] = "2";
            dr2["NAME"] = "2号行车";
            DataRow dr3 = dt.NewRow();
            dr3["ID"] = "3";
            dr3["NAME"] = "3号行车";
            DataRow dr4 = dt.NewRow();
            dr4["ID"] = "4";
            dr4["NAME"] = "4号行车";
            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            dt.Rows.Add(dr4);
            //绑定数据
            cmbScanCar.DataSource = dt;
            cmbScanCar.ValueMember = "ID";
            cmbScanCar.DisplayMember = "NAME";
            //设置默认值
            this.cmbScanCar.SelectedIndex = 0;
        }

    }
}
