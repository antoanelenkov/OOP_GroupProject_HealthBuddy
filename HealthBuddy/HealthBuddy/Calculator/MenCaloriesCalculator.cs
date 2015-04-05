using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Calculator
{
    /// <summary>
    /// Calculates BMR by different formula depending on the gender       (man)
    /// </summary>
    /// <returns></returns>
     class MenCaloriesCalculator:MenDietCalculator
    {
        //since the progress of science is growing exponentially, tomorrow these constants could be different :)
         private const int START_COEF = 66;
         private const double WEIGHT_COEF = 13.7;
         private const double HEIGHT_COEF = 5;
         private const double AGE_COEF = 6.8;
         public UserPurpose Purpose { get; set; }

         public MenCaloriesCalculator(double weight, double height, int age,UserPurpose purpose) : base(weight, height, age) 
         {
             this.Purpose = purpose;
         }

         public int CalculateCalories()
         {
             int formula = (int)(START_COEF + WEIGHT_COEF * this.WeightOfPerson + HEIGHT_COEF * this.HeightOfPerson - AGE_COEF * this.AgeOfPerson);
             return formula;
         }
    }
}
