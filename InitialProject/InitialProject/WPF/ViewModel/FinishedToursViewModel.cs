using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class FinishedToursViewModel : ViewModelBase
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }

        private readonly TourService _tourService;
        private readonly MessageBoxService _messageBoxService;


        private RelayCommand statistics;
        public RelayCommand StatisticsCommand
        {
            get { return statistics; }
            set
            {
                statistics = value;
            }
        }

        public delegate void EventHandler1(Tour tour);

        public event EventHandler1 StatisticsEvent;

        public FinishedToursViewModel(User user)
        {
            LoggedInUser = user;
            _tourService = new TourService();
            _messageBoxService = new MessageBoxService();
            Tours = new ObservableCollection<Tour>(_tourService.GetFinishedToursByUser(user));

            StatisticsCommand = new RelayCommand(Execute_Statistics, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_Statistics(object obj)
        {
            if(SelectedTour != null)
            {
                StatisticsEvent?.Invoke(SelectedTour);
            }
            else
            {
                _messageBoxService.ShowMessage("Please, first select a tour");
            }
        }
    }
}
