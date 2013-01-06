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
    /// Interaction logic for ViewTransaction.xaml
    /// </summary>
    public partial class ViewTransaction : Window
    {
        public ViewTransaction()
        {
            InitializeComponent();

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Transaction> transaction = (from e1 in con.Transactions
                                    select e1).ToList();
            TransactionGrid.ItemsSource = transaction;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TransactionGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
