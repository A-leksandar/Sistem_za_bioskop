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
    public partial class stranicaFilmovi : Page
    {
        public stranicaFilmovi()
        {
            InitializeComponent();
            binDataGrid();
            binDataGridGlumac();
        }
        private void binDataGrid()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [FILM]";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("FILM");
            dataAdapter.Fill(dataTable);
            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void binDataGridGlumac()
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
            DataGridGlumac.ItemsSource = dataTable.DefaultView;
        }

        private void ponisti()
        {
            txtbxidFilma.Text = "";
            txtbxidGlumca.Text = "";
            txtbxNaziv.Text = "";
            txtbxReziser.Text = "";
            txtbxOpis.Text = "";
            txtbxGodinaIzdanja.Text = "";
            txtbxCena.Text = "";
            cmbbxZanr.SelectedIndex = 0;
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
                    opcija = "Komedija";
                    break;
                case 1:
                    opcija = "Horor";
                    break;
                case 2:
                    opcija = "Fantazija";
                    break;
                case 3:
                    opcija = "Romantika";
                    break;
                case 4:
                    opcija = "Akcija";
                    break;
                case 5:
                    opcija = "Sci-Fi";
                    break;
                case 6:
                    opcija = "Drama";
                    break;
                case 7:
                    opcija = "Western";
                    break;
                case 8:
                    opcija = "Triler";
                    break;
                case 9:
                    opcija = "Misterija";
                    break;
                case 10:
                    opcija = "Animacija";
                    break;
                case 11:
                    opcija = "Mjuzikl";
                    break;
            }
            return opcija;
        }

        protected int indexProveri(string a)
        {
            int b=0;
            switch(a)
            {
                case "Komedija":
                    b = 0;
                    break;
                case "Horor":
                    b = 1;
                    break;
                case "Fantazija":
                    b = 2;
                    break;
                case "Romantika":
                    b = 3;
                    break;
                case "Akcija":
                    b = 4;
                    break;
                case "Sci-Fi":
                    b = 5;
                    break;
                case "Drama":
                    b = 6;
                    break;
                case "Western":
                    b = 7;
                    break;
                case "Triler":
                    b = 8;
                    break;
                case "Misterija":
                    b = 9;
                    break;
                case "Animacija":
                    b = 10;
                    break;
                case "Mjuzikl":
                    b = 11;
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
            command.CommandText = "INSERT INTO [FILM] (naziv, reziser, opis, godinaIzdanja, zanr, cena) VALUES(@naziv, @reziser, @opis, @godinaIzdanja, @zanr, @cena)";
            command.Parameters.AddWithValue("@naziv", txtbxNaziv.Text);
            command.Parameters.AddWithValue("@reziser", txtbxReziser.Text);
            command.Parameters.AddWithValue("@opis", txtbxOpis.Text);
            command.Parameters.AddWithValue("@cena", txtbxCena.Text);
            command.Parameters.AddWithValue("@godinaIzdanja", txtbxGodinaIzdanja.Text);
            string opcija=proveriIndex(cmbbxZanr.SelectedIndex);
            command.Parameters.AddWithValue("@zanr", opcija);
            command.Connection = connection;
            int proveri = command.ExecuteNonQuery();
            if (proveri == 1)
            {
                MessageBox.Show("Uspesno uneti podaci.");
                binDataGrid();
            }
            ponisti();
        }

        private void btnUnesiVezu_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [FILM_GLUMAC] (idFilma, idGlumca) VALUES (@idFilma, @idGlumca)";
            command.Parameters.AddWithValue("@idFilma", Convert.ToInt32(txtbxidFilma.Text));
            command.Parameters.AddWithValue("@idGlumca", Convert.ToInt32(txtbxidGlumca.Text));
            command.Connection = connection;
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
                txtbxidFilma.Text = dr["idFilma"].ToString();
                txtbxNaziv.Text = dr["naziv"].ToString();
                txtbxReziser.Text = dr["reziser"].ToString();
                txtbxOpis.Text = dr["opis"].ToString();
                txtbxGodinaIzdanja.Text = dr["godinaIzdanja"].ToString();
                txtbxCena.Text = dr["cena"].ToString();
                cmbbxZanr.SelectedIndex = indexProveri(dr["zanr"].ToString());
            }
        }

        private void DataGridGlumac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtbxidGlumca.Text = dr["idGlumca"].ToString();
            }
        }
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE FILM SET naziv = @naziv, reziser = @reziser, godinaIzdanja = @godinaIzdanja, zanr = @zanr, opis = @opis, cena = @cena where idFilma = @idFilma";
            command.Parameters.AddWithValue("@idFilma", txtbxidFilma.Text);
            command.Parameters.AddWithValue("@naziv", txtbxNaziv.Text);
            command.Parameters.AddWithValue("@reziser", txtbxReziser.Text);
            command.Parameters.AddWithValue("@opis", txtbxOpis.Text);
            command.Parameters.AddWithValue("@cena", txtbxCena.Text);
            command.Parameters.AddWithValue("@godinaIzdanja", txtbxGodinaIzdanja.Text);
            int a = cmbbxZanr.SelectedIndex;
            command.Parameters.AddWithValue("@zanr", proveriIndex(a));
            command.Connection = connection;
            int proveri = command.ExecuteNonQuery();
            if (proveri == 1)
            {
                MessageBox.Show("Uspesno izmenjeni podaci.");
                binDataGrid();
            }
            ponisti();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand deleteCommand = new SqlCommand();
            deleteCommand.CommandText = "DELETE FROM [FILM_GLUMAC] WHERE idFilma = @idFilma";
            deleteCommand.Parameters.AddWithValue("@idFilma", txtbxidFilma.Text);
            deleteCommand.Connection = connection;
            deleteCommand.ExecuteNonQuery();
            SqlCommand deleteFilmCommand = new SqlCommand();
            deleteFilmCommand.CommandText = "DELETE FROM [FILM] WHERE idFilma = @idFilma";
            deleteFilmCommand.Parameters.AddWithValue("@idFilma", txtbxidFilma.Text);
            deleteFilmCommand.Connection = connection;
            deleteFilmCommand.ExecuteNonQuery();
            MessageBox.Show("Uspesno obrisani podaci i veze sa glumcima.");
            binDataGrid();
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
            binDataGridGlumac();
        }
    }
}
