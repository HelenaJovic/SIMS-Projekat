using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ReserveTour.xaml
    /// </summary>
    public partial class ReserveTour : Window
    {
        public Tour SelectedTour { get; set; }

        public static ObservableCollection<Tour> Tours { get; set; }
        public static ObservableCollection<Tour> ToursMainList { get; set; }
        public static ObservableCollection<Tour> ToursCopyList { get; set; }
        public static ObservableCollection<Tour> ReservedTours { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourRepository _tourRepository;
        public User LoggedInUser { get; set; }
        private string _guestNum;

        public string GuestNum
        {
            get => _guestNum;
            set
            {
                if (value != _guestNum)
                {
                    _guestNum = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ReserveTour(Tour tour, User user)
        {
            InitializeComponent();
            SelectedTour = tour;
            DataContext = this;
            LoggedInUser = user;
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();
        }

        private void Button_Click_FindTour(object sender, RoutedEventArgs e)
        {
            int max = 0;
            if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
            {
                return;
            }
            if (SelectedTour.FreeSetsNum - max >= 0 || txtGuestNum.Text.Equals(""))
            {
                SelectedTour.FreeSetsNum -= max;
                string TourName = _tourRepository.GetTourNameById(SelectedTour.Id);
                TourReservation newReservedTour = new TourReservation(SelectedTour.Id, TourName, LoggedInUser.Id, int.Parse(GuestNum), SelectedTour.FreeSetsNum, -1, LoggedInUser.Username);
                TourReservation savedReservedTour = _tourReservationRepository.Save(newReservedTour);
                Guest2MainWindow.ReservedTours.Add(savedReservedTour);
            }
            else
            {

            }

            Close();
        }

        private void Button_Click_CancelTour(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
