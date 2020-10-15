using System;
using System.Collections.Generic;
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

            Quiz.LoadQuestions();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hackheroes());
        }
    }
}
