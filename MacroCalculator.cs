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

        public enum Gender
        {
            Male = 0,
            Female
        }

        public void CalculateMacro(float weight, float height, int age, Gender gender)
        {
            float rmr;
            if (gender == Gender.Male)
            {
                rmr = height * 6.25f + weight * 10f - (age * 5f) + 5f;
            }
            else
            {
                rmr = height * 6.25f + weight * 10f - (age * 5f) - 161f;
            }

            calories = (int)(rmr * 1.45f);
            float caloriesLeft = calories;

            protein = (int)weight * 2;
            caloriesLeft -= protein * 4;

            fat = (int)(caloriesLeft * (0.35f / 9f));
            caloriesLeft -= fat * 9;

            carbohydrates = (int)(caloriesLeft / 4f);
        }
    }
}
