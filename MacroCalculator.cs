using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void CalculateMacro(float _weight, float _height, int _age, Gender _gender)
        {
            if (_gender == Gender.Male)
            {
                var rmr = _height * 6.25f + _weight * 10f - (_age * 5f) + 5f;

                calories = (int)rmr;
                float caloriesLeft = calories;

                protein = (int)_weight * 2;
                caloriesLeft -= protein * 4;

                fat = (int)(caloriesLeft * 0.25f);
                caloriesLeft -= fat * 9;

                carbohydrates = (int)(caloriesLeft / 4f);
            }
            else
            {
                var rmr = _height * 6.25f + _weight * 10f - (_age * 5f) - 161f;

                calories = (int)rmr;
                float caloriesLeft = calories;

                protein = (int)_weight * 2;
                caloriesLeft -= protein * 4;

                fat = (int)(caloriesLeft * 0.25f);
                caloriesLeft -= fat * 9;

                carbohydrates = (int)(caloriesLeft / 4f);
            }
        }
    }
}
