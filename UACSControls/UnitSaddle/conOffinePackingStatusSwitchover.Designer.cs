namespace UACSControls
{
    partial class conOffinePackingStatusSwitchover
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubAreaName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnWorking = new System.Windows.Forms.Button();
            this.btnCraneAuto = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnWorking, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCraneAuto, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 253);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Controls.Add(this.lblSubAreaName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 56);
            this.panel1.TabIndex = 0;
            // 
            // lblSubAreaName
            // 
            this.lblSubAreaName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSubAreaName.AutoSize = true;
            this.lblSubAreaName.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSubAreaName.ForeColor = System.Drawing.Color.White;
            this.lblSubAreaName.Location = new System.Drawing.Point(94, 14);
            this.lblSubAreaName.Name = "lblSubAreaName";
            this.lblSubAreaName.Size = new System.Drawing.Size(62, 31);
            this.lblSubAreaName.TabIndex = 0;
            this.lblSubAreaName.Text = "小区";
            this.lblSubAreaName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.Color.Brown;
            this.lblStatus.Location = new System.Drawing.Point(50, 75);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(162, 39);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "指令执行中";
            // 
            // btnWorking
            // 
            this.btnWorking.BackColor = System.Drawing.Color.SkyBlue;
            this.btnWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWorking.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWorking.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWorking.Location = new System.Drawing.Point(4, 130);
            this.btnWorking.Name = "btnWorking";
            this.btnWorking.Size = new System.Drawing.Size(255, 56);
            this.btnWorking.TabIndex = 4;
            this.btnWorking.Text = "包装作业中";
            this.btnWorking.UseVisualStyleBackColor = false;
            this.btnWorking.Click += new System.EventHandler(this.btnWorking_Click);
            // 
            // btnCraneAuto
            // 
            this.btnCraneAuto.BackColor = System.Drawing.Color.SkyBlue;
            this.btnCraneAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCraneAuto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCraneAuto.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCraneAuto.Location = new System.Drawing.Point(4, 193);
            this.btnCraneAuto.Name = "btnCraneAuto";
            this.btnCraneAuto.Size = new System.Drawing.Size(255, 56);
            this.btnCraneAuto.TabIndex = 6;
            this.btnCraneAuto.Text = "自动吊运";
            this.btnCraneAuto.UseVisualStyleBackColor = false;
            this.btnCraneAuto.Click += new System.EventHandler(this.btnCraneAuto_Click);
            // 
            // conOffinePackingStatusSwitchover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "conOffinePackingStatusSwitchover";
            this.Size = new System.Drawing.Size(263, 253);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnWorking;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSubAreaName;
        private System.Windows.Forms.Button btnCraneAuto;

    }
}
