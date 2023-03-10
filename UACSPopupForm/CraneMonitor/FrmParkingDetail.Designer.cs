namespace UACSPopupForm
{
    partial class FrmParkingDetail
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
            this.plParking = new System.Windows.Forms.Panel();
            this.dgvCraneOder = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvStowageMessage = new System.Windows.Forms.DataGridView();
            this.ORDER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_GROUP_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXE_SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_PRIORITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLAN_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FROM_STOCK_NO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TO_STOCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REQ_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACT_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPD_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REC_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPacking = new System.Windows.Forms.Label();
            this.lblCarNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCarStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCarType = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ORDER_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CNAME2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FROM_STOCK_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TO_STOCK_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plParking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCraneOder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStowageMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // plParking
            // 
            this.plParking.BackColor = System.Drawing.Color.LightSteelBlue;
            this.plParking.Controls.Add(this.dgvCraneOder);
            this.plParking.Controls.Add(this.label2);
            this.plParking.Controls.Add(this.dgvStowageMessage);
            this.plParking.Controls.Add(this.label1);
            this.plParking.Dock = System.Windows.Forms.DockStyle.Top;
            this.plParking.Location = new System.Drawing.Point(0, 0);
            this.plParking.Name = "plParking";
            this.plParking.Size = new System.Drawing.Size(1179, 521);
            this.plParking.TabIndex = 0;
            // 
            // dgvCraneOder
            // 
            this.dgvCraneOder.AllowUserToDeleteRows = false;
            this.dgvCraneOder.AllowUserToResizeRows = false;
            this.dgvCraneOder.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCraneOder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCraneOder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCraneOder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDER_NO2,
            this.MAT_CNAME2,
            this.FROM_STOCK_NO2,
            this.TO_STOCK_NO2,
            this.BAY_NO});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCraneOder.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCraneOder.EnableHeadersVisualStyles = false;
            this.dgvCraneOder.Location = new System.Drawing.Point(12, 300);
            this.dgvCraneOder.Name = "dgvCraneOder";
            this.dgvCraneOder.ReadOnly = true;
            this.dgvCraneOder.RowHeadersVisible = false;
            this.dgvCraneOder.RowHeadersWidth = 62;
            this.dgvCraneOder.RowTemplate.Height = 23;
            this.dgvCraneOder.Size = new System.Drawing.Size(1159, 213);
            this.dgvCraneOder.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(16, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(329, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "---指令信息-----------------------------------------";
            // 
            // dgvStowageMessage
            // 
            this.dgvStowageMessage.AllowUserToDeleteRows = false;
            this.dgvStowageMessage.AllowUserToResizeRows = false;
            this.dgvStowageMessage.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStowageMessage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStowageMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStowageMessage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDER_NO,
            this.ORDER_GROUP_NO,
            this.EXE_SEQ,
            this.ORDER_PRIORITY,
            this.CMD_STATUS,
            this.PLAN_NO,
            this.MAT_CNAME,
            this.FROM_STOCK_NO1,
            this.TO_STOCK_NO,
            this.REQ_WEIGHT,
            this.ACT_WEIGHT,
            this.UPD_TIME,
            this.REC_TIME});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStowageMessage.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStowageMessage.EnableHeadersVisualStyles = false;
            this.dgvStowageMessage.Location = new System.Drawing.Point(12, 28);
            this.dgvStowageMessage.Name = "dgvStowageMessage";
            this.dgvStowageMessage.ReadOnly = true;
            this.dgvStowageMessage.RowHeadersVisible = false;
            this.dgvStowageMessage.RowHeadersWidth = 62;
            this.dgvStowageMessage.RowTemplate.Height = 23;
            this.dgvStowageMessage.Size = new System.Drawing.Size(1159, 238);
            this.dgvStowageMessage.TabIndex = 8;
            // 
            // ORDER_NO
            // 
            this.ORDER_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ORDER_NO.DataPropertyName = "ORDER_NO";
            this.ORDER_NO.HeaderText = "指令号";
            this.ORDER_NO.MinimumWidth = 8;
            this.ORDER_NO.Name = "ORDER_NO";
            this.ORDER_NO.ReadOnly = true;
            this.ORDER_NO.Width = 83;
            // 
            // ORDER_GROUP_NO
            // 
            this.ORDER_GROUP_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ORDER_GROUP_NO.DataPropertyName = "ORDER_GROUP_NO";
            this.ORDER_GROUP_NO.HeaderText = "指令组号";
            this.ORDER_GROUP_NO.MinimumWidth = 8;
            this.ORDER_GROUP_NO.Name = "ORDER_GROUP_NO";
            this.ORDER_GROUP_NO.ReadOnly = true;
            this.ORDER_GROUP_NO.Width = 99;
            // 
            // EXE_SEQ
            // 
            this.EXE_SEQ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EXE_SEQ.DataPropertyName = "EXE_SEQ";
            this.EXE_SEQ.HeaderText = "指令顺序";
            this.EXE_SEQ.MinimumWidth = 8;
            this.EXE_SEQ.Name = "EXE_SEQ";
            this.EXE_SEQ.ReadOnly = true;
            this.EXE_SEQ.Width = 99;
            // 
            // ORDER_PRIORITY
            // 
            this.ORDER_PRIORITY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ORDER_PRIORITY.DataPropertyName = "ORDER_PRIORITY";
            this.ORDER_PRIORITY.HeaderText = "指令优先级";
            this.ORDER_PRIORITY.MinimumWidth = 8;
            this.ORDER_PRIORITY.Name = "ORDER_PRIORITY";
            this.ORDER_PRIORITY.ReadOnly = true;
            this.ORDER_PRIORITY.Width = 115;
            // 
            // CMD_STATUS
            // 
            this.CMD_STATUS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CMD_STATUS.DataPropertyName = "CMD_STATUS";
            this.CMD_STATUS.HeaderText = "指令状态";
            this.CMD_STATUS.MinimumWidth = 8;
            this.CMD_STATUS.Name = "CMD_STATUS";
            this.CMD_STATUS.ReadOnly = true;
            this.CMD_STATUS.Width = 99;
            // 
            // PLAN_NO
            // 
            this.PLAN_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PLAN_NO.DataPropertyName = "PLAN_NO";
            this.PLAN_NO.HeaderText = "计划号";
            this.PLAN_NO.MinimumWidth = 8;
            this.PLAN_NO.Name = "PLAN_NO";
            this.PLAN_NO.ReadOnly = true;
            this.PLAN_NO.Width = 83;
            // 
            // MAT_CNAME
            // 
            this.MAT_CNAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_CNAME.DataPropertyName = "MAT_CNAME";
            this.MAT_CNAME.HeaderText = "物料名称";
            this.MAT_CNAME.MinimumWidth = 8;
            this.MAT_CNAME.Name = "MAT_CNAME";
            this.MAT_CNAME.ReadOnly = true;
            this.MAT_CNAME.Width = 99;
            // 
            // FROM_STOCK_NO1
            // 
            this.FROM_STOCK_NO1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FROM_STOCK_NO1.DataPropertyName = "FROM_STOCK_NO";
            this.FROM_STOCK_NO1.HeaderText = "取料位";
            this.FROM_STOCK_NO1.MinimumWidth = 8;
            this.FROM_STOCK_NO1.Name = "FROM_STOCK_NO1";
            this.FROM_STOCK_NO1.ReadOnly = true;
            this.FROM_STOCK_NO1.Width = 83;
            // 
            // TO_STOCK_NO
            // 
            this.TO_STOCK_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TO_STOCK_NO.DataPropertyName = "TO_STOCK_NO";
            this.TO_STOCK_NO.HeaderText = "落料位";
            this.TO_STOCK_NO.MinimumWidth = 8;
            this.TO_STOCK_NO.Name = "TO_STOCK_NO";
            this.TO_STOCK_NO.ReadOnly = true;
            this.TO_STOCK_NO.Width = 83;
            // 
            // REQ_WEIGHT
            // 
            this.REQ_WEIGHT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.REQ_WEIGHT.DataPropertyName = "REQ_WEIGHT";
            this.REQ_WEIGHT.HeaderText = "要求重量";
            this.REQ_WEIGHT.MinimumWidth = 8;
            this.REQ_WEIGHT.Name = "REQ_WEIGHT";
            this.REQ_WEIGHT.ReadOnly = true;
            this.REQ_WEIGHT.Width = 99;
            // 
            // ACT_WEIGHT
            // 
            this.ACT_WEIGHT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ACT_WEIGHT.DataPropertyName = "ACT_WEIGHT";
            this.ACT_WEIGHT.HeaderText = "实际重量";
            this.ACT_WEIGHT.MinimumWidth = 8;
            this.ACT_WEIGHT.Name = "ACT_WEIGHT";
            this.ACT_WEIGHT.ReadOnly = true;
            this.ACT_WEIGHT.Width = 99;
            // 
            // UPD_TIME
            // 
            this.UPD_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UPD_TIME.DataPropertyName = "UPD_TIME";
            this.UPD_TIME.HeaderText = "更新时间";
            this.UPD_TIME.MinimumWidth = 8;
            this.UPD_TIME.Name = "UPD_TIME";
            this.UPD_TIME.ReadOnly = true;
            this.UPD_TIME.Width = 99;
            // 
            // REC_TIME
            // 
            this.REC_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.REC_TIME.DataPropertyName = "REC_TIME";
            this.REC_TIME.HeaderText = "记录时间";
            this.REC_TIME.MinimumWidth = 8;
            this.REC_TIME.Name = "REC_TIME";
            this.REC_TIME.ReadOnly = true;
            this.REC_TIME.Width = 99;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "---配载图信息-----------------------------------------";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(32, 544);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "停车位：";
            // 
            // lblPacking
            // 
            this.lblPacking.AutoSize = true;
            this.lblPacking.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPacking.ForeColor = System.Drawing.Color.Black;
            this.lblPacking.Location = new System.Drawing.Point(103, 544);
            this.lblPacking.Name = "lblPacking";
            this.lblPacking.Size = new System.Drawing.Size(70, 22);
            this.lblPacking.TabIndex = 2;
            this.lblPacking.Text = "999999";
            // 
            // lblCarNo
            // 
            this.lblCarNo.AutoSize = true;
            this.lblCarNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCarNo.ForeColor = System.Drawing.Color.Black;
            this.lblCarNo.Location = new System.Drawing.Point(265, 544);
            this.lblCarNo.Name = "lblCarNo";
            this.lblCarNo.Size = new System.Drawing.Size(70, 22);
            this.lblCarNo.TabIndex = 4;
            this.lblCarNo.Text = "999999";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(210, 544);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 22);
            this.label6.TabIndex = 3;
            this.label6.Text = "车号：";
            // 
            // lblCarStatus
            // 
            this.lblCarStatus.AutoSize = true;
            this.lblCarStatus.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCarStatus.ForeColor = System.Drawing.Color.Black;
            this.lblCarStatus.Location = new System.Drawing.Point(728, 544);
            this.lblCarStatus.Name = "lblCarStatus";
            this.lblCarStatus.Size = new System.Drawing.Size(70, 22);
            this.lblCarStatus.TabIndex = 6;
            this.lblCarStatus.Text = "999999";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(671, 544);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 22);
            this.label8.TabIndex = 5;
            this.label8.Text = "状态：";
            // 
            // lblCarType
            // 
            this.lblCarType.AutoSize = true;
            this.lblCarType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCarType.ForeColor = System.Drawing.Color.Black;
            this.lblCarType.Location = new System.Drawing.Point(490, 544);
            this.lblCarType.Name = "lblCarType";
            this.lblCarType.Size = new System.Drawing.Size(70, 22);
            this.lblCarType.TabIndex = 8;
            this.lblCarType.Text = "999999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(401, 544);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 22);
            this.label5.TabIndex = 7;
            this.label5.Text = "车辆类型：";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::UACSPopupForm.Properties.Resources.bg_btn;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(1071, 534);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 40);
            this.button1.TabIndex = 9;
            this.button1.Text = "详情";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ORDER_NO2
            // 
            this.ORDER_NO2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ORDER_NO2.DataPropertyName = "ORDER_NO";
            this.ORDER_NO2.HeaderText = "指令号";
            this.ORDER_NO2.MinimumWidth = 8;
            this.ORDER_NO2.Name = "ORDER_NO2";
            this.ORDER_NO2.ReadOnly = true;
            this.ORDER_NO2.Width = 83;
            // 
            // MAT_CNAME2
            // 
            this.MAT_CNAME2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_CNAME2.DataPropertyName = "MAT_CNAME";
            this.MAT_CNAME2.HeaderText = "物料名称";
            this.MAT_CNAME2.MinimumWidth = 8;
            this.MAT_CNAME2.Name = "MAT_CNAME2";
            this.MAT_CNAME2.ReadOnly = true;
            this.MAT_CNAME2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAT_CNAME2.Width = 80;
            // 
            // FROM_STOCK_NO2
            // 
            this.FROM_STOCK_NO2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FROM_STOCK_NO2.DataPropertyName = "FROM_STOCK_NO";
            this.FROM_STOCK_NO2.HeaderText = "取料位";
            this.FROM_STOCK_NO2.MinimumWidth = 8;
            this.FROM_STOCK_NO2.Name = "FROM_STOCK_NO2";
            this.FROM_STOCK_NO2.ReadOnly = true;
            this.FROM_STOCK_NO2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FROM_STOCK_NO2.Width = 64;
            // 
            // TO_STOCK_NO2
            // 
            this.TO_STOCK_NO2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TO_STOCK_NO2.DataPropertyName = "TO_STOCK_NO";
            this.TO_STOCK_NO2.HeaderText = "落料位";
            this.TO_STOCK_NO2.MinimumWidth = 8;
            this.TO_STOCK_NO2.Name = "TO_STOCK_NO2";
            this.TO_STOCK_NO2.ReadOnly = true;
            this.TO_STOCK_NO2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TO_STOCK_NO2.Width = 64;
            // 
            // BAY_NO
            // 
            this.BAY_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BAY_NO.DataPropertyName = "BAY_NO";
            this.BAY_NO.HeaderText = "跨别";
            this.BAY_NO.MinimumWidth = 8;
            this.BAY_NO.Name = "BAY_NO";
            this.BAY_NO.ReadOnly = true;
            this.BAY_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BAY_NO.Width = 48;
            // 
            // FrmParkingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1179, 586);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblCarType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCarStatus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblCarNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblPacking);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.plParking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmParkingDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "停车位详细";
            this.Load += new System.EventHandler(this.FrmParkingDetail_Load);
            this.plParking.ResumeLayout(false);
            this.plParking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCraneOder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStowageMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plParking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvStowageMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvCraneOder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPacking;
        private System.Windows.Forms.Label lblCarNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCarStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCarType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_GROUP_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXE_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_PRIORITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FROM_STOCK_NO1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TO_STOCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn REQ_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACT_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPD_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn REC_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CNAME2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FROM_STOCK_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TO_STOCK_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAY_NO;
        // private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}