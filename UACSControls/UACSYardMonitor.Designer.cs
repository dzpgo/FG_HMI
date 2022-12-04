namespace UACSControls
{
    partial class UACSYardMonitor
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlYard = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.Label();
            this.conCrane4 = new UACSControls.conCrane();
            this.conCrane3 = new UACSControls.conCrane();
            this.conCrane2 = new UACSControls.conCrane();
            this.conCrane1 = new UACSControls.conCrane();
            this.tlpnlCrStatus = new System.Windows.Forms.TableLayoutPanel();
            this.conCraneStatus4 = new UACSControls.conCraneStatus();
            this.conCraneStatus3 = new UACSControls.conCraneStatus();
            this.conCraneStatus2 = new UACSControls.conCraneStatus();
            this.conCraneStatus1 = new UACSControls.conCraneStatus();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labBayNO = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnShowCrane = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnShowXY = new System.Windows.Forms.Button();
            this.btnSeekCoil = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlYard.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tlpnlCrStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlYard, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tlpnlCrStatus, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1042, 550);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlYard
            // 
            this.pnlYard.BackColor = System.Drawing.Color.YellowGreen;
            this.pnlYard.Controls.Add(this.panel4);
            this.pnlYard.Controls.Add(this.conCrane4);
            this.pnlYard.Controls.Add(this.conCrane3);
            this.pnlYard.Controls.Add(this.conCrane2);
            this.pnlYard.Controls.Add(this.conCrane1);
            this.pnlYard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlYard.Location = new System.Drawing.Point(3, 58);
            this.pnlYard.Name = "pnlYard";
            this.pnlYard.Size = new System.Drawing.Size(1036, 309);
            this.pnlYard.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtY);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtX);
            this.panel4.Location = new System.Drawing.Point(913, 249);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 60);
            this.panel4.TabIndex = 18;
            this.panel4.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "X:";
            // 
            // txtY
            // 
            this.txtY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtY.AutoSize = true;
            this.txtY.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtY.ForeColor = System.Drawing.Color.Black;
            this.txtY.Location = new System.Drawing.Point(36, 31);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(64, 21);
            this.txtY.TabIndex = 15;
            this.txtY.Text = "999999";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y:";
            // 
            // txtX
            // 
            this.txtX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtX.AutoSize = true;
            this.txtX.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtX.ForeColor = System.Drawing.Color.Black;
            this.txtX.Location = new System.Drawing.Point(36, 0);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(64, 21);
            this.txtX.TabIndex = 14;
            this.txtX.Text = "999999";
            // 
            // conCrane4
            // 
            this.conCrane4.BackColor = System.Drawing.Color.Transparent;
            this.conCrane4.CraneNO = "";
            this.conCrane4.Location = new System.Drawing.Point(395, 3);
            this.conCrane4.Name = "conCrane4";
            this.conCrane4.Size = new System.Drawing.Size(47, 408);
            this.conCrane4.TabIndex = 3;
            this.conCrane4.Visible = false;
            // 
            // conCrane3
            // 
            this.conCrane3.BackColor = System.Drawing.Color.Transparent;
            this.conCrane3.CraneNO = "";
            this.conCrane3.Location = new System.Drawing.Point(311, 3);
            this.conCrane3.Name = "conCrane3";
            this.conCrane3.Size = new System.Drawing.Size(47, 408);
            this.conCrane3.TabIndex = 2;
            this.conCrane3.Visible = false;
            // 
            // conCrane2
            // 
            this.conCrane2.BackColor = System.Drawing.Color.Transparent;
            this.conCrane2.CraneNO = "";
            this.conCrane2.Location = new System.Drawing.Point(97, 3);
            this.conCrane2.Name = "conCrane2";
            this.conCrane2.Size = new System.Drawing.Size(47, 408);
            this.conCrane2.TabIndex = 1;
            this.conCrane2.Visible = false;
            // 
            // conCrane1
            // 
            this.conCrane1.BackColor = System.Drawing.Color.Transparent;
            this.conCrane1.CraneNO = "";
            this.conCrane1.Location = new System.Drawing.Point(16, 5);
            this.conCrane1.Name = "conCrane1";
            this.conCrane1.Size = new System.Drawing.Size(47, 408);
            this.conCrane1.TabIndex = 0;
            this.conCrane1.Visible = false;
            // 
            // tlpnlCrStatus
            // 
            this.tlpnlCrStatus.ColumnCount = 4;
            this.tlpnlCrStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpnlCrStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpnlCrStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpnlCrStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpnlCrStatus.Controls.Add(this.conCraneStatus4, 3, 0);
            this.tlpnlCrStatus.Controls.Add(this.conCraneStatus3, 2, 0);
            this.tlpnlCrStatus.Controls.Add(this.conCraneStatus2, 1, 0);
            this.tlpnlCrStatus.Controls.Add(this.conCraneStatus1, 0, 0);
            this.tlpnlCrStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpnlCrStatus.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tlpnlCrStatus.Location = new System.Drawing.Point(3, 373);
            this.tlpnlCrStatus.Name = "tlpnlCrStatus";
            this.tlpnlCrStatus.RowCount = 1;
            this.tlpnlCrStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlCrStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tlpnlCrStatus.Size = new System.Drawing.Size(1036, 174);
            this.tlpnlCrStatus.TabIndex = 1;
            // 
            // conCraneStatus4
            // 
            this.conCraneStatus4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus4.CraneNO = "";
            this.conCraneStatus4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus4.Location = new System.Drawing.Point(780, 3);
            this.conCraneStatus4.Name = "conCraneStatus4";
            this.conCraneStatus4.Size = new System.Drawing.Size(253, 168);
            this.conCraneStatus4.TabIndex = 3;
            this.conCraneStatus4.Visible = false;
            // 
            // conCraneStatus3
            // 
            this.conCraneStatus3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus3.CraneNO = "";
            this.conCraneStatus3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus3.Location = new System.Drawing.Point(521, 3);
            this.conCraneStatus3.Name = "conCraneStatus3";
            this.conCraneStatus3.Size = new System.Drawing.Size(253, 168);
            this.conCraneStatus3.TabIndex = 2;
            this.conCraneStatus3.Visible = false;
            // 
            // conCraneStatus2
            // 
            this.conCraneStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus2.CraneNO = "";
            this.conCraneStatus2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus2.Location = new System.Drawing.Point(262, 3);
            this.conCraneStatus2.Name = "conCraneStatus2";
            this.conCraneStatus2.Size = new System.Drawing.Size(253, 168);
            this.conCraneStatus2.TabIndex = 1;
            this.conCraneStatus2.Visible = false;
            // 
            // conCraneStatus1
            // 
            this.conCraneStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus1.CraneNO = "";
            this.conCraneStatus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus1.Location = new System.Drawing.Point(3, 3);
            this.conCraneStatus1.Name = "conCraneStatus1";
            this.conCraneStatus1.Size = new System.Drawing.Size(253, 168);
            this.conCraneStatus1.TabIndex = 0;
            this.conCraneStatus1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labBayNO);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.btnShow);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 49);
            this.panel1.TabIndex = 2;
            // 
            // labBayNO
            // 
            this.labBayNO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labBayNO.AutoSize = true;
            this.labBayNO.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labBayNO.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labBayNO.Location = new System.Drawing.Point(481, 4);
            this.labBayNO.Name = "labBayNO";
            this.labBayNO.Size = new System.Drawing.Size(50, 42);
            this.labBayNO.TabIndex = 41;
            this.labBayNO.Text = "跨";
            // 
            // panel8
            // 
            this.panel8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel8.Controls.Add(this.btnShowCrane);
            this.panel8.Controls.Add(this.button1);
            this.panel8.Controls.Add(this.btnShowXY);
            this.panel8.Controls.Add(this.btnSeekCoil);
            this.panel8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel8.Location = new System.Drawing.Point(537, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(436, 43);
            this.panel8.TabIndex = 39;
            this.panel8.Visible = false;
            // 
            // btnShowCrane
            // 
            this.btnShowCrane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowCrane.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowCrane.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShowCrane.ForeColor = System.Drawing.Color.Black;
            this.btnShowCrane.Location = new System.Drawing.Point(333, 5);
            this.btnShowCrane.Name = "btnShowCrane";
            this.btnShowCrane.Size = new System.Drawing.Size(101, 31);
            this.btnShowCrane.TabIndex = 9;
            this.btnShowCrane.Text = "隐藏行车";
            this.btnShowCrane.UseVisualStyleBackColor = true;
            this.btnShowCrane.Click += new System.EventHandler(this.btnShowCrane_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(14, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 31);
            this.button1.TabIndex = 36;
            this.button1.Text = "多库位报警";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnShowXY
            // 
            this.btnShowXY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowXY.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowXY.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShowXY.ForeColor = System.Drawing.Color.Black;
            this.btnShowXY.Location = new System.Drawing.Point(228, 5);
            this.btnShowXY.Name = "btnShowXY";
            this.btnShowXY.Size = new System.Drawing.Size(101, 31);
            this.btnShowXY.TabIndex = 28;
            this.btnShowXY.Text = "显示XY";
            this.btnShowXY.UseVisualStyleBackColor = true;
            this.btnShowXY.Click += new System.EventHandler(this.btnShowXY_Click);
            // 
            // btnSeekCoil
            // 
            this.btnSeekCoil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeekCoil.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSeekCoil.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSeekCoil.ForeColor = System.Drawing.Color.Black;
            this.btnSeekCoil.Location = new System.Drawing.Point(121, 5);
            this.btnSeekCoil.Name = "btnSeekCoil";
            this.btnSeekCoil.Size = new System.Drawing.Size(101, 31);
            this.btnSeekCoil.TabIndex = 29;
            this.btnSeekCoil.Text = "库区找卷";
            this.btnSeekCoil.UseVisualStyleBackColor = true;
            this.btnSeekCoil.Click += new System.EventHandler(this.btnSeekCoil_Click);
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShow.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShow.Location = new System.Drawing.Point(993, 8);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(36, 31);
            this.btnShow.TabIndex = 40;
            this.btnShow.Text = "<";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::UACSControls.Properties.Resources.baosight;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(270, 49);
            this.panel7.TabIndex = 38;
            // 
            // UACSYardMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UACSYardMonitor";
            this.Size = new System.Drawing.Size(1042, 550);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlYard.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tlpnlCrStatus.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlYard;
        private System.Windows.Forms.TableLayoutPanel tlpnlCrStatus;
        private conCrane conCrane1;
        private conCrane conCrane4;
        private conCrane conCrane3;
        private conCrane conCrane2;
        private conCraneStatus conCraneStatus1;
        private conCraneStatus conCraneStatus4;
        private conCraneStatus conCraneStatus3;
        private conCraneStatus conCraneStatus2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnShowCrane;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnShowXY;
        private System.Windows.Forms.Button btnSeekCoil;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label labBayNO;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtX;
    }
}
