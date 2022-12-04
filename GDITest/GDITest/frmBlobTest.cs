using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GDITest
{
    public partial class frmBlobTest : Form
    {
        public frmBlobTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string strConnection = "Provider=IBMDADB2.DB2COPY1;Data Source=LOCDB;Persist Security Info=True;User ID=administrator;Password=workinglike1dog;Location=127.0.0.1";
                System.Data.OleDb.OleDbConnection cnn = new System.Data.OleDb.OleDbConnection(strConnection);
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;

                Image img = Image.FromFile(@"d:\display.bmp");
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                byte[] myData = ms.ToArray();
                string strSql="Insert into BlocK_Test values(1,?) ";

                cmd.CommandText = strSql;
                cmd.Parameters.Add("@data ", SqlDbType.Image);
                cmd.Parameters[0].Value = myData;

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
