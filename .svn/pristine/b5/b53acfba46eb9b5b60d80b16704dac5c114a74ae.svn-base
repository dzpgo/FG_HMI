using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;
using UACSPopupForm;

namespace UACSView
{
    public partial class FrmPDAUser : FormBase
    {
        public FrmPDAUser()
        {
            InitializeComponent();
        }

        private bool ret = false;
        private DataTable dt = new DataTable();
        private bool hasSetColumn;

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            if (ret == false)
            {
                MessageBox.Show("请先以管理员用户登录!");
                return;
            }
            //TODO:查询
            string sql = @"SELECT USERID, USERNAME, PRIORITY, CREW, SHIFT, PASSWORD FROM UACS_USER_INF";
            if (txtUserID.Text != null && txtUserID.Text.Trim() != string.Empty)
            {
                sql += string.Format(" WHERE USERID LIKE '%{0}%'", txtUserID.Text);
            }
            else if (txtUserName.Text != null && txtUserName.Text.Trim() != string.Empty)
            {
                sql += string.Format(" WHERE USERNAME LIKE '%{0}%'", txtUserName.Text);
            }
            else if (txtUserName.Text != null && txtUserName.Text.Trim() != string.Empty)
            {
                sql += string.Format(" WHERE USERNAME LIKE '%{0}%' AND USERNAME LIKE '%{0}%'", txtUserName.Text);
            }
            else
            {
                sql = @"SELECT USERID, USERNAME, PRIORITY, CREW, SHIFT, PASSWORD FROM UACS_USER_INF";
            }

            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                dt.Clear();
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (!hasSetColumn)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = rdr.GetName(i);
                            dt.Columns.Add(dc);
                        }
                        dr[i] = rdr[i];
                    }
                    hasSetColumn = true;
                    dt.Rows.Add(dr);
                }
            }
            this.dataGridView1.DataSource = dt;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (ret == false)
            {
                MessageBox.Show("请先以管理员用户登录!");
                return;
            }
            try
            {
                List<string> dt = new List<string>();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (!r.IsNewRow && r.Cells["Check1"].Value != null && (bool)r.Cells["check1"].Value)
                    {
                        string id = r.Cells["USERID"].Value.ToString().Trim();
                        string name = r.Cells["User"].Value.ToString().Trim();
                        string priorty = r.Cells["Priorty"].Value.ToString().Trim();
                        string crew = r.Cells["Crew"].Value.ToString().Trim();
                        string shift = r.Cells["Shift"].Value.ToString().Trim();
                        string password = r.Cells["PassWord"].Value.ToString().Trim();
                        if (id != string.Empty && name != string.Empty && priorty != string.Empty && crew != string.Empty && shift != string.Empty && password != string.Empty)
                        {
                            string sql = string.Format("INSERT INTO UACS_USER_INF(USERID, USERNAME, PRIORITY, CREW, SHIFT, PASSWORD) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", id, name, priorty, crew, shift, password);
                            dt.Add(sql);
                        }
                        else
                        {
                            throw new Exception("字段不能为空");
                        }
                    }
                }
                foreach (string sql in dt)
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sql);
                }
                MessageBox.Show(string.Format("成功新增{0}条记录！", dt.Count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (ret == false)
            {
                MessageBox.Show("请先以管理员用户登录!");
                return;
            }
            try
            {
                List<string> dt = new List<string>();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (!r.IsNewRow && r.Cells["Check1"].Value != null && (bool)r.Cells["check1"].Value)
                    {
                        string id = r.Cells["USERID"].Value.ToString().Trim();
                        string name = r.Cells["User"].Value.ToString().Trim();
                        string priorty = r.Cells["Priorty"].Value.ToString().Trim();
                        string crew = r.Cells["Crew"].Value.ToString().Trim();
                        string shift = r.Cells["Shift"].Value.ToString().Trim();
                        string password = r.Cells["PassWord"].Value.ToString().Trim();
                        string sqlSelect = string.Format("SELECT * FROM UACS_USER_INF WHERE USERID = '{0}' AND USERNAME = '{1}' AND PRIORITY = '{2}'", id, name, priorty);
                        IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlSelect);
                        if (!rdr.Read())
                        {
                            MessageBox.Show("修改失败,您所修改的人员信息不存在，请新增该人员信息！");
                            return;
                        }

                        if (id != string.Empty && name != string.Empty && priorty != string.Empty && password != string.Empty)
                        {
                            string sql = string.Format("UPDATE UACS_USER_INF SET USERNAME = '{1}', PRIORITY = '{2}', CREW = '{3}', SHIFT = '{4}', PASSWORD = '{5}'WHERE USERID = '{0}'", id, name, priorty, crew, shift, password);
                            dt.Add(sql);
                        }
                        else
                        {
                            throw new Exception("（人员编号、人员姓名、优先级、密码）字段不能为空");
                        }
                    }
                }
                foreach (string sql in dt)
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sql);
                }
                MessageBox.Show(string.Format("成功修改{0}条记录！", dt.Count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ret == false)
            {
                MessageBox.Show("请先以管理员用户登录!");
                return;
            }
            try
            {
                List<string> dt = new List<string>();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (!r.IsNewRow && r.Cells["Check1"].Value != null && (bool)r.Cells["check1"].Value && r.Cells["USERID"].Value != null && r.Cells["USERID"].Value.ToString().Trim() != string.Empty)
                    {
                        string id = r.Cells["USERID"].Value.ToString();
                        string sql = string.Format("DELETE FROM UACS_USER_INF WHERE USERID = '{0}'", id);
                        dt.Add(sql);
                    }
                }
                foreach (string sql in dt)
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sql);
                }
                MessageBox.Show(string.Format("成功删除{0}条记录！", dt.Count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLogon_Click(object sender, EventArgs e)
        {
            PDAUSserLogon logon = new PDAUSserLogon();
            logon.ShowDialog();
            ret = logon.ret;
            labelUserName.Text = logon.userName.ToString().Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ret = false;
            MessageBox.Show("用户已退出");
            labelUserName.Text = "未登录";
        }

        private void checBox_All_Details_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = checBox_All_Details.Checked;
            }
        }

        private void btnReSet_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dataGridView1.DataSource = dt;
            checBox_All_Details.Checked = false;
        }
    }
}
