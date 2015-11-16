using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceHandling.Designs
{
    public static class DesigerConfiguration
    {
        private const double LowThreshold = 0.2;
        private const double HighThreshold = 0.78;
        public static Designer GetConfiguration(ImgInfo imgInfo )
        {
            var result = new Designer();
            result.Add(new DesignExpression(cl=>  cl.Ratio > LowThreshold));
            result.Add(new DesignExpression(cl => cl.Ratio < HighThreshold));
            
            result.Add(new DesignExpression(cl => (float)cl.Width / cl.Height > 0.1));
            result.Add(new DesignExpression(cl => (float)cl.Width / cl.Height < 10));
            result.Add(new DesignExpression(cl => cl.MaxDiff < cl.Weight / 3.33));
            //result.Add(new DesignExpression(cl => cl.Height > 20));
            //result.Add(new DesignExpression(cl => cl.Width > 20)); 
            result.Add(new DesignExpression(cl => cl.Weight > 70));
            result.Add(new DesignExpression(cl=> !(cl.Angles.Left.X == 0 && cl.Angles.Right.X ==  imgInfo.OldPicture.Width-1) ));

            return result;
        }

        public static Designer GetSecondConfiguration(ImgInfo imgInfo)
        {
            var result = new Designer();
            result.Add(new DesignExpression(cl => imgInfo.LincedLaters.Contains(cl.Class)));
            return result;
        }
    }
}
