using license_helper.Windows;
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
    /// Interaktionslogik für EditProject.xaml
    /// </summary>
    public partial class EditProject : Page
    {
        private int _indexOfProject;
        public EditProject(int indexOfProject)
        {
            InitializeComponent();

            nameProject.Text = MainWindow.Projects[indexOfProject].Name;
            projectUrl.Text = MainWindow.Projects[indexOfProject].ProjectUrl;
            extraInfos.Text = MainWindow.Projects[indexOfProject].ExtraInfos;
            version.Text = MainWindow.Projects[indexOfProject].Version;

            _indexOfProject = indexOfProject;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MainWindow.Projects[_indexOfProject].Name = nameProject.Text;
           MainWindow.Projects[_indexOfProject].ProjectUrl = projectUrl.Text;
           MainWindow.Projects[_indexOfProject].ExtraInfos = extraInfos.Text;
           MainWindow.Projects[_indexOfProject].Version = version.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Navigate(new Start());
        }

        private void addNewPacket(object sender, RoutedEventArgs e)
        {
            license_helper.Windows.AddPacket pk = new license_helper.Windows.AddPacket(_indexOfProject);

            pk.ShowDialog();

            //TODO: Reload all Legal texts

        }
    }
}
