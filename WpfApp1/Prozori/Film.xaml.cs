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
using System.Windows.Shapes;

namespace WpfApp1.Prozori
{
    /// <summary>
    /// Interaction logic for Film.xaml
    /// </summary>
    public partial class Film : Window
    {
        int idFilma;
        string nazivFilma;
        string opisFilma;
        string email;
        double cena;

        public Film(int idFilma, string email_Korinsika)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.idFilma = idFilma;
            email = email_Korinsika;

            popuni_Film(idFilma);
            dataGrid_Film();
        }

        private void popuni_Film(int idFilma)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_PopuniFilm = new SqlCommand();

            komanda_PopuniFilm.CommandText = "SELECT nazivFilma FROM FILM WHERE idFilma = @idFilma";
            komanda_PopuniFilm.Parameters.AddWithValue("idFilma", idFilma);
            komanda_PopuniFilm.Connection = connection;

            SqlDataReader citac_UzmiNazivFilma = komanda_PopuniFilm.ExecuteReader();

            if(citac_UzmiNazivFilma.Read() == false)
            {
                citac_UzmiNazivFilma.Close();
                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju naziva filma.");
            }
            else
            {
                string ucitan_NazivFilma = citac_UzmiNazivFilma.GetString(0);
                citac_UzmiNazivFilma.Close();

                komanda_PopuniFilm.CommandText = "SELECT opisFilma FROM FILM WHERE idFilma = @idFilma";
                komanda_PopuniFilm.Connection = connection;

                SqlDataReader citac_UzmiOpisFilma = komanda_PopuniFilm.ExecuteReader();

                if(citac_UzmiOpisFilma.Read() == false)
                {
                    citac_UzmiOpisFilma.Close();
                    System.Windows.MessageBox.Show("Došlo je do greške pri čitanju opisa filma.");
                }
                else
                {
                    string ucitan_OpisFilma = citac_UzmiOpisFilma.GetString(0);
                    citac_UzmiOpisFilma.Close();

                    komanda_PopuniFilm.CommandText = "SELECT cena FROM FILM WHERE idFilma = @idFilma";
                    komanda_PopuniFilm.Connection = connection;

                    SqlDataReader citac_UzmiCenu = komanda_PopuniFilm.ExecuteReader();

                    if(citac_UzmiCenu.Read() == false)
                    {
                        citac_UzmiCenu.Close();
                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju cene.");
                    }
                    else
                    {
                        cena = Convert.ToDouble(citac_UzmiCenu.GetDecimal(0));
                        citac_UzmiCenu.Close();

                        nazivFilma = ucitan_NazivFilma;
                        opisFilma = ucitan_OpisFilma;

                        BitmapImage bitmapImage = null;

                        try
                        {
                            bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.UriSource = new Uri("Images/" + nazivFilma + ".png", UriKind.Relative);
                            bitmapImage.EndInit();

                            if (bitmapImage.PixelWidth == 0 || bitmapImage.PixelHeight == 0)
                            {
                                throw new Exception("Slika nije pronađena ili je prazna.");
                            }
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.UriSource = new Uri("Images/default.png", UriKind.Relative);
                                bitmapImage.EndInit();

                                if (bitmapImage.PixelWidth == 0 || bitmapImage.PixelHeight == 0)
                                {
                                    throw new Exception("Podrazumevana slika nije pronađena ili je prazna.");
                                }
                            }
                            catch (Exception defaultEx)
                            {
                                bitmapImage = null;
                            }
                        }

                        Image slika = new Image
                        {
                            Source = bitmapImage,
                            Width = 250,
                            Height = 387,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Stretch = Stretch.Fill
                        };

                        Grid.SetColumn(slika, 1);

                        grid_0.Children.Add(slika);
                    }
                }
            }
        }

        private void dataGrid_Film()
        {
            labela_NazivFilma.Content = nazivFilma;
            labela_OpisFilma.Content = opisFilma;
            labela_CenaFilma.Content = "Cena karte: " + Convert.ToString(cena);

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_UzmiGlumca = new SqlCommand();
            komanda_UzmiGlumca.CommandText = "SELECT GLUMI.idGlumca, OSOBA.ime + ' ' + OSOBA.prezime as 'GLUMAC', GLUMI.uloga FROM GLUMI INNER JOIN GLUMAC on GLUMI.idGlumca = GLUMAC.idGlumca INNER JOIN OSOBA on GLUMAC.idOsobe = OSOBA.idOsobe WHERE idFilma = @idFilma";
            komanda_UzmiGlumca.Parameters.AddWithValue("@idFilma", idFilma);
            komanda_UzmiGlumca.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda_UzmiGlumca);

            DataTable dataTable = new DataTable("GLUMCI I ULOGE");
            dataAdapter.Fill(dataTable);
            datagrid_GlumciIUloge.ItemsSource = dataTable.DefaultView;
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Filmovi fM = new Filmovi(email);
            fM.Show();
            this.Close();
        }

        private void klik_RezervisiFilm(object sender, RoutedEventArgs e)
        {
            Rezervacija rZ = new Rezervacija(idFilma, email);
            rZ.Show();
            this.Close();
        }
    }
}
