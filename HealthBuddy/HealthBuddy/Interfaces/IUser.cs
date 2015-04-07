
using HealthBuddy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Interfaces
{
    public interface IUser
    {
        string Name { get; set; }

        int Age { get; set; }

        UserGender Gender { get; set; }

        double Weight { get; set; }

        double Height { get; set; }

        UserPurpose Purpose { get; set; }

         List<string> Choosen_indigrediants { get; set; }
    }
}
