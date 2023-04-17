using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
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
    public class AlternativeToursViewModel : ViewModelBase
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public static ObservableCollection<Tour> AlternativeToursMainList { get; set; }
        public static ObservableCollection<Tour> AlternativeToursCopyList { get; set; }
        public User LoggedInUser { get; set; }
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedTourReservation { get; set; }
        public Tour SelectedAlternativeTour { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _tourReservationRepository;

        public Action CloseAction { get; set; }
        private string AgainGuestNum { get; set; }

        public ICommand ReserveAlternativeCommand { get; set; }
        public ICommand ViewGalleryCommand { get; set; }
        public ICommand AlternativeFilteringCommand { get; set; }
        public ICommand RestartCommand { get; set; }

        public AlternativeToursViewModel(User user, Tour tour, TourReservation tourReservation, string againGuestNum, Tour alternativeTour)
        {
            LoggedInUser = user;
            SelectedTour = tour;
            SelectedTourReservation = tourReservation;
            AgainGuestNum = againGuestNum;
            SelectedAlternativeTour = alternativeTour;
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetByUser(user));
            AlternativeToursMainList = new ObservableCollection<Tour>();
            AlternativeToursCopyList = new ObservableCollection<Tour>(_tourRepository.GetAll());
            Locations = new ObservableCollection<Location>();
            InitializeCommands();

            foreach (Tour tours in AlternativeToursCopyList)
            {
                if (SelectedTourReservation != null)
                {
                    ReservedAlternativeTourList(tours);
                }
                else
                {
                    AlternativeTourList(tours);
                }

            }

            AlternativeToursCopyList.Clear();

            foreach (Tour t in AlternativeToursMainList)
            {
                AlternativeToursCopyList.Add(t);
            }
        }

        private void InitializeCommands()
        {
            ReserveAlternativeCommand = new RelayCommand(Execute_ReserveAlternativeCommand, CanExecute_Command);
            ViewGalleryCommand =  new RelayCommand(Execute_ViewGalleryCommand, CanExecute_Command);
            AlternativeFilteringCommand = new RelayCommand(Execute_AlternativeFilteringCommand, CanExecute_Command);
            RestartCommand =  new RelayCommand(Execute_RestartCommand, CanExecute_Command);
        }

        private void Execute_RestartCommand(object obj)
        {
            AlternativeToursMainList.Clear();
            foreach (Tour t in AlternativeToursCopyList)
            {
                AlternativeToursMainList.Add(t);
            }
        }

        private void Execute_AlternativeFilteringCommand(object obj)
        {
            AlternativeTourFiltering filterAlternativeTour = new AlternativeTourFiltering();
            filterAlternativeTour.Show();
        }

        private void Execute_ViewGalleryCommand(object obj)
        {
            ViewTourGalleryGuide viewTourGallery = new ViewTourGalleryGuide(SelectedTour);
            viewTourGallery.Show();
        }

        private void Execute_ReserveAlternativeCommand(object obj)
        {
                if (SelectedAlternativeTour != null)
                {
                    ReserveAlternativeTour();
                }
                else
                {
                    MessageBox.Show("Choose a tour which you can reserve");
                }
            CloseAction();
        }

        private void ReserveAlternativeTour()
        {
            if (SelectedAlternativeTour.FreeSetsNum - int.Parse(AgainGuestNum) >= 0 || AgainGuestNum.Equals(""))
            {
                SelectedAlternativeTour.FreeSetsNum -= int.Parse(AgainGuestNum);
                string TourName = _tourRepository.GetTourNameById(SelectedAlternativeTour.Id);
                TourReservation newAlternativeTour = new TourReservation(SelectedAlternativeTour.Id, TourName, LoggedInUser.Id, int.Parse(AgainGuestNum), SelectedAlternativeTour.FreeSetsNum, -1, LoggedInUser.Username);
                TourReservation savedAlternativeTour = _tourReservationRepository.Save(newAlternativeTour);
                Guest2MainWindowViewModel.ReservedTours.Add(savedAlternativeTour);
            }
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void AlternativeTourList(Tour tours)
        {
            if (SelectedTour.Location.Country == tours.Location.Country && SelectedTour.Location.City == tours.Location.City && int.Parse(AgainGuestNum) <= tours.MaxGuestNum)
            {
                AlternativeToursMainList.Add(tours);
            }
        }

        private void ReservedAlternativeTourList(Tour tours)
        {
            Location location = _tourRepository.GetLocationById(SelectedTourReservation.IdTour);
            if (location.Country == tours.Location.Country && location.City == tours.Location.City && int.Parse(AgainGuestNum) <= tours.MaxGuestNum)
            {
                AlternativeToursMainList.Add(tours);
            }
        }
    }
}
