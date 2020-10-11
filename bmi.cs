using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    class BMI
    {
        public static void CalculateBmi (User user)
        {
            
            float BMI = user.weight / (user.height / 100 * user.height / 100);
            Console.Write($"Twoje BMI to {BMI}");
            if (BMI < 18.5) Console.WriteLine("Niedowaga");
            else if (BMI < 25) Console.WriteLine("Waga prawidlowa");
            else Console.WriteLine("Nadwaga");
        }
    }
}
