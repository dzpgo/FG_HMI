﻿namespace UACSParking
{
    partial class FrmCarEntry
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPacking = new System.Windows.Forms.TextBox();
            this.txtCarNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFlag = new System.Windows.Forms.ComboBox();
            this.txtDirection = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCarType = new System.Windows.Forms.ComboBox();
            this.cmbWordMode = new System.Windows.Forms.ComboBox();
            this.cmbScanCar = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "停车位：";
            // 
            // txtPacking
            // 
            this.txtPacking.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPacking.Location = new System.Drawing.Point(180, 45);
            this.txtPacking.Margin = new System.Windows.Forms.Padding(4);
            this.txtPacking.Name = "txtPacking";
            this.txtPacking.Size = new System.Drawing.Size(223, 39);
            this.txtPacking.TabIndex = 1;
            // 
            // txtCarNo
            // 
            this.txtCarNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarNo.Location = new System.Drawing.Point(180, 114);
            this.txtCarNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarNo.Name = "txtCarNo";
            this.txtCarNo.Size = new System.Drawing.Size(223, 39);
            this.txtCarNo.TabIndex = 3;
            this.txtCarNo.TextChanged += new System.EventHandler(this.txtCarNo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(84, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "车号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(36, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "空满标记：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(36, 333);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 31);
            this.label4.TabIndex = 6;
            this.label4.Text = "车头方向：";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(42, 551);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 60);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::UACSView.Properties.Resources.bg_btn;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(251, 551);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 60);
            this.button2.TabIndex = 9;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFlag
            // 
            this.txtFlag.Enabled = false;
            this.txtFlag.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtFlag.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFlag.FormattingEnabled = true;
            this.txtFlag.Items.AddRange(new object[] {
            "1",
            "2"});
            this.txtFlag.Location = new System.Drawing.Point(180, 183);
            this.txtFlag.Margin = new System.Windows.Forms.Padding(4);
            this.txtFlag.Name = "txtFlag";
            this.txtFlag.Size = new System.Drawing.Size(223, 39);
            this.txtFlag.TabIndex = 10;
            this.txtFlag.Text = "空";
            // 
            // txtDirection
            // 
            this.txtDirection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtDirection.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDirection.FormattingEnabled = true;
            this.txtDirection.Location = new System.Drawing.Point(180, 327);
            this.txtDirection.Margin = new System.Windows.Forms.Padding(4);
            this.txtDirection.Name = "txtDirection";
            this.txtDirection.Size = new System.Drawing.Size(223, 39);
            this.txtDirection.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(36, 260);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 31);
            this.label6.TabIndex = 13;
            this.label6.Text = "车辆类型：";
            // 
            // cmbCarType
            // 
            this.cmbCarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbCarType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCarType.FormattingEnabled = true;
            this.cmbCarType.Location = new System.Drawing.Point(180, 255);
            this.cmbCarType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCarType.Name = "cmbCarType";
            this.cmbCarType.Size = new System.Drawing.Size(223, 39);
            this.cmbCarType.TabIndex = 14;
            this.cmbCarType.SelectedIndexChanged += new System.EventHandler(this.cmbCarType_SelectedIndexChanged);
            // 
            // cmbWordMode
            // 
            this.cmbWordMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbWordMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbWordMode.FormattingEnabled = true;
            this.cmbWordMode.Location = new System.Drawing.Point(178, 398);
            this.cmbWordMode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWordMode.Name = "cmbWordMode";
            this.cmbWordMode.Size = new System.Drawing.Size(223, 39);
            this.cmbWordMode.TabIndex = 11;
            // 
            // cmbScanCar
            // 
            this.cmbScanCar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbScanCar.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbScanCar.FormattingEnabled = true;
            this.cmbScanCar.Location = new System.Drawing.Point(178, 472);
            this.cmbScanCar.Margin = new System.Windows.Forms.Padding(4);
            this.cmbScanCar.Name = "cmbScanCar";
            this.cmbScanCar.Size = new System.Drawing.Size(223, 39);
            this.cmbScanCar.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(34, 406);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 31);
            this.label5.TabIndex = 6;
            this.label5.Text = "装料模式：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(36, 472);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 31);
            this.label7.TabIndex = 6;
            this.label7.Text = "扫描行车：";
            // 
            // FrmCarEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(470, 652);
            this.Controls.Add(this.cmbCarType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbScanCar);
            this.Controls.Add(this.cmbWordMode);
            this.Controls.Add(this.txtDirection);
            this.Controls.Add(this.txtFlag);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCarNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPacking);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmCarEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车入位";
            this.Activated += new System.EventHandler(this.FrmCarEntry_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPacking;
        private System.Windows.Forms.TextBox txtCarNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox txtFlag;
        private System.Windows.Forms.ComboBox txtDirection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCarType;
        private System.Windows.Forms.ComboBox cmbWordMode;
        private System.Windows.Forms.ComboBox cmbScanCar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}