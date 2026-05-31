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
            if(LegalText.Text == "" || Name.Text == "" || Url.Text == "")
            {
                return;
            }


            MainWindow.Projects[index].Projects.Add(new Classes.Packet()
            {
                LegalText = LegalText.Text,
                Name = Name.Text,
                Url = Url.Text,
            });



            LegalText.Text = "";
            Name.Text = "";
            Url.Text = "";
        }
    }
}
