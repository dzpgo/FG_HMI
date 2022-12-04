namespace UACSControls
{
    partial class CoilUnitSaddle
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
            this.申请钢卷信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.申请钢卷信息ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            // 
            // 申请钢卷信息ToolStripMenuItem
            // 
            this.申请钢卷信息ToolStripMenuItem.Name = "申请钢卷信息ToolStripMenuItem";
            this.申请钢卷信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.申请钢卷信息ToolStripMenuItem.Text = "申请钢卷信息";
            this.申请钢卷信息ToolStripMenuItem.Click += new System.EventHandler(this.申请钢卷信息ToolStripMenuItem_Click);
            // 
            // CoilUnitSaddle
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "CoilUnitSaddle";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 申请钢卷信息ToolStripMenuItem;


    }
}
