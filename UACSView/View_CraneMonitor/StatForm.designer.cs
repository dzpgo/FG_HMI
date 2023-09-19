namespace UACSView
{
    partial class StatForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbb_CRANE_NO = new System.Windows.Forms.ComboBox();
            this.cbb_CRANE_MODE = new System.Windows.Forms.ComboBox();
            this.bt_AnnualTime = new System.Windows.Forms.Button();
            this.bt_QuarterlyTime = new System.Windows.Forms.Button();
            this.bt_MonthlyTime = new System.Windows.Forms.Button();
            this.bt_TodayTime = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.OPER_ID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRANE_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRANE_MODE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_SEQ2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.RowNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutoFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PERCENTAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "*.xls";
            this.saveFileDialog1.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(77, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(150, 29);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1349, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 45);
            this.button1.TabIndex = 17;
            this.button1.Text = "查询";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 21;
            this.label4.Text = "开始：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(453, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 21;
            this.label1.Text = "行车号：";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(297, 32);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(150, 29);
            this.dateTimePicker2.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(645, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "操作模式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 23;
            this.label2.Text = "结束：";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.btnExport.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(1466, 23);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 45);
            this.btnExport.TabIndex = 34;
            this.btnExport.Text = "统计导出";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbb_CRANE_NO
            // 
            this.cbb_CRANE_NO.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbb_CRANE_NO.FormattingEnabled = true;
            this.cbb_CRANE_NO.Location = new System.Drawing.Point(516, 32);
            this.cbb_CRANE_NO.Name = "cbb_CRANE_NO";
            this.cbb_CRANE_NO.Size = new System.Drawing.Size(120, 29);
            this.cbb_CRANE_NO.TabIndex = 35;
            // 
            // cbb_CRANE_MODE
            // 
            this.cbb_CRANE_MODE.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbb_CRANE_MODE.FormattingEnabled = true;
            this.cbb_CRANE_MODE.Location = new System.Drawing.Point(724, 32);
            this.cbb_CRANE_MODE.Name = "cbb_CRANE_MODE";
            this.cbb_CRANE_MODE.Size = new System.Drawing.Size(120, 29);
            this.cbb_CRANE_MODE.TabIndex = 35;
            // 
            // bt_AnnualTime
            // 
            this.bt_AnnualTime.BackColor = System.Drawing.Color.White;
            this.bt_AnnualTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_AnnualTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_AnnualTime.ForeColor = System.Drawing.Color.White;
            this.bt_AnnualTime.Location = new System.Drawing.Point(1181, 23);
            this.bt_AnnualTime.Name = "bt_AnnualTime";
            this.bt_AnnualTime.Size = new System.Drawing.Size(100, 45);
            this.bt_AnnualTime.TabIndex = 39;
            this.bt_AnnualTime.Text = "年度";
            this.bt_AnnualTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_AnnualTime.UseVisualStyleBackColor = false;
            this.bt_AnnualTime.Click += new System.EventHandler(this.bt_AnnualTime_Click);
            // 
            // bt_QuarterlyTime
            // 
            this.bt_QuarterlyTime.BackColor = System.Drawing.Color.White;
            this.bt_QuarterlyTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_QuarterlyTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_QuarterlyTime.ForeColor = System.Drawing.Color.White;
            this.bt_QuarterlyTime.Location = new System.Drawing.Point(1075, 23);
            this.bt_QuarterlyTime.Name = "bt_QuarterlyTime";
            this.bt_QuarterlyTime.Size = new System.Drawing.Size(100, 45);
            this.bt_QuarterlyTime.TabIndex = 38;
            this.bt_QuarterlyTime.Text = "季度";
            this.bt_QuarterlyTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_QuarterlyTime.UseVisualStyleBackColor = false;
            this.bt_QuarterlyTime.Click += new System.EventHandler(this.bt_QuarterlyTime_Click);
            // 
            // bt_MonthlyTime
            // 
            this.bt_MonthlyTime.BackColor = System.Drawing.Color.White;
            this.bt_MonthlyTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_MonthlyTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_MonthlyTime.ForeColor = System.Drawing.Color.White;
            this.bt_MonthlyTime.Location = new System.Drawing.Point(969, 23);
            this.bt_MonthlyTime.Name = "bt_MonthlyTime";
            this.bt_MonthlyTime.Size = new System.Drawing.Size(100, 45);
            this.bt_MonthlyTime.TabIndex = 37;
            this.bt_MonthlyTime.Text = "月度";
            this.bt_MonthlyTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_MonthlyTime.UseVisualStyleBackColor = false;
            this.bt_MonthlyTime.Click += new System.EventHandler(this.bt_MonthlyTime_Click);
            // 
            // bt_TodayTime
            // 
            this.bt_TodayTime.BackColor = System.Drawing.Color.White;
            this.bt_TodayTime.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_TodayTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_TodayTime.ForeColor = System.Drawing.Color.White;
            this.bt_TodayTime.Location = new System.Drawing.Point(863, 23);
            this.bt_TodayTime.Name = "bt_TodayTime";
            this.bt_TodayTime.Size = new System.Drawing.Size(100, 45);
            this.bt_TodayTime.TabIndex = 36;
            this.bt_TodayTime.Text = "当日";
            this.bt_TodayTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bt_TodayTime.UseVisualStyleBackColor = false;
            this.bt_TodayTime.Click += new System.EventHandler(this.bt_TodayTime_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.bt_TodayTime);
            this.groupBox1.Controls.Add(this.bt_MonthlyTime);
            this.groupBox1.Controls.Add(this.bt_QuarterlyTime);
            this.groupBox1.Controls.Add(this.bt_AnnualTime);
            this.groupBox1.Controls.Add(this.cbb_CRANE_MODE);
            this.groupBox1.Controls.Add(this.cbb_CRANE_NO);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1572, 81);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(0, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1572, 726);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1566, 706);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OPER_ID2,
            this.CRANE_NO2,
            this.CRANE_MODE2,
            this.CMD_SEQ2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(786, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(777, 276);
            this.dataGridView2.TabIndex = 11;
            // 
            // OPER_ID2
            // 
            this.OPER_ID2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OPER_ID2.DataPropertyName = "OPER_ID2";
            this.OPER_ID2.Frozen = true;
            this.OPER_ID2.HeaderText = "序号";
            this.OPER_ID2.Name = "OPER_ID2";
            this.OPER_ID2.ReadOnly = true;
            this.OPER_ID2.Width = 127;
            // 
            // CRANE_NO2
            // 
            this.CRANE_NO2.DataPropertyName = "CRANE_NO2";
            this.CRANE_NO2.HeaderText = "行车";
            this.CRANE_NO2.Name = "CRANE_NO2";
            this.CRANE_NO2.ReadOnly = true;
            // 
            // CRANE_MODE2
            // 
            this.CRANE_MODE2.DataPropertyName = "CRANE_MODE2";
            this.CRANE_MODE2.HeaderText = "吊运模式";
            this.CRANE_MODE2.Name = "CRANE_MODE2";
            this.CRANE_MODE2.ReadOnly = true;
            // 
            // CMD_SEQ2
            // 
            this.CMD_SEQ2.DataPropertyName = "CMD_SEQ2";
            this.CMD_SEQ2.HeaderText = "吊运总数";
            this.CMD_SEQ2.Name = "CMD_SEQ2";
            this.CMD_SEQ2.ReadOnly = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNumber,
            this.AutoFlag,
            this.OrderCount,
            this.PERCENTAGE});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(777, 276);
            this.dataGridView1.TabIndex = 16;
            // 
            // RowNumber
            // 
            this.RowNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RowNumber.DataPropertyName = "RowNumber";
            this.RowNumber.Frozen = true;
            this.RowNumber.HeaderText = "序号";
            this.RowNumber.Name = "RowNumber";
            this.RowNumber.ReadOnly = true;
            this.RowNumber.Width = 67;
            // 
            // AutoFlag
            // 
            this.AutoFlag.DataPropertyName = "AutoFlag";
            this.AutoFlag.HeaderText = "吊运模式";
            this.AutoFlag.Name = "AutoFlag";
            this.AutoFlag.ReadOnly = true;
            // 
            // OrderCount
            // 
            this.OrderCount.DataPropertyName = "OrderCount";
            this.OrderCount.HeaderText = "吊运总数";
            this.OrderCount.Name = "OrderCount";
            this.OrderCount.ReadOnly = true;
            // 
            // PERCENTAGE
            // 
            this.PERCENTAGE.DataPropertyName = "PERCENTAGE";
            this.PERCENTAGE.HeaderText = "百分比(%)";
            this.PERCENTAGE.Name = "PERCENTAGE";
            this.PERCENTAGE.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 285);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 418);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(786, 285);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(777, 418);
            this.panel2.TabIndex = 18;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.LegendText = "#SERIESNAME";
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(777, 418);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // StatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1572, 814);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "StatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "行车作业统计";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StatForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cbb_CRANE_NO;
        private System.Windows.Forms.ComboBox cbb_CRANE_MODE;
        private System.Windows.Forms.Button bt_AnnualTime;
        private System.Windows.Forms.Button bt_QuarterlyTime;
        private System.Windows.Forms.Button bt_MonthlyTime;
        private System.Windows.Forms.Button bt_TodayTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OPER_ID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_MODE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_SEQ2;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn AutoFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PERCENTAGE;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}