namespace corrected_Parameter_Change
{
    internal class correctedParameterChange
    {
        //获取传入图片的长宽，与picturebox1的长宽比较，确定压缩参数
        public double parameter(Image image, int pictureboxWidth, int pictureboxHeight)
        {
            if (image.Width / image.Height >= pictureboxWidth / pictureboxHeight)
            {
                //return pictureboxWidth / image.Width;
                return 1;
            }
            else
            {
                //return pictureboxHeight / image.Height;
                return 1;
            }
        }
    }
}
