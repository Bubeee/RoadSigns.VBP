using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurfaceHandling
{
    public class PixelClass
    {
        public PixelClass()
        {
           LineList = new List<Func<int, int>>();  
           LikedClass = new List<PixelClass>();
        }

        public int Class { get; set; }
        public int Square { get; set; }  
        public Angles Angles { get; set; }

        public int Weight { get; set; }

        public Point Center { get; set; }
        public int Height { get; set; }
        public int Width { get; set; } 
        public float Ratio { get; set; }

        public int MaxDiff { get; set; }
        public int[] FourWeights { get; set; }

        public List<PixelClass> LikedClass { get; set; }
        public Point[] FourPoints { get; set; }
        public List<Func<int, int>> LineList { get; set;}
        public float WBrtight { get; set; }
    }
}
