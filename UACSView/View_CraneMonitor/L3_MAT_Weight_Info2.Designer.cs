namespace UACSView
{
    partial class L3_MAT_Weight_Info2
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker2_recTime = new System.Windows.Forms.DateTimePicker();
            this.textWORK_SEQ_NO = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1_recTime = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PLAN_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRUCK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAR_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_PROD_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_WT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REC_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRID_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPD_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.BackColor = System.Drawing.Color.White;
            this.btnQuery.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(683, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(103, 38);
            this.btnQuery.TabIndex = 13;
            this.btnQuery.Text = "查询";
            this.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(2, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(813, 94);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.dateTimePicker2_recTime);
            this.panel1.Controls.Add(this.textWORK_SEQ_NO);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dateTimePicker1_recTime);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 74);
            this.panel1.TabIndex = 14;
            // 
            // dateTimePicker2_recTime
            // 
            this.dateTimePicker2_recTime.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker2_recTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker2_recTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2_recTime.Location = new System.Drawing.Point(478, 22);
            this.dateTimePicker2_recTime.Name = "dateTimePicker2_recTime";
            this.dateTimePicker2_recTime.Size = new System.Drawing.Size(119, 29);
            this.dateTimePicker2_recTime.TabIndex = 12;
            // 
            // textWORK_SEQ_NO
            // 
            this.textWORK_SEQ_NO.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textWORK_SEQ_NO.Location = new System.Drawing.Point(88, 22);
            this.textWORK_SEQ_NO.Name = "textWORK_SEQ_NO";
            this.textWORK_SEQ_NO.Size = new System.Drawing.Size(121, 29);
            this.textWORK_SEQ_NO.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(242, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "创建时间:";
            // 
            // dateTimePicker1_recTime
            // 
            this.dateTimePicker1_recTime.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker1_recTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1_recTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1_recTime.Location = new System.Drawing.Point(326, 22);
            this.dateTimePicker1_recTime.Name = "dateTimePicker1_recTime";
            this.dateTimePicker1_recTime.Size = new System.Drawing.Size(121, 29);
            this.dateTimePicker1_recTime.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(453, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "~";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(23, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "计划号:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(807, 333);
            this.panel2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PLAN_NO,
            this.TRUCK_NO,
            this.CAR_TYPE,
            this.MAT_PROD_CODE,
            this.MAT_CNAME,
            this.MAT_WT,
            this.REC_TIME,
            this.GRID_NAME,
            this.UPD_TIME});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(807, 333);
            this.dataGridView1.TabIndex = 2;
            // 
            // PLAN_NO
            // 
            this.PLAN_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PLAN_NO.DataPropertyName = "WORK_SEQ_NO";
            this.PLAN_NO.Frozen = true;
            this.PLAN_NO.HeaderText = "计划号";
            this.PLAN_NO.MinimumWidth = 8;
            this.PLAN_NO.Name = "PLAN_NO";
            this.PLAN_NO.ReadOnly = true;
            this.PLAN_NO.Width = 83;
            // 
            // TRUCK_NO
            // 
            this.TRUCK_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TRUCK_NO.DataPropertyName = "TRUCK_NO";
            this.TRUCK_NO.HeaderText = "车辆牌号";
            this.TRUCK_NO.MinimumWidth = 8;
            this.TRUCK_NO.Name = "TRUCK_NO";
            this.TRUCK_NO.ReadOnly = true;
            this.TRUCK_NO.Width = 99;
            // 
            // CAR_TYPE
            // 
            this.CAR_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CAR_TYPE.DataPropertyName = "CAR_TYPE";
            this.CAR_TYPE.HeaderText = "车辆类型";
            this.CAR_TYPE.MinimumWidth = 8;
            this.CAR_TYPE.Name = "CAR_TYPE";
            this.CAR_TYPE.ReadOnly = true;
            this.CAR_TYPE.Visible = false;
            // 
            // MAT_PROD_CODE
            // 
            this.MAT_PROD_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_PROD_CODE.DataPropertyName = "MAT_PROD_CODE";
            this.MAT_PROD_CODE.HeaderText = "物料编号";
            this.MAT_PROD_CODE.MinimumWidth = 8;
            this.MAT_PROD_CODE.Name = "MAT_PROD_CODE";
            this.MAT_PROD_CODE.ReadOnly = true;
            this.MAT_PROD_CODE.Width = 99;
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
            // MAT_WT
            // 
            this.MAT_WT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAT_WT.DataPropertyName = "MAT_WT";
            this.MAT_WT.HeaderText = "物料重量";
            this.MAT_WT.MinimumWidth = 8;
            this.MAT_WT.Name = "MAT_WT";
            this.MAT_WT.ReadOnly = true;
            this.MAT_WT.Width = 99;
            // 
            // REC_TIME
            // 
            this.REC_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.REC_TIME.DataPropertyName = "REC_TIME";
            this.REC_TIME.HeaderText = "下发时间";
            this.REC_TIME.MinimumWidth = 8;
            this.REC_TIME.Name = "REC_TIME";
            this.REC_TIME.ReadOnly = true;
            this.REC_TIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.REC_TIME.Width = 80;
            // 
            // GRID_NAME
            // 
            this.GRID_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.GRID_NAME.DataPropertyName = "GRID_NAME";
            this.GRID_NAME.HeaderText = "入料料格";
            this.GRID_NAME.MinimumWidth = 8;
            this.GRID_NAME.Name = "GRID_NAME";
            this.GRID_NAME.ReadOnly = true;
            this.GRID_NAME.Visible = false;
            // 
            // UPD_TIME
            // 
            this.UPD_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UPD_TIME.DataPropertyName = "UPD_TIME";
            this.UPD_TIME.HeaderText = "更新时间";
            this.UPD_TIME.MinimumWidth = 8;
            this.UPD_TIME.Name = "UPD_TIME";
            this.UPD_TIME.ReadOnly = true;
            this.UPD_TIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPD_TIME.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(2, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(813, 353);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // L3_MAT_Weight_Info2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(809, 449);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "L3_MAT_Weight_Info2";
            this.Text = "L3送料计划";
            this.Load += new System.EventHandler(this.L3_MAT_Weight_Info2_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2_recTime;
        private System.Windows.Forms.TextBox textWORK_SEQ_NO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1_recTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRUCK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAR_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_PROD_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_WT;
        private System.Windows.Forms.DataGridViewTextBoxColumn REC_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRID_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPD_TIME;
    }
}