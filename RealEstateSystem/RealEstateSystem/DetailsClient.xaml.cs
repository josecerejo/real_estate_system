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
    /// Interaction logic for DetailsClient.xaml
    /// </summary>
    public partial class DetailsClient : Window
    {
        private Client client;

        public DetailsClient(Client client)
        {
            InitializeComponent();

            this.client = client;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FirstName.Text = client.FirstName.Trim();
            LastName.Text = client.LastName.Trim();
            
            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Estate> estates = (from e1 in con.Estates
                                    where e1.Owner == client.Id
                                    select e1).ToList();
            EstateGrid.ItemsSource = estates;
        }

        private void EstateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
