using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Filters;

namespace DesktopUI
{
    public partial class Form1 : Form
    {
        private readonly double[,] _kernel = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
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
                    var pic = new Bitmap(imageStream);
                    resultPictureBox.Image = Filter.TransferToBlackAndWhite(pic);     

                    //resultPictureBox.Image = Filter.Convultion(pic, this._kernel);
                    this.Width = resultPictureBox.Width = pic.Width;
                    this.Height = resultPictureBox.Height = pic.Height;
                }
            }
        }
    }
}