namespace HealthBuddy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appetisers",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Calories = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proteins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Carbohydrates = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fats = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portion_Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories_Per_Portions = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Breakfasts",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Calories = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proteins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Carbohydrates = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fats = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portion_Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories_Per_Portions = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Desserts",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Calories = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proteins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Carbohydrates = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fats = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portion_Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories_Per_Portions = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Liquids",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Calories = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proteins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Carbohydrates = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fats = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portion_Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories_Per_Portions = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Mains",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Calories = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proteins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Carbohydrates = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fats = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portion_Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories_Per_Portions = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Salads",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Calories = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proteins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Carbohydrates = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fats = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portion_Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories_Per_Portions = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Soups",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Calories = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proteins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Carbohydrates = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fats = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portion_Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories_Per_Portions = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Soups");
            DropTable("dbo.Salads");
            DropTable("dbo.Mains");
            DropTable("dbo.Liquids");
            DropTable("dbo.Desserts");
            DropTable("dbo.Breakfasts");
            DropTable("dbo.Appetisers");
        }
    }
}
