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
    /// Interaction logic for EditEstate.xaml
    /// </summary>
    public partial class EditEstate : Window
    {
        public EditEstate()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Estate> estates = (from e1 in con.Estates
                                    select e1).ToList();
            EditEstatee.ItemsSource = estates;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Estate selected = EditEstatee.SelectedItem as Estate;
            if (selected == null)
                MessageBox.Show("You must select a student");
            else
            {
                UpdateEstate estate = new UpdateEstate(selected);
                estate.ShowDialog();
            }
        }

        private void EditEstatee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Estate selected = EditEstatee.SelectedItem as Estate;
            if (selected == null)
                MessageBox.Show("You must select a estate");
            else
            {
                if(MessageBoxResult.Yes == MessageBox.Show("Are you sure", "delete Estate", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    Admin.DeleteEstate(selected);
                    Window_Loaded(null,null);
                }
            }

        }

        private void xml_export_click(object sender, RoutedEventArgs e)
        {


        }

    }
}
