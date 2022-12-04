namespace UACSPopupForm
{
    partial class FrmSetCoilWidth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetCoilWidth));
            this.button1 = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPackingCode = new System.Windows.Forms.ComboBox();
            this.txtCoilWidth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCoilType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(187, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 40);
            this.button1.TabIndex = 15;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnOk.BackgroundImage")));
            this.BtnOk.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnOk.Location = new System.Drawing.Point(16, 284);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(100, 40);
            this.BtnOk.TabIndex = 14;
            this.BtnOk.Text = "执 行 ";
            this.BtnOk.UseVisualStyleBackColor = false;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "包装代码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(-6, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "宽度(mm)：";
            // 
            // txtPackingCode
            // 
            this.txtPackingCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtPackingCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPackingCode.FormattingEnabled = true;
            this.txtPackingCode.Items.AddRange(new object[] {
            "w0",
            "w1",
            "1w",
            "w2",
            "2w",
            "w4",
            "4w",
            "w5",
            "5w",
            "w7",
            "w8",
            "2m",
            "n1",
            "1n",
            "p0",
            "0p",
            "p2",
            "2p",
            "p5",
            "5p",
            "x5",
            "x6"});
            this.txtPackingCode.Location = new System.Drawing.Point(107, 61);
            this.txtPackingCode.Name = "txtPackingCode";
            this.txtPackingCode.Size = new System.Drawing.Size(180, 29);
            this.txtPackingCode.TabIndex = 16;
            // 
            // txtCoilWidth
            // 
            this.txtCoilWidth.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCoilWidth.FormattingEnabled = true;
            this.txtCoilWidth.Items.AddRange(new object[] {
            "1000",
            "1050",
            "1100",
            "1150",
            "1200",
            "1250",
            "850",
            "900",
            "950",
            "1300",
            "1350",
            "1400",
            "800",
            "750",
            "700",
            "650"});
            this.txtCoilWidth.Location = new System.Drawing.Point(107, 207);
            this.txtCoilWidth.Name = "txtCoilWidth";
            this.txtCoilWidth.Size = new System.Drawing.Size(180, 29);
            this.txtCoilWidth.TabIndex = 17;
            this.txtCoilWidth.SelectedIndexChanged += new System.EventHandler(this.txtCoilWidth_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(2, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 18;
            this.label3.Text = "钢卷类型：";
            // 
            // cmbCoilType
            // 
            this.cmbCoilType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCoilType.FormattingEnabled = true;
            this.cmbCoilType.Items.AddRange(new object[] {
            "普冷",
            "镀锌"});
            this.cmbCoilType.Location = new System.Drawing.Point(107, 134);
            this.cmbCoilType.Name = "cmbCoilType";
            this.cmbCoilType.Size = new System.Drawing.Size(180, 29);
            this.cmbCoilType.TabIndex = 19;
            // 
            // FrmSetCoilWidth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(314, 365);
            this.Controls.Add(this.cmbCoilType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCoilWidth);
            this.Controls.Add(this.txtPackingCode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "FrmSetCoilWidth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "包装纸规格";
            this.Activated += new System.EventHandler(this.FrmSetCoilWidth_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtPackingCode;
        private System.Windows.Forms.ComboBox txtCoilWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCoilType;
    }
}