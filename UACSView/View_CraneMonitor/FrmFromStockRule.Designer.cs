namespace UACSView.View_CraneMonitor
{
    partial class FrmFromStockRule
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CRANE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLAMP_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STARTEGY_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WORK_POS_X_START = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WORK_POS_X_END = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLAMP_LENGTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLAMP_WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UP_MAT_DIR_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOWN_MAT_DIR_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UP_MAT_DIR_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOWN_MAT_DIR_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UP_MAT_LIMIT_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOWN_MAT_LIMIT_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UP_MAT_LIMIT_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOWN_MAT_LIMIT_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UP_MAT_LIMIT_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOWN_MAT_LIMIT_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UP_MAT_LIMIT_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOWN_MAT_LIMIT_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_Refurbish = new System.Windows.Forms.Button();
            this.bt_Save = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(3, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1254, 560);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CRANE_NO,
            this.BAY_NO,
            this.CLAMP_TYPE,
            this.SEQ_NO,
            this.STARTEGY_TYPE,
            this.WORK_POS_X_START,
            this.WORK_POS_X_END,
            this.CLAMP_LENGTH,
            this.CLAMP_WIDTH,
            this.UP_MAT_DIR_1,
            this.DOWN_MAT_DIR_1,
            this.UP_MAT_DIR_2,
            this.DOWN_MAT_DIR_2,
            this.UP_MAT_LIMIT_1,
            this.DOWN_MAT_LIMIT_1,
            this.UP_MAT_LIMIT_2,
            this.DOWN_MAT_LIMIT_2,
            this.UP_MAT_LIMIT_3,
            this.DOWN_MAT_LIMIT_3,
            this.UP_MAT_LIMIT_4,
            this.DOWN_MAT_LIMIT_4});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1254, 560);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // CRANE_NO
            // 
            this.CRANE_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CRANE_NO.DataPropertyName = "CRANE_NO";
            this.CRANE_NO.Frozen = true;
            this.CRANE_NO.HeaderText = "行车号";
            this.CRANE_NO.Name = "CRANE_NO";
            this.CRANE_NO.ReadOnly = true;
            this.CRANE_NO.Width = 61;
            // 
            // BAY_NO
            // 
            this.BAY_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BAY_NO.DataPropertyName = "BAY_NO";
            this.BAY_NO.HeaderText = "跨号";
            this.BAY_NO.Name = "BAY_NO";
            this.BAY_NO.ReadOnly = true;
            this.BAY_NO.Width = 51;
            // 
            // CLAMP_TYPE
            // 
            this.CLAMP_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CLAMP_TYPE.DataPropertyName = "CLAMP_TYPE";
            this.CLAMP_TYPE.HeaderText = "吊具类型";
            this.CLAMP_TYPE.Name = "CLAMP_TYPE";
            this.CLAMP_TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CLAMP_TYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.CLAMP_TYPE.Width = 61;
            // 
            // SEQ_NO
            // 
            this.SEQ_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SEQ_NO.DataPropertyName = "SEQ_NO";
            this.SEQ_NO.HeaderText = "位置排序";
            this.SEQ_NO.Name = "SEQ_NO";
            this.SEQ_NO.Width = 61;
            // 
            // STARTEGY_TYPE
            // 
            this.STARTEGY_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STARTEGY_TYPE.DataPropertyName = "STARTEGY_TYPE";
            this.STARTEGY_TYPE.HeaderText = "取放料策略";
            this.STARTEGY_TYPE.Name = "STARTEGY_TYPE";
            this.STARTEGY_TYPE.Width = 72;
            // 
            // WORK_POS_X_START
            // 
            this.WORK_POS_X_START.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WORK_POS_X_START.DataPropertyName = "WORK_POS_X_START";
            this.WORK_POS_X_START.HeaderText = "起始坐标";
            this.WORK_POS_X_START.Name = "WORK_POS_X_START";
            this.WORK_POS_X_START.Width = 61;
            // 
            // WORK_POS_X_END
            // 
            this.WORK_POS_X_END.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WORK_POS_X_END.DataPropertyName = "WORK_POS_X_END";
            this.WORK_POS_X_END.HeaderText = "终点坐标";
            this.WORK_POS_X_END.Name = "WORK_POS_X_END";
            this.WORK_POS_X_END.Width = 61;
            // 
            // CLAMP_LENGTH
            // 
            this.CLAMP_LENGTH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CLAMP_LENGTH.DataPropertyName = "CLAMP_LENGTH";
            this.CLAMP_LENGTH.HeaderText = "吸盘长度";
            this.CLAMP_LENGTH.Name = "CLAMP_LENGTH";
            this.CLAMP_LENGTH.Width = 61;
            // 
            // CLAMP_WIDTH
            // 
            this.CLAMP_WIDTH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CLAMP_WIDTH.DataPropertyName = "CLAMP_WIDTH";
            this.CLAMP_WIDTH.HeaderText = "吸盘宽度";
            this.CLAMP_WIDTH.Name = "CLAMP_WIDTH";
            this.CLAMP_WIDTH.Width = 61;
            // 
            // UP_MAT_DIR_1
            // 
            this.UP_MAT_DIR_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UP_MAT_DIR_1.DataPropertyName = "UP_MAT_DIR_1";
            this.UP_MAT_DIR_1.HeaderText = "归堆取料策略";
            this.UP_MAT_DIR_1.Name = "UP_MAT_DIR_1";
            this.UP_MAT_DIR_1.Width = 72;
            // 
            // DOWN_MAT_DIR_1
            // 
            this.DOWN_MAT_DIR_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DOWN_MAT_DIR_1.DataPropertyName = "DOWN_MAT_DIR_1";
            this.DOWN_MAT_DIR_1.HeaderText = "归堆放料策略";
            this.DOWN_MAT_DIR_1.Name = "DOWN_MAT_DIR_1";
            this.DOWN_MAT_DIR_1.Width = 72;
            // 
            // UP_MAT_DIR_2
            // 
            this.UP_MAT_DIR_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UP_MAT_DIR_2.DataPropertyName = "UP_MAT_DIR_2";
            this.UP_MAT_DIR_2.HeaderText = "装槽取料策略";
            this.UP_MAT_DIR_2.Name = "UP_MAT_DIR_2";
            this.UP_MAT_DIR_2.Width = 72;
            // 
            // DOWN_MAT_DIR_2
            // 
            this.DOWN_MAT_DIR_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DOWN_MAT_DIR_2.DataPropertyName = "DOWN_MAT_DIR_2";
            this.DOWN_MAT_DIR_2.HeaderText = "装槽放料策略";
            this.DOWN_MAT_DIR_2.Name = "DOWN_MAT_DIR_2";
            this.DOWN_MAT_DIR_2.Width = 72;
            // 
            // UP_MAT_LIMIT_1
            // 
            this.UP_MAT_LIMIT_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UP_MAT_LIMIT_1.DataPropertyName = "UP_MAT_LIMIT_1";
            this.UP_MAT_LIMIT_1.HeaderText = "归堆取料上限高度";
            this.UP_MAT_LIMIT_1.Name = "UP_MAT_LIMIT_1";
            this.UP_MAT_LIMIT_1.Width = 83;
            // 
            // DOWN_MAT_LIMIT_1
            // 
            this.DOWN_MAT_LIMIT_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DOWN_MAT_LIMIT_1.DataPropertyName = "DOWN_MAT_LIMIT_1";
            this.DOWN_MAT_LIMIT_1.HeaderText = "归堆取料下限高度";
            this.DOWN_MAT_LIMIT_1.Name = "DOWN_MAT_LIMIT_1";
            this.DOWN_MAT_LIMIT_1.Width = 83;
            // 
            // UP_MAT_LIMIT_2
            // 
            this.UP_MAT_LIMIT_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UP_MAT_LIMIT_2.DataPropertyName = "UP_MAT_LIMIT_2";
            this.UP_MAT_LIMIT_2.HeaderText = "装槽取料上限高度";
            this.UP_MAT_LIMIT_2.Name = "UP_MAT_LIMIT_2";
            this.UP_MAT_LIMIT_2.Width = 83;
            // 
            // DOWN_MAT_LIMIT_2
            // 
            this.DOWN_MAT_LIMIT_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DOWN_MAT_LIMIT_2.DataPropertyName = "DOWN_MAT_LIMIT_2";
            this.DOWN_MAT_LIMIT_2.HeaderText = "装槽取料下限高度";
            this.DOWN_MAT_LIMIT_2.Name = "DOWN_MAT_LIMIT_2";
            this.DOWN_MAT_LIMIT_2.Width = 83;
            // 
            // UP_MAT_LIMIT_3
            // 
            this.UP_MAT_LIMIT_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UP_MAT_LIMIT_3.DataPropertyName = "UP_MAT_LIMIT_3";
            this.UP_MAT_LIMIT_3.HeaderText = "归堆放料上限高度";
            this.UP_MAT_LIMIT_3.Name = "UP_MAT_LIMIT_3";
            this.UP_MAT_LIMIT_3.Width = 83;
            // 
            // DOWN_MAT_LIMIT_3
            // 
            this.DOWN_MAT_LIMIT_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DOWN_MAT_LIMIT_3.DataPropertyName = "DOWN_MAT_LIMIT_3";
            this.DOWN_MAT_LIMIT_3.HeaderText = "归堆放料下限高度";
            this.DOWN_MAT_LIMIT_3.Name = "DOWN_MAT_LIMIT_3";
            this.DOWN_MAT_LIMIT_3.Width = 83;
            // 
            // UP_MAT_LIMIT_4
            // 
            this.UP_MAT_LIMIT_4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UP_MAT_LIMIT_4.DataPropertyName = "UP_MAT_LIMIT_4";
            this.UP_MAT_LIMIT_4.HeaderText = "装槽放料上限高度";
            this.UP_MAT_LIMIT_4.Name = "UP_MAT_LIMIT_4";
            this.UP_MAT_LIMIT_4.Width = 83;
            // 
            // DOWN_MAT_LIMIT_4
            // 
            this.DOWN_MAT_LIMIT_4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DOWN_MAT_LIMIT_4.DataPropertyName = "DOWN_MAT_LIMIT_4";
            this.DOWN_MAT_LIMIT_4.HeaderText = "装槽放料下限高度";
            this.DOWN_MAT_LIMIT_4.Name = "DOWN_MAT_LIMIT_4";
            this.DOWN_MAT_LIMIT_4.Width = 83;
            // 
            // bt_Refurbish
            // 
            this.bt_Refurbish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Refurbish.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_Refurbish.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Refurbish.ForeColor = System.Drawing.Color.White;
            this.bt_Refurbish.Location = new System.Drawing.Point(1082, 578);
            this.bt_Refurbish.Name = "bt_Refurbish";
            this.bt_Refurbish.Size = new System.Drawing.Size(85, 40);
            this.bt_Refurbish.TabIndex = 10;
            this.bt_Refurbish.Text = "刷新";
            this.bt_Refurbish.UseVisualStyleBackColor = true;
            this.bt_Refurbish.Click += new System.EventHandler(this.bt_Refurbish_Click);
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Save.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.bt_Save.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Save.ForeColor = System.Drawing.Color.White;
            this.bt_Save.Location = new System.Drawing.Point(1173, 578);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(85, 40);
            this.bt_Save.TabIndex = 11;
            this.bt_Save.Text = "保存";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // FrmFromStockRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1269, 619);
            this.Controls.Add(this.bt_Refurbish);
            this.Controls.Add(this.bt_Save);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFromStockRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "取料规则";
            this.Load += new System.EventHandler(this.FrmFromStockRule_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAY_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLAMP_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STARTEGY_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn WORK_POS_X_START;
        private System.Windows.Forms.DataGridViewTextBoxColumn WORK_POS_X_END;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLAMP_LENGTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLAMP_WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn UP_MAT_DIR_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOWN_MAT_DIR_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UP_MAT_DIR_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOWN_MAT_DIR_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UP_MAT_LIMIT_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOWN_MAT_LIMIT_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UP_MAT_LIMIT_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOWN_MAT_LIMIT_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UP_MAT_LIMIT_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOWN_MAT_LIMIT_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn UP_MAT_LIMIT_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOWN_MAT_LIMIT_4;
        private System.Windows.Forms.Button bt_Refurbish;
        private System.Windows.Forms.Button bt_Save;
    }
}