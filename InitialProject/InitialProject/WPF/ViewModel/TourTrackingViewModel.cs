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
    public class TourTrackingViewModel : ViewModelBase
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTodayTour { get; set; }
        public User LoggedInUser { get; set; }
        public int MaxOrder { get; set; }

        private readonly TourService _tourService;
        

        private RelayCommand startTour;
        public RelayCommand StartTourCommand
        {
            get => startTour;
            set
            {
                if (value != startTour)
                {
                    startTour = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourTrackingViewModel(User user)
        {
            LoggedInUser = user;
            _tourService = new TourService();
            TodayTours = new ObservableCollection<Tour>(_tourService.GetAllByUserAndDate(user, DateTime.Now));
            StartTourCommand = new RelayCommand(Execute_StartTour, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_StartTour(object obj)
        {
            if (SelectedTodayTour != null)
            {
                if (IsUserAvaliable(LoggedInUser))
                {
                    _tourService.StartTour(SelectedTodayTour);
                    TourPoints tourPoints = new TourPoints(SelectedTodayTour);
                    tourPoints.Show();
                }
                else
                    MessageBox.Show("Other tour already started at the same time");
            }
            else
                MessageBox.Show("Choose a tour which you want to start");
        }

        public bool IsUserAvaliable(User user)
        {
            foreach (Tour tour in _tourService.GetAllByUser(user))
            {
                if (tour.Active && !tour.Paused)
                    return false;
            }
            return true;
        }

    }
}
