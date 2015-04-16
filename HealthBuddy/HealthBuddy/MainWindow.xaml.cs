namespace HealthBuddy
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
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

    using HealthBuddy.Calculator;
    using HealthBuddy.Models;
    using HealthBuddy.Interfaces;
    using HealthBuddy.Enums;
    using HealthBuddy.Exceptions;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HealthBuddyContext context = new HealthBuddyContext();
      //  IEnumerable<Canvas> Windows = HealthBuddyWindow.Children.OfType<Canvas>();
        List<object> selectedIngrediants = new List<object>();
        List<object> unSelectedIngrediants = new List<object>() 
                                { "Fruit", "Vegetables", "Nuts", "Legumes", 
                                   "Grain", "Milk", "Fish", "Meat" };

        private List<object> selectedTypeMeals = new List<object>();
        private HashSet<History> AllHistory = new HashSet<History>();
        private int userCalories = new int();
        private string unhealthyExMessage = @"Do not eat this! It is NOT good for you! 
Regards, your Healty Buddy  :* ";

        IEnumerable<string> fullMealList = context.Appetisers.Select(x => x.Name).ToList()
                                  .Concat(context.Breakfasts.Select(x => x.Name).ToList())
                                  .Concat(context.Desserts.Select(x => x.Name).ToList())
                                  .Concat(context.Liquids.Select(x => x.Name).ToList())
                                  .Concat(context.Mains.Select(x => x.Name).ToList())
                                  .Concat(context.Salads.Select(x => x.Name).ToList())
                                  .Concat(context.Soups.Select(x => x.Name).ToList());

        public MainWindow()
        {
            InitializeComponent();
            // choosenPurpose.ItemsSource = typeof(DietCalculator.UserPurpose).GetEnumNames().Select(x => x.Replace('_', ' ')); Why UserPurposi is in class Calculator?
            choosenPurpose.ItemsSource = typeof(UserPurpose).GetEnumNames().Select(x => x.Replace('_', ' '));
            SecondFoodCombo.ItemsSource = fullMealList;
            FirstFoodCombo.ItemsSource = fullMealList;
        }
        #region Get User's info
        User user = new User("Anonymous", 50, UserGender.Female, 40, 200, UserPurpose.Keep_Weight, new List<string>());
        private void UserTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Engine.GetUserInfo.GetUserName(user, sender);
        }        

        private void UserTextBoxAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            Engine.GetUserInfo.GetUserAge(user, sender);
        }      

        // TODO: Check the cultureInfo for the double values (replace ','->'.') set to invariant?
        private void UserTextBoxWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            Engine.GetUserInfo.GetUserWeight(user, sender);
        }
        
        private void UserTextBoxHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            Engine.GetUserInfo.GetUserHeight(user, sender);
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
            // throw new NotImplementedException("Still not implemented, do not choose purpose for the moment :) ");
           // var purpose = new object();
           // var purpose = (choosenPurpose.SelectedItem as PropertyInfo).Name;
           // var test =new  UserPurpose();
           // var enumPurposeList = test.GetType().GetEnumValues();
           //user.Purpose = enumPurposeList.
            //UserPurpose selectedColor = new UserPurpose();
            //var test = selectedColor.
            //user.Purpose = test;
            //test.Text = user.Purpose.ToString();
        }
        #endregion
        
        private void Generate_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<System.Windows.Controls.CheckBox> childs =
                TypeMealsCheckBox.Children.OfType<CheckBox>(); // the key for the baraka is here :D

            childs = childs.Where(x => x.IsChecked == true).ToList();
            selectedTypeMeals = childs.Select(x => x.Content).ToList();

            try
            {
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
                }
               
                SimplexMealGenerator simplex = new SimplexMealGenerator(filtredMeals, userCalories); 
                simplex.Generate();
                for (int i = 0; i < filtredMeals.Count; i++)
                {
                    if (simplex.MealPortions[i] != 0)
                    {
                        var menuItem = new KeyValuePair<Meal, int>(filtredMeals[i], simplex.MealPortions[i]);
                        menuItems.Add(menuItem);
                    }
                }

                //TODO: Event for the scrollbar
                ClearMenuInfoBar();

                PrintMenuInfo(menuItems);

                // Add to History
                var newHistory = new History();
                if (Calendar.SelectedDate.HasValue)
                {
                    newHistory.Date = Calendar.SelectedDate.Value;
                }
                else
                {
                    newHistory.Date = DateTime.Now.Date;
                }// TODO: Change with value from calendar
                newHistory.Menu = menuItems;
                var date = Calendar.SelectedDate.Value;
                AllHistory.Remove(AllHistory.Where(x => x.Date == date).Select(z => z).FirstOrDefault());
                AllHistory.Add(newHistory);
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

        #region Switch Windows
        private void Menu_MyMenu_Click(object sender, RoutedEventArgs e)
        {
            var Windows = HealthBuddyWindow.Children.OfType<Canvas>();
            Engine.InteractionManager.SwitchToWindow(Windows,Menu);
            userCalories = Engine.InteractionManager.CalculateUserCalories(user, userCalories, userCaloriesInfo);
        }
        
        private void Menu_ICompare_Click(object sender, RoutedEventArgs e)
        {
            var Windows = HealthBuddyWindow.Children.OfType<Canvas>();
            Engine.InteractionManager.SwitchToWindow(Windows,Icompare);
        }

        private void Menu_Profile_Click(object sender, RoutedEventArgs e)
        {
            var Windows = HealthBuddyWindow.Children.OfType<Canvas>();
            Engine.InteractionManager.SwitchToWindow(Windows, Profile);
        }

        private void ProceedToProfile_Click(object sender, RoutedEventArgs e)
        {
            //    IEnumerable<System.Windows.Controls.CheckBox> childs =
            //        FoodSelectionStack.Children.OfType<CheckBox>(); // the key for the baraka is here :D

            //    //selectedIngrediants = childs.Where(x => x.IsChecked == true).Select(x => x.Content).ToList();
            //    //unSelectedIngrediants = childs.Where(x => x.IsChecked == false).Select(x => x.Content).ToList();

            //    FoodSelectionWindow.Visibility = System.Windows.Visibility.Hidden;
            //    Menu.Visibility = System.Windows.Visibility.Visible;
        }
        private void Proceed_Click(object sender, RoutedEventArgs e)
        {
            WeightProfile.Text = user.Weight.ToString();
            HeightProfile.Text = user.Height.ToString();
            ListSelections.Text += string.Join("\n", selectedIngrediants);

            Menu_MyMenu_Click(sender, e);
        }
        #endregion
        
        private void CompareMeals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                }
                else if (second < first)
                {
                    FirstComparerImage.Source = new BitmapImage(uriSourceNot);
                    SecondComparerImage.Source = new BitmapImage(uriSource);
                }
                else
                {
                    FirstComparerImage.Source = new BitmapImage(uriSource);
                    SecondComparerImage.Source = new BitmapImage(uriSource);
                }

                TestCompareFirst.Content += first.ToString();
                TestCompareSecond.Content += second.ToString();
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
            // if (first == null) first = context.Appetisers.FirstOrDefault(x => x.Name == firstMealString); // TODO: ???
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
            try
            {
                if (first > second) throw new UnHealthyException(unhealthyExMessage);             
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
             
        }

        private void SecondComparer_Click(object sender, RoutedEventArgs e)
        {
            var firstMealString = FirstFoodCombo.SelectedValue as String;
            var secondMealString = SecondFoodCombo.SelectedValue as String;

            var first = GetMealToCompare(firstMealString);
            var second = GetMealToCompare(secondMealString);
            try
            {
                if (first < second) throw new UnHealthyException(unhealthyExMessage);    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }
        #region Ingredients Selection
        private void SelectFruit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fruit selected!");
            if (selectedIngrediants.Contains("Fruit"))
            {
                selectedIngrediants.Remove("Fruit");
                unSelectedIngrediants.Add("Fruit");
            }
            else
            {
                selectedIngrediants.Add("Fruit");
                unSelectedIngrediants.Remove("Fruit");
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
        #endregion
    }
}