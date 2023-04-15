using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class ActiveTourViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        public static User LoggedUser { get; set; }
        public ICommand ConfirmAttendenceCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ToursCommand { get; set; }
        public ICommand ReservationsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }
        public ICommand CheckNotificationsCommand { get; set; }
        public ICommand MyAccountCommand { get; set; }
        

        public int brojac = 0;
        public static ObservableCollection<Tour> ActiveTour { get; set; }
        public static ObservableCollection<TourPoint> TourPoints { get; set; }

        private readonly TourService _tourService;
        private readonly TourPointService _tourPointService;
        private readonly TourAttendanceService _tourAttendanceService;
        private readonly UserService _userService;

        public ActiveTourViewModel(User user, int brojac)
        {
            InitializeCommands();
            LoggedUser = user;
            _tourService= new TourService();
            _tourPointService = new TourPointService();
            _tourAttendanceService= new TourAttendanceService();
            _userService= new UserService();
            if (brojac==1)
            {
                ActiveTour = new ObservableCollection<Tour>(_tourService.GetActiveTour());
                TourPoints = new ObservableCollection<TourPoint>(_tourPointService.GetAllByTourId(ActiveTour[0].Id));
            }
            
        }


        private void InitializeCommands()
        {
            ConfirmAttendenceCommand = new RelayCommand(Execute_ConfirmAttendenceCommand, CanExecute_Command);
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
            foreach (TourAttendance tA in _tourAttendanceService.GetAllAttendedToursByUser(LoggedUser))
            {
                if (activ.Id ==  tA.IdTour)
                {
                    _tourAttendanceService.Delete(tA);
                }
            }
        }

        private void GetCurrentActiveTour(ref int brojac, ref Tour activ)
        {
            foreach (TourAttendance tourAttendence in _tourAttendanceService.GetAllAttendedToursByUser(LoggedUser))
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
            Guest2MainWindow guest2MainWindow = new Guest2MainWindow(LoggedUser);
            guest2MainWindow.Show();
            CloseAction();
        }

        private void Execute_ConfirmAttendenceCommand(object obj)
        {
            TourAttendence tourAttendance = new TourAttendence(LoggedUser);
            tourAttendance.Show();
            CloseAction();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
