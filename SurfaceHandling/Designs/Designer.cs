using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceHandling.Designs
{
    public class Designer : IDesign
    {
        private readonly List<IDesign> _designs; 
        public List<IDesign> Designs
        {
            get
            {
                return new List<IDesign>(_designs);
            }

        }

        public Designer()
        {
            _designs = new List<IDesign>();
        }

        public void Add(IDesign d)
        {
            _designs.Add(d);
        }

        public bool Decide(PixelClass p)
        {
            return Designs.All(d => d.Decide(p));
        }
    }
}
