namespace HealthBuddy
{
    using System;
    using System.Collections.Generic;

    public abstract class Meal
    {
        private double calories_per_portion;

        public Meal()
        {
        }

        public Meal(string name, double calories, double carbs, double proteins, double lipids, double portionSize, List<string> indigredients, double portionCalories)
        {
            this.Name = name;
            this.Calories = calories;
            this.Carbohydrates = carbs;
            this.Proteins = proteins;
            this.Lipids = lipids;
            this.Portion_Size = portionSize;
            this.Indigrediants = indigredients;
            this.Calories_per_portion = portionCalories;
        }

        public string Name { get; set; }

        public double Calories { get; set; } //by 100g

        public double Carbohydrates { get; set; } // may be class

        public double Proteins { get; set; } // may be class

        public double Lipids { get; set; } // may be class

        public double Portion_Size { get; set; } // e.g. 150g (for 1 apple - in the DB)

        public List<string> Indigrediants { get; set; }

        public double Calories_per_portion
        {
            get { return this.calories_per_portion; }
            set { this.calories_per_portion = this.CalcCaloriesPerPortion(); }
        }

        private double CalcCaloriesPerPortion()
        {
            return (this.Calories * this.Portion_Size) / 100;
        }
    }
}
