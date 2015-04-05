namespace HealthBuddy.Models
{
    using System.Collections.Generic;

    public class Breakfast : Meal
    {
        public Breakfast()
            : base()
        { 
        }

        public Breakfast(string name, decimal calories, decimal proteins, decimal carbs, decimal fats, decimal portionSize, decimal caloriesPerPortion, string ingredients)
            :base(name, calories, proteins, carbs, fats, portionSize, caloriesPerPortion, ingredients)
        {
            
        }
    }
}
