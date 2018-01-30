using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PEA
{
    class Matrix : IGraph
    {
        int[,] matrix;
        int dimension = 0;
        bool matrixFilled = false;

        //getter for matrix dimension
        public int Dimension { get { return dimension; } }

        public Matrix() { }

        /// <summary>
        /// if rand = true then matrix will be filled with random numbers (0-999)
        /// </summary>
        /// <param name="dim"></param>
        /// <param name="rand"></param>
        public Matrix(int dim, bool rand = false)
        {
            matrix = new int[dim, dim];
            dimension = dim;
            if(rand)
            {
                Random randomizer = new Random(Environment.TickCount);
                for (int a = 0; a < dimension; a++)
                    for (int b = 0; b < dimension; b++)
                        matrix[a, b] = randomizer.Next() % 1000;
                for (int i = 0; i < dimension; i++)
                    matrix[i, i] = Int32.MaxValue;
            }
        }

        public Matrix(TspLibNet.TspLib95Item item)
        {
            dimension = item.Problem.NodeProvider.CountNodes();
            matrix = new int[dimension, dimension];
            var nodes = item.Problem.NodeProvider.GetNodes();
            foreach (var i in nodes)
            {
                foreach (var j in nodes)
                {
                    if (i.Id-1 == j.Id-1) matrix[i.Id-1, j.Id-1] = Int32.MaxValue;
                    else matrix[i.Id-1, j.Id-1] = (Int32)item.Problem.EdgeWeightsProvider.GetWeight(i, j);
                }
            }
            this.matrixFilled = true;
        }

        /// <summary>
        /// Copy values from one Matrix object to another
        /// </summary>
        /// <param name="m"></param>
        public void Copy(Matrix m)
        {
            for (int a = 0; a < dimension; a++)
                for (int b = 0; b < dimension; b++)
                    matrix[a, b] = m[a, b];
            this.matrixFilled = true;
        }

        /// <summary>
        /// Override operand [,], easy access to matrix
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int this[int a, int b]
        {
            get { return matrix[a, b]; }
            set { matrix[a, b] = value; }
        }

        /// <summary>
        /// Load data from file, it recognizes FULL_MATRIX, LOWER_DIAG_ROW, GEO and normal file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public String LoadFile(StreamReader file)
        {
            string firstLine = file.ReadLine();
            if (firstLine.Contains("NAME"))
            {//TSPLIB
                string description = "";
                description = file.ReadLine();
                //Reads file until data section
                while (!description.Contains("SECTION"))
                {
                    description += file.ReadLine();
                    description += "\n";
                }
                //Table containing description (TSPLIB)
                string[] descriptionTable = description.Split('\n');
                //Finding interesting information in file (which type of input data)
                foreach (var descriptionLine in descriptionTable)
                {
                    //Read dimension of matrix
                    if (descriptionLine.Contains("DIMENSION"))
                    {
                        string tmp = Regex.Match(descriptionLine, @"\d+").Value;
                        dimension = Int32.Parse(tmp);
                    }
                    //Data has FULL_MATRIX structure
                    if (descriptionLine.Contains("FULL_MATRIX"))
                        return LoadFileATSP(file);
                    //Symmetric matrix, only lower triangle
                    if (descriptionLine.Contains("LOWER_DIAG_ROW"))
                        return LoadFileSymetricLowerDiag(file);
                    //Geographical data
                    if (descriptionLine.Contains("GEO"))
                        return LoadFileSymetricGeo(file);
                }
            }
            else
            {
                //Ordinary matrix (dimension in first line, then FULL_MATRIX
                firstLine = Regex.Match(firstLine, @"\d+").Value;
                dimension = Int32.Parse(firstLine);
                return LoadFileOrdinary(file);
            }
            return "ERROR when loading graph, load good file (ATSP, LOWER_DIAG_ROW, GEO (FUNCTION) )\n";
        }

        /// <summary>
        /// Converts coordinates to longitude and latitude in GEO, parameter is table of coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        private void convertCoordinatesToLatitudeAndLongitude(double[,] coordinates)
        {
            //Algorithm from TSPLIB
            //deg = nint(x[i]);
            //min = x[i] - deg;
            //latitude[i] = PI * (deg + 5.0 * min / 3.0) / 180.0;
            double PI = 3.141592;
            for (int row = 0; row < dimension; row++)
                for (int column = 0; column < 2; column++)
                {
                    double deg = Math.Floor(coordinates[row, column]);
                    double min = coordinates[row, column] - deg;
                    coordinates[row, column] = PI * (deg + 5.0 * min / 3.0) / 180.0;
                }
        }

        /// <summary>
        /// Calculates distance between to points, parameter is table of coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        private void calculateGeoDistance(double[,] coordinates)
        {
            //Something is not working here, will be improved soon (i hope)



            //Algorithm from TSPLIB
            //[i, 0] = latitude, [i, 1]=longitude
            //RRR = 6378.388;
            //q1 = cos(longitude[i] - longitude[j]);
            //q2 = cos(latitude[i] - latitude[j]);
            //q3 = cos(latitude[i] + latitude[j]);
            //dij = (int)(RRR * acos(0.5 * ((1.0 + q1) * q2 - (1.0 - q1) * q3)) + 1.0);
            double RRR = 6378.388, q1, q2, q3;
            for (int row = 0; row < dimension; row++)
                for (int column = row + 1; column < dimension; column++)
                {
                    q1 = Math.Cos(coordinates[row, 1] - coordinates[column, 1]);
                    q2 = Math.Cos(coordinates[row, 0] - coordinates[column, 0]);
                    q3 = Math.Cos(coordinates[row, 0] + coordinates[column, 0]);
                    matrix[row, column] = (int)Math.Floor(RRR * Math.Acos(0.5 * ((1.0 + q1) * q2 - (1.0 - q1) * q3)) + 1.0);
                }
        }

        /// <summary>
        /// Reads GEO from TSPLIB file, returns matrix dimension desc in string if successfull
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public String LoadFileSymetricGeo(StreamReader file)
        {
            double[,] coordinates = new double[dimension, 2];
            //Create and fill matrix
            matrix = new int[dimension, dimension];
            char[] spaces = new char[] { ' ', '\t', '\r', '\n' };
            //Read matrix from file
            String line = file.ReadToEnd();
            int row = 0, column = 0; string str;
            //Convert every number in array splited with white space
            foreach (var col in line.Split(spaces, StringSplitOptions.RemoveEmptyEntries))
            {
                //Parse number in string to double
                str = "";
                str = Regex.Match(col, @"-\d*\.\d*").Value;
                if(str.Equals(""))
                    str = Regex.Match(col, @"\d*\.\d*").Value;
                double numValue;

                bool parsed = double.TryParse(str.Replace('.', ','), out numValue);
                if (!parsed)
                {
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an double.\n", str);
                    column--;
                }
                else
                    coordinates[row, column] = numValue;
                column++;
                if (column == 2)
                {
                    row++;
                    column = 0;
                }
            }

            //Convert data coordinates[i, 0] = latitude, [i, 1]=longitude
            convertCoordinatesToLatitudeAndLongitude(coordinates);
            //Calculate distances
            calculateGeoDistance(coordinates);

            for (int r = 0; r < dimension; r++)
                for (int c = 0; c <= r; c++)
                    if (c == r)
                        matrix[r, c] = Int32.MaxValue;
                    else
                        matrix[r, c] = matrix[c, r];

            matrixFilled = true;
            return "Matrix is " + dimension + "x" + dimension + "\n";
        }

        /// <summary>
        /// Reads LOWER_DIAG_ROW from TSPLIB file, returns matrix dimension desc in string if successfull
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public String LoadFileSymetricLowerDiag(StreamReader file)
        {
            //Create and fill matrix
            matrix = new int[dimension, dimension];
            char[] spaces = new char[] { ' ', '\t', '\r', '\n' };
            //Read matrix from file
            String line = file.ReadToEnd();
            int row = 0, column = 0; string str;
            //Convert every number in array splited with white space
            foreach (var col in line.Split(spaces, StringSplitOptions.RemoveEmptyEntries))
            {
                //Parse number in string to int
                str = Regex.Match(col, @"\d+").Value;
                int numValue;
                bool parsed = Int32.TryParse(str, out numValue);
                if (!parsed)
                {
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", str);
                    column--;
                }
                else
                    matrix[row, column] = numValue;
                column++;
                if(numValue == 0)
                {
                    row++;
                    column = 0;
                }
            }
            //Copy symetric values
            for (int r = 0; r < dimension; r++)
                for (int c = 0; c < dimension; c++)
                    if (c > r)
                        matrix[r, c] = matrix[c, r];
                    else if (c == r)
                        matrix[r, c] = Int32.MaxValue;
            matrixFilled = true;
            return "Matrix is " + dimension + "x" + dimension + "\n";
        }

        /// <summary>
        /// Reads FULL_MATRIX from TSPLIB file, returns matrix dimension desc in string if successfull
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public String LoadFileATSP(StreamReader file)
        {
            //Create and fill matrix
            matrix = new int[dimension, dimension];
            char[] spaces = new char[] { ' ', '\t', '\r', '\n' };
            //Read matrix from file
            String line = file.ReadToEnd();
            int row = 0, column = 0; string str;
            //Convert every number in array splited with white space
            foreach (var col in line.Split(spaces, StringSplitOptions.RemoveEmptyEntries))
            {
                //Check if row is filled
                if (column == dimension)
                {
                    row++;
                    column = 0;
                }
                //Parse number in string to int
                str = Regex.Match(col, @"\d+").Value;
                int numValue;
                bool parsed = Int32.TryParse(str, out numValue);
                if (!parsed)
                {
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", str);
                    column--;
                }
                else
                    matrix[row, column] = numValue;
                column++;
            }

            for (int a = 0; a < dimension; a++)
                matrix[a, a] = Int32.MaxValue;

            matrixFilled = true;

            return "Matrix is " + dimension + "x" + dimension + "\n";
        }

        /// <summary>
        /// Loads matrix from file(first line is dimension, then full matrix is given)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        internal string LoadFileOrdinary(StreamReader file)
        {
            //Create and fill matrix
            matrix = new int[dimension, dimension];
            char[] spaces = new char[] { ' ', '\t', '\r', '\n' };
            //Read matrix from file
            String line = file.ReadToEnd();
            int row = 0, column = 0; string str;
            //Convert every number in array splited with white space
            foreach (var col in line.Split(spaces, StringSplitOptions.RemoveEmptyEntries))
            {
                //Check if row is filled
                if (column == dimension)
                {
                    row++;
                    column = 0;
                }
                //Parse number in string to int
                str = Regex.Match(col, @"\d+").Value;
                int numValue;
                bool parsed = Int32.TryParse(str, out numValue);
                if (!parsed)
                {
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", str);
                    column--;
                }
                else
                    matrix[row, column] = numValue;
                column++;
            }

            for (int a = 0; a < dimension; a++)
                matrix[a, a] = Int32.MaxValue;

            matrixFilled = true;

            return "Matrix is " + dimension + "x" + dimension + "\n";
        }

        /// <summary>
        /// Returns description string to view matrix
        /// </summary>
        /// <returns></returns>
        public String toString()
        {
            StringBuilder resultString = new StringBuilder();
            //For every number add it to description string
            for (int row = 0; row < dimension; row++)
            {
                for (int column = 0; column < dimension; column++)
                {
                    //Every number has the same width in view
                    if (matrix[row, column] == Int32.MaxValue)
                        resultString.Append(string.Format("{0,-7}", "inf"));
                    else
                        resultString.Append(string.Format("{0,-7}", matrix[row, column]));
                }
                resultString.Append("\n");
            }

            return resultString.ToString();
        }

        /// <summary>
        /// Returns true if matrix is filled with data
        /// </summary>
        /// <returns></returns>
        public bool IsFilled()
        {
            return matrixFilled;
        }
    }
}
