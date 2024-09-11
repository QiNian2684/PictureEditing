using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;

namespace Picture_Change
{
    class PictureChange
    {
        public enum CHANGE_TYPE
        {
            LIGHT,
            CONTRAST,
            RED,
            GREEN,
            BLUE
        }
        public unsafe Image Picture_change_operate(CHANGE_TYPE ty, int value, Image currentImage)
        {
            if (currentImage != null)
            {
                // 创建一个新的Bitmap对象
                Bitmap result = new Bitmap(currentImage);
                // 加载图片
                Bitmap bitmap = new Bitmap(currentImage);
                // 将Bitmap对象转换为Mat对象
                Mat imgMat = BitmapConverter.ToMat(bitmap);
                switch (ty)
                {
                    case CHANGE_TYPE.LIGHT:
                        {
                            Mat outputMat = imgMat.Clone();
                            for (int y = 0; y < imgMat.Rows; y++)
                            {
                                for (int x = 0; x < imgMat.Cols; x++)
                                {
                                    Vec3b pixel = outputMat.Get<Vec3b>(y, x);
                                    for (int i = 0; i < 3; i++)
                                    {
                                        int newValue = pixel[i] + value;
                                        newValue = Math.Max(0, Math.Min(255, newValue)); // 限制在0到255之间
                                        pixel[i] = (byte)newValue;
                                    }
                                    outputMat.Set(y, x, pixel);
                                }
                            }

                            // 将Mat对象转换为Bitmap对象
                            Bitmap bmp1 = BitmapConverter.ToBitmap(outputMat);
                            result = bmp1;
                            break;
                        }
                    case CHANGE_TYPE.CONTRAST:
                        {
                            double alpha = (double)value / 50.0;
                            Mat outputMat = new Mat(imgMat.Size(), imgMat.Type());

                            int channels = imgMat.Channels();
                            int rows = imgMat.Rows;
                            int cols = imgMat.Cols;

                            byte* srcData = (byte*)imgMat.DataPointer;
                            byte* dstData = (byte*)outputMat.DataPointer;

                            int srcStep = (int)imgMat.Step();
                            int dstStep = (int)outputMat.Step();

                            for (int y = 0; y < rows; y++)
                            {
                                byte* srcRow = srcData + y * srcStep;
                                byte* dstRow = dstData + y * dstStep;

                                for (int x = 0; x < cols; x++)
                                {
                                    byte* srcPixel = srcRow + x * channels;
                                    byte* dstPixel = dstRow + x * channels;

                                    for (int c = 0; c < channels; c++)
                                    {
                                        dstPixel[c] = saturate(alpha * (double)srcPixel[c] + (1 - alpha) * 128);
                                    }
                                }
                            }

                            // 将Mat对象转换为Bitmap对象
                            Bitmap bmp1 = BitmapConverter.ToBitmap(outputMat);
                            result = bmp1;
                            break;
                        }
                    case CHANGE_TYPE.RED:
                    case CHANGE_TYPE.GREEN:
                    case CHANGE_TYPE.BLUE:
                        {
                            int color = 3;
                            if (ty == CHANGE_TYPE.RED)
                            {
                                color = 2;
                            }
                            if (ty == CHANGE_TYPE.GREEN)
                            {
                                color = 1;
                            }
                            if (ty == CHANGE_TYPE.BLUE)
                            {
                                color = 0;
                            }
                            // 锁定内存区域，以便直接访问像素数据
                            Rectangle rect = new Rectangle(0, 0, result.Width, result.Height);
                            BitmapData bmpData = result.LockBits(rect, ImageLockMode.ReadWrite, result.PixelFormat);

                            // 获取像素数据的起始位置
                            IntPtr ptr = bmpData.Scan0;

                            // 获取像素数据的步幅
                            int stride = bmpData.Stride;

                            // 获取像素的位深度
                            int bitsPerPixel = Image.GetPixelFormatSize(result.PixelFormat);

                            // 计算每个像素占用的字节数
                            int bytesPerPixel = bitsPerPixel / 8;

                            // 遍历每个像素
                            unsafe
                            {
                                byte* p = (byte*)(void*)ptr;

                                for (int y = 0; y < result.Height; y++)
                                {
                                    for (int x = 0; x < result.Width; x++)
                                    {
                                        // 计算当前像素的索引
                                        int index = y * stride + x * bytesPerPixel;

                                        // 将原来的通道值加上拖动条的值
                                        int newColorValue = p[index + color] + value;

                                        // 确保通道值在0-255之间
                                        newColorValue = Math.Max(0, Math.Min(255, newColorValue));

                                        // 将修改后的蓝色通道值设置回像素数据
                                        p[index + color] = (byte)newColorValue;
                                    }
                                }
                            }

                            // 解锁内存区域
                            result.UnlockBits(bmpData);
                            break;
                        }
                    default:
                        break;
                }
                return result;
            }
            else
                return null;
        }

        //数据控制
        private byte saturate(double x)
        {
            if (x > 255.0) return 255;
            else if (x < 0.0) return 0;
            else return (byte)x;
        }
    }
}