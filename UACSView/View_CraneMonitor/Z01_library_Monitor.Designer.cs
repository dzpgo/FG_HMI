namespace UACSView.View_CraneMonitor
{
    partial class Z01_library_Monitor
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
            this.components = new System.ComponentModel.Container();
            this.panelZ61Bay = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.Label();
            this.conCrane2_4 = new UACSControls.conCrane();
            this.conCrane2_3 = new UACSControls.conCrane();
            this.conCrane2_2 = new UACSControls.conCrane();
            this.conCrane2_1 = new UACSControls.conCrane();
            this.label4 = new System.Windows.Forms.Label();
            this.timerCrane = new System.Windows.Forms.Timer(this.components);
            this.timerArea = new System.Windows.Forms.Timer(this.components);
            this.timerUnit = new System.Windows.Forms.Timer(this.components);
            this.timerClear = new System.Windows.Forms.Timer(this.components);
            this.timer_ShowXY = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.conCraneStatus2_2 = new UACSControls.conCraneStatus();
            this.conCraneStatus2_1 = new UACSControls.conCraneStatus();
            this.conCraneStatus2_3 = new UACSControls.conCraneStatus();
            this.conCraneStatus2_4 = new UACSControls.conCraneStatus();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelZ53BayS = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer_InitializeLoad = new System.Windows.Forms.Timer(this.components);
            this.panelZ61Bay.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelZ53BayS.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelZ61Bay
            // 
            this.panelZ61Bay.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelZ61Bay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelZ61Bay.Controls.Add(this.panel4);
            this.panelZ61Bay.Controls.Add(this.conCrane2_4);
            this.panelZ61Bay.Controls.Add(this.conCrane2_3);
            this.panelZ61Bay.Controls.Add(this.conCrane2_2);
            this.panelZ61Bay.Controls.Add(this.conCrane2_1);
            this.panelZ61Bay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelZ61Bay.Location = new System.Drawing.Point(0, 0);
            this.panelZ61Bay.Name = "panelZ61Bay";
            this.panelZ61Bay.Size = new System.Drawing.Size(1265, 388);
            this.panelZ61Bay.TabIndex = 6;
            this.panelZ61Bay.Paint += new System.Windows.Forms.PaintEventHandler(this.panelZ11_Z22Bay_Paint);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtY);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtX);
            this.panel4.Location = new System.Drawing.Point(1165, 322);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 66);
            this.panel4.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "X:";
            // 
            // txtY
            // 
            this.txtY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtY.AutoSize = true;
            this.txtY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtY.ForeColor = System.Drawing.Color.White;
            this.txtY.Location = new System.Drawing.Point(36, 37);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(50, 17);
            this.txtY.TabIndex = 15;
            this.txtY.Text = "999999";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y:";
            // 
            // txtX
            // 
            this.txtX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtX.AutoSize = true;
            this.txtX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtX.ForeColor = System.Drawing.Color.White;
            this.txtX.Location = new System.Drawing.Point(36, 5);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(50, 17);
            this.txtX.TabIndex = 14;
            this.txtX.Text = "999999";
            // 
            // conCrane2_4
            // 
            this.conCrane2_4.BackColor = System.Drawing.SystemColors.Control;
            this.conCrane2_4.CraneNO = null;
            this.conCrane2_4.Location = new System.Drawing.Point(1099, 23);
            this.conCrane2_4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCrane2_4.Name = "conCrane2_4";
            this.conCrane2_4.Size = new System.Drawing.Size(47, 408);
            this.conCrane2_4.TabIndex = 4;
            // 
            // conCrane2_3
            // 
            this.conCrane2_3.BackColor = System.Drawing.SystemColors.Control;
            this.conCrane2_3.CraneNO = null;
            this.conCrane2_3.Location = new System.Drawing.Point(904, 21);
            this.conCrane2_3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCrane2_3.Name = "conCrane2_3";
            this.conCrane2_3.Size = new System.Drawing.Size(47, 408);
            this.conCrane2_3.TabIndex = 3;
            // 
            // conCrane2_2
            // 
            this.conCrane2_2.BackColor = System.Drawing.SystemColors.Control;
            this.conCrane2_2.CraneNO = null;
            this.conCrane2_2.Location = new System.Drawing.Point(514, 22);
            this.conCrane2_2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCrane2_2.Name = "conCrane2_2";
            this.conCrane2_2.Size = new System.Drawing.Size(47, 408);
            this.conCrane2_2.TabIndex = 2;
            // 
            // conCrane2_1
            // 
            this.conCrane2_1.BackColor = System.Drawing.SystemColors.Control;
            this.conCrane2_1.CraneNO = null;
            this.conCrane2_1.Location = new System.Drawing.Point(317, 22);
            this.conCrane2_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCrane2_1.Name = "conCrane2_1";
            this.conCrane2_1.Size = new System.Drawing.Size(47, 408);
            this.conCrane2_1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(1025, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(246, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "注意：自动行车不进入红色区域(安全门没关)";
            // 
            // timerCrane
            // 
            this.timerCrane.Interval = 1000;
            this.timerCrane.Tick += new System.EventHandler(this.timerCrane_Tick);
            // 
            // timerArea
            // 
            this.timerArea.Tick += new System.EventHandler(this.timerArea_Tick);
            // 
            // timerUnit
            // 
            this.timerUnit.Interval = 8000;
            this.timerUnit.Tick += new System.EventHandler(this.timerUnit_Tick);
            // 
            // timerClear
            // 
            this.timerClear.Interval = 100000;
            this.timerClear.Tick += new System.EventHandler(this.timerClear_Tick);
            // 
            // timer_ShowXY
            // 
            this.timer_ShowXY.Tick += new System.EventHandler(this.timer_ShowXY_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 231F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1283, 707);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.conCraneStatus2_2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.conCraneStatus2_1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.conCraneStatus2_3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.conCraneStatus2_4, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 479);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1277, 225);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // conCraneStatus2_2
            // 
            this.conCraneStatus2_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus2_2.CraneNO = "";
            this.conCraneStatus2_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus2_2.Location = new System.Drawing.Point(323, 4);
            this.conCraneStatus2_2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCraneStatus2_2.Name = "conCraneStatus2_2";
            this.conCraneStatus2_2.Size = new System.Drawing.Size(311, 217);
            this.conCraneStatus2_2.TabIndex = 3;
            // 
            // conCraneStatus2_1
            // 
            this.conCraneStatus2_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus2_1.CraneNO = "";
            this.conCraneStatus2_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus2_1.Location = new System.Drawing.Point(4, 4);
            this.conCraneStatus2_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCraneStatus2_1.Name = "conCraneStatus2_1";
            this.conCraneStatus2_1.Size = new System.Drawing.Size(311, 217);
            this.conCraneStatus2_1.TabIndex = 0;
            // 
            // conCraneStatus2_3
            // 
            this.conCraneStatus2_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus2_3.CraneNO = "";
            this.conCraneStatus2_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus2_3.Location = new System.Drawing.Point(642, 4);
            this.conCraneStatus2_3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCraneStatus2_3.Name = "conCraneStatus2_3";
            this.conCraneStatus2_3.Size = new System.Drawing.Size(311, 217);
            this.conCraneStatus2_3.TabIndex = 1;
            // 
            // conCraneStatus2_4
            // 
            this.conCraneStatus2_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conCraneStatus2_4.CraneNO = "";
            this.conCraneStatus2_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conCraneStatus2_4.Location = new System.Drawing.Point(961, 4);
            this.conCraneStatus2_4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.conCraneStatus2_4.Name = "conCraneStatus2_4";
            this.conCraneStatus2_4.Size = new System.Drawing.Size(312, 217);
            this.conCraneStatus2_4.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1277, 64);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1277, 64);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(537, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "1号配料间主监控";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.panelZ53BayS);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1277, 400);
            this.panel3.TabIndex = 4;
            // 
            // panelZ53BayS
            // 
            this.panelZ53BayS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelZ53BayS.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelZ53BayS.Controls.Add(this.panelZ61Bay);
            this.panelZ53BayS.Location = new System.Drawing.Point(6, 6);
            this.panelZ53BayS.Name = "panelZ53BayS";
            this.panelZ53BayS.Size = new System.Drawing.Size(1265, 388);
            this.panelZ53BayS.TabIndex = 0;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(0, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            // 
            // timer_InitializeLoad
            // 
            this.timer_InitializeLoad.Tick += new System.EventHandler(this.timer_InitializeLoad_Tick);
            // 
            // Z01_library_Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 707);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Z01_library_Monitor";
            this.Text = "Z12_library_Monitor";
            this.TabActivated += new System.EventHandler(this.MyTabActivated);
            this.TabDeactivated += new System.EventHandler(this.MyTabDeactivated);
            this.panelZ61Bay.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panelZ53BayS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelZ61Bay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timerCrane;
        private System.Windows.Forms.Timer timerArea;
        private System.Windows.Forms.Timer timerUnit;
        private System.Windows.Forms.Timer timerClear;
        private System.Windows.Forms.Timer timer_ShowXY;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelZ53BayS;
        private System.Windows.Forms.Timer timer_InitializeLoad;        
        private UACSControls.conCrane conCrane2_2;
        private UACSControls.conCrane conCrane2_1;
        private UACSControls.conCrane conCrane2_3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private UACSControls.conCrane conCrane2_4;
        private UACSControls.conCraneStatus conCraneStatus2_3;
        private UACSControls.conCraneStatus conCraneStatus2_4;
        private UACSControls.conCraneStatus conCraneStatus2_2;
        private UACSControls.conCraneStatus conCraneStatus2_1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtX;
    }
}