namespace HealthBuddy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    using HealthBuddy.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HealthBuddy.HealthBuddyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HealthBuddy.HealthBuddyContext context)
        {
            // TODO: Delete Testing Salads and add ones with real values
            context.Salads.AddOrUpdate(
                record => record.Name,
                 new Salad
                 {
                     Name = "Mixed Salad",
                     Calories = 80,
                     Carbohydrates = 40,
                     Proteins = 40,
                     Fats = 20,
                     Portion_Size = 250,
                     Calories_Per_Portions = 0,
                     Ingredients = "vegetables"
                 },
                new Salad
                {
                    Name = "Kapreze",
                    Calories = 100,
                    Carbohydrates = 80,
                    Proteins = 20,
                    Fats = 0,
                    Portion_Size = 310,
                    Calories_Per_Portions = 0,
                    Ingredients = "vegetables"
                },
                    new Salad
                    {
                        Name = "Whitesnow",
                        Calories = 400,
                        Carbohydrates = 200,
                        Proteins = 100,
                        Fats = 20,
                        Portion_Size = 300,
                        Calories_Per_Portions = 0,
                        Ingredients = "vegetables milk"
                    }
                );
            #region Add Mains to DB
            context.Mains.AddOrUpdate(
                record => record.Name,
                   new Main { Name = "turkey breast", Calories = 112m, Carbohydrates = 20m, Proteins = 0m, Fats = 3.6m, Portion_Size = 150m, Calories_Per_Portions = 168.6m, Ingredients = "meat" },
                   new Main { Name = "roast chicken with rice", Calories = 141m, Carbohydrates = 6m, Proteins = 17.2m, Fats = 5.3m, Portion_Size = 300m, Calories_Per_Portions = 421.5m, Ingredients = "meat  grain" },
                   new Main { Name = " grilled chicken breast", Calories = 96m, Carbohydrates = 21m, Proteins = 0m, Fats = 1.3m, Portion_Size = 200m, Calories_Per_Portions = 191.4m, Ingredients = "meat" },
                   new Main { Name = "grilled chicken feet", Calories = 178m, Carbohydrates = 16.5m, Proteins = 0.2m, Fats = 12.4m, Portion_Size = 200m, Calories_Per_Portions = 356.8m, Ingredients = "meat" },
                   new Main { Name = "grilled chicken liver", Calories = 143m, Carbohydrates = 21m, Proteins = 0.9m, Fats = 6.2m, Portion_Size = 200m, Calories_Per_Portions = 42m, Ingredients = "meat" },
                   new Main { Name = "grilled beef steak", Calories = 142m, Carbohydrates = 23m, Proteins = 0m, Fats = 5.6m, Portion_Size = 200m, Calories_Per_Portions = 284.8m, Ingredients = "meat" },
                   new Main { Name = "roast beef lunchmeat", Calories = 119m, Carbohydrates = 22m, Proteins = 1m, Fats = 3m, Portion_Size = 200m, Calories_Per_Portions = 238m, Ingredients = "meat" },
                   new Main { Name = "beef meatloaf", Calories = 210m, Carbohydrates = 16m, Proteins = 9.5m, Fats = 12m, Portion_Size = 300m, Calories_Per_Portions = 630m, Ingredients = "meat" },
                   new Main { Name = "roast beef heart", Calories = 149m, Carbohydrates = 25.6m, Proteins = 0.1m, Fats = 5.1m, Portion_Size = 250m, Calories_Per_Portions = 371.75m, Ingredients = "meat" },
                   new Main { Name = "fried pork bacon ", Calories = 286m, Carbohydrates = 22m, Proteins = 0m, Fats = 22m, Portion_Size = 100m, Calories_Per_Portions = 286m, Ingredients = "meat" },
                   new Main { Name = "roast pork sausages", Calories = 363m, Carbohydrates = 21m, Proteins = 0m, Fats = 31m, Portion_Size = 150m, Calories_Per_Portions = 544.5m, Ingredients = "meat" },
                   new Main { Name = "roast pork tongue", Calories = 260m, Carbohydrates = 20m, Proteins = 0m, Fats = 20m, Portion_Size = 200m, Calories_Per_Portions = 520m, Ingredients = "meat" },
                   new Main { Name = "grilled pork ribs", Calories = 300m, Carbohydrates = 20m, Proteins = 10m, Fats = 20m, Portion_Size = 150m, Calories_Per_Portions = 450m, Ingredients = "meat" },
                   new Main { Name = "grilled pork steak with BBQ saus", Calories = 260m, Carbohydrates = 20m, Proteins = 0m, Fats = 20m, Portion_Size = 200m, Calories_Per_Portions = 520m, Ingredients = "meat" },
                   new Main { Name = "roast lamb leg", Calories = 190m, Carbohydrates = 22m, Proteins = 0m, Fats = 11.3m, Portion_Size = 250m, Calories_Per_Portions = 474.25m, Ingredients = "meat" },
                   new Main { Name = "breast lamb chops", Calories = 416m, Carbohydrates = 19.5m, Proteins = 0m, Fats = 37.5m, Portion_Size = 130m, Calories_Per_Portions = 540.15m, Ingredients = "meat" },
                   new Main { Name = "grilled salmon", Calories = 170m, Carbohydrates = 20m, Proteins = 0m, Fats = 10m, Portion_Size = 300m, Calories_Per_Portions = 510m, Ingredients = "fish" },
                   new Main { Name = "grilled salmon with fried rise ", Calories = 125m, Carbohydrates = 10m, Proteins = 10m, Fats = 5m, Portion_Size = 200m, Calories_Per_Portions = 250m, Ingredients = "fish" },
                   new Main { Name = "cooked haddock", Calories = 101m, Carbohydrates = 23m, Proteins = 0m, Fats = 1m, Portion_Size = 200m, Calories_Per_Portions = 202m, Ingredients = "fish" },
                   new Main { Name = "smoked haddock", Calories = 119m, Carbohydrates = 27.4m, Proteins = 0m, Fats = 1m, Portion_Size = 200m, Calories_Per_Portions = 237.2m, Ingredients = "fish" },
                   new Main { Name = "smoked carp", Calories = 164m, Carbohydrates = 24m, Proteins = 0m, Fats = 7.5m, Portion_Size = 200m, Calories_Per_Portions = 327m, Ingredients = "fish" },
                   new Main { Name = "cooked carp", Calories = 135m, Carbohydrates = 19m, Proteins = 0m, Fats = 6.5m, Portion_Size = 200m, Calories_Per_Portions = 269m, Ingredients = "fish" },
                //  new Main { Name = "medium boiled egg", Calories = 156m, Carbohydrates = 14m, Proteins = 1.2m, Fats = 10.6m, Portion_Size = 50m, Calories_Per_Portions = 78.1m, Ingredients = "milk" },
                   new Main { Name = "medium tomato", Calories = 25m, Carbohydrates = 1.1m, Proteins = 4.5m, Fats = 0.3m, Portion_Size = 123m, Calories_Per_Portions = 30.873m, Ingredients = "vegetables" },
                   new Main { Name = "half cucumber ", Calories = 17m, Carbohydrates = 0.6m, Proteins = 3.5m, Fats = 0.1m, Portion_Size = 150m, Calories_Per_Portions = 25.95m, Ingredients = "vegetables" },
                   new Main { Name = "baked sweet potato", Calories = 101m, Carbohydrates = 2.2m, Proteins = 22.5m, Fats = 0.2m, Portion_Size = 150m, Calories_Per_Portions = 150.9m, Ingredients = "vegetables" },
                   new Main { Name = "baked potato", Calories = 127m, Carbohydrates = 3.2m, Proteins = 28m, Fats = 0.2m, Portion_Size = 270m, Calories_Per_Portions = 341.82m, Ingredients = "vegetables" },
                // new Main { Name = "almonds", Calories = 644m, Carbohydrates = 22.9m, Proteins = 23.4m, Fats = 51m, Portion_Size = 50m, Calories_Per_Portions = 322.1m, Ingredients = "nuts" },
                //  new Main { Name = "walnuts", Calories = 615m, Carbohydrates = 15.2m, Proteins = 13.7m, Fats = 55.5m, Portion_Size = 50m, Calories_Per_Portions = 307.55m, Ingredients = "nuts" },
                //  new Main { Name = "hazelnuts", Calories = 717m, Carbohydrates = 15.2m, Proteins = 17.8m, Fats = 65m, Portion_Size = 50m, Calories_Per_Portions = 358.5m, Ingredients = "nuts" },
                   new Main { Name = "cashew", Calories = 576m, Carbohydrates = 18m, Proteins = 27m, Fats = 44m, Portion_Size = 50m, Calories_Per_Portions = 288m, Ingredients = "nuts" },
                //   new Main { Name = "walnuts", Calories = 673m, Carbohydrates = 15m, Proteins = 7m, Fats = 65m, Portion_Size = 50m, Calories_Per_Portions = 336.5m, Ingredients = "nuts" },
                //   new Main { Name = "peanuts", Calories = 573m, Carbohydrates = 26m, Proteins = 7m, Fats = 49m, Portion_Size = 50m, Calories_Per_Portions = 286.5m, Ingredients = "nuts" },
                   new Main { Name = "sunflower seeds", Calories = 571m, Carbohydrates = 22.8m, Proteins = 8.3m, Fats = 49.6m, Portion_Size = 50m, Calories_Per_Portions = 285.4m, Ingredients = "nuts" },
                   new Main { Name = "wheat bread", Calories = 244m, Carbohydrates = 7.1m, Proteins = 51.4m, Fats = 1.1m, Portion_Size = 50m, Calories_Per_Portions = 121.95m, Ingredients = "grain" },
                //   new Main { Name = "rye bread", Calories = 231m, Carbohydrates = 6.8m, Proteins = 48.3m, Fats = 1.2m, Portion_Size = 50m, Calories_Per_Portions = 115.6m, Ingredients = "grain" },
                //   new Main { Name = "brown bread", Calories = 234m, Carbohydrates = 8m, Proteins = 46.6m, Fats = 1.7m, Portion_Size = 50m, Calories_Per_Portions = 116.85m, Ingredients = "grain" },
                   new Main { Name = "boiled white rice", Calories = 332m, Carbohydrates = 2.2m, Proteins = 80m, Fats = 0.3m, Portion_Size = 100m, Calories_Per_Portions = 331.5m, Ingredients = "grain" },
                   new Main { Name = "boiled brown rice", Calories = 316m, Carbohydrates = 2.3m, Proteins = 75m, Fats = 0.8m, Portion_Size = 100m, Calories_Per_Portions = 316.4m, Ingredients = "grain" },
                   new Main { Name = "cooked green beans", Calories = 275m, Carbohydrates = 2.5m, Proteins = 65m, Fats = 0.5m, Portion_Size = 100m, Calories_Per_Portions = 274.5m, Ingredients = "legumes" },
                   new Main { Name = "cooked brown beans", Calories = 241m, Carbohydrates = 2.9m, Proteins = 54m, Fats = 1.5m, Portion_Size = 100m, Calories_Per_Portions = 241.1m, Ingredients = "legumes" },
                   new Main { Name = "cooked chickpeas", Calories = 145m, Carbohydrates = 6.5m, Proteins = 22.5m, Fats = 3.2m, Portion_Size = 100m, Calories_Per_Portions = 144.8m, Ingredients = "legumes" },
                   new Main { Name = "cooked black beans", Calories = 108m, Carbohydrates = 7.5m, Proteins = 18.5m, Fats = 0.4m, Portion_Size = 100m, Calories_Per_Portions = 107.6m, Ingredients = "legumes" },
                   new Main { Name = "pasta with chicken breast", Calories = 147m, Carbohydrates = 14m, Proteins = 16m, Fats = 3m, Portion_Size = 300m, Calories_Per_Portions = 441m, Ingredients = "grain meat" }
                );
            #endregion
            # region Add Breakfast to DB
            context.Breakfasts.AddOrUpdate(
                record => record.Name,
                new Breakfast { Name = "medium boiled egg", Calories = 156m, Carbohydrates = 14m, Proteins = 1.2m, Fats = 10.6m, Portion_Size = 50m, Calories_Per_Portions = 78.1m, Ingredients = "milk" },
                new Breakfast { Name = "whole milk", Calories = 64m, Carbohydrates = 3m, Proteins = 5m, Fats = 3.6m, Portion_Size = 200m, Calories_Per_Portions = 128.8m, Ingredients = "milk" },
                new Breakfast { Name = "2% fat milk", Calories = 50m, Carbohydrates = 3m, Proteins = 5m, Fats = 2m, Portion_Size = 200m, Calories_Per_Portions = 100m, Ingredients = "milk" },
                new Breakfast { Name = "0.1% fat milk", Calories = 33m, Carbohydrates = 3m, Proteins = 5m, Fats = 0.1m, Portion_Size = 200m, Calories_Per_Portions = 65.8m, Ingredients = "milk" },
                new Breakfast { Name = "piece of cheddar cheese(kashkaval)", Calories = 434m, Carbohydrates = 26m, Proteins = 1.5m, Fats = 36m, Portion_Size = 50m, Calories_Per_Portions = 217m, Ingredients = "milk" },
                new Breakfast { Name = "piece of feta cheese(sirene)", Calories = 296m, Carbohydrates = 16m, Proteins = 4m, Fats = 24m, Portion_Size = 50m, Calories_Per_Portions = 148m, Ingredients = "milk" },
                new Breakfast { Name = "cup of cottage cheese", Calories = 103m, Carbohydrates = 12m, Proteins = 3.6m, Fats = 4.5m, Portion_Size = 210m, Calories_Per_Portions = 216.09m, Ingredients = "milk" },
                new Breakfast { Name = "medium banana", Calories = 103m, Carbohydrates = 1.1m, Proteins = 24m, Fats = 0.3m, Portion_Size = 115m, Calories_Per_Portions = 118.565m, Ingredients = "fruit" },
                new Breakfast { Name = "medium orange", Calories = 55m, Carbohydrates = 0.9m, Proteins = 12.5m, Fats = 0.15m, Portion_Size = 131m, Calories_Per_Portions = 71.9845m, Ingredients = "fruit" },
                new Breakfast { Name = "medium apple", Calories = 59m, Carbohydrates = 0.3m, Proteins = 13.9m, Fats = 0.2m, Portion_Size = 180m, Calories_Per_Portions = 105.48m, Ingredients = "fruit" },
                new Breakfast { Name = "cup of grape", Calories = 81m, Carbohydrates = 0.8m, Proteins = 19m, Fats = 0.15m, Portion_Size = 150m, Calories_Per_Portions = 120.825m, Ingredients = "fruit" },
                new Breakfast { Name = "piece of watermelon", Calories = 61m, Carbohydrates = 0.6m, Proteins = 14.1m, Fats = 0.25m, Portion_Size = 290m, Calories_Per_Portions = 177.045m, Ingredients = "fruit" },
                new Breakfast { Name = "almonds", Calories = 644m, Carbohydrates = 22.9m, Proteins = 23.4m, Fats = 51m, Portion_Size = 50m, Calories_Per_Portions = 322.1m, Ingredients = "nuts" },
                // new Breakfast { Name = "walnuts", Calories = 615m, Carbohydrates = 15.2m, Proteins = 13.7m, Fats = 55.5m, Portion_Size = 50m, Calories_Per_Portions = 307.55m, Ingredients = "nuts" },
                new Breakfast { Name = "hazelnuts", Calories = 717m, Carbohydrates = 15.2m, Proteins = 17.8m, Fats = 65m, Portion_Size = 50m, Calories_Per_Portions = 358.5m, Ingredients = "nuts" },
                new Breakfast { Name = "cashew", Calories = 576m, Carbohydrates = 18m, Proteins = 27m, Fats = 44m, Portion_Size = 50m, Calories_Per_Portions = 288m, Ingredients = "nuts" },
                new Breakfast { Name = "walnuts", Calories = 673m, Carbohydrates = 15m, Proteins = 7m, Fats = 65m, Portion_Size = 50m, Calories_Per_Portions = 336.5m, Ingredients = "nuts" },
                new Breakfast { Name = "peanuts", Calories = 573m, Carbohydrates = 26m, Proteins = 7m, Fats = 49m, Portion_Size = 50m, Calories_Per_Portions = 286.5m, Ingredients = "nuts" },
                //  new Breakfast { Name = "sunflower seeds", Calories = 571m, Carbohydrates = 22.8m, Proteins = 8.3m, Fats = 49.6m, Portion_Size = 50m, Calories_Per_Portions = 285.4m, Ingredients = "nuts" },
                new Breakfast { Name = "wheat bread", Calories = 244m, Carbohydrates = 7.1m, Proteins = 51.4m, Fats = 1.1m, Portion_Size = 50m, Calories_Per_Portions = 121.95m, Ingredients = "grain" },
                new Breakfast { Name = "rye bread", Calories = 231m, Carbohydrates = 6.8m, Proteins = 48.3m, Fats = 1.2m, Portion_Size = 50m, Calories_Per_Portions = 115.6m, Ingredients = "grain" },
                new Breakfast { Name = "brown bread", Calories = 243m, Carbohydrates = 8m, Proteins = 46.6m, Fats = 1.7m, Portion_Size = 50m, Calories_Per_Portions = 116.85m, Ingredients = "grain" }
);
            #endregion
            #region Add Soups to DB
            context.Soups.AddOrUpdate(
                record => record.Name,
                new Soup { Name = "bean soup", Calories = 72m, Carbohydrates = 4m, Proteins = 10m, Fats = 1.8m, Portion_Size = 350m, Calories_Per_Portions = 252.7m, Ingredients = "legumes" },
                new Soup { Name = "pea soup", Calories = 75m, Carbohydrates = 4.4m, Proteins = 8.9m, Fats = 2.4m, Portion_Size = 350m, Calories_Per_Portions = 261.8m, Ingredients = "legumes" },
                new Soup { Name = "mushroom soup", Calories = 27m, Carbohydrates = 1.3m, Proteins = 1.3m, Fats = 1.8m, Portion_Size = 350m, Calories_Per_Portions = 93.1m, Ingredients = "vegetables" },
                new Soup { Name = "tomato soup", Calories = 30m, Carbohydrates = 1.3m, Proteins = 1.8m, Fats = 1.9m, Portion_Size = 350m, Calories_Per_Portions = 103.25m, Ingredients = "vegetables" },
                new Soup { Name = "tomato soup with pasta", Calories = 35m, Carbohydrates = 1.5m, Proteins = 3.3m, Fats = 1.8m, Portion_Size = 350m, Calories_Per_Portions = 123.9m, Ingredients = "vegetables grain" },
                new Soup { Name = "vegetable soup", Calories = 48m, Carbohydrates = 1.7m, Proteins = 6.2m, Fats = 1.8m, Portion_Size = 350m, Calories_Per_Portions = 167.3m, Ingredients = "vegetables" },
                new Soup { Name = "potato soup", Calories = 54m, Carbohydrates = 1.3m, Proteins = 9.5m, Fats = 1.2m, Portion_Size = 350m, Calories_Per_Portions = 189m, Ingredients = "vegetables" },
                new Soup { Name = "fruit soup with apples", Calories = 49m, Carbohydrates = 0.1m, Proteins = 11.8m, Fats = 0.1m, Portion_Size = 350m, Calories_Per_Portions = 169.75m, Ingredients = "fruit" },
                new Soup { Name = "fish soup ", Calories = 45m, Carbohydrates = 3.4m, Proteins = 5.5m, Fats = 1m, Portion_Size = 350m, Calories_Per_Portions = 156.1m, Ingredients = "fish" },
                new Soup { Name = "spanich soup", Calories = 43m, Carbohydrates = 2.2m, Proteins = 6.6m, Fats = 0.9m, Portion_Size = 350m, Calories_Per_Portions = 151.55m, Ingredients = "vegetables" },
                new Soup { Name = "green beans soup", Calories = 68m, Carbohydrates = 3.2m, Proteins = 11.2m, Fats = 1.2m, Portion_Size = 350m, Calories_Per_Portions = 239.4m, Ingredients = "legumes" },
                new Soup { Name = "shkembe soup", Calories = 84m, Carbohydrates = 16m, Proteins = 0m, Fats = 2.2m, Portion_Size = 350m, Calories_Per_Portions = 293.3m, Ingredients = "meat" }
);
            #endregion
            #region Add Dessert to DB
            context.Desserts.AddOrUpdate(
                record => record.Name,
                //new Dessert { Name = "whole milk", Calories = 64m, Carbohydrates = 3m, Proteins = 5m, Fats = 3.6m, Portion_Size = 200m, Calories_Per_Portions = 128.8m, Ingredients = "milk" },
                //new Dessert { Name = "2% fat milk", Calories = 50m, Carbohydrates = 3m, Proteins = 5m, Fats = 2m, Portion_Size = 200m, Calories_Per_Portions = 100m, Ingredients = "milk" },
                //new Dessert { Name = "0.1% fat milk", Calories = 33m, Carbohydrates = 3m, Proteins = 5m, Fats = 0.1m, Portion_Size = 200m, Calories_Per_Portions = 65.8m, Ingredients = "milk" },
                //new Dessert { Name = "cup of cottage cheese", Calories = 103m, Carbohydrates = 12m, Proteins = 3.6m, Fats = 4.5m, Portion_Size = 210m, Calories_Per_Portions = 216.09m, Ingredients = "milk" },
                //new Dessert { Name = "medium banana", Calories = 103m, Carbohydrates = 1.1m, Proteins = 24m, Fats = 0.3m, Portion_Size = 115m, Calories_Per_Portions = 118.565m, Ingredients = "fruit" },
                //new Dessert { Name = "medium orange", Calories = 55m, Carbohydrates = 0.9m, Proteins = 12.5m, Fats = 0.15m, Portion_Size = 131m, Calories_Per_Portions = 71.9845m, Ingredients = "fruit" },
                //new Dessert { Name = "medium apple", Calories = 59m, Carbohydrates = 0.3m, Proteins = 13.9m, Fats = 0.2m, Portion_Size = 180m, Calories_Per_Portions = 105.48m, Ingredients = "fruit" },
                //new Dessert { Name = "cup of grape", Calories = 81m, Carbohydrates = 0.8m, Proteins = 19m, Fats = 0.15m, Portion_Size = 150m, Calories_Per_Portions = 120.825m, Ingredients = "fruit" },
                //new Dessert { Name = "piece of watermelon", Calories = 61m, Carbohydrates = 0.6m, Proteins = 14.1m, Fats = 0.25m, Portion_Size = 290m, Calories_Per_Portions = 177.045m, Ingredients = "fruit" },
                //new Dessert { Name = "almond cake", Calories = 310m, Carbohydrates = 8.5m, Proteins = 65.5m, Fats = 1.6m, Portion_Size = 130m, Calories_Per_Portions = 403.52m, Ingredients = "nuts sweet milk" },
                new Dessert { Name = "cacao bisquits", Calories = 444m, Carbohydrates = 6.1m, Proteins = 51.4m, Fats = 23.8m, Portion_Size = 130m, Calories_Per_Portions = 577.46m, Ingredients = "grain" },
                new Dessert { Name = "bisquit cake with fruits", Calories = 438m, Carbohydrates = 5.6m, Proteins = 58.8m, Fats = 20m, Portion_Size = 130m, Calories_Per_Portions = 568.88m, Ingredients = "grain milk" },
                new Dessert { Name = "cake with nuts and dry fruits", Calories = 398m, Carbohydrates = 6.4m, Proteins = 53.5m, Fats = 17.6m, Portion_Size = 130m, Calories_Per_Portions = 517.4m, Ingredients = "grain nuts fruit" },
                new Dessert { Name = "cheesecake", Calories = 335m, Carbohydrates = 13.9m, Proteins = 39.7m, Fats = 13.4m, Portion_Size = 130m, Calories_Per_Portions = 435.5m, Ingredients = "milk" },
                new Dessert { Name = "tiramissu", Calories = 385m, Carbohydrates = 5m, Proteins = 35m, Fats = 25m, Portion_Size = 150m, Calories_Per_Portions = 577.5m, Ingredients = "milk" },
                new Dessert { Name = "strawberry with cream", Calories = 132m, Carbohydrates = 1.1m, Proteins = 8.4m, Fats = 10.4m, Portion_Size = 130m, Calories_Per_Portions = 171.08m, Ingredients = "fruit milk" }
);
            #endregion
            #region Add Liquid to DB
            context.Liquids.AddOrUpdate(
                record => record.Name,
                // new Liquid { Name = "cup of orange juice", Calories = 46, Carbohydrates = 0.6m, Proteins = 10.2m, Fats = 0.3m, Portion_Size = 250m, Calories_Per_Portions = 114.75m, Ingredients = "fruit" },
                //new Liquid { Name = "piece of pineapple", Calories = 66, Carbohydrates = 0.7m, Proteins = 15.6m, Fats = 0.1m, Portion_Size = 170m, Calories_Per_Portions = 112.37m, Ingredients = "fruit" },
                //new Liquid { Name = "cup of pineapple juice", Calories = 67, Carbohydrates = 0.5m, Proteins = 16m, Fats = 0.1m, Portion_Size = 250m, Calories_Per_Portions = 167.25m, Ingredients = "fruit" },
               new Liquid { Name = "cup of apple juice", Calories = 46m, Carbohydrates = 0.3m, Proteins = 10.5m, Fats = 0.3m, Portion_Size = 250m, Calories_Per_Portions = 114.75m, Ingredients = "fruit" }
);
            #endregion
            #region Add Appetiser to DB
            context.Appetisers.AddOrUpdate(
                record => record.Name,
                new Appetiser { Name = "piece of cheddar cheese(kashkaval)", Calories = 434m, Carbohydrates = 26m, Proteins = 1.5m, Fats = 36m, Portion_Size = 50m, Calories_Per_Portions = 217m, Ingredients = "milk" },
                new Appetiser { Name = "piece of feta cheese(sirene)", Calories = 296m, Carbohydrates = 16m, Proteins = 4m, Fats = 24m, Portion_Size = 50m, Calories_Per_Portions = 148m, Ingredients = "milk" },
                new Appetiser { Name = "cup of cottage cheese", Calories = 103m, Carbohydrates = 12m, Proteins = 3.6m, Fats = 4.5m, Portion_Size = 210m, Calories_Per_Portions = 216.09m, Ingredients = "milk" },
                //  new Appetiser { Name = "almonds", Calories = 644m, Carbohydrates = 22.9m, Proteins = 23.4m, Fats = 51m, Portion_Size = 50m, Calories_Per_Portions = 322.1m, Ingredients = "nuts" },
                //  new Appetiser { Name = "walnuts", Calories = 615m, Carbohydrates = 15.2m, Proteins = 13.7m, Fats = 55.5m, Portion_Size = 50m, Calories_Per_Portions = 307.55m, Ingredients = "nuts" },
                //  new Appetiser { Name = "hazelnuts", Calories = 717m, Carbohydrates = 15.2m, Proteins = 17.8m, Fats = 65m, Portion_Size = 50m, Calories_Per_Portions = 358.5m, Ingredients = "nuts" },
                new Appetiser { Name = "cashew", Calories = 576m, Carbohydrates = 18m, Proteins = 27m, Fats = 44m, Portion_Size = 50m, Calories_Per_Portions = 288m, Ingredients = "nuts" },
                //   new Appetiser { Name = "walnuts", Calories = 673m, Carbohydrates = 15m, Proteins = 7m, Fats = 65m, Portion_Size = 50m, Calories_Per_Portions = 336.5m, Ingredients = "nuts" },
                //   new Appetiser { Name = "peanuts", Calories = 573m, Carbohydrates = 26m, Proteins = 7m, Fats = 49m, Portion_Size = 50m, Calories_Per_Portions = 286.5m, Ingredients = "nuts" },
                new Appetiser { Name = "sunflower seeds", Calories = 571m, Carbohydrates = 22.8m, Proteins = 8.3m, Fats = 49.6m, Portion_Size = 50m, Calories_Per_Portions = 285.4m, Ingredients = "nuts" }
);
            #endregion
        }
    }
}
