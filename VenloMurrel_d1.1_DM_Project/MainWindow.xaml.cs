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
using Vluchten_DAL;

namespace VenloMurrel_d1._1_DM_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            datagridVluchten.ItemsSource = DatabaseOperations.VluchtenZoeken();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //cmbKlasse.ItemsSource = DatabaseOperations.OphalenKlasse();

        }

        private void btnGewensteVlucht_Click(object sender, RoutedEventArgs e)
        {
            datagridVluchten.ItemsSource = DatabaseOperations.GewensteVluchtenZoeken(txtVertrek.Text, txtAankomst.Text);
        }

        private void datagridVluchten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagridVluchten.SelectedItem is Vlucht vlucht)
            {
                VluchtBoeken vluchtBoeken = new VluchtBoeken();
                vluchtBoeken.Show();
            }
        }
    }
}
