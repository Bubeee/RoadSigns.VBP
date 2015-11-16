using System.Drawing;
using System.IO;
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

        private void ThresholdInputTextChanged(object sender, System.EventArgs e)
        {
            int treshold;
            if (int.TryParse(thresholdInput.Text, out treshold))
            {
                //resultPictureBox.Image = Filter.ApplySobelFilter(Filter.BitmapToBlackWhite(this._currentImage, treshold)); 
                //resultPictureBox.Image = Filter.ApplySobelFilter(_currentImage); 

                //var img = new Bitmap(this._currentImage);
                //Filter.GradientEdgeDetection(img, treshold);
                //resultPictureBox.Image = img;
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
            int[,] processedPictureInt = new int[ni,nj];
            for (int i = 0; i< processedPicture.Length; i++  )
            {
                for(int j = 0; j< nj; j++)
                {
                    processedPictureInt[i,j] = (int)processedPicture[i][j];
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
                _info = x.Result.OrderByDescending(y=>y.Classes.Values.Count()).First();




                _myMap =_info.NewPicture;
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
                if (_info.OldPicture == _info.NewPicture || (_info.Classes.Values.Any() && _info.Classes.Values.Count<4 ))
                {
                    if (bin+5 <255)
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

                   sum+= Math.Abs(_currentImage.GetPixel(i, j).GetBrightness() - _currentImage.GetPixel(i-1, j).GetBrightness());
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
    }
}