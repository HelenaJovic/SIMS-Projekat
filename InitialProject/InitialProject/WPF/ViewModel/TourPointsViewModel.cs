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
        public Tour SelectedTour { get; set; }
        private readonly TourPointService _tourPointService;
        private readonly TourService _tourService;
        public int MaxOrder { get; set; }
        public int Order = 0;
        public Action CloseAction { get; set; }


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


        public TourPointsViewModel(Tour tour)
        {
            SelectedTour = tour;
            _tourPointService = new TourPointService();
            _tourService = new TourService();
            Points = new ObservableCollection<TourPoint>(_tourPointService.GetAllByTourId(SelectedTour.Id));
            MaxOrder = _tourPointService.FindMaxOrder(tour.Id); //Execute_CreateTour
            SuddenEndCommand = new RelayCommand(Execute_SuddenEnd, CanExecute_Command);
            PauseCommand = new RelayCommand(Execute_Pause, CanExecute_Command);
        }

        private void Execute_Pause(object obj)
        {
            MessageBox.Show("Tour is paused");
            _tourService.PauseTour(SelectedTour);
            CloseAction();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_SuddenEnd(object obj)
        {
            MessageBox.Show("Tour is done");
            _tourService.EndTour(SelectedTour);
            CloseAction();
        }

        public void Changed(object sender)
        {
            Order++;
            if (Order == MaxOrder)
            {
                MessageBox.Show("Tour is done");
                _tourService.EndTour(SelectedTour);
                CloseAction();
            }
            else
            {
                foreach (TourPoint point in Points)
                {
                    if (point.Active && !point.GuestAdded)
                    {

                        point.GuestAdded = true;
                        _tourPointService.Update(point);
                        TourGuests tourGuests = new TourGuests(SelectedTour,point);
                        tourGuests.Show();
                    }
                }
            }
        }

    }
}
