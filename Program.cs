using System;
using System.Windows.Forms;

namespace app
{
    static class Program
    {
        static public Random rnd;

        [STAThread]
        static void Main()
        {
            rnd = new Random();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hackheroes());
        }
    }
}
