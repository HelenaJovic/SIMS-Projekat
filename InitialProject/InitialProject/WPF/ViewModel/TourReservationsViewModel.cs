using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourReservationsViewModel : ViewModelBase
    {
        public static ObservableCollection<TourReservation> ReservedTours { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedReservedTour { get; set; }
        public User LoggedInUser { get; set; }
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private readonly UserService _userService;
        private readonly TourAttendanceService _tourAttendanceService;
        private readonly IMessageBoxService _messageBoxService;
        public TourPoint CurrentPoint { get; set; }
        public Tour ActiveTour { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public Action CloseAction { get; set; }
        public List<Tour> tours { get; set; }

        public ICommand ToursCommand { get; set; }
        public ICommand ReservationsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }
        public ICommand CheckNotificationsCommand { get; set; }
        public ICommand ChangeGuestNumCommand { get; set; }
        public ICommand GiveUpReservationCommand { get; set; }
        public ICommand MyAccountCommand { get; set; }


        public TourReservationsViewModel(User user)
        {
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _userService = new UserService();
            _tourAttendanceService = new TourAttendanceService();
            _messageBoxService = new MessageBoxService();
            InitializeProperties(user);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ToursCommand = new RelayCommand(Execute_ToursCommand, CanExecute_Command);
            ReservationsCommand = new RelayCommand(Execute_ReservationsCommand, CanExecute_Command);
            VouchersCommand = new RelayCommand(Execute_VouchersCommand, CanExecute_Command);
            ActiveTourCommand =new RelayCommand(Execute_ActiveTourCommand, CanExecute_Command);
            TourAttendenceCommand = new RelayCommand(Execute_TourAttendenceCommand, CanExecute_Command);
            CheckNotificationsCommand =  new RelayCommand(Execute_CheckNotificationsCommand, CanExecute_Command);
            GiveUpReservationCommand =  new RelayCommand(Execute_GiveUpReservationCommand, CanExecute_Command);
            ChangeGuestNumCommand =new RelayCommand(Execute_ChangeGuestNumCommand, CanExecute_Command);
            MyAccountCommand =new RelayCommand(Execute_MyAccountCommand, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void InitializeProperties(User user)
        {
            LoggedInUser = user;
            ReservedTours = new ObservableCollection<TourReservation>(_tourReservationService.GetByUser(user));
            Locations = new ObservableCollection<Location>();
        }

        private void Execute_ReservationsCommand(object obj)
        {
            TourReservations tourReservations = new TourReservations(LoggedInUser);
            tourReservations.Show();
            CloseAction();
        }


        private void Execute_MyAccountCommand(object obj)
        {
            Guest2Account guest2Account = new Guest2Account(LoggedInUser);
            guest2Account.Show();
            CloseAction();
        }

        private void Execute_CheckNotificationsCommand(object obj)
        {
            int brojac = 0;
            User user = _userService.GetByUsername(LoggedInUser.Username);
            Tour activ = new Tour();
            GetCurrentActiveTour(ref brojac, ref activ);

            string message = LoggedInUser.Username + " are you present at current active tour " + activ.Name + "?";
            string title = "Confirmation window";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            MessageBoxResult(brojac, activ, result);
        }

        private void MessageBoxResult(int brojac, Tour activ, MessageBoxResult result)
        {
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                ActiveTour activeTour = new ActiveTour(LoggedInUser, brojac);
                activeTour.Show();
                CloseAction();
            }
            else
            {
                DeleteFromAttendedTours(activ);

            }
        }

        private void DeleteFromAttendedTours(Tour activ)
        {
            foreach (TourAttendance tA in _tourAttendanceService.GetAllAttendedToursByUser(LoggedInUser))
            {
                if (activ.Id ==  tA.IdTour)
                {
                    _tourAttendanceService.Delete(tA);
                }
            }
        }

        private void GetCurrentActiveTour(ref int brojac, ref Tour activ)
        {
            foreach (TourAttendance tourAttendence in _tourAttendanceService.GetAllAttendedToursByUser(LoggedInUser))
            {
                Tour tour = _tourService.GetById(tourAttendence.IdTour);
                if (tour.Active==true)
                {
                    brojac =1;
                    activ=tour;
                }
            }
        }

        private void Execute_ToursCommand(object obj)
        {

            Guest2MainWindow guest2MainWindow = new Guest2MainWindow(LoggedInUser);
            guest2MainWindow.Show();
            CloseAction();
        }

        private void Execute_VouchersCommand(object obj)
        {

            TourVouchers tourVouchers = new TourVouchers(LoggedInUser, null);
            tourVouchers.Show();
            CloseAction();

        }

        private void Execute_ChangeGuestNumCommand(object obj)
        {
            if (SelectedReservedTour != null)
            {
                ReserveTour resTour = new ReserveTour(SelectedTour, SelectedReservedTour, LoggedInUser);
                resTour.Show();
            }
            else
            {
                _messageBoxService.ShowMessage("Choose a tour which you can change");
            }
        }

        private void Execute_GiveUpReservationCommand(object obj)
        {
            _tourReservationService.Delete(SelectedReservedTour);
            ReservedTours.Remove(SelectedReservedTour);
        }

        private void Execute_TourAttendenceCommand(object obj)
        {
            TourAttendence tourAttendance = new TourAttendence(LoggedInUser);
            tourAttendance.Show();
            CloseAction();
        }

        private void Execute_ActiveTourCommand(object obj)
        {
            ActiveTour activeTour = new ActiveTour(LoggedInUser, 0);
            activeTour.Show();
            CloseAction();
        }
    }
}
