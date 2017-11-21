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

namespace Dynamic
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        private DateTime dt = DateTime.Now;

        Matrix matrixGraph;
        public Form1()
        {
            InitializeComponent();
            matrixGraph = new Matrix();

            synchronizationContext = SynchronizationContext.Current;
            buttonDynamic.Enabled = false;
            buttonShowMatrix.Enabled = false;
        }

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
                int value = dp.RunAlgorithm();
                timer.Stop();
                //Get optimal path
                string str = dp.GetPath();
                var elapsedTime = timer.ElapsedMilliseconds;
                AppendTextBox("Calculated cost: " + value + "\n" + str);
                AppendTextBox("Algorithm was executed in: " + elapsedTime + " ms\n\n");
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
