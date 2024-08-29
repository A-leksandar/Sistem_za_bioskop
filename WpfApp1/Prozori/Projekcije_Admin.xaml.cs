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
using Xceed.Wpf.Toolkit;

namespace WpfApp1.Prozori
{
    /// <summary>
    /// Interaction logic for Projekcije_Admin.xaml
    /// </summary>
    public partial class Projekcije_Admin : Window
    {
        public Projekcije_Admin()
        {
            InitializeComponent();
            dataGrid_Filmovi();
            dataGrid_Projekcije();
            sedista_Default();
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        void dataGrid_Projekcije()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT SALA.idSale, PROJEKCIJA.idFilma, FILM.nazivFilma, PROJEKCIJA.datumProjekcije, PROJEKCIJA.idProjekcije FROM PROJEKCIJA INNER JOIN SALA on PROJEKCIJA.idSale = SALA.idSale INNER JOIN FILM on PROJEKCIJA.idFilma = FILM.idFilma";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("PROJEKCIJA");
            dataAdapter.Fill(dataTable);
            datagrid_Projekcije.ItemsSource = dataTable.DefaultView;
        }

        void dataGrid_Filmovi()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT idFilma, nazivFilma FROM FILM";
            komanda.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);

            DataTable dataTable = new DataTable("FILM");
            dataAdapter.Fill(dataTable);
            datagrid_Filmovi.ItemsSource = dataTable.DefaultView;
        }

        private void projekcije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdProjekcijeAdmin.Text = dr["idProjekcije"].ToString();
                textbox_IdFilmaAdmin.Text = dr["idFilma"].ToString();
                combobox_IdSaleAdmin.SelectedIndex = Convert.ToInt32(dr["idSale"].ToString()) - 1;
                textbox_NazivFilmaAdmin.Text = dr["nazivFilma"].ToString();
                datepicker_DatumProjekcijeAdmin.Value = Convert.ToDateTime(dr["datumProjekcije"]);

                Broj_sale.Content = "SALA " + dr["idSale"].ToString();
                Datum_projekcije.Content = dr["datumProjekcije"].ToString();

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                connection.Open();

                SqlCommand komanda_ProveriZauzeto = new SqlCommand();

                for (int i = 1; i <= 60; i++)
                {
                    komanda_ProveriZauzeto.CommandText = "SELECT rezervisano FROM SE_NALAZI_U WHERE idSale = @idSale AND idProjekcije = @idProjekcije AND idSedista = @idSedista";
                    komanda_ProveriZauzeto.Parameters.AddWithValue("@idSale", Convert.ToInt32(dr["idSale"]));
                    komanda_ProveriZauzeto.Parameters.AddWithValue("@idProjekcije", Convert.ToInt32(dr["idProjekcije"]));
                    komanda_ProveriZauzeto.Parameters.AddWithValue("@idSedista", i);
                    komanda_ProveriZauzeto.Connection = connection;

                    SqlDataReader citac_ProveriZauzeto = komanda_ProveriZauzeto.ExecuteReader();

                    if(citac_ProveriZauzeto.Read() == false)
                    {
                        citac_ProveriZauzeto.Close();
                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju zauzeća sedišta.");
                    }
                    else
                    {
                        string proveri_Zauzeto = citac_ProveriZauzeto.GetString(0);
                        citac_ProveriZauzeto.Close();

                        komanda_ProveriZauzeto.Parameters.Clear();

                        if(proveri_Zauzeto == "Da")
                        {
                            ImageBrush slika = new ImageBrush();
                            slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Zauzeto.png", UriKind.Relative));

                            switch (i)
                            {
                                case 1:
                                    sediste_1.Background = slika;
                                    break;
                                case 2:
                                    sediste_2.Background = slika;
                                    break;
                                case 3:
                                    sediste_3.Background = slika;
                                    break;
                                case 4:
                                    sediste_4.Background = slika;
                                    break;
                                case 5:
                                    sediste_5.Background = slika;
                                    break;
                                case 6:
                                    sediste_6.Background = slika;
                                    break;
                                case 7:
                                    sediste_7.Background = slika;
                                    break;
                                case 8:
                                    sediste_8.Background = slika;
                                    break;
                                case 9:
                                    sediste_9.Background = slika;
                                    break;
                                case 10:
                                    sediste_10.Background = slika;
                                    break;
                                case 11:
                                    sediste_11.Background = slika;
                                    break;
                                case 12:
                                    sediste_12.Background = slika;
                                    break;
                                case 13:
                                    sediste_13.Background = slika;
                                    break;
                                case 14:
                                    sediste_14.Background = slika;
                                    break;
                                case 15:
                                    sediste_15.Background = slika;
                                    break;
                                case 16:
                                    sediste_16.Background = slika;
                                    break;
                                case 17:
                                    sediste_17.Background = slika;
                                    break;
                                case 18:
                                    sediste_18.Background = slika;
                                    break;
                                case 19:
                                    sediste_19.Background = slika;
                                    break;
                                case 20:
                                    sediste_20.Background = slika;
                                    break;
                                case 21:
                                    sediste_21.Background = slika;
                                    break;
                                case 22:
                                    sediste_22.Background = slika;
                                    break;
                                case 23:
                                    sediste_23.Background = slika;
                                    break;
                                case 24:
                                    sediste_24.Background = slika;
                                    break;
                                case 25:
                                    sediste_25.Background = slika;
                                    break;
                                case 26:
                                    sediste_26.Background = slika;
                                    break;
                                case 27:
                                    sediste_27.Background = slika;
                                    break;
                                case 28:
                                    sediste_28.Background = slika;
                                    break;
                                case 29:
                                    sediste_29.Background = slika;
                                    break;
                                case 30:
                                    sediste_30.Background = slika;
                                    break;
                                case 31:
                                    sediste_31.Background = slika;
                                    break;
                                case 32:
                                    sediste_32.Background = slika;
                                    break;
                                case 33:
                                    sediste_33.Background = slika;
                                    break;
                                case 34:
                                    sediste_34.Background = slika;
                                    break;
                                case 35:
                                    sediste_35.Background = slika;
                                    break;
                                case 36:
                                    sediste_36.Background = slika;
                                    break;
                                case 37:
                                    sediste_37.Background = slika;
                                    break;
                                case 38:
                                    sediste_38.Background = slika;
                                    break;
                                case 39:
                                    sediste_39.Background = slika;
                                    break;
                                case 40:
                                    sediste_40.Background = slika;
                                    break;
                                case 41:
                                    sediste_41.Background = slika;
                                    break;
                                case 42:
                                    sediste_42.Background = slika;
                                    break;
                                case 43:
                                    sediste_43.Background = slika;
                                    break;
                                case 44:
                                    sediste_44.Background = slika;
                                    break;
                                case 45:
                                    sediste_45.Background = slika;
                                    break;
                                case 46:
                                    sediste_46.Background = slika;
                                    break;
                                case 47:
                                    sediste_47.Background = slika;
                                    break;
                                case 48:
                                    sediste_48.Background = slika;
                                    break;
                                case 49:
                                    sediste_49.Background = slika;
                                    break;
                                case 50:
                                    sediste_50.Background = slika;
                                    break;
                                case 51:
                                    sediste_51.Background = slika;
                                    break;
                                case 52:
                                    sediste_52.Background = slika;
                                    break;
                                case 53:
                                    sediste_53.Background = slika;
                                    break;
                                case 54:
                                    sediste_54.Background = slika;
                                    break;
                                case 55:
                                    sediste_55.Background = slika;
                                    break;
                                case 56:
                                    sediste_56.Background = slika;
                                    break;
                                case 57:
                                    sediste_57.Background = slika;
                                    break;
                                case 58:
                                    sediste_58.Background = slika;
                                    break;
                                case 59:
                                    sediste_59.Background = slika;
                                    break;
                                case 60:
                                    sediste_60.Background = slika;
                                    break;
                            }
                        }
                        else if(proveri_Zauzeto == "Ne")
                        {
                            ImageBrush slika = new ImageBrush();
                            slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Slobodno.png", UriKind.Relative));

                            switch (i)
                            {
                                case 1:
                                    sediste_1.Background = slika;
                                    break;
                                case 2:
                                    sediste_2.Background = slika;
                                    break;
                                case 3:
                                    sediste_3.Background = slika;
                                    break;
                                case 4:
                                    sediste_4.Background = slika;
                                    break;
                                case 5:
                                    sediste_5.Background = slika;
                                    break;
                                case 6:
                                    sediste_6.Background = slika;
                                    break;
                                case 7:
                                    sediste_7.Background = slika;
                                    break;
                                case 8:
                                    sediste_8.Background = slika;
                                    break;
                                case 9:
                                    sediste_9.Background = slika;
                                    break;
                                case 10:
                                    sediste_10.Background = slika;
                                    break;
                                case 11:
                                    sediste_11.Background = slika;
                                    break;
                                case 12:
                                    sediste_12.Background = slika;
                                    break;
                                case 13:
                                    sediste_13.Background = slika;
                                    break;
                                case 14:
                                    sediste_14.Background = slika;
                                    break;
                                case 15:
                                    sediste_15.Background = slika;
                                    break;
                                case 16:
                                    sediste_16.Background = slika;
                                    break;
                                case 17:
                                    sediste_17.Background = slika;
                                    break;
                                case 18:
                                    sediste_18.Background = slika;
                                    break;
                                case 19:
                                    sediste_19.Background = slika;
                                    break;
                                case 20:
                                    sediste_20.Background = slika;
                                    break;
                                case 21:
                                    sediste_21.Background = slika;
                                    break;
                                case 22:
                                    sediste_22.Background = slika;
                                    break;
                                case 23:
                                    sediste_23.Background = slika;
                                    break;
                                case 24:
                                    sediste_24.Background = slika;
                                    break;
                                case 25:
                                    sediste_25.Background = slika;
                                    break;
                                case 26:
                                    sediste_26.Background = slika;
                                    break;
                                case 27:
                                    sediste_27.Background = slika;
                                    break;
                                case 28:
                                    sediste_28.Background = slika;
                                    break;
                                case 29:
                                    sediste_29.Background = slika;
                                    break;
                                case 30:
                                    sediste_30.Background = slika;
                                    break;
                                case 31:
                                    sediste_31.Background = slika;
                                    break;
                                case 32:
                                    sediste_32.Background = slika;
                                    break;
                                case 33:
                                    sediste_33.Background = slika;
                                    break;
                                case 34:
                                    sediste_34.Background = slika;
                                    break;
                                case 35:
                                    sediste_35.Background = slika;
                                    break;
                                case 36:
                                    sediste_36.Background = slika;
                                    break;
                                case 37:
                                    sediste_37.Background = slika;
                                    break;
                                case 38:
                                    sediste_38.Background = slika;
                                    break;
                                case 39:
                                    sediste_39.Background = slika;
                                    break;
                                case 40:
                                    sediste_40.Background = slika;
                                    break;
                                case 41:
                                    sediste_41.Background = slika;
                                    break;
                                case 42:
                                    sediste_42.Background = slika;
                                    break;
                                case 43:
                                    sediste_43.Background = slika;
                                    break;
                                case 44:
                                    sediste_44.Background = slika;
                                    break;
                                case 45:
                                    sediste_45.Background = slika;
                                    break;
                                case 46:
                                    sediste_46.Background = slika;
                                    break;
                                case 47:
                                    sediste_47.Background = slika;
                                    break;
                                case 48:
                                    sediste_48.Background = slika;
                                    break;
                                case 49:
                                    sediste_49.Background = slika;
                                    break;
                                case 50:
                                    sediste_50.Background = slika;
                                    break;
                                case 51:
                                    sediste_51.Background = slika;
                                    break;
                                case 52:
                                    sediste_52.Background = slika;
                                    break;
                                case 53:
                                    sediste_53.Background = slika;
                                    break;
                                case 54:
                                    sediste_54.Background = slika;
                                    break;
                                case 55:
                                    sediste_55.Background = slika;
                                    break;
                                case 56:
                                    sediste_56.Background = slika;
                                    break;
                                case 57:
                                    sediste_57.Background = slika;
                                    break;
                                case 58:
                                    sediste_58.Background = slika;
                                    break;
                                case 59:
                                    sediste_59.Background = slika;
                                    break;
                                case 60:
                                    sediste_60.Background = slika;
                                    break;
                            }
                        }
                        else
                        {
                            ImageBrush slika = new ImageBrush();
                            slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Error.png", UriKind.Relative));

                            switch (i)
                            {
                                case 1:
                                    sediste_1.Background = slika;
                                    break;
                                case 2:
                                    sediste_2.Background = slika;
                                    break;
                                case 3:
                                    sediste_3.Background = slika;
                                    break;
                                case 4:
                                    sediste_4.Background = slika;
                                    break;
                                case 5:
                                    sediste_5.Background = slika;
                                    break;
                                case 6:
                                    sediste_6.Background = slika;
                                    break;
                                case 7:
                                    sediste_7.Background = slika;
                                    break;
                                case 8:
                                    sediste_8.Background = slika;
                                    break;
                                case 9:
                                    sediste_9.Background = slika;
                                    break;
                                case 10:
                                    sediste_10.Background = slika;
                                    break;
                                case 11:
                                    sediste_11.Background = slika;
                                    break;
                                case 12:
                                    sediste_12.Background = slika;
                                    break;
                                case 13:
                                    sediste_13.Background = slika;
                                    break;
                                case 14:
                                    sediste_14.Background = slika;
                                    break;
                                case 15:
                                    sediste_15.Background = slika;
                                    break;
                                case 16:
                                    sediste_16.Background = slika;
                                    break;
                                case 17:
                                    sediste_17.Background = slika;
                                    break;
                                case 18:
                                    sediste_18.Background = slika;
                                    break;
                                case 19:
                                    sediste_19.Background = slika;
                                    break;
                                case 20:
                                    sediste_20.Background = slika;
                                    break;
                                case 21:
                                    sediste_21.Background = slika;
                                    break;
                                case 22:
                                    sediste_22.Background = slika;
                                    break;
                                case 23:
                                    sediste_23.Background = slika;
                                    break;
                                case 24:
                                    sediste_24.Background = slika;
                                    break;
                                case 25:
                                    sediste_25.Background = slika;
                                    break;
                                case 26:
                                    sediste_26.Background = slika;
                                    break;
                                case 27:
                                    sediste_27.Background = slika;
                                    break;
                                case 28:
                                    sediste_28.Background = slika;
                                    break;
                                case 29:
                                    sediste_29.Background = slika;
                                    break;
                                case 30:
                                    sediste_30.Background = slika;
                                    break;
                                case 31:
                                    sediste_31.Background = slika;
                                    break;
                                case 32:
                                    sediste_32.Background = slika;
                                    break;
                                case 33:
                                    sediste_33.Background = slika;
                                    break;
                                case 34:
                                    sediste_34.Background = slika;
                                    break;
                                case 35:
                                    sediste_35.Background = slika;
                                    break;
                                case 36:
                                    sediste_36.Background = slika;
                                    break;
                                case 37:
                                    sediste_37.Background = slika;
                                    break;
                                case 38:
                                    sediste_38.Background = slika;
                                    break;
                                case 39:
                                    sediste_39.Background = slika;
                                    break;
                                case 40:
                                    sediste_40.Background = slika;
                                    break;
                                case 41:
                                    sediste_41.Background = slika;
                                    break;
                                case 42:
                                    sediste_42.Background = slika;
                                    break;
                                case 43:
                                    sediste_43.Background = slika;
                                    break;
                                case 44:
                                    sediste_44.Background = slika;
                                    break;
                                case 45:
                                    sediste_45.Background = slika;
                                    break;
                                case 46:
                                    sediste_46.Background = slika;
                                    break;
                                case 47:
                                    sediste_47.Background = slika;
                                    break;
                                case 48:
                                    sediste_48.Background = slika;
                                    break;
                                case 49:
                                    sediste_49.Background = slika;
                                    break;
                                case 50:
                                    sediste_50.Background = slika;
                                    break;
                                case 51:
                                    sediste_51.Background = slika;
                                    break;
                                case 52:
                                    sediste_52.Background = slika;
                                    break;
                                case 53:
                                    sediste_53.Background = slika;
                                    break;
                                case 54:
                                    sediste_54.Background = slika;
                                    break;
                                case 55:
                                    sediste_55.Background = slika;
                                    break;
                                case 56:
                                    sediste_56.Background = slika;
                                    break;
                                case 57:
                                    sediste_57.Background = slika;
                                    break;
                                case 58:
                                    sediste_58.Background = slika;
                                    break;
                                case 59:
                                    sediste_59.Background = slika;
                                    break;
                                case 60:
                                    sediste_60.Background = slika;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void filmovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdFilmaAdmin.Text = dr["idFilma"].ToString();
                textbox_NazivFilmaAdmin.Text = dr["nazivFilma"].ToString();
            }
        }

        private void klik_UnesiProjekciju(object sender, RoutedEventArgs e)
        {
            int idFilma;

            int idSale = combobox_IdSaleAdmin.SelectedIndex;
            idSale += 1;
            System.Windows.MessageBox.Show("ID SALE: " + idSale);
            string idFilmaString = textbox_IdFilmaAdmin.Text;
            string nazivFilma = textbox_NazivFilmaAdmin.Text;
            DateTime? datumProjekcije = datepicker_DatumProjekcijeAdmin.Value;

            if(idSale == -1)
            {
                System.Windows.MessageBox.Show("Nije odabrana ni jedna sala, ponovo odaberite salu.");
            }
            else
            {
                if(idFilmaString == null || idFilmaString == "")
                {
                    System.Windows.MessageBox.Show("Polje ID filma ne sme ostati prazno.");
                }
                else
                {
                    idFilma = Convert.ToInt32(idFilmaString);

                    if(nazivFilma == null || nazivFilma == "")
                    {
                        System.Windows.MessageBox.Show("Polje naziv filma ne sme ostati prazno.");
                    }
                    else
                    {
                        if(nazivFilma.Length >= 51)
                        {
                            System.Windows.MessageBox.Show("");
                        }
                        else
                        {
                            if (datumProjekcije == null)
                            {
                                System.Windows.MessageBox.Show("Datum projekcije ne sme ostati prazan.");
                            }
                            else
                            {
                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                connection.Open();

                                SqlCommand komanda_DodajProjekciju = new SqlCommand();
                                komanda_DodajProjekciju.CommandText = "INSERT INTO PROJEKCIJA (idSale, idFilma, datumProjekcije) VALUES (@idSale, @idFilma, @datumProjekcije)";
                                komanda_DodajProjekciju.Parameters.AddWithValue("idSale", idSale);
                                komanda_DodajProjekciju.Parameters.AddWithValue("idFilma", idFilma);
                                komanda_DodajProjekciju.Parameters.AddWithValue("datumProjekcije", datumProjekcije);
                                komanda_DodajProjekciju.Connection = connection;

                                int proveri = komanda_DodajProjekciju.ExecuteNonQuery();
                                if (proveri == 1)
                                {
                                    osveziPolja();
                                    dataGrid_Filmovi();
                                    dataGrid_Projekcije();
                                    System.Windows.MessageBox.Show("Uspešno dodati podaci o projekciji.");

                                    SqlCommand komanda_UzmiIdProjekcije = new SqlCommand();
                                    komanda_UzmiIdProjekcije.CommandText = "SELECT idProjekcije FROM PROJEKCIJA WHERE datumProjekcije = @datumProjekcije AND idSale = @idSale AND idFilma = @idFilma";
                                    komanda_UzmiIdProjekcije.Parameters.AddWithValue("@datumProjekcije", datumProjekcije);
                                    komanda_UzmiIdProjekcije.Parameters.AddWithValue("@idSale", idSale);
                                    komanda_UzmiIdProjekcije.Parameters.AddWithValue("@idFilma", idFilma);
                                    komanda_UzmiIdProjekcije.Connection = connection;

                                    SqlDataReader citac_UzmiIdProjekcije = komanda_UzmiIdProjekcije.ExecuteReader();

                                    if(citac_UzmiIdProjekcije.Read() == false)
                                    {
                                        citac_UzmiIdProjekcije.Close();
                                        System.Windows.MessageBox.Show("Došlo je do greške pri čitanju ID-a projekcije.");
                                    }
                                    else
                                    {
                                        int idProjekcije = citac_UzmiIdProjekcije.GetInt32(0);
                                        citac_UzmiIdProjekcije.Close();

                                        SqlCommand komanda_DodajSedista = new SqlCommand();

                                        for (int i = 1; i <= 60; i++)
                                        {
                                            string rezervisano = "Ne";

                                            komanda_DodajSedista.CommandText = "INSERT INTO SE_NALAZI_U (idSale, idSedista, idProjekcije, rezervisano) VALUES (@idSale, @idSedista, @idProjekcije, @rezervisano)";
                                            komanda_DodajSedista.Parameters.AddWithValue("@idSale", idSale);
                                            komanda_DodajSedista.Parameters.AddWithValue("@idSedista", i);
                                            komanda_DodajSedista.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                            komanda_DodajSedista.Parameters.AddWithValue("@rezervisano", rezervisano);
                                            komanda_DodajSedista.Connection = connection;

                                            if(komanda_DodajSedista.ExecuteNonQuery() != 1)
                                            {
                                                komanda_DodajSedista.Parameters.Clear();
                                                System.Windows.MessageBox.Show("Došlo je do greške pri ubacivanju sedišta: " + i);
                                            }
                                            else
                                            {
                                                komanda_DodajSedista.Parameters.Clear();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Došlo je do greške pri ubacivanju podataka o projekciji.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void klik_IzmeniProjekciju(object sender, RoutedEventArgs e)
        {
            int idFilma;
            int idProjekcije;

            int idSale = combobox_IdSaleAdmin.SelectedIndex;
            idSale += 1;
            string idProjekcijeString = textbox_IdProjekcijeAdmin.Text;
            string idFilmaString = textbox_IdFilmaAdmin.Text;
            string nazivFilma = textbox_NazivFilmaAdmin.Text;
            DateTime? datumProjekcije = datepicker_DatumProjekcijeAdmin.Value;

            if(idProjekcijeString == null || idProjekcijeString == "")
            {
                System.Windows.MessageBox.Show("Polje ID projekcije ne sme ostati prazno.");
            }
            else
            {
                idProjekcije = Convert.ToInt32(idProjekcijeString);

                if (idSale == -1)
                {
                    System.Windows.MessageBox.Show("Nije odabrana ni jedna sala, ponovo odaberite salu.");
                }
                else
                {
                    if (idFilmaString == null || idFilmaString == "")
                    {
                        System.Windows.MessageBox.Show("Polje ID filma ne sme ostati prazno.");
                    }
                    else
                    {
                        idFilma = Convert.ToInt32(idFilmaString);

                        if (nazivFilma == null || nazivFilma == "")
                        {
                            System.Windows.MessageBox.Show("Polje naziv filma ne sme ostati prazno.");
                        }
                        else
                        {
                            if (datumProjekcije == null)
                            {
                                System.Windows.MessageBox.Show("Datum projekcije ne sme ostati prazan.");
                            }
                            else
                            {
                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                connection.Open();

                                SqlCommand komanda_IzmeniProjekciju = new SqlCommand();
                                komanda_IzmeniProjekciju.CommandText = "SELECT idFilma from PROJEKCIJA where idProjekcije = @idProjekcije";
                                komanda_IzmeniProjekciju.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                komanda_IzmeniProjekciju.Connection = connection;

                                SqlDataReader citac_UzmiIdFilma = komanda_IzmeniProjekciju.ExecuteReader();

                                int proveri_IdFilma;
                                if(citac_UzmiIdFilma.Read() == false)
                                {
                                    citac_UzmiIdFilma.Close();
                                    System.Windows.MessageBox.Show("Došlo je do greške pri čitanju ID-a filma.");
                                }
                                else
                                {
                                    proveri_IdFilma = citac_UzmiIdFilma.GetInt32(0);
                                    citac_UzmiIdFilma.Close();

                                    if(idFilma == proveri_IdFilma)
                                    {
                                        komanda_IzmeniProjekciju.CommandText = "SELECT FILM.nazivFilma from FILM where idFilma = @idFilma";
                                        komanda_IzmeniProjekciju.Parameters.AddWithValue("@idFilma", idFilma);
                                        komanda_IzmeniProjekciju.Connection = connection;

                                        SqlDataReader citac_UzmiNazivFilma = komanda_IzmeniProjekciju.ExecuteReader();

                                        string proveri_NazivFilma;
                                        if (citac_UzmiNazivFilma.Read() == false)
                                        {
                                            citac_UzmiNazivFilma.Close();
                                            System.Windows.MessageBox.Show("Došlo je do greške pri čitanju naziva filma.");
                                        }
                                        else
                                        {
                                            proveri_NazivFilma = citac_UzmiNazivFilma.GetString(0);
                                            citac_UzmiNazivFilma.Close();

                                            if (nazivFilma == proveri_NazivFilma)
                                            {
                                                SqlCommand komanda_IzbrisiSveOStarojProjekciji = new SqlCommand();
                                                komanda_IzbrisiSveOStarojProjekciji.CommandText = "SELECT MAX(idKarte) FROM KARTA";
                                                komanda_IzbrisiSveOStarojProjekciji.Connection = connection;

                                                SqlDataReader citac_UzmiBrojKarata = komanda_IzbrisiSveOStarojProjekciji.ExecuteReader();

                                                if(citac_UzmiBrojKarata.Read() == false)
                                                {
                                                    citac_UzmiBrojKarata .Close();
                                                    System.Windows.MessageBox.Show("Došlo je do greške pri čitanju broja karata.");
                                                }
                                                else
                                                {
                                                    if (!citac_UzmiBrojKarata.IsDBNull(0))
                                                    {
                                                        int brojKarata = citac_UzmiBrojKarata.GetInt32(0);
                                                        citac_UzmiBrojKarata.Close();

                                                        komanda_IzbrisiSveOStarojProjekciji.Parameters.Clear();

                                                        for(int i = 1; i <= brojKarata; i++)
                                                        {
                                                            komanda_IzbrisiSveOStarojProjekciji.CommandText = "SELECT idProjekcije FROM KARTA WHERE idKarte = @idKarte";
                                                            komanda_IzbrisiSveOStarojProjekciji.Parameters.AddWithValue("@idKarte", i);
                                                            komanda_IzbrisiSveOStarojProjekciji.Connection = connection;

                                                            SqlDataReader citac_UzmiIdProjekcije = komanda_IzbrisiSveOStarojProjekciji.ExecuteReader();

                                                            if(citac_UzmiIdProjekcije.Read() == false )
                                                            {
                                                                citac_UzmiIdProjekcije.Close();
                                                                komanda_IzbrisiSveOStarojProjekciji.Parameters.Clear();
                                                                System.Windows.MessageBox.Show("Došlo je do greške pro čitanju ID-a projekcije.");
                                                            }
                                                            else
                                                            {
                                                                int proveri_IdProjekcije= citac_UzmiIdProjekcije.GetInt32(0);
                                                                citac_UzmiIdProjekcije.Close();

                                                                if(idProjekcije != proveri_IdFilma)
                                                                {
                                                                    continue;
                                                                }
                                                                else
                                                                {
                                                                    komanda_IzbrisiSveOStarojProjekciji.CommandText = "DELETE FROM TRANSAKCIJA WHERE idKarte = @idKarte";
                                                                    komanda_IzbrisiSveOStarojProjekciji.Connection = connection;

                                                                    if (komanda_IzbrisiSveOStarojProjekciji.ExecuteNonQuery() >= 0)
                                                                    {
                                                                        komanda_IzbrisiSveOStarojProjekciji.CommandText = "DELETE FROM KARTA WHERE idKarte = @idKarte";
                                                                        komanda_IzbrisiSveOStarojProjekciji.Connection = connection;

                                                                        if (komanda_IzbrisiSveOStarojProjekciji.ExecuteNonQuery() >= 0)
                                                                        {
                                                                            komanda_IzbrisiSveOStarojProjekciji.Parameters.Clear();
                                                                        }
                                                                        else
                                                                        {
                                                                            komanda_IzbrisiSveOStarojProjekciji.Parameters.Clear();
                                                                            System.Windows.MessageBox.Show("Došlo je do greške pri brisanju karte.");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        komanda_IzbrisiSveOStarojProjekciji.Parameters.Clear();
                                                                        System.Windows.MessageBox.Show("Došlo je do greške pri brisanju transakcije.");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        citac_UzmiBrojKarata.Close();
                                                    }

                                                    komanda_IzmeniProjekciju.Parameters.Clear();

                                                    komanda_IzmeniProjekciju.CommandText = "UPDATE PROJEKCIJA SET idSale = @idSale, datumProjekcije = @datumProjekcije WHERE idFilma = @idFilma and idProjekcije = @idProjekcije";
                                                    komanda_IzmeniProjekciju.Parameters.AddWithValue("@idSale", idSale);
                                                    komanda_IzmeniProjekciju.Parameters.AddWithValue("@datumProjekcije", datumProjekcije);
                                                    komanda_IzmeniProjekciju.Parameters.AddWithValue("@idFilma", idFilma);
                                                    komanda_IzmeniProjekciju.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                                    komanda_IzmeniProjekciju.Connection = connection;

                                                    if (komanda_IzmeniProjekciju.ExecuteNonQuery() == 1)
                                                    {
                                                        komanda_IzmeniProjekciju.Parameters.Clear();

                                                        komanda_IzmeniProjekciju.CommandText = "UPDATE SE_NALAZI_U SET rezervisano = 'Ne', idSale = @idSale WHERE idProjekcije = @idProjekcije";
                                                        komanda_IzmeniProjekciju.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                                        komanda_IzmeniProjekciju.Parameters.AddWithValue("@idSale", idSale);
                                                        komanda_IzmeniProjekciju.Connection = connection;

                                                        if (komanda_IzmeniProjekciju.ExecuteNonQuery() == 60)
                                                        {
                                                            osveziPolja();
                                                            dataGrid_Filmovi();
                                                            dataGrid_Projekcije();
                                                            sedista_Default();
                                                            System.Windows.MessageBox.Show("Uspešno izmenjeni podaci.");
                                                        }
                                                        else
                                                        {
                                                            System.Windows.MessageBox.Show("Došlo je do greške pri izmeni zauzeća sedišta.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Windows.MessageBox.Show("Došlo je do greške pri izmeni podataka.");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Windows.MessageBox.Show("Naziv filma ne odgovara ID-u projekcije, ponovo odaberite projekciju.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("ID filma ne odgovara ID-u projekcije, ponovo odaberite projekciju.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void klik_IzbrisiProjekciju(object sender, RoutedEventArgs e)
        {
            int idFilma;
            int idProjekcije;

            int idSale = combobox_IdSaleAdmin.SelectedIndex;
            idSale += 1;
            string idProjekcijeString = textbox_IdProjekcijeAdmin.Text;
            string idFilmaString = textbox_IdFilmaAdmin.Text;
            string nazivFilma = textbox_NazivFilmaAdmin.Text;
            DateTime? datumProjekcije = datepicker_DatumProjekcijeAdmin.Value;

            if (idProjekcijeString == null || idProjekcijeString == "")
            {
                System.Windows.MessageBox.Show("Polje ID projekcije ne sme ostati prazno.");
            }
            else
            {
                idProjekcije = Convert.ToInt32(idProjekcijeString);

                if (idSale == -1)
                {
                    System.Windows.MessageBox.Show("Nije odabrana ni jedna sala, ponovo odaberite salu.");
                }
                else
                {
                    if (idFilmaString == null || idFilmaString == "")
                    {
                        System.Windows.MessageBox.Show("Polje ID filma ne sme ostati prazno.");
                    }
                    else
                    {
                        idFilma = Convert.ToInt32(idFilmaString);

                        if (nazivFilma == null || nazivFilma == "")
                        {
                            System.Windows.MessageBox.Show("Polje naziv filma ne sme ostati prazno.");
                        }
                        else
                        {
                            if (datumProjekcije == null)
                            {
                                System.Windows.MessageBox.Show("Datum projekcije ne sme ostati prazan.");
                            }
                            else
                            {
                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                connection.Open();

                                SqlCommand komanda_ProveriIdKarte = new SqlCommand();
                                komanda_ProveriIdKarte.CommandText = "SELECT MAX(idKarte) FROM KARTA";
                                komanda_ProveriIdKarte.Connection = connection;

                                SqlDataReader citac_UzmiMaxIdKarte = komanda_ProveriIdKarte.ExecuteReader();

                                if(citac_UzmiMaxIdKarte.Read() == false)
                                {
                                    System.Windows.MessageBox.Show("Došlo je do greške pri čitanju ID-a karte.");
                                }
                                else
                                {
                                    if(!citac_UzmiMaxIdKarte.IsDBNull(0))
                                    {
                                        int brojKarata = citac_UzmiMaxIdKarte.GetInt32(0);
                                        citac_UzmiMaxIdKarte.Close();

                                        SqlCommand komanda_IzbrisiTransakciju = new SqlCommand();

                                        for(int i = 1; i <= brojKarata; i++)
                                        {
                                            komanda_IzbrisiTransakciju.CommandText = "SELECT idProjekcije FROM KARTA WHERE idKarte = @idKarte";
                                            komanda_IzbrisiTransakciju.Parameters.AddWithValue("@idKarte", i);
                                            komanda_IzbrisiTransakciju.Connection = connection;

                                            SqlDataReader citac_UzmiIdProjekcije = komanda_IzbrisiTransakciju.ExecuteReader();

                                            if(citac_UzmiIdProjekcije.Read() == false)
                                            {
                                                komanda_IzbrisiTransakciju.Parameters.Clear();
                                                citac_UzmiIdProjekcije.Close();
                                            }
                                            else
                                            {
                                                int proveri_IdProjekcije = citac_UzmiIdProjekcije.GetInt32(0);
                                                citac_UzmiIdProjekcije.Close();

                                                if(idProjekcije != proveri_IdProjekcije)
                                                {
                                                    komanda_IzbrisiTransakciju.Parameters.Clear();
                                                    continue;
                                                }
                                                else
                                                {
                                                    komanda_IzbrisiTransakciju.CommandText = "DELETE FROM TRANSAKCIJA WHERE idKarte = @idKarte";
                                                    komanda_IzbrisiTransakciju.Connection = connection;

                                                    if(komanda_IzbrisiTransakciju.ExecuteNonQuery() != 1)
                                                    {
                                                        komanda_IzbrisiTransakciju.Parameters.Clear();
                                                        System.Windows.MessageBox.Show("Došlo je do greške pri brisanju transakcije.");
                                                    }
                                                    else
                                                    {
                                                        komanda_IzbrisiTransakciju.Parameters.Clear();

                                                        SqlCommand komanda_IzbrisiKartu = new SqlCommand();
                                                        komanda_IzbrisiKartu.CommandText = "DELETE FROM KARTA WHERE idKarte = @idKarte";
                                                        komanda_IzbrisiKartu.Parameters.AddWithValue("@idKarte", i);
                                                        komanda_IzbrisiKartu.Connection = connection;

                                                        if(komanda_IzbrisiKartu.ExecuteNonQuery() == 1)
                                                        {
                                                            komanda_IzbrisiKartu.Parameters.Clear();
                                                        }
                                                        else
                                                        {
                                                            komanda_IzbrisiKartu.Parameters.Clear();
                                                            System.Windows.MessageBox.Show("Došlo je do greške pri brisanju karte.");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        citac_UzmiMaxIdKarte.Close();
                                    }

                                    SqlCommand komanda_IzbrisiSedista = new SqlCommand();
                                    komanda_IzbrisiSedista.CommandText = "DELETE FROM SE_NALAZI_U WHERE idProjekcije = @idProjekcije";
                                    komanda_IzbrisiSedista.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                    komanda_IzbrisiSedista.Connection = connection;

                                    if (komanda_IzbrisiSedista.ExecuteNonQuery() == 60)
                                    {
                                        SqlCommand komanda_IzbrisiProjekciju = new SqlCommand();
                                        komanda_IzbrisiProjekciju.CommandText = "DELETE FROM PROJEKCIJA WHERE idProjekcije = @idProjekcije";
                                        komanda_IzbrisiProjekciju.Parameters.AddWithValue("@idProjekcije", idProjekcije);
                                        komanda_IzbrisiProjekciju.Connection = connection;

                                        if (komanda_IzbrisiProjekciju.ExecuteNonQuery() == 1)
                                        {
                                            dataGrid_Filmovi();
                                            dataGrid_Projekcije();
                                            sedista_Default();
                                            System.Windows.MessageBox.Show("Podaci o projekciji su uspešno izbrisani.");
                                        }
                                        else
                                        {
                                            System.Windows.MessageBox.Show("Došlo je do greške pri brisanju projekcije.");
                                        }
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Došlo je do greške pri brisanju sedišta.");
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
            sedista_Default();
        }

        private void klik_OsveziTabele(object sender, RoutedEventArgs e)
        {
            dataGrid_Projekcije();
            dataGrid_Filmovi();
        }

        private void osveziPolja()
        {
            textbox_IdProjekcijeAdmin.Text = "";
            textbox_IdFilmaAdmin.Text = "";
            textbox_NazivFilmaAdmin.Text = "";
            combobox_IdSaleAdmin.SelectedIndex = -1;
            datepicker_DatumProjekcijeAdmin.Value = null;
        }

        private void sedista_Default()
        {
            ImageBrush slika = new ImageBrush();
            slika.ImageSource = new BitmapImage(new Uri("Images/sediste_Error.png", UriKind.Relative));

            Broj_sale.Content = "SALA X";
            Datum_projekcije.Content = "DATUM PROJEKCIJE";

            sediste_1.Background  = slika;
            sediste_2.Background  = slika;
            sediste_3.Background  = slika;
            sediste_4.Background  = slika;
            sediste_5.Background  = slika;
            sediste_6.Background  = slika;
            sediste_7.Background  = slika;
            sediste_8.Background  = slika;
            sediste_9.Background  = slika;
            sediste_10.Background = slika;
            sediste_11.Background = slika;
            sediste_12.Background = slika;
            sediste_13.Background = slika;
            sediste_14.Background = slika;
            sediste_15.Background = slika;
            sediste_16.Background = slika;
            sediste_17.Background = slika;
            sediste_18.Background = slika;
            sediste_19.Background = slika;
            sediste_20.Background = slika;
            sediste_21.Background = slika;
            sediste_22.Background = slika;
            sediste_23.Background = slika;
            sediste_24.Background = slika;
            sediste_25.Background = slika;
            sediste_26.Background = slika;
            sediste_27.Background = slika;
            sediste_28.Background = slika;
            sediste_29.Background = slika;
            sediste_30.Background = slika;
            sediste_31.Background = slika;
            sediste_32.Background = slika;
            sediste_33.Background = slika;
            sediste_34.Background = slika;
            sediste_35.Background = slika;
            sediste_36.Background = slika;
            sediste_37.Background = slika;
            sediste_38.Background = slika;
            sediste_39.Background = slika;
            sediste_40.Background = slika;
            sediste_41.Background = slika;
            sediste_42.Background = slika;
            sediste_43.Background = slika;
            sediste_44.Background = slika;
            sediste_45.Background = slika;
            sediste_46.Background = slika;
            sediste_47.Background = slika;
            sediste_48.Background = slika;
            sediste_49.Background = slika;
            sediste_50.Background = slika;
            sediste_51.Background = slika;
            sediste_52.Background = slika;
            sediste_53.Background = slika;
            sediste_54.Background = slika;
            sediste_55.Background = slika;
            sediste_56.Background = slika;
            sediste_57.Background = slika;
            sediste_58.Background = slika;
            sediste_59.Background = slika;
            sediste_60.Background = slika;
        }
    }
}
