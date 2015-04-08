namespace HealthBuddy.Models
{
    using System;
    using System.Collections.Generic;

    public struct History
    {
        public DateTime Date;
        public ICollection<KeyValuePair<Meal, int>> Menu;
    }
}
