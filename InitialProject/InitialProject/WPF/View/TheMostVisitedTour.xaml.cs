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
using InitialProject.Domain.Model;
using InitialProject.WPF.ViewModel;

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for TheMostVisited.xaml
    /// </summary>
    public partial class TheMostVisitedTour : Window
    {
        public TheMostVisitedTour(User user)
        {
            this.Width = 430;
            this.Height = 750;
            InitializeComponent();
            DataContext = new TheMostVisitedTourViewModel(user);
        }

    }
}
