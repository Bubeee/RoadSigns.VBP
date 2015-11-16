using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using SurfaceHandling.Info;

namespace SurfaceHandling
{
    public class ImgInfo
    {
        public Bitmap OldPicture { get; set; }
        public Bitmap NewPicture { get; set; }
        public List<int> LincedLaters { get; set; }
        public Dictionary<int, PixelClass> Classes { get; set; }
        public List<Line> Lines { get; set; }
        public int[,] Labels { get; set; }
        public Point FourPoints { get; set; }
        public int Brightnest { get; set; }
    }
}
