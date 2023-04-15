using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Applications.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InitialProject.WPF.ViewModel;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window
    {
        public TourTracking(User user)
        {
            this.Width = 430;
            this.Height = 750;
            InitializeComponent();
            DataContext = new TourTrackingViewModel(user);
        }

    }
}
