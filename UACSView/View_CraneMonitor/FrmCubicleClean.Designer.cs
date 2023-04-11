﻿namespace UACSView.View_CraneMonitor
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
            this.SuspendLayout();
            // 
            // cmb_Cubicle
            // 
            this.cmb_Cubicle.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_Cubicle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Cubicle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Cubicle.FormattingEnabled = true;
            this.cmb_Cubicle.Location = new System.Drawing.Point(137, 136);
            this.cmb_Cubicle.Name = "cmb_Cubicle";
            this.cmb_Cubicle.Size = new System.Drawing.Size(141, 30);
            this.cmb_Cubicle.TabIndex = 2;
            // 
            // cmb_CraneNO
            // 
            this.cmb_CraneNO.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_CraneNO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_CraneNO.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_CraneNO.FormattingEnabled = true;
            this.cmb_CraneNO.Location = new System.Drawing.Point(139, 81);
            this.cmb_CraneNO.Name = "cmb_CraneNO";
            this.cmb_CraneNO.Size = new System.Drawing.Size(141, 30);
            this.cmb_CraneNO.TabIndex = 1;
            this.cmb_CraneNO.SelectedIndexChanged += new System.EventHandler(this.cm_CraneNO_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(82, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 108;
            this.label3.Text = "工位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(68, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 107;
            this.label2.Text = "行车号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F);
            this.label1.Location = new System.Drawing.Point(153, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 46);
            this.label1.TabIndex = 106;
            this.label1.Text = "清扫";
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(277, 267);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 33);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFinish.BackgroundImage")));
            this.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFinish.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnFinish.ForeColor = System.Drawing.Color.White;
            this.btnFinish.Location = new System.Drawing.Point(153, 267);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(100, 33);
            this.btnFinish.TabIndex = 5;
            this.btnFinish.Text = "清扫完成";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // bt_Confirm
            // 
            this.bt_Confirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_Confirm.BackgroundImage")));
            this.bt_Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_Confirm.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.bt_Confirm.ForeColor = System.Drawing.Color.White;
            this.bt_Confirm.Location = new System.Drawing.Point(26, 267);
            this.bt_Confirm.Name = "bt_Confirm";
            this.bt_Confirm.Size = new System.Drawing.Size(100, 33);
            this.bt_Confirm.TabIndex = 4;
            this.bt_Confirm.Text = "确定";
            this.bt_Confirm.UseVisualStyleBackColor = true;
            this.bt_Confirm.Click += new System.EventHandler(this.bt_Confirm_Click);
            // 
            // cmb_MatCode
            // 
            this.cmb_MatCode.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_MatCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_MatCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_MatCode.FormattingEnabled = true;
            this.cmb_MatCode.Location = new System.Drawing.Point(137, 194);
            this.cmb_MatCode.Name = "cmb_MatCode";
            this.cmb_MatCode.Size = new System.Drawing.Size(141, 30);
            this.cmb_MatCode.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(82, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 110;
            this.label4.Text = "物料：";
            // 
            // FrmCubicleClean
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(403, 322);
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
    }
}