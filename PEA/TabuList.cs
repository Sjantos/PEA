using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEA
{
    class TabuList
    {
        int[,] table;
        int numberOfCities;
        int timeInTabu;

        public TabuList(int size, int time)
        {
            numberOfCities = size;
            timeInTabu = time;
            table = new int[numberOfCities, numberOfCities];
        }

        public void TabuTick()
        {
            for (int i = 0; i < numberOfCities; i++)
                for (int j = 0; j < numberOfCities; j++)
                    if (table[i, j] != 0) table[i, j]--;
        }

        public void AddTabu(int city1, int city2)
        {
            table[city1, city2] += timeInTabu;
            table[city2, city1] += timeInTabu;
        }

        public bool SwapPossible(int city1, int city2)
        {
            if (table[city1, city2] == 0) return true;
            return false;
        }
    }
}
