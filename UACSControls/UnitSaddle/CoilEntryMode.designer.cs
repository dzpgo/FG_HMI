namespace UACSControls
{
    partial class CoilEntryMode
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
            this.lblAutoMode = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAutoStart = new System.Windows.Forms.Button();
            this.btnAutoStop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblAutoMode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 115);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblAutoMode
            // 
            this.lblAutoMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAutoMode.AutoSize = true;
            this.lblAutoMode.Font = new System.Drawing.Font("微软雅黑", 17F, System.Drawing.FontStyle.Bold);
            this.lblAutoMode.ForeColor = System.Drawing.Color.Black;
            this.lblAutoMode.Location = new System.Drawing.Point(80, 13);
            this.lblAutoMode.Name = "lblAutoMode";
            this.lblAutoMode.Size = new System.Drawing.Size(60, 31);
            this.lblAutoMode.TabIndex = 0;
            this.lblAutoMode.Text = "模式";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnAutoStart, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAutoStop, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 60);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(214, 52);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnAutoStart
            // 
            this.btnAutoStart.BackColor = System.Drawing.Color.LightSalmon;
            this.btnAutoStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAutoStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoStart.ForeColor = System.Drawing.Color.White;
            this.btnAutoStart.Location = new System.Drawing.Point(3, 3);
            this.btnAutoStart.Name = "btnAutoStart";
            this.btnAutoStart.Size = new System.Drawing.Size(101, 46);
            this.btnAutoStart.TabIndex = 0;
            this.btnAutoStart.Text = "开始";
            this.btnAutoStart.UseVisualStyleBackColor = false;
            this.btnAutoStart.Click += new System.EventHandler(this.btnAutoStart_Click);
            // 
            // btnAutoStop
            // 
            this.btnAutoStop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAutoStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAutoStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoStop.ForeColor = System.Drawing.Color.White;
            this.btnAutoStop.Location = new System.Drawing.Point(110, 3);
            this.btnAutoStop.Name = "btnAutoStop";
            this.btnAutoStop.Size = new System.Drawing.Size(101, 46);
            this.btnAutoStop.TabIndex = 1;
            this.btnAutoStop.Text = "停止";
            this.btnAutoStop.UseVisualStyleBackColor = false;
            this.btnAutoStop.Click += new System.EventHandler(this.btnAutoStop_Click);
            // 
            // CoilEntryMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CoilEntryMode";
            this.Size = new System.Drawing.Size(220, 115);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblAutoMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnAutoStart;
        private System.Windows.Forms.Button btnAutoStop;
    }
}
