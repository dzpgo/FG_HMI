using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UACSDAL
{
    /// <summary>
    /// 行车指令数据处理类
    /// </summary>
    public class CraneOrderConfig
    {

        public enum SEQ_TYPE
        {
            /// <summary>
            /// 前置
            /// </summary>
            PRE,
            /// <summary>
            /// 后置
            /// </summary>
            POST
        }

        public enum SORT_TYPE
        {
            /// <summary>
            /// 按顺序排序
            /// </summary>
            INDEX_SORT,
            /// <summary>
            /// 按启用标记排序
            /// </summary>
            ENABLED_SORT
        }

        /// <summary>
        /// 获取行车指令分组的信息
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public DataTable GetCraneOrderExcuteGroupOfData(string craneNo,SORT_TYPE sort_type,out string error)
        {
            DataTable dt = new DataTable();
            error = null;
            try
            {
                string sql = @"SELECT  ID,CRANE_NO, GROUP_ID, INDEX_IN_GROUP, EXCUTE_TIMES_SET, 
                              CASE
                               WHEN FLAG_ENABLED = 0 THEN '关'
                               WHEN FLAG_ENABLED = 1 THEN '开'
                                ELSE '其他'
                               END as  FLAG_ENABLED ,
                    FLAG_CURRENT_INDEX, EXCUTE_TIMES_ACT, DESCRIBE, TASK_NAME FROM CRANE_ORDER_EXCUTE_GROUP where CRANE_NO = '" + craneNo + "' ";
                if (sort_type == SORT_TYPE.INDEX_SORT)
                {
                    sql += " ORDER BY INDEX_IN_GROUP ";
                }
                if (sort_type == SORT_TYPE.ENABLED_SORT)
                {
                    sql += " ORDER BY FLAG_ENABLED ";
                }

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(rdr);
                }

            }
            catch (Exception er)
            {
                error = er.Message + er.Source;
                return dt = null;        
            }
            return dt;
        }

        /// <summary>
        /// 是否分组启用标记
        /// </summary>
        /// <param name="craneNo"></param>
        /// <param name="taskName"></param>
        /// <param name="flag"></param>
        /// <param name="error"></param>
        public void UpCraneOrderEnadled(string craneNo,int id,int flag,out string error)
        {
            error = null;
            try
            {
                string sql = @"update CRANE_ORDER_EXCUTE_GROUP set FLAG_ENABLED = " + flag + " WHERE CRANE_NO = '" + craneNo +  "' AND ID = '" + id + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                error = er.Message + er.Source;   
            }
        }

        /// <summary>
        /// 提升任务优先级
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="index_in_group">组内执行顺序</param>
        /// <param name="id">ID</param>
        /// <param name="groupId">分组号</param>
        /// <param name="error">错误</param>
        public void UpPromotionTask(string craneNo, string index_in_group,int id,int groupId, out string error)
        {
            error = null;
           
            try
            {
                int current = Convert.ToInt32(index_in_group);
                //提升优先级不能提升最高等级
                if (current == 1)
                {
                    return;
                }
                //排在当前前面的顺序号
                int front = current - 1;


                //step1 先改变前面一个的顺序号
                string sql1 = @"update CRANE_ORDER_EXCUTE_GROUP set INDEX_IN_GROUP = " + current + " WHERE CRANE_NO = '" + craneNo + "'  AND  INDEX_IN_GROUP = " + front + " AND GROUP_ID = " + groupId + "";
                DB2Connect.DBHelper.ExecuteNonQuery(sql1);

                //step2 提升当前任务优先级
                string sql2 = @"update CRANE_ORDER_EXCUTE_GROUP set INDEX_IN_GROUP = " + front + " WHERE ID = " + id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql2);

            }
            catch (Exception er)
            {
                error = er.Message + er.Source;   
            }
        }

        /// <summary>
        /// 提升前置后置任务优先级
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="excute_index">顺序号</param>
        /// <param name="id">id</param>
        /// <param name="error">错误</param>
        public void UpSequencePromotionTask(string craneNo, int excute_index, int id, out string error)
        {
            error = null;
            try
            {
                //提升优先级不能提升最高等级
                if (excute_index == 1)
                {
                    return;
                }


                //排在当前前面的顺序号
                int front = excute_index - 1;

                //固定标记
                int flag = 0;
                string sql3 = @"SELECT FIXED FROM CRANE_ORDER_EXCUTE_SEQUENCE WHERE CRANE_NO = '" + craneNo + "'   AND EXCUTE_INDEX = " + front + "";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql3))
                {
                    while (rdr.Read())
                    {
                        if(rdr["FIXED"]!=DBNull.Value)
                        {
                            flag = Convert.ToInt32(rdr["FIXED"]);
                        }
                    }
                }
                if(flag == 1)
                {
                    return;
                }

                //step1 先改变前面一个的顺序号
                string sql1 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set EXCUTE_INDEX = " + excute_index + " WHERE CRANE_NO = '" + craneNo + "'   AND EXCUTE_INDEX = " + front + "";
                DB2Connect.DBHelper.ExecuteNonQuery(sql1);

                //step2 提升当前任务优先级
                string sql2 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set EXCUTE_INDEX = " + front + " WHERE ID = " + id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql2);

            }
            catch (Exception er)
            {              
               error = er.Message + er.Source;   
            }
           
        }


        /// <summary>
        /// 降低任务优先级
        /// </summary>
        /// <param name="craneNo"></param>
        /// <param name="index_in_group"></param>
        /// <param name="id"></param>
        /// <param name="groupId"></param>
        /// <param name="error"></param>
        public void UpLowerTask(string craneNo, string index_in_group, int id, int groupId, out string error)
        {
            error = null;
        }


        /// <summary>
        /// 任务置顶
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="index_in_group">组内执行顺序</param>
        /// <param name="id">ID</param>
        /// <param name="groupId">分组号</param>
        /// <param name="error">错误</param>
        public void UpTaskTop(string craneNo, string index_in_group, int id, int groupId, out string error)
        {
            error = null;
            
            try
            {
                int current = Convert.ToInt32(index_in_group);
                //提升优先级不能提升最高等级
                if (current == 1)
                {
                    return;
                }

                //step1 先改变顺序号为1的顺序号
                string sql1 = @"update CRANE_ORDER_EXCUTE_GROUP set INDEX_IN_GROUP = " + current + " WHERE CRANE_NO = '" + craneNo + "'  AND  INDEX_IN_GROUP = 1 AND GROUP_ID =  " + groupId + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql1);

                //step2 提升当前任务置顶
                string sql2 = @"update CRANE_ORDER_EXCUTE_GROUP set INDEX_IN_GROUP = 1 WHERE ID = " + id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql2);


            }
            catch (Exception er)
            {            
                error = er.Message + er.Source;   
            }
        }

        /// <summary>
        /// 设置指针
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="id">ID</param>
        /// <param name="groupId">组号</param>
        /// <param name="error">错误</param>
        public void SetCurrentPointer(string craneNo,int id,int groupId, out string error)
        {
            error = null;
            try
            {
                //step1 找出已有指针的ID
                int currentIndexId = 0;
                string sql1 = @"SELECT ID FROM CRANE_ORDER_EXCUTE_GROUP WHERE CRANE_NO = '" + craneNo + "' AND  GROUP_ID =  " + groupId + " AND FLAG_CURRENT_INDEX = 1";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql1))
                {
                    while (rdr.Read())
                    {
                        currentIndexId = Convert.ToInt32(rdr["ID"]);
                    }
                }
                if (currentIndexId != 0)
                {
                    //step2 已有指针的ID指针置0 实际执行次数置0
                    string sql2 = @"update CRANE_ORDER_EXCUTE_GROUP set FLAG_CURRENT_INDEX = 0 ,EXCUTE_TIMES_ACT = 0 WHERE ID =  " + currentIndexId + " ";
                    DB2Connect.DBHelper.ExecuteNonQuery(sql2);

                    //step3 设置传进来的指针为1
                    string sql3 = @"update CRANE_ORDER_EXCUTE_GROUP set FLAG_CURRENT_INDEX = 1 WHERE ID =  " + id + " ";
                    DB2Connect.DBHelper.ExecuteNonQuery(sql3);
                }
            }
            catch (Exception er)
            {         
                 error = er.Message + er.Source;   
            }
        }

        /// <summary>
        /// 设置组内的执行次数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        public void SetExcuteTimesCount(int id,int count,out string error)
        {
            error = null;
            try
            {
                string sql = @"update CRANE_ORDER_EXCUTE_GROUP set EXCUTE_TIMES_SET = " + count + " WHERE ID =  " + id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {              
                error = er.Message + er.Source;  
            }
        }


        /// <summary>
        /// 获取任务的前后置指令
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="type">前置后置区别</param>
        /// <param name="error">错误</param>
        /// <returns></returns>
        public DataTable GetCraneOrderExcuteSequenceOfData(string craneNo,SEQ_TYPE type,out string error)
        {
            DataTable dt = new DataTable();
            error = null;
            try
            {
                string sql = @"SELECT ID,CRANE_NO,EXCUTE_INDEX,DESCRIBE,TASK_NAME,
                             CASE
                                 WHEN FLAG_ENABLED = 0 THEN '关'
                                 WHEN FLAG_ENABLED = 1 THEN '开'
                                 ELSE '其他'
                                 END as  FLAG_ENABLED,FIXED FROM 
                             CRANE_ORDER_EXCUTE_SEQUENCE where CRANE_NO = '" + craneNo + "' ";
                if (type == SEQ_TYPE.PRE)
                {
                    sql += " and SEQ_TYPE = 'PRE' ";
                }
                else if (type == SEQ_TYPE.POST)
                {
                    sql += " and SEQ_TYPE = 'POST' ";
                }

                sql += " ORDER BY EXCUTE_INDEX ";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(rdr);
                }
            }
            catch (Exception er)
            {
                error = er.Message + er.Source;
                return dt = null;       
            }
            return dt;
        }

        /// <summary>
        /// 是否启用前置后置启用标记
        /// </summary>
        public void UpCraneOrderSeqTypeEnadled(int id ,int flag,out string error)
        {
            error = null;
            try
            {
                string sql = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = " + flag + " WHERE ID =  " + id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);

            }
            catch (Exception er)
            {
                error = er.Message + er.Source;
            }
        }


        /// <summary>
        /// 删除前置后置的任务
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="seqType">前置后置区别</param>
        /// <param name="taskName">任务名称</param>
        /// <param name="error">错误</param>
        public void DelSequenceTask(string craneNo,SEQ_TYPE seqType,string taskName,out string error)
        {
            error = null;
            //数据是否存在
            bool isHaveData = false;
            int del_Excute_Index = 0;

            string _seqType = string.Empty;
            if (seqType == SEQ_TYPE.PRE)
                _seqType = "PRE";
            else if (seqType == SEQ_TYPE.POST)
                _seqType = "POST";
            else
            {
                error = "选择的SEQ_TYPE错误";
                return;
            }
            try
            {
                //step 1 查询改任务是否还存在
                string sql1 = @"SELECT *  FROM CRANE_ORDER_EXCUTE_SEQUENCE WHERE CRANE_NO = '" + craneNo + "' AND SEQ_TYPE = '" + _seqType + "' AND TASK_NAME = '" + taskName + "' ";

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql1))
                {
                    while (rdr.Read())
                    {
                        isHaveData = true;
                        del_Excute_Index = Convert.ToInt32(rdr["EXCUTE_INDEX"].ToString());
                    }
                }

                if (!isHaveData)
                    return;
                
                //step 2 删除当前的任务
                string sql2 = @"DELETE FROM CRANE_ORDER_EXCUTE_SEQUENCE WHERE CRANE_NO = '" + craneNo + "' AND SEQ_TYPE = '" + _seqType + "' AND TASK_NAME = '" + taskName + "' ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql2);

                //step 3 删除后 将后面的顺序置前
                string sql3 = @"SELECT *  FROM CRANE_ORDER_EXCUTE_SEQUENCE WHERE CRANE_NO = '" + craneNo + "' AND SEQ_TYPE = '" + _seqType + "' and EXCUTE_INDEX > " + del_Excute_Index + " ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql3))
                {
                    while (rdr.Read())
                    {
                        //当前顺序
                        int excute_index = Convert.ToInt32(rdr["EXCUTE_INDEX"].ToString());
                        int id = Convert.ToInt32(rdr["ID"].ToString());

                        int up_excute_index = CheckIndexSequence(excute_index, del_Excute_Index);
                        if (up_excute_index != 0)
                        {
                            //step 4 更新当前的顺序
                            string sql4 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set EXCUTE_INDEX = " + up_excute_index + " WHERE ID = " + id + "";
                            DB2Connect.DBHelper.ExecuteNonQuery(sql4);
                        }
                        else
                        {
                            error = "检查顺序有误";
                            return;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                error = er.Message + er.Source;
            }
        }



        /// <summary>
        /// 检查顺序
        /// </summary>
        /// <param name="index">当前的</param>
        /// <param name="delIndex">删除的</param>
        /// <returns></returns>
        private int CheckIndexSequence(int index,int delIndex)
        {
            if ((index - delIndex) == 1)
            {
                return delIndex;
            }
            else if ((index - delIndex) > 1)
            {
                return delIndex + (index - delIndex) - 1;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 检查任务是否存在
        /// </summary>
        /// <param name="craneNo"></param>
        /// <param name="seqType"></param>
        /// <param name="checkTaskName"></param>
        /// <returns></returns>
        public bool CheckSequenceTaskName(string craneNo, SEQ_TYPE seqType,string checkTaskName,out string error)
        {
            error = null;
            bool isExist = false;
            string _seqType = string.Empty;
            if (seqType == SEQ_TYPE.PRE)
                _seqType = "PRE";
            else if (seqType == SEQ_TYPE.POST)
                _seqType = "POST";
            else
            {
                error = "选择的SEQ_TYPE错误";
                return false;
            }
               

            try
            {
                string sql = @"SELECT * FROM CRANE_ORDER_EXCUTE_SEQUENCE WHERE CRANE_NO = '" + craneNo + "' AND SEQ_TYPE =  '" + _seqType + "' AND TASK_NAME = '" + checkTaskName + "' ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        isExist = true;
                    }
                }
            }
            catch (Exception er)
            {
                error = er.Message + er.Source;
                return false;
            }
            return isExist;
        }


        /// <summary>
        /// 添加前置后置新任务
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="seqType">前置后置区别</param>
        /// <param name="addTaskName">添加的任务名</param>
        /// <param name="describe">描述</param>
        /// <param name="error">错误</param>
        /// <returns></returns>
        public bool AddSequenceTask(string craneNo, SEQ_TYPE seqType, string addTaskName,string describe, out string error)
        {
            error = null;
            string _seqType = string.Empty;
            if (seqType == SEQ_TYPE.PRE)
                _seqType = "PRE";
            else if (seqType == SEQ_TYPE.POST)
                _seqType = "POST";
            else
            {
                error = "选择的SEQ_TYPE错误";
                return false;
            }
            int currentIndex = 0;
            try
            {
                // step 1 -查询当前的任务序列 
                string sql1 = @"select  count(EXCUTE_INDEX) as index from CRANE_ORDER_EXCUTE_SEQUENCE where CRANE_NO = '" + craneNo + "' and SEQ_TYPE = '" + _seqType + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql1))
                {
                    while (rdr.Read())
                    {
                        currentIndex = Convert.ToInt32( rdr["index"].ToString());
                    }
                }

                currentIndex = currentIndex + 1;

                // step 2 -添加新任务
                string sql2 = @" INSERT INTO CRANE_ORDER_EXCUTE_SEQUENCE(CRANE_NO, SEQ_TYPE, EXCUTE_INDEX, TASK_NAME, FLAG_ENABLED, DESCRIBE) VALUES 
                               ('" + craneNo + "','" + _seqType + "'," + currentIndex + ",'" + addTaskName + "',1,'" + describe + "')";

                DB2Connect.DBHelper.ExecuteNonQuery(sql2);
            }
            catch (Exception er)
            {
                error = er.Message + er.Source;
                return false;
            }

            return true;
        }


        /// <summary>
        /// 获取任务详细列表
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="error">错误</param>
        /// <returns></returns>
        public DataTable GetTaskDefineData(string craneNo,out string error)
        {
            DataTable dt = new DataTable();
            error = null;

            try
            {
                string sql = @"SELECT * FROM CRANE_ORDER_TASK_DEFINE WHERE CRANE_NO = '" + craneNo + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(rdr);
                }
            }
            catch (Exception er)
            {
                error = er.Message + er.Source;
                return dt = null;     ;
            }
            return dt;
        }



    }

    
}
