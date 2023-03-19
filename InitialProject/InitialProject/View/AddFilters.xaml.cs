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
    /// Interaction logic for AddFilters.xaml
    /// </summary>
    public partial class AddFilters : Window
    {
        public AddFilters()
        {
            InitializeComponent();

        }

        private void Button_Click_Done(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
