namespace Draw_Point
{
    class DrawPoint
    {
        private PictureBox pictureBox1;
        private Point pointLocation;

        // 新增两个属性
        public Color PenColor = Color.Red; // 默认颜色为红色
        public float PenWidth = 3; // 默认线条宽度为3

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pointLocation = e.Location;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = GetImage();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //在pointlocation处画一个粗细为3半径为5的红色圆圈
            e.Graphics.DrawEllipse(new Pen(PenColor, PenWidth), pointLocation.X - 5, pointLocation.Y - 5, 10, 10);
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
                pictureBox1.MouseUp -= pictureBox1_MouseUp;
                pictureBox1.Paint -= pictureBox1_Paint;
            }
            else
            {
                // 添加事件处理程序
                pictureBox1.MouseDown += pictureBox1_MouseDown;
                pictureBox1.MouseUp += pictureBox1_MouseUp;
                pictureBox1.Paint += pictureBox1_Paint;
            }
        }

        //外部调用的函数，初始PictureBox pictureBoxIn这两个变量
        public void pointAddOperation(PictureBox pictureBoxIn)
        {
            pictureBox1 = pictureBoxIn;
        }

        //外部调用的函数，修改线条粗细和颜色这两个变量
        public void SetPen(int width, Color color)
        {
            PenWidth = width;
            PenColor = color;
        }
    }
}
