namespace app
{
    static class Calculator
    {
        public static void CalculateBMI(User user)
        {
            user.BMI = user.weight / (user.height / 100f * user.height / 100f);
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
            user.activityLevel += user.trainingsInWeek * 0.5f;
            user.activityLevel += user.dailyMovementLevel * 0.2f;

        }
    }
}
