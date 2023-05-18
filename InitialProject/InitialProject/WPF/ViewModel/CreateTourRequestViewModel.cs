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
        
        private readonly TourRequestService _tourRequestService;
        private readonly LocationService _locationService;
        public User LoggedInUser { get; set; }
        public Action CloseAction { get; set; }
        public static ObservableCollection<String> Countries { get; set; }

        public CreateTourRequestViewModel(User user)
        {
            LoggedInUser = user;
            _locationService = new LocationService();
            _tourRequestService = new TourRequestService();
            Countries = new ObservableCollection<String>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<String>();
            SendRequestCommand = new RelayCommand(Execute_SendRequestCommand, CanExecute_Command);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
        }

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
                    Cities = new ObservableCollection<String>(_locationService.GetCities(SelectedCountry));
                    IsCityEnabled = true;
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }


        private string _validationResult;
        public string ValidationResult
        {
            get { return _validationResult; }
            set
            {
                _validationResult = value;
                OnPropertyChanged(nameof(ValidationResult));
            }
        }

        private string _validationResult2;
        public string ValidationResult2
        {
            get { return _validationResult2; }
            set
            {
                _validationResult2 = value;
                OnPropertyChanged(nameof(ValidationResult2));
            }
        }


        public TourRequest tourRequest = new TourRequest();

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

        private string _startDate;
        private string _endDate;

        public string startDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string endDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_CancelCommand(object obj)
        {
            CloseAction();
        }

        private bool IsCityValid()
        {
            if (SelectedCity  == null)
            {
                ValidationResult = "City is required";
                return false;
            }
            ValidationResult = "";
            return true;
        }

        private bool IsCountryValid()
        {
            if (SelectedCountry  == null)
            {
                ValidationResult2 = "Country is required";
                return false;
            }
            ValidationResult2 = "";
            return true;
        }

        private void Execute_SendRequestCommand(object obj)
        {
            TourRequests.NewStartDate = DateOnly.Parse(startDate);
            TourRequests.NewEndDate = DateOnly.Parse(endDate);
            TourRequests.Validate();

            bool cityValid = IsCityValid();
            bool countryValid = IsCountryValid();

            if (TourRequests.IsValid && cityValid && countryValid)
            {
                CreateTourRequestValid();
            }
            else
            {
                OnPropertyChanged(nameof(TourRequests));
            }
        }

        private void CreateTourRequestValid()
        {
            Location location = _locationService.FindLocation(SelectedCountry, SelectedCity);

            TourRequest newTourRequest = new TourRequest(location, LoggedInUser.Id, TourRequests.TourLanguage, TourRequests.GuestNum, TourRequests.NewStartDate, TourRequests.NewEndDate, location.Id, TourRequests.Description);


             TourRequest savedTour = _tourRequestService.Save(newTourRequest);
             TourRequestsViewModel.TourRequestsMainList.Add(savedTour);

            CloseAction();
        }
    }
}
