using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DesktopUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Stream imageStream;
            var openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "D:\\";
            openFileDialog.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png*)|*.png*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((imageStream = openFileDialog.OpenFile()) != null)
                    {
                        using (imageStream)
                        {
                            var pic = new Bitmap(imageStream);
                            greyPoctureBox.Image = TransferToBlackAndWhite(pic);
                            greyPoctureBox.Width = pic.Width;
                            greyPoctureBox.Height = pic.Height;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private Bitmap TransferToBlackAndWhite(Bitmap sourceImage)
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
    }
}