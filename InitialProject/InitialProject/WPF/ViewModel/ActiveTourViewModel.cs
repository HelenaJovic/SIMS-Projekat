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
        public ICommand VouchersCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }
        public Tour ActiveTour { get; set; }
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;

        public ActiveTourViewModel(User user) 
        {
            InitializeCommands();
            LoggedUser = user;
            _tourService= new TourService();
            _tourReservationService = new TourReservationService();
            ActiveTour = _tourService.GetActiveTour(user);
        }

        private void InitializeCommands()
        {
            ConfirmAttendenceCommand = new RelayCommand(Execute_ConfirmAttendenceCommand, CanExecute_Command);
            CancelCommand =  new RelayCommand(Execute_CancelCommand, CanExecute_Command);
            ToursCommand = new RelayCommand(Execute_ToursCommand, CanExecute_Command);
            VouchersCommand = new RelayCommand(Execute_VouchersCommand, CanExecute_Command);
            ActiveTourCommand =  new RelayCommand(Execute_ActiveTourCommand, CanExecute_Command);
            TourAttendenceCommand = new RelayCommand(Execute_TourAttendenceCommand, CanExecute_Command);
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
            ActiveTour activeTour = new ActiveTour(LoggedUser);
            activeTour.Show();
            CloseAction();

        }

        private void Execute_VouchersCommand(object obj)
        {
            TourVouchers tourVouchers = new TourVouchers(LoggedUser);
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
