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
    /// Interaction logic for ViewAds.xaml
    /// </summary>
    public partial class ViewAds : Window
    {
        public ViewAds()
        {
            InitializeComponent();

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Ad> transaction = (from e1 in con.Ads
                                             select e1).ToList();
            AdGrid.ItemsSource = transaction;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AdGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
