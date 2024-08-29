using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for Filmovi_Admin.xaml
    /// </summary>
    public partial class Filmovi_Admin : Window
    {
        public Filmovi_Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dataGrid_Film();
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        private string zanr_ProveriIndeks(int a)
        {
            string odabranIndeks;

            switch (a)
            {
                case 0:
                    odabranIndeks = "Komedija";
                    break;
                case 1:
                    odabranIndeks = "Horor";
                    break;
                case 2:
                    odabranIndeks = "Fantazija";
                    break;
                case 3:
                    odabranIndeks = "Naučna fantastika";
                    break;
                case 4:
                    odabranIndeks = "Romansa";
                    break;
                case 5:
                    odabranIndeks = "Akcija";
                    break;
                case 6:
                    odabranIndeks = "Dokumentarac";
                    break;
                case 7:
                    odabranIndeks = "Istorija";
                    break;
                case 8:
                    odabranIndeks = "Triler";
                    break;
                case 9:
                    odabranIndeks = "Kriminalistički";
                    break;
                case 10:
                    odabranIndeks = "Drama";
                    break;
                case 11:
                    odabranIndeks = "Animirani";
                    break;
                case 12:
                    odabranIndeks = "Mjuzikl";
                    break;
                case 13:
                    odabranIndeks = "Misterija";
                    break;
                default:
                    odabranIndeks = "";
                    break;
            }
            return odabranIndeks;
        }

        private void klik_UnesiFilm(object sender, RoutedEventArgs e)
        {
            int zanrInt = combobox_ZanrAdmin.SelectedIndex;
            string idFilmaString = textbox_IdFilmaAdmin.Text;
            string cenaString = textbox_CenaAdmin.Text;
            string godinaIzdanjaString = textbox_GodinaIzdanjaAdmin.Text;

            string nazivFilma = textbox_NazivFilmaAdmin.Text;
            string opisFilma = textbox_OpisAdmin.Text;
            string zanr;
            double cena;
            int godinaIzdanja;

            int slucaj;

            if(idFilmaString == null || idFilmaString == "")
            {
                slucaj = 1;
            }
            else
            {
                slucaj = 0;
            }

            switch(slucaj)
            {
                case 0:
                    System.Windows.MessageBox.Show("Došlo je do greške, osvežite polja i pokušajte ponovo.");
                    break;
                case 1:

                    if(nazivFilma == null || nazivFilma == "")
                    {
                        System.Windows.MessageBox.Show("Polje naziv filma ne sme ostati prazno.");
                    }
                    else
                    {
                        if(nazivFilma.Length >= 51)
                        {
                            System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera (50).");
                        }
                        else
                        {
                            if (godinaIzdanjaString == null || godinaIzdanjaString == "")
                            {
                                System.Windows.MessageBox.Show("Polje godina izdanja ne sme ostati prazno.");
                            }
                            else
                            {
                                if (Regex.IsMatch(godinaIzdanjaString, @"^\d{4}$") == false)
                                {
                                    System.Windows.MessageBox.Show("Polje godina izdanja filma ne sme sadržati ništa osim brojeva.");
                                }
                                else
                                {
                                    godinaIzdanja = Convert.ToInt32(godinaIzdanjaString);

                                    if (godinaIzdanja <= 1894)
                                    {
                                        System.Windows.MessageBox.Show("Polje godina izdanja može sadržati samo vredonsti između 1895 i 2024 (uključujući i njih).");
                                    }
                                    else
                                    {
                                        if (godinaIzdanja >= 2025)
                                        {
                                            System.Windows.MessageBox.Show("Polje godina izdanja može sadržati samo vredonsti između 1895 i 2024 (uključujući i njih).");
                                        }
                                        else
                                        {
                                            if (zanrInt == -1)
                                            {
                                                System.Windows.MessageBox.Show("Polje žanr filma ne sme ostati prazno.");
                                            }
                                            else
                                            {
                                                zanr = zanr_ProveriIndeks(zanrInt);

                                                if (opisFilma == null || opisFilma == "")
                                                {
                                                    System.Windows.MessageBox.Show("Polje opis filma ne sme ostati prazno.");
                                                }
                                                else
                                                {
                                                    if(opisFilma.Length >= 601)
                                                    {
                                                        System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera (600).");
                                                    }
                                                    else
                                                    {
                                                        if (cenaString == null || cenaString == "")
                                                        {
                                                            System.Windows.MessageBox.Show("Polje cena filma ne sme ostati prazno.");
                                                        }
                                                        else
                                                        {
                                                            if (Regex.IsMatch(cenaString, @"^\d+,\d+$") == false)
                                                            {
                                                                System.Windows.MessageBox.Show("Polje cena filma ne sme sadržati ništa osim brojeva.");
                                                            }
                                                            else
                                                            {
                                                                cena = Convert.ToDouble(cenaString);

                                                                if (cena <= 299)
                                                                {
                                                                    System.Windows.MessageBox.Show("Cena mora biti veća od 0.");
                                                                }
                                                                else
                                                                {
                                                                    SqlConnection connection = new SqlConnection();
                                                                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                                                    connection.Open();

                                                                    SqlCommand komanda_DodajFilm = new SqlCommand();
                                                                    komanda_DodajFilm.CommandText = "INSERT INTO FILM (nazivFilma, opisFilma, godinaIzdanja, zanr, cena) VALUES (@nazivFilma, @opisFilma, @godinaIzdanja, @zanr, @cena)";
                                                                    komanda_DodajFilm.Parameters.AddWithValue("@nazivFilma", nazivFilma);
                                                                    komanda_DodajFilm.Parameters.AddWithValue("@opisFilma", opisFilma);
                                                                    komanda_DodajFilm.Parameters.AddWithValue("@godinaIzdanja", godinaIzdanja);
                                                                    komanda_DodajFilm.Parameters.AddWithValue("@zanr", zanr);
                                                                    komanda_DodajFilm.Parameters.AddWithValue("@cena", cena);
                                                                    komanda_DodajFilm.Connection = connection;

                                                                    int proveri = komanda_DodajFilm.ExecuteNonQuery();
                                                                    if (proveri == 1)
                                                                    {
                                                                        osveziPolja();
                                                                        dataGrid_Film();
                                                                        MessageBox.Show("Film je uspešno unešen u bazu.", "Uspešan unos", MessageBoxButton.OK);
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri unosu.");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            
        }

        void osveziPolja()
        {
            textbox_IdFilmaAdmin.Text = "";
            textbox_NazivFilmaAdmin.Text = "";
            textbox_OpisAdmin.Text = "";
            textbox_GodinaIzdanjaAdmin.Text = "";
            textbox_CenaAdmin.Text = "";
            combobox_ZanrAdmin.SelectedIndex = -1;
        }

        void dataGrid_Film()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT FILM.idFilma, FILM.nazivFilma, FILM.opisFilma, FILM.godinaIzdanja, FILM.zanr, FILM.cena FROM FILM";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("FILM");
            dataAdapter.Fill(dataTable);
            datagrid_Filmovi.ItemsSource = dataTable.DefaultView;
        }

        private void klik_IzmeniFilm(object sender, RoutedEventArgs e)
        {
            int zanrInt = combobox_ZanrAdmin.SelectedIndex;
            string idFilmaString = textbox_IdFilmaAdmin.Text;
            string cenaString = textbox_CenaAdmin.Text;
            string godinaIzdanjaString = textbox_GodinaIzdanjaAdmin.Text;

            string nazivFilma = textbox_NazivFilmaAdmin.Text;
            string opisFilma = textbox_OpisAdmin.Text;
            string zanr;
            double cena;
            int godinaIzdanja;

            int slucaj;

            if (idFilmaString == null || idFilmaString == "")
            {
                slucaj = 0;
            }
            else
            {
                slucaj = 1;
            }

            switch (slucaj)
            {
                case 0:
                    System.Windows.MessageBox.Show("Došlo je do greške, odaberite film iz tabele.");
                    break;
                case 1:

                    if (nazivFilma == null || nazivFilma == "")
                    {
                        System.Windows.MessageBox.Show("Polje naziv filma ne sme ostati prazno.");
                    }
                    else
                    {
                        if(nazivFilma.Length >= 51)
                        {
                            System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera (600).");
                        }
                        else
                        {
                            if (godinaIzdanjaString == null || godinaIzdanjaString == "")
                            {
                                System.Windows.MessageBox.Show("Polje godina izdanja ne sme ostati prazno.");
                            }
                            else
                            {
                                if (Regex.IsMatch(godinaIzdanjaString, @"^\d{4}$") == false)
                                {
                                    System.Windows.MessageBox.Show("Polje godina izdanja filma ne sme sadržati ništa osim brojeva.");
                                }
                                else
                                {
                                    godinaIzdanja = Convert.ToInt32(godinaIzdanjaString);

                                    if (godinaIzdanja <= 1894)
                                    {
                                        System.Windows.MessageBox.Show("Polje godina izdanja može sadržati samo vredonsti između 1895 i 2024 (uključujući i njih).");
                                    }
                                    else
                                    {
                                        if (godinaIzdanja >= 2025)
                                        {
                                            System.Windows.MessageBox.Show("Polje godina izdanja može sadržati samo vredonsti između 1895 i 2024 (uključujući i njih).");
                                        }
                                        else
                                        {
                                            if (zanrInt == -1)
                                            {
                                                System.Windows.MessageBox.Show("Polje žanr filma ne sme ostati prazno.");
                                            }
                                            else
                                            {
                                                zanr = zanr_ProveriIndeks(zanrInt);

                                                if (opisFilma == null || opisFilma == "")
                                                {
                                                    System.Windows.MessageBox.Show("Polje opis filma ne sme ostati prazno.");
                                                }
                                                else
                                                {
                                                    if(opisFilma.Length >= 601)
                                                    {
                                                        System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera.");
                                                    }
                                                    else
                                                    {
                                                        if (cenaString == null || cenaString == "")
                                                        {
                                                            System.Windows.MessageBox.Show("Polje cena filma ne sme ostati prazno.");
                                                        }
                                                        else
                                                        {
                                                            string sablon = "^\\d+(,\\d+)?$";
                                                            if (Regex.IsMatch(cenaString, sablon) == false)
                                                            {
                                                                System.Windows.MessageBox.Show("Polje cena filma ne sme sadržati ništa osim brojeva i mora sadržati jedan decimalni zarez.");
                                                            }
                                                            else
                                                            {
                                                                cena = Convert.ToDouble(cenaString);

                                                                if (cena <= 299)
                                                                {
                                                                    System.Windows.MessageBox.Show("Cena mora biti veća od 0.");
                                                                }
                                                                else
                                                                {
                                                                    SqlConnection connection = new SqlConnection();
                                                                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                                                    connection.Open();

                                                                    SqlCommand komanda_IzmeniFilm = new SqlCommand();

                                                                    komanda_IzmeniFilm.CommandText = "UPDATE FILM SET nazivFilma = @nazivFilma, opisFilma = @opisFilma, godinaIzdanja = @godinaIzdanja, zanr = @zanr, cena = @cena WHERE idFilma = @idFilma";
                                                                    komanda_IzmeniFilm.Parameters.AddWithValue("@idFilma", Convert.ToInt32(idFilmaString));
                                                                    komanda_IzmeniFilm.Parameters.AddWithValue("@nazivFilma", nazivFilma);
                                                                    komanda_IzmeniFilm.Parameters.AddWithValue("@opisFilma", opisFilma);
                                                                    komanda_IzmeniFilm.Parameters.AddWithValue("@godinaIzdanja", godinaIzdanja);
                                                                    komanda_IzmeniFilm.Parameters.AddWithValue("@zanr", zanr);
                                                                    komanda_IzmeniFilm.Parameters.AddWithValue("@cena", cena);
                                                                    komanda_IzmeniFilm.Connection = connection;

                                                                    int proveri = komanda_IzmeniFilm.ExecuteNonQuery();
                                                                    if (proveri == 1)
                                                                    {
                                                                        osveziPolja();
                                                                        dataGrid_Film();
                                                                        MessageBox.Show("Film je uspešno izmenjen.", "Uspešna izmena", MessageBoxButton.OK);
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri izmeni.");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private void filmovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                string zanrFilma = dr["zanr"].ToString();

                textbox_IdFilmaAdmin.Text       = dr["idFilma"].ToString();
                textbox_NazivFilmaAdmin.Text    = dr["nazivFilma"].ToString();
                textbox_GodinaIzdanjaAdmin.Text = dr["godinaIzdanja"].ToString();
                textbox_OpisAdmin.Text          = dr["opisFilma"].ToString();
                textbox_CenaAdmin.Text          = dr["cena"].ToString();

                combobox_ZanrAdmin.SelectedIndex = pretvori_UIndex(zanrFilma);
            }
        }

        private int pretvori_UIndex(string zanrFilma)
        {
            int a=0;

            if (zanrFilma == "Komedija")          { a = 0; }
            if (zanrFilma == "Horor")             { a = 1; }
            if (zanrFilma == "Fantazija")         { a = 2; }
            if (zanrFilma == "Naučna fantastika") { a = 3; }
            if (zanrFilma == "Romansa")           { a = 4; }
            if (zanrFilma == "Akcija")            { a = 5; }
            if (zanrFilma == "Dokumentarac")      { a = 6; }
            if (zanrFilma == "Istorija")          { a = 7; }
            if (zanrFilma == "Triler")            { a = 8; }
            if (zanrFilma == "Kriminalistički")   { a = 9; }
            if (zanrFilma == "Drama")             { a = 10;}
            if (zanrFilma == "Animirani")         { a = 11;}
            if (zanrFilma == "Mjuzikl")           { a = 12;}
            if (zanrFilma == "Misterija")         { a = 13;}

            return a;
        }

        private void klik_IzbrisiFilm(object sender, RoutedEventArgs e)
        {
            int zanrInt = combobox_ZanrAdmin.SelectedIndex;

            string idFilmaString = textbox_IdFilmaAdmin.Text;

            string cenaString = textbox_CenaAdmin.Text;

            string godinaIzdanjaString = textbox_GodinaIzdanjaAdmin.Text;

            string nazivFilma = textbox_NazivFilmaAdmin.Text;

            string opisFilma = textbox_OpisAdmin.Text;

            string zanr;
            double cena;
            int godinaIzdanja;

            int slucaj;

            if (idFilmaString == null || idFilmaString == "")
            {
                slucaj = 0;
            }
            else
            {
                slucaj = 1;
            }

            switch (slucaj)
            {
                case 0:
                    System.Windows.MessageBox.Show("Došlo je do greške, odaberite film iz tabele.");
                    break;
                case 1:

                    if (nazivFilma == null || nazivFilma == "")
                    {
                        System.Windows.MessageBox.Show("Polje naziv filma ne sme ostati prazno.");
                    }
                    else
                    {
                        if (godinaIzdanjaString == null || godinaIzdanjaString == "")
                        {
                            System.Windows.MessageBox.Show("Polje godina izdanja ne sme ostati prazno.");
                        }
                        else
                        {
                            if (Regex.IsMatch(godinaIzdanjaString, @"^\d{4}$") == false)
                            {
                                System.Windows.MessageBox.Show("Polje godina izdanja filma ne sme sadržati ništa osim brojeva.");
                            }
                            else
                            {
                                godinaIzdanja = Convert.ToInt32(godinaIzdanjaString);

                                if (godinaIzdanja <= 1894)
                                {
                                    System.Windows.MessageBox.Show("Polje godina izdanja može sadržati samo vredonsti između 1895 i 2024 (uključujući i njih).");
                                }
                                else
                                {
                                    if (godinaIzdanja >= 2025)
                                    {
                                        System.Windows.MessageBox.Show("Polje godina izdanja može sadržati samo vredonsti između 1895 i 2024 (uključujući i njih).");
                                    }
                                    else
                                    {
                                        if (zanrInt == -1)
                                        {
                                            System.Windows.MessageBox.Show("Polje žanr filma ne sme ostati prazno.");
                                        }
                                        else
                                        {
                                            zanr = zanr_ProveriIndeks(zanrInt);

                                            if (opisFilma == null || opisFilma == "")
                                            {
                                                System.Windows.MessageBox.Show("Polje opis filma ne sme ostati prazno.");
                                            }
                                            else
                                            {
                                                if (cenaString == null || cenaString == "")
                                                {
                                                    System.Windows.MessageBox.Show("Polje cena filma ne sme ostati prazno.");
                                                }
                                                else
                                                {
                                                    if (Regex.IsMatch(cenaString, @"^\d+,\d+$") == false)
                                                    {
                                                        System.Windows.MessageBox.Show("Polje cena filma ne sme sadržati ništa osim brojeva i mora sadržati jedan decimalni zarez.");
                                                    }
                                                    else
                                                    {
                                                        cena = Convert.ToDouble(cenaString);

                                                        if (cena <= 299)
                                                        {
                                                            System.Windows.MessageBox.Show("Cena mora biti veća od 0.");
                                                        }
                                                        else
                                                        {
                                                            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete film? Ova akcija će obrisati sve transakcije vezane za film, kao i karte i projekcije.", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);

                                                            if (result == MessageBoxResult.No)
                                                            {

                                                            }
                                                            else if (result == MessageBoxResult.Yes)
                                                            {
                                                                SqlConnection connection = new SqlConnection();
                                                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                                                connection.Open();

                                                                SqlCommand komanda_UzmiIdKarte = new SqlCommand();
                                                                komanda_UzmiIdKarte.CommandText = "SELECT MAX(idKarte) FROM KARTA";
                                                                komanda_UzmiIdKarte.Connection = connection;

                                                                SqlDataReader citac_MaxIdKarte = komanda_UzmiIdKarte.ExecuteReader();

                                                                if(citac_MaxIdKarte.Read() == true)
                                                                {
                                                                    if (!citac_MaxIdKarte.IsDBNull(0))
                                                                    {
                                                                        int maxIdKarte = citac_MaxIdKarte.GetInt32(0);
                                                                        citac_MaxIdKarte.Close();

                                                                        for (int i = 1; i <= maxIdKarte; i++)
                                                                        {
                                                                            komanda_UzmiIdKarte.CommandText = "SELECT idFilma FROM KARTA WHERE idKarte = @idKarte";
                                                                            komanda_UzmiIdKarte.Parameters.AddWithValue("@idKarte", i);
                                                                            komanda_UzmiIdKarte.Connection = connection;

                                                                            SqlDataReader citac_UzmiIdKarte = komanda_UzmiIdKarte.ExecuteReader();

                                                                            if (citac_UzmiIdKarte.Read() == false)
                                                                            {
                                                                                citac_UzmiIdKarte.Close();
                                                                                komanda_UzmiIdKarte.Parameters.Clear();
                                                                            }
                                                                            else
                                                                            {
                                                                                int trenutni_IdFilma = citac_UzmiIdKarte.GetInt32(0);
                                                                                citac_UzmiIdKarte.Close();
                                                                                komanda_UzmiIdKarte.Parameters.Clear();

                                                                                if (trenutni_IdFilma != Convert.ToInt32(idFilmaString))
                                                                                {

                                                                                }
                                                                                else
                                                                                {
                                                                                    SqlCommand komanda_IzbrisiTransakciju = new SqlCommand();
                                                                                    komanda_IzbrisiTransakciju.CommandText = "DELETE FROM TRANSAKCIJA WHERE idKarte = @idKarte";
                                                                                    komanda_IzbrisiTransakciju.Parameters.AddWithValue("@idKarte", i);
                                                                                    komanda_IzbrisiTransakciju.Connection = connection;

                                                                                    if (komanda_IzbrisiTransakciju.ExecuteNonQuery() != 1)
                                                                                    {
                                                                                        komanda_IzbrisiTransakciju.Parameters.Clear();
                                                                                        System.Windows.MessageBox.Show("Došlo je do problema pri brisanju transakcije.");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        komanda_IzbrisiTransakciju.Parameters.Clear();
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        citac_MaxIdKarte.Close();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    citac_MaxIdKarte.Close();
                                                                    System.Windows.MessageBox.Show("Došlo je do greše pri čitanju poslednjeg ID-a karte.");
                                                                }

                                                                SqlCommand komanda_IzbrisiFilm = new SqlCommand();

                                                                komanda_IzbrisiFilm.CommandText = "DELETE FROM GLUMI WHERE idFilma = @idFilma";
                                                                komanda_IzbrisiFilm.Parameters.AddWithValue("@idFilma", Convert.ToInt32(idFilmaString));
                                                                komanda_IzbrisiFilm.Connection = connection;

                                                                if (komanda_IzbrisiFilm.ExecuteNonQuery() >= 0)
                                                                {
                                                                    komanda_IzbrisiFilm.CommandText = "SELECT MAX(idProjekcije) FROM PROJEKCIJA";
                                                                    komanda_IzbrisiFilm.Connection = connection;

                                                                    SqlDataReader citac_UzmiMaxIdProjekcije = komanda_IzbrisiFilm.ExecuteReader();

                                                                    if(citac_UzmiMaxIdProjekcije.Read() == false)
                                                                    {
                                                                        citac_UzmiMaxIdProjekcije.Close();
                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju najvećeg ID-a projekcije.");
                                                                    }
                                                                    else
                                                                    {
                                                                        if(!citac_UzmiMaxIdProjekcije.IsDBNull(0))
                                                                        {
                                                                            int brojProjekcija = citac_UzmiMaxIdProjekcije.GetInt32(0);

                                                                            citac_UzmiMaxIdProjekcije.Close();

                                                                            komanda_IzbrisiFilm.Parameters.Clear();

                                                                            for(int i = 1; i <= brojProjekcija; i++)
                                                                            {
                                                                                komanda_IzbrisiFilm.CommandText = "SELECT idFilma FROM PROJEKCIJA WHERE idProjekcije = @idProjekcije";
                                                                                komanda_IzbrisiFilm.Parameters.AddWithValue("@idProjekcije", i);

                                                                                SqlDataReader citac_ProveriIdFilma = komanda_IzbrisiFilm.ExecuteReader();

                                                                                if(citac_ProveriIdFilma.Read() == false)
                                                                                {
                                                                                    citac_ProveriIdFilma.Close();
                                                                                    komanda_IzbrisiFilm.Parameters.Clear();
                                                                                }
                                                                                else
                                                                                {
                                                                                    int proveri_IdFilma = citac_ProveriIdFilma.GetInt32(0);
                                                                                    citac_ProveriIdFilma .Close();

                                                                                    komanda_IzbrisiFilm.Parameters.Clear();

                                                                                    if (Convert.ToInt32(idFilmaString) != proveri_IdFilma)
                                                                                    {
                                                                                        continue;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        komanda_IzbrisiFilm.CommandText = "DELETE FROM SE_NALAZI_U WHERE idProjekcije = @idProjekcije";
                                                                                        komanda_IzbrisiFilm.Parameters.AddWithValue("@idProjekcije", i);
                                                                                        komanda_IzbrisiFilm.Connection = connection;

                                                                                        if(komanda_IzbrisiFilm.ExecuteNonQuery() != 60)
                                                                                        {
                                                                                            komanda_IzbrisiFilm.Parameters.Clear();
                                                                                            System.Windows.MessageBox.Show("Došlo je do greške pri brisanju sedišta.");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            komanda_IzbrisiFilm.CommandText = "DELETE FROM KARTA WHERE idProjekcije = @idProjekcije";
                                                                                            komanda_IzbrisiFilm.Connection = connection;

                                                                                            if(komanda_IzbrisiFilm.ExecuteNonQuery() < 0)
                                                                                            {
                                                                                                komanda_IzbrisiFilm.Parameters.Clear();
                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri brisanju karte.");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                komanda_IzbrisiFilm.CommandText = "DELETE FROM PROJEKCIJA WHERE idProjekcije = @idProjekcije";
                                                                                                komanda_IzbrisiFilm.Connection = connection;

                                                                                                if (komanda_IzbrisiFilm.ExecuteNonQuery() != 1)
                                                                                                {
                                                                                                    komanda_IzbrisiFilm.Parameters.Clear();
                                                                                                    System.Windows.MessageBox.Show("Došlo je do greške pri brisanju projekcije.");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    komanda_IzbrisiFilm.Parameters.Clear();
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            citac_UzmiMaxIdProjekcije.Close();
                                                                        }
                                                                    }

                                                                    komanda_IzbrisiFilm.Parameters.Clear();

                                                                    komanda_IzbrisiFilm.CommandText = "DELETE FROM FILM WHERE nazivFilma = @nazivFilma AND opisFilma = @opisFilma AND godinaIzdanja = @godinaIzdanja AND zanr = @zanr AND cena = @cena AND idFilma = @idFilma";
                                                                    komanda_IzbrisiFilm.Parameters.AddWithValue("@nazivFilma", nazivFilma);
                                                                    komanda_IzbrisiFilm.Parameters.AddWithValue("@opisFilma", opisFilma);
                                                                    komanda_IzbrisiFilm.Parameters.AddWithValue("@godinaIzdanja", godinaIzdanja);
                                                                    komanda_IzbrisiFilm.Parameters.AddWithValue("@zanr", zanr);
                                                                    komanda_IzbrisiFilm.Parameters.AddWithValue("@cena", cena);
                                                                    komanda_IzbrisiFilm.Parameters.AddWithValue("@idFilma", Convert.ToInt32(idFilmaString));
                                                                    komanda_IzbrisiFilm.Connection = connection;

                                                                    if (komanda_IzbrisiFilm.ExecuteNonQuery() == 1)
                                                                    {
                                                                        osveziPolja();
                                                                        dataGrid_Film();
                                                                        System.Windows.MessageBox.Show("Film je uspešno izbrisan.", "Uspešno brisanje", MessageBoxButton.OK);
                                                                    }
                                                                    else
                                                                    {
                                                                        osveziPolja();
                                                                        dataGrid_Film();
                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri brisanju, ponovo odaberite film iz tabele.");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Windows.MessageBox.Show("Došlo je do greške pri brisanju uloga.");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private void klik_OsveziPolja(object sender, RoutedEventArgs e)
        {
            osveziPolja();
        }

        private void klik_OsveziTabelu(object sender, RoutedEventArgs e)
        {
            dataGrid_Film();
        }
    }
}
