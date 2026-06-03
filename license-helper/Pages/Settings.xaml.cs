using System;
using System.Collections.Generic;
using System.IO;
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

namespace license_helper.Pages
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            LoadAllDbs();
        }

        private void LoadAllDbs()
        {
            AllOtherLocations.Items.Clear();

            foreach (var item in Properties.Settings.Default.FileLocations)
            {
                AllOtherLocations.Items.Add(item);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Location.Text) == false)
            {
                MessageBox.Show("File does not exists");
                return;
            }

            Properties.Settings.Default.FileLocations.Add(Location.Text);
            Properties.Settings.Default.Save();
            LoadAllDbs();
        }

        private void AllOtherLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AllOtherLocations.SelectedIndex == -1)
            {
                return;
            }

            MessageBoxResult res = MessageBox.Show("Are you sure to delete this db refrence?", "?", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.FileLocations.RemoveAt(AllOtherLocations.SelectedIndex);
                Properties.Settings.Default.Save();
            }
            LoadAllDbs();
        }

        private void SaveAndBack(object sender, RoutedEventArgs e)
        {
            MainWindow.Navigate(new Start());
        }
    }
}
