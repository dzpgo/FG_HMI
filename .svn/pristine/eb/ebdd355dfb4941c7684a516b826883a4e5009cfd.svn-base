using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Common;
using UACSDAL;


namespace UACSPopupForm
{
    public partial class PDAUSserLogon : Form
    {
        public PDAUSserLogon()
        {
            InitializeComponent();
        }
        public bool ret = false;
        public string userName = "未登录";


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "")
            {
                MessageBox.Show("请输入用户名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtPassWord.Text == "")
                {
                    MessageBox.Show("请输入密码", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = @"SELECT USERID, USERNAME, PRIORITY, CREW, SHIFT, PASSWORD FROM UACS_USER_INF";
                    using (IDataReader dr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        while(dr.Read())
                        {
                            if (txtUserID.Text.ToString().Trim() == dr["USERID"].ToString().Trim())
                            {
                                if (txtPassWord.Text.ToString().Trim() == dr["PASSWORD"].ToString().Trim())
                                {
                                    if(dr["PRIORITY"].ToString().Trim() == "10")
                                    {
                                        MessageBox.Show("登录成功！", "登录提示");
                                        //PDAUserManage main = new PDAUserManage();
                                        ret = true;
                                        userName = dr["USERNAME"].ToString().Trim();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("该用户不是管理员！", "登录提示");
                                        txtUserID.Clear();
                                        txtPassWord.Clear();
                                    }
                                    
                                }
                                else
                                {
                                    MessageBox.Show("输入密码有误，请重新输入！", "登录提示");
                                    txtPassWord.Clear();
                                }
                            }
                        }
                        //if (txtUserID.Text.ToString().Trim() == dr["USERID"].ToString().Trim())
                        //{
                        //    if (txtPassWord.Text.ToString().Trim() == dr["PASSWORD"].ToString().Trim())
                        //    {
                        //        MessageBox.Show("登录成功！", "登录提示");
                        //        PDAUserManage main = new PDAUserManage();
                        //        ret = true;
                        //        userName = dr["USERNAME"].ToString().Trim();
                        //        this.Close();
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("输入密码有误，请重新输入！", "登录提示");
                        //        txtPassWord.Clear();
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("输入用户有误，请重新输入！", "登录提示");
                        //    txtPassWord.Clear();
                        //}
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
