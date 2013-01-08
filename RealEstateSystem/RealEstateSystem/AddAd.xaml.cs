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
using Xceed.Wpf.Toolkit;
using EstateData;
using Microsoft.Win32;
using System.IO;

namespace RealEstateSystem
{
    /// <summary>
    /// Interaction logic for AddAd.xaml
    /// </summary>
    public partial class AddAd : Window
    {
        public AddAd()
        {
            InitializeComponent();

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Estate> client = (from e1 in con.Estates
                                   select e1).ToList();

            comboBox1.ItemsSource = client;
            comboBox1.DisplayMemberPath = "Name";
            comboBox1.SelectedValuePath = "Id";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int num;
            DateTime dat;

            if (Title.Text.Trim() == "")
                System.Windows.MessageBox.Show("Missing title");
            if (Description.Text.Trim() == "")
                System.Windows.MessageBox.Show("Missing description");
            else
            {
                Ad ad = new Ad();
                ad.Title = Title.Text.Trim();
                ad.Description = Description.Text.Trim();

                num = Convert.ToInt32(comboBox1.SelectedValue);
                ad.EstateId = num;

                dat = Convert.ToDateTime(datePicker1.Value);
                ad.StartDate = dat;

                dat = Convert.ToDateTime(datePicker2.Value);
                ad.EndDate = dat;

                if (File_Path.Text.Length > 0)
                {
                    FileStream photo = File.Open(Convert.ToString(File_Path.Text), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    BinaryReader photoBinary = new BinaryReader(photo);
                    ad.Photo = photoBinary.ReadBytes((int)photo.Length);
                }
                Admin.AddAd(ad);
                System.Windows.MessageBox.Show(ad.Title + " was added");
                DialogResult = true;
            }
        }

        private void Upload_File(object sender, RoutedEventArgs e)
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

        private void File_Path_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
