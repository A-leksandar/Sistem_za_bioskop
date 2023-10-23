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
    /// Interaction logic for stranicaFilmovi.xaml
    /// </summary>
    public partial class stranicaGlumci : Page
    {
        public stranicaGlumci()
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
            command.CommandText = "SELECT OSOBA.ime, OSOBA.prezime, GLUMAC.idGlumca, GLUMAC.idOsobe from [OSOBA] INNER JOIN [GLUMAC] on OSOBA.idOsobe = GLUMAC.idOsobe";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("GLUMAC");
            dataAdapter.Fill(dataTable);
            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void binDataGridFilm()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT FILM.naziv FROM [FILM] INNER JOIN [FILM_GLUMAC] ON FILM.idFilma = FILM_GLUMAC.idFilma WHERE FILM_GLUMAC.idGlumca = @idGlumca";
            command.Parameters.AddWithValue("@idGlumca", Convert.ToInt32(txtbxidGlumca.Text));
            command.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("FILM");
            dataAdapter.Fill(dataTable);
            DataGridFilm.ItemsSource = dataTable.DefaultView;
        }

        private void ponisti()
        {
            txtbxIme.Text = "";
            txtbxPrezime.Text = "";
            txtbxidGlumca.Text = "";
            txtbxidOsobe.Text = "";
        }
        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            ponisti();
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

            command.CommandText = "INSERT INTO [GLUMAC] (idOsobe) VALUES (@idOsobe)";
            command.Parameters.AddWithValue("@idOsobe", osobaId);

            int proveri = command.ExecuteNonQuery();
            if (proveri == 1)
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
                txtbxidGlumca.Text = dr["idGlumca"].ToString();
                txtbxidOsobe.Text = dr["idOsobe"].ToString();
                txtbxIme.Text = dr["ime"].ToString();
                txtbxPrezime.Text = dr["prezime"].ToString();
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
            command.CommandText = "DELETE FROM [GLUMAC] WHERE idOsobe = @idOsobe";
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

        private void btnOsvezi_Click(object sender, RoutedEventArgs e)
        {
            binDataGrid();
        }

        private void btnPretrazi_Click(object sender, RoutedEventArgs e)
        {
            binDataGridFilm();
        }
    }
}
