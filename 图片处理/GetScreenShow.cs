namespace Get_Screen_Show
{
    class GetScreenShow
    {
        private PictureBox pictureBox1;

        // 添加 PictureBox
        public void AddPictureBox(PictureBox pictureBoxIn)
        {
            if (pictureBoxIn == null)
            {
                throw new ArgumentNullException(nameof(pictureBoxIn), "图片槽不能为空！");
            }

            pictureBox1 = pictureBoxIn;
        }

        // 将 picturebox1 上的所有图层的图像合并到原先在 picturebox1 中显示的图像里中，并作为返回值返回
        public Image CaptureScreen()
        {
            if (pictureBox1 == null)
            {
                MessageBox.Show("系统未能找到显示区域，请稍后重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("当前没有图像可供截屏。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            try
            {
                Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.DrawToBitmap(image, pictureBox1.ClientRectangle);
                return image;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("操作过程中发生错误，请检查输入并重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("操作当前不可执行，请稍后重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("截屏过程中发生未知错误，请稍后重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}