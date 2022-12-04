using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSDAL
{
    public class OffinePackingUrgentStop
    {
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        private CraneStatusInBay craneStatusInBay = new CraneStatusInBay();

        private string BayNo = null;
        public OffinePackingUrgentStop(string bayNo,params string[] craneNo)
        {
            tagDataProvider.ServiceName = "iplature";
            BayNo = bayNo;
            craneStatusInBay.InitTagDataProvide(SaddleBase.tagServiceName);
            for (int i = 0; i < craneNo.Length; i++)
            {
                craneStatusInBay.AddCraneNO(craneNo[i]);
            }
            craneStatusInBay.SetReady();
        }
        // Xmin Xmax Ymin Ymax
        private int[,] Z12BayOffinePackingArea = {{50000,68321,100,14000}};
        //private int[,] Z52BayOffinePackingArea = {{176000,211000,300,7800}};
        //private int[,] Z53BayOffinePackingArea = { {182000,205000,1000,19200}};


        public bool isUrgentStop()
        {
            string error = null;
            craneStatusInBay.getAllPLCStatusInBay(craneStatusInBay.lstCraneNO);

            foreach (string craneNo in craneStatusInBay.lstCraneNO)
            {
                CraneStatusBase craneBase = craneStatusInBay.DicCranePLCStatusBase[craneNo];
                int[,] offinePacking;
                if (BayNo == "A")
                {
                    offinePacking = Z12BayOffinePackingArea;
                }
                //else if (BayNo == "Z52-1")
                //{
                //    offinePacking = Z52BayOffinePackingArea;
                //}
                //else if (BayNo == "Z53-1")
                //{
                //    offinePacking = Z53BayOffinePackingArea;
                //}
                else
                {
                    return false;
                }

                //大于离线包装X最小 小于离线包装X最大
                if (Convert.ToInt32(craneBase.XAct) > offinePacking[0, 0] && Convert.ToInt32(craneBase.XAct) < offinePacking[0, 1])
                {
                    //大于离线包装Y最小 小于离线包装Y最大
                    if (Convert.ToInt32(craneBase.YAct) > offinePacking[0, 2] && Convert.ToInt32(craneBase.YAct) < offinePacking[0, 3])
                    {
                        //MessageBox.Show(craneNo+"行车在"+BayNo+"库离线上方");

                        SendShortCmd(craneNo, 200, out error);
                        LogManager.GenSQL(craneNo, "", "", 5, "行车紧停", out error);
                        if (error != null)
                        {
                            MessageBox.Show(error);
                        }
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="theCraneNO">行车号</param>
        /// <param name="cmdFlag">对应模式切换数值 200</param>
        private void SendShortCmd(string theCraneNO, long cmdFlag,out string error)
        {
            error = null;
            try
            {
                string messageBuffer = string.Empty;

                messageBuffer = theCraneNO + "," + cmdFlag.ToString();

                Baosight.iSuperframe.TagService.DataCollection<object> wirteDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
                wirteDatas[theCraneNO + "_DownLoadShortCommand"] = messageBuffer;
                tagDataProvider.SetData(theCraneNO + "_DownLoadShortCommand", messageBuffer);

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
           
        }

       


        
    }
}
