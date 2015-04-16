namespace HealthBuddy.Engine
{
    using System.Windows.Controls;

    using HealthBuddy.Calculator;
    using HealthBuddy.Enums;
    using HealthBuddy.Models;
    using System.Collections.Generic;    

    public abstract class InteractionManager
    {
        public static void SwitchToWindow(IEnumerable<Canvas> windowsCollection, Canvas windowToShow)
        {
            foreach (var page in windowsCollection)
            {
                if (page.Name != windowToShow.Name)
                {
                    page.Visibility = System.Windows.Visibility.Hidden;
                }
                windowToShow.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public static int CalculateUserCalories(User user, int userCalories, TextBlock userCaloriesInfo)
        {
            if (user.Gender == UserGender.Male)
            {
                MenCaloriesCalculator calcCalories = new MenCaloriesCalculator(user.Weight, user.Height, user.Age, UserPurpose.Keep_Weight);
                userCalories = calcCalories.CalculateCalories();
            }
            else if (user.Gender == UserGender.Female)
            {
                WomanCaloriesCalculator calcCalories = new WomanCaloriesCalculator(user.Weight, user.Height, user.Age, UserPurpose.Keep_Weight);
                userCalories = calcCalories.CalculateCalories();
            }
            userCaloriesInfo.Text = "Your calories:  " + userCalories;
            return userCalories;
        }

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
