namespace UACSView.View_CraneMonitor
{
    partial class FrmCubicleClean
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCubicleClean));
            this.cmb_Cubicle = new System.Windows.Forms.ComboBox();
            this.cmb_CraneNO = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.bt_Confirm = new System.Windows.Forms.Button();
            this.cmb_MatCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Height = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmb_Cubicle
            // 
            this.cmb_Cubicle.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_Cubicle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Cubicle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmb_Cubicle.FormattingEnabled = true;
            this.cmb_Cubicle.Location = new System.Drawing.Point(195, 212);
            this.cmb_Cubicle.Name = "cmb_Cubicle";
            this.cmb_Cubicle.Size = new System.Drawing.Size(184, 34);
            this.cmb_Cubicle.TabIndex = 2;
            // 
            // cmb_CraneNO
            // 
            this.cmb_CraneNO.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_CraneNO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_CraneNO.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmb_CraneNO.FormattingEnabled = true;
            this.cmb_CraneNO.Location = new System.Drawing.Point(195, 122);
            this.cmb_CraneNO.Name = "cmb_CraneNO";
            this.cmb_CraneNO.Size = new System.Drawing.Size(184, 34);
            this.cmb_CraneNO.TabIndex = 1;
            this.cmb_CraneNO.SelectedIndexChanged += new System.EventHandler(this.cm_CraneNO_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(110, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 108;
            this.label3.Text = "工位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(91, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 107;
            this.label2.Text = "行车号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F);
            this.label1.Location = new System.Drawing.Point(187, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 46);
            this.label1.TabIndex = 106;
            this.label1.Text = "工位清扫";
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
            this.btnCancel.TabIndex = 7;
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
            this.btnFinish.TabIndex = 6;
            this.btnFinish.Text = "结束清扫";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // bt_Confirm
            // 
            this.bt_Confirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_Confirm.BackgroundImage")));
            this.bt_Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_Confirm.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Confirm.ForeColor = System.Drawing.Color.White;
            this.bt_Confirm.Location = new System.Drawing.Point(61, 487);
            this.bt_Confirm.Name = "bt_Confirm";
            this.bt_Confirm.Size = new System.Drawing.Size(101, 49);
            this.bt_Confirm.TabIndex = 5;
            this.bt_Confirm.Text = "开始清扫";
            this.bt_Confirm.UseVisualStyleBackColor = true;
            this.bt_Confirm.Click += new System.EventHandler(this.bt_Confirm_Click);
            // 
            // cmb_MatCode
            // 
            this.cmb_MatCode.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_MatCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_MatCode.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmb_MatCode.FormattingEnabled = true;
            this.cmb_MatCode.Location = new System.Drawing.Point(195, 302);
            this.cmb_MatCode.Name = "cmb_MatCode";
            this.cmb_MatCode.Size = new System.Drawing.Size(184, 34);
            this.cmb_MatCode.TabIndex = 3;
            this.cmb_MatCode.SelectedIndexChanged += new System.EventHandler(this.cmb_MatCode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label4.Location = new System.Drawing.Point(110, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 25);
            this.label4.TabIndex = 110;
            this.label4.Text = "物料：";
            // 
            // txt_Height
            // 
            this.txt_Height.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Height.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.txt_Height.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Height.Location = new System.Drawing.Point(195, 386);
            this.txt_Height.Multiline = true;
            this.txt_Height.Name = "txt_Height";
            this.txt_Height.ReadOnly = true;
            this.txt_Height.Size = new System.Drawing.Size(184, 32);
            this.txt_Height.TabIndex = 4;
            this.txt_Height.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Height_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label5.Location = new System.Drawing.Point(110, 393);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 25);
            this.label5.TabIndex = 112;
            this.label5.Text = "高度：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(389, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 20);
            this.label6.TabIndex = 112;
            this.label6.Text = "毫米";
            // 
            // FrmCubicleClean
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(556, 563);
            this.Controls.Add(this.txt_Height);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_MatCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.bt_Confirm);
            this.Controls.Add(this.cmb_Cubicle);
            this.Controls.Add(this.cmb_CraneNO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmCubicleClean";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工位清扫";
            this.Load += new System.EventHandler(this.FrmCubicleClean_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button bt_Confirm;
        private System.Windows.Forms.ComboBox cmb_Cubicle;
        private System.Windows.Forms.ComboBox cmb_CraneNO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_MatCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Height;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}