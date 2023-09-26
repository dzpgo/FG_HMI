namespace UACSView
{
    partial class FrmLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bt_TodayTime = new System.Windows.Forms.Button();
            this.bt_MonthlyTime = new System.Windows.Forms.Button();
            this.bt_QuarterlyTime = new System.Windows.Forms.Button();
            this.bt_AnnualTime = new System.Windows.Forms.Button();
            this.txtKey1 = new System.Windows.Forms.TextBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbLevelId = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.freashBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxKey1 = new System.Windows.Forms.DataGridView();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROWNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRows = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucPage1 = new UACSControls.Page.ucPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bt_WeekTime = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKey1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 743);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.freashBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1362, 97);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel4.Controls.Add(this.bt_WeekTime);
            this.panel4.Controls.Add(this.bt_TodayTime);
            this.panel4.Controls.Add(this.bt_MonthlyTime);
            this.panel4.Controls.Add(this.bt_QuarterlyTime);
            this.panel4.Controls.Add(this.bt_AnnualTime);
            this.panel4.Controls.Add(this.txtKey1);
            this.panel4.Controls.Add(this.txtInfo);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.btnSearch);
            this.panel4.Controls.Add(this.comboBox2);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.cmbLevelId);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.dateTimeStart);
            this.panel4.Controls.Add(this.dateTimeEnd);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1362, 97);
            this.panel4.TabIndex = 527;
            // 
            // bt_TodayTime
            // 
            this.bt_TodayTime.BackColor = System.Drawing.Color.White;
            this.bt_TodayTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_TodayTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_TodayTime.ForeColor = System.Drawing.Color.White;
            this.bt_TodayTime.Location = new System.Drawing.Point(773, 28);
            this.bt_TodayTime.Name = "bt_TodayTime";
            this.bt_TodayTime.Size = new System.Drawing.Size(100, 45);
            this.bt_TodayTime.TabIndex = 527;
            this.bt_TodayTime.Text = "本日";
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
            this.bt_MonthlyTime.Location = new System.Drawing.Point(985, 28);
            this.bt_MonthlyTime.Name = "bt_MonthlyTime";
            this.bt_MonthlyTime.Size = new System.Drawing.Size(100, 45);
            this.bt_MonthlyTime.TabIndex = 528;
            this.bt_MonthlyTime.Text = "本月";
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
            this.bt_QuarterlyTime.Location = new System.Drawing.Point(1092, 28);
            this.bt_QuarterlyTime.Name = "bt_QuarterlyTime";
            this.bt_QuarterlyTime.Size = new System.Drawing.Size(100, 45);
            this.bt_QuarterlyTime.TabIndex = 529;
            this.bt_QuarterlyTime.Text = "本季";
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
            this.bt_AnnualTime.Location = new System.Drawing.Point(1198, 28);
            this.bt_AnnualTime.Name = "bt_AnnualTime";
            this.bt_AnnualTime.Size = new System.Drawing.Size(100, 45);
            this.bt_AnnualTime.TabIndex = 530;
            this.bt_AnnualTime.Text = "年度";
            this.bt_AnnualTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_AnnualTime.UseVisualStyleBackColor = false;
            this.bt_AnnualTime.Visible = false;
            this.bt_AnnualTime.Click += new System.EventHandler(this.bt_AnnualTime_Click);
            // 
            // txtKey1
            // 
            this.txtKey1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKey1.Location = new System.Drawing.Point(330, 56);
            this.txtKey1.Name = "txtKey1";
            this.txtKey1.Size = new System.Drawing.Size(180, 29);
            this.txtKey1.TabIndex = 526;
            // 
            // txtInfo
            // 
            this.txtInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfo.Location = new System.Drawing.Point(568, 14);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(180, 29);
            this.txtInfo.TabIndex = 358;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(516, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 359;
            this.label5.Text = "描述:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Location = new System.Drawing.Point(1254, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 45);
            this.btnSearch.TabIndex = 525;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "全部",
            "eblrcv",
            "hsv",
            "gtm",
            "L1eblsnd",
            "L1rcv",
            "L1snd",
            "L3rcv",
            "L3snd",
            "Lst",
            "mmrcv",
            "mmsnd",
            "mtr",
            "pmg",
            "prm",
            "rdc",
            "rmg",
            "smg",
            "stm",
            "tdh",
            "tgs",
            "wdt"});
            this.comboBox2.Location = new System.Drawing.Point(568, 56);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(180, 29);
            this.comboBox2.TabIndex = 354;
            this.comboBox2.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(516, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 21);
            this.label9.TabIndex = 355;
            this.label9.Text = "模块:";
            this.label9.Visible = false;
            // 
            // cmbLevelId
            // 
            this.cmbLevelId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevelId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbLevelId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLevelId.FormattingEnabled = true;
            this.cmbLevelId.Items.AddRange(new object[] {
            "全部",
            "出错信息",
            "普通警告",
            "普通信息"});
            this.cmbLevelId.Location = new System.Drawing.Point(330, 14);
            this.cmbLevelId.Name = "cmbLevelId";
            this.cmbLevelId.Size = new System.Drawing.Size(180, 29);
            this.cmbLevelId.TabIndex = 354;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(278, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 21);
            this.label4.TabIndex = 355;
            this.label4.Text = "级别:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(8, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 21);
            this.label7.TabIndex = 514;
            this.label7.Text = "结束时间:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(278, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 21);
            this.label1.TabIndex = 353;
            this.label1.Text = "类型:";
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(92, 14);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(180, 29);
            this.dateTimeStart.TabIndex = 511;
            this.dateTimeStart.Value = new System.DateTime(2015, 4, 17, 15, 57, 0, 0);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CalendarFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeEnd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(92, 56);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(180, 29);
            this.dateTimeEnd.TabIndex = 512;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(8, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 21);
            this.label6.TabIndex = 513;
            this.label6.Text = "开始时间:";
            // 
            // freashBox
            // 
            this.freashBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.freashBox.Location = new System.Drawing.Point(1513, 14);
            this.freashBox.Name = "freashBox";
            this.freashBox.Size = new System.Drawing.Size(86, 25);
            this.freashBox.TabIndex = 526;
            this.freashBox.Text = "自动刷新";
            this.freashBox.UseVisualStyleBackColor = true;
            this.freashBox.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1362, 631);
            this.panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbxKey1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1356, 586);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "画面日志";
            // 
            // cbxKey1
            // 
            this.cbxKey1.AllowUserToAddRows = false;
            this.cbxKey1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.LightGray;
            this.cbxKey1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.cbxKey1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.cbxKey1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxKey1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.cbxKey1.ColumnHeadersHeight = 35;
            this.cbxKey1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.cbxKey1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column16,
            this.ROWNUM,
            this.TotalRows,
            this.Column7,
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column12,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column8});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cbxKey1.DefaultCellStyle = dataGridViewCellStyle15;
            this.cbxKey1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxKey1.EnableHeadersVisualStyles = false;
            this.cbxKey1.Location = new System.Drawing.Point(3, 17);
            this.cbxKey1.MultiSelect = false;
            this.cbxKey1.Name = "cbxKey1";
            this.cbxKey1.ReadOnly = true;
            this.cbxKey1.RowHeadersVisible = false;
            this.cbxKey1.RowTemplate.Height = 23;
            this.cbxKey1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cbxKey1.Size = new System.Drawing.Size(1350, 566);
            this.cbxKey1.TabIndex = 1;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "ROW_INDEX";
            this.Column16.HeaderText = "序号";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ROWNUM
            // 
            this.ROWNUM.DataPropertyName = "ROWNUM";
            this.ROWNUM.HeaderText = "ID";
            this.ROWNUM.Name = "ROWNUM";
            this.ROWNUM.ReadOnly = true;
            this.ROWNUM.Visible = false;
            // 
            // TotalRows
            // 
            this.TotalRows.DataPropertyName = "TotalRows";
            this.TotalRows.HeaderText = "总数";
            this.TotalRows.Name = "TotalRows";
            this.TotalRows.ReadOnly = true;
            this.TotalRows.Visible = false;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "SEQNO";
            this.Column7.HeaderText = "编号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TOC";
            this.Column1.HeaderText = "操作时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "KEY1";
            this.Column2.HeaderText = "类型";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 31;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "KEY2";
            this.Column5.HeaderText = "键值2";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            this.Column5.Width = 150;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "INFO";
            this.Column12.HeaderText = "日志信息";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 875;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "LEVEL";
            this.Column3.HeaderText = "日志等级";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "MODULE";
            this.Column4.HeaderText = "操作画面";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 150;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.DataPropertyName = "USERID";
            this.Column6.HeaderText = "操作用户";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "USERNAME";
            this.Column8.HeaderText = "操作人员";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ucPage1);
            this.groupBox3.Location = new System.Drawing.Point(4, 583);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1354, 45);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // ucPage1
            // 
            this.ucPage1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPage1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucPage1.CurrentPage = 0;
            this.ucPage1.Location = new System.Drawing.Point(3, 12);
            this.ucPage1.Name = "ucPage1";
            this.ucPage1.PageSize = 0;
            this.ucPage1.Size = new System.Drawing.Size(1345, 31);
            this.ucPage1.TabIndex = 0;
            this.ucPage1.TotalPages = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // bt_WeekTime
            // 
            this.bt_WeekTime.BackColor = System.Drawing.Color.White;
            this.bt_WeekTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_WeekTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_WeekTime.ForeColor = System.Drawing.Color.White;
            this.bt_WeekTime.Location = new System.Drawing.Point(879, 28);
            this.bt_WeekTime.Name = "bt_WeekTime";
            this.bt_WeekTime.Size = new System.Drawing.Size(100, 45);
            this.bt_WeekTime.TabIndex = 531;
            this.bt_WeekTime.Text = "本周";
            this.bt_WeekTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_WeekTime.UseVisualStyleBackColor = false;
            this.bt_WeekTime.Click += new System.EventHandler(this.bt_WeekTime_Click);
            // 
            // FrmLogForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1370, 743);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogForm";
            this.Text = "操作日志画面";
            this.Load += new System.EventHandler(this.MLogForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbxKey1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox freashBox;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbLevelId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView cbxKey1;
        private System.Windows.Forms.TextBox txtKey1;
        private System.Windows.Forms.GroupBox groupBox3;
        private UACSControls.Page.ucPage ucPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_TodayTime;
        private System.Windows.Forms.Button bt_MonthlyTime;
        private System.Windows.Forms.Button bt_QuarterlyTime;
        private System.Windows.Forms.Button bt_AnnualTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROWNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button bt_WeekTime;
    }
}