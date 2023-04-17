using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
    public class TourGuestsViewModel : ViewModelBase
    {
        public static ObservableCollection<TourReservation> Users { get; set; }
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITourAttendanceRepository _tourAttendanceRepository;

        public Tour Tour;
        public TourReservation SelectedUser { get; set; }
        public TourPoint CurrentPoint { get; set; }

        public Action CloseAction;

        private RelayCommand _addGuests;
        public RelayCommand AddGuestCommand
        {
            get => _addGuests;
            set
            {
                if (value != _addGuests)
                {
                    _addGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand _done;
        public RelayCommand DoneAddingCommand
        {
            get => _done;
            set
            {
                if (value != _done)
                {
                    _done = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourGuestsViewModel(Tour tour, TourPoint tourPoint)
        {
            _tourReservationRepository = Inject.CreateInstance<ITourReservationRepository>();
            _userRepository = Inject.CreateInstance<IUserRepository>();
            _tourAttendanceRepository = Inject.CreateInstance<ITourAttendanceRepository>();
            CurrentPoint = tourPoint;
            Tour = tour;
            Users = new ObservableCollection<TourReservation>(_tourReservationRepository.GetByTour(CurrentPoint.IdTour));
            //CreateTourCommand = new RelayCommand(Execute_CreateTour, CanExecute_Command);
            AddGuestCommand = new RelayCommand(Execute_AddGuest, CanExecute_Command);
            DoneAddingCommand = new RelayCommand(Execute_DoneAdding, CanExecute_Command);
        }

        private void Execute_DoneAdding(object obj)
        {
            CloseAction();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_AddGuest(object obj)
        {
            User user = _userRepository.GetByUsername(SelectedUser.UserName);
            CurrentPoint.Guests.Add(user);
            string message = SelectedUser.UserName + " are you present at tourpoint " + CurrentPoint.Name;
            string title = "Confirmation window";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            if (result == MessageBoxResult.Yes)
            {
                TourAttendance tourAttendance = new TourAttendance(CurrentPoint.IdTour, Tour.IdUser, SelectedUser.Id, CurrentPoint.Id, false, CurrentPoint.Name);
                TourAttendance savedTA = _tourAttendanceRepository.Save(tourAttendance);
                //_tourReservationRepository.Delete(SelectedUser);
                Users.Remove(SelectedUser);
            }
        }

    }
}
