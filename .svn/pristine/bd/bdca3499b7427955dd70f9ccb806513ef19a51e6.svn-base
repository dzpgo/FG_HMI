using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UACSControls
{
    public partial class CoilUnitStatus : UserControl
    {
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
        
        public void InitTagDataProvide(string tagServiceName)
        {
            try
            {
                tagDataProvider.ServiceName = tagServiceName;
            }
            catch (Exception ex)
            {
            }
        }
        public CoilUnitStatus()
        {
            InitializeComponent();
        }

        private string myStatusTagName = "";
        [Description("对应状态的tag名")]
        public string MyStatusTagName
        {
            get { return myStatusTagName; }
            set { myStatusTagName = value; }
        }

        private string[] arrTagAdress;
        public void SetReady()
        {
            try
            {
                List<string> lstAdress = new List<string>();
                lstAdress.Add(myStatusTagName);
                arrTagAdress = lstAdress.ToArray<string>();
            }
            catch (Exception er)
            {

            }
        }

        public void RefreshControl()
        {
            try
            {
                SetReady();
                if (myStatusTagName != null)
                {
                    readTags();
                    get_value_x(myStatusTagName);
                    SetColor(get_value_x(myStatusTagName));
                }
            }
            catch (Exception er)
            {

            }
        }

        private void readTags()
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
            return theValue; ;
        }

        public delegate void delSetColor(long theValue);

        public void SetColor(long theValue)
        {
            if (myStatusTagName.Contains("MC_SAFE_DOOR") || myStatusTagName.Contains("AREA_SAFE"))
            {
                if (theValue == 0)
                {
                    plColor.BackColor = Color.LightGreen;
                }
                else
                {
                    plColor.BackColor = Color.Red;
                }
            }
            else
            {
                if (theValue == 1)
                {
                    plColor.BackColor = Color.LightGreen;
                }
                else
                {
                    plColor.BackColor = Color.Red;
                }
            }
            
        }
    }
}
