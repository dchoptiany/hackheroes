using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    static class BMI
    {
        public static void CalculateBmi (ref User user)
        {
            user.BMI = user.weight / (user.height / 100 * user.height / 100);
        }
    }
}
