using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEA
{
    interface IGraph
    {
        String LoadFile(StreamReader file);
        String toString();
        bool IsFilled();
    }
}
