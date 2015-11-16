using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceHandling
{
    public class SameList
    {
        private List<List<int>> _sameList;
        public SameList() 
        {
            _sameList = new List<List<int>>();
        }

        public void Add(int A, int B) 
        {
            int a = 0;
            var sameList = _sameList.Where(x => x.Contains(A) || x.Contains(B));
            if (sameList.Count() > 1)
            {
                var newList = new List<int>();
                foreach (var v in sameList)
                {
                    newList.AddRange(v);
                    
                }
                _sameList.RemoveAll(x => sameList.Contains(x));
                _sameList.Add(newList);
                sameList = new List<List<int>>{newList};
            }
            if (sameList.Any())
            {
                
                if (!sameList.First().Contains(A))
                  sameList.First().Add(A);

                if (!sameList.First().Contains(B))
                    sameList.First().Add(B);
            }
            else 
            {
                var newList = new List<int> { A, B };
                _sameList.Add(newList);
            }
        }

        public int SerchClass(int A) 
        {
            return _sameList.Where(x => x.Contains(A)).First().First();
        }

        public List<int> AllClasses() 
        {
            var result = new List<int>();
            foreach (var s in  _sameList)
            {
                result.AddRange(s);
            }
            return result;
        }
        
    }
}
