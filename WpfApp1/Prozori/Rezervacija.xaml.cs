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
    /// Interaction logic for Rezervacija.xaml
    /// </summary>
    public partial class Rezervacija : Window
    {

        int idFilma_Rez;
        string email_Rez;
        int idSale;

        List<string> datumiProjekcijaString = new List<string>();
        List<DateTime> datumiProjekcijaDateTime = new List<DateTime>();

        public Rezervacija(int idFilma, string email)
        {
            InitializeComponent();

            idFilma_Rez = idFilma;
            email_Rez = email;

            ucitaj_DatumeProjekcije(idFilma_Rez);

            ucitaj_NazivFilma(idFilma);

            ImageBrush slika = new ImageBrush();
            slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Error.png", UriKind.Relative));

            Sediste_1.Click  += klik_Sediste_1 ;
            Sediste_1.Background = slika ;

            Sediste_2.Click  += klik_Sediste_2 ;
            Sediste_2.Background = slika ;

            Sediste_3.Click  += klik_Sediste_3 ;
            Sediste_3.Background = slika ;

            Sediste_4.Click  += klik_Sediste_4 ;
            Sediste_4.Background = slika ;

            Sediste_5.Click  += klik_Sediste_5 ;
            Sediste_5.Background = slika ;

            Sediste_6.Click  += klik_Sediste_6 ;
            Sediste_6.Background = slika ;

            Sediste_7.Click  += klik_Sediste_7 ;
            Sediste_7.Background = slika ;

            Sediste_8.Click  += klik_Sediste_8 ;
            Sediste_8.Background = slika ;

            Sediste_9.Click  += klik_Sediste_9 ;
            Sediste_9.Background = slika ;

            Sediste_10.Click += klik_Sediste_10;
            Sediste_10.Background = slika;

            Sediste_11.Click += klik_Sediste_11;
            Sediste_11.Background = slika;

            Sediste_12.Click += klik_Sediste_12;
            Sediste_12.Background = slika;

            Sediste_13.Click += klik_Sediste_13;
            Sediste_13.Background = slika;

            Sediste_14.Click += klik_Sediste_14;
            Sediste_14.Background = slika;

            Sediste_15.Click += klik_Sediste_15;
            Sediste_15.Background = slika;

            Sediste_16.Click += klik_Sediste_16;
            Sediste_16.Background = slika;

            Sediste_17.Click += klik_Sediste_17;
            Sediste_17.Background = slika;

            Sediste_18.Click += klik_Sediste_18;
            Sediste_18.Background = slika;

            Sediste_19.Click += klik_Sediste_19;
            Sediste_19.Background = slika;

            Sediste_20.Click += klik_Sediste_20;
            Sediste_20.Background = slika;

            Sediste_21.Click += klik_Sediste_21;
            Sediste_21.Background = slika;

            Sediste_22.Click += klik_Sediste_22;
            Sediste_22.Background = slika;

            Sediste_23.Click += klik_Sediste_23;
            Sediste_23.Background = slika;

            Sediste_24.Click += klik_Sediste_24;
            Sediste_24.Background = slika;

            Sediste_25.Click += klik_Sediste_25;
            Sediste_25.Background = slika;

            Sediste_26.Click += klik_Sediste_26;
            Sediste_26.Background = slika;

            Sediste_27.Click += klik_Sediste_27;
            Sediste_27.Background = slika;

            Sediste_28.Click += klik_Sediste_28;
            Sediste_28.Background = slika;

            Sediste_29.Click += klik_Sediste_29;
            Sediste_29.Background = slika;

            Sediste_30.Click += klik_Sediste_30;
            Sediste_30.Background = slika;

            Sediste_31.Click += klik_Sediste_31;
            Sediste_31.Background = slika;

            Sediste_32.Click += klik_Sediste_32;
            Sediste_32.Background = slika;

            Sediste_33.Click += klik_Sediste_33;
            Sediste_33.Background = slika;

            Sediste_34.Click += klik_Sediste_34;
            Sediste_34.Background = slika;

            Sediste_35.Click += klik_Sediste_35;
            Sediste_35.Background = slika;

            Sediste_36.Click += klik_Sediste_36;
            Sediste_36.Background = slika;

            Sediste_37.Click += klik_Sediste_37;
            Sediste_37.Background = slika;

            Sediste_38.Click += klik_Sediste_38;
            Sediste_38.Background = slika;

            Sediste_39.Click += klik_Sediste_39;
            Sediste_39.Background = slika;

            Sediste_40.Click += klik_Sediste_40;
            Sediste_40.Background = slika;

            Sediste_41.Click += klik_Sediste_41;
            Sediste_41.Background = slika;

            Sediste_42.Click += klik_Sediste_42;
            Sediste_42.Background = slika;

            Sediste_43.Click += klik_Sediste_43;
            Sediste_43.Background = slika;

            Sediste_44.Click += klik_Sediste_44;
            Sediste_44.Background = slika;

            Sediste_45.Click += klik_Sediste_45;
            Sediste_45.Background = slika;

            Sediste_46.Click += klik_Sediste_46;
            Sediste_46.Background = slika;

            Sediste_47.Click += klik_Sediste_47;
            Sediste_47.Background = slika;

            Sediste_48.Click += klik_Sediste_48;
            Sediste_48.Background = slika;

            Sediste_49.Click += klik_Sediste_49;
            Sediste_49.Background = slika;

            Sediste_50.Click += klik_Sediste_50;
            Sediste_50.Background = slika;

            Sediste_51.Click += klik_Sediste_51;
            Sediste_51.Background = slika;

            Sediste_52.Click += klik_Sediste_52;
            Sediste_52.Background = slika;

            Sediste_53.Click += klik_Sediste_53;
            Sediste_53.Background = slika;

            Sediste_54.Click += klik_Sediste_54;
            Sediste_54.Background = slika;

            Sediste_55.Click += klik_Sediste_55;
            Sediste_55.Background = slika;

            Sediste_56.Click += klik_Sediste_56;
            Sediste_56.Background = slika;

            Sediste_57.Click += klik_Sediste_57;
            Sediste_57.Background = slika;

            Sediste_58.Click += klik_Sediste_58;
            Sediste_58.Background = slika;

            Sediste_59.Click += klik_Sediste_59;
            Sediste_59.Background = slika;

            Sediste_60.Click += klik_Sediste_60;
            Sediste_60.Background = slika;
        }

        void klik_Sediste_1(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "1";
        }

        void klik_Sediste_2(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "2";
        }

        void klik_Sediste_3(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "3";
        }

        void klik_Sediste_4(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "4";
        }

        void klik_Sediste_5(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "5";
        }

        void klik_Sediste_6(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "6";
        }

        void klik_Sediste_7(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "7";
        }

        void klik_Sediste_8(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "8";
        }

        void klik_Sediste_9(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "9";
        }

        void klik_Sediste_10(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "10";
        }

        void klik_Sediste_11(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "11";
        }

        void klik_Sediste_12(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "12";
        }

        void klik_Sediste_13(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "13";
        }

        void klik_Sediste_14(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "14";
        }

        void klik_Sediste_15(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "15";
        }

        void klik_Sediste_16(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "16";
        }

        void klik_Sediste_17(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "17";
        }

        void klik_Sediste_18(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "18";
        }

        void klik_Sediste_19(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "19";
        }

        void klik_Sediste_20(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "20";
        }

        void klik_Sediste_21(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "21";
        }

        void klik_Sediste_22(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "22";
        }

        void klik_Sediste_23(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "23";
        }

        void klik_Sediste_24(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "24";
        }

        void klik_Sediste_25(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "25";
        }

        void klik_Sediste_26(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "26";
        }

        void klik_Sediste_27(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "27";
        }

        void klik_Sediste_28(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "28";
        }

        void klik_Sediste_29(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "29";
        }
        void klik_Sediste_30(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "30";
        }

        void klik_Sediste_31(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "31";
        }

        void klik_Sediste_32(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "32";
        }

        void klik_Sediste_33(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "33";
        }

        void klik_Sediste_34(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "34";
        }

        void klik_Sediste_35(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "35";
        }

        void klik_Sediste_36(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "36";
        }

        void klik_Sediste_37(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "37";
        }

        void klik_Sediste_38(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "38";
        }

        void klik_Sediste_39(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "39";
        }

        void klik_Sediste_40(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "40";
        }

        void klik_Sediste_41(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "41";
        }

        void klik_Sediste_42(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "42";
        }

        void klik_Sediste_43(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "43";
        }

        void klik_Sediste_44(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "44";
        }

        void klik_Sediste_45(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "45";
        }

        void klik_Sediste_46(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "46";
        }

        void klik_Sediste_47(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "47";
        }

        void klik_Sediste_48(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "48";
        }

        void klik_Sediste_49(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "49";
        }

        void klik_Sediste_50(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "50";
        }

        void klik_Sediste_51(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "51";
        }

        void klik_Sediste_52(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "52";
        }

        void klik_Sediste_53(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "53";
        }

        void klik_Sediste_54(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "54";
        }

        void klik_Sediste_55(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "55";
        }

        void klik_Sediste_56(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "56";
        }

        void klik_Sediste_57(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "57";
        }

        void klik_Sediste_58(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "58";
        }

        void klik_Sediste_59(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "59";
        }

        void klik_Sediste_60(object sender, RoutedEventArgs e)
        {
            textbox_IdSedistaREADONLY.Text = "60";
        }

        void ucitaj_NazivFilma(int id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_UzmiNazivFilma = new SqlCommand();

            komanda_UzmiNazivFilma.CommandText = "SELECT nazivFilma FROM FILM WHERE idFilma = @idFilma";
            komanda_UzmiNazivFilma.Parameters.AddWithValue("@idFilma", id);
            komanda_UzmiNazivFilma.Connection = connection;

            SqlDataReader citac_UzmiNazivFilma = komanda_UzmiNazivFilma.ExecuteReader();

            if (citac_UzmiNazivFilma.Read() == false)
            {
                citac_UzmiNazivFilma.Close();
                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju naziva filma.");
            }
            else
            {
                labela_NazivFilma.Content = citac_UzmiNazivFilma.GetString(0);
                citac_UzmiNazivFilma.Close();
            }

            connection.Close();
        }

        void ucitaj_DatumeProjekcije(int id)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_UzmiDatumeProjekcija = new SqlCommand();

            komanda_UzmiDatumeProjekcija.CommandText = "SELECT datumProjekcije FROM PROJEKCIJA WHERE idFilma = @idFilma";
            komanda_UzmiDatumeProjekcija.Parameters.AddWithValue("@idFilma", id);
            komanda_UzmiDatumeProjekcija.Connection = connection;

            SqlDataReader citac_UzmiDatumeProjekcija = komanda_UzmiDatumeProjekcija.ExecuteReader();

            while(citac_UzmiDatumeProjekcija.Read())
            {
                if(!citac_UzmiDatumeProjekcija.IsDBNull(0))
                {
                    DateTime datum = citac_UzmiDatumeProjekcija.GetDateTime(0);

                    datumiProjekcijaDateTime.Add(citac_UzmiDatumeProjekcija.GetDateTime(0));

                    datumiProjekcijaString.Add(datum.ToString("yyyy.MM.dd HH:mm:ss"));
                }
                else
                {
                    System.Windows.MessageBox.Show("Nije učitano.");
                    break;
                }
            }
            citac_UzmiDatumeProjekcija.Close();

            connection.Close();

            combobox_DatumiProjekcije.ItemsSource = datumiProjekcijaString;
        }

        void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Film fM = new Film(idFilma_Rez, email_Rez);
            fM.Show();
            this.Close();
        }

        private void combobox_DatumiProjekcijeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            osveziSedista();
        }

        void osveziSedista()
        {
            textbox_DatumProjekcijeREADONLY.Text = datumiProjekcijaString[combobox_DatumiProjekcije.SelectedIndex];

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_UzmiIdSale = new SqlCommand();

            komanda_UzmiIdSale.CommandText = "SELECT idSale FROM PROJEKCIJA WHERE datumProjekcije = @datumProjekcije";
            komanda_UzmiIdSale.Parameters.AddWithValue("@datumProjekcije", datumiProjekcijaDateTime[combobox_DatumiProjekcije.SelectedIndex]);
            komanda_UzmiIdSale.Connection = connection;

            SqlDataReader citac_UzmiIdSale = komanda_UzmiIdSale.ExecuteReader();

            if (citac_UzmiIdSale.Read() == false)
            {
                citac_UzmiIdSale.Close();
                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju sale.");
            }
            else
            {
                idSale = citac_UzmiIdSale.GetInt32(0);
                citac_UzmiIdSale.Close();
                connection.Close();

                for (int i = 1; i <= 60; i++)
                {
                    SqlConnection konekcija = new SqlConnection();
                    konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                    konekcija.Open();

                    SqlCommand komanda_UzmiIdProjekcije = new SqlCommand();
                    komanda_UzmiIdProjekcije.CommandText = "SELECT idProjekcije FROM PROJEKCIJA WHERE datumProjekcije = @datumProjekcije";
                    komanda_UzmiIdProjekcije.Parameters.AddWithValue("@datumProjekcije", datumiProjekcijaDateTime[combobox_DatumiProjekcije.SelectedIndex]);
                    komanda_UzmiIdProjekcije.Connection = konekcija;

                    SqlDataReader citac_UzmiIdProjekcije = komanda_UzmiIdProjekcije.ExecuteReader();

                    if(citac_UzmiIdProjekcije.Read() == false)
                    {
                        citac_UzmiIdProjekcije.Close();
                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju projekcije.");
                    }
                    else
                    {
                        int idProjekcije = citac_UzmiIdProjekcije.GetInt32(0);
                        citac_UzmiIdProjekcije.Close();

                        SqlCommand komanda_ProveriZauzeto = new SqlCommand();

                        komanda_ProveriZauzeto.CommandText = "SELECT rezervisano FROM SE_NALAZI_U WHERE idSale = @idSale AND idSedista = @idSedista AND idProjekcije = @idProjekcije";
                        komanda_ProveriZauzeto.Parameters.AddWithValue("@idSedista", i);
                        komanda_ProveriZauzeto.Parameters.AddWithValue("@idSale", idSale);
                        komanda_ProveriZauzeto.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                        komanda_ProveriZauzeto.Connection = konekcija;

                        SqlDataReader citac_ProveriZauzeto = komanda_ProveriZauzeto.ExecuteReader();

                        if (citac_ProveriZauzeto.Read() == false)
                        {
                            citac_ProveriZauzeto.Close();
                            System.Windows.MessageBox.Show("Došlo je do greške pri čitanju zauzeća sedišta.");
                        }
                        else
                        {
                            string zauzeto = citac_ProveriZauzeto.GetString(0);
                            citac_ProveriZauzeto.Close();
                            konekcija.Close();

                            labela_BrojSale.Content = "Broj sale: SALA " + idSale;

                            ImageBrush pozadina_Slika = new ImageBrush();

                            if (zauzeto == "Ne")
                            {
                                pozadina_Slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Slobodno.png", UriKind.Relative));
                            }
                            else if (zauzeto == "Da")
                            {
                                pozadina_Slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Zauzeto.png", UriKind.Relative));
                            }
                            else
                            {
                                pozadina_Slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Error.png", UriKind.Relative));
                            }

                            switch (i)
                            {
                                case 1:
                                    Sediste_1.Background = pozadina_Slika;
                                    break;
                                case 2:
                                    Sediste_2.Background = pozadina_Slika;
                                    break;
                                case 3:
                                    Sediste_3.Background = pozadina_Slika;
                                    break;
                                case 4:
                                    Sediste_4.Background = pozadina_Slika;
                                    break;
                                case 5:
                                    Sediste_5.Background = pozadina_Slika;
                                    break;
                                case 6:
                                    Sediste_6.Background = pozadina_Slika;
                                    break;
                                case 7:
                                    Sediste_7.Background = pozadina_Slika;
                                    break;
                                case 8:
                                    Sediste_8.Background = pozadina_Slika;
                                    break;
                                case 9:
                                    Sediste_9.Background = pozadina_Slika;
                                    break;
                                case 10:
                                    Sediste_10.Background = pozadina_Slika;
                                    break;
                                case 11:
                                    Sediste_11.Background = pozadina_Slika;
                                    break;
                                case 12:
                                    Sediste_12.Background = pozadina_Slika;
                                    break;
                                case 13:
                                    Sediste_13.Background = pozadina_Slika;
                                    break;
                                case 14:
                                    Sediste_14.Background = pozadina_Slika;
                                    break;
                                case 15:
                                    Sediste_15.Background = pozadina_Slika;
                                    break;
                                case 16:
                                    Sediste_16.Background = pozadina_Slika;
                                    break;
                                case 17:
                                    Sediste_17.Background = pozadina_Slika;
                                    break;
                                case 18:
                                    Sediste_18.Background = pozadina_Slika;
                                    break;
                                case 19:
                                    Sediste_19.Background = pozadina_Slika;
                                    break;
                                case 20:
                                    Sediste_20.Background = pozadina_Slika;
                                    break;
                                case 21:
                                    Sediste_21.Background = pozadina_Slika;
                                    break;
                                case 22:
                                    Sediste_22.Background = pozadina_Slika;
                                    break;
                                case 23:
                                    Sediste_23.Background = pozadina_Slika;
                                    break;
                                case 24:
                                    Sediste_24.Background = pozadina_Slika;
                                    break;
                                case 25:
                                    Sediste_25.Background = pozadina_Slika;
                                    break;
                                case 26:
                                    Sediste_26.Background = pozadina_Slika;
                                    break;
                                case 27:
                                    Sediste_27.Background = pozadina_Slika;
                                    break;
                                case 28:
                                    Sediste_28.Background = pozadina_Slika;
                                    break;
                                case 29:
                                    Sediste_29.Background = pozadina_Slika;
                                    break;
                                case 30:
                                    Sediste_30.Background = pozadina_Slika;
                                    break;
                                case 31:
                                    Sediste_31.Background = pozadina_Slika;
                                    break;
                                case 32:
                                    Sediste_32.Background = pozadina_Slika;
                                    break;
                                case 33:
                                    Sediste_33.Background = pozadina_Slika;
                                    break;
                                case 34:
                                    Sediste_34.Background = pozadina_Slika;
                                    break;
                                case 35:
                                    Sediste_35.Background = pozadina_Slika;
                                    break;
                                case 36:
                                    Sediste_36.Background = pozadina_Slika;
                                    break;
                                case 37:
                                    Sediste_37.Background = pozadina_Slika;
                                    break;
                                case 38:
                                    Sediste_38.Background = pozadina_Slika;
                                    break;
                                case 39:
                                    Sediste_39.Background = pozadina_Slika;
                                    break;
                                case 40:
                                    Sediste_40.Background = pozadina_Slika;
                                    break;
                                case 41:
                                    Sediste_41.Background = pozadina_Slika;
                                    break;
                                case 42:
                                    Sediste_42.Background = pozadina_Slika;
                                    break;
                                case 43:
                                    Sediste_43.Background = pozadina_Slika;
                                    break;
                                case 44:
                                    Sediste_44.Background = pozadina_Slika;
                                    break;
                                case 45:
                                    Sediste_45.Background = pozadina_Slika;
                                    break;
                                case 46:
                                    Sediste_46.Background = pozadina_Slika;
                                    break;
                                case 47:
                                    Sediste_47.Background = pozadina_Slika;
                                    break;
                                case 48:
                                    Sediste_48.Background = pozadina_Slika;
                                    break;
                                case 49:
                                    Sediste_49.Background = pozadina_Slika;
                                    break;
                                case 50:
                                    Sediste_50.Background = pozadina_Slika;
                                    break;
                                case 51:
                                    Sediste_51.Background = pozadina_Slika;
                                    break;
                                case 52:
                                    Sediste_52.Background = pozadina_Slika;
                                    break;
                                case 53:
                                    Sediste_53.Background = pozadina_Slika;
                                    break;
                                case 54:
                                    Sediste_54.Background = pozadina_Slika;
                                    break;
                                case 55:
                                    Sediste_55.Background = pozadina_Slika;
                                    break;
                                case 56:
                                    Sediste_56.Background = pozadina_Slika;
                                    break;
                                case 57:
                                    Sediste_57.Background = pozadina_Slika;
                                    break;
                                case 58:
                                    Sediste_58.Background = pozadina_Slika;
                                    break;
                                case 59:
                                    Sediste_59.Background = pozadina_Slika;
                                    break;
                                case 60:
                                    Sediste_60.Background = pozadina_Slika;
                                    break;
                            }
                        }
                    }

                    konekcija.Close();
                }
            }
        }
        private void klik_RezervisiKartu(object sender, RoutedEventArgs e)
        {
            string brojKartice = textbox_BrojKarticeKorisnika.Text;
            string bezbednosniKod = textbox_BezbednosniKodKarticeKorisnika.Text;

            if(combobox_VrstaKartica.SelectedIndex == -1 || combobox_VrstaKartica.SelectedIndex == null)
            {
                System.Windows.MessageBox.Show("Obavezno je odabrati vrstu kartice.");
            }
            else
            {
                if (textbox_EmailKorisnika.Text == email_Rez)
                {
                    if (Regex.IsMatch(brojKartice, @"^\d{16}$") == false)
                    {
                        System.Windows.MessageBox.Show("Broj kartice mora sadržati 16 brojeva, i samo brojeva.");
                    }
                    else
                    {
                        if (Regex.IsMatch(bezbednosniKod, @"^\d{3,4}$") == false)
                        {
                            System.Windows.MessageBox.Show("Bezbednosni kod mora sadržati između 3 i 4 broja i samo broja.");
                        }
                        else
                        {
                            SqlConnection connection = new SqlConnection();
                            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                            connection.Open();

                            SqlCommand komanda_UzmiImeOsobe = new SqlCommand();

                            komanda_UzmiImeOsobe.CommandText = "SELECT OSOBA.ime FROM OSOBA INNER JOIN KORISNIK on OSOBA.idOsobe = KORISNIK.idOsobe WHERE KORISNIK.email = @email";
                            komanda_UzmiImeOsobe.Parameters.AddWithValue("@email", email_Rez);
                            komanda_UzmiImeOsobe.Connection = connection;

                            SqlDataReader citac_UzmiImeOsobe = komanda_UzmiImeOsobe.ExecuteReader();

                            if (citac_UzmiImeOsobe.Read() == false)
                            {
                                citac_UzmiImeOsobe.Close();
                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju imena.");
                            }
                            else
                            {
                                string ime_Proveri = citac_UzmiImeOsobe.GetString(0);
                                citac_UzmiImeOsobe.Close();

                                if (textbox_ImeKorisnika.Text == ime_Proveri)
                                {
                                    komanda_UzmiImeOsobe.CommandText = "SELECT OSOBA.prezime FROM OSOBA INNER JOIN KORISNIK on OSOBA.idOsobe = KORISNIK.idOsobe WHERE KORISNIK.email = @email";
                                    komanda_UzmiImeOsobe.Connection = connection;

                                    SqlDataReader citac_UzmiPrezimeOsobe = komanda_UzmiImeOsobe.ExecuteReader();

                                    if (citac_UzmiPrezimeOsobe.Read() == false)
                                    {
                                        citac_UzmiPrezimeOsobe.Close();
                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju prezimena.");
                                    }
                                    else
                                    {
                                        string prezime_Proveri = citac_UzmiPrezimeOsobe.GetString(0);
                                        citac_UzmiPrezimeOsobe.Close();

                                        if (textbox_PrezimeKorisnika.Text == prezime_Proveri)
                                        {
                                            SqlCommand komanda_UzmiIdProjekcije = new SqlCommand();
                                            komanda_UzmiIdProjekcije.CommandText = "SELECT idProjekcije FROM PROJEKCIJA WHERE datumProjekcije = @datumProjekcije";
                                            komanda_UzmiIdProjekcije.Parameters.AddWithValue("@datumProjekcije", datumiProjekcijaDateTime[combobox_DatumiProjekcije.SelectedIndex]);
                                            komanda_UzmiIdProjekcije.Connection = connection;

                                            SqlDataReader citac_UzmiIdProjekcije = komanda_UzmiIdProjekcije.ExecuteReader();

                                            if(citac_UzmiIdProjekcije.Read() == false)
                                            {
                                                citac_UzmiIdProjekcije.Close();
                                                System.Windows.MessageBox.Show("Došlo je do greške pri čitanju projekcije.");
                                            }
                                            else
                                            {
                                                int idProjekcije = citac_UzmiIdProjekcije.GetInt32(0);
                                                citac_UzmiIdProjekcije.Close();

                                                SqlCommand komanda_ProveriZauzeto = new SqlCommand();

                                                komanda_ProveriZauzeto.CommandText = "SELECT rezervisano FROM SE_NALAZI_U WHERE idSale = @idSale AND idSedista = @idSedista AND idProjekcije = @idProjekcije";
                                                komanda_ProveriZauzeto.Parameters.AddWithValue("@idSedista", Convert.ToInt32(textbox_IdSedistaREADONLY.Text));
                                                komanda_ProveriZauzeto.Parameters.AddWithValue("@idSale", idSale);
                                                komanda_ProveriZauzeto.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                                komanda_ProveriZauzeto.Connection = connection;

                                                SqlDataReader citac_ProveriZauzeto = komanda_ProveriZauzeto.ExecuteReader();

                                                if (citac_ProveriZauzeto.Read() == false)
                                                {
                                                    citac_ProveriZauzeto.Close();
                                                    System.Windows.MessageBox.Show("Došlo je do greške pri čitanju zauzeća sedišta.");
                                                }
                                                else
                                                {
                                                    string zauzeto_Proveri = citac_ProveriZauzeto.GetString(0);
                                                    citac_ProveriZauzeto.Close();

                                                    if (zauzeto_Proveri == "Da")
                                                    {
                                                        System.Windows.MessageBox.Show("Sedište koje ste odabrali je već zauzeto.");
                                                    }
                                                    else
                                                    {
                                                        int idSedista = Convert.ToInt32(textbox_IdSedistaREADONLY.Text);
                                                        SqlCommand komanda_RezervisiSediste = new SqlCommand();

                                                        komanda_RezervisiSediste.CommandText = "UPDATE SE_NALAZI_U set rezervisano = 'Da' WHERE idSale = @idSale AND idSedista = @idSedista AND idProjekcije = @idProjekcije";
                                                        komanda_RezervisiSediste.Parameters.AddWithValue("@idSale", idSale);
                                                        komanda_RezervisiSediste.Parameters.AddWithValue("@idSedista", idSedista);
                                                        komanda_RezervisiSediste.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                                        komanda_RezervisiSediste.Connection = connection;

                                                        if (komanda_RezervisiSediste.ExecuteNonQuery() == 1)
                                                        {
                                                            komanda_RezervisiSediste.CommandText = "INSERT INTO KARTA (idFilma, idSale, idSedista, idProjekcije) VALUES (@idFilma, @idSale, @idSedista, @idProjekcije)";
                                                            komanda_RezervisiSediste.Parameters.AddWithValue("@idFilma", idFilma_Rez);
                                                            komanda_RezervisiSediste.Connection = connection;

                                                            if (komanda_RezervisiSediste.ExecuteNonQuery() == 1)
                                                            {
                                                                SqlCommand komanda_UzmiIdKarte = new SqlCommand();

                                                                komanda_UzmiIdKarte.CommandText = "SELECT TOP 1 idKarte FROM KARTA ORDER BY idKarte DESC";
                                                                komanda_UzmiIdKarte.Connection = connection;

                                                                SqlDataReader citac_UzmiIdKarte = komanda_UzmiIdKarte.ExecuteReader();

                                                                if (citac_UzmiIdKarte.Read() == false)
                                                                {
                                                                    citac_UzmiIdKarte.Close();
                                                                    System.Windows.MessageBox.Show("Došlo je do greške pri čitanju karte.");
                                                                }
                                                                else
                                                                {
                                                                    int idKarte = citac_UzmiIdKarte.GetInt32(0);
                                                                    citac_UzmiIdKarte.Close();

                                                                    SqlCommand komanda_UzmiIdKorisnika = new SqlCommand();

                                                                    komanda_UzmiIdKorisnika.CommandText = "SELECT idKorisnika FROM KORISNIK WHERE email = @email";
                                                                    komanda_UzmiIdKorisnika.Parameters.AddWithValue("@email", email_Rez);
                                                                    komanda_UzmiIdKorisnika.Connection = connection;

                                                                    SqlDataReader citac_UzmiIdKorisnika = komanda_UzmiIdKorisnika.ExecuteReader();

                                                                    if (citac_UzmiIdKorisnika.Read() == false)
                                                                    {
                                                                        citac_UzmiIdKorisnika.Close();
                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju korisnika.");
                                                                    }
                                                                    else
                                                                    {
                                                                        int idKorisnika = citac_UzmiIdKorisnika.GetInt32(0);
                                                                        citac_UzmiIdKorisnika.Close();

                                                                        komanda_RezervisiSediste.CommandText = "INSERT INTO TRANSAKCIJA (brojKartice, bezbednosniBroj, idKorisnika, idKarte) VALUES (@brojKartice, @bezbednosniBroj, @idKorisnika, @idKarte)";
                                                                        komanda_RezervisiSediste.Parameters.AddWithValue("@brojKartice", textbox_BrojKarticeKorisnika.Text);
                                                                        komanda_RezervisiSediste.Parameters.AddWithValue("@bezbednosniBroj", textbox_BezbednosniKodKarticeKorisnika.Text);
                                                                        komanda_RezervisiSediste.Parameters.AddWithValue("@idKorisnika", idKorisnika);
                                                                        komanda_RezervisiSediste.Parameters.AddWithValue("@idKarte", idKarte);
                                                                        komanda_RezervisiSediste.Connection = connection;

                                                                        if (komanda_RezervisiSediste.ExecuteNonQuery() == 1)
                                                                        {
                                                                            osveziSedista();
                                                                            System.Windows.MessageBox.Show("Uspešno izvršena transakcija, sedište je rezervisano.");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Windows.MessageBox.Show("Došlo je do greške pri transakciji.");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Windows.MessageBox.Show("Došlo je do greške pri unošenju karte.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Windows.MessageBox.Show("Došlo je do greške pri izmeni zauzeća sedišta");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            System.Windows.MessageBox.Show("Prezime koje ste uneli ne odgovara prezimenu sa kojim ste se registrovali.");
                                        }
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Ime koje ste uneli ne odgovara imenu sa kojim ste se registrovali.");
                                }
                            }
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Email koji ste uneli se ne poklapa sa emailom sa kojim ste se prijavili.");
                }
            }
        }
    }
}
