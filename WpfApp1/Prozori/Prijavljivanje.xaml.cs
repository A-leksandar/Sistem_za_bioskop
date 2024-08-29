using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for Prijavljivanje.xaml
    /// </summary>
    public partial class Prijavljivanje : Window
    {
        public Prijavljivanje()
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

        private void klik_PrijaviSe(object sender, RoutedEventArgs e)
        {
            string email = textboxPrijavljivanjeEmail.Text;
            string lozinka = textboxPrijavljivanjeLozinka.Text;

            string sablon = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if(email == "admin" && lozinka == "admin123")
            {
                Admin aD = new Admin();
                aD.Show();
                this.Close();
            }
            else
            {
                if (email != null)
                {
                    if (lozinka != null)
                    {
                        if (Regex.IsMatch(email, sablon) == true)
                        {
                            SqlConnection connection = new SqlConnection();
                            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                            connection.Open();

                            SqlCommand komanda_ProveriEmail = new SqlCommand();

                            komanda_ProveriEmail.CommandText = "SELECT email from [KORISNIK] GROUP BY email HAVING email = @email";//Selektovanje email-a na osnovu unesenog email-a
                            komanda_ProveriEmail.Parameters.AddWithValue("@email", email);
                            komanda_ProveriEmail.Connection = connection;

                            SqlDataReader citac_ProveriEmail = komanda_ProveriEmail.ExecuteReader();

                            string email_ZaProveru;

                            if (citac_ProveriEmail.Read() == false)
                            {
                                email_ZaProveru = null;
                            }
                            else
                            {
                                email_ZaProveru = citac_ProveriEmail.GetString(0);
                            }
                            citac_ProveriEmail.Close();

                            if (email == email_ZaProveru)
                            {
                                SqlCommand komanda_ProveriLozinku = new SqlCommand();
                                komanda_ProveriLozinku.CommandText = "SELECT lozinka from [KORISNIK] GROUP BY email, lozinka HAVING email = @email";
                                komanda_ProveriLozinku.Parameters.AddWithValue("@email", email);
                                komanda_ProveriLozinku.Connection = connection;

                                SqlDataReader citac_ProveriLozinku = komanda_ProveriLozinku.ExecuteReader();

                                if (citac_ProveriLozinku.Read() == false)
                                {
                                    System.Windows.MessageBox.Show("Doslo je greske pri ucitavanju sifre.");
                                }
                                else
                                {
                                    string lozinka_ZaProveru = citac_ProveriLozinku.GetString(0);

                                    if (lozinka == lozinka_ZaProveru)
                                    {
                                        osveziPolja();
                                        System.Windows.MessageBox.Show("Uspešno ste se prijavili.");
                                        Filmovi fL = new Filmovi(email);
                                        fL.Show();
                                        this.Close();
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Lozinka koju ste uneli nije tačna.");
                                    }
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Mejl koji ste uneli nije registrovan.");
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Email nije odgovarajućeg oblika.");
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Polje lozinka ne sme ostati prazno.");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Polje email ne sme ostati prazno.");
                }
            }
        }

        protected void osveziPolja()
        {
            textboxPrijavljivanjeEmail.Text = "";
            textboxPrijavljivanjeLozinka.Text = "";
        }
    }
}
