using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
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
        public static ObservableCollection<String> Countries { get; set; }
        public Action CloseAction { get; set; }

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

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string TourLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuestNum;
        public string GuestNum
        {
            get => _maxGuestNum;
            set
            {
                if (value != _maxGuestNum)
                {
                    _maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _startDate;
        public string StartDate
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

        private string _endDate;
        public string EndDate
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


        public CreateTourRequestViewModel(User user)
        {
            LoggedInUser = user;
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
            Location location = _locationRepository.FindLocation(SelectedCountry, SelectedCity);

            TourRequest newTourRequest = new TourRequest(TourName, location, TourLanguage, int.Parse(GuestNum), DateOnly.Parse(StartDate), DateOnly.Parse(EndDate), location.Id, Description);

            TourRequest savedTour = _tourRequestService.Save(newTourRequest);
            TourRequestsViewModel.TourRequestsMainList.Add(savedTour);

            CloseAction();
        }
    }
}
