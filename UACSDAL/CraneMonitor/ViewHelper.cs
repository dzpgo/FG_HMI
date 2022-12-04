using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Baosight.iSuperframe.Common;

namespace UACS
{
    public class ViewHelper
    {
        /// <summary>
        /// 使用sql语句查询出数据库表，然后与DataGridView逐行绑定
        /// 使用方法：
        /// string sql = "select * from LV_CRANE_PLAN";
        /// ViewHelper.GenDataGridViewData(Helper, dataGridView1, sql, isFalseIn);
        /// 其中Helper是：IDBHelper helper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("APP");的属性返回
        /// </summary>
        /// <param name="Helper">数据库访问对象，iPlature提供，</param>
        /// <param name="dataGridView1">需要绑定数据的DataGridView</param>
        /// <param name="sql">查询数据的SQL语句</param>
        /// <param name="isFirstIn">是否第一次调用，需要返回新值，需要加 ref </param>
        public static string GenDataGridViewData(IDBHelper Helper, DataGridView dataGridView1, string sql, bool isNeedSeq)
        {
            if (Helper == null)
            {
                return "Helper未初始化";
            }

            DataTable datatable = new DataTable();
            bool flag = false;
            using (IDataReader odrIn = Helper.ExecuteReader(sql))
            {
                while (odrIn.Read())
                {
                    //如果是第一次调用，需要为DataTable建立Column。
                    if (!flag)
                    {
                        for (int i = 0; i < odrIn.FieldCount; i++)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = odrIn.GetName(i);
                            datatable.Columns.Add(dc);
                        }
                        flag = true;
                    }
                    

                    //逐列赋值
                    DataRow dr = datatable.NewRow();
                    for (int i = 0; i < odrIn.FieldCount; i++)
                    {
                        dr[i] = odrIn[i];
                    }
                    datatable.Rows.Add(dr);
                }//while结束

                //为datagridview赋值
                string str = SetDataGridViewData(dataGridView1, datatable, isNeedSeq);

                
                return str;

            }
        }

        /// <summary>
        /// 给dataGridView1用datatable逐行赋值
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="datatable"></param>
        public static string SetDataGridViewData(DataGridView dataGridView1, DataTable datatable,bool isNeedSeq)
        {
            if (datatable == null)
            {
                return "datatable为NULL";
            }

            if (datatable.Rows.Count > 0)
            {
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    if (isNeedSeq)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = i + 1;
                        for (int j = 0; j < datatable.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i].Cells[j+1].Value = datatable.Rows[i][j].ToString();
                        }
                    }
                    else
                    {
                        for (int j = 0; j < datatable.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = datatable.Rows[i][j].ToString();
                        }
                    }
                   
                }
            }

            return "";
        }

        private static string SetComboBoxData(ComboBox comBox, string columnName, DataTable datatable)
        {
            if (datatable == null)
            {
                return "datatable为NULL";
            }
            comBox.Items.Clear();
            columnName = columnName.ToUpper().Trim();
            bool columnExist = false;

            //查找datatable中是否存在名字为columnName的列
            for (int k = 0; k < datatable.Columns.Count; k++)
            {
                if (datatable.Columns[k].ColumnName.ToUpper().Trim() == columnName)
                {
                    columnExist = true;
                    break;
                }
            }
            if (!columnExist)
            {
                return "datatable中不存在列名：" + columnName;
            }

            List<string> listdata = new List<string>();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                listdata.Add(datatable.Rows[i][columnName].ToString());
               
            }
            IEnumerable<string> dislistdata = listdata.Distinct();
            foreach (string item in dislistdata)
            {
                comBox.Items.Add(item);
            }
            return "";
        }
        /// <summary>
        /// 使用sql语句查询出数据库表，然后与DataGridView逐行绑定，同时还将其中的一列数据绑定到combox用于下拉框
        /// 说明：需要去除combox可能的重复值。
        /// </summary>
        /// <param name="Helper"></param>
        /// <param name="dataGridView1"></param>
        /// <param name="sql"></param>
        /// <param name="isFirstIn"></param>
        /// <param name="combox"></param>
        /// <returns></returns>
        public static string GenDataGridViewData(IDBHelper Helper, DataGridView dataGridView1, string sql, bool isNeedSeq, string columnName, ComboBox combox)
        {
            if (Helper == null)
            {
                return "Helper未初始化";
            }
            bool flag = false;
            DataTable datatable = new DataTable();
            using (IDataReader odrIn = Helper.ExecuteReader(sql))
            {
                while (odrIn.Read())
                {
                    if (!flag)
                    {
                        //如果是第一次调用，需要为DataTable建立Column。
                        for (int i = 0; i < odrIn.FieldCount; i++)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = odrIn.GetName(i);
                            datatable.Columns.Add(dc);

                        }
                        flag = true;
                    }
                    

                    //逐列赋值
                    DataRow dr = datatable.NewRow();
                    for (int i = 0; i < odrIn.FieldCount; i++)
                    {
                        dr[i] = odrIn[i];
                    }
                    datatable.Rows.Add(dr);
                }//while结束

                //为datagridview赋值
                string str = SetDataGridViewData(dataGridView1, datatable, isNeedSeq);
                str = SetComboBoxData(combox, columnName, datatable);
                return str;
            }

        }



        public static string GenTimeSpanSQL(DateTime start, DateTime end, string fieldName)
        {
            DateTime end2 = end.AddDays(1);
            string sql = fieldName + ">'" + start.ToString("yyyyMMdd") + "080000'";
            sql = sql + "and " + fieldName + "<'" + end2.ToString("yyyyMMdd") + "080000'";

            return sql;

        }

        public static string GenTimeSpanSQL(DateTime start,  string fieldName)
        {
            string sql = fieldName + ">'" + start.ToString("yyyyMMdd") + "000000'";
            return sql;

        }

        public static string GenTimeSpanSQL(string shift, DateTime dt, string fieldName)
        {
            string sql = fieldName + ">='" + dt.ToString("yyyyMMdd");


            if (shift == "全部")
            {
                DateTime dt2 = dt.AddDays(1);
                sql = sql + "080000" + " AND " + fieldName + "<='" + dt2.ToString("yyyyMMdd") + "080000'"+  "'";
            }
            else if (shift == "早班")
            {
                sql = sql + "080000' and " + fieldName + "<'" + dt.ToString("yyyyMMdd") + "200000'";
            }
            else if (shift == "晚班")
            {
                DateTime dt2 = dt.AddDays(1);
                sql = fieldName + ">='" + dt.ToString("yyyyMMdd") + "200000' and " + fieldName + "<'" + dt2.ToString("yyyyMMdd") + "080000'";
            }
            else
            {
                return "无效的班组信息";
            }

            return sql;
        }

        public static string GenTimeSpanSQL(string shift, DateTime dt1, DateTime dt2, string fieldName)
        {
            string sql = fieldName + ">='" + dt1.ToString("yyyyMMdd");


            if (shift == "全部")
            {
                DateTime dt3 = dt2.AddDays(1);
                sql = fieldName + ">='" + dt1.ToString("yyyyMMdd") + "080000' and " + fieldName + "<'" + dt3.ToString("yyyyMMdd") + "080000'";
                //sql = sql + "000000" + " AND " + fieldName + "<='" + dt.ToString("yyyyMMdd") + "235959'"+  "'";
            }
            else if (shift == "早班")
            {
                sql = sql + "080000' and " + fieldName + "<'" + dt1.ToString("yyyyMMdd") + "200000'";
            }
            else if (shift == "晚班")
            {
                DateTime dt4 = dt1.AddDays(1);
                sql = fieldName + ">='" + dt1.ToString("yyyyMMdd") + "200000' and " + fieldName + "<'" + dt4.ToString("yyyyMMdd") + "080000'";
            }
            else
            {
                return "无效的班组信息";
            }

            return sql;
        }


        /// <summary>
        /// 用于DataGridView初始化一般属性
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static string DataGridViewInit(DataGridView dataGridView)
        {
            //dataGridView.ReadOnly = true;
            //列标题属性
            dataGridView.AutoGenerateColumns = false;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;//标题背景颜色
            //设置列高
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView.ColumnHeadersHeight = 35;
            //设置标题内容居中显示;  
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //设置行属性
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;  //隐藏行标题
            //禁止用户改变DataGridView1所有行的行高  
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowTemplate.Height = 30;

            dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            return "";
        }
    }
}
