using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EstateData;

namespace RealEstateSystem
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text.Trim() == "")
                MessageBox.Show("Missing name");
            if (LastName.Text.Trim() == "")
                MessageBox.Show("Missing fee");
            else
            {
                Client client = new Client();
                client.FirstName = FirstName.Text.Trim();
                client.LastName = LastName.Text.Trim();

                Admin.AddClient(client);
                MessageBox.Show(client.FirstName + " was added");
                DialogResult = true;

            }
        }
    }
}
