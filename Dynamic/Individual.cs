using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    /// <summary>
    /// Solution of tsp represented by city permutation (without city repetition - for loop)
    /// </summary>
    class Individual
    {
        static Matrix graphMatrix;
        int[] solution;
        static int numberOfCities;

        internal static int NumberOfCities { set => numberOfCities = value; }

        internal static Matrix GraphMatrix { set => graphMatrix = value; }

        public int this[int i] { get { return solution[i]; } set { solution[i] = value; } }

        public Individual(int[] permutation)
        {
            //solution = new int[numberOfCities + 1];
            solution = permutation;
        }

        /// <summary>
        /// Creates child from two Individuals, PMX Crossover
        /// </summary>
        /// <param name="mother">parent 1</param>
        /// <param name="father">parent 2</param>
        public Individual(Individual mother, Individual father)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            solution = new int[numberOfCities];
            for (int i = 0; i < solution.Length; i++)
                solution[i] = -1;
            int leftIndex = 3;// rand.Next() % solution.Length;
            int rightIndex = 7;// rand.Next() % solution.Length;
            if(rightIndex < leftIndex)
            {
                int tmp = leftIndex;
                leftIndex = rightIndex;
                rightIndex = tmp;
            }
            for (int i = leftIndex; i <= rightIndex; i++)
                solution[i] = mother[i];

            for (int i = leftIndex; i <= rightIndex; i++)
            {
                bool fatherValueFoundInChild = false;
                for (int j = 0; j < solution.Length; j++)
                    if (solution[j] == father[i])
                    {
                        fatherValueFoundInChild = true;
                        break;
                    }

                if(!fatherValueFoundInChild)
                {
                    int fatherIndex = -1, motherValue = mother[i];
                    while(true)
                    {
                        fatherIndex = father.Find(motherValue);
                        if (solution[fatherIndex] != -1)
                            motherValue = mother[fatherIndex];
                        else
                            break;
                    }
                    solution[fatherIndex] = father[i];
                }
            }

            for (int i = 0; i < solution.Length; i++)
                if (solution[i] == -1)
                    solution[i] = father[i];
        }

        /// <summary>
        /// Returns quality of Indiviual (tsp route length)
        /// </summary>
        /// <returns></returns>
        public int Quality()
        {
            int quality = 0;
            for (int i = 0; i < solution.Length - 1; i++)
                quality += graphMatrix[solution[i], solution[i + 1]];
            quality += graphMatrix[solution[solution.Length - 1], solution[0]];
            return quality;
        }

        /// <summary>
        /// Return index of found argument in Individual, else -1
        /// </summary>
        /// <param name="number">Number to look in Individual (solution)</param>
        /// <returns></returns>
        public int Find(int number)
        {
            for (int i = 0; i < solution.Length; i++)
            {
                if (solution[i] == number)
                    return i;
            }
            return -1;
        }
    }
}
