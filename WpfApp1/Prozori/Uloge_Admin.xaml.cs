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

namespace WpfApp1.Prozori
{
    /// <summary>
    /// Interaction logic for Uloge_Admin.xaml
    /// </summary>
    public partial class Uloge_Admin : Window
    {
        public Uloge_Admin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dataGrid_Uloge();
            dataGrid_Glumci();
            dataGrid_Filmovi();
        }

        private void klik_Nazad(object sender, RoutedEventArgs e)
        {
            Admin aD = new Admin();
            aD.Show();
            this.Close();
        }

        private void glumac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdGlumcaAdmin.Text = dr["idGlumca"].ToString();
            }
        }

        private void film_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdFilmaAdmin.Text = dr["idFilma"].ToString();
            }
        }

        private void uloge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgr = sender as DataGrid;
            DataRowView dr = dgr.SelectedItem as DataRowView;
            if (dr != null)
            {
                textbox_IdGlumcaAdmin.Text = dr["idGlumca"].ToString();
                textbox_IdFilmaAdmin.Text = dr["idFilma"].ToString();
                textbox_IdUlogeAdmin.Text = dr["idUloge"].ToString();
                textbox_UlogaAdmin.Text = dr["uloga"].ToString();
            }
        }

        private void klik_OsveziPolja(object sender, RoutedEventArgs e)
        {
            osveziPolja();
        }

        private void klik_OsveziTabele(object sender, RoutedEventArgs e)
        {
            dataGrid_Uloge();
            dataGrid_Glumci();
            dataGrid_Filmovi();
        }

        void osveziPolja()
        {
            textbox_UlogaAdmin.Text = "";
            textbox_IdFilmaAdmin.Text = "";
            textbox_IdGlumcaAdmin.Text = "";
            textbox_IdUlogeAdmin.Text = "";
        }

        void dataGrid_Uloge()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_PopuniUloge = new SqlCommand();
            komanda_PopuniUloge.CommandText = "SELECT GLUMI.idGlumca, GLUMI.idFilma, GLUMI.idUloge, GLUMI.uloga, OSOBA.ime + ' ' + OSOBA.prezime as 'GLUMAC', FILM.nazivFilma FROM [GLUMI] INNER JOIN [GLUMAC] on GLUMI.idGlumca = GLUMAC.idGlumca INNER JOIN OSOBA on GLUMAC.idOsobe = OSOBA.idOsobe INNER JOIN FILM on GLUMI.idFilma = FILM.idFilma";
            komanda_PopuniUloge.Connection = connection;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda_PopuniUloge);

            DataTable dataTable_ULOGE = new DataTable("ULOGE");
            dataAdapter.Fill(dataTable_ULOGE);
            datagrid_Uloge.ItemsSource = dataTable_ULOGE.DefaultView;
        }

        void dataGrid_Glumci()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_PopuniGlumca = new SqlCommand();
            komanda_PopuniGlumca.CommandText = "SELECT GLUMAC.idGlumca, GLUMAC.idOsobe, OSOBA.ime, OSOBA.prezime FROM [GLUMAC] INNER JOIN [OSOBA] on GLUMAC.idOsobe = OSOBA.idOsobe";
            komanda_PopuniGlumca.Connection = connection;

            SqlDataAdapter dataAdapter_GLUMAC = new SqlDataAdapter(komanda_PopuniGlumca);

            DataTable dataTable_GLUMAC = new DataTable("GLUMAC");
            dataAdapter_GLUMAC.Fill(dataTable_GLUMAC);
            datagrid_Glumci.ItemsSource = dataTable_GLUMAC.DefaultView;
        }

        void dataGrid_Filmovi()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand komanda_PopuniFilm = new SqlCommand();
            komanda_PopuniFilm.CommandText = "SELECT FILM.idFilma, FILM.nazivFilma, FILM.godinaIzdanja FROM [FILM]";
            komanda_PopuniFilm.Connection = connection;

            SqlDataAdapter dataAdapter_FILM = new SqlDataAdapter(komanda_PopuniFilm);

            DataTable dataTable_FILM = new DataTable("FILM");
            dataAdapter_FILM.Fill(dataTable_FILM);
            datagrid_Filmovi.ItemsSource = dataTable_FILM.DefaultView;
        }

        private void klik_UnesiUlogu(object sender, RoutedEventArgs e)
        {
            int idGlumca;
            int idFilma;

            string uloga = textbox_UlogaAdmin.Text;

            string idGlumcaString = textbox_IdGlumcaAdmin.Text;
            string idFilmaString = textbox_IdFilmaAdmin.Text;
            string idUlogeString = textbox_IdUlogeAdmin.Text;

            if(idUlogeString != "")
            {
                System.Windows.MessageBox.Show("Polje ID mora biti prazno, osvezite polja.");
            }
            else
            {
                if (uloga == null || uloga == "")
                {
                    System.Windows.MessageBox.Show("Polje uloga ne sme ostati prazno.");
                }
                else
                {
                    if(uloga.Length >= 41)
                    {
                        System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera za ulogu (40).");
                    }
                    else
                    {
                        if (idGlumcaString == null || idGlumcaString == "")
                        {
                            System.Windows.MessageBox.Show("Polje ID glumca ne sme ostati prazno.");
                        }
                        else
                        {
                            idGlumca = Convert.ToInt32(idGlumcaString);

                            if (idFilmaString == null || idFilmaString == "")
                            {
                                System.Windows.MessageBox.Show("Polje ID filma ne sme ostati prazno.");
                            }
                            else
                            {
                                idFilma = Convert.ToInt32(idFilmaString);

                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                connection.Open();

                                SqlCommand komanda_ProveriUlogu = new SqlCommand();

                                komanda_ProveriUlogu.CommandText = "SELECT uloga FROM [GLUMI] WHERE idGlumca = @idGlumca and idFilma = @idFilma";
                                komanda_ProveriUlogu.Parameters.AddWithValue("@idGlumca", idGlumca);
                                komanda_ProveriUlogu.Parameters.AddWithValue("@idFilma", idFilma);
                                komanda_ProveriUlogu.Connection = connection;

                                SqlDataReader citac_ProveriUlogu = komanda_ProveriUlogu.ExecuteReader();

                                int slucaj;

                                if (citac_ProveriUlogu.Read() == false)
                                {
                                    citac_ProveriUlogu.Close();
                                    slucaj = 0;
                                }
                                else
                                {
                                    string proveriUlogu = citac_ProveriUlogu.GetString(0);
                                    citac_ProveriUlogu.Close();

                                    if (uloga == proveriUlogu)
                                    {
                                        slucaj = 1;
                                        System.Windows.MessageBox.Show("Došlo je do greške, ponovo odaberite ulogu iz tabele.");
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("ULOGA JE OK");
                                        slucaj = 0;
                                    }
                                }

                                switch (slucaj)
                                {
                                    case 0:
                                        System.Windows.MessageBox.Show("CASE 0");

                                        SqlCommand komanda_DodajUlogu = new SqlCommand();

                                        komanda_DodajUlogu.CommandText = "INSERT INTO [GLUMI] (uloga, idGlumca, idFilma) VALUES (@uloga, @idGlumca, @idFilma)";
                                        komanda_DodajUlogu.Parameters.AddWithValue("@uloga", uloga);
                                        komanda_DodajUlogu.Parameters.AddWithValue("@idGlumca", idGlumca);
                                        komanda_DodajUlogu.Parameters.AddWithValue("@idFilma", idFilma);
                                        komanda_DodajUlogu.Connection = connection;

                                        if (komanda_DodajUlogu.ExecuteNonQuery() == 1)
                                        {
                                            MessageBox.Show("Uspešno uneti podaci.");
                                            dataGrid_Uloge();
                                        }
                                        else
                                        {
                                            System.Windows.MessageBox.Show("Došlo je do greške pri unosu.");
                                        }
                                        osveziPolja();
                                        break;
                                    case 1:
                                        System.Windows.MessageBox.Show("Ovaj glumac sa ovom ulogom u ovom filmu se već nalazi u bazi.");
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void klik_IzmeniUlogu(object sender, RoutedEventArgs e)
        {
            int idGlumca;
            int idFilma;
            int idUloge;

            string uloga = textbox_UlogaAdmin.Text;

            string idGlumcaString = textbox_IdGlumcaAdmin.Text;
            string idFilmaString = textbox_IdFilmaAdmin.Text;
            string idUlogeString = textbox_IdUlogeAdmin.Text;

            if (uloga == null || uloga == "")
            {
                System.Windows.MessageBox.Show("Polje uloga ne sme ostati prazno.");
            }
            else
            {
                if(uloga.Length >= 41)
                {
                    System.Windows.MessageBox.Show("Prekoračen broj dozvoljenih karaktera za ulogu (40).");
                }
                else
                {
                    if (idGlumcaString == null || idGlumcaString == "")
                    {
                        System.Windows.MessageBox.Show("Polje ID glumca ne sme ostati prazno.");
                    }
                    else
                    {
                        idGlumca = Convert.ToInt32(idGlumcaString);

                        if (idFilmaString == null || idFilmaString == "")
                        {
                            System.Windows.MessageBox.Show("Polje ID filma ne sme ostati prazno.");
                        }
                        else
                        {
                            idFilma = Convert.ToInt32(idFilmaString);

                            if (idUlogeString == null || idUlogeString == "")
                            {
                                System.Windows.MessageBox.Show("Polje ID uloge ne sme ostati prazno.");
                            }
                            else
                            {
                                idUloge = Convert.ToInt32(idUlogeString);

                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                connection.Open();

                                SqlCommand komanda_IzmeniUlogu = new SqlCommand();

                                komanda_IzmeniUlogu.CommandText = "UPDATE [GLUMI] SET uloga = @uloga WHERE idGlumca = @idGlumca and idFilma = @idFilma and idUloge = @idUloge";
                                komanda_IzmeniUlogu.Parameters.AddWithValue("@uloga", uloga);
                                komanda_IzmeniUlogu.Parameters.AddWithValue("@idGlumca", idGlumca);
                                komanda_IzmeniUlogu.Parameters.AddWithValue("@idFilma", idFilma);
                                komanda_IzmeniUlogu.Parameters.AddWithValue("@idUloge", idUloge);
                                komanda_IzmeniUlogu.Connection = connection;

                                if (komanda_IzmeniUlogu.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("Uspešno izmenjeni podaci.");
                                    dataGrid_Uloge();
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
        }

        private void klik_IzbrisiUlogu(object sender, RoutedEventArgs e)
        {
            int idUloge;
            int idGlumca;
            int idFilma;

            string uloga = textbox_UlogaAdmin.Text;

            string idUlogeString = textbox_IdUlogeAdmin.Text;
            string idGlumcaString = textbox_IdGlumcaAdmin.Text;
            string idFilmaString = textbox_IdFilmaAdmin.Text;

            if (uloga == null || uloga == "")
            {
                System.Windows.MessageBox.Show("Polje uloga ne sme ostati prazno.");
            }
            else
            {
                if (idGlumcaString == null || idGlumcaString == "")
                {
                    System.Windows.MessageBox.Show("Polje ID glumca ne sme ostati prazno.");
                }
                else
                {
                    idGlumca = Convert.ToInt32(idGlumcaString);

                    if (idFilmaString == null || idFilmaString == "")
                    {
                        System.Windows.MessageBox.Show("Polje ID filma ne sme ostati prazno.");
                    }
                    else
                    {
                        idFilma = Convert.ToInt32(idFilmaString);

                        if (idUlogeString == null || idUlogeString == "")
                        {
                            System.Windows.MessageBox.Show("Polje ID uloge ne sme ostati prazno.");
                        }
                        else
                        {
                            if(idUlogeString == null || idUlogeString == "")
                            {
                                System.Windows.MessageBox.Show("Polje ID uloge ne sme ostati prazno.");
                            }
                            else
                            {
                                idUloge = Convert.ToInt32(idUlogeString);

                                SqlConnection connection = new SqlConnection();
                                connection.ConnectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.SZB_ConnectionString"].ConnectionString;
                                connection.Open();

                                SqlCommand komanda_IzbrisiUlogu = new SqlCommand();

                                komanda_IzbrisiUlogu.CommandText = "SELECT uloga FROM [GLUMI] WHERE idGlumca = @idGlumca and idFilma = @idFilma and idUloge = @idUloge";
                                komanda_IzbrisiUlogu.Parameters.AddWithValue("@idGlumca", idGlumca);
                                komanda_IzbrisiUlogu.Parameters.AddWithValue("@idFilma", idFilma);
                                komanda_IzbrisiUlogu.Parameters.AddWithValue("@idUloge", idUloge);
                                komanda_IzbrisiUlogu.Connection = connection;

                                SqlDataReader citac_ProveriUlogu = komanda_IzbrisiUlogu.ExecuteReader();
                                string proveriUlogu;

                                if (citac_ProveriUlogu.Read() )
                                {
                                    proveriUlogu = citac_ProveriUlogu.GetString(0);
                                    citac_ProveriUlogu.Close();

                                    if(uloga == proveriUlogu)
                                    {
                                        komanda_IzbrisiUlogu.CommandText = "DELETE FROM GLUMI WHERE idUloge = @idUloge and idGlumca = @idGlumca and idFilma = @idFilma";
                                        komanda_IzbrisiUlogu.Connection = connection;

                                        int proveri = komanda_IzbrisiUlogu.ExecuteNonQuery();
                                        if (proveri == 1)
                                        {
                                            osveziPolja();
                                            dataGrid_Uloge();
                                            MessageBox.Show("Uloga je uspešno izbrisana.", "Uspešno brisanje", MessageBoxButton.OK);
                                        }
                                        else
                                        {
                                            System.Windows.MessageBox.Show("Došlo je do greške pri brisanju.");
                                        }
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Uloga ne odgovara ID-evima, ponovo odaberite ulogu iz tabele.");
                                    }
                                }
                                else
                                {
                                    citac_ProveriUlogu.Close();
                                    System.Windows.MessageBox.Show("Došlo je do greške pri čitanju.");
                                }
                                osveziPolja();
                            }
                        }
                    }
                }
            }
        }
    }
}
