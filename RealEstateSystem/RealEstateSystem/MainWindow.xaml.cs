﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using EstateData;

namespace RealEstateSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        // ESTATE START
        private void MenuItem_ViewEstate(object sender, RoutedEventArgs e)
        {
            ViewEstates estate = new ViewEstates();
            estate.ShowDialog();
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_About(object sender, RoutedEventArgs e)
        {
            About aboutDialog = new About();
            aboutDialog.ShowDialog();
        }

        private void MenuItem_AddEstate(object sender, RoutedEventArgs e)
        {
            AddEstate estete = new AddEstate();
            estete.ShowDialog();
        }

        private void MenuItem_EditEstate(object sender, RoutedEventArgs e)
        {
            EditEstate estete = new EditEstate();
            estete.ShowDialog();
        }

        private void showparams_Click(object sender, RoutedEventArgs e)
        {
            EditEstate estate = new EditEstate();
            estate.ShowDialog();
        }

        // ESTATE END

        // CLIENT START

        private void MenuItem_ViewClient(object sender, RoutedEventArgs e)
        {
            ViewClients client = new ViewClients();
            client.ShowDialog();
        }

        private void MenuItem_AddClient(object sender, RoutedEventArgs e)
        {
            AddClient client = new AddClient();
            client.ShowDialog();
        }

        private void MenuItem_EditClient(object sender, RoutedEventArgs e)
        {
            EditClient client = new EditClient();
            client.ShowDialog();
        }

        // CLIENT END

        // TRANSACTION START

        private void MenuItem_AddTransaction(object sender, RoutedEventArgs e)
        {
            AddTransaction transaction = new AddTransaction();
            transaction.ShowDialog();
        }

        private void MenuItem_ViewTransaction(object sender, RoutedEventArgs e)
        {
            ViewTransaction transaction = new ViewTransaction();
            transaction.ShowDialog();
        }

        // TRANSACTION END

        // AD START

        private void MenuItem_AddAd(object sender, RoutedEventArgs e)
        {
            AddAd ad = new AddAd();
            ad.ShowDialog();
        }

        private void MenuItem_ViewAd(object sender, RoutedEventArgs e)
        {
            ViewAds ad = new ViewAds();
            ad.ShowDialog();
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        // AD END

        private void MenuItem_Summary(object sender, RoutedEventArgs e)
        {
            SqlConnection conN = new SqlConnection();
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.DataSource = ".";
            con.InitialCatalog = "estate";
            con.IntegratedSecurity = true;
            conN.ConnectionString = con.ConnectionString;
            conN.Open();

            string zp = "SELECT COUNT(*) FROM Estate";
            SqlCommand command = conN.CreateCommand();
            command.CommandText = zp;
            SqlDataReader reader = command.ExecuteReader();
            string tekst = "";
            while (reader.Read())
            {
                tekst += reader.GetInt32(0);
            }
            reader.Close();
            command.Cancel();

            zp = "SELECT COUNT(*) FROM Client";
            command = conN.CreateCommand();
            command.CommandText = zp;
            reader = command.ExecuteReader();
            string tekst2 = "";
            while (reader.Read())
            {
                tekst2 += reader.GetInt32(0);
            }
            reader.Close();
            command.Cancel();

            zp = "SELECT SUM(Fee) FROM Estate";
            command = conN.CreateCommand();
            command.CommandText = zp;
            reader = command.ExecuteReader();
            string tekst3 = "";
            while (reader.Read())
            {
                tekst3 += reader.GetDouble(0);
            }
            reader.Close();
            command.Cancel();
            MessageBox.Show("At this moment in our system we have:\n" + tekst + " estates,\n" + tekst2 + " clients.\n\n" + "All our estates generate " + tekst3 + " income.");
        }
        
    }
}
