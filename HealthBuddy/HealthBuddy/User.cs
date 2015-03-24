namespace HealthBuddy
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        private double calories_per_day;

        public string Name { get; set; }

        public int Age { get; set; }

        public UserGender Gender { get; set; }

        public double Weight { get; set; } // in kg

        public double Height { get; set; } // in cm

        public UserPurpose Purpose { get; set; }

        public List<string> Choosen_indigrediants { get; set; }

        public enum UserGender
        {
            Female, Male
        }

        public enum UserPurpose
        {
            Gain_Weight, Loose_Weight, Keep_Weight
        }

        public double Calories_per_day
        {
            get { return this.calories_per_day; }
            set { this.calories_per_day = this.CalcCaloriesPerDay(); }
        }

        /// <summary>
        /// Calculates BMR by different formula depending on the gender
        /// BMR = 10 * weight(kg) + 6.25 * height(cm) - 5 * age(y) + 5         (man)
        /// BMR = 10 * weight(kg) + 6.25 * height(cm) - 5 * age(y) - 161     (woman)
        /// </summary>
        /// <returns></returns>
        private double CalcCaloriesPerDay()
        {
            double BMR = new double();
            if (this.Gender.Equals(UserGender.Female))
            {
                BMR = 10 * this.Weight + 6.25 * this.Height - 5 * this.Age - 161;
            }
            else if (this.Gender.Equals(UserGender.Male))
            {
                BMR = 10 * this.Weight + 6.25 * this.Height - 5 * this.Age + 5;
            }

            switch (this.Purpose)
            {
                case UserPurpose.Gain_Weight: BMR *= 1.2; break;
                case UserPurpose.Loose_Weight: BMR *= 0.8; break;
                // case userPurpose.Keep_Weight -> BMR does not change
            }

            return BMR;
        }
    }
}
