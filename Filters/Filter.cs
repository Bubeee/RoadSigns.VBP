using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Filters
{
    public static class Filter
    {
        // transfers image to 255 shades of grey =)
        public static Bitmap TransferToGreyShades(Bitmap sourceImage)
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

        // changes the image applying Sobel' Filter (not really fast)
        public static Bitmap ApplySobelFilter(Bitmap source)
        {
            Bitmap b = source;
            Bitmap bb = new Bitmap(source);
            int width = b.Width;
            int height = b.Height;
            int[,] gx = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            const int Limit = 128 * 128;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = b.GetPixel(i, j).R;
                    allPixG[i, j] = b.GetPixel(i, j).G;
                    allPixB[i, j] = b.GetPixel(i, j).B;
                }
            }

            int new_rx, new_ry;
            int new_gx, new_gy;
            int new_bx, new_by;
            int rc, gc, bc;

            for (int i = 1; i < b.Width - 1; i++)
            {
                for (int j = 1; j < b.Height - 1; j++)
                {
                    new_rx = 0;
                    new_ry = 0;
                    new_gx = 0;
                    new_gy = 0;
                    new_bx = 0;
                    new_by = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > Limit 
                        || new_gx * new_gx + new_gy * new_gy > Limit
                        || new_bx * new_bx + new_by * new_by > Limit)
                    {
                        bb.SetPixel(i, j, Color.Black);
                    }             //bb.SetPixel (i, j, Color.FromArgb(allPixR[i,j],allPixG[i,j],allPixB[i,j]));
                    else
                    {
                        bb.SetPixel(i, j, Color.Transparent);
                    }
                }
            }
            return bb;
        }

        // transfers image to black and white with Single treshold method
        public static Bitmap ToBlackWhite(Bitmap src, int treshold)
        {
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
        
        // detects the edges with gradient method (fast!)
        public static void GradientEdgeDetection(Bitmap sourceImage, float threshold)
        {
            Bitmap sourceImageClone = (Bitmap)sourceImage.Clone();

            BitmapData bmData = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = sourceImageClone.LockBits(new Rectangle(0, 0, sourceImageClone.Width, sourceImageClone.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;

            unsafe
            {
                byte* pointerDest = (byte*)(void*)bmData.Scan0;
                byte* pointerSrc = (byte*)(void*)bmSrc.Scan0;

                int nOffset = stride - sourceImage.Width * 3;
                int nWidth = sourceImage.Width - 1;
                int nHeight = sourceImage.Height - 1;

                for (var y = 0; y < nHeight; ++y)
                {
                    for (var x = 0; x < nWidth; ++x)
                    {
                        var p0 = ToGray(pointerSrc);
                        var p1 = ToGray(pointerSrc + 3);
                        var p2 = ToGray(pointerSrc + 3 + stride);

                        if (Math.Abs(p1 - p2) + Math.Abs(p1 - p0) > threshold)
                            pointerDest[0] = pointerDest[1] = pointerDest[2] = 255;
                        else
                            pointerDest[0] = pointerDest[1] = pointerDest[2] = 0;

                        pointerDest += 3;
                        pointerSrc += 3;
                    }
                    pointerDest += nOffset;
                    pointerSrc += nOffset;
                }
            }

            sourceImage.UnlockBits(bmData);
            sourceImageClone.UnlockBits(bmSrc);
        }

        // transfers single pixel tp gray color
        private static unsafe float ToGray(byte* bgr)
        {
            return bgr[2] * 0.3f + bgr[1] * 0.59f + bgr[0] * 0.11f;
        }
    }
}