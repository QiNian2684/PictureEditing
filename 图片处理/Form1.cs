using corrected_Parameter_Change;
using Draw_Curve;
using Draw_Line;
using Draw_Point;
using Draw_Rectangle;
using Get_Screen_Show;
using Gray_Scale;
using OpenCvSharp;
using Picture_Change;
using Screen_Shot_Image_List;

namespace 图片处理
{
    public partial class Form1 : Form
    {
        // 绘图状态标志
        private bool is_draw_point = false; // 是否绘制点
        private bool is_draw_line = false; // 是否绘制线
        private bool is_draw_rectangle = false; // 是否绘制矩形
        private bool is_draw_curve = false; // 是否绘制曲线

        // 当前操作的图像
        private Image currentImage; // 当前操作的图像实例

        // 图像是否为灰度图的标志
        private bool isGrayScale = false; // 当前图像是否为灰度图
        private Image originalImage; // 原始图像

        // 绘图属性
        private Color penColor = Color.Red; // 默认绘图颜色为红色
        //墨迹粗细
        private int penWidth = 3; // 默认绘图笔宽为3

        // 二值化处理相关的Mat对象
        private Mat? originalMat = null; // 原始图像的Mat对象
        private Mat? grayMat = null; // 灰度化后的Mat对象
        private Mat? binaryMat = null; // 二值化处理后的Mat对象
        private Bitmap? tempImage = null; // 临时图像，用于显示处理结果
        // 外接矩形处理相关的Mat对象
        private Mat? originalMat1 = null; // 原始图像的Mat对象，用于外接矩形处理
        private Mat? grayMat1 = null; // 灰度化后的Mat对象，用于外接矩形处理
        private Mat? binaryMat1 = null; // 二值化处理后的Mat对象，用于外接矩形处理
        private Bitmap? tempImage1 = null; // 临时图像，用于显示外接矩形处理结果
        // 图像处理参数
        //private double changeParameter = 1; // 特定的图像处理参数
        private double imageParameter = 0.00892857; // 通用的图像处理参数
        private double correctedParameter = 0.00892857; // 经过修正的图像处理参数

        //对象实例化
        GetScreenShow GSS = new GetScreenShow();
        ScreenShotImageList SSIL = new ScreenShotImageList();
        PictureChange PC = new PictureChange();
        DrawPoint DP = new DrawPoint();
        DrawLine DL = new DrawLine();
        DrawCurve DC = new DrawCurve();
        DrawRectangle DR = new DrawRectangle();
        correctedParameterChange CPC = new correctedParameterChange();
        GrayScale GS = new GrayScale();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        OpenFileDialog openFile = new OpenFileDialog();


        public Form1()
        {
            InitializeComponent();

            // 清空保存图片的临时文件夹
            DeleteSaveImgFolderContents();

            // 初始化绘图操作
            DP.pointAddOperation(Main_PictureBox); // 点绘制
            DL.lineAddOperation(Main_PictureBox, correctedParameter); // 线绘制，包含修正参数
            DC.curveAddOperation(Main_PictureBox, correctedParameter); // 曲线绘制，包含修正参数
            DR.rectangleAddOperation(Main_PictureBox, correctedParameter); // 矩形绘制，包含修正参数

            // 将PictureBox注册到屏幕显示服务
            GSS.AddPictureBox(Main_PictureBox);

        }
        //删除文件操作
        private void DeleteSaveImgFolderContents()
        {
            try
            {
                string directoryPath = Path.Combine(Application.StartupPath, "saveimg");

                if (Directory.Exists(directoryPath))
                {
                    foreach (string file in Directory.GetFiles(directoryPath))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch (IOException)
                        {
                            // 忽略无法删除的文件
                        }
                        catch (UnauthorizedAccessException)
                        {
                            // 忽略无法删除的文件
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除图片时出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*-------------------------------------加载图片-------------------------------------*/
        private void button_add_MouseClick(object sender, MouseEventArgs e)
        {
            openFile.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentImage = Image.FromFile(openFile.FileName);
                Main_PictureBox.Image = currentImage;
            }
        }

        /*-------------------------------------保存图片-------------------------------------*/
        private void button_save_Click(object sender, EventArgs e)
        {
            if(Main_PictureBox.Image == null)
            {
                MessageBox.Show("请先加载一张图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Image SaveImage = GSS.CaptureScreen();

                // 设置保存文件对话框的初始目录和文件名过滤器
                saveFileDialog.InitialDirectory = "D:\\";
                saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";

                // 显示保存文件对话框
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取要保存的图像文件名
                    string fileName = saveFileDialog.FileName;

                    // 将PictureBox控件中的图像保存到文件中
                    SaveImage.Save(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存图像时出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //截取显示图片
        private void Screen_Shot_Click(object sender, EventArgs e)
        {
            if (Main_PictureBox.Image != null)
            {
                Image screemShotImage = GSS.CaptureScreen();
                SSIL.ScreenShotImageOperation(screemShotImage, ScreenShotHistoryList, Main_PictureBox);
            }
        }

        /*-------------------------------------拖动条操作-------------------------------------*/
        private void ResetSlidersExcept(string sliderName)
        {
            if (sliderName != "LIGHTNESS") LIGHTNESS.Value = 0;
            if (sliderName != "CONTRAST") CONTRAST.Value = (CONTRAST.Maximum + CONTRAST.Minimum) / 2;
            if (sliderName != "RGB_R") RGB_R.Value = 0;
            if (sliderName != "RGB_G") RGB_G.Value = 0;
            if (sliderName != "RGB_B") RGB_B.Value = 0;
        }

        private void ApplyImageChange(PictureChange.CHANGE_TYPE changeType, int value)
        {
            Main_PictureBox.Image = PC.Picture_change_operate(changeType, value, currentImage);
        }

        private void LIGHTNESS_MouseUp(object sender, MouseEventArgs e)
        {
            ResetSlidersExcept("LIGHTNESS");
            ApplyImageChange(PictureChange.CHANGE_TYPE.LIGHT, LIGHTNESS.Value);
        }

        private void CONTRAST_MouseUp(object sender, MouseEventArgs e)
        {
            ResetSlidersExcept("CONTRAST");
            ApplyImageChange(PictureChange.CHANGE_TYPE.CONTRAST, CONTRAST.Value);
        }

        private void RGB_R_MouseUp(object sender, MouseEventArgs e)
        {
            ResetSlidersExcept("RGB_R");
            ApplyImageChange(PictureChange.CHANGE_TYPE.RED, RGB_R.Value);
        }

        private void RGB_G_MouseUp(object sender, MouseEventArgs e)
        {
            ResetSlidersExcept("RGB_G");
            ApplyImageChange(PictureChange.CHANGE_TYPE.GREEN, RGB_G.Value);
        }

        private void RGB_B_MouseUp(object sender, MouseEventArgs e)
        {
            ResetSlidersExcept("RGB_B");
            ApplyImageChange(PictureChange.CHANGE_TYPE.BLUE, RGB_B.Value);
        }

        /*-------------------------------------绘图操作-------------------------------------*/
        private void DrawPoint_Click(object sender, EventArgs e)
        {
            DrawPoint.Text = DrawPoint.Text == "打点操作" ? "ON" : "打点操作";
            DrawRectangle.Text = "画框操作";
            DrawCurve.Text = "绘制曲线";
            DrawLine.Text = "绘制线段";

            is_draw_point = is_draw_point ? false : true;
            is_draw_line = false;
            is_draw_rectangle = false;
            is_draw_curve = false;

            DP.AddOrRemoveEvent(false);
            DC.AddOrRemoveEvent(false);
            DL.AddOrRemoveEvent(false);
            DR.AddOrRemoveEvent(false);

            if (is_draw_point)
            {
                DP.AddOrRemoveEvent(true);
            }
            else
            {
                DP.AddOrRemoveEvent(false);
            }
        }

        private void DrawRectangle_Click(object sender, EventArgs e)
        {
            DrawRectangle.Text = DrawRectangle.Text == "画框操作" ? "ON" : "画框操作";
            DrawPoint.Text = "打点操作";
            DrawCurve.Text = "绘制曲线";
            DrawLine.Text = "绘制线段";

            is_draw_rectangle = is_draw_rectangle ? false : true;
            is_draw_point = false;
            is_draw_line = false;
            is_draw_curve = false;

            DP.AddOrRemoveEvent(false);
            DL.AddOrRemoveEvent(false);
            DC.AddOrRemoveEvent(false);

            if (is_draw_rectangle)
            {
                DR.AddOrRemoveEvent(true);
            }
            else
            {
                DR.AddOrRemoveEvent(false);
            }
        }

        private void DrawCurve_Click(object sender, EventArgs e)
        {
            DrawCurve.Text = DrawCurve.Text == "绘制曲线" ? "ON" : "绘制曲线";
            DrawPoint.Text = "打点操作";
            DrawRectangle.Text = "画框操作";
            DrawLine.Text = "绘制线段";

            is_draw_curve = is_draw_curve ? false : true;
            is_draw_point = false;
            is_draw_line = false;
            is_draw_rectangle = false;

            DP.AddOrRemoveEvent(false);
            DL.AddOrRemoveEvent(false);
            DR.AddOrRemoveEvent(false);

            if (is_draw_curve)
            {
                DC.AddOrRemoveEvent(true);
            }
            else
            {
                DC.AddOrRemoveEvent(false);
            }
        }

        private void DrawLine_Click(object sender, EventArgs e)
        {
            DrawLine.Text = DrawLine.Text == "绘制线段" ? "ON" : "绘制线段";
            DrawPoint.Text = "打点操作";
            DrawRectangle.Text = "画框操作";
            DrawCurve.Text = "绘制曲线";

            is_draw_line = is_draw_line ? false : true;
            is_draw_point = false;
            is_draw_rectangle = false;
            is_draw_curve = false;

            DP.AddOrRemoveEvent(false);
            DC.AddOrRemoveEvent(false);
            DR.AddOrRemoveEvent(false);

            if (is_draw_line)
            {
                DL.AddOrRemoveEvent(true);
            }
            else
            {
                DL.AddOrRemoveEvent(false);
            }
        }

        /*-------------------------------------倍率选择-------------------------------------*/
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 根据 comboBox1 的选择执行不同的代码逻辑
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    correctedParameter = imageParameter / 1;
                    break;
                case 1:
                    correctedParameter = imageParameter / 6;
                    break;
                case 2:
                    correctedParameter = imageParameter / 16;
                    break;
                case 3:
                    correctedParameter = imageParameter / 25;
                    break;
                case 4:
                    correctedParameter = imageParameter / 40;
                    break;
                default:
                    correctedParameter = imageParameter; // 默认情况
                    break;
            }

            // 调用修正方法
            Change();
        }

        // change 参数修正
        private void Change()
        {
            DC.change = correctedParameter;
            DL.change = correctedParameter;
            DR.change = correctedParameter;
        }


        /*-------------------------------------ListView的ItemSelectionChanged事件处理程序-------------------------------------*/
        private void ScreenShotHistoryList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                string filePath = e.Item.Tag as string;

                try
                {
                    using (var tempImage = Image.FromFile(filePath))
                    {
                        Main_PictureBox.Image = new Bitmap(tempImage);
                        currentImage = Main_PictureBox.Image;
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("文件不存在: " + filePath, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载图像失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*-------------------------------------关掉这个窗口的时候删除imagesave文件夹里面的所有文件-------------------------------------*/
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 使用 Application.StartupPath 来构建 directoryPath
                string directoryPath = Path.Combine(Application.StartupPath, "ImageSave");

                if (Directory.Exists(directoryPath))
                {
                    foreach (string file in Directory.GetFiles(directoryPath))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch (IOException)
                        {
                            // 忽略无法删除的文件
                        }
                        catch (UnauthorizedAccessException)
                        {
                            // 忽略无法删除的文件
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除图片时出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*-------------------------------------转化为灰度图像-------------------------------------*/
        private void button_grayScale_Click(object sender, EventArgs e)
        {
            if (Main_PictureBox.Image == null)
            {
                MessageBox.Show("请先加载一张图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!isGrayScale)
            {
                // 保存原始图像
                originalImage = Main_PictureBox.Image;

                Bitmap originalBitmap = new Bitmap(Main_PictureBox.Image);
                Image grayScaleImage = GS.ConvertToGrayscale(originalBitmap);

                // 更新 PictureBox 显示灰度图像
                Main_PictureBox.Image = grayScaleImage;

                // 调用与截屏操作相同的代码
                SSIL.ScreenShotImageOperation(grayScaleImage, ScreenShotHistoryList, Main_PictureBox);

                isGrayScale = true;
            }
            else
            {
                // 恢复原始图像（再次点击时）
                Main_PictureBox.Image = originalImage;

                // 调用与截屏操作相同的代码
                SSIL.ScreenShotImageOperation(originalImage, ScreenShotHistoryList, Main_PictureBox);

                isGrayScale = false;
            }
        }

        /*-------------------------------------二值化外接矩形-------------------------------------*/
        private unsafe void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (originalMat1 == null || grayMat1 == null)
            {
                return;
            }

            // 获取当前拖动条的值作为阈值
            int thresholdValue = trackBar1.Value;

            // 二值化处理
            Cv2.Threshold(grayMat1, binaryMat1, thresholdValue, 255, ThresholdTypes.Binary);

            // 查找轮廓
            var contours = new OpenCvSharp.Point[][] { };
            var hierarchy = new OpenCvSharp.HierarchyIndex[] { };
            Cv2.FindContours(binaryMat1, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // 克隆当前图像用于绘制结果
            Mat result = originalMat1.Clone();

            // 绘制矩形框
            foreach (var contour in contours)
            {
                var rect = Cv2.BoundingRect(contour);
                Cv2.Rectangle(result, rect, new Scalar(0, 0, 10), 3); // 将线条宽度设置为3
            }

            // 将结果图像显示在 PictureBox 中
            Main_PictureBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
        }

        // 在鼠标按下 TrackBar 时保存当前图像并进行预处理
        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Main_PictureBox.Image == null)
            {
                return;
            }

            // 保存当前显示的图像
            tempImage1 = new Bitmap(Main_PictureBox.Image);

            // 转换为 OpenCvSharp 的 Mat 对象
            originalMat1 = OpenCvSharp.Extensions.BitmapConverter.ToMat(tempImage1);

            // 转换为灰度图像
            grayMat1 = new Mat();
            Cv2.CvtColor(originalMat1, grayMat1, ColorConversionCodes.BGR2GRAY);

            // 初始化二值化图像
            binaryMat1 = new Mat();
        }

        // 在松开 TrackBar 时恢复原始图像并将 TrackBar 的值设置为中间值
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Main_PictureBox.Image == null)
            {
                MessageBox.Show("请先加载一张图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 将 TrackBar 的值设置为中间值
                trackBar1.Value = (trackBar1.Minimum + trackBar1.Maximum) / 2;
                return;
            }

            // 恢复原始图像
            Main_PictureBox.Image = new Bitmap(tempImage1);
            originalMat1 = null;
            grayMat1 = null;
            binaryMat1 = null; // 清除临时图像

            // 将 TrackBar 的值设置为中间值
            trackBar1.Value = (trackBar1.Minimum + trackBar1.Maximum) / 2;
        }


        /*-------------------------------------二值化轮廓-------------------------------------*/
        private unsafe void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (originalMat == null || grayMat == null)
            {
                return;
            }

            // 获取当前拖动条的值作为阈值
            int thresholdValue = trackBar2.Value;

            // 二值化处理
            Cv2.Threshold(grayMat, binaryMat, thresholdValue, 255, ThresholdTypes.Binary);

            // 查找轮廓
            var contours = new OpenCvSharp.Point[][] { };
            var hierarchy = new OpenCvSharp.HierarchyIndex[] { };
            Cv2.FindContours(binaryMat, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // 创建黑色背景图像
            Mat result = new Mat(originalMat.Size(), MatType.CV_8UC3, Scalar.All(0));

            // 绘制红色加粗轮廓
            Cv2.DrawContours(result, contours, -1, new Scalar(penColor.B, penColor.G, penColor.R), penWidth);

            // 将结果图像显示在 PictureBox 中
            Main_PictureBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
        }

        // 在鼠标按下 TrackBar 时保存当前图像并进行预处理
        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            if (Main_PictureBox.Image == null)
            {
                return;
            }

            // 保存当前显示的图像
            tempImage = new Bitmap(Main_PictureBox.Image);

            // 转换为 OpenCvSharp 的 Mat 对象
            originalMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(tempImage);

            // 转换为灰度图像
            grayMat = new Mat();
            Cv2.CvtColor(originalMat, grayMat, ColorConversionCodes.BGR2GRAY);

            // 初始化二值化图像
            binaryMat = new Mat();
        }

        // 在松开 TrackBar 时恢复原始图像并将 TrackBar 的值设置为中间值
        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            if (Main_PictureBox.Image == null)
            {
                MessageBox.Show("请先加载一张图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 将 TrackBar 的值设置为中间值
                trackBar2.Value = (trackBar2.Minimum + trackBar2.Maximum) / 2;
                return;
            }

            // 恢复原始图像
            Main_PictureBox.Image = new Bitmap(tempImage);
            originalMat = null;
            grayMat = null;
            binaryMat = null; // 清除临时图像

            // 将 TrackBar 的值设置为中间值
            trackBar2.Value = (trackBar2.Minimum + trackBar2.Maximum) / 2;
        }

        /*-------------------------------------改变墨迹颜色-------------------------------------*/
        private void comboBox_color_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_color.SelectedItem.ToString())
            {
                case "红色":
                    penColor = Color.Red;
                    break;
                case "橙色":
                    penColor = Color.FromArgb(255, 97, 0);
                    break;
                case "黄色":
                    penColor = Color.FromArgb(255, 215, 0);
                    break;
                case "绿色":
                    penColor = Color.FromArgb(0, 255, 0);
                    break;
                case "青色":
                    penColor = Color.FromArgb(0, 255, 225);
                    break;
                case "蓝色":
                    penColor = Color.FromArgb(0, 134, 225);
                    break;
                case "紫色":
                    penColor = Color.FromArgb(138, 43, 226);
                    break;
                default:
                    penColor = Color.Red; // 默认颜色
                    break;
            }
            DC.SetPen(penWidth, penColor);
            DL.SetPen(penWidth, penColor);
            DP.SetPen(penWidth, penColor);
            DR.SetPen(penWidth, penColor);
        }

        /*-------------------------------------改变画笔粗细-------------------------------------*/
        private void comboBox_thick_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_thick.SelectedItem.ToString())
            {
                case " 粗":
                    penWidth = 5;
                    break;
                case "中等":
                    penWidth = 3;
                    break;
                case " 细":
                    penWidth = 1;
                    break;
                default:
                    penWidth = 3; // 默认为中
                    break;
            }

            // 更新绘图工具的笔宽
            DC.SetPen(penWidth, penColor);
            DL.SetPen(penWidth, penColor);
            DP.SetPen(penWidth, penColor);
            DR.SetPen(penWidth, penColor);
        }

        /*-------------------------------------删除图片-------------------------------------*/
        private void delete_pic_Click(object sender, EventArgs e)
        {
            // 检查是否有选中的项
            if (ScreenShotHistoryList.SelectedItems.Count > 0)
            {
                // 获取选中的项
                var selectedItem = ScreenShotHistoryList.SelectedItems[0];
                string filePath = selectedItem.Tag as string;

                if (filePath == null)
                {
                    MessageBox.Show("无法获取文件路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 如果PictureBox当前显示的图片是即将删除的图片，先清空PictureBox的Image属性
                if (Main_PictureBox.Image != null && Main_PictureBox.ImageLocation == filePath)
                {
                    Main_PictureBox.Image.Dispose(); // 释放PictureBox中的图片资源
                    Main_PictureBox.Image = null;
                }

                // 确保UI更新完成，释放所有可能的文件锁
                Application.DoEvents();

                // 删除ListView中的项
                ScreenShotHistoryList.Items.Remove(selectedItem);

                // 删除文件系统中的图片文件
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                        MessageBox.Show("图片删除成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("删除图片时出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("文件不存在: " + filePath, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个要删除的图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}