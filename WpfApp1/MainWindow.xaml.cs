using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Prozori;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void klik_PrijavljivanjeProzor(object sender, RoutedEventArgs e)
        {
            Prijavljivanje pR = new Prijavljivanje();
            pR.Show();
            this.Close();
        }

        private void klik_Registrovanje(object sender, RoutedEventArgs e)
        {
            Registrovanje rG = new Registrovanje();
            rG.Show();
            this.Close();
        }
    }
}
