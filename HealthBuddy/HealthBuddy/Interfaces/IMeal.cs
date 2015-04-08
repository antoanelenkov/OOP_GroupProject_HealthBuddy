namespace HealthBuddy.Interfaces
{
    public interface IMeal
    {
        
         string Name { get; set; }

         decimal Calories { get; set; }

         decimal Proteins { get; set; }

         decimal Carbohydrates { get; set; }

         decimal Fats { get; set; }

         decimal Portion_Size { get; set; }

         decimal Calories_Per_Portions { get; set; }

         string Ingredients { get; set; }
    }
}
