using license_helper.Classes;
using license_helper.Services;
using license_helper.Windows;
using Microsoft.Win32;
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
    /// Interaktionslogik für EditProject.xaml
    /// </summary>
    public partial class EditProject : Page
    {
        private int _indexOfProject;
        private InIFile[] files;
        public EditProject(int indexOfProject)
        {
            InitializeComponent();

            nameProject.Text = MainWindow.Projects[indexOfProject].Name;
            projectUrl.Text = MainWindow.Projects[indexOfProject].ProjectUrl;
            extraInfos.Text = MainWindow.Projects[indexOfProject].ExtraInfos;
            version.Text = MainWindow.Projects[indexOfProject].Version;

            files = TemplateService.GetAllTempalte();

            foreach (var file in files)
            {
                templates.Items.Add(file.Name + file.Uid);

                if (MainWindow.Projects[indexOfProject].TemplateGuid == file.Uid)
                {
                    templates.SelectedIndex = templates.Items.Count;
                }
            }

            _indexOfProject = indexOfProject;

            LoadAllPackets();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Projects[_indexOfProject].Name = nameProject.Text;
            MainWindow.Projects[_indexOfProject].ProjectUrl = projectUrl.Text;
            MainWindow.Projects[_indexOfProject].ExtraInfos = extraInfos.Text;
            MainWindow.Projects[_indexOfProject].Version = version.Text;
            MainWindow.Projects[_indexOfProject].TemplateGuid = files[templates.SelectedIndex].Uid;
            DbService.Save();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Navigate(new Start());
        }

        private void addNewPacket(object sender, RoutedEventArgs e)
        {
            license_helper.Windows.AddPacket pk = new license_helper.Windows.AddPacket(_indexOfProject);

            pk.ShowDialog();

            //Reload all Legal texts
            LoadAllPackets();

            DbService.Save();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string content = TemplateService.GenerateLegalText(files[templates.SelectedIndex], MainWindow.Projects[_indexOfProject]);

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "third-party-notice";
            dlg.DefaultExt = ".text"; 
            dlg.Filter = "Text documents (.txt)|*.txt"; 


            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                File.WriteAllText(dlg.FileName, content);
            }

           
        
        }


        private void LoadAllPackets()
        {
            packets.Items.Clear();
            foreach (var item in MainWindow.Projects[_indexOfProject].Projects.Select(x => x.Name))
            {
                packets.Items.Add(item);
            }
        }

        private void packets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(packets.SelectedIndex == -1)
            {
                return;
            }

            EditPacket editPacket = new EditPacket(MainWindow.Projects[_indexOfProject].Projects[packets.SelectedIndex], MainWindow.Projects[_indexOfProject]);
            editPacket.ShowDialog();

            LoadAllPackets();

            DbService.Save();
        }
    }
}
