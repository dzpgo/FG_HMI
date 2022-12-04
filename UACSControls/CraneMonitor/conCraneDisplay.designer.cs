namespace UACSControls
{
    partial class conCraneDisplay
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
            this.panelCrane = new System.Windows.Forms.Panel();
            this.panelCrab = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStrip_YardToTard = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip_DelCraneOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.设置避让ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.确定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCrane.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCrane
            // 
            this.panelCrane.BackColor = System.Drawing.Color.White;
            this.panelCrane.BackgroundImage = global::UACSControls.Resource1.行车_Avoid;
            this.panelCrane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelCrane.Controls.Add(this.panelCrab);
            this.panelCrane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCrane.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelCrane.Location = new System.Drawing.Point(0, 0);
            this.panelCrane.Name = "panelCrane";
            this.panelCrane.Size = new System.Drawing.Size(47, 408);
            this.panelCrane.TabIndex = 3;
            this.panelCrane.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCrane_Paint);
            this.panelCrane.DoubleClick += new System.EventHandler(this.panelCrane_DoubleClick);
            // 
            // panelCrab
            // 
            this.panelCrab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCrab.BackColor = System.Drawing.Color.White;
            this.panelCrab.BackgroundImage = global::UACSControls.Resource1.imgCarNoCoil;
            this.panelCrab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelCrab.Location = new System.Drawing.Point(0, 194);
            this.panelCrab.Name = "panelCrab";
            this.panelCrab.Size = new System.Drawing.Size(47, 27);
            this.panelCrab.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStrip_YardToTard,
            this.ToolStrip_DelCraneOrder,
            this.设置避让ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
            // 
            // ToolStrip_YardToTard
            // 
            this.ToolStrip_YardToTard.Name = "ToolStrip_YardToTard";
            this.ToolStrip_YardToTard.Size = new System.Drawing.Size(152, 22);
            this.ToolStrip_YardToTard.Text = "倒垛任务";
            this.ToolStrip_YardToTard.Click += new System.EventHandler(this.ToolStrip_YardToTard_Click);
            // 
            // ToolStrip_DelCraneOrder
            // 
            this.ToolStrip_DelCraneOrder.Name = "ToolStrip_DelCraneOrder";
            this.ToolStrip_DelCraneOrder.Size = new System.Drawing.Size(152, 22);
            this.ToolStrip_DelCraneOrder.Text = "清空指令";
            this.ToolStrip_DelCraneOrder.Click += new System.EventHandler(this.ToolStrip_DelCraneOrder_Click);
            // 
            // 设置避让ToolStripMenuItem
            // 
            this.设置避让ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.确定ToolStripMenuItem,
            this.取消ToolStripMenuItem});
            this.设置避让ToolStripMenuItem.Name = "设置避让ToolStripMenuItem";
            this.设置避让ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置避让ToolStripMenuItem.Text = "设置避让";
            // 
            // 确定ToolStripMenuItem
            // 
            this.确定ToolStripMenuItem.Name = "确定ToolStripMenuItem";
            this.确定ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.确定ToolStripMenuItem.Text = "确定";
            this.确定ToolStripMenuItem.Click += new System.EventHandler(this.确定ToolStripMenuItem_Click);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.取消ToolStripMenuItem.Text = "取消";
            this.取消ToolStripMenuItem.Click += new System.EventHandler(this.取消ToolStripMenuItem_Click);
            // 
            // conCraneDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelCrane);
            this.Name = "conCraneDisplay";
            this.Size = new System.Drawing.Size(47, 408);
            this.panelCrane.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCrane;
        private System.Windows.Forms.Panel panelCrab;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip_YardToTard;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip_DelCraneOrder;
        private System.Windows.Forms.ToolStripMenuItem 设置避让ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 确定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;


    }
}
