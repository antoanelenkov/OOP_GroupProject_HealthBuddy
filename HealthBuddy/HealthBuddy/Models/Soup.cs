namespace HealthBuddy.Models
{
    using System.Collections.Generic;

    public class Soup : Meal
    {
        public Soup()
            :base()
        {

        }
        public Soup(string name, decimal calories, decimal proteins, decimal carbs, decimal fats, decimal portionSize, decimal caloriesPerPortion, List<string> ingredients)
            :base(name, calories, proteins, carbs, fats, portionSize, caloriesPerPortion, ingredients)
        {
            
        }
    }
}
