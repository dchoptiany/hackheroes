using System;
using System.Windows.Forms;

namespace app
{
    static class Calculator
    {
        public static void CalculateBMI(User user)
        {
            try
            {
                if(user.height == 0)
                {
                    throw(new DivideByZeroException("Wzrost użytkownika jest równy zero."));
                }
                user.BMI = user.weight / (user.height / 100f * user.height / 100f);
            }
            catch(DivideByZeroException e)
            {
                MessageBox.Show("Wystąpił błąd podczas obliczania BMI. Uzupełnij dane profilu i spróbuj ponownie.\n", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
