using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace app
{
    static class Program
    {
        static public List<User> users;
        static public int currentUserIndex;
        static public Random rnd;
        [STAThread]
        static void Main()
        {
            currentUserIndex = 0;
            users = new List<User>();
            rnd = new Random();

            Quiz.LoadQuestions();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hackheroes());
        }
    }
}
