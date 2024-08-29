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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfApp1.Prozori
{
    /// <summary>
    /// Interaction logic for Karte.xaml
    /// </summary>
    public partial class Karte : Window
    {
        string email;

        public Karte(string email_Korisnika)
        {
            InitializeComponent();

            email = email_Korisnika;
            dataGrid_Karte();
        }

        private void dataGrid_Karte()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_UzmiIdKorisnika = new SqlCommand();
            komanda_UzmiIdKorisnika.CommandText = "SELECT KORISNIK.idKorisnika FROM KORISNIK WHERE email = @email";
            komanda_UzmiIdKorisnika.Parameters.AddWithValue("@email", email);
            komanda_UzmiIdKorisnika.Connection = connection;

            SqlDataReader citac_UzmiIdKorisnika = komanda_UzmiIdKorisnika.ExecuteReader();

            if(citac_UzmiIdKorisnika.Read() == false)
            {
                citac_UzmiIdKorisnika.Close();
                System.Windows.MessageBox.Show("Došlo je do greške pri učitavanju korisnika.");
            }
            else
            {
                int idKorisnika = citac_UzmiIdKorisnika.GetInt32(0);
                citac_UzmiIdKorisnika.Close();

                SqlCommand komanda_UcitajPodatkeOKarti = new SqlCommand();
                komanda_UcitajPodatkeOKarti.CommandText = "SELECT TRANSAKCIJA.idKarte, FILM.nazivFilma, PROJEKCIJA.datumProjekcije, FILM.cena, SALA.nazivSale, SEDISTE.idSedista FROM TRANSAKCIJA INNER JOIN KARTA on TRANSAKCIJA.idkarte = KARTA.idKarte INNER JOIN FILM on KARTA.idFilma = FILM.idFilma INNER JOIN PROJEKCIJA on KARTA.idProjekcije = PROJEKCIJA.idProjekcije INNER JOIN SALA on KARTA.idSale = SALA.idSale INNER JOIN SEDISTE on KARTA.idSedista = SEDISTE.idSedista WHERE TRANSAKCIJA.idKorisnika = @idKorisnika GROUP BY TRANSAKCIJA.idKarte, FILM.nazivFilma, FILM.cena, SEDISTE.idSedista, SALA.nazivSale, PROJEKCIJA.datumProjekcije";
                komanda_UcitajPodatkeOKarti.Parameters.AddWithValue("@idKorisnika", idKorisnika);
                komanda_UcitajPodatkeOKarti.Connection = connection;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda_UcitajPodatkeOKarti);

                DataTable dataTable = new DataTable("KARTA_KORISNIK");
                dataAdapter.Fill(dataTable);
                datagrid_KarteKorisnik.ItemsSource = dataTable.DefaultView;
            }

            connection.Close();

        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Filmovi fM = new Filmovi(email);
            fM.Show();
            this.Close();
        }

        private void karte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdKarteKorisnik.Text = dr["idKarte"].ToString();
            }
        }

        private void klik_IzbrisiKartu(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Da li želite da nastavite?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);

            // Provera šta je korisnik izabrao
            if (result == MessageBoxResult.No)
            {
                
            }
            else if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                connection.Open();

                SqlCommand komanda_IzbrisiKartu = new SqlCommand();
                komanda_IzbrisiKartu.CommandText = "DELETE FROM TRANSAKCIJA WHERE idKarte = @idKarte";
                komanda_IzbrisiKartu.Parameters.AddWithValue("@idKarte", Convert.ToInt32(textbox_IdKarteKorisnik.Text));
                komanda_IzbrisiKartu.Connection = connection;

                if (komanda_IzbrisiKartu.ExecuteNonQuery() != 1)
                {
                    System.Windows.MessageBox.Show("Došlo je do greške pri brisanju transakcije.");
                }
                else
                {
                    SqlCommand komanda_UzmiIdSedista = new SqlCommand();
                    komanda_UzmiIdSedista.CommandText = "SELECT idSedista FROM KARTA WHERE idKarte = @idKarte";
                    komanda_UzmiIdSedista.Parameters.AddWithValue("@idKarte", Convert.ToInt32(textbox_IdKarteKorisnik.Text));
                    komanda_UzmiIdSedista.Connection = connection;

                    SqlDataReader citac_UzmiIdSedista = komanda_UzmiIdSedista.ExecuteReader();

                    if (citac_UzmiIdSedista.Read() == false)
                    {
                        citac_UzmiIdSedista.Close();
                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju sedišta.");
                    }
                    else
                    {
                        int idSedista = citac_UzmiIdSedista.GetInt32(0);
                        citac_UzmiIdSedista.Close();

                        SqlCommand komanda_UzmiIdSale = new SqlCommand();
                        komanda_UzmiIdSale.CommandText = "SELECT idSale FROM KARTA WHERE idKarte = @idKarte";
                        komanda_UzmiIdSale.Parameters.AddWithValue("@idKarte", Convert.ToInt32(textbox_IdKarteKorisnik.Text));
                        komanda_UzmiIdSale.Connection = connection;

                        SqlDataReader citac_UzmiIdSale = komanda_UzmiIdSale.ExecuteReader();

                        if (citac_UzmiIdSale.Read() == false)
                        {
                            citac_UzmiIdSale.Close();
                            System.Windows.MessageBox.Show("Došlo je do greške pri čitanju sale.");
                        }
                        else
                        {
                            int idSale = citac_UzmiIdSale.GetInt32(0);
                            citac_UzmiIdSale.Close();

                            komanda_IzbrisiKartu.CommandText = "SELECT idProjekcije FROM KARTA WHERE idKarte = @idKarte";
                            komanda_IzbrisiKartu.Connection = connection;

                            SqlDataReader citac_UzmiIdProjekcije = komanda_IzbrisiKartu.ExecuteReader();

                            if (citac_UzmiIdProjekcije.Read() == true)
                            {
                                int idProjekcije = citac_UzmiIdProjekcije.GetInt32(0);
                                citac_UzmiIdProjekcije.Close();

                                komanda_IzbrisiKartu.CommandText = "UPDATE SE_NALAZI_U SET rezervisano = 'Ne' WHERE idSedista = @idSedista AND idSale = @idSale AND idProjekcije = @idProjekcije";
                                komanda_IzbrisiKartu.Parameters.AddWithValue("@idSedista", idSedista);
                                komanda_IzbrisiKartu.Parameters.AddWithValue("@idSale", idSale);
                                komanda_IzbrisiKartu.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                komanda_IzbrisiKartu.Connection = connection;

                                if (komanda_IzbrisiKartu.ExecuteNonQuery() == 1)
                                {
                                    komanda_IzbrisiKartu.CommandText = "DELETE FROM KARTA WHERE idKarte = @idKarte";
                                    komanda_IzbrisiKartu.Connection = connection;

                                    if (komanda_IzbrisiKartu.ExecuteNonQuery() == 1)
                                    {
                                        dataGrid_Karte();
                                        osveziPolje();
                                        System.Windows.MessageBox.Show("Karta je uspešno izbrisana.");
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Došlo je do greške pri brisanju karte.");
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Došlo je do greške pri izmeni rezervacije sedišta.");
                                }
                            }
                            else
                            {
                                citac_UzmiIdProjekcije.Close();
                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju projekcije.");
                            }
                        }
                    }
                }
            }
        }

        void osveziPolje()
        {
            textbox_IdKarteKorisnik.Text = "";
        }
    }
}
