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
    /// Interaction logic for Transakcije_Admin.xaml
    /// </summary>
    public partial class Transakcije_Admin : Window
    {
        public Transakcije_Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        private void dataGrid_Korisnici()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT TRANSAKCIJA.idTransakcije, TRANSAKCIJA.brojKartice, TRANSAKCIJA.bezbednosniBroj, TRANSAKCIJA.idKorisnika, TRANSAKCIJA.idKarte FROM TRANSAKCIJA";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("TRANSAKCIJA");
            dataAdapter.Fill(dataTable);
            datagrid_Transakcije.ItemsSource = dataTable.DefaultView;
        }

        private void klik_OsveziTabelu(object sender, RoutedEventArgs e)
        {
            dataGrid_Korisnici();
        }
    }
}
