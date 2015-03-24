using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace HealthBuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            choosenPurpose.ItemsSource = typeof(User.UserPurpose).GetEnumNames().Select(x => x.Replace('_', ' '));
        }

        #region Get User's info
        User user = new User();

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
            user.Gender = User.UserGender.Female;
        }

        private void Male_Click(object sender, RoutedEventArgs e)
        {
            user.Gender = User.UserGender.Male;
        }

        private void ChoosenPurpose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            throw new NotImplementedException("Still not implemented, do not choose purpose for the moment :) ");
            User.UserPurpose selectedColor = (User.UserPurpose)(choosenPurpose.SelectedItem as PropertyInfo).GetValue(null, null);
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
        // GO TO: MyMenu 
        private void ProceedToProfile_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<System.Windows.Controls.CheckBox> childs =
                FoodSelectionStack.Children.OfType<CheckBox>(); // the key for the baraka is here :D

            childs = childs.Where(x => x.IsChecked == true).ToList();
            selectedIngrediants = childs.Select(x => x.Content).ToList();

            FoodSelectionWindow.Visibility = System.Windows.Visibility.Hidden;
            //Profile.Visibility = System.Windows.Visibility.Visible;
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

            //Debug - assuming the menu is ready and contains only appetiser
            Menu menu = new Menu();
            var appetiser = new Appetiser();
            appetiser.Name = "Oriz";
            appetiser.Calories = 100;
            appetiser.Carbohydrates = 56;
            appetiser.Proteins = 24;
            appetiser.Lipids = 20;
            appetiser.Portion_Size = 150;
            appetiser.Indigrediants = new List<string> { "vegetables, nuts" };
            menu._Appetiser = appetiser;

            try
            {
                foreach (var typeMeal in selectedTypeMeals)
                {
                    var currentType = ("_" + typeMeal);

                    Name_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                                                        .GetType().GetProperty("Name").GetValue(appetiser, null));
                    Calories_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                                                        .GetType().GetProperty("Calories").GetValue(appetiser, null));
                    Carbs_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                                                        .GetType().GetProperty("Carbohydrates").GetValue(appetiser, null));
                    Proteins_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                                                            .GetType().GetProperty("Proteins").GetValue(appetiser, null));
                    Lipids_MenuInfo.Text += "\n" + (menu.GetType().GetProperty(currentType).GetValue(menu, null)
                                                            .GetType().GetProperty("Lipids").GetValue(appetiser, null));
                }
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
    }
}
