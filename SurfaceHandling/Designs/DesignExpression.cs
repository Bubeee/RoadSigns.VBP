using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceHandling.Designs
{
    class DesignExpression:IDesign
    {
       public Func<PixelClass, bool> DesignFunc { get; private set; }

       public DesignExpression(Func<PixelClass, bool> ex)
       {
           if (ex == null)
               throw new ArgumentNullException("ex");
           DesignFunc = ex;
       }
       public bool Decide(PixelClass p)
       {
           return DesignFunc != null && DesignFunc(p);
       }
    }
}
