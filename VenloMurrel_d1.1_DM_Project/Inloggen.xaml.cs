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
    /// Interaction logic for Inloggen.xaml
    /// </summary>
    public partial class Inloggen : Window
    {
        public Inloggen()
        {
            InitializeComponent();
        }

        private void BtnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            PassagierWindow passagierWindow = new PassagierWindow();
            passagierWindow.Show();
        }

        private void BtnAanmelden_Click(object sender, RoutedEventArgs e)
        {
            
            string foutmeldingen = ValideerGegevens();

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Reservering rv = new Reservering();
                Passagier pg = new Passagier();
                pg.achternaam = txtAchternaam.Text;
                rv.boekingscode = txtpassagierId.Text;

                var pn = DatabaseOperations.PassagierMetNaam(txtAchternaam.Text);
                
                if (pn != null )
                {
                    var rc = DatabaseOperations.PassagierMetNaam(txtpassagierId.Text);
                    if (rc != null)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("De opgegeven gebruikersnaam en boekingsreferentie komen niet overeen!", "foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Deze gebruikersnaam bestaat niet!", "foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmeldingen, "foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ValideerGegevens()
        {
            string foutmeldingen = "";

            if (string.IsNullOrWhiteSpace(txtAchternaam.Text))
            {
                foutmeldingen += "Uw achternaam is vereist!" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtpassagierId.Text))
            {
                foutmeldingen += "Uw boekingscode is vereist!" + Environment.NewLine;
            }
            return foutmeldingen;
        }
    }
}
