using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace Homography
{
    public static class Homograph
    {
        public static Bitmap Rectification(Bitmap srcBitmap, PointF[] srcPoints)
        {
            srcBitmap = RotateImage(srcBitmap, srcPoints);

            var point21 = new PointF(srcPoints[0].X, srcPoints[0].Y);
            var point22 = new PointF(srcPoints[1].X, srcPoints[1].Y);
            var point23 = new PointF(point22.X, srcPoints[0].Y + (float)((srcPoints[1].X - srcPoints[0].X) / 4.85));
            var point24 = new PointF(srcPoints[0].X, point23.Y);

            var destPoints = new[] { point21, point22, point23, point24 };

            var resultBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height);
            var matrix = Homograph.GetHMatrix(srcPoints, destPoints);

            for (var i = (int)srcPoints[0].X; i < (int)srcPoints[2].X; i++)
            {
                for (var j = (int)srcPoints[0].Y; j < (int)srcPoints[2].Y; j++)
                {
                    resultBitmap.SetPixel(i - (int)srcPoints[0].X, j - (int)srcPoints[0].Y, srcBitmap.GetPixel(i, j));

                    //var point = new Point(i, j);
                    //var newPoint = MultiplyOnMatrix(point, matrix);
                    //resultBitmap.SetPixel(Math.Abs(newPoint.X) - (int)srcPoints[0].X, Math.Abs(newPoint.Y) - (int)srcPoints[0].Y, srcBitmap.GetPixel(i, j));
                }
            }

            return resultBitmap;
        }

        public static Bitmap RotateImage(Bitmap srcBitmap, PointF[] srcPoints)
        {
            var angle = Math.Atan((srcPoints[1].Y - srcPoints[0].Y) / (srcPoints[1].X - srcPoints[0].X));
            angle *= -57.2957795;

            Bitmap returnBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform(srcPoints[0].X, srcPoints[0].Y);
                //rotate
                g.RotateTransform((float)angle);
                //move image back
                g.TranslateTransform(-srcPoints[0].X, -srcPoints[0].Y);
                //draw passed in image onto graphics object
                g.DrawImage(srcBitmap, new Point(0, 0));
            }

            //var horisontalDifference = Math.Sqrt(Math.Pow(srcPoints[1].X - srcPoints[0].X, 2) +
            //                                  Math.Pow(srcPoints[1].Y - srcPoints[0].Y, 2));

            //srcPoints[2].X = srcPoints[0].X + (float)horisontalDifference;
            //srcPoints[2].Y -= srcPoints[1].Y - srcPoints[0].Y;

            return returnBitmap;
        }

        private static RectangleF FindMinRectangle(PointF[] srcPoints)
        {
            if (srcPoints == null || srcPoints.Count() == 0)
            {
                return new RectangleF();
            }

            var maxLeftPoint = srcPoints.First();
            var maxRightPoint = srcPoints.Last();

            foreach (var srcPoint in srcPoints)
            {
                if (srcPoint.X < maxLeftPoint.X)
                {
                    maxLeftPoint.X = srcPoint.X;
                }

                if (srcPoint.Y < maxLeftPoint.Y)
                {
                    maxLeftPoint.Y = srcPoint.Y;
                }

                if (srcPoint.X > maxRightPoint.X)
                {
                    maxRightPoint.X = srcPoint.X;
                }

                if (srcPoint.Y > maxRightPoint.Y)
                {
                    maxRightPoint.Y = srcPoint.Y;
                }
            }

            return new RectangleF(maxLeftPoint.X, maxLeftPoint.Y, maxRightPoint.X - maxLeftPoint.X, maxRightPoint.Y - maxLeftPoint.Y);
        }

        public static Point MultiplyOnMatrix(Point srcPoint, double[,] matrix)
        {
            var resultVector = new double[3];
            int[] vector = { srcPoint.X, srcPoint.Y, 1 };

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    resultVector[i] += matrix[i, j] * vector[j];
                }
            }

            return new Point((int)resultVector[0], (int)resultVector[1]);
        }

        public static double[,] GetHMatrix(PointF[] srcPoints, PointF[] destPoints)
        {
            var matrix = new Matrix<Double>(3, 3);
            CvInvoke.FindHomography(srcPoints, destPoints, matrix, HomographyMethod.Default);

            var result = new double[3, 3];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }

            return result;
        }

        static double[] Gaus(double[,] a)
        {
            int n = 9;

            double[] x = new double[a.Length / n];
            for (int z = 0; z < a.Length / n; z++)
            {
                x[z] = a[z, n - 1];
            }

            double m;
            for (int k = 1; k < a.Length / n; k++)
            {
                for (int j = k; j < a.Length / n; j++)
                {
                    m = a[j, k - 1] / a[k - 1, k - 1] == 0 ? 1 : a[k - 1, k - 1];
                    for (int i = 0; i < n; i++)
                    {
                        a[j, i] = a[j, i] - m * a[k - 1, i];
                    }
                    x[j] = x[j] - m * x[k - 1];
                }
            }

            for (int i = a.Length / n - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < a.Length / n; j++)
                    x[i] -= a[i, j] * x[j];
                x[i] = x[i] / a[i, i] == 0 ? 1 : a[i, i];
            }

            return x;
        }

        public static double[,] GetEquationSystem(List<Point> source, List<Point> dest)
        {
            return new double[,]
            {
                {source[0].X, source[0].Y, 1, 1, 1, 1, -source[0].X * dest[0].X, -source[0].Y * dest[0].X, dest[0].X},
                {1, 1, 1, source[0].X, source[0].Y, 1, -source[0].X * dest[0].Y, -source[0].Y * dest[0].Y, dest[0].Y},
                {source[1].X, source[1].Y, 1, 1, 1, 1, -source[1].X * dest[1].X, -source[1].Y * dest[1].X, dest[1].X},
                {1, 1, 1, source[1].X, source[1].Y, 1, -source[1].X * dest[1].Y, -source[1].Y * dest[1].Y, dest[1].Y},
                {source[2].X, source[2].Y, 1, 1, 1, 1, -source[2].X * dest[2].X, -source[2].Y * dest[2].X, dest[2].X},
                {1, 1, 1, source[2].X, source[2].Y, 1, -source[2].X * dest[2].Y, -source[2].Y * dest[2].Y, dest[2].Y},
                {source[3].X, source[3].Y, 1, 1, 1, 1, -source[3].X * dest[3].X, -source[3].Y * dest[3].X, dest[3].X},
                {1, 1, 1, source[3].X, source[3].Y, 1, -source[3].X * dest[3].Y, -source[3].Y * dest[3].Y, dest[3].Y},
            };
        }
    }
}