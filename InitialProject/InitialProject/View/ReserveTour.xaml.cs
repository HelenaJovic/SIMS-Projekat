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
    public partial class ReserveTour : Window, INotifyPropertyChanged
    {
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedReservation { get; set; }

        public Tour AlternativeTour { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourRepository _tourRepository;
        public User LoggedInUser { get; set; }
<<<<<<< HEAD
        
=======

>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
        private string _guestNum;

        public string GuestNum
        {
            get => _guestNum;
            set
            {
<<<<<<< HEAD
                if(value != _guestNum)
                {
                    _guestNum=value;
=======
                if (value != _guestNum)
                {
                    _guestNum = value;
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
                    OnPropertyChanged();
                }
            }
        }
<<<<<<< HEAD
        
=======

>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ReserveTour(Tour tour, TourReservation reserve, User user)
        {
            InitializeComponent();
            SelectedTour = tour;
            SelectedReservation = reserve;
<<<<<<< HEAD
            DataContext =this;
            LoggedInUser= user;
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();
            
=======
            DataContext = this;
            LoggedInUser = user;
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();

>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
        }

        private void Button_Click_FindTour(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                int max = 0;
<<<<<<< HEAD
                if(!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
=======
                if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
                {
                    return;
                }
                SelectedReservation.FreeSetsNum += SelectedReservation.GuestNum;
                if (SelectedReservation.FreeSetsNum - max >= 0 || txtGuestNum.Text.Equals(""))
                {
                    UpdateSelectedReservation(max);
                }
<<<<<<< HEAD
                else 
                {
                    
=======
                else
                {

>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
                    MessageBox.Show("Find alternative tours because there isn't enaough room for that number of guest");
                    FindAlternativeTours findAlternative = new FindAlternativeTours(LoggedInUser, SelectedTour, SelectedReservation);
                    findAlternative.Show();
                }
            }
<<<<<<< HEAD
            else 
=======
            else
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
            {
                int max = 0;
                if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
                {
                    return;
                }
                if (SelectedTour.FreeSetsNum - max >= 0 || txtGuestNum.Text.Equals(""))
                {
                    ReserveSelectedTour(max);
                }
                else
                {
                    MessageBox.Show("Find alternative tours because there isn't enaough room for that number of guest");
                    FindAlternativeTours findAlternative = new FindAlternativeTours(LoggedInUser, SelectedTour, SelectedReservation);
                    findAlternative.Show();
                }
            }
            Close();
        }

        private void ReserveSelectedTour(int max)
        {
            SelectedTour.FreeSetsNum -= max;
            string TourName = _tourRepository.GetTourNameById(SelectedTour.Id);
<<<<<<< HEAD
            TourReservation newReservedTour = new TourReservation(SelectedTour.Id, TourName, LoggedInUser.Id, int.Parse(GuestNum), SelectedTour.FreeSetsNum);
=======
            TourReservation newReservedTour = new TourReservation(SelectedTour.Id, TourName, LoggedInUser.Id, int.Parse(GuestNum), SelectedTour.FreeSetsNum, -1, LoggedInUser.Username);
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346

            TourReservation savedReservedTour = _tourReservationRepository.Save(newReservedTour);
            Guest2MainWindow.ReservedTours.Add(savedReservedTour);
        }

        private void UpdateSelectedReservation(int max)
        {
<<<<<<< HEAD
            SelectedReservation.GuestNum= max;
            SelectedReservation.FreeSetsNum -= max;
            _tourReservationRepository.Update(SelectedReservation);
            Guest2MainWindow.ReservedTours.Clear();
            
=======
            SelectedReservation.GuestNum = max;
            SelectedReservation.FreeSetsNum -= max;
            _tourReservationRepository.Update(SelectedReservation);
            Guest2MainWindow.ReservedTours.Clear();

>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
            foreach (TourReservation tour in _tourReservationRepository.GetAll())
            {
                Guest2MainWindow.ReservedTours.Add(tour);
            }
        }

        private void Button_Click_CancelTour(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
