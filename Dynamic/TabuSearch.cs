﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Dynamic
{
    class TabuSearch
    {
        int tabuTime;
        int timeLimit;
        bool diversification;

        DateTime lastImprovementTime;
        Matrix graph;
        TabuList tabuList;
        int[] theBestEverSolutionPath;
        int theBestEverSolutionCost;

        int[] bestSolutionPath;
        int bestSolutionCost;

        public TabuSearch(Matrix g, int tabuDuration, int maxTime = 600, bool divers = true)
        {
            diversification = divers;
            timeLimit = maxTime;
            tabuTime = tabuDuration;
            graph = new Matrix(g.Dimension);
            graph.Copy(g);
            tabuList = new TabuList(graph.Dimension, tabuTime);
        }

        public string RunAlgorithm()
        {
            TabuSearchAlgorithm();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("" + theBestEverSolutionCost);
            sb.Append("Path: ");
            foreach (var item in theBestEverSolutionPath)
                sb.Append(item + " ");
            sb.Append("\n");

            return sb.ToString();
        }

        private void TabuSearchAlgorithm()
        {
            DateTime algorithmStartTime = DateTime.Now;
            int[] currentSolution = StartSolutionPath();//RandomSolutionPath();
            theBestEverSolutionPath = new int[currentSolution.Length];
            bestSolutionPath = new int[currentSolution.Length];
            Array.Copy(currentSolution, theBestEverSolutionPath, currentSolution.Length);
            Array.Copy(currentSolution, bestSolutionPath, currentSolution.Length);
            bestSolutionCost = SolutionCost(bestSolutionPath);
            theBestEverSolutionCost = SolutionCost(theBestEverSolutionPath);

            double seconds = 0;
            while((seconds = (DateTime.Now - algorithmStartTime).TotalSeconds) < timeLimit)
            {
                currentSolution = getBetterPossibleSolution(currentSolution);
                int currentSolutionCost = SolutionCost(currentSolution);

                if(currentSolutionCost < bestSolutionCost)
                {
                    Array.Copy(currentSolution, bestSolutionPath, currentSolution.Length);
                    bestSolutionCost = currentSolutionCost;

                    if(bestSolutionCost < theBestEverSolutionCost)
                    {
                        theBestEverSolutionCost = bestSolutionCost;
                        Array.Copy(bestSolutionPath, theBestEverSolutionPath, bestSolutionPath.Length);
                    }
                    lastImprovementTime = DateTime.Now;
                }
                if (diversification)
                    MakeDiversification();
            }
        }

        private int[] getBetterPossibleSolution(int[] solution)
        {
            int city1 = -1, city2 = -1;
            bool firstNeighbor = true;

            int[] bestSolution = new int[solution.Length];
            Array.Copy(solution, bestSolution, bestSolution.Length);

            int bestCost = SolutionCost(bestSolution);

            for (int i = 1; i < bestSolution.Length - 1; i++)
            {
                for (int j = 2; j < bestSolution.Length - 1; j++)
                {
                    if (i == j) continue;

                    int[] newBestSol = new int[bestSolution.Length];
                    Array.Copy(bestSolution, newBestSol, newBestSol.Length);

                    newBestSol = swapCities(i, j, solution);
                    int newBestCost = SolutionCost(newBestSol);

                    bool isBetterOrFirstSolution = (bestCost > newBestCost) || firstNeighbor;
                    bool swapPossible = tabuList.SwapPossible(i, j);

                    if ((isBetterOrFirstSolution && swapPossible) || ((bestSolutionCost > newBestCost)))
                    {
                        firstNeighbor = false;
                        city1 = i;
                        city2 = j;

                        Array.Copy(newBestSol, bestSolution, newBestSol.Length);
                        bestCost = newBestCost;
                    }
                }
            }

            if (city1 != -1)
            {
                tabuList.TabuTick();
                tabuList.AddTabu(city1, city2);
            }

            return bestSolution;
        }

        private int[] swapCities(int city1, int city2, int[] solution)
        {
            int tmp = solution[city1];
            solution[city1] = solution[city2];
            solution[city2] = tmp;

            return solution;
        }

        private int SolutionCost(int[] solution)
        {
            int cost = 0;
            for (int i = 0; i < solution.Length-1; i++)
                cost += graph[solution[i], solution[i + 1]];
            return cost;
        }

        private int[] StartSolutionPath()
        {
            //int[] solution = new int[graph.Dimension];
            //for (int i = 0; i < graph.Dimension; i++)
            //    solution[i] = i + 1;
            //solution[graph.Dimension] = 0;
            LinkedList<int> sol = new LinkedList<int>();
            sol.AddFirst(0);
            LinkedList<int> citiesToPick = new LinkedList<int>();
            for (int i = 1; i < graph.Dimension; i++)
                citiesToPick.AddFirst(i);
            while(citiesToPick.Count > 0)
            {
                int cityWithLowestCost = 0;
                int lowestCost = Int32.MaxValue;
                foreach (int neighbour in citiesToPick)
                {
                    if (graph[sol.Last(), neighbour] < lowestCost)
                    {
                        cityWithLowestCost = neighbour;
                        lowestCost = graph[sol.Last(), neighbour];
                    }
                }
                sol.AddLast(cityWithLowestCost);
                citiesToPick.Remove(cityWithLowestCost);
            }
            sol.AddLast(sol.First());
            int[] solution = sol.ToArray();
            return solution;
        }

        private int[] RandomSolutionPath()
        {
            LinkedList<int> solution = new LinkedList<int>();
            LinkedList<int> citiesToPick = new LinkedList<int>();
            for (int i = 0; i < graph.Dimension; i++)
                citiesToPick.AddLast(i);
            Random randomizer = new Random(DateTime.Now.Millisecond);
            while(citiesToPick.Count != 0)
            {
                solution.AddFirst(citiesToPick.ElementAt(randomizer.Next() % citiesToPick.Count));
                //citiesToPick contains unique values
                citiesToPick.Remove(solution.First());
            }
            solution.AddLast(solution.First());

            return solution.ToArray<int>();
        }

        private void MakeDiversification()
        {
            if((DateTime.Now - lastImprovementTime).TotalSeconds > 30)
            {
                int[] randomSolution = RandomSolutionPath();
                Array.Copy(randomSolution, bestSolutionPath, randomSolution.Length);
                bestSolutionCost = SolutionCost(randomSolution);
            }
        }
    }
}

//Other method for getBetterPossibleSolution
//int numberOfCitiesInNeighbourhood = (int)(graph.Dimension * 0.1);
//int lefti = i, righti = i, k = 0;
//LinkedList<int> neighbours = new LinkedList<int>();
//while (k < numberOfCitiesInNeighbourhood)
//{
//    if ((lefti > 0) && (k < numberOfCitiesInNeighbourhood))
//    {
//        neighbours.AddFirst(currentBestSolution[--lefti]);
//        k++;
//    }
//    if ((righti < currentBestSolution.Length - 2) && (k < numberOfCitiesInNeighbourhood))
//    {
//        neighbours.AddLast(currentBestSolution[++righti]);
//        k++;
//    }
//}
//foreach (int j in neighbours)
//{
//    int[] currentNewSolution = new int[currentBestSolution.Length];
//    Array.Copy(currentBestSolution, currentNewSolution, currentBestSolution.Length);
//    currentNewSolution = swapCities(i, j, currentNewSolution);
//    int currentNewCost = SolutionCost(currentNewSolution);

//    bool currentNewBetterOrFirstSolution = (currentBestCost > currentNewCost) || firstNeighbour;
//    //znaleziono lepsze rozwiazanie AND mozliwy swap OR lepsze niz globalne rozwiazanie(aspiracja)
//    if ((currentNewBetterOrFirstSolution && tabuList.SwapPossible(i, j)) || (currentNewCost < bestSolutionCost))
//    {
//        firstNeighbour = false;
//        city1 = i;
//        city2 = j;

//        Array.Copy(currentNewSolution, currentBestSolution/*bestSolutionPath*/, currentNewSolution.Length);
//        bestSolutionCost = currentNewCost;
//    }
//}