using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class AcceptTourRequestViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<TourRequest> Requests { get; set; }
        public static ObservableCollection<TourRequest> RequestsCopyList { get; set; }

        public TourRequest SelectedRequest { get; set; }

        private readonly TourRequestService _tourRequestService;


        private RelayCommand filter;
        public RelayCommand FilterCommand
        {
            get => filter;
            set
            {
                if (value != filter)
                {
                    filter = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand accept;
        public RelayCommand AcceptCommand
        {
            get => accept;
            set
            {
                if (value != accept)
                {
                    accept = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand statistics;
        public RelayCommand StatisticsCommand
        {
            get => statistics;
            set
            {
                if (value != statistics)
                {
                    statistics = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand createRequest;
        public RelayCommand CreateRequestCommand
        {
            get => createRequest;
            set
            {
                if (value != createRequest)
                {
                    createRequest = value;
                    OnPropertyChanged();
                }
            }
        }


        public delegate void EventHandler1();
        public delegate void EventHandler2(TourRequest request);
        public delegate void EventHandler3();
        public delegate void EventHandler4();

        public event EventHandler1 FilterEvent;
        public event EventHandler2 AcceptEvent;
        public event EventHandler3 StatistcisEvent;
        public event EventHandler4 CreateRequestEvent;

        public AcceptTourRequestViewModel(User user)
        {
            LoggedInUser = user;
            _tourRequestService= new TourRequestService();
            Requests = new ObservableCollection<TourRequest>(_tourRequestService.GetAllUnaccepted());
            RequestsCopyList = new ObservableCollection<TourRequest>(_tourRequestService.GetAllUnaccepted());

            FilterCommand = new RelayCommand(Execute_Filter, CanExecute_Command);
            AcceptCommand = new RelayCommand(Execute_Accept, CanExecute_Command);
            StatisticsCommand = new RelayCommand(Execute_Statistics, CanExecute_Command);
            CreateRequestCommand = new RelayCommand(Execute_CreateRequest, CanExecute_Command);
        }

        private void Execute_CreateRequest(object obj)
        {
            CreateRequestEvent?.Invoke();
        }

        private void Execute_Statistics(object obj)
        {
            StatistcisEvent?.Invoke();
        }

        private void Execute_Accept(object obj)
        {
            AcceptEvent?.Invoke(SelectedRequest);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_Filter(object obj)
        {
            FilterEvent?.Invoke();
        }
    }
}
