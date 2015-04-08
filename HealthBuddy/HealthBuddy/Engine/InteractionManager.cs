namespace HealthBuddy.Engine
{
    using HealthBuddy.Models;

    public class InteractionManager
    {

        public static Meal ConvertToTypeMeal(Meal meal, string type)
        {
            switch (type)
            {
                case "Appetiser":
                    return new Appetiser(meal.Name, meal.Calories, meal.Proteins, meal.Carbohydrates, meal.Fats,
                                           meal.Portion_Size, meal.Calories_Per_Portions, meal.Ingredients);
                case "Breakfast": 
                    return new Breakfast(meal.Name, meal.Calories, meal.Proteins, meal.Carbohydrates, meal.Fats,
                                            meal.Portion_Size, meal.Calories_Per_Portions, meal.Ingredients);
                case "Dessert": 
                    return new Dessert(meal.Name, meal.Calories, meal.Proteins, meal.Carbohydrates, meal.Fats,
                                          meal.Portion_Size, meal.Calories_Per_Portions, meal.Ingredients);
                case "Liquid": 
                    return new Liquid(meal.Name, meal.Calories, meal.Proteins, meal.Carbohydrates, meal.Fats,
                                         meal.Portion_Size, meal.Calories_Per_Portions, meal.Ingredients);
                case "Main": 
                    return new Main(meal.Name, meal.Calories, meal.Proteins, meal.Carbohydrates, meal.Fats,
                                       meal.Portion_Size, meal.Calories_Per_Portions, meal.Ingredients);
                case "Salad": 
                    return new Salad(meal.Name, meal.Calories, meal.Proteins, meal.Carbohydrates, meal.Fats,
                                        meal.Portion_Size, meal.Calories_Per_Portions, meal.Ingredients);
                case "Soup": 
                    return new Soup(meal.Name, meal.Calories, meal.Proteins, meal.Carbohydrates, meal.Fats,
                                       meal.Portion_Size, meal.Calories_Per_Portions, meal.Ingredients);

                default: return new Dessert();                   
            }
        }       
    }
}
