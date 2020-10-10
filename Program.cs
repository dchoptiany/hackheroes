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
            User user = new User("User", 18, 80f, 180f, Gender.Male);

            macroCalculator.CalculateMacro(ref user);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
