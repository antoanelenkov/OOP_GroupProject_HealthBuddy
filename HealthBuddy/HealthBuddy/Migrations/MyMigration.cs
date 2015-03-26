namespace HealthBuddy.Migrations
{
    using HealthBuddy.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    public class MyMigration : DropCreateDatabaseIfModelChanges<HealthBuddyContext>
    {
        protected override void Seed(HealthBuddyContext context)
        {
            context.Desserts.Add(new Dessert("Tiramissu", 200m, 150m, 25, 25, 150, 56, new List<string> { "nuts" }));
            context.Salads.Add(new Salad("TestSalad", 200m, 150m, 25, 25, 150, 56, new List<string> { "nuts" }));    
            context.SaveChanges();

        }
    }
}
