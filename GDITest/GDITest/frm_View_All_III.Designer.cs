namespace GDITest
{
    partial class frm_View_All_III
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Delete = new System.Windows.Forms.Button();
            this.cmdPeiJuan = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridLaserResult = new System.Windows.Forms.DataGridView();
            this.Check_Data = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Treatment_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scan_Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.theX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.theY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.theZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parking_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Car_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtXRLine = new System.Windows.Forms.Label();
            this.txtXLLine = new System.Windows.Forms.Label();
            this.cmdLockRLine = new System.Windows.Forms.Button();
            this.cmdLockLLine = new System.Windows.Forms.Button();
            this.cmdLockCenterLine = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCenterLineCalulated = new System.Windows.Forms.Label();
            this.txtXLowLine = new System.Windows.Forms.Label();
            this.txtXUpLine = new System.Windows.Forms.Label();
            this.cmdLockLowLine = new System.Windows.Forms.Button();
            this.cmdLockUpLine = new System.Windows.Forms.Button();
            this.cmd_Zoom_Out = new System.Windows.Forms.Button();
            this.cmd_Zoom_IN = new System.Windows.Forms.Button();
            this.cmdReadData = new System.Windows.Forms.Button();
            this.comboBox_SCO_ParkingNO = new System.Windows.Forms.ComboBox();
            this.panel_X_Y = new System.Windows.Forms.Panel();
            this.txt_ZMin = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_ZMax = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_YMin = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_YMax = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_XMin = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_XMax = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Scale = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Z = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Y = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_X = new System.Windows.Forms.Label();
            this.cmd_TreatData = new System.Windows.Forms.Button();
            this.cmdCommitData = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox_Horizontal = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox_Vertical = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.conLine_Horizontal_Y = new GDITest.conLine();
            this.conLine_Vertical_X = new GDITest.conLine();
            this.conLine_Vertical_Y = new GDITest.conLine();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLaserResult)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel_X_Y.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Horizontal)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Vertical)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Controls.Add(this.Delete);
            this.panel3.Controls.Add(this.cmdPeiJuan);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel_X_Y);
            this.panel3.Controls.Add(this.cmd_TreatData);
            this.panel3.Controls.Add(this.cmdCommitData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 500);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1262, 247);
            this.panel3.TabIndex = 2;
            // 
            // Delete
            // 
            this.Delete.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Delete.Location = new System.Drawing.Point(1030, 10);
            this.Delete.Margin = new System.Windows.Forms.Padding(2);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(103, 50);
            this.Delete.TabIndex = 122;
            this.Delete.Text = "删除";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // cmdPeiJuan
            // 
            this.cmdPeiJuan.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdPeiJuan.Location = new System.Drawing.Point(1157, 10);
            this.cmdPeiJuan.Margin = new System.Windows.Forms.Padding(2);
            this.cmdPeiJuan.Name = "cmdPeiJuan";
            this.cmdPeiJuan.Size = new System.Drawing.Size(103, 50);
            this.cmdPeiJuan.TabIndex = 121;
            this.cmdPeiJuan.Text = "配卷";
            this.cmdPeiJuan.UseVisualStyleBackColor = true;
            this.cmdPeiJuan.Visible = false;
            this.cmdPeiJuan.Click += new System.EventHandler(this.cmdPeiJuan_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridLaserResult);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(150, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(758, 247);
            this.panel4.TabIndex = 120;
            // 
            // dataGridLaserResult
            // 
            this.dataGridLaserResult.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridLaserResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridLaserResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridLaserResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check_Data,
            this.Treatment_NO,
            this.Scan_Count,
            this.theX,
            this.theY,
            this.theZ,
            this.Parking_NO,
            this.Car_NO});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridLaserResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridLaserResult.Location = new System.Drawing.Point(0, 64);
            this.dataGridLaserResult.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridLaserResult.Name = "dataGridLaserResult";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridLaserResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridLaserResult.RowHeadersVisible = false;
            this.dataGridLaserResult.RowTemplate.Height = 40;
            this.dataGridLaserResult.Size = new System.Drawing.Size(756, 275);
            this.dataGridLaserResult.StandardTab = true;
            this.dataGridLaserResult.TabIndex = 119;
            this.dataGridLaserResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridLaserResult_CellContentClick);
            // 
            // Check_Data
            // 
            this.Check_Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Check_Data.DataPropertyName = "Check_Data";
            this.Check_Data.FalseValue = "0";
            this.Check_Data.Frozen = true;
            this.Check_Data.HeaderText = "选择";
            this.Check_Data.Name = "Check_Data";
            this.Check_Data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check_Data.TrueValue = "1";
            this.Check_Data.Width = 60;
            // 
            // Treatment_NO
            // 
            this.Treatment_NO.HeaderText = "处理号";
            this.Treatment_NO.Name = "Treatment_NO";
            this.Treatment_NO.ReadOnly = true;
            // 
            // Scan_Count
            // 
            this.Scan_Count.HeaderText = "激光扫描";
            this.Scan_Count.Name = "Scan_Count";
            this.Scan_Count.ReadOnly = true;
            // 
            // theX
            // 
            this.theX.HeaderText = "X";
            this.theX.Name = "theX";
            this.theX.ReadOnly = true;
            // 
            // theY
            // 
            this.theY.HeaderText = "Y";
            this.theY.Name = "theY";
            this.theY.ReadOnly = true;
            // 
            // theZ
            // 
            this.theZ.HeaderText = "Z";
            this.theZ.Name = "theZ";
            this.theZ.ReadOnly = true;
            // 
            // Parking_NO
            // 
            this.Parking_NO.HeaderText = "停车位";
            this.Parking_NO.Name = "Parking_NO";
            this.Parking_NO.ReadOnly = true;
            // 
            // Car_NO
            // 
            this.Car_NO.HeaderText = "车号";
            this.Car_NO.Name = "Car_NO";
            this.Car_NO.ReadOnly = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtXRLine);
            this.panel5.Controls.Add(this.txtXLLine);
            this.panel5.Controls.Add(this.cmdLockRLine);
            this.panel5.Controls.Add(this.cmdLockLLine);
            this.panel5.Controls.Add(this.cmdLockCenterLine);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.txtCenterLineCalulated);
            this.panel5.Controls.Add(this.txtXLowLine);
            this.panel5.Controls.Add(this.txtXUpLine);
            this.panel5.Controls.Add(this.cmdLockLowLine);
            this.panel5.Controls.Add(this.cmdLockUpLine);
            this.panel5.Controls.Add(this.cmd_Zoom_Out);
            this.panel5.Controls.Add(this.cmd_Zoom_IN);
            this.panel5.Controls.Add(this.cmdReadData);
            this.panel5.Controls.Add(this.comboBox_SCO_ParkingNO);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(865, 62);
            this.panel5.TabIndex = 0;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // txtXRLine
            // 
            this.txtXRLine.BackColor = System.Drawing.Color.White;
            this.txtXRLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtXRLine.Location = new System.Drawing.Point(386, 41);
            this.txtXRLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtXRLine.Name = "txtXRLine";
            this.txtXRLine.Size = new System.Drawing.Size(72, 15);
            this.txtXRLine.TabIndex = 135;
            // 
            // txtXLLine
            // 
            this.txtXLLine.BackColor = System.Drawing.Color.White;
            this.txtXLLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtXLLine.Location = new System.Drawing.Point(386, 13);
            this.txtXLLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtXLLine.Name = "txtXLLine";
            this.txtXLLine.Size = new System.Drawing.Size(72, 15);
            this.txtXLLine.TabIndex = 134;
            // 
            // cmdLockRLine
            // 
            this.cmdLockRLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdLockRLine.Location = new System.Drawing.Point(293, 38);
            this.cmdLockRLine.Margin = new System.Windows.Forms.Padding(2);
            this.cmdLockRLine.Name = "cmdLockRLine";
            this.cmdLockRLine.Size = new System.Drawing.Size(75, 22);
            this.cmdLockRLine.TabIndex = 133;
            this.cmdLockRLine.Text = "定右边线";
            this.cmdLockRLine.UseVisualStyleBackColor = true;
            // 
            // cmdLockLLine
            // 
            this.cmdLockLLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdLockLLine.Location = new System.Drawing.Point(293, 10);
            this.cmdLockLLine.Margin = new System.Windows.Forms.Padding(2);
            this.cmdLockLLine.Name = "cmdLockLLine";
            this.cmdLockLLine.Size = new System.Drawing.Size(75, 22);
            this.cmdLockLLine.TabIndex = 132;
            this.cmdLockLLine.Text = "定左边线";
            this.cmdLockLLine.UseVisualStyleBackColor = true;
            // 
            // cmdLockCenterLine
            // 
            this.cmdLockCenterLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdLockCenterLine.Location = new System.Drawing.Point(478, 38);
            this.cmdLockCenterLine.Margin = new System.Windows.Forms.Padding(2);
            this.cmdLockCenterLine.Name = "cmdLockCenterLine";
            this.cmdLockCenterLine.Size = new System.Drawing.Size(148, 22);
            this.cmdLockCenterLine.TabIndex = 131;
            this.cmdLockCenterLine.Text = "锁定计算中线";
            this.cmdLockCenterLine.UseVisualStyleBackColor = true;
            this.cmdLockCenterLine.Click += new System.EventHandler(this.cmdLockCenterLine_Click_1);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gray;
            this.label7.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(478, 13);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 130;
            this.label7.Text = "推算中线";
            // 
            // txtCenterLineCalulated
            // 
            this.txtCenterLineCalulated.BackColor = System.Drawing.Color.White;
            this.txtCenterLineCalulated.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCenterLineCalulated.Location = new System.Drawing.Point(551, 13);
            this.txtCenterLineCalulated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtCenterLineCalulated.Name = "txtCenterLineCalulated";
            this.txtCenterLineCalulated.Size = new System.Drawing.Size(72, 15);
            this.txtCenterLineCalulated.TabIndex = 129;
            // 
            // txtXLowLine
            // 
            this.txtXLowLine.BackColor = System.Drawing.Color.White;
            this.txtXLowLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtXLowLine.Location = new System.Drawing.Point(201, 41);
            this.txtXLowLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtXLowLine.Name = "txtXLowLine";
            this.txtXLowLine.Size = new System.Drawing.Size(72, 15);
            this.txtXLowLine.TabIndex = 128;
            // 
            // txtXUpLine
            // 
            this.txtXUpLine.BackColor = System.Drawing.Color.White;
            this.txtXUpLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtXUpLine.Location = new System.Drawing.Point(201, 13);
            this.txtXUpLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtXUpLine.Name = "txtXUpLine";
            this.txtXUpLine.Size = new System.Drawing.Size(72, 15);
            this.txtXUpLine.TabIndex = 127;
            // 
            // cmdLockLowLine
            // 
            this.cmdLockLowLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdLockLowLine.Location = new System.Drawing.Point(108, 38);
            this.cmdLockLowLine.Margin = new System.Windows.Forms.Padding(2);
            this.cmdLockLowLine.Name = "cmdLockLowLine";
            this.cmdLockLowLine.Size = new System.Drawing.Size(75, 22);
            this.cmdLockLowLine.TabIndex = 126;
            this.cmdLockLowLine.Text = "定下边线";
            this.cmdLockLowLine.UseVisualStyleBackColor = true;
            // 
            // cmdLockUpLine
            // 
            this.cmdLockUpLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdLockUpLine.Location = new System.Drawing.Point(108, 10);
            this.cmdLockUpLine.Margin = new System.Windows.Forms.Padding(2);
            this.cmdLockUpLine.Name = "cmdLockUpLine";
            this.cmdLockUpLine.Size = new System.Drawing.Size(75, 22);
            this.cmdLockUpLine.TabIndex = 125;
            this.cmdLockUpLine.Text = "定上边线";
            this.cmdLockUpLine.UseVisualStyleBackColor = true;
            // 
            // cmd_Zoom_Out
            // 
            this.cmd_Zoom_Out.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmd_Zoom_Out.Location = new System.Drawing.Point(644, 38);
            this.cmd_Zoom_Out.Margin = new System.Windows.Forms.Padding(2);
            this.cmd_Zoom_Out.Name = "cmd_Zoom_Out";
            this.cmd_Zoom_Out.Size = new System.Drawing.Size(100, 22);
            this.cmd_Zoom_Out.TabIndex = 122;
            this.cmd_Zoom_Out.Text = "缩小";
            this.cmd_Zoom_Out.UseVisualStyleBackColor = true;
            // 
            // cmd_Zoom_IN
            // 
            this.cmd_Zoom_IN.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmd_Zoom_IN.Location = new System.Drawing.Point(644, 10);
            this.cmd_Zoom_IN.Margin = new System.Windows.Forms.Padding(2);
            this.cmd_Zoom_IN.Name = "cmd_Zoom_IN";
            this.cmd_Zoom_IN.Size = new System.Drawing.Size(100, 22);
            this.cmd_Zoom_IN.TabIndex = 121;
            this.cmd_Zoom_IN.Text = "放大";
            this.cmd_Zoom_IN.UseVisualStyleBackColor = true;
            this.cmd_Zoom_IN.Click += new System.EventHandler(this.cmd_Zoom_IN_Click_1);
            // 
            // cmdReadData
            // 
            this.cmdReadData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdReadData.Location = new System.Drawing.Point(16, 38);
            this.cmdReadData.Margin = new System.Windows.Forms.Padding(2);
            this.cmdReadData.Name = "cmdReadData";
            this.cmdReadData.Size = new System.Drawing.Size(72, 22);
            this.cmdReadData.TabIndex = 119;
            this.cmdReadData.Text = "读取数据";
            this.cmdReadData.UseVisualStyleBackColor = true;
            this.cmdReadData.Click += new System.EventHandler(this.cmdReadData_Click_1);
            // 
            // comboBox_SCO_ParkingNO
            // 
            this.comboBox_SCO_ParkingNO.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBox_SCO_ParkingNO.FormattingEnabled = true;
            this.comboBox_SCO_ParkingNO.Items.AddRange(new object[] {
            "Z01A1",
            "Z01A2"});
            this.comboBox_SCO_ParkingNO.Location = new System.Drawing.Point(16, 10);
            this.comboBox_SCO_ParkingNO.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_SCO_ParkingNO.Name = "comboBox_SCO_ParkingNO";
            this.comboBox_SCO_ParkingNO.Size = new System.Drawing.Size(72, 24);
            this.comboBox_SCO_ParkingNO.TabIndex = 117;
            // 
            // panel_X_Y
            // 
            this.panel_X_Y.BackColor = System.Drawing.Color.White;
            this.panel_X_Y.Controls.Add(this.txt_ZMin);
            this.panel_X_Y.Controls.Add(this.label9);
            this.panel_X_Y.Controls.Add(this.txt_ZMax);
            this.panel_X_Y.Controls.Add(this.label5);
            this.panel_X_Y.Controls.Add(this.txt_YMin);
            this.panel_X_Y.Controls.Add(this.label10);
            this.panel_X_Y.Controls.Add(this.txt_YMax);
            this.panel_X_Y.Controls.Add(this.label12);
            this.panel_X_Y.Controls.Add(this.txt_XMin);
            this.panel_X_Y.Controls.Add(this.label8);
            this.panel_X_Y.Controls.Add(this.txt_XMax);
            this.panel_X_Y.Controls.Add(this.label6);
            this.panel_X_Y.Controls.Add(this.txt_Scale);
            this.panel_X_Y.Controls.Add(this.label2);
            this.panel_X_Y.Controls.Add(this.txt_Z);
            this.panel_X_Y.Controls.Add(this.label4);
            this.panel_X_Y.Controls.Add(this.txt_Y);
            this.panel_X_Y.Controls.Add(this.label3);
            this.panel_X_Y.Controls.Add(this.txt_X);
            this.panel_X_Y.Controls.Add(this.label1);
            this.panel_X_Y.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_X_Y.Location = new System.Drawing.Point(0, 0);
            this.panel_X_Y.Margin = new System.Windows.Forms.Padding(2);
            this.panel_X_Y.Name = "panel_X_Y";
            this.panel_X_Y.Size = new System.Drawing.Size(150, 247);
            this.panel_X_Y.TabIndex = 2;
            // 
            // txt_ZMin
            // 
            this.txt_ZMin.AutoSize = true;
            this.txt_ZMin.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ZMin.Location = new System.Drawing.Point(50, 190);
            this.txt_ZMin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_ZMin.Name = "txt_ZMin";
            this.txt_ZMin.Size = new System.Drawing.Size(55, 15);
            this.txt_ZMin.TabIndex = 19;
            this.txt_ZMin.Text = "label4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(7, 190);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "ZMin";
            // 
            // txt_ZMax
            // 
            this.txt_ZMax.AutoSize = true;
            this.txt_ZMax.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ZMax.Location = new System.Drawing.Point(51, 175);
            this.txt_ZMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_ZMax.Name = "txt_ZMax";
            this.txt_ZMax.Size = new System.Drawing.Size(55, 15);
            this.txt_ZMax.TabIndex = 17;
            this.txt_ZMax.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(7, 175);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "ZMax";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txt_YMin
            // 
            this.txt_YMin.AutoSize = true;
            this.txt_YMin.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_YMin.Location = new System.Drawing.Point(50, 160);
            this.txt_YMin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_YMin.Name = "txt_YMin";
            this.txt_YMin.Size = new System.Drawing.Size(55, 15);
            this.txt_YMin.TabIndex = 15;
            this.txt_YMin.Text = "label4";
            this.txt_YMin.Click += new System.EventHandler(this.txt_YMin_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(7, 160);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "YMin";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // txt_YMax
            // 
            this.txt_YMax.AutoSize = true;
            this.txt_YMax.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_YMax.Location = new System.Drawing.Point(51, 145);
            this.txt_YMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_YMax.Name = "txt_YMax";
            this.txt_YMax.Size = new System.Drawing.Size(55, 15);
            this.txt_YMax.TabIndex = 13;
            this.txt_YMax.Text = "label4";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(7, 145);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 15);
            this.label12.TabIndex = 12;
            this.label12.Text = "YMax";
            // 
            // txt_XMin
            // 
            this.txt_XMin.AutoSize = true;
            this.txt_XMin.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_XMin.Location = new System.Drawing.Point(51, 130);
            this.txt_XMin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_XMin.Name = "txt_XMin";
            this.txt_XMin.Size = new System.Drawing.Size(55, 15);
            this.txt_XMin.TabIndex = 11;
            this.txt_XMin.Text = "label4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(7, 130);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "XMin";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // txt_XMax
            // 
            this.txt_XMax.AutoSize = true;
            this.txt_XMax.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_XMax.Location = new System.Drawing.Point(50, 115);
            this.txt_XMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_XMax.Name = "txt_XMax";
            this.txt_XMax.Size = new System.Drawing.Size(71, 15);
            this.txt_XMax.TabIndex = 9;
            this.txt_XMax.Text = "txt_XMax";
            this.txt_XMax.Click += new System.EventHandler(this.txt_XMax_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(7, 115);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "XMax";
            // 
            // txt_Scale
            // 
            this.txt_Scale.AutoSize = true;
            this.txt_Scale.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Scale.Location = new System.Drawing.Point(50, 91);
            this.txt_Scale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_Scale.Name = "txt_Scale";
            this.txt_Scale.Size = new System.Drawing.Size(55, 15);
            this.txt_Scale.TabIndex = 7;
            this.txt_Scale.Text = "label4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(23, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "1:";
            // 
            // txt_Z
            // 
            this.txt_Z.AutoSize = true;
            this.txt_Z.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Z.Location = new System.Drawing.Point(49, 59);
            this.txt_Z.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_Z.Name = "txt_Z";
            this.txt_Z.Size = new System.Drawing.Size(82, 22);
            this.txt_Z.TabIndex = 5;
            this.txt_Z.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(23, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 22);
            this.label4.TabIndex = 4;
            this.label4.Text = "Z";
            // 
            // txt_Y
            // 
            this.txt_Y.AutoSize = true;
            this.txt_Y.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Y.Location = new System.Drawing.Point(49, 34);
            this.txt_Y.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_Y.Name = "txt_Y";
            this.txt_Y.Size = new System.Drawing.Size(82, 22);
            this.txt_Y.TabIndex = 3;
            this.txt_Y.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(24, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txt_X
            // 
            this.txt_X.AutoSize = true;
            this.txt_X.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_X.Location = new System.Drawing.Point(49, 9);
            this.txt_X.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_X.Name = "txt_X";
            this.txt_X.Size = new System.Drawing.Size(82, 22);
            this.txt_X.TabIndex = 1;
            this.txt_X.Text = "label2";
            this.txt_X.Click += new System.EventHandler(this.txt_X_Click);
            // 
            // cmd_TreatData
            // 
            this.cmd_TreatData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmd_TreatData.Location = new System.Drawing.Point(912, 10);
            this.cmd_TreatData.Margin = new System.Windows.Forms.Padding(2);
            this.cmd_TreatData.Name = "cmd_TreatData";
            this.cmd_TreatData.Size = new System.Drawing.Size(103, 22);
            this.cmd_TreatData.TabIndex = 120;
            this.cmd_TreatData.Text = "刷新";
            this.cmd_TreatData.UseVisualStyleBackColor = true;
            // 
            // cmdCommitData
            // 
            this.cmdCommitData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdCommitData.Location = new System.Drawing.Point(912, 38);
            this.cmdCommitData.Margin = new System.Windows.Forms.Padding(2);
            this.cmdCommitData.Name = "cmdCommitData";
            this.cmdCommitData.Size = new System.Drawing.Size(103, 22);
            this.cmdCommitData.TabIndex = 123;
            this.cmdCommitData.Text = "提交";
            this.cmdCommitData.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1266, 749);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.conLine_Horizontal_Y);
            this.panel2.Controls.Add(this.pictureBox_Horizontal);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 251);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1262, 245);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox_Horizontal
            // 
            this.pictureBox_Horizontal.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_Horizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Horizontal.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Horizontal.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Horizontal.Name = "pictureBox_Horizontal";
            this.pictureBox_Horizontal.Size = new System.Drawing.Size(1262, 245);
            this.pictureBox_Horizontal.TabIndex = 1;
            this.pictureBox_Horizontal.TabStop = false;
            this.pictureBox_Horizontal.Click += new System.EventHandler(this.pictureBox_Horizontal_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.conLine_Vertical_X);
            this.panel1.Controls.Add(this.conLine_Vertical_Y);
            this.panel1.Controls.Add(this.pictureBox_Vertical);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1262, 245);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox_Vertical
            // 
            this.pictureBox_Vertical.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_Vertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Vertical.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Vertical.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Vertical.Name = "pictureBox_Vertical";
            this.pictureBox_Vertical.Size = new System.Drawing.Size(1262, 245);
            this.pictureBox_Vertical.TabIndex = 0;
            this.pictureBox_Vertical.TabStop = false;
            this.pictureBox_Vertical.Click += new System.EventHandler(this.pictureBox_Vertical_Click_1);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // conLine_Horizontal_Y
            // 
            this.conLine_Horizontal_Y.BackColor = System.Drawing.Color.Red;
            this.conLine_Horizontal_Y.Location = new System.Drawing.Point(716, 78);
            this.conLine_Horizontal_Y.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.conLine_Horizontal_Y.Name = "conLine_Horizontal_Y";
            this.conLine_Horizontal_Y.Size = new System.Drawing.Size(8, 120);
            this.conLine_Horizontal_Y.TabIndex = 10;
            this.conLine_Horizontal_Y.Load += new System.EventHandler(this.conLine_Horizontal_Y_Load);
            // 
            // conLine_Vertical_X
            // 
            this.conLine_Vertical_X.BackColor = System.Drawing.Color.Red;
            this.conLine_Vertical_X.Location = new System.Drawing.Point(821, 78);
            this.conLine_Vertical_X.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.conLine_Vertical_X.Name = "conLine_Vertical_X";
            this.conLine_Vertical_X.Size = new System.Drawing.Size(8, 120);
            this.conLine_Vertical_X.TabIndex = 10;
            this.conLine_Vertical_X.Load += new System.EventHandler(this.conLine_Vertical_X_Load);
            // 
            // conLine_Vertical_Y
            // 
            this.conLine_Vertical_Y.BackColor = System.Drawing.Color.Red;
            this.conLine_Vertical_Y.Location = new System.Drawing.Point(716, 78);
            this.conLine_Vertical_Y.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.conLine_Vertical_Y.Name = "conLine_Vertical_Y";
            this.conLine_Vertical_Y.Size = new System.Drawing.Size(8, 120);
            this.conLine_Vertical_Y.TabIndex = 9;
            this.conLine_Vertical_Y.Load += new System.EventHandler(this.conLine_Vertical_Y_Load);
            // 
            // frm_View_All_III
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 749);
            this.Controls.Add(this.tableLayoutPanel);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_View_All_III";
            this.Text = "frm_View_All_III";
            this.Load += new System.EventHandler(this.frm_View_All_III_Load);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLaserResult)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel_X_Y.ResumeLayout(false);
            this.panel_X_Y.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Horizontal)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Vertical)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel_X_Y;
        private System.Windows.Forms.Label txt_Z;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txt_Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txt_X;
        private conLine conLine_Vertical_X;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel2;
        private conLine conLine_Horizontal_Y;
        private System.Windows.Forms.PictureBox pictureBox_Horizontal;
        private System.Windows.Forms.Panel panel1;
        private conLine conLine_Vertical_Y;
        private System.Windows.Forms.PictureBox pictureBox_Vertical;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txt_Scale;
        private System.Windows.Forms.Label txt_XMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label txt_YMin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label txt_YMax;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label txt_XMin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridLaserResult;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button cmdCommitData;
        private System.Windows.Forms.Button cmd_Zoom_Out;
        private System.Windows.Forms.Button cmd_Zoom_IN;
        private System.Windows.Forms.Button cmd_TreatData;
        private System.Windows.Forms.Button cmdReadData;
        private System.Windows.Forms.ComboBox comboBox_SCO_ParkingNO;
        private System.Windows.Forms.Button cmdLockLowLine;
        private System.Windows.Forms.Button cmdLockUpLine;
        private System.Windows.Forms.Label txtXLowLine;
        private System.Windows.Forms.Label txtXUpLine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txtCenterLineCalulated;
        private System.Windows.Forms.Button cmdLockCenterLine;
        private System.Windows.Forms.Button cmdPeiJuan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txt_ZMax;
        private System.Windows.Forms.Label txt_ZMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check_Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Treatment_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scan_Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn theX;
        private System.Windows.Forms.DataGridViewTextBoxColumn theY;
        private System.Windows.Forms.DataGridViewTextBoxColumn theZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parking_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Car_NO;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button cmdLockRLine;
        private System.Windows.Forms.Button cmdLockLLine;
        private System.Windows.Forms.Label txtXRLine;
        private System.Windows.Forms.Label txtXLLine;
    }
}