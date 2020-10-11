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
            MacroCalculator macroCalculator = new MacroCalculator();

            var basicUser = new User("User", 18, 80f, 180, Gender.Male);
            users.Add(basicUser);

            macroCalculator.CalculateMacro(users[currentUserIndex]);

            BMICalculator.CalculateBmi(basicUser);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hackheroes());
        }
    }
}
