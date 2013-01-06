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
    /// Interaction logic for EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        public EditClient()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Client> client = (from e1 in con.Clients
                                   select e1).ToList();

            EditClientt.ItemsSource = client;
            EditClientt.SelectionChanged += new SelectionChangedEventHandler(EditClienttSelectionChanged);
            EditClientt.IsReadOnly = true;

            show.IsEnabled      = false;
            delete.IsEnabled    = false;
            update.IsEnabled    = false;
        }

        private void EditClienttSelectionChanged(object sender, EventArgs e)
        {
            Client selected = EditClientt.SelectedItem as Client;
            if (selected != null && selected.Id.ToString().Length > 0)
            {
                show.IsEnabled = true;
                delete.IsEnabled = true;
                update.IsEnabled = true;
            }
            else
            {
                show.IsEnabled = false;
                delete.IsEnabled = false;
                update.IsEnabled = false;
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Client selected = EditClientt.SelectedItem as Client;
            if (selected == null)
                MessageBox.Show("You must select a client");
            else
            {
                UpdateClient client = new UpdateClient(selected);
                client.ShowDialog();
            }
        }

        private void EditClientt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void show_Click(object sender, RoutedEventArgs e)
        {
            Client selected = EditClientt.SelectedItem as Client;
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
