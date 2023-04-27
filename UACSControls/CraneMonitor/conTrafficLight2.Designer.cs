namespace UACSControls
{
    partial class conTrafficLight2
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GreenLight = new System.Windows.Forms.ToolStripMenuItem();
            this.RedLight = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AutoSize = false;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GreenLight,
            this.RedLight});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(30, 48);
            // 
            // GreenLight
            // 
            this.GreenLight.BackColor = System.Drawing.Color.Lime;
            this.GreenLight.Name = "GreenLight";
            this.GreenLight.Size = new System.Drawing.Size(180, 22);
            this.GreenLight.Text = "绿灯";
            this.GreenLight.Click += new System.EventHandler(this.GreenLight_Click);
            // 
            // RedLight
            // 
            this.RedLight.BackColor = System.Drawing.Color.Tomato;
            this.RedLight.Name = "RedLight";
            this.RedLight.Size = new System.Drawing.Size(180, 22);
            this.RedLight.Text = "红灯";
            this.RedLight.Click += new System.EventHandler(this.RedLight_Click);
            // 
            // conTrafficLight2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "conTrafficLight2";
            this.Size = new System.Drawing.Size(25, 25);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.conTrafficLight2_MouseClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem GreenLight;
        private System.Windows.Forms.ToolStripMenuItem RedLight;
    }
}
