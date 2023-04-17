using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourAttendenceViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        public static ObservableCollection<TourAttendance> ToursAttended { get; set; }
        public TourAttendance SelectedAttendedTour { get; set; }
        public static string TourPointName { get; set; }
        public User LoggedUser { get; set; }
        private readonly TourAttendanceService _tourAttendenceService;
        private readonly TourService _tourService;
        private readonly TourPointService _tourPointService;
        private readonly UserService _userService;
        public ICommand RateTourCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ToursCommand { get; set; }
        public ICommand ReservationsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }
        public ICommand CheckNotificationsCommand { get; set; }
        public ICommand MyAccountCommand { get; set; }

        public TourAttendenceViewModel(User user)
        {
            LoggedUser =user;
            _tourAttendenceService = new TourAttendanceService();
            ToursAttended =  new ObservableCollection<TourAttendance>(_tourAttendenceService.GetAllAttendedToursByUser(user));
            _tourService = new TourService();
            _tourPointService = new TourPointService();
            _userService = new UserService();
            InitializeCommands();
            BindData();
        }

        private void BindData()
        {
            foreach (TourAttendance tourAttendance in ToursAttended)
            {
                tourAttendance.Tour = _tourService.GetById(tourAttendance.Id);
                tourAttendance.TourPointName = _tourPointService.GetTourPointNameByTourPointId(tourAttendance.IdTourPoint);
            }
        }

        private void InitializeCommands()
        {
            RateTourCommand = new RelayCommand(Execute_RateTourCommand, CanExecute_Command);
            CancelCommand =  new RelayCommand(Execute_CancelCommand, CanExecute_Command);
            ToursCommand = new RelayCommand(Execute_ToursCommand, CanExecute_Command);
            ReservationsCommand = new RelayCommand(Execute_ReservationsCommand, CanExecute_Command);
            VouchersCommand = new RelayCommand(Execute_VouchersCommand, CanExecute_Command);
            ActiveTourCommand =  new RelayCommand(Execute_ActiveTourCommand, CanExecute_Command);
            TourAttendenceCommand = new RelayCommand(Execute_TourAttendenceCommand, CanExecute_Command);
            CheckNotificationsCommand = new RelayCommand(Execute_CheckNotificationsCommand, CanExecute_Command);
            MyAccountCommand = new RelayCommand(Execute_MyAccountCommand, CanExecute_Command);
        }

        private void Execute_ReservationsCommand(object obj)
        {
            TourReservations tourReservations = new TourReservations(LoggedUser);
            tourReservations.Show();
            CloseAction();
        }

        private void Execute_MyAccountCommand(object obj)
        {
            Guest2Account guest2Account = new Guest2Account(LoggedUser);
            guest2Account.Show();
            CloseAction();
        }

        private void Execute_CheckNotificationsCommand(object obj)
        {
            int brojac = 0;
            User user = _userService.GetByUsername(LoggedUser.Username);
            Tour activ = new Tour();
            GetCurrentActiveTour(ref brojac, ref activ);

            string message = LoggedUser.Username + " are you present at current active tour " + activ.Name + "?";
            string title = "Confirmation window";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            MessageBoxResult(brojac, activ, result);
        }

        private void MessageBoxResult(int brojac, Tour activ, MessageBoxResult result)
        {
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                ActiveTour activeTour = new ActiveTour(LoggedUser, brojac);
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
            foreach (TourAttendance tA in _tourAttendenceService.GetAllAttendedToursByUser(LoggedUser))
            {
                if (activ.Id ==  tA.IdTour)
                {
                    _tourAttendenceService.Delete(tA);
                }
            }
        }

        private void GetCurrentActiveTour(ref int brojac, ref Tour activ)
        {
            foreach (TourAttendance tourAttendence in _tourAttendenceService.GetAllAttendedToursByUser(LoggedUser))
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
            Guest2MainWindow guest2MainWindow = new Guest2MainWindow(LoggedUser);
            guest2MainWindow.Show();
            CloseAction();
        }
        private void Execute_TourAttendenceCommand(object obj)
        {
            TourAttendence tourAttendance = new TourAttendence(LoggedUser);
            tourAttendance.Show();
            CloseAction();

        }

        private void Execute_ActiveTourCommand(object obj)
        {
          
            ActiveTour activeTour = new ActiveTour(LoggedUser, 0);
            activeTour.Show();
            CloseAction();
            
        }

        private void Execute_VouchersCommand(object obj)
        {
            
            TourVouchers tourVouchers = new TourVouchers(LoggedUser, null);
            tourVouchers.Show();
            CloseAction();
            
        }

        private void Execute_CancelCommand(object obj)
        {
            CloseAction();
        }

        private void Execute_RateTourCommand(object obj)
        {
            RateTour rateTour = new RateTour(LoggedUser, SelectedAttendedTour);
            rateTour.Show();

        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
