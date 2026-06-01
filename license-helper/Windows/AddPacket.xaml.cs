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
using System.Windows.Shapes;

namespace license_helper.Windows
{
    /// <summary>
    /// Interaktionslogik für AddPacket.xaml
    /// </summary>
    public partial class AddPacket : System.Windows.Window
    {
        private int index;
        public AddPacket(int index)
        {
            InitializeComponent();
            this.index = index;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {


            TextRange textRange = new TextRange(
       LegalText.Document.ContentStart,
       LegalText.Document.ContentEnd
   );

            if (textRange.Text == "" || Name.Text == "" || Url.Text == "")
            {
                return;
            }



            MainWindow.Projects[index].Projects.Add(new Classes.Packet()
            {
                LegalText = textRange.Text,
                Name = Name.Text,
                Url = Url.Text,
                Version = Version.Text
            });


            Version.Text = "";
            Name.Text = "";
            Url.Text = "";
        }
    }
}
