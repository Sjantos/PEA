using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    public enum CrossoverMethod
    {
        PMX,
        OX
    }

    public enum MutationMethod
    {
        Swap,
        Scramble,
        Inversion
    }

    /// <summary>
    /// Solution of tsp represented by city permutation (without city repetition - for loop)
    /// </summary>
    class Individual
    {
        static Matrix graphMatrix;
        static int numberOfCities;
        int[] solution;

        internal static int NumberOfCities { set => numberOfCities = value; }

        internal static Matrix GraphMatrix { set => graphMatrix = value; }

        public int this[int i] { get { return solution[i]; } set { solution[i] = value; } }

        public int[] Solution { get { return solution; } }

        /// <summary>
        /// Creates ne Individual with random cities permutation
        /// </summary>
        public Individual()
        {
            solution = new int[numberOfCities];
            for (int i = 0; i < numberOfCities; i++)
                solution[i] = i;
            new Random().Shuffle<int>(solution);
        }

        /// <summary>
        /// Creates new Individual from given cities permutation
        /// </summary>
        /// <param name="permutation"></param>
        public Individual(int[] permutation)
        {
            solution = new int[numberOfCities];
            Array.Copy(permutation, solution, permutation.Length);
        }

        /// <summary>
        /// Creates new Individual from two parents with some crossover method
        /// </summary>
        /// <param name="mother">parent 1</param>
        /// <param name="father">parent 2</param>
        /// <param name="method">corssover method</param>
        public Individual(Individual mother, Individual father, CrossoverMethod method)
        {
            switch (method)
            {
                case CrossoverMethod.PMX:
                    PMXIndividual(mother, father);
                    break;
                case CrossoverMethod.OX:
                    OXIndividual(mother, father);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Mutates Individual with specified method
        /// </summary>
        /// <param name="method">Method of mutation</param>
        public void Mutate(MutationMethod method)
        {
            switch (method)
            {
                case MutationMethod.Swap:
                    SwapMutation();
                    break;
                case MutationMethod.Scramble:
                    ScrambleMutation();
                    break;
                case MutationMethod.Inversion:
                    InversionMutation();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates child from two Individuals, PMX Crossover
        /// </summary>
        /// <param name="mother">parent 1</param>
        /// <param name="father">parent 2</param>
        private void PMXIndividual(Individual mother, Individual father)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            solution = new int[numberOfCities];
            for (int i = 0; i < solution.Length; i++)
                solution[i] = -1;
            int leftIndex = rand.Next() % solution.Length;
            int rightIndex = rand.Next() % solution.Length;
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
        /// Creates child from two Individuals, OX Crossover
        /// </summary>
        /// <param name="mother">parent 1</param>
        /// <param name="father">parent 2</param>
        private void OXIndividual(Individual mother, Individual father)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            solution = new int[numberOfCities];
            for (int i = 0; i < solution.Length; i++)
                solution[i] = -1;
            int leftIndex = rand.Next() % solution.Length;
            int rightIndex = rand.Next() % solution.Length;
            if (rightIndex < leftIndex)
            {
                int tmp = leftIndex;
                leftIndex = rightIndex;
                rightIndex = tmp;
            }

            //Fill rest with father alleles
            Individual tmpFather = new Individual(father.Solution);
            for (int i = leftIndex; i <= rightIndex; i++)
            {
                solution[i] = mother[i];
                tmpFather[father.Find(mother[i])] = -1;
            }

            int fatherIndex = (rightIndex + 1) % solution.Length;
            int solutionIndex = (rightIndex + 1) % solution.Length;
            while (solutionIndex != leftIndex)
            {
                while (tmpFather[fatherIndex] == -1)
                    fatherIndex = (fatherIndex + 1) % solution.Length;

                solution[solutionIndex] = tmpFather[fatherIndex];
                solutionIndex = (solutionIndex + 1) % solution.Length;
                fatherIndex = (fatherIndex + 1) % solution.Length;
            }

            for (int i = 0; i < solution.Length; i++)
            {
                if (solution[i] == -1)
                    throw new Exception();
            }
        }

        /// <summary>
        /// Swap two randomly picked cities in solution
        /// </summary>
        private void SwapMutation()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int city1Index = rand.Next() % solution.Length;
            int city2Index = rand.Next() % solution.Length;
            int tmp = solution[city1Index];
            solution[city1Index] = solution[city2Index];
            solution[city2Index] = tmp;
        }

        /// <summary>
        /// Shuffle cities in a random swath in solution
        /// </summary>
        private void ScrambleMutation()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int city1Index = rand.Next() % solution.Length;
            int city2Index = rand.Next() % solution.Length;
            if(city1Index > city2Index)
            {
                int tmp = city1Index;
                city1Index = city2Index;
                city2Index = tmp;
            }
            int arrayToShuffleLength = city2Index - city1Index + 1;
            int[] arrayToShuffle = new int[arrayToShuffleLength];
            Array.Copy(solution, city1Index, arrayToShuffle, 0, arrayToShuffleLength);
            new Random().Shuffle<int>(arrayToShuffle);
            Array.Copy(arrayToShuffle, 0, solution, city1Index, arrayToShuffleLength);
        }

        /// <summary>
        /// Inverse cities in rando swath in solution
        /// </summary>
        private void InversionMutation()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int city1Index = rand.Next() % solution.Length;
            int city2Index = rand.Next() % solution.Length;
            if (city1Index > city2Index)
            {
                int tmp = city1Index;
                city1Index = city2Index;
                city2Index = tmp;
            }
            int arrayToInverseLength = city2Index - city1Index + 1;
            int[] swath = new int[arrayToInverseLength];
            Array.Copy(solution, city1Index, swath, 0, arrayToInverseLength);
            for (int i = city1Index, j = swath.Length - 1; j >= 0; i++, j--)
                solution[i] = swath[j];
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
