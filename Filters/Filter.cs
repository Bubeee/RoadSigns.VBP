using System.Drawing;

namespace Filters
{
    public static class Filter
    {
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

        public static Bitmap BitmapToBlackWhite(Bitmap src, int treshold)
        {
            // 1.
            //double treshold = 0.5;

            Bitmap dst = new Bitmap(src.Width, src.Height);

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    // 1.
                    //dst.SetPixel(i, j, src.GetPixel(i, j).GetBrightness() < treshold ? Color.Black : Color.White);

                     //2 
                    Color color = src.GetPixel(i, j);
                    int average = (color.R + color.B + color.G) / 3;
                    dst.SetPixel(i, j, average < treshold ? System.Drawing.Color.Black : System.Drawing.Color.White);
                }
            }

            return dst;
        }
    }
}