using System.Drawing.Imaging;

namespace Screen_Shot_Image_List
{
    class ScreenShotImageList
    {
        public void ScreenShotImageOperation(Image image, ListView listView, PictureBox pictureBox)
        {
            if (listView.View != View.LargeIcon) // 如果ListView的View属性不是LargeIcon，就设置为LargeIcon
            {
                listView.View = View.LargeIcon;
            }

            if (listView.LargeImageList == null) // 如果大图标列表为null，就新建一个LargeImageList对象
            {
                listView.LargeImageList = new ImageList();
            }

            listView.LargeImageList.ImageSize = new Size(230, 130); // 设置ImageList的ImageSize属性

            // 生成时间戳文件名
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");


            // 获取项目的根目录路径（即包含.exe文件的目录）
            string directoryPath = Path.Combine(Application.StartupPath, "ImageSave");
            string filePath = Path.Combine(directoryPath, timestamp + ".png");

            // 创建文件夹（如果不存在）
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // 创建带有时间戳的位图
            Bitmap stampedBitmap = new Bitmap(image);
            using (Graphics graphics = Graphics.FromImage(stampedBitmap))
            {
                string displayTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Font font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold); // 设置字体为粗体
                Brush brush = new SolidBrush(Color.Red); // 设置字体颜色为红色

                // 计算时间戳的位置
                SizeF textSize = graphics.MeasureString(displayTimestamp, font);
                float x = (stampedBitmap.Width - textSize.Width) / 2;
                float y = stampedBitmap.Height - textSize.Height - 5;

                graphics.DrawString(displayTimestamp, font, brush, new PointF(x, y));
                graphics.Save();
            }

            // 保存带有时间戳的图像
            try
            {
                stampedBitmap.Save(filePath, ImageFormat.Png);
                //ssageBox.Show($"图像已保存到 {filePath}", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //ssageBox.Show($"保存图像失败: {ex.Message}", "保存错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 调整原图像大小为1920x1020并保持比例
            Image resizedImage = ResizeImage(image, new Size(1920, 1020));

            // 将调整后的图像添加到ListView的大图标列表中
            listView.LargeImageList.Images.Add(resizedImage);
            ListViewItem item = new ListViewItem
            {
                ImageIndex = listView.LargeImageList.Images.Count - 1, // 设置该项在大图标列表中的索引
                Text = timestamp, // 设置项的文本为时间戳
                Tag = filePath // 将文件路径存储在ListViewItem的Tag属性中
            };
            listView.Items.Add(item); // 将该项添加到ListView中
        }

        // 将原始图片缩放到指定大小并保持比例
        private Image ResizeImage(Image image, Size size)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            float ratioX = (float)size.Width / originalWidth;
            float ratioY = (float)size.Height / originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            Bitmap result = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return result;
        }
    }
}
