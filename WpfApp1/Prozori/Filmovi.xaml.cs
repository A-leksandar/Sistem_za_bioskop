using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for Filmovi.xaml
    /// </summary>
    public partial class Filmovi : Window//-----------------------------------------------------------------------------
    {
        string email;
        public Filmovi(string login_Email)
        {
            InitializeComponent();

            email = login_Email;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_PrebrojFilmove = new SqlCommand();
            komanda_PrebrojFilmove.CommandText = "SELECT count(idFilma) from [FILM]";
            komanda_PrebrojFilmove.Connection = connection;

            int broj_Filmova = (int)komanda_PrebrojFilmove.ExecuteScalar();

            int brojRedova = (broj_Filmova / 4);

            if(broj_Filmova % 4 != 0)
            {
                brojRedova += 1;
            }

            for (int i = 0; i < brojRedova; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(200);
                grid_Filmovi.RowDefinitions.Add(rowDefinition);
            }

            int l = 0 ,j = -1, k = -1, m=1;

            while ( l != broj_Filmova)
            {
                SqlCommand komanda_ProveriIdFilma = new SqlCommand();
                komanda_ProveriIdFilma.CommandText = "SELECT idFilma from FILM where idFilma = @idFilma";
                komanda_ProveriIdFilma.Parameters.AddWithValue("@idFilma", m);
                komanda_ProveriIdFilma.Connection = connection;

                SqlDataReader citac_ProveriIdFilma = komanda_ProveriIdFilma.ExecuteReader();

                int proveri_IdFilma;
                if(citac_ProveriIdFilma.Read() == false)
                {
                    m++;
                    citac_ProveriIdFilma.Close();
                }
                else
                {
                    proveri_IdFilma = citac_ProveriIdFilma.GetInt32(0);
                    citac_ProveriIdFilma.Close();

                    if (m != proveri_IdFilma)
                    {
                        m++;
                        System.Windows.MessageBox.Show("Došlo je do greške sa ID-evima.");
                    }
                    else
                    {
                        SqlCommand komanda_UzmiNazivFilma = new SqlCommand();
                        komanda_UzmiNazivFilma.CommandText = "SELECT nazivFilma from FILM WHERE idFilma = @idFilma";
                        komanda_UzmiNazivFilma.Parameters.AddWithValue("@idFilma", m);
                        komanda_UzmiNazivFilma.Connection = connection;

                        SqlDataReader citac_UzmiNazivFilma = komanda_UzmiNazivFilma.ExecuteReader();

                        if (citac_UzmiNazivFilma.Read() == false)
                        {
                            m++;
                            citac_UzmiNazivFilma.Close();
                            break;
                        }
                        else
                        {
                            m++;
                            j++;
                            l++;
                            string nazivFilma = citac_UzmiNazivFilma.GetString(0);
                            byte[] bytes = Encoding.Unicode.GetBytes(nazivFilma);
                            nazivFilma = Encoding.Unicode.GetString(bytes);
                            citac_UzmiNazivFilma.Close();

                            Button dugme = new Button
                            {
                                Content = nazivFilma,
                                Name = "dugme_Film" + (j + 1),
                                Height = 20,
                                FontSize = 11,
                                FontFamily = new FontFamily("Segoe UI"),
                                Margin = new Thickness(5),
                                Background = new SolidColorBrush(Color.FromArgb(96, 0, 0, 0)),
                                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                                VerticalAlignment = VerticalAlignment.Bottom,
                                BorderBrush = Brushes.Transparent
                            };

                            int id = m;

                            dugme.Click += (sender, e) => klik_Film(sender, e, id-1, email);

                            if( (j%4) == 0)
                            {
                                k++;
                            }

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
                                Width = 100,
                                Height = 150,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Stretch = Stretch.Fill
                            };

                            Grid.SetRow(dugme, k);
                            Grid.SetColumn(dugme, j % 4);

                            Grid.SetRow(slika, k);
                            Grid.SetColumn(slika, j % 4);

                            grid_Filmovi.Children.Add(slika);
                            grid_Filmovi.Children.Add(dugme);
                        }
                    }
                }
            }
            connection.Close();
        }//-------------------------------------------------------------------------------------

        private void klik_Film(object sender, RoutedEventArgs e, int idFilma, string email_Korisnika)
        {
            Film fI = new Film(idFilma, email_Korisnika);
            fI.Show();
            this.Close();
        }


        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Prijavljivanje pR = new Prijavljivanje();
            pR.Show();
            this.Close();
        }

        private void klik_Karte(object sender, RoutedEventArgs e)
        {
            string login_email = email;
            Karte kT = new Karte(login_email);
            kT.Show();
            this.Close();
        }
    }
}
