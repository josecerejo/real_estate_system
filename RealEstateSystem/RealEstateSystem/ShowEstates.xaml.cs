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
    /// Interaction logic for ShowEstates.xaml
    /// </summary>
    public partial class ShowEstates : Window
    {
        public ShowEstates()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            float fee;
            float.TryParse(Fee.Text, out fee);

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Estate> estates = (from e1 in con.Estates
                                    where e1.Fee >= fee
                                    select e1).ToList();
            dataGrid1.ItemsSource = estates;
        }

        private void dataGrid1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
