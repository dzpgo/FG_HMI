using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.TagService.Interface;

namespace UACSDAL
{
    public class UnitSaddleMethod
    {


        //tag provide 对象的定义
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        /// <summary>
        /// tag点值的map
        /// </summary>
        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();


        private string unitNO = "";
        /// <summary>
        /// 机组号
        /// </summary>
        public string UnitNO
        {
            get { return unitNO; }
            set { unitNO = value; }
        }
        //出入口鞍座区分
        private int flagUnitExit = 1;
        /// <summary>
        /// 出入口鞍座区分
        /// </summary>
        public int FlagUnitExit
        {
            get { return flagUnitExit; }
            set { flagUnitExit = value; }
        }


        /// <summary>
        /// 键字为 L2SaddleName  1，2，3，4，5，6.......
        /// </summary>
        private Dictionary<string, UnitSaddleBase> dicSaddles = new Dictionary<string, UnitSaddleBase>();

        public Dictionary<string, UnitSaddleBase> DicSaddles
        {
            get { return dicSaddles; }
            set { dicSaddles = value; }
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public UnitSaddleMethod()
        {

        }
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="unit_no">机组号</param>
        /// <param name="flag_unit_exit">出入口区分</param>
        /// <param name="tagServiceName">tagService名称</param>
        public UnitSaddleMethod(string unit_no, int flag_unit_exit, string tagServiceName)
        {
            unitNO = unit_no;
            flagUnitExit = flag_unit_exit;
            tagDataProvider.ServiceName = tagServiceName;
        }

        /// <summary>
        /// 获取鞍座的所有信息给类赋值
        /// </summary>
        public void ReadDefintion()
        {
            try
            {
                dicSaddles.Clear();
                string sql = null;
                if (unitNO != "" && flagUnitExit == 1 )   // 出口
                {
                    sql = @"SELECT A.*,B.COIL_NO FROM UACS_LINE_SADDLE_DEFINE A LEFT JOIN
                                UACS_LINE_EXIT_L2INFO B ON A.UNIT_NO = B.UNIT_NO AND A.SADDLE_L2NAME = B.SADDLE_L2NAME ";
                    sql += "WHERE A.UNIT_NO = '" + unitNO + "' AND A.FLAG_UNIT_EXIT = '" + flagUnitExit + "' ORDER BY SADDLE_L2NAME ";
                }
                else if (unitNO != "" && flagUnitExit == 0 )   // 入口
                {
                    if (unitNO == "MC1" )
                    {
                       sql =@" SELECT b.*,c.COIL_NO FROM
                               UACS_YARDMAP_STOCK_DEFINE a LEFT JOIN UACS_LINE_SADDLE_DEFINE b
                               ON a.STOCK_NO = b.STOCK_NO LEFT JOIN UACS_LINE_ENTRY_L2INFO c 
                               ON b.UNIT_NO= c.UNIT_NO AND b.SADDLE_L2NAME=c.SADDLE_L2NAME
                         WHERE a.STOCK_NO like '%" + unitNO + "%' ";
                    }
                    else  if (unitNO == "MC2")
                    {
                        sql = @"SELECT b.*, a.MAT_NO COIL_NO FROM
                               UACS_YARDMAP_STOCK_DEFINE a LEFT JOIN UACS_LINE_SADDLE_DEFINE b
                               ON a.STOCK_NO = b.STOCK_NO LEFT JOIN UACS_LINE_ENTRY_L2INFO c 
                               ON b.UNIT_NO= c.UNIT_NO AND b.SADDLE_L2NAME=c.SADDLE_L2NAME
                         WHERE a.STOCK_NO like '%" + unitNO + "%' ";
                    }                    
                    else
                    {
                        sql = @"SELECT A.*,B.COIL_NO FROM UACS_LINE_SADDLE_DEFINE A LEFT JOIN
                                UACS_LINE_ENTRY_L2INFO B ON A.UNIT_NO = B.UNIT_NO AND A.SADDLE_L2NAME = B.SADDLE_L2NAME ";
                        sql += "WHERE A.UNIT_NO = '" + unitNO + "' AND A.FLAG_UNIT_EXIT = '" + flagUnitExit + "' ORDER BY SADDLE_L2NAME ";
                    }
                   
                }
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        UnitSaddleBase theSaddle = new UnitSaddleBase();
                        //鞍座号
                        if (rdr["STOCK_NO"] != System.DBNull.Value)
                            theSaddle.SaddleNo = Convert.ToString(rdr["STOCK_NO"]);
                        else
                            theSaddle.SaddleNo = string.Empty;
                        //机组号
                        if (rdr["UNIT_NO"] != System.DBNull.Value)
                            theSaddle.UnitNo = Convert.ToString(rdr["UNIT_NO"]);
                        else
                            theSaddle.UnitNo = string.Empty;
                        //出入库区分
                        if (rdr["FLAG_UNIT_EXIT"] != System.DBNull.Value)
                            theSaddle.FlagUnitExit = long.Parse(rdr["FLAG_UNIT_EXIT"].ToString());
                        else
                            theSaddle.FlagUnitExit = 0;
                        //鞍座名称
                        if (rdr["SADDLE_L2NAME"] != System.DBNull.Value)
                            theSaddle.SaddleL2Name = Convert.ToString(rdr["SADDLE_L2NAME"]);
                        else
                            theSaddle.SaddleL2Name = string.Empty;
                        //检查数据
                        if (rdr["FLAG_CHECKDATA"] != System.DBNull.Value)
                            theSaddle.FlagChcekData = long.Parse(rdr["FLAG_CHECKDATA"].ToString());
                        else
                            theSaddle.FlagChcekData = 0;
                        //可以生成指令
                        if (rdr["FLAG_CREATEORDER"] != System.DBNull.Value)
                            theSaddle.FlagCreateOrder = long.Parse(rdr["FLAG_CREATEORDER"].ToString());
                        else
                            theSaddle.FlagCreateOrder = 0;
                        //可以自动起卷
                        if (rdr["FLAG_CANAUTOUP"] != System.DBNull.Value)
                            theSaddle.FlagCanAutoUp = long.Parse(rdr["FLAG_CANAUTOUP"].ToString());
                        else
                            theSaddle.FlagCanAutoUp = 0;
                        //可以自动落卷
                        if (rdr["FLAG_CANAUTODOWN"] != System.DBNull.Value)
                            theSaddle.FlagCanAutoDown = long.Parse(rdr["FLAG_CANAUTODOWN"].ToString());
                        else
                            theSaddle.FlagCanAutoDown = 0;
                        //锁定请求信号点地址
                        if (rdr["TAG_LOCK_REQUEST"] != System.DBNull.Value)
                            theSaddle.TagAdd_LockRequest = Convert.ToString(rdr["TAG_LOCK_REQUEST"]) + "_SET";
                        else
                            theSaddle.TagAdd_LockRequest = string.Empty;
                        //锁定反馈信号点地址
                        if (rdr["TAG_ISLOCKED"] != System.DBNull.Value)
                            theSaddle.TagAdd_IsLocked = Convert.ToString(rdr["TAG_ISLOCKED"]);
                        else
                            theSaddle.TagAdd_IsLocked = string.Empty;
                        //鞍座占位信号点地址
                        if (rdr["TAG_ISEMPTY"] != System.DBNull.Value)
                            theSaddle.TagAdd_IsOccupied = Convert.ToString(rdr["TAG_ISEMPTY"]);
                        else
                            theSaddle.TagAdd_IsOccupied = string.Empty;
                        //钢卷号
                        if (rdr["COIL_NO"] != System.DBNull.Value)
                            theSaddle.CoilNO = Convert.ToString(rdr["COIL_NO"]);
                        else
                            theSaddle.CoilNO = string.Empty;

                        dicSaddles[theSaddle.SaddleNo] = theSaddle;
                    }
                }
            }
            catch (Exception er)
            {}
        }

        private string[] arrTagAdress;
        /// <summary>
        /// 存储所有tag点的地址
        /// </summary>
        public void getTagNameList()
        {
            try
            {
                List<string> TagNamelist = new List<string>();
                foreach (UnitSaddleBase theSaddle in dicSaddles.Values)
                {
                    TagNamelist.Add(theSaddle.TagAdd_LockRequest);
                    TagNamelist.Add(theSaddle.TagAdd_IsLocked);
                    TagNamelist.Add(theSaddle.TagAdd_IsOccupied);
                }

                arrTagAdress = TagNamelist.ToArray<string>();
            }
            catch (Exception er)
            {}
        }

       

        /// <summary>
        /// 遍历tag点获取值
        /// </summary>
        public void getTagValues()
        {
            try
            {
                //清空原来的map
                inDatas.Clear();
                //读所有鞍座tag点的值
                tagDataProvider.GetData(arrTagAdress, out inDatas);

                //为每个鞍座的tag点值属性赋值
                foreach (UnitSaddleBase theSaddle in dicSaddles.Values)
                {
                    theSaddle.TagVal_IsLocked = get_value_x(theSaddle.TagAdd_IsLocked);
                    theSaddle.TagVal_IsOccupied = get_value_x(theSaddle.TagAdd_IsOccupied);
                    theSaddle.TagVal_LockRequest = get_value_x(theSaddle.TagAdd_LockRequest);
                }
            }
            catch (Exception er)
            {}
        }

        /// <summary>
        /// 根据tag返回一个值
        /// </summary>
        /// <param name="tagName">tag名称</param>
        /// <returns></returns>
        private long get_value_x(string tagName)
        {
            long theValue = 0;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToInt32(valueObject);
            }
            catch
            {
                valueObject = null;
            }
            return theValue;
        }

        public void GetCoilLabel(string saddleno, System.Windows.Forms.Label lblCoilNO, System.Windows.Forms.Label lblStockNO)
        {
            try
            {
                string NEXT_COILNO = null;
                string stockNo = null;
                string sql = @"SELECT NEXT_COILNO FROM UACS_LINE_NEXTCOIL WHERE UNIT_NO = '" + saddleno + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["NEXT_COILNO"] != DBNull.Value)
                        {
                            NEXT_COILNO = rdr["NEXT_COILNO"].ToString();
                        }
                        else
                            NEXT_COILNO = null;

                    }
                }

                if (NEXT_COILNO != null)
                {
                    lblCoilNO.Text = NEXT_COILNO;
                    string sqlStock = @"SELECT STOCK_NO FROM UACS_YARDMAP_STOCK_DEFINE WHERE MAT_NO = '" + NEXT_COILNO + "'";
                    using (IDataReader rdrStock = DB2Connect.DBHelper.ExecuteReader(sqlStock))
                    {
                        while (rdrStock.Read())
                        {
                            if (rdrStock["STOCK_NO"] != DBNull.Value)
                            {
                                stockNo = rdrStock["STOCK_NO"].ToString();
                            }
                            else
                                stockNo = null;

                        }
                    }
                    if (stockNo != null)
                    {
                        lblStockNO.Text = stockNo;
                    }
                    else
                        lblStockNO.Text = "无库位";
                }
                else
                {
                    lblCoilNO.Text = "没有钢卷";
                    lblStockNO.Text = "无库位";
                }

            }
            catch (Exception er)
            {

                //throw;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
