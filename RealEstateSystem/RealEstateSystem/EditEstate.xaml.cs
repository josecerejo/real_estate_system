using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq; 
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
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

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

        private RealEstateDataDataContext con;
        private List<Estate> estates;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            con = new RealEstateDataDataContext();
            estates = (from e1 in con.Estates
                                    select e1).ToList();
            EditEstatee.ItemsSource = estates;
            EditEstatee.SelectionChanged += new SelectionChangedEventHandler(EditEstateeSelectionChanged);
            EditEstatee.IsManipulationEnabled = false;
            EditEstatee.IsReadOnly = true;

            show.IsEnabled   = false;
            delete.IsEnabled = false;
            update.IsEnabled = false;
            //export.IsEnabled = false;
            
        }

        private void EditEstateeSelectionChanged(object sender, EventArgs e)
        {
            Estate selected = EditEstatee.SelectedItem as Estate;
            if (selected != null && selected.Id.ToString().Length > 0)
            {
                show.IsEnabled = true;
                delete.IsEnabled = true;
                update.IsEnabled = true;
                //export.IsEnabled = true;
            }
            else
            {
                show.IsEnabled = false;
                delete.IsEnabled = false;
                update.IsEnabled = false;
                //export.IsEnabled = false;
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Estate selected = EditEstatee.SelectedItem as Estate;
            if (selected == null)
                System.Windows.MessageBox.Show("You must select a student");
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
                System.Windows.MessageBox.Show("You must select a estate");
            else
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show("Are you sure?", "Yes", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    Admin.DeleteEstate(selected);
                    Window_Loaded(null,null);
                }
            }
        }

        private void xml_export_click(object sender, RoutedEventArgs e)
        {
            XElement xmlfromdb = new XElement("Estates",
                                    from e1 in estates
                                    select
                                    new XElement("Estate",
                                        new XElement("ID", e1.Id),
                                        new XElement("Name", e1.Name),
                                        new XElement("Description", e1.Description),
                                        new XElement("Fee", e1.Fee),
                                        new XElement("Owner", e1.Owner)
                                    )
                                 );

            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.Filter = "XML document (*.xml)|*.xml"; 
            save.AddExtension = true;
            Nullable<bool> result = save.ShowDialog();
            if (save.FileName.Length > 0)
            {
                xmlfromdb.Save(save.FileName);
                System.Windows.MessageBox.Show("XML has been successfully saved");
            }
            else
            {
                System.Windows.MessageBox.Show("XML save failed - please select file destination");
            }


        }

        private void show_Click(object sender, RoutedEventArgs e)
        {
            Estate selected = EditEstatee.SelectedItem as Estate;
            if (selected == null)
                System.Windows.MessageBox.Show("You must select a student");
            else
            {
                DetailsEstate estate_details = new DetailsEstate(selected);
                estate_details.ShowDialog();
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            findFilteredEstates();
        }

        private void findFilteredEstates()
        {
            float fee;
            float.TryParse(Fee.Text, out fee);

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            estates = (from e1 in con.Estates
                                    where e1.Fee >= fee
                                    select e1).ToList();
            EditEstatee.ItemsSource = estates;
        }

        private void Fee_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                findFilteredEstates();
            }
        }

    }
}
