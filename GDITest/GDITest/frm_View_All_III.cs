using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.TagService;
using System.IO;


namespace GDITest
{
    public partial class frm_View_All_III : Baosight.iSuperframe.Forms.FormBase
    {
        public frm_View_All_III()
        {
            InitializeComponent();
        }

        private string fileName = string.Empty;
        private string fileNameVertical = string.Empty;
        private string fileNameHorizontal = string.Empty;
        private string filePathDataFile = "z:";

        private string strParkingNO = string.Empty;
        private string strCarNO = string.Empty;
        private string strTreatmentNO = string.Empty;
        long intMaxCount = 1;
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagProvider_InStock = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        private clsCarLocationLineAssistant carLocationLineAssistant = new clsCarLocationLineAssistant();

        //激光数据的对象
        clsLaserPointsMap LaserPointMap = new clsLaserPointsMap();

        //垂直视角图片对象
        clsBmpFactory_Vertical BmpVertical = new clsBmpFactory_Vertical();

        //水平视角图片对象
        clsBmpFactory_Horizontal BmpHorizontal = new clsBmpFactory_Horizontal();

        //鼠标所在的点对应实际坐标系中的坐标
        int X = 0;
        int Y = 0;
        int Z = 1600;


        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
      
        //窗体load函数
        private void frm_View_All_III_Load(object sender, EventArgs e)
        {
            try
            {
                LaserPointMap.FilePath = this.filePathDataFile;
                //LaserPointMap.FileName = this.fileName;
                //fileNameVertical = fileName + "Vertical";
                //fileNameHorizontal = fileName + "Horizontal";
                //BmpVertical.FileName = fileNameVertical;
                //BmpHorizontal.FileName = fileNameHorizontal;

                //绑定各种事件

                this.comboBox_SCO_ParkingNO.SelectedIndexChanged += new System.EventHandler(this.comboBox_SCO_ParkingNO_SelectedIndexChanged);

                this.cmdReadData.MouseClick += new MouseEventHandler(cmdReadData_Click);

                this.cmd_TreatData.MouseClick += new MouseEventHandler(cmd_TreatData_Click);

                this.cmdCommitData.Click += new System.EventHandler(this.cmdCommitData_Click);

                this.cmd_Zoom_IN.MouseClick += new MouseEventHandler(cmd_Zoom_IN_Click);

                this.cmd_Zoom_Out.MouseClick += new MouseEventHandler(cmd_Zoom_Out_Click);

                this.cmdLockUpLine.Click += new System.EventHandler(this.cmdLockUpLine_Click);

                this.cmdLockLowLine.Click += new System.EventHandler(this.cmdLockLowLine_Click);
                //绑定锁定左右边线事件
                this.cmdLockLLine.Click += new System.EventHandler(this.cmdLockLLine_Click);

                this.cmdLockRLine.Click += new System.EventHandler(this.cmdLockRLine_Click);

                this.cmdLockCenterLine.Click += new System.EventHandler(this.cmdLockCenterLine_Click);


                this.pictureBox_Horizontal.MouseMove += new MouseEventHandler(pictureBox_Horizontal_MouseMove);

                this.pictureBox_Vertical.MouseMove += new MouseEventHandler(pictureBox_Vertical_MouseMove);

            

                this.conLine_Vertical_X.Click += new System.EventHandler(this.conLine_Vertical_X_Click);

                this.conLine_Vertical_Y.Click += new System.EventHandler(this.conLine_Vertical_Y_Click);

                this.pictureBox_Vertical.Click += new System.EventHandler(this.pictureBox_Vertical_Click);

                this.FormClosing+=new FormClosingEventHandler(Form_Closing);
                
                tagProvider_InStock.ServiceName = "iplature";
                tagProvider_InStock.AutoRegist = true;
                TagValues.Clear();


            }
            catch (Exception ex)
            {
            }
        }

        private void comboBox_SCO_ParkingNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
            }
        }

          //读取数据处理按钮按下
        private void cmdReadData_Click(object sender, EventArgs e)
        {
            try
            {
                carLocationLineAssistant = new clsCarLocationLineAssistant();
                displayInfoCarLocationLine();

                BmpVertical = new clsBmpFactory_Vertical();
                BmpHorizontal = new clsBmpFactory_Horizontal();
                LaserPointMap = new clsLaserPointsMap();
                LaserPointMap.FilePath = this.filePathDataFile;

                strParkingNO = comboBox_SCO_ParkingNO.Text.Trim();
                if (strParkingNO == "Z01A2")
                {
                    BmpVertical.CenterParkingX = 96216;
                    LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                }
                else if (strParkingNO == "Z01A1")
                {
                     BmpVertical.CenterParkingX = 91333;
                     LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                }                
                
                //else if (strParkingNO == "0305" || strParkingNO == "0307")
                //{
                //    BmpVertical.CenterParkingX = 137745;
                //    LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                //}
                //else if (strParkingNO == "0306" || strParkingNO == "0308")
                //{
                //    BmpVertical.CenterParkingX = 100095;
                //    LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                //}
                //else if (strParkingNO == "0401" || strParkingNO == "0403")
                //{
                //    BmpVertical.CenterParkingX = 48068;
                //    LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                //}
                //else if (strParkingNO == "0402" || strParkingNO == "0404")
                //{
                //    BmpVertical.CenterParkingX = 53418;
                //    LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                //}
                //else if (strParkingNO == "0405" || strParkingNO == "0407")
                //{
                //    BmpVertical.CenterParkingX = 48189;
                //    LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                //}
                //else if (strParkingNO == "0406" || strParkingNO == "0408")
                //{
                //    BmpVertical.CenterParkingX = 53539;
                //    LaserPointMap.CenterParkingX = BmpVertical.CenterParkingX;
                //}


                dataGridLaserResult.Rows.Clear();
                pictureBox_Vertical.Image = null;
                pictureBox_Horizontal.Image = null;

                clsParkingSpaceUtility parkingSpaceUtility = new clsParkingSpaceUtility();
                strParkingNO = comboBox_SCO_ParkingNO.Text.Trim();
                strTreatmentNO = parkingSpaceUtility.Get_TreatmentNO_OnParkingSpace(strParkingNO);
                //strTreatmentNO = "CP112000179";  //to be deleted


                //string strSN = parkingSpaceUtility.Get_SN_OnParkingSpace(strParkingNO);


                string strHead_Postion = parkingSpaceUtility.Get_Head_Postion_OnParkingSpace(strParkingNO);
                //string strHead_Postion = "N";       //to be deleted


                strCarNO = parkingSpaceUtility.Get_CarNO_OnParkingSpace(strParkingNO);
                //strCarNO = "B123456";      //to be deleted


                string strIsLoaded = parkingSpaceUtility.Get_ISLoaded_OnParkingSpace(strParkingNO);
                //string strIsLoaded = "0";          //to be deleted



                intMaxCount = parkingSpaceUtility.Get_MAXCount_OfCramera(strTreatmentNO);
                //intMaxCount = 1;// to be deleted

                bool FlagConfirmed = parkingSpaceUtility.Get_FlagConfirmed_OfCramera(strTreatmentNO, intMaxCount);
                //bool FlagConfirmed = false;// to be deleted

                bool hasCranePlan = parkingSpaceUtility.Has_CranePlan(strTreatmentNO);
                //bool hasCranePlan = false;// to be deleted

                //if (strParkingNO != string.Empty && strTreatmentNO != string.Empty && strIsLoaded == "0" && intMaxCount > 0 && FlagConfirmed == false)
                //{
                this.fileName = strTreatmentNO;
                //this.fileName = "CP112000179";
                    //;// to be deleted
                    LaserPointMap.FileName = this.fileName + ".txt";
                    fileNameVertical = fileName + "Vertical";
                    fileNameHorizontal = fileName + "Horizontal";
                    BmpVertical.FileName = fileNameVertical;
                    BmpHorizontal.FileName = fileNameHorizontal;

                    //拿到数据
                    LaserPointMap.GetData_Txt();

                    if (LaserPointMap.LaserPoints.Count == 0)
                    {
                        MessageBox.Show("没有读取到激光数据");
                    }
                    else
                    {
                        //把激光数据赋值给图形处理数据
                        BmpVertical.LaserPointMap = this.LaserPointMap;
                        BmpVertical.Scale = 10;




                        BmpHorizontal.LaserPointMap = this.LaserPointMap;
                        BmpHorizontal.Scale = 10;

                        System.Threading.Thread threadVertical = new System.Threading.Thread(Init_Data_Vertical);
                        threadVertical.Start();

                        System.Threading.Thread threadHorizontal = new System.Threading.Thread(Init_Data_Horizontal);
                        threadHorizontal.Start();

                        //Init_Data_Vertical();
                        //Init_Data_Horizontal();

                        //MessageBox.Show("数据读取成功");

                        needToShowFirstTime = true;
                        timer.Interval = 1000;
                        timer.Enabled = true;
                        timer.Start();


                    }



                //}
                //else
                //{
                //    MessageBox.Show("选择正确的车位 \n strParkingNO=" + strParkingNO + "\n strTreatmentNO=" + strTreatmentNO + "\n strIsLoaded=" + strIsLoaded + "\n intMaxCount=" + intMaxCount + "\n FlagConfirmed=" + FlagConfirmed);
                //}
            }
            catch (Exception ex)
            {
            }
            finally
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            }
        }

        //图像展示按钮按下
        private void cmd_TreatData_Click(object sender, EventArgs e)
        {
            try
            {


                BmpVertical.Display_Bmp(pictureBox_Vertical);


                BmpHorizontal.Display_Bmp(pictureBox_Horizontal);

                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);


                txt_Scale.Text = BmpVertical.Scale.ToString();
                txt_XMax.Text = LaserPointMap.X_Max.ToString();
                txt_XMin.Text = LaserPointMap.X_Min.ToString();
                txt_YMax.Text = LaserPointMap.Y_Max.ToString();
                txt_YMin.Text = LaserPointMap.Y_Min.ToString();
                txt_ZMax.Text = LaserPointMap.Z_Max.ToString();
                txt_ZMin.Text = LaserPointMap.Z_Min.ToString();
 

            }
            catch (Exception ex)
            {
            }
        }

        private void Init_Data_Vertical()
        {
            try
            {
               
                //让图形处理对象产生1：1的原图
                BmpVertical.Create_Original_Bmp();

                //让图形处理对象产生各个比例的图
                BmpVertical.Create_DifferentScale_Bmps();

                Init_XY_TopLeft_Vertical();

                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);


                ////显示垂直视角的图片
                //BmpVertical.Display_Bmp(pictureBox_Vertical);
            }
            catch (Exception ex)
            {
            }
        }
        private void Init_Data_Horizontal()
        {
            try
            {
                BmpHorizontal.Create_Original_Bmp();

                BmpHorizontal.Create_DifferentScale_Bmps();

                Init_XY_TopLeft_Vertical();

                ////显示水平视角的图片
                //BmpHorizontal.Display_Bmp(pictureBox_Horizontal);
            }
            catch (Exception ex)
            {
            }
        }

        //数据显示按钮按下
        private void cmd_DisplayImage_Click(object sender, EventArgs e)
        {
            try
            {

                BmpVertical.Display_Bmp(pictureBox_Vertical);


                BmpHorizontal.Display_Bmp(pictureBox_Horizontal);

                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);


                txt_Scale.Text = BmpVertical.Scale.ToString();
                txt_XMax.Text = LaserPointMap.X_Max.ToString();
                txt_XMin.Text = LaserPointMap.X_Min.ToString();
                txt_YMax.Text = LaserPointMap.Y_Max.ToString();
                txt_YMin.Text = LaserPointMap.Y_Min.ToString();
                txt_ZMax.Text = LaserPointMap.Z_Max.ToString();
                txt_ZMin.Text = LaserPointMap.Z_Min.ToString();

            }
            catch (Exception ex)
            {
            }
        }


        private void cmdLockUpLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.carLocationLineAssistant.NeedToLocateUpLine = !this.carLocationLineAssistant.NeedToLocateUpLine;
                if (this.carLocationLineAssistant.NeedToLocateUpLine == true)
                {
                    this.carLocationLineAssistant.NeedToLocateLowLine = false;
                }
                displayInfoCarLocationLine();
            }
            catch (Exception ex)
            {
            }
        }


        private void cmdLockLowLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.carLocationLineAssistant.NeedToLocateLowLine = !this.carLocationLineAssistant.NeedToLocateLowLine;
                if (this.carLocationLineAssistant.NeedToLocateLowLine == true)
                {
                    this.carLocationLineAssistant.NeedToLocateUpLine = false;
                }
                displayInfoCarLocationLine();
            }
            catch (Exception ex)
            {
            }
        }

        //锁定左边线
        private void cmdLockLLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.carLocationLineAssistant.NeedToLocateLLine = !this.carLocationLineAssistant.NeedToLocateLLine;
                if (this.carLocationLineAssistant.NeedToLocateLLine == true)
                {
                    this.carLocationLineAssistant.NeedToLocateRLine = false;
                }
                displayInfoCarLocationLine();
            }
            catch (Exception ex)
            {
            }
        }

        //锁定右边线
        private void cmdLockRLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.carLocationLineAssistant.NeedToLocateRLine = !this.carLocationLineAssistant.NeedToLocateRLine;
                if (this.carLocationLineAssistant.NeedToLocateRLine == true)
                {
                    this.carLocationLineAssistant.NeedToLocateLLine = false;
                }
                displayInfoCarLocationLine();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdLockCenterLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.carLocationLineAssistant.NeedLockCenterLine = !this.carLocationLineAssistant.NeedLockCenterLine;
                displayInfoCarLocationLine();
            }
            catch (Exception ex)
            {
            }
        }

        private void displayInfoCarLocationLine()
        {
            try
            {
                
                if (carLocationLineAssistant.NeedToLocateUpLine == true)
                {
                    cmdLockUpLine.BackColor = Color.LightGreen;
                }
                else
                {
                    cmdLockUpLine.BackColor = Color.LightGray;
                }
                if (carLocationLineAssistant.NeedToLocateLowLine == true)
                {
                    cmdLockLowLine.BackColor = Color.LightGreen;
                }
                else
                {
                    cmdLockLowLine.BackColor = Color.LightGray;
                }
                //左右边线控件使能颜色
                if (carLocationLineAssistant.NeedToLocateLLine == true)
                {
                    cmdLockLLine.BackColor = Color.LightGreen;
                }
                else
                {
                    cmdLockLLine.BackColor = Color.LightGray;
                }
                if (carLocationLineAssistant.NeedToLocateRLine == true)
                {
                    cmdLockRLine.BackColor = Color.LightGreen;
                }
                else
                {
                    cmdLockRLine.BackColor = Color.LightGray;
                }
                if (carLocationLineAssistant.NeedLockCenterLine == true)
                {
                    cmdLockCenterLine.BackColor = Color.LightGreen;
                }
                else
                {
                    cmdLockCenterLine.BackColor = Color.LightGray;
                }
                if (carLocationLineAssistant.HasLocatedUpLine == true)
                {
                    txtXUpLine.Text = carLocationLineAssistant.XUpLine.ToString();
                }
                else
                {
                    txtXUpLine.Text = string.Empty;
                }
                if (carLocationLineAssistant.HasLocatedLowLine == true)
                {
                    txtXLowLine.Text = carLocationLineAssistant.XLowLine.ToString();
                }
                else
                {
                    txtXLowLine.Text = string.Empty;
                }
                //左右边线坐标
                if (carLocationLineAssistant.HasLocatedLLine == true)
                {
                    txtXLLine.Text = carLocationLineAssistant.XLLine.ToString();
                }
                else
                {
                    txtXLLine.Text = string.Empty;
                }
                if (carLocationLineAssistant.HasLocatedRLine == true)
                {
                    txtXRLine.Text = carLocationLineAssistant.XRLine.ToString();
                }
                else
                {
                    txtXRLine.Text = string.Empty;
                }
                if (carLocationLineAssistant.HasLocatedCenterLine == true)
                {
                    txtCenterLineCalulated.Text = carLocationLineAssistant.XCenterLine.ToString();
                }
                else
                {
                    txtCenterLineCalulated.Text = string.Empty;
                }
                //增加边框大小判断
                if (txtXUpLine.Text != string.Empty && txtXLowLine.Text != string.Empty && carLocationLineAssistant.XUpLine > carLocationLineAssistant.XLowLine
                    && carLocationLineAssistant.NeedToLocateUpLine == false && carLocationLineAssistant.NeedToLocateLowLine == false)
                {
                    MessageBox.Show("上边线比下边线大，请重新定上下边线");
                }
                if (txtXLLine.Text != string.Empty && txtXRLine.Text != string.Empty && carLocationLineAssistant.XLLine > carLocationLineAssistant.XRLine
                    && carLocationLineAssistant.NeedToLocateLLine == false && carLocationLineAssistant.NeedToLocateRLine == false)
                {
                    MessageBox.Show("左边线比右边线大，请重新定左右边线");
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        //鼠标移动
        void pictureBox_Vertical_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ModifyPos_Line_XY(sender, e, true);
            }
            catch (Exception ex)
            {
            }
        }

        //鼠标移动
        void pictureBox_Horizontal_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ModifyPos_Line_XY(sender,e, false);
            }
            catch (Exception ex)
            {
            }
        }

        private void ModifyPos_Line_XY(object sender, MouseEventArgs e, bool needToMove_Line_Vertical_X)
        {
            try
            {
                if (e.X > 0 && e.Y > 0 && e.X < this.pictureBox_Vertical.Width && e.Y < this.pictureBox_Vertical.Height)
                {
                    if (this.conLine_Vertical_Y.Width != 1) { this.conLine_Vertical_Y.Width = 1; }
                    if (this.conLine_Vertical_Y.Height != this.pictureBox_Vertical.Height)
                    {
                        this.conLine_Vertical_Y.Height = this.pictureBox_Vertical.Height;
                    }
                    if (this.conLine_Vertical_Y.Top != 0) { this.conLine_Vertical_Y.Top = 0; }
                    this.conLine_Vertical_Y.Left = e.X;

                    if (needToMove_Line_Vertical_X == true)
                    {
                        if (this.conLine_Vertical_X.Width != this.pictureBox_Vertical.Width)
                        {
                            this.conLine_Vertical_X.Width = this.pictureBox_Vertical.Width;
                        }
                        if (this.conLine_Vertical_X.Height != 1) { this.conLine_Vertical_X.Height = 1; }
                        this.conLine_Vertical_X.Top = e.Y;
                        if (this.conLine_Vertical_X.Left != 0) { this.conLine_Vertical_X.Left = 0; }
                    }


                }

                if (e.X > 0 && e.Y > 0 && e.X < this.pictureBox_Horizontal.Width && e.Y < this.pictureBox_Horizontal.Height)
                {
                    if (this.conLine_Horizontal_Y.Width != 1) { this.conLine_Horizontal_Y.Width = 1; }
                    if (this.conLine_Horizontal_Y.Top != 0) { this.conLine_Horizontal_Y.Top = 0; }
                    if (this.conLine_Horizontal_Y.Height != this.pictureBox_Horizontal.Height)
                    {
                        this.conLine_Horizontal_Y.Height = this.pictureBox_Horizontal.Height;
                    }
                    this.conLine_Horizontal_Y.Left = e.X;

                }
                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);
            }
            catch (Exception ex)
            {
            }

        }

        //按照输入的XY的相对坐标 计算XY的决定坐标
        private void Cal_X_Y_Z_MousePoint(ref int X, ref int Y, ref int Z)
        {
            try
            {
                //X=图像左上角X + 相对距离*比例
                X = this.BmpVertical.X_LeftTop + conLine_Vertical_X.Top * this.BmpVertical.Scale;
                //Y=图像左上角Y + 相对距离*比例
                Y = Convert.ToInt32(this.BmpVertical.Y_LEftTop + conLine_Vertical_Y.Left * this.BmpVertical.Scale);
                Z = this.LaserPointMap.GetSampleZ(Y);
                //Z = 1600;


                txt_X.Text = X.ToString();
                txt_Y.Text = Y.ToString();
                txt_Z.Text = Z.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void Init_XY_TopLeft_Vertical()
        {
            try
            {
                //图片左上角的X Y 赋予初始值
                BmpVertical.X_LeftTop = Convert.ToInt32(this.LaserPointMap.X_Min + (this.LaserPointMap.X_Max - this.LaserPointMap.X_Min) / 2) - Convert.ToInt32(pictureBox_Vertical.Height / 2 * this.BmpVertical.Scale); ; ;
                //BmpVertical.X_LeftTop = Convert.ToInt32(BmpVertical.CenterParkingX) - Convert.ToInt32(pictureBox_Vertical.Height / 2 * this.BmpVertical.Scale); ;
                BmpVertical.Y_LEftTop = Convert.ToInt32(LaserPointMap.Y_Min);

                BmpHorizontal.Y_LEftTop = Convert.ToInt32(LaserPointMap.Y_Min);
                BmpHorizontal.Z_LeftTop = BmpHorizontal.ZMax / 2 + this.conLine_Horizontal_Y.Height / 2 * BmpHorizontal.Scale;

            }
            catch (Exception ex)
            {
            }
        }
        private void Cal_XY_TopLeft_Vertical(int X, int Y)
        {
            try
            {
                //计算图片左上角的实际坐标
                //X=X中轴-图框一半高度*比例
                //BmpVertical.X_LeftTop = Convert.ToInt32(this.LaserPointMap.X_Min
                //                        + (this.LaserPointMap.X_Max - this.LaserPointMap.Y_Min) / 2)
                //                        - Convert.ToInt32(pictureBox_Vertical.Height / 2 * BmpVertical.Scale);
                BmpVertical.X_LeftTop = Convert.ToInt32(BmpVertical.CenterParkingX) - Convert.ToInt32(pictureBox_Vertical.Height / 2 * this.BmpVertical.Scale); ;
                //Y=鼠标所在点实际Y-图框左侧到指示线距离*比例
                BmpVertical.Y_LEftTop = Y - conLine_Vertical_Y.Left * BmpVertical.Scale;

                BmpHorizontal.Y_LEftTop = Y - conLine_Vertical_Y.Left * BmpVertical.Scale;

                BmpHorizontal.Z_LeftTop = BmpHorizontal.ZMax / 2 + this.conLine_Horizontal_Y.Height / 2 * BmpHorizontal.Scale;

            }
            catch (Exception ex)
            {
            }
        }

        //放大
        private void cmd_Zoom_IN_Click(object sender, EventArgs e)
        {
            try
            {

                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);

                BmpVertical.AdjustScale_ZoomIn();

                BmpHorizontal.AdjustScale_ZoomIn();

                Cal_XY_TopLeft_Vertical(X, Y);

                BmpVertical.Display_Bmp(pictureBox_Vertical);

                BmpHorizontal.Display_Bmp(pictureBox_Horizontal);

                //因为比例变更过，所以从新计算鼠标点对应的XYZ
                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);


                txt_Scale.Text = BmpVertical.Scale.ToString();
                txt_XMax.Text = LaserPointMap.X_Max.ToString();
                txt_XMin.Text = LaserPointMap.X_Min.ToString();
                txt_YMax.Text = LaserPointMap.Y_Max.ToString();
                txt_YMin.Text = LaserPointMap.Y_Min.ToString();
                txt_ZMax.Text = LaserPointMap.Z_Max.ToString();
                txt_ZMin.Text = LaserPointMap.Z_Min.ToString();


            }
            catch (Exception ex)
            {
            }
        }

        //缩小
        private void cmd_Zoom_Out_Click(object sender, EventArgs e)
        {
            try
            {
                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);

                BmpVertical.AdjustScale_ZoomOut();

                BmpHorizontal.AdjustScale_ZoomOut();

                Cal_XY_TopLeft_Vertical(X, Y);

                BmpVertical.Display_Bmp(pictureBox_Vertical);

                BmpHorizontal.Display_Bmp(pictureBox_Horizontal);

                //因为比例变更过，所以从新计算鼠标点对应的XYZ
                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);


                txt_Scale.Text = BmpVertical.Scale.ToString();
                txt_XMax.Text = LaserPointMap.X_Max.ToString();
                txt_XMin.Text = LaserPointMap.X_Min.ToString();
                txt_YMax.Text = LaserPointMap.Y_Max.ToString();
                txt_YMin.Text = LaserPointMap.Y_Min.ToString();
                txt_ZMax.Text = LaserPointMap.Z_Max.ToString();
                txt_ZMin.Text = LaserPointMap.Z_Min.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        //配卷按钮按下
        private void cmdPeiJuan_Click(object sender, EventArgs e)
        {
            try
            {
                frmSocialCarOut frmSocialCarOutOper = new frmSocialCarOut();
                frmSocialCarOutOper.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }

        DateTime timeClicked = DateTime.Now;

        private void pictureBox_Vertical_Click(object sender, EventArgs e)
        {
            try
            {
                TimeSpan span = DateTime.Now - timeClicked;
                if (span.TotalSeconds < 2)
                {
                    return;
                }
                else
                {
                    timeClicked = DateTime.Now;
                }

                lockPostion();
            }
            catch (Exception ex)
            {
            }
        }

        private void conLine_Vertical_Y_Click(object sender, EventArgs e)
        {
            try
            {
                TimeSpan span = DateTime.Now - timeClicked;
                if (span.TotalSeconds < 2)
                {
                    return;
                }
                else
                {
                    timeClicked = DateTime.Now;
                }

                lockPostion();
            }
            catch (Exception ex)
            {
            }
        }

        private void conLine_Vertical_X_Click(object sender, EventArgs e)
        {
            try
            {
                TimeSpan span = DateTime.Now - timeClicked;
                if (span.TotalSeconds < 2)
                {
                    return;
                }
                else
                {
                    timeClicked = DateTime.Now;
                }

                lockPostion();
            }
            catch (Exception ex)
            {
            }
        }

        private void lockPostion()
        {
            try
            {
                Cal_X_Y_Z_MousePoint(ref X, ref Y, ref Z);

                if (carLocationLineAssistant.NeedToLocateUpLine == true)
                {
                    carLocationLineAssistant.NeedToLocateUpLine = false;
                    carLocationLineAssistant.XUpLine = X;
                    carLocationLineAssistant.HasLocatedUpLine = true;
                    carLocationLineAssistant.tryCalCenterX();
                    displayInfoCarLocationLine();
                    return;
                }
                if (carLocationLineAssistant.NeedToLocateLowLine == true)
                {
                    carLocationLineAssistant.NeedToLocateLowLine = false;
                    carLocationLineAssistant.XLowLine = X;
                    carLocationLineAssistant.HasLocatedLowLine = true;
                    carLocationLineAssistant.tryCalCenterX();
                    displayInfoCarLocationLine();
                    return;
                }
                //增加左右边线
                if (carLocationLineAssistant.NeedToLocateLLine == true)
                {
                    carLocationLineAssistant.NeedToLocateLLine = false;
                    carLocationLineAssistant.XLLine = Y;
                    carLocationLineAssistant.HasLocatedLLine = true;
                    //carLocationLineAssistant.tryCalCenterX();
                    displayInfoCarLocationLine();
                    return;
                }
                if (carLocationLineAssistant.NeedToLocateRLine == true)
                {
                    carLocationLineAssistant.NeedToLocateRLine = false;
                    carLocationLineAssistant.XRLine = Y;
                    carLocationLineAssistant.HasLocatedRLine = true;
                    //carLocationLineAssistant.tryCalCenterX();
                    displayInfoCarLocationLine();
                    return;
                }


                int Z30Minus = this.LaserPointMap.GetSampleZ(Y - 300);
                int Z30Plus = this.LaserPointMap.GetSampleZ(Y + 300);

                int Z20Minus = this.LaserPointMap.GetSampleZ(Y - 200);
                int Z20Plus = this.LaserPointMap.GetSampleZ(Y + 200);

                int Z10Minus = this.LaserPointMap.GetSampleZ(Y - 100);
                int Z10Plus = this.LaserPointMap.GetSampleZ(Y + 100);

                //if ( isZOK(Z30Minus) && isZOK(Z30Plus) &&
                //    isZOK(Z20Minus) && isZOK(Z20Plus) &&
                //    isZOK(Z10Minus) && isZOK(Z10Plus) &&
                //    isZOK(Z))
                //{

                //}
                //else
                //{
                //    MessageBox.Show("所选择的位置高度不安全");
                //    return;
                //}

                //if (isXOK(X))
                //{
                //}
                //else
                //{
                //    MessageBox.Show("所选择的位置偏离停车位中心线过多");
                //    return;
                //}

                //安全起见，车边线定好
                if (txtXUpLine.Text == string.Empty || txtXLowLine.Text == string.Empty || txtXLLine.Text == string.Empty || txtXRLine.Text == string.Empty)
                {
                    MessageBox.Show("请先定好车的四条边线再选点");
                }
                else
                {
                    //选择的点要在定好的框内
                    if (Y > carLocationLineAssistant.XLLine && Y < carLocationLineAssistant.XRLine && X > carLocationLineAssistant.XUpLine && X < carLocationLineAssistant.XLowLine)
                    {
                        if (Z20Minus < Z || Z20Plus < Z)
                        {
                            DialogResult ret = MessageBox.Show("这可能不是凹槽底部,要选择这个点吗?", "这可能不是凹槽底部,要选择这个点吗?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                            if (ret == DialogResult.No)
                            {
                                return;
                            }
                        }

                        dataGridLaserResult.Rows.Add();
                        DataGridViewRow theRow = dataGridLaserResult.Rows[dataGridLaserResult.Rows.Count - 1];
                        theRow.Cells["Treatment_NO"].Value = strTreatmentNO.ToString();
                        theRow.Cells["Scan_Count"].Value = intMaxCount.ToString();
                        //添加-----20190809----
                        theRow.Cells["Parking_NO"].Value = strParkingNO.ToString();
                        theRow.Cells["Car_NO"].Value = strCarNO.ToString();
                        if (carLocationLineAssistant.NeedLockCenterLine == true && carLocationLineAssistant.NeedLockCenterLine == true && carLocationLineAssistant.XCenterLine > 0)
                        {
                            theRow.Cells["theX"].Value = carLocationLineAssistant.XCenterLine.ToString();
                        }
                        else
                        {
                            theRow.Cells["theX"].Value = X.ToString();
                        }

                        theRow.Cells["theY"].Value = Y.ToString();
                        //激光数据z不稳定暂时给1330定值
                        int A = 1330;
                        theRow.Cells["theZ"].Value = A.ToString();
                        if (carLocationLineAssistant.NeedLockCenterLine == true && carLocationLineAssistant.NeedLockCenterLine == true && carLocationLineAssistant.XCenterLine > 0)
                        {
                            BmpVertical.drawPointSelected(carLocationLineAssistant.XCenterLine, Y);
                        }
                        else
                        {
                            BmpVertical.drawPointSelected(X, Y);
                        }
                        //BmpVertical.Create_DifferentScale_Bmps();
                        BmpVertical.CopyImage(BmpVertical.Scale);
                        BmpVertical.Display_Bmp(pictureBox_Vertical);

                        System.Threading.Thread thread = new System.Threading.Thread(BmpVertical.Create_DifferentScale_Bmps_ExceptCurrentScale);
                        thread.Start();
                        //BmpVertical.Create_DifferentScale_Bmps_ExceptCurrentScale();
                    }

                    else
                    {
                        MessageBox.Show("所选的点不在指定框内，请重新选择");
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        private bool isZOK(int z)
        {
            bool ret = false;
            try
            {
                if (z < 2000 && z>500)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
            }
            return ret;
        }
        private bool isXOK(int x)
        {
            bool ret = false;
            try
            {
                if (carLocationLineAssistant.NeedLockCenterLine == true && carLocationLineAssistant.NeedLockCenterLine == true && carLocationLineAssistant.XCenterLine > 0)
                {
                        ret = true;
                }
                else
                {
                    if (Math.Abs(BmpVertical.CenterParkingX - x) <= 500)
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return ret;
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
            }
        }
//, Convert.ToInt64(theRow.Cells["Parking_NO"].Value.ToString()),Convert.ToInt64(theRow.Cells["Car_NO"].Value.ToString())
        private void cmdCommitData_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult ret = MessageBox.Show("请确认提交数据", "请确认提交数据", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (ret == DialogResult.No)
                {
                    return;
                }
                if (strParkingNO != string.Empty && strTreatmentNO != string.Empty && intMaxCount != 0 && dataGridLaserResult.Rows.Count > 0)
                {
                    clsParkingSpaceUtility parkingSpaceUtility = new clsParkingSpaceUtility();
                    parkingSpaceUtility.Delete_UACS_CAMERASCAN_DETAIL(strTreatmentNO, intMaxCount);
                    foreach (DataGridViewRow theRow in dataGridLaserResult.Rows)
                    {
                       
                            parkingSpaceUtility.InsertInto_UACS_CAMERASCAN_DETAIL(theRow.Cells["Treatment_NO"].Value.ToString(),
                                                                                    Convert.ToInt64(theRow.Cells["Scan_Count"].Value.ToString()),
                                                                                    Convert.ToInt64(theRow.Cells["theX"].Value.ToString()),
                                                                                    Convert.ToInt64(theRow.Cells["theY"].Value.ToString()),
                                                                                    Convert.ToInt64(theRow.Cells["theZ"].Value.ToString()));
                  
                    }

                    
                    //Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
                    TagValues.Add("EV_PARKING_OUT_LASEREND", null);
                    string tagLaserOut = "";
                    tagLaserOut = strParkingNO + "|" + strCarNO + "|" + strTreatmentNO + "|" + intMaxCount;
                    tagProvider_InStock.SetData("EV_NEW_PARKING_OUT_LASEREND", tagLaserOut);
                    MessageBox.Show("数据提交成功");
                }
                else
                {
                    MessageBox.Show("提交数据的条件不满足");
                }

            }
            catch (Exception)
            {
            }
        }

        private bool needToShowFirstTime = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (needToShowFirstTime == true)
                {
                    if (File.Exists(BmpVertical.FilePath + BmpVertical.FileName + "_10" + ".bmp")==true)
                    {
                        if (File.Exists(BmpHorizontal.FilePath + BmpHorizontal.FileName + "_10" + ".bmp")==true)
                        {
                            Init_XY_TopLeft_Vertical();

                            //BmpVertical.Display_Bmp(pictureBox_Vertical);


                            //BmpHorizontal.Display_Bmp(pictureBox_Horizontal);


                            //MessageBox.Show("图像处理成功");

                            needToShowFirstTime = false;
                            timer.Enabled = false;
                            cmd_DisplayImage_Click(new object(), new EventArgs());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txt_X_Click(object sender, EventArgs e)
        {

        }

        private void txt_YMin_Click(object sender, EventArgs e)
        {

        }
        private void txt_ZMax_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Vertical_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox_Horizontal_Click(object sender, EventArgs e)
        {

        }

        private void conLine_Vertical_X_Load(object sender, EventArgs e)
        {

        }

        private void conLine_Vertical_Y_Load(object sender, EventArgs e)
        {

        }

        private void conLine_Horizontal_Y_Load(object sender, EventArgs e)
        {

        }

        private void cmdReadData_Click_1(object sender, EventArgs e)
        {

        }

        private void cmd_Zoom_IN_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridLaserResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txt_XMax_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmdLockCenterLine_Click_1(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        //删除按钮
        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = this.dataGridLaserResult.Rows.Count - 1; i >= 0; i--)
                {
                    //string  hasChecked = this.dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value.ToString();
                    //string coilNo = this.dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                    bool hasChecked = (bool)this.dataGridLaserResult.Rows[i].Cells["Check_Data"].EditedFormattedValue;
                    if (hasChecked)
                    { 
                        
                        //BmpVertical.drawPointSelected_1((int)dataGridLaserResult.Rows[i].Cells["theX"].Value, (int)dataGridLaserResult.Rows[i].Cells["theY"].Value);
                       
                        ////BmpVertical.Create_DifferentScale_Bmps();
                        //BmpVertical.CopyImage(BmpVertical.Scale);
                        //BmpVertical.Display_Bmp(pictureBox_Vertical);

                        //System.Threading.Thread thread = new System.Threading.Thread(BmpVertical.Create_DifferentScale_Bmps_ExceptCurrentScale);
                        //thread.Start();
                        dataGridLaserResult.Rows.Remove(dataGridLaserResult.CurrentRow);
                      
                        //this.dataGridLaserResult.Rows[i].Cells["Treatment_NO"].Value = "";
                        //this.dataGridLaserResult.Rows[i].Cells["Scan_Count"].Value = "";
                        //dataGridLaserResult.Rows[i].Cells["theX"].Value = "";
                        //dataGridLaserResult.Rows[i].Cells["theY"].Value = "";
                        //dataGridLaserResult.Rows[i].Cells["theZ"].Value = ""; 

                        //消除打钩
                        
                        this.dataGridLaserResult.Rows[i].Cells["Check_Data"].Value = 0;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }















 


        //private void pictureBox_Horizontal_MouseClick(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        Graphics g = this.pictureBox_Horizontal.CreateGraphics();
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //        Brush bush = new SolidBrush(Color.SeaShell);//填充的颜色
        //        g.FillEllipse(bush, e.X-100, e.Y-100, 200, 200);

        //        bush = new SolidBrush(Color.Black);
        //        g.FillEllipse(bush, e.X - 15, e.Y - 15, 30, 30);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
















    }
}
