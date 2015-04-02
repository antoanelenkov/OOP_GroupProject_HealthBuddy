﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Runtime.Serialization;
using System.Data.Sql;
using HealthBuddy.Calculator;
using HealthBuddy.Models;
using System.Data.SqlClient;
using HealthBuddy.Interfaces;

namespace HealthBuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HealthBuddyContext context = new HealthBuddyContext();

        IEnumerable<string> fullMealList = context.Appetisers.Select(x => x.Name).ToList()
                                  .Concat(context.Breakfasts.Select(x => x.Name).ToList())
                                  .Concat(context.Desserts.Select(x => x.Name).ToList())
                                  .Concat(context.Liquids.Select(x => x.Name).ToList())
                                  .Concat(context.Mains.Select(x => x.Name).ToList())
                                  .Concat(context.Salads.Select(x => x.Name).ToList())
                                  .Concat(context.Soups.Select(x => x.Name).ToList());
        public MainWindow()
        {

            //Database.SetInitializer(new DbConfiguration(DbConfiguration));
            InitializeComponent();
            // choosenPurpose.ItemsSource = typeof(DietCalculator.UserPurpose).GetEnumNames().Select(x => x.Replace('_', ' ')); Why UserPurposi is in class Calculator?

            SecondFoodCombo.ItemsSource = fullMealList;
            FirstFoodCombo.ItemsSource = fullMealList;
        }
        #region Get User's info
        User user = new User("Maria", 50, UserGender.Female, 40, 200, UserPurpose.Keep_Weight, new List<string>());

        private void UserTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var name = sender as TextBox;
            name.GotFocus += ClearDefaultText_userInfoTextBox;
            user.Name = name.Text;
        }

        private void UserTextBoxAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ageBox = sender as TextBox;
            ageBox.GotFocus += ClearDefaultText_userInfoTextBox;

            int age = new int();
            int.TryParse(ageBox.Text, out age);
            user.Age = age;
        }

        // TODO: Check the cultureInfo for the double values (replace ','->'.') set to invariant?
        private void UserTextBoxWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            var weightBox = sender as TextBox;
            weightBox.GotFocus += ClearDefaultText_userInfoTextBox;

            double weight = new double();
            double.TryParse(weightBox.Text, out weight);
            user.Weight = weight;
        }

        private void UserTextBoxHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            var heightBox = sender as TextBox;
            heightBox.GotFocus += ClearDefaultText_userInfoTextBox;
            double height = new double();
            double.TryParse(heightBox.Text, out height);
            user.Height = height;
        }

        private void ClearDefaultText_userInfoTextBox(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = string.Empty;
        }

        private void Female_Click(object sender, RoutedEventArgs e)
        {
            user.Gender = UserGender.Female;
        }

        private void Male_Click(object sender, RoutedEventArgs e)
        {
            user.Gender = UserGender.Male;
        }

        private void ChoosenPurpose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // TODO: Do this on group meeting 
            throw new NotImplementedException("Still not implemented, do not choose purpose for the moment :) ");
            UserPurpose selectedColor = (UserPurpose)(choosenPurpose.SelectedItem as PropertyInfo).GetValue(null, null);
            user.Purpose = selectedColor;
            // test.Text = user.Purpose.ToString();
        }
        #endregion

        // GO TO: 2nd window: Selection of foor ingredients
        private void Proceed_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.Visibility = System.Windows.Visibility.Hidden;
            FoodSelectionWindow.Visibility = System.Windows.Visibility.Visible;
        }

        List<object> selectedIngrediants = new List<object>();
        List<object> unSelectedIngrediants = new List<object>();
        // GO TO: MyMenu 
        private void ProceedToProfile_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<System.Windows.Controls.CheckBox> childs =
                FoodSelectionStack.Children.OfType<CheckBox>(); // the key for the baraka is here :D

            selectedIngrediants = childs.Where(x => x.IsChecked == true).Select(x => x.Content).ToList();
            unSelectedIngrediants = childs.Where(x => x.IsChecked == false).Select(x => x.Content).ToList();

            FoodSelectionWindow.Visibility = System.Windows.Visibility.Hidden;
            Menu.Visibility = System.Windows.Visibility.Visible;

            WeightProfile.Text = user.Weight.ToString();
            HeightProfile.Text = user.Height.ToString();
            ListSelections.Text += string.Join("\n", selectedIngrediants);
        }

        List<object> selectedTypeMeals = new List<object>();

        private void Generate_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<System.Windows.Controls.CheckBox> childs =
                TypeMealsCheckBox.Children.OfType<CheckBox>(); // the key for the baraka is here :D

            childs = childs.Where(x => x.IsChecked == true).ToList();
            selectedTypeMeals = childs.Select(x => x.Content).ToList();

            // TODO: Set Loading giff while the data is loading :)))
            try
            {
                // var context = new HealthBuddyContext();

                var filtredMeals = new List<Meal>();

                foreach (var meal in selectedTypeMeals)
                {
                    string table = meal as String;
                    var test = new List<Meal>();
                    var raw = new List<Meal>();
                    using (var ctx = new HealthBuddyContext())
                    {
                        var window = new Window();
                        string query = string.Format("SELECT *FROM {0}s", table);
                        raw = ctx.Database.SqlQuery<Meal>(query, table).ToList(); // TODO: Edit to IQuerable<Meal>

                        List<string> strings = unSelectedIngrediants.Select(c => c.ToString()).ToList();

                         //test = raw.Where(x => strings.Any(y => x.Ingredients.Split(' ').Contains(y))).ToList();  // Ivaylo Kenov
                       
                       test = raw.Where(x => Meal.Filter(x, unSelectedIngrediants)).Select(x => x).ToList();   
                        
                        // DEBUG 
                        for (int index = 0; index < test.Count; index++)
                        {
                            window.Content += test[index].GetType().Name;
                            window.Content += test[index].Name;
                            window.Content += "\n";
                            test[index] = Engine.InteractionManager.ConvertToTypeMeal(test[index], table);
                            window.Content += test[index].GetType().Name;
                            window.Content += test[index].Name;
                            window.Content += "\n";
                        }
                        MessageBox.Show(window.Content.ToString());
                    }
                }
                JustMenu menu = new JustMenu();

                //INFO:  We will know what kind of meal to search from <selectedTypeMeals>
                //Dessert tiramissu = context.Desserts.FirstOrDefault(x => x.Name == "Tiramissu");// TODO: get from Simplex
                //menu._Dessert = tiramissu; 

                //Salad salad = context.Salads.FirstOrDefault(x => x.Name == "TestSalad"); //TODO: get from Simplex               
                //menu._Salad = salad;

                //var listOfMealsFromMenu = new List<Meal>();

                //listOfMealsFromMenu.Add(salad); // will be added in order
                //listOfMealsFromMenu.Add(tiramissu);

                //var index = 0;
                //foreach (var typeMeal in selectedTypeMeals)
                //{
                //    var currentType = ("_" + typeMeal);


                //    Name_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                //                                        .GetType().GetProperty("Name").GetValue(listOfMealsFromMenu[index], null));
                //    Calories_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                //                                        .GetType().GetProperty("Calories").GetValue(listOfMealsFromMenu[index], null));
                //    Carbs_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                //                                        .GetType().GetProperty("Carbohydrates").GetValue(listOfMealsFromMenu[index], null));
                //    Proteins_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                //                                        .GetType().GetProperty("Proteins").GetValue(listOfMealsFromMenu[index], null));
                //    Lipids_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                //                                        .GetType().GetProperty("Fats").GetValue(listOfMealsFromMenu[index], null));
                //    index++;
                //    ProgressBar.Value += 10;
                //}
                //ProgressBar.Value = 100;

                //TEST
                User person1 = new User("Antoan", 24, UserGender.Male, 78, 180, UserPurpose.Loose_Weight, new List<string>());
                MenCaloriesCalculator calcCalories = new MenCaloriesCalculator(person1.Weight, person1.Height, person1.Age, person1.Purpose);
                MenWaterNeedsCalculator calcWater = new MenWaterNeedsCalculator(person1.Weight, person1.Height, person1.Age);
                int caloriesOfperson1 = calcCalories.CalculateCalories();
                double waterOfperson1 = calcWater.CalculateWaterNeeds();
                //TEST

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Menu_Profile_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = System.Windows.Visibility.Visible;
            Menu.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Menu_MyMenu_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = System.Windows.Visibility.Hidden;
            Menu.Visibility = System.Windows.Visibility.Visible;
        }

        private void Menu_ICompare_Click(object sender, RoutedEventArgs e)
        {

            Icompare.Visibility = System.Windows.Visibility.Visible;

        }

        private void CompareMeals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Meal first = FirstFoodCombo.SelectedValue as Meal;  
                // Meal second = SecondFoodCombo.SelectedValue as Meal;
                FirstComparer.Visibility = System.Windows.Visibility.Visible;
                SecondComparer.Visibility = System.Windows.Visibility.Visible;

                var firstMealString = FirstFoodCombo.SelectedValue as String;
                var secondMealString = SecondFoodCombo.SelectedValue as String;

                var first = GetMealToCompare(firstMealString);
                var second = GetMealToCompare(secondMealString);

                var uriSource = new Uri(@"Images\Passed.png", UriKind.Relative);
                var uriSourceNot = new Uri(@"Images\NotPassed.png", UriKind.Relative);


                if (first < second)
                {
                    FirstComparerImage.Source = new BitmapImage(uriSource);
                    SecondComparerImage.Source = new BitmapImage(uriSourceNot);
                    TestCompare.Text += first.Name;
                    TestCompare.Text += "\n" + first.Calories;
                    TestCompare.Text += "\n" + second.Calories;

                }

                else if (second < first)
                {
                    FirstComparerImage.Source = new BitmapImage(uriSourceNot);
                    SecondComparerImage.Source = new BitmapImage(uriSource);
                    TestCompare.Text += second.Name;
                    TestCompare.Text += "\n" + second.Calories;
                    TestCompare.Text += "\n" + first.Calories;
                }
                else
                {
                    FirstComparerImage.Source = new BitmapImage(uriSource);
                    SecondComparerImage.Source = new BitmapImage(uriSource);
                    TestCompare.Text = "Equal";
                }

                var neshtosi = context.Desserts.Where(x => x.Name == "Raffaelo").First();
                TestCompare.Text += neshtosi.Name;
                TestCompare.Text += neshtosi.Ingredients.First();
                TestCompare.Text += string.Join("\n", neshtosi.Ingredients);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static Meal GetMealToCompare(string mealAsString)
        {
            var first = context.Desserts.FirstOrDefault(x => x.Name == mealAsString) as Meal;
            if (first == null) first = context.Breakfasts.FirstOrDefault(x => x.Name == mealAsString);
            // if (first == null) first = context.Appetisers.FirstOrDefault(x => x.Name == firstMealString); ???
            if (first == null) first = context.Liquids.FirstOrDefault(x => x.Name == mealAsString);
            if (first == null) first = context.Mains.FirstOrDefault(x => x.Name == mealAsString);
            if (first == null) first = context.Salads.FirstOrDefault(x => x.Name == mealAsString);
            if (first == null) first = context.Soups.FirstOrDefault(x => x.Name == mealAsString);
            return first;
        }

        private void FirstComparer_Click(object sender, RoutedEventArgs e)
        {
            var firstMealString = FirstFoodCombo.SelectedValue as String;
            var secondMealString = SecondFoodCombo.SelectedValue as String;

            var first = GetMealToCompare(firstMealString);
            var second = GetMealToCompare(secondMealString);
            if (first > second) MessageBox.Show(@"New Unhealty Exeption(should be implemented here). 
Do not eat this! It is NOT good for you! 
Regards, your Healty Buddy  :* ");
        }
        private void SecondComparer_Click(object sender, RoutedEventArgs e)
        {
            var firstMealString = FirstFoodCombo.SelectedValue as String;
            var secondMealString = SecondFoodCombo.SelectedValue as String;

            var first = GetMealToCompare(firstMealString);
            var second = GetMealToCompare(secondMealString);
            if (first < second) MessageBox.Show(@"New Unhealty Exeption(should be implemented here). 
Do not eat this! It is NOT good for you! 
Regards, your Healty Buddy  :* ");
        }



    }
}
