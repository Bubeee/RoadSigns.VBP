using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Filters;

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

            ThresholdInputTextChanged(sender, e);
        }
        
        private void ThresholdInputTextChanged(object sender, System.EventArgs e)
        {
            int treshold;
            if (int.TryParse(thresholdInput.Text, out treshold))
            {
                //resultPictureBox.Image = Filter.ApplySobelFilter(Filter.BitmapToBlackWhite(this._currentImage, treshold)); 
                //resultPictureBox.Image = Filter.ApplySobelFilter(_currentImage); 

                var img = new Bitmap(this._currentImage);
                Filter.GradientEdgeDetection(img, treshold);
                resultPictureBox.Image = img;
            }
        }
    }
}