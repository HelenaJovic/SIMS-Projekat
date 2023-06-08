using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModel
{
    public class TourStatisticsGuest2ViewModel : ViewModelBase
    {
        public static User LoggedInUser { get; set; }
        public static string TopGuestNum { get; set; }
        public static double TopAcceptedRequests { get; set; }
        public static double TopRejectedRequests { get; set; }
        public List<TourRequest> TourRequests { get; set; }
        public static ObservableCollection<int> Years { get; set; }
        public static ObservableCollection<string> Languages { get; set; }
        public static ObservableCollection<String> Countries { get; set; }
        private readonly TourRequestService _tourRequestService;
        private readonly LocationRepository _locationRepository;

        private SeriesCollection _generalPie;

        public SeriesCollection GeneralPie
        {
            get => _generalPie;
            set
            {
                if (_generalPie != value)
                {
                    _generalPie = value;
                    OnPropertyChanged("GeneralPie");
                }
            }
        }
        private SeriesCollection _selectedYearPie;

        public SeriesCollection SelectedYearPie
        {
            get => _selectedYearPie;
            set
            {
                if (_selectedYearPie != value)
                {
                    _selectedYearPie = value;
                    OnPropertyChanged("SelectedYearPie");
                }
            }
        }


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
            Languages = new ObservableCollection<string>(_tourRequestService.GetLanguages());
            TourRequests = new List<TourRequest>(_tourRequestService.GetAll());
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            TopGuestNum = _tourRequestService.GetTopGuestNumGeneral().ToString();
            //TopAcceptedRequests = _tourRequestService.GetTopAcceptedRequests().ToString();
            //TopRejectedRequests = _tourRequestService.GetTopRejectedRequests().ToString();
            TopAcceptedRequests = _tourRequestService.GetTopAcceptedRequests();
            TopRejectedRequests = _tourRequestService.GetTopRejectedRequests();
            TopYearGuestNum = TopGuestNum;
            TopYearAcceptedRequests = TopAcceptedRequests;
            TopYearRejectedRequests = TopRejectedRequests;





            ////////////////
            GeneralPie = new SeriesCollection();
            CreateGeneralPie();

            SelectedYearPie = new SeriesCollection();
            CreateSelectedYearPie();
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
                    TopYearAcceptedRequests = _tourRequestService.GetTopYearAcceptedRequests(int.Parse(SelectedYear));
                    TopYearRejectedRequests = _tourRequestService.GetTopYearRejectedRequests(int.Parse(SelectedYear));
                    OnPropertyChanged(nameof(SelectedYear));
                    OnPropertyChanged(nameof(TopYearGuestNum));
                    OnPropertyChanged(nameof(TopYearAcceptedRequests));
                    OnPropertyChanged(nameof(TopYearRejectedRequests));
                    OnPropertyChanged(nameof(SelectedYearPie));
                    OnPropertyChanged(nameof(CreateSelectedYearPie));
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



        private double _topYearAcceptedRequests;
        public double TopYearAcceptedRequests
        {
            get { return _topYearAcceptedRequests; }
            set
            {
                if (_topYearAcceptedRequests != value)
                {
                    _topYearAcceptedRequests = value;

                    OnPropertyChanged(nameof(TopYearAcceptedRequests));
                    OnPropertyChanged(nameof(SelectedYearPie));
                    OnPropertyChanged(nameof(CreateSelectedYearPie));
                }
            }
        }

        private double _topYearRejectedRequests;
        public double TopYearRejectedRequests
        {
            get { return _topYearRejectedRequests; }
            set
            {
                if (_topYearRejectedRequests != value)
                {
                    _topYearRejectedRequests = value;

                    OnPropertyChanged(nameof(TopYearRejectedRequests));
                    OnPropertyChanged(nameof(SelectedYearPie));
                    OnPropertyChanged(nameof(CreateSelectedYearPie));
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

        private void CreateGeneralPie()
        {
            GeneralPie.Add(new PieSeries
            {
                Title = "Accepted requests",
                Values = new ChartValues<double> { TopAcceptedRequests },
                DataLabels = true,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0000cd")),
                LabelPoint = point => $"{point.Y} ({point.Participation:P0})"
            });
            GeneralPie.Add(new PieSeries
            {
                Title = "Rejected requests",
                Values = new ChartValues<double> { TopRejectedRequests },
                DataLabels = true,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff69b4")),
                LabelPoint = point => $"{point.Y} ({point.Participation:P0})"
            });
        }

        private void CreateSelectedYearPie()
        {
            SelectedYearPie.Add(new PieSeries
            {
                Title = "Accepted requests",
                Values = new ChartValues<double> { TopYearAcceptedRequests },
                DataLabels = true,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0000cd")),
                LabelPoint = point => $"{point.Y} ({point.Participation:P0})"
            });
            SelectedYearPie.Add(new PieSeries
            {
                Title = "Rejected requests",
                Values = new ChartValues<double> { TopYearRejectedRequests },
                DataLabels = true,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff69b4")),
                LabelPoint = point => $"{point.Y} ({point.Participation:P0})"
            });
        }

    }
}
