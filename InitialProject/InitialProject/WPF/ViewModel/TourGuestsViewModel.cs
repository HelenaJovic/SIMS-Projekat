﻿using InitialProject.Commands;
using InitialProject.Domain.Model;
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
        private TourReservationRepository _tourReservationRepository;
        private UserRepository _userRepository;
        private TourAttendanceRepository _tourAttendanceRepository;

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

        public TourGuestsViewModel(TourPoint tourPoint)
        {
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            _tourAttendanceRepository = new TourAttendanceRepository();
            CurrentPoint = tourPoint;
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
                TourAttendance tourAttendance = new TourAttendance(CurrentPoint.IdTour, SelectedUser.Id, CurrentPoint.Id);
                TourAttendance savedTA = _tourAttendanceRepository.Save(tourAttendance);
                _tourReservationRepository.Delete(SelectedUser);
                Users.Remove(SelectedUser);
            }
        }

    }
}
