using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    static class Program
    {
        static public List<User> users;
        static public int currentUserIndex;

        [STAThread]
        static void Main()
        {
            currentUserIndex = 0;
            users = new List<User>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hackheroes());
        }
    }
}
