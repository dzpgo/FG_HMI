namespace UACS
{
    partial class FrmStcokNoCoil
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
            this.label6 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtStockFlag = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStockStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetStockNo = new System.Windows.Forms.Button();
            this.txtCoilNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStockNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBayNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(81, 288);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 17);
            this.label7.TabIndex = 31;
            this.label7.Text = "封锁标记(0:可用 1:待判 2:封锁)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(84, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 17);
            this.label6.TabIndex = 30;
            this.label6.Text = "库位状态(0:无卷 1:预订 2:占用)";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(203, 335);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 37);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "取  消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(24, 335);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(98, 37);
            this.btnSelect.TabIndex = 28;
            this.btnSelect.Text = "选  择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtStockFlag
            // 
            this.txtStockFlag.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStockFlag.Location = new System.Drawing.Point(84, 259);
            this.txtStockFlag.Name = "txtStockFlag";
            this.txtStockFlag.ReadOnly = true;
            this.txtStockFlag.Size = new System.Drawing.Size(140, 26);
            this.txtStockFlag.TabIndex = 27;
            this.txtStockFlag.Text = "999999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(20, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 21);
            this.label5.TabIndex = 26;
            this.label5.Text = "标记：";
            // 
            // txtStockStatus
            // 
            this.txtStockStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStockStatus.Location = new System.Drawing.Point(84, 200);
            this.txtStockStatus.Name = "txtStockStatus";
            this.txtStockStatus.ReadOnly = true;
            this.txtStockStatus.Size = new System.Drawing.Size(140, 26);
            this.txtStockStatus.TabIndex = 25;
            this.txtStockStatus.Text = "999999";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(20, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 24;
            this.label4.Text = "状态：";
            // 
            // btnGetStockNo
            // 
            this.btnGetStockNo.Location = new System.Drawing.Point(252, 90);
            this.btnGetStockNo.Name = "btnGetStockNo";
            this.btnGetStockNo.Size = new System.Drawing.Size(63, 23);
            this.btnGetStockNo.TabIndex = 22;
            this.btnGetStockNo.Text = "查询";
            this.btnGetStockNo.UseVisualStyleBackColor = true;
            this.btnGetStockNo.Click += new System.EventHandler(this.btnGetStockNo_Click);
            // 
            // txtCoilNo
            // 
            this.txtCoilNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCoilNo.Location = new System.Drawing.Point(84, 142);
            this.txtCoilNo.Name = "txtCoilNo";
            this.txtCoilNo.ReadOnly = true;
            this.txtCoilNo.Size = new System.Drawing.Size(140, 26);
            this.txtCoilNo.TabIndex = 21;
            this.txtCoilNo.Text = "999999";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(20, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 20;
            this.label3.Text = "钢卷：";
            // 
            // txtStockNo
            // 
            this.txtStockNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStockNo.Location = new System.Drawing.Point(84, 88);
            this.txtStockNo.Name = "txtStockNo";
            this.txtStockNo.Size = new System.Drawing.Size(140, 26);
            this.txtStockNo.TabIndex = 19;
            this.txtStockNo.Text = "999999";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(20, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 18;
            this.label2.Text = "库位：";
            // 
            // txtBayNo
            // 
            this.txtBayNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBayNo.Location = new System.Drawing.Point(84, 37);
            this.txtBayNo.Name = "txtBayNo";
            this.txtBayNo.ReadOnly = true;
            this.txtBayNo.Size = new System.Drawing.Size(140, 26);
            this.txtBayNo.TabIndex = 17;
            this.txtBayNo.Text = "999999";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 16;
            this.label1.Text = "跨别：";
            // 
            // FrmStcokNoCoil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 409);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtStockFlag);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStockStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGetStockNo);
            this.Controls.Add(this.txtCoilNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStockNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBayNo);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStcokNoCoil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选空库位";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtStockFlag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStockStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetStockNo;
        private System.Windows.Forms.TextBox txtCoilNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStockNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBayNo;
        private System.Windows.Forms.Label label1;
    }
}