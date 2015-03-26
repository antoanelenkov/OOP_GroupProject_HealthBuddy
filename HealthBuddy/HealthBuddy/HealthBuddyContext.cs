using HealthBuddy.Models;

namespace HealthBuddy
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class HealthBuddyContext : DbContext
    {
        public HealthBuddyContext()
            : base("name=HealthBuddyContext")
        {
            
        }

        public virtual DbSet<Appetiser> Appetisers { get; set; }

        public virtual DbSet<Breakfast> Breakfasts { get; set; }

        public virtual DbSet<Dessert> Desserts { get; set; }

        public virtual DbSet<Liquid> Liquids { get; set; }

        public virtual DbSet<Main> Mains { get; set; }

        public virtual DbSet<Salad> Salads { get; set; }

        public virtual DbSet<Soup> Soups { get; set; }

    }
}