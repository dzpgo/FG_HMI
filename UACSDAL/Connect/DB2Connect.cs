using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UACSDAL
{
    /// <summary>
    /// 平台数据连接
    /// </summary>
    public class DB2Connect
    {
        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;
        /// <summary>
        /// 项目所用数据库
        /// </summary>
        public static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//
                    }
                    catch (System.Exception e)
                    {
                        //throw e;
                    }
                }
                return dbHelper;
            }
        }


        private static Baosight.iSuperframe.Common.IDBHelper db0Helper = null;
        /// <summary>
        /// 平台所用数据库
        /// </summary>
        public static Baosight.iSuperframe.Common.IDBHelper DB0Helper
        {
            get
            {
                if (db0Helper == null)
                {
                    try
                    {
                        db0Helper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");
                    }
                    catch (System.Exception e)
                    {
                        //throw e;
                    }
                }
                return db0Helper;
            }
        }

    }
}
