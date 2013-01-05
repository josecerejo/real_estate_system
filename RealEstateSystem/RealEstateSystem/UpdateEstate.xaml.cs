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
using System.Drawing;
using EstateData;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;

namespace RealEstateSystem
{
    /// <summary>
    /// Interaction logic for UpdateEstate.xaml
    /// </summary>
    public partial class UpdateEstate : Window
    {
        private Estate estate;

        public UpdateEstate(Estate estate)
        {
            InitializeComponent();

            RealEstateDataDataContext con = new RealEstateDataDataContext();
            List<Client> client = (from e1 in con.Clients
                                   select e1).ToList();

            comboBox1.ItemsSource = client;
            comboBox1.DisplayMemberPath = "FirstName";
            comboBox1.SelectedValuePath = "Id";

            this.estate = estate;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = estate.Name.Trim();
            Description.Text = estate.Description.Trim();
            Fee.Text = estate.Fee.ToString();
            
            comboBox1.SelectedIndex = 0;
            comboBox1.DataContext = estate;

            int strippedImageLength = estate.Photo.Length;
            byte[] imagdata = new byte[strippedImageLength];
            Array.Copy(estate.Photo.ToArray(), 0, imagdata, 0, strippedImageLength);

            byte[] barrImg = (byte[])estate.Photo.ToArray();
            string strfn = Convert.ToString(DateTime.Now.ToFileTime());
            FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
            fs1.Write(barrImg, 0, barrImg.Length);
            fs1.Flush();
            fs1.Close();
            ImageSourceConverter imgs = new ImageSourceConverter();
            imgScan.SetValue(System.Windows.Controls.Image.SourceProperty, imgs.ConvertFromString(strfn));
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
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
                estate.Name = Name.Text.Trim();
                estate.Description = Description.Text.Trim();
                estate.Fee = number;

                num = Convert.ToInt32(comboBox1.SelectedValue);
                FileStream photo = File.Open(Convert.ToString(File_Path.Text), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader photoBinary = new BinaryReader(photo);
                estate.Photo = photoBinary.ReadBytes((int)photo.Length);

                estate.Owner = num;

                Admin.UpdateEstate(estate);
                MessageBox.Show(estate.Name + " was updated");
                DialogResult = true;
            }
        
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            
        }
    }
}
