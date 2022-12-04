namespace UACSPopupForm
{
    partial class FrmUnitRotateRequest
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
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCoilno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCloseToYard = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCoilStatus = new System.Windows.Forms.Label();
            this.lblPackCode = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblOutdia = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInDia = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblStockStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbCraneNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbFromStock = new System.Windows.Forms.ComboBox();
            this.cbbToStock = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbbRotate = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(206, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 39);
            this.label7.TabIndex = 0;
            this.label7.Text = "旋转指令";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(106, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "钢卷号：";
            // 
            // txtCoilno
            // 
            this.txtCoilno.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCoilno.Location = new System.Drawing.Point(200, 227);
            this.txtCoilno.Multiline = true;
            this.txtCoilno.Name = "txtCoilno";
            this.txtCoilno.Size = new System.Drawing.Size(185, 28);
            this.txtCoilno.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(106, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "起卷位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(106, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "落卷位：";
            // 
            // BtnOk
            // 
            this.BtnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnOk.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnOk.Location = new System.Drawing.Point(47, 479);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(114, 51);
            this.BtnOk.TabIndex = 7;
            this.BtnOk.Text = "执 行 ";
            this.BtnOk.UseVisualStyleBackColor = false;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCloseToYard
            // 
            this.BtnCloseToYard.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnCloseToYard.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCloseToYard.Location = new System.Drawing.Point(213, 479);
            this.BtnCloseToYard.Name = "BtnCloseToYard";
            this.BtnCloseToYard.Size = new System.Drawing.Size(114, 51);
            this.BtnCloseToYard.TabIndex = 8;
            this.BtnCloseToYard.Text = "取 消";
            this.BtnCloseToYard.UseVisualStyleBackColor = false;
            this.BtnCloseToYard.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(380, 479);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(114, 51);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "关闭";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(197, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblCoilStatus);
            this.groupBox1.Controls.Add(this.lblPackCode);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblOutdia);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.lblInDia);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblWidth);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblWeight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblStockStatus);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(47, 424);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 49);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检查信息";
            this.groupBox1.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(13, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "钢卷号是否存在多处：";
            // 
            // lblCoilStatus
            // 
            this.lblCoilStatus.AutoSize = true;
            this.lblCoilStatus.ForeColor = System.Drawing.Color.Red;
            this.lblCoilStatus.Location = new System.Drawing.Point(175, 47);
            this.lblCoilStatus.Name = "lblCoilStatus";
            this.lblCoilStatus.Size = new System.Drawing.Size(33, 20);
            this.lblCoilStatus.TabIndex = 12;
            this.lblCoilStatus.Text = "999";
            // 
            // lblPackCode
            // 
            this.lblPackCode.AutoSize = true;
            this.lblPackCode.Location = new System.Drawing.Point(253, 103);
            this.lblPackCode.Name = "lblPackCode";
            this.lblPackCode.Size = new System.Drawing.Size(33, 20);
            this.lblPackCode.TabIndex = 11;
            this.lblPackCode.Text = "999";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(165, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 20);
            this.label13.TabIndex = 10;
            this.label13.Text = "包装代码：";
            // 
            // lblOutdia
            // 
            this.lblOutdia.AutoSize = true;
            this.lblOutdia.Location = new System.Drawing.Point(70, 103);
            this.lblOutdia.Name = "lblOutdia";
            this.lblOutdia.Size = new System.Drawing.Size(33, 20);
            this.lblOutdia.TabIndex = 9;
            this.lblOutdia.Text = "999";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 20);
            this.label15.TabIndex = 8;
            this.label15.Text = "外径：";
            // 
            // lblInDia
            // 
            this.lblInDia.AutoSize = true;
            this.lblInDia.Location = new System.Drawing.Point(365, 77);
            this.lblInDia.Name = "lblInDia";
            this.lblInDia.Size = new System.Drawing.Size(33, 20);
            this.lblInDia.TabIndex = 7;
            this.lblInDia.Text = "999";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(308, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 6;
            this.label12.Text = "内径：";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(227, 77);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(33, 20);
            this.lblWidth.TabIndex = 5;
            this.lblWidth.Text = "999";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(165, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "宽度：";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(70, 77);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(33, 20);
            this.lblWeight.TabIndex = 3;
            this.lblWeight.Text = "999";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "重量：";
            // 
            // lblStockStatus
            // 
            this.lblStockStatus.AutoSize = true;
            this.lblStockStatus.Location = new System.Drawing.Point(250, 15);
            this.lblStockStatus.Name = "lblStockStatus";
            this.lblStockStatus.Size = new System.Drawing.Size(33, 20);
            this.lblStockStatus.TabIndex = 1;
            this.lblStockStatus.Text = "999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "库位状态：";
            // 
            // cbbCraneNo
            // 
            this.cbbCraneNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbCraneNo.FormattingEnabled = true;
            this.cbbCraneNo.Location = new System.Drawing.Point(200, 83);
            this.cbbCraneNo.Name = "cbbCraneNo";
            this.cbbCraneNo.Size = new System.Drawing.Size(185, 28);
            this.cbbCraneNo.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(106, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 48;
            this.label1.Text = "行车号：";
            // 
            // cbbFromStock
            // 
            this.cbbFromStock.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbFromStock.FormattingEnabled = true;
            this.cbbFromStock.Location = new System.Drawing.Point(200, 155);
            this.cbbFromStock.Name = "cbbFromStock";
            this.cbbFromStock.Size = new System.Drawing.Size(185, 28);
            this.cbbFromStock.TabIndex = 49;
            this.cbbFromStock.SelectedIndexChanged += new System.EventHandler(this.cbbFromStock_SelectedIndexChanged);
            // 
            // cbbToStock
            // 
            this.cbbToStock.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbToStock.FormattingEnabled = true;
            this.cbbToStock.Location = new System.Drawing.Point(200, 371);
            this.cbbToStock.Name = "cbbToStock";
            this.cbbToStock.Size = new System.Drawing.Size(185, 28);
            this.cbbToStock.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(106, 298);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 25);
            this.label11.TabIndex = 51;
            this.label11.Text = "是否旋转：";
            // 
            // cbbRotate
            // 
            this.cbbRotate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbRotate.FormattingEnabled = true;
            this.cbbRotate.Items.AddRange(new object[] {
            "旋转",
            "不旋转"});
            this.cbbRotate.Location = new System.Drawing.Point(219, 299);
            this.cbbRotate.Name = "cbbRotate";
            this.cbbRotate.Size = new System.Drawing.Size(166, 28);
            this.cbbRotate.TabIndex = 52;
            // 
            // FrmUnitRotateRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(556, 563);
            this.Controls.Add(this.cbbRotate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbbToStock);
            this.Controls.Add(this.cbbFromStock);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbCraneNo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.BtnCloseToYard);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCoilno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmUnitRotateRequest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "`";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCoilno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCloseToYard;
        private System.Windows.Forms.Button btnClear;
        //       private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStockStatus;
        private System.Windows.Forms.Label lblInDia;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPackCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblOutdia;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCoilStatus;
        private System.Windows.Forms.ComboBox cbbCraneNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbFromStock;
        private System.Windows.Forms.ComboBox cbbToStock;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbbRotate;
    }
}