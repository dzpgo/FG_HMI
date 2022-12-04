namespace GDITest
{
    partial class Form2
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
            this.panelGraphic = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdDisPlay = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGraphic
            // 
            this.panelGraphic.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGraphic.Location = new System.Drawing.Point(0, 0);
            this.panelGraphic.Name = "panelGraphic";
            this.panelGraphic.Size = new System.Drawing.Size(1376, 616);
            this.panelGraphic.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdDisPlay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 669);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1376, 157);
            this.panel2.TabIndex = 1;
            // 
            // cmdDisPlay
            // 
            this.cmdDisPlay.Location = new System.Drawing.Point(416, 32);
            this.cmdDisPlay.Name = "cmdDisPlay";
            this.cmdDisPlay.Size = new System.Drawing.Size(204, 65);
            this.cmdDisPlay.TabIndex = 0;
            this.cmdDisPlay.Text = "button1";
            this.cmdDisPlay.UseVisualStyleBackColor = true;
            this.cmdDisPlay.Click += new System.EventHandler(this.cmdDisPlay_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 826);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelGraphic);
            this.Name = "Form2";
            this.Text = "Form2";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGraphic;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdDisPlay;
    }
}