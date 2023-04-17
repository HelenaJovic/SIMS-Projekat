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
    internal class Guest2MainWindowViewModel : ViewModelBase
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public static ObservableCollection<Tour> ToursMainList { get; set; }
        public static ObservableCollection<Tour> ToursCopyList { get; set; }
        //public static ObservableCollection<TourAttendance> ToursAttended { get; set; }
        public static ObservableCollection<TourReservation> ReservedTours { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedReservedTour { get; set; }
        public User LoggedInUser { get; set; }
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private readonly LocationRepository _locationRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Tour> tours { get; set; }

        public ICommand ReserveTourCommand { get; set; }
        public ICommand ViewTourGalleryCommand { get; set; }
        public ICommand AddFiltersCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }
        public ICommand ChangeGuestNumCommand { get; set; }
        public ICommand GiveUpReservationCommand { get; set; }

        public Guest2MainWindowViewModel(User user)
        {
            _tourReservationService= new TourReservationService();
            _tourService = new TourService();
            _locationRepository = new LocationRepository();
            InitializeProperties(user);
            InitializeCommands();
        }

        private void InitializeProperties(User user)
        {
            LoggedInUser = user;
            Tours = new ObservableCollection<Tour>(_tourService.GetUpcomingToursByUser(user));
            ToursMainList = new ObservableCollection<Tour>(_tourService.GetUpcomingToursByUser(user));
            ToursCopyList = new ObservableCollection<Tour>(_tourService.GetUpcomingToursByUser(user));
            ReservedTours = new ObservableCollection<TourReservation>(_tourReservationService.GetByUser(user));
            Locations = new ObservableCollection<Location>();
            ReservedTours = new ObservableCollection<TourReservation>(_tourReservationService.GetByUser(user));
            //ToursAttended = new ObservableCollection<TourAttendance>();
        }

        private void InitializeCommands()
        {
            ReserveTourCommand = new RelayCommand(Execute_ReserveTourCommand, CanExecute_Command);
            AddFiltersCommand =  new RelayCommand(Execute_AddFiltersCommand, CanExecute_Command);
            ViewTourGalleryCommand = new RelayCommand(Execute_ViewTourGalleryCommand, CanExecute_Command);
            RestartCommand = new RelayCommand(Execute_RestartCommand, CanExecute_Command);
            ActiveTourCommand =new RelayCommand(Execute_ActiveTourCommand, CanExecute_Command);
            TourAttendenceCommand = new RelayCommand(Execute_TourAttendenceCommand, CanExecute_Command);
            GiveUpReservationCommand =  new RelayCommand(Execute_GiveUpReservationCommand, CanExecute_Command);
            ChangeGuestNumCommand =new RelayCommand(Execute_ChangeGuestNumCommand, CanExecute_Command);
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
                MessageBox.Show("Choose a tour which you can change");
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
        }

        

        private void Execute_ActiveTourCommand(object obj)
        {
            ActiveTour activeTour = new ActiveTour();
            activeTour.Show();
        }

        private void Execute_RestartCommand(object obj)
        {
            ToursMainList.Clear();
            foreach (Tour t in ToursCopyList)
            {
                t.Location = _locationRepository.GetById(t.IdLocation);
                ToursMainList.Add(t);
            }
        }

        private void Execute_ViewTourGalleryCommand(object obj)
        {
            if (SelectedTour != null)
            {
              /*ViewTourGallery viewTourGallery = new ViewTourGallery(SelectedTour);
                viewTourGallery.Show();*/
            }
            else
            {
                MessageBox.Show("Choose a tour which you want to see");
            }
        }

        private void Execute_AddFiltersCommand(object obj)
        {
            TourFiltering tourFiltering = new TourFiltering();
            tourFiltering.Show();
        }

        private void Execute_ReserveTourCommand(object obj)
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

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
