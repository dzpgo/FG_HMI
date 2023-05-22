namespace UACSPopupForm.CraneMonitor
{
    partial class FrmCorrectionHeight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCorrectionHeight));
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bt_Submit = new System.Windows.Forms.Button();
            this.cmCraneNO = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_Password
            // 
            this.txt_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Password.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.txt_Password.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Password.Location = new System.Drawing.Point(200, 343);
            this.txt_Password.Multiline = true;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(184, 32);
            this.txt_Password.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(125, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 100;
            this.label3.Text = "密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F);
            this.label1.Location = new System.Drawing.Point(192, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 46);
            this.label1.TabIndex = 98;
            this.label1.Text = "矫正高度";
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
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "关闭窗体";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // bt_Submit
            // 
            this.bt_Submit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_Submit.BackgroundImage")));
            this.bt_Submit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_Submit.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Submit.ForeColor = System.Drawing.Color.White;
            this.bt_Submit.Location = new System.Drawing.Point(61, 487);
            this.bt_Submit.Name = "bt_Submit";
            this.bt_Submit.Size = new System.Drawing.Size(101, 49);
            this.bt_Submit.TabIndex = 2;
            this.bt_Submit.Text = "确定";
            this.bt_Submit.UseVisualStyleBackColor = true;
            this.bt_Submit.Click += new System.EventHandler(this.bt_Submit_Click);
            // 
            // cmCraneNO
            // 
            this.cmCraneNO.BackColor = System.Drawing.SystemColors.Window;
            this.cmCraneNO.Enabled = false;
            this.cmCraneNO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmCraneNO.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmCraneNO.FormattingEnabled = true;
            this.cmCraneNO.Location = new System.Drawing.Point(200, 186);
            this.cmCraneNO.Name = "cmCraneNO";
            this.cmCraneNO.Size = new System.Drawing.Size(184, 34);
            this.cmCraneNO.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(106, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 106;
            this.label2.Text = "行车号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.Location = new System.Drawing.Point(234, 408);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 107;
            this.label4.Text = "密码：123";
            // 
            // FrmCorrectionHeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(556, 563);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmCraneNO);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.bt_Submit);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmCorrectionHeight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "矫正高度";
            this.Load += new System.EventHandler(this.FrmCorrectionHeight_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button bt_Submit;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmCraneNO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}