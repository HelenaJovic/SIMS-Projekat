using GalaSoft.MvvmLight.Command;
using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class ViewOneComplexRequestViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }

        public static ObservableCollection<TourRequest> SimpleRequests { get; set; }
        public TourRequest SelectedRequest {get; set;}

        private readonly TourRequestService _tourRequestService;

        private RelayCommand accept;
        public RelayCommand AcceptRequestCommand
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

        public ViewOneComplexRequestViewModel(User user, int complexReuqestId)
        {
            LoggedInUser= user;
            _tourRequestService= new TourRequestService();
            SimpleRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetAllTourRequestByComplexRequestId(complexReuqestId));
            AcceptRequestCommand = new RelayCommand(Execute_AcceptRequest, CanExecute_Command);
        }

        private bool CanExecute_Command()
        {
            return true;
        }

        private void Execute_AcceptRequest()
        {
            
        }
    }
}
