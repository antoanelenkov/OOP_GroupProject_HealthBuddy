using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Calculator
{
    class MenWaterNeedsCalculator:MenDietCalculator
    {
        //very complicated calculations(empiric from my expirience :D)
         private const double WEIGHT_COEF = 2.84;


         public MenWaterNeedsCalculator(double weight, double height, int age) : base(weight, height, age) { }

         public double CalculateWaterNeeds()
         {
             double formula = WEIGHT_COEF * this.WeightOfPerson/100;
             return formula;
         }
    }
}
