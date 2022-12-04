namespace UACSPopupForm
{
    partial class FrmHotCoilYard2Yard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvCoilStrategy = new System.Windows.Forms.DataGridView();
            this.NEME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAG = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CRANE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoilStrategy)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvCoilStrategy);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(900, 457);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "------------------热卷区倒垛信息";
            // 
            // dgvCoilStrategy
            // 
            this.dgvCoilStrategy.AllowUserToAddRows = false;
            this.dgvCoilStrategy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCoilStrategy.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCoilStrategy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCoilStrategy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoilStrategy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NEME,
            this.FLAG,
            this.CRANE_NO});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCoilStrategy.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCoilStrategy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCoilStrategy.EnableHeadersVisualStyles = false;
            this.dgvCoilStrategy.Location = new System.Drawing.Point(3, 22);
            this.dgvCoilStrategy.Name = "dgvCoilStrategy";
            this.dgvCoilStrategy.ReadOnly = true;
            this.dgvCoilStrategy.RowTemplate.Height = 23;
            this.dgvCoilStrategy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCoilStrategy.Size = new System.Drawing.Size(894, 432);
            this.dgvCoilStrategy.TabIndex = 0;
            this.dgvCoilStrategy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCoilStrategy_CellContentClick);
            this.dgvCoilStrategy.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvCoilStrategy_Scroll);
            // 
            // NEME
            // 
            this.NEME.DataPropertyName = "NEME";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NEME.DefaultCellStyle = dataGridViewCellStyle5;
            this.NEME.HeaderText = "排号";
            this.NEME.Name = "NEME";
            this.NEME.ReadOnly = true;
            // 
            // FLAG
            // 
            this.FLAG.DataPropertyName = "FLAG";
            this.FLAG.HeaderText = "倒垛开关";
            this.FLAG.Name = "FLAG";
            this.FLAG.ReadOnly = true;
            this.FLAG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FLAG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // CRANE_NO
            // 
            this.CRANE_NO.DataPropertyName = "CRANE_NO";
            this.CRANE_NO.HeaderText = "行车";
            this.CRANE_NO.Name = "CRANE_NO";
            this.CRANE_NO.ReadOnly = true;
            // 
            // FrmHotCoilYard2Yard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(900, 457);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmHotCoilYard2Yard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "热卷区倒垛";
            this.Load += new System.EventHandler(this.FrmHotCoilYard2Yard_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoilStrategy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvCoilStrategy;
        private System.Windows.Forms.DataGridViewTextBoxColumn NEME;
        private System.Windows.Forms.DataGridViewButtonColumn FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRANE_NO;
    }
}