namespace HealthBuddy.Calculator
{
    using HealthBuddy.Interfaces;

   public abstract class MenDietCalculator : DietCalculator, IDietCalculator
    {
        public MenDietCalculator(double weight, double height, int age) : base(weight, height, age) { }
    }
}
