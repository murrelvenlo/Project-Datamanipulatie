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
                //lblVluchtInformatie.Content += "De passagier, " + passagier.voornaam + " " + passagier.achternaam + Environment.NewLine;
                foreach (var reservering in passagier.Reserveringen)
                {

                    //lblVluchtInformatie.Content += "met boekingsreferentie: " + reservering.boekingscode + Environment.NewLine;

                    foreach (var reserveringvlucht in reservering.Reserveringvluchten)
                    {

                        lblVluchtInformatie.Content += "De passagier, " + passagier.voornaam + " " + passagier.achternaam + " met boekingsreferentie: " + reservering.boekingscode + " met het vluchtnummer: " + reserveringvlucht.vluchtId + " van " + reserveringvlucht.Vlucht.Luchthaven1 + " naar " + reserveringvlucht.Vlucht.Luchthaven + Environment.NewLine;
                        //lblVluchtInformatie.Content += "met vluchtnummer: " + reserveringvlucht.vluchtId + " van " + reserveringvlucht.Vlucht.Luchthaven1 + " naar " + reserveringvlucht.Vlucht.Luchthaven + Environment.NewLine;
                    }
                }
            }
            //cmbVluchtnummer.DisplayMemberPath = "Luchthaven";
            //cmbVluchtnummer.ItemsSource = DatabaseOperations.LijstVluchtnummers();
        }

        private void BtnVerderGaan(object sender, RoutedEventArgs e)
        {
            PassagierWindow pgs = new PassagierWindow();
            pgs.Show();
        }

        private void BtnGeboekteVluchten_Click(object sender, RoutedEventArgs e)
        {
            //datagridReserveringen.ItemsSource = DatabaseOperations.AlleVluchten();
        }

        //private string Valideer(string columName)
        //{
        //    if (columName == "vluchtId" && cmbVluchtnummer.SelectedItem == null)
        //    {
        //        return "Selecteer eerst een vluchtnummer!" + Environment.NewLine;
        //    }
        //    if (columName == "Reserveringvlucht" && datagridReserveringen.SelectedItem == null)
        //    {
        //        return "Selecteer eerst een reserveringvlucht!" + Environment.NewLine;
        //    }
        //    if (columName == "txtReserveringId" && !int.TryParse(txtReserveringId.Text, out int reserveringId))
        //    {
        //        return "Geef eerst een boekingsreferentie in!" + Environment.NewLine;
        //    }
        //    return "";
        //}

        /*private void BtnRToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //Valideren
            string foutmeldingen = Valideer("vluchtId");
            foutmeldingen += Valideer("reserveringId");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //Reserveringvlucht aanmaken
                Vlucht vlucht = cmbVluchtnummer.SelectedItem as Vlucht;

                Reserveringvlucht reserveringvlucht = new Reserveringvlucht();

                reserveringvlucht.reserveringId = int.Parse(txtReserveringId.Text);
                reserveringvlucht.vluchtId = vlucht.id;

                //Probeer iet met luchthaven hier

                if (reserveringvlucht.IsGeldig())
                {
                    int gelukt = DatabaseOperations.ToevoegenReservatie(reserveringvlucht);

                    if (gelukt > 0)
                    {
                        datagridReserveringen.ItemsSource = DatabaseOperations.ReserveringIdOphalen(reserveringvlucht.reserveringId);
                        //Misschien Resetten();
                    }
                    else
                    {
                        MessageBox.Show("Reserveringvlucht is niet ingegeven!");
                    }
                }
                else
                {
                    MessageBox.Show(reserveringvlucht.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }*/

        private void BtnTry_Click(object sender, RoutedEventArgs e)
        {
            Inloggen inloggen = new Inloggen();
            inloggen.Show();
        }

        private void btnGa_Click(object sender, RoutedEventArgs e)
        {
            ReservatieWindow reservatieWindow = new ReservatieWindow();
            reservatieWindow.Show();
        }

        private void BtnToonPassagier_Click(object sender, RoutedEventArgs e)
        {
            DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
        }

        private void DataPassagiers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PassagierWindow passagierWindow = new PassagierWindow();
            passagierWindow.Show();

            if (DataPassagiers.SelectedItem is Passagier passagier)
            {
                passagierWindow.txtNaam.Text = passagier.achternaam;
                passagierWindow.txtVoornaam.Text = passagier.voornaam;
                passagierWindow.txtEmail.Text = passagier.emailadres;
                passagierWindow.txtNationaliteit.Text = passagier.nationaliteit;
                passagierWindow.txtGeboorte.Text = passagier.geboortedatum.ToString();
                passagierWindow.txtPlaats.Text = passagier.plaats;
                passagierWindow.txtpNummer.Text = passagier.id.ToString();
                passagierWindow.txtTelefoonnummer.Text = passagier.telefoonnummer;
                passagierWindow.txtpNummer.IsEnabled = false;
            }

        }

        private void BtnPassagierVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            
            string foutmeldingen = Valideer("Passagier");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Passagier passagier = DataPassagiers.SelectedItem as Passagier;
                string achternaam = passagier.achternaam;

                int gelukt = DatabaseOperations.PassagierVerwijderen(passagier);
                if (gelukt > 0)
                {
                    DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
                }
                else
                {
                    MessageBox.Show("Passagier is niet verwijderd!");
                }
            }
        }

        private void BtnPassagierBijwerken_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("Passagier");

            foutmeldingen += Valideer("geboortedatum");

            

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Passagier passagier = DataPassagiers.SelectedItem as Passagier;

                PassagierWindow passagierWindow = new PassagierWindow();
                passagierWindow.Show();

                passagierWindow.txtGeboorte.Text = passagier.geboortedatum.ToString();

                if (passagier.IsGeldig())
                {
                    int gelukt = DatabaseOperations.PassagierAanpassen(passagier);

                    if (gelukt > 0)
                    {
                        DataPassagiers.ItemsSource = DatabaseOperations.PassagierOphalen();
                    }
                    else
                    {
                        MessageBox.Show("Passagier is niet aangepast");
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
            if (columnName == "Passagier" && DataPassagiers.SelectedItem == null)
            {
                return "Selecteer eerst een passagier" + Environment.NewLine;
            }
            return "";
        }
    }
}
