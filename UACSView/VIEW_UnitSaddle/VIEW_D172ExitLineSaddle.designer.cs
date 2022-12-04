namespace UACSView
{
    partial class VIEW_D172ExitLineSaddle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VIEW_D172ExitLineSaddle));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer_LineSaddleControl = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.coilUnitSaddleButton6 = new UACSControls.CoilUnitSaddleButton();
            this.coilUnitSaddleButton5 = new UACSControls.CoilUnitSaddleButton();
            this.coilUnitSaddleButton4 = new UACSControls.CoilUnitSaddleButton();
            this.coilUnitSaddle6 = new UACSControls.CoilUnitSaddle();
            this.coilUnitSaddle5 = new UACSControls.CoilUnitSaddle();
            this.coilUnitSaddle4 = new UACSControls.CoilUnitSaddle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvExitSaddleInfo = new System.Windows.Forms.DataGridView();
            this.SADDLE_L2NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COIL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACT_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACT_WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INDIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTDIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORBIDEN_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NEXT_UNIT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L3_COIL_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WORK_ORDER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORBIDEN_COIL_WHERE_TO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_Status = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_Coil_Up = new System.Windows.Forms.Panel();
            this.panel_Coil_Down = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_CraneNo = new System.Windows.Forms.ComboBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Coil_Up = new System.Windows.Forms.Button();
            this.btn_Coil_Down = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExitSaddleInfo)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_LineSaddleControl
            // 
            this.timer_LineSaddleControl.Enabled = true;
            this.timer_LineSaddleControl.Interval = 3000;
            this.timer_LineSaddleControl.Tick += new System.EventHandler(this.timer_LineSaddleControl_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.14286F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.85714F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 700);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.coilUnitSaddleButton6);
            this.panel1.Controls.Add(this.coilUnitSaddleButton5);
            this.panel1.Controls.Add(this.coilUnitSaddleButton4);
            this.panel1.Controls.Add(this.coilUnitSaddle6);
            this.panel1.Controls.Add(this.coilUnitSaddle5);
            this.panel1.Controls.Add(this.coilUnitSaddle4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1356, 450);
            this.panel1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.LightYellow;
            this.button4.Location = new System.Drawing.Point(1203, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(118, 45);
            this.button4.TabIndex = 133;
            this.button4.Text = "返回主监控";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // coilUnitSaddleButton6
            // 
            this.coilUnitSaddleButton6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.coilUnitSaddleButton6.Location = new System.Drawing.Point(765, 219);
            this.coilUnitSaddleButton6.MySaddleNo = "";
            this.coilUnitSaddleButton6.MySaddleTagName = "";
            this.coilUnitSaddleButton6.Name = "coilUnitSaddleButton6";
            this.coilUnitSaddleButton6.Size = new System.Drawing.Size(132, 35);
            this.coilUnitSaddleButton6.TabIndex = 9;
            // 
            // coilUnitSaddleButton5
            // 
            this.coilUnitSaddleButton5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.coilUnitSaddleButton5.Location = new System.Drawing.Point(596, 219);
            this.coilUnitSaddleButton5.MySaddleNo = "";
            this.coilUnitSaddleButton5.MySaddleTagName = "";
            this.coilUnitSaddleButton5.Name = "coilUnitSaddleButton5";
            this.coilUnitSaddleButton5.Size = new System.Drawing.Size(132, 35);
            this.coilUnitSaddleButton5.TabIndex = 8;
            // 
            // coilUnitSaddleButton4
            // 
            this.coilUnitSaddleButton4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.coilUnitSaddleButton4.Location = new System.Drawing.Point(421, 218);
            this.coilUnitSaddleButton4.MySaddleNo = "";
            this.coilUnitSaddleButton4.MySaddleTagName = "";
            this.coilUnitSaddleButton4.Name = "coilUnitSaddleButton4";
            this.coilUnitSaddleButton4.Size = new System.Drawing.Size(132, 35);
            this.coilUnitSaddleButton4.TabIndex = 7;
            // 
            // coilUnitSaddle6
            // 
            this.coilUnitSaddle6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.coilUnitSaddle6.ButtonDImage = ((System.Drawing.Image)(resources.GetObject("coilUnitSaddle6.ButtonDImage")));
            this.coilUnitSaddle6.ButtonUImage = ((System.Drawing.Image)(resources.GetObject("coilUnitSaddle6.ButtonUImage")));
            this.coilUnitSaddle6.CoilBackColor = System.Drawing.Color.SkyBlue;
            this.coilUnitSaddle6.CoilFontColor = System.Drawing.Color.Black;
            this.coilUnitSaddle6.CoilId = "";
            this.coilUnitSaddle6.CoilLength = 16;
            this.coilUnitSaddle6.CoilStatus = -10;
            this.coilUnitSaddle6.Director = Baosight.ColdRolling.TcmControl.Direct.Horizontal;
            this.coilUnitSaddle6.DownEnable = true;
            this.coilUnitSaddle6.DownVisiable = false;
            this.coilUnitSaddle6.Location = new System.Drawing.Point(765, 105);
            this.coilUnitSaddle6.Name = "coilUnitSaddle6";
            this.coilUnitSaddle6.PosName = "D172WC0006";
            this.coilUnitSaddle6.PosNo = 1;
            this.coilUnitSaddle6.Size = new System.Drawing.Size(180, 150);
            this.coilUnitSaddle6.SkidName = "skidControl";
            this.coilUnitSaddle6.SkidSize = new System.Drawing.Size(160, 115);
            this.coilUnitSaddle6.TabIndex = 3;
            this.coilUnitSaddle6.TextFont = new System.Drawing.Font("微软雅黑", 12F);
            this.coilUnitSaddle6.UpEnable = true;
            this.coilUnitSaddle6.UpVisiable = false;
            // 
            // coilUnitSaddle5
            // 
            this.coilUnitSaddle5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.coilUnitSaddle5.ButtonDImage = ((System.Drawing.Image)(resources.GetObject("coilUnitSaddle5.ButtonDImage")));
            this.coilUnitSaddle5.ButtonUImage = ((System.Drawing.Image)(resources.GetObject("coilUnitSaddle5.ButtonUImage")));
            this.coilUnitSaddle5.CoilBackColor = System.Drawing.Color.SkyBlue;
            this.coilUnitSaddle5.CoilFontColor = System.Drawing.Color.Black;
            this.coilUnitSaddle5.CoilId = "";
            this.coilUnitSaddle5.CoilLength = 16;
            this.coilUnitSaddle5.CoilStatus = -10;
            this.coilUnitSaddle5.Director = Baosight.ColdRolling.TcmControl.Direct.Horizontal;
            this.coilUnitSaddle5.DownEnable = true;
            this.coilUnitSaddle5.DownVisiable = false;
            this.coilUnitSaddle5.Location = new System.Drawing.Point(596, 105);
            this.coilUnitSaddle5.Name = "coilUnitSaddle5";
            this.coilUnitSaddle5.PosName = "D172WC0005";
            this.coilUnitSaddle5.PosNo = 1;
            this.coilUnitSaddle5.Size = new System.Drawing.Size(168, 150);
            this.coilUnitSaddle5.SkidName = "skidControl";
            this.coilUnitSaddle5.SkidSize = new System.Drawing.Size(160, 115);
            this.coilUnitSaddle5.TabIndex = 2;
            this.coilUnitSaddle5.TextFont = new System.Drawing.Font("微软雅黑", 12F);
            this.coilUnitSaddle5.UpEnable = true;
            this.coilUnitSaddle5.UpVisiable = false;
            // 
            // coilUnitSaddle4
            // 
            this.coilUnitSaddle4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.coilUnitSaddle4.ButtonDImage = ((System.Drawing.Image)(resources.GetObject("coilUnitSaddle4.ButtonDImage")));
            this.coilUnitSaddle4.ButtonUImage = ((System.Drawing.Image)(resources.GetObject("coilUnitSaddle4.ButtonUImage")));
            this.coilUnitSaddle4.CoilBackColor = System.Drawing.Color.SkyBlue;
            this.coilUnitSaddle4.CoilFontColor = System.Drawing.Color.Black;
            this.coilUnitSaddle4.CoilId = "";
            this.coilUnitSaddle4.CoilLength = 16;
            this.coilUnitSaddle4.CoilStatus = -10;
            this.coilUnitSaddle4.Director = Baosight.ColdRolling.TcmControl.Direct.Horizontal;
            this.coilUnitSaddle4.DownEnable = true;
            this.coilUnitSaddle4.DownVisiable = false;
            this.coilUnitSaddle4.Location = new System.Drawing.Point(421, 105);
            this.coilUnitSaddle4.Name = "coilUnitSaddle4";
            this.coilUnitSaddle4.PosName = "D172WC0004";
            this.coilUnitSaddle4.PosNo = 1;
            this.coilUnitSaddle4.Size = new System.Drawing.Size(169, 150);
            this.coilUnitSaddle4.SkidName = "skidControl";
            this.coilUnitSaddle4.SkidSize = new System.Drawing.Size(160, 115);
            this.coilUnitSaddle4.TabIndex = 1;
            this.coilUnitSaddle4.TextFont = new System.Drawing.Font("微软雅黑", 12F);
            this.coilUnitSaddle4.UpEnable = true;
            this.coilUnitSaddle4.UpVisiable = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(222)))), ((int)(((byte)(241)))));
            this.groupBox1.Controls.Add(this.dgvExitSaddleInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1356, 238);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "鞍座信息";
            // 
            // dgvExitSaddleInfo
            // 
            this.dgvExitSaddleInfo.AllowUserToAddRows = false;
            this.dgvExitSaddleInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            this.dgvExitSaddleInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvExitSaddleInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExitSaddleInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvExitSaddleInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(222)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExitSaddleInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvExitSaddleInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExitSaddleInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SADDLE_L2NAME,
            this.STOCK_NO,
            this.COIL_NO,
            this.WEIGHT,
            this.ACT_WEIGHT,
            this.WIDTH,
            this.ACT_WIDTH,
            this.INDIA,
            this.OUTDIA,
            this.FORBIDEN_FLAG,
            this.NEXT_UNIT_NO,
            this.PACK_CODE,
            this.PACK_FLAG,
            this.L3_COIL_STATUS,
            this.WORK_ORDER_NO,
            this.PRODUCT_DATE,
            this.FORBIDEN_COIL_WHERE_TO});
            this.dgvExitSaddleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExitSaddleInfo.EnableHeadersVisualStyles = false;
            this.dgvExitSaddleInfo.Location = new System.Drawing.Point(3, 19);
            this.dgvExitSaddleInfo.Name = "dgvExitSaddleInfo";
            this.dgvExitSaddleInfo.ReadOnly = true;
            this.dgvExitSaddleInfo.RowHeadersVisible = false;
            this.dgvExitSaddleInfo.RowTemplate.Height = 23;
            this.dgvExitSaddleInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExitSaddleInfo.Size = new System.Drawing.Size(1350, 216);
            this.dgvExitSaddleInfo.TabIndex = 3;
            // 
            // SADDLE_L2NAME
            // 
            this.SADDLE_L2NAME.DataPropertyName = "SADDLE_L2NAME";
            this.SADDLE_L2NAME.HeaderText = "序号";
            this.SADDLE_L2NAME.Name = "SADDLE_L2NAME";
            this.SADDLE_L2NAME.ReadOnly = true;
            // 
            // STOCK_NO
            // 
            this.STOCK_NO.DataPropertyName = "STOCK_NO";
            this.STOCK_NO.HeaderText = "鞍座号";
            this.STOCK_NO.Name = "STOCK_NO";
            this.STOCK_NO.ReadOnly = true;
            // 
            // COIL_NO
            // 
            this.COIL_NO.DataPropertyName = "COIL_NO";
            this.COIL_NO.HeaderText = "钢卷号";
            this.COIL_NO.Name = "COIL_NO";
            this.COIL_NO.ReadOnly = true;
            // 
            // WEIGHT
            // 
            this.WEIGHT.DataPropertyName = "WEIGHT";
            this.WEIGHT.HeaderText = "理论重量";
            this.WEIGHT.Name = "WEIGHT";
            this.WEIGHT.ReadOnly = true;
            // 
            // ACT_WEIGHT
            // 
            this.ACT_WEIGHT.DataPropertyName = "ACT_WEIGHT";
            this.ACT_WEIGHT.HeaderText = "实际重量";
            this.ACT_WEIGHT.Name = "ACT_WEIGHT";
            this.ACT_WEIGHT.ReadOnly = true;
            // 
            // WIDTH
            // 
            this.WIDTH.DataPropertyName = "WIDTH";
            this.WIDTH.HeaderText = "理论宽度";
            this.WIDTH.Name = "WIDTH";
            this.WIDTH.ReadOnly = true;
            // 
            // ACT_WIDTH
            // 
            this.ACT_WIDTH.DataPropertyName = "ACT_WIDTH";
            this.ACT_WIDTH.HeaderText = "实际宽度";
            this.ACT_WIDTH.Name = "ACT_WIDTH";
            this.ACT_WIDTH.ReadOnly = true;
            // 
            // INDIA
            // 
            this.INDIA.DataPropertyName = "INDIA";
            this.INDIA.HeaderText = "内径";
            this.INDIA.Name = "INDIA";
            this.INDIA.ReadOnly = true;
            // 
            // OUTDIA
            // 
            this.OUTDIA.DataPropertyName = "OUTDIA";
            this.OUTDIA.HeaderText = "外径";
            this.OUTDIA.Name = "OUTDIA";
            this.OUTDIA.ReadOnly = true;
            // 
            // FORBIDEN_FLAG
            // 
            this.FORBIDEN_FLAG.DataPropertyName = "FORBIDEN_FLAG";
            this.FORBIDEN_FLAG.HeaderText = "封闭标记";
            this.FORBIDEN_FLAG.Name = "FORBIDEN_FLAG";
            this.FORBIDEN_FLAG.ReadOnly = true;
            // 
            // NEXT_UNIT_NO
            // 
            this.NEXT_UNIT_NO.DataPropertyName = "NEXT_UNIT_NO";
            this.NEXT_UNIT_NO.HeaderText = "下道机组";
            this.NEXT_UNIT_NO.Name = "NEXT_UNIT_NO";
            this.NEXT_UNIT_NO.ReadOnly = true;
            // 
            // PACK_CODE
            // 
            this.PACK_CODE.DataPropertyName = "PACK_CODE";
            this.PACK_CODE.HeaderText = "包装代码";
            this.PACK_CODE.Name = "PACK_CODE";
            this.PACK_CODE.ReadOnly = true;
            // 
            // PACK_FLAG
            // 
            this.PACK_FLAG.DataPropertyName = "PACK_FLAG";
            this.PACK_FLAG.HeaderText = "是否包装";
            this.PACK_FLAG.Name = "PACK_FLAG";
            this.PACK_FLAG.ReadOnly = true;
            // 
            // L3_COIL_STATUS
            // 
            this.L3_COIL_STATUS.DataPropertyName = "L3_COIL_STATUS";
            this.L3_COIL_STATUS.HeaderText = "L3卷状态";
            this.L3_COIL_STATUS.Name = "L3_COIL_STATUS";
            this.L3_COIL_STATUS.ReadOnly = true;
            // 
            // WORK_ORDER_NO
            // 
            this.WORK_ORDER_NO.DataPropertyName = "WORK_ORDER_NO";
            this.WORK_ORDER_NO.HeaderText = "生产合同号";
            this.WORK_ORDER_NO.Name = "WORK_ORDER_NO";
            this.WORK_ORDER_NO.ReadOnly = true;
            // 
            // PRODUCT_DATE
            // 
            this.PRODUCT_DATE.DataPropertyName = "PRODUCT_DATE";
            this.PRODUCT_DATE.HeaderText = "生产日期";
            this.PRODUCT_DATE.Name = "PRODUCT_DATE";
            this.PRODUCT_DATE.ReadOnly = true;
            // 
            // FORBIDEN_COIL_WHERE_TO
            // 
            this.FORBIDEN_COIL_WHERE_TO.DataPropertyName = "FORBIDEN_COIL_WHERE_TO";
            this.FORBIDEN_COIL_WHERE_TO.HeaderText = "封闭卷流向";
            this.FORBIDEN_COIL_WHERE_TO.Name = "FORBIDEN_COIL_WHERE_TO";
            this.FORBIDEN_COIL_WHERE_TO.ReadOnly = true;
            this.FORBIDEN_COIL_WHERE_TO.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.label_Status);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.panel_Coil_Up);
            this.groupBox3.Controls.Add(this.panel_Coil_Down);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cbb_CraneNo);
            this.groupBox3.Controls.Add(this.btn_Cancel);
            this.groupBox3.Controls.Add(this.btn_Coil_Up);
            this.groupBox3.Controls.Add(this.btn_Coil_Down);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(3, 257);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 190);
            this.groupBox3.TabIndex = 140;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "称重卷吊运";
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(119, 167);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(33, 20);
            this.label_Status.TabIndex = 8;
            this.label_Status.Text = "999";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "称重吊运状态：";
            // 
            // panel_Coil_Up
            // 
            this.panel_Coil_Up.BackColor = System.Drawing.Color.White;
            this.panel_Coil_Up.Location = new System.Drawing.Point(331, 106);
            this.panel_Coil_Up.Name = "panel_Coil_Up";
            this.panel_Coil_Up.Size = new System.Drawing.Size(25, 25);
            this.panel_Coil_Up.TabIndex = 6;
            // 
            // panel_Coil_Down
            // 
            this.panel_Coil_Down.BackColor = System.Drawing.Color.White;
            this.panel_Coil_Down.Location = new System.Drawing.Point(331, 50);
            this.panel_Coil_Down.Name = "panel_Coil_Down";
            this.panel_Coil_Down.Size = new System.Drawing.Size(25, 25);
            this.panel_Coil_Down.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "选择吊运行车";
            // 
            // cbb_CraneNo
            // 
            this.cbb_CraneNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_CraneNo.FormattingEnabled = true;
            this.cbb_CraneNo.Location = new System.Drawing.Point(8, 47);
            this.cbb_CraneNo.Name = "cbb_CraneNo";
            this.cbb_CraneNo.Size = new System.Drawing.Size(120, 28);
            this.cbb_CraneNo.TabIndex = 3;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Cancel.Location = new System.Drawing.Point(195, 147);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(130, 40);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "取消操作";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Coil_Up
            // 
            this.btn_Coil_Up.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_Coil_Up.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Coil_Up.Location = new System.Drawing.Point(195, 91);
            this.btn_Coil_Up.Name = "btn_Coil_Up";
            this.btn_Coil_Up.Size = new System.Drawing.Size(130, 40);
            this.btn_Coil_Up.TabIndex = 1;
            this.btn_Coil_Up.Text = "机组称重(吊离)";
            this.btn_Coil_Up.UseVisualStyleBackColor = false;
            this.btn_Coil_Up.Click += new System.EventHandler(this.btn_Coil_Up_Click);
            // 
            // btn_Coil_Down
            // 
            this.btn_Coil_Down.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_Coil_Down.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Coil_Down.Location = new System.Drawing.Point(195, 35);
            this.btn_Coil_Down.Name = "btn_Coil_Down";
            this.btn_Coil_Down.Size = new System.Drawing.Size(130, 40);
            this.btn_Coil_Down.TabIndex = 0;
            this.btn_Coil_Down.Text = "机组称重(吊入)";
            this.btn_Coil_Down.UseVisualStyleBackColor = false;
            this.btn_Coil_Down.Click += new System.EventHandler(this.btn_Coil_Down_Click);
            // 
            // VIEW_D172ExitLineSaddle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 700);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VIEW_D172ExitLineSaddle";
            this.Text = "VIEW_ExitLineSaddle";
            this.TabActivated += new System.EventHandler(this.MyTabActivated);
            this.TabDeactivated += new System.EventHandler(this.MyTabDeactivated);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExitSaddleInfo)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Timer timer_LineSaddleControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private UACSControls.CoilUnitSaddle coilUnitSaddle6;
        private UACSControls.CoilUnitSaddle coilUnitSaddle5;
        private UACSControls.CoilUnitSaddle coilUnitSaddle4;
        private UACSControls.CoilUnitSaddleButton coilUnitSaddleButton5;
        private UACSControls.CoilUnitSaddleButton coilUnitSaddleButton4;
        private UACSControls.CoilUnitSaddleButton coilUnitSaddleButton6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dgvExitSaddleInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SADDLE_L2NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn COIL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACT_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACT_WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn INDIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTDIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORBIDEN_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn NEXT_UNIT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn L3_COIL_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn WORK_ORDER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORBIDEN_COIL_WHERE_TO;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_Coil_Up;
        private System.Windows.Forms.Panel panel_Coil_Down;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_CraneNo;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Coil_Up;
        private System.Windows.Forms.Button btn_Coil_Down;
    }
}