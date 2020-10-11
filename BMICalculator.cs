namespace app
{
    static class BMICalculator
    {
        public static void CalculateBmi (ref User user)
        {
            user.BMI = user.weight / (user.height / 100 * user.height / 100);
        }
    }
}
