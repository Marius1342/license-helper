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
    /// Interaktionslogik für AddProject.xaml
    /// </summary>
    public partial class AddProject : Page
    {
        public AddProject()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(nameProject.Text == "" && 
               projectUrl.Text == "")
            {
                MessageBox.Show("All fields are requierd");
                return;
            }

            if(MainWindow.Projects.Any(x => x.Name.Normalize() == nameProject.Text.Normalize()))
            {
                MessageBox.Show("Project already exists");
                return;
            }

            MainWindow.Projects.Add(new Project { Name = nameProject.Text,ExtraInfos = extraInfos.Text, ProjectUrl = projectUrl.Text, Version = version.Text });


            DbService.Save();
            MainWindow.Navigate(new Start());
        }
    }
}
