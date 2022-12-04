namespace GDITest
{
    partial class frmView_Horizontal
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.conLine_Y = new GDITest.conLine();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdDisplay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1407, 808);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 10;
            this.pictureBox.TabStop = false;
            // 
            // conLine_Y
            // 
            this.conLine_Y.BackColor = System.Drawing.Color.Red;
            this.conLine_Y.Location = new System.Drawing.Point(698, 329);
            this.conLine_Y.Name = "conLine_Y";
            this.conLine_Y.Size = new System.Drawing.Size(11, 150);
            this.conLine_Y.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cmdDisplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 638);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1407, 170);
            this.panel1.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(258, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(172, 70);
            this.button2.TabIndex = 10;
            this.button2.Text = "1:40";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 70);
            this.button1.TabIndex = 9;
            this.button1.Text = "1:30";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmdDisplay
            // 
            this.cmdDisplay.Location = new System.Drawing.Point(1183, 45);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Size = new System.Drawing.Size(172, 70);
            this.cmdDisplay.TabIndex = 8;
            this.cmdDisplay.Text = "显示";
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // frmView_Horizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 808);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.conLine_Y);
            this.Controls.Add(this.pictureBox);
            this.Name = "frmView_Horizontal";
            this.Text = "frmView_Horizontal";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private conLine conLine_Y;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdDisplay;
    }
}