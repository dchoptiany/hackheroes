using System;
using System.Windows.Forms;

namespace app
{
    static class Calculator
    {
        public static bool CalculateBMI(User user)
        {
            try
            {
                if(user.height == 0)
                {
                    throw(new DivideByZeroException("Wzrost użytkownika jest równy zero."));
                }
                user.BMI = user.weight / (user.height / 100f * user.height / 100f);
                return true;
            }
            catch(DivideByZeroException exception)
            {
                MessageBox.Show("Wystąpił błąd podczas obliczania BMI.\nSprawdź poprawność danch w zakładce Profile i spróbuj ponownie.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public static void CalculateMacro(User user)
        {
            float rmr = user.height * 6.25f + user.weight * 10f - (user.age * 5f);
            rmr += user.gender == Gender.Male ? 5f : -161f;

            user.calories = (int)(rmr * user.activityLevel);
            float caloriesLeft = user.calories;

            user.protein = (int)user.weight * 2;
            caloriesLeft -= user.protein * 4;

            user.fat = (int)(caloriesLeft * (0.35f / 9f));
            caloriesLeft -= user.fat * 9;

            user.carbohydrates = (int)(caloriesLeft / 4f);
        }

        public static void CalculateActivityLevel(User user)
        {
            user.activityLevel = 1.1f;
            if (user.physicalJob)
            {
                user.activityLevel += 0.1f;
            }
            user.activityLevel += user.trainingsInWeek * 0.05f;
            user.activityLevel += user.dailyMovementLevel * 0.025f;

        }
    }
}
