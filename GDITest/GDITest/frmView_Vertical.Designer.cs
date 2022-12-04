namespace GDITest
{
    partial class frmView_Vertical
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
            this.cmdDisplay = new System.Windows.Forms.Button();
            this.panelGraphic_Vertical = new System.Windows.Forms.Panel();
            this.conLine_Y = new GDITest.conLine();
            this.panelGraphic_Vertical.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdDisplay
            // 
            this.cmdDisplay.Location = new System.Drawing.Point(732, 680);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Size = new System.Drawing.Size(172, 70);
            this.cmdDisplay.TabIndex = 2;
            this.cmdDisplay.Text = "显示";
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // panelGraphic_Vertical
            // 
            this.panelGraphic_Vertical.Controls.Add(this.conLine_Y);
            this.panelGraphic_Vertical.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGraphic_Vertical.Location = new System.Drawing.Point(0, 0);
            this.panelGraphic_Vertical.Name = "panelGraphic_Vertical";
            this.panelGraphic_Vertical.Size = new System.Drawing.Size(1192, 616);
            this.panelGraphic_Vertical.TabIndex = 3;
            this.panelGraphic_Vertical.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelGraphic_Vertical_MouseMove);
            // 
            // conLine_Y
            // 
            this.conLine_Y.BackColor = System.Drawing.Color.Red;
            this.conLine_Y.Location = new System.Drawing.Point(387, 175);
            this.conLine_Y.Name = "conLine_Y";
            this.conLine_Y.Size = new System.Drawing.Size(11, 150);
            this.conLine_Y.TabIndex = 0;
            this.conLine_Y.Visible = false;
            // 
            // frmView_Vertical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 762);
            this.Controls.Add(this.panelGraphic_Vertical);
            this.Controls.Add(this.cmdDisplay);
            this.Name = "frmView_Vertical";
            this.Text = "frmView_Vertical";
            this.panelGraphic_Vertical.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdDisplay;
        private System.Windows.Forms.Panel panelGraphic_Vertical;
        private conLine conLine_Y;
    }
}