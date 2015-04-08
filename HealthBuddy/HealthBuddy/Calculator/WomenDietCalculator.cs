namespace HealthBuddy.Calculator
{
    using HealthBuddy.Interfaces;

    abstract class WomenDietCalculator : DietCalculator, IDietCalculator
    {
        public WomenDietCalculator(double weight, double height, int age)
            : base(weight, height, age)
        {

        }
    }
}
