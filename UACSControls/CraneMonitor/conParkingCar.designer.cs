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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lb_ShowCar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_ShowCar
            // 
            this.lb_ShowCar.BackColor = System.Drawing.Color.Transparent;
            this.lb_ShowCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_ShowCar.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.lb_ShowCar.Location = new System.Drawing.Point(0, 0);
            this.lb_ShowCar.Name = "lb_ShowCar";
            this.lb_ShowCar.Size = new System.Drawing.Size(85, 262);
            this.lb_ShowCar.TabIndex = 0;
            this.lb_ShowCar.Text = "SG06";
            this.lb_ShowCar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_ShowCar.Click += new System.EventHandler(this.lb_ShowCar_Click);
            // 
            // conParkingCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.BackgroundImage = global::UACSControls.Resource1.WeightCarBody;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.lb_ShowCar);
            this.DoubleBuffered = true;
            this.Name = "conParkingCar";
            this.Size = new System.Drawing.Size(85, 262);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_ShowCar;
    }
}
