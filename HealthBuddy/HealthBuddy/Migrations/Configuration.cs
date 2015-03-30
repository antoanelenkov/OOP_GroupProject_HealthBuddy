namespace HealthBuddy.Migrations
{
    using HealthBuddy.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;


    internal sealed class Configuration : DbMigrationsConfiguration<HealthBuddy.HealthBuddyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HealthBuddy.HealthBuddyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var milk = new List<string>{"milk"};

            context.Desserts.AddOrUpdate(
                record => record.Name,
                new Dessert { Name = "Raffaelo", Calories = 222, Carbohydrates = 122, Proteins = 50, Fats = 50, 
                         Portion_Size = 150, Calories_Per_Portions = 0, Ingredients =  "nuts" },
                new Dessert { Name = "Tiramissu", Calories = 250, Carbohydrates = 80, Proteins = 65, Fats = 45, 
                         Portion_Size = 265, Calories_Per_Portions = 0, Ingredients =  "milk"  }                          
                );

            context.Salads.AddOrUpdate(
                record => record.Name,
                 new Salad { Name = "Mixed Salad", Calories = 80, Carbohydrates = 40, Proteins = 40, Fats = 20, 
                Portion_Size = 250, Calories_Per_Portions = 0, Ingredients = "vegetables"},
                new Salad { Name = "Kapreze", Calories =100, Carbohydrates = 80, Proteins = 20, Fats =0, 
                    Portion_Size = 310, Calories_Per_Portions = 0, Ingredients = "vegetables" },
                    new Salad { Name = "Whitesnow",Calories = 400, Carbohydrates = 200, Proteins = 100, Fats = 20, 
                    Portion_Size = 300, Calories_Per_Portions = 0, Ingredients = "vegetables milk"}
                );
        }
    }
}
