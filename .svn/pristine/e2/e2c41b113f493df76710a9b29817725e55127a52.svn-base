using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UACSDAL
{
    public class LogManager
    {
        static object locker = new object();
        /// <summary>
        /// 重要信息写入日志
        /// </summary>
        /// <param name="logs">日志列表，每条日志占一行</param>
        public static void WriteProgramLog(params string[] logs)
        {
            lock (locker)
            {
                string LogAddress = Environment.CurrentDirectory + "\\Log";
                if (!Directory.Exists(LogAddress + "\\HMI_LOG"))
                {
                    Directory.CreateDirectory(LogAddress + "\\HMI_LOG");
                }
                LogAddress = string.Concat(LogAddress, "\\HMI_LOG\\",
                 DateTime.Now.Year, '-', DateTime.Now.Month, '-',
                 DateTime.Now.Day, "_program.log");
                StreamWriter sw = new StreamWriter(LogAddress, true);
                foreach (string log in logs)
                {
                    sw.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString(), log));
                }
                sw.Close();
            }
        }



        private static string hostname = System.Net.Dns.GetHostName();
        System.Net.IPAddress[] _IPList = System.Net.Dns.GetHostAddresses(hostname);


        static string strIPList = "";

        public static string StrIPList
        {
            get
            {
                if (strIPList == "")
                {
                    strIPList = getIPList(hostname);
                }
                return strIPList;
            }
            //set { strIPList = value; }
        }

        private static string getIPList(string computerName)
        {
            string IPList;
            System.Net.IPAddress[] _IPList = System.Net.Dns.GetHostAddresses(computerName);
            string strIPList = "";
            for (int i = 0; i != _IPList.Length; i++)
            {
                if (_IPList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    if (_IPList[i].ToString().Contains("192.168.10"))
                    {
                        strIPList += _IPList[i].ToString();
                        strIPList += "  ";
                    }      
                }
            }
            IPList = strIPList;
            return IPList;
        }

        public static void GenSQL(string key1, string key2, string info, int level, string module,out string error,string userid = null)
        {

            error = null;
            ////获得主机名
            //string HostName = Dns.GetHostName();
            //Console.WriteLine("主机名是：" + HostName);

            ////遍历地址列表，如果电脑有多网卡只能这样遍历显示
            //IPAddress[] iplist = Dns.GetHostAddresses(HostName);
            //for (int i = 0; i < iplist.Length; i++)
            //{
            //    Console.WriteLine("IP地址：" + iplist[i]);
            //}
            string g = strIPList;
            userid = string.Format("{0}-{1}", hostname, StrIPList);

            string sql = "insert into  UACS_HMI_LOG(key1,key2,level,info,module,userid) values (@key1,@key2,@level,@info,@module,@userid)";
            if (string.IsNullOrEmpty(key1))
                sql = sql.Replace("@key1", "NULL");
            else
                sql = sql.Replace("@key1", "'" + key1 + "'");

            if (string.IsNullOrEmpty(key2))
                sql = sql.Replace("@key2", "NULL");
            else
                sql = sql.Replace("@key2", "'" + key2 + "'");

            if (string.IsNullOrEmpty(module))
                sql = sql.Replace("@module", "NULL");
            else
                sql = sql.Replace("@module", "'" + module + "'");

            if (string.IsNullOrEmpty(userid))
                sql = sql.Replace("@userid", "NULL");
            else
                sql = sql.Replace("@userid", "'" + userid + "'");

            if (string.IsNullOrEmpty(info))
                sql = sql.Replace("@info", "NULL");
            else
                sql = sql.Replace("@info", "'" + info + "'");

            sql = sql.Replace("@level", ((int)level).ToString());


            try
            {
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {

                error = er.Message + er.Source;
            }
           
        }
    }
}
