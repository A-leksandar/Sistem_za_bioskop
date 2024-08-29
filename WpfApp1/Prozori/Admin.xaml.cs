using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.Prozori
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void klik_OdjaviSe(object sender, RoutedEventArgs e)
        {
            MainWindow mW = new MainWindow();
            mW.Show();
            this.Close();
        }

        private void klik_FilmoviAdmin(object sender, RoutedEventArgs e)
        {
            Filmovi_Admin fA = new Filmovi_Admin();
            fA.Show();
            this.Close();
        }

        private void klik_GlumciAdmin(object sender, RoutedEventArgs e)
        {
            Glumci_Admin gA = new Glumci_Admin();
            gA.Show();
            this.Close();
        }

        private void klik_UlogeAdmin(object sender, RoutedEventArgs e)
        {
            Uloge_Admin uA = new Uloge_Admin();
            uA.Show();
            this.Close();
        }

        private void klik_ProjekcijeAdmin(object sender, RoutedEventArgs e)
        {
            Projekcije_Admin pA = new Projekcije_Admin();
            pA.Show();
            this.Close();
        }

        private void klik_KarteAdmin(object sender, RoutedEventArgs e)
        {
            Karte_Admin kA = new Karte_Admin();
            kA.Show();
            this.Close();
        }

        private void klik_TransakcijeAdmin(object sender, RoutedEventArgs e)
        {
            Transakcije_Admin tA = new Transakcije_Admin();
            tA.Show();
            this.Close();
        }

        private void klik_KorisniciAdmin(object sender, RoutedEventArgs e)
        {
            Korisnici_Admin koA = new Korisnici_Admin();
            koA.Show();
            this.Close();
        }

        private void klik_RadniciAdmin(object sender, RoutedEventArgs e)
        {
            Radnici_Admin rA = new Radnici_Admin();
            rA.Show();
            this.Close();
        }
    }
}
