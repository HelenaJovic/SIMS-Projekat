using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourStatisticsGuest2ViewModel : ViewModelBase
    {
        public static string TopGuestNum { get; set; }

        public static string TopAcceptedRequests { get; set; }
        public static string TopRejectedRequests { get; set; }
        public List<TourRequest> TourRequests { get; set; }
        public static ObservableCollection<int> Years { get; set; }
        public static ObservableCollection<string> Languages { get; set; }
        public static User LoggedInUser { get; set; }
        private readonly TourRequestService _tourRequestService;
        private readonly LocationRepository _locationRepository;
        public static ObservableCollection<String> Countries { get; set; }
        public TourStatisticsGuest2ViewModel(User user)
        { 
            LoggedInUser = user;
            _tourRequestService = new TourRequestService();
            _locationRepository = new LocationRepository();
            InitializeProperties();
            BindLocation();
            IsCityEnabled = false;
        }

        private void InitializeProperties()
        {
            Years = new ObservableCollection<int>(_tourRequestService.GetAllYears());
            Languages = new ObservableCollection<string>(_tourRequestService.GetAllLanguages());
            TourRequests = new List<TourRequest>(_tourRequestService.GetAll());
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            TopGuestNum = _tourRequestService.GetTopGuestNumGeneral().ToString();
            TopAcceptedRequests = _tourRequestService.GetTopAcceptedRequests().ToString()+ "%";
            TopRejectedRequests = _tourRequestService.GetTopRejectedRequests().ToString()+ "%";
            TopYearGuestNum = TopGuestNum;
            TopYearAcceptedRequests = TopAcceptedRequests;
            TopYearRejectedRequests = TopRejectedRequests;
            

        }

        private void BindLocation()
        {
            foreach (Tour tour in ToursViewModel.ToursCopyList)
            {
                tour.Location = _locationRepository.GetById(tour.IdLocation);
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
                LocationGuestNum = _tourRequestService.GetLocationGuestNum(SelectedCountry, SelectedCity).ToString();
                OnPropertyChanged(nameof(SelectedCity));
                OnPropertyChanged(nameof(LocationGuestNum));

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
                    if (Cities.Count == 0)
                    {
                        IsCityEnabled = false;
                    }
                    else
                    {
                        IsCityEnabled = true;
                    }
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                    OnPropertyChanged(nameof(IsCityEnabled));
                }
            }
        }

        


        private String _selectedYear;
        public String SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    TopYearGuestNum = _tourRequestService.GetTopGuestByYear(int.Parse(SelectedYear)).ToString();
                    TopYearAcceptedRequests = _tourRequestService.GetTopYearAcceptedRequests(int.Parse(SelectedYear)).ToString()+ "%";
                    TopYearRejectedRequests = _tourRequestService.GetTopYearRejectedRequests(int.Parse(SelectedYear)).ToString()+ "%";
                    OnPropertyChanged(nameof(SelectedYear));
                    OnPropertyChanged(nameof(TopYearGuestNum));
                    OnPropertyChanged(nameof(TopYearAcceptedRequests));
                    OnPropertyChanged(nameof(TopYearRejectedRequests));
                }
            }
        }

        private String _selectedLanguage;
        public String SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    LanguageGuestNum = _tourRequestService.GetLanguageGuestNum(SelectedLanguage).ToString();
                    OnPropertyChanged(nameof(LanguageGuestNum));
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        private string _topYearGuestNum;
        public string TopYearGuestNum
        {
            get { return _topYearGuestNum; }
            set
            {
                if (_topYearGuestNum != value)
                {
                    _topYearGuestNum = value;

                    OnPropertyChanged(nameof(TopYearGuestNum));
                }
            }
        }



        private string _topYearAcceptedRequests;
        public string TopYearAcceptedRequests
        {
            get { return _topYearAcceptedRequests; }
            set
            {
                if (_topYearAcceptedRequests != value)
                {
                    _topYearAcceptedRequests = value;

                    OnPropertyChanged(nameof(TopYearAcceptedRequests));
                }
            }
        }

        private string _topYearRejectedRequests;
        public string TopYearRejectedRequests
        {
            get { return _topYearRejectedRequests; }
            set
            {
                if (_topYearRejectedRequests != value)
                {
                    _topYearRejectedRequests = value;

                    OnPropertyChanged(nameof(TopYearRejectedRequests));
                }
            }
        }

        private string _languageGuestNum;
        public string LanguageGuestNum
        {
            get { return _languageGuestNum; }
            set
            {
                if (_languageGuestNum != value)
                {
                    _languageGuestNum = value;

                    OnPropertyChanged(nameof(LanguageGuestNum));
                }
            }
        }

        private string _locationGuestNum;
        public string LocationGuestNum
        {
            get { return _locationGuestNum; }
            set
            {
                if (_locationGuestNum != value)
                {
                    _locationGuestNum = value;

                    OnPropertyChanged(nameof(LocationGuestNum));
                }
            }
        }

    }
}
