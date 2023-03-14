using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UACSView.View_CraneMonitor
{
    public partial class CraneBackColor : Form
    {
        private Z01_library_Monitor Z01_lm;
        public CraneBackColor()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="form"></param>
        public CraneBackColor(Z01_library_Monitor form) : this()
        {
            Z01_lm = form;
        }
        /// <summary>
        /// 修改行车背景颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Z01_lm.UpdataCrane("1");
        }
        /// <summary>
        /// 恢复行车背景颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Z01_lm.OutCrane("1");
        }

    }
}
