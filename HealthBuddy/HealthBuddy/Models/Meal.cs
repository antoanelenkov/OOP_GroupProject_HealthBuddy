namespace HealthBuddy.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class Meal
    {
        private string name;
        private decimal calories;
        private decimal proteins;
        private decimal carbohydrates;
        private decimal fats;
        private decimal portionSize;
        private decimal caloriesPerPortion;
        private List<string> ingredients;

        public Meal(string name, decimal calories, decimal proteins, decimal carbohydrates, decimal fats, decimal portionSize, decimal caloriesPerPortion, List<string> ingredients)
        {
            this.Name = name;
            this.Calories = calories;
            this.Proteins = proteins;
            this.Carbohydrates = carbohydrates;
            this.Fats = fats;
            this.Portion_Size = portionSize;
            this.Calories_Per_Portions = caloriesPerPortion;
            this.Ingredients = ingredients;
        }
        
        [Key]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public decimal Calories
        {
            get { return this.calories; }
            set { this.calories = value; }
        }

        public decimal Proteins
        {
            get { return this.proteins; }
            set { this.proteins = value; }
        }

        public decimal Carbohydrates
        {
            get { return this.carbohydrates; }
            set { this.carbohydrates = value; }
        }

        public decimal Fats
        {
            get { return this.fats; }
            set { this.fats = value; }
        }

        public decimal Portion_Size
        {
            get { return this.portionSize; }
            set { this.portionSize = value; }
        }

        public List<string> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }

        public decimal Calories_Per_Portions
        {
            get { return this.caloriesPerPortion; }
            set { this.caloriesPerPortion = this.CalcCaloriesPerPortions(); }
        }

        private decimal CalcCaloriesPerPortions()
        {
            return (this.Calories*this.Portion_Size)/ 100m;
        }
    }
}
