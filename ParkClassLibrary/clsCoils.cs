using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkClassLibrary
{
    public class clsCoils
    {
        public string MAT_NO = "";
        public string PLAN_NO = "";
        public string ColumnNO = "";
        public string BigareaNo = "";
        public string weight = "";
        public string width = "";
        public string outDIA = "";
        public override string ToString()
        {
            return MAT_NO + "  " + PLAN_NO + "  " + ColumnNO + "  " + weight + "  " + width + "  " + outDIA;
        }
    }
}
