namespace UACSView
{
    partial class FrmUACSMainMonitor
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
            this.uacsYardMonitor1 = new UACSControls.UACSYardMonitor();
            this.SuspendLayout();
            // 
            // uacsYardMonitor1
            // 
            this.uacsYardMonitor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(254)))));
            this.uacsYardMonitor1.BayNO = "Z21";
            this.uacsYardMonitor1.CraneNOs = new string[] {
        "6_1",
        "6_2",
        "6_3"};
            this.uacsYardMonitor1.CraneNums = 3;
            this.uacsYardMonitor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uacsYardMonitor1.Location = new System.Drawing.Point(0, 0);
            this.uacsYardMonitor1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uacsYardMonitor1.Mode = UACSControls.UACSYardMonitor.HMI_MDDE.UASER;
            this.uacsYardMonitor1.Name = "uacsYardMonitor1";
            this.uacsYardMonitor1.Size = new System.Drawing.Size(1332, 749);
            this.uacsYardMonitor1.StopReflesh = true;
            this.uacsYardMonitor1.TabIndex = 0;
            this.uacsYardMonitor1.UnitNames = new string[] {
        "D108-WC",
        "D208-WC",
        "D173-WR",
        "D173-WR"};
            this.uacsYardMonitor1.UnitNums = 4;
            this.uacsYardMonitor1.XAxisRight = true;
            this.uacsYardMonitor1.YAxisDown = false;
            // 
            // FrmUACSMainMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 749);
            this.Controls.Add(this.uacsYardMonitor1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmUACSMainMonitor";
            this.Text = "UACS主监控";
            this.ResumeLayout(false);

        }

        #endregion

        private UACSControls.UACSYardMonitor uacsYardMonitor1;





    }
}