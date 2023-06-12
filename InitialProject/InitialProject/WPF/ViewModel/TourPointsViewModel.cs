using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
    public class TourPointsViewModel : ViewModelBase
    {
        public static ObservableCollection<TourPoint> Points { get; set; }
        public static ObservableCollection<TourReservation> Users { get; set; }
        public TourReservation SelectedUser { get; set; }
        public Tour SelectedTour { get; set; }
        public TourPoint SelectedPoint { get; set; }
        public int MaxOrder { get; set; }


        private readonly TourPointService _tourPointService;
        private readonly TourService _tourService;
        private readonly MessageBoxService _messageBoxService;
        private readonly TourReservationService _tourReservationService;
        private readonly UserService _userService;
        private readonly TourAttendanceService _tourAttendanceService;


        private bool _active;
        public bool Active
        {
            get => _active;
            set
            {
                if (value != _active)
                {
                    _active = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand suddenEnd;
        public RelayCommand SuddenEndCommand
        {
            get => suddenEnd;
            set
            {
                if (value != suddenEnd)
                {
                    suddenEnd= value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand pause;
        public RelayCommand PauseCommand
        {
            get => pause;
            set
            {
                if (value != pause)
                {
                    pause = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private RelayCommand _checked;
        public RelayCommand CheckedPointCommand
        {
            get => _checked;
            set
            {
                if (value != _checked)
                {
                    _checked = value;
                    OnPropertyChanged();
                }
            }
        }


        public delegate void EventHandler1();

        public event EventHandler1 DoneTracking;


        public TourPointsViewModel(Tour tour)
        {
            SelectedTour = tour;
            _tourPointService = new TourPointService();
            _tourService = new TourService();
            _messageBoxService = new MessageBoxService();
            _tourReservationService = new TourReservationService();
            _userService = new UserService();
            _tourAttendanceService = new TourAttendanceService();
            InitializeCommands();
            Points = new ObservableCollection<TourPoint>(_tourPointService.GetAllByTourId(SelectedTour.Id));
            Users = new ObservableCollection<TourReservation>();
            MaxOrder = _tourPointService.GetMaxOrder(tour.Id);

        }

        private void InitializeCommands()
        {
            AddGuestCommand = new RelayCommand(Execute_AddGuest, CanExecute_Command);
            DoneAddingCommand = new RelayCommand(Execute_DoneAdding, CanExecute_Command);
            SuddenEndCommand = new RelayCommand(Execute_SuddenEnd, CanExecute_Command);
            CheckedPointCommand = new RelayCommand(Execute_CheckedPoint, CanExecute_Command);
        }

        private void Execute_CheckedPoint(object obj)
        {
            if(SelectedPoint.Order == MaxOrder)
            {
                DoneTour(SelectedTour);
            }
            else
            {
                foreach(TourReservation tr in _tourReservationService.GetByTour(SelectedTour.Id))
                {
                    Users.Add(tr);
                }
            }
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_SuddenEnd(object obj)
        {
            _messageBoxService.ShowMessage("Tour is done");
            SelectedTour.Active = false;
            SelectedTour.Ended = false;
            _tourService.Update(SelectedTour);
            DoneTracking?.Invoke();
        }
        private void Execute_DoneAdding(object obj)
        {
            Users.Clear();

        }

        private void Execute_AddGuest(object obj)
        {
            //User user = _userService.GetById(SelectedUser.Id);
            TourAttendance tourAttendance = new TourAttendance(SelectedPoint.IdTour, SelectedTour.IdUser, SelectedUser.Id, SelectedPoint.Id, SelectedUser.UsedVoucher, SelectedPoint.Name);
            _tourAttendanceService.Save(tourAttendance);
            Users.Remove(SelectedUser);

        }

        private void DoneTour(Tour selectedTour)
        {
            _messageBoxService.ShowMessage("Tour is done");

            SelectedTour.Active = false;
            SelectedTour.Ended = false;
            _tourService.Update(SelectedTour);
            DoneTracking?.Invoke();

        }

    }
}
