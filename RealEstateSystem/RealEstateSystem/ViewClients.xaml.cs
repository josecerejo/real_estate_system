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
    /// Interaction logic for ViewClients.xaml
    /// </summary>
    public partial class ViewClients : Window
    {
        public ViewClients()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Client> client = (from e1 in con.Clients
                                    select e1).ToList();
            ClientGrid.ItemsSource = client;
        }

        private void ClientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            Client selected = ClientGrid.SelectedItem as Client;
            if (selected == null)
                MessageBox.Show("You must select a client");
            else
            {
                DetailsClient client = new DetailsClient(selected);
                client.ShowDialog();
            }
        }
    }
}