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

            var basicUser = new User("User", 18, 80f, 180, Gender.Male);
            users.Add(basicUser);

            MacroCalculator.CalculateMacro(users[currentUserIndex]);

            BMI.CalculateBmi(basicUser);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hackheroes());
        }
    }
}
