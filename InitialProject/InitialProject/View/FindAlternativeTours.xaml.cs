using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FindAlternativeTours.xaml
    /// </summary>
    public partial class FindAlternativeTours : Window
    {
        public User LoggedInUser { get; set; }
        public Tour SelectedTour { get; set; }
        public TourReservation TourReservation { get; set; }
        public Tour AlternativeTour { get; set; } 
        private string _againGuestNum;


        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourRepository _tourRepository;

        public string AgainGuestNum
        {
            get => _againGuestNum;
            set
            {
                if (value != _againGuestNum)
                {
                    _againGuestNum=value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public FindAlternativeTours(User user, Tour tour, TourReservation reservation)
        {
            InitializeComponent();
            DataContext=this;
            LoggedInUser = user;
            SelectedTour = tour;
            TourReservation = reservation;

            _tourRepository= new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
        }

        private void Button_Click_FindAlternativeTour(object sender, RoutedEventArgs e)
        {
            if (TourReservation != null)
            {
                TourReservation.FreeSetsNum += TourReservation.GuestNum;
                _tourReservationRepository.Delete(TourReservation);
                Guest2MainWindow.ReservedTours.Remove(TourReservation);
            }
            AlternativeTours alternativeTours = new AlternativeTours(LoggedInUser, SelectedTour, TourReservation,AgainGuestNum, AlternativeTour);
            alternativeTours.Show();
            Close();
        }

        private void Button_Click_CancelFindingAlternativeTour(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
