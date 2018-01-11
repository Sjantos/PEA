using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    class GeneticAlgorithm
    {
        Individual[] population;
        Individual[] parents;
        Individual[] children;

        int numberOfPopulation;
        int numberOfGenerations;
        double mutationChance;

        int tournamentSize;

        Matrix graph;
        Random rand;

        /// <summary>
        /// New instance of genetic algorithm for TSP
        /// </summary>
        /// <param name="g">graph in IGraph type</param>
        /// <param name="popSize">Number of Individuals in population (will be raised to multiplicity of 3)</param>
        /// <param name="iterations">Number of generations (iterations) - algorithm terminate condition</param>
        /// <param name="mutChance">Chance to make a mutation in child (new Individual)</param>
        /// <param name="tournament">Number of Individuals in tournament (selection)</param>
        public GeneticAlgorithm(IGraph g, int popSize, int iterations, double mutChance, int tournament)
        {
            rand = new Random(DateTime.Now.Millisecond);
            graph = (Matrix) g;
            numberOfPopulation = popSize;
            while (numberOfPopulation % 3 != 0)
                numberOfPopulation++;
            numberOfGenerations = iterations;
            mutationChance = mutChance;
            tournamentSize = tournament;

            population = new Individual[numberOfPopulation];
            for (int i = 0; i < numberOfPopulation; i++)
            {
                population[i] = new Individual();
            }
            int thirdPart = numberOfPopulation / 3;
            parents = new Individual[thirdPart];
            children = new Individual[2 * thirdPart];
        }

        /// <summary>
        /// Run algorithm, return TSPResult
        /// </summary>
        /// <returns></returns>
        public TSPResult<int> RunAlgorithm()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Individual bestSolution = Algorithm();
            watch.Stop();
            return new TSPResult<int>(bestSolution.Quality(), bestSolution.Solution, watch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Genetic algorithm, numberOfGenerations = iterations, return best solution (Individual)
        /// </summary>
        /// <returns></returns>
        private Individual Algorithm()
        {
            for (int i = 0; i < numberOfGenerations; i++)
            {
                Selection();
                Crossover();
                Mutation();
                MakePopulation();
            }

            int bestIndividualIndex = 0, bestSolution = population[0].Quality();
            for (int i = 0; i < population.Length; i++)
                if(population[i].Quality() < bestSolution)
                {
                    bestIndividualIndex = i;
                    bestSolution = population[i].Quality();
                }

            return population[bestIndividualIndex];
        }

        /// <summary>
        /// Selects best parents with Tournament()
        /// </summary>
        private void Selection()
        {
            //Select best parents
            for (int i = 0; i < parents.Length; i++)
                parents[i] = Tournament();
        }

        /// <summary>
        /// Makes children from parents selected by Tournament()
        /// </summary>
        private void Crossover()
        {
            for (int i = 0; i < children.Length; i += 2)
            {
                //Make childrens, pick randomly two parents and make two children (crossing p1+p2  and p2+p1)
                int p1Index = rand.Next() % parents.Length, p2Index = -1;
                do
                {
                    //Make sure it will be two different indexes
                    p2Index = rand.Next() % parents.Length;
                } while (p1Index == p2Index);
                Individual p1 = parents[p1Index], p2 = parents[p2Index];
                children[i] = new Individual(p1, p2, CrossoverMethod.PMX);
                children[i + 1] = new Individual(p2, p1, CrossoverMethod.PMX);
            }
        }

        /// <summary>
        /// Performs possible mutations to children
        /// </summary>
        private void Mutation()
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (rand.NextDouble() < mutationChance)
                    children[i].Mutate(MutationMethod.Inversion);
            }
        }

        /// <summary>
        /// Join parents and children into population
        /// </summary>
        private void MakePopulation()
        {
            Array.Copy(parents, 0, population, 0, parents.Length);
            Array.Copy(children, 0, population, parents.Length, children.Length);
        }

        /// <summary>
        /// Tournament return best Individual from some random group
        /// </summary>
        /// <returns></returns>
        private Individual Tournament()
        {
            List<Individual> participants = new List<Individual>();
            for (int i = 0; i < tournamentSize; i++)
                participants.Add(population[rand.Next() % numberOfPopulation]);

            //Choose best Individual
            int bestParentIndex = 0, best = participants[0].Quality();
            for (int i = 1; i < tournamentSize; i++)
            {
                int newBest = participants[i].Quality();
                if (best > newBest)
                {
                    best = newBest;
                    bestParentIndex = i;
                }
            }

            return participants[bestParentIndex];
        }
    }
}
