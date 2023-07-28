namespace UACSView
{
    partial class frmCraneAlarmLogQuery
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCraneAlarmLogQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_TodayTime = new System.Windows.Forms.Button();
            this.bt_MonthlyTime = new System.Windows.Forms.Button();
            this.bt_QuarterlyTime = new System.Windows.Forms.Button();
            this.bt_AnnualTime = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAlarmCode = new System.Windows.Forms.TextBox();
            this.comboBox_ShipLotNo = new System.Windows.Forms.ComboBox();
            this.cmdQuery = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GridAlarmLog = new System.Windows.Forms.DataGridView();
            this.CRANE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROWNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRows = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALARM_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALARM_INFO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALARM_CLASS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALARM_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X_ACT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y_ACT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z_ACT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAS_COIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLAMP_WIDTH_ACT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROL_MODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRANE_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucPageDemo = new UACSControls.Page.ucPage();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridAlarmLog)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "*.xls";
            this.saveFileDialog1.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_TodayTime);
            this.groupBox1.Controls.Add(this.bt_MonthlyTime);
            this.groupBox1.Controls.Add(this.bt_QuarterlyTime);
            this.groupBox1.Controls.Add(this.bt_AnnualTime);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAlarmCode);
            this.groupBox1.Controls.Add(this.comboBox_ShipLotNo);
            this.groupBox1.Controls.Add(this.cmdQuery);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dateTimePicker_End);
            this.groupBox1.Controls.Add(this.dateTimePicker_Start);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1713, 108);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // bt_TodayTime
            // 
            this.bt_TodayTime.BackColor = System.Drawing.Color.White;
            this.bt_TodayTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_TodayTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_TodayTime.ForeColor = System.Drawing.Color.White;
            this.bt_TodayTime.Location = new System.Drawing.Point(1067, 32);
            this.bt_TodayTime.Name = "bt_TodayTime";
            this.bt_TodayTime.Size = new System.Drawing.Size(100, 45);
            this.bt_TodayTime.TabIndex = 44;
            this.bt_TodayTime.Text = "当日";
            this.bt_TodayTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_TodayTime.UseVisualStyleBackColor = false;
            this.bt_TodayTime.Click += new System.EventHandler(this.bt_TodayTime_Click);
            // 
            // bt_MonthlyTime
            // 
            this.bt_MonthlyTime.BackColor = System.Drawing.Color.White;
            this.bt_MonthlyTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_MonthlyTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_MonthlyTime.ForeColor = System.Drawing.Color.White;
            this.bt_MonthlyTime.Location = new System.Drawing.Point(1173, 32);
            this.bt_MonthlyTime.Name = "bt_MonthlyTime";
            this.bt_MonthlyTime.Size = new System.Drawing.Size(100, 45);
            this.bt_MonthlyTime.TabIndex = 45;
            this.bt_MonthlyTime.Text = "月度";
            this.bt_MonthlyTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_MonthlyTime.UseVisualStyleBackColor = false;
            this.bt_MonthlyTime.Click += new System.EventHandler(this.bt_MonthlyTime_Click);
            // 
            // bt_QuarterlyTime
            // 
            this.bt_QuarterlyTime.BackColor = System.Drawing.Color.White;
            this.bt_QuarterlyTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_QuarterlyTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_QuarterlyTime.ForeColor = System.Drawing.Color.White;
            this.bt_QuarterlyTime.Location = new System.Drawing.Point(1279, 32);
            this.bt_QuarterlyTime.Name = "bt_QuarterlyTime";
            this.bt_QuarterlyTime.Size = new System.Drawing.Size(100, 45);
            this.bt_QuarterlyTime.TabIndex = 46;
            this.bt_QuarterlyTime.Text = "季度";
            this.bt_QuarterlyTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_QuarterlyTime.UseVisualStyleBackColor = false;
            this.bt_QuarterlyTime.Click += new System.EventHandler(this.bt_QuarterlyTime_Click);
            // 
            // bt_AnnualTime
            // 
            this.bt_AnnualTime.BackColor = System.Drawing.Color.White;
            this.bt_AnnualTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_AnnualTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_AnnualTime.ForeColor = System.Drawing.Color.White;
            this.bt_AnnualTime.Location = new System.Drawing.Point(1385, 32);
            this.bt_AnnualTime.Name = "bt_AnnualTime";
            this.bt_AnnualTime.Size = new System.Drawing.Size(100, 45);
            this.bt_AnnualTime.TabIndex = 47;
            this.bt_AnnualTime.Text = "年度";
            this.bt_AnnualTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_AnnualTime.UseVisualStyleBackColor = false;
            this.bt_AnnualTime.Click += new System.EventHandler(this.bt_AnnualTime_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(1609, 31);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 45);
            this.button1.TabIndex = 43;
            this.button1.Text = "导    出";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnDown.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDown.Location = new System.Drawing.Point(946, 54);
            this.btnDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(100, 45);
            this.btnDown.TabIndex = 42;
            this.btnDown.Text = "向下";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
            this.btnUp.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnUp.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUp.Location = new System.Drawing.Point(946, 9);
            this.btnUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(100, 45);
            this.btnUp.TabIndex = 41;
            this.btnUp.Text = "向上";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(712, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 40;
            this.label2.Text = "报警号：";
            // 
            // txtAlarmCode
            // 
            this.txtAlarmCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAlarmCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlarmCode.Location = new System.Drawing.Point(791, 38);
            this.txtAlarmCode.Name = "txtAlarmCode";
            this.txtAlarmCode.Size = new System.Drawing.Size(150, 29);
            this.txtAlarmCode.TabIndex = 39;
            // 
            // comboBox_ShipLotNo
            // 
            this.comboBox_ShipLotNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox_ShipLotNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_ShipLotNo.FormattingEnabled = true;
            this.comboBox_ShipLotNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBox_ShipLotNo.Location = new System.Drawing.Point(618, 38);
            this.comboBox_ShipLotNo.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_ShipLotNo.Name = "comboBox_ShipLotNo";
            this.comboBox_ShipLotNo.Size = new System.Drawing.Size(90, 29);
            this.comboBox_ShipLotNo.TabIndex = 38;
            this.comboBox_ShipLotNo.Text = "1";
            // 
            // cmdQuery
            // 
            this.cmdQuery.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdQuery.BackgroundImage")));
            this.cmdQuery.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmdQuery.ForeColor = System.Drawing.SystemColors.Control;
            this.cmdQuery.Location = new System.Drawing.Point(1505, 31);
            this.cmdQuery.Margin = new System.Windows.Forms.Padding(2);
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.Size = new System.Drawing.Size(100, 45);
            this.cmdQuery.TabIndex = 37;
            this.cmdQuery.Text = "查    询";
            this.cmdQuery.UseVisualStyleBackColor = true;
            this.cmdQuery.Click += new System.EventHandler(this.cmdQuery_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(4, 44);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 21);
            this.label6.TabIndex = 34;
            this.label6.Text = "起始：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(556, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 36;
            this.label1.Text = "行车：";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(280, 44);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 21);
            this.label8.TabIndex = 35;
            this.label8.Text = "终了：";
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePicker_End.CalendarFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker_End.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker_End.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_End.Location = new System.Drawing.Point(342, 38);
            this.dateTimePicker_End.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(210, 29);
            this.dateTimePicker_End.TabIndex = 33;
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePicker_Start.CalendarFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker_Start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker_Start.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_Start.Location = new System.Drawing.Point(66, 38);
            this.dateTimePicker_Start.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(210, 29);
            this.dateTimePicker_Start.TabIndex = 32;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.GridAlarmLog);
            this.groupBox2.Location = new System.Drawing.Point(0, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1713, 650);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // GridAlarmLog
            // 
            this.GridAlarmLog.AllowUserToAddRows = false;
            this.GridAlarmLog.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.LightGray;
            this.GridAlarmLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.GridAlarmLog.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridAlarmLog.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridAlarmLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.GridAlarmLog.ColumnHeadersHeight = 29;
            this.GridAlarmLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridAlarmLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CRANE_NO,
            this.ROWNUM,
            this.TotalRows,
            this.ALARM_CODE,
            this.ALARM_INFO,
            this.ALARM_CLASS,
            this.ALARM_TIME,
            this.X_ACT,
            this.Y_ACT,
            this.Z_ACT,
            this.HAS_COIL,
            this.CLAMP_WIDTH_ACT,
            this.CONTROL_MODE,
            this.CRANE_STATUS,
            this.ORDER_ID});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridAlarmLog.DefaultCellStyle = dataGridViewCellStyle16;
            this.GridAlarmLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridAlarmLog.EnableHeadersVisualStyles = false;
            this.GridAlarmLog.Location = new System.Drawing.Point(3, 17);
            this.GridAlarmLog.Margin = new System.Windows.Forms.Padding(2);
            this.GridAlarmLog.Name = "GridAlarmLog";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridAlarmLog.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.GridAlarmLog.RowHeadersVisible = false;
            this.GridAlarmLog.RowHeadersWidth = 62;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GridAlarmLog.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.GridAlarmLog.RowTemplate.Height = 45;
            this.GridAlarmLog.RowTemplate.ReadOnly = true;
            this.GridAlarmLog.Size = new System.Drawing.Size(1707, 630);
            this.GridAlarmLog.TabIndex = 87;
            // 
            // CRANE_NO
            // 
            this.CRANE_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CRANE_NO.DataPropertyName = "CRANE_NO";
            this.CRANE_NO.FillWeight = 15F;
            this.CRANE_NO.HeaderText = "行车号";
            this.CRANE_NO.MinimumWidth = 8;
            this.CRANE_NO.Name = "CRANE_NO";
            this.CRANE_NO.ReadOnly = true;
            this.CRANE_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CRANE_NO.Width = 150;
            // 
            // ROWNUM
            // 
            this.ROWNUM.DataPropertyName = "ROWNUM";
            this.ROWNUM.HeaderText = "ID";
            this.ROWNUM.Name = "ROWNUM";
            this.ROWNUM.Visible = false;
            // 
            // TotalRows
            // 
            this.TotalRows.DataPropertyName = "TotalRows";
            this.TotalRows.HeaderText = "总数";
            this.TotalRows.Name = "TotalRows";
            this.TotalRows.Visible = false;
            // 
            // ALARM_CODE
            // 
            this.ALARM_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ALARM_CODE.DataPropertyName = "ALARM_CODE";
            this.ALARM_CODE.FillWeight = 15F;
            this.ALARM_CODE.HeaderText = "报警代码";
            this.ALARM_CODE.MinimumWidth = 150;
            this.ALARM_CODE.Name = "ALARM_CODE";
            this.ALARM_CODE.ReadOnly = true;
            this.ALARM_CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ALARM_CODE.Width = 150;
            // 
            // ALARM_INFO
            // 
            this.ALARM_INFO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ALARM_INFO.DataPropertyName = "ALARM_INFO";
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ALARM_INFO.DefaultCellStyle = dataGridViewCellStyle15;
            this.ALARM_INFO.FillWeight = 15F;
            this.ALARM_INFO.HeaderText = "描述";
            this.ALARM_INFO.MinimumWidth = 8;
            this.ALARM_INFO.Name = "ALARM_INFO";
            this.ALARM_INFO.ReadOnly = true;
            this.ALARM_INFO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ALARM_INFO.Width = 250;
            // 
            // ALARM_CLASS
            // 
            this.ALARM_CLASS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ALARM_CLASS.DataPropertyName = "ALARM_CLASS";
            this.ALARM_CLASS.FillWeight = 20F;
            this.ALARM_CLASS.HeaderText = "等级";
            this.ALARM_CLASS.MinimumWidth = 8;
            this.ALARM_CLASS.Name = "ALARM_CLASS";
            this.ALARM_CLASS.ReadOnly = true;
            this.ALARM_CLASS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ALARM_CLASS.Width = 150;
            // 
            // ALARM_TIME
            // 
            this.ALARM_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ALARM_TIME.DataPropertyName = "ALARM_TIME";
            this.ALARM_TIME.FillWeight = 20F;
            this.ALARM_TIME.HeaderText = "记录时刻";
            this.ALARM_TIME.MinimumWidth = 8;
            this.ALARM_TIME.Name = "ALARM_TIME";
            this.ALARM_TIME.ReadOnly = true;
            this.ALARM_TIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ALARM_TIME.Width = 250;
            // 
            // X_ACT
            // 
            this.X_ACT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.X_ACT.DataPropertyName = "X_ACT";
            this.X_ACT.FillWeight = 5F;
            this.X_ACT.HeaderText = "X";
            this.X_ACT.MinimumWidth = 8;
            this.X_ACT.Name = "X_ACT";
            this.X_ACT.ReadOnly = true;
            this.X_ACT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.X_ACT.Width = 80;
            // 
            // Y_ACT
            // 
            this.Y_ACT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Y_ACT.DataPropertyName = "Y_ACT";
            this.Y_ACT.FillWeight = 5F;
            this.Y_ACT.HeaderText = "Y";
            this.Y_ACT.MinimumWidth = 8;
            this.Y_ACT.Name = "Y_ACT";
            this.Y_ACT.ReadOnly = true;
            this.Y_ACT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Y_ACT.Width = 80;
            // 
            // Z_ACT
            // 
            this.Z_ACT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Z_ACT.DataPropertyName = "Z_ACT";
            this.Z_ACT.FillWeight = 5F;
            this.Z_ACT.HeaderText = "Z";
            this.Z_ACT.MinimumWidth = 8;
            this.Z_ACT.Name = "Z_ACT";
            this.Z_ACT.ReadOnly = true;
            this.Z_ACT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Z_ACT.Width = 80;
            // 
            // HAS_COIL
            // 
            this.HAS_COIL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.HAS_COIL.DataPropertyName = "HAS_COIL";
            this.HAS_COIL.FillWeight = 5F;
            this.HAS_COIL.HeaderText = "有料";
            this.HAS_COIL.MinimumWidth = 8;
            this.HAS_COIL.Name = "HAS_COIL";
            this.HAS_COIL.ReadOnly = true;
            this.HAS_COIL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HAS_COIL.Width = 50;
            // 
            // CLAMP_WIDTH_ACT
            // 
            this.CLAMP_WIDTH_ACT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CLAMP_WIDTH_ACT.DataPropertyName = "CLAMP_WIDTH_ACT";
            this.CLAMP_WIDTH_ACT.FillWeight = 5F;
            this.CLAMP_WIDTH_ACT.HeaderText = "夹钳开度";
            this.CLAMP_WIDTH_ACT.MinimumWidth = 8;
            this.CLAMP_WIDTH_ACT.Name = "CLAMP_WIDTH_ACT";
            this.CLAMP_WIDTH_ACT.ReadOnly = true;
            this.CLAMP_WIDTH_ACT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CLAMP_WIDTH_ACT.Visible = false;
            this.CLAMP_WIDTH_ACT.Width = 75;
            // 
            // CONTROL_MODE
            // 
            this.CONTROL_MODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CONTROL_MODE.DataPropertyName = "CONTROL_MODE";
            this.CONTROL_MODE.HeaderText = "控制模式";
            this.CONTROL_MODE.MinimumWidth = 8;
            this.CONTROL_MODE.Name = "CONTROL_MODE";
            this.CONTROL_MODE.Width = 90;
            // 
            // CRANE_STATUS
            // 
            this.CRANE_STATUS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CRANE_STATUS.DataPropertyName = "CRANE_STATUS";
            this.CRANE_STATUS.HeaderText = "行车状态";
            this.CRANE_STATUS.MinimumWidth = 8;
            this.CRANE_STATUS.Name = "CRANE_STATUS";
            this.CRANE_STATUS.Width = 120;
            // 
            // ORDER_ID
            // 
            this.ORDER_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ORDER_ID.DataPropertyName = "ORDER_ID";
            this.ORDER_ID.HeaderText = "指令号";
            this.ORDER_ID.MinimumWidth = 8;
            this.ORDER_ID.Name = "ORDER_ID";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucPageDemo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 763);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1713, 47);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // ucPageDemo
            // 
            this.ucPageDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPageDemo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucPageDemo.CurrentPage = 0;
            this.ucPageDemo.Location = new System.Drawing.Point(1, 13);
            this.ucPageDemo.Name = "ucPageDemo";
            this.ucPageDemo.PageSize = 0;
            this.ucPageDemo.Size = new System.Drawing.Size(1706, 31);
            this.ucPageDemo.TabIndex = 2;
            this.ucPageDemo.TotalPages = 0;
            // 
            // frmCraneAlarmLogQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1713, 810);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCraneAlarmLogQuery";
            this.Text = "报警信息管理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridAlarmLog)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_TodayTime;
        private System.Windows.Forms.Button bt_MonthlyTime;
        private System.Windows.Forms.Button bt_QuarterlyTime;
        private System.Windows.Forms.Button bt_AnnualTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAlarmCode;
        private System.Windows.Forms.ComboBox comboBox_ShipLotNo;
        private System.Windows.Forms.Button cmdQuery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView GridAlarmLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private UACSControls.Page.ucPage ucPageDemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROWNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALARM_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALARM_INFO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALARM_CLASS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALARM_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn X_ACT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y_ACT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z_ACT;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAS_COIL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLAMP_WIDTH_ACT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTROL_MODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_ID;
    }
}

