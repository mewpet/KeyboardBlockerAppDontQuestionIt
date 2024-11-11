using System;
using System.Windows.Forms;

namespace ActiveWindowTitleApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Launch Form1 as the main form
        }
    }
}
