namespace UACSView.CLTSHMI
{
    partial class FrmCLTS_CAR_TO_YARD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCLTS_CAR_TO_YARD));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCarNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCarLeave = new System.Windows.Forms.Button();
            this.btnCarArrive = new System.Windows.Forms.Button();
            this.cbbParkingNo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCoilSelect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checBox_All_Details = new System.Windows.Forms.CheckBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.CHECK_COLUMN2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SEQ_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COIL_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARKING_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAR_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btn_MoveDown = new System.Windows.Forms.Button();
            this.btn_MoveUp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CHECK_COLUMN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.STOWAGE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NEXT_UNIT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COIL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTDIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INDIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DUMMY_COIL_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtCarNo);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btnCarLeave);
            this.groupBox1.Controls.Add(this.btnCarArrive);
            this.groupBox1.Controls.Add(this.cbbParkingNo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnCoilSelect);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 127);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // txtCarNo
            // 
            this.txtCarNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarNo.Location = new System.Drawing.Point(339, 19);
            this.txtCarNo.Multiline = true;
            this.txtCarNo.Name = "txtCarNo";
            this.txtCarNo.Size = new System.Drawing.Size(120, 28);
            this.txtCarNo.TabIndex = 59;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label13.Location = new System.Drawing.Point(259, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 21);
            this.label13.TabIndex = 58;
            this.label13.Text = "车号：";
            // 
            // btnCarLeave
            // 
            this.btnCarLeave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.btnCarLeave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCarLeave.BackgroundImage")));
            this.btnCarLeave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCarLeave.ForeColor = System.Drawing.Color.Black;
            this.btnCarLeave.Location = new System.Drawing.Point(580, 15);
            this.btnCarLeave.Name = "btnCarLeave";
            this.btnCarLeave.Size = new System.Drawing.Size(97, 38);
            this.btnCarLeave.TabIndex = 57;
            this.btnCarLeave.Text = "车离开";
            this.btnCarLeave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCarLeave.UseVisualStyleBackColor = false;
            this.btnCarLeave.Click += new System.EventHandler(this.btnCarLeave_Click);
            // 
            // btnCarArrive
            // 
            this.btnCarArrive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.btnCarArrive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCarArrive.BackgroundImage")));
            this.btnCarArrive.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCarArrive.ForeColor = System.Drawing.Color.Black;
            this.btnCarArrive.Location = new System.Drawing.Point(477, 15);
            this.btnCarArrive.Name = "btnCarArrive";
            this.btnCarArrive.Size = new System.Drawing.Size(97, 38);
            this.btnCarArrive.TabIndex = 56;
            this.btnCarArrive.Text = "车到位";
            this.btnCarArrive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCarArrive.UseVisualStyleBackColor = false;
            this.btnCarArrive.Click += new System.EventHandler(this.btnCarArrive_Click);
            // 
            // cbbParkingNo
            // 
            this.cbbParkingNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbParkingNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbParkingNo.FormattingEnabled = true;
            this.cbbParkingNo.Location = new System.Drawing.Point(105, 19);
            this.cbbParkingNo.Name = "cbbParkingNo";
            this.cbbParkingNo.Size = new System.Drawing.Size(121, 28);
            this.cbbParkingNo.TabIndex = 53;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label12.Location = new System.Drawing.Point(9, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 21);
            this.label12.TabIndex = 52;
            this.label12.Text = "停车位：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label11.Location = new System.Drawing.Point(9, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(362, 21);
            this.label11.TabIndex = 51;
            this.label11.Text = "查询提示：车到位后，点击查询即可查询钢卷信息";
            // 
            // btnCoilSelect
            // 
            this.btnCoilSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.btnCoilSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCoilSelect.BackgroundImage")));
            this.btnCoilSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCoilSelect.ForeColor = System.Drawing.Color.Black;
            this.btnCoilSelect.Location = new System.Drawing.Point(477, 59);
            this.btnCoilSelect.Name = "btnCoilSelect";
            this.btnCoilSelect.Size = new System.Drawing.Size(97, 38);
            this.btnCoilSelect.TabIndex = 39;
            this.btnCoilSelect.Text = "查询";
            this.btnCoilSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCoilSelect.UseVisualStyleBackColor = false;
            this.btnCoilSelect.Click += new System.EventHandler(this.btnCoilSelect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checBox_All_Details);
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(787, 145);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(495, 401);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "入库计划顺序";
            // 
            // checBox_All_Details
            // 
            this.checBox_All_Details.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checBox_All_Details.AutoSize = true;
            this.checBox_All_Details.BackColor = System.Drawing.Color.LightBlue;
            this.checBox_All_Details.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checBox_All_Details.Location = new System.Drawing.Point(4, 25);
            this.checBox_All_Details.Name = "checBox_All_Details";
            this.checBox_All_Details.Size = new System.Drawing.Size(61, 25);
            this.checBox_All_Details.TabIndex = 29;
            this.checBox_All_Details.Text = "全选";
            this.checBox_All_Details.UseVisualStyleBackColor = false;
            this.checBox_All_Details.CheckedChanged += new System.EventHandler(this.checBox_All_Details_CheckedChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView2.ColumnHeadersHeight = 30;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHECK_COLUMN2,
            this.SEQ_NO2,
            this.COIL_NO2,
            this.PARKING_NO2,
            this.CAR_NO2});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(0, 20);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(495, 381);
            this.dataGridView2.TabIndex = 28;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // CHECK_COLUMN2
            // 
            this.CHECK_COLUMN2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CHECK_COLUMN2.DataPropertyName = "CHECK_COLUMN";
            this.CHECK_COLUMN2.FalseValue = "0";
            this.CHECK_COLUMN2.Frozen = true;
            this.CHECK_COLUMN2.HeaderText = "";
            this.CHECK_COLUMN2.Name = "CHECK_COLUMN2";
            this.CHECK_COLUMN2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CHECK_COLUMN2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CHECK_COLUMN2.TrueValue = "1";
            this.CHECK_COLUMN2.Width = 60;
            // 
            // SEQ_NO2
            // 
            this.SEQ_NO2.DataPropertyName = "SEQ_NO";
            this.SEQ_NO2.Frozen = true;
            this.SEQ_NO2.HeaderText = "序号";
            this.SEQ_NO2.Name = "SEQ_NO2";
            this.SEQ_NO2.Width = 60;
            // 
            // COIL_NO2
            // 
            this.COIL_NO2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.COIL_NO2.DataPropertyName = "COIL_NO";
            this.COIL_NO2.Frozen = true;
            this.COIL_NO2.HeaderText = "材料号";
            this.COIL_NO2.Name = "COIL_NO2";
            this.COIL_NO2.ReadOnly = true;
            this.COIL_NO2.Width = 94;
            // 
            // PARKING_NO2
            // 
            this.PARKING_NO2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PARKING_NO2.DataPropertyName = "PARKING_NO";
            this.PARKING_NO2.HeaderText = "停车位";
            this.PARKING_NO2.Name = "PARKING_NO2";
            this.PARKING_NO2.ReadOnly = true;
            this.PARKING_NO2.Width = 94;
            // 
            // CAR_NO2
            // 
            this.CAR_NO2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CAR_NO2.DataPropertyName = "CAR_NO";
            this.CAR_NO2.HeaderText = "车号";
            this.CAR_NO2.Name = "CAR_NO2";
            this.CAR_NO2.Width = 75;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(0, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 38);
            this.button1.TabIndex = 47;
            this.button1.Text = "增加";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(0, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 38);
            this.button2.TabIndex = 48;
            this.button2.Text = "删除";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnSubmit);
            this.groupBox4.Controls.Add(this.btn_MoveDown);
            this.groupBox4.Controls.Add(this.btn_MoveUp);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(684, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(97, 534);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(0, 241);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(97, 38);
            this.btnSubmit.TabIndex = 51;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MoveDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.btn_MoveDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_MoveDown.BackgroundImage")));
            this.btn_MoveDown.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_MoveDown.ForeColor = System.Drawing.Color.Black;
            this.btn_MoveDown.Location = new System.Drawing.Point(0, 285);
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(97, 38);
            this.btn_MoveDown.TabIndex = 50;
            this.btn_MoveDown.Text = "下移";
            this.btn_MoveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_MoveDown.UseVisualStyleBackColor = false;
            this.btn_MoveDown.Visible = false;
            this.btn_MoveDown.Click += new System.EventHandler(this.btn_MoveDown_Click);
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MoveUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.btn_MoveUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_MoveUp.BackgroundImage")));
            this.btn_MoveUp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_MoveUp.ForeColor = System.Drawing.Color.Black;
            this.btn_MoveUp.Location = new System.Drawing.Point(0, 329);
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(97, 38);
            this.btn_MoveUp.TabIndex = 49;
            this.btn_MoveUp.Text = "上移";
            this.btn_MoveUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_MoveUp.UseVisualStyleBackColor = false;
            this.btn_MoveUp.Visible = false;
            this.btn_MoveUp.Click += new System.EventHandler(this.btn_MoveUp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(3, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(675, 401);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "钢卷信息";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.LightBlue;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(1, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 25);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHECK_COLUMN,
            this.STOWAGE_ID,
            this.NEXT_UNIT_NO,
            this.COIL_NO,
            this.STOCK_NO,
            this.WEIGHT,
            this.OUTDIA,
            this.WIDTH,
            this.INDIA,
            this.DUMMY_COIL_FLAG,
            this.PACK_CODE,
            this.PACK_FLAG,
            this.BAY_NO});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(675, 381);
            this.dataGridView1.TabIndex = 38;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // CHECK_COLUMN
            // 
            this.CHECK_COLUMN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CHECK_COLUMN.DataPropertyName = "CHECK_COLUMN";
            this.CHECK_COLUMN.FalseValue = "0";
            this.CHECK_COLUMN.HeaderText = "";
            this.CHECK_COLUMN.Name = "CHECK_COLUMN";
            this.CHECK_COLUMN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CHECK_COLUMN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CHECK_COLUMN.TrueValue = "1";
            this.CHECK_COLUMN.Width = 60;
            // 
            // STOWAGE_ID
            // 
            this.STOWAGE_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STOWAGE_ID.DataPropertyName = "STOWAGE_ID";
            this.STOWAGE_ID.HeaderText = "配载ID";
            this.STOWAGE_ID.Name = "STOWAGE_ID";
            this.STOWAGE_ID.ReadOnly = true;
            this.STOWAGE_ID.Width = 95;
            // 
            // NEXT_UNIT_NO
            // 
            this.NEXT_UNIT_NO.DataPropertyName = "NEXT_UNIT_NO";
            this.NEXT_UNIT_NO.HeaderText = "下道机组";
            this.NEXT_UNIT_NO.Name = "NEXT_UNIT_NO";
            this.NEXT_UNIT_NO.Visible = false;
            this.NEXT_UNIT_NO.Width = 94;
            // 
            // COIL_NO
            // 
            this.COIL_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.COIL_NO.DataPropertyName = "COIL_NO";
            this.COIL_NO.HeaderText = "材料号";
            this.COIL_NO.Name = "COIL_NO";
            this.COIL_NO.ReadOnly = true;
            this.COIL_NO.Width = 94;
            // 
            // STOCK_NO
            // 
            this.STOCK_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STOCK_NO.DataPropertyName = "STOCK_NO";
            this.STOCK_NO.HeaderText = "库位号";
            this.STOCK_NO.Name = "STOCK_NO";
            this.STOCK_NO.ReadOnly = true;
            this.STOCK_NO.Visible = false;
            this.STOCK_NO.Width = 94;
            // 
            // WEIGHT
            // 
            this.WEIGHT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WEIGHT.DataPropertyName = "WEIGHT";
            this.WEIGHT.HeaderText = "重量";
            this.WEIGHT.Name = "WEIGHT";
            this.WEIGHT.ReadOnly = true;
            this.WEIGHT.Width = 75;
            // 
            // OUTDIA
            // 
            this.OUTDIA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OUTDIA.DataPropertyName = "OUTDIA";
            this.OUTDIA.HeaderText = "外径";
            this.OUTDIA.Name = "OUTDIA";
            this.OUTDIA.ReadOnly = true;
            this.OUTDIA.Width = 75;
            // 
            // WIDTH
            // 
            this.WIDTH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WIDTH.DataPropertyName = "WIDTH";
            this.WIDTH.HeaderText = "宽度";
            this.WIDTH.Name = "WIDTH";
            this.WIDTH.ReadOnly = true;
            this.WIDTH.Width = 75;
            // 
            // INDIA
            // 
            this.INDIA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.INDIA.DataPropertyName = "INDIA";
            this.INDIA.HeaderText = "内径";
            this.INDIA.Name = "INDIA";
            this.INDIA.ReadOnly = true;
            this.INDIA.Width = 75;
            // 
            // DUMMY_COIL_FLAG
            // 
            this.DUMMY_COIL_FLAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DUMMY_COIL_FLAG.DataPropertyName = "DUMMY_COIL_FLAG";
            this.DUMMY_COIL_FLAG.HeaderText = "返回卷标记";
            this.DUMMY_COIL_FLAG.Name = "DUMMY_COIL_FLAG";
            this.DUMMY_COIL_FLAG.ReadOnly = true;
            this.DUMMY_COIL_FLAG.Visible = false;
            this.DUMMY_COIL_FLAG.Width = 132;
            // 
            // PACK_CODE
            // 
            this.PACK_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PACK_CODE.DataPropertyName = "PACK_CODE";
            this.PACK_CODE.HeaderText = "包装代码";
            this.PACK_CODE.Name = "PACK_CODE";
            this.PACK_CODE.ReadOnly = true;
            this.PACK_CODE.Visible = false;
            this.PACK_CODE.Width = 113;
            // 
            // PACK_FLAG
            // 
            this.PACK_FLAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PACK_FLAG.DataPropertyName = "PACK_FLAG";
            this.PACK_FLAG.HeaderText = "是否包装";
            this.PACK_FLAG.Name = "PACK_FLAG";
            this.PACK_FLAG.ReadOnly = true;
            this.PACK_FLAG.Visible = false;
            this.PACK_FLAG.Width = 113;
            // 
            // BAY_NO
            // 
            this.BAY_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BAY_NO.DataPropertyName = "BAY_NO";
            this.BAY_NO.HeaderText = "跨号";
            this.BAY_NO.Name = "BAY_NO";
            this.BAY_NO.ReadOnly = true;
            this.BAY_NO.Visible = false;
            this.BAY_NO.Width = 75;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(787, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(495, 127);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "画面操作提示";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(10, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 21);
            this.label8.TabIndex = 5;
            this.label8.Text = "删除计划顺序：";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(10, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 21);
            this.label6.TabIndex = 4;
            this.label6.Text = "添加计划顺序：";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(138, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "2.点击删除按钮即可删除计划顺序。";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(138, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(295, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "1.勾选右侧（计划顺序表）里的钢卷号；";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(138, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "2.点击增加按钮即可生成计划顺序。";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(138, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "1.勾选左侧（钢卷信息表）里的钢卷号；";
            // 
            // FrmCLTS_CAR_TO_YARD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1284, 550);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCLTS_CAR_TO_YARD";
            this.Text = "CLT车辆入库";
            this.Load += new System.EventHandler(this.CraneOrderHisyManage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDERNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rECOMMENDIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDERGROUPNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bAYNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mATNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDERTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDERPRIORITYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fROMSTOCKNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tOSTOCKNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMDSTATUSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fLAGDISPATDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fLAGENABLEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRANENODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pLANNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rECTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uPDTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dELTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCoilSelect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checBox_All_Details;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_MoveDown;
        private System.Windows.Forms.Button btn_MoveUp;
        private System.Windows.Forms.ComboBox cbbParkingNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCarNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCarLeave;
        private System.Windows.Forms.Button btnCarArrive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHECK_COLUMN2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn COIL_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PARKING_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAR_NO2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHECK_COLUMN;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOWAGE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NEXT_UNIT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn COIL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTDIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn INDIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DUMMY_COIL_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAY_NO;
        //private DataSet1TableAdapters.DataTable2TableAdapter dataTable2TableAdapter;
    }
}