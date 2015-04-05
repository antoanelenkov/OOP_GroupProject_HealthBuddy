namespace HealthBuddy.Models
{
    using System.Collections.Generic;

    public class Liquid : Meal
    {
        public Liquid()
            :base()
        {
            
        }
        public Liquid(string name, decimal calories, decimal proteins, decimal carbs, decimal fats, decimal portionSize, decimal caloriesPerPortion, string ingredients)
            :base(name, calories, proteins, carbs, fats, portionSize, caloriesPerPortion, ingredients)
        {
            
        }
    }
}
