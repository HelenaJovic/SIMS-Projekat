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

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for TourAttendence.xaml
    /// </summary>
    public partial class TourAttendence : Window
    {
        public TourAttendence()
        {
            InitializeComponent();
        }

        private void Button_Click_RateTour(object sender, RoutedEventArgs e)
        {
            RateTour rateTour = new RateTour();
            rateTour.Show();
        }

        private void Button_Click_CancelRaiting(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
