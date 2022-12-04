namespace GDITest
{
    partial class frmView_Vertical_II
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.conLine_Y = new GDITest.conLine();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdDisplay
            // 
            this.cmdDisplay.Location = new System.Drawing.Point(1185, 654);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Size = new System.Drawing.Size(172, 70);
            this.cmdDisplay.TabIndex = 4;
            this.cmdDisplay.Text = "显示";
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 664);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 70);
            this.button1.TabIndex = 6;
            this.button1.Text = "1:30";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, 664);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(172, 70);
            this.button2.TabIndex = 7;
            this.button2.Text = "1:40";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1401, 616);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 9;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove_1);
            // 
            // conLine_Y
            // 
            this.conLine_Y.BackColor = System.Drawing.Color.Red;
            this.conLine_Y.Location = new System.Drawing.Point(387, 175);
            this.conLine_Y.Name = "conLine_Y";
            this.conLine_Y.Size = new System.Drawing.Size(11, 150);
            this.conLine_Y.TabIndex = 8;
            // 
            // frmView_Vertical_II
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1401, 746);
            this.Controls.Add(this.conLine_Y);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdDisplay);
            this.Name = "frmView_Vertical_II";
            this.Text = "frmView_Vertical_II";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdDisplay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private conLine conLine_Y;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}