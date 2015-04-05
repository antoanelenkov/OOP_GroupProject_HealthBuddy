using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthBuddy.Models;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using Microsoft.SolverFoundation.Services;

namespace HealthBuddy
{
    class SimplexMealGenerator
    {
        List<Meal> meals;
        List<int> mealPortions;
        private int calories;
        private int proteins;
        private int carbohydrates;
        private int fats;
        public SimplexMealGenerator(List<Meal> Meals, int Calories = 0, int Proteins = 10, int Carbohydrates = 80, int Fats = 10)
        {
            this.meals = Meals;
            calories = Calories;
            proteins = Proteins;
            carbohydrates = Carbohydrates;
            fats = Fats;
        }
        public List<Meal> Meals
        {
            get { return this.meals; }
            set { this.meals = value; }
        }
        public List<int> MealPortions
        {
            get { return this.mealPortions; }
            set { this.mealPortions = value; }
        }
        public int Calories
        {
            get { return this.calories; }
            set { this.calories = value; }
        }

        public int Proteins
        {
            get { return this.proteins; }
            set { this.proteins = value; }
        }
        public int Carbohydrates
        {
            get { return this.carbohydrates; }
            set { this.carbohydrates = value; }
        }

        public int Fats
        {
            get { return this.fats; }
            set { this.fats = value; }
        }

        public void Generate()
        {
            // TODO: Int32 is overloading
            SimplexSolver solver = new SimplexSolver();
            mealPortions = new List<int>();
            int Calories, cost, Proteins, Carbohydrates, Fats;
            solver.AddRow("calories", out Calories);
            solver.SetIntegrality(Calories, true);
            solver.AddRow("proteins", out Proteins);
            solver.SetIntegrality(Calories, true);
            solver.AddRow("carbohydrates", out Carbohydrates);
            solver.SetIntegrality(Calories, true);
            solver.AddRow("fats", out Fats);
            solver.SetIntegrality(Fats, true);
            solver.AddRow("cost", out cost);

            Random rand = new Random();
            foreach (Meal m in this.meals)
            {
                int portions = new int();
                solver.AddVariable(m.Name, out portions);
                solver.SetBounds(portions, 0, 5);
                solver.SetIntegrality(portions, true);
                solver.SetCoefficient(Calories, portions, (int)m.Calories_Per_Portions);
                int prots = ((int)(m.Proteins * m.Portion_Size / 100) - this.proteins) * (-1);
                solver.SetCoefficient(Proteins, portions, prots);
                int carbs = ((int)(m.Carbohydrates * m.Portion_Size / 100) - this.carbohydrates) * (-1);
                solver.SetCoefficient(Carbohydrates, portions, carbs);
                int fa = ((int)(m.Fats * m.Portion_Size / 100) - this.fats) * (-1);
                solver.SetCoefficient(Fats, portions, fa);
                solver.SetCoefficient(cost, portions, rand.Next(0, 100));
                mealPortions.Add(portions);
            }
            solver.SetBounds(Calories, this.calories, this.calories);
            //solver.SetBounds(Proteins, 0, int.MaxValue);
            //solver.SetBounds(Carbohydrates, 0, int.MaxValue);
            //solver.SetBounds(Fats, 0, int.MaxValue);
            solver.AddGoal(cost, 1, true);
            solver.Solve(new SimplexSolverParams());
            for (int i = 0; i < this.mealPortions.Count; i++)
            {
                this.mealPortions[i] = (int)solver.GetValue(this.mealPortions[i]).ToDouble();
            }
        }
    }
}
