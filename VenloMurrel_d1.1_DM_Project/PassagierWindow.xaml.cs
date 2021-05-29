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
    /// Interaction logic for PassagierWindow.xaml
    /// </summary>
    public partial class PassagierWindow : Window
    {
        public PassagierWindow()
        {
            InitializeComponent();
        }

        
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Passagiers toevoegen";
            
        }

        

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            VlakkenLeegMaken();
        }

        private string Valideer(string columnName)
        {
            if (columnName == "id" && !int.TryParse(txtpNummer.Text, out int id))
            {
                return "Passagiersnummer moet positief zijn!" + Environment.NewLine;
            }
            if (columnName == "geboortedatum" && !DateTime.TryParse(dpGeboorte.Text, out DateTime geboortedatum))
            {
                return "Geboortedatum moet in dateformat zijn!" + Environment.NewLine;
            }
            return "";
        }

        

        private void BtnSluiten_Click(object sender, RoutedEventArgs e)
        {
            //Methode om het huidige scherm af te sluiten. In dit geval sluit ik de derde van de 3 schermen.
            this.Close();
        }

        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("Passagier");

            foutmeldingen += Valideer("id");
            foutmeldingen += Valideer("geboortedatum");

            //foutmeldingen += Valideer();
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Passagier passagier = new Passagier();
                passagier.achternaam = txtNaam.Text;
                passagier.voornaam = txtVoornaam.Text;
                passagier.emailadres = txtEmail.Text;
                passagier.nationaliteit = txtNationaliteit.Text;
                passagier.geboortedatum = DateTime.Parse(dpGeboorte.Text);
                passagier.plaats = txtPlaats.Text;
                passagier.id = int.Parse(txtpNummer.Text);
                passagier.telefoonnummer = txtTelefoonnummer.Text;

                if (passagier.IsGeldig())
                {
                    int gelukt = DatabaseOperations.PassagierToevoegen(passagier);

                    if (gelukt > 0)
                    {
                        CustomMessageBoxStatic.CustomMessage.Toon("Passagier is toegevoegd!");
                        CustomMessageBoxStatic.CustomMessage.Succes();
                        VlakkenLeegMaken();
                    }
                    else
                    {
                        CustomMessageBoxStatic.CustomMessage.Toon("Passagier is niet toegevoeg!");
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

        private void VlakkenLeegMaken()
        {
            txtNaam.Text = "";
            txtVoornaam.Text = "";
            txtEmail.Text = "";
            txtNationaliteit.Text = "";
            dpGeboorte.Text = "";
            txtPlaats.Text = "";
            txtpNummer.Text = "";
            txtTelefoonnummer.Text = "";
        }
    }
}

