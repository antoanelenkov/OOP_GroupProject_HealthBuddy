namespace HealthBuddy.Calculator
{
    using HealthBuddy.Interfaces;

    public abstract class WomenDietCalculator : DietCalculator, IDietCalculator
    {
        public WomenDietCalculator(double weight, double height, int age)
            : base(weight, height, age)
        {

        }
    }
}
