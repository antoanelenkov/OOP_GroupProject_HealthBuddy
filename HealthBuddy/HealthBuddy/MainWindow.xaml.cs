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

namespace HealthBuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var context = new HealthBuddyContext();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<HealthBuddyContext>());

            var dbContext = new HealthBuddyContext();

            Console.WriteLine(dbContext.Breakfasts.Count());
            MessageBox.Show(context.Breakfasts.Count().ToString());

            InitializeComponent();
        }
    }
}
