using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Calculator
{
    abstract class WomenDietCalculator:DietCalculator
    {
        public WomenDietCalculator(double weight, double height, int age) : base(weight, height, age) { }
    }
}
