using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.UseCases;
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
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTodayTour { get; set; }
        public User LoggedInUser { get; set; }
        public int MaxOrder { get; set; }

        private readonly TourService _tourService;

        public TourTracking(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourService = new TourService();
            TodayTours = new ObservableCollection<Tour>(_tourService.GetAllByUserAndDate(user, DateTime.Now));
        }

        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTodayTour != null)
            {
                if (_tourService.IsUserAvaliable(LoggedInUser))
                {
                    _tourService.StartTour(SelectedTodayTour);
                    TourPoints tourPoints = new TourPoints(SelectedTodayTour);
                    tourPoints.Show();
                }
                else
                    MessageBox.Show("Other tour already started at the same time");
            }
            else
                MessageBox.Show("Choose a tour which you want to start");
            
        }
    }
}
