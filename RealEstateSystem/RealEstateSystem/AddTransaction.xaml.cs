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
    /// Interaction logic for AddTransaction.xaml
    /// </summary>
    public partial class AddTransaction : Window
    {
        public AddTransaction()
        {
            InitializeComponent();

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Client> client = (from e1 in con.Clients
                                   select e1).ToList();

            comboBox1.ItemsSource = client;
            comboBox1.DisplayMemberPath = "FirstName";
            comboBox1.SelectedValuePath = "Id";

            comboBox2.ItemsSource = client;
            comboBox2.DisplayMemberPath = "FirstName";
            comboBox2.SelectedValuePath = "Id";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            float mon;
            int num;

            if (Title.Text.Trim() == "")
                MessageBox.Show("Missing title");
            else
            {
                Transaction transaction = new Transaction();
                transaction.Title = Title.Text.Trim();
                float.TryParse(Amount.Text, out mon);
                transaction.Amount = (decimal)mon;
                transaction.ValueDate = DateTime.Now;

                num = Convert.ToInt32(comboBox1.SelectedValue);
                transaction.CreditClient = num;
                num = Convert.ToInt32(comboBox2.SelectedValue);
                transaction.DebitClient = num;

                Admin.AddTransaction(transaction);
                MessageBox.Show("Transaction was added");
                DialogResult = true;

            }
        }
    }
}
