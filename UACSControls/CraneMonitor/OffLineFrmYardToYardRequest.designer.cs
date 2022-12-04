namespace UACSPopupForm
{
    partial class OffLineFrmYardToYardRequest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OffLineFrmYardToYardRequest));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCoilNO = new System.Windows.Forms.TextBox();
            this.txtFromYard = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtToYard = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCloseToYard = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCoilStatus = new System.Windows.Forms.Label();
            this.lblPackCode = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblOutdia = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInDia = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblStockStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSaddleNO = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CoilNO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCoilSelect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CHECK_COLUMN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NEXT_UNIT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COIL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTDIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INDIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORBIDEN_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DUMMY_COIL_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(72, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "钢卷号：";
            // 
            // txtCoilNO
            // 
            this.txtCoilNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCoilNO.Location = new System.Drawing.Point(165, 129);
            this.txtCoilNO.Multiline = true;
            this.txtCoilNO.Name = "txtCoilNO";
            this.txtCoilNO.Size = new System.Drawing.Size(185, 28);
            this.txtCoilNO.TabIndex = 2;
            
            // 
            // txtFromYard
            // 
            this.txtFromYard.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFromYard.Location = new System.Drawing.Point(165, 235);
            this.txtFromYard.Multiline = true;
            this.txtFromYard.Name = "txtFromYard";
            this.txtFromYard.Size = new System.Drawing.Size(185, 28);
            this.txtFromYard.TabIndex = 4;
            this.txtFromYard.Visible = false;
            this.txtFromYard.TextChanged += new System.EventHandler(this.txtFromYard_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(72, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "起卷位：";
            this.label3.Visible = false;
            // 
            // txtToYard
            // 
            this.txtToYard.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtToYard.Location = new System.Drawing.Point(165, 343);
            this.txtToYard.Multiline = true;
            this.txtToYard.Name = "txtToYard";
            this.txtToYard.Size = new System.Drawing.Size(184, 28);
            this.txtToYard.TabIndex = 6;
            this.txtToYard.TextChanged += new System.EventHandler(this.txtToYard_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(72, 343);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "落卷位：";
            // 
            // BtnOk
            // 
            this.BtnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnOk.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnOk.Location = new System.Drawing.Point(3, 572);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(114, 51);
            this.BtnOk.TabIndex = 7;
            this.BtnOk.Text = "执 行 ";
            this.BtnOk.UseVisualStyleBackColor = false;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCloseToYard
            // 
            this.BtnCloseToYard.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnCloseToYard.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCloseToYard.Location = new System.Drawing.Point(160, 572);
            this.BtnCloseToYard.Name = "BtnCloseToYard";
            this.BtnCloseToYard.Size = new System.Drawing.Size(114, 51);
            this.BtnCloseToYard.TabIndex = 8;
            this.BtnCloseToYard.Text = "取 消";
            this.BtnCloseToYard.UseVisualStyleBackColor = false;
            this.BtnCloseToYard.Visible = false;
            this.BtnCloseToYard.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(317, 572);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(114, 51);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "关闭";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(197, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblCoilStatus);
            this.groupBox1.Controls.Add(this.lblPackCode);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblOutdia);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.lblInDia);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblWidth);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblWeight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblStockStatus);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 485);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 49);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检查信息";
            this.groupBox1.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(13, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "钢卷号是否存在多处：";
            // 
            // lblCoilStatus
            // 
            this.lblCoilStatus.AutoSize = true;
            this.lblCoilStatus.ForeColor = System.Drawing.Color.Red;
            this.lblCoilStatus.Location = new System.Drawing.Point(175, 47);
            this.lblCoilStatus.Name = "lblCoilStatus";
            this.lblCoilStatus.Size = new System.Drawing.Size(33, 20);
            this.lblCoilStatus.TabIndex = 12;
            this.lblCoilStatus.Text = "999";
            // 
            // lblPackCode
            // 
            this.lblPackCode.AutoSize = true;
            this.lblPackCode.Location = new System.Drawing.Point(253, 103);
            this.lblPackCode.Name = "lblPackCode";
            this.lblPackCode.Size = new System.Drawing.Size(33, 20);
            this.lblPackCode.TabIndex = 11;
            this.lblPackCode.Text = "999";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(165, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 20);
            this.label13.TabIndex = 10;
            this.label13.Text = "包装代码：";
            // 
            // lblOutdia
            // 
            this.lblOutdia.AutoSize = true;
            this.lblOutdia.Location = new System.Drawing.Point(70, 103);
            this.lblOutdia.Name = "lblOutdia";
            this.lblOutdia.Size = new System.Drawing.Size(33, 20);
            this.lblOutdia.TabIndex = 9;
            this.lblOutdia.Text = "999";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 20);
            this.label15.TabIndex = 8;
            this.label15.Text = "外径：";
            // 
            // lblInDia
            // 
            this.lblInDia.AutoSize = true;
            this.lblInDia.Location = new System.Drawing.Point(365, 77);
            this.lblInDia.Name = "lblInDia";
            this.lblInDia.Size = new System.Drawing.Size(33, 20);
            this.lblInDia.TabIndex = 7;
            this.lblInDia.Text = "999";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(308, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 6;
            this.label12.Text = "内径：";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(227, 77);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(33, 20);
            this.lblWidth.TabIndex = 5;
            this.lblWidth.Text = "999";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(165, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "宽度：";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(70, 77);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(33, 20);
            this.lblWeight.TabIndex = 3;
            this.lblWeight.Text = "999";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "重量：";
            // 
            // lblStockStatus
            // 
            this.lblStockStatus.AutoSize = true;
            this.lblStockStatus.Location = new System.Drawing.Point(250, 15);
            this.lblStockStatus.Name = "lblStockStatus";
            this.lblStockStatus.Size = new System.Drawing.Size(33, 20);
            this.lblStockStatus.TabIndex = 1;
            this.lblStockStatus.Text = "999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "库位状态：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSaddleNO);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.CoilNO);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnCoilSelect);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 123);
            this.panel1.TabIndex = 17;
            // 
            // txtSaddleNO
            // 
            this.txtSaddleNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSaddleNO.Location = new System.Drawing.Point(358, 50);
            this.txtSaddleNO.Multiline = true;
            this.txtSaddleNO.Name = "txtSaddleNO";
            this.txtSaddleNO.Size = new System.Drawing.Size(120, 28);
            this.txtSaddleNO.TabIndex = 46;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label11.Location = new System.Drawing.Point(294, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 21);
            this.label11.TabIndex = 43;
            this.label11.Text = "鞍座：";
            // 
            // CoilNO
            // 
            this.CoilNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CoilNO.Location = new System.Drawing.Point(83, 50);
            this.CoilNO.Multiline = true;
            this.CoilNO.Name = "CoilNO";
            this.CoilNO.Size = new System.Drawing.Size(120, 28);
            this.CoilNO.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(3, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 39;
            this.label1.Text = "钢卷号：";
            // 
            // btnCoilSelect
            // 
            this.btnCoilSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCoilSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(137)))), ((int)(((byte)(241)))));
            this.btnCoilSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCoilSelect.BackgroundImage")));
            this.btnCoilSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCoilSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoilSelect.ForeColor = System.Drawing.Color.White;
            this.btnCoilSelect.Location = new System.Drawing.Point(768, 44);
            this.btnCoilSelect.Name = "btnCoilSelect";
            this.btnCoilSelect.Size = new System.Drawing.Size(97, 38);
            this.btnCoilSelect.TabIndex = 38;
            this.btnCoilSelect.Text = "查询";
            this.btnCoilSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCoilSelect.UseVisualStyleBackColor = false;
            this.btnCoilSelect.Click += new System.EventHandler(this.btnCoilSelect_Click_1);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.BtnOk);
            this.panel2.Controls.Add(this.BtnCloseToYard);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.txtToYard);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtFromYard);
            this.panel2.Controls.Add(this.txtCoilNO);
            this.panel2.Location = new System.Drawing.Point(894, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 648);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(0, 129);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(888, 519);
            this.panel3.TabIndex = 18;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHECK_COLUMN,
            this.NEXT_UNIT_NO,
            this.COIL_NO,
            this.STOCK_NO,
            this.WEIGHT,
            this.OUTDIA,
            this.WIDTH,
            this.INDIA,
            this.FORBIDEN_FLAG,
            this.DUMMY_COIL_FLAG,
            this.PACK_CODE,
            this.PACK_FLAG,
            this.BAY_NO});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(888, 519);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // CHECK_COLUMN
            // 
            this.CHECK_COLUMN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CHECK_COLUMN.DataPropertyName = "CHECK_COLUMN";
            this.CHECK_COLUMN.FalseValue = "0";
            this.CHECK_COLUMN.Frozen = true;
            this.CHECK_COLUMN.HeaderText = "选择";
            this.CHECK_COLUMN.Name = "CHECK_COLUMN";
            this.CHECK_COLUMN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CHECK_COLUMN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CHECK_COLUMN.TrueValue = "1";
            this.CHECK_COLUMN.Width = 60;
            // 
            // NEXT_UNIT_NO
            // 
            this.NEXT_UNIT_NO.DataPropertyName = "NEXT_UNIT_NO";
            this.NEXT_UNIT_NO.Frozen = true;
            this.NEXT_UNIT_NO.HeaderText = "下道机组";
            this.NEXT_UNIT_NO.Name = "NEXT_UNIT_NO";
            this.NEXT_UNIT_NO.Width = 94;
            // 
            // COIL_NO
            // 
            this.COIL_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.COIL_NO.DataPropertyName = "COIL_NO";
            this.COIL_NO.Frozen = true;
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
            // FORBIDEN_FLAG
            // 
            this.FORBIDEN_FLAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FORBIDEN_FLAG.DataPropertyName = "FORBIDEN_FLAG";
            this.FORBIDEN_FLAG.HeaderText = "封闭标记";
            this.FORBIDEN_FLAG.Name = "FORBIDEN_FLAG";
            this.FORBIDEN_FLAG.ReadOnly = true;
            this.FORBIDEN_FLAG.Width = 113;
            // 
            // DUMMY_COIL_FLAG
            // 
            this.DUMMY_COIL_FLAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DUMMY_COIL_FLAG.DataPropertyName = "DUMMY_COIL_FLAG";
            this.DUMMY_COIL_FLAG.HeaderText = "返回卷标记";
            this.DUMMY_COIL_FLAG.Name = "DUMMY_COIL_FLAG";
            this.DUMMY_COIL_FLAG.ReadOnly = true;
            this.DUMMY_COIL_FLAG.Width = 132;
            // 
            // PACK_CODE
            // 
            this.PACK_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PACK_CODE.DataPropertyName = "PACK_CODE";
            this.PACK_CODE.HeaderText = "包装代码";
            this.PACK_CODE.Name = "PACK_CODE";
            this.PACK_CODE.ReadOnly = true;
            this.PACK_CODE.Width = 113;
            // 
            // PACK_FLAG
            // 
            this.PACK_FLAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PACK_FLAG.DataPropertyName = "PACK_FLAG";
            this.PACK_FLAG.HeaderText = "是否包装";
            this.PACK_FLAG.Name = "PACK_FLAG";
            this.PACK_FLAG.ReadOnly = true;
            this.PACK_FLAG.Width = 113;
            // 
            // BAY_NO
            // 
            this.BAY_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BAY_NO.DataPropertyName = "BAY_NO";
            this.BAY_NO.HeaderText = "跨号";
            this.BAY_NO.Name = "BAY_NO";
            this.BAY_NO.ReadOnly = true;
            this.BAY_NO.Width = 75;
            // 
            // OffLineFrmYardToYardRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1331, 649);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "OffLineFrmYardToYardRequest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "台车选卷";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCoilNO;
        private System.Windows.Forms.TextBox txtFromYard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtToYard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCloseToYard;
        private System.Windows.Forms.Button btnClear;
        //       private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStockStatus;
        private System.Windows.Forms.Label lblInDia;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPackCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblOutdia;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCoilStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCoilSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CoilNO;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHECK_COLUMN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NEXT_UNIT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn COIL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTDIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn INDIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORBIDEN_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn DUMMY_COIL_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAY_NO;
        private System.Windows.Forms.TextBox txtSaddleNO;
    }
}