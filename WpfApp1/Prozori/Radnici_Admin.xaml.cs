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
using System.Text.RegularExpressions;

namespace WpfApp1.Prozori
{
    /// <summary>
    /// Interaction logic for Radnici_Admin.xaml
    /// </summary>
    public partial class Radnici_Admin : Window
    {
        public Radnici_Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dataGrid_Radnik();
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        private void klik_UnesiRadnika(object sender, RoutedEventArgs e)
        {
            int idOsobe;
            int PTT;
            double plata;

            string idRadnikaString = textbox_IdRadnikaAdmin.Text;
            string idOsobeString = textbox_IdOsobeRadnikAdmin.Text;
            string plataString = textbox_PlataRadnikaAdmin.Text;

            string ime = textbox_ImeRadnikaAdmin.Text;
            string prezime = textbox_PrezimeRadnikaAdmin.Text;
            string radnaPozicija = textbox_RadnaPozicijaRadnikaAdmin.Text;
            string JMBG = textbox_JMBGRadnikaAdmin.Text;
            string PTTString = textbox_PTTRadnikaAdmin.Text;
            string adresaStanovanja = textbox_AdresaStanovanjaRadnikaAdmin.Text;
            string kontaktTelefon = textbox_KontaktTelefonRadnikaAdmin.Text;

            if(idRadnikaString != "")
            {
                System.Windows.MessageBox.Show("Polje ID radnika mora biti prazno, osvežite polja.");
            }
            else
            {
                if (ime == null || ime == "")
                {
                    System.Windows.MessageBox.Show("Polje ime ne sme ostati prazno.");
                }
                else
                {
                    if(ime.Length >= 31)
                    {
                        System.Windows.MessageBox.Show("Prekoračen je broj dozvoljenih karaktera za ime (30).");
                    }
                    else
                    {
                        if (prezime == null || prezime == "")
                        {
                            System.Windows.MessageBox.Show("Polje prezime ne sme ostati prazno.");
                        }
                        else
                        {
                            if(prezime.Length >= 51)
                            {
                                System.Windows.MessageBox.Show("Prekoračen je broj dozvoljenih karaktera za prezime (50).");
                            }
                            else
                            {
                                if (radnaPozicija == null || radnaPozicija == "")
                                {
                                    System.Windows.MessageBox.Show("Polje radna pozicija ne sme ostati prazno.");
                                }
                                else
                                {
                                    if (radnaPozicija.Length > 40)
                                    {
                                        System.Windows.MessageBox.Show("Polje radna pozicija ne može sadržati više od 40 karaktera.");
                                    }
                                    else
                                    {
                                        if (JMBG == null || JMBG == "")
                                        {
                                            System.Windows.MessageBox.Show("Polje JMBG ne sme ostati prazno.");
                                        }
                                        else
                                        {
                                            if (JMBG.Length > 13)
                                            {
                                                System.Windows.MessageBox.Show("Polje JMBG ne može sadržati više od 13 karaktera.");
                                            }
                                            else
                                            {
                                                if (JMBG.Length < 13)
                                                {
                                                    System.Windows.MessageBox.Show("Polje JMBG ne može sadržati manje od 13 karaktera.");
                                                }
                                                else
                                                {
                                                    if (PTTString == null || PTTString == "")
                                                    {
                                                        System.Windows.MessageBox.Show("Polje PTT ne sme ostati prazno.");
                                                    }
                                                    else
                                                    {
                                                        if (PTTString.Length > 5)
                                                        {
                                                            System.Windows.MessageBox.Show("Polje PTT ne može sadržati više od 5 karaktera.");
                                                        }
                                                        else
                                                        {
                                                            if (PTTString.Length < 5)
                                                            {
                                                                System.Windows.MessageBox.Show("Polje PTT ne može sadržati manje od 5 karaktera.");
                                                            }
                                                            else
                                                            {
                                                                PTT = Convert.ToInt32(PTTString);

                                                                if (adresaStanovanja == null || adresaStanovanja == "")
                                                                {
                                                                    System.Windows.MessageBox.Show("Polje adresa stanovanja ne sme ostati prazno.");
                                                                }
                                                                else
                                                                {
                                                                    if (adresaStanovanja.Length > 80)
                                                                    {
                                                                        System.Windows.MessageBox.Show("Polje adresa stanovanja ne može sadržati više od 80 karaktera.");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (kontaktTelefon == null || kontaktTelefon == "")
                                                                        {
                                                                            System.Windows.MessageBox.Show("Polje kontakt telefon ne sme ostati prazno.");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (kontaktTelefon.Length > 20)
                                                                            {
                                                                                System.Windows.MessageBox.Show("Polje kontakt telefon ne može sadržati više od 20 karaktera.");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (plataString == null || plataString == "")
                                                                                {
                                                                                    System.Windows.MessageBox.Show("Polje plata ne sme ostati prazno.");
                                                                                }
                                                                                else
                                                                                {
                                                                                    string sablon = "^\\d+(,\\d+)?$";
                                                                                    if (Regex.IsMatch(plataString, sablon) == false)
                                                                                    {
                                                                                        System.Windows.MessageBox.Show("Plata nije odgovarajućeg oblika.");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        plata = Convert.ToDouble(plataString);

                                                                                        SqlConnection connection = new SqlConnection();
                                                                                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                                                                        connection.Open();

                                                                                        SqlCommand komanda_DodajRadnika = new SqlCommand();
                                                                                        komanda_DodajRadnika.CommandText = "INSERT INTO OSOBA (ime, prezime) VALUES (@ime, @prezime)";
                                                                                        komanda_DodajRadnika.Parameters.AddWithValue("@ime", ime);
                                                                                        komanda_DodajRadnika.Parameters.AddWithValue("@prezime", prezime);
                                                                                        komanda_DodajRadnika.Connection = connection;

                                                                                        int proveri1 = komanda_DodajRadnika.ExecuteNonQuery();
                                                                                        if (proveri1 == 1)
                                                                                        {
                                                                                            komanda_DodajRadnika.CommandText = "SELECT TOP 1 idOsobe FROM OSOBA ORDER BY idOsobe DESC";
                                                                                            komanda_DodajRadnika.Connection = connection;

                                                                                            SqlDataReader citac_UzmiIdOsobe = komanda_DodajRadnika.ExecuteReader();

                                                                                            if (citac_UzmiIdOsobe.Read() == false)
                                                                                            {
                                                                                                citac_UzmiIdOsobe.Close();
                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju.");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                idOsobe = citac_UzmiIdOsobe.GetInt32(0);
                                                                                                citac_UzmiIdOsobe.Close();

                                                                                                komanda_DodajRadnika.CommandText = "INSERT INTO RADNIK (idOsobe, radnaPozicija, JMBG, PTT, adresaStanovanja, kontaktTelefon, plata) VALUES (@idOsobe, @radnaPozicija, @JMBG, @PTT, @adresaStanovanja, @kontaktTelefon, @plata)";
                                                                                                komanda_DodajRadnika.Parameters.AddWithValue("@idOsobe", idOsobe);
                                                                                                komanda_DodajRadnika.Parameters.AddWithValue("@radnaPozicija", radnaPozicija);
                                                                                                komanda_DodajRadnika.Parameters.AddWithValue("@JMBG", JMBG);
                                                                                                komanda_DodajRadnika.Parameters.AddWithValue("@PTT", PTT);
                                                                                                komanda_DodajRadnika.Parameters.AddWithValue("@adresaStanovanja", adresaStanovanja);
                                                                                                komanda_DodajRadnika.Parameters.AddWithValue("@kontaktTelefon", kontaktTelefon);
                                                                                                komanda_DodajRadnika.Parameters.AddWithValue("@plata", plata);
                                                                                                komanda_DodajRadnika.Connection = connection;

                                                                                                int proveri2 = komanda_DodajRadnika.ExecuteNonQuery();
                                                                                                if (proveri2 == 1)
                                                                                                {
                                                                                                    osveziPolja();
                                                                                                    dataGrid_Radnik();
                                                                                                    System.Windows.MessageBox.Show("Podaci o radniku su uspešno dodati.");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    System.Windows.MessageBox.Show("Greška pri dodavanju radnika.");
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            System.Windows.MessageBox.Show("Greška pri dodavanju osobe.");
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
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void klik_IzmeniRadnika(object sender, RoutedEventArgs e)
        {
            int idRadnika;
            int idOsobe;
            int PTT;
            double plata;

            string idRadnikaString = textbox_IdRadnikaAdmin.Text;
            string idOsobeString = textbox_IdOsobeRadnikAdmin.Text;
            string plataString = textbox_PlataRadnikaAdmin.Text;

            string ime = textbox_ImeRadnikaAdmin.Text;
            string prezime = textbox_PrezimeRadnikaAdmin.Text;
            string radnaPozicija = textbox_RadnaPozicijaRadnikaAdmin.Text;
            string JMBG = textbox_JMBGRadnikaAdmin.Text;
            string PTTString = textbox_PTTRadnikaAdmin.Text;
            string adresaStanovanja = textbox_AdresaStanovanjaRadnikaAdmin.Text;
            string kontaktTelefon = textbox_KontaktTelefonRadnikaAdmin.Text;

            if (idRadnikaString == "")
            {
                System.Windows.MessageBox.Show("Polje ID radnika ne sme biti prazno, ponovo odaberite radnika iz tabele.");
            }
            else
            {
                idRadnika = Convert.ToInt32(idRadnikaString);

                if(idOsobeString == "")
                {
                    System.Windows.MessageBox.Show("Polje  ID osobe ne sme biti prazno, ponovo odaberite radnika iz tabele.");
                }
                else
                {
                    idOsobe = Convert.ToInt32(idOsobeString);

                    if (ime == null || ime == "")
                    {
                        System.Windows.MessageBox.Show("Polje ime ne sme ostati prazno.");
                    }
                    else
                    {
                        if(ime.Length >= 31)
                        {
                            System.Windows.MessageBox.Show("Prekoračen je broj dozvoljenih karaktera za ime (30).");
                        }
                        else
                        {
                            if (prezime == null || prezime == "")
                            {
                                System.Windows.MessageBox.Show("Polje prezime ne sme ostati prazno.");
                            }
                            else
                            {
                                if(prezime.Length >= 51)
                                {
                                    System.Windows.MessageBox.Show("Prekoračen je broj dozvoljenih karaktera za prezime (50).");
                                }
                                else
                                {
                                    if (radnaPozicija == null || radnaPozicija == "")
                                    {
                                        System.Windows.MessageBox.Show("Polje radna pozicija ne sme ostati prazno.");
                                    }
                                    else
                                    {
                                        if (radnaPozicija.Length > 40)
                                        {
                                            System.Windows.MessageBox.Show("Polje radna pozicija ne može sadržati više od 40 karaktera.");
                                        }
                                        else
                                        {
                                            if (JMBG == null || JMBG == "")
                                            {
                                                System.Windows.MessageBox.Show("Polje JMBG ne sme ostati prazno.");
                                            }
                                            else
                                            {
                                                if (JMBG.Length > 13)
                                                {
                                                    System.Windows.MessageBox.Show("Polje JMBG ne može sadržati više od 13 karaktera.");
                                                }
                                                else
                                                {
                                                    if (JMBG.Length < 13)
                                                    {
                                                        System.Windows.MessageBox.Show("Polje JMBG ne može sadržati manje od 13 karaktera.");
                                                    }
                                                    else
                                                    {
                                                        if (PTTString == null || PTTString == "")
                                                        {
                                                            System.Windows.MessageBox.Show("Polje PTT ne sme ostati prazno.");
                                                        }
                                                        else
                                                        {
                                                            if (PTTString.Length > 5)
                                                            {
                                                                System.Windows.MessageBox.Show("Polje PTT ne može sadržati više od 5 karaktera.");
                                                            }
                                                            else
                                                            {
                                                                if (PTTString.Length < 5)
                                                                {
                                                                    System.Windows.MessageBox.Show("Polje PTT ne može sadržati manje od 5 karaktera.");
                                                                }
                                                                else
                                                                {
                                                                    PTT = Convert.ToInt32(PTTString);

                                                                    if (adresaStanovanja == null || adresaStanovanja == "")
                                                                    {
                                                                        System.Windows.MessageBox.Show("Polje adresa stanovanja ne sme ostati prazno.");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (adresaStanovanja.Length > 80)
                                                                        {
                                                                            System.Windows.MessageBox.Show("Polje adresa stanovanja ne može sadržati više od 80 karaktera.");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (kontaktTelefon == null || kontaktTelefon == "")
                                                                            {
                                                                                System.Windows.MessageBox.Show("Polje kontakt telefon ne sme ostati prazno.");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (kontaktTelefon.Length > 20)
                                                                                {
                                                                                    System.Windows.MessageBox.Show("Polje kontakt telefon ne može sadržati više od 20 karaktera.");
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (plataString == null || plataString == "")
                                                                                    {
                                                                                        System.Windows.MessageBox.Show("Polje plata ne sme ostati prazno.");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        string sablon = "^\\d+(,\\d+)?$";
                                                                                        if (Regex.IsMatch(plataString, sablon) == false)
                                                                                        {
                                                                                            System.Windows.MessageBox.Show("Plata nije odgovarajućeg oblika.");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            plata = Convert.ToDouble(plataString);

                                                                                            SqlConnection connection = new SqlConnection();
                                                                                            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                                                                            connection.Open();

                                                                                            SqlCommand komanda_IzmeniRadnika = new SqlCommand();
                                                                                            komanda_IzmeniRadnika.CommandText = "SELECT idOsobe FROM OSOBA WHERE idOsobe = @idOsobe";
                                                                                            komanda_IzmeniRadnika.Parameters.AddWithValue("@idOsobe", idOsobe);
                                                                                            komanda_IzmeniRadnika.Connection = connection;

                                                                                            SqlDataReader citac_UzmiIdOsobe = komanda_IzmeniRadnika.ExecuteReader();

                                                                                            if (citac_UzmiIdOsobe.Read() == false)
                                                                                            {
                                                                                                citac_UzmiIdOsobe.Close();
                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju.");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                int proveri_IdOsobe = citac_UzmiIdOsobe.GetInt32(0);
                                                                                                citac_UzmiIdOsobe.Close();

                                                                                                if (idOsobe == proveri_IdOsobe)
                                                                                                {
                                                                                                    komanda_IzmeniRadnika.CommandText = "SELECT idRadnika FROM RADNIK WHERE idRadnika = @idRadnika";
                                                                                                    komanda_IzmeniRadnika.Parameters.AddWithValue("@idRadnika", idRadnika);
                                                                                                    komanda_IzmeniRadnika.Connection = connection;

                                                                                                    SqlDataReader citac_UzmiIdRadnika = komanda_IzmeniRadnika.ExecuteReader();

                                                                                                    if (citac_UzmiIdRadnika.Read() == false)
                                                                                                    {
                                                                                                        citac_UzmiIdRadnika.Close();
                                                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju.");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        int proveri_IdRadnika = citac_UzmiIdRadnika.GetInt32(0);
                                                                                                        citac_UzmiIdRadnika.Close();

                                                                                                        if (idRadnika == proveri_IdRadnika)
                                                                                                        {
                                                                                                            komanda_IzmeniRadnika.CommandText = "UPDATE OSOBA SET ime = @ime, prezime = @prezime WHERE idOsobe = @idOsobe";
                                                                                                            komanda_IzmeniRadnika.Parameters.AddWithValue("@ime", ime);
                                                                                                            komanda_IzmeniRadnika.Parameters.AddWithValue("@prezime", prezime);
                                                                                                            komanda_IzmeniRadnika.Connection = connection;

                                                                                                            int proveri1 = komanda_IzmeniRadnika.ExecuteNonQuery();
                                                                                                            if (proveri1 == 1)
                                                                                                            {
                                                                                                                komanda_IzmeniRadnika.CommandText = "UPDATE RADNIK SET radnaPozicija = @radnaPozicija, JMBG = @JMBG, PTT = @PTT, adresaStanovanja = @adresaStanovanja, kontaktTelefon = @kontaktTelefon, plata = @plata WHERE idOsobe = @idOsobe and idRadnika = @idRadnika";
                                                                                                                komanda_IzmeniRadnika.Parameters.AddWithValue("@radnaPozicija", radnaPozicija);
                                                                                                                komanda_IzmeniRadnika.Parameters.AddWithValue("@JMBG", JMBG);
                                                                                                                komanda_IzmeniRadnika.Parameters.AddWithValue("@PTT", PTT);
                                                                                                                komanda_IzmeniRadnika.Parameters.AddWithValue("@adresaStanovanja", adresaStanovanja);
                                                                                                                komanda_IzmeniRadnika.Parameters.AddWithValue("@kontaktTelefon", kontaktTelefon);
                                                                                                                komanda_IzmeniRadnika.Parameters.AddWithValue("@plata", plata);
                                                                                                                komanda_IzmeniRadnika.Connection = connection;

                                                                                                                int proveri2 = komanda_IzmeniRadnika.ExecuteNonQuery();
                                                                                                                if (proveri2 == 1)
                                                                                                                {
                                                                                                                    osveziPolja();
                                                                                                                    dataGrid_Radnik();
                                                                                                                    System.Windows.MessageBox.Show("Podaci o radniku su uspešno izmenjeni.");
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    System.Windows.MessageBox.Show("Došlo je do greške pri izmeni radnika.");
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri izmeni imena i prezimena radnika.");
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            System.Windows.MessageBox.Show("Došlo je do greške kod ID-a radnika.");
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    System.Windows.MessageBox.Show("Došlo je do greške kod ID-a osobe.");
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

        private void klik_IzbrisiRadnika(object sender, RoutedEventArgs e)
        {
            int idRadnika;
            int idOsobe;
            int PTT;
            double plata;

            string idRadnikaString = textbox_IdRadnikaAdmin.Text;
            string idOsobeString = textbox_IdOsobeRadnikAdmin.Text;
            string plataString = textbox_PlataRadnikaAdmin.Text;

            string ime = textbox_ImeRadnikaAdmin.Text;
            string prezime = textbox_PrezimeRadnikaAdmin.Text;
            string radnaPozicija = textbox_RadnaPozicijaRadnikaAdmin.Text;
            string JMBG = textbox_JMBGRadnikaAdmin.Text;
            string PTTString = textbox_PTTRadnikaAdmin.Text;
            string adresaStanovanja = textbox_AdresaStanovanjaRadnikaAdmin.Text;
            string kontaktTelefon = textbox_KontaktTelefonRadnikaAdmin.Text;

            if (idRadnikaString == "")
            {
                System.Windows.MessageBox.Show("Polje ID radnika ne sme biti prazno, ponovo odaberite radnika iz tabele.");
            }
            else
            {
                idRadnika = Convert.ToInt32(idRadnikaString);

                if(idOsobeString == "")
                {
                    System.Windows.MessageBox.Show("Polje ID osobe ne sme biti prazno, ponovo odaberite radnika iz tabele.");
                }
                else
                {
                    idOsobe = Convert.ToInt32(idOsobeString);

                    if (ime == null || ime == "")
                    {
                        System.Windows.MessageBox.Show("Polje ime ne sme ostati prazno.");
                    }
                    else
                    {
                        if (prezime == null || prezime == "")
                        {
                            System.Windows.MessageBox.Show("Polje prezime ne sme ostati prazno.");
                        }
                        else
                        {
                            if (radnaPozicija == null || radnaPozicija == "")
                            {
                                System.Windows.MessageBox.Show("Polje radna pozicija ne sme ostati prazno.");
                            }
                            else
                            {
                                if (radnaPozicija.Length > 40)
                                {
                                    System.Windows.MessageBox.Show("Polje radna pozicija ne može sadržati više od 40 karaktera.");
                                }
                                else
                                {
                                    if (JMBG == null || JMBG == "")
                                    {
                                        System.Windows.MessageBox.Show("Polje JMBG ne sme ostati prazno.");
                                    }
                                    else
                                    {
                                        if (JMBG.Length > 13)
                                        {
                                            System.Windows.MessageBox.Show("Polje JMBG ne može sadržati više od 13 karaktera.");
                                        }
                                        else
                                        {
                                            if (JMBG.Length < 13)
                                            {
                                                System.Windows.MessageBox.Show("Polje JMBG ne može sadržati manje od 13 karaktera.");
                                            }
                                            else
                                            {
                                                if (PTTString == null || PTTString == "")
                                                {
                                                    System.Windows.MessageBox.Show("Polje PTT ne sme ostati prazno.");
                                                }
                                                else
                                                {
                                                    if (PTTString.Length > 5)
                                                    {
                                                        System.Windows.MessageBox.Show("Polje PTT ne može sadržati više od 5 karaktera.");
                                                    }
                                                    else
                                                    {
                                                        if (PTTString.Length < 5)
                                                        {
                                                            System.Windows.MessageBox.Show("Polje PTT ne može sadržati manje od 5 karaktera.");
                                                        }
                                                        else
                                                        {
                                                            PTT = Convert.ToInt32(PTTString);

                                                            if (adresaStanovanja == null || adresaStanovanja == "")
                                                            {
                                                                System.Windows.MessageBox.Show("Polje adresa stanovanja ne sme ostati prazno.");
                                                            }
                                                            else
                                                            {
                                                                if (adresaStanovanja.Length > 80)
                                                                {
                                                                    System.Windows.MessageBox.Show("Polje adresa stanovanja ne može sadržati više od 80 karaktera.");
                                                                }
                                                                else
                                                                {
                                                                    if (kontaktTelefon == null || kontaktTelefon == "")
                                                                    {
                                                                        System.Windows.MessageBox.Show("Polje kontakt telefon ne sme ostati prazno.");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (kontaktTelefon.Length > 20)
                                                                        {
                                                                            System.Windows.MessageBox.Show("Polje kontakt telefon ne može sadržati više od 20 karaktera.");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (plataString == null || plataString == "")
                                                                            {
                                                                                System.Windows.MessageBox.Show("Polje plata ne sme ostati prazno.");
                                                                            }
                                                                            else
                                                                            {
                                                                                string sablon = "^\\d+(,\\d+)?$";
                                                                                if (Regex.IsMatch(plataString, sablon) == false)
                                                                                {
                                                                                    System.Windows.MessageBox.Show("Plata nije odgovarajućeg oblika.");
                                                                                }
                                                                                else
                                                                                {
                                                                                    plata = Convert.ToDouble(plataString);

                                                                                    SqlConnection connection = new SqlConnection();
                                                                                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                                                                    connection.Open();

                                                                                    SqlCommand komanda_IzbrisiRadnika = new SqlCommand();
                                                                                    komanda_IzbrisiRadnika.CommandText = "SELECT idOsobe FROM RADNIK WHERE idOsobe = @idOsobe";
                                                                                    komanda_IzbrisiRadnika.Parameters.AddWithValue("@idOsobe", idOsobe);
                                                                                    komanda_IzbrisiRadnika.Connection = connection;

                                                                                    SqlDataReader citac_UzmiIdOsobe = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                    if (citac_UzmiIdOsobe.Read() == false)
                                                                                    {
                                                                                        citac_UzmiIdOsobe.Close();
                                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju ID-a osobe.");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        int proveri_IdOsobe = citac_UzmiIdOsobe.GetInt32(0);
                                                                                        citac_UzmiIdOsobe.Close();

                                                                                        if(idOsobe == proveri_IdOsobe)
                                                                                        {
                                                                                            komanda_IzbrisiRadnika.CommandText = "SELECT idRadnika FROM RADNIK WHERE idRadnika = @idRadnika";
                                                                                            komanda_IzbrisiRadnika.Parameters.AddWithValue("@idRadnika", idRadnika);
                                                                                            komanda_IzbrisiRadnika.Connection = connection;

                                                                                            SqlDataReader citac_UzmiIdRadnika = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                            if (citac_UzmiIdRadnika.Read() == false)
                                                                                            {
                                                                                                citac_UzmiIdRadnika.Close();
                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju ID-a radnika.");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                int proveri_idRadnika = citac_UzmiIdRadnika.GetInt32(0);
                                                                                                citac_UzmiIdRadnika .Close();

                                                                                                if(idRadnika == proveri_idRadnika)
                                                                                                {
                                                                                                    komanda_IzbrisiRadnika.CommandText = "SELECT radnaPozicija FROM RADNIK WHERE idRadnika = @idRadnika and idOsobe = @idOsobe";
                                                                                                    komanda_IzbrisiRadnika.Connection = connection;

                                                                                                    SqlDataReader citac_UzmiRadnuPozicijuRadnika = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                                    if (citac_UzmiRadnuPozicijuRadnika.Read() == false)
                                                                                                    {
                                                                                                        citac_UzmiRadnuPozicijuRadnika.Close();
                                                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju radne pozicije.");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        string proveri_PozicijuRadnika = citac_UzmiRadnuPozicijuRadnika.GetString(0);
                                                                                                        citac_UzmiRadnuPozicijuRadnika.Close();

                                                                                                        if(radnaPozicija == proveri_PozicijuRadnika)
                                                                                                        {
                                                                                                            komanda_IzbrisiRadnika.CommandText = "SELECT JMBG FROM RADNIK WHERE idRadnika = @idRadnika and idOsobe = @idOsobe";
                                                                                                            komanda_IzbrisiRadnika.Connection = connection;

                                                                                                            SqlDataReader citac_JMBGRadnika = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                                            if (citac_JMBGRadnika.Read() == false)
                                                                                                            {
                                                                                                                citac_JMBGRadnika.Close();
                                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju JMBG-a.");
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                string proveri_JMBG = citac_JMBGRadnika.GetString(0);
                                                                                                                citac_JMBGRadnika.Close();

                                                                                                                if(JMBG == proveri_JMBG)
                                                                                                                {
                                                                                                                    komanda_IzbrisiRadnika.CommandText = "SELECT PTT FROM RADNIK WHERE idRadnika = @idRadnika and idOsobe = @idOsobe";
                                                                                                                    komanda_IzbrisiRadnika.Connection = connection;

                                                                                                                    SqlDataReader citac_PTTRadnika = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                                                    if (citac_PTTRadnika.Read() == false)
                                                                                                                    {
                                                                                                                        citac_PTTRadnika.Close();
                                                                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju PTT-a.");
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        int proveri_PTT = citac_PTTRadnika.GetInt32(0);
                                                                                                                        citac_PTTRadnika.Close();

                                                                                                                        if(PTT == proveri_PTT)
                                                                                                                        {
                                                                                                                            komanda_IzbrisiRadnika.CommandText = "SELECT adresaStanovanja FROM RADNIK WHERE idRadnika = @idRadnika and idOsobe = @idOsobe";
                                                                                                                            komanda_IzbrisiRadnika.Connection = connection;

                                                                                                                            SqlDataReader citac_AdresaStanovanjaRadnika = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                                                            if (citac_AdresaStanovanjaRadnika.Read() == false)
                                                                                                                            {
                                                                                                                                citac_AdresaStanovanjaRadnika.Close();
                                                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju adrese stanovanja.");
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                string proveri_AdresuStanovanja = citac_AdresaStanovanjaRadnika.GetString(0);
                                                                                                                                citac_AdresaStanovanjaRadnika.Close();

                                                                                                                                if(adresaStanovanja == proveri_AdresuStanovanja)
                                                                                                                                {
                                                                                                                                    komanda_IzbrisiRadnika.CommandText = "SELECT kontaktTelefon FROM RADNIK WHERE idRadnika = @idRadnika and idOsobe = @idOsobe";
                                                                                                                                    komanda_IzbrisiRadnika.Connection = connection;

                                                                                                                                    SqlDataReader citac_KontaktTelefonRadnika = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                                                                    if (citac_KontaktTelefonRadnika.Read() == false)
                                                                                                                                    {
                                                                                                                                        citac_KontaktTelefonRadnika.Close();
                                                                                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju kontakt telefona.");
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        string proveri_KontaktTelefon = citac_KontaktTelefonRadnika.GetString(0);
                                                                                                                                        citac_KontaktTelefonRadnika.Close();

                                                                                                                                        if(kontaktTelefon == proveri_KontaktTelefon)
                                                                                                                                        {
                                                                                                                                            komanda_IzbrisiRadnika.CommandText = "SELECT plata FROM RADNIK WHERE idRadnika = @idRadnika and idOsobe = @idOsobe";
                                                                                                                                            komanda_IzbrisiRadnika.Connection = connection;

                                                                                                                                            SqlDataReader citac_PlataRadnika = komanda_IzbrisiRadnika.ExecuteReader();

                                                                                                                                            if (citac_PlataRadnika.Read() == false)
                                                                                                                                            {
                                                                                                                                                citac_PlataRadnika.Close();
                                                                                                                                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju kontakt telefona.");
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                decimal proveri_Platu = citac_PlataRadnika.GetDecimal(0);
                                                                                                                                                citac_PlataRadnika.Close();

                                                                                                                                                if(plata == (double)proveri_Platu)
                                                                                                                                                {
                                                                                                                                                    komanda_IzbrisiRadnika.CommandText = "DELETE FROM RADNIK WHERE idRadnika = @idRadnika and idOsobe = @idOsobe and radnaPozicija = @radnaPozicija and JMBG = @JMBG and PTT = @PTT and adresaStanovanja = @adresaStanovanja and kontaktTelefon = @kontaktTelefon and plata = @plata";
                                                                                                                                                    komanda_IzbrisiRadnika.Parameters.AddWithValue("@radnaPozicija", radnaPozicija);
                                                                                                                                                    komanda_IzbrisiRadnika.Parameters.AddWithValue("@JMBG", JMBG);
                                                                                                                                                    komanda_IzbrisiRadnika.Parameters.AddWithValue("@PTT", PTT);
                                                                                                                                                    komanda_IzbrisiRadnika.Parameters.AddWithValue("@adresaStanovanja", adresaStanovanja);
                                                                                                                                                    komanda_IzbrisiRadnika.Parameters.AddWithValue("@kontaktTelefon", kontaktTelefon);
                                                                                                                                                    komanda_IzbrisiRadnika.Parameters.AddWithValue("@plata", plata);
                                                                                                                                                    komanda_IzbrisiRadnika.Connection = connection;

                                                                                                                                                    int proveri = komanda_IzbrisiRadnika.ExecuteNonQuery();
                                                                                                                                                    if(proveri == 1)
                                                                                                                                                    {
                                                                                                                                                        osveziPolja();
                                                                                                                                                        dataGrid_Radnik();
                                                                                                                                                        System.Windows.MessageBox.Show("Radnik je uspešno izbrisan.");
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri brisanju.");
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    System.Windows.MessageBox.Show("Plata ne odgovara onoj koja se nalazi u bazi, ponovo odaberite radnika iz tabele.");
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            System.Windows.MessageBox.Show("Kontakt  telefon ne odgovara onom koji se nalazi u bazi, ponovo odaberite radnika iz tabele.");
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    System.Windows.MessageBox.Show("Adresa stanovanja ne odgovara onoj koja se nalazi u bazi, ponovo odaberite radnika iz tabele.");
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            System.Windows.MessageBox.Show("PTT ne odgovara onom koji se nalazi u bazi, ponovo odaberite radnika iz tabele.");
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    System.Windows.MessageBox.Show("JMBG ne odgovara onom koji se nalazi u bazi, ponovo odaberite radnika iz tabele.");
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            System.Windows.MessageBox.Show("Radna pozicija ne odgovara onoj koja se nalazi u bazi, ponovo odaberite radnika iz tabele.");
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    System.Windows.MessageBox.Show("Polje ID radnika ne odgovara ID-u koji se nalazi u bazi, ponovo odaberite radnika iz tabele.");
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            System.Windows.MessageBox.Show("Polje ID osobe ne odgovara ID-u koji se nalazi u bazi, ponovo odaberite radnika iz tabele.");
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
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void klik_OsveziPolja(object sender, RoutedEventArgs e)
        {
            osveziPolja();
        }

        private void klik_OsveziTabelu(object sender, RoutedEventArgs e)
        {
            dataGrid_Radnik();
        }

        void osveziPolja()
        {
            textbox_IdRadnikaAdmin.Text = "";
            textbox_ImeRadnikaAdmin.Text = "";
            textbox_PrezimeRadnikaAdmin.Text = "";
            textbox_RadnaPozicijaRadnikaAdmin.Text = "";
            textbox_JMBGRadnikaAdmin.Text = "";
            textbox_PTTRadnikaAdmin.Text = "";
            textbox_AdresaStanovanjaRadnikaAdmin.Text = "";
            textbox_KontaktTelefonRadnikaAdmin.Text = "";
            textbox_PlataRadnikaAdmin.Text = "";
            textbox_IdOsobeRadnikAdmin.Text = "";
        }

        void dataGrid_Radnik()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT RADNIK.idRadnika, OSOBA.ime, OSOBA.prezime, RADNIK.radnaPozicija, RADNIK.JMBG, RADNIK.PTT, RADNIK.adresaStanovanja, RADNIK.kontaktTelefon, RADNIK.plata, RADNIK.idOsobe FROM RADNIK INNER JOIN OSOBA on RADNIK.idOsobe = OSOBA.idOsobe";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("RADNIK");
            dataAdapter.Fill(dataTable);
            datagrid_Radnici.ItemsSource = dataTable.DefaultView;
        }

        private void radnici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdOsobeRadnikAdmin.Text = dr["idOsobe"].ToString();
                textbox_IdRadnikaAdmin.Text = dr["idRadnika"].ToString();
                textbox_RadnaPozicijaRadnikaAdmin.Text = dr["radnaPozicija"].ToString();
                textbox_JMBGRadnikaAdmin.Text = dr["JMBG"].ToString();
                textbox_PTTRadnikaAdmin.Text = dr["PTT"].ToString();
                textbox_AdresaStanovanjaRadnikaAdmin.Text = dr["adresaStanovanja"].ToString();
                textbox_KontaktTelefonRadnikaAdmin.Text = dr["kontaktTelefon"].ToString();
                textbox_PlataRadnikaAdmin.Text = dr["plata"].ToString();
                textbox_ImeRadnikaAdmin.Text = dr["ime"].ToString();
                textbox_PrezimeRadnikaAdmin.Text = dr["prezime"].ToString();
            }
        }
    }
}
