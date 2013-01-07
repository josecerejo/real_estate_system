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
using Microsoft.Win32;
using System.IO;

namespace RealEstateSystem
{
    /// <summary>
    /// Interaction logic for AddEstate.xaml
    /// </summary>
    public partial class AddEstate : Window
    {
        public AddEstate()
        {
            InitializeComponent();

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Client> client = (from e1 in con.Clients
                                    select e1).ToList();

            comboBox1.ItemsSource = client;
            comboBox1.DisplayMemberPath = "FirstName";
            comboBox1.SelectedValuePath = "Id";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            float number;
            int num;

            if (Name.Text.Trim() == "")
                MessageBox.Show("Missing name");
            if (Fee.Text.Trim() == "")
                MessageBox.Show("Missing fee");
            if (float.TryParse(Fee.Text, out number) == false)
                MessageBox.Show("You must enter a number");
            else
            {
                Estate estate = new Estate();
                estate.Name = Name.Text.Trim();
                estate.Description = Description.Text.Trim();
                estate.Fee = number;

                num = Convert.ToInt32(comboBox1.SelectedValue);
                if (Convert.ToString(File_Path.Text).Length > 0)
                {
                    FileStream photo = File.Open(Convert.ToString(File_Path.Text), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    BinaryReader photoBinary = new BinaryReader(photo);
                    estate.Photo = photoBinary.ReadBytes((int)photo.Length);
                }
                estate.Owner = num;

                Admin.AddEstate(estate);
                MessageBox.Show(estate.Name + " was added");
                DialogResult = true;
                
            }
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = open.ShowDialog();

            if (result == true)
            {
                File_Path.Text = open.FileName;
            }
            else
            {
                System.Windows.MessageBox.Show("Better select any photo");
            }
        }

    }
}
