using System;
using System.Windows.Forms;

namespace ConwayLife.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLifeGame());
        }
    }
}
