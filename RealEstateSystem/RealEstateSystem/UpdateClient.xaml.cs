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
    /// Interaction logic for UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Window
    {
        private Client client;

        public UpdateClient(Client client)
        {
            InitializeComponent();

            this.client = client;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FirstName.Text = client.FirstName.Trim();
            LastName.Text = client.LastName.Trim();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text.Trim() == "")
                MessageBox.Show("Missing first name");
            if (LastName.Text.Trim() == "")
                MessageBox.Show("Missing last name");
            else
            {
                client.FirstName = FirstName.Text.Trim();
                client.LastName = LastName.Text.Trim();

                Admin.UpdateClient(client);
                MessageBox.Show(client.FirstName + " was updated");
                DialogResult = true;
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
