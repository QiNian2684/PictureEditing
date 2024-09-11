using System.Drawing.Imaging;

namespace Gray_Scale
{
    internal class GrayScale
    {
        // 将图像转换为灰度图像
        public unsafe Image ConvertToGrayscale(Bitmap original) // 修改为 public
        {
            Bitmap grayscaleBitmap = new Bitmap(original.Width, original.Height, original.PixelFormat);

            // 锁定原始图像的位图数据
            BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, original.PixelFormat);
            // 锁定灰度图像的位图数据
            BitmapData grayscaleData = grayscaleBitmap.LockBits(new Rectangle(0, 0, grayscaleBitmap.Width, grayscaleBitmap.Height), ImageLockMode.WriteOnly, grayscaleBitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(original.PixelFormat) / 8;
            int heightInPixels = originalData.Height;
            int widthInBytes = originalData.Width * bytesPerPixel;

            byte* originalScan0 = (byte*)originalData.Scan0;
            byte* grayscaleScan0 = (byte*)grayscaleData.Scan0;

            Parallel.For(0, heightInPixels, y =>
            {
                byte* originalRow = originalScan0 + (y * originalData.Stride);
                byte* grayscaleRow = grayscaleScan0 + (y * grayscaleData.Stride);

                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    byte b = originalRow[x];
                    byte g = originalRow[x + 1];
                    byte r = originalRow[x + 2];
                    byte a = bytesPerPixel == 4 ? originalRow[x + 3] : (byte)255;

                    int grayScale = (int)(r * 0.3 + g * 0.59 + b * 0.11);
                    grayscaleRow[x] = (byte)grayScale;
                    grayscaleRow[x + 1] = (byte)grayScale;
                    grayscaleRow[x + 2] = (byte)grayScale;
                    if (bytesPerPixel == 4)
                    {
                        grayscaleRow[x + 3] = a;
                    }
                }
            });

            // 解锁位图数据
            original.UnlockBits(originalData);
            grayscaleBitmap.UnlockBits(grayscaleData);

            return grayscaleBitmap;
        }
    }
}
