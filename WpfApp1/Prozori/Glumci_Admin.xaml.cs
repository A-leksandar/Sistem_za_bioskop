using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Glumci_Admin.xaml
    /// </summary>
    public partial class Glumci_Admin : Window
    {
        public Glumci_Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dataGrid_Glumac();
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        private void klik_UnesiGlumca(object sender, RoutedEventArgs e)
        {
            int idOsobe;

            string ime = textbox_ImeGlumcaAdmin.Text;
            string prezime = textbox_PrezimeGlumcaAdmin.Text;

            if(ime == null)
            {
                System.Windows.MessageBox.Show("Polje ime ne sme ostati prazno.");
            }
            else
            {
                if(ime.Length >= 31)
                {
                    System.Windows.MessageBox.Show("Prekoračen dozvoljen broj karaktera za ime (30).");
                }
                else
                {
                    if(prezime == null)
                    {
                        System.Windows.MessageBox.Show("Polje prezime ne sme ostati prazno.");
                    }
                    else
                    {
                        if(prezime.Length >= 51)
                        {
                            System.Windows.MessageBox.Show("Prekoračen dozvoljen broj karaktera za prezime (50).");
                        }
                        else
                        {
                            bool proveriIntIme = Regex.IsMatch(ime, @"^[a-zA-Z]+([ ]?[a-zA-Z]+|[-]?[a-zA-Z]+)*$");

                            if (proveriIntIme == false)
                            {
                                System.Windows.MessageBox.Show("Ime ne sme sadržati nista osim slova.");
                            }
                            else
                            {
                                bool proveriPrezime = Regex.IsMatch(prezime, @"^[a-zA-Z]+([ ]?[a-zA-Z]+|[-]?[a-zA-Z]+)*$");

                                if (proveriPrezime == false)
                                {
                                    System.Windows.MessageBox.Show("Prezime ne sme sadržati nista osim slova.");
                                }
                                else
                                {
                                    string idOsobeString = textbox_IdOsobeGlumacAdmin.Text;

                                    int slucaj;

                                    if (idOsobeString == null || idOsobeString == "")
                                    {
                                        slucaj = 0;
                                    }
                                    else
                                    {
                                        idOsobe = int.Parse(idOsobeString);

                                        SqlConnection connection = new SqlConnection();
                                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                        connection.Open();

                                        SqlCommand komanda_ProveriId = new SqlCommand();
                                        komanda_ProveriId.CommandText = "SELECT idOsobe from OSOBA GROUP BY idOsobe HAVING idOsobe = @idOsobe";
                                        komanda_ProveriId.Parameters.AddWithValue("@idOsobe", idOsobe);
                                        komanda_ProveriId.Connection = connection;

                                        SqlDataReader citac_ProveriId = komanda_ProveriId.ExecuteReader();

                                        if (citac_ProveriId.Read() == false)
                                        {
                                            slucaj = 0; //Ne postoji taj ID
                                            citac_ProveriId.Close();
                                        }
                                        else
                                        {
                                            slucaj = 1;
                                            citac_ProveriId.Close();
                                        }
                                    }

                                    switch (slucaj)
                                    {
                                        case 0:
                                            SqlConnection connection = new SqlConnection();
                                            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                            connection.Open();

                                            SqlCommand komanda_DodajGlumca = new SqlCommand();
                                            komanda_DodajGlumca.CommandText = "INSERT INTO OSOBA (ime, prezime) VALUES (@ime, @prezime)";
                                            komanda_DodajGlumca.Parameters.AddWithValue("@ime", ime);
                                            komanda_DodajGlumca.Parameters.AddWithValue("@prezime", prezime);
                                            komanda_DodajGlumca.Connection = connection;

                                            int proveri1 = komanda_DodajGlumca.ExecuteNonQuery();
                                            if (proveri1 == 1)
                                            {
                                                komanda_DodajGlumca.CommandText = "SELECT TOP 1 idOsobe from OSOBA ORDER BY idOsobe DESC";

                                                SqlDataReader citac_UcitajIdOsobe = komanda_DodajGlumca.ExecuteReader();

                                                if (citac_UcitajIdOsobe.Read())
                                                {
                                                    idOsobe = citac_UcitajIdOsobe.GetInt32(0);
                                                    citac_UcitajIdOsobe.Close();

                                                    komanda_DodajGlumca.CommandText = "INSERT INTO [GLUMAC] (idOsobe) VALUES (@idOsobe); SELECT SCOPE_IDENTITY();";
                                                    komanda_DodajGlumca.Parameters.AddWithValue("@idOsobe", idOsobe);
                                                    komanda_DodajGlumca.Connection = connection;

                                                    int proveri2 = komanda_DodajGlumca.ExecuteNonQuery();
                                                    if (proveri2 == 1)
                                                    {
                                                        osveziPolja();
                                                        dataGrid_Glumac();
                                                        MessageBox.Show("Glumac je uspešno unešen u bazu.", "Uspešan unos", MessageBoxButton.OK);
                                                    }
                                                    else
                                                    {
                                                        System.Windows.MessageBox.Show("Greška pri dodavanju glumca.");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Windows.MessageBox.Show("Greška pri dodavanju osobe.");
                                            }
                                            break;
                                        case 1:
                                            System.Windows.MessageBox.Show("Ovaj glumac se već nalazi u bazi.");
                                            break;
                                        default:
                                            System.Windows.MessageBox.Show("Ovaj glumac se već nalazi u bazi.");
                                            break;
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void osveziPolja()
        {
            textbox_IdGlumcaAdmin.Text = "";
            textbox_IdOsobeGlumacAdmin.Text = "";
            textbox_ImeGlumcaAdmin.Text = "";
            textbox_PrezimeGlumcaAdmin.Text = "";
        }

        private void glumci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdGlumcaAdmin.Text = dr["idGlumca"].ToString();
                textbox_IdOsobeGlumacAdmin.Text = dr["idOsobe"].ToString();
                textbox_ImeGlumcaAdmin.Text = dr["ime"].ToString();
                textbox_PrezimeGlumcaAdmin.Text = dr["prezime"].ToString();
            }
        }

        public void dataGrid_Glumac()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT GLUMAC.idGlumca, GLUMAC.idOsobe, OSOBA.ime, OSOBA.prezime FROM [GLUMAC] INNER JOIN [OSOBA] on GLUMAC.idOsobe = OSOBA.idOsobe";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("GLUMAC");
            dataAdapter.Fill(dataTable);
            datagrid_Glumci.ItemsSource = dataTable.DefaultView;
        }

        private void klik_OsveziPolja(object sender, RoutedEventArgs e)
        {
            osveziPolja();
        }

        private void klik_OsveziTabelu(object sender, RoutedEventArgs e)
        {
            dataGrid_Glumac();
        }

        private void klik_IzmeniGlumca(object sender, RoutedEventArgs e)
        {
            string ime = textbox_ImeGlumcaAdmin.Text;
            string prezime = textbox_PrezimeGlumcaAdmin.Text;

            int idOsobe;

            if(textbox_IdOsobeGlumacAdmin.Text == null)
            {
                System.Windows.MessageBox.Show("Morate odabrati glumca iz tabele.");
            }
            else
            {
                idOsobe = Convert.ToInt32(textbox_IdOsobeGlumacAdmin.Text);

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                connection.Open();

                SqlCommand komanda_ProveriIdOsobe = new SqlCommand();

                komanda_ProveriIdOsobe.CommandText = "SELECT idOsobe from [OSOBA] GROUP BY idOsobe HAVING idOsobe = @idOsobe";//Selektovanje IdOsobe-a na osnovu unesenog IdOsobe-a
                komanda_ProveriIdOsobe.Parameters.AddWithValue("@idOsobe", idOsobe);
                komanda_ProveriIdOsobe.Connection = connection;

                SqlDataReader citac_ProveriIdOsobe = komanda_ProveriIdOsobe.ExecuteReader();//Provera da li id osobe postoji u bazi

                if (citac_ProveriIdOsobe.Read() == false)
                {
                    citac_ProveriIdOsobe.Close();
                    System.Windows.MessageBox.Show("Došlo je do greške, ovaj glumac se ne nalazi u bazi.");
                }
                else
                {
                    int proveri_IdOsobe = citac_ProveriIdOsobe.GetInt32(0);
                    citac_ProveriIdOsobe.Close();

                    if(idOsobe == proveri_IdOsobe)
                    {
                        if(ime == null || ime == "")
                        {
                            System.Windows.MessageBox.Show("Polje ime glumca ne sme ostati prazno.");
                        }
                        else
                        {
                            if(ime.Length >= 31)
                            {
                                System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera za ime (30).");
                            }
                            else
                            {
                                if(prezime == null || prezime == "")
                                {
                                    System.Windows.MessageBox.Show("Polje prezime glumca ne sme ostati prazno.");
                                }
                                else
                                {
                                    if(prezime.Length >= 51)
                                    {
                                        System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera za prezime (50).");
                                    }
                                    else
                                    {
                                        SqlCommand komanda_IzmeniGlumca = new SqlCommand();

                                        komanda_IzmeniGlumca.CommandText = "UPDATE OSOBA SET ime = @ime, prezime = @prezime WHERE idOsobe = @idOsobe";
                                        komanda_IzmeniGlumca.Parameters.AddWithValue("@idOsobe", idOsobe);
                                        komanda_IzmeniGlumca.Parameters.AddWithValue("@ime", ime);
                                        komanda_IzmeniGlumca.Parameters.AddWithValue("@prezime", prezime);
                                        komanda_IzmeniGlumca.Connection = connection;

                                        if (komanda_IzmeniGlumca.ExecuteNonQuery() == 1)
                                        {
                                            MessageBox.Show("Uspešno izmenjeni podaci.");
                                            dataGrid_Glumac();
                                        }
                                        else
                                        {
                                            System.Windows.MessageBox.Show("Došlo je do greške pri izmeni.");
                                        }
                                        osveziPolja();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Došlo je do greške pri selekciji.");
                    }
                }
            }
        }

        private void klik_IzbrisiGlumca(object sender, RoutedEventArgs e)
        {
            int idOsobe;

            if (textbox_IdOsobeGlumacAdmin.Text == null)
            {
                System.Windows.MessageBox.Show("Morate odabrati glumca iz tabele.");
            }
            else
            {
                idOsobe = Convert.ToInt32(textbox_IdOsobeGlumacAdmin.Text);

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                connection.Open();

                SqlCommand komanda_ProveriIdOsobe = new SqlCommand();

                komanda_ProveriIdOsobe.CommandText = "SELECT idOsobe from [OSOBA] GROUP BY idOsobe HAVING idOsobe = @idOsobe";//Selektovanje IdOsobe-a na osnovu unesenog IdOsobe-a
                komanda_ProveriIdOsobe.Parameters.AddWithValue("@idOsobe", idOsobe);
                komanda_ProveriIdOsobe.Connection = connection;

                SqlDataReader citac_ProveriIdOsobe = komanda_ProveriIdOsobe.ExecuteReader();//Provera da li id osobe postoji u bazi

                if (citac_ProveriIdOsobe.Read() == false)
                {
                    citac_ProveriIdOsobe.Close();
                    System.Windows.MessageBox.Show("Došlo je do greške, ovaj glumac se ne nalazi u bazi.");
                }
                else
                {
                    int proveri_IdOsobe = citac_ProveriIdOsobe.GetInt32(0);
                    citac_ProveriIdOsobe.Close();

                    if (idOsobe == proveri_IdOsobe)
                    {
                        SqlCommand komanda_IzbrisiOsobu = new SqlCommand();

                        komanda_IzbrisiOsobu.CommandText = "SELECT idGlumca FROM GLUMAC WHERE idOsobe = @idOsobe";
                        komanda_IzbrisiOsobu.Parameters.AddWithValue("@idOsobe", idOsobe);
                        komanda_IzbrisiOsobu.Connection = connection;

                        SqlDataReader citac_UzmiIdGlumca = komanda_IzbrisiOsobu.ExecuteReader();

                        if (citac_UzmiIdGlumca.Read() == false)
                        {
                            citac_UzmiIdGlumca.Close();
                            System.Windows.MessageBox.Show("Došlo je do greške pri čitanju ID-a glumca.");
                        }
                        else
                        {
                            int idGlumca = citac_UzmiIdGlumca.GetInt32(0);
                            citac_UzmiIdGlumca.Close();

                            komanda_IzbrisiOsobu.CommandText = "DELETE FROM GLUMI WHERE idGlumca = @idGlumca";
                            komanda_IzbrisiOsobu.Parameters.AddWithValue("@idOsobe", idOsobe);
                            komanda_IzbrisiOsobu.Connection = connection;

                            if (komanda_IzbrisiOsobu.ExecuteNonQuery() < 0)
                            {
                                System.Windows.MessageBox.Show("Došlo je do greške pri brisanju uloga glumca.");
                            }
                            else
                            {
                                komanda_IzbrisiOsobu.CommandText = "DELETE FROM GLUMAC WHERE idOsobe = @idOsobe";
                                komanda_IzbrisiOsobu.Connection = connection;

                                if (komanda_IzbrisiOsobu.ExecuteNonQuery() == 1)
                                {
                                    komanda_IzbrisiOsobu.CommandText = "DELETE FROM OSOBA WHERE idOsobe = @idOsobe";
                                    komanda_IzbrisiOsobu.Connection = connection;

                                    if (komanda_IzbrisiOsobu.ExecuteNonQuery() == 1)
                                    {
                                        System.Windows.MessageBox.Show("Podaci o glumcu su uspešno izbrisani.");
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Došlo je do greške pri brisanju (2).");
                                    }
                                    dataGrid_Glumac();
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Došlo je do greške pri brisanju (1).");
                                }
                                osveziPolja();
                            }
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Došlo je do greške pri selekciji.");
                    }
                }
            }
        }
    }
}
