using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
    public class CreateTourByLanguageViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        public string TopLanguage { get; set; }


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


        private Tour tour = new Tour();
        public Tour Tour
        {
            get { return tour; }
            set
            {
                tour = value;
                OnPropertyChanged("Tour");
            }
        }

        private RelayCommand create;
        public RelayCommand CreateByLanguageCommand
        {
            get => create;
            set
            {
                if (value != create)
                {
                    create = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand images;
        public RelayCommand AddImagesCommand
        {
            get => images;
            set
            {
                if (value != images)
                {
                    images = value;
                    OnPropertyChanged();
                }

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




        private string _startTime;
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _startDate;
        public string Date
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

        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private int maxGuestNum;
        public int MaxGuestNum
        {
            get => maxGuestNum;
            set
            {
                if (value != maxGuestNum)
                {
                    maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }


        private readonly IImageRepository _imageRepository;
        private readonly LocationService _locationService;
        private readonly TourService _tourService;
        private readonly TourPointService _tourPointService;
        private readonly TourRequestService _tourRequestService;
        public List<string> ImagePaths { get; set; }
        public static ObservableCollection<String> Countries { get; set; }

        public delegate void EventHandler1();

        public event EventHandler1 EndCreatingEvent;



        public CreateTourByLanguageViewModel(User user) 
        {
            LoggedInUser = user;
            _imageRepository = Inject.CreateInstance<IImageRepository>();
            _locationService= new LocationService();
            _tourService = new TourService();
            _tourPointService= new TourPointService();
            _tourRequestService= new TourRequestService();


            InitializeProperties();

            TopLanguage = _tourRequestService.GetTopLanguage();
            CreateByLanguageCommand = new RelayCommand(Execute_CreateByLanguage, CanExecute_Command);
            AddImagesCommand = new RelayCommand(Execute_AddImages, CanExecute_Command);
        }

        private void InitializeProperties()
        {
            Countries = new ObservableCollection<String>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<String>();
            MaxGuestNum = 10;
            Duration = 1;
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

        private bool IsCityValid()
        {
            if (SelectedCity == null)
            {
                ValidationResult = "City is required";
                return false;
            }
            ValidationResult = "";
            return true;
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_CreateByLanguage(object obj)
        {
            Tour.Validate();
            bool validCity = IsCityValid();

            if (Tour.IsValid && validCity)
            {
                TimeOnly _startTime = ConvertTime(StartTime);

                Location location = _locationService.FindLocation(SelectedCountry, SelectedCity);
                
               Tour newTour = new Tour(tour.Name, location, TopLanguage, MaxGuestNum, DateOnly.Parse(Date), _startTime, Duration, MaxGuestNum, false, LoggedInUser.Id, location.Id, false); ;

                Tour savedTour = _tourService.Save(newTour);
                GuideMainWindowViewModel.Tours.Add(newTour);

                CreatePoints(savedTour);
                CreateImages(savedTour);

                EndCreatingEvent?.Invoke();

            }
            else
            {
                OnPropertyChanged(nameof(Tour));
            }

        }

        private void Execute_AddImages(object obj)
        {
            ImagePaths = FileDialogService.GetImagePaths("Resources\\Images\\Tours", "/Resources/Images");
        }

        private void CreateImages(Tour savedTour)
        {
            foreach (string name in ImagePaths)
            {
                Image newImage = new Image(name, 0, savedTour.Id, 0);
                Image savedImage = _imageRepository.Save(newImage);
                savedTour.Images.Add(savedImage);
            }
        }

        private void CreatePoints(Tour savedTour)
        {
            string[] pointsNames = Tour.Points.Split(",");
            int order = 1;
            foreach (string name in pointsNames)
            {
                TourPoint newTourPoint = new TourPoint(name, false, false, order, savedTour.Id);
                TourPoint savedTourPoint = _tourPointService.Save(newTourPoint);
                //savedTour.Points.Add(savedTourPoint);
                order++;
            }

        }

        public TimeOnly ConvertTime(string times)
        {
            StartTimes time = (StartTimes)Enum.Parse(typeof(StartTimes), times);
            TimeOnly startTime;
            switch (time)
            {
                case StartTimes._8AM:
                    startTime = new TimeOnly(8, 0);
                    break;
                case StartTimes._10AM:
                    startTime = new TimeOnly(10, 0);
                    break;
                case StartTimes._12PM:
                    startTime = new TimeOnly(12, 0);
                    break;
                case StartTimes._2PM:
                    startTime = new TimeOnly(14, 0);
                    break;
                case StartTimes._4PM:
                    startTime = new TimeOnly(16, 0);
                    break;
                case StartTimes._6PM:
                    startTime = new TimeOnly(18, 0);

                    break;
            }
            return startTime;
        }

    }
}
