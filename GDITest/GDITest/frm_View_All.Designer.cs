namespace GDITest
{
    partial class frm_View_All
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmd_Zoom_Out = new System.Windows.Forms.Button();
            this.cmd_Zoom_IN = new System.Windows.Forms.Button();
            this.panel_X_Y = new System.Windows.Forms.Panel();
            this.txt_Y = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_X = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmd_DisplayImage = new System.Windows.Forms.Button();
            this.cmd_TreatData = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.conLine_Horizontal = new GDITest.conLine();
            this.pictureBox_Horizontal = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.conLine_Vertical = new GDITest.conLine();
            this.pictureBox_Vertical = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel_X_Y.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Horizontal)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Vertical)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1924, 1057);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Controls.Add(this.cmd_Zoom_Out);
            this.panel3.Controls.Add(this.cmd_Zoom_IN);
            this.panel3.Controls.Add(this.panel_X_Y);
            this.panel3.Controls.Add(this.cmd_DisplayImage);
            this.panel3.Controls.Add(this.cmd_TreatData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 707);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1918, 347);
            this.panel3.TabIndex = 2;
            // 
            // cmd_Zoom_Out
            // 
            this.cmd_Zoom_Out.Location = new System.Drawing.Point(1014, 262);
            this.cmd_Zoom_Out.Name = "cmd_Zoom_Out";
            this.cmd_Zoom_Out.Size = new System.Drawing.Size(106, 54);
            this.cmd_Zoom_Out.TabIndex = 4;
            this.cmd_Zoom_Out.Text = "缩小";
            this.cmd_Zoom_Out.UseVisualStyleBackColor = true;
            this.cmd_Zoom_Out.Click += new System.EventHandler(this.cmd_Zoom_Out_Click);
            // 
            // cmd_Zoom_IN
            // 
            this.cmd_Zoom_IN.Location = new System.Drawing.Point(850, 262);
            this.cmd_Zoom_IN.Name = "cmd_Zoom_IN";
            this.cmd_Zoom_IN.Size = new System.Drawing.Size(106, 54);
            this.cmd_Zoom_IN.TabIndex = 3;
            this.cmd_Zoom_IN.Text = "放大";
            this.cmd_Zoom_IN.UseVisualStyleBackColor = true;
            this.cmd_Zoom_IN.Click += new System.EventHandler(this.cmd_Zoom_IN_Click);
            // 
            // panel_X_Y
            // 
            this.panel_X_Y.BackColor = System.Drawing.Color.White;
            this.panel_X_Y.Controls.Add(this.txt_Y);
            this.panel_X_Y.Controls.Add(this.label3);
            this.panel_X_Y.Controls.Add(this.txt_X);
            this.panel_X_Y.Controls.Add(this.label1);
            this.panel_X_Y.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_X_Y.Location = new System.Drawing.Point(0, 0);
            this.panel_X_Y.Name = "panel_X_Y";
            this.panel_X_Y.Size = new System.Drawing.Size(200, 347);
            this.panel_X_Y.TabIndex = 2;
            // 
            // txt_Y
            // 
            this.txt_Y.AutoSize = true;
            this.txt_Y.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Y.Location = new System.Drawing.Point(33, 124);
            this.txt_Y.Name = "txt_Y";
            this.txt_Y.Size = new System.Drawing.Size(102, 28);
            this.txt_Y.TabIndex = 3;
            this.txt_Y.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(33, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            // 
            // txt_X
            // 
            this.txt_X.AutoSize = true;
            this.txt_X.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_X.Location = new System.Drawing.Point(33, 56);
            this.txt_X.Name = "txt_X";
            this.txt_X.Size = new System.Drawing.Size(102, 28);
            this.txt_X.TabIndex = 1;
            this.txt_X.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // cmd_DisplayImage
            // 
            this.cmd_DisplayImage.Location = new System.Drawing.Point(691, 262);
            this.cmd_DisplayImage.Name = "cmd_DisplayImage";
            this.cmd_DisplayImage.Size = new System.Drawing.Size(106, 54);
            this.cmd_DisplayImage.TabIndex = 1;
            this.cmd_DisplayImage.Text = "显示图像";
            this.cmd_DisplayImage.UseVisualStyleBackColor = true;
            this.cmd_DisplayImage.Click += new System.EventHandler(this.cmd_DisplayImage_Click);
            // 
            // cmd_TreatData
            // 
            this.cmd_TreatData.Location = new System.Drawing.Point(533, 262);
            this.cmd_TreatData.Name = "cmd_TreatData";
            this.cmd_TreatData.Size = new System.Drawing.Size(106, 54);
            this.cmd_TreatData.TabIndex = 0;
            this.cmd_TreatData.Text = "数据处理";
            this.cmd_TreatData.UseVisualStyleBackColor = true;
            this.cmd_TreatData.Click += new System.EventHandler(this.cmd_TreatData_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.conLine_Horizontal);
            this.panel2.Controls.Add(this.pictureBox_Horizontal);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1918, 346);
            this.panel2.TabIndex = 1;
            // 
            // conLine_Horizontal
            // 
            this.conLine_Horizontal.BackColor = System.Drawing.Color.Red;
            this.conLine_Horizontal.Location = new System.Drawing.Point(954, 98);
            this.conLine_Horizontal.Name = "conLine_Horizontal";
            this.conLine_Horizontal.Size = new System.Drawing.Size(11, 150);
            this.conLine_Horizontal.TabIndex = 10;
            // 
            // pictureBox_Horizontal
            // 
            this.pictureBox_Horizontal.BackColor = System.Drawing.Color.Black;
            this.pictureBox_Horizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Horizontal.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Horizontal.Name = "pictureBox_Horizontal";
            this.pictureBox_Horizontal.Size = new System.Drawing.Size(1918, 346);
            this.pictureBox_Horizontal.TabIndex = 1;
            this.pictureBox_Horizontal.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.conLine_Vertical);
            this.panel1.Controls.Add(this.pictureBox_Vertical);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1918, 346);
            this.panel1.TabIndex = 0;
            // 
            // conLine_Vertical
            // 
            this.conLine_Vertical.BackColor = System.Drawing.Color.Red;
            this.conLine_Vertical.Location = new System.Drawing.Point(954, 98);
            this.conLine_Vertical.Name = "conLine_Vertical";
            this.conLine_Vertical.Size = new System.Drawing.Size(11, 150);
            this.conLine_Vertical.TabIndex = 9;
            // 
            // pictureBox_Vertical
            // 
            this.pictureBox_Vertical.BackColor = System.Drawing.Color.Black;
            this.pictureBox_Vertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Vertical.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Vertical.Name = "pictureBox_Vertical";
            this.pictureBox_Vertical.Size = new System.Drawing.Size(1918, 346);
            this.pictureBox_Vertical.TabIndex = 0;
            this.pictureBox_Vertical.TabStop = false;
            // 
            // frm_View_All
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1057);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "frm_View_All";
            this.Text = "frm_View_All";
            this.Load += new System.EventHandler(this.frm_View_All_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_View_All_FormClosing);
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel_X_Y.ResumeLayout(false);
            this.panel_X_Y.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Horizontal)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Vertical)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox_Horizontal;
        private System.Windows.Forms.PictureBox pictureBox_Vertical;
        private System.Windows.Forms.Button cmd_DisplayImage;
        private System.Windows.Forms.Button cmd_TreatData;
        private conLine conLine_Horizontal;
        private conLine conLine_Vertical;
        private System.Windows.Forms.Panel panel_X_Y;
        private System.Windows.Forms.Label txt_Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txt_X;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmd_Zoom_Out;
        private System.Windows.Forms.Button cmd_Zoom_IN;
    }
}