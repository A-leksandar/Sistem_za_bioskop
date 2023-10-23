using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bioskop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Filmovi_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Stranice.stranicaFilmovi();
        }

        private void Korisnici_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Stranice.stranicaKorisnici();
        }

        private void Glumci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Stranice.stranicaGlumci();
        }

        private void Radnici_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Stranice.stranicaRadnici();
        }

        private void Rezervacije_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Stranice.stranicaRezervacije();
        }
    }
}
