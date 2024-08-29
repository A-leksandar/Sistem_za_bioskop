using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
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
    /// Interaction logic for Korisnici_Admin.xaml
    /// </summary>
    public partial class Korisnici_Admin : Window
    {
        public Korisnici_Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dataGrid_Korisnici();
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        private void klik_OsveziTabelu(object sender, RoutedEventArgs e)
        {
            dataGrid_Korisnici();
        }

        private void dataGrid_Korisnici()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT KORISNIK.idKorisnika, KORISNIK.email, KORISNIK.lozinka, KORISNIK.idOsobe, OSOBA.ime, OSOBA.prezime from KORISNIK INNER JOIN OSOBA on KORISNIK.idOsobe = OSOBA.idOsobe";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("KORISNIK");
            dataAdapter.Fill(dataTable);
            datagrid_Korisnici.ItemsSource = dataTable.DefaultView;
        }
    }
}
