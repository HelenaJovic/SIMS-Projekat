using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
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
    /// Interaction logic for Guest2MainWindow.xaml
    /// </summary>
    public partial class Guest2MainWindow : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public static ObservableCollection<Tour> ToursMainList { get; set; }
        public static ObservableCollection<Tour> ToursCopyList { get; set; }
        public static ObservableCollection<TourReservation> ReservedTours { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedReservedTour { get; set; }
        public User LoggedInUser { get; set; }

        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly LocationRepository _locationRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Tour> tours { get; set; }

        public Guest2MainWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            _locationRepository = new LocationRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            ToursMainList = new ObservableCollection<Tour>(_tourRepository.GetAll());
            ToursCopyList = new ObservableCollection<Tour>(_tourRepository.GetAll());
            ReservedTours = new ObservableCollection<TourReservation>(_tourReservationRepository.GetByUser(user));
            Locations = new ObservableCollection<Location>();
            ReservedTours = new ObservableCollection<TourReservation>(_tourReservationRepository.GetByUser(user));
        }


        private void Button_Click_Filters(object sender, RoutedEventArgs e)
        {
            TourFiltering tourFiltering = new TourFiltering();
            tourFiltering.Show();
        }

        private void Button_Click_Restart(object sender, RoutedEventArgs e)
        {
            ToursMainList.Clear();
            foreach (Tour t in ToursCopyList)
            {
                ToursMainList.Add(t);
            }
        }

        private void Button_Click_Resrve(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedIndex == 0)
            {

                if (SelectedTour != null)
                {
                    ReserveTour resTour = new ReserveTour(SelectedTour, SelectedReservedTour, LoggedInUser);
                    resTour.Show();
                }
                else
                {
                    MessageBox.Show("Choose a tour which you can reserve");
                }
            }
        }

        private void Button_Click_Change(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedIndex == 1)
            {

                if (SelectedReservedTour != null)
                {
                    ReserveTour resTour = new ReserveTour(SelectedTour, SelectedReservedTour, LoggedInUser);
                    resTour.Show();
                }
                else
                {
                    MessageBox.Show("Choose a tour which you can change");
                }
            }
        }
        private void Button_Click_GiveUp(object sender, RoutedEventArgs e)
        {
            _tourReservationRepository.Delete(SelectedReservedTour);
            ReservedTours.Remove(SelectedReservedTour);
        }

        private void Button_Click_ViewGallery(object sender, RoutedEventArgs e)
        {
            ViewTourGallery viewTourGallery = new ViewTourGallery(SelectedTour);
            viewTourGallery.Show();
        }
    }
}
