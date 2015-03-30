namespace HealthBuddy.Models
{   
    using System.Collections.Generic;

    public class JustMenu
    {
        public Appetiser _Appetiser { get; set; }

        public Breakfast _Breakfast { get; set; }

        public Dessert _Dessert { get; set; }

        public Liquid _Liquid_Food { get; set; }

        public Main _Main_Meal { get; set; }

        public Salad _Salad { get; set; }

        public Soup _Soup { get; set; }

        public double Calories { get; set; }

        public List<Meal> LoadPossibleMeals() // takes list of choosen kinds of meal OR one by one e.g Salad, Dessert AND List<> selectedIngrediants
        {
            var possibleMeals = new List<Meal>();
            return possibleMeals;
        }
    }
}
