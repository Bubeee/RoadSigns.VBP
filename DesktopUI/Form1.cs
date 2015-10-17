using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Filters;
using System;

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

            if (BinarizeCheckBox.Checked)
            {
                float threshold = (float)binarizeNumericUpDown.Value;
                processedPicture = FilterMatrix.ApplyBinarize(processedPicture, threshold);
            }

            resultPictureBox.Image = FilterMatrix.GetBmp(processedPicture);
        }
    }
}