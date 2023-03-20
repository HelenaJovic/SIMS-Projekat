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
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetByUserAndTime(user));
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour(LoggedInUser);
            createTour.Show();
        }

        private void TourTracking_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            TourTracking tourTracking= new TourTracking(LoggedInUser);
=======
            TourTracking tourTracking = new TourTracking(LoggedInUser);
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
            tourTracking.Show();
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                AddDate addDate = new AddDate(SelectedTour);
                addDate.Show();
            }
            else
                MessageBox.Show("Choose a tour which you want to multiply");
            
        }

        private void ViewGallery_Click(object sender, RoutedEventArgs e)
        {
            ViewTourGallery viewTourGallery = new ViewTourGallery(SelectedTour);
            viewTourGallery.Show();
        }
    }
}
