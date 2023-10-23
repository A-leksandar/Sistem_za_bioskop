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

namespace Bioskop.Stranice
{
    /// <summary>
    /// Interaction logic for stranicaRadnici.xaml
    /// </summary>
    public partial class stranicaRadnici : Page
    {
        public stranicaRadnici()
        {
            InitializeComponent();
            binDataGrid();
        }

        private void binDataGrid()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT OSOBA.ime, OSOBA.prezime, RADNIK.pozicija, RADNIK.telefon, RADNIK.adresa, RADNIK.jmbg, RADNIK.plata, RADNIK.idRadnika, RADNIK.idOsobe FROM [OSOBA] INNER JOIN [RADNIK] ON OSOBA.idOsobe = RADNIK.idOsobe";
            command.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("RADNIK");

            dataAdapter.Fill(dataTable);
            DataGrid.ItemsSource = dataTable.DefaultView;
        }
        private void ponisti()
        {
            txtbxIme.Text = "";
            txtbxPrezime.Text = "";
            txtbxAdresa.Text = "";
            txtbxPlata.Text = "";
            txtbxJmbg.Text = "";
            txtbxTelefon.Text = "";
            cmbbxPozicija.SelectedIndex = 0;
        }
        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            ponisti();
        }

        protected string proveriIndex(int a)
        {
            string opcija;
            switch (a)
            {
                default:
                case 0:
                    opcija = "Menadzer";
                    break;
                case 1:
                    opcija = "Cistac";
                    break;
                case 2:
                    opcija = "Prodavac karata";
                    break;
                case 3:
                    opcija = "Prodavac hrane";
                    break;
                case 4:
                    opcija = "Administrator";
                    break;
                case 5:
                    opcija = "Tehnicko osoblje";
                    break;
            }
            return opcija;
        }

        protected int indexProveri(string a)
        {
            int b = 0;
            switch (a)
            {
                case "Menadzer":
                    b = 0;
                    break;
                case "Cistac":
                    b = 1;
                    break;
                case "Prodavac karata":
                    b = 2;
                    break;
                case "Prodavac hrane":
                    b = 3;
                    break;
                case "Administrator":
                    b = 4;
                    break;
                case "Tehnicko osoblje":
                    b = 5;
                    break;
            }
            return b;
        }
        private void btnUnesi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [OSOBA] (ime, prezime) VALUES (@ime, @prezime); SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@ime", txtbxIme.Text);
            command.Parameters.AddWithValue("@prezime", txtbxPrezime.Text);
            command.Connection = connection;
            int osobaId = Convert.ToInt32(command.ExecuteScalar());
            command.CommandText = "INSERT INTO [RADNIK] (idOsobe, pozicija, telefon, adresa, jmbg, plata) VALUES (@idOsobe, @pozicija, @telefon, @adresa, @jmbg, @plata)";
            command.Parameters.AddWithValue("@idOsobe", osobaId);
            command.Parameters.AddWithValue("@telefon", txtbxTelefon.Text);
            command.Parameters.AddWithValue("@adresa", txtbxAdresa.Text);
            command.Parameters.AddWithValue("@jmbg", txtbxJmbg.Text);
            command.Parameters.AddWithValue("@plata", txtbxPlata.Text);
            string opcija = proveriIndex(cmbbxPozicija.SelectedIndex);
            command.Parameters.AddWithValue("@pozicija", opcija);
            command.Connection = connection;
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Uspesno uneti podaci.");
                binDataGrid();
            }
            ponisti();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtbxidRadnika.Text = dr["idRadnika"].ToString();
                txtbxidOsobe.Text = dr["idOsobe"].ToString();
                txtbxIme.Text = dr["ime"].ToString();
                txtbxPrezime.Text = dr["prezime"].ToString();
                txtbxTelefon.Text = dr["telefon"].ToString();
                txtbxAdresa.Text = dr["adresa"].ToString();
                txtbxJmbg.Text = dr["jmbg"].ToString();
                txtbxPlata.Text = dr["plata"].ToString();
                cmbbxPozicija.SelectedIndex = indexProveri(dr["pozicija"].ToString());
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "UPDATE OSOBA SET ime = @ime, prezime = @prezime WHERE idOsobe = @idOsobe";
            command1.Parameters.AddWithValue("@ime", txtbxIme.Text);
            command1.Parameters.AddWithValue("@prezime", txtbxPrezime.Text);
            command1.Parameters.AddWithValue("@idOsobe", txtbxidOsobe.Text);
            command1.Connection = connection;
            command1.ExecuteNonQuery();

            SqlCommand command2 = new SqlCommand();
            command2.CommandText = "UPDATE RADNIK SET pozicija = @pozicija, plata = @plata, adresa = @adresa, jmbg = @jmbg, telefon = @telefon WHERE idRadnika = @idRadnika";
            command2.Parameters.AddWithValue("@pozicija", proveriIndex(cmbbxPozicija.SelectedIndex));
            command2.Parameters.AddWithValue("@plata", txtbxPlata.Text);
            command2.Parameters.AddWithValue("@adresa", txtbxAdresa.Text);
            command2.Parameters.AddWithValue("@jmbg", txtbxJmbg.Text);
            command2.Parameters.AddWithValue("@telefon", txtbxTelefon.Text);
            command2.Parameters.AddWithValue("@idRadnika", txtbxidRadnika.Text);
            command2.Connection = connection;
            command2.ExecuteNonQuery();

            MessageBox.Show("Uspesno izmenjeni podaci.");
            binDataGrid();
            ponisti();
        }
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM [RADNIK] WHERE idOsobe = @idOsobe";
            command.Parameters.AddWithValue("@idOsobe", txtbxidOsobe.Text);
            int proveriRadnik = command.ExecuteNonQuery();
            if (proveriRadnik == 1)
            {
                command.CommandText = "DELETE FROM [OSOBA] WHERE idOsobe = @idOsobe";
                int proveriOsoba = command.ExecuteNonQuery();
                if (proveriOsoba == 1)
                {
                    MessageBox.Show("Uspesno izbrisani podaci.");
                    binDataGrid();
                }
            }
            ponisti();
        }
        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();

            Window oldWindow = Window.GetWindow(this);
            oldWindow.Close();
        }
    }
}
