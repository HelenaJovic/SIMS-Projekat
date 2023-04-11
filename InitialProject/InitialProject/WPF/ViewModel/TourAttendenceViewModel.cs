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
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourAttendenceViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        public static ObservableCollection<TourAttendance> ToursAttended { get; set; }
        public TourAttendance SelectedAttendedTour { get; set; }
        public User LoggedUser { get; set; }
        private readonly TourAttendenceService _tourAttendenceService;
        private readonly TourService _tourService;
        public ICommand RateTourCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ToursCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }

        public TourAttendenceViewModel(User user)
        { 
            LoggedUser =user;
            _tourAttendenceService = new TourAttendenceService();
            ToursAttended =  new ObservableCollection<TourAttendance>(_tourAttendenceService.GetAllAttendedTours(user));
            _tourService = new TourService();
            InitializeCommands();
            BindData();
        }

        private void BindData()
        {
            foreach(TourAttendance tourAttendance in ToursAttended)
            {
                tourAttendance.Tour = _tourService.GetById(tourAttendance.Id);
            }
        }

        private void InitializeCommands()
        {
            RateTourCommand = new RelayCommand(Execute_RateTourCommand, CanExecute_Command);
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
            CloseAction();
        }

        private void Execute_RateTourCommand(object obj)
        {
            RateTour rateTour = new RateTour(LoggedUser, SelectedAttendedTour);
            rateTour.Show();
            CloseAction();

        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
