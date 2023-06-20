namespace UACSPopupForm
{
    partial class SuctionAndReplenishment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuctionAndReplenishment));
            this.txt_MatWeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_ToStock = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_FromStock = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Rdb_Replenishment = new System.Windows.Forms.RadioButton();
            this.Rdb_Suction = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cbb_MatCode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_MatWeight
            // 
            this.txt_MatWeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_MatWeight.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.txt_MatWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_MatWeight.Location = new System.Drawing.Point(205, 398);
            this.txt_MatWeight.MaxLength = 5;
            this.txt_MatWeight.Multiline = true;
            this.txt_MatWeight.Name = "txt_MatWeight";
            this.txt_MatWeight.Size = new System.Drawing.Size(184, 32);
            this.txt_MatWeight.TabIndex = 6;
            this.txt_MatWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_MatWeight_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(116, 405);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 92;
            this.label3.Text = "重量：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F);
            this.label1.Location = new System.Drawing.Point(165, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 46);
            this.label1.TabIndex = 90;
            this.label1.Text = "吸料补料";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label4.Location = new System.Drawing.Point(116, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 25);
            this.label4.TabIndex = 91;
            this.label4.Text = "跨位：";
            // 
            // cmb_ToStock
            // 
            this.cmb_ToStock.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_ToStock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_ToStock.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmb_ToStock.FormattingEnabled = true;
            this.cmb_ToStock.Location = new System.Drawing.Point(205, 254);
            this.cmb_ToStock.Name = "cmb_ToStock";
            this.cmb_ToStock.Size = new System.Drawing.Size(184, 34);
            this.cmb_ToStock.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(393, 487);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 49);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "关闭窗体";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFinish.BackgroundImage")));
            this.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFinish.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnFinish.ForeColor = System.Drawing.Color.White;
            this.btnFinish.Location = new System.Drawing.Point(227, 487);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(101, 49);
            this.btnFinish.TabIndex = 8;
            this.btnFinish.Text = "结束";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(61, 487);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(101, 49);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "开始";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(395, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 92;
            this.label5.Text = "公斤";
            // 
            // cmb_FromStock
            // 
            this.cmb_FromStock.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_FromStock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_FromStock.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmb_FromStock.FormattingEnabled = true;
            this.cmb_FromStock.Location = new System.Drawing.Point(205, 175);
            this.cmb_FromStock.Name = "cmb_FromStock";
            this.cmb_FromStock.Size = new System.Drawing.Size(184, 34);
            this.cmb_FromStock.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(116, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 94;
            this.label2.Text = "工位：";
            // 
            // Rdb_Replenishment
            // 
            this.Rdb_Replenishment.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Rdb_Replenishment.Location = new System.Drawing.Point(309, 90);
            this.Rdb_Replenishment.Name = "Rdb_Replenishment";
            this.Rdb_Replenishment.Size = new System.Drawing.Size(80, 35);
            this.Rdb_Replenishment.TabIndex = 2;
            this.Rdb_Replenishment.Tag = "Liao";
            this.Rdb_Replenishment.Text = "补料";
            this.Rdb_Replenishment.UseVisualStyleBackColor = true;
            // 
            // Rdb_Suction
            // 
            this.Rdb_Suction.Checked = true;
            this.Rdb_Suction.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Rdb_Suction.Location = new System.Drawing.Point(173, 90);
            this.Rdb_Suction.Name = "Rdb_Suction";
            this.Rdb_Suction.Size = new System.Drawing.Size(80, 35);
            this.Rdb_Suction.TabIndex = 1;
            this.Rdb_Suction.TabStop = true;
            this.Rdb_Suction.Tag = "Liao";
            this.Rdb_Suction.Text = "吸料";
            this.Rdb_Suction.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label6.Location = new System.Drawing.Point(116, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 25);
            this.label6.TabIndex = 91;
            this.label6.Text = "物料";
            // 
            // cbb_MatCode
            // 
            this.cbb_MatCode.BackColor = System.Drawing.SystemColors.Window;
            this.cbb_MatCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbb_MatCode.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cbb_MatCode.FormattingEnabled = true;
            this.cbb_MatCode.Location = new System.Drawing.Point(205, 327);
            this.cbb_MatCode.Name = "cbb_MatCode";
            this.cbb_MatCode.Size = new System.Drawing.Size(184, 34);
            this.cbb_MatCode.TabIndex = 5;
            // 
            // SuctionAndReplenishment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(556, 563);
            this.Controls.Add(this.Rdb_Suction);
            this.Controls.Add(this.Rdb_Replenishment);
            this.Controls.Add(this.cmb_FromStock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txt_MatWeight);
            this.Controls.Add(this.cbb_MatCode);
            this.Controls.Add(this.cmb_ToStock);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SuctionAndReplenishment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "吸料补料";
            this.Load += new System.EventHandler(this.SuctionAndReplenishment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox txt_MatWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_ToStock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_FromStock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton Rdb_Replenishment;
        private System.Windows.Forms.RadioButton Rdb_Suction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbb_MatCode;
    }
}