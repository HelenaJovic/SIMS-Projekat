using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class MenuWindowGuest2ViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        public Tour tour { get; set; }
        public TourReservation tourReservation { get; set; }
        public TourRequest tourRequest { get; set; }
        public Action CloseAction { get; set; }
        public UserControl CurrentUserControl { get; set; }
        public ICommand ToursCommand { get; set; }
        public ICommand ReservationsCommand { get; set; }
        public ICommand TourRequestsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand StatisticsCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }
        public ICommand CheckNotificationsCommand { get; set; }
        public ICommand MyAccountCommand { get; set; }

        private readonly TourService _tourService;
        private readonly TourAttendanceService _tourAttendanceService;
        private int brojac;

        public MenuWindowGuest2ViewModel(User guest)
        {
            LoggedInUser=guest;
            var toursViewModel = new ToursViewModel(guest);
            CurrentUserControl = new ToursGuest2(guest, toursViewModel);
            _tourService = new TourService();
            _tourAttendanceService = new TourAttendanceService();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ToursCommand = new RelayCommand(Execute_ToursCommand, CanExecute_Command);
            ReservationsCommand = new RelayCommand(Execute_ReservationsCommand, CanExecute_Command);
            TourRequestsCommand = new RelayCommand(Execute_TourRequestsCommand, CanExecute_Command);
            VouchersCommand = new RelayCommand(Execute_VouchersCommand, CanExecute_Command);
            StatisticsCommand = new RelayCommand(Execute_StatisticsCommand, CanExecute_Command);
            ActiveTourCommand =new RelayCommand(Execute_ActiveTourCommand, CanExecute_Command);
            TourAttendenceCommand = new RelayCommand(Execute_TourAttendenceCommand, CanExecute_Command);
            CheckNotificationsCommand =  new RelayCommand(Execute_CheckNotificationsCommand, CanExecute_Command);
            MyAccountCommand =new RelayCommand(Execute_MyAccountCommand, CanExecute_Command);
        }

        private void Execute_StatisticsCommand(object obj)
        {
            var tourStatisticsGuest2ViewModel = new TourStatisticsGuest2ViewModel(LoggedInUser);
            CurrentUserControl.Content = new TourStatisticsGuest2(LoggedInUser, tourStatisticsGuest2ViewModel);

        }

        private void Execute_TourRequestsCommand(object obj)
        {
            var tourRequestsViewModel = new TourRequestsViewModel(LoggedInUser, tourRequest);
            CurrentUserControl.Content = new TourRequests(LoggedInUser, tourRequestsViewModel);

            tourRequestsViewModel.CreateTourRequest += OnCreateTourRequest;
        }

        private void OnCreateTourRequest()
        {
            CreateTourRequest createTourRequest = new CreateTourRequest(LoggedInUser);
            createTourRequest.Show();
        }

        private void Execute_MyAccountCommand(object obj)
        {
            var guest2AccountViewModel = new Guest2AccountViewModel(LoggedInUser);
            CurrentUserControl.Content = new Guest2Account(LoggedInUser, guest2AccountViewModel);

            guest2AccountViewModel.LogOutEvent += OnLogOutEvent;

        }

        private void OnLogOutEvent()
        {
            //ZATVARANJE PROZORA
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MenuWindowGuest2)
                {
                    window.Close();
                }
            }
        }

        private void Execute_CheckNotificationsCommand(object obj)
        {
            int brojac = 0;
            Tour activ = new Tour();
            GetCurrentActiveTour(ref brojac, ref activ);
            NewNotification(brojac, activ);
        }

        private void NewNotification(int brojac, Tour activ)
        {
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
                var activeTourViewModel = new ActiveTourViewModel(LoggedInUser, brojac);
                CurrentUserControl.Content = new ActiveTour(LoggedInUser,brojac ,activeTourViewModel);
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


        private void Execute_TourAttendenceCommand(object obj)
        {
            var tourAttendanceViewModel = new TourAttendenceViewModel(LoggedInUser);
            CurrentUserControl.Content = new TourAttendence(LoggedInUser, tourAttendanceViewModel);
        }

        private void Execute_ActiveTourCommand(object obj)
        {
            var activeTourViewModel = new ActiveTourViewModel(LoggedInUser, brojac);
            CurrentUserControl.Content = new ActiveTour(LoggedInUser, brojac, activeTourViewModel);

            activeTourViewModel.ConfirmEvent += OnConfirmEvent;
        }

        private void OnConfirmEvent()
        {
            var tourAttendanceViewModel = new TourAttendenceViewModel(LoggedInUser);
            CurrentUserControl.Content = new TourAttendence(LoggedInUser, tourAttendanceViewModel);
        }

        private void Execute_VouchersCommand(object obj)
        {
            var tourVouchersViewModel = new TourVouchersViewModel(LoggedInUser, tour, tourReservation);
            CurrentUserControl.Content = new TourVouchers(LoggedInUser, tour,tourReservation, tourVouchersViewModel);

        }

        private void Execute_ReservationsCommand(object obj)
        {
            var tourReservationsViewModel = new TourReservationsViewModel(LoggedInUser);
            CurrentUserControl.Content = new TourReservations(LoggedInUser, tourReservationsViewModel);
        }

        
        private void Execute_ToursCommand(object obj)
        {
            var toursViewModel = new ToursViewModel(LoggedInUser);
            CurrentUserControl.Content = new ToursGuest2(LoggedInUser, toursViewModel);

            toursViewModel.ReserveEvent += OnReserve;
            toursViewModel.AddFiltersEvent += OnAddFilter;
        }


        private void OnAddFilter()
        {
            TourFiltering tourFiltering = new TourFiltering();
            tourFiltering.Show();
        }


        private void OnReserve()
        {
            ReserveTour resTour = new ReserveTour(tour, tourReservation, LoggedInUser);
            resTour.Show();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

    }
}
