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
            lblInhoud.Content = tekst;
            this.ShowDialog();

        }

        public void Toon(string tekst, string titel)
        {
            lblInhoud.Content = tekst; 
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
        
        
    }
}
