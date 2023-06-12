using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Image = InitialProject.Domain.Model.Image;
using System.Windows.Input;
using InitialProject.Commands;
using System.Xml.Linq;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using System.Security.Policy;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using InitialProject.WPF.Converters;
using System.Diagnostics.Metrics;
using System.Timers;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class CreateTourViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        private readonly TourService _tourService;
        private readonly TourPointService _tourPointService;
        private readonly LocationService _locationService;
        private readonly IImageRepository _imageRepository;

        public static ObservableCollection<String> Countries { get; set; }
        public List<string> ImagePaths { get; set; }


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


        public Tour tour = new Tour();

        public Tour Tour
        {
            get { return tour; }
            set
            {
                tour = value;
                OnPropertyChanged("Tour");
            }
        }

        private RelayCommand confirmCreate;
        public RelayCommand CreateTourCommand
        {
            get => confirmCreate;
            set
            {
                if (value != confirmCreate)
                {
                    confirmCreate = value;
                    OnPropertyChanged();
                }

            }
        } 

        private RelayCommand addImages;
        public RelayCommand AddImagesCommand
        {
            get => addImages;
            set
            {
                if (value != addImages)
                {
                    addImages = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand demo;
        public RelayCommand DemoCommand
        {
            get => demo;
            set
            {
                if (value != demo)
                {
                    demo = value;
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


        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if(value != duration)
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

        public delegate void EventHandler1();

        public event EventHandler1 EndCreatingEvent;

        public delegate void EventHandler2();

        public event EventHandler2 DemoEvent;

        public bool IsDemoRunning;


        public CreateTourViewModel(User user)
        {
            LoggedInUser = user;
            _locationService = new LocationService();
            _tourService = new TourService();
            _tourPointService = new TourPointService();
            _imageRepository = Inject.CreateInstance<IImageRepository>();
            InitializeProperties();
            InitializeCommands();
            
            IsDemoRunning = false;
            

        }

        private void InitializeCommands()
        {
            CreateTourCommand = new RelayCommand(Execute_CreateTour, CanExecute_Command);
            AddImagesCommand = new RelayCommand(Execute_AddImages, CanExecute_Command);
            DemoCommand = new RelayCommand(Execute_Demo, CanExecute_Command);
        }

        private void Execute_Demo(object obj)
        {
            DemoEvent?.Invoke();
        }

        private void InitializeProperties()
        {
            Countries = new ObservableCollection<String>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<String>();
            MaxGuestNum = 10;
            Duration = 1;
        }


        private bool CanExecute_Command(object parameter)
        {
            return true;
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

        private string _validationResult3;
        public string ValidationResult3
        {
            get { return _validationResult3; }
            set
            {
                _validationResult3 = value;
                OnPropertyChanged(nameof(ValidationResult3));
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
        private bool ArePointsValid()
        {
            string[] pointsNames = Tour.Points.Split(",");
            if(pointsNames.Length < 2 )
            {
                ValidationResult2 = "You need at least 2 tour points";
                return false;
            }
            ValidationResult2 = "";
            return true;
        }
        private bool IsImageSelected()
        {
            if (ImagePaths == null)
            {
                ValidationResult3 = "You need at least 2 images";
                return false;
            }
            ValidationResult3 = "";
            return true;
        }

        private void Execute_CreateTour(object sender)
        {

            Tour.Validate();
            bool validCity = IsCityValid();
            bool validPoints = ArePointsValid();
            bool validImage = IsImageSelected();

            if (Tour.IsValid && validCity && validPoints && validImage)
            {
                TimeOnly _startTime = ConvertTime();

                Location location = _locationService.FindLocation(SelectedCountry, SelectedCity);


                Tour newTour = new Tour(tour.Name, location, tour.Language, MaxGuestNum, DateOnly.Parse(Date), _startTime, Duration, MaxGuestNum, false, LoggedInUser.Id, location.Id, false); ;

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

        public TimeOnly ConvertTime()
        {
            TimeOnly time;
            switch (_startTime)
            {
                case "08:00AM":
                    time = new TimeOnly(8, 0);
                    break;
                case "10:00AM":
                    time = new TimeOnly(10, 0);
                    break;
                case "12:00PM":
                    time = new TimeOnly(12, 0);
                    break;
                case "14:00PM":
                    time = new TimeOnly(14, 0);
                    break;
                case "16:00PM":
                    time = new TimeOnly(16, 0);
                    break;
                case "18:00PM":
                    time = new TimeOnly(18, 0);
                    break;
                case "20:00PM":
                    time = new TimeOnly(20, 0);
                    break;
            }
            return time;
        }
       
    }
}
