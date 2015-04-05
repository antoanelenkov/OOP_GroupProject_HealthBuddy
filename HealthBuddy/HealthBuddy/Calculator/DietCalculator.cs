using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBuddy.Calculator
{
    abstract class DietCalculator
    {
        private double weightOfPerson;
        private double  heightOfPerson;
        private int ageOfPerson;

        public DietCalculator(double weight,double height,int age)
        {
            this.WeightOfPerson = weight;
            this.HeightOfPerson = height;
            this.AgeOfPerson = age;
        }

        public int AgeOfPerson
        {
            get { return ageOfPerson; }
            set
            {
                if (value < 1) { throw new IndexOutOfRangeException("The age must be possitive value!"); }
                ageOfPerson = value; 
            }
        }
        

        public double  HeightOfPerson
        {
            get { return heightOfPerson; }
            set
            {
                if (value < 1) { throw new IndexOutOfRangeException("The height must be possitive value!"); }
                heightOfPerson = value;
            }
        }
        

        public double WeightOfPerson
        {
            get { return weightOfPerson; }
            set
            {
                if (value < 1) { throw new IndexOutOfRangeException("The weight must be possitive value!"); }
                weightOfPerson = value;
            }
        }
    }

    public enum UserPurpose
    {
        Gain_Weight, Loose_Weight, Keep_Weight
    }
}
