namespace HealthBuddy.Interfaces
{
    using System.Collections.Generic;

    using HealthBuddy.Models;
    
    public interface ISimplexMealCalculator
    {
        List<Meal> Meals { get; set; } 

        List<int> MealPortions { get; set; }

        int Calories { get; set; }

        int Proteins { get; set; }

        int Carbohydrates { get; set; }

        int Fats { get; set; }

        void Generate();
    }
}
