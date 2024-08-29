using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Interaction logic for Registrovanje.xaml
    /// </summary>
    public partial class Registrovanje : Window
    {

        protected string email;
        protected string ime;
        protected string prezime;
        protected string lozinka;
        protected string potvrdi_Lozinku;

        protected int idOsobe;
        public Registrovanje()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mW = new MainWindow();
            mW.Show();
            this.Close();
        }

        private void klik_RegistrujSe(object sender, RoutedEventArgs e)
        {
            email = textbox_EmailRegistrovanje.Text;
            ime = textbox_ImeRegistrovanje.Text;
            prezime = textbox_PrezimeRegistrovanje.Text;
            lozinka = textbox_LozinkaRegistrovanje.Text;
            potvrdi_Lozinku = textbox_PotvrdaLozinkeRegistrovanje.Text;


            bool proveriIntIme = Regex.IsMatch(ime, @"^[a-zA-Z]+$");
            bool proveriPrezime = Regex.IsMatch(prezime, @"^[a-zA-Z]+$");

            string sablon = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; //REGEX provera da li je email ispravnog oblika

            //---------------------------------------------------------------PROVERA I UNOS PODATAKA------------------------------------------------------------------------------------------------
            if (email == null || email == "")
            {
                System.Windows.MessageBox.Show("Polje Email ne sme ostati prazno.");
            }
            else
            {
                if(ime == null || ime == "")
                {
                    System.Windows.MessageBox.Show("Polje ime ne sme ostati prazno.");
                }
                else
                {
                    if(prezime == null || prezime == "")
                    {
                        System.Windows.MessageBox.Show("Polje prezime ne sme ostati prazno.");
                    }
                    else
                    {
                        if(lozinka == null || potvrdi_Lozinku == "")
                        {
                            System.Windows.MessageBox.Show("Polje lozinka ne sme ostati prazno.");
                        }
                        else
                        {
                            if(potvrdi_Lozinku == null || potvrdi_Lozinku == "")
                            {
                                System.Windows.MessageBox.Show("Polje za potvrdu lozinke ne sme ostati prazno.");
                            }
                            else
                            {
                                //PROVERA DA LI SE EMAIL VEC KORISTI------------------------------------------------------------------------------------------------------
                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                connection.Open();

                                SqlCommand komanda_ProveriEmail = new SqlCommand();

                                komanda_ProveriEmail.CommandText = "SELECT email from [KORISNIK] GROUP BY email HAVING email = @email";//Selektovanje email-a na osnovu unesenog email-a
                                komanda_ProveriEmail.Parameters.AddWithValue("@email", email);
                                komanda_ProveriEmail.Connection = connection;

                                SqlDataReader citac_ProveriEmail = komanda_ProveriEmail.ExecuteReader();

                                string email_ZaProveru;

                                if(citac_ProveriEmail.Read() == false)
                                {
                                    email_ZaProveru = null;
                                }
                                else
                                {
                                    email_ZaProveru = citac_ProveriEmail.GetString(0);
                                }
                                citac_ProveriEmail.Close();

                                if (email == email_ZaProveru)//Provera
                                {
                                    //Mejl se vec koristi
                                    System.Windows.MessageBox.Show("Mejl koji ste uneli se vec koristi.");
                                }
                                else
                                {//------------------------------------------------------------PROVERA O ISPRAVNOSTI UNETIH PODATAKA------------------------------------------------------------
                                    if (potvrdi_Lozinku == lozinka)
                                    {
                                        if (Regex.IsMatch(email, sablon) == true)
                                        {
                                            if (proveriIntIme == false)
                                            {
                                                System.Windows.MessageBox.Show("Ime ne može sadrzati ništa osim slova.");
                                            }
                                            else
                                            {
                                                if(proveriPrezime == false)
                                                {
                                                    System.Windows.MessageBox.Show("Prezime ne može sadrzati ništa osim slova.");
                                                }
                                                else
                                                {
                                                    if (potvrdi_Lozinku == lozinka)
                                                    {
                                                        if (Regex.IsMatch(email, sablon) == true)
                                                        {
                                                            if (proveriIntIme == false)
                                                            {
                                                                System.Windows.MessageBox.Show("Ime ne može sadrzati ništa osim slova.");
                                                            }
                                                            else if (proveriPrezime == false)
                                                            {
                                                                System.Windows.MessageBox.Show("Prezime ne može sadrzati ništa osim slova.");
                                                            }
                                                            else
                                                            {
                                                                SqlCommand komanda = new SqlCommand();
                                                                komanda.CommandText = "INSERT INTO [OSOBA] (ime, prezime) VALUES (@ime, @prezime); SELECT SCOPE_IDENTITY();";
                                                                komanda.Parameters.AddWithValue("@ime", ime);
                                                                komanda.Parameters.AddWithValue("@prezime", prezime);
                                                                komanda.Connection = connection;

                                                                int proveri1 = komanda.ExecuteNonQuery();
                                                                if (proveri1 == 1)
                                                                {
                                                                    komanda.CommandText = "SELECT TOP 1 idOsobe FROM [OSOBA] ORDER BY idOsobe DESC";
                                                                    SqlDataReader citac = komanda.ExecuteReader();

                                                                    if (citac.Read())
                                                                    {
                                                                        idOsobe = citac.GetInt32(0);
                                                                        citac.Close();
                                                                        komanda.CommandText = "INSERT INTO [KORISNIK] (email, lozinka, idOsobe) VALUES (@email, @lozinka, @idOsobe); SELECT SCOPE_IDENTITY();";
                                                                        komanda.Parameters.AddWithValue("@email", email);
                                                                        komanda.Parameters.AddWithValue("@lozinka", lozinka);
                                                                        komanda.Parameters.AddWithValue("@idOsobe", idOsobe);
                                                                        komanda.Connection = connection;
                                                                    }
                                                                    else
                                                                    {
                                                                        citac.Close();
                                                                        MessageBox.Show("NIJE UCITAO.");
                                                                    }

                                                                    int proveri2 = komanda.ExecuteNonQuery();
                                                                    if (proveri2 == 1)
                                                                    {
                                                                        osveziPolja();
                                                                        MessageBox.Show("Uspesno ste se registrovali.", "Uspesna registracija", MessageBoxButton.OK);
                                                                        Prijavljivanje pR = new Prijavljivanje();
                                                                        pR.Show();
                                                                        this.Close();
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("GRESKA.");
                                                                    }
                                                                    osveziPolja();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Windows.MessageBox.Show("Email nije ispravnog oblika.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Windows.MessageBox.Show("Lozinke koje ste uneli se ne poklapaju.");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            System.Windows.MessageBox.Show("Email nije ispravnog oblika.");
                                        }
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Lozinke koje ste uneli se ne poklapaju.");
                                    }
                                }//-------------------------------------------------------------------------------------------------------------------------------------------------------------
                            }
                        }
                    }
                }
            }//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        }//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        protected void osveziPolja()
        {
            textbox_EmailRegistrovanje.Text = "";
            textbox_ImeRegistrovanje.Text = "";
            textbox_PrezimeRegistrovanje.Text = "";
            textbox_LozinkaRegistrovanje.Text = "";
            textbox_PotvrdaLozinkeRegistrovanje.Text = "";
        }
    }
}
