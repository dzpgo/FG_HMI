namespace UACSControls
{
    partial class conCrane
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
            this.避让ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登车ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登机请求ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登机1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登机2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.矫正高度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改卸下位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开启ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCrane.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCrane
            // 
            this.panelCrane.BackColor = System.Drawing.Color.Transparent;
            this.panelCrane.BackgroundImage = global::UACSControls.Resource1.行车_Run;
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
            this.panelCrab.BackColor = System.Drawing.SystemColors.Control;
            this.panelCrab.BackgroundImage = global::UACSControls.Resource1.imgCarCoil;
            this.panelCrab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelCrab.Location = new System.Drawing.Point(0, 194);
            this.panelCrab.Name = "panelCrab";
            this.panelCrab.Size = new System.Drawing.Size(47, 27);
            this.panelCrab.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStrip_YardToTard,
            this.ToolStrip_DelCraneOrder,
            this.设置避让ToolStripMenuItem,
            this.登车ToolStripMenuItem,
            this.登机请求ToolStripMenuItem,
            this.矫正高度ToolStripMenuItem,
            this.修改卸下位置ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(175, 186);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // ToolStrip_YardToTard
            // 
            this.ToolStrip_YardToTard.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.ToolStrip_YardToTard.Name = "ToolStrip_YardToTard";
            this.ToolStrip_YardToTard.Size = new System.Drawing.Size(174, 26);
            this.ToolStrip_YardToTard.Text = "归堆指令";
            this.ToolStrip_YardToTard.Click += new System.EventHandler(this.ToolStrip_YardToTard_Click);
            // 
            // ToolStrip_DelCraneOrder
            // 
            this.ToolStrip_DelCraneOrder.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.ToolStrip_DelCraneOrder.Name = "ToolStrip_DelCraneOrder";
            this.ToolStrip_DelCraneOrder.Size = new System.Drawing.Size(174, 26);
            this.ToolStrip_DelCraneOrder.Text = "清空指令";
            this.ToolStrip_DelCraneOrder.Click += new System.EventHandler(this.ToolStrip_DelCraneOrder_Click);
            // 
            // 设置避让ToolStripMenuItem
            // 
            this.设置避让ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.避让ToolStripMenuItem,
            this.取消ToolStripMenuItem});
            this.设置避让ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.设置避让ToolStripMenuItem.Name = "设置避让ToolStripMenuItem";
            this.设置避让ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.设置避让ToolStripMenuItem.Text = "设置避让";
            this.设置避让ToolStripMenuItem.Visible = false;
            // 
            // 避让ToolStripMenuItem
            // 
            this.避让ToolStripMenuItem.Name = "避让ToolStripMenuItem";
            this.避让ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.避让ToolStripMenuItem.Text = "确定";
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.取消ToolStripMenuItem.Text = "取消";
            // 
            // 登车ToolStripMenuItem
            // 
            this.登车ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.登车ToolStripMenuItem.Name = "登车ToolStripMenuItem";
            this.登车ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.登车ToolStripMenuItem.Text = "登车";
            this.登车ToolStripMenuItem.Visible = false;
            // 
            // 登机请求ToolStripMenuItem
            // 
            this.登机请求ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登机1ToolStripMenuItem,
            this.登机2ToolStripMenuItem,
            this.关闭ToolStripMenuItem1});
            this.登机请求ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.登机请求ToolStripMenuItem.Name = "登机请求ToolStripMenuItem";
            this.登机请求ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.登机请求ToolStripMenuItem.Text = "登机请求";
            this.登机请求ToolStripMenuItem.Visible = false;
            // 
            // 登机1ToolStripMenuItem
            // 
            this.登机1ToolStripMenuItem.Name = "登机1ToolStripMenuItem";
            this.登机1ToolStripMenuItem.Size = new System.Drawing.Size(121, 26);
            this.登机1ToolStripMenuItem.Text = "登机1";
            this.登机1ToolStripMenuItem.Click += new System.EventHandler(this.登机1ToolStripMenuItem_Click);
            this.登机1ToolStripMenuItem.Visible = false;
            // 
            // 登机2ToolStripMenuItem
            // 
            this.登机2ToolStripMenuItem.Name = "登机2ToolStripMenuItem";
            this.登机2ToolStripMenuItem.Size = new System.Drawing.Size(121, 26);
            this.登机2ToolStripMenuItem.Text = "登机2";
            this.登机2ToolStripMenuItem.Visible = false;
            // 
            // 关闭ToolStripMenuItem1
            // 
            this.关闭ToolStripMenuItem1.Name = "关闭ToolStripMenuItem1";
            this.关闭ToolStripMenuItem1.Size = new System.Drawing.Size(121, 26);
            this.关闭ToolStripMenuItem1.Text = "关闭";
            this.关闭ToolStripMenuItem1.Click += new System.EventHandler(this.关闭ToolStripMenuItem1_Click);
            // 
            // 矫正高度ToolStripMenuItem
            // 
            this.矫正高度ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.矫正高度ToolStripMenuItem.Name = "矫正高度ToolStripMenuItem";
            this.矫正高度ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.矫正高度ToolStripMenuItem.Text = "矫正高度";
            this.矫正高度ToolStripMenuItem.Click += new System.EventHandler(this.矫正高度ToolStripMenuItem_Click);
            // 
            // 修改卸下位置ToolStripMenuItem
            // 
            this.修改卸下位置ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.修改卸下位置ToolStripMenuItem.Name = "修改卸下位置ToolStripMenuItem";
            this.修改卸下位置ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.修改卸下位置ToolStripMenuItem.Text = "修改卸下位置";
            this.修改卸下位置ToolStripMenuItem.Visible = false;
            // 
            // 开启ToolStripMenuItem
            // 
            this.开启ToolStripMenuItem.Name = "开启ToolStripMenuItem";
            this.开启ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.开启ToolStripMenuItem.Text = "开启";
            this.开启ToolStripMenuItem.Click += new System.EventHandler(this.开启ToolStripMenuItem_Click);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // conCrane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelCrane);
            this.Name = "conCrane";
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
        private System.Windows.Forms.ToolStripMenuItem 避让ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登车ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 矫正高度ToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem 高度ToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem 角度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登机请求ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登机1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登机2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem1;
        //private System.Windows.Forms.ToolStripMenuItem 行车排水ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开启ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改卸下位置ToolStripMenuItem;
    }
}
