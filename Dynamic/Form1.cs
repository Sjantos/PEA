using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using TspLibNet;
using System.Text.RegularExpressions;

namespace Dynamic
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        private DateTime dt = DateTime.Now;
        private TspLib95 library;
        bool libraryLoaded;
        

        Matrix matrixGraph;
        public Form1(string path)
        {
            InitializeComponent();
            this.TopMost = false;
            matrixGraph = new Matrix();

            synchronizationContext = SynchronizationContext.Current;
            buttonDynamic.Enabled = false;
            buttonShowMatrix.Enabled = false;
            buttonTabuSearch.Enabled = false;
            libraryLoaded = false;
            try
            {
                library = new TspLib95(path);
                LinkedList<TspLib95Item> allTSPandATSPLibrary = new LinkedList<TspLib95Item>();
                var tspLibrary = library.LoadAllTSP();
                foreach (var item in tspLibrary)
                    allTSPandATSPLibrary.AddFirst(item);
                var atspLibrary = library.LoadAllATSP();
                foreach (var item in atspLibrary)
                    allTSPandATSPLibrary.AddFirst(item);

                foreach (var item in allTSPandATSPLibrary)
                {
                    if (item.Problem.Type == ProblemType.ATSP)
                        listView1.Items.Add(new ListViewItem("ATSP " + item.Problem.Name));
                    else
                        listView1.Items.Add(new ListViewItem("TSP " + item.Problem.Name));
                }
                libraryLoaded = true;
            }
            catch (Exception)
            {
                AppendTextBox("Failed to load TSPLIB95 directory\n");
            }
            if (libraryLoaded) buttonLoadFromList.Enabled = true;
            else buttonLoadFromList.Enabled = false;
            //AppendTextBox(Environment.CurrentDirectory);
        }

        #region temporary region
        /// <summary>
        /// Run test for matrix with random values
        /// </summary>
        /// <param name="numberOfRepeats">number of repeats to calculate average execution time</param>
        /// <param name="testDimension">dimension of matrix with input data</param>
        /// <returns></returns>
        private float Test(int numberOfRepeats, int testDimension)
        {
            Stopwatch timer = new Stopwatch();
            for (int i = 0; i < numberOfRepeats; i++)
            {
                DynamicAlgorithm dp = new DynamicAlgorithm(new Matrix(testDimension, true));
                timer.Start();
                dp.RunAlgorithm();
                timer.Stop();
            }
            
            long elapsedTime = timer.ElapsedMilliseconds;
            return (float)elapsedTime / numberOfRepeats;
        }

        /// <summary>
        /// Loads matrix form file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            //Show file select window
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //If OK clicked, load matrix
                try
                {
                    StreamReader file = new StreamReader(openFileDialog1.OpenFile());
                    richTextBoxStatus.AppendText(openFileDialog1.FileName + "\n");
                    richTextBoxStatus.AppendText(matrixGraph.LoadFile(file));
                }
                catch (Exception ex)
                {
                    //If there was an error, show warrning
                    richTextBoxStatus.AppendText("Opening file error\n");
                    richTextBoxStatus.AppendText(ex.ToString() + "\n");
                }
            }

            //If matrix is loaded, enable option to show it
            if (matrixGraph.IsFilled())
            {
                if (buttonShowMatrix.Enabled == false)
                    buttonShowMatrix.Enabled = true;
                if (buttonDynamic.Enabled == false)
                    buttonDynamic.Enabled = true;
                if (buttonTabuSearch.Enabled == false)
                    buttonTabuSearch.Enabled = true;
            }
        }

        /// <summary>
        /// Shows readed matrix in new window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowMatrix_Click(object sender, EventArgs e)
        {
            MatrixView mv = new MatrixView();
            mv.FillMatrixValues(matrixGraph.toString());
            mv.Show();
        }

        /// <summary>
        /// Runs algorithm, show result in textbox, will be used in async execution in buttonDynamic_Click
        /// </summary>
        private void buttonDynamicFunctionality()
        {
            try
            {
                //New instance of algorithm
                DynamicAlgorithm dp = new DynamicAlgorithm(matrixGraph);
                Stopwatch timer = new Stopwatch();
                //Measure time
                timer.Start();
                TSPResult<int> dpResult = dp.RunAlgorithm();
                int value = dpResult.PathCost;
                timer.Stop();
                //Get optimal path
                StringBuilder s = new StringBuilder();
                int[] path = dpResult.Path;
                foreach (int item in path)
                    s.Append(item + " ");
                s.Append("\n");
                string str = s.ToString();
                var elapsedTime = timer.ElapsedMilliseconds;
                AppendTextBox("Algorithm was executed in: " + elapsedTime + " ms\n");
                AppendTextBox("Calculated cost: " + value + "\n" + str + "\n");
            }
            catch (OutOfMemoryException)
            {
                AppendTextBox("Too big instance, algorithm needed too much space\n");
            }
        }

        /// <summary>
        /// Updates richTextBoxStatus with some string
        /// </summary>
        /// <param name="value">String value, to put in richTextBoxStatus</param>
        private void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            richTextBoxStatus.Text += value;
            richTextBoxStatus.SelectionStart = richTextBoxStatus.Text.Length;
            richTextBoxStatus.ScrollToCaret();
        }

        /// <summary>
        /// Async execution of algorithm (when algorithm take more time, it prevent the "window not responding")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonDynamic_Click(object sender, EventArgs e)
        {
            //Deactivate all buttons
            Button[] buttons = new Button[] { buttonDynamic, buttonLoadFile, buttonShowMatrix, buttonTest };
            foreach (var button in buttons)
                button.Enabled = false;

            //Run algorithm
            await Task.Run(delegate () { buttonDynamicFunctionality(); });

            //Activate all buttons
            foreach (var button in buttons)
                button.Enabled = true;
        }

        /// <summary>
        /// Runs algorithm for random values, show result in textbox, will be used in async execution in buttonTest_Click
        /// Iit also parses values from textBoxRepeatsInTest and textBoxDimension
        /// </summary>
        private void buttonTestFunctionality()
        {
            try
            {
                int readyToTest = 0, r = 0, d = 0;
                //Testing if values are valid
                int numValue;
                bool parsed = Int32.TryParse(textBoxRepeatsInTest.Text, out numValue);
                if (!parsed)
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", textBoxRepeatsInTest.Text);
                else
                {
                    r = numValue;
                    readyToTest++;
                }
                parsed = Int32.TryParse(textBoxDimension.Text, out numValue);
                if (!parsed)
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", textBoxDimension.Text);
                else
                {
                    d = numValue;
                    readyToTest++;
                }
                //if readyToTest == 2 then both values are valid
                if (readyToTest == 2)
                {
                    float averageTIme = Test(r, d);
                    richTextBoxStatus.AppendText("Average time for " + r + " repeats with " + d + "x" + d + " matrix was: " + averageTIme + "ms\n");
                }
                else
                    richTextBoxStatus.AppendText("Not valid values in test repeats or test dimension\n");
            }
            catch (OutOfMemoryException)
            {
                richTextBoxStatus.AppendText("Too big instance, algorithm needed too much space\n");
            }
        }

        /// <summary>
        /// Async execution of algorithm for random values (when algorithm take more time, it prevent the "window not responding")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonTest_Click(object sender, EventArgs e)
        {
            Button[] buttons = new Button[] { buttonDynamic, buttonLoadFile, buttonShowMatrix, buttonTest };
            foreach (var button in buttons)
                button.Enabled = false;

            await Task.Run(delegate () { buttonTestFunctionality(); });

            foreach (var button in buttons)
                button.Enabled = true;
        }

        /// <summary>
        /// Runs algorithm, show result in textbox, will be used in async execution in buttonTabuSearch_Click
        /// </summary>
        private void buttonTabuSearchFunctionality()
        {
            try
            {
                int readyToTest = 0, totalIterations = 0, tabuTime = 0;
                //Testing if values are valid
                int numValue;
                bool parsed = Int32.TryParse(textBoxTabuTime.Text, out numValue);
                if (!parsed)
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", textBoxTabuTime.Text);
                else
                {
                    tabuTime = numValue;
                    readyToTest++;
                }
                parsed = Int32.TryParse(textBoxTotalIterations.Text, out numValue);
                if (!parsed)
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", textBoxTotalIterations.Text);
                else
                {
                    totalIterations = numValue;
                    readyToTest++;
                }
                //if readyToTest == 2 then both values are valid
                if (readyToTest == 2)
                {
                    //New instance of algorithm
                    TabuSearch ts = new TabuSearch(matrixGraph, tabuTime, totalIterations);

                    TSPResult<int> tsResult = ts.RunAlgorithm();
                    int value = tsResult.PathCost;
                    StringBuilder s = new StringBuilder();
                    int[] path = tsResult.Path;
                    foreach (int item in path)
                        s.Append(item + " ");
                    s.Append("\n");
                    string str = s.ToString();
                    AppendTextBox($"Number of iterations: {totalIterations}    Tabu time: {tabuTime}\n");
                    AppendTextBox($"Calculated cost: {value}, Path: {str}\n");
                }
                else
                    richTextBoxStatus.AppendText("Not valid values in test repeats or test dimension\n");

                
            }
            catch (OutOfMemoryException)
            {
                AppendTextBox("Too big instance, algorithm needed too much space\n");
            }
        }

        private async void buttonTabuSearch_Click(object sender, EventArgs e)
        {
            //Deactivate all buttons
                Button[] buttons = new Button[] { buttonDynamic, buttonLoadFile, buttonShowMatrix, buttonTest, buttonTabuSearch, buttonLoadFromList, buttonTabuTest };
            foreach (var button in buttons)
                button.Enabled = false;

            //Run algorithm
            await Task.Run(delegate () { buttonTabuSearchFunctionality(); });

            //Activate all buttons
            foreach (var button in buttons)
                button.Enabled = true;
        }

        private void buttonTabuTestFunctionality()
        {
            LinkedList<TspLib95Item> items = new LinkedList<TspLib95Item>();
            items.AddFirst(library.GetItemByName("rbg443", ProblemType.ATSP));
            items.AddFirst(library.GetItemByName("rbg323", ProblemType.ATSP));
            items.AddFirst(library.GetItemByName("ft70", ProblemType.ATSP));
            items.AddFirst(library.GetItemByName("gr431", ProblemType.TSP));
            items.AddFirst(library.GetItemByName("gil262", ProblemType.TSP));
            items.AddFirst(library.GetItemByName("eil76", ProblemType.TSP));

            foreach (var item in items)
            {
                AppendTextBox($"Loaded: {item.Problem.Name}   Best known: {item.OptimalTourDistance}\n");
                Matrix m = new Matrix(item);
                //AppendTextBox("Tabu time\n");
                //for (int k = 0; k < 10; k++)
                //{
                //    int tabuTime = k * 2;
                //    TabuSearch ts = new TabuSearch(m, tabuTime, 100);

                //    TSPResult<int> tsResult = ts.RunAlgorithm();
                //    int value = tsResult.PathCost;
                //    AppendTextBox($"{100},{tabuTime},{Math.Round((double)value / item.OptimalTourDistance, 4)}\n");
                //}

                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        int totalIterations = i*10, tabuTime = j * 2;
                        TabuSearch ts = new TabuSearch(m, tabuTime, totalIterations);

                        TSPResult<int> tsResult = ts.RunAlgorithm();
                        AppendTextBox($"{totalIterations},{tabuTime},{tsResult.PathCost},{item.OptimalTourDistance},      {tsResult.ImprovementCounter},{tsResult.Time}\n");
                    }
                }

            }
        }

        private async void buttonTabuTest_Click(object sender, EventArgs e)
        {
            //Deactivate all buttons
            Button[] buttons = new Button[] { buttonDynamic, buttonLoadFile, buttonShowMatrix, buttonTest, buttonTabuSearch, buttonLoadFromList, buttonTabuTest };
            foreach (var button in buttons)
                button.Enabled = false;

            //Run algorithm
            await Task.Run(delegate () { buttonTabuTestFunctionality(); });

            //Activate all buttons
            foreach (var button in buttons)
                button.Enabled = true;
        }

        private void buttonLoadFromList_Click(object sender, EventArgs e)
        {
            string problemName = listView1.SelectedItems[0].Text;
            if (problemName.Contains("ATSP"))
            {
                problemName = problemName.Remove(0, 5);
                AppendTextBox($"Loaded: {problemName}   Best known: {library.GetItemByName(problemName, ProblemType.ATSP).OptimalTourDistance}\n");
                matrixGraph = new Matrix(library.GetItemByName(problemName, ProblemType.ATSP));
            }
            else
            {
                problemName = problemName.Remove(0, 4);
                AppendTextBox($"Loaded: {problemName}   Best known: {library.GetItemByName(problemName, ProblemType.TSP).OptimalTourDistance}\n");
                matrixGraph = new Matrix(library.GetItemByName(problemName, ProblemType.TSP));
            }

            //If matrix is loaded, enable option to show it
            if (matrixGraph.IsFilled())
            {
                if (buttonShowMatrix.Enabled == false)
                    buttonShowMatrix.Enabled = true;
                if (buttonDynamic.Enabled == false)
                    buttonDynamic.Enabled = true;
                if (buttonTabuSearch.Enabled == false)
                    buttonTabuSearch.Enabled = true;
            }
        }
        #endregion

        private void buttonGenetic_Click(object sender, EventArgs e)
        {
            Individual.GraphMatrix = matrixGraph;
            Individual.NumberOfCities = matrixGraph.Dimension;
            Individual i1 = new Individual(new int[10] { 8, 4, 7, 3, 6, 2, 5, 1, 9, 0 });
            Individual i2 = new Individual(new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Individual child = new Individual(i1, i2);
            for (int i = 0; i < 10; i++)
            {
                AppendTextBox(child[i] + " ");
            }
        }
        






        //Function used in tests, not used in final program

        //private void FinalTest()
        //{
        //    int[] dimension = new int[] { 10, 12, 14, 16, 18, 20};
        //    foreach (var dim in dimension)
        //    {

        //        float averageTIme = Test(1000, dim);
        //        richTextBoxStatus.AppendText("Average time for " + 1000 + " repeats with " + dim + "x" + dim + " matrix was: " + averageTIme + "ms\n");
        //    }
        //}

        //private async void buttonFinalTest_Click(object sender, EventArgs e)
        //{
        //    Button[] buttons = new Button[] { buttonDynamic, buttonFinalTest, buttonLoadMatrix, buttonLoadTSPLIB, buttonShowMatrix, buttonTest };
        //    foreach (var button in buttons)
        //        button.Enabled = false;

        //    await Task.Run(delegate() { FinalTest(); });

        //    foreach (var button in buttons)
        //        button.Enabled = true;
        //    //FinalTest();
        //}
    }
}
