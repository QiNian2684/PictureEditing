namespace Draw_Rectangle
{
    class DrawRectangle
    {
        public double change;
        private PictureBox pictureBox1;
        private System.Drawing.Point start_rectangle; //矩形的起始点
        private int tmp = -1;
        private System.Drawing.Point ansLocation;
        private double wide;
        private double high;
        private Image tmpImage;

        // 新增两个属性
        public Color PenColor = Color.Red; // 默认颜色为红色
        public float PenWidth = 3; // 默认线条宽度为3

        Rectangle rect;

        //用于计算长度的起始终止点坐标
        System.Drawing.Point Begin, final;

        //外部调用的函数，初始PictureBox pictureBoxIn, double changeIn这两个变量
        public void rectangleAddOperation(PictureBox pictureBoxIn, double changeIn)
        {
            pictureBox1 = pictureBoxIn;
            change = changeIn;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tmp == -1)
            {
                tmp++;
                tmpImage = pictureBox1.Image;
            }

            if (e.Button == MouseButtons.Right)
            {
                pictureBox1.Image = tmpImage;
                Init();
                return;
            }
            else
            {
                if (tmp == 0)
                {
                    start_rectangle = e.Location;
                    Begin = e.Location;
                }
                else if (tmp == 1)
                {
                    ansLocation = e.Location;
                    pictureBox1.Image = GetImage();
                    tmp++;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tmp == 0)
            {
                final = e.Location;
                wide = ((double)final.X - (double)Begin.X) * change;
                wide = Math.Abs(wide);
                high = ((double)final.Y - (double)Begin.Y) * change;
                high = Math.Abs(high);

                pictureBox1.Image = GetImage();
                tmp++;
            }
            else if (tmp == 2)
            {
                Init();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tmp == 0)
            {
                if (pictureBox1.Image != null)
                {
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        e.Graphics.DrawRectangle(new Pen(PenColor, PenWidth), rect); // 使用 PenColor 和 PenWidth 重新绘制
                    }
                }
            }
            else if (tmp == 1)
            {
                string ansText = wide.ToString("0.00(mm)") + "*" + high.ToString("0.00(mm)");
                e.Graphics.DrawString(ansText, new Font("宋体", 20), new SolidBrush(PenColor), ansLocation);
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tmp == 0)
            {
                if (e.Button != MouseButtons.Left)//判断是否按下左键
                    return;
                System.Drawing.Point tempEndPoint = e.Location; //记录框的位置和大小
                rect.Location = new System.Drawing.Point(
                Math.Min(start_rectangle.X, tempEndPoint.X),
                Math.Min(start_rectangle.Y, tempEndPoint.Y));
                rect.Size = new System.Drawing.Size(
                Math.Abs(start_rectangle.X - tempEndPoint.X),
                Math.Abs(start_rectangle.Y - tempEndPoint.Y));
                pictureBox1.Invalidate();
            }

            else if (tmp == 1)
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

        //初始化所有声明的变量
        public void Init()
        {
            tmp = -1;
            start_rectangle = new System.Drawing.Point();
            ansLocation = new System.Drawing.Point();
            wide = 0;
            high = 0;
            rect = new Rectangle();
            Begin = new System.Drawing.Point();
            final = new System.Drawing.Point();
            tmpImage = GetImage();
        }

        public void SetPen(int width, Color color)
        {
            PenWidth = width;
            PenColor = color;
        }
    }
}

