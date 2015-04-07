using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Interfaces
{
    public interface IDietCalculator
    {
         int AgeOfPerson { get; set; }

         double HeightOfPerson { get; set; }

         double WeightOfPerson { get; set; }
    }
}
