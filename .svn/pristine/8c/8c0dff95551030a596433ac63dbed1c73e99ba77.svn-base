using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UACSDAL
{
    public class UnitSaddleTagRead
    {
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        private Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

        private List<string> lstTagName = new List<string>();


        private string[] arrTagAdress;
        /// <summary>
        /// 初始化tag
        /// </summary>
        /// <param name="_tagServiceName"></param>
        public void InitTagDataProvider(string _tagServiceName)
        {
            tagDataProvider.ServiceName = _tagServiceName; 
        }

        /// <summary>
        /// 添加tag点
        /// </summary>
        /// <param name="tagName"></param>
        public void AddTagName(string tagName)
        {
            lstTagName.Add(tagName);
        }

        /// <summary>
        /// 设置tag点
        /// </summary>
        public void SetReady()
        {
            try
            {
                List<string> lstAdress = new List<string>();
                foreach (string theTagName in lstTagName)
                {
                    lstAdress.Add(theTagName);
                }
               
                arrTagAdress = lstAdress.ToArray<string>();
            }
            catch (Exception er)
            {

            }
        }

        /// <summary>
        /// 读取tag点
        /// </summary>
        public void readTags()
        {
            try
            {
                inDatas.Clear();
                tagDataProvider.GetData(arrTagAdress, out inDatas);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 取tag值
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public long getTagValue(string tagName)
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
            return theValue; ;
        }

        /// <summary>
        /// 设定tag的value
        /// </summary>
        /// <param name="tagName">tag名</param>
        /// <param name="tagValue">tag值</param>
        public void setTagValue(string tagName,int tagValue,out string error)
        {
            error = string.Empty;
            try
            {
                inDatas[tagName] = tagValue;
                tagDataProvider.Write2Level1(inDatas, 1);
            }
            catch (Exception er)
            {

                error = er.Message;
            }
        }           



    }
}
