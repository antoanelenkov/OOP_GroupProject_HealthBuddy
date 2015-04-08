namespace HealthBuddy.Engine
{
    using HealthBuddy.Models;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    public abstract class GetUserInfo
    {
        public static void GetUserName(User user, object sender)
        {
            var name = sender as TextBox;
            name.GotFocus += ClearDefaultText_userInfoTextBox;
            user.Name = name.Text;
        }

        public static void GetUserAge(User user, object sender)
        {
            var ageBox = sender as TextBox;
            ageBox.GotFocus += ClearDefaultText_userInfoTextBox;

            int age = new int();
            int.TryParse(ageBox.Text, out age);
            user.Age = age;
        }

        public static void GetUserWeight(User user, object sender)
        {
            var weightBox = sender as TextBox;
            weightBox.GotFocus += ClearDefaultText_userInfoTextBox;

            double weight = new double();
            double.TryParse(weightBox.Text, out weight);
            user.Weight = weight;
        }
        public static void GetUserHeight(User user,object sender)
        {
            var heightBox = sender as TextBox;
            heightBox.GotFocus += ClearDefaultText_userInfoTextBox;
            double height = new double();
            double.TryParse(heightBox.Text, out height);
            user.Height = height;
        }

        public static void ClearDefaultText_userInfoTextBox(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = string.Empty;
        }
    }
}
