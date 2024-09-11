namespace Draw_Angle
{


    class DrawAngle
    {
        private PictureBox pictureBox1;

        private System.Drawing.Point start_angle; // 直线的起始点
        private System.Drawing.Point middle_angle; // 角度的中间点
        private System.Drawing.Point end_angle; // 角度的结束点
        private System.Drawing.Point ansLocation;
        private int tmp = -1;
        private Image tmpImage;
        double degreeAngle;

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
                    start_angle = e.Location;
                }
                else if (tmp == 1)
                {
                    middle_angle = e.Location;
                    end_angle = e.Location;
                    pictureBox1.Image = GetImage();
                }
                else if (tmp == 2)
                {
                    end_angle = e.Location;
                    pictureBox1.Image = GetImage();
                    ansLocation = end_angle;
                    pictureBox1.Image = GetImage();
                    tmp++;
                }
                else if (tmp == 3)
                {
                    // Do nothing
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
                tmp++;
            }
            else if (tmp == 2)
            {
                // 计算向量1和向量2
                double v1x = middle_angle.X - start_angle.X;
                double v1y = middle_angle.Y - start_angle.Y;
                double v2x = end_angle.X - middle_angle.X;
                double v2y = end_angle.Y - middle_angle.Y;

                // 计算向量1和向量2的模
                double v1Length = Math.Sqrt(v1x * v1x + v1y * v1y);
                double v2Length = Math.Sqrt(v2x * v2x + v2y * v2y);

                // 计算向量1和向量2的点积
                double dotProduct = v1x * v2x + v1y * v2y;

                // 计算夹角（弧度制）
                double angle = Math.Acos(dotProduct / (v1Length * v2Length));

                // 将弧度制转换为角度制
                degreeAngle = angle * 180 / Math.PI;

                tmp++;
            }
            if (tmp == 4)
            {
                Init();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tmp == 1)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 3), start_angle, middle_angle);
            }
            else if (tmp == 2)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 3), start_angle, middle_angle);
                e.Graphics.DrawLine(new Pen(Color.Red, 3), middle_angle, end_angle);
            }
            else if (tmp == 3)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 3), start_angle, middle_angle);
                e.Graphics.DrawLine(new Pen(Color.Red, 3), middle_angle, end_angle);
                e.Graphics.DrawString(degreeAngle.ToString("0.00°"), new Font("宋体", 20), new SolidBrush(Color.Red), ansLocation);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tmp == 1)
            {
                middle_angle = e.Location;
                pictureBox1.Invalidate();
            }
            if (tmp == 2)
            {
                end_angle = e.Location;
                pictureBox1.Invalidate(); // 触发Paint事件重新绘制PictureBox
            }
        }

        // 将picturebox1上的所有图层的图像合并到原先在picturebox1中显示的图像里中，并作为返回值返回
        public Image GetImage()
        {
            Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(image, pictureBox1.ClientRectangle);
            return image;
        }

        // 判断执行此函数时有没有加载事件处理程序。如果现在有事件处理程序就删掉，如果没有就添加（无参且外部调用）
        public void AddOrRemoveEvent(bool isDrawing)
        {
            if (isDrawing == false)
            {
                // 删除事件处理程序
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

        // 外部调用的函数，初始PictureBox pictureBoxIn这两个变量
        public void angleAddOperation(PictureBox pictureBoxIn)
        {
            pictureBox1 = pictureBoxIn;
        }

        // 初始化所有声明的变量
        public void Init()
        {
            start_angle = new System.Drawing.Point(0, 0);
            middle_angle = new System.Drawing.Point(0, 0);
            end_angle = new System.Drawing.Point(0, 0);
            ansLocation = new System.Drawing.Point(0, 0);
            tmp = -1;
            degreeAngle = 0;
            tmpImage = GetImage();
        }
    }
}