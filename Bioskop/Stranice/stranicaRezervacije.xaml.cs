using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// Interaction logic for stranicaRezervacije.xaml
    /// </summary>
    public partial class stranicaRezervacije : Page
    {
        public stranicaRezervacije()
        {
            InitializeComponent();
            binDataGrid();
            binDataGridFilm();
            binDataGridKorisnik();
            binDataGridTransakcija();
        }
        private void binDataGrid()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT KARTA.idKarte, KARTA.idSale, KARTA.idSedista, FILM.cena, FILM.idFilma from [KARTA] INNER JOIN [FILM] on KARTA.idFilma = FILM.idFilma INNER JOIN [SALA] on KARTA.idSale = SALA.idSale INNER JOIN [SEDISTE] on  KARTA.idSedista = SEDISTE.idSedista";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("KARTA");
            dataAdapter.Fill(dataTable);
            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void binDataGridTransakcija()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT TRANSAKCIJA.idTransakcije, TRANSAKCIJA.bezbednosniKod,TRANSAKCIJA.brojKartice, TRANSAKCIJA.idKorisnika, TRANSAKCIJA.idKarte from [TRANSAKCIJA]";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("TRANSAKCIJA");
            dataAdapter.Fill(dataTable);
            DataGridTransakcija.ItemsSource = dataTable.DefaultView;
        }
        private void binDataGridKorisnik()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT KORISNIK.idKorisnika, KORISNIK.korisnickoIme, KORISNIK.email from [KORISNIK]";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("KORISNIK");
            dataAdapter.Fill(dataTable);
            DataGridKorisnik.ItemsSource = dataTable.DefaultView;
        }
        private void binDataGridFilm()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT FILM.idFilma, FILM.naziv, FILM.cena FROM [FILM]";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("FILM");
            dataAdapter.Fill(dataTable);
            DataGridFilm.ItemsSource = dataTable.DefaultView;
        }
        private void ponisti()
        {
            txtbxbrojKartice.Text = "";
            txtbxidKorisnika.Text = "";
            txtbxidTransakcije.Text = "";
            txtbxbrojKartice.Text = "";
            txtbxbezbednosniKod.Text = "";
            txtbxCena.Text = "";
            txtbxidFilma.Text = "";
            txtbxidSedista.Text = "";
            cmbbxidSale.SelectedIndex = 0;
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
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.CommandText = "SELECT zauzeto FROM SEDISTE WHERE idSedista = @idSedista AND idSale = @idSale";
            selectCommand.Parameters.AddWithValue("@idSedista", Convert.ToInt32(txtbxidSedista.Text));
            selectCommand.Parameters.AddWithValue("@idSale", cmbbxidSale.SelectedIndex + 1);
            selectCommand.Connection = connection;
            string zauzeto = selectCommand.ExecuteScalar()?.ToString();
            if (zauzeto == "da")
            {
                connection.Close();
                MessageBox.Show("Sediste je vec zauzeto.");
                ponisti();
                return;
            }
            else
            {
                SqlCommand updateSedisteCommand = new SqlCommand();
                updateSedisteCommand.CommandText = "UPDATE SEDISTE SET zauzeto = 'da' WHERE idSedista = @idSedista AND idSale = @idSale";
                updateSedisteCommand.Parameters.AddWithValue("@idSedista", Convert.ToInt32(txtbxidSedista.Text));
                updateSedisteCommand.Parameters.AddWithValue("@idSale", cmbbxidSale.SelectedIndex+1);
                updateSedisteCommand.Connection = connection;
                updateSedisteCommand.ExecuteNonQuery();
                SqlCommand insertCommandKarta = new SqlCommand();
                insertCommandKarta.CommandText = "INSERT INTO KARTA (idFilma, idSale, idSedista) VALUES (@idFilma, @idSale, @idSedista); SELECT SCOPE_IDENTITY();";
                insertCommandKarta.Parameters.AddWithValue("@idFilma", Convert.ToInt32(txtbxidFilma.Text));
                insertCommandKarta.Parameters.AddWithValue("@idSale", Convert.ToInt32(cmbbxidSale.SelectedIndex + 1));
                insertCommandKarta.Parameters.AddWithValue("@idSedista", Convert.ToInt32(txtbxidSedista.Text));
                insertCommandKarta.Connection = connection;
                int idKarte = Convert.ToInt32(insertCommandKarta.ExecuteScalar());
                SqlCommand command2 = new SqlCommand();
                command2.CommandText = "INSERT INTO TRANSAKCIJA (brojKartice, bezbednosniKod, idKorisnika, idKarte) VALUES (@brojKartice, @bezbednosniKod, @idKorisnika, @idKarte);";
                command2.Parameters.AddWithValue("@brojKartice", txtbxbrojKartice.Text);
                command2.Parameters.AddWithValue("@bezbednosniKod", txtbxbezbednosniKod.Text);
                command2.Parameters.AddWithValue("@idKorisnika", Convert.ToInt32(txtbxidKorisnika.Text));
                command2.Parameters.AddWithValue("@idKarte", idKarte);
                command2.Connection = connection;
                command2.ExecuteNonQuery();
                MessageBox.Show("Uspesno uneti podaci.");
                binDataGrid();
                binDataGridFilm();
                binDataGridKorisnik();
                binDataGridTransakcija();
                ponisti();
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "UPDATE TRANSAKCIJA SET bezbednosniKod = @bezbednosniKod, brojKartice = @brojKartice WHERE idTransakcije = @idTransakcije";
            command1.Parameters.AddWithValue("@idTransakcije", txtbxidTransakcije.Text);
            command1.Parameters.AddWithValue("@bezbednosniKod", txtbxbezbednosniKod.Text);
            command1.Parameters.AddWithValue("@brojKartice", txtbxbrojKartice.Text);
            command1.Connection = connection;
            command1.ExecuteNonQuery();
            command1.Connection = connection;
            if (command1.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Uspesno izmenjeni podaci.");
                binDataGridTransakcija();
            }
            ponisti();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Bioskop.Properties.Settings.BIOSKOPConnectionString"].ConnectionString;
            connection.Open();
            int idKarte = Convert.ToInt32(txtbxidKarte.Text);
            SqlCommand deleteTransakcijaCommand = new SqlCommand();
            deleteTransakcijaCommand.CommandText = "DELETE FROM TRANSAKCIJA WHERE idKarte = @idKarte";
            deleteTransakcijaCommand.Parameters.AddWithValue("@idKarte", idKarte);
            deleteTransakcijaCommand.Connection = connection;
            deleteTransakcijaCommand.ExecuteNonQuery();
            SqlCommand updateSedisteCommand = new SqlCommand();
            updateSedisteCommand.CommandText = "UPDATE SEDISTE SET zauzeto = 'ne' WHERE idSedista IN (SELECT idSedista FROM KARTA WHERE idKarte = @idKarte)";
            updateSedisteCommand.Parameters.AddWithValue("@idKarte", idKarte);
            updateSedisteCommand.Connection = connection;
            updateSedisteCommand.ExecuteNonQuery();
            SqlCommand deleteKartaCommand = new SqlCommand();
            deleteKartaCommand.CommandText = "DELETE FROM KARTA WHERE idKarte = @idKarte";
            deleteKartaCommand.Parameters.AddWithValue("@idKarte", idKarte);
            deleteKartaCommand.Connection = connection;
            deleteKartaCommand.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Podaci o transakciji i karti su uspesno izbrisani.");
            binDataGrid();
            binDataGridFilm();
            binDataGridKorisnik();
            binDataGridTransakcija();
            ponisti();
        }

        private void btnOsvezi_Click(object sender, RoutedEventArgs e)
        {
            binDataGrid();
            binDataGridFilm();
            binDataGridKorisnik();
            binDataGridTransakcija();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();

            Window oldWindow = Window.GetWindow(this);
            oldWindow.Close();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtbxidKarte.Text = dr["idKarte"].ToString();
                txtbxidSedista.Text = dr["idSedista"].ToString();
                txtbxidFilma.Text = dr["idFilma"].ToString();
                txtbxCena.Text = dr["cena"].ToString();
                cmbbxidSale.SelectedIndex = Convert.ToInt32(dr["idSale"]) -1;
                
            }
        }
        private void DataGridFilm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtbxidFilma.Text = dr["idFilma"].ToString();
                txtbxCena.Text = dr["cena"].ToString();
            }
        }
        private void DataGridKorisnik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtbxidKorisnika.Text = dr["idKorisnika"].ToString();
            }
        }

        private void DataGridTransakcija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtbxidTransakcije.Text = dr["idTransakcije"].ToString();
                txtbxidKorisnika.Text = dr["idKorisnika"].ToString();
                txtbxbezbednosniKod.Text = dr["bezbednosniKod"].ToString();
                txtbxbrojKartice.Text = dr["brojKartice"].ToString();
            }
        }
    }
}
