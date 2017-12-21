using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    class TSPResult<T>
    {
        public T PathCost;
        public int[] Path;
        public long Time; //time taken in ms
        public int ImprovementCounter;

        public TSPResult(T cost, int[] path, long t = 0, int impr = 0)
        {
            this.PathCost = cost;
            this.Path = path;
            this.Time = t;
            this.ImprovementCounter = impr;
        }
    }
}
