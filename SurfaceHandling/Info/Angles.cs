using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace SurfaceHandling
{
    public class Angles
    {
        public Angles()
        {
             Left = new Point(int.MaxValue,0);
             Top = new Point(0,int.MaxValue);
        }

        
        public Point Top { get; set; }

        public Point Right { get; set; }

        public Point Left { get; set; }

        public Point Bottom { get; set; }
    }
}
