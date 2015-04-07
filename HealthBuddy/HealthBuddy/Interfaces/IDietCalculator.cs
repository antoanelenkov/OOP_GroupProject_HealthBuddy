using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Interfaces
{
    public interface IDietCalculator
    {
        public int AgeOfPerson { get; set; }

        public double HeightOfPerson { get; set; }

        public double WeightOfPerson { get; set; }
    }
}
