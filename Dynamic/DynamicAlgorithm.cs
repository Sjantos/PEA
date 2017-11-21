using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    class DynamicAlgorithm
    {
        int[,] costs;           //Stores best path lengths for visited cities (bits in column represents cities)
        int[,] paths;           //Stroes predecessor of actual city (for retrieving path)
        List<int> finalPath;    //The best path
        Matrix graphMatrix;     //Input data
        int numberOfCities;
        int startCity = 0;
        int fullSet;            //Number that represents all cities visited except for starting city ( 2^N -1 )

        public DynamicAlgorithm(Matrix m)
        {
            //More user friendly name and if matrix dimension is too big, << will shift too many times and algorithm could calculate pointless
            if (m.Dimension > 31)
            {
                throw new OutOfMemoryException();
            }
            else numberOfCities = m.Dimension;

            fullSet = (1 << numberOfCities) -1;
            graphMatrix = new Matrix(numberOfCities);
            graphMatrix.Copy(m);
            finalPath = new List<int>();
            //Tables with Nx2^N size
            costs = new int[numberOfCities, 1 << numberOfCities];
            paths = new int[numberOfCities, 1 << numberOfCities];
            //Initialization with -1
            for (int a = 0; a < numberOfCities; a++)
                for (int b = 0; b < (1 << numberOfCities); b++)
                {
                    costs[a, b] = -1;
                    paths[a, b] = -1;
                }

            //Distances for 1 node and empty set is known from input matrix (first step of dynamic)
            for (int i = 0; i < numberOfCities; i++)
                costs[i, 0] = graphMatrix[i, 0];
        }

        /// <summary>
        /// Runs dynamic algorithm, starts from city 0
        /// </summary>
        /// <returns></returns>
        public int RunAlgorithm()
        {
            return tsp(0, fullSet - 1);
        }

        /// <summary>
        /// Returns path in user friendly shape
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            findPath(0, (1 << numberOfCities) - 2);
            StringBuilder str = new StringBuilder();
            str.Append(startCity + "->");
            foreach (var item in finalPath)
                str.Append(item + "->");
            str.Append(startCity + "\n");
            return str.ToString();
        }

        /// <summary>
        /// Recursive function for calculating optimal tour (dynamically)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        private int tsp(int start, int set)
        {
            //file.Write(descMatrix(costs));
            int masked, result = -1, temp;
            //If already calculated then return value
            if (costs[start, set] != -1)
            {
                return costs[start, set];
            }
            else
            {
                //Foreach neighbour
                for (int x = 0; x < numberOfCities; x++)
                {
                    masked = set & (fullSet - (1 << x));
                    if (masked != set)
                    {
                        //Calculate best way in this particular combination
                        temp = graphMatrix[start, x] + tsp(x, masked);
                        //If better than actual, replace it
                        if (result == -1 || result > temp)
                        {
                            result = temp;
                            //Remember path
                            paths[start, set] = x;
                        }
                    }
                }
                //Remember best solution
                costs[start, set] = result;
                return result;
            }
        }

        /// <summary>
        /// Finds optimal path in path table
        /// </summary>
        /// <param name="start"></param>
        /// <param name="set"></param>
        private void findPath(int start, int set)
        {
            if (paths[start, set] == -1)
                return;
            //Get city number
            int city = paths[start, set];
            //Mark current city as visited
            int masked = set & (fullSet - (1 << city));
            //Remember city in best path
            finalPath.Add(city);
            //Go to next city
            findPath(city, masked);
        }
    }
}
