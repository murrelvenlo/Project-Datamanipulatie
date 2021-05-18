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

        private void datagridVluchten_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void btnToonPgs_Click(object sender, RoutedEventArgs e)
        {
            VluchtBoeken vluchtBoeken = new VluchtBoeken();
            vluchtBoeken.Show();
        }

        /*private void datagridVluchten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//Moet verder gedaan worden   
            
            VluchtBoeken vluchtBoeken = new VluchtBoeken();
            vluchtBoeken.Show();
            List<Vlucht> vluchten = DatabaseOperations.VluchtenZoeken();
            
            if (datagridVluchten.SelectedItem is Vlucht vluchtje)
            {
                string eenNieuweRegel = Environment.NewLine;
                vluchtBoeken.lblVluchtInformatie.Content = "";
                vluchtBoeken.lblVluchtInformatie.Content += $"{vluchtje.vertrektijd}  -------------- {vluchtje.aankomsttijd}{eenNieuweRegel}{vluchtje.Luchthaven1.land} --------------  {vluchtje.Luchthaven.land}" + eenNieuweRegel;
                vluchtBoeken.lblVluchtInformatie.Content += $"Vertrek vanaf:  {vluchtje.vertrektijd} {vluchtje.Luchthaven1.IATA} {vluchtje.Luchthaven1.stad}" + eenNieuweRegel;
                vluchtBoeken.lblVluchtInformatie.Content += $"Aankomst in:  {vluchtje.aankomsttijd} {vluchtje.Luchthaven.IATA} {vluchtje.Luchthaven.stad}" + eenNieuweRegel;
                vluchtBoeken.lblVluchtInformatie.Content += $"Datum:  {vluchtje.datum.ToString()} " + eenNieuweRegel;
                vluchtBoeken.lblVluchtInformatie.Content += $"Reisduur: {(vluchtje.aankomsttijd - vluchtje.vertrektijd).ToString()} " + eenNieuweRegel;

                
                foreach (var  passagier in passagiers) { foreach (var reservering in passagier.Reserveringen) { foreach (var reserveringvlucht in reservering.Reserveringvluchten) {
                            vluchtBoeken.lblVluchtInformatie.Content += $"Passagier: {passagier.voornaam}"; } } }

                //Heen vlucht
                /*vluchtBoeken.txtInfoVLucht.Text = (vluchtje.vertrektijd   + " ------------- " + vluchtje.aankomsttijd).PadRight(3) + "\n"  + (vluchtje.Luchthaven1.land + " ------------- " + vluchtje.Luchthaven.land).PadLeft(3);
                vluchtBoeken.txtHeenVlucht.Text = "Vertrek: " + vluchtje.vertrektijd + " " + vluchtje.Luchthaven1.IATA + " " + vluchtje.Luchthaven1.stad;
                vluchtBoeken.txtEindBestemming.Text = "Aankomst: " + vluchtje.aankomsttijd + " " + vluchtje.Luchthaven.IATA + " " + vluchtje.Luchthaven.stad;
                vluchtBoeken.txtDatum.Text = "Datum: " + vluchtje.datum.ToString();
                vluchtBoeken.txtReisduur.Text = "Reisduur: " + (vluchtje.aankomsttijd - vluchtje.vertrektijd).ToString();
                */
        /*Terug
        vluchtBoeken.txtInfoVLucht2.Text = (vluchtje.vertrektijd + " ------------- " + vluchtje.aankomsttijd).PadRight(3) + "\n" + (vluchtje.Luchthaven.land + " ------------- " + vluchtje.Luchthaven1.land).PadLeft(3);
        vluchtBoeken.txtHeenVlucht2.Text = "Vertrek: " + vluchtje.vertrektijd + " " + vluchtje.Luchthaven.IATA + " " + vluchtje.Luchthaven.stad;
        vluchtBoeken.txtEindBestemming2.Text = "Aankomst: " + vluchtje.aankomsttijd + " " + vluchtje.Luchthaven1.IATA + " " + vluchtje.Luchthaven1.stad;
        vluchtBoeken.txtDatum2.Text = "Datum: " + vluchtje.datum.ToString();
        vluchtBoeken.txtReisduur2.Text = "Reisduur: " + (vluchtje.aankomsttijd - vluchtje.vertrektijd).ToString();
        */

    }
        }

        /*private void datagridVluchten_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {

        }

        private void btnToonPgs(object sender, RoutedEventArgs e)
        {
            VluchtBoeken vluchtBoeken = new VluchtBoeken();
            vluchtBoeken.Show();
        }

        /*private void VlGegevensBekijken_Click(object sender, MouseButtonEventArgs e)
        {
            VluchtBoeken vluchtBoeken = new VluchtBoeken();
            vluchtBoeken.Show();
        }

        private void datagridVluchten_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            VluchtBoeken vluchtBoeken = new VluchtBoeken();
            vluchtBoeken.Show();
        }*/
    

