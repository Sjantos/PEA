using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dynamic
{
    public partial class MatrixView : Form
    {
        public MatrixView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fill richTextBox with string argument
        /// </summary>
        /// <param name="str"></param>
        public void FillMatrixValues(String str)
        {
            //Fint must be monospace to make columns
            richTextBoxMatrix.Font = new Font(FontFamily.GenericMonospace, richTextBoxMatrix.Font.Size);
            //Fill textbox with matrix
            richTextBoxMatrix.Text = str;
        }
    }
}
