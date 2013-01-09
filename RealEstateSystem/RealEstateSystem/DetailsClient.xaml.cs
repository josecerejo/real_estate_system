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
using System.Data.SqlClient;
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
            nameLabel.Content = client.FirstName.Trim() + " " + client.LastName.Trim();

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Estate> estates = (from e1 in con.Estates
                                    where e1.Owner == client.Id
                                    select e1).ToList();
            EstateGrid.ItemsSource = estates;
            EstateGrid.IsReadOnly = true;

            int cl;
            cl = Convert.ToInt32(client.Id);
            count_moneyResult result = new count_moneyResult();
            var a = (count_moneyResult)con.count_money(cl).SingleOrDefault();
            nameLabe2.Content = a.Column1;
        }

        private void EstateGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void editClick(object sender, RoutedEventArgs e)
        {
            if (client == null)
                MessageBox.Show("You must select a client");
            else
            {
                UpdateClient clientUpdateDialog = new UpdateClient(client);
                clientUpdateDialog.ShowDialog();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conN = new SqlConnection();
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.DataSource = ".";
            con.InitialCatalog = "estate";
            con.IntegratedSecurity = true;
            conN.ConnectionString = con.ConnectionString;
            conN.Open();
            string zp = "SELECT COUNT(Id) FROM Estate";
            SqlCommand command = conN.CreateCommand();
            command.CommandText = zp;
            SqlDataReader reader = command.ExecuteReader();
            string tekst = "";
            while (reader.Read())
            {
                tekst += reader.GetInt32(0);
            }
            reader.Close();			//zakończenie wczytywania
            command.Cancel();
            MessageBox.Show("dsa" + tekst);
        }
    }
}
