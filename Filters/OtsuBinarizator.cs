using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Filters
{
    public static class OtsuBinarizator
    {
        private static Bitmap TransferToGreyShades(Bitmap sourceImage)
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

        public static Bitmap ToBlackWhite(this Bitmap src)
        {
            src = TransferToGreyShades(src);
            int treshold = GetOtsuTreshold(src);
            var dst = new Bitmap(src.Width, src.Height);

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    Color color = src.GetPixel(i, j);
                    int average = (color.R + color.B + color.G) / 3;
                    dst.SetPixel(i, j, average < treshold ? Color.Black : Color.White);
                }
            }

            return dst;
        }

        public static int GetOtsuTreshold(Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            
            int numPixels = bmpData.Stride * bmp.Height;
            byte[] pixels = new byte[numPixels];

            Marshal.Copy(bmpData.Scan0, pixels, 0, numPixels);

            //for (int counter = 0; counter < pixels.Length; counter += 3)
            //{
            //    int h = 0xFF & pixels[counter];
            //    histogram[h]++;
            //}

            int min = pixels[0], max = pixels[0];
            int temp;
            for (int i = 0; i < pixels.Length; i += 3)
            {
                temp = pixels[i];
                if (temp < min) min = temp;
                if (temp > max) max = temp;
            }

            byte[] histogram = new byte[max - min + 1];
            
            for (int i = 0; i < pixels.Length; i+=3)
                histogram[pixels[i] - min]++;

            float sum = 0;
            for (int t = 0; t < 256; t++) sum += t * histogram[t];

            float sumB = 0;
            int wB = 0;
            int wF = 0;

            float varMax = 0;
            int threshold = 0;

            for (int t = 0; t < 256; t++)
            {
                wB += histogram[t];               // Weight Background
                if (wB == 0) continue;

                wF = numPixels - wB;                 // Weight Foreground
                if (wF == 0) break;

                sumB += (float)(t * histogram[t]);

                float mB = sumB / wB;            // Mean Background
                float mF = (sum - sumB) / wF;    // Mean Foreground

                // Calculate Between Class Variance
                float varBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                // Check if new maximum found
                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = t;
                }
            }
            
            bmp.UnlockBits(bmpData);

            return threshold;
        }
    }
}