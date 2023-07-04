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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.plParking = new System.Windows.Forms.Panel();
            this.dgvCraneOder = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvStowageMessage = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPacking = new System.Windows.Forms.Label();
            this.lblCarNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCarStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCarType = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_REQ_WEIGHT = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lb_ACT_WEIGHT = new System.Windows.Forms.Label();
            this.bt_Refurbish = new System.Windows.Forms.Button();
            this.bt_Save = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bt_Send = new System.Windows.Forms.Button();
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
            this.plParking.Size = new System.Drawing.Size(1269, 563);
            this.plParking.TabIndex = 0;
            // 
            // dgvCraneOder
            // 
            this.dgvCraneOder.AllowUserToAddRows = false;
            this.dgvCraneOder.AllowUserToDeleteRows = false;
            this.dgvCraneOder.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgvCraneOder.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCraneOder.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCraneOder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCraneOder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCraneOder.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCraneOder.EnableHeadersVisualStyles = false;
            this.dgvCraneOder.Location = new System.Drawing.Point(12, 322);
            this.dgvCraneOder.Name = "dgvCraneOder";
            this.dgvCraneOder.RowHeadersVisible = false;
            this.dgvCraneOder.RowHeadersWidth = 62;
            this.dgvCraneOder.RowTemplate.Height = 23;
            this.dgvCraneOder.Size = new System.Drawing.Size(1245, 229);
            this.dgvCraneOder.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(16, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(329, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "---指令信息-----------------------------------------";
            // 
            // dgvStowageMessage
            // 
            this.dgvStowageMessage.AllowUserToAddRows = false;
            this.dgvStowageMessage.AllowUserToDeleteRows = false;
            this.dgvStowageMessage.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            this.dgvStowageMessage.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStowageMessage.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStowageMessage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStowageMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStowageMessage.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvStowageMessage.EnableHeadersVisualStyles = false;
            this.dgvStowageMessage.Location = new System.Drawing.Point(12, 28);
            this.dgvStowageMessage.Name = "dgvStowageMessage";
            this.dgvStowageMessage.RowHeadersVisible = false;
            this.dgvStowageMessage.RowHeadersWidth = 62;
            this.dgvStowageMessage.RowTemplate.Height = 23;
            this.dgvStowageMessage.Size = new System.Drawing.Size(1245, 258);
            this.dgvStowageMessage.TabIndex = 8;
            this.dgvStowageMessage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStowageMessage_CellContentClick);
            this.dgvStowageMessage.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvStowageMessage_CellPainting);
            this.dgvStowageMessage.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvStowageMessage_CurrentCellDirtyStateChanged);
            this.dgvStowageMessage.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvStowageMessage_EditingControlShowing);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 26);
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
            this.label3.Location = new System.Drawing.Point(16, 579);
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
            this.lblPacking.Location = new System.Drawing.Point(87, 579);
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
            this.lblCarNo.Location = new System.Drawing.Point(212, 579);
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
            this.label6.Location = new System.Drawing.Point(157, 579);
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
            this.lblCarStatus.Location = new System.Drawing.Point(506, 579);
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
            this.label8.Location = new System.Drawing.Point(449, 579);
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
            this.lblCarType.Location = new System.Drawing.Point(376, 579);
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
            this.label5.Location = new System.Drawing.Point(287, 579);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 22);
            this.label5.TabIndex = 7;
            this.label5.Text = "车辆类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(577, 579);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 5;
            this.label4.Text = "要求重量：";
            // 
            // lb_REQ_WEIGHT
            // 
            this.lb_REQ_WEIGHT.AutoSize = true;
            this.lb_REQ_WEIGHT.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_REQ_WEIGHT.ForeColor = System.Drawing.Color.Black;
            this.lb_REQ_WEIGHT.Location = new System.Drawing.Point(659, 579);
            this.lb_REQ_WEIGHT.Name = "lb_REQ_WEIGHT";
            this.lb_REQ_WEIGHT.Size = new System.Drawing.Size(70, 22);
            this.lb_REQ_WEIGHT.TabIndex = 6;
            this.lb_REQ_WEIGHT.Text = "999999";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(730, 579);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 22);
            this.label9.TabIndex = 5;
            this.label9.Text = "累计重量：";
            // 
            // lb_ACT_WEIGHT
            // 
            this.lb_ACT_WEIGHT.AutoSize = true;
            this.lb_ACT_WEIGHT.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ACT_WEIGHT.ForeColor = System.Drawing.Color.Black;
            this.lb_ACT_WEIGHT.Location = new System.Drawing.Point(815, 579);
            this.lb_ACT_WEIGHT.Name = "lb_ACT_WEIGHT";
            this.lb_ACT_WEIGHT.Size = new System.Drawing.Size(70, 22);
            this.lb_ACT_WEIGHT.TabIndex = 6;
            this.lb_ACT_WEIGHT.Text = "999999";
            // 
            // bt_Refurbish
            // 
            this.bt_Refurbish.BackgroundImage = global::UACSPopupForm.Properties.Resources.bg_btn;
            this.bt_Refurbish.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Refurbish.ForeColor = System.Drawing.Color.White;
            this.bt_Refurbish.Location = new System.Drawing.Point(1081, 569);
            this.bt_Refurbish.Name = "bt_Refurbish";
            this.bt_Refurbish.Size = new System.Drawing.Size(85, 40);
            this.bt_Refurbish.TabIndex = 9;
            this.bt_Refurbish.Text = "刷新";
            this.bt_Refurbish.UseVisualStyleBackColor = true;
            this.bt_Refurbish.Click += new System.EventHandler(this.bt_Refurbish_Click);
            // 
            // bt_Save
            // 
            this.bt_Save.BackgroundImage = global::UACSPopupForm.Properties.Resources.bg_btn;
            this.bt_Save.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Save.ForeColor = System.Drawing.Color.White;
            this.bt_Save.Location = new System.Drawing.Point(1172, 569);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(85, 40);
            this.bt_Save.TabIndex = 9;
            this.bt_Save.Text = "保存";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::UACSPopupForm.Properties.Resources.bg_btn;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(851, 569);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 40);
            this.button1.TabIndex = 9;
            this.button1.Text = "详情";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt_Send
            // 
            this.bt_Send.BackgroundImage = global::UACSPopupForm.Properties.Resources.bg_btn;
            this.bt_Send.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Send.ForeColor = System.Drawing.Color.White;
            this.bt_Send.Location = new System.Drawing.Point(942, 569);
            this.bt_Send.Name = "bt_Send";
            this.bt_Send.Size = new System.Drawing.Size(85, 40);
            this.bt_Send.TabIndex = 9;
            this.bt_Send.Text = "发送";
            this.bt_Send.UseVisualStyleBackColor = true;
            this.bt_Send.Click += new System.EventHandler(this.bt_Send_Click);
            // 
            // FrmParkingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1269, 619);
            this.Controls.Add(this.bt_Refurbish);
            this.Controls.Add(this.bt_Save);
            this.Controls.Add(this.bt_Send);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblCarType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_ACT_WEIGHT);
            this.Controls.Add(this.lb_REQ_WEIGHT);
            this.Controls.Add(this.lblCarStatus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_REQ_WEIGHT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lb_ACT_WEIGHT;
        private System.Windows.Forms.Button bt_Refurbish;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.Button bt_Send;
        // private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}