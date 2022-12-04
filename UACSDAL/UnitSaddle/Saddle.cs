using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UACS
{
    /// <summary>
    /// 机组鞍座信息
    /// </summary>
    public class Saddle
    {
        private string unitNo;
        /// <summary>
        ///  鞍座号
        /// </summary>
        public string UnitNo
        {
            get 
            { 
                return unitNo; 
            }
            set 
            { 
                unitNo = value; 
            }
        }

        private string saddleNo;
        /// <summary>
        /// 鞍座号
        /// </summary>
        public string SaddleNo
        {
            get 
            { 
                return saddleNo; 
            }
            set 
            { 
                saddleNo = value; 
            }
        }

        private long flagUnitExit;
        /// <summary>
        /// 出入口区分
        /// </summary>
        public long FlagUnitExit
        {
            get
            {
                return flagUnitExit;
            }
            set
            {
                flagUnitExit = value;
            }
        }

        private string saddleL2Name;
        /// <summary>
        /// 鞍座命名
        /// </summary>
        public string SaddleL2Name
        {
            get 
            { 
                return saddleL2Name; 
            }
            set 
            { 
                saddleL2Name = value; 
            }
        }

        private long flagChcekData;
        /// <summary>
        /// 需要检查数据
        /// </summary>
        public long FlagChcekData
        {
            get 
            { 
                return flagChcekData; 
            }
            set 
            { 
                flagChcekData = value; 
            }
        }

        private long flagCreateOrder;
        /// <summary>
        /// 可以生成指令
        /// </summary>
        public long FlagCreateOrder
        {
            get 
            { 
                return flagCreateOrder; 
            }
            set 
            { 
                flagCreateOrder = value; 
            }
        }

        private long flagCanAutoUp;
        /// <summary>
        /// 可以自动起卷
        /// </summary>
        public long FlagCanAutoUp
        {
            get 
            { 
                return flagCanAutoUp; 
            }
            set 
            { 
                flagCanAutoUp = value; 
            }
        }

        private long flagCanAutoDown;
        /// <summary>
        /// 可以自动落卷
        /// </summary>
        public long FlagCanAutoDown
        {
            get 
            { 
                return flagCanAutoDown; 
            }
            set 
            { 
                flagCanAutoDown = value; 
            }
        }


        private string tagAdd_LockRequest;
        /// <summary>
        /// 锁定请求信号点地址
        /// </summary>
        public string TagAdd_LockRequest
        {
            get 
            {
                return tagAdd_LockRequest; 
            }
            set 
            { 
                tagAdd_LockRequest = value;
            }
        }

        
        private string tagAdd_IsLocked;
        /// <summary>
        /// 锁定反馈信号点地址
        /// </summary>
        public string TagAdd_IsLocked
        {
            get 
            { 
                return tagAdd_IsLocked; 
            }
            set 
            { 
                tagAdd_IsLocked = value; 
            }
        }

        private string tagAdd_IsOccupied;
        /// <summary>
        /// 鞍座占位信号点地址
        /// </summary>
        public string TagAdd_IsOccupied
        {
            get 
            {
                return tagAdd_IsOccupied; 
            }
            set 
            {
                tagAdd_IsOccupied = value; 
            }
        }

        private long tagVal_LockRequest;
        /// <summary>
        /// 锁定请求信号点值
        /// </summary>
        public long TagVal_LockRequest
        {
            get
            {
                return tagVal_LockRequest;
            }
            set
            {
                tagVal_LockRequest = value;
            }
        }

        private long tagVal_IsLocked;
        /// <summary>
        /// 锁定反馈信号点值
        /// </summary>
        public long TagVal_IsLocked
        {
            get
            {
                return tagVal_IsLocked;
            }
            set
            {
                tagVal_IsLocked = value;
            }
        }

        private long tagVal_IsOccupied;
        /// <summary>
        /// 鞍座占位信号点值
        /// </summary>
        public long TagVal_IsOccupied
        {
            get
            {
                return tagVal_IsOccupied;
            }
            set
            {
                tagVal_IsOccupied = value;
            }
        }

        private string coilNO;
        /// <summary>
        /// 钢卷号
        /// </summary>
        public string CoilNO
        {
            get 
            { 
                return coilNO; 
            }
            set 
            { 
                coilNO = value; 
            }
        }
    }
}
