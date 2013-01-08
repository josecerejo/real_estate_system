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
using System.IO;
namespace RealEstateSystem
{
    /// <summary>
    /// Interaction logic for DetailsEstate.xaml
    /// </summary>
    public partial class DetailsEstate : Window
    {
        private Estate estate;
        private Client owner;
        public DetailsEstate(Estate estate)
        {
            InitializeComponent();
            this.estate = estate;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RealEstateDataDataContext con = new RealEstateDataDataContext();
            titleLabel.Content = estate.Name;
            nameField.Text = estate.Name;
            descriptionField.Text = estate.Description;
            feeField.Text = estate.Fee.ToString();

            owner = con.Clients.Single(c => c.Id == estate.Owner);
            ownerField.Text = (owner.FirstName+" "+owner.LastName);

            byte[] barrImg = (byte[])estate.Photo.ToArray();
            string strfn = Convert.ToString(DateTime.Now.ToFileTime());
            FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
            fs1.Write(barrImg, 0, barrImg.Length);
            fs1.Flush();
            fs1.Close();
            ImageSourceConverter imgs = new ImageSourceConverter();
            image1.SetValue(System.Windows.Controls.Image.SourceProperty, imgs.ConvertFromString(strfn));

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (estate == null)
                System.Windows.MessageBox.Show("Estate is empty (wtf?)");
            else
            {
                UpdateEstate estateUpdateDialog = new UpdateEstate(estate);
                estateUpdateDialog.ShowDialog();
            }
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (estate == null)
                System.Windows.MessageBox.Show("Estate is empty (wtf?)");
            else
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show("Are you sure?", "Yes", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    Admin.DeleteEstate(estate);
                    System.Windows.MessageBox.Show("Estate has been successfull deleted");
                    DialogResult = false; //Window_Loaded(null, null);
                }
            }
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            if (owner == null)
                MessageBox.Show("Owner is empty");
            else
            {
                DetailsClient ownerDetails = new DetailsClient(owner);
                ownerDetails.ShowDialog();
            }
        }

        private void nameField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
