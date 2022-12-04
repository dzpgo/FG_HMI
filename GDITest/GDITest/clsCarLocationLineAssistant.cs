using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDITest
{
    public class clsCarLocationLineAssistant
    {
        private bool needToLocateUpLine = false;

        public bool NeedToLocateUpLine
        {
            get { return needToLocateUpLine; }
            set { needToLocateUpLine = value; }
        }

        private bool hasLocatedUpLine = false;

        public bool HasLocatedUpLine
        {
            get { return hasLocatedUpLine; }
            set { hasLocatedUpLine = value; }
        }

        private int xUpLine = 0;

        public int XUpLine
        {
            get { return xUpLine; }
            set { xUpLine = value; }
        }


        private bool needToLocateLowLine = false;

        public bool NeedToLocateLowLine
        {
            get { return needToLocateLowLine; }
            set { needToLocateLowLine = value; }
        }

        private bool hasLocatedLowLine = false;

        public bool HasLocatedLowLine
        {
            get { return hasLocatedLowLine; }
            set { hasLocatedLowLine = value; }
        }

        private int xLowLine = 0;

        public int XLowLine
        {
            get { return xLowLine; }
            set { xLowLine = value; }
        }

        //增加左右边线
        private bool needToLocateLLine = false;

        public bool NeedToLocateLLine
        {
            get { return needToLocateLLine; }
            set { needToLocateLLine = value; }
        }

        private bool hasLocatedLLine = false;

        public bool HasLocatedLLine
        {
            get { return hasLocatedLLine; }
            set { hasLocatedLLine = value; }
        }

        private int xLLine = 0;

        public int XLLine
        {
            get { return xLLine; }
            set { xLLine = value; }
        }


        private bool needToLocateRLine = false;

        public bool NeedToLocateRLine
        {
            get { return needToLocateRLine; }
            set { needToLocateRLine = value; }
        }

        private bool hasLocatedRLine = false;

        public bool HasLocatedRLine
        {
            get { return hasLocatedRLine; }
            set { hasLocatedRLine = value; }
        }

        private int xRLine = 0;

        public int XRLine
        {
            get { return xRLine; }
            set { xRLine = value; }
        }

        private bool hasLocatedCenterLine = false;

        public bool HasLocatedCenterLine
        {
            get { return hasLocatedCenterLine; }
            set { hasLocatedCenterLine = value; }
        }

        private bool needLockCenterLine = false;

        public bool NeedLockCenterLine
        {
            get { return needLockCenterLine; }
            set { needLockCenterLine = value; }
        }

        private int xCenterLine = 0;

        public int XCenterLine
        {
            get { return xCenterLine; }
            set { xCenterLine = value; }
        }

        public void tryCalCenterX()
        {
            try
            {
                if (hasLocatedLowLine == true && hasLocatedUpLine == true && xUpLine > 0 && xLowLine > 0)
                {
                    int carWidth = Math.Abs(xLowLine - xUpLine);
                    int halfCarWidth = carWidth / 2;

                    if (xUpLine < xLowLine)
                    {
                        xCenterLine = xUpLine + halfCarWidth;
                        hasLocatedCenterLine = true;
                    }
                    else
                    {
                        xCenterLine = xLowLine + halfCarWidth;
                        hasLocatedCenterLine = true;
                    }
                }
                else
                {
                    xCenterLine = 0;
                    hasLocatedCenterLine = false;

                }

            }
            catch (Exception ex)
            {
            }
        }

    }
}
