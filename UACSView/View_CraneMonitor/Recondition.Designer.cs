namespace UACSView.View_CraneMonitor
{
    partial class Recondition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recondition));
            this.txt_Act_X = new System.Windows.Forms.TextBox();
            this.cmCraneNO = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_Act_X
            // 
            this.txt_Act_X.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Act_X.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.txt_Act_X.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Act_X.Location = new System.Drawing.Point(195, 310);
            this.txt_Act_X.Multiline = true;
            this.txt_Act_X.Name = "txt_Act_X";
            this.txt_Act_X.Size = new System.Drawing.Size(184, 32);
            this.txt_Act_X.TabIndex = 2;
            this.txt_Act_X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Act_X_KeyPress);
            // 
            // cmCraneNO
            // 
            this.cmCraneNO.BackColor = System.Drawing.SystemColors.Window;
            this.cmCraneNO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmCraneNO.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cmCraneNO.FormattingEnabled = true;
            this.cmCraneNO.Location = new System.Drawing.Point(195, 176);
            this.cmCraneNO.Name = "cmCraneNO";
            this.cmCraneNO.Size = new System.Drawing.Size(184, 34);
            this.cmCraneNO.TabIndex = 1;
            this.cmCraneNO.SelectedIndexChanged += new System.EventHandler(this.cmCraneNO_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(108, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 25);
            this.label3.TabIndex = 92;
            this.label3.Text = "X坐标：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(101, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 91;
            this.label2.Text = "行车号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F);
            this.label1.Location = new System.Drawing.Point(219, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 46);
            this.label1.TabIndex = 90;
            this.label1.Text = "检修";
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
            this.btnCancel.TabIndex = 5;
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
            this.btnFinish.TabIndex = 4;
            this.btnFinish.Text = "结束检修";
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
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "开始检修";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // Recondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(556, 563);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txt_Act_X);
            this.Controls.Add(this.cmCraneNO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Recondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检修";
            this.Load += new System.EventHandler(this.Recondition_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox txt_Act_X;
        private System.Windows.Forms.ComboBox cmCraneNO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}