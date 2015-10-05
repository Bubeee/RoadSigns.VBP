using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Filters;

namespace DesktopUI
{
    public partial class Form1 : Form
    {
        private Bitmap _currentImage;

        public Form1()
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
                    var miniature = new Bitmap(this._currentImage);
                    miniature.SetResolution(0.001f, 0.001f);
                    sourcePictureBox.Image = miniature;
                }
            }
        }

        private void CalculateButtonClick(object sender, System.EventArgs e)
        {
            int treshold;
            if (int.TryParse(thresholdInput.Text, out treshold))
            {
                resultPictureBox.Image = Filter.ApplySobelFilter(Filter.BitmapToBlackWhite(this._currentImage, treshold));

                //resultPictureBox.Image = (Filter.BitmapToBlackWhite(pic, treshold));
            }
        }
    }
}