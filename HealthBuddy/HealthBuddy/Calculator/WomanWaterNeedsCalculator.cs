namespace HealthBuddy.Calculator
{
    using HealthBuddy.Interfaces;

    public class WomanWaterNeedsCalculator : WomenDietCalculator, IDietCalculator
    {
        //very complicated calculations(empiric from my expirience :D)
        private const double WEIGHT_COEF = 2.63;

        public WomanWaterNeedsCalculator(double weight, double height, int age) : base(weight, height, age) { }

        public double CalculateWaterNeeds()
        {
            double formula = WEIGHT_COEF * this.WeightOfPerson / 100;
            return formula;
        }
    }
}
