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
        public static double TopGuestNum { get; set; }
        public static double TopYearGuestNum { get; set; }
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
            Years = new ObservableCollection<int>(GetAllYears());
            Languages = new ObservableCollection<string>(GetAllLanguages());
            TourRequests = new List<TourRequest>(_tourRequestService.GetAll());
            TopGuestNum = GetTopGuestNumGeneral();

            _locationRepository = new LocationRepository();
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            IsCityEnabled = false;
            BindLocation();
        }

        private IEnumerable<string> GetAllLanguages()
        {
            List<string> languages = new List<string>();
            foreach (TourRequest t in _tourRequestService.GetAll())
            {
                if (!languages.Contains(t.Language))
                {
                    languages.Add(t.Language);
                }
            }
            return languages;
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

        private double GetTopGuestByYear(int year)
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in TourRequests)
            {
                if (tourRequest.StartDate.Year == year)
                {
                    if (tourRequest.Status.Equals(RequestType.Approved))
                    {
                        sum += tourRequest.MaxGuestNum;
                        brojac++;
                    }
                }
                
            }
            return sum/brojac;
        }

        private double GetTopGuestNumGeneral()
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in TourRequests)
            {
                if(tourRequest.Status.Equals(RequestType.Approved))
                {
                    sum += tourRequest.MaxGuestNum;
                    brojac++;
                }
            }
            return sum/brojac;
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
                    TopYearGuestNum = GetTopGuestByYear(int.Parse(SelectedYear));
                    
                    OnPropertyChanged(nameof(TopYearGuestNum));
                    OnPropertyChanged(nameof(SelectedYear));
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

                    OnPropertyChanged(nameof(_selectedLanguage));
                }
            }
        }


        private List<int> GetAllYears()
        {
            List<int> years = new List<int>();
            foreach (TourRequest t in _tourRequestService.GetAll())
            {
                if (!years.Contains(t.StartDate.Year))
                {
                    years.Add(t.StartDate.Year);
                }
            }
            return years;
        }
    }
}
