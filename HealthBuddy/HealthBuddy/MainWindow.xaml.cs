using System;
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
using HealthBuddy.Enums;

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
        List<object> unSelectedIngrediants = new List<object>() {"Fruits", "Vegetables", "Nuts", "Legumes", "Grain", "Milk", "Fish" , "Meat" };
        // GO TO: MyMenu 
        private void ProceedToProfile_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<System.Windows.Controls.CheckBox> childs =
                FoodSelectionStack.Children.OfType<CheckBox>(); // the key for the baraka is here :D

            //selectedIngrediants = childs.Where(x => x.IsChecked == true).Select(x => x.Content).ToList();
            //unSelectedIngrediants = childs.Where(x => x.IsChecked == false).Select(x => x.Content).ToList();

            FoodSelectionWindow.Visibility = System.Windows.Visibility.Hidden;
            Menu.Visibility = System.Windows.Visibility.Visible;

            WeightProfile.Text = user.Weight.ToString();
            HeightProfile.Text = user.Height.ToString();
            ListSelections.Text += string.Join("\n", selectedIngrediants);
        }

        List<object> selectedTypeMeals = new List<object>();
        HashSet<History> AllHistory = new HashSet<History>();

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
                var menuItems = new List<KeyValuePair<Meal, int>>();
                foreach (var meal in selectedTypeMeals)
                {
                    string table = meal as String;
                    var test = new List<Meal>();
                    var raw = new List<Meal>();
                    using (var ctx = new HealthBuddyContext())
                    {
                        string query = string.Format("SELECT *FROM {0}s", table);
                        raw = ctx.Database.SqlQuery<Meal>(query, table).ToList(); // TODO: Edit to IQuerable<Meal>

                        List<string> strings = unSelectedIngrediants.Select(c => c.ToString()).ToList();

                        //test = raw.Where(x => strings.Any(y => x.Ingredients.Split(' ').Contains(y))).ToList();  // Ivaylo Kenov

                        test = raw.Where(x => Meal.Filter(x, unSelectedIngrediants)).Select(x => x).ToList();



                    }
                    filtredMeals = filtredMeals.Concat(test).ToList();
                    // DEBUG 
                    //var window = new Window();
                    //for (int index = 0; index < filtredMeals.Count; index++)
                    //{
                    //    window.Content += filtredMeals[index].GetType().Name;
                    //    window.Content += filtredMeals[index].Name;
                    //    window.Content += "\n";
                    //    filtredMeals[index] = Engine.InteractionManager.ConvertToTypeMeal(filtredMeals[index], table);
                    //    window.Content += filtredMeals[index].GetType().Name;
                    //    window.Content += filtredMeals[index].Name;
                    //    window.Content += "\n";
                    //}
                    //MessageBox.Show(window.Content.ToString());
                }

                SimplexMealGenerator simplex = new SimplexMealGenerator(filtredMeals, 1875); // TODO: Set user's Calories
                simplex.Generate();
                for (int i = 0; i < filtredMeals.Count; i++)
                {
                    if (simplex.MealPortions[i] != 0)
                    {
                        // MessageBox.Show(string.Format("{0}:{1}", filtredMeals[i].Name, simplex.MealPortions[i])); // TODO: Remove
                        //TODO: Save info from meniTems list in History (struct)
                        var menuItem = new KeyValuePair<Meal, int>(filtredMeals[i], simplex.MealPortions[i]);
                        menuItems.Add(menuItem);
                    }
                }

                //TODO: Event for the scrollbar
                ClearMenuInfoBar();

                PrintMenuInfo(menuItems);
                // Add to History

                var newHistory = new History();
                newHistory.Date = (DateTime)Calendar.SelectedDate.Value == null ? DateTime.Now : Calendar.SelectedDate.Value; // TODO: Change with value from calendar
                newHistory.Menu = menuItems;
                var date = Calendar.SelectedDate.Value;
                AllHistory.Remove(AllHistory.Where(x => x.Date == date).Select(z=>z).FirstOrDefault());
                AllHistory.Add(newHistory);



                //var searchedHistory = AllHistory.Where(x => x.Date == Calendar.SelectedDate).Select(m => m.Menu).First();
                //PrintMenuInfo(searchedHistory);


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

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearMenuInfoBar();
            var c = sender as Calendar;
            if (c.SelectedDate.HasValue)
            {
                var date = c.SelectedDate.Value;
                if (AllHistory.Any(x => x.Date == date))
                {
                    var searchedHistory = AllHistory.Where(x => x.Date == date).Select(m => m.Menu).FirstOrDefault();
                    PrintMenuInfo(searchedHistory);
                }
            }
        }

        private void ClearMenuInfoBar()
        {
            Name_MenuInfo.Text = "Name";
            Calories_MenuInfo.Text = "Calories";
            Carbs_MenuInfo.Text = "Carbs";
            Proteins_MenuInfo.Text = "Proteins";
            Lipids_MenuInfo.Text = "Fats";
            PortionSize_MenuInfo.Text = "Portion Size";
            Portions_MenuInfo.Text = "Portions";
        }

        private void PrintMenuInfo(IEnumerable<KeyValuePair<Meal, int>> menuItems)
        {
            foreach (var pair in menuItems)
            {
                var meal = pair.Key;
                Name_MenuInfo.Text += Environment.NewLine + meal.Name;

                Calories_MenuInfo.Text += Environment.NewLine + meal.Calories;

                Carbs_MenuInfo.Text += Environment.NewLine + meal.Carbohydrates;

                Proteins_MenuInfo.Text += Environment.NewLine + meal.Proteins;

                Lipids_MenuInfo.Text += Environment.NewLine + meal.Fats;

                PortionSize_MenuInfo.Text += Environment.NewLine + meal.Portion_Size;

                Portions_MenuInfo.Text += Environment.NewLine + pair.Value;
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

        private void SelectFruit_Click(object sender, RoutedEventArgs e)
        {            
            MessageBox.Show("Fruit selected!");
            if (selectedIngrediants.Contains("Fruits"))
            {
                selectedIngrediants.Remove("Fruits");
                unSelectedIngrediants.Add("Fruits");
            }

            else
            {
                selectedIngrediants.Add("Fruits");
                unSelectedIngrediants.Remove("Fruits");
            }
        }

        private void SelectVegetable_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIngrediants.Contains("Vegetables"))
            {
                selectedIngrediants.Remove("Vegetables");
                unSelectedIngrediants.Add("Vegetables");
            }

            else
            {
                selectedIngrediants.Add("Vegetables");
                unSelectedIngrediants.Remove("Vegetables");
            }
        }

        private void SelectNuts_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIngrediants.Contains("Nuts"))
            {
                selectedIngrediants.Remove("Nuts");
                unSelectedIngrediants.Add("Nuts");
            }

            else
            {
                selectedIngrediants.Add("Nuts");
                unSelectedIngrediants.Remove("Nuts");
            }
        }

        private void SelectLegumes_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIngrediants.Contains("Legumes"))
            {
                selectedIngrediants.Remove("Legumes");
                unSelectedIngrediants.Add("Legumes");
            }

            else
            {
                selectedIngrediants.Add("Legumes");
                unSelectedIngrediants.Remove("Legumes");
            }
        }

        private void SelectGrain_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIngrediants.Contains("Grain"))
            {
                selectedIngrediants.Remove("Grain");
                unSelectedIngrediants.Add("Grain");
            }

            else
            {
                selectedIngrediants.Add("Grain");
                unSelectedIngrediants.Remove("Grain");
            }
        }

        private void SelectMilk_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIngrediants.Contains("Milk"))
            {
                selectedIngrediants.Remove("Milk");
                unSelectedIngrediants.Add("Milk");
            }

            else
            {
                selectedIngrediants.Add("Milk");
                unSelectedIngrediants.Remove("Milk");
            }
        }

        private void SelectFish_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIngrediants.Contains("Fish"))
            {
                selectedIngrediants.Remove("Fish");
                unSelectedIngrediants.Add("Fish");
            }

            else
            {
                selectedIngrediants.Add("Fish");
                unSelectedIngrediants.Remove("Fish");
            }
        }

        private void SelectMeat_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIngrediants.Contains("Meat"))
            {
                selectedIngrediants.Remove("Meat");
                unSelectedIngrediants.Add("Meat");
            }

            else
            {
                selectedIngrediants.Add("Meat");
                unSelectedIngrediants.Remove("Meat");
            }
        }





    }
}
