using license_helper.Classes;
using license_helper.Services;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für Start.xaml
    /// </summary>
    public partial class Start : Page
    {
        public Start()
        {
            InitializeComponent();

            foreach(var item in MainWindow.Projects.Select(x => x.Name))
            {
                projects.Items.Add(new ListBoxItem() { Content = item});
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Navigate(new AddProject());
        }

        private void projects_Clicked(object sender, MouseButtonEventArgs e)
        {
            if(projects.SelectedIndex < 0 || projects.SelectedIndex > MainWindow.Projects.Count)
            {
                return;
            }

            MainWindow.Navigate(new EditProject(projects.SelectedIndex));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Navigate(new Settings());
            DbService.LoadProjects();
        }
    }
}
