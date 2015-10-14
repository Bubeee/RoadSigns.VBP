using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    public static class FilterMatrix
    {
        /// <summary>
        /// В получившейся матрице для получения яркости обращаться [x][y], т.е. х - ширина, y - высота
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static double[][] GetMatrixBrightness(Bitmap bmp)
        {
            double[][] result = new double[bmp.Width][];
            for (int i = 0; i < bmp.Width; i++)
            {
                result[i] = new double[bmp.Height];
                for (int j = 0; j < bmp.Height; j++)
                {
                    result[i][j] = bmp.GetPixel(i, j).GetBrightness() * 255;
                }
            }
            return result;
        }

        public static double[][] ApplyFilter(double[][] matrix, double[][] filter)
        {
            double[][] rightFilter = TurnFilter(filter);

            int filterWide = rightFilter.Length;
            int pixelWide = (filterWide - 1) / 2;

            double[][] widerMatrix = MakeWiderMirror(matrix, pixelWide);


            int width = widerMatrix.Length - pixelWide - pixelWide;
            int height = widerMatrix[0].Length - pixelWide - pixelWide;
            double[][] result = new double[width][];

            for (int i = 0; i < width; i++)
            {
                result[i] = new double[height];
            }

            for (int x = pixelWide; x < pixelWide + width; x++)
            {
                for (int y = pixelWide; y < pixelWide + height; y++)
                {
                    double pixelResult = 0;
                    for (int i = 0; i < filterWide; i++)
                    {
                        for (int j = 0; j < filterWide; j++)
                        {
                            pixelResult += widerMatrix[x + i - pixelWide][y + j - pixelWide] * rightFilter[i][j];
                        }
                    }
                    result[x - pixelWide][y - pixelWide] = pixelResult;
                }
            }
            return result;
        }

        public static double[][] ApplySobel(double[][] matrix)
        {
            var width = matrix.Length;
            var height = matrix[0].Length;

            var xFilter = new double[][]{                         
                            new double []{-1,0,1},
                            new double []{-2,0,2},
                            new double []{-1,0,1}
                         };

            var yFilter = new double[][]{                         
                            new double []{-1,-2,-1},
                            new double []{0,0,0},
                            new double []{1,2,1}
                         };

            double[][] xGradientMatrix = ApplyFilter(matrix, xFilter);
            double[][] yGradientMatrix = ApplyFilter(matrix, yFilter);
            double[][] result = new double[width][];

            for (int i = 0; i < width; i++)
            {
                result[i] = new double[height];
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    result[i][j] = Math.Sqrt(xGradientMatrix[i][j] * xGradientMatrix[i][j] + yGradientMatrix[i][j] * yGradientMatrix[i][j]);
                }
            }
            return result;
        }
        public static double[][] ApplyBinarize(double[][] matrix, float threshold)
        {
            var width = matrix.Length;
            var height = matrix[0].Length;

            double[][] result = new double[width][];

            for (int i = 0; i < width; i++)
            {
                result[i] = new double[height];
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (matrix[i][j] > threshold)
                    {
                        result[i][j] = 255;
                    }
                    else
                    {
                        result[i][j] = 0;
                    }
                }
            }
            return result;
        }

        private static double[][] TurnFilter(double[][] filter)
        {
            double[][] result = new double[filter.Length][];
            for (int i = 0; i < filter.Length; i++)
            {
                result[i] = new double[filter.Length];
            }

            for (int i = 0; i < filter.Length; i++)
            {
                for (int j = 0; j < filter.Length; j++)
                {
                    result[i][j] = filter[j][i];
                }
            }

            return result;
        }

        private static double[][] MakeWiderMirror(double[][] bmp, int pixelWide)
        {
            int bmpWidth = bmp.Length;
            int bmpHeight = bmp[0].Length;
            int doublePixelWide = pixelWide * 2;
            double[][] widerBmp = new double[bmpWidth + doublePixelWide][];

            for (int i = 0; i < bmpWidth + doublePixelWide; i++)
            {
                widerBmp[i] = new double[bmpHeight + doublePixelWide];
            }

            // скопировал основную чать
            for (int i = 0; i < bmpWidth; i++)
            {
                for (int j = 0; j < bmpHeight; j++)
                {
                    widerBmp[i + pixelWide][j + pixelWide] = bmp[i][j];
                }
            }

            //отражение верхней и нижней границ
            for (int i = pixelWide; i < bmpWidth + pixelWide; i++)
            {
                for (int j = 0; j < pixelWide; j++)
                {
                    widerBmp[i][pixelWide - 1 - j] = widerBmp[i][pixelWide + j];
                    widerBmp[i][bmpHeight + pixelWide + j] = widerBmp[i][bmpHeight + pixelWide - j - 1];
                }
            }

            //отражение левой и правой границ
            for (int i = 0; i < pixelWide; i++)
            {
                for (int j = 0; j < bmpHeight + doublePixelWide; j++)
                {
                    widerBmp[pixelWide - 1 - i][j] = widerBmp[pixelWide + i][j];
                    widerBmp[bmpWidth + pixelWide + i][j] = widerBmp[bmpWidth + pixelWide - i - 1][j];
                }
            }

            return widerBmp;
        }

        public static Bitmap GetBmp(double[][] matrix)
        {
            Bitmap result = new Bitmap(matrix.Length, matrix[0].Length);

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    byte brightnessByte = (byte)( matrix[i][j]);
                    result.SetPixel(i, j, Color.FromArgb(brightnessByte, brightnessByte, brightnessByte));
                }
            }
            return result;
        }

        /// <summary>
        /// filterWide Должно быть нечетным
        /// </summary>
        /// <param name="widerMatrix"></param>
        /// <param name="filterWide">Должно быть нечетным</param>
        public static double[][] ApplyMediane(double[][] matrix, int filterWide)
        {
            int pixelWide = (filterWide - 1) / 2;
            int width = matrix.Length;
            int height = matrix[0].Length;
            double[][] result = new double[width][];

            for (int i = 0; i < width; i++)
            {
                result[i] = new double[height];
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double pixelResult = 0;
                    List<double> pixels = new List<double>();
                    for (int i = 0; i < filterWide; i++)
                    {
                        for (int j = 0; j < filterWide; j++)
                        {
                            if ((x + i - pixelWide >= 0) && (x + i - pixelWide < matrix.Length) && (y + j - pixelWide >= 0) && (y + j - pixelWide < matrix[0].Length))
                            {
                                pixels.Add(matrix[x + i - pixelWide][y + j - pixelWide]);
                            }
                        }
                    }
                    pixels.Sort();
                    result[x][y] = pixels[pixels.Count / 2 + 1];
                }
            }

            return result;
        }

    }

}
