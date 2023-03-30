namespace UACSControls
{
    partial class conParkingCar
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
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.timer1 = new System.Windows.Forms.Timer();
            this.lb_ShowCarNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_ShowCarNo
            // 
            this.lb_ShowCarNo.BackColor = System.Drawing.Color.Transparent;
            this.lb_ShowCarNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_ShowCarNo.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.lb_ShowCarNo.Location = new System.Drawing.Point(0, 0);
            this.lb_ShowCarNo.Name = "lb_ShowCarNo";
            this.lb_ShowCarNo.Size = new System.Drawing.Size(85, 262);
            this.lb_ShowCarNo.TabIndex = 0;
            this.lb_ShowCarNo.Text = "SG01";
            this.lb_ShowCarNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_ShowCarNo.Click += new System.EventHandler(this.lb_ShowCarNo_Click);
            // 
            // conParkingCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.BackgroundImage = global::UACSControls.Resource1.WeightCarBody;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.lb_ShowCarNo);
            this.DoubleBuffered = true;
            this.Name = "conParkingCar";
            this.Size = new System.Drawing.Size(85, 262);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_ShowCarNo;
    }
}
