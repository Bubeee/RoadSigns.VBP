using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceHandling.Info
{
    public class Line
    {
        public float k;

        public float b;
        public Func<int, int> Func { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
}
