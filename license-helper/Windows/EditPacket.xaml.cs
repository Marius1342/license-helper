using license_helper.Classes;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace license_helper.Windows
{
    /// <summary>
    /// Interaktionslogik für EditPacket.xaml
    /// </summary>
    public partial class EditPacket : Window
    {
        private Packet packet;
        private Project project;
        public EditPacket(Packet pk, Project project)
        {
            packet = pk;
            this.project = project;

            InitializeComponent();
            Name.Text = pk.Name;
            Version.Text = pk.Version;
            Url.Text = pk.Url;

            LegalText.Document.Blocks.Clear();
            LegalText.Document.Blocks.Add(new Paragraph(new Run(pk.LegalText)));

        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            MainWindow.Projects.First(x => x.Name == project.Name).Projects.Remove(packet);
            this.Close();
        }

        private void SaveAndExit(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(
                LegalText.Document.ContentStart,
                LegalText.Document.ContentEnd
            );

            MainWindow.Projects.First(x => x.Name == project.Name).Projects.First().Name = Name.Text;
            MainWindow.Projects.First(x => x.Name == project.Name).Projects.First().Version = Version.Text;
            MainWindow.Projects.First(x => x.Name == project.Name).Projects.First().Url = Url.Text;
            MainWindow.Projects.First(x => x.Name == project.Name).Projects.First().LegalText = textRange.Text;

            this.Close();
        }
    }
}
