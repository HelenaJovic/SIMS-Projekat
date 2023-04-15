using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class FindAlternativeToursViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        public Tour SelectedTour { get; set; }
        public TourReservation TourReservation { get; set; }
        public Tour AlternativeTour { get; set; }
        private string _againGuestNum;
        public ICommand FindAlternativeTourCommand { get; set; }
        public ICommand CancelFindingAltrnativeTour { get; set; }

        public Action CloseAction { get; set; }  //kako da uradim close windowa nekog
        private readonly TourReservationService _tourReservationService;

        public string AgainGuestNum
        {
            get => _againGuestNum;
            set
            {
                if (value != _againGuestNum)
                {
                    _againGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FindAlternativeToursViewModel(User user, Tour tour, TourReservation reservation)
        {
            LoggedInUser=user;
            SelectedTour=tour;
            TourReservation=reservation;
            _tourReservationService = new TourReservationService();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            FindAlternativeTourCommand =  new RelayCommand(Execute_FindAlternativeTourCommand, CanExecute_Command);
            CancelFindingAltrnativeTour = new RelayCommand(Execute_CancelFindingAltrnativeTour, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_CancelFindingAltrnativeTour(object obj)
        {
            CloseAction();
        }

        private void Execute_FindAlternativeTourCommand(object obj)
        {
            if (TourReservation != null)
            {
                RemoveFromReservedTours();
            }
            AlternativeTours alternativeTours = new AlternativeTours(LoggedInUser, SelectedTour, TourReservation, AgainGuestNum, AlternativeTour);
            alternativeTours.Show();
            CloseAction();
        }

        private void RemoveFromReservedTours()
        {
            TourReservation.FreeSetsNum += TourReservation.GuestNum;
            _tourReservationService.Delete(TourReservation);
            Guest2MainWindowViewModel.ReservedTours.Remove(TourReservation);
        }
    }
}
