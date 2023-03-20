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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateReservation.xaml
    /// </summary>
    public partial class CreateReservation : Window
    {
        public CreateReservation()
        {
            InitializeComponent();
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelCreate_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
