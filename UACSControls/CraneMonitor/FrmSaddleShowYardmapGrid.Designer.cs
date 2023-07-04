namespace UACSView.View_CraneMonitor
{
    partial class FrmSaddleShowYardmapGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaddleShowYardmapGrid));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GRID_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AREA_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRID_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRID_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(543, 510);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 49);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "关闭窗体";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.BackgroundImage")));
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btn_Refresh.ForeColor = System.Drawing.Color.White;
            this.btn_Refresh.Location = new System.Drawing.Point(377, 510);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(101, 49);
            this.btn_Refresh.TabIndex = 50;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(211, 510);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(101, 49);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "保存";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GRID_NO,
            this.AREA_NO,
            this.GRID_NAME,
            this.MAT_CNAME,
            this.MAT_CODE,
            this.GRID_STATUS});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(853, 477);
            this.dataGridView1.TabIndex = 51;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // GRID_NO
            // 
            this.GRID_NO.DataPropertyName = "GRID_NO";
            this.GRID_NO.Frozen = true;
            this.GRID_NO.HeaderText = "料格号";
            this.GRID_NO.Name = "GRID_NO";
            this.GRID_NO.ReadOnly = true;
            this.GRID_NO.Width = 150;
            // 
            // AREA_NO
            // 
            this.AREA_NO.DataPropertyName = "AREA_NO";
            this.AREA_NO.HeaderText = "区域号";
            this.AREA_NO.Name = "AREA_NO";
            this.AREA_NO.ReadOnly = true;
            this.AREA_NO.Visible = false;
            this.AREA_NO.Width = 150;
            // 
            // GRID_NAME
            // 
            this.GRID_NAME.DataPropertyName = "GRID_NAME";
            this.GRID_NAME.HeaderText = "料格名";
            this.GRID_NAME.Name = "GRID_NAME";
            this.GRID_NAME.ReadOnly = true;
            this.GRID_NAME.Width = 180;
            // 
            // MAT_CNAME
            // 
            this.MAT_CNAME.DataPropertyName = "MAT_CNAME";
            this.MAT_CNAME.HeaderText = "物料名";
            this.MAT_CNAME.Name = "MAT_CNAME";
            this.MAT_CNAME.ReadOnly = true;
            this.MAT_CNAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MAT_CNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.MAT_CNAME.Width = 180;
            // 
            // MAT_CODE
            // 
            this.MAT_CODE.DataPropertyName = "MAT_CODE";
            this.MAT_CODE.HeaderText = "物料代码";
            this.MAT_CODE.Name = "MAT_CODE";
            this.MAT_CODE.ReadOnly = true;
            this.MAT_CODE.Width = 150;
            // 
            // GRID_STATUS
            // 
            this.GRID_STATUS.DataPropertyName = "GRID_STATUS";
            this.GRID_STATUS.HeaderText = "料格状态";
            this.GRID_STATUS.Name = "GRID_STATUS";
            this.GRID_STATUS.ReadOnly = true;
            this.GRID_STATUS.Width = 150;
            // 
            // FrmSaddleShowYardmapGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(875, 571);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btnConfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmSaddleShowYardmapGrid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改料格物料布局";
            this.Load += new System.EventHandler(this.FrmSaddleShowYardmapGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRID_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn AREA_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRID_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRID_STATUS;
    }
}