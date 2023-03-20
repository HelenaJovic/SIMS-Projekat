using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        private readonly TourRepository _tourRepository;
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            DataContext= this;
            LoggedInUser= user;
            _tourRepository= new TourRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetByUser(user));
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour(LoggedInUser);
            createTour.Show();
        }

        private void TourTracking_Click(object sender, RoutedEventArgs e)
        {
            TourTracking tourTracking= new TourTracking(LoggedInUser);
            tourTracking.Show();
        }
    }
}
