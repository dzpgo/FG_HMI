using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GDITest
{
    public class clsLaserPointsMap
    {


        private string fileName = "";

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string filePath = "";

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }


        List<clsLaserPoint> lstLaserPoints = new List<clsLaserPoint>();

        public List<clsLaserPoint> LaserPoints
        {
            get { return lstLaserPoints; }
        }

        private long x_min = 0;
        public long X_Min
        {
            get { return x_min; }
        }

        private long x_max = 0;
        public long X_Max
        {
            get { return x_max; }
        }

        private long y_min = 0;
        public long Y_Min
        {
            get { return y_min; }
        }

        private long y_max = 0;
        public long Y_Max
        {
            get { return y_max; }
        }

        private long z_min = 0;
        public long Z_Min
        {
            get { return z_min; }
        }

        private long z_max = 0;
        public long Z_Max
        {
            get { return z_max; }
        }

        private int centerParkingX = 0;

        public int CenterParkingX
        {
            get { return centerParkingX; }
            set { centerParkingX = value; }
        }

        private Dictionary<long, long> dicSampleZ = new Dictionary<long, long>();

        public void GetData_Txt()
        {
            try
            {
                StreamReader sr = new StreamReader(this.filePath+this.fileName, Encoding.Default);
                String line;
                bool needtoread = true;

                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line.ToString());
                    if (needtoread == true)
                    {
                        TreatSingleLineData_Txt(line);
                    }
                    //needtoread = !needtoread;
                    //lstLaserPoints.Sort();
                }

                Get_Dic_Sample_Z();
            }
            catch (Exception ex)
            {
            }
        }

        private void Get_Dic_Sample_Z()
        {
            try
            {
                //计算需要取样的Y
                //long intSample_X = this.centerParkingX;
                long intSample_X = x_min+(x_max - x_min) / 2;
                //遍历数据，如果在取样Y附近的数据那么进行处理
                foreach(clsLaserPoint thePoint in this.lstLaserPoints)
                {
                    if (Math.Abs(thePoint.X - intSample_X) <= 200)//find the data near by the center line
                    {
                        long theKey = Convert.ToInt64(thePoint.Y / 100) * 100;
                        if(dicSampleZ.ContainsKey(theKey))
                        {
                            long tempZ=dicSampleZ[theKey];
                            if (tempZ < thePoint.Z)
                            {
                                dicSampleZ[theKey] = thePoint.Z;
                            }

                        }
                        else
                        {
                            dicSampleZ.Add(theKey,thePoint.Z);
                        }
                    }
                }
                //数据比取样字典中的数据大，那么给字典赋值，以保留最大值
            }
            catch (Exception ex)
            {
            }
        }

        private void TreatSingleLineData_Txt(string theLine)
        {
            try
            {
                string[] strDataElements=theLine.Split(' ');
                int intStep = 1;
                clsLaserPoint thePoint = new clsLaserPoint();
                foreach (string theElement in strDataElements)
                {
                    if (intStep == 1)
                    {
                        thePoint = new clsLaserPoint();
                        thePoint.X = Convert.ToInt64(theElement);
                        //Find_Min_Max(thePoint);
                        //lstLaserPoints.Add(thePoint);
                    }
                    else if (intStep == 2)
                    {
                        thePoint.Y = Convert.ToInt64(theElement);
                        //Find_Min_Max(thePoint);
                        //lstLaserPoints.Add(thePoint);
                    }
                    else if (intStep == 3)
                    {
                        thePoint.Z = Convert.ToInt64(theElement);
                        Find_Min_Max(thePoint);
                        lstLaserPoints.Add(thePoint);
                    }
                    intStep++;
                    if (intStep >= 4)
                    {
                        intStep = 1;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Find_Min_Max(clsLaserPoint thePoint)
        {
            try
            {
                if (x_min == 0)
                {
                    x_min = thePoint.X;
                }
                else
                {
                    if (x_min > thePoint.X)
                    {
                        x_min = thePoint.X;
                    }
                }

                if (x_max == 0)
                {
                    x_max = thePoint.X;
                }
                else
                {
                    if (x_max < thePoint.X)
                    {
                        x_max = thePoint.X;
                    }
                }


                if (y_min == 0)
                {
                    y_min = thePoint.Y;
                }
                else
                {
                    if (y_min > thePoint.Y)
                    {
                        y_min = thePoint.Y;
                    }
                }

                if (y_max == 0)
                {
                    y_max = thePoint.Y;
                }
                else
                {
                    if (y_max < thePoint.Y)
                    {
                        y_max = thePoint.Y;
                    }
                }


                if (z_min == 0)
                {
                    z_min = thePoint.Z;
                }
                else
                {
                    if (z_min > thePoint.Z)
                    {
                        z_min = thePoint.Z;
                    }
                }

                if (z_max == 0)
                {
                    z_max = thePoint.Z;
                }
                else
                {
                    if (z_max < thePoint.Z)
                    {
                        z_max = thePoint.Z;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public int GetSampleZ(long Y)
        {
            int Z = 2000;
            try
            {

                long theKey=Convert.ToInt64(Y/100)*100;

                if (dicSampleZ.ContainsKey(theKey))
                {
                    Z = Convert.ToInt32(dicSampleZ[theKey]);
                }
            }
            catch (Exception ex)
            {
            }
            return Z;
        }
        

    }
}
