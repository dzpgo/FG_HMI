using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDITest
{
    public class clsLaserPoint:IComparable
    {
        public clsLaserPoint()
        {
        }

        public clsLaserPoint(long intX,long intY,long intZ)
        {
            x = intX;
            y = intY;
            z = intZ;
        }

        private long x;

        public long X
        {
            get { return x; }
            set { x = value; }
        }


        private long y;

        public long Y
        {
            get { return y; }
            set { y = value; }
        }


        private long z;

        public long Z
        {
            get { return z; }
            set { z = value; }
        }
    

        public int  CompareTo(object obj)
        {
            return this.X.CompareTo(  ((clsLaserPoint)obj).X ); 
        }




}
}
