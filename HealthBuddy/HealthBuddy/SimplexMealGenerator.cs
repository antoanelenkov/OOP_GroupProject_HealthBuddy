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
        SimplexMealGenerator(List<Meal> Meals, int Calories = 0, int Proteins = 0, int Carbohydrates = 0, int Fats = 0)
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
                solver.SetBounds(portions, 0, 3);
                solver.SetIntegrality(p, true);
                solver.SetCoefficient(Calories, portions, (int)m.Calories);
                solver.SetCoefficient(Proteins, portions, (int)m.Proteins);
                solver.SetCoefficient(Carbohydrates, portions, (int)m.Carbohydrates);
                solver.SetCoefficient(Fats, portions, (int)m.Fats);
                solver.SetCoefficient(cost, portions, rand.Next(0, 100));
                mealPortions.Add(portions);
            }
            solver.SetBounds(Calories, this.calories, this.calories);
            //solver.SetBounds(Proteins, this.proteins, this.proteins);
            //solver.SetBounds(Carbohydrates, this.carbohydrates, this.carbohydrates);
            //solver.SetBounds(Fats, this.fats, this.fats);
            solver.AddGoal(cost, 1, true);
            solver.Solve(new SimplexSolverParams());
            
        }
    }
}
