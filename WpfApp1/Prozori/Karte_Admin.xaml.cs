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
    /// Interaction logic for Karte_Admin.xaml
    /// </summary>
    public partial class Karte_Admin : Window
    {
        public Karte_Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dataGrid_Karte();
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        private void dataGrid_Karte()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT KARTA.idKarte, KARTA.idFilma, KARTA.idSale, KARTA.idSedista FROM KARTA";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("KARTA");
            dataAdapter.Fill(dataTable);
            datagrid_Karte.ItemsSource = dataTable.DefaultView;
        }
    }
}
