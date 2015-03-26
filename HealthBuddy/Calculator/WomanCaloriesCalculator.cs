using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthBuddy;

namespace HealthBuddy.Calculator
{
    /// <summary>
    /// Calculates BMR by different formula depending on the gender
    /// </summary>
    /// <returns></returns>
    class WomanCaloriesCalculator:WomenDietCalculator
    {
        private const int START_COEF = 655;
        private const double WEIGHT_COEF = 9.6;
        private const double HEIGHT_COEF = 1.8;
        private const double AGE_COEF = 4.7;
        public UserPurpose Purpose { get; set; }

        public WomanCaloriesCalculator(double weight, double height, int age, UserPurpose purpose)
            : base(weight, height, age) 
         {
             this.Purpose = purpose;
         }

        public WomanCaloriesCalculator(double weight, double height, int age) : base(weight, height, age) { }

        public int CalculateCalories()
        {
            int formula = (int)(START_COEF + WEIGHT_COEF * this.WeightOfPerson + HEIGHT_COEF * this.HeightOfPerson - AGE_COEF * this.AgeOfPerson);
            return formula;
        }
    }
}
