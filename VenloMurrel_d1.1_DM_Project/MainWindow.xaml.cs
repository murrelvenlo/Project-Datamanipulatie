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

        private void btnGewensteVlucht_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(txtVertrek.Text) && !string.IsNullOrWhiteSpace(txtAankomst.Text))
            {

                datagridVluchten.ItemsSource = DatabaseOperations.GewensteVluchtenZoeken(txtVertrek.Text, txtAankomst.Text);
            }
            else if (!string.IsNullOrWhiteSpace(txtAankomst.Text))
            {
                //CustomMessageBox.Toon("Vanaf waar vertrekt u?");
                CustomMessageBoxStatic.CustomMessage.Toon("vanaf waar vertrekt u?");
                CustomMessageBoxStatic.CustomMessage.Fail();
            }
            else if (!string.IsNullOrWhiteSpace(txtVertrek.Text))
            {
                CustomMessageBoxStatic.CustomMessage.Toon("Geef een bestemming in!");
                CustomMessageBoxStatic.CustomMessage.Fail();
            }

            else
            {
                CustomMessageBoxStatic.CustomMessage.Toon("De velden mogen niet leeg zijn!");
                CustomMessageBoxStatic.CustomMessage.Fail();
            }

        }


        private void datagridVluchten_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void btnToonPgs_Click(object sender, RoutedEventArgs e)
        {
            VluchtBoeken vluchtBoeken = new VluchtBoeken();
            vluchtBoeken.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Vluchten";
        }
    }
}