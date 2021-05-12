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
            //lblVluchtDetails.Content = "";
            //List<Luchthaven> lhs = DatabaseOperations.VluchtInformatie();
            //foreach (Luchthaven lh in lhs)
            //{
                //lblVluchtDetails.Content += lh.land;
            //}
        }
    }
}
