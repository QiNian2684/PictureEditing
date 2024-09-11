using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Drawing2D;

namespace Draw_Line
{
    class DrawLine
    {
        public double change;
        private PictureBox pictureBox1;
        private double ans;
        private System.Drawing.Point start_line; // 直线的起始点
        private System.Drawing.Point end_line;   // 直线的结束点
        private System.Drawing.Point ansLocation;
        private int tmp = -1;
        private Image tmpImage;

        // 新增两个属性
        public Color PenColor = Color.Red; // 默认颜色为红色
        public float PenWidth = 3; // 默认线条宽度为3

        private Graphics graphics; // 双缓冲绘图使用的 Graphics 对象

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tmp == -1)
            {
                tmp++;
                tmpImage = pictureBox1.Image;
            }

            if (e.Button == MouseButtons.Right)
            {
                //若鼠标右键按下，则初始化
                if (e.Button == MouseButtons.Right)
                {
                    pictureBox1.Image = tmpImage;
                    Init();
                    return;
                }
            }
            else
            {
                if (tmp == 0)
                {
                    start_line = e.Location;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tmp == 0)
            {
                tmp++;
            }
            else if (tmp == 1)
            {
                pictureBox1.Image = GetImage();
                end_line = e.Location;
                tmp++;
                ans = (Math.Sqrt((end_line.X - start_line.X) * (end_line.X - start_line.X) + (end_line.Y - start_line.Y) * (end_line.Y - start_line.Y))) * change;
            }
            else if (tmp == 2)
            {
                pictureBox1.Image = GetImage();
                Init();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tmp == 0)
            {

            }
            else if (tmp == 1)
            {
                // 绘制粗细为3的红色直线
                e.Graphics.DrawLine(new Pen(PenColor, PenWidth), start_line, end_line);
            }
            else if (tmp == 2)
            {
                // 绘制直线长度文本
                e.Graphics.DrawString($"{ans:F2}(mm)", new Font("", 20), new SolidBrush(PenColor), ansLocation);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tmp == 1)
            {
                end_line = e.Location;
                pictureBox1.Invalidate();
            }
            else if (tmp == 2)
            {
                ansLocation = e.Location;
                pictureBox1.Invalidate();
            }
        }

        //将picturebox1上的所有图层的图像合并到原先在picturebox1中显示的图像里中，并作为返回值返回
        public Image GetImage()
        {
            Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(image, pictureBox1.ClientRectangle);
            return image;
        }

        //初始化所有变量
        public void Init()
        {
            tmp = -1;
            start_line = new System.Drawing.Point();
            end_line = new System.Drawing.Point();
            ansLocation = new System.Drawing.Point();
            ans = 0;
            tmpImage = GetImage();
        }

        //外部调用的函数，初始PictureBox pictureBoxIn, double changeIn这两个变量
        public void lineAddOperation(PictureBox pictureBoxIn, double changeIn)
        {
            pictureBox1 = pictureBoxIn;
            change = changeIn;

            // 初始化双缓冲绘图所需的位图和 Graphics 对象
            // 创建初始位图
            Mat mat = new Mat(pictureBox1.Height, pictureBox1.Width, MatType.CV_8UC3, new Scalar(255, 255, 255));
            Bitmap bitmap = BitmapConverter.ToBitmap(mat);

            // 初始化双缓存绘图所需的 Graphics 对象
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        //判断执行此函数时有没有加载事件处理程序。如果现在有事件处理程序就删掉，如果没有就添加（无参且外部调用）
        public void AddOrRemoveEvent(bool isDrawing)
        {
            if (isDrawing == false)
            {
                //删除事件处理程序
                pictureBox1.MouseDown -= pictureBox1_MouseDown;
                pictureBox1.MouseMove -= pictureBox1_MouseMove;
                pictureBox1.MouseUp -= pictureBox1_MouseUp;
                pictureBox1.Paint -= pictureBox1_Paint;
            }
            else
            {
                // 添加事件处理程序
                pictureBox1.MouseDown += pictureBox1_MouseDown;
                pictureBox1.MouseMove += pictureBox1_MouseMove;
                pictureBox1.MouseUp += pictureBox1_MouseUp;
                pictureBox1.Paint += pictureBox1_Paint;
            }
        }

        //外部调用的函数，修改线条粗细和颜色这两个变量
        public void SetPen(int width, Color color)
        {
            PenWidth = width;
            PenColor = color;
        }
    }
}