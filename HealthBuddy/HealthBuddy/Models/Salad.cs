namespace HealthBuddy.Models
{
    using System.Collections.Generic;

    public class Salad : Meal
    {
        public Salad()
            :base() // TODO: Add constructors for others
        { 
        }

        public Salad(string name, decimal calories, decimal proteins, decimal carbs, decimal fats, decimal portionSize, decimal caloriesPerPortion, string ingredients)
            :base(name, calories, proteins, carbs, fats, portionSize, caloriesPerPortion, ingredients)
        {
            
        }
    }
}
