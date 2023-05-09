using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
    public class CreateTourRequestViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        private readonly TourRequestService _tourRequestService;
        private readonly ILocationRepository _locationRepository;

        public DateOnly startDate1;

        public DateOnly endDate1;
        public static ObservableCollection<String> Countries { get; set; }
        public TourRequest tourRequest = new TourRequest();
        public Action CloseAction { get; set; }

        private ObservableCollection<String> _cities;
        public ObservableCollection<String> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        private bool _isCityEnabled;
        public bool IsCityEnabled
        {
            get { return _isCityEnabled; }
            set
            {
                _isCityEnabled = value;
                OnPropertyChanged(nameof(IsCityEnabled));
            }
        }

        private String _selectedCity;
        public String SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;

            }
        }

        private String _selectedCountry;
        public String SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    Cities = new ObservableCollection<String>(_locationRepository.GetCities(SelectedCountry));
                    IsCityEnabled = true;
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        

        public TourRequest TourRequests
        {
            get { return tourRequest; }
            set
            {
                tourRequest = value;
                OnPropertyChanged(nameof(TourRequests));
            }
        }

        private RelayCommand sendRequestCommand;
        public RelayCommand SendRequestCommand
        {
            get => sendRequestCommand;
            set
            {
                if (value != sendRequestCommand)
                {
                    sendRequestCommand = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get => cancelCommand;
            set
            {
                if (value != cancelCommand)
                {
                    cancelCommand = value;
                    OnPropertyChanged();
                }

            }
        }

        private DateOnly _startDate;
        private DateOnly _endDate;


        public DateTime startDate
        {
            get => _startDate.ToDateTime(TimeOnly.MinValue);
            set
            {
                if (value != _startDate.ToDateTime(TimeOnly.MinValue))
                {
                    _startDate = DateOnly.FromDateTime(value.Date);
                    OnPropertyChanged(nameof(startDate));
                }
            }
        }

        public DateTime endDate
        {
            get => _endDate.ToDateTime(TimeOnly.MinValue);
            set
            {
                if (value != _endDate.ToDateTime(TimeOnly.MinValue))
                {
                    _endDate = DateOnly.FromDateTime(value.Date);
                    OnPropertyChanged(nameof(endDate));
                }
            }
        }



        public CreateTourRequestViewModel(User user)
        {
            LoggedInUser = user;
            startDate = DateTime.Today;
            endDate = DateTime.Today;
            _locationRepository = Inject.CreateInstance<ILocationRepository>();
            _tourRequestService = new TourRequestService();
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            SendRequestCommand = new RelayCommand(Execute_SendRequestCommand, CanExecute_Command);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_CancelCommand(object obj)
        {
            CloseAction();
        }       

        private void Execute_SendRequestCommand(object obj)
        {
            TourRequests.NewStartDate = DateOnly.FromDateTime(startDate);
            TourRequests.NewEndDate = DateOnly.FromDateTime(endDate);
            TourRequests.Validate();

            if (TourRequests.IsValid)
            {
                // Create a new OwnerReview object with the validated values and save it


                Location location = _locationRepository.FindLocation(SelectedCountry, SelectedCity);

                TourRequest newTourRequest = new TourRequest(location, TourRequests.TourLanguage, TourRequests.GuestNum, TourRequests.NewStartDate, TourRequests.NewEndDate, location.Id, TourRequests.Description);

                TourRequest savedTour = _tourRequestService.Save(newTourRequest);
                TourRequestsViewModel.TourRequestsMainList.Add(savedTour);

                CloseAction();
            }
            else
            {
                // Update the view with the validation errors
                OnPropertyChanged(nameof(TourRequests));
            }
        }
    }
}
