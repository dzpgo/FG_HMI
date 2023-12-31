﻿namespace UACSView.View_CraneMonitor
{
    partial class FG_Order_Oper1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OPER_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROWNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRows = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRANE_MODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRANE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAS_COIL_WGT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLAN_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACT_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLAN_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACT_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FROM_STOCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TO_STOCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_REQ_WGT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CUR_WGT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REC_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_OrderNo = new System.Windows.Forms.Panel();
            this.bt_OrderNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_CRANE_MODE = new System.Windows.Forms.ComboBox();
            this.cbb_BAY_NO = new System.Windows.Forms.ComboBox();
            this.dateTimePicker2_recTime = new System.Windows.Forms.DateTimePicker();
            this.textWORK_SEQ_NO = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1_recTime = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucPageDemo = new UACSControls.Page.ucPage();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tb_OrderNo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(-2, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1393, 625);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1387, 605);
            this.panel2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OPER_ID,
            this.ROWNUM,
            this.TotalRows,
            this.ORDER_NO,
            this.CRANE_MODE,
            this.CRANE_NO,
            this.ORDER_TYPE,
            this.BAY_NO,
            this.MAT_CODE,
            this.MAT_CNAME,
            this.HAS_COIL_WGT,
            this.PLAN_X,
            this.ACT_X,
            this.PLAN_Y,
            this.ACT_Y,
            this.FROM_STOCK_NO,
            this.TO_STOCK_NO,
            this.STOCK_NO,
            this.MAT_REQ_WGT,
            this.MAT_CUR_WGT,
            this.MAT_TYPE,
            this.CMD_STATUS,
            this.CMD_SEQ,
            this.REC_TIME});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1387, 605);
            this.dataGridView1.TabIndex = 2;
            // 
            // OPER_ID
            // 
            this.OPER_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OPER_ID.DataPropertyName = "OPER_ID";
            this.OPER_ID.Frozen = true;
            this.OPER_ID.HeaderText = "实绩号";
            this.OPER_ID.MinimumWidth = 8;
            this.OPER_ID.Name = "OPER_ID";
            this.OPER_ID.ReadOnly = true;
            this.OPER_ID.Width = 83;
            // 
            // ROWNUM
            // 
            this.ROWNUM.DataPropertyName = "ROWNUM";
            this.ROWNUM.HeaderText = "ID";
            this.ROWNUM.Name = "ROWNUM";
            this.ROWNUM.ReadOnly = true;
            this.ROWNUM.Visible = false;
            this.ROWNUM.Width = 52;
            // 
            // TotalRows
            // 
            this.TotalRows.DataPropertyName = "TotalRows";
            this.TotalRows.HeaderText = "总数";
            this.TotalRows.Name = "TotalRows";
            this.TotalRows.ReadOnly = true;
            this.TotalRows.Visible = false;
            this.TotalRows.Width = 67;
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
            // CRANE_MODE
            // 
            this.CRANE_MODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CRANE_MODE.DataPropertyName = "CRANE_MODE";
            this.CRANE_MODE.HeaderText = "操作模式";
            this.CRANE_MODE.MinimumWidth = 8;
            this.CRANE_MODE.Name = "CRANE_MODE";
            this.CRANE_MODE.ReadOnly = true;
            this.CRANE_MODE.Width = 99;
            // 
            // CRANE_NO
            // 
            this.CRANE_NO.DataPropertyName = "CRANE_NO";
            this.CRANE_NO.HeaderText = "行车号";
            this.CRANE_NO.Name = "CRANE_NO";
            this.CRANE_NO.ReadOnly = true;
            this.CRANE_NO.Width = 83;
            // 
            // ORDER_TYPE
            // 
            this.ORDER_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ORDER_TYPE.DataPropertyName = "ORDER_TYPE";
            this.ORDER_TYPE.HeaderText = "指令类型";
            this.ORDER_TYPE.MinimumWidth = 8;
            this.ORDER_TYPE.Name = "ORDER_TYPE";
            this.ORDER_TYPE.ReadOnly = true;
            this.ORDER_TYPE.Width = 99;
            // 
            // BAY_NO
            // 
            this.BAY_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BAY_NO.DataPropertyName = "BAY_NO";
            this.BAY_NO.HeaderText = "跨号";
            this.BAY_NO.MinimumWidth = 8;
            this.BAY_NO.Name = "BAY_NO";
            this.BAY_NO.ReadOnly = true;
            this.BAY_NO.Width = 67;
            // 
            // MAT_CODE
            // 
            this.MAT_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_CODE.DataPropertyName = "MAT_CODE";
            this.MAT_CODE.HeaderText = "物料编号";
            this.MAT_CODE.MinimumWidth = 8;
            this.MAT_CODE.Name = "MAT_CODE";
            this.MAT_CODE.ReadOnly = true;
            this.MAT_CODE.Width = 99;
            // 
            // MAT_CNAME
            // 
            this.MAT_CNAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_CNAME.DataPropertyName = "MAT_CNAME";
            this.MAT_CNAME.HeaderText = "物料名";
            this.MAT_CNAME.MinimumWidth = 8;
            this.MAT_CNAME.Name = "MAT_CNAME";
            this.MAT_CNAME.ReadOnly = true;
            this.MAT_CNAME.Width = 83;
            // 
            // HAS_COIL_WGT
            // 
            this.HAS_COIL_WGT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HAS_COIL_WGT.DataPropertyName = "HAS_COIL_WGT";
            this.HAS_COIL_WGT.HeaderText = "重量";
            this.HAS_COIL_WGT.MinimumWidth = 8;
            this.HAS_COIL_WGT.Name = "HAS_COIL_WGT";
            this.HAS_COIL_WGT.ReadOnly = true;
            this.HAS_COIL_WGT.Width = 67;
            // 
            // PLAN_X
            // 
            this.PLAN_X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PLAN_X.DataPropertyName = "PLAN_X";
            this.PLAN_X.HeaderText = "计划X";
            this.PLAN_X.MinimumWidth = 8;
            this.PLAN_X.Name = "PLAN_X";
            this.PLAN_X.ReadOnly = true;
            this.PLAN_X.Width = 77;
            // 
            // ACT_X
            // 
            this.ACT_X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ACT_X.DataPropertyName = "ACT_X";
            this.ACT_X.HeaderText = "实际X";
            this.ACT_X.MinimumWidth = 8;
            this.ACT_X.Name = "ACT_X";
            this.ACT_X.ReadOnly = true;
            this.ACT_X.Width = 77;
            // 
            // PLAN_Y
            // 
            this.PLAN_Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PLAN_Y.DataPropertyName = "PLAN_Y";
            this.PLAN_Y.HeaderText = "计划Y";
            this.PLAN_Y.MinimumWidth = 8;
            this.PLAN_Y.Name = "PLAN_Y";
            this.PLAN_Y.ReadOnly = true;
            this.PLAN_Y.Width = 77;
            // 
            // ACT_Y
            // 
            this.ACT_Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ACT_Y.DataPropertyName = "ACT_Y";
            this.ACT_Y.HeaderText = "实际Y";
            this.ACT_Y.MinimumWidth = 8;
            this.ACT_Y.Name = "ACT_Y";
            this.ACT_Y.ReadOnly = true;
            this.ACT_Y.Width = 77;
            // 
            // FROM_STOCK_NO
            // 
            this.FROM_STOCK_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FROM_STOCK_NO.DataPropertyName = "FROM_STOCK_NO";
            this.FROM_STOCK_NO.HeaderText = "指令起吊位置";
            this.FROM_STOCK_NO.MinimumWidth = 8;
            this.FROM_STOCK_NO.Name = "FROM_STOCK_NO";
            this.FROM_STOCK_NO.ReadOnly = true;
            this.FROM_STOCK_NO.Width = 131;
            // 
            // TO_STOCK_NO
            // 
            this.TO_STOCK_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TO_STOCK_NO.DataPropertyName = "TO_STOCK_NO";
            this.TO_STOCK_NO.HeaderText = "指令放下位置";
            this.TO_STOCK_NO.MinimumWidth = 8;
            this.TO_STOCK_NO.Name = "TO_STOCK_NO";
            this.TO_STOCK_NO.ReadOnly = true;
            this.TO_STOCK_NO.Width = 131;
            // 
            // STOCK_NO
            // 
            this.STOCK_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STOCK_NO.DataPropertyName = "STOCK_NO";
            this.STOCK_NO.HeaderText = "当前位置";
            this.STOCK_NO.MinimumWidth = 8;
            this.STOCK_NO.Name = "STOCK_NO";
            this.STOCK_NO.ReadOnly = true;
            this.STOCK_NO.Width = 99;
            // 
            // MAT_REQ_WGT
            // 
            this.MAT_REQ_WGT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_REQ_WGT.DataPropertyName = "MAT_REQ_WGT";
            this.MAT_REQ_WGT.HeaderText = "要求重量-KG";
            this.MAT_REQ_WGT.MinimumWidth = 8;
            this.MAT_REQ_WGT.Name = "MAT_REQ_WGT";
            this.MAT_REQ_WGT.ReadOnly = true;
            this.MAT_REQ_WGT.Width = 128;
            // 
            // MAT_CUR_WGT
            // 
            this.MAT_CUR_WGT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_CUR_WGT.DataPropertyName = "MAT_CUR_WGT";
            this.MAT_CUR_WGT.HeaderText = "已作业量-KG";
            this.MAT_CUR_WGT.MinimumWidth = 8;
            this.MAT_CUR_WGT.Name = "MAT_CUR_WGT";
            this.MAT_CUR_WGT.ReadOnly = true;
            this.MAT_CUR_WGT.Width = 128;
            // 
            // MAT_TYPE
            // 
            this.MAT_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_TYPE.DataPropertyName = "MAT_TYPE";
            this.MAT_TYPE.HeaderText = "废钢类型";
            this.MAT_TYPE.MinimumWidth = 8;
            this.MAT_TYPE.Name = "MAT_TYPE";
            this.MAT_TYPE.ReadOnly = true;
            this.MAT_TYPE.Width = 99;
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
            // CMD_SEQ
            // 
            this.CMD_SEQ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CMD_SEQ.DataPropertyName = "CMD_SEQ";
            this.CMD_SEQ.HeaderText = "指令执行次数";
            this.CMD_SEQ.MinimumWidth = 8;
            this.CMD_SEQ.Name = "CMD_SEQ";
            this.CMD_SEQ.ReadOnly = true;
            this.CMD_SEQ.Width = 131;
            // 
            // REC_TIME
            // 
            this.REC_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.REC_TIME.DataPropertyName = "REC_TIME";
            this.REC_TIME.HeaderText = "记录时间";
            this.REC_TIME.MinimumWidth = 8;
            this.REC_TIME.Name = "REC_TIME";
            this.REC_TIME.ReadOnly = true;
            this.REC_TIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.REC_TIME.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_OrderNo);
            this.groupBox1.Location = new System.Drawing.Point(-2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1396, 94);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tb_OrderNo
            // 
            this.tb_OrderNo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tb_OrderNo.Controls.Add(this.bt_OrderNo);
            this.tb_OrderNo.Controls.Add(this.label3);
            this.tb_OrderNo.Controls.Add(this.cbb_CRANE_MODE);
            this.tb_OrderNo.Controls.Add(this.cbb_BAY_NO);
            this.tb_OrderNo.Controls.Add(this.dateTimePicker2_recTime);
            this.tb_OrderNo.Controls.Add(this.textWORK_SEQ_NO);
            this.tb_OrderNo.Controls.Add(this.label6);
            this.tb_OrderNo.Controls.Add(this.dateTimePicker1_recTime);
            this.tb_OrderNo.Controls.Add(this.label7);
            this.tb_OrderNo.Controls.Add(this.label2);
            this.tb_OrderNo.Controls.Add(this.label1);
            this.tb_OrderNo.Controls.Add(this.label4);
            this.tb_OrderNo.Controls.Add(this.button3);
            this.tb_OrderNo.Controls.Add(this.button2);
            this.tb_OrderNo.Controls.Add(this.button1);
            this.tb_OrderNo.Controls.Add(this.button4);
            this.tb_OrderNo.Controls.Add(this.btnQuery);
            this.tb_OrderNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_OrderNo.Location = new System.Drawing.Point(3, 17);
            this.tb_OrderNo.Name = "tb_OrderNo";
            this.tb_OrderNo.Size = new System.Drawing.Size(1390, 74);
            this.tb_OrderNo.TabIndex = 14;
            // 
            // bt_OrderNo
            // 
            this.bt_OrderNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_OrderNo.Location = new System.Drawing.Point(442, 42);
            this.bt_OrderNo.Name = "bt_OrderNo";
            this.bt_OrderNo.Size = new System.Drawing.Size(121, 29);
            this.bt_OrderNo.TabIndex = 9;
            this.bt_OrderNo.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(374, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 21);
            this.label3.TabIndex = 17;
            this.label3.Text = "指令号:";
            // 
            // cbb_CRANE_MODE
            // 
            this.cbb_CRANE_MODE.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbb_CRANE_MODE.FormattingEnabled = true;
            this.cbb_CRANE_MODE.Location = new System.Drawing.Point(443, 5);
            this.cbb_CRANE_MODE.Name = "cbb_CRANE_MODE";
            this.cbb_CRANE_MODE.Size = new System.Drawing.Size(120, 29);
            this.cbb_CRANE_MODE.TabIndex = 7;
            // 
            // cbb_BAY_NO
            // 
            this.cbb_BAY_NO.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbb_BAY_NO.FormattingEnabled = true;
            this.cbb_BAY_NO.Location = new System.Drawing.Point(669, 5);
            this.cbb_BAY_NO.Name = "cbb_BAY_NO";
            this.cbb_BAY_NO.Size = new System.Drawing.Size(120, 29);
            this.cbb_BAY_NO.TabIndex = 8;
            // 
            // dateTimePicker2_recTime
            // 
            this.dateTimePicker2_recTime.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker2_recTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker2_recTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2_recTime.Location = new System.Drawing.Point(210, 7);
            this.dateTimePicker2_recTime.Name = "dateTimePicker2_recTime";
            this.dateTimePicker2_recTime.Size = new System.Drawing.Size(122, 29);
            this.dateTimePicker2_recTime.TabIndex = 6;
            // 
            // textWORK_SEQ_NO
            // 
            this.textWORK_SEQ_NO.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textWORK_SEQ_NO.Location = new System.Drawing.Point(668, 38);
            this.textWORK_SEQ_NO.Name = "textWORK_SEQ_NO";
            this.textWORK_SEQ_NO.Size = new System.Drawing.Size(121, 29);
            this.textWORK_SEQ_NO.TabIndex = 10;
            this.textWORK_SEQ_NO.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(6, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "日期:";
            // 
            // dateTimePicker1_recTime
            // 
            this.dateTimePicker1_recTime.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker1_recTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1_recTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1_recTime.Location = new System.Drawing.Point(58, 7);
            this.dateTimePicker1_recTime.Name = "dateTimePicker1_recTime";
            this.dateTimePicker1_recTime.Size = new System.Drawing.Size(121, 29);
            this.dateTimePicker1_recTime.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(185, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "~";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(359, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "操作模式:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(617, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "跨号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(585, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "物料编号:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(260, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 29);
            this.button3.TabIndex = 2;
            this.button3.Text = "月度";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(144, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "季度";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(35, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "年度";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(1214, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 40);
            this.button4.TabIndex = 13;
            this.button4.Text = "导出";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.BackColor = System.Drawing.Color.White;
            this.btnQuery.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(1300, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(80, 40);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ucPageDemo);
            this.groupBox3.Location = new System.Drawing.Point(1, 721);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1390, 45);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // ucPageDemo
            // 
            this.ucPageDemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPageDemo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucPageDemo.CurrentPage = 0;
            this.ucPageDemo.Location = new System.Drawing.Point(3, 12);
            this.ucPageDemo.Name = "ucPageDemo";
            this.ucPageDemo.PageSize = 0;
            this.ucPageDemo.Size = new System.Drawing.Size(1381, 31);
            this.ucPageDemo.TabIndex = 0;
            this.ucPageDemo.TotalPages = 0;
            // 
            // FG_Order_Oper1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1393, 771);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FG_Order_Oper1";
            this.Text = "行车作业实绩管理";
            this.Load += new System.EventHandler(this.FG_Order_Oper1_Load);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tb_OrderNo.ResumeLayout(false);
            this.tb_OrderNo.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel tb_OrderNo;
        private System.Windows.Forms.ComboBox cbb_CRANE_MODE;
        private System.Windows.Forms.ComboBox cbb_BAY_NO;
        private System.Windows.Forms.DateTimePicker dateTimePicker2_recTime;
        private System.Windows.Forms.TextBox textWORK_SEQ_NO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1_recTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBox3;
        private UACSControls.Page.ucPage ucPageDemo;
        private System.Windows.Forms.TextBox bt_OrderNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn OPER_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROWNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_MODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAY_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAS_COIL_WGT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACT_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACT_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn FROM_STOCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TO_STOCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_REQ_WGT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CUR_WGT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn REC_TIME;
    }
}