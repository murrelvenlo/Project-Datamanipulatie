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
            Title = "Gedetailleerde informatie over reservering en passagiers";

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
            btnPassagierVerwijderen.Visibility = Visibility.Hidden;
            btnPassagierBijwerken.Visibility = Visibility.Hidden;
            btnUpdateAnnuleren.Visibility = Visibility.Hidden;

        }

        private void BtnVerderGaan(object sender, RoutedEventArgs e)
        {
            PassagierWindow pgs = new PassagierWindow();
            pgs.Show();
        }


        private void BtnToonPassagier_Click(object sender, RoutedEventArgs e)
        {
            DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();

        }

        private void DataPassagiers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DataPassagiers.SelectedItem is Passagier passagier)
            {
                txtFamilienaam.Text = passagier.achternaam;
                txtVoornaam.Text = passagier.voornaam;
                txtMail.Text = passagier.emailadres;
                txtNationaliteit.Text = passagier.nationaliteit;
                dpGeboorte.Text = passagier.geboortedatum.ToLongDateString();
                txtPlaats.Text = passagier.plaats;
                txtTelefoonnummer.Text = passagier.telefoonnummer;

                btnUpdateAnnuleren.Visibility = Visibility.Visible;

                btnPassagierBijwerken.Visibility = Visibility.Visible;
                btnPassagierVerwijderen.Visibility = Visibility.Visible;
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
                CustomMessageBoxStatic.CustomMessage.Toon(foutmeldingen);
            }
        }

        private void BtnPassagierBijwerken_Click(object sender, RoutedEventArgs e)
        {
            
            string foutmeldingen = Valideer("Passagier");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Passagier passagier = DataPassagiers.SelectedItem as Passagier;
                


                passagier.achternaam = txtFamilienaam.Text;
                passagier.voornaam = txtVoornaam.Text;
                passagier.emailadres = txtMail.Text;
                passagier.nationaliteit = txtNationaliteit.Text;
                dpGeboorte.Text = passagier.geboortedatum.ToString() ;
                passagier.plaats = txtPlaats.Text;
                passagier.telefoonnummer = txtTelefoonnummer.Text;
                

                if (passagier.IsGeldig())
                {
                    int gelukt = DatabaseOperations.PassagierAanpassen(passagier);

                    if (gelukt > 0)
                    {
                        DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
                        btnPassagierBijwerken.Visibility = Visibility.Hidden;
                        btnUpdateAnnuleren.Visibility = Visibility.Hidden;
                        vlakkenLeegmaken();
                        DataPassagiers.Items.Refresh();

                    }
                    else
                    {
                        CustomMessageBoxStatic.CustomMessage.Toon("Passagier is niet aangepast!");
                        CustomMessageBoxStatic.CustomMessage.Fail();
                    }
                }
                else
                {
                    CustomMessageBoxStatic.CustomMessage.Toon(passagier.Error);
                    CustomMessageBoxStatic.CustomMessage.Fail();
                }

            }
            else
            {
                CustomMessageBoxStatic.CustomMessage.Toon(foutmeldingen);
                CustomMessageBoxStatic.CustomMessage.Fail();
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
            passagierWindow.ShowDialog();
            DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
        }

        

        private void BtnVerwijderenAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            lblBevestiging.Visibility = Visibility.Hidden;
            DataPassagiers.SelectedItem = false;
            vlakkenLeegmaken();
            btnVerwijderenBevestigen.Visibility = Visibility.Hidden;
            btnVerwijderenAnnuleren.Visibility = Visibility.Hidden;
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
                    DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
                    vlakkenLeegmaken();
                    lblBevestiging.Visibility = Visibility.Hidden;
                    
                    MessageBox.Show("Passagier is verwijderd!");
                    btnVerwijderenBevestigen.Visibility = Visibility.Hidden;
                    btnVerwijderenAnnuleren.Visibility = Visibility.Hidden;
                    DataPassagiers.Items.Refresh();

                }
                else
                {
                    CustomMessageBoxStatic.CustomMessage.Toon("Passagier is niet verwijderd!");
                    CustomMessageBoxStatic.CustomMessage.Fail();
                    DataPassagiers.Items.Refresh();
                }
            }
            else
            {
                CustomMessageBoxStatic.CustomMessage.Toon(foutmeldingen);
                CustomMessageBoxStatic.CustomMessage.Fail();
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


        private void vlakkenLeegmaken()
        {
            txtFamilienaam.Text = "";
            txtVoornaam.Text = "";
            txtMail.Text = "";
            txtNationaliteit.Text = "";
            dpGeboorte.Text = "";
            txtPlaats.Text = "";
            txtTelefoonnummer.Text = "";
            DataPassagiers.SelectedItem = null;
            btnUpdateAnnuleren.Visibility = Visibility.Hidden;
        }

    }
}
