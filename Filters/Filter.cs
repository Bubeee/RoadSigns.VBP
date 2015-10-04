using System.Collections.Generic;
using System.Drawing;

namespace Filters
{
    public static class Filter
    {
        public static Bitmap Convultion(Bitmap source, double[,] kernel)
        {
            //Получаем байты изображения
            byte[] inputBytes = GetBytes(source);
            byte[] outputBytes = new byte[inputBytes.Length];

            int width = source.Width;
            int height = source.Height;

            int kernelWidth = kernel.GetLength(0);
            int kernelHeight = kernel.GetLength(1);

            //Производим вычисления
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double rSum = 0, gSum = 0, bSum = 0, kSum = 0;

                    for (int i = 0; i < kernelWidth; i++)
                    {
                        for (int j = 0; j < kernelHeight; j++)
                        {
                            int pixelPosX = x + (i - (kernelWidth / 2));
                            int pixelPosY = y + (j - (kernelHeight / 2));
                            if ((pixelPosX < 0) ||
                              (pixelPosX >= width) ||
                              (pixelPosY < 0) ||
                              (pixelPosY >= height)) continue;

                            byte r = inputBytes[3 * (width * pixelPosY + pixelPosX) + 0];
                            byte g = inputBytes[3 * (width * pixelPosY + pixelPosX) + 1];
                            byte b = inputBytes[3 * (width * pixelPosY + pixelPosX) + 2];

                            double kernelVal = kernel[i, j];

                            rSum += r * kernelVal;
                            gSum += g * kernelVal;
                            bSum += b * kernelVal;

                            kSum += kernelVal;
                        }
                    }

                    if (kSum <= 0) kSum = 1;

                    //Контролируем переполнения переменных
                    rSum /= kSum;
                    if (rSum < 0) rSum = 0;
                    if (rSum > 255) rSum = 255;

                    gSum /= kSum;
                    if (gSum < 0) gSum = 0;
                    if (gSum > 255) gSum = 255;

                    bSum /= kSum;
                    if (bSum < 0) bSum = 0;
                    if (bSum > 255) bSum = 255;

                    //Записываем значения в результирующее изображение
                    outputBytes[3 * (width * y + x) + 0] = (byte)rSum;
                    outputBytes[3 * (width * y + x) + 1] = (byte)gSum;
                    outputBytes[3 * (width * y + x) + 2] = (byte)bSum;
                }
            }
            //Возвращаем отфильтрованное изображение
            return BytesToBitmap(outputBytes, width, height);
        }

        public static Bitmap TransferToBlackAndWhite(Bitmap sourceImage)
        {
            var greyImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (var y = 0; y < sourceImage.Height; ++y)
            {
                for (var x = 0; x < sourceImage.Width; ++x)
                {
                    Color c = sourceImage.GetPixel(x, y);
                    var rgb = (byte)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
                    greyImage.SetPixel(x, y, Color.FromArgb(c.A, rgb, rgb, rgb));
                }
            }

            return greyImage;
        }

        private static byte[] GetBytes(Bitmap bitmap)
        {
            var result = new List<byte>();
            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    result.Add(bitmap.GetPixel(i, j).R);
                    result.Add(bitmap.GetPixel(i, j).G);
                    result.Add(bitmap.GetPixel(i, j).B);
                }
            }

            return result.ToArray();
        } 
        
        private static Bitmap BytesToBitmap(byte[] bytes, int width, int height)
        {
            var result = new Bitmap(width, height);
            for (var x = 0; x < bytes.Length; x += 3)
            {
                var y = x / width;
                result.SetPixel(x % width, y % height, Color.FromArgb(255, bytes[x], bytes[x + 1], bytes[x + 2]));
            }

            return result;
        }
    }
}