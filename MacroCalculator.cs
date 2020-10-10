using System;
using System.Diagnostics;

namespace app
{
    class MacroCalculator
    {
        public int calories;

        public int protein;
        public int carbohydrates;
        public int fat;

        public void CalculateMacro(User user)
        {
            float rmr;
            if (user.gender == Gender.Male)
            {
                rmr = user.height * 6.25f + user.weight * 10f - (user.age * 5f) + 5f;
            }
            else
            {
                rmr = user.height * 6.25f + user.weight * 10f - (user.age * 5f) - 161f;
            }

            calories = (int)(rmr * 1.45f);
            float caloriesLeft = calories;

            protein = (int)user.weight * 2;
            caloriesLeft -= protein * 4;

            fat = (int)(caloriesLeft * (0.35f / 9f));
            caloriesLeft -= fat * 9;

            carbohydrates = (int)(caloriesLeft / 4f);
        }
    }
}
