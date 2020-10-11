﻿using System;
using System.Diagnostics;

namespace app
{
    static class MacroCalculator
    {
        public static void CalculateMacro(User user)
        {
            float rmr = user.height * 6.25f + user.weight * 10f - (user.age * 5f);
            rmr += user.gender == Gender.Male ? 5f : -161f;

            float activityLevel = 1.35f; // TO DO !!!
            user.calories = (int)(rmr * activityLevel);
            float caloriesLeft = user.calories;

            user.protein = (int)user.weight * 2;
            caloriesLeft -= user.protein * 4;

            user.fat = (int)(caloriesLeft * (0.35f / 9f));
            caloriesLeft -= user.fat * 9;

            user.carbohydrates = (int)(caloriesLeft / 4f);
        }
    }
}