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

        public delegate void EventHandler1();

        public event EventHandler1 FilterEvent;

        public AcceptTourRequestViewModel(User user)
        {
            LoggedInUser = user;
            _tourRequestService= new TourRequestService();
            Requests = new ObservableCollection<TourRequest>(_tourRequestService.GetAll());
            RequestsCopyList = new ObservableCollection<TourRequest>(_tourRequestService.GetAll());

            FilterCommand = new RelayCommand(Execute_Filter, CanExecute_Command);
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
