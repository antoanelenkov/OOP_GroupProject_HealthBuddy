namespace HealthBuddy.Models
{
    using HealthBuddy.Interfaces;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Meal : IMeal
    {
        private string name;
        private decimal calories;
        private decimal proteins;
        private decimal carbohydrates;
        private decimal fats;
        private decimal portionSize;
        private decimal caloriesPerPortion;
        private string ingredients;

        public Meal()
        {
        }
        public Meal(string name, decimal calories, decimal proteins, decimal carbohydrates, decimal fats, decimal portionSize, decimal caloriesPerPortion, string ingredients)
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

        // [Range(0, 100001)]
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

        public string Ingredients
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
            return (this.Calories * this.Portion_Size) / 100m;
        }

        public static bool operator <(Meal first, Meal second)
        {
            if (first.Calories < second.Calories) return true;
            return false;
        }

        public static bool operator >(Meal first, Meal second)
        {
            if (first.Calories > second.Calories) return true;
            return false;
        }

        public static bool Filter(Meal x, List<object> unSelectedIngrediants)
        {
            foreach (var ingredient in unSelectedIngrediants)
            {
                if (x.Ingredients.Contains(ingredient.ToString().ToLower()))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine(string.Format("Calories: {0}", this.Calories));
            result.AppendLine(string.Format("Carbohydrates: {0}", this.Carbohydrates));
            result.AppendLine(string.Format("Proteins: {0}", this.Proteins));
            result.AppendLine(string.Format("Fats: {0}", this.Fats));
            result.AppendLine(string.Format("Portion Size: {0}", this.Portion_Size));
            return result.ToString();
        }
    }
}
