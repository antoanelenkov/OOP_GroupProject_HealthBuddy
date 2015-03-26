using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Calculator
{

    abstract class MenDietCalculator:DietCalculator
    {
        public MenDietCalculator(double weight, double height, int age) : base(weight, height, age) { }
    }
}
