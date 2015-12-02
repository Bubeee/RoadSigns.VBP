using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SurfaceHandling;
using Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using SurfaceHandling.Draw;
using SurfaceHandling.Info;
using Point = SurfaceHandling.Point;

namespace DesktopUI
{
    public partial class ImageRecognitor : Form
    {
        private Bitmap _currentImage;

        public ImageRecognitor()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         InitialDirectory = "C:\\",
                                         Filter = "PNG (*.png*)|*.png|JPEG (*.jpg)|*.jpg",
                                         FilterIndex = 2,
                                         RestoreDirectory = true
                                     };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream imageStream;
                using (imageStream = openFileDialog.OpenFile())
                {
                    this._currentImage = new Bitmap(imageStream);

                    var miniature = new Bitmap(sourcePictureBox.Width, sourcePictureBox.Height);
                    using (var g = Graphics.FromImage(miniature))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(this._currentImage, 0, 0, miniature.Width, miniature.Height);
                    }

                    sourcePictureBox.Image = miniature;
                }
            }
        }


        private void haussToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            resultPictureBox.Image = this._currentImage.ConvolutionFilter(GaussianFilter.CalculateMatrix(3, 9.25));
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            //resultPictureBox.Image = this._currentImage.ToBlackWhite();

            //this._currentImage = (Bitmap)Bitmap.FromFile(@"D:\my_programs\9_sem\MISOI\MISOI\MISOI\bin\Debug\pic.bmp");
            sourcePictureBox.Image = this._currentImage;

            var processedPicture = FilterMatrix.GetMatrixBrightness(this._currentImage);

            if (gausCheckBox.Checked)
            {
                // нужно подставить правильную матрицу и будет ок
                var filter = new double[][]{                         
                            new double []{0.000789,0.006581,0.013347,0.006581,0.000789},
                            new double []{0.006581,0.054901,0.111345,0.054901,0.006581},
                            new double []{0.013347,0.111345,0.225821,0.111345,0.013347},
                            new double []{0.006581,0.054901,0.111345,0.054901,0.006581},
                            new double []{0.000789,0.006581,0.013347,0.006581,0.000789},
                         };
                processedPicture = FilterMatrix.ApplyFilter(processedPicture, filter);
            }

            if (medianCheckBox.Checked)
            {
                processedPicture = FilterMatrix.ApplyMediane(processedPicture, Convert.ToInt32(medianNumeric.Value));
            }

            if (clarityСheckBox.Checked)
            {
                // нужно подставить правильную матрицу и будет ок
                var filter = new double[][]{                         
                            new double []{-1,-1,-1},
                            new double []{-1,9,-1},
                            new double []{-1,-1,-1}
                         };
                processedPicture = FilterMatrix.ApplyFilter(processedPicture, filter);
            }

            if (SobelCheckBox.Checked)
            {
                processedPicture = FilterMatrix.ApplySobel(processedPicture);
            }

            //if (BinarizeCheckBox.Checked)
            //{
            //    float threshold = (float)binarizeNumericUpDown.Value;
            //    processedPicture = FilterMatrix.ApplyBinarize(processedPicture, threshold);
            //}

            if (BinarizeCheckBox.Checked)
            {
                float threshold = (float)binarizeNumericUpDown.Value;
                processedPicture = FilterMatrix.ApplyAdaptiveBinarize(processedPicture, threshold);
            }



            resultPictureBox.Image = FilterMatrix.GetBmp(processedPicture);
        }

        private ImgInfo _info;
        private Dictionary<int, PixelClass> _classes;
        private Bitmap _myMap;
        private void button2_Click(object sender, EventArgs e)
        {
            var processedPicture = FilterMatrix.GetMatrixBrightness(_currentImage);
            processedPicture = FilterMatrix.ApplyBinarize(processedPicture, 125);

            int ni = processedPicture.Length;
            int nj = processedPicture[0].Length;
            int[,] processedPictureInt = new int[ni, nj];
            for (int i = 0; i < processedPicture.Length; i++)
            {
                for (int j = 0; j < nj; j++)
                {
                    processedPictureInt[i, j] = (int)processedPicture[i][j];
                }
            }
            var labeler = new SurfaceLabeler();

            Task<ImgInfo>.Factory.StartNew(() => labeler.Labeling(processedPictureInt, _currentImage)).ContinueWith(x =>
            {
                _info = x.Result;
                resultPictureBox.Image = x.Result.NewPicture;
                _myMap = x.Result.NewPicture;

            },
            TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Labeling(125);
        }




        public void LabelingForce(int bin)
        {

            var labeler = new SurfaceLabeler(255);

            var listinf = new List<ImgInfo>();

            Task<List<ImgInfo>>.Factory.StartNew(
                () =>
                {
                    var result = new List<ImgInfo>();
                    for (int i = 100; i < 200; i += 10)
                        result.Add(labeler.Labeling(Binar(i), _currentImage));
                    return result;
                }
                ).ContinueWith(x =>
            {
                _info = x.Result.OrderByDescending(y => y.Classes.Values.Count()).First();




                _myMap = _info.NewPicture;
            },
            TaskScheduler.FromCurrentSynchronizationContext());
        }


        public int[,] Binar(int bin)
        {
            var processedPicture = FilterMatrix.GetMatrixBrightness(_currentImage);
            processedPicture = FilterMatrix.ApplyBinarize(processedPicture, bin);

            int ni = processedPicture.Length;
            int nj = processedPicture[0].Length;
            int[,] processedPictureInt = new int[ni, nj];
            for (int i = 0; i < processedPicture.Length; i++)
            {
                for (int j = 0; j < nj; j++)
                {
                    processedPictureInt[i, j] = (int)processedPicture[i][j];
                }
            }
            return processedPictureInt;
        }

        public void Labeling(int bin)
        {
            var processedPicture = FilterMatrix.GetMatrixBrightness(_currentImage);
            processedPicture = FilterMatrix.ApplyBinarize(processedPicture, bin);

            int ni = processedPicture.Length;
            int nj = processedPicture[0].Length;
            int[,] processedPictureInt = new int[ni, nj];
            for (int i = 0; i < processedPicture.Length; i++)
            {
                for (int j = 0; j < nj; j++)
                {
                    processedPictureInt[i, j] = (int)processedPicture[i][j];
                }
            }
            var labeler = new SurfaceLabeler(255);

            Task<ImgInfo>.Factory.StartNew(() => labeler.Labeling(processedPictureInt, _currentImage)).ContinueWith(x =>
            {
                _info = x.Result;
                if (_info.OldPicture == _info.NewPicture || (_info.Classes.Values.Any() && _info.Classes.Values.Count < 4))
                {
                    if (bin + 5 < 255)
                        Labeling(bin + 5);
                }
                else
                {
                    resultPictureBox.Image = x.Result.NewPicture;
                }
                _myMap = x.Result.NewPicture;
            },
            TaskScheduler.FromCurrentSynchronizationContext());
        }



        public void GetLines(Dictionary<int, PixelClass> classes)
        {
            /**SurfaceLabeler labeler = new SurfaceLabeler();
            foreach (var v in classes)
            {
                v.Value.LineList.Add(labeler.GetLineFunc(v.Value.Angles.Top, v.Value.Angles.Bottom));
                v.Value.LineList.Add(labeler.GetLineFunc(v.Value.Angles.Bottom, v.Value.Angles.Right));
                v.Value.LineList.Add(labeler.GetLineFunc(v.Value.Angles.Right, v.Value.Angles.Right));
                v.Value.LineList.Add(labeler.GetLineFunc(v.Value.Angles.Bottom, v.Value.Angles.Right));
                
            }**/
        }

        public bool DrawLine(Bitmap bitmap, Line line)
        {
            var min = Math.Min(line.Right, bitmap.Width);
            for (int i = line.Left; i < min; i++)
            {
                var h = line.Func(i);
                if (h < bitmap.Height && h >= 0)
                    bitmap.SetPixel(i, line.Func(i), Color.Red);
            }

            return false;
        }

        private void resultPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Draw.Noise(_currentImage);
            resultPictureBox.Image = _currentImage;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            float sum = 0;
            for (int i = 1; i < _currentImage.Width; i++)
            {
                for (int j = 0; j < _currentImage.Height; j++)
                {

                    sum += Math.Abs(_currentImage.GetPixel(i, j).GetBrightness() - _currentImage.GetPixel(i - 1, j).GetBrightness());
                }
            }

            if (sum > 30000)
            {
                var filter = new double[][]
                {
                    new double[] {0.000789, 0.006581, 0.013347, 0.006581, 0.000789},
                    new double[] {0.006581, 0.054901, 0.111345, 0.054901, 0.006581},
                    new double[] {0.013347, 0.111345, 0.225821, 0.111345, 0.013347},
                    new double[] {0.006581, 0.054901, 0.111345, 0.054901, 0.006581},
                    new double[] {0.000789, 0.006581, 0.013347, 0.006581, 0.000789},
                };
                var processedPicture = FilterMatrix.GetMatrixBrightness(this._currentImage);

                processedPicture = FilterMatrix.ApplyFilter(processedPicture, filter);

                _currentImage = FilterMatrix.GetBmp(processedPicture);
            }

            resultPictureBox.Image = _currentImage;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            LabelingForce(100);
        }

        private void RotateButton_Click(object sender, EventArgs e)
        {
            var point11 = new Point(74, 66);
            var point12 = new Point(233, 31);
            var point13 = new Point(251, 192);
            var point14 = new Point(39, 173);
            var point21 = new Point(0, 0);
            var point22 = new Point(110, 0);
            var point23 = new Point(110, 90);
            var point24 = new Point(0, 90);

            var scrPoints = new List<Point> { point11, point12, point13, point14 };
            var destPoints = new List<Point> { point21, point22, point23, point24 };

            var resultBitmap = new Bitmap(_currentImage.Width, _currentImage.Height);

            for (var i = 0; i < _currentImage.Width; i++)
            {
                for (var j = 0; j < _currentImage.Height; j++)
                {
                    var point = new Point(i, j);
                    var newPoint = MultiplyOnMatrix(point, GetHMatrix(scrPoints, destPoints));
                    resultBitmap.SetPixel(newPoint.X, newPoint.Y, _currentImage.GetPixel(i, j));
                }
            }

            resultPictureBox.Image = resultBitmap;
        }

        private static Point MultiplyOnMatrix(Point srcPoint, int[,] matrix)
        {
            var resultVector = new int[3];
            int[] vector = { srcPoint.X, srcPoint.Y, 1 };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    resultVector[i] += matrix[i, j] * vector[j];
                }
            }

            return new Point(resultVector[0], resultVector[1]);
        }

        private static int[,] GetHMatrix(List<Point> srcPoints, List<Point> destPoints)
        {
            var matrix = GetTransfromMatrix(srcPoints, destPoints);
            var result = new int[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = (int)matrix[i][j];
                }
            }

            return result;
        }

        public static double[][] GetTransfromMatrix(List<Point> image, List<Point> carNumber)
        {
            var matr = new double[8, 9];
            for (int i = 0; i < 4; i++)
            {
                matr[i, 0] = image[i].X;
                matr[i, 1] = image[i].Y;
                matr[i, 2] = 1;
                matr[i, 3] = 0;
                matr[i, 4] = 0;
                matr[i, 5] = 0;
                matr[i, 6] = -image[i].X * carNumber[i].X;
                matr[i, 7] = -image[i].Y * carNumber[i].X;
                matr[i, 8] = carNumber[i].X;
            }

            for (int i = 4; i < 8; i++)
            {
                matr[i, 0] = 0;
                matr[i, 1] = 0;
                matr[i, 2] = 0;
                matr[i, 3] = image[i - 4].X;
                matr[i, 4] = image[i - 4].Y;
                matr[i, 5] = 1;
                matr[i, 6] = image[i - 4].X * carNumber[i - 4].X;
                matr[i, 7] = image[i - 4].Y * carNumber[i - 4].X;
                matr[i, 8] = carNumber[i - 4].X;
            }

            var x = Gaus(matr);
            var d = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                d[i] = new double[3];
                for (int j = 0; j < 3; j++)
                {
                    d[i][i] = x[i + j];
                }
            }
            d[2][2] = 1;
            return d;
        }

        static double[] Gaus(double[,] matr)
        {
            double[,] a = matr;
            int n = 9;
            //for (int i = 0; i < a.Length / n; i++)
            //{
            //    for (int j = 0; j < n; j++)
            //    {
            //        Console.Write(a[i, j].ToString() + "x" + j.ToString() + " ");
            //    }
            //    Console.Write("= " + a[i, n - 1].ToString());
            //    Console.WriteLine("");
            //}
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
                    m = a[j, k - 1] / a[k - 1, k - 1];
                    for (int i = 0; i < n; i++)
                    {
                        a[j, i] = a[j, i] - m * a[k - 1, i];
                    }
                    x[j] = x[j] - m * x[k - 1];
                }
            }

            for (int i = a.Length / n - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < a.Length / n; j++) x[i] -= a[i, j] * x[j];
                x[i] = x[i] / a[i, i];
            }

            return x;
        }
    }
}