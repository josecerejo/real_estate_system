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
    /// Interaction logic for ViewEstates.xaml
    /// </summary>
    public partial class ViewEstates : Window
    {
        public ViewEstates()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Estate> estates = (from e1 in con.Estates
                                    select e1).ToList();
            EstateGrid.ItemsSource = estates;
        }

        private void EstateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
