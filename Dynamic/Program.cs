using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dynamic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Wybierz folder TSPLIB95 \ndo pobrania ze strony\n" +
                                    "https://github.com/pdrozdowski/TSPLib.Net \n";
            folder.ShowDialog();
            string path = folder.SelectedPath;
            Application.Run(new Form1(path));
        }
    }
}
