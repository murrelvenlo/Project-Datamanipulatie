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
using System.Windows.Shapes;
using Vluchten_DAL;
using Vluchten_Models;

namespace VenloMurrel_d1._1_DM_Project
{
    /// <summary>
    /// Interaction logic for VluchtBoeken.xaml
    /// </summary>
    public partial class VluchtBoeken : Window
    {
        public VluchtBoeken()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblVluchtInformatie.Content = "";
            List<Passagier> passagiers = DatabaseOperations.PassagiersOphalen();
            foreach (var passagier in passagiers)
            {
                
                foreach (var reservering in passagier.Reserveringen)
                {
                    foreach (var reserveringvlucht in reservering.Reserveringvluchten)
                    {

                        lblVluchtInformatie.Content += "De passagier, " + passagier.voornaam + " " + passagier.achternaam + " met boekingsreferentie: " + reservering.boekingscode + " met het vluchtnummer: " + reserveringvlucht.vluchtId + " van " + reserveringvlucht.Vlucht.Luchthaven1 + " naar " + reserveringvlucht.Vlucht.Luchthaven + Environment.NewLine;
                        
                    }
                }
            }
            lblBevestiging.Visibility = Visibility.Hidden;
            btnVerwijderenBevestigen.Visibility = Visibility.Hidden;
            btnVerwijderenAnnuleren.Visibility = Visibility.Hidden;
            btnPassagierBijwerken.Visibility = Visibility.Hidden;
            btnPassagierVerwijderen.Visibility = Visibility.Hidden;
            btnUpdateAnnuleren.Visibility = Visibility.Hidden;
        }

        private void BtnVerderGaan(object sender, RoutedEventArgs e)
        {
            PassagierWindow pgs = new PassagierWindow();
            pgs.Show();
        }

        //private void BtnGeboekteVluchten_Click(object sender, RoutedEventArgs e)
        //{
        //    //datagridReserveringen.ItemsSource = DatabaseOperations.AlleVluchten();
        //}

       

        private void BtnToonPassagier_Click(object sender, RoutedEventArgs e)
        {
            DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
            btnPassagierBijwerken.Visibility = Visibility.Visible;
            btnPassagierVerwijderen.Visibility = Visibility.Visible;
            schuifbalkActiveren();

        }

        private void DataPassagiers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DataPassagiers.SelectedItem is Passagier passagier)
            {
                txtFamilienaam.Text = passagier.achternaam;
                txtVoornaam.Text = passagier.voornaam;
                txtMail.Text = passagier.emailadres;
                txtNationaliteit.Text = passagier.nationaliteit;
                txtGeboorte.Text = passagier.geboortedatum.ToLongDateString();
                txtPlaats.Text = passagier.plaats;
                txtTelefoonnummer.Text = passagier.telefoonnummer;
            }

        }

        private void BtnPassagierVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            Passagier passagier = DataPassagiers.SelectedItem as Passagier;
            
            string foutmeldingen = Valideer("Passagier");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                lblBevestiging.Visibility = Visibility.Visible;
                lblBevestiging.Content = $"Weet u zeker dat u  {passagier.voornaam} {passagier.achternaam} wilt verwijderen?";
                btnVerwijderenBevestigen.Visibility = Visibility.Visible;
                btnVerwijderenAnnuleren.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }

        private void BtnPassagierBijwerken_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateAnnuleren.Visibility = Visibility.Visible;


            string foutmeldingen = Valideer("Passagier");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Passagier passagier = DataPassagiers.SelectedItem as Passagier;

                

                passagier.achternaam = txtFamilienaam.Text;
                passagier.voornaam = txtVoornaam.Text;
                passagier.emailadres = txtMail.Text;
                passagier.nationaliteit = txtNationaliteit.Text;
                txtGeboorte.Text = passagier.geboortedatum.ToLongDateString() ;
                passagier.plaats = txtPlaats.Text;
                passagier.telefoonnummer = txtTelefoonnummer.Text;
                

                if (passagier.IsGeldig())
                {
                    int gelukt = DatabaseOperations.PassagierAanpassen(passagier);

                    if (gelukt > 0)
                    {
                        DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
                        lblBevestiging.Visibility = Visibility.Hidden;
                        
                    }
                    else
                    {
                        MessageBox.Show("Passagier is niet aangepast!");
                    }
                }
                else
                {
                    MessageBox.Show(passagier.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }

        }

        private string Valideer(string columnName)
        {
            if (columnName == "Passagier" && DataPassagiers.SelectedIndex == -1)
            {
                return "Selecteer eerst een passagier" + Environment.NewLine;
            }
            return "";
        }

        private void BtnPassagierToevoegen_Click(object sender, RoutedEventArgs e)
        {
            PassagierWindow passagierWindow = new PassagierWindow();
            passagierWindow.Show();
        }

        

        private void BtnVerwijderenAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            lblBevestiging.Visibility = Visibility.Hidden;
            DataPassagiers.SelectedItem = false;
            vlakkenLeegmaken();
        }

        private void BtnVerwijderenBevestigen_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("Passagier");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Passagier passagier = DataPassagiers.SelectedItem as Passagier;


                int gelukt = DatabaseOperations.PassagierVerwijderen(passagier);
                if (gelukt > 0)
                {
                    DataPassagiers.ItemsSource = DatabaseOperations.PassagierMetIdOphalen(passagier.id);
                    lblBevestiging.Visibility = Visibility.Hidden;

                }
                else
                {
                    MessageBox.Show("Passagier is niet verwijderd!");
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }

        private void BtnUpdateAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            vlakkenLeegmaken();
        }

        private void BtnSchermAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void schuifbalkActiveren()
        {
            ScrollViewer.SetHorizontalScrollBarVisibility(this.DataPassagiers, ScrollBarVisibility.Visible);
            ScrollViewer.SetVerticalScrollBarVisibility(this.DataPassagiers, ScrollBarVisibility.Visible);
        }

        private void vlakkenLeegmaken()
        {
            txtFamilienaam.Text = "";
            txtVoornaam.Text = "";
            txtMail.Text = "";
            txtNationaliteit.Text = "";
            txtGeboorte.Text = "";
            txtPlaats.Text = "";
            txtTelefoonnummer.Text = "";
            DataPassagiers.SelectedItem = null;
            btnUpdateAnnuleren.Visibility = Visibility.Hidden;
        }

    }
}
