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

namespace VenloMurrel_d1._1_DM_Project
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        public void Toon(string tekst)
        {
            
            lblInhoud.Content = BerichtAanpassen(tekst);
            this.ShowDialog();

        }

        public void Toon(string tekst, string titel)
        {
            
            lblInhoud.Content = BerichtAanpassen(tekst);
            this.Title = titel; 
            this.ShowDialog();

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Succes()
        {
            lblInhoud.Foreground = Brushes.Green;
            
        }
        public void Fail()
        {
            lblInhoud.Foreground = Brushes.Red;
            btnOk.Background = Brushes.Red;
            
        }
        //om de content er niet uit te laten vallen;
        public string BerichtAanpassen(string bericht)
        {
            int count = 0; //Startpositie --> zolang het bericht groter of gelijk is aan de count: positie verhogen

            while (count <= bericht.Length)
            {
                if (bericht.Length > 30)
                {
                    count += 30;
                }
                else if (bericht.Length > 60)
                {
                    count += 60;
                }
                else { count += 90; }
                if (count > bericht.Length) //Als de count tocht groter is dan het bericht, wordt de lus verbroken
                {
                    break;
                }
                /*   bericht = bericht.Insert(count, "\n");*/ //bericht gelijkstellen aan het nieuwe bericht met nieuwe regel op de positie van count
            }
            return bericht;
        }
    }
}
