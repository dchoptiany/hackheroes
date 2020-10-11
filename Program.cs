using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    static class Program
    {   
        [STAThread]
        static void Main()
        {
            MacroCalculator macroCalculator = new MacroCalculator();
            List<User> users = new List<User>();
            int currentUserIndex = 0;

            var basicUser = new User("User", 18, 80f, 180f, Gender.Male);
            users.Add(basicUser);

            macroCalculator.CalculateMacro(users[currentUserIndex]);
            Console.WriteLine(users[currentUserIndex].age);

            BMI.CalculateBmi(basicUser);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hackheroes());
        }
    }
}
