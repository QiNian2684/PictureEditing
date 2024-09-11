namespace Draw_Curve
{
    class DrawCurve
    {
        private Graphics graphics;
        public double change;
        private PictureBox pictureBox1;

        private int tmp = -1;
        private System.Drawing.Point ansLocation;
        private double area = 0;
        private Image tmpImage;

        private List<System.Drawing.Point> nodes = new List<System.Drawing.Point>(); // 用于存储节点的列表

        // 新增两个属性
        public Color PenColor = Color.Red; // 默认颜色为红色
        public float PenWidth = 3; // 默认线条宽度为3


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
                // 如果正在绘制曲线并且按下了鼠标左键，就添加一个新节点到节点列表中
                nodes.Add(new System.Drawing.Point(e.X, e.Y));
                pictureBox1.Invalidate(); // 使图片框失效并强制重新绘制

                if (tmp == 1)
                {
                    pictureBox1.Image = GetImage();
                    Init();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (nodes.Count >= 2)
            {
                // 如果节点数大于等于 2，则表示已经确定了至少一条线段
                int lastNodeIndex = nodes.Count - 1;

                if (Distance(nodes[lastNodeIndex], nodes[0]) < 10)
                {
                    // 如果新添加的节点与第一个节点之间的距离小于 10 像素，则连接上一个节点与此节点的同时连接最后一个节点与第一个节点
                    nodes[lastNodeIndex] = nodes[0];

                    pictureBox1.Image = GetImage();
                    area = GetLength(nodes) * change;
                    tmp++;
                }
                pictureBox1.Invalidate(); // 使图片框失效并强制重新绘制
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (tmp == 0)
            {
                //第一个节点的绘制
                if (nodes.Count == 1)
                {
                    // 绘制节点
                    foreach (System.Drawing.Point node in nodes)
                    {
                        e.Graphics.FillEllipse(new SolidBrush(PenColor), node.X - 5, node.Y - 5, 10, 10);
                    }
                }

                //随后的节点绘制
                if (nodes.Count >= 2)
                {
                    // 如果正在绘制曲线并且节点数大于等于 2，则开始绘制曲线
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    // 绘制节点
                    foreach (System.Drawing.Point node in nodes)
                    {
                        e.Graphics.FillEllipse(new SolidBrush(PenColor), node.X - 5, node.Y - 5, 10, 10);
                    }
                    // 绘制线段

                    for (int i = 1; i < nodes.Count; i++)
                    {
                        // 使用 PenColor 和原有 pen 的宽度创建一个新的 Pen 对象来绘制线条
                        using (Pen currentPen = new Pen(PenColor, PenWidth))
                        {
                            e.Graphics.DrawLine(currentPen, nodes[i - 1], nodes[i]);
                        }
                        if (Distance(nodes[nodes.Count - 1], nodes[0]) < 10)
                        {
                            // 如果新添加的节点与第一个节点之间的距离小于 10 像素，则连接上一个节点与此节点的同时连接最后一个节点与第一个节点
                            using (Pen currentPen = new Pen(PenColor, PenWidth))
                            {
                                e.Graphics.DrawLine(currentPen, nodes[nodes.Count - 1], nodes[0]);
                            }
                            nodes[nodes.Count - 1] = nodes[0]; // 修改最后一个节点的位置，使它与第一个节点重合
                                                               // 绘制封闭图形
                            using (Pen currentPen = new Pen(PenColor, PenWidth))
                            {
                                e.Graphics.DrawLine(currentPen, nodes[nodes.Count - 1], nodes[0]);
                            }
                        }
                    }
                }
            }
            if (tmp == 1)
            {
                //在第一个节点处用 PenColor 和 20号宋体输出area的值，保留两位小数
                e.Graphics.DrawString(area.ToString("0.00(mm)"), new Font("宋体", 20), new SolidBrush(PenColor), ansLocation);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tmp == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        // 如果正在绘制曲线并且按下了鼠标左键，就更新最后一个节点的位置
                        int lastNodeIndex = nodes.Count() - 1;
                        nodes[lastNodeIndex] = new System.Drawing.Point(e.X, e.Y);
                        pictureBox1.Invalidate(); // 使图片框失效并强制重新绘制
                    }
                }
            }


            if (tmp == 1)
            {
                ansLocation = e.Location;
                pictureBox1.Invalidate();
            }
        }


        //计算两点之间的距离
        private double Distance(System.Drawing.Point p1, System.Drawing.Point p2)
        {
            int dx = p1.X - p2.X;
            int dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
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
            nodes.Clear();
            tmp = -1;
            area = 0;
            ansLocation = new System.Drawing.Point(0, 0);
            tmpImage = GetImage();
        }

        //外部调用的函数，初始PictureBox pictureBoxIn, double changeIn这两个变量
        public void curveAddOperation(PictureBox pictureBoxIn, double changeIn)
        {
            pictureBox1 = pictureBoxIn;
            change = changeIn;
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

        //points列表中相邻两个节点之间的距离之和。
        public double GetLength(List<System.Drawing.Point> points)
        {
            double length = 0;
            for (int i = 1; i < points.Count; i++)
            {
                length += Distance(points[i - 1], points[i]);
            }
            return length;
        }

        //外部调用的函数，修改线条粗细和颜色这两个变量
        public void SetPen(int width, Color color)
        {
            PenWidth = width;
            PenColor = color;
        }
    }
}