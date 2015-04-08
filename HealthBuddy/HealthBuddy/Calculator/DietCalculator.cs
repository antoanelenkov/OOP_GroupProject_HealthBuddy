namespace HealthBuddy.Calculator
{
    using System;

    using HealthBuddy.Interfaces;

    public abstract class DietCalculator : IDietCalculator
    {
        private double weightOfPerson;
        private double heightOfPerson;
        private int ageOfPerson;

        public DietCalculator(double weight, double height, int age)
        {
            this.WeightOfPerson = weight;
            this.HeightOfPerson = height;
            this.AgeOfPerson = age;
        }

        public int AgeOfPerson
        {
            get
            {
                return this.ageOfPerson;
            }

            set
            {
                if (value < 1) { throw new IndexOutOfRangeException("The age must be possitive value!"); }
                this.ageOfPerson = value;
            }
        }

        public double HeightOfPerson
        {
            get
            {
                return this.heightOfPerson;
            }

            set
            {
                if (value < 1) { throw new IndexOutOfRangeException("The height must be possitive value!"); }
                this.heightOfPerson = value;
            }
        }

        public double WeightOfPerson
        {
            get
            {
                return weightOfPerson;
            }

            set
            {
                if (value < 1) { throw new IndexOutOfRangeException("The weight must be possitive value!"); }
                weightOfPerson = value;
            }
        }
    }
}
