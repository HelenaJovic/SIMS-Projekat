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
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTodayTour { get; set; }
        public User LoggedInUser { get; set; }
<<<<<<< HEAD
=======
        public int MaxOrder { get; set; }

>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
        private readonly TourRepository _tourRepository;
        public TourTracking(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();
            TodayTours = new ObservableCollection<Tour>(_tourRepository.GetAllByUserAndDate(user, DateTime.Now));
        }

        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if(SelectedTodayTour != null)
            {
                _tourRepository.StartTour(SelectedTodayTour);
                TourPoints tourPoints = new TourPoints(SelectedTodayTour);
                tourPoints.Show();
                //disable button za start ostalih
                
            }
            else
            {
                MessageBox.Show("Choose a tour which you want to start");
            }
=======
            if (SelectedTodayTour != null)
            {
                if (_tourRepository.IsUserAvaliable(LoggedInUser))
                {
                    _tourRepository.StartTour(SelectedTodayTour);
                    TourPoints tourPoints = new TourPoints(SelectedTodayTour);
                    tourPoints.Show();
                }
                else
                    MessageBox.Show("Other tour already started at the same time");
            }
            else
                MessageBox.Show("Choose a tour which you want to start");
            
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
        }
    }
}
